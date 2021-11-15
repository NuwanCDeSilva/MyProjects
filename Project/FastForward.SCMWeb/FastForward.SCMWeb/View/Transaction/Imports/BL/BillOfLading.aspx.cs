using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Imports;
using FF.BusinessObjects;
using FF.BusinessObjects.Financial;
using FF.BusinessObjects.InventoryNew;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Imports.BL
{
    public partial class BillOfLading : Base
    {
        #region add by lakshan 10Dec2017
        string _selectedPiNo
        {
            get { if (Session["_selectedPiNoBL"] != null) { return (string)Session["_selectedPiNoBL"]; } else { return ""; } }
            set { Session["_selectedPiNoBL"] = value; }
        }
        Int32 _selectedPiLine
        {
            get { if (Session["_selectedPiLineBL"] != null) { return (Int32)Session["_selectedPiLineBL"]; } else { return 0; } }
            set { Session["_selectedPiLineBL"] = value; }
        }
       
        bool _isExcUpload
        {
            get { if (Session["_isExcUploadBL"] != null) { return (bool)Session["_isExcUploadBL"]; } else { return false; } }
            set { Session["_isExcUploadBL"] = value; }
        }
        bool _bindGrid
        {
            get { if (Session["_bindGridBL"] != null) { return (bool)Session["_bindGridBL"]; } else { return false; } }
            set { Session["_bindGridBL"] = value; }
        }
        bool _showItmCdChg
        {
            get { if (Session["showItmCdChgBl"] != null) { return (bool)Session["showItmCdChgBl"]; } else { return false; } }
            set { Session["showItmCdChgBl"] = value; }
        }
        List<ImportsBLItems> _blItmsUpItmList
        {
            get { if (Session["_blItmsUpItmList"] != null) { return (List<ImportsBLItems>)Session["_blItmsUpItmList"]; } else { return new List<ImportsBLItems>(); } }
            set { Session["_blItmsUpItmList"] = value; }
        }
        bool _showExcelPop
        {
            get { if (Session["_showExcelPopBL"] != null) { return (bool)Session["_showExcelPopBL"]; } else { return false; } }
            set { Session["_showExcelPopBL"] = value; }
        }
        bool _showErrPop
        {
            get { if (Session["_showErrPopBL"] != null) { return (bool)Session["_showErrPopBL"]; } else { return false; } }
            set { Session["_showErrPopBL"] = value; }
        }
        string _filPath
        {
            get { if (Session["_filPathBL"] != null) { return (string)Session["_filPathBL"]; } else { return ""; } }
            set { Session["_filPathBL"] = value; }
        }
        #endregion
        private bool _isDel
        {
            get
            {
                if (Session["_isDel"] != null)
                {
                    return (bool)Session["_isDel"];
                }
                else
                {
                    return false;
                }
            }
            set { Session["_isDel"] = value; }
        }
        private bool _tagChgRestrict
        {
            get
            {
                if (Session["_tagChgRestrict"] != null)
                {
                    return (bool)Session["_tagChgRestrict"];
                }
                else
                {
                    return false;
                }
            }
            set { Session["_tagChgRestrict"] = value; }
        }
        private string _tagChgRestrictVal
        {
            get
            {
                if (Session["_tagChgRestrictVal"] != null)
                {
                    return (string)Session["_tagChgRestrictVal"];
                }
                else
                {
                    return "";
                }
            }
            set { Session["_tagChgRestrictVal"] = value; }
        }
        private List<Order_Financing> oFinHeaders = new List<Order_Financing>();
        private List<ImportsBLItems> oImportsBLItems = new List<ImportsBLItems>();
        private List<ImportsBLSInvoice> OImportsBLSInvoice = new List<ImportsBLSInvoice>();
        private List<ImportsBLContainer> oImportsBLContainers = new List<ImportsBLContainer>();
        private ImportsBLHeader oHeader = new ImportsBLHeader();
        private List<ImportsBLCost> oImportsBLCosts = new List<ImportsBLCost>();

        protected List<ImportsBLItems> _PIITEMBACKUP { get { return (List<ImportsBLItems>)Session["_PIITEMBACKUP"]; } set { Session["_PIITEMBACKUP"] = value; } }
        DataTable GLOB_DataTable = new DataTable();
        clsImports obj = new clsImports();
        protected bool confresult { get { return (bool)Session["_confresult"]; } set { Session["_confresult"] = value; } }
        protected int index { get { return (int)Session["_index"]; } set { Session["_index"] = value; } }

        protected int confresult2 { get { return (int)Session["_confresult2"]; } set { Session["_confresult2"] = value; } }
        protected bool _isDuplicate { get { return (bool)Session["_isDuplicate"]; } set { Session["_isDuplicate"] = value; } }

        protected List<OrderPlanItem> _OrderPlanItemBackup { get { return (List<OrderPlanItem>)Session["_OrderPlanItemBackup"]; } set { Session["_OrderPlanItemBackup"] = value; } }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.SupplierImport:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "S" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PINO:
                    {
                        paramsText.Append(BaseCls.GlbPINo + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TradeTerms:
                    {
                        paramsText.Append("TOT" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PaymentTerms:
                    {
                        paramsText.Append("IPM" + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OrderPlanNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "" + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append("BANK" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "A" + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemNew:
                    {
                        paramsText.Append(txtExporter.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Declarant:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "D" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ImportAgent:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "A" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BLHeader:
                    {
                        // paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator);
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserSBU"].ToString() + seperator + -1 + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["UserSBU"].ToString()))
                {
                    lblSbuMsg1.Text = "SBU (Strategic Business) is not allocate for your login ID.";
                    lblSbuMsg2.Text = "There is not setup default SBU (Sttre Buds Unit) for your login ID.";
                    SbuPopup.Show();
                }
                //txtETD.Attributes.Add("Readonly", "Fales");
                ValidateTrue();
                var ctrlName = Request.Params[Page.postEventSourceID];
                SetFocus(ctrlName);
                var args = Request.Params[Page.postEventArgumentID];

                if (!IsPostBack)
                {
                    clear();
                }
                else
                {
                    if (Session["IsSearch"] != null && Convert.ToBoolean(Session["IsSearch"]) == true)
                    {
                        if (Session["IsSearch"].ToString() != "false")
                        {
                            Session["IsSearch"] = null;
                            mpUserPopup.Show();
                            txtSearchbyword.Focus();
                        }
                    }
                    if (_showItmCdChg)
                    {
                        popItmCdChg.Show();
                    }
                    else
                    {
                        popItmCdChg.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...  ", 4);
                //lblWarning.Text = "Error Occurred while processing...  " + ex;
                //divWarning.Visible = true;
            }
        }

        #region Search
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...  ", 4);
                //lblWarning.Text = "Error Occurred while processing...  " + ex;
                //divWarning.Visible = true;
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
                DisplayMessage("Error Occurred while processing...  ", 4);
                //lblWarning.Text = "Error Occurred while processing...  " + ex;
                //divWarning.Visible = true;
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                Session["IsSearch"] = null;
                mpUserPopup.Hide();

                if (lblvalue.Text == "DocNo")
                {
                    txtFinancialDocumentNo.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                else if (lblvalue.Text == "Item")
                {
                    GridViewRow row = (GridViewRow)dgvInvoiceDetails.Rows[dgvInvoiceDetails.EditIndex];
                    TextBox txtIbi_itm_cd = (TextBox)row.FindControl("txtIbi_itm_cd");
                    txtIbi_itm_cd.Text = grdResult.SelectedRow.Cells[2].Text;
                    if (!checkItemCode(txtIbi_itm_cd.Text))
                    {
                        //divWarning.Visible = true;
                        //lblWarning.Text = "Invalid item code";
                        DisplayMessage("Invalid item code");
                        txtIbi_itm_cd.Text = "";
                    }
                }
                else if (lblvalue.Text == "txtItem")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[1].Text.Trim();
                    txtItem_TextChanged(null, null);
                }
                else if (lblvalue.Text == "txtChgItem")
                {
                    txtchgItmCd.Text = grdResult.SelectedRow.Cells[1].Text.Trim();
                    txtchgItmCd_TextChanged(null, null);
                }
                else if (lblvalue.Text == "txtBLItem")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[1].Text.Trim();
                    txtItem_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Company")
                {
                    txtConsignee.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtConsigneeDesc.Text = grdResult.SelectedRow.Cells[2].Text;
                }
                else if (lblvalue.Text == "Declarant")
                {
                    txtDeclarant.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtDeclarant.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                }

                else if (lblvalue.Text == "Supplier")
                {
                    txtExporter.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtExporter.ToolTip = grdResult.SelectedRow.Cells[2].Text;

                    loadFromPorts();
                    txtExporter.Focus();
                    txtExporter_TextChanged(null, null);
                }

                else if (lblvalue.Text == "ImportAgent")
                {
                    txtAgent.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtAgent.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                }
                else if (lblvalue.Text == "BLHeader")
                {
                    txtDocNumber.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtDocNumber_TextChanged(null, null);
                }

                else if (lblvalue.Text == "CountyOfOrigin")
                {
                    txtCountyOfOrigin.Text = grdResult.SelectedRow.Cells[2].Text;
                    txtCountyOfOrigin.ToolTip = grdResult.SelectedRow.Cells[1].Text;
                }
                else if (lblvalue.Text == "ExporterCounty")
                {
                    txtExporterCounty.Text = grdResult.SelectedRow.Cells[2].Text;
                    txtExporterCounty.ToolTip = grdResult.SelectedRow.Cells[1].Text;
                }

                ViewState["SEARCH"] = null;

            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...");
                //lblWarning.Text = "Error Occurred while processing...  " + ex;
                //divWarning.Visible = true;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Session["IsSearch"] = null;
            mpUserPopup.Hide();
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

        private void FilterData()
        {
            if (ViewState["SEARCH"] != null)
            {
                if (lblvalue.Text == "DocNo")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                    DataTable result = CHNLSVC.CommonSearch.SEARCH_FIN_HDR(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    if (string.IsNullOrEmpty(txtSearchbyword.Text))
                    {
                        for (int x = result.Rows.Count; x > 100; x--)
                        {
                            DataRow dr = result.Rows[x - 1];
                            dr.Delete();
                        }
                        result.AcceptChanges();
                    }
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "Item")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "txtItem")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "txtChgItem")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "txtBLItem")
                {
                    oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetBLItems(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim(), oImportsBLItems.FirstOrDefault().Ibi_seq_no);
                    if (result.Rows.Count < 1)
                    {
                        result = CHNLSVC.CommonSearch.GetPIItems(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim(), hdfPiNo.Value);
                    }
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "Company")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                    DataTable result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "Declarant")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Declarant);
                    DataTable result = CHNLSVC.CommonSearch.GetAgents(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "Supplier")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    DataTable result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "ImportAgent")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                    DataTable result = CHNLSVC.CommonSearch.GetAgents(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "BLHeader")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                    // DataTable result = CHNLSVC.CommonSearch.SearchBLHeader(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    DataTable result = CHNLSVC.CommonSearch.SearchBLHeaderForSI(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "CountyOfOrigin")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                    DataTable result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    result.Columns["Code"].SetOrdinal(1);
                    result.Columns["Description"].SetOrdinal(0);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
                else if (lblvalue.Text == "ExporterCounty")
                {
                    ValidateTrue();
                    ViewState["SEARCH"] = null;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
                    DataTable result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, ddlSearchbykey.Text, txtSearchbyword.Text.Trim());
                    result.Columns["Code"].SetOrdinal(1);
                    result.Columns["Description"].SetOrdinal(0);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }
            }
        }

        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (validateHeader())
            {
                oHeader = new ImportsBLHeader();
                oHeader.Ib_seq_no = 0;
                oHeader.Ib_com = Session["UserCompanyCode"].ToString();
                oHeader.Ib_sbu = Session["UserSBU"].ToString();
                oHeader.Ib_doc_no = txtDocNumber.Text.Trim();
                oHeader.Ib_bl_no = txtBLNumber.Text.Trim();
                oHeader.Ib_bl_dt = Convert.ToDateTime(txtShipmentDate.Text);
                oHeader.Ib_clear_pty = (chkBypassEntry.Checked) ? true : false;
                oHeader.Ib_is_cusdec = 0;
                oHeader.Ib_tot_foc = (chkTotalFOCShipment.Checked) ? true : false;
                oHeader.Ib_ref_no = txtReferenceNum.Text; // txtReferenceNum.Text.Trim(); 2015-12-02
                oHeader.Ib_rmk = txtRemark.Text;
                oHeader.Ib_mbl_no = "test";// string.Empty;
                oHeader.Ib_supp_tp = "S";
                oHeader.Ib_supp_cd = txtExporter.Text.Trim();
                oHeader.Ib_consi_cd = txtConsignee.Text.Trim();
                oHeader.Ib_decl_cd = txtDeclarant.Text.Trim();
                oHeader.Ib_agent_tp = "A";
                oHeader.Ib_agent_cd = txtAgent.Text.Trim();
                oHeader.Ib_expo_cnty = txtExporterCounty.Text.Trim();
                oHeader.Ib_origin_cnty = txtCountyOfOrigin.Text.Trim();
                oHeader.Ib_desti_cnty = "LK";
                oHeader.Ib_tot_pkg = txtTotalPackages.Text.Trim();
                oHeader.Ib_pack_lst_no = txtPackingListNo.Text.Trim();
                oHeader.Ib_vessel_no = txtVessel.Text.Trim();
                oHeader.Ib_voyage = txtVoyage.Text.Trim();
                oHeader.Ib_loading_place = txtPlaceOfLanding.Text.Trim();
                oHeader.Ib_frm_port = ddlFromPort.SelectedValue.ToString();
                oHeader.Ib_to_port = ddlToPort.SelectedValue.ToString().Trim();
                oHeader.Ib_anal_3 = ddlFinalPort.SelectedValue.ToString().Trim();
                oHeader.Ib_etd = Convert.ToDateTime(txtETD.Text);
                oHeader.Ib_eta = Convert.ToDateTime(txtETA.Text);
                oHeader.Ib_cur_cd = lblCurCode.Text;
                oHeader.Ib_anal_1 = ddlTradeTerms.SelectedValue.ToString();
                oHeader.Ib_anal_2 = ddlUOM.SelectedValue.ToString();
                oHeader.Ib_anal_4 = string.Empty;
                oHeader.Ib_entry_no = string.Empty;
                oHeader.Ib_bl_ref_no = txtReferenceNum.Text.Trim();
                oHeader.Ib_carry_tp = ddlCaringType.SelectedValue.ToString();
                oHeader.Ib_tos = ddlModeofShipment.SelectedValue.ToString();

                oHeader.Ib_si_seq_no = string.IsNullOrEmpty(txtShSeq.Text) ? 0 : Convert.ToInt32(txtShSeq.Text);

                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                MasterCompany _mstCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                string curCd = "LKR";
                if (_mstCom != null)
                {
                    if (!string.IsNullOrEmpty(_mstCom.Mc_cur_cd))
                    {
                        curCd = _mstCom.Mc_cur_cd;
                    }
                }
                if (lbtnrate.Text != "")
                {
                    oHeader.Ib_ex_rt = Convert.ToDecimal(lbtnrate.Text);
                }
                else
                {
                    DisplayMessage("Please update exchange rate");
                    return;
                }
                //MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), lblCurCode.Text.Trim(), oHeader.Ib_bl_dt, curCd, string.Empty);
                //if (_exc1 != null)
                //{
                //    if (_exc1.Mer_id != null)
                //    {
                //        oHeader.Ib_ex_rt = _exc1.Mer_bnkbuy_rt;
                //    }
                //}


                //if (oHeader.Ib_ex_rt == 0)
                //{
                //    DisplayMessage("Please update exchange rate");
                //    return;
                //}

                //oHeader.Ib_tot_bl_amt = (String.IsNullOrEmpty(txtTotalOrderValueIn.Text)) ? 0 : Convert.ToDecimal(txtTotalOrderValueIn.Text);
                oHeader.Ib_tot_bl_amt = (String.IsNullOrEmpty(txtBlValue.Text)) ? 0 : Convert.ToDecimal(txtBlValue.Text);
                oHeader.Ib_loc_of_goods = txtlocationOfGoods.Text;
                if (txtDocReceivedDate.Text == "")
                {
                    oHeader.Ib_doc_clear_dt = DateTime.Now;
                    oHeader.Ib_doc_rec_dt = DateTime.Now;
                }
                else
                {
                    oHeader.Ib_doc_clear_dt = Convert.ToDateTime(txtDocReceivedDate.Text);
                    oHeader.Ib_doc_rec_dt = Convert.ToDateTime(txtDocReceivedDate.Text);
                }
                oHeader.Ib_ignore = 0;
                oHeader.Ib_entry_no = string.Empty;
                oHeader.Ib_stus = "P";
                oHeader.Ib_cre_by = Session["UserID"].ToString();
                oHeader.Ib_cre_dt = DateTime.Now;
                oHeader.Ib_mod_by = Session["UserID"].ToString();
                oHeader.Ib_mod_dt = DateTime.Now;
                oHeader.Ib_session_id = Session["SessionID"].ToString();

                //lakshan 2016 Mar 16 add IB_STANDARD_LEAD
                oHeader.Ib_standard_lead = 0;
                SupplierPort _supPort = CHNLSVC.General.GetSupplierPort(new SupplierPort()
                {
                    MSPR_COM = Session["UserCompanyCode"].ToString(),
                    MSPR_CD = oHeader.Ib_supp_cd,
                    MSPR_TP = "S",
                    MSPR_ACT = 1
                });
                if (_supPort != null)
                {
                    oHeader.Ib_standard_lead = _supPort.MSPR_LEAD_TIME;
                }
                else
                {
                    DisplayMessage("Please update supplier Lead time CMB ");
                    return;
                }
                MasterAutoNumber mastAutoNo = new MasterAutoNumber();
                //mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString() + "(" + Session["UserSBU"].ToString() + ")";
                mastAutoNo.Aut_cate_cd = Session["UserCompanyCode"].ToString();
                mastAutoNo.Aut_cate_tp = "COM";
                mastAutoNo.Aut_direction = null;
                mastAutoNo.Aut_modify_dt = null;
                mastAutoNo.Aut_moduleid = "SI";
                mastAutoNo.Aut_start_char = "SI";
                mastAutoNo.Aut_year = DateTime.Now.Year;

                oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];

                oImportsBLContainers = (List<ImportsBLContainer>)Session["oImportsBLContainers"];

                OImportsBLSInvoice = (List<ImportsBLSInvoice>)Session["OImportsBLSInvoice"];

                oImportsBLCosts = (List<ImportsBLCost>)Session["oImportsBLCosts"];

                if (OImportsBLSInvoice == null)
                {
                    OImportsBLSInvoice = new List<ImportsBLSInvoice>();
                }
                bool isNewRocord = true;
                String err;
                String outBlNumber;
                foreach (var item in oImportsBLItems)
                {
                    if (string.IsNullOrEmpty(item.Ibi_tag))
                    {
                        item.Ibi_tag = "N";
                    }
                }
                #region add by lakshan 07Dec2017 validate item code
                MasterItem _mstItm = new MasterItem();
                foreach (var _blItm in oImportsBLItems)
                {
                    _mstItm = CHNLSVC.General.GetItemMaster(_blItm.Ibi_itm_cd.Trim().ToUpper());
                    if (_mstItm == null)
                    {
                        DispMsg("Invalid item code ! " + _blItm.Ibi_itm_cd.Trim().ToUpper()); return;
                    }
                }
                #endregion
                #region add by lakshan as per the nuwan/dharshana
                DateTime _tmpAllDt = DateTime.MinValue;
                oHeader.IB_CREDALOW_DT = DateTime.TryParse(txtCreditAllowDate.Text, out _tmpAllDt) ? Convert.ToDateTime(txtCreditAllowDate.Text) : DateTime.MinValue;
                #endregion
                Int32 result = CHNLSVC.Financial.SaveBillOfLading(oHeader, oImportsBLItems, OImportsBLSInvoice, oImportsBLContainers, oImportsBLCosts, mastAutoNo, isNewRocord, out err, out outBlNumber);
                if (result > 0)
                {
                    DisplayMessage("Successfully saved the B/L number : " + outBlNumber, 3);
                    clear();
                }
                else
                {
                    if (err.Contains("CHK_IPIBALQTY"))
                    {
                        DisplayMessage("PI balance quentity exceed !!!", 1);
                    }
                    else
                    {
                        DisplayMessage("Error :" + err, 4);
                    }
                }
            }
        }
        private void DispMsg(string msgText, string msgType = "")
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "W" || msgType == "")
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
        }
        protected void btnUpdateAdvnDet_Click(object sender, EventArgs e)
        {
            if (validateHeader())
            {
                if (Session["oHeader"] != null)
                {
                    oHeader = (ImportsBLHeader)Session["oHeader"];

                    if (oHeader.Ib_stus == "A")
                    {
                        DisplayMessage("Document is already approved. Cannot amend.", 2);
                        return;
                    }
                    if (oHeader.Ib_stus == "C")
                    {
                        DisplayMessage("Document is cancelled. Can not amend", 2);
                        return;
                    }

                    oHeader.Ib_com = Session["UserCompanyCode"].ToString();
                    oHeader.Ib_sbu = Session["UserSBU"].ToString();
                    oHeader.Ib_doc_no = txtDocNumber.Text.Trim();
                    oHeader.Ib_bl_no = txtBLNumber.Text.Trim();
                    oHeader.Ib_bl_dt = Convert.ToDateTime(txtShipmentDate.Text);
                    oHeader.Ib_clear_pty = (chkBypassEntry.Checked) ? true : false;
                    oHeader.Ib_is_cusdec = 0;
                    oHeader.Ib_tot_foc = (chkTotalFOCShipment.Checked) ? true : false;
                    oHeader.Ib_ref_no = txtReferenceNum.Text; // txtReferenceNum.Text.Trim(); 2015-12-02
                    oHeader.Ib_rmk = txtRemark.Text;
                    oHeader.Ib_mbl_no = "test";// string.Empty;
                    oHeader.Ib_supp_tp = "S";
                    oHeader.Ib_supp_cd = txtExporter.Text.Trim();
                    oHeader.Ib_consi_cd = txtConsignee.Text.Trim();
                    oHeader.Ib_decl_cd = txtDeclarant.Text.Trim();
                    oHeader.Ib_agent_tp = "A";
                    oHeader.Ib_agent_cd = txtAgent.Text.Trim();
                    oHeader.Ib_expo_cnty = txtExporterCounty.Text.Trim();
                    oHeader.Ib_origin_cnty = txtCountyOfOrigin.Text.Trim();
                    oHeader.Ib_desti_cnty = "LK";
                    oHeader.Ib_tot_pkg = txtTotalPackages.Text.Trim();
                    oHeader.Ib_pack_lst_no = txtPackingListNo.Text.Trim();
                    oHeader.Ib_vessel_no = txtVessel.Text.Trim();
                    oHeader.Ib_voyage = txtVoyage.Text.Trim();
                    oHeader.Ib_loading_place = txtPlaceOfLanding.Text.Trim();
                    oHeader.Ib_frm_port = ddlFromPort.SelectedValue.ToString();
                    oHeader.Ib_to_port = ddlToPort.SelectedValue.ToString().Trim();
                    oHeader.Ib_anal_3 = ddlFinalPort.SelectedValue.ToString().Trim();
                    oHeader.Ib_etd = Convert.ToDateTime(txtETD.Text);
                    oHeader.Ib_eta = Convert.ToDateTime(txtETA.Text);
                    oHeader.Ib_cur_cd = lblCurCode.Text;
                    oHeader.Ib_anal_1 = ddlTradeTerms.SelectedValue.ToString();
                    oHeader.Ib_anal_2 = ddlUOM.SelectedValue.ToString();
                    oHeader.Ib_anal_4 = string.Empty;
                    oHeader.Ib_entry_no = string.Empty;
                    oHeader.Ib_bl_ref_no = txtReferenceNum.Text.Trim();
                    oHeader.Ib_carry_tp = ddlCaringType.SelectedValue.ToString();
                    oHeader.Ib_tos = ddlModeofShipment.SelectedValue.ToString();

                    MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                    MasterCompany _mstCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                    string curCd = "LKR";
                    if (_mstCom != null)
                    {
                        if (!string.IsNullOrEmpty(_mstCom.Mc_cur_cd))
                        {
                            curCd = _mstCom.Mc_cur_cd;
                        }
                    }
                    MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), lblCurCode.Text.Trim(), oHeader.Ib_bl_dt, curCd, string.Empty);
                    if (_exc1 != null)
                    {
                        if (_exc1.Mer_id != null)
                        {
                            oHeader.Ib_ex_rt = _exc1.Mer_bnkbuy_rt;
                        }
                    }
                    if (oHeader.Ib_ex_rt == 0)
                    {
                        DisplayMessage("Please update exchange rate");
                        return;
                    }

                    //oHeader.Ib_tot_bl_amt = (String.IsNullOrEmpty(txtTotalOrderValueIn.Text)) ? 0 : Convert.ToDecimal(txtTotalOrderValueIn.Text);
                    oHeader.Ib_tot_bl_amt = (String.IsNullOrEmpty(txtBlValue.Text)) ? 0 : Convert.ToDecimal(txtBlValue.Text);
                    oHeader.Ib_loc_of_goods = txtlocationOfGoods.Text;
                    //oHeader.Ib_doc_clear_dt = Convert.ToDateTime(txtDocReceivedDate.Text);
                    //oHeader.Ib_doc_rec_dt = Convert.ToDateTime(txtDocReceivedDate.Text);
                    oHeader.Ib_ignore = 0;
                    oHeader.Ib_anal_4 = string.Empty;
                    oHeader.Ib_entry_no = string.Empty;
                    oHeader.Ib_stus = "P";
                    oHeader.Ib_cre_by = Session["UserID"].ToString();
                    oHeader.Ib_cre_dt = DateTime.Now;
                    oHeader.Ib_mod_by = Session["UserID"].ToString();
                    oHeader.Ib_mod_dt = DateTime.Now;
                    oHeader.Ib_session_id = Session["SessionID"].ToString();
                    //lakshan 2016 Mar 16 add IB_STANDARD_LEAD
                    oHeader.Ib_standard_lead = 0;
                    SupplierPort _supPort = CHNLSVC.General.GetSupplierPort(new SupplierPort()
                    {
                        MSPR_COM = Session["UserCompanyCode"].ToString(),
                        MSPR_CD = oHeader.Ib_supp_cd,
                        MSPR_TP = "S",
                        MSPR_ACT = 1
                    });
                    if (_supPort != null)
                    {
                        oHeader.Ib_standard_lead = _supPort.MSPR_LEAD_TIME;
                    }
                    else
                    {
                        DisplayMessage("Please update supplier Lead time CMB ");
                        return;
                    }
                    oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];

                    oImportsBLContainers = (List<ImportsBLContainer>)Session["oImportsBLContainers"];

                    OImportsBLSInvoice = (List<ImportsBLSInvoice>)Session["OImportsBLSInvoice"];

                    if (OImportsBLSInvoice == null)
                    {
                        OImportsBLSInvoice = new List<ImportsBLSInvoice>();
                    }

                    oImportsBLCosts = (List<ImportsBLCost>)Session["oImportsBLCosts"];
                    foreach (ImportsBLCost item in oImportsBLCosts)
                    {
                        item.Ibcs_doc_no = txtDocNumber.Text.ToUpper();
                        item.Ibcs_act = 1;
                    }
                    if (oImportsBLCosts == null)
                    {
                        DisplayMessage("please setup cost details", 2);
                        return;
                    }

                    bool isNewRocord = false;
                    String err;
                    String outBlNumber;
                    MasterAutoNumber _masterAuto = new MasterAutoNumber();
                    #region add by lakshan 07Dec2017 validate item code
                    MasterItem _mstItm = new MasterItem();
                    foreach (var _blItm in oImportsBLItems)
                    {
                        _mstItm = CHNLSVC.General.GetItemMaster(_blItm.Ibi_itm_cd.Trim().ToUpper());
                        if (_mstItm == null)
                        {
                            DispMsg("Invalid item code ! " + _blItm.Ibi_itm_cd.Trim().ToUpper()); return;
                        }
                    }
                    #endregion
                    Int32 result = CHNLSVC.Financial.UpdateBillOfLadingWeb(oHeader, oImportsBLItems, OImportsBLSInvoice, oImportsBLContainers, oImportsBLCosts,
                        _masterAuto, isNewRocord, out err, out outBlNumber);
                    if (result > 0)
                    {
                        DisplayMessage("Successfully updated. The B/L Number :" + outBlNumber, 3);
                        clear();
                    }
                    else
                    {
                        DisplayMessage("Error :" + err, 4);
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (hdfCancel.Value == "No")
            {
                return;
            }
            if (Session["oHeader"] != null)
            {
                oHeader = (ImportsBLHeader)Session["oHeader"];
                Int32 Seq = oHeader.Ib_seq_no;
                string err;

                if (oHeader.Ib_stus == "A")
                {
                    DisplayMessage("Cannot cancel approved documents", 2);
                    return;
                }

                Int32 result = CHNLSVC.Financial.UPDATE_IMP_BL_STUS(Session["UserID"].ToString(), "C", Seq, Session["SessionID"].ToString(), out err);
                if (result > 0)
                {
                    DisplayMessage("Successfully cancelled the document", 3);
                    clear();
                }
                else
                {
                    DisplayMessage("Error :" + err, 4);
                }
            }
            else
            {
                DisplayMessage("please select the B/L number", 2);
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (hdfAppro.Value == "No")
            {
                return;
            }
            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16029))
            {
                DisplayMessage("You don not have permission to approve. Permission code - 16029");
                return;
            }

            if (Session["oHeader"] != null)
            {
                oHeader = (ImportsBLHeader)Session["oHeader"];
                if (oHeader.Ib_stus == "A")
                {
                    DisplayMessage("Selected document is already approved", 2);
                    return;
                }
                Int32 Seq = oHeader.Ib_seq_no;
                string err;
                Int32 result = CHNLSVC.Financial.UPDATE_IMP_BL_STUS(Session["UserID"].ToString(), "A", Seq, Session["SessionID"].ToString(), out err);
                if (result > 0)
                {
                    DisplayMessage("Successfully approved", 3);
                    sendApprovedMails(oHeader.Ib_doc_no);
                    clear();
                }
                else
                {
                    DisplayMessage("Error :" + err, 4);
                }
            }
            else
            {
                DisplayMessage("please recall the B/L.", 2);
            }
        }

        protected void lblClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void btnFinancialDocumentNo_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
            DataTable result = CHNLSVC.CommonSearch.SEARCH_FIN_HDR(SearchParams, null, null);
            for (int x = result.Rows.Count; x > 100; x--)
            {
                DataRow dr = result.Rows[x - 1];
                dr.Delete();
            }
            result.AcceptChanges();
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "DocNo";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show(); Session["IsSearch"] = true;
        }

        protected void grdResult_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdResult.PageIndex = e.NewPageIndex;
                grdResult.DataSource = null;
                grdResult.DataSource = (DataTable)ViewState["SEARCH"];
                grdResult.DataBind();
                mpUserPopup.Show(); Session["IsSearch"] = true;
            }
            catch (Exception ex)
            {
                DisplayMessage("Error Occurred while processing...  ", 4);
                //lblWarning.Text = "Error Occurred while processing...  " + ex;
                //divWarning.Visible = true;
            }
        }

        protected void btnFinancialDocumentNoView_Click(object sender, EventArgs e)
        {
            if (ddlModeofShipment.SelectedIndex < 1 && dgvFinancialDocs.Rows.Count > 1)
            {
                DisplayMessage("Please select a mode Of shipment ", 2);
                return;
            }

            if (!string.IsNullOrEmpty(txtFinancialDocumentNo.Text))
            {
                DataTable _dt = CHNLSVC.Inventory.getImpPiHdrTps(txtFinancialDocumentNo.Text);
                if (_dt.Rows.Count < 1)
                {
                    DisplayMessage("Financial document dont have valid type", 2);
                    return;
                }
                else if (_dt.Rows.Count > 1)
                {
                    DisplayMessage("Financial document have types more than 1", 2);
                    return;
                }
                else
                {
                    if (dgvFinancialDocs.Rows.Count > 1)
                    {
                        if (_dt.Rows[0]["ip_tp"].ToString() != ddlModeofShipment.SelectedValue)
                        {
                            DisplayMessage("Please select a valid mode of shipment", 2);
                            return;
                        }
                    }
                    else
                    {
                        ddlModeofShipment.SelectedIndex = ddlModeofShipment.Items.IndexOf(ddlModeofShipment.Items.FindByValue(_dt.Rows[0]["ip_tp"].ToString()));
                    }
                }
                Order_Financing oFinalcialHeader = CHNLSVC.Financial.GET_IMP_FIN_HDR_BY_DOC(Session["UserCompanyCode"].ToString(), txtFinancialDocumentNo.Text.Trim());
                if (oFinalcialHeader != null)
                {
                    if (oFinalcialHeader.If_stus == "C")
                    {
                        DisplayMessage("Selected document is cancelled", 2);
                        return;
                    }
                    if (oFinalcialHeader.If_stus == "S")
                    {
                        DisplayMessage("Selected document is not approved", 2);
                        return;
                    }

                    if (Session["oFinHeaders"] != null)
                    {
                        oFinHeaders = (List<Order_Financing>)Session["oFinHeaders"];
                    }
                    else
                    {
                        oFinHeaders = new List<Order_Financing>();
                    }

                    if (oFinHeaders.Exists(x => x.If_doc_no == oFinalcialHeader.If_doc_no && x.If_doc_dt == oFinalcialHeader.If_doc_dt))
                    {
                        DisplayMessage("Selected document is already added.", 2);
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScrollPage", "window.scroll(0,0);", true);
                        BindFinHeaders();
                        return;
                    }

                    oFinHeaders.Add(oFinalcialHeader);
                    Session["oFinHeaders"] = oFinHeaders;
                    txtFinancialDocumentNo.Text = "";
                    BindFinHeaders();
                }
                else
                {
                    DisplayMessage("Please enter correct a financing number...", 2);
                }
            }
            else
            {
                DisplayMessage("Please enter an order financing number", 2);
            }
        }

        protected void btnClosePIs_Click(object sender, EventArgs e)
        {
            mpPerfomaInvoices.Hide();
        }

        protected void dgvFinancialDocs_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            CheckBox cb1 = (CheckBox)dgvFinancialDocs.Rows[index].FindControl("chkSelect");
            Label lblif_doc_no = (Label)dgvFinancialDocs.Rows[index].FindControl("lblif_doc_no");
            Label lblif_ref_no = (Label)dgvFinancialDocs.Rows[index].FindControl("lblif_ref_no");

            if (cb1.Checked)
            {
                txtReferenceNum.Text = lblif_ref_no.Text;
                Session["lblif_doc_no"] = lblif_doc_no.Text;
                Order_Financing oFinalcialHeader = CHNLSVC.Financial.GET_IMP_FIN_HDR_BY_DOC(Session["UserCompanyCode"].ToString(), lblif_doc_no.Text.Trim());
                if (oFinalcialHeader != null)
                {
                    ddlTradeTerms.SelectedIndex = ddlTradeTerms.Items.IndexOf(ddlTradeTerms.Items.FindByValue(oFinalcialHeader.If_top));
                    ddlTradeTerms_SelectedIndexChanged(null, null);
                    txtExporter.Text = oFinalcialHeader.If_supp;
                    txtExporter_TextChanged(null, null);
                    Session["CreditP"] = oFinalcialHeader.If_crdt_pd;
                    int _crep = (int)Session["CreditP"];
                    txtCreditAllowDate.Text = Convert.ToDateTime(txtShipmentDate.Text).AddDays(oFinalcialHeader.If_crdt_pd).ToString("dd/MMM/yyyy");
                    // BusEntityItem _bs=CHNLSVC.General.get_
                    //  txtCreditAllowDate.Text = Convert.ToDateTime();
                    //   txtExporterCounty.Text=oFinalcialHeader.If_supp
                }

                GetPerfomaInvoices(lblif_doc_no.Text);
                mpPerfomaInvoices.Show();
            }
            else
            {
                if (Session["oImportsBLItems"] != null)
                {
                    oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];

                    if (oImportsBLItems.FindAll(x => x.Ibi_fin_no == lblif_doc_no.Text).Count > 0)
                    {
                        List<ImportsBLItems> oList = oImportsBLItems.FindAll(x => x.Ibi_fin_no == lblif_doc_no.Text);
                        oList.ForEach(x => x.Ibi_stus = 0);//.Where(x => x.Ibi_fin_no == lblif_doc_no.Text);
                        Session["oImportsBLItems"] = oImportsBLItems;
                        BindPInvoiceItems();
                        clearEntryLine();
                        setCostValues();
                        BindCostItems();
                    }
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            GetPerfomaInvoices(Session["lblif_doc_no"].ToString());
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (isInvoiceSelect())
            {
                for (int i = 0; i < dgvPerfomaInvoices.Rows.Count; i++)
                {
                    GridViewRow row = (GridViewRow)dgvPerfomaInvoices.Rows[i];
                    CheckBox cb1 = (CheckBox)row.FindControl("chkSelectPIs");
                    if (cb1.Checked)
                    {
                        Label lblIfp_seq_no = (Label)row.FindControl("lblIfp_seq_no");
                        Label lblIfp_doc_no = (Label)row.FindControl("lblIfp_doc_no");
                        Label lblIfp_pi_no = (Label)row.FindControl("lblIfp_pi_no");

                        //add by rukshan order plane item tag view 
                        List<OrderPlanItem> _OrderPlaneItem = CHNLSVC.Financial.GET_IMP_OPBY_PI(lblIfp_pi_no.Text);

                        hdfPiNo.Value = lblIfp_pi_no.Text;
                        hdfPiSeqNo.Value = lblIfp_seq_no.Text;
                        //

                        List<ImportPIDetails> oImportPIDetails = CHNLSVC.Financial.GET_IMP_PI_ITM_BY_PINO(lblIfp_pi_no.Text.Trim(), Convert.ToInt32(lblIfp_seq_no.Text));
                        if (oImportPIDetails != null && oImportPIDetails.Count > 0)
                        {
                            decimal TotalQty = oImportPIDetails.Sum(x => x.IPI_BAL_QTY);
                            if (TotalQty == 0)
                            {
                                DisplayMessage("There is no balance quantity in PI : " + lblIfp_pi_no.Text, 2);
                                return;
                            }

                            List<ImportsBLItems> temp = new List<ImportsBLItems>();
                            foreach (ImportPIDetails item in oImportPIDetails)
                            {
                                ImportsBLItems I1 = PIToBL(item);

                                //add by rukshan order plane item tag view 
                                var _filter = _OrderPlaneItem.Where(x => x.IOI_ITM_CD == item.IPI_ITM_CD && x.IOI_ITM_STUS == item.IPI_ITM_STUS).ToList();
                                foreach (var va in _filter)
                                {
                                    if (va != null)
                                    {
                                        I1.Ibi_tag = va.IOI_TAG;
                                        if ((I1.Ibi_tag == "N") || (I1.Ibi_tag == ""))
                                        {
                                            I1.Ibi_tag_Desc = "General";
                                        }
                                        else
                                        {
                                            I1.Ibi_tag_Desc = "Special Project";
                                        }
                                        I1.Ibi_project_name = va.IOI_ProjectName;
                                    }
                                }
                                //
                                temp.Add(I1);
                            }
                            if (temp.Count > 0)
                            {
                                var v = temp.Distinct().FirstOrDefault();
                                if (v != null)
                                {
                                    ImportPIHeader _piHeader = CHNLSVC.Financial.GetPIByPIID(v.Ibi_pi_no);
                                    if (_piHeader != null)
                                    {
                                        ddlFromPort.SelectedIndex = ddlFromPort.Items.IndexOf(ddlFromPort.Items.FindByValue(_piHeader.IP_FRM_PORT));
                                        ddlToPort.SelectedIndex = ddlToPort.Items.IndexOf(ddlToPort.Items.FindByValue(_piHeader.IP_TO_PORT));
                                        // ddlFinalPort.SelectedIndex = ddlToPort.Items.IndexOf(ddlToPort.Items.FindByValue(_piHeader.Ib_anal_3));
                                        ddlModeofShipment.SelectedIndex = ddlModeofShipment.Items.IndexOf(ddlModeofShipment.Items.FindByValue(_piHeader.IP_TOS));
                                    }
                                }

                            }
                            AddInvoiceItems(temp);
                            _PIITEMBACKUP = temp;
                        }
                        else
                        {
                            divAlert.Visible = true;
                            lblAlert.Text = "No invoice items found.";
                        }
                    }
                }
                BindPInvoiceItems();
            }
            else
            {
                divAlert.Visible = true;
                lblAlert.Text = "Please select invoices to add";
            }
        }

        protected void OnUpdate(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblIbi_pi_line = (Label)row.FindControl("lblIbi_pi_line");
                Label lblTmp_ibi_line = (Label)row.FindControl("lblTmp_ibi_line");
                Label lblIbi_line = (Label)row.FindControl("lblIbi_line");
                TextBox txtIbi_itm_cd = (TextBox)row.FindControl("txtIbi_itm_cd");
                TextBox txtIbi_pi_no = (TextBox)row.FindControl("txtIbi_pi_no");
                TextBox txtIbi_tag_Desc = (TextBox)row.FindControl("txtIbi_tag_Desc");
                TextBox txtIbi_qty = (TextBox)row.FindControl("txtIbi_qty");
                TextBox txtIbi_pi_unit_rt = (TextBox)row.FindControl("txtIbi_pi_unit_rt");
                Label lblisNewItem = (Label)row.FindControl("lblisNewItem");
                DropDownList ddlSelectPriceBase = (DropDownList)row.FindControl("ddlSelectPriceBase");
                DropDownList ddlItemTag = (DropDownList)row.FindControl("ddlItemTag");
                TextBox _shseq = (TextBox)row.FindControl("txtSeqNo");

                Label lblIbi_qty_back = (Label)row.FindControl("lblIbi_qty_back");
                index = row.RowIndex;
                string ItemCOde = txtIbi_itm_cd.Text;

                if (!checkItemCode(ItemCOde))
                {
                    DisplayMessage("Invalid item code.", 2);
                    return;
                }

                string ItemTag = ddlItemTag.SelectedValue.ToString();
                string priceBasis = ddlSelectPriceBase.SelectedValue.ToString();

                string orderQty = txtIbi_qty.Text.Trim();
                string unitPrice = txtIbi_pi_unit_rt.Text.Trim();
                decimal actuqty = 0;
                if (!string.IsNullOrEmpty(lblIbi_qty_back.Text))
                {
                    actuqty = Convert.ToDecimal(lblIbi_qty_back.Text);
                }




                if (string.IsNullOrEmpty(orderQty))
                {
                    DisplayMessage("Please enter quantity", 2);
                    return;
                }
                if (string.IsNullOrEmpty(unitPrice))
                {
                    DisplayMessage("Please unit price", 2);
                    return;
                }

                if (!isdecimal(orderQty))
                {
                    DisplayMessage("Please enter valid quantity", 2);
                    txtQty.Focus();
                    return;
                }

                if (Convert.ToDecimal(unitPrice) == 0)
                {
                    DisplayMessage("Unit price cannot be zero", 2);
                    return;
                }

                if (Convert.ToDecimal(orderQty) == 0)
                {
                    DisplayMessage("Order quantity cannot be zero", 2);
                    return;
                }


                List<ImportPIDetails> oPIItems = CHNLSVC.Financial.GET_IMP_PIITEM(txtIbi_pi_no.Text.Trim());
                //comment by rukshan need to edit qty 
                //if (oPIItems != null && oPIItems.Count > 0)
                //{
                //    ImportPIDetails oItemSele = oPIItems.Find(x => x.IPI_ITM_CD == txtIbi_itm_cd.Text.Trim());
                //    if (oItemSele != null && oItemSele.IPI_BAL_QTY < Convert.ToDecimal(orderQty))
                //    {
                //        DisplayMessage("You cannot exceed balance quantity.", 2);
                //        txtQty.Focus();
                //        return;
                //    }
                //}

                if (!isdecimal(unitPrice))
                {
                    DisplayMessage("Please enter valid unit price", 2);
                    txtQty.Focus();
                    return;
                }

                if (Convert.ToDecimal(orderQty) < 0)
                {
                    DisplayMessage("Please valid quantity", 2);
                    return;
                }
                if (Convert.ToDecimal(unitPrice) < 0)
                {
                    DisplayMessage("Please valid unit price", 2);
                    return;
                }
                if (confresult == false)
                {
                    if (Convert.ToDecimal(orderQty) > actuqty)
                    {
                        lblMssg.Text = "You're exceeding the PI balance qty";
                        lblMssg1.Text = "Do you want to continue ?";
                        PopupConfBox.Show();
                        return;
                    }
                }
                if (Session["oImportsBLItems"] != null)
                {
                    oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];
                    // ImportsBLItems oEdiedItem = oImportsBLItems.Find(x => x.Ibi_pi_no == txtIbi_pi_no.Text.Trim() && x.Ibi_pi_line == Convert.ToInt32(lblIbi_pi_line.Text.Trim()));
                    ImportsBLItems oEdiedItem = oImportsBLItems.Find(x => x.Tmp_ibi_line == Convert.ToInt32(lblTmp_ibi_line.Text.Trim()));
                    // oEdiedItem.Ibi_itm_cd = ItemCOde.Trim();

                    int indexOf = oImportsBLItems.IndexOf(oEdiedItem);

                    if (oEdiedItem == null)
                    {
                        return;
                    }
                    oEdiedItem.Ibi_qty = Convert.ToDecimal(orderQty);
                    oEdiedItem.Ibi_bal_qty = Convert.ToDecimal(orderQty);
                    oEdiedItem.Ibi_unit_rt = Convert.ToDecimal(unitPrice);
                    oEdiedItem.isRecordStatus = 1;
                    oEdiedItem.Ibi_tp = priceBasis;
                    oEdiedItem.Ibi_tag = ItemTag;
                    oEdiedItem.Ibi_anal_1 = _shseq.Text.ToString();
                    updateItemDetials(ItemCOde);
                    Session["oImportsBLItems"] = oImportsBLItems;
                    dgvInvoiceDetails.EditIndex = -1;
                    BindPInvoiceItems();
                }
                else
                {
                    divWarning.Visible = true;
                    lblAlert.Text = "Error";
                }

                ddlTradeTerms_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void OnCancel(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    dgvInvoiceDetails.EditIndex = -1;
                    BindPInvoiceItems();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void dgvInvoiceDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void dgvInvoiceDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (hdfDelInvoiceItem.Value == "No")
                {
                    return;
                }
                _isDel = true;
                GridViewRow row = dgvInvoiceDetails.Rows[e.RowIndex];
                Label lblIbi_pi_line = (Label)row.FindControl("lblIbi_line");
                Label lblTmp_ibi_line = (Label)row.FindControl("lblTmp_ibi_line");
                Label lblIbi_pi_no = (Label)row.FindControl("lblIbi_pi_no");
                Label lblisNewItem = (Label)row.FindControl("lblisNewItem");
                Label lblIbi_itm_cd = (Label)row.FindControl("lblIbi_itm_cd");

                if (Session["oImportsBLItems"] != null)
                {
                    oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];
                    ImportsBLItems oEdiedItem = oImportsBLItems[e.RowIndex];
                    oEdiedItem.isRecordStatus = 1;
                    oEdiedItem.Ibi_stus = 0;
                    oImportsBLItems.RemoveAll(x => x.Tmp_ibi_line == Convert.ToInt32(lblTmp_ibi_line.Text));
                    Session["oImportsBLItems"] = oImportsBLItems;

                    dgvInvoiceDetails.EditIndex = -1;
                    dgvInvoiceDetails.DataSource = oImportsBLItems;
                    dgvInvoiceDetails.DataBind();
                    BindPInvoiceItems();

                    setCostValues();
                }
                else
                {
                    divWarning.Visible = true;
                    lblAlert.Text = "Error";
                }
                _isDel = false;
            }
            catch (Exception ex)
            {
                //Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void dgvInvoiceDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvInvoiceDetails.EditIndex = e.NewEditIndex;
            //dgvInvoiceDetails.DataBind();
            BindPInvoiceItems();

            GridViewRow row = dgvInvoiceDetails.Rows[e.NewEditIndex];
            TextBox txtIbi_itm_cd = (TextBox)row.FindControl("txtIbi_itm_cd");
            TextBox txtIbi_pi_no = (TextBox)row.FindControl("txtIbi_pi_no");
            Label lblIbi_pi_no2 = (Label)row.FindControl("lblIbi_pi_no2");
            DropDownList ddlSelectPriceBase = (DropDownList)row.FindControl("ddlSelectPriceBase");
            DropDownList ddlItemTag = (DropDownList)row.FindControl("ddlItemTag");


            LinkButton btnItemSearchGrid = row.FindControl("btnItemSearchGrid") as LinkButton;

            //if (!string.IsNullOrEmpty(txtIbi_pi_no.Text))
            //{
            //    txtIbi_itm_cd.ReadOnly = true;
            //    txtIbi_pi_no.ReadOnly = true;
            //    ddlSelectPriceBase.Enabled = false;
            //    ddlItemTag.Enabled = false;
            //}

            if (lblIbi_pi_no2.Text != "NewRecords")
            {
                txtIbi_itm_cd.ReadOnly = true;
                btnItemSearchGrid.Visible = false;
            }
            else
            {
                txtIbi_itm_cd.ReadOnly = false;
                btnItemSearchGrid.Visible = true;
            }
        }

        protected void dgvInvoiceDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void btnItemSearchGrid_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "Item";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show(); Session["IsSearch"] = true;
        }

        protected void chkSelectAllPis_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)dgvPerfomaInvoices.HeaderRow.FindControl("chkSelectAllPis");
            foreach (GridViewRow row in dgvPerfomaInvoices.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkSelectPIs");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
            mpPerfomaInvoices.Show();
        }

        protected void btnItemSearch_Click(object sender, EventArgs e)
        {
            if (chkItem.Checked == false)
            {
                ValidateTrue();
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "txtItem";
                result.Columns["MODEL"].SetOrdinal(0);
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpUserPopup.Show(); Session["IsSearch"] = true;
            }
            else
            {
                oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];
                if (oImportsBLItems != null)
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = CHNLSVC.CommonSearch.GetBLItems(SearchParams, null, null, oImportsBLItems.FirstOrDefault().Ibi_seq_no);
                    if (result.Rows.Count < 1)
                    {
                        result = CHNLSVC.CommonSearch.GetPIItems(SearchParams, null, null, hdfPiNo.Value);
                    }
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "txtBLItem";
                    result.Columns["MODEL"].SetOrdinal(0);
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    mpUserPopup.Show(); Session["IsSearch"] = true;
                }


                //grdOderItem.DataSource = _PIITEMBACKUP;
                //grdOderItem.DataBind();
                //OderplaneItempopoup.Show();
            }
        }


        protected void btnAddNewtems_Click(object sender, EventArgs e)
        {
            if (validateNewLine())
            {
                if (string.IsNullOrEmpty(txtExporter.Text.ToString()))
                {
                    DisplayMessage("Please Select Exporter", 2);
                    return;
                }
                //  bool _isDuplicate = false;
                string Finno = String.Empty;
                string kititem = String.Empty;
                string Pino = "NewRecords";
                int piline = 0;
                int KITline = 0;
                string projectname = string.Empty;
                string _item = txtItem.Text.ToString();
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemNew);
                DataTable _result = CHNLSVC.Inventory.GetSuppierItem(SearchParams, "ITEM", "%" + _item);
                if (_result.Rows.Count > 0)
                {
                    if (Session["oImportsBLItems"] != null)
                    {
                        oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];
                    }
                    else
                    {
                        oImportsBLItems = new List<ImportsBLItems>();
                    }
                    if (_PIITEMBACKUP != null)
                    {
                        if (_PIITEMBACKUP.Count > 0)
                        {
                            var _filter = _PIITEMBACKUP.Find(x => x.Ibi_itm_cd == _item);
                            if (_filter != null)
                            {
                                Finno = _filter.Ibi_fin_no;
                                Pino = _filter.Ibi_pi_no;
                                piline = _filter.Ibi_pi_line;
                                projectname = _filter.Ibi_project_name;
                                KITline = _filter.Ibi_kit_line;
                            }
                            else
                            {
                                Finno = _PIITEMBACKUP[0].Ibi_fin_no;
                                //Pino = _PIITEMBACKUP[0].Ibi_pi_no;
                                // piline = _PIITEMBACKUP[0].Ibi_pi_line;
                                piline = 0;
                            }
                        }
                    }


                    //if (oImportsBLItems.FindAll(x => x.Ibi_pi_no == "NewRecords" && x.Ibi_itm_cd == txtItem.Text.Trim().ToUpper() && x.Ibi_tp == ddlPriceType.SelectedValue.ToString() && x.Ibi_tag == ddlTag.SelectedValue.ToString()).Count > 0)
                    //{
                    //    DisplayMessage("This item is already added in the save Performa invoice", 2);
                    //    return;
                    //}

                    if (oImportsBLItems != null)
                    {
                        if (oImportsBLItems.Count > 0)
                        {
                            var _filter = oImportsBLItems.Find(x => x.Ibi_itm_cd == _item && x.Ibi_unit_rt == Convert.ToDecimal(txtUnitPrice.Text));
                            _isDuplicate = false;
                            if (_filter != null)
                            {

                                if (confresult2 == 0)
                                {
                                    lblconfimmsg.Text = "Already added following item do you want to update";
                                    Userconfmsg.Show();
                                    return;
                                }
                                else if (confresult2 == 1)
                                {
                                    _isDuplicate = true;
                                    _filter.Ibi_qty = Convert.ToDecimal(txtQty.Text) + _filter.Ibi_qty;
                                    _filter.Ibi_bal_qty = _filter.Ibi_qty;
                                    _filter.Ibi_anal_5 = (Convert.ToDecimal(txtQty.Text) * _filter.Ibi_unit_rt).ToString();
                                    confresult2 = 0;
                                }



                            }

                        }
                    }

                    if (!_isDuplicate)
                    {
                        confresult2 = 0;
                        MasterItem oMasterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim().ToUpper());
                        ImportsBLItems oOutput = new ImportsBLItems();
                        oOutput.Ibi_seq_no = oImportsBLItems.Count > 0 ? oImportsBLItems[0].Ibi_seq_no : 0;
                        oOutput.Ibi_line = 0;
                        oOutput.Ibi_doc_no = String.Empty;
                        oOutput.Ibi_ref_line = 0;
                        oOutput.Ibi_f_line = 0;
                        oOutput.Ibi_stus = 1;
                        oOutput.Ibi_itm_cd = txtItem.Text.ToUpper();
                        oOutput.Ibi_itm_stus = "GOD";// oMasterItem.Mi_itm_stus; 2016-01-20
                        oOutput.Ibi_hs_cd = oMasterItem.Mi_hs_cd;
                        oOutput.Ibi_model = oMasterItem.Mi_model;
                        oOutput.Ibi_tp = ddlPriceType.SelectedValue.ToString();
                        oOutput.Ibi_tp_Desc = ddlPriceType.SelectedItem.ToString();
                        oOutput.Ibi_tag = ddlTag.SelectedValue.ToString();                  // N-Normal , S-Special Project
                        oOutput.Ibi_tag_Desc = ddlTag.SelectedItem.ToString();             // N-Normal , S-Special Project
                        oOutput.Ibi_pi_unit_rt = 0;
                        //Lakshan
                        oOutput.Ibi_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
                        oOutput.Ibi_qty = Convert.ToDecimal(txtQty.Text);
                        oOutput.Ibi_bal_qty = Convert.ToDecimal(txtQty.Text);
                        oOutput.Ibi_fin_no = Finno;
                        oOutput.Ibi_pi_no = Pino;
                        oOutput.Ibi_pi_line = piline;
                        oOutput.Ibi_kit_line = KITline;
                        oOutput.Ibi_kit_itm_cd = kititem;
                        oOutput.Ibi_anal_1 = String.Empty;
                        oOutput.Ibi_anal_2 = String.Empty;
                        oOutput.Ibi_anal_3 = String.Empty;
                        oOutput.Ibi_anal_4 = String.Empty;
                        oOutput.Ibi_anal_5 = String.Empty;
                        oOutput.Ibi_cre_by = Session["UserID"].ToString();
                        oOutput.Ibi_cre_dt = DateTime.Now;
                        oOutput.Ibi_mod_by = Session["UserID"].ToString();
                        oOutput.Ibi_mod_dt = DateTime.Now;
                        oOutput.Ibi_session_id = Session["SessionID"].ToString();

                        oOutput.MI_SHORTDESC = oMasterItem.Mi_shortdesc;
                        oOutput.MI_COLOR_EXT = oMasterItem.Mi_color_ext;
                        oOutput.MI_ITM_TP = oMasterItem.Mi_itm_tp;
                        oOutput.MI_ITM_UOM = oMasterItem.Mi_itm_uom;
                        oOutput.MI_HS_CD = oMasterItem.Mi_hs_cd;
                        oOutput.Ibi_project_name = projectname;
                        oOutput.isNewItem = 1;
                        Int32 seqNo = 0;
                        string err = "";
                        seqNo = CHNLSVC.Inventory.GetBlItmMaxSeqNo(oOutput, out err) + 1;
                        // int _dd = CHNLSVC.Inventory.GetBlItmMaxSeqNo(oOutput, out err);
                        // oOutput.Ibi_anal_1 = (seqNo+1).ToString();
                        if (oImportsBLItems.Count == 0)
                        {
                            oOutput.Ibi_anal_1 = seqNo.ToString();
                        }
                        else
                        {
                            var _find = oImportsBLItems.Where(x => x.Ibi_fin_no == Finno).ToList();
                            if (_find.Count > 0)
                            {
                                oOutput.Ibi_anal_1 = (seqNo - 1).ToString();
                            }
                            else
                            {
                                oOutput.Ibi_anal_1 = seqNo.ToString();
                            }

                        }
                        #region add by lakshan 22Dec2017
                        if (_isExcUpload)
                        {
                            oOutput.Ibi_anal_1 = "1";
                        }
                        #endregion
                        Int32 LineNumber = 0;
                        Int32 ibi_line = 0;


                        if (oImportsBLItems.Count > 0)
                        {
                            LineNumber = oImportsBLItems.Max(x => x.Ibi_pi_line) + 1;
                            ibi_line = oImportsBLItems.Max(x => x.Ibi_line) + 1;
                        }
                        else
                        {
                            LineNumber = 1;
                            ibi_line = 1;
                        }

                        // oOutput.Ibi_pi_line = LineNumber;
                        oOutput.Ibi_line = ibi_line;
                        Int32 _maxLine = 0;
                        if (oImportsBLItems.Count > 0)
                        {
                            _maxLine = oImportsBLItems.Max(x => x.Tmp_ibi_line);
                        }
                        oOutput.Tmp_ibi_line = _maxLine + 1;
                        oImportsBLItems.Add(oOutput);
                    }
                    Session["oImportsBLItems"] = oImportsBLItems;
                    if (!_bindGrid)
                    {
                        BindPInvoiceItems();
                    } 
                    clearEntryLine();
                    setCostValues();
                    BindCostItems();
                }
                else
                {
                    string msg = "Please allocate this item : " + _item + " for Exporter";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                }



            }
        }

        protected void lbtnedititem_Click(object sender, EventArgs e)
        {
            Session["IsSearch"] = "false";
        }

        protected void btnConsignee_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
            DataTable result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "Company";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show(); Session["IsSearch"] = true;
        }

        protected void btnDeclarant_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Declarant);
            DataTable result = CHNLSVC.CommonSearch.GetAgents(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "Declarant";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show(); Session["IsSearch"] = true;
        }

        protected void btnExporter_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
            DataTable result = CHNLSVC.CommonSearch.GetSupplierData(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "Supplier";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show(); Session["IsSearch"] = true;
        }

        protected void btnAgent_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
            DataTable result = CHNLSVC.CommonSearch.GetAgents(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "ImportAgent";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show(); Session["IsSearch"] = true;
        }

        protected void btnInvoiceAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                DisplayMessage("please enter an invoice number", 2);
                return;
            }
            if (String.IsNullOrEmpty(txtInvoiceDate.Text))
            {
                DisplayMessage("please enter a invoice date", 2);
                return;
            }

            if (Session["OImportsBLSInvoice"] == null)
            {
                OImportsBLSInvoice = new List<ImportsBLSInvoice>();
            }
            else
            {
                OImportsBLSInvoice = (List<ImportsBLSInvoice>)Session["OImportsBLSInvoice"]; ;
            }

            ImportsBLSInvoice oNewItems = new ImportsBLSInvoice();
            oNewItems.Ibs_si_no = txtInvoiceNo.Text.Trim();
            oNewItems.Ibs_dt = Convert.ToDateTime(txtInvoiceDate.Text);
            oNewItems.Ibs_act = 1;

            if (OImportsBLSInvoice.FindAll(x => x.Ibs_si_no == txtInvoiceNo.Text.Trim() && x.Ibs_act == 1).Count > 0)
            {
                DisplayMessage("Invoice is already added", 2);
                return;
            }
            else
            {
                OImportsBLSInvoice.Add(oNewItems);
                Session["OImportsBLSInvoice"] = OImportsBLSInvoice;
                BindSInvoices();
            }
            cleaINvoiceEntyLine();
        }

        protected void btnContainerAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContainerNo.Text))
            {
                DisplayMessage("Please enter a Container number", 2);
                return;
            }
            if (ddlContainersType.SelectedIndex == 0)
            {
                DisplayMessage("Please select a Container type", 2);
                return;
            }

            if (Session["oImportsBLContainers"] == null)
            {
                oImportsBLContainers = new List<ImportsBLContainer>();
            }
            else
            {
                oImportsBLContainers = (List<ImportsBLContainer>)Session["oImportsBLContainers"]; ;
            }
            ImportsBLContainer oNewItem = new ImportsBLContainer();
            oNewItem.Ibc_desc = txtContainerNo.Text.Trim();
            oNewItem.Ibc_tp = ddlContainersType.SelectedValue.ToString();
            oNewItem.Ibc_act = 1;

            //if (oImportsBLContainers.FindAll(x => x.Ibc_desc == txtContainerNo.Text.Trim() && x.Ibc_tp == ddlContainersType.SelectedValue.ToString() && x.Ibc_act == 1).Count > 0)
            if (oImportsBLContainers.FindAll(x => x.Ibc_desc == txtContainerNo.Text.Trim() && x.Ibc_act == 1).Count > 0)
            {
                DisplayMessage("Container is already added.", 2);
                return;
            }
            else
            {
                oImportsBLContainers.Add(oNewItem);
                Session["oImportsBLContainers"] = oImportsBLContainers;
                BindContainers();

                txtContainerNo.Text = "";
                ddlContainersType.SelectedIndex = 0;
            }
        }

        protected void ddlTradeTerms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTradeTerms.SelectedIndex != 0)
            {
                //if (isNewRecord.Value == "1")
                {
                    Session["oImportsBLCosts"] = null;
                    List<MST_COST_ELE> oMST_COST_ELE = CHNLSVC.Financial.GET_COST_ELE_DATA(new MST_COST_ELE()
                    {
                        Mcae_com = Session["UserCompanyCode"].ToString(),
                        Mcae_ele_tp = ddlTradeTerms.SelectedValue.ToString(),
                        Mcae_ele_cat = "TOT"
                    });
                    if (oMST_COST_ELE != null)
                    {
                        if (Session["oImportsBLCosts"] != null)
                        {
                            oImportsBLCosts = (List<ImportsBLCost>)Session["oImportsBLCosts"];
                        }
                        else
                        {
                            oImportsBLCosts = new List<ImportsBLCost>();

                            foreach (MST_COST_ELE item in oMST_COST_ELE)
                            {
                                ImportsBLCost oImportsBLCost = new ImportsBLCost();
                                oImportsBLCost.Ibcs_ele_cat = item.Mcae_ele_cat;
                                oImportsBLCost.Ibcs_ele_tp = item.Mcae_ele_tp;
                                oImportsBLCost.Ibcs_ele_cd = item.Mcae_cd;
                                oImportsBLCost.Ibcs_act = 1;
                                oImportsBLCosts.Add(oImportsBLCost);
                            }
                        }
                        Session["oImportsBLCosts"] = oImportsBLCosts;

                        setCostValues();

                        BindCostItems();
                        //ddlTradeTerms.Enabled = false;
                    }
                    else
                    {
                        oImportsBLCosts = new List<ImportsBLCost>();
                        Session["oImportsBLCosts"] = oImportsBLCosts;
                        setCostValues();
                        BindCostItems();
                    }
                }
            }
        }

        protected void OnUpdateCost(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                //GridViewRow row = dgvCostItems.Rows[e..RowIndex];
                Label lblIbcs_ele_cd = (Label)row.FindControl("lblIbcs_ele_cd");
                TextBox txtIbcs_amt = (TextBox)row.FindControl("txtIbcs_amt");

                if (Session["oImportsBLCosts"] != null)
                {
                    oImportsBLCosts = (List<ImportsBLCost>)Session["oImportsBLCosts"];
                    if (!isdecimal(txtIbcs_amt.Text))
                    {
                        DisplayMessage("Please enter valid amount", 2);
                        return;
                    }
                    ImportsBLCost oEditedItem = oImportsBLCosts.Find(x => x.Ibcs_ele_cd == lblIbcs_ele_cd.Text);
                    oEditedItem.Ibcs_amt = (txtIbcs_amt.Text == "") ? 0 : Convert.ToDecimal(txtIbcs_amt.Text);
                    Session["oImportsBLCosts"] = oImportsBLCosts;
                    BindCostItems();
                    dgvCostItems.EditIndex = -1;
                    setCostValues();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
            BindCostItems();
        }

        protected void OnCancelCost(object sender, EventArgs e)
        {
            try
            {
                dgvCostItems.EditIndex = -1;
                BindCostItems();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void dgvCostItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            dgvCostItems.EditIndex = -1;
            BindCostItems();
        }

        protected void dgvCostItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            dgvCostItems.EditIndex = e.NewEditIndex;
            // BindCostItems();

            GridViewRow dr = dgvCostItems.Rows[e.NewEditIndex];
            Label lblIbcs_ele_cd = dr.FindControl("lblIbcs_ele_cd") as Label;
            TextBox txtIbcs_amt = dr.FindControl("txtIbcs_amt") as TextBox;

            if (lblIbcs_ele_cd.Text.ToUpper() == "COST")
            {
                //txtIbcs_amt.ReadOnly = true;
                dgvCostItems.EditIndex = -1;
                return;
            }
            BindCostItems();
        }

        protected void dgvCostItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void dgvCostItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void btnDocNumber_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BLHeader);
            DataTable result = CHNLSVC.CommonSearch.SearchBLHeader(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "BLHeader";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show(); Session["IsSearch"] = true;
        }

        protected void btnRecDelete_Click(object sender, EventArgs e)
        {

        }

        protected void btnContaiDelete_Click(object sender, EventArgs e)
        {

        }

        protected void dgvContainers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                if (hdfDelConta.Value == "No")
                {
                    return;
                }

                GridViewRow row = dgvContainers.Rows[e.RowIndex];
                Label lblIbc_tp = (Label)row.FindControl("lblIbc_tp");
                Label lblIbc_desc = (Label)row.FindControl("lblIbc_desc");


                if (Session["oImportsBLContainers"] != null)
                {
                    oImportsBLContainers = (List<ImportsBLContainer>)Session["oImportsBLContainers"]; ;

                    ImportsBLContainer oNewItem = oImportsBLContainers.Find(x => x.Ibc_desc == lblIbc_desc.Text && x.Ibc_tp == lblIbc_tp.Text);
                    if (oNewItem == null)
                    {
                        return;
                    }
                    oNewItem.Ibc_act = 0;
                    Session["oImportsBLContainers"] = oImportsBLContainers;

                    BindContainers();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void dgvInvoiceNums_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (hdfDelSI.Value == "Yes")
                {
                    GridViewRow row = dgvInvoiceNums.Rows[e.RowIndex];
                    Label lblIBS_SI_NO = (Label)row.FindControl("lblIBS_SI_NO");
                    Label lblIBS_DT = (Label)row.FindControl("lblIBS_DT");
                    Label lblIBS_ACT = (Label)row.FindControl("lblIBS_ACT");

                    if (Session["OImportsBLSInvoice"] != null)
                    {
                        OImportsBLSInvoice = (List<ImportsBLSInvoice>)Session["OImportsBLSInvoice"]; ;

                        ImportsBLSInvoice oNewItem = OImportsBLSInvoice.Find(x => x.Ibs_si_no == lblIBS_SI_NO.Text && x.Ibs_dt == Convert.ToDateTime(lblIBS_DT.Text).Date);
                        if (oNewItem == null)
                        {
                            return;
                        }
                        oNewItem.Ibs_act = 0;
                        Session["OImportsBLSInvoice"] = OImportsBLSInvoice;

                        BindSInvoices();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void chkTotalFOCShipment_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvFinancialDocs.Rows.Count > 0)
            {
                DisplayMessage("Please clear the screen.", 2);
                return;
            }
            else
            {
                if (chkTotalFOCShipment.Checked)
                {
                    ddlPriceType.SelectedIndex = ddlPriceType.Items.IndexOf(ddlPriceType.Items.FindByText("FOC"));
                    ddlPriceType.Enabled = false;
                    //txtFinancialDocumentNo.Enabled = false;
                    btnFinancialDocumentNo.Enabled = false;
                    btnFinancialDocumentNoView.Enabled = false;
                }
                else
                {
                    ddlPriceType.SelectedIndex = 0;
                    ddlPriceType.Enabled = true;
                    //txtFinancialDocumentNo.Enabled = true;
                    btnFinancialDocumentNo.Enabled = true;
                    btnFinancialDocumentNoView.Enabled = true;
                }
            }
        }

        protected void btnCountyOfOrigin_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
            DataTable result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, null, null);

            result.Columns["Code"].SetOrdinal(1);
            result.Columns["Description"].SetOrdinal(0);

            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "CountyOfOrigin";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show(); Session["IsSearch"] = true;
        }

        protected void btnExporterCounty_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
            DataTable result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, null, null);
            result.Columns["Code"].SetOrdinal(1);
            result.Columns["Description"].SetOrdinal(0);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "ExporterCounty";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show(); Session["IsSearch"] = true;
        }

        protected void dgvCostItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                try
                {
                    dgvCostItems.EditIndex = -1;
                    BindCostItems();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void lbtnedititem_Click1(object sender, EventArgs e)
        {

        }

        #region TextChanged

        protected void txtFinancialDocumentNo_TextChanged(object sender, EventArgs e)
        {
            /* Lakshan 2016/02/17 */
            if (string.IsNullOrEmpty(txtFinancialDocumentNo.Text))
            {
                DisplayMessage("Please enter a financial document no", 2); txtFinancialDocumentNo.Text = ""; txtFinancialDocumentNo.Focus(); return;
            }
            if (!string.IsNullOrEmpty(txtFinancialDocumentNo.Text))
            {
                Order_Financing oFinalcialHeader = CHNLSVC.Financial.GET_IMP_FIN_HDR_BY_DOC(Session["UserCompanyCode"].ToString(), txtFinancialDocumentNo.Text.Trim());
                if (oFinalcialHeader == null)
                {
                    DisplayMessage("Please enter a valid financial document no", 2); txtFinancialDocumentNo.Text = ""; txtFinancialDocumentNo.Focus(); return;
                }
                else
                {

                }
            }
            btnFinancialDocumentNoView_Click(null, null);
            //txtFinancialDocumentNo.Focus();
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                _tagChgRestrict = false;
                _tagChgRestrictVal = "";
                bool b2 = false;
                string toolTip = "";

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, "ITEM", txtItem.Text.Trim().ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["ITEM"].ToString()))
                    {
                        if (txtItem.Text.ToUpper() == row["ITEM"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            break;
                        }
                    }
                }
                if (b2)
                {
                    txtItem.ToolTip = toolTip;
                    MasterItem oMasterItem = CHNLSVC.General.GetItemMaster(txtItem.Text.Trim().ToUpper());
                    if (oMasterItem != null && oMasterItem.Mi_cd != null)
                    {
                        lblDescription.Text = oMasterItem.Mi_shortdesc;
                        lblBrand.Text = oMasterItem.Mi_brand;
                        lblUOM.Text = oMasterItem.Mi_itm_uom;

                        txtQty.Focus();
                        if (_PIITEMBACKUP != null)
                        {
                            var _filter = _PIITEMBACKUP.Find(x => x.Ibi_itm_cd == txtItem.Text.Trim().ToUpper());
                            if (_filter != null)
                            {
                                txtQty.Text = _filter.Ibi_qty.ToString();
                                txtUnitPrice.Text = _filter.Ibi_unit_rt.ToString();
                                txtUnitTotal.Text = (_filter.Ibi_qty * _filter.Ibi_unit_rt).ToString();
                                ddlTag.SelectedIndex = ddlTag.Items.IndexOf(ddlTag.Items.FindByValue(_filter.Ibi_tag));
                                _tagChgRestrict = true;
                                _tagChgRestrictVal = ddlTag.SelectedValue;
                            }
                            else
                            {
                                if (_OrderPlanItemBackup != null)
                                {
                                    var _filter2 = _OrderPlanItemBackup.Find(x => x.IOI_ITM_CD == txtItem.Text.Trim().ToUpper());
                                    if (_filter2 != null)
                                    {
                                        txtQty.Text = _filter2.IOI_QTY.ToString();
                                        txtUnitPrice.Text = _filter2.IOI_UNIT_RT.ToString();
                                        txtUnitTotal.Text = (_filter2.IOI_QTY * _filter2.IOI_UNIT_RT).ToString();
                                        ddlTag.SelectedIndex = ddlTag.Items.IndexOf(ddlTag.Items.FindByValue(_filter2.IOI_TAG));
                                        _tagChgRestrict = true;
                                        _tagChgRestrictVal = ddlTag.SelectedValue;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (_OrderPlanItemBackup != null)
                            {
                                var _filter = _OrderPlanItemBackup.Find(x => x.IOI_ITM_CD == txtItem.Text.Trim().ToUpper());
                                if (_filter != null)
                                {
                                    txtQty.Text = _filter.IOI_QTY.ToString();
                                    txtUnitPrice.Text = _filter.IOI_UNIT_RT.ToString();
                                    txtUnitTotal.Text = (_filter.IOI_QTY * _filter.IOI_UNIT_RT).ToString();
                                    ddlTag.SelectedIndex = ddlTag.Items.IndexOf(ddlTag.Items.FindByValue(_filter.IOI_TAG));
                                    _tagChgRestrict = true;
                                    _tagChgRestrictVal = ddlTag.SelectedValue;
                                }
                            }
                        }




                    }
                    else
                    {
                        txtItem.ToolTip = "";
                        txtItem.Text = "";
                        txtItem.Focus();
                        DisplayMessage("Please enter a valid item code", 2);
                        return;
                    }
                }
            }
            else
            {
                txtItem.ToolTip = "";
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            calUnitTotal();
            ddlPriceType.Focus();
            decimal val = Convert.ToDecimal(txtQty.Text.ToString());

            if (val%1 != 0)
            {
                DisplayMessage("Decimal Qty Added!", 2);
            }
          
        }

        protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            decimal values;

            if (!decimal.TryParse(txtUnitPrice.Text, out values))
            {
                DisplayMessage("Enter valid amount", 5);
                txtUnitPrice.Focus();
                return;
            }

            calUnitTotal();
            ddlTag.Focus();
        }

        protected void txtExporter_TextChanged(object sender, EventArgs e)
        {
            loadExportCurCode();
            txtExporter.Focus();

            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TradeTerms);
            DataTable result = CHNLSVC.Financial.GetSupplierPorts(Session["UserCompanyCode"].ToString(), txtExporter.Text);
            ddlFromPort.DataSource = result;
            ddlFromPort.DataTextField = "MP_NAME";
            ddlFromPort.DataValueField = "mspr_frm_port";
            ddlFromPort.DataBind();
            ddlFromPort.Items.Insert(0, new ListItem(" - - Select - - ", "0"));
            txtETD.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
            loadFromPorts();
        }

        protected void txtDocNumber_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDocNumber.Text))
            {
                LoadDocDetails();
            }
            txtDocNumber.Focus();
        }

        protected void txtConsignee_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtConsignee.Text))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, "CODE", txtConsignee.Text);
                if (result.Rows.Count > 0)
                {
                    txtConsigneeDesc.Text = result.Rows[0][1].ToString();
                }
                else
                {
                    DisplayMessage("Please enter a valid consignee code", 2);
                    txtConsignee.Text = "";
                    txtConsignee.Focus();
                    txtConsigneeDesc.Text = "";
                    //txtConsignee.Focus();
                    return;
                }
            }
            //txtConsignee.Focus();
        }

        protected void txtDeclarantDesc_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtDeclarant_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDeclarant.Text))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Declarant);
                DataTable result = CHNLSVC.CommonSearch.GetAgents(SearchParams, "CODE", txtDeclarant.Text);
                if (result.Rows.Count > 0)
                {
                    txtDeclarant.ToolTip = result.Rows[0][1].ToString();
                }
                else
                {
                    DisplayMessage("Please enter a valid declarant code", 2);
                    txtDeclarant.Text = "";
                    txtDeclarant.Focus();
                    txtDeclarant.ToolTip = "";
                    //txtDeclarant.Focus();
                    return;
                }
            }
            //txtDeclarant.Focus();
        }

        protected void txtAgent_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAgent.Text))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                DataTable result = CHNLSVC.CommonSearch.GetAgents(SearchParams, "CODE", txtAgent.Text);
                if (result.Rows.Count > 0)
                {
                    txtAgent.ToolTip = result.Rows[0][1].ToString();
                }
                else
                {
                    DisplayMessage("Please enter a valid agent code", 2);
                    txtAgent.Text = "";
                    txtAgent.ToolTip = "";
                    txtAgent.Focus();
                    return;
                }
            }
            //txtAgent.Focus();
        }

        protected void txtCountyOfOrigin_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCountyOfOrigin.Text))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                DataTable result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, "CODE", txtCountyOfOrigin.Text);
                if (result.Rows.Count > 0)
                {
                    txtCountyOfOrigin.ToolTip = result.Rows[0][1].ToString();
                }
                else
                {
                    DisplayMessage("Please enter a country code for origin", 2);
                    txtCountyOfOrigin.Focus();
                    txtCountyOfOrigin.Text = "";
                    return;
                }
            }
            //txtCountyOfOrigin.Focus();
        }

        protected void txtExporterCounty_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtExporterCounty.Text))
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ImportAgent);
                DataTable result = CHNLSVC.CommonSearch.GetCountrySearchData(SearchParams, "CODE", txtExporterCounty.Text);
                if (result.Rows.Count > 0)
                {
                    txtExporterCounty.ToolTip = result.Rows[0][1].ToString();
                }
                else
                {
                    DisplayMessage("Please enter a country code for exporter", 2);
                    txtExporterCounty.Text = "";
                    txtExporterCounty.Focus();
                    return;
                }
            }
            //txtExporterCounty.Focus();
        }


        #endregion

        #region  Methods

        private void ValidateTrue()
        {
            divWarning.Visible = false;
            lblWarning.Text = "";
            divSuccess.Visible = false;
            lblSuccess.Text = "";
            divAlert.Visible = false;
            lblAlert.Text = "";
        }

        private void clear()
        {
            _selectedPiNo = "";
            _selectedPiLine=0;
            _showItmCdChg = false;
            _isExcUpload = false;
            _bindGrid = false;
            _tagChgRestrict = false;
            _tagChgRestrictVal = "";
            _isDuplicate = false;
            _PIITEMBACKUP = new List<ImportsBLItems>();
            _OrderPlanItemBackup = new List<OrderPlanItem>();
            Session["_confresult2"] = 0;
            confresult = false;
            Session["CreditP"] = 0;
            Session["IsSearch"] = null;
            btnSave.Visible = true;
            txtDocNumber.Text = "";
            txtPackingListNo.Text = "";
            txtBLNumber.Text = "";
            txtReferenceNum.Text = "";
            txtShipmentDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtDocReceivedDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtDocReceivedDate.Text = "";
            txtConsignee.Text = "";
            txtConsigneeDesc.Text = "";
            txtDeclarant.Text = Session["UserCompanyCode"].ToString();
            //txtDeclarantDesc.Text = "";
            txtExporter.Text = "";
            //txtExporterDesc.Text = "";
            txtAgent.Text = "";
            //txtAgentDesc.Text = "";
            txtTotalPackages.Text = "";
            txtVessel.Text = "";
            txtVoyage.Text = "";
            txtCountyOfOrigin.Text = "";
            txtExporterCounty.Text = "";
            txtPlaceOfLanding.Text = "";
            txtETA.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtETD.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            DateTime orddate = DateTime.Now;
            txtissuedate.Text = orddate.ToString("dd/MMM/yyyy");
            txtexDate.Text = orddate.ToString("dd/MMM/yyyy");
            txtCancel.Text = orddate.ToString("dd/MMM/yyyy");


            txtlocationOfGoods.Text = "";

            dgvFinancialDocs.DataSource = new int[] { };
            dgvFinancialDocs.DataBind();

            dgvInvoiceDetails.DataSource = new int[] { };
            dgvInvoiceDetails.DataBind();

            dgvInvoiceNums.DataSource = new int[] { };
            dgvInvoiceNums.DataBind();

            dgvContainers.DataSource = new int[] { };
            dgvContainers.DataBind();

            dgvPerfomaInvoices.DataSource = new int[] { };
            dgvPerfomaInvoices.DataBind();

            dgvCostItems.DataSource = new int[] { };
            dgvCostItems.DataBind();

            chkBypassEntry.Checked = false;
            chkTotalFOCShipment.Checked = false;


            txtFinancialDocumentNo.Text = "";
            txtInvoiceNo.Text = "";
            txtContainerNo.Text = "";
            ddlContainersType.SelectedIndex = 0;
            txtItem.Text = "";
            txtQty.Text = "";
            ddlPriceType.SelectedIndex = 0;
            if (chkTotalFOCShipment.Checked)
            {
                ddlPriceType.SelectedIndex = ddlPriceType.Items.IndexOf(ddlPriceType.Items.FindByText("FOC"));
            }
            else
            {
                ddlPriceType.SelectedIndex = ddlPriceType.Items.IndexOf(ddlPriceType.Items.FindByText("Charge"));
            }

            ddlTag.SelectedIndex = 0;
            txtUnitPrice.Text = "";
            txtUnitTotal.Text = "";
            txtTotalOrderQty.Text = "";
            txtTotalOrderValueIn.Text = "";

            Session["oFinHeaders"] = null;
            Session["oImportsBLItems"] = null;
            Session["OImportsBLSInvoice"] = null;
            Session["oImportsBLContainers"] = null;
            Session["oImportsBLCosts"] = null;
            Session["oHeader"] = null;

            lblDescription.Text = "";
            lblBrand.Text = "";
            lblUOM.Text = "";

            loadTrareTerms();
            loadContainerType();
            loadToPorts();
            loadFromPorts();

            txtInvoiceDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtInvoiceNo.Text = "";
            txtRemark.Text = "";

            chkBypassEntry.Checked = false;
            chkTotalFOCShipment.Checked = false;

            lblCurCode.Text = "";
            isNewRecord.Value = "1";
            txtDocNumber.Focus();
            ddlTradeTerms.Enabled = true;

            txtCurrenyCode.Text = "";

            txtStatus.Text = "";
            hdfAppro.Value = null;
            hdfCancel.Value = null;
            setbtnenable(true);

            txtConsignee.Text = Session["UserCompanyCode"].ToString();
            txtConsignee_TextChanged(null, null);

            loadUOM();

            ddlUOM.SelectedIndex = 0;
            ddlFinalPort.SelectedIndex = 0;
            if (ddlFinalPort.Items.Count > 1)
            {
                ddlFinalPort.SelectedIndex = 1;
            }

            txtCreditAllowDate.Text = "";
            txtShSeq.Text = "";

            loadCarryTyps();
            ddlModeofShipment.SelectedIndex = 0;
            ddlCaringType.SelectedIndex = 0;

            ddlTag.SelectedIndex = 1;
            if (chkTotalFOCShipment.Checked)
            {
                ddlPriceType.SelectedIndex = ddlPriceType.Items.IndexOf(ddlPriceType.Items.FindByText("FOC"));
            }
        }

        private void GetPerfomaInvoices(String DocNum)
        {
            List<PIOrderFinancing> oPIOrderFinancings = CHNLSVC.Financial.GET_IMP_FIN_PI_BY_DOC(DocNum);
            if (oPIOrderFinancings != null && oPIOrderFinancings.Count > 0)
            {
                dgvPerfomaInvoices.DataSource = oPIOrderFinancings;
                dgvPerfomaInvoices.DataBind();
            }
            else
            {
                dgvPerfomaInvoices.DataSource = new int[] { };
                dgvPerfomaInvoices.DataBind();
            }
        }

        private void BindFinHeaders()
        {
            if (Session["oFinHeaders"] != null)
            {
                oFinHeaders = (List<Order_Financing>)Session["oFinHeaders"];
                dgvFinancialDocs.DataSource = oFinHeaders;
            }
            else
            {
                dgvFinancialDocs.DataSource = new int[] { };
            }
            dgvFinancialDocs.DataBind();
        }

        private bool isInvoiceSelect()
        {
            bool status = false;

            for (int i = 0; i < dgvPerfomaInvoices.Rows.Count; i++)
            {
                GridViewRow row = (GridViewRow)dgvPerfomaInvoices.Rows[i];
                CheckBox cb1 = (CheckBox)row.FindControl("chkSelectPIs");
                if (cb1.Checked)
                {
                    status = true;
                    break;
                }
            }

            return status;
        }

        private void AddInvoiceItems(List<ImportsBLItems> newItems)
        {
            if (Session["oImportsBLItems"] != null)
            {
                oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];
            }
            else
            {
                oImportsBLItems = new List<ImportsBLItems>();
            }

            foreach (ImportsBLItems newItem in newItems)
            {
                if (oImportsBLItems.FindAll(x => x.Ibi_pi_no == newItem.Ibi_pi_no && x.Ibi_pi_line == newItem.Ibi_pi_line).Count > 0)
                {
                    oImportsBLItems.RemoveAll(x => x.Ibi_pi_no == newItem.Ibi_pi_no && x.Ibi_pi_line == newItem.Ibi_pi_line);
                }
                Int32 _maxLine = 0;
                if (oImportsBLItems.Count > 0)
                {
                    _maxLine = oImportsBLItems.Max(x => x.Tmp_ibi_line);
                }
                newItem.Tmp_ibi_line = _maxLine + 1;
                oImportsBLItems.Add(newItem);
            }
            Session["oImportsBLItems"] = oImportsBLItems.Where(x => x.Ibi_bal_qty != 0).ToList();
            setCostValues();
        }

        private void BindPInvoiceItems()
        {
            txtShSeq.Text = "";
            dgvInvoiceDetails.DataSource = new int[] { };
            dgvInvoiceDetails.DataBind();
            if (Session["oImportsBLItems"] != null)
            {
                oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];

                oImportsBLItems.ForEach(x => x.Ibi_anal_5 = (x.Ibi_qty * x.Ibi_unit_rt).ToString());
                oImportsBLItems = oImportsBLItems.OrderBy(a => a.Ibi_line).ToList();
                dgvInvoiceDetails.DataSource = oImportsBLItems;

                if (oImportsBLItems != null)
                {
                    if (oImportsBLItems.Count > 0)
                    {
                        txtShSeq.Text = oImportsBLItems.Max(d => d.Ibi_anal_1).ToString();
                    }
                }
            }
            else
            {
                dgvInvoiceDetails.DataSource = new int[] { };
            }
            dgvInvoiceDetails.DataBind();
            MasterItem _mstItm = new MasterItem();
            for (int i = 0; i < dgvInvoiceDetails.Rows.Count; i++)
            {
                GridViewRow row = (GridViewRow)dgvInvoiceDetails.Rows[i];
                Label lblIbi_stus = (Label)row.FindControl("lblIbi_stus");
                Label lblIbi_pi_no = (Label)row.FindControl("lblIbi_pi_no");
                TextBox txtIbi_pi_no = (TextBox)row.FindControl("txtIbi_pi_no");
                Label lblIbi_anal_5 = (Label)row.FindControl("lblIbi_anal_5");
                Label lblibi_tp = (Label)row.FindControl("lblIbi_tp");
                Label lblIbi_tp_Desc = (Label)row.FindControl("lblIbi_tp_Desc");
                Label lblIbi_tag_Desc = (Label)row.FindControl("lblIbi_tag_Desc");
                Label lblIbi_tag = (Label)row.FindControl("lblIbi_tag");
                Label lblIbi_itm_cd = (Label)row.FindControl("lblIbi_itm_cd");
                LinkButton lbtnItemChg = (LinkButton)row.FindControl("lbtnItemChg");

                if (lblIbi_pi_no != null && lblIbi_pi_no.Text == "NewRecords")
                {
                    lblIbi_pi_no.Visible = false;
                }
                if (txtIbi_pi_no != null && txtIbi_pi_no.Text == "NewRecords")
                {
                    txtIbi_pi_no.Visible = false;
                }

                if (lblIbi_stus.Text == "0" && !_isDel)
                {
                    row.Visible = false;
                }

                if (lblIbi_tp_Desc != null)
                {
                    if (lblibi_tp.Text == "C")
                    {
                        lblIbi_tp_Desc.Text = "Charge";
                    }
                    else
                    {
                        lblIbi_tp_Desc.Text = "FOC";
                    }
                }

                if (lblIbi_tag_Desc != null)
                {
                    if (lblIbi_tag.Text == "S")
                    {
                        lblIbi_tag_Desc.Text = "Special Project";
                    }
                    else
                    {
                        lblIbi_tag_Desc.Text = "General";
                    }
                }

                lblIbi_anal_5.Text = Convert.ToDecimal(lblIbi_anal_5.Text).ToString("N5");
                if (lblIbi_itm_cd != null)
                {
                    if (!string.IsNullOrEmpty(lblIbi_itm_cd.Text))
                    {
                        _mstItm = CHNLSVC.General.GetItemMaster(lblIbi_itm_cd.Text.Trim().ToUpper());
                        if (_mstItm == null)
                        {
                            lblIbi_itm_cd.Visible = false;
                            lbtnItemChg.Visible = true;
                        }
                        else
                        {
                            lblIbi_itm_cd.Visible = true;
                            lbtnItemChg.Visible = false;
                        }
                    }
                }
                else
                {
                    lbtnItemChg.Visible = false;
                }
            }
            calculateTotals();

        }

        private void BindSInvoices()
        {
            if (Session["OImportsBLSInvoice"] != null)
            {
                OImportsBLSInvoice = (List<ImportsBLSInvoice>)Session["OImportsBLSInvoice"];
                dgvInvoiceNums.DataSource = OImportsBLSInvoice;
            }
            else
            {
                dgvInvoiceNums.DataSource = new int[] { };
            }
            dgvInvoiceNums.DataBind();

            for (int i = 0; i < dgvInvoiceNums.Rows.Count; i++)
            {
                GridViewRow row = (GridViewRow)dgvInvoiceNums.Rows[i];
                Label lblIBS_ACT = (Label)row.FindControl("lblIBS_ACT");
                Label lblIBS_DT = (Label)row.FindControl("lblIBS_DT");
                if (lblIBS_ACT.Text == "0")
                {
                    row.Visible = false;
                }
                lblIBS_DT.Text = Convert.ToDateTime(lblIBS_DT.Text).ToString("dd/MMM/yyyy");
            }
        }

        private void BindContainers()
        {
            dgvContainers.DataSource = new int[] { };
            dgvContainers.DataBind();
            if (Session["oImportsBLContainers"] != null)
            {
                oImportsBLContainers = (List<ImportsBLContainer>)Session["oImportsBLContainers"];
                dgvContainers.DataSource = oImportsBLContainers;
            }
            else
            {
                dgvContainers.DataSource = new int[] { };
            }

            dgvContainers.DataBind();

            for (int i = 0; i < dgvContainers.Rows.Count; i++)
            {
                GridViewRow row = dgvContainers.Rows[i];
                Label lblIbc_act = (Label)row.FindControl("lblIbc_act");
                if (lblIbc_act.Text == "0")
                {
                    row.Visible = false;
                }
            }
        }

        private void BindCostItems()
        {
            dgvCostItems.DataSource = new int[] { };
            dgvCostItems.DataBind();
            if (Session["oImportsBLCosts"] != null)
            {
                oImportsBLCosts = (List<ImportsBLCost>)Session["oImportsBLCosts"];
                oImportsBLCosts = oImportsBLCosts.OrderBy(a => a.Ibcs_ele_cd).ToList();
                dgvCostItems.DataSource = oImportsBLCosts;
                dgvCostItems.DataBind();
            }
            else
            {
                dgvCostItems.DataSource = new int[] { };
                dgvCostItems.DataBind();
            }
            dgvCostItems.DataBind();

            for (int i = 0; i < dgvCostItems.Rows.Count; i++)
            {
                GridViewRow row = dgvCostItems.Rows[i];
                Label lblibcs_act = (Label)row.FindControl("lblibcs_act");
                Label lblIbcs_ele_cd = (Label)row.FindControl("lblIbcs_ele_cd");
                LinkButton lbtnedititem = (LinkButton)row.FindControl("lbtnedititem");


                if (lblibcs_act.Text == "0")
                {
                    row.Visible = false;
                }

                if (lblIbcs_ele_cd.Text == "COST")
                {
                    lbtnedititem.Visible = false;
                }
            }
        }

        private ImportsBLItems PIToBL(ImportPIDetails oInput)
        {
            ImportsBLItems oOutput = new ImportsBLItems();
            oOutput.Ibi_seq_no = 0;
            oOutput.Ibi_line = 0;
            oOutput.Ibi_doc_no = String.Empty;
            oOutput.Ibi_ref_line = oInput.IPI_REF_LINE;
            oOutput.Ibi_f_line = oInput.IPI_F_LINE;
            oOutput.Ibi_stus = oInput.IPI_STUS;
            oOutput.Ibi_itm_cd = oInput.IPI_ITM_CD;
            oOutput.Ibi_itm_stus = oInput.IPI_ITM_STUS;
            oOutput.Ibi_hs_cd = oInput.MI_HS_CD;
            oOutput.Ibi_model = oInput.IPI_MODEL;
            oOutput.Ibi_tp = "C";
            oOutput.Ibi_tp_Desc = "Charge";
            oOutput.Ibi_tag = "N";                         // N-Normal , S-Special Project
            oOutput.Ibi_tag_Desc = "General";                         // N-Normal , S-Special Project
            oOutput.Ibi_pi_unit_rt = oInput.IPI_UNIT_RT;
            oOutput.Ibi_unit_rt = oInput.IPI_UNIT_RT;
            //oOutput.Ibi_qty = oInput.IPI_QTY;
            oOutput.Ibi_qty = oInput.IPI_BAL_QTY;
            oOutput.Ibi_bal_qty = oInput.IPI_BAL_QTY;
            oOutput.Ibi_fin_no = Session["lblif_doc_no"].ToString();
            oOutput.Ibi_pi_no = oInput.IPI_PI_NO;
            oOutput.Ibi_pi_line = oInput.IPI_LINE;
            oOutput.Ibi_kit_line = oInput.IPI_KIT_LINE;
            oOutput.Ibi_kit_itm_cd = oInput.IPI_KIT_ITM_CD;
            string err = "";
            Int32 seqNo = 0;
            seqNo = CHNLSVC.Inventory.GetBlItmMaxSeqNo(oOutput, out err) + 1;
            oOutput.Ibi_anal_1 = seqNo.ToString();
            oOutput.Ibi_anal_2 = String.Empty;
            oOutput.Ibi_anal_3 = String.Empty;
            oOutput.Ibi_anal_4 = String.Empty;
            oOutput.Ibi_anal_5 = String.Empty;
            oOutput.Ibi_cre_by = Session["UserID"].ToString();
            oOutput.Ibi_cre_dt = DateTime.Now;
            oOutput.Ibi_mod_by = Session["UserID"].ToString();
            oOutput.Ibi_mod_dt = DateTime.Now;
            oOutput.Ibi_session_id = Session["SessionID"].ToString();

            oOutput.MI_SHORTDESC = oInput.MI_SHORTDESC;
            oOutput.MI_COLOR_EXT = oInput.MI_COLOR_EXT;
            oOutput.MI_ITM_TP = oInput.MI_ITM_TP;
            oOutput.MI_ITM_UOM = oInput.MI_ITM_UOM;
            oOutput.MI_HS_CD = oInput.MI_HS_CD;

            oOutput.isNewItem = 0;

            return oOutput;
        }

        private bool updateItemDetials(String ItmeCode)
        {
            bool status = false;

            GridViewRow row = (GridViewRow)dgvInvoiceDetails.Rows[dgvInvoiceDetails.EditIndex];
            Label lblIbi_pi_line = (Label)row.FindControl("lblIbi_pi_line");
            TextBox txtIbi_pi_no = (TextBox)row.FindControl("txtIbi_pi_no");
            oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];
            ImportsBLItems oEdiedItem = oImportsBLItems.Find(x => x.Ibi_pi_no == txtIbi_pi_no.Text.Trim() && x.Ibi_pi_line == Convert.ToInt32(lblIbi_pi_line.Text.Trim()));

            int indexOf = oImportsBLItems.IndexOf(oEdiedItem);


            MasterItem oMasterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), ItmeCode);
            if (oMasterItem != null && oMasterItem.Mi_cd != null)
            {
                status = true;
                oEdiedItem.MI_SHORTDESC = oMasterItem.Mi_shortdesc;
                oEdiedItem.MI_COLOR_EXT = oMasterItem.Mi_color_ext;
                oEdiedItem.MI_ITM_TP = oMasterItem.Mi_itm_tp;
                oEdiedItem.MI_ITM_UOM = oMasterItem.Mi_itm_uom;
                oEdiedItem.Ibi_itm_cd = ItmeCode;
                Session["oImportsBLItems"] = oImportsBLItems;
            }

            return status;
        }

        private bool checkItemCode(String ItmeCode)
        {
            bool status = false;

            MasterItem oMasterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), ItmeCode);
            if (oMasterItem != null && oMasterItem.Mi_cd != null)
            {
                status = true;
            }

            return status;
        }

        private void calculateTotals()
        {
            oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];
            if (oImportsBLItems != null && oImportsBLItems.Count > 0)
            {
                Decimal totalQty = oImportsBLItems.Where(y => y.Ibi_stus == 1).Sum(x => x.Ibi_qty);
                //Decimal totalUniteRate = oImportsBLItems.Where(y => y.Ibi_stus == 1).Sum(x => x.Ibi_pi_unit_rt);

                Decimal totalUniteRate = oImportsBLItems.Where(y => y.Ibi_stus == 1).Sum(x => x.Ibi_unit_rt * x.Ibi_qty);
                Decimal totalUniteRateFoc = oImportsBLItems.Where(y => y.Ibi_stus == 1 && y.Ibi_tp != "F").Sum(x => x.Ibi_unit_rt * x.Ibi_qty);

                txtTotalOrderQty.Text = totalQty.ToString("N5");
                txtTotalOrderValueIn.Text = totalUniteRate.ToString("N5");
                txtBlValue.Text = totalUniteRateFoc.ToString("N5");
            }
        }

        private bool validateNewLine()
        {
            bool status = true;

            if (String.IsNullOrEmpty(txtItem.Text))
            {
                DisplayMessage("Please select an item code", 2);
                status = false;
                return status;
            }
            if (!checkItemCode(txtItem.Text.Trim()))
            {
                DisplayMessage("Please select a valid item code", 2);
                status = false;
                return status;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                DisplayMessage("Please enter item quantity", 2);
                status = false;
                return status;
            }
            if (Convert.ToDecimal(txtQty.Text) == 0)
            {
                DisplayMessage("Please enter valid item quantity", 2);
                status = false;
                return status;
            }
            if (String.IsNullOrEmpty(txtUnitPrice.Text))
            {
                DisplayMessage("Please enter unit price", 2);
                status = false;
                return status;
            }
            if (String.IsNullOrEmpty(txtUnitTotal.Text))
            {
                DisplayMessage("Please enter unit total", 2);
                status = false;
                return status;
            }
            if (ddlPriceType.SelectedIndex == 0)
            {
                DisplayMessage("Please select a Price Basis", 2);
                status = false;
                return status;
            }
            if (ddlTag.SelectedIndex == 0)
            {
                DisplayMessage("Please select a Tag", 2);
                status = false;
                return status;
            }

            return status;
        }

        private void DispalyMessages_NOTUSE(string msg, Int32 option)
        {
            if (option == 1)
            {
                divAlert.Visible = true;
                lblAlert.Text = msg;
            }
            else if (option == 2)
            {
                divSuccess.Visible = true;
                lblSuccess.Text = msg;
            }
            else if (option == 3)
            {
                divWarning.Visible = true;
                lblWarning.Text = msg;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "scroll", "scrollTop();", true);
        }

        private void DisplayMessage(String Msg, Int32 option, Exception ex = null)
        {
            Msg = Msg.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ").Replace("\r", "");
            if (option == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
            }
            else if (option == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + Msg + "');", true);
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

        private void DisplayMessage(String Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
        }

        private void DisplayMessageJS(String Msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
        }

        private void clearEntryLine()
        {
            lblDescription.Text = "";
            lblBrand.Text = "";
            lblUOM.Text = "";

            txtItem.Text = "";
            txtQty.Text = "";
            ddlPriceType.SelectedIndex = 0;
            if (chkTotalFOCShipment.Checked)
            {
                ddlPriceType.SelectedIndex = ddlPriceType.Items.IndexOf(ddlPriceType.Items.FindByText("FOC"));
            }
            txtUnitPrice.Text = "";
            txtUnitTotal.Text = "";
            ddlTag.SelectedIndex = 0;
            ddlTag.Enabled = true;
            txtItem.Focus();
        }

        private void cleaINvoiceEntyLine()
        {
            txtInvoiceDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
            txtInvoiceNo.Text = "";
        }

        private void calUnitTotal()
        {
            decimal qty = 0;
            decimal unitPrice = 0;
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                qty = 1;
            }
            else
            {
                if (!isdecimal(txtQty.Text))
                {
                    DisplayMessage("Please enter valid quantity", 2);
                    txtQty.Focus();
                    return;
                }

                qty = Convert.ToDecimal(txtQty.Text);
            }
            if (string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                unitPrice = 0;
            }
            else
            {
                if (!isdecimal(txtUnitPrice.Text))
                {
                    DisplayMessage("Please enter valid unit price", 2);
                    txtQty.Focus();
                    return;
                }
                unitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            }

            txtUnitTotal.Text = (unitPrice * qty).ToString("N5");
        }

        private void loadTrareTerms()
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TradeTerms);
            DataTable result = CHNLSVC.CommonSearch.SearchTraderTerms(SearchParams, null, null);
            ddlTradeTerms.DataSource = result;
            ddlTradeTerms.DataTextField = "CODE";
            ddlTradeTerms.DataValueField = "CODE";
            ddlTradeTerms.DataBind();
            ddlTradeTerms.Items.Insert(0, new ListItem(" - - Select - - ", "0"));
            ddlTradeTerms.SelectedValue = "0";
        }

        private void loadFromPorts()
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.TradeTerms);
            DataTable result = CHNLSVC.Financial.GetSupplierPorts(Session["UserCompanyCode"].ToString(), txtExporter.Text);
            ddlFromPort.DataSource = result;
            ddlFromPort.DataTextField = "MP_NAME";
            ddlFromPort.DataValueField = "mspr_frm_port";
            ddlFromPort.DataBind();
            ddlFromPort.Items.Insert(0, new ListItem(" - - Select - - ", "0"));
            txtETD.Text = (DateTime.Now.Date).ToString("dd/MMM/yyyy");
        }

        private void loadContainerType()
        {
            List<MST_CONTAINER_TP> oMST_CONTAINER_TPs = CHNLSVC.Financial.GET_CONTAINER_TYPES();
            if (oMST_CONTAINER_TPs != null)
            {
                List<ComboBoxObject> oItems = new List<ComboBoxObject>();
                ComboBoxObject oItem1 = new ComboBoxObject();
                oItem1.Text = "-- Select --";
                oItem1.Value = "0";
                oItems.Add(oItem1);
                foreach (MST_CONTAINER_TP item in oMST_CONTAINER_TPs)
                {
                    ComboBoxObject oItem2 = new ComboBoxObject();
                    oItem2.Text = item.Mct_desc;
                    oItem2.Value = item.Mct_tp;
                    oItems.Add(oItem2);
                }

                ddlContainersType.DataSource = oItems;
                ddlContainersType.DataTextField = "Text";
                ddlContainersType.DataValueField = "Value";
                ddlContainersType.DataBind();
            }
        }

        private bool validateHeader()
        {
            bool status = true;
            if (string.IsNullOrEmpty(txtDocNumber.Text.Trim()))
            {
                if (string.IsNullOrEmpty(txtReferenceNum.Text))
                {
                    DisplayMessage("Please enter a reference number", 2);
                    txtReferenceNum.Focus();
                    status = false;
                    return status;
                }
            }
            if (string.IsNullOrEmpty(txtBLNumber.Text))
            {
                DisplayMessage("Please enter a B/L number", 2);
                txtBLNumber.Focus();
                status = false;
                return status;
            }
            //if (string.IsNullOrEmpty(txtPackingListNo.Text))
            //{
            //    DisplayMessage("Please enter packing list", 2);
            //    txtPackingListNo.Focus();
            //    status = false;
            //    return status;
            //}
            if (string.IsNullOrEmpty(txtTotalPackages.Text))
            {
                DisplayMessage("Please enter total packages", 2);
                txtTotalPackages.Focus();
                status = false;
                return status;
            }
            if (ddlTradeTerms.SelectedIndex == 0)
            {
                DisplayMessage("Please select a trade term", 2);
                ddlTradeTerms.Focus();
                status = false;
                return status;
            }
            //if (string.IsNullOrEmpty(txtDeliveryTerms.Text))
            //{
            //    DispalyMessages("Please enter delivery terms", 2);
            //    txtDeliveryTerms.Focus();
            //    status = false;
            //    return status;
            //}
            if (string.IsNullOrEmpty(txtConsignee.Text))
            {
                DisplayMessage("Please enter consignee", 2);
                txtConsignee.Focus();
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtDeclarant.Text))
            {
                DisplayMessage("Please enter declarant", 2);
                txtDeclarant.Focus();
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtExporter.Text))
            {
                DisplayMessage("Please enter exporter", 2);
                txtExporter.Focus();
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtAgent.Text))
            {
                DisplayMessage("Please enter agent", 2);
                txtAgent.Focus();
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtVessel.Text))
            {
                DisplayMessage("Please enter vessel", 2);
                txtVessel.Focus();
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtVoyage.Text))
            {
                DisplayMessage("Please enter voyage", 2);
                txtVoyage.Focus();
                status = false;
                return status;
            }
            if (ddlFromPort.SelectedIndex == 0)
            {
                DisplayMessage("Please select a from port", 2);
                ddlFromPort.Focus();
                status = false;
                return status;
            }
            if (ddlToPort.SelectedIndex == 0)
            {
                DisplayMessage("Please select a to port", 2);
                ddlToPort.Focus();
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtCountyOfOrigin.Text))
            {
                DisplayMessage("Please enter county of origin", 2);
                txtCountyOfOrigin.Focus();
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtlocationOfGoods.Text))
            {
                DisplayMessage("Please enter location of goods", 2);
                txtCountyOfOrigin.Focus();
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtExporterCounty.Text))
            {
                DisplayMessage("Please enter exporter county", 2);
                txtExporterCounty.Focus();
                status = false;
                return status;
            }
            if (string.IsNullOrEmpty(txtPlaceOfLanding.Text))
            {
                DisplayMessage("Please enter place of landing", 2);
                txtPlaceOfLanding.Focus();
                status = false;
                return status;
            }
            if (ddlModeofShipment.SelectedIndex == 0)
            {
                DisplayMessage("Please select mode of shipment", 2);
                ddlModeofShipment.Focus();
                status = false;
                return status;
            }
            if (ddlCaringType.SelectedIndex == 0)
            {
                DisplayMessage("Please caring type", 2);
                ddlCaringType.Focus();
                status = false;
                return status;
            }
            if (dgvCostItems.Rows.Count < 1)
            {
                DisplayMessage("Please setup the costing details", 2);
                ddlCaringType.Focus();
                status = false;
                return status;
            }
            if (CHNLSVC.Financial.IsCosting(txtDocNumber.Text))
            {
                DisplayMessage("Can't Save! Already Costing Completed! ", 2);
                status = false;
                return status;
            }
            OImportsBLSInvoice = (List<ImportsBLSInvoice>)Session["OImportsBLSInvoice"];
            //Removed as per the discussion had with the department on 2016-01-08
            //if (OImportsBLSInvoice == null || OImportsBLSInvoice.Count == 0 || OImportsBLSInvoice.FindAll(x => x.Ibs_act == 1).Count == 0)
            //{
            //    DisplayMessage("Please add invoice numbers", 2);
            //    txtInvoiceNo.Focus();
            //    status = false;
            //    return status;
            //}

            //if (dgvInvoiceNums.Rows.Count == 0)
            //{
            //    DispalyMessages("Please add invoice numbers", 1);
            //    txtInvoiceNo.Focus();
            //    status = false;
            //    return status;
            //}

            oImportsBLContainers = (List<ImportsBLContainer>)Session["oImportsBLContainers"];
            if (oImportsBLContainers == null || oImportsBLContainers.Count == 0 || oImportsBLContainers.FindAll(x => x.Ibc_act == 1).Count == 0)
            {
                DisplayMessage("Please add container details", 2);
                txtContainerNo.Focus();
                status = false;
                return status;
            }
            //if (dgvContainers.Rows.Count == 0)
            //{
            //    DispalyMessages("Please add container details", 1);
            //    txtContainerNo.Focus();
            //    status = false;
            //    return status;
            //}

            oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];
            if (oImportsBLItems == null || oImportsBLItems.Count == 0 || oImportsBLItems.FindAll(x => x.Ibi_stus == 1).Count == 0)
            {
                DisplayMessage("Please add items", 2);
                txtItem.Focus();
                status = false;
                return status;
            }
            //if (dgvInvoiceDetails.Rows.Count == 0)
            //{
            //    DispalyMessages("Please add items", 1);
            //    txtItem.Focus();
            //    status = false;
            //    return status;
            //}
            return status;
        }

        private void loadToPorts()
        {
            List<ComboBoxObject> oItems = new List<ComboBoxObject>();
            ComboBoxObject oItem1 = new ComboBoxObject();
            oItem1.Text = "-- Select --";
            oItem1.Value = "0";
            oItems.Add(oItem1);
            ComboBoxObject oItem2 = new ComboBoxObject();
            oItem2.Text = "Colombo";
            oItem2.Value = "CMB";
            oItems.Add(oItem2);
            ddlToPort.DataSource = oItems;
            ddlToPort.DataTextField = "Text";
            ddlToPort.DataValueField = "Value";
            ddlToPort.DataBind();

            ddlToPort.SelectedValue = "CMB";

            ddlFinalPort.DataSource = oItems;
            ddlFinalPort.DataTextField = "Text";
            ddlFinalPort.DataValueField = "Value";
            ddlFinalPort.DataBind();
        }

        private void loadExportCurCode()
        {
            txtCurrenyCode.Text = "";
            txtCurrenyCode.ToolTip = "";
            List<MasterBusinessEntity> oMasterBusinessEntitys = CHNLSVC.Financial.GetCustomerDetailList(Session["UserCompanyCode"].ToString(), txtExporter.Text.Trim(), "", "", "S");
            if (oMasterBusinessEntitys != null && oMasterBusinessEntitys.Count > 0)
            {
                lblCurCode.Text = oMasterBusinessEntitys[0].Mbe_cur_cd;
                if (!string.IsNullOrEmpty(txtDocNumber.Text))
                {
                    ImportsBLHeader _tmpSiData = CHNLSVC.Financial.GET_BL_HEADER_BY_DOC(Session["UserCompanyCode"].ToString(), txtDocNumber.Text.Trim(), "ALL");
                    if (_tmpSiData != null)
                    {
                        lblCurCode.Text = _tmpSiData.Ib_cur_cd;
                    }
                }
                txtExporter.ToolTip = oMasterBusinessEntitys[0].Mbe_name;

                txtCreditAllowDate.Text = DateTime.Now.AddDays(oMasterBusinessEntitys[0].MBE_CR_PERIOD).ToString("dd/MMM/yyyy");

                if (String.IsNullOrEmpty(lblCurCode.Text))
                {
                    DisplayMessage("please setup the exporter currency code", 2);
                    txtExporter.Text = "";
                    txtExporter.Focus();
                    txtExporter.ToolTip = "";
                    lblCurCode.Text = "";
                }
                else
                {
                    List<MasterCurrency> oMasterCurrencys = CHNLSVC.General.GetAllCurrency(lblCurCode.Text);
                    if (oMasterCurrencys != null && oMasterCurrencys.Count > 0)
                    {
                        txtCurrenyCode.Text = oMasterCurrencys[0].Mcr_desc;
                        txtCurrenyCode.ToolTip = txtCurrenyCode.Text;
                        ExcRate();
                    }
                }
            }
            else
            {
                DisplayMessage("please select a correct exporter", 2);
                txtExporter.Text = "";
                txtExporter.Focus();
                txtExporter.ToolTip = "";
                lblCurCode.Text = "";
            }
        }

        private void LoadDocDetails()
        {
            oHeader = CHNLSVC.Financial.GET_BL_HEADER_BY_DOC(Session["UserCompanyCode"].ToString(), txtDocNumber.Text.Trim(), "ALL");
            if (oHeader != null)
            {
                #region oHeader
                List<ImportsBLItems> oImportsBLItems = CHNLSVC.Financial.GET_BL_ITMS_BY_SEQ(oHeader.Ib_seq_no);
                _PIITEMBACKUP = oImportsBLItems;

                if (string.IsNullOrEmpty(txtDocNumber.Text))
                {
                    Session["IsSearch"] = true;
                }
                if (oImportsBLItems != null && oImportsBLItems.Count > 0)
                {
                    btnSave.Visible = false;

                    hdfHEaderStatus.Value = oHeader.Ib_stus;

                    isNewRecord.Value = "0";
                    txtBLNumber.Text = oHeader.Ib_bl_no;
                    txtShipmentDate.Text = oHeader.Ib_bl_dt.ToString("dd/MMM/yyyy");
                    chkBypassEntry.Checked = oHeader.Ib_clear_pty;
                    chkTotalFOCShipment.Checked = oHeader.Ib_tot_foc;
                    txtReferenceNum.Text = oHeader.Ib_bl_ref_no;
                    txtRemark.Text = oHeader.Ib_rmk;
                    txtExporter.Text = oHeader.Ib_supp_cd;
                    txtExporter_TextChanged(null, null);
                    loadFromPorts();
                    loadToPorts();
                    txtConsignee.Text = oHeader.Ib_consi_cd;
                    txtConsignee_TextChanged(null, null);
                    txtDeclarant.Text = oHeader.Ib_decl_cd;
                    txtDeclarant_TextChanged(null, null);
                    txtAgent.Text = oHeader.Ib_agent_cd;
                    txtAgent_TextChanged(null, null);
                    txtExporterCounty.Text = oHeader.Ib_expo_cnty;
                    txtCountyOfOrigin.Text = oHeader.Ib_origin_cnty;
                    txtTotalPackages.Text = oHeader.Ib_tot_pkg;
                    txtPackingListNo.Text = oHeader.Ib_pack_lst_no;
                    txtVessel.Text = oHeader.Ib_vessel_no;
                    txtVoyage.Text = oHeader.Ib_voyage;
                    txtPlaceOfLanding.Text = oHeader.Ib_loading_place;
                    ddlFromPort.SelectedIndex = ddlFromPort.Items.IndexOf(ddlFromPort.Items.FindByValue(oHeader.Ib_frm_port));
                    ddlToPort.SelectedIndex = ddlToPort.Items.IndexOf(ddlToPort.Items.FindByValue(oHeader.Ib_to_port));
                    // ddlToPort.SelectedValue = oHeader.Ib_to_port;
                    if (!string.IsNullOrEmpty(oHeader.Ib_anal_3))
                    {
                        // ddlFinalPort.SelectedValue = oHeader.Ib_anal_3;
                        ddlFinalPort.SelectedIndex = ddlFinalPort.Items.IndexOf(ddlFinalPort.Items.FindByValue(oHeader.Ib_anal_3));
                    }
                    if (!string.IsNullOrEmpty(oHeader.Ib_anal_2))
                    {
                        ddlUOM.SelectedValue = oHeader.Ib_anal_2;
                    }
                    txtETD.Text = oHeader.Ib_etd.ToString("dd/MMM/yyyy");
                    txtETA.Text = oHeader.Ib_eta.ToString("dd/MMM/yyyy");
                    lblCurCode.Text = oHeader.Ib_cur_cd;
                    txtTotalOrderValueIn.Text = oHeader.Ib_tot_bl_amt.ToString("N2");
                    txtBlValue.Text = oHeader.Ib_tot_bl_amt.ToString("N2");

                    txtlocationOfGoods.Text = oHeader.Ib_loc_of_goods;

                    txtDocReceivedDate.Text = oHeader.Ib_doc_clear_dt.ToString("dd/MMM/yyyy");
                    txtDocReceivedDate.Text = oHeader.Ib_doc_rec_dt.ToString("dd/MMM/yyyy");
                    if (oHeader.Ib_doc_clear_dt == DateTime.MinValue && oHeader.Ib_doc_rec_dt == DateTime.MinValue)
                    {
                        txtDocReceivedDate.Text = "";
                    }
                    txtShSeq.Text = oHeader.Ib_si_seq_no.ToString();

                    txtguno.Text = oHeader.Ib_gurr_no;
                    txtissuedate.Text = oHeader.Ib_issue_date.ToString("dd/MMM/yyyy");
                    DateTime orddate = DateTime.Now;
                    if (txtissuedate.Text == "01/Jan/0001")
                    {
                        txtissuedate.Text = orddate.ToString("dd/MMM/yyyy");
                    }
                    txtexDate.Text = oHeader.Ib_ex_date.ToString("dd/MMM/yyyy");
                    if (txtexDate.Text == "01/Jan/0001")
                    {
                        txtexDate.Text = orddate.ToString("dd/MMM/yyyy");
                    }
                    txtCancel.Text = oHeader.Ib_cancel_date.ToString("dd/MMM/yyyy");
                    if (txtCancel.Text == "01/Jan/0001")
                    {
                        txtCancel.Text = orddate.ToString("dd/MMM/yyyy");
                    }

                    chkIscancel.Checked = oHeader.Ib_is_cancel;
                    if (chkIscancel.Checked)
                    {
                        pnlcancl.Visible = true;
                    }
                    else { pnlcancl.Visible = false; }
                    if (!string.IsNullOrEmpty(oHeader.Ib_anal_1))
                    {
                        ddlTradeTerms.SelectedIndex = ddlTradeTerms.Items.IndexOf(ddlTradeTerms.Items.FindByValue(oHeader.Ib_anal_1.ToString()));
                        ddlTradeTerms_SelectedIndexChanged(null, null);
                    }
                    else
                    {
                        ddlTradeTerms.SelectedIndex = 0;
                    }

                    if (!string.IsNullOrEmpty(oHeader.Ib_anal_2))
                    {
                        ddlUOM.SelectedValue = oHeader.Ib_anal_2.ToString();
                    }
                    else
                    {
                        ddlUOM.SelectedIndex = 0;
                    }
                    string bankcode = string.Empty;
                    Session["oFinHeaders"] = null;
                    dgvFinancialDocs.DataSource = new int[] { };
                    dgvFinancialDocs.DataBind();
                    var _scanlc = oImportsBLItems.GroupBy(x => new { x.Ibi_fin_no }).Select(group => new { Peo = group.Key });

                    foreach (var itm in _scanlc)
                    {
                        if (itm.Peo.Ibi_fin_no != "")
                        {
                            Order_Financing oFinalcialHeader = CHNLSVC.Financial.GET_IMP_FIN_HDR_BY_DOC(Session["UserCompanyCode"].ToString(), itm.Peo.Ibi_fin_no);
                            oFinalcialHeader.If_ref_no = txtReferenceNum.Text.ToString();
                            if (Session["oFinHeaders"] != null)
                            {
                                oFinHeaders = (List<Order_Financing>)Session["oFinHeaders"];
                            }
                            else
                            {
                                oFinHeaders = new List<Order_Financing>();
                            }
                            oFinHeaders.Add(oFinalcialHeader);
                            Session["oFinHeaders"] = oFinHeaders;
                            BindFinHeaders();
                            bankcode = oFinalcialHeader.If_bank_cd;
                            Session["CreditP"] = oFinalcialHeader.If_crdt_pd;
                            int _crep = (int)Session["CreditP"];
                            txtCreditAllowDate.Text = Convert.ToDateTime(txtShipmentDate.Text).AddDays(oFinalcialHeader.If_crdt_pd).ToString("dd/MMM/yyyy");
                        }
                    }
                    MasterOutsideParty _MasterOutsideParty = new MasterOutsideParty();
                    _MasterOutsideParty = CHNLSVC.Sales.GetOutSidePartyDetails(bankcode, "BANK");
                    if (_MasterOutsideParty.Mbi_cd != null)
                    {
                        lblbank.Text = _MasterOutsideParty.Mbi_desc;
                    }

                    string _piNo = "";
                    List<OrderPlanItem> _OrderPlaneItem = new List<OrderPlanItem>();
                    foreach (ImportsBLItems _oImportsBLItems in oImportsBLItems)
                    {
                        if (_piNo != _oImportsBLItems.Ibi_pi_no)
                        {
                            _piNo = _oImportsBLItems.Ibi_pi_no;
                            _OrderPlaneItem = CHNLSVC.Financial.GET_IMP_OPBY_PI(_oImportsBLItems.Ibi_pi_no);
                        }
                        if (_OrderPlaneItem != null)
                        {

                            if (_OrderPlanItemBackup != null)
                            {
                                if (_OrderPlanItemBackup.Count > 0)
                                {
                                    var _filterpi = _OrderPlanItemBackup.Where(x => x.IOI_OP_NO == _oImportsBLItems.Ibi_pi_no).ToList();
                                    if (_filterpi.Count == 0)
                                    {
                                        _OrderPlanItemBackup = _OrderPlaneItem;
                                    }
                                }
                                else
                                {
                                    _OrderPlanItemBackup = _OrderPlaneItem;
                                }
                            }

                            var _filter = _OrderPlaneItem.Where(x => x.IOI_ITM_CD == _oImportsBLItems.Ibi_itm_cd && x.IOI_ITM_STUS == _oImportsBLItems.Ibi_itm_stus && x.IOI_STUS == 1).ToList();
                            foreach (var va in _filter)
                            {
                                if (va != null)
                                {
                                    if (string.IsNullOrEmpty(txtDocNumber.Text))
                                    {
                                        _oImportsBLItems.Ibi_tag = va.IOI_TAG;
                                    }
                                    if ((_oImportsBLItems.Ibi_tag == "N") || (_oImportsBLItems.Ibi_tag == ""))
                                    {
                                        _oImportsBLItems.Ibi_tag_Desc = "General";
                                    }
                                    else
                                    {
                                        _oImportsBLItems.Ibi_tag_Desc = "Special Project";
                                    }
                                    _oImportsBLItems.Ibi_project_name = va.IOI_ProjectName;
                                }
                            }
                        }

                    }
                    #region MyRegion

                    Session["oHeader"] = oHeader;
                    foreach (var item in oImportsBLItems)
                    {
                        item.Tmp_ibi_line = item.Ibi_line;
                    }
                    Session["oImportsBLItems"] = oImportsBLItems;
                    BindPInvoiceItems();

                    oImportsBLContainers = CHNLSVC.Financial.GET_IMP_BL_CONTNR_BY_SEQ(oHeader.Ib_seq_no);
                    Session["oImportsBLContainers"] = oImportsBLContainers;
                    BindContainers();

                    OImportsBLSInvoice = CHNLSVC.Financial.GET_IMP_BL_SI_BY_SEQ(oHeader.Ib_seq_no);
                    Session["OImportsBLSInvoice"] = OImportsBLSInvoice;
                    BindSInvoices();

                    oImportsBLCosts = CHNLSVC.Financial.GET_IMP_BL_COST_BY_SEQ(oHeader.Ib_seq_no);
                    Session["oImportsBLCosts"] = oImportsBLCosts;
                    //BindCostItems();
                    if (oImportsBLCosts != null)
                    {
                        //ddlTradeTerms.Enabled = false;
                    }
                    else
                    {
                        ddlTradeTerms.Enabled = true;
                    }

                    setbtnenable(true);
                    if (oHeader.Ib_stus == "A")
                    {
                        txtStatus.Text = "Approved";
                        setbtnenable(true);
                        setResetButton();
                    }
                    else if (oHeader.Ib_stus == "P")
                    {
                        txtStatus.Text = "Pending";
                        btnreset.Visible = false;

                    }
                    else if (oHeader.Ib_stus == "C")
                    {
                        txtStatus.Text = "Cancelled";
                        setbtnenable(false);
                    }
                    else
                    {
                        txtStatus.Text = "";
                    }

                    if (oHeader.Ib_clear_pty)
                    {
                        chkBypassEntry.Checked = true;
                    }
                    else
                    {
                        chkBypassEntry.Checked = false;
                    }

                    setCostValues();

                    if (!string.IsNullOrEmpty(oHeader.Ib_tos))
                    {
                        ddlModeofShipment.SelectedValue = oHeader.Ib_tos.ToString();
                    }
                    else
                    {
                        ddlModeofShipment.SelectedIndex = 0;
                    }

                    if (!string.IsNullOrEmpty(oHeader.Ib_carry_tp))
                    {
                        ddlCaringType.SelectedValue = oHeader.Ib_carry_tp.ToString();
                    }
                    else
                    {
                        ddlCaringType.SelectedIndex = 0;
                    }


                    #endregion

                }
                else
                {
                    clear();
                }
                #endregion
                #region add by lakshan 25Dec2017
                if (!string.IsNullOrEmpty(txtDocNumber.Text))
                {
                    txtCreditAllowDate.Text = oHeader.IB_CREDALOW_DT.ToString("dd/MMM/yyyy");
                }
                #endregion
            }
            else
            {
                DisplayMessage("please select a valid doc # !!!", 1);
                txtDocNumber.Text = ""; txtDocNumber.Focus(); return;
            }
        }


        private void setResetButton()
        {

            btnreset.Visible = true;

        }
        private void setCostValues()
        {
            if (Session["oImportsBLItems"] != null)
            {
                Decimal oItemValue = 0;
                Decimal oTotalValue = 0;

                oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];

                if (Session["oImportsBLCosts"] != null)
                {
                    oImportsBLCosts = (List<ImportsBLCost>)Session["oImportsBLCosts"];

                    if (oImportsBLCosts.FindAll(x => x.Ibcs_ele_cd.ToString().ToUpper() == "COST").Count > 0)
                    {
                        ImportsBLCost oCostItem = oImportsBLCosts.Find(x => x.Ibcs_ele_cd.ToString().ToUpper() == "COST");
                        oItemValue = oImportsBLItems.Where(z => z.Ibi_stus == 1 && z.Ibi_tp != "F").Sum(x => x.Ibi_unit_rt * x.Ibi_qty);
                        oCostItem.Ibcs_amt = oItemValue;
                        Session["oImportsBLCosts"] = oImportsBLCosts;
                    }

                    Session["oImportsBLCosts"] = oImportsBLCosts;
                    oTotalValue = oImportsBLCosts.Where(z => z.Ibcs_act == 1).Sum(x => x.Ibcs_amt);
                    //txtTotalOrderValueIn.Text = oTotalValue.ToString("N2");
                    txtBlValue.Text = oTotalValue.ToString("N2");

                    if (oTotalValue == 0)
                    {
                        oImportsBLItems.ForEach(x => x.Ibi_anal_5 = (x.Ibi_qty * x.Ibi_unit_rt).ToString());
                        oTotalValue = oImportsBLItems.Sum(x => Convert.ToDecimal(x.Ibi_anal_5));
                        txtTotalOrderValueIn.Text = oTotalValue.ToString("N2");
                        var v = oImportsBLItems.Where(z => z.Ibi_stus == 1 && z.Ibi_tp != "F").ToList();
                        if (v != null)
                        {
                            txtBlValue.Text = v.Sum(x => Convert.ToDecimal(x.Ibi_anal_5)).ToString("N2");
                        }
                    }
                    BindCostItems();
                }
            }
        }

        private void SetFocus(String ControlName)
        {
            if (ControlName != null)
            {
                String[] arr = ControlName.Split('$');
                if (arr != null && arr.Length > 0)
                {
                    string Selectedcontrol = arr[arr.Length - 1];
                    //Control myControl1 = FindControl(ControlName);
                    //myControl1.Focus();
                }
            }
        }

        private bool isdecimal(string txt)
        {
            decimal asdasd;
            return decimal.TryParse(txt, out asdasd);
        }

        private void loadCarryTyps()
        {
            List<ComboBoxObject> oItem = CHNLSVC.Financial.GET_REF_CARY_TYPE();
            ddlCaringType.DataSource = oItem;
            ddlCaringType.DataTextField = "Text";
            ddlCaringType.DataValueField = "Value";
            ddlCaringType.DataBind();
        }


        #endregion

        protected void txtBLNumber_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBLNumber.Text))
            {
                ImportsBLHeader oheader = CHNLSVC.Financial.GET_IMP_BL_BY_BL(Session["UserCompanyCode"].ToString(), txtBLNumber.Text);
                if (oheader != null && oheader.Ib_doc_no != null)
                {
                    DisplayMessage("Records are exist for B/L number : " + txtBLNumber.Text, 2);
                    txtBLNumber.Text = string.Empty;
                }
            }
        }

        protected void btnDelOrderFinance_Click(object sender, EventArgs e)
        {
            try
            {
                txtShSeq.Text = "";
                if (hdfDelofrF.Value == "Yes")
                {
                    GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
                    int index = row.RowIndex;
                    CheckBox cb1 = (CheckBox)dgvFinancialDocs.Rows[index].FindControl("chkSelect");
                    Label lblif_doc_no = (Label)dgvFinancialDocs.Rows[index].FindControl("lblif_doc_no");

                    if (Session["oFinHeaders"] != null)
                    {
                        oFinHeaders = (List<Order_Financing>)Session["oFinHeaders"];
                        oFinHeaders.RemoveAll(x => x.If_doc_no == lblif_doc_no.Text);
                        dgvFinancialDocs.DataSource = oFinHeaders;
                        Session["oFinHeaders"] = oFinHeaders;
                        BindFinHeaders();
                    }

                    if (Session["oImportsBLItems"] != null)
                    {
                        oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];

                        if (oImportsBLItems.FindAll(x => x.Ibi_fin_no == lblif_doc_no.Text).Count > 0)
                        {
                            List<ImportsBLItems> oList = oImportsBLItems.FindAll(x => x.Ibi_fin_no == lblif_doc_no.Text);
                            oList.ForEach(x => x.Ibi_stus = 0);//.Where(x => x.Ibi_fin_no == lblif_doc_no.Text);
                            Session["oImportsBLItems"] = oImportsBLItems;
                            BindPInvoiceItems();
                            clearEntryLine();
                            setCostValues();
                            BindCostItems();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void setbtnenable(bool isEnable)
        {
            if (isEnable)
            {
                btnCancel.CssClass = "buttonUndocolor";
                btnCancel.OnClientClick = "ConfirmCancel();";
                btnCancel.Enabled = true;

                btnApprove.CssClass = "buttonUndocolor";
                btnApprove.OnClientClick = "ConfirmApprove();";
                btnApprove.Enabled = true;

                btnUpdateAdvnDet.CssClass = "buttonUndocolor";
                btnUpdateAdvnDet.OnClientClick = "ConfirmUpdate();";
                btnUpdateAdvnDet.Enabled = true;



                chkBypassEntry.Enabled = true;
                chkTotalFOCShipment.Enabled = true;

                btnInvoiceAdd.Enabled = true; btnInvoiceAdd.CssClass = "buttonUndocolor";
                btnContainerAdd.Enabled = true; btnContainerAdd.CssClass = "buttonUndocolor";
                btnFinancialDocumentNo.Enabled = true; btnFinancialDocumentNo.CssClass = "buttonUndocolor";
                btnFinancialDocumentNoView.Enabled = true; btnFinancialDocumentNoView.CssClass = "buttonUndocolor";

                btnAddNewtems.Enabled = true;

                LinkButton lbtnDelAllItem = dgvInvoiceDetails.HeaderRow.FindControl("lbtnDelAllItem") as LinkButton;
                lbtnDelAllItem.CssClass = "buttonUndocolor";
                lbtnDelAllItem.OnClientClick = "ConfDelAll();";
                lbtnDelAllItem.Enabled = true;
            }
            else
            {
                btnCancel.CssClass = "buttoncolor";
                btnCancel.OnClientClick = "";
                btnCancel.Enabled = false;

                btnApprove.CssClass = "buttoncolor";
                btnApprove.OnClientClick = "";
                btnApprove.Enabled = false;

                btnUpdateAdvnDet.CssClass = "buttoncolor";
                btnUpdateAdvnDet.OnClientClick = "";
                btnUpdateAdvnDet.Enabled = false;



                for (int i = 0; i < dgvInvoiceNums.Rows.Count; i++)
                {
                    GridViewRow dr = dgvInvoiceNums.Rows[i];
                    LinkButton btnSIDelete = dr.FindControl("btnSIDelete") as LinkButton;
                    btnSIDelete.CssClass = "buttoncolor";
                    btnSIDelete.OnClientClick = "";
                    btnSIDelete.Enabled = false;
                }

                for (int i = 0; i < dgvContainers.Rows.Count; i++)
                {
                    GridViewRow dr = dgvContainers.Rows[i];
                    LinkButton btnContaiDelete = dr.FindControl("btnContaiDelete") as LinkButton;
                    btnContaiDelete.CssClass = "buttoncolor";
                    btnContaiDelete.OnClientClick = "";
                    btnContaiDelete.Enabled = false;
                }

                for (int i = 0; i < dgvInvoiceDetails.Rows.Count; i++)
                {
                    GridViewRow dr = dgvInvoiceDetails.Rows[i];
                    LinkButton lbtnedititem = dr.FindControl("lbtnedititem") as LinkButton;
                    lbtnedititem.CssClass = "buttoncolor";
                    lbtnedititem.OnClientClick = "";
                    lbtnedititem.Enabled = false;

                    LinkButton btnRecDelete = dr.FindControl("btnRecDelete") as LinkButton;
                    btnRecDelete.CssClass = "buttoncolor";
                    btnRecDelete.OnClientClick = "";
                    btnRecDelete.Enabled = false;
                }

                chkBypassEntry.Enabled = false;
                chkTotalFOCShipment.Enabled = false;

                btnInvoiceAdd.Enabled = false; btnInvoiceAdd.CssClass = "buttoncolor";
                btnContainerAdd.Enabled = false; btnContainerAdd.CssClass = "buttoncolor";
                btnFinancialDocumentNo.Enabled = false; btnFinancialDocumentNo.CssClass = "buttoncolor";
                btnFinancialDocumentNoView.Enabled = false; btnFinancialDocumentNoView.CssClass = "buttoncolor";

                btnAddNewtems.Enabled = false;
                LinkButton lbtnDelAllItem = dgvInvoiceDetails.HeaderRow.FindControl("lbtnDelAllItem") as LinkButton;
                lbtnDelAllItem.CssClass = "buttoncolor";
                lbtnDelAllItem.OnClientClick = "";
                lbtnDelAllItem.Enabled = false;
            }
        }

        private void loadUOM()
        {
            List<ComboBoxObject> oItem = CHNLSVC.Financial.GET_PKG_UOM();
            ddlUOM.DataSource = oItem;
            ddlUOM.DataTextField = "Text";
            ddlUOM.DataValueField = "Value";
            ddlUOM.DataBind();
        }

        private void sendApprovedMails(string BLNum)
        {
            try
            {
                string err = string.Empty;
                string BccAdd = string.Empty;
                string SMSBody = string.Empty;

                List<REF_ALERT_SETUP> oItems = CHNLSVC.General.GET_REF_ALERT_SETUP(Session["UserCompanyCode"].ToString(), "SBU", Session["UserSBU"].ToString(), "m_Trans_Imports_BL", "A");
                if (oItems != null && oItems.Count > 0)
                {
                    string msgBody = PopulateBody("~/Common/template.html");
                    foreach (REF_ALERT_SETUP item in oItems)
                    {
                        if (item.Rals_via_email == 1)
                        {
                            SystemUser oSecUser = CHNLSVC.Security.GetUserByUserID(item.Rals_to_user);
                            SystemUser oAppUser = CHNLSVC.Security.GetUserByUserID(Session["UserID"].ToString());
                            if (oSecUser != null && !string.IsNullOrEmpty(oSecUser.Se_usr_id) && !string.IsNullOrEmpty(oSecUser.se_Email))
                            {
                                string content = "BL : " + BLNum + " is approved by " + oAppUser.Se_usr_name;
                                msgBody = msgBody.Replace("[Receiver]", oSecUser.Se_usr_name);
                                msgBody = msgBody.Replace("[content]", content);
                                msgBody = msgBody.Replace("[Header]", "Bill Of Lading Approval Note");

                                Int32 result = CHNLSVC.MsgPortal.SendEMail_HTML(oSecUser.se_Email, "Approval Note", msgBody, "", out err, BccAdd);
                            }
                            if (oSecUser != null && !string.IsNullOrEmpty(oSecUser.Se_usr_id) && !string.IsNullOrEmpty(oSecUser.se_Mob))
                            {
                                string content = "BL : " + BLNum + "is approved by " + oAppUser.Se_usr_name;
                                OutSMS _out = new OutSMS();
                                _out.Msg = content;
                                _out.Msgstatus = 0;
                                _out.Msgtype = "S";
                                _out.Receivedtime = DateTime.Now;
                                _out.Receiver = "";
                                _out.Receiverphno = oSecUser.se_Mob;
                                _out.Senderphno = oSecUser.se_Mob;
                                _out.Refdocno = "0";
                                _out.Sender = "Message Agent";
                                _out.Createtime = DateTime.Now;
                                int resultSms = CHNLSVC.General.SaveSMSOut(_out);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private string PopulateBody(string path)
        {
            string body = File.ReadAllText(Server.MapPath(path));
            return body;
        }

        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDocNumber.Text))
            {
                DisplayMessage("Please select BL", 2);
                return;
            }

            MoPopupGu.Show();
        }


        protected void lbtnPrint_Click(object sender, EventArgs e)
        {

            try
            {

                string url = "";
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                if (string.IsNullOrEmpty(txtDocNumber.Text))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please Select Assessment No')", true);
                    return;
                }

                else
                {
                    InvReportPara _invRepPara = new InvReportPara();

                    _invRepPara._GlbCompany = Session["UserCompanyCode"].ToString();
                    _invRepPara._GlbDocNo = txtDocNumber.Text;
                    _invRepPara._GlbUserID = Session["UserID"].ToString();

                    obj.GetShippingDetailReport(_invRepPara);
                    PrintPDF(targetFileName, obj._shippingInvoice);
                    url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    CHNLSVC.MsgPortal.SaveReportErrorLog("CusdecAsses", "CusdecAsses", "Run Ok", Session["UserID"].ToString());

                }

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
                CHNLSVC.MsgPortal.SaveReportErrorLog("Bill of lading Print", "BillOfLading", "Run Ok", Session["UserID"].ToString());
            }
        }



        protected void btnupdate_Click(object sender, EventArgs e)
        {
            if (hdfUpdate.Value == "No")
            {
                return;
            }
            List<ImportsBLHeader> _bl = new List<ImportsBLHeader>();
            ImportsBLHeader __blObj = new ImportsBLHeader();
            __blObj.Ib_doc_no = txtDocNumber.Text;
            __blObj.Ib_gurr_no = txtguno.Text;
            __blObj.Ib_issue_date = Convert.ToDateTime(txtissuedate.Text);
            __blObj.Ib_ex_date = Convert.ToDateTime(txtexDate.Text);
            __blObj.Ib_cancel_date = Convert.ToDateTime(txtCancel.Text);
            __blObj.Ib_is_cancel = chkIscancel.Checked;
            _bl.Add(__blObj);
            string err = string.Empty;
            Int32 result = CHNLSVC.Financial.UPDATE_BL(_bl, out err);
            if (result > 0)
            {
                DisplayMessage("Successfully updated. The B/L Number :" + txtDocNumber.Text, 3);
                clear();
            }
            else
            {
                DisplayMessage("Error :" + err, 4);
            }
        }

        protected void chkIscancel_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIscancel.Checked)
            {
                pnlcancl.Visible = true;
                MoPopupGu.Show();
            }
            else { pnlcancl.Visible = false; MoPopupGu.Show(); }
        }

        private void ExcRate()
        {
            DataTable ERateTbl = CHNLSVC.Financial.GetExchangeRate(Session["UserCompanyCode"].ToString(), lblCurCode.Text, "LKR");
            if (ERateTbl != null)
            {
                if (ERateTbl.Rows.Count > 0)
                {

                    lbtnrate.Text = ERateTbl.Rows[0][5].ToString();

                }
                //else
                //{
                //    divalert.Visible = true;
                //    lblalert.Text = "Exchange rate not setup";
                //}
            }
            else
            {
                //divalert.Visible = true;
                //lblalert.Text = "Currency not setup";
            }
        }

        protected void txtShipmentDate_TextChanged(object sender, EventArgs e)
        {
            int _crep = (int)Session["CreditP"];
            txtCreditAllowDate.Text = Convert.ToDateTime(txtShipmentDate.Text).AddDays(_crep).ToString("dd/MMM/yyyy");
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtStatus.Text.ToString() == "Pending")
                {
                    DisplayMessage(txtDocNumber.Text.ToString() + "was already reset !", 2);
                    return;
                }

                if (txtDocNumber.Text.ToString() == "")
                {
                    DisplayMessage("Please Select BL No", 2);
                    return;
                }
                if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16029))
                {
                    DisplayMessage("Sorry, You have no permission! ( Advice: Required permission code : 16029)");
                    return;
                }

                //Check cusdec entry
                DataTable cusdecdata = CHNLSVC.Financial.GetCusdechdrByBL(Session["UserCompanyCode"].ToString(), txtDocNumber.Text.ToString());
                //Change bl hdr status
                if (cusdecdata.Rows.Count > 0)
                {
                    //cannot update
                    DisplayMessage("Cannot reset documents which already has a Cusdec Entry !", 2);
                    return;

                }
                else
                {
                    //update

                    int result = CHNLSVC.Financial.UpdateBLStatus(Session["UserCompanyCode"].ToString(), txtDocNumber.Text.ToString(), "P");
                    if (result > 0)
                    {
                        DisplayMessage(txtDocNumber.Text.ToString() + " has been reset successfully !", 3);
                        clear();
                    }
                    else
                    {
                        DisplayMessage("Get Error !!", 2);
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }


        protected void btnok_Click(object sender, EventArgs e)
        {
            confresult = true;

            if (Session["oImportsBLItems"] != null)
            {
                //GridViewRow row = dgvInvoiceDetails.s;
                // GridViewRow gvRow = dgvInvoiceDetails.Rows[index];

                GridViewRow row = (GridViewRow)dgvInvoiceDetails.Rows[dgvInvoiceDetails.EditIndex];
                TextBox txtIbi_pi_no = (TextBox)row.FindControl("txtIbi_pi_no");
                Label lblIbi_pi_line = (Label)row.FindControl("lblIbi_pi_line");
                TextBox orderQty = (TextBox)row.FindControl("txtIbi_qty");
                TextBox unitPrice = (TextBox)row.FindControl("txtIbi_pi_unit_rt");
                TextBox ItemCOde = (TextBox)row.FindControl("txtIbi_itm_cd");
                DropDownList ddlItemTag = (DropDownList)row.FindControl("ddlItemTag");
                DropDownList ddlSelectPriceBase = (DropDownList)row.FindControl("ddlSelectPriceBase");
                TextBox _shpseq = (TextBox)row.FindControl("txtSeqNo");

                oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];
                ImportsBLItems oEdiedItem = oImportsBLItems.Find(x => x.Ibi_pi_no == txtIbi_pi_no.Text.Trim() && x.Ibi_pi_line == Convert.ToInt32(lblIbi_pi_line.Text.Trim()));
                // oEdiedItem.Ibi_itm_cd = ItemCOde.Trim();
                string ItemTag = ddlItemTag.SelectedValue.ToString();
                string priceBasis = ddlSelectPriceBase.SelectedValue.ToString();

                int indexOf = oImportsBLItems.IndexOf(oEdiedItem);

                if (oEdiedItem == null)
                {
                    return;
                }
                oEdiedItem.Ibi_qty = Convert.ToDecimal(orderQty.Text);
                oEdiedItem.Ibi_bal_qty = Convert.ToDecimal(orderQty.Text);
                oEdiedItem.Ibi_unit_rt = Convert.ToDecimal(unitPrice.Text);
                oEdiedItem.isRecordStatus = 1;
                oEdiedItem.Ibi_tp = priceBasis;
                oEdiedItem.Ibi_tag = ItemTag;
                oEdiedItem.Ibi_anal_1 = _shpseq.Text.ToString();
                updateItemDetials(ItemCOde.Text);
                Session["oImportsBLItems"] = oImportsBLItems;
                dgvInvoiceDetails.EditIndex = -1;
                BindPInvoiceItems();
                confresult = false;
            }
            else
            {
                divWarning.Visible = true;
                lblAlert.Text = "Error";
            }

            ddlTradeTerms_SelectedIndexChanged(null, null);
        }
        protected void btnno_Click(object sender, EventArgs e)
        {
            confresult = false;
            PopupConfBox.Hide();
        }

        protected void lbtnDelAllItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkTotalFOCShipment.Checked)
                {
                    ddlPriceType.SelectedIndex = ddlPriceType.Items.IndexOf(ddlPriceType.Items.FindByText("FOC"));
                }
                else
                {
                    ddlPriceType.SelectedIndex = ddlPriceType.Items.IndexOf(ddlPriceType.Items.FindByText("Charge"));
                }
                if (Session["oImportsBLItems"] != null)
                {
                    oImportsBLItems = (List<ImportsBLItems>)Session["oImportsBLItems"];
                    _PIITEMBACKUP = oImportsBLItems;
                    //  ImportsBLItems oEdiedItem = oImportsBLItems[e.RowIndex];
                    //oEdiedItem.isRecordStatus = 1;
                    // oEdiedItem.Ibi_stus = 0;
                    oImportsBLItems = new List<ImportsBLItems>();
                    Session["oImportsBLItems"] = oImportsBLItems;
                    BindPInvoiceItems();

                    setCostValues();
                }
                else
                {
                    DisplayMessage("No items found !");
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("Error :" + ex, 4);
            }
        }



        protected void btnSbu_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/View/ADMIN/Home.aspx");
        }

        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                //ReportDocument Rel = new ReportDocument();
                ReportDocument rptDoc = (ReportDocument)_rpt;
                DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                rptDoc.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                //if (rbpdf.Checked)
                //{
                rptDoc.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                //}
                //if (rbexel.Checked)
                //{
                //    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.Excel;
                //}
                //if (rbexeldata.Checked)
                //{
                //    rptDoc.ExportOptions.ExportFormatType = ExportFormatType.Excel;
                //}
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

        protected void btnconfok_Click(object sender, EventArgs e)
        {

            confresult2 = 1;
            btnAddNewtems_Click(null, null);
        }
        protected void btnconfno_Click(object sender, EventArgs e)
        {
            _isDuplicate = false;
            confresult2 = 2;
            Userconfmsg.Hide();
            btnAddNewtems_Click(null, null);
        }

        protected void lbwupdate_Click(object sender, EventArgs e)
        {
            if (validateHeader())
            {
                if (Session["oHeader"] != null)
                {
                    oHeader = (ImportsBLHeader)Session["oHeader"];

                    //if (oHeader.Ib_stus == "A")
                    //{
                    //    DisplayMessage("Document is already approved. Cannot amend.", 2);
                    //    return;
                    //}
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16055))
                    {
                        string Msg = "Sorry, You have no permission to update!( Advice: Required permission code :16055)";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
                        return;
                    }
                    if (oHeader.Ib_stus == "C")
                    {
                        DisplayMessage("Document is cancelled. Can not amend", 2);
                        return;
                    }

                    oHeader.Ib_com = Session["UserCompanyCode"].ToString();
                    oHeader.Ib_sbu = Session["UserSBU"].ToString();

                    oHeader.Ib_bl_no = txtBLNumber.Text.Trim();

                    oHeader.Ib_rmk = txtRemark.Text;


                    oHeader.Ib_origin_cnty = txtCountyOfOrigin.Text.Trim();

                    //oHeader.Ib_desti_cnty = "LK";
                    //oHeader.Ib_tot_pkg = txtTotalPackages.Text.Trim();
                    //oHeader.Ib_pack_lst_no = txtPackingListNo.Text.Trim();
                    //oHeader.Ib_vessel_no = txtVessel.Text.Trim();
                    //oHeader.Ib_voyage = txtVoyage.Text.Trim();
                    //oHeader.Ib_loading_place = txtPlaceOfLanding.Text.Trim();
                    //oHeader.Ib_frm_port = ddlFromPort.SelectedValue.ToString();
                    //oHeader.Ib_to_port = ddlToPort.SelectedValue.ToString().Trim();
                    //oHeader.Ib_anal_3 = ddlFinalPort.SelectedValue.ToString().Trim();
                    //oHeader.Ib_etd = Convert.ToDateTime(txtETD.Text);
                    //oHeader.Ib_eta = Convert.ToDateTime(txtETA.Text);
                    //oHeader.Ib_cur_cd = lblCurCode.Text;
                    //oHeader.Ib_anal_1 = ddlTradeTerms.SelectedValue.ToString();
                    //oHeader.Ib_anal_2 = ddlUOM.SelectedValue.ToString();
                    //oHeader.Ib_anal_4 = string.Empty;
                    //oHeader.Ib_entry_no = string.Empty;
                    //oHeader.Ib_bl_ref_no = txtReferenceNum.Text.Trim();
                    //oHeader.Ib_carry_tp = ddlCaringType.SelectedValue.ToString();
                    oHeader.Ib_tos = ddlModeofShipment.SelectedValue.ToString();

                    MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                    MasterCompany _mstCom = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());
                    string curCd = "LKR";
                    if (_mstCom != null)
                    {
                        if (!string.IsNullOrEmpty(_mstCom.Mc_cur_cd))
                        {
                            curCd = _mstCom.Mc_cur_cd;
                        }
                    }
                    MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), lblCurCode.Text.Trim(), oHeader.Ib_bl_dt, curCd, string.Empty);
                    if (_exc1 != null)
                    {
                        if (_exc1.Mer_id != null)
                        {
                            oHeader.Ib_ex_rt = _exc1.Mer_bnkbuy_rt;
                        }
                    }
                    if (oHeader.Ib_ex_rt == 0)
                    {
                        DisplayMessage("Please update exchange rate");
                        return;
                    }

                    //oHeader.Ib_tot_bl_amt = (String.IsNullOrEmpty(txtTotalOrderValueIn.Text)) ? 0 : Convert.ToDecimal(txtTotalOrderValueIn.Text);
                    oHeader.Ib_tot_bl_amt = (String.IsNullOrEmpty(txtBlValue.Text)) ? 0 : Convert.ToDecimal(txtBlValue.Text);
                    oHeader.Ib_loc_of_goods = txtlocationOfGoods.Text;
                    oHeader.Ib_doc_clear_dt = Convert.ToDateTime(txtDocReceivedDate.Text);
                    oHeader.Ib_doc_rec_dt = Convert.ToDateTime(txtDocReceivedDate.Text);
                    oHeader.Ib_ignore = 0;
                    oHeader.Ib_anal_4 = string.Empty;
                    oHeader.Ib_entry_no = string.Empty;
                    // oHeader.Ib_stus = "P";
                    oHeader.Ib_cre_by = Session["UserID"].ToString();
                    oHeader.Ib_cre_dt = DateTime.Now;
                    oHeader.Ib_mod_by = Session["UserID"].ToString();
                    oHeader.Ib_mod_dt = DateTime.Now;
                    oHeader.Ib_session_id = Session["SessionID"].ToString();
                    //lakshan 2016 Mar 16 add IB_STANDARD_LEAD
                    oHeader.Ib_standard_lead = 0;
                    SupplierPort _supPort = CHNLSVC.General.GetSupplierPort(new SupplierPort()
                    {
                        MSPR_COM = Session["UserCompanyCode"].ToString(),
                        MSPR_CD = oHeader.Ib_supp_cd,
                        MSPR_TP = "S",
                        MSPR_ACT = 1
                    });
                    if (_supPort != null)
                    {
                        oHeader.Ib_standard_lead = _supPort.MSPR_LEAD_TIME;
                    }
                    else
                    {
                        DisplayMessage("Please update supplier Lead time CMB ");
                        return;
                    }

                    oImportsBLContainers = (List<ImportsBLContainer>)Session["oImportsBLContainers"];


                    bool isNewRocord = false;
                    String err;
                    String outBlNumber = txtDocNumber.Text;
                    MasterAutoNumber _masterAuto = new MasterAutoNumber();

                    Int32 result = CHNLSVC.Financial.UpdateBillOfLadingWebWharf(oHeader, oImportsBLContainers, out err);
                    if (result > 0)
                    {
                        DisplayMessage("Successfully updated. The B/L Number :" + outBlNumber, 3);
                        clear();
                    }
                    else
                    {
                        DisplayMessage("Error :" + err, 4);
                    }
                }
            }
        }

        protected void btndgvpendSelect_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label item = (Label)row.FindControl("lblItemCode");
            txtItem.Text = item.Text;
            txtItem_TextChanged(null, null);
        }

        protected void ddlTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_tagChgRestrict)
            {
                ddlTag.SelectedIndex = ddlTag.Items.IndexOf(ddlTag.Items.FindByValue(_tagChgRestrictVal));
            }
        }

        protected void txtissuedate_TextChanged(object sender, EventArgs e)
        {
            txtexDate.Text = Convert.ToDateTime(txtissuedate.Text).AddMonths(1).ToString();
        }

        protected void lbtnExcelUploadClose_Click(object sender, EventArgs e)
        {
            popupExcel.Hide();
        }

        protected void lbtnUploadExcelFile_Click(object sender, EventArgs e)
        {
            try
            {
                lblExcelUploadError.Visible = false;
                lblExcelUploadError.Text = "";
                if (fileUploadExcel.HasFile)
                {
                    string FileName = Path.GetFileName(fileUploadExcel.PostedFile.FileName);
                    string Extension = Path.GetExtension(fileUploadExcel.PostedFile.FileName);

                    if (Extension == ".xls" || Extension == ".XLS" || Extension == ".xlsx" || Extension == ".XLSX")
                    {

                    }
                    else
                    {
                        lblExcelUploadError.Visible = true;
                        lblExcelUploadError.Text = "Please select a valid excel (.xls or .xlsx) file";
                    }

                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string ValidateFilePath = Server.MapPath(FolderPath + FileName);
                    fileUploadExcel.SaveAs(ValidateFilePath);
                    _filPath = ValidateFilePath;
                    UploadData();
                    _showExcelPop = false;
                    popupExcel.Hide();
                    //  DispMsg("Excel file upload completed. Do you want to process ? ");
                }
                else
                {
                    DispMsg("Please select the correct upload file path !");
                }
                if (lblExcelUploadError.Visible == true)
                {
                    _showExcelPop = true;
                    popupExcel.Show();
                }
            }
            catch (Exception ex)
            {
                DispMsg("Error Occurred :" + ex.Message, "E");
            }
        }

        protected void lbtnExcClose_Click(object sender, EventArgs e)
        {

        }



        protected void btnCancelProcess_Click(object sender, EventArgs e)
        {
            popOpExcSave.Hide();
        }
        public string ConnectionString(string FileName, string Header)
        {
            OleDbConnectionStringBuilder Builder = new OleDbConnectionStringBuilder();
            if (System.IO.Path.GetExtension(FileName).ToUpper() == ".XLS")
            {
                Builder.Provider = "Microsoft.Jet.OLEDB.4.0";
                Builder.Add("Extended Properties", string.Format("Excel 8.0;IMEX=1;HDR={0};", Header));
            }
            else
            {
                Builder.Provider = "Microsoft.ACE.OLEDB.12.0";
                Builder.Add("Extended Properties", string.Format("Excel 12.0;IMEX=1;HDR={0};", Header));
            }

            Builder.DataSource = FileName;
            return Builder.ConnectionString;
        }
        public DataTable[] ReadExcelData(string FileName, out string _error)
        {
            _error = "";
            #region Excel Process
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();
            using (OleDbConnection cn = new OleDbConnection { ConnectionString = ConnectionString(FileName, "No") })
            {
                try
                {
                    cn.Open();
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable dtExcelSchema;
                    cmdExcel.Connection = cn;

                    dtExcelSchema = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    cn.Close();

                    //Read Data from First Sheet
                    cn.Open();
                    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(Tax);
                }
                catch (Exception ex)
                {
                    _error = ex.Message;
                    return new DataTable[] { Tax };
                }
                return new DataTable[] { Tax };
            }
            #endregion
        }
        private void UploadData()
        {
            try
            {
                _blItmsUpItmList = new List<ImportsBLItems>();
                string _error = "";
                #region Excel hdr data read
                if (string.IsNullOrEmpty(_filPath))
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "File Path Invalid Please Upload ";
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                DataTable[] GetExecelTbl = ReadExcelData(_filPath, out _error);
                if (!string.IsNullOrEmpty(_error))
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = _error;
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                DateTime _tmpDt = new DateTime();
                if (GetExecelTbl == null)
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "Data not available in excel ";
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }

                #endregion
                DataTable _dtExData = GetExecelTbl[0];
                #region MyRegion
                if (_dtExData == null)
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "Data not available in excel ";
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                if (_dtExData.Rows.Count < 2)
                {
                    lblExcelUploadError.Visible = true;
                    lblExcelUploadError.Text = "Data not available in excel ";
                    _showExcelPop = true;
                    popupExcel.Show();
                    return;
                }
                #endregion
                ImportsBLItems _blItm = new ImportsBLItems();
                for (int i = 1; i < _dtExData.Rows.Count; i++)
                {
                    #region column null check
                    if (string.IsNullOrEmpty(_dtExData.Rows[i][0].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][1].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][2].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][3].ToString())
                        && string.IsNullOrEmpty(_dtExData.Rows[i][4].ToString())
                        )
                    {
                        continue;
                    }
                    #endregion
                    #region itm
                    _blItm = new ImportsBLItems();
                    _blItm.Ibi_itm_cd = string.IsNullOrEmpty(_dtExData.Rows[i][0].ToString()) ? "" : _dtExData.Rows[i][0].ToString().Trim().ToUpper();
                    _blItm.Tmp_itm_model = string.IsNullOrEmpty(_dtExData.Rows[i][1].ToString()) ? "" : _dtExData.Rows[i][1].ToString().Trim().ToUpper();
                    _blItm.Tmp_ibi_qty = string.IsNullOrEmpty(_dtExData.Rows[i][2].ToString()) ? "" : _dtExData.Rows[i][2].ToString().Trim();
                    _blItm.Ibi_tp = string.IsNullOrEmpty(_dtExData.Rows[i][3].ToString()) ? "" : _dtExData.Rows[i][3].ToString().Trim().ToUpper();
                    _blItm.Tmp_ibi_unit_rt = string.IsNullOrEmpty(_dtExData.Rows[i][4].ToString()) ? "" : _dtExData.Rows[i][4].ToString().Trim();
                    _blItm.Ibi_tag = string.IsNullOrEmpty(_dtExData.Rows[i][5].ToString()) ? "" : _dtExData.Rows[i][5].ToString().Trim().ToUpper();
                    #endregion
                    _blItmsUpItmList.Add(_blItm);
                }
                popupExcel.Hide();
                popOpExcSave.Show();
                //ProcessExcelData();
            }
            catch (Exception ex)
            {
                lblExcelUploadError.Visible = true;
                lblExcelUploadError.Text = ex.Message;
                _showExcelPop = true;
                popupExcel.Show();
                return;
            }
        }
        protected void btnGenOrdPlans_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessExcelData();
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message);
            }
        }
        private decimal ToDecimal(string _qty)
        {
            decimal d = 0, td = 0;
            d = decimal.TryParse(_qty, out td) ? Convert.ToDecimal(_qty) : 0;
            return d;
        }
        private void ProcessExcelData()
        {
            List<TmpValidation> _errList = new List<TmpValidation>();
            TmpValidation _err = new TmpValidation();
            string _erMsg = "";
            MasterItem _mstItm = new MasterItem();
            foreach (var item in _blItmsUpItmList)
            {
                #region validation
                _erMsg = "";
                _err = new TmpValidation();
                _err.Sad_itm_line ++;
                if (_errList.Count > 0)
                {
                    //_err.Sad_itm_line = _errList.Max(c => c.Sad_itm_line) + 1;
                    //_err.Sad_itm_line++;
                }
                if (string.IsNullOrEmpty(item.Ibi_itm_cd))
                {
                    if (!string.IsNullOrEmpty(item.Tmp_itm_model))
                    {
                        bool _isCorMod = false;
                        List<MasterItemModel> _mstItmModList = CHNLSVC.General.GetItemModelNew(item.Tmp_itm_model);
                        if (_mstItmModList != null)
                        {
                            if (_mstItmModList.Count > 0)
                            {
                                _isCorMod = true;
                            }
                        }
                        if (!_isCorMod)
                        {
                             _erMsg = "Please enter valid model # ";
                             _err.Sad_itm_cd = item.Tmp_itm_model;
                             _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                        }
                        else
                        {
                            List<MasterItem> _itmlist = CHNLSVC.Inventory.GetAllItemByModel(item.Tmp_itm_model);
                            if (_itmlist!=null)
                            {
                                if (_itmlist.Count==0)
                                {
                                    _erMsg = "Item not found ! ";
                                    _err.Sad_itm_cd = item.Tmp_itm_model;
                                    _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                                }
                                else if (_itmlist.Count==1)
                                {
                                    item.Ibi_itm_cd = _itmlist[0].Mi_cd;
                                }
                                else
                                {
                                    _erMsg = "Multiple Items found !";
                                    _err.Sad_itm_cd = item.Tmp_itm_model;
                                    _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                                }
                            }
                        }
                    }
                    else
                    {
                        _erMsg = "Please enter item code # ";
                        _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                    }
                }
                if (!string.IsNullOrEmpty(item.Ibi_itm_cd))
                {
                    _mstItm = CHNLSVC.General.GetItemMaster(item.Ibi_itm_cd);
                    if (_mstItm == null)
                    {
                        _erMsg = "Invalid item code ! ";
                        _err.Sad_itm_cd = item.Ibi_itm_cd;
                        _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                    }
                    if (_mstItm != null)
                    {
                        if (!_mstItm.Mi_act)
                        {
                            _erMsg = "Invalid item code ! ";
                            _err.Sad_itm_cd = item.Ibi_itm_cd;
                            _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                        }
                    }
                }
                else
                {
                    _erMsg = "Please enter item code # ";
                    _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                }
                if (string.IsNullOrEmpty(item.Tmp_ibi_qty))
                {
                    _erMsg = "Please enter quentity ";
                    _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                }
                if (!string.IsNullOrEmpty(item.Tmp_ibi_qty))
                {
                    item.Ibi_qty = ToDecimal(item.Tmp_ibi_qty);
                }
                if (item.Ibi_qty <= 0)
                {
                    _erMsg = "Please enter valid quentity ";
                    _err.Sad_itm_cd = item.Ibi_itm_cd;
                    _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                }
                if (_mstItm != null)
                {
                    if (_mstItm.Mi_is_ser1 != -1)
                    {
                        if ((item.Ibi_qty % 1) > 0)
                        {
                            _erMsg = "Please enter valid quentity ";
                            _err.Sad_itm_cd = item.Ibi_itm_cd;
                            _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                        }
                    }
                }
                if (string.IsNullOrEmpty(item.Ibi_tp))
                {
                    _erMsg = "Please enter price basis ";
                    _err.Sad_itm_cd = item.Ibi_tp;
                    _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                }
                if (!string.IsNullOrEmpty(item.Ibi_tp))
                {
                    if (item.Ibi_tp != "C")
                    {
                        if (item.Ibi_tp != "F")
                        {
                            _erMsg = "Please enter valid price basis ";
                            _err.Sad_itm_cd = item.Ibi_tp;
                            _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                        }
                    }
                }
                if (string.IsNullOrEmpty(item.Tmp_ibi_unit_rt))
                {
                    _erMsg = "Please enter unit price ";
                    _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                }
                if (!string.IsNullOrEmpty(item.Tmp_ibi_unit_rt))
                {
                    item.Ibi_unit_rt = ToDecimal(item.Tmp_ibi_unit_rt);
                }
                if (item.Ibi_unit_rt <= 0)
                {
                    _erMsg = "Please enter valid price ";
                    _err.Sad_itm_cd = item.Ibi_unit_rt.ToString();
                    _err.errorMsg = string.IsNullOrEmpty(_err.errorMsg) ? _erMsg : _err.errorMsg;
                }
                if (!string.IsNullOrEmpty(_err.errorMsg))
                {
                    _errList.Add(_err);
                }
	            #endregion
            }
            if (_errList.Count > 0)
            {
                dgvError.DataSource = _errList;
                dgvError.DataBind();
                _showErrPop = true;
                popupErro.Show();
            }
            else
            {
                Int32 _c = 0;
                foreach (var item in _blItmsUpItmList)
                {
                    _c++;
                    if (_c==200)
                    {
                        int x = 0;
                    }
                    txtItem.Text = item.Ibi_itm_cd;
                    txtItem_TextChanged(null, null);
                    txtQty.Text = item.Ibi_qty.ToString();
                    txtQty_TextChanged(null, null);
                    ddlPriceType.SelectedIndex = ddlPriceType.Items.IndexOf(ddlPriceType.Items.FindByValue(item.Ibi_tp));
                    txtUnitPrice.Text = item.Ibi_unit_rt.ToString();
                    txtUnitPrice_TextChanged(null, null);
                    ddlTag.SelectedIndex = ddlTag.Items.IndexOf(ddlTag.Items.FindByValue(item.Ibi_tag));
                    _bindGrid = true;
                    _isExcUpload = true;
                    btnAddNewtems_Click(null, null);
                    _isExcUpload = false;
                    _bindGrid = false;
                }
                BindPInvoiceItems(); 
            }
        }

        protected void btnExcelDataUpload_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            _showExcelPop = true;
            popupExcel.Show();
        }

        protected void lbtnItemChg_Click(object sender, EventArgs e)
        {
            try
            {
                _selectedPiNo = "";
                _selectedPiLine = 0;
                GridViewRow row = ((GridViewRow)((LinkButton)sender).NamingContainer);
                Label lblIbi_pi_no = row.FindControl("lblIbi_pi_no") as Label;
                Label lblIbi_pi_line = row.FindControl("lblIbi_pi_line") as Label;
                _selectedPiNo = lblIbi_pi_no.Text;
                _selectedPiLine = Convert.ToInt32(lblIbi_pi_line.Text);
                popItmCdChg.Show();
                _showItmCdChg = true;
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message,"E");
            }
        }

        protected void ItmCdChgNo_Click(object sender, EventArgs e)
        {
            _showItmCdChg = false;
            popItmCdChg.Hide();
        }

        protected void txtchgItmCd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool b2 = false;
                string toolTip = "";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, "ITEM", txtchgItmCd.Text.Trim().ToUpper());
                foreach (DataRow row in _result.Rows)
                {
                    if (!string.IsNullOrEmpty(row["ITEM"].ToString()))
                    {
                        if (txtchgItmCd.Text.ToUpper() == row["ITEM"].ToString())
                        {
                            b2 = true;
                            toolTip = row["Description"].ToString();
                            break;
                        }
                    }
                }
                if (!b2)
                {
                    txtchgItmCd.ToolTip = "";
                    txtchgItmCd.Text = "";
                    txtchgItmCd.Focus();
                    DispMsg("Please enter a valid item code");
                    return;
                }
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }

        protected void lbtnSerItemCd_Click(object sender, EventArgs e)
        {
            ValidateTrue();
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
            DataTable result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "txtChgItem";
            result.Columns["MODEL"].SetOrdinal(0);
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpUserPopup.Show(); Session["IsSearch"] = true;
        }
        protected void btnItmCdChgOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtchgItmCd.Text))
                {
                    DispMsg("Please enter item code !"); return;
                }
                BusEntityItem _busEntItm = CHNLSVC.General.GetBuninessEntityItemBySupplierItm(Session["UserCompanyCode"].ToString(), txtExporter.Text.Trim().ToUpper(), txtchgItmCd.Text.ToUpper());
                if (_busEntItm==null)
                {
                    string msg = "Please allocate this item : " + txtchgItmCd.Text.ToUpper() + " for Exporter";
                    DispMsg(msg); return;
                }
               // _selectedPiNo = lblIbi_pi_no.Text;
                
            }
            catch (Exception ex)
            {
                DispMsg(ex.Message, "E");
            }
        }
    }
}