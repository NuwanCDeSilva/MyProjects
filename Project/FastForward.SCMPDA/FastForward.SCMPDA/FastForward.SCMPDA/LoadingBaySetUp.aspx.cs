using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastForward.SCMPDA.Services;
using FF.BusinessObjects;

namespace FastForward.SCMPDA
{
    public partial class LoadingBaySetUp : BasePage
    {
        string _userid = string.Empty;
        string _warehcom = string.Empty;
        string _warehcomloc = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadUserLoadingBays();
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + ex.Message.ToString() + "');", true);
            } 
        }

        private void SetScrollTop()
        {
            try
            {
                Page.SetFocus(this.maindvlb.ClientID);
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + ex.Message.ToString() + "');", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + ex.Message.ToString() + "');", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + ex.Message.ToString() + "');", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + ex.Message.ToString() + "');", true);
            }
        }

        private void LoadUserLoadingBays()
        {
            try
            {
                _userid = (string)Session["UserID"];
                _warehcom = (string)Session["WAREHOUSE_COMPDA"];
                _warehcomloc = (string)Session["WAREHOUSE_LOCPDA"];

                DataTable dtuserloadingpoints = CHNLSVC.Inventory.LoadUserLoadingBays(_userid, Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), _warehcom, _warehcomloc);

                string defaultpoint = string.Empty;

                foreach (DataRow ddr in dtuserloadingpoints.Rows)
                {
                    if (ddr["selb_def_lbcd"].ToString() == "1")
                    {
                        defaultpoint = ddr["selb_lb_cd"].ToString();
                    }    
                }

                if (dtuserloadingpoints.Rows.Count > 0)
                {
                    ddlbay.DataSource = dtuserloadingpoints;
                    ddlbay.DataTextField = "mws_res_name";
                    ddlbay.DataValueField = "selb_lb_cd";
                    ddlbay.DataBind();
                }

                ddlbay.SelectedValue = defaultpoint;
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + ex.Message.ToString() + "');", true);
            }
        }

        protected void btnselect_Click(object sender, EventArgs e)
        {
            try
            {
                Session["LOADING_POINT"] = ddlbay.SelectedValue;
                Session["LOADING_POINT_NAME"] = ddlbay.SelectedItem.Text;
                Response.Redirect("Default.aspx",false);
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