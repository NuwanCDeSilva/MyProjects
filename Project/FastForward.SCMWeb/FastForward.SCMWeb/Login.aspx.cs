using FastForward.SCMWeb.Common;
using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb
{
    public partial class Login : BasePage
    {
        string error;
        private string userID, password, userName;
        private int _loginRetryInCounter = 0;
        private int _loginRetryOutCounter = 0;
        protected LoginUser _loginUser { get { return (LoginUser)Session["_loginUser"]; } set { Session["_loginUser"] = value; } }
        private readonly string invaldlogin = "The user name or password you entered isn't correct. Try entering it again.";
        private readonly string invaldconnection = "Server not connected......!!!";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GlbisLoging = true;
                if (!Page.IsPostBack)
                {
                    _loginUser = new LoginUser();
                    BindUserCompanyDDLData(ddlCompany, "");
                    txtUserName.Focus();

                }
            }
            catch (Exception ex)
            {

                msg(invaldconnection);
                return;
            }
        }

        public void BindUserCompanyDDLData(DropDownList ddl, String username)
        {
            //ClearMsg();
            try
            {
                ddl.Items.Clear();
                ddl.DataSource = CHNLSVC.Security.GetUserCompany(username);

                if (!string.IsNullOrEmpty(username))
                {
                    List<SystemUserCompany> _systemUserCompanyList = CHNLSVC.Security.GetUserCompany(username);
                    if (_systemUserCompanyList != null)

                        foreach (SystemUserCompany item in _systemUserCompanyList)
                        {
                            ddl.Items.Add(new ListItem(item.MasterComp.Mc_desc, item.SEC_COM_CD));
                        }
                    if (_systemUserCompanyList == null)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                //DisplayMessages(ex.Message);
                msg("Server error");
            }
        }
        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            // ClearMsg();
            try
            {
                if (!string.IsNullOrEmpty(txtUserName.Text))
                {
                    BindUserCompanyDDLData(ddlCompany, txtUserName.Text.ToUpper().ToString());
                    txtPassword.Focus();
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                // MessageBox.Show(err.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // ClearMsg();
            Login_Process();
        }
        #region Login Process
        public bool CheckServerDateTime()
        {
            //function provided by Chamal on 19/2/2013
            if (CHNLSVC.Security.GetServerDateTime().Date != DateTime.Now.Date)
            {
                //this.Cursor = Cursors.Default;
                string value = "Your machine date conflict with the server date! \nPlease contact system administrator..";
                msg(value);
                // MessageBox.Show("Your machine date conflict with the server date! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            TimeZone zone = TimeZone.CurrentTimeZone;
            TimeSpan offset = zone.GetUtcOffset(DateTime.Now);

            string _serverUTC = CHNLSVC.Security.GetServerTimeZoneOffset();
            string _localUTC = offset.ToString();

            if (_serverUTC != _localUTC)
            {
                //this.Cursor = Cursors.Default;
                string value = "Your machine time zone conflict with the server time zone! \nPlease contact system administrator....";
                msg(value);
                return false;
            }

            return true;
        }
        public string GetIP()
        {
            IPHostEntry host;
            string localIP = "";
            Session["HostName"] = Dns.GetHostName().ToString();
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
        public string getLogUserIP()
        {
            Session["HostName"] = Dns.GetHostName().ToString();
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(Request.ServerVariables["REMOTE_ADDR"].ToString()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            if (IP4Address != String.Empty)
            {
                return IP4Address;
            }

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            return IP4Address;
        }
        public string GetSBU()
        {
            string SBU = "";
            DataTable SBUTbl = CHNLSVC.Security.GetSBU_User(ddlCompany.SelectedValue, txtUserName.Text.ToUpper(), null);
            foreach (DataRow dr in SBUTbl.Rows)
            {
                int value = Convert.ToInt32(dr[2].ToString());
                if (value == 1)
                {
                    SBU = dr[1].ToString();
                }
            }
            return SBU;
        }
        protected void LinkButton9_Click(object sender, EventArgs e)
        {
            divwarnning.Visible = false;
        }
        private void Login_Process()
        {
            //if (txtconformmessageValue.va)
            //{

            //}
            ClearMsg();
            try
            {
                if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                    if (ddlCompany.Items.Count <= 0)
                    {
                        string msgv = "Your have not assign any companies to login.";
                        msg(msgv);
                        // MessageBox.Show("Your have not assign any companies to login.", "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //Application.Exit();
                        // Response.Redirect(Request.RawUrl, false);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(ddlCompany.Text))
                {

                    if (CheckServerDateTime() == false)
                    {
                        //Application.Exit();
                        Response.Redirect(Request.RawUrl, false);
                        return;
                    }

                    CultureInfo ci = CultureInfo.CurrentCulture;

                    bool _dateFormat = false;
                    if (ci.DateTimeFormat.ShortDatePattern.ToString().ToUpper() == "DD/MMM/YYYY") _dateFormat = true;
                    if (ci.DateTimeFormat.ShortDatePattern.ToString().ToUpper() == "DD-MMM-YY") _dateFormat = true;

                    //if (_dateFormat == false)
                    //{
                    //    string mss = "Your system date format is incorrect! \nPlease contact system administrator....";
                    //    msg(mss);
                    //    //MessageBox.Show("Your system date format is incorrect! \nPlease contact system administrator....", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    //Application.Exit();
                    //    //Response.Redirect(Request.RawUrl, false);
                    //    return;
                    //}

                    string _msg = string.Empty;
                    string _msgTitle = string.Empty;
                    int _msgStatus = 0;

                    Session["UserID"] = txtUserName.Text.ToString().ToUpper();
                    Session["UserCompanyCode"] = ddlCompany.Text.ToString().ToUpper();
                    Session["UserIP"] = getLogUserIP();// GetIP().ToString();
                    string _WindowsLogonName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    string _WindowsUser = Environment.UserName;
                    Session["UserSBU"] = GetSBU().ToString();

                    string _ipAdd = string.Empty;
                    string _pcName = string.Empty;
                    string _loginTime = string.Empty;
                    bool isLog = CHNLSVC.Security.IsActiveSessions(txtUserName.Text.ToString().ToUpper(), ddlCompany.Text.ToString().ToUpper(), out _ipAdd, out _pcName, out _loginTime);
                    if (txtUserName.Text.ToString().ToUpper() != "ADMIN")
                    {

                        if (isLog == true)
                        {
                            string msg = "Active session is exist for the user '" + txtUserName.Text.ToString() + "'" + "\n \n * Computer Name       :  " + _pcName + "\n * Computer IP              :  " + _ipAdd + "\n * Logged on                 :  " + _loginTime + "\n\nDo you want to close the existing session and log to system?";
                            //  _msg = "Active session is exist for the user '" + txtUserName.Text.ToString() + "'" + "\n \n * Computer Name       :  " + _pcName + "\n * Computer IP              :  " + _ipAdd + "\n * Logged on                 :  " + _loginTime + "\n\n";
                            lblMssg.Text = "Active session is exist for the user '" + txtUserName.Text.ToString() + "'";
                            lblMssg1.Text = " Computer Name       :  " + _pcName + "";
                            lblMssg2.Text = "Computer IP              :  " + _ipAdd + "";
                            lblMssg3.Text = "Logged on                 :  " + _loginTime + "";
                            lblMssg4.Text = "Do you want to close the existing session and log to system?";
                            lblAlertValue.Text = "1";
                            lblpsw.Text = txtPassword.Text.ToString();
                            UserPopoup.Show();

                            // string msg = "Active session is exist for the user";
                            // string msg1 = txtUserName.Text.ToString() ;
                            // string msg2 = "Computer Name:";
                            // string str = "Are you sure, you want to Approve this Record?";
                            //  this.ClientScript.RegisterStartupScript(typeof(Page), "Popup", "ConfirmApproval('" + str + "');", true);
                            // ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "Confirm('" + msg + msg1 + '\n' + msg2 + "')", true);
                            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "Confirms()", true);

                            return;
                            // Response.Write("<script language='javascript'>Confirm(" + _msg + ");</script>");

                            //if (MessageBox.Show(_msg, "Fast Forward - SCM-II", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button3) == DialogResult.Yes)
                            //{
                            //CHNLSVC.Security.ExitLoginSession(txtUserName.Text.ToString().ToUpper(), ddlCompany.Text.ToString().ToUpper(), "-1");
                            //}
                            //else
                            //{
                            //    //Application.Exit();

                            //}
                        }
                    }


                    //Session["SessionID"] = Convert.ToString(CHNLSVC.Security.SaveLoginSession(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), Session["UserIP"].ToString(),"", _WindowsLogonName, _WindowsUser));
                    if (Session["LoginCounter"] != null)
                    {
                        var x = Convert.ToInt16(Session["LoginCounter"].ToString());
                        if (Convert.ToInt16(Session["LoginCounter"].ToString()) >= 5)
                        {
                            _loginRetryInCounter = Convert.ToInt16(Session["LoginCounter"].ToString());
                            Session["LoginCounter"] = null;
                        }
                    }

                    _loginUser = CHNLSVC.Security.LoginToSystem(txtUserName.Text.ToString().ToUpper(), txtPassword.Text.ToString(), ddlCompany.Text.ToString().ToUpper(), BaseCls.GlbVersionNo, Session["UserIP"].ToString(), Session["HostName"].ToString(), _loginRetryInCounter, out _loginRetryOutCounter, out _msgStatus, out _msg, out _msgTitle);
                    SystemUser _systemuser = CHNLSVC.Security.GetUserByUserID(txtUserName.Text.ToString().ToUpper());
                    if (_systemuser != null)
                    {
                        if (_systemuser.Se_pw_chng_dt.Date.AddDays(45) <= DateTime.Now.Date)
                        {
                            if (_loginUser.Pw_expire && _msgStatus == 1)
                            {
                                _msg = "Your password has expired and must be changed!";
                                msg(_msg);
                                ShowChangePasswordPanel();
                                return;
                            }
                        }
                    }
                    if (_msgStatus == 1 || _msgStatus == -3)
                    {
                        Session["LoginCounter"] = null;
                        Base Base = new Base();
                        Base.SetLoginCacheLayer(_loginUser, _WindowsLogonName, _WindowsUser);

                        List<PriceDefinitionRef> _PriceDefinitionRef = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, string.Empty, Session["UserDefProf"].ToString());
                        Session["_PriceDefinitionRef_1"] = _PriceDefinitionRef;
                        MasterProfitCenter _ctn = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                        Session["MasterProfitCenter_1"] = _ctn;
                        //add by Chamal using Dulaj PC 07-09-2018
                        if (_ctn != null)
                        {
                            DataTable ChannelDefinition_ = CHNLSVC.Inventory.GetChannelDetail(Session["UserCompanyCode"].ToString(), _ctn.Mpc_chnl);
                            Session["ChannelDefinition_"] = ChannelDefinition_;
                        }
                        else
                        {
                            Session["ChannelDefinition_"] = null;
                        }


                        bool IsSaleValueRoundUp = CHNLSVC.General.IsSaleFigureRoundUp(Session["UserCompanyCode"].ToString());
                        Session["IsSaleValueRoundUp_1"] = IsSaleValueRoundUp;


                        MasterLocation oLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), _loginUser.User_def_loc);
                        //add by Chamal using Dulaj PC 07-09-2018
                        if (oLocation != null)
                        {
                            Session["GlbDefChannel"] = oLocation.Ml_cate_2;
                        }
                        else
                        {
                            Session["GlbDefChannel"] = null;
                        }
                        if (_loginUser.Pw_must_change == true)
                        {
                            _msg = "The user's password must be changed before logging on the this time.";
                            msg(_msg);
                            //MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ShowChangePasswordPanel();

                            return;
                        }

                        //if (txtUser.Text.ToString().ToUpper() == "1800019")
                        //{
                        if (_loginUser.Pw_expire == true)
                        {
                            if (_loginUser.Remaining_days > 0)
                            {
                                _msg = "Your password will expire in " + _loginUser.Remaining_days.ToString() + " days. Would you like to change it now?";
                                //msg(_msg);
                                lblMssg.Text = "Your password will expire in " + _loginUser.Remaining_days.ToString() + "days.";
                                lblMssg1.Text = " Would you like to change it now?";
                                lblAlertValue.Text = "2";
                                //if (MessageBox.Show(_msg, "Expire password", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                //{
                                //ShowChangePasswordPanel();
                                //    return;
                                //}
                                UserPopoup.Show();
                            }
                            else if (_loginUser.Remaining_days == 0)
                            {
                                _msg = "Your password expires today. Would you like to change it now?";
                                //msg(_msg);
                                lblMssg.Text = "Your password expires today. Would you like to change it now?";
                                lblAlertValue.Text = "3";
                                //if (MessageBox.Show(_msg, "Expire password", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                //{
                                //ShowChangePasswordPanel();
                                //    return;
                                //}

                            }

                            if (_loginUser.Pw_expire_must_change == true)
                            {
                                _msg = "Your password has expired and must be changed!";
                                msg(_msg);
                                // MessageBox.Show(_msg, "Expire password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ShowChangePasswordPanel();
                                return;
                            }
                        }
                        //}

                        //this.Hide();
                        //MainMenu _mainMenu = new MainMenu();
                        //_mainMenu.Show();
                        //_isLog = true;
                        Session["UserID"] = txtUserName.Text.ToString().ToUpper();
                        Session["UserCompanyCode"] = ddlCompany.SelectedItem.Value.ToString();
                        Session["UserCompanyName"] = ddlCompany.SelectedItem.Text.ToString();
                        Session["UserIP"] = Request.UserHostAddress;
                        Session["UserComputer"] = Request.UserHostName;
                        Session["version"] = BaseCls.GlbVersionNo.ToString();
                        FormsAuthentication.SetAuthCookie(userID, false);
                        Response.Redirect("View/ADMIN/Home.aspx");
                    }
                    else if (_msgStatus == -1)
                    {
                        if (_loginUser.Login_attempts >= _loginRetryOutCounter)
                        {
                            if (Session["LoginCounter"] == null)
                            {
                                Session["LoginCounter"] = 1;
                            }
                            else
                            {
                                Session["LoginCounter"] = Convert.ToInt16(Session["LoginCounter"].ToString()) + 1;
                                string x = Session["LoginCounter"].ToString();
                            }

                            _loginRetryInCounter = _loginRetryOutCounter;
                            txtUserName.Text = "";
                            txtPassword.Focus();
                            msg(_msg);
                            //MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        else
                        {
                            CHNLSVC.Security.Save_User_Falis(txtUserName.Text.ToString(), txtPassword.Text.ToString(), Session["UserCompanyCode"].ToString().ToString(), Session["UserIP"].ToString(), _WindowsLogonName, _WindowsUser);
                            _msg = "You have failed to remember your details. \nContact Administration for further instructions";
                            msg(_msg);
                            //MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            // Application.Exit();
                            // Response.Redirect(Request.RawUrl, false);
                            return;
                        }
                    }
                    else if (_msgStatus == -2)
                    {
                        if (Session["LoginCounter"] == null)
                        {
                            Session["LoginCounter"] = 0;
                        }
                        else
                        {
                            Session["LoginCounter"] = Convert.ToInt16(Session["LoginCounter"].ToString()) + 1;
                        }

                        _loginRetryInCounter = _loginRetryOutCounter;
                        msg(_msg);
                        //MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //Application.Exit();
                        // Response.Redirect(Request.RawUrl, false);
                        return;
                    }

                    else if (_msgStatus == -4)
                    {
                        //MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //Application.Exit();
                        msg(_msg);
                        // Response.Redirect(Request.RawUrl, false);
                        return;
                    }
                    else if (_msgStatus == -99)
                    {
                        msg(_msg);
                        //MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

            }
            catch (Exception err)
            {
                //MessageBox.Show(err.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseChannel();
                GC.Collect();
            }
        }

        private void ShowChangePasswordPanel()
        {

            divlogin.Visible = false;
            divChangePassword.Visible = true;
            txtNewPassword.Focus();
        }
        #endregion
        #region Reset Password

        protected void txtNewPassword_TextChanged(object sender, EventArgs e)
        {

            txtConfirmNewPassword.Focus();

        }

        protected void btnResetLogin_Click(object sender, EventArgs e)
        {
            try
            {
                int _loginRetryOutCounter = 0;
                int _msgStatus = 0;
                string _msgTitle = string.Empty;
                string _msg = string.Empty;
                string _err = string.Empty;
                string _newPw = txtNewPassword.Text.Trim();
                string _confirmPw = txtConfirmNewPassword.Text.Trim();

                _loginUser = CHNLSVC.Security.LoginToSystem(usrName.Text.ToString(), crntPasswd.Text, Session["UserCompanyCode"].ToString(), BaseCls.GlbVersionNo.ToString(), Request.UserHostAddress, Request.UserHostAddress, 1, out _loginRetryOutCounter, out _msgStatus, out  _msg, out  _msgTitle);

                if (_msgStatus == -1)
                {
                    error = "Please Enter Correct User Name and Password.!";
                    msg(error);
                    return;
                }

                if (_newPw != _confirmPw)
                {
                    // MessageBox.Show("The passwords you typed don't match. Please try again!", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    error = "The passwords you typed don't match. Please try again!";
                    msg(error);
                    txtNewPassword.Focus();
                    //txtNewPassword.SelectAll();
                    return;
                }

                if (_newPw.ToUpper() == txtPassword.Text.ToUpper())
                {
                    _err = "Your new password was rejected because it does not meet the minimum security requirements.\n";
                    _err = _err + "\n" + "Your password was rejected because it:";
                    _err = _err + "\n" + "- was similar to your current password";

                    //MessageBox.Show(_err, "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    error = _err;
                    msg(error);
                    txtNewPassword.Focus();
                    //txtNewPw.SelectAll();
                    return;
                }

                if (CHNLSVC.Security.CheckPasswordPolicy(Session["UserID"].ToString(), _newPw, out  _err) == false)
                {
                    //MessageBox.Show("The password you entered doesn't meet the minimum security requirements!", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    // MessageBox.Show(_err, "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    error = _err;
                    msg(error);
                    txtNewPassword.Focus();
                    // txtNewPw.SelectAll();
                    return;
                }

                //Cursor.Current = Cursors.WaitCursor;

                Int32 _ref = UpdateSystemUser(Session["UserID"].ToString(), _newPw);

                if (_ref > 0)
                {
                    // Cursor.Current = Cursors.Default;
                    error = "Your password has been changed!";
                    msg(error);
                    //MessageBox.Show("Your password has been changed!", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Hide();
                    //MainMenu _mainMenu = new MainMenu();
                    //_mainMenu.Show();
                    //_isLog = true;
                    FormsAuthentication.SetAuthCookie(userID, false);
                    Response.Redirect("View/ADMIN/Home.aspx");
                }
                else
                {
                    // Cursor.Current = Cursors.Default;
                    // MessageBox.Show("Unable to change password! Try later...", "Change password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    error = "Unable to change password!";
                    msg(error);
                }
            }
            catch (Exception err)
            {
                // Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                // MessageBox.Show(err.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool CheckPasswordComplexity(string password)
        {
            //  return CheckPasswordComplexity(Membership.Provider, password);
            if (string.IsNullOrEmpty(password)) return false;
            //if (password.Length < Membership.MinRequiredPasswordLength) return false;
            if (password.Length < 8) return false;

            int nonAlnumCount = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i)) nonAlnumCount++;
            }

            if (nonAlnumCount < 1)//Membership.MinRequiredNonAlphanumericCharacters)
                return false;
            // if (!string.IsNullOrEmpty(Membership.PasswordStrengthRegularExpression) && !Regex.IsMatch(password, Membership.PasswordStrengthRegularExpression))
            //if (!Regex.IsMatch(password, "[A-Z]+")) //should contain atleast one capital letter
            //{
            //    return false;
            //}
            if (!Regex.IsMatch(password, "[\\d]+")) //should contain atleast one numeric character
            {
                return false;
            }
            return true;
        }

        private Int32 UpdateSystemUser(string _user, string _newPw)
        {
            SystemUser _SystemUser = new SystemUser();
            _SystemUser = CHNLSVC.Security.GetUserByUserID(_user);
            _SystemUser.Se_usr_pw = _newPw;
            _SystemUser.Se_pw_chng_by = _user;
            if (_SystemUser.Se_pw_mustchange == 1) _SystemUser.Se_pw_mustchange = 0;
            return CHNLSVC.Security.Change_Password(_SystemUser);
        }

        private void btnResetCancel_Click(object sender, EventArgs e)
        {
            // Application.Exit();
        }
        #endregion
        private void msg(string _msg)
        {
            divwarnning.Visible = true;
            lblWarning.Text = _msg;
        }
        private void Askmsg(string _msg)
        {
            //DivAsk.Visible = true;
            //lblAsk.Text = _msg;
        }
        private void ClearMsg()
        {
            divwarnning.Visible = false;
            //DivAsk.Visible = false;
        }

        protected void lbtColse_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");

        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            ClearMsg();
            CHNLSVC.Security.ExitLoginSession(txtUserName.Text.ToString().ToUpper(), ddlCompany.Text.ToString().ToUpper(), "-1");
            LoginP.Enabled = true;
        }

        protected void lbtnSave_Click1(object sender, EventArgs e)
        {
            ClearMsg();
            CHNLSVC.Security.ExitLoginSession(txtUserName.Text.ToString().ToUpper(), ddlCompany.Text.ToString().ToUpper(), "-1");
            LoginP.Enabled = true;
            ClearMsg();
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            ClearMsg();
            if (lblAlertValue.Text == "1")
            {
                int _loginRetryOutCounter = 0;
                int _msgStatus = 0;
                string _msgTitle = string.Empty;
                string _msg = string.Empty;
                string _WindowsLogonName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                string _WindowsUser = Environment.UserName;

                int value = CHNLSVC.Security.ExitLoginSession(txtUserName.Text.ToString().ToUpper(), ddlCompany.Text.ToString().ToUpper(), "-1");
                _loginUser = CHNLSVC.Security.LoginToSystem(txtUserName.Text.ToString(), lblpsw.Text, Session["UserCompanyCode"].ToString(), BaseCls.GlbVersionNo.ToString(), Request.UserHostAddress, Request.UserHostAddress, 1, out _loginRetryOutCounter, out _msgStatus, out  _msg, out  _msgTitle);
                if (_msgStatus == 1 || _msgStatus == -3)
                {

                    Session["UserID"] = txtUserName.Text.ToString().ToUpper();
                    Session["UserCompanyCode"] = ddlCompany.SelectedItem.Value.ToString();
                    Session["UserCompanyName"] = ddlCompany.SelectedItem.Text.ToString();
                    Session["UserIP"] = getLogUserIP(); // GetIP().ToString();// Request.UserHostAddress; 
                    Session["UserComputer"] = Request.UserHostName;
                    Session["version"] = BaseCls.GlbVersionNo.ToString();
                    Session["UserSBU"] = GetSBU();
                    Session["UserDefLoca"] = _loginUser.User_def_loc;
                    Session["UserDefProf"] = _loginUser.User_def_pc;
                    Session["GlbDefaultBin"] = CHNLSVC.Inventory.Get_defaultBinCDWeb(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                    //Session["SessionID"] = Convert.ToString(CHNLSVC.Security.SaveLoginSession(Session["UserID"].ToString(), Session["UserCompanyCode"].ToString(), Session["UserIP"].ToString(),"", _WindowsLogonName, _WindowsUser));
                    Base Base = new Base();
                    Base.SetLoginCacheLayer(_loginUser, _WindowsLogonName, _WindowsUser);
                    List<PriceDefinitionRef> _PriceDefinitionRef = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, string.Empty, Session["UserDefProf"].ToString());
                    Session["_PriceDefinitionRef_1"] = _PriceDefinitionRef;
                    MasterProfitCenter _ctn = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                    Session["MasterProfitCenter_1"] = _ctn;
                    if (_ctn != null)
                    {
                        DataTable ChannelDefinition_ = CHNLSVC.Inventory.GetChannelDetail(Session["UserCompanyCode"].ToString(), _ctn.Mpc_chnl);
                        Session["ChannelDefinition_"] = ChannelDefinition_;
                    }
                    bool IsSaleValueRoundUp = CHNLSVC.General.IsSaleFigureRoundUp(Session["UserCompanyCode"].ToString());
                    Session["IsSaleValueRoundUp_1"] = IsSaleValueRoundUp;


                    MasterLocation oLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), _loginUser.User_def_loc);
                    if (oLocation != null)
                    {
                        Session["GlbDefChannel"] = oLocation.Ml_cate_2;
                        UserPopoup.Hide();
                    }
                    else
                    {
                        Session["GlbDefChannel"] = "";
                        UserPopoup.Hide();
                    }


                    //Session["UserCompanyCode"] = ddlCompany.Text.ToString().ToUpper();
                    //BaseCls.GlbUserIP = GetIP();
                    //string _WindowsLogonName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    //Session["UserSBU"] = GetSBU();
                    //Session["UserSBU"] = Convert.ToString(Session["UserSBU"]);
                    //Session["UserID"] = txtUserName.Text.ToString().ToUpper();
                    //Session["UserCompanyCode"] = ddlCompany.SelectedItem.Value.ToString();
                    //Session["UserCompanyName"] = ddlCompany.SelectedItem.Text.ToString();
                    //Session["UserIP"] = Request.UserHostAddress;
                    //Session["UserComputer"] = Request.UserHostName;
                    //Session["version"] = GlbVersionNo;
                    //FormsAuthentication.SetAuthCookie(userID, false);
                    //Base Base = new Base();

                    //  Base.SetLoginCacheLayer(_loginUser, _WindowsLogonName, _WindowsUser);
                    FormsAuthentication.SetAuthCookie(userID, false);
                    Response.Redirect("View/ADMIN/Home.aspx");
                }
                else if (_msgStatus == -1)
                {
                    UserPopoup.Hide();
                    if (_loginUser.Login_attempts >= _loginRetryOutCounter)
                    {
                        _loginRetryInCounter = _loginRetryOutCounter;
                        txtUserName.Text = "";
                        txtPassword.Focus();
                        msg(_msg);
                        //MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    else
                    {
                        CHNLSVC.Security.Save_User_Falis(txtUserName.Text.ToString(), txtPassword.Text.ToString(), Session["UserCompanyCode"].ToString().ToString(), Session["UserIP"].ToString(), _WindowsLogonName, _WindowsUser);
                        _msg = "You have failed to remember your details. \nContact Administration for further instructions";
                        msg(_msg);
                        //MessageBox.Show(_msg, "Login Fail", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        // Application.Exit();
                        // Response.Redirect(Request.RawUrl, false);
                        return;
                    }
                }
                else if (_msgStatus == -2)
                {
                    UserPopoup.Hide();
                    _loginRetryInCounter = _loginRetryOutCounter;
                    msg(_msg);
                    //MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //Application.Exit();
                    // Response.Redirect(Request.RawUrl, false);
                    return;
                }

                else if (_msgStatus == -4)
                {
                    UserPopoup.Hide();
                    //MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //Application.Exit();
                    msg(_msg);
                    // Response.Redirect(Request.RawUrl, false);
                    return;
                }
                else if (_msgStatus == -99)
                {
                    UserPopoup.Hide();
                    msg(_msg);
                    //MessageBox.Show(_msg, _msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (lblAlertValue.Text == "2")
            {
                ShowChangePasswordPanel();
            }
            else if (lblAlertValue.Text == "3")
            {
                ShowChangePasswordPanel();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ClearMsg();
            if (lblAlertValue.Text == "1")
            {
                Response.Redirect(Request.RawUrl, false);
                UserPopoup.Hide();
            }
            else if (lblAlertValue.Text == "2")
            {
                UserPopoup.Hide();
            }
            else if (lblAlertValue.Text == "3")
            {
                UserPopoup.Hide();
            }
        }

    }
}