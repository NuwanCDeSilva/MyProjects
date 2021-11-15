using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMPDA
{
    public partial class ChangeLoadingBay : BasePage
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
                    BindGrid();
                    GetSelectedRecord();
                }

                string ispointchanged = (string)Session["BAY_CHANGED"];
                if (!string.IsNullOrEmpty(ispointchanged))
                {
                    string pointname = (string)Session["LOADING_POINT_NAME"];
                    divok.Visible = true;
                    lblok.Text = "Loading point changed to " + pointname;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "HideLabel();", true);
                }
                Session["BAY_CHANGED"] = null;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            } 
        }

        private void GetSelectedRecord()
        {
            try
            {
                string loadingpoint = (string)Session["LOADING_POINT"];

                foreach (GridViewRow ddr in grdloadingbay.Rows)
                {
                    Int32 gridline = ddr.RowIndex;
                    RadioButton rb = (RadioButton)grdloadingbay.Rows[gridline]
                                    .Cells[0].FindControl("RadioButton1");

                    HiddenField hf = (HiddenField)grdloadingbay.Rows[gridline]
                                           .Cells[0].FindControl("HiddenField1");

                    if (loadingpoint == hf.Value)
                    {
                        if (rb != null)
                        {
                            rb.Checked = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void BindGrid()
        {
            try
            {
                _userid = (string)Session["UserID"];
                _warehcom = (string)Session["WAREHOUSE_COMPDA"];
                _warehcomloc = (string)Session["WAREHOUSE_LOCPDA"];

                DataTable dtuserloadingpoints = CHNLSVC.Inventory.LoadUserLoadingBays(_userid, Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), _warehcom, _warehcomloc);

                if (dtuserloadingpoints.Rows.Count > 0)
                {
                    grdloadingbay.DataSource = dtuserloadingpoints;
                    grdloadingbay.DataBind();
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

        protected void btnback_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Settings.aspx");
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        private void SetSelectedRecord()
        {
            try
            {
                foreach (GridViewRow ddr in grdloadingbay.Rows)
                {
                    Int32 gridline = ddr.RowIndex;
                    RadioButton rb = (RadioButton)grdloadingbay.Rows[gridline]
                                    .Cells[0].FindControl("RadioButton1");

                    HiddenField hf = (HiddenField)grdloadingbay.Rows[gridline]
                                           .Cells[0].FindControl("HiddenField1");

                    HiddenField hf2 = (HiddenField)grdloadingbay.Rows[gridline]
                                          .Cells[0].FindControl("HiddenField2");

                    if (rb != null)
                    {
                        if (rb.Checked == true)
                        {
                            string pointcode = hf.Value;
                            string pointcodename = hf2.Value;

                            Session["LOADING_POINT"] = pointcode;
                            Session["LOADING_POINT_NAME"] = pointcodename;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void btnchangeloc_Click(object sender, EventArgs e)
        {
            try
            {
                SetSelectedRecord();
                Session["BAY_CHANGED"] = "1";
                Response.Redirect(Request.RawUrl, false);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }
    }
}