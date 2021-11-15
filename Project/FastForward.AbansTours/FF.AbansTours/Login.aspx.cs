using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.AbansTours.Common;

using FF.BusinessObjects;

namespace FF.AbansTours
{
    public partial class Login : BasePage
    {
        private string userID, password, userName;
        private int _loginRetryInCounter = 0;
        private int _loginRetryOutCounter = 0;

        private readonly string invaldlogin = "The user name or password you entered isn't correct. Try entering it again.";
        private readonly string invaldconnection = "Server not connected......!!!";

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GlbisLoging = true;
                if (!Page.IsPostBack)
                {
                    lblMessage.Text = "";
                    imgLoading.Visible = false;
                    BindUserCompanyDDLData(ddlCompany, "");
                    txtUserName.Focus();
                    if (Session["SessionLogout"] == "1")
                    {
                        WebMessageBox.ShowAlertMessage(this, "Current Session is expired or has been closed by administrator!");
                    }
                    else
                    {
                        Session.Clear();
                    }

                }
            }
            catch (Exception ex)
            {
                DisplayMessages(msg.invaldconnection);

                return;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                imgLoading.Visible = true;
                userID = txtUserName.Text.Trim().ToUpper();
                Session["userid"] = userID;
                password = txtPassword.Text.Trim();

                if (userID != "" && password != "")
                {
                    System.Threading.Thread.Sleep(1000);

                    if (Check_System_Login() == false)
                    {

                        FormsAuthentication.SignOut();
                        return;
                    }
                    DisplayMessages("ok");
                    Load_Session_Variables();
                    GlbisLoging = false;
                }
                else
                {
                    DisplayMessages(msg.UserNameAPass);
                }
                imgLoading.Visible = false;
            }
            catch (Exception ex)
            {
                DisplayMessages(ex.Message);
                lblmsg.Text = invaldconnection;
            }
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnLogin.Enabled = true;
        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        btnLogin.Enabled = true;
        //        ddlCompany.Enabled = true;
        //        txtPassword.ReadOnly = false;
        //        txtUserName.ReadOnly = false;
        //        txtUserName.Text = string.Empty;
        //        txtPassword.Text = string.Empty;
        //        userID = string.Empty;
        //        password = string.Empty;
        //        BindUserCompanyDDLData(ddlCompany, "");
        //        lblmsg.Text = "";
        //        txtUserName.Focus();
        //    }
        //    catch (Exception ex)
        //    {
        //        DisplayMessages(invaldconnection);
        //        lblmsg.Text = invaldconnection;
        //    }
        //}

        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text != string.Empty)
                {
                    SystemUser _systemaduser = new SystemUser();
                    SystemUser _systemuser = null;
                    _systemuser = CHNLSVC.Security.GetUserByUserID(txtUserName.Text.Trim().ToUpper());
                    if (_systemuser != null)
                    {
                        lblmsg.Visible = false;
                        BindUserCompanyDDLData(ddlCompany, txtUserName.Text.Trim().ToUpper());
                        txtPassword.Focus();
                    }
                    else
                    {
                        ddlCompany.Items.Clear();
                        ddlCompany.Items.Add(new ListItem("--Select--", "-1"));
                        lblmsg.Visible = true;
                        lblmsg.Text = "Invalid Name..";
                        DisplayMessages("Invalid Name..");
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = invaldconnection;
                DisplayMessages(invaldconnection);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            mpPleaseWait.Hide();
        }


        #endregion Events

        #region Methods

        public static bool isMobileBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            HttpContext context = HttpContext.Current;

            //FIRST TRY BUILT IN ASP.NT CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
            context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                                new[]
                                            {
                                                "midp", "j2me", "avant", "docomo",
                                                "novarra", "palmos", "palmsource",
                                                "240x320", "opwv", "chtml",
                                                "pda", "windows ce", "mmp/",
                                                "blackberry", "mib/", "symbian",
                                                "wireless", "nokia", "hand", "mobi",
                                                "phone", "cdm", "up.b", "audio",
                                                "SIE-", "SEC-", "samsung", "HTC",
                                                "mot-", "mitsu", "sagem", "sony"
                                                , "alcatel", "lg", "eric", "vx",
                                                "NEC", "philips", "mmm", "xx",
                                                "panasonic", "sharp", "wap", "sch",
                                                "rover", "pocket", "benq", "java",
                                                "pt", "pg", "vox", "amoi",
                                                "bird", "compal", "kg", "voda",
                                                "sany", "kdd", "dbt", "sendo",
                                                "sgh", "gradi", "jb", "dddi",
                                                "moto", "iphone"
                                            };

                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void Load_Session_Variables()
        {
            imgLoading.Visible = true;
            Session["SessionID"] = CHNLSVC.Security.SaveLoginSession(userID, ddlCompany.SelectedItem.Value.ToString(), Request.UserHostAddress, Request.UserHostName, "", "");

            if (Request.Browser.IsMobileDevice == true)
            {
                Session["IsMobileDevice"] = "1";
            }
            if (isMobileBrowser() == true)
            {
                Session["IsMobileDevice"] = "1";
            }

            List<SystemUserProf> _userprofs = CHNLSVC.Security.GetUserProfCenters(userID, ddlCompany.SelectedItem.Value.ToString());
            if (_userprofs != null)
            {
                var _userprofQuery =
                from userProf in _userprofs
                where userProf.Sup_usr_id == userID && userProf.Sup_com_cd == ddlCompany.SelectedItem.Value.ToString() && userProf.Sup_def_pccd == true
                select userProf;

                foreach (var _userprof in _userprofQuery)
                {
                    Session["UserDefProf"] = _userprof.Sup_pc_cd;
                }
            }

            if (Session["UserDefProf"] == null)
            {
                DisplayMessages("Please setup profit center for this user.");
                return;
            }

            List<SystemUserLoc> _userLocas = CHNLSVC.Security.GetUserLoc(userID, ddlCompany.SelectedItem.Value.ToString());
            if (_userLocas != null)
            {
                var _userLocaQuery =
                    from userLoca in _userLocas
                    where userLoca.SEL_USR_ID == userID && userLoca.SEL_COM_CD == ddlCompany.SelectedItem.Value.ToString() && userLoca.SEL_DEF_LOCCD == 1
                    select userLoca;

                foreach (var _userLoca in _userLocaQuery)
                {
                    Session["UserDefLoca"] = _userLoca.SEL_LOC_CD;
                }

                if (Session["UserDefLoca"] == null)
                {
                    DisplayMessages("Please setup location for this user.");
                    return;
                }

                MasterLocation oMstLoc1 = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                if (oMstLoc1 != null)
                {
                    Session["UserChannl"] = oMstLoc1.Ml_cate_2;
                    Session["UserSubChannl"] = oMstLoc1.Ml_cate_3;
                }
            }

            if (Request.QueryString["ReturnUrl"] != null)
            {
                FormsAuthentication.RedirectFromLoginPage(userID, false);
            }
            else
            {
                imgLoading.Visible = true;
                FormsAuthentication.SetAuthCookie(userID, false);
                Response.Redirect("Default.aspx");
            }
        }

        public string GetIP()
        {
            IPHostEntry host;
            string localIP = "";
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

        public void BindUserCompanyDDLData(DropDownList ddl, String username)
        {
            imgLoading.Visible = true;
            ddl.Items.Clear();
            ddl.DataSource = CHNLSVC.Security.GetUserCompany(username);
            imgLoading.Visible = false;

            if (!string.IsNullOrEmpty(username))
            {
                imgLoading.Visible = true;
                List<SystemUserCompany> _systemUserCompanyList = CHNLSVC.Security.GetUserCompany(username);
                imgLoading.Visible = false;
                if (_systemUserCompanyList != null)
                    foreach (SystemUserCompany item in _systemUserCompanyList)
                    {
                        ddl.Items.Add(new ListItem(item.MasterComp.Mc_desc, item.SEC_COM_CD));
                    }
            }
        }

        public void Load_Home()
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Login.aspx", false);
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Home.aspx", false);
            }
        }

        private Boolean Check_System_Login()
        {
            SystemUser _systemaduser = new SystemUser();
            SystemUser _systemuser = null;
            _systemuser = CHNLSVC.Security.GetUserByUserID(userID);

            if (_systemuser == null)
            {
                DisplayMessages(msg.sysUserNull);
                return false;
            }

            Session["password"] = _systemuser.Se_usr_pw;

            List<SystemOption> _allSystemOptions = null;

            if (CHNLSVC.Security.GetServerDateTime().Date != DateTime.Now.Date)
            {
                DisplayMessages(msg.DateMissMatch);
                return false;
            }

            if (txtUserName.Text.Trim() == "")
            {
                DisplayMessages(msg.UserNameEmpty);
                return false;
            }
            if (txtPassword.Text.Trim() == "")
            {
                DisplayMessages(msg.PassWordEmty);
                return false;
            }

            if (ddlCompany.SelectedValue == "-1")
            {
                DisplayMessages(msg.SelectComp);
                return false;
            }

            string _msg = string.Empty;
            string _msgTitle = string.Empty;
            int _msgStatus = 0;
            Session["UserID"] = txtUserName.Text.ToString().ToUpper();
            Session["UserCompanyCode"] = ddlCompany.SelectedItem.Value.ToString();
            Session["UserCompanyName"] = ddlCompany.SelectedItem.Text.ToString();
            Session["UserIP"] = Request.UserHostAddress;
            Session["UserComputer"] = Request.UserHostName;
            Session["version"] = GlbVersionNo;

            LoginUser _loginUser = CHNLSVC.Security.LoginToSystem(txtUserName.Text.ToString().ToUpper(), txtPassword.Text.ToString(), Session["UserCompanyName"].ToString().ToUpper(), Session["version"].ToString(), Session["UserIP"].ToString(), Session["UserComputer"].ToString(), _loginRetryInCounter, out _loginRetryOutCounter, out _msgStatus, out _msg, out _msgTitle);

            if (_msgStatus == -1)
            {
                if (_loginUser.Login_attempts <= _loginRetryOutCounter)
                {
                    _loginRetryInCounter = _loginRetryOutCounter;
                    txtPassword.Text = "";
                    txtPassword.Focus();
                    DisplayMessages(msg.Remember);
                    return false;
                }
            }
            else if (_msgStatus == -2)
            {
                _loginRetryInCounter = _loginRetryOutCounter;
                DisplayMessages(msg.DataSaved);
                return false;
            }
            else if (_msgStatus == 1 || _msgStatus == -3)
            {
                string _ipAdd = string.Empty;
                string _pcName = string.Empty;
                string _loginTime = string.Empty;

                if (txtUserName.Text.ToString().ToUpper() != "ADMIN")
                {
                    if (CHNLSVC.Security.IsActiveSessions(txtUserName.Text.ToString().ToUpper(), Session["UserCompanyCode"].ToString(), out _ipAdd, out _pcName, out _loginTime) == true)
                    {
                        //WebMessageBox.ShowAlertMessage(this, "Active session is exist for the user " + txtUserName.Text.ToString() + " on " + Session["UserCompanyName"].ToString() + "  ,You can close the existing session and log to system.If not Click on CANCEL");
                        //DisplayMessages("Active session is exist for the user " + txtUserName.Text.ToString() + " on " + Session["UserCompanyName"].ToString() + "  ,You can close the existing session and log to system.");
                        //btnLogin.Enabled = false;
                        //ddlCompany.Enabled = false;
                        //txtPassword.ReadOnly = true;
                        //txtUserName.ReadOnly = true;
                        //return false;
                    }
                }

                if (_loginUser.Pw_must_change == true)
                {
                    _msg = "password must be changed before logging this time";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "SaveOk('" + _msg + "')", true);
                    DisplayMessages(_msg);
                    return false;
                }

                if (_loginUser.Pw_expire == true)
                {
                    if (_loginUser.Remaining_days > 0)
                    {
                        _msg = "Your password will expire in " + _loginUser.Remaining_days.ToString() + " days. Would you like to change it now?";
                        string chekmsg = "show";
                        this.ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "SaveYesNO('" + _msg + "','" + chekmsg + "')", true);
                        DisplayMessages(_msg);
                        return false;
                    }
                    else if (_loginUser.Remaining_days == 0)
                    {
                        _msg = "Your password expires today. Would you like to change it now?";
                        string chekmsg = "show";
                        this.ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "SaveYesNO('" + _msg + "','" + chekmsg + "')", true);
                        DisplayMessages(_msg);
                        return false;
                    }

                    if (_loginUser.Pw_expire_must_change == true)
                    {
                        _msg = "Your password has expired and must be changed!";
                        this.ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "SaveOk('" + _msg + "')", true);
                        DisplayMessages(_msg);
                        return false;
                    }
                }
            }
            else if (_msgStatus == -99)
            {
                return false;
            }

            if (_systemuser == null)
            {
                DisplayMessages(msg.sysUserNull);
                //DisplayMessages( msg.sysUserNull);
                return false;
            }

            if (_systemuser.Se_act == 0)
            {
                lblmsg.Text = invaldlogin;
                DisplayMessages(invaldlogin);
                return false;
            }

            if (Session["password"].ToString() != password)
            {
                DisplayMessages(msg.IncorrectLogin);
                return false;
            }

            if ((_systemuser.Se_isdomain == 1) && (!CHNLSVC.Security.CheckValidADUser(_systemuser.Se_domain_id, out _systemaduser)))
            {
                DisplayMessages(msg.Domain);
                return false;
            }

            //_allSystemOptions = CHNLSVC.Security.GetAllSystemOptions();
            //if (_allSystemOptions == null)
            //{
            //    //lblmsg.Text = "Unsuccessful login. Still you don't have to acess system menu permissions! Please contact your system administrator.";
            //    DisplayMessages(msg.GetAllSystemOptions);
            //    return false;
            //}

            userName = _systemuser.Se_usr_name;
            return true;
        }

        private void BindJavaScripts()
        {
            txtUserName.Attributes.Add("onblur", "GetUserCompany()");
        }

        public void ShowChangePasswordPanel()
        {
        }

        private void DisplayMessages(string message)
        {
            try
            {
                lblMessage.Visible = true;
                lblMessage.Text = message;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + message + "');", true);

                Div1.Visible = true;
                lblDivMessage.Text = message;
            }
            catch (Exception ex)
            {
            }
        }

        private void DisplayMessages1(string message)
        {
            pnlWait.Visible = true;
            lblText.Text = message;
        }

        #endregion Methods

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Div1.Visible = false;
        }

    }
}