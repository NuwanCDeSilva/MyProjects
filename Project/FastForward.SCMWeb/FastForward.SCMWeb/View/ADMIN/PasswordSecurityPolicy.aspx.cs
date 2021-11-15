using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb
{
    public partial class PasswordSecurityPolicy : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    string _errString = string.Empty;
                    SecurityPolicy _securityPolicy = CHNLSVC.Security.GetSecurityPolicy(1, out _errString);
                    if (string.IsNullOrEmpty(_errString))
                    {
                        txtmaxpwag.Text = _securityPolicy.Spp_max_pw_age.ToString();
                        txtminpwag.Text = _securityPolicy.Spp_min_pw_age.ToString();
                        txtenfpwhis.Text = _securityPolicy.Spp_pw_histtory.ToString();
                        txtminpwlen.Text = _securityPolicy.Spp_min_pw_length.ToString();
                        txtlockfail.Text = _securityPolicy.Spp_lock_err_atmps.ToString();
                        txtchar.Text = _securityPolicy.Spp_cons_ident_char.ToString();
                        chkpwusename.Checked = _securityPolicy.Spp_notmatch_usr;
                        chkpwcomplex.Checked = _securityPolicy.Spp_pw_complexity;
                        //chkworddict.Checked = _securityPolicy.Spp_pw_dictionary;

                        if (chkpwusename.Checked == true)
                        {
                            chkpwusename.Text = "Enabled";
                        }
                        else
                        {
                            chkpwusename.Text = "Disabled";
                        }


                        if (chkpwcomplex.Checked == true)
                        {
                            chkpwcomplex.Text = "Enabled";
                        }
                        else
                        {
                            chkpwcomplex.Text = "Disabled";
                        }

                        //if (chkworddict.Checked == true)
                        //{
                        //    chkworddict.Text = "Enabled";
                        //}
                        //else
                        //{
                        //    chkworddict.Text = "Disabled";
                        //}
                    }
                    else
                    {
                        Response.Redirect("~/Error.aspx?Error=" + _errString + "");
                    }
                }
            }
            catch (Exception err)
            {
                Response.Redirect("~/Error.aspx?Error=" + err.Message.ToString() + "");
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }


        protected void btnadd_Click1(object sender, EventArgs e)
        {
            try
            {
                //for (int i = 0; i < 1500; i++)
                //{

                //    if (i == 1200)
                //    {
                //        i = 1200;
                //    }
                //    List<MasterDepartment> _deptList = CHNLSVC.General.GetDepartment();
                //    _deptList = null;

                //}
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sucessfully updated')", true);
                DivAsk.Visible = true;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void chkpwusename_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkpwusename.Checked == true)
                {
                    chkpwusename.Text = "Enabled";
                }
                else
                {
                    chkpwusename.Text = "Disabled";
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void chkpwcomplex_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkpwcomplex.Checked == true)
                {
                    chkpwcomplex.Text = "Enabled";
                }
                else
                {
                    chkpwcomplex.Text = "Disabled";
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void chkworddict_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if (chkworddict.Checked == true)
                //{
                //    chkworddict.Text = "Enabled";
                //}
                //else
                //{
                //    chkworddict.Text = "Disabled";
                //}
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtColse_Click(object sender, EventArgs e)
        {
            DivAsk.Visible = false;
        }
    }
}