using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMPDA
{
    public partial class CheckJobItems : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdjobitems.DataSource = new int[] { };
                grdjobitems.DataBind();
                LoadGrid();
            }
        }

        protected void LoadGrid() {
            DataTable dtitems = CHNLSVC.Inventory.loadDocumentItems((string)Session["DOCNO"]);
            grdjobitems.DataSource = null;
            grdjobitems.DataBind();

            grdjobitems.DataSource = dtitems;
            grdjobitems.DataBind();
        }
        protected void btnback_Click(object sender, EventArgs e)
        {
            try
            {
                string docdirection = (string)Session["DOCDIRECTION"];

                if (docdirection == "1")
                {
                    Response.Redirect("CreateJobNumber.aspx?JobNo=" + (string)Session["DOCNO"]);
                }
                else
                {
                    if (Session["UserCompanyName"].ToString() == "AAL")
                    {
                        Response.Redirect("CreateOutJobNew.aspx?JobNo=" + (string)Session["DOCNO"]);
                    }
                    else
                    {
                        Response.Redirect("CreateOutJob.aspx?JobNo=" + (string)Session["DOCNO"]);
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
        protected void lbtndicalertclose_Click(object sender, EventArgs e)
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

        protected void lbtndivinfoclose_Click(object sender, EventArgs e)
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
    }
}