using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Inventory;
using FastForward.SCMWeb.View.Reports.Sales;
using FF.BusinessObjects;
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
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Sales
{
    public partial class CustomerQuotation : BasePage
    {

        #region Variables
        protected Int32 SOConfirm { get { return (Int32)Session["SOConfirm"]; } set { Session["SOConfirm"] = value; } }
        protected Int32 _serID { get { return (Int32)Session["_serID"]; } set { Session["_serID"] = value; } }
        protected String _itmType { get { return (string)Session["_itmType"]; } set { Session["_itmType"] = value; } }
        protected Boolean _isMinus { get { return (Boolean)Session["_isMinus"]; } set { Session["_isMinus"] = value; } }
        protected MasterCompany _companyDet { get { return (MasterCompany)Session["_companyDet"]; } set { Session["_companyDet"] = value; } }
        protected decimal SSNormalPrice { get { return (decimal)Session["SSNormalPrice"]; } set { Session["SSNormalPrice"] = value; } }
        protected List<QuotationSerial> _QuoSerials { get { return (List<QuotationSerial>)Session["_QuoSerials"]; } set { Session["_QuoSerials"] = value; } }
        protected Int32 quoSeq { get { return (Int32)Session["quoSeq"]; } set { Session["quoSeq"] = value; } }
        protected List<ReptPickSerials> _ResList { get { return (List<ReptPickSerials>)Session["_ResList"]; } set { Session["_ResList"] = value; } }
        protected Decimal _totDPAmt { get { return (Decimal)Session["_totDPAmt"]; } set { Session["_totDPAmt"] = value; } }
        protected Decimal _dpRate { get { return (Decimal)Session["_dpRate"]; } set { Session["_dpRate"] = value; } }
        protected Int32 _isQuoBase { get { return (Int32)Session["_isQuoBase"]; } set { Session["_isQuoBase"] = value; } }
        protected Int32 _isSelQuoBaseLevel { get { return (Int32)Session["_isSelQuoBaseLevel"]; } set { Session["_isSelQuoBaseLevel"] = value; } }
        protected Int16 _quoValidPeriod { get { return (Int16)Session["_quoValidPeriod"]; } set { Session["_quoValidPeriod"] = value; } }
        protected List<QoutationDetails> _invoiceItemList { get { return (List<QoutationDetails>)Session["_invoiceItemList"]; } set { Session["_invoiceItemList"] = value; } }
        protected List<QoutationDetails> _invoiceItemListWithDiscount { get { return (List<QoutationDetails>)Session["_invoiceItemListWithDiscount"]; } set { Session["_invoiceItemListWithDiscount"] = value; } }
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


        protected bool _isStrucBaseTax { get { Session["_isStrucBaseTax "] = (Session["_isStrucBaseTax "] == null) ? false : (bool)Session["_isStrucBaseTax "]; ; return (bool)Session["_isStrucBaseTax "]; } set { Session["_isStrucBaseTax "] = value; } }
        protected bool _isAllocateCustomer { get { Session["_isAllocateCustomer "] = (Session["_isAllocateCustomer "] == null) ? false : (bool)Session["_isAllocateCustomer "]; ; return (bool)Session["_isAllocateCustomer "]; } set { Session["_isAllocateCustomer "] = value; } }
        #endregion Variables

        protected void Page_Load(object sender, EventArgs e)
        {
            clearMessages();
            if (!IsPostBack)
            {
                try
                {
                    ClearVariables();

                    Clear();
                    Clear_Data();
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
                    chkDeliverNow.Checked = true;
                 
                    txtexcutive.Text = string.Empty;
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
                //else if (objectNames[objectNames.Length - 1] == "txtSerialNo")
                //{
                //    CheckSerialAvailability();
                //}

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
          

            //txtDisRate.Attributes.Add("onblur", Page.ClientScript.GetPostBackEventReference(txtDisRate, "OnBlur"));
            //txtDisAmt.Attributes.Add("onblur", Page.ClientScript.GetPostBackEventReference(txtDisAmt, "OnBlur"));
        }

        #region Main Buttons
       

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtconfirmplaceord.Value == "Yes")
                {
                    if (lblStus.Text == "Approved")
                    {
                        string msg = "Selected quotation already approved.";
                        DisplayMessage(msg, 2);
                        return;
                    }
                    if (lblStus.Text == "Cancelled")
                    {
                        string msg = "Selected quotation already cancelled.";
                        DisplayMessage(msg, 2);
                        return;
                    }
                    if (lblStus.Text == "Pending")
                    {
                        string msg = "Selected quotation already save.";
                        DisplayMessage(msg, 2);
                        return;
                    }
                    saveQO();

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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
                    if (lblStus.Text == "Pending")
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16008))
                        {
                            string _msg = "Sorry, You have no permission to cancel this Customer Quotation.( Advice: Required permission code : 16008)";
                            DisplayMessage(_msg, 2);
                            return;
                        }
                    }
                    if (lblStus.Text == "Approved")
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16052))
                        {
                            string _msg = "Sorry, You have no permission to cancel this Customer Quotation.( Advice: Required permission code : 16052)";
                            DisplayMessage(_msg, 2);
                            return;
                        }
                    }
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text))
                    {
                        DisplayMessage("Please select quotation #", 2);
                        return;
                    }
                    if (lblStus.Text == "Finished")
                    {
                        DisplayMessage("This is quotation already invoice", 2);
                        return;
                    }
                    if (lblStus.Text == "Cancelled")
                    {
                        DisplayMessage("This is already cancelled", 2);   
                        return;
                    }
                    DataTable _dtl = CHNLSVC.Sales.GetDeliveredQuotation(Session["UserCompanyCode"].ToString(), txtInvoiceNo.Text);
                    if (_dtl != null && _dtl.Rows.Count > 0)
                    {
                        DisplayMessage("Unable to cancel this quotation, Delivery order already raised based on this", 2);   
                        return;
                    }


                    //if (MessageBox.Show("Are you sure ?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                    QuotationHeader _cancelHdr = new QuotationHeader();
                    _cancelHdr = CHNLSVC.Sales.Get_Quotation_HDR(txtInvoiceNo.Text);
                    if (_cancelHdr != null)
                    {
                        string _outmsg = "";
                        Int32 _eff = CHNLSVC.Sales.QuotationCancelProcess(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoiceNo.Text,"","","","", out _outmsg);
                        if (_eff != -99 && _eff >= 0)
                        {
                            DisplayMessage("Successfully cancelled", 3);   
                            Clear_Data();
                        }
                        else
                        {
                            DisplayMessage(_outmsg, 4);   
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
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

     
        protected void lbtnupload_Click(object sender, EventArgs e)
        {
            try
            {
              

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
                if (lblvalue.Text == "engin")
                {
                    txtengine.Text = grdResult.SelectedRow.Cells[1].Text;
                    ReptPickSerials _serialList = new ReptPickSerials();
                    _serialList = CHNLSVC.Inventory.GetAvailableSerIDInformation(Session["UserCompanyCode"].ToString(),
                        Session["UserDefLoca"].ToString(), lblItem.Text.Trim(), txtengine.Text.Trim(), string.Empty, string.Empty);

                    if (_serialList.Tus_ser_1 != null)
                    {
                        txtengine.Text = _serialList.Tus_ser_1;
                        _serID = _serialList.Tus_ser_id;
                        if (_serialList.Tus_ser_2 != null)
                        {
                            txtChasis.Text = _serialList.Tus_ser_2;
                        }
                        else
                        {
                            txtChasis.Text = "";
                        }

                    }

                    serialPopoup.Show();
                }
                if (lblvalue.Text == "Sale_Ex")
                {
                    txtexcutive.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtexcutive.ToolTip = grdResult.SelectedRow.Cells[2].Text;
                    lblSalesEx.Text = grdResult.SelectedRow.Cells[2].Text;
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

                //if (lblvalue.Text == "76")
                //{
                //    txtSerialNo.Text = grdResult.SelectedRow.Cells[1].Text;
                //    Session["WARRANTY"] = grdResult.SelectedRow.Cells[2].Text;
                //    //txtSerialNo_TextChanged(null, null);
                //    CheckSerialAvailability();
                //}

                //if (lblvalue.Text == "158")
                //{
                //    txtSerialNo.Text = grdResult.SelectedRow.Cells[1].Text;
                //    CheckSerialAvailability();
                //    chkDeliverLater.Checked = false;
                //}
                //if (lblvalue.Text == "16")
                //{
                //    txtCustomer.Text = grdResult.SelectedRow.Cells[2].Text;
                //    LoadCusData();
                //    LoadCusLoyalityNo();
                //    LoadInvItems(Convert.ToInt32(grdResult.SelectedRow.Cells[3].Text));
                //}
                
                if (lblvalue.Text == "81")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    //  LoadItemDetail(txtItem.Text);
                    txtItem_TextChanged(null, null);
                }

                if (lblvalue.Text == "Quotation")
                {
                    txtInvoiceNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    load_save_Quotation();
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
               
                if (lblvalue.Text == "InvoiceWithDate")
                {
                    txtInvoiceNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtInvoiceNo_TextChanged(null, null);
                }
                if (lblvalue.Text == "CustomerDel")
                {
                    MpDelivery.Show();
                    txtdelcuscode.Text = grdResult.SelectedRow.Cells[1].Text;
                }
                if (lblvalue.Text == "LocationDel")
                {
                    MpDelivery.Show();
                  
                }
                
              

                if (lblvalue.Text == "Customer_DEL")
                {
                    txtdelcuscode.Text = grdResult.SelectedRow.Cells[1].Text;
                    LoadCusData_Del();
                    MpDelivery.Show();
                }
              
                else if (lblvalue.Text == "16")
                {
                    _invoiceItemList = new List<QoutationDetails>();
                    txtCustomer.Text = grdResult.SelectedRow.Cells[2].Text;
                    LoadCusData();
                    LoadCusLoyalityNo();
                    string Reqno = grdResult.SelectedRow.Cells[1].Text;
                   
                    txtdocrefno.Text = Reqno;

                    LoadHDD(Reqno);
                    LoadPriceLevel(cmbInvType.Text, cmbBook.Text);
                    LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                    CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                    ClearPriceTextBox();
                  
                    foreach (GridViewRow row in gvInvoiceItem.Rows)
                    {
                        Decimal qty = 0;
                        Decimal upri = 0;
                        Decimal dis = 0;
                        Decimal tax = 0;

                        if ((string.IsNullOrEmpty(row.Cells[4].Text.ToString())) || (row.Cells[4].Text.ToString() == "&nbsp;"))
                        {
                            qty = 0;
                        }
                        else
                        {
                            qty = Convert.ToDecimal(row.Cells[4].Text);
                        }

                        if ((string.IsNullOrEmpty(row.Cells[5].Text.ToString())) || (row.Cells[5].Text.ToString() == "&nbsp;"))
                        {
                            upri = 0;
                        }
                        else
                        {
                            upri = Convert.ToDecimal(row.Cells[5].Text);
                        }

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

                        CalculateGrandTotalNew(qty, upri, dis, tax, true);
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

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "Quotation")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerQuo);
                    DataTable result = CHNLSVC.CommonSearch.GetQuotationAll(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "Quotation";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                if (lblvalue.Text == "engin")
                {
                    //string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
                   // DataTable result = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                    DataTable result = CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "engin";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
                }
                if (lblvalue.Text == "Customer_13")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerBYsalesExe(SearchParams,  ddlSearchbykey.SelectedItem.Text, "%" + txtSearchbyword.Text, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
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
                    DataTable result = CHNLSVC.Sales.SearchSalesRequest(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "16";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
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
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "421";
                    ViewState["SEARCH"] = result;
                    SIPopup.Show(); Session["SIPopup"] = "SIPopup";
                    txtSearchbyword.Focus();
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
                    DataTable result = CHNLSVC.Sales.SearchSalesOrder(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text);
                    grdResultD.DataSource = result;
                    grdResultD.DataBind();
                    lblvalue.Text = "InvoiceWithDate";
                    ViewState["SEARCH"] = result;
                    UserDPopoup.Show();
                    Session["DPopup"] = "DPopup";
                    txtSearchbyword.Focus();
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
        private void Clear_Data()
        {
            _dpRate = 0;
            _isSelQuoBaseLevel = 0;
            _serverDt = CHNLSVC.Security.GetServerDateTime().Date;
            MasterCompany _masterComp = null;
            _masterComp = CHNLSVC.General.GetCompByCode(Session["UserCompanyCode"].ToString());

            if (_masterComp.MC_TAX_CALC_MTD == "1")
            {
                _isStrucBaseTax = true;
            }
            else { _isStrucBaseTax = false; }

            _MasterProfitCenter = null;
            _PriceDefinitionRef = null;
            _isGroup = false;
            _priceBookLevelRef = null;
            _priceBookLevelRefList = null;
            _companyDet = null;
            PriceCombinItemSerialList = null;
            _masterBusinessCompany = new MasterBusinessEntity();
            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            _itemdetail = new MasterItem();
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            _invoiceItemList = new List<QoutationDetails>();
            _QuoSerials = new List<QuotationSerial>();
            _ResList = new List<ReptPickSerials>();
            _priceDetailRef = null;
            MainTaxConstant = null;
            _masterItemComponent = null;
            _isCheckedPriceCombine = false;
            IsSaleFigureRoundUp = false;
            chkDeliverLater.Checked = true;
            gvInvoiceItem.AutoGenerateColumns = false;
            gvInvoiceItem.DataSource = new List<QoutationDetails>();
            gvInvoiceItem.DataBind();

            IsPriceLevelAllowDoAnyStatus = false;
            grdSerial.AutoGenerateColumns = false;
            grdSerial.DataSource = new List<QuotationSerial>();
            grdSerial.DataBind();
            lblLine.Text = "";
            lblItem.Text = "";
            lblQty.Text = "";
            txtengine.Text = "";
            txtChasis.Text = "";
            VirtualCounter = 1;
            DefaultBook = string.Empty;
            DefaultLevel = string.Empty;
            DefaultInvoiceType = string.Empty;
            DefaultStatus = string.Empty;
            _isBackDate = false;
            _quoValidPeriod = 0;
            _lineNo = 0;
            GrndSubTotal = 0;
            GrndDiscount = 0;
            GrndTax = 0;
            lblGrndSubTotal.Text = FormatToCurrency("0");
            lblGrndDiscount.Text = FormatToCurrency("0");
            lblGrndAfterDiscount.Text = FormatToCurrency("0");
            lblGrndTax.Text = FormatToCurrency("0");
            lblGrndTotalAmount.Text = FormatToCurrency("0");

           // LoadCachedObjects();
           // InitializeValuesNDefaultValueSet();
            //BackDatePermission();
            txtdocrefno.Text = "";
            txtManualRefNo.Text = "";
            txtInvoiceNo.Text = "";
            txtCustomer.Text = "";
            txtCusName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtMobile.Text = "";

            txtdelad1.Text = "";
            txtdelad2.Text = "";
            txtdelcuscode.Text = "";
            txtdelname.Text = "";
            txtDNic.Text = "";
            txtDMob.Text = "";
            txtDFax.Text = "";
            txtItem.Text = "";
            lblItemModel.Text = "";
            chkTaxPayable.Checked = false;
            //WarrantyRemarks = string.Empty;
            SSPriceBookPrice = 0;
            SSPriceBookSequance = string.Empty;
            SSPriceBookItemSequance = string.Empty;
            SSIsLevelSerialized = string.Empty;
            SSPromotionCode = string.Empty;
            SSCirculerCode = string.Empty;
            SSPRomotionType = 0;
            SSNormalPrice = 0;
            SSCombineLine = 0;
            WarrantyPeriod = 0;
            _isCompleteCode = false;
            _isCombineAdding = false;
            _isEditPrice = false;
            _isEditDiscount = false;
            _isInventoryCombineAdded = false;
            ManagerDiscount = null;
            //pnlPriceNPromotion.Visible = false;
            _isFirstPriceComItem = false;
            _combineCounter = 0;
            txtexcutive.Text = "";
            txtPaymentTerm.Text = "";
            txtRemarks.Text = "";
            txtAddWara.Text = "";
            lblVatExemptStatus.Text = "";
            lblSVatStatus.Text = "";
            //dtpValidTo.Enabled = true;
            //btnSave.Enabled = true;
            chkRes.Checked = false;
            _serID = 0;
            lblStus.Text = "";
            _itmType = string.Empty;
            _isMinus = false;
            _totDPAmt = 0;
            quoSeq = 0;
            // LoadExecutive();
        }
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

                if (IsAllovcateCustomer(txtexcutive.Text))
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Customer);
                    DataTable result = CHNLSVC.CommonSearch.GetCustomerBYsalesExe(SearchParams, null, null, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
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
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
                DataTable result = CHNLSVC.Sales.SearchSalesOrder(SearchParams, null, null);
                grdResultD.DataSource = result;
                grdResultD.DataBind();
                lblvalue.Text = "InvoiceWithDate";
                BindUCtrlDDLData(result);
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

        public void BindUCtrlDDLData2(DataTable _dataSource)
        {
            this.ddlSearchbykeyD.Items.Clear();
            foreach (DataColumn col in _dataSource.Columns)
            {
                this.ddlSearchbykeyD.Items.Add(col.ColumnName);
            }
            this.ddlSearchbykeyD.SelectedIndex = 0;
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
                //if (_priceBookLevelRef.Sapl_is_serialized && string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                //{
                //    DisplayMessage("You are going to select a serialized price level without serial.Please select the serial !!!");
                //    txtSerialNo.Text = string.Empty;
                //    return;
                //}
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
                if (_isMinus == true)
                {
                    decimal val = Convert.ToDecimal(txtUnitPrice.Text);
                    _totDPAmt = _totDPAmt + val;
                    val = val * -1;
                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(val));
                }
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
                Session["DCUSADD1"] = txtdelad1.Text.Trim();
                Session["DCUSADD2"] = txtdelad2.Text.Trim();
               
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
                txtDNic.Text = string.Empty;
                txtDMob.Text = string.Empty;
                txtDFax.Text = string.Empty;
                txtdelname.Text = string.Empty;
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
            txtdelcuscode.Text = txtCustomer.Text;
            txtdelname.Text = txtCusName.Text;
            txtdelad1.Text = txtAddress1.Text;
            txtdelad2.Text = txtAddress2.Text;
            txtDNic.Text = txtNIC.Text;
            txtDMob.Text = txtMobile.Text;
           
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
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                txtQty.Text = "1";
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
            txtItem.Text = txtItem.Text.ToUpper();

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


              
                if (_IsVirtualItem)
                {
                    bool block = CheckBlockItem(txtItem.Text.Trim(), 0, false);
                    if (block)
                    {
                        txtItem.Text = "";
                    }
                }

              
                lblSelectRevLine.Text = "";
               
                bool isJSEND = false;
                CheckQty(true, out isJSEND);
              
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
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...s');", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing...');", true);
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
            if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
            else return Math.Round(value, 2);
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
                            if (_isTaxfaction == false) _taxsEffDt = CHNLSVC.Sales.GetTaxLog(Session["UserCompanyCode"].ToString(), _item, _status, Convert.ToDateTime(txtdate.Text)); else _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT", Convert.ToDateTime(txtdate.Text));
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
                                    if (_isStrucBaseTax == true)       //RUKSHAN
                                    {
                                        MasterItem _mstItem = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim());
                                        _tax = CHNLSVC.Sales.GetItemTax_strucbase(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, "VAT", _mstItem.Mi_anal1);
                                    }
                                    else
                                        _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, "VAT");

                                    //List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                                    if (_tax != null && _tax.Count > 0)
                                    {
                                        decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                        txtTaxAmt.Text = SetDecimalPoint(Convert.ToString(FigureRoundUp(_vatval, true)));
                                    }
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

            //if (_itemdetail.Mi_is_ser1 == 1 && !string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
            //    txtQty.Text = "1";
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
            _PriceDetailRefPromo = CHNLSVC.Sales.GetPriceForQuo(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtdate.Text), Convert.ToDateTime(txtValidTo.Text));

            //if (_PriceDetailRefPromo == null && _PriceSerialRefPromo == null && _PriceSerialRefNormal == null) return false;
            //if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0 && !string.IsNullOrEmpty(txtSerialNo.Text))
            //{
            //    Session["_PriceDetailRefPromo"] = _PriceDetailRefPromo;
            //    Session["_PriceSerialRefPromo"] = _PriceSerialRefPromo;
            //    Session["_PriceSerialRefNormal"] = _PriceSerialRefNormal;

            //    var _isSerialAvailable = _PriceSerialRefNormal.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
            //    if (_isSerialAvailable != null && _isSerialAvailable.Count > 0)
            //    {
            //        if (hdfnormalSerialized.Value == "")
            //        {
            //            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "selectNormalSerialized('');", true);
            //            IsReturnToScript = 1;
            //            return false;
            //        }


            //        if (hdfnormalSerialized.Value != null && hdfnormalSerialized.Value.ToString() == "1")
            //        {
            //            _isNewPromotionProcess = true;
            //            CheckSerializedPriceLevelAndLoadSerials(true);
            //            return true;
            //        }
            //        else
            //        {

            //        }

            //        //DialogResult _normalSerialized = new DialogResult();
            //        //using (new CenterWinDialog(this))
            //        //{
            //        //    _normalSerialized = MessageBox.Show("This item is having normal serialized price.Do you need to select normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        //}
            //        //if (_normalSerialized == System.Windows.Forms.DialogResult.Yes)
            //        //{
            //        //    _isNewPromotionProcess = true;
            //        //    CheckSerializedPriceLevelAndLoadSerials(true);
            //        //    return true;
            //        //}
            //    }
            //    else
            //    {
            //        _isNewPromotionProcess = false;
            //        _PriceSerialRefNormal = null;
            //    }
            //}
            //else if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0 )
            //{
            //    if (hdfcontinueWithNormalSerializedPrice.Value == "")
            //    {
            //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "continueWithNormalPerializedPrice('');", true);
            //        IsReturnToScript = 1;
            //        return false;
            //    }

            //    if (hdfcontinueWithNormalSerializedPrice != null && hdfcontinueWithNormalSerializedPrice.Value == "1")
            //    {
            //        _isNewPromotionProcess = true;
            //        CheckSerializedPriceLevelAndLoadSerials(true);
            //        return true;
            //    }
            //    else
            //    {
            //        _isNewPromotionProcess = false;
            //        _PriceSerialRefNormal = null;
            //    }


            //    //DialogResult _normalSerialized = new System.Windows.Forms.DialogResult();
            //    //using (new CenterWinDialog(this)) { _normalSerialized = MessageBox.Show("This item having normal serialized price. Do you need to continue with normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
            //    //if (_normalSerialized == System.Windows.Forms.DialogResult.Yes)
            //    //{
            //    //    _isNewPromotionProcess = true;
            //    //    CheckSerializedPriceLevelAndLoadSerials(true);
            //    //    return true;
            //    //}
            //    //else
            //    //{
            //    //    _isNewPromotionProcess = false;
            //    //    _PriceSerialRefNormal = null;
            //    //}
            //}
            //if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0 && !string.IsNullOrEmpty(txtSerialNo.Text))
            //{
            //    var _isSerialPromoAvailable = _PriceSerialRefPromo.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
            //    if (_isSerialPromoAvailable != null && _isSerialPromoAvailable.Count > 0)
            //    {

            //        if (hdfselectPromotionalSerializedPrice.Value == "")
            //        {
            //            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "alert", "selectPromotionalSerializedPrice('');", true);
            //            IsReturnToScript = 1;
            //            return false;
            //        }

            //        if (hdfselectPromotionalSerializedPrice.Value != null && hdfselectPromotionalSerializedPrice.Value == "1")
            //        {
            //            _isNewPromotionProcess = true;
            //            CheckSerializedPriceLevelAndLoadSerials(true);
            //            return true;
            //        }
            //        else
            //        {
            //            _isNewPromotionProcess = false;
            //            _PriceSerialRefPromo = null;
            //        }

            //        //DialogResult _promoSerialized = new System.Windows.Forms.DialogResult();
            //        //using (new CenterWinDialog(this)) { _promoSerialized = MessageBox.Show("This item is having promotional serialized price.Do you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
            //        //if (_promoSerialized == System.Windows.Forms.DialogResult.Yes)
            //        //{
            //        //    _isNewPromotionProcess = true;
            //        //    CheckSerializedPriceLevelAndLoadSerials(true);
            //        //    return true;
            //        //}
            //        //else
            //        //{
            //        //    _isNewPromotionProcess = false;
            //        //    _PriceSerialRefPromo = null;
            //        //}
            //    }
            //    //else if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0 && string.IsNullOrEmpty(txtSerialNo.Text))
            //    //{

            //    //    DialogResult _promoSerialized = new System.Windows.Forms.DialogResult();
            //    //    using (new CenterWinDialog(this)) { _promoSerialized = MessageBox.Show("This item is having promotional serialized price.Do you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question); }
            //    //    if (_promoSerialized == System.Windows.Forms.DialogResult.Yes)
            //    //    {
            //    //        _isNewPromotionProcess = true;
            //    //        CheckSerializedPriceLevelAndLoadSerials(true);
            //    //        return true;
            //    //    }
            //    //    else
            //    //    {
            //    //        _isNewPromotionProcess = false;
            //    //        _PriceSerialRefPromo = null;
            //    //    }
            //    //}
            //}
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
                        _PriceDetailRefPromo = _PriceDetailRefPromo.Where(x => x.Sapd_customer_cd == "").ToList();
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
                    return false;
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
             _list.ForEach(x => x.Sapd_itm_price = CheckSubItemTax(x.Sapd_itm_cd) * x.Sapd_itm_price);

            var _normal = _list.Where(x => x.Sapd_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sapd_price_type != 0).ToList();

            gvNormalPrice.DataSource = _normal;
            gvPromotionPrice.DataSource = _promotion;

            gvNormalPrice.DataBind();
            gvPromotionPrice.DataBind();
           // gvNormalPrice.DataBind();
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
            isJS_END = false;

            if (_isCompleteCode == null)
            {
                _isCompleteCode = false;
            }
            txtDisRate.Text = "0";
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
                if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized) { }
                    //if (CheckSerializedPriceLevelAndLoadSerials(true))
                    //{
                    //    _IsTerminate = true;
                    //    CalculateItem();
                    //    return _IsTerminate;
                    //}
            }
            if (IsGiftVoucher(_itemdetail.Mi_itm_tp)) return true;
            GeneralDiscount_new = CHNLSVC.Sales.GetDocPriceDetails(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbBook.Text.Trim(), cmbLevel.Text.Trim(), cmbInvType.Text);
            if (IsVirtual(_itemdetail.Mi_itm_tp) && _isCompleteCode == false)
            {
                txtUnitPrice.ReadOnly = false;
                txtDisRate.ReadOnly = false;
                txtDisAmt.ReadOnly = false;
                txtUnitAmt.ReadOnly = true;
                txtTaxAmt.ReadOnly = true;
                txtLineTotAmt.ReadOnly = true;
                return true;
            }

            else if (GeneralDiscount_new.SADD_IS_EDIT == 1)
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
           // _priceDetailRef = CHNLSVC.Sales.GetPriceForQuo(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtdate.Text), Convert.ToDateTime(txtValidTo.Text));

            if (_priceDetailRef.Count <= 0)
            {
                if (!_isCompleteCode)
                {
                    //divalert.Visible = true;
                    DisplayMessage("There is no assigned normal price for the selected item under the selected level");
                    SetDecimalTextBoxForZero(true, false, true);
                    _IsTerminate = true;
                    return _IsTerminate;
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
                        DisplayMessage("Price has been suspended. Please contact IT dept !!!");
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
                _isMinus = _itemdetail.Mi_anal4;
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
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes:
                    {
                        paramsText.Append(Session["_SerialSearchType"].ToString() + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + lblItem.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + lblItem.Text.Trim() + seperator);
                        break;
                    }
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
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
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
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
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
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesOrderNew:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        paramsText.Append("ADVAN" + seperator + Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerQuo:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
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

      
        private void SetGridViewAutoColumnGenerate()
        {
            gvInvoiceItem.AutoGenerateColumns = false;
           
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
                 
                    txtExecutive.Text = _MasterProfitCenter.Mpc_man;
                    _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCustomer.Text, string.Empty, string.Empty, "C");
                }
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
                DataTable dtcusdata = CHNLSVC.Sales.SearchCustomer(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
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
                        cmbTitle.SelectedValue = item[2].ToString();

                        txtdelcuscode.Text = txtCustomer.Text.Trim();
                        txtdelname.Text = item[3].ToString();
                        txtdelad1.Text = item[4].ToString();
                        txtdelad2.Text = item[5].ToString();
                        
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
                    txtMobile.Text = _masterBusinessCompany.Mbe_mob;
                    txtNIC.Text = _masterBusinessCompany.Mbe_nic;
                    txtDNic.Text = _masterBusinessCompany.Mbe_nic;
                    txtDMob.Text = _masterBusinessCompany.Mbe_mob;
                    txtDFax.Text = _masterBusinessCompany.Mbe_fax;
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

                    txtDNic.Text = _hdr.Sah_Nic;
                   // txtDMob.Text = _hdr.;
                   // txtDFax.Text = _masterBusinessCompany.Mbe_fax;
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

      
        private void ViewCustomerAccountDetail(string _customer)
        {
            try
            {
                if (string.IsNullOrEmpty(_customer.Trim())) return;
                if (_customer != "CASH")
                {
                    CustomerAccountRef _account = CHNLSVC.Sales.GetCustomerAccount(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                   
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
                    if (_masterBusinessCompany.Mbe_act == false)
                    {
                        DisplayMessage("This customer already inactive. Please contact IT dept");
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
                    }

                    DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                    if (_table != null && _table.Rows.Count > 0)
                    {
                        var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                        if (_isAvailable == null || _isAvailable.Count <= 0)
                        {
                            DisplayMessage("Customer is not allow for enter transaction under selected invoice type");
                            ClearCustomer(true);
                            txtCustomer.Focus();
                            return;
                        }
                    }
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
                    GroupBussinessEntity _grupProf = GetbyCustCDGrup(txtCustomer.Text.Trim().ToUpper());
                    if (_grupProf.Mbg_cd != null && _grupProf.Mbg_act == true)
                    {
                        SetCustomerAndDeliveryDetailsGroup(_grupProf);
                        _isGroup = true;

                        DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                        if (_table != null && _table.Rows.Count > 0)
                        {
                            var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                            if (_isAvailable == null || _isAvailable.Count <= 0)
                            {
                                DisplayMessage("Customer is not allow for enter transaction under selected invoice type");
                                ClearCustomer(true);
                                txtCustomer.Focus();
                                return;
                            }
                        }
                        //else if (cmbInvType.Text != "CS")
                        //{
                        //    DisplayMessage("Selected Customer is not allow for enter transaction under selected invoice type");
                        //    ClearCustomer(true);
                        //    txtCustomer.Focus();
                        //    return;
                        //}
                    }
                    else
                    {
                        _isGroup = false;
                        DisplayMessage("Please select the valid customer");
                        ClearCustomer(true);
                        txtCustomer.Focus();
                        return;
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

                        DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                        if (_table != null && _table.Rows.Count > 0)
                        {
                            var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                            if (_isAvailable == null || _isAvailable.Count <= 0)
                            {
                                DisplayMessage("Customer is not allow for enter transaction under selected invoice type");
                                ClearCustomer(true);
                                txtCustomer.Focus();
                                return;
                            }
                        }
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
            DateTime _validTo = DateTime.Now.Date;
            if (_masterComp.MC_TAX_CALC_MTD == "1") _isStrucBaseTax = true;
            _quoValidPeriod = Convert.ToInt16(_masterComp.Mc_anal4);
            _validTo = Convert.ToDateTime(txtdate.Text).AddDays(_quoValidPeriod).Date;
            txtValidTo.Text = _validTo.ToString("dd/MMM/yyyy");

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
                SOConfirm = 0;
                _isQuoBase = 0;
                Session["_isExtentedPage"] = false;
                Session["_itemSerializedStatus"] = "";
              
                Session["_isOrderbase"] = false;
                btnSave.Enabled = true;
                btnSave.OnClientClick = "ConfirmPlaceOrder();";
                btnSave.CssClass = "buttonUndocolor";

                gvInvoiceItem.Enabled = true;
               

                setGriDel_enables(true);

                _stopit = false;
                Session["oBBNewItems"] = null;
                hdfShowCustomer.Value = null;
                Session["ucc"] = null;
                lblBackDateInfor.Text = string.Empty;
                txtdocrefno.Text = string.Empty;
                txtManualRefNo.Text = string.Empty;
                txtInvoiceNo.Text = string.Empty;
               
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
            
                chkTaxPayable.Checked = false;
                lblSVatStatus.Text = string.Empty;
                lblVatExemptStatus.Text = string.Empty;
              
                txtExecutive.Text = string.Empty;

                txtItem.Text = string.Empty;
                txtQty.Text = "1";
                txtUnitPrice.Text = "0.00";
                txtUnitAmt.Text = "0.00";
                txtDisRate.Text = "0.00";
                txtDisAmt.Text = "0.00";
                txtTaxAmt.Text = "0.00";
                txtLineTotAmt.Text = "0.00";

                lblItemDescription.Text = string.Empty;
                lblItemModel.Text = string.Empty;
                lblItemBrand.Text = string.Empty;
                lblItemSerialStatus.Text = string.Empty;

                cmbTitle.SelectedItem.Text = "MR.";
                txtCusName.Text = "CASH";
                txtAddress1.Text = "N/A";

                txtCustomer.Enabled = true;
                txtCustomer.ReadOnly = false;

              
                // btnSearchDelLocation.Enabled = true;

                chkDeliverNow.Enabled = true;
                chkDeliverNow.Checked = false;
             

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
             

                //grvwarehouseitms.DataSource = null;
                //grvwarehouseitms.DataBind();

                gvInvoiceItem.DataSource = new int[] { };
                gvInvoiceItem.DataBind();


                gvNormalPrice.DataSource = new int[] { };
                gvNormalPrice.DataBind();

                gvPromotionPrice.DataSource = new int[] { };
                gvPromotionPrice.DataBind();

                gvPromotionItem.DataSource = new int[] { };
                gvPromotionItem.DataBind();

                gvPromotionSerial.DataSource = new int[] { };
                gvPromotionSerial.DataBind();

               

                DateTime orddate = DateTime.Now;
                txtdate.Text = orddate.ToString("dd/MMM/yyyy");
                txtValidTo.Text = orddate.AddDays(14).ToString("dd/MMM/yyyy");
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
             



               
                _invoiceItemList = new List<QoutationDetails>();
                InvoiceSerialList = new List<InvoiceSerial>();


                _selectedItemList = new List<ReptPickSerials>();

              
                txtexcutive.Text = string.Empty;
              

             

                loadItemStatus();



                pnlDeliverBOdy.Enabled = true;

                
                mpSavePO.Hide();
              


                btnSave.Enabled = true;
                txtItem.Enabled = true;
               
                lbtnadditems.Enabled = true;

                btnSave.Enabled = true;
                btnSave.OnClientClick = "ConfirmPlaceOrder();";
                btnSave.CssClass = "buttonUndocolor";

                gvInvoiceItem.Enabled = true;
              

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
                dt.Rows.Add(gridline + 1, txtItem.Text.Trim(), lblItemDescription.Text.Trim(), _statustxt, txtQty.Text.Trim(), txtUnitPrice.Text.Trim(), txtUnitAmt.Text.Trim(), txtDisRate.Text.Trim(), txtDisAmt.Text.Trim(), txtTaxAmt.Text.Trim(), txtLineTotAmt.Text.Trim(), cmbBook.SelectedValue, cmbLevel.SelectedValue, _seqnoforrequest);
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
                        DataTable _table = CHNLSVC.Sales.GetCustomerAllowInvoiceType(Session["UserCompanyCode"].ToString(), txtCustomer.Text.Trim());
                        if (_table != null && _table.Rows.Count > 0)
                        {
                            var _isAvailable = _table.AsEnumerable().Where(x => x.Field<string>("mbsa_sa_tp") == cmbInvType.Text.Trim()).ToList();
                            if (_isAvailable == null || _isAvailable.Count <= 0)
                            {
                                DisplayMessage("Customer is not allow for enter transaction under selected order type");
                                ClearCustomer(true);
                                txtCustomer.Focus();
                                return;
                            }
                        }
                        else if (cmbInvType.Text != "CS")
                        {
                            ClearCustomer(true);
                            DisplayMessage("Selected Customer is not allow for enter transaction under selected order type");
                            txtCustomer.Focus();
                            return;
                        }
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
                lblGrndTotalAmount.Text = _invoiceItemList.Sum(x => x.Qd_tot_amt).ToString("N2");
            else
                lblGrndTotalAmount.Text = (Convert.ToString("0.00"));
        }

        private void SetColumnForPriceDetailNPromotion(bool _isSerializedPriceLevel)
        {
            if (_isSerializedPriceLevel)
            {
                // NorPrice_Select.Visible = true;

                //    NorPrice_Serial.DataPropertyName = "sars_ser_no";
                //    NorPrice_Serial.Visible = true;
                //    NorPrice_Item.DataPropertyName = "Sars_itm_cd";
                //    NorPrice_Item.Visible = true;
                //    NorPrice_UnitPrice.DataPropertyName = "sars_itm_price";
                //    NorPrice_UnitPrice.Visible = true;
                //    NorPrice_Circuler.DataPropertyName = "sars_circular_no";
                //    NorPrice_PriceType.DataPropertyName = "sars_price_type";
                //    NorPrice_PriceTypeDescription.DataPropertyName = "sars_price_type_desc";
                //    NorPrice_ValidTill.DataPropertyName = "sars_val_to";
                //    NorPrice_ValidTill.Visible = true;
                //    NorPrice_Pb_Seq.DataPropertyName = "sars_pb_seq";
                //    NorPrice_PbLineSeq.DataPropertyName = "1";
                //    NorPrice_PromotionCD.DataPropertyName = "sars_promo_cd";
                //    NorPrice_IsFixQty.DataPropertyName = "sars_is_fix_qty";
                //    NorPrice_BkpUPrice.DataPropertyName = "sars_cre_by";
                //    NorPrice_WarrantyRmk.DataPropertyName = "sars_warr_remarks";
                //    NorPrice_Book.DataPropertyName = "sars_pbook";
                //    NorPrice_Level.DataPropertyName = "sars_price_lvl";

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
               
                txtdelcuscode.Text = txtCustomer.Text.ToUpper().Trim();
                txtdelname.Text = txtCusName.Text.ToUpper().Trim();
                txtdelad1.Text = txtAddress1.Text.ToUpper().Trim();
                txtdelad2.Text = txtAddress2.Text.ToUpper().Trim();
                txtDMob.Text = txtMobile.Text.Trim();
                txtDNic.Text = txtNIC.Text.Trim();

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
                    DisplayMessage(_item + " item already blocked by the Costing Dept.", 1);
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



        private QoutationDetails AssignDataToObject(bool _isPromotion, MasterItem _item, string _originalItem)
        {

            //String.Format("{0:0,0.0}", 12345.67); 
            QoutationDetails _tempItem = new QoutationDetails();
            _tempItem.Qd_amt = Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text);
            _tempItem.Qd_cbatch_line = 0;
            _tempItem.Qd_cdoc_no = Convert.ToString(SSPRomotionType);
            _tempItem.Qd_citm_line = Convert.ToInt32(SSCombineLine);
            _tempItem.Qd_cost_amt = 0;
            _tempItem.Qd_dis_amt = Convert.ToDecimal(txtDisAmt.Text);
            _tempItem.Qd_dit_rt = Convert.ToDecimal(txtDisRate.Text);
            _tempItem.Qd_frm_qty = Convert.ToDecimal(txtQty.Text);
            if (_item.Mi_anal4 == true)
            {
                _tempItem.Qd_issue_qty = Convert.ToDecimal(txtQty.Text);
            }
            else
            {
                _tempItem.Qd_issue_qty = 0;
            }
            _tempItem.Qd_itm_cd = txtItem.Text;
            _tempItem.Qd_itm_desc = _item.Mi_longdesc;
            _tempItem.Qd_itm_stus = cmbStatus.Text;
            _tempItem.Mi_model = _item.Mi_model;
            _tempItem.Qd_itm_tax = Convert.ToDecimal(txtTaxAmt.Text);
            _tempItem.Qd_line_no = _lineNo;
            _tempItem.Qd_nitm_cd = null;
            _tempItem.Qd_nitm_desc = null;
            _tempItem.Qd_no = "";
            _tempItem.Qd_pb_lvl = cmbLevel.Text;
            _tempItem.Qd_pb_price = Convert.ToDecimal(SSPriceBookPrice);
            _tempItem.Qd_pb_seq = Convert.ToInt32(SSPriceBookSequance);
            _tempItem.Qd_pbook = cmbBook.Text;
            _tempItem.Qd_quo_tp = "R";
            _tempItem.Qd_res_no = null;
            _tempItem.Qd_res_qty = 0;
            _tempItem.Qd_resbal_qty = 0;
            _tempItem.Qd_resitm_cd = txtItem.Text;
            _tempItem.Qd_resline_no = 0;
            _tempItem.Qd_resreq_no = null;
            _tempItem.Qd_seq_no = 0;
            _tempItem.Qd_to_qty = Convert.ToDecimal(txtQty.Text);
            _tempItem.Qd_tot_amt = Convert.ToDecimal(txtLineTotAmt.Text);
            _tempItem.Qd_unit_price = Convert.ToDecimal(txtUnitPrice.Text);

            string _NormalPb = "";
            string _NormalLvl = "";

            MasterCompany _mastercompany = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

            _NormalPb = _mastercompany.Mc_anal7;
            _NormalLvl = _mastercompany.Mc_anal8;

            if (Convert.ToDecimal(txtUnitPrice.Text) > 0)
            {
                List<PriceDetailRef> _NormalPrice = new List<PriceDetailRef>();
                _NormalPrice = CHNLSVC.Sales.GetPriceForQuo(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, _NormalPb, _NormalLvl, txtCustomer.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(txtdate.Text), Convert.ToDateTime(txtValidTo.Text));

                if (_NormalPrice.Count <= 0)
                {

                    _tempItem.Qd_unit_cost = 0;

                }
                else
                {


                    List<PriceDetailRef> _new = _NormalPrice;
                    _NormalPrice = new List<PriceDetailRef>();
                    var _p = _new.Where(x => x.Sapd_price_type == 0 || x.Sapd_price_type == 4).ToList();
                    if (_p != null)
                        if (_p.Count > 0)
                            _NormalPrice.Add(_p[0]);



                    if (_NormalPrice != null && _NormalPrice.Count > 0)
                    {
                        var _isSuspend = _NormalPrice.Where(x => x.Sapd_price_stus == "S").Count();
                        if (_isSuspend > 0)
                        {
                            _tempItem.Qd_unit_cost = 0;
                        }
                        else
                        {
                            foreach (PriceDetailRef _tmp in _NormalPrice)
                            {
                                PriceBookLevelRef _LevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), _NormalPb, _NormalLvl);
                                decimal _vatPortion = 0;
                                _vatPortion = FigureRoundUp(TaxCalculationForNorPrice(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _LevelRef, Convert.ToDecimal(_tmp.Sapd_itm_price), 0, true), true);
                                _vatPortion = _vatPortion / Convert.ToDecimal(txtQty.Text);
                                _tempItem.Qd_unit_cost = FigureRoundUp(_tmp.Sapd_itm_price + _vatPortion, true);
                            }
                        }
                    }

                }
            }
            else
            {
                _tempItem.Qd_unit_cost = 0;
            }
            _tempItem.Qd_uom = _item.Mi_itm_uom;
            _tempItem.Qd_warr_rmk = WarrantyRemarks;
            _tempItem.Qd_warr_pd = WarrantyPeriod;

            //kapila 11/3/2016
            _tempItem.Qd_quo_base = _isQuoBase;
            if (_isQuoBase == 1)
                _isSelQuoBaseLevel = 1;



            //_tempItem.Sad_alt_itm_cd = "";
            //_tempItem.Sad_alt_itm_desc = "";
            //_tempItem.Sad_comm_amt = 0;
            //_tempItem.Sad_disc_amt = Convert.ToDecimal(txtDisAmt.Text);
            //_tempItem.Sad_disc_rt = Convert.ToDecimal(txtDisRate.Text);
            //_tempItem.Sad_do_qty = 0;
            //_tempItem.Sad_inv_no = "";
            //_tempItem.Sad_is_promo = _isPromotion;
            //_tempItem.Qd_itm_cd = txtItem.Text;
            //_tempItem.Sad_itm_line = _lineNo;
            //_tempItem.Sad_itm_seq = Convert.ToInt32(SSPriceBookItemSequance);
            //_tempItem.Sad_itm_stus = cmbStatus.Text;
            //_tempItem.Sad_itm_tax_amt = Convert.ToDecimal(txtTaxAmt.Text);
            //_tempItem.Sad_itm_tp = _item.Mi_itm_tp;
            //_tempItem.Sad_job_no = "";
            //_tempItem.Sad_merge_itm = "";
            //_tempItem.Sad_pb_lvl = cmbLevel.Text;
            //_tempItem.Sad_pb_price = Convert.ToDecimal(SSPriceBookPrice);
            //_tempItem.Sad_pbook = cmbBook.Text;
            //_tempItem.Sad_print_stus = false;
            //_tempItem.Sad_promo_cd = SSPromotionCode;
            //_tempItem.Qd_frm_qty = Convert.ToDecimal(txtQty.Text);
            //_tempItem.Sad_res_line_no = 0;
            //_tempItem.Sad_res_no = "";
            //_tempItem.Sad_seq = Convert.ToInt32(SSPriceBookSequance);
            //_tempItem.Sad_seq_no = 0;
            //_tempItem.Sad_srn_qty = 0;
            //_tempItem.Sad_tot_amt = Convert.ToDecimal(txtLineTotAmt.Text);
            //_tempItem.Sad_unit_amt = Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text);
            //_tempItem.Sad_unit_rt = Convert.ToDecimal(txtUnitPrice.Text);
            //_tempItem.Sad_uom = "";
            //_tempItem.Sad_warr_based = false;
            //_tempItem.Mi_longdesc = _item.Mi_longdesc;
            //_tempItem.Mi_itm_tp = _item.Mi_itm_tp;
            //_tempItem.Sad_job_line = Convert.ToInt32(SSCombineLine);
            //_tempItem.Sad_warr_period = WarrantyPeriod;
            //_tempItem.Sad_warr_remarks = WarrantyRemarks;
            //_tempItem.Sad_sim_itm_cd = _originalItem;
            //_tempItem.Sad_merge_itm = Convert.ToString(SSPRomotionType);

            return _tempItem;
        }
        private decimal TaxCalculationForNorPrice(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, bool _isTaxfaction)
        {
            try
            {
                if (_priceBookLevelRef != null)
                    if (_priceBookLevelRef.Sapl_vat_calc)
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString()); else _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, string.Empty);
                        var _Tax = from _itm in _taxs
                                   select _itm;
                        foreach (MasterItemTax _one in _Tax)
                        {
                            //if (lblVatExemptStatus.Text != "Available")
                            //{
                            //  if (_isTaxfaction == false) _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate; else _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                            if (_isTaxfaction == false)
                                _pbUnitPrice = _pbUnitPrice * _one.Mict_tax_rate;
                            else
                                //if (chkTaxPayable.Checked == true)
                                //{
                                //    _discount = _pbUnitPrice * _qty * Convert.ToDecimal(txtDisRate.Text) / 100;
                                //    _pbUnitPrice = ((_pbUnitPrice - _discount / _qty) * _one.Mict_tax_rate / 100) * _qty;
                                //}
                                //else
                                _pbUnitPrice = (_pbUnitPrice * _one.Mict_tax_rate / 100) * _qty;

                            //}
                            //else
                            //{
                            //    if (_isTaxfaction) _pbUnitPrice = 0;
                            //}
                        }
                    }
                    else
                        if (_isTaxfaction) _pbUnitPrice = 0;


                return _pbUnitPrice;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4);
               // MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                return 0;
            }
           
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
            gvInvoiceItem.DataSource = new List<QoutationDetails>();
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
                foreach (QoutationDetails item in _invoiceItemList)
                {
                    MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == item.Qd_itm_stus);
                    if (oStatus != null)
                    {
                        item.Mi_statusDes = oStatus.Mis_desc;
                    }
                    else
                    {
                        item.Mi_statusDes = item.Qd_itm_stus;
                    }
                }

                gvInvoiceItem.DataSource = new List<QoutationDetails>();
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

       
        private bool CheckItemWarranty(string _item, string _status)
        {
            bool _isNoWarranty = false;
            List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
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
            _invoiceItemList = new List<QoutationDetails>();
            _invoiceItemListWithDiscount = new List<QoutationDetails>();
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

                //_lst.RemoveAll(x => x.Tus_ser_1 == txtSerialNo.Text);

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

                if (_tempPriceCombinItem != null && _tempPriceCombinItem.Count > 0)
                {
                    for (int i = 0; i < gvPromotionItem.Rows.Count; i++)
                    {
                        GridViewRow row = gvPromotionItem.Rows[i];
                        Label lblsapc_itm_cd = (Label)row.FindControl("lblsapc_itm_cd");
                        DropDownList PromItm_Status = (DropDownList)row.FindControl("PromItm_Status");

                        _tempPriceCombinItem.Find(x => x.Sapc_itm_cd == lblsapc_itm_cd.Text.Trim()).Status = PromItm_Status.SelectedValue.ToString().Trim();

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
                                        if (IsPriceLevelAllowDoAnyStatus) _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == _item).ToList().Select(x => x.Qd_frm_qty).Sum(); else _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == _item && x.Qd_itm_stus == _status).ToList().Select(x => x.Qd_frm_qty).Sum();
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
                                        if (IsPriceLevelAllowDoAnyStatus) _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == _item).ToList().Select(x => x.Qd_frm_qty).Sum(); else _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == _item && x.Qd_itm_stus == _status).ToList().Select(x => x.Qd_frm_qty).Sum();
                                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                        List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _status);
                                        if (_inventoryLocation != null)
                                            if (_inventoryLocation.Count > 0)
                                            {
                                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                                if (Convert.ToDecimal(_qty) > _invBal)
                                                {
                                                    DisplayMessageJS(_item + " item quantity exceeding its inventory balance Inventory balance" + FormatToQty(Convert.ToString(_invBal)));
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                DisplayMessageJS(_item + " item quantity exceeding its inventory balance Inventory balance  " + FormatToQty(Convert.ToString("0")));
                                                return;
                                            }
                                        else
                                        {
                                            DisplayMessageJS(_item + " item quantity exceeding its inventory balance Inventory balance  " + FormatToQty(Convert.ToString("0")));
                                            return;
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
         private void LoadTaxDetail(MasterBusinessEntity _entity)
        {
            lblSVatStatus.Text = _entity.Mbe_is_svat ? "Available" : "None";
            lblVatExemptStatus.Text = _entity.Mbe_tax_ex ? "Available" : "None";
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

                load_save_Quotation();
          
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
                    DisplayMessage("Error occurred while processing.");
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
            IsDlvSaleCancelAllowUser = false;
            lbtncancel.Enabled = false;
            string _masterLocation = (string.IsNullOrEmpty(Session["UserDefLoca"].ToString())) ? Session["UserDefProf"].ToString() : Session["UserDefLoca"].ToString();
            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10002))
            {
                IsFwdSaleCancelAllowUser = true;
                lbtncancel.Enabled = true;
            }
            else
            {
                IsFwdSaleCancelAllowUser = false;
                lbtncancel.Enabled = false;
            }
            if (CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10042))
            {
                IsFwdSaleCancelAllowUser = true; IsDlvSaleCancelAllowUser = true;
                lbtncancel.Enabled = true;
            }
            else
            {
                if (!IsFwdSaleCancelAllowUser)
                {
                    IsDlvSaleCancelAllowUser = false;
                    lbtncancel.Enabled = false;
                }
            }
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
            txtExecutive.Text = _hdr.Sah_sales_ex_cd;
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
          
            txtexcutive.Text = _code;
            lblSalesEx.Text = _name;
         

            txtManualRefNo.Text = _hdr.Sah_man_ref;
           // chkTaxPayable.Checked = _hdr.Sah_tax_inv ? true : false;
            txtManualRefNo.Text = _hdr.Sah_man_ref;
            txtdocrefno.Text = _hdr.Sah_ref_doc;
           

            //Load inter company customer details 2016-02-03
            if (_masterBusinessCompany.Mbe_cd == null && !string.IsNullOrEmpty(_hdr.Sah_anal_4))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetActiveBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), _hdr.Sah_cus_cd, string.Empty, string.Empty, "S");
                SetCustomerAndDeliveryDetails(true, _hdr);
            }


            txtGroup.Text = _hdr.Sah_grup_cd;

            

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
                CheckNewDiscountRate_New();
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

      
       
        protected void txtresno_TextChanged(object sender, EventArgs e)
        {

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

  

        protected void txtADVRNumber_TextChanged(object sender, EventArgs e)
        {
            mpAdavce.Show();
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


          
            txtItem.Text = string.Empty;
            txtQty.Text = "1";
            txtUnitPrice.Text = "0.00";
            txtUnitAmt.Text = "0.00";
            txtDisRate.Text = "0.00";
            txtDisAmt.Text = "0.00";
            txtTaxAmt.Text = "0.00";
            txtLineTotAmt.Text = "0.00";
           
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

              

                lbtncode.CssClass = "buttonUndocolor";
                lbtncode.Attributes.Add("onclick", "lbtncode_Click");

                btnSearch_NIC.CssClass = "buttonUndocolor";
                btnSearch_NIC.Attributes.Add("onclick", "btnSearch_NIC_Click");

                btnSearch_Mobile.CssClass = "buttonUndocolor";
                btnSearch_Mobile.Attributes.Add("onclick", "btnSearch_Mobile_Click");

                btnSearch_Loyalty.CssClass = "buttonUndocolor";
                btnSearch_Loyalty.Attributes.Add("onclick", "btnSearch_Loyalty_Click");

                
                
               

                btnSearch_Item.CssClass = "buttonUndocolor";
                btnSearch_Item.Attributes.Add("onclick", "btnSearch_Item_Click");

             

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

               

                lbtncode.CssClass = "buttoncolor";
                lbtncode.Attributes.Add("onclick", "return false;");

                btnSearch_NIC.CssClass = "buttoncolor";
                btnSearch_NIC.Attributes.Add("onclick", "return false;");

                btnSearch_Mobile.CssClass = "buttoncolor";
                btnSearch_Mobile.Attributes.Add("onclick", "return false;");

                btnSearch_Loyalty.CssClass = "buttoncolor";
                btnSearch_Loyalty.Attributes.Add("onclick", "return false;");

             
               

                btnSearch_Item.CssClass = "buttoncolor";
                btnSearch_Item.Attributes.Add("onclick", "return false;");

          

           
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
               // if (!_isWriteToTemporaryTable)
                //    CommonOutScan_AddSerialClick(_selectedItemList);

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
                _reptPickSerial_.Tus_bin = BaseCls.GlbDefaultBin;
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

      
        #endregion

     

   

        private void loadItemStatus()
        {
            oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
        }

        private List<QoutationDetails> setItemDescriptions(List<QoutationDetails> itemList)
        {
            if (oMasterItemStatuss != null && oMasterItemStatuss.Count > 0)
            {
                foreach (QoutationDetails item in itemList)
                {
                    MasterItemStatus oStatus = oMasterItemStatuss.Find(x => x.Mis_cd == item.Qd_itm_stus);
                    if (oStatus != null)
                    {
                        item.Mi_statusDes = oStatus.Mis_desc;
                    }
                    else
                    {
                        item.Mi_statusDes = item.Qd_itm_stus;
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
            _invoiceItemList = new List<QoutationDetails>();
            _invoiceItemListWithDiscount = new List<QoutationDetails>();
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
          

            txtDisRate.Text = "0";
            txtDisAmt.Text = "0";
            txtTaxAmt.Text = "0";
            txtLineTotAmt.Text = "0";

            txtdocrefno.Focus();
            Session["ucc"] = null;
            hdfShowCustomer.Value = null;

           
            txtexcutive.Text = string.Empty;
         

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
            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
            DataTable result = CHNLSVC.Sales.SearchSalesOrder(SearchParams, null, null);
            grdResultD.DataSource = result;
            grdResultD.DataBind();
            lblvalue.Text = "SalesOrderNew";
            BindUCtrlDDLData2(result);
            ViewState["SEARCH"] = result;
            UserDPopoup.Show();
            Session["DPopup"] = "DPopup";
            txtSearchbyword.Focus();
        }

      

        protected void chekBasedOnSalesOrder_CheckedChanged(object sender, EventArgs e)
        {

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





        //protected void lbtnapprove_Click(object sender, EventArgs e)
        //{
        //    if (txtapprove.Value == "Yes")
        //    {
        //        try
        //        {
        //            if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16011))
        //            {
        //                string msg = "You dont have permission to approve .Permission code : 16011";
        //                DisplayMessage(msg, 1);
        //                return;
        //            }
        //            string ordstatus = (string)Session["STATUS"];
        //            if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
        //            {
        //                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('Please select an order !!!')", true);

                       
        //                return;
        //            }
        //            if (ordstatus == "A")
        //            {
        //                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyWarningToast('This order is already approved !!!')", true);

        //                return;
        //            }
        //            _userid = (string)Session["UserID"];
        //            SalesOrderHeader _SalesOrder = new SalesOrderHeader();
        //            _SalesOrder.SOH_STUS = "A";
        //            _SalesOrder.SOH_SO_NO = txtInvoiceNo.Text.Trim();
        //            _SalesOrder.SOH_COM = Session["UserCompanyCode"].ToString();
        //            _SalesOrder.SOH_PC = Session["UserDefProf"].ToString();
        //            _SalesOrder.SOH_MOD_BY = _userid;
        //            _SalesOrder.SOH_MOD_WHEN = CHNLSVC.Security.GetServerDateTime();

        //            Int32 outputresultapprove = CHNLSVC.Sales.UpdateSOStatus(_SalesOrder);

        //            if (outputresultapprove == 1)
        //            {
        //                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickySuccessToast('Successfully approved !!!')", true);
        //                Clear(); ClearAll();
        //            }
        //            else
        //            {
        //                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
        //        }
        //    }
        //}

        protected void lbtnViewinv_Click(object sender, EventArgs e)
        {
           
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label InvItm_Item = dr.FindControl("InvItm_Item") as Label;
            Label stsatus = dr.FindControl("lblsad_itm_stus") as Label;
           
        }

        protected void lbtnadReq_Click(object sender, EventArgs e)
        {

            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label InvItm_Item = dr.FindControl("InvItm_Item") as Label;
            txtItem.Text = InvItm_Item.Text;
            txtItem_TextChanged(null, null);
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
                DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, "CODE", txtexcutive.Text);
                if (result.Rows.Count == 1)
                {
                    txtexcutive.Text = result.Rows[0][0].ToString();
                    txtexcutive.ToolTip = result.Rows[0][1].ToString();
                    lblSalesEx.Text = result.Rows[0][1].ToString();
                }
                else
                {
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

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
           
            if (txtInvoiceNo.Text.Trim() == null | txtInvoiceNo.Text.Trim() == "")
            {
                DisplayMessage("Please Select Quotation No", 1);
            }
            else
            {
                print(txtInvoiceNo.Text.ToString());
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



        protected void CalendarExtender1_Leave(object sender, EventArgs e)
        {
            Int32 _days = 0;
            _days = (Convert.ToDateTime(txtValidTo.Text).Date - Convert.ToDateTime(txtdate.Text).Date).Days;

            if (_quoValidPeriod < _days)
            {
                string msg = "Cannot exceed define valid days." + _quoValidPeriod + "Customer Quotation";
                DisplayMessage(msg, 1);
                txtValidTo.Text = Convert.ToDateTime(txtdate.Text).AddDays(_quoValidPeriod).Date.ToString();
                txtValidTo.Focus();
                return;
            }
        }
        protected void lbtnSearch_Invoice_Click(object sender, EventArgs e)
        {
            try
            {
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerQuo);
                DataTable _result = CHNLSVC.CommonSearch.GetQuotationAll(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "Quotation";
                ViewState["SEARCH"] = _result;
                SIPopup.Show();
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }

        }

        protected void lbtnadditems_Click(object sender, EventArgs e)
        {

            if (txtLineTotAmt.Text == "0")
            {
                DisplayMessage("You cannot add zero price to the list", 1);
                txtQty.Focus();
                return;
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
            AddItem(SSPromotionCode == "0" || string.IsNullOrEmpty(SSPromotionCode) ? false : true, string.Empty);
            if (_priceDetailRef != null && _priceDetailRef.Count > 0)
            {
                if (_priceDetailRef[0].Sapd_customer_cd == txtCustomer.Text.ToUpper().Trim())
                {
                    txtCustomer.ReadOnly = true;
                    lbtncode.Enabled = false;
                }
            }
            lbtnValidTo.Visible = false;
            txtQty.Text = "1";

        }
        private void AddItem(bool _isPromotion, string _originalItem)
        {
            try
            {


                ReptPickSerials _serLst = null;
                List<ReptPickSerials> _nonserLst = null;
                MasterItem _itm = null;
                _invoiceItemList = Session["_invoiceItemList"] as List<QoutationDetails>;

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
                        if (string.IsNullOrEmpty(txtItem.Text))
                        {
                            DisplayMessage("Please select the item", 1);
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
                                             where _list.Qd_itm_cd == txtItem.Text.ToUpper() && _list.Qd_itm_stus == cmbStatus.SelectedValue.ToString() && _list.Qd_pbook == cmbBook.Text && _list.Qd_pb_lvl == cmbLevel.Text && _list.Qd_unit_price == Convert.ToDecimal(txtUnitPrice.Text) && _list.Qd_dit_rt == Convert.ToDecimal(txtDisRate.Text.Trim())
                                             select _list;

                        if (_duplicateItem.Count() > 0)
                        //Similar item available
                        {
                            _isDuplicateItem0 = true;
                            foreach (var _similerList in _duplicateItem)
                            {
                                _duplicateComLine0 = _similerList.Qd_citm_line;
                                _duplicateItmLine0 = _similerList.Qd_line_no;
                                _similerList.Qd_dis_amt = Convert.ToDecimal(_similerList.Qd_dis_amt) + Convert.ToDecimal(txtDisAmt.Text);
                                _similerList.Qd_itm_tax = Convert.ToDecimal(_similerList.Qd_itm_tax) + Convert.ToDecimal(txtTaxAmt.Text);
                                _similerList.Qd_frm_qty = Convert.ToDecimal(_similerList.Qd_frm_qty) + Convert.ToDecimal(txtQty.Text);
                                _similerList.Qd_to_qty = Convert.ToDecimal(_similerList.Qd_to_qty) + Convert.ToDecimal(txtQty.Text);
                                _similerList.Qd_tot_amt = Convert.ToDecimal(_similerList.Qd_tot_amt) + Convert.ToDecimal(txtLineTotAmt.Text);
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
                //if (!string.IsNullOrEmpty(txtSerialNo.Text))
                //{
                //    if (!string.IsNullOrEmpty(txtItem.Text.Trim()))
                //    {
                //        // Edt0001
                //        if (_itm.Mi_is_ser1 == 1 && (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false && _priceBookLevelRef.Sapl_is_serialized))
                //        {
                //            DisplayMessage("Please select the serial number", 1);
                //            txtSerialNo.Focus();
                //            return;
                //        }
                //    }
                //}

                #region sachith check item balance

                if (chkDeliverNow.Checked && _itm.Mi_itm_tp == "M")
                {
                    List<ReptPickSerials> serial_list = new List<ReptPickSerials>();
                    if (_itm.Mi_is_ser1 == 0)
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Mi_cd, string.Empty, string.Empty).ToList();//.Where(x => x.Tus_itm_stus == cmbStatus.SelectedValue.ToString()).ToList();
                    else if (_itm.Mi_is_ser1 == 1) //serial
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Mi_cd, string.Empty, string.Empty).ToList();//.Where(x => x.Tus_itm_stus == cmbStatus.SelectedValue.ToString()).ToList();
                    else if (_itm.Mi_is_ser1 == -1)
                        serial_list = CHNLSVC.Inventory.Search_serials_for_itemCD(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Mi_cd, string.Empty, string.Empty).ToList();//.Where(x => x.Tus_itm_stus == cmbStatus.SelectedValue.ToString()).ToList();

                    if (IsPriceLevelAllowDoAnyStatus)
                    {
                        serial_list = serial_list.Where(x => x.Tus_itm_stus == cmbStatus.SelectedValue.ToString()).ToList();
                    }

                    if (Convert.ToDecimal(txtQty.Text) > serial_list.Count)
                    {
                       // DisplayMessage("-------- Develop ----------6087", 1);
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
                                            _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == _item).ToList().Select(x => x.Qd_frm_qty).Sum();
                                        else
                                            _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == _item && x.Qd_itm_stus == _status).ToList().Select(x => x.Qd_frm_qty).Sum();

                                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                        List<InventoryLocation> _inventoryLocation = null;
                                        if (IsPriceLevelAllowDoAnyStatus)
                                            _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty);
                                        else
                                            _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _status);
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
                            if (!string.IsNullOrEmpty(_taxNotdefine))
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
                                if (IsPriceLevelAllowDoAnyStatus) _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == _com.ComponentItem.Mi_cd).ToList().Select(x => x.Qd_frm_qty).Sum();
                                else _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == _com.ComponentItem.Mi_cd && x.Qd_itm_stus == cmbStatus.SelectedValue.ToString().Trim()).ToList().Select(x => x.Qd_frm_qty).Sum();
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

                        //    if (_comItem.Count > 1 && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                        //    {//hdnItemCode.value
                        //        ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _mainItem.Trim(), txtSerialNo.Text.Trim());
                        //        if (_pick != null)
                        //            if (!string.IsNullOrEmpty(_pick.Tus_itm_cd))
                        //            {
                        //                var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _pick.Tus_itm_cd && x.Tus_ser_1 == _pick.Tus_ser_1).ToList();
                        //                if (_dup != null)
                        //                    if (_dup.Count <= 0)
                        //                        InventoryCombinItemSerialList.Add(_pick);
                        //            }
                        //        _comItem.ForEach(x => x.Micp_itm_cd = _combineStatus);
                        //        var _listComItem = (from _one in _comItem where _one.ComponentItem.Mi_itm_tp != "M" select new { Mi_cd = _one.ComponentItem.Mi_cd, Mi_longdesc = _one.ComponentItem.Mi_longdesc, Micp_itm_cd = _one.Micp_itm_cd, Micp_qty = _one.Micp_qty, Mi_itm_tp = _one.ComponentItem.Mi_itm_tp }).ToList();
                        //        DisplayMessage("--------- Develop ! 6380 ------------", 1);
                        //        //gvPopComItem.DataSource = _listComItem;
                        //        //pnlInventoryCombineSerialPick.Visible = true;
                        //        //pnlMain.Enabled = false;
                        //        //_isInventoryCombineAdded = false;
                        //        //this.Cursor = Cursors.Default;
                        //        return;
                        //    }
                        //    else if (_comItem.Count == 1 && chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                        //    {//hdnItemCode.Value
                        //        ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _mainItem.Trim(), txtSerialNo.Text.Trim());
                        //        if (_pick != null)
                        //            if (!string.IsNullOrEmpty(_pick.Tus_itm_cd))
                        //            { var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _pick.Tus_itm_cd && x.Tus_ser_1 == _pick.Tus_ser_1).ToList(); if (_dup != null)                                        if (_dup.Count <= 0) InventoryCombinItemSerialList.Add(_pick); }
                        //    }
                        //}
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
                                            //txtSerialNo.Text = _serItm.Tus_ser_1;
                                            //ScanSerialNo = txtSerialNo.Text;
                                            // txtSerialNo.Text = ScanSerialNo;
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
                                            // txtSerialNo.Text = string.Empty;
                                            // txtSerialNo.Text = string.Empty;
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
                                        //txtSerialNo.Text = string.Empty;
                                        //txtSerialNo.Text = string.Empty;
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
                            //xtSerialNo.Text = string.Empty;
                            if (_isCombineAdding == false)
                            {
                                // txtSerialNo.Text = "";
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
                            //if (_itm.Mi_is_ser1 == 1)
                            //{
                            //    if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                            //    {
                            //        DisplayMessage("Please select the serial number", 1);
                            //        txtSerialNo.Focus();
                            //        return;
                            //    }
                            //    _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                            //    if (_serLst == null || string.IsNullOrEmpty(_serLst.Tus_com))
                            //    {
                            //        if (_isAgePriceLevel)
                            //            DisplayMessage("There is no serial available for the selected item in a ageing price level", 1);
                            //        else
                            //            DisplayMessage("There is no serial available for the selected item", 1);
                            //        return;
                            //    }
                            //}
                            if (_itm.Mi_is_ser1 == 0)
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
                            // if (_itm.Mi_is_ser1 == 1)
                            //_serLst = VirtualSerialLine(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text.Trim()), txtSerialNo.Text.Trim())[0];
                            if
                               (_itm.Mi_is_ser1 == 0)
                                _nonserLst = VirtualSerialLine(txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text.Trim()), string.Empty);
                        }
                    }
                    else if ((chkDeliverLater.Checked == false && chkDeliverNow.Checked == false) || IsGiftVoucher(_itm.Mi_itm_tp) || (_isRegistrationMandatory))
                    {
                        //if (_itm.Mi_is_ser1 == 1)
                        //{
                        //    //if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                        //    //{
                        //    //    DisplayMessage("Please select the serial number", 1);
                        //    //    txtSerialNo.Focus(); return;
                        //    //}
                        //    if (!string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                        //    {
                        //        bool _isGiftVoucher = IsGiftVoucher(_itm.Mi_itm_tp);
                        //        if (!_isGiftVoucher)
                        //            _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, txtItem.Text.Trim(), txtSerialNo.Text.Trim());
                        //        else
                        //            _serLst = CHNLSVC.Inventory.GetGiftVoucherDetail(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtItem.Text.Trim(), Convert.ToInt32(_serial2), Convert.ToInt32(txtSerialNo.Text.Trim()), _prefix);

                        //        if (_serLst != null && !string.IsNullOrEmpty(_serLst.Tus_com))
                        //        {
                        //            if (_serLst.Tus_doc_dt >= _serialpickingdate)
                        //            {
                        //                if (_isAgePriceLevel)
                        //                {
                        //                    DisplayMessage("There is no serial available for the selected item in a aging price level", 1);
                        //                    return;
                        //                }
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        //_serLst = new ReptPickSerials();
                        //    }
                        //}
                        if (_itm.Mi_is_ser1 == 0)
                        {
                            if (IsPriceLevelAllowDoAnyStatus == false) _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                            else _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
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
                        DisplayMessage("Please select the valid quantity", 1);
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
                                                if (Math.Round(_lsts.Sapd_itm_price, 0) != Math.Round(pickUPrice, 0))
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
                                                        if (ddUprice > pickUPrice)
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
                    //if (_isCombineAdding == false)
                    //    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory))
                    //    {
                    //        if (_itm.Mi_is_ser1 == 1 && ScanSerialList != null)
                    //        {
                    //            var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == txtItem.Text.ToUpper() && x.Tus_ser_1 == ScanSerialNo).ToList();
                    //            if (_dup != null)
                    //                if (_dup.Count > 0)
                    //                {
                    //                    txtSerialNo.Focus();
                    //                    DisplayMessage(ScanSerialNo + " serial is already picked!", 1);
                    //                    return;
                    //                }
                    //        }

                    //        if (!IsPriceLevelAllowDoAnyStatus)
                    //        {
                    //            if (_serLst != null)
                    //                if (string.IsNullOrEmpty(_serLst.Tus_com))
                    //                {
                    //                    if (_serLst.Tus_itm_stus != cmbStatus.SelectedValue.ToString().Trim())
                    //                    {
                    //                        DisplayMessage(ScanSerialNo + " serial status is not match with the price level status", 1);

                    //                        return;
                    //                    }
                    //                }
                    //        }
                    //    }


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
                                    _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == txtItem.Text.ToUpper().Trim()).ToList().Select(x => x.Qd_frm_qty).Sum();
                                }
                                else
                                {
                                    _pickQty = _invoiceItemList.Where(x => x.Qd_itm_cd == txtItem.Text.ToUpper().Trim() && x.Qd_itm_stus == cmbStatus.SelectedValue.ToString().Trim()).ToList().Select(x => x.Qd_frm_qty).Sum();
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
                                        DisplayMessage(txtItem.Text + " item quantity exceeding its inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal)), 1);
                                        return;
                                    }
                                }
                                else
                                {
                                    //DisplayMessage(txtItem.Text + " item qty exceeding its inventory balance. Inventory balance  0", 1);
                                    DisplayMessageJS(txtItem.Text + " item quantity exceeding its inventory balance. Inventory balance  0");
                                    return;
                                }
                            else
                            {
                                //DisplayMessage(txtItem.Text + " item qty exceeding its inventory balance. Inventory balance  0", 1);
                                DisplayMessageJS(txtItem.Text + " item quantity exceeding its inventory balance. Inventory balance  0");
                                return;
                            }

                            //if (_itm.Mi_is_ser1 == 1 && ScanSerialList != null && ScanSerialList.Count > 0)
                            //{
                            //    var _serDup = (from _lst in ScanSerialList
                            //                   where _lst.Tus_ser_1 == txtSerialNo.Text.Trim() && _lst.Tus_itm_cd == txtItem.Text.ToUpper().Trim()
                            //                   select _lst).ToList();

                            //    if (_serDup != null)
                            //        if (_serDup.Count > 0)
                            //        {
                            //            DisplayMessage("Selected Serial is duplicating", 1);
                            //            return;
                            //        }
                            //}
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
                        //var _duplicateItem = from _list in _invoiceItemList
                        //                     where _list.Qd_itm_cd == txtItem.Text.ToUpper() && _list.Sad_itm_stus == cmbStatus.SelectedValue.ToString() && _list.Sad_pbook == cmbBook.Text && _list.Sad_pb_lvl == cmbLevel.Text && _list.Sad_unit_rt == Convert.ToDecimal(txtUnitPrice.Text) && _list.Sad_disc_rt == Convert.ToDecimal(txtDisRate.Text.Trim())
                        //                     select _list;
                        var _duplicateItem = from _list in _invoiceItemList
                                             where _list.Qd_itm_cd == txtItem.Text.ToUpper()
                                             select _list;
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

                                _isDuplicateItem = true;
                                foreach (var _similerList in _duplicateItem)
                                {
                                    _duplicateComLine = _similerList.Qd_citm_line;
                                    _duplicateItmLine = _similerList.Qd_line_no;
                                    _similerList.Qd_dis_amt = Convert.ToDecimal(_similerList.Qd_dis_amt) + Convert.ToDecimal(txtDisAmt.Text);
                                    _similerList.Qd_itm_tax = Convert.ToDecimal(_similerList.Qd_itm_tax) + Convert.ToDecimal(txtTaxAmt.Text);


                                    //   if (_similerList.Qd_itm_cd == txtItem.Text)
                                    //  {
                                    //      _similerList.Qd_frm_qty = Convert.ToDecimal(txtQty.Text) + _similerList.Qd_frm_qty;
                                    //  }
                                    //  else
                                    //  {
                                    _similerList.Qd_frm_qty = Convert.ToDecimal(txtQty.Text);
                                    // }
                                    _similerList.Qd_unit_price = Convert.ToDecimal(txtUnitPrice.Text);
                                    _similerList.Qd_unit_price = Convert.ToDecimal(txtUnitPrice.Text) * _similerList.Qd_frm_qty;
                                    _similerList.Qd_tot_amt = Convert.ToDecimal(_similerList.Qd_tot_amt) + Convert.ToDecimal(txtLineTotAmt.Text);
                                    _similerList.Qd_pbook = cmbBook.Text;//add 09/apr/2016
                                    _similerList.Qd_pb_lvl = cmbLevel.Text;//add 09/apr/2016
                                }
                            }
                        }
                        else
                        {
                            _isDuplicateItem = false;
                            _lineNo += 1;
                            if (!_isCombineAdding) SSCombineLine += 1;
                            _invoiceItemList.Add(AssignDataToObject(_isPromotion, _itm, _originalItem));
                        }
                    }

                    //Adding Items to grid end here ----------------------------------------------------------------------

                    #endregion Check Inventory Balance if deliver now!

                    #region Adding Serial/Non Serial items

                    ////Scan By Serial ----------start----------------------------------
                    //if ((chkDeliverLater.Checked == false && chkDeliverNow.Checked == false) || _priceBookLevelRef.Sapl_is_serialized || IsGiftVoucher(_itm.Mi_itm_tp) || _isRegistrationMandatory)
                    //{
                    //    if (_isFirstPriceComItem)
                    //        _isCombineAdding = true;
                    //    if (ScanSequanceNo == 0) ScanSequanceNo = -100;
                    //    if (_itm.Mi_is_ser1 == 1)
                    //    {
                    //        if (!string.IsNullOrEmpty(txtSerialNo.Text))
                    //        {
                    //            _serLst.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                    //            _serLst.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine;
                    //            _serLst.Tus_usrseq_no = ScanSequanceNo;
                    //            _serLst.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                    //            _serLst.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty;
                    //            _serLst.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty;
                    //            _serLst.ItemType = _itm.Mi_itm_tp;
                    //            ScanSerialList.Add(_serLst);
                    //        }
                    //    }
                    //    if (_itm.Mi_is_ser1 == 0)
                    //    {
                    //        if (_nonserLst.Count < Convert.ToDecimal(txtQty.Text.Trim()))
                    //        {
                    //            if (_isAgePriceLevel == false)
                    //            {
                    //                DisplayMessage(txtItem.Text + " item quantity exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_nonserLst.Count)), 1);
                    //                var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                    //                foreach (QoutationDetails _one in _partly)
                    //                    DeleteIfPartlyAdded(_one.Sad_job_line, _one.Qd_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Qd_frm_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);
                    //                return;
                    //            }
                    //            else
                    //            {
                    //                if (gvInvoiceItem.Rows.Count > 0)
                    //                {
                    //                    DisplayMessage("This serial can't select under aging price level. Please check the aging status with IT dept.", 1);
                    //                    return;
                    //                }
                    //                var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                    //                foreach (InvoiceItem _one in _partly)
                    //                    DeleteIfPartlyAdded(_one.Sad_job_line, _one.Qd_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Qd_frm_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);
                    //                return;
                    //            }
                    //        }
                    //        _nonserLst.ForEach(x => x.Tus_base_doc_no = Convert.ToString(ScanSequanceNo));
                    //        _nonserLst.ForEach(x => x.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine);
                    //        _nonserLst.ForEach(x => x.Tus_usrseq_no = ScanSequanceNo);
                    //        _nonserLst.ForEach(x => x.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim()));
                    //        _nonserLst.ForEach(x => x.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty);
                    //        _nonserLst.ForEach(x => x.Tus_ser_id = -1);
                    //        _nonserLst.ForEach(x => x.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty);
                    //        _nonserLst.ForEach(x => x.ItemType = _itm.Mi_itm_tp);
                    //        ScanSerialList.AddRange(_nonserLst);
                    //    }
                    //    if (_itm.Mi_is_ser1 == -1)
                    //    {
                    //        //if (_nonserLst.Count < Convert.ToDecimal(txtQty.Text.Trim()))
                    //        //{
                    //        //    if (_isAgePriceLevel == false)
                    //        //    {
                    //        //        this.Cursor = Cursors.Default;
                    //        //        using (new CenterWinDialog(this)) { MessageBox.Show(txtItem.Text + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_nonserLst.Count)), "Inventory Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    //        //        var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                    //        //        foreach (InvoiceItem _one in _partly)
                    //        //            DeleteIfPartlyAdded(_one.Sad_job_line, _one.Qd_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Qd_frm_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);

                    //        //        return;
                    //        //    }
                    //        //    else
                    //        //    {
                    //        //        this.Cursor = Cursors.Default;
                    //        //        if (gvInvoiceItem.Rows.Count > 0) { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("This serial can't select under ageing price level. Please check the ageing status with IT dept.", "Age Price Level", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                    //        //        var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                    //        //        foreach (InvoiceItem _one in _partly)
                    //        //            DeleteIfPartlyAdded(_one.Sad_job_line, _one.Qd_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Qd_frm_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);

                    //        //        return;
                    //        //    }
                    //        //}
                    //        ReptPickSerials _chk = new ReptPickSerials();
                    //        _chk.Tus_base_doc_no = Convert.ToString(ScanSequanceNo);
                    //        _chk.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine;
                    //        _chk.Tus_usrseq_no = ScanSequanceNo;
                    //        _chk.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                    //        _chk.Tus_itm_cd = txtItem.Text.ToUpper().Trim();
                    //        _chk.Tus_itm_stus = cmbStatus.SelectedValue.ToString();
                    //        _chk.Tus_ser_id = 0;
                    //        _chk.Tus_qty = Convert.ToDecimal(txtQty.Text);
                    //        _chk.Tus_bin = BaseCls.GlbDefaultBin;
                    //        _chk.Tus_ser_1 = "N/A";
                    //        _chk.Tus_ser_2 = "N/A";
                    //        _chk.Tus_ser_3 = "N/A";
                    //        _chk.Tus_ser_4 = "N/A";
                    //        _chk.Tus_ser_id = 0;
                    //        _chk.Tus_serial_id = "0";
                    //        _chk.Tus_com = Session["UserCompanyCode"].ToString();
                    //        _chk.Tus_loc = Session["UserDefLoca"].ToString();
                    //        _chk.ItemType = _itm.Mi_itm_tp;
                    //        _chk.Tus_cre_by = Session["UserID"].ToString();
                    //        _chk.Tus_cre_by = Session["UserID"].ToString();
                    //        _chk.Tus_itm_desc = _itm.Mi_shortdesc;
                    //        _chk.Tus_itm_model = _itm.Mi_model;
                    //        _chk.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim());
                    //        ScanSerialList.Add(_chk);

                    //        //_nonserLst.ForEach(x => x.Tus_base_doc_no = Convert.ToString(ScanSequanceNo));
                    //        //_nonserLst.ForEach(x => x.Tus_base_itm_line = _isDuplicateItem == false ? _lineNo : _duplicateItmLine);
                    //        //_nonserLst.ForEach(x => x.Tus_usrseq_no = ScanSequanceNo);
                    //        //_nonserLst.ForEach(x => x.Tus_unit_price = Convert.ToDecimal(txtUnitPrice.Text.Trim()));
                    //        //_nonserLst.ForEach(x => x.Tus_serial_id = _isCombineAdding ? Convert.ToString(SSCombineLine) : string.Empty);
                    //        //_nonserLst.ForEach(x => x.Tus_ser_id = -1);
                    //        //_nonserLst.ForEach(x => x.Tus_new_status = _isCombineAdding == true ? "C" : string.Empty);
                    //        //_nonserLst.ForEach(x => x.ItemType = _itm.Mi_itm_tp);
                    //        //_nonserLst.ForEach(x=>x.Tus_ser_1 = "N/A");
                    //        //_nonserLst.ForEach(x=>x.Tus_ser_2 = "N/A");
                    //        //_nonserLst.ForEach(x=>x.Tus_ser_3 = "N/A");
                    //        //_nonserLst.ForEach(x=>x.Tus_ser_4 = "N/A");
                    //        //_nonserLst.ForEach(x=>x.Tus_ser_id = 0);
                    //        //_nonserLst.ForEach(x=>x.Tus_serial_id = "0");
                    //        // _nonserLst.ForEach(x=>x.Tus_unit_cost = 0);
                    //        // _nonserLst.ForEach(x=>x.Tus_unit_price = 0);

                    //        //ScanSerialList.AddRange(_nonserLst);
                    //    }

                    //    gvPopSerial.DataSource = new List<ReptPickSerials>();
                    //    gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList();
                    //    gvPopSerial.DataSource = setSerialStatusDescriptions(ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList());
                    //    gvPopSerial.DataBind();
                    //    var filenamesList = new BindingList<ReptPickSerials>(ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList());


                    //    if (_isFirstPriceComItem)
                    //    {
                    //        _isCombineAdding = false;
                    //        _isFirstPriceComItem = false;
                    //    }

                    //    if (IsGiftVoucher(_itm.Mi_itm_tp)) _isCombineAdding = true;
                    //}

                    #endregion Adding Serial/Non Serial items

                    bool _isDuplicate = false;
                    //if (InvoiceSerialList != null)
                    //    if (InvoiceSerialList.Count > 0)
                    //    { if (_itm.Mi_is_ser1 == 1) { var _dup = (from _i in InvoiceSerialList where _i.Sap_ser_1 == txtSerialNo.Text.Trim() && _i.Sap_itm_cd == txtItem.Text.ToUpper().Trim() select _i).ToList(); if (_dup != null)                                if (_dup.Count > 0)                                    _isDuplicate = true; } }
                    //if (_isDuplicate == false)
                    //{
                    //    InvoiceSerial _invser = new InvoiceSerial();
                    //    _invser.Sap_del_loc = Session["UserDefLoca"].ToString();
                    //    _invser.Sap_itm_cd = txtItem.Text.ToUpper().Trim();
                    //    _invser.Sap_itm_line = _lineNo;
                    //    _invser.Sap_remarks = string.Empty;
                    //    _invser.Sap_seq_no = Convert.ToInt32(SSPriceBookSequance);
                    //    _invser.Sap_ser_1 = txtSerialNo.Text;
                    //    _invser.Sap_ser_line = _isCombineAdding ? Convert.ToInt32(SSCombineLine) : 0;
                    //    InvoiceSerialList.Add(_invser);
                    //}
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
                                    string _originalItm = _list.Sapc_itm_cd;
                                    string _similerItem = _list.Similer_item;
                                    _combineStatus = _list.Status;
                                    if (!string.IsNullOrEmpty(_similerItem))
                                        txtItem.Text = _similerItem;
                                    else txtItem.Text = _list.Sapc_itm_cd;
                                    // if (_priceBookLevelRef.Sapl_is_serialized) txtSerialNo.Text = _list.Sapc_sub_ser;
                                    LoadItemDetail(txtItem.Text.Trim());
                                    //if (IsGiftVoucher(_itemdetail.Mi_itm_tp))
                                    //{
                                    //    foreach (ReptPickSerials _lists in PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == txtItem.Text.ToUpper().Trim()).ToList())
                                    //    {
                                    //        txtSerialNo.Text = _lists.Tus_ser_1;
                                    //        ScanSerialNo = _lists.Tus_ser_1;
                                    //        string _originalItms = _lists.Tus_session_id;
                                    //        if (string.IsNullOrEmpty(_originalItm))
                                    //        {
                                    //            txtItem.Text = _lists.Tus_itm_cd; _serial2 = _lists.Tus_ser_2;
                                    //            _prefix = _lists.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                    //            cmbStatus.SelectedValue = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_price).Sum();
                                    //            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_qty).Sum();
                                    //            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                    //            txtUnitPrice.Text = UnitPrice.ToString("N2");
                                    //            if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                    //            txtDisRate.Text = FormatToCurrency("0");
                                    //            txtDisAmt.Text = FormatToCurrency("0");
                                    //            txtTaxAmt.Text = FormatToCurrency("0");
                                    //            txtLineTotAmt.Text = FormatToCurrency("0");
                                    //            CalculateItem(); AddItem(_isPromotion, string.Empty);
                                    //        }
                                    //        else
                                    //        {
                                    //            txtItem.Text = _lists.Tus_itm_cd; _serial2 = _lists.Tus_ser_2;
                                    //            _prefix = _lists.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                    //            cmbStatus.SelectedValue = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_price).Sum();
                                    //            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_qty).Sum();
                                    //            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                    //            txtUnitPrice.Text = UnitPrice.ToString("N2");
                                    //            if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                    //            txtDisRate.Text = FormatToCurrency("0");
                                    //            txtDisAmt.Text = FormatToCurrency("0");
                                    //            txtTaxAmt.Text = FormatToCurrency("0");
                                    //            txtLineTotAmt.Text = FormatToCurrency("0");
                                    //            CalculateItem(); AddItem(_isPromotion, _originalItm);
                                    //        }
                                    //        _combineCounter += 1;
                                    //    }
                                    //}
                                    //else
                                    // {
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
                                    // }
                                }
                                //}
                                //else
                                //{
                                //    if (PriceCombinItemSerialList == null || PriceCombinItemSerialList.Count == 0)
                                //        _isSingleItemSerializedInCombine = false;
                                //    foreach (ReptPickSerials _list in PriceCombinItemSerialList)
                                //    {
                                //        txtSerialNo.Text = _list.Tus_ser_1;
                                //        ScanSerialNo = _list.Tus_ser_1;
                                //        string _originalItm = _list.Tus_session_id;
                                //        _combineStatus = _list.Tus_itm_stus;
                                //        if (string.IsNullOrEmpty(_originalItm))
                                //        {
                                //            txtItem.Text = _list.Tus_itm_cd; _serial2 = _list.Tus_ser_2;
                                //            _prefix = _list.Tus_ser_3; LoadItemDetail(txtItem.Text.Trim());
                                //            cmbStatus.SelectedValue = _combineStatus; decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_price).Sum();
                                //            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_qty).Sum();
                                //            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                //            txtUnitPrice.Text = UnitPrice.ToString("N2");
                                //            txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty)));
                                //            txtDisRate.Text = FormatToCurrency("0");
                                //            txtDisAmt.Text = FormatToCurrency("0");
                                //            txtTaxAmt.Text = FormatToCurrency("0");
                                //            txtLineTotAmt.Text = FormatToCurrency("0");
                                //            CalculateItem(); AddItem(_isPromotion, string.Empty);
                                //        }
                                //        else
                                //        {
                                //            txtItem.Text = _list.Tus_itm_cd;
                                //            _serial2 = _list.Tus_ser_2;
                                //            _prefix = _list.Tus_ser_3;
                                //            LoadItemDetail(txtItem.Text.Trim());
                                //            cmbStatus.SelectedValue = _combineStatus;
                                //            decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_price).Sum();
                                //            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_qty).Sum();

                                //            Qty = (Qty == 0) ? 1 : Qty;

                                //            var _Increaseable = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(x => x.Sapc_increse).Distinct().ToList();
                                //            bool _isIncreaseable = false;

                                //            if (_Increaseable.Count == 0)
                                //            {
                                //                _isIncreaseable = false;
                                //            }
                                //            else
                                //            {
                                //                _isIncreaseable = Convert.ToBoolean(_Increaseable[0]);
                                //            }
                                //            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                //            txtUnitPrice.Text = UnitPrice.ToString("N2");
                                //            if (_isIncreaseable)
                                //                txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty)));
                                //            else
                                //                txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                //            txtDisRate.Text = FormatToCurrency("0");
                                //            txtDisAmt.Text = FormatToCurrency("0");
                                //            txtTaxAmt.Text = FormatToCurrency("0");
                                //            txtLineTotAmt.Text = FormatToCurrency("0");
                                //            CalculateItem();
                                //            AddItem(_isPromotion, _originalItm);
                                //        }
                                //        _combineCounter += 1;
                                //    }
                                //    foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                                //    {
                                //        MasterItem _i = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _list.Sapc_itm_cd);
                                //        _combineStatus = _list.Status;
                                //        if (_i.Mi_is_ser1 != 1)
                                //        {
                                //            string _originalItm = _list.Sapc_itm_cd; string _similerItem = _list.Similer_item;
                                //            if (!string.IsNullOrEmpty(_similerItem)) txtItem.Text = _similerItem; else txtItem.Text = _list.Sapc_itm_cd;
                                //            LoadItemDetail(txtItem.Text.Trim());
                                //            cmbStatus.SelectedValue = _combineStatus;
                                //            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
                                //            txtUnitPrice.Text = _list.Sapc_price.ToString("N2");
                                //            if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty)));
                                //            txtDisRate.Text = FormatToCurrency("0");
                                //            txtDisAmt.Text = FormatToCurrency("0");
                                //            txtTaxAmt.Text = FormatToCurrency("0");
                                //            txtLineTotAmt.Text = FormatToCurrency("0");
                                //            CalculateItem(); AddItem(_isPromotion, _originalItm);
                                //            _combineCounter += 1;
                                //        }
                                //    }
                                //}

                                // if (chkDeliverLater.Checked == true || chkDeliverNow.Checked == true)
                                if (_combineCounter == _MainPriceCombinItem.Count)
                                {
                                    _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                                    PriceCombinItemSerialList = new List<ReptPickSerials>();
                                    _isCombineAdding = false;
                                    SSPromotionCode = string.Empty;
                                    ScanSerialNo = string.Empty;
                                    _serial2 = string.Empty;
                                    _prefix = string.Empty;

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
                                //if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory))
                                //{
                                //    if (_isSingleItemSerializedInCombine)
                                //    {
                                //        if (_combineCounter == PriceCombinItemSerialList.Count)
                                //        {
                                //            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                                //            PriceCombinItemSerialList = new List<ReptPickSerials>();
                                //            _isCombineAdding = false;
                                //            SSPromotionCode = string.Empty;
                                //            ScanSerialNo = string.Empty;
                                //            _serial2 = string.Empty;
                                //            _prefix = string.Empty;

                                //            SSCombineLine += 1;
                                //            _combineCounter = 0;
                                //            _isCheckedPriceCombine = false;

                                //            if (_isCombineAdding == false)
                                //            {
                                //                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "confimationAddAnother()", true);
                                //                //if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                //                //{
                                //                //    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false || (_isRegistrationMandatory))
                                //                //    {
                                //                //        txtSerialNo.Focus();
                                //                //    }
                                //                //    else { txtItem.Focus(); }
                                //                //}
                                //                //else
                                //                //{
                                //                //    //ucPayModes1.button1.Focus();
                                //                //}
                                //            } return;
                                //        }
                                //        else if (_combineCounter == _MainPriceCombinItem.Count)
                                //        {
                                //            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                                //            PriceCombinItemSerialList = new List<ReptPickSerials>();
                                //            _isCombineAdding = false;
                                //            SSPromotionCode = string.Empty;
                                //            ScanSerialNo = string.Empty;
                                //            _serial2 = string.Empty;
                                //            _prefix = string.Empty;

                                //            SSCombineLine += 1;
                                //            _combineCounter = 0;
                                //            _isCheckedPriceCombine = false;

                                //            if (_isCombineAdding == false)
                                //            {
                                //                // this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "confimationAddAnother()", true);
                                //                //if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                //                //{
                                //                //    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                //                //    {
                                //                //        txtSerialNo.Focus();
                                //                //    }
                                //                //    else
                                //                //    {
                                //                //        txtItem.Focus();
                                //                //    }
                                //                //}
                                //                //else
                                //                //{
                                //                //    //ucPayModes1.button1.Focus();
                                //                //}
                                //            } return;
                                //        }
                                //    }
                                //    else
                                //        if (_combineCounter == _MainPriceCombinItem.Count)
                                //        {
                                //            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
                                //            PriceCombinItemSerialList = new List<ReptPickSerials>();
                                //            _isCombineAdding = false;
                                //            SSPromotionCode = string.Empty;
                                //            ScanSerialNo = string.Empty;
                                //            _serial2 = string.Empty;
                                //            _prefix = string.Empty;

                                //            SSCombineLine += 1;
                                //            _combineCounter = 0;
                                //            _isCheckedPriceCombine = false;

                                //            if (_isCombineAdding == false)
                                //            {
                                //                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "confimationAddAnother()", true);
                                //                //if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                //                //{
                                //                //    if (chkDeliverLater.Checked == false && chkDeliverNow.Checked == false)
                                //                //    {
                                //                //        txtSerialNo.Focus();
                                //                //    }
                                //                //    else
                                //                //    {
                                //                //        txtItem.Focus();
                                //                //    }
                                //                //}
                                //                //else
                                //                //{
                                //                //    ucPayModes1.button1.Focus();
                                //                //}
                                //            }
                                //            return;
                                //        }//hdnSerialNo.Value = ""
                                //}
                            }
                        }



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
                        mpAddNewItem.Show();
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
                
                
                
            }
            catch (Exception ex)
            {
                CHNLSVC.CloseChannel();
                DisplayMessage(ex.Message, 4);
                return;
            }
        }
        private bool IsAllScaned(List<ReptPickSerials> _list)
        {// Nadeeka 08-09-2015
            bool _isok = false;
            if (_invoiceItemList != null && _list != null)
            {
                foreach (GridViewRow _itm in gvInvoiceItem.Rows)
                {
                    Label _item = (Label)_itm.FindControl("col_item");
                    Label _qt = (Label)_itm.FindControl("col_qty");
                    decimal _scanQty = Convert.ToDecimal(_qt.Text);
                    decimal _serialcount = 0;


                    MasterItem _itemDet = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item.Text);
                    if (_itemDet.Mi_is_ser1 == 1)
                    {
                        _serialcount = (from _l in _list where _l.Tus_itm_cd == _item.Text select _l.Tus_qty).Sum();

                        if (_scanQty != _serialcount)
                        {
                            _isok = false; break;
                        }
                        else _isok = true;
                    }
                }
            }
            return _isok;
        }

        private void saveQO()
        {
            try
            {
                Int32 row_aff = 0;
                ReptPickHeader _SerHeader = new ReptPickHeader();
                List<ReptPickSerials> _tempSerialSave = new List<ReptPickSerials>();

                if (string.IsNullOrEmpty(txtCustomer.Text))
                {
                    DisplayMessage("Please select customer", 1);
                    txtCustomer.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtdelcuscode.Text))
                {
                    DisplayMessage("Please select delivery customer", 1);
                    txtdelcuscode.Focus();
                    return;
                }

                if (_invoiceItemList == null || _invoiceItemList.Count <= 0)
                {
                    DisplayMessage("Please select relavant items", 1);
                    return;
                }

                if (chkRes.Checked == true)
                {
                    if (grdSerial.Rows.Count <= 0)
                    {
                        DisplayMessage("You select item reservation option but not select details.", 1);
                        return;
                    }
                }

                _masterBusinessCompany = new MasterBusinessEntity();
                if (!string.IsNullOrEmpty(txtdelcuscode.Text))
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtdelcuscode.Text, string.Empty, string.Empty, "C");
                }

                if (_masterBusinessCompany.Mbe_cd == null)//13-10-2015 Nadeeka
                {
                    DisplayMessage("Please select the valid delivery customer", 1);
                    txtdelcuscode.Focus();
                    return;
                }
                if (_dpRate > 0) //kapila 8/1/2016
                {
                    if (((Convert.ToDecimal(lblGrndTotalAmount.Text) + _totDPAmt) / 100 * _dpRate) > _totDPAmt)
                    {
                        DisplayMessage("Down payment is less than the minimum down payment", 1);
                        return;
                    }
                }



                HpSystemParameters _SystemPara = new HpSystemParameters();// Nadeeka 08-09-2015
                _SystemPara = CHNLSVC.Sales.GetSystemParameter("CHNL", BaseCls.GlbDefChannel, "QUOSRL", DateTime.Now.Date);
                if (_SystemPara.Hsy_desc != null)
                {
                    if (_SystemPara.Hsy_val == 1)// Compulsary serial reserve
                    {
                        chkRes.Checked = true;
                        if (grdSerial.Rows.Count <= 0)
                        {
                            DisplayMessage("Please select relavant serials.", 1);
                            return;
                        }



                        #region Check for Scan serial with qty
                        bool _isOk = IsAllScaned(_ResList);
                        if (_isOk == false)
                        {

                            DisplayMessage("Scan serial count and the serial are mismatch", 1);
                            return;
                        }
                        #endregion
                    }
                }



                if (chkRes.Checked == true)
                {
                    if (grdSerial.Rows.Count <= 0)
                    {
                        DisplayMessage("If you want to reserve the items. Please select relavant serials.", 1);     
                        return;
                    }
                    if (SOConfirm == 0)
                    {
                        Label19.Text = "You are going to reserve selected serials. Please confirm ?";
                        MdlSalesOrder.Show();
                        return;
                        //if (MessageBox.Show("You are going to reserve selected serials. Please confirm ?", "Customer Quotation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                        //{
                        //    return;
                        //}
                    }
                    
                }


                //collect quo header
                QuotationHeader _saveHdr = new QuotationHeader();
                _saveHdr.Qh_seq_no = quoSeq;
                _saveHdr.Qh_add1 = txtAddress1.Text;
                _saveHdr.Qh_add2 = txtAddress2.Text;
                _saveHdr.Qh_com = Session["UserCompanyCode"].ToString();
                _saveHdr.Qh_cre_by = Session["UserID"].ToString();
                _saveHdr.Qh_cur_cd = "LKR";
                _saveHdr.Qh_del_cusadd1 = txtdelad1.Text;
                _saveHdr.Qh_del_cusadd2 = txtdelad2.Text;
                _saveHdr.Qh_del_cuscd = txtdelcuscode.Text;
                _saveHdr.Qh_del_cusfax = txtDFax.Text;
                _saveHdr.Qh_del_cusid = txtDNic.Text;
                _saveHdr.Qh_del_cusname = txtdelname.Text;
                _saveHdr.Qh_del_custel = txtDMob.Text;
                _saveHdr.Qh_del_cusvatreg = null;
                _saveHdr.Qh_dt = Convert.ToDateTime(txtdate.Text).Date;
                _saveHdr.Qh_ex_dt = Convert.ToDateTime(txtValidTo.Text).Date;
                _saveHdr.Qh_ex_rt = 1;
                _saveHdr.Qh_frm_dt = Convert.ToDateTime(txtdate.Text).Date;
                _saveHdr.Qh_is_tax = chkTaxPayable.Checked;
                _saveHdr.Qh_jobno = "";
                _saveHdr.Qh_mobi = txtMobile.Text;
                _saveHdr.Qh_mod_by = Session["UserID"].ToString();
                _saveHdr.Qh_no = txtInvoiceNo.Text;
                _saveHdr.Qh_party_cd = txtCustomer.Text;
                _saveHdr.Qh_party_name = txtCusName.Text;
                _saveHdr.Qh_pc = Session["UserDefProf"].ToString();
                _saveHdr.Qh_ref = txtdocrefno.Text;
                _saveHdr.Qh_remarks = txtRemarks.Text;
                _saveHdr.Qh_add_wararmk = txtAddWara.Text;
                _saveHdr.Qh_sales_ex = txtexcutive.Text;
                //  _saveHdr.Qh_seq_no = 0;
                _saveHdr.Qh_session_id = Session["SessionID"].ToString();
                _saveHdr.Qh_stus = "P";
                _saveHdr.Qh_sub_tp = "N";
                _saveHdr.Qh_tel = txtMobile.Text;
                _saveHdr.Qh_tp = "C";
                _saveHdr.Qh_anal_1 = _MasterProfitCenter.Mpc_man;
                _saveHdr.Qh_anal_2 = txtPaymentTerm.Text.Trim();
                _saveHdr.Qh_anal_3 = cmbInvType.Text;
                //kapila 11/3/2016
                _saveHdr.Qh_quo_base = _isSelQuoBaseLevel;

                if (quoSeq == 0)
                {
                    _saveHdr.Qh_no = null;
                }

                Int32 _isRes = 0;
                if (chkRes.Checked == true)
                {
                    _isRes = 1;
                }
                else
                {
                    _isRes = 0;
                }



                _saveHdr.Qh_anal_5 = _isRes;

                if (_isRes == 1)// 24-11-2015 Nadeeka
                {

                    _tempSerialSave.ForEach(x => x.Tus_resqty = 1);
                }
                else
                {
                    _tempSerialSave.ForEach(x => x.Tus_resqty = 0);
                }

                MasterAutoNumber masterAuto = new MasterAutoNumber();
                masterAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
                masterAuto.Aut_cate_tp = "PC";
                masterAuto.Aut_direction = null;
                masterAuto.Aut_modify_dt = null;
                masterAuto.Aut_moduleid = "QUA";
                masterAuto.Aut_number = 5;//what is Aut_number
                masterAuto.Aut_start_char = "QUA";
                masterAuto.Aut_year = null;

                if (chkRes.Checked == true)
                {
                    _SerHeader.Tuh_usrseq_no = CHNLSVC.Inventory.GetSerialID();  //CHNLSVC.Inventory.Generate_new_seq_num(BaseCls.GlbUserID, "RECEIPT", 0, Session["UserCompanyCode"].ToString());
                    _SerHeader.Tuh_usr_id = Session["UserID"].ToString();
                    _SerHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                    _SerHeader.Tuh_session_id = Session["SessionID"].ToString();
                    _SerHeader.Tuh_cre_dt = Convert.ToDateTime(txtValidTo.Text).Date;
                    _SerHeader.Tuh_doc_tp = "QUO";
                    _SerHeader.Tuh_direct = false;
                    _SerHeader.Tuh_ischek_itmstus = true;
                    _SerHeader.Tuh_ischek_simitm = true;
                    _SerHeader.Tuh_ischek_reqqty = true;
                    _SerHeader.Tuh_doc_no = null;


                    foreach (ReptPickSerials line in _ResList)
                    {
                        line.Tus_usrseq_no = _SerHeader.Tuh_usrseq_no;
                        line.Tus_cre_by = Session["UserID"].ToString();
                        _tempSerialSave.Add(line);
                    }
                }

                string QTNum;

                row_aff = (Int32)CHNLSVC.Sales.Quotation_save(_saveHdr, _invoiceItemList, masterAuto, _QuoSerials,null,null,null, chkRes.Checked, _SerHeader, _tempSerialSave, out QTNum);



                if (row_aff >= 1)
                {
                    DisplayMessage("Customer Quotation Successfully Created " + QTNum, 3);
                    //MessageBox.Show("Successfully created.Quotation No: " + QTNum, "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   // print(QTNum); print error


                    Clear_Data();

                }
                else
                {
                    if (!string.IsNullOrEmpty(QTNum))
                    {
                        DisplayMessage(QTNum, 1);
                    }
                    else
                    {
                        DisplayMessage("Creation Fail.", 1);


                    }
                }
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);

            }

        }

        private void print(string _QtNo)
        {
            Session["GlbReportType"] = "SCM1_QUO";            
            Session["GlbReportName"] = "Quotation_RepPrint.rpt";
            Session["documntNo"] = _QtNo;
            string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
            string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";
            PrintPDF(targetFileName);
            string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            PopupConfBox.Hide();
        }
       
        public void PrintPDF(string targetFileName)
        {
            try
            {
                clsSales obj = new clsSales();
                obj.QuotationPrintReport(Session["documntNo"].ToString(), Session["UserCompanyCode"].ToString());
                //ReportDocument Rel = new ReportDocument();
                ReportDocument rptDoc = (ReportDocument)obj._QuoPrint;
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
        protected void lbtnApprove_Click(object sender, EventArgs e)
        {
            string err = "";
            try
            {
                if (txtapprove.Value == "Yes")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16007))
                    {
                        string msg = "Sorry, You have no permission to approve this Customer Quotation.( Advice: Required permission code : 16007)";
                        DisplayMessage(msg, 2);
                        return;
                    }
                    if (string.IsNullOrEmpty(txtInvoiceNo.Text))
                    {
                        string msg = "Please select a customer quotation";
                        DisplayMessage(msg, 2);
                        return;
                    }
                    if (lblStus.Text == "Approved")
                    {
                        string msg = "Selected quotation already approved.";
                        DisplayMessage(msg, 2); 
                        return;
                    }
                    if (lblStus.Text == "Cancelled")
                    {
                        string msg = "Selected quotation already cancelled.";
                        DisplayMessage(msg, 2); 
                        return;
                    }
                    //if (string.IsNullOrEmpty(lblStus.Text))
                    //{
                    //    lblWarning.Text = "Selected quotation not in pending status.";
                    //    divWarning.Visible = true;
                    //    return;
                    //}

                    int count = CHNLSVC.Financial.UpdateStatus_QUO_HDR(txtInvoiceNo.Text, "A", Session["UserID"].ToString(), DateTime.Now, out err);
                    if (count == 1)
                    {
                        string msg = "Successfully Approved. Quotation No: " + txtInvoiceNo.Text;
                        DisplayMessage(msg, 3);
                        Clear(); ClearAll(); Clear_Data();
                        return;
                    }
                    else
                    {
                        string msg = "Approned Fail ." + err;
                        DisplayMessage(msg, 1);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "Error Occurred while processing...  " + ex;
                DisplayMessage(msg, 4);
            }
        }
        private void load_save_Quotation()
        {
            try
            {
                QuotationHeader _saveHdr = new QuotationHeader();

                _saveHdr = CHNLSVC.Sales.Get_Quotation_HDR(txtInvoiceNo.Text);

                if (_saveHdr != null)
                {
                    quoSeq = _saveHdr.Qh_seq_no;
                    txtdate.Text = _saveHdr.Qh_dt.ToShortDateString();
                    txtdocrefno.Text = _saveHdr.Qh_ref;
                    txtCustomer.Text = _saveHdr.Qh_party_cd;
                    txtCusName.Text = _saveHdr.Qh_party_name;
                    txtAddress1.Text = _saveHdr.Qh_add1;
                    txtAddress2.Text = _saveHdr.Qh_add2;
                    txtMobile.Text = _saveHdr.Qh_mobi;
                    chkTaxPayable.Checked = _saveHdr.Qh_is_tax;
                    txtdelcuscode.Text = _saveHdr.Qh_del_cuscd;
                    txtdelname.Text = _saveHdr.Qh_del_cusname;
                    txtdelad1.Text = _saveHdr.Qh_del_cusadd1;
                    txtdelad2.Text = _saveHdr.Qh_del_cusadd2;
                    txtDNic.Text = _saveHdr.Qh_del_cusid;
                    txtDMob.Text = _saveHdr.Qh_del_custel;
                    txtDFax.Text = _saveHdr.Qh_del_cusfax;
                    txtRemarks.Text = _saveHdr.Qh_remarks;
                    txtPaymentTerm.Text = _saveHdr.Qh_anal_2;
                    txtAddWara.Text = _saveHdr.Qh_add_wararmk;
                    txtexcutive.Text = _saveHdr.Qh_sales_ex;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceType);
                    DataTable result = CHNLSVC.CommonSearch.Get_Sales_Ex(SearchParams, "Code", "%" + txtexcutive.Text);
                    if (result.Rows.Count == 1)
                    {
                        lblSalesEx.Text = result.Rows[0][1].ToString();
                    }

                    chkRes.Checked = Convert.ToBoolean(_saveHdr.Qh_anal_5);
                    if (_saveHdr.Qh_stus == "A")
                    {
                        lblStus.Text = "Approved";
                    }
                    else if (_saveHdr.Qh_stus == "P")
                    {
                        lblStus.Text = "Pending";
                    }
                    else if (_saveHdr.Qh_stus == "D")
                    {
                        lblStus.Text = "Finished";

                    }
                    else
                    {
                        lblStus.Text = "Cancelled";
                    }
                    //lblStus.Text = _saveHdr.Qh_stus == "A" ? "Active" : "Cancelled";

                    List<QoutationDetails> _recallList = new List<QoutationDetails>();
                    _recallList = CHNLSVC.Sales.Get_all_linesForQoutation(txtInvoiceNo.Text);
                    _invoiceItemList = _recallList.ToList();
                    foreach (QoutationDetails itm in _invoiceItemList)
                    { 
                        CalculateGrandTotal(itm.Qd_to_qty, itm.Qd_unit_cost, itm.Qd_dis_amt, itm.Qd_itm_tax, true); _lineNo += 1; SSCombineLine += 1;
                        _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Qd_itm_cd);
                        itm.Mi_model = _itemdetail.Mi_model;
                    }
         
                    gvInvoiceItem.AutoGenerateColumns = false;
                    gvInvoiceItem.DataSource = new List<QoutationDetails>();
                    gvInvoiceItem.DataSource = setItemDescriptions(_recallList);
                    gvInvoiceItem.DataBind();
                    List<QuotationSerial> _recallSerList = new List<QuotationSerial>();
                    _recallSerList = CHNLSVC.Sales.GetQuoSerials(txtInvoiceNo.Text);

                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = new List<QuotationSerial>();
                    grdSerial.DataSource = _recallSerList;
                    grdSerial.DataBind();
                    //btnSave.Enabled = false; // Nadeeka 25-08-2015 (Need to update quotation )
                }
                else
                {
                    DisplayMessage("Invalid quotation.", 1);
                    Clear_Data();
                }
            }
            catch (Exception ex)
            {

                DisplayMessage(ex.Message, 4);
            }


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
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesOrderNew);
            DataTable result = CHNLSVC.Sales.SearchSalesOrder(SearchParams, ddlSearchbykeyD.SelectedItem.Text, txtSearchbywordD.Text);
            grdResultD.DataSource = result;
            grdResultD.DataBind();
            lblvalue.Text = "InvoiceWithDate";
            ViewState["SEARCH"] = result;
            UserDPopoup.Show();
            Session["DPopup"] = "DPopup";
            txtSearchbywordD.Focus();
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

        protected void btnAddSerials_Click(object sender, EventArgs e)
        {
            
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label _mainItm = dr.FindControl("col_item") as Label;
            Label _mainLine = dr.FindControl("col_line") as Label;
            Label _mainQty = dr.FindControl("col_qty") as Label;
            lblItem.Text = _mainItm.Text;
            lblLine.Text = _mainLine.Text;
            lblQty.Text = _mainQty.Text;
            serialPopoup.Show();
        }

        protected void txtengine_Click(object sender, EventArgs e)
        {
            try
            {
                serialPopoup.Show();
                if (string.IsNullOrEmpty(txtengine.Text))
                {
                    serialPopoup.Show();
                    return;
                }
                ReptPickSerials _serialList = new ReptPickSerials();
                _serialList = CHNLSVC.Inventory.GetAvailableSerIDInformation(Session["UserCompanyCode"].ToString(),
                    Session["UserDefLoca"].ToString(), lblItem.Text.Trim(), txtengine.Text.Trim(), string.Empty, string.Empty);

                if (_serialList.Tus_ser_1 != null)
                {
                    txtengine.Text = _serialList.Tus_ser_1;
                    _serID = _serialList.Tus_ser_id;
                    if (_serialList.Tus_ser_2 != null)
                    {
                        txtChasis.Text = _serialList.Tus_ser_2;
                    }
                    else
                    {
                        txtChasis.Text = "";
                    }

                }
                else
                {
                    DisplayMessage("Invalid serials.", 2);
                 
                    txtengine.Text = "";
                    txtChasis.Text = "";
                    _serID = 0;
                    txtengine.Focus();
                    serialPopoup.Show();
                    return;
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4);
            }
        }

        protected void lbtnebgine_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblItem.Text))
                {
                    DisplayMessage("Please select item first.", 2);
                    serialPopoup.Show();
                    return;
                }
                Session["_SerialSearchType"] = "SER1_WITEM";
               // string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
              //  DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(SearchParams, null, null);
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes);
                DataTable _result = CHNLSVC.CommonSearch.Search_inr_ser_infor(SearchParams, null, null);
                grdResult.DataSource = _result;
                grdResult.DataBind();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "engin";
                ViewState["SEARCH"] = _result;
                SIPopup.Show();
                serialPopoup.Show();
               
            }
            catch (Exception err)
            {

                DisplayMessage(err.Message, 4);
                serialPopoup.Show();
            }
        }

        protected void lbtnaddserial_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblItem.Text))
                {
                    DisplayMessage("Please select item.", 2);
                    serialPopoup.Show();
                    return;
                 
                }

                if (string.IsNullOrEmpty(txtengine.Text))
                {
                    DisplayMessage("Please select serial number", 2);
                    serialPopoup.Show();
                    return;
                }

                //if (string.IsNullOrEmpty(txtChasis.Text))
                //{
                //    MessageBox.Show("Please select chasiss.", "Customer Quotation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                if (string.IsNullOrEmpty(lblLine.Text))
                {
                    DisplayMessage("Please select item", 2);
                    serialPopoup.Show();
                    return;
                }

                var _record = (from _lst in _QuoSerials
                               where _lst.Qs_item == lblItem.Text.Trim() && _lst.Qs_ser == txtengine.Text.Trim() && _lst.Qs_chassis == txtChasis.Text.Trim()
                               select _lst.Qs_item).ToList();

                if (_record.Count > 0)
                {
                    DisplayMessage("Already select this serial", 2);
                    serialPopoup.Show();
                    return;
                }

                Int32 _qty = 0;
                foreach (QuotationSerial _tmp in _QuoSerials)
                {
                    if (_tmp.Qs_item == lblItem.Text && _tmp.Qs_main_line == Convert.ToInt16(lblLine.Text))
                    {
                        _qty = _qty + 1;
                    }
                }

                decimal _orgQty = 0;
                _orgQty = Convert.ToDecimal(lblQty.Text);

                if (Convert.ToInt16(_orgQty) <= _qty)
                {
                    DisplayMessage("Qty mismatch.", 2);
                    serialPopoup.Show();
                    return;
                }

                QuotationSerial _addSer = new QuotationSerial();
                if (string.IsNullOrEmpty(txtChasis.Text)) { txtChasis.Text = "N/A"; }
                _addSer.Qs_chassis = txtChasis.Text;
                _addSer.Qs_item = lblItem.Text;
                _addSer.Qs_main_line = Convert.ToInt16(lblLine.Text);
                _addSer.Qs_no = null;
                _addSer.Qs_seq_no = 0;
                _addSer.Qs_ser = txtengine.Text;
                _addSer.Qs_ser_line = _QuoSerials.Count + 1;
                _addSer.Qs_ser_id = _serID;
                _addSer.Qs_ser_loc = Session["UserDefLoca"].ToString();
                _QuoSerials.Add(_addSer);

                grdSerial.AutoGenerateColumns = false;
                grdSerial.DataSource = new List<QuotationSerial>();
                grdSerial.DataSource = _QuoSerials;
                grdSerial.DataBind();

                ReptPickSerials _tempItem = new ReptPickSerials();
                _tempItem = CHNLSVC.Inventory.GetAvailableSerIDInformation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), lblItem.Text.Trim(), txtengine.Text.Trim(), string.Empty, string.Empty);

                if (_tempItem.Tus_itm_cd != null)
                {
                    MasterItem _itemList = new MasterItem();
                    _itemList = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), lblItem.Text.Trim());


                    _tempItem.Tus_itm_desc = _itemList.Mi_shortdesc;
                    _tempItem.Tus_itm_model = _itemList.Mi_model;
                    _tempItem.Tus_itm_brand = _itemList.Mi_brand;
                    _tempItem.Tus_base_doc_no = null;
                    _tempItem.Tus_base_itm_line = Convert.ToInt16(lblLine.Text);
                    _tempItem.Tus_isapp = 1;
                    _tempItem.Tus_iscovernote = 1;
                    _tempItem.Tus_com = Session["UserCompanyCode"].ToString();
                    _tempItem.Tus_loc = Session["UserDefLoca"].ToString();
                    if (chkRes.Checked == true)// Nadeeka 10-09-2015
                    {
                        _tempItem.Tus_resqty = 1;
                    }
                    _ResList.Add(_tempItem);


                }


                serialPopoup.Show();
                txtengine.Text = "";
                txtChasis.Text = "";
                txtengine.Focus();

            }
            catch (Exception err)
            {
                serialPopoup.Show();
                DisplayMessage(err.Message, 4);
            }
        }

        protected void lbtnSerAppDel_Click(object sender, EventArgs e)
        {
            #region MyRegion
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label itm = (Label)dr.FindControl("Col_SerItem");
                Label serial = (Label)dr.FindControl("col_SerSerial");
            

                Int32 rowIndex = dr.RowIndex;

                
                if (hdfDeleteItem.Value != null && hdfDeleteItem.Value.ToString() == "1")
                {
                    _QuoSerials.RemoveAt(rowIndex);

                    grdSerial.AutoGenerateColumns = false;
                    grdSerial.DataSource = new List<QuotationSerial>();
                    grdSerial.DataSource = _QuoSerials;
                    grdSerial.DataBind();
                    serialPopoup.Show();
                }

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
                serialPopoup.Show();
            }
            #endregion

          

        }

        protected void lbtndelitem_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                Label _Line = (Label)dr.FindControl("col_line");
                Label _mainItm = (Label)dr.FindControl("col_item");
                Label _tot = (Label)dr.FindControl("col_Tot");
                Label _qty = (Label)dr.FindControl("col_qty");
                Label _uprice = (Label)dr.FindControl("col_UP");
                Label _DAmt = (Label)dr.FindControl("col_DisAmt");
                Label _tax = (Label)dr.FindControl("col_Tax");



                Int32 rowIndex = dr.RowIndex;

                decimal _totVal = Convert.ToDecimal(_tot.Text);
                int _mainLine = Convert.ToInt32(_Line.Text);
                #region Deleting Row



                //kapila 8/1/2016

                if (_totVal < 0) _totDPAmt = _totDPAmt + _totVal;

                _QuoSerials.RemoveAll(x => x.Qs_item == _mainItm.Text && x.Qs_main_line == _mainLine);


                // Int32 _combineLine = Convert.ToInt32(dgItem.Rows[_rowIndex].Cells["InvItm_JobLine"].Value);
                //if (_MainPriceSerial != null)
                //if (_MainPriceSerial.Count > 0)
                //{

                //    //sad_itm_cd,sad_itm_stus,sad_unit_rt,sad_pbook,sad_pb_lvl,Sad_qty
                //    string _item = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Item"].Value;
                //    decimal _uRate = (decimal)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_UPrice"].Value;
                //    string _pbook = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Book"].Value;
                //    string _level = (string)gvInvoiceItem.Rows[_rowIndex].Cells["InvItm_Level"].Value;

                //    List<PriceSerialRef> _tempSerial = _MainPriceSerial;
                //    var _remove = from _list in _tempSerial
                //                  where _list.Sars_itm_cd == _item && _list.Sars_itm_price == _uRate && _list.Sars_pbook == _pbook && _list.Sars_price_lvl == _level
                //                  select _list;

                //    foreach (PriceSerialRef _single in _remove)
                //    {
                //        _tempSerial.Remove(_single);
                //    }

                //    _MainPriceSerial = _tempSerial;
                //}

                List<QoutationDetails> _tempList = _invoiceItemList;
                //var _promo = (from _pro in _invoiceItemList
                //              where _pro.qd_ == _combineLine
                //              select _pro).ToList();

                //if (_promo.Count() > 0)
                //{
                //    foreach (InvoiceItem code in _promo)
                //    {
                //        CalculateGrandTotal(Convert.ToDecimal(code.Sad_qty), (decimal)code.Sad_unit_rt, (decimal)code.Sad_disc_amt, (decimal)code.Sad_itm_tax_amt, false);
                //        //_tempList.Remove(code);
                //        ScanSerialList.RemoveAll(x => x.Tus_base_itm_line == code.Sad_itm_line);
                //        InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == code.Sad_itm_line);
                //    }
                //    _tempList.RemoveAll(x => x.Sad_job_line == _combineLine);
                //}
                //else
                //{
                CalculateGrandTotal(Convert.ToDecimal(_qty.Text), Convert.ToDecimal(_uprice.Text), Convert.ToDecimal(_DAmt.Text), Convert.ToDecimal(_tax.Text), false);
                //InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == _tempList[_rowIndex].Sad_itm_line);
                _tempList.RemoveAt(rowIndex);
                //}

                _invoiceItemList = _tempList;

                Int32 _newLine = 1;
                List<QoutationDetails> _tempLists = _invoiceItemList;
                List<QuotationSerial> _tempSerList = _QuoSerials;

                if (_tempLists != null)
                    if (_tempLists.Count > 0)
                    {
                        foreach (QoutationDetails _itm in _tempLists)
                        {
                            Int32 _line = _itm.Qd_line_no;
                            _invoiceItemList.Where(Y => Y.Qd_line_no == _line).ToList().ForEach(x => x.Qd_line_no = _newLine);


                            foreach (QuotationSerial _ser in _tempSerList)
                            {

                                _QuoSerials.Where(T => T.Qs_main_line == _line).ToList().ForEach(S => S.Qs_main_line = _newLine);
                            }
                            //InvoiceSerialList.Where(y => y.Sap_itm_line == _line).ToList().ForEach(x => x.Sap_itm_line = _newLine);
                            //ScanSerialList.Where(y => y.Tus_base_itm_line == _line).ToList().ForEach(x => x.Tus_base_itm_line = _newLine);

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
                grdSerial.AutoGenerateColumns = false;
                grdSerial.DataSource = new List<QuotationSerial>();
                grdSerial.DataSource = _QuoSerials;
                grdSerial.DataBind();
                return;

                #endregion
            }


            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void btnSoConfirm_Click(object sender, EventArgs e)
        {

            SOConfirm = 1;//
            saveQO();
        }

        protected void btnSoConfirm2_Click(object sender, EventArgs e)
        {
            SOConfirm = 0;//
            
        }

    }
}