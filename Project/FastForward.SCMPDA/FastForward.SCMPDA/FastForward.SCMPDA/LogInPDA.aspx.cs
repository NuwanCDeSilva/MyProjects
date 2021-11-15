using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastForward.SCMPDA.Services;
using FF.BusinessObjects;
using System.Data;
using System.Net;
using System.Globalization;
using System.Web.Security;

namespace FastForward.SCMPDA
{
    public partial class LogInPDA : BasePage
    {
        private string userID, password, userName;
        private int _loginRetryInCounter = 0;
        private int _loginRetryOutCounter = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindUserCompanyDDLData(ddlcompany, string.Empty);
                    txtuser.Focus();
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = "Server not connected......!!!";
                //SetScrollTop();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('Server not conected.');", true);
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
                    {

                        foreach (SystemUserCompany item in _systemUserCompanyList)
                        {
                            ddl.Items.Add(new ListItem(item.MasterComp.Mc_desc, item.SEC_COM_CD));
                        }
                    }
                    else
                    {
                        ddl.Items.Add(new ListItem("--Select--", ""));
                    }
                }
                else
                {
                    ddl.Items.Add(new ListItem("--Select--", ""));
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = "Server Not connected !!!";
                //SetScrollTop();
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('Server not conected.');", true);
            }
        }
        protected void lbtnok_Click(object sender, EventArgs e)
        {
            try
            {
                divok.Visible = false;
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('Server not conected.');", true);
            }
        }

        protected void lbtnalert_Click(object sender, EventArgs e)
        {
            try
            {
                divalert.Visible = false;
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('Server not conected.');", true);
            }
        }

        protected void lbtninfo_Click(object sender, EventArgs e)
        {
            try
            {
                Divinfo.Visible = false;
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('Server not conected.');", true);
            }
        }

        private void DivsHide()
        {
            try
            {
                divalert.Visible = false;
                Divinfo.Visible = false;
                divok.Visible = false;
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('Server not conected.');", true);
            }
        }
        public string getLogUserIP()
        {
            Session["GlbHostName"] = Dns.GetHostName().ToString();
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
        public string GetIP()
        {
            IPHostEntry host;
            string localIP = "";
            Session["GlbHostName"] = Dns.GetHostName();
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

        public string GetSBU()
        {
            string SBU = "";
            DataTable SBUTbl = CHNLSVC.Security.GetSBU_User(ddlcompany.SelectedValue, txtuser.Text.ToUpper(), null);
            foreach (DataRow dr in SBUTbl.Rows)
            {
                int value = Convert.ToInt32(dr[2].ToString());
                if (value == 1)
                {
                    SBU = dr[0].ToString();
                }
            }
            return SBU;
        }

        private void SetScrollTop()
        {
            try
            {
                Page.SetFocus(this.maindvlogin.ClientID);
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('Server not conected.');", true);
            }
        }

        private void Login_Process()
        {
            try
            {
                CHNLSVC.Security.ExitLoginSession(txtuser.Text.ToString().ToUpper(), ddlcompany.Text.ToString().ToUpper(), "-1");

                if (!string.IsNullOrEmpty(txtuser.Text) && !string.IsNullOrEmpty(txtpw.Text))
                {
                    if (ddlcompany.Items.Count <= 0)
                    {
                        string msgv = "Your have not assign any companies to login !!!";
                        //divalert.Visible = true;
                        //lblalert.Text = msgv;
                        //SetScrollTop();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + msgv + "');", true);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(txtuser.Text) && !string.IsNullOrEmpty(txtpw.Text) && !string.IsNullOrEmpty(ddlcompany.Text))
                {
                    //if (CheckServerDateTime() == false)
                    //{
                    //    Response.Redirect(Request.RawUrl, false);
                    //    return;
                    //}

                    CultureInfo ci = CultureInfo.CurrentCulture;
                    //if (ci.DateTimeFormat.ShortDatePattern.ToString().ToUpper() != "DD/MMM/YYYY")
                    //{
                    //    string mss = "Your system date format is incorrect.Please contact system administrator !!!";
                    //    divalert.Visible = true;
                    //    lblalert.Text = mss;
                    //    return;
                    //}

                    string _msg = string.Empty;
                    string _msgTitle = string.Empty;
                    int _msgStatus = 0;
                    Session["UserID"] = txtuser.Text.ToString().ToUpper();
                    Session["UserCompanyCode"] = ddlcompany.SelectedItem.Value.ToString();
                    Session["UserCompanyName"] = ddlcompany.SelectedItem.Text.ToString();
                    Session["UserIP"] = Request.UserHostAddress;
                    Session["UserComputer"] = Request.UserHostName;
                    string vers=CHNLSVC.Security.GetCurrentVersion().ToString();
                    Session["version"] = vers;// BaseCls.GlbVersionNo.ToString();
                   // Session["UserID"] = txtuser.Text.ToString().ToUpper();
                    Session["UserCompanyName"] = ddlcompany.Text.ToString().ToUpper();
                    Session["GlbUserIP"] = getLogUserIP();// GetIP();
                    string _WindowsLogonName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    string _WindowsUser = Environment.UserName;
                    Session["GlbSBU"] = GetSBU();
                    LoginUser _loginUser = CHNLSVC.Security.LoginToSystem(txtuser.Text.ToString().ToUpper(), txtpw.Text.ToString(), ddlcompany.Text.ToString().ToUpper(), vers, (string)Session["GlbUserIP"], (string)Session["GlbHostName"], _loginRetryInCounter, out _loginRetryOutCounter, out _msgStatus, out _msg, out _msgTitle);
                    if (_msgStatus == -1)
                    {
                        if (_loginUser.Login_attempts >= _loginRetryOutCounter)
                        {
                            _loginRetryInCounter = _loginRetryOutCounter;
                            txtuser.Text = "";
                            txtpw.Focus();
                            //divalert.Visible = true;
                            //lblalert.Text = _msg;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + _msg + "');", true);
                            return;
                        }
                        else
                        {
                            CHNLSVC.Security.Save_User_Falis(txtuser.Text.ToString(), txtpw.Text.ToString(), Session["UserCompanyName"].ToString(), (string)Session["GlbUserIP"], _WindowsLogonName, _WindowsUser);
                            _msg = "You have failed to remember your details.Contact Administration for further instructions !!!";
                            //divalert.Visible = true;
                            //lblalert.Text = _msg;
                            //SetScrollTop();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + _msg + "');", true);
                            return;
                        }
                    }
                    else if (_msgStatus == -2)
                    {
                        _loginRetryInCounter = _loginRetryOutCounter;
                        //divalert.Visible = true;
                        //lblalert.Text = _msg;
                        //SetScrollTop();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + _msg.Replace("\n","") + "');", true);
                        return;
                    }
                    else if (_msgStatus == 1 || _msgStatus == -3)
                    {
                        //string _ipAdd = string.Empty;
                        //string _pcName = string.Empty;
                        //string _loginTime = string.Empty;

                        //if (txtuser.Text.ToString().ToUpper() != "ADMIN")
                        //{
                        //    if (CHNLSVC.Security.IsActiveSessions(txtuser.Text.ToString().ToUpper(), ddlcompany.Text.ToString().ToUpper(), out _ipAdd, out _pcName, out _loginTime) == true)
                        //    {
                        //        divalert.Visible = true;
                        //        string msg = "Active session is exist for the user '" + txtuser.Text.ToString() + "'" + "\n \n * Computer Name       :  " + _pcName + "\n * Computer IP              :  " + _ipAdd + "\n * Logged on                 :  " + _loginTime + "\n\nDo you want to close the existing session and log to system?";
                        //        lblalert.Text = msg;
                        //        SetScrollTop();
                        //        return;
                        //    }
                        //}
                        Base Base = new Base();
                        Base.SetLoginCacheLayer(_loginUser, _WindowsLogonName, _WindowsUser);

                        if (_loginUser.Pw_must_change == true)
                        {
                            _msg = "The user's password must be changed before logging on the this time.";
                            //divalert.Visible = true;
                            //lblalert.Text = _msg;
                            //SetScrollTop();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + _msg + "');", true);
                            return;
                        }
                        if (_loginUser.Pw_expire == true)
                        {
                            if (_loginUser.Pw_expire_must_change == true)
                            {
                                divalert.Visible = true;
                                _msg = "Your password has expired and must be changed!";
                                //lblalert.Text = _msg;
                                //SetScrollTop();
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + _msg + "');", true);
                                return;
                            }
                        }
                        FormsAuthentication.SetAuthCookie(userID, false);
                        Session["UserDefProf"] = _loginUser.User_def_pc;
                        Response.Redirect("Default.aspx");
                    }
                    else if (_msgStatus == -4)
                    {
                        divalert.Visible = true;
                        //lblalert.Text = _msg;
                        //SetScrollTop();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + _msg + "');", true);
                        return;
                    }
                    else if (_msgStatus == -99)
                    {
                        divalert.Visible = true;
                        //lblalert.Text = _msg;
                        //SetScrollTop();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + _msg + "');", true);
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + ex.Message.ToString() + "');", true);
            }
            finally
            {
                CHNLSVC.CloseChannel();
                GC.Collect();
            }
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                Login_Process();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + ex.Message.ToString() + "');", true);
            }
        }

        protected void txtuser_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtuser.Text))
                {
                    BindUserCompanyDDLData(ddlcompany, txtuser.Text.ToUpper().ToString());
                    txtpw.Focus();
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + ex.Message.ToString() + "');", true);
            }
        }

    }
}