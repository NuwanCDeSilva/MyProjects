using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastForward.SCMPDA.Services;
using FF.BusinessObjects;
using System.Data; 

namespace FastForward.SCMPDA
{
    public partial class Default : BasePage
    {
        string _userid = string.Empty;
        string _warehcom = string.Empty;
        string _warehcomloc = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtchk_warehouse_availability = CHNLSVC.Inventory.CheckWareHouseAvailability(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString());

                if (dtchk_warehouse_availability.Rows.Count > 0)
                {
                    foreach (DataRow ddrware in dtchk_warehouse_availability.Rows)
                    {
                        Session["WAREHOUSE_COMPDA"] = ddrware["ml_wh_com"].ToString();
                        Session["WAREHOUSE_LOCPDA"] = ddrware["ml_wh_cd"].ToString();
                    }

                    DataTable dtbays = CHNLSVC.Inventory.LoadLoadingBays(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), "LB");

                    if (dtbays.Rows.Count > 0)
                    {
                        _userid = (string)Session["UserID"];
                        _warehcom = (string)Session["WAREHOUSE_COMPDA"];
                        _warehcomloc = (string)Session["WAREHOUSE_LOCPDA"];

                        DataTable dtuserloadingpoints = CHNLSVC.Inventory.LoadUserLoadingBays(_userid, Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString(), _warehcom, _warehcomloc);

                        string userloadingpoint = (string)Session["LOADING_POINT_NAME"];

                        if ((dtuserloadingpoints.Rows.Count > 0) && (string.IsNullOrEmpty(userloadingpoint)))
                        {
                            Response.Redirect("LoadingBaySetUp.aspx", false);
                        }
                        else if ((dtuserloadingpoints.Rows.Count == 0) && (string.IsNullOrEmpty(userloadingpoint)))
                        {
                            Session["WAREHOUSE_COMPDA"] = null;
                            Session["WAREHOUSE_LOCPDA"] = null;
                            Response.Redirect("Error.aspx?ERROR=2", false);
                        }
                    }
                    else
                    {
                        Session["WAREHOUSE_COMPDA"] = null;
                        Session["WAREHOUSE_LOCPDA"] = null;
                        Response.Redirect("Error.aspx?ERROR=1", false);
                    }
                }
                else {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('Please setup location wharehouse location and company.');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('"+ex.Message.ToString()+"');", true);
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

        private void SetScrollTop()
        {
            try
            {
                Page.SetFocus(this.maindvdef.ClientID);
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
        protected void btnsetings_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Settings.aspx");
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + ex.Message.ToString() + "');", true);

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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + ex.Message.ToString() + "');", true);
            }
        } 
        protected void btnscan_Click(object sender, EventArgs e)
        {
            try
            {
                _warehcom = (string)Session["WAREHOUSE_COMPDA"];
                _warehcomloc = (string)Session["WAREHOUSE_LOCPDA"];
                if (string.IsNullOrEmpty(_warehcom) || string.IsNullOrEmpty(_warehcomloc))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please setup location wharehouse location and company";
                    SetScrollTop();
                    return;
                }
                DivsHide();
                if ((ddltypes.SelectedValue != "1") && (ddltypes.SelectedValue != "0") && (ddltypes.SelectedValue != "2"))
                {
                    divalert.Visible = true;
                    lblalert.Text = "Please select Category";
                    SetScrollTop();
                    return;
                }

                Session["DOCDIRECTION"] = ddltypes.SelectedValue;
                if (ddltypes.SelectedValue == "1") {
                    DataTable dtbincode = CHNLSVC.Inventory.LoadBinCode(Session["UserCompanyName"].ToString(), Session["UserDefLoca"].ToString());
                    Session["dtbincode"] = dtbincode;
                    DataTable dtstatus=CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyName"].ToString());
                    Session["dtstatus"] = dtstatus;
                }
                Response.Redirect("ScanItem.aspx",false);
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                //lblalert.Text = ex.Message;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "alert('" + ex.Message.ToString() + "');", true);
            }
        }

        protected void ddltypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DivsHide();
                Session["DOCDIRECTION"] = ddltypes.SelectedValue;
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