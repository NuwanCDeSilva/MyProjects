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
    public partial class CurrentJobSelect : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PopulateDropDowns();
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void SetScrollTop()
        {
            try
            {
                Page.SetFocus(this.dvcurrentjobselect.ClientID);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        private void PopulateDropDowns()
        {
            try
            {
                string docdirection = (string)Session["DOCDIRECTION"];
                DataTable tempdoctype = CHNLSVC.Inventory.GetTempPickDocTypes(Convert.ToInt32(docdirection));
                if (tempdoctype.Rows.Count > 0)
                {
                    ddldoctype.DataSource = tempdoctype;
                    ddldoctype.DataTextField = "tdt_tp_desc";
                    ddldoctype.DataValueField = "tdt_tp";
                    ddldoctype.DataBind();
                }
                ddldoctype.Items.Insert(0, new ListItem("Select", ""));
                ddldoctype.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
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
                divalert.Visible = true;
                lblalert.Text = ex.Message;
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
                divalert.Visible = true;
                lblalert.Text = ex.Message;
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
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("ScanItem.aspx");
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
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
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void btncurrjob_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                if (ddldoctype.SelectedIndex == 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please select Document Type !!!";
                    //SetScrollTop();
                    return;
                }

                string doctype = ddldoctype.SelectedValue;
                Session["DOCTYPE"] = doctype;
                Response.Redirect("CurrentJobs.aspx?DocType="+doctype, false);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void ddldoctype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["DOCTYPE"] = ddldoctype.SelectedValue;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void btnReOpen_Click(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                if (ddldoctype.SelectedIndex == 0)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please select Document Type !!!";
                   // SetScrollTop();
                    return;
                }
                string doctype = ddldoctype.SelectedValue;
                Session["DOCTYPE"] = doctype;
                Response.Redirect("ReopenJobs.aspx?DocType=" + doctype, false);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
    }
}