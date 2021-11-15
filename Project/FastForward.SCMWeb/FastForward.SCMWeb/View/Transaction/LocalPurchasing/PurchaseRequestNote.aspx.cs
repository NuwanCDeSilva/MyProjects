using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Inventory;
using FF.BusinessObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Local_Purchasing
{
    public partial class PurchaseRequestNote : BasePage
    {

        DataTable uniqueCols = new DataTable();
        string _userid = string.Empty;
        private MasterItem _itemdetail = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string UserDefProf = Session["UserDefProf"].ToString();

                if (string.IsNullOrEmpty(UserDefProf))
                {
                    //string msg = "You are not authorized to view this page, profit center not set up...!!!";
                    //Response.Redirect("~/Error.aspx?Error=" + msg + "");

                    //Udaya modify 14.08.2017
                    Response.Redirect("~/View/ADMIN/Home.aspx");
                }

                if (!IsPostBack)
                {
                    grdorderdetails.DataSource = new int[] { };
                    grdorderdetails.DataBind();

                    PopulateDropDowns();

                    DateTime orddate = DateTime.Now;
                    txtRequestDate.Text = orddate.ToString("dd/MMM/yyyy");

                    DateTime orddate1 = DateTime.Now;
                    txtRequriedDate.Text = orddate1.ToString("dd/MMM/yyyy");

                    ViewState["PRNItemsTable"] = null;

                    DataTable dtitems = new DataTable();
                    dtitems.Columns.AddRange(new DataColumn[7] { new DataColumn("Item"), new DataColumn("Description"), new DataColumn("Brand"), new DataColumn("Model"), new DataColumn("Status"), new DataColumn("Qty"), new DataColumn("Remarks") });
                    ViewState["PRNItemsTable"] = dtitems;
                    this.BindGrid();

                    foreach (GridViewRow hiderowbtn in this.grdorderdetails.Rows)
                    {
                        LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndelitem");

                        _delbutton.Enabled = false;
                        _delbutton.CssClass = "buttonUndocolor";
                        _delbutton.OnClientClick = "ConfirmDelete();";
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void BindGrid()
        {
            try
            {
                grdorderdetails.DataSource = (DataTable)ViewState["PRNItemsTable"];
                grdorderdetails.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "407")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                    DataTable result = CHNLSVC.CommonSearch.SearchItemforchange(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "407";
                    ViewState["SEARCH"] = result;
                    UserPopup.Show();
                    txtSearchbyword.Focus();
                }
                //Modified By Udaya 14.08.2017 Searching add new SP
                else if (lblvalue.Text == "409")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PRNRequest);
                    DataTable result = CHNLSVC.CommonSearch.SEARCH_PRN_REQNo(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);//SearchPRNREQNoforall
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "409";
                    ViewState["SEARCH"] = result;
                    UserPopup.Show();
                    txtSearchbyword.Focus();
                }
                else if (ViewState["SEARCH"] != null)
                {
                    DataTable result = (DataTable)ViewState["SEARCH"];
                    DataView dv = new DataView(result);
                    string searchParameter = ddlSearchbykey.Text;
                    dv.RowFilter = "" + ddlSearchbykey.Text + " like '%" + txtSearchbyword.Text + "%'";
                    if (dv.Count > 0)
                    {
                        result = dv.ToTable();
                    }

                    grdResult.DataSource = null;
                    grdResult.DataBind();

                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    UserPopup.Show();
                    txtSearchbyword.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PRNRequest:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportItem:
                    {
                        paramsText.Append(string.Empty + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PRNRequestFORREQUEST:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void dvResultUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                UserPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void dvResultUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblvalue.Text == "407")
                {
                    txtitem.Text = grdResult.SelectedRow.Cells[2].Text;

                    if (!string.IsNullOrEmpty(txtitem.Text)) _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtitem.Text);
                    if (_itemdetail != null)
                    {
                        if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                        {
                            if (_itemdetail.Mi_itm_tp == "V")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Virtual item not allowed !!!');document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                                txtitem.Text = string.Empty;
                                lbldesc.Text = string.Empty;
                                lblmodel.Text = string.Empty;
                                lblbrand.Text = string.Empty;
                                lblpart.Text = string.Empty;
                                return;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                            }
                        }
                    }

                    lbldesc.Text = grdResult.SelectedRow.Cells[1].Text;
                    lblmodel.Text = grdResult.SelectedRow.Cells[3].Text;
                    lblbrand.Text = grdResult.SelectedRow.Cells[4].Text;
                    lblpart.Text = grdResult.SelectedRow.Cells[5].Text;
                    txtqty.Focus();
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                if (lblvalue.Text == "409")
                {
                    txtreqno.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadPRNHeader();
                    LoadPRNItems();

                    string ordstatus = (string)Session["STATUS"];

                    if (ordstatus == "P")
                    {
                        foreach (GridViewRow hiderowbtn in this.grdorderdetails.Rows)
                        {
                            LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndelitem");

                            _delbutton.Enabled = true;
                            _delbutton.CssClass = "buttonUndocolor";
                            _delbutton.OnClientClick = "ConfirmDelete();";
                        }
                    }
                    else
                    {
                        foreach (GridViewRow hiderowbtn in this.grdorderdetails.Rows)
                        {
                            LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndelitem");

                            _delbutton.Enabled = false;
                            _delbutton.CssClass = "buttoncolor";
                            _delbutton.OnClientClick = "return Enable();";
                        }
                    }
                }

                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
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

        private void PopulateDropDowns()
        {
            try
            {
                DataTable _CompanyItemStatus = CacheLayer.Get<DataTable>(CacheLayer.Key.CompanyItemStatus.ToString());
                if (_CompanyItemStatus.Rows.Count > 0)
                {
                    ddlstatus.DataSource = _CompanyItemStatus;
                    ddlstatus.DataTextField = "MIS_DESC";
                    ddlstatus.DataValueField = "MIC_CD";
                    ddlstatus.DataBind();
                }
                ddlstatus.SelectedValue = "GDLP";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private bool ValidateAddEntry()
        {
            DateTime todaydate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime orddate = Convert.ToDateTime(txtRequestDate.Text.Trim());
            DateTime ordreqdate = Convert.ToDateTime(txtRequriedDate.Text.Trim());

            if (string.IsNullOrEmpty(txtRequestDate.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select date !!!')", true);
                txtRequestDate.Focus();
                return false;
            }

            if (orddate < todaydate)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Date can not be a back date !!!')", true);
                txtRequestDate.Focus();
                return false;
            }

            if (orddate > todaydate)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Date can not be a future date !!!')", true);
                txtRequestDate.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtRequriedDate.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select required date !!!')", true);
                txtRequriedDate.Focus();
                return false;
            }

            if (ordreqdate < todaydate)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Required date can not be a back date !!!')", true);
                txtRequestDate.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtrefno.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter ref # !!!')", true);
                txtrefno.Focus();
                return false;
            }

            if (grdorderdetails.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please add order items !!!')", true);
                lbtnmodelfind.Focus();
                return false;
            }
            return true;
        }

        private bool ValidateAddItemsEntry()
        {
            if (string.IsNullOrEmpty(txtitem.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an item !!!')", true);
                lbtnmodelfind.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtqty.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter qty !!!')", true);
                txtqty.Focus();
                return false;
            }

            if (!IsNumeric(txtqty.Text.Trim(), NumberStyles.Float))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid number for qty !!!')", true);
                txtqty.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtqty.Text.Trim()) == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('0 qty is not allowed !!!')", true);
                txtqty.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtqty.Text.Trim()) < 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Minus values are not allowed !!!')", true);
                txtqty.Focus();
                return false;
            }

            return true;
        }

        private string GetRequestNo()
        {
            string _reqNo = string.Empty;

            if (string.IsNullOrEmpty(txtreqno.Text))
                _reqNo = "DOC-" + DateTime.Now.TimeOfDay.Hours.ToString() + ":" + DateTime.Now.TimeOfDay.Minutes.ToString() + ":" + DateTime.Now.TimeOfDay.Seconds.ToString();
            else
                _reqNo = txtreqno.Text;

            return _reqNo;
        }


        private void SaveInventoryRequestData()
        {
            try
            {
                bool isvalid = ValidateAddEntry();
                if (isvalid == false)
                {
                    return;
                }

                InventoryRequest _inventoryRequest = new InventoryRequest();

                string reqno = string.Empty;

                if (string.IsNullOrEmpty(txtreqno.Text.Trim()))
                {
                    reqno = GetRequestNo();
                }
                else
                {
                    reqno = txtreqno.Text.Trim();
                }

                _inventoryRequest.Itr_com = Session["UserCompanyCode"].ToString();
                _inventoryRequest.Itr_req_no = reqno;
                _inventoryRequest.Itr_tp = "PRQ";
                _inventoryRequest.Itr_sub_tp = "NOR";
                //Modified By Udaya 15.08.2017
                _inventoryRequest.Itr_loc = Session["UserDefLoca"].ToString();//Session["UserDefProf"].ToString();
                _inventoryRequest.Itr_ref = txtrefno.Text.Trim();
                _inventoryRequest.Itr_dt = Convert.ToDateTime(txtRequestDate.Text);
                _inventoryRequest.Itr_exp_dt = Convert.ToDateTime(txtRequriedDate.Text);
                _inventoryRequest.Itr_stus = "P";  //P - Pending , A - Approved. 
                _inventoryRequest.Itr_job_no = string.Empty;  //Invoice No.
                _inventoryRequest.Itr_bus_code = string.Empty;  //Customer Code.
                _inventoryRequest.Itr_note = txtRemark.Text;
                _inventoryRequest.Itr_issue_from = string.Empty;
                //Modified By Udaya 15.08.2017
                _inventoryRequest.Itr_rec_to = Session["UserDefLoca"].ToString();//Session["UserDefProf"].ToString();
                _inventoryRequest.Itr_direct = 0;
                _inventoryRequest.Itr_country_cd = string.Empty;  //Country Code.
                _inventoryRequest.Itr_town_cd = string.Empty;     //Town Code.
                _inventoryRequest.Itr_cur_code = string.Empty;    //Currency Code.
                _inventoryRequest.Itr_exg_rate = 0;              //Exchange rate.
                _inventoryRequest.Itr_collector_id = string.Empty;
                _inventoryRequest.Itr_collector_name = string.Empty;
                _inventoryRequest.Itr_act = 1;
                _inventoryRequest.Itr_cre_by = Session["UserID"].ToString();
                _inventoryRequest.Itr_session_id = Session["SessionID"].ToString();
                _inventoryRequest.Itr_issue_com = string.Empty;

                DataTable dt = ViewState["PRNItemsTable"] as DataTable;

                List<InventoryRequestItem> _invReqItemList = new List<InventoryRequestItem>();

                foreach (DataRow row in dt.Rows)
                {
                    InventoryRequestItem _inventoryRequestItem = new InventoryRequestItem();
                    MasterItem _itemdetail = new MasterItem();
                    Int32 rowline = dt.Rows.IndexOf(row) + 1;

                    _itemdetail.Mi_cd = row[0].ToString();
                    _inventoryRequestItem.Itri_app_qty = Convert.ToDecimal(row[4].ToString());//row[5]
                    _inventoryRequestItem.Itri_itm_cd = row[0].ToString();
                    _inventoryRequestItem.Itri_itm_stus = ddlstatus.SelectedValue;//"GDLP";
                    _inventoryRequestItem.Itri_line_no = rowline;
                    _inventoryRequestItem.Itri_note = row[6].ToString();
                    _inventoryRequestItem.Itri_qty = Convert.ToDecimal(row[4].ToString());//row[5]
                    _inventoryRequestItem.Itri_res_no = string.Empty;
                    //_inventoryRequestItem.Itri_seq_no = 0;
                    _inventoryRequestItem.Itri_unit_price = 0;
                    _inventoryRequestItem.Itri_bqty = Convert.ToDecimal(row[4].ToString());//row[5]
                    _inventoryRequestItem.MasterItem = _itemdetail;
                    _inventoryRequestItem.Itri_issue_qty = 0; //Added by Sahan.Requested by Rukshan
                    _inventoryRequestItem.Itri_note = row[5].ToString();
                    _invReqItemList.Add(_inventoryRequestItem);
                }

                List<InventoryRequestItem> _inventoryRequestItemList = _invReqItemList;
                _inventoryRequest.InventoryRequestItemList = _inventoryRequestItemList;

                int rowsAffected = 0;
                string _docNo = string.Empty;

                if (string.IsNullOrEmpty(txtreqno.Text))
                {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, GenerateMasterAutoNumber(), out _docNo);

                    if (rowsAffected == 1)
                    {
                        string Msg = "Successfully Saved! Auto Generated Document No is " + _docNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + Msg + "')", true);
                        Session["GlbReportType"] = "";
                        Session["GlbReportName"] = "PurchaseRequest.rpt";
                        string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                        string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                        clsInventory obj = new clsInventory();
                        //Modified By Udaya 15.08.2017
                        obj.get_printdata(_docNo, Session["UserCompanyCode"].ToString(), "PRQ", Session["UserID"].ToString(), Session["UserDefLoca"].ToString());//Session["UserDefProf"].ToString()
                        PrintPDF(targetFileName, obj._PurchaseRequest);

                        string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                        Clear();
                    }
                    else
                    {
                        string msg = "Error :" + _docNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                    }
                }
                else
                {
                    rowsAffected = CHNLSVC.Inventory.SaveInventoryRequestData(_inventoryRequest, null, out _docNo);

                    if (rowsAffected == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully Saved !!!')", true);
                        Clear();
                    }
                    else
                    {
                        string msg = "Error :" + _docNo;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void Clear()
        {
            try
            {
                txtreqno.Text = string.Empty;
                txtRemark.Text = string.Empty;
                txtrefno.Text = string.Empty;
                txtitem.Text = string.Empty;
                txtitmremarks.Text = string.Empty;
                txtqty.Text = string.Empty;
                grdorderdetails.DataSource = null;
                grdorderdetails.DataBind();
                lbldesc.Text = string.Empty;
                lblmodel.Text = string.Empty;
                lblbrand.Text = string.Empty;
                lblpart.Text = string.Empty;

                ViewState["PRNItemsTable"] = null;

                DataTable dtitems = new DataTable();
                dtitems.Columns.AddRange(new DataColumn[7] { new DataColumn("Item"), new DataColumn("Description"), new DataColumn("Brand"), new DataColumn("Model"), new DataColumn("Status"), new DataColumn("Qty"), new DataColumn("Remarks") });
                ViewState["PRNItemsTable"] = dtitems;
                this.BindGrid();

                DateTime orddate = DateTime.Now;
                txtRequestDate.Text = orddate.ToString("dd/MMM/yyyy");

                DateTime orddate1 = DateTime.Now;
                txtRequriedDate.Text = orddate1.ToString("dd/MMM/yyyy");
                SessionClear();

                foreach (GridViewRow hiderowbtn in this.grdorderdetails.Rows)
                {
                    LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndelitem");

                    _delbutton.Enabled = false;
                    _delbutton.CssClass = "buttonUndocolor";
                    _delbutton.OnClientClick = "ConfirmDelete();";
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private MasterAutoNumber GenerateMasterAutoNumber()
        {
            MasterAutoNumber masterAuto;

            masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_tp = "PC";
            //Modified By Udaya 15.08.2017
            masterAuto.Aut_cate_cd = Session["UserDefLoca"].ToString();//Session["UserDefProf"].ToString(); 
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "PRQ";
            masterAuto.Aut_number = 0;
            masterAuto.Aut_start_char = "PRQ";
            masterAuto.Aut_year = DateTime.Now.Year;
            //RNGD-PRQ-15-00001 Sample format of Auto Number
            return masterAuto;
        }

        protected void lbtnsave_Click(object sender, EventArgs e)
        {
            if (txtconfirmplaceord.Value == "Yes")
            {
                try
                {
                    SaveInventoryRequestData();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

        protected void lbtnpcsearch_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "400";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnmodelfind_Click(object sender, EventArgs e)
        {
            try
            {
                //ViewState["SEARCH"] = null;
                //txtSearchbyword.Text = string.Empty;
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                //DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, null, null);
                //grdResult.DataSource = result;
                //grdResult.DataBind();
                //lblvalue.Text = "401";
                //BindUCtrlDDLData(result);
                //ViewState["SEARCH"] = result;
                //UserPopup.Show();
                //txtSearchbyword.Focus();

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportItem);
                DataTable result = CHNLSVC.CommonSearch.SearchItemforchange(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "407";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnadditems_Click(object sender, EventArgs e)
        {
            try
            {
                string ordstatus = (string)Session["STATUS"];
                if (ordstatus == "A")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allowed to add new items for an already approved request !!!')", true);
                    return;
                }

                DataTable dtcheck = CheckUseInPO();

                if (dtcheck.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allowed to add new items for a request which is already used for a purchase order !!!')", true);
                    return;
                }

                InsertToGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private DataTable CheckUseInPO()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = CHNLSVC.Inventory.CheckReqUseInPO(txtreqno.Text.Trim());
                return dt;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                return dt;
            }
            finally
            {
                CHNLSVC.CloseChannel();
            }
        }
        protected void InsertToGrid()
        {
            try
            {
                bool isvaliditems = ValidateAddItemsEntry();
                if (isvaliditems == false)
                {
                    return;
                }
                DataTable dt = (DataTable)ViewState["PRNItemsTable"];
                if(dt.Rows.Count <= 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("Item", typeof(System.String));
                    dt.Columns.Add("Description", typeof(System.String));
                    dt.Columns.Add("Brand", typeof(System.String));
                    dt.Columns.Add("Model", typeof(System.String));
                    dt.Columns.Add("Qty", typeof(System.Decimal));
                    dt.Columns.Add("Remarks", typeof(System.String));
                    dt.Columns.Add("Line", typeof(System.Int32));
                    dt.Columns.Add("B.Qty", typeof(System.Decimal));
                    dt.Columns.Add("Po.Qty", typeof(System.Decimal));
                    dt.Columns.Add("Status", typeof(System.String));
                    dt.Columns.Add("ItemStatus", typeof(System.String));
                }

                //if (dt.Rows.Count <= 0)
                //{
                //    foreach (DataColumn dc in dt.Columns)
                //    {
                //        if (dc.ColumnName == "Status")
                //        {
                //            dt.Columns["Status"].ColumnName = "ItemStatus";
                //        }
                //    }
                //}
                //if (!chkRecall)
                //{
                //    dt.Rows.Add(txtitem.Text.Trim(), lbldesc.Text.Trim(), lblbrand.Text.Trim(), lblmodel.Text.Trim(), ddlstatus.SelectedItem.Text, txtqty.Text.Trim(), txtitmremarks.Text.Trim());
                //}
                dt.Rows.Add(txtitem.Text.Trim(), lbldesc.Text.Trim(), lblbrand.Text.Trim(), lblmodel.Text.Trim(), txtqty.Text.Trim(), txtitmremarks.Text.Trim(), dt.Rows.Count + 1, txtqty.Text.Trim(), 0, ddlstatus.SelectedValue, ddlstatus.SelectedValue);
                
                ViewState["PRNItemsTable"] = dt;
                uniqueCols = RemoveDuplicateRows(dt, "Item");

                grdorderdetails.DataSource = uniqueCols;
                grdorderdetails.DataBind();

                txtitem.Text = string.Empty;
                txtqty.Text = string.Empty;
                txtitmremarks.Text = string.Empty;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName1)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if ((hTable.Contains(drow[colName1])))
                {
                    duplicateList.Add(drow);
                }
                else
                {
                    hTable.Add(drow[colName1], string.Empty);
                }
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
            {
                dTable.Rows.Remove(dRow);
                string msg = "Item " + txtitem.Text.Trim() + " is already in item list !!!";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msg + "')", true);
            }
            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        protected void grdorderdetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string item = e.Row.Cells[0].Text;
                    foreach (LinkButton button in e.Row.Cells[8].Controls.OfType<LinkButton>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            button.Attributes["onclick"] = "if(!confirm('Do you want to delete item " + item + "?')){ return false; };";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void grdorderdetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdorderdetails.EditIndex = e.NewEditIndex;
                this.BindGrid();

                foreach (GridViewRow hiderowbtn in this.grdorderdetails.Rows)
                {
                    LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndelitem");

                    _delbutton.Enabled = false;
                    _delbutton.CssClass = "buttoncolor";
                    _delbutton.OnClientClick = "return Enable();";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            try
            {
                grdorderdetails.EditIndex = -1;
                this.BindGrid();
                foreach (GridViewRow hiderowbtn in this.grdorderdetails.Rows)
                {
                    LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndelitem");

                    _delbutton.Enabled = false;
                    _delbutton.CssClass = "buttoncolor";
                    _delbutton.OnClientClick = "return Enable();";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                DataTable dt = ViewState["PRNItemsTable"] as DataTable;
                dt.Rows[index].Delete();
                dt.AcceptChanges();
                ViewState["PRNItemsTable"] = dt;
                BindGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void OnUpdate(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;

                string Qty = (row.Cells[5].Controls[0] as TextBox).Text;
                string Remarks = (row.Cells[6].Controls[0] as TextBox).Text;
                
                if (string.IsNullOrEmpty(Qty))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a quantity !!!')", true);
                    return;
                }

                if (string.IsNullOrEmpty(Remarks))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter a remark !!!')", true);
                    return;
                }
                
                if (!IsNumeric(Qty, NumberStyles.Float))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid number for qty !!!')", true);
                    return;
                }

                if (Convert.ToDecimal(Qty) == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('0 qty is not allowed !!!')", true);
                    return;
                }

                if (Convert.ToDecimal(Qty) < 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Minus values are not allowed !!!')", true);
                    return;
                }

                if (Qty.Length > 8)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Length of qty should be 8 numbers !!!')", true);
                    return;
                }

                if (Remarks.Length > 200)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Length of remarks should be 200 characters !!!')", true);
                    return;
                }

                DataTable dt = ViewState["PRNItemsTable"] as DataTable;

                dt.Rows[row.RowIndex]["Qty"] = Qty;
                dt.Rows[row.RowIndex]["Remarks"] = Remarks;

                ViewState["PRNItemsTable"] = dt;
                grdorderdetails.EditIndex = -1;
                this.BindGrid();

                foreach (GridViewRow hiderowbtn in this.grdorderdetails.Rows)
                {
                    LinkButton _delbutton = (LinkButton)hiderowbtn.FindControl("lbtndelitem");

                    _delbutton.Enabled = false;
                    _delbutton.CssClass = "buttoncolor";
                    _delbutton.OnClientClick = "return Enable();";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnreqnosearch_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;

                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PRNRequest);//PRNRequestFORREQUEST
                DataTable result = CHNLSVC.CommonSearch.SearchPRNREQNoforall(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "409";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            } 
        }

        private void LoadPRNHeader()
        {
            try
            {
                DataTable dtHeaders = CHNLSVC.CommonSearch.SearchPRNHeader(txtreqno.Text.Trim());
                if (dtHeaders.Rows.Count > 0)
                {
         
                    foreach (DataRow item in dtHeaders.Rows)
                    {
                        string stus = dtHeaders.Rows[0]["ITR_STUS"].ToString();
                        if (stus == "A")
                        {
                            //lbtnsave.Visible = false;
                            lbtnsave.Enabled = false;
                            lbtnsave.OnClientClick = "";
                            lbtnsave.CssClass = "buttoncolor";
                            lbtnClear.Enabled = false;
                            lbtnClear.OnClientClick = "";
                            lbtnClear.CssClass = "buttoncolor";
                            lbtnapprove.Enabled = false;
                            lbtnapprove.OnClientClick = "";
                            lbtnapprove.CssClass = "buttoncolor";
                        }
                        else if (stus == "P")
                        {
                            lbtnapprove.Enabled = true;
                            lbtnapprove.OnClientClick = "ConfirmApprove();";
                            lbtnapprove.CssClass = "buttonUndocolor";
                        }
                        
                            Session["PRNSEQNO"] = item[0].ToString();
                    
                            DateTime oreddate = Convert.ToDateTime(item[7].ToString());
                            string oreddatetext = oreddate.ToString("dd/MMM/yyyy");
                            txtRequestDate.Text = oreddatetext;

                            DateTime reqdate = Convert.ToDateTime(item[8].ToString());
                            string reqdatetext = reqdate.ToString("dd/MMM/yyyy");
                            txtRequriedDate.Text = reqdatetext;
                     
                            txtrefno.Text = item[6].ToString();
                            txtRemark.Text = item[12].ToString();
                            Session["STATUS"] = item[9].ToString();
                        
                    
                   }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        private void LoadPRNItems()
        {
            try
            {
                string seqenceno = (string)Session["PRNSEQNO"];

                DataTable dtitems = CHNLSVC.CommonSearch.SearchPRNItemsfordesc(Convert.ToInt32(seqenceno));
                Session["ITEMCOUNT"] = dtitems.Rows.Count;
                foreach(var data1 in dtitems.Rows)
                {
                    string stus = dtitems.Rows[0]["Status"].ToString();//Before exist stus but sp in Status -- Modified By Udaya 17.08.2017
                if(stus=="A")
                {
                    grdorderdetails.Enabled = false;
                }
                }
               
                if (dtitems.Rows.Count > 0)
                {
                    grdorderdetails.DataSource = null;
                    grdorderdetails.DataBind();

                    grdorderdetails.DataSource = dtitems;
                    grdorderdetails.DataBind();
                }

                ViewState["PRNItemsTable"] = null;
                this.BindGrid();

                ViewState["PRNItemsTable"] = dtitems;
                this.BindGrid();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                try
                {
                    Clear();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

        protected void lbtnapprove_Click(object sender, EventArgs e)
        {
            if (txtapprove.Value == "Yes")
            {
                try
                {
                    string ordstatus = (string)Session["STATUS"];
                    Int32 recordcount = (Int32)Session["ITEMCOUNT"];

                    if (grdorderdetails.Rows.Count > recordcount)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please save the request before approve !!!')", true);
                        txtreqno.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtreqno.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an order !!!')", true);
                        txtreqno.Focus();
                        return;
                    }

                    if (ordstatus == "A")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This order is already approved !!!')", true);
                        return;
                    }

                    if (ordstatus == "C")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This order is already cancelled !!!')", true);
                        return;
                    }

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16005))
                    {
                        string msg = "Sorry, You have no permission to approve this order.( Advice: Required permission code : 16005) !!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg +"')", true);
                        return;
                    }
                    _userid = (string)Session["UserID"];

                    IntReq PRNHeader = new IntReq();

                    PRNHeader.ITR_REQ_NO = txtreqno.Text.Trim();
                    PRNHeader.ITR_STUS = "A";
                    PRNHeader.ITR_MOD_BY= _userid;
                    PRNHeader.ITR_MOD_DT = CHNLSVC.Security.GetServerDateTime();
                    PRNHeader.ITR_APP_BY = _userid;
                    PRNHeader.ITR_APP_DT = DateTime.Now;
                    Int32 outputresult = CHNLSVC.Financial.UpdatePRNStatus(PRNHeader);

                    string seqenceno = (string)Session["PRNSEQNO"];
                    Int32 apresult = 0;

                    foreach (GridViewRow row in grdorderdetails.Rows)
                    {
                        //MOdified By Udaya 17.08.2017 directly status can get

                        //DataTable dtstatus = CHNLSVC.Sales.GetItemStatusVal(row.Cells[4].Text);
                        //string _itemval = string.Empty;
                        //foreach (DataRow itemval in dtstatus.Rows)
                        //{
                        //    _itemval = itemval["MIS_CD"].ToString();
                        //}

                        InventoryRequestItem myitem = new InventoryRequestItem();
                        myitem.Itri_app_qty = Convert.ToDecimal(row.Cells[5].Text);
                        myitem.Itri_bqty = Convert.ToDecimal(row.Cells[5].Text);
                        myitem.Itri_seq_no = Convert.ToInt32(seqenceno);
                        myitem.Itri_itm_cd = row.Cells[0].Text;
                        myitem.Itri_itm_stus = row.Cells[4].Text; //_itemval;

                        apresult = CHNLSVC.Inventory.UpdatePRNQty(myitem); 
                    }

                    if ((outputresult == 1) && (apresult == 1))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully approved !!!')", true);
                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            if (txtcancel.Value == "Yes")
            {
                try
                {
                    string ordstatus = (string)Session["STATUS"];
                    if (string.IsNullOrEmpty(txtreqno.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an order !!!')", true);
                        txtreqno.Focus();
                        return;
                    }

                    if (ordstatus == "C")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This order is already cancelled !!!')", true);
                        return;
                    }

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16006))
                    {
                        string msg = "Sorry, You have no permission to cancel this order.( Advice: Required permission code : 16006) !!!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
                        return;
                    }

                    DataTable dtcheck = CheckUseInPO();

                    if (dtcheck.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('You are not allowed to cancel a request which is already used to perform a purchase order !!!')", true);
                        return;
                    }

                    _userid = (string)Session["UserID"];

                    IntReq PRNHeader = new IntReq();

                    PRNHeader.ITR_REQ_NO = txtreqno.Text.Trim();
                    PRNHeader.ITR_STUS = "C";
                    PRNHeader.ITR_MOD_BY = _userid;
                    PRNHeader.ITR_MOD_DT = CHNLSVC.Security.GetServerDateTime();

                    Int32 outputresult = CHNLSVC.Financial.UpdatePRNStatus(PRNHeader);

                    if (outputresult == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully cancelled !!!')", true);
                        Clear();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                }
            }
        }

        private void SessionClear()
        {
            try
            {
                Session["PRNSEQNO"] = null;
                Session["STATUS"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }
        protected void txtitem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtitem.Text))
                {
                    txtitem.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Enter Item Code !!!')", true);
                    return;
                }

                if (!string.IsNullOrEmpty(txtitem.Text)) _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtitem.Text);
                 if (_itemdetail != null)
                 {
                     if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                     {
                         if (_itemdetail.Mi_itm_tp == "V")
                         {
                             ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Virtual item not allowed !!!');", true);
                             txtitem.Text = string.Empty;
                             lbldesc.Text = string.Empty;
                             lblmodel.Text = string.Empty;
                             lblbrand.Text = string.Empty;
                             lblpart.Text = string.Empty;
                             return;
                         } 
                         else
                         {
                             lbldesc.Text = _itemdetail.Mi_shortdesc;  
                         }
                     }
                 }
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel);
                //DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, "ITEM", txtitem.Text.Trim());

                DataTable result = CHNLSVC.Financial.GetItemDetails(txtitem.Text);

                if (result.Rows.Count == 0)
                {
                    txtitem.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid item !!!')", true);
                    lbldesc.Text = string.Empty;
                    lblmodel.Text = string.Empty;
                    lblbrand.Text = string.Empty;
                    lblpart.Text = string.Empty;
                    return;
                }

                foreach (DataRow DDritem in result.Rows)
                {
                    txtitem.Text = DDritem[0].ToString();
                    lbldesc.Text = DDritem[1].ToString();
                    lblmodel.Text = DDritem[5].ToString();
                    lblbrand.Text = DDritem[4].ToString();
                    lblpart.Text = DDritem[6].ToString();
                }
                txtqty.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
        }

        protected void lbtnprint_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GlbReportType"] = "";
                Session["Type"] = "PRQ";
                Session["RefDoc"] = txtreqno.Text.Trim();
                if (txtreqno.Text.Trim() == null | txtreqno.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Please Select Request')", true);
                    return;
                }
                else
                {
                    //PopupConfBox.Hide();
                    Session["GlbReportType"] = "";
                    Session["GlbReportName"] = "PurchaseRequest.rpt";
                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsInventory obj = new clsInventory();
                    //Modified By Udaya 15.08.2017
                    obj.get_printdata(txtreqno.Text.Trim(), Session["UserCompanyCode"].ToString(), "PRQ", Session["UserID"].ToString(), Session["UserDefLoca"].ToString());//Session["UserDefProf"].ToString()
                    PrintPDF(targetFileName, obj._PurchaseRequest);

                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    //PopupConfBox.Hide();

                    //Response.Redirect("~/View/Reports/Inventory/InventoryReportViewer.aspx", false);
                }
            }
            catch(Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Purchase Request Note Print", "PurchaseRequestNote", ex.Message, Session["UserID"].ToString());
            }
        }



        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                ReportDocument rptDoc = (ReportDocument)_rpt;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                diskOpts.DiskFileName = targetFileName;
                rptDoc.ExportOptions.DestinationOptions = diskOpts;
                rptDoc.Export();

                rptDoc.Close();
                rptDoc.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtqty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                MasterItem _mstItm = CHNLSVC.Inventory.GetItem("", txtitem.Text.Trim().ToUpper());
                if (_mstItm!=null)
                {
                    if (_mstItm.Mi_is_ser1!=-1)
                    {
                        decimal _qty = 0, _tmpqty = 0;
                        _qty = decimal.TryParse(txtqty.Text, out _tmpqty) ? Convert.ToDecimal(txtqty.Text) : 0;

                        if ((_qty % 1 )>0)
                        {
                            DispMsg("Invalid quentity !","W");
                            txtqty.Text = "";
                            txtqty.Focus();
                        }
                    }
                }
                else
                {
                    DispMsg("Invalid item code !","W");
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }
        private void DispMsg(string msgText, string msgType)
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
            }
            else if (msgType == "S")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msgText + "');", true);
            }
            else if (msgType == "E")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + msgText + "');", true);
            }
            else if (msgType == "A")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msgText + "');", true);
            }
        }
    }
}