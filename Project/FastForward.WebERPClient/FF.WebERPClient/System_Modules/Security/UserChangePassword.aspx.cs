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

namespace FF.WebERPClient.System_Modules.Security
{
    public partial class UserChangePassword : BasePage
    {
        string userID, password;
        readonly string invaldlogin = "The old password isn't correct. Please retype it. You have to type letters in passwords using the correct case.";

        protected void Page_Load(object sender, EventArgs e)
        {

            //Before do any thing, have to check this userID  can access this company
            //if (false)
            //{
            //    throw new Exception("you don't have have Access");
            //}

            if (!Page.IsPostBack)
            {
                //BindJavaScripts();
                txtNewUserName.Text = GlbUserName;
                txtCurrentPassword.Focus();
            }

        }

        #region Change Password Button
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            userID = txtNewUserName.Text.Trim().ToUpper();
            password = txtCurrentPassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmNewPassword.Text.Trim();

            MembershipUser _membershipUser = Membership.GetUser(userID);

            if (_membershipUser == null)
            {
                lblChangePwErrMsg.Text = invaldlogin;
                //txtNewUserName.Text = "";
                //txtNewUserName.Focus();
                txtCurrentPassword.Focus();
                return;
            }

            if (!_membershipUser.IsApproved) //Chamal 20-Jun-2012
            {
                lblChangePwErrMsg.Text  = "Your account has not yet been approved by the system administrators. Please try again later...";
                return;
            }

            if (_membershipUser.IsLockedOut) //Chamal 20-Jun-2012
            {
                lblChangePwErrMsg.Text = "Your account has been locked out because of a maximum number of incorrect login attempts. You will NOT be able to login until you contact a system administrator and have your account unlocked.";
                return;
            }

            if (!Membership.ValidateUser(userID, password))
            {
                lblChangePwErrMsg.Text = invaldlogin;
                //txtNewUserName.Text = "";
                //txtNewUserName.Focus();
                txtCurrentPassword.Focus();
                return;
            }

            if (CheckPasswordComplexity(newPassword) == false)
            {
                lblChangePwErrMsg.Text = "The password you entered doesn't meet the minimum security requirements.";
                //txtNewUserName.Text = "";
                //txtNewUserName.Focus();
                txtCurrentPassword.Focus();
                return;
            }

            if (password == newPassword)
            {
                lblChangePwErrMsg.Text = "The password you entered doesn't meet the minimum security requirements.";
                //txtNewUserName.Text = "";
                //txtNewUserName.Focus();
                txtCurrentPassword.Focus();
                return;
            }

            if (newPassword != confirmPassword)
            {
                lblChangePwErrMsg.Text = "The passwords you typed don't match. Please try again.";
                //txtNewUserName.Text = "";
                //txtNewUserName.Focus();
                txtCurrentPassword.Focus();
                return;
            }

            UpdateSystemUser(userID, newPassword);

            FormsAuthentication.SignOut();
            //FormsAuthentication.RedirectFromLoginPage(userID, false);
            string Msg = "<script>alert('Your password has been changed. Click OK to sign in with your new password.');window.location = '/Login.aspx' </script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", Msg, false);

            //Response.Redirect("~/Default.aspx", false);
            //divlogin.Visible = true;
            //divChangePassword.Visible = false;
            //txtUserName.Focus();

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


    }
}