using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastForward.SCMPDA.Services;
using FF.BusinessObjects;
using System.Data;
using System.Text;

namespace FastForward.SCMPDA
{
    public partial class ChangeLocation : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindGrid();
                    GetSelectedRecord();
                }

                string islocchanged = (string)Session["LOCCHANGED"];
                if (!string.IsNullOrEmpty(islocchanged))
                {
                    divok.Visible = true;
                    lblok.Text = "Location changed to " + Session["UserDefLoca"].ToString();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "HideLabel();", true);
                }
                Session["LOCCHANGED"] = null;
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            } 
        }

        private void SetScrollTop()
        {
            try
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "scrolltop", "scrollTop();", true);
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
                foreach (GridViewRow ddr in grdlocation.Rows)
                {
                    Int32 gridline = ddr.RowIndex;
                    RadioButton rb = (RadioButton)grdlocation.Rows[gridline]
                                    .Cells[0].FindControl("RadioButton1");

                    HiddenField hf = (HiddenField)grdlocation.Rows[gridline]
                                           .Cells[0].FindControl("HiddenField1");
                    if (rb != null)
                    {
                        if (rb.Checked == true)
                        {
                            Session["UserDefLoca"] = hf.Value;
                        }
                    }
                }

                Session["WAREHOUSE_COMPDA"] = null;
                Session["WAREHOUSE_LOCPDA"] = null;
                Session["LOADING_POINT"] = null;
                Session["LOADING_POINT_NAME"] = null;
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
                string UserLoc = Session["UserDefLoca"].ToString();

                foreach (GridViewRow ddr in grdlocation.Rows)
                {
                    Int32 gridline = ddr.RowIndex;
                    RadioButton rb = (RadioButton)grdlocation.Rows[gridline]
                                    .Cells[0].FindControl("RadioButton1");

                    HiddenField hf = (HiddenField)grdlocation.Rows[gridline]
                                           .Cells[0].FindControl("HiddenField1");

                    if (UserLoc == hf.Value)
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
                DataTable dtlocatoin = CHNLSVC.CommonSearch.LoadUserLocation(Session["UserID"].ToString(), Session["UserCompanyName"].ToString());
                if (dtlocatoin.Rows.Count > 0)
                {
                    grdlocation.DataSource = dtlocatoin;
                    grdlocation.DataBind();
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
        protected void btnchangeloc_Click(object sender, EventArgs e)
        {
            try
            {
                SetSelectedRecord();
                Session["LOCCHANGED"] = "1";
                Response.Redirect(Request.RawUrl, false);
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

        protected void btnfindloc_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtlocatoin = CHNLSVC.CommonSearch.LoadUserLocation(Session["UserID"].ToString(), Session["UserCompanyName"].ToString());
                if (dtlocatoin.Rows.Count > 0)
                {
                    grdlocation.DataSource = dtlocatoin;
                    grdlocation.DataBind();
                }

                DataView dv = new DataView(dtlocatoin);

                dv.RowFilter = "Code = '" + txtfindloc.Text.ToUpper().Trim() + "'";

                dtlocatoin = dv.ToTable();

                grdlocation.DataSource = dtlocatoin;
                grdlocation.DataBind();

                GetSelectedRecord();
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

        protected void txtfindloc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                btnfindloc_Click(null, null);
            }
            catch (Exception ex)
            {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

    }
}