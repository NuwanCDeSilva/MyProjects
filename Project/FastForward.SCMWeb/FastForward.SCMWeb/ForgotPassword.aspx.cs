using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb
{
    public partial class ForgotPassword : BasePage
    {
        private readonly string invaldconnection = "Server connection error.please contact administrator!";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GlbisLoging = true;
                if (!Page.IsPostBack)
                {
                    txtUserId.Focus();
                }
            }
            catch (Exception ex)
            {

                msg(invaldconnection);
                return;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ClearMsg();

                if (txtEmail.Text != "" || txtUserId.Text != "")
                {
                    bool valid = IsValidEmail(txtEmail.Text.Trim());
                    if (valid == true)
                    {
                        string email = txtEmail.Text.Trim();
                        string id = txtUserId.Text.Trim();
                        int status = CHNLSVC.Security.CheckUserAvailability(id, email);
                        if (status == 1)
                        {
                            string path = HttpContext.Current.Server.MapPath("~/Email/resetPassword.html");
                            string message = File.ReadAllText(path);
                            string host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                            int respo = CHNLSVC.Security.SendPasswordResetEmail(email, id, message, host);
                            if (respo == 1)
                            {
                                txtEmail.Text = "";
                                txtUserId.Text = "";
                                msg("Please check email to reset password.","info");
                            }else if(respo ==-1){
                                msg("Unable to reset user password.");
                            }
                            else {
                                msg("Unable to reset password.");
                            }

                        }
                        else if (status == -1)
                        {
                            msg("Locked user.Please contact administrator");
                        }
                        else if (status == 0)
                        {
                            msg("Inactive user");
                        }
                        else if (status == -2)
                        {
                            msg("Disable user");
                        }
                        else if (status == -100)
                        {
                            msg("Invalid user name or email");
                        }
                    }
                    else
                    {
                        msg("Please enter valid email.");
                    }

                }
                else
                {
                    msg("Invalid input.");
                }
            }
            catch (Exception ex) {
                msg(invaldconnection);
            }
        }

        private void ClearMsg()
        {
            divwarnning.Visible = false;
            divInfo.Visible = false;
        }
        private void msg(string _msg,string type="error")
        {
            if (type == "error") {
                divwarnning.Visible = true;
                lblWarning.Text = _msg;
            }
            else if (type == "info")
            {
                divInfo.Visible = true;
                lblInfo.Text = _msg;
            }
            
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}