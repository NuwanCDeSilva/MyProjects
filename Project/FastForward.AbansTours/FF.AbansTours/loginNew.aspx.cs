using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.AbansTours.Common;
using FF.BusinessObjects;

namespace FF.AbansTours
{
    public partial class loginNew : BasePage
    {
        private string userID, password, userName;
        private int _loginRetryInCounter = 0;
        private int _loginRetryOutCounter = 0;
        private readonly string invaldlogin = "The user name or password you entered isn't correct. Try entering it again.";
        private readonly string invaldconnection = "Server not connected......!!!";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmsg.Text = "";
                BindUserCompanyDDLData(ddlCompany, txtUserName.Text);
            }
        }

        public void BindUserCompanyDDLData(DropDownList ddl, String username)
        {
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
                }
            }
            catch (Exception ex)
            {
                DisplayMessages(ex.Message);
            }
        }

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
                        BindUserCompanyDDLData(ddlCompany, txtUserName.Text.Trim().ToUpper());
                        txtPassword.Focus();
                    }
                    else
                    {
                        ddlCompany.Items.Clear();
                        ddlCompany.Items.Add(new ListItem("--Select--", "-1"));
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessages(ex.Message);
            }
        }

        private void DisplayMessages(string message)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + message + "');", true);
            }
            catch (Exception ex)
            {
            }
        }

        private Boolean Check_System_Login()
        {
            SystemUser _systemaduser = new SystemUser();
            SystemUser _systemuser = null;
            _systemuser = CHNLSVC.Security.GetUserByUserID(userID);

            if (_systemuser == null)
            {
                lblmsg.Text = "Unsuccessful login.  Please contact your system administrator.";
                DisplayMessages(msg.sysUserNull);
                return false;
            }

            Session["password"] = _systemuser.Se_usr_pw;

            List<SystemOption> _allSystemOptions = null;

            if (CHNLSVC.Security.GetServerDateTime().Date != DateTime.Now.Date)
            {
                lblmsg.Text = "Your system date is not equal to server date! \nPlease contact system administrator....";
                DisplayMessages(msg.DateMissMatch);
                return false;
            }

            if (txtUserName.Text.Trim() == "")
            {
                lblmsg.Text = "Please enter UserName";
                DisplayMessages(msg.UserNameEmpty);
                return false;
            }
            if (txtPassword.Text.Trim() == "")
            {
                lblmsg.Text = "Please enter Password";
                DisplayMessages(msg.PassWordEmty);
                return false;
            }

            if (ddlCompany.SelectedValue == "-1")
            {
                lblmsg.Text = "Please select the company";
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
                    lblmsg.Text = "You have failed to remember your details. \nContact Administration for further instructions";
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
                        WebMessageBox.ShowAlertMessage(this, "Active session is exist for the user " + txtUserName.Text.ToString() + " on " + Session["UserCompanyName"].ToString() + "  ,You can close the existing session and log to system.If not Click on CANCEL");
                        //btnForceLogout.Visible = true;
                        btnLogin.Enabled = false;
                        ddlCompany.Enabled = false;
                        txtPassword.ReadOnly = true;
                        txtUserName.ReadOnly = true;
                        //btnForceLogout.Focus();
                        return false;
                    }
                }

                if (_loginUser.Pw_must_change == true)
                {
                    _msg = "password must be changed before logging this time";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "SaveOk('" + _msg + "')", true);
                    return false;
                }

                if (_loginUser.Pw_expire == true)
                {
                    if (_loginUser.Remaining_days > 0)
                    {
                        _msg = "Your password will expire in " + _loginUser.Remaining_days.ToString() + " days. Would you like to change it now?";
                        string chekmsg = "show";
                        this.ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "SaveYesNO('" + _msg + "','" + chekmsg + "')", true);
                        return false;
                    }
                    else if (_loginUser.Remaining_days == 0)
                    {
                        _msg = "Your password expires today. Would you like to change it now?";
                        string chekmsg = "show";
                        this.ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "SaveYesNO('" + _msg + "','" + chekmsg + "')", true);
                        return false;
                    }

                    if (_loginUser.Pw_expire_must_change == true)
                    {
                        _msg = "Your password has expired and must be changed!";
                        this.ClientScript.RegisterStartupScript(this.GetType(), "fnCall", "SaveOk('" + _msg + "')", true);
                        return false;
                    }
                }
            }
            else if (_msgStatus == -99)
            {
                lblmsg.Text = _msg;
                DisplayMessages(_msg);
                return false;
            }

            if (_systemuser == null)
            {
                lblmsg.Text = "Unsuccessful login.  Please contact your system administrator.";
                DisplayMessages(msg.sysUserNull);
                return false;
            }

            if (_systemuser.Se_act == 0)
            {
                lblmsg.Text = invaldlogin;
                return false;
            }

            if (Session["password"].ToString() != password)
            {
                DisplayMessages(msg.IncorrectLogin);
                return false;
            }

            if ((_systemuser.Se_isdomain == 1) && (!CHNLSVC.Security.CheckValidADUser(_systemuser.Se_domain_id, out _systemaduser)))
            {
                lblmsg.Text = "Unsuccessful login. Your domain user id has been disable.";
                DisplayMessages(msg.Domain);
                return false;
            }

            _allSystemOptions = CHNLSVC.Security.GetAllSystemOptions();
            if (_allSystemOptions == null)
            {
                lblmsg.Text = "Unsuccessful login. Still you don't have to access system menu permissions! Please contact your system administrator.";
                DisplayMessages(msg.GetAllSystemOptions);
                return false;
            }

            userName = _systemuser.Se_usr_name;
            return true;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    userID = txtUserName.Text.Trim().ToUpper();
                    password = txtPassword.Text.Trim();

                    if (Check_System_Login() == false)
                    {
                        FormsAuthentication.SignOut();
                        return;
                    }
                    Load_Session_Variables();
                    GlbisLoging = false;
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        protected void Load_Session_Variables()
        {
            Session["UserID"] = userID;
            Session["UserName"] = userName;
            Session["UserIP"] = Request.UserHostAddress;
            Session["UserComputer"] = Request.UserHostName;
            Session["UserCompanyCode"] = ddlCompany.SelectedItem.Value.ToString();
            Session["UserCompanyName"] = ddlCompany.SelectedItem.Text.ToString();
            Session["SessionID"] = CHNLSVC.Security.SaveLoginSession(userID, ddlCompany.SelectedItem.Value.ToString(), Request.UserHostAddress, Request.UserHostName, "", "");

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
            }

            if (Request.QueryString["ReturnUrl"] != null)
            {
                FormsAuthentication.RedirectFromLoginPage(userID, false);
            }
            else
            {
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
    }

    public class WebMessageBox
    {
        public static void ShowAlertMessage(Page aPage, String Message)
        {
            string Output;
            Output = String.Format(" alert('{0}');", Message);
            aPage.ClientScript.RegisterStartupScript(aPage.GetType(), "Key", Output, true);
        }
    }
}