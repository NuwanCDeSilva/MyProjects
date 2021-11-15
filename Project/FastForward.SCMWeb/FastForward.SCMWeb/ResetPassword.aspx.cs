using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FastForward.SCMWeb
{
    public partial class ResetPassword : BasePage
    {
        private readonly string invaldconnection = "Server not connected......!!!";
        public string email = "";
        public string id = "";
        string secTOkecn = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ClearMsg();
                    if (Request.QueryString["token"] != null && Request.QueryString["id"] != null)
                    {

                        secTOkecn = Request.QueryString["token"].Trim();
                        id = Request.QueryString["id"].Trim();
                        if (!string.IsNullOrEmpty(secTOkecn) && !string.IsNullOrEmpty(id))
                        {
                            bool response = CHNLSVC.Security.CheckPwResetAuth(secTOkecn, id);

                            if (response == true)
                            {
                                txtNewPw.Focus();
                            }
                            else 
                           {
                               msg("Invalid or expired security token.");
                           }
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Error.aspx?Error=Invalid request");
                    }
                }
            }
            catch (Exception ex) {
                msg(ex.Message.ToString());
                return;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                secTOkecn = Request.QueryString["token"].Trim();
                id = Request.QueryString["id"].Trim();
                string _err = string.Empty;
                if (!string.IsNullOrEmpty(secTOkecn))
                {
                    bool resp = CHNLSVC.Security.CheckPwResetAuth(secTOkecn, id);
                    if (resp == true)
                    {
                        if (txtNewPw.Text.Trim() != "" && txtConfPw.Text.Trim() != "")
                        {
                            if (txtNewPw.Text.Trim() == txtConfPw.Text.Trim())
                            {
                                string pw = txtNewPw.Text.Trim();
                                if (CHNLSVC.Security.CheckPasswordPolicy(id, pw, out  _err) == true)
                                {
                                    string path = HttpContext.Current.Server.MapPath("~/Email/successMsg.html");
                                    string message = File.ReadAllText(path);
                                    string host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                                    bool response = CHNLSVC.Security.UpdateUserPassword(pw, secTOkecn, id, message, host);
                                    if (response == true)
                                    {
                                        Response.Redirect("~/Login.aspx");
                                    }
                                    else
                                    {
                                        msg("Unable to update password.");
                                    }
                                }
                                else
                                {
                                    msg(_err);
                                }
                            }
                            else
                            {
                                msg("Password does not match.");
                            }
                        }
                        else
                        {
                            msg("Invalid password enterd.");
                        }
                    }
                    else {
                        msg("Invalid security token.");
                    }
                }
                else
                {
                    msg("Invalid request.");
                }
            }
            catch (Exception ex) {
                msg(ex.Message.ToString());
                return;
            }
        }
        private void msg(string _msg)
        {
            divwarnning.Visible = true;
            lblWarning.Text = _msg;
        }
        private void ClearMsg()
        {
            divwarnning.Visible = false;
            //DivAsk.Visible = false;
        }

    }
}