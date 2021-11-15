using FastForward.SCMWeb.Services;
using FF.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Inventory
{
    public partial class AODDocumentAmendment : Base
    {
        private List<InventoryRequestItem> SaveItemList = null;
        private List<ReptPickSerials> saveSerialList = null;
        string _aodIssueLoc = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ValidateTrue();
                if (!IsPostBack)
                {
                    PageClear();
                    bool _allowCurrentTrans = false;
                    IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, Session["GlbModuleName"].ToString(), txtDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
                }
                else
                {
                    if (Session["1"] == "true")
                    {
                        UserPopup.Show();
                        Session["1"] = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...')", true);
                return;
            }
        }   
        protected void txtIncorrectLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtIncorrectLocation.Text))
                {
                    MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), txtIncorrectLocation.Text.ToString());
                    if (_masterLocation != null)
                    {
                        txtIncorrectLocation.ToolTip = _masterLocation.Ml_loc_desc;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid location code.')", true);
                        txtIncorrectLocation.Text = string.Empty;
                        txtIncorrectLocation.ToolTip = string.Empty;
                        txtIncorrectLocation.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...')", true);
                return;
            }
        }
        protected void lbtnIncorrectLocation_Click(object sender, EventArgs e)
        {
            try
            {
                DateRow.Visible = false;
                txtFromDateSearch.Text = string.Empty;
                txtToDateSearch.Text = string.Empty;

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "1I";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error Occurred while processing...')", true);
                return;
            }
        }
        protected void txtDocumentNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                btnSearch_Click(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error Occurred while processing...')", true);
                return;
            }            
        }
        protected void lbtnDocumentNo_Click(object sender, EventArgs e)
        {
            try
            {
                DateRow.Visible = true;
                txtFromDateSearch.Text = (DateTime.Now.AddMonths(-3)).Date.ToString("dd/MMM/yyyy");
                txtToDateSearch.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                var sortedTable = new DataTable();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                //DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                string SearchParams;
                DataTable result;
                if (string.IsNullOrEmpty(txtIncorrectLocation.Text))
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GitDocDateSearch);
                    result = CHNLSVC.CommonSearch.Search_GIT_AODs(SearchParams, null, null, Convert.ToDateTime(txtFromDateSearch.Text).Date, Convert.ToDateTime(txtToDateSearch.Text).Date);
                    lblvalue.Text = "135";

                  
                }
                else
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GitDocWithLocDateSearch);
                    result = CHNLSVC.CommonSearch.Search_GIT_AODs_WithLoc(SearchParams, null, null, Convert.ToDateTime(txtFromDateSearch.Text).Date, Convert.ToDateTime(txtToDateSearch.Text).Date);
                    lblvalue.Text = "136";
                    
                }

                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error Occurred while processing...')", true);
                return;
            }
        }
        protected void txtCorrectLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCorrectLocation.Text))
                {
                    MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), txtCorrectLocation.Text.ToString());
                    if (_masterLocation != null)
                    {
                        txtCorrectLocation.ToolTip = _masterLocation.Ml_loc_desc;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid location for the correct location.')", true);
                        txtCorrectLocation.Text = string.Empty;
                        txtCorrectLocation.ToolTip = string.Empty;
                        txtCorrectLocation.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error Occurred while processing...')", true);
                return;
            }
        }
        protected void lbtnCorrectLocation_Click(object sender, EventArgs e)
        {
            try
            {
                DateRow.Visible = false;
                txtFromDateSearch.Text = string.Empty;
                txtToDateSearch.Text = string.Empty;

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "1C";
                DateRow.Visible = false;
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error Occurred while processing...')", true);
                return;
            }
        }        
        
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date;
                if (!DateTime.TryParse(txtDate.Text, out date))
                {
                    //lblWarning.Text = "Please select valid Date.";
                    //divWarning.Visible = true;
                    //return;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select valid Date.')", true);
                    return;
                }

                if (ddlType.SelectedValue == "0")
                {
                    //lblWarning.Text = "Please select amendment type.";
                    //divWarning.Visible = true;
                    //return;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select amendment type.')", true);
                    return;
                }

              

                if (CheckServerDateTime() == false) return;

                if (string.IsNullOrEmpty(txtIncorrectLocation.Text) || string.IsNullOrEmpty(txtDocumentNo.Text) || grdItems.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the valid AOD document.')", true);
                    txtDocumentNo.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(txtDocumentNo.Text) && grdItems.Rows.Count <= 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item(s) not available in the AOD issue # : " + txtDocumentNo.Text.Trim() + "')", true);
                    txtDocumentNo.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCorrectLocation.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the correct location.')", true);
                    txtCorrectLocation.Focus();
                    return;
                }
                if (ddlType.SelectedItem.Value == "0")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showWarningToast('Please select Type.')", true);
                    ddlType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtManualRef.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the manual reference.')", true);
                    txtManualRef.Focus();
                    return;
                }

                if (txtIncorrectLocation.Text.Trim() == txtCorrectLocation.Text.Trim())
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Incorrect location and correct location cannot be same.')", true);
                    txtCorrectLocation.Focus();
                    return;
                }

                bool _allowCurrentTrans = false;
                if (IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, "AOD Correction", txtDate, lblBackDateInfor, Convert.ToDateTime(txtDate.Text).ToShortDateString(), out _allowCurrentTrans) == false)
                 //   IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), string.Empty, Session["UserDefProf"].ToString(), Session["GlbModuleName"].ToString(), txtSRNDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (Convert.ToDateTime(txtDate.Text).Date != DateTime.Now.Date)
                        {
                            txtDate.Enabled = true;
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date is not allowed for transaction');", true);
                            // MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtDate.Focus();
                            return;                            
                        }
                    }
                    else
                    {
                        txtDate.Enabled = true;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Selected date is not allowed for transaction');", true);
                        //MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDate.Focus();
                    }
                }

                if (hdnSave.Value == "Yes")
                {
                    string _inwardNo = string.Empty;
                    string _outwardNo = string.Empty;
                    _aodIssueLoc = txtOriginalIssueLoc.Text.Trim();


                    bool _checkaod = false;
                    string _loc =string.Empty;
                   _checkaod =CHNLSVC.Inventory.CheckAOD_AlreadyIn(txtDocumentNo.Text.ToString(),Session["UserCompanyCode"].ToString(),out  _loc);
                    if(_checkaod==true)
                    {
                        string msg = "Already received AOD to location -" + _loc;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('"+msg+"')", true);
                        return;
                    }

                    int result = CHNLSVC.Inventory.AODCorrection(Session["UserCompanyCode"].ToString(), txtDocumentNo.Text.ToString(), Convert.ToDateTime(txtDate.Text).Date, _aodIssueLoc, txtIncorrectLocation.Text.ToString(), txtCorrectLocation.Text.ToString(), txtManualRef.Text.ToString(), txtOtherRef.Text.ToString(), txtRemarks.Text.ToString(), Session["UserID"].ToString(), Session["SessionID"].ToString(), out _inwardNo, out _outwardNo);
                    if (result != -99 && result >= 0)
                    {
                        //if (MessageBox.Show("Successfully Saved! \ndocument no (+AOD) : " + _inwardNo + " and \ndocument no (-AOD) : " + _outwardNo + "\nDo you want to print this?", "AOD Correction", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        //{
                        //    BaseCls.GlbReportTp = "INWARD";
                        //    Reports.Inventory.ReportViewerInventory _viewPlus = new Reports.Inventory.ReportViewerInventory();
                        //    if (Session["UserCompanyCode"].ToString() == "SGL") //Sanjeewa 2014-01-07
                        //        _viewPlus.GlbReportName = "Inward_Docs.rpt";
                        //    else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                        //        _viewPlus.GlbReportName = "Dealer_Inward_Docs.rpt";
                        //    else _viewPlus.GlbReportName = "Inward_Docs.rpt";
                        //    _viewPlus.GlbReportDoc = _inwardNo;
                        //    _viewPlus.Show();
                        //    _viewPlus = null;

                        //    BaseCls.GlbReportTp = "OUTWARD";
                        //    Reports.Inventory.ReportViewerInventory _viewMinus = new Reports.Inventory.ReportViewerInventory();
                        //    if (Session["UserCompanyCode"].ToString() == "SGL") //Sanjeewa 2014-01-07
                        //        _viewMinus.GlbReportName = "Outward_Docs.rpt";
                        //    else if (BaseCls.GlbDefChannel == "AUTO_DEL") //Sanjeewa 2014-03-06
                        //        _viewMinus.GlbReportName = "Dealer_Outward_Docs.rpt";
                        //    else _viewMinus.GlbReportName = "Outward_Docs.rpt";
                        //    _viewMinus.GlbReportDoc = _outwardNo;
                        //    _viewMinus.Show();
                        //    _viewMinus = null;
                        //}

                        PageClear();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully Saved!. Document no (+AOD) : " + _inwardNo + " and document no (-AOD) : " + _outwardNo + " ')", true);
                        return;  
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Process Terminated.')", true);
                        return;  
                    }                 
                   
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }                
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                if (hdnClear.Value == "Yes")
                {
                    PageClear();
                }
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
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
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                //IncorrectLocation
                if (lblvalue.Text == "1I")
                {
                    txtIncorrectLocation.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtIncorrectLocation_TextChanged(null, null);
                }
                //CorrectLocation
                if (lblvalue.Text == "1C")
                {
                    txtCorrectLocation.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtCorrectLocation_TextChanged(null, null);
                }
                //GitDocDateSearch
                if (lblvalue.Text == "135")
                {
                    txtDocumentNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtDocumentNo_TextChanged(null, null);
                }
                //GitDocWithLocDateSearch
                if (lblvalue.Text == "136")
                {
                    txtDocumentNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtDocumentNo_TextChanged(null, null);
                }
                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error Occurred while processing...')", true);
                return;
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
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                lblWarning.Text = "Error Occurred while processing...  " + ex;
                divWarning.Visible = true;
            }
        }
        private void FilterData()
        {
            //IncorrectLocation
            if (lblvalue.Text == "1I")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "1I";
                Session["1"] = "true";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //CorrectLocation
            if (lblvalue.Text == "1C")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "1C";
                Session["1"] = "true";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //GitDocDateSearch
            if (lblvalue.Text == "135")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GitDocDateSearch);
                //DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                DataTable result = CHNLSVC.CommonSearch.Search_GIT_AODs(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, Convert.ToDateTime(txtFromDateSearch.Text).Date, Convert.ToDateTime(txtToDateSearch.Text).Date);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "135";
                Session["1"] = "true";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //GitDocWithLocDateSearch
            if (lblvalue.Text == "136")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GitDocWithLocDateSearch);
                //DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                DataTable result = CHNLSVC.CommonSearch.Search_GIT_AODs_WithLoc(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, Convert.ToDateTime(txtFromDateSearch.Text).Date, Convert.ToDateTime(txtToDateSearch.Text).Date);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "136";
                Session["1"] = "true";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GitDocDateSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "AOD" + seperator + "0" + seperator + "A" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GitDocWithLocDateSearch:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "AOD" + seperator + "0" + seperator + txtIncorrectLocation.Text.ToString() + seperator + "A" + seperator);
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error Occurred while processing...')", true);
                return;
            }
        }
        private void PopulateRequestData()
        {
            //if (string.IsNullOrEmpty(txtRequestNo.Text))
            //{
            //    txtRequestNo.Text = string.Empty;
            //    lblWarning.Text = "Please Enter Request No.";
            //    divWarning.Visible = true;
            //    return;
            //}

            //INT_REQ int_req = new INT_REQ();
            //int_req = CHNLSVC.Sales.GetRequest(txtRequestNo.Text);

            lbtnSave.Enabled = false;
            lbtnSave.OnClientClick = "return Enable();";
            lbtnSave.CssClass = "buttoncolor";

            //lbtnUpdate.Enabled = false;
            //lbtnUpdate.OnClientClick = "return Enable();";
            //lbtnUpdate.CssClass = "buttoncolor";
            //lbtnCancel.Enabled = false;
            //lbtnCancel.OnClientClick = "return Enable();";
            //lbtnCancel.CssClass = "buttoncolor";

            //if (int_req_itm.Sum(x => x.ITRI_APP_QTY) == 0 && int_req.ITR_STUS == "A")
            //{
            //    lbtnUpdate.Enabled = true;
            //    lbtnUpdate.OnClientClick = "ConfirmUpdate();";
            //    lbtnUpdate.CssClass = "buttonUndocolor";

            //    lbtnCancel.Enabled = true;
            //    lbtnCancel.OnClientClick = "ConfirmCancel();";
            //    lbtnCancel.CssClass = "buttonUndocolor";
            //}            

        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                bool _invalidDoc = true;
                int _lineNo = 0;

                _aodIssueLoc = string.Empty;

                InventoryHeader _invHdr = new InventoryHeader();

                _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocumentNo.Text);
                #region Check Valid Document No
                if (_invHdr == null)
                {
                    _invalidDoc = false;
                    goto err;
                }
                if (_invHdr.Ith_doc_tp != "AOD")
                {
                    _invalidDoc = false;
                    goto err;
                }
                if (_invHdr.Ith_direct == true)
                {
                    _invalidDoc = false;
                    goto err;
                }

                //TODO
                //DataTable dtSCM = CHNLSVC.Inventory.CheckIsAodReceived(txtDocumentNo.Text.ToString());
                //if (dtSCM != null)
                //{
                //    if (dtSCM.Rows.Count > 0)
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('AOD Issue document already received.')", true);
                //        txtDocumentNo.Text = string.Empty;
                //        txtDocumentNo.Focus();
                //        return;
                //    }
                //}

            err:
                if (_invalidDoc == false)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid AOD issue number.')", true);
                    txtDocumentNo.Text = string.Empty;
                    txtDocumentNo.Focus();
                    return;
                }
                else
                {
                    _aodIssueLoc = _invHdr.Ith_loc;
                    txtIncorrectLocation.Text = _invHdr.Ith_oth_loc;
                    txtOriginalIssueLoc.Text = _invHdr.Ith_loc;
                    MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), txtIncorrectLocation.Text.ToString());
                    if (_masterLocation != null)
                    {
                        txtIncorrectLocation.ToolTip = _masterLocation.Ml_loc_desc;
                    }
                    SaveItemList = new List<InventoryRequestItem>();
                    saveSerialList = new List<ReptPickSerials>();
                }
                #endregion

                #region Get Serials
                SaveItemList = new List<InventoryRequestItem>();
                saveSerialList = new List<ReptPickSerials>();
                saveSerialList = CHNLSVC.Inventory.Get_Int_Ser(txtDocumentNo.Text);
                if (saveSerialList != null)
                {
                    var _scanItems = saveSerialList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var itm in _scanItems)
                    {
                        _lineNo += 1;
                        InventoryRequestItem _itm = new InventoryRequestItem();
                        _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                        _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                        _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                        _itm.Itri_line_no = _lineNo;
                        _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                        _itm.Mi_longdesc = itm.Peo.Tus_itm_desc;
                        _itm.Mi_model = itm.Peo.Tus_itm_model;
                        _itm.Mi_brand = itm.Peo.Tus_itm_brand;
                        _itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                        SaveItemList.Add(_itm);
                    }

                    grdItems.AutoGenerateColumns = false;
                    grdItems.DataSource = SaveItemList;
                    grdItems.DataBind();

                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = saveSerialList;
                    grdSerial.DataBind();

                    txtIncorrectLocation.ReadOnly = true;
                    lbtnIncorrectLocation.Enabled = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item not found.')", true);
                    txtDocumentNo.Text = string.Empty;
                    txtDocumentNo.Focus();
                    return;                    
                }

                #endregion
            }
            catch (Exception err)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Error Occurred while processing...')", true);
                return;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        public Boolean GetDaleteVisibility()
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
            txtDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");

            txtIncorrectLocation.Text = string.Empty;
            txtIncorrectLocation.ReadOnly = false;
            lbtnIncorrectLocation.Enabled = true;

            txtDocumentNo.Text = string.Empty;
            txtCorrectLocation.Text = string.Empty;
            txtOriginalIssueLoc.Text = string.Empty;

            ddlType.SelectedIndex = 0;

            txtManualRef.Text = string.Empty;
            txtOtherRef.Text = string.Empty;
            txtRemarks.Text = string.Empty;

            grdItems.DataSource = new int[] { };
            grdItems.DataBind();
            grdSerial.DataSource = new int[] { };
            grdSerial.DataBind();

            lbtnSave.Enabled = true;
            lbtnSave.OnClientClick = "ConfirmSave();";
            lbtnSave.CssClass = "buttonUndocolor";
        }

    }
}