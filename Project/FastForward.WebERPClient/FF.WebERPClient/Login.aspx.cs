using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Oracle.DataAccess.Client;
using FF.BusinessObjects;
using System.Text.RegularExpressions;
using System.Transactions;

namespace FF.WebERPClient
{
    public partial class Login : BasePage
    {
        string userID, password, userName;
        readonly string invaldlogin = "The user name or password you entered isn't correct. Try entering it again.";
        readonly string invaldconnection = "Server not connected......!!!";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Before do any thing, have to check this userID  can access this company
                //if (false)
                //{
                //    throw new Exception("you don't have have Access");
                //}

GlbisLoging = true;
                if (!Page.IsPostBack)
                {
                    //BindJavaScripts();
                    BindUserCompanyDDLData(ddlCompany, "");
                    txtUserName.Focus();
                    
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = invaldconnection;
                return;
            }
        }

        #region Login Button
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Page.IsValid)
                //{ 

                //}
                userID = txtUserName.Text.Trim().ToUpper();
                password = txtPassword.Text.Trim();
                //String test_username1 = GlbUserName;
                //string pwd = Membership.GetUser(userName).GetPassword();

                if (Check_System_Login() == false)
                {
                    FormsAuthentication.SignOut();
                    return;
                }
                Load_Session_Variables();
                GlbisLoging = false;

            }
            catch (Exception ex)
            {
                lblmsg.Text = invaldconnection;
            }
        }
        #endregion

        #region Load Session Variables
        protected void Load_Session_Variables()
        {
            #region Clear Login Information Sessions
            //Session["UserID"] = string.Empty;
            //Session["UserName"] = string.Empty;
            //Session["UserIP"] = string.Empty;
            //Session["UserComputer"] = string.Empty;
            //Session["UserCompanyCode"] = string.Empty;
            //Session["UserCompanyName"] = string.Empty;
            //Session["SessionID"] = string.Empty;
            //Session["UserDefProf"] = string.Empty;
            //Session["UserDefLoca"] = string.Empty;
            #endregion

            Session["UserID"] = userID;
            Session["UserName"] = userName;
            Session["UserIP"] = Request.UserHostAddress;
            Session["UserComputer"] = Request.UserHostName;
            Session["UserCompanyCode"] = ddlCompany.SelectedItem.Value.ToString();
            Session["UserCompanyName"] = ddlCompany.SelectedItem.Text.ToString();
            Session["SessionID"] = CHNLSVC.Security.SaveLoginSession(userID, ddlCompany.SelectedItem.Value.ToString(), Request.UserHostAddress, Request.UserHostName);

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

        public void BindUserCompanyDDLData(DropDownList ddl, String username)
        {

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--Select--", "-1"));
            ddl.DataSource = CHNLSVC.Security.GetUserCompany(username);

            if (!string.IsNullOrEmpty(username))
            {
                List<SystemUserCompany> _systemUserCompanyList = CHNLSVC.Security.GetUserCompany(username);
                if (_systemUserCompanyList != null)
                    //ddl.DataSource = _systemUserCompanyList;
                    //ddl.DataSource = _systemUserCompanyList.OrderBy(items => items.MasterComp.Mc_desc);
                    //ddl.DataTextField = "Mc_desc";
                    //ddl.DataValueField = "Sec_com_cd";
                    //ddl.DataBind();

                    foreach (SystemUserCompany item in _systemUserCompanyList)
                    {
                        ddl.Items.Add(new ListItem(item.MasterComp.Mc_desc, item.SEC_COM_CD));
                    }
            }

        }

        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtUserName.Text = string.Empty;
                txtPassword.Text = string.Empty;
                userID = string.Empty;
                password = string.Empty;
                BindUserCompanyDDLData(ddlCompany, "");
                lblmsg.Text = "";
                txtUserName.Focus();
            }
            catch (Exception ex)
            {
                lblmsg.Text = invaldconnection;
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
                lblmsg.Text = invaldconnection;
            }
        }

        #region Check System Login Information
        private Boolean Check_System_Login()
        {
            MembershipUser _membershipUser = Membership.GetUser(userID);

            
            _membershipUser.UnlockUser();

            
            string _password  =_membershipUser.GetPassword();
            SystemUser _systemaduser = new SystemUser();
            SystemUser _systemuser = null;
            _systemuser = CHNLSVC.Security.GetUserByUserID(userID);
            List<SystemOption> _allSystemOptions = null;

            if (_membershipUser == null)
            {
                lblmsg.Text = invaldlogin;
                return false;
            }

            if (ddlCompany.SelectedValue == "-1")
            {
                lblmsg.Text = "Please select the company";
                return false;
            }

            if (_systemuser == null)
            {
                lblmsg.Text = "Unsuccessful login.  Please contact your system administrator.";
                return false;
            }

            if (_systemuser.Se_act == 0)
            {
                lblmsg.Text = invaldlogin;
                return false;
            }

            if (!_membershipUser.IsApproved) //Chamal 20-Jun-2012
            {
                lblmsg.Text = "Your account has not yet been approved by the system administrators. Please try again later...";
                return false;
            }

            if (_membershipUser.IsLockedOut) //Chamal 20-Jun-2012
            {
                lblmsg.Text = "Your account has been locked out because of a maximum number of incorrect login attempts. You will NOT be able to login until you contact a system administrator and have your account unlocked.";
                return false;
            }

            //if (userID == "ADMIN")
            //{
            //    password = "ADMIN@1";
            //}

            if (!Membership.ValidateUser(userID, password))
            {
                lblmsg.Text = invaldlogin;
                return false;
            }

            if ((_systemuser.Se_isdomain == 1) && (!CHNLSVC.Security.CheckValidADUser(_systemuser.Se_domain_id, out _systemaduser)))
            {
                lblmsg.Text = "Unsuccessful login. Your domain user id has been disable.";
                return false;
            }

            // Chamal 04/05/2012
            _allSystemOptions = CHNLSVC.Security.GetAllSystemOptions();
            if (_allSystemOptions == null)
            {
                lblmsg.Text = "Unsuccessful login. Still you don't have to acess system menu permissions! Please contact your system administrator.";
                return false;
            }

            // Chamal 23/05/2012
            if (_systemuser.Se_pw_mustchange == 1)
            {
                lblChangePwInfor.Text = "Your password must be changed before logging on the first time.";
                divlogin.Visible = false;
                divChangePassword.Visible = true;
                txtNewUserName.Focus();
                return false;
            }

            // Chamal 23/05/2012
            if (_systemuser.Se_pw_expire == 1)
            {
                if (_systemuser.Se_noofdays != 0)
                {
                    if (_membershipUser.LastPasswordChangedDate.AddDays((double)_systemuser.Se_noofdays) < DateTime.Now)
                    {
                        lblChangePwInfor.Text = "Your password has expired and you need to change it before you sign in to system.";
                        divlogin.Visible = false;
                        divChangePassword.Visible = true;
                        txtNewUserName.Focus();
                        return false;
                    }
                }
            }

            userName = _systemuser.Se_usr_name;
            return true;
        }
        #endregion

        #region Change Password Button
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                userID = txtNewUserName.Text.Trim().ToUpper();
                password = txtCurrentPassword.Text.Trim();
                string newPassword = txtNewPassword.Text.Trim();
                string confirmPassword = txtConfirmNewPassword.Text.Trim();

                MembershipUser _membershipUser = Membership.GetUser(userID);


                if (_membershipUser == null)
                {
                    lblChangePwErrMsg.Text = invaldlogin;
                    txtNewUserName.Text = "";
                    txtNewUserName.Focus();
                    return;
                }

                if (!Membership.ValidateUser(userID, password))
                {
                    lblChangePwErrMsg.Text = invaldlogin;
                    txtNewUserName.Text = "";
                    txtNewUserName.Focus();
                    return;
                }

                if (CheckPasswordComplexity(newPassword) == false)
                {
                    lblChangePwErrMsg.Text = "The password you entered doesn't meet the minimum security requirements.";
                    txtNewUserName.Text = "";
                    txtNewUserName.Focus();
                    return;
                }

                if (password == newPassword)
                {
                    lblChangePwErrMsg.Text = "The password you entered doesn't meet the minimum security requirements.";
                    txtNewUserName.Text = "";
                    txtNewUserName.Focus();
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    lblChangePwErrMsg.Text = "The passwords you typed don't match. Please try again.";
                    txtNewUserName.Text = "";
                    txtNewUserName.Focus();
                    return;
                }

                UpdateSystemUser(userID, newPassword);

                //divlogin.Visible = true;
                //divChangePassword.Visible = false;
                //txtUserName.Focus();

                Load_Session_Variables();
            }
            catch (Exception ex)
            {
                lblmsg.Text = invaldconnection;
            }
        }

        static public bool CheckPasswordComplexity(string password)
        {
            return CheckPasswordComplexity(Membership.Provider, password);
        }

        /// <summary>
        /// Checks password complexity requirements for the given membership provider
        /// </summary>
        /// <param name="membershipProvider">membership provider</param>
        /// <param name="password">password to check</param>
        /// <returns>true if the password meets the req. complexity</returns>
        static public bool CheckPasswordComplexity(MembershipProvider membershipProvider, string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            if (password.Length < membershipProvider.MinRequiredPasswordLength) return false;
            int nonAlnumCount = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (!char.IsLetterOrDigit(password, i)) nonAlnumCount++;
            }
            if (nonAlnumCount < membershipProvider.MinRequiredNonAlphanumericCharacters) return false;
            if (!string.IsNullOrEmpty(membershipProvider.PasswordStrengthRegularExpression) &&
                !Regex.IsMatch(password, membershipProvider.PasswordStrengthRegularExpression))
            {
                return false;
            }
            return true;
        }


        private void UpdateSystemUser(string _user, string _newPw)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;
            MembershipUser _membershipUser = Membership.GetUser(userID);

            SystemUser _SystemUser = new SystemUser();
            _SystemUser = CHNLSVC.Security.GetUserByUserID(_user);
            _SystemUser.Se_usr_pw = _newPw;
            if (_SystemUser.Se_pw_mustchange == 1)
            {
                _SystemUser.Se_pw_mustchange = 0;
            }
            //_SystemUser.Se_pw_expire = (chkPWExpire.Checked == true) ? 1 : 0;
            using (TransactionScope tr = new TransactionScope())
            {
                _membershipUser.ChangePassword(_membershipUser.GetPassword(), _newPw);
                row_aff = CHNLSVC.Security.UpdateUser(_SystemUser);
                tr.Complete();
            }
        }
        #endregion

        private void BindJavaScripts()
        {
            txtUserName.Attributes.Add("onblur", "GetUserCompany()");
        }


    }
}