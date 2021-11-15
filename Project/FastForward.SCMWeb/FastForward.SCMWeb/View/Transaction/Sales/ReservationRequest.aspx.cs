using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Sales
{
    public partial class ReservationRequest : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ValidateTrue();
                if (!IsPostBack)
                {
                    PageClear();
                    PopulateddlRequestReason();
                    PopulateddlRequestType();
                    Int32 validExpDtRng = getValidExpDateRange();
                    if (validExpDtRng == -1)
                    {
                        txtExpectedDate.Text = "";
                        lbtnExpectedDate.Visible = false;
                        return;
                    }
                    else
                    {
                        lbtnExpectedDate.Visible = true;
                        txtExpectedDate.Text = DateTime.Now.AddDays(validExpDtRng).ToString("dd/MMM/yyyy") ;
                    }
                }
                
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void txtRequestNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRequestNo.Text))
                {
                    txtRequestNo.Text = string.Empty;
                    DisplayMessage("Please Enter Request No. ", 2);
                    txtRequestNo.Focus();
                    return;

                }

                DataTable dt = CHNLSVC.Sales.Check_INT_REQ_RER(txtRequestNo.Text, "RER");

                if (dt.Rows.Count != 1)
                {
                    txtRequestNo.Text = string.Empty;
                    DisplayMessage("Invalid Request Number.", 2);
                    txtRequestNo.Focus();
                    return;
                }
                else
                {
                    txtRequestNo.Text = dt.Rows[0]["ITR_REQ_NO"].ToString();
                    txtexcutive.Text = dt.Rows[0]["itr_anal2"].ToString();
                    txtexcutive_TextChanged(null, null);
                    PopulateRequestData();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing... ", 4);
                return;
            }
        }
        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykeyD.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyD.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
        }
        protected void lbtRequestNo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Regex.IsMatch(txtFDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
                {

                }
                else
                {
                    txtFDate.Text = DateTime.Now.Date.AddMonths(-1).ToString("dd/MMM/yyyy");

                }
                if (Regex.IsMatch(txtTDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
                {

                }
                else
                {
                    txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");

                }

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RequestNo);
                DataTable result = CHNLSVC.CommonSearch.SearchResvationRequestNo(SearchParams, null, null, Convert.ToDateTime(DateTime.Now).Date.AddMonths(-1), Convert.ToDateTime(DateTime.Now).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "420";
                BindUCtrlDDLData2(result);
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage("Invalid Request No.", 2);
            }
        }
        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustomer.Text))
                {
                    txtCustomer.Text = string.Empty;
                    txtCustomer.ToolTip = string.Empty;
                    DisplayMessage("Please Enter Invoice Customer.", 2);
                    return;
                }
                DataTable dt = CHNLSVC.Financial.CheckBusentity(Session["UserCompanyCode"].ToString(), "C", null, txtCustomer.Text);

                if (dt.Rows.Count != 1)
                {
                    txtCustomer.Text = string.Empty;
                    txtCustomer.ToolTip = string.Empty;
                    DisplayMessage("Invalid Invoice Customer.", 2);
                    return;
                }
                else
                {
                    txtCustomer.Text = dt.Rows[0]["CODE"].ToString();
                    txtCustomer.ToolTip = dt.Rows[0]["NAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "401";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnRequestType_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RequestType);
                DataTable result = CHNLSVC.CommonSearch.SearchRequestType(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "418";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnSalesExcecutive_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesExcecutive);
                DataTable result = CHNLSVC.CommonSearch.SearchExecutive(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "419";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    txtItem.Text = string.Empty;
                    DisplayMessage("Please Enter Item Code.", 2);
                    txtItem.Focus();
                    return;
                }

                DataTable dt = CHNLSVC.Financial.GetItemDetails(txtItem.Text);

                if (dt.Rows.Count <= 0)
                {
                    txtItem.Text = string.Empty;
                    DisplayMessage("Please Enter Item Code.", 2);
                    txtItem.Focus();
                    return;
                }
                else
                {
                    DataTable dtItem = CHNLSVC.Financial.GetItemDetails(txtItem.Text);

                    lblModel.Text = dtItem.Rows[0]["MI_MODEL"].ToString();
                    lblDescription.Text = dtItem.Rows[0]["MI_SHORTDESC"].ToString();
                    lblBrand.Text = dtItem.Rows[0]["MI_BRAND"].ToString();
                    lblPartNo.Text = dtItem.Rows[0]["MI_PART_NO"].ToString();
                    ViewState["UOM"] = dtItem.Rows[0]["MI_ITM_UOM"].ToString();

                    txtItem.ReadOnly = true;
                }

            }
            catch (Exception ex)
            {

                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnItem_Click(object sender, EventArgs e)
        {
            try
            {
                grdResult.PageIndex = 0;
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItemforchange(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                grdResult.PageIndex = 0;
                lblvalue.Text = "407";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {

                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnItemStock_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    txtItem.Text = string.Empty;
                    DisplayMessage("Please Enter Item Code.", 2);
                    return;
                }
                List<INR_LOC> inr_loc = CHNLSVC.Sales.GetINR_LOC(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text);
                if (inr_loc.Count == 0)
                {
                    DisplayMessage("No stock balance available...!", 2);
                    return;
                }
                grdStock.DataSource = inr_loc;
                grdStock.DataBind();

                mpopStock.Show();
            }
            catch (Exception ex)
            {

                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void txtLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLocation.Text))
                {
                    txtLocation.Text = string.Empty;
                    DisplayMessage("Please Enter Location.", 2);
                    return;
                }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable dt = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, "Code", "%" + txtLocation.Text);
                if (dt.Rows.Count == 0)
                {
                    txtLocation.Text = string.Empty;
                    DisplayMessage("Please Enter Correct Location.", 2);
                    return;
                }
                txtQty.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnLocation_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlRequestType.SelectedItem.Text == " - - Select - - ")
                {
                    DisplayMessage("Please Select Request Type. ", 2);
                    return;
                }
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    DisplayMessage("Please enter Item.", 2);
                    return;
                }
                if (ddlStatus.SelectedItem.Text == " - - Select - - ")
                {
                    DisplayMessage("Please Select Status.", 2);
                    return;
                }
                //if (string.IsNullOrEmpty(txtLocation.Text))
                //{
                //    DisplayMessage("Please enter Location.", 2);
                //    return;
                //} 
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    DisplayMessage("Please enter Qty.", 2);
                    return;
                }
                if (!IsNumeric(txtQty.Text.Trim(), NumberStyles.Float))
                {
                    DisplayMessage("Please enter Qty.", 2);
                    txtQty.Text = "";
                    txtQty.Focus();
                    return;
                }
                List<INT_REQ_ITM> int_req_itm = new List<INT_REQ_ITM>();

                if (ViewState["int_req_itm"] != null)
                {
                    int_req_itm = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                }

                int seqno = 0;
                int lineNo = 1;
                if (int_req_itm.Count > 0)
                {
                    if (int_req_itm.Exists(x => x.ITRI_ITM_CD == txtItem.Text && x.ITRI_LOC == txtLocation.Text))
                    {
                        DisplayMessage("Duplicate Items Are Not Allowed.", 2);
                        return;
                    }

                    seqno = int_req_itm[0].ITRI_SEQ_NO;

                    lineNo = int_req_itm.Max(x => x.ITRI_LINE_NO) + 1;
                }

                INT_REQ_ITM item = new INT_REQ_ITM();
                item.ITRI_SEQ_NO = 0;
                item.ITRI_LINE_NO = lineNo;
                item.ITRI_ITM_CD = txtItem.Text;
                item.DESCRIPTION = lblDescription.Text;
                item.MODEL = lblModel.Text;
                item.ITRI_ITM_STUS = ddlStatus.SelectedValue;
                item.MIS_DESC = ddlStatus.SelectedItem.Text;
                item.ITRI_QTY = Convert.ToDecimal(txtQty.Text);
                //item.ITRI_UNIT_PRICE = Convert.ToDecimal(txtUnitPrice.Text);
                //item.AMOUNT = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text);
                item.ITRI_APP_QTY = Convert.ToDecimal(0);
                item.ITRI_RES_NO = "";
                item.ITRI_NOTE = "";
                item.ITRI_MITM_CD = "";
                item.ITRI_MITM_STUS = "";
                item.ITRI_MQTY = Convert.ToDecimal(0);
                item.ITRI_BQTY = Convert.ToDecimal(txtQty.Text);
                item.ITRI_ITM_COND = "";
                item.ITRI_JOB_NO = "";
                item.ITRI_JOB_LINE = 0;
                item.ITRI_COM = Session["UserCompanyCode"].ToString();
                item.ITRI_LOC = txtLocation.Text;

                int_req_itm.Add(item);

                grdItem.DataSource = int_req_itm;
                grdItem.DataBind();

                txtTotalQty.Text = int_req_itm.Sum(x => x.ITRI_QTY).ToString("N2");

                ViewState["int_req_itm"] = int_req_itm;

                txtItem.Text = string.Empty;
                txtItem.ReadOnly = false;
                txtQty.Text = string.Empty;
                txtLocation.Text = string.Empty;
                ddlStatus.SelectedValue = "0";

                lblModel.Text = string.Empty;
                lblDescription.Text = string.Empty;
                lblBrand.Text = string.Empty;
                lblPartNo.Text = string.Empty;

                ddlRequestType.Enabled = false;

            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnClearItem_Click(object sender, EventArgs e)
        {

            try
            {
                txtItem.Text = string.Empty;
                txtItem.ReadOnly = false;
                txtQty.Text = string.Empty;
                //txtUnitPrice.Text = string.Empty;
                txtLocation.Text = string.Empty;
                lblPartNo.Text = string.Empty;
                lblBrand.Text = string.Empty;
                lblModel.Text = string.Empty;
                lblDescription.Text = string.Empty;
                txtexcutive.Text = string.Empty;
                lblSalesEx.Text = "";
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnGrdSerial_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];

                ViewState["Item"] = item[grdr.RowIndex].ITRI_ITM_CD;
                ViewState["Location"] = item[grdr.RowIndex].ITRI_LOC;
                ViewState["RowIndex"] = grdr.RowIndex;

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable result = CHNLSVC.CommonSearch.Search_INR_SER(SearchParams, null, null);
                if (result.Rows.Count == 0)
                {
                    DisplayMessage("Serial is not available for this item", 2);
                    return;
                }
                grdResult1.DataSource = result;
                grdResult1.DataBind();
                lblvalue.Text = "50";
                BindUCtrlDDLData1(result);
                ViewState["SEARCH"] = result;


                List<INT_REQ_SER> req_serList = new List<INT_REQ_SER>();
                if (ViewState["int_req_ser"] != null)
                {
                    req_serList = (List<INT_REQ_SER>)ViewState["int_req_ser"];
                    req_serList = (from i in req_serList
                                   where i.ITRS_ITM_CD == ViewState["Item"].ToString()
                                   select i).ToList();
                }
                grdSerial.DataSource = req_serList;
                grdSerial.DataBind();

                PopupSerial.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnGrdItemDalete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDeleteconformmessageValue.Value == "No")
                {
                    return;
                }
                // if (!string.IsNullOrEmpty(txtStatus.Text))
                // {
                //    DisplayMessage("Can Not Delete Items", 2);
                //   return;
                // }
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                item.RemoveAt(grdr.RowIndex);

                //var row = (GridViewRow)btn.NamingContainer;
                // if (row != null)
                // {
                //     string _item = (row.FindControl("lblItemCodeEdit") as Label).Text;
                //     string _lineno = (row.FindControl("lblItemlineno") as Label).Text;
                //      CHNLSVC.Inventory.UPDATE_ITM_STUS(txtRequestNo.Text,Convert.ToInt32(_lineno),_item,)
                // }


                txtTotalQty.Text = item.Sum(x => x.ITRI_QTY).ToString("N2");

                ViewState["int_req_itm"] = item;
                grdItem.DataSource = item;
                grdItem.DataBind();

                if (item.Count <= 0)
                {
                    ddlRequestType.Enabled = true;
                    ddlRequestType.SelectedIndex = -1;
                }

            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnGrdItemEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                grdItem.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                grdItem.DataSource = item;
                grdItem.DataBind();

                TextBox txtQtyEdit = ((TextBox)grdItem.Rows[grdr.RowIndex].FindControl("txtQtyEdit"));
                txtQtyEdit.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnGrdItemCancel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                //e.Cancel = true;
                grdItem.EditIndex = -1;
                List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                grdItem.DataSource = item;
                grdItem.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnGrdItemUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                decimal qty;
                decimal unitrate;

                if (!decimal.TryParse(((TextBox)grdItem.Rows[grdr.RowIndex].FindControl("txtQtyEdit")).Text, out qty))
                {
                    DisplayMessage("Please Enter valid Item Qty.", 2);
                    return;
                }

                item[grdr.RowIndex].ITRI_QTY = qty;
                grdItem.EditIndex = -1;
                ViewState["int_req_itm"] = item;
                grdItem.DataSource = item;
                grdItem.DataBind();

                txtTotalQty.Text = item.Sum(x => x.ITRI_QTY).ToString("N2");

            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnSerialDalete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDeleteconformmessageValue.Value == "No")
                {
                    return;
                }

                //if (!string.IsNullOrEmpty(txtStatus.Text))
                //{
                //    lblWarning.Text = "Can Not Delete Items.";
                //    divWarning.Visible = true;
                //    return;
                //}
                LinkButton btn = (LinkButton)sender;
                GridViewRow grdr = (GridViewRow)btn.NamingContainer;
                List<INT_REQ_SER> item = (List<INT_REQ_SER>)ViewState["int_req_ser"];
                item.RemoveAt(grdr.RowIndex);

                ViewState["int_req_ser"] = item;

                item = (from i in item
                        where i.ITRS_ITM_CD == ViewState["Item"].ToString()
                        select i).ToList();

                grdSerial.DataSource = item;
                grdSerial.DataBind();

                PopupSerial.Show();

            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validateExpectedDate())
                {
                    return;
                }
                if (txtexcutive.Text == "")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16095))
                    {
                        DisplayMessage("Please add sales executive to continue.</br>You need 16095 permission to continue request without executive.", 4);
                        return;
                    }
                }
                if (hdnSave.Value == "Yes")
                {
                    
                    if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16014))
                    {
                        lblMssg.Text = "Do you want to approve this reservation ";
                        PopupConfBox.Show();
                        return;
                    }
                    DateTime date;
                    DateTime exDate;
                    if (!DateTime.TryParse(txtDate.Text, out date))
                    {
                        DisplayMessage("Please popupvalid Date.", 2);
                        return;
                    }
                    if (!DateTime.TryParse(txtExpectedDate.Text, out exDate))
                    {
                        DisplayMessage("Please select valid Expected Date.", 2);
                        return;
                    }
                   
                    //if (string.IsNullOrEmpty(txtCustomer.Text))
                    //{
                    //    DisplayMessage("Please enter Customer..", 2);
                    //    return;

                    //}
                    if (ddlRequestReason.SelectedItem.Text == " - - Select - - ")
                    {
                        DisplayMessage("Please select Request Reason.", 2);
                        return;
                    }
                    if (ddlRequestType.SelectedItem.Text == " - - Select - - ")
                    {
                        DisplayMessage("Please select Request Type.", 2);
                        return;
                    }
                    if (grdItem.Rows.Count == 0)
                    {
                        DisplayMessage("Please enter Items.", 2);
                        return;
                    }

                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_cd = "LOCALL";
                    masterAuto.Aut_cate_tp = "LOC";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "RER";
                    masterAuto.Aut_number = 5;
                    masterAuto.Aut_start_char = "RER";
                    masterAuto.Aut_year = Convert.ToInt32(DateTime.Now.ToString("yy"));

                    INT_REQ int_req = new INT_REQ();
                    int_req.ITR_SEQ_NO = 0;
                    int_req.ITR_COM = Session["UserCompanyCode"].ToString();
                    int_req.ITR_REQ_NO = "";
                    int_req.ITR_TP = "RER";
                    int_req.ITR_SUB_TP = ddlRequestReason.SelectedItem.Value;
                    int_req.ITR_LOC = Session["UserDefLoca"].ToString();
                    int_req.ITR_REF = "";
                    int_req.ITR_DT = date;
                    int_req.ITR_EXP_DT = exDate;
                    int_req.ITR_STUS = "P";
                    int_req.ITR_JOB_NO = "";
                    int_req.ITR_BUS_CODE = txtCustomer.Text;
                    int_req.ITR_NOTE = txtRemarks.Text;
                    int_req.ITR_ISSUE_FROM = "";
                    int_req.ITR_REC_TO = "";
                    int_req.ITR_DIRECT = 0;
                    int_req.ITR_COUNTRY_CD = "";
                    int_req.ITR_TOWN_CD = "";
                    int_req.ITR_CUR_CODE = "";
                    int_req.ITR_EXG_RATE = Convert.ToDecimal(0);
                    int_req.ITR_COLLECTOR_ID = "";
                    int_req.ITR_COLLECTOR_NAME = "";
                    int_req.ITR_ACT = 1;
                    int_req.ITR_CRE_BY = Session["UserID"].ToString();
                    int_req.ITR_CRE_DT = DateTime.Now;
                    int_req.ITR_MOD_BY = "";
                    int_req.ITR_MOD_DT = DateTime.Now;
                    int_req.ITR_SESSION_ID = Session["SessionID"].ToString();
                    int_req.ITR_ANAL1 = ddlRequestType.SelectedItem.Value;
                    int_req.ITR_ANAL2 = txtexcutive.Text;
                    int_req.ITR_ANAL3 = "";
                    int_req.ITR_ISSUE_COM = Session["UserCompanyCode"].ToString();
                    int_req.ITR_GRAN_OPT1 = 0;
                    int_req.ITR_GRAN_OPT2 = 0;
                    int_req.ITR_GRAN_OPT3 = 0;
                    int_req.ITR_GRAN_OPT4 = 0;
                    int_req.ITR_GRAN_NSTUS = "";
                    int_req.ITR_GRAN_APP_BY = "";
                    int_req.ITR_GRAN_NARRT = "";
                    int_req.ITR_GRAN_APP_NOTE = "";
                    int_req.ITR_GRAN_APP_STUS = "";
                    int_req.ITR_JOB_LINE = 0;
                    int_req.ITR_APP_BY = Session["UserID"].ToString();
                    int_req.ITR_APP_DT = DateTime.Now;

                    List<INT_REQ_ITM> int_req_itm = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                    List<INT_REQ_SER> int_req_ser = (List<INT_REQ_SER>)ViewState["int_req_ser"];

                    int row_aff = 0;
                    string msg;

                    row_aff = CHNLSVC.Sales.SaveReservationRequest(int_req, int_req_itm, int_req_ser, masterAuto, out msg);
                    if (row_aff >= 1)
                    {
                        InventoryRequest _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest() { Itr_req_no = msg }).FirstOrDefault();
                        #region genaret mail 11 Jan 2017 by lakshan
                        if (_invReq != null)
                        {
                            _invReq.InventoryRequestItemList = CHNLSVC.Inventory.GET_INT_REQ_ITM_DATA_BY_REQ_NO(msg);
                            _invReq.Itr_rec_to = Session["UserDefLoca"].ToString();
                            try
                            {
                                CHNLSVC.MsgPortal.SendMailReservationRequestApprove(_invReq, "REQUEST");
                            }
                            catch (Exception ex)
                            {
                                string _msg = "Successfully Created. Request No: " + msg + " and email send fail.";
                                DisplayMessage(_msg, 3);
                                PageClear();
                            }
                        }
                        #endregion
                    }
                    if (row_aff >= 1)
                    {
                        string _msg = "Successfully Created. Request No: " + msg;
                        DisplayMessage(_msg, 3);
                        PageClear();
                        //return;
                    }
                    else
                    {
                        string _msg = "Insert Fail ...";
                        DisplayMessage(_msg, 1);
                        //return;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnUpdate.Value == "Yes")
                {
                    if (txtRequestNo.Text == "")
                    {
                        DisplayMessage("Please select request number.", 2);
                        return;
                    }
                    if (!validateExpectedDate())
                    {
                        return;
                    }
                    DateTime date;
                    DateTime exDate;
                    if (!DateTime.TryParse(txtDate.Text, out date))
                    {
                        DisplayMessage("Please select valid Date.", 2);
                        return;
                    }
                    if (!DateTime.TryParse(txtExpectedDate.Text, out exDate))
                    {
                        DisplayMessage("Please select valid Expected Date.", 2);
                        return;
                    }
                    if (ddlRequestReason.SelectedItem.Text == " - - Select - - ")
                    {
                        DisplayMessage("Please select Request Reason.", 2);
                        return;
                    }
                    if (ddlRequestType.SelectedItem.Text == " - - Select - - ")
                    {
                        DisplayMessage("Please select Request Type.", 2);
                        return;
                    }
                    //if (string.IsNullOrEmpty(txtCustomer.Text))
                    //{
                    //    DisplayMessage("Please enter Customer.", 2);
                    //    return;
                    //}
                    if (grdItem.Rows.Count == 0)
                    {
                        DisplayMessage("Please enter Items.", 2);
                        return;
                    }

                    INT_REQ req = new INT_REQ();
                    req = (INT_REQ)ViewState["int_req"];

                    INT_REQ int_req = new INT_REQ();
                    int_req.ITR_SEQ_NO = req.ITR_SEQ_NO;
                    int_req.ITR_COM = Session["UserCompanyCode"].ToString();
                    int_req.ITR_REQ_NO = req.ITR_REQ_NO;
                    int_req.ITR_TP = "RER";
                    int_req.ITR_SUB_TP = ddlRequestReason.SelectedItem.Value;
                    int_req.ITR_LOC = Session["UserDefLoca"].ToString();
                    int_req.ITR_REF = "";
                    int_req.ITR_DT = date;
                    int_req.ITR_EXP_DT = exDate;
                    int_req.ITR_STUS = "P";
                    int_req.ITR_JOB_NO = "";
                    int_req.ITR_BUS_CODE = txtCustomer.Text;
                    int_req.ITR_NOTE = txtRemarks.Text;
                    int_req.ITR_ISSUE_FROM = "";
                    int_req.ITR_REC_TO = "";
                    int_req.ITR_DIRECT = 0;
                    int_req.ITR_COUNTRY_CD = "";
                    int_req.ITR_TOWN_CD = "";
                    int_req.ITR_CUR_CODE = "";
                    int_req.ITR_EXG_RATE = Convert.ToDecimal(0);
                    int_req.ITR_COLLECTOR_ID = "";
                    int_req.ITR_COLLECTOR_NAME = "";
                    int_req.ITR_ACT = 1;
                    int_req.ITR_CRE_BY = Session["UserID"].ToString();
                    int_req.ITR_CRE_DT = DateTime.Now;
                    int_req.ITR_MOD_BY = Session["UserID"].ToString();
                    int_req.ITR_MOD_DT = DateTime.Now;
                    int_req.ITR_SESSION_ID = Session["SessionID"].ToString();
                    int_req.ITR_ANAL1 = ddlRequestType.SelectedItem.Value;
                    int_req.ITR_ANAL2 = txtexcutive.Text;
                    int_req.ITR_ANAL3 = "";
                    int_req.ITR_ISSUE_COM = Session["UserCompanyCode"].ToString();
                    int_req.ITR_GRAN_OPT1 = 0;
                    int_req.ITR_GRAN_OPT2 = 0;
                    int_req.ITR_GRAN_OPT3 = 0;
                    int_req.ITR_GRAN_OPT4 = 0;
                    int_req.ITR_GRAN_NSTUS = "";
                    int_req.ITR_GRAN_APP_BY = "";
                    int_req.ITR_GRAN_NARRT = "";
                    int_req.ITR_GRAN_APP_NOTE = "";
                    int_req.ITR_GRAN_APP_STUS = "";
                    int_req.ITR_JOB_LINE = 0;
                    int_req.ITR_APP_BY = Session["UserID"].ToString();
                    int_req.ITR_APP_DT = DateTime.Now;

                    List<INT_REQ_ITM> int_req_itm = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                    List<INT_REQ_SER> int_req_ser = (List<INT_REQ_SER>)ViewState["int_req_ser"];

                    int row_aff = 0;
                    string msg;

                    row_aff = CHNLSVC.Sales.UpdateSalesOrderRequest(int_req, int_req_itm, out msg);

                    if (row_aff >= 1)
                    {
                        string _msg = "Successfully Updated. Request No: " + txtRequestNo.Text;
                        DisplayMessage(_msg, 3);
                        PageClear();
                        return;
                    }
                    else
                    {
                        string _msg = "Updated Fail ...";
                        DisplayMessage(_msg, 1);
                        return;

                    }
                }

            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnCancel_Click(object sender, EventArgs e)
        {
            string err = "";
            try
            {
                if (hdnCancel.Value == "Yes")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16015))
                    {

                        string _Msg = "Sorry, You have no permission to cancel this Reservation Request. Advice Required permission code  16015";
                        DisplayMessage(_Msg, 2);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtRequestNo.Text))
                    {
                        txtRequestNo.Text = string.Empty;
                        DisplayMessage("Please Enter Request No.", 2);
                        return;
                    }

                    int row_aff = CHNLSVC.Sales.UpdateStatus_INT_REQ(txtRequestNo.Text, "C");

                    if (row_aff == 1)
                    {

                        string _Msg = "Successfully Canceled. Request No: " + txtRequestNo.Text;
                        DisplayMessage(_Msg, 3);
                        PageClear();
                        return;
                    }
                    else
                    {
                        DisplayMessage("Cancel Fail ... ", 2);
                        return;

                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {

            try
            {
                if (hdnClear.Value == "Yes")
                {
                    try
                    {
                        Response.Redirect(Request.RawUrl, false);
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message.ToString(), 4);
            }
        }
        protected void lbtnApprove_Click(object sender, EventArgs e)
        {
            string err = "";
            try
            {
                if (hdnApprove.Value == "Yes")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16014))
                    {

                        string _Msg = "Sorry, You have no permission to approve this Reservation Request. Advice Required permission code  16014";
                        DisplayMessage(_Msg, 2);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtRequestNo.Text))
                    {
                        DisplayMessage("Please enter Request No.", 2);
                        return;
                    }
                    if (txtStatus.Text == "APPROVED")
                    {
                        DisplayMessage("Already approved request.", 2);
                        return;

                    }
                    int count = CHNLSVC.Sales.UpdateStatus_INT_REQ(txtRequestNo.Text, "A");
                    if (count == 1)
                    {
                        InventoryRequest _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest() { Itr_req_no = txtRequestNo.Text.Trim() }).FirstOrDefault();
                        _invReq.InventoryRequestItemList = CHNLSVC.Inventory.GET_INT_REQ_ITM_DATA_BY_REQ_NO(txtRequestNo.Text.Trim());
                        string _Msg = "Successfully Approved. Request No: " + txtRequestNo.Text;
                        DisplayMessage(_Msg, 3);
                        PageClear();
                        #region genaret mail 11 Jan 2017 by lakshan
                        if (_invReq != null)
                        {
                            _invReq.Itr_rec_to = Session["UserDefLoca"].ToString();
                            try
                            {
                                CHNLSVC.MsgPortal.SendMailReservationRequestApprove(_invReq, "APPROVE");
                            }
                            catch (Exception ex)
                            {
                                string _msg = _Msg + " and email send fail.";
                                DisplayMessage(_msg, 3);
                            }
                        }
                        #endregion
                        return;
                    }
                    else
                    {
                        string _Msg = "Approned Fail ." + err;
                        DisplayMessage(_Msg, 2);
                        PageClear();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message.ToString(), 4);
            }
        }
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                if (lblvalue.Text == "Sale_Ex")
                {
                    txtexcutive.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtexcutive.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    lblSalesEx.Text = grdResult.SelectedRow.Cells[2].Text;
                }
                //Item
                if (lblvalue.Text == "407")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[2].Text;
                    txtItem_TextChanged(null, null);
                }
                //RequestNo
                if (lblvalue.Text == "420")
                {
                    txtRequestNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtRequestNo_TextChanged(null, null);
                }
                //Customer
                if (lblvalue.Text == "401")
                {
                    txtCustomer.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtCustomer.ToolTip = grdResult.SelectedRow.Cells[3].Text;
                    txtCustomer_TextChanged(null, null);
                }
                //Location
                if (lblvalue.Text == "5")
                {
                    txtLocation.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                //Serial
                if (lblvalue.Text == "50")
                {
                    if (!GetSerialVisibility())
                    {
                        //lblWarning.Text = "Duplicate Items Are Not Allowed.";
                        //divWarning.Visible = true;
                        PopupSerial.Show();
                        return;
                    }

                    List<INT_REQ_SER> req_serList = new List<INT_REQ_SER>();
                    INT_REQ_SER req_ser = new INT_REQ_SER();

                    if (ViewState["int_req_ser"] != null)
                    {
                        req_serList = (List<INT_REQ_SER>)ViewState["int_req_ser"];
                    }
                    int lineNo = 1;
                    if (req_serList.Count > 0)
                    {
                        if (req_serList.Exists(x => x.ITRS_ITM_CD == ViewState["Item"].ToString() && x.ITRS_SER_1 == grdResult1.SelectedRow.Cells[1].Text))
                        {
                            //lblWarning.Text = "Duplicate Items Are Not Allowed.";
                            //divWarning.Visible = true;
                            PopupSerial.Show();
                            return;
                        }
                        if (req_serList.Exists(x => x.ITRS_ITM_CD == ViewState["Item"].ToString()))
                        {
                            List<INT_REQ_SER> req = (from re in req_serList
                                                     where re.ITRS_ITM_CD == ViewState["Item"].ToString()
                                                     select re).ToList();
                            lineNo = req.Max(x => x.ITRS_SER_LINE) + 1;
                        }

                    }

                    List<INT_REQ_ITM> item = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                    int rowIndex = Convert.ToInt32(ViewState["RowIndex"]);

                    req_ser.ITRS_SEQ_NO = item[rowIndex].ITRI_SEQ_NO;
                    req_ser.ITRS_LINE_NO = item[rowIndex].ITRI_LINE_NO;
                    req_ser.ITRS_SER_LINE = lineNo;
                    req_ser.ITRS_ITM_CD = ViewState["Item"].ToString();
                    req_ser.ITRS_ITM_STUS = item[rowIndex].ITRI_ITM_STUS;
                    req_ser.ITRS_SER_1 = grdResult1.SelectedRow.Cells[1].Text;
                    req_ser.ITRS_SER_2 = grdResult1.SelectedRow.Cells[2].Text;
                    if (req_ser.ITRS_SER_2 == "&nbsp;")
                    {
                        req_ser.ITRS_SER_2 = string.Empty;
                    }
                    //req_ser.ITRS_SER_3        = 
                    //req_ser.ITRS_SER_4        = 
                    req_ser.ITRS_QTY = item[rowIndex].ITRI_QTY;
                    //req_ser.ITRS_IN_SEQNO     = 
                    //req_ser.ITRS_IN_DOCNO     = 
                    //req_ser.ITRS_IN_ITMLINE   = 
                    //req_ser.ITRS_IN_BATCHLINE = 
                    //req_ser.ITRS_IN_SERLINE   = 
                    //req_ser.ITRS_IN_DOCDT     = 
                    //req_ser.ITRS_TRNS_TP      = 
                    //req_ser.ITRS_RMK          = 
                    //req_ser.ITRS_SER_ID       = 
                    //req_ser.ITRS_NITM_STUS    = 

                    req_serList.Add(req_ser);

                    ViewState["int_req_ser"] = req_serList;

                    req_serList = (from i in req_serList
                                   where i.ITRS_ITM_CD == ViewState["Item"].ToString()
                                   select i).ToList();

                    grdSerial.DataSource = req_serList;
                    grdSerial.DataBind();

                    lblvalue.Text = "50";
                    PopupSerial.Show();
                }
                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text) || !string.IsNullOrEmpty(txtSearchbyword1.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }
        private void FilterData()
        {
            if (lblvalue.Text == "Sale_Ex")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Sale_Ex";
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            //Item
            if (lblvalue.Text == "407")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItemforchange(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "407";
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            //RequestType
            else if (lblvalue.Text == "418")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RequestType);
                DataTable result = CHNLSVC.CommonSearch.SearchRequestType(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "418";
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            //SearchExecutive
            else if (lblvalue.Text == "419")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesExcecutive);
                DataTable result = CHNLSVC.CommonSearch.SearchExecutive(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "419";
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            //Customer
            else if (lblvalue.Text == "401")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierImport);
                DataTable result = CHNLSVC.CommonSearch.SearchBusEntity(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "401";
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            //RequestNo
            else if (lblvalue.Text == "420")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RequestNo);
                DataTable result = CHNLSVC.CommonSearch.Search_INT_REQ_RER(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "420";
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            //Location
            else if (lblvalue.Text == "5")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "5";
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            //Serial
            else if (lblvalue.Text == "50")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable result = CHNLSVC.CommonSearch.Search_INR_SER(SearchParams, ddlSearchbykey1.SelectedItem.Text, txtSearchbyword1.Text);
                grdResult1.DataSource = result;
                grdResult1.DataBind();
                lblvalue.Text = "50";
                ViewState["SEARCH"] = result;
                PopupSerial.Show();
            }
            else if (ViewState["SEARCH"] != null)
            {
                DataTable result = (DataTable)ViewState["SEARCH"];
                DataView dv = new DataView(result);
                string searchParameter = ddlSearchbykey.Text;
                dv.RowFilter = "" + ddlSearchbykey.Text + " like '%" + txtSearchbyword.Text + "%'";
                // dv.RowFilter = "REFERENCESNO = '" + txtSearchbyword.Text + "' ";
                if (dv.Count > 0)
                {
                    result = dv.ToTable();
                }
                grdResult.DataSource = result;
                grdResult.DataBind();
                UserPopoup.Show();
            }

        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SupplierImport:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "C" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RequestType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesExcecutive:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + ViewState["Location"] + seperator + ViewState["Item"] + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RequestNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "RER" + seperator);
                        break;
                    }
               
                default:
                    break;
            }
            return paramsText.ToString();
        }
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.ddlSearchbykey.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey.SelectedIndex = 0;
        }
        public void BindUCtrlDDLData1(DataTable _dataSource)
        {
            this.ddlSearchbykey1.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykey1.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykey1.SelectedIndex = 0;
        }
        private void PopulateRequestData()
        {
            if (string.IsNullOrEmpty(txtRequestNo.Text))
            {
                txtRequestNo.Text = string.Empty;
                DisplayMessage("Please Enter Request No.", 2);
                return;
            }

            INT_REQ int_req = new INT_REQ();
            int_req = CHNLSVC.Sales.GetRequest(txtRequestNo.Text);

            if (int_req == null)
            {
                txtRequestNo.Text = string.Empty;
                DisplayMessage("Invalid Request Details.", 2);
                return;
            }

            ViewState["int_req"] = int_req;

            DataTable dt = CHNLSVC.Sales.Select_MST_STUS(int_req.ITR_STUS);

            txtDate.Text = int_req.ITR_DT.ToString("dd/MMM/yyyy");
            txtExpectedDate.Text = int_req.ITR_EXP_DT.ToString("dd/MMM/yyyy");
            txtStatus.Text = dt.Rows[0]["MSS_DESC"].ToString();
            txtCustomer.Text = int_req.ITR_BUS_CODE;
            txtRemarks.Text = int_req.ITR_NOTE;

            ddlRequestReason.SelectedValue = int_req.ITR_SUB_TP;
            PopulateddlRequestType();
            ddlRequestType.SelectedValue = int_req.ITR_ANAL1;

            List<INT_REQ_ITM> int_req_itm = new List<INT_REQ_ITM>();
            int_req_itm = CHNLSVC.Sales.GetRequestItem(int_req.ITR_SEQ_NO);

            if (int_req_itm.Count <= 0)
            {
                txtRequestNo.Text = string.Empty;
                DisplayMessage("Invalid Request Item Details.", 2);
                return;
            }


            grdItem.DataSource = int_req_itm;
            grdItem.DataBind();
            ViewState["int_req_itm"] = int_req_itm;
            txtTotalQty.Text = int_req_itm.Sum(x => x.ITRI_QTY).ToString("N2");

            List<INT_REQ_SER> int_req_ser = new List<INT_REQ_SER>();
            int_req_ser = CHNLSVC.Sales.GetINT_REQ_SER(int_req.ITR_SEQ_NO);
            ViewState["int_req_ser"] = int_req_ser;

            lbtnSave.Enabled = false;
            lbtnSave.OnClientClick = "return Enable();";
            lbtnSave.CssClass = "buttoncolor";

            lbtnUpdate.Enabled = false;
            lbtnUpdate.OnClientClick = "return Enable();";
            lbtnUpdate.CssClass = "buttoncolor";
            //lbtnCancel.Enabled = false;
            //lbtnCancel.OnClientClick = "return Enable();";
            //lbtnCancel.CssClass = "buttoncolor";
            //lbtnApprove.Enabled = false;
            //lbtnApprove.OnClientClick = "return Enable();";
            //lbtnApprove.CssClass = "buttoncolor";

            if (int_req_itm.Sum(x => x.ITRI_APP_QTY) == 0 && int_req.ITR_STUS == "P")
            {
                //if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16014))
                //{
                //    lbtnApprove.Enabled = true;
                //    lbtnApprove.OnClientClick = "ConfirmApprove();";
                //    lbtnApprove.CssClass = "buttonUndocolor";
                //}
                //if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16015))
                //{
                //    lbtnCancel.Enabled = true;
                //    lbtnCancel.OnClientClick = "ConfirmCancel();";
                //    lbtnCancel.CssClass = "buttonUndocolor";
                //}

                lbtnUpdate.Enabled = true;
                lbtnUpdate.OnClientClick = "ConfirmUpdate();";
                lbtnUpdate.CssClass = "buttonUndocolor";
            }

            if (int_req_itm.Sum(x => x.ITRI_APP_QTY) == 0 && int_req.ITR_STUS == "A")
            {
                //if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16015))
                //{
                //    lbtnCancel.Enabled = true;
                //    lbtnCancel.OnClientClick = "ConfirmCancel();";
                //    lbtnCancel.CssClass = "buttonUndocolor";
                //}
            }

            lbtnAdd.Visible = true;
            lbtnClearItem.Visible = true;
            ItemAdd.Visible = true;

            if (txtStatus.Text == "PENDING")
            {
                ViewState["DaleteVisibility"] = false;
            }
            else
            {
                ViewState["DaleteVisibility"] = true;
            }
        }
        private void PopulateddlRequestReason()
        {
            ddlRequestReason.DataSource = CHNLSVC.Sales.Select_REF_LOC_CATE1();
            ddlRequestReason.DataTextField = "RLC_DESC";
            ddlRequestReason.DataValueField = "RLC_CD";
            ddlRequestReason.DataBind();
            ddlRequestReason.Items.Insert(0, new ListItem(" - - Select - - ", "0"));
            ddlRequestReason.SelectedValue = "0";
        }
        private void PopulateddlRequestType()
        {
            ddlRequestType.DataSource = CHNLSVC.Sales.Select_REF_REQ_SUBTP(ddlRequestReason.SelectedValue);
            ddlRequestType.DataTextField = "RRS_DESC";
            ddlRequestType.DataValueField = "RRS_TP";
            ddlRequestType.DataBind();
            ddlRequestType.Items.Insert(0, new ListItem(" - - Select - - ", "0"));
            ddlRequestType.SelectedValue = "0";
        }
        public Boolean GetDaleteVisibility()
        {
            Boolean b = false;
            b = (Boolean)ViewState["DaleteVisibility"];
            return b;
        }
        public Boolean GetSerialVisibility()
        {
            Boolean b = false;
            b = (Boolean)ViewState["DaleteVisibility"];
            return b;
        }
        private void ValidateTrue()
        {
            divWarning.Visible = false;
            lblWarning.Text = "";
            divSuccess.Visible = false;
            lblSuccess.Text = "";
            divAlert.Visible = false;
            lblAlert.Text = "";
        }
        private void PageClear()
        {
            txtRequestNo.Text = string.Empty;
            txtStatus.Text = string.Empty;

            txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            //added by kelum : 2016-June-04
            lbtnDate.Enabled = false;
            CalendarExtender3.Enabled = false;
            lbtnDate.CssClass = "buttoncolor";
            //
            txtExpectedDate.Text = DateTime.Now.AddMonths(1).ToString("dd/MMM/yyyy");

            txtCustomer.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            ddlRequestReason.SelectedIndex = 0;
            ddlRequestType.Enabled = true;
            ddlRequestType.SelectedIndex = 0;

            txtItem.Text = string.Empty;
            txtItem.ReadOnly = false;
            txtQty.Text = string.Empty;
            //ddlStatus.SelectedIndex = 0;
            //ddlStatus.Items.FindByText("GOOD").Selected = true;
            // ddlStatus.Items.FindByValue("GOD").Selected = true;
            ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue("GOD"));
            txtLocation.Text = string.Empty;

            lblModel.Text = string.Empty;
            lblDescription.Text = string.Empty;
            lblBrand.Text = string.Empty;
            lblPartNo.Text = string.Empty;

            lbtnAdd.Visible = true;
            lbtnClearItem.Visible = true;
            ItemAdd.Visible = true;

            grdItem.DataSource = new int[] { };
            grdItem.DataBind();
            grdSerial.DataSource = new int[] { };
            grdSerial.DataBind();
            grdResult.DataSource = new int[] { };
            grdResult.DataBind();
            grdResult1.DataSource = new int[] { };
            grdResult1.DataBind();

            txtTotalQty.Text = string.Empty;

            ViewState["int_req"] = null;
            ViewState["int_req_itm"] = null;
            ViewState["int_req_ser"] = null;
            ViewState["Item"] = null;
            ViewState["Location"] = null;
            ViewState["RowIndex"] = null;
            ViewState["DaleteVisibility"] = true;

            lbtnSave.Enabled = true;
            lbtnSave.OnClientClick = "ConfirmSave();";
            lbtnSave.CssClass = "buttonUndocolor";
            //lbtnUpdate.Enabled = false;
            //lbtnUpdate.OnClientClick = "return Enable();";
            //lbtnUpdate.CssClass = "buttoncolor";
            //lbtnCancel.Enabled = false;
            //lbtnCancel.OnClientClick = "return Enable();";
            //lbtnCancel.CssClass = "buttoncolor";
            //lbtnApprove.Enabled = false;
            //lbtnApprove.OnClientClick = "return Enable();";
            //lbtnApprove.CssClass = "buttoncolor";

        }
        private void DisplayMessage(String Msg, Int32 option)
        {
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
            }
            else if (option == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + Msg + "');", true);
            }
        }

        protected void grdResult1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult1.PageIndex = e.NewPageIndex;
                grdResult1.DataSource = null;
                grdResult1.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult1.DataBind();
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }

        protected void ddlRequestReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateddlRequestType();
        }

        protected void txtexcutive_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, "CODE", txtexcutive.Text);
                if (result.Rows.Count == 1)
                {
                    txtexcutive.Text = result.Rows[0][0].ToString();
                    txtexcutive.ToolTip = result.Rows[0][1].ToString();
                    lblSalesEx.Text = result.Rows[0][1].ToString();
                }
                else
                {
                    txtexcutive.Text = "";
                    txtexcutive.ToolTip = "";
                    lblSalesEx.Text = "";
                    DisplayMessage("Invalid sales executive code", 1);
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }


        protected void lbtnEx_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Sale_Ex";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopoup.Show();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }



        }

        protected void btnconfsave_Click(object sender, EventArgs e)
        {
            try
            {
                
                    DateTime date;
                    DateTime exDate;
                    if (!DateTime.TryParse(txtDate.Text, out date))
                    {
                        DisplayMessage("Please popupvalid Date.", 2);
                        return;
                    }
                    if (!DateTime.TryParse(txtExpectedDate.Text, out exDate))
                    {
                        DisplayMessage("Please select valid Expected Date.", 2);
                        return;
                    }

                    if (ddlRequestReason.SelectedItem.Text == " - - Select - - ")
                    {
                        DisplayMessage("Please select Request Reason.", 2);
                        return;
                    }
                    if (ddlRequestType.SelectedItem.Text == " - - Select - - ")
                    {
                        DisplayMessage("Please select Request Type.", 2);
                        return;
                    }
                    if (grdItem.Rows.Count == 0)
                    {
                        DisplayMessage("Please enter Items.", 2);
                        return;
                    }

                    MasterAutoNumber masterAuto = new MasterAutoNumber();
                    masterAuto.Aut_cate_cd = "LOCALL";
                    masterAuto.Aut_cate_tp = "LOC";
                    masterAuto.Aut_direction = null;
                    masterAuto.Aut_modify_dt = null;
                    masterAuto.Aut_moduleid = "RER";
                    masterAuto.Aut_number = 5;
                    masterAuto.Aut_start_char = "RER";
                    masterAuto.Aut_year = Convert.ToInt32(DateTime.Now.ToString("yy"));

                    INT_REQ int_req = new INT_REQ();
                    int_req.ITR_SEQ_NO = 0;
                    int_req.ITR_COM = Session["UserCompanyCode"].ToString();
                    int_req.ITR_REQ_NO = "";
                    int_req.ITR_TP = "RER";
                    int_req.ITR_SUB_TP = ddlRequestReason.SelectedItem.Value;
                    int_req.ITR_LOC = Session["UserDefLoca"].ToString();
                    int_req.ITR_REF = "";
                    int_req.ITR_DT = date;
                    int_req.ITR_EXP_DT = exDate;
                    int_req.ITR_STUS = "A";
                    int_req.ITR_JOB_NO = "";
                    int_req.ITR_BUS_CODE = txtCustomer.Text;
                    int_req.ITR_NOTE = txtRemarks.Text;
                    int_req.ITR_ISSUE_FROM = "";
                    int_req.ITR_REC_TO = "";
                    int_req.ITR_DIRECT = 0;
                    int_req.ITR_COUNTRY_CD = "";
                    int_req.ITR_TOWN_CD = "";
                    int_req.ITR_CUR_CODE = "";
                    int_req.ITR_EXG_RATE = Convert.ToDecimal(0);
                    int_req.ITR_COLLECTOR_ID = "";
                    int_req.ITR_COLLECTOR_NAME = "";
                    int_req.ITR_ACT = 1;
                    int_req.ITR_CRE_BY = Session["UserID"].ToString();
                    int_req.ITR_CRE_DT = DateTime.Now;
                    int_req.ITR_MOD_BY = "";
                    int_req.ITR_MOD_DT = DateTime.Now;
                    int_req.ITR_SESSION_ID = Session["SessionID"].ToString();
                    int_req.ITR_ANAL1 = ddlRequestType.SelectedItem.Value;
                    int_req.ITR_ANAL2 = txtexcutive.Text;
                    int_req.ITR_ANAL3 = "";
                    int_req.ITR_ISSUE_COM = Session["UserCompanyCode"].ToString();
                    int_req.ITR_GRAN_OPT1 = 0;
                    int_req.ITR_GRAN_OPT2 = 0;
                    int_req.ITR_GRAN_OPT3 = 0;
                    int_req.ITR_GRAN_OPT4 = 0;
                    int_req.ITR_GRAN_NSTUS = "";
                    int_req.ITR_GRAN_APP_BY = "";
                    int_req.ITR_GRAN_NARRT = "";
                    int_req.ITR_GRAN_APP_NOTE = "";
                    int_req.ITR_GRAN_APP_STUS = "";
                    int_req.ITR_JOB_LINE = 0;
                    int_req.ITR_APP_BY = Session["UserID"].ToString();
                    int_req.ITR_APP_DT = DateTime.Now;

                    List<INT_REQ_ITM> int_req_itm = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                    List<INT_REQ_SER> int_req_ser = (List<INT_REQ_SER>)ViewState["int_req_ser"];

                    int row_aff = 0;
                    string msg;

                    row_aff = CHNLSVC.Sales.SaveReservationRequest(int_req, int_req_itm, int_req_ser, masterAuto, out msg);
                    if (row_aff >= 1)
                    {
                        InventoryRequest _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest() { Itr_req_no = msg }).FirstOrDefault();
                   
                        #region genaret mail 11 Jan 2017 by lakshan
                        if (_invReq != null)
                        {
                            _invReq.InventoryRequestItemList = CHNLSVC.Inventory.GET_INT_REQ_ITM_DATA_BY_REQ_NO(msg);
                            _invReq.Itr_rec_to = Session["UserDefLoca"].ToString();
                            try
                            {
                                CHNLSVC.MsgPortal.SendMailReservationRequestApprove(_invReq, "APPROVE");
                            }
                            catch (Exception ex)
                            {
                                string _msg = "Successfully Created. Request No: " + msg+" and email send fail.";
                                DisplayMessage(_msg, 3);
                                PageClear();
                            }
                        }
                        #endregion
                    }
                    if (row_aff >= 1)
                    {
                        string _msg = "Successfully Created. Request No: " + msg;
                        DisplayMessage(_msg, 3);
                        PageClear();
                        //return;
                    }
                    else
                    {
                        string _msg = "Insert Fail ...";
                        DisplayMessage(_msg, 1);
                        //return;
                    }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message.ToString(), 4);
            }
        }
        protected void btnconfcancel_Click(object sender, EventArgs e)
        {
            try
            {


                DateTime date;
                DateTime exDate;
                if (!DateTime.TryParse(txtDate.Text, out date))
                {
                    DisplayMessage("Please popupvalid Date.", 2);
                    return;
                }
                if (!DateTime.TryParse(txtExpectedDate.Text, out exDate))
                {
                    DisplayMessage("Please select valid Expected Date.", 2);
                    return;
                }

                if (ddlRequestReason.SelectedItem.Text == " - - Select - - ")
                {
                    DisplayMessage("Please select Request Reason.", 2);
                    return;
                }
                if (ddlRequestType.SelectedItem.Text == " - - Select - - ")
                {
                    DisplayMessage("Please select Request Type.", 2);
                    return;
                }
                if (grdItem.Rows.Count == 0)
                {
                    DisplayMessage("Please enter Items.", 2);
                    return;
                }

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = "LOCALL";
                masterAuto.Aut_cate_tp = "LOC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "RER";
                masterAuto.Aut_number = 5;
                masterAuto.Aut_start_char = "RER";
                masterAuto.Aut_year = Convert.ToInt32(DateTime.Now.ToString("yy"));

                INT_REQ int_req = new INT_REQ();
                int_req.ITR_SEQ_NO = 0;
                int_req.ITR_COM = Session["UserCompanyCode"].ToString();
                int_req.ITR_REQ_NO = "";
                int_req.ITR_TP = "RER";
                int_req.ITR_SUB_TP = ddlRequestReason.SelectedItem.Value;
                int_req.ITR_LOC = Session["UserDefLoca"].ToString();
                int_req.ITR_REF = "";
                int_req.ITR_DT = date;
                int_req.ITR_EXP_DT = exDate;
                int_req.ITR_STUS = "P";
                int_req.ITR_JOB_NO = "";
                int_req.ITR_BUS_CODE = txtCustomer.Text;
                int_req.ITR_NOTE = txtRemarks.Text;
                int_req.ITR_ISSUE_FROM = "";
                int_req.ITR_REC_TO = "";
                int_req.ITR_DIRECT = 0;
                int_req.ITR_COUNTRY_CD = "";
                int_req.ITR_TOWN_CD = "";
                int_req.ITR_CUR_CODE = "";
                int_req.ITR_EXG_RATE = Convert.ToDecimal(0);
                int_req.ITR_COLLECTOR_ID = "";
                int_req.ITR_COLLECTOR_NAME = "";
                int_req.ITR_ACT = 1;
                int_req.ITR_CRE_BY = Session["UserID"].ToString();
                int_req.ITR_CRE_DT = DateTime.Now;
                int_req.ITR_MOD_BY = "";
                int_req.ITR_MOD_DT = DateTime.Now;
                int_req.ITR_SESSION_ID = Session["SessionID"].ToString();
                int_req.ITR_ANAL1 = ddlRequestType.SelectedItem.Value;
                int_req.ITR_ANAL2 = txtexcutive.Text;
                int_req.ITR_ANAL3 = "";
                int_req.ITR_ISSUE_COM = Session["UserCompanyCode"].ToString();
                int_req.ITR_GRAN_OPT1 = 0;
                int_req.ITR_GRAN_OPT2 = 0;
                int_req.ITR_GRAN_OPT3 = 0;
                int_req.ITR_GRAN_OPT4 = 0;
                int_req.ITR_GRAN_NSTUS = "";
                int_req.ITR_GRAN_APP_BY = "";
                int_req.ITR_GRAN_NARRT = "";
                int_req.ITR_GRAN_APP_NOTE = "";
                int_req.ITR_GRAN_APP_STUS = "";
                int_req.ITR_JOB_LINE = 0;
                int_req.ITR_APP_BY = Session["UserID"].ToString();
                int_req.ITR_APP_DT = DateTime.Now;

                List<INT_REQ_ITM> int_req_itm = (List<INT_REQ_ITM>)ViewState["int_req_itm"];
                List<INT_REQ_SER> int_req_ser = (List<INT_REQ_SER>)ViewState["int_req_ser"];

                int row_aff = 0;
                string msg;

                row_aff = CHNLSVC.Sales.SaveReservationRequest(int_req, int_req_itm, int_req_ser, masterAuto, out msg);
                if (row_aff >= 1)
                {
                    InventoryRequest _invReq = CHNLSVC.Inventory.GET_INT_REQ_DATA(new InventoryRequest() { Itr_req_no = msg }).FirstOrDefault();
                    #region genaret mail 11 Jan 2017 by lakshan
                    if (_invReq != null)
                    {
                        _invReq.InventoryRequestItemList = CHNLSVC.Inventory.GET_INT_REQ_ITM_DATA_BY_REQ_NO(msg);
                        _invReq.Itr_rec_to = Session["UserDefLoca"].ToString();
                        try
                        {
                            CHNLSVC.MsgPortal.SendMailReservationRequestApprove(_invReq, "REQUEST");
                        }
                        catch (Exception ex)
                        {
                            string _msg = "Successfully Created. Request No: " + msg + " and email send fail.";
                            DisplayMessage(_msg, 3);
                            PageClear();

                        }
                    }
                    #endregion
                }
                if (row_aff >= 1)
                {
                    string _msg = "Successfully Created. Request No: " + msg;
                    DisplayMessage(_msg, 3);
                    PageClear();
                    //return;
                }
                else
                {
                    string _msg = "Insert Fail ...";
                    DisplayMessage(_msg, 1);
                    //return;
                }

            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...", 4);
            }
        }

        protected void btnDClose_Click(object sender, EventArgs e)
        {
            txtSearchbywordD.Text = "";
            UserDPopoup.Hide();
        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "420")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RequestNo);
                DataTable result = CHNLSVC.CommonSearch.SearchResvationRequestNo(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "420";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
        }

        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {

        }

        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "420")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.RequestNo);
                DataTable result = CHNLSVC.CommonSearch.SearchResvationRequestNo(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text).Date, Convert.ToDateTime(txtTDate.Text).Date);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "420";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
        }

        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Name = grdResultD.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "420")
            {
                txtRequestNo.Text = grdResultD.SelectedRow.Cells[1].Text;
                txtRequestNo_TextChanged(null, null);
                lblvalue.Text = "";
            }

        }

        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (lblvalue.Text == "420")
            {
                grdResultD.PageIndex = e.NewPageIndex;
                grdResultD.DataSource = null;
                grdResultD.DataSource = (DataTable)ViewState["SEARCH"];
                grdResultD.DataBind();
                UserDPopoup.Show();
            }
        }
        public Int32 getValidExpDateRange()
        {
            try
            {

                Int32 numofdt=-1;
                if (Session["UserDefLoca"].ToString() != "")
                {
                    string error = string.Empty;
                    string locatoion = Session["UserDefLoca"].ToString();
                    string com = Session["UserCompanyCode"].ToString();
                    numofdt = CHNLSVC.Sales.getExpireNumberofDateConf(locatoion, com, out error);
                    if (error != "")
                    {
                        DisplayMessage(error, 4);
                        return -1;
                    }
                    if (numofdt == -1)
                    {
                        DisplayMessage("Please setup minimum expiry/expected date limit for location.", 2);
                        return -1;
                    }
                    return numofdt;
                }
                else
                {
                    DisplayMessage("Please setup location for user.", 2);
                    return -1;
                }
               
               
            }
            catch (Exception ex)
            {
                lblMssg.Text = ex.Message.ToString();
                PopupConfBox.Show();
                return -1;
            }
        }
       
        protected bool validateExpectedDate()
        {
            try
            {
                Int32 validExpDtRng = getValidExpDateRange();
                if (validExpDtRng == -1)
                {
                    txtExpectedDate.Text = "";
                    lbtnExpectedDate.Visible = false;
                    DisplayMessage("Please setup minimum expiry/expected date limit for location.", 2);
                    return false;
                }
                else
                {
                  DateTime Date = Convert.ToDateTime(txtDate.Text.ToString());
                  DateTime datetimexp =    DateTime.Now.AddDays(validExpDtRng);
                  DateTime selected = Convert.ToDateTime(txtExpectedDate.Text.ToString());
                  if (selected > datetimexp || Date > selected)
                  {
                      DisplayMessage("Please selected valid date between " + Date.ToString("dd/MMM/yyyy") + " and " + datetimexp.ToString("dd/MMM/yyyy"), 2);
                      return false;
                  }
                  return true;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message.ToString(), 4);
                return false;
            }
        }
    }
}