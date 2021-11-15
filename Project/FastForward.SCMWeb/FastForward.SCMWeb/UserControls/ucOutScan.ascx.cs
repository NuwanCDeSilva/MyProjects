using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Transaction.Inventory;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.UserControls
{
    public partial class ucOutScan : System.Web.UI.UserControl
    {
        Base _base = new Base();

        public string _SerialSearchType = string.Empty;
        private List<ReptPickSerials> serial_list = null;
        MasterItem msitem = new MasterItem();
        private string _chargeType = string.Empty;
        private MasterItem _itemdetail = null;
        bool _isDecimalAllow = false;
        private List<InventoryRequestItem> ItemList = new List<InventoryRequestItem>();
        private List<ReptPickSerials> reptPickSerial = new List<ReptPickSerials>();
        public bool _ispda=false;
        public delegate void OnButtonClick(string strValue);

        #region public properties

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool _derectOut
        {
            get
            {
                if (ViewState["_derectOut"] == null)
                {
                    return false;
                }
                else { return Convert.ToBoolean(ViewState["_derectOut"].ToString()); }
            }
            set { ViewState["_derectOut"] = value; }
        }
        public bool _isQutationBaseDO
        {
            get
            {
                if (ViewState["_isQutationBaseDO"] == null)
                {
                    return false;
                }
                else { return Convert.ToBoolean(ViewState["_isQutationBaseDO"].ToString()); }
            }
            set { ViewState["_isQutationBaseDO"] = value; }
        }

        public string _pkgTblTp
        {
            get { if (ViewState["_pkgTblTp"] == null) return null; return ViewState["_pkgTblTp"].ToString(); }
            set { ViewState["_pkgTblTp"] = value; }
        }
        public bool? isResQTY
        {
            get { if (ViewState["isResQTY"] == null) return false; return Convert.ToBoolean(ViewState["isResQTY"]); }
            set { ViewState["isResQTY"] = value; }
        }
        public bool _batchBaseOut
        {
            get { if (Session["_batchBaseOut"] != null) { return (bool)Session["_batchBaseOut"]; } else { return false; } }
            set { Session["_batchBaseOut"] = value; }
        }
        public string _batchNo
        {
            get { if (ViewState["_batchNo"] == null) return null; return ViewState["_batchNo"].ToString(); }
            set { ViewState["_batchNo"] = value; }
        }
        public bool _prodPlanBaseOut
        {
            get { if (Session["_prodPlanBaseOut"] != null) { return (bool)Session["_prodPlanBaseOut"]; } else { return false; } }
            set { Session["_prodPlanBaseOut"] = value; }
        }
        public string _prodNo
        {
            get { if (ViewState["_prodNo"] == null) return null; return ViewState["_prodNo"].ToString(); }
            set { ViewState["_prodNo"] = value; }
        }
        public string adjustmentTypeValue
        {
            //get { if (ViewState["adjustmentTypeValue"] == null) return null; return ViewState["adjustmentTypeValue"].ToString(); }
            //set { ViewState["adjustmentTypeValue"] = value; }
            get { return adjustmentTypeValue; }
            set { ViewState["adjustmentTypeValue"] = value; }
        }
        public string adjustmentType
        {
            get { return adjustmentType; }
            set { ViewState["adjustmentType"] = value; }
        }
        public string DOC_No
        {
            get { if (ViewState["DOC_No"] == null) return null; return ViewState["DOC_No"].ToString(); }
            set { ViewState["DOC_No"] = value; }
        }
        public string doc_tp
        {
            get { if (ViewState["doc_tp"] == null) return null; return ViewState["doc_tp"].ToString(); }
            set { ViewState["doc_tp"] = value; }
        }
        public string userSeqNo
        {
            get { if (ViewState["userSeqNo"] == null) return null; return ViewState["userSeqNo"].ToString(); }
            set { ViewState["userSeqNo"] = value; }
        }
        public bool? isApprovalSend
        {
            get { if (ViewState["isApprovalSend"] == null) return null; return Convert.ToBoolean(ViewState["isApprovalSend"]); }
            set { ViewState["isApprovalSend"] = value; }
        }
        public List<InventoryRequestItem> ScanItemList
        {
            get { if (ViewState["ItemList"] != null) ItemList = (List<InventoryRequestItem>)ViewState["ItemList"]; return ItemList; }
            set { ItemList = value; ViewState["ItemList"] = value; }
        }
        public List<ReptPickSerials> PickSerial
        {
            get { if (ViewState["reptPickSerial"] != null) reptPickSerial = (List<ReptPickSerials>)ViewState["reptPickSerial"]; return reptPickSerial; }
            set { reptPickSerial = value; ViewState["reptPickSerial"] = value; }
        }

        public Panel PNLTobechange
        {
            get { return pnlTobechange; }
            set { pnlTobechange = value; }
        }
        public TextBox TXTItemCode
        {
            get { return txtItemCode; }
            set { txtItemCode = value; }
        }
        public DropDownList DDLStatus
        {
            get { return ddlStatus; }
            set { ddlStatus = value; }
        }
        public DropDownList DDLBOXTP
        {
            get { return ddlBoxTp; }
            set { ddlBoxTp = value; }
        }
        public LinkButton LBTNAdd
        {
            get { return lbtnAdd; }
            set { lbtnAdd = value; }
        }

        public LinkButton LBSERIALSEARChnew
        {
            get { return lbseradd; }
            set { lbseradd = value; }
        }

        public LinkButton LBSERIALSEARCh
        {
            get { return lbtnSerialI; }
            set { lbtnSerialI = value; }
        }
        public bool ISPDA
        {
            get { return _ispda; }
            set { _ispda = value; }
        }
        //Lakshan 17 May 2016
        public string PageName
        {
            get { if (Session["PageName"] == null) return null; return Session["PageName"].ToString(); }
            set { Session["PageName"] = value; }
        }

        //Tharaka 2015-10-03
        public string ScanDocument
        {
            get { if (ViewState["ScanDocument"] == null) return null; return ViewState["ScanDocument"].ToString(); }
            set { ViewState["ScanDocument"] = value; }
        }
        public decimal PopupQty
        {
            get { if (ViewState["PopupQty"] == null) return 0; return Convert.ToDecimal(ViewState["PopupQty"]); }
            set { ViewState["PopupQty"] = value; }
        }
        public decimal ApprovedQty
        {
            get { if (ViewState["ApprovedQty"] == null) return 0; return Convert.ToDecimal(ViewState["ApprovedQty"]); }
            set { ViewState["ApprovedQty"] = value; }
        }
        public decimal ScanQty
        {
            get { if (ViewState["ScanQty"] == null) return 0; return Convert.ToDecimal(ViewState["ScanQty"]); }
            set { ViewState["ScanQty"] = value; }
        }
        public string ItemStatus
        {
            get { if (ViewState["ItemStatus"] == null) return null; return ViewState["ItemStatus"].ToString(); }
            set { ViewState["ItemStatus"] = value; }
        }
        public Int32 ItemLineNo
        {
            get { if (ViewState["ItemLineNo"] == null) return 0; return Convert.ToInt32(ViewState["ItemLineNo"]); }
            set { ViewState["ItemLineNo"] = value; }
        }
        public string JobNo
        {
            get { if (ViewState["JobNo"] == null) return null; return ViewState["JobNo"].ToString(); }
            set { ViewState["JobNo"] = value; }
        }
        public string MainItemCode
        {
            get { if (ViewState["MainItemCode"] == null) return null; return ViewState["MainItemCode"].ToString(); }
            set { ViewState["MainItemCode"] = value; }
        }
        public Int32 JobLineNo
        {
            get { if (ViewState["JobLineNo"] == null) return 0; return Convert.ToInt32(ViewState["JobLineNo"]); }
            set { ViewState["JobLineNo"] = value; }
        }
        public bool IsCheckStatus
        {
            get { if (ViewState["IsCheckStatus"] == null) return false; return Convert.ToBoolean(ViewState["IsCheckStatus"]); }
            set { ViewState["IsCheckStatus"] = value; }
        }
        public Int32 ModuleTypeNo
        {
            get { if (ViewState["ModuleTypeNo"] == null) return 0; return Convert.ToInt32(ViewState["ModuleTypeNo"]); }
            set { ViewState["ModuleTypeNo"] = value; }
        }

        public bool IsBinTransfer
        {
            get { if (ViewState["IsBinTransfer"] == null) return false; return Convert.ToBoolean(ViewState["IsBinTransfer"]); }
            set { ViewState["IsBinTransfer"] = value; }
        }

        public string BinCode
        {
            get { if (ViewState["BinCode"] == null) return null; return ViewState["BinCode"].ToString(); }
            set { ViewState["BinCode"] = value; }
        }
        public string ToBinCode
        {
            get { if (ViewState["ToBinCode"] == null) return null; return ViewState["ToBinCode"].ToString(); }
            set { ViewState["ToBinCode"] = value; }
        }

        public bool ReqSupplier
        {
            get { if (ViewState["ReqSupplier"] == null) return false; return Convert.ToBoolean(ViewState["ReqSupplier"]); }
            set { ViewState["ReqSupplier"] = value; }
        }
        public bool isConsReturnNote
        {
            get { if (ViewState["isConsReturnNote"] == null) return false; return Convert.ToBoolean(ViewState["isConsReturnNote"]); }
            set { ViewState["isConsReturnNote"] = value; }
        }
        public Int32 direction
        {
            get { if (ViewState["direction"] == null) return 0; return Convert.ToInt32(ViewState["direction"]); }
            set { ViewState["direction"] = value; }
        }

        public bool isbatchserial
        {
            get { if (Session["isbatchserial"] == null) return false; return Convert.ToBoolean(Session["isbatchserial"]); }
            set { Session["isbatchserial"] = value; }
        }
        public string Batchno
        {
            get { if (Session["Batchno"] == null) return null; return Session["Batchno"].ToString(); }
            set { Session["Batchno"] = value; }
        }
        #endregion public properties

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PageClear();
                    ListClear();
                    loadItemStatus();
                    Session["Binnew"] = "";
                    Session["itemcode"] = "";
                }
                else
                {
                    if (_ispda == false)
                    {
                        Defaul.Visible = true;
                        pnlPDA.Visible = false;
                    }
                    else
                    {
                        pnlPDA.Visible = true;
                        Defaul.Visible = false;
                    }
                    if (Session["SP"] != null)
                    {
                        UserPopup.Show();
                        Session["SP"] = "";
                    }

                    loadBinCode();

                    string excelpoup = (string)Session["EXCELPOPUP"];

                    if (!string.IsNullOrEmpty(excelpoup))
                    {
                        mpexcel.Show();
                    }
                    else
                    {
                        mpexcel.Hide();
                    }
                }
                if (isApprovalSend == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please set isApprovalSend value.');", true);
                    throw new Exception("Please set isApprovalSend value.");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtnItemCode_Click(object sender, EventArgs e)
        {
            try
            {

                Session["SP"] = "show";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = string.Empty;
                DataTable result = new DataTable();
                if (!IsBinTransfer)
                {
                    if (!_batchBaseOut)
                    {
                        if (!_prodPlanBaseOut)
                        {
                            SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                            result = _base.CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                            lblvalue.Text = "3";
                        }
                        else
                        {
                            SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlanItem);
                            result = _base.CHNLSVC.CommonSearch.GetItemSearchDataForProductionPlan(SearchParams, null, null);
                            lblvalue.Text = "ProductionPlanItem";
                        }
                    }
                    else
                    {
                        SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BatchBase);
                        result = _base.CHNLSVC.CommonSearch.GetItemSearchDataForBatch(SearchParams, null, null);
                        lblvalue.Text = "BatchBase";
                    }
                }
                else
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemWithBin);
                    result = _base.CHNLSVC.CommonSearch.GetItemSearchDataWithBIN(SearchParams, null, null);
                    lblvalue.Text = "ItemWithBin";
                }
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                DateRow.Visible = false;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        public void ListClear()
        {
            ViewState["SerialID"] = string.Empty;
            ViewState["reptPickSerial"] = null;
            ViewState["SerialList"] = null;
            ItemList = new List<InventoryRequestItem>();
            reptPickSerial = new List<ReptPickSerials>();
            ScanItemList = null;
            PickSerial = null;
        }
        private void BindBoxType(string _itmCode)
        {
            _pkgTblTp = "";
            pnlPkg.Visible = true;
            while (ddlBoxTp.Items.Count > 0)
            {
                ddlBoxTp.Items.RemoveAt(0);
            }
            MasterItem _mstIttm = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itmCode.Trim());
            if (_mstIttm != null)
            {
                List<UnitConvert> _unitConvList = new List<UnitConvert>();
                List<UnitConvert> _unitConvert = _base.CHNLSVC.Inventory.GET_UNIT_CONVERTER_DATA(new UnitConvert()
                {
                    mmu_model = _mstIttm.Mi_model,
                    mmu_com = Session["UserCompanyCode"].ToString()
                });
                if (_unitConvert.Count > 0)
                {
                    _unitConvList = _unitConvert;
                    ddlBoxTp.DataSource = _unitConvList;
                    ddlBoxTp.DataValueField = "mmu_qty";
                    ddlBoxTp.DataTextField = "mmu_model_uom";
                    ddlBoxTp.DataBind();
                    ddlBoxTp.SelectedIndex = 0;
                    _pkgTblTp = "MODEL_TBL";
                    ddlBoxTp_SelectedIndexChanged(null, null);
                }
                else
                {
                    UnitConvert _tmpUnitCon = new UnitConvert()
                    {
                        mmu_model_uom = _mstIttm.Mi_itm_uom,
                    };
                    _unitConvList.Add(_tmpUnitCon);
                    ddlBoxTp.DataSource = _unitConvList;
                    ddlBoxTp.DataValueField = "mmu_qty";
                    ddlBoxTp.DataTextField = "mmu_model_uom";
                    ddlBoxTp.DataBind();
                    ddlBoxTp.SelectedIndex = 0;
                    _pkgTblTp = "ITM_TBL";
                    ddlBoxTp_SelectedIndexChanged(null, null);
                    
                }
            }
        }
        public void txtItemCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                #region add box type data bind 06 Dec 2016
                pnlPkg.Visible = false;
                lblPkgQty.Text = "";
                if (_derectOut)
                {
                    BindBoxType(txtItemCode.Text);
                }
                #endregion

                string glbStatus = ddlStatus.SelectedValue;
                if (string.IsNullOrEmpty(txtItemCode.Text))
                {
                    PageClear();
                    txtItemCode.Focus();
                    return;
                }
                if (_batchBaseOut)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BatchBase);
                    DataTable   result = _base.CHNLSVC.CommonSearch.GetItemSearchDataForBatch(SearchParams, "ITEM", txtItemCode.Text.Trim());
                    bool b2 = false;
                    foreach (DataRow row in result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["ITEM"].ToString()))
                        {
                            if (txtItemCode.Text.Trim() == row["ITEM"].ToString())
                            {
                                b2 = true;
                                break;
                            }
                        }
                    }
                    if (!b2)
                    {
                        txtItemCode.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Item is not available in the batch');", true);
                        return;
                    }
                }
                if (_prodPlanBaseOut)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductionPlanItem);
                    DataTable result = _base.CHNLSVC.CommonSearch.GetItemSearchDataForProductionPlan(SearchParams, "ITEM", txtItemCode.Text.Trim());
                    bool b2 = false;
                    foreach (DataRow row in result.Rows)
                    {
                        if (!string.IsNullOrEmpty(row["ITEM"].ToString()))
                        {
                            if (txtItemCode.Text.Trim() == row["ITEM"].ToString())
                            {
                                b2 = true;
                                break;
                            }
                        }
                    }
                    if (!b2)
                    {
                        txtItemCode.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Item is not available in the production plan');", true);
                        return;
                    }
                }
                loadBinCode();

                if (ddlItemType.SelectedItem.Value == "M")
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = _base.CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, "Model", txtItemCode.Text.ToUpper());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "3";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    DateRow.Visible = false;

                    ddlSearchbykey.SelectedItem.Text = "Model";
                    ddlSearchbykey.DataBind();
                    txtSearchbyword.Text = txtItemCode.Text.ToUpper();
                    txtItemCode.Text = string.Empty;

                    UserPopup.Show();

                }
                if (ddlItemType.SelectedItem.Value == "P")
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable result = _base.CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, "Part No", txtItemCode.Text.ToUpper());
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "3";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    DateRow.Visible = false;

                    ddlSearchbykey.SelectedItem.Text = "Part";
                    ddlSearchbykey.DataBind();
                    txtSearchbyword.Text = txtItemCode.Text.ToUpper();
                    txtItemCode.Text = string.Empty;

                    UserPopup.Show();
                }


                if (LoadItemDetail(txtItemCode.Text.ToUpper().Trim()) == false)
                {
                    txtItemCode.Focus();
                    return;
                }

                txtItemCode.Text = txtItemCode.Text.ToUpper().Trim();

                if (string.IsNullOrEmpty(txtItemToBeChange.Text))
                {
                    txtItemToBeChange.Text = txtItemCode.Text.ToUpper().Trim();
                }

                if (ViewState["adjustmentTypeValue"] == null)
                {
                    txtItemCode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please set the adjustment type!.');", true);
                    return;
                }
                if (ViewState["adjustmentTypeValue"].ToString() == "0")
                {
                    txtItemCode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the adjustment type!.');", true);
                    return;
                }
                DataTable _inventoryLocation = new DataTable();
                if (isResQTY == false)
                {
                    _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), null);
                }
                else
                {
                    _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalanceStatus_RESNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(),null);
                }

                if (IsBinTransfer)
                {
                    _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalanceQty(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), null);
                }
                DataTable bins = _base.CHNLSVC.Inventory.LoadDistinctBins(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim());
                if (bins == null || bins.Rows.Count <= 0)
                {
                    PageClear();
                    txtItemCode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('No stock balance available.');", true);
                    return;
                }
                else
                {
                    if (IsBinTransfer == false)
                    {
                        ddlBinCode.DataSource = null;
                        ddlBinCode.DataSource = bins;
                        ddlBinCode.DataTextField = "INB_BIN";
                        ddlBinCode.DataValueField = "INB_BIN";
                        ddlBinCode.DataBind();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(BinCode))
                        {
                            txtItemCode.Text = "";
                            txtItemToBeChange.Text = "";
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Select from BIN code.');", true);
                            return;
                        }

                        bins.Clear();
                        DataRow dr = bins.NewRow();
                        dr["INB_BIN"] = BinCode;
                        bins.Rows.Add(dr);

                        ddlBinCode.DataSource = null;
                        ddlBinCode.DataSource = bins;
                        ddlBinCode.DataTextField = "INB_BIN";
                        ddlBinCode.DataValueField = "INB_BIN";
                        ddlBinCode.DataBind();
                        //if(lockStus.Checked)
                        BindUserCompanyItemStatusDDLData(ddlStatus);
                        ddlStatus.SelectedValue = glbStatus;
                    }
                }

                if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0)
                {
                    PageClear();
                    txtItemCode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('No stock balance available!.');", true);
                    return;
                }
                else
                {
                    if (ViewState["doc_tp"].ToString() == "ADJ")
                    {
                        for (int i = _inventoryLocation.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr = _inventoryLocation.Rows[i];
                            if (dr[0].ToString() == "CONS")
                            {
                                dr.Delete();
                            }
                        }
                        _inventoryLocation.AcceptChanges();

                    }

                    ddlStatus.DataSource = null;
                   
                    ddlStatus.DataSource = _inventoryLocation;
                    ddlStatus.DataTextField = "MIS_DESC";
                    ddlStatus.DataValueField = "INL_ITM_STUS";
                    ddlStatus.SelectedValue = _inventoryLocation.Rows[0][0].ToString(); 
                    ddlStatus.DataBind();
                    if (IsBinTransfer)
                    {
                        if (!_base.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10114))
                        {
                            ddlStatusToBeChange.SelectedValue = ddlStatus.SelectedValue;
                            ddlStatusToBeChange.Enabled = false;
                        }
                        else
                        {
                            ddlStatusToBeChange.Enabled = true;
                        }
                    }
                    else
                    {
                       
                    }
                    if (pnlSerialized.Visible == true)
                    {
                        List<ReptPickSerials> Tempserial_list = _base.CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text, string.Empty, string.Empty);

                        if (!IsBinTransfer)
                        {
                            if (Tempserial_list.Count <= 0)
                            {
                                txtItemCode.Text = string.Empty;
                                ddlStatus.Items.Clear();
                                ddlBinCode.Items.Clear();
                                lblDescription.Text = string.Empty;
                                lblModel.Text = string.Empty;
                                lblBrand.Text = string.Empty;
                                lblPart.Text = string.Empty;

                                txtItemToBeChange.Text = "";
                                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Item is not available.');", true);
                                return;
                            }
                            else
                            {
                                txtItemCode.ReadOnly = true;
                                txtQty.Text = "1";
                                //ddlBoxTp_SelectedIndexChanged(null, null);
                                txtQty.ReadOnly = true;
                                chkOnlyQty.Checked = false;
                                txtSerialI.Focus();
                            }
                        }
                        else
                        {
                            MasterItem _itemdetail = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.ToUpper().Trim());
                            if (_itemdetail.Mi_is_ser1 != Convert.ToInt32("0"))
                            {
                                txtItemCode.ReadOnly = true;
                                txtQty.Text = "1";
                                //ddlBoxTp_SelectedIndexChanged(null, null);
                                txtQty.ReadOnly = true;
                                txtSerialI.Focus();
                            }
                        }
                    }
                    else
                    {
                        txtItemCode.ReadOnly = false;
                        txtQty.ReadOnly = false;
                        if (bins.Rows.Count == 1)
                        {
                            ddlStatus.Focus();
                            if (_inventoryLocation.Rows.Count == 1)
                                txtQty.Focus();
                            else
                                ddlStatus.Focus();
                        }
                        else
                            ddlBinCode.Focus();
                    }
                }

                grdInventoryBalance.DataSource = null;
                grdInventoryBalance.AutoGenerateColumns = false;
                grdInventoryBalance.DataSource = _inventoryLocation;
                grdInventoryBalance.DataBind();

                if (IsBinTransfer)
                {
                    DataTable dt = _base.CHNLSVC.Inventory.GetItemBalanceForBIN(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), string.Empty, BinCode);
                    grdInventoryBalance.DataSource = null;
                    grdInventoryBalance.AutoGenerateColumns = false;
                    grdInventoryBalance.DataSource = dt;
                    grdInventoryBalance.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal qty;
                if (!decimal.TryParse(txtQty.Text, out qty))
                {
                    txtQty.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please enter valid quantity.');", true);
                    return;
                }

                DataTable _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), string.Empty);
                if (isResQTY == true)
                {
                    _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalanceStatus_RESNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), string.Empty);
                }
                if (_inventoryLocation.Rows.Count > 0)
                {
                    var count = from bl in _inventoryLocation.AsEnumerable()
                                where bl.Field<string>("MIS_DESC") == ddlStatus.SelectedItem.Text && bl.Field<decimal>("INL_FREE_QTY") > 0
                                select bl.Field<decimal>("INL_FREE_QTY");
                    decimal itemcount;
                    if (!(decimal.TryParse(count.First().ToString(), out itemcount)))
                    {
                        txtQty.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('No stock balance available...!');", true);
                        return;
                    }
                    else
                    {
                        if (itemcount < qty)
                        {
                            txtQty.Text = string.Empty;
                            txtQty.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please check the inventory balance...!');", true);
                            return;
                        }
                        else
                        {
                            txtQty.Text = qty.ToString("#,##0.####");
                            ddlBoxTp_SelectedIndexChanged(null, null);
                            lbtnAdd.Focus();
                        }
                    }
                }
                else
                {
                    txtItemCode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('No stock balance available...!');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtnItemToBeChange_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = _base.CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "C3";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                DateRow.Visible = false;
                Session["SP"] = "true";
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtItemToBeChange_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtItemToBeChange.Text))
                {
                    txtItemToBeChange.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(ViewState["adjustmentTypeValue"].ToString()) || ViewState["adjustmentTypeValue"].ToString() == "0")
                {
                    txtItemToBeChange.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the adjustment type!.');", true);
                    return;
                }

                //if (LoadItemDetail(txtItemToBeChange.Text.Trim()) == false)
                //{
                //    txtItemToBeChange.Focus();
                //    return;
                //}

                txtItemToBeChange.Text = txtItemToBeChange.Text.Trim();

                //DataTable _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemToBeChange.Text.Trim(), string.Empty);
                //if (ViewState["adjustmentTypeValue"].ToString() == "-")
                //{
                //    DataTable bins = _base.CHNLSVC.Inventory.LoadDistinctBins(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemToBeChange.Text.Trim());
                //    if (bins == null || bins.Rows.Count <= 0)
                //    {
                //        ddlStatus.DataSource = new List<InventoryLocation>();
                //        txtItemToBeChange.Focus();
                //        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('No stock bins.');", true);
                //        return;
                //    }
                //    else
                //    {
                //        ddlBinCode.DataSource = null;
                //        ddlBinCode.DataSource = bins;
                //        ddlBinCode.DataTextField = "INB_BIN";
                //        ddlBinCode.DataValueField = "INB_BIN";
                //        ddlBinCode.DataBind();

                //        if (bins.Rows.Count == 1)
                //            ddlStatus.Focus();
                //        else
                //            ddlBinCode.Focus();
                //    }

                //    if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0)
                //    {
                //        ddlStatus.DataSource = new List<InventoryLocation>();
                //        txtItemToBeChange.Focus();
                //        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('No stock balance available!.');", true);
                //        return;
                //    }
                //    else
                //    {
                //        ddlStatus.DataSource = null;
                //        ddlStatus.DataSource = _inventoryLocation;
                //        ddlStatus.DataTextField = "mis_desc";
                //        ddlStatus.DataValueField = "inl_itm_stus";
                //        ddlStatus.DataBind();

                //        if (_inventoryLocation.Rows.Count == 1)
                //            txtQty.Focus();
                //        else
                //            ddlStatus.Focus();
                //    }
                //}

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void chkDirectScan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDirectScan.Checked == true)
                {
                    txtSerialI.Focus();
                }
                else
                {
                    txtItemCode.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtSerialI_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bool _isBondLoc = false;
                MasterLocation _mstLoc = _base.CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                if (_mstLoc != null)
                {
                    if (_mstLoc.Ml_cate_1 == "DFS")
                    {
                        if (txtSerialI.Text != "N/A")
                        {
                            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobNo);
                            DataTable result = _base.CHNLSVC.CommonSearch.SearchSerialsByJobNo(SearchParams, "SERIAL #", txtSerialI.Text.Trim());
                            bool _avaSer = false;
                            foreach (DataRow _dr in result.Rows)
                            {
                                if (_dr["SERIAL #"].ToString() == txtSerialI.Text.Trim())
                                {
                                    _avaSer = true;
                                    break;
                                }
                            }
                            if (!_avaSer)
                            {
                                txtSerialI.Text = "";
                                string _msg = "Please enter valid serial # ";
                                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);
                                return;
                            }
                        }
                    }
                }
                if (_batchBaseOut || _prodPlanBaseOut)
                {
                    if (txtSerialI.Text== "N/A")
                    {
                        txtSerialI.Text = "";
                        string _msg = "Please enter valid serial # ";
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyNoticeToast('" + _msg + "');", true);
                        return;
                    }
                    if (_prodPlanBaseOut)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductConditionUpdate);
                        DataTable result = _base.CHNLSVC.Inventory.SearchSerialsByProductionNo(SearchParams, "SERIAL #", txtSerialI.Text.Trim());
                        bool _avaSer = false;
                        foreach (DataRow _dr in result.Rows)
                        {
                            if (_dr["SERIAL #"].ToString()==txtSerialI.Text.Trim())
                            {
                                _avaSer = true;
                                break;
                            }
                        }
                        if (!_avaSer)
                        {
                            txtSerialI.Text = "";
                            string _msg = "Please enter valid serial # ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);
                            return;
                        }
                    }
                    if (_batchBaseOut)
                    {
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                        DataTable result = _base.CHNLSVC.Inventory.SearchSerialsInr_Batchno(SearchParams, "SERIAL #", txtSerialI.Text.Trim());
                        bool _avaSer = false;
                        foreach (DataRow _dr in result.Rows)
                        {
                            if (_dr["SERIAL #"].ToString() == txtSerialI.Text.Trim())
                            {
                                _avaSer = true;
                                break;
                            }
                        }
                        if (!_avaSer)
                        {
                            txtSerialI.Text = "";
                            string _msg = "Please enter valid serial # ";
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);
                            return;
                        }
                    }

                }
                #region Validate Scan Pick
                if (!_derectOut)
                {
                    bool _serAlreadyPick = false;
                    if (!string.IsNullOrEmpty(ScanDocument))
                    {
                        ReptPickHeader _tmpHdr = _base.CHNLSVC.Inventory.GET_TEMP_PICK_HDR_DATA(new ReptPickHeader()
                        {
                            Tuh_usr_com = Session["UserCompanyCode"].ToString(),
                            Tuh_doc_no = ScanDocument,
                        }).FirstOrDefault();
                        if (_tmpHdr != null)
                        {
                            List<ReptPickSerials> _repSerList = _base.CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = _tmpHdr.Tuh_usrseq_no });
                            if (_repSerList != null)
                            {
                                if (_repSerList.Count > 0)
                                {
                                    _serAlreadyPick = true;
                                }
                            }
                        }
                        if (_serAlreadyPick && _tmpHdr.Tuh_usr_id != Session["UserID"].ToString())
                        {
                            string _msg = "Request is in process by " + _tmpHdr.Tuh_usr_id;
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyNoticeToast('" + _msg + "');", true);
                            txtSerialI.Text = "";
                            return;
                            // DisplayMessage(_msg); 
                        }
                    }
                }
                #endregion

                //Add by lakshan
                MasterLocation _mstLocation = _base.CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                if (_mstLocation != null)
                {
                    if (!_mstLocation.Ml_is_serial)
                    {
                        int _usrSeq = _base.CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(doc_tp, Session["UserCompanyCode"].ToString(), ScanDocument, 0);
                        if (_usrSeq != -1)
                        {
                            ReptPickHeader _ReptPickHeader = _base.CHNLSVC.Inventory.GetAllScanSerialParameters(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), _usrSeq, doc_tp);
                            if (_ReptPickHeader != null)
                            {
                                if (!string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_com) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_loc) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_load_bay))
                                {
                                    DisplayMessage("Serials are already picked from PDA !");
                                    return;
                                }
                            }
                        }
                    }
                }
                if (chkDirectScan.Checked == false)
                { 
                
                }
                //

                string itemCode = txtItemCode.Text.ToUpper();
                List<InventorySerialN> _invSerList = _base.CHNLSVC.Inventory.Get_INR_SER_DATA(new InventorySerialN()
                {
                    Ins_com = Session["UserCompanyCode"].ToString(),
                    Ins_loc = Session["UserDefLoca"].ToString(),
                    Ins_itm_cd = string.IsNullOrEmpty(itemCode) ? null : itemCode,
                    Ins_ser_1 = txtSerialI.Text.Trim(),
                    Ins_available=-2
                }).ToList();
                bool _serPick = false;
                bool _serNotAva = false;
                if (_invSerList == null)
                {
                    DisplayMessage("This serial number is not available! Please check again.! "); txtSerialI.Text = ""; return;
                }
                if (_invSerList != null)
                {
                    foreach (var item in _invSerList)
                    {
                        if (item.Ins_available==-1)
                        {
                            _serPick = true;
                            break;
                        }
                        //if (item.Ins_available == 0)
                        //{
                        //    _serNotAva = true;
                        //    break;
                        //}
                    }
                }
                //if (_serNotAva)
                //{
                //    DisplayMessage("This serial number is not available! Please check again.! "); txtSerialI.Text = ""; return;
                //}
                if (_serPick)
                {
                    DisplayMessage("This serial number is already picked ! "); txtSerialI.Text = ""; return;
                }

                DataTable dtserials = _base.CHNLSVC.Inventory.GET_INR_SER(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), null, itemCode, ddlStatus.SelectedValue, txtSerialI.Text.Trim());

                Session["SELECTED_SERIAL_CHSTO"] = txtSerialI.Text.Trim();
                if (dtserials.Rows.Count <= 0)
                {
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid serial number...!')", true);
                    DisplayMessage("This serial number is not available! Please check again.!");
                    txtSerialI.Text = string.Empty;
                    txtSerialI.Focus();
                    return;
                }
                

                if (dtserials.Rows.Count > 1)
                {
                    dgvItem.DataSource = new DataTable();
                    dgvItem.DataSource = dtserials;
                    dgvItem.DataBind();
                    lblcaption.Text = "Select Item Code";
                    mpexcel.Show();
                    Session["EXCELPOPUP"] = "Yes";
                    return;
                }
                else
                {
                    string binno = dtserials.Rows[0]["INS_BIN"].ToString();
                    TextBox txtFromBin = (TextBox)this.Parent.FindControl("txtFromBin");
                    if (txtFromBin!=null)
                    {
                        ((TextBox)this.Parent.FindControl("txtFromBin")).Text = binno;
                        BinCode = binno;
                        loadBinCode();
                    }
                }

                if (IsBinTransfer)
                {
                    if (string.IsNullOrEmpty(BinCode))
                    {
                        txtItemCode.Text = "";
                        txtItemToBeChange.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Select from BIN code.');", true);
                        return;
                    }
                    else
                    {
                        if (dtserials.Rows[0]["INS_BIN"].ToString() != BinCode)
                        {
                            txtSerialI.Text = "";
                            txtSerialII.Text = "";
                            txtSerialIII.Text = "";
                            txtItemCode.Text = "";
                            txtItemToBeChange.Text = "";
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Select a serial from selected BIN.');", true);
                            return;
                        }
                    }
                }

                ViewState["SerialID"] = dtserials.Rows[0]["INS_SER_ID"].ToString();

                if (chkDirectScan.Checked == false)
                {
                    txtItemCode.Text = dtserials.Rows[0]["INS_ITM_CD"].ToString();
                    BindBoxType(txtItemCode.Text.ToUpper().Trim());
                    txtItemCode.ReadOnly = true;
                    txtSerialI.Text = dtserials.Rows[0]["INS_SER_1"].ToString();
                    txtSerialII.Text = dtserials.Rows[0]["INS_SER_2"].ToString();
                    txtSerialIII.Text = dtserials.Rows[0]["INS_SER_3"].ToString();
                    // ddlStatus.SelectedValue = dtserials.Rows[0]["INS_ITM_STUS"].ToString();
                    ddlBinCode.DataSource = dtserials;
                    ddlBinCode.DataTextField = "INS_BIN";
                    ddlBinCode.DataValueField = "INS_BIN";
                    ddlBinCode.DataBind();
                    ddlStatus.DataSource = dtserials;
                    ddlStatus.DataTextField = "mis_desc";
                    ddlStatus.DataValueField = "INS_ITM_STUS";
                    ddlStatus.DataBind();
                    if (string.IsNullOrEmpty(txtItemToBeChange.Text))
                    {
                        txtItemToBeChange.Text = txtItemCode.Text.ToUpper();
                    }
                    txtSerialI.ReadOnly = true;
                    //Session["INS_SEQ_NO"] = dtserials.Rows[0]["INS_SEQ_NO"].ToString();
                    txtSerialII.ReadOnly = true;
                    txtSerialIII.ReadOnly = true;
                    txtQty.Text = "1";
                    txtQty.ReadOnly = true;

                    ddlStatusToBeChange.SelectedIndex =ddlStatusToBeChange.Items.IndexOf(ddlStatusToBeChange.Items.FindByValue(dtserials.Rows[0]["INS_ITM_STUS"].ToString()));
                    //Add by Lakshan
                    if (ScanItemList != null && PageName == "AOD-OUT" && !_derectOut)
                    {
                        var _itm = ScanItemList.Where(c => c.Itri_itm_cd == txtItemCode.Text && c.Itri_itm_stus == ddlStatus.SelectedValue).FirstOrDefault();
                        decimal _qty = PickSerial.Where(c => c.Tus_itm_cd == txtItemCode.Text && c.Tus_itm_stus == ddlStatus.SelectedValue).ToList().Sum(c => c.Tus_qty);
                        if (_itm != null)
                        {
                            if (_itm.Itri_bqty<1)
                            {
                                DisplayMessage("You cannot exceed the approved quantity.");
                                return;
                            }
                        }
                            //PickSerial
                    }
                   // new lakshan
                    //
                }
                else
                {
                    txtItemCode.Text = dtserials.Rows[0]["INS_ITM_CD"].ToString();
                    BindBoxType(txtItemCode.Text.ToUpper().Trim());
                    txtItemCode.ReadOnly = true;
                    txtSerialI.Text = dtserials.Rows[0]["INS_SER_1"].ToString();
                    txtSerialII.Text = dtserials.Rows[0]["INS_SER_2"].ToString();
                    txtSerialIII.Text = dtserials.Rows[0]["INS_SER_3"].ToString();
                    //ddlStatus.SelectedValue = dtserials.Rows[0]["INS_ITM_STUS"].ToString();
                    txtSerialI.ReadOnly = true;
                    txtSerialII.ReadOnly = true;
                    txtSerialIII.ReadOnly = true;

                    txtItemCode.Text = dtserials.Rows[0]["INS_ITM_CD"].ToString();
                    if (string.IsNullOrEmpty(txtItemToBeChange.Text))
                    {
                        txtItemToBeChange.Text = txtItemCode.Text.ToUpper();
                    }
                    ddlBinCode.DataSource = dtserials;
                    ddlBinCode.DataTextField = "INS_BIN";
                    ddlBinCode.DataValueField = "INS_BIN";
                    ddlBinCode.DataBind();
                    ddlStatus.DataSource = dtserials;
                    ddlStatus.DataTextField = "mis_desc";
                    ddlStatus.DataValueField = "INS_ITM_STUS";
                    ddlStatus.DataBind();

                    txtQty.Text = "1";
                    txtQty.ReadOnly = true;

                }

                if (!string.IsNullOrEmpty(txtItemCode.Text.ToUpper()))
                {
                    _itemdetail = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.ToUpper());
                }
                if (_itemdetail != null)
                {
                    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        if (_itemdetail.Mi_itm_tp == "V")
                        {
                            PageClear();
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Virtual item not allowed.');", true);
                            return;
                        }

                        _chargeType = _itemdetail.Mi_chg_tp;
                        string _description = _itemdetail.Mi_longdesc;
                        string _model = _itemdetail.Mi_model;
                        string _brand = _itemdetail.Mi_brand;
                        string _partNo = _itemdetail.Mi_part_no;
                        string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Serialized" : "Non-Serialized";

                        if (_itemdetail.Mi_is_ser1 != 1)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Non-Serialized item.');", true);
                            return;
                        }

                        lblDescription.Text = _description;
                        lblModel.Text = _model;
                        lblBrand.Text = _brand;
                        lblPart.Text = _partNo;
                        //lblItemSerialStatus.Text = "Serial Status : " + _serialstatus;
                        _isDecimalAllow = _base.CHNLSVC.Inventory.IsUOMDecimalAllow(txtItemCode.Text.ToUpper());
                        if (_itemdetail.MI_IS_EXP_DT == 1)
                        {
                            divExpiryDate.Visible = true;
                            //dtExp.Visible = true;
                        }
                        else
                        {
                            divExpiryDate.Visible = false;
                            //dtExp.Visible = false;
                        }
                    }
                }
                else
                {
                    txtItemCode.Text = string.Empty;
                    lblDescription.Text = string.Empty;
                    lblModel.Text = string.Empty;
                    lblBrand.Text = string.Empty;
                    //lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
                    //txtUnitCost.Text = string.Empty;
                    txtQty.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Invalid item code.');", true);
                }

                DataTable _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), string.Empty);
                grdInventoryBalance.DataSource = null;
                grdInventoryBalance.AutoGenerateColumns = false;
                grdInventoryBalance.DataSource = _inventoryLocation;
                grdInventoryBalance.DataBind();

                if (IsBinTransfer)
                {
                    DataTable dt = _base.CHNLSVC.Inventory.GetItemBalanceForBIN(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), string.Empty, BinCode);
                    grdInventoryBalance.DataSource = null;
                    grdInventoryBalance.AutoGenerateColumns = false;
                    grdInventoryBalance.DataSource = dt;
                    grdInventoryBalance.DataBind();
                }
                lbtnAdd_Click(null, null);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void lbtnSerialI_Click(object sender, EventArgs e)
        {
            try
            {
                Session["SP"] = "show";
                if (string.IsNullOrEmpty(txtItemCode.Text))
                    Session["_SerialSearchType"] = "SER1_WOITEM";
                else
                    Session["_SerialSearchType"] = "SER1_WITEM";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;

                string SearchParams = string.Empty;
                DataTable result = new DataTable();

                if (!ReqSupplier)
                {
                    if (!IsBinTransfer)
                    {
                        if (isbatchserial == false)
                        {
                            bool _isBondLoc = false;
                            MasterLocation _mstLoc = _base.CHNLSVC.General.GetLocationByLocCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                            if (_mstLoc!=null)
                            {
                                if (_mstLoc.Ml_cate_1=="DFS")
                                {
                                    _isBondLoc = true;
                                }
                            }
                            if (!_isBondLoc)
                            {
                                if (_prodPlanBaseOut == false)
                                {
                                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                                    result = _base.CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, null, null);
                                    lblvalue.Text = "134";
                                }
                                else
                                {
                                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductConditionUpdate);
                                    result = _base.CHNLSVC.Inventory.SearchSerialsByProductionNo(SearchParams, null, null);
                                    lblvalue.Text = "ProductBaseSerial";
                                }
                            }
                            else
                            {
                                SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobNo);
                                result = _base.CHNLSVC.CommonSearch.SearchSerialsByJobNo(SearchParams, null, null);
                                lblvalue.Text = "JobNo";
                            }
                        }

                        else
                        {
                            SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                            result = _base.CHNLSVC.Inventory.SearchSerialsInr_Batchno(SearchParams, null, null);
                            lblvalue.Text = "BatchBaseSerial";
                        }
                    }
                    else
                    {
                        if (isbatchserial == false)
                        {
                            SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearialSearchTOBIN);
                            result = _base.CHNLSVC.CommonSearch.SearchSerialByLocBIN(SearchParams, null, null);
                            lblvalue.Text = "SearialSearchTOBIN";
                        }
                        else
                        {
                            SearchParams=SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                            result = _base.CHNLSVC.Inventory.SearchSerialsInr_Batchno(SearchParams, null, null);
                            lblvalue.Text = "SearialSearchTOBIN";
                        }
                    }
                    if (result.Rows.Count > 0 && ddlStatus.SelectedItem != null && !string.IsNullOrEmpty(ddlStatus.SelectedItem.Text))
                    {
                        DataRow[] dr = result.Select("[Item Status] = '" + ddlStatus.SelectedItem.Text + "'");
                        if (dr.Length > 0)
                        {
                            result = result.Select("[Item Status] = '" + ddlStatus.SelectedItem.Text + "'").CopyToDataTable();
                        }
                        else
                        {
                            result.Rows.Clear();
                        }
                    }
                    int count = result.Rows.Count;
                    for (int x = count - 1; x >= 500; x--)
                    {
                        DataRow dr = result.Rows[x];
                            dr.Delete();
                    }
                    result.AcceptChanges();
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    DateRow.Visible = false;
                    UserPopup.Show();
                }
                else
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                    result = _base.CHNLSVC.Inventory.GetSupplierItemSerial(SearchParams, null, null);
                    result.Columns.Remove("Serial 3");
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    lblvalue.Text = "Serial";
                    DateRow.Visible = false;
                    UserPopup.Show();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        //public void lbtnAdd_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        if (PNLTobechange.Visible == true)
        //        {
        //            if (string.IsNullOrEmpty(txtItemToBeChange.Text.Trim()))
        //            {
        //                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the item To Be Change.')", true);
        //                txtItemToBeChange.Focus();
        //                return;
        //            }
        //            if (string.IsNullOrEmpty(ddlStatusToBeChange.Text.ToString()))
        //            {
        //                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the  item status.')", true);
        //                ddlStatusToBeChange.Focus();
        //                return;
        //            }
        //            if (ddlStatusToBeChange.SelectedValue.ToString() == "CONS")
        //            {
        //                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('New status cannot be Consignment.')", true);
        //                ddlStatusToBeChange.Focus();
        //                return;
        //            }
        //            if (ddlStatus.SelectedValue.ToString() == ddlStatusToBeChange.SelectedValue.ToString())
        //            {
        //                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('New status cannot be same as available status.')", true);
        //                ddlStatusToBeChange.Focus();
        //                return;
        //            }
        //        }

        //            //Int32 _lineNo = 0;
        //            //Int32 _serID = 0;
        //            //bool _updateItem = false;
        //            MasterItem _itms1 = new MasterItem();
        //            _itms1 = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.Trim());

        //            if (_itms1.Mi_is_ser1 == 1)
        //            {
        //                if (string.IsNullOrEmpty(txtSerialI.Text.ToString()))
        //                {
        //                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the serial no 1.')", true);
        //                    //if (txtSerialI.Enabled == true) txtSerialI.Focus();
        //                    //if (txtSerialII.Enabled == true) txtSerialII.Focus();
        //                    txtSerialI.Focus();
        //                    return;
        //                }
        //                if (_itms1.Mi_is_ser2 == true)
        //                {
        //                    if (string.IsNullOrEmpty(txtSerialII.Text.ToString()))
        //                    {
        //                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the serial no 2.')", true);
        //                        //if (txtSerialI.Enabled == true) txtSerialI.Focus();
        //                        //if (txtSerialII.Enabled == true) txtSerialII.Focus();
        //                        txtSerialII.Focus();
        //                        return;
        //                    }
        //                }
        //                if (_itms1.Mi_is_ser3 == true)
        //                {
        //                    if (string.IsNullOrEmpty(txtSerialIII.Text.ToString()))
        //                    {
        //                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the serial no 3.')", true);
        //                        //if (txtSerialI.Enabled == true) txtSerialI.Focus();
        //                        //if (txtSerialII.Enabled == true) txtSerialII.Focus();
        //                        txtSerialIII.Focus();
        //                        return;
        //                    }
        //                }
        //            }
        //        //}
        //        //if (btnHandler != null)
        //        //    btnHandler(string.Empty);

        //        if (string.IsNullOrEmpty(txtItemCode.Text.Trim()))
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please select the item.');", true);
        //            txtItemCode.Focus();
        //            return;
        //        }
        //        if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please select the status.');", true);
        //            ddlStatus.Focus();
        //            return;
        //        }
        //        if (string.IsNullOrEmpty(txtQty.Text))
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please select the qty.');", true);
        //            txtQty.Focus();
        //            return;
        //        }
        //        //if (pnlSup.Visible == true)
        //        //    if (dtGRN.Value.Date > DateTime.Now.Date)
        //        //    {
        //        //        MessageBox.Show("Invalid GRN Date", "GRN Date", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //        return;
        //        //    }


        //        //if (ddlAdjType.SelectedItem.ToString() == "ADJ+")
        //        //{
        //        //    MasterItem _mstItm = new MasterItem();
        //        //    _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text);
        //        //    if (_mstItm.Mi_is_ser1 == 1)    //kapila 26/8/2015
        //        //    {
        //        //        bool _permission = CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 11065);
        //        //        if (!_permission)
        //        //        {
        //        //            if (string.IsNullOrEmpty(txtGRNno.Text) || string.IsNullOrEmpty(txtSup.Text) || string.IsNullOrEmpty(txtBatch.Text))
        //        //            {
        //        //                MessageBox.Show("Please enter supplier/GRN No/Batch No (Permission code-11067)", "Unit Cost", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //                return;
        //        //            }
        //        //        }
        //        //        else
        //        //        {
        //        //            if (MessageBox.Show("GRN details not entered. Are you sure ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.No) return;
        //        //        }
        //        //    }
        //        //}


        //        //if (ViewState["adjustmentTypeValue"].ToString() == "+" && adjustmentType != "AFSL")
        //        //{
        //        //    if (string.IsNullOrEmpty(txtUnitCost.Text))
        //        //    {
        //        //        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Please enter unit cost amount.');", true);
        //        //        txtUnitCost.Focus();
        //        //        return;
        //        //    }
        //        //    else
        //        //    {
        //        //        if (Convert.ToDecimal(txtUnitCost.Text.ToString()) <= 0 && ddlStatus.Text.ToString().ToUpper() != "CONSIGNMENT")
        //        //        {
        //        //            if (_chargeType != "FOC")
        //        //            {
        //        //                MessageBox.Show("Unit cost can't be less than or equal to zero amount.", "Unit Cost", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //                txtUnitCost.Clear();
        //        //                txtUnitCost.Focus();
        //        //                return;
        //        //            }
        //        //        }
        //        //        else
        //        //        {
        //        //            if (ddlStatus.Text.ToString().ToUpper() == "CONSIGNMENT")
        //        //            {
        //        //                if (Convert.ToDecimal(txtUnitCost.Text.ToString()) != 0)
        //        //                {
        //        //                    MessageBox.Show("Unit cost should be zero(0.00) for CONSIGNMENT status.", "Unit Cost", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        //                    txtUnitCost.Clear();
        //        //                    txtUnitCost.Focus();
        //        //                    return;
        //        //                }
        //        //            }
        //        //        }
        //        //    }
        //        //}
        //        //else
        //        //{
        //        //    txtUnitCost.Text = "0";
        //        //}


        //        //this.Cursor = Cursors.WaitCursor;
        //        MasterItem _itms = new MasterItem();
        //        _itms = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.Trim());
        //        InventoryRequestItem _itm = new InventoryRequestItem();

        //        ////Added by Prabhath on 17/12/2013 ************* start **************
        //        //Decimal _ucost = Convert.ToDecimal(txtUnitCost.Text.Trim());
        //        //if (AdjType == "AFSL")
        //        //{
        //        //    _ucost = CHNLSVC.Inventory.GetLatestCost(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), "GOD");
        //        //    //Common function written by Sachith, Where used it in Revert Module Line 816 - 836, pick it as same
        //        //    List<InventoryCostRate> _costList = CHNLSVC.Inventory.GetInventoryCostRate(Session["UserCompanyCode"].ToString(), "AFSLRV", Convert.ToString(ddlStatus.SelectedValue), (((dtpDate.Value.Year - dtpDate.Value.Year) * 12) + dtpDate.Value.Month - dtpDate.Value.Month), "GOD");
        //        //    if (_costList != null && _costList.Count > 0)
        //        //        if (_costList[0].Rcr_rt == 0) _ucost = Math.Round(_ucost - _costList[0].Rcr_val, 2);
        //        //        else _ucost = Math.Round(_ucost - ((_ucost * _costList[0].Rcr_rt) / 100), 2);
        //        //}
        //        ////Added by Prabhath on 17/12/2013 ************* end **************

        //        if (ScanItemList != null)
        //            if (ScanItemList.Count > 0)
        //            {
        //                var _duplicate = from _ls in ScanItemList
        //                                 where _ls.Itri_itm_cd == txtItemCode.Text.Trim() && _ls.Itri_itm_stus == ddlStatus.SelectedValue.ToString()
        //                                 select _ls;

        //                if (_duplicate != null)
        //                    if (_duplicate.Count() > 0)
        //                    {
        //                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Selected item already available.');", true);
        //                        return;
        //                    }

        //                var _maxline = (from _ls in ScanItemList
        //                                select _ls.Itri_line_no).Max();
        //                _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
        //                _itm.Itri_itm_cd = txtItemCode.Text.Trim();
        //                _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
        //                _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
        //                _itm.Itri_qty = 0;
        //                _itm.Mi_longdesc = _itms.Mi_longdesc;
        //                _itm.Mi_model = _itms.Mi_model;
        //                _itm.Mi_brand = _itms.Mi_brand;
        //                ////Added by Prabhath on 17/12/2013 ************* start **************
        //                //_itm.Itri_unit_price = _ucost;
        //                ////Added by Prabhath on 17/12/2013 ************* end **************
        //                ////kapila 3/7/2015
        //                //_itm.Itri_batchno = txtBatch.Text;

        //                if (divExpiryDate.Visible == true)
        //                    _itm.Itri_expdate = Convert.ToDateTime(ddlExpiryDate.SelectedItem.Value);

        //                //_itm.Itri_grndate = dtGRN.Value.Date;
        //                //_itm.Itri_grnno = txtGRNno.Text;
        //                //_itm.Itri_supplier = txtSup.Text;

        //                if (PNLTobechange.Visible == true)
        //                {
        //                    _itm.Itri_note = ddlStatusToBeChange.SelectedValue.ToString();
        //                    _itm.Itri_mitm_cd = txtItemToBeChange.Text;
        //                }
        //            }
        //            else
        //            {
        //                _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
        //                _itm.Itri_itm_cd = txtItemCode.Text.Trim();
        //                _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
        //                _itm.Itri_line_no = 1;
        //                _itm.Itri_qty = 0;
        //                _itm.Mi_longdesc = _itms.Mi_longdesc;
        //                _itm.Mi_model = _itms.Mi_model;
        //                _itm.Mi_brand = _itms.Mi_brand;
        //                ////Added by Prabhath on 17/12/2013 ************* start **************
        //                //_itm.Itri_unit_price = _ucost;
        //                ////Added by Prabhath on 17/12/2013 ************* end **************
        //                ////kapila 3/7/2015
        //                //_itm.Itri_batchno = txtBatch.Text;

        //                if (divExpiryDate.Visible == true)
        //                    _itm.Itri_expdate = Convert.ToDateTime(ddlExpiryDate.SelectedItem.Value);

        //                //_itm.Itri_grndate = dtGRN.Value.Date;
        //                //_itm.Itri_grnno = txtGRNno.Text;
        //                //_itm.Itri_supplier = txtSup.Text;

        //                if (PNLTobechange.Visible == true)
        //                {
        //                    _itm.Itri_note = ddlStatusToBeChange.SelectedValue.ToString();
        //                    _itm.Itri_mitm_cd = txtItemToBeChange.Text;
        //                }
        //            }

        //        ScanItemList.Add(_itm);
        //        ScanItemList = ScanItemList;

        //        if ((ViewState["userSeqNo"] == null))
        //        {
        //            int retValue = GenerateNewUserSeqNo();
        //            if (retValue == 0)
        //            {
        //                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Fail Generate New User SeqNo.');", true);
        //                ddlStatus.Focus();
        //                return;
        //            }
        //        }

        //        List<ReptPickItems> _saveonly = new List<ReptPickItems>();
        //        foreach (InventoryRequestItem _addedItem in ScanItemList)
        //        {
        //            ReptPickItems _reptitm = new ReptPickItems();
        //            _reptitm.Tui_usrseq_no = Convert.ToInt32(ViewState["userSeqNo"].ToString());
        //            _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
        //            _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
        //            _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
        //            _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
        //            _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_unit_price);
        //            _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;
        //            //_reptitm.Tui_sup = txtSup.Text;
        //            //_reptitm.Tui_grn = txtGRNno.Text;
        //            //_reptitm.Tui_batch = txtBatch.Text;
        //            //_reptitm.Tui_grn_dt = dtGRN.Value.Date;
        //            if (divExpiryDate.Visible == true)
        //                _reptitm.Tui_exp_dt = Convert.ToDateTime(ddlExpiryDate.SelectedItem.Value);

        //            if (PNLTobechange.Visible == true)
        //            {
        //                _reptitm.Tui_sup = _addedItem.Itri_mitm_cd;
        //            }
        //            _saveonly.Add(_reptitm);
        //        }

        //        //DELETE all items and again add all
        //        _base.CHNLSVC.Inventory.Del_temp_pick_itm(Convert.ToInt32((ViewState["userSeqNo"].ToString())), string.Empty, string.Empty, 0, 3);

        //        _base.CHNLSVC.Inventory.SavePickedItems(_saveonly);

        //        //gvItems.DataSource = null;
        //        //gvItems.DataSource = ScanItemList;
        //        //grdItem.DataSource = ScanItemList;
        //        //grdItem.DataBind();

        //        if (((GridView)this.Parent.FindControl("grdItems")) == null)
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('grdItems not found.');", true);
        //            return;
        //        }
        //        ((GridView)this.Parent.FindControl("grdItems")).DataSource = ScanItemList;
        //        ((GridView)this.Parent.FindControl("grdItems")).DataBind();



        //        //decimal _appQty = Convert.ToDecimal(lblPopupQty.Text.ToString());
        //        //if (_appQty < num_of_checked_itms)
        //        //{
        //        //    MessageBox.Show("Can't exceed Approved Qty. You can add only " + Convert.ToString(_appQty - _appQty) + " item more.", "Approved Qty", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        //        //    return;
        //        //}

        //        //#region Adj

        //        Int32 generated_seq = -1;
        //        msitem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text);


        //        //Int32 user_seq_num = CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("ADJ", Session["UserCompanyCode"].ToString(), ScanDocument, 0);
        //        //if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
        //        //{
        //        //    generated_seq = user_seq_num;
        //        //}
        //        //else
        //        //{
        //        //    generated_seq = CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "ADJ", 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
        //        //    //assign user_seqno
        //        //    ReptPickHeader RPH = new ReptPickHeader();
        //        //    RPH.Tuh_doc_tp = "ADJ";
        //        //    RPH.Tuh_cre_dt = DateTime.Today;
        //        //    RPH.Tuh_ischek_itmstus = true;
        //        //    RPH.Tuh_ischek_reqqty = true;
        //        //    RPH.Tuh_ischek_simitm = true;
        //        //    RPH.Tuh_session_id = Session["SessionID"].ToString();
        //        //    RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
        //        //    RPH.Tuh_usr_id = Session["UserID"].ToString();
        //        //    RPH.Tuh_usrseq_no = generated_seq;

        //        //    RPH.Tuh_direct = false;
        //        //    RPH.Tuh_doc_no = ScanDocument;
        //        //    //write entry to TEMP_PICK_HDR
        //        //    int affected_rows = CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

        //        //}

        //        //if (msitem.Mi_is_ser1 != -1)
        //        ////change msitem.Mi_is_ser1 == true
        //        //{
        //        //    int rowCount = 0;

        //        //    foreach (DataGridViewRow gvr in this.GridPopup.Rows)
        //        //    {
        //        //        Int32 serID = Convert.ToInt32(gvr.Cells["ser_SerialID"].Value);

        //        //        DataGridViewCheckBoxCell chkSelect = (DataGridViewCheckBoxCell)gvr.Cells["ser_Select"];
        //        //        if (Convert.ToBoolean(chkSelect.Value) == true)
        //        //        {
        //        //            //-------------
        //        //            string binCode = gvr.Cells["ser_Bin"].Value.ToString();
        //        //            ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, itemCode, serID);
        //        //            //Update_inrser_INS_AVAILABLE
        //        //            Boolean update_inr_ser = CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), itemCode, serID, -1);

        //        //            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
        //        //            _reptPickSerial_.Tus_usrseq_no = generated_seq;
        //        //            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
        //        //            _reptPickSerial_.Tus_base_doc_no = ScanDocument;
        //        //            _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(hdnInvoiceLineNo.Text.ToString());
        //        //            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
        //        //            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
        //        //            _reptPickSerial_.Tus_job_no = JobNo;
        //        //            _reptPickSerial_.Tus_job_line = JobLineNo;
        //        //            //enter row into TEMP_PICK_SER
        //        //            Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

        //        //            rowCount++;
        //        //            //isManualscan = true;

        //        //        }

        //        //    }
        //        //}
        //        //else
        //        //{
        //        ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
        //        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
        //        _reptPickSerial_.Tus_usrseq_no = Convert.ToInt32(ViewState["userSeqNo"].ToString());//generated_seq;
        //        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
        //        _reptPickSerial_.Tus_base_doc_no = ViewState["userSeqNo"].ToString(); //ScanDocument;
        //        _reptPickSerial_.Tus_base_itm_line = ScanItemList.Count;//Convert.ToInt32(hdnInvoiceLineNo.Text.ToString());
        //        _reptPickSerial_.Tus_itm_desc = lblDescription.Text;//msitem.Mi_shortdesc;
        //        _reptPickSerial_.Tus_itm_model = lblModel.Text;//msitem.Mi_model;
        //        _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
        //        _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
        //        _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
        //        _reptPickSerial_.Tus_itm_cd = txtItemCode.Text;//itemCode;
        //        _reptPickSerial_.Tus_itm_stus = ddlStatus.SelectedItem.Value;//ItemStatus;
        //        _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtQty.Text);//Convert.ToDecimal(txtPopupQty.Text.ToString());
        //        _reptPickSerial_.Tus_ser_1 = txtSerialI.Text;
        //        _reptPickSerial_.Tus_ser_2 = txtSerialII.Text;
        //        _reptPickSerial_.Tus_ser_3 = txtSerialIII.Text;
        //        _reptPickSerial_.Tus_ser_4 = "N/A";
        //        _reptPickSerial_.Tus_ser_id = Convert.ToInt32(ViewState["SerialID"].ToString());
        //        //_reptPickSerial_.Tus_serial_id = ViewState["SerialID"].ToString();
        //        _reptPickSerial_.Tus_unit_cost = 0;
        //        _reptPickSerial_.Tus_unit_price = 0;
        //        _reptPickSerial_.Tus_unit_price = 0;

        //        //_reptPickSerial_.Tus_job_no = JobNo;
        //        //-reptPickSerial_.Tus_job_line = JobLineNo;
        //        //enter row into TEMP_PICK_SER

        //        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
        //        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;

        //        if (PNLTobechange.Visible == true)
        //        {
        //            _reptPickSerial_.Tus_new_itm_cd = txtItemToBeChange.Text;//newitemCode;
        //            _reptPickSerial_.Tus_new_status = ddlStatusToBeChange.SelectedItem.Value;//newItemStatus;
        //        }

        //        Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
        //        //}

        //        //#endregion

        //        LoadItems(ViewState["userSeqNo"].ToString());
        //        PageClear();

        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
        //    }
        //}
        public void lbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //Dulaj 2018-Aug-01 Check Bin
                DataTable dt = _base.CHNLSVC.Inventory.GetItemsByBin(Session["UserCompanyCode"].ToString(), txtItemCode.Text, Session["UserDefLoca"].ToString(), ddlBinCode.SelectedValue.ToString(), ddlStatus.SelectedValue.ToString());
                decimal qty = 0;
                foreach(DataRow dr in dt.Rows)
                {
                    decimal rowQty = Convert.ToDecimal(dr["inb_qty"].ToString());
                    qty = rowQty + qty;
                }
                if(!String.IsNullOrEmpty(txtQty.Text))
                {
                    if(Convert.ToDecimal(txtQty.Text)>qty)
                    {
                        DisplayMessage("Selected bin " + ddlBinCode.SelectedValue.ToString() + " has only "+qty+" qty left. In "+ddlStatus.SelectedValue.ToString()+" item status.");
                        return;
                    }
                }
                //
                //Add by lakshan
                MasterLocation _mstLocation = _base.CHNLSVC.General.GetLocationInfor(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());
                if (_mstLocation != null)
                {
                    if (!_mstLocation.Ml_is_serial)
                    {
                        int _usrSeq = _base.CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(doc_tp, Session["UserCompanyCode"].ToString(), ScanDocument, 0);
                        if (_usrSeq != -1)
                        {
                            ReptPickHeader _ReptPickHeader = _base.CHNLSVC.Inventory.GetAllScanSerialParameters(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), _usrSeq, doc_tp);
                            if (_ReptPickHeader != null)
                            {
                                if (!string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_com) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_wh_loc) && !string.IsNullOrEmpty(_ReptPickHeader.Tuh_load_bay))
                                {
                                    DisplayMessage("Serials are already picked from PDA !");
                                    return;
                                }
                            }
                        }
                    }
                }

                //dilshan on 23/04/2018************
                if (ScanItemList != null && chkOnlyQty.Checked)
                {
                     var _itm = ScanItemList.Where(c => c.Itri_itm_cd == txtItemCode.Text && c.Itri_itm_stus == ddlStatus.SelectedValue).FirstOrDefault();
                     if (!string.IsNullOrEmpty(txtItemCode.Text))
                     {
                            //decimal _qtyTmp = Convert.ToDecimal(txtQty.Text);

                            if (_itm != null)
                            {
                                if (_itm.Itri_itm_cd == txtItemCode.Text)
                                {
                                    DisplayMessage("Item Already added.");
                                    return;
                                }
                            }
                      }
                        //PickSerial
                }
                //*********************************
                //Add by Lakshan
                if (chkDirectScan.Checked == false)
                {
                    if (ScanItemList != null && PageName == "AOD-OUT" && !_derectOut)
                    {
                        var _itm = ScanItemList.Where(c => c.Itri_itm_cd == txtItemCode.Text && c.Itri_itm_stus== ddlStatus.SelectedValue).FirstOrDefault();
                        if (!string.IsNullOrEmpty(txtQty.Text))
                        {
                            decimal _qtyTmp = Convert.ToDecimal(txtQty.Text);

                            if (_itm != null)
                            {
                                if (_itm.Itri_bqty < _qtyTmp)
                                {
                                    DisplayMessage("You cannot exceed the approved quantity.");
                                    return;
                                }
                            }
                        }
                        //PickSerial
                    }
                }
                if (_batchBaseOut)
                {
                    decimal _bal = _base.CHNLSVC.Inventory.GET_INR_BATCH_BALANCE_BY_BATCH_NO(new InventoryBatchN()
                    {
                        Inb_batch_no = _batchNo,
                        Inb_itm_cd = txtItemCode.Text.Trim(),
                        Inb_itm_stus = ddlStatus.SelectedValue,
                        Inb_com = Session["UserCompanyCode"].ToString(),
                        Inb_loc = Session["UserDefLoca"].ToString()
                    });
                    decimal tmpDes = 0, _bll = 0;
                    _bll = decimal.TryParse(txtQty.Text, out tmpDes) ? Convert.ToDecimal(txtQty.Text) : 0;
                    if (_bal < _bll)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Balance not available !');", true);
                        return;
                    }
                }

                string sessionpopupcrn = (string)Session["POPUP_CRN_RETURN"];
                string passedseq = (string)Session["GEN_SEQ"];
                if (!string.IsNullOrEmpty(PageName))
                {
                    if (PageName=="AOD_OUT")
                    {
                        if (true)
                        {
                            
                        }
                    }
                }
                if (doc_tp == "COM_OUT" && Session["AOD_DIRECT"] != null)
                {
                    if (Session["AOD_DIRECT"].ToString().ToUpper() == false.ToString().ToUpper())
                    {
                        DisplayMessage("Item adding is only allowed for direct out");
                        return;
                    }
                }

                if (PNLTobechange.Visible == true)
                {
                    if (string.IsNullOrEmpty(txtItemToBeChange.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the item to be change…!')", true);
                        txtItemToBeChange.Focus();
                        return;
                    }
                    if (string.IsNullOrEmpty(ddlStatusToBeChange.Text.ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the  item status.')", true);
                        ddlStatusToBeChange.Focus();
                        return;
                    }
                    if (!IsBinTransfer)
                    {
                        if (ddlStatusToBeChange.SelectedValue.ToString() == "CONS")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('New status cannot be Consignment.')", true);
                            ddlStatusToBeChange.Focus();
                            return;
                        }
                    }

                    if (!IsBinTransfer)
                    {
                        if ((ddlStatus.SelectedValue.ToString() == ddlStatusToBeChange.SelectedValue.ToString()) && (txtItemCode.Text.ToUpper().Trim() == txtItemToBeChange.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('New status cannot be same as available status.')", true);
                            ddlStatusToBeChange.Focus();
                            return;
                        }
                    }
                    if (IsBinTransfer && string.IsNullOrEmpty(ToBinCode))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select to BIN code.')", true);
                        return;
                    }
                }

                if (doc_tp == "DISP")
                {
                    if (!_base.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10119) && !_base.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034))
                    {
                        if (((DropDownList)this.Parent.FindControl("ddlPaymentType")).SelectedValue == "PRD" && ((TextBox)this.Parent.FindControl("txtReceipt")).Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please complete the payment details.')", true);
                            return;
                        }
                    }
                    if (_base.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16034))
                    {
                        if (((DropDownList)this.Parent.FindControl("ddlPaymentType")).SelectedValue == "PRD" && ((TextBox)this.Parent.FindControl("txtReceipt")).Text == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please complete the payment details.')", true);
                            return;
                        }
                    }
                }

                if (string.IsNullOrEmpty(txtItemCode.Text))
                {
                    DisplayMessage("Please select the item code");
                    return;
                }
                MasterItem _itms = new MasterItem();
                _itms = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.ToUpper().Trim());

                if (_itms.Mi_is_ser1 == 1)
                {
                    if (string.IsNullOrEmpty(txtSerialI.Text.ToString()) && !chkOnlyQty.Checked)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the serial number…!')", true);
                        txtSerialI.Focus();
                        return;
                    }
                    //if (_itms.Mi_is_ser2 == true)
                    //{
                    //    if (string.IsNullOrEmpty(txtSerialII.Text.ToString()))
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the serial no 2.')", true);
                    //        txtSerialII.Focus();
                    //        return;
                    //    }
                    //}
                    //if (_itms.Mi_is_ser3 == true)
                    //{
                    //    if (string.IsNullOrEmpty(txtSerialIII.Text.ToString()))
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select the serial no 3.')", true);
                    //        txtSerialIII.Focus();
                    //        return;
                    //    }
                    //}
                }

                if (string.IsNullOrEmpty(txtItemCode.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the item...!');", true);
                    txtItemCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the status...!');", true);
                    ddlStatus.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the quantity...!');", true);
                    txtQty.Focus();
                    return;
                }

                if (doc_tp != "COM_OUT")
                {
                    if (!string.IsNullOrEmpty(sessionpopupcrn))
                    {
                        ViewState["userSeqNo"] = passedseq;
                    }
                   // ViewState["userSeqNo"] = 247892970;
                    if ((ViewState["userSeqNo"] == null))
                    {
                        int retValue = GenerateNewUserSeqNo();
                        userSeqNo = retValue.ToString();

                        if (retValue == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Fail Generate New User SeqNo.');", true);
                            ddlStatus.Focus();
                            return;
                        }
                    }
                }

                if (doc_tp == "DO")
                {
                    string msg;
                    AddSerials_DO(out msg);

                    if (!string.IsNullOrEmpty(msg))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                    }
                }
                else if (doc_tp == "IS")
                {
                    string msg;
                    AddSerials_IS(out msg);

                    if (!string.IsNullOrEmpty(msg))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('" + msg + "');", true);
                    }
                    return;
                }
                #region cmnts
                //else if (doc_tp == "COM_OUT")
                //{
                //    #region MyRegion
                //    ////if (Session["ScanItemListNew"] != null)
                //    ////{
                //    ////    ScanItemList = (List<ReptPickSerials>)Session["ScanItemListNew"];
                //    ////}
                //    ////else
                //    ////{
                //    ////    ScanItemList = new List<InventoryRequestItem>();
                //    ////}

                //    //if (ScanDocument != null)
                //    //{
                //    //    Int32 generated_seq = -1;
                //    //    Int32 user_seq_num = 0;
                //    //    if (msitem.Mi_is_ser1 == -1)
                //    //    {
                //    //        user_seq_num = _base.CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("COM_OUT", Session["UserCompanyCode"].ToString(), ScanDocument, 0);
                //    //        if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                //    //        {
                //    //            generated_seq = user_seq_num;
                //    //        }

                //    //        ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                //    //        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                //    //        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                //    //        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                //    //        _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                //    //        _reptPickSerial_.Tus_base_itm_line = ItemLineNo;
                //    //        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                //    //        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                //    //        _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                //    //        _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                //    //        _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                //    //        _reptPickSerial_.Tus_itm_cd = msitem.Mi_cd;
                //    //        _reptPickSerial_.Tus_itm_stus = ItemStatus;
                //    //        _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtQty.Text.ToString());
                //    //        _reptPickSerial_.Tus_ser_1 = "N/A";
                //    //        _reptPickSerial_.Tus_ser_2 = "N/A";
                //    //        _reptPickSerial_.Tus_ser_3 = "N/A";
                //    //        _reptPickSerial_.Tus_ser_4 = "N/A";
                //    //        _reptPickSerial_.Tus_ser_id = 0;
                //    //        _reptPickSerial_.Tus_serial_id = "0";
                //    //        _reptPickSerial_.Tus_job_no = JobNo;
                //    //        _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                //    //        _reptPickSerial_.Tus_job_line = JobLineNo;
                //    //        _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                //    //        _reptPickSerial_.Tus_bin_to = ToBinCode;

                //    //        // _reptPickSerial_.Tus_unit_cost = 0;
                //    //        // _reptPickSerial_.Tus_unit_price = 0;
                //    //        // _reptPickSerial_.Tus_unit_price = 0;

                //    //        //enter row into TEMP_PICK_SER
                //    //        Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                //    //        List<ReptPickSerials> _serList1 = new List<ReptPickSerials>();
                //    //        _serList1 = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "DO");
                //    //        ((GridView)this.Parent.FindControl("dvDOSerials")).DataSource = _serList1;
                //    //        ((GridView)this.Parent.FindControl("dvDOSerials")).DataBind();
                //    //    }
                //    //}
                //    //else
                //    //{

                //    //    //Direct Mode

                //    //    Int32 UserSeqNum = (Session["UserSeqNo"] != null) ? Convert.ToInt32(Session["UserSeqNo"]) : Convert.ToInt32(userSeqNo);
                //    //    //Int32 UserSeqNum = _base.CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("COM_OUT", Session["UserCompanyCode"].ToString(), "", 0);
                //    //    userSeqNo = UserSeqNum.ToString();

                //    //    TextBox txtRequest = (TextBox)this.Parent.FindControl("txtRequest");
                //    //    CheckBox chkDirectOut = (CheckBox)this.Parent.FindControl("chkDirectOut");
                //    //    DropDownList ddlRecCompany = (DropDownList)this.Parent.FindControl("ddlRecCompany");
                //    //    TextBox txtDispatchRequried = (TextBox)this.Parent.FindControl("txtDispatchRequried");

                //    //    if (string.IsNullOrEmpty(txtItemCode.Text)) return;
                //    //    if (string.IsNullOrEmpty(txtRequest.Text))
                //    //        if (!chkDirectOut.Checked)
                //    //        {
                //    //            DisplayMessage("Please select the reference document or direct method first");
                //    //            return;
                //    //        }
                //    //    if (string.IsNullOrEmpty(ddlRecCompany.Text.ToString()))
                //    //    {
                //    //        DisplayMessage("Please select receiving company");
                //    //        return;
                //    //    }
                //    //    if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                //    //    {
                //    //        DisplayMessage("Please select receiving location");
                //    //        return;
                //    //    }

                //    //    if (chkDirectOut.Checked == false)
                //    //    {
                //    //        DisplayMessage("Item adding only allow for direct out");
                //    //        return;
                //    //    }
                //    //    if (string.IsNullOrEmpty(ddlRecCompany.Text.ToString()))
                //    //    {
                //    //        DisplayMessage("Please select the receiving company");
                //    //        ddlRecCompany.Focus();
                //    //        return;
                //    //    }
                //    //    if (string.IsNullOrEmpty(txtDispatchRequried.Text))
                //    //    {
                //    //        DisplayMessage("Please select the receiving location");
                //    //        txtDispatchRequried.Focus();
                //    //        return;
                //    //    }
                //    //    if (string.IsNullOrEmpty(txtItemCode.Text.Trim()))
                //    //    {
                //    //        DisplayMessage("Please select the item");
                //    //        txtItemCode.Focus();
                //    //        return;
                //    //    }
                //    //    if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
                //    //    {
                //    //        DisplayMessage("Please select the status");
                //    //        ddlStatus.Focus();
                //    //        return;
                //    //    }
                //    //    if (string.IsNullOrEmpty(txtQty.Text))
                //    //    {
                //    //        DisplayMessage("Please select the qty");
                //    //        txtQty.Focus();
                //    //        return;
                //    //    }
                //    //    try
                //    //    {
                //    //        _itms = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.Trim());

                //    //        InventoryRequestItem _itm = new InventoryRequestItem();
                //    //        if (ScanItemList != null)
                //    //        {
                //    //            if (ScanItemList.Count > 0)
                //    //            {
                //    //                var _duplicate = from _ls in ScanItemList where _ls.Itri_itm_cd == txtItemCode.Text.Trim() && _ls.Itri_itm_stus == ddlStatus.SelectedValue.ToString() select _ls;
                //    //                if (_duplicate != null) if (_duplicate.Count() > 0)
                //    //                    {
                //    //                        DisplayMessage("Selected item already available");
                //    //                        return;
                //    //                    }
                //    //                var _maxline = (from _ls in ScanItemList select _ls.Itri_line_no).Max();
                //    //                Session["_maxline"] = _maxline;
                //    //                _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
                //    //                _itm.Itri_itm_cd = txtItemCode.Text.Trim(); _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
                //    //                _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                //    //                _itm.Itri_qty = Convert.ToDecimal(txtQty.Text);
                //    //                _itm.Mi_longdesc = _itms.Mi_longdesc;
                //    //                _itm.Mi_model = _itms.Mi_model;
                //    //                _itm.Mi_brand = _itms.Mi_brand;
                //    //            }
                //    //            else
                //    //            {
                //    //                _itm.Itri_app_qty = Convert.ToDecimal(txtQty.Text);
                //    //                _itm.Itri_itm_cd = txtItemCode.Text.Trim();
                //    //                _itm.Itri_itm_stus = ddlStatus.SelectedValue.ToString();
                //    //                _itm.Itri_line_no = 1;
                //    //                _itm.Itri_qty = Convert.ToDecimal(txtQty.Text);
                //    //                _itm.Mi_longdesc = _itms.Mi_longdesc;
                //    //                _itm.Mi_model = _itms.Mi_model;
                //    //                _itm.Mi_brand = _itms.Mi_brand;
                //    //            }
                //    //        }

                //    //        ScanItemList.Add(_itm);
                //    //        string RequestNo = Session["UserSeqNo"].ToString();

                //    //        if (chkDirectOut.Checked)
                //    //            ScanItemList.Where(x => string.IsNullOrEmpty(x.Itri_note)).ToList().ForEach(x => x.Itri_note = RequestNo);

                //    //        ((GridView)this.Parent.FindControl("gvItems")).DataSource = ScanItemList;
                //    //        ((GridView)this.Parent.FindControl("gvItems")).DataBind();
                //    //        ViewState["ScanItemList"] = ScanItemList;
                //    //        ItemList = ScanItemList;

                //    //        ddlRecCompany.Enabled = false;
                //    //        txtDispatchRequried.Enabled = false;


                //    //        // Add Selected Serial


                //    //        //DropDownList ddlRecCompany = ((DropDownList)this.Parent.FindControl("ddlRecCompany"));
                //    //        //TextBox txtDispatchRequried = ((TextBox)this.Parent.FindControl("txtDispatchRequried"));
                //    //        DropDownList cmbDirType = ((DropDownList)this.Parent.FindControl("cmbDirType"));

                //    //        //ReptPickHeader RPH = new ReptPickHeader();
                //    //        //RPH.Tuh_doc_tp = "COM_OUT";
                //    //        //RPH.Tuh_cre_dt = DateTime.Today;
                //    //        //RPH.Tuh_ischek_itmstus = false;
                //    //        //RPH.Tuh_ischek_reqqty = false;
                //    //        //RPH.Tuh_ischek_simitm = false;
                //    //        //RPH.Tuh_session_id = Session["SessionID"].ToString();
                //    //        //RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                //    //        //RPH.Tuh_usr_id = Session["UserID"].ToString();
                //    //        //RPH.Tuh_usrseq_no = UserSeqNum;
                //    //        //RPH.Tuh_direct = false;
                //    //        //RPH.Tuh_rec_com = Convert.ToString(ddlRecCompany.SelectedValue);
                //    //        //RPH.Tuh_rec_loc = Convert.ToString(txtDispatchRequried.Text);
                //    //        //RPH.Tuh_isdirect = true;
                //    //        //RPH.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                //    //        //RPH.Tuh_dir_type = Convert.ToString(cmbDirType.SelectedValue);
                //    //        //RPH.Tuh_doc_no = UserSeqNum.ToString();
                //    //        //int affected_rows = _base.CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);


                //    //        if (_itms.Mi_is_ser1 != 1)
                //    //        {
                //    //            addNonSerializeDirectOut();
                //    //            return;
                //    //        }

                //    //        //List<ReptPickSerials> oSelectedSerialList = new List<ReptPickSerials>();


                //    //        List<ReptPickSerials> oSelectedSerialList;
                //    //        if (Session["ScanItemListNew"] == null)
                //    //        {
                //    //            oSelectedSerialList = new List<ReptPickSerials>();
                //    //        }
                //    //        else
                //    //        {
                //    //            oSelectedSerialList = (List<ReptPickSerials>)Session["ScanItemListNew"];
                //    //        }

                //    //        List<ReptPickSerials> Tempserial_list = _base.CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text, string.Empty, string.Empty);
                //    //        Tempserial_list = Tempserial_list.Where(x => x.Tus_ser_1 == txtSerialI.Text).ToList();
                //    //        ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;

                //    //        var serialLine = (from _ls in ScanItemList
                //    //                          where _ls.Itri_itm_cd == txtItemCode.Text
                //    //                          select _ls.Itri_line_no).Max();

                //    //        if (Tempserial_list == null || Tempserial_list.Count == 0)
                //    //        {
                //    //            return;
                //    //        }

                //    //        ReptPickSerials oSelectedSerail = new ReptPickSerials();
                //    //        oSelectedSerail.Tus_ser_id = Tempserial_list[0].Tus_ser_id;
                //    //        oSelectedSerail.Tus_bin = ddlBinCode.SelectedValue.ToString();
                //    //        oSelectedSerail.Tus_itm_cd = txtItemCode.Text;
                //    //        oSelectedSerail.Tus_ser_1 = txtSerialI.Text.Trim();
                //    //        oSelectedSerail.Tus_itm_model = lblModel.Text.Trim();
                //    //        oSelectedSerail.Tus_itm_stus = ddlStatus.SelectedValue.ToString();
                //    //        oSelectedSerail.Tus_base_itm_line = serialLine;
                //    //        oSelectedSerail.Tus_qty = 1;
                //    //        oSelectedSerialList.Add(oSelectedSerail);

                //    //        StringBuilder _pickedserial = new StringBuilder();

                //    //        foreach (ReptPickSerials gvr in oSelectedSerialList)
                //    //        {
                //    //            Int32 serID = Convert.ToInt32(gvr.Tus_ser_id);
                //    //            string binCode = gvr.Tus_bin;
                //    //            ReptPickSerials _reptPickSerial_ = _base.CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, gvr.Tus_itm_cd, serID);
                //    //            if (string.IsNullOrEmpty(_reptPickSerial_.Tus_com))
                //    //            {
                //    //                if (_itm.Mi_is_ser1 == 1) _pickedserial.Append(gvr.Tus_ser_1 + "/");
                //    //            }
                //    //        }
                //    //        if (!string.IsNullOrEmpty(_pickedserial.ToString()))
                //    //        {
                //    //            DisplayMessage("Selected Serial " + _pickedserial.ToString() + " already picked by other process. Please check your inventory");
                //    //            return;
                //    //        }
                //    //        foreach (ReptPickSerials gvr in oSelectedSerialList)
                //    //        {
                //    //            Int32 serID = Convert.ToInt32(gvr.Tus_ser_id);
                //    //            string binCode = gvr.Tus_bin;

                //    //            ReptPickSerials _reptPickSerial_ = _base.CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, gvr.Tus_itm_cd, serID);

                //    //            if (_reptPickSerial_ == null || _reptPickSerial_.Tus_com == null)
                //    //            {
                //    //                if (_itm.Mi_is_ser1 == 0)
                //    //                {
                //    //                    _reptPickSerial_ = _base.CHNLSVC.Inventory.GetNonSerializedItemRandomly(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text, ddlStatus.SelectedValue.ToString(), 1)[0];
                //    //                    serID = _reptPickSerial_.Tus_ser_id;
                //    //                }
                //    //                if (_itm.Mi_is_ser1 == 1)
                //    //                {
                //    //                    DisplayMessage("Selected serial " + gvr.Tus_ser_1 + " already picked by another process");
                //    //                    continue;
                //    //                }
                //    //            }

                //    //            //Check saved
                //    //            List<ReptPickSerials> savedSerials = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), UserSeqNum, "COM_OUT");
                //    //            if (savedSerials != null && savedSerials.Count > 0)
                //    //            {
                //    //                if (savedSerials.FindAll(x => x.Tus_ser_id == _reptPickSerial_.Tus_ser_id && x.Tus_itm_cd == _reptPickSerial_.Tus_itm_cd).Count > 0)
                //    //                {
                //    //                    continue;
                //    //                }
                //    //            }

                //    //            Boolean update_inr_ser = _base.CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), gvr.Tus_itm_cd, serID, -1);
                //    //            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                //    //            _reptPickSerial_.Tus_usrseq_no = UserSeqNum;
                //    //            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                //    //            _reptPickSerial_.Tus_base_doc_no = UserSeqNum.ToString();
                //    //            _reptPickSerial_.Tus_base_itm_line = _itm.Itri_line_no;
                //    //            _reptPickSerial_.Tus_itm_desc = _itm.Mi_shortdesc;
                //    //            _reptPickSerial_.Tus_itm_model = _itm.Mi_model;
                //    //            _reptPickSerial_.Tus_new_remarks = DocumentTypeDecider(gvr.Tus_ser_id);
                //    //            _reptPickSerial_.Tus_job_no = "";
                //    //            _reptPickSerial_.Tus_pgs_prefix = txtItemCode.Text;
                //    //            _reptPickSerial_.Tus_job_line = Convert.ToInt32("0");
                //    //            _reptPickSerial_.Tus_itm_stus = ddlStatus.SelectedValue.ToString();
                //    //            _reptPickSerial_.Tus_qty = 1;
                //    //            _reptPickSerial_.Tus_base_itm_line = serialLine;
                //    //            _reptPickSerial_.Tus_bin_to = ToBinCode;

                //    //            if (serID > 0)
                //    //            {
                //    //                Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                //    //            }
                //    //        }

                //    //        ((GridView)this.Parent.FindControl("gvSerial")).DataSource = oSelectedSerialList;
                //    //        ((GridView)this.Parent.FindControl("gvSerial")).DataBind();

                //    //        ViewState["SerialList"] = oSelectedSerialList;
                //    //        Session["ScanItemListNew"] = oSelectedSerialList;
                //    //    }
                //    //    catch (Exception ex)
                //    //    {
                //    //        DisplayMessage(ex.Message);
                //    //    }
                //    //} 
                //    #endregion
                //} 
                #endregion
                else
                {
                    if (string.IsNullOrEmpty(sessionpopupcrn))
                    {
                        bool msgDisplay = true;
                        if (ViewState["userSeqNo"] == null && doc_tp == "COM_OUT")
                        {
                            if (Session["AOD_DIRECT"] != null)
                            {
                                if ((bool)Session["AOD_DIRECT"])
                                {
                                    msgDisplay = false;
                                }
                            }
                            if (msgDisplay)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select a pending request ...');", true);
                                return;
                            }
                        }
                        AddItem(txtItemCode.Text.ToUpper(), _itms.Mi_std_cost.ToString(), ddlStatus.SelectedItem.Value, txtQty.Text, 
                            ViewState["userSeqNo"] != null?(ViewState["userSeqNo"].ToString()):null, txtSerialI.Text);
                    }
                    else
                    {
                        ViewState["userSeqNo"] = passedseq;
                    }
                    if (!chkOnlyQty.Checked)
                    {
                        AddSerials(txtItemCode.Text.ToUpper(), txtSerialI.Text, (ViewState["userSeqNo"].ToString()), ddlStatus.SelectedItem.Value, Convert.ToDecimal(txtQty.Text));
                    }
                    if (chkOnlyQty.Checked && _derectOut && !_mstLocation.Ml_is_serial)
                    {
                        #region add serials for serial non main tain location 14 Dec 2016
                        Int32 seq = Convert.ToInt32(ViewState["userSeqNo"].ToString());
                        List<ReptPickSerials> _repSerList = _base.CHNLSVC.Inventory.GET_TEMP_PICK_SER_DATA(new ReptPickSerials() { Tus_usrseq_no = seq });
                        Int32 _lineNo = 1;
                        Int32 _baseLineNo = 1;
                        if (_repSerList != null)
                        {
                            if (_repSerList.Count > 0)
                            {
                                _lineNo = _repSerList.Where(c => c.Tus_itm_cd == txtItemCode.Text.ToUpper() && c.Tus_itm_stus == ddlStatus.SelectedItem.Value).Max(a => a.Tus_itm_line);
                                _baseLineNo = _repSerList.Where(c => c.Tus_base_doc_no == seq.ToString()).Max(a => a.Tus_base_itm_line);
                            }
                        }

                        MasterItem msitem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.ToUpper());
                        ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_usrseq_no = seq;
                        _reptPickSerial_.Tus_seq_no = seq;
                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_base_doc_no = seq.ToString();

                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(_baseLineNo);
                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                        _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                        _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                        _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                        _reptPickSerial_.Tus_itm_cd = txtItemCode.Text.ToUpper();
                        _reptPickSerial_.Tus_itm_stus = ddlStatus.SelectedItem.Value;
                        _reptPickSerial_.Tus_itm_line = Convert.ToInt32(_lineNo);
                        _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtQty.Text);
                        _reptPickSerial_.Tus_ser_1 = "N/A";
                        _reptPickSerial_.Tus_ser_2 = "N/A";
                        _reptPickSerial_.Tus_ser_3 = "N/A";
                        _reptPickSerial_.Tus_ser_4 = "DFS";
                        _reptPickSerial_.Tus_ser_id = 0;
                        _reptPickSerial_.Tus_serial_id = "0";
                        _reptPickSerial_.Tus_unit_cost = 0;
                        _reptPickSerial_.Tus_unit_price = 0;
                        _reptPickSerial_.Tus_unit_price = 0;
                        _reptPickSerial_.Tus_job_no = JobNo;
                        //_reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                        //_reptPickSerial_.Tus_job_line = JobLineNo;
                        //_reptPickSerial_.Tus_exist_supp = suppler;
                        //_reptPickSerial_.Tus_orig_supp = suppler;
                        //if (Convert.ToInt32(lblitri_res_qty.Text)>0)
                        //{
                        //    _reptPickSerial_.Tus_resqty = _reptPickSerial_.Tus_qty;
                        //}
                        Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                        List<ReptPickSerials> _serList = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(),
                            Session["UserID"].ToString(), seq, doc_tp);
                        ViewState["SerialList"] = _serList;
                        List<MasterItemStatus> oItemStaus = (List<MasterItemStatus>)Session["ItemStatus"];

                        if (_serList != null)
                        {
                            foreach (ReptPickSerials itemSer in _serList)
                            {
                                itemSer.Tus_new_status_Desc = getItemStatusDesc(itemSer.Tus_new_status);

                                if (oItemStaus != null && oItemStaus.Count > 0)
                                {
                                    itemSer.Tus_itm_stus_Desc = oItemStaus.Find(x => x.Mis_cd == itemSer.Tus_itm_stus).Mis_desc;
                                }
                            }
                        }
                        if (ViewState["SerialList"] != null)
                        {
                            serial_list = ViewState["SerialList"] as List<ReptPickSerials>;
                            //grdSerial.DataSource = serial_list;
                            //grdSerial.DataBind();
                            if (((GridView)this.Parent.FindControl("grdSerial")) != null)
                            {
                                ((GridView)this.Parent.FindControl("grdSerial")).DataSource = _serList;
                                ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
                                ViewState["SerialList"] = serial_list;
                            }
                            if(((GridView)this.Parent.FindControl("gvSerial")) != null)
                            {
                                ((GridView)this.Parent.FindControl("gvSerial")).DataSource = _serList;
                                ((GridView)this.Parent.FindControl("gvSerial")).DataBind();
                                ViewState["SerialList"] = serial_list;
                            }
                            //((GridView)this.Parent.FindControl("grdSerial")).DataSource = serial_list;
                        }
                        if (ScanItemList != null)
                        {
                            foreach (var item in ScanItemList)
                            {
                                item.Itri_qty = item.Itri_app_qty;
                                item.Itri_bqty = 0;
                            }
                            Session["ScanItemListUC"] = ScanItemList;
                            if (((GridView)this.Parent.FindControl("grdItems")) != null)
                            {
                                ((GridView)this.Parent.FindControl("grdItems")).DataSource = ScanItemList;
                                ((GridView)this.Parent.FindControl("grdItems")).DataBind();
                            }
                            else if (((GridView)this.Parent.FindControl("gvItems")) != null)
                            {
                                ((GridView)this.Parent.FindControl("gvItems")).DataSource = ScanItemList;
                                ((GridView)this.Parent.FindControl("gvItems")).DataBind();
                            };
                        }
                        #endregion
                        
                    }
                }



                string itmCOdeMsg = txtItemCode.Text.ToUpper().Trim();

                Session["SELECTED_ITEM"] = txtItemCode.Text.Trim();
                Session["SELECTED_ITEM_QTY"] = txtQty.Text.Trim();
                //Add 28/Jun/2016
                bool pageClear = true;
                if (Session["ScanItemListUC"]!=null)
                {
                    List<InventoryRequestItem> _invReqItms = (List<InventoryRequestItem>)Session["ScanItemListUC"];
                    var v = _invReqItms.Where(c => c.Itri_itm_cd == txtItemCode.Text.Trim().ToUpper()).FirstOrDefault();
                    if (v!=null)
                    {
                        if (v.Itri_bqty!=0)
                        {
                            pageClear = false;
                        }
                    }
                }
                if (Session["tempDoItemList"] != null)
                {
                    List<InvoiceItem> _invReqItms = (List<InvoiceItem>)Session["tempDoItemList"];
                    var v = _invReqItms.Where(c => c.Sad_itm_cd == txtItemCode.Text.Trim().ToUpper()).FirstOrDefault();
                    if (v != null)
                    {
                        if (v.Sad_qty > v.Sad_srn_qty)
                        {
                            pageClear = false;
                        }
                    }
                }
                if (pageClear)
                {
                    PageClear();
                    if (ScanItemList.Count > 0)
                    {
                        BindUserCompanyItemStatusDDLData(ddlStatus);
                        ddlStatus.SelectedValue = ScanItemList.LastOrDefault().Itri_itm_stus;
                    }
                }
                else
                {
                    txtSerialI.Text = "";
                    txtSerialII.Text = "";
                    txtSerialIII.Text = "";

                    txtSerialI.ReadOnly = false;
                    txtSerialII.ReadOnly = false;
                    txtSerialIII.ReadOnly = false;

                    txtSerialI.Focus();
                   
                }
                //END

                if (Session["Binnew"].ToString() == "Binnew")
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyNoticeToast('Item code : " + itmCOdeMsg + " added successfully.');", true);
                    txtSerialI.Focus();
                    return;
                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }


        private void AddSerials(string _item, string _Serial, string _Seqno, string _stus, decimal _qty)
        {
            string suppler = string.Empty;

            if (Session["CRN_SUPP"] != null)
            {
                suppler = (string)Session["CRN_SUPP"].ToString();
            }


            Int32 generated_seq = -1;
            MasterItem msitem = new MasterItem();
            List<ReptPickSerials> Tempserial_list = new List<ReptPickSerials>();
            msitem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

            string sessionpopupcrn = (string)Session["POPUP_CRN_RETURN"];

            if (!string.IsNullOrEmpty(sessionpopupcrn))
            {
                var mylist = (List<InventoryRequestItem>)Session["SCAN_ITEM_LIST"];
                ScanItemList = mylist;
            }

            if (msitem.Mi_is_ser1 == 1)
            {
                Tempserial_list = new List<ReptPickSerials>();
                Tempserial_list = _base.CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, string.Empty);
                //if (IsTemp == true)
                //{
                //    Tempserial_list = Tempserial_list.Where(x => (x.Tus_ser_1 == _Serial)).ToList();
                //}
                //else
                //{
                Tempserial_list = Tempserial_list.Where(x => x.Tus_ser_1 == _Serial).ToList();

                if (!string.IsNullOrEmpty(suppler))
                {
                    foreach (ReptPickSerials item in Tempserial_list)
                    {
                        item.Tus_orig_supp = suppler;
                        item.Tus_exist_supp = suppler;
                    }
                }
                // }
            }
            else
            {
                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                Tempserial_list = new List<ReptPickSerials>();
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                _reptPickSerial_.Tus_base_itm_line = 0;
                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                if (!IsBinTransfer)
                {
                   // _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                    _reptPickSerial_.Tus_bin = ddlBinCode.SelectedValue.ToString();
                }
                else
                {
                    _reptPickSerial_.Tus_bin = ddlBinCode.SelectedValue.ToString();
                }
                _reptPickSerial_.Tus_itm_cd = _item;
                _reptPickSerial_.Tus_itm_stus = _stus;
                _reptPickSerial_.Tus_qty = Convert.ToDecimal(_qty);
                _reptPickSerial_.Tus_ser_1 = "N/A";
                _reptPickSerial_.Tus_ser_2 = "N/A";
                _reptPickSerial_.Tus_ser_3 = "N/A";
                _reptPickSerial_.Tus_ser_4 = "N/A";
                _reptPickSerial_.Tus_ser_id = 0;
                _reptPickSerial_.Tus_serial_id = "0";
                _reptPickSerial_.Tus_unit_cost = 0;
                _reptPickSerial_.Tus_unit_price = 0;
                _reptPickSerial_.Tus_unit_price = 0;
                _reptPickSerial_.Tus_job_no = JobNo;
                _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                _reptPickSerial_.Tus_job_line = JobLineNo;
                _reptPickSerial_.Tus_exist_supp = suppler;
                _reptPickSerial_.Tus_orig_supp = suppler;
                _reptPickSerial_.Tus_pkg_uom_tp = _derectOut ? ddlBoxTp.SelectedItem.Text : null;
                decimal _tmpBQty = 0, _pkgQty = 0;
                _pkgQty = decimal.TryParse(lblPkgQty.Text, out _tmpBQty) ? Convert.ToDecimal(lblPkgQty.Text) : 0;
                _reptPickSerial_.Tus_pkg_uom_qty = !_derectOut ? 0 : _pkgQty;

                if (isResQTY == true)
                {
                    _reptPickSerial_.Tus_resqty = Convert.ToDecimal(_qty);
                }

                Tempserial_list.Add(_reptPickSerial_);
            }

            var serialLine = 0;
            var _baseItmLine = 0;
            if ((ScanItemList != null))
            {
                if (ScanItemList.Count > 0)
                {
                    serialLine = (from _ls in ScanItemList
                                  where _ls.Itri_itm_cd == _item
                                  select _ls.Itri_line_no).Max();
                    _baseItmLine = ScanItemList.Where(c => c.Itri_itm_cd == _item && c.Itri_itm_stus == _stus).FirstOrDefault().Itri_line_no;
                }
            }
            Int32 user_seq_num = -1;
            foreach (ReptPickSerials serial in Tempserial_list)
            {
                #region PRN
                if (ViewState["userSeqNo"] == null)
                {
                    user_seq_num = _base.CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(ViewState["doc_tp"].ToString(), Session["UserCompanyCode"].ToString(), _Seqno, 0);
                }
                else
                {
                    user_seq_num = Convert.ToInt32(ViewState["userSeqNo"].ToString());
                }

                if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                {
                    generated_seq = user_seq_num;
                }
                else
                {
                    generated_seq = _base.CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), ViewState["doc_tp"].ToString(), 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
                    //assign user_seqno
                    ReptPickHeader RPH = new ReptPickHeader();
                    RPH.Tuh_doc_tp = ViewState["doc_tp"].ToString();
                    RPH.Tuh_cre_dt = DateTime.Today;
                    RPH.Tuh_ischek_itmstus = true;
                    RPH.Tuh_ischek_reqqty = true;
                    RPH.Tuh_ischek_simitm = true;
                    RPH.Tuh_session_id = Session["SessionID"].ToString();
                    RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    RPH.Tuh_usr_id = Session["UserID"].ToString();
                    RPH.Tuh_usrseq_no = generated_seq;
                    RPH.Tuh_direct = false;
                    RPH.Tuh_doc_no = _Seqno;
                    //write entry to TEMP_PICK_HDR
                    int affected_rows = _base.CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);
                }

                if (msitem.Mi_is_ser1 != -1)
                {

                    string DocType = ViewState["doc_tp"].ToString();
                    if (DocType == "PRN")
                    {
                        #region PRN
                        List<ReptPickSerials> _serList2 = new List<ReptPickSerials>();
                        _serList2 = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), Convert.ToInt32(_Seqno), "PRN");
                        if (_serList2 != null)
                        {
                            if ((_serList2.Count > 0))
                            {
                               // var _filter = _serList2.SingleOrDefault(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == serial.Tus_itm_stus);
                                var _filter = _serList2.Where(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == serial.Tus_itm_stus).FirstOrDefault();
                                if (_filter != null && msitem.Mi_is_ser1 == 0)
                                {

                                    _filter.Tus_qty = _filter.Tus_qty + Convert.ToDecimal(_qty);
                                    Int32 value = _base.CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                                }
                                else
                                {
                                    if (msitem.Mi_is_ser1 == 0)
                                    {
                                        #region MyRegion
                                        ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                        _reptPickSerial_.Tus_seq_no = generated_seq;
                                        _reptPickSerial_.Tus_base_doc_no = _Seqno;
                                        _reptPickSerial_.Tus_doc_no = serial.Tus_doc_no;
                                        _reptPickSerial_.Tus_exist_grnno = serial.Tus_doc_no;
                                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(serialLine); //Convert.ToInt32(serial.Tus_itm_line);
                                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                        _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                        _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                        if (!IsBinTransfer)
                                        {
                                            _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                                        }
                                        else
                                        {
                                            _reptPickSerial_.Tus_bin = ddlBinCode.SelectedValue.ToString();
                                        }
                                        _reptPickSerial_.Tus_itm_cd = serial.Tus_itm_cd;
                                        _reptPickSerial_.Tus_itm_stus = serial.Tus_itm_stus;

                                        _reptPickSerial_.Tus_itm_stus_Desc = getItemStatusDesc(_reptPickSerial_.Tus_itm_stus);

                                        _reptPickSerial_.Tus_qty = Convert.ToDecimal(serial.Tus_qty);
                                        _reptPickSerial_.Tus_ser_1 = _Serial;
                                        _reptPickSerial_.Tus_ser_2 = "N/A";
                                        _reptPickSerial_.Tus_ser_3 = "N/A";
                                        _reptPickSerial_.Tus_ser_4 = "N/A";
                                        _reptPickSerial_.Tus_ser_id = 0;
                                        _reptPickSerial_.Tus_serial_id = "0";
                                        _reptPickSerial_.Tus_unit_cost = 0;
                                        _reptPickSerial_.Tus_unit_price = 0;
                                        _reptPickSerial_.Tus_unit_price = 0;
                                        _reptPickSerial_.Tus_job_no = serial.Tus_job_no;
                                        _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
                                        _reptPickSerial_.Tus_bin_to = ToBinCode;
                                        _reptPickSerial_.Tus_exist_supp = suppler;
                                        _reptPickSerial_.Tus_orig_supp = suppler;
                                        if (doc_tp == "COM_OUT")
                                        {
                                            _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                                        }

                                        //enter row into TEMP_PICK_SER

                                        if (PNLTobechange.Visible == true)
                                        {
                                            _reptPickSerial_.Tus_new_itm_cd = txtItemToBeChange.Text;//newitemCode;
                                            _reptPickSerial_.Tus_new_status = ddlStatusToBeChange.SelectedItem.Value;//newItemStatus;
                                        }

                                        Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                        #endregion
                                    }
                                    if (msitem.Mi_is_ser1 == 1)
                                    {
                                        #region MyRegion
                                        int rowCount = 0;
                                        //-------------

                                        ReptPickSerials _reptPickSerial_ = _base.CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_bin, serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id));
                                        //Update_inrser_INS_AVAILABLE
                                        Boolean update_inr_ser = _base.CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id), -1);

                                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                        _reptPickSerial_.Tus_seq_no = generated_seq;
                                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                        _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                        _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                        _reptPickSerial_.Tus_doc_no = serial.Tus_doc_no;
                                        _reptPickSerial_.Tus_exist_grnno = serial.Tus_doc_no;
                                        _reptPickSerial_.Tus_base_doc_no = _Seqno;
                                        _reptPickSerial_.Tus_itm_cd = _item;
                                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(serialLine);//Convert.ToInt32(serial.Tus_itm_line);
                                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                        _reptPickSerial_.Tus_job_no = serial.Tus_job_no;//JobNo;
                                        _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
                                        _reptPickSerial_.Tus_itm_stus = _stus;
                                        _reptPickSerial_.Tus_ser_1 = _Serial;
                                        _reptPickSerial_.Tus_bin_to = ToBinCode;
                                        _reptPickSerial_.Tus_bin = ddlBinCode.SelectedValue.ToString();
                                        _reptPickSerial_.Tus_exist_supp = suppler;
                                        _reptPickSerial_.Tus_orig_supp = suppler;

                                        List<ReptPickSerials> tempList = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ViewState["doc_tp"].ToString());

                                        //if (tempList != null && tempList.Count > 0)
                                        //{
                                        //    var _maxline = (from _ls in tempList
                                        //                    select _ls.Tus_itm_line).Max();
                                        //    _reptPickSerial_.Tus_itm_line = Convert.ToInt32(_maxline) + 1;
                                        //}
                                        //else
                                        //{

                                        //}

                                        if (doc_tp == "COM_OUT")
                                        {
                                            _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                                        }

                                        if (PNLTobechange.Visible == true)
                                        {
                                            _reptPickSerial_.Tus_new_itm_cd = txtItemToBeChange.Text;//newitemCode;
                                            _reptPickSerial_.Tus_new_status = ddlStatusToBeChange.SelectedItem.Value;//newItemStatus;          
                                            _reptPickSerial_.Tus_new_status_Desc = getItemStatusDesc(_reptPickSerial_.Tus_new_status);
                                        }

                                        if (_reptPickSerial_.Tus_qty == 0 && msitem.Mi_is_ser1 == 0)
                                        {
                                            _reptPickSerial_.Tus_qty = Convert.ToDecimal(_qty);
                                        }

                                        if (!IsBinTransfer)
                                        {
                                            //enter row into TEMP_PICK_SER
                                            Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                        }
                                        else
                                        {
                                            if (tempList != null && tempList.Count > 0 && tempList.FindAll(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == _stus && x.Tus_new_status == _reptPickSerial_.Tus_new_status).Count > 0)
                                            {
                                                if (msitem.Mi_is_ser1 != 1)
                                                {
                                                    Int32 resuilt = _base.CHNLSVC.Inventory.UPDATE_QTY_ITM_STUS_NEWSTUS(Convert.ToDecimal(txtQty.Text), _reptPickSerial_.Tus_seq_no, _item, _stus, _reptPickSerial_.Tus_new_status);
                                                }
                                                else
                                                {
                                                    Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                                }
                                            }

                                            else
                                            {
                                                Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                            }
                                        }

                                        rowCount++;
                                        #endregion
                                    }
                                }

                            }
                            else
                            {
                                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                                _reptPickSerial_.Tus_seq_no = generated_seq;
                                _reptPickSerial_.Tus_base_doc_no = _Seqno;
                                _reptPickSerial_.Tus_doc_no = serial.Tus_doc_no;
                                _reptPickSerial_.Tus_exist_grnno = serial.Tus_doc_no;
                                _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(serialLine); //Convert.ToInt32(serial.Tus_itm_line);
                                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                if (!IsBinTransfer)
                                {
                                    _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();
                                }
                                else
                                {
                                    _reptPickSerial_.Tus_bin = ddlBinCode.SelectedValue.ToString();
                                }
                                _reptPickSerial_.Tus_itm_cd = serial.Tus_itm_cd;
                                _reptPickSerial_.Tus_itm_stus = serial.Tus_itm_stus;

                                _reptPickSerial_.Tus_itm_stus_Desc = getItemStatusDesc(_reptPickSerial_.Tus_itm_stus);

                                _reptPickSerial_.Tus_qty = Convert.ToDecimal(serial.Tus_qty);
                                _reptPickSerial_.Tus_ser_1 = _Serial;
                                _reptPickSerial_.Tus_ser_2 = "N/A";
                                _reptPickSerial_.Tus_ser_3 = "N/A";
                                _reptPickSerial_.Tus_ser_4 = "N/A";
                                _reptPickSerial_.Tus_ser_id = 0;
                                _reptPickSerial_.Tus_serial_id = "0";
                                _reptPickSerial_.Tus_unit_cost = 0;
                                _reptPickSerial_.Tus_unit_price = 0;
                                _reptPickSerial_.Tus_unit_price = 0;
                                _reptPickSerial_.Tus_job_no = serial.Tus_job_no;
                                _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
                                _reptPickSerial_.Tus_bin_to = ToBinCode;

                                if (doc_tp == "COM_OUT")
                                {
                                    _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                                }

                                _reptPickSerial_.Tus_exist_supp = suppler;
                                _reptPickSerial_.Tus_orig_supp = suppler;
                                //enter row into TEMP_PICK_SER

                                if (PNLTobechange.Visible == true)
                                {
                                    _reptPickSerial_.Tus_new_itm_cd = txtItemToBeChange.Text;//newitemCode;
                                    _reptPickSerial_.Tus_new_status = ddlStatusToBeChange.SelectedItem.Value;//newItemStatus;
                                }

                                Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            }
                        }
                        else
                        {
                            int rowCount = 0;
                            //-------------

                            ReptPickSerials _reptPickSerial_ = _base.CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_bin, serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id));
                            //Update_inrser_INS_AVAILABLE
                            Boolean update_inr_ser = _base.CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id), -1);

                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_usrseq_no = generated_seq;
                            _reptPickSerial_.Tus_seq_no = generated_seq;
                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                            _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                            _reptPickSerial_.Tus_doc_no = serial.Tus_doc_no;
                            _reptPickSerial_.Tus_exist_grnno = serial.Tus_doc_no;
                            _reptPickSerial_.Tus_base_doc_no = _Seqno;
                            _reptPickSerial_.Tus_itm_cd = _item;
                            _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(serialLine);//Convert.ToInt32(serial.Tus_itm_line);
                            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                            _reptPickSerial_.Tus_job_no = serial.Tus_job_no;//JobNo;
                            _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
                            _reptPickSerial_.Tus_itm_stus = _stus;
                            _reptPickSerial_.Tus_ser_1 = _Serial;
                            _reptPickSerial_.Tus_bin_to = ToBinCode;
                            _reptPickSerial_.Tus_bin = ddlBinCode.SelectedValue.ToString();
                            _reptPickSerial_.Tus_exist_supp = suppler;
                            _reptPickSerial_.Tus_orig_supp = suppler;

                            List<ReptPickSerials> tempList = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ViewState["doc_tp"].ToString());

                            //if (tempList != null && tempList.Count > 0)
                            //{
                            //    var _maxline = (from _ls in tempList
                            //                    select _ls.Tus_itm_line).Max();
                            //    _reptPickSerial_.Tus_itm_line = Convert.ToInt32(_maxline) + 1;
                            //}
                            //else
                            //{

                            //}

                            if (doc_tp == "COM_OUT")
                            {
                                _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                            }

                            if (PNLTobechange.Visible == true)
                            {
                                _reptPickSerial_.Tus_new_itm_cd = txtItemToBeChange.Text;//newitemCode;
                                _reptPickSerial_.Tus_new_status = ddlStatusToBeChange.SelectedItem.Value;//newItemStatus;          
                                _reptPickSerial_.Tus_new_status_Desc = getItemStatusDesc(_reptPickSerial_.Tus_new_status);
                            }

                            if (_reptPickSerial_.Tus_qty == 0 && msitem.Mi_is_ser1 == 0)
                            {
                                _reptPickSerial_.Tus_qty = Convert.ToDecimal(_qty);
                            }

                            if (!IsBinTransfer)
                            {
                                //enter row into TEMP_PICK_SER
                                Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            }
                            else
                            {
                                if (tempList != null && tempList.Count > 0 && tempList.FindAll(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == _stus && x.Tus_new_status == _reptPickSerial_.Tus_new_status).Count > 0)
                                {
                                    if (msitem.Mi_is_ser1 != 1)
                                    {
                                        Int32 resuilt = _base.CHNLSVC.Inventory.UPDATE_QTY_ITM_STUS_NEWSTUS(Convert.ToDecimal(txtQty.Text), _reptPickSerial_.Tus_seq_no, _item, _stus, _reptPickSerial_.Tus_new_status);
                                    }
                                    else
                                    {
                                        Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                    }
                                }

                                else
                                {
                                    Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                }
                            }

                            rowCount++;
                        }

                        #endregion


                    }

                    else
                    {
                        #region
                        int rowCount = 0;
                        //-------------
                        if ((ScanDocument != "") && (ScanDocument != null) && (ScanDocument != "N/A"))
                        {
                            _Seqno = ScanDocument;
                        }
                        else
                        {
                            _Seqno = generated_seq.ToString();
                        }
                        ReptPickSerials _reptPickSerial_ = _base.CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_bin, serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id));
                        //Update_inrser_INS_AVAILABLE
                        Boolean update_inr_ser = _base.CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), serial.Tus_itm_cd, Convert.ToInt32(serial.Tus_ser_id), -1);

                        string _origSup=_reptPickSerial_.Tus_orig_supp;
                        string _excstSup=_reptPickSerial_.Tus_exist_supp;
                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                        // _reptPickSerial_.Tus_seq_no = generated_seq;
                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                        _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                        _reptPickSerial_.Tus_doc_no = serial.Tus_doc_no;
                        _reptPickSerial_.Tus_exist_grnno = serial.Tus_doc_no;
                        _reptPickSerial_.Tus_base_doc_no = _Seqno;
                        _reptPickSerial_.Tus_itm_cd = _item;
                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(serialLine);//Convert.ToInt32(serial.Tus_itm_line);
                        if (doc_tp == "MRNA")
                        {
                            _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(_baseItmLine);
                        }
                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                        _reptPickSerial_.Tus_job_no = serial.Tus_job_no;//JobNo;
                        _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
                        _reptPickSerial_.Tus_itm_stus = _stus;
                        _reptPickSerial_.Tus_ser_1 = msitem.Mi_is_ser1 == 0 ? "N/A" : _Serial;
                        _reptPickSerial_.Tus_bin_to = ToBinCode;
                        _reptPickSerial_.Tus_bin = ddlBinCode.SelectedValue.ToString();
                        _reptPickSerial_.Tus_exist_supp = suppler;
                        _reptPickSerial_.Tus_orig_supp = suppler;
                        if (string.IsNullOrEmpty(suppler))
                        {
                            _reptPickSerial_.Tus_exist_supp = _excstSup;
                            _reptPickSerial_.Tus_orig_supp = _origSup;
                        }
                        _reptPickSerial_.Tus_pkg_uom_tp = _derectOut ? ddlBoxTp.SelectedItem.Text : null;
                        decimal _tmpBQty = 0, _pkgQty = 0;
                        _pkgQty = decimal.TryParse(lblPkgQty.Text, out _tmpBQty) ? Convert.ToDecimal(lblPkgQty.Text) : 0;
                        _reptPickSerial_.Tus_pkg_uom_qty = !_derectOut ? 0 : _pkgQty;

                        if (isResQTY == true)
                        {
                            _reptPickSerial_.Tus_resqty = Convert.ToDecimal(_qty);
                        }
                        List<ReptPickSerials> tempList = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ViewState["doc_tp"].ToString());

                        //if (tempList != null && tempList.Count > 0)
                        //{
                        //    var _maxline = (from _ls in tempList
                        //                    select _ls.Tus_itm_line).Max();
                        //    _reptPickSerial_.Tus_itm_line = Convert.ToInt32(_maxline) + 1;
                        //}
                        //else
                        //{

                        //}

                        if (doc_tp == "COM_OUT")
                        {
                            _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                        }

                        if (PNLTobechange.Visible == true)
                        {
                            _reptPickSerial_.Tus_new_itm_cd = txtItemToBeChange.Text;//newitemCode;
                            _reptPickSerial_.Tus_new_status = ddlStatusToBeChange.SelectedItem.Value;//newItemStatus;          
                            _reptPickSerial_.Tus_new_status_Desc = getItemStatusDesc(_reptPickSerial_.Tus_new_status);
                        }

                        if (_reptPickSerial_.Tus_qty == 0 && msitem.Mi_is_ser1 == 0)
                        {
                            _reptPickSerial_.Tus_qty = Convert.ToDecimal(_qty);
                        }

                        if (!IsBinTransfer)
                        {
                            if (_reptPickSerial_.Tus_ser_1 == "" && tempList != null && DocType == "DISP")
                            {
                                var _filter = tempList.FirstOrDefault(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == serial.Tus_itm_stus);
                                if (_filter != null)
                                {
                                    _filter.Tus_qty = _filter.Tus_qty + Convert.ToDecimal(_qty);
                                    Int32 value = _base.CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                                }
                                else
                                {
                                    Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                }
                            }
                            else if ((_reptPickSerial_.Tus_ser_1 == "" || _reptPickSerial_.Tus_ser_1 == "N/A") && tempList != null && DocType == "ADJ")
                            {
                                var _filter = tempList.FirstOrDefault(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == serial.Tus_itm_stus && x.Tus_bin == serial.Tus_bin);
                                if (_filter != null)
                                {
                                    _filter.Tus_qty = _filter.Tus_qty + Convert.ToDecimal(_qty);
                                    Int32 value = _base.CHNLSVC.Inventory.UpdateAllScanSerials(_filter);
                                }
                                else
                                {
                                    Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                }
                            }
                            else
                            {
                                //enter row into TEMP_PICK_SER
                                Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            }
                        }
                        else
                        {
                            if (tempList != null && tempList.Count > 0 && tempList.FindAll(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == _stus && x.Tus_new_status == _reptPickSerial_.Tus_new_status).Count > 0)
                            {
                                if (msitem.Mi_is_ser1 != 1)
                                {
                                    Int32 resuilt = _base.CHNLSVC.Inventory.UPDATE_QTY_ITM_STUS_NEWSTUS(Convert.ToDecimal(txtQty.Text), _reptPickSerial_.Tus_seq_no, _item, _stus, _reptPickSerial_.Tus_new_status);
                                }
                                else
                                {
                                    Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                }
                            }
                            else
                            {
                                Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                            }
                        }

                        rowCount++;
                        //isManualscan = true;
                        #endregion
                    }
                }
                else
                {
                    ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = generated_seq;
                    _reptPickSerial_.Tus_seq_no = generated_seq;
                    _reptPickSerial_.Tus_base_doc_no = _Seqno;
                    if (!string.IsNullOrEmpty(ScanDocument))
                    {
                        if (doc_tp=="INTR" || doc_tp=="MRNA" || doc_tp=="REQD" || doc_tp=="MRN" || doc_tp=="EX" || doc_tp=="RE" || doc_tp=="BOI")
                        {
                            _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                        }
                    }
                    _reptPickSerial_.Tus_doc_no = serial.Tus_doc_no;
                    _reptPickSerial_.Tus_exist_grnno = serial.Tus_doc_no;
                    _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(serialLine); //Convert.ToInt32(serial.Tus_itm_line);
                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                    if (!IsBinTransfer)
                    {
                        _reptPickSerial_.Tus_bin = ddlBinCode.SelectedValue.ToString();
                    }
                    else
                    {
                        _reptPickSerial_.Tus_bin = ddlBinCode.SelectedValue.ToString();
                    }
                    _reptPickSerial_.Tus_itm_cd = serial.Tus_itm_cd;
                    _reptPickSerial_.Tus_itm_stus = serial.Tus_itm_stus;

                    _reptPickSerial_.Tus_itm_stus_Desc = getItemStatusDesc(_reptPickSerial_.Tus_itm_stus);

                    _reptPickSerial_.Tus_qty = Convert.ToDecimal(serial.Tus_qty);
                    _reptPickSerial_.Tus_ser_1 = string.IsNullOrEmpty(_Serial) ? "N/A" : _Serial;
                    _reptPickSerial_.Tus_ser_2 = "N/A";
                    _reptPickSerial_.Tus_ser_3 = "N/A";
                    _reptPickSerial_.Tus_ser_4 = "N/A";
                    _reptPickSerial_.Tus_ser_id = 0;
                    _reptPickSerial_.Tus_serial_id = "0";
                    _reptPickSerial_.Tus_unit_cost = 0;
                    _reptPickSerial_.Tus_unit_price = 0;
                    _reptPickSerial_.Tus_unit_price = 0;
                    _reptPickSerial_.Tus_job_no = serial.Tus_job_no;
                    _reptPickSerial_.Tus_job_line = serial.Tus_job_line;
                    _reptPickSerial_.Tus_bin_to = ToBinCode;
                    _reptPickSerial_.Tus_exist_supp = suppler;
                    _reptPickSerial_.Tus_orig_supp = suppler;
                    _reptPickSerial_.Tus_pkg_uom_qty = serial.Tus_pkg_uom_qty;
                    _reptPickSerial_.Tus_pkg_uom_tp = serial.Tus_pkg_uom_tp;

                    if (doc_tp == "COM_OUT")
                    {
                        _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                    }

                    //enter row into TEMP_PICK_SER

                    if (PNLTobechange.Visible == true)
                    {
                        _reptPickSerial_.Tus_new_itm_cd = txtItemToBeChange.Text;//newitemCode;
                        _reptPickSerial_.Tus_new_status = ddlStatusToBeChange.SelectedItem.Value;//newItemStatus;
                    }

                    Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                }

                #endregion
            }

            List<ReptPickSerials> _serList = new List<ReptPickSerials>();
           
          

            if (isConsReturnNote)
            {
                Int32 newUserSeq = _base.CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), doc_tp, Session["UserID"].ToString(), direction, DOC_No);
                _serList = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(),
                    newUserSeq, doc_tp);
            }
            else
            {
                _serList = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ViewState["doc_tp"].ToString());
            }

            List<MasterItemStatus> oItemStaus = (List<MasterItemStatus>)Session["ItemStatus"];
          
            if (_serList != null)
            {
                foreach (ReptPickSerials itemSer in _serList)
                {
                    itemSer.Tus_new_status_Desc = getItemStatusDesc(itemSer.Tus_new_status);

                    if (oItemStaus != null && oItemStaus.Count > 0)
                    {
                        itemSer.Tus_itm_stus_Desc = oItemStaus.Find(x => x.Mis_cd == itemSer.Tus_itm_stus).Mis_desc;
                    }
                }
            }
            //MasterItemStatus itob = new MasterItemStatus();
            //foreach()
            //{

            //}

            if (ScanItemList != null)
            {
                string DocType = ViewState["doc_tp"].ToString();
                if (DocType == "PRN")
                {
                    foreach (var item in ScanItemList)
                    {
                        item.Itri_bqty = item.Itri_qty;
                        item.Itri_app_qty = item.Itri_qty;
                        //if (item.Itri_app_qty < item.Itri_qty)
                        //{
                        //    item.Itri_app_qty = item.Itri_qty;
                        //    item.Itri_bqty = 0;
                        //}
                        //else
                        //{
                        //    item.Itri_bqty = item.Itri_app_qty - item.Itri_qty;
                        //}
                    }
                }
                else
                {
                    if (isConsReturnNote)
                    {
                        //foreach (var item in ScanItemList)
                        //{
                        //    item.Itri_bqty = _serList.Where(c=> c.Tus_itm_cd==item.Itri_itm_cd && c.Tus_itm_stus== item.Itri_itm_stus).Sum(c=> c.Tus_qty);
                        //    item.Itri_bqty = 0;
                        //}
                    }
                    else
                    {
                        foreach (var item in ScanItemList)
                        {
                            if (item.Itri_app_qty < item.Itri_qty)
                            {
                                item.Itri_app_qty = item.Itri_qty;
                                item.Itri_bqty = 0;
                            }
                            else
                            {
                                item.Itri_bqty = item.Itri_app_qty - item.Itri_qty;
                            }
                        }
                    }
                }
            }

            if (ViewState["SerialList"] != null)
            {
                serial_list = ViewState["SerialList"] as List<ReptPickSerials>;
                serial_list = serial_list.Concat(Tempserial_list).ToList();
                //grdSerial.DataSource = serial_list;
                //grdSerial.DataBind();
                if (((GridView)this.Parent.FindControl("grdSerial")) != null)
                {
                    ((GridView)this.Parent.FindControl("grdSerial")).DataSource = _serList;
                    ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
                    ViewState["SerialList"] = serial_list;
                    return;
                }
                else if (((GridView)this.Parent.FindControl("gvSerial")) != null)
                {
                    ((GridView)this.Parent.FindControl("gvSerial")).DataSource = _serList;
                    ((GridView)this.Parent.FindControl("gvSerial")).DataBind();
                    ViewState["SerialList"] = serial_list;
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('grdSerial not found.');", true);
                    return;
                }
                //((GridView)this.Parent.FindControl("grdSerial")).DataSource = serial_list;

            }
            //grdSerial.DataSource = Tempserial_list;
            //grdSerial.DataBind();
         
            //foreach (var item in _serList)
            //{
            //    _serListNEW.Add(item);
            //}

            if (((GridView)this.Parent.FindControl("grdSerial")) != null)
            {
                ((GridView)this.Parent.FindControl("grdSerial")).DataSource = _serList;
                ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
                ViewState["SerialList"] = _serList;
            }
            else if (((GridView)this.Parent.FindControl("gvSerial")) != null)
            {
                ((GridView)this.Parent.FindControl("gvSerial")).DataSource = _serList;
                ((GridView)this.Parent.FindControl("gvSerial")).DataBind();
                ViewState["SerialList"] = _serList;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('grdSerial not found.');", true);
                return;
            }
            // Session["ScanItemListUC"] = Session["ScanItemListUC"] = ScanItemList;
            //if (doc_tp == "COM_OUT")
            //{

            

            Session["ScanItemListUC"] = ScanItemList;
            if (((GridView)this.Parent.FindControl("grdItems")) != null)
            {
                ((GridView)this.Parent.FindControl("grdItems")).DataSource = ScanItemList;
                ((GridView)this.Parent.FindControl("grdItems")).DataBind();
            }
            else if (((GridView)this.Parent.FindControl("gvItems")) != null)
            {
                ((GridView)this.Parent.FindControl("gvItems")).DataSource = ScanItemList;
                ((GridView)this.Parent.FindControl("gvItems")).DataBind();
                if (ScanItemList != null)
                {
                    Label lblDocSerPickQty = this.Parent.FindControl("lblDocSerPickQty") as Label;
                    if (lblDocSerPickQty != null)
                    {
                        decimal _pickQty = ScanItemList.Sum(c => c.Itri_qty);
                        lblDocSerPickQty.Text = _pickQty.ToString("N2");
                    }
                }
            };
            #region MyRegion
            
          
            //if (isConsReturnNote)
            //{
            //    if (_serList != null)
            //    {
            //        if (direction == 0)
            //        {
            //            //var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
            //            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
            //            foreach (var itm in _scanItems)
            //            {
            //                foreach (GridViewRow row in ((GridView)this.Parent.FindControl("grdItems")).Rows)
            //                {
            //                    if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
            //                    {
            //                        //((Label)row.FindControl("lblitm_PickQty")).Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
            //                        ((Label)row.FindControl("lblitri_qty")).Text = Convert.ToDecimal(itm.theCount).ToString("N2"); // Current scan qty
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
            //            foreach (var itm in _scanItems)
            //            {
            //                foreach (GridViewRow row in ((GridView)this.Parent.FindControl("grdItems")).Rows)
            //                {
            //                    if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
            //                    {
            //                        ((Label)row.FindControl("lblitri_qty")).Text = Convert.ToDecimal(itm.theCount).ToString("N2"); // Current scan qty
            //                    }
            //                }
            //            }
            //        }
            //        if (((GridView)this.Parent.FindControl("grdSerial")) == null)
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('grdSerial not found.');", true);
            //            return;
            //        }
            //        ((GridView)this.Parent.FindControl("grdSerial")).DataSource = _serList;
            //        ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
            //    }
            //}
            #endregion
        }
        protected void grdInventoryBalance_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                ////DocSubType
                //if (lblvalue.Text == "123")
                //{
                //    txtSubType.Text = grdResult.SelectedRow.Cells[1].Text;
                //    txtSubType_TextChanged(null, null);
                //}
                ////MovementDocDateSearch
                //if (lblvalue.Text == "128")
                //{
                //    txtDocumentNo.Text = grdResult.SelectedRow.Cells[1].Text;
                //    txtDocumentNo_TextChanged(null, null);
                //}

                //ViewState["SEARCH"] = null;

                // ddlStatus.SelectedValue = grdInventoryBalance.SelectedRow.Cells[4].Text;
                ddlStatus.SelectedValue = (grdInventoryBalance.SelectedRow.FindControl("INL_ITM_STUS") as Label).Text;
                if (ddlStatusToBeChange.SelectedItem != null)
                {
                    ddlStatusToBeChange.SelectedItem.Text = grdInventoryBalance.SelectedRow.Cells[1].Text;
                }


                // ddlStatus.DataBind();
                foreach (GridViewRow row1 in grdInventoryBalance.Rows)
                {
                    row1.BackColor = System.Drawing.Color.White;
                }

                foreach (GridViewRow row in grdInventoryBalance.Rows)
                {
                    for (int i = 0; i < grdInventoryBalance.Columns.Count; i++)
                    {
                        String header = grdInventoryBalance.Columns[i].HeaderText;
                        String cellText = row.Cells[i].Text;

                        if (cellText == ddlStatus.SelectedItem.Text)
                        {
                            row.BackColor = System.Drawing.Color.LightBlue;
                        }

                    }


                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void AddItem(string _item, string _UnitCost, string _status, string _qty, string _UserSeqNo, string _serial)
        {
            try
            {
                
                MasterItem _itms = new MasterItem();
                _itms = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                InventoryRequestItem _itm = new InventoryRequestItem();
                //  ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;
                int itemline = 0;

                Int32 NewSeq = 0;
                if (string.IsNullOrEmpty(_UserSeqNo))
                {
                    NewSeq = GenerateNewUserSeqNo();
                    _UserSeqNo = NewSeq.ToString();
                }

                if (ScanItemList != null)
                {
                    if (ScanItemList.Count > 0)
                    {

                        //ScanItemList._itri_itm_cd = _item;
                        var _duplicate = from _ls in ScanItemList
                                         where _ls.Itri_itm_cd == _item && _ls.Itri_itm_stus == _status && _ls.Itri_mitm_stus == ddlStatusToBeChange.Text
                                         select _ls;

                        if (IsBinTransfer)
                        {
                            _duplicate = from _ls in ScanItemList
                                         where _ls.Itri_itm_cd == _item && _ls.Itri_itm_stus == _status && _ls.Itri_note == ddlStatusToBeChange.Text
                                         select _ls;
                        }

                        //dilshan on 23/04/2018************
                        //if (chkOnlyQty.Checked)
                        //{
                        //    if (_duplicate.Count() > 0)
                        //    {
                        //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Item Already Added')", true);
                        //        return;
                        //    }
                        //}
                        //*********************************

                        if (_duplicate != null)
                            if (_duplicate.Count() > 0)
                            {
                                var line = from d in _duplicate
                                           select d.Itri_line_no;
                                itemline = (line).First();
                                (from res in ScanItemList
                                 where res.Itri_line_no == itemline //&& res.Sad_itm_stus == _itemstatus
                                 select res).ToList<InventoryRequestItem>().ForEach(x => x.Itri_qty = x.Itri_qty + Convert.ToDecimal(txtQty.Text.ToString()));

                                //itemline = (line).First();
                                //(from res in ScanItemList
                                // where res.Itri_line_no == itemline //&& res.Sad_itm_stus == _itemstatus
                                // select res).ToList<InventoryRequestItem>().ForEach(x => x.Itri_app_qty = x.Itri_app_qty + Convert.ToDecimal(txtQty.Text.ToString()));

                                itemline = (line).First();
                                (from res in ScanItemList
                                 where res.Itri_line_no == itemline //&& res.Sad_itm_stus == _itemstatus
                                 select res).ToList<InventoryRequestItem>().ForEach(x => x.Itri_bqty = x.Itri_bqty - Convert.ToDecimal(txtQty.Text.ToString()));
                                ScanItemList = ScanItemList;
                            }
                            else
                            {
                                var _maxline = (from _ls in ScanItemList
                                                select _ls.Itri_line_no).Max();
                                _itm.Itri_itm_cd = _item;
                                _itm.Itri_itm_stus = _status;
                                _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                                _itm.Itri_qty =chkOnlyQty.Checked?0: Convert.ToDecimal(_qty);
                                if (isApprovalSend == true)
                                {
                                    _itm.Itri_app_qty = 0;
                                    _itm.Itri_bqty = 0;
                                }
                                else
                                {
                                    _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                                    _itm.Itri_bqty =Convert.ToDecimal(_qty);
                                }
                                _itm.Mi_longdesc = _itms.Mi_longdesc;
                                _itm.Mi_model = _itms.Mi_model;
                                _itm.Mi_brand = _itms.Mi_brand;
                                //Added by Prabhath on 17/12/2013 ************* start **************
                                _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                                //Added by Prabhath on 17/12/2013 ************* end **************

                                if (PNLTobechange.Visible == true)
                                {
                                    _itm.Itri_note = ddlStatusToBeChange.SelectedValue.ToString();
                                    _itm.Itri_mitm_cd = txtItemToBeChange.Text;
                                }
                                if (doc_tp == "COM_OUT")
                                {
                                    _itm.Itri_note = userSeqNo;
                                }
                                ScanItemList.Add(_itm);
                            }
                    }
                    else
                    {
                        _itm.Itri_itm_cd = _item;
                        _itm.Itri_itm_stus = _status;

                        _itm.Itri_line_no = 1;
                        _itm.Itri_qty =chkOnlyQty.Checked?0: Convert.ToDecimal(_qty);
                        if (isApprovalSend == true)
                        {
                            _itm.Itri_app_qty = 0;
                            _itm.Itri_bqty = 0;
                        }
                        else
                        {
                            _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                            _itm.Itri_bqty = chkOnlyQty.Checked ? 0 : Convert.ToDecimal(_qty);
                            if (_derectOut && chkOnlyQty.Checked)
                            {
                               _itm.Itri_bqty =  Convert.ToDecimal(_qty); 
                            }
                        }
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        //Added by Prabhath on 17/12/2013 ************* start **************
                        _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                        //Added by Prabhath on 17/12/2013 ************* end **************
                        if (PNLTobechange.Visible == true)
                        {
                            _itm.Itri_note = ddlStatusToBeChange.SelectedValue.ToString();
                            _itm.Itri_mitm_cd = txtItemToBeChange.Text;
                        }
                        _itm.Itri_note = ddlStatusToBeChange.SelectedValue.ToString();
                        _itm.Itri_itm_stus_desc = getItemStatusDesc(_status);
                        _itm.Itri_note_desc = getItemStatusDesc(ddlStatusToBeChange.SelectedValue.ToString());
                        _itm.Itri_seq_no = Convert.ToInt32(_UserSeqNo);

                        if (doc_tp == "COM_OUT")
                        {
                            _itm.Itri_note = userSeqNo;
                        }

                        ScanItemList = new List<InventoryRequestItem>();
                        ScanItemList.Add(_itm);
                    }
                }
                else
                {
                    _itm.Itri_itm_cd = _item;
                    _itm.Itri_itm_stus = _status;
                    _itm.Itri_line_no = 1;
                    _itm.Itri_qty = Convert.ToDecimal(_qty);
                    if (isApprovalSend == true)
                    {
                        _itm.Itri_app_qty = 0;
                        _itm.Itri_bqty = 0;
                    }
                    else
                    {
                        _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_bqty = Convert.ToDecimal(_qty);
                    }
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    //Added by Prabhath on 17/12/2013 ************* start **************
                    _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                    //Added by Prabhath on 17/12/2013 ************* end **************

                    if (PNLTobechange.Visible == true)
                    {
                        _itm.Itri_note = ddlStatusToBeChange.SelectedValue.ToString();
                        _itm.Itri_mitm_cd = txtItemToBeChange.Text;
                    }

                    _itm.Itri_itm_stus_desc = getItemStatusDesc(_status);
                    _itm.Itri_note_desc = getItemStatusDesc(ddlStatusToBeChange.SelectedValue.ToString());

                    ScanItemList = new List<InventoryRequestItem>();
                    ScanItemList.Add(_itm);
                }

                //Test
                List<ReptPickItems> _reptItemsTemp = new List<ReptPickItems>();
                _reptItemsTemp = _base.CHNLSVC.Inventory.GetAllScanRequestItemsList(Convert.ToInt32(ViewState["userSeqNo"].ToString()));

                List<MasterItemStatus> oItemStaus = (List<MasterItemStatus>)Session["ItemStatus"];

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(ViewState["userSeqNo"].ToString());
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_itm_stus);
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;

                    if (_addedItem.MasterItem == null)
                    {
                        _addedItem.MasterItem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _addedItem.Itri_itm_cd);
                    }

                    if (!IsBinTransfer)
                    {
                        if (_reptItemsTemp.Count > 0)
                        {
                            ReptPickItems oItem = _reptItemsTemp.Find(x => x.Tui_req_itm_qty == _addedItem.Itri_app_qty && x.Tui_req_itm_cd == _addedItem.Itri_itm_cd && _reptitm.Tui_req_itm_stus == _addedItem.Itri_itm_stus);
                            if (oItem != null)
                            {
                                _reptitm.Tui_pic_itm_cd = oItem.Tui_pic_itm_cd;
                                _reptitm.Tui_pic_itm_stus = oItem.Tui_pic_itm_stus;
                            }
                        }
                    }
                    else
                    {
                        _reptitm.Tui_pic_itm_stus = "1";
                    }
                    _saveonly.Add(_reptitm);

                    if (oItemStaus != null && oItemStaus.Count > 0)
                    {
                        _addedItem.Mis_desc = oItemStaus.Find(x => x.Mis_cd == _addedItem.Itri_itm_stus).Mis_desc;
                    }

                    _addedItem.Itri_itm_stus_desc = getItemStatusDesc(_addedItem.Itri_itm_stus);
                    _addedItem.Itri_note_desc = getItemStatusDesc(_addedItem.Itri_note);
                }
                _base.CHNLSVC.Inventory.SavePickedItems(_saveonly);

                //grdItems.DataSource = null;
                //grdItems.DataSource = ScanItemList;
                //grdItems.DataBind();

                if (((GridView)this.Parent.FindControl("grdItems")) != null)
                {
                    if (_derectOut)
                    {
                        Session["ScanItemListNew"] = ScanItemList;
                    }
                    ((GridView)this.Parent.FindControl("grdItems")).DataSource = ScanItemList;
                    ((GridView)this.Parent.FindControl("grdItems")).DataBind();

                }
                else if (((GridView)this.Parent.FindControl("gvItems")) != null)
                {
                    if (_derectOut)
                    {
                        Session["ScanItemListNew"] = ScanItemList;
                    }
                    ((GridView)this.Parent.FindControl("gvItems")).DataSource = ScanItemList;
                    ((GridView)this.Parent.FindControl("gvItems")).DataBind();
                    if (ScanItemList != null)
                    {
                        Label lblDocSerPickQty = this.Parent.FindControl("lblDocSerPickQty") as Label;
                        if (lblDocSerPickQty!=null)
                        {
                            decimal _pickQty = ScanItemList.Sum(c => c.Itri_qty);
                            lblDocSerPickQty.Text = _pickQty.ToString("N2"); 
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('grdItems not found.');", true);
                    return;
                }

                // ViewState["ScanItemList"] = ScanItemList;
                ItemList = ScanItemList;
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);

                _base.CHNLSVC.CloseChannel();
                return;
            }
        }
        protected void lbtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                //if (hdnClear.Value == "Yes")
                //{
                pnlPkg.Visible = false;
                lblPkgQty.Text = "";
                PageClear();
                grdInventoryBalance.DataSource = new int[] { };
                grdInventoryBalance.DataBind();
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }


        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                //Item
                if (lblvalue.Text == "ProductionPlanItem")
                {

                    txtItemCode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItemCode_TextChanged(null, null);
                }
                if (lblvalue.Text == "BatchBase")
                {

                    txtItemCode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItemCode_TextChanged(null, null);
                }
                if (lblvalue.Text == "3")
                {
                  
                    txtItemCode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItemCode_TextChanged(null, null);
                }
                if (lblvalue.Text == "3_PDA")
                {
                    txtPDAItemcode.Text = grdResult.SelectedRow.Cells[1].Text;
                    PDAITemLoad();
                }
                //ItemToBeChange
                if (lblvalue.Text == "C3")
                {
                    txtItemToBeChange.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItemToBeChange_TextChanged(null, null);
                }
                //AvailableSerialWithTypes
                if (lblvalue.Text == "134")
                {
                    DataTable _dtSer = new DataTable();
                    _dtSer = _base.CHNLSVC.Inventory.GetSerialDetailsBySerialwithoutItem(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), grdResult.SelectedRow.Cells[1].Text);
                    
                    if (_dtSer.Rows.Count > 1)
                    {
                        string itmstus = grdResult.SelectedRow.Cells[3].Text;

                        DataTable dtstatus = _base.CHNLSVC.Sales.GetItemStatusVal(itmstus);

                        string _itemval = string.Empty;

                        foreach (DataRow itemval in dtstatus.Rows)
                        {
                            _itemval = itemval["MIS_CD"].ToString();
                        }

                        ddlStatus.SelectedValue = _itemval;
                        ddlStatusToBeChange.SelectedValue = _itemval;

                        txtSerialI.Text = grdResult.SelectedRow.Cells[1].Text;
                        txtSerialI_TextChangedDuplicate();
                    }
                    else
                    {
                        txtSerialI.Text = grdResult.SelectedRow.Cells[1].Text;
                        txtSerialI_TextChanged(null, null);
                    }
                }

                if (lblvalue.Text == "ItemWithBin")
                {
                    txtItemCode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItemCode_TextChanged(null, null);
                }

                if (lblvalue.Text == "SearialSearchTOBIN")
                {
                    txtSerialI.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSerialI_TextChanged(null, null);
                }
                if (lblvalue.Text == "Serial")
                {
                    txtSerialI.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSerialI_TextChanged(null, null);
                }
                if (lblvalue.Text == "BatchBaseSerial")
                {
                    txtSerialI.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSerialI_TextChanged(null, null);
                }
                if (lblvalue.Text == "ProductBaseSerial")
                {
                    txtSerialI.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSerialI_TextChanged(null, null);
                }
                Session["SP"] = null;
                ViewState["SEARCH"] = null;
                UserPopup.Hide();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    FilterData();
                    UserPopup.Show();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
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
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item_Serials:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "1" + seperator + Batchno);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ProductConditionUpdate:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + "1" + seperator + _prodNo);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BatchBase:
                    {
                        paramsText.Append(_batchNo + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes:
                    {
                        paramsText.Append(Session["_SerialSearchType"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtItemCode.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemWithBin:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + BinCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearialSearchTOBIN:
                    {
                        paramsText.Append(Session["_SerialSearchType"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtItemCode.Text.ToUpper() + seperator + BinCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial:
                    {
                        string suppCd = "";
                        if (!string.IsNullOrEmpty(Session["SupplierCode"].ToString()))
                        {
                            suppCd = (string)Session["SupplierCode"];
                        }
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + suppCd + seperator + txtItemCode.Text.ToUpper());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ProductionPlanItem:
                    {
                        paramsText.Append(_prodNo + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.JobNo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString()
                            + seperator + "1" + seperator + JobNo + seperator + JobLineNo + seperator);
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
        private void FilterData()
        {
            //Item
            if (lblvalue.Text == "3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = _base.CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "3";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            if (lblvalue.Text == "3_PDA")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = _base.CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "3_PDA";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            //ItemToBeChange
            if (lblvalue.Text == "C3")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = _base.CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "C3";
                ViewState["SEARCH"] = result;
                Session["SP"] = "true";
                UserPopup.Show();
            }
            //AvailableSerialWithTypes
            if (lblvalue.Text == "134")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                DataTable result = _base.CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "134";
                ViewState["SEARCH"] = result;
                Session["SP"] = "true";
                UserPopup.Show();
            }
            if (lblvalue.Text == "ItemWithBin")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemWithBin);
                DataTable result = _base.CHNLSVC.CommonSearch.GetItemSearchDataWithBIN(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "ItemWithBin";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            if (lblvalue.Text == "SearialSearchTOBIN")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearialSearchTOBIN);
                DataTable result = _base.CHNLSVC.CommonSearch.SearchSerialByLocBIN(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "SearialSearchTOBIN";
                ViewState["SEARCH"] = result;
                Session["SP"] = "true";
                UserPopup.Show();
            }
            if (lblvalue.Text == "Serial")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                DataTable result = _base.CHNLSVC.Inventory.GetSupplierItemSerial(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                result.Columns.Remove("Serial 3");
                grdResult.DataSource = result;
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Serial";
                ViewState["SEARCH"] = result;
                Session["SP"] = "true";
                UserPopup.Show();
            }
            if (lblvalue.Text == "BatchBase")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BatchBase);
                DataTable result = _base.CHNLSVC.CommonSearch.GetItemSearchDataForBatch(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "BatchBase";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            if (lblvalue.Text == "BatchBaseSerial")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                DataTable result = _base.CHNLSVC.Inventory.SearchSerialsInr_Batchno(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim());
               
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "BatchBaseSerial";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            if (lblvalue.Text == "ProductBaseSerial")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ProductConditionUpdate);
                DataTable result = _base.CHNLSVC.Inventory.SearchSerialsByProductionNo(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim());

                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "ProductBaseSerial";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
            if (lblvalue.Text == "JobNo")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.JobNo);
                DataTable result = _base.CHNLSVC.CommonSearch.SearchSerialsByJobNo(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text.Trim());
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "JobNo";
                ViewState["SEARCH"] = result;
                UserPopup.Show();
            }
        }
        private void LoadItems(string _seqNo)
        {
            try
            {
                int _direction = 0;
                if (ViewState["adjustmentTypeValue"].ToString() == "+")
                {
                    _direction = 1;
                }

                Int32 user_seq_num = _base.CHNLSVC.Inventory.Get_Scan_SeqNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), ViewState["doc_tp"].ToString(), Session["UserID"].ToString(), _direction, _seqNo);
                if (user_seq_num == -1)
                {
                    user_seq_num = GenerateNewUserSeqNo();
                }

                List<InventoryRequestItem> _itmList = new List<InventoryRequestItem>();
                List<ReptPickItems> _reptItems = new List<ReptPickItems>();
                _reptItems = _base.CHNLSVC.Inventory.GetAllScanRequestItemsList(user_seq_num);
                foreach (ReptPickItems _reptitem in _reptItems)
                {
                    InventoryRequestItem _itm = new InventoryRequestItem();
                    MasterItem _itms = new MasterItem();
                    _itms = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _reptitem.Tui_req_itm_cd);
                    _itm.Itri_app_qty = _reptitem.Tui_req_itm_qty;
                    _itm.Itri_itm_cd = _reptitem.Tui_req_itm_cd;
                    _itm.Itri_itm_stus = _reptitem.Tui_req_itm_stus;
                    _itm.Itri_line_no = Convert.ToInt32(_reptitem.Tui_pic_itm_cd.ToString());
                    _itm.Itri_qty = _reptitem.Tui_pic_itm_qty;
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    _itm.Itri_unit_price = Convert.ToDecimal(_reptitem.Tui_pic_itm_stus.ToString());
                    _itm.Itri_supplier = _reptitem.Tui_sup;
                    _itm.Itri_batchno = _reptitem.Tui_batch;
                    _itm.Itri_grnno = _reptitem.Tui_grn;
                    _itm.Itri_grndate = _reptitem.Tui_grn_dt;
                    _itm.Itri_expdate = _reptitem.Tui_exp_dt;
                  //  _itm.Itri_bqty=
                    if (PNLTobechange.Visible == true)
                    {
                        _itm.Itri_note = ddlStatusToBeChange.SelectedValue.ToString();
                        //_itm.Itri_note =

                    }

                    _itmList.Add(_itm);
                }
                ScanItemList = _itmList.OrderBy(o => o.Itri_line_no).ToList(); ;

                //gvItems.AutoGenerateColumns = false;
                //gvItems.DataSource = ScanItemList;
                if (((GridView)this.Parent.FindControl("grdItems")) == null)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('grdItems not found.');", true);
                    return;
                }
                ((GridView)this.Parent.FindControl("grdItems")).DataSource = ScanItemList;
                ((GridView)this.Parent.FindControl("grdItems")).DataBind();


                List<ReptPickSerials> _serList = new List<ReptPickSerials>();
                _serList = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, ViewState["doc_tp"].ToString());
                if (_serList != null)
                {
                    if (_direction == 0)
                    {
                        //var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                        foreach (var itm in _scanItems)
                        {
                            foreach (GridViewRow row in ((GridView)this.Parent.FindControl("grdItems")).Rows)
                            {
                                if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString() && itm.Peo.Tus_base_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
                                {
                                    //((Label)row.FindControl("lblitm_PickQty")).Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
                                    ((Label)row.FindControl("lblitri_qty")).Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
                                }
                            }
                        }
                    }
                    else
                    {
                        var _scanItems = _serList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                        foreach (var itm in _scanItems)
                        {
                            foreach (GridViewRow row in ((GridView)this.Parent.FindControl("grdItems")).Rows)
                            {
                                if (itm.Peo.Tus_itm_cd == ((Label)row.FindControl("lblitri_itm_cd")).Text.ToString() && itm.Peo.Tus_itm_line == Convert.ToInt32(((Label)row.FindControl("lblitri_line_no")).Text.ToString()))
                                {
                                    ((Label)row.FindControl("lblitri_qty")).Text = Convert.ToDecimal(itm.theCount).ToString(); // Current scan qty
                                }
                            }
                        }
                    }
                    //gvSerial.AutoGenerateColumns = false;
                    //gvSerial.DataSource = _serList;
                    if (((GridView)this.Parent.FindControl("grdSerial")) == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('grdSerial not found.');", true);
                        return;
                    }
                    ((GridView)this.Parent.FindControl("grdSerial")).DataSource = _serList;
                    ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
                }
                else
                {
                    List<ReptPickSerials> emptyGridList = new List<ReptPickSerials>();
                    //gvSerial.AutoGenerateColumns = false;
                    //gvSerial.DataSource = emptyGridList;
                    if (((GridView)this.Parent.FindControl("grdSerial")) == null)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('grdSerial not found.');", true);
                        return;
                    }
                    ((GridView)this.Parent.FindControl("grdSerial")).DataSource = emptyGridList;
                    ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
                }
                //gvItems.AutoGenerateColumns = false;
                //gvItems.DataSource = gvItems;
            }
            catch (Exception err)
            {
                //Cursor.Current = Cursors.Default;
                //CHNLSVC.CloseChannel();
                //MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }
        private bool LoadItemDetail(string _item)
        {
            bool _isValid = false;
            try
            {
                _chargeType = string.Empty;
                //lblDescription.Text = string.Empty;
                //lblModel.Text = string.Empty;
                //lblBrand.Text = string.Empty;
                //lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
                PageClear();
                grdInventoryBalance.DataSource = new int[] { };
                grdInventoryBalance.DataBind();
                txtItemCode.Text = _item;
                _itemdetail = new MasterItem();

                if (!string.IsNullOrEmpty(_item)) _itemdetail = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (_itemdetail != null)
                {
                    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        if (_itemdetail.Mi_itm_tp == "V")
                        {
                            //txtItemCode.Text = string.Empty;
                            //lblDescription.Text = string.Empty;
                            //lblModel.Text = string.Empty;
                            //lblBrand.Text = string.Empty;
                            //lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
                            //txtUnitCost.Text = string.Empty;
                            //txtQty.Text = string.Empty;
                            PageClear();
                            grdInventoryBalance.DataSource = new int[] { };
                            grdInventoryBalance.DataBind();
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Cannot add virtual items.');", true);
                            return true;
                        }

                        _isValid = true;
                        _chargeType = _itemdetail.Mi_chg_tp;
                        string _description = _itemdetail.Mi_longdesc;
                        string _model = _itemdetail.Mi_model;
                        string _brand = _itemdetail.Mi_brand;
                        string _partNo = _itemdetail.Mi_part_no;
                        string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Serialized" : "Non-Serialized";

                        if (_itemdetail.Mi_is_ser1 == 1)
                        {
                            pnlSerialized.Visible = true;
                        }
                        else
                        {
                            pnlSerialized.Visible = false;
                            // chkDirectScan.Checked = false;
                        }

                        lblDescription.Text = _description;
                        lblModel.Text = _model;
                        lblBrand.Text = _brand;
                        lblPart.Text = _partNo;
                        //lblItemSerialStatus.Text = "Serial Status : " + _serialstatus;
                        _isDecimalAllow = _base.CHNLSVC.Inventory.IsUOMDecimalAllow(_item);
                        if (_itemdetail.MI_IS_EXP_DT == 1)
                        {
                            divExpiryDate.Visible = true;
                            //dtExp.Visible = true;
                        }
                        else
                        {
                            divExpiryDate.Visible = false;
                            //dtExp.Visible = false;
                        }
                    }
                }
                else
                {
                    txtItemCode.Text = string.Empty;
                    lblDescription.Text = string.Empty;
                    lblModel.Text = string.Empty;
                    lblBrand.Text = string.Empty;
                    //lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
                    //txtUnitCost.Text = string.Empty;
                    txtQty.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Invalid item code.');", true);
                }
            }
            catch (Exception ex)
            {
                _isValid = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing....');", true);
                return _isValid;
            }
            return _isValid;
        }
        private void BindUserCompanyItemStatusDDLData(DropDownList ddl)
        {
            DataTable _tbl = _base.CHNLSVC.Inventory.GetAllCompanyStatus(Session["UserCompanyCode"].ToString()); ;
            if (ViewState["doc_tp"] == "ADJ")
            {
                for (int i = _tbl.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = _tbl.Rows[i];
                    if (dr[1].ToString() == "CONS")
                    {
                        dr.Delete();
                    }
                }
                _tbl.AcceptChanges();

            }
            var _s = (from L in _tbl.AsEnumerable()
                      select new
                      {
                          MIS_DESC = L.Field<string>("MIS_DESC"),
                          MIC_CD = L.Field<string>("MIC_CD")
                      }).ToList();
            var _n = new { MIS_DESC = string.Empty, MIC_CD = string.Empty };
            _s.Insert(0, _n);
            ddl.DataSource = _s;
            ddl.DataTextField = "MIS_DESC";
            ddl.DataValueField = "MIC_CD";
            ddl.DataBind();
            ddl.SelectedValue = "GOD";
        }
        private Int32 GenerateNewUserSeqNo()
        {
            Int32 generated_seq = 0;
            Int16 _direction = 0;
            if (ViewState["adjustmentTypeValue"].ToString() == "+")
            {
                _direction = 1;
            }
            generated_seq = _base.CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), ViewState["doc_tp"].ToString(), _direction, Session["UserCompanyCode"].ToString());//direction always =1 for this method                    //assign user_seqno

            ReptPickHeader RPH = new ReptPickHeader();

            if (ViewState["doc_tp"] == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please set doc_tp.')", true);
                return 0;
            }
            if (((TextBox)this.Parent.FindControl("txtUserSeqNo")) == null && ((Label)this.Parent.FindControl("lblseq")) == null)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('txtUserSeqNo not found.')", true);
                return 0;
            }

            RPH.Tuh_doc_tp = ViewState["doc_tp"].ToString();
            RPH.Tuh_cre_dt = DateTime.Today;//might change //Calendar-SelectedDate;
            RPH.Tuh_ischek_itmstus = true;//might change 
            RPH.Tuh_ischek_reqqty = true;//might change
            RPH.Tuh_ischek_simitm = true;//might change
            RPH.Tuh_session_id = Session["SessionID"].ToString();
            RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();//might change 
            RPH.Tuh_usr_id = Session["UserID"].ToString();
            RPH.Tuh_usrseq_no = generated_seq;
            if (_direction == 1)//direction always (-) for change status
            {
                RPH.Tuh_direct = true;
            }
            else
            {
                RPH.Tuh_direct = false;
            }
            RPH.Tuh_doc_no = generated_seq.ToString();
            if (_derectOut)
            {
                RPH.Tuh_usr_loc = Session["UserDefLoca"].ToString();
                RPH.Tuh_isdirect = true;
            }
            //write entry to TEMP_PICK_HDR
            int affected_rows = _base.CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

            if (affected_rows == 1)
            {
                userSeqNo = generated_seq.ToString();
                if ((TextBox)this.Parent.FindControl("txtUserSeqNo") == null)
                {
                    ((Label)this.Parent.FindControl("lblseq")).Text = ViewState["userSeqNo"].ToString();
                }
                else
                {
                    ((TextBox)this.Parent.FindControl("txtUserSeqNo")).Text = ViewState["userSeqNo"].ToString();
                }
                return generated_seq;
            }
            else
            {
                return 0;
            }
        }

        public void GridClear()
        {
            ViewState["SerialList"] = null;
            ViewState["ScanItemList"] = null;

            ((GridView)this.Parent.FindControl("grdItems")).DataSource = null;
            ((GridView)this.Parent.FindControl("grdItems")).DataBind();
            ((GridView)this.Parent.FindControl("grdSerial")).DataSource = null;
            ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
            Response.Redirect(Request.RawUrl, false);
        }
        public void PageClear()
        {
            //_pkgTblTp = "";
            
            if (_ispda == false)
            {
                Defaul.Visible = true;
                pnlPDA.Visible = false;
            }
            else
            {
                pnlPDA.Visible = true;
                Defaul.Visible = false;
            }
            chkDirectScan.Checked = false;

            ddlItemType.SelectedIndex = -1;
            txtItemCode.ReadOnly = false;
            txtItemCode.Text = string.Empty;
            ddlBinCode.Items.Clear();
            ddlBinCode.SelectedIndex = -1;
            ddlStatus.Items.Clear();
            ddlStatus.SelectedIndex = -1;
            txtQty.ReadOnly = false;
            txtQty.Text = string.Empty;

            txtItemToBeChange.Text = string.Empty;
            ddlStatusToBeChange.SelectedIndex = -1;

            ddlManufactureDate.Items.Clear();
            ddlManufactureDate.SelectedIndex = -1;
            ddlExpiryDate.Items.Clear();
            ddlExpiryDate.SelectedIndex = -1;

            txtSerialI.Text = string.Empty;
            txtSerialII.Text = string.Empty;
            txtSerialIII.Text = string.Empty;
            txtSerialI.ReadOnly = false;
            txtSerialII.ReadOnly = false;
            txtSerialIII.ReadOnly = false;

            lblDescription.Text = string.Empty;
            lblModel.Text = string.Empty;
            lblBrand.Text = string.Empty;
            lblPart.Text = string.Empty;

            grdInventoryBalance.DataSource = new int[] { };
            grdInventoryBalance.DataBind();
            txtQty.Enabled = true;
            if (PNLTobechange.Visible == true)
            {
                BindUserCompanyItemStatusDDLData(ddlStatusToBeChange);
            }

            ViewState["SerialID"] = string.Empty;
            ViewState["reptPickSerial"] = null;
            Session["tempDoItemList"] = new List<InvoiceItem>();
            ViewState["SerialList"] = null;
            ItemList = new List<InventoryRequestItem>();
            reptPickSerial = new List<ReptPickSerials>();

            txtItemToBeChange.ReadOnly = false;
            lbtnItemToBeChange.Enabled = true;
            if (IsBinTransfer)
            {
                txtItemToBeChange.ReadOnly = true;
                lbtnItemToBeChange.Enabled = false;
            }
            Session["EXCELPOPUP"] = null;
            chkOnlyQty.Checked=false;
        }

        private void AddSerials_DO(out string msg)
        {
            msg = string.Empty;

            List<InvoiceItem> invoice_items = (List<InvoiceItem>)Session["dvDOItems"];
            List<ReptPickSerials> _serList = (List<ReptPickSerials>)Session["dvDOSerials"];
            if (invoice_items == null)
            {
                msg = "Please select the document again";
                return;

            }
            if (_serList == null)
            {
                _serList = new List<ReptPickSerials>();
            }

            MasterItem msitem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.ToUpper().Trim());
            Int32 num_of_checked_itms = 1;
            Int32 PickQty = 0;
            Int32 generated_seq = -1;
            string MainItemCode = msitem.Mi_cd;
            decimal lblpopUpQty = 0;
            decimal totalpick = 0;
            Int32 _itemLineNo = 0;
            string _invoiceNo = string.Empty;
            string ItemStatus = string.Empty;

            if (invoice_items.FindAll(x => x.Sad_itm_cd == txtItemCode.Text.ToUpper().Trim() && x.Sad_itm_stus == DDLStatus.SelectedValue.ToString()).Count > 0)
            {
                InvoiceItem oSelectedItem = invoice_items.Find(x => x.Sad_itm_cd == txtItemCode.Text.ToUpper().Trim() && x.Sad_itm_stus == DDLStatus.SelectedValue.ToString());
                if (oSelectedItem != null)
                {
                    _itemLineNo = oSelectedItem.Sad_itm_line;
                    _invoiceNo = oSelectedItem.Sad_inv_no;
                    ItemStatus = oSelectedItem.Sad_itm_stus;
                    lblpopUpQty = oSelectedItem.Sad_qty - oSelectedItem.Sad_do_qty;
                    totalpick = oSelectedItem.Sad_srn_qty + Convert.ToDecimal(txtQty.Text);
                }
            }
            else if (invoice_items.FindAll(x => x.Sad_sim_itm_cd == txtItemCode.Text.ToUpper().Trim() && x.Sad_itm_stus == DDLStatus.SelectedValue.ToString()).Count > 0)
            {
                InvoiceItem oSelectedItem = invoice_items.Find(x => x.Sad_sim_itm_cd == txtItemCode.Text.ToUpper().Trim() && x.Sad_itm_stus == DDLStatus.SelectedValue.ToString());
                if (oSelectedItem != null)
                {
                    _itemLineNo = oSelectedItem.Sad_itm_line;
                    _invoiceNo = oSelectedItem.Sad_inv_no;
                    ItemStatus = oSelectedItem.Sad_itm_stus;
                    lblpopUpQty = oSelectedItem.Sad_qty - oSelectedItem.Sad_do_qty;
                    totalpick = oSelectedItem.Sad_srn_qty + Convert.ToDecimal(txtQty.Text);
                }
            }
            else
            {
                bool IsPriceLevelAllowDoAnyStatus = false;
                if (invoice_items != null)
                {
                    var _invItm = invoice_items.Where(x => x.Sad_itm_cd == txtItemCode.Text.ToUpper().Trim()).FirstOrDefault();
                    if (_invItm != null)
                    {
                        List<PriceBookLevelRef> _lvls = _base.CHNLSVC.Sales.GetPriceLevelList(Session["UserCompanyCode"].ToString(), _invItm.Sad_pbook, _invItm.Sad_pb_lvl);
                        if (_lvls != null && _lvls.Count > 0)
                        {
                            var _pbLvl = _lvls.Where(c => c.Sapl_itm_stuts == DDLStatus.SelectedValue.ToString() && c.Sapl_act == true).FirstOrDefault();
                            if (_pbLvl != null)
                            {
                                IsPriceLevelAllowDoAnyStatus = true;
                            }
                        }
                        if (IsPriceLevelAllowDoAnyStatus)
                        {
                            _itemLineNo = _invItm.Sad_itm_line;
                            _invoiceNo = _invItm.Sad_inv_no;
                            //ItemStatus = _invItm.Sad_itm_stus;
                            ItemStatus=DDLStatus.SelectedValue.ToString();
                            lblpopUpQty = _invItm.Sad_qty - _invItm.Sad_do_qty;
                            totalpick = _invItm.Sad_srn_qty + Convert.ToDecimal(txtQty.Text);
                        }
                        else
                        {
                            msg = "Please select a valid serial with the item status"; return;
                        }
                    }
                    else
                    {
                        msg = "Please select a valid serial with the item status"; return;
                    }
                }
                else 
                {
                    msg = "Please select a valid serial with the item status"; return;
                }
            }
            List<MasterItemStatus>  oMasterItemStatuss = _base.CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
           
            string ScanDocument = _invoiceNo;
            if (Convert.ToInt32(lblpopUpQty) < totalpick)
            {
                msg = ("Cant exceed the request quantity!");
                return;
            }
            if (msitem.Mi_is_ser1 == -1)
            {
                //if (Convert.ToInt32(lblpopUpQty) < Convert.ToDecimal(txtQty.Text))
                //{
                //    msg = ("Cant exceed the request quantity!");
                //    return;
                //}

                List<InventoryLocation> _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), msitem.Mi_cd, ItemStatus);
                if (_inventoryLocation != null)
                {
                    if (_inventoryLocation.Count == 1)
                    {
                        foreach (InventoryLocation _loc in _inventoryLocation)
                        {
                            decimal _formQty = Convert.ToDecimal(txtQty.Text);
                            if (_formQty > _loc.Inl_free_qty)
                            {
                                msg = ("Please check the inventory balance!");
                                txtQty.Text = string.Empty;
                                txtQty.Focus();
                                return;
                            }
                        }
                    }
                    else
                    {
                        msg = ("Please check the inventory balance!");
                        return;
                    }
                }
                else
                {
                    msg = ("Please check the inventory balance!");
                    return;
                }
            }

            ////Decimal pending_amt = lblpopUpQty - Convert.ToDecimal(lblScanQty.Text.ToString());
            ////if (num_of_checked_itms > pending_amt)
            ////{
            ////    msg = ("Can't exceed Approved Qty. You can add only " + pending_amt + " itmes more.");
            ////    return;
            ////}

            Int32 serialID = 0;

            List<ReptPickSerials> Tempserial_list = _base.CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), string.Empty, string.Empty);
            if (Tempserial_list.Count > 0)
            {
                if (msitem.Mi_is_ser1 == 1)
                {
                    Tempserial_list = Tempserial_list.Where(x => x.Tus_ser_1 == txtSerialI.Text.Trim()).ToList();
                    if (Tempserial_list != null && Tempserial_list.Count == 1)
                    {
                        serialID = Tempserial_list[0].Tus_ser_id;
                    }
                    else
                    {
                        msg = "Cannot find the serial";
                        return;
                    }
                }
            }

            bool _isWriteToTemporaryTable = true;
            Int32 user_seq_num = 0;
            string _docTp = "DO";
            if (_isQutationBaseDO)
            {
                _docTp = "QUO";
            }
            if (_isWriteToTemporaryTable)//Added by Prabhath on 14/06/2013
            {
                user_seq_num = _base.CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE(_docTp, Session["UserCompanyCode"].ToString(), ScanDocument, 0);
                if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
                {
                    generated_seq = user_seq_num;
                }
                else
                {
                    generated_seq = _base.CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), _docTp, 1, Session["UserCompanyCode"].ToString());//direction always =1 for this method
                    //assign user_seqno
                    ReptPickHeader RPH = new ReptPickHeader();
                    RPH.Tuh_doc_tp = _docTp;
                    RPH.Tuh_cre_dt = DateTime.Today;
                    RPH.Tuh_ischek_itmstus = true;
                    RPH.Tuh_ischek_reqqty = true;
                    RPH.Tuh_ischek_simitm = true;
                    RPH.Tuh_session_id = Session["SessionID"].ToString();
                    RPH.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    RPH.Tuh_usr_id = Session["UserID"].ToString();
                    RPH.Tuh_usrseq_no = generated_seq;

                    RPH.Tuh_direct = false;
                    RPH.Tuh_doc_no = ScanDocument;
                    //write entry to TEMP_PICK_HDR
                    int affected_rows = _base.CHNLSVC.Inventory.SaveSeq_to_TempPickHDR(RPH);

                }
            }

            if (msitem.Mi_is_ser1 == 1)// (msitem.Mi_is_ser1 != -1)
            {
                int rowCount = 0;
                {
                    {
                        //-------------
                        string binCode = Session["GlbDefaultBin"].ToString();
                        binCode = null;
                        ReptPickSerials _reptPickSerial_ = _base.CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, MainItemCode, serialID);
                        //Update_inrser_INS_AVAILABLE
                        Boolean update_inr_ser = false;
                        if (_isWriteToTemporaryTable) update_inr_ser = _base.CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), MainItemCode, serialID, -1);

                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                        _reptPickSerial_.Tus_base_itm_line = _itemLineNo;
                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                        _reptPickSerial_.Tus_job_no = string.Empty;
                        _reptPickSerial_.Tus_pgs_prefix = string.Empty;
                        _reptPickSerial_.Tus_job_line = 0;
                        _reptPickSerial_.Tus_bin_to = ToBinCode;
                        _reptPickSerial_.Tus_com=Session["UserCompanyCode"].ToString();
                        if (isResQTY == true)
                        {
                            _reptPickSerial_.Tus_resqty = 1;
                        }
                        //enter row into TEMP_PICK_SER
                        Int32 affected_rows = -1;

                        if (_isWriteToTemporaryTable) affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                    }
                }
            }
            else
            {
                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_base_doc_no = ScanDocument;
                _reptPickSerial_.Tus_base_itm_line = _itemLineNo;
                _reptPickSerial_.Tus_itm_line = _itemLineNo;
                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                _reptPickSerial_.Tus_itm_brand = msitem.Mi_brand;
                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                _reptPickSerial_.Tus_bin = BinCode;
                _reptPickSerial_.Tus_itm_cd = MainItemCode;
                _reptPickSerial_.Tus_itm_stus = ItemStatus;
                _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtQty.Text.ToString());
                _reptPickSerial_.Tus_ser_1 = "N/A";
                _reptPickSerial_.Tus_ser_2 = "N/A";
                _reptPickSerial_.Tus_ser_3 = "N/A";
                _reptPickSerial_.Tus_ser_4 = "N/A";
                _reptPickSerial_.Tus_ser_id = 0;
                _reptPickSerial_.Tus_serial_id = "0";
                _reptPickSerial_.Tus_job_no = string.Empty;
                _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                _reptPickSerial_.Tus_job_line = 0;
                _reptPickSerial_.Tus_bin_to = ToBinCode;
                if (isResQTY == true)
                {
                    _reptPickSerial_.Tus_resqty = 1;
                }
                Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
            }

            List<ReptPickSerials> _serList1 = new List<ReptPickSerials>();
            if (user_seq_num == -1)
            {
                user_seq_num = generated_seq;
            }
            _serList1 = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, _docTp);
            if (oMasterItemStatuss != null)
            {
                if (_serList1 != null)
                {
                    foreach (ReptPickSerials _ser in _serList1)
                    {
                        MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == _ser.Tus_itm_stus);
                        MasterItem _masterItem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _ser.Tus_itm_cd);
                        if (oStatus != null)
                        {
                            _ser.Tus_itm_stus_Desc = oStatus.Mis_desc;

                        }
                        else
                        {
                            _ser.Tus_itm_stus_Desc = _ser.Tus_itm_stus;
                        }
                    }
                }
            }

            ((GridView)this.Parent.FindControl("dvDOSerials")).DataSource = _serList1;
            ((GridView)this.Parent.FindControl("dvDOSerials")).DataBind();

            if (_serList != null)
            {
                if (_serList1 != null)
                {
                    var _scanItems = _serList1.GroupBy(x => new { x.Tus_itm_cd, x.Tus_base_itm_line }).Select(group => new { Peo = group.Key, theCount = group.Sum(o => o.Tus_qty) });
                    foreach (var itm in _scanItems)
                    {
                        foreach (InvoiceItem _invItem in invoice_items)
                        {
                            if ((itm.Peo.Tus_itm_cd == _invItem.Sad_itm_cd || itm.Peo.Tus_itm_cd == _invItem.Sad_sim_itm_cd) && itm.Peo.Tus_base_itm_line == _invItem.Sad_itm_line)
                            {

                                _invItem.Sad_srn_qty = itm.theCount; // Current scan qty

                            }

                        }
                    }
                }
                ((GridView)this.Parent.FindControl("dvDOItems")).DataSource = invoice_items;
                ((GridView)this.Parent.FindControl("dvDOItems")).DataBind();
                Session["tempDoItemList"] = invoice_items;

            }
        }


        private void AddSerials_IS(out string msg)
        {
            msg = string.Empty;

            ViewState["ISITEM"] = Session["ISITEM"];
            DataTable invoice_items = new DataTable();
            invoice_items = ViewState["ISITEM"] as DataTable;
            List<ReptPickSerials> _serList = new List<ReptPickSerials>();
            _serList = ViewState["ISSerials"] as List<ReptPickSerials>;
            if (ViewState["ISITEM"] == null)
            {
                string _msg = "Please select the document again";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);

                return;
            }
            if (invoice_items == null)
            {
                string _msg = "Please select the pending request";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);

                return;
            }
            if (_serList == null)
            {
                _serList = new List<ReptPickSerials>();
            }
            MasterItem msitem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.ToUpper().Trim());
            string MainItemCode = msitem.Mi_cd;
            string _invoiceNo = string.Empty;
            string ItemStatus = string.Empty;
            // string ScanDocument = ViewState["ScanDocument"].ToString();
            ItemStatus = ddlStatus.SelectedValue;
            bool _isWriteToTemporaryTable = true;
            Int32 user_seq_num = 0;
            user_seq_num = _base.CHNLSVC.Inventory.GET_SEQNUM_FOR_INVOICE("IS", Session["UserCompanyCode"].ToString(), ViewState["userSeqNo"].ToString(), 0);
            if (user_seq_num != -1)//check whether Tuh_doc_no exists in temp_pick_hdr
            {

            }
            else
            {
                GenerateNewUserSeqNo();
                user_seq_num = Convert.ToInt32(ViewState["userSeqNo"].ToString());
            }

            DataView dv = new DataView(invoice_items);
            dv.RowFilter = "itri_itm_cd ='" + txtItemCode.Text.ToUpper() + "'";

            DataView dv2 = new DataView(invoice_items);
            dv2.RowFilter = "itri_itm_cd <> '" + txtItemCode.Text.ToUpper() + "'";

            DataTable _FilterSelectItem = new DataTable();
            DataTable _UnSelectItem = new DataTable();
            _FilterSelectItem = dv.ToTable();
            _UnSelectItem = dv2.ToTable();
            ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
            if ((_FilterSelectItem != null) && (_FilterSelectItem.Rows.Count > 0))
            {
                foreach (DataRow dr in _FilterSelectItem.Rows)
                {
                    if (txtItemCode.Text.ToUpper() == dr["itri_itm_cd"].ToString())
                    {
                        if (msitem.Mi_is_ser1 == 1)
                        {

                            decimal _pickqty = Convert.ToDecimal(dr["itri_mqty"]);
                            decimal _Balanqty_s = Convert.ToDecimal(dr["itri_bqty"].ToString());

                            if (_Balanqty_s > _pickqty)
                            {

                                dr["itri_mqty"] = _pickqty + 1; //itm.theCount;//itm.Peo.Tus_qty; // Current scan qty    
                                _UnSelectItem.Merge(_FilterSelectItem);
                                ((GridView)this.Parent.FindControl("grdItems")).DataSource = _UnSelectItem;
                                ((GridView)this.Parent.FindControl("grdItems")).DataBind();
                                Session["ISITEM"] = _UnSelectItem;
                                #region serial
                                Int32 serialID = 0;

                                List<ReptPickSerials> Tempserial_list = _base.CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), string.Empty, string.Empty);
                                Tempserial_list = Tempserial_list.Where(x => x.Tus_ser_1 == txtSerialI.Text.Trim()).ToList();
                                if (Tempserial_list != null && Tempserial_list.Count == 1)
                                {
                                    serialID = Tempserial_list[0].Tus_ser_id;
                                }
                                else
                                {
                                    string _msg = "Cannot find the serial";
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);
                                    return;
                                }

                                int rowCount = 0;
                                {
                                    {
                                        //-------------
                                        string binCode = Session["GlbDefaultBin"].ToString();
                                        binCode = null;
                                        _reptPickSerial_ = _base.CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, MainItemCode, serialID);
                                        //Update_inrser_INS_AVAILABLE
                                        Boolean update_inr_ser = false;
                                        if (_isWriteToTemporaryTable) update_inr_ser = _base.CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), MainItemCode, serialID, -1);

                                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                        _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                        _reptPickSerial_.Tus_base_doc_no = dr["itr_req_no"].ToString();
                                        foreach (DataRow row in _FilterSelectItem.Rows)
                                        {
                                            if (_reptPickSerial_.Tus_itm_cd == row["itri_itm_cd"].ToString())
                                            {
                                                string issuesvalue = row["itri_issue_qty"].ToString();
                                                if (issuesvalue == "")
                                                {
                                                    _reptPickSerial_.Tus_isqty = 0;
                                                }
                                                else
                                                {
                                                    _reptPickSerial_.Tus_isqty = Convert.ToDecimal(row["itri_issue_qty"].ToString());
                                                }


                                                _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(row["itri_line_no"].ToString());
                                            }
                                        }
                                        //_reptPickSerial_.Tus_base_itm_line = _itemLineNo;

                                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                        _reptPickSerial_.Tus_itm_brand = lblBrand.Text;
                                        _reptPickSerial_.Tus_job_no = string.Empty;
                                        _reptPickSerial_.Tus_pgs_prefix = string.Empty;
                                        _reptPickSerial_.Tus_job_line = 0;
                                        _reptPickSerial_.Tus_itm_stus = ddlStatus.SelectedValue;
                                        _reptPickSerial_.Tus_bin_to = ToBinCode;
                                        //enter row into TEMP_PICK_SER
                                        Int32 affected_rows = -1;

                                        if (_isWriteToTemporaryTable) affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                    }
                                }
                                #endregion
                                List<ReptPickSerials> _serListFilter = new List<ReptPickSerials>();
                                _serListFilter = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "IS");

                                ((GridView)this.Parent.FindControl("grdSerial")).DataSource = _serListFilter;
                                ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
                                ViewState["ISSerials"] = _serListFilter;
                                string Msg = txtItemCode.Text.ToUpper() + " item added successfully !!!";
                                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
                                PageClear();

                                return;
                            }
                            else
                            {
                                //'" + txtItemCode.Text + "' issue completed
                                string _msg = "There is no balance available for the selected items";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);
                                PageClear();

                                return;
                            }
                        }
                        else
                        {
                            decimal _pickqty = Convert.ToDecimal(dr["itri_mqty"]);
                            decimal _Balanqty_s = Convert.ToDecimal(dr["itri_bqty"].ToString());

                            if (_Balanqty_s > _pickqty)
                            {
                                decimal newqty = Convert.ToDecimal(txtQty.Text);
                                decimal newpick = newqty + _pickqty;
                                dr["itri_mqty"] = newpick; //itm.theCount;//itm.Peo.Tus_qty; // Current scan qty    
                                _UnSelectItem.Merge(_FilterSelectItem);

                                #region Non-Serial
                                List<InventoryLocation> _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), msitem.Mi_cd, ItemStatus);
                                if (_inventoryLocation != null)
                                {
                                    if (_inventoryLocation.Count == 1)
                                    {
                                        foreach (InventoryLocation _loc in _inventoryLocation)
                                        {
                                            decimal _formQty = Convert.ToDecimal(txtQty.Text);
                                            if (_formQty > _loc.Inl_free_qty)
                                            {
                                                string _msg = "Please check the inventory balance!";
                                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);

                                                txtQty.Text = string.Empty;
                                                txtQty.Focus();
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string _msg = "Please check the inventory balance!";
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);
                                        return;
                                    }
                                }
                                else
                                {
                                    string _msg = "Please check the inventory balance!";
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);

                                    return;
                                }

                                _reptPickSerial_.Tus_itm_cd = MainItemCode;
                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                _reptPickSerial_.Tus_base_doc_no = dr["itr_req_no"].ToString();
                                foreach (DataRow row2 in invoice_items.Rows)
                                {
                                    if (_reptPickSerial_.Tus_itm_cd == row2["itri_itm_cd"].ToString())
                                    {
                                        decimal _NewQty = 0;
                                        if (row2["itri_issue_qty"].ToString() == "")
                                        {
                                            _NewQty = 0;
                                            _reptPickSerial_.Tus_isqty = _NewQty;
                                        }
                                        else
                                        {
                                            _reptPickSerial_.Tus_isqty = Convert.ToInt32(row2["itri_issue_qty"].ToString());
                                        }

                                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(row2["itri_line_no"].ToString());
                                    }
                                }
                                //_reptPickSerial_.Tus_base_itm_line = _itemLineNo;
                                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                                _reptPickSerial_.Tus_itm_brand = lblBrand.Text;
                                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();

                                _reptPickSerial_.Tus_itm_stus = ddlStatus.SelectedValue;
                                _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtQty.Text.ToString());
                                _reptPickSerial_.Tus_ser_1 = "N/A";
                                _reptPickSerial_.Tus_ser_2 = "N/A";
                                _reptPickSerial_.Tus_ser_3 = "N/A";
                                _reptPickSerial_.Tus_ser_4 = "N/A";
                                _reptPickSerial_.Tus_ser_id = 0;
                                _reptPickSerial_.Tus_serial_id = "0";
                                _reptPickSerial_.Tus_job_no = string.Empty;
                                _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                                _reptPickSerial_.Tus_job_line = 0;
                                _reptPickSerial_.Tus_bin_to = ToBinCode;
                                Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                                #endregion
                                ((GridView)this.Parent.FindControl("grdItems")).DataSource = _UnSelectItem;
                                ((GridView)this.Parent.FindControl("grdItems")).DataBind();
                                Session["ISITEM"] = _UnSelectItem;

                                List<ReptPickSerials> _serListFilter = new List<ReptPickSerials>();
                                _serListFilter = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "IS");

                                ((GridView)this.Parent.FindControl("grdSerial")).DataSource = _serListFilter;
                                ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
                                ViewState["ISSerials"] = _serListFilter;
                                string Msg = txtItemCode.Text.ToUpper() + " item added successfully !!!";
                                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
                                PageClear();
                                return;
                            }
                            else
                            {
                                string _msg = "There is no balance available for the Selected item  ";
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);
                            }
                        }

                    }
                }
            }
            else
            {
                if (msitem.Mi_is_ser1 == 1)
                {
                    #region serial
                    Int32 serialID = 0;

                    List<ReptPickSerials> Tempserial_list = _base.CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), string.Empty, string.Empty);
                    Tempserial_list = Tempserial_list.Where(x => x.Tus_ser_1 == txtSerialI.Text.Trim()).ToList();
                    if (Tempserial_list != null && Tempserial_list.Count == 1)
                    {
                        serialID = Tempserial_list[0].Tus_ser_id;
                    }
                    else
                    {
                        string _msg = "Cannot find the serial";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);
                        return;
                    }

                    int rowCount = 0;
                    {
                        {
                            //-------------
                            string binCode = Session["GlbDefaultBin"].ToString();
                            binCode = null;
                            _reptPickSerial_ = _base.CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, MainItemCode, serialID);
                            //Update_inrser_INS_AVAILABLE
                            Boolean update_inr_ser = false;
                            if (_isWriteToTemporaryTable) update_inr_ser = _base.CHNLSVC.Inventory.Update_serialID_INS_AVAILABLE(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), MainItemCode, serialID, -1);

                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_base_doc_no = Session["_Reqno"].ToString();

                            _reptPickSerial_.Tus_base_itm_line = 0;

                            _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                            _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                            _reptPickSerial_.Tus_itm_brand = lblBrand.Text;
                            _reptPickSerial_.Tus_job_no = string.Empty;
                            _reptPickSerial_.Tus_pgs_prefix = string.Empty;
                            _reptPickSerial_.Tus_job_line = 0;
                            _reptPickSerial_.Tus_bin_to = ToBinCode;
                            //enter row into TEMP_PICK_SER
                            Int32 affected_rows = -1;

                            if (_isWriteToTemporaryTable) affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                        }
                    }
                    #endregion
                    DataRow dr = null;
                    dr = _UnSelectItem.NewRow();
                    dr["ITRI_ITM_CD"] = txtItemCode.Text.ToUpper();
                    dr["MI_LONGDESC"] = lblDescription.Text;
                    dr["MI_MODEL"] = lblModel.Text;
                    dr["MI_PART_NO"] = lblPart.Text;
                    dr["ITRI_MQTY"] = Convert.ToDecimal(txtQty.Text);
                    dr["ITRI_QTY"] = 0.0;
                    dr["ITRI_APP_QTY"] = 0.0;
                    dr["ITRI_QTY"] = 0.0;
                    dr["ITRI_PO_QTY"] = 0.0;
                    dr["ITRI_BQTY"] = 0.0;
                    if (Session["_Reqno"] == "")
                    {
                        string MsgReq = "Please select request number";
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickySuccessToast('" + MsgReq + "');", true);
                    }
                    else
                    {
                        dr["ITR_REQ_NO"] = Session["_Reqno"].ToString();
                    }

                    _UnSelectItem.Rows.Add(dr);
                    ((GridView)this.Parent.FindControl("grdItems")).DataSource = _UnSelectItem;
                    ((GridView)this.Parent.FindControl("grdItems")).DataBind();
                    Session["ISITEM"] = _UnSelectItem;

                    List<ReptPickSerials> _serListFilter = new List<ReptPickSerials>();
                    _serListFilter = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "IS");

                    ((GridView)this.Parent.FindControl("grdSerial")).DataSource = _serListFilter;
                    ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
                    ViewState["ISSerials"] = _serListFilter;
                    string Msg = txtItemCode.Text.ToUpper() + " item added successfully !!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
                    PageClear();

                    return;
                }
                else
                {
                    #region Non-Serial
                    //List<ReptPickSerials> _CheckserList = new List<ReptPickSerials>();
                    //_CheckserList = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "IS");
                    //if (_CheckserList != null)
                    //{
                    //    var _CheckItem = _CheckserList.Where(x => x.Tus_itm_cd == txtItemCode.Text).ToList();
                    //    if (_CheckItem.Count!=0)
                    //    {
                    //        string _msg = "already  added this item.";
                    //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);
                    //        return;
                    //    }
                    //}
                    List<InventoryLocation> _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), msitem.Mi_cd, ItemStatus);
                    if (_inventoryLocation != null)
                    {
                        if (_inventoryLocation.Count == 1)
                        {
                            foreach (InventoryLocation _loc in _inventoryLocation)
                            {
                                decimal _formQty = Convert.ToDecimal(txtQty.Text);
                                if (_formQty > _loc.Inl_free_qty)
                                {
                                    string _msg = "Please check the inventory balance!";
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);

                                    txtQty.Text = string.Empty;
                                    txtQty.Focus();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            string _msg = "Please check the inventory balance!";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);
                            return;
                        }
                    }
                    else
                    {
                        string _msg = "Please check the inventory balance!";
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "');", true);

                        return;
                    }

                    _reptPickSerial_.Tus_itm_cd = MainItemCode;
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    _reptPickSerial_.Tus_usrseq_no = user_seq_num;
                    _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                    //_reptPickSerial_.Tus_base_doc_no = "";
                    _reptPickSerial_.Tus_base_doc_no = Session["_Reqno"].ToString();
                    _reptPickSerial_.Tus_base_itm_line = 0;
                    _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                    _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                    _reptPickSerial_.Tus_itm_brand = lblBrand.Text;
                    _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                    _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                    _reptPickSerial_.Tus_bin = Session["GlbDefaultBin"].ToString();

                    _reptPickSerial_.Tus_itm_stus = ddlStatus.SelectedValue;
                    _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtQty.Text.ToString());
                    _reptPickSerial_.Tus_ser_1 = "N/A";
                    _reptPickSerial_.Tus_ser_2 = "N/A";
                    _reptPickSerial_.Tus_ser_3 = "N/A";
                    _reptPickSerial_.Tus_ser_4 = "N/A";
                    _reptPickSerial_.Tus_ser_id = 0;
                    _reptPickSerial_.Tus_serial_id = "0";
                    _reptPickSerial_.Tus_job_no = string.Empty;
                    _reptPickSerial_.Tus_pgs_prefix = MainItemCode;
                    _reptPickSerial_.Tus_job_line = 0;
                    _reptPickSerial_.Tus_bin_to = ToBinCode;
                    Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
                    #endregion
                    DataRow dr = null;
                    dr = _UnSelectItem.NewRow();
                    dr["ITRI_ITM_CD"] = txtItemCode.Text.ToUpper();
                    dr["MI_LONGDESC"] = lblDescription.Text;
                    dr["MI_MODEL"] = lblModel.Text;
                    // dr["TUS_ITM_BRAND"] = lblModel.Text;
                    dr["MI_PART_NO"] = lblPart.Text;
                    dr["ITRI_MQTY"] = Convert.ToDecimal(txtQty.Text);
                    dr["ITRI_QTY"] = 0.0;
                    dr["ITRI_APP_QTY"] = 0.0;
                    dr["ITRI_QTY"] = 0.0;
                    dr["ITRI_PO_QTY"] = 0.0;
                    dr["ITRI_BQTY"] = 0.0;
                    if (Session["_Reqno"] == "")
                    {
                        string MsgReq = "Please select request number";
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickySuccessToast('" + MsgReq + "');", true);
                    }
                    else
                    {
                        dr["ITR_REQ_NO"] = Session["_Reqno"].ToString();
                    }
                    _UnSelectItem.Rows.Add(dr);
                    ((GridView)this.Parent.FindControl("grdItems")).DataSource = _UnSelectItem;
                    ((GridView)this.Parent.FindControl("grdItems")).DataBind();
                    Session["ISITEM"] = _UnSelectItem;

                    List<ReptPickSerials> _serListFilter = new List<ReptPickSerials>();
                    _serListFilter = _base.CHNLSVC.Inventory.GetAllScanSerialsList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserID"].ToString(), user_seq_num, "IS");

                    ((GridView)this.Parent.FindControl("grdSerial")).DataSource = _serListFilter;
                    ((GridView)this.Parent.FindControl("grdSerial")).DataBind();
                    ViewState["ISSerials"] = _serListFilter;



                    string Msg = msitem.Mi_cd + " item added successfully !!!";
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickySuccessToast('" + Msg + "');", true);
                    PageClear();

                    return;
                }
            }


        }

        public void ItemCodeChange()
        {
            txtItemCode_TextChanged(null, null);
        }

        private void DisplayMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msg + "')", true);
        }

        private string DocumentTypeDecider(Int32 _serialID)
        {
            InventorySerialMaster _master = _base.CHNLSVC.Inventory.GetSerialMasterDetailBySerialID(_serialID);
            string _userCompany = Session["UserCompanyCode"].ToString();//ABL
            DropDownList ddlRecCompany = (DropDownList)this.Parent.FindControl("ddlRecCompany");
            string _selectCompany = ddlRecCompany.Text.ToString();//AAL
            string _itemReceiveCompany = _master.Irsm_rec_com;//AAL
            string _comOutDocType = "NON";
            if (_userCompany == _selectCompany)
                _comOutDocType = "AOD-OUT";
            else if (_selectCompany == _itemReceiveCompany)
                _comOutDocType = "PRN";
            else if (_selectCompany != _itemReceiveCompany)
                _comOutDocType = "DO";
            if (_master.Irsm_itm_stus == "CONS") _comOutDocType = "AOD-OUT"; //Add by Chamal 06-May-2014
            return _comOutDocType;
        }

        private void addNonSerializeDirectOut()
        {
            try
            {
                Int32 userSeqNoNew = (Session["UserSeqNo"] != null) ? Convert.ToInt32(Session["UserSeqNo"]) : Convert.ToInt32(userSeqNo);

                MasterItem _itm = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.ToUpper());
                StringBuilder _errorserial = new StringBuilder();
                StringBuilder _pickedserial = new StringBuilder();

                Int32 _maxline = (Session["_maxline"] != null) ? Convert.ToInt32(Session["_maxline"]) : 1;

                List<ReptPickSerials> oSelectedSerialList;
                if (Session["ScanItemListNew"] == null)
                {
                    oSelectedSerialList = new List<ReptPickSerials>();
                }
                else
                {
                    oSelectedSerialList = (List<ReptPickSerials>)Session["ScanItemListNew"];
                }
                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_usrseq_no = userSeqNoNew;
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_doc_dt = DateTime.Now.Date;
                _reptPickSerial_.Tus_base_itm_line = 0;
                _reptPickSerial_.Tus_itm_desc = _itm.Mi_shortdesc;
                _reptPickSerial_.Tus_itm_model = _itm.Mi_model;
                _reptPickSerial_.Tus_new_remarks = "AOD-OUT";
                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                _reptPickSerial_.Tus_bin = ddlBinCode.SelectedValue.ToString();
                _reptPickSerial_.Tus_itm_cd = txtItemCode.Text.ToUpper();
                _reptPickSerial_.Tus_itm_stus = ddlStatus.SelectedValue.ToString();
                _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtQty.Text);
                _reptPickSerial_.Tus_ser_id = 0;
                _reptPickSerial_.Tus_itm_desc = lblDescription.Text;
                _reptPickSerial_.Tus_itm_model = lblModel.Text;
                _reptPickSerial_.Tus_itm_brand = lblBrand.Text;
                _reptPickSerial_.Tus_base_doc_no = userSeqNoNew.ToString();
                _reptPickSerial_.Tus_base_itm_line = _maxline;
                _reptPickSerial_.Tus_ageloc = Session["UserDefLoca"].ToString();
                _reptPickSerial_.Tus_bin_to = ToBinCode;

                oSelectedSerialList.Add(_reptPickSerial_);
                Int32 affected_rows = _base.CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);

                ((GridView)this.Parent.FindControl("gvSerial")).DataSource = oSelectedSerialList;
                ((GridView)this.Parent.FindControl("gvSerial")).DataBind();

                Session["ScanItemListNew"] = oSelectedSerialList;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Session["SP"] = null;
            txtSearchbyword.Text = "";
            UserPopup.Hide();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsBinTransfer)
            {
                if (!_base.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10114))
                {
                    ddlStatusToBeChange.SelectedValue = ddlStatus.SelectedValue;
                    ddlStatusToBeChange.Enabled = false;
                }
                else
                {
                    ddlStatusToBeChange.Enabled = true;
                }
            }


            foreach (GridViewRow row1 in grdInventoryBalance.Rows)
            {
                row1.BackColor = System.Drawing.Color.White;
            }

            foreach (GridViewRow row in grdInventoryBalance.Rows)
            {
                for (int i = 0; i < grdInventoryBalance.Columns.Count; i++)
                {
                    String header = grdInventoryBalance.Columns[i].HeaderText;
                    String cellText = row.Cells[i].Text;

                    if (cellText == ddlStatus.SelectedItem.Text)
                    {
                        row.BackColor = System.Drawing.Color.LightBlue;
                    }

                }
                //string _curSch = row.DataItem,"MIS_DESC";
                //if (_curSch == ddlStatus.SelectedItem.Text)
                //{
                //    row.BackColor = System.Drawing.Color.LightBlue;
                //}

            }
            //  DrugDetailGridView_RowDataBound(System.Web.UI.WebControls.GridView, System.Web.UI.WebControls.GridViewRowEventArgs);
        }
        protected void DrugDetailGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // To check condition on string value 
                //Note: "Drug" is my data Column Name  and "Hydralazine" is value to be match
                if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "MIS_DESC")) == ddlStatus.SelectedItem.Text)
                {
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                }
                //else
                //{
                //    // Whatever you want to do.......
                //    // e.Row.ForeColor = System.Drawing.Color.Yellow;
                //}

                //// To check condition on integer value
                //if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "Dosage")) == 50)
                //{
                //    e.Row.BackColor = System.Drawing.Color.Cyan;
                //}
            }
        }

        private void loadItemStatus()
        {
            List<MasterItemStatus> oMasterItemStatuss = _base.CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            Session["ItemStatus"] = oMasterItemStatuss;
        }

        public void loadBinCode()
        {
            if (IsBinTransfer)
            {
                if (!string.IsNullOrEmpty(BinCode))
                {
                    DataTable bins = _base.CHNLSVC.Inventory.LoadDistinctBins(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim());
                   
                    if (bins.Rows.Count > 0)
                    {
                        bool has=false;
                        foreach (DataRow rw in bins.Rows){
                            if (BinCode == rw["INB_BIN"].ToString() && BinCode != "")
                            {
                                has = true;
                            }
                        }
                        if (has==true)
                        {
                        
                            bins.Clear();
                            DataRow dr = bins.NewRow();
                            dr["INB_BIN"] = BinCode;
                            bins.Rows.Add(dr);

                            ddlBinCode.DataSource = null;
                            ddlBinCode.DataSource = bins;
                            ddlBinCode.DataTextField = "INB_BIN";
                            ddlBinCode.DataValueField = "INB_BIN";
                            ddlBinCode.DataBind();

                            ddlBinCode_PDA.DataSource = null;
                            ddlBinCode_PDA.DataSource = bins;
                            ddlBinCode_PDA.DataTextField = "INB_BIN";
                            ddlBinCode_PDA.DataValueField = "INB_BIN";
                            ddlBinCode_PDA.DataBind();
                        }
                        else
                        {
                            PageClear();
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript2", "showStickyErrorToast('Selected item bin is invalid !!!')", true);
                        }
                       
                    }
                }

                if (!_base.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10114))
                {
                    ddlStatusToBeChange.SelectedValue = ddlStatus.SelectedValue;
                    ddlStatusToBeChange.Enabled = false;
                }
                else
                {
                    ddlStatusToBeChange.Enabled = true;
                }
            }
        }

        private string getItemStatusDesc(string stis)
        {
            List<MasterItemStatus> oStatuss = (List<MasterItemStatus>)Session["ItemStatus"];
            if (oStatuss.FindAll(x => x.Mis_cd == stis).Count > 0)
            {
                stis = oStatuss.Find(x => x.Mis_cd == stis).Mis_desc;
            }
            return stis;
        }

        protected void gvitems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["EXCELPOPUP"] = null;

                string seril = (string)Session["SELECTED_SERIAL_CHSTO"];

                string itmstus = (dgvItem.SelectedRow.FindControl("lblstuscode") as Label).Text;

                ddlStatus.SelectedValue = itmstus;
                ddlStatusToBeChange.SelectedValue = itmstus;

                txtSerialI.Text = seril;
                txtSerialI_TextChangedDuplicate();

                mpexcel.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            finally
            {
                _base.CHNLSVC.CloseChannel();
            }
        }

        protected void LinkButton8_Click(object sender, EventArgs e)
        {
            Session["EXCELPOPUP"] = null;
            mpexcel.Hide();
        }

        protected void txtSerialI_TextChangedDuplicate()
        {
            try
            {
                string itemCode = txtItemCode.Text.ToUpper();
                DataTable dtserials = _base.CHNLSVC.Inventory.GET_INR_SER(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), null, itemCode, ddlStatus.SelectedValue, txtSerialI.Text.Trim());

                if (dtserials.Rows.Count <= 0)
                {
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid serial number...!')", true);
                    DisplayMessage("This serial number is not available! Please check again.!");
                    txtSerialI.Text = string.Empty;
                    txtSerialI.Focus();
                    return;
                }

                if (IsBinTransfer)
                {
                    if (string.IsNullOrEmpty(BinCode))
                    {
                        txtItemCode.Text = "";
                        txtItemToBeChange.Text = "";
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Select from BIN code.');", true);
                        return;
                    }
                    else
                    {
                        if (dtserials.Rows[0]["INS_BIN"].ToString() != BinCode)
                        {
                            txtSerialI.Text = "";
                            txtSerialII.Text = "";
                            txtSerialIII.Text = "";
                            txtItemCode.Text = "";
                            txtItemToBeChange.Text = "";
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Select a serial from selected BIN.');", true);
                            return;
                        }
                    }
                }

                ViewState["SerialID"] = dtserials.Rows[0]["INS_SER_ID"].ToString();

                if (chkDirectScan.Checked == false)
                {
                    txtItemCode.Text = dtserials.Rows[0]["INS_ITM_CD"].ToString();
                    txtItemCode.ReadOnly = true;
                    txtSerialI.Text = dtserials.Rows[0]["INS_SER_1"].ToString();
                    txtSerialII.Text = dtserials.Rows[0]["INS_SER_2"].ToString();
                    txtSerialIII.Text = dtserials.Rows[0]["INS_SER_3"].ToString();
                    // ddlStatus.SelectedValue = dtserials.Rows[0]["INS_ITM_STUS"].ToString();
                    ddlBinCode.DataSource = dtserials;
                    ddlBinCode.DataTextField = "INS_BIN";
                    ddlBinCode.DataValueField = "INS_BIN";
                    ddlBinCode.DataBind();
                    ddlStatus.DataSource = dtserials;
                    ddlStatus.DataTextField = "mis_desc";
                    ddlStatus.DataValueField = "INS_ITM_STUS";
                    ddlStatus.DataBind();
                    if (string.IsNullOrEmpty(txtItemToBeChange.Text))
                    {
                        txtItemToBeChange.Text = txtItemCode.Text.ToUpper();
                    }
                    txtSerialI.ReadOnly = true;
                    txtSerialII.ReadOnly = true;
                    txtSerialIII.ReadOnly = true;
                    txtQty.Text = "1";
                    txtQty.ReadOnly = true;

                    ddlStatusToBeChange.SelectedValue = dtserials.Rows[0]["INS_ITM_STUS"].ToString();
                }
                else
                {
                    txtItemCode.Text = dtserials.Rows[0]["INS_ITM_CD"].ToString();
                    txtItemCode.ReadOnly = true;
                    txtSerialI.Text = dtserials.Rows[0]["INS_SER_1"].ToString();
                    txtSerialII.Text = dtserials.Rows[0]["INS_SER_2"].ToString();
                    txtSerialIII.Text = dtserials.Rows[0]["INS_SER_3"].ToString();
                    //ddlStatus.SelectedValue = dtserials.Rows[0]["INS_ITM_STUS"].ToString();
                    txtSerialI.ReadOnly = true;
                    txtSerialII.ReadOnly = true;
                    txtSerialIII.ReadOnly = true;

                    txtItemCode.Text = dtserials.Rows[0]["INS_ITM_CD"].ToString();
                    if (string.IsNullOrEmpty(txtItemToBeChange.Text))
                    {
                        txtItemToBeChange.Text = txtItemCode.Text.ToUpper();
                    }
                    ddlBinCode.DataSource = dtserials;
                    ddlBinCode.DataTextField = "INS_BIN";
                    ddlBinCode.DataValueField = "INS_BIN";
                    ddlBinCode.DataBind();
                    ddlStatus.DataSource = dtserials;
                    ddlStatus.DataTextField = "mis_desc";
                    ddlStatus.DataValueField = "INS_ITM_STUS";
                    ddlStatus.DataBind();

                    txtQty.Text = "1";
                    txtQty.ReadOnly = true;
                }

                if (!string.IsNullOrEmpty(txtItemCode.Text.ToUpper()))
                {
                    _itemdetail = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.ToUpper());
                }
                if (_itemdetail != null)
                {
                    if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                    {
                        if (_itemdetail.Mi_itm_tp == "V")
                        {
                            PageClear();
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Virtual item not allowed.');", true);
                            return;
                        }

                        _chargeType = _itemdetail.Mi_chg_tp;
                        string _description = _itemdetail.Mi_longdesc;
                        string _model = _itemdetail.Mi_model;
                        string _brand = _itemdetail.Mi_brand;
                        string _partNo = _itemdetail.Mi_part_no;
                        string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Serialized" : "Non-Serialized";

                        if (_itemdetail.Mi_is_ser1 != 1)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Non-Serialized item.');", true);
                            return;
                        }

                        lblDescription.Text = _description;
                        lblModel.Text = _model;
                        lblBrand.Text = _brand;
                        lblPart.Text = _partNo;
                        //lblItemSerialStatus.Text = "Serial Status : " + _serialstatus;
                        _isDecimalAllow = _base.CHNLSVC.Inventory.IsUOMDecimalAllow(txtItemCode.Text.ToUpper());
                        if (_itemdetail.MI_IS_EXP_DT == 1)
                        {
                            divExpiryDate.Visible = true;
                            //dtExp.Visible = true;
                        }
                        else
                        {
                            divExpiryDate.Visible = false;
                            //dtExp.Visible = false;
                        }
                    }
                }
                else
                {
                    txtItemCode.Text = string.Empty;
                    lblDescription.Text = string.Empty;
                    lblModel.Text = string.Empty;
                    lblBrand.Text = string.Empty;
                    //lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
                    //txtUnitCost.Text = string.Empty;
                    txtQty.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Invalid item code.');", true);
                }

                DataTable _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), string.Empty);
                grdInventoryBalance.DataSource = null;
                grdInventoryBalance.AutoGenerateColumns = false;
                grdInventoryBalance.DataSource = _inventoryLocation;
                grdInventoryBalance.DataBind();

                if (IsBinTransfer)
                {
                    DataTable dt = _base.CHNLSVC.Inventory.GetItemBalanceForBIN(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), string.Empty, BinCode);
                    grdInventoryBalance.DataSource = null;
                    grdInventoryBalance.AutoGenerateColumns = false;
                    grdInventoryBalance.DataSource = dt;
                    grdInventoryBalance.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }


        protected void lbtnPDAItemcode_Click(object sender, EventArgs e)
        {
            try
            {
            
                Session["SP"] = "show";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = string.Empty;
                DataTable result = new DataTable();
                if (!IsBinTransfer)
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    result = _base.CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                    lblvalue.Text = "3_PDA";
                }
                else
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemWithBin);
                    result = _base.CHNLSVC.CommonSearch.GetItemSearchDataWithBIN(SearchParams, null, null);
                    lblvalue.Text = "ItemWithBin_PDA";
                }
                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                DateRow.Visible = false;
                UserPopup.Show();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        public void PDAITemLoad()
        {
            try
            {
                if (string.IsNullOrEmpty(txtPDAItemcode.Text))
                {
                    PageClear();
                    txtPDAItemcode.Focus();
                    return;
                }

                loadBinCode();

  
                if (LoadItemDetail(txtPDAItemcode.Text.ToUpper().Trim()) == false)
                {
                    txtPDAItemcode.Focus();
                    return;
                }

                txtPDAItemcode.Text = txtPDAItemcode.Text.ToUpper().Trim();

                if (string.IsNullOrEmpty(txtItemToBeChange.Text))
                {
                    txtItemToBeChange.Text = txtItemCode.Text.ToUpper().Trim();
                }

                if (ViewState["adjustmentTypeValue"] == null)
                {
                    txtPDAItemcode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please set the adjustment type!.');", true);
                    return;
                }
                if (ViewState["adjustmentTypeValue"].ToString() == "0")
                {
                    txtPDAItemcode.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the adjustment type!.');", true);
                    return;
                }

                DataTable _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtPDAItemcode.Text.ToUpper().Trim(), string.Empty);
                DataTable bins = _base.CHNLSVC.Inventory.LoadDistinctBins(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtPDAItemcode.Text.ToUpper().Trim());
                if (bins == null || bins.Rows.Count <= 0)
                {
                    PageClear();
                    txtPDAItemcode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('No stock balance available.');", true);
                    return;
                }
                else
                {
                    if (IsBinTransfer == false)
                    {
                        ddlBinCode.DataSource = null;
                        ddlBinCode.DataSource = bins;
                        ddlBinCode.DataTextField = "INB_BIN";
                        ddlBinCode.DataValueField = "INB_BIN";
                        ddlBinCode.DataBind();

                        ddlBinCode_PDA.DataSource = null;
                        ddlBinCode_PDA.DataSource = bins;
                        ddlBinCode_PDA.DataTextField = "INB_BIN";
                        ddlBinCode_PDA.DataValueField = "INB_BIN";
                        ddlBinCode_PDA.DataBind();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(BinCode))
                        {
                            txtPDAItemcode.Text = "";
                            txtItemToBeChange.Text = "";
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Select from BIN code.');", true);
                            return;
                        }

                        bins.Clear();
                        DataRow dr = bins.NewRow();
                        dr["INB_BIN"] = BinCode;
                        bins.Rows.Add(dr);

                        ddlBinCode.DataSource = null;
                        ddlBinCode.DataSource = bins;
                        ddlBinCode.DataTextField = "INB_BIN";
                        ddlBinCode.DataValueField = "INB_BIN";
                        ddlBinCode.DataBind();

                        ddlBinCode_PDA.DataSource = null;
                        ddlBinCode_PDA.DataSource = bins;
                        ddlBinCode_PDA.DataTextField = "INB_BIN";
                        ddlBinCode_PDA.DataValueField = "INB_BIN";
                        ddlBinCode_PDA.DataBind();
                    }
                }

                if (_inventoryLocation == null || _inventoryLocation.Rows.Count <= 0)
                {
                    PageClear();
                    txtPDAItemcode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('No stock balance available!.');", true);
                    return;
                }
                else
                {

                    ddlStatus.DataSource = null;
                    ddlStatus.DataSource = _inventoryLocation;
                    ddlStatus.DataTextField = "mis_desc";
                    ddlStatus.DataValueField = "inl_itm_stus";
                    ddlStatus.DataBind();

                    ddlStatus_PDA.DataSource = null;
                    ddlStatus_PDA.DataSource = _inventoryLocation;
                    ddlStatus_PDA.DataTextField = "mis_desc";
                    ddlStatus_PDA.DataValueField = "inl_itm_stus";
                    ddlStatus_PDA.DataBind();
                    if (IsBinTransfer)
                    {
                        if (!_base.CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10114))
                        {
                            ddlStatusToBeChange.SelectedValue = ddlStatus.SelectedValue;
                            ddlStatusToBeChange.Enabled = false;
                        }
                        else
                        {
                            ddlStatusToBeChange.Enabled = true;
                        }
                    }

                    if (pnlSerialized.Visible == true)
                    {
                   
                    }
                    else
                    {
                        txtPDAItemcode.ReadOnly = false;
                        txtQty_PDA.ReadOnly = false;
                        if (bins.Rows.Count == 1)
                        {
                            ddlStatus.Focus();
                            if (_inventoryLocation.Rows.Count == 1)
                                txtQty_PDA.Focus();
                            else
                                ddlStatus_PDA.Focus();
                        }
                        else
                            ddlBinCode_PDA.Focus();
                    }
                }

                grdInventoryBalance.DataSource = null;
                grdInventoryBalance.AutoGenerateColumns = false;
                grdInventoryBalance.DataSource = _inventoryLocation;
                grdInventoryBalance.DataBind();

                if (IsBinTransfer)
                {
                    DataTable dt = _base.CHNLSVC.Inventory.GetItemBalanceForBIN(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), string.Empty, BinCode);
                    grdInventoryBalance.DataSource = null;
                    grdInventoryBalance.AutoGenerateColumns = false;
                    grdInventoryBalance.DataSource = dt;
                    grdInventoryBalance.DataBind();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void ddlStatus_PDA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsBinTransfer)
            {
               
                ddlStatus_PDA.Enabled = true;
                
            }


            foreach (GridViewRow row1 in grdInventoryBalance.Rows)
            {
                row1.BackColor = System.Drawing.Color.White;
            }

            foreach (GridViewRow row in grdInventoryBalance.Rows)
            {
                for (int i = 0; i < grdInventoryBalance.Columns.Count; i++)
                {
                    String header = grdInventoryBalance.Columns[i].HeaderText;
                    String cellText = row.Cells[i].Text;

                    if (cellText == ddlStatus_PDA.SelectedItem.Text)
                    {
                        row.BackColor = System.Drawing.Color.LightBlue;
                    }

                }
                //string _curSch = row.DataItem,"MIS_DESC";
                //if (_curSch == ddlStatus.SelectedItem.Text)
                //{
                //    row.BackColor = System.Drawing.Color.LightBlue;
                //}

            }
            //  DrugDetailGridView_RowDataBound(System.Web.UI.WebControls.GridView, System.Web.UI.WebControls.GridViewRowEventArgs);
        }

        public void lbtnAdd_PDA_Click(object sender, EventArgs e)
        {
            try
            {
                string sessionpopupcrn = (string)Session["POPUP_CRN_RETURN"];
                string passedseq = (string)Session["GEN_SEQ"];

                if (doc_tp == "COM_OUT" && Session["AOD_DIRECT"] != null)
                {
                    if (Session["AOD_DIRECT"].ToString().ToUpper() == false.ToString().ToUpper())
                    {
                        DisplayMessage("Item adding is only allowed for direct out");
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtItemCode.Text))
                {
                    DisplayMessage("Please select the item code");
                    return;
                }
                MasterItem _itms = new MasterItem();
                _itms = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.ToUpper().Trim());



                if (string.IsNullOrEmpty(txtItemCode.Text.Trim()))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the item...!');", true);
                    txtItemCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(ddlStatus.Text.ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the status...!');", true);
                    ddlStatus.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtQty_PDA.Text))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please select the quantity...!');", true);
                    txtQty.Focus();
                    return;
                }





                AddItem_PDA(txtPDAItemcode.Text.ToUpper(), _itms.Mi_std_cost.ToString(), ddlStatus_PDA.SelectedItem.Value, txtQty_PDA.Text, "", txtSerialI.Text);






                string itmCOdeMsg = txtPDAItemcode.Text.ToUpper().Trim();

                Session["SELECTED_ITEM"] = txtPDAItemcode.Text.Trim();
                Session["SELECTED_ITEM_QTY"] = txtQty_PDA.Text.Trim();

                PageClear();

                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Item code : " + itmCOdeMsg + " added successfully.');", true);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void AddItem_PDA(string _item, string _UnitCost, string _status, string _qty, string _UserSeqNo, string _serial)
        {
            try
            {
                MasterItem _itms = new MasterItem();
                _itms = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                InventoryRequestItem _itm = new InventoryRequestItem();
                //  ScanItemList = ViewState["ScanItemList"] as List<InventoryRequestItem>;
                int itemline = 0;
                if (ScanItemList != null)
                {
                    if (ScanItemList.Count > 0)
                    {
                        var _duplicate = from _ls in ScanItemList
                                         where _ls.Itri_itm_cd == _item && _ls.Itri_itm_stus == _status && _ls.Itri_mitm_stus == ddlStatusToBeChange.Text
                                         select _ls;

                        if (IsBinTransfer)
                        {
                            _duplicate = from _ls in ScanItemList
                                         where _ls.Itri_itm_cd == _item && _ls.Itri_itm_stus == _status && _ls.Itri_note == ddlStatusToBeChange.Text
                                         select _ls;
                        }

                        if (_duplicate != null)
                            if (_duplicate.Count() > 0)
                            {
                                var line = from d in _duplicate
                                           select d.Itri_line_no;
                                itemline = (line).First();
                                (from res in ScanItemList
                                 where res.Itri_line_no == itemline //&& res.Sad_itm_stus == _itemstatus
                                 select res).ToList<InventoryRequestItem>().ForEach(x => x.Itri_qty = x.Itri_qty + Convert.ToDecimal(txtQty.Text.ToString()));

                                itemline = (line).First();
                                (from res in ScanItemList
                                 where res.Itri_line_no == itemline //&& res.Sad_itm_stus == _itemstatus
                                 select res).ToList<InventoryRequestItem>().ForEach(x => x.Itri_app_qty = x.Itri_app_qty + Convert.ToDecimal(txtQty.Text.ToString()));

                                itemline = (line).First();
                                (from res in ScanItemList
                                 where res.Itri_line_no == itemline //&& res.Sad_itm_stus == _itemstatus
                                 select res).ToList<InventoryRequestItem>().ForEach(x => x.Itri_bqty = x.Itri_bqty + Convert.ToDecimal(txtQty.Text.ToString()));
                                ScanItemList = ScanItemList;
                            }
                            else
                            {
                                var _maxline = (from _ls in ScanItemList
                                                select _ls.Itri_line_no).Max();
                                _itm.Itri_itm_cd = _item;
                                _itm.Itri_itm_stus = _status;
                                _itm.Itri_line_no = Convert.ToInt32(_maxline) + 1;
                                _itm.Itri_qty = Convert.ToDecimal(_qty);
                                if (isApprovalSend == true)
                                {
                                    _itm.Itri_app_qty = 0;
                                    _itm.Itri_bqty = 0;
                                }
                                else
                                {
                                    _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                                    _itm.Itri_bqty = Convert.ToDecimal(_qty);
                                }
                                _itm.Mi_longdesc = _itms.Mi_longdesc;
                                _itm.Mi_model = _itms.Mi_model;
                                _itm.Mi_brand = _itms.Mi_brand;
                                //Added by Prabhath on 17/12/2013 ************* start **************
                                _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                                //Added by Prabhath on 17/12/2013 ************* end **************

                                if (PNLTobechange.Visible == true)
                                {
                                    _itm.Itri_note = ddlStatusToBeChange.SelectedValue.ToString();
                                    _itm.Itri_mitm_cd = txtItemToBeChange.Text;
                                }
                                if (doc_tp == "COM_OUT")
                                {
                                    _itm.Itri_note = userSeqNo;
                                }
                                ScanItemList.Add(_itm);
                            }
                    }
                    else
                    {
                        _itm.Itri_itm_cd = _item;
                        _itm.Itri_itm_stus = _status;

                        _itm.Itri_line_no = 1;
                        _itm.Itri_qty = Convert.ToDecimal(_qty);
                        if (isApprovalSend == true)
                        {
                            _itm.Itri_app_qty = 0;
                            _itm.Itri_bqty = 0;
                        }
                        else
                        {
                            _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                            _itm.Itri_bqty = Convert.ToDecimal(_qty);
                        }
                        _itm.Mi_longdesc = _itms.Mi_longdesc;
                        _itm.Mi_model = _itms.Mi_model;
                        _itm.Mi_brand = _itms.Mi_brand;
                        //Added by Prabhath on 17/12/2013 ************* start **************
                        _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                        //Added by Prabhath on 17/12/2013 ************* end **************
                        //if (PNLTobechange.Visible == true)
                        //{
                        //    _itm.Itri_note = ddlStatusToBeChange.SelectedValue.ToString();
                        //    _itm.Itri_mitm_cd = txtItemToBeChange.Text;
                        //}
                        //_itm.Itri_note = ddlStatusToBeChange.SelectedValue.ToString();
                        //_itm.Itri_itm_stus_desc = getItemStatusDesc(_status);
                        //_itm.Itri_note_desc = getItemStatusDesc(ddlStatusToBeChange.SelectedValue.ToString());
                        //_itm.Itri_seq_no = Convert.ToInt32(_UserSeqNo);

                        if (doc_tp == "COM_OUT")
                        {
                            _itm.Itri_note = userSeqNo;
                        }

                        ScanItemList = new List<InventoryRequestItem>();
                        ScanItemList.Add(_itm);
                    }
                }
                else
                {
                    _itm.Itri_itm_cd = _item;
                    _itm.Itri_itm_stus = _status;
                    _itm.Itri_line_no = 1;
                    _itm.Itri_qty = Convert.ToDecimal(_qty);
                    if (isApprovalSend == true)
                    {
                        _itm.Itri_app_qty = 0;
                        _itm.Itri_bqty = 0;
                    }
                    else
                    {
                        _itm.Itri_app_qty = Convert.ToDecimal(_qty);
                        _itm.Itri_bqty = Convert.ToDecimal(_qty);
                    }
                    _itm.Mi_longdesc = _itms.Mi_longdesc;
                    _itm.Mi_model = _itms.Mi_model;
                    _itm.Mi_brand = _itms.Mi_brand;
                    //Added by Prabhath on 17/12/2013 ************* start **************
                    _itm.Itri_unit_price = Convert.ToDecimal(_UnitCost);
                    //Added by Prabhath on 17/12/2013 ************* end **************

                    if (PNLTobechange.Visible == true)
                    {
                        _itm.Itri_note = ddlStatusToBeChange.SelectedValue.ToString();
                        _itm.Itri_mitm_cd = txtItemToBeChange.Text;
                    }

                    _itm.Itri_itm_stus_desc = getItemStatusDesc(_status);
                    _itm.Itri_note_desc = getItemStatusDesc(ddlStatusToBeChange.SelectedValue.ToString());

                    ScanItemList = new List<InventoryRequestItem>();
                    ScanItemList.Add(_itm);
                }

                if (string.IsNullOrEmpty(_UserSeqNo))
                {
                    GenerateNewUserSeqNo();
                }

                //Test
                List<ReptPickItems> _reptItemsTemp = new List<ReptPickItems>();
                _reptItemsTemp = _base.CHNLSVC.Inventory.GetAllScanRequestItemsList(Convert.ToInt32(ViewState["userSeqNo"].ToString()));

                List<MasterItemStatus> oItemStaus = (List<MasterItemStatus>)Session["ItemStatus"];

                List<ReptPickItems> _saveonly = new List<ReptPickItems>();
                foreach (InventoryRequestItem _addedItem in ScanItemList)
                {
                    ReptPickItems _reptitm = new ReptPickItems();
                    _reptitm.Tui_usrseq_no = Convert.ToInt32(ViewState["userSeqNo"].ToString());
                    _reptitm.Tui_req_itm_qty = _addedItem.Itri_app_qty;
                    _reptitm.Tui_req_itm_cd = _addedItem.Itri_itm_cd;
                    _reptitm.Tui_req_itm_stus = _addedItem.Itri_itm_stus;
                    _reptitm.Tui_pic_itm_cd = Convert.ToString(_addedItem.Itri_line_no);
                    _reptitm.Tui_pic_itm_stus = Convert.ToString(_addedItem.Itri_itm_stus);
                    _reptitm.Tui_pic_itm_qty = _addedItem.Itri_qty;

                    if (_addedItem.MasterItem == null)
                    {
                        _addedItem.MasterItem = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _addedItem.Itri_itm_cd);
                    }

                    if (!IsBinTransfer)
                    {
                        if (_reptItemsTemp.Count > 0)
                        {
                            ReptPickItems oItem = _reptItemsTemp.Find(x => x.Tui_req_itm_qty == _addedItem.Itri_app_qty && x.Tui_req_itm_cd == _addedItem.Itri_itm_cd && _reptitm.Tui_req_itm_stus == _addedItem.Itri_itm_stus);
                            if (oItem != null)
                            {
                                _reptitm.Tui_pic_itm_cd = oItem.Tui_pic_itm_cd;
                                _reptitm.Tui_pic_itm_stus = oItem.Tui_pic_itm_stus;
                            }
                        }
                    }
                    else
                    {
                        _reptitm.Tui_pic_itm_stus = "1";
                    }
                    _saveonly.Add(_reptitm);

                    if (oItemStaus != null && oItemStaus.Count > 0)
                    {
                        _addedItem.Mis_desc = oItemStaus.Find(x => x.Mis_cd == _addedItem.Itri_itm_stus).Mis_desc;
                    }

                    _addedItem.Itri_itm_stus_desc = getItemStatusDesc(_addedItem.Itri_itm_stus);
                    _addedItem.Itri_note_desc = getItemStatusDesc(_addedItem.Itri_note);
                }
                _base.CHNLSVC.Inventory.SavePickedItems(_saveonly);

                //grdItems.DataSource = null;
                //grdItems.DataSource = ScanItemList;
                //grdItems.DataBind();

                Session["ScanItemListUC"] = ScanItemList;

                if (((GridView)this.Parent.FindControl("grdItems")) != null)
                {
                    ((GridView)this.Parent.FindControl("grdItems")).DataSource = ScanItemList;
                    ((GridView)this.Parent.FindControl("grdItems")).DataBind();

                }
                else if (((GridView)this.Parent.FindControl("gvItems")) != null)
                {
                    ((GridView)this.Parent.FindControl("gvItems")).DataSource = ScanItemList;
                    ((GridView)this.Parent.FindControl("gvItems")).DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('grdItems not found.');", true);
                    return;
                }

                // ViewState["ScanItemList"] = ScanItemList;
                ItemList = ScanItemList;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('" + ex.Message + "');", true);

                _base.CHNLSVC.CloseChannel();
                return;
            }
        }

        protected void txtQty_PDA_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal qty;
                if (!decimal.TryParse(txtQty_PDA.Text, out qty))
                {
                    txtQty_PDA.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please enter valid quantity.');", true);
                    return;
                }

                DataTable _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtPDAItemcode.Text.ToUpper().Trim(), string.Empty);
                if (_inventoryLocation.Rows.Count > 0)
                {
                    var count = from bl in _inventoryLocation.AsEnumerable()
                                where bl.Field<string>("MIS_DESC") == ddlStatus.SelectedItem.Text && bl.Field<decimal>("INL_FREE_QTY") > 0
                                select bl.Field<decimal>("INL_FREE_QTY");
                    decimal itemcount;
                    if (!(decimal.TryParse(count.First().ToString(), out itemcount)))
                    {
                        txtQty_PDA.Focus();
                        ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('No stock balance available...!');", true);
                        return;
                    }
                    else
                    {
                        if (itemcount < qty)
                        {
                            txtQty_PDA.Text = string.Empty;
                            txtQty_PDA.Focus();
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Please check the inventory balance...!');", true);
                            return;
                        }
                        else
                        {
                            txtQty_PDA.Text = qty.ToString("N2");
                        }
                    }
                }
                else
                {
                    txtPDAItemcode.Focus();
                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('No stock balance available...!');", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void chkOnlyQty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOnlyQty.Checked)
            {
                txtQty.ReadOnly = false; 
            }
            else
            {
                txtQty.ReadOnly = true;
                txtItemCode.Text = "";
                txtItemCode_TextChanged(null,null);
            }
        }

        protected void lbtnSerClick_Click(object sender, EventArgs e)
        {
            txtSerialI_TextChanged(null, null);
        }

        protected void lbseradd_Click(object sender, EventArgs e)
        {
            try
            {
                Session["SP"] = "show";
                if (string.IsNullOrEmpty(txtItemCode.Text))
                    Session["_SerialSearchType"] = "SER1_WOITEM";
                else
                    Session["_SerialSearchType"] = "SER1_WITEM";
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;

                string SearchParams = string.Empty;
                DataTable result = new DataTable();

                if (!ReqSupplier)
                {
                    if (!IsBinTransfer)
                    {
                        if (isbatchserial == false)
                        {
                            SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                            result = _base.CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, null, null);
                            lblvalue.Text = "134";
                        }

                        else
                        {
                            SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                            result = _base.CHNLSVC.Inventory.SearchSerialsInr_Batchno(SearchParams, null, null);
                            lblvalue.Text = "SearialSearchTOBIN";
                        }
                    }
                    else
                    {
                        if (isbatchserial == false)
                        {
                            SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearialSearchTOBIN);
                            result = _base.CHNLSVC.CommonSearch.SearchSerialByLocBINNEW(SearchParams, null, null);
                            lblvalue.Text = "SearialSearchTOBIN";
                        }
                        else
                        {
                            SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item_Serials);
                            result = _base.CHNLSVC.Inventory.SearchSerialsInr_Batchno(SearchParams, null, null);
                            lblvalue.Text = "SearialSearchTOBIN";
                        }
                    }
                    if (result.Rows.Count > 0 && ddlStatus.SelectedItem != null && !string.IsNullOrEmpty(ddlStatus.SelectedItem.Text))
                    {
                        DataRow[] dr = result.Select("[ItemStatus] = '" + ddlStatus.SelectedItem.Text + "'");
                        if (dr.Length > 0)
                        {
                            result = result.Select("[ItemStatus] = '" + ddlStatus.SelectedItem.Text + "'").CopyToDataTable();
                        }
                        else
                        {
                            result.Rows.Clear();
                        }
                    }
                    grdserdata.DataSource = result;
                    grdserdata.DataBind();
                   // BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    DateRow.Visible = false;
                    Userpopup2.Show();
                }
                else
                {
                    SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial);
                    result = _base.CHNLSVC.Inventory.GetSupplierItemSerial(SearchParams, null, null);
                    result.Columns.Remove("Serial 3");
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    lblvalue.Text = "Serial";
                    DateRow.Visible = false;
                    UserPopup.Show();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        protected void btnclose2_Click(object sender, EventArgs e)
        {
            Userpopup2.Hide();
            UserPopup.Hide();
        }

        protected void chkall_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in grdserdata.Rows)
            {
                if(chkall.Checked)
                {
                    ((CheckBox)row.FindControl("chkser")).Checked = true;
                }
                else
                {
                    ((CheckBox)row.FindControl("chkser")).Checked = false;
                }
             
               
            }
          
            UserPopup.Hide();
            Userpopup2.Show();
        }

        protected void btnaddserall_Click(object sender, EventArgs e)
        {
            AddallSerials();
        }
        private void AddallSerials()
        {

            try
            {
                Session["Binnew"] = "Binnew";
                Session["itemcode"] = txtItemCode.Text.ToUpper();

                foreach (GridViewRow row in grdserdata.Rows)
                {
                    if (((CheckBox)row.FindControl("chkser")).Checked)
                    {
                        txtItemCode.Text = Session["itemcode"].ToString();
                        string ser1 = ((Label)row.FindControl("lbser1")).Text.ToString();
                        txtSerialI.Text = ser1;
                        string itemCode = txtItemCode.Text.ToUpper();
                        DataTable dtserials = _base.CHNLSVC.Inventory.GET_INR_SER(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), null, itemCode, ddlStatus.SelectedValue, txtSerialI.Text.Trim());

                        Session["SELECTED_SERIAL_CHSTO"] = txtSerialI.Text.Trim();
                        if (dtserials.Rows.Count <= 0)
                        {
                            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter valid serial number...!')", true);
                            DisplayMessage("This serial number is not available! Please check again.!");
                            txtSerialI.Text = string.Empty;
                            txtSerialI.Focus();
                            return;
                        }

                        if (dtserials.Rows.Count > 1)
                        {
                            dgvItem.DataSource = new DataTable();
                            dgvItem.DataSource = dtserials;
                            dgvItem.DataBind();
                            lblcaption.Text = "Select Item Code";
                            mpexcel.Show();
                            Session["EXCELPOPUP"] = "Yes";
                            return;
                        }

                        if (IsBinTransfer)
                        {
                            if (string.IsNullOrEmpty(BinCode))
                            {
                                txtItemCode.Text = "";
                                txtItemToBeChange.Text = "";
                                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Select from BIN code.');", true);
                                return;
                            }
                            else
                            {
                                if (dtserials.Rows[0]["INS_BIN"].ToString() != BinCode)
                                {
                                    txtSerialI.Text = "";
                                    txtSerialII.Text = "";
                                    txtSerialIII.Text = "";
                                    txtItemCode.Text = "";
                                    txtItemToBeChange.Text = "";
                                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Select a serial from selected BIN.');", true);
                                    return;
                                }
                            }
                        }

                        ViewState["SerialID"] = dtserials.Rows[0]["INS_SER_ID"].ToString();

                        if (chkDirectScan.Checked == false)
                        {
                            txtItemCode.Text = dtserials.Rows[0]["INS_ITM_CD"].ToString();
                            txtItemCode.ReadOnly = true;
                            txtSerialI.Text = dtserials.Rows[0]["INS_SER_1"].ToString();
                            txtSerialII.Text = dtserials.Rows[0]["INS_SER_2"].ToString();
                            txtSerialIII.Text = dtserials.Rows[0]["INS_SER_3"].ToString();
                            // ddlStatus.SelectedValue = dtserials.Rows[0]["INS_ITM_STUS"].ToString();
                            ddlBinCode.DataSource = dtserials;
                            ddlBinCode.DataTextField = "INS_BIN";
                            ddlBinCode.DataValueField = "INS_BIN";
                            ddlBinCode.DataBind();
                            ddlStatus.DataSource = dtserials;
                            ddlStatus.DataTextField = "mis_desc";
                            ddlStatus.DataValueField = "INS_ITM_STUS";
                            ddlStatus.DataBind();
                            if (string.IsNullOrEmpty(txtItemToBeChange.Text))
                            {
                                txtItemToBeChange.Text = txtItemCode.Text.ToUpper();
                            }
                            txtSerialI.ReadOnly = true;
                            txtSerialII.ReadOnly = true;
                            txtSerialIII.ReadOnly = true;
                            txtQty.Text = "1";
                            txtQty.ReadOnly = true;

                            ddlStatusToBeChange.SelectedIndex = ddlStatusToBeChange.Items.IndexOf(ddlStatusToBeChange.Items.FindByValue(dtserials.Rows[0]["INS_ITM_STUS"].ToString()));
                        }
                        else
                        {
                            txtItemCode.Text = dtserials.Rows[0]["INS_ITM_CD"].ToString();
                            txtItemCode.ReadOnly = true;
                            txtSerialI.Text = dtserials.Rows[0]["INS_SER_1"].ToString();
                            txtSerialII.Text = dtserials.Rows[0]["INS_SER_2"].ToString();
                            txtSerialIII.Text = dtserials.Rows[0]["INS_SER_3"].ToString();
                            //ddlStatus.SelectedValue = dtserials.Rows[0]["INS_ITM_STUS"].ToString();
                            txtSerialI.ReadOnly = true;
                            txtSerialII.ReadOnly = true;
                            txtSerialIII.ReadOnly = true;

                            txtItemCode.Text = dtserials.Rows[0]["INS_ITM_CD"].ToString();
                            if (string.IsNullOrEmpty(txtItemToBeChange.Text))
                            {
                                txtItemToBeChange.Text = txtItemCode.Text.ToUpper();
                            }
                            ddlBinCode.DataSource = dtserials;
                            ddlBinCode.DataTextField = "INS_BIN";
                            ddlBinCode.DataValueField = "INS_BIN";
                            ddlBinCode.DataBind();
                            ddlStatus.DataSource = dtserials;
                            ddlStatus.DataTextField = "mis_desc";
                            ddlStatus.DataValueField = "INS_ITM_STUS";
                            ddlStatus.DataBind();

                            txtQty.Text = "1";
                            txtQty.ReadOnly = true;
                        }

                        if (!string.IsNullOrEmpty(txtItemCode.Text.ToUpper()))
                        {
                            _itemdetail = _base.CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItemCode.Text.ToUpper());
                        }
                        if (_itemdetail != null)
                        {
                            if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                            {
                                if (_itemdetail.Mi_itm_tp == "V")
                                {
                                    PageClear();
                                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Virtual item not allowed.');", true);
                                    return;
                                }

                                _chargeType = _itemdetail.Mi_chg_tp;
                                string _description = _itemdetail.Mi_longdesc;
                                string _model = _itemdetail.Mi_model;
                                string _brand = _itemdetail.Mi_brand;
                                string _partNo = _itemdetail.Mi_part_no;
                                string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Serialized" : "Non-Serialized";

                                if (_itemdetail.Mi_is_ser1 != 1)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Non-Serialized item.');", true);
                                    return;
                                }

                                lblDescription.Text = _description;
                                lblModel.Text = _model;
                                lblBrand.Text = _brand;
                                lblPart.Text = _partNo;
                                //lblItemSerialStatus.Text = "Serial Status : " + _serialstatus;
                                _isDecimalAllow = _base.CHNLSVC.Inventory.IsUOMDecimalAllow(txtItemCode.Text.ToUpper());
                                if (_itemdetail.MI_IS_EXP_DT == 1)
                                {
                                    divExpiryDate.Visible = true;
                                    //dtExp.Visible = true;
                                }
                                else
                                {
                                    divExpiryDate.Visible = false;
                                    //dtExp.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            txtItemCode.Text = string.Empty;
                            lblDescription.Text = string.Empty;
                            lblModel.Text = string.Empty;
                            lblBrand.Text = string.Empty;
                            //lblItemSerialStatus.Text = "Serial Status : " + string.Empty;
                            //txtUnitCost.Text = string.Empty;
                            txtQty.Text = string.Empty;
                            ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyWarningToast('Invalid item code.');", true);
                        }

                        DataTable _inventoryLocation = _base.CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), string.Empty);
                        grdInventoryBalance.DataSource = null;
                        grdInventoryBalance.AutoGenerateColumns = false;
                        grdInventoryBalance.DataSource = _inventoryLocation;
                        grdInventoryBalance.DataBind();

                        if (IsBinTransfer)
                        {
                            DataTable dt = _base.CHNLSVC.Inventory.GetItemBalanceForBIN(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItemCode.Text.ToUpper().Trim(), string.Empty, BinCode);
                            grdInventoryBalance.DataSource = null;
                            grdInventoryBalance.AutoGenerateColumns = false;
                            grdInventoryBalance.DataSource = dt;
                            grdInventoryBalance.DataBind();
                        }
                        lbtnAdd_Click(null, null);
                    }
                }
                Userpopup2.Hide();
                UserPopup.Hide();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        
        }

        protected void ddlBoxTp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblPkgQty.Text = "";
                decimal tmpPkgQty = 0, pkgQty = 0, _modPkgQty = 0, _pickQty = 0;
                if (_pkgTblTp=="MODEL_TBL")
                {
                    _modPkgQty = decimal.TryParse(ddlBoxTp.SelectedValue, out tmpPkgQty) ? Convert.ToDecimal(ddlBoxTp.SelectedValue) : 0;
                    _pickQty = decimal.TryParse(txtQty.Text, out tmpPkgQty) ? Convert.ToDecimal(txtQty.Text.Trim()) : 0;
                    //if (_pickQty >= _modPkgQty)
                    //{
                        pkgQty = (_pickQty /_modPkgQty);
                    //}
                }
                else
                {
                    pkgQty = decimal.TryParse(txtQty.Text, out tmpPkgQty) ? Convert.ToDecimal(txtQty.Text.Trim()) : 1;
                }
                lblPkgQty.Text = pkgQty.ToString("N2");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            }
        }

        //Adde By Dulaj 2018/Jun/05
        public void changeName()
        {
            divItemCodeDiv.InnerText = "Asset Code";
        }

    }
 
}