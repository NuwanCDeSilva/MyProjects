using FastForward.SCMPDA.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FF.BusinessObjects;

namespace FastForward.SCMPDA
{
    public partial class CreateJob : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string scantype1 = (string)Session["DOCDIRECTION"];
                if (scantype1 != "0")
                {
                    itmCdPnl.Visible = false;

                }
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
                Page.SetFocus(this.maindvcrjob.ClientID);
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
                    SetScrollTop();
                    return;
                }
                
                Session["Doctype"] = ddldoctype.Text;
                Session["ToLocation"] = txtToLocation.Text.Trim();
                string scantype1 = (string)Session["DOCDIRECTION"];
                string loginLocation = Session["UserDefLoca"].ToString();
                if (txtToLocation.Text.Trim() == loginLocation)
                {
                    divalert.Visible = true;
                    lblalert.Text = "Cannot create jobs to same location!";
                    SetScrollTop();
                    return;
                }
                string location = Session["UserDefLoca"].ToString(); 
                string company = Session["UserCompanyName"].ToString();
                if (txtToLocation.Text.Trim().ToUpper() != "")
                {
                    DataTable cate = CHNLSVC.Inventory.Get_location_by_code(company, txtToLocation.Text.Trim().ToUpper());
                    string toCate3 = "";
                    if (cate.Rows.Count > 0)
                    {
                        if (cate.Rows[0]["ML_CATE_3"] != DBNull.Value)
                        {
                            toCate3 = cate.Rows[0]["ML_CATE_3"].ToString();
                        }

                    }
                    DataTable _permCatwise = CHNLSVC.Inventory.GetToLocationPermission(company, location, company, toCate3, "AODOUT_DIRECT");

                    DataTable _permLocwise = CHNLSVC.Inventory.GetToLocationPermission(company, location, company, txtToLocation.Text.Trim().ToUpper(), "AODOUT_DIRECT");
                    if (_permLocwise == null || _permLocwise.Rows.Count <= 0)
                    {
                        if (_permCatwise == null || _permCatwise.Rows.Count <= 0)
                        {
                            divalert.Visible = true;
                            lblalert.Text = "Permission Required for the dispatch location. Please check the location !";
                            txtToLocation.Focus();
                            return;
                        }
                    }
                }
                if (txtToLocation.Text.ToString() != "" && scantype1=="0")
                {
                    DataTable templocation = CHNLSVC.Inventory.GetTempPickLocations(txtToLocation.Text.ToString().Trim());
                    if (templocation.Rows.Count == 0)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Invalid location";
                        //SetScrollTop();
                        return;
                    }
                   
                    if (templocation.Rows[0]["ML_COM_CD"].ToString() != company)
                    {
                        divalert.Visible = true;
                        lblalert.Text = "Cannot send to different company  locations.";
                        //SetScrollTop();
                        return;
                    }
                }
                if (scantype1 == "1")
                {
                    Response.Redirect("CreateJobNumber.aspx");
                }
                else
                {
                    if (Session["UserCompanyName"].ToString() == "AAL")
                    {
                        Response.Redirect("CreateOutJobNew.aspx");
                    }
                    else
                    {
                        Response.Redirect("CreateOutJob.aspx");
                    }
                }
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

        protected void btToLocClk_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = "";
                if (txtToLocation.Text.ToString() != "")
                {
                    DataTable templocation = CHNLSVC.Inventory.GetTempPickLocations(txtToLocation.Text.ToString().Trim());
                    if (templocation.Rows.Count == 0) {
                        msg = "Invalid location.";
                    }
                }
                divalert.Visible = true;
                lblalert.Text = msg;
            }
            catch (Exception ex) {
                divalert.Visible = true;
                lblalert.Text = ex.Message;
            }
        }

    }
}