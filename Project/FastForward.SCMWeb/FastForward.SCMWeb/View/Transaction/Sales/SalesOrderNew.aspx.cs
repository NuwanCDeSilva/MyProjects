using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Sales;
using FF.BusinessObjects;
using FF.BusinessObjects.CustService;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.Sales;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Sales
{
    public partial class SalesOrderNew : BasePage
    {
        protected Int32 _postBackCount
        {
            get
            {
                if (Session["_postBackCount"] == null)
                {
                    return 0;
                }
                else
                {
                    return (Int32)Session["_postBackCount"];
                }
            }
            set { Session["_postBackCount"] = value; }
        }
        List<INR_RES_LOG> _resLogAvaData = new List<INR_RES_LOG>();
        #region Variables
        protected bool IscheckApproval { get { return (bool)Session["IscheckApproval"]; } set { Session["IscheckApproval"] = value; } }
        protected bool Isrequestbase { get { return (bool)Session["Isrequestbase"]; } set { Session["Isrequestbase"] = value; } }
        protected List<InvoiceItem> _invoiceItemList { get { return (List<InvoiceItem>)Session["_invoiceItemListSo"]; } set { Session["_invoiceItemListSo"] = value; } }
        protected List<InvoiceItem> _invoiceItemListWithDiscount { get { return (List<InvoiceItem>)Session["_invoiceItemListWithDiscountSO"]; } set { Session["_invoiceItemListWithDiscountSO"] = value; } }
        protected List<RecieptItem> _recieptItem { get { return (List<RecieptItem>)Session["_recieptItem"]; } set { Session["_recieptItem"] = value; } }
        protected List<RecieptItem> _newRecieptItem { get { return (List<RecieptItem>)Session["_newRecieptItem"]; } set { Session["_newRecieptItem"] = value; } }
        protected MasterBusinessEntity _businessEntity { get { return (MasterBusinessEntity)Session["_businessEntity"]; } set { Session["_businessEntity"] = value; } }
        protected List<MasterItemComponent> _masterItemComponent { get { return (List<MasterItemComponent>)Session["_masterItemComponent"]; } set { Session["_masterItemComponent"] = value; } }
        protected PriceBookLevelRef _priceBookLevelRef { get { return (PriceBookLevelRef)Session["_priceBookLevelRef"]; } set { Session["_priceBookLevelRef"] = value; } }
        protected List<PriceBookLevelRef> _priceBookLevelRefList { get { return (List<PriceBookLevelRef>)Session["_priceBookLevelRefList"]; } set { Session["_priceBookLevelRefList"] = value; } }
        protected List<PriceDetailRef> _priceDetailRef { get { return (List<PriceDetailRef>)Session["_priceDetailRef"]; } set { Session["_priceDetailRef"] = value; } }
        protected MasterBusinessEntity _masterBusinessCompany { get { return (MasterBusinessEntity)Session["_masterBusinessCompany"]; } set { Session["_masterBusinessCompany"] = value; } }
        protected List<PriceSerialRef> _MainPriceSerial { get { return (List<PriceSerialRef>)Session["_MainPriceSerial"]; } set { Session["_MainPriceSerial"] = value; } }
        protected List<PriceSerialRef> _tempPriceSerial { get { return (List<PriceSerialRef>)Session["_tempPriceSerial"]; } set { Session["_tempPriceSerial"] = value; } }
        protected List<PriceCombinedItemRef> _MainPriceCombinItem { get { return (List<PriceCombinedItemRef>)Session["_MainPriceCombinItem"]; } set { Session["_MainPriceCombinItem"] = value; } }
        protected List<PriceCombinedItemRef> _tempPriceCombinItem { get { return (List<PriceCombinedItemRef>)Session["_tempPriceCombinItem"]; } set { Session["_tempPriceCombinItem"] = value; } }
        protected bool _isInventoryCombineAdded { get { return (bool)Session["_isInventoryCombineAdded"]; } set { Session["_isInventoryCombineAdded"] = value; } }
        protected Int32 ScanSequanceNo { get { return (Int32)Session["ScanSequanceNo"]; } set { Session["ScanSequanceNo"] = value; } }
        protected List<ReptPickSerials> ScanSerialList { get { return (List<ReptPickSerials>)Session["ScanSerialList"]; } set { Session["ScanSerialList"] = value; } }
        protected bool IsPriceLevelAllowDoAnyStatus { get { return (bool)Session["IsPriceLevelAllowDoAnyStatus"]; } set { Session["IsPriceLevelAllowDoAnyStatus"] = value; } }
        protected string WarrantyRemarks { get { return (string)Session["WarrantyRemarks"]; } set { Session["WarrantyRemarks"] = value; } }
        protected Int32 WarrantyPeriod { get { return (Int32)Session["WarrantyPeriod"]; } set { Session["WarrantyPeriod"] = value; } }
        protected string ScanSerialNo { get { return (string)Session["ScanSerialNo"]; } set { Session["ScanSerialNo"] = value; } }
        protected string DefaultItemStatus { get { return (string)Session["DefaultItemStatus"]; } set { Session["DefaultItemStatus"] = value; } }
        protected List<InvoiceSerial> InvoiceSerialList { get { return (List<InvoiceSerial>)Session["InvoiceSerialList"]; } set { Session["InvoiceSerialList"] = value; } }
        protected List<ReptPickSerials> InventoryCombinItemSerialList { get { return (List<ReptPickSerials>)Session["InventoryCombinItemSerialList"]; } set { Session["InventoryCombinItemSerialList"] = value; } }
        protected List<ReptPickSerials> PriceCombinItemSerialList { get { return (List<ReptPickSerials>)Session["PriceCombinItemSerialList"]; } set { Session["PriceCombinItemSerialList"] = value; } }
        protected List<ReptPickSerials> BuyBackItemList { get { return (List<ReptPickSerials>)Session["BuyBackItemList"]; } set { Session["BuyBackItemList"] = value; } }
        protected Int32 _lineNo { get { return (Int32)Session["_lineNo"]; } set { Session["_lineNo"] = value; } }
        protected bool _isEditPrice { get { return (bool)Session["_isEditPrice"]; } set { Session["_isEditPrice"] = value; } }
        protected bool _isEditDiscount { get { return (bool)Session["_isEditDiscount"]; } set { Session["_isEditDiscount"] = value; } }
        protected decimal GrndSubTotal { get { return (decimal)Session["GrndSubTotal"]; } set { Session["GrndSubTotal"] = value; } }
        protected decimal GrndDiscount { get { return (decimal)Session["GrndDiscount"]; } set { Session["GrndDiscount"] = value; } }
        protected decimal GrndTax { get { return (decimal)Session["GrndTax"]; } set { Session["GrndTax"] = value; } }
        protected decimal _toBePayNewAmount { get { return (decimal)Session["_toBePayNewAmount"]; } set { Session["_toBePayNewAmount"] = value; } }
        //protected bool _isCompleteCode { get { return (bool)Session["_isCompleteCode"]; } set { Session["_isCompleteCode"] = value; } }
        //private bool _isCompleteCode = false;
        protected bool _isCompleteCode { get { return (bool)Session["_isCompleteCode"]; } set { Session["_isCompleteCode"] = value; } }
        protected decimal SSPriceBookPrice { get { return (decimal)Session["SSPriceBookPrice"]; } set { Session["SSPriceBookPrice"] = value; } }
        protected string SSPriceBookSequance { get { return (string)Session["SSPriceBookSequance"]; } set { Session["SSPriceBookSequance"] = value; } }
        protected string SSPriceBookItemSequance { get { return (string)Session["SSPriceBookItemSequance"]; } set { Session["SSPriceBookItemSequance"] = value; } }
        protected string SSIsLevelSerialized { get { return (string)Session["SSIsLevelSerialized"]; } set { Session["SSIsLevelSerialized"] = value; } }
        protected string SSPromotionCode { get { return (string)Session["SSPromotionCode"]; } set { Session["SSPromotionCode"] = value; } }
        protected string SSCirculerCode { get { return (string)Session["SSCirculerCode"]; } set { Session["SSCirculerCode"] = value; } }
        protected Int32 SSPRomotionType { get { return (Int32)Session["SSPRomotionType"]; } set { Session["SSPRomotionType"] = value; } }
        protected Int32 SSCombineLine { get { return (Int32)Session["SSCombineLine"]; } set { Session["SSCombineLine"] = value; } }
        protected Dictionary<decimal, decimal> ManagerDiscount { get { return (Dictionary<decimal, decimal>)Session["ManagerDiscount"]; } set { Session["ManagerDiscount"] = value; } }
        protected CashGeneralEntiryDiscountDef GeneralDiscount { get { return (CashGeneralEntiryDiscountDef)Session["GeneralDiscount"]; } set { Session["GeneralDiscount"] = value; } }
        protected SarDocumentPriceDefn GeneralDiscount_new { get { return (SarDocumentPriceDefn)Session["GeneralDiscount_new"]; } set { Session["GeneralDiscount_new"] = value; } }
        protected string DefaultBook { get { return (string)Session["DefaultBook"]; } set { Session["DefaultBook"] = value; } }
        protected string DefaultLevel { get { return (string)Session["DefaultLevel"]; } set { Session["DefaultLevel"] = value; } }
        protected string DefaultInvoiceType { get { return (string)Session["DefaultInvoiceType"]; } set { Session["DefaultInvoiceType"] = value; } }
        protected string DefaultStatus { get { return (string)Session["DefaultStatus"]; } set { Session["DefaultStatus"] = value; } }
        protected string DefaultBin { get { return (string)Session["DefaultBin"]; } set { Session["DefaultBin"] = value; } }
        protected MasterItem _itemdetail { get { return (MasterItem)Session["_itemdetail"]; } set { Session["_itemdetail"] = value; } }
        protected List<MasterItemTax> MainTaxConstant { get { return (List<MasterItemTax>)Session["MainTaxConstant"]; } set { Session["MainTaxConstant"] = value; } }
        protected List<ReptPickSerials> _promotionSerial { get { return (List<ReptPickSerials>)Session["_promotionSerial"]; } set { Session["_promotionSerial"] = value; } }
        protected List<ReptPickSerials> _promotionSerialTemp { get { return (List<ReptPickSerials>)Session["_promotionSerialTemp"]; } set { Session["_promotionSerialTemp"] = value; } }
        protected bool _isBackDate { get { return (bool)Session["_isBackDate"]; } set { Session["_isBackDate"] = value; } }
        protected MasterProfitCenter _MasterProfitCenter { get { return (MasterProfitCenter)Session["_MasterProfitCenter"]; } set { Session["_MasterProfitCenter"] = value; } }

        protected SarDocumentPriceDefn _docPriceDefnforprofitcentr { get { return (SarDocumentPriceDefn)Session["_docPriceDefnforprofitcentr"]; } set { Session["_docPriceDefnforprofitcentr"] = value; } }
        protected List<PriceDefinitionRef> _PriceDefinitionRef { get { return (List<PriceDefinitionRef>)Session["_PriceDefinitionRef"]; } set { Session["_PriceDefinitionRef"] = value; } }
        protected bool _isGiftVoucherCheckBoxClick { get { return (bool)Session["_isGiftVoucherCheckBoxClick"]; } set { Session["_isGiftVoucherCheckBoxClick"] = value; } }
        protected DataTable MasterChannel { get { return (DataTable)Session["MasterChannel"]; } set { Session["MasterChannel"] = value; } }

        private const string InvoiceBackDateName = "SALESENTRY";

        private static int VirtualCounter = 1;
        protected bool IsToken { get { Session["IsToken"] = (Session["IsToken"] == null) ? false : (bool)Session["IsToken"]; return (bool)Session["IsToken"]; } set { Session["IsToken"] = value; } }
        protected bool IsSaleFigureRoundUp { get { return (bool)Session["IsSaleFigureRoundUp"]; } set { Session["IsSaleFigureRoundUp"] = value; } }
        protected DataTable _tblExecutive { get { return (DataTable)Session["_tblExecutive"]; } set { Session["_tblExecutive"] = value; } }
        protected bool IsDlvSaleCancelAllowUser { get { return (bool)Session["IsDlvSaleCancelAllowUser"]; } set { Session["IsDlvSaleCancelAllowUser"] = value; } }
        protected bool _IsVirtualItem { get { Session["_IsVirtualItem"] = (Session["_IsVirtualItem"] == null) ? false : (bool)Session["_IsVirtualItem"]; return (bool)Session["_IsVirtualItem"]; } set { Session["_IsVirtualItem"] = value; } }
        protected bool _isNewPromotionProcess { get { Session["_isNewPromotionProcess"] = (Session["_isNewPromotionProcess"] == null) ? false : (bool)Session["_isNewPromotionProcess"]; return (bool)Session["_isNewPromotionProcess"]; } set { Session["_isNewPromotionProcess"] = value; } }
        protected string technicianCode { get { return (string)Session["technicianCode"]; } set { Session["technicianCode"] = value; } }
        protected bool _iswhat { get { Session["_iswhat"] = (Session["_iswhat"] == null) ? false : (bool)Session["_iswhat"]; return (bool)Session["_iswhat"]; } set { Session["_iswhat"] = value; } }
        protected DataTable _tblPromotor { get { return (DataTable)Session["_tblPromotor"]; } set { Session["_tblPromotor"] = value; } }
        protected bool _serialMatch { get { Session["_serialMatch"] = (Session["_serialMatch"] == null) ? false : (bool)Session["_serialMatch"]; return (bool)Session["_serialMatch"]; } set { Session["_serialMatch"] = value; } }
        protected PriortyPriceBook _priorityPriceBook { get { return (PriortyPriceBook)Session["_priorityPriceBook"]; } set { Session["_priorityPriceBook"] = value; } }
        protected bool _processMinusBalance { get { Session["_processMinusBalance"] = (Session["_processMinusBalance"] == null) ? false : (bool)Session["_processMinusBalance"]; return (bool)Session["_processMinusBalance"]; } set { Session["_processMinusBalance"] = value; } }
        protected int _discountSequence { get { Session["_discountSequence"] = (Session["_discountSequence"] == null) ? 0 : 0; return (int)Session["_discountSequence"]; } set { Session["_discountSequence"] = value; } }
        protected bool _isRegistrationMandatory { get { Session["_isRegistrationMandatory"] = (Session["_isRegistrationMandatory"] == null) ? false : (bool)Session["_isRegistrationMandatory"]; return (bool)Session["_isRegistrationMandatory"]; } set { Session["_isRegistrationMandatory"] = value; } }
        protected bool _isNeedRegistrationReciept { get { Session["_isNeedRegistrationReciept"] = (Session["_isNeedRegistrationReciept"] == null) ? false : (bool)Session["_isNeedRegistrationReciept"]; return (bool)Session["_isNeedRegistrationReciept"]; } set { Session["_isNeedRegistrationReciept"] = value; } }
        protected decimal _totalRegistration { get { Session["_totalRegistration"] = (Session["_totalRegistration"] == null) ? 0 : 0; return (decimal)Session["_totalRegistration"]; } set { Session["_totalRegistration"] = value; } }
        protected LoyaltyType _loyaltyType { get { return (LoyaltyType)Session["_loyaltyType"]; } set { Session["_loyaltyType"] = value; } }
        protected int _proVouInvcLine { get { Session["_proVouInvcLine"] = (Session["_proVouInvcLine"] == null) ? 0 : 0; return (int)Session["_proVouInvcLine"]; } set { Session["_proVouInvcLine"] = value; } }
        protected string _proVouInvcItem { get { return (string)Session["_proVouInvcItem"]; } set { Session["_proVouInvcItem"] = value; } }
        protected Boolean _isGroup { get { Session["_isGroup"] = (Session["_isGroup"] == null) ? false : (Boolean)Session["_isGroup"]; return (Boolean)Session["_isGroup"]; } set { Session["_isGroup"] = value; } }
        protected DateTime _serverDt { get { return (DateTime)Session["_serverDt"]; } set { Session["_serverDt"] = value; } }
        protected bool _isCombineAdding { get { Session["_isCombineAdding"] = (Session["_isCombineAdding"] == null) ? false : (bool)Session["_isCombineAdding"]; ; return (bool)Session["_isCombineAdding"]; } set { Session["_isCombineAdding"] = value; } }
        protected int _combineCounter { get { Session["_combineCounter"] = (Session["_combineCounter"] == null) ? 0 : 0; return (int)Session["_combineCounter"]; } set { Session["_combineCounter"] = value; } }
        protected string _paymodedef { get { return (string)Session["_paymodedef"]; } set { Session["_paymodedef"] = value; } }
        protected bool _isCheckedPriceCombine { get { Session["_isCheckedPriceCombine"] = (Session["_isCheckedPriceCombine"] == null) ? false : (bool)Session["_isCheckedPriceCombine"]; return (bool)Session["_isCheckedPriceCombine"]; } set { Session["_isCheckedPriceCombine"] = value; } }
        protected bool _isFirstPriceComItem { get { Session["_isFirstPriceComItem"] = (Session["_isFirstPriceComItem"] == null) ? false : (bool)Session["_isFirstPriceComItem"]; return (bool)Session["_isFirstPriceComItem"]; } set { Session["_isFirstPriceComItem"] = value; } }
        protected string _serial2 { get { return (string)Session["_serial2"]; } set { Session["_serial2"] = value; } }
        protected string _prefix { get { return (string)Session["_prefix"]; } set { Session["_prefix"] = value; } }
        protected bool _isBlocked { get { Session["_isBlocked"] = (Session["_isBlocked"] == null) ? false : (bool)Session["_isBlocked"]; return (bool)Session["_isBlocked"]; } set { Session["_isBlocked"] = value; } }
        protected bool _isItemChecking { get { Session["_isItemChecking"] = (Session["_isItemChecking"] == null) ? false : (bool)Session["_isItemChecking"]; return (bool)Session["_isItemChecking"]; } set { Session["_isItemChecking"] = value; } }
        protected DataTable _levelStatus { get { return (DataTable)Session["_levelStatus"]; } set { Session["_levelStatus"] = value; } }
        protected string _userid { get { return (string)Session["_userid"]; } set { Session["_userid"] = value; } }
        protected Decimal exchangerate { get { Session["exchangerate"] = (Session["exchangerate"] == null) ? 0 : 0; return (Decimal)Session["exchangerate"]; } set { Session["exchangerate"] = value; } }
        protected MasterBusinessEntity _entity { get { return (MasterBusinessEntity)Session["_entity"]; } set { Session["_entity"] = value; } }
        protected bool _stopit { get { Session["_stopit"] = (Session["_stopit"] == null) ? false : (bool)Session["_stopit"]; return (bool)Session["_stopit"]; } set { Session["_stopit"] = value; } }
        protected DataTable uniqueitems { get { return (DataTable)Session["uniqueitems"]; } set { Session["uniqueitems"] = value; } }
        protected DataTable dtiInvtems { get { return (DataTable)Session["dtiInvtems"]; } set { Session["dtiInvtems"] = value; } }
        protected List<RegistrationList> _List { get { return (List<RegistrationList>)Session["_List"]; } set { Session["_List"] = value; } }

        //protected List<PriceDetailRef> _PriceDetailRefPromo { get { return (List<PriceDetailRef>)Session["_PriceDetailRefPromo"]; } set { Session["_PriceDetailRefPromo"] = value; } }
        //protected List<PriceSerialRef> _PriceSerialRefPromo { get { return (List<PriceSerialRef>)Session["_PriceSerialRefPromo"]; } set { Session["_PriceSerialRefPromo"] = value; } }
        //protected List<PriceSerialRef> _PriceSerialRefNormal { get { return (List<PriceSerialRef>)Session["_PriceSerialRefNormal"]; } set { Session["_PriceSerialRefNormal"] = value; } }

        private List<PriceDetailRef> _PriceDetailRefPromo = new List<PriceDetailRef>();
        private List<PriceSerialRef> _PriceSerialRefPromo = new List<PriceSerialRef>();
        private List<PriceSerialRef> _PriceSerialRefNormal = new List<PriceSerialRef>();

        protected bool IsFwdSaleCancelAllowUser { get { Session["IsFwdSaleCancelAllowUser"] = (Session["IsFwdSaleCancelAllowUser"] == null) ? false : (bool)Session["IsFwdSaleCancelAllowUser"]; return (bool)Session["IsFwdSaleCancelAllowUser"]; } set { Session["IsFwdSaleCancelAllowUser"] = value; } }
        protected List<CashGeneralEntiryDiscountDef> _CashGeneralEntiryDiscount { get { return (List<CashGeneralEntiryDiscountDef>)Session["_CashGeneralEntiryDiscount"]; } set { Session["_CashGeneralEntiryDiscount"] = value; } }
        protected bool _isGiftVoucherMsgPopup { get { Session["_isGiftVoucherMsgPopup"] = (Session["_isGiftVoucherMsgPopup"] == null) ? false : (bool)Session["_isGiftVoucherMsgPopup"]; return (bool)Session["_isGiftVoucherMsgPopup"]; } set { Session["_isGiftVoucherMsgPopup"] = value; } }
        protected List<ReptPickSerials> _selectedItemList { get { return (List<ReptPickSerials>)Session["_selectedItemList"]; } set { Session["_selectedItemList"] = value; } }
        protected List<MasterItemStatus> oMasterItemStatuss { get { return (List<MasterItemStatus>)Session["oMasterItemStatuss"]; } set { Session["oMasterItemStatuss"] = value; } }

        protected CashPromotionDiscountDetail _CashPromotionDiscountDetail { get { return (CashPromotionDiscountDetail)Session["_CashPromotionDiscountDetail"]; } set { Session["_CashPromotionDiscountDetail"] = value; } }
        protected bool _isStrucBaseTax { get { Session["_isStrucBaseTax "] = (Session["_isStrucBaseTax "] == null) ? false : (bool)Session["_isStrucBaseTax "]; ; return (bool)Session["_isStrucBaseTax "]; } set { Session["_isStrucBaseTax "] = value; } }
        protected bool _isAllocateCustomer { get { Session["_isAllocateCustomer "] = (Session["_isAllocateCustomer "] == null) ? false : (bool)Session["_isAllocateCustomer "]; ; return (bool)Session["_isAllocateCustomer "]; } set { Session["_isAllocateCustomer "] = value; } }
        #endregion Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null || Session["UserCompanyCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            clearMessages();
            if (!IsPostBack)
            {
                try
                {
                    ClearVariables();
                    //Dulaj 2018/Dec/20
                    if (CheckPCTempSave())
                    {
                        tempSaveDiv.Visible = true;
                        SoTempAll.Visible = true;
                        SoTempPending.Visible = true;
                    }
                    else
                    {
                        tempSaveDiv.Visible = false;
                        SoTempAll.Visible = false;
                        SoTempPending.Visible = false;
                    }
                    //
                    Clear();
                    Invoice();
                    InitializeValuesNDefaultValueSet();
                    Invoice_Load();

                    ViewState["ITEMSTABLE"] = null;
                    DataTable dtitems = new DataTable();
                    dtitems.Columns.AddRange(new DataColumn[15] { new DataColumn("soi_itm_line"), new DataColumn("soi_itm_cd"), new DataColumn("description"), new DataColumn("soi_itm_stus"), new DataColumn("soi_qty"), new DataColumn("soi_unit_rt"), new DataColumn("soi_unit_amt"), new DataColumn("soi_disc_rt"), new DataColumn("soi_disc_amt"), new DataColumn("soi_itm_tax_amt"), new DataColumn("soi_tot_amt"), new DataColumn("soi_pbook"), new DataColumn("soi_pb_lvl"), new DataColumn("soi_res_no"), new DataColumn("itri_seq_no") });
                    ViewState["ITEMSTABLE"] = dtitems;
                    this.BindItemsGrid();

                    ViewState["SERIALSTABLE"] = null;
                    DataTable dtserials = new DataTable();
                    dtserials.Columns.AddRange(new DataColumn[8] { new DataColumn("sose_itm_line"), new DataColumn("sose_itm_cd"), new DataColumn("Model"), new DataColumn("Status"), new DataColumn("Qty"), new DataColumn("sose_ser_1"), new DataColumn("sose_ser_2"), new DataColumn("Warranty") });
                    ViewState["SERIALSTABLE"] = dtserials;
                    this.BindSerialsGrid();

                    //string SearchParamsTown = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Town);
                    //DataTable result = CHNLSVC.CommonSearch.GetTown(SearchParamsTown, null, null);
                    //ddltown.DataSource = result;
                    //ddltown.DataTextField = "TOWN";
                    //ddltown.DataValueField = "CODE";
                    //if (result.Rows.Count > 0)
                    //{
                    //    ddltown.DataBind();
                    //}
                    //ddltown.Items.Insert(0, new ListItem("Select", "0"));

                    txtDisRate.Text = "0";
                    txtDisAmt.Text = "0";
                    txtTaxAmt.Text = "0";
                    txtLineTotAmt.Text = "0";

                    txtdocrefno.Focus();
                    Session["ucc"] = null;
                    hdfShowCustomer.Value = null;

                    cmbExecutive.SelectedIndex = 0;
                    // txtexcutive.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    DisplayMessage(ex.Message, 4);
                }
            }
            else
            {
                if (Session["mpPriceNPromotion"] != null && Session["mpPriceNPromotion"].ToString() == "1")
                {
                    mpPriceNPromotion.Show();
                }

                if (Session["mpPickSerial"] != null && Session["mpPickSerial"].ToString() == "Y")
                {
                    mpPickSerial.Show();
                }

                if (Session["ucc"] != null && Session["ucc"].ToString() == "ucv")
                {
                    CustomerPopoup.Show();
                }

                if (Session["_cusCode"] != null)
                {
                    txtCustomer.Text = Session["_cusCode"].ToString();
                    LoadCustomerDetailsByCustomer();
                    Session["_cusCode"] = null;
                }



                //if (!String.IsNullOrEmpty(txtSerialNo.Text))
                //{
                //    return;
                //}

                var ctrlName = Request.Params[Page.postEventSourceID];
                var args = Request.Params[Page.postEventArgumentID];

                string[] objectNames = ctrlName.ToString().Split('$');

                if (objectNames[objectNames.Length - 1] == "txtItem")
                {
                    //if (!String.IsNullOrEmpty(txtSerialNo.Text))
                    //{
                    //    CheckSerialAvailability();
                    //    return;
                    //}
                    Session["mpPriceNPromotion"] = null;
                    hdfConf.Value = "1";
                    hdfConfItem.Value = null;
                    hdfConfStatus.Value = null;
                    txtItem_TextChanged(null, null);
                }
                else if (objectNames[objectNames.Length - 1] == "txtSerialNo")
                {
                    CheckSerialAvailability();
                }

                if (objectNames.Length > 2 && objectNames[2] == "ucPayModes1")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "scrollTop();", true);
                }

                if (Session["SIPopup"] != null && Session["SIPopup"].ToString() == "SIPopup")
                {
                    txtSearchbyword.Focus();
                    SIPopup.Show();
                }

            }
        }


        protected void Page_Init(object sender, EventArgs e)
        {
            var onBlurScript = Page.ClientScript.GetPostBackEventReference(txtItem, "OnBlur");
            txtItem.Attributes.Add("onblur", onBlurScript);
            txtQty.Attributes.Add("onblur", Page.ClientScript.GetPostBackEventReference(txtQty, "OnBlur"));
            txtSerialNo.Attributes.Add("onblur", Page.ClientScript.GetPostBackEventReference(txtSerialNo, "OnBlur"));

            //txtDisRate.Attributes.Add("onblur", Page.ClientScript.GetPostBackEventReference(txtDisRate, "OnBlur"));
            //txtDisAmt.Attributes.Add("onblur", Page.ClientScript.GetPostBackEventReference(txtDisAmt, "OnBlur"));
        }

        #region Main Buttons
        private bool ValidateSO()
        {
            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select customer !!!')", true);

                lbtncode.Focus();
                return false;
            }

            if (cmbTitle.SelectedItem.Text == "Select")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select title !!!')", true);

                cmbTitle.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPerTown.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter the dispatch location !!!')", true);

                txtlocation.Focus();
                return false;
            }

            if (gvInvoiceItem.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please enter order items !!!')", true);

                txtItem.Focus();
                return false;
            }


            //if (cmbExecutive.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select sales executive !!!')", true);

            //    cmbExecutive.Focus();
            //    return false;
            //}

            if (string.IsNullOrEmpty(txtexcutive.Text))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select sales executive !!!')", true);

                txtexcutive.Focus();
                return false;
            }

            //if (cmbTechnician.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select promotor !!!')", true);

            //    cmbTechnician.Focus();
            //    return false;
            //}
            return true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmplaceord.Value == "Yes")
                {
                    bool isvalidate = ValidateSO();
                    if (isvalidate == false)
                    {
                        return;
                    }
                    saveNew();

                }

            }
            catch (Exception ex)
            {
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                DispMsg(ex.Message, "E");
            }
        }
        private void saveNew()
        {
            _userid = (string)Session["UserID"];
            //string _customerCompany = (string)Session["CUSCOM"];
            //string _customerLocation = (string)Session["CUSLOC"];
            //string _cushascompany = (string)Session["CUSHASCOMPANY"];
            decimal exchangerate = 0;
            if (!string.IsNullOrEmpty(txtcurrency.Text.Trim()))
            {
                exchangerate = GetExchangeRate();
            }
            Int32 so_rest_stk = 0;
            Int32 mpc_so_res = 0;
            List<Int32> qtylist = new List<int>();


            #region checkvalidation
            if (!string.IsNullOrEmpty(txtPerTown.Text))
            {
                DataTable dtchkpc_rest_stk = CHNLSVC.Sales.Check_PC_SO_REST_STK(Session["UserCompanyCode"].ToString(), txtPerTown.Text.ToString());

                if (dtchkpc_rest_stk.Rows.Count > 0)
                {
                    foreach (DataRow ddr in dtchkpc_rest_stk.Rows)
                    {
                        if (!string.IsNullOrEmpty(ddr["mpc_so_rest_stk"].ToString()))
                        {
                            so_rest_stk = Convert.ToInt32(ddr["mpc_so_rest_stk"].ToString());
                        }
                        if (!string.IsNullOrEmpty(ddr["mpc_so_res"].ToString()))
                        {
                            mpc_so_res = Convert.ToInt32(ddr["mpc_so_res"].ToString());
                        }
                    }
                }

                if (so_rest_stk == 1)
                {
                    foreach (GridViewRow ddr in gvInvoiceItem.Rows)
                    {
                        Label status = (ddr.FindControl("lblsad_itm_stus") as Label);
                        Label Item = (ddr.FindControl("InvItm_Item") as Label);
                        //DataTable dtstatusBAL = CHNLSVC.Sales.GetItemStatusVal(status.Text);

                        //string _StatusBAL = string.Empty;

                        //if (dtstatusBAL.Rows.Count > 0)
                        //{
                        //    foreach (DataRow ddr2BAL in dtstatusBAL.Rows)
                        //    {
                        //        _StatusBAL = ddr2BAL[0].ToString();
                        //    }
                        //}

                        DataTable dtbal = CHNLSVC.Sales.CheckLocationBaBalance(Session["UserCompanyCode"].ToString(), txtPerTown.Text.ToString(), Item.Text, status.Text);
                        Decimal balance = 0;

                        foreach (DataRow item in dtbal.Rows)
                        {
                            balance = Convert.ToDecimal(item["inl_free_qty"].ToString());
                        }

                        if (balance > 0)
                        {
                            qtylist.Add(0);
                        }
                        else
                        {
                            qtylist.Add(1);
                        }
                    }
                }
            }

            if (qtylist.Contains(1))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Stock is not available for added item/items at the dispatch location')", true);
                return;
            }
            #endregion


            #region Header

            string SBU_Character = Session["UserSBU"].ToString();
            MasterAutoNumber mastAutoNo = new MasterAutoNumber();
            mastAutoNo.Aut_cate_cd = Session["UserDefProf"].ToString();
            mastAutoNo.Aut_cate_tp = "LOC";
            mastAutoNo.Aut_direction = 0;
            mastAutoNo.Aut_moduleid = "SO";
            mastAutoNo.Aut_start_char = cmbInvType.SelectedValue;
            mastAutoNo.Aut_year = DateTime.Now.Year;

            SalesOrderHeader SalesOrder = new SalesOrderHeader();

            string seqnofororder = (string)Session["SOSEQNO"];

            if (string.IsNullOrEmpty(seqnofororder))
            {
                seqnofororder = "0";
            }

            SalesOrder.SOH_COM = Session["UserCompanyCode"].ToString();
            SalesOrder.SOH_PC = Session["UserDefProf"].ToString();
            SalesOrder.SOH_TP = "INV";
            SalesOrder.SOH_SO_TP = cmbInvType.SelectedValue;
            SalesOrder.SOH_SO_SUB_TP = "SA";
            SalesOrder.SOH_SO_NO = txtInvoiceNo.Text.Trim();
            SalesOrder.SOH_DT = Convert.ToDateTime(txtdate.Text);
            SalesOrder.SOH_MANUAL = 0;
            SalesOrder.SOH_MAN_REF = txtManualRefNo.Text.Trim();
            SalesOrder.SOH_REF_DOC = txtdocrefno.Text.Trim();
            SalesOrder.SOH_CUS_CD = txtCustomer.Text.Trim();
            SalesOrder.SOH_CUS_NAME = txtCusName.Text.Trim();
            SalesOrder.SOH_CUS_ADD1 = txtAddress1.Text.Trim();
            SalesOrder.SOH_CUS_ADD2 = txtAddress2.Text.Trim();
            SalesOrder.SOH_CURRENCY = txtcurrency.Text.Trim();
            SalesOrder.SOH_EX_RT = exchangerate;
            SalesOrder.SOH_TOWN_CD = txtlocation.Text;//_dcustown;
            SalesOrder.SOH_D_CUST_CD = txtdelcuscode.Text.Trim();//_dcuscode;
            SalesOrder.SOH_D_CUST_ADD1 = txtdelad1.Text.Trim();//_dcusadd1;
            SalesOrder.SOH_D_CUST_ADD2 = txtdelad2.Text.Trim(); //_dcusadd2;
            SalesOrder.SOH_MAN_CD = txtexcutive.Text;//cmbExecutive.SelectedValue;
            SalesOrder.SOH_SALES_EX_CD = txtexcutive.Text;//cmbExecutive.SelectedValue;
            SalesOrder.SOH_SALES_STR_CD = string.Empty;
            SalesOrder.SOH_SALES_SBU_CD = string.Empty;
            SalesOrder.SOH_SALES_SBU_MAN = string.Empty;
            SalesOrder.SOH_SALES_CHN_CD = string.Empty;
            SalesOrder.SOH_SALES_CHN_MAN = string.Empty;
            SalesOrder.SOH_SALES_REGION_CD = string.Empty;
            SalesOrder.SOH_SALES_REGION_MAN = string.Empty;
            SalesOrder.SOH_SALES_ZONE_CD = string.Empty;
            SalesOrder.SOH_SALES_ZONE_MAN = string.Empty;
            SalesOrder.SOH_STRUCTURE_SEQ = txtQuotation.Text.Trim();
            SalesOrder.SOH_ESD_RT = 0;
            SalesOrder.SOH_WHT_RT = 0;
            SalesOrder.SOH_EPF_RT = 0;
            SalesOrder.SOH_PDI_REQ = 0;
            SalesOrder.SOH_REMARKS = string.Empty;
            SalesOrder.SOH_IS_ACC_UPLOAD = 0;
            SalesOrder.SOH_REMARKS = txtRemarks.Text.ToString();

            SalesOrder.SOH_ANAL_10 = Convert.ToDecimal(lblGrndTotalAmount.Text);
            SalesOrder.SOH_ANAL_11 = Convert.ToDecimal(lblAvailableCredit.Text);
            HpSystemParameters _SystemPara = new HpSystemParameters();
            _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", Session["UserDefProf"].ToString(), "SOAP", Convert.ToDateTime(txtdate.Text));

            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16011))
            {
                //if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16055))
                //{

                //    SalesOrder.SOH_STUS = "A";
                //}
                if (_SystemPara.Hsy_cd == "SOAP")
                {
                    decimal _totalInvoice = Convert.ToDecimal(lblGrndTotalAmount.Text);
                    decimal _credit = Convert.ToDecimal(lblAvailableCredit.Text);
                    if (_credit < _totalInvoice)
                    {
                        SalesOrder.SOH_STUS = "S";
                    }
                    else
                    {
                        SalesOrder.SOH_STUS = "A";
                    }
                }
                else if (_MasterProfitCenter.Mpc_chk_credit == true)
                {
                    SalesOrder.SOH_STUS = "S";
                }
                else if (_MasterProfitCenter.Mpc_chk_credit == false)
                {
                    SalesOrder.SOH_STUS = "A";
                }
            }
            else
            {
                SalesOrder.SOH_STUS = "A";
            }

            if (Session["UserCompanyCode"].ToString() == "AAL")
            {
                _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", Session["UserDefProf"].ToString(), SalesOrder.SOH_SO_TP, Convert.ToDateTime(txtdate.Text));
                if (_SystemPara.Hsy_val == 1)
                {
                    SalesOrder.SOH_STUS = "A";
                }
            }
            //Added By Dulaj 2018/Dec/20 
            if (CheckPCTempSave())
            {
                SalesOrder.SOH_STUS = "P";
            }
            //
            //if (_userid == "ADMIN")
            //{
            //    SalesOrder.SOH_STUS = "A";
            //}
            //else
            //{
            //    SalesOrder.SOH_STUS = "S";
            //}

            SalesOrder.SOH_CRE_BY = _userid;
            SalesOrder.SOH_CRE_WHEN = CHNLSVC.Security.GetServerDateTime();
            SalesOrder.SOH_MOD_BY = _userid;
            SalesOrder.SOH_MOD_WHEN = CHNLSVC.Security.GetServerDateTime();
            SalesOrder.SOH_SESSION_ID = Session["SessionID"].ToString();
            SalesOrder.SOH_ANAL_1 = txtPromotor.Text.Trim();
            SalesOrder.SOH_ANAL_2 = string.Empty;
            SalesOrder.SOH_ANAL_3 = string.Empty;
            SalesOrder.SOH_ANAL_4 = txtPoNo.Text.Trim();
            SalesOrder.SOH_ANAL_5 = cmbTechnician.SelectedValue;
            SalesOrder.SOH_ANAL_6 = string.Empty;
            SalesOrder.SOH_ANAL_7 = 0;
            SalesOrder.SOH_ANAL_8 = Convert.ToInt32(0);
            SalesOrder.SOH_ANAL_9 = Convert.ToInt32(0);
            SalesOrder.SOH_ANAL_10 = Convert.ToDecimal(lblGrndTotalAmount.Text);
            SalesOrder.SOH_ANAL_11 = Convert.ToDecimal(lblAvailableCredit.Text);
            SalesOrder.SOH_ANAL_12 = Convert.ToDateTime(DateTime.Now);
            SalesOrder.SOH_DIRECT = 1;
            SalesOrder.SOH_TAX_INV = chkTaxPayable.Checked ? 1 : 0;
            SalesOrder.SOH_GRUP_CD = string.Empty;
            SalesOrder.SOH_ACC_NO = string.Empty;
            SalesOrder.SOH_TAX_EXEMPTED = lblVatExemptStatus.Text == "Available" ? 1 : 0;
            //if (_masterBusinessCompany != null)
            //{
            //    lblSVatStatus.Text = _masterBusinessCompany.Mbe_is_svat ? "Available" : "None";
            //}
            if (string.IsNullOrEmpty(lblSVatStatus.Text.Trim()))
            {
                DispMsg("Exempt status cannot be null !"); return;
            }
            SalesOrder.SOH_IS_SVAT = lblSVatStatus.Text == "Available" ? 1 : 0;
            SalesOrder.SOH_FIN_CHRG = 1;
            SalesOrder.SOH_DEL_LOC = txtdellocation.Text; //_dcusloc;
            // SalesOrder.SOH_GRN_COM = _customerCompany;
            SalesOrder.SOH_GRN_LOC = txtlocation.Text.Trim();
            // SalesOrder.SOH_IS_GRN = Convert.ToInt32(_cushascompany);
            SalesOrder.SOH_D_CUST_NAME = txtdelname.Text.Trim();// _dcusname;
            SalesOrder.SOH_IS_DAYEND = 0;
            SalesOrder.SOH_SCM_UPLOAD = 0;
            SalesOrder.SOH_SEQ_NO = Convert.ToInt32(seqnofororder);
            SalesOrder.mpc_so_res = mpc_so_res;
            SalesOrder.SOH_DISP_LOC = txtPerTown.Text.Trim();
            #endregion

            List<SalesOrderItems> _SalesOrderItemsList = new List<SalesOrderItems>();
            #region Items
            if (gvInvoiceItem.Rows.Count > 0)
            {
                foreach (GridViewRow ddr in gvInvoiceItem.Rows)
                {
                    Label status = (ddr.FindControl("lblsad_itm_stus") as Label);
                    // DataTable dtstatus = CHNLSVC.Sales.GetItemStatusVal(status.Text);

                    string _Status = status.Text;

                    //if (dtstatus.Rows.Count > 0)
                    //{
                    //    foreach (DataRow ddr2 in dtstatus.Rows)
                    //    {
                    //        _Status = ddr2[0].ToString();
                    //    }
                    //}
                    Label SOI_ITM_LINE = (ddr.FindControl("lblsad_itm_line") as Label);
                    Label SOI_ITM_CD = (ddr.FindControl("InvItm_Item") as Label);
                    Label SOI_QTY = (ddr.FindControl("InvItm_Qty") as Label);
                    Label SOI_INV_QTY = (ddr.FindControl("lblsad_itm_stus") as Label);
                    Label SOI_UNIT_RT = (ddr.FindControl("InvItm_UPrice") as Label);
                    Label SOI_UNIT_AMT = (ddr.FindControl("lblsad_unit_amt") as Label);
                    Label SOI_DISC_RT = (ddr.FindControl("lblsad_disc_rt") as Label);
                    Label SOI_DISC_AMT = (ddr.FindControl("InvItm_DisAmt") as Label);
                    Label SOI_ITM_TAX_AMT = (ddr.FindControl("InvItm_TaxAmt") as Label);
                    Label SOI_TOT_AMT = (ddr.FindControl("lblsad_tot_amt") as Label);
                    Label SOI_PBOOK = (ddr.FindControl("InvItm_Book") as Label);
                    Label SOI_PB_LVL = (ddr.FindControl("InvItm_Level") as Label);
                    Label SOI_RES_NO = (ddr.FindControl("InvItm_ResNo") as Label);
                    Label SOI_PB_SEQ = (ddr.FindControl("InvItm_PbSeq") as Label);
                    Label SOI_ITEMSEQ = (ddr.FindControl("InvItm_PbLineSeq") as Label);
                    Label SOI_PROCD = (ddr.FindControl("InvItm_PromoCd") as Label);
                    Label InvItm_JobLine = (Label)ddr.FindControl("InvItm_JobLine");
                    Label InvItm_ResLine = (Label)ddr.FindControl("InvItm_ResLine");

                    int Resline = 0;
                    Resline = Convert.ToInt32(InvItm_ResLine.Text);
                    if (SOI_PBOOK.Text == "")
                    {
                        SalesOrder = null;
                        mastAutoNo = null;
                        _SalesOrderItemsList = null;
                        string msg = "Please edit item" + SOI_ITM_CD.Text;
                        DisplayMessage(msg, 2);
                        return;
                    }

                    SalesOrderItems SalesOrderItems = new SalesOrderItems();
                    //  SalesOrderItems.SOI_SEQ_NO = Convert.ToInt32(newseqno);
                    SalesOrderItems.SOI_ITM_LINE = Convert.ToInt32(SOI_ITM_LINE.Text);
                    // SalesOrderItems.SOI_SO_NO = outputopno;
                    SalesOrderItems.SOI_ITM_CD = SOI_ITM_CD.Text;
                    SalesOrderItems.SOI_ITM_STUS = _Status;
                    SalesOrderItems.SOI_ITM_TP = string.Empty;
                    SalesOrderItems.SOI_UOM = string.Empty;
                    SalesOrderItems.SOI_QTY = Convert.ToDecimal(SOI_QTY.Text);
                    SalesOrderItems.SOI_INV_QTY = Convert.ToDecimal(SOI_QTY.Text);
                    SalesOrderItems.SOI_UNIT_RT = Convert.ToDecimal(SOI_UNIT_RT.Text);
                    SalesOrderItems.SOI_UNIT_AMT = Convert.ToDecimal(SOI_UNIT_AMT.Text);
                    SalesOrderItems.SOI_DISC_RT = Convert.ToDecimal(SOI_DISC_RT.Text);
                    SalesOrderItems.SOI_DISC_AMT = Convert.ToDecimal(SOI_DISC_AMT.Text);
                    SalesOrderItems.SOI_ITM_TAX_AMT = Convert.ToDecimal(SOI_ITM_TAX_AMT.Text);
                    SalesOrderItems.SOI_TOT_AMT = Convert.ToDecimal(SOI_TOT_AMT.Text);
                    SalesOrderItems.SOI_PBOOK = SOI_PBOOK.Text;
                    SalesOrderItems.SOI_PB_LVL = SOI_PB_LVL.Text;
                    SalesOrderItems.SOI_PB_PRICE = 0;
                    SalesOrderItems.SOI_SEQ = Convert.ToInt32(SOI_PB_SEQ.Text);
                    SalesOrderItems.SOI_ITM_SEQ = Convert.ToInt32(SOI_ITEMSEQ.Text); //add rukshan 
                    // SalesOrderItems.SOI_ITM_SEQ = Convert.ToInt32((ddr.FindControl("itri_seq_no") as Label).Text);

                    if (_invoiceItemList.Count > 0)
                    {
                        WarrantyPeriod = _invoiceItemList.Find(x => x.Sad_itm_cd == SOI_ITM_CD.Text).Sad_warr_period;
                        WarrantyRemarks = _invoiceItemList.Find(x => x.Sad_itm_cd == SOI_ITM_CD.Text).Sad_warr_remarks;
                    }

                    SalesOrderItems.SOI_WARR_PERIOD = WarrantyPeriod;
                    SalesOrderItems.SOI_WARR_REMARKS = WarrantyRemarks;


                    SalesOrderItems.SOI_IS_PROMO = 0;
                    SalesOrderItems.SOI_PROMO_CD = SOI_PROCD.Text;
                    SalesOrderItems.SOI_ALT_ITM_CD = string.Empty;
                    SalesOrderItems.SOI_ALT_ITM_DESC = string.Empty;
                    SalesOrderItems.SOI_PRINT_STUS = 0;
                    SalesOrderItems.SOI_RES_NO = SOI_RES_NO.Text;
                    SalesOrderItems.SOI_RES_LINE_NO = Resline;
                    SalesOrderItems.SOI_JOB_NO = string.Empty;
                    SalesOrderItems.SOI_WARR_BASED = 0;
                    SalesOrderItems.SOI_MERGE_ITM = string.Empty;
                    SalesOrderItems.SOI_JOB_LINE = Convert.ToInt32(InvItm_JobLine.Text.ToString());
                    SalesOrderItems.SOI_OUTLET_DEPT = string.Empty;
                    SalesOrderItems.SOI_TRD_SVC_CHRG = 0;
                    SalesOrderItems.SOI_DIS_SEQ = 0;
                    SalesOrderItems.SOI_DIS_LINE = 0;
                    SalesOrderItems.SOI_DIS_TYPE = string.Empty;
                    SalesOrderItems.SOI_ANAL1 = string.Empty;
                    SalesOrderItems.SOI_ANAL2 = string.Empty;
                    SalesOrderItems.SOI_ANAL3 = string.Empty;
                    SalesOrderItems.SOI_ANAL4 = 1;
                    SalesOrderItems.SOI_ANAL5 = 1;
                    SalesOrderItems.SOI_PROMO_CD = cmbTechnician.SelectedValue;
                    SalesOrderItems.SOI_allocation = 1;
                    SalesOrderItems.SOI_resLogUpdate = 1;
                    _SalesOrderItemsList.Add(SalesOrderItems);
                }
            }
            #endregion
            List<SalesOrderSer> _SalesOrderSerList = new List<SalesOrderSer>();
            #region SalesOrderSer

            if (gvPopSerial.Rows.Count > 0)
            {
                foreach (GridViewRow ddr in gvPopSerial.Rows)
                {
                    Label SOSE_ITM_LINE = (ddr.FindControl("popSer_BaseItemLine") as Label);
                    Label SOSE_ITM_CD = (ddr.FindControl("popSer_Item") as Label);
                    Label SOSE_SER_1 = (ddr.FindControl("popSer_Serial1") as Label);

                    SalesOrderSer _SalesOrderSer = new SalesOrderSer();
                    //_SalesOrderSer.SOSE_SEQ_NO = Convert.ToInt32(newseqno);
                    _SalesOrderSer.SOSE_ITM_LINE = Convert.ToInt32(SOSE_ITM_LINE.Text);
                    //_SalesOrderSer.SOSE_SO_NO = outputopno;
                    _SalesOrderSer.SOSE_ITM_CD = SOSE_ITM_CD.Text;
                    _SalesOrderSer.SOSE_SER_1 = SOSE_SER_1.Text;
                    _SalesOrderSer.SOSE_REMARKS = string.Empty;
                    _SalesOrderSer.SOSE_SEV_LOC = string.Empty;
                    _SalesOrderSer.SOSE_DEL_LOC = txtPerTown.Text;//txtdellocation.Text;
                    _SalesOrderSer.SOSE_SER_LINE = 0;
                    _SalesOrderSer.SOSE_SER_2 = string.Empty;
                    _SalesOrderSer.mpc_so_res = mpc_so_res;
                    _SalesOrderSerList.Add(_SalesOrderSer);
                }
            }
            #endregion
            //for (int y = _SalesOrderItemsList.Count - 1; y >= 0; y--)
            //{
            //    MasterItem _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _SalesOrderItemsList[y].SOI_ITM_CD);
            //    if (_itemdetail != null)
            //    {
            //        if (_itemdetail.Mi_itm_tp == "V")
            //        {
            //            _SalesOrderItemsList.RemoveAll(x => x.SOI_ITM_CD == _SalesOrderItemsList[y].SOI_ITM_CD);
            //        }
            //    }
            //}
            string _msg = string.Empty;
            SalesOrder.SOH_ALLOCATION = false;
            Int32 _result = CHNLSVC.Sales.SaveSalesOrder(SalesOrder, mastAutoNo, _SalesOrderItemsList, _SalesOrderSerList, out _msg, IscheckApproval);
            if (_result > 0)
            {
                //string msg = "Successfully created sales order- " + _msg;
                //DisplayMessage(msg, 3);
                //ClearAll();
                //Clear();

                string msg = "Successfully created sales order- " + _msg;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msg + "');", true);
                //MessageBox.Show("Successfully Saved! Document No : " + documntNo, "Goods Received Note", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Session["OrNo"] = _msg;
                ClearAll();
                Clear();
                BackDatePermission();
                //  lblMssg.Text = "Successfully Saved! Document No :" + documntNo;
                lblMssg.Text = "Do you want print now?";
                PopupConfBox.Show();

            }
            else
            {
                if (_msg == "CL")
                {
                    Label19.Text = "Credit Period ";
                    Label18.Text = "Credit Limit ";
                    MdlSalesOrderApp.Show();
                    return;
                }
                else if (_msg == "CP")
                {
                    Label18.Text = "Credit Period ";
                    Label19.Text = "Credit Limit ";
                    MdlSalesOrderApp.Show();
                    return;
                }
                DisplayMessage(_msg, 4);
            }
        }

        protected void lbtnprintord_Click(object sender, EventArgs e)
        {
            if (txtconfirmclear.Value == "Yes")
            {
                try
                {
                    try
                    {
                        Response.Redirect(Request.RawUrl, false);
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
                    }
                    // ClearAll();
                    //ClearVariables();
                    //Response.Redirect(Request.RawUrl);
                    //Clear();
                    //DivsHide();
                }
                catch (Exception ex)
                {
                    //divalert.Visible = true;
                    DisplayMessage(ex.Message, 4);
                }
            }
        }

        #region Drop down
        protected void lbtncancel_Click(object sender, EventArgs e)
        {
            if (txtcancel.Value == "Yes")
            {
                try
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16012))
                    {
                        string msg = "You dont have permission to cancel .Permission code : 16012";
                        DisplayMessage(msg, 1);
                        return;
                    }

                    string ordstatus = (string)Session["STATUS"];
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an order !!!')", true);
                        lbtnsupplier.Focus();
                        return;
                    }

                    if (ordstatus == "C")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This order is already cancelled !!!')", true);
                        return;
                    }
                    if (CHNLSVC.Sales.SOACancleCheck(txtInvoiceNo.Text.Trim()).Rows.Count > 0)
                    {
                        string msg = "Selected order no still processing in inventry...!!!";
                        DisplayMessage(msg, 1);
                        return;
                    }
                    _userid = (string)Session["UserID"];
                    #region add by lakshan
                    List<InvoiceHeader> _avaInvList = CHNLSVC.Sales.ChkInvoiceAvailableForSalesOredr(txtInvoiceNo.Text.Trim());
                    if (_avaInvList != null)
                    {
                        if (_avaInvList.Count > 0)
                        {
                            DispMsg("Invoice number available. : " + _avaInvList[0].Sah_inv_no); return;
                        }
                    }
                    #endregion
                    SalesOrderHeader _SalesOrder = new SalesOrderHeader();

                    _SalesOrder.SOH_STUS = "C";
                    _SalesOrder.SOH_SO_NO = txtInvoiceNo.Text.Trim();
                    _SalesOrder.SOH_COM = Session["UserCompanyCode"].ToString();
                    _SalesOrder.SOH_PC = Session["UserDefProf"].ToString();
                    _SalesOrder.SOH_MOD_BY = _userid;
                    _SalesOrder.SOH_MOD_WHEN = CHNLSVC.Security.GetServerDateTime();
                    string error = string.Empty;
                    _SalesOrder.SOH_CRE_BY = Session["UserID"].ToString();
                    _SalesOrder.SOH_CRE_WHEN = DateTime.Now;
                    _SalesOrder.SOH_MOD_BY = Session["UserID"].ToString();
                    _SalesOrder.SOH_MOD_WHEN = DateTime.Now;
                    _SalesOrder.SOH_SESSION_ID = Session["SessionID"].ToString();
                    Int32 outputresultapprove = CHNLSVC.Sales.UpdateSOStatus(_SalesOrder, out error);

                    if (outputresultapprove == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully cancelled !!!')", true);

                        Clear();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(error))
                        {
                            DispMsg(error, "E");
                        }
                        else
                        {
                            //  ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                            DispMsg(error, "E");
                        }
                    }
                }
                catch (Exception ex)
                {
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    DispMsg(ex.Message, "E");
                }
            }
        }
        protected void lbtnkititem_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable result = CHNLSVC.Sales.SearchSalesRequest(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "16";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtncustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Session["_isFromOther"] = "true";
                Session["ucc"] = "ucv";
                hdfShowCustomer.Value = "ucv";
                CustomerPopoup.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtndiscount_Click(object sender, EventArgs e)
        {
            MPDis.Show();

            txtDisAmount.ReadOnly = false;
            txtDisAmount.Focus();

            ddlDisCategory.Enabled = true;
            try
            {
                BindGeneralDiscount();
                ddlDisCategory.Text = "Customer";

                if (string.IsNullOrEmpty(txtCustomer.Text))
                {

                    DisplayMessage("Please select the customer.");
                    return;
                }

                if (txtCustomer.Text == "CASH")
                {
                    MPDis.Hide();
                    DisplayMessage("Please select the valid customer. Customer should be registered.");
                    return;
                }

                if (_invoiceItemList != null)
                    if (_invoiceItemList.Count > 0)
                    {
                        ddlDisCategory.Enabled = true;
                    }
                    else
                    {
                        ddlDisCategory.Text = "Customer";
                        ddlDisCategory.Enabled = false;
                    }
                else
                {
                    ddlDisCategory.Text = "Customer";
                    ddlDisCategory.Enabled = false;
                }

                gvDisItem.Columns[6].Visible = false;

                if (_invoiceItemList != null)
                {
                    if (_invoiceItemList.Count > 0)
                    {
                        _CashGeneralEntiryDiscount = new List<CashGeneralEntiryDiscountDef>();
                        foreach (InvoiceItem _i in _invoiceItemList)
                        {
                            CashGeneralEntiryDiscountDef _one = new CashGeneralEntiryDiscountDef();

                            var _dup = from _l in _CashGeneralEntiryDiscount
                                       where _l.Sgdd_itm == _i.Sad_itm_cd && _l.Sgdd_pb == _i.Sad_pbook && _l.Sgdd_pb_lvl == _i.Sad_pb_lvl
                                       select _l;

                            if (_dup == null || _dup.Count() <= 0)
                            {
                                _one.Sgdd_itm = _i.Sad_itm_cd;
                                _one.Sgdd_pb = _i.Sad_pbook;
                                _one.Sgdd_pb_lvl = _i.Sad_pb_lvl;

                                _CashGeneralEntiryDiscount.Add(_one);
                            }
                        }
                        gvDisItem.DataSource = _CashGeneralEntiryDiscount;
                        gvDisItem.DataBind();
                    }
                }

                ddlDisCategory_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void lbtnupload_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtInvoiceNo.Text))
                {
                    //txtdellocation.Text = Session["UserDefLoca"].ToString();

                    // btnSearchDelLocation.Enabled = false;
                    txtdellocation.ReadOnly = true;
                    chkOpenDelivery.Enabled = false;
                    //  txtdellocation.Text = Session["UserDefLoca"].ToString();


                    MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, Session["UserCompanyCode"].ToString());
                    if (_masterBusinessCompany != null && _masterBusinessCompany.Mbe_intr_com && _masterBusinessCompany.Mbe_tp == "C")
                    {
                        //  btnSearchDelLocation.Enabled = true;
                        txtdellocation.ReadOnly = false;
                        chkOpenDelivery.Enabled = true;
                    }
                }
                else
                {

                }

                MpDelivery.Show();
                //MpDeliveryShow();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        #endregion

        #endregion



        #region Search
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
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
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
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
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
                grdResult.PageIndex = 0;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }


        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);

                if (lblvalue.Text == "Sale_Ex")
                {
                    txtexcutive.Text = grdResult.SelectedRow.Cells[2].Text;
                    txtexcutive.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    lblSalesEx.Text = grdResult.SelectedRow.Cells[3].Text;
                }
                if ((lblvalue.Text == "13") || (lblvalue.Text == "Customer_13"))
                {
                    txtCustomer.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadCustomerDetailsByCustomer();
                }

                if (lblvalue.Text == "32")
                {
                    txtNIC.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadCustomerDetailsByNIC();
                }

                if (lblvalue.Text == "33")
                {
                    txtMobile.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadCustomerDetailsByMobile();
                }

                if (lblvalue.Text == "168")
                {
                    txtLoyalty.Text = grdResult.SelectedRow.Cells[1].Text;
                    SetLoyalityColor();
                }

                if (lblvalue.Text == "76")
                {
                    txtSerialNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    Session["WARRANTY"] = grdResult.SelectedRow.Cells[2].Text;
                    //txtSerialNo_TextChanged(null, null);
                    CheckSerialAvailability();
                }

                if (lblvalue.Text == "158")
                {
                    txtSerialNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    CheckSerialAvailability();
                    chkDeliverLater.Checked = false;
                }
                //if (lblvalue.Text == "16")
                //{
                //    txtCustomer.Text = grdResult.SelectedRow.Cells[2].Text;
                //    LoadCusData();
                //    LoadCusLoyalityNo();
                //    LoadInvItems(Convert.ToInt32(grdResult.SelectedRow.Cells[3].Text));
                //}
                if (lblvalue.Text == "14")
                {
                    txtcurrency.Text = grdResult.SelectedRow.Cells[1].Text;
                    lblcurrency.Text = grdResult.SelectedRow.Cells[2].Text;
                }
                if (lblvalue.Text == "81")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    //  LoadItemDetail(txtItem.Text);
                    txtItem_TextChanged(null, null);
                }

                if (lblvalue.Text == "421")
                {
                    txtInvoiceNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadSalesOrderDetails();
                    //LoadSoData(txtInvoiceNo.Text, Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                    //LoadSoItemData(txtInvoiceNo.Text);
                    //LoadSoSerialData(txtInvoiceNo.Text);
                }

                if (lblvalue.Text == "1")
                {
                    MpDelivery.Show();
                    txtlocation.Text = grdResult.SelectedRow.Cells[1].Text;
                }

                if (lblvalue.Text == "Group_Sale")
                {
                    txtGroup.Text = grdResult.SelectedRow.Cells[1].Text;
                    mpGroup.Show();
                    txtGroup_TextChanged(null, null);
                }
                if (lblvalue.Text == "BuyBackItem")
                {
                    mpBuyBack.Show();
                    txtBBItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtBBItem_TextChanged(null, null);
                }
                //if (lblvalue.Text == "InvoiceWithDate")
                //{
                //    txtInvoiceNo.Text = grdResult.SelectedRow.Cells[1].Text;
                //    txtInvoiceNo_TextChanged(null, null);
                //}
                if (lblvalue.Text == "CustomerDel")
                {
                    MpDelivery.Show();
                    txtdelcuscode.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                if (lblvalue.Text == "LocationDel")
                {
                    MpDelivery.Show();
                    txtdellocation.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                if (lblvalue.Text == "Town")
                {
                    txtPerTown.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                if (lblvalue.Text == "DocNo")
                {
                    txtADVRNumber.Text = grdResult.SelectedRow.Cells[1].Text;
                    mpAdavce.Show();
                }

                if (lblvalue.Text == "Customer_DEL")
                {
                    txtdelcuscode.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadCusData_Del();
                    MpDelivery.Show();
                }
                if (lblvalue.Text == "SalesOrderNew")
                {
                    txtSalesOrderSearch.Text = grdResult.SelectedRow.Cells[1].Text;
                    mpLoadSalesOrder.Show();
                }
                else if (lblvalue.Text == "16")
                {
                    _invoiceItemList = new List<InvoiceItem>();
                    txtCustomer.Text = grdResult.SelectedRow.Cells[2].Text;
                    LoadCusData();
                    txtdellocation.Text = txtPerTown.Text;
                    LoadCusLoyalityNo();
                    string Reqno = grdResult.SelectedRow.Cells[1].Text;
                    // LoadInvItems(Convert.ToInt32(grdResult.SelectedRow.Cells[3].Text));
                    Addreqitems(Convert.ToInt32(grdResult.SelectedRow.Cells[3].Text));
                    txtdocrefno.Text = Reqno;
                    ViewCustomerAccountDetail(txtCustomer.Text);
                    LoadHDD(Reqno);
                    LoadPriceLevel(cmbInvType.Text, cmbBook.Text);
                    LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                    CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                    ClearPriceTextBox();
                    btnSearch_Serial.Visible = false;
                    Isrequestbase = true;
                    DataTable dt = (DataTable)ViewState["ITEMSTABLE"];
                    int k = 0;
                    foreach (GridViewRow row in gvInvoiceItem.Rows)
                    {

                        Decimal qty = 0;
                        Decimal upri = 0;
                        Decimal dis = 0;
                        Decimal tax = 0;

                        //if ((string.IsNullOrEmpty(row.Cells[4].Text.ToString())) || (row.Cells[4].Text.ToString() == "&nbsp;"))
                        //{
                        //    qty = 0;
                        //}
                        //else
                        //{
                        //    qty = Convert.ToDecimal(row.Cells[4].Text);
                        //}
                        if ((string.IsNullOrEmpty(dt.Rows[k]["sad_qty"].ToString())) || (dt.Rows[k]["sad_qty"].ToString() == "&nbsp;"))
                        {
                            qty = 0;
                        }
                        else
                        {
                            qty = Convert.ToDecimal(dt.Rows[k]["sad_qty"].ToString());
                        }

                        if ((string.IsNullOrEmpty(dt.Rows[k]["sad_unit_rt"].ToString())) || (dt.Rows[k]["sad_unit_rt"].ToString() == "&nbsp;"))
                        {
                            upri = 0;
                        }
                        else
                        {
                            upri = Convert.ToDecimal(dt.Rows[k]["sad_unit_rt"].ToString());
                        }
                        //if ((string.IsNullOrEmpty(row.Cells[5].Text.ToString())) || (row.Cells[5].Text.ToString() == "&nbsp;"))
                        //{
                        //    upri = 0;
                        //}
                        //else
                        //{
                        //    upri = Convert.ToDecimal(row.Cells[5].Text);
                        //}

                        if ((string.IsNullOrEmpty(row.Cells[8].Text.ToString())) || (row.Cells[8].Text.ToString() == "&nbsp;"))
                        {
                            dis = 0;
                        }
                        else
                        {
                            dis = Convert.ToDecimal(row.Cells[8].Text);
                        }

                        if ((string.IsNullOrEmpty(row.Cells[9].Text.ToString())) || (row.Cells[9].Text.ToString() == "&nbsp;"))
                        {
                            tax = 0;
                        }
                        else
                        {
                            tax = Convert.ToDecimal(row.Cells[9].Text);
                        }

                        // CalculateGrandTotalNew(qty, upri, dis, tax, true);
                        k++;
                    }

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
                }

                SIPopup.Hide();
                Session["SIPopup"] = null;
                ViewState["SEARCH"] = null;
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }



        private void Addreqitems(Int32 seqno)
        {
            DataTable dt = CHNLSVC.Sales.GetInvReqItems(seqno.ToString(), Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());

            int i = 1;
            foreach (DataRow row in dt.Rows)
            {

                InvoiceItem _list = new InvoiceItem();
                _list.Sad_itm_line = i;
                _list.Sad_itm_cd = row[1].ToString();
                _list.Sad_itm_stus = row[2].ToString();
                _list.Sad_qty = Convert.ToDecimal(row[5].ToString());
                txtItem.Text = _list.Sad_itm_cd;
                txtItem_TextChanged(null, null);
                cmbStatus.SelectedValue = _list.Sad_itm_stus;
                txtQty.Text = _list.Sad_qty.ToString();
                txtQty_TextChanged(null, null);
                lbtnadditems_Click(null, null);

                i++;

            }
        }
        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "Customer_13")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerBYsalesExe(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "Customer_13";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                if (lblvalue.Text == "Sale_Ex")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                    DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "Sale_Ex";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                if (lblvalue.Text == "32")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "32";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "13")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "13";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "33")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Mobile);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "33";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "168")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                    DataTable result = CHNLSVC.CommonSearch.SearchLoyaltyCard(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "168";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "76")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                    DataTable result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "76";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "158")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableGiftVoucher);
                    DataTable result = CHNLSVC.CommonSearch.SearchAvailableGiftVoucher(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "158";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "16")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    DataTable result = CHNLSVC.Sales.SearchSalesOrderRequest(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                    grdResultD.DataSource = result;
                    grdResultD.DataBind();
                    lblvalue.Text = "16";
                    BindUCtrlDDLData2(result);
                    ViewState["SEARCH"] = result;
                    UserDPopoup.Show();
                    Session["SIPopup"] = "DPopup";
                    txtSearchbyword.Focus();
                    //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                    //DataTable result = CHNLSVC.Sales.SearchSalesRequest(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    //grdResult.DataSource = result;
                    //grdResult.DataBind();
                    //lblvalue.Text = "16";
                    //ViewState["SEARCH"] = result;
                    //SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    //txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "14")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                    DataTable result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "14";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "81")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "81";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "421")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
                    DataTable result = CHNLSVC.Sales.SearchSalesOrder(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResultD.DataSource = result;
                    grdResultD.DataBind();
                    lblvalue.Text = "InvoiceWithDate";
                    ViewState["SEARCH"] = result;
                    UserDPopoup.Show();
                    Session["DPopup"] = "DPopup";
                    txtSearchbywordD.Focus();
                }
                else if (lblvalue.Text == "1")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.Sales.SearchLocations(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "1";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }

                else if (lblvalue.Text == "Group_Sale")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Group_Sale);
                    DataTable result = CHNLSVC.CommonSearch.GetGroupSaleSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "Group_Sale";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "BuyBackItem")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BuyBackItem);
                    DataTable result = CHNLSVC.CommonSearch.SearchBuyBackItem(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "BuyBackItem";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "InvoiceWithDate")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
                    DataTable result = new DataTable();
                    if (CheckPCTempSave() && SoTempPending.Checked)
                    {
                        result = CHNLSVC.CommonSearch.SearchSalesOrderWeb(SearchParams, "PENDING", txtSearchbywordD.Text);
                    }
                    else
                    {
                        result = CHNLSVC.CommonSearch.SearchSalesOrderWeb(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text);
                    }
                    grdResultD.DataSource = result;
                    grdResultD.DataBind();
                    lblvalue.Text = "InvoiceWithDate";
                    ViewState["SEARCH"] = result;
                    UserDPopoup.Show();
                    Session["DPopup"] = "DPopup";
                    txtSearchbywordD.Focus();
                }
                else if (lblvalue.Text == "CustomerDel")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "CustomerDel";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "LocationDel")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "LocationDel";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "Town")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                    DataTable result = CHNLSVC.Sales.SearchLocations(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "Town";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtPerTown.Focus();
                }
                else if (lblvalue.Text == "DocNo")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                    DataTable result = CHNLSVC.CommonSearch.Get_DocNum_ByRefNo(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "DocNo";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtPerTown.Focus();
                }
                else if (lblvalue.Text == "Customer_DEL")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "Customer_DEL";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                else if (lblvalue.Text == "SalesOrderNew")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
                    DataTable result = CHNLSVC.Sales.SearchSalesOrder(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "SalesOrderNew";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show();
                    Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                //else if (ViewState["SEARCH"] != null)
                //{
                //    DataTable result = (DataTable)ViewState["SEARCH"];
                //    DataView dv = new DataView(result);
                //    string searchParameter = ddlSearchbykey.Text;

                //    dv.RowFilter = "" + ddlSearchbykey.Text + " like '%" + txtSearchbyword.Text + "%'";

                //    if (dv.Count > 0)
                //    {
                //        result = dv.ToTable();
                //    }
                //    grdResult.DataSource = result;
                //    grdResult.DataBind();
                //    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                //    txtSearchbyword.Focus();
                //}
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }



        #endregion

        private void LoadHDD(string _req)
        {

            DataTable dtHeaders = CHNLSVC.CommonSearch.SearchPRNHeader(_req);
            if (dtHeaders.Rows.Count > 0)
            {
                try
                {
                    foreach (DataRow item in dtHeaders.Rows)
                    {
                        //  cmbExecutive.SelectedValue = item[30].ToString();
                        txtexcutive.Text = item[30].ToString();
                        cmbInvType.SelectedValue = item[4].ToString();
                        txtManualRefNo.Text = item[6].ToString();
                        txtRemarks.Text = item[12].ToString();
                        string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                        DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, "Code", "%" + txtexcutive.Text);
                        if (result.Rows.Count == 1)
                        {
                            lblSalesEx.Text = result.Rows[0][1].ToString();
                        }

                    }
                }
                catch (Exception ex)
                {
                    DisplayMessage("Sales executer invalid ", 1);
                }
            }

            for (int i = 0; i < gvInvoiceItem.Rows.Count; i++)
            {
                GridViewRow dr = gvInvoiceItem.Rows[i];
                LinkButton btnAddSerials = dr.FindControl("lbtnadReq") as LinkButton;
                btnAddSerials.Visible = true;
                Session["_isOrderbase"] = true;
            }

        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {
            try
            {
                MPPV.Show();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void LinkButton10_Click(object sender, EventArgs e)
        {
            try
            {
                mpBuyBack.Show();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtncode_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtexcutive.Text))
                {
                    DisplayMessage("Please select sales executive", 1);
                    return;
                }

                //if (IsAllovcateCustomer(txtexcutive.Text))
                //{
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerBYsalesExe(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                if (result != null)
                {
                    if (result.Rows.Count > 0)
                    {
                        grdResult.DataSource = result;
                        grdResult.DataBind();
                        lblvalue.Text = "Customer_13";
                        BindUCtrlDDLData(result);
                        ViewState["SEARCH"] = result;
                        SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                        txtSearchbyword.Text = "";
                        txtSearchbyword.Focus();
                        return;
                    }
                    else
                    {
                        ViewState["SEARCH"] = null;
                        txtSearchbyword.Text = string.Empty;
                        string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                        DataTable result2 = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams2, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                        grdResult.DataSource = result2;
                        grdResult.DataBind();
                        lblvalue.Text = "13";
                        BindUCtrlDDLData(result2);
                        ViewState["SEARCH"] = result2;
                        SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                        txtSearchbyword.Text = "";
                        txtSearchbyword.Focus();
                    }

                }
                else
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result2 = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams2, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    grdResult.DataSource = result2;
                    grdResult.DataBind();
                    lblvalue.Text = "13";
                    BindUCtrlDDLData(result2);
                    ViewState["SEARCH"] = result2;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Text = "";
                    txtSearchbyword.Focus();
                }
                if (Session["UserCompanyCode"].ToString() == "AAL")
                {
                    ddlSearchbykey.SelectedIndex = ddlSearchbykey.Items.IndexOf(ddlSearchbykey.Items.FindByText("Name"));
                }
                //}





            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnSearch_NIC_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.NIC);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "32";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnSearch_Mobile_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Mobile);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "33";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnSearch_Loyalty_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCustomer.Text))
                {
                    DisplayMessage("Please select customer code !!!");
                    lbtncode.Focus();
                    return;
                }

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable result = CHNLSVC.CommonSearch.SearchLoyaltyCard(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "168";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                txtSearchbyword.Text = "";
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "81";
                BindUCtrlDDLData(result);
                ddlSearchbykey.SelectedIndex = ddlSearchbykey.Items.IndexOf(ddlSearchbykey.Items.FindByText("Model"));
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void btnSearch_Serial_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPerTown.Text == "")
                {
                    DisplayMessage("Please select dispatch location", 1);
                    txtCustomer.Text = string.Empty;
                    return;
                }
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                DataTable result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "76";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
                txtSearchbyword.Focus();


            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtCustomer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtexcutive.Text))
                {
                    DisplayMessage("Please select sales executive", 1);
                    txtCustomer.Text = string.Empty;
                    return;
                }
                // txtCusName.Text = txtCustomer.Text.ToUpper();

                if (IsAllovcateCustomer(txtexcutive.Text))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerBYsalesExe(SearchParams, "Code", txtCustomer.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                    if (result.Rows.Count == 0)
                    {
                        DisplayMessage("This customer not allow for sales executive", 1);
                        ClearCustomer(true);
                        return;
                    }
                }
                LoadCustomerDetailsByCustomer();



                if (!string.IsNullOrEmpty(txtCustomer.Text))
                {
                    txtdelcuscode.Text = txtCustomer.Text;
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtLoyalty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtLoyalty.Text)) return;

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable _result = CHNLSVC.CommonSearch.SearchLoyaltyCard(SearchParams, null, null);
                if (_result != null) if (_result.Rows.Count > 0)
                    {
                        var _results = _result.AsEnumerable().ToList().Where(x => x.Field<string>("Card No") == txtLoyalty.Text.Trim()).ToList();
                        if (_results == null || _results.Count <= 0)
                        {
                            DisplayMessage("Please check the loyalty card");
                            txtLoyalty.Text = string.Empty;
                            txtLoyalty.Focus();
                            return;
                        }
                        else
                        {
                            string _tem = _results.AsEnumerable().Select(x => x.Field<string>("Type")).ToList()[0];
                            _loyaltyType = CHNLSVC.Sales.GetLoyaltyType(_tem);
                            if (_loyaltyType == null)
                            {
                                //divalert.Visible = true;
                                DisplayMessage("Loyalty Card Type not found");
                                txtLoyalty.Text = string.Empty;
                            }
                        }
                    }
                SetLoyalityColor();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void lbtncurrency_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Currency);
                DataTable result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "14";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnsupplier_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    DivsHide();
            //    ViewState["SEARCH"] = null;
            //    txtSearchbyword.Text = string.Empty;
            //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
            //    DataTable result = CHNLSVC.Sales.SearchSalesOrder(SearchParams, null, null);
            //    grdResult.DataSource = result;
            //    grdResult.DataBind();
            //    lblvalue.Text = "421";
            //    BindUCtrlDDLData(result);
            //    ViewState["SEARCH"] = result;
            //    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
            //    txtSearchbyword.Focus();
            //}
            //catch (Exception ex)
            //{
            //    //divalert.Visible = true;
            //    DisplayMessage(ex.Message, 4);
            //}

            try
            {
                if (Regex.IsMatch(txtFDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
                {

                }
                else
                {
                    txtFDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    string _Msg = "Please enter valid date.";
                    DisplayMessage(_Msg, 1);
                }
                if (Regex.IsMatch(txtTDate.Text, "^(0?[1-9]|[12][0-9]|3[01])/(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)/(19|20)\\d\\d$"))
                {

                }
                else
                {
                    txtTDate.Text = DateTime.Now.Date.ToString("dd/MMM/yyyy");
                    string _Msg = "Please enter valid date.";
                    DisplayMessage(_Msg, 1);
                }
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
                DataTable result = new DataTable();
                if (CheckPCTempSave() && SoTempPending.Checked)
                {
                    result = CHNLSVC.CommonSearch.SearchSalesOrderWeb(SearchParams, "PENDING", null);
                }
                else
                {
                    result = CHNLSVC.CommonSearch.SearchSalesOrderWeb(SearchParams, null, null);
                }
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "InvoiceWithDate";
                BindUCtrlDDLData2(result);
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnadditems_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLineTotAmt.Text == "0")
                {
                    DisplayMessage("You cannot add zero price to the list", 1);
                    txtQty.Focus();
                    return;
                }
                if (txtSerialNo.Text != "")
                {
                    chkDeliverLater.Checked = false;
                    chkDeliverNow.Checked = false;
                }
                if (_priceBookLevelRef.Sapl_is_serialized)
                {
                    chkDeliverNow.Checked = true;
                }
                if (chkQuotation.Checked)
                {
                    DisplayMessage("You are not allow to add additional items for the selected quotation !!!", 1);
                    return;
                }
                //block item
                List<MasterCompanyItem> _comitm = new List<MasterCompanyItem>();
                _comitm = CHNLSVC.General.GetComItem(txtItem.Text);
                if (_comitm == null)
                {
                    DisplayMessage("Invalid Item. Please Contact Inventory dept", 1);
                    return;
                }
                if (_comitm.Count > 0)
                {
                    var _filter = _comitm.FirstOrDefault(x => x.Msi_restric_inv_tp == cmbInvType.SelectedValue && x.Mci_act == true);
                    if (_filter != null)
                    {
                        DisplayMessage("Prohibited the Item by Inventory dept.Please Contact Inventory dept", 1);
                        return;
                    }

                }
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    DisplayMessage("Select a item code", 1);
                    txtItem.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    DisplayMessage("Enter quantity", 1);
                    txtQty.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPerTown.Text))
                {
                    DisplayMessage("Enter dispatch location", 1);
                    txtQty.Focus();
                    return;
                }
                #region add validation for reservation by lakshan 03 Mar 2017
                if (!string.IsNullOrEmpty(txtresno.Text))
                {
                    string _err = ValidateReservationNo();
                    if (!string.IsNullOrEmpty(_err))
                    {
                        DispMsg(_err); return;
                    }
                }
                #endregion
                #region reservation
                //if (!string.IsNullOrEmpty(txtresno.Text))
                //{
                //    List<INR_RES_DET> oINR_RES_DETs = CHNLSVC.Sales.GET_RESERVATION_DET(0, txtresno.Text);

                //    DataTable dt = GlobalMethod.ToDataTable(oINR_RES_DETs);

                //    if (oINR_RES_DETs != null && oINR_RES_DETs.Count > 0)
                //    {
                //        INR_RES_DET oINR_RES_DET = oINR_RES_DETs.Find(x => x.IRD_ITM_CD == txtItem.Text && x.IRD_ITM_STUS == cmbStatus.SelectedValue.ToString() && x.IRD_RES_BQTY > x.Ird_so_mrn_bqty);
                //        if (oINR_RES_DET != null && oINR_RES_DET.IRD_RES_NO != null)
                //        {
                //            decimal UsedQty = 0;
                //            decimal balance = 0;
                //            balance = oINR_RES_DET.IRD_RES_QTY - oINR_RES_DET.Ird_so_mrn_bqty;
                //            List<InvoiceItem> oSaveDInvoiceItem = CHNLSVC.Sales.GET_INV_ITM_BY_RESNO_LINE(txtresno.Text, oINR_RES_DET.IRD_LINE);
                //            if (oSaveDInvoiceItem != null && oSaveDInvoiceItem.Count > 0)
                //            {
                //                UsedQty = oSaveDInvoiceItem.Sum(x => x.Sad_qty);
                //            }
                //            if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                //            {
                //                UsedQty = UsedQty + _invoiceItemList.Where(x => x.Sad_res_no == oINR_RES_DET.IRD_RES_NO && x.Sad_res_line_no == oINR_RES_DET.IRD_LINE).Sum(x => x.Sad_qty);
                //            }
                //            if (balance <= UsedQty || balance < Convert.ToDecimal(txtQty.Text))
                //            {
                //                string _msg = "Cannot exceed reserved quantity-" + balance;
                //                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('"+_msg+"')", true);
                //                lblSelectRevervation.Text = "";
                //                txtresno.Text = "";
                //                //mpReservations.Show();
                //                return;
                //            }
                //            lblSelectRevervation.Text = txtresno.Text;
                //            lblSelectRevLine.Text = oINR_RES_DET.IRD_LINE.ToString();
                //            //DisplayMessageJS("Successfully reservation added.");
                //           // mpReservations.Hide();
                //           // return;
                //        }
                //        else
                //        {
                //            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot find a reservation for selected item and status or exceed qty !!!')", true);
                //            lblSelectRevervation.Text = "";
                //            txtresno.Text = "";
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot find a reservation details !!!')", true);
                //        lblSelectRevervation.Text = "";
                //        txtresno.Text = "";
                //        return;
                //    }
                //}
                #endregion
                #region add validation for allocation data chk 02 Nov 2016
                MasterProfitCenter _mstProNew = new MasterProfitCenter();
                _mstProNew = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                if (_mstProNew != null)
                {
                    if (_mstProNew.Mpc_tp == "P")
                    {
                        decimal _allAllocationQty = 0;
                        decimal _chnlAllocationQty = 0;
                        decimal _allInvBal = 0;
                        decimal _reqAppQty = 0;
                        decimal _tmpDecimal = 0;
                        List<InventoryAllocateDetails> _allAllocation = new List<InventoryAllocateDetails>();
                        List<InventoryAllocateDetails> _chnlAllocation = new List<InventoryAllocateDetails>();
                        //  InventoryLocation _inrLocBal = new InventoryLocation();
                        decimal _inrLocBal = 0;
                        _reqAppQty = decimal.TryParse(txtQty.Text, out _tmpDecimal) ? Convert.ToDecimal(txtQty.Text) : 0;
                        _allAllocation = CHNLSVC.Inventory.GET_INR_STOCK_ALOC_DATA(new InventoryAllocateDetails
                        {
                            Isa_com = Session["UserCompanyCode"].ToString(),
                            Isa_itm_cd = txtItem.Text
                            //Isa_itm_stus = item.Itri_itm_stus
                        });
                        if (_allAllocation.Count > 0)
                        {
                            _allAllocationQty = _allAllocation.Sum(c => c.Isa_aloc_bqty);
                        }
                        if (_allAllocationQty > 0)
                        {
                            _chnlAllocation = CHNLSVC.Inventory.GET_INR_STOCK_ALOC_DATA(new InventoryAllocateDetails
                            {
                                Isa_chnl = _mstProNew.Mpc_chnl,
                                Isa_com = Session["UserCompanyCode"].ToString(),
                                Isa_itm_cd = txtItem.Text
                                // Isa_itm_stus = item.Itri_itm_stus
                            });
                            if (_chnlAllocation.Count > 0)
                            {
                                _chnlAllocationQty = _chnlAllocation.Sum(c => c.Isa_aloc_bqty);
                            }

                            _inrLocBal = CHNLSVC.Inventory.GET_INR_LOC_BALANCE_BY_COM(new InventoryLocation()
                            {
                                Inl_com = Session["UserCompanyCode"].ToString(),
                                // Inl_loc = txtPerTown.Text,
                                Inl_itm_cd = txtItem.Text,
                                Inl_itm_stus = cmbStatus.SelectedValue
                            });
                            //if (_inrLocBal != null)
                            //{
                            _allInvBal = _inrLocBal;//_inrLocBal.Inl_free_qty;
                            //}
                            List<InventoryRequestItem> _reqItemDetails = ViewState["_ApproveItem"] as List<InventoryRequestItem>;
                            if (_reqItemDetails != null)
                            {
                                if (_reqItemDetails.Count > 0)
                                {
                                    //_reqAppQty =  _reqAppQty + _reqItemDetails.Sum(c=> c.Itri_qty);
                                }
                            }
                            if (_reqAppQty > _chnlAllocationQty)
                            {
                                decimal _availableBalanc = _chnlAllocationQty + (_allInvBal - _allAllocationQty);
                                if (_reqAppQty > _availableBalanc)
                                {
                                    if (_availableBalanc > -1)
                                    {
                                        DisplayMessage("You cannot exceed the allocation qty. Available Balance : " + (_allInvBal - _allAllocationQty + _chnlAllocationQty));

                                        return;
                                    }
                                    else
                                    {
                                        DisplayMessage("You cannot exceed the allocation qty. Available Balance : " + _chnlAllocationQty);

                                        return;
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                }
                #endregion

                string _itemSerializedStatus = Session["_itemSerializedStatus"].ToString();
                if ((_itemSerializedStatus == "1") || (_itemSerializedStatus == "0"))
                {
                    decimal number = Convert.ToDecimal(txtQty.Text);
                    decimal fractionalPart = number % 1;
                    if (fractionalPart != 0)
                    {
                        DisplayMessage("only allow numeric value", 2);
                        return;
                    }


                }
                if (_priceBookLevelRef != null)
                {
                    if (_priceBookLevelRef.Sapl_currency_cd != txtcurrency.Text.Trim())
                    {
                        DisplayMessage("You are not allow to add  items for the selected Price level.currency mismatch", 1);
                        return;
                    }

                }
                #region Check Customer has promotion voucher avoid the discount :: Chamal 04/Jul/2014

                if (!string.IsNullOrEmpty(txtCustomer.Text))
                {
                    if (txtCustomer.Text != "CASH")
                    {
                        if ((string.IsNullOrEmpty(txtDisRate.Text) && string.IsNullOrEmpty(txtDisAmt.Text)))
                        {
                            CashGeneralEntiryDiscountDef _discVou = CHNLSVC.Sales.CheckCustHaveDiscountPromoVoucher(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCustomer.Text, Convert.ToDateTime(txtdate.Text).Date, cmbBook.Text, cmbLevel.Text, txtItem.Text.Trim(), string.Empty, txtNIC.Text, txtMobile.Text);
                            if (_discVou != null)
                            {
                                //DisplayMessage("Promotion voucher discount available for this item", 1);
                                DisplayMessageJS("Promotion voucher discount available for this item");
                                //return;
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(txtDisRate.Text) <= 0 && Convert.ToDecimal(txtDisAmt.Text) <= 0)
                            {
                                CashGeneralEntiryDiscountDef _discVou = CHNLSVC.Sales.CheckCustHaveDiscountPromoVoucher(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCustomer.Text, Convert.ToDateTime(txtdate.Text).Date, cmbBook.Text, cmbLevel.Text, txtItem.Text.Trim(), string.Empty, txtNIC.Text, txtMobile.Text);
                                if (_discVou != null)
                                {
                                    //DisplayMessage("Promotion voucher discount available for this item", 1);
                                    DisplayMessageJS("Promotion voucher discount available for this item");
                                    //return;
                                }
                            }
                        }
                    }
                    else
                    {
                        _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, Session["UserCompanyCode"].ToString());
                    }
                }

                #endregion Check Customer has promotion voucher avoid the discount :: Chamal 04/Jul/2014

                if (_priceBookLevelRef.Sapl_is_serialized == false)
                {
                    if (!string.IsNullOrEmpty(SSPromotionCode))
                    {
                        List<PriceDetailRef> _promoList = CHNLSVC.Sales.GetPriceByPromoCD(SSPromotionCode);
                        if (_promoList == null || _promoList.Count == 0)
                        {
                            return;
                        }
                        else
                        {
                            decimal qty = _promoList[0].Sapd_qty_to;
                            List<InvoiceItem> _alredyAddList = (from _res in _invoiceItemList
                                                                where _res.Sad_itm_cd == txtItem.Text.ToUpper().Trim() && _res.Sad_itm_stus == cmbStatus.SelectedValue.ToString()
                                                                select _res).ToList<InvoiceItem>();
                            if (_alredyAddList != null)
                            {
                                qty = qty + _alredyAddList.Count;
                            }
                            if (Convert.ToDecimal(txtQty.Text) > qty)
                            {
                                DisplayMessage("Invoice quantity exceed promotion allowed quantity !!!", 1);
                                return;
                            }
                        }
                    }
                }

                List<MasterItemComponent> _com = CHNLSVC.Inventory.GetItemComponents(txtItem.Text.Trim());
                if (_com != null && _com.Count > 0)
                {
                    foreach (MasterItemComponent _itmCom in _com)
                    {
                        MasterItem _temItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itmCom.ComponentItem.Mi_cd);
                        if (_isRegistrationMandatory)
                        {
                            if (_temItm.Mi_need_reg)
                            {
                                _isNeedRegistrationReciept = true;
                            }
                        }
                    }
                }
                else
                {
                    MasterItem _temItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                    if (_isRegistrationMandatory)
                    {
                        if (_temItm.Mi_need_reg)
                        {
                            _isNeedRegistrationReciept = true;
                        }
                    }
                }

                AddItem(SSPromotionCode == "0" || string.IsNullOrEmpty(SSPromotionCode) ? false : true, string.Empty);
                if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                {
                    if (_priceDetailRef[0].Sapd_customer_cd == txtCustomer.Text.ToUpper().Trim())
                    {
                        txtCustomer.ReadOnly = true;
                        lbtncode.Enabled = false;
                    }
                }
                AddItemDisableCustomer(true);

                bool _isOrderbase = (bool)Session["_isOrderbase"];
                if (_isOrderbase == true)
                {
                    for (int i = 0; i < gvInvoiceItem.Rows.Count; i++)
                    {
                        GridViewRow dr = gvInvoiceItem.Rows[i];
                        LinkButton btnAddSerials = dr.FindControl("lbtnadReq") as LinkButton;
                        btnAddSerials.Visible = true;
                    }
                }
                //bool isvalidateitm = ValidateSOItems();
                //if (isvalidateitm == false)
                //{
                //    return;
                //}

                //InsertToItemsGrid();
                //if (!string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                //{
                //    InsertToSerialsGrid();
                //}


                ////LoadPriceDefaultValue();
                _isInventoryCombineAdded = false;
                txtQty.Text = "1";

                //mpAddNewItem.Show();

                //txtItem.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        protected void gvInvoiceItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //string item = e.Row.Cells[1].Text;
                    //foreach (LinkButton button in e.Row.Cells[e.Row.Cells.Count].Controls.OfType<LinkButton>())
                    //{
                    //    if (button.CommandName == "Delete")
                    //    {
                    //        button.Attributes["onclick"] = "if(!confirm('Do you want to delete item " + item + "?')){ return false; };";
                    //    }
                    //}

                    //if (e.Row.RowType == DataControlRowType.DataRow)
                    //{
                    //    e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvInvoiceItem, "Select$" + e.Row.RowIndex);
                    //    e.Row.Attributes["style"] = "cursor:pointer";
                    //}
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void gvInvoiceItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtNIC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCustomerDetailsByNIC();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtMobile_TextChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCustomerDetailsByMobile();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtnloc_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.Sales.SearchLocations(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "1";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtQuotation_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuotation.Text.Trim())) return;
            try
            {
                QuotationHeader _saveHdr = CHNLSVC.Sales.Get_Quotation_HDR(txtQuotation.Text);
                if (_saveHdr != null && !string.IsNullOrEmpty(_saveHdr.Qh_no))
                {
                    if (_saveHdr.Qh_stus == "D")
                    {
                        DisplayMessage("This quotation is already used.");
                        txtQuotation.Text = "";
                        return;
                    }
                    txtCustomer.Text = _saveHdr.Qh_del_cuscd;
                    txtexcutive.Text = _saveHdr.Qh_sales_ex;
                    loadExname(txtexcutive.Text);
                    txtCustomer_TextChanged(null, null);
                }

                _invoiceItemList = CHNLSVC.Sales.GetQuotationDetail(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtQuotation.Text.Trim(), Convert.ToDateTime(txtdate.Text));
                //get serial
                List<QuotationSerial> _serialList = CHNLSVC.Sales.GetQuoSerials(txtQuotation.Text.Trim());
                if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                {
                    _invoiceItemList.ForEach(X => X.Sad_job_line = X.Sad_itm_line);

                    #region Check For Inventory Balance if Delivered Now

                    if (1 == 1)
                    {
                        bool _isPricelevelallowforDOanystatus = false;
                        string _balanceexceedList = string.Empty;
                        foreach (InvoiceItem _itm in _invoiceItemList)
                        {
                            //------------------------------------------------------------------------------------------------
                            if (!string.IsNullOrEmpty(_itm.Sad_pbook) && !string.IsNullOrEmpty(_itm.Sad_pb_lvl))
                            {
                                List<PriceBookLevelRef> _lvl = CHNLSVC.Sales.GetPriceLevelList(Session["UserCompanyCode"].ToString(), _itm.Sad_pbook, _itm.Sad_pb_lvl);
                                if (_lvl != null)
                                    if (_lvl.Count > 0)
                                    {
                                        var _bool = from _l in _lvl where _l.Sapl_chk_st_tp == true select _l.Sapl_chk_st_tp;
                                        if (_bool != null)
                                            if (_bool.Count() > 0)
                                                _isPricelevelallowforDOanystatus = false;
                                            else
                                                _isPricelevelallowforDOanystatus = true;
                                        else
                                            _isPricelevelallowforDOanystatus = true;
                                    }
                            }
                            else
                                _isPricelevelallowforDOanystatus = true;

                            //------------------------------------------------------------------------------------------------
                            decimal _pickQty = 0;
                            if (_isPricelevelallowforDOanystatus)
                                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _itm.Sad_itm_cd).ToList().Select(x => x.Sad_qty).Sum();
                            else
                                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _itm.Sad_itm_cd && x.Mi_itm_stus == _itm.Mi_itm_stus).ToList().Select(x => x.Sad_qty).Sum();

                            _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                            List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Sad_itm_cd, _itm.Mi_itm_stus);

                            if (_inventoryLocation != null && _inventoryLocation.Count > 0)
                            {
                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                if (_pickQty > _invBal)

                                    if (string.IsNullOrEmpty(_balanceexceedList))
                                        _balanceexceedList = _itm.Sad_itm_cd;
                                    else
                                        _balanceexceedList = ", " + _itm.Sad_itm_cd;
                            }
                            else
                                if (string.IsNullOrEmpty(_balanceexceedList))
                                    _balanceexceedList = _itm.Sad_itm_cd;
                                else
                                    _balanceexceedList = ", " + _itm.Sad_itm_cd;
                        }

                        if (!string.IsNullOrEmpty(_balanceexceedList))
                        {
                            _invoiceItemList = new List<InvoiceItem>();
                            ScanSerialList = new List<ReptPickSerials>();
                            InvoiceSerialList = new List<InvoiceSerial>();
                            DisplayMessage("Item(s) inventory balance exceeds");
                            return;
                        }
                        //InvItm_SerialAdd.Visible = true;
                    }

                    #endregion Check For Inventory Balance if Delivered Now

                    GrndSubTotal = 0;
                    GrndDiscount = 0;
                    GrndTax = 0;

                    foreach (InvoiceItem itm in _invoiceItemList)
                    {
                        CalculateGrandTotal(itm.Sad_qty, itm.Sad_unit_rt, itm.Sad_disc_amt, itm.Sad_itm_tax_amt, true);
                        _lineNo += 1;
                        SSCombineLine += 1;
                    }

                    var _invlst = new BindingList<InvoiceItem>(_invoiceItemList);
                    gvInvoiceItem.DataSource = setItemDescriptions(_invoiceItemList);
                    gvInvoiceItem.DataBind();

                    //for (int i = 0; i < gvInvoiceItem.Rows.Count; i++)
                    //{
                    //    GridViewRow dr = gvInvoiceItem.Rows[i];
                    //    LinkButton btnAddSerials = dr.FindControl("btnAddSerials") as LinkButton;
                    //    btnAddSerials.Visible = true;
                    //}

                    ScanSerialList = new List<ReptPickSerials>();
                    string _defbin = CHNLSVC.Inventory.GetDefaultBinCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

                    foreach (InvoiceItem _itm in _invoiceItemList)
                    {
                        MasterItem _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Sad_itm_cd);
                        if (_item.Mi_is_ser1 == 0)
                        {
                            List<ReptPickSerials> _nonserLst = null;
                            if (IsPriceLevelAllowDoAnyStatus == false)
                                _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Sad_itm_cd, _itm.Sad_itm_stus, Convert.ToDecimal(_itm.Sad_qty));
                            else
                                _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Sad_itm_cd, string.Empty, Convert.ToDecimal(_itm.Sad_qty));
                            _nonserLst.ForEach(x => x.Tus_base_doc_no = Convert.ToString(-100));
                            _nonserLst.ForEach(x => x.Tus_base_itm_line = _itm.Sad_itm_line);
                            _nonserLst.ForEach(x => x.Tus_usrseq_no = -100);
                            _nonserLst.ForEach(x => x.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim()));
                            _nonserLst.ForEach(x => x.Tus_serial_id = string.Empty);
                            _nonserLst.ForEach(x => x.Tus_ser_id = -1);
                            _nonserLst.ForEach(x => x.Tus_new_status = string.Empty);
                            ScanSerialList.AddRange(_nonserLst);
                        }
                        else if (_item.Mi_is_ser1 == -1)
                        {
                            ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                            _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                            _reptPickSerial_.Tus_base_doc_no = Convert.ToString(-100);
                            _reptPickSerial_.Tus_base_itm_line = _itm.Sad_itm_line;
                            _reptPickSerial_.Tus_bin = _defbin;
                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_cre_dt = DateTime.Now;
                            _reptPickSerial_.Tus_cross_batchline = 0;
                            _reptPickSerial_.Tus_cross_itemline = 0;
                            _reptPickSerial_.Tus_cross_seqno = 0;
                            _reptPickSerial_.Tus_cross_serline = 0;
                            _reptPickSerial_.Tus_doc_dt = Convert.ToDateTime(txtdate.Text);
                            _reptPickSerial_.Tus_doc_no = string.Empty;
                            _reptPickSerial_.Tus_exist_grncom = string.Empty;
                            _reptPickSerial_.Tus_isapp = 1;
                            _reptPickSerial_.Tus_iscovernote = 1;
                            _reptPickSerial_.Tus_itm_brand = _itm.Mi_brand;
                            _reptPickSerial_.Tus_itm_cd = _itm.Sad_itm_cd;
                            _reptPickSerial_.Tus_itm_desc = _itm.Mi_longdesc;
                            _reptPickSerial_.Tus_itm_line = 0;
                            _reptPickSerial_.Tus_itm_model = _itm.Mi_model;
                            _reptPickSerial_.Tus_itm_stus = _itm.Sad_itm_stus;
                            _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                            _reptPickSerial_.Tus_new_status = string.Empty;
                            _reptPickSerial_.Tus_qty = _itm.Sad_qty;
                            _reptPickSerial_.Tus_ser_1 = "N/A";
                            _reptPickSerial_.Tus_ser_2 = "N/A";
                            _reptPickSerial_.Tus_ser_id = 0;
                            _reptPickSerial_.Tus_ser_line = 0;
                            _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();
                            _reptPickSerial_.Tus_unit_cost = 0;
                            _reptPickSerial_.Tus_unit_price = 0;
                            _reptPickSerial_.Tus_usrseq_no = -100;
                            _reptPickSerial_.Tus_warr_no = "N/A";
                            _reptPickSerial_.Tus_warr_period = 0;
                            _reptPickSerial_.Tus_new_remarks = string.Empty;
                            ScanSerialList.Add(_reptPickSerial_);
                        }
                        else
                        {
                            if (_serialList != null && _serialList.Count > 0)
                            {
                                List<QuotationSerial> _itmSerial = (from _res in _serialList
                                                                    where _res.Qs_item == _itm.Sad_itm_cd && _res.Qs_main_line == _itm.Sad_itm_line
                                                                    select _res).ToList<QuotationSerial>();
                                if (_itmSerial != null && _itmSerial.Count > 0)
                                {
                                    List<InventorySerialRefN> _invSerials = CHNLSVC.Inventory.GetSerialByID(_itmSerial[0].Qs_ser_id.ToString(), Session["UserDefLoca"].ToString());
                                    if (_invSerials == null && _invSerials.Count <= 0)
                                    {
                                        DisplayMessage("Quotation serial id not found on inventory.SERIAL ID - " + _itmSerial[0].Qs_ser_id);
                                        return;
                                    }

                                    _invSerials = (from _res in _invSerials
                                                   where _res.Ins_available == -1 || _res.Ins_available == 1 // added by Nadeeka
                                                   select _res).ToList<InventorySerialRefN>();
                                    if (_invSerials != null && _invSerials.Count > 0)
                                    {
                                        ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                        _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                        _reptPickSerial_.Tus_base_doc_no = Convert.ToString(-100);
                                        _reptPickSerial_.Tus_base_itm_line = _itm.Sad_itm_line;
                                        _reptPickSerial_.Tus_bin = _invSerials[0].Ins_bin;
                                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                        _reptPickSerial_.Tus_cre_dt = DateTime.Now;
                                        _reptPickSerial_.Tus_cross_batchline = _invSerials[0].Ins_cross_batchline;
                                        _reptPickSerial_.Tus_cross_itemline = _invSerials[0].Ins_cross_itmline;
                                        _reptPickSerial_.Tus_cross_seqno = _invSerials[0].Ins_cross_seqno;
                                        _reptPickSerial_.Tus_cross_serline = _invSerials[0].Ins_cross_serline;
                                        _reptPickSerial_.Tus_doc_dt = _invSerials[0].Ins_doc_dt;
                                        _reptPickSerial_.Tus_doc_no = _invSerials[0].Ins_doc_no;
                                        _reptPickSerial_.Tus_exist_grncom = _invSerials[0].Ins_exist_grncom;
                                        _reptPickSerial_.Tus_isapp = 1;
                                        _reptPickSerial_.Tus_iscovernote = 1;
                                        _reptPickSerial_.Tus_itm_brand = _itm.Mi_brand;
                                        _reptPickSerial_.Tus_itm_cd = _itm.Sad_itm_cd;
                                        _reptPickSerial_.Tus_itm_desc = _itm.Mi_longdesc;
                                        _reptPickSerial_.Tus_itm_line = _itm.Sad_itm_line;
                                        _reptPickSerial_.Tus_itm_model = _itm.Mi_model;
                                        _reptPickSerial_.Tus_itm_stus = _itm.Sad_itm_stus;
                                        _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                        _reptPickSerial_.Tus_new_status = string.Empty;
                                        _reptPickSerial_.Tus_qty = _itm.Sad_qty;
                                        _reptPickSerial_.Tus_ser_1 = _invSerials[0].Ins_ser_1;
                                        _reptPickSerial_.Tus_ser_2 = _invSerials[0].Ins_ser_2;
                                        _reptPickSerial_.Tus_ser_id = _invSerials[0].Ins_ser_id;
                                        _reptPickSerial_.Tus_ser_line = _invSerials[0].Ins_ser_line;
                                        _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();
                                        _reptPickSerial_.Tus_unit_cost = _invSerials[0].Ins_unit_cost;
                                        _reptPickSerial_.Tus_unit_price = _invSerials[0].Ins_unit_price;
                                        _reptPickSerial_.Tus_usrseq_no = -100;
                                        _reptPickSerial_.Tus_warr_no = _invSerials[0].Ins_warr_no;
                                        _reptPickSerial_.Tus_warr_period = _invSerials[0].Ins_warr_period;
                                        _reptPickSerial_.Tus_new_remarks = string.Empty;
                                        ScanSerialList.Add(_reptPickSerial_);
                                    }
                                }
                            }
                        }
                    }

                    foreach (ReptPickSerials item in ScanSerialList)
                    {
                        InvoiceSerial _invser = new InvoiceSerial();
                        _invser.Sap_del_loc = Session["UserDefLoca"].ToString();
                        _invser.Sap_itm_cd = item.Tus_itm_cd;
                        _invser.Sap_itm_line = item.Tus_itm_line;
                        _invser.Sap_remarks = string.Empty;

                        if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                        {
                            InvoiceItem oItem = _invoiceItemList.Find(x => x.Sad_itm_cd == item.Tus_itm_cd && x.Sad_itm_line == item.Tus_itm_line);
                            _invser.Sap_seq_no = oItem.Sad_seq;
                        }

                        _invser.Sap_ser_1 = item.Tus_ser_1;
                        _invser.Sap_ser_line = _isCombineAdding ? Convert.ToInt32(SSCombineLine) : 0;
                        InvoiceSerialList.Add(_invser);
                    }

                    var _serlst = new BindingList<ReptPickSerials>(ScanSerialList);
                    gvPopSerial.DataSource = _serlst;
                    gvPopSerial.DataSource = setSerialStatusDescriptions(ScanSerialList);
                    gvPopSerial.DataBind();

                    decimal _tobepays = 0;
                    if (lblSVatStatus.Text == "Available") _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                    else _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());


                    //ucPayModes1.TotalAmount = _tobepays;
                    //ucPayModes1.InvoiceItemList = _invoiceItemList;
                    //ucPayModes1.SerialList = InvoiceSerialList;
                    //ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays));
                    //ucPayModes1.IsTaxInvoice = chkTaxPayable.Checked;
                    //if (ucPayModes1.HavePayModes && _isCombineAdding == false)
                    //    ucPayModes1.LoadData();



                }
                else
                {
                    //divalert.Visible = true;
                    DisplayMessage("Invalid quotation number !!!");
                    _invoiceItemList = new List<InvoiceItem>();
                    var _nulllst = new BindingList<InvoiceItem>(_invoiceItemList);
                    gvInvoiceItem.DataSource = setItemDescriptions(_invoiceItemList);
                    gvInvoiceItem.DataBind();
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void cmbTechnician_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(Convert.ToString(cmbTechnician.SelectedValue)))
            //{
            //    if (_tblPromotor != null)
            //    {
            //        var _find = (from DataRow _l in _tblPromotor.Rows where _l.Field<string>("mpp_promo_name") == cmbTechnician.Text select _l).ToList();
            //        if (_find != null && _find.Count > 0)
            //        {
            //            txtPromotor.Text = Convert.ToString(cmbTechnician.SelectedValue);
            //        }
            //        else
            //        {
            //            //divalert.Visible = true;
            //            lblalert.Text = "Please select the correct sales promotor";
            //            txtPromotor.Text = string.Empty;
            //            cmbTechnician.SelectedIndex = 0;
            //        }
            //    }
            //}
            //else
            //{
            //    txtPromotor.Text = string.Empty;
            //    cmbTechnician.SelectedIndex = 0;
            //}
        }

        protected void gvPopSerial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string item = e.Row.Cells[1].Text;
                    foreach (LinkButton button in e.Row.Cells[8].Controls.OfType<LinkButton>())
                    {
                        if (button.CommandName == "Delete")
                        {
                            button.Attributes["onclick"] = "if(!confirm('Do you want to delete serial " + item + "?')){ return false; };";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void gvPopSerial_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //try
            //{
            //    int Myindex = Convert.ToInt32(e.RowIndex);
            //    DataTable dt = ViewState["SERIALSTABLE"] as DataTable;
            //    dt.Rows[Myindex].Delete();
            //    dt.AcceptChanges();
            //    ViewState["SERIALSTABLE"] = dt;
            //    BindSerialsGrid();
            //}
            //catch (Exception ex)
            //{
            //    //divalert.Visible = true;
            //    DisplayMessage(ex.Message, 4);
            //}
        }

        protected void txtSerialNo_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    CheckSerialAvailability();
            //}
            //catch (Exception ex)
            //{
            //    //divalert.Visible = true;
            //    DisplayMessage(ex.Message, 4);
            //}
        }

        protected void cmbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(Session["UserCompanyCode"].ToString(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                SetColumnForPriceDetailNPromotion(_priceBookLevelRef.Sapl_is_serialized);
                if (_priceBookLevelRef.Sapl_is_serialized && string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                {
                    DisplayMessage("You are going to select a serialized price level without serial.Please select the serial !!!");
                    txtSerialNo.Text = string.Empty;
                    return;
                }
                bool isJSEnd = false;
                CheckQty(false, out isJSEnd);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    CheckUnitPrice();
            //    if ((!string.IsNullOrEmpty(txtQty.Text.Trim())) && (!string.IsNullOrEmpty(txtUnitPrice.Text.Trim())))
            //    {
            //        CalCulateVal();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //divalert.Visible = true;
            //    DisplayMessage(ex.Message, 4);
            //}
            if (txtUnitPrice.ReadOnly) return;


            if (_IsVirtualItem)
            {
                CalculateItem();
                return;
            }

            try
            {

                if (_isCompleteCode && GeneralDiscount_new.SADD_IS_EDIT == 1 && Convert.ToDecimal(txtUnitPrice.Text.Trim()) > 0)
                {
                    DisplayMessage("Not allow price edit for com codes!");
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text)) return;
                if (IsNumeric(txtQty.Text) == false)
                {
                    DisplayMessage("Please select valid quantity");
                    return;
                }
                if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;

                if (_MasterProfitCenter.Mpc_without_price && _priceBookLevelRef.Sapl_is_without_p)
                {
                    if (string.IsNullOrEmpty(txtUnitPrice.Text)) txtUnitPrice.Text = FormatToCurrency("0");
                    decimal vals = Convert.ToDecimal(txtUnitPrice.Text);
                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(vals));
                    txtUnitPrice.Text = vals.ToString("N2");
                    CalculateItem();
                    return;
                }
                if (!_isCompleteCode)
                {
                    //check minus unit price validation
                    decimal _unitAmt = 0;
                    bool _isUnitAmt = Decimal.TryParse(txtUnitPrice.Text, out _unitAmt);
                    if (!_isUnitAmt)
                    {
                        DisplayMessage("Unit Price has to be number!");
                        return;
                    }
                    if (_unitAmt <= 0)
                    {
                        DisplayMessage("Unit Price has to be greater than 0!");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtUnitPrice.Text) && _isEditDiscount == false)
                    {
                        decimal _pb_price;
                        if (SSPriceBookPrice == 0)
                        {
                            DisplayMessage("Price not define. Please check the system updated price.");
                            txtUnitPrice.Text = FormatToCurrency("0");
                            return;
                        }
                        _pb_price = SSPriceBookPrice;
                        decimal _txtUprice = Convert.ToDecimal(txtUnitPrice.Text);
                        if (GeneralDiscount_new.SADD_IS_EDIT == 1)
                        {
                            if (_pb_price > _txtUprice)
                            {
                                decimal _diffPecentage = ((_pb_price - _txtUprice) / _pb_price) * 100;
                                if (_diffPecentage > GeneralDiscount_new.SADD_EDIT_RT)
                                {
                                    DisplayMessage("You cannot deduct price more than " + GeneralDiscount_new.SADD_EDIT_RT + "% from the price book price.");
                                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                                    txtUnitPrice.Text = _pb_price.ToString("N2");
                                    _isEditPrice = false;
                                    return;
                                }
                                else
                                {
                                    _isEditPrice = true;
                                }
                            }
                        }
                        else
                        {
                            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                            txtUnitPrice.Text = _pb_price.ToString("N2");
                            _isEditPrice = false;
                        }
                    }
                }
                if (string.IsNullOrEmpty(txtUnitPrice.Text)) txtUnitPrice.Text = FormatToCurrency("0");
                decimal val = Convert.ToDecimal(txtUnitPrice.Text);
                txtUnitPrice.Text = FormatToCurrency(Convert.ToString(val));
                txtUnitPrice.Text = val.ToString("N2");
                CalculateItem();
            }
            catch (Exception ex)
            {
                txtUnitPrice.Text = FormatToCurrency("0");
                CalculateItem();
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    if ((!string.IsNullOrEmpty(txtQty.Text.Trim())) && (!string.IsNullOrEmpty(txtUnitPrice.Text.Trim())))
            //    {
            //        CalCulateVal();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //divalert.Visible = true;
            //    DisplayMessage(ex.Message, 4);
            //}


            if (_IsVirtualItem) return;
            try
            {
                string _itemSerializedStatus = Session["_itemSerializedStatus"].ToString();
                if ((_itemSerializedStatus == "1") || (_itemSerializedStatus == "0"))
                {
                    decimal number = Convert.ToDecimal(txtQty.Text);
                    decimal fractionalPart = number % 1;
                    if (fractionalPart != 0)
                    {
                        DisplayMessage("only allow numeric value", 2);
                        return;
                    }


                }
                if (Convert.ToDecimal(txtQty.Text.Trim()) < 0)
                {
                    DisplayMessage("Quantity should be positive value.");
                    return;
                }
                if (!string.IsNullOrEmpty(txtresno.Text))
                {
                    decimal _resqty = Convert.ToDecimal(Session["RESQTY"]);
                    if (Convert.ToDecimal(txtQty.Text.Trim()) > _resqty)
                    {
                        DisplayMessage("Cannot exceed reservation  qty");
                        return;
                    }
                    string _err = ValidateReservationNo();
                    if (!string.IsNullOrEmpty(_err))
                    {
                        DispMsg(_err); return;
                    }
                }
                bool isEndfromJS = false;
                CheckQty(true, out isEndfromJS);

            }
            catch (Exception ex)
            {
                txtQty.Text = FormatToQty("1");
                DisplayMessage(ex.Message);
            }
        }

        protected void ddltown_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MpDeliveryShow();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        #region Delivery
        protected void lbtndconfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Session["DCUSCODE"] = txtdelcuscode.Text.Trim();
                Session["DCUSNAME"] = txtdelname.Text.Trim();
                Session["DTOWN"] = txtPerTown.Text.Trim();
                Session["DCUSADD1"] = txtdelad1.Text.Trim();
                Session["DCUSADD2"] = txtdelad2.Text.Trim();
                Session["DCUSLOC"] = txtdellocation.Text.Trim();//Session["UserDefLoca"].ToString();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtndreset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetDeliveryInstructionToOriginalCustomer();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

            MpDelivery.Show();
        }

        protected void lbtndclear_Click(object sender, EventArgs e)
        {
            try
            {
                txtdelad1.Text = string.Empty;
                txtdelad2.Text = string.Empty;
                txtdelcuscode.Text = string.Empty;
                txtdellocation.Text = string.Empty;
                txtdelname.Text = string.Empty;
                chkOpenDelivery.Checked = false;
                MpDelivery.Show();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void lbtndcancel_Click(object sender, EventArgs e)
        {
            try
            {
                MpDelivery.Hide();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void ResetDeliveryInstructionToOriginalCustomer()
        {
            txtdellocation.Text = Session["UserDefLoca"].ToString();
            txtdelcuscode.Text = txtCustomer.Text;
            txtdelname.Text = txtCusName.Text;
            txtdelad1.Text = txtAddress1.Text;
            txtdelad2.Text = txtAddress2.Text;
            chkOpenDelivery.Checked = false;
        }

        #endregion

        protected void btncClose_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerPopoup.Hide();
                Session["ucc"] = null;
                hdfShowCustomer.Value = null;
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void gvInvoiceItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //txtItem.Text = gvInvoiceItem.SelectedRow.Cells[1].Text;

                //lblItemDescription.Text = gvInvoiceItem.SelectedRow.Cells[2].Text;

                //if ((!string.IsNullOrEmpty(gvInvoiceItem.SelectedRow.Cells[11].Text)) && ((gvInvoiceItem.SelectedRow.Cells[11].Text != "&nbsp;")))
                //{
                //    cmbBook.SelectedValue = gvInvoiceItem.SelectedRow.Cells[11].Text;
                //}

                //if ((!string.IsNullOrEmpty(gvInvoiceItem.SelectedRow.Cells[12].Text)) && ((gvInvoiceItem.SelectedRow.Cells[12].Text != "&nbsp;")))
                //{
                //    cmbLevel.SelectedValue = gvInvoiceItem.SelectedRow.Cells[12].Text;
                //}

                cmbStatus.SelectedIndex = 0;

                //txtQty.Text = SetDecimalPoint(gvInvoiceItem.SelectedRow.Cells[4].Text);
                //txtUnitPrice.Text = SetDecimalPoint(gvInvoiceItem.SelectedRow.Cells[5].Text);
                //txtUnitAmt.Text = SetDecimalPoint(gvInvoiceItem.SelectedRow.Cells[6].Text);
                //txtDisRate.Text = SetDecimalPoint(gvInvoiceItem.SelectedRow.Cells[7].Text);
                //txtDisAmt.Text = SetDecimalPoint(gvInvoiceItem.SelectedRow.Cells[8].Text);
                //txtTaxAmt.Text = SetDecimalPoint(gvInvoiceItem.SelectedRow.Cells[9].Text);
                //txtLineTotAmt.Text = SetDecimalPoint(gvInvoiceItem.SelectedRow.Cells[10].Text);
                //lblItemDescription.Text = gvInvoiceItem.SelectedRow.Cells[2].Text;

                foreach (GridViewRow row in gvInvoiceItem.Rows)
                {
                    if (row.RowIndex == gvInvoiceItem.SelectedIndex)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                    }
                    else
                    {
                        row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    }
                }

                Session["RSEQNO"] = gvInvoiceItem.SelectedRow.Cells[14].Text;


            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }
        private void LoadDispatchDetails(string company, string loc, string item, string itm_status)
        {
            try
            {
                DataTable dtstatus = CHNLSVC.Sales.GetItemStatusVal(itm_status);

                string _itemval = string.Empty;

                foreach (DataRow itemval in dtstatus.Rows)
                {
                    _itemval = itemval["MIS_CD"].ToString();
                }

                DataTable dtitemsdata = CHNLSVC.Sales.GetWareHousetemsData(company, loc, item, itm_status);
                if (dtitemsdata.Rows.Count > 0)
                {
                    grvwarehouseitms.DataSource = null;
                    grvwarehouseitms.DataBind();

                    grvwarehouseitms.DataSource = dtitemsdata;
                    grvwarehouseitms.DataBind();
                }
                else
                {
                    grvwarehouseitms.DataSource = null;
                    grvwarehouseitms.DataBind();
                }
            }
            catch (Exception ex)
            {
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                DispMsg(ex.Message, "E");
            }
        }

        protected void dgvMultipleItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void dgvMultipleItems_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnMuliItemSelect_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblITEM = (Label)row.FindControl("lblITEM");
                string _item = lblITEM.Text;
                txtItem.Text = _item.Trim();
                txtQty.Text = "1";
                mpMultipleItems.Hide();
                if (LoadItemDetail(_item) == false)
                {
                    DisplayMessage("This item already inactive or invalid code. Please check with inventory dept.", 1);
                    txtItem.Text = "";
                    return;
                }

                bool isJSEnd = false;
                CheckQty(true, out isJSEnd);
                lbtnadditems.Focus();
            }

            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            if (Session["UserCompanyCode"].ToString() == "AAL")
            {
                GetCustomerDiscount();
            }
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                txtQty.Text = "1";
            }
            if (string.IsNullOrEmpty(txtSerialNo.Text))
            {
                txtQty.ReadOnly = false;
            }

            if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            {
                txtUnitPrice.Text = FormatToCurrency("0");
                txtUnitAmt.Text = FormatToCurrency("0");
                txtDisRate.Text = FormatToCurrency("0");
                txtDisAmt.Text = FormatToCurrency("0");
                txtTaxAmt.Text = FormatToCurrency("0");
                txtLineTotAmt.Text = FormatToCurrency("0");
                return;
            }
            LoadLevelStatus(cmbInvType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
            txtItem.Text = txtItem.Text.ToUpper().Trim();

            if (_isItemChecking)
            {
                _isItemChecking = false;
                return;
            }
            _isItemChecking = true;
            try
            {
                if (!LoadItemDetail(txtItem.Text))
                {
                    DisplayMessage("Please check the item code", 1);
                    txtItem.Text = "";
                    txtItem.Focus();

                    txtresno.Text = "";
                    if (IsPriceLevelAllowDoAnyStatus == false)
                    {
                        cmbStatus.SelectedIndex = 0;
                    }
                    return;
                }

                //if (_itemdetail.Mi_is_ser1 == 1 && IsGiftVoucher(_itemdetail.Mi_itm_tp))
                //{
                //    if (string.IsNullOrEmpty(txtSerialNo.Text))
                //    {
                //        DisplayMessage("Please select the gift voucher number", 1);
                //        txtItem.Text = "";
                //        txtSerialNo.Text = "";
                //    }

                //    return;
                //}
                IsVirtual(_itemdetail.Mi_itm_tp);

                //if ((_itemdetail.Mi_is_ser1 == 1  && chkDeliverNow.Checked == false && string.IsNullOrEmpty(txtSerialNo.Text.Trim()) && _priceBookLevelRef.Sapl_is_serialized == false))
                //{
                //    DisplayMessage("You have to select the serial number for the serialized item", 1);
                //    return;
                //}


                if (txtSerialNo.Text != "")
                {
                    txtQty.Text = FormatToQty("1");
                }
                if (_IsVirtualItem)
                {
                    bool block = CheckBlockItem(txtItem.Text.Trim(), 0, false);
                    if (block)
                    {
                        txtItem.Text = "";
                    }
                }

                txtresno.Text = "";
                lblSelectRevervation.Text = "";
                lblSelectRevLine.Text = "";
                if (txtPerTown.Text == "")
                {
                    DisplayMessage("Please select dispatch location", 1);
                    return;
                }
                bool isJSEND = false;
                _isInventoryCombineAdded = false; _isCombineAdding = false;
                CheckQty(true, out isJSEND);

                LoadDispatchDetails(Session["UserCompanyCode"].ToString(), txtPerTown.Text, txtItem.Text, cmbStatus.SelectedValue);
                lbtnadditems.Focus();

                gvPromotionItem.DataSource = new int[] { };
                gvPromotionItem.DataBind();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
                _isItemChecking = false;
            }
        }

        protected void cmbBook_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                //DefaultLevel = "";
                LoadPriceLevel(cmbInvType.Text, cmbBook.Text);
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                ClearPriceTextBox();
            }
            catch (Exception ex)
            {
                ClearPriceTextBox();
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void test2_Click(object sender, EventArgs e)
        {
            // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...s');", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
            DispMsg("test2_Click", "E");
        }

        private void CheckSerialAvailability()
        {
            //if (_stopit) return;
            hdfConfItem.Value = null;

            if (string.IsNullOrEmpty(txtSerialNo.Text.Trim())) return;
            if (txtSerialNo.Text.Trim().ToUpper() == "N/A" || txtSerialNo.Text.Trim().ToUpper() == "NA")
            {
                txtSerialNo.Text = "";
                DisplayMessage("Selected serial number is invalid or not available in your location.Please check your inventory !!!", 1);
                return;
            }
            txtItem.Text = string.Empty;
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItemBrand.Text = string.Empty;

            try
            {
                if (txtPerTown.Text.Trim() == "")
                {
                    txtSerialNo.Text = "";
                    DisplayMessage("Please select dispatch locatioon.", 1);
                    return;
                }
                string diploc = txtPerTown.Text.Trim();

                DataTable _multiItemforSerial = CHNLSVC.Inventory.GetMultipleItemforOneSerial(Session["UserCompanyCode"].ToString(), /*Session["UserDefLoca"].ToString()*/diploc, string.Empty, txtSerialNo.Text.Trim(), string.Empty);
                Int32 _isAvailable = _multiItemforSerial.Rows.Count;

                if (_isAvailable <= 0)
                {
                    //divalert.Visible = true;
                    txtSerialNo.Text = "";
                    DisplayMessage("Selected serial number is invalid or not available in your location.Please check your inventory !!!");
                    ClearPriceTextBox();
                    return;
                }

                if (_isAvailable > 1)
                {
                    dgvMultipleItems.DataSource = _multiItemforSerial;
                    dgvMultipleItems.DataBind();
                    mpMultipleItems.Show();
                    return;
                }

                string _item = _multiItemforSerial.Rows[0].Field<string>("Item");
                List<ReptPickSerials> _one = CHNLSVC.Inventory.Search_by_serial(Session["UserCompanyCode"].ToString(), diploc /*Session["UserDefLoca"].ToString()*/, _item, string.Empty, txtSerialNo.Text.Trim(), string.Empty);
                if (!string.IsNullOrEmpty(Convert.ToString(cmbLevel.Text)) && !string.IsNullOrEmpty(Convert.ToString(cmbBook.Text)))
                {
                    bool _isAgeLevel = false;
                    int _noofday = 0;
                    CheckNValidateAgeItem(_item, string.Empty, cmbBook.Text, cmbLevel.Text, cmbStatus.SelectedValue.ToString(), out _isAgeLevel, out _noofday);
                    if (_isAgeLevel)
                        _one = GetAgeItemList(Convert.ToDateTime(txtdate.Text), _isAgeLevel, _noofday, _one);
                    if (_one == null || _one.Count <= 0)
                    {
                        //divalert.Visible = true;
                        DisplayMessage("This serial cant select under ageing price level Please check the ageing status with IT dept", 1);
                        txtSerialNo.Text = string.Empty;
                        txtItem.Text = string.Empty;
                        txtSerialNo.Focus();
                        return;
                    }


                }
                if (_one != null && _one.Count > 0 && IsPriceLevelAllowDoAnyStatus == false)
                {
                    string _serialstatus = _one[0].Tus_itm_stus;

                    ListItem li = new ListItem();
                    li.Text = _serialstatus;
                    ListItem asd = cmbStatus.Items.FindByValue(_serialstatus);

                    bool _exist = false;
                    //bool _exist = cmbStatus.Items.Contains(li);
                    if (asd != null)
                    {
                        _exist = true;
                    }

                    if (_exist == false)
                    {
                        //DisplayMessage("Selected serial item inventory status not available in price level status collection. Please contact IT dept ", 2);
                        DisplayMessageJS("Selected serial item inventory status not available in price level status collection. Please contact IT dept");
                        txtSerialNo.Text = string.Empty;
                        txtItem.Text = string.Empty;
                        txtSerialNo.Focus();
                        return;
                    }
                    else
                        cmbStatus.SelectedValue = _serialstatus;
                }

                if (LoadMultiCombinItem(_item) == false)
                {
                    LoadItemDetail(_item);
                    txtItem.Text = _item;
                    txtQty.Text = "1";
                    _stopit = true;

                    bool isJSEnd = false;

                    txtQty.ReadOnly = true;

                    CheckQty(true, out isJSEnd);
                }
                else
                {
                    DataTable _invnetoryCombinAnalalize = CHNLSVC.Inventory.GetCompeleteItemfromAssambleItem(_item);
                    dgvMultipleItems.DataSource = _invnetoryCombinAnalalize;
                    dgvMultipleItems.DataBind();
                    mpMultipleItems.Show();
                }

                if (_one != null && _one.Count == 1)
                {
                    cmbStatus.SelectedValue = _one[0].Tus_itm_stus;
                }

            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private bool BindItemComponent(string _item)
        {
            _masterItemComponent = CHNLSVC.Inventory.GetItemComponents(_item);
            if (_masterItemComponent != null)
            {
                if (_masterItemComponent.Count > 0)
                {
                    _masterItemComponent.ForEach(X => X.Micp_must_scan = false);
                    if (_masterItemComponent != null)
                    {
                        if (_masterItemComponent.Count > 0)
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else return false;
            }
            else return false;
        }

        private bool CheckInventoryCombine()
        {
            bool _IsTerminate = false;
            _isCompleteCode = false;

            if (!string.IsNullOrEmpty(txtItem.Text))
            {
                MasterItem _itemDet = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                if (_itemDet.Mi_itm_tp == "V" && _itemDet.Mi_is_subitem == true)
                    _isCompleteCode = BindItemComponent(txtItem.Text.Trim());

                if (_isCompleteCode)
                {
                    if (_masterItemComponent != null)
                    {
                        if (_masterItemComponent.Count > 0)
                        {
                            _isInventoryCombineAdded = false;
                            _isCompleteCode = true;
                            _IsTerminate = false;
                            return _IsTerminate;
                        }
                        else
                        {
                            _isCompleteCode = false;
                            _IsTerminate = true;
                        }
                    }
                    else
                    {
                        _isCompleteCode = false;
                        _IsTerminate = true;
                    }
                }
            }
            else
            {
                _isCompleteCode = false;
                _IsTerminate = true;
            }

            return _IsTerminate;
        }

        public static decimal RoundUpForPlace(decimal input, int places)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));
            return Math.Ceiling(input * Convert.ToDecimal(multiplier)) / Convert.ToDecimal(multiplier);
        }

        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            //if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
            //else return Math.Round(value, 2);
            return value;
        }

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    else _isVATInvoice = false;

                    if (Convert.ToDateTime(txtdate.Text) == _serverDt)
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false)
                        {
                            _taxs = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), _item, _status);
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), _item, _status, null, null, _mstItem.Mi_anal1);
                            }
                            else
                            {
                                _taxs = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), _item, _status);
                            }

                        }
                        else
                        {
                            //_taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(),_item, _status, string.Empty, "VAT");
                            if (_isStrucBaseTax == true)       //kapila
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT", _mstItem.Mi_anal1);
                            }
                            else
                                _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT");
                        }

                        var _Tax = from _itm in _taxs
                                   select _itm;
                        foreach (MasterItemTax _one in _Tax)
                        {
                            if (lblVatExemptStatus.Text != "Available")
                            {
                                if (_isTaxfaction == false)
                                    _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                else
                                    if (_isVATInvoice)
                                    {
                                        _discount = _pbUnitPrice * _qty * _disRate / 100;
                                        _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                    }
                                    else
                                        _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                            }
                            else
                            {
                                if (_isTaxfaction) _pbUnitPrice = 0;
                            }
                        }
                    }
                    else
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTaxEffDt(Session["UserCompanyCode"].ToString(), _item, _status, Convert.ToDateTime(txtdate.Text)); else _taxs = CHNLSVC.Sales.GetItemTaxEffDt(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT", Convert.ToDateTime(txtdate.Text));
                        var _Tax = from _itm in _taxs
                                   select _itm;
                        if (_taxs.Count > 0)
                        {
                            foreach (MasterItemTax _one in _Tax)
                            {
                                if (lblVatExemptStatus.Text != "Available")
                                {
                                    if (_isTaxfaction == false)
                                        _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                                    else
                                        if (_isVATInvoice)
                                        {
                                            _discount = _pbUnitPrice * _qty * _disRate / 100;
                                            _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                        }
                                        else
                                            _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;
                                }
                                else
                                {
                                    if (_isTaxfaction) _pbUnitPrice = 0;
                                }
                            }
                        }
                        else
                        {
                            List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                            if (_isTaxfaction == false)
                                _taxsEffDt = CHNLSVC.Sales.GetTaxLog(Session["UserCompanyCode"].ToString(), _item, _status, Convert.ToDateTime(txtdate.Text));
                            else
                                _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT", Convert.ToDateTime(txtdate.Text));
                            var _TaxEffDt = from _itm in _taxsEffDt
                                            select _itm;
                            foreach (LogMasterItemTax _one in _TaxEffDt)
                            {
                                if (lblVatExemptStatus.Text != "Available")
                                {
                                    if (_isTaxfaction == false)
                                        _pbUnitPrice = _pbUnitPrice * _one.Lict_tax_rate;
                                    else
                                        if (_isVATInvoice)
                                        {
                                            _discount = _pbUnitPrice * _qty * _disRate / 100;
                                            _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Lict_tax_rate / 100) * _qty;
                                        }
                                        else
                                            _pbUnitPrice = (_pbUnitPrice * _one.Lict_tax_rate / 100) * _qty;
                                }
                                else
                                {
                                    if (_isTaxfaction) _pbUnitPrice = 0;
                                }
                            }
                        }
                    }
                }
                else
                    if (_isTaxfaction) _pbUnitPrice = 0;
            return _pbUnitPrice;
        }

        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), cmbBook.Text, cmbLevel.Text);

                txtUnitAmt.Text = SetDecimalPoint((Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true))));

                decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true), true);
                txtTaxAmt.Text = SetDecimalPoint((Convert.ToString(_vatPortion)));

                decimal _totalAmount = Convert.ToDecimal(txtQty.Text) * Convert.ToDecimal(txtUnitPrice.Text);
                decimal _disAmt = 0;

                if (!string.IsNullOrEmpty(txtDisRate.Text))
                {
                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    else _isVATInvoice = false;

                    if (_isVATInvoice)
                        _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                    else
                    {
                        _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(txtDisRate.Text) / 100), true);
                        if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        {
                            if (_priceBookLevelRef != null)
                                if (_priceBookLevelRef.Sapl_vat_calc)
                                {
                                    List<MasterItemTax> _tax = new List<MasterItemTax>();
                                    if (Convert.ToDateTime(txtdate.Text) == _serverDt)
                                    {

                                        if (_isStrucBaseTax == true)       //RUKSHAN
                                        {
                                            MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                                            _tax = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, "VAT", _mstItem.Mi_anal1);
                                        }
                                        else
                                            _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, "VAT");

                                        if (_tax != null && _tax.Count > 0)
                                        {
                                            decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                            txtTaxAmt.Text = SetDecimalPoint(Convert.ToString(FigureRoundUp(_vatval, true)));
                                        }
                                    }
                                    else
                                    {

                                        // if (_isTaxfaction == false)
                                        //{
                                        // _tax = CHNLSVC.Sales.GetTaxEffDt(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), Convert.ToDateTime(txtdate.Text));
                                        // }
                                        //else
                                        // {
                                        _tax = CHNLSVC.Sales.GetItemTaxEffDt(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, "VAT", Convert.ToDateTime(txtdate.Text));
                                        // }

                                        if (_tax != null && _tax.Count > 0)
                                        {
                                            decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                            txtTaxAmt.Text = SetDecimalPoint(Convert.ToString(FigureRoundUp(_vatval, true)));
                                        }
                                        else
                                        {
                                            List<LogMasterItemTax> _taxsEffDt = new List<LogMasterItemTax>();
                                            // if (_isTaxfaction == false) 
                                            // _taxsEffDt = CHNLSVC.Sales.GetTaxLog(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), Convert.ToDateTime(txtdate.Text)); 
                                            // else 
                                            _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, "VAT", Convert.ToDateTime(txtdate.Text));
                                            if (_taxsEffDt != null && _taxsEffDt.Count > 0)
                                            {
                                                decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _taxsEffDt[0].Lict_tax_rate) / (100 + _taxsEffDt[0].Lict_tax_rate);
                                                txtTaxAmt.Text = SetDecimalPoint(Convert.ToString(FigureRoundUp(_vatval, true)));
                                            }
                                        }

                                    }

                                    //List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                                    //if (_tax != null && _tax.Count > 0)
                                    //{
                                    //    decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                    //    txtTaxAmt.Text = SetDecimalPoint(Convert.ToString(FigureRoundUp(_vatval, true)));
                                    //}
                                }
                        }
                    }

                    txtDisAmt.Text = _disAmt.ToString("N2");
                }

                if (!string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);
                }

                txtLineTotAmt.Text = SetDecimalPoint(_totalAmount);
            }
        }

        private bool CheckQtyPriliminaryRequirements()
        {
            bool _IsTerminate = false;
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                _IsTerminate = true; return _IsTerminate;
            }
            if (!IsNumeric(txtQty.Text.Trim(), NumberStyles.Float))
            {
                //divalert.Visible = true;
                DisplayMessage("Please select the valid quantity");
                _IsTerminate = true;
                txtQty.Focus();
                return _IsTerminate; ;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) { _IsTerminate = true; return _IsTerminate; };

            if (_itemdetail.Mi_is_ser1 == 1 && !string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                txtQty.Text = "1";
            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            if (string.IsNullOrEmpty(txtQty.Text)) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; WarrantyPeriod = 0; WarrantyRemarks = string.Empty; _IsTerminate = true; return _IsTerminate; }
            if (Convert.ToDecimal(txtQty.Text) <= 0) { CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; WarrantyPeriod = 0; WarrantyRemarks = string.Empty; _IsTerminate = true; return _IsTerminate; }
            if (string.IsNullOrEmpty(cmbInvType.Text))
            {
                DisplayMessage("Please select invoice type");
                _IsTerminate = true;
                cmbInvType.Focus();
                return _IsTerminate;
            }
            // 26/Apr/2016 credit customer normal price issue comment by rukshan
            //if (string.IsNullOrEmpty(txtCustomer.Text))
            //{
            //    DisplayMessage("Please select the customer");
            //    _IsTerminate = true;
            //    txtCustomer.Focus();
            //    return _IsTerminate;
            //}

            if (string.IsNullOrEmpty(txtItem.Text))
            {
                DisplayMessage("Please select the item");
                _IsTerminate = true;
                txtItem.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(cmbBook.Text))
            {
                DisplayMessage("Price book not select");
                _IsTerminate = true;
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(cmbLevel.Text))
            {
                DisplayMessage("Please select the price level !!!");
                _IsTerminate = true;
                cmbLevel.Focus();
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(cmbStatus.Text) || cmbStatus.SelectedIndex == 0)
            {
                DisplayMessage("Please select the item status !!!");
                _IsTerminate = true;
                cmbStatus.Focus();
                return _IsTerminate;
            }
            return _IsTerminate;
        }

        private bool CheckTaxAvailability()
        {
            bool _IsTerminate = false;
            if (!_isCompleteCode)
            {
                if (Convert.ToDateTime(txtdate.Text) == _serverDt)
                {
                    //  List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty);
                    List<MasterItemTax> _tax = new List<MasterItemTax>();
                    if (_isStrucBaseTax == true)       //kapila
                    {
                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text);
                        _tax = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty, _mstItem.Mi_anal1);
                    }
                    else
                        _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty);

                    if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                        _IsTerminate = true;
                    if (_tax.Count <= 0)
                        _IsTerminate = true;
                }
                else
                {
                    List<MasterItemTax> _taxEff = CHNLSVC.Sales.GetItemTaxEffDt(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), "VAT", string.Empty, Convert.ToDateTime(txtdate.Text));
                    if (_taxEff.Count <= 0)
                    {
                        List<LogMasterItemTax> _tax = CHNLSVC.Sales.GetItemTaxLog(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), "VAT", string.Empty, Convert.ToDateTime(txtdate.Text));
                        if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                            _IsTerminate = true;
                        if (_tax.Count <= 0)
                            _IsTerminate = true;
                    }
                }
            }
            return _IsTerminate;
        }

        private void CheckItemTax(string _item)
        {
            MainTaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef != null)
            {
                if (_priceBookLevelRef.Sapl_vat_calc == true)
                {
                    MainTaxConstant = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), _item, cmbStatus.SelectedValue.ToString().Trim());
                }
            }
        }

        private void SetDecimalTextBoxForZero(bool _isUnit, bool _isAccBal, bool _isQty)
        {
            txtDisRate.Text = "0";
            txtDisAmt.Text = "0";
            //if (_isQty)
            //    txtQty.Text = "1";
            txtTaxAmt.Text = "0";
            if (_isUnit)
                txtUnitPrice.Text = "0";
            txtUnitAmt.Text = "0";
            txtLineTotAmt.Text = "0";
            if (_isAccBal)
            {
                lblAccountBalance.Text = "0"; lblAvailableCredit.Text = "0";
            }
        }

        private bool CheckProfitCenterAllowForWithoutPrice()
        {
            bool _isAvailable = false;
            if ((_MasterProfitCenter != null) && (_priceBookLevelRef != null))
            {
                if (_MasterProfitCenter.Mpc_without_price && _priceBookLevelRef.Sapl_is_without_p)
                {
                    SetDecimalTextBoxForZero(false, false, false);
                    _isAvailable = true;
                    return _isAvailable;
                }
            }
            return _isAvailable;
        }

        private decimal CheckSubItemTax(string _item)
        {
            decimal _fraction = 1;
            List<MasterItemTax> TaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
            {
                TaxConstant = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), _item, cmbStatus.SelectedValue.ToString().Trim());
                if (TaxConstant != null)
                    if (TaxConstant.Count > 0)
                        _fraction = TaxConstant[0].Mict_tax_rate;
            }
            return _fraction;
        }

        protected void BindConsumableItem(List<InventoryBatchRefN> _consumerpricelist)
        {
            _consumerpricelist.ForEach(x => x.Inb_unit_cost = x.Inb_unit_price * CheckSubItemTax(x.Inb_itm_cd));
        }

        private bool ConsumerItemProduct()
        {
            bool _isAvailable = false;
            bool _isMRP = _itemdetail.Mi_anal3;
            if (_isMRP && chkDeliverNow.Checked == false)
            {
                List<InventoryBatchRefN> _batchRef = new List<InventoryBatchRefN>();
                if (_priceBookLevelRef != null)
                {
                    if (_priceBookLevelRef.Sapl_chk_st_tp) _batchRef = CHNLSVC.Sales.GetConsumerProductPriceList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim()); else _batchRef = CHNLSVC.Sales.GetConsumerProductPriceList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), string.Empty);
                }
                if (_batchRef.Count > 0)
                    if (_batchRef.Count > 1)
                    {
                        BindConsumableItem(_batchRef);
                    }
                    else if (_batchRef.Count == 1)
                    {
                        if (_batchRef[0].Inb_free_qty < Convert.ToDecimal(txtQty.Text.Trim()))
                        {
                            DisplayMessage("Invoice quantity is " + txtQty.Text + " and inventory available quantity having only " + _batchRef[0].Inb_free_qty.ToString());
                            _isAvailable = true;
                            return _isAvailable;
                        }
                        decimal prinValue = _batchRef[0].Inb_unit_price * CheckSubItemTax(_batchRef[0].Inb_itm_cd);
                        txtUnitPrice.Text = prinValue.ToString("N2");
                        txtUnitPrice.Focus();
                        _isAvailable = false;
                    }
                _isEditPrice = false;
                _isEditDiscount = false;
                if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = "0";
                decimal val = Convert.ToDecimal(txtQty.Text);
                txtQty.Text = (Convert.ToString(val));
                CalculateItem();
                _isAvailable = true;
            }
            return _isAvailable;
        }

        private bool CheckItemPromotion(out int IsReturnToScript)
        {
            IsReturnToScript = 0;
            DateTime txtDate = Convert.ToDateTime(txtdate.Text);
            _isNewPromotionProcess = false;
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                DisplayMessage("Please select the item");
                return false;
            }
            _PriceDetailRefPromo = null;
            _PriceSerialRefPromo = null;
            _PriceSerialRefNormal = null;
            CHNLSVC.Sales.GetPromotionNew(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtItem.Text.Trim(), txtDate.Date, txtCustomer.Text.Trim(), Convert.ToDecimal(txtQty.Text), out _PriceDetailRefPromo, out _PriceSerialRefPromo, out _PriceSerialRefNormal);
            if (_PriceDetailRefPromo == null && _PriceSerialRefPromo == null && _PriceSerialRefNormal == null) return false;
            if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0 && !string.IsNullOrEmpty(txtSerialNo.Text))
            {
                Session["_PriceDetailRefPromo"] = _PriceDetailRefPromo;
                Session["_PriceSerialRefPromo"] = _PriceSerialRefPromo;
                Session["_PriceSerialRefNormal"] = _PriceSerialRefNormal;

                var _isSerialAvailable = _PriceSerialRefNormal.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
                if (_isSerialAvailable != null && _isSerialAvailable.Count > 0)
                {
                    if (hdfnormalSerialized.Value == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "selectNormalSerialized('');", true);
                        IsReturnToScript = 1;
                        return false;
                    }


                    if (hdfnormalSerialized.Value != null && hdfnormalSerialized.Value.ToString() == "1")
                    {
                        _isNewPromotionProcess = true;
                        CheckSerializedPriceLevelAndLoadSerials(true);
                        return true;
                    }
                    else
                    {

                    }

                    //DialogResult _normalSerialized = new DialogResult();
                    //using (new CenterWinDialog(this))
                    //{
                    //    _normalSerialized = MessageBox.Show("This item is having normal serialized price.Do you need to select normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //}
                    //if (_normalSerialized == System.Windows.Forms.DialogResult.Yes)
                    //{
                    //    _isNewPromotionProcess = true;
                    //    CheckSerializedPriceLevelAndLoadSerials(true);
                    //    return true;
                    //}
                }
                else
                {
                    _isNewPromotionProcess = false;
                    _PriceSerialRefNormal = null;
                }
            }
            else if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0 && string.IsNullOrEmpty(txtSerialNo.Text))
            {
                if (hdfcontinueWithNormalSerializedPrice.Value == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "continueWithNormalPerializedPrice('');", true);
                    IsReturnToScript = 1;
                    return false;
                }

                if (hdfcontinueWithNormalSerializedPrice != null && hdfcontinueWithNormalSerializedPrice.Value == "1")
                {
                    _isNewPromotionProcess = true;
                    CheckSerializedPriceLevelAndLoadSerials(true);
                    return true;
                }
                else
                {
                    _isNewPromotionProcess = false;
                    _PriceSerialRefNormal = null;
                }


                //DialogResult _normalSerialized = new System.Windows.Forms.DialogResult();
                //using (new CenterWinDialog(this)) { _normalSerialized = MessageBox.Show("This item having normal serialized price. Do you need to continue with normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
                //if (_normalSerialized == System.Windows.Forms.DialogResult.Yes)
                //{
                //    _isNewPromotionProcess = true;
                //    CheckSerializedPriceLevelAndLoadSerials(true);
                //    return true;
                //}
                //else
                //{
                //    _isNewPromotionProcess = false;
                //    _PriceSerialRefNormal = null;
                //}
            }
            if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0 && !string.IsNullOrEmpty(txtSerialNo.Text))
            {
                var _isSerialPromoAvailable = _PriceSerialRefPromo.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
                if (_isSerialPromoAvailable != null && _isSerialPromoAvailable.Count > 0)
                {

                    if (hdfselectPromotionalSerializedPrice.Value == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "selectPromotionalSerializedPrice('');", true);
                        IsReturnToScript = 1;
                        return false;
                    }

                    if (hdfselectPromotionalSerializedPrice.Value != null && hdfselectPromotionalSerializedPrice.Value == "1")
                    {
                        _isNewPromotionProcess = true;
                        CheckSerializedPriceLevelAndLoadSerials(true);
                        return true;
                    }
                    else
                    {
                        _isNewPromotionProcess = false;
                        _PriceSerialRefPromo = null;
                    }

                    //DialogResult _promoSerialized = new System.Windows.Forms.DialogResult();
                    //using (new CenterWinDialog(this)) { _promoSerialized = MessageBox.Show("This item is having promotional serialized price.Do you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
                    //if (_promoSerialized == System.Windows.Forms.DialogResult.Yes)
                    //{
                    //    _isNewPromotionProcess = true;
                    //    CheckSerializedPriceLevelAndLoadSerials(true);
                    //    return true;
                    //}
                    //else
                    //{
                    //    _isNewPromotionProcess = false;
                    //    _PriceSerialRefPromo = null;
                    //}
                }
                //else if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0 && string.IsNullOrEmpty(txtSerialNo.Text))
                //{

                //    DialogResult _promoSerialized = new System.Windows.Forms.DialogResult();
                //    using (new CenterWinDialog(this)) { _promoSerialized = MessageBox.Show("This item is having promotional serialized price.Do you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
                //    if (_promoSerialized == System.Windows.Forms.DialogResult.Yes)
                //    {
                //        _isNewPromotionProcess = true;
                //        CheckSerializedPriceLevelAndLoadSerials(true);
                //        return true;
                //    }
                //    else
                //    {
                //        _isNewPromotionProcess = false;
                //        _PriceSerialRefPromo = null;
                //    }
                //}
            }
            if (_PriceDetailRefPromo != null && _PriceDetailRefPromo.Count > 0)
            {
                _PriceDetailRefPromo = _PriceDetailRefPromo.Where(x => x.Sapd_pb_tp_cd == cmbBook.Text && x.Sapd_pbk_lvl_cd == cmbLevel.Text).ToList();
                if (txtCustomer.Text == "CASH")
                {
                    _PriceDetailRefPromo = _PriceDetailRefPromo.Where(x => x.Sapd_customer_cd == "").ToList();
                }
                else
                {
                    List<PriceDetailRef> _pro = new List<PriceDetailRef>();
                    _pro = _PriceDetailRefPromo.Where(x => x.Sapd_customer_cd == txtCustomer.Text).ToList();
                    if (_pro.Count == 0)
                    {
                        _PriceDetailRefPromo = _PriceDetailRefPromo.Where(x => x.Sapd_customer_cd == "" || x.Sapd_customer_cd == "N/A").ToList();
                    }
                    else
                    {
                        _PriceDetailRefPromo = _pro;
                    }
                }

                if (_PriceDetailRefPromo.Count == 0)
                {
                    // _PriceDetailRefPromo = _PriceDetailRefPromo.Where(x => x.Sapd_qty_from >= Convert.ToDecimal(txtQty.Text)).ToList();
                    // DisplayMessage("No Promotions found for the selected customer", 1);
                    // DisplayMessage("No Promotions found for the selected customer", 1);
                    return false;
                }
                if (txtItem.Text.Trim() != hdfConfItem.Value.Trim())
                {
                    hdfConf.Value = "";
                    mpConfirmation.Show();
                    btnConfYes.Focus();
                    lblConfText.Text = "This item is having promotions. Do you need to continue with the available promotions?";
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "continueWithTheAvailablePromotions('');", true);
                    IsReturnToScript = 1;
                    return true;
                }
                else if (hdfConf.Value == "1")
                {
                    //DisplayMessage("Develop promotion 4518");
                    Session["mpPriceNPromotion"] = "1";
                    mpPriceNPromotion.Show();

                    SetColumnForPriceDetailNPromotion(false);
                    gvNormalPrice.DataSource = new List<PriceDetailRef>();
                    gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                    //gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                    //gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                    BindNonSerializedPrice(_PriceDetailRefPromo);
                    _isNewPromotionProcess = true;
                    return true;
                }
                else if (hdfConf.Value == "0")
                {
                    _isNewPromotionProcess = false;
                    return false;
                }



                //if (hdfcontinueWithTheAvailablePromotions.Value != null && hdfcontinueWithTheAvailablePromotions.Value == "1")
                //{
                //    DisplayMessage("Develop promotion 4518");
                //    //SetColumnForPriceDetailNPromotion(false);
                //    //gvNormalPrice.DataSource = new List<PriceDetailRef>();
                //    //gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                //    //gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                //    //gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                //    //BindNonSerializedPrice(_PriceDetailRefPromo);
                //    //pnlPriceNPromotion.Visible = true;
                //    //pnlMain.Enabled = false;
                //    //_isNewPromotionProcess = true;
                //    return true;
                //}
                //else
                //{
                //    _isNewPromotionProcess = false;
                //    return false;
                //}

                //lblConfText.Text = "This item is having promotions. Do you need to continue with the available promotions?";
                //mpConfirmation.Show();

                //DialogResult _promo = new System.Windows.Forms.DialogResult();
                //using (new CenterWinDialog(this)) { _promo = MessageBox.Show("This item is having promotions. Do you need to continue with the available promotions?", "Promotion", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
                //if (_promo == System.Windows.Forms.DialogResult.Yes)
                //{
                //    SetColumnForPriceDetailNPromotion(false);
                //    gvNormalPrice.DataSource = new List<PriceDetailRef>();
                //    gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                //    gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                //    gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                //    BindNonSerializedPrice(_PriceDetailRefPromo);
                //    pnlPriceNPromotion.Visible = true;
                //    pnlMain.Enabled = false;
                //    _isNewPromotionProcess = true;
                //    return true;
                //}
                //else
                //{
                //    _isNewPromotionProcess = false;
                //    return false;
                //}
            }
            return false;
        }

        protected void BindSerializedPrice(List<PriceSerialRef> _list)
        {
            _list.ForEach(x => x.Sars_cre_by = Convert.ToString(x.Sars_itm_price));
            _list.ForEach(x => x.Sars_itm_price = CheckSubItemTax(x.Sars_itm_cd) * x.Sars_itm_price);
            var _normal = _list.Where(x => x.Sars_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sars_price_type != 0).ToList();
        }

        protected void BindPriceAndPromotion(List<PriceSerialRef> _list)
        {
            _list.ForEach(x => x.Sars_cre_by = Convert.ToString(x.Sars_itm_price));
            _list.ForEach(x => x.Sars_itm_price = x.Sars_itm_price * CheckSubItemTax(x.Sars_itm_cd));
            var _normal = _list.Where(x => x.Sars_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sars_price_type != 0).ToList();
        }

        private void DisplayAvailableQty(string _item, Label _withStatus, Label _withoutStatus, string _status)
        {
            List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item.Trim(), string.Empty);
            if (_inventoryLocation != null)
                if (_inventoryLocation.Count > 0)
                {
                    var _woStatus = _inventoryLocation.Select(x => x.Inl_free_qty).Sum();
                    var _wStatus = _inventoryLocation.Where(x => x.Inl_itm_stus == _status).Select(x => x.Inl_free_qty).Sum();
                    _withStatus.Text = FormatToQty(Convert.ToString(_wStatus));
                    _withoutStatus.Text = FormatToQty(Convert.ToString(_woStatus));
                }
                else { _withStatus.Text = FormatToQty("0"); _withoutStatus.Text = FormatToQty("0"); }
            else { _withoutStatus.Text = FormatToQty("0"); _withStatus.Text = FormatToQty("0"); }
        }

        private bool CheckSerializedPriceLevelAndLoadSerials(bool _isSerialized)
        {
            //Session["_PriceDetailRefPromo"] = _PriceDetailRefPromo;
            //Session["_PriceSerialRefPromo"] = _PriceSerialRefPromo;
            //Session["_PriceSerialRefNormal"] = _PriceSerialRefNormal;

            if (_PriceSerialRefPromo == null && Session["_PriceSerialRefPromo"] != null)
            {
                _PriceSerialRefPromo = (List<PriceSerialRef>)Session["_PriceSerialRefPromo"];
            }
            if (_PriceSerialRefNormal == null && Session["_PriceSerialRefNormal"] != null)
            {
                _PriceSerialRefNormal = (List<PriceSerialRef>)Session["_PriceSerialRefNormal"];
            }


            bool _isAvailable = false;
            if (_isSerialized)
            {
                if (string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    //divalert.Visible = true;
                    DisplayMessage("You are selected a serialized price level, hence you have not select the serial number. Please select the serial number");
                    _isAvailable = true;
                    return _isAvailable;
                }
                List<PriceSerialRef> _list = null;
                if (_isNewPromotionProcess == false)
                    _list = CHNLSVC.Sales.GetAllPriceSerialFromSerial(cmbBook.Text, cmbLevel.Text, txtItem.Text.Trim(), Convert.ToDateTime(txtdate.Text), txtCustomer.Text, Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtSerialNo.Text.Trim());
                else if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0)
                    _list = _PriceSerialRefNormal;
                else if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0)
                    _list = _PriceSerialRefPromo;
                _tempPriceSerial = new List<PriceSerialRef>();
                _tempPriceSerial = _list;
                if (_list != null)
                {
                    if (_list.Count <= 0)
                    {
                        //divalert.Visible = true;
                        DisplayMessage("There are no serials available for the selected item");
                        txtQty.Text = "0";
                        _isAvailable = true;
                        txtQty.Focus();
                        return _isAvailable;
                    }
                    var _oneSerial = _list.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
                    _list = _oneSerial;
                    if (_list.Count < Convert.ToDecimal(txtQty.Text))
                    {
                        //divalert.Visible = true;
                        DisplayMessage("Selected quantity is exceeds available serials at the price definition");

                        txtQty.Text = "0";
                        // IsNoPriceDefinition = true;
                        _isAvailable = true;
                        txtQty.Focus();
                        return _isAvailable;
                    }
                    //if (_list.Count == 1)
                    //{
                    string _book = _list[0].Sars_pbook;
                    string _level = _list[0].Sars_price_lvl;
                    cmbBook.Text = _book;
                    cmbLevel.Text = _level;
                    if (!_isSerialized)
                        cmbLevel_SelectedIndexChanged(null, null);

                    int _priceType = 0;
                    _priceType = _list[0].Sars_price_type;
                    PriceTypeRef _promotion = TakePromotion(_priceType);
                    decimal UnitPrice = TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _list[0].Sars_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true);
                    UnitPrice = _list[0].Sars_itm_price;

                    txtUnitPrice.Text = (Convert.ToString(UnitPrice));
                    txtUnitPrice.Text = UnitPrice.ToString("N2");
                    WarrantyRemarks = _list[0].Sars_warr_remarks;
                    SetSSPriceDetailVariable(_list[0].Sars_circular_no, "0", Convert.ToString(_list[0].Sars_pb_seq), Convert.ToString(_list[0].Sars_itm_price), _list[0].Sars_promo_cd, Convert.ToString(_list[0].Sars_price_type));

                    Int32 _pbSq = _list[0].Sars_pb_seq;
                    string _mItem = _list[0].Sars_itm_cd;
                    _isAvailable = true;
                    //if (_promotion.Sarpt_is_com)
                    //{
                    SetColumnForPriceDetailNPromotion(true);
                    BindSerializedPrice(_list);

                    //if (gvPromotionPrice.RowCount > 0)
                    //{
                    //    gvPromotionPrice_CellDoubleClick(0, false, _isSerialized);
                    //    pnlPriceNPromotion.Visible = true;
                    //    pnlMain.Enabled = false;
                    //    return _isAvailable;
                    //}
                    //else
                    if (_isCombineAdding == false) txtUnitPrice.Focus();
                    //}
                    //else
                    //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                    return _isAvailable;
                    //}
                    //if (_list.Count > 1)
                    //{
                    //    SetColumnForPriceDetailNPromotion(true);
                    //    BindPriceAndPromotion(_list);
                    //    //DisplayAvailableQty(txtItem.Text, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, cmbStatus.Text);

                    //    _isAvailable = true;
                    //    return _isAvailable;
                    //}
                }
                else
                {
                    //divalert.Visible = true;
                    DisplayMessage("There are no serials available for the selected item");

                    txtQty.Text = "0";
                    _isAvailable = true;
                    txtQty.Focus();
                    return _isAvailable;
                }
            }
            return _isAvailable;
        }

        private void SetSSPriceDetailVariable(string _circuler, string _pblineseq, string _pbseqno, string _pbprice, string _promotioncd, string _promotiontype)
        {
            SSCirculerCode = _circuler;
            SSPriceBookItemSequance = _pblineseq;
            SSPriceBookPrice = Convert.ToDecimal(_pbprice);
            SSPriceBookSequance = _pbseqno;
            SSPromotionCode = _promotioncd;
            if (string.IsNullOrEmpty(_promotioncd) || _promotioncd.Trim().ToUpper() == "N/A") SSPromotionCode = string.Empty;
            SSPRomotionType = Convert.ToInt32(_promotiontype);
        }

        protected PriceTypeRef TakePromotion(Int32 _priceType)
        {
            List<PriceTypeRef> _type = CHNLSVC.Sales.GetAllPriceType(string.Empty);
            var _ptype = from _types in _type
                         where _types.Sarpt_indi == _priceType
                         select _types;
            PriceTypeRef _list = new PriceTypeRef();
            foreach (PriceTypeRef _ones in _ptype)
            {
                _list = _ones;
            }
            return _list;
        }

        private bool IsGiftVoucher(string _type)
        {
            if (_type == "G")
                return true;
            else
                return false;
        }

        protected void BindNonSerializedPrice(List<PriceDetailRef> _list)
        {
            _list.ForEach(x => x.Sapd_cre_by = Convert.ToString(x.Sapd_itm_price));
            // _list.ForEach(x => x.Sapd_itm_price = CheckSubItemTax(x.Sapd_itm_cd) * x.Sapd_itm_price);

            var _normal = _list.Where(x => x.Sapd_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sapd_price_type != 0).ToList();

            gvNormalPrice.DataSource = _normal;
            gvPromotionPrice.DataSource = _promotion;

            // gvNormalPrice.DataBind();
            gvPromotionPrice.DataBind();
        }

        private void markSelectedItems()
        {
            if (PriceCombinItemSerialList != null && PriceCombinItemSerialList.Count > 0)
            {
                if (gvPromotionSerial.Rows.Count > 0)
                {
                    for (int i = 0; i < gvPromotionSerial.Rows.Count; i++)
                    {
                        GridViewRow Row = gvPromotionSerial.Rows[i];
                        CheckBox chkSelectPromSerial = (CheckBox)Row.FindControl("chkSelectPromSerial");
                        Label lblTus_itm_cd = (Label)Row.FindControl("lblTus_itm_cd");
                        Label lblTus_ser_id = (Label)Row.FindControl("lblTus_ser_id");

                        if (PriceCombinItemSerialList.FindAll(x => x.Tus_itm_cd == lblTus_itm_cd.Text.Trim() && x.Tus_ser_id == Convert.ToInt32(lblTus_ser_id.Text.Trim())).Count > 0)
                        {
                            chkSelectPromSerial.Checked = true;
                        }
                    }
                }
            }
        }

        private bool IsVirtual(string _type)
        { if (_type == "V") { _IsVirtualItem = true; return true; } else { _IsVirtualItem = false; return false; } }

        private decimal CalculateItemTem(decimal _qty, decimal _unitPrice, decimal _disAmount, decimal _disRt)
        {
            string unitAmt = (Convert.ToString(FigureRoundUp(Convert.ToDecimal(_unitPrice) * Convert.ToDecimal(_qty), true)));

            decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(_qty), _priceBookLevelRef, Convert.ToDecimal(_unitPrice), Convert.ToDecimal(_disAmount), Convert.ToDecimal(_disRt), true), true);
            string tax = (Convert.ToString(_vatPortion));

            decimal _totalAmount = Convert.ToDecimal(_qty) * Convert.ToDecimal(_unitPrice);
            decimal _disAmt = 0;

            if (_disRt != 0)
            {
                bool _isVATInvoice = false;
                if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                else _isVATInvoice = false;

                if (_isVATInvoice)
                    _disAmt = FigureRoundUp(_totalAmount * (Convert.ToDecimal(_disRt) / 100), true);
                else
                {
                    _disAmt = FigureRoundUp((_totalAmount + _vatPortion) * (Convert.ToDecimal(_disRt) / 100), true);
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                    {

                        List<MasterItemTax> _tax = new List<MasterItemTax>();
                        if (_isStrucBaseTax == true)       //kapila
                        {
                            MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text);
                            _tax = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty, _mstItem.Mi_anal1);
                        }
                        else
                            _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                        if (_tax != null && _tax.Count > 0)
                        {
                            decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                            tax = Convert.ToString(FigureRoundUp(_vatval, true));
                        }

                        //List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                        //if (_tax != null && _tax.Count > 0)
                        //{
                        //    decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                        //    tax = Convert.ToString(FigureRoundUp(_vatval, true));
                        //}
                    }
                }

                Convert.ToString(_disAmt);
            }

            if (!string.IsNullOrEmpty(tax))
            {
                if (Convert.ToDecimal(txtDisRate.Text) > 0)
                    _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                else
                    _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(tax) - _disAmt, true);
            }

            return _totalAmount;
        }

        protected bool CheckQty(bool _isSearchPromotion, out bool isJS_END)
        {
            _postBackCount++;
            lblPostCount.Text = _postBackCount.ToString();
            isJS_END = false;

            if (_isCompleteCode == null)
            {
                _isCompleteCode = false;
            }

            bool _supDisAva = false;
            if (!string.IsNullOrEmpty(txtCustomer.Text) && Session["UserCompanyCode"].ToString() == "AAL")
            {
                List<CashGeneralEntiryDiscountDef> _disList = CHNLSVC.Sales.GetGeneralEntityDiscountDef(Session["UserCompanyCode"].ToString(),
                    Session["UserDefProf"].ToString(),
                    Convert.ToDateTime(txtdate.Text.Trim()).Date,
                    cmbBook.Text.Trim(),
                    cmbLevel.Text.Trim());
                if (_disList != null)
                {
                    var _custDis = _disList.Where(c => c.Sgdd_cust_cd == txtCustomer.Text.Trim().ToUpper()).FirstOrDefault();
                    if (_custDis != null)
                    {
                        _supDisAva = true;
                        txtDisRate.Text = _custDis.Sgdd_disc_rt.ToString("");
                        txtDisRate_TextChanged(null, null);
                    }
                }
            }
            if (!_supDisAva)
            {
                txtDisRate.Text = "0";
            }
            txtDisAmt.Text = "0";
            WarrantyPeriod = 0;
            WarrantyRemarks = string.Empty;
            bool _IsTerminate = false;
            ManagerDiscount = new Dictionary<decimal, decimal>();
            SSPriceBookSequance = "0";
            SSPriceBookItemSequance = "0";
            SSPriceBookPrice = 0;
            if (_isCompleteCode == false)
                if (CheckInventoryCombine())
                {
                    DisplayMessage("This compete code does not having a collection. Please contact inventory !!!");
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            if (CheckQtyPriliminaryRequirements()) return true;

            if (_isCombineAdding == false)
                if (CheckTaxAvailability())
                {
                    DisplayMessage("Tax rates not setup for selected item code and item status.Please contact Inventory Department !!!");
                    _IsTerminate = true;
                    return _IsTerminate;
                }

            if (_isCombineAdding == false) CheckItemTax(txtItem.Text.Trim());
            if (_isCombineAdding == false)
                if (CheckProfitCenterAllowForWithoutPrice())
                {
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            if (_isCombineAdding == false)
                if (ConsumerItemProduct())
                {
                    _IsTerminate = true;
                    //return _IsTerminate;
                }
            if (_isSearchPromotion)
            {
                int isRetunrToJS = 0;
                //CheckItemPromotion(out isRetunrToJS);
                if (CheckItemPromotion(out isRetunrToJS))
                {
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            }

            if (_priceBookLevelRef != null)
            {
                if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized)
                    if (CheckSerializedPriceLevelAndLoadSerials(true))
                    {
                        _IsTerminate = true;
                        CalculateItem();
                        return _IsTerminate;
                    }
            }
            if (IsGiftVoucher(_itemdetail.Mi_itm_tp)) return true;
            GeneralDiscount_new = CHNLSVC.Sales.GetDocPriceDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbBook.Text.Trim(), cmbLevel.Text.Trim(), cmbInvType.Text);
            //if (IsVirtual(_itemdetail.Mi_itm_tp) && _isCompleteCode == false)
            //{
            //    txtUnitPrice.ReadOnly = false;
            //    txtDisRate.ReadOnly = false;
            //    txtDisAmt.ReadOnly = false;
            //    txtUnitAmt.ReadOnly = true;
            //    txtTaxAmt.ReadOnly = true;
            //    txtLineTotAmt.ReadOnly = true;
            //    return true;
            //}

            if (GeneralDiscount_new.SADD_IS_EDIT == 1)
            {
                txtUnitPrice.ReadOnly = false;
                txtDisRate.ReadOnly = false;
                txtDisAmt.ReadOnly = false;
                txtUnitAmt.ReadOnly = true;
                txtTaxAmt.ReadOnly = true;
                txtLineTotAmt.ReadOnly = true;
            }
            else
            {
                txtUnitPrice.ReadOnly = true;
                txtUnitAmt.ReadOnly = true;
                txtTaxAmt.ReadOnly = true;
                txtLineTotAmt.ReadOnly = true;
                if (_itemdetail.Mi_itm_tp == "V")
                {
                    txtDisRate.ReadOnly = true;
                    txtDisAmt.ReadOnly = true;
                }
                else
                {
                    txtDisRate.ReadOnly = false;
                    txtDisAmt.ReadOnly = false;
                }
            }


            if (GeneralDiscount_new.SADD_IS_DISC == 1)
            {
                txtDisRate.ReadOnly = false;
                txtDisAmt.ReadOnly = false;
            }
            else
            {
                txtDisRate.ReadOnly = true;
                txtDisAmt.ReadOnly = true;
            }
            if (GeneralDiscount_new.SADD_IS_EDIT == 1)
            {
                txtUnitPrice.ReadOnly = false;
                txtUnitAmt.ReadOnly = false;
            }
            else
            {
                txtUnitPrice.ReadOnly = true;
                txtUnitAmt.ReadOnly = true;
            }
            _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtdate.Text));
            if (_priceDetailRef.Count <= 0)
            {
                if (!_isCompleteCode)
                {
                    // if (Isrequestbase = false)
                    // {
                    //divalert.Visible = true;
                    DisplayMessage("There is no assigned normal price for the selected item under the selected level");
                    SetDecimalTextBoxForZero(true, false, true);
                    _IsTerminate = true;
                    return _IsTerminate;
                    //}
                }
                else
                {
                    txtUnitPrice.Text = "0";
                }
            }
            else
            {
                if (_isCompleteCode)
                {
                    List<PriceDetailRef> _new = _priceDetailRef;
                    _priceDetailRef = new List<PriceDetailRef>();
                    var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                    if (_p != null)
                        if (_p.Count > 0)
                        {
                            if (_p.Count > 1) _p = _p.Where(x => x.Sapd_price_type == 0).ToList();
                            _priceDetailRef.Add(_p[0]);
                        }
                }
                if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                {
                    var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                    if (_isSuspend > 0)
                    {
                        DisplayMessage("Price has been suspended. ");
                        _IsTerminate = true;
                        return _IsTerminate;
                    }
                }
                if (_priceDetailRef.Count > 1)
                {
                    SetColumnForPriceDetailNPromotion(false);
                    //gvNormalPrice.DataSource = new List<PriceDetailRef>();
                    //gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                    //gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                    //gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                    BindNonSerializedPrice(_priceDetailRef);
                    _IsTerminate = true;

                    return _IsTerminate;
                }
                else if (_priceDetailRef.Count == 1)
                {
                    var _one = from _itm in _priceDetailRef
                               select _itm;
                    int _priceType = 0;
                    foreach (var _single in _one)
                    {
                        _priceType = _single.Sapd_price_type;
                        PriceTypeRef _promotion = TakePromotion(_priceType);
                        decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                        UnitPrice = _single.Sapd_itm_price;
                        txtUnitPrice.Text = (Convert.ToString(UnitPrice));
                        txtUnitPrice.Text = UnitPrice.ToString("N2");
                        WarrantyRemarks = _single.Sapd_warr_remarks;
                        SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                        Int32 _pbSq = _single.Sapd_pb_seq;
                        Int32 _pbiSq = _single.Sapd_seq_no;
                        string _mItem = _single.Sapd_itm_cd;
                        //if (_promotion.Sarpt_is_com)
                        //{
                        SetColumnForPriceDetailNPromotion(false);
                        //gvNormalPrice.DataSource = new List<PriceDetailRef>();
                        //gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                        //gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                        //gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                        BindNonSerializedPrice(_priceDetailRef);

                        if (_isCombineAdding == false) txtUnitPrice.Focus();

                        //}
                        //else
                        //{
                        //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                        //}
                    }
                }
            }
            _isEditPrice = false;
            _isEditDiscount = false;
            if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = "0";
            decimal vals = Convert.ToDecimal(txtQty.Text);
            txtQty.Text = (Convert.ToString(vals));
            CalculateItem();

            //get price for priority pb
            if (_priorityPriceBook != null && cmbBook.SelectedValue != _priorityPriceBook.Sppb_pb && cmbLevel.SelectedValue != _priorityPriceBook.Sppb_pb_lvl)
            {
                decimal normalPrice = Convert.ToDecimal(txtLineTotAmt.Text);

                _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, _priorityPriceBook.Sppb_pb, _priorityPriceBook.Sppb_pb_lvl, txtCustomer.Text, txtItem.Text.Trim(), Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtdate.Text));
                string _unitPrice = "";
                if (_priceDetailRef.Count <= 0)
                {
                    return false;
                }

                if (_priceDetailRef.Count <= 0)
                {
                    if (!_isCompleteCode)
                    {
                        //this.Cursor = Cursors.Default;
                        //using (new CenterWinDialog(this)) { MessageBox.Show("There is no price for the selected item", "No Price", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        //SetDecimalTextBoxForZero(true, false, true);
                        return false;
                    }
                    else
                    {
                        _unitPrice = "0";
                    }
                }
                else
                {
                    if (_isCompleteCode)
                    {
                        List<PriceDetailRef> _new = _priceDetailRef;
                        _priceDetailRef = new List<PriceDetailRef>();
                        var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                        if (_p != null)
                            if (_p.Count > 0)
                            {
                                if (_p.Count > 1) _p = _p.Where(x => x.Sapd_price_type == 0).ToList();
                                _priceDetailRef.Add(_p[0]);
                            }
                    }
                    if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                    {
                        var _isSuspend = _priceDetailRef.Where(x => x.Sapd_price_stus == "S").Count();
                        if (_isSuspend > 0)
                        {
                            return false;
                        }
                    }
                    if (_priceDetailRef.Count > 1)
                    {
                        /*
                        DialogResult _result = new DialogResult();
                        using (new CenterWinDialog(this)) { _result = MessageBox.Show("This item has " +_priorityPriceBook.Sppb_pb + " " + _priorityPriceBook.Sppb_pb_lvl + " Promotion."+"Do you want to select " + _priorityPriceBook.Sppb_pb + " Promotion?", "Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                        if (_result == DialogResult.Yes)
                        {
                            SetColumnForPriceDetailNPromotion(false);
                            gvNormalPrice.DataSource = new List<PriceDetailRef>();
                            gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                            gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                            gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                            BindNonSerializedPrice(_priceDetailRef);
                            pnlPriceNPromotion.Visible = true;
                            _IsTerminate = true;
                            pnlMain.Enabled = false;

                            return _IsTerminate;
                        }
                        else {
                            return false;
                        }
                        */
                        return false;
                    }
                    else if (_priceDetailRef.Count == 1)
                    {
                        var _one = from _itm in _priceDetailRef
                                   select _itm;
                        int _priceType = 0;
                        foreach (var _single in _one)
                        {
                            _priceType = _single.Sapd_price_type;
                            PriceTypeRef _promotion = TakePromotion(_priceType);
                            decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                            _unitPrice = Convert.ToString(UnitPrice);
                            WarrantyRemarks = _single.Sapd_warr_remarks;
                            //SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                            Int32 _pbSq = _single.Sapd_pb_seq;
                            Int32 _pbiSq = _single.Sapd_seq_no;
                            string _mItem = _single.Sapd_itm_cd;
                            //if (_promotion.Sarpt_is_com)
                            //{
                            SetColumnForPriceDetailNPromotion(false);
                            //gvNormalPrice.DataSource = new List<PriceDetailRef>();
                            //gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                            //gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                            //gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                            BindNonSerializedPrice(_priceDetailRef);

                            //if (gvPromotionPrice.RowCount > 0)
                            //{
                            //    //gvPromotionPrice_CellDoubleClick(0, false, false);
                            //    //pnlPriceNPromotion.Visible = true;
                            //    //pnlMain.Enabled = false;
                            //    //_IsTerminate = true;
                            //    //return _IsTerminate;
                            //}
                            //else
                            //{
                            if (_isCombineAdding == false) txtUnitPrice.Focus();
                            //}

                            //}
                            //else
                            //{
                            //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                            //}
                        }
                    }
                }
                _isEditPrice = false;
                _isEditDiscount = false;
                if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = "0";
                decimal vals1 = Convert.ToDecimal(txtQty.Text);
                txtQty.Text = (Convert.ToString(vals1));
                decimal otherPrice = 0;
                if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(_unitPrice))
                {
                    decimal _disRate = 0;
                    decimal _disAmt = 0;
                    if (!string.IsNullOrEmpty(txtDisRate.Text))
                    {
                        _disRate = Convert.ToDecimal(txtDisRate.Text);
                    }
                    if (!string.IsNullOrEmpty(txtDisAmt.Text))
                    {
                        _disAmt = Convert.ToDecimal(txtDisAmt.Text);
                    }

                    otherPrice = CalculateItemTem(Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(_unitPrice), _disAmt, _disRate);
                }
                else
                    return false;
                //decimal otherPrice = Convert.ToDecimal(txtLineTotAmt.Text);
                //if price change display message
                if (otherPrice < normalPrice)
                {
                    // using (new CenterWinDialog(this)) { _result = MessageBox.Show(_priorityPriceBook.Sppb_pb + " " + _priorityPriceBook.Sppb_pb_lvl + " Price - " + (otherPrice.ToString()) + "Do you want to select " + _priorityPriceBook.Sppb_pb + " Price?", "Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                    string msg = _priorityPriceBook.Sppb_pb + " " + _priorityPriceBook.Sppb_pb_lvl + " Price - " + (otherPrice.ToString()) + " " + " Do you want to select " + _priorityPriceBook.Sppb_pb + " Price?";
                    lblConfText2.Text = msg;
                    mpConfirmation2.Show();

                    _IsTerminate = true;
                    return _IsTerminate;

                    //if (_result == DialogResult.Yes)
                    //{
                    //    txtUnitPrice.Text = ("0");
                    //    txtUnitAmt.Text = ("0");
                    //    txtDisRate.Text = ("0");
                    //    txtDisAmt.Text = ("0");
                    //    txtTaxAmt.Text = ("0");
                    //    txtLineTotAmt.Text = ("0");
                    //    cmbBook.Text = _priorityPriceBook.Sppb_pb;
                    //    cmbLevel.Text = _priorityPriceBook.Sppb_pb_lvl;
                    //    CheckQty(false);
                    //}
                    //else
                    //{
                    //    SSPRomotionType = 0;
                    //    SSPromotionCode = string.Empty;
                    //}
                }
            }

            return _IsTerminate;
        }

        private bool LoadMultiCombineItem(string _item)
        {
            bool _isMultiCom = false;
            DataTable _invnetoryCombinAnalalize = CHNLSVC.Inventory.GetCompeleteItemfromAssambleItem(_item);
            if (_invnetoryCombinAnalalize != null)
                if (_invnetoryCombinAnalalize.Rows.Count > 0)
                {
                    _isMultiCom = true;
                    //dgvMultipleItems.DataSource = _invnetoryCombinAnalalize;
                    //dgvMultipleItems.DataBind();
                    //mpMultipleItems.Show();

                    //gvMultiCombineItem.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    //gvMultiCombineItem.DataSource = _invnetoryCombinAnalalize;
                    //_isMultiCom = true;
                    //pnlMain.Enabled = false;
                    //pnlMultiCombine.Visible = true;
                    //gvMultiCombineItem.Focus();
                }
            return _isMultiCom;
        }

        private bool LoadMultiCombinItem(string _item)
        {
            bool _isManyItem = false;
            if (LoadMultiCombineItem(_item))
            {
                _isManyItem = true;
            }
            return _isManyItem;
        }

        private List<ReptPickSerials> GetAgeItemList(DateTime _date, bool _isAgePriceLevel, int _noOfDays, List<ReptPickSerials> _referance)
        {
            List<ReptPickSerials> _ageLst = new List<ReptPickSerials>();
            if (_isAgePriceLevel)
                _ageLst = _referance.Where(x => x.Tus_exist_grndt <= _date.AddDays(-_noOfDays)).ToList();
            else
                _ageLst = _referance;
            return _ageLst;
        }

        private void CheckNValidateAgeItem(string _itemc, string _itemcategory, string _bookc, string _levelc, string _status, out bool IsAgePriceLevel, out int AgeDays)
        {
            bool _isAgePriceLevel = false;
            int _ageingDays = -1;
            MasterItem _item = null;
            if (string.IsNullOrEmpty(_itemcategory))
            { _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itemc); if (_item != null) _itemcategory = _item.Mi_cate_1; }
            List<PriceBookLevelRef> _level = _priceBookLevelRefList;
            if (_level != null)
                if (_level.Count > 0)
                {
                    var _lvl = _level.Where(x => x.Sapl_isage && x.Sapl_itm_stuts == _status).ToList();
                    if (_lvl != null) if (_lvl.Count() > 0)
                            _isAgePriceLevel = true;
                }
            if (_isAgePriceLevel)
            {
                DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_itemcategory);
                if (_categoryDet != null && _categoryDet.Rows.Count > 0)
                {
                    if (_categoryDet.Rows[0]["ric1_age"] != DBNull.Value)
                        _ageingDays = Convert.ToInt32(_categoryDet.Rows[0].Field<Int16>("ric1_age"));
                    else _ageingDays = 0;
                }
            }

            IsAgePriceLevel = _isAgePriceLevel;
            AgeDays = _ageingDays;
        }

        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            lblItemSerialStatus.Text = string.Empty;
            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item))
            {
                _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            }
            if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
            {
                _isValid = true;
                string _description = _itemdetail.Mi_longdesc;
                string _model = _itemdetail.Mi_model;
                string _brand = _itemdetail.Mi_brand;
                string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "Non";

                lblItemDescription.Text = _description;
                lblItemModel.Text = _model;
                lblItemBrand.Text = _brand;
                lblItemSerialStatus.Text = _serialstatus;
                Session["_itemSerializedStatus"] = _itemdetail.Mi_is_ser1;
            }
            else _isValid = false;
            return _isValid;
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.SalesOrder:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "SO" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + cmbInvType.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevel:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + cmbInvType.Text + seperator + cmbBook.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + cmbBook.Text + seperator + cmbLevel.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtexcutive.Text);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "SEX" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank:
                    {
                        paramsText.Append(CommonUIDefiniton.BusinessEntityType.BANK.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + CommonUIDefiniton.PayMode.ADVAN.ToString() + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtPerTown.Text.ToString().Trim() /*Session["UserDefLoca"].ToString()*/ + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Group_Sale:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + 1 + seperator);
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.QuotationForInvoice:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + txtdate.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, Session["UserCompanyCode"].ToString());
                        if (_masterBusinessCompany != null && _masterBusinessCompany.Mbe_intr_com && _masterBusinessCompany.Mbe_tp == "C")
                        {
                            List<InterCompanySalesParameter> oInterCompanySalesParameters = CHNLSVC.Sales.GET_INTERCOM_PAR_BY_CUST(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                            if (oInterCompanySalesParameters != null && oInterCompanySalesParameters.Count > 0)
                            {
                                paramsText.Append(oInterCompanySalesParameters[0].Sritc_to_com + seperator);
                                break;
                            }
                            else
                            {
                                DisplayMessageJS("Inter company customer parameters not setuped");
                                paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                                break;
                            }
                        }
                        else
                        {
                            paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                            break;
                        }
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(Session["UserID"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableGiftVoucher:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "ITEM" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard:
                    {
                        paramsText.Append(txtCustomer.Text.Trim() + seperator + txtdate.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesOrderNew:
                    {

                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + txtFDate.Text + seperator + txtTDate.Text);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append("ADVAN" + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
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

        private void LoadExecutive()
        {
            cmbExecutive.DataSource = null;
            txtexcutive.Text = string.Empty;
            DataTable _tblExecutive = CHNLSVC.Sales.GetProfitCenterAllocatedExecutive(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            if (_tblExecutive != null)
            {
                var _lst = _tblExecutive.AsEnumerable().Where(X => X.Field<string>("esep_cat_cd") != "TECH").ToList();
                cmbExecutive.DataValueField = "esep_epf";
                cmbExecutive.DataTextField = "esep_first_name";
                if (_lst != null && _lst.Count > 0)
                    cmbExecutive.DataSource = _lst.CopyToDataTable();
                cmbExecutive.DataBind();
                if (_tblExecutive != null)
                {
                    if (cmbExecutive.Items.FindByValue(_MasterProfitCenter.Mpc_man) != null)
                    {
                        cmbExecutive.SelectedValue = _MasterProfitCenter.Mpc_man;
                    }
                }
                if (_MasterProfitCenter.Mpc_chnl == "ELITE")
                {
                    cmbExecutive.SelectedIndex = -1;
                }

                cmbExecutive.Items.Insert(0, new ListItem("Select", "0"));
            }
        }

        private void SetGridViewAutoColumnGenerate()
        {
            gvInvoiceItem.AutoGenerateColumns = false;
            gvPopSerial.AutoGenerateColumns = false;

            //gvDisItem.AutoGenerateColumns = false;
            //gvNormalPrice.AutoGenerateColumns = false;
            //gvPopComItem.AutoGenerateColumns = false;
            //gvPopComItemSerial.AutoGenerateColumns = false;
            //gvPopConsumPricePick.AutoGenerateColumns = false;
            //gvPromotionItem.AutoGenerateColumns = false;
            //gvPromotionPrice.AutoGenerateColumns = false;
            //gvPromotionSerial.AutoGenerateColumns = false;
            //gvRePayment.AutoGenerateColumns = false;
        }

        public void Invoice()
        {
            //try
            //{

            if (string.IsNullOrEmpty(Session["UserDefProf"].ToString()))
            {
                //divalert.Visible = true;
                DisplayMessage("You do not have assigned a profit center. Invoice is terminating now !!!");
                return;
            }
            if (string.IsNullOrEmpty(Session["UserDefLoca"].ToString()))
            {
                //divalert.Visible = true;
                DisplayMessage("You do not have assigned a delivery location. Invoice is de-activating delivery option now !!!");
                return;
            }
            else
            {
                LoadCachedObjects();
                SetGridViewAutoColumnGenerate();
                // SetPanelSize();
                // InitializeValuesNDefaultValueSet();
                TextBox _txt = new TextBox();
            }
            //}
            //catch (Exception ex)
            //{
            //    //divalert.Visible = true;
            //    DisplayMessage(ex.Message, 4);
            //}
            //finally
            //{
            //    CHNLSVC.CloseAllChannels();
            //}
        }

        private void LoadCachedObjects()
        {
            _MasterProfitCenter = (MasterProfitCenter)Session["MasterProfitCenter_1"];//CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
            _PriceDefinitionRef = (List<PriceDefinitionRef>)Session["_PriceDefinitionRef_1"];// CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
            MasterChannel = (DataTable)Session["ChannelDefinition_"];//CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
            IsSaleFigureRoundUp = (bool)Session["IsSaleValueRoundUp_1"];//CacheLayer.Get(CacheLayer.Key.IsSaleValueRoundUp.ToString());
            _docPriceDefnforprofitcentr = (SarDocumentPriceDefn)Session["MasterProfitCenter_2"];
            //_MasterProfitCenter = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            //_PriceDefinitionRef = CHNLSVC.Sales.GetPriceDefinitionByBookAndLevel(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, string.Empty, Session["UserDefProf"].ToString());
            //MasterChannel = CHNLSVC.Inventory.GetChannelDetail(Session["UserCompanyCode"].ToString(), _MasterProfitCenter.Mpc_chnl);
            //IsSaleFigureRoundUp = CHNLSVC.General.IsSaleFigureRoundUp(Session["UserCompanyCode"].ToString());

        }

        private bool LoadPriceLevel(string _invoiceType, string _book)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _levels = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == cmbInvType.Text && x.Sadd_pb == _book).Select(y => y.Sadd_p_lvl).Distinct().ToList();
                    _levels.Add("");
                    //filter non for sales 20/Apr/2016
                    for (int i = _levels.Count - 1; i >= 0; i--)
                    {
                        PriceBookLevelRef _filterLevel = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), _book, _levels[i]);
                        if (_filterLevel.Sapl_is_sales == false)
                        {
                            _levels.Remove(_levels[i]);
                        }

                    }


                    cmbLevel.DataTextField = "";
                    cmbLevel.DataValueField = "";
                    cmbLevel.DataSource = _levels;
                    cmbLevel.DataBind();
                    //cmbLevel.SelectedIndex = cmbLevel.Items.Count - 1;
                    var _def = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == cmbInvType.Text && x.Sadd_pb == _book && x.Sadd_def == true).ToList();
                    //var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList();

                    if ((_def != null) && (_def.Count > 0))
                    {
                        if (_def.Count >= 1)
                        {
                            DefaultLevel = _def[0].Sadd_p_lvl;
                        }
                        else
                        {
                            DefaultLevel = "";
                            cmbLevel.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        DefaultLevel = "";
                        // cmbLevel.SelectedIndex = 0;
                    }

                    if (!string.IsNullOrEmpty(DefaultLevel) && !string.IsNullOrEmpty(cmbBook.Text))
                        cmbLevel.Text = DefaultLevel;
                    _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(Session["UserCompanyCode"].ToString(), _book.Trim(), cmbLevel.Text.Trim());
                    LoadPriceLevelMessage();
                }
                else
                    cmbLevel.DataSource = null;
            else cmbLevel.DataSource = null;

            return _isAvailable;
        }

        private void LoadPriceDefaultValue()
        {
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    var _defaultValue = _PriceDefinitionRef.Where(x => x.Sadd_def == true).ToList();
                    if (_defaultValue != null)
                        if (_defaultValue.Count > 0)
                        {
                            DefaultInvoiceType = _defaultValue[0].Sadd_doc_tp;
                            DefaultBook = _defaultValue[0].Sadd_pb;
                            DefaultLevel = _defaultValue[0].Sadd_p_lvl;
                            DefaultStatus = _defaultValue[0].Sadd_def_stus;
                            DefaultItemStatus = _defaultValue[0].Sadd_def_stus;
                            LoadInvoiceType();
                            LoadPriceBook(cmbInvType.Text);
                            LoadPriceLevel(cmbInvType.Text, cmbBook.Text.Trim());
                            LoadLevelStatus(cmbInvType.Text, cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                            CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                        }
                } cmbTitle.SelectedIndex = 0;
        }

        private void LoadInvoiceProfitCenterDetail()
        {
            if (_MasterProfitCenter != null) if (_MasterProfitCenter.Mpc_cd != null)
                {
                    if (GeneralDiscount_new.SADD_IS_EDIT != 1) txtUnitPrice.ReadOnly = true;
                    txtCustomer.Text = _MasterProfitCenter.Mpc_def_customer;

                    string COMPANYCURRE = "";
                    DataTable CompanyCurrancytbl = CHNLSVC.CommonSearch.SearchCompanyCurrancy(Session["UserCompanyCode"].ToString());
                    if (CompanyCurrancytbl.Rows.Count > 0)
                    {
                        COMPANYCURRE = CompanyCurrancytbl.Rows[0]["CURRENCY"].ToString();
                    }
                    DataTable ERateTbl = CHNLSVC.Financial.GetExchangeRate(Session["UserCompanyCode"].ToString(), _MasterProfitCenter.Mpc_def_exrate, COMPANYCURRE);
                    if (ERateTbl.Rows.Count > 0)
                    {

                        lblcurrency.Text = _MasterProfitCenter.Mpc_def_exrate + "(" + ERateTbl.Rows[0][5].ToString() + ")";
                    }

                    // lblcurrency.Text = _MasterProfitCenter.Mpc_def_exrate + " - Sri Lankan Rupees";
                    txtcurrency.Text = _MasterProfitCenter.Mpc_def_exrate;
                    txtexcutive.Text = _MasterProfitCenter.Mpc_man;
                    loadExname(txtexcutive.Text);
                    _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCustomer.Text, string.Empty, string.Empty, "C");
                }
        }
        private void loadExname(string code)
        {
            DataTable _recallemp = CHNLSVC.Sales.GetinvEmp(Session["UserCompanyCode"].ToString(), code);
            string _name = string.Empty;
            string _code = "";
            if (_recallemp != null && _recallemp.Rows.Count > 0)
            {
                _name = _recallemp.Rows[0].Field<string>("esep_first_name");
                _code = _recallemp.Rows[0].Field<string>("esep_epf");
            }
            //lblSalesEx.Text = _name;
            // txtexcutive.Text = _code;
        }
        private void CheckPriceLevelStatusForDoAllow(string _level, string _book)
        {
            if (!string.IsNullOrEmpty(_level.Trim()) && !string.IsNullOrEmpty(_book.Trim()))
            {
                List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
                if (_lvl != null)
                    if (_lvl.Count > 0)
                    {
                        var _bool = (from _l in _lvl where _l.Sapl_chk_st_tp == true select _l.Sapl_chk_st_tp).ToList();
                        if (_bool != null && _bool.Count() > 0)
                            IsPriceLevelAllowDoAnyStatus = false;
                        else IsPriceLevelAllowDoAnyStatus = true;
                    }
            }
            else
                IsPriceLevelAllowDoAnyStatus = true;
        }

        private bool LoadLevelStatus(string _invType, string _book, string _level)
        {
            _levelStatus = null;
            bool _isAvailable = false;
            string _initPara = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PriceLevelItemStatus);
            _levelStatus = CHNLSVC.CommonSearch.GetPriceLevelItemStatusData(_initPara, null, null);
            if (_levelStatus != null)
                if (_levelStatus.Rows.Count > 0)
                {

                    //Code
                    //Description
                    _isAvailable = true;
                    var _types = _levelStatus.AsEnumerable().Select(x => x.Field<string>("Code")).Distinct().ToList();
                    _types.Add("");
                    cmbStatus.DataSource = null;
                    cmbStatus.SelectedIndex = -1;
                    cmbStatus.DataSource = _levelStatus;
                    cmbStatus.DataTextField = "Description";
                    cmbStatus.DataValueField = "Code";
                    cmbStatus.DataBind();
                    cmbStatus.SelectedIndex = cmbStatus.Items.Count - 1;

                    DataRow[] result = _levelStatus.Select("Code='" + DefaultStatus + "'");

                    foreach (DataRow row in result)
                    {
                        if (!string.IsNullOrEmpty(DefaultInvoiceType))
                            cmbStatus.SelectedValue = DefaultStatus;
                    }
                    _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), cmbBook.Text, cmbLevel.Text);
                    LoadPriceLevelMessage();
                    cmbStatus.Items.Insert(0, new ListItem("Select", "0"));
                }
                else
                    cmbStatus.DataSource = null;
            else
                cmbStatus.DataSource = null;
            return _isAvailable;
        }
        private bool IsAllowBackDateForModuleNew(string _com, string _loc, string _pc, System.Web.UI.Page _page, string _backdate, LinkButton _imgcontrol, Label _lblcontrol, String moduleName, out bool _allowCurrentTrans)
        {
            string _outmsg = "";
            BackDates _bdt = new BackDates();
            _imgcontrol.Visible = false;
            _allowCurrentTrans = false;
            string _filename = string.Empty;

            if (String.IsNullOrEmpty(moduleName))
            {
                _filename = _page.AppRelativeVirtualPath.Substring(_page.AppRelativeVirtualPath.LastIndexOf("/") + 1).Replace(".aspx", "").ToUpper();
            }
            else
            {
                _filename = moduleName;
            }
            bool _isAllow = CHNLSVC.General.IsAllowBackDateForModule(_com, _loc, _pc, _filename, _backdate, out _bdt);
            if (_isAllow == true)
            {
                _imgcontrol.Visible = true;
                if (_bdt != null)
                {
                    _outmsg = "This module back dated. Valid period is from " + _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy") + " date to " + _bdt.Gad_act_to_dt.Date.ToString("dd/MMM/yyyy") + " date.";
                    txtdate.Text = _bdt.Gad_act_from_dt.Date.ToString("dd/MMM/yyyy");
                }
            }

            if (_bdt == null)
            {
                _allowCurrentTrans = true;

            }
            else
            {
                if (_bdt.Gad_alw_curr_trans == true)
                {
                    _allowCurrentTrans = true;
                    lbtnimgselectdate.Visible = true;
                }
                else
                {
                    lbtnimgselectdate.Visible = false;
                }
            }

            //MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _outmsg);
            _lblcontrol.Text = _outmsg;

            return _isAllow;
        }

        private void BackDatePermission()
        {
            try
            {
                _isBackDate = false;
                bool _allowCurrentTrans = false;
                LinkButton btntest = new LinkButton();
                if (IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), "", Session["UserDefProf"].ToString(), this.Page, "", btntest, lblBackDateInfor, "m_Trans_Sales_SalesInvoice", out  _allowCurrentTrans))
                {
                    CalendarExtender3.Enabled = true;
                    lbtnimgselectdate.CssClass = "buttonUndocolor";
                }
                else
                {
                    CalendarExtender3.Enabled = false;
                    lbtnimgselectdate.CssClass = "buttoncolor";
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        private void Invoice_Load()
        {
            try
            {
                BackDatePermission();
                List<PriceDefinitionRef> tem = (from _res in _PriceDefinitionRef
                                                where _res.Sadd_def_pb
                                                select _res).ToList<PriceDefinitionRef>();
                if (tem != null && tem.Count > 0)
                {
                    _priorityPriceBook = new PriortyPriceBook();
                    _priorityPriceBook.Sppb_pb = tem[0].Sadd_pb;
                    _priorityPriceBook.Sppb_pb_lvl = tem[0].Sadd_p_lvl;
                }
                if (_MasterProfitCenter.Mpc_is_do_now == 0)
                {
                    ////chkDeliverLater.Checked = false;
                    ////chkDeliverNow.Checked = false;
                    ////chkDeliverLater_CheckedChanged(null, null);
                }
                else if (_MasterProfitCenter.Mpc_is_do_now == 1)
                {
                    ////chkDeliverNow.Checked = true;
                    ////chkDeliverLater.Checked = false;
                    ////chkDeliverNow_CheckedChanged(null, null);
                }
                else
                {
                    ////chkDeliverLater.Checked = true;
                    ////chkDeliverNow.Checked = false;
                    ////chkDeliverLater_CheckedChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private bool LoadInvoiceType()
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _types = _PriceDefinitionRef.Where(X => !X.Sadd_doc_tp.Contains("HS")).Select(x => x.Sadd_doc_tp).Distinct().ToList();
                    if (_types.Count > 0)
                    {
                        cmbInvType.DataSource = _types;
                        cmbInvType.DataTextField = "";
                        cmbInvType.DataValueField = "";
                        cmbInvType.DataBind();
                        cmbInvType.SelectedIndex = cmbInvType.Items.Count - 1;

                        if (cmbInvType.Text.Trim() == "CS")
                            txtCustomer.Text = "CASH";
                    }

                    if (!string.IsNullOrEmpty(DefaultInvoiceType))
                    {
                        cmbInvType.Text = DefaultInvoiceType;
                    }
                }
                else
                    cmbInvType.DataSource = null;
            else
                cmbInvType.DataSource = null;

            return _isAvailable;
        }

        private void LoadCusData()
        {
            try
            {
                DataTable dtcusdata = CHNLSVC.Sales.SearchCustomer2(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                cmbTitle.SelectedIndex = 0;
                if (dtcusdata.Rows.Count > 0)
                {
                    foreach (DataRow item in dtcusdata.Rows)
                    {
                        txtNIC.Text = item[0].ToString();
                        txtMobile.Text = item[1].ToString();
                        txtCusName.Text = item[3].ToString();
                        txtAddress1.Text = item[4].ToString();
                        txtAddress2.Text = item[5].ToString();
                        if (item[7].ToString() == "0")
                        {
                            chkTaxPayable.Checked = false;
                        }
                        else
                        {
                            chkTaxPayable.Checked = true;
                        }
                        //if (item[9].ToString() == "0")
                        //{
                        //    lblSVatStatus.Text = "None";
                        //}
                        //else
                        //{
                        //    lblSVatStatus.Text = "Available";
                        //}
                        if (item[9].ToString() == "1")
                        {
                            lblSVatStatus.Text = "Available";
                        }
                        else
                        {
                            lblSVatStatus.Text = "None";
                        }
                        //if (item[8].ToString() == "0")
                        //{
                        //    lblVatExemptStatus.Text = "None";
                        //}
                        //else
                        //{
                        //    lblVatExemptStatus.Text = "Available";
                        //}
                        if (item[8].ToString() == "1")
                        {
                            lblVatExemptStatus.Text = "Available";
                        }
                        else
                        {
                            lblVatExemptStatus.Text = "None";
                        }
                        string _title = item[2].ToString();
                        if (!string.IsNullOrEmpty(_title))
                        {
                            cmbTitle.SelectedValue = item[2].ToString();
                        }


                        txtdelcuscode.Text = txtCustomer.Text.Trim();
                        txtdelname.Text = item[3].ToString();
                        txtdelad1.Text = item[4].ToString();
                        txtdelad2.Text = item[5].ToString();
                        txtdellocation.Text = Session["UserDefProf"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }


        private void LoadCusData_Del()
        {
            try
            {
                DataTable dtcusdata = CHNLSVC.Sales.SearchCustomer(Session["UserCompanyCode"].ToString(), txtdelcuscode.Text.Trim());
                cmbTitle.SelectedIndex = 0;
                if (dtcusdata.Rows.Count > 0)
                {
                    foreach (DataRow item in dtcusdata.Rows)
                    {
                        // txtNIC.Text = item[0].ToString();
                        // txtMobile.Text = item[1].ToString();
                        txtdelname.Text = item[3].ToString();
                        txtdelad1.Text = item[4].ToString();
                        txtdelad2.Text = item[5].ToString();
                        // cmbTitle.SelectedValue = item[2].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void LoadCusLoyalityNo()
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable dtloyal = CHNLSVC.CommonSearch.SearchLoyaltyCard(SearchParams, null, null);
                if (dtloyal.Rows.Count > 0)
                {
                    foreach (DataRow item in dtloyal.Rows)
                    {
                        txtLoyalty.Text = item[0].ToString();
                    }
                    SetLoyalityColor();
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void ClearCustomer(bool _isCustomer)
        {
            try
            {
                if (_isCustomer)
                {
                    txtCustomer.Text = string.Empty;
                }
                txtCusName.Text = string.Empty;
                txtAddress1.Text = string.Empty;
                txtAddress2.Text = string.Empty;
                txtMobile.Text = string.Empty;
                txtNIC.Text = string.Empty;
                chkTaxPayable.Checked = false;
                txtLoyalty.Text = string.Empty;
                SetLoyalityColor();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void SetCustomerAndDeliveryDetailsGroup(GroupBussinessEntity _cust)
        {
            try
            {
                txtCustomer.Text = _cust.Mbg_cd;
                txtCusName.Text = _cust.Mbg_name;
                txtAddress1.Text = _cust.Mbg_add1;
                txtAddress2.Text = _cust.Mbg_add2;
                txtMobile.Text = _cust.Mbg_mob;
                txtNIC.Text = _cust.Mbg_nic;

                //txtDelAddress1.Text = _cust.Mbg_add1;
                //txtDelAddress2.Text = _cust.Mbg_add2;
                //txtDelCustomer.Text = _cust.Mbg_cd;
                //txtDelName.Text = _cust.Mbg_name;

                chkTaxPayable.Checked = false;
                chkTaxPayable.Enabled = false;

                cmbTitle.Text = _cust.Mbg_tit;
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void SetCustomerAndDeliveryDetails(bool _isRecall, InvoiceHeader _hdr)
        {
            try
            {
                txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
                txtCusName.Text = _masterBusinessCompany.Mbe_name;
                txtAddress1.Text = _masterBusinessCompany.Mbe_add1;
                txtAddress2.Text = _masterBusinessCompany.Mbe_add2;
                txtMobile.Text = _masterBusinessCompany.Mbe_mob;
                txtNIC.Text = _masterBusinessCompany.Mbe_nic;
                if (_masterBusinessCompany.MBE_TIT == "")
                {
                    cmbTitle.SelectedItem.Text = "MR.";
                }
                else
                {
                    cmbTitle.SelectedItem.Text = _masterBusinessCompany.MBE_TIT;

                }

                _entity = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCustomer.Text, string.Empty, string.Empty, "C");
                if (_entity != null)
                    if (_entity.Mbe_cd != null)
                        if (!string.IsNullOrEmpty(_entity.Mbe_cust_com) && !string.IsNullOrEmpty(_entity.Mbe_cust_loc))
                        { Session["CUSHASCOMPANY"] = true; Session["CUSCOM"] = _entity.Mbe_cust_com; Session["CUSLOC"] = _entity.Mbe_cust_loc; }

                // txtPerTown.Text = _masterBusinessCompany.Mbe_town_cd.ToUpper();

                if (_isRecall == false)
                {
                    txtdelad1.Text = _masterBusinessCompany.Mbe_add1;
                    txtdelad2.Text = _masterBusinessCompany.Mbe_add2;
                    txtdelcuscode.Text = _masterBusinessCompany.Mbe_cd;
                    txtdelname.Text = _masterBusinessCompany.Mbe_name;
                    txtdellocation.Text = Session["UserDefLoca"].ToString();
                }
                else
                {
                    txtCusName.Text = _hdr.Sah_cus_name;
                    txtAddress1.Text = _hdr.Sah_cus_add1;
                    txtAddress2.Text = _hdr.Sah_cus_add2;

                    txtdelad1.Text = _hdr.Sah_d_cust_add1;
                    txtdelad2.Text = _hdr.Sah_d_cust_add2;
                    txtdelcuscode.Text = _hdr.Sah_d_cust_cd;
                    txtdelname.Text = _hdr.Sah_d_cust_name;
                    txtdellocation.Text = _hdr.Sah_del_loc;
                }

                if (_isRecall == false)
                {
                    if (_masterBusinessCompany.Mbe_is_tax) { chkTaxPayable.Checked = true; chkTaxPayable.Enabled = true; } else { chkTaxPayable.Checked = false; chkTaxPayable.Enabled = false; }
                }

                if (string.IsNullOrEmpty(txtNIC.Text)) { cmbTitle.SelectedIndex = 0; return; }
                if (IsValidNIC(txtNIC.Text) == false) { cmbTitle.SelectedIndex = 0; return; }
                GetNICGender();
                if (string.IsNullOrEmpty(txtCusName.Text)) txtCusName.Text = cmbTitle.Text.Trim();
                else
                {
                    string _title = ExtractTitleByCustomerName(txtCusName.Text.Trim());
                    ListItem li = new ListItem();
                    li.Text = _title;
                    bool _exist = cmbTitle.Items.Contains(li);
                    if (_exist)
                        cmbTitle.Text = _title;
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        public string ExtractTitleByCustomerName(string _customerName)
        {
            string[] _splits = _customerName.Split('.');
            StringBuilder _actualTitle = new StringBuilder(string.Empty);
            if (_splits.Length > 1)
            {
                string _title = _splits[0];
                _actualTitle.Append(_title.Substring(0, 1)); _actualTitle.Append(_title.Substring(1, _title.Length - 1).ToLower()); _actualTitle.Append(".");
            }
            return _actualTitle.ToString();
        }

        private void GetNICGender()
        {
            try
            {
                String nic_ = txtNIC.Text.Trim().ToUpper();
                char[] nicarray = nic_.ToCharArray();
                string thirdNum = (nicarray[2]).ToString();
                if (thirdNum == "5" || thirdNum == "6" || thirdNum == "7" || thirdNum == "8" || thirdNum == "9")
                {
                    cmbTitle.Text = "MS.";
                }
                else
                {
                    cmbTitle.Text = "MR.";
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void LoadTaxDetail(MasterBusinessEntity _entity)
        {
            try
            {
                lblSVatStatus.Text = _entity.Mbe_is_svat ? "Available" : "None";
                lblVatExemptStatus.Text = _entity.Mbe_tax_ex ? "Available" : "None";
                if (lblSVatStatus.Text == "Available")
                {
                    //Comment by the lakshan As per thedharshana 17 Mar 2017
                    // lblSVatStatus.Text = _entity.Mbe_svat_no;
                }
                txttaxPay.Text = _entity.Mbe_tax_no;
                // lblVatExemptStatus.Text = _entity. ? "Available" : "None";
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void ViewCustomerAccountDetail(string _customer)
        {
            try
            {
                if (string.IsNullOrEmpty(_customer.Trim())) return;
                if (_customer != "CASH")
                {
                    // CustomerAccountRef _account = CHNLSVC.Sales.GetCustomerAccount(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                    // lblAccountBalance.Text = _account.Saca_acc_bal.ToString();
                    // lblAvailableCredit.Text = (_account.Saca_crdt_lmt - _account.Saca_ord_bal - _account.Saca_acc_bal).ToString();

                    List<SunAccountBusEntity> _account = CHNLSVC.CustService.GetSunAccountDetailsforSO(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                    decimal ACC_BAL = 0;
                    decimal CREDIT_BAL = 0;
                    decimal CREDIT_LIMIT = 0;
                    decimal PENDING = 0;
                    decimal TOT_BAL = 0;
                    if (_account.Count > 0)
                    {
                        ACC_BAL = (_account[0].ACC_BALNCE * -1);
                        PENDING = (_account[0].ORD_BALNCE * -1);
                        TOT_BAL = ACC_BAL + PENDING;
                        CREDIT_LIMIT = _account[0].CREDIT_LIM;
                        CREDIT_BAL = CREDIT_LIMIT - TOT_BAL;
                    }

                    lblAvailableCredit.Text = CREDIT_BAL.ToString();
                    lblAccountBalance.Text = ACC_BAL.ToString();//(_account.Saca_crdt_lmt - _account.Saca_ord_bal - _account.Saca_acc_bal).ToString();
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        public GroupBussinessEntity GetbyCustCDGrup(string custCD)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(custCD, null, null, null, null, null);
        }

        private string ReturnLoyaltyNo()
        {
            string _no = string.Empty;
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard);
                DataTable _result = CHNLSVC.CommonSearch.SearchLoyaltyCard(SearchParams, null, null);

                if (_result != null && _result.Rows.Count > 0)
                {
                    if (_result.Rows.Count > 1)
                    {
                        //divalert.Visible = true;
                        DisplayMessage("Customer having multiple loyalty cards. Please select one of them !!!");
                        txtLoyalty.BackColor = Color.White;
                        return _no;
                    }
                    _no = _result.Rows[0].Field<string>("Card No");
                }
                else txtLoyalty.BackColor = Color.White;
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            return _no;
        }

        private void EnableDisableCustomer()
        {
            try
            {
                if (txtCustomer.Text == "CASH")
                {
                    txtCustomer.Enabled = true;
                    txtCusName.Enabled = true;
                    txtAddress1.Enabled = true;
                    txtAddress2.Enabled = true;
                    txtMobile.Enabled = true;
                    txtNIC.Enabled = true;

                    btnSearch_NIC.Enabled = true;
                    lbtncode.Enabled = true;
                    btnSearch_Mobile.Enabled = true;
                }
                else
                {
                    txtCusName.Enabled = false;
                    txtAddress1.Enabled = false;
                    txtAddress2.Enabled = false;
                    txtMobile.Enabled = false;
                    txtNIC.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void LoadCustomerDetailsByCustomer()
        {
            if (string.IsNullOrEmpty(txtCustomer.Text)) return;
            try
            {
                if (cmbInvType.Text.Trim() == "CRED" && txtCustomer.Text.Trim() == "CASH")
                {
                    DisplayMessage("You cannot select customer as CASH, because your invoice type is " + cmbInvType.Text);
                    ClearCustomer(false);
                    txtCustomer.Focus();
                    return;
                }
                _masterBusinessCompany = new MasterBusinessEntity();

                if (!string.IsNullOrEmpty(txtCustomer.Text))
                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, Session["UserCompanyCode"].ToString());

                if (_masterBusinessCompany.Mbe_cd != null)
                {
                    if (_masterBusinessCompany.Mbe_tp != "C")
                    {
                        DisplayMessage("Selected Customer is not allow for enter transaction");
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        DisplayMessage("This customer already inactive. Please contact IT dept");
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }
                    if (_masterBusinessCompany.Mbe_is_suspend == true)
                    {
                        DisplayMessage("This customer already suspended.");
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }
                    //DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                    //if (_table != null && _table.Rows.Count > 0)
                    //{
                    //    var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                    //    if (_isAvailable == null || _isAvailable.Count <= 0)
                    //    {
                    //        DisplayMessage("Customer is not allow for enter transaction under selected invoice type");
                    //        ClearCustomer(true);
                    //        txtCustomer.Focus();
                    //        return;
                    //    }
                    //}
                    //else if (cmbInvType.Text != "CS")
                    //{
                    //    DisplayMessage("Selected Customer is not allow for enter transaction under selected invoice type");
                    //    ClearCustomer(true);
                    //    txtCustomer.Focus();
                    //    return;
                    //}

                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCustomer.Text = _masterBusinessCompany.Mbe_cd;
                        SetCustomerAndDeliveryDetails(false, null);
                    }
                    else
                    {
                        LoadTaxDetail(_masterBusinessCompany);
                        SetCustomerAndDeliveryDetails(false, null);
                    }

                    ViewCustomerAccountDetail(txtCustomer.Text);
                }
                else
                {
                    _isGroup = false;
                    DisplayMessage("Please select the valid customer");
                    ClearCustomer(true);
                    txtCustomer.Focus();
                    return;
                    //GroupBussinessEntity _grupProf = GetbyCustCDGrup(txtCustomer.Text.Trim().ToUpper());
                    //if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    //{
                    //    SetCustomerAndDeliveryDetailsGroup(_grupProf);
                    //    _isGroup = true;

                    //    //DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                    //    //if (_table != null && _table.Rows.Count > 0)
                    //    //{
                    //    //    var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                    //    //    if (_isAvailable == null || _isAvailable.Count <= 0)
                    //    //    {
                    //    //        DisplayMessage("Customer is not allow for enter transaction under selected invoice type");
                    //    //        ClearCustomer(true);
                    //    //        txtCustomer.Focus();
                    //    //        return;
                    //    //    }
                    //    //}
                    //    //else if (cmbInvType.Text != "CS")
                    //    //{
                    //    //    DisplayMessage("Selected Customer is not allow for enter transaction under selected invoice type");
                    //    //    ClearCustomer(true);
                    //    //    txtCustomer.Focus();
                    //    //    return;
                    //    //}
                    //}
                    //else
                    //{
                    //    _isGroup = false;
                    //    DisplayMessage("Please select the valid customer");
                    //    ClearCustomer(true);
                    //    txtCustomer.Focus();
                    //    return;
                    //}
                }
                txtLoyalty.Text = ReturnLoyaltyNo();
                txtLoyalty_TextChanged(null, null);
                EnableDisableCustomer();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }


        public GroupBussinessEntity GetbyNICGrup(string nic)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, nic, null, null, null, null);
        }

        protected void LoadCustomerDetailsByNIC()
        {
            if (string.IsNullOrEmpty(txtNIC.Text)) { return; }
            _masterBusinessCompany = new MasterBusinessEntity();
            try
            {
                if (!string.IsNullOrEmpty(txtNIC.Text))
                {
                    if (!IsValidNIC(txtNIC.Text))
                    {
                        DisplayMessage("Please select the valid NIC");
                    }
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(Session["UserCompanyCode"].ToString(), string.Empty, txtNIC.Text, string.Empty, "C");

                    if (cmbInvType.Text.Trim() == "CRED")
                    {
                        if (_custList != null && _custList.Count > 0)
                        {
                            foreach (MasterBusinessEntity _cust in _custList)
                            {
                                if (_cust.Mbe_is_suspend == true)
                                {
                                    DisplayMessage("Customer suspend !!! - [" + _cust.Mbe_cd + " | " + _cust.Mbe_name + " For more information, please contact Accounts Dept.");
                                }
                            }
                        }
                    }

                    _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(null, txtNIC.Text, null, null, null, Session["UserCompanyCode"].ToString());
                }
                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    if (_masterBusinessCompany.Mbe_act == true)
                    {
                        LoadTaxDetail(_masterBusinessCompany);
                        SetCustomerAndDeliveryDetails(false, null);
                        ViewCustomerAccountDetail(txtCustomer.Text);
                        GetNICGender();
                    }
                    else
                    {
                        DisplayMessage("This customer already inactive. Please contact accounts dept");
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }
                }
                else
                {
                    GroupBussinessEntity _grupProf = GetbyNICGrup(txtNIC.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        GetNICGender();
                        SetCustomerAndDeliveryDetailsGroup(_grupProf);
                        _isGroup = true;

                        //DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                        //if (_table != null && _table.Rows.Count > 0)
                        //{
                        //    var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                        //    if (_isAvailable == null || _isAvailable.Count <= 0)
                        //    {
                        //        DisplayMessage("Customer is not allow for enter transaction under selected invoice type");
                        //        ClearCustomer(true);
                        //        txtCustomer.Focus();
                        //        return;
                        //    }
                        //}
                        //else if (cmbInvType.Text != "CS")
                        //{
                        //    DisplayMessage("Selected Customer is not allow for enter transaction under selected invoice type");
                        //    ClearCustomer(true);
                        //    txtCustomer.Focus();
                        //    return;
                        //}
                    }
                }

                txtLoyalty.Text = ReturnLoyaltyNo();
                txtLoyalty_TextChanged(null, null);
                EnableDisableCustomer();
                txtMobile.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void LoadInvItems(Int32 seqno)
        {
            try
            {
                dtiInvtems = CHNLSVC.Sales.GetInvReqItems(seqno.ToString(), Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());



                //  ViewState["ITEMSTABLE"] = null;
                //  this.BindItemsGrid();


                //_invoiceItemList = dtiInvtems as List<InvoiceItem>;

                //this.BindItemsGrid();
                int i = 1;
                foreach (DataRow row in dtiInvtems.Rows)
                {


                    InvoiceItem _list = new InvoiceItem();
                    _list.Sad_itm_line = i;
                    _list.Sad_itm_cd = row[1].ToString();
                    _list.Mi_longdesc = row[3].ToString();
                    _list.Sad_itm_stus = row[2].ToString();
                    _list.Sad_itm_stus_desc = row[4].ToString();
                    _list.Sad_qty = Convert.ToDecimal(row[5].ToString());
                    _list.Sad_unit_rt = Convert.ToDecimal(row[6].ToString());
                    _list.Sad_unit_amt = Convert.ToDecimal(row[7].ToString());
                    _list.Sad_seq = 0;
                    _list.Sad_itm_seq = 0;
                    _invoiceItemList.Add(_list);

                    if (string.IsNullOrEmpty(dtiInvtems.Rows[i - 1]["sad_disc_rt"].ToString())) dtiInvtems.Rows[i - 1]["sad_disc_rt"] = "0";
                    if (string.IsNullOrEmpty(dtiInvtems.Rows[i - 1]["sad_disc_amt"].ToString())) dtiInvtems.Rows[i - 1]["sad_disc_amt"] = "0";
                    if (string.IsNullOrEmpty(dtiInvtems.Rows[i - 1]["sad_itm_tax_amt"].ToString())) dtiInvtems.Rows[i - 1]["sad_itm_tax_amt"] = "0";


                    i++;
                    // where _list.Sad_itm_cd == txtItem.Text.ToUpper() && _list.Sad_itm_stus == cmbStatus.SelectedValue.ToString() && _list.Sad_pbook == cmbBook.Text && _list.Sad_pb_lvl == cmbLevel.Text && _list.Sad_unit_rt == Convert.ToDecimal(txtUnitPrice.Text) && _list.Sad_disc_rt == Convert.ToDecimal(txtDisRate.Text.Trim())
                }
                if (dtiInvtems.Rows.Count > 0)
                {
                    gvInvoiceItem.DataSource = null;
                    gvInvoiceItem.DataBind();


                    gvInvoiceItem.DataSource = dtiInvtems;// setItemDescriptionsDt(dtiInvtems);
                    gvInvoiceItem.DataBind();
                }
                ViewState["ITEMSTABLE"] = dtiInvtems;
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private bool LoadPriceBook(string _invoiceType)
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _books = _PriceDefinitionRef.Where(x => x.Sadd_doc_tp == cmbInvType.Text).Select(x => x.Sadd_pb).Distinct().ToList();
                    _books.Add("");
                    cmbBook.DataTextField = "";
                    cmbBook.DataValueField = "";
                    cmbBook.DataSource = _books;
                    cmbBook.DataBind();
                    cmbBook.SelectedIndex = cmbBook.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultBook))
                    {
                        var result1 = _books.Where(item => item == DefaultBook).ToList();
                        if (result1.Count > 0)
                        {
                            cmbBook.Text = DefaultBook;
                        }
                        else
                        {
                            cmbBook.Text = _books[0].ToString(); ;
                        }
                    }



                }
                else
                    cmbBook.DataSource = null;
            else
                cmbBook.DataSource = null;

            return _isAvailable;
        }

        private void LoadPriceLevelMessage()
        {
            DataTable _msg = CHNLSVC.Sales.GetPriceLevelMessage(Session["UserCompanyCode"].ToString(), cmbBook.Text, cmbLevel.Text);
            if (_msg != null && _msg.Rows.Count > 0)
                lblLvlMsg.Text = _msg.Rows[0].Field<string>("Sapl_spmsg");
            else lblLvlMsg.Text = string.Empty;
        }

        private void LoadPromotor()
        {
            cmbTechnician.DataSource = null;
            txtPromotor.Text = "";
            DataTable _tblPromotor = CHNLSVC.General.GetProfitCenterAllocatedPromotors(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());

            if (_tblPromotor != null)
            {
                //AutoCompleteStringCollection _string0 = new AutoCompleteStringCollection();
                //var _lst0 = _tblPromotor.AsEnumerable().ToList();
                cmbTechnician.DataSource = _tblPromotor;
                cmbTechnician.DataValueField = "mpp_promo_cd";
                cmbTechnician.DataTextField = "mpp_promo_name";
                cmbTechnician.DataBind();

                if (_tblPromotor.Rows.Count > 0)
                {
                    cmbTechnician.DataBind();
                }
                cmbTechnician.Items.Insert(0, new ListItem("Select", "0"));
                //if (_lst0 != null && _lst0.Count > 0) cmbTechnician.DataSource = _lst0.CopyToDataTable();
                //{ Parallel.ForEach(_lst0, x => _string0.Add(x.Field<string>("mpp_promo_name"))); cmbTechnician.AutoCompleteSource = AutoCompleteSource.CustomSource; cmbTechnician.AutoCompleteMode = AutoCompleteMode.SuggestAppend; cmbTechnician.AutoCompleteCustomSource = _string0; }

                //cmbTechnician.ValueMember = "mpp_promo_cd"; cmbTechnician.DisplayMember = "mpp_promo_name";
                //cmbExecutive.DataSource = _tblPromotor; cmbExecutive.DropDownWidth = 200;
            }
        }

        private void InitializeValuesNDefaultValueSet()
        {
            //try
            //{
            //    _serverDt = CHNLSVC.Security.GetServerDateTime().Date;
            //VaribleClear();
            //VariableInitialization();
            LoadInvoiceProfitCenterDetail();
            LoadPriceDefaultValue();
            LoadCancelPermission();
            //SetDecimalTextBoxForZero(true, true, true);
            LoadPayMode();
            //LoadControl();
            //lblBackDateInfor.Text = string.Empty;
            //ResetDeliveryInstructionToOriginalCustomer();
            //chkDeliverLater_CheckedChanged(null, null);
            //CheckPrintStatus();
            BuyBackItemList = null;
            //SetDateTopPayMode();
            //txtQty.Text = FormatToQty("1");
            txtPromotor.Text = "";
            LoadExecutive();
            LoadPromotor();
            //if (string.IsNullOrEmpty(Session["UserDefLoca"].ToString()))
            //{ chkDeliverLater.Checked = true; chkDeliverLater.Enabled = false; }
            //else chkDeliverLater.Enabled = true; LoadInvoiceType();
            //if (cmbInvType.Text.Trim() != "CRED")
            //{
            //    LoadCustomerDetailsByCustomer(null, null);
            //    cmbTitle_SelectedIndexChanged(null, null);
            //}
            //if (_MasterProfitCenter.Mpc_chnl == "AUTO_DEL")
            //{ txtManualRefNo.Enabled = true; }
            //else
            //{
            //    txtManualRefNo.Enabled = false;
            //}

            _isRegistrationMandatory = false;

            //txtPromoVouNo.Clear();
            //lblPromoVouNo.Text = "";
            //lblPromoVouUsedFlag.Text = "";
            //}
            //catch (Exception ex)
            //{
            //    //divalert.Visible = true;
            //    DisplayMessage(ex.Message, 4);
            //}
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());

            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;



        }


        private bool ValidateSOItems()
        {
            if (string.IsNullOrEmpty(txtItem.Text.Trim()))
            {
                DisplayMessage("Please select items by item or by serial");
                lbtncode.Focus();
                return false;
            }

            if (cmbStatus.SelectedIndex == 0)
            {
                DisplayMessage("Please select status");
                cmbStatus.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                DisplayMessage("Please enter quantity");
                txtQty.Focus();
                return false;
            }

            if (!IsNumeric(txtQty.Text.Trim(), NumberStyles.Float))
            {
                DisplayMessage("Please enter valid number for quantity");
                txtQty.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0)
            {
                DisplayMessage("0 quantity is not allowed");
                txtQty.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtQty.Text.Trim()) < 0)
            {
                DisplayMessage("Minus values are not allowed");
                txtQty.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtUnitPrice.Text.Trim()))
            {
                DisplayMessage("Please enter Unit Price");
                txtUnitPrice.Focus();
                return false;
            }

            if (!IsNumeric(txtUnitPrice.Text.Trim(), NumberStyles.Float))
            {
                DisplayMessage("Please enter valid number for Unit Price");
                txtUnitPrice.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtUnitPrice.Text.Trim()) == 0)
            {
                DisplayMessage("0 quantity is not allowed");
                txtUnitPrice.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtUnitPrice.Text.Trim()) < 0)
            {
                DisplayMessage("Minus values are not allowed");
                txtUnitPrice.Focus();
                return false;
            }

            if (!IsNumeric(txtDisRate.Text.Trim(), NumberStyles.Float))
            {
                DisplayMessage("Please enter valid number for Dis.Rate");
                txtDisRate.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtDisRate.Text.Trim()) < 0)
            {
                DisplayMessage("Minus values are not allowed");
                txtDisRate.Focus();
                return false;
            }

            if (!IsNumeric(txtDisAmt.Text.Trim(), NumberStyles.Float))
            {
                DisplayMessage("Please enter valid number for Dis.Rate");
                txtDisAmt.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtDisAmt.Text.Trim()) < 0)
            {
                DisplayMessage("Minus values are not allowed");
                txtDisAmt.Focus();
                return false;
            }

            if (!IsNumeric(txtTaxAmt.Text.Trim(), NumberStyles.Float))
            {
                txtTaxAmt.Focus();
                DisplayMessage("Please enter valid number for Dis.Rate");
                return false;
            }

            if (Convert.ToDecimal(txtTaxAmt.Text.Trim()) < 0)
            {
                DisplayMessage("Minus values are not allowed");
                txtTaxAmt.Focus();
                return false;
            }

            if (!IsNumeric(txtLineTotAmt.Text.Trim(), NumberStyles.Float))
            {
                DisplayMessage("Please enter valid number for Dis.Rate");
                txtLineTotAmt.Focus();
                return false;
            }

            if (Convert.ToDecimal(txtLineTotAmt.Text.Trim()) < 0)
            {
                DisplayMessage("Minus values are not allowed");
                txtLineTotAmt.Focus();
                return false;
            }

            return true;
        }

        public bool CheckServerDateTime()
        {
            if (CHNLSVC.Security.GetServerDateTime().Date != DateTime.Now.Date)
            {
                DisplayMessage("Your machine date conflict with the server date!.please contact system administrator");
                return false;
            }

            System.Globalization.CultureInfo.CurrentCulture.ClearCachedData();
            TimeZone zone = TimeZone.CurrentTimeZone;
            TimeSpan offset = zone.GetUtcOffset(DateTime.Now);

            string _serverUTC = CHNLSVC.Security.GetServerTimeZoneOffset();
            string _localUTC = offset.ToString();

            if (_serverUTC != _localUTC)
            {
                DisplayMessage("Your machine time zone conflict with the server time zone!.please contact system administrator");
                return false;
            }
            return true;
        }

        private bool IsBackDateOk(bool _isDelverNow, bool _isBuyBackItemAvailable)
        {
            bool _isOK = true;
            _isBackDate = true;
            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), string.Empty.ToUpper().ToString(), Session["UserDefProf"].ToString(), this.Page, txtdate.Text, lbtnimgselectdate, lblBackDateInfor, "m_Trans_Sales_SalesInvoice", out  _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(txtdate.Text) != DateTime.Now.Date)
                    {
                        txtdate.Focus();
                        _isOK = false;
                        _isBackDate = false;
                        return _isOK;
                    }
                }
                else
                {
                    DisplayMessage("Back date not allow for selected date for the profit center " + Session["UserDefProf"].ToString() + "!.");
                    txtdate.Focus();
                    _isOK = false;
                    _isBackDate = false;
                    return _isOK;
                }
            }
            return _isOK;
        }

        public bool IsReferancedDocDateAppropriate(List<ReptPickSerials> _reptPrickSerialList, DateTime _processDate)
        {
            bool _appropriate = true;
            if (_reptPrickSerialList != null)
            {
                var _isInAppropriate = _reptPrickSerialList.Where(x => x.Tus_doc_dt.Date > _processDate.Date).ToList();
                if (_isInAppropriate == null || _isInAppropriate.Count <= 0) _appropriate = true;
                else _appropriate = false;
                if (_appropriate == false)
                {
                    StringBuilder _documents = new StringBuilder();
                    foreach (ReptPickSerials _one in _isInAppropriate)
                    {
                        if (string.IsNullOrEmpty(_documents.ToString()))
                            _documents.Append(_one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                        else
                            _documents.Append(" and " + _one.Tus_doc_no + "- dated " + _one.Tus_doc_dt.ToShortDateString() + " where item " + _one.Tus_itm_cd + "/n");
                    }
                    DisplayMessage("The Inward documents " + _documents.ToString() + " equal or grater than to a this Outward document " + _processDate.Date.ToShortDateString() + " date!");
                }
            }
            return _appropriate;
        }

        private decimal GetExchangeRate()
        {
            try
            {
                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                decimal _exchangRate = 0;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(Session["UserCompanyCode"].ToString(), txtcurrency.Text.Trim(), DateTime.Now, _pc.Mpc_def_exrate, string.Empty);
                if (_exc1 != null)
                {
                    _exchangRate = _exc1.Mer_bnkbuy_rt;
                }
                return _exchangRate;
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
                return 0;
            }
        }

        private void SessionClear()
        {
            try
            {
                Session["SOSEQNO"] = null;
                Session["SONO"] = null;
                Session["CUSCOM"] = null;
                Session["CUSLOC"] = null;
                Session["CUSHASCOMPANY"] = null;
                Session["WARRANTY"] = null;

                Session["DCUSCODE"] = null;
                Session["DCUSNAME"] = null;
                Session["DTOWN"] = null;
                Session["DCUSADD1"] = null;
                Session["DCUSADD2"] = null;
                Session["DCUSLOC"] = null;

                Session["SOSEQNO"] = null;
                Session["RSEQNO"] = null;
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void Clear()
        {
            try
            {
                _postBackCount = 0;
                IscheckApproval = false;
                Isrequestbase = false;
                lblAvailableCredit.Text = "0";
                lblAccountBalance.Text = "0";
                Session["_isExtentedPage"] = false;
                Session["_itemSerializedStatus"] = "";
                txttaxPay.Text = "";
                Session["_isOrderbase"] = false;
                btnSave.Enabled = true;
                btnSave.OnClientClick = "ConfirmPlaceOrder();";
                btnSave.CssClass = "buttonUndocolor";

                gvInvoiceItem.Enabled = true;
                gvPopSerial.Enabled = true;

                setGriDel_enables(true);

                _stopit = false;
                Session["oBBNewItems"] = null;
                hdfShowCustomer.Value = null;
                Session["ucc"] = null;
                lblBackDateInfor.Text = string.Empty;
                txtdocrefno.Text = string.Empty;
                txtManualRefNo.Text = string.Empty;
                txtInvoiceNo.Text = string.Empty;
                txtcurrency.Text = string.Empty;
                txtCustomer.Text = string.Empty;
                txtNIC.Text = string.Empty;
                txtMobile.Text = string.Empty;
                txtLoyalty.Text = string.Empty;
                SetLoyalityColor();
                cmbTitle.SelectedIndex = 0;
                txtCusName.Text = string.Empty;
                txtAddress1.Text = string.Empty;
                txtAddress2.Text = string.Empty;
                txtlocation.Text = string.Empty;
                lblAccountBalance.Text = string.Empty;
                // lblAvailableCredit.Text = string.Empty;
                chkTaxPayable.Checked = false;
                lblSVatStatus.Text = string.Empty;
                lblVatExemptStatus.Text = string.Empty;
                txtPoNo.Text = string.Empty;
                txtexcutive.Text = string.Empty;
                chkQuotation.Checked = false;
                txtQuotation.Text = string.Empty;
                txtQuotation.ReadOnly = true;
                txtPromotor.Text = string.Empty;
                txtSerialNo.Text = string.Empty;
                txtItem.Text = string.Empty;
                txtQty.Text = "1";
                txtUnitPrice.Text = "0.00";
                txtUnitAmt.Text = "0.00";
                txtDisRate.Text = "0.00";
                txtDisRate.ReadOnly = false;
                txtDisAmt.Text = "0.00";
                txtTaxAmt.Text = "0.00";
                txtLineTotAmt.Text = "0.00";
                txtresno.Text = string.Empty;
                lblItemDescription.Text = string.Empty;
                lblItemModel.Text = string.Empty;
                lblItemBrand.Text = string.Empty;
                lblItemSerialStatus.Text = string.Empty;

                cmbTitle.SelectedItem.Text = "MR.";
                txtCusName.Text = "CASH";
                txtAddress1.Text = "N/A";

                txtCustomer.Enabled = true;
                txtCustomer.ReadOnly = false;

                chkOpenDelivery.Enabled = true;
                txtdellocation.Enabled = true;
                // btnSearchDelLocation.Enabled = true;

                chkDeliverNow.Enabled = true;
                chkDeliverNow.Checked = false;
                chkOpenDelivery.Checked = false;

                SessionClear();

                ViewState["ITEMSTABLE"] = null;
                DataTable dtitems = new DataTable();
                dtitems.Columns.AddRange(new DataColumn[15] { new DataColumn("soi_itm_line"), new DataColumn("soi_itm_cd"), new DataColumn("description"), new DataColumn("soi_itm_stus"), new DataColumn("soi_qty"), new DataColumn("soi_unit_rt"), new DataColumn("soi_unit_amt"), new DataColumn("soi_disc_rt"), new DataColumn("soi_disc_amt"), new DataColumn("soi_itm_tax_amt"), new DataColumn("soi_tot_amt"), new DataColumn("soi_pbook"), new DataColumn("soi_pb_lvl"), new DataColumn("soi_res_no"), new DataColumn("itri_seq_no") });
                ViewState["ITEMSTABLE"] = dtitems;
                this.BindItemsGrid();

                ViewState["SERIALSTABLE"] = null;
                DataTable dtserials = new DataTable();
                dtserials.Columns.AddRange(new DataColumn[8] { new DataColumn("sose_itm_line"), new DataColumn("sose_itm_cd"), new DataColumn("Model"), new DataColumn("Status"), new DataColumn("Qty"), new DataColumn("sose_ser_1"), new DataColumn("sose_ser_2"), new DataColumn("Warranty") });
                ViewState["SERIALSTABLE"] = dtitems;
                this.BindSerialsGrid();

                //grvwarehouseitms.DataSource = null;
                //grvwarehouseitms.DataBind();

                gvInvoiceItem.DataSource = new int[] { };
                gvInvoiceItem.DataBind();

                gvPopSerial.DataSource = new int[] { };
                gvPopSerial.DataBind();



                gvNormalPrice.DataSource = new int[] { };
                gvNormalPrice.DataBind();

                gvPromotionPrice.DataSource = new int[] { };
                gvPromotionPrice.DataBind();

                gvPromotionItem.DataSource = new int[] { };
                gvPromotionItem.DataBind();

                gvPromotionSerial.DataSource = new int[] { };
                gvPromotionSerial.DataBind();

                txtresno.Text = "N/A";
                txtresno.Text = "";

                DateTime orddate = DateTime.Now;
                txtdate.Text = orddate.ToString("dd/MMM/yyyy");
                txtTDate.Text = orddate.ToString("dd/MMM/yyyy");
                txtFDate.Text = orddate.ToString("dd/MMM/yyyy");//orddate.AddDays(-5).ToString("dd/MMM/yyyy");

                if (cmbInvType.Text.Trim() == "CS")
                    txtCustomer.Text = "CASH";

                lblGrndSubTotal.Text = "0";
                lblGrndDiscount.Text = "0";
                lblGrndAfterDiscount.Text = "0";
                lblGrndTax.Text = "0";
                lblGrndTotalAmount.Text = "0";

                VariableInitialize();


                chkDeliverNow.Checked = false;


                _serverDt = CHNLSVC.Security.GetServerDateTime().Date;

                hdfConf.Value = null;
                hdfConfItem.Value = null;
                hdfConfStatus.Value = null;
                hdfcontinueWithNormalSerializedPrice.Value = null;
                hdfcontinueWithTheAvailablePromotions.Value = null;
                hdfnormalSerialized.Value = null;
                hdfselectPromotionalSerializedPrice.Value = null;
                hdfSavePO.Value = null;


                lblPromoVouUsedFlag.Text = "";

                LookingForBuyBack();
                LoadPriceDefaultValue();
                lblcurrency.Text = "";

                txtBBItem.Text = "";
                txtBBQty.Text = "";
                txtBBSerial1.Text = "";
                txtBBSerial2.Text = "";
                txtBBWarranty.Text = "";
                lblBBDescription.Text = "";
                lblBBModel.Text = "";
                lblBBBrand.Text = "";

                gvAddBuyBack.DataSource = new int[] { };
                gvAddBuyBack.DataBind();

                gvInvoiceItem.Columns[13].Visible = false;

                gvPopSerial.Enabled = true;

                dgvReceiptItems.DataSource = new int[] { };
                dgvReceiptItems.DataBind();

                _invoiceItemList = new List<InvoiceItem>();
                InvoiceSerialList = new List<InvoiceSerial>();


                _selectedItemList = new List<ReptPickSerials>();

                cmbExecutive.SelectedIndex = 0;
                txtexcutive.Text = string.Empty;
                txtPerTown.Text = "";

                grvwarehouseitms.DataSource = new int[] { };
                grvwarehouseitms.DataBind();

                loadItemStatus();



                pnlDeliverBOdy.Enabled = true;

                txtPromotor.Text = "";
                cmbTechnician.SelectedIndex = 0;

                mpSavePO.Hide();
                txtSalesOrderSearch.Text = "";


                btnSave.Enabled = true;
                txtItem.Enabled = true;
                txtSerialNo.Enabled = true;
                lbtnadditems.Enabled = true;

                btnSave.Enabled = true;
                btnSave.OnClientClick = "ConfirmPlaceOrder();";
                btnSave.CssClass = "buttonUndocolor";

                gvInvoiceItem.Enabled = true;
                gvPopSerial.Enabled = true;

                setGriDel_enables(true);

                pnlDeliverBOdy.Enabled = true;

                txtCustomer.Enabled = true;
                txtCusName.Enabled = true;
                txtAddress1.Enabled = true;
                txtAddress2.Enabled = true;
                txtMobile.Enabled = true;
                txtNIC.Enabled = true;

                btnSearch_NIC.Enabled = true;
                lbtncode.Enabled = true;
                btnSearch_Mobile.Enabled = true;

                txtexcutive.Text = string.Empty;
                lblSalesEx.Text = string.Empty;

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
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
            }
            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        protected void InsertToItemsGrid()
        {
            try
            {
                Int32 gridline = gvInvoiceItem.Rows.Count;

                //foreach (GridViewRow row in gvInvoiceItem.Rows)
                //{
                //    if (row.RowIndex == gvInvoiceItem.SelectedIndex)
                //    {
                //        if (row.BackColor.Name != "0")
                //        {
                //            DataTable dtdup = ViewState["ITEMSTABLE"] as DataTable;
                //            dtdup.Rows[row.RowIndex].Delete();
                //            dtdup.AcceptChanges();
                //            ViewState["ITEMSTABLE"] = dtdup;
                //            BindItemsGrid();
                //        }
                //    }
                //}

                string _statustxt = string.Empty;

                DataTable dtstatustx = CHNLSVC.Sales.GetItemStatusTxt(cmbStatus.SelectedValue);

                if (dtstatustx.Rows.Count > 0)
                {
                    foreach (DataRow ddr2 in dtstatustx.Rows)
                    {
                        _statustxt = ddr2[0].ToString();
                    }
                }

                string _seqnoforrequest = (string)Session["RSEQNO"];

                DataTable dt = (DataTable)ViewState["ITEMSTABLE"];
                dt.Rows.Add(gridline + 1, txtItem.Text.Trim(), lblItemDescription.Text.Trim(), _statustxt, txtQty.Text.Trim(), txtUnitPrice.Text.Trim(), txtUnitAmt.Text.Trim(), txtDisRate.Text.Trim(), txtDisAmt.Text.Trim(), txtTaxAmt.Text.Trim(), txtLineTotAmt.Text.Trim(), cmbBook.SelectedValue, cmbLevel.SelectedValue, txtresno.Text.Trim(), _seqnoforrequest);
                ViewState["ITEMSTABLE"] = dt;

                //uniqueitems = RemoveDuplicateRows(dt, "soi_itm_cd");

                gvInvoiceItem.DataSource = setItemDescriptionsDt(dt);
                gvInvoiceItem.DataBind();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void InsertToSerialsGrid()
        {
            try
            {
                string warranty = (string)Session["WARRANTY"];
                DataTable dt1 = (DataTable)ViewState["SERIALSTABLE"];
                dt1.Rows.Add(gvPopSerial.Rows.Count + 1, txtItem.Text.Trim(), lblItemModel.Text.Trim(), cmbStatus.SelectedValue, txtQty.Text.Trim(), txtSerialNo.Text.Trim(), string.Empty, warranty);
                ViewState["SERIALSTABLE"] = dt1;

                uniqueitems = RemoveDuplicateRows(dt1, "sose_itm_cd");

                gvPopSerial.DataSource = setSerialStatusDescriptionsDt(uniqueitems);
                gvPopSerial.DataBind();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void AddItemDisableCustomer(bool _disable)
        {
            if (_disable == false)
            {
                txtCustomer.Enabled = true;
                btnSearch_NIC.Enabled = true;
                lbtncode.Enabled = true;
                btnSearch_Mobile.Enabled = true;
                txtdate.Enabled = true;
            }
            else
            {
                if (txtCustomer.Text.ToString() != "CASH")
                {
                    txtCustomer.Enabled = false;
                    btnSearch_NIC.Enabled = false;
                    lbtncode.Enabled = false;
                    btnSearch_Mobile.Enabled = false;
                }
                txtdate.Enabled = false;
            }
        }

        protected void LoadCustomerDetailsByMobile()
        {
            if (string.IsNullOrEmpty(txtMobile.Text)) return;
            _masterBusinessCompany = new MasterBusinessEntity();
            try
            {
                if (!string.IsNullOrEmpty(txtMobile.Text))
                {
                    if (!IsValidMobileOrLandNo(txtMobile.Text))
                    {
                        DisplayMessage("Please select the valid mobile");
                    }
                    List<MasterBusinessEntity> _custList = CHNLSVC.Sales.GetActiveCustomerDetailList(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMobile.Text, "C");

                    if (!string.IsNullOrEmpty(txtCustomer.Text) && txtCustomer.Text.Trim() != "CASH")
                        _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim(), string.Empty, txtMobile.Text, "C");
                    else
                        _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), string.Empty, string.Empty, txtMobile.Text, "C");
                    // ucPayModes1.Mobile = txtMobile.Text.Trim();
                }
                if (!string.IsNullOrEmpty(_masterBusinessCompany.Mbe_cd))
                {
                    if (_masterBusinessCompany.Mbe_act == true)
                    {
                        LoadTaxDetail(_masterBusinessCompany);
                        SetCustomerAndDeliveryDetails(false, null);
                        ViewCustomerAccountDetail(txtCustomer.Text);
                    }
                    else
                    {
                        ClearCustomer(true);
                        DisplayMessage("This customer already inactive. Please contact accounts dept");
                        txtCustomer.Focus();
                        return;
                    }
                }
                else
                {
                    GroupBussinessEntity _grupProf = GetbyMobGrup(txtMobile.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        SetCustomerAndDeliveryDetailsGroup(_grupProf);
                        _isGroup = true;
                        //DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                        //if (_table != null && _table.Rows.Count > 0)
                        //{
                        //    var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                        //    if (_isAvailable == null || _isAvailable.Count <= 0)
                        //    {
                        //        DisplayMessage("Customer is not allow for enter transaction under selected order type");
                        //        ClearCustomer(true);
                        //        txtCustomer.Focus();
                        //        return;
                        //    }
                        //}
                        //else if (cmbInvType.Text != "CS")
                        //{
                        //    ClearCustomer(true);
                        //    DisplayMessage("Selected Customer is not allow for enter transaction under selected order type");
                        //    txtCustomer.Focus();
                        //    return;
                        //}
                    }
                    else
                    {
                        _isGroup = false;
                    }
                }

                txtLoyalty.Text = ReturnLoyaltyNo();
                txtLoyalty_TextChanged(null, null);
                EnableDisableCustomer();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        public GroupBussinessEntity GetbyMobGrup(string mobNo)
        {
            return CHNLSVC.Sales.GetCustomerProfileByGrup(null, null, null, null, null, mobNo);
        }

        private void CalculateGrandTotal(decimal _qty, decimal _uprice, decimal _discount, decimal _tax, bool _isAddition)
        {
            if (_isAddition)//++
            {
                GrndSubTotal = GrndSubTotal + Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount + Convert.ToDecimal(_discount);
                GrndTax = GrndTax + Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = GrndSubTotal.ToString("N2");
                lblGrndDiscount.Text = GrndDiscount.ToString("N2");
                lblGrndTax.Text = GrndTax.ToString("N2");
            }
            else//--
            {
                GrndSubTotal = GrndSubTotal - Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount - Convert.ToDecimal(_discount);
                GrndTax = GrndTax - Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = GrndSubTotal.ToString("N2");
                lblGrndDiscount.Text = GrndDiscount.ToString("N2");
                lblGrndTax.Text = GrndTax.ToString("N2");
            }

            lblGrndAfterDiscount.Text = (GrndSubTotal - GrndDiscount).ToString("N2");

            if (_invoiceItemList != null || _invoiceItemList.Count > 0)
                lblGrndTotalAmount.Text = _invoiceItemList.Sum(x => x.Sad_tot_amt).ToString("N2");
            else
                lblGrndTotalAmount.Text = (Convert.ToString("0.00"));
        }

        private void SetColumnForPriceDetailNPromotion(bool _isSerializedPriceLevel)
        {
            if (_isSerializedPriceLevel)
            {
                //NorPrice_Select.Visible = true;

                //NorPrice_Serial.DataPropertyName = "sars_ser_no";
                //NorPrice_Serial.Visible = true;
                //NorPrice_Item.DataPropertyName = "Sars_itm_cd";
                //NorPrice_Item.Visible = true;
                //NorPrice_UnitPrice.DataPropertyName = "sars_itm_price";
                //NorPrice_UnitPrice.Visible = true;
                //NorPrice_Circuler.DataPropertyName = "sars_circular_no";
                //NorPrice_PriceType.DataPropertyName = "sars_price_type";
                //NorPrice_PriceTypeDescription.DataPropertyName = "sars_price_type_desc";
                //NorPrice_ValidTill.DataPropertyName = "sars_val_to";
                //NorPrice_ValidTill.Visible = true;
                //NorPrice_Pb_Seq.DataPropertyName = "sars_pb_seq";
                //NorPrice_PbLineSeq.DataPropertyName = "1";
                //NorPrice_PromotionCD.DataPropertyName = "sars_promo_cd";
                //NorPrice_IsFixQty.DataPropertyName = "sars_is_fix_qty";
                //NorPrice_BkpUPrice.DataPropertyName = "sars_cre_by";
                //NorPrice_WarrantyRmk.DataPropertyName = "sars_warr_remarks";
                //NorPrice_Book.DataPropertyName = "sars_pbook";
                //NorPrice_Level.DataPropertyName = "sars_price_lvl";

                //    PromPrice_Select.Visible = true;

                //    PromPrice_Serial.DataPropertyName = "sars_ser_no";
                //    PromPrice_Serial.Visible = true;
                //    PromPrice_Item.DataPropertyName = "Sars_itm_cd";
                //    PromPrice_Item.Visible = true;
                //    PromPrice_UnitPrice.DataPropertyName = "sars_itm_price";
                //    PromPrice_UnitPrice.Visible = true;
                //    PromPrice_Circuler.DataPropertyName = "sars_circular_no";
                //    PromPrice_PriceType.DataPropertyName = "sars_price_type";
                //    PromPrice_PriceTypeDescription.DataPropertyName = "sars_price_type_desc";
                //    PromPrice_ValidTill.DataPropertyName = "sars_val_to";
                //    PromPrice_ValidTill.Visible = true;
                //    PromPrice_Pb_Seq.DataPropertyName = "sars_pb_seq";
                //    //PromPrice_PbLineSeq.DataPropertyName = "1";
                //    PromPrice_PromotionCD.DataPropertyName = "sars_promo_cd";
                //    PromPrice_IsFixQty.DataPropertyName = "sars_is_fix_qty";
                //    PromPrice_BkpUPrice.DataPropertyName = "sars_cre_by";
                //    PromPrice_WarrantyRmk.DataPropertyName = "sars_warr_remarks";
                //    PromPrice_Book.DataPropertyName = "sars_pbook";
                //    PromPrice_Level.DataPropertyName = "sars_price_lvl";
                //}
                //else
                //{
                //    NorPrice_Select.Visible = false;

                //    NorPrice_Serial.Visible = false;
                //    NorPrice_Item.DataPropertyName = "sapd_itm_cd";
                //    NorPrice_Item.Visible = true;
                //    NorPrice_UnitPrice.DataPropertyName = "Sapd_itm_price";
                //    NorPrice_UnitPrice.Visible = true;
                //    NorPrice_Circuler.DataPropertyName = "Sapd_circular_no";
                //    NorPrice_Circuler.Visible = true;
                //    NorPrice_PriceType.DataPropertyName = "Sarpt_cd";
                //    NorPrice_PriceTypeDescription.DataPropertyName = "SARPT_CD";
                //    NorPrice_ValidTill.DataPropertyName = "Sapd_to_date";
                //    NorPrice_ValidTill.Visible = true;
                //    NorPrice_Pb_Seq.DataPropertyName = "sapd_pb_seq";
                //    NorPrice_PbLineSeq.DataPropertyName = "sapd_seq_no";
                //    NorPrice_PromotionCD.DataPropertyName = "sapd_promo_cd";
                //    NorPrice_IsFixQty.DataPropertyName = "sapd_is_fix_qty";
                //    NorPrice_BkpUPrice.DataPropertyName = "sapd_cre_by";
                //    NorPrice_WarrantyRmk.DataPropertyName = "sapd_warr_remarks";
                //    NorPrice_Book.DataPropertyName = "sapd_pb_tp_cd";
                //    NorPrice_Level.DataPropertyName = "sapd_pbk_lvl_cd";

                //    PromPrice_Select.Visible = true;

                //    PromPrice_Serial.Visible = false;
                //    PromPrice_Item.DataPropertyName = "sapd_itm_cd";
                //    PromPrice_Item.Visible = true;
                //    PromPrice_UnitPrice.DataPropertyName = "Sapd_itm_price";
                //    PromPrice_UnitPrice.Visible = true;
                //    PromPrice_Circuler.DataPropertyName = "Sapd_circular_no";
                //    PromPrice_Circuler.Visible = true;
                //    PromPrice_PriceType.DataPropertyName = "sapd_price_type"; //"Sarpt_cd";
                //    PromPrice_PriceTypeDescription.DataPropertyName = "Sarpt_cd";
                //    PromPrice_ValidTill.DataPropertyName = "Sapd_to_date";
                //    PromPrice_ValidTill.Visible = true;
                //    PromPrice_Pb_Seq.DataPropertyName = "sapd_pb_seq";
                //    PromPrice_PbLineSeq.DataPropertyName = "sapd_seq_no";
                //    PromPrice_PromotionCD.DataPropertyName = "sapd_promo_cd";
                //    PromPrice_IsFixQty.DataPropertyName = "sapd_is_fix_qty";
                //    PromPrice_BkpUPrice.DataPropertyName = "sapd_cre_by";
                //    PromPrice_WarrantyRmk.DataPropertyName = "sapd_warr_remarks";
                //    PromPrice_Book.DataPropertyName = "sapd_pb_tp_cd";
                //    PromPrice_Level.DataPropertyName = "sapd_pbk_lvl_cd";
            }
        }

        protected void CheckUnitPrice()
        {
            if (txtUnitPrice.ReadOnly) return;


            if (_IsVirtualItem) { CalculateItem(); return; }
            try
            {
                if (_isCompleteCode && GeneralDiscount_new.SADD_IS_EDIT == 1 && Convert.ToDecimal(txtUnitPrice.Text.Trim()) > 0)
                {
                    DisplayMessage("Not allow price edit for com codes");
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text)) return;
                if (!IsNumeric(txtQty.Text.Trim(), NumberStyles.Float))
                {
                    DisplayMessage("Please select valid quantity");
                    return;
                }
                if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;

                if ((_MasterProfitCenter != null) && (_priceBookLevelRef != null))
                {
                    if (_MasterProfitCenter.Mpc_without_price && _priceBookLevelRef.Sapl_is_without_p)
                    {
                        if (string.IsNullOrEmpty(txtUnitPrice.Text)) txtUnitPrice.Text = ("0");
                        decimal vals = Convert.ToDecimal(txtUnitPrice.Text);
                        txtUnitPrice.Text = (Convert.ToString(vals));
                        txtUnitPrice.Text = vals.ToString("N2");
                        CalculateItem();
                        return;
                    }
                }
                if (!_isCompleteCode)
                {
                    //check minus unit price validation
                    decimal _unitAmt = 0;
                    bool _isUnitAmt = Decimal.TryParse(txtUnitPrice.Text, out _unitAmt);
                    if (!_isUnitAmt)
                    {
                        DisplayMessage("Unit Price has to be number");
                        return;
                    }
                    if (_unitAmt <= 0)
                    {
                        DisplayMessage("Unit Price has to be greater than 0");
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtUnitPrice.Text) && _isEditDiscount == false)
                    {
                        decimal _pb_price;
                        if (SSPriceBookPrice == 0)
                        {
                            DisplayMessage("Price not define. Please check the system updated price");
                            return;
                        }
                        _pb_price = SSPriceBookPrice;
                        decimal _txtUprice = Convert.ToDecimal(txtUnitPrice.Text);
                        GeneralDiscount_new = CHNLSVC.Sales.GetDocPriceDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbBook.Text.Trim(), cmbLevel.Text.Trim(), cmbInvType.Text);

                        if (GeneralDiscount_new.SADD_IS_EDIT == 1) //(_MasterProfitCenter.SADD_IS_EDIT==1)
                        {
                            if (_pb_price > _txtUprice)
                            {
                                decimal _diffPecentage = ((_pb_price - _txtUprice) / _pb_price) * 100;
                                if (_diffPecentage > GeneralDiscount_new.SADD_EDIT_RT)
                                {
                                    DisplayMessage("You cannot deduct price more than " + GeneralDiscount_new.SADD_EDIT_RT + "% from the price book price.");
                                    txtUnitPrice.Text = (Convert.ToString(_pb_price));
                                    txtUnitPrice.Text = _pb_price.ToString("N2");
                                    _isEditPrice = false;
                                    return;
                                }
                                else
                                {
                                    _isEditPrice = true;
                                }
                            }
                        }
                        else
                        {
                            txtUnitPrice.Text = (Convert.ToString(_pb_price));
                            txtUnitPrice.Text = _pb_price.ToString("N2");
                            _isEditPrice = false;
                        }
                    }
                }
                if (string.IsNullOrEmpty(txtUnitPrice.Text)) txtUnitPrice.Text = ("0");
                decimal val = Convert.ToDecimal(txtUnitPrice.Text);
                txtUnitPrice.Text = (Convert.ToString(val));
                txtUnitPrice.Text = val.ToString("N2");
                CalculateItem();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void CalCulateVal()
        {
            try
            {
                Decimal totval = Convert.ToDecimal(txtQty.Text.Trim()) * Convert.ToDecimal(txtUnitPrice.Text.Trim());
                txtUnitAmt.Text = SetDecimalPoint(totval.ToString());
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void BindItemsGrid()
        {
            try
            {
                gvInvoiceItem.DataSource = setItemDescriptionsDt((DataTable)ViewState["ITEMSTABLE"]);
                gvInvoiceItem.DataBind();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void BindSerialsGrid()
        {
            try
            {
                gvPopSerial.DataSource = setSerialStatusDescriptionsDt((DataTable)ViewState["SERIALSTABLE"]);
                gvPopSerial.DataBind();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void clearMessages()
        {
            //divok.Visible = false;
            //divalert.Visible = false;
            //Divinfo.Visible = false;
        }

        private void MpDeliveryShow()
        {
            try
            {
                txtdellocation.Text = Session["UserDefProf"].ToString();
                txtdelcuscode.Text = txtCustomer.Text.ToUpper().Trim();
                txtdelname.Text = txtCusName.Text.ToUpper().Trim();
                txtdelad1.Text = txtAddress1.Text.ToUpper().Trim();
                txtdelad2.Text = txtAddress2.Text.ToUpper().Trim();

                if (!IsPostBack)
                {
                }

                MpDelivery.Show();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void DisplayMessage(String Msg, Int32 option)
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
                //WriteErrLog(Msg);
            }
            //else if (option == 5)
            //{
            //    WriteErrLog(Msg);
            //}
        }

        private void DisplayMessage(String Msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + Msg + "');", true);
        }

        private void DisplayMessageJS(String Msg)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + Msg + "');", true);
        }

        private bool CheckBlockItem(string _item, int _pricetype, bool _isCombineItemAddingNow)
        {
            if (_isCombineItemAddingNow) return false;
            _isBlocked = false;
            if (_priceBookLevelRef.Sapl_is_serialized == false)
            {
                MasterItemBlock _block = CHNLSVC.Inventory.GetBlockedItemByPriceType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _item, _pricetype);
                if (_block != null && !string.IsNullOrEmpty(_block.Mib_itm))
                {
                    DisplayMessageJS(_item + " item already blocked by the Costing Dept.");
                    _isBlocked = true;
                }
            }
            return _isBlocked;
        }

        private void ClearPriceTextBox()
        {
            txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
        }

        private void AddItem(bool _isPromotion, string _originalItem)
        {
            try
            {


                ReptPickSerials _serLst = null;
                List<ReptPickSerials> _nonserLst = null;
                MasterItem _itm = null;
                _invoiceItemList = Session["_invoiceItemListSo"] as List<InvoiceItem>;

                #region Check for Payment

                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
                    {
                        DisplayMessage("You have already added the payment details to the grid", 1);
                        return;
                    }

                #endregion Check for Payment

                #region Priority Base Validation

                if (_masterBusinessCompany == null)
                {
                    DisplayMessage("Please select the customer code", 1);
                    return;
                }


                if (string.IsNullOrEmpty(cmbBook.Text))
                {
                    DisplayMessage("Please select the price book", 1);
                    return;
                }

                if (string.IsNullOrEmpty(cmbLevel.Text))
                {
                    DisplayMessage("Please select the price level", 1);
                    return;
                }

                if (string.IsNullOrEmpty(cmbStatus.Text) || cmbStatus.SelectedIndex == 0)
                {
                    DisplayMessage("Please select the item status", 1);
                    return;
                }

                if (string.IsNullOrEmpty(cmbInvType.Text))
                {
                    DisplayMessage("Please select the invoice type", 1);
                    return;
                }

                if (string.IsNullOrEmpty(txtCustomer.Text))
                {
                    DisplayMessage("Please select the customer", 1);
                    return;
                }

                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                    {
                        if (string.IsNullOrEmpty(txtSerialNo.Text))
                        {
                            DisplayMessage("Please select the serial", 1);
                            return;
                        }
                        else
                        {
                            DisplayMessage("Please select the item", 1);
                            txtItem.Focus();
                            return;
                        }
                    }
                    else
                    {
                        DisplayMessage("Please select the item", 1);
                        txtItem.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    DisplayMessage("Please select the quantity", 1);
                    return;
                }
                else if (IsNumeric(txtQty.Text, NumberStyles.Any) == false)
                {
                    DisplayMessage("Please select valid quantity", 1);
                    return;
                }
                else if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0)
                {
                    DisplayMessage("Please select the valid quantity amount", 1);
                    return;
                }
                if (string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    DisplayMessage("Please select the unit price", 1);
                    return;
                }
                if (string.IsNullOrEmpty(txtDisRate.Text))
                {
                    DisplayMessage("Please select the discount %", 1);
                    return;
                }
                if (string.IsNullOrEmpty(txtDisAmt.Text))
                {
                    DisplayMessage("Please select the discount amount", 1);
                    return;
                }
                if (string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    DisplayMessage("Please select the VAT amount", 1);
                    return;
                }
                if (string.IsNullOrEmpty(txtPerTown.Text))
                {
                    DisplayMessage("Please select dispatch location", 1);
                    return;
                }
                string dispatch = txtPerTown.Text.Trim();
                //Update SO Item
                if (btnUpdate.Visible == true)
                {
                    if (Isrequestbase == false)
                    {
                        _lineNo = _invoiceItemList.Count;
                    }

                }

                #endregion Priority Base Validation

                #region Virtual Item

                if (_IsVirtualItem && _isCompleteCode == false)
                {
                    bool _isDuplicateItem0 = false;
                    Int32 _duplicateComLine0 = 0;
                    Int32 _duplicateItmLine0 = 0;
                    WarrantyPeriod = 0;
                    CalculateItem();

                    #region Adding Invoice Item

                    //Adding Items to grid goes here ----------------------------------------------------------------------
                    if (_invoiceItemList == null || _invoiceItemList.Count <= 0)
                    //No Records
                    {
                        _isDuplicateItem0 = false;
                        _lineNo += 1;
                        if (!_isCombineAdding) SSCombineLine += 1;//_lineNo;
                        _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itemdetail, _originalItem));
                    }
                    else
                    //Having some records
                    {
                        var _duplicateItem = from _list in _invoiceItemList
                                             where _list.Sad_itm_cd == txtItem.Text.ToUpper() && _list.Sad_itm_stus == cmbStatus.SelectedValue.ToString() && _list.Sad_pbook == cmbBook.Text && _list.Sad_pb_lvl == cmbLevel.Text && _list.Sad_unit_rt == Convert.ToDecimal(txtUnitPrice.Text) && _list.Sad_disc_rt == Convert.ToDecimal(txtDisRate.Text.Trim())
                                             select _list;

                        if (_duplicateItem.Count() > 0)
                        //Similar item available
                        {
                            _isDuplicateItem0 = true;
                            foreach (var _similerList in _duplicateItem)
                            {
                                _duplicateComLine0 = _similerList.Sad_job_line;
                                _duplicateItmLine0 = _similerList.Sad_itm_line;
                                _similerList.Sad_disc_amt = Convert.ToDecimal(_similerList.Sad_disc_amt) + Convert.ToDecimal(txtDisAmt.Text);
                                _similerList.Sad_itm_tax_amt = Convert.ToDecimal(_similerList.Sad_itm_tax_amt) + Convert.ToDecimal(txtTaxAmt.Text);
                                _similerList.Sad_qty = Convert.ToDecimal(_similerList.Sad_qty) + Convert.ToDecimal(txtQty.Text);
                                _similerList.Sad_tot_amt = Convert.ToDecimal(_similerList.Sad_tot_amt) + Convert.ToDecimal(txtLineTotAmt.Text);
                            }
                        }
                        else
                        //No similar item found
                        {
                            _isDuplicateItem0 = false;
                            _lineNo += 1;
                            if (!_isCombineAdding) SSCombineLine += 1;//_lineNo;
                            _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itemdetail, _originalItem));
                        }
                    }
                    //Adding Items to grid end here ----------------------------------------------------------------------

                    #endregion Adding Invoice Item

                    CalculateGrandTotal(Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text), Convert.ToDecimal(txtTaxAmt.Text), true);
                    _itemdetail = new MasterItem();
                    txtSerialNo.Text = "";
                    ClearAfterAddItem();
                    SSPriceBookSequance = "0";
                    SSPriceBookItemSequance = "0";
                    SSPriceBookPrice = 0;
                    if (_isCombineAdding == false) SSPromotionCode = string.Empty;
                    SSPRomotionType = 0;
                    //_//txtItem.Focus();
                    BindAddItem();
                    SetDecimalTextBoxForZero(true);
                    decimal _tobepays0 = 0;
                    if (lblSVatStatus.Text == "Available")
                        _tobepays0 = FigureRoundUp(Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim()), true);
                    else
                        _tobepays0 = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());


                    // LookingForBuyBack();
                    if (_isCombineAdding == false)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "confimationAddAnother()", true);
                        //DisplayMessage("", 1);
                        //if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                        //    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                        //    {
                        //        txtSerialNo.Focus();
                        //    }
                        //    else
                        //    {
                        //        txtItem.Focus();
                        //    }
                        //}
                        //else
                        //{
                        //    ucPayModes1.button1.Focus();
                        //}
                    }
                    return;
                }

                _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());

                //if (!chkDeliverLater.Checked && !chkDeliverNow.Checked)
                //{
                //    List<ReptPickSerials> _temp = CHNLSVC.Inventory.Search_by_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Mi_cd, string.Empty, txtSerialNo.Text.Trim(), string.Empty);
                //    if (!string.IsNullOrEmpty(Convert.ToString(cmbLevel.Text)) && !string.IsNullOrEmpty(Convert.ToString(cmbBook.Text)))
                //    {
                //        bool _isAgeLevel = false;
                //        int _noofday = 0;
                //        CheckNValidateAgeItem(_itm.Mi_cd, string.Empty, cmbBook.Text, cmbLevel.Text, cmbStatus.Text, out _isAgeLevel, out _noofday);
                //        if (_isAgeLevel)
                //            _temp = GetAgeItemList(Convert.ToDateTime(txtDate.Value.Date).Date, _isAgeLevel, _noofday, _temp);
                //        if (_temp == null || _temp.Count <= 0)
                //        {
                //            this.Cursor = Cursors.Default;
                //            using (new CenterWinDialog(this)) { MessageBox.Show("This serial can't select under ageing price level. Please check the ageing status with IT dept.", "Age Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                //            txtSerialNo.Clear();
                //            txtItem.Clear();
                //            txtSerialNo.Focus();
                //            return;
                //        }
                //    }
                //}
                // CheckSerialAvailability(null, null);
                if (!string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    if (!string.IsNullOrEmpty(txtItem.Text.Trim()))
                    {
                        // Edt0001
                        if (_itm.Mi_is_ser1 == 1 && (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false && _priceBookLevelRef.Sapl_is_serialized))
                        {
                            DisplayMessage("Please select the serial number", 1);
                            txtSerialNo.Focus();
                            return;
                        }
                    }
                }

                #region sachith check item balance

                if (chkDeliverNow.Checked && _itm.Mi_itm_tp == "M")
                {
                    List<ReptPickSerials> serial_list = new List<ReptPickSerials>();
                    if (_itm.Mi_is_ser1 == 0)
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), /*Session["UserDefLoca"].ToString()*/dispatch, _itm.Mi_cd, string.Empty, string.Empty).ToList();//.Where(x => x.Tus_itm_stus == cmbStatus.SelectedValue.ToString()).ToList();
                    else if (_itm.Mi_is_ser1 == 1) //serial
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), /*Session["UserDefLoca"].ToString()*/dispatch, _itm.Mi_cd, string.Empty, string.Empty).ToList();//.Where(x => x.Tus_itm_stus == cmbStatus.SelectedValue.ToString()).ToList();
                    else if (_itm.Mi_is_ser1 == -1)
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), /*Session["UserDefLoca"].ToString()*/dispatch, _itm.Mi_cd, string.Empty, string.Empty).ToList();//.Where(x => x.Tus_itm_stus == cmbStatus.SelectedValue.ToString()).ToList();

                    if (IsPriceLevelAllowDoAnyStatus)
                    {
                        serial_list = serial_list.Where(x => x.Tus_itm_stus == cmbStatus.SelectedValue.ToString()).ToList();
                    }

                    if (Convert.ToDecimal(txtQty.Text) > serial_list.Count)
                    {
                        DisplayMessage("-------- Develop ----------6087", 1);
                        //if (MessageBox.Show("Inventory has only " + serial_list.Count + " items Do you want to proceed?", "Serial Qty", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //    return;
                        //else
                        //{
                        //}
                    }
                }

                #endregion sachith check item balance

                #endregion Virtual Item

                #region Price Combine Checking Process - Costing Dept.

                if (_isCheckedPriceCombine == false)
                    if (_MainPriceCombinItem != null)
                        if (_MainPriceCombinItem.Count > 0)
                        {
                            string _serialiNotpick = string.Empty;
                            string _serialDuplicate = string.Empty;
                            string _taxNotdefine = string.Empty;
                            string _noInventoryBalance = string.Empty;
                            string _noWarrantySetup = string.Empty;
                            string _mItem = txtItem.Text.ToUpper().Trim();
                            if (CheckBlockItem(_mItem, SSPRomotionType, _isCombineAdding))
                            {
                                _isCheckedPriceCombine = false;
                                return;
                            }
                            var _dupsMain = ScanSerialList.Where(x => x.Tus_itm_cd == _mItem && x.Tus_ser_1 == ScanSerialNo);
                            if (_dupsMain != null)
                                if (_dupsMain.Count() > 0)
                                {
                                    _isCheckedPriceCombine = false;
                                    DisplayMessage(_mItem + " serial " + ScanSerialNo + " is already picked!", 1);
                                    return;
                                }
                            foreach (PriceCombinedItemRef _ref in _MainPriceCombinItem)
                            {
                                string _item = _ref.Sapc_itm_cd;
                                string _originalItm = _ref.Sapc_itm_cd;
                                decimal _qty = _ref.Sapc_qty;

                                string _status = _ref.Status;
                                string _similerItem = Convert.ToString(_ref.Similer_item);
                                if (!string.IsNullOrEmpty(_similerItem))
                                    _item = _similerItem;
                                if (CheckBlockItem(_item, SSPRomotionType, _isCombineAdding))
                                {
                                    _isCheckedPriceCombine = false; break;
                                }
                                List<MasterItemTax> _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, string.Empty);
                                if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                                {
                                    if (string.IsNullOrEmpty(_taxNotdefine)) _taxNotdefine = _item;
                                    else
                                        _taxNotdefine += "," + _item;
                                }
                                if (CheckItemWarranty(_item, _status))
                                {
                                    if (string.IsNullOrEmpty(_noWarrantySetup))
                                        _noWarrantySetup = _item;
                                    else _noWarrantySetup += "," + _item;
                                }
                                MasterItem _itmS = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

                                if ((chkDeliverLater.Checked == false && chkDeliverNow.Checked == false && _isCheckedPriceCombine == false) || IsGiftVoucher(_itmS.Mi_itm_tp))
                                {
                                    _isCheckedPriceCombine = true;
                                    if (_itmS.Mi_is_ser1 == 1)
                                    {
                                        var _exist = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item);
                                        if (_qty > _exist.Count())
                                        { if (string.IsNullOrEmpty(_serialiNotpick)) _serialiNotpick = _item; else _serialiNotpick += "," + _item; }
                                        foreach (ReptPickSerials _p in _exist)
                                        {
                                            string _serial = _p.Tus_ser_1;
                                            var _dup = ScanSerialList.Where(x => x.Tus_ser_1 == _serial);
                                            if (_dup != null)
                                                if (_dup.Count() > 0)
                                                {
                                                    if (string.IsNullOrEmpty(_serialDuplicate))
                                                        _serialDuplicate = _item + "/" + _serial;
                                                    else _serialDuplicate = "," + _item + "/" + _serial;
                                                }
                                        }
                                    }
                                    if (!IsGiftVoucher(_itmS.Mi_itm_tp))
                                    {
                                        decimal _pickQty = 0;
                                        if (IsPriceLevelAllowDoAnyStatus)
                                            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item).ToList().Select(x => x.Sad_qty).Sum();
                                        else
                                            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item && x.Mi_itm_stus == _status).ToList().Select(x => x.Sad_qty).Sum();

                                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                        List<InventoryLocation> _inventoryLocation = null;
                                        if (IsPriceLevelAllowDoAnyStatus)
                                            _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), /*Session["UserDefLoca"].ToString()*/dispatch, _item, string.Empty);
                                        else
                                            _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), /*Session["UserDefLoca"].ToString()*/dispatch, _item, _status);
                                        if (_inventoryLocation != null)
                                            if (_inventoryLocation.Count > 0)
                                            {
                                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                                if (_pickQty > _invBal)
                                                {
                                                    if (string.IsNullOrEmpty(_noInventoryBalance))
                                                        _noInventoryBalance = _item;
                                                    else
                                                        _noInventoryBalance = "," + _item;
                                                }
                                            }
                                            else
                                            {
                                                if (string.IsNullOrEmpty(_noInventoryBalance))
                                                    _noInventoryBalance = _item;
                                                else
                                                    _noInventoryBalance = "," + _item;
                                            }
                                        else
                                        {
                                            if (string.IsNullOrEmpty(_noInventoryBalance))
                                                _noInventoryBalance = _item;
                                            else
                                                _noInventoryBalance = "," + _item;
                                        }
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(_taxNotdefine) && !_isStrucBaseTax)
                            {
                                _isCheckedPriceCombine = false;
                                DisplayMessage(_taxNotdefine + " does not have setup tax definition for the selected status. Please contact Inventory dept.", 1);
                                return;
                            }
                            //if (!string.IsNullOrEmpty(_serialiNotpick))
                            //{
                            //    DisplayMessage("Item quantity and picked serial mismatch for the following item(s) " + _serialiNotpick, 1);
                            //    return;
                            //}
                            if (!string.IsNullOrEmpty(_serialDuplicate))
                            {
                                _isCheckedPriceCombine = false;
                                //DisplayMessage("Serial duplicating for the following item(s) " + _serialDuplicate, 1);
                                DisplayMessage("Serial duplicating!");
                                return;
                            }
                            if (!string.IsNullOrEmpty(_noInventoryBalance) && !IsGiftVoucher(_itm.Mi_itm_tp))
                            {
                                _isCheckedPriceCombine = false;
                                DisplayMessage(_noInventoryBalance + " item(s) does not having inventory balance for release.", 1);
                                return;
                            }

                            if (!string.IsNullOrEmpty(_noWarrantySetup))
                            {
                                _isCheckedPriceCombine = false;
                                DisplayMessage(_noWarrantySetup + " item(s)'s warranty not define.", 1);
                                return;
                            }
                            _isFirstPriceComItem = true;
                            _isCheckedPriceCombine = true;
                        }
                if (_isCompleteCode && _isInventoryCombineAdded == false)
                    BindItemComponent(txtItem.Text);
                if (_masterItemComponent != null && _masterItemComponent.Count > 0 && _isInventoryCombineAdded == false)
                {
                    string _combineStatus = string.Empty;
                    decimal _discountRate = -1;
                    decimal _combineQty = 0;
                    string _mainItem = string.Empty;
                    _combineCounter = 0;
                    _isInventoryCombineAdded = true; _isCombineAdding = true;
                    if (string.IsNullOrEmpty(_combineStatus)) _combineStatus = cmbStatus.SelectedValue.ToString();
                    if (_combineQty == 0) _combineQty = Convert.ToDecimal(txtQty.Text);
                    if (_discountRate == -1) _discountRate = Convert.ToDecimal(txtDisRate.Text);
                    List<MasterItemComponent> _comItem = new List<MasterItemComponent>();
                    var _item_ = (from _n in _masterItemComponent where _n.Micp_itm_tp == "M" select _n.ComponentItem.Mi_cd).ToList();
                    if (!string.IsNullOrEmpty(_item_[0]))
                    {
                        string _mItem = Convert.ToString(_item_[0]);
                        _mainItem = Convert.ToString(_item_[0]);
                        _priceDetailRef = new List<PriceDetailRef>();
                        _priceDetailRef = CHNLSVC.Sales.GetPrice(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, _mItem, _combineQty, Convert.ToDateTime(txtdate.Text));
                        _priceDetailRef = _priceDetailRef.Where(X => X.Sapd_price_type == 0).ToList();
                        if (CheckItemWarranty(_mItem, cmbStatus.SelectedValue.ToString().Trim()))
                        {
                            DisplayMessage(_mItem + " item's warranty period not setup by the inventory department. Please contact inventory department", 1);
                            _isInventoryCombineAdded = false;
                            return;
                        }

                        if (_priceDetailRef.Count <= 0)
                        {
                            DisplayMessage(_item_[0].ToString() + " does not having price. Please contact IT dept.", 1);
                            _isInventoryCombineAdded = false;
                            return;
                        }
                        else
                        {
                            if (CheckBlockItem(_mItem, _priceDetailRef[0].Sapd_price_type, _isCombineAdding))
                            {
                                _isInventoryCombineAdded = false; return;
                            }
                            if (_priceDetailRef.Count == 1 && _priceDetailRef[0].Sapd_price_type != 0 && _priceDetailRef[0].Sapd_price_type != 4)
                            {
                                DisplayMessage(_item_[0].ToString() + " price is available for only promotion. Complete code does not support for promotion", 1);
                                _isInventoryCombineAdded = false; return;
                            }
                        }
                    }
                    foreach (MasterItemComponent _com in _masterItemComponent.Where(X => X.ComponentItem.Mi_cd != _item_[0]))
                    {
                        if (CheckItemWarranty(_com.ComponentItem.Mi_cd, cmbStatus.SelectedValue.ToString().Trim()))
                        {
                            DisplayMessage(_com.ComponentItem.Mi_cd + " item's warranty period not setup by the inventory department. Please contact inventory department", 1);
                            _isInventoryCombineAdded = false;
                            return;
                        }
                        if (CheckBlockItem(_com.ComponentItem.Mi_cd, _priceDetailRef[0].Sapd_price_type, _isCombineAdding))
                        {
                            _isInventoryCombineAdded = false;
                            return;
                        }
                    }
                    bool _isMainSerialCheck = false;
                    if (ScanSerialList != null && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                    {
                        if (ScanSerialList.Count > 0)
                        {
                            if (_isMainSerialCheck == false)
                            {
                                var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == _item_[0].ToString() && x.Tus_ser_1 == ScanSerialNo);
                                if (_dup != null)
                                    if (_dup.Count() > 0)
                                    {
                                        DisplayMessage(_item_[0].ToString() + " serial " + ScanSerialNo + " is already picked!", 1);
                                        _isInventoryCombineAdded = false;
                                        return;
                                    }
                                _isMainSerialCheck = true;
                            }
                            foreach (MasterItemComponent _com in _masterItemComponent)
                            {
                                string _serial = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _com.ComponentItem.Mi_cd).Select(y => y.Tus_ser_1).ToString();
                                var _dup = ScanSerialList.Where(x => x.Tus_ser_1 == _serial && x.Tus_itm_cd == _com.ComponentItem.Mi_cd);
                                if (_dup != null)
                                    if (_dup.Count() > 0)
                                    {
                                        DisplayMessage("Item " + _com.ComponentItem.Mi_cd + "," + _serial + " serial is already picked!", 1);
                                        _isInventoryCombineAdded = false;
                                        return;
                                    }
                            }
                        }
                    }
                    if (InventoryCombinItemSerialList.Count == 0)
                    {
                        _isCombineAdding = true;
                        foreach (MasterItemComponent _com in _masterItemComponent)
                        {
                            //List<MasterItemTax> _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _com.ComponentItem.Mi_cd, _combineStatus, string.Empty, string.Empty);
                            List<MasterItemTax> _taxs = new List<MasterItemTax>();
                            if (_isStrucBaseTax == true)       //kapila added one in pos invoice copy by darshana
                            {
                                MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _com.ComponentItem.Mi_cd);
                                _taxs = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), _com.ComponentItem.Mi_cd, _combineStatus, string.Empty, string.Empty, _mstItem.Mi_anal1);
                            }
                            else
                                _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _com.ComponentItem.Mi_cd, _combineStatus, string.Empty, string.Empty);





                            if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                            {
                                DisplayMessage(_com.ComponentItem.Mi_cd + " does not have setup tax definition for the selected status. Please contact Inventory dept.", 1);
                                _isInventoryCombineAdded = false;
                                return;
                            }

                            if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                            {
                                decimal _pickQty = 0;
                                if (IsPriceLevelAllowDoAnyStatus) _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd).ToList().Select(x => x.Sad_qty).Sum();
                                else _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd && x.Mi_itm_stus == cmbStatus.SelectedValue.ToString().Trim()).ToList().Select(x => x.Sad_qty).Sum();
                                _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _com.ComponentItem.Mi_cd, cmbStatus.SelectedValue.ToString().Trim());
                                if (_inventoryLocation != null)
                                    if (_inventoryLocation.Count > 0)
                                    {
                                        decimal _invBal = _inventoryLocation[0].Inl_qty;
                                        if (_pickQty > _invBal)
                                        {
                                            DisplayMessage(_com.ComponentItem.Mi_cd + " item quantity exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), 1);
                                            _isInventoryCombineAdded = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        DisplayMessage(_com.ComponentItem.Mi_cd + " item quantity exceeding it's inventory balance. Inventory balance : " + FormatToQty("0"), 1);
                                        _isInventoryCombineAdded = false;
                                        return;
                                    }
                                else
                                {
                                    DisplayMessage(_com.ComponentItem.Mi_cd + " item quantity exceeding it's inventory balance. Inventory balance : " + FormatToQty("0"), 1);
                                    _isInventoryCombineAdded = false;
                                    return;
                                }
                            }
                            _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _com.ComponentItem.Mi_cd);

                            if (_itm.Mi_is_ser1 == 1 && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                            {
                                _comItem.Add(_com);
                            }
                        }

                        if (_comItem.Count > 1 && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                        {//hdnItemCode.value
                            ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _mainItem.Trim(), txtSerialNo.Text.Trim());
                            if (_pick != null)
                                if (!string.IsNullOrEmpty(_pick.Tus_itm_cd))
                                {
                                    var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _pick.Tus_itm_cd && x.Tus_ser_1 == _pick.Tus_ser_1).ToList();
                                    if (_dup != null)
                                        if (_dup.Count <= 0)
                                            InventoryCombinItemSerialList.Add(_pick);
                                }
                            _comItem.ForEach(x => x.Micp_itm_cd = _combineStatus);
                            var _listComItem = (from _one in _comItem where _one.ComponentItem.Mi_itm_tp != "M" select new { Mi_cd = _one.ComponentItem.Mi_cd, Mi_longdesc = _one.ComponentItem.Mi_longdesc, Micp_itm_cd = _one.Micp_itm_cd, Micp_qty = _one.Micp_qty, Mi_itm_tp = _one.ComponentItem.Mi_itm_tp }).ToList();
                            DisplayMessage("--------- Develop ! 6380 ------------", 1);
                            //gvPopComItem.DataSource = _listComItem;
                            //pnlInventoryCombineSerialPick.Visible = true;
                            //pnlMain.Enabled = false;
                            //_isInventoryCombineAdded = false;
                            //this.Cursor = Cursors.Default;
                            return;
                        }
                        else if (_comItem.Count == 1 && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                        {//hdnItemCode.Value
                            ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _mainItem.Trim(), txtSerialNo.Text.Trim());
                            if (_pick != null)
                                if (!string.IsNullOrEmpty(_pick.Tus_itm_cd))
                                { var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _pick.Tus_itm_cd && x.Tus_ser_1 == _pick.Tus_ser_1).ToList(); if (_dup != null)                                        if (_dup.Count <= 0) InventoryCombinItemSerialList.Add(_pick); }
                        }
                    }
                    SSCombineLine += 1;
                    foreach (MasterItemComponent _com in _masterItemComponent.OrderByDescending(x => x.ComponentItem.Mi_itm_tp))
                    {
                        //If going to deliver now
                        if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false && InventoryCombinItemSerialList.Count > 0)
                        {
                            var _comItemSer = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _com.ComponentItem.Mi_cd).ToList();
                            if (_comItemSer != null)
                                if (_comItemSer.Count > 0)
                                {
                                    foreach (ReptPickSerials _serItm in _comItemSer)
                                    {
                                        txtSerialNo.Text = _serItm.Tus_ser_1;
                                        ScanSerialNo = txtSerialNo.Text;
                                        txtSerialNo.Text = ScanSerialNo;
                                        txtItem.Text = _com.ComponentItem.Mi_cd;
                                        cmbStatus.SelectedValue = _combineStatus;
                                        txtQty.Text = FormatToQty("1");

                                        bool isJSEnd = false;
                                        CheckQty(false, out isJSEnd);

                                        txtDisRate.Text = SetDecimalPoint(Convert.ToString(_discountRate));
                                        txtDisAmt.Text = SetDecimalPoint(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                                        txtTaxAmt.Text = SetDecimalPoint(FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true))));
                                        txtLineTotAmt.Text = FormatToCurrency("0");
                                        CalculateItem();
                                        AddItem(false, string.Empty);
                                        ScanSerialNo = string.Empty;
                                        txtSerialNo.Text = string.Empty;
                                        txtSerialNo.Text = string.Empty;
                                    }
                                    _combineCounter += 1;
                                }
                                else
                                {
                                    txtItem.Text = _com.ComponentItem.Mi_cd;
                                    cmbStatus.SelectedValue = _combineStatus;
                                    txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));

                                    bool isJSEND = false;
                                    CheckQty(false, out isJSEND);

                                    txtDisRate.Text = SetDecimalPoint(Convert.ToString(_discountRate));
                                    txtDisAmt.Text = SetDecimalPoint(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                                    txtTaxAmt.Text = SetDecimalPoint(FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true))));
                                    txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    AddItem(false, string.Empty);
                                    ScanSerialNo = string.Empty;
                                    txtSerialNo.Text = string.Empty;
                                    txtSerialNo.Text = string.Empty;
                                    _combineCounter += 1;
                                }
                        }
                        //If deliver later
                        else if ((chkDeliverLater.Checked || chkDeliverNow.Checked) && InventoryCombinItemSerialList.Count == 0)
                        {
                            txtItem.Text = _com.ComponentItem.Mi_cd;
                            LoadItemDetail(txtItem.Text.Trim());
                            cmbStatus.SelectedValue = _combineStatus;
                            txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));

                            bool isJSEND = false;
                            CheckQty(false, out isJSEND);


                            txtDisRate.Text = SetDecimalPoint(Convert.ToString(_discountRate));
                            txtDisAmt.Text = SetDecimalPoint(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                            txtTaxAmt.Text = SetDecimalPoint(FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true))));
                            txtLineTotAmt.Text = FormatToCurrency("0");
                            CalculateItem();
                            AddItem(false, string.Empty); _combineCounter += 1;
                        }
                    }
                    if (_combineCounter == _masterItemComponent.Count)
                    {
                        _masterItemComponent = new List<MasterItemComponent>();
                        _isCompleteCode = false; _isInventoryCombineAdded = false;
                        _isCombineAdding = false; ScanSerialNo = string.Empty;
                        InventoryCombinItemSerialList = new List<ReptPickSerials>();
                        txtSerialNo.Text = string.Empty;
                        if (_isCombineAdding == false)
                        {
                            txtSerialNo.Text = "";
                            ClearAfterAddItem();
                            _combineCounter = 0;
                            SSPriceBookSequance = "0";
                            SSPriceBookItemSequance = "0";
                            SSPriceBookPrice = 0;
                            if (_isCombineAdding == false) SSPromotionCode = string.Empty;
                            SSPRomotionType = 0;

                            //_//txtItem.Focus();
                            BindAddItem();
                            SetDecimalTextBoxForZero(true);

                            decimal _tobepay = 0;
                            if (lblSVatStatus.Text == "Available")
                                _tobepay = FigureRoundUp(Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim()), true);
                            else
                                _tobepay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());



                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "confimationAddAnother()", true);
                            //if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            //{
                            //    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                            //    {
                            //        txtSerialNo.Focus();
                            //    }
                            //    else
                            //    {
                            //        txtItem.Focus();
                            //    }
                            //}
                            //else
                            //{
                            //    // ucPayModes1.button1.Focus();
                            //}
                        }
                        return;
                    }
                }
                bool _isAgePriceLevel = false;
                int _noofDays = 0;
                DateTime _serialpickingdate = Convert.ToDateTime(txtdate.Text).Date;
                CheckNValidateAgeItem(txtItem.Text.Trim(), _itm.Mi_cate_1, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), cmbStatus.SelectedValue.ToString(), out _isAgePriceLevel, out _noofDays);
                if (_isAgePriceLevel)
                    _serialpickingdate = _serialpickingdate.AddDays(-_noofDays);
                if (_priceBookLevelRef.Sapl_is_serialized)
                {
                    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory))
                    {
                        if (_itm.Mi_is_ser1 == 1)
                        {
                            if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                            {
                                DisplayMessage("Please select the serial number", 1);
                                txtSerialNo.Focus();
                                return;
                            }
                            _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                            if (_serLst == null || string.IsNullOrEmpty(_serLst.Tus_com))
                            {
                                if (_isAgePriceLevel)
                                    DisplayMessage("There is no serial available for the selected item in a ageing price level", 1);
                                else
                                    DisplayMessage("There is no serial available for the selected item", 1);
                                return;
                            }
                        }
                        else if (_itm.Mi_is_ser1 == 0)
                        {
                            if (IsPriceLevelAllowDoAnyStatus == false)
                                _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                            else
                                _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                            if (_nonserLst == null || _nonserLst.Count <= 0)
                            {
                                if (_isAgePriceLevel)
                                    DisplayMessage("There is no available quantity for the selected item in a aging price level", 1);
                                else

                                    DisplayMessage("There is no available quantity for the selected item", 1);
                                return;
                            }
                        }
                        else if (_itm.Mi_is_ser1 == -1)
                        {
                        }
                    }
                    else
                    {
                        if (_itm.Mi_is_ser1 == 1)
                            _serLst = VirtualSerialLine(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text.Trim()), txtSerialNo.Text.Trim())[0];
                        else if
                            (_itm.Mi_is_ser1 == 0)
                            _nonserLst = VirtualSerialLine(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text.Trim()), string.Empty);
                    }
                }
                else if ((chkDeliverLater.Checked == false && chkDeliverNow.Checked == false) || IsGiftVoucher(_itm.Mi_itm_tp) || (_isRegistrationMandatory))
                {
                    if (_itm.Mi_is_ser1 == 1)
                    {
                        //if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                        //{
                        //    DisplayMessage("Please select the serial number", 1);
                        //    txtSerialNo.Focus(); return;
                        //}
                        if (!string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                        {
                            bool _isGiftVoucher = IsGiftVoucher(_itm.Mi_itm_tp);
                            if (!_isGiftVoucher)
                                _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), dispatch /*Session["UserDefLoca"].ToString()*/, string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                            else
                                _serLst = CHNLSVC.Inventory.GetGiftVoucherDetail(Session["UserCompanyCode"].ToString(), dispatch/*Session["UserDefProf"].ToString()*/, txtItem.Text.Trim(), Convert.ToInt32(_serial2), Convert.ToInt32(txtSerialNo.Text.Trim()), _prefix);

                            if (_serLst != null && !string.IsNullOrEmpty(_serLst.Tus_com))
                            {
                                if (_serLst.Tus_doc_dt >= _serialpickingdate)
                                {
                                    if (_isAgePriceLevel)
                                    {
                                        DisplayMessage("There is no serial available for the selected item in a aging price level", 1);
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //_serLst = new ReptPickSerials();
                        }
                    }
                    else if (_itm.Mi_is_ser1 == 0)
                    {
                        if (IsPriceLevelAllowDoAnyStatus == false) _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), dispatch /*Session["UserDefLoca"].ToString()*/, txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                        else _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), /*Session["UserDefLoca"].ToString()*/dispatch, txtItem.Text.Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                        if (_nonserLst == null || _nonserLst.Count <= 0)
                        {
                            if (_isAgePriceLevel)

                                DisplayMessage("There is no available quantity for the selected item in a aging price level", 1);
                            else
                                DisplayMessage("There is no available quantity for the selected item", 1);
                            return;
                        }
                    }
                    else if (_itm.Mi_is_ser1 == -1)
                    {
                        //if (IsPriceLevelAllowDoAnyStatus == false) _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                        //else _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                        //if (_nonserLst == null || _nonserLst.Count <= 0)
                        //{ this.Cursor = Cursors.Default; if (_isAgePriceLevel) using (new CenterWinDialog(this)) { MessageBox.Show("There is no available qty for the selected item in a ageing price level.", "No Available Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } else                                using (new CenterWinDialog(this)) { MessageBox.Show("There is no available qty for the selected item.", "No Available Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                    }
                }
                if ((SSPriceBookSequance == "0" || string.IsNullOrEmpty(SSPriceBookSequance)) && !IsGiftVoucher(_itm.Mi_itm_tp) && _MasterProfitCenter.Mpc_without_price == false && _priceBookLevelRef.Sapl_is_without_p == false)
                    if (!_isCombineAdding)
                    {
                        DisplayMessage("Please select the valid price", 1);
                        return;
                    }
                if (string.IsNullOrEmpty(txtQty.Text.Trim()))
                {
                    DisplayMessage("Please select the valid quantity", 1);
                    return;
                }
                if (Convert.ToDecimal(txtQty.Text) == 0)
                {
                    // DisplayMessage("Please select the valid quantity", 1);
                    return;
                }
                if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0)
                {
                    DisplayMessage("Please select the valid quantity", 1);
                    return;
                }
                if (string.IsNullOrEmpty(txtUnitPrice.Text.Trim()))
                {
                    DisplayMessage("Please select the valid unit price", 1);
                    return;
                }
                if (!_isCombineAdding)
                {
                    // List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), string.Empty, string.Empty);
                    List<MasterItemTax> _tax = new List<MasterItemTax>();
                    if (_isStrucBaseTax == true)       //kapila
                    {
                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text);
                        _tax = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), cmbStatus.Text.Trim(), string.Empty, string.Empty, _mstItem.Mi_anal1);
                    }
                    else
                        _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), cmbStatus.Text.Trim(), string.Empty, string.Empty);





                    if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                    {
                        DisplayMessage("Tax rates not setup for selected item code and item status.Please contact Inventory Department", 1);
                        cmbStatus.Focus();
                        return;
                    }
                }
                if (Convert.ToDecimal(txtUnitPrice.Text.Trim()) == 0 && _isCombineAdding == false && !IsGiftVoucher(_itm.Mi_itm_tp) && (SSPriceBookSequance == "0" || string.IsNullOrEmpty(SSPriceBookSequance)))
                {
                    bool isJSEnd = false;
                    bool _isTerminate = CheckQty(false, out isJSEnd);
                    if (_isTerminate)
                    {
                        return;
                    }
                }
                if (CheckBlockItem(txtItem.Text.Trim(), SSPRomotionType, _isCombineAdding))
                    return;
                if (_isCombineAdding == false && _MasterProfitCenter.Mpc_without_price == false && _priceBookLevelRef.Sapl_is_without_p == false)
                {
                    PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(txtItem.Text.Trim(), Convert.ToInt32(SSPriceBookItemSequance), Convert.ToInt32(SSPriceBookSequance));
                    if (_lsts != null && _isCombineAdding == false)
                    {
                        if (string.IsNullOrEmpty(_lsts.Sapd_itm_cd))
                        {
                            DisplayMessage(txtItem.Text + " does not available price. Please contact IT dept.", 1);
                            return;
                        }
                        else
                        {
                            decimal _tax = 0;
                            if (MainTaxConstant != null && MainTaxConstant.Count > 0)
                            {
                                _tax = MainTaxConstant[0].Mict_tax_rate;
                            }

                            decimal sysUPrice = FigureRoundUp(_lsts.Sapd_itm_price * _tax, true);
                            decimal pickUPrice = Convert.ToDecimal(txtUnitPrice.Text);
                            if (_MasterProfitCenter != null && _priceBookLevelRef != null)
                                if (!string.IsNullOrEmpty(_MasterProfitCenter.Mpc_com) && !string.IsNullOrEmpty(_priceBookLevelRef.Sapl_com_cd))

                                    if (!_MasterProfitCenter.Mpc_without_price && !_priceBookLevelRef.Sapl_is_without_p)
                                        if (GeneralDiscount_new.SADD_IS_EDIT != 1)
                                        {
                                            //comment by darshana 23-08-2013
                                            //re-open by chamal 18-Nov-2014
                                            decimal sysUPrice2 = Math.Round(_lsts.Sapd_itm_price, 2);
                                            if (Math.Round(sysUPrice2) != Math.Round(pickUPrice, 0))
                                            {
                                                DisplayMessage("Price Book price and the unit price is different. Please check the price you selected!", 1);
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            if (sysUPrice != pickUPrice)
                                                if (sysUPrice > pickUPrice)
                                                {
                                                    decimal sysEditRate = GeneralDiscount_new.SADD_EDIT_RT;
                                                    decimal ddUprice = sysUPrice - ((sysUPrice * sysEditRate) / 100);

                                                    //if (ddUprice > pickUPrice)
                                                    if (Math.Round(ddUprice, 2) > pickUPrice)
                                                    {
                                                        DisplayMessage("Price Book price and the unit price is different. Please check the price you selected!", 1);
                                                        return;
                                                    }
                                                }
                                        }
                        }
                    }
                    else
                    {
                        if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized == false && !IsGiftVoucher(_itm.Mi_itm_tp))
                        {
                            DisplayMessage(txtItem.Text + " does not available price. Please contact IT dept.", 1);
                            return;
                        }
                    }
                }
                if (_isCombineAdding == false)
                    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory))
                    {
                        if (_itm.Mi_is_ser1 == 1 && ScanSerialList != null)
                        {
                            var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == txtItem.Text.ToUpper() && x.Tus_ser_1 == ScanSerialNo).ToList();
                            if (_dup != null)
                                if (_dup.Count > 0)
                                {
                                    txtSerialNo.Focus();
                                    DisplayMessage(ScanSerialNo + " serial is already picked!", 1);
                                    return;
                                }
                        }

                        if (!IsPriceLevelAllowDoAnyStatus)
                        {
                            if (_serLst != null)
                                if (string.IsNullOrEmpty(_serLst.Tus_com))
                                {
                                    if (_serLst.Tus_itm_stus != cmbStatus.SelectedValue.ToString().Trim())
                                    {
                                        DisplayMessage(ScanSerialNo + " serial status is not match with the price level status", 1);
                                        txtSerialNo.Focus();
                                        return;
                                    }
                                }
                        }
                    }

                #endregion Price Combine Checking Process - Costing Dept.

                CalculateItem();

                #region Check Inventory Balance if deliver now!

                if (_isCombineAdding == false)
                    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory))
                    {
                        decimal _pickQty = 0;
                        if (_invoiceItemList != null)
                        {
                            if (IsPriceLevelAllowDoAnyStatus)
                            {
                                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.ToUpper().Trim()).ToList().Select(x => x.Sad_qty).Sum();
                            }
                            else
                            {
                                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.ToUpper().Trim() && x.Mi_itm_stus == cmbStatus.SelectedValue.ToString().Trim()).ToList().Select(x => x.Sad_qty).Sum();
                            }
                        }

                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                        List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim());

                        if (_inventoryLocation != null)
                            if (_inventoryLocation.Count > 0)
                            {
                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                if (_pickQty > _invBal)
                                {
                                    //cmt by lakshan
                                    // DisplayMessage(txtItem.Text + " item quantity exceeding its inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), 1);
                                    //return;
                                }
                            }
                            else
                            {
                                //DisplayMessage(txtItem.Text + " item qty exceeding its inventory balance. Inventory balance  0", 1); cmt by lakshan
                                // DisplayMessageJS(txtItem.Text + " item quantity exceeding its inventory balance. Inventory balance  0"); cmt by lakshan
                                //return;
                            }
                        else
                        {
                            //DisplayMessage(txtItem.Text + " item qty exceeding its inventory balance. Inventory balance  0", 1);cmt by lakshan
                            //DisplayMessageJS(txtItem.Text + " item quantity exceeding its inventory balance. Inventory balance  0");cmt by lakshan
                            //return;cmt by lakshan
                        }

                        if (_itm.Mi_is_ser1 == 1 && ScanSerialList != null && ScanSerialList.Count > 0)
                        {
                            var _serDup = (from _lst in ScanSerialList
                                           where _lst.Tus_ser_1 == txtSerialNo.Text.Trim() && _lst.Tus_itm_cd == txtItem.Text.ToUpper().Trim()
                                           select _lst).ToList();

                            if (_serDup != null)
                                if (_serDup.Count > 0)
                                {
                                    DisplayMessage("Selected Serial is duplicating", 1);
                                    return;
                                }
                        }
                    }
                List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
                if (_lvl != null)
                    if (_lvl.Count > 0)
                    {
                        var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == cmbStatus.SelectedValue.ToString().Trim() select _l).ToList();
                        if (_lst != null)
                            if (_lst.Count > 0)
                            {
                                DataTable _temWarr = CHNLSVC.Sales.GetPCWara(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDateTime(txtdate.Text).Date);

                                if (_lst[0].Sapl_set_warr == true)
                                {
                                    WarrantyPeriod = _lst[0].Sapl_warr_period;
                                }
                                else if (_temWarr != null && _temWarr.Rows.Count > 0)
                                {
                                    WarrantyPeriod = Convert.ToInt32(_temWarr.Rows[0]["SPW_WARA_PD"].ToString());
                                    WarrantyRemarks = _temWarr.Rows[0]["SPW_WARA_RMK"].ToString();
                                }
                                else
                                {
                                    MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim());
                                    if (_period != null)
                                    {
                                        WarrantyPeriod = _period.Mwp_val;
                                        WarrantyRemarks = _period.Mwp_rmk;
                                    }
                                    else
                                    {
                                        DisplayMessage("Warranty period not setup by the inventory department. Please contact inventory department", 1);
                                        return;
                                    }
                                }
                            }
                    }
                bool _isDuplicateItem = false;
                Int32 _duplicateComLine = 0;
                Int32 _duplicateItmLine = 0;
                if (_invoiceItemList == null || _invoiceItemList.Count <= 0)
                {
                    _isDuplicateItem = false;
                    _lineNo += 1;
                    if (!_isCombineAdding) SSCombineLine += 1;
                    _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm, _originalItem));
                }
                else
                {
                    if (Isrequestbase == false)
                    {

                        var _duplicateItem = from _list in _invoiceItemList
                                             where _list.Sad_itm_cd == txtItem.Text.ToUpper() && _list.Sad_itm_stus == cmbStatus.SelectedValue.ToString() && _list.Sad_pbook == cmbBook.Text && _list.Sad_pb_lvl == cmbLevel.Text && _list.Sad_unit_rt == Convert.ToDecimal(txtUnitPrice.Text) && _list.Sad_disc_rt == Convert.ToDecimal(txtDisRate.Text.Trim())
                                             select _list;
                        //var _duplicateItem = from _list in _invoiceItemList
                        //                     where _list.Sad_itm_cd == txtItem.Text.ToUpper()
                        //                     select _list;
                        if (_duplicateItem.Count() > 0)
                        {
                            if (_isPromotion == true)
                            {
                                //23/Apr/2016 add rukshan (promotion have same item code then add new recode)
                                _isDuplicateItem = false;
                                _lineNo += 1;
                                if (!_isCombineAdding) SSCombineLine += 1;
                                _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm, _originalItem));
                            }
                            else
                            {
                                if (_isCombineAdding)
                                {
                                    foreach (var _similerList in _duplicateItem)
                                    {
                                        _duplicateComLine = _similerList.Sad_job_line;
                                        _duplicateItmLine = _similerList.Sad_itm_line;
                                        _similerList.Sad_disc_amt = Convert.ToDecimal(_similerList.Sad_disc_amt) + Convert.ToDecimal(txtDisAmt.Text);
                                        _similerList.Sad_itm_tax_amt = Convert.ToDecimal(_similerList.Sad_itm_tax_amt) + Convert.ToDecimal(txtTaxAmt.Text);
                                        _similerList.Sad_qty = Convert.ToDecimal(txtQty.Text) + _similerList.Sad_qty;
                                        // }
                                        _similerList.Sad_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
                                        _similerList.Sad_unit_amt = _similerList.Sad_unit_rt * _similerList.Sad_qty;
                                        _similerList.Sad_tot_amt = Convert.ToDecimal(_similerList.Sad_tot_amt) + Convert.ToDecimal(txtLineTotAmt.Text);
                                        _similerList.Sad_pbook = cmbBook.Text;//add 09/apr/2016
                                        _similerList.Sad_pb_lvl = cmbLevel.Text;//add 09/apr/2016
                                    }
                                }
                                else
                                {
                                    _isDuplicateItem = true;
                                    foreach (var _similerList in _duplicateItem)
                                    {
                                        _duplicateComLine = _similerList.Sad_job_line;
                                        _duplicateItmLine = _similerList.Sad_itm_line;
                                        _similerList.Sad_disc_amt = Convert.ToDecimal(_similerList.Sad_disc_amt) + Convert.ToDecimal(txtDisAmt.Text);
                                        _similerList.Sad_itm_tax_amt = Convert.ToDecimal(_similerList.Sad_itm_tax_amt) + Convert.ToDecimal(txtTaxAmt.Text);


                                        //   if (_similerList.Sad_itm_cd == txtItem.Text)
                                        //  {
                                        //      _similerList.Sad_qty = Convert.ToDecimal(txtQty.Text) + _similerList.Sad_qty;
                                        //  }
                                        //  else
                                        //  {
                                        #region chg below lines to fixed AAL invoice amount issue 04Oct2017
                                        // _similerList.Sad_qty = Convert.ToDecimal(txtQty.Text);//+ _similerList.Sad_qty;
                                        _similerList.Sad_qty = Convert.ToDecimal(txtQty.Text) + _similerList.Sad_qty;
                                        #endregion
                                        //  _similerList.Sad_qty = Convert.ToDecimal(txtQty.Text);//+ _similerList.Sad_qty;

                                        // }
                                        _similerList.Sad_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
                                        _similerList.Sad_unit_amt = Convert.ToDecimal(txtUnitPrice.Text) * _similerList.Sad_qty;
                                        _similerList.Sad_tot_amt = Convert.ToDecimal(_similerList.Sad_tot_amt) + Convert.ToDecimal(txtLineTotAmt.Text);
                                        _similerList.Sad_pbook = cmbBook.Text;//add 09/apr/2016
                                        _similerList.Sad_pb_lvl = cmbLevel.Text;//add 09/apr/2016
                                    }
                                }

                            }
                        }
                        else
                        {
                            _isDuplicateItem = false;
                            _lineNo = _lineNo + 1;
                            if (!_isCombineAdding) SSCombineLine += 1;
                            _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm, _originalItem));
                        }
                    }
                    else
                    {
                        int Reqline = Convert.ToInt32(Session["linenoreq"]);
                        var _duplicateItem = from _list in _invoiceItemList
                                             where _list.Sad_itm_cd == txtItem.Text.ToUpper() && _list.Sad_itm_line == Reqline
                                             select _list;
                        if (_duplicateItem.Count() > 0)
                        {


                            _isDuplicateItem = true;
                            foreach (var _similerList in _duplicateItem)
                            {
                                _duplicateComLine = _similerList.Sad_job_line;
                                _duplicateItmLine = _similerList.Sad_itm_line;
                                _similerList.Sad_disc_amt = Convert.ToDecimal(txtDisAmt.Text);
                                _similerList.Sad_itm_tax_amt = Convert.ToDecimal(txtTaxAmt.Text);


                                //   if (_similerList.Sad_itm_cd == txtItem.Text)
                                //  {
                                //      _similerList.Sad_qty = Convert.ToDecimal(txtQty.Text) + _similerList.Sad_qty;
                                //  }
                                //  else
                                //  {

                                #region chg below lines to fixed AAL invoice amount issue 04Oct2017
                                // _similerList.Sad_qty = Convert.ToDecimal(txtQty.Text);//+ _similerList.Sad_qty;
                                _similerList.Sad_qty = Convert.ToDecimal(txtQty.Text) + _similerList.Sad_qty;
                                #endregion
                                // }
                                _similerList.Sad_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
                                _similerList.Sad_unit_amt = Convert.ToDecimal(txtUnitPrice.Text) * _similerList.Sad_qty;
                                _similerList.Sad_tot_amt = Convert.ToDecimal(txtLineTotAmt.Text);
                                _similerList.Sad_pbook = cmbBook.Text;//add 09/apr/2016
                                _similerList.Sad_pb_lvl = cmbLevel.Text;//add 09/apr/2016
                            }

                        }
                        else
                        {
                            _isDuplicateItem = false;
                            _lineNo = _lineNo + 1;
                            if (!_isCombineAdding) SSCombineLine += 1;
                            _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm, _originalItem));
                        }
                    }
                }
                //Adding Items to grid end here ----------------------------------------------------------------------

                #endregion Check Inventory Balance if deliver now!

                #region Adding Serial/Non Serial items

                //Scan By Serial ----------start----------------------------------
                if ((chkDeliverLater.Checked == false && chkDeliverNow.Checked == false) || _priceBookLevelRef.Sapl_is_serialized || IsGiftVoucher(_itm.Mi_itm_tp) || _isRegistrationMandatory)
                {
                    if (_isFirstPriceComItem)
                        _isCombineAdding = true;
                    if (ScanSequanceNo == 0) ScanSequanceNo = -100;
                    if (_itm.Mi_is_ser1 == 1)
                    {
                        if (!string.IsNullOrEmpty(txtSerialNo.Text))
                        {
                            _serLst.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                            _serLst.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine;
                            _serLst.Tus_usrseq_no = ScanSequanceNo;
                            _serLst.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                            _serLst.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty;
                            _serLst.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty;
                            _serLst.ItemType = _itm.Mi_itm_tp;
                            ScanSerialList.Add(_serLst);
                        }
                    }
                    if (_itm.Mi_is_ser1 == 0)
                    {
                        if (_nonserLst.Count < Convert.ToDecimal(txtQty.Text.Trim()))
                        {
                            if (_isAgePriceLevel == false)
                            {
                                DisplayMessage(txtItem.Text + " item quantity exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_nonserLst.Count)), 1);
                                var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                                foreach (InvoiceItem _one in _partly)
                                    DeleteIfPartlyAdded(_one.Sad_job_line, _one.Sad_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Sad_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);
                                return;
                            }
                            else
                            {
                                if (gvInvoiceItem.Rows.Count > 0)
                                {
                                    DisplayMessage("This serial can't select under aging price level. Please check the aging status with IT dept.", 1);
                                    return;
                                }
                                var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                                foreach (InvoiceItem _one in _partly)
                                    DeleteIfPartlyAdded(_one.Sad_job_line, _one.Sad_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Sad_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);
                                return;
                            }
                        }
                        _nonserLst.ForEach(x => x.Tus_base_doc_no = Convert.ToString(ScanSequanceNo));
                        _nonserLst.ForEach(x => x.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine);
                        _nonserLst.ForEach(x => x.Tus_usrseq_no = ScanSequanceNo);
                        _nonserLst.ForEach(x => x.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim()));
                        _nonserLst.ForEach(x => x.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty);
                        _nonserLst.ForEach(x => x.Tus_ser_id = -1);
                        _nonserLst.ForEach(x => x.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty);
                        _nonserLst.ForEach(x => x.ItemType = _itm.Mi_itm_tp);
                        ScanSerialList.AddRange(_nonserLst);
                    }
                    if (_itm.Mi_is_ser1 == -1)
                    {
                        //if (_nonserLst.Count < Convert.ToDecimal(txtQty.Text.Trim()))
                        //{
                        //    if (_isAgePriceLevel == false)
                        //    {
                        //        this.Cursor = Cursors.Default;
                        //        using (new CenterWinDialog(this)) { MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_nonserLst.Count)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        //        var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                        //        foreach (InvoiceItem _one in _partly)
                        //            DeleteIfPartlyAdded(_one.Sad_job_line, _one.Sad_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Sad_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);

                        //        return;
                        //    }
                        //    else
                        //    {
                        //        this.Cursor = Cursors.Default;
                        //        if (gvInvoiceItem.Rows.Count > 0) { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("This serial can't select under ageing price level. Please check the ageing status with IT dept.", "Age Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                        //        var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                        //        foreach (InvoiceItem _one in _partly)
                        //            DeleteIfPartlyAdded(_one.Sad_job_line, _one.Sad_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Sad_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);

                        //        return;
                        //    }
                        //}
                        ReptPickSerials _chk = new ReptPickSerials();
                        _chk.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                        _chk.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine;
                        _chk.Tus_usrseq_no = ScanSequanceNo;
                        _chk.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                        _chk.Tus_itm_cd = txtItem.Text.ToUpper().Trim();
                        _chk.Tus_itm_stus = cmbStatus.SelectedValue.ToString();
                        _chk.Tus_ser_id = 0;
                        _chk.Tus_qty = Convert.ToDecimal(txtQty.Text);
                        _chk.Tus_bin = BaseCls.GlbDefaultBin;
                        _chk.Tus_ser_1 = "N/A";
                        _chk.Tus_ser_2 = "N/A";
                        _chk.Tus_ser_3 = "N/A";
                        _chk.Tus_ser_4 = "N/A";
                        _chk.Tus_ser_id = 0;
                        _chk.Tus_serial_id = "0";
                        _chk.Tus_com = Session["UserCompanyCode"].ToString();
                        _chk.Tus_loc = Session["UserDefLoca"].ToString();
                        _chk.ItemType = _itm.Mi_itm_tp;
                        _chk.Tus_cre_by = Session["UserID"].ToString();
                        _chk.Tus_cre_by = Session["UserID"].ToString();
                        _chk.Tus_itm_desc = _itm.Mi_shortdesc;
                        _chk.Tus_itm_model = _itm.Mi_model;
                        _chk.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                        ScanSerialList.Add(_chk);

                        //_nonserLst.ForEach(x => x.Tus_base_doc_no = Convert.ToString(ScanSequanceNo));
                        //_nonserLst.ForEach(x => x.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine);
                        //_nonserLst.ForEach(x => x.Tus_usrseq_no = ScanSequanceNo);
                        //_nonserLst.ForEach(x => x.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim()));
                        //_nonserLst.ForEach(x => x.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty);
                        //_nonserLst.ForEach(x => x.Tus_ser_id = -1);
                        //_nonserLst.ForEach(x => x.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty);
                        //_nonserLst.ForEach(x => x.ItemType = _itm.Mi_itm_tp);
                        //_nonserLst.ForEach(x=>x.Tus_ser_1 = "N/A");
                        //_nonserLst.ForEach(x=>x.Tus_ser_2 = "N/A");
                        //_nonserLst.ForEach(x=>x.Tus_ser_3 = "N/A");
                        //_nonserLst.ForEach(x=>x.Tus_ser_4 = "N/A");
                        //_nonserLst.ForEach(x=>x.Tus_ser_id = 0);
                        //_nonserLst.ForEach(x=>x.Tus_serial_id = "0");
                        // _nonserLst.ForEach(x=>x.Tus_unit_cost = 0);
                        // _nonserLst.ForEach(x=>x.Tus_unit_price = 0);

                        //ScanSerialList.AddRange(_nonserLst);
                    }

                    gvPopSerial.DataSource = new List<ReptPickSerials>();
                    gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList();
                    gvPopSerial.DataSource = setSerialStatusDescriptions(ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList());
                    gvPopSerial.DataBind();
                    var filenamesList = new BindingList<ReptPickSerials>(ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList());


                    if (_isFirstPriceComItem)
                    {
                        _isCombineAdding = false;
                        _isFirstPriceComItem = false;
                    }

                    if (IsGiftVoucher(_itm.Mi_itm_tp)) _isCombineAdding = true;
                }

                #endregion Adding Serial/Non Serial items

                bool _isDuplicate = false;
                if (InvoiceSerialList != null)
                    if (InvoiceSerialList.Count > 0)
                    { if (_itm.Mi_is_ser1 == 1) { var _dup = (from _i in InvoiceSerialList where _i.Sap_ser_1 == txtSerialNo.Text.Trim() && _i.Sap_itm_cd == txtItem.Text.ToUpper().Trim() select _i).ToList(); if (_dup != null)                                if (_dup.Count > 0)                                    _isDuplicate = true; } }
                if (_isDuplicate == false)
                {
                    InvoiceSerial _invser = new InvoiceSerial();
                    _invser.Sap_del_loc = Session["UserDefLoca"].ToString();
                    _invser.Sap_itm_cd = txtItem.Text.ToUpper().Trim();
                    _invser.Sap_itm_line = _lineNo;
                    _invser.Sap_remarks = string.Empty;
                    _invser.Sap_seq_no = Convert.ToInt32(SSPriceBookSequance);
                    _invser.Sap_ser_1 = txtSerialNo.Text;
                    _invser.Sap_ser_line = _isCombineAdding ? Convert.ToInt32(SSCombineLine) : 0;
                    InvoiceSerialList.Add(_invser);
                }
                CalculateGrandTotal(Convert.ToDecimal(txtQty.Text), Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text), Convert.ToDecimal(txtTaxAmt.Text), true);
                if (_MainPriceCombinItem != null)
                {
                    string _combineStatus = string.Empty;
                    decimal _combineQty = 0;
                    bool _isSingleItemSerializedInCombine = true;
                    if (_MainPriceCombinItem.Count > 0 && _isCombineAdding == false)
                    {
                        _isCombineAdding = true;

                        if (string.IsNullOrEmpty(_combineStatus))
                            _combineStatus = cmbStatus.SelectedValue.ToString();
                        if (_combineQty == 0)
                            _combineQty = Convert.ToDecimal(txtQty.Text);
                        if (chkDeliverLater.Checked == true || chkDeliverNow.Checked == true)
                        {
                            foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                            {
                                string _originalItm = _list.Sapc_itm_cd; string _similerItem = _list.Similer_item;
                                _combineStatus = _list.Status; if (!string.IsNullOrEmpty(_similerItem)) txtItem.Text = _similerItem; else txtItem.Text = _list.Sapc_itm_cd;
                                if (_priceBookLevelRef.Sapl_is_serialized) txtSerialNo.Text = _list.Sapc_sub_ser;
                                LoadItemDetail(txtItem.Text.Trim());
                                if (IsGiftVoucher(_itemdetail.Mi_itm_tp))
                                {
                                    foreach (ReptPickSerials _lists in PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == txtItem.Text.ToUpper().Trim()).ToList())
                                    {
                                        txtSerialNo.Text = _lists.Tus_ser_1;
                                        ScanSerialNo = _lists.Tus_ser_1;
                                        string _originalItms = _lists.Tus_session_id;
                                        if (string.IsNullOrEmpty(_originalItm))
                                        {
                                            txtItem.Text = _lists.Tus_itm_cd; _serial2 = _lists.Tus_ser_2;
                                            _prefix = _lists.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                            cmbStatus.SelectedValue = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_price).Sum();
                                            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_qty).Sum();
                                            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                            txtUnitPrice.Text = UnitPrice.ToString("N2");
                                            if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                            txtDisRate.Text = FormatToCurrency("0");
                                            txtDisAmt.Text = FormatToCurrency("0");
                                            txtTaxAmt.Text = FormatToCurrency("0");
                                            txtLineTotAmt.Text = FormatToCurrency("0");
                                            CalculateItem(); AddItem(_isPromotion, string.Empty);
                                        }
                                        else
                                        {
                                            txtItem.Text = _lists.Tus_itm_cd; _serial2 = _lists.Tus_ser_2;
                                            _prefix = _lists.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                            cmbStatus.SelectedValue = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_price).Sum();
                                            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_qty).Sum();
                                            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                            txtUnitPrice.Text = UnitPrice.ToString("N2");
                                            if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                            txtDisRate.Text = FormatToCurrency("0");
                                            txtDisAmt.Text = FormatToCurrency("0");
                                            txtTaxAmt.Text = FormatToCurrency("0");
                                            txtLineTotAmt.Text = FormatToCurrency("0");
                                            CalculateItem(); AddItem(_isPromotion, _originalItm);
                                        }
                                        _combineCounter += 1;
                                    }
                                }
                                else
                                {
                                    cmbStatus.SelectedValue = _combineStatus;
                                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
                                    txtUnitPrice.Text = _list.Sapc_price.ToString("N2");
                                    if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty /* * _combineQty */))); else txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty)));
                                    txtDisRate.Text = FormatToCurrency("0");
                                    txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0");
                                    txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion, _originalItm);
                                    _combineCounter += 1;
                                }
                            }
                        }
                        else
                        {
                            if (PriceCombinItemSerialList == null || PriceCombinItemSerialList.Count == 0)
                                _isSingleItemSerializedInCombine = false;
                            foreach (ReptPickSerials _list in PriceCombinItemSerialList)
                            {
                                txtSerialNo.Text = _list.Tus_ser_1;
                                ScanSerialNo = _list.Tus_ser_1;
                                string _originalItm = _list.Tus_session_id;
                                _combineStatus = _list.Tus_itm_stus;
                                if (string.IsNullOrEmpty(_originalItm))
                                {
                                    txtItem.Text = _list.Tus_itm_cd; _serial2 = _list.Tus_ser_2;
                                    _prefix = _list.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                    cmbStatus.SelectedValue = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_price).Sum();
                                    decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_qty).Sum();
                                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                    txtUnitPrice.Text = UnitPrice.ToString("N2");
                                    txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty)));
                                    txtDisRate.Text = FormatToCurrency("0");
                                    txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0");
                                    txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion, string.Empty);
                                }
                                else
                                {
                                    txtItem.Text = _list.Tus_itm_cd;
                                    _serial2 = _list.Tus_ser_2;
                                    _prefix = _list.Tus_ser_3;
                                    LoadItemDetail(txtItem.Text.Trim());
                                    cmbStatus.SelectedValue = _combineStatus;
                                    decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_price).Sum();
                                    decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_qty).Sum();

                                    Qty = (Qty == 0) ? 1 : Qty;

                                    var _Increaseable = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(x => x.Sapc_increse).Distinct().ToList();
                                    bool _isIncreaseable = false;

                                    if (_Increaseable.Count == 0)
                                    {
                                        _isIncreaseable = false;
                                    }
                                    else
                                    {
                                        _isIncreaseable = Convert.ToBoolean(_Increaseable[0]);
                                    }
                                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                    txtUnitPrice.Text = UnitPrice.ToString("N2");
                                    if (_isIncreaseable)
                                        txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty)));
                                    else
                                        txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                    txtDisRate.Text = FormatToCurrency("0");
                                    txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0");
                                    txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    AddItem(_isPromotion, _originalItm);
                                }
                                _combineCounter += 1;
                            }
                            foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                            {
                                MasterItem _i = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _list.Sapc_itm_cd);
                                _combineStatus = _list.Status;
                                if (_i.Mi_is_ser1 != 1)
                                {
                                    string _originalItm = _list.Sapc_itm_cd; string _similerItem = _list.Similer_item;
                                    if (!string.IsNullOrEmpty(_similerItem)) txtItem.Text = _similerItem; else txtItem.Text = _list.Sapc_itm_cd;
                                    LoadItemDetail(txtItem.Text.Trim());
                                    cmbStatus.SelectedValue = _combineStatus;
                                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
                                    txtUnitPrice.Text = _list.Sapc_price.ToString("N2");
                                    if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty)));
                                    txtDisRate.Text = FormatToCurrency("0");
                                    txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0");
                                    txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion, _originalItm);
                                    _combineCounter += 1;
                                }
                            }
                        }

                        if (chkDeliverLater.Checked == true || chkDeliverNow.Checked == true)
                            if (_combineCounter == _MainPriceCombinItem.Count)
                            {
                                _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                                PriceCombinItemSerialList = new List<ReptPickSerials>();
                                _isCombineAdding = false;
                                SSPromotionCode = string.Empty;
                                ScanSerialNo = string.Empty;
                                _serial2 = string.Empty;
                                _prefix = string.Empty;
                                txtSerialNo.Text = "";
                                txtSerialNo.Text = string.Empty;
                                SSCombineLine += 1;
                                _combineCounter = 0;
                                _isCheckedPriceCombine = false;

                                if (_isCombineAdding == false)
                                {
                                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "confimationAddAnother()", true);
                                    //this.Cursor = Cursors.Default;
                                    //if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    //{
                                    //    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory))
                                    //    {
                                    //        txtSerialNo.Focus();
                                    //    }
                                    //    else
                                    //    {
                                    //        txtItem.Focus();
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    ucPayModes1.button1.Focus();
                                    //}
                                }
                                return;
                            }//hdnSerialNo.Value = ""
                        if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory))
                        {
                            if (_isSingleItemSerializedInCombine)
                            {
                                if (_combineCounter == PriceCombinItemSerialList.Count)
                                {
                                    _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                                    PriceCombinItemSerialList = new List<ReptPickSerials>();
                                    _isCombineAdding = false;
                                    SSPromotionCode = string.Empty;
                                    ScanSerialNo = string.Empty;
                                    _serial2 = string.Empty;
                                    _prefix = string.Empty;
                                    txtSerialNo.Text = "";
                                    txtSerialNo.Text = string.Empty;
                                    SSCombineLine += 1;
                                    _combineCounter = 0;
                                    _isCheckedPriceCombine = false;

                                    if (_isCombineAdding == false)
                                    {
                                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "confimationAddAnother()", true);
                                        //if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        //{
                                        //    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory))
                                        //    {
                                        //        txtSerialNo.Focus();
                                        //    }
                                        //    else { txtItem.Focus(); }
                                        //}
                                        //else
                                        //{
                                        //    //ucPayModes1.button1.Focus();
                                        //}
                                    } return;
                                }
                                else if (_combineCounter == _MainPriceCombinItem.Count)
                                {
                                    _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                                    PriceCombinItemSerialList = new List<ReptPickSerials>();
                                    _isCombineAdding = false;
                                    SSPromotionCode = string.Empty;
                                    ScanSerialNo = string.Empty;
                                    _serial2 = string.Empty;
                                    _prefix = string.Empty;
                                    txtSerialNo.Text = "";
                                    txtSerialNo.Text = string.Empty;
                                    SSCombineLine += 1;
                                    _combineCounter = 0;
                                    _isCheckedPriceCombine = false;

                                    if (_isCombineAdding == false)
                                    {
                                        // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "confimationAddAnother()", true);
                                        //if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        //{
                                        //    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                        //    {
                                        //        txtSerialNo.Focus();
                                        //    }
                                        //    else
                                        //    {
                                        //        txtItem.Focus();
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    //ucPayModes1.button1.Focus();
                                        //}
                                    } return;
                                }
                            }
                            else
                                if (_combineCounter == _MainPriceCombinItem.Count)
                                {
                                    _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                                    PriceCombinItemSerialList = new List<ReptPickSerials>();
                                    _isCombineAdding = false;
                                    SSPromotionCode = string.Empty;
                                    ScanSerialNo = string.Empty;
                                    _serial2 = string.Empty;
                                    _prefix = string.Empty;
                                    txtSerialNo.Text = "";
                                    txtSerialNo.Text = string.Empty;
                                    SSCombineLine += 1;
                                    _combineCounter = 0;
                                    _isCheckedPriceCombine = false;

                                    if (_isCombineAdding == false)
                                    {
                                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "confimationAddAnother()", true);
                                        //if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        //{
                                        //    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                        //    {
                                        //        txtSerialNo.Focus();
                                        //    }
                                        //    else
                                        //    {
                                        //        txtItem.Focus();
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    ucPayModes1.button1.Focus();
                                        //}
                                    }
                                    return;
                                }//hdnSerialNo.Value = ""
                        }
                    }
                }

                txtSerialNo.Text = "";
                txtresno.Text = "";
                lblSelectRevervation.Text = "";
                lblSelectRevLine.Text = "";

                ClearAfterAddItem();
                SSPriceBookSequance = "0";
                SSPriceBookItemSequance = "0";
                SSPriceBookPrice = 0;
                if (_isCombineAdding == false) SSPromotionCode = string.Empty;
                SSPRomotionType = 0;
                //_//txtItem.Focus();
                BindAddItem();
                SetDecimalTextBoxForZero(true);
                decimal _tobepays = 0;
                if (lblSVatStatus.Text == "Available")
                    _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                else
                    _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());

                ListItemCollection lstColl = cmbInvType.Items;
                foreach (ListItem item in lstColl)
                {

                }

                //mpAddConf.Show();
                if (Session["UserCompanyCode"].ToString() != "AAL")
                {
                    mpAddNewItem.Show();
                }

                //cmbStatus.SelectedIndex = -1;


                LookingForBuyBack();
                if (_isCombineAdding == false)
                {
                    //this.Cursor = Cursors.Default;
                    //if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                    //    {
                    //        txtSerialNo.Focus();
                    //    }
                    //    else
                    //    {
                    //        txtItem.Focus();
                    //    }
                    //}
                    //else
                    //{
                    //    ucPayModes1.button1.Focus();
                    //}
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(ex.Message, 4);
                return;
            }
        }


        private InvoiceItem AssignDataToObject(bool _isPromotion, MasterItem _item, string _originalItem)
        {
            InvoiceItem _tempItem = new InvoiceItem();
            IsVirtual(_item.Mi_itm_tp);
            _tempItem.Sad_alt_itm_cd = "";
            _tempItem.Sad_alt_itm_desc = "";
            _tempItem.Sad_comm_amt = 0;
            _tempItem.Sad_disc_amt = Convert.ToDecimal(txtDisAmt.Text);
            _tempItem.Sad_disc_rt = Convert.ToDecimal(txtDisRate.Text);
            _tempItem.Sad_do_qty = (IsGiftVoucher(_item.Mi_itm_tp) || _IsVirtualItem) ? Convert.ToDecimal(txtQty.Text) : 0;
            _tempItem.Sad_inv_no = "";
            _tempItem.Sad_is_promo = _isPromotion;
            _tempItem.Sad_itm_cd = txtItem.Text.ToUpper();
            _tempItem.Sad_itm_line = _lineNo;
            _tempItem.Sad_itm_seq = Convert.ToInt32(SSPriceBookItemSequance);
            _tempItem.Sad_itm_stus = cmbStatus.SelectedValue.ToString();
            _tempItem.Sad_itm_tax_amt = Convert.ToDecimal(txtTaxAmt.Text);
            _tempItem.Sad_itm_tp = _item.Mi_itm_tp;
            _tempItem.Sad_job_no = "";
            _tempItem.Sad_res_line_no = 0;
            _tempItem.Sad_res_no = "";
            if (_proVouInvcItem == txtItem.Text.ToUpper().ToString())
            {
                if (string.IsNullOrEmpty(lblPromoVouUsedFlag.Text))
                {
                    lblPromoVouUsedFlag.Text = "U";
                    _proVouInvcLine = _lineNo;
                    // _tempItem.Sad_res_line_no = Convert.ToInt32(lblPromoVouNo.Text.ToString());
                    _tempItem.Sad_res_no = "PROMO_VOU";
                }
            }
            _tempItem.Sad_merge_itm = "";
            _tempItem.Sad_pb_lvl = cmbLevel.Text;
            _tempItem.Sad_pb_price = Convert.ToDecimal(SSPriceBookPrice);
            _tempItem.Sad_pbook = cmbBook.Text;
            _tempItem.Sad_print_stus = false;
            _tempItem.Sad_promo_cd = SSPromotionCode;
            _tempItem.Sad_qty = Convert.ToDecimal(txtQty.Text);
            _tempItem.Sad_seq = Convert.ToInt32(SSPriceBookSequance);
            _tempItem.Sad_seq_no = 0;
            _tempItem.Sad_srn_qty = 0;
            _tempItem.Sad_tot_amt = Convert.ToDecimal(txtLineTotAmt.Text);
            _tempItem.Sad_unit_amt = Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text);
            _tempItem.Sad_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
            _tempItem.Sad_uom = "";
            _tempItem.Sad_warr_based = false;
            _tempItem.Mi_longdesc = _item.Mi_longdesc;
            _tempItem.Mi_itm_tp = _item.Mi_itm_tp;
            _tempItem.Mi_brand = _item.Mi_brand;
            _tempItem.Mi_cate_1 = _item.Mi_cate_1;
            _tempItem.Mi_cate_2 = _item.Mi_cate_2;
            _tempItem.Sad_job_line = Convert.ToInt32(SSCombineLine);
            _tempItem.Sad_warr_period = WarrantyPeriod;
            _tempItem.Sad_warr_remarks = WarrantyRemarks;
            _tempItem.Sad_sim_itm_cd = _originalItem;
            _tempItem.Sad_merge_itm = _item.Mi_itm_tp != "M" ? "0" : Convert.ToString(SSPRomotionType);
            if (!string.IsNullOrEmpty(txtDisRate.Text.Trim()) && IsNumeric(txtDisRate.Text.Trim()))
                if (Convert.ToDecimal(txtDisRate.Text.Trim()) > 0 && GeneralDiscount != null && (GeneralDiscount.Sgdd_disc_val != 00 || GeneralDiscount.Sgdd_disc_rt != 00))
                {
                    _tempItem.Sad_dis_type = "M";
                    _tempItem.Sad_dis_seq = GeneralDiscount.Sgdd_seq;
                    _tempItem.Sad_dis_line = 0;
                }

            if (lblSelectRevervation.Text.Trim() != "")
            {
                #region reservation
                if (!string.IsNullOrEmpty(txtresno.Text))
                {
                    List<INR_RES_DET> oINR_RES_DETs = CHNLSVC.Sales.GET_RESERVATION_DET(0, txtresno.Text);

                    DataTable dt = GlobalMethod.ToDataTable(oINR_RES_DETs);

                    if (oINR_RES_DETs != null && oINR_RES_DETs.Count > 0)
                    {
                        List<INR_RES_LOG> _resLogAvaData1 = new List<INR_RES_LOG>();
                        INR_RES_LOG _resObj = new INR_RES_LOG();
                        _resObj.IRL_RES_NO = txtresno.Text;
                        _resObj.IRL_CURT_COM = Session["UserCompanyCode"].ToString();
                        _resObj.IRL_CURT_LOC = txtlocation.Text;
                        _resObj.IRL_ACT = 1;
                        _resLogAvaData1 = CHNLSVC.Inventory.GET_INR_RES_LOG_DATA_NEW(_resObj);
                        if (_resLogAvaData1.Count == 0)
                        {
                            string _msg = "Please check your dispatch location";
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "')", true);
                            // _tempItem = new InvoiceItem();
                        }

                        INR_RES_DET oINR_RES_DET = oINR_RES_DETs.Find(x => x.IRD_ITM_CD == _tempItem.Sad_itm_cd && x.IRD_ITM_STUS == cmbStatus.SelectedValue.ToString());
                        if (oINR_RES_DET != null && oINR_RES_DET.IRD_RES_NO != null)
                        {
                            decimal UsedQty = 0;

                            List<InvoiceItem> oSaveDInvoiceItem = CHNLSVC.Sales.GET_INV_ITM_BY_RESNO_LINE(txtresno.Text, oINR_RES_DET.IRD_LINE);
                            if (oSaveDInvoiceItem != null && oSaveDInvoiceItem.Count > 0)
                            {
                                UsedQty = oSaveDInvoiceItem.Sum(x => x.Sad_qty);
                            }
                            if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                            {
                                UsedQty = UsedQty + _invoiceItemList.Where(x => x.Sad_res_no == oINR_RES_DET.IRD_RES_NO && x.Sad_res_line_no == oINR_RES_DET.IRD_LINE).Sum(x => x.Sad_qty);
                            }
                            if (oINR_RES_DET.IRD_RES_BQTY <= UsedQty || oINR_RES_DET.IRD_RES_BQTY < Convert.ToDecimal(txtQty.Text))
                            {
                                string _msg = "Cannot exceed reserved quantity-" + oINR_RES_DET.IRD_RES_BQTY;
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + _msg + "')", true);
                                // lblSelectRevervation.Text = "";
                                //txtresno.Text = "";
                                //mpReservations.Show();
                                // _tempItem = new InvoiceItem();

                            }
                            lblSelectRevervation.Text = txtresno.Text;
                            lblSelectRevLine.Text = oINR_RES_DET.IRD_LINE.ToString();
                            //DisplayMessageJS("Successfully reservation added.");
                            // mpReservations.Hide();
                            // return;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot find a reservation for selected item and status !!!')", true);
                            // lblSelectRevervation.Text = "";
                            // txtresno.Text = "";
                            // _tempItem = new InvoiceItem();

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Cannot find a reservation details !!!')", true);
                        // lblSelectRevervation.Text = "";
                        // txtresno.Text = "";
                        // _tempItem = new InvoiceItem();

                    }
                }
                #endregion

                List<INR_RES_LOG> _resLogAvaData = new List<INR_RES_LOG>();
                _resLogAvaData = CHNLSVC.Inventory.GET_INR_RES_LOG_DATA_NEW(
                new INR_RES_LOG()
                {
                    IRL_RES_NO = lblSelectRevervation.Text,
                    IRL_ITM_CD = _tempItem.Sad_itm_cd,
                    IRL_ITM_STUS = _tempItem.Sad_itm_stus,
                });

                if (_resLogAvaData.Count > 0)
                {
                    _tempItem.Sad_res_no = lblSelectRevervation.Text;
                    _tempItem.Sad_res_line_no = Convert.ToInt32(lblSelectRevLine.Text);
                    gvInvoiceItem.Columns[13].Visible = true;
                }
                //_tempItem.Sad_res_no = lblSelectRevervation.Text;
                //_tempItem.Sad_res_line_no = Convert.ToInt32(lblSelectRevLine.Text);
                //gvInvoiceItem.Columns[13].Visible = true;
            }
            return _tempItem;
        }

        private void ClearAfterAddItem()
        {
            txtItem.Text = "";
            LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
            // cmbStatus.SelectedItem.Text = DefaultItemStatus;
            txtQty.Text = FormatToQty("1");
            LoadItemDetail(string.Empty);
            txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            txtItem.ReadOnly = false;
        }

        protected void BindAddItem()
        {
            gvInvoiceItem.DataSource = new List<InvoiceItem>();
            gvInvoiceItem.DataSource = _invoiceItemList;
            gvInvoiceItem.DataBind();

            if (_invoiceItemList == null)
            {
                AddItemDisableCustomer(false);
            }
            if (_invoiceItemList.Count <= 0)
            {
                AddItemDisableCustomer(false);
            }

            if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
            {
                foreach (InvoiceItem item in _invoiceItemList)
                {
                    MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == item.Sad_itm_stus);
                    if (oStatus != null)
                    {
                        item.Sad_itm_stus_desc = oStatus.Mis_desc;
                    }
                    else
                    {
                        item.Sad_itm_stus_desc = item.Mi_itm_stus;
                    }
                }

                gvInvoiceItem.DataSource = new List<InvoiceItem>();
                gvInvoiceItem.DataSource = _invoiceItemList;
                gvInvoiceItem.DataBind();
            }
        }

        private void SetDecimalTextBoxForZero(bool _isUnit)
        {
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtQty.Text = FormatToQty("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            if (_isUnit)
                txtUnitPrice.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
        }

        private void LookingForBuyBack()
        {

        }

        private Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }

        private List<ReptPickSerials> VirtualSerialLine(string _item, string _status, decimal _qty, string _serialno)
        {
            List<ReptPickSerials> _ser = new List<ReptPickSerials>();
            if (!string.IsNullOrEmpty(_serialno))
            {
                ReptPickSerials _one = new ReptPickSerials();
                _one.Tus_com = Session["UserCompanyCode"].ToString();
                _one.Tus_itm_cd = _item;
                _one.Tus_itm_stus = _status;
                _one.Tus_loc = Session["UserDefLoca"].ToString();
                _one.Tus_qty = Convert.ToDecimal(_qty);
                _one.Tus_ser_1 = _serialno;
                _one.Tus_ser_2 = "N/A";
                _one.Tus_ser_3 = "N/A";
                _one.Tus_ser_4 = "N/A";
                _one.Tus_ser_id = VirtualCounter + 1;
                _one.Tus_ser_line = 1;
                _ser.Add(_one);
            }
            else
            {
                for (int i = 0; i < Convert.ToInt32(_qty); i++)
                {
                    ReptPickSerials _one = new ReptPickSerials();
                    _one.Tus_com = Session["UserCompanyCode"].ToString();
                    _one.Tus_itm_cd = _item;
                    _one.Tus_itm_stus = _status;
                    _one.Tus_loc = Session["UserDefLoca"].ToString();
                    _one.Tus_qty = 1;
                    _one.Tus_ser_1 = "N/A";
                    _one.Tus_ser_2 = "N/A";
                    _one.Tus_ser_3 = "N/A";
                    _one.Tus_ser_4 = "N/A";
                    _one.Tus_ser_id = VirtualCounter + 1;
                    _one.Tus_ser_line = 1;
                    _ser.Add(_one);
                }
            }
            return _ser;
        }

        private void DeleteIfPartlyAdded(int _joblineno, string _itemc, decimal _unitratec, string _bookc, string _levelc, decimal _qtyc, decimal _discountamt, decimal _taxamt, int _itmlineno, int _rowidx)
        {
            Int32 _combineLine = _joblineno;
            if (_MainPriceSerial != null)
                if (_MainPriceSerial.Count > 0)
                {
                    string _item = _itemc;
                    decimal _uRate = _unitratec;
                    string _pbook = _bookc;
                    string _level = _levelc;

                    List<PriceSerialRef> _tempSerial = _MainPriceSerial;
                    var _remove = from _list in _tempSerial
                                  where _list.Sars_itm_cd == _item && _list.Sars_itm_price == _uRate && _list.Sars_pbook == _pbook && _list.Sars_price_lvl == _level
                                  select _list;
                    foreach (PriceSerialRef _single in _remove)
                    {
                        _tempSerial.Remove(_single);
                    }

                    _MainPriceSerial = _tempSerial;
                }

            List<InvoiceItem> _tempList = _invoiceItemList;
            var _promo = (from _pro in _invoiceItemList
                          where _pro.Sad_job_line == _combineLine
                          select _pro).ToList();

            if (_promo.Count() > 0)
            {
                foreach (InvoiceItem code in _promo)
                {
                    CalculateGrandTotal(Convert.ToDecimal(code.Sad_qty), (decimal)code.Sad_unit_rt, (decimal)code.Sad_disc_amt, (decimal)code.Sad_itm_tax_amt, false);
                    ScanSerialList.RemoveAll(x => x.Tus_base_itm_line == code.Sad_itm_line);
                    InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == code.Sad_itm_line);
                }
                if (_tempList != null && _tempList.Count > 0)
                    _tempList.RemoveAll(x => x.Sad_job_line == _combineLine);
            }
            else
            {
                CalculateGrandTotal(_qtyc, _unitratec, _discountamt, _taxamt, false);
                InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == _itmlineno);
                if (_tempList != null && _tempList.Count > 0)
                    try
                    {
                        _tempList.RemoveAt(_rowidx);
                    }
                    catch
                    {
                    }
            }

            _invoiceItemList = _tempList;
            if (_invoiceItemList != null || _invoiceItemList.Count > 0) lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt))); else lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0"));

            Int32 _newLine = 1;
            List<InvoiceItem> _tempLists = _invoiceItemList;
            if (_tempLists != null)
                if (_tempLists.Count > 0)
                {
                    foreach (InvoiceItem _itm in _tempLists)
                    {
                        Int32 _line = _itm.Sad_itm_line;
                        _invoiceItemList.Where(Y => Y.Sad_itm_line == _line).ToList().ForEach(x => x.Sad_itm_line = _newLine);
                        InvoiceSerialList.Where(y => y.Sap_itm_line == _line).ToList().ForEach(x => x.Sap_itm_line = _newLine);
                        ScanSerialList.Where(y => y.Tus_base_itm_line == _line).ToList().ForEach(x => x.Tus_base_itm_line = _newLine);
                        _newLine += 1;
                    }
                    _lineNo = _newLine - 1;
                }
                else
                {
                    _lineNo = 0;
                }
            else
            {
                _lineNo = 0;
            }
            BindAddItem();
            gvPopSerial.DataSource = new List<ReptPickSerials>();
            gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList();


        }

        private bool CheckItemWarranty(string _item, string _status)
        {
            bool _isNoWarranty = false;
            List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
            if (_status == null) _status = "";
            if (_lvl != null)
                if (_lvl.Count > 0)
                {
                    var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == _status.Trim() select _l).ToList();
                    if (_lst != null)
                        if (_lst.Count > 0)
                        {
                            DataTable _temWarr = CHNLSVC.Sales.GetPCWara(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _item.Trim(), _status.Trim(), Convert.ToDateTime(txtdate.Text).Date);

                            if (_lst[0].Sapl_set_warr == true)
                            {
                                WarrantyPeriod = _lst[0].Sapl_warr_period;

                            }
                            else if (_temWarr != null && _temWarr.Rows.Count > 0)
                            {
                                WarrantyPeriod = Convert.ToInt32(_temWarr.Rows[0]["SPW_WARA_PD"].ToString());
                                WarrantyRemarks = _temWarr.Rows[0]["SPW_WARA_RMK"].ToString();
                            }
                            else
                            {
                                MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_item.Trim(), _status.Trim()); if (_period != null) { WarrantyPeriod = _period.Mwp_val; WarrantyRemarks = _period.Mwp_rmk; }

                            }
                        }
                }
            return _isNoWarranty;
        }

        private void VariableInitialize()
        {
            _invoiceItemList = new List<InvoiceItem>();
            _invoiceItemListWithDiscount = new List<InvoiceItem>();
            _recieptItem = new List<RecieptItem>();
            _newRecieptItem = new List<RecieptItem>();
            _businessEntity = new MasterBusinessEntity();
            _masterItemComponent = new List<MasterItemComponent>();
            _priceBookLevelRef = new PriceBookLevelRef();
            _priceBookLevelRefList = new List<PriceBookLevelRef>();
            _priceDetailRef = new List<PriceDetailRef>();
            _masterBusinessCompany = new MasterBusinessEntity();
            _MainPriceSerial = new List<PriceSerialRef>();
            _tempPriceSerial = new List<PriceSerialRef>();
            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            _isInventoryCombineAdded = false;
            ScanSequanceNo = 0;
            ScanSerialList = new List<ReptPickSerials>();
            IsPriceLevelAllowDoAnyStatus = false;
            WarrantyRemarks = string.Empty;
            WarrantyPeriod = 0;
            ScanSerialNo = string.Empty;
            DefaultItemStatus = string.Empty;
            InvoiceSerialList = new List<InvoiceSerial>();
            InventoryCombinItemSerialList = new List<ReptPickSerials>();
            PriceCombinItemSerialList = new List<ReptPickSerials>();
            BuyBackItemList = new List<ReptPickSerials>();
            _lineNo = 0;
            _isEditPrice = false;
            _isEditDiscount = false;
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            _toBePayNewAmount = 0;
            _isCompleteCode = false;
            SSPriceBookPrice = 0;
            SSPriceBookSequance = string.Empty;
            SSPriceBookItemSequance = string.Empty;
            SSIsLevelSerialized = string.Empty;
            SSPromotionCode = string.Empty;
            SSCirculerCode = string.Empty;
            SSPRomotionType = 0;
            SSCombineLine = 0;
            ManagerDiscount = new Dictionary<decimal, decimal>();
            GeneralDiscount = new CashGeneralEntiryDiscountDef();
            GeneralDiscount_new = new SarDocumentPriceDefn();
            DefaultBook = string.Empty;
            DefaultLevel = string.Empty;
            DefaultInvoiceType = string.Empty;
            DefaultStatus = string.Empty;
            DefaultBin = string.Empty;
            _itemdetail = new MasterItem();
            MainTaxConstant = new List<MasterItemTax>();
            _promotionSerial = new List<ReptPickSerials>();
            _promotionSerialTemp = new List<ReptPickSerials>();
            _isBackDate = false;
            _MasterProfitCenter = new MasterProfitCenter();
            _PriceDefinitionRef = new List<PriceDefinitionRef>();
            _isGiftVoucherCheckBoxClick = false;
            MasterChannel = new DataTable();
        }

        private bool IsInvoiceItemNSerialListTally(out string _Item)
        {
            bool _tally = true;
            string _errorItem = string.Empty;
            if (IsPriceLevelAllowDoAnyStatus)
            {
                var _itemswitouthstatus = (from _l in _invoiceItemList where !IsGiftVoucher(_l.Sad_itm_tp) && !IsVirtual(_l.Sad_itm_tp) group _l by new { _l.Sad_itm_cd } into _i select new { Sad_itm_cd = _i.Key.Sad_itm_cd, Sad_qty = _i.Sum(p => p.Sad_qty) }).ToList();
                Parallel.ForEach(_itemswitouthstatus, _itm =>
                {
                    string _item = _itm.Sad_itm_cd;
                    decimal _qty = _itm.Sad_qty;

                    decimal _scanItemQty = (from _n in ScanSerialList where _n.Tus_itm_cd == _item select _n.Tus_qty).Sum();
                    if (_qty != _scanItemQty)
                    {
                        if (string.IsNullOrEmpty(_errorItem))
                            _errorItem = _item;
                        else
                            _errorItem += ", " + _item;
                        _tally = false;
                    }
                });
            }
            else
            {
                var _itemswithstatus = (from _l in _invoiceItemList where !IsGiftVoucher(_l.Sad_itm_tp) && !IsVirtual(_l.Sad_itm_tp) group _l by new { _l.Sad_itm_cd, _l.Sad_itm_stus } into _i select new { Sad_itm_cd = _i.Key.Sad_itm_cd, Sad_itm_stus = _i.Key.Sad_itm_stus, Sad_qty = _i.Sum(p => p.Sad_qty) }).ToList();
                Parallel.ForEach(_itemswithstatus, _itm =>
                {
                    string _item = _itm.Sad_itm_cd;
                    string _status = _itm.Sad_itm_stus;
                    decimal _qty = _itm.Sad_qty;

                    decimal _scanItemQty = (from _n in ScanSerialList where _n.Tus_itm_cd == _item && _n.Tus_itm_stus == _status select _n.Tus_qty).Sum();
                    if (_qty != _scanItemQty)
                    {
                        if (string.IsNullOrEmpty(_errorItem))
                            _errorItem = _item;
                        else
                            _errorItem += ", " + _item;
                        _tally = false;
                    }
                });
            }
            _Item = _errorItem;
            return _tally;
        }

        private bool IsInventoryBalanceNInvoiceItemTally(out string _NotTallyList)
        {
            bool availability = true;
            MasterItem _itm = null;
            string _itemList = string.Empty;

            var _modifySerialList = (from _l in ScanSerialList group _l by new { _l.Tus_itm_cd, _l.Tus_itm_stus, _l.Tus_ser_1 } into _new select new { Tus_itm_cd = _new.Key.Tus_itm_cd, Tus_itm_stus = _new.Key.Tus_itm_stus, Tus_ser_1 = _new.Key.Tus_ser_1, Tus_qty = _new.Sum(p => p.Tus_qty) }).ToList();

            foreach (var _serial in _modifySerialList)
            {
                _itm = null;
                string _item = _serial.Tus_itm_cd;
                string _serialno = _serial.Tus_ser_1;
                string _status = _serial.Tus_itm_stus;
                decimal _qty = _serial.Tus_qty;
                _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                if (IsGiftVoucher(_itm.Mi_itm_tp)) continue;

                bool _isAgePriceLevel = false;
                int _noofDays = 0;
                DateTime _serialpickingdate = Convert.ToDateTime(txtdate.Text).Date;
                CheckNValidateAgeItem(_item, _itm.Mi_cate_1, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), _status, out _isAgePriceLevel, out _noofDays);
                if (_isAgePriceLevel) _serialpickingdate.AddDays(-_noofDays);
                if (_itm.Mi_is_ser1 == 1)
                {
                    ReptPickSerials _chk = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _item, _serialno);
                    if (string.IsNullOrEmpty(_chk.Tus_com)) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                    else if (IsPriceLevelAllowDoAnyStatus == false)
                        if (_chk.Tus_itm_stus != _status) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                }
                else if (_itm.Mi_is_ser1 == 0)
                {
                    List<ReptPickSerials> _chk;
                    if (IsPriceLevelAllowDoAnyStatus == false)
                        _chk = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _status, _qty, _serialpickingdate.Date);
                    else
                        _chk = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty, _qty, _serialpickingdate.Date);
                    if (_chk != null)
                        if (_chk.Count > 0)
                        {
                            if (IsPriceLevelAllowDoAnyStatus == false)
                            {
                                decimal _statuswiseqty = (from i in _chk where i.Tus_itm_cd == _item && i.Tus_itm_stus == _status select i.Tus_qty).Sum();
                                if (_statuswiseqty < _qty) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                            }
                            else
                                if (_chk.Count() < _qty) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                        }
                        else { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                    else { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                }
                else if (_itm.Mi_is_ser1 == -1)
                {
                    List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty);

                    if (_inventoryLocation != null)
                        if (_inventoryLocation.Count > 0)
                        {
                            if (IsPriceLevelAllowDoAnyStatus == false)
                            {
                                decimal _statuswiseqty = (from i in _inventoryLocation where i.Inl_itm_cd == _item && i.Inl_itm_stus == _status select i.Inl_free_qty).Sum();
                                if (_statuswiseqty < _qty) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                            }
                            else
                            {
                                decimal _withoustusqty = (from i in _inventoryLocation where i.Inl_itm_cd == _item select i.Inl_free_qty).Sum();
                                if (_withoustusqty < _qty) { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                            }
                        }
                        else { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                    else { availability = false; if (string.IsNullOrEmpty(_itemList)) _itemList = _item; else _itemList += "," + _item; }
                }
            }
            _NotTallyList = _itemList;
            return availability;
        }

        private void CollectBusinessEntity()
        {
            _businessEntity = new MasterBusinessEntity();
            _businessEntity.Mbe_act = true;
            _businessEntity.Mbe_add1 = txtAddress1.Text.ToUpper();
            _businessEntity.Mbe_add2 = txtAddress2.Text.ToUpper();
            _businessEntity.Mbe_cd = "c1";
            _businessEntity.Mbe_com = Session["UserCompanyCode"].ToString();
            _businessEntity.Mbe_contact = string.Empty;
            _businessEntity.Mbe_email = string.Empty;
            _businessEntity.Mbe_fax = string.Empty;
            _businessEntity.Mbe_is_tax = false;
            _businessEntity.Mbe_mob = txtMobile.Text;
            _businessEntity.Mbe_name = txtCusName.Text.ToUpper();
            _businessEntity.Mbe_nic = txtNIC.Text.ToUpper();
            _businessEntity.Mbe_tax_no = string.Empty;
            _businessEntity.Mbe_tel = string.Empty;
            _businessEntity.Mbe_tp = "C";
            _businessEntity.Mbe_pc_stus = "GOOD";
            _businessEntity.Mbe_ho_stus = "GOOD";
            _businessEntity.MBE_TIT = cmbTitle.Text;
            _businessEntity.Mbe_cate = "INDIVIDUAL";
            _businessEntity.Mbe_town_cd = txtPerTown.Text.ToUpper();// Nadeeka 
        }

        public static string FormatDiscoutnItem(int Indent, string Value)
        {
            return new string('\t', Indent) + Value;
        }

        private bool CheckItemWarrantyNew(string _item, string _status, Int32 _pbSeq, Int32 _itmSeq, string _pb, string _pbLvl, Boolean _isPbWara, decimal _unitPrice, Int32 _pbWarrPd)
        {
            DateTime txtDate = Convert.ToDateTime(txtdate.Text);
            bool _isNoWarranty = false;
            MasterItemWarrantyPeriod _period = new MasterItemWarrantyPeriod();
            LogMasterItemWarranty _periodLog = new LogMasterItemWarranty();
            //List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
            //if (_lvl != null)
            //    if (_lvl.Count > 0)
            //    {
            //        var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == _status.Trim() select _l).ToList();
            //        if (_lst != null)
            //if (_lst.Count > 0)
            //{
            DataTable _temWarr = CHNLSVC.Sales.GetPCWara(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _item.Trim(), _status.Trim(), txtDate.Date);

            if (_isPbWara == true && _unitPrice > 0)
            {
                WarrantyPeriod = _pbWarrPd;
                PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(_item, _itmSeq, _pbSeq);
                if (_lsts != null)
                {
                    WarrantyRemarks = _lsts.Sapd_warr_remarks;
                }

            }
            else if (_temWarr != null && _temWarr.Rows.Count > 0)
            {
                WarrantyPeriod = Convert.ToInt32(_temWarr.Rows[0]["SPW_WARA_PD"].ToString());
                WarrantyRemarks = _temWarr.Rows[0]["SPW_WARA_RMK"].ToString();
            }
            else if (txtDate.Date != _serverDt)
            {
                _period = new MasterItemWarrantyPeriod();
                _period = CHNLSVC.Sales.GetItemWarrEffDt(_item, _status, 1, txtDate.Date);
                if (_period.Mwp_itm_cd != null)
                {
                    WarrantyPeriod = _period.Mwp_val;
                    WarrantyRemarks = _period.Mwp_rmk;
                }
                else
                {
                    _periodLog = new LogMasterItemWarranty();
                    _periodLog = CHNLSVC.Sales.GetItemWarrEffDtLog(_item.Trim(), _status.Trim(), 1, txtDate.Date); if (_periodLog.Lmwp_itm_cd != null) { WarrantyPeriod = _periodLog.Lmwp_val; WarrantyRemarks = _periodLog.Lmwp_rmk; }
                    else { _isNoWarranty = true; }
                }
            }
            else
            {
                _period = new MasterItemWarrantyPeriod();
                _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_item.Trim(), _status.Trim()); if (_period.Mwp_itm_cd != null) { WarrantyPeriod = _period.Mwp_val; WarrantyRemarks = _period.Mwp_rmk; }
                else { _isNoWarranty = true; }
            }
            return _isNoWarranty;
        }

        private VehicalRegistration AssingRegDetails(string _itm, decimal _reg, decimal _claim, string _engine, string _chassis)
        {
            VehicalRegistration _tempReg = new VehicalRegistration();
            MasterItem _itemList = new MasterItem();
            _itemList = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Trim());

            _tempReg.P_seq = 1;
            _tempReg.P_srvt_ref_no = "na";
            _tempReg.P_svrt_com = Session["UserCompanyCode"].ToString();
            _tempReg.P_svrt_pc = Session["UserDefProf"].ToString();
            _tempReg.P_svrt_dt = Convert.ToDateTime(txtdate.Text).Date;
            //_tempReg.P_svrt_inv_no = txtInvoice.Text.Trim();
            _tempReg.P_svrt_sales_tp = cmbInvType.SelectedValue.ToString();
            _tempReg.P_svrt_reg_val = _reg;
            _tempReg.P_svrt_claim_val = _claim;
            _tempReg.P_svrt_id_tp = "NIC";
            _tempReg.P_svrt_id_ref = txtNIC.Text.ToUpper().Trim();
            _tempReg.P_svrt_cust_cd = txtCustomer.Text.ToUpper().Trim();
            _tempReg.P_svrt_cust_title = "Mr.";
            _tempReg.P_svrt_last_name = txtCusName.Text.ToUpper().Trim();
            _tempReg.P_svrt_full_name = txtCusName.Text.ToUpper().Trim();
            _tempReg.P_svrt_initial = "";
            _tempReg.P_svrt_add01 = txtAddress1.Text.ToUpper();
            _tempReg.P_svrt_add02 = txtAddress2.Text.ToUpper().Trim();
            _tempReg.P_svrt_city = "";
            _tempReg.P_svrt_district = _masterBusinessCompany.Mbe_distric_cd;
            _tempReg.P_svrt_province = _masterBusinessCompany.Mbe_province_cd;
            _tempReg.P_svrt_contact = txtMobile.Text.Trim();
            _tempReg.P_svrt_model = _itemList.Mi_model;
            _tempReg.P_svrt_brd = _itemList.Mi_brand;
            _tempReg.P_svrt_chassis = _chassis;
            _tempReg.P_svrt_engine = _engine;
            _tempReg.P_svrt_color = _itemList.Mi_color_ext;
            _tempReg.P_svrt_fuel = "";
            _tempReg.P_svrt_capacity = 0;
            _tempReg.P_svrt_unit = "";
            _tempReg.P_svrt_horse_power = 0;
            _tempReg.P_svrt_wheel_base = 0;
            _tempReg.P_svrt_tire_front = "";
            _tempReg.P_svrt_tire_rear = "";
            _tempReg.P_svrt_weight = 0;
            _tempReg.P_svrt_man_year = 0;
            _tempReg.P_svrt_import = "";
            _tempReg.P_svrt_authority = "";
            _tempReg.P_svrt_country = _itemList.Mi_country_cd;
            _tempReg.P_svrt_custom_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_clear_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_declear_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_importer = "";
            _tempReg.P_svrt_importer_add01 = "";
            _tempReg.P_svrt_importer_add02 = "";
            _tempReg.P_svrt_cre_bt = Session["UserID"].ToString();
            _tempReg.P_svrt_cre_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_prnt_stus = 0;
            _tempReg.P_svrt_prnt_by = "";
            _tempReg.P_svrt_prnt_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_srvt_rmv_stus = 0;
            _tempReg.P_srvt_rmv_by = "";
            _tempReg.P_srvt_rmv_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_veh_reg_no = "";
            _tempReg.P_svrt_reg_by = "";
            _tempReg.P_svrt_reg_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_svrt_image = "";
            _tempReg.P_srvt_cust_stus = 0;
            _tempReg.P_srvt_cust_by = "";
            _tempReg.P_srvt_cust_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_srvt_cls_stus = 0;
            _tempReg.P_srvt_cls_by = "";
            _tempReg.P_srvt_cls_dt = Convert.ToDateTime("31/Dec/2999");
            _tempReg.P_srvt_insu_ref = "";
            _tempReg.P_srvt_itm_cd = _itm;
            return _tempReg;
        }

        private bool CheckGeneralDiscount()
        {
            DateTime txtDate = Convert.ToDateTime(txtdate.Text);
            string _cusCode = txtCustomer.Text.ToUpper().Trim();
            foreach (InvoiceItem _invItm in _invoiceItemList)
            {
                if (_invItm.Sad_dis_type == "M" && _invItm.Sad_disc_rt > 0)
                {
                    //get discount line
                    //validate data
                    CashGeneralEntiryDiscountDef _def = CHNLSVC.Sales.GetGeneralDiscountDefinitionBySequence(_invItm.Sad_dis_seq);
                    if (_def == null)
                    {
                        //using (new CenterWinDialog(this)) { MessageBox.Show("Item - " + _invItm.Sad_itm_cd + "General Discount not foundPlease contact IT Dept.", "General Discount", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        return true;
                    }
                    else
                    {
                        //Add by Chamal 22-Dec-2014
                        if (_def.Sgdd_stus == true && _def.Sgdd_from_dt.Date <= txtDate.Date && _def.Sgdd_to_dt.Date >= txtDate.Date)
                        {
                            if (txtCustomer.Text.Trim() != _def.Sgdd_cust_cd && !string.IsNullOrEmpty(_def.Sgdd_cust_cd))
                            {
                                DisplayMessage("Item - " + _invItm.Sad_itm_cd + "General Discount definition customer and invoice customer mismatchDefinition Customer - " + _def.Sgdd_cust_cd + "Please contact IT Dept.");
                                return false;
                            }
                            if (!string.IsNullOrEmpty(_def.Sgdd_itm) && (_invItm.Sad_itm_cd != _def.Sgdd_itm))
                            {
                                DisplayMessage("Item - " + _invItm.Sad_itm_cd + "General Discount definition item and invoice item mismatchDefinition item - " + _def.Sgdd_itm + "Please contact IT Dept.");
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        protected void chkQuotation_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQuotation.Checked)
            {
                txtQuotation.ReadOnly = false;
            }
            else
            {
                txtQuotation.ReadOnly = true;
                Clear();
            }
        }

        private void BindPriceCombineItem(Int32 _pbseq, Int32 _pblineseq, Int32 _priceType, string _mainItem, string _mainSerial, string FQty, string TQty)
        {
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
            //PriceTypeRef _list = TakePromotion(_priceType);
            //if (_list.Sarpt_is_com)
            if (_priceBookLevelRef.Sapl_is_serialized)
            {
                _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItem(_pbseq, _mainItem, _mainSerial);
                //PromItm_Serial.Visible = true;
            }
            else
            {
                _tempPriceCombinItem = CHNLSVC.Sales.GetPriceCombinedItemLine(_pbseq, _pblineseq, _mainItem, string.Empty);
                //PromItm_Serial.Visible = false;
            }
            if (_tempPriceCombinItem != null && _tempPriceCombinItem.Count > 0)
            {
                int balnce = Convert.ToInt32(txtQty.Text.Trim()) / Convert.ToInt32(FQty);
                if (balnce == 0) { balnce = 1; }
                _tempPriceCombinItem.ForEach(x => x.Mi_cre_by = Convert.ToString(x.Mi_std_price));
                _tempPriceCombinItem.Where(x => x.Sapc_increse).ToList().ForEach(x => x.Sapc_qty = x.Sapc_qty * balnce);
                //  _tempPriceCombinItem.ForEach(x => x.Sapc_price = x.Sapc_price * CheckSubItemTax(x.Sapc_itm_cd));
                _tempPriceCombinItem.Where(x => !string.IsNullOrEmpty(x.Sapc_sub_ser)).ToList().ForEach(x => x.Sapc_increse = true);

                //int noqty = Convert.ToInt32(TQty) - Convert.ToInt32(FQty);

                //_tempPriceCombinItem.Where(x => x.Sapc_increse==false).ToList().ForEach(x => x.Sapc_qty = x.Sapc_qty * Convert.ToDecimal(balnce));

                gvPromotionItem.DataSource = _tempPriceCombinItem;
                gvPromotionItem.DataBind();
                HangGridComboBoxStatus();
            }
            else
            {
                gvPromotionItem.DataSource = _tempPriceCombinItem;
                gvPromotionItem.DataBind();
                HangGridComboBoxStatus();

                gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                gvPromotionSerial.DataBind();
            }
        }

        private void HangGridComboBoxStatus()
        {
            if (_levelStatus == null || _levelStatus.Rows.Count <= 0)
                return;
            var _types = _levelStatus.AsEnumerable().Select(x => x.Field<string>("Code")).Distinct().ToList();
            _types.Add("");


            for (int i = 0; i < gvPromotionItem.Rows.Count; i++)
            {
                DropDownList PromItm_Status = (DropDownList)gvPromotionItem.Rows[i].FindControl("PromItm_Status");
                //PromItm_Status.DataSource = _types;
                //PromItm_Status.DataTextField = "";
                //PromItm_Status.DataBind();

                PromItm_Status.SelectedValue = cmbStatus.SelectedValue;
            }

            //PromItm_Status.DataSource = _types;
            //foreach (DataGridViewRow r in gvPromotionItem.Rows)
            //{
            //    r.Cells["PromItm_Status"].Value = cmbStatus.Text;
            //}
        }

        private void LoadGiftVoucherBalance(string _item, Label _withStatus, Label _withoutStatus, out List<ReptPickSerials> GiftVoucher)
        {
            List<ReptPickSerials> _gifVoucher = CHNLSVC.Inventory.GetAvailableGiftVoucher(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _item);
            if (_gifVoucher == null || _gifVoucher.Count <= 0)
            {
                DisplayMessage("There is no gift vouchers available");
                _withStatus.Text = string.Empty;
                _withoutStatus.Text = string.Empty;
                GiftVoucher = _gifVoucher;
                return;
            }
            int _count = _gifVoucher.AsEnumerable().Count();
            _withStatus.Text = FormatToQty(Convert.ToString(_count));
            _withoutStatus.Text = FormatToQty(Convert.ToString(_count));
            var _list = _gifVoucher.AsEnumerable().Where(x => x.Tus_itm_cd == _item).ToList();
            GiftVoucher = _list;
        }

        private void LoadSelectedItemSerialForPriceComItemSerialGv(string _item, string _status, decimal _qty, bool _isPromotion, int _isStatusCol)
        {
            DateTime txtDate = Convert.ToDateTime(txtdate.Text);
            List<ReptPickSerials> _lst = null;
            _promotionSerial = new List<ReptPickSerials>();
            _promotionSerialTemp = new List<ReptPickSerials>();
            MasterItem _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            if (_itm.Mi_is_ser1 == 1)
            {
                if (IsPriceLevelAllowDoAnyStatus)
                    _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _item.Trim().ToUpper(), string.Empty, _qty);
                else
                    _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _item.Trim().ToUpper(), _status, _qty);

                if (IsPriceLevelAllowDoAnyStatus == false && (_lst == null || _lst.Count <= 0))
                {
                    if (cmbStatus.SelectedValue == "CONS")
                    {
                        _status = "CONS";
                        _lst = CHNLSVC.Inventory.GetNonSerializedItemInTopOrder(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _item.Trim().ToUpper(), _status, _qty);
                    }
                }
                foreach (ReptPickSerials _ser in ScanSerialList.Where(x => x.Tus_itm_cd == _item.Trim()))
                    _lst.RemoveAll(x => x.Tus_ser_1 == _ser.Tus_ser_1);

                _lst.RemoveAll(x => x.Tus_ser_1 == txtSerialNo.Text);

                #region Age Price level - serial pick

                bool _isAgePriceLevel = false;
                int _noOfDays = 0;
                CheckNValidateAgeItem(_item.Trim(), _itm.Mi_cate_1, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), _status, out _isAgePriceLevel, out _noOfDays);
                List<ReptPickSerials> _newlist = GetAgeItemList(txtDate.Date, _isAgePriceLevel, _noOfDays, _lst);

                #endregion Age Price level - serial pick

                gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                var _list = new BindingList<ReptPickSerials>(_newlist);
                if (_isPromotion)
                {
                    gvPromotionSerial.DataSource = _list;
                    gvPromotionSerial.DataBind();

                    markSelectedItems();
                }
                else
                {
                    // gvPopComItemSerial.DataSource = _list;
                }
                _promotionSerial = _lst;
            }
            else
            {
                if (_isStatusCol == 7) return;
                DisplayMessageJS("No need to pick non serialized item");
                return;
            }
        }

        public struct RegistrationList
        {
            private string item;

            public string Item
            {
                get { return item; }
                set { item = value; }
            }

            private string stus;

            public string Stus
            {
                get { return stus; }
                set { stus = value; }
            }

            private decimal qty;

            public decimal Qty
            {
                get { return qty; }
                set { qty = value; }
            }

            private decimal total_value;

            public decimal Total_value
            {
                get { return total_value; }
                set { total_value = value; }
            }

            private decimal item_reg;

            public decimal Item_reg
            {
                get { return item_reg; }
                set { item_reg = value; }
            }

            private decimal item_claim;

            public decimal Item_claim
            {
                get { return item_claim; }
                set { item_claim = value; }
            }

            private decimal registrationAmt;

            public decimal RegistrationAmt
            {
                get { return registrationAmt; }
                set { registrationAmt = value; }
            }

            private decimal claimAmt;

            public decimal ClaimAmt
            {
                get { return claimAmt; }
                set { claimAmt = value; }
            }
        }

        #region Buy Back
        protected void txtBBItem_TextChanged(object sender, EventArgs e)
        {
            mpBuyBack.Show();

            txtBBSerial1.Text = "";
            txtBBSerial2.Text = "";
            txtBBWarranty.Text = "";
            if (string.IsNullOrEmpty(txtBBItem.Text.Trim())) return;
            try
            {
                if (!LoadBuyBackItemDetail(txtBBItem.Text.Trim()))
                {
                    DisplayMessageJS("Please check the buy back item code");
                    DisplayMessage("Please check the buy back item code");
                    txtBBItem.Text = "";
                    txtBBItem.Focus();
                    txtBBSerial1.ReadOnly = false;
                    txtBBSerial2.ReadOnly = false;
                    txtBBQty.ReadOnly = true;
                    txtBBQty.Text = "";

                    lblBBDescription.Text = string.Empty;
                    lblBBModel.Text = string.Empty;
                    lblBBBrand.Text = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtBBQty_TextChanged(object sender, EventArgs e)
        {
            mpBuyBack.Show();
        }

        protected void txtBBSerial1_TextChanged(object sender, EventArgs e)
        {
            mpBuyBack.Show();

        }

        protected void txtBBSerial2_TextChanged(object sender, EventArgs e)
        {
            mpBuyBack.Show();

        }

        protected void txtBBWarranty_TextChanged(object sender, EventArgs e)
        {
            mpBuyBack.Show();

        }

        private bool LoadBuyBackItemDetail(string _item)
        {
            lblBBDescription.Text = string.Empty;
            lblBBModel.Text = string.Empty;
            lblBBBrand.Text = string.Empty;
            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item))
                _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            if (_itemdetail != null)
                if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    _isValid = true;
                    string _description = _itemdetail.Mi_longdesc;
                    string _model = _itemdetail.Mi_model;
                    string _brand = _itemdetail.Mi_brand;
                    string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "Non";

                    lblBBDescription.Text = _description;
                    lblBBModel.Text = _model;
                    lblBBBrand.Text = _brand;
                    if (_itemdetail.Mi_is_ser1 == 0)
                    {
                        txtBBSerial1.ReadOnly = true;
                        txtBBSerial2.ReadOnly = true;
                        txtBBQty.ReadOnly = false;
                    }
                    else
                    {
                        txtBBQty.Text = "1";
                        txtBBSerial1.ReadOnly = false;
                        txtBBSerial2.ReadOnly = false;
                        txtBBQty.ReadOnly = true;
                    }
                }
            if (!_item.Contains("BUY BACK"))
                _isValid = false;

            return _isValid;
        }

        protected void btnBuyBackClose_Click(object sender, EventArgs e)
        {
            mpBuyBack.Hide();
        }

        protected void btnBBAddItem_Click(object sender, EventArgs e)
        {
            mpBuyBack.Show();
            if (string.IsNullOrEmpty(txtBBItem.Text))
            {
                DisplayMessageJS("Please select the buy back item.");
                txtBBItem.Text = "";
                txtBBItem.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtBBQty.Text))
            {
                DisplayMessageJS("Please select the quantity");
                txtBBQty.Focus();
                return;
            }
            try
            {
                var _bbQty = _invoiceItemList.Where(x => x.Sad_merge_itm == "3").Sum(x => x.Sad_qty);
                if (_bbQty == 0)
                {
                    DisplayMessageJS("There is no buy back promotion.");
                    return;
                }

                List<ReptPickSerials> oBBNewItems = new List<ReptPickSerials>();
                if (Session["oBBNewItems"] != null)
                {
                    oBBNewItems = (List<ReptPickSerials>)Session["oBBNewItems"];
                }

                decimal _pickedBBitem = oBBNewItems.Sum(x => x.Tus_qty);

                if (_bbQty < _pickedBBitem + Convert.ToDecimal(txtBBQty.Text))
                {
                    DisplayMessageJS("cannot exceed the buy-back promotion quantity with returning quantity.");
                    return;
                }

                MasterItem _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtBBItem.Text.Trim());

                if (oBBNewItems.FindAll(x => x.Tus_itm_cd == txtBBItem.Text.Trim() && x.Tus_ser_1 == txtBBSerial1.Text.Trim() && x.Tus_ser_2 == txtBBSerial2.Text.Trim() && (x.Tus_ser_1 != "N/A" || x.Tus_ser_1 != "NA") && (x.Tus_ser_2 != "N/A" || x.Tus_ser_2 != "NA")).Count > 0)
                {
                    DisplayMessageJS("Selected item/serial already added!");
                    return;
                }

                LoadBuyBackItemDetail(txtBBItem.Text.Trim());

                txtBBSerial1.Text = txtBBSerial1.Text.Replace("'", "").ToString();
                txtBBSerial2.Text = txtBBSerial2.Text.Replace("'", "").ToString();
                txtBBWarranty.Text = txtBBWarranty.Text.Replace("'", "").ToString();

                ReptPickSerials oNewItem = new ReptPickSerials();
                oNewItem.Tus_itm_cd = txtBBItem.Text.Trim();
                oNewItem.Tus_itm_desc = _itemdetail.Mi_longdesc;
                oNewItem.Tus_itm_model = _itemdetail.Mi_model;
                oNewItem.Tus_itm_stus = "BB";

                MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == oNewItem.Tus_itm_stus);
                oNewItem.Tus_itm_stus_Desc = oStatus.Mis_desc;

                oNewItem.Tus_qty = Convert.ToDecimal(txtBBQty.Text.Trim());
                oNewItem.Tus_ser_1 = string.IsNullOrEmpty(txtBBSerial1.Text.Trim()) ? "N/A" : txtBBSerial1.Text.Trim();
                oNewItem.Tus_ser_2 = string.IsNullOrEmpty(txtBBSerial2.Text.Trim()) ? "N/A" : txtBBSerial2.Text.Trim();
                oNewItem.Tus_warr_no = string.IsNullOrEmpty(txtBBWarranty.Text.Trim()) ? "N/A" : txtBBWarranty.Text.Trim();
                oBBNewItems.Add(oNewItem);

                gvAddBuyBack.DataSource = oBBNewItems;
                gvAddBuyBack.DataBind();

                Session["oBBNewItems"] = oBBNewItems;

                txtBBItem.Text = "";
                txtBBQty.Text = "1";
                txtBBSerial1.Text = "";
                txtBBSerial2.Text = "";
                txtBBWarranty.Text = "";
                LoadBuyBackItemDetail(string.Empty);
                txtBBItem.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void btnBBConfirm_Click(object sender, EventArgs e)
        {
            mpBuyBack.Show();

            if (gvAddBuyBack.Rows.Count <= 0)
            {
                DisplayMessageJS("Please select the buy back item");
                return;
            }
            try
            {

                List<ReptPickSerials> oBBNewItems = (List<ReptPickSerials>)Session["oBBNewItems"];

                if (BuyBackItemList == null)
                {
                    BuyBackItemList = new List<ReptPickSerials>();
                }

                var _bbQty = _invoiceItemList.Where(x => x.Sad_merge_itm == "3" && x.Sad_unit_rt != 0).Sum(x => x.Sad_qty);

                var _pickedBBitem = oBBNewItems.Sum(x => x.Tus_qty);
                var _alreadyPick = BuyBackItemList.Sum(x => x.Tus_qty);

                if (_bbQty != _pickedBBitem + _alreadyPick)
                {
                    DisplayMessageJS("Please check the buy-back return item quantity with promotion quantity. Promotion quantity : " + _bbQty.ToString() + " and return quantity " + (_pickedBBitem + _alreadyPick).ToString());
                    return;
                }

                BuyBackItemList.AddRange(oBBNewItems);

                foreach (ReptPickSerials item in BuyBackItemList)
                {
                    item.Tus_com = Session["UserCompanyCode"].ToString();
                    item.Tus_loc = Session["UserDefLoca"].ToString();
                    item.Tus_cre_by = Session["UserID"].ToString();
                    item.Tus_cre_dt = DateTime.Now.Date;
                    item.Tus_session_id = Session["SessionID"].ToString();
                    item.Tus_ser_id = CHNLSVC.Inventory.GetSerialID();
                }

                var _bind = new BindingList<ReptPickSerials>(BuyBackItemList);

                txtBBItem.Text = "";
                txtBBQty.Text = "1";
                txtBBSerial1.Text = "";
                txtBBSerial2.Text = "";
                txtBBWarranty.Text = "";
                gvAddBuyBack.DataSource = new List<ReptPickSerials>();
                gvAddBuyBack.DataBind();

                pnlBuyBack.Visible = false;

                mpBuyBack.Hide();

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void btnBBItemSearch_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.BuyBackItem);
            DataTable result = CHNLSVC.CommonSearch.SearchBuyBackItem(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "BuyBackItem";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            SIPopup.Show(); Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
            txtBBItem.Focus();
        }

        protected void btnBBNewItemDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblTus_itm_cd = (Label)row.FindControl("lblTus_itm_cd");
            Label lblTus_itm_model = (Label)row.FindControl("lblTus_itm_model");
            Label lblTus_itm_stus = (Label)row.FindControl("lblTus_itm_stus");
            Label lblTus_ser_1 = (Label)row.FindControl("lblTus_ser_1");

            List<ReptPickSerials> oBBNewItems = (List<ReptPickSerials>)Session["oBBNewItems"];
            if (oBBNewItems != null && oBBNewItems.Count > 0)
            {
                oBBNewItems.RemoveAll(x => x.Tus_itm_cd == lblTus_itm_cd.Text.Trim() && x.Tus_itm_model == lblTus_itm_model.Text.Trim() && x.Tus_itm_stus == lblTus_itm_stus.Text && x.Tus_ser_1 == lblTus_ser_1.Text.Trim());
                Session["oBBNewItems"] = oBBNewItems;

                gvAddBuyBack.DataSource = oBBNewItems;
                gvAddBuyBack.DataBind();
                txtBBItem.Focus();
            }

            mpBuyBack.Show();
        }


        #endregion

        #region Confirmation
        protected void btnConfClose_Click(object sender, EventArgs e)
        {
            lblConfText.Text = "";
            mpConfirmation.Hide();
        }

        protected void btnConfYes_Click(object sender, EventArgs e)
        {
            mpConfirmation.Hide();
            hdfConf.Value = "1";
            hdfConfItem.Value = txtItem.Text.ToUpper().Trim();
            hdfConfStatus.Value = cmbStatus.SelectedValue.ToString();
            txtItem_TextChanged(null, null);
        }

        protected void btnConfNo_Click(object sender, EventArgs e)
        {
            hdfConf.Value = "0";
            hdfConfItem.Value = txtItem.Text.ToUpper().Trim();
            hdfConfStatus.Value = cmbStatus.SelectedValue.ToString();
            txtItem_TextChanged(null, null);
        }
        #endregion

        protected void btnTest_Click(object sender, EventArgs e)
        {
        }

        protected void btnPromotionPriceSelect_Click(object sender, EventArgs e)
        {
            Int32 _row = 0;
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            bool _isValidate = false;
            bool _IsSerializedPriceLevel = _priceBookLevelRef.Sapl_is_serialized;

            if (_IsSerializedPriceLevel)
            {

                //    //DataGridViewCheckBoxCell _chk = gvPromotionPrice.Rows[_row].Cells["PromPrice_Select"] as DataGridViewCheckBoxCell;
                //    //bool _isSelected = false;
                //    //if (Convert.ToBoolean(_chk.Value))
                //    //    _isSelected = true;
                //    //UncheckNormalPriceOrPromotionPrice(true, false);


                //    TextBox lblSapd_itm_cd = (TextBox)row.FindControl("lblSapd_itm_cd");
                //    TextBox lblPbSeq = (TextBox)row.FindControl("lblPbSeq");
                //    TextBox lblSapd_price_type = (TextBox)row.FindControl("lblSapd_price_type");
                //    TextBox txtIcet_actl_rt = (TextBox)row.FindControl("txticet_actl_rt");


                //    string _mainItem = gvPromotionPrice.Rows[_row].Cells["PromPrice_Item"].Value.ToString();
                //    string _mainSerial = gvPromotionPrice.Rows[_row].Cells["PromPrice_Serial"].Value.ToString();
                //    string _pbseq = gvPromotionPrice.Rows[_row].Cells["PromPrice_Pb_Seq"].Value.ToString();
                //    string _priceType = gvPromotionPrice.Rows[_row].Cells["PromPrice_PriceType"].Value.ToString();
                //    BindPriceCombineItem(Convert.ToInt32(_pbseq), 1, Convert.ToInt32(_priceType), _mainItem, _mainSerial);
                //    if (_isValidate)
                //    {
                //        if (_isSelected) _chk.Value = false; else _chk.Value = true;
                //        decimal _count = (from DataGridViewRow row in gvNormalPrice.Rows
                //                          where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
                //                          select row).Count();
                //        if (_count > Convert.ToDecimal(txtQty.Text.Trim()))
                //        {
                //            _chk.Value = false;
                //            this.Cursor = Cursors.Default;
                //            using (new CenterWinDialog(this))
                //            {
                //                MessageBox.Show("Qty and the selected serials are mismatch.", "Serial and Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //            }
                //            return;
                //        }
                //    }
                //    if (_isSelected) _chk.Value = false; else _chk.Value = true;
            }
            else
            {
                GridViewRow row1 = (sender as LinkButton).NamingContainer as GridViewRow;

                CheckBox chkSelectPromPrice = (CheckBox)row1.FindControl("chkSelectPromPrice");
                bool _isSelected = false;

                if (Convert.ToBoolean(chkSelectPromPrice.Checked))
                {
                    _isSelected = true;
                }
                else
                {
                    _isSelected = false;
                }

                //UncheckNormalPriceOrPromotionPrice(false, true);
                //BindingSource _source = new BindingSource();
                //_source.DataSource = new List<PriceCombinedItemRef>();
                gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                if (_isSelected)
                {
                    Label lblSapd_itm_cd = (Label)row.FindControl("lblSapd_itm_cd");
                    Label lblPbSeq = (Label)row.FindControl("lblPbSeq");
                    Label lblSapd_price_type = (Label)row.FindControl("lblSapd_price_type");
                    Label lblsapd_seq_no = (Label)row.FindControl("lblsapd_seq_no");
                    Label lblSapd_promo_cd = (Label)row.FindControl("lblSapd_promo_cd");
                    Session["Promotion"] = lblSapd_promo_cd.Text.Trim();

                    string _mainItem = lblSapd_itm_cd.Text.Trim();
                    string _pbseq = lblPbSeq.Text.Trim();
                    string _priceType = lblSapd_price_type.Text.Trim();
                    string _pblineseq = lblsapd_seq_no.Text.Trim();
                    BindPriceCombineItem(Convert.ToInt32(_pbseq), Convert.ToInt32(_pblineseq), Convert.ToInt32(_priceType), _mainItem, string.Empty
                        , null, null);
                    chkSelectPromPrice.Checked = true;
                }
                else
                {
                    chkSelectPromPrice.Checked = false;
                    gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                    gvPromotionItem.DataBind();
                }
            }
        }

        protected void gvPromotionPrice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = gvPromotionPrice.Rows[Convert.ToInt32(e.CommandArgument)];


        }

        protected void chkSelectPromPrice_CheckedChanged(object sender, EventArgs e)
        {
            mpPriceNPromotion.Show();

            //Int32 _row = 0;
            //GridViewRow row = (sender as CheckBox).NamingContainer as GridViewRow;
            //bool _isValidate = false;
            //bool _IsSerializedPriceLevel = _priceBookLevelRef.Sapl_is_serialized;

            //if (_IsSerializedPriceLevel)
            //{

            //    CheckBox chkSelectPromPrice = (CheckBox)gvPromotionPrice.SelectedRow.FindControl("chkSelectPromPrice");
            //    //DataGridViewCheckBoxCell _chk = gvPromotionPrice.Rows[_row].Cells["PromPrice_Select"] as DataGridViewCheckBoxCell;

            //    bool _isSelected = false;

            //    if (Convert.ToBoolean(chkSelectPromPrice.Checked))
            //        _isSelected = true;

            //    //UncheckNormalPriceOrPromotionPrice(true, false);

            //    TextBox lblSapd_itm_cd = (TextBox)row.FindControl("lblSapd_itm_cd");
            //    TextBox lblPbSeq = (TextBox)row.FindControl("lblPbSeq");
            //    TextBox lblSapd_price_type = (TextBox)row.FindControl("lblSapd_price_type");
            //    TextBox txtIcet_actl_rt = (TextBox)row.FindControl("txticet_actl_rt");


            //    //string _mainItem = gvPromotionPrice.Rows[_row].Cells["PromPrice_Item"].Value.ToString();
            //    //string _mainSerial = gvPromotionPrice.Rows[_row].Cells["PromPrice_Serial"].Value.ToString();
            //    //string _pbseq = gvPromotionPrice.Rows[_row].Cells["PromPrice_Pb_Seq"].Value.ToString();
            //    //string _priceType = gvPromotionPrice.Rows[_row].Cells["PromPrice_PriceType"].Value.ToString();
            //    //BindPriceCombineItem(Convert.ToInt32(_pbseq), 1, Convert.ToInt32(_priceType), _mainItem, _mainSerial);
            //    //if (_isValidate)
            //    //{
            //    //    if (_isSelected)
            //    //    {
            //    //        _chk.Value = false;
            //    //    }
            //    //    else
            //    //    {
            //    //        _chk.Value = true;
            //    //    }

            //    //    decimal _count = (from DataGridViewRow row in gvNormalPrice.Rows
            //    //                      where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
            //    //                      select row).Count();
            //    //    if (_count > Convert.ToDecimal(txtQty.Text.Trim()))
            //    //    {
            //    //        _chk.Value = false;
            //    //        this.Cursor = Cursors.Default;
            //    //        using (new CenterWinDialog(this))
            //    //        {
            //    //            MessageBox.Show("Qty and the selected serials are mismatch.", "Serial and Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    //        }
            //    //        return;
            //    //    }
            //    //}
            //    //if (_isSelected)
            //    //    _chk.Value = false;
            //    //else
            //    //    _chk.Value = true;
            //}
            //else
            //{
            //    GridViewRow row1 = (sender as CheckBox).NamingContainer as GridViewRow;
            //    // GridViewRow parentRow = chkSelectPromPrice.NamingContainer as GridViewRow;

            //    // Label lblRate = parentRow.FindControl("Label3") as Label;
            //    //if (lblRate != null)
            //    //{
            //    //    //Write logic here.	  
            //    //}
            //    if (gvPromotionPrice.SelectedRow != null)
            //    {

            //        CheckBox chkSelectPromPrice = (CheckBox)gvPromotionPrice.SelectedRow.FindControl("chkSelectPromPrice");

            //        bool _isSelected = false;

            //        if (Convert.ToBoolean(chkSelectPromPrice.Checked))
            //            _isSelected = true;
            //        //UncheckNormalPriceOrPromotionPrice(false, true);

            //        //BindingSource _source = new BindingSource();
            //        //_source.DataSource = new List<PriceCombinedItemRef>();
            //        gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
            //        if (_isSelected)
            //        {
            //            chkSelectPromPrice.Checked = false;
            //        }
            //        else
            //        {
            //            if (row.DataItem == null)
            //            {
            //                return;
            //            }
            //            TextBox lblSapd_itm_cd = (TextBox)row.FindControl("lblSapd_itm_cd");
            //            TextBox lblPbSeq = (TextBox)row.FindControl("lblPbSeq");
            //            TextBox lblSapd_price_type = (TextBox)row.FindControl("lblSapd_price_type");
            //            TextBox lblsapd_seq_no = (TextBox)row.FindControl("lblsapd_seq_no");

            //            string _mainItem = lblSapd_itm_cd.Text.Trim();
            //            string _pbseq = lblPbSeq.Text.Trim();
            //            string _priceType = lblSapd_price_type.Text.Trim();
            //            string _pblineseq = lblsapd_seq_no.Text.Trim();
            //            BindPriceCombineItem(Convert.ToInt32(_pbseq), Convert.ToInt32(_pblineseq), Convert.ToInt32(_priceType), _mainItem, string.Empty);
            //            chkSelectPromPrice.Checked = true;
            //        }
            //    }
            //}
        }

        protected void btnPromotionItemSimiler_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvPromotionItem.Rows.Count > 0)
                {
                    GridViewRow row1 = (sender as LinkButton).NamingContainer as GridViewRow;
                    int _col = 0;
                    Int32 _row = row1.RowIndex;

                    if (_row != -1)
                    {
                        LinkButton btnPromotionItemSimiler = (LinkButton)row1.FindControl("btnPromotionItemSimiler");

                        Label lblsapc_itm_cd = (Label)row1.FindControl("lblsapc_itm_cd");
                        string _originalItem = lblsapc_itm_cd.Text.Trim();
                        string _item = lblsapc_itm_cd.Text.Trim();
                        Label lblSimiler_item = (Label)row1.FindControl("lblSimiler_item");
                        string _similerItem = lblSimiler_item.Text.Trim();

                        DropDownList PromItm_Status = (DropDownList)row1.FindControl("PromItm_Status");
                        string _status = PromItm_Status.SelectedValue.ToString(); //cmbStatus.Text.Trim();

                        Label lblsapc_qty = (Label)row1.FindControl("lblsapc_qty");
                        string _qty = lblsapc_qty.Text.Trim();

                        Label lblsapc_sub_ser = (Label)row1.FindControl("lblsapc_sub_ser");
                        string _serial = lblsapc_sub_ser.Text.Trim();

                        Label lblsapc_increse = (Label)row1.FindControl("lblsapc_increse");
                        bool _haveSerial = Convert.ToBoolean(lblsapc_increse.Text.ToString());

                        string _PromotionCD = Session["Promotion"].ToString();

                        List<ReptPickSerials> _giftVoucher = new List<ReptPickSerials>();

                        if (!string.IsNullOrEmpty(_similerItem))
                            _item = _similerItem;
                        bool _isGiftVoucher = IsGiftVoucher(CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item).Mi_itm_tp);

                        if (!_isGiftVoucher)
                        {
                            DisplayAvailableQty(_item, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, _status);
                        }
                        else
                        {
                            LoadGiftVoucherBalance(_item, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, out _giftVoucher);
                        }

                        if (btnPromotionItemSimiler.ID != "btnPromotionItemSimiler")
                        {
                            if (_isGiftVoucher)
                            {
                                DisplayMessageJS("Develop 10672");

                                //List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                                //_promotionSerial = new List<ReptPickSerials>();
                                //_promotionSerialTemp = new List<ReptPickSerials>();
                                //if (_giftVoucher != null)
                                //    if (_giftVoucher.Count > 0)
                                //        _lst.AddRange(_giftVoucher);
                                //_promotionSerial = _lst;
                                //gvPopComItemSerial.DataSource = new List<ReptPickSerials>();
                                //gvPopComItemSerial.DataSource = _lst;
                                //txtPriNProSerialSearch.Text = ".";
                                //txtPriNProSerialSearch.Text = string.Empty;
                            }
                            else if (_priceBookLevelRef.Sapl_is_serialized)
                            {
                                DisplayMessageJS("Develop 10687");
                                //if (_haveSerial == false && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                //    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, _col);
                                //else if (_haveSerial == true && !string.IsNullOrEmpty(_similerItem) && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                //    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, _col);
                                //else if (_haveSerial == true && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                //{
                                //    List<InventorySerialRefN> _ref = CHNLSVC.Inventory.GetItemDetailBySerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _serial);
                                //    if (_ref != null)
                                //        if (_ref.Count > 0)
                                //        {
                                //            var _available = _ref.Where(x => x.Ins_itm_cd == _item).ToList();
                                //            if (_available == null || _available.Count <= 0)
                                //            {
                                //                this.Cursor = Cursors.Default;
                                //                using (new CenterWinDialog(this)) { MessageBox.Show("Selected item does not available in the current inventory", "No Stock", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                //                return;
                                //            }
                                //        }
                                //}
                            }
                            else if (chkDeliverNow.Checked == false)
                            {
                                LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, _col);
                                DisplayMessageJS("Develop 10709");
                            }
                            else
                            {
                                var _list = new BindingList<ReptPickSerials>(new List<ReptPickSerials>());
                                gvPromotionSerial.DataSource = _list;
                                gvPromotionSerial.DataBind();
                                markSelectedItems();
                            }
                        }

                        #region Similar Item Call

                        if (!_isGiftVoucher)
                            //if (gvPromotionItem.Columns[e.ColumnIndex].Name == "PromItm_SelectSimilerItem" && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                            if (chkDeliverNow.Checked == false)
                            {
                                DataTable _dtTable = CHNLSVC.Inventory.GetItemInventoryBalanceStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty);
                                if (_dtTable != null)
                                    if (_dtTable.Rows.Count > 0)
                                    {
                                        DisplayMessageJS("Stock balance is available for the promotion item. No need to pick similar item here!.");
                                        return;
                                    }

                                //DisplayMessageJS("DEVELOP 10735");

                                hdfOriginalItem.Value = _item;
                                getSilimerItems("S", _item, _PromotionCD);
                                mpPriceNPromotion.Hide();
                                mpSimilrItmes.Show();
                                return;
                                //TextBox _box = new TextBox();
                                //CommonSearch.SearchSimilarItems _similarItems = new CommonSearch.SearchSimilarItems();
                                //_similarItems.DocumentType = "S";
                                //_similarItems.ItemCode = _item;
                                //_similarItems.FunctionDate = txtDate.Value.Date;
                                //_similarItems.DocumentNo = string.Empty;
                                //_similarItems.PromotionCode = _PromotionCD;
                                //_similarItems.obj_TragetTextBox = _box;
                                //_similarItems.ShowDialog();
                                //if (!string.IsNullOrEmpty(_box.Text))
                                //{
                                //    _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == _originalItem).ToList().ForEach(x => x.Similer_item = _box.Text);
                                //    _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == _originalItem).ToList().ForEach(x => x.Sapc_increse = false);
                                //    _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == _originalItem).ToList().ForEach(x => x.Sapc_sub_ser = string.Empty);
                                //    BindingSource _source = new BindingSource();
                                //    _source.DataSource = _tempPriceCombinItem;
                                //    gvPromotionItem.DataSource = _source;
                                //    _box.Clear();
                                //}
                            }
                            else if (chkDeliverNow.Checked == false)
                            {
                                DisplayMessageJS("You cannot pick similar item unless you have deliver now!");
                                return;
                            }

                        #endregion Similar Item Call
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void gvPromotionItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (_levelStatus == null || _levelStatus.Rows.Count <= 0)
                    return;
                var _types = _levelStatus.AsEnumerable().Select(x => x.Field<string>("Code")).Distinct().ToList();
                _types.Add("");

                DropDownList ddl = (DropDownList)e.Row.FindControl("PromItm_Status");
                //foreach (string colName in _types)
                //    ddl.Items.Add(new ListItem(colName));

                for (int i = 0; i < _levelStatus.Rows.Count; i++)
                {
                    ddl.Items.Add(new ListItem(_levelStatus.Rows[i][1].ToString(), _levelStatus.Rows[i]["Code"].ToString()));
                }
            }
        }

        protected void btnPromotionItemSelect_click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                promotionsCustomMethod(dr);

            }
            catch (Exception ex)
            {
                DisplayMessageJS(ex.Message);
            }
        }

        protected void btnPromConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Session["mpPriceNPromotion"] = null;
                _isCombineAdding = false;
                if (_tempPriceCombinItem != null && _tempPriceCombinItem.Count > 0)
                {
                    for (int i = 0; i < gvPromotionItem.Rows.Count; i++)
                    {
                        GridViewRow row = gvPromotionItem.Rows[i];
                        Label lblsapc_itm_cd = (Label)row.FindControl("lblsapc_itm_cd");
                        DropDownList PromItm_Status = (DropDownList)row.FindControl("PromItm_Status");

                        var _filter = _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == lblsapc_itm_cd.Text.Trim()).ToList();
                        foreach (PriceCombinedItemRef _ref in _filter)
                        {
                            _ref.Status = PromItm_Status.SelectedValue.ToString().Trim();
                        }
                        // _tempPriceCombinItem.Find(x => x.Sapc_itm_cd == lblsapc_itm_cd.Text.Trim()).Status = PromItm_Status.SelectedValue.ToString().Trim();

                    }
                }

                if (_priceBookLevelRef.Sapl_is_serialized)
                {
                    int _normalCount = 0;

                    int _promoCount = 0;
                    for (int i = 0; i < gvPromotionPrice.Rows.Count; i++)
                    {
                        GridViewRow row = gvPromotionPrice.Rows[i];
                        CheckBox chkSelectPromPrice = (CheckBox)row.FindControl("chkSelectPromPrice");
                        if (chkSelectPromPrice.Checked)
                        {
                            _promoCount = _promoCount + 1;
                        }
                    }

                    int _totalPickedSerial = _normalCount + _promoCount;

                    if (_totalPickedSerial == 0)
                    {
                        DisplayMessageJS("Please select the price from normal or promotion");
                        return;
                    }
                    if (_totalPickedSerial > 1)
                    {
                        DisplayMessageJS("You have selected more than one selection.");
                        return;
                    }
                    if (_normalCount > 0)
                    {
                        DisplayMessageJS("Develop 10906");
                        //var _normalRow = from DataGridViewRow row in gvNormalPrice.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true select row;
                        //if (_normalRow != null)
                        //{
                        //    foreach (var _row in _normalRow)
                        //    {
                        //        string _unitPrice = _row.Cells["NorPrice_UnitPrice"].Value.ToString();
                        //        string _bkpPrice = _row.Cells["NorPrice_BkpUPrice"].Value.ToString();
                        //        string _pbseq = _row.Cells["NorPrice_Pb_Seq"].Value.ToString();
                        //        string _pblineseq = string.Empty;
                        //        if (string.IsNullOrEmpty(Convert.ToString(_row.Cells["NorPrice_PbLineSeq"].Value))) _pblineseq = "1";
                        //        else _pblineseq = _row.Cells["NorPrice_PbLineSeq"].Value.ToString();
                        //        string _warrantyrmk = _row.Cells["NorPrice_WarrantyRmk"].Value.ToString();
                        //        if (!string.IsNullOrEmpty(_unitPrice))
                        //        {
                        //            txtUnitPrice.Text = FormatToCurrency(_unitPrice);
                        //            SSPriceBookPrice = Convert.ToDecimal(_bkpPrice);
                        //            SSPriceBookSequance = _pbseq;
                        //            SSPriceBookItemSequance = _pblineseq;
                        //            WarrantyRemarks = _warrantyrmk;
                        //            CalculateItem();
                        //            pnlMain.Enabled = true;
                        //            pnlPriceNPromotion.Visible = false;
                        //        }
                        //    }
                        //}
                        return;
                    }
                    if (_promoCount > 0)
                    {
                        for (int i = 0; i < gvPromotionPrice.Rows.Count; i++)
                        {
                            GridViewRow row = gvPromotionPrice.Rows[i];
                            CheckBox chkSelectPromPrice = (CheckBox)row.FindControl("chkSelectPromPrice");
                            if (chkSelectPromPrice.Checked)
                            {
                                Label lblSapd_itm_cd = (Label)row.FindControl("lblSapd_itm_cd");
                                Label lblPbSeq = (Label)row.FindControl("lblPbSeq");
                                string _pbLineSeq = "0";
                                Label lblPbLineSeq = (Label)row.FindControl("lblPbLineSeq");
                                if (lblPbLineSeq.Text != "")
                                {
                                    _pbLineSeq = Convert.ToString(lblPbLineSeq.Text.Trim());
                                }
                                Label lblSapd_warr_remarks = (Label)row.FindControl("lblSapd_warr_remarks");
                                Label lblSapd_itm_price = (Label)row.FindControl("lblSapd_itm_price");
                                Label lblSapd_promo_cd = (Label)row.FindControl("lblSapd_promo_cd");
                                Label lblSapd_circular_no = (Label)row.FindControl("lblSapd_circular_no");
                                Label lblSapd_price_type = (Label)row.FindControl("lblSapd_price_type");
                                Label lblBkpUPrice = (Label)row.FindControl("lblBkpUPrice");

                                string _mainItem = lblSapd_itm_cd.Text.Trim();
                                string _pbSeq = lblPbSeq.Text.Trim();
                                //Rukshan 27/Apr/2016 restic promotion add same so
                                for (int j = 0; j < gvInvoiceItem.Rows.Count; j++)
                                {
                                    GridViewRow rowrestic = gvInvoiceItem.Rows[j];
                                    Label _promo_cdrestic = (Label)rowrestic.FindControl("InvItm_PromoCd");
                                    if (lblSapd_promo_cd.Text == _promo_cdrestic.Text)
                                    {
                                        DisplayMessage("sd", 2);
                                        return;
                                    }
                                }



                                //string _pbLineSeq = "0";
                                //if (Convert.ToString(_row.Cells["PromPrice_PbLineSeq"].Value) == string.Empty)
                                //    _pbLineSeq = "0";
                                //else
                                //    _pbLineSeq = Convert.ToString(_row.Cells["PromPrice_PbLineSeq"].Value);
                                string _pbWarranty = lblSapd_warr_remarks.Text.Trim();
                                string _unitprice = lblSapd_itm_price.Text.Trim();
                                string _promotioncode = lblSapd_promo_cd.Text.Trim();
                                string _circulerncode = lblSapd_circular_no.Text.Trim();
                                string _promotiontype = lblSapd_price_type.Text.Trim();
                                string _pbPrice = lblBkpUPrice.Text.Trim();
                                bool _isSingleItemSerialized = false;

                                PriceDetailRestriction _restriction = CHNLSVC.Sales.GetPromotionRestriction(Session["UserCompanyCode"].ToString(), _promotioncode);

                                if (_restriction != null)
                                {
                                    //show message
                                    if (!string.IsNullOrEmpty(_restriction.Spr_msg))
                                    {
                                        DisplayMessageJS(_restriction.Spr_msg);

                                        bool nic = false;
                                        bool mob = false;
                                        bool cus = false;

                                        if (_restriction.Spr_need_cus && (string.IsNullOrEmpty(txtCustomer.Text) || txtCustomer.Text.ToUpper() == "CASH"))
                                        {
                                            cus = true;
                                        }
                                        if (_restriction.Spr_need_mob && string.IsNullOrEmpty(txtMobile.Text))
                                        {
                                            mob = true;
                                        }
                                        if (_restriction.Spr_need_nic && string.IsNullOrEmpty(txtNIC.Text))
                                        {
                                            nic = true;
                                        }

                                        string _message = "";
                                        if (cus)
                                        {
                                            _message = _message + "This promotion need Customer code, Please enter customer code";
                                        }
                                        if (nic)
                                        {
                                            _message = _message + "This promotion need ID Number, Please enter ID Number";
                                        }
                                        if (mob)
                                        {
                                            _message = _message + "This promotion need Mobile Number, Please enter  Mobile Number";
                                        }
                                        if (cus || nic || mob)
                                        {
                                            DisplayMessageJS(_message);
                                            return;
                                        }
                                    }
                                }

                                foreach (PriceCombinedItemRef _ref in _tempPriceCombinItem)
                                {
                                    string _item = _ref.Sapc_itm_cd;
                                    string _originalItem = _ref.Sapc_itm_cd;
                                    string _similerItem = Convert.ToString(_ref.Similer_item);
                                    if (!string.IsNullOrEmpty(_similerItem)) _item = _similerItem;
                                    string _status = _ref.Status; //cmbStatus.Text.Trim();
                                    string _qty = Convert.ToString(_ref.Sapc_qty);
                                    bool _haveSerial = Convert.ToBoolean(_ref.Sapc_increse);
                                    string _serialno = Convert.ToString(_ref.Sapc_sub_ser);

                                    MasterItem _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                                    if (_itm.Mi_is_ser1 == 1) _isSingleItemSerialized = true;
                                    if (_haveSerial && _itm.Mi_is_ser1 == 1)
                                    {
                                        if (!string.IsNullOrEmpty(_serialno) && chkDeliverNow.Checked == false)
                                        {
                                            List<InventorySerialRefN> _refs = CHNLSVC.Inventory.GetItemDetailBySerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _serialno);
                                            if (_ref != null)
                                                if (_refs.Count > 0)
                                                {
                                                    var _available = _refs.Where(x => x.Ins_itm_cd == _item).ToList();
                                                    if (_available == null || _available.Count <= 0)
                                                    {
                                                        DisplayMessageJS(_item + " item, " + _serialno + " serial  does not available in the current inventory stock.");
                                                        return;
                                                    }
                                                }
                                        }
                                        else if (string.IsNullOrEmpty(_serialno) && chkDeliverNow.Checked == false)
                                        {
                                            decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                            if (_serialcount != Convert.ToDecimal(_qty))
                                            {
                                                DisplayMessageJS("Quantity and the selected serials mismatch in " + _item);
                                                return;
                                            }
                                        }
                                        else if (_itm.Mi_is_ser1 == 1 && chkDeliverNow.Checked == false)
                                        {
                                            ReptPickSerials _one = new ReptPickSerials();
                                            if (!string.IsNullOrEmpty(_serialno)) PriceCombinItemSerialList.Add(VirtualSerialLine(_item, _status, Convert.ToDecimal(_qty), _serialno)[0]);
                                        }
                                    }
                                    else if (_haveSerial == false && _itm.Mi_is_ser1 == 1 && chkDeliverNow.Checked == false)
                                    {
                                        decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                        if (_serialcount != Convert.ToDecimal(_qty))
                                        {
                                            DisplayMessageJS("Quantity and the selected serials mismatch in " + _item);
                                            return;
                                        }
                                    }
                                    else if (_haveSerial == false && (_itm.Mi_is_ser1 == 0 || _itm.Mi_is_ser1 == -1) && chkDeliverNow.Checked == false)
                                    {
                                        decimal _pickQty = 0;
                                        if (IsPriceLevelAllowDoAnyStatus) _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item).ToList().Select(x => x.Sad_qty).Sum(); else _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item && x.Mi_itm_stus == _status).ToList().Select(x => x.Sad_qty).Sum();
                                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                        List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _status);
                                        if (_inventoryLocation != null)
                                            if (_inventoryLocation.Count > 0)
                                            {
                                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                                if (_pickQty > _invBal)
                                                {
                                                    DisplayMessageJS(_item + " item quantity exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)));
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                DisplayMessageJS(_item + " item quantity exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")));
                                                return;
                                            }
                                        else
                                        {
                                            DisplayMessageJS(_item + " item quantity exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0")));
                                            return;
                                        }
                                    }
                                    else if (_itm.Mi_is_ser1 == 1 && (chkDeliverNow.Checked))
                                    {
                                        ReptPickSerials _one = new ReptPickSerials();
                                        if (!string.IsNullOrEmpty(_serialno))
                                        {
                                            _one.Tus_com = Session["UserCompanyCode"].ToString();
                                            _one.Tus_itm_cd = _item;
                                            _one.Tus_itm_stus = _status;
                                            _one.Tus_loc = Session["UserDefLoca"].ToString();
                                            _one.Tus_qty = Convert.ToDecimal(_qty);
                                            _one.Tus_ser_1 = _serialno;
                                            _one.Tus_ser_2 = "N/A";
                                            _one.Tus_ser_3 = "N/A";
                                            _one.Tus_ser_4 = "N/A";
                                            _one.Tus_ser_id = -100;
                                            _one.Tus_ser_line = 1;
                                            PriceCombinItemSerialList.Add(_one);
                                        }
                                    }
                                }

                                if (_isSingleItemSerialized && chkDeliverNow.Checked == false)
                                    if (PriceCombinItemSerialList == null)
                                    {
                                        DisplayMessageJS("You have to select the serial for the promotion items");
                                        return;
                                    }
                                if (_isSingleItemSerialized && chkDeliverNow.Checked == false)
                                    if (PriceCombinItemSerialList.Count <= 0)
                                    {
                                        DisplayMessageJS("You have to select the serial for the promotion items");
                                        return;
                                    }
                                SetSSPriceDetailVariable(_circulerncode, _pbLineSeq, _pbSeq, _pbPrice, _promotioncode, _promotiontype);
                                _MainPriceCombinItem = _tempPriceCombinItem;
                                decimal pvalue = Convert.ToDecimal(_unitprice);
                                txtUnitPrice.Text = FormatToCurrency(_unitprice);
                                txtUnitPrice.Text = pvalue.ToString("N2");
                                txtTaxAmt.Text = SetDecimalPoint(FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false))));
                                CalculateItem();
                                mpPriceNPromotion.Hide();
                                lbtnadditems.Focus();

                            }
                        }
                        return;
                    }
                }
                else
                {
                    bool _isSelect = false;

                    if (gvPromotionPrice.Rows.Count > 0)
                    {
                        GridViewRow drSelected = gvPromotionPrice.Rows[0];

                        for (int i = 0; i < gvPromotionPrice.Rows.Count; i++)
                        {
                            GridViewRow drRow = gvPromotionPrice.Rows[i];
                            CheckBox chkSelectPromPrice = (CheckBox)drRow.FindControl("chkSelectPromPrice");
                            if (chkSelectPromPrice.Checked)
                            {
                                _isSelect = true;
                                drSelected = drRow;
                                break;
                            }

                        }

                        if (!_isSelect)
                        {
                            DisplayMessageJS("You have to select a promotion.");
                            return;
                        }
                        if (_tempPriceCombinItem == null)
                        {
                            DisplayMessageJS("You have to select a promotion items.");
                            return;
                        }
                        //if (_tempPriceCombinItem.Count <= 0 && _isHavingSubItem)
                        //{ this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("You have to select a promotion items.", "No Promotion item", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                        if (_isSelect)
                        {
                            Label lblSapd_itm_cd = (Label)drSelected.FindControl("lblSapd_itm_cd");
                            Label lblPbSeq = (Label)drSelected.FindControl("lblPbSeq");
                            Label lblPbLineSeq = (Label)drSelected.FindControl("lblPbLineSeq");
                            Label lblSapd_warr_remarks = (Label)drSelected.FindControl("lblSapd_warr_remarks");
                            Label lblSapd_itm_price = (Label)drSelected.FindControl("lblSapd_itm_price");
                            Label lblSapd_promo_cd = (Label)drSelected.FindControl("lblSapd_promo_cd");
                            Label lblSapd_circular_no = (Label)drSelected.FindControl("lblSapd_circular_no");
                            Label lblSapd_price_type = (Label)drSelected.FindControl("lblSapd_price_type");
                            Label lblBkpUPrice = (Label)drSelected.FindControl("lblBkpUPrice");
                            //Rukshan 27/Apr/2016 restic promotion add same so
                            for (int j = 0; j < gvInvoiceItem.Rows.Count; j++)
                            {
                                GridViewRow row = gvInvoiceItem.Rows[j];
                                Label _promo_cdrestic = (Label)row.FindControl("InvItm_PromoCd");
                                if (lblSapd_promo_cd.Text == _promo_cdrestic.Text)
                                {
                                    DisplayMessage("You have been already added this promotion to the list", 1);
                                    return;
                                }
                            }
                            string _mainItem = lblSapd_itm_cd.Text.Trim();
                            string _pbSeq = lblPbSeq.Text.Trim();
                            string _pbLineSeq = lblPbLineSeq.Text.Trim();
                            string _pbWarranty = lblSapd_warr_remarks.Text.Trim();
                            string _unitprice = Convert.ToString(FigureRoundUp(Convert.ToDecimal(lblSapd_itm_price.Text.Trim()), true));
                            string _promotioncode = lblSapd_promo_cd.Text.Trim();
                            string _circulerncode = lblSapd_circular_no.Text.Trim();
                            string _promotiontype = lblSapd_price_type.Text.Trim();
                            string _pbPrice = lblBkpUPrice.Text.Trim();
                            bool _isSingleItemSerialized = false;

                            PriceDetailRestriction _restriction = CHNLSVC.Sales.GetPromotionRestriction(Session["UserCompanyCode"].ToString(), _promotioncode);

                            if (_restriction != null)
                            {
                                //show message
                                if (!string.IsNullOrEmpty(_restriction.Spr_msg))
                                {
                                    DisplayMessageJS(_restriction.Spr_msg);

                                    bool nic = false;
                                    bool mob = false;
                                    bool cus = false;

                                    if (_restriction.Spr_need_cus && (string.IsNullOrEmpty(txtCustomer.Text) || txtCustomer.Text.ToUpper() == "CASH"))
                                    {
                                        cus = true;
                                    }
                                    if (_restriction.Spr_need_mob && string.IsNullOrEmpty(txtMobile.Text))
                                    {
                                        mob = true;
                                    }
                                    if (_restriction.Spr_need_nic && string.IsNullOrEmpty(txtNIC.Text))
                                    {
                                        nic = true;
                                    }

                                    string _message = "";
                                    if (cus)
                                    {
                                        _message = _message + "This promotion need Customer code, Please enter customer code";
                                    }
                                    if (nic)
                                    {
                                        _message = _message + "This promotion need ID Number, Please enter ID Number";
                                    }
                                    if (mob)
                                    {
                                        _message = _message + "This promotion need Mobile Number, Please enter  Mobile Number";
                                    }
                                    if (cus || nic || mob)
                                    {
                                        DisplayMessageJS(_message);
                                        DisplayMessage(_message, 1);
                                        return;
                                    }
                                }
                            }

                            if (chkDeliverNow.Checked == false)
                                foreach (PriceCombinedItemRef _ref in _tempPriceCombinItem)
                                {
                                    string _item = _ref.Sapc_itm_cd;
                                    string _originalItem = _ref.Sapc_itm_cd;
                                    string _similerItem = Convert.ToString(_ref.Similer_item);
                                    if (!string.IsNullOrEmpty(_similerItem)) _item = _similerItem;
                                    string _status = _ref.Status;
                                    string _qty = Convert.ToString(_ref.Sapc_qty);
                                    MasterItem _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                                    if (_itm.Mi_is_ser1 == 1)
                                    {
                                        _isSingleItemSerialized = true;
                                        decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                        //if (_serialcount != Convert.ToDecimal(_qty))
                                        //{
                                        //    DisplayMessageJS("Quantity and the selected serials mismatch in " + _item);
                                        //    return;
                                        //}
                                    }
                                    else if (_itm.Mi_is_ser1 == 0 || _itm.Mi_is_ser1 == -1)
                                    {
                                        decimal _pickQty = 0;
                                        if (IsPriceLevelAllowDoAnyStatus) _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item).ToList().Select(x => x.Sad_qty).Sum(); else _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item && x.Mi_itm_stus == _status).ToList().Select(x => x.Sad_qty).Sum();
                                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                        List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _status);
                                        if (_inventoryLocation != null)
                                            if (_inventoryLocation.Count > 0)
                                            {
                                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                                if (Convert.ToDecimal(_qty) > _invBal)
                                                {
                                                    //DisplayMessageJS(_item + " item quantity exceeding its inventory balance Inventory balance" + FormatToQty(Convert.ToString(_invBal)));
                                                    //return;
                                                }
                                            }
                                            else
                                            {
                                                //DisplayMessageJS(_item + " item quantity exceeding its inventory balance Inventory balance  " + FormatToQty(Convert.ToString("0")));
                                                //return;
                                            }
                                        else
                                        {
                                            //DisplayMessageJS(_item + " item quantity exceeding its inventory balance Inventory balance  " + FormatToQty(Convert.ToString("0")));
                                            // return;
                                        }
                                    }
                                }
                            if (chkDeliverNow.Checked)
                            {
                                foreach (PriceCombinedItemRef _ref in _tempPriceCombinItem)
                                {
                                    string _item = _ref.Sapc_itm_cd;
                                    string _originalItem = _ref.Sapc_itm_cd;
                                    string _similerItem = Convert.ToString(_ref.Similer_item);
                                    if (!string.IsNullOrEmpty(_similerItem)) _item = _similerItem;
                                    string _status = _ref.Status;
                                    string _qty = Convert.ToString(_ref.Sapc_qty);
                                    MasterItem _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                                    if (IsGiftVoucher(_itm.Mi_itm_tp))
                                    {
                                        _isSingleItemSerialized = true;
                                        decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                        if (_serialcount != Convert.ToDecimal(_qty))
                                        {
                                            DisplayMessageJS("Quantity and the selected serials mismatch in " + _item);
                                            return;
                                        }
                                    }
                                }
                            }
                            //if (_isSingleItemSerialized && chkDeliverNow.Checked == false)
                            //    if (PriceCombinItemSerialList == null)
                            //    {
                            //        DisplayMessageJS("You have to select the serial for the promotion items");
                            //        return;
                            //    }
                            //if (_isSingleItemSerialized && chkDeliverNow.Checked == false)
                            //    if (PriceCombinItemSerialList.Count <= 0)
                            //    {
                            //        DisplayMessageJS("You have to select the serial for the promotion items");
                            //        return;
                            //    }
                            SetSSPriceDetailVariable(_circulerncode, _pbLineSeq, _pbSeq, _pbPrice, _promotioncode, _promotiontype);
                            _MainPriceCombinItem = _tempPriceCombinItem;
                            decimal pvalue = Convert.ToDecimal(_unitprice);
                            txtUnitPrice.Text = FormatToCurrency(_unitprice);
                            txtUnitPrice.Text = pvalue.ToString("N2");
                            txtTaxAmt.Text = SetDecimalPoint(FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false))));
                            CalculateItem();
                            mpPriceNPromotion.Hide();
                            lbtnadditems.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessageJS(ex.Message);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void PromItm_Status_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList lb = (DropDownList)sender;
            GridViewRow row1 = (GridViewRow)lb.NamingContainer;

            Session["SelectedPromoItmRow"] = row1;

            try
            {
                string _oldStatus = lb.Items[0].Value.ToString();

                List<ReptPickSerials> _giftVoucher = new List<ReptPickSerials>();

                Label lblsapc_itm_cd = (Label)row1.FindControl("lblsapc_itm_cd");
                string _originalItem = lblsapc_itm_cd.Text.Trim();
                string _item = lblsapc_itm_cd.Text.Trim();
                Label lblSimiler_item = (Label)row1.FindControl("lblSimiler_item");
                string _similerItem = lblSimiler_item.Text.Trim();

                DropDownList PromItm_Status = (DropDownList)row1.FindControl("PromItm_Status");
                string _status = PromItm_Status.SelectedValue.ToString(); //cmbStatus.Text.Trim();

                Label lblsapc_qty = (Label)row1.FindControl("lblsapc_qty");
                string _qty = lblsapc_qty.Text.Trim();

                Label lblsapc_sub_ser = (Label)row1.FindControl("lblsapc_sub_ser");
                string _serial = lblsapc_sub_ser.Text.Trim();

                Label lblsapc_increse = (Label)row1.FindControl("lblsapc_increse");
                bool _haveSerial = Convert.ToBoolean(lblsapc_increse.Text.ToString());

                string _PromotionCD = Session["Promotion"].ToString();

                if (!string.IsNullOrEmpty(_similerItem))
                    _item = _similerItem;

                if (PriceCombinItemSerialList != null && PriceCombinItemSerialList.Count > 0)
                {
                    PriceCombinItemSerialList.RemoveAll(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == _oldStatus);
                }

                bool _isGiftVoucher = IsGiftVoucher(CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item).Mi_itm_tp);

                if (!_isGiftVoucher)
                {
                    DisplayAvailableQty(_item, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, _status);
                }
                else
                {
                    LoadGiftVoucherBalance(_item, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, out _giftVoucher);
                }

                if (_isGiftVoucher)
                {
                    //List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                    //_promotionSerial = new List<ReptPickSerials>();
                    //_promotionSerialTemp = new List<ReptPickSerials>();
                    //if (_giftVoucher != null)
                    //    if (_giftVoucher.Count > 0)
                    //        _lst.AddRange(_giftVoucher);
                    //_promotionSerial = _lst;
                    //gvPopComItemSerial.DataSource = new List<ReptPickSerials>();
                    //gvPopComItemSerial.DataSource = _lst;
                    //txtPriNProSerialSearch.Text = ".";
                    //txtPriNProSerialSearch.Text = string.Empty;
                    DisplayMessageJS("Develop 11423");
                }
                else if (_priceBookLevelRef.Sapl_is_serialized)
                {
                    if (_haveSerial == false && chkDeliverNow.Checked == false)
                        LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, 7);
                    else if (_haveSerial == true && !string.IsNullOrEmpty(_similerItem))
                        LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, 7);
                    else if (_haveSerial == true && chkDeliverNow.Checked == false)
                    {
                        List<InventorySerialRefN> _ref = CHNLSVC.Inventory.GetItemDetailBySerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _serial);
                        if (_ref != null)
                            if (_ref.Count > 0)
                            {
                                var _available = _ref.Where(x => x.Ins_itm_cd == _item).ToList();
                                if (_available == null || _available.Count <= 0)
                                {
                                    DisplayMessageJS("Selected item does not available in the current inventory");
                                    return;
                                }
                            }
                    }
                }
                else if (chkDeliverNow.Checked == false)
                {
                    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, 7);
                }
                else
                {
                    var _list = new BindingList<ReptPickSerials>(new List<ReptPickSerials>());
                    gvPromotionSerial.DataSource = _list;
                    gvPromotionSerial.DataBind();
                    markSelectedItems();
                }
            }
            catch (Exception ex)
            {
                DisplayMessageJS(ex.Message);
            }
        }

        private void promotionsCustomMethod(GridViewRow row1)
        {
            Session["SelectedPromoItmRow"] = row1;

            List<ReptPickSerials> _giftVoucher = new List<ReptPickSerials>();

            Label lblsapc_itm_cd = (Label)row1.FindControl("lblsapc_itm_cd");
            string _originalItem = lblsapc_itm_cd.Text.Trim();
            string _item = lblsapc_itm_cd.Text.Trim();
            Label lblSimiler_item = (Label)row1.FindControl("lblSimiler_item");
            string _similerItem = lblSimiler_item.Text.Trim();

            DropDownList PromItm_Status = (DropDownList)row1.FindControl("PromItm_Status");

            string _oldStatus = PromItm_Status.Items[0].Value.ToString();

            string _status = PromItm_Status.SelectedValue.ToString(); //cmbStatus.Text.Trim();

            Label lblsapc_qty = (Label)row1.FindControl("lblsapc_qty");
            string _qty = lblsapc_qty.Text.Trim();

            Label lblsapc_sub_ser = (Label)row1.FindControl("lblsapc_sub_ser");
            string _serial = lblsapc_sub_ser.Text.Trim();

            Label lblsapc_increse = (Label)row1.FindControl("lblsapc_increse");
            bool _haveSerial = Convert.ToBoolean(lblsapc_increse.Text.ToString());

            string _PromotionCD = Session["Promotion"].ToString();

            if (!string.IsNullOrEmpty(_similerItem))
                _item = _similerItem;

            if (PriceCombinItemSerialList != null && PriceCombinItemSerialList.Count > 0)
            {
                PriceCombinItemSerialList.RemoveAll(x => x.Tus_itm_cd == _item && x.Tus_itm_stus == _oldStatus);
            }

            bool _isGiftVoucher = IsGiftVoucher(CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item).Mi_itm_tp);

            if (!_isGiftVoucher)
            {
                DisplayAvailableQty(_item, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, _status);
            }
            else
            {
                LoadGiftVoucherBalance(_item, lblPriNProAvailableStatusQty, lblPriNProAvailableQty, out _giftVoucher);
            }

            if (_isGiftVoucher)
            {
                //List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                //_promotionSerial = new List<ReptPickSerials>();
                //_promotionSerialTemp = new List<ReptPickSerials>();
                //if (_giftVoucher != null)
                //    if (_giftVoucher.Count > 0)
                //        _lst.AddRange(_giftVoucher);
                //_promotionSerial = _lst;
                //gvPopComItemSerial.DataSource = new List<ReptPickSerials>();
                //gvPopComItemSerial.DataSource = _lst;
                //txtPriNProSerialSearch.Text = ".";
                //txtPriNProSerialSearch.Text = string.Empty;
                DisplayMessageJS("Develop 11423");
            }
            else if (_priceBookLevelRef.Sapl_is_serialized)
            {
                if (_haveSerial == false && chkDeliverNow.Checked == false)
                    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, 7);
                else if (_haveSerial == true && !string.IsNullOrEmpty(_similerItem))
                    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, 7);
                else if (_haveSerial == true && chkDeliverNow.Checked == false)
                {
                    List<InventorySerialRefN> _ref = CHNLSVC.Inventory.GetItemDetailBySerial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _serial);
                    if (_ref != null)
                        if (_ref.Count > 0)
                        {
                            var _available = _ref.Where(x => x.Ins_itm_cd == _item).ToList();
                            if (_available == null || _available.Count <= 0)
                            {
                                DisplayMessageJS("Selected item does not available in the current inventory");
                                return;
                            }
                        }
                }
            }
            else if (chkDeliverNow.Checked == false)
            {
                LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, 7);
            }
            else
            {
                var _list = new BindingList<ReptPickSerials>(new List<ReptPickSerials>());
                gvPromotionSerial.DataSource = _list;
                gvPromotionSerial.DataBind();
                markSelectedItems();
            }

        }

        protected void btnPromoCLose_Click(object sender, EventArgs e)
        {
            hdfConf.Value = "0";
            Session["mpPriceNPromotion"] = null;
            mpPriceNPromotion.Hide();
        }

        protected void btnPriNProSerClear_Click(object sender, EventArgs e)
        {
            txtPriNProSerialSearch.Text = "";
            gvPromotionSerial.DataSource = new int[] { };
            gvPromotionSerial.DataBind();
        }

        protected void btnPriNProSerConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["SelectedPromoItmRow"] == null)
                {
                    DisplayMessageJS("Please select promotion item again!");
                    return;
                }

                GridViewRow row1 = (GridViewRow)Session["SelectedPromoItmRow"];

                Label lblsapc_itm_cd = (Label)row1.FindControl("lblsapc_itm_cd");
                Label lblSimiler_item = (Label)row1.FindControl("lblSimiler_item");
                DropDownList PromItm_Status = (DropDownList)row1.FindControl("PromItm_Status");
                Label lblsapc_qty = (Label)row1.FindControl("lblsapc_qty");
                Label lblsapc_sub_ser = (Label)row1.FindControl("lblsapc_sub_ser");
                Label lblsapc_increse = (Label)row1.FindControl("lblsapc_increse");

                txtPriNProSerialSearch.Text = string.Empty;
                decimal _serialcount = 0;
                decimal _promotionItemQty = Convert.ToDecimal(lblsapc_qty.Text.Trim());
                string _promotionItem = lblsapc_itm_cd.Text.Trim();
                string _promotionOriginalItem = lblsapc_itm_cd.Text.Trim();
                string _SimilerItem = lblSimiler_item.Text.Trim();
                if (!string.IsNullOrEmpty(_SimilerItem))
                {
                    _promotionItem = _SimilerItem;
                }

                for (int i = 0; i < gvPromotionSerial.Rows.Count; i++)
                {
                    CheckBox chkSelectPromSerial = (CheckBox)gvPromotionSerial.Rows[i].FindControl("chkSelectPromSerial");
                    if (chkSelectPromSerial != null && chkSelectPromSerial.Checked == true)
                    {
                        _serialcount += 1;
                    }
                }

                if (_serialcount != _promotionItemQty)
                {
                    DisplayMessageJS("Quantity and the selected serials mismatch. Item quantity - " + _promotionItemQty.ToString() + "but serials - " + _serialcount.ToString());
                    return;
                }
                if (_serialcount > _promotionItemQty)
                {
                    DisplayMessageJS("Quantity and the selected serials mismatch. Item quantity - " + _promotionItemQty.ToString() + "but serials - " + _serialcount.ToString());
                    return;
                }
                if (PriceCombinItemSerialList != null)
                    if (PriceCombinItemSerialList.Count > 0)
                    {
                        decimal _count = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _promotionItem).Count();
                        if (_count >= _promotionItemQty)
                        {
                            DisplayMessageJS("You already pick serials for the item");
                            return;
                        }
                    }

                for (int i = 0; i < gvPromotionSerial.Rows.Count; i++)
                {

                    CheckBox chkSelectPromSerial = (CheckBox)gvPromotionSerial.Rows[i].FindControl("chkSelectPromSerial");
                    if (chkSelectPromSerial != null && chkSelectPromSerial.Checked == true)
                    {
                        Label lblTus_itm_cd = (Label)gvPromotionSerial.Rows[i].FindControl("lblTus_itm_cd");
                        Label lblTus_ser_1 = (Label)gvPromotionSerial.Rows[i].FindControl("lblTus_ser_1");
                        Label lblTus_ser_2 = (Label)gvPromotionSerial.Rows[i].FindControl("lblTus_ser_2");
                        Label lblTus_ser_3 = (Label)gvPromotionSerial.Rows[i].FindControl("lblTus_ser_3");

                        string _item = lblTus_itm_cd.Text.Trim();
                        string _serial = lblTus_ser_1.Text.Trim();
                        string _serial2 = lblTus_ser_2.Text.Trim();
                        MasterItem _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
                        string _prefix = lblTus_ser_3.Text.Trim();
                        bool _isGiftVoucher = IsGiftVoucher(_itm.Mi_itm_tp);
                        ReptPickSerials _serLst = null;
                        if (!_isGiftVoucher) _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _item, _serial); else _serLst = CHNLSVC.Inventory.GetGiftVoucherDetail(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _item, Convert.ToInt32(_serial2), Convert.ToInt32(_serial), _prefix);
                        _serLst.Tus_session_id = _promotionOriginalItem;
                        if (_serLst != null)
                            if (_serLst.Tus_ser_1 != null || !string.IsNullOrEmpty(_serLst.Tus_ser_1))
                            {
                                if (PriceCombinItemSerialList != null)
                                    if (PriceCombinItemSerialList.Count > 0)
                                    {
                                        var _dup = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _serLst.Tus_itm_cd && x.Tus_ser_1 == _serLst.Tus_ser_1).ToList();
                                        if (_dup != null)
                                            if (_dup.Count > 0)
                                            {
                                                DisplayMessageJS(_serLst.Tus_ser_1 + "Serial duplicating!");
                                                return;
                                            }
                                            else
                                                PriceCombinItemSerialList.Add(_serLst);
                                        else
                                            PriceCombinItemSerialList.Add(_serLst);
                                    }
                                    else
                                    {
                                        PriceCombinItemSerialList.Add(_serLst);
                                    }
                                else
                                {
                                    PriceCombinItemSerialList.Add(_serLst);
                                }
                            }
                    }
                }
                List<ReptPickSerials> _lst = new List<ReptPickSerials>();
                gvPromotionSerial.DataSource = _lst;
                gvPromotionSerial.DataBind();
                markSelectedItems();

                mpPriceNPromotion.Show();
            }
            catch (Exception ex)
            {
                DisplayMessageJS(ex.Message);
            }
        }

        protected void btnPriNProConfirm_Click(object sender, EventArgs e)
        {

        }

        protected void btnPriNProCancel_Click(object sender, EventArgs e)
        {
            try
            {
                PriceCombinItemSerialList = new List<ReptPickSerials>();
                _tempPriceCombinItem = new List<PriceCombinedItemRef>();
                _promotionSerial = new List<ReptPickSerials>();
                _promotionSerialTemp = new List<ReptPickSerials>();
                txtUnitPrice.Text = FormatToCurrency("0");
                CalculateItem();
                hdfConf.Value = "0";
                Session["mpPriceNPromotion"] = null;
                hdfConfItem.Value = null;
                mpPriceNPromotion.Hide();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void btnCloseGroup_Click(object sender, EventArgs e)
        {
            mpGroup.Hide();
        }

        protected void btnGroupSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbInvType.Text.Trim()))
            {
                DisplayMessage("Please select the invoice type!");
                cmbInvType.Focus();
                return;
            }

            if (cmbInvType.Text.Trim() != "CRED")
            {
                DisplayMessage("Group sales only available for credit sales!");
                return;
            }
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Group_Sale);
                DataTable result = CHNLSVC.CommonSearch.GetGroupSaleSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Group_Sale";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
                txtGroup.Focus();
            }
            catch (Exception ex)
            {
                txtGroup.Text = "";
                DisplayMessage(ex.Message);
            }
        }

        protected void btnGroup_Click(object sender, EventArgs e)
        {
            mpGroup.Show();
        }

        protected void btnConfDiscClose_Click(object sender, EventArgs e)
        {
            mpConfDiscount.Hide();
        }

        protected void btnConfDisYes_Click(object sender, EventArgs e)
        {

        }

        protected void btnConfDisNo_Click(object sender, EventArgs e)
        {

        }
        private void LoadSoData(string sono, string company, string pc)
        {
            try
            {
                DataTable dtHeadersSo = CHNLSVC.Sales.SearchSalesOrdData(sono, company, pc);
                if (dtHeadersSo.Rows.Count > 0)
                {
                    foreach (DataRow item in dtHeadersSo.Rows)
                    {
                        Session["SOSEQNO"] = item[25].ToString();
                        Session["STUS"] = item[19].ToString();
                        if (Session["STUS"].ToString() != "S")
                        {
                            btnUpdate.Visible = false;
                        }
                        else
                        {
                            btnUpdate.Visible = true;
                        }
                        if (Session["STUS"].ToString() == "P")
                        {
                            btnUpdate.Visible = true;
                        }

                        DateTime oreddate = Convert.ToDateTime(item[3].ToString());
                        string oreddatetext = oreddate.ToString("dd/MMM/yyyy");
                        txtdate.Text = oreddatetext;

                        cmbInvType.SelectedValue = item[1].ToString();
                        txtManualRefNo.Text = item[5].ToString();
                        txtdocrefno.Text = item[6].ToString();
                        txtCustomer.Text = item[7].ToString();
                        txtCusName.Text = item[8].ToString();
                        txtAddress1.Text = item[9].ToString();
                        txtAddress2.Text = item[10].ToString();
                        txtcurrency.Text = item[11].ToString();
                        txtPoNo.Text = item[21].ToString();
                        txtdelcuscode.Text = item[13].ToString();
                        txtPerTown.Text = item[36].ToString();
                        txtRemarks.Text = item[37].ToString();
                        DataTable dtcusdata = CHNLSVC.Sales.SearchCustomer(Session["UserCompanyCode"].ToString(), txtdelcuscode.Text.Trim());

                        if (dtcusdata.Rows.Count > 0)
                        {
                            foreach (DataRow item_2 in dtcusdata.Rows)
                            {
                                txtdellocation.Text = item[23].ToString();

                                txtdelname.Text = item_2[3].ToString();
                                txtdelad1.Text = item[14].ToString();
                                txtdelad2.Text = item[15].ToString();
                            }
                        }

                        if (cmbExecutive.Items.FindByValue(item[17].ToString()) != null)
                        {
                            cmbExecutive.SelectedValue = item[17].ToString();
                        }
                        else
                        {
                            cmbExecutive.SelectedIndex = 0;
                        }

                        txtexcutive.Text = item[17].ToString();
                        txtQuotation.Text = item[18].ToString();
                        txtPromotor.Text = item[20].ToString();
                        //txtLoyalty.Text = item[26].ToString();

                        if (item[27].ToString() == "1")
                        {
                            chkTaxPayable.Checked = true;
                        }
                        else
                        {
                            chkTaxPayable.Checked = false;
                        }

                        if (item[28].ToString() == "1")
                        {
                            lblVatExemptStatus.Text = "Available";
                        }
                        else
                        {
                            lblVatExemptStatus.Text = "None";
                        }

                        if (item[22].ToString() == "1")
                        {
                            lblSVatStatus.Text = "Available";
                        }
                        else
                        {
                            lblSVatStatus.Text = "None";
                        }

                        txtlocation.Text = item[29].ToString();

                        if (cmbTechnician.Items.FindByValue(item[30].ToString()) != null)
                        {
                            cmbTechnician.SelectedValue = item[30].ToString();
                        }
                        else
                        {
                            cmbTechnician.SelectedIndex = 0;
                        }

                        Session["DCUSCODE"] = item[13].ToString();
                        Session["DCUSNAME"] = item[24].ToString();
                        Session["DTOWN"] = item[12].ToString();
                        Session["DCUSADD1"] = item[14].ToString();
                        Session["DCUSADD2"] = item[15].ToString();
                        Session["DCUSLOC"] = item[23].ToString();

                        DataTable dttowns = CHNLSVC.General.GetTownByCode(item[12].ToString());
                        string townname = string.Empty;

                        if (dttowns.Rows.Count > 0)
                        {
                            foreach (DataRow ddrin in dttowns.Rows)
                            {
                                townname = ddrin["mt_desc"].ToString();
                            }
                        }

                        Session["DTOWNNAME"] = townname;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Invalid sales order number !!!')", true);
                    txtInvoiceNo.Text = string.Empty;
                    return;
                }
            }
            catch (Exception ex)
            {
                //  ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                DispMsg(ex.Message, "E");
            }
        }
        private void LoadSoItemData(string sono)
        {
            try
            {
                _invoiceItemList = null;
                _invoiceItemList = new List<InvoiceItem>();
                List<SalesOrderItems> dtitems = new List<SalesOrderItems>();
                dtitems = CHNLSVC.Sales.SearchSalesOrdItems(sono);
                if (dtitems.Count > 0)
                {
                    foreach (SalesOrderItems _itm in dtitems)
                    {
                        InvoiceItem _IItem = new InvoiceItem();
                        _IItem.Sad_itm_line = _itm.SOI_ITM_LINE;
                        _IItem.Sad_itm_cd = _itm.SOI_ITM_CD;

                        _IItem.Mi_longdesc = _itm.Mi_longdesc;
                        _IItem.Sad_itm_stus = _itm.SOI_ITM_STUS;
                        _IItem.Sad_itm_stus_desc = _itm.Mi_status_Des;
                        _IItem.Sad_qty = _itm.SOI_QTY;
                        _IItem.Sad_unit_rt = _itm.SOI_UNIT_RT;
                        _IItem.Sad_unit_amt = _itm.SOI_UNIT_AMT;
                        _IItem.Sad_disc_amt = _itm.SOI_DISC_AMT;
                        _IItem.Sad_itm_tax_amt = _itm.SOI_ITM_TAX_AMT;
                        _IItem.Sad_tot_amt = _itm.SOI_TOT_AMT;
                        _IItem.Sad_pbook = _itm.SOI_PBOOK;
                        _IItem.Sad_pb_lvl = _itm.SOI_PB_LVL;
                        _IItem.Sad_res_no = _itm.SOI_RES_NO;
                        _IItem.Sad_job_line = _itm.SOI_JOB_LINE;
                        _IItem.Sad_res_no = _itm.SOI_RES_NO;
                        _IItem.Sad_res_line_no = _itm.SOI_RES_LINE_NO;
                        _IItem.Sad_promo_cd = _itm.SOI_PROMO_CD;
                        _invoiceItemList.Add(_IItem);
                    }
                    gvInvoiceItem.DataSource = null;
                    gvInvoiceItem.DataBind();

                    gvInvoiceItem.DataSource = _invoiceItemList;
                    gvInvoiceItem.DataBind();
                }

                // ViewState["ITEMSTABLE"] = null;
                // this.BindItemsGrid();

                ViewState["ITEMSTABLE"] = _invoiceItemList;
                //  this.BindItemsGrid();
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                DispMsg(ex.Message, "E");
            }
        }
        private void LoadSoSerialData(string sono)
        {
            try
            {
                DataTable dtserialdata = CHNLSVC.Sales.SearchSalesOrdSerials(sono);
                List<ReptPickSerials> _serial = new List<ReptPickSerials>();
                DataTable uniqueCols = RemoveDuplicateRows(dtserialdata, "sose_ser_1");
                foreach (DataRow _row in uniqueCols.Rows)
                {
                    ReptPickSerials _serlaobj = new ReptPickSerials();
                    _serlaobj.Tus_itm_line = Convert.ToInt32(_row[0].ToString());
                    // _serlaobj.Tus_base_itm_line = Convert.ToInt32(_row[0].ToString());
                    _serlaobj.Tus_itm_cd = _row[1].ToString();
                    _serlaobj.Tus_itm_model = _row[2].ToString();
                    _serlaobj.Tus_itm_stus = _row[3].ToString();
                    _serlaobj.Tus_qty = Convert.ToInt32(_row[4].ToString());
                    _serlaobj.Tus_ser_1 = _row[5].ToString();
                    _serlaobj.Tus_ser_2 = _row[6].ToString();
                    _serlaobj.Tus_warr_no = _row[7].ToString();
                    _serlaobj.Tus_itm_stus_Desc = _row[8].ToString();
                    _serial.Add(_serlaobj);
                }
                if (dtserialdata.Rows.Count > 0)
                {
                    gvPopSerial.DataSource = null;
                    gvPopSerial.DataBind();

                    gvPopSerial.DataSource = _serial;
                    gvPopSerial.DataBind();
                    ViewState["SERIALSTABLE"] = _serial;
                }

                //ViewState["SERIALSTABLE"] = null;
                // this.BindSerialsGrid();

                //  ViewState["SERIALSTABLE"] = uniqueCols;
                //  this.BindSerialsGrid();
            }
            catch (Exception ex)
            {
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                DispMsg(ex.Message, "E");
            }
        }
        private void CalculateGrandTotalNew(Decimal _qty, Decimal _uprice, Decimal _discount, Decimal _tax, bool _isAddition)
        {
            if (_isAddition)//++
            {
                GrndSubTotal = GrndSubTotal + Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount + Convert.ToDecimal(_discount);
                GrndTax = GrndTax + Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = DoFormat(GrndSubTotal);
                lblGrndDiscount.Text = DoFormat(GrndDiscount);
                lblGrndTax.Text = DoFormat(GrndTax);
            }
            else//--
            {
                GrndSubTotal = GrndSubTotal - Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount - Convert.ToDecimal(_discount);
                GrndTax = GrndTax - Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = DoFormat(GrndSubTotal);
                lblGrndDiscount.Text = DoFormat(GrndDiscount);
                lblGrndTax.Text = DoFormat(GrndTax);
            }

            lblGrndAfterDiscount.Text = (Convert.ToString(GrndSubTotal - GrndDiscount));
            lblGrndAfterDiscount.Text = DoFormat(GrndSubTotal - GrndDiscount);
            //if (_invoiceItemList != null || _invoiceItemList.Count > 0)
            //{
            //    lblGrndTotalAmount.Text = (Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt)));
            //}
            //else
            //{
            //    lblGrndTotalAmount.Text = (Convert.ToString("0"));
            //}

            Decimal grand = GrndSubTotal + GrndTax;
            lblGrndTotalAmount.Text = DoFormat(grand);
        }
        public static string DoFormat(Decimal myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);

            if (s.EndsWith("00"))
            {
                return s;
            }
            else
            {
                return s;
            }
        }
        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text)) { txtCustomer.Focus(); return; }
            try
            {
                GrndSubTotal = 0;
                GrndDiscount = 0;
                GrndTax = 0;
                LoadSoData(txtInvoiceNo.Text, Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                LoadSoItemData(txtInvoiceNo.Text);
                LoadSoSerialData(txtInvoiceNo.Text);
                // CheckInvoiceNo();

                _masterBusinessCompany = new MasterBusinessEntity();
                _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, Session["UserCompanyCode"].ToString());

                if (_masterBusinessCompany.Mbe_is_tax)
                {
                    chkTaxPayable.Checked = true;
                    chkTaxPayable.Enabled = true;
                }
                else
                {
                    chkTaxPayable.Checked = false;
                    chkTaxPayable.Enabled = false;
                }
                ViewCustomerAccountDetail(txtCustomer.Text.Trim());

                string stus = (string)Session["STUS"];

                if (stus == "R")
                {
                    btnSave.CssClass = "buttoncolor";
                    btnSave.OnClientClick = "return Enable();";

                    lbtnapprove.CssClass = "buttoncolor";
                    lbtnapprove.OnClientClick = "return Enable();";

                    // lbtncancel.CssClass = "buttoncolor";
                    // lbtncancel.OnClientClick = "return Enable();";

                    // lbtnreject.CssClass = "buttoncolor";
                    // lbtnreject.OnClientClick = "return Enable();";
                }

                else if (stus == "A")
                {
                    btnSave.CssClass = "buttoncolor";
                    btnSave.OnClientClick = "return Enable();";

                    lbtnapprove.CssClass = "buttoncolor";
                    lbtnapprove.OnClientClick = "return Enable();";

                    //  lbtnreject.CssClass = "buttoncolor";
                    //  lbtnreject.OnClientClick = "return Enable();";

                    //  lbtncancel.CssClass = "buttonUndocolor";
                    // lbtncancel.OnClientClick = "ConfirmCancelOrder();";
                    LinkButton4.Visible = false;
                }

                else if (stus == "C")
                {
                    btnSave.CssClass = "buttoncolor";
                    btnSave.OnClientClick = "return Enable();";

                    lbtnapprove.CssClass = "buttoncolor";
                    lbtnapprove.OnClientClick = "return Enable();";

                    // lbtnreject.CssClass = "buttoncolor";
                    //  lbtnreject.OnClientClick = "return Enable();";

                    //   lbtncancel.CssClass = "buttoncolor";
                    //  lbtncancel.OnClientClick = "return Enable();";lblGrndTotalAmount
                }
                else if (stus == "F")
                {
                    btnSave.CssClass = "buttoncolor";
                    btnSave.OnClientClick = "return Enable();";

                    lbtnapprove.CssClass = "buttoncolor";
                    lbtnapprove.OnClientClick = "return Enable();";

                    // lbtnreject.CssClass = "buttoncolor";
                    //  lbtnreject.OnClientClick = "return Enable();";

                    //   lbtncancel.CssClass = "buttoncolor";
                    //  lbtncancel.OnClientClick = "return Enable();";
                }
                else if (stus == "S")
                {
                    //btnSave.CssClass = "buttoncolor";
                    //btnSave.OnClientClick = "return Enable();";

                    //lbtnapprove.CssClass = "buttonUndocolor";
                    //lbtnapprove.OnClientClick = "ConfirmApproveOrder();";

                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    LinkButton4.Visible = false;


                }
                else if (stus == "P")
                {
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    LinkButton4.Visible = true;
                }
                else
                {
                    btnSave.CssClass = "buttonUndocolor";
                    btnSave.OnClientClick = "ConfirmPlaceOrder();";

                    lbtnapprove.CssClass = "buttonUndocolor";
                    lbtnapprove.OnClientClick = "ConfirmApproveOrder();";

                    // lbtnreject.CssClass = "buttonUndocolor";
                    // lbtnreject.OnClientClick = "ConfirmRejectOrder();";

                    // lbtncancel.CssClass = "buttonUndocolor";
                    //  lbtncancel.OnClientClick = "ConfirmCancelOrder();";
                }
                decimal _tmpTotAmt = 0;
                foreach (GridViewRow row in gvInvoiceItem.Rows)
                {
                    Label Qty = (Label)row.FindControl("InvItm_Qty");
                    Label unitPrice = (Label)row.FindControl("InvItm_UPrice");
                    Label DisAmt = (Label)row.FindControl("InvItm_DisAmt");
                    Label Tax = (Label)row.FindControl("InvItm_TaxAmt");
                    Label lblsad_tot_amt = row.FindControl("lblsad_tot_amt") as Label;
                    _tmpTotAmt = _tmpTotAmt + Convert.ToDecimal(lblsad_tot_amt.Text);
                    CalculateGrandTotalNew(Convert.ToDecimal(Qty.Text), Convert.ToDecimal(unitPrice.Text), Convert.ToDecimal(DisAmt.Text), Convert.ToDecimal(Tax.Text), true);
                }


                Decimal grandtot = Convert.ToDecimal(lblGrndAfterDiscount.Text) + Convert.ToDecimal(lblGrndTax.Text);
                lblGrndTotalAmount.Text = DoFormat(grandtot);
                if (!string.IsNullOrEmpty(txtInvoiceNo.Text))
                {
                    lblGrndTotalAmount.Text = DoFormat(_tmpTotAmt);
                }
                // dvhiddendel.Visible = false;
            }
            catch (Exception ex)
            {
                txtInvoiceNo.Text = "";
                DisplayMessage(ex.Message);
            }
        }

        private void DecideTokenInvoice()
        {
            //if (lblInvoice.BackColor == Color.SteelBlue) 
            //    IsToken = false; 
            //else
            //    IsToken = true;
        }

        private void RecallInvoice()
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text))
                return;
            InvoiceHeader _hdr = CHNLSVC.Sales.GetInvoiceHeaderDetails(txtInvoiceNo.Text);

            if (_hdr == null)
            {
                DisplayMessage("Please select the valid invoice");
                txtInvoiceNo.Text = string.Empty;
                return;
            }
            //Add by Chamal 20-07-2014
            if (_hdr.Sah_pc != Session["UserDefProf"].ToString().ToString())
            {
                DisplayMessage("Please select the valid invoice");
                txtInvoiceNo.Text = string.Empty;
                return;
            }
            //Add by Chamal 25-08-2014
            if (_hdr.Sah_tp != "INV")
            {
                DisplayMessage("Please select the valid invoice");
                txtInvoiceNo.Text = string.Empty;
                return;
            }
            if (_hdr.Sah_inv_tp == "CS")
            {
            }
            else if (_hdr.Sah_inv_tp == "CRED")
            {
            }
            else
            {
                DisplayMessage("Please select the valid invoice");
                txtInvoiceNo.Text = string.Empty;
                return;
            }



            AssignInvoiceHeaderDetail(_hdr);
            List<InvoiceItem> _list = CHNLSVC.Sales.GetInvoiceDetailByInvoice(txtInvoiceNo.Text.Trim());
            _invoiceItemList = _list;
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            InvoiceSerialList = new List<InvoiceSerial>();
            ScanSerialList = new List<ReptPickSerials>();
            InvoiceSerialList = CHNLSVC.Sales.GetInvoiceSerial(txtInvoiceNo.Text.Trim());
            foreach (InvoiceItem itm in _list)
            { CalculateGrandTotal(itm.Sad_qty, itm.Sad_unit_rt, itm.Sad_disc_amt, itm.Sad_itm_tax_amt, true); _lineNo += 1; SSCombineLine += 1; }
            if (InvoiceSerialList == null)
                InvoiceSerialList = new List<InvoiceSerial>();
            gvInvoiceItem.DataSource = setItemDescriptions(_list);
            gvInvoiceItem.DataBind();

            if (_list.FindAll(x => x.Sad_res_no != "").Count > 0)
            {
                gvInvoiceItem.Columns[13].Visible = true;
            }
            else
            {
                gvInvoiceItem.Columns[13].Visible = false;
            }

            //load invoice serials
            if (InvoiceSerialList != null && InvoiceSerialList.Count > 0)
            {
                foreach (InvoiceSerial invSer in InvoiceSerialList)
                {
                    //ReptPickSerials _rept = CHNLSVC.Inventory.GetAvailableSerIDInformation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), invSer.Sap_itm_cd, invSer.Sap_ser_1, "N/A", "");
                    //if (_rept != null)
                    //{
                    //    List<InvoiceItem> _item = (from _res in _invoiceItemList
                    //                               where _res.Sad_itm_cd == invSer.Sap_itm_cd &&
                    //                               _res.Sad_itm_line == invSer.Sap_itm_line
                    //                               select _res).ToList<InvoiceItem>();
                    //    if (_item == null || _item.Count <= 0)
                    //    {
                    //        DisplayMessage("Error occurred while recalling invoiceItem - " + invSer.Sap_itm_cd + " not found on item list");
                    //        return;
                    //    }
                    //    _rept.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                    //    _rept.Tus_base_itm_line = _item[0].Sad_itm_line;
                    //    _rept.Tus_usrseq_no = -100;
                    //    _rept.Tus_unit_price = _rept.Tus_unit_price;
                    //    MasterItem msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), invSer.Sap_itm_cd);
                    //    //get item status

                    //    _rept.Tus_new_status = _item[0].Mi_itm_stus;
                    //    _rept.ItemType = msitem.Mi_itm_tp;
                    //    ScanSerialList.Add(_rept);
                    //}

                    DataTable dtSerDet = CHNLSVC.Inventory.GetSerialItem("SERIAL1", Session["UserCompanyCode"].ToString(), invSer.Sap_ser_1.Trim(), 1);
                    if (dtSerDet != null && dtSerDet.Rows.Count > 0)
                    {

                        InvoiceItem _listSe = _list.Find(x => x.Sad_itm_cd == invSer.Sap_itm_cd && x.Sad_itm_line == invSer.Sap_itm_line);
                        if (_listSe != null)
                        {
                            ReptPickSerials oNewser = new ReptPickSerials();
                            oNewser.Tus_itm_line = 0;
                            oNewser.Tus_itm_cd = invSer.Sap_itm_cd;
                            oNewser.Tus_itm_model = dtSerDet.Rows[0]["MI_MODEL"].ToString();
                            oNewser.Tus_itm_stus = _listSe.Sad_itm_stus;
                            oNewser.Tus_qty = 1;
                            oNewser.Tus_ser_1 = invSer.Sap_ser_1;
                            oNewser.Tus_ser_2 = invSer.Sap_ser_2;
                            oNewser.Tus_base_itm_line = invSer.Sap_itm_line;
                            oNewser.Tus_warr_no = "";
                            oNewser.Tus_serial_id = "";
                            oNewser.Tus_new_status = "";
                            oNewser.Tus_unit_price = 0;
                            ScanSerialList.Add(oNewser);
                        }
                    }
                }
            }

            gvPopSerial.AutoGenerateColumns = false;
            gvPopSerial.DataSource = setSerialStatusDescriptions(ScanSerialList);
            gvPopSerial.DataBind();

            gvPopSerial.Enabled = false;

            //end load invoice serials

            List<RecieptItem> _itms = CHNLSVC.Sales.GetReceiptItemList(txtInvoiceNo.Text.Trim());

            _recieptItem = _itms;


            if (_hdr.Sah_stus != "H")
            {
                btnSave.Enabled = false;
                txtItem.Enabled = false;
                txtSerialNo.Enabled = false;
                lbtnadditems.Enabled = false;

                btnSave.Enabled = false;
                btnSave.OnClientClick = "";
                btnSave.CssClass = "buttoncolor";

                gvInvoiceItem.Enabled = false;
                gvPopSerial.Enabled = false;

                setGriDel_enables(false);
                pnlDeliverBOdy.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
                txtItem.Enabled = true;
                txtSerialNo.Enabled = true;
                lbtnadditems.Enabled = true;

                btnSave.Enabled = true;
                btnSave.OnClientClick = "ConfirmPlaceOrder();";
                btnSave.CssClass = "buttonUndocolor";

                gvInvoiceItem.Enabled = true;
                gvPopSerial.Enabled = true;

                setGriDel_enables(true);

                pnlDeliverBOdy.Enabled = true;
            }

            List<ReptPickSerials> obuybackItems = CHNLSVC.Sales.GET_BUYBACKITMES_FOR_INVOIC(txtInvoiceNo.Text.Trim());
            if (obuybackItems != null && obuybackItems.Count > 0)
            {

            }

            txtdellocation.Text = _hdr.Sah_del_loc;
            txtdelcuscode.Text = _hdr.Sah_d_cust_cd;
            txtdelname.Text = _hdr.Sah_d_cust_name;
            txtdelad1.Text = _hdr.Sah_d_cust_add1;
            txtdelad2.Text = _hdr.Sah_d_cust_add2;





            DataTable dtVoucher = CHNLSVC.Sales.getSalesGiftVouchaer(txtInvoiceNo.Text.Trim());
            if (dtVoucher != null && dtVoucher.Rows.Count > 0)
            {
                List<ReptPickSerials> oinvoiVouc = new List<ReptPickSerials>();
                for (int i = 0; i < dtVoucher.Rows.Count; i++)
                {
                    ReptPickSerials oNew = new ReptPickSerials();
                    oNew.Tus_itm_cd = dtVoucher.Rows[i]["stvo_gv_itm"].ToString();
                    oNew.Tus_qty = 1;
                    oNew.Tus_ser_1 = dtVoucher.Rows[i]["stvo_pageno"].ToString();
                    oNew.Tus_ser_2 = dtVoucher.Rows[i]["stvo_bookno"].ToString();
                    oNew.Tus_warr_no = dtVoucher.Rows[i]["stvo_itm_cd"].ToString();
                    oinvoiVouc.Add(oNew);
                }


            }
        }

        private void Cancel()
        {
            if (IsBackDateOk(true, false) == false)
            {
                DisplayMessage("Cannot cancel back date invoices");
                return;
            }

            if (string.IsNullOrEmpty(txtInvoiceNo.Text))
            {
                DisplayMessage("Please select the invoice no");
                txtInvoiceNo.Focus();
                return;
            }
            List<InvoiceHeader> _header = CHNLSVC.Sales.GetPendingInvoices(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), string.Empty, txtInvoiceNo.Text.Trim(), "C", DateTime.MinValue.ToString(), DateTime.MinValue.ToString());
            if (_header.Count <= 0)
            {
                DisplayMessage("Selected invoice no already canceled or invalid.");
                return;
            }
            if ((_header[0].Sah_stus == "A" || _header[0].Sah_stus == "H"))
            {
                if (!IsFwdSaleCancelAllowUser)
                {
                    DisplayMessage("You are not allow to cancel this forward sale. Please make a request for the forward sale cancellation. Permission code | 10002");
                    return;
                }
            }
            if (_header[0].Sah_stus == "D")
            {
                if (!IsDlvSaleCancelAllowUser)
                {
                    DisplayMessage("You are not allow to cancel delivered sale. Please make a request for the delivered sale cancellation. Permission code | 10042");
                    return;
                }
            }
            if (_header[0].Sah_inv_sub_tp.Contains("CC"))
            {
                DisplayMessage("Selected invoice belongs to a cash conversion. You cannot cancel  this invoice.");
                return;
            }
            Int32 _isRegistered = CHNLSVC.Sales.CheckforInvoiceRegistration(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoiceNo.Text.Trim());
            if (_isRegistered != 1)
            {
                DisplayMessage("This invoice already registered!. You are not allow for cancellation.");
                return;
            }
            Int32 _isInsured = CHNLSVC.Sales.CheckforInvoiceInsurance(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoiceNo.Text.Trim());
            if (_isInsured != 1)
            {
                DisplayMessage("This invoice already insured!. You are not allow for cancellation.");
                return;
            }
            //:: Chamal 7-Jul-2014 | :: If promotion voucher no generated invoice, refer for another invoice
            bool _isPromoVou = CHNLSVC.Sales.CheckPromoVoucherInvoiceUsed(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoiceNo.Text.Trim());
            if (_isPromoVou == true)
            {
                DisplayMessage("This invoice already used for promotion voucher invoice!. You are not allow for cancellation.");
                return;
            }

            //Tharaka Check PO number 2016-02-11
            if (!string.IsNullOrEmpty(_header[0].Sah_anal_4))
            {
                PurchaseOrder oPOheader = CHNLSVC.Inventory.GetPOHeader(Session["UserCompanyCode"].ToString(), _header[0].Sah_anal_4, "L");
                if (oPOheader != null && !string.IsNullOrEmpty(oPOheader.Poh_doc_no))
                {
                    InventoryHeader oInventoryHeader = CHNLSVC.Inventory.GetINTHDRByOthDoc(Session["UserCompanyCode"].ToString(), "GRN", oPOheader.Poh_doc_no);
                    if (oInventoryHeader != null && !string.IsNullOrEmpty(oInventoryHeader.Ith_doc_no))
                    {
                        DisplayMessage("GRN is created for the PO. PO number : " + oPOheader.Poh_doc_no);
                        return;
                    }
                }
            }

            try
            {
                DataTable _buybackdoc = CHNLSVC.Inventory.GetBuyBackInventoryDocument(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtInvoiceNo.Text.Trim());
                if (_buybackdoc != null)
                    if (_buybackdoc.Rows.Count > 0)
                    {
                        string _adjno = Convert.ToString(_buybackdoc.Rows[0].Field<string>("ith_doc_no"));
                        string _buybackloc = Convert.ToString(_buybackdoc.Rows[0].Field<string>("ith_loc"));
                        if (!string.IsNullOrEmpty(_adjno))
                        {
                            _header[0].Sah_del_loc = _buybackloc;
                            DataTable _referdoc = CHNLSVC.Inventory.CheckInwardDocumentUseStatus(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _adjno);
                            if (_referdoc != null)
                                if (_referdoc.Rows.Count > 0)
                                {
                                    string _referno = Convert.ToString(_referdoc.Rows[0].Field<string>("ith_doc_no"));
                                    DisplayMessage("The invoice having buy back return item which already out from the location refer document " + _referno + ", buy back inventory no " + _adjno);
                                    return;
                                }
                        }
                    }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
            List<InventoryHeader> _cancelDocument = null;
            try
            {
                DataTable _consignDocument = CHNLSVC.Inventory.GetConsginmentDocumentByInvoice(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtInvoiceNo.Text.Trim());
                if (_consignDocument != null)
                    if (_consignDocument.Rows.Count > 0)
                    {
                        foreach (DataRow _r in _consignDocument.Rows)
                        {
                            InventoryHeader _one = new InventoryHeader();
                            if (_cancelDocument == null) _cancelDocument = new List<InventoryHeader>();
                            string _type = _r["ith_doc_tp"] == DBNull.Value ? string.Empty : Convert.ToString(_r["ith_doc_tp"]);
                            string _document = _r["ith_doc_no"] == DBNull.Value ? string.Empty : Convert.ToString(_r["ith_doc_no"]);
                            bool _direction = _r["ith_direct"] == DBNull.Value ? false : Convert.ToBoolean(_r["ith_direct"]);
                            _one.Ith_doc_no = _document;
                            _one.Ith_doc_tp = _type;
                            _one.Ith_direct = _direction;
                            _cancelDocument.Add(_one);
                        }
                    }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
            try
            {
                string _msg = "";
                Int32 _effect = CHNLSVC.Sales.InvoiceCancelation(_header[0], out _msg, _cancelDocument);
                if (_effect == 1)
                {
                    _msg = "Successfully Canceled!";
                    DisplayMessage(_msg);
                }
                else
                {
                    //DisplayMessage("Error occurred while processing.");
                    DispMsg(_msg, "E");
                }

                Clear();
                //Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        private void LoadCancelPermission()
        {
            IsFwdSaleCancelAllowUser = false;
            IsDlvSaleCancelAllowUser = true;
            lbtncancel.Enabled = true;
            //string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            //if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10002))
            //{
            //    IsFwdSaleCancelAllowUser = true;
            //    lbtncancel.Enabled = true;
            //}
            //else
            //{
            //    IsFwdSaleCancelAllowUser = false;
            //    lbtncancel.Enabled = false;
            //}
            //if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10042))
            //{
            //    IsFwdSaleCancelAllowUser = true; IsDlvSaleCancelAllowUser = true;
            //    lbtncancel.Enabled = true;
            //}
            //else
            //{
            //    if (!IsFwdSaleCancelAllowUser)
            //    {
            //        IsDlvSaleCancelAllowUser = false;
            //        lbtncancel.Enabled = false;
            //    }
            //}
        }

        private void AssignInvoiceHeaderDetail(InvoiceHeader _hdr)
        {
            cmbInvType.Text = _hdr.Sah_inv_tp;
            txtdate.Text = _hdr.Sah_dt.ToString("dd/MM/yyyy"); ;
            txtCustomer.Text = _hdr.Sah_cus_cd;
            txtLoyalty.Text = _hdr.Sah_anal_6;
            SetLoyalityColor();
            _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCustomer.Text, string.Empty, string.Empty, "C");
            SetCustomerAndDeliveryDetails(true, _hdr);
            ViewCustomerAccountDetail(txtCustomer.Text);
            txtexcutive.Text = _hdr.Sah_sales_ex_cd;
            DataTable _recallemp = CHNLSVC.Sales.GetinvEmp(Session["UserCompanyCode"].ToString(), _hdr.Sah_sales_ex_cd);
            string _name = string.Empty;
            string _code = "";
            if (_recallemp != null && _recallemp.Rows.Count > 0)
            {
                _name = _recallemp.Rows[0].Field<string>("esep_first_name");
                _code = _recallemp.Rows[0].Field<string>("esep_epf");
            }
            //cmbExecutive.DataSource = null;
            //cmbExecutive.Items.Clear();
            //cmbExecutive.Items.Add(_name);
            if (cmbExecutive.Items.FindByValue(_code) != null)
            {
                cmbExecutive.SelectedValue = _code;
            }
            txtexcutive.Text = _code;
            lblSalesEx.Text = _name;
            lblcurrency.Text = _hdr.Sah_currency;
            txtcurrency.Text = _hdr.Sah_currency;

            txtManualRefNo.Text = _hdr.Sah_man_ref;
            chkTaxPayable.Checked = _hdr.Sah_tax_inv ? true : false;
            txtManualRefNo.Text = _hdr.Sah_man_ref;
            txtdocrefno.Text = _hdr.Sah_ref_doc;
            txtPoNo.Text = _hdr.Sah_anal_4;

            //Load inter company customer details 2016-02-03
            if (_masterBusinessCompany.Mbe_cd == null && !string.IsNullOrEmpty(_hdr.Sah_anal_4))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), _hdr.Sah_cus_cd, string.Empty, string.Empty, "S");
                SetCustomerAndDeliveryDetails(true, _hdr);
            }


            txtGroup.Text = _hdr.Sah_grup_cd;

            if (string.IsNullOrEmpty(_hdr.Sah_del_loc))
            {
                chkOpenDelivery.Checked = true;
            }
            else
            {
                chkOpenDelivery.Checked = false;
            }

            txtQuotation.Text = _hdr.Sah_structure_seq;

            txtPromotor.Text = _hdr.Sah_anal_1;

            if (_hdr.Sah_anal_1 != "")
            {
                cmbTechnician.SelectedValue = _hdr.Sah_anal_1;
            }
        }

        protected void lbtndelitem_Click(object sender, EventArgs e)
        {
            #region MyRegion
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label InvItm_JobLine = (Label)dr.FindControl("InvItm_JobLine");
                Label InvItm_ITMLINE = (Label)dr.FindControl("lblsad_itm_line");
                Label InvItm_ResNo = (Label)dr.FindControl("InvItm_ResNo");
                Label InvItm_ResLine = (Label)dr.FindControl("InvItm_ResLine");
                Label InvItm_Item = (Label)dr.FindControl("InvItm_Item");
                Label InvItm_UPrice = (Label)dr.FindControl("InvItm_UPrice");
                Label InvItm_Book = (Label)dr.FindControl("InvItm_Book");
                Label InvItm_Level = (Label)dr.FindControl("InvItm_Level");
                Label InvItm_Qty = (Label)dr.FindControl("InvItm_Qty");
                Label InvItm_DisAmt = (Label)dr.FindControl("InvItm_DisAmt");
                Label InvItm_TaxAmt = (Label)dr.FindControl("InvItm_TaxAmt");

                int jobline = Convert.ToInt32(InvItm_JobLine.Text.ToString());
                if (jobline > 0)
                {
                    InvItm_ITMLINE.Text = jobline.ToString();
                }

                Int32 rowIndex = dr.RowIndex;

                _isInventoryCombineAdded = false;

                if (hdfDeleteItem.Value != null && hdfDeleteItem.Value.ToString() == "1")
                {
                    if (_recieptItem != null)
                    {
                        if (_recieptItem.Count > 0)
                        {
                            DisplayMessage("You have already added the payment details to the grid");
                            return;
                        }
                        else
                        {

                        }
                    }
                    int _combineLine = Convert.ToInt32(InvItm_ITMLINE.Text);
                    int _combinelinereal = Convert.ToInt32(InvItm_JobLine.Text);
                    string _resNo = InvItm_ResNo.Text.Trim();
                    int _resLine = Convert.ToInt32(InvItm_ResLine.Text.Trim());//Add by Chamal 6-Jul-2014
                    if (_MainPriceSerial != null)
                        if (_MainPriceSerial.Count > 0)
                        {
                            string _item = InvItm_Item.Text.Trim();
                            decimal _uRate = Convert.ToDecimal(InvItm_UPrice.Text);
                            string _pbook = InvItm_Book.Text.Trim();
                            string _level = InvItm_Level.Text.Trim();

                            List<PriceSerialRef> _tempSerial = _MainPriceSerial;
                            var _remove = from _list in _tempSerial
                                          where _list.Sars_itm_cd == _item && _list.Sars_itm_price == _uRate && _list.Sars_pbook == _pbook && _list.Sars_price_lvl == _level
                                          select _list;

                            foreach (PriceSerialRef _single in _remove)
                            {
                                _tempSerial.Remove(_single);
                            }

                            _MainPriceSerial = _tempSerial;
                        }
                    List<InvoiceItem> _tempList = _invoiceItemList;
                    var _promo = (from _pro in _invoiceItemList
                                  where _pro.Sad_job_line == _combineLine
                                  select _pro).ToList();
                    if (_promo.Count() > 0)
                    {
                        foreach (InvoiceItem code in _promo)
                        {
                            CalculateGrandTotal(Convert.ToDecimal(code.Sad_qty), (decimal)code.Sad_unit_rt, (decimal)code.Sad_disc_amt, (decimal)code.Sad_itm_tax_amt, false);
                            ScanSerialList.RemoveAll(x => x.Tus_base_itm_line == code.Sad_itm_line);
                            InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == code.Sad_itm_line);
                        }
                        _tempList.RemoveAll(x => x.Sad_job_line == _combineLine);
                    }
                    else
                    {
                        CalculateGrandTotal(Convert.ToDecimal(InvItm_Qty.Text.Trim()), Convert.ToDecimal(InvItm_UPrice.Text), Convert.ToDecimal(InvItm_DisAmt.Text), Convert.ToDecimal(InvItm_TaxAmt.Text), false);
                        InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == _tempList[rowIndex].Sad_itm_line);
                        _tempList.RemoveAt(rowIndex);
                    }
                    _invoiceItemList = _tempList;


                    int _newLine = 1;
                    List<InvoiceItem> _tempLists = _invoiceItemList;
                    if (_tempLists != null)
                        if (_tempLists.Count > 0)
                        {
                            foreach (InvoiceItem _itm in _tempLists)
                            {
                                Int32 _line = _itm.Sad_itm_line;
                                _invoiceItemList.Where(Y => Y.Sad_itm_line == _line).ToList().ForEach(x => x.Sad_itm_line = _newLine);
                                InvoiceSerialList.Where(y => y.Sap_itm_line == _line).ToList().ForEach(x => x.Sap_itm_line = _newLine);
                                ScanSerialList.Where(y => y.Tus_base_itm_line == _line).ToList().ForEach(x => x.Tus_base_itm_line = _newLine);

                                _newLine += 1;
                            }
                            _lineNo = _newLine - 1;
                        }
                        else _lineNo = 0;
                    else _lineNo = 0;
                    BindAddItem();
                    if (_invoiceItemList != null || _invoiceItemList.Count > 0) lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt))); else lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0"));
                    gvPopSerial.DataSource = new List<ReptPickSerials>();
                    gvPopSerial.DataSource = setSerialStatusDescriptions(ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList());
                    gvPopSerial.DataBind();


                    //update promotion
                    //update promotion
                    List<InvoiceItem> _temItems = (from _invItm in _invoiceItemList
                                                   where !string.IsNullOrEmpty(_invItm.Sad_promo_cd)
                                                   select _invItm).ToList<InvoiceItem>();

                    //LookingForBuyBack();

                    //REGISTRATION PROCESS
                    //ADDED 2013/12/06
                    //search invoice item list if registration item not found set visibility
                    if (_isRegistrationMandatory && _isNeedRegistrationReciept)
                    {
                        bool _isHaveReg = false;
                        if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                        {
                            foreach (InvoiceItem _invItm in _invoiceItemList)
                            {
                                //check item need registration
                                MasterItem _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                                if (_itm != null)
                                {
                                    if (_itm.Mi_need_reg)
                                    {
                                        _isHaveReg = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (!_isHaveReg)
                        {
                            _isNeedRegistrationReciept = false;
                            lnkProcessRegistration.Visible = false;
                        }
                    }
                    //END
                    grvwarehouseitms.DataSource = new int[] { };
                    grvwarehouseitms.DataBind();
                    return;
                }

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
            #endregion

            #region MyRegion
            //try
            //{
            //    GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            //    Label popSer_Item = (Label)dr.FindControl("popSer_Item");
            //    Label popSer_SerialID = (Label)dr.FindControl("popSer_SerialID");
            //    Label popSer_UnitPrice = (Label)dr.FindControl("popSer_UnitPrice");
            //    Label popSer_BaseItemLine = (Label)dr.FindControl("popSer_BaseItemLine");
            //    Label popSer_Status = (Label)dr.FindControl("popSer_Status");
            //    Label popSer_Serial1 = (Label)dr.FindControl("popSer_Serial1");

            //    if (gvInvoiceItem.Rows.Count > 0)
            //    {
            //        Int32 _rowIndex = dr.RowIndex;
            //        if (_rowIndex != -1)
            //        {
            //            if (true)
            //            {
            //                if (_recieptItem != null)
            //                    if (_recieptItem.Count > 0)
            //                    {
            //                        DisplayMessage("You are already payment added!");
            //                        return;
            //                    }

            //                if (ScanSerialList != null)
            //                    if (ScanSerialList.Count > 0)
            //                    {
            //                        int row_id = _rowIndex;
            //                        string _item = popSer_Item.Text.Trim();
            //                        string _comline = popSer_SerialID.Text.Trim();
            //                        Int32 _combineLine;
            //                        if (string.IsNullOrEmpty(_comline))
            //                            _combineLine = -1;
            //                        else
            //                            _combineLine = Convert.ToInt32(popSer_SerialID.Text);
            //                        decimal uPrice = Convert.ToDecimal(popSer_UnitPrice.Text);
            //                        Int32 _invLine = Convert.ToInt32(popSer_BaseItemLine.Text);
            //                        string _combineStatus = popSer_Status.Text.Trim();
            //                        string _serialno = popSer_Serial1.Text.Trim();
            //                        if (_combineLine == -1)
            //                        {
            //                            var _invoicelst = _invoiceItemList.Where(x => x.Sad_itm_line == _invLine).ToList();
            //                            if (_invoicelst != null)
            //                                if (_invoicelst.Count > 0)
            //                                {
            //                                    foreach (InvoiceItem _itm in _invoicelst)
            //                                    {
            //                                        if (_itm.Sad_qty == 1)
            //                                        {
            //                                            CalculateGrandTotal(Convert.ToDecimal(1), (decimal)_itm.Sad_unit_rt, (decimal)_itm.Sad_disc_amt, (decimal)_itm.Sad_itm_tax_amt, false);
            //                                            _invoiceItemList.Remove(_itm);
            //                                            ScanSerialList.RemoveAll(x => x.Tus_ser_1 == _serialno);
            //                                            InvoiceSerialList.RemoveAll(x => x.Sap_ser_1 == _serialno);
            //                                        }
            //                                        else
            //                                        {
            //                                            InvoiceItem _myItem = new InvoiceItem();
            //                                            _myItem = _itm;
            //                                            decimal o_qty = _itm.Sad_qty;
            //                                            decimal o_unitprice = _itm.Sad_unit_rt;
            //                                            decimal o_unitamount = _itm.Sad_unit_amt;
            //                                            decimal o_tax = _itm.Sad_itm_tax_amt;
            //                                            decimal o_disamount = _itm.Sad_disc_amt;
            //                                            decimal o_disrate = _itm.Sad_disc_rt;
            //                                            decimal n_qty = 0;
            //                                            decimal n_unitprice = 0;
            //                                            decimal n_unitamount = 0;
            //                                            decimal n_tax = 0;
            //                                            decimal n_disamount = 0;
            //                                            decimal n_disrate = 0;
            //                                            decimal n_totalAmount = 0;
            //                                            n_qty = _itm.Sad_qty - 1;
            //                                            n_unitprice = _itm.Sad_unit_rt;
            //                                            n_unitamount = n_qty * n_unitprice;
            //                                            n_tax = (_itm.Sad_itm_tax_amt / _itm.Sad_qty) * n_qty;
            //                                            n_disamount = (_itm.Sad_disc_amt / _itm.Sad_qty) * n_qty;
            //                                            n_disrate = n_unitamount == 0 ? 0 : n_disamount / n_unitamount * 100;
            //                                            n_totalAmount = n_unitamount + n_tax - n_disamount;
            //                                            _itm.Sad_qty = n_qty;
            //                                            _itm.Sad_unit_amt = n_unitamount;
            //                                            _itm.Sad_itm_tax_amt = n_tax;
            //                                            _itm.Sad_disc_amt = n_disamount;
            //                                            _itm.Sad_disc_rt = n_disrate;
            //                                            _itm.Sad_tot_amt = n_totalAmount;
            //                                            CalculateGrandTotal(o_qty, o_unitprice, o_disamount, o_tax, false);
            //                                            _invoiceItemList.Remove(_myItem);
            //                                            CalculateGrandTotal(n_qty, n_unitprice, n_disamount, n_tax, true);
            //                                            _invoiceItemList.Add(_itm);
            //                                            InvoiceSerialList.RemoveAll(x => x.Sap_ser_1 == _serialno);
            //                                            ScanSerialList.RemoveAll(x => x.Tus_ser_1 == _serialno);
            //                                        }
            //                                    }
            //                                }
            //                        }
            //                        else
            //                        {
            //                            var _serLst = ScanSerialList.Where(x => x.Tus_serial_id == Convert.ToString(_combineLine)).ToList();
            //                            if (_serLst != null)
            //                                if (_serLst.Count > 0)
            //                                {
            //                                    foreach (ReptPickSerials _itms in _serLst)
            //                                    {
            //                                        Int32 _invoiceline = _itms.Tus_base_itm_line;
            //                                        var _invoiveLst = _invoiceItemList.Where(x => x.Sad_itm_line == _invoiceline).ToList();
            //                                        if (_invoiveLst != null)
            //                                            if (_invoiveLst.Count > 0)
            //                                            {
            //                                                foreach (InvoiceItem _itm in _invoiveLst)
            //                                                {
            //                                                    if (_itm.Sad_qty == 1)
            //                                                    {
            //                                                        CalculateGrandTotal(Convert.ToDecimal(1), (decimal)_itm.Sad_unit_rt, (decimal)_itm.Sad_disc_amt, (decimal)_itm.Sad_itm_tax_amt, false);
            //                                                        _invoiceItemList.Remove(_itm);
            //                                                    }
            //                                                    else
            //                                                    {
            //                                                        InvoiceItem _myItem = new InvoiceItem();
            //                                                        _myItem = _itm;
            //                                                        decimal o_qty = _itm.Sad_qty;
            //                                                        decimal o_unitprice = _itm.Sad_unit_rt;
            //                                                        decimal o_unitamount = _itm.Sad_unit_amt;
            //                                                        decimal o_tax = _itm.Sad_itm_tax_amt;
            //                                                        decimal o_disamount = _itm.Sad_disc_amt;
            //                                                        decimal o_disrate = _itm.Sad_disc_rt;
            //                                                        decimal n_qty = 0;
            //                                                        decimal n_unitprice = 0;
            //                                                        decimal n_unitamount = 0;
            //                                                        decimal n_tax = 0;
            //                                                        decimal n_disamount = 0;
            //                                                        decimal n_disrate = 0;
            //                                                        decimal n_totalAmount = 0;
            //                                                        n_qty = _itm.Sad_qty - 1;
            //                                                        n_unitprice = _itm.Sad_unit_rt;
            //                                                        n_unitamount = n_qty * n_unitprice;
            //                                                        n_tax = (_itm.Sad_itm_tax_amt / _itm.Sad_qty) * n_qty;
            //                                                        n_disamount = (_itm.Sad_disc_amt / _itm.Sad_qty) * n_qty;
            //                                                        n_disrate = n_unitamount == 0 ? 0 : n_disamount / n_unitamount * 100;
            //                                                        n_totalAmount = n_unitamount + n_tax - n_disamount;
            //                                                        _itm.Sad_qty = n_qty;
            //                                                        _itm.Sad_unit_amt = n_unitamount;
            //                                                        _itm.Sad_itm_tax_amt = n_tax;
            //                                                        _itm.Sad_disc_amt = n_disamount;
            //                                                        _itm.Sad_disc_rt = n_disrate;
            //                                                        _itm.Sad_tot_amt = n_totalAmount;
            //                                                        CalculateGrandTotal(o_qty, o_unitprice, o_disamount, o_tax, false);
            //                                                        _invoiceItemList.Remove(_myItem);
            //                                                        CalculateGrandTotal(n_qty, n_unitprice, n_disamount, n_tax, true);
            //                                                        _invoiceItemList.Add(_itm);
            //                                                    }
            //                                                }
            //                                            }
            //                                    }
            //                                    ScanSerialList.RemoveAll(x => x.Tus_serial_id == Convert.ToString(_combineLine));
            //                                    InvoiceSerialList.RemoveAll(x => x.Sap_ser_line == Convert.ToInt32(_combineLine));
            //                                }
            //                        }

            //                        Int32 _newLine = 1;
            //                        List<InvoiceItem> _tempLists = _invoiceItemList;
            //                        if (_tempLists != null)
            //                            if (_tempLists.Count > 0)
            //                            {
            //                                foreach (InvoiceItem _itm in _tempLists)
            //                                {
            //                                    Int32 _line = _itm.Sad_itm_line;
            //                                    _invoiceItemList.Where(Y => Y.Sad_itm_line == _line).ToList().ForEach(x => x.Sad_itm_line = _newLine);
            //                                    InvoiceSerialList.Where(y => y.Sap_itm_line == _line).ToList().ForEach(x => x.Sap_itm_line = _newLine);
            //                                    ScanSerialList.Where(y => y.Tus_base_itm_line == _line).ToList().ForEach(x => x.Tus_base_itm_line = _newLine);
            //                                    _newLine += 1;
            //                                }
            //                                _lineNo = _newLine - 1;
            //                            }
            //                            else
            //                            {
            //                                _lineNo = 0;
            //                            }
            //                        else
            //                        {
            //                            _lineNo = 0;
            //                        }
            //                        gvPopSerial.DataSource = new List<ReptPickSerials>();
            //                        gvPopSerial.DataBind();
            //                        gvInvoiceItem.DataSource = new List<InvoiceItem>();
            //                        gvInvoiceItem.DataBind();

            //                        if (ScanSerialList != null)
            //                            if (ScanSerialList.Count > 0)
            //                            {
            //                                gvPopSerial.DataSource = ScanSerialList.Where(X => X.Tus_ser_1 != "N/A" && !IsGiftVoucher(X.ItemType)).ToList();
            //                                gvPopSerial.DataBind();

            //                                gvGiftVoucher.DataSource = new List<ReptPickSerials>();
            //                                gvGiftVoucher.DataSource = ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList();
            //                                gvGiftVoucher.DataBind();

            //                                gvInvoiceItem.DataSource = _invoiceItemList;
            //                                gvInvoiceItem.DataBind();
            //                            }
            //                            else
            //                            {
            //                                gvInvoiceItem.DataSource = _invoiceItemList;
            //                                gvInvoiceItem.DataBind();
            //                            }
            //                    }
            //            }
            //            //else
            //            //{
            //            //    string _item = popSer_Item.Text.Trim();
            //            //    string _serialno = popSer_Serial1.Text.Trim();
            //            //    Int32 _serialid = 0;
            //            //    if (!string.IsNullOrEmpty(_serialno)) _serialid = ScanSerialList.Where(x => x.Tus_itm_cd == _item && x.Tus_ser_1 == _serialno).Select(x => x.Tus_ser_id).ToList()[0];
            //            //    List<InventoryWarrantySubDetail> dt = CHNLSVC.Inventory.GetSubItemSerials(_item, _serialno, _serialid);
            //            //    if (dt != null)
            //            //        if (dt.Count > 0)
            //            //        {
            //            //            var _lst = new BindingList<InventoryWarrantySubDetail>(dt);
            //            //            gvSubSerial.AutoGenerateColumns = false;
            //            //            gvSubSerial.DataSource = _lst;
            //            //            if (_lst != null) if (_lst.Count > 0) pnlSubSerial.Visible = true;
            //            //                else pnlSubSerial.Visible = false;
            //            //        }
            //            //}
            //        }
            //        LookingForBuyBack();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    DisplayMessage(ex.Message);
            //}
            //finally
            //{
            //    if (_invoiceItemList != null || _invoiceItemList.Count > 0)
            //        lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt)));
            //    else
            //        lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0"));
            //    ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
            //    ucPayModes1.LoadData();
            //    CHNLSVC.CloseAllChannels();
            //} 
            #endregion


        }

        protected void btnDelSerials(object sender, EventArgs e)
        {
            #region MyRegion
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label popSer_Item = (Label)dr.FindControl("popSer_Item");
                Label popSer_SerialID = (Label)dr.FindControl("popSer_SerialID");
                Label popSer_UnitPrice = (Label)dr.FindControl("popSer_UnitPrice");
                Label popSer_BaseItemLine = (Label)dr.FindControl("popSer_BaseItemLine");
                Label popSer_Status = (Label)dr.FindControl("popSer_Status");
                Label popSer_Serial1 = (Label)dr.FindControl("popSer_Serial1");

                if (gvInvoiceItem.Rows.Count > 0)
                {
                    Int32 _rowIndex = dr.RowIndex;
                    if (_rowIndex != -1)
                    {
                        if (true)
                        {
                            if (_recieptItem != null)
                            {
                                if (_recieptItem.Count > 0)
                                {
                                    DisplayMessage("You are already payment added!");
                                    return;
                                }
                                else
                                {

                                }
                            }

                            if (ScanSerialList != null)
                                if (ScanSerialList.Count > 0)
                                {
                                    int row_id = _rowIndex;
                                    string _item = popSer_Item.Text.Trim();
                                    string _comline = popSer_SerialID.Text.Trim();
                                    Int32 _combineLine;
                                    if (string.IsNullOrEmpty(_comline))
                                        _combineLine = -1;
                                    else
                                        _combineLine = Convert.ToInt32(popSer_SerialID.Text);
                                    decimal uPrice = Convert.ToDecimal(popSer_UnitPrice.Text);
                                    Int32 _invLine = Convert.ToInt32(popSer_BaseItemLine.Text);
                                    string _combineStatus = popSer_Status.Text.Trim();
                                    string _serialno = popSer_Serial1.Text.Trim();
                                    if (_combineLine == -1)
                                    {
                                        var _invoicelst = _invoiceItemList.Where(x => x.Sad_itm_line == _invLine).ToList();
                                        if (_invoicelst != null)
                                            if (_invoicelst.Count > 0)
                                            {
                                                foreach (InvoiceItem _itm in _invoicelst)
                                                {
                                                    if (_itm.Sad_qty == 1)
                                                    {
                                                        CalculateGrandTotal(Convert.ToDecimal(1), (decimal)_itm.Sad_unit_rt, (decimal)_itm.Sad_disc_amt, (decimal)_itm.Sad_itm_tax_amt, false);
                                                        _invoiceItemList.Remove(_itm);
                                                        ScanSerialList.RemoveAll(x => x.Tus_ser_1 == _serialno);
                                                        InvoiceSerialList.RemoveAll(x => x.Sap_ser_1 == _serialno);
                                                    }
                                                    else
                                                    {
                                                        InvoiceItem _myItem = new InvoiceItem();
                                                        _myItem = _itm;
                                                        decimal o_qty = _itm.Sad_qty;
                                                        decimal o_unitprice = _itm.Sad_unit_rt;
                                                        decimal o_unitamount = _itm.Sad_unit_amt;
                                                        decimal o_tax = _itm.Sad_itm_tax_amt;
                                                        decimal o_disamount = _itm.Sad_disc_amt;
                                                        decimal o_disrate = _itm.Sad_disc_rt;
                                                        decimal n_qty = 0;
                                                        decimal n_unitprice = 0;
                                                        decimal n_unitamount = 0;
                                                        decimal n_tax = 0;
                                                        decimal n_disamount = 0;
                                                        decimal n_disrate = 0;
                                                        decimal n_totalAmount = 0;
                                                        n_qty = _itm.Sad_qty - 1;
                                                        n_unitprice = _itm.Sad_unit_rt;
                                                        n_unitamount = n_qty * n_unitprice;
                                                        n_tax = (_itm.Sad_itm_tax_amt / _itm.Sad_qty) * n_qty;
                                                        n_disamount = (_itm.Sad_disc_amt / _itm.Sad_qty) * n_qty;
                                                        n_disrate = n_unitamount == 0 ? 0 : n_disamount / n_unitamount * 100;
                                                        n_totalAmount = n_unitamount + n_tax - n_disamount;
                                                        _itm.Sad_qty = n_qty;
                                                        _itm.Sad_unit_amt = n_unitamount;
                                                        _itm.Sad_itm_tax_amt = n_tax;
                                                        _itm.Sad_disc_amt = n_disamount;
                                                        _itm.Sad_disc_rt = n_disrate;
                                                        _itm.Sad_tot_amt = n_totalAmount;
                                                        CalculateGrandTotal(o_qty, o_unitprice, o_disamount, o_tax, false);
                                                        _invoiceItemList.Remove(_myItem);
                                                        CalculateGrandTotal(n_qty, n_unitprice, n_disamount, n_tax, true);
                                                        _invoiceItemList.Add(_itm);
                                                        InvoiceSerialList.RemoveAll(x => x.Sap_ser_1 == _serialno);
                                                        ScanSerialList.RemoveAll(x => x.Tus_ser_1 == _serialno);
                                                    }
                                                }
                                            }
                                    }
                                    else
                                    {
                                        var _serLst = ScanSerialList.Where(x => x.Tus_serial_id == Convert.ToString(_combineLine)).ToList();
                                        if (_serLst != null)
                                            if (_serLst.Count > 0)
                                            {
                                                foreach (ReptPickSerials _itms in _serLst)
                                                {
                                                    Int32 _invoiceline = _itms.Tus_base_itm_line;
                                                    var _invoiveLst = _invoiceItemList.Where(x => x.Sad_itm_line == _invoiceline).ToList();
                                                    if (_invoiveLst != null)
                                                        if (_invoiveLst.Count > 0)
                                                        {
                                                            foreach (InvoiceItem _itm in _invoiveLst)
                                                            {
                                                                if (_itm.Sad_qty == 1)
                                                                {
                                                                    CalculateGrandTotal(Convert.ToDecimal(1), (decimal)_itm.Sad_unit_rt, (decimal)_itm.Sad_disc_amt, (decimal)_itm.Sad_itm_tax_amt, false);
                                                                    _invoiceItemList.Remove(_itm);
                                                                }
                                                                else
                                                                {
                                                                    InvoiceItem _myItem = new InvoiceItem();
                                                                    _myItem = _itm;
                                                                    decimal o_qty = _itm.Sad_qty;
                                                                    decimal o_unitprice = _itm.Sad_unit_rt;
                                                                    decimal o_unitamount = _itm.Sad_unit_amt;
                                                                    decimal o_tax = _itm.Sad_itm_tax_amt;
                                                                    decimal o_disamount = _itm.Sad_disc_amt;
                                                                    decimal o_disrate = _itm.Sad_disc_rt;
                                                                    decimal n_qty = 0;
                                                                    decimal n_unitprice = 0;
                                                                    decimal n_unitamount = 0;
                                                                    decimal n_tax = 0;
                                                                    decimal n_disamount = 0;
                                                                    decimal n_disrate = 0;
                                                                    decimal n_totalAmount = 0;
                                                                    n_qty = _itm.Sad_qty - 1;
                                                                    n_unitprice = _itm.Sad_unit_rt;
                                                                    n_unitamount = n_qty * n_unitprice;
                                                                    n_tax = (_itm.Sad_itm_tax_amt / _itm.Sad_qty) * n_qty;
                                                                    n_disamount = (_itm.Sad_disc_amt / _itm.Sad_qty) * n_qty;
                                                                    n_disrate = n_unitamount == 0 ? 0 : n_disamount / n_unitamount * 100;
                                                                    n_totalAmount = n_unitamount + n_tax - n_disamount;
                                                                    _itm.Sad_qty = n_qty;
                                                                    _itm.Sad_unit_amt = n_unitamount;
                                                                    _itm.Sad_itm_tax_amt = n_tax;
                                                                    _itm.Sad_disc_amt = n_disamount;
                                                                    _itm.Sad_disc_rt = n_disrate;
                                                                    _itm.Sad_tot_amt = n_totalAmount;
                                                                    CalculateGrandTotal(o_qty, o_unitprice, o_disamount, o_tax, false);
                                                                    _invoiceItemList.Remove(_myItem);
                                                                    CalculateGrandTotal(n_qty, n_unitprice, n_disamount, n_tax, true);
                                                                    _invoiceItemList.Add(_itm);
                                                                }
                                                            }
                                                        }
                                                }
                                                ScanSerialList.RemoveAll(x => x.Tus_serial_id == Convert.ToString(_combineLine));
                                                InvoiceSerialList.RemoveAll(x => x.Sap_ser_line == Convert.ToInt32(_combineLine));
                                            }
                                    }

                                    Int32 _newLine = 1;
                                    List<InvoiceItem> _tempLists = _invoiceItemList;
                                    if (_tempLists != null)
                                        if (_tempLists.Count > 0)
                                        {
                                            foreach (InvoiceItem _itm in _tempLists)
                                            {
                                                Int32 _line = _itm.Sad_itm_line;
                                                _invoiceItemList.Where(Y => Y.Sad_itm_line == _line).ToList().ForEach(x => x.Sad_itm_line = _newLine);
                                                InvoiceSerialList.Where(y => y.Sap_itm_line == _line).ToList().ForEach(x => x.Sap_itm_line = _newLine);
                                                ScanSerialList.Where(y => y.Tus_base_itm_line == _line).ToList().ForEach(x => x.Tus_base_itm_line = _newLine);
                                                _newLine += 1;
                                            }
                                            _lineNo = _newLine - 1;
                                        }
                                        else
                                        {
                                            _lineNo = 0;
                                        }
                                    else
                                    {
                                        _lineNo = 0;
                                    }
                                    gvPopSerial.DataSource = new List<ReptPickSerials>();
                                    gvPopSerial.DataBind();
                                    gvInvoiceItem.DataSource = new List<InvoiceItem>();
                                    gvInvoiceItem.DataBind();

                                    if (ScanSerialList != null)
                                        if (ScanSerialList.Count > 0)
                                        {
                                            gvPopSerial.DataSource = setSerialStatusDescriptions(ScanSerialList.Where(X => X.Tus_ser_1 != "N/A" && !IsGiftVoucher(X.ItemType)).ToList());
                                            gvPopSerial.DataBind();


                                            gvInvoiceItem.DataSource = setItemDescriptions(_invoiceItemList);
                                            gvInvoiceItem.DataBind();
                                        }
                                        else
                                        {
                                            gvInvoiceItem.DataSource = setItemDescriptions(_invoiceItemList);
                                            gvInvoiceItem.DataBind();
                                        }
                                }
                        }
                        //else
                        //{
                        //    string _item = popSer_Item.Text.Trim();
                        //    string _serialno = popSer_Serial1.Text.Trim();
                        //    Int32 _serialid = 0;
                        //    if (!string.IsNullOrEmpty(_serialno)) _serialid = ScanSerialList.Where(x => x.Tus_itm_cd == _item && x.Tus_ser_1 == _serialno).Select(x => x.Tus_ser_id).ToList()[0];
                        //    List<InventoryWarrantySubDetail> dt = CHNLSVC.Inventory.GetSubItemSerials(_item, _serialno, _serialid);
                        //    if (dt != null)
                        //        if (dt.Count > 0)
                        //        {
                        //            var _lst = new BindingList<InventoryWarrantySubDetail>(dt);
                        //            gvSubSerial.AutoGenerateColumns = false;
                        //            gvSubSerial.DataSource = _lst;
                        //            if (_lst != null) if (_lst.Count > 0) pnlSubSerial.Visible = true;
                        //                else pnlSubSerial.Visible = false;
                        //        }
                        //}
                    }
                    LookingForBuyBack();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
            finally
            {
                if (_invoiceItemList != null || _invoiceItemList.Count > 0)
                    lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt)));
                else
                    lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0"));

                CHNLSVC.CloseAllChannels();
            }
            #endregion
        }

        protected void lnkProcessRegistration_Click(object sender, EventArgs e)
        {

        }

        protected void chkDeliverLater_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isGiftVoucherCheckBoxClick)
                    return;
                txtdellocation.Text = Session["UserDefLoca"].ToString();
                chkOpenDelivery.Checked = false;

                chkOpenDelivery.Enabled = false;
                txtdellocation.Enabled = false;
                // btnSearchDelLocation.Enabled = false;

                //chkOpenDelivery.CssClass = "buttoncolor";
                //txtdellocation.CssClass = "buttoncolor";
                //btnSearchDelLocation.CssClass = "buttoncolor";

                chkDeliverNow.Enabled = true;
                //chkDeliverNow.CssClass = "buttoncolor";

                chkDeliverNow.Checked = false;



            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected void btndelcuscode_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CustomerDel";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
                txtdelcuscode.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void btnSearchDelLocation_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "LocationDel";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
                txtdellocation.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void txtDisRate_TextChanged(object sender, EventArgs e)
        {

            if (_IsVirtualItem)
            {
                txtDisRate.Text = "";
                txtDisAmt.Text = "";
                txtDisAmt.Text = FormatToCurrency("0");
                txtDisRate.Text = FormatToCurrency("0");
                return;
            }
            try
            {


                if (Convert.ToDecimal(txtDisRate.Text.Trim()) > 100)
                {
                    // using (new CenterWinDialog(this))
                    //{ 
                    DisplayMessage("Discount Rate should be less than 100%!", 1);
                    // MessageBox.Show("Discount Rate should be less than 100%!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    // }
                    txtDisRate.Text = string.Empty;
                    txtDisRate.Text = FormatToQty("0");
                    return;
                }
                if (Convert.ToDecimal(txtDisRate.Text.Trim()) < 0)
                {
                    //MessageBox.Show("Discount rate should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtDisRate.Clear();
                    //txtDisRate.Text = FormatToQty("0");
                    //return;
                }

                #region add by lakshan 13Sep2017
                bool _supDisAva = false;
                if (!string.IsNullOrEmpty(txtCustomer.Text) && Session["UserCompanyCode"].ToString() == "AAL")
                {
                    List<CashGeneralEntiryDiscountDef> _disList = CHNLSVC.Sales.GetGeneralEntityDiscountDef(Session["UserCompanyCode"].ToString(),
                        Session["UserDefProf"].ToString(),
                        Convert.ToDateTime(txtdate.Text.Trim()).Date,
                        cmbBook.Text.Trim(),
                        cmbLevel.Text.Trim());
                    if (_disList != null)
                    {
                        var _custDis = _disList.Where(c => c.Sgdd_cust_cd == txtCustomer.Text.Trim().ToUpper()).FirstOrDefault();
                        if (_custDis != null)
                        {
                            _supDisAva = true;
                        }
                    }
                }
                #endregion
                _CashPromotionDiscountDetail = new CashPromotionDiscountDetail();
                _CashPromotionDiscountDetail = CHNLSVC.Sales.GetitemDiscount("PC", Session["UserDefProf"].ToString(), txtItem.Text.ToUpper(), cmbInvType.Text, Convert.ToDateTime(txtdate.Text), cmbBook.Text.Trim(), cmbLevel.Text.Trim(), Convert.ToDecimal(txtQty.Text));
                if (_CashPromotionDiscountDetail != null)
                {
                    if (string.IsNullOrEmpty(txtItem.Text))
                        return;
                    if (IsNumeric(txtQty.Text) == false)
                    {
                        DisplayMessage("Please select the valid quantity");
                        return;
                    }
                    if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;

                    if (!string.IsNullOrEmpty(txtDisRate.Text) && _isEditPrice == false)
                    {
                        decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
                        decimal rates = _CashPromotionDiscountDetail.Spdd_disc_rt;
                        if (rates < _disRate)
                        {
                            CalculateItem();
                            string msgText = "Exceeds maximum discount allowed " + rates + "% !." + _disRate + "% discounted price is " + txtLineTotAmt.Text;
                            DisplayMessage(msgText);
                            txtDisRate.Text = FormatToCurrency("0");
                            CalculateItem();
                            _isEditDiscount = false;
                            return;
                        }
                        else
                        {
                            CalculateItem();
                            _isEditDiscount = true;
                        }
                    }

                    //CheckNewDiscountRate();
                }
                /*
                if (_isCompleteCode && GeneralDiscount_new.SADD_IS_EDIT==1 && Convert.ToDecimal(txtDisRate.Text.Trim()) > 0)
                {

                    // using (new CenterWinDialog(this)) 
                    // { 
                    DisplayMessage("You are not allow discount for com codes!", 1);
                    // MessageBox.Show("You are not allow discount for com codes!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    // }
                    txtDisRate.Text = string.Empty;
                    txtDisRate.Text = FormatToQty("0");
                    return;
                }
                  
                CheckNewDiscountRate();
                 * */

                //if (_isCompleteCode && (_docPriceDefnforprofitcentr.SADD_EDIT_RT==0) && Convert.ToDecimal(txtDisRate.Text.Trim()) > 0)
                //{

                //    // using (new CenterWinDialog(this)) 
                //    // { 
                //    DisplayMessage("You are not allow discount for com codes!", 1);
                //    // MessageBox.Show("You are not allow discount for com codes!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                //    // }
                //    txtDisRate.Text = string.Empty;
                //    txtDisRate.Text = FormatToQty("0");
                //    return;
                //}
                else if (_supDisAva)
                {
                    if (string.IsNullOrEmpty(txtItem.Text))
                        return;
                    if (IsNumeric(txtQty.Text) == false)
                    {
                        DisplayMessage("Please select the valid quantity");
                        return;
                    }
                    if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return;

                    if (!string.IsNullOrEmpty(txtDisRate.Text) && _isEditPrice == false)
                    {
                        decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
                        decimal rates = 0;
                        List<CashGeneralEntiryDiscountDef> _disList = CHNLSVC.Sales.GetGeneralEntityDiscountDef(Session["UserCompanyCode"].ToString(),
                            Session["UserDefProf"].ToString(),
                            Convert.ToDateTime(txtdate.Text.Trim()).Date,
                            cmbBook.Text.Trim(),
                            cmbLevel.Text.Trim());
                        if (_disList != null)
                        {
                            var _custDis = _disList.Where(c => c.Sgdd_cust_cd == txtCustomer.Text.Trim().ToUpper()).FirstOrDefault();
                            if (_custDis != null)
                            {
                                rates = _custDis.Sgdd_disc_rt;
                            }
                        }
                        if (rates < _disRate)
                        {
                            CalculateItem();
                            string msgText = "Exceeds maximum discount allowed " + rates + "% !." + _disRate + "% discounted price is " + txtLineTotAmt.Text;
                            DisplayMessage(msgText);
                            txtDisRate.Text = FormatToCurrency("0");
                            CalculateItem();
                            _isEditDiscount = false;
                            return;
                        }
                        else
                        {
                            CalculateItem();
                            _isEditDiscount = true;
                        }
                    }
                }
                else
                {
                    CheckNewDiscountRate_New();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void txtDisAmt_TextChanged(object sender, EventArgs e)
        {

            if (_IsVirtualItem)
            {
                txtDisRate.Text = "";
                txtDisAmt.Text = "";
                txtDisAmt.Text = FormatToCurrency("0");
                txtDisRate.Text = FormatToCurrency("0");
                return;
            }
            try
            {
                if (string.IsNullOrEmpty(txtDisAmt.Text))
                    return;
                if (Convert.ToDecimal(txtDisAmt.Text) < 0)
                {
                }
                CheckNewDiscountAmount();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        protected bool CheckNewDiscountRate()
        {
            if (string.IsNullOrEmpty(txtItem.Text))
                return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                DisplayMessage("Please select the valid quantity");
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;

            if (!string.IsNullOrEmpty(txtDisRate.Text) && _isEditPrice == false)
            {
                decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
                bool _IsPromoVou = false;
                if (_disRate > 0)
                {
                    if (GeneralDiscount == null)
                        GeneralDiscount = new CashGeneralEntiryDiscountDef();

                    GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToDateTime(txtdate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);

                    if (GeneralDiscount != null && (GeneralDiscount.Sgdd_disc_val != 00 || GeneralDiscount.Sgdd_disc_rt != 00))
                    {
                        decimal vals = GeneralDiscount.Sgdd_disc_val;
                        decimal rates = GeneralDiscount.Sgdd_disc_rt;

                        if (lblPromoVouUsedFlag.Text.Contains("U") == true)
                        {
                            DisplayMessage("Voucher already used!");
                            txtDisRate.Text = FormatToCurrency("0");
                            _isEditDiscount = false;
                            return false;
                        }

                        if (_IsPromoVou == true)
                        {
                            if (rates == 0 && vals > 0)
                            {
                                CalculateItem();
                                if (Convert.ToDecimal(txtDisAmt.Text) > vals)
                                {
                                    DisplayMessage("Voucher discount amount should be " + vals + ".Not allowed discount rate " + _disRate + "%");
                                    txtDisRate.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }
                            else
                            {
                                if (rates != _disRate)
                                {
                                    CalculateItem();
                                    DisplayMessage("Voucher discount rate should be " + rates + "% !.Not allowed discount rate " + _disRate + "% discounted price is " + txtLineTotAmt.Text);
                                    txtDisRate.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (rates < _disRate)
                            {
                                CalculateItem();
                                string msgText = "Exceeds maximum discount allowed " + rates + "% !." + _disRate + "% discounted price is " + txtLineTotAmt.Text;
                                DisplayMessage(msgText);
                                txtDisRate.Text = FormatToCurrency("0");
                                CalculateItem();
                                _isEditDiscount = false;
                                return false;
                            }
                            else
                            {
                                _isEditDiscount = true;
                            }
                        }
                    }
                    else
                    {
                        DisplayMessage("You are not allow for apply discount");
                        txtDisRate.Text = FormatToCurrency("0");
                        _isEditDiscount = false;
                        return false;
                    }

                    if (_isEditDiscount == true)
                    {
                        if (_IsPromoVou == true)
                        {
                            _proVouInvcItem = txtItem.Text.ToUpper().ToString();
                        }
                    }
                }
                else
                    _isEditDiscount = false;
            }
            else if (_isEditPrice)
            {
                txtDisRate.Text = FormatToCurrency("0");
            }
            if (string.IsNullOrEmpty(txtDisRate.Text)) txtDisRate.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisRate.Text);
            txtDisRate.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            //lbtnadditems.Focus();
            return true;
        }
        protected bool CheckNewDiscountRate_New()
        {
            if (string.IsNullOrEmpty(txtItem.Text))
                return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                DisplayMessage("Please select the valid quantity");
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;

            if (!string.IsNullOrEmpty(txtDisRate.Text) && _isEditPrice == false)
            {
                decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
                bool _IsPromoVou = false;
                if (_disRate > 0)
                {
                    if (GeneralDiscount_new == null)
                        GeneralDiscount_new = new SarDocumentPriceDefn();

                    GeneralDiscount_new = CHNLSVC.Sales.GetDocPriceDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbBook.Text.Trim(), cmbLevel.Text.Trim(), cmbInvType.Text);

                    if (_isCompleteCode && (GeneralDiscount_new.SADD_EDIT_RT == 0) && Convert.ToDecimal(txtDisRate.Text.Trim()) > 0)
                    {

                        // using (new CenterWinDialog(this)) 
                        // { 
                        DisplayMessage("You are not allow discount for com codes!", 1);
                        // MessageBox.Show("You are not allow discount for com codes!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                        // }
                        txtDisRate.Text = string.Empty;
                        txtDisRate.Text = FormatToQty("0");
                        return false;
                    }
                    if (GeneralDiscount_new != null && (GeneralDiscount_new.SADD_DISC_RT != 00))
                    {
                        decimal vals = 0; //GeneralDiscount_new.Sgdd_disc_val;
                        decimal rates = GeneralDiscount_new.SADD_DISC_RT;

                        if (lblPromoVouUsedFlag.Text.Contains("U") == true)
                        {
                            DisplayMessage("Voucher already used!");
                            txtDisRate.Text = FormatToCurrency("0");
                            _isEditDiscount = false;
                            return false;
                        }

                        if (_IsPromoVou == true)
                        {
                            if (rates == 0 && vals > 0)
                            {
                                CalculateItem();
                                if (Convert.ToDecimal(txtDisAmt.Text) > vals)
                                {
                                    DisplayMessage("Voucher discount amount should be " + vals + ".Not allowed discount rate " + _disRate + "%");
                                    txtDisRate.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }
                            else
                            {
                                if (rates != _disRate)
                                {
                                    CalculateItem();
                                    DisplayMessage("Voucher discount rate should be " + rates + "% !.Not allowed discount rate " + _disRate + "% discounted price is " + txtLineTotAmt.Text);
                                    txtDisRate.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (rates < _disRate)
                            {
                                CalculateItem();
                                string msgText = "Exceeds maximum discount allowed " + rates + "% !." + _disRate + "% discounted price is " + txtLineTotAmt.Text;
                                DisplayMessage(msgText);
                                txtDisRate.Text = FormatToCurrency("0");
                                CalculateItem();
                                _isEditDiscount = false;
                                return false;
                            }
                            else
                            {
                                _isEditDiscount = true;
                            }
                        }
                    }
                    else
                    {
                        DisplayMessage("You are not allow for apply discount");
                        txtDisRate.Text = FormatToCurrency("0");
                        _isEditDiscount = false;
                        return false;
                    }

                    if (_isEditDiscount == true)
                    {
                        if (_IsPromoVou == true)
                        {
                            _proVouInvcItem = txtItem.Text.ToUpper().ToString();
                        }
                    }
                }
                else
                    _isEditDiscount = false;
            }
            else if (_isEditPrice)
            {
                txtDisRate.Text = FormatToCurrency("0");
            }
            if (string.IsNullOrEmpty(txtDisRate.Text)) txtDisRate.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisRate.Text);
            txtDisRate.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            //lbtnadditems.Focus();
            return true;
        }

        private bool CheckNewDiscountAmount()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                DisplayMessage("Please select the valid quantity");
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;
            if (!string.IsNullOrEmpty(txtDisAmt.Text) && _isEditPrice == false && !string.IsNullOrEmpty(txtQty.Text))
            {
                decimal _disAmt = Convert.ToDecimal(txtDisAmt.Text);
                decimal _uRate = Convert.ToDecimal(txtUnitPrice.Text);
                decimal _qty = Convert.ToDecimal(txtQty.Text);
                decimal _totAmt = _uRate * _qty;
                decimal _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;

                if (_disAmt > 0)
                {
                    if (GeneralDiscount != null && (GeneralDiscount.Sgdd_disc_val != 00 || GeneralDiscount.Sgdd_disc_rt != 00))
                    {
                        decimal vals = GeneralDiscount.Sgdd_disc_val;
                        decimal rates = GeneralDiscount.Sgdd_disc_rt;

                        if (vals < _disAmt && rates == 0)
                        {
                            DisplayMessage("You cannot discount price more than " + vals + ".");
                            txtDisAmt.Text = FormatToCurrency("0");
                            txtDisRate.Text = FormatToCurrency("0");
                            _isEditDiscount = false;
                            return false;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = "0";
                            CalculateItem();
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) _percent = _totAmt != 0 ? (_disAmt / Convert.ToDecimal(txtLineTotAmt.Text)) * 100 : 0;
                            if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = FormatToCurrency(Convert.ToString(_percent));
                            CalculateItem();
                            CheckNewDiscountRate();
                            _isEditDiscount = true;
                        }
                    }
                    else
                    {
                        if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
                        bool _IsPromoVou = false;
                        // if (string.IsNullOrEmpty(lblPromoVouNo.Text))
                        // {
                        GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToDateTime(txtdate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCustomer.Text.Trim(), txtItem.Text.Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                        // }
                        //else
                        //{
                        //    GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCustomer.Text.Trim(), Convert.ToDateTime(txtdate.Text.Trim()).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtItem.Text.Trim(), lblPromoVouNo.Text.Trim());
                        //    if (GeneralDiscount != null && (GeneralDiscount.Sgdd_disc_val != 00 || GeneralDiscount.Sgdd_disc_rt != 00))
                        //    {
                        //        _IsPromoVou = true;
                        //       // GeneralDiscount.Sgdd_seq = Convert.ToInt32(lblPromoVouNo.Text);
                        //    }
                        //}

                        if (GeneralDiscount != null && (GeneralDiscount.Sgdd_disc_val != 00 || GeneralDiscount.Sgdd_disc_rt != 00))
                        {
                            decimal vals = GeneralDiscount.Sgdd_disc_val;
                            decimal rates = GeneralDiscount.Sgdd_disc_rt;

                            if (_IsPromoVou == true)
                            {
                                if (vals < _disAmt && rates == 0)
                                {
                                    DisplayMessage("Voucher discount amount should be " + vals + "!./nNot allowed discount amount " + _disAmt + " discounted price is " + txtLineTotAmt.Text);
                                    txtDisAmt.Text = FormatToCurrency("0");
                                    txtDisRate.Text = FormatToCurrency("0");
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }

                            if (vals < _disAmt && rates == 0)
                            {
                                DisplayMessage("You cannot discount price more than " + vals + ".");
                                txtDisAmt.Text = FormatToCurrency("0");
                                txtDisRate.Text = FormatToCurrency("0");
                                _isEditDiscount = false;
                                return false;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = "0";
                                CalculateItem();
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) _percent = _totAmt != 0 ? (_disAmt / _totAmt) * 100 : 0;
                                if (!string.IsNullOrEmpty(txtDisRate.Text) && Convert.ToDecimal(txtDisRate.Text) == 0) txtDisRate.Text = FormatToCurrency(Convert.ToString(_percent));
                                CalculateItem();
                                CheckNewDiscountRate();
                                _isEditDiscount = true;
                            }
                        }
                        else
                        {
                            DisplayMessage("You are not allow for discount");
                            txtDisAmt.Text = FormatToCurrency("0");
                            txtDisRate.Text = FormatToCurrency("0");
                            _isEditDiscount = false;
                            return false;
                        }
                    }
                }
                else
                    _isEditDiscount = false;
            }
            else if (_isEditPrice)
            {
                txtDisAmt.Text = FormatToCurrency("0");
                txtDisRate.Text = FormatToCurrency("0");
            }

            if (string.IsNullOrEmpty(txtDisAmt.Text))
                txtDisAmt.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisAmt.Text);
            txtDisAmt.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            return true;
        }

        protected void btnPromoVou_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPromoVouNo_new.Text))
            {
                int val;
                if (!int.TryParse(txtPromoVouNo_new.Text, out val))
                {
                    MPPV.Show();
                    DisplayMessage("Invalid Voucher Number", 1);
                    txtPromoVouNo_new.Text = "";
                    txtPromoVouNo_new.Focus();
                    return;
                }

                string _vouMsg = string.Empty;
                if (CHNLSVC.Sales.CheckPromoVoucherNo(Session["UserCompanyCode"].ToString(), txtCustomer.Text, txtNIC.Text, txtMobile.Text, Convert.ToDateTime(txtdate.Text).Date, Convert.ToInt32(txtPromoVouNo_new.Text), out _vouMsg) == false)
                {
                    DisplayMessage(_vouMsg, 1);
                    txtPromoVouNo_new.Text = "";
                    txtPromoVouNo_new.Focus();
                    MPPV.Show();
                }
                else
                {
                    // lblPromoVouNo.Text = txtPromoVouNo_new.Text;
                    btnPromoVouClose_Click(null, null);
                }
            }
            else
            {
                MPPV.Show();
                DisplayMessage("Please enter the promotion voucher number", 1);
                txtPromoVouNo_new.Focus();
            }
        }

        protected void btnPromoVouClose_Click(object sender, EventArgs e)
        {
            MPPV.Hide();
        }

        #region Add new item confirmation
        protected void btnAddnewYes_Click(object sender, EventArgs e)
        {
            txtItem.Focus();
        }

        protected void btnAddnewNO_Click(object sender, EventArgs e)
        {

        }

        protected void btnClosenewitm_Click(object sender, EventArgs e)
        {
            mpAddNewItem.Hide();
        }
        #endregion

        protected void btnSapd_itm_cd_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 _row = 0;
                GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
                bool _isValidate = false;
                bool _IsSerializedPriceLevel = _priceBookLevelRef.Sapl_is_serialized;

                for (int i = 0; i < gvPromotionPrice.Rows.Count; i++)
                {
                    GridViewRow dr = gvPromotionPrice.Rows[i];
                    CheckBox chkes = (CheckBox)dr.FindControl("chkSelectPromPrice");
                    chkes.Checked = false;
                    dr.BackColor = System.Drawing.Color.Transparent;
                }

                CheckBox chkSelectPromPrice = (CheckBox)row.FindControl("chkSelectPromPrice");
                chkSelectPromPrice.Checked = true;
                row.BackColor = System.Drawing.Color.LightCyan;

                Label lblBook = (Label)row.FindControl("lblBook");
                Label lblLevel = (Label)row.FindControl("lblLevel");
                Label Fromqty = (Label)row.FindControl("lblsapd_qty_from");
                Label ToQt = (Label)row.FindControl("lblsapd_qty_to");
                cmbBook.Text = lblBook.Text;

                LoadPriceLevel(cmbInvType.Text, cmbBook.Text);
                cmbLevel.Text = lblLevel.Text;
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                ClearPriceTextBox();
                if (_IsSerializedPriceLevel)
                {

                    //    //DataGridViewCheckBoxCell _chk = gvPromotionPrice.Rows[_row].Cells["PromPrice_Select"] as DataGridViewCheckBoxCell;
                    //    //bool _isSelected = false;
                    //    //if (Convert.ToBoolean(_chk.Value))
                    //    //    _isSelected = true;
                    //    //UncheckNormalPriceOrPromotionPrice(true, false);


                    //    TextBox lblSapd_itm_cd = (TextBox)row.FindControl("lblSapd_itm_cd");
                    //    TextBox lblPbSeq = (TextBox)row.FindControl("lblPbSeq");
                    //    TextBox lblSapd_price_type = (TextBox)row.FindControl("lblSapd_price_type");
                    //    TextBox txtIcet_actl_rt = (TextBox)row.FindControl("txticet_actl_rt");


                    //    string _mainItem = gvPromotionPrice.Rows[_row].Cells["PromPrice_Item"].Value.ToString();
                    //    string _mainSerial = gvPromotionPrice.Rows[_row].Cells["PromPrice_Serial"].Value.ToString();
                    //    string _pbseq = gvPromotionPrice.Rows[_row].Cells["PromPrice_Pb_Seq"].Value.ToString();
                    //    string _priceType = gvPromotionPrice.Rows[_row].Cells["PromPrice_PriceType"].Value.ToString();
                    //    BindPriceCombineItem(Convert.ToInt32(_pbseq), 1, Convert.ToInt32(_priceType), _mainItem, _mainSerial);
                    //    if (_isValidate)
                    //    {
                    //        if (_isSelected) _chk.Value = false; else _chk.Value = true;
                    //        decimal _count = (from DataGridViewRow row in gvNormalPrice.Rows
                    //                          where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true
                    //                          select row).Count();
                    //        if (_count > Convert.ToDecimal(txtQty.Text.Trim()))
                    //        {
                    //            _chk.Value = false;
                    //            this.Cursor = Cursors.Default;
                    //            using (new CenterWinDialog(this))
                    //            {
                    //                MessageBox.Show("Qty and the selected serials are mismatch.", "Serial and Qty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //            }
                    //            return;
                    //        }
                    //    }
                    //    if (_isSelected) _chk.Value = false; else _chk.Value = true;
                }
                else
                {
                    Label lblSapd_itm_cd = (Label)row.FindControl("lblSapd_itm_cd");
                    Label lblPbSeq = (Label)row.FindControl("lblPbSeq");
                    Label lblSapd_price_type = (Label)row.FindControl("lblSapd_price_type");
                    Label lblsapd_seq_no = (Label)row.FindControl("lblsapd_seq_no");
                    Label lblSapd_promo_cd = (Label)row.FindControl("lblSapd_promo_cd");
                    Session["Promotion"] = lblSapd_promo_cd.Text.Trim();

                    string _mainItem = lblSapd_itm_cd.Text.Trim();
                    string _pbseq = lblPbSeq.Text.Trim();
                    string _priceType = lblSapd_price_type.Text.Trim();
                    string _pblineseq = lblsapd_seq_no.Text.Trim();
                    BindPriceCombineItem(Convert.ToInt32(_pbseq), Convert.ToInt32(_pblineseq), Convert.ToInt32(_priceType),
                        _mainItem, string.Empty, Fromqty.Text, ToQt.Text);
                    chkSelectPromPrice.Checked = true;


                    //bool _isSelected = false;

                    //if (Convert.ToBoolean(chkSelectPromPrice.Checked))
                    //{
                    //    _isSelected = true;
                    //}
                    //else
                    //{
                    //    _isSelected = false;
                    //}

                    ////UncheckNormalPriceOrPromotionPrice(false, true);
                    ////BindingSource _source = new BindingSource();
                    ////_source.DataSource = new List<PriceCombinedItemRef>();
                    //gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                    //if (_isSelected)
                    //{
                    //    Label lblSapd_itm_cd = (Label)row.FindControl("lblSapd_itm_cd");
                    //    Label lblPbSeq = (Label)row.FindControl("lblPbSeq");
                    //    Label lblSapd_price_type = (Label)row.FindControl("lblSapd_price_type");
                    //    Label lblsapd_seq_no = (Label)row.FindControl("lblsapd_seq_no");
                    //    Label lblSapd_promo_cd = (Label)row.FindControl("lblSapd_promo_cd");
                    //    Session["Promotion"] = lblSapd_promo_cd.Text.Trim();

                    //    string _mainItem = lblSapd_itm_cd.Text.Trim();
                    //    string _pbseq = lblPbSeq.Text.Trim();
                    //    string _priceType = lblSapd_price_type.Text.Trim();
                    //    string _pblineseq = lblsapd_seq_no.Text.Trim();
                    //    BindPriceCombineItem(Convert.ToInt32(_pbseq), Convert.ToInt32(_pblineseq), Convert.ToInt32(_priceType), _mainItem, string.Empty);
                    //    chkSelectPromPrice.Checked = true;
                    //}
                    //else
                    //{
                    //    chkSelectPromPrice.Checked = false;
                    //    gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                    //    gvPromotionItem.DataBind();
                    //}
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void btnsapc_itm_cd_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                promotionsCustomMethod(dr);

            }
            catch (Exception ex)
            {
                DisplayMessageJS(ex.Message);
            }
        }

        protected void btnCLoseReservation_Click(object sender, EventArgs e)
        {
            mpReservations.Hide();
        }

        protected void btnReservationConfirm_Click(object sender, EventArgs e)
        {
            mpReservations.Show();
            if (dgvReservation.Rows.Count > 0)
            {
                for (int i = 0; i < dgvReservation.Rows.Count; i++)
                {
                    GridViewRow dr = dgvReservation.Rows[i];
                    CheckBox chkSel = (CheckBox)dr.FindControl("chkSelect");
                    Label lblirs_res_no = (Label)dr.FindControl("lblirs_res_no");
                    Label lblirs_seq = (Label)dr.FindControl("lblirs_seq");

                    if (chkSel.Checked)
                    {
                        lblSelectRevervation.Text = lblirs_res_no.Text;
                        txtresno.Text = lblirs_res_no.Text;

                        List<INR_RES_DET> oINR_RES_DETs = CHNLSVC.Sales.GET_RESERVATION_DET(Convert.ToInt32(lblirs_seq.Text), null);

                        DataTable dt = GlobalMethod.ToDataTable(oINR_RES_DETs);

                        if (oINR_RES_DETs != null && oINR_RES_DETs.Count > 0)
                        {
                            INR_RES_DET oINR_RES_DET = oINR_RES_DETs.Find(x => x.IRD_ITM_CD == txtItem.Text && x.IRD_ITM_STUS == cmbStatus.SelectedValue.ToString());
                            if (oINR_RES_DET != null && oINR_RES_DET.IRD_RES_NO != null)
                            {
                                decimal UsedQty = 0;
                                decimal balance = 0;
                                balance = oINR_RES_DET.IRD_RES_QTY - oINR_RES_DET.Ird_so_mrn_bqty;
                                List<InvoiceItem> oSaveDInvoiceItem = CHNLSVC.Sales.GET_INV_ITM_BY_RESNO_LINE(lblirs_res_no.Text, oINR_RES_DET.IRD_LINE);
                                if (oSaveDInvoiceItem != null && oSaveDInvoiceItem.Count > 0)
                                {
                                    UsedQty = oSaveDInvoiceItem.Sum(x => x.Sad_qty);
                                }
                                if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                                {
                                    UsedQty = UsedQty + _invoiceItemList.Where(x => x.Sad_res_no == oINR_RES_DET.IRD_RES_NO && x.Sad_res_line_no == oINR_RES_DET.IRD_LINE).Sum(x => x.Sad_qty);
                                }
                                Session["RESQTY"] = oINR_RES_DET.IRD_RES_BQTY;
                                if (balance <= UsedQty || balance < Convert.ToDecimal(txtQty.Text))
                                {
                                    DisplayMessage("Cannot exceed reserved quantity. Available Balance: " + oINR_RES_DET.IRD_RES_BQTY);
                                    lblSelectRevervation.Text = "";
                                    txtresno.Text = "";
                                    mpReservations.Show();
                                    return;
                                }

                                lblSelectRevLine.Text = oINR_RES_DET.IRD_LINE.ToString();
                                //DisplayMessageJS("Successfully reservation added.");
                                mpReservations.Hide();
                                return;
                            }
                            else
                            {
                                DisplayMessage("Cannot find a reservation for selected item and status");
                                lblSelectRevervation.Text = "";
                                txtresno.Text = "";
                                return;
                            }
                        }
                        else
                        {
                            DisplayMessage("Cannot find a reservation details");
                            lblSelectRevervation.Text = "";
                            txtresno.Text = "";
                            return;
                        }
                    }
                }
            }
        }

        protected void txtresno_TextChanged(object sender, EventArgs e)
        {
            txtresno.Text = txtresno.Text.Trim();
            bool _checkres = CHNLSVC.Sales.Check_resno(Session["UserCompanyCode"].ToString(), txtresno.Text, "N/A", txtPerTown.Text);
            if (!_checkres)
            {
                txtresno.Text = string.Empty;
                DisplayMessage("Please type correct reservation no");
                return;
            }
            lblSelectRevervation.Text = txtresno.Text;
            string _err = ValidateReservationNo();
            if (!string.IsNullOrEmpty(_err))
            {
                DispMsg(_err); return;
            }
        }

        protected void btnSearchReservation_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCustomer.Text))
            {
                DisplayMessage("Please select a customer.");
                return;
            }
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                DisplayMessage("Please select a item code");
                return;
            }
            if (cmbStatus.SelectedIndex == 0)
            {
                DisplayMessage("Please select a item status");
                return;
            }
            List<INR_RES> oINR_RESs = CHNLSVC.Sales.GET_RESERVATION_HDR(Session["UserCompanyCode"].ToString(), txtCustomer.Text, "A", txtPerTown.Text);
            if (oINR_RESs != null && oINR_RESs.Count > 0)
            {
                dgvReservation.DataSource = null;
                dgvReservation.DataSource = oINR_RESs;
                dgvReservation.DataBind();
                mpReservations.Show();
            }
            else
            {
                DisplayMessage("No records found.");
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (dgvReservation.Rows.Count > 0)
            {
                for (int i = 0; i < dgvReservation.Rows.Count; i++)
                {
                    GridViewRow dr = dgvReservation.Rows[i];
                    CheckBox chkSel = (CheckBox)dr.FindControl("chkSelect");
                    chkSel.Checked = false;
                }
            }

            GridViewRow drClick = (sender as CheckBox).NamingContainer as GridViewRow;
            CheckBox chkSel1 = (CheckBox)drClick.FindControl("chkSelect");
            chkSel1.Checked = true;
            mpReservations.Show();
        }

        #region Discount Request

        protected void btnDiscountEditItem_Click(object sender, EventArgs e)
        {
            MPDis.Show();

        }

        protected void btnDiscountUpdate_Click(object sender, EventArgs e)
        {
            MPDis.Show();
            gvDisItem.EditIndex = -1;
        }

        protected void btnDiscountCancelEdit_Click(object sender, EventArgs e)
        {
            try
            {
                MPDis.Show();
                gvDisItem.EditIndex = -1;
                gvDisItem.DataSource = _CashGeneralEntiryDiscount;
                gvDisItem.DataBind();
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            MPDis.Show();
            List<MsgInformation> _infor = CHNLSVC.General.GetMsgInformation(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString());
            if (_infor == null)
            {
                DisplayMessage("Your location does not setup detail which the request need to corroborate. Please contact IT dept.");
                return;
            }
            if (_infor.Count <= 0)
            {
                DisplayMessage("Your location does not setup detail which the request need to corroborate. Please contact IT dept.");
                return;
            }
            var _available = _infor.Where(x => x.Mmi_msg_tp != "A" && x.Mmi_receiver == Session["UserID"].ToString()).ToList();
            if (_available == null || _available.Count <= 0)
            {
                DisplayMessage("Your user ID is not setup. Please contact IT department for more details");
                return;
            }
            List<CashGeneralEntiryDiscountDef> _list = new List<CashGeneralEntiryDiscountDef>();
            if (ddlDisCategory.Text == "Customer")
            {
                if (string.IsNullOrEmpty(txtDisAmount.Text))
                {
                    DisplayMessage("Please select the discount rate");
                    return;
                }

                if (IsNumeric(txtDisAmount.Text) == false)
                {
                    DisplayMessage("Please select the valid discount rate");
                    return;
                }

                if (Convert.ToDecimal(txtDisAmount.Text.Trim()) > 100)
                {
                    DisplayMessage("Discount rate cannot exceed the 100%");
                    return;
                }

                if (Convert.ToDecimal(txtDisAmount.Text.Trim()) == 0)
                {
                    DisplayMessage("Discount rate cannot exceed the 0%");
                    return;
                }
            }
            if (ddlDisCategory.Text == "Item")
            {
                if (gvDisItem.Rows.Count > 0)
                {
                    bool _isCheckSingle = false;
                    for (int i = 0; i < gvDisItem.Rows.Count; i++)
                    {
                        GridViewRow dr = gvDisItem.Rows[i];
                        CheckBox DisItem_Select = (CheckBox)dr.FindControl("DisItem_Select");
                        if (DisItem_Select.Checked)
                        {
                            _isCheckSingle = true;
                            break;
                        }
                    }

                    if (_isCheckSingle == false)
                    {
                        DisplayMessage("Please select the item which you need to request");
                        return;
                    }
                }

                txtDisAmount.Text = "";
            }
            string _customer = txtCustomer.Text;
            string _customerReq = "DISREQ" + Convert.ToString(CHNLSVC.Inventory.GetSerialID());
            bool _isSuccessful = true;
            if (gvDisItem.Rows.Count > 0 && ddlDisCategory.Text == "Item")
            {
                for (int i = 0; i < gvDisItem.Rows.Count; i++)
                {
                    GridViewRow dr = gvDisItem.Rows[i];
                    CheckBox DisItem_Select = (CheckBox)dr.FindControl("DisItem_Select");
                    DropDownList ddlDiscReqType = (DropDownList)dr.FindControl("ddlDiscReqType");
                    TextBox txtDisItem_Amount = (TextBox)dr.FindControl("txtDisItem_Amount");
                    Label lblSgdd_itm = (Label)dr.FindControl("lblSgdd_itm");
                    Label lblSgdd_pb = (Label)dr.FindControl("lblSgdd_pb");
                    Label lblSgdd_pb_lvl = (Label)dr.FindControl("lblSgdd_pb_lvl");

                    if (DisItem_Select.Checked)
                    {

                        string _item = lblSgdd_itm.Text.Trim();
                        string _pricebook = lblSgdd_pb.Text.Trim();
                        string _pricelvl = lblSgdd_pb_lvl.Text.Trim();

                        if (string.IsNullOrEmpty(Convert.ToString(txtDisItem_Amount.Text).Trim()))
                        {
                            DisplayMessage("Please select the amount for " + _item);
                            _isSuccessful = false;
                            break;
                        }

                        if (!IsNumeric(Convert.ToString(txtDisItem_Amount.Text).Trim()))
                        {
                            DisplayMessage("Please select the valid amount for " + _item);
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(Convert.ToString(txtDisItem_Amount.Text).Trim()) <= 0)
                        {
                            DisplayMessage("Please select the valid amount for " + _item);
                            _isSuccessful = false;
                            break;
                        }
                        if (Convert.ToDecimal(Convert.ToString(txtDisItem_Amount.Text).Trim()) > 100 && ddlDiscReqType.SelectedValue.ToString().Contains("Rate"))
                        {
                            DisplayMessage("Rate cannot be exceed the 100% in " + _item);
                            _isSuccessful = false;
                            break;
                        }
                        CashGeneralEntiryDiscountDef _discount = new CashGeneralEntiryDiscountDef();
                        _discount.Sgdd_com = Session["UserCompanyCode"].ToString();
                        _discount.Sgdd_cre_by = Session["UserID"].ToString();
                        _discount.Sgdd_cre_dt = DateTime.Now.Date;
                        _discount.Sgdd_cust_cd = _customer;
                        if (ddlDiscReqType.SelectedValue.ToString().Contains("Rate"))
                            _discount.Sgdd_disc_rt = Convert.ToDecimal(txtDisItem_Amount.Text.Trim());
                        else
                            _discount.Sgdd_disc_val = Convert.ToDecimal(txtDisItem_Amount.Text.Trim());
                        _discount.Sgdd_from_dt = Convert.ToDateTime(txtdate.Text);
                        _discount.Sgdd_itm = _item;
                        _discount.Sgdd_mod_by = Session["UserID"].ToString();
                        _discount.Sgdd_mod_dt = DateTime.Now.Date;
                        _discount.Sgdd_no_of_times = 1;
                        _discount.Sgdd_no_of_used_times = 0;
                        _discount.Sgdd_pb = _pricebook;
                        _discount.Sgdd_pb_lvl = _pricelvl;
                        _discount.Sgdd_pc = Session["UserDefProf"].ToString();
                        _discount.Sgdd_req_ref = _customerReq;
                        _discount.Sgdd_sale_tp = cmbInvType.Text.Trim();
                        _discount.Sgdd_seq = 0;
                        _discount.Sgdd_stus = false;
                        _discount.Sgdd_to_dt = Convert.ToDateTime(txtdate.Text);
                        _list.Add(_discount);
                    }
                }
            }
            else
            {
                CashGeneralEntiryDiscountDef _discount = new CashGeneralEntiryDiscountDef();
                _discount.Sgdd_com = Session["UserCompanyCode"].ToString();
                _discount.Sgdd_cre_by = Session["UserID"].ToString();
                _discount.Sgdd_cre_dt = DateTime.Now.Date;
                _discount.Sgdd_cust_cd = _customer;
                _discount.Sgdd_disc_rt = Convert.ToDecimal(txtDisAmount.Text.Trim());
                _discount.Sgdd_from_dt = Convert.ToDateTime(txtdate.Text);
                _discount.Sgdd_itm = string.Empty;
                _discount.Sgdd_mod_by = Session["UserID"].ToString();
                _discount.Sgdd_mod_dt = DateTime.Now.Date;
                _discount.Sgdd_no_of_times = 1;
                _discount.Sgdd_no_of_used_times = 0;
                _discount.Sgdd_pb = cmbBook.Text.Trim();
                _discount.Sgdd_pb_lvl = cmbLevel.Text.Trim();
                _discount.Sgdd_pc = Session["UserDefProf"].ToString();
                _discount.Sgdd_req_ref = _customerReq;
                _discount.Sgdd_sale_tp = cmbInvType.Text.Trim();
                _discount.Sgdd_seq = 0;
                _discount.Sgdd_stus = false;
                _discount.Sgdd_to_dt = Convert.ToDateTime(txtdate.Text);
                _list.Add(_discount);
            }

            if (_isSuccessful)
            {
                if (string.IsNullOrEmpty(txtDisAmount.Text)) txtDisAmount.Text = "0.0";
                try
                {
                    int _effect = CHNLSVC.Sales.SaveCashGeneralEntityDiscountWindows(CommonUIDefiniton.SMSDocumentType.DISCOUNT.ToString(), Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _customerReq, Session["UserID"].ToString(), _list, txtCustomer.Text.Trim(), Convert.ToDecimal(txtDisAmount.Text.Trim()));
                    string Msg = string.Empty;
                    if (_effect > 0)
                    {
                        Msg = "Successfully saved! Document number: " + _customerReq + ".";
                        txtDisAmount.Text = "";
                    }
                    else
                    {
                        Msg = "Document not processed! please try again.";
                    }
                    DisplayMessage(Msg);
                }
                catch (Exception ex)
                {
                    CHNLSVC.CloseChannel();
                    DisplayMessage(ex.Message);
                }
            }

        }

        protected void BindGeneralDiscount()
        {
            MPDis.Show();
            gvDisItem.DataSource = new List<CashGeneralEntiryDiscountDef>();
            gvDisItem.DataBind();

        }

        protected void ddlDisCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            MPDis.Show();
            if (ddlDisCategory.SelectedValue.ToString() == "Item")
            {
                txtDisAmount.ReadOnly = true;
                txtDisAmount.Text = "";
                for (int i = 0; i < gvDisItem.Rows.Count; i++)
                {
                    GridViewRow dr = gvDisItem.Rows[i];
                    CheckBox DisItem_Select = (CheckBox)dr.FindControl("DisItem_Select");
                    TextBox txtDisItem_Amount = (TextBox)dr.FindControl("txtDisItem_Amount");
                    DisItem_Select.Enabled = true;
                    txtDisItem_Amount.Enabled = true;

                }
            }
            else
            {
                txtDisAmount.ReadOnly = false;
                txtDisAmount.Text = "";
                for (int i = 0; i < gvDisItem.Rows.Count; i++)
                {
                    GridViewRow dr = gvDisItem.Rows[i];
                    CheckBox DisItem_Select = (CheckBox)dr.FindControl("DisItem_Select");
                    TextBox txtDisItem_Amount = (TextBox)dr.FindControl("txtDisItem_Amount");
                    DisItem_Select.Enabled = false;
                    txtDisItem_Amount.Enabled = false;
                }
            }
        }

        #endregion

        protected void gvDisItem_RowEditing(object sender, GridViewEditEventArgs e)
        {
            MPDis.Show();
            gvDisItem.EditIndex = e.NewEditIndex;
            gvDisItem.DataSource = _CashGeneralEntiryDiscount;
            gvDisItem.DataBind();
        }

        protected void chkGiftVoucher_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                _isGiftVoucherCheckBoxClick = true;

                chkDeliverNow.Checked = false;



                _isGiftVoucherCheckBoxClick = false;

                // gf_assignItem.Visible = false;
                if (!_isGiftVoucherMsgPopup)
                    clarByRefresh();
                if (_isGiftVoucherMsgPopup)
                    _isGiftVoucherMsgPopup = false;

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        private void clarByRefresh()
        {
            Response.Redirect(Request.RawUrl);
        }

        protected void btnaddnewYes_Click1(object sender, EventArgs e)
        {
            txtItem.Focus();
        }

        protected void btnaddnewNNo_Click(object sender, EventArgs e)
        {
        }

        protected void txtPerTown_TextChanged(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable result = CHNLSVC.Sales.SearchLocations(SearchParams, "LOCATION CODE", txtPerTown.Text.Trim().ToUpper());
            if (result.Rows.Count == 0)
            {
                DisplayMessage("Please select valid location", 1);
                txtPerTown.Text = "";
                txtPerTown.Focus();
                return;
            }
        }

        protected void btnTownSearch_Click(object sender, EventArgs e)
        {
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
            DataTable result = CHNLSVC.Sales.SearchLocations(SearchParams, null, null);
            grdResult.DataSource = result;
            grdResult.DataBind();
            lblvalue.Text = "Town";
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            SIPopup.Show(); Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
            txtGroup.Focus();
        }

        protected void txtGroup_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGroup.Text))
                return;
            if (string.IsNullOrEmpty(cmbInvType.Text.Trim()))
            {
                DisplayMessage("Please select the invoice type!");
                cmbInvType.Focus();
                return;
            }
            if (cmbInvType.Text.Trim() != "CRED")
            {
                DisplayMessage("Group sales only available for credit sales!");
                return;
            }

            try
            {
                GroupSaleHeader _groupSale = CHNLSVC.Sales.GetGroupSaleHeaderDetails(txtGroup.Text.Trim());
                if (_groupSale != null)
                    if (!string.IsNullOrEmpty(_groupSale.Hgr_com))
                    {
                        ClearTop2p0();
                        ClearTop2p1();
                        ClearTop2p2();
                        txtCustomer.Text = _groupSale.Hgr_Grup_com;
                        txtCustomer_TextChanged(null, null);
                        return;
                    }
                DisplayMessage("Please check the group sale code.");
            }
            catch (Exception ex)
            {
                txtCustomer.Text = "";
                DisplayMessage(ex.Message);
            }
        }

        private void ClearTop2p0()
        {
            txtCustomer.Text = "";
            txtNIC.Text = "";
            txtMobile.Text = "";
            txtLoyalty.Text = "";
            txtCusName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            SetLoyalityColor();
        }

        private void ClearTop2p1()
        {
            chkTaxPayable.Checked = false;
            lblSVatStatus.Text = string.Empty;
            lblVatExemptStatus.Text = string.Empty;
        }

        private void ClearTop2p2()
        {
            lblAccountBalance.Text = FormatToCurrency("0");
            lblAvailableCredit.Text = FormatToCurrency("0");
        }

        protected void chkManualRef_CheckedChanged(object sender, EventArgs e)
        {
            try
            {


                txtManualRefNo.Text = string.Empty;
                txtManualRefNo.Enabled = false;

            }
            catch (Exception ex)
            {
                txtManualRefNo.Text = "";
                txtManualRefNo.Enabled = false;

                DisplayMessage(ex.Message);
            }
        }

        protected void btnconfsaveclose_Click(object sender, EventArgs e)
        {
            mpConfSaveDisco.Hide();
        }

        protected void btnYesConfSavePromo_Click(object sender, EventArgs e)
        {
            txtSavePromotion.Value = "1";
            btnSave_Click(null, null);
        }

        protected void btnNoConfSavePromo_Click(object sender, EventArgs e)
        {
            mpConfSaveDisco.Hide();
        }

        private string SetDecimalPoint(decimal amount)
        {
            return amount.ToString("N2");
        }

        private string SetDecimalPoint(string amount)
        {
            decimal value = Convert.ToDecimal(amount);
            return value.ToString("N2");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            SIPopup.Hide();
            txtSearchbyword.Text = "";
        }

        protected void btnClose_Click1(object sender, EventArgs e)
        {
            SIPopup.Hide();
            txtSearchbyword.Text = "";
        }

        protected void btnCloseSearchMP_Click(object sender, EventArgs e)
        {
            Session["SIPopup"] = null;
            SIPopup.Hide();
            txtSearchbyword.Text = "";
        }

        protected void chkOpenDelivery_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txtdellocation.ReadOnly = true;
                if (_MasterProfitCenter != null)
                    if (_MasterProfitCenter.Mpc_com != null)
                    {
                        if (string.IsNullOrEmpty(_MasterProfitCenter.Mpc_def_loc))
                        {
                            if (chkOpenDelivery.Checked == false)
                            {
                                DisplayMessage("Default location not setup. You have to contact inventory department.");
                                txtdellocation.Text = Session["UserDefLoca"].ToString();
                                return;
                            }
                        }
                        else
                        {
                            if (chkOpenDelivery.Checked == false)
                            {
                                txtdellocation.Text = _MasterProfitCenter.Mpc_def_loc;
                                // btnSearchDelLocation.CssClass = "buttonUndocolor";
                                // btnSearchDelLocation.Enabled = true;
                                txtdellocation.ReadOnly = false;
                            }
                            else
                            {
                                txtdellocation.Text = "";
                                txtdellocation.ReadOnly = true;
                                //  btnSearchDelLocation.CssClass = "buttoncolor";
                                // btnSearchDelLocation.Enabled = false;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                chkOpenDelivery.Checked = false;
                DisplayMessage(ex.Message, 4);
            }
            MpDelivery.Show();
        }

        protected void btnSearchFreSerials_Click(object sender, EventArgs e)
        {
            txtPriNProSerialSearch_TextChanged(null, null);
        }

        protected void txtPriNProSerialSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (gvPromotionSerial.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(txtPriNProSerialSearch.Text.Trim()))
                    {
                        var query = _promotionSerial.Where(x => x.Tus_ser_1.Contains(txtPriNProSerialSearch.Text.Trim())).ToList();
                        if (query != null)
                        {
                            if (query.Count() > 0)
                            {
                                gvPromotionSerial.DataSource = query;
                                gvPromotionSerial.DataBind();
                            }
                            else
                            {
                                gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                                gvPromotionSerial.DataBind();
                            }
                        }
                        else
                        {
                            gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                            gvPromotionSerial.DataBind();
                        }
                    }
                    else
                    {
                        gvPromotionSerial.DataSource = _promotionSerial;
                        gvPromotionSerial.DataBind();
                    }


                }
                else
                {
                    gvPromotionSerial.DataSource = _promotionSerial;
                    gvPromotionSerial.DataBind();
                }


                for (int i = 0; i < gvPromotionSerial.Rows.Count; i++)
                {
                    GridViewRow dr = gvPromotionSerial.Rows[i];
                    Label lblTus_ser_id = dr.FindControl("lblTus_ser_id") as Label;
                    string _id = lblTus_ser_id.Text.Trim();
                    CheckBox chkSelectPromSerial = dr.FindControl("chkSelectPromSerial") as CheckBox;

                    if (_promotionSerialTemp.FindAll(x => x.Tus_ser_id == Convert.ToInt32(_id)).Count > 0)
                    {
                        chkSelectPromSerial.Checked = true;
                    }
                    else
                    {
                        chkSelectPromSerial.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }

            mpPriceNPromotion.Show();
        }

        #region Based on Advance Receipt

        protected void chkBasedOnAdvanceRecept_CheckedChanged(object sender, EventArgs e)
        {


            mpAdavce.Hide();

        }

        protected void btnCloseAdvcRe_Click(object sender, EventArgs e)
        {
            mpAdavce.Hide();
        }

        protected void btnSearchADVR_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DocNo);
                DataTable result = CHNLSVC.CommonSearch.Get_DocNum_ByRefNo(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "DocNo";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtdellocation.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void txtADVRNumber_TextChanged(object sender, EventArgs e)
        {
            mpAdavce.Show();
        }

        protected void btnLoadADV_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtADVRNumber.Text))
            {
                Decimal _validDays = 0;
                List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADPROMAXDT", "COM", Session["UserCompanyCode"].ToString());
                if (para.Count > 0)
                {
                    _validDays = para[0].Hsy_val;
                }

                List<ReceiptItemDetails> oReceiptItemDetails = CHNLSVC.Sales.GetAdvanReceiptItems(txtADVRNumber.Text.Trim());
                RecieptHeader oRecieptHeader = CHNLSVC.Sales.GetReceiptHeaderByType(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtADVRNumber.Text.Trim(), "ADVAN");
                if (oRecieptHeader == null || string.IsNullOrEmpty(oRecieptHeader.Sar_receipt_no))
                {
                    DisplayMessageJS("Please select a valid receipt advance number");
                    mpAdavce.Show();
                    return;
                }
                if (oRecieptHeader.SAR_VALID_TO.Date < DateTime.Now.Date)
                {
                    DisplayMessageJS("Selected receipt is expired");
                    dgvReceiptItems.DataSource = oReceiptItemDetails;
                    dgvReceiptItems.DataBind();
                    mpAdavce.Show();
                    return;
                }

                if ((DateTime.Now.Date - oRecieptHeader.Sar_receipt_date).TotalDays > Convert.ToDouble(_validDays))
                {
                    DisplayMessageJS("Selected receipt is expired");
                    dgvReceiptItems.DataSource = oReceiptItemDetails;
                    dgvReceiptItems.DataBind();
                    mpAdavce.Show();
                    return;
                }

                cmbInvType.SelectedValue = oRecieptHeader.Sar_inv_type;
                //_//cmbInvType_Leave(null, null);

                txtCustomer.Text = oRecieptHeader.Sar_debtor_cd;
                LoadCustomerDetailsByCustomer();

                _invoiceItemList = new List<InvoiceItem>();
                int itmLine = 0;
                if (oReceiptItemDetails == null)
                {
                    DisplayMessageJS("No items to load");
                    mpAdavce.Show();
                    return;
                }
                foreach (ReceiptItemDetails item in oReceiptItemDetails)
                {
                    MasterItem oMasterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), item.Sari_item);
                    itmLine = itmLine + 1;
                    InvoiceItem oNewItem = new InvoiceItem();
                    oNewItem.Sad_alt_itm_cd = item.Sari_item;
                    oNewItem.Sad_alt_itm_desc = item.Sari_item_desc;
                    oNewItem.Sad_comm_amt = 0;
                    oNewItem.Sad_disc_amt = 0;
                    oNewItem.Sad_disc_rt = 0;
                    oNewItem.Sad_do_qty = 0;
                    oNewItem.Sad_fws_ignore_qty = 0;
                    oNewItem.Sad_inv_no = string.Empty;
                    oNewItem.Sad_is_promo = false;
                    oNewItem.Sad_itm_cd = item.Sari_item;
                    oNewItem.Sad_itm_line = itmLine;
                    oNewItem.Sad_itm_seq = 0;
                    oNewItem.Sad_itm_stus = item.Sari_sts;
                    oNewItem.Sad_itm_tax_amt = item.Sari_tax_amt;
                    oNewItem.Sad_itm_tp = oMasterItem.Mi_itm_tp;
                    //oNewItem.Sad_job_line = 0;
                    oNewItem.Sad_job_no = string.Empty;
                    oNewItem.Sad_merge_itm = string.Empty;
                    oNewItem.Sad_outlet_dept = string.Empty;
                    oNewItem.Sad_pbook = item.Sari_pb;
                    oNewItem.Sad_pb_lvl = item.Sari_pb_lvl;
                    oNewItem.Sad_pb_price = 0;
                    oNewItem.Sad_print_stus = false;
                    oNewItem.Sad_promo_cd = string.Empty;
                    oNewItem.Sad_qty = item.Sari_qty;
                    oNewItem.Sad_res_line_no = 0;
                    oNewItem.Sad_res_no = string.Empty;
                    oNewItem.Sad_seq = 0;
                    oNewItem.Sad_seq_no = 0;
                    oNewItem.Sad_sim_itm_cd = string.Empty;
                    oNewItem.Sad_srn_qty = 0;
                    oNewItem.Sad_tot_amt = item.Sari_unit_amt;
                    oNewItem.Sad_trd_svc_chrg = 0;
                    oNewItem.Sad_unit_amt = item.Sari_unit_amt;
                    oNewItem.Sad_unit_rt = item.Sari_unit_rate;
                    oNewItem.Sad_uom = oMasterItem.Mi_itm_uom;
                    oNewItem.Sad_warr_based = false;
                    oNewItem.Sad_warr_period = 0;
                    oNewItem.Sad_warr_remarks = string.Empty;
                    oNewItem.Sad_isapp = false;
                    oNewItem.Sad_iscovernote = false;
                    oNewItem.Mi_longdesc = oMasterItem.Mi_longdesc;

                    _invoiceItemList.Add(oNewItem);

                    if (!String.IsNullOrEmpty(item.Sari_serial))
                    {
                        _lineNo += 1;
                        ReptPickSerials _serLst = null;
                        _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, item.Sari_item.Trim(), item.Sari_serial.Trim());
                        if (oMasterItem.Mi_is_ser1 == 1)
                        {
                            _serLst.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                            _serLst.Tus_base_itm_line = _lineNo;
                            _serLst.Tus_usrseq_no = ScanSequanceNo;
                            _serLst.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                            _serLst.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty;
                            _serLst.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty;
                            _serLst.ItemType = oMasterItem.Mi_itm_tp;
                            ScanSerialList.Add(_serLst);
                        }

                        foreach (ReptPickSerials itemSer in ScanSerialList)
                        {
                            InvoiceSerial _invser = new InvoiceSerial();
                            _invser.Sap_del_loc = Session["UserDefLoca"].ToString();
                            _invser.Sap_itm_cd = itemSer.Tus_itm_cd;
                            _invser.Sap_itm_line = itemSer.Tus_itm_line;
                            if (ScanSerialList.Count == 1)
                            {
                                _invser.Sap_itm_line = 1;
                            }
                            _invser.Sap_remarks = string.Empty;

                            if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                            {
                                InvoiceItem oItem = _invoiceItemList.Find(x => x.Sad_itm_cd == itemSer.Tus_itm_cd && x.Sad_itm_line == itemSer.Tus_itm_line);
                                if (oItem != null)
                                {
                                    _invser.Sap_seq_no = oItem.Sad_seq;
                                }
                                else
                                {
                                    InvoiceItem oItem2 = _invoiceItemList.Find(x => x.Sad_itm_cd == itemSer.Tus_itm_cd);
                                    _invser.Sap_seq_no = oItem2.Sad_seq;
                                }
                            }

                            _invser.Sap_ser_1 = itemSer.Tus_ser_1;
                            _invser.Sap_ser_line = _isCombineAdding ? Convert.ToInt32(SSCombineLine) : 0;
                            InvoiceSerialList.Add(_invser);
                        }

                    }

                    CalculateGrandTotal(item.Sari_qty, item.Sari_unit_rate, 0, item.Sari_tax_amt, true);
                }

                gvPopSerial.DataSource = new List<ReptPickSerials>();
                gvPopSerial.DataSource = setSerialStatusDescriptions(ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList());
                gvPopSerial.DataBind();
                BindAddItem();

                decimal _tobepays = 0;
                if (lblSVatStatus.Text == "Available") _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                else _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());



                //btnAddItem.Enabled = false;
                //txtSerialNo.Enabled = false;
                //txtItem.Enabled = false;
                //btnSearch_Serial.Enabled = false;
                //btnSearch_Item.Enabled = false;


                //ucPayModes1.ComboChange(txtADVRNumber.Text.Trim());
                mpAdavce.Hide();
            }
            else
            {
                DisplayMessageJS("Please select a receipt number");
                mpAdavce.Show();
            }
        }

        #endregion

        private void LoadPayMode()
        {
            Label pdAmount = new Label();
            pdAmount.Text = "0";
            TextBox ucAmont = new TextBox();
            ucAmont.Text = "0";

        }

        protected void cmbInvType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // LoadPayMode();


            #region Clear Customer

            txtCusName.Text = string.Empty;
            txtAddress1.Text = string.Empty;
            txtAddress2.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtNIC.Text = string.Empty;
            chkTaxPayable.Checked = false;
            txtCustomer.Text = string.Empty;
            txtdelad1.Text = string.Empty; ;
            txtdelad2.Text = string.Empty;
            txtdelcuscode.Text = string.Empty;
            txtdelname.Text = string.Empty;
            if (cmbInvType.Text.Trim() == "CS")
                txtCustomer.Text = "CASH";


            txtSerialNo.Text = string.Empty;
            txtItem.Text = string.Empty;
            txtQty.Text = "1";
            txtUnitPrice.Text = "0.00";
            txtUnitAmt.Text = "0.00";
            txtDisRate.Text = "0.00";
            txtDisAmt.Text = "0.00";
            txtTaxAmt.Text = "0.00";
            txtLineTotAmt.Text = "0.00";
            txtresno.Text = string.Empty;
            lblItemDescription.Text = string.Empty;
            lblItemModel.Text = string.Empty;
            lblItemBrand.Text = string.Empty;
            lblItemSerialStatus.Text = string.Empty;

            #endregion Clear Customer

            LoadPriceBook(cmbInvType.Text.Trim());
            LoadPriceLevel(cmbInvType.Text.Trim(), cmbBook.Text.Trim());
            LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
            LoadPayMode();
        }

        protected void btnCloseSimiler_Click(object sender, EventArgs e)
        {
            mpSimilrItmes.Hide();
            mpPriceNPromotion.Show();
        }

        protected void btnTest_Click1(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "scrollWin()", true);

            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "EnableCheckBox();", true);

            //chkDeliverLater.InputAttributes.Add("disabled", "disabled");

            ////mpSimilrItmes.Show();
            //chkDeliverLater.Enabled = true;
            //chkDeliverLater.Checked = false;
        }

        private void getSilimerItems(string DocumentType, string ItemCode, string PromotionCode)
        {
            List<MasterItemSimilar> dt = CHNLSVC.Inventory.GetSimilarItems(DocumentType, ItemCode, Session["UserCompanyCode"].ToString(), Convert.ToDateTime(txtdate.Text).Date, string.Empty, PromotionCode, Session["UserDefLoca"].ToString(), Session["UserDefProf"].ToString());
            if (dt != null)
            {
                if (dt.Count > 0)
                {
                    dgvSimilerItemPick.AutoGenerateColumns = false;
                    dgvSimilerItemPick.DataSource = dt;
                    dgvSimilerItemPick.DataBind();
                }
            }
        }

        protected void btnMISI_SIM_ITM_CD_Click(object sender, EventArgs e)
        {
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblMISI_SIM_ITM_CD = dr.FindControl("lblMISI_SIM_ITM_CD") as Label;
            string _originalItem = hdfOriginalItem.Value.ToString();

            _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == _originalItem).ToList().ForEach(x => x.Similer_item = lblMISI_SIM_ITM_CD.Text);
            _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == _originalItem).ToList().ForEach(x => x.Sapc_increse = false);
            _tempPriceCombinItem.Where(x => x.Sapc_itm_cd == _originalItem).ToList().ForEach(x => x.Sapc_sub_ser = string.Empty);

            gvPromotionItem.DataSource = _tempPriceCombinItem;
            gvPromotionItem.DataBind();

            mpSimilrItmes.Hide();
            mpPriceNPromotion.Show();
        }

        private void setGriDel_enables(bool isEnable)
        {
            if (isEnable)
            {
                for (int i = 0; i < gvInvoiceItem.Rows.Count; i++)
                {
                    GridViewRow dr = gvInvoiceItem.Rows[i];
                    LinkButton lbtndelitem = dr.FindControl("lbtndelitem") as LinkButton;
                    lbtndelitem.OnClientClick = "ConfirmDeleteItem();";
                    lbtndelitem.CssClass = "buttonUndocolor";
                }

                for (int i = 0; i < gvPopSerial.Rows.Count; i++)
                {
                    GridViewRow dr = gvPopSerial.Rows[i];
                    LinkButton lbtndelitem = dr.FindControl("lbtndelitem") as LinkButton;
                    lbtndelitem.OnClientClick = "ConfirmDeleteSerial();";
                    lbtndelitem.CssClass = "buttonUndocolor";
                }

                lbtncode.CssClass = "buttonUndocolor";
                lbtncode.Attributes.Add("onclick", "lbtncode_Click");

                btnSearch_NIC.CssClass = "buttonUndocolor";
                btnSearch_NIC.Attributes.Add("onclick", "btnSearch_NIC_Click");

                btnSearch_Mobile.CssClass = "buttonUndocolor";
                btnSearch_Mobile.Attributes.Add("onclick", "btnSearch_Mobile_Click");

                btnSearch_Loyalty.CssClass = "buttonUndocolor";
                btnSearch_Loyalty.Attributes.Add("onclick", "btnSearch_Loyalty_Click");

                btnTownSearch.CssClass = "buttonUndocolor";
                btnTownSearch.Attributes.Add("onclick", "btnTownSearch_Click");

                lbtncurrency.CssClass = "buttonUndocolor";
                lbtncurrency.Attributes.Add("onclick", "lbtncurrency_Click");

                btnSearch_Serial.CssClass = "buttonUndocolor";
                btnSearch_Serial.Attributes.Add("onclick", "btnSearch_Serial_Click");

                btnSearch_Item.CssClass = "buttonUndocolor";
                btnSearch_Item.Attributes.Add("onclick", "btnSearch_Item_Click");

                btnSearchReservation.CssClass = "buttonUndocolor";
                btnSearchReservation.Attributes.Add("onclick", "btnSearchReservation_Click");

                lbtnadditems.CssClass = "buttonUndocolor";
                lbtnadditems.Attributes.Add("OnClick", "lbtnadditems_Click");
            }
            else
            {
                for (int i = 0; i < gvInvoiceItem.Rows.Count; i++)
                {
                    GridViewRow dr = gvInvoiceItem.Rows[i];
                    LinkButton lbtndelitem = dr.FindControl("lbtndelitem") as LinkButton;
                    lbtndelitem.OnClientClick = "";
                    lbtndelitem.CssClass = "buttoncolor";
                }

                for (int i = 0; i < gvPopSerial.Rows.Count; i++)
                {
                    GridViewRow dr = gvPopSerial.Rows[i];
                    LinkButton lbtndelitem = dr.FindControl("lbtndelitem") as LinkButton;
                    lbtndelitem.OnClientClick = "";
                    lbtndelitem.CssClass = "buttoncolor";
                }

                lbtncode.CssClass = "buttoncolor";
                lbtncode.Attributes.Add("onclick", "return false;");

                btnSearch_NIC.CssClass = "buttoncolor";
                btnSearch_NIC.Attributes.Add("onclick", "return false;");

                btnSearch_Mobile.CssClass = "buttoncolor";
                btnSearch_Mobile.Attributes.Add("onclick", "return false;");

                btnSearch_Loyalty.CssClass = "buttoncolor";
                btnSearch_Loyalty.Attributes.Add("onclick", "return false;");

                btnTownSearch.CssClass = "buttoncolor";
                btnTownSearch.Attributes.Add("onclick", "return false;");

                lbtncurrency.CssClass = "buttoncolor";
                lbtncurrency.Attributes.Add("onclick", "return false;");

                btnSearch_Serial.CssClass = "buttoncolor";
                btnSearch_Serial.Attributes.Add("onclick", "return false;");

                btnSearch_Item.CssClass = "buttoncolor";
                btnSearch_Item.Attributes.Add("onclick", "return false;");

                btnSearchReservation.CssClass = "buttoncolor";
                btnSearchReservation.Attributes.Add("onclick", "return false;");

                lbtnadditems.CssClass = "buttoncolor";
                lbtnadditems.Attributes.Add("onclick", "return false;");

            }
        }

        private void loadDiscountRequests()
        {
            List<CashGeneralEntiryDiscountDef> oItems = CHNLSVC.Sales.GET_DIS_REQ_BY_CUSTOMER(cmbInvType.SelectedValue.ToString(), Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToDateTime(txtdate.Text).Date, txtCustomer.Text.Trim());
            if (oItems != null && oItems.Count > 0)
            {
                //oItems = oItems.FindAll(x => x.Sgdd_stus == true);
                if (oItems != null && oItems.Count > 0)
                {
                    ddlDisCategory.Enabled = false;

                    List<String> itemsList = oItems.Select(x => x.Sgdd_itm).Distinct().ToList();
                    itemsList.RemoveAll(str => String.IsNullOrEmpty(str));

                    //string[] itemsList = oItems.Select(x => x.Sgdd_itm).Distinct().ToArray();

                    if (itemsList.Count > 0)
                    {
                        gvDisItem.DataSource = oItems;
                        gvDisItem.DataBind();

                        gvDisItem.Columns[6].Visible = true;

                        for (int i = 0; i < gvDisItem.Rows.Count; i++)
                        {
                            GridViewRow dr = gvDisItem.Rows[i];
                            Label lblSgdd_stus = dr.FindControl("lblSgdd_stus") as Label;
                            Label lblSgdd_itm = dr.FindControl("lblSgdd_itm") as Label;

                            if (lblSgdd_stus.Text.ToUpper() == "TRUE")
                            {
                                lblSgdd_stus.Text = "Active";
                            }
                            else
                            {
                                lblSgdd_stus.Text = "Inactive";
                            }

                            if (lblSgdd_itm == null || string.IsNullOrEmpty(lblSgdd_itm.Text))
                            {
                                dr.Visible = false;
                            }
                        }

                        for (int i = 0; i < oItems.Count; i++)
                        {
                            CashGeneralEntiryDiscountDef item = oItems[i];
                            GridViewRow dr = gvDisItem.Rows[i];
                            DropDownList ddlDiscReqType = dr.FindControl("ddlDiscReqType") as DropDownList;
                            TextBox txtDisItem_Amount = dr.FindControl("txtDisItem_Amount") as TextBox;

                            if (item.Sgdd_disc_rt > 0 & item.Sgdd_disc_val == 0)
                            {
                                ddlDiscReqType.SelectedIndex = 0;
                            }
                            else
                            {
                                txtDisItem_Amount.Text = item.Sgdd_disc_val.ToString();
                                ddlDiscReqType.SelectedIndex = 1;
                            }

                            ddlDiscReqType.Enabled = false;
                        }
                    }
                    else
                    {
                        txtDisAmount.Text = oItems[0].Sgdd_disc_rt.ToString("N2");
                        gvDisItem.DataSource = new int[] { };
                        gvDisItem.DataBind();
                    }

                    if (itemsList.Count != oItems.Count)
                    {
                        List<CashGeneralEntiryDiscountDef> oItemsTemp = oItems.FindAll(y => y.Sgdd_stus == true && y.Sgdd_itm == "");
                        if (oItemsTemp != null && oItemsTemp.Count > 0 && oItemsTemp.FindAll(x => x.Sgdd_cre_dt == oItemsTemp.Max(y => y.Sgdd_cre_dt)).Count > 0)
                        {
                            txtDisAmount.Text = oItemsTemp.FindAll(x => x.Sgdd_cre_dt == oItemsTemp.Max(y => y.Sgdd_cre_dt))[0].Sgdd_disc_rt.ToString("N2");
                        }
                    }
                }
            }
        }

        protected void btnBBGVItemDelete_Click(object sender, EventArgs e)
        {
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lbltus_itm_cd = dr.FindControl("lbltus_itm_stus") as Label;
            Label lbltus_itm_desc = dr.FindControl("lbltus_itm_desc") as Label;
            Label lbltus_itm_model = dr.FindControl("lbltus_itm_model") as Label;
            Label lbltus_itm_stus = dr.FindControl("lbltus_itm_stus") as Label;

            ReptPickSerials oDelItem = BuyBackItemList.Find(x => x.Tus_itm_cd == lbltus_itm_cd.Text.Trim() && x.Tus_itm_desc == lbltus_itm_desc.Text.Trim() && x.Tus_itm_model == lbltus_itm_model.Text.Trim());
            BuyBackItemList.Remove(oDelItem);

            var _bind = new BindingList<ReptPickSerials>(BuyBackItemList);

        }

        protected void btnLoadDisReqs_Click(object sender, EventArgs e)
        {
            loadDiscountRequests();

            MPDis.Show();
        }

        protected void btnAddSerials_Click(object sender, EventArgs e)
        {
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label InvItm_Item = dr.FindControl("InvItm_Item") as Label;
            Label lblsad_itm_stus = dr.FindControl("lblsad_itm_stus") as Label;
            Label lblsad_itm_line = dr.FindControl("lblsad_itm_line") as Label;
            Label InvItm_Qty = dr.FindControl("InvItm_Qty") as Label;
            Label InvItm_Book = dr.FindControl("InvItm_Book") as Label;
            Label InvItm_Level = dr.FindControl("InvItm_Level") as Label;
            Label InvItm_PromoCd = dr.FindControl("InvItm_PromoCd") as Label;

            string _item = InvItm_Item.Text.Trim();
            string _status = lblsad_itm_stus.Text.Trim();
            int _itemLineNo = Convert.ToInt32(lblsad_itm_line.Text.Trim());
            Session["PSITemLine"] = _itemLineNo.ToString();

            decimal _invoiceQty = Convert.ToDecimal(InvItm_Qty.Text.Trim());
            decimal _doQty = 0;
            decimal _scanQty = 0;
            if (ScanSerialList != null) _scanQty = ScanSerialList.Where(x => x.Tus_base_itm_line == _itemLineNo).Sum(x => x.Tus_qty);
            string _priceBook = InvItm_Book.Text.Trim();
            string _priceLevel = InvItm_Level.Text.Trim();
            int pbCount = CHNLSVC.Sales.GetDOPbCount(Session["UserCompanyCode"].ToString(), _priceBook, _priceLevel);
            string _promotioncd = InvItm_PromoCd.Text.Trim();
            bool _isAgePriceLevel = false;
            int _ageingDays = -1;
            MasterItem _itemM = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            if (_itemM.Mi_is_ser1 == -1 || _itemM.Mi_is_ser1 == 0)
            {
                DisplayMessage("You do not need to pick non-serialized item.");
                return;
            }
            DataTable _categoryDet = CHNLSVC.General.GetMainCategoryDetail(_itemM.Mi_cate_1);
            List<PriceBookLevelRef> _level = CHNLSVC.Sales.GetPriceLevelList(Session["UserCompanyCode"].ToString(), _priceBook, _priceLevel);
            if (_level != null)
                if (_level.Count > 0)
                {
                    var _lvl = _level.Where(x => x.Sapl_isage).ToList();
                    if (_lvl != null) if (_lvl.Count() > 0)
                            _isAgePriceLevel = true;
                }

            if (_categoryDet != null && _isAgePriceLevel)
                if (_categoryDet.Rows.Count > 0)
                {
                    if (_categoryDet.Rows[0]["ric1_age"] != DBNull.Value)
                        _ageingDays = Convert.ToInt32(_categoryDet.Rows[0].Field<Int16>("ric1_age"));
                    else _ageingDays = 0;
                }
            if ((_invoiceQty - _doQty) <= 0) return;
            if ((_invoiceQty - _doQty) <= _scanQty) return;


            lblPopupItemCode.Text = _item;
            loadPickSerials(_item, _itemLineNo, _ageingDays, _status);
            mpPickSerial.Show();
            lblPopupQty.Text = (_invoiceQty - _doQty).ToString();
            lblScanQty.Text = _scanQty.ToString();
            Session["mpPickSerial"] = "Y";
            //_commonOutScan = new CommonSearch.CommonOutScan();
            //_commonOutScan._isWriteToTemporaryTable = false;
            //_commonOutScan.ModuleTypeNo = 1;
            //_commonOutScan.ScanDocument = "N/A";
            //_commonOutScan.DocumentType = "DO";
            //_commonOutScan.PopupItemCode = _item;
            //_commonOutScan.ItemStatus = _status;
            //_commonOutScan.ItemLineNo = _itemLineNo;
            //_commonOutScan.PopupQty = _invoiceQty - _doQty;
            //_commonOutScan.ApprovedQty = _doQty;
            //_commonOutScan.ScanQty = _scanQty;
            //_commonOutScan.IsAgePriceLevel = _isAgePriceLevel;
            //_commonOutScan.DocumentDate = txtDate.Value.Date;
            //_commonOutScan.NoOfDays = _ageingDays;
            //if (pbCount <= 0) _commonOutScan.IsCheckStatus = false;
            //else _commonOutScan.IsCheckStatus = true;
            //_commonOutScan.AddSerialClick += new EventHandler(CommonOutScan_AddSerialClick);
            //_commonOutScan.Location = new Point(((this.Width - _commonOutScan.Width) / 2), ((this.Height - _commonOutScan.Height) / 2) + 50);
            //_commonOutScan.ShowDialog();
            return;
        }

        #region Add Serials to invoice

        protected void btnPSPClose_Click(object sender, EventArgs e)
        {
            mpPickSerial.Hide();
            Session["mpPickSerial"] = null;
        }

        protected void txtSearchbywordA_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSearchbywordA.Text))
            {
                List<ReptPickSerials> oAllserioal = (List<ReptPickSerials>)Session["SerialPick"];
                if (ddlSearchbykeyA.SelectedItem.Text == "Serial 1")
                {
                    oAllserioal = oAllserioal.FindAll(x => x.Tus_ser_1.Contains(txtSearchbywordA.Text.Trim()));

                }
                else if (ddlSearchbykeyA.SelectedItem.Text == "Serial 2")
                {
                    oAllserioal = oAllserioal.FindAll(x => x.Tus_ser_2.Contains(txtSearchbywordA.Text.Trim()));

                }
                grdAdSearch.DataSource = oAllserioal;
                grdAdSearch.DataBind();
            }
            else
            {
                List<ReptPickSerials> oAllserioal = (List<ReptPickSerials>)Session["SerialPick"];
                grdAdSearch.DataSource = oAllserioal;
                grdAdSearch.DataBind();
            }
        }

        protected void lbtnSearchA_Click(object sender, EventArgs e)
        {

        }

        protected void btnAdvanceAddItem_Click(object sender, EventArgs e)
        {
            Int32 generated_seq = -1;
            Int32 num_of_checked_itms = 0;
            bool _isWriteToTemporaryTable = false;

            string itemCode = lblPopupItemCode.Text.Trim();

            for (int i = 0; i < grdAdSearch.Rows.Count; i++)
            {
                GridViewRow dr = grdAdSearch.Rows[i];
                CheckBox selectchk = dr.FindControl("selectchk") as CheckBox;
                if (selectchk.Checked)
                {
                    num_of_checked_itms = num_of_checked_itms + 1;
                }
            }

            Decimal pending_amt = Convert.ToDecimal(lblPopupQty.Text.ToString()) - Convert.ToDecimal(lblScanQty.Text.ToString());
            if (num_of_checked_itms > pending_amt)
            {
                DisplayMessage("Can't exceed Approved Qty. You can add only " + pending_amt + " itmes more.");
                return;
            }

            MasterItem msitem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itemCode);

            Int32 user_seq_num = 0;
            if (msitem.Mi_is_ser1 != -1)
            {
                int rowCount = 0;

                for (int i = 0; i < grdAdSearch.Rows.Count; i++)
                {
                    GridViewRow dr = grdAdSearch.Rows[i];
                    Label lblTus_ser_id = dr.FindControl("lblTus_ser_id") as Label;
                    Label lblTus_bin = dr.FindControl("lblTus_bin") as Label;
                    Label lblTus_itm_stus = dr.FindControl("lblTus_itm_stus") as Label;

                    Int32 serID = Convert.ToInt32(lblTus_ser_id.Text);
                    CheckBox selectchk = dr.FindControl("selectchk") as CheckBox;

                    if (selectchk.Checked)
                    {
                        //-------------
                        string binCode = lblTus_bin.Text.Trim();

                        ReptPickSerials _reptPickSerial_ = CHNLSVC.Inventory.Get_all_details_on_serialID(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), binCode, itemCode, serID);

                        //Update_inrser_INS_AVAILABLE

                        Boolean update_inr_ser = false;

                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_usrseq_no = generated_seq;
                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                        _reptPickSerial_.Tus_base_doc_no = "N/A";
                        _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(lblInvoiceLine.Text.Trim());
                        _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                        _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                        _reptPickSerial_.Tus_job_no = null;
                        _reptPickSerial_.Tus_pgs_prefix = null;
                        _reptPickSerial_.Tus_job_line = 0;
                        //enter row into TEMP_PICK_SER
                        Int32 affected_rows = -1;

                        if (Session["PSITemLine"] != null)
                        {
                            _reptPickSerial_.Tus_itm_line = Convert.ToInt32(Session["PSITemLine"].ToString());
                        }

                        rowCount++;
                        if (!_isWriteToTemporaryTable)
                        {
                            if (_selectedItemList == null || _selectedItemList.Count <= 0)
                                _selectedItemList = new List<ReptPickSerials>();
                            _selectedItemList.Add(_reptPickSerial_);
                        }
                        //isManualscan = true;

                    }

                }
                if (!_isWriteToTemporaryTable)
                    CommonOutScan_AddSerialClick(_selectedItemList);

            }
            else
            {
                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_usrseq_no = generated_seq;
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_base_doc_no = "N/A"; ;
                _reptPickSerial_.Tus_base_itm_line = Convert.ToInt32(lblInvoiceLine.Text.ToString());
                _reptPickSerial_.Tus_itm_desc = msitem.Mi_shortdesc;
                _reptPickSerial_.Tus_itm_model = msitem.Mi_model;
                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                _reptPickSerial_.Tus_bin = (string)Session["GlbDefaultBin"];
                _reptPickSerial_.Tus_itm_cd = itemCode;
                _reptPickSerial_.Tus_itm_stus = lblItemStatusSer.Text;
                _reptPickSerial_.Tus_qty = Convert.ToDecimal(txtPopupQty.Text.ToString());
                _reptPickSerial_.Tus_ser_1 = "N/A";
                _reptPickSerial_.Tus_ser_2 = "N/A";
                _reptPickSerial_.Tus_ser_3 = "N/A";
                _reptPickSerial_.Tus_ser_4 = "N/A";
                _reptPickSerial_.Tus_ser_id = 0;
                _reptPickSerial_.Tus_serial_id = "0";
                _reptPickSerial_.Tus_job_no = null;
                _reptPickSerial_.Tus_pgs_prefix = null;
                _reptPickSerial_.Tus_job_line = 0;
                // _reptPickSerial_.Tus_unit_cost = 0;
                // _reptPickSerial_.Tus_unit_price = 0;
                // _reptPickSerial_.Tus_unit_price = 0;

                //enter row into TEMP_PICK_SER
                Int32 affected_rows = CHNLSVC.Inventory.SaveAllScanSerials(_reptPickSerial_, null);
            }

            Session["mpPickSerial"] = null;
            mpPickSerial.Hide();
        }

        protected void grdAdSearch_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grdAdSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                grdAdSearch.PageIndex = e.NewPageIndex;
                grdAdSearch.DataSource = null;
                grdAdSearch.DataSource = (List<ReptPickSerials>)Session["SerialPick"];
                grdAdSearch.DataBind();
                txtSearchbywordA.Focus();
                mpPickSerial.Show();
                Session["mpPickSerial"] = "Y";
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }

        private void loadPickSerials(string item, Int32 lineNum, int _ageingDays, string itemStatus)
        {
            lblInvoiceLine.Text = lineNum.ToString();
            lblItemStatusSer.Text = itemStatus;
            List<ReptPickSerials> serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), item, "", "");

            serial_list = setSerialStatusDescriptions(serial_list);

            serial_list = GetAgeItemList(serial_list, _ageingDays);
            Session["SerialPick"] = serial_list;
            grdAdSearch.DataSource = serial_list;
            grdAdSearch.DataBind();

        }

        private List<ReptPickSerials> GetAgeItemList(List<ReptPickSerials> _referance, int _noOfDays)
        {
            List<ReptPickSerials> _ageLst = new List<ReptPickSerials>();

            bool _isAgePriceLevel = false;
            DateTime _documentDate = Convert.ToDateTime(txtdate.Text);
            if (_isAgePriceLevel)
            {
                _ageLst = _referance.Where(x => x.Tus_exist_grndt <= _documentDate.AddDays(-_noOfDays) || (x.Tus_itm_stus == "AGE" || x.Tus_itm_stus == "AGLP")).ToList();
            }
            else
            {
                _ageLst = _referance;
            }

            return _ageLst;

        }

        private void CommonOutScan_AddSerialClick(List<ReptPickSerials> SelectedItemListss)
        {
            try
            {
                List<ReptPickSerials> _ser = SelectedItemListss;
                if (_ser != null && _ser.Count > 0)
                {
                    if (ScanSerialList != null && ScanSerialList.Count > 0)
                    {
                        string _dupsLst = string.Empty;
                        Parallel.ForEach(_ser, x => { var _dups = ScanSerialList.Where(y => y.Tus_ser_1 == x.Tus_ser_1 && y.Tus_itm_cd == x.Tus_itm_cd).Count(); if (_dups != 0) if (string.IsNullOrEmpty(_dupsLst)) _dupsLst = " Item : " + x.Tus_itm_cd + "/Serial : " + x.Tus_ser_1; else _dupsLst += ", Item : " + x.Tus_itm_cd + "/Serial : " + x.Tus_ser_1; });
                        if (!string.IsNullOrEmpty(_dupsLst))
                        {
                            //DisplayMessage("Following Item Serial(s) is duplicating!" + _dupsLst);
                            DisplayMessage("Item serial(s) is duplicating!");
                            _selectedItemList = new List<ReptPickSerials>();
                            return;
                        }
                        else
                        {
                            ScanSerialList.AddRange(_ser);
                        }
                    }
                    else
                        ScanSerialList.AddRange(_ser);

                    gvPopSerial.DataSource = new List<ReptPickSerials>();
                    gvPopSerial.DataSource = setSerialStatusDescriptions(ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList());
                    gvPopSerial.DataBind();

                    foreach (ReptPickSerials item in ScanSerialList)
                    {
                        InvoiceSerial _invser = new InvoiceSerial();
                        _invser.Sap_del_loc = Session["UserDefLoca"].ToString();
                        _invser.Sap_itm_cd = item.Tus_itm_cd;
                        _invser.Sap_itm_line = item.Tus_itm_line;
                        _invser.Sap_remarks = string.Empty;

                        if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                        {
                            InvoiceItem oItem = _invoiceItemList.Find(x => x.Sad_itm_cd == item.Tus_itm_cd && x.Sad_itm_line == item.Tus_itm_line);
                            _invser.Sap_seq_no = oItem.Sad_seq;
                        }

                        _invser.Sap_ser_1 = item.Tus_ser_1;
                        _invser.Sap_ser_line = _isCombineAdding ? Convert.ToInt32(SSCombineLine) : 0;
                        InvoiceSerialList.Add(_invser);
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        #endregion

        protected void btnInvItemDisRate_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                lblDiscountRowNum.Text = dr.RowIndex.ToString();
                mpDiscountRate.Show();
                txtDisRateInvItem.Text = "";
                txtDisRateInvItem.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        #region Invoice Item Discount
        protected void btnCLoseDisRate_Click(object sender, EventArgs e)
        {
            txtDisRateInvItem.Text = "";
            mpDiscountRate.Hide();
        }

        protected void btnApplyDiscountRate_Click(object sender, EventArgs e)
        {
            mpDiscountRate.Show();

            GridViewRow dr = gvInvoiceItem.Rows[Convert.ToInt32(lblDiscountRowNum.Text)];
            Label lblsad_disc_rt = dr.FindControl("lblsad_disc_rt") as Label;
            Label lblsad_itm_line = dr.FindControl("lblsad_itm_line") as Label;
            Label InvItm_Book = dr.FindControl("InvItm_Book") as Label;
            Label InvItm_Level = dr.FindControl("InvItm_Level") as Label;
            Label InvItm_Item = dr.FindControl("InvItm_Item") as Label;
            Label lblsad_itm_stus = dr.FindControl("lblsad_itm_stus") as Label;

            decimal _prevousDisRate = Convert.ToDecimal(lblsad_disc_rt.Text);
            int _lineno0 = Convert.ToInt32(lblsad_itm_line.Text);
            string _book = Convert.ToString(InvItm_Book.Text);
            string _level = Convert.ToString(InvItm_Level.Text);
            string _item = Convert.ToString(InvItm_Item.Text);
            string _status = Convert.ToString(lblsad_itm_stus.Text);
            bool _isSerialized = false;

            string _userDisRate = txtDisRateInvItem.Text.Trim();
            if (string.IsNullOrEmpty(_userDisRate))
                return;
            if (IsNumeric(_userDisRate) == false || Convert.ToDecimal(_userDisRate) > 100 || Convert.ToDecimal(_userDisRate) < 0)
            {
                DisplayMessage("Please select the valid discount rate");
                return;
            }
            decimal _disRate = Convert.ToDecimal(_userDisRate);
            bool _IsPromoVou = false;
            if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();

            //  if (string.IsNullOrEmpty(lblPromoVouNo.Text))
            // {
            GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToDateTime(txtdate.Text.Trim()).Date, _book, _level, txtCustomer.Text.Trim(), _item, _isSerialized, false);
            // }
            //else
            //{
            //    GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCustomer.Text.Trim(), Convert.ToDateTime(txtdate.Text.Trim()).Date, _book, _level, _item, lblPromoVouNo.Text.Trim());
            //    if (GeneralDiscount != null)
            //    {
            //        _IsPromoVou = true;
            //        GeneralDiscount.Sgdd_seq = Convert.ToInt32(lblPromoVouNo.Text);
            //    }
            //}


            if (GeneralDiscount != null)
            {
                decimal vals = GeneralDiscount.Sgdd_disc_val;
                decimal rates = GeneralDiscount.Sgdd_disc_rt;

                if (lblPromoVouUsedFlag.Text.Contains("U") == true)
                {
                    DisplayMessage("Voucher already used!");
                    txtDisRate.Text = FormatToCurrency("0");
                    _isEditDiscount = false;
                    return;
                }
                if (_IsPromoVou == true)
                {
                    if (rates == 0 && vals > 0)
                    {
                        // CalculateItem();
                        if (Convert.ToDecimal(txtDisAmt.Text) > vals)
                        {
                            DisplayMessage("Voucher discount amount should be " + vals + ".\nNot allowed discount rate " + _disRate + "%");
                            _isEditDiscount = false;
                            return;
                        }
                    }
                    else
                    {
                        if (rates != _disRate)
                        {
                            DisplayMessage("Voucher discount rate should be " + rates + "% !.\nNot allowed discount rate " + _disRate + "% discounted price is " + txtLineTotAmt.Text);
                            _isEditDiscount = false;
                            return;
                        }
                    }
                }
                else
                {
                    if (rates < _disRate)
                    {
                        DisplayMessage("You cannot apply discount more than " + rates + "%.");
                        return;
                    }
                }
            }
            else
            {
                DisplayMessage("You are not allow for apply discount");
                return;
            }

            if (_isEditDiscount == true)
            {
                if (_IsPromoVou == true)
                {
                    //lblPromoVouUsedFlag.Text = "U";
                    _proVouInvcItem = _item;
                }
            }

            var _itm = _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList();
            if (_item != null && _item.Count() > 0) foreach (InvoiceItem _one in _itm)
                {
                    CalculateGrandTotal(_one.Sad_qty, _one.Sad_unit_rt, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, false);
                    decimal _unitRate = _one.Sad_unit_rt;
                    decimal _unitAmt = _one.Sad_unit_amt;
                    decimal _disVal = 0;
                    decimal _vatPortion = 0;
                    decimal _lineamount = 0;
                    decimal _newvatval = 0;

                    bool _isTaxDiscount = chkTaxPayable.Checked ? true : (lblSVatStatus.Text == "Available") ? true : false;

                    if (_isTaxDiscount)
                    {
                        _vatPortion = FigureRoundUp(TaxCalculation(_item, _status, _one.Sad_qty, _priceBookLevelRef, _unitRate, _disVal, _disRate, true), true);
                        _disVal = FigureRoundUp(_unitAmt * _disRate / 100, true);
                        _lineamount = FigureRoundUp(_unitRate * _one.Sad_qty + _vatPortion - _disVal, true);
                    }
                    else
                    {
                        _vatPortion = FigureRoundUp(TaxCalculation(_item, _status, _one.Sad_qty, _priceBookLevelRef, _unitRate, _disVal, _disRate, true), true);
                        _disVal = FigureRoundUp((_unitAmt + _vatPortion) * _disRate / 100, true);

                        if (_disRate > 0)
                        {
                            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, string.Empty);
                            if (_tax != null && _tax.Count > 0)
                            {
                                _newvatval = ((_unitRate * _one.Sad_qty + _vatPortion - _disVal) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                            }
                        }
                        if (_disRate > 0)
                        {
                            _lineamount = FigureRoundUp(_unitRate * _one.Sad_qty + _vatPortion - _disVal, true);
                            _vatPortion = FigureRoundUp(_newvatval, true);
                        }
                        else
                        {
                            _lineamount = FigureRoundUp(_unitRate * _one.Sad_qty + _vatPortion - _disVal, true);
                        }
                    }
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_disc_rt = _disRate);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_disc_amt = _disVal);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_itm_tax_amt = _vatPortion);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_tot_amt = FigureRoundUp(_lineamount, true));
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_seq = GeneralDiscount.Sgdd_seq);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_type = "M");

                    if (_proVouInvcItem == txtItem.Text.ToString())
                    {
                        //if (string.IsNullOrEmpty(lblPromoVouUsedFlag.Text) && !string.IsNullOrEmpty(lblPromoVouNo.Text))
                        //{
                        //    lblPromoVouUsedFlag.Text = "U";
                        //    _proVouInvcLine = _lineno0;
                        //    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_res_line_no = Convert.ToInt32(lblPromoVouNo.Text.ToString()));
                        //    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_res_no = "PROMO_VOU");
                        //    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_seq = Convert.ToInt32(lblPromoVouNo.Text.ToString()));
                        //    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_type = "M");
                        //    //_tempItem.Sad_res_line_no = Convert.ToInt32(lblPromoVouNo.Text.ToString());
                        //    //_tempItem.Sad_res_no = "PROMO_VOU";
                        //}
                    }
                    BindAddItem();
                    CalculateGrandTotal(_one.Sad_qty, _unitRate, _disVal, _vatPortion, true);
                    decimal _tobepays = 0;
                    if (lblSVatStatus.Text == "Available") _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim()); else _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());


                    mpDiscountRate.Hide();
                }

        }
        #endregion

        private void loadItemStatus()
        {
            oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
        }

        private List<InvoiceItem> setItemDescriptions(List<InvoiceItem> itemList)
        {
            if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
            {
                foreach (InvoiceItem item in itemList)
                {
                    MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == item.Sad_itm_stus);
                    if (oStatus != null)
                    {
                        item.Sad_itm_stus_desc = oStatus.Mis_desc;
                    }
                    else
                    {
                        item.Sad_itm_stus_desc = item.Mi_itm_stus;
                    }
                }
            }

            return itemList;
        }

        private DataTable setItemDescriptionsDt(DataTable itemList)
        {
            if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
            {
                for (int i = 0; i < itemList.Rows.Count; i++)
                {
                    MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == itemList.Rows[i]["Sad_itm_stus"]);
                    if (oStatus != null)
                    {
                        itemList.Rows[i]["Sad_itm_stus_desc"] = oStatus.Mis_desc;
                    }
                    else
                    {
                        itemList.Rows[i]["Sad_itm_stus_desc"] = itemList.Rows[i]["Sad_itm_stus"].ToString();
                    }
                }
            }

            return itemList;
        }

        private List<ReptPickSerials> setSerialStatusDescriptions(List<ReptPickSerials> itemList)
        {
            if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
            {
                foreach (ReptPickSerials item in itemList)
                {
                    MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == item.Tus_itm_stus);
                    if (oStatus != null)
                    {
                        item.Tus_itm_stus_Desc = oStatus.Mis_desc;
                    }
                    else
                    {
                        item.Tus_itm_stus_Desc = item.Tus_itm_stus;
                    }
                }
            }
            return itemList;
        }

        private DataTable setSerialStatusDescriptionsDt(DataTable itemList)
        {
            if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
            {
                for (int i = 0; i < itemList.Rows.Count; i++)
                {
                    MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == itemList.Rows[i]["Tus_itm_stus"].ToString());
                    if (oStatus != null)
                    {
                        itemList.Rows[i]["Tus_itm_stus_Desc"] = oStatus.Mis_desc;
                        //item.Tus_itm_stus_Desc = oStatus.Mis_desc;
                    }
                    else
                    {
                        itemList.Rows[i]["Tus_itm_stus_Desc"] = itemList.Rows[i]["Tus_itm_stus"].ToString();
                        //item.Tus_itm_stus_Desc = item.Tus_itm_stus;
                    }
                }
            }
            return itemList;
        }

        private void ClearVariables()
        {
            _invoiceItemList = new List<InvoiceItem>();
            _invoiceItemListWithDiscount = new List<InvoiceItem>();
            _recieptItem = new List<RecieptItem>();
            _newRecieptItem = new List<RecieptItem>();
            _businessEntity = new MasterBusinessEntity();
            _masterItemComponent = new List<MasterItemComponent>();
            _priceBookLevelRef = new PriceBookLevelRef();
            _priceBookLevelRefList = new List<PriceBookLevelRef>();
            _priceDetailRef = new List<PriceDetailRef>();
            _masterBusinessCompany = new MasterBusinessEntity();
            _MainPriceSerial = new List<PriceSerialRef>();
            _tempPriceSerial = new List<PriceSerialRef>();
            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            _isInventoryCombineAdded = false;
            ScanSequanceNo = 0;
            ScanSerialList = new List<ReptPickSerials>();
            IsPriceLevelAllowDoAnyStatus = false;
            WarrantyRemarks = string.Empty;
            WarrantyPeriod = 0;
            ScanSerialNo = string.Empty;
            DefaultItemStatus = string.Empty;
            InvoiceSerialList = new List<InvoiceSerial>();
            InventoryCombinItemSerialList = new List<ReptPickSerials>();
            PriceCombinItemSerialList = new List<ReptPickSerials>();
            BuyBackItemList = new List<ReptPickSerials>();
            _lineNo = 0;
            _isEditPrice = false;
            _isEditDiscount = false;
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            _toBePayNewAmount = 0;
            _isCompleteCode = false;
            SSPriceBookPrice = 0;
            SSPriceBookSequance = string.Empty;
            SSPriceBookItemSequance = string.Empty;
            SSIsLevelSerialized = string.Empty;
            SSPromotionCode = string.Empty;
            SSCirculerCode = string.Empty;
            SSPRomotionType = 0;
            SSCombineLine = 0;
            ManagerDiscount = new Dictionary<decimal, decimal>();
            GeneralDiscount = new CashGeneralEntiryDiscountDef();
            DefaultBook = string.Empty;
            DefaultLevel = string.Empty;
            DefaultInvoiceType = string.Empty;
            DefaultStatus = string.Empty;
            DefaultBin = string.Empty;
            _itemdetail = new MasterItem();
            MainTaxConstant = new List<MasterItemTax>();
            _promotionSerial = new List<ReptPickSerials>();
            _promotionSerialTemp = new List<ReptPickSerials>();
            _isBackDate = false;
            _MasterProfitCenter = new MasterProfitCenter();
            _PriceDefinitionRef = new List<PriceDefinitionRef>();
            _isGiftVoucherCheckBoxClick = false;
            MasterChannel = new DataTable();
            VirtualCounter = 1;
            IsToken = false;
            IsSaleFigureRoundUp = false;
            _tblExecutive = new DataTable();
            IsDlvSaleCancelAllowUser = false;
            _IsVirtualItem = false;
            _isNewPromotionProcess = false;
            technicianCode = string.Empty;
            _iswhat = false;
            _tblPromotor = new DataTable();
            _serialMatch = false;
            _priorityPriceBook = new PriortyPriceBook();
            _processMinusBalance = false;
            _discountSequence = 0;
            _isRegistrationMandatory = false;
            _isNeedRegistrationReciept = false;
            _totalRegistration = 0;
            _loyaltyType = new LoyaltyType();
            _proVouInvcLine = 0;
            _proVouInvcItem = string.Empty;
            _isGroup = false;
            _serverDt = DateTime.Now;
            _isCombineAdding = false;
            _combineCounter = 0;
            _paymodedef = string.Empty;
            _isCheckedPriceCombine = false;
            _isFirstPriceComItem = false;
            _serial2 = string.Empty;
            _prefix = string.Empty;
            _isBlocked = false;
            _isItemChecking = false;
            _levelStatus = new DataTable();
            _userid = string.Empty;
            exchangerate = 0;
            _entity = new MasterBusinessEntity();
            _stopit = false;
            uniqueitems = new DataTable();
            dtiInvtems = new DataTable();
            _List = new List<RegistrationList>();
            _PriceDetailRefPromo = new List<PriceDetailRef>();
            _PriceSerialRefPromo = new List<PriceSerialRef>();
            _PriceSerialRefNormal = new List<PriceSerialRef>();
            _CashGeneralEntiryDiscount = new List<CashGeneralEntiryDiscountDef>();
            _isGiftVoucherMsgPopup = false;
            _selectedItemList = new List<ReptPickSerials>();
            oMasterItemStatuss = new List<MasterItemStatus>();

            Session["mpPriceNPromotion"] = null;
            Session["mpPickSerial"] = null;
            Session["_cusCode"] = null;
            Session["_isFromOther"] = null;
            Session["ucc"] = null;
            Session["DCUSCODE"] = null;
            Session["DCUSNAME"] = null;
            Session["DTOWN"] = null;
            Session["DCUSADD1"] = null;
            Session["DCUSADD2"] = null;
            Session["DCUSLOC"] = null;
            Session["RSEQNO"] = null;
            Session["_PriceDetailRefPromo"] = null;
            Session["_PriceSerialRefPromo"] = null;
            Session["_PriceSerialRefNormal"] = null;
            Session["CUSHASCOMPANY"] = null;
            Session["CUSCOM"] = null;
            Session["CUSLOC"] = null;
            Session["oBBNewItems"] = null;
            Session["Promotion"] = null;
            Session["SelectedPromoItmRow"] = null;
        }

        private void WriteErrLog(string err)
        {
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/data.txt"), true))
            {
                _testData.WriteLine(DateTime.Now.ToString("dd/MMM/yyyy  hh:mm:ss tt") + " - " + GetIPAddress() + "  | " + err);
            }
        }

        private string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        private void SetLoyalityColor()
        {
            if (!string.IsNullOrEmpty(txtLoyalty.Text.Trim()))
            {
                txtLoyalty.BackColor = Color.Goldenrod;
            }
            else
            {
                txtLoyalty.BackColor = Color.Transparent;
            }
        }

        private void ClearAll()
        {
            ClearVariables();
            Clear();
            Invoice();
            InitializeValuesNDefaultValueSet();
            Invoice_Load();
            LoadInvoiceProfitCenterDetail();
            ViewState["ITEMSTABLE"] = null;
            DataTable dtitems = new DataTable();
            dtitems.Columns.AddRange(new DataColumn[15] { new DataColumn("soi_itm_line"), new DataColumn("soi_itm_cd"), new DataColumn("description"), new DataColumn("soi_itm_stus"), new DataColumn("soi_qty"), new DataColumn("soi_unit_rt"), new DataColumn("soi_unit_amt"), new DataColumn("soi_disc_rt"), new DataColumn("soi_disc_amt"), new DataColumn("soi_itm_tax_amt"), new DataColumn("soi_tot_amt"), new DataColumn("soi_pbook"), new DataColumn("soi_pb_lvl"), new DataColumn("soi_res_no"), new DataColumn("itri_seq_no") });
            ViewState["ITEMSTABLE"] = dtitems;
            this.BindItemsGrid();

            ViewState["SERIALSTABLE"] = null;
            DataTable dtserials = new DataTable();
            dtserials.Columns.AddRange(new DataColumn[8] { new DataColumn("sose_itm_line"), new DataColumn("sose_itm_cd"), new DataColumn("Model"), new DataColumn("Status"), new DataColumn("Qty"), new DataColumn("sose_ser_1"), new DataColumn("sose_ser_2"), new DataColumn("Warranty") });
            ViewState["SERIALSTABLE"] = dtserials;
            this.BindSerialsGrid();

            txtDisRate.Text = "0";
            txtDisAmt.Text = "0";
            txtTaxAmt.Text = "0";
            txtLineTotAmt.Text = "0";

            txtdocrefno.Focus();
            Session["ucc"] = null;
            hdfShowCustomer.Value = null;

            cmbExecutive.SelectedIndex = 0;
            txtexcutive.Text = string.Empty;
            grvwarehouseitms.DataSource = new int[] { };
            grvwarehouseitms.DataBind();

        }

        protected void btnDelCustomerSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //if (IsAllovcateCustomer(txtexcutive.Text))
                //{
                //     ViewState["SEARCH"] = null;
                //    txtSearchbyword.Text = string.Empty;
                //    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                //    DataTable result = CHNLSVC.CommonSearch.GetCustomerBYsalesExe(SearchParams, null, null);
                //    grdResult.DataSource = result;
                //    grdResult.DataBind();
                //    lblvalue.Text = "Customer_Sal";
                //    BindUCtrlDDLData(result);
                //    ViewState["SEARCH"] = result;
                //    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                //    txtSearchbyword.Text = "";
                //    txtSearchbyword.Focus();
                //    return;
                //}



                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams2 = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                DataTable result2 = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams2, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                grdResult.DataSource = result2;
                grdResult.DataBind();
                lblvalue.Text = "Customer_DEL";
                BindUCtrlDDLData(result2);
                ViewState["SEARCH"] = result2;
                SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtPoNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPoNo.Text.Trim()) && !string.IsNullOrEmpty(txtCustomer.Text.Trim()))
            {
                MasterBusinessEntity _masterBusinessCompany = CHNLSVC.Sales.GetCustomerProfileByCom(txtCustomer.Text, null, null, null, null, Session["UserCompanyCode"].ToString());
                if (_masterBusinessCompany != null && _masterBusinessCompany.Mbe_intr_com && _masterBusinessCompany.Mbe_tp == "C")
                {
                    List<InterCompanySalesParameter> oInterCompanySalesParameters = CHNLSVC.Sales.GET_INTERCOM_PAR_BY_CUST(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                    if (oInterCompanySalesParameters != null && oInterCompanySalesParameters.Count > 0)
                    {
                        btnSave.OnClientClick = "ConfirmPlaceOrder();";
                        btnSave.CssClass = "buttonUndocolor";

                        PurchaseOrder oPurchaseOrder = CHNLSVC.Inventory.GetPurchaseOrderHeaderDetails(oInterCompanySalesParameters[0].Sritc_to_com, txtPoNo.Text.Trim());
                        if (oPurchaseOrder != null && !string.IsNullOrEmpty(oPurchaseOrder.Poh_doc_no))
                        {
                            if (oInterCompanySalesParameters.FindAll(x => x.Sritc_sup == oPurchaseOrder.Poh_supp).Count > 0)
                            {
                                if (oPurchaseOrder.Poh_stus == "C")
                                {
                                    DisplayMessage("Selected purchase order is canceled.");
                                    txtPoNo.Text = "";
                                }
                                else if (oPurchaseOrder.Poh_stus == "P")
                                {
                                    DisplayMessage("Selected purchase order is not approved.");
                                    txtPoNo.Text = "";
                                }
                                else if (oPurchaseOrder.Poh_stus == "U")
                                {
                                    DisplayMessage("Selected purchase order is already used.");
                                    txtPoNo.Text = "";
                                }
                            }
                            else
                            {
                                DisplayMessage("Selected purchase order supplier is not related for the customer.");
                                txtPoNo.Text = "";
                            }
                        }
                        else
                        {
                            DisplayMessage("Purchase order is invalid.");
                            txtPoNo.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        DisplayMessage("Inter company parameters are not setuped.");
                        btnSave.OnClientClick = "";
                        btnSave.CssClass = "buttoncolor";
                        return;
                    }
                }
            }
        }

        protected void btnSave_temp_Click(object sender, EventArgs e)
        {
            btnSave_Click(null, null);
        }

        #region Save PO Confirmation

        protected void btnSavePOYes_Click(object sender, EventArgs e)
        {
            hdfSavePO.Value = "YY";
            mpSavePO.Hide();
            btnSave_Click(null, null);
        }

        protected void btnSavePONo_Click(object sender, EventArgs e)
        {
            mpSavePO.Hide();
        }
        #endregion

        protected void gvGiftVoucher_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlItems = (e.Row.FindControl("ddlItems") as DropDownList);
                    if (_invoiceItemList.Count > 0)
                    {
                        List<string> oItemList = _invoiceItemList.Where(x => x.Mi_itm_tp != "G").Select(t => t.Sad_itm_cd).ToList();
                        oItemList.Insert(0, "");
                        ddlItems.DataSource = oItemList;
                        ddlItems.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void txtCusName_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCusName.Text))
            {
                txtdelname.Text = txtCusName.Text.Trim();
            }
        }

        protected void txtAddress1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddress1.Text))
            {
                txtdelad1.Text = txtAddress1.Text.Trim();
            }
        }

        protected void txtAddress2_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtAddress2.Text))
            {
                txtdelad2.Text = txtAddress2.Text.Trim();
            }

        }

        #region Sales Order

        protected void btnCloseSalesorder_Click(object sender, EventArgs e)
        {

        }

        protected void txtSalesOrderSearch_TextChanged(object sender, EventArgs e)
        {
            mpLoadSalesOrder.Show();
        }

        protected void btnSearchSalesOrder_Click(object sender, EventArgs e)
        {
            //ViewState["SEARCH"] = null;
            //txtSearchbyword.Text = string.Empty;
            //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
            //DataTable result = CHNLSVC.Sales.SearchSalesOrder(SearchParams, null, null);
            //grdResult.DataSource = result;
            //grdResult.DataBind();
            //lblvalue.Text = "SalesOrderNew";
            //BindUCtrlDDLData(result);
            //ViewState["SEARCH"] = result;
            //SIPopup.Show();
            //txtSearchbyword.Focus();
        }

        protected void btnLoadSalesOrder_Click(object sender, EventArgs e)
        {
            LoadSalesOrderDetails();
            //LoadSoData(txtSalesOrderSearch.Text, Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            //LoadSoItemData(txtSalesOrderSearch.Text);
            //LoadSoSerialData(txtSalesOrderSearch.Text);
        }

        protected void chekBasedOnSalesOrder_CheckedChanged(object sender, EventArgs e)
        {

        }

        private List<InvoiceItem> SalesOrderItmToInvoiceItem(DataTable dtItems)
        {
            List<InvoiceItem> oItems = new List<InvoiceItem>();

            for (int i = 0; i < dtItems.Rows.Count; i++)
            {
                MasterItem oMasterItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), dtItems.Rows[i]["SOI_ITM_CD"].ToString());
                InvoiceItem oNewItem = new InvoiceItem();
                //oNewItem.Sad_seq_no = Convert.ToInt32(dtItems.Rows[i]["SOI_SEQ_NO"].ToString());
                oNewItem.Sad_itm_line = Convert.ToInt32(dtItems.Rows[i]["SOI_ITM_LINE"].ToString());
                //oNewItem.Sad_so_no = dtItems.Rows[i]["SOI_SO_NO"].ToString();
                oNewItem.Sad_itm_cd = dtItems.Rows[i]["SOI_ITM_CD"].ToString();
                oNewItem.Sad_itm_stus = dtItems.Rows[i]["SOI_ITM_STUS"].ToString();
                oNewItem.Sad_itm_tp = oMasterItem.Mi_itm_tp;
                oNewItem.Sad_uom = oMasterItem.Mi_itm_uom;
                oNewItem.Sad_qty = Convert.ToDecimal(dtItems.Rows[i]["SOI_QTY"].ToString());
                //oNewItem.Sad_inv_qty =  dtItems.Rows[i]["SOI_INV_QTY"].ToString();
                oNewItem.Sad_unit_rt = Convert.ToDecimal(dtItems.Rows[i]["SOI_UNIT_RT"].ToString());
                oNewItem.Sad_unit_amt = Convert.ToDecimal(dtItems.Rows[i]["SOI_UNIT_AMT"].ToString());
                oNewItem.Sad_disc_rt = Convert.ToDecimal(dtItems.Rows[i]["SOI_DISC_RT"].ToString());
                oNewItem.Sad_disc_amt = Convert.ToDecimal(dtItems.Rows[i]["SOI_DISC_AMT"].ToString());
                oNewItem.Sad_itm_tax_amt = Convert.ToDecimal(dtItems.Rows[i]["SOI_ITM_TAX_AMT"].ToString());
                oNewItem.Sad_tot_amt = Convert.ToDecimal(dtItems.Rows[i]["SOI_TOT_AMT"].ToString());
                oNewItem.Sad_pbook = dtItems.Rows[i]["SOI_PBOOK"].ToString();
                oNewItem.Sad_pb_lvl = dtItems.Rows[i]["SOI_PB_LVL"].ToString();
                oNewItem.Sad_pb_price = Convert.ToDecimal(dtItems.Rows[i]["SOI_PB_PRICE"].ToString());
                oNewItem.Sad_seq = Convert.ToInt32(dtItems.Rows[i]["SOI_SEQ"].ToString());
                oNewItem.Sad_itm_seq = Convert.ToInt32(dtItems.Rows[i]["SOI_ITM_SEQ"].ToString());
                oNewItem.Sad_warr_period = Convert.ToInt32(dtItems.Rows[i]["SOI_WARR_PERIOD"].ToString());
                oNewItem.Sad_warr_remarks = dtItems.Rows[i]["SOI_WARR_REMARKS"].ToString();
                oNewItem.Sad_is_promo = Convert.ToBoolean(dtItems.Rows[i]["SOI_IS_PROMO"]);
                oNewItem.Sad_promo_cd = dtItems.Rows[i]["SOI_PROMO_CD"].ToString();
                oNewItem.Sad_alt_itm_cd = dtItems.Rows[i]["SOI_ALT_ITM_CD"].ToString();
                oNewItem.Sad_alt_itm_desc = dtItems.Rows[i]["SOI_ALT_ITM_DESC"].ToString();
                oNewItem.Sad_print_stus = Convert.ToBoolean(dtItems.Rows[i]["SOI_PRINT_STUS"]);
                oNewItem.Sad_res_no = dtItems.Rows[i]["SOI_RES_NO"].ToString();
                oNewItem.Sad_res_line_no = Convert.ToInt32(dtItems.Rows[i]["SOI_RES_LINE_NO"].ToString());
                oNewItem.Sad_job_no = dtItems.Rows[i]["SOI_JOB_NO"].ToString();
                oNewItem.Sad_warr_based = Convert.ToBoolean(dtItems.Rows[i]["SOI_WARR_BASED"]);
                oNewItem.Sad_merge_itm = dtItems.Rows[i]["SOI_MERGE_ITM"].ToString();
                oNewItem.Sad_job_line = Convert.ToInt32(dtItems.Rows[i]["SOI_JOB_LINE"].ToString());
                oNewItem.Sad_outlet_dept = dtItems.Rows[i]["SOI_OUTLET_DEPT"].ToString();
                oNewItem.Sad_trd_svc_chrg = Convert.ToDecimal(dtItems.Rows[i]["SOI_TRD_SVC_CHRG"].ToString());
                oNewItem.Sad_dis_seq = Convert.ToInt32(dtItems.Rows[i]["SOI_DIS_SEQ"].ToString());
                oNewItem.Sad_dis_line = Convert.ToInt32(dtItems.Rows[i]["SOI_DIS_LINE"].ToString());
                oNewItem.Sad_dis_type = dtItems.Rows[i]["SOI_DIS_TYPE"].ToString();
                oItems.Add(oNewItem);
            }
            return oItems;
        }

        private List<InvoiceSerial> SalesOrderSerialToInvoiceSerial(DataTable dtSerials, out List<ReptPickSerials> oSerials)
        {
            oSerials = new List<ReptPickSerials>();
            List<InvoiceSerial> oResult = new List<InvoiceSerial>();
            for (int i = 0; i < dtSerials.Rows.Count; i++)
            {
                InvoiceSerial oNewSerial = new InvoiceSerial();
                oNewSerial.Sap_seq_no = Convert.ToInt32(dtSerials.Rows[i]["SOSE_SEQ_NO"].ToString());
                oNewSerial.Sap_itm_line = Convert.ToInt32(dtSerials.Rows[i]["SOSE_ITM_LINE"].ToString());
                oNewSerial.Sap_inv_no = dtSerials.Rows[i]["SOSE_SO_NO"].ToString();
                oNewSerial.Sap_itm_cd = dtSerials.Rows[i]["SOSE_ITM_CD"].ToString();
                oNewSerial.Sap_ser_1 = dtSerials.Rows[i]["SOSE_SER_1"].ToString();
                oNewSerial.Sap_remarks = dtSerials.Rows[i]["SOSE_REMARKS"].ToString();
                oNewSerial.Sap_sev_loc = dtSerials.Rows[i]["SOSE_SEV_LOC"].ToString();
                oNewSerial.Sap_del_loc = dtSerials.Rows[i]["SOSE_DEL_LOC"].ToString();
                oNewSerial.Sap_ser_line = Convert.ToInt32(dtSerials.Rows[i]["SOSE_SER_LINE"].ToString());
                oNewSerial.Sap_ser_2 = dtSerials.Rows[i]["SOSE_SER_2"].ToString();
                oResult.Add(oNewSerial);

                MasterItem oItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), oNewSerial.Sap_itm_cd);

                ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                _reptPickSerial_.Tus_base_doc_no = Convert.ToString(-100);
                _reptPickSerial_.Tus_base_itm_line = oNewSerial.Sap_itm_line;
                _reptPickSerial_.Tus_bin = (string)Session["GlbDefaultBin"];
                _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                _reptPickSerial_.Tus_cre_dt = DateTime.Now;
                _reptPickSerial_.Tus_cross_batchline = 0;
                _reptPickSerial_.Tus_cross_itemline = 0;
                _reptPickSerial_.Tus_cross_seqno = 0;
                _reptPickSerial_.Tus_cross_serline = 0;
                //_reptPickSerial_.Tus_doc_dt = DateTime.Now;
                //_reptPickSerial_.Tus_doc_no = _invSerials[0].Ins_doc_no;
                //_reptPickSerial_.Tus_exist_grncom = _invSerials[0].Ins_exist_grncom;
                _reptPickSerial_.Tus_isapp = 1;
                _reptPickSerial_.Tus_iscovernote = 1;
                _reptPickSerial_.Tus_itm_brand = oItem.Mi_brand;
                _reptPickSerial_.Tus_itm_cd = oNewSerial.Sap_itm_cd;
                _reptPickSerial_.Tus_itm_desc = oItem.Mi_longdesc;
                _reptPickSerial_.Tus_itm_line = oNewSerial.Sap_itm_line;
                _reptPickSerial_.Tus_itm_model = oItem.Mi_model;

                if (_invoiceItemList.Count > 0 && _invoiceItemList.FindAll(x => x.Sad_itm_cd == oNewSerial.Sap_itm_cd).Count > 0)
                {
                    _reptPickSerial_.Tus_itm_stus = _invoiceItemList.Find(x => x.Sad_itm_cd == oNewSerial.Sap_itm_cd).Sad_itm_stus;
                    _reptPickSerial_.Tus_qty = _invoiceItemList.Find(x => x.Sad_itm_cd == oNewSerial.Sap_itm_cd).Sad_qty;
                }

                _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                _reptPickSerial_.Tus_new_status = string.Empty;
                _reptPickSerial_.Tus_ser_1 = oNewSerial.Sap_ser_1;
                _reptPickSerial_.Tus_ser_2 = dtSerials.Rows[i]["SOSE_SER_2"].ToString();
                _reptPickSerial_.Tus_ser_id = 0;
                _reptPickSerial_.Tus_ser_line = 0;
                _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();
                _reptPickSerial_.Tus_unit_cost = 0;
                _reptPickSerial_.Tus_unit_price = 0;
                _reptPickSerial_.Tus_usrseq_no = -100;
                _reptPickSerial_.Tus_warr_no = string.Empty;
                _reptPickSerial_.Tus_warr_period = 0;
                _reptPickSerial_.Tus_new_remarks = string.Empty;
                oSerials.Add(_reptPickSerial_);
            }
            return oResult;
        }

        private void LoadSalesOrderDetails()
        {
            if (string.IsNullOrEmpty(txtSalesOrderSearch.Text.Trim()))
            {
                return;
            }
            try
            {
                DataTable dtHeadersSo = CHNLSVC.Sales.SearchSalesOrdData(txtSalesOrderSearch.Text, Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                if (dtHeadersSo.Rows.Count > 0)
                {
                    foreach (DataRow item in dtHeadersSo.Rows)
                    {
                        Session["SOSEQNO"] = item[25].ToString();

                        DateTime oreddate = Convert.ToDateTime(item[3].ToString());
                        string oreddatetext = oreddate.ToString("dd/MMM/yyyy");
                        txtdate.Text = oreddatetext;

                        cmbInvType.SelectedValue = item[1].ToString();
                        txtManualRefNo.Text = item[5].ToString();
                        txtdocrefno.Text = item[6].ToString();
                        txtCustomer.Text = item[7].ToString();
                        txtCustomer_TextChanged(null, null);
                        txtCusName.Text = item[8].ToString();
                        txtAddress1.Text = item[9].ToString();
                        txtAddress2.Text = item[10].ToString();
                        txtcurrency.Text = item[11].ToString();
                        cmbExecutive.SelectedValue = item[17].ToString();
                        txtexcutive.Text = item[17].ToString();
                        txtQuotation.Text = item[18].ToString();
                        txtPromotor.Text = item[20].ToString();
                        txtLoyalty.Text = item[26].ToString();
                        SetLoyalityColor();

                        if (item[27].ToString() == "1")
                        {
                            chkTaxPayable.Checked = true;
                        }
                        else
                        {
                            chkTaxPayable.Checked = false;
                        }

                        if (item[28].ToString() == "1")
                        {
                            lblVatExemptStatus.Text = "Available";
                        }
                        else
                        {
                            lblVatExemptStatus.Text = "None";
                        }

                        if (item[22].ToString() == "1")
                        {
                            lblSVatStatus.Text = "Available";
                        }
                        else
                        {
                            lblSVatStatus.Text = "None";
                        }

                        txtlocation.Text = item[29].ToString();
                    }
                }

                DataTable dtitems = CHNLSVC.Sales.GET_SAO_ITEMS_BY_SO_NO(txtSalesOrderSearch.Text.Trim());
                _invoiceItemList = SalesOrderItmToInvoiceItem(dtitems);

                //get serial
                //_//List<QuotationSerial> _serialList = CHNLSVC.Sales.GetQuoSerials(txtQuotation.Text.Trim());

                List<ReptPickSerials> oSerials = null;
                DataTable dtserialdata = CHNLSVC.Sales.GET_SAO_SER_BY_SO_NO(txtSalesOrderSearch.Text.Trim());
                if (dtserialdata.Rows.Count > 0)
                    SalesOrderSerialToInvoiceSerial(dtserialdata, out oSerials);

                if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                {
                    _invoiceItemList.ForEach(X => X.Sad_job_line = X.Sad_itm_line);

                    #region Check For Inventory Balance if Delivered Now

                    if (1 == 1)
                    {
                        bool _isPricelevelallowforDOanystatus = false;
                        string _balanceexceedList = string.Empty;
                        foreach (InvoiceItem _itm in _invoiceItemList)
                        {
                            //------------------------------------------------------------------------------------------------
                            if (!string.IsNullOrEmpty(_itm.Sad_pbook) && !string.IsNullOrEmpty(_itm.Sad_pb_lvl))
                            {
                                List<PriceBookLevelRef> _lvl = CHNLSVC.Sales.GetPriceLevelList(Session["UserCompanyCode"].ToString(), _itm.Sad_pbook, _itm.Sad_pb_lvl);
                                if (_lvl != null)
                                    if (_lvl.Count > 0)
                                    {
                                        var _bool = from _l in _lvl where _l.Sapl_chk_st_tp == true select _l.Sapl_chk_st_tp;
                                        if (_bool != null)
                                            if (_bool.Count() > 0)
                                                _isPricelevelallowforDOanystatus = false;
                                            else
                                                _isPricelevelallowforDOanystatus = true;
                                        else
                                            _isPricelevelallowforDOanystatus = true;
                                    }
                            }
                            else
                                _isPricelevelallowforDOanystatus = true;

                            //------------------------------------------------------------------------------------------------
                            decimal _pickQty = 0;
                            if (_isPricelevelallowforDOanystatus)
                                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _itm.Sad_itm_cd).ToList().Select(x => x.Sad_qty).Sum();
                            else
                                _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _itm.Sad_itm_cd && x.Mi_itm_stus == _itm.Mi_itm_stus).ToList().Select(x => x.Sad_qty).Sum();

                            _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                            List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Sad_itm_cd, _itm.Mi_itm_stus);

                            if (_inventoryLocation != null && _inventoryLocation.Count > 0)
                            {
                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                if (_pickQty > _invBal)

                                    if (string.IsNullOrEmpty(_balanceexceedList))
                                        _balanceexceedList = _itm.Sad_itm_cd;
                                    else
                                        _balanceexceedList = ", " + _itm.Sad_itm_cd;
                            }
                            else
                                if (string.IsNullOrEmpty(_balanceexceedList))
                                    _balanceexceedList = _itm.Sad_itm_cd;
                                else
                                    _balanceexceedList = ", " + _itm.Sad_itm_cd;
                        }

                        if (!string.IsNullOrEmpty(_balanceexceedList))
                        {
                            _invoiceItemList = new List<InvoiceItem>();
                            ScanSerialList = new List<ReptPickSerials>();
                            InvoiceSerialList = new List<InvoiceSerial>();
                            DisplayMessage("Item(s) inventory balance exceeds");
                            return;
                        }
                        //InvItm_SerialAdd.Visible = true;
                    }

                    #endregion Check For Inventory Balance if Delivered Now

                    GrndSubTotal = 0;
                    GrndDiscount = 0;
                    GrndTax = 0;

                    foreach (InvoiceItem itm in _invoiceItemList)
                    {
                        CalculateGrandTotal(itm.Sad_qty, itm.Sad_unit_rt, itm.Sad_disc_amt, itm.Sad_itm_tax_amt, true);
                        _lineNo += 1;
                        SSCombineLine += 1;
                    }

                    var _invlst = new BindingList<InvoiceItem>(_invoiceItemList);
                    gvInvoiceItem.DataSource = setItemDescriptions(_invoiceItemList);
                    gvInvoiceItem.DataBind();

                    for (int i = 0; i < gvInvoiceItem.Rows.Count; i++)
                    {
                        GridViewRow dr = gvInvoiceItem.Rows[i];
                        LinkButton btnAddSerials = dr.FindControl("btnAddSerials") as LinkButton;
                        btnAddSerials.Visible = true;
                    }

                    ScanSerialList = new List<ReptPickSerials>();
                    string _defbin = CHNLSVC.Inventory.GetDefaultBinCode(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString());

                    foreach (InvoiceItem _itm in _invoiceItemList)
                    {
                        MasterItem _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itm.Sad_itm_cd);
                        if (_item.Mi_is_ser1 == 0)
                        {
                            List<ReptPickSerials> _nonserLst = null;
                            if (IsPriceLevelAllowDoAnyStatus == false)
                                _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Sad_itm_cd, _itm.Sad_itm_stus, Convert.ToDecimal(_itm.Sad_qty));
                            else
                                _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomly(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Sad_itm_cd, string.Empty, Convert.ToDecimal(_itm.Sad_qty));
                            _nonserLst.ForEach(x => x.Tus_base_doc_no = Convert.ToString(-100));
                            _nonserLst.ForEach(x => x.Tus_base_itm_line = _itm.Sad_itm_line);
                            _nonserLst.ForEach(x => x.Tus_usrseq_no = -100);
                            _nonserLst.ForEach(x => x.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim()));
                            _nonserLst.ForEach(x => x.Tus_serial_id = string.Empty);
                            _nonserLst.ForEach(x => x.Tus_ser_id = -1);
                            _nonserLst.ForEach(x => x.Tus_new_status = string.Empty);
                            ScanSerialList.AddRange(_nonserLst);
                        }
                        else if (_item.Mi_is_ser1 == -1)
                        {
                            ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                            _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                            _reptPickSerial_.Tus_base_doc_no = Convert.ToString(-100);
                            _reptPickSerial_.Tus_base_itm_line = _itm.Sad_itm_line;
                            _reptPickSerial_.Tus_bin = _defbin;
                            _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                            _reptPickSerial_.Tus_cre_dt = DateTime.Now;
                            _reptPickSerial_.Tus_cross_batchline = 0;
                            _reptPickSerial_.Tus_cross_itemline = 0;
                            _reptPickSerial_.Tus_cross_seqno = 0;
                            _reptPickSerial_.Tus_cross_serline = 0;
                            _reptPickSerial_.Tus_doc_dt = Convert.ToDateTime(txtdate.Text);
                            _reptPickSerial_.Tus_doc_no = string.Empty;
                            _reptPickSerial_.Tus_exist_grncom = string.Empty;
                            _reptPickSerial_.Tus_isapp = 1;
                            _reptPickSerial_.Tus_iscovernote = 1;
                            _reptPickSerial_.Tus_itm_brand = _itm.Mi_brand;
                            _reptPickSerial_.Tus_itm_cd = _itm.Sad_itm_cd;
                            _reptPickSerial_.Tus_itm_desc = _itm.Mi_longdesc;
                            _reptPickSerial_.Tus_itm_line = 0;
                            _reptPickSerial_.Tus_itm_model = _itm.Mi_model;
                            _reptPickSerial_.Tus_itm_stus = _itm.Sad_itm_stus;
                            _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                            _reptPickSerial_.Tus_new_status = string.Empty;
                            _reptPickSerial_.Tus_qty = _itm.Sad_qty;
                            _reptPickSerial_.Tus_ser_1 = "N/A";
                            _reptPickSerial_.Tus_ser_2 = "N/A";
                            _reptPickSerial_.Tus_ser_id = 0;
                            _reptPickSerial_.Tus_ser_line = 0;
                            _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();
                            _reptPickSerial_.Tus_unit_cost = 0;
                            _reptPickSerial_.Tus_unit_price = 0;
                            _reptPickSerial_.Tus_usrseq_no = -100;
                            _reptPickSerial_.Tus_warr_no = "N/A";
                            _reptPickSerial_.Tus_warr_period = 0;
                            _reptPickSerial_.Tus_new_remarks = string.Empty;
                            ScanSerialList.Add(_reptPickSerial_);
                        }
                        else
                        {
                            if (oSerials != null && oSerials.Count > 0)
                            {
                                List<ReptPickSerials> _itmSerial = (from _res in oSerials
                                                                    where _res.Tus_itm_cd == _itm.Sad_itm_cd && _res.Tus_itm_line == _itm.Sad_itm_line
                                                                    select _res).ToList<ReptPickSerials>();
                                if (_itmSerial != null && _itmSerial.Count > 0)
                                {

                                    ReptPickSerials oSelectedSerila = oSerials.Find(x => x.Tus_itm_cd == _itm.Sad_itm_cd && x.Tus_base_itm_line == _itm.Sad_itm_line);

                                    ReptPickSerials oSerial = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), null, _itm.Sad_itm_cd, oSelectedSerila.Tus_ser_1);
                                    if (oSerial == null)
                                    {
                                        DisplayMessage("Cannot find serial details");
                                        return;
                                    }

                                    List<InventorySerialRefN> _invSerials = CHNLSVC.Inventory.GetSerialByID(oSerial.Tus_ser_id.ToString(), Session["UserDefLoca"].ToString());
                                    if (_invSerials == null && _invSerials.Count <= 0)
                                    {
                                        DisplayMessage("Serial id not found on inventory.SERIAL ID - " + oSerial.Tus_ser_id);
                                        return;
                                    }

                                    _invSerials = (from _res in _invSerials
                                                   where _res.Ins_available == -1 || _res.Ins_available == 1 // added by Nadeeka
                                                   select _res).ToList<InventorySerialRefN>();
                                    if (_invSerials != null && _invSerials.Count > 0)
                                    {
                                        ReptPickSerials _reptPickSerial_ = new ReptPickSerials();
                                        _reptPickSerial_.Tus_com = Session["UserCompanyCode"].ToString();
                                        _reptPickSerial_.Tus_base_doc_no = Convert.ToString(-100);
                                        _reptPickSerial_.Tus_base_itm_line = _itm.Sad_itm_line;
                                        _reptPickSerial_.Tus_bin = _invSerials[0].Ins_bin;
                                        _reptPickSerial_.Tus_cre_by = Session["UserID"].ToString();
                                        _reptPickSerial_.Tus_cre_dt = DateTime.Now;
                                        _reptPickSerial_.Tus_cross_batchline = _invSerials[0].Ins_cross_batchline;
                                        _reptPickSerial_.Tus_cross_itemline = _invSerials[0].Ins_cross_itmline;
                                        _reptPickSerial_.Tus_cross_seqno = _invSerials[0].Ins_cross_seqno;
                                        _reptPickSerial_.Tus_cross_serline = _invSerials[0].Ins_cross_serline;
                                        _reptPickSerial_.Tus_doc_dt = _invSerials[0].Ins_doc_dt;
                                        _reptPickSerial_.Tus_doc_no = _invSerials[0].Ins_doc_no;
                                        _reptPickSerial_.Tus_exist_grncom = _invSerials[0].Ins_exist_grncom;
                                        _reptPickSerial_.Tus_isapp = 1;
                                        _reptPickSerial_.Tus_iscovernote = 1;
                                        _reptPickSerial_.Tus_itm_brand = _itm.Mi_brand;
                                        _reptPickSerial_.Tus_itm_cd = _itm.Sad_itm_cd;
                                        _reptPickSerial_.Tus_itm_desc = _itm.Mi_longdesc;
                                        _reptPickSerial_.Tus_itm_line = _itm.Sad_itm_line;
                                        _reptPickSerial_.Tus_itm_model = _itm.Mi_model;
                                        _reptPickSerial_.Tus_itm_stus = _itm.Sad_itm_stus;
                                        _reptPickSerial_.Tus_loc = Session["UserDefLoca"].ToString();
                                        _reptPickSerial_.Tus_new_status = string.Empty;
                                        _reptPickSerial_.Tus_qty = _itm.Sad_qty;
                                        _reptPickSerial_.Tus_ser_1 = _invSerials[0].Ins_ser_1;
                                        _reptPickSerial_.Tus_ser_2 = _invSerials[0].Ins_ser_2;
                                        _reptPickSerial_.Tus_ser_id = _invSerials[0].Ins_ser_id;
                                        _reptPickSerial_.Tus_ser_line = _invSerials[0].Ins_ser_line;
                                        _reptPickSerial_.Tus_session_id = Session["SessionID"].ToString();
                                        _reptPickSerial_.Tus_unit_cost = _invSerials[0].Ins_unit_cost;
                                        _reptPickSerial_.Tus_unit_price = _invSerials[0].Ins_unit_price;
                                        _reptPickSerial_.Tus_usrseq_no = -100;
                                        _reptPickSerial_.Tus_warr_no = _invSerials[0].Ins_warr_no;
                                        _reptPickSerial_.Tus_warr_period = _invSerials[0].Ins_warr_period;
                                        _reptPickSerial_.Tus_new_remarks = string.Empty;
                                        ScanSerialList.Add(_reptPickSerial_);
                                    }
                                }
                            }
                        }
                    }

                    foreach (ReptPickSerials item in ScanSerialList)
                    {
                        InvoiceSerial _invser = new InvoiceSerial();
                        _invser.Sap_del_loc = Session["UserDefLoca"].ToString();
                        _invser.Sap_itm_cd = item.Tus_itm_cd;
                        _invser.Sap_itm_line = item.Tus_itm_line;
                        _invser.Sap_remarks = string.Empty;

                        if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                        {
                            InvoiceItem oItem = _invoiceItemList.Find(x => x.Sad_itm_cd == item.Tus_itm_cd && x.Sad_itm_line == item.Tus_itm_line);
                            _invser.Sap_seq_no = oItem.Sad_seq;
                        }

                        _invser.Sap_ser_1 = item.Tus_ser_1;
                        _invser.Sap_ser_line = _isCombineAdding ? Convert.ToInt32(SSCombineLine) : 0;
                        InvoiceSerialList.Add(_invser);
                    }

                    var _serlst = new BindingList<ReptPickSerials>(ScanSerialList);
                    gvPopSerial.DataSource = _serlst;
                    gvPopSerial.DataSource = setSerialStatusDescriptions(ScanSerialList);
                    gvPopSerial.DataBind();

                    decimal _tobepays = 0;
                    if (lblSVatStatus.Text == "Available") _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                    else _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());


                    //ucPayModes1.TotalAmount = _tobepays;
                    //ucPayModes1.InvoiceItemList = _invoiceItemList;
                    //ucPayModes1.SerialList = InvoiceSerialList;
                    //ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays));
                    //ucPayModes1.IsTaxInvoice = chkTaxPayable.Checked;
                    //if (ucPayModes1.HavePayModes && _isCombineAdding == false)
                    //    ucPayModes1.LoadData();


                }
                else
                {
                    //divalert.Visible = true;
                    DisplayMessage("sales order serial line  mismatch");
                    _invoiceItemList = new List<InvoiceItem>();
                    var _nulllst = new BindingList<InvoiceItem>(_invoiceItemList);
                    gvInvoiceItem.DataSource = setItemDescriptions(_invoiceItemList);
                    gvInvoiceItem.DataBind();
                }
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        //private void LoadSoData(string sono, string company, string pc)
        //{
        //    try
        //    {
        //        DataTable dtHeadersSo = CHNLSVC.Sales.SearchSalesOrdData(sono, company, pc);
        //        if (dtHeadersSo.Rows.Count > 0)
        //        {
        //            foreach (DataRow item in dtHeadersSo.Rows)
        //            {
        //                Session["SOSEQNO"] = item[25].ToString();

        //                DateTime oreddate = Convert.ToDateTime(item[3].ToString());
        //                string oreddatetext = oreddate.ToString("dd/MMM/yyyy");
        //                txtdate.Text = oreddatetext;

        //                cmbInvType.SelectedValue = item[1].ToString();
        //                txtManualRefNo.Text = item[5].ToString();
        //                txtdocrefno.Text = item[6].ToString();
        //                txtCustomer.Text = item[7].ToString();
        //                txtCusName.Text = item[8].ToString();
        //                txtAddress1.Text = item[9].ToString();
        //                txtAddress2.Text = item[10].ToString();
        //                txtcurrency.Text = item[11].ToString();
        //                cmbExecutive.SelectedValue = item[17].ToString();
        //                txtQuotation.Text = item[18].ToString();
        //                txtPromotor.Text = item[20].ToString();
        //                txtLoyalty.Text = item[26].ToString();
        //                SetLoyalityColor();

        //                if (item[27].ToString() == "1")
        //                {
        //                    chkTaxPayable.Checked = true;
        //                }
        //                else
        //                {
        //                    chkTaxPayable.Checked = false;
        //                }

        //                if (item[28].ToString() == "1")
        //                {
        //                    lblVatExemptStatus.Text = "Available";
        //                }
        //                else
        //                {
        //                    lblVatExemptStatus.Text = "None";
        //                }

        //                if (item[22].ToString() == "1")
        //                {
        //                    lblSVatStatus.Text = "Available";
        //                }
        //                else
        //                {
        //                    lblSVatStatus.Text = "None";
        //                }

        //                txtlocation.Text = item[29].ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //divalert.Visible = true;
        //        DisplayMessage(ex.Message, 4);
        //    }
        //}

        //private void LoadSoItemData(string sono)
        //{
        //    try
        //    {
        //        DataTable dtitems = CHNLSVC.Sales.GET_SAO_ITEMS_BY_SO_NO(sono);
        //        if (dtitems.Rows.Count > 0)
        //        {
        //            gvInvoiceItem.DataSource = null;
        //            gvInvoiceItem.DataBind();

        //            _invoiceItemList = SalesOrderItmToInvoiceItem(dtitems);

        //            gvInvoiceItem.DataSource = _invoiceItemList;
        //            gvInvoiceItem.DataBind();
        //        }

        //        //ViewState["ITEMSTABLE"] = null;
        //        //this.BindItemsGrid();

        //        //ViewState["ITEMSTABLE"] = dtitems;
        //        //this.BindItemsGrid();
        //    }
        //    catch (Exception ex)
        //    {
        //        //divalert.Visible = true;
        //        DisplayMessage(ex.Message, 4);
        //    }
        //}

        //private void LoadSoSerialData(string sono)
        //{
        //    try
        //    {
        //        DataTable dtserialdata = CHNLSVC.Sales.GET_SAO_SER_BY_SO_NO(sono);
        //        if (dtserialdata.Rows.Count > 0)
        //            SalesOrderSerialToInvoiceSerial(dtserialdata);
        //        {
        //            gvPopSerial.DataSource = null;
        //            gvPopSerial.DataBind();

        //            gvPopSerial.DataSource = ScanSerialList;
        //            gvPopSerial.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        DisplayMessage(ex.Message, 4);
        //    }
        //}

        #endregion


        protected void lbtnreject_Click(object sender, EventArgs e)
        {
            if (txtreject.Value == "Yes")
            {
                try
                {


                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16013))
                    {
                        string msg = "You dont have permission to reject .Permission code : 16013";
                        DisplayMessage(msg, 1);
                        return;
                    }

                    string ordstatus = (string)Session["STATUS"];
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an order !!!')", true);

                        lbtnsupplier.Focus();
                        return;
                    }

                    if (ordstatus == "R")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This order is already rejected !!!')", true);

                        return;
                    }

                    _userid = (string)Session["UserID"];

                    SalesOrderHeader _SalesOrder = new SalesOrderHeader();

                    _SalesOrder.SOH_STUS = "R";
                    _SalesOrder.SOH_SO_NO = txtInvoiceNo.Text.Trim();
                    _SalesOrder.SOH_COM = Session["UserCompanyCode"].ToString();
                    _SalesOrder.SOH_PC = Session["UserDefProf"].ToString();
                    _SalesOrder.SOH_MOD_BY = _userid;
                    _SalesOrder.SOH_MOD_WHEN = CHNLSVC.Security.GetServerDateTime();
                    string error = string.Empty;
                    Int32 outputresultapprove = CHNLSVC.Sales.UpdateSOStatus(_SalesOrder, out error);

                    if (outputresultapprove == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully rejected !!!')", true);
                        ClearAll();
                        Clear();
                    }
                    else
                    {
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        DispMsg(error, "E");
                    }
                }
                catch (Exception ex)
                {
                    //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    DispMsg(ex.Message, "E");
                }
            }
        }
        protected void lbtnREqitem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPerTown.Text.ToString() == "")
                {
                    DisplayMessage("Please Select Dispatch Location", 1);
                    return;
                }


                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable result = CHNLSVC.Sales.SearchSalesOrderRequest(SearchParams, null, null, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "16";
                BindUCtrlDDLData2(result);
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                txtSearchbyword.Focus();
                //ViewState["SEARCH"] = null;
                //txtSearchbyword.Text = string.Empty;
                //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                //DataTable result = CHNLSVC.Sales.SearchSalesRequest(SearchParams, null, null);
                //grdResult.DataSource = result;
                //grdResult.DataBind();
                //lblvalue.Text = "16";
                //BindUCtrlDDLData(result);
                //ViewState["SEARCH"] = result;
                //SIPopup.Show();
                //txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                DispMsg(ex.Message, "E");
            }
        }


        protected void lbtnapprove_Click(object sender, EventArgs e)
        {
            if (txtapprove.Value == "Yes")
            {
                try
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16011))
                    {
                        string msg = "You dont have permission to approve .Permission code : 16011";
                        DisplayMessage(msg, 1);
                        return;
                    }
                    string ordstatus = (string)Session["STATUS"];
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an order !!!')", true);

                        lbtnsupplier.Focus();
                        return;
                    }
                    if (ordstatus == "A")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This order is already approved !!!')", true);

                        return;
                    }
                    _userid = (string)Session["UserID"];
                    SalesOrderHeader _SalesOrder = new SalesOrderHeader();
                    _SalesOrder.SOH_STUS = "A";
                    _SalesOrder.SOH_SO_NO = txtInvoiceNo.Text.Trim();
                    _SalesOrder.SOH_COM = Session["UserCompanyCode"].ToString();
                    _SalesOrder.SOH_PC = Session["UserDefProf"].ToString();
                    _SalesOrder.SOH_MOD_BY = _userid;
                    _SalesOrder.SOH_MOD_WHEN = CHNLSVC.Security.GetServerDateTime();
                    _SalesOrder.SOH_SALES_EX_CD = txtexcutive.Text;
                    _SalesOrder.SOH_MAN_REF = txtManualRefNo.Text;
                    _SalesOrder.SOH_CUS_CD = txtCustomer.Text;
                    _SalesOrder.SOH_CUS_NAME = txtCusName.Text;
                    string error = string.Empty;
                    Int32 outputresultapprove = CHNLSVC.Sales.UpdateSOStatus(_SalesOrder, out error);

                    if (outputresultapprove == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully approved !!!')", true);
                        //added by dilshan on 04-09-2018******
                        CHNLSVC.MsgPortal.GenarateSOSMS(Session["UserCompanyCode"].ToString(), txtPoNo.Text, txtexcutive.Text, "");
                        //************************************                           
                        Clear(); ClearAll();
                    }
                    else
                    {
                        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        DispMsg(error, "E");
                    }
                }
                catch (Exception ex)
                {
                    // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    DispMsg(ex.Message, "E");
                }
            }
        }

        protected void lbtnViewinv_Click(object sender, EventArgs e)
        {
            if (txtPerTown.Text == "")
            {
                string msg = "Enter dispatch location";
                DisplayMessage(msg, 2);
                grvwarehouseitms.DataSource = new int[] { };
                grvwarehouseitms.DataBind();
                return;
            }
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label InvItm_Item = dr.FindControl("InvItm_Item") as Label;
            Label stsatus = dr.FindControl("lblsad_itm_stus") as Label;
            LoadDispatchDetails(Session["UserCompanyCode"].ToString(), txtPerTown.Text, InvItm_Item.Text, stsatus.Text);
        }

        protected void lbtnadReq_Click(object sender, EventArgs e)
        {
            Session["linenoreq"] = "";
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label InvItm_Item = dr.FindControl("InvItm_Item") as Label;
            Label lblsad_itm_line = dr.FindControl("lblsad_itm_line") as Label;

            Session["linenoreq"] = lblsad_itm_line.Text;
            txtItem.Text = InvItm_Item.Text;
            txtItem_TextChanged(null, null);
            if (Isrequestbase = true)
            {
                if (_invoiceItemList.Count > 0)
                {
                    _lineNo = _invoiceItemList.Max(x => x.Sad_itm_line);
                    _invoiceItemList.RemoveAll(x => x.Sad_itm_line == Convert.ToInt32(lblsad_itm_line.Text));
                    //_lineNo = _invoiceItemList.Max(x=>x.Sad_itm_line);
                }
            }
            //lbtndelitem_Click(null, null);
        }


        #region Confirmation2
        protected void btnConfClose2_Click(object sender, EventArgs e)
        {
            lblConfText2.Text = "";
            mpConfirmation2.Hide();
        }

        protected void btnConfYes2_Click(object sender, EventArgs e)
        {
            mpConfirmation2.Hide();
            hdfConf2.Value = "1";
            // hdfConfItem2.Value = txtItem.Text.ToUpper().Trim();
            // hdfConfStatus2.Value = cmbStatus.SelectedValue.ToString();
            txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            cmbBook.Text = _priorityPriceBook.Sppb_pb;
            LoadPriceLevel(cmbInvType.Text, cmbBook.Text);
            cmbLevel.Text = _priorityPriceBook.Sppb_pb_lvl;
            // LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
            // CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
            bool isJSEnd = false;
            CheckQty(false, out isJSEnd);
        }

        protected void btnConfNo2_Click(object sender, EventArgs e)
        {
            hdfConf2.Value = "0";
            //hdfConfItem2.Value = txtItem.Text.ToUpper().Trim();
            //hdfConfStatus2.Value = cmbStatus.SelectedValue.ToString();
            //txtItem_TextChanged(null, null);
            lblConfText2.Text = "";
            mpConfirmation2.Hide();
        }
        #endregion

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
                SIPopup.Show();
                Session["SIPopup"] = "SIPopup"; txtSearchbyword.Text = "";
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }

        }

        protected void txtexcutive_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, "EPF", txtexcutive.Text);
                if (result.Rows.Count == 1)
                {
                    txtexcutive.Text = result.Rows[0][1].ToString();
                    txtexcutive.ToolTip = result.Rows[0][1].ToString();
                    lblSalesEx.Text = result.Rows[0][2].ToString();
                }
                else
                {
                    txtexcutive.Text = string.Empty;
                    DisplayMessage("Invalid sales executive code", 1);
                }

            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }


        private bool IsAllovcateCustomer(string _emp)
        {
            _isAllocateCustomer = CHNLSVC.General.CHECKALLOCATE_CUS(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _emp);
            return _isAllocateCustomer;
        }

        protected void CheckDiscountRate(object sender, EventArgs e)
        {
            // if (chkPickGV.Checked) return;
            if (_IsVirtualItem)
            {
                txtDisRate.Text = string.Empty;
                txtDisAmt.Text = string.Empty;
                txtDisAmt.Text = FormatToCurrency("0");
                txtDisRate.Text = FormatToCurrency("0");
                return;
            }
            try
            {
                if (Convert.ToDecimal(txtDisRate.Text.Trim()) > 100)
                {
                    // using (new CenterWinDialog(this))
                    //{ 
                    DisplayMessage("Discount Rate should be less than 100%!", 1);
                    // MessageBox.Show("Discount Rate should be less than 100%!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    // }
                    txtDisRate.Text = string.Empty;
                    txtDisRate.Text = FormatToQty("0");
                    return;
                }
                if (Convert.ToDecimal(txtDisRate.Text.Trim()) < 0)
                {
                    //MessageBox.Show("Discount rate should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtDisRate.Clear();
                    //txtDisRate.Text = FormatToQty("0");
                    //return;
                }


                if (_isCompleteCode && GeneralDiscount_new.SADD_IS_EDIT == 1 && Convert.ToDecimal(txtDisRate.Text.Trim()) > 0)
                {

                    // using (new CenterWinDialog(this)) 
                    // { 
                    DisplayMessage("You are not allow discount for com codes!", 1);
                    // MessageBox.Show("You are not allow discount for com codes!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                    // }
                    txtDisRate.Text = string.Empty;
                    txtDisRate.Text = FormatToQty("0");
                    return;
                }


                //else
                //{
                //    if (Convert.ToDecimal(txtQty.Text) != 1)
                //    {
                //        this.Cursor = Cursors.Default;
                //        using (new CenterWinDialog(this)) { MessageBox.Show("Promotion voucher allow for only one(1) item!", "Discount Rate", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                //        txtDisRate.Clear();
                //        txtDisRate.Text = FormatToQty("0");
                //        return;
                //    }
                //}
                CheckNewDiscountRate();
            }
            catch (Exception ex)
            {

            }

        }
        public void PrintPDF(string targetFileName, ReportDocument _rpt)
        {
            try
            {
                //clsInventory obj = new clsInventory();
                //InvReportPara _objRepoPara = new InvReportPara();
                //_objRepoPara = Session["InvReportPara"] as InvReportPara;
                //obj.Print_AOA_Warra(_objRepoPara);
                //ReportDocument Rel = new ReportDocument();
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
                DisplayMessage(ex.Message, 2);
                return;
            }
        }
        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string url2 = "";
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
                Session["GlbReportType"] = "";
                Session["OrNo"] = txtInvoiceNo.Text.Trim();
                if (txtInvoiceNo.Text.Trim() == null | txtInvoiceNo.Text.Trim() == "")
                {
                    DisplayMessage("Please Select Order No", 1);
                }
                else
                {
                    Session["GlbReportName"] = "sales_order.rpt";

                    clsSales obj = new clsSales();
                    string COM = Session["UserCompanyCode"] as string;
                    string pc = Session["UserDefProf"] as string;
                    string OrNo = Session["OrNo"] as string;
                    string loc = Session["UserDefLoca"].ToString();
                    obj.get_Sales_Orders(OrNo, COM, pc, loc);
                    //  CVSale.ReportSource = obj._sales_order;
                    // CVSale.RefreshReport();
                    PrintPDF(targetFileName, obj._sales_order);
                    url2 = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url2, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url2, false);
                    CHNLSVC.MsgPortal.SaveReportErrorLog("SALES ORDER", "SALES ORDER", "Run Ok", Session["UserID"].ToString());
                    //  Response.Redirect("~/View/Reports/Sales/SalesReportViewer.aspx", false);

                    string url = "<script>window.open('/View/Reports/Sales/SalesReportViewer.aspx','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Sales Order Print", "SalesOrderNew", ex.Message, Session["UserID"].ToString());
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["GlbReportType"] = "";
            Session["GlbReportName"] = "sales_order.rpt";
            string url = "<script>window.open('/View/Reports/Sales/SalesReportViewer.aspx','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            PopupConfBox.Hide();
        }
        #region Modal Popup 2
        protected void btnDClose_Click(object sender, EventArgs e)
        {
            txtSearchbywordD.Text = "";
            UserDPopoup.Hide();
            // UserPopup.Hide();
        }
        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            if (lblvalue.Text == "16")
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable result = CHNLSVC.Sales.SearchSalesOrderRequest(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text, Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "16";
                BindUCtrlDDLData2(result);
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                txtSearchbyword.Focus();

            }
            else
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);

                DataTable result = new DataTable();
                if (CheckPCTempSave() && SoTempPending.Checked)
                {
                    result = CHNLSVC.CommonSearch.SearchSalesOrderWeb(SearchParams, "PENDING", txtSearchbywordD.Text);
                }
                else
                {
                    result = CHNLSVC.CommonSearch.SearchSalesOrderWeb(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text);
                }

                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "InvoiceWithDate";
                ViewState["SEARCH"] = result;
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }

        }
        protected void grdResultD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbywordD.ClientID + "').value = '';", true);
            string Name = grdResultD.SelectedRow.Cells[1].Text;
            if (lblvalue.Text == "InvoiceWithDate")
            {
                txtInvoiceNo.Text = grdResultD.SelectedRow.Cells[1].Text;
                txtInvoiceNo_TextChanged(null, null);
            }
            else if (lblvalue.Text == "16")
            {
                Isrequestbase = true;
                _invoiceItemList = new List<InvoiceItem>();
                txtCustomer.Text = grdResultD.SelectedRow.Cells[2].Text;
                LoadCusData();
                txtdellocation.Text = txtPerTown.Text;
                LoadCusLoyalityNo();
                string Reqno = grdResultD.SelectedRow.Cells[1].Text;
                // LoadInvItems(Convert.ToInt32(grdResult.SelectedRow.Cells[3].Text));
                Addreqitems(Convert.ToInt32(grdResultD.SelectedRow.Cells[3].Text));
                txtdocrefno.Text = Reqno;
                ViewCustomerAccountDetail(txtCustomer.Text);
                LoadHDD(Reqno);
                LoadPriceLevel(cmbInvType.Text, cmbBook.Text);
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                ClearPriceTextBox();
                btnSearch_Serial.Visible = false;

                DataTable dt = (DataTable)ViewState["ITEMSTABLE"];
                int k = 0;
                //foreach (GridViewRow row in gvInvoiceItem.Rows)
                //{

                //    Decimal qty = 0;
                //    Decimal upri = 0;
                //    Decimal dis = 0;
                //    Decimal tax = 0;

                //    //if ((string.IsNullOrEmpty(row.Cells[4].Text.ToString())) || (row.Cells[4].Text.ToString() == "&nbsp;"))
                //    //{
                //    //    qty = 0;
                //    //}
                //    //else
                //    //{
                //    //    qty = Convert.ToDecimal(row.Cells[4].Text);
                //    //}
                //    if ((string.IsNullOrEmpty(dt.Rows[k]["sad_qty"].ToString())) || (dt.Rows[k]["sad_qty"].ToString() == "&nbsp;"))
                //    {
                //        qty = 0;
                //    }
                //    else
                //    {
                //        qty = Convert.ToDecimal(dt.Rows[k]["sad_qty"].ToString());
                //    }

                //    if ((string.IsNullOrEmpty(dt.Rows[k]["sad_unit_rt"].ToString())) || (dt.Rows[k]["sad_unit_rt"].ToString() == "&nbsp;"))
                //    {
                //        upri = 0;
                //    }
                //    else
                //    {
                //        upri = Convert.ToDecimal(dt.Rows[k]["sad_unit_rt"].ToString());
                //    }
                //    //if ((string.IsNullOrEmpty(row.Cells[5].Text.ToString())) || (row.Cells[5].Text.ToString() == "&nbsp;"))
                //    //{
                //    //    upri = 0;
                //    //}
                //    //else
                //    //{
                //    //    upri = Convert.ToDecimal(row.Cells[5].Text);
                //    //}

                //    if ((string.IsNullOrEmpty(row.Cells[8].Text.ToString())) || (row.Cells[8].Text.ToString() == "&nbsp;"))
                //    {
                //        dis = 0;
                //    }
                //    else
                //    {
                //        dis = Convert.ToDecimal(row.Cells[8].Text);
                //    }

                //    if ((string.IsNullOrEmpty(row.Cells[9].Text.ToString())) || (row.Cells[9].Text.ToString() == "&nbsp;"))
                //    {
                //        tax = 0;
                //    }
                //    else
                //    {
                //        tax = Convert.ToDecimal(row.Cells[9].Text);
                //    }

                //    // CalculateGrandTotalNew(qty, upri, dis, tax, true);
                //    k++;
                //}

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "document.getElementById('" + txtSearchbyword.ClientID + "').value = '';", true);
            }

        }
        protected void grdResultD_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultD.PageIndex = e.NewPageIndex;
            try
            {
                grdResultD.DataSource = null;
                grdResultD.DataSource = (DataTable)ViewState["SEARCH"];
                grdResultD.DataBind();
                UserDPopoup.Show();
                Session["DPopup"] = "DPopup";
                txtSearchbywordD.Focus();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }

        }
        protected void lbtnSearchD_Click(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }
        }
        protected void txtSearchbywordD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                //divalert.Visible = true;
                DisplayMessage(ex.Message, 4);
            }

        }
        #endregion

        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykeyD.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyD.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtconfirmplaceord.Value == "Yes")
            {
                //Get seq no
                string orderno = txtInvoiceNo.Text.ToString();
                string com = Session["UserCompanyCode"].ToString();
                Int32 seqno = 0;
                Int32 reqseqno = 0;
                string cuscode = txtCustomer.Text.ToString();
                if (orderno == "")
                {
                    string msg = "Please Select Order No";
                    DisplayMessage(msg, 2);
                    return;
                }
                else
                {
                    DataTable seqdata = CHNLSVC.Sales.GetSOSeqno(com, orderno);
                    DataTable reqseqdata = CHNLSVC.Sales.GetREQITMSeqno(com, orderno);
                    if (seqdata.Rows.Count > 0 && reqseqdata.Rows.Count > 0)
                    {
                        seqno = Convert.ToInt32(seqdata.Rows[0][0].ToString());
                        reqseqno = Convert.ToInt32(reqseqdata.Rows[0][0].ToString());
                    }
                    else
                    {
                        string msg = "Please Select Correct Order No";
                        DisplayMessage(msg, 2);
                        return;
                    }
                }
                #region Header

                SalesOrderHeader SalesOrder = new SalesOrderHeader();

                string seqnofororder = (string)Session["SOSEQNO"];

                if (string.IsNullOrEmpty(seqnofororder))
                {
                    seqnofororder = "0";
                }
                
                    SalesOrder.SOH_STUS = "S";
                    //Added By Dulaj 2018/Dec/20 
                    if (CheckPCTempSave())
                    {
                        SalesOrder.SOH_STUS = "P";
                    }
                SalesOrder.SOH_COM = Session["UserCompanyCode"].ToString();
                SalesOrder.SOH_PC = Session["UserDefProf"].ToString();
                SalesOrder.SOH_TP = "INV";
                SalesOrder.SOH_SO_TP = cmbInvType.SelectedValue;
                SalesOrder.SOH_SO_SUB_TP = "SA";
                SalesOrder.SOH_SO_NO = txtInvoiceNo.Text.Trim();
                SalesOrder.SOH_DT = Convert.ToDateTime(txtdate.Text);
                SalesOrder.SOH_MANUAL = 0;
                SalesOrder.SOH_MAN_REF = txtManualRefNo.Text.Trim();
                SalesOrder.SOH_REF_DOC = txtdocrefno.Text.Trim();
                SalesOrder.SOH_CUS_CD = txtCustomer.Text.Trim();
                SalesOrder.SOH_CUS_NAME = txtCusName.Text.Trim();
                SalesOrder.SOH_CUS_ADD1 = txtAddress1.Text.Trim();
                SalesOrder.SOH_CUS_ADD2 = txtAddress2.Text.Trim();
                SalesOrder.SOH_CURRENCY = txtcurrency.Text.Trim();
                // SalesOrder.SOH_EX_RT = exchangerate;
                SalesOrder.SOH_TOWN_CD = txtlocation.Text;//_dcustown;
                SalesOrder.SOH_D_CUST_CD = txtdelcuscode.Text.Trim();//_dcuscode;
                SalesOrder.SOH_D_CUST_ADD1 = txtdelad1.Text.Trim();//_dcusadd1;
                SalesOrder.SOH_D_CUST_ADD2 = txtdelad2.Text.Trim(); //_dcusadd2;
                SalesOrder.SOH_MAN_CD = txtexcutive.Text;//cmbExecutive.SelectedValue;
                SalesOrder.SOH_SALES_EX_CD = txtexcutive.Text;//cmbExecutive.SelectedValue;
                SalesOrder.SOH_SALES_STR_CD = string.Empty;
                SalesOrder.SOH_SALES_SBU_CD = string.Empty;
                SalesOrder.SOH_SALES_SBU_MAN = string.Empty;
                SalesOrder.SOH_SALES_CHN_CD = string.Empty;
                SalesOrder.SOH_SALES_CHN_MAN = string.Empty;
                SalesOrder.SOH_SALES_REGION_CD = string.Empty;
                SalesOrder.SOH_SALES_REGION_MAN = string.Empty;
                SalesOrder.SOH_SALES_ZONE_CD = string.Empty;
                SalesOrder.SOH_SALES_ZONE_MAN = string.Empty;
                SalesOrder.SOH_STRUCTURE_SEQ = txtQuotation.Text.Trim();
                SalesOrder.SOH_ESD_RT = 0;
                SalesOrder.SOH_WHT_RT = 0;
                SalesOrder.SOH_EPF_RT = 0;
                SalesOrder.SOH_PDI_REQ = 0;
                SalesOrder.SOH_REMARKS = string.Empty;
                SalesOrder.SOH_IS_ACC_UPLOAD = 0;
                SalesOrder.SOH_REMARKS = txtRemarks.Text.ToString();

                SalesOrder.SOH_CRE_BY = _userid;
                SalesOrder.SOH_CRE_WHEN = CHNLSVC.Security.GetServerDateTime();
                SalesOrder.SOH_MOD_BY = _userid;
                SalesOrder.SOH_MOD_WHEN = CHNLSVC.Security.GetServerDateTime();
                SalesOrder.SOH_SESSION_ID = Session["SessionID"].ToString();
                SalesOrder.SOH_ANAL_1 = txtPromotor.Text.Trim();
                SalesOrder.SOH_ANAL_2 = string.Empty;
                SalesOrder.SOH_ANAL_3 = string.Empty;
                SalesOrder.SOH_ANAL_4 = txtPoNo.Text.Trim();
                SalesOrder.SOH_ANAL_5 = cmbTechnician.SelectedValue;
                SalesOrder.SOH_ANAL_6 = string.Empty;
                SalesOrder.SOH_ANAL_7 = 0;
                SalesOrder.SOH_ANAL_8 = Convert.ToInt32(0);
                SalesOrder.SOH_ANAL_9 = Convert.ToInt32(0);
                SalesOrder.SOH_ANAL_10 = Convert.ToDecimal(lblGrndTotalAmount.Text);
                SalesOrder.SOH_ANAL_11 = Convert.ToDecimal(lblAvailableCredit.Text);
                SalesOrder.SOH_ANAL_12 = Convert.ToDateTime(DateTime.Now);
                SalesOrder.SOH_DIRECT = 1;
                SalesOrder.SOH_TAX_INV = chkTaxPayable.Checked ? 1 : 0;
                SalesOrder.SOH_GRUP_CD = string.Empty;
                SalesOrder.SOH_ACC_NO = string.Empty;
                SalesOrder.SOH_TAX_EXEMPTED = lblVatExemptStatus.Text == "Available" ? 1 : 0;

                //if (_masterBusinessCompany != null)
                //{
                //    lblSVatStatus.Text = _masterBusinessCompany.Mbe_is_svat ? "Available" : "None";
                //}
                if (string.IsNullOrEmpty(lblSVatStatus.Text.Trim()))
                {
                    DispMsg("Exempt Status cannot be null !"); return;
                }
                SalesOrder.SOH_IS_SVAT = lblSVatStatus.Text == "Available" ? 1 : 0;

                SalesOrder.SOH_FIN_CHRG = 1;
                SalesOrder.SOH_DEL_LOC = txtPerTown.Text;//txtdellocation.Text; //_dcusloc;
                // SalesOrder.SOH_GRN_COM = _customerCompany;
                SalesOrder.SOH_GRN_LOC = txtlocation.Text.Trim();
                // SalesOrder.SOH_IS_GRN = Convert.ToInt32(_cushascompany);
                SalesOrder.SOH_D_CUST_NAME = txtdelname.Text.Trim();// _dcusname;
                SalesOrder.SOH_IS_DAYEND = 0;
                SalesOrder.SOH_SCM_UPLOAD = 0;
                SalesOrder.SOH_SEQ_NO = Convert.ToInt32(seqnofororder);
                // SalesOrder.mpc_so_res = mpc_so_res;
                SalesOrder.SOH_DISP_LOC = txtPerTown.Text.Trim();
                SalesOrder.SOH_CRE_BY = Session["UserID"].ToString();
                #endregion

                //Update SO

                List<SalesOrderItems> _SalesOrderItemsList = new List<SalesOrderItems>();
                #region Items
                if (gvInvoiceItem.Rows.Count > 0)
                {
                    foreach (GridViewRow ddr in gvInvoiceItem.Rows)
                    {
                        Label status = (ddr.FindControl("lblsad_itm_stus") as Label);
                        // DataTable dtstatus = CHNLSVC.Sales.GetItemStatusVal(status.Text);

                        string _Status = status.Text;

                        //if (dtstatus.Rows.Count > 0)
                        //{
                        //    foreach (DataRow ddr2 in dtstatus.Rows)
                        //    {
                        //        _Status = ddr2[0].ToString();
                        //    }
                        //}
                        Label SOI_ITM_LINE = (ddr.FindControl("lblsad_itm_line") as Label);
                        Label SOI_ITM_CD = (ddr.FindControl("InvItm_Item") as Label);
                        Label SOI_QTY = (ddr.FindControl("InvItm_Qty") as Label);
                        Label SOI_INV_QTY = (ddr.FindControl("lblsad_itm_stus") as Label);
                        Label SOI_UNIT_RT = (ddr.FindControl("InvItm_UPrice") as Label);
                        Label SOI_UNIT_AMT = (ddr.FindControl("lblsad_unit_amt") as Label);
                        Label SOI_DISC_RT = (ddr.FindControl("lblsad_disc_rt") as Label);
                        Label SOI_DISC_AMT = (ddr.FindControl("InvItm_DisAmt") as Label);
                        Label SOI_ITM_TAX_AMT = (ddr.FindControl("InvItm_TaxAmt") as Label);
                        Label SOI_TOT_AMT = (ddr.FindControl("lblsad_tot_amt") as Label);
                        Label SOI_PBOOK = (ddr.FindControl("InvItm_Book") as Label);
                        Label SOI_PB_LVL = (ddr.FindControl("InvItm_Level") as Label);
                        Label SOI_RES_NO = (ddr.FindControl("InvItm_ResNo") as Label);
                        Label SOI_PB_SEQ = (ddr.FindControl("InvItm_PbSeq") as Label);
                        Label SOI_ITEMSEQ = (ddr.FindControl("InvItm_PbLineSeq") as Label);
                        Label SOI_PROCD = (ddr.FindControl("InvItm_PromoCd") as Label);
                        Label InvItm_JobLine = (Label)ddr.FindControl("InvItm_JobLine");


                        if (SOI_PBOOK.Text == "")
                        {
                            _SalesOrderItemsList = null;
                            string msg = "Please edit item" + SOI_ITM_CD.Text;
                            DisplayMessage(msg, 2);
                            return;
                        }

                        SalesOrderItems SalesOrderItems = new SalesOrderItems();
                        //  SalesOrderItems.SOI_SEQ_NO = Convert.ToInt32(newseqno);
                        SalesOrderItems.SOI_ITM_LINE = Convert.ToInt32(SOI_ITM_LINE.Text);
                        // SalesOrderItems.SOI_SO_NO = outputopno;
                        SalesOrderItems.SOI_ITM_CD = SOI_ITM_CD.Text;
                        SalesOrderItems.SOI_ITM_STUS = _Status;
                        SalesOrderItems.SOI_ITM_TP = string.Empty;
                        SalesOrderItems.SOI_UOM = string.Empty;
                        SalesOrderItems.SOI_QTY = Convert.ToDecimal(SOI_QTY.Text);
                        SalesOrderItems.SOI_INV_QTY = Convert.ToDecimal(SOI_QTY.Text);
                        SalesOrderItems.SOI_UNIT_RT = Convert.ToDecimal(SOI_UNIT_RT.Text);
                        SalesOrderItems.SOI_UNIT_AMT = Convert.ToDecimal(SOI_UNIT_AMT.Text);
                        SalesOrderItems.SOI_DISC_RT = Convert.ToDecimal(SOI_DISC_RT.Text);
                        SalesOrderItems.SOI_DISC_AMT = Convert.ToDecimal(SOI_DISC_AMT.Text);
                        SalesOrderItems.SOI_ITM_TAX_AMT = Convert.ToDecimal(SOI_ITM_TAX_AMT.Text);
                        SalesOrderItems.SOI_TOT_AMT = Convert.ToDecimal(SOI_TOT_AMT.Text);
                        SalesOrderItems.SOI_PBOOK = SOI_PBOOK.Text;
                        SalesOrderItems.SOI_PB_LVL = SOI_PB_LVL.Text;
                        SalesOrderItems.SOI_PB_PRICE = 0;
                        SalesOrderItems.SOI_SEQ = Convert.ToInt32(SOI_PB_SEQ.Text);
                        SalesOrderItems.SOI_ITM_SEQ = Convert.ToInt32(SOI_ITEMSEQ.Text); //add rukshan 
                        // SalesOrderItems.SOI_ITM_SEQ = Convert.ToInt32((ddr.FindControl("itri_seq_no") as Label).Text);
                        SalesOrderItems.SOI_WARR_PERIOD = 0;
                        SalesOrderItems.SOI_WARR_REMARKS = string.Empty;
                        SalesOrderItems.SOI_IS_PROMO = 0;
                        SalesOrderItems.SOI_PROMO_CD = SOI_PROCD.Text;
                        SalesOrderItems.SOI_ALT_ITM_CD = string.Empty;
                        SalesOrderItems.SOI_ALT_ITM_DESC = string.Empty;
                        SalesOrderItems.SOI_PRINT_STUS = 0;
                        SalesOrderItems.SOI_RES_NO = SOI_RES_NO.Text;
                        SalesOrderItems.SOI_RES_LINE_NO = 0;
                        SalesOrderItems.SOI_JOB_NO = string.Empty;
                        SalesOrderItems.SOI_WARR_BASED = 0;
                        SalesOrderItems.SOI_MERGE_ITM = string.Empty;
                        SalesOrderItems.SOI_JOB_LINE = Convert.ToInt32(InvItm_JobLine.Text.ToString());
                        SalesOrderItems.SOI_OUTLET_DEPT = string.Empty;
                        SalesOrderItems.SOI_TRD_SVC_CHRG = 0;
                        SalesOrderItems.SOI_DIS_SEQ = 0;
                        SalesOrderItems.SOI_DIS_LINE = 0;
                        SalesOrderItems.SOI_DIS_TYPE = string.Empty;
                        SalesOrderItems.SOI_ANAL1 = string.Empty;
                        SalesOrderItems.SOI_ANAL2 = string.Empty;
                        SalesOrderItems.SOI_ANAL3 = string.Empty;
                        SalesOrderItems.SOI_ANAL4 = 1;
                        SalesOrderItems.SOI_ANAL5 = 1;
                        SalesOrderItems.SOI_PROMO_CD = cmbTechnician.SelectedValue;
                        _SalesOrderItemsList.Add(SalesOrderItems);
                    }
                }
                #endregion

                int effect = CHNLSVC.Sales.Update_SalesOrder(_SalesOrderItemsList, seqno, orderno, cuscode, com, reqseqno, SalesOrder);

                if (effect >= 0)
                {
                    string msg = "Successfully Updated";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('" + msg + "');", true);
                    ClearAll();
                }
                else
                {
                    //   ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    DispMsg("Data not updated correctly", "E");
                }

            }
            else
            {
                return;
            }
        }

        protected void lbtnApplyUnitRate_Click(object sender, EventArgs e)
        {
            // MpPriceEdit.Show();

            GridViewRow dr = gvInvoiceItem.Rows[Convert.ToInt32(lblDiscountRowNum.Text)];
            Label lblsad_disc_rt = dr.FindControl("lblsad_disc_rt") as Label;
            Label lblsad_itm_line = dr.FindControl("lblsad_itm_line") as Label;
            Label InvItm_Book = dr.FindControl("InvItm_Book") as Label;
            Label InvItm_Level = dr.FindControl("InvItm_Level") as Label;
            Label InvItm_Item = dr.FindControl("InvItm_Item") as Label;
            Label lblsad_itm_stus = dr.FindControl("lblsad_itm_stus") as Label;
            Label InvItm_UPrice = dr.FindControl("InvItm_UPrice") as Label;
            Label InvItm_Qty = dr.FindControl("InvItm_Qty") as Label;


            decimal _prevousDisRate = Convert.ToDecimal(lblsad_disc_rt.Text);
            int _lineno0 = Convert.ToInt32(lblsad_itm_line.Text);
            string _book = Convert.ToString(InvItm_Book.Text);
            string _level = Convert.ToString(InvItm_Level.Text);
            string _item = Convert.ToString(InvItm_Item.Text);
            string _status = Convert.ToString(lblsad_itm_stus.Text);
            bool _isSerialized = false;

            string _userUnitRate = txtPriceEdit.Text.Trim();
            if (string.IsNullOrEmpty(_userUnitRate))
                return;
            SarDocumentPriceDefn GeneralDiscount_new = new SarDocumentPriceDefn();
            GeneralDiscount_new = CHNLSVC.Sales.GetDocPriceDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _book, _level, cmbInvType.Text.ToString());

            decimal _pb_price;
            decimal _qty = Convert.ToDecimal(InvItm_Qty.Text);
            decimal _disRate = Convert.ToDecimal(lblsad_disc_rt.Text);
            decimal _txtUprice = Convert.ToDecimal(_userUnitRate);
            _pb_price = Convert.ToDecimal(InvItm_UPrice.Text);
            if (_pb_price == 0)
            {
                DisplayMessage("Not allow price edit for price!", 2);
                return;
            }
            decimal _Amt;
            bool _isUnitAmt = Decimal.TryParse(InvItm_UPrice.Text, out _Amt);
            if (!_isUnitAmt)
            {
                DisplayMessage("Unit Price has to be number!", 2);
                MpPriceEdit.Show();
                return;
            }
            if (_Amt <= 0)
            {
                DisplayMessage("Unit Price has to be greater than 0!", 2);
                MpPriceEdit.Show();
                return;
            }
            if (GeneralDiscount_new.SADD_IS_EDIT == 0 && _txtUprice > 0)
            {
                DisplayMessage("Not allow price edit for price!", 2);
                MpPriceEdit.Show();
                return;
            }
            if (GeneralDiscount_new.SADD_IS_EDIT == 1)
            {
                if (_pb_price > _txtUprice)
                {
                    decimal _diffPecentage = ((_pb_price - _txtUprice) / _pb_price) * 100;
                    if (_diffPecentage > GeneralDiscount_new.SADD_EDIT_RT)
                    {
                        DisplayMessage("You cannot deduct price more than " + GeneralDiscount_new.SADD_EDIT_RT + "% from the price book price.", 2);
                        //txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                        //txtUnitPrice.Text = _pb_price.ToString("N2");
                        //_isEditPrice = false;
                        MpPriceEdit.Show();
                        return;
                    }
                    else
                    {
                        _isEditPrice = true;
                    }
                }
                else
                {
                    if (GeneralDiscount_new.SADD_IS_EDIT == 1)
                    {
                        _isEditPrice = true;
                    }
                    else
                    {
                        decimal _diffPecentage = ((_txtUprice - _pb_price) / _txtUprice) * 100;
                        if (_diffPecentage > GeneralDiscount_new.SADD_EDIT_RT)
                        {
                            DisplayMessage("You cannot increase price more than " + GeneralDiscount_new.SADD_EDIT_RT + "% from the price book price.", 2);
                            // txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_pb_price));
                            //txtUnitPrice.Text = _pb_price.ToString("N2");
                            //_isEditPrice = false;
                            MpPriceEdit.Show();
                            return;
                        }
                        else
                        {
                            _isEditPrice = true;
                        }
                    }
                }
            }


            var _itm = _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList();
            if (_item != null && _item.Count() > 0) foreach (InvoiceItem _one in _itm)
                {
                    CalculateGrandTotal(_one.Sad_qty, _txtUprice, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, false);
                    decimal _unitRate = _txtUprice;
                    decimal _unitAmt = _txtUprice;
                    decimal _disVal = 0;
                    decimal _vatPortion = 0;
                    decimal _lineamount = 0;
                    decimal _newvatval = 0;

                    bool _isTaxDiscount = chkTaxPayable.Checked ? true : (lblSVatStatus.Text == "Available") ? true : false;

                    if (_isTaxDiscount)
                    {
                        _vatPortion = FigureRoundUp(TaxCalculation(_item, _status, _one.Sad_qty, _priceBookLevelRef, _unitRate, _disVal, _disRate, true), true);
                        _disVal = FigureRoundUp(_unitAmt * _disRate / 100, true);
                        _lineamount = FigureRoundUp(_unitRate * _one.Sad_qty + _vatPortion - _disVal, true);
                    }
                    else
                    {
                        _vatPortion = FigureRoundUp(TaxCalculation(_item, _status, _one.Sad_qty, _priceBookLevelRef, _unitRate, _disVal, _disRate, true), true);
                        _disVal = FigureRoundUp((_unitAmt + _vatPortion) * _disRate / 100, true);

                        if (_disRate > 0)
                        {
                            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, string.Empty);
                            if (_tax != null && _tax.Count > 0)
                            {
                                _newvatval = ((_unitRate * _one.Sad_qty + _vatPortion - _disVal) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                            }
                        }
                        if (_disRate > 0)
                        {
                            _lineamount = FigureRoundUp(_unitRate * _one.Sad_qty + _vatPortion - _disVal, true);
                            _vatPortion = FigureRoundUp(_newvatval, true);
                        }
                        else
                        {
                            _lineamount = FigureRoundUp(_unitRate * _one.Sad_qty + _vatPortion - _disVal, true);
                        }
                    }
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_disc_rt = _disRate);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_disc_amt = _disVal);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_itm_tax_amt = _vatPortion);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_tot_amt = FigureRoundUp(_lineamount, true));
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_seq = GeneralDiscount.Sgdd_seq);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_type = "M");
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_unit_rt = _unitRate);
                    _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_unit_amt = _unitAmt);

                    //if (_proVouInvcItem == txtItem.Text.ToString())
                    //{
                    //    if (string.IsNullOrEmpty(lblPromoVouUsedFlag.Text) && !string.IsNullOrEmpty(lblPromoVouNo.Text))
                    //    {
                    //        lblPromoVouUsedFlag.Text = "U";
                    //        _proVouInvcLine = _lineno0;
                    //        _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_res_line_no = Convert.ToInt32(lblPromoVouNo.Text.ToString()));
                    //        _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_res_no = "PROMO_VOU");
                    //        _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_seq = Convert.ToInt32(lblPromoVouNo.Text.ToString()));
                    //        _invoiceItemList.Where(x => x.Sad_itm_line == _lineno0).ToList().ForEach(x => x.Sad_dis_type = "M");
                    //        //_tempItem.Sad_res_line_no = Convert.ToInt32(lblPromoVouNo.Text.ToString());
                    //        //_tempItem.Sad_res_no = "PROMO_VOU";
                    //    }
                    //}
                    BindAddItem();
                    CalculateGrandTotal(_one.Sad_qty, _unitRate, _disVal, _vatPortion, true);
                    decimal _tobepays = 0;
                    if (lblSVatStatus.Text == "Available") _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim()); else _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                    //ucPayModes1.TotalAmount = _tobepays;
                    ////update promotion
                    //List<InvoiceItem> _temItems = (from _invItm in _invoiceItemList
                    //                               where !string.IsNullOrEmpty(_invItm.Sad_promo_cd)
                    //                               select _invItm).ToList<InvoiceItem>();
                    //if (_temItems != null && _temItems.Count > 0)
                    //{
                    //    ucPayModes1.ISPromotion = true;
                    //}
                    //else
                    //    ucPayModes1.ISPromotion = false;
                    //ucPayModes1.InvoiceItemList = _invoiceItemList;
                    //ucPayModes1.SerialList = InvoiceSerialList;
                    //ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays));
                    //ucPayModes1.lblBalanceAmountPub.Text = _tobepays.ToString("N2"); ;
                    //if (ucPayModes1.HavePayModes && _isCombineAdding == false)
                    //{
                    //    ucPayModes1.LoadData();
                    //}
                    mpSavePO.Hide();
                }

        }
        protected void lbtnPriceClose_Click(object sender, EventArgs e)
        {
            txtPriceEdit.Text = "";
            MpPriceEdit.Hide();
        }
        protected void lbtnItemPrice_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                lblDiscountRowNum.Text = dr.RowIndex.ToString();
                MpPriceEdit.Show();
                txtPriceEdit.Text = string.Empty;
                txtPriceEdit.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
            }
        }

        protected void ItemFocus_Click(object sender, EventArgs e)
        {
            btnSearch_Item.Focus();
            txtItem.Focus();
        }

        //private int CheckProfitCenterAllowForCredit()
        //{
        //    int _iscredit = 0;
        //    if ((_MasterProfitCenter != null) && (_priceBookLevelRef != null))
        //    {
        //        _iscredit = _MasterProfitCenter.Mpc_chk_credit;
        //    }
        //    return _isAvailable;
        //}


        protected void lbtnSoAppConfirm_Click(object sender, EventArgs e)
        {
            MdlSalesOrderApp.Hide();
            IscheckApproval = true;
            saveNew();

        }
        private void DispMsg(string msgText, string msgType = "")
        {
            msgText = msgText.Replace("\"", " ").Replace("'", " ").Replace(":", " ").Replace("\n", " ").Replace(@"\", " ");

            if (msgType == "N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyNoticeToast('" + msgText + "');", true);
            }
            else if (msgType == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('" + msgText + "');", true);
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
            else if (msgType == "A")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "alert('" + msgText + "');", true);
            }
        }

        public string ValidateReservationNo()
        {
            string _err = "";
            #region add validation for reservation by lakshan 03 Mar 2017
            if (!string.IsNullOrEmpty(txtresno.Text))
            {
                if (string.IsNullOrEmpty(txtPerTown.Text))
                {
                    _err = "Please enter dispatch location !"; return _err;
                }
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    txtresno.Text = ""; _err = "Please select the item !"; return _err;
                }
                if (cmbStatus.SelectedValue == "0")
                {
                    txtresno.Text = ""; _err = "Please select the item status !"; return _err;
                }

                _resLogAvaData = CHNLSVC.Inventory.GET_INR_RES_LOG_DATA_NEW(
                                new INR_RES_LOG()
                                {
                                    IRL_RES_NO = txtresno.Text.Trim(),
                                    IRL_ITM_CD = txtItem.Text.Trim(),
                                    IRL_ITM_STUS = cmbStatus.SelectedValue,
                                    IRL_CURT_COM = Session["UserCompanyCode"].ToString(),
                                    IRL_CURT_LOC = txtPerTown.Text.Trim().ToUpper(),
                                    IRL_CURT_DOC_TP = "INV",
                                    IRL_CURT_DOC_NO = txtresno.Text.Trim(),
                                    IRL_ACT = 1
                                });
                if (_resLogAvaData.Count < 1)
                {
                    txtresno.Text = ""; _err = "Invalid reservation number !"; return _err;
                }
                decimal _resAvaQty = _resLogAvaData.Sum(c => c.IRL_RES_BQTY);
                decimal _tmpDec = 0;
                //decimal _mrnBalAva = _inrRes.IRD_RES_BQTY + _inrRes.Ird_so_mrn_bqty;
                decimal _appqty = decimal.TryParse(txtQty.Text, out _tmpDec) ? Convert.ToDecimal(txtQty.Text) : 0;
                if (_resAvaQty < _appqty)
                {
                    txtresno.Text = ""; _err = "No reservation balance available !"; return _err;
                }

                INR_RES_DET _inrRes = CHNLSVC.Inventory.GET_INR_RES_DET_DATA_NEW(new INR_RES_DET()
                {
                    IRD_RES_NO = txtresno.Text.Trim(),
                    IRD_ITM_CD = txtItem.Text.Trim(),
                    IRD_ITM_STUS = cmbStatus.SelectedValue
                }).FirstOrDefault();
                if (_inrRes == null)
                {
                    txtresno.Text = "";
                    _err = "Invalid reservation details !"; return _err;
                }
                decimal _resDBal = _inrRes.IRD_MRN_AVA_BAL;
                if (_resDBal <= 0)
                {
                    txtresno.Text = "";
                    _err = "Reservation detail balance is not available !"; txtresno.Text = ""; return _err; ;
                }
                if (_resDBal < _appqty)
                {
                    txtresno.Text = "";
                    _err = "Reservation detail balance is not available !"; txtresno.Text = ""; return _err;
                }
                if (_invoiceItemList != null)
                {
                    if (_invoiceItemList.Count > 0)
                    {
                        decimal _resBalVal = _invoiceItemList.Where(c => c.Sad_res_no == txtresno.Text.Trim()).ToList().Sum(a => a.Sad_qty);
                        if (_resDBal < (_resBalVal + _appqty))
                        {
                            txtresno.Text = "";
                            _err = "Reservation detail balance is not available !"; txtresno.Text = ""; return _err; ;
                        }
                    }
                }
            }
            #endregion
            return _err;
        }
        public string ValidateReservationNo(string _resNo, string _itmCd, string _itmStus, string _qty)
        {
            string _err = "";
            #region add validation for reservation by lakshan 03 Mar 2017
            if (!string.IsNullOrEmpty(_resNo))
            {
                _resLogAvaData = CHNLSVC.Inventory.GET_INR_RES_LOG_DATA_NEW(
                                new INR_RES_LOG()
                                {
                                    IRL_RES_NO = _resNo,
                                    IRL_ITM_CD = _itmCd,
                                    IRL_ITM_STUS = _itmStus,
                                    IRL_CURT_COM = Session["UserCompanyCode"].ToString(),
                                    IRL_CURT_LOC = txtPerTown.Text.Trim().ToUpper(),
                                    IRL_CURT_DOC_TP = "INV",
                                    IRL_CURT_DOC_NO = _resNo,
                                    IRL_ACT = 1
                                });
                if (_resLogAvaData.Count < 1)
                {
                    _err = "Invalid reservation number !"; return _err;
                }
                decimal _resAvaQty = _resLogAvaData.Sum(c => c.IRL_RES_BQTY);
                decimal _tmpDec = 0;
                //decimal _mrnBalAva = _inrRes.IRD_RES_BQTY + _inrRes.Ird_so_mrn_bqty;
                decimal _appqty = decimal.TryParse(_qty, out _tmpDec) ? Convert.ToDecimal(_qty) : 0;
                if (_resAvaQty < _appqty)
                {
                    _err = "No reservation balance available !"; return _err;
                }

                INR_RES_DET _inrRes = CHNLSVC.Inventory.GET_INR_RES_DET_DATA_NEW(new INR_RES_DET()
                {
                    IRD_RES_NO = _resNo,
                    IRD_ITM_CD = _itmCd,
                    IRD_ITM_STUS = _itmStus
                }).FirstOrDefault();
                if (_inrRes == null)
                {
                    _err = "Invalid reservation details !"; return _err; ;
                }
                decimal _resDBal = _inrRes.IRD_MRN_AVA_BAL;
                if (_resDBal <= 0)
                {
                    _err = "Reservation detail balance is not available !"; return _err; ;
                }
                if (_resDBal < _appqty)
                {
                    _err = "Reservation detail balance is not available !"; return _err;
                }
                if (_invoiceItemList != null)
                {
                    if (_invoiceItemList.Count > 0)
                    {
                        decimal _resBalVal = _invoiceItemList.Where(c => c.Sad_res_no == _resNo && c.Sad_itm_cd == _itmCd).ToList().Sum(a => a.Sad_qty);
                        if (_resDBal < (_resBalVal + _appqty))
                        {
                            _err = "Reservation detail balance is not available !"; return _err; ;
                        }
                    }
                }
            }
            #endregion
            return _err;
        }
        protected void lbtnResEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow grdr = (GridViewRow)btn.NamingContainer;
            var row = (GridViewRow)btn.NamingContainer;
            if (row != null)
            {
                gvInvoiceItem.EditIndex = grdr.RowIndex;//e.NewEditIndex;
                List<InventoryRequestItem> _select = new List<InventoryRequestItem>();
                _select = Session["ScanItemListNew"] as List<InventoryRequestItem>;
                gvInvoiceItem.DataSource = _invoiceItemList;
                gvInvoiceItem.DataBind();

            }
        }

        protected void lbtnResUpdate_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow grdr = (GridViewRow)btn.NamingContainer;
            var row = (GridViewRow)btn.NamingContainer;
            string txtGridResNo = (grdr.FindControl("txtGridResNo") as TextBox).Text.Trim();
            string InvItm_Item = (grdr.FindControl("InvItm_Item") as Label).Text.Trim();
            string lblsad_itm_stus = (grdr.FindControl("lblsad_itm_stus") as Label).Text.Trim();
            string InvItm_Qty = (grdr.FindControl("InvItm_Qty") as Label).Text.Trim();
            Int32 lblsad_itm_line = Convert.ToInt32((grdr.FindControl("lblsad_itm_line") as Label).Text.Trim());
            if (!string.IsNullOrEmpty(txtGridResNo))
            {
                string _errMsg = ValidateReservationNo(txtGridResNo, InvItm_Item, lblsad_itm_stus, InvItm_Qty);
                if (!string.IsNullOrEmpty(_errMsg))
                {
                    DisplayMessage(_errMsg, 1);
                    gvInvoiceItem.EditIndex = -1;
                    gvInvoiceItem.DataSource = _invoiceItemList;
                    gvInvoiceItem.DataBind();
                }
                else
                {
                    var v = _invoiceItemList.Where(c => c.Sad_itm_cd == InvItm_Item && c.Sad_itm_stus == lblsad_itm_stus
                        && c.Sad_itm_line == lblsad_itm_line).FirstOrDefault();
                    if (v != null)
                    {
                        v.Sad_res_no = txtGridResNo;
                    }
                    gvInvoiceItem.EditIndex = -1;
                    gvInvoiceItem.DataSource = _invoiceItemList;
                    gvInvoiceItem.DataBind();
                }
            }
            else
            {
                var v = _invoiceItemList.Where(c => c.Sad_itm_cd == InvItm_Item && c.Sad_itm_stus == lblsad_itm_stus
                      && c.Sad_itm_line == lblsad_itm_line).FirstOrDefault();
                if (v != null)
                {
                    v.Sad_res_no = "";
                }
                gvInvoiceItem.EditIndex = -1;
                gvInvoiceItem.DataSource = _invoiceItemList;
                gvInvoiceItem.DataBind();
            }
        }
        private void GetCustomerDiscount()
        {
            if (!string.IsNullOrEmpty(txtCustomer.Text))
            {
                List<CashGeneralEntiryDiscountDef> _disList = CHNLSVC.Sales.GetGeneralEntityDiscountDef(Session["UserCompanyCode"].ToString(),
                    Session["UserDefProf"].ToString(),
                    Convert.ToDateTime(txtdate.Text.Trim()).Date,
                    cmbBook.Text.Trim(),
                    cmbLevel.Text.Trim());
                if (_disList != null)
                {
                    var _custDis = _disList.Where(c => c.Sgdd_cust_cd == txtCustomer.Text.Trim().ToUpper()).FirstOrDefault();
                    if (_custDis != null)
                    {
                        txtDisRate.Text = _custDis.Sgdd_disc_rt.ToString();
                        txtDisRate_TextChanged(null, null);
                    }
                }
            }
        }
        //Dulaj 2018/Dec/20
        private bool CheckPCTempSave()
        {
            bool isTempSave = false;
            DataTable paraTB = CHNLSVC.Inventory.getMstSysPara(Session["UserCompanyCode"].ToString(), "PC", Session["UserDefProf"].ToString(), "TEMPSOPC", Session["UserCompanyCode"].ToString());
            if (paraTB != null)
            {

                if (paraTB.Rows.Count > 0)
                {
                    isTempSave = true;
                }
            }
            return isTempSave;
        }

        protected void lbtnapproveTemp_Click(object sender, EventArgs e)
        {
            if (txtapprove.Value == "Yes")
            {
                try
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16138))
                    {
                        string msg = "You dont have permission to approve .Permission code : 16138";
                        DisplayMessage(msg, 1);
                        return;
                    }
                    string ordstatus = (string)Session["STATUS"];
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an order !!!')", true);

                        lbtnsupplier.Focus();
                        return;
                    }
                    if (ordstatus == "A")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This order is already approved !!!')", true);

                        return;
                    }
                    _userid = (string)Session["UserID"];
                    SalesOrderHeader _SalesOrder = new SalesOrderHeader();
                    _SalesOrder.SOH_STUS = "S";
                    _SalesOrder.SOH_SO_NO = txtInvoiceNo.Text.Trim();
                    _SalesOrder.SOH_COM = Session["UserCompanyCode"].ToString();
                    _SalesOrder.SOH_PC = Session["UserDefProf"].ToString();
                    _SalesOrder.SOH_MOD_BY = _userid;
                    _SalesOrder.SOH_MOD_WHEN = CHNLSVC.Security.GetServerDateTime();
                    _SalesOrder.SOH_SALES_EX_CD = txtexcutive.Text;
                    _SalesOrder.SOH_MAN_REF = txtManualRefNo.Text;
                    _SalesOrder.SOH_CUS_CD = txtCustomer.Text;
                    _SalesOrder.SOH_CUS_NAME = txtCusName.Text;
                    string error = string.Empty;
                    Int32 outputresultapprove = CHNLSVC.Sales.UpdateSOStatus(_SalesOrder, out error);

                    if (outputresultapprove == 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully approved !!!')", true);
                        //added by dilshan on 04-09-2018******
                        CHNLSVC.MsgPortal.GenarateSOSMS(Session["UserCompanyCode"].ToString(), txtPoNo.Text, txtexcutive.Text, "");
                        //************************************                           
                        Clear(); ClearAll();
                        UserDPopoup.Hide();
                    }
                    else
                    {
                        // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                        DispMsg(error, "E");
                    }
                }
                catch (Exception ex)
                {
                    // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
                    DispMsg(ex.Message, "E");
                }
            }
            UserDPopoup.Hide();
        }
    }
}