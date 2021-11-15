using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using FastForward.SCMWeb.Services;
using FastForward.SCMWeb.View.Reports.Sales;
using FF.BusinessObjects;
using FF.BusinessObjects.Financial;
using FF.BusinessObjects.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.View.Transaction.Finance
{
    public partial class ReceiptEntry : BasePage
    {
        #region Variables
        protected List<HpSheduleDetails> _sheduleDetails { get { return (List<HpSheduleDetails>)Session["_sheduleDetails"]; } set { Session["_sheduleDetails"] = value; } }
        protected List<VehicalRegistration> _regList { get { return (List<VehicalRegistration>)Session["_regList"]; } set { Session["_regList"] = value; } }
        protected List<VehicleInsuarance> _insList { get { return (List<VehicleInsuarance>)Session["_insList"]; } set { Session["_insList"] = value; } }
        protected HpAccount _HpAccount { get { return (HpAccount)Session["_HpAccount"]; } set { Session["_HpAccount"] = value; } }

        protected List<InvOutstanding> _outstandingList { get { return (List<InvOutstanding>)Session["InvOutstanding"]; } set { Session["InvOutstanding"] = value; } }

        protected HpSchemeDetails _SchemeDetails { get { return (HpSchemeDetails)Session["_SchemeDetails"]; } set { Session["_SchemeDetails"] = value; } }
        protected List<ReptPickSerials> _ResList { get { return (List<ReptPickSerials>)Session["_ResList"]; } set { Session["_ResList"] = value; } }
        protected List<RecieptItem> _list { get { return (List<RecieptItem>)Session["_list"]; } set { Session["_list"] = value; } }
        protected List<GiftVoucherPages> _gvDetails { get { return (List<GiftVoucherPages>)Session["_gvDetails"]; } set { Session["_gvDetails"] = value; } }
        protected List<ReceiptItemDetails> _tmpRecItem { get { return (List<ReceiptItemDetails>)Session["_tmpRecItem"]; } set { Session["_tmpRecItem"] = value; } }
        protected List<HpSchemeDefinition> _SchemeDefinition { get { return (List<HpSchemeDefinition>)Session["_SchemeDefinition"]; } set { Session["_SchemeDefinition"] = value; } }
        protected MasterBusinessEntity _businessEntity { get { return (MasterBusinessEntity)Session["_businessEntity"]; } set { Session["_businessEntity"] = value; } }
        protected List<MasterItemComponent> _masterItemComponent { get { return (List<MasterItemComponent>)Session["_masterItemComponent"]; } set { Session["_masterItemComponent"] = value; } }
        protected PriceBookLevelRef _priceBookLevelRef { get { return (PriceBookLevelRef)Session["_priceBookLevelRef"]; } set { Session["_priceBookLevelRef"] = value; } }
        protected List<PriceBookLevelRef> _priceBookLevelRefList { get { return (List<PriceBookLevelRef>)Session["_priceBookLevelRefList"]; } set { Session["_priceBookLevelRefList"] = value; } }
        protected bool _isInventoryCombineAdded { get { return (bool)Session["_isInventoryCombineAdded"]; } set { Session["_isInventoryCombineAdded"] = value; } }
        protected Int32 ScanSequanceNo { get { return (Int32)Session["ScanSequanceNo"]; } set { Session["ScanSequanceNo"] = value; } }
        protected List<ReptPickSerials> ScanSerialList { get { return (List<ReptPickSerials>)Session["ScanSerialList"]; } set { Session["ScanSerialList"] = value; } }
        protected bool IsPriceLevelAllowDoAnyStatus { get { return (bool)Session["IsPriceLevelAllowDoAnyStatus"]; } set { Session["IsPriceLevelAllowDoAnyStatus"] = value; } }
        protected string WarrantyRemarks { get { return (string)Session["WarrantyRemarks"]; } set { Session["WarrantyRemarks"] = value; } }
        protected Int32 WarrantyPeriod { get { return (Int32)Session["WarrantyPeriod"]; } set { Session["WarrantyPeriod"] = value; } }
        protected string ScanSerialNo { get { return (string)Session["ScanSerialNo"]; } set { Session["ScanSerialNo"] = value; } }
        protected string DefaultItemStatus { get { return (string)Session["DefaultItemStatus"]; } set { Session["DefaultItemStatus"] = value; } }
        protected Dictionary<decimal, decimal> ManagerDiscount { get { return (Dictionary<decimal, decimal>)Session["ManagerDiscount"]; } set { Session["ManagerDiscount"] = value; } }
        protected CashGeneralEntiryDiscountDef GeneralDiscount { get { return (CashGeneralEntiryDiscountDef)Session["GeneralDiscount"]; } set { Session["GeneralDiscount"] = value; } }
        protected string DefaultBook { get { return (string)Session["DefaultBook"]; } set { Session["DefaultBook"] = value; } }
        protected string DefaultLevel { get { return (string)Session["DefaultLevel"]; } set { Session["DefaultLevel"] = value; } }
        protected string DefaultInvoiceType { get { return (string)Session["DefaultInvoiceType"]; } set { Session["DefaultInvoiceType"] = value; } }
        protected string DefaultStatus { get { return (string)Session["DefaultStatus"]; } set { Session["DefaultStatus"] = value; } }
        protected string DefaultBin { get { return (string)Session["DefaultBin"]; } set { Session["DefaultBin"] = value; } }
        protected MasterItem _itemdetail { get { return (MasterItem)Session["_itemdetail"]; } set { Session["_itemdetail"] = value; } }
        protected List<PriceDetailRef> _priceDetailRef { get { return (List<PriceDetailRef>)Session["_priceDetailRef"]; } set { Session["_priceDetailRef"] = value; } }
        protected MasterBusinessEntity _masterBusinessCompany { get { return (MasterBusinessEntity)Session["_masterBusinessCompany"]; } set { Session["_masterBusinessCompany"] = value; } }
        protected List<PriceSerialRef> _MainPriceSerial { get { return (List<PriceSerialRef>)Session["_MainPriceSerial"]; } set { Session["_MainPriceSerial"] = value; } }
        protected List<PriceSerialRef> _tempPriceSerial { get { return (List<PriceSerialRef>)Session["_tempPriceSerial"]; } set { Session["_tempPriceSerial"] = value; } }
        protected List<PriceCombinedItemRef> _MainPriceCombinItem { get { return (List<PriceCombinedItemRef>)Session["_MainPriceCombinItem"]; } set { Session["_MainPriceCombinItem"] = value; } }
        protected List<PriceCombinedItemRef> _tempPriceCombinItem { get { return (List<PriceCombinedItemRef>)Session["_tempPriceCombinItem"]; } set { Session["_tempPriceCombinItem"] = value; } }
        protected Int32 _lineNo { get { return (Int32)Session["_lineNo"]; } set { Session["_lineNo"] = value; } }
        protected bool _isEditPrice { get { return (bool)Session["_isEditPrice"]; } set { Session["_isEditPrice"] = value; } }
        protected bool _isEditDiscount { get { return (bool)Session["_isEditDiscount"]; } set { Session["_isEditDiscount"] = value; } }
        protected decimal GrndSubTotal { get { return (decimal)Session["GrndSubTotal"]; } set { Session["GrndSubTotal"] = value; } }
        protected decimal GrndDiscount { get { return (decimal)Session["GrndDiscount"]; } set { Session["GrndDiscount"] = value; } }
        protected decimal _toBePayNewAmount { get { return (decimal)Session["_toBePayNewAmount"]; } set { Session["_toBePayNewAmount"] = value; } }
        protected bool _isCompleteCode { get { return (bool)Session["_isCompleteCode"]; } set { Session["_isCompleteCode"] = value; } }
        protected bool _isGiftVoucherCheckBoxClick { get { return (bool)Session["_isGiftVoucherCheckBoxClick"]; } set { Session["_isGiftVoucherCheckBoxClick"] = value; } }
        protected DataTable MasterChannel { get { return (DataTable)Session["MasterChannel"]; } set { Session["MasterChannel"] = value; } }
        protected bool IsToken { get { return (bool)Session["IsToken"]; } set { Session["IsToken"] = value; } }
        protected bool IsSaleFigureRoundUp { get { return (bool)Session["IsSaleFigureRoundUp"]; } set { Session["IsSaleFigureRoundUp"] = value; } }
        protected DataTable _tblExecutive { get { return (DataTable)Session["_tblExecutive"]; } set { Session["_tblExecutive"] = value; } }
        protected bool IsFwdSaleCancelAllowUser { get { return (bool)Session["IsFwdSaleCancelAllowUser"]; } set { Session["IsFwdSaleCancelAllowUser"] = value; } }
        protected bool IsDlvSaleCancelAllowUser { get { return (bool)Session["IsDlvSaleCancelAllowUser"]; } set { Session["IsDlvSaleCancelAllowUser"] = value; } }
        protected bool _IsVirtualItem { get { return (bool)Session["_IsVirtualItem"]; } set { Session["_IsVirtualItem"] = value; } }
        protected string technicianCode { get { return (string)Session["technicianCode"]; } set { Session["technicianCode"] = value; } }
        protected bool _iswhat { get { return (bool)Session["_iswhat"]; } set { Session["_iswhat"] = value; } }
        protected decimal SSPriceBookPrice { get { return (decimal)Session["SSPriceBookPrice"]; } set { Session["SSPriceBookPrice"] = value; } }
        protected string SSPriceBookSequance { get { return (string)Session["SSPriceBookSequance"]; } set { Session["SSPriceBookSequance"] = value; } }
        protected string SSPriceBookItemSequance { get { return (string)Session["SSPriceBookItemSequance"]; } set { Session["SSPriceBookItemSequance"] = value; } }
        protected string SSIsLevelSerialized { get { return (string)Session["SSIsLevelSerialized"]; } set { Session["SSIsLevelSerialized"] = value; } }
        protected string SSPromotionCode { get { return (string)Session["SSPromotionCode"]; } set { Session["SSPromotionCode"] = value; } }
        protected string SSCirculerCode { get { return (string)Session["SSCirculerCode"]; } set { Session["SSCirculerCode"] = value; } }
        protected Int32 SSPRomotionType { get { return (Int32)Session["SSPRomotionType"]; } set { Session["SSPRomotionType"] = value; } }
        protected Int32 SSCombineLine { get { return (Int32)Session["SSCombineLine"]; } set { Session["SSCombineLine"] = value; } }
        protected List<MasterItemTax> MainTaxConstant { get { return (List<MasterItemTax>)Session["MainTaxConstant"]; } set { Session["MainTaxConstant"] = value; } }
        protected List<ReptPickSerials> _promotionSerial { get { return (List<ReptPickSerials>)Session["_promotionSerial"]; } set { Session["_promotionSerial"] = value; } }
        protected List<ReptPickSerials> _promotionSerialTemp { get { return (List<ReptPickSerials>)Session["_promotionSerialTemp"]; } set { Session["_promotionSerialTemp"] = value; } }
        protected bool _isBackDate { get { return (bool)Session["_isBackDate"]; } set { Session["_isBackDate"] = value; } }
        protected MasterProfitCenter _MasterProfitCenter { get { return (MasterProfitCenter)Session["_MasterProfitCenter"]; } set { Session["_MasterProfitCenter"] = value; } }
        protected List<PriceDefinitionRef> _PriceDefinitionRef { get { return (List<PriceDefinitionRef>)Session["_PriceDefinitionRef"]; } set { Session["_PriceDefinitionRef"] = value; } }
        protected string InvoiceBackDateName { get { return (string)Session["InvoiceBackDateName"]; } set { Session["InvoiceBackDateName"] = value; } }
        protected int VirtualCounter { get { return (int)Session["int VirtualCounter"]; } set { Session["int VirtualCounter"] = value; } }
        protected List<InvoiceSerial> InvoiceSerialList { get { return (List<InvoiceSerial>)Session["InvoiceSerialList"]; } set { Session["InvoiceSerialList"] = value; } }
        protected List<ReptPickSerials> InventoryCombinItemSerialList { get { return (List<ReptPickSerials>)Session["InventoryCombinItemSerialList"]; } set { Session["InventoryCombinItemSerialList"] = value; } }
        protected List<ReptPickSerials> PriceCombinItemSerialList { get { return (List<ReptPickSerials>)Session["PriceCombinItemSerialList"]; } set { Session["PriceCombinItemSerialList"] = value; } }
        protected List<ReptPickSerials> BuyBackItemList { get { return (List<ReptPickSerials>)Session["BuyBackItemList"]; } set { Session["BuyBackItemList"] = value; } }
        protected string _proVouInvcItem { get { return (string)Session["_proVouInvcItem"]; } set { Session["_proVouInvcItem"] = value; } }
        protected bool _serialMatch { get { return (bool)Session["_serialMatch"]; } set { Session["_serialMatch"] = value; } }
        protected PriortyPriceBook _priorityPriceBook { get { return (PriortyPriceBook)Session["_priorityPriceBook"]; } set { Session["_priorityPriceBook"] = value; } }
        protected Boolean _isProcess { get { return (Boolean)Session["_isProcess"]; } set { Session["_isProcess"] = value; } }
        protected decimal _regAmt { get { return (decimal)Session["_regAmt"]; } set { Session["_regAmt"] = value; } }
        protected string _invType { get { return (string)Session["_invType"]; } set { Session["_invType"] = value; } }
        protected Int32 _invLine { get { return (Int32)Session["_invLine"]; } set { Session["_invLine"] = value; } }
        protected string _accNo { get { return (string)Session["_accNo"]; } set { Session["_accNo"] = value; } }
        protected string _invNo { get { return (string)Session["_invNo"]; } set { Session["_invNo"] = value; } }
        protected Int32 _insuTerm { get { return (Int32)Session["_insuTerm"]; } set { Session["_insuTerm"] = value; } }
        protected Int32 _colTerm { get { return (Int32)Session["_colTerm"]; } set { Session["_colTerm"] = value; } }
        protected decimal _insuAmt { get { return (decimal)Session["_insuAmt"]; } set { Session["_insuAmt"] = value; } }
        protected Boolean _IsRecall { get { return (Boolean)Session["_IsRecall"]; } set { Session["_IsRecall"] = value; } }
        protected Boolean _RecStatus { get { return (Boolean)Session["_RecStatus"]; } set { Session["_RecStatus"] = value; } }
        protected Boolean _sunUpload { get { return (Boolean)Session["_sunUpload"]; } set { Session["_sunUpload"] = value; } }
        protected Boolean _isRes { get { return (Boolean)Session["_isRes"]; } set { Session["_isRes"] = value; } }
        protected decimal _usedAmt { get { return (decimal)Session["_usedAmt"]; } set { Session["_usedAmt"] = value; } }
        protected string _selectPromoCode { get { return (string)Session["_selectPromoCode"]; } set { Session["_selectPromoCode"] = value; } }
        protected string _selectSerial { get { return (string)Session["_selectSerial"]; } set { Session["_selectSerial"] = value; } }
        protected DateTime _serverDt { get { return (DateTime)Session["_serverDt"]; } set { Session["_serverDt"] = value; } }
        protected Boolean chkDeliverLater { get { return (Boolean)Session["chkDeliverLater"]; } set { Session["chkDeliverLater"] = value; } }
        protected Boolean chkDeliverNow { get { return (Boolean)Session["chkDeliverNow"]; } set { Session["chkDeliverNow"] = value; } }
        protected string lblPromoVouNo { get { return (string)Session["lblPromoVouNo"]; } set { Session["lblPromoVouNo"] = value; } }
        protected string lblPromoVouUsedFlag { get { return (string)Session["lblPromoVouUsedFlag"]; } set { Session["lblPromoVouUsedFlag"] = value; } }
        protected List<InvoiceItem> _invoiceItemList { get { return (List<InvoiceItem>)Session["_invoiceItemList"]; } set { Session["_invoiceItemList"] = value; } }
        protected List<InvoiceItem> _invoiceItemListWithDiscount { get { return (List<InvoiceItem>)Session["_invoiceItemListWithDiscount"]; } set { Session["_invoiceItemListWithDiscount"] = value; } }
        protected List<RecieptItem> _recieptItem { get { return (List<RecieptItem>)Session["_recieptItem"]; } set { Session["_recieptItem"] = value; } }
        protected List<RecieptItem> _newRecieptItem { get { return (List<RecieptItem>)Session["_newRecieptItem"]; } set { Session["_newRecieptItem"] = value; } }
        protected DataTable _levelStatus { get { return (DataTable)Session["_levelStatus"]; } set { Session["_levelStatus"] = value; } }
        protected Boolean _isBlocked { get { return (Boolean)Session["_isBlocked"]; } set { Session["_isBlocked"] = value; } }
        protected Boolean _isItemChecking { get { return (Boolean)Session["_isItemChecking"]; } set { Session["_isItemChecking"] = value; } }
        protected LoyaltyType _loyaltyType { get { return (LoyaltyType)Session["_loyaltyType"]; } set { Session["_loyaltyType"] = value; } }


        protected decimal _vouDisvals { get { return (decimal)Session["_vouDisvals"]; } set { Session["_vouDisvals"] = value; } }
        protected decimal _vouDisrates { get { return (decimal)Session["_vouDisrates"]; } set { Session["_vouDisrates"] = value; } }
        protected string _vouNo { get { return (string)Session["_vouNo"]; } set { Session["_vouNo"] = value; } }
        protected decimal _maxAllowQty { get { return (decimal)Session["_maxAllowQty"]; } set { Session["_maxAllowQty"] = value; } }
        protected decimal _NetAmt { get { return (decimal)Session["_NetAmt"]; } set { Session["_NetAmt"] = value; } }
        protected decimal _TotVat { get { return (decimal)Session["_TotVat"]; } set { Session["_TotVat"] = value; } }
        protected decimal _DisCashPrice { get { return (decimal)Session["_DisCashPrice"]; } set { Session["_DisCashPrice"] = value; } }
        protected decimal _varInstallComRate { get { return (decimal)Session["_varInstallComRate"]; } set { Session["_varInstallComRate"] = value; } }
        protected string _SchTP { get { return (string)Session["_SchTP"]; } set { Session["_SchTP"] = value; } }
        protected decimal _commission { get { return (decimal)Session["_commission"]; } set { Session["_commission"] = value; } }
        protected decimal _discount { get { return (decimal)Session["_discount"]; } set { Session["_discount"] = value; } }
        protected decimal _UVAT { get { return (decimal)Session["_UVAT"]; } set { Session["_UVAT"] = value; } }
        protected decimal _varVATAmt { get { return (decimal)Session["_varVATAmt"]; } set { Session["_varVATAmt"] = value; } }
        protected decimal _IVAT { get { return (decimal)Session["_IVAT"]; } set { Session["_IVAT"] = value; } }
        protected decimal _varCashPrice { get { return (decimal)Session["_varCashPrice"]; } set { Session["_varCashPrice"] = value; } }
        protected decimal _varInitialVAT { get { return (decimal)Session["_varInitialVAT"]; } set { Session["_varInitialVAT"] = value; } }
        protected decimal _vDPay { get { return (decimal)Session["_vDPay"]; } set { Session["_vDPay"] = value; } }
        protected decimal _varInsVAT { get { return (decimal)Session["_varInsVAT"]; } set { Session["_varInsVAT"] = value; } }
        protected decimal _MinDPay { get { return (decimal)Session["_MinDPay"]; } set { Session["_MinDPay"] = value; } }
        protected decimal _varAmountFinance { get { return (decimal)Session["_varAmountFinance"]; } set { Session["_varAmountFinance"] = value; } }
        protected decimal _varIntRate { get { return (decimal)Session["_varIntRate"]; } set { Session["_varIntRate"] = value; } }
        protected decimal _varInterestAmt { get { return (decimal)Session["_varInterestAmt"]; } set { Session["_varInterestAmt"] = value; } }
        protected decimal _varServiceCharge { get { return (decimal)Session["_varServiceCharge"]; } set { Session["_varServiceCharge"] = value; } }
        protected decimal _varInitServiceCharge { get { return (decimal)Session["_varInitServiceCharge"]; } set { Session["_varInitServiceCharge"] = value; } }
        protected decimal _varServiceChargesAdd { get { return (decimal)Session["_varServiceChargesAdd"]; } set { Session["_varServiceChargesAdd"] = value; } }
        protected decimal _varHireValue { get { return (decimal)Session["_varHireValue"]; } set { Session["_varHireValue"] = value; } }
        protected decimal _varCommAmt { get { return (decimal)Session["_varCommAmt"]; } set { Session["_varCommAmt"] = value; } }
        protected decimal _varStampduty { get { return (decimal)Session["_varStampduty"]; } set { Session["_varStampduty"] = value; } }
        protected decimal _varInitialStampduty { get { return (decimal)Session["_varInitialStampduty"]; } set { Session["_varInitialStampduty"] = value; } }
        protected decimal _varOtherCharges { get { return (decimal)Session["_varOtherCharges"]; } set { Session["_varOtherCharges"] = value; } }
        protected decimal _varFInsAmount { get { return (decimal)Session["_varFInsAmount"]; } set { Session["_varFInsAmount"] = value; } }
        protected decimal _varInsAmount { get { return (decimal)Session["_varInsAmount"]; } set { Session["_varInsAmount"] = value; } }
        protected decimal _varInsCommRate { get { return (decimal)Session["_varInsCommRate"]; } set { Session["_varInsCommRate"] = value; } }
        protected decimal _varInsVATRate { get { return (decimal)Session["_varInsVATRate"]; } set { Session["_varInsVATRate"] = value; } }
        protected decimal _varTotCash { get { return (decimal)Session["_varTotCash"]; } set { Session["_varTotCash"] = value; } }
        protected decimal _varTotalInstallmentAmt { get { return (decimal)Session["_varTotalInstallmentAmt"]; } set { Session["_varTotalInstallmentAmt"] = value; } }
        protected decimal _varRental { get { return (decimal)Session["_varRental"]; } set { Session["_varRental"] = value; } }
        protected decimal _varAddRental { get { return (decimal)Session["_varAddRental"]; } set { Session["_varAddRental"] = value; } }
        protected decimal _ExTotAmt { get { return (decimal)Session["_ExTotAmt"]; } set { Session["_ExTotAmt"] = value; } }
        protected decimal BalanceAmount { get { return (decimal)Session["BalanceAmount"]; } set { Session["BalanceAmount"] = value; } }
        protected decimal PaidAmount { get { return (decimal)Session["PaidAmount"]; } set { Session["PaidAmount"] = value; } }
        protected decimal BankOrOther_Charges { get { return (decimal)Session["BankOrOther_Charges"]; } set { Session["BankOrOther_Charges"] = value; } }
        protected decimal AmtToPayForFinishPayment { get { return (decimal)Session["AmtToPayForFinishPayment"]; } set { Session["AmtToPayForFinishPayment"] = value; } }
        protected Boolean _isBlack { get { return (Boolean)Session["_isBlack"]; } set { Session["_isBlack"] = value; } }
        protected Boolean _insuAllow { get { return (Boolean)Session["_insuAllow"]; } set { Session["_insuAllow"] = value; } }
        protected Int16 _priceType { get { return (Int16)Session["_priceType"]; } set { Session["_priceType"] = value; } }
        protected string _invoicePrefix { get { return (string)Session["_invoicePrefix"]; } set { Session["_invoicePrefix"] = value; } }
        protected decimal _varMgrComm { get { return (decimal)Session["_varMgrComm"]; } set { Session["_varMgrComm"] = value; } }
        protected Boolean _isCalProcess { get { return (Boolean)Session["_isCalProcess"]; } set { Session["_isCalProcess"] = value; } }
        protected Boolean _isSysReceipt { get { return (Boolean)Session["_isSysReceipt"]; } set { Session["_isSysReceipt"] = value; } }
        protected Boolean _isGV { get { return (Boolean)Session["_isGV"]; } set { Session["_isGV"] = value; } }
        protected string _manCd { get { return (string)Session["_manCd"]; } set { Session["_manCd"] = value; } }
        protected Boolean _isFoundTaxDef { get { return (Boolean)Session["_isFoundTaxDef"]; } set { Session["_isFoundTaxDef"] = value; } }
        protected Int32 _calMethod { get { return (Int32)Session["_calMethod"]; } set { Session["_calMethod"] = value; } }


        protected bool _isCombineAdding { get { return (bool)Session["_isCombineAdding"]; } set { Session["_isCombineAdding"] = value; } }
        protected int _combineCounter { get { return (int)Session["_combineCounter"]; } set { Session["_combineCounter"] = value; } }
        protected string _paymodedef { get { return (string)Session["_paymodedef"]; } set { Session["_paymodedef"] = value; } }
        protected bool _isCheckedPriceCombine { get { return (bool)Session["_isCheckedPriceCombine"]; } set { Session["_isCheckedPriceCombine"] = value; } }
        protected bool _isFirstPriceComItem { get { return (bool)Session["_isFirstPriceComItem"]; } set { Session["_isFirstPriceComItem"] = value; } }
        protected string _serial2 { get { return (string)Session["_serial2"]; } set { Session["_serial2"] = value; } }
        protected string _prefix { get { return (string)Session["_prefix"]; } set { Session["_prefix"] = value; } }
        protected bool _isRegistrationMandatory { get { return (bool)Session["_isRegistrationMandatory"]; } set { Session["_isRegistrationMandatory"] = value; } }

        protected bool _isNewPromotionProcess { get { return (bool)Session["_isNewPromotionProcess"]; } set { Session["_isNewPromotionProcess"] = value; } }
        protected List<PriceDetailRef> _PriceDetailRefPromo { get { return (List<PriceDetailRef>)Session["_PriceDetailRefPromo"]; } set { Session["_PriceDetailRefPromo"] = value; } }
        protected List<PriceSerialRef> _PriceSerialRefPromo { get { return (List<PriceSerialRef>)Session["_PriceSerialRefPromo"]; } set { Session["_PriceSerialRefPromo"] = value; } }
        protected List<PriceSerialRef> _PriceSerialRefNormal { get { return (List<PriceSerialRef>)Session["_PriceSerialRefNormal"]; } set { Session["_PriceSerialRefNormal"] = value; } }

        protected decimal GrndTax { get { return (decimal)Session["GrndTax"]; } set { Session["GrndTax"] = value; } }

        protected List<gvRegItems> oDebitInvoices { get { return (List<gvRegItems>)Session["oDebitInvoices"]; } set { Session["oDebitInvoices"] = value; } }
        public List<ReceiptEntryExcel> _listReceiptEntry = new List<ReceiptEntryExcel>();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Clear_Data();
            }
            else
            {


                var ctrlName = Request.Params[Page.postEventSourceID];
                var args = Request.Params[Page.postEventArgumentID];
                string[] objectNames = ctrlName.ToString().Split('$');

                //Exception asdasd = new Exception();
                //WriteErrLog(objectNames[objectNames.Length - 1], asdasd);

                if (Session["ShowCU"] == "Y")
                {
                    mpCustomer.Show();
                }

                if (objectNames[objectNames.Length - 1] == "txtSerialNo")
                {
                    txtSerial_TextChange();
                }
                else if (objectNames[objectNames.Length - 1] == "TxtAdvItem")
                {
                    //Con_continueWithTheAvailablePromotions.Value = null;
                    txtItemCode_TextChange();
                }

                if (Session["_cusCode"] != null)
                {
                    txtCusCode.Text = Session["_cusCode"].ToString();
                    txtCusCode_TextChanged(null, null);
                    Session["_cusCode"] = null;
                }
            }
        }

        protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
        {

        }

        #region View one

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (_outstandingList.Count > 0)
            {
                ModalPopupExtenderOutstanding.Show();
                GridViewInv.DataSource = _outstandingList;
                GridViewInv.DataBind();
            }
            else
            {
                btnSaveSub_Click(null, null);
            }
        }

        protected void btnConOutfYes_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderOutstanding.Hide();
            _outstandingList = new List<InvOutstanding>();
            btnSaveSub_Click(null, null);
        }
        protected void btnConfNoOut_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderOutstanding.Hide();
        }

        protected void btnSaveSub_Click(object sender, EventArgs e)
        {
            try
            {
                string checkTimeMsg = string.Empty;
                if (CheckServerDateTime(out checkTimeMsg) == false)
                {
                    DisplayMessage(checkTimeMsg);
                    return;
                }

                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    DisplayMessage("Please select customer.");
                    txtCusCode.Text = "";
                    txtCusCode.Focus();
                    return;
                }

                if (ucPayModes1.Balance != 0)
                {
                    DisplayMessage("Payment not completed.");
                    return;
                }

                if (string.IsNullOrEmpty(txtRecType.Text))
                {
                    DisplayMessage("Receipt type is missing.");
                    txtRecType.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtDivision.Text))
                {
                    DisplayMessage("Receipt division is missing.");
                    txtDivision.Focus();
                    return;
                }

                if (!CHNLSVC.Sales.IsValidDivision(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtDivision.Text.Trim()))
                {
                    DisplayMessage("Invalid division.");
                    txtDivision.Text = "";
                    txtDivision.Focus();
                    return;
                }

                decimal _shouldPay = 0;
                if (txtRecType.Text == "ADVAN")
                {
                    if (!string.IsNullOrEmpty(TxtAdvItem.Text))
                    {
                        //for (int i = 0; i < dgvItem.Rows.Count; i++)
                        //{
                        //    if (dgvItem.Rows[i].Cells[11].Value.ToString() == TxtAdvItem.Text)
                        //    {
                        //        _shouldPay = _shouldPay + Convert.ToDecimal(dgvItem.Rows[i].Cells[11].Value.ToString());
                        //    }

                        //    if (Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text) < _shouldPay)
                        //    {
                        //        DisplayMessage("Amount should not be lesser than the item value.");
                        //        txtPayment.Text = _shouldPay.ToString("n"); ;
                        //        txtPayment.Focus();
                        //        return;
                        //    }
                        //}
                    }

                    //if (string.IsNullOrEmpty(comboBoxPrefix.Text))
                    //{
                    //    DisplayMessage("Please enter prefix.");
                    //    comboBoxPrefix.Focus();
                    //    return;
                    //}
                }

                //if (radioButtonManual.Checked == true)
                //{
                //    if (string.IsNullOrEmpty(txtManual.Text))
                //    {
                //        DisplayMessage("Please enter manual document number.");
                //        txtManual.Text = "";
                //        txtManual.Focus();
                //        return;
                //    }

                //    Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument_prefix(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "MDOC_AVREC", Convert.ToInt32(txtManual.Text), comboBoxPrefix.Text);
                //    if (_IsValid == false)
                //    {
                //        DisplayMessage("Invalid manual document number.");
                //        txtManual.Text = "";
                //        txtManual.Focus();
                //        return;
                //    }

                //    RecieptHeader rh = new RecieptHeader();
                //    rh = CHNLSVC.Sales.Check_ManRef_Rec_prefix(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtRecType.Text.Trim(), txtManual.Text.Trim(), comboBoxPrefix.Text);

                //    if (rh != null)
                //    {
                //        DisplayMessage("Receipt number : " + txtManual.Text + " already used.");
                //        txtManual.Text = "";
                //        txtManual.Focus();
                //        return;
                //    }
                //}

                if (txtRecType.Text == "ADVAN")
                {
                    if (radioButtonSystem.Checked == true)
                    {
                        DataTable chkloc = CHNLSVC.Financial.CheckLoctype(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "SR");
                        Int32 count = chkloc.Rows.Count;

                        if (string.IsNullOrEmpty(txtManual.Text) && count > 0)
                        {
                            DisplayMessage("Please enter system document number.");
                            txtManual.Text = "";
                            txtManual.Focus();
                            return;
                        }

                        //2016-08-02 for 
                        //Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument_prefix(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "SDOC_AVREC", Convert.ToInt32(txtManual.Text), comboBoxPrefix.Text);
                        //if (_IsValid == false)
                        //{
                        //    DisplayMessage("Invalid system document number.");
                        //    txtManual.Text = "";
                        //    txtManual.Focus();
                        //    return;
                        //}

                        RecieptHeader rh = new RecieptHeader();
                        rh = CHNLSVC.Sales.Check_ManRef_Rec_prefix(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtRecType.Text.Trim(), txtManual.Text.Trim(), comboBoxPrefix.Text);

                        if (rh != null)
                        {
                            DisplayMessage("Receipt number : " + txtManual.Text + " already used.");
                            txtManual.Text = "";
                            txtManual.Focus();
                            return;
                        }
                    }

                    //if (txtRecType.Text == "ADVAN")
                    //  {

                    //if (string.IsNullOrEmpty(comboBoxPrefix.Text))
                    //{
                    //    MessageBox.Show("Please enter prefix.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    comboBoxPrefix.Focus();
                    //    return;
                    //}

                    if (!string.IsNullOrEmpty(txtItem.Text))
                    {
                        DisplayMessage("Please add item before save receipt.");
                        txtItem.Focus();
                        return;
                    }

                    List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADREFMAXDT", "COM", Session["UserCompanyCode"].ToString());

                    if (para.Count <= 0)
                    {
                        DisplayMessage("system parameter not setup for Advance receipt valid period.");
                        return;
                    }
                }

                if (dgvItem.Rows.Count > 0)
                {
                    if (Con_itemswhichYouSelectIsSorrect.Value == "Y")
                    {

                    }
                    else if (Con_itemswhichYouSelectIsSorrect.Value == "N")
                    {
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "itemswhichYouSelectIsSorrect('');", true);
                        return;
                    }
                    //_??if (MessageBox.Show("Confirm the items which you select is correct.?", "Receipt Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    //_??{
                    //_??    return;
                    //_??}
                }


                if (ucPayModes1.MainGrid.Rows.Count == 0)
                {
                    DisplayMessage("Payments are not found.");
                    return;
                }

                if (txtRecType.Text == "VHREG")
                {
                    if (dgvReg.Rows.Count <= 0)
                    {
                        DisplayMessage("Registration details are not found.");
                        return;
                    }
                }
                else if (txtRecType.Text == "VHINS")
                {
                    if (dgvIns.Rows.Count <= 0)
                    {
                        DisplayMessage("Insurance details are not found.");
                        return;
                    }
                }
                else if (txtRecType.Text == "ADINS")
                {
                    if (dgvIns.Rows.Count <= 0)
                    {
                        DisplayMessage("Insurance details are not found.");
                        return;
                    }
                }

                //kapila 28/7/2015
                else if (txtRecType.Text == "DEBT" || txtRecType.Text == "SRTN" || txtRecType.Text == "SVAT" || txtRecType.Text == "DAJ")
                {
                    Decimal _Balance = 0;
                    Decimal _minCommAllow = 0;
                    Decimal _maxDaysAllow = 0;
                    HpSystemParameters _getSystemParameter = new HpSystemParameters();

                    _getSystemParameter = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "COMIGNORE", Convert.ToDateTime(dtpRecDate.Text).Date);

                    if (_getSystemParameter.Hsy_cd != null)
                        _maxDaysAllow = _getSystemParameter.Hsy_val;
                    else
                        _maxDaysAllow = -1;

                    if (_maxDaysAllow != -1)    //record found in hpr_sys_para table
                    {
                        if (chkOth.Checked == true)
                            _Balance = CHNLSVC.Sales.GetOutInvAmt(Session["UserCompanyCode"].ToString(), txtOthSR.Text, txtCusCode.Text.Trim(), _invNo);
                        else
                            _Balance = CHNLSVC.Sales.GetOutInvAmt(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text.Trim(), _invNo);

                        _getSystemParameter = CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "CRCOMMINAW", Convert.ToDateTime(dtpRecDate.Text).Date);

                        if (_getSystemParameter.Hsy_cd != null)
                            _minCommAllow = _getSystemParameter.Hsy_val;
                        else
                            _minCommAllow = 0;

                        if ((_Balance - Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text)) <= _minCommAllow)
                        {
                            //check whether registration is done
                            List<VehicalRegistration> _preReg = new List<VehicalRegistration>();
                            _preReg = CHNLSVC.General.GetVehiclesByInvoiceNo(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _invNo);

                            if (_preReg != null)
                            {
                                decimal _count = _preReg.Where(x => x.P_svrt_prnt_stus != 2).Count();
                                if (_count == 0)
                                {
                                    if (Con_RegistrationIsNotAvailableAreYouSure.Value == "Y")
                                    {

                                    }
                                    else if (Con_RegistrationIsNotAvailableAreYouSure.Value == "N")
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "RegistrationIsNotAvailableAreYouSure('');", true);
                                        return;
                                    }

                                    //_??if (MessageBox.Show("Commission is not calculated. Reason: Registration is not available. Are you sure ?", "Receipt Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                    //_??{
                                    //_??    return;
                                    //_??}
                                }
                                else
                                {
                                    DataTable _dtInvDt = CHNLSVC.Sales.GetSalesHdr(_invNo);
                                    Int32 _days = Convert.ToInt32((Convert.ToDateTime(_dtInvDt.Rows[0]["sah_dt"]) - _preReg[0].P_svrt_reg_dt).TotalDays);
                                    if (_days > _maxDaysAllow)
                                    {
                                        if (Con_RegAftrThAlowPridAreYouSure.Value == "Y")
                                        {

                                        }
                                        else if (Con_RegAftrThAlowPridAreYouSure.Value == "N")
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "RegistrationIsNotAvailableAreYouSure('');", true);
                                            return;
                                        }
                                        //_??if (MessageBox.Show("Commission is not calculated. Reason: Registration is done after the allow period. Are you sure ?", "Receipt Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                        //_??{
                                        //_??    return;
                                        //_??}
                                    }
                                }
                            }
                            else
                            {

                                if (Con_CommissionIsNotCalculated.Value == "Y")
                                {

                                }
                                else if (Con_CommissionIsNotCalculated.Value == "N")
                                {
                                    return;
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "CommissionIsNotCalculated('');", true);
                                    return;
                                }

                                //_??if (MessageBox.Show("Commission is not calculated \n Reason: Registration is not available. \n Are you sure ?", "Receipt Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No)
                                //_??{
                                //_??    return;
                                //_??}
                            }
                        }
                    }
                }

                if (txtRecType.Text == "DISP")
                {
                    if (string.IsNullOrEmpty(txtDisposalJob.Text.Trim()))
                    {
                        DisplayMessage("Please select disposal job number");
                        txtDisposalJob.Focus();
                        return;
                    }
                }

                decimal _wkNo = 0;
                int _weekNo = CHNLSVC.General.GetWeek(Convert.ToDateTime(dtpRecDate.Text).Date, out _wkNo, Session["UserCompanyCode"].ToString());

                if (_weekNo == 0)
                {
                    DisplayMessage("Week Definition is still not setup for current date.Please contact retail accounts dept.");
                    return;
                }
                bool _allowCurrentTrans = false;


                if (!IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserDefProf"].ToString(), this.Page, dtpRecDate.Text, btndtpRecDate, lblBackDateInfor, "m_Trans_Sales_SalesInvoice", out  _allowCurrentTrans))
                {
                    if (Convert.ToDateTime(dtpRecDate.Text).Date != DateTime.Now.Date)
                    {
                        //dtpRecDate.Enabled = true;
                        DisplayMessage("Back date not allow for selected date!");
                        dtpRecDate.Focus();
                        return;
                    }
                }
                else
                {
                    //dtpRecDate.Enabled = true;
                    DisplayMessage("Back date not allow for selected date!");
                    dtpRecDate.Focus();
                    return;
                }

                if (_isRes == true)
                {
                    if ((txtRecType.Text == "ADVAN") || (txtRecType.Text == "VHREG") || (txtRecType.Text == "VHINS"))
                    {
                        foreach (ReptPickSerials line in _ResList)
                        {
                            ReptPickSerials _tempItem = new ReptPickSerials();
                            _tempItem = CHNLSVC.Inventory.GetAvailableSerIDInformation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), line.Tus_itm_cd, line.Tus_ser_1, string.Empty, string.Empty);

                            if (_tempItem.Tus_itm_cd == null)
                            {
                                DisplayMessage("Selected serial not available in inventory.Please check.");
                                return;
                            }
                        }
                    }
                }

                Decimal _needItem = 0;
                if (txtRecType.Text == "ADVAN")// Nadeeka 11-11-2015
                {
                    List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("RECITMNEED", "CHNL", BaseCls.GlbDefChannel);
                    if (para.Count > 0)
                    {
                        _needItem = para[0].Hsy_val;
                    }

                    //if (_needItem > 0 && dgvItem.Rows.Count == 0)
                    //{
                    //    DisplayMessage("Items must be enter for this advance receipt.");
                    //    return;
                    //}
                }

                //btnSave.Enabled = false;
                //setButtionEnable(false, btnSave, "confSave");
                SaveReceiptHeader();
            }

            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        protected void btn_add_ser_Click(object sender, EventArgs e)
        {
            try
            {
                decimal _valPd = 0;
                if (MultiView1.ActiveViewIndex == 0)
                {
                    LoadCachedObjects();
                    //LoadInvoiceType();
                    if (txtCusCode.Text == "CASH")
                    {
                        DisplayMessage("Adding items is not allowed for CASH Customer Code");
                        return;
                    }
                    if (txtRecType.Text == "ADVAN")
                    {
                        List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADREFMAXDT", "COM", Session["UserCompanyCode"].ToString());
                        if (para.Count > 0)
                        {
                            _valPd = para[0].Hsy_val;
                        }
                    }
                    dtpValidTill.Text = Convert.ToDateTime(dtpRecDate.Text.Trim()).Date.AddDays(Convert.ToDouble(_valPd)).Date.ToString("dd/MMM/yyyy");
                    LoadPriceDefaultValue();
                    MultiView1.ActiveViewIndex = 1;
                }
                else
                {
                    MultiView1.ActiveViewIndex = 0;
                }

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void btnRecType_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptType);
                DataTable result = CHNLSVC.CommonSearch.GetReceiptTypes(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "ReceiptType";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void btnDivision_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Division);
                DataTable result = CHNLSVC.CommonSearch.GetDivision(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Division";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void btnReceiptNo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRecType.Text))
                {
                    DisplayMessage("Please select receipt type.");
                    txtRecType.Focus();
                    return;
                }

                DateTime d1 = DateTime.Now;
                d1 = d1.AddMonths(-1);
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptDate);
                DataTable _result = new DataTable();
                if (!chkUnAllocated.Checked)
                {
                    _result = CHNLSVC.CommonSearch.GetReceiptsDate(SearchParams, null, null, d1, DateTime.Now);
                }
                else
                {
                    _result = CHNLSVC.CommonSearch.SEARCH_RECEIPT_UNALO(SearchParams, null, null, d1, DateTime.Now);
                }
                grdResult.DataSource = _result;
                grdResult.DataBind();
                txtTDate.Text = System.DateTime.Now.ToShortDateString();
                txtFDate.Text = d1.ToShortDateString();
                BindUCtrlDDLData(_result);
                lblvalue.Text = "ReceiptDate";
                ViewState["SEARCH"] = _result;
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
                selDatePanal(true);

                if (chkUnAllocated.Checked)
                {

                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4, err);
            }
        }

        protected void txtRecType_TextChanged(object sender, EventArgs e)
        {
            if (txtRecType.Text == "DEBT")
            {
                lbtnExcelUpload.Visible = true;
            }
            else lbtnExcelUpload.Visible = false;

            if (!string.IsNullOrEmpty(txtRecType.Text))
            {
                if (!CHNLSVC.Sales.IsValidReceiptType(Session["UserCompanyCode"].ToString(), txtRecType.Text.Trim()))
                {
                    DisplayMessage("Invalid receipt type.");
                    txtRecType.Text = "";
                    txtRecType.Focus();
                    return;
                }
                else
                {
                    chkOth.Visible = false;
                    txtOthSR.Visible = false;
                    btnOthSR.Visible = false;
                    txtengine.Enabled = true;
                    txtChasis.Enabled = true;
                    pnlExtraChg.Visible = false;
                    pnlDisposal.Visible = false;

                    if (txtRecType.Text == "DEBT" || txtRecType.Text == "SRTN" || txtRecType.Text == "SVAT" || txtRecType.Text == "DAJ")
                    {
                        txtInvoice.Text = "";
                        txtBalance.Text = "0.00";
                        txtPayment.Text = "0.00";
                        txtInvoice.Enabled = true;

                        radioButtonManual.Checked = false;
                        radioButtonManual.Enabled = true;

                        chkDel.Checked = false;
                        chkDel.Enabled = false;
                        gbItem.Visible = false;
                        gbInsu.Visible = false;
                        gbsettle.Visible = true;
                        chkOth.Visible = true;
                        txtOthSR.Visible = true;
                        btnOthSR.Visible = true;
                        txtGVCode.Text = "";
                        lblFrompg.Text = "";
                        lblPageCount.Text = "";
                        cmbGvBook.DataSource = new DataTable();
                        cmbGvBook.DataBind();
                        cmbTopg.DataSource = new DataTable();
                        cmbTopg.DataBind();
                        txtPgAmt.Text = "";
                        txtTotGvAmt.Text = "";
                        gbGVDet.Visible = false;

                        chkUnAllocated.Visible = true;
                        pnlDebtInvoices.Visible = false;
                        ClearCus_Data();
                        ClearSettle_Data();
                    }
                    else if (txtRecType.Text == "VHREG")
                    {
                        txtInvoice.Text = "";
                        txtBalance.Text = "0.00";
                        txtPayment.Text = "0.00";
                        lblExtraChg.Text = "0.00";
                        txtInvoice.Enabled = true;

                        radioButtonManual.Checked = false;
                        radioButtonManual.Enabled = true;

                        chkDel.Checked = false;
                        chkDel.Enabled = true;
                        gbItem.Visible = true;
                        gbInsu.Visible = false;
                        gbsettle.Visible = true;
                        txtGVCode.Text = "";
                        lblFrompg.Text = "";
                        lblPageCount.Text = "";
                        cmbGvBook.DataSource = new DataTable();
                        cmbGvBook.DataBind();
                        cmbTopg.DataSource = new DataTable();
                        cmbTopg.DataBind();
                        txtPgAmt.Text = "";
                        txtTotGvAmt.Text = "";
                        gbGVDet.Visible = false;
                        pnlExtraChg.Visible = true;
                        ClearCus_Data();
                        ClearSettle_Data();
                    }
                    else if (txtRecType.Text == "VHINS")
                    {
                        txtInvoice.Text = "";
                        txtBalance.Text = "0.00";
                        txtPayment.Text = "0.00";
                        txtInvoice.Enabled = true;

                        radioButtonManual.Checked = false;
                        radioButtonManual.Enabled = true;

                        chkDel.Checked = false;
                        chkDel.Enabled = true;
                        gbItem.Visible = true;
                        gbInsu.Visible = true;
                        gbsettle.Visible = true;
                        txtGVCode.Text = "";
                        lblFrompg.Text = "";
                        lblPageCount.Text = "";
                        cmbGvBook.DataSource = new DataTable();
                        cmbGvBook.DataBind();
                        cmbTopg.DataSource = new DataTable();
                        cmbTopg.DataBind();
                        txtPgAmt.Text = "";
                        txtTotGvAmt.Text = "";
                        gbGVDet.Visible = false;
                        ClearCus_Data();
                        ClearSettle_Data();
                        MasterOutsideParty _insCom = CHNLSVC.Sales.GetOutSidePartyDetails(null, "INS");
                        if (_insCom.Mbi_cd != null)
                        {
                            txtInsCom.Text = _insCom.Mbi_cd;
                        }
                        else
                        {
                            txtInsCom.Text = "";
                        }

                        InsuarancePolicy _insPol = CHNLSVC.Sales.GetInusPolicy(null);
                        if (_insPol.Svip_polc_cd != null)
                        {
                            txtInsPol.Text = _insPol.Svip_polc_cd;
                        }
                        else
                        {
                            txtInsPol.Text = "";
                        }

                    }
                    else if (txtRecType.Text == "ADINS")
                    {
                        txtInvoice.Text = "";
                        txtBalance.Text = "0.00";
                        txtPayment.Text = "0.00";
                        txtInvoice.Enabled = true;

                        radioButtonManual.Checked = false;
                        radioButtonManual.Enabled = true;

                        chkDel.Checked = false;
                        chkDel.Enabled = false;
                        gbItem.Visible = true;
                        gbInsu.Visible = true;
                        gbsettle.Visible = true;
                        txtengine.Enabled = true;
                        txtChasis.Enabled = true;
                        txtGVCode.Text = "";
                        lblFrompg.Text = "";
                        lblPageCount.Text = "";
                        cmbGvBook.DataSource = new DataTable();
                        cmbGvBook.DataBind();
                        cmbTopg.DataSource = new DataTable();
                        cmbTopg.DataBind();
                        txtPgAmt.Text = "";
                        txtTotGvAmt.Text = "";
                        gbGVDet.Visible = false;
                        ClearCus_Data();
                        ClearSettle_Data();
                        MasterOutsideParty _insCom = CHNLSVC.Sales.GetOutSidePartyDetails(null, "INS");
                        if (_insCom.Mbi_cd != null)
                        {
                            txtInsCom.Text = _insCom.Mbi_cd;
                        }
                        else
                        {
                            txtInsCom.Text = "";
                        }

                        InsuarancePolicy _insPol = CHNLSVC.Sales.GetInusPolicy(null);
                        if (_insPol.Svip_polc_cd != null)
                        {
                            txtInsPol.Text = _insPol.Svip_polc_cd;
                        }
                        else
                        {
                            txtInsPol.Text = "";
                        }

                    }
                    else if (txtRecType.Text == "ADVAN")
                    {
                        loadPrifixes();
                        txtInvoice.Text = "";
                        txtBalance.Text = "0.00";
                        txtPayment.Text = "0.00";
                        txtInvoice.Enabled = false;
                        gbItem.Visible = true;
                        chkDel.Checked = false;
                        chkDel.Enabled = false;
                        gbInsu.Visible = false;
                        gbsettle.Visible = true;

                        if (Session["UserCompanyCode"].ToString() == "AAL")
                        {
                            if (Session["UserDefProf"].ToString() != "500")
                            {
                                radioButtonManual.Checked = true;
                                radioButtonManual.Enabled = false;
                            }
                            else
                            {
                                radioButtonManual.Checked = false;
                                radioButtonManual.Enabled = true;
                            }
                        }
                        else
                        {

                            MasterProfitCenter _ctn = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                            if (_ctn != null)
                            {
                                if (_ctn.Mpc_chnl != "ELITE" && _ctn.Mpc_chnl != "RMSR" && _ctn.Mpc_chnl != "AOA_CH" && _ctn.Mpc_chnl != "CLEARENCE_SALES" && _ctn.Mpc_chnl != "APPLE" && _ctn.Mpc_chnl != "RAPS" && _ctn.Mpc_chnl != "RCLS" && _ctn.Mpc_oth_ref != "SYS")
                                {
                                    radioButtonManual.Checked = true;
                                    //   radioButtonManual.Enabled = false;
                                    txtManual.Text = "";        //kapila 6/4/2015
                                }

                            }

                        }
                        txtGVCode.Text = "";
                        lblFrompg.Text = "";
                        lblPageCount.Text = "";
                        cmbGvBook.DataSource = new DataTable();
                        cmbGvBook.DataBind();
                        cmbTopg.DataSource = new DataTable();
                        cmbTopg.DataBind();
                        txtPgAmt.Text = "";
                        txtTotGvAmt.Text = "";
                        gbGVDet.Visible = false;
                        ClearCus_Data();
                        ClearSettle_Data();
                        txtCusCode.Text = "CASH";
                    }
                    else if (txtRecType.Text == "GVISU")
                    {
                        ClearCus_Data();
                        ClearSettle_Data();
                        if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 10061))
                        {
                            chkAllowPromo.Checked = false;
                            chkAllowPromo.Visible = false;
                            chkGvFOC.Checked = false;
                            chkGvFOC.Visible = false;
                            dtGVExp.Visible = false;
                            lblGVExp.Visible = false;
                        }
                        else
                        {
                            chkAllowPromo.Checked = false;
                            chkAllowPromo.Visible = true;
                            chkGvFOC.Checked = false;
                            chkGvFOC.Visible = true;
                            dtGVExp.Visible = true;
                            lblGVExp.Visible = true;

                        }
                        txtGVCode.Text = "";
                        lblFrompg.Text = "";
                        lblPageCount.Text = "";
                        cmbGvBook.DataSource = new DataTable();
                        cmbGvBook.DataBind();
                        cmbTopg.DataSource = new DataTable();
                        cmbTopg.DataBind();
                        txtPgAmt.Text = "";
                        txtTotGvAmt.Text = "";
                        gbsettle.Visible = false;
                        gbGVDet.Visible = true;
                        txtCusCode.Text = "CASH";
                    }
                    else if (txtRecType.Text == "DISP")
                    {
                        pnlDisposal.Visible = true;
                        txtDisposalJob.Text = "";
                        gbsettle.Visible = false;
                    }
                    else
                    {
                        txtInvoice.Text = "";
                        txtBalance.Text = "0.00";
                        txtPayment.Text = "0.00";
                        txtInvoice.Enabled = false;
                        radioButtonManual.Checked = false;
                        radioButtonManual.Enabled = true;
                        chkDel.Checked = false;
                        chkDel.Enabled = false;
                        gbInsu.Visible = false;
                        gbItem.Visible = false;
                        gbsettle.Visible = true;
                        txtGVCode.Text = "";
                        lblFrompg.Text = "";
                        lblPageCount.Text = "";
                        cmbGvBook.DataSource = new DataTable();
                        cmbGvBook.DataBind();
                        cmbTopg.DataSource = new DataTable();
                        cmbTopg.DataBind();
                        txtPgAmt.Text = "";
                        txtTotGvAmt.Text = "";
                        gbGVDet.Visible = false;
                        ClearCus_Data();
                        ClearSettle_Data();
                    }

                    ucPayModes1.InvoiceType = txtRecType.Text.Trim();
                    ucPayModes1.LoadData();
                }

                MasterReceiptDivision _RecDiv = new MasterReceiptDivision();
                _RecDiv = CHNLSVC.Sales.GetDefRecDivision(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
                if (_RecDiv.Msrd_cd != null)
                {
                    txtDivision.Text = _RecDiv.Msrd_cd;
                }
                else
                {
                    txtDivision.Text = "";
                }
            }
        }

        protected void txtDivision_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDivision.Text))
            {
                if (!CHNLSVC.Sales.IsValidDivision(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtDivision.Text.Trim()))
                {
                    DisplayMessage("Invalid division.");
                    txtDivision.Text = "";
                    txtDivision.Focus();
                    return;
                }
            }
        }

        protected void radioButtonManual_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonManual.Checked == true)
            {
                txtManual.Enabled = true;
                Int32 _NextNo = CHNLSVC.Inventory.GetNextManualDocNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "MDOC_AVREC");
                if (_NextNo != 0)
                {
                    txtManual.Text = _NextNo.ToString();
                }
                else
                {
                    txtManual.Text = "";
                }
            }

            else
            {
                if (BaseCls.GlbIsManChkLoc == true)
                {
                    txtManual.Text = "";
                }
                else
                {
                    txtManual.Text = "";
                    txtManual.Enabled = true;
                }
            }
            loadPrifixes();
        }

        protected void radioButtonSystem_CheckedChanged(object sender, EventArgs e)
        {
            loadPrifixes();
        }

        protected void txtManual_TextChanged(object sender, EventArgs e)
        {
            if (_IsRecall == true)
            {
                return;
            }
            if (radioButtonManual.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtManual.Text))
                {
                    if (!IsNumeric(txtManual.Text))
                    {
                        DisplayMessage("Invalid manual document number.");
                        txtManual.Text = "";
                        txtManual.Focus();
                        return;
                    }
                    if (!string.IsNullOrEmpty(comboBoxPrefix.Text))
                    {

                        Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument_prefix(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "MDOC_AVREC", Convert.ToInt32(txtManual.Text), comboBoxPrefix.Text);
                        if (_IsValid == false)
                        {
                            DisplayMessage("Invalid manual document number.");
                            txtManual.Text = "";
                            txtManual.Focus();
                            return;
                        }
                    }
                }
                else
                {
                }
            }

            if (radioButtonSystem.Checked == true)
            {
                if (!string.IsNullOrEmpty(txtManual.Text))
                {
                    if (!IsNumeric(txtManual.Text))
                    {
                        DisplayMessage("Invalid manual document number.");
                        txtManual.Text = "";
                        txtManual.Focus();
                        return;
                    }
                    if (!string.IsNullOrEmpty(comboBoxPrefix.Text))
                    {
                        //2016-08-02 comment for Account departments 
                        //Boolean _IsValid = CHNLSVC.Inventory.IsValidManualDocument_prefix(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "SDOC_AVREC", Convert.ToInt32(txtManual.Text), comboBoxPrefix.Text);
                        //if (_IsValid == false)
                        //{
                        //    DisplayMessage("Invalid prefix.");
                        //    txtManual.Text = "";
                        //    txtManual.Focus();
                        //    return;
                        //}
                    }
                }
                else
                {

                }
            }
        }

        protected void txtCusCode_TextChanged(object sender, EventArgs e)
        {
            LoadCustomerDetails();
        }

        protected void btnCode_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                DataTable result = CHNLSVC.CommonSearch.GetCustomerCommon(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "CustomerCommon";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void btnCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                Session["ShowCU"] = "Y";
                Session["_isFromOther"] = "true";
                mpCustomer.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void btnCloseCustomer_Click(object sender, EventArgs e)
        {
            Session["ShowCU"] = null;
            mpCustomer.Hide();
        }

        protected void txtNIC_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNIC.Text))
            {
                if (!IsValidNIC(txtNIC.Text.Trim()))
                {
                    DisplayMessage("Invalid NIC number.");
                    txtNIC.Text = "";
                    txtNIC.Focus();
                    return;
                }
            }
        }

        protected void txtMobile_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMobile.Text))
            {
                if (!IsValidMobileOrLandNo(txtMobile.Text.Trim()))
                {
                    DisplayMessage("Invalid mobile number.");
                    txtMobile.Text = "";
                    txtMobile.Focus();
                    return;
                }
            }
        }

        protected void cmbDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetProvince();
        }

        protected void btnInvoiceSearch_Click(object sender, EventArgs e)
        {

            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;

            if (txtRecType.Text == "DEBT" || txtRecType.Text == "SRTN" || txtRecType.Text == "SVAT" || txtRecType.Text == "DAJ")
            {

                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    DisplayMessage("Please select customer.");
                    txtCusCode.Focus();
                    return;
                }

                DataTable result = null;

                if (chkOth.Checked == true)
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OutstandingInvOth);
                    result = CHNLSVC.CommonSearch.GetOutstandingInvoiceweb(SearchParams, null, null);
                    lblvalue.Text = "OutstandingInvOth";
                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OutstandingInv);
                    result = CHNLSVC.CommonSearch.GetOutstandingInvoice(SearchParams, null, null);
                    lblvalue.Text = "OutstandingInv";
                }

                grdResult.DataSource = result;
                grdResult.DataBind();
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
            else if ((txtRecType.Text == "VHREG") || (txtRecType.Text == "VHINS") || (txtRecType.Text == "ADINS"))
            {
                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesInvoice);
                    DataTable result = CHNLSVC.CommonSearch.GetAllInvoiceSearchData(SearchParams, null, null);
                    lblvalue.Text = "SalesInvoice";
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    mpSearchAdance.Show();
                    txtSearchbyword.Text = "";
                    selDatePanal(false);

                }
                else
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceByCus);
                    DataTable result = CHNLSVC.CommonSearch.GetInvoicebyCustomer(SearchParams, null, null);
                    lblvalue.Text = "InvoiceByCus";
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    mpSearchAdance.Show();
                    txtSearchbyword.Text = "";
                    selDatePanal(false);
                }
            }
        }

        protected void txtInvoice_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInvoice.Text))
            {
                if (txtRecType.Text == "DEBT" || txtRecType.Text == "SRTN" || txtRecType.Text == "SVAT" || txtRecType.Text == "DAJ")
                {
                    if (string.IsNullOrEmpty(txtCusCode.Text))
                    {
                        DisplayMessage("Please select customer first.");
                        txtCusCode.Text = "";
                        txtCusCode.Focus();
                        return;
                    }

                    //check valid invoice
                    List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                    if (chkOth.Checked == true)
                        _invHdr = CHNLSVC.Sales.GetPendingInvoicesweb(Session["UserCompanyCode"].ToString(), txtOthSR.Text, txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", dtpRecDate.Text.Trim(), dtpRecDate.Text.Trim());
                    else
                        _invHdr = CHNLSVC.Sales.GetPendingInvoicesweb(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", dtpRecDate.Text.Trim(), dtpRecDate.Text.Trim());

                    if (_invHdr == null || _invHdr.Count == 0)
                    {
                        DisplayMessage("Invalid invoice number.");
                        txtInvoice.Text = "";
                        txtInvoice.Focus();
                        return;
                    }

                    foreach (InvoiceHeader _tmpInv in _invHdr)
                    {
                        if (_tmpInv.Sah_stus == "C" || _tmpInv.Sah_stus == "R")
                        {
                            DisplayMessage("Selected invoice is canceled or reversed.");
                            txtInvoice.Text = "";
                            txtInvoice.Focus();
                            return;
                        }
                    }

                    if (chkOth.Checked == true)
                        txtBalance.Text = Convert.ToDecimal(CHNLSVC.Sales.GetOutInvAmtweb(Session["UserCompanyCode"].ToString(), txtOthSR.Text, txtCusCode.Text, txtInvoice.Text)).ToString("n");
                    else
                        txtBalance.Text = Convert.ToDecimal(CHNLSVC.Sales.GetOutInvAmtweb(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text, txtInvoice.Text)).ToString("n");

                    if (Convert.ToDecimal(txtBalance.Text) <= 0)
                    {
                        DisplayMessage("Cannot find outstanding amount.");
                        txtInvoice.Text = "";
                        txtBalance.Text = "0.00";
                        txtInvoice.Focus();
                    }
                }
                else if ((txtRecType.Text == "VHREG") || (txtRecType.Text == "VHINS") || (txtRecType.Text == "ADINS"))
                {
                    txtBalance.Text = "0.00";
                    txtItem.Text = "";
                    txtengine.Text = "";
                    txtChasis.Text = "";
                    txtPayment.Text = "0.00";
                    lblExtraChg.Text = "0.00";
                    //check valid invoice
                    List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                    _invHdr = CHNLSVC.Sales.GetPendingInvoices(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text.Trim(), txtInvoice.Text.Trim(), "C", dtpRecDate.Text.Trim(), dtpRecDate.Text.Trim());

                    foreach (InvoiceHeader _tempInv in _invHdr)
                    {
                        if (_tempInv.Sah_inv_no == null)
                        {
                            DisplayMessage("Invalid invoice number.");
                            txtInvoice.Text = "";
                            _accNo = "";
                            _invLine = 0;
                            txtInvoice.Focus();
                            return;
                        }
                        if (_tempInv.Sah_stus == "C" || _tempInv.Sah_stus == "R")
                        {
                            DisplayMessage("Selected invoice is canceled or reversed.");
                            txtInvoice.Text = "";
                            txtInvoice.Focus();
                            return;
                        }
                        if (string.IsNullOrEmpty(txtCusCode.Text))
                        {
                            txtCusCode.Text = _tempInv.Sah_cus_cd;
                            LoadCustomerDetails();
                        }
                        _invType = _tempInv.Sah_inv_tp;
                        _accNo = _tempInv.Sah_anal_2;
                    }
                }
                else
                {
                    txtBalance.Text = "0.00";
                }
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear_Data();
        }

        protected void chkOth_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOth.Checked == true)
            {
                txtOthSR.Enabled = true;
                btnOthSR.Enabled = true;
            }
            else
            {
                txtOthSR.Enabled = false;
                btnOthSR.Enabled = false;
            }
        }

        protected void txtOthSR_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOthSR.Text))
            {
                if (Session["UserDefProf"].ToString() == txtOthSR.Text)
                {
                    DisplayMessage("Same profit center cannot be selected.");
                    txtOthSR.Focus();
                    return;
                }
                Boolean _IsValid = CHNLSVC.Sales.IsvalidPC(Session["UserCompanyCode"].ToString(), txtOthSR.Text);
                if (_IsValid == false)
                {
                    DisplayMessage("Invalid Profit Center.");
                    txtOthSR.Focus();
                    return;
                }
            }
        }

        protected void btnOthSR_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
                DataTable result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "AllProfitCenters";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void txtItem_TextChanged(object sender, EventArgs e)
        {
            pnlItemAlloc.Visible = false;
            Boolean _isLease = false;
            Boolean _isAllowEdit = false;
            decimal _invRegAllowQty = 0;
            decimal _invRegAllowVal = 0;
            string _invTp = "";
            string _cusTp = "";
            decimal _invItmQty = 0;
            decimal _invItmVal = 0;
            string _pbook = "";
            string _plvl = "";
            string _sch = "";
            string _type = "";
            string _value = "";
            Boolean _regFound = false;
            Boolean _isInsuFound = false;

            if (!string.IsNullOrEmpty(txtItem.Text.ToUpper()))
            {
                MasterItem _itemList = new MasterItem();
                _itemList = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().ToUpper().Trim());
                if (_itemList == null)
                {
                    DisplayMessage("Invalid item selected.");
                    txtItem.Text = "";
                    txtItem.Focus();
                    return;
                }
                if (_itemList.Mi_cd == null)
                {
                    DisplayMessage("Invalid item selected.");
                    txtItem.Focus();
                    return;
                }

                txtAllocQty.Text = "0";
                txtRecQty.Text = "0";
                txtFreeQty.Text = "0";

                if (txtRecType.Text == "ADVAN")
                {

                    DataTable _alloc = CHNLSVC.Inventory.GetItemAllocationDet(txtItem.Text.ToUpper().Trim());
                    if (_alloc != null)
                    {
                        if (_alloc.Rows.Count > 0)
                        {
                            pnlItemAlloc.Visible = true;
                            foreach (DataRow r in _alloc.Rows)
                            {
                                txtAllocQty.Text = Convert.ToString(Convert.ToDecimal(r["SSA_QTY"]));
                                txtRecQty.Text = Convert.ToString(Convert.ToDecimal(r["SSA_REC_QTY"]));
                                txtFreeQty.Text = Convert.ToString(Convert.ToDecimal(r["SSA_QTY"]) - Convert.ToDecimal(r["SSA_REC_QTY"]));
                            }
                        }
                        else
                        {
                            pnlItemAlloc.Visible = false;
                        }
                    }
                }

                if ((txtRecType.Text == "VHREG") || (txtRecType.Text == "VHINS") || (txtRecType.Text == "ADINS"))
                {
                    InvoiceItem _invItem = new InvoiceItem();
                    _invItem = CHNLSVC.Sales.GetPendingInvoiceItemsByItem(txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim());

                    if (_invItem != null)
                    {
                        if (_invItem.Sad_inv_no != null)
                        {
                            if (_invItem.Sad_do_qty > 0)
                            {
                                chkDel.Checked = true;
                            }
                            else
                            {
                                chkDel.Checked = false;
                            }
                            _invLine = _invItem.Sad_itm_line;
                        }
                        else
                        {
                            DisplayMessageJS("Can't find such item in selected invoice.");
                            txtItem.Focus();
                            return;
                        }
                    }
                    else
                    {
                        DisplayMessageJS("Can't find such item in selected invoice.");
                        txtItem.Focus();
                        return;
                    }


                    if (txtRecType.Text == "VHREG")
                    {
                        if (_itemList.Mi_need_reg == true)
                        {
                            VehicalRegistrationDefnition _vehDef = new VehicalRegistrationDefnition();
                            _isLease = CHNLSVC.Sales.IsCheckLeaseCom(txtInvoice.Text.Trim(), Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "LEASE");

                            DataTable _invRegDet = CHNLSVC.Sales.GetRegInvDet(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoice.Text.Trim());

                            if (_invRegDet.Rows.Count > 0)
                            {
                                _invRegAllowQty = Convert.ToDecimal(_invRegDet.Rows[0]["totqty"]);

                                if (_invRegAllowQty <= 0)
                                {
                                    DisplayMessageJS("Cannot find valid invoice qty.");
                                    return;
                                }

                                _invRegAllowVal = Convert.ToDecimal(_invRegDet.Rows[0]["totval"]);
                            }
                            else
                            {
                                DisplayMessageJS("Cannot find valid invoice details to proceed.");
                                return;
                            }

                            DataTable _invItmRegDet = CHNLSVC.Sales.GetRegInvItmDet(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoice.Text.Trim(), _invLine, txtItem.Text.ToUpper().Trim());

                            if (_invItmRegDet.Rows.Count > 0)
                            {

                                _invItmQty = Convert.ToDecimal(_invItmRegDet.Rows[0]["tot_qty"]);

                                if (_invItmQty <= 0)
                                {
                                    DisplayMessageJS("Cannot find valid invoice qty.");
                                    return;
                                }

                                _invItmVal = Convert.ToDecimal(_invItmRegDet.Rows[0]["tot_val"]);
                                _invTp = _invItmRegDet.Rows[0]["sah_inv_tp"].ToString();
                                _cusTp = _invItmRegDet.Rows[0]["mbe_cate"].ToString();
                                _pbook = _invItmRegDet.Rows[0]["sad_pbook"].ToString();
                                _plvl = _invItmRegDet.Rows[0]["sad_pb_lvl"].ToString();
                            }

                            //check whether all item qty generate registrations
                            List<VehicalRegistration> _preReg = new List<VehicalRegistration>();
                            _preReg = CHNLSVC.General.GetVehiclesByInvoiceNo(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtInvoice.Text.Trim());

                            if (_preReg != null)
                            {
                                if (_preReg.Count > 0)
                                {
                                    decimal _count = _preReg.Where(x => x.P_srvt_itm_cd == txtItem.Text.ToUpper().Trim() && x.P_svrt_prnt_stus != 2).Count();
                                    _count = _count + 1;

                                    if (_count > _invItmQty)
                                    {
                                        DisplayMessageJS("Registration receipt already available for selected invoice item.");
                                        txtInvoice.Text = "";
                                        txtItem.Text = "";
                                        txtInvoice.Focus();
                                        return;
                                    }
                                }
                            }

                            if (_isLease == false)
                            {
                                if (_invTp == "HS")
                                {
                                    // hiresale scheme
                                    if (!string.IsNullOrEmpty(_accNo))
                                    {
                                        _HpAccount = new HpAccount();
                                        _HpAccount = CHNLSVC.Sales.GetHP_Account_onAccNo(_accNo);

                                        _sch = _HpAccount.Hpa_sch_cd;
                                    }
                                    else
                                    {
                                        DisplayMessageJS("Hire sales invoice cannot get account details.");
                                        return;
                                    }

                                    List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                                    if (_Saleshir.Count > 0)
                                    {
                                        foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                                        {
                                            _regFound = false;
                                            _type = _one.Mpi_cd;
                                            _value = _one.Mpi_val;

                                            _vehDef = CHNLSVC.Sales.GetVehRegAmtDirectNew(_type, _value, _invTp, txtItem.Text.ToUpper().Trim(), Convert.ToDateTime(dtpRecDate.Text).Date, _sch, _invItmQty, _invItmVal, _pbook, _plvl, "");

                                            if (_vehDef.Svrd_itm != null)
                                            {
                                                txtBalance.Text = _vehDef.Svrd_val.ToString("0.00");
                                                txtPayment.Text = _vehDef.Svrd_val.ToString("0.00");
                                                _regAmt = _vehDef.Svrd_claim_val;
                                                _regFound = true;
                                                goto L1;
                                            }
                                            else
                                            {
                                                _vehDef = CHNLSVC.Sales.GetVehRegAmtDirectNew(_type, _value, _invTp, txtItem.Text.ToUpper().Trim(), Convert.ToDateTime(dtpRecDate.Text).Date, null, _invItmQty, _invItmVal, _pbook, _plvl, "");

                                                if (_vehDef.Svrd_itm != null)
                                                {
                                                    txtBalance.Text = _vehDef.Svrd_val.ToString("0.00");
                                                    txtPayment.Text = _vehDef.Svrd_val.ToString("0.00");
                                                    _regAmt = _vehDef.Svrd_claim_val;
                                                    _regFound = true;
                                                    goto L1;
                                                }
                                                //else
                                                //{
                                                //    DisplayMessageJS("Registration amount definitions not set.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //    txtItem.Text = "";
                                                //    txtBalance.Text = "0.00";
                                                //    txtPayment.Text = "0.00";
                                                //    _regAmt = 0;
                                                //    txtItem.Focus();
                                                //    return;
                                                //}
                                            }
                                        }
                                    }
                                    else
                                    {
                                        DisplayMessageJS("Hierarchy not define.");
                                        return;
                                    }


                                    if (_regFound == false)
                                    {
                                        DisplayMessageJS("Registration amount definitions not set.");
                                        txtItem.Text = "";
                                        txtBalance.Text = "0.00";
                                        txtPayment.Text = "0.00";
                                        lblExtraChg.Text = "0.00";
                                        _regAmt = 0;
                                        txtItem.Focus();
                                        return;
                                    }

                                L1: Int32 i = 1;
                                }
                                else
                                {

                                    List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                                    if (_Saleshir.Count > 0)
                                    {
                                        foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                                        {
                                            _regFound = false;
                                            _type = _one.Mpi_cd;
                                            _value = _one.Mpi_val;

                                            //_vehDef = CHNLSVC.Sales.GetVehRegAmtDirect(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "LEASE", txtItem.Text.Trim(), Convert.ToDateTime(dtpRecDate.Text).Date);
                                            _vehDef = CHNLSVC.Sales.GetVehRegAmtDirectNew(_type, _value, _invTp, txtItem.Text.ToUpper().Trim(), Convert.ToDateTime(dtpRecDate.Text).Date, null, _invItmQty, _invItmVal, _pbook, _plvl, "");

                                            if (_vehDef.Svrd_itm != null)
                                            {
                                                txtBalance.Text = _vehDef.Svrd_val.ToString("0.00");
                                                txtPayment.Text = _vehDef.Svrd_val.ToString("0.00");
                                                _regAmt = _vehDef.Svrd_claim_val;
                                                _regFound = true;
                                                goto L3;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        DisplayMessageJS("Hierarchy not define.");
                                        return;
                                    }

                                    if (_regFound == false)
                                    {
                                        DisplayMessageJS("Registration amount definitions not set.");
                                        txtItem.Text = "";
                                        txtBalance.Text = "0.00";
                                        txtPayment.Text = "0.00";
                                        lblExtraChg.Text = "0.00";
                                        _regAmt = 0;
                                        txtItem.Focus();
                                        return;
                                    }
                                L3: Int32 i = 1;
                                    //_vehDef = CHNLSVC.Sales.GetVehRegDef(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim(), txtItem.Text.Trim(), Convert.ToDateTime(dtpRecDate.Text).Date);

                                    //if (_vehDef.Svrd_itm != null)
                                    //{
                                    //    txtBalance.Text = _vehDef.Svrd_val.ToString("0.00");
                                    //    txtPayment.Text = _vehDef.Svrd_val.ToString("0.00");
                                    //    _regAmt = _vehDef.Svrd_claim_val;
                                    //}
                                    //else
                                    //{

                                    //    DisplayMessageJS("Registration amount definitions not set.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    txtItem.Text = "";
                                    //    txtBalance.Text = "0.00";
                                    //    txtPayment.Text = "0.00";
                                    //    _regAmt = 0;
                                    //    txtItem.Focus();
                                    //    return;
                                    //}
                                }
                            }
                            else if (_isLease == true)
                            {

                                List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                                if (_Saleshir.Count > 0)
                                {
                                    foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                                    {
                                        _regFound = false;
                                        _type = _one.Mpi_cd;
                                        _value = _one.Mpi_val;

                                        //_vehDef = CHNLSVC.Sales.GetVehRegAmtDirect(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "LEASE", txtItem.Text.Trim(), Convert.ToDateTime(dtpRecDate.Text).Date);
                                        _vehDef = CHNLSVC.Sales.GetVehRegAmtDirectNew(_type, _value, "LEASE", txtItem.Text.ToUpper().Trim(), Convert.ToDateTime(dtpRecDate.Text).Date, null, _invItmQty, _invItmVal, _pbook, _plvl, "");

                                        if (_vehDef.Svrd_itm != null)
                                        {
                                            txtBalance.Text = _vehDef.Svrd_val.ToString("0.00");
                                            txtPayment.Text = _vehDef.Svrd_val.ToString("0.00");
                                            _regAmt = _vehDef.Svrd_claim_val;
                                            _regFound = true;
                                            goto L2;
                                        }
                                    }
                                }
                                else
                                {
                                    DisplayMessageJS("Hirarchy not define.");
                                    return;
                                }

                                if (_regFound == false)
                                {
                                    DisplayMessageJS("Registration amount definitions not set for leasing company.");
                                    txtItem.Text = "";
                                    txtBalance.Text = "0.00";
                                    txtPayment.Text = "0.00";
                                    lblExtraChg.Text = "0.00";
                                    _regAmt = 0;
                                    txtItem.Focus();
                                    return;
                                }
                            L2: Int32 i = 1;
                            }


                            //check registartion amount edit profit center
                            _isAllowEdit = CHNLSVC.Sales.IsCheckAllowFunction(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtRecType.Text.Trim(), "ALWEDIT");
                            if (_isAllowEdit == true)
                            {
                                txtBalance.ReadOnly = false;
                                txtBalance.Enabled = true;
                            }
                            else
                            {
                                txtBalance.ReadOnly = true;
                                txtBalance.Enabled = false;
                            }
                        }
                        else
                        {
                            DisplayMessageJS("This item is not allow to registration process.");
                            txtItem.Text = "";
                            txtItem.Focus();
                            return;
                        }
                    }
                    else if (txtRecType.Text == "VHINS")
                    {
                        if (_itemList.Mi_need_insu == false)
                        {
                            txtItem.Focus();
                            txtItem.Text = "";
                            DisplayMessageJS("This item is not allow to insurance process");
                            return;
                        }

                        Int32 _HpTerm = 0;

                        if ((!string.IsNullOrEmpty(txtInsPol.Text)) && (!string.IsNullOrEmpty(txtInsCom.Text)))
                        {

                            InsuarancePolicy _insuPolicy = new InsuarancePolicy();
                            _insuPolicy = CHNLSVC.Sales.GetInusPolicy(txtInsPol.Text.Trim());

                            if (_insuPolicy.Svip_polc_cd == null)
                            {
                                DisplayMessageJS("Invalid insurance policy.");
                                txtInsPol.Text = "";
                                txtInsPol.Focus();
                                return;
                            }

                            MasterOutsideParty _OutPartyDetails = new MasterOutsideParty();
                            _OutPartyDetails = CHNLSVC.Sales.GetOutSidePartyDetails(txtInsCom.Text.Trim(), "INS");

                            if (_OutPartyDetails.Mbi_cd == null)
                            {
                                DisplayMessageJS("Invalid insurance company.");
                                txtInsCom.Text = "";
                                txtInsCom.Focus();
                                return;
                            }

                            txtInsCom.Text = _OutPartyDetails.Mbi_cd;
                            txtInsPol.Text = _insuPolicy.Svip_polc_cd;
                            MasterVehicalInsuranceDefinitionNew _vehIns = new MasterVehicalInsuranceDefinitionNew();
                            InvoiceItem _invItm = new InvoiceItem();

                            //Get invoice details
                            string _pBook = "";
                            string _pLvl = "";
                            string _promoCd = "";
                            decimal _itmVal = 0;
                            _isInsuFound = false;

                            _invItm = CHNLSVC.Sales.GetInvDetByLine(txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), _invLine);
                            if (_invItm.Sad_itm_cd == null)
                            {
                                DisplayMessageJS("Cannot find invoice item details.");
                                txtItem.Text = "";
                                txtItem.Focus();
                                return;
                            }
                            else
                            {
                                _pBook = _invItm.Sad_pbook;
                                _pLvl = _invItm.Sad_pb_lvl;
                                _promoCd = _invItm.Sad_promo_cd;
                                _itmVal = _invItm.Sad_tot_amt / _invItem.Sad_qty;
                            }



                            List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                            string _Subchannel = "";
                            string _typeSubChnl = "SCHNL";

                            string _Mainchannel = "";
                            string _typeMainChanl = "CHNL";

                            string _Pctype = "PC";
                            string _typePc = Session["UserDefProf"].ToString();


                            if (_Saleshir.Count > 0)
                            {
                                _Subchannel = (from _lst in _Saleshir
                                               where _lst.Mpi_cd == "SCHNL"
                                               select _lst.Mpi_val).ToList<string>()[0];


                                _Mainchannel = (from _lst in _Saleshir
                                                where _lst.Mpi_cd == "CHNL"
                                                select _lst.Mpi_val).ToList<string>()[0];



                                if (_accNo != null && _accNo != "")
                                {
                                    _HpAccount = new HpAccount();
                                    _HpAccount = CHNLSVC.Sales.GetHP_Account_onAccNo(_accNo);

                                    _HpTerm = _HpAccount.Hpa_term;

                                    if (_HpTerm < 12)
                                    {
                                        chkAnnual.Checked = false;
                                        chkAnnual.Visible = true;
                                    }
                                    else
                                    {
                                        chkAnnual.Checked = false;
                                        chkAnnual.Visible = false;
                                    }


                                    if ((_HpTerm + 3) <= 3)
                                    {
                                        _HpTerm = 3;
                                    }
                                    else if ((_HpTerm + 3) <= 6)
                                    {
                                        _HpTerm = 6;
                                    }
                                    else if ((_HpTerm + 3) <= 9)
                                    {
                                        _HpTerm = 9;
                                    }
                                    else
                                    {
                                        _HpTerm = 12;
                                    }

                                    //_vehIns = CHNLSVC.Sales.GetVehInsDef(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim(), txtItem.Text.Trim(), Convert.ToDateTime(dtpRecDate.Text).Date, Session["UserDefProf"].ToString(), txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _HpTerm);

                                    if (!string.IsNullOrEmpty(_promoCd))
                                    {
                                        //check serial + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }
                                        //check pc + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //check sub Channel + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //Check channel + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //check serial
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //check pc
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //check sub Channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //Check channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }
                                    }
                                    else
                                    {
                                        //check serial
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }
                                        //check pc
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //check sub Channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //Check channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }
                                    }
                                }
                                else
                                {
                                    _HpTerm = 12;
                                    // _vehIns = CHNLSVC.Sales.GetVehInsDef(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), Convert.ToDateTime(dtpRecDate.Text).Date, Session["UserDefProf"].ToString(), txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _HpTerm);
                                    //_vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), null, null, null, null, null, null, 12, null, null, Convert.ToDateTime(dtpRecDate.Text).Date, 25, 25, null);
                                    if (!string.IsNullOrEmpty(_promoCd))
                                    {
                                        //check serial + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }
                                        //check pc + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //check sub Channel + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //Check channel + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //check serial
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }
                                        //check pc
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //check sub Channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //Check channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }
                                    }
                                    else
                                    {
                                        //check serial
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }
                                        //check pc
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //check sub Channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }

                                        //Check channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L55;
                                        }
                                    }
                                }



                                //if (_vehIns.Ins_com_cd != null)
                                //{
                                //    txtBalance.Text = _vehIns.Value.ToString("0.00");
                                //    txtPayment.Text = _vehIns.Value.ToString("0.00");
                                //}
                                //else
                                //{
                                //    DisplayMessageJS("Insuarance amount definitions not set for the term " + _HpTerm, "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    txtBalance.Text = "0.00";
                                //    txtPayment.Text = "0.00";
                                //    txtItem.Text = "";
                                //    txtItem.Focus();
                                //    return;
                                //}

                            }
                            else
                            {
                                DisplayMessageJS("Profit center hierarchy not set.");
                                txtBalance.Text = "0.00";
                                txtPayment.Text = "0.00";
                                lblExtraChg.Text = "0.00";
                                return;
                            }

                        }

                        if (_isInsuFound == false)
                        {
                            DisplayMessageJS("Cannot find insurance definition for the term " + _HpTerm);
                            txtBalance.Text = "0.00";
                            txtPayment.Text = "0.00";
                            lblExtraChg.Text = "0.00";
                            txtInsCom.Text = "";
                            txtInsPol.Text = "";
                            return;
                        }

                    }
                    else if (txtRecType.Text == "ADINS")
                    {
                        if ((!string.IsNullOrEmpty(txtInsPol.Text)) && (!string.IsNullOrEmpty(txtInsCom.Text)))
                        {

                            InsuarancePolicy _insuPolicy = new InsuarancePolicy();
                            _insuPolicy = CHNLSVC.Sales.GetInusPolicy(txtInsPol.Text.Trim());

                            if (_insuPolicy.Svip_polc_cd == null)
                            {
                                DisplayMessageJS("Invalid insurance policy.");
                                txtInsPol.Text = "";
                                txtInsPol.Focus();
                                return;
                            }

                            MasterOutsideParty _OutPartyDetails = new MasterOutsideParty();
                            _OutPartyDetails = CHNLSVC.Sales.GetOutSidePartyDetails(txtInsCom.Text.Trim(), "INS");

                            if (_OutPartyDetails.Mbi_cd == null)
                            {
                                DisplayMessageJS("Invalid insurance company.");
                                txtInsCom.Text = "";
                                txtInsCom.Focus();
                                return;
                            }

                            txtInsCom.Text = _OutPartyDetails.Mbi_cd;
                            txtInsPol.Text = _insuPolicy.Svip_polc_cd;
                            MasterVehicalInsuranceDefinitionNew _vehIns = new MasterVehicalInsuranceDefinitionNew();
                            InvoiceItem _invItm = new InvoiceItem();

                            //Get invoice details
                            string _pBook = "";
                            string _pLvl = "";
                            string _promoCd = "";
                            decimal _itmVal = 0;
                            string _cat1 = "";
                            string _cat2 = "";
                            string _brand = "";
                            _isInsuFound = false;

                            _invItm = CHNLSVC.Sales.GetInvDetByLine(txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), _invLine);
                            if (_invItm.Sad_itm_cd == null)
                            {
                                DisplayMessageJS("Cannot find invoice item details.");
                                txtItem.Text = "";
                                txtItem.Focus();
                                return;
                            }
                            else
                            {
                                _itemList = new MasterItem();
                                _itemList = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().Trim());

                                _pBook = _invItm.Sad_pbook;
                                _pLvl = _invItm.Sad_pb_lvl;
                                _promoCd = _invItm.Sad_promo_cd;
                                _itmVal = _invItm.Sad_tot_amt / _invItem.Sad_qty;

                                if (_itemList.Mi_cd == null)
                                {
                                    DisplayMessageJS("Cannot find item details.");
                                    txtItem.Text = "";
                                    txtItem.Focus();
                                    return;
                                }
                                else
                                {
                                    _cat1 = _itemList.Mi_cate_1;
                                    _cat2 = _itemList.Mi_cate_2;
                                    _brand = _itemList.Mi_brand;
                                }
                            }



                            List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                            string _Subchannel = "";
                            string _typeSubChnl = "SCHNL";

                            string _Mainchannel = "";
                            string _typeMainChanl = "CHNL";

                            string _Pctype = "PC";
                            string _typePc = Session["UserDefProf"].ToString();


                            if (_Saleshir.Count > 0)
                            {
                                _Subchannel = (from _lst in _Saleshir
                                               where _lst.Mpi_cd == "SCHNL"
                                               select _lst.Mpi_val).ToList<string>()[0];


                                _Mainchannel = (from _lst in _Saleshir
                                                where _lst.Mpi_cd == "CHNL"
                                                select _lst.Mpi_val).ToList<string>()[0];


                                //check pc
                                // check item + price book + price level
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + pb + plvl
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + brand + pb + plvl
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + cat2 + pb + plvl
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + cat2 + brand + pb + plvl
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                // check item
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + brand 
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + cat2 
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + cat2 + brand
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }


                                //check sub Channel
                                // check item + price book + price level
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + pb + plvl
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + brand + pb + plvl
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + cat2 + pb + plvl
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + cat2 + brand + pb + plvl
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                // check item
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + brand 
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + cat2 
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + cat2 + brand
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }

                                //Check channel
                                // check item + price book + price level
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + pb + plvl
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + brand + pb + plvl
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + cat2 + pb + plvl
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + cat2 + brand + pb + plvl
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, _pBook, _pLvl);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                // check item
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + brand 
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + cat2 
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                                //check cat1 + cat2 + brand
                                _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, null, null);

                                if (_vehIns.Svid_com != null)
                                {
                                    if (_vehIns.Svid_is_rt == 1)
                                    {
                                        txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }
                                    else
                                    {
                                        txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                        txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                        _isInsuFound = true;
                                        goto L55;
                                    }

                                }
                            }
                            else
                            {
                                DisplayMessageJS("Profit center hierarchy not set.");
                                txtBalance.Text = "0.00";
                                txtPayment.Text = "0.00";
                                lblExtraChg.Text = "0.00";
                                return;
                            }

                            if (_isInsuFound == false)
                            {
                                DisplayMessageJS("Cannot find insurance definition");
                                //DisplayMessageJS("Cannot find insurance definition.");
                                txtBalance.Text = "0.00";
                                txtPayment.Text = "0.00";
                                lblExtraChg.Text = "0.00";
                                txtInsCom.Text = "";
                                txtInsPol.Text = "";
                                return;
                            }
                        }
                    }
                L55: int I = 0;
                }
            }
        }

        protected void btnItem_Click(object sender, EventArgs e)
        {
            if ((txtRecType.Text == "VHREG") || (txtRecType.Text == "VHINS") || (txtRecType.Text == "ADINS"))
            {
                if (string.IsNullOrEmpty(txtInvoice.Text))
                {
                    DisplayMessage("Please select related invoice.");
                    txtInvoice.Focus();
                    return;
                }

                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
                DataTable result = CHNLSVC.CommonSearch.GetInvoiceItem(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "InvoiceItems";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
            else if (txtRecType.Text == "ADVAN")
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "Item";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
        }

        protected void txtengine_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtengine.Text))
            {
                if ((txtRecType.Text == "VHREG") || (txtRecType.Text == "VHINS") || (txtRecType.Text == "ADINS"))
                {
                    if (txtRecType.Text == "VHREG")
                    {
                        lblExtraChg.Text = "0.00";
                        VehicalRegistration _RegPrvDetails = new VehicalRegistration();
                        _RegPrvDetails = CHNLSVC.Sales.CheckPrvRegDetails(txtengine.Text.Trim(), txtItem.Text.ToUpper().Trim(), Session["UserCompanyCode"].ToString(), 2);

                        if (_RegPrvDetails.P_srvt_ref_no != null)
                        {
                            if (string.IsNullOrEmpty(txtProvince.Text))
                            {
                                DisplayMessage("Please select the province.");
                                cmbDistrict.Focus();
                                return;
                            }
                            if (_RegPrvDetails.P_svrt_province != txtProvince.Text)
                            {
                                //kapila 6/7/2015 check whether last inv is reversed or reverted
                                DataTable _dtReg = CHNLSVC.Financial.GetLastRegDetails(txtengine.Text.Trim(), txtItem.Text.ToUpper().Trim(), Session["UserCompanyCode"].ToString(), txtInvoice.Text);
                                if (_dtReg.Rows.Count > 0 || _dtReg != null)
                                {
                                    string _lastInv = _dtReg.Rows[0]["svrt_inv_no"].ToString();
                                    string _invTp = _dtReg.Rows[0]["svrt_sales_tp"].ToString();

                                    if (_invTp == "HS")
                                    {
                                        //check account is reverted
                                        Boolean _isAccRvt = CHNLSVC.Financial.IsRevertAccount("", "", _lastInv, Convert.ToDateTime(dtpRecDate.Text).Date);
                                        if (_isAccRvt == true)
                                            calc_revert_charge(_invTp, _RegPrvDetails.P_svrt_province);
                                    }
                                    else
                                    {
                                        //check inv is reversed
                                    }
                                }
                                else
                                {
                                    DisplayMessage("Already generated registration for this engine.Invoice # :" + _RegPrvDetails.P_svrt_inv_no);
                                    txtengine.Text = "";
                                    txtChasis.Text = "";
                                    txtengine.Focus();
                                    return;
                                }
                            }
                            else
                            {
                                DisplayMessage("Already generated registration for this engine.Invoice # :" + _RegPrvDetails.P_svrt_inv_no);
                                txtengine.Text = "";
                                txtChasis.Text = "";
                                txtengine.Focus();
                                return;
                            }
                        }

                    }
                    else if ((txtRecType.Text == "VHINS") || txtRecType.Text == "ADINS")
                    {
                        VehicleInsuarance _InsPrvDetails = new VehicleInsuarance();
                        _InsPrvDetails = CHNLSVC.Sales.CheckPrvInsDetails(txtengine.Text.Trim(), txtItem.Text.ToUpper().Trim(), Session["UserCompanyCode"].ToString(), 2);

                        if (_InsPrvDetails.Svit_ref_no != null)
                        {
                            DisplayMessage("Already generated insurance collection for this engine.Invoice # :" + _InsPrvDetails.Svit_inv_no);
                            txtengine.Text = "";
                            txtChasis.Text = "";
                            txtengine.Focus();
                            return;
                        }
                    }


                    if (chkDel.Checked == false)
                    {
                        ReptPickSerials _serialList = new ReptPickSerials();
                        _serialList = CHNLSVC.Inventory.GetAvailableSerIDInformation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), txtengine.Text.Trim(), string.Empty, string.Empty);

                        if (_serialList.Tus_ser_1 != null)
                        {
                            txtengine.Text = _serialList.Tus_ser_1;
                            if (txtChasis.Visible == true)
                            {
                                txtChasis.Text = _serialList.Tus_ser_2;
                            }
                        }
                        else
                        {
                            //ADDED BY SACHITH
                            //2012/11/15

                            if (txtRecType.Text == "VHINS")
                            {
                                DataTable _dt = CHNLSVC.Sales.GetInsuranceOnEngine(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "Reg", txtengine.Text);
                                if (_dt.Rows.Count > 0)
                                {
                                    //get chassis
                                    txtChasis.Text = _dt.Rows[0]["SVRT_CHASSIS"].ToString();
                                }
                                else
                                {
                                    DisplayMessage("Invalid serial / engine number.");
                                    txtengine.Text = "";
                                    txtengine.Focus();
                                    return;
                                }
                            }
                            else if (txtRecType.Text == "VHREG")
                            {
                                DataTable _dt = CHNLSVC.Sales.GetInsuranceOnEngine(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "Ins", txtengine.Text);
                                if (_dt.Rows.Count > 0)
                                {
                                    //get chassis
                                    txtChasis.Text = _dt.Rows[0]["SVIT_CHASSIS"].ToString();
                                }
                                else
                                {
                                    DisplayMessage("Invalid serial / engine number.");
                                    txtengine.Text = "";
                                    txtengine.Focus();
                                    return;
                                }

                            }

                            //END
                        }
                    }
                    else
                    {
                        InventorySerialN _delList = new InventorySerialN();
                        _delList = CHNLSVC.Inventory.GetDeliveredSerialForItem(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), txtengine.Text.Trim());

                        if (_delList != null)
                        {
                            txtengine.Text = _delList.Ins_ser_1;
                            if (txtChasis.Visible == true)
                            {
                                txtChasis.Text = _delList.Ins_ser_2;
                            }
                        }
                        else
                        {
                            DisplayMessage("Invalid serial / engine number.");
                            txtengine.Text = "";
                            txtengine.Focus();
                            return;
                        }

                    }
                }
                else if (txtRecType.Text == "ADINS")
                {
                    InventorySerialN _delList = new InventorySerialN();
                    _delList = CHNLSVC.Inventory.GetDeliveredSerialForItem(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), txtengine.Text.Trim());

                    if (_delList != null)
                    {
                        txtengine.Text = _delList.Ins_ser_1;
                        if (txtChasis.Visible == true)
                        {
                            txtChasis.Text = _delList.Ins_ser_2;
                        }
                    }
                    else
                    {
                        DisplayMessage("Invalid serial number.");
                        txtengine.Text = "";
                        txtengine.Focus();
                        return;
                    }
                }
                else
                {
                    ReptPickSerials _serialList = new ReptPickSerials();
                    _serialList = CHNLSVC.Inventory.GetAvailableSerIDInformation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), txtengine.Text.Trim(), string.Empty, string.Empty);

                    if (_serialList.Tus_ser_1 != null)
                    {
                        txtengine.Text = _serialList.Tus_ser_1;
                        if (txtChasis.Visible == true)
                        {
                            txtChasis.Text = _serialList.Tus_ser_2;
                        }
                    }
                    else
                    {
                        DisplayMessage("Invalid serial / engine number.");
                        txtengine.Text = "";
                        txtengine.Focus();
                        return;
                    }
                }
            }
        }

        protected void btnSerial_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRecType.Text))
            {
                DisplayMessage("Please select receipt type.");
                txtRecType.Focus();
                return;
            }

            if ((txtRecType.Text == "VHREG") || (txtRecType.Text == "VHINS"))
            {
                if (string.IsNullOrEmpty(txtItem.Text.ToUpper()))
                {
                    DisplayMessage("Please select invoice item.");
                    txtItem.Focus();
                    return;
                }

                if (chkDel.Checked == false)
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
                    DataTable result = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "AvailableSerialWithOth";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    mpSearchAdance.Show();
                    txtSearchbyword.Text = "";
                    selDatePanal(false);
                }
                else
                {
                    ViewState["SEARCH"] = null;
                    txtSearchbyword.Text = string.Empty;
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DeliverdSerials);
                    DataTable result = CHNLSVC.CommonSearch.GetDeliverdInvoiceItemSerials(SearchParams, null, null);
                    grdResult.DataSource = result;
                    grdResult.DataBind();
                    lblvalue.Text = "DeliverdSerials";
                    BindUCtrlDDLData(result);
                    ViewState["SEARCH"] = result;
                    mpSearchAdance.Show();
                    txtSearchbyword.Text = "";
                    selDatePanal(false);
                }
            }
            else if (txtRecType.Text == "ADINS")
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DeliverdSerials);
                DataTable result = CHNLSVC.CommonSearch.GetDeliverdInvoiceItemSerials(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "DeliverdSerials";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
            else
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth);
                DataTable result = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "AvailableSerialWithOth";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "ChangeSelctedTab(4);", true);
        }

        protected void txtInsCom_TextChanged(object sender, EventArgs e)
        {
            Int32 _HpTerm = 0;
            Boolean _isInsuFound = false;

            if (!string.IsNullOrEmpty(txtInsCom.Text))
            {
                MasterOutsideParty _OutPartyDetails = new MasterOutsideParty();
                _OutPartyDetails = CHNLSVC.Sales.GetOutSidePartyDetails(txtInsCom.Text.Trim(), "INS");

                if (_OutPartyDetails.Mbi_cd == null)
                {
                    DisplayMessage("Invalid insurance company.");
                    txtInsCom.Text = "";
                    txtInsPol.Text = "";
                    txtInsCom.Focus();
                    return;
                }
                else
                {
                    txtInsCom.Text = _OutPartyDetails.Mbi_cd;

                    if (!string.IsNullOrEmpty(txtInsPol.Text))
                    {
                        InsuarancePolicy _insuPolicy = new InsuarancePolicy();
                        _insuPolicy = CHNLSVC.Sales.GetInusPolicy(txtInsPol.Text.Trim());

                        if (_insuPolicy.Svip_polc_cd == null)
                        {
                            DisplayMessage("Invalid insurance policy.");
                            txtInsPol.Text = "";
                            return;
                        }

                        if (!string.IsNullOrEmpty(txtItem.Text.ToUpper()))
                        {
                            MasterVehicalInsuranceDefinitionNew _vehIns = new MasterVehicalInsuranceDefinitionNew();

                            InvoiceItem _invItm = new InvoiceItem();

                            //Get invoice details
                            string _pBook = "";
                            string _pLvl = "";
                            string _promoCd = "";
                            decimal _itmVal = 0;
                            string _cat1 = "";
                            string _cat2 = "";
                            string _brand = "";
                            _isInsuFound = false;

                            _invItm = CHNLSVC.Sales.GetInvDetByLine(txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), _invLine);
                            if (_invItm.Sad_itm_cd == null)
                            {
                                DisplayMessage("Cannot find invoice item details.");
                                txtItem.Text = "";
                                txtItem.Focus();
                                return;
                            }
                            else
                            {
                                MasterItem _itemList = new MasterItem();
                                _itemList = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().Trim());

                                _pBook = _invItm.Sad_pbook;
                                _pLvl = _invItm.Sad_pb_lvl;
                                _promoCd = _invItm.Sad_promo_cd;
                                _itmVal = _invItm.Sad_tot_amt / _invItm.Sad_qty;

                                if (_itemList.Mi_cd == null)
                                {
                                    DisplayMessage("Cannot find item details.");
                                    txtItem.Text = "";
                                    txtItem.Focus();
                                    return;
                                }
                                else
                                {
                                    _cat1 = _itemList.Mi_cate_1;
                                    _cat2 = _itemList.Mi_cate_2;
                                    _brand = _itemList.Mi_brand;
                                }
                            }

                            if (txtRecType.Text == "VHINS")
                            {
                                List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                                string _Subchannel = "";
                                string _typeSubChnl = "SCHNL";

                                string _Mainchannel = "";
                                string _typeMainChanl = "CHNL";

                                string _Pctype = "PC";
                                string _typePc = Session["UserDefProf"].ToString();


                                if (_Saleshir.Count > 0)
                                {
                                    _Subchannel = (from _lst in _Saleshir
                                                   where _lst.Mpi_cd == "SCHNL"
                                                   select _lst.Mpi_val).ToList<string>()[0];


                                    _Mainchannel = (from _lst in _Saleshir
                                                    where _lst.Mpi_cd == "CHNL"
                                                    select _lst.Mpi_val).ToList<string>()[0];

                                    if (_accNo != null && _accNo != "")
                                    {

                                        _HpAccount = new HpAccount();
                                        _HpAccount = CHNLSVC.Sales.GetHP_Account_onAccNo(_accNo);

                                        _HpTerm = _HpAccount.Hpa_term;

                                        if (_HpTerm < 12)
                                        {
                                            chkAnnual.Checked = false;
                                            chkAnnual.Visible = true;
                                        }
                                        else
                                        {
                                            chkAnnual.Checked = false;
                                            chkAnnual.Visible = false;
                                        }


                                        if ((_HpTerm + 3) <= 3)
                                        {
                                            _HpTerm = 3;
                                        }
                                        else if ((_HpTerm + 3) <= 6)
                                        {
                                            _HpTerm = 6;
                                        }
                                        else if ((_HpTerm + 3) <= 9)
                                        {
                                            _HpTerm = 9;
                                        }
                                        else
                                        {
                                            _HpTerm = 12;
                                        }


                                        if (!string.IsNullOrEmpty(_promoCd))
                                        {
                                            //check serial + promo
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //check pc + promo
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //check sub Channel + promo
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //Check channel + promo
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //check pc
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //check sub Channel
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //Check channel
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                        }
                                        else
                                        {
                                            //check serial
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //check pc
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //check sub Channel
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //Check channel
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                        }


                                        //_vehIns = CHNLSVC.Sales.GetVehInsDef(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), Convert.ToDateTime(dtpRecDate.Text).Date, Session["UserDefProf"].ToString(), txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _HpTerm);
                                    }

                                    else
                                    {
                                        _HpTerm = 12;
                                        //_vehIns = CHNLSVC.Sales.GetVehInsDef(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), Convert.ToDateTime(dtpRecDate.Text).Date, Session["UserDefProf"].ToString(), txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _HpTerm);
                                        if (!string.IsNullOrEmpty(_promoCd))
                                        {
                                            //check serial + promo
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //check pc + promo
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //check sub Channel + promo
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //Check channel + promo
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //check serial
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //check pc
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //check sub Channel
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //Check channel
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                        }
                                        else
                                        {
                                            //check serial
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //check pc
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //check sub Channel
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                            //Check channel
                                            _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                            if (_vehIns.Svid_itm != null)
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    DisplayMessage("Profit center hierarchy not set.");
                                    txtBalance.Text = "0.00";
                                    txtPayment.Text = "0.00";
                                    return;
                                }

                                if (_isInsuFound == false)
                                {
                                    DisplayMessage("Cannot find insurance definition.");
                                    txtBalance.Text = "0.00";
                                    txtPayment.Text = "0.00";
                                    txtInsCom.Text = "";
                                    txtInsPol.Text = "";
                                    return;
                                }

                            }
                            else if (txtRecType.Text == "ADINS")
                            {
                                if ((!string.IsNullOrEmpty(txtInsPol.Text)) && (!string.IsNullOrEmpty(txtInsCom.Text)))
                                {

                                    //InsuarancePolicy _insuPolicy = new InsuarancePolicy();
                                    //_insuPolicy = CHNLSVC.Sales.GetInusPolicy(txtInsPol.Text.Trim());

                                    //if (_insuPolicy.Svip_polc_cd == null)
                                    //{
                                    //    MessageBox.Show("Invalid insuarance policy.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    txtInsPol.Text = "";
                                    //    txtInsPol.Focus();
                                    //    return;
                                    //}

                                    //MasterOutsideParty _OutPartyDetails = new MasterOutsideParty();
                                    //_OutPartyDetails = CHNLSVC.Sales.GetOutSidePartyDetails(txtInsCom.Text.Trim(), "INS");

                                    //if (_OutPartyDetails.Mbi_cd == null)
                                    //{
                                    //    MessageBox.Show("Invalid insuarance company.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    txtInsCom.Text = "";
                                    //    txtInsCom.Focus();
                                    //    return;
                                    //}

                                    //txtInsCom.Text = _OutPartyDetails.Mbi_cd;
                                    //txtInsPol.Text = _insuPolicy.Svip_polc_cd;
                                    _vehIns = new MasterVehicalInsuranceDefinitionNew();


                                    ////Get invoice details
                                    //string _pBook = "";
                                    //string _pLvl = "";
                                    //string _promoCd = "";
                                    //decimal _itmVal = 0;
                                    _isInsuFound = false;

                                    //_invItm = CHNLSVC.Sales.GetInvDetByLine(txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), _invLine);
                                    //if (_invItm.Sad_itm_cd == null)
                                    //{
                                    //    MessageBox.Show("Cannot find invoice item details.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //    txtItem.Text.ToUpper() = "";
                                    //    txtItem.Focus();
                                    //    return;
                                    //}
                                    //else
                                    //{
                                    //    _pBook = _invItm.Sad_pbook;
                                    //    _pLvl = _invcmbLevel.Text;
                                    //    _promoCd = _invItm.Sad_promo_cd;
                                    //    _itmVal = _invItm.Sad_tot_amt / _invItem.Sad_qty;
                                    //}



                                    List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                                    string _Subchannel = "";
                                    string _typeSubChnl = "SCHNL";

                                    string _Mainchannel = "";
                                    string _typeMainChanl = "CHNL";

                                    string _Pctype = "PC";
                                    string _typePc = Session["UserDefProf"].ToString();


                                    if (_Saleshir.Count > 0)
                                    {
                                        _Subchannel = (from _lst in _Saleshir
                                                       where _lst.Mpi_cd == "SCHNL"
                                                       select _lst.Mpi_val).ToList<string>()[0];


                                        _Mainchannel = (from _lst in _Saleshir
                                                        where _lst.Mpi_cd == "CHNL"
                                                        select _lst.Mpi_val).ToList<string>()[0];


                                        //---------------
                                        //check pc
                                        // check item + price book + price level
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + pb + plvl
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + brand + pb + plvl
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + cat2 + pb + plvl
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + cat2 + brand + pb + plvl
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        // check item
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + brand 
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + cat2 
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + cat2 + brand
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }


                                        //check sub Channel
                                        // check item + price book + price level
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + pb + plvl
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + brand + pb + plvl
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + cat2 + pb + plvl
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + cat2 + brand + pb + plvl
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        // check item
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + brand 
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + cat2 
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + cat2 + brand
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }

                                        //Check channel
                                        // check item + price book + price level
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + pb + plvl
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + brand + pb + plvl
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + cat2 + pb + plvl
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + cat2 + brand + pb + plvl
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, _pBook, _pLvl);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        // check item
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + brand 
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + cat2 
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //check cat1 + cat2 + brand
                                        _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, null, null);

                                        if (_vehIns.Svid_com != null)
                                        {
                                            if (_vehIns.Svid_is_rt == 1)
                                            {
                                                txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }
                                            else
                                            {
                                                txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                                txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                                _isInsuFound = true;
                                                goto L59;
                                            }

                                        }
                                        //--------------
                                    }
                                    else
                                    {
                                        DisplayMessage("Profit center hierarchy not set.");
                                        txtBalance.Text = "0.00";
                                        txtPayment.Text = "0.00";
                                        return;
                                    }

                                    if (_isInsuFound == false)
                                    {
                                        DisplayMessage("Cannot find insurance definition.");
                                        txtBalance.Text = "0.00";
                                        txtPayment.Text = "0.00";
                                        txtInsCom.Text = "";
                                        txtInsPol.Text = "";
                                        return;
                                    }
                                }
                            }

                            //if (_isInsuFound == false)
                        //{
                        //    MessageBox.Show("Insuarance amount definitions not set.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    txtBalance.Text = "0.00";
                        //    txtPayment.Text = "0.00";
                        //    txtInsPol.Text = "";
                        //    txtInsCom.Text = "";
                        //    txtInsCom.Focus();
                        //    return;
                        //}

                        L59: int I = 0;
                        }
                    }
                }
            }
        }

        protected void btnInsCom_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuCom);
                DataTable result = CHNLSVC.CommonSearch.GetInsuCompany(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "InsuCom";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void txtInsPol_TextChanged(object sender, EventArgs e)
        {
            Int32 _HpTerm = 0;
            Boolean _isInsuFound = false;

            if (!string.IsNullOrEmpty(txtInsPol.Text))
            {
                if (!string.IsNullOrEmpty(txtItem.Text.ToUpper()))
                {
                    InsuarancePolicy _insuPolicy = new InsuarancePolicy();
                    _insuPolicy = CHNLSVC.Sales.GetInusPolicy(txtInsPol.Text.Trim());

                    if (_insuPolicy.Svip_polc_cd == null)
                    {
                        DisplayMessage("Invalid insurance policy.");
                        txtInsPol.Text = "";
                        txtInsPol.Focus();
                        return;
                    }

                    else
                    {
                        txtInsPol.Text = _insuPolicy.Svip_polc_cd;
                        MasterVehicalInsuranceDefinitionNew _vehIns = new MasterVehicalInsuranceDefinitionNew();

                        InvoiceItem _invItm = new InvoiceItem();

                        //Get invoice details
                        string _pBook = "";
                        string _pLvl = "";
                        string _promoCd = "";
                        decimal _itmVal = 0;
                        string _cat1 = "";
                        string _cat2 = "";
                        string _brand = "";
                        _isInsuFound = false;

                        _invItm = CHNLSVC.Sales.GetInvDetByLine(txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), _invLine);
                        if (_invItm.Sad_itm_cd == null)
                        {
                            DisplayMessage("Cannot find invoice item details.");
                            txtItem.Text = "";
                            txtItem.Focus();
                            return;
                        }
                        else
                        {
                            MasterItem _itemList = new MasterItem();
                            _itemList = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().Trim());

                            _pBook = _invItm.Sad_pbook;
                            _pLvl = _invItm.Sad_pb_lvl;
                            _promoCd = _invItm.Sad_promo_cd;
                            _itmVal = _invItm.Sad_tot_amt / _invItm.Sad_qty;

                            if (_itemList.Mi_cd == null)
                            {
                                DisplayMessage("Cannot find item details.");
                                txtItem.Text = "";
                                txtItem.Focus();
                                return;
                            }
                            else
                            {
                                _cat1 = _itemList.Mi_cate_1;
                                _cat2 = _itemList.Mi_cate_2;
                                _brand = _itemList.Mi_brand;
                            }
                        }


                        if (txtRecType.Text == "VHINS")
                        {
                            List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                            string _Subchannel = "";
                            string _typeSubChnl = "SCHNL";

                            string _Mainchannel = "";
                            string _typeMainChanl = "CHNL";

                            string _Pctype = "PC";
                            string _typePc = Session["UserDefProf"].ToString();


                            if (_Saleshir.Count > 0)
                            {
                                _Subchannel = (from _lst in _Saleshir
                                               where _lst.Mpi_cd == "SCHNL"
                                               select _lst.Mpi_val).ToList<string>()[0];


                                _Mainchannel = (from _lst in _Saleshir
                                                where _lst.Mpi_cd == "CHNL"
                                                select _lst.Mpi_val).ToList<string>()[0];

                                if (_accNo != null && _accNo != "")
                                {

                                    _HpAccount = new HpAccount();
                                    _HpAccount = CHNLSVC.Sales.GetHP_Account_onAccNo(_accNo);

                                    _HpTerm = _HpAccount.Hpa_term;

                                    if (_HpTerm < 12)
                                    {
                                        chkAnnual.Checked = false;
                                        chkAnnual.Visible = true;
                                    }
                                    else
                                    {
                                        chkAnnual.Checked = false;
                                        chkAnnual.Visible = false;
                                    }


                                    if ((_HpTerm + 3) <= 3)
                                    {
                                        _HpTerm = 3;
                                    }
                                    else if ((_HpTerm + 3) <= 6)
                                    {
                                        _HpTerm = 6;
                                    }
                                    else if ((_HpTerm + 3) <= 9)
                                    {
                                        _HpTerm = 9;
                                    }
                                    else
                                    {
                                        _HpTerm = 12;
                                    }


                                    if (!string.IsNullOrEmpty(_promoCd))
                                    {
                                        //check serial + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check pc + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check serial + sub Channel + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check sub Channel + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //Check serial + channel + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //Check channel + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check serial
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check pc
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check serial + sub Channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        //check sub Channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //Check serial + channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //Check channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                    }
                                    else
                                    {
                                        //check serial
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check pc
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check serial + sub Channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check sub Channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //Check serial + channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //Check channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                    }


                                    //_vehIns = CHNLSVC.Sales.GetVehInsDef(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), Convert.ToDateTime(dtpRecDate.Text).Date, Session["UserDefProf"].ToString(), txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _HpTerm);
                                }

                                else
                                {
                                    _HpTerm = 12;
                                    //_vehIns = CHNLSVC.Sales.GetVehInsDef(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), Convert.ToDateTime(dtpRecDate.Text).Date, Session["UserDefProf"].ToString(), txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _HpTerm);
                                    if (!string.IsNullOrEmpty(_promoCd))
                                    {
                                        //check serial + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check pc + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check serial +sub Channel + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check sub Channel + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //Check serial + channel + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //Check channel + promo
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check serial
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check pc
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check serial + sub Channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check sub Channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //Check serial + channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //Check channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                    }
                                    else
                                    {
                                        //check serial
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //check pc
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        //check serial + sub Channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        //check sub Channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //Check serial + channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                        //Check channel
                                        _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                        if (_vehIns.Svid_itm != null)
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                    }
                                }

                                //if (_vehIns.Ins_com_cd != null)
                                //{
                                //    txtBalance.Text = _vehIns.Value.ToString("0.00");
                                //    txtPayment.Text = _vehIns.Value.ToString("0.00");
                                //}
                                //else
                                //{
                                //    MessageBox.Show("Insuarance amount definitions not set.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    txtBalance.Text = "0.00";
                                //    txtPayment.Text = "0.00";
                                //    txtInsPol.Text = "";
                                //    txtInsPol.Focus();
                                //    return;
                                //}

                            }
                            if (_isInsuFound == false)
                            {
                                DisplayMessageJS("Insurance amount definitions not set.");
                                txtBalance.Text = "0.00";
                                txtPayment.Text = "0.00";
                                txtInsPol.Text = "";
                                txtInsPol.Focus();
                                return;
                            }
                        }
                        else if (txtRecType.Text == "ADINS")
                        {
                            if ((!string.IsNullOrEmpty(txtInsPol.Text)) && (!string.IsNullOrEmpty(txtInsCom.Text)))
                            {


                                _vehIns = new MasterVehicalInsuranceDefinitionNew();
                                _isInsuFound = false;


                                List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                                string _Subchannel = "";
                                string _typeSubChnl = "SCHNL";

                                string _Mainchannel = "";
                                string _typeMainChanl = "CHNL";

                                string _Pctype = "PC";
                                string _typePc = Session["UserDefProf"].ToString();


                                if (_Saleshir.Count > 0)
                                {
                                    _Subchannel = (from _lst in _Saleshir
                                                   where _lst.Mpi_cd == "SCHNL"
                                                   select _lst.Mpi_val).ToList<string>()[0];


                                    _Mainchannel = (from _lst in _Saleshir
                                                    where _lst.Mpi_cd == "CHNL"
                                                    select _lst.Mpi_val).ToList<string>()[0];


                                    //check pc
                                    // check item + price book + price level
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + pb + plvl
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + brand + pb + plvl
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + cat2 + pb + plvl
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + cat2 + brand + pb + plvl
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    // check item
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + brand 
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + cat2 
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + cat2 + brand
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }


                                    //check sub Channel
                                    // check item + price book + price level
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + pb + plvl
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + brand + pb + plvl
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + cat2 + pb + plvl
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + cat2 + brand + pb + plvl
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    // check item
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + brand 
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + cat2 
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + cat2 + brand
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }

                                    //Check channel
                                    // check item + price book + price level
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + pb + plvl
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + brand + pb + plvl
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + cat2 + pb + plvl
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + cat2 + brand + pb + plvl
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, _pBook, _pLvl);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    // check item
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, txtItem.Text.ToUpper().Trim(), null, null, null, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, null, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + brand 
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, null, _brand, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + cat2 
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, null, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                    //check cat1 + cat2 + brand
                                    _vehIns = CHNLSVC.Sales.GetAddInsAmt(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _invType, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal, txtRecType.Text, null, _cat1, _cat2, _brand, null, null);

                                    if (_vehIns.Svid_com != null)
                                    {
                                        if (_vehIns.Svid_is_rt == 1)
                                        {
                                            txtBalance.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            txtPayment.Text = (_vehIns.Svid_val * _itmVal / 100).ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }
                                        else
                                        {
                                            txtBalance.Text = _vehIns.Svid_val.ToString("0.00");
                                            txtPayment.Text = _vehIns.Svid_val.ToString("0.00");
                                            _isInsuFound = true;
                                            goto L59;
                                        }

                                    }
                                }
                                else
                                {
                                    DisplayMessage("Profit center hierarchy not set.");
                                    txtBalance.Text = "0.00";
                                    txtPayment.Text = "0.00";
                                    return;
                                }

                                if (_isInsuFound == false)
                                {
                                    DisplayMessage("Cannot find insurance definition.");
                                    txtBalance.Text = "0.00";
                                    txtPayment.Text = "0.00";
                                    txtInsCom.Text = "";
                                    txtInsPol.Text = "";
                                    return;
                                }
                            }
                        }
                    }

                L59: int I = 0;
                }
                else
                {
                    DisplayMessage("Invoice item is missing.");
                    txtInsPol.Text = "";
                    txtItem.Focus();
                    return;
                }

            }
        }

        #endregion

        #region Search
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuCom:
                    {
                        paramsText.Append("INS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuPolicy:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReceiptType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Division:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OutstandingInv:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + txtCusCode.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OutstandingInvOth:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + txtOthSR.Text + seperator + txtCusCode.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "INV" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceByCus:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + "INV" + seperator + txtCusCode.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItems:
                    {
                        paramsText.Append(txtInvoice.Text.Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtItem.Text.ToUpper().Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DeliverdSerials:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator + txtInvoice.Text.Trim() + seperator + txtItem.Text.ToUpper().Trim() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator + txtRecType.Text.Trim());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GetItmByType:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + "G" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReceiptDate:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString());
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator);
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
                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefLoca"].ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DisposalJOb:
                    {
                        paramsText.Append(Session["UserCompanyCode"].ToString() + seperator + Session["UserDefProf"].ToString() + seperator); break;
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void btnSchAdvClose_Click(object sender, EventArgs e)
        {
            txtSearchbyword.Text = "";
            mpSearchAdance.Hide();
        }

        protected void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                FilterData();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
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
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void grdResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                Session["ExchangeRate"] = 1;
                if (lblvalue.Text == "ReceiptType")
                {
                    txtRecType.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtRecType_TextChanged(null, null);
                }
                else if (lblvalue.Text == "ReceiptType")
                {
                    txtDivision.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtDivision_TextChanged(null, null);
                }
                else if (lblvalue.Text == "ReceiptDate")
                {
                    txtRecNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtRecNo_TextChanged(null, null);
                }
                else if (lblvalue.Text == "CustomerCommon")
                {
                    txtCusCode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtCusCode_TextChanged(null, null);
                }
                else if (lblvalue.Text == "OutstandingInvOth" || lblvalue.Text == "OutstandingInv" || lblvalue.Text == "SalesInvoice" || lblvalue.Text == "InvoiceByCus")
                {
                    txtInvoice.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtInvoice_TextChanged(null, null);
                }
                else if (lblvalue.Text == "AllProfitCenters")
                {
                    txtOthSR.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtOthSR_TextChanged(null, null);
                }
                else if (lblvalue.Text == "InvoiceItems")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItem_TextChanged(null, null);
                }
                else if (lblvalue.Text == "InsuCom")
                {
                    txtInsCom.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtInsCom_TextChanged(null, null);
                }
                else if (lblvalue.Text == "ItemAvailableSerial")
                {
                    txtSerialNo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtSerial_TextChange();
                }
                else if (lblvalue.Text == "InvoiceItemUnAssable")
                {
                    TxtAdvItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItemCode_TextChange();
                }
                else if (lblvalue.Text == "AvailableSerialWithOth")
                {
                    txtengine.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtengine_TextChanged(null, null);
                }
                else if (lblvalue.Text == "GetItmByType")
                {
                    txtGVCode.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtGVCode_TextChanged(null, null);
                    txtGVCode.Focus();
                }
                else if (lblvalue.Text == "InsuPolicy")
                {
                    txtInsPol.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtInsPol_TextChanged(null, null);
                    txtInsPol.Focus();
                }
                else if (lblvalue.Text == "DeliverdSerials")
                {
                    txtengine.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtengine_TextChanged(null, null);
                    txtengine.Focus();
                }
                else if (lblvalue.Text == "DisposalJOb")
                {
                    txtDisposalJob.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtDisposalJob_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Item")
                {
                    txtItem.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtItem_TextChanged(null, null);
                }
                else if (lblvalue.Text == "OutstandingInv_Allo")
                {
                    txtInvoiceNumAllo.Text = grdResult.SelectedRow.Cells[1].Text;
                    txtInvoiceNumAllo_TextChanged(null, null);
                }
                else if (lblvalue.Text == "Division")
                {
                    txtDivision.Text = grdResult.SelectedRow.Cells[1].Text;

                }

                ViewState["SEARCH"] = null;
                mpSearchAdance.Hide();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
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
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
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

        private void FilterData()
        {
            try
            {
                if (lblvalue.Text == "ReceiptType")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptType);
                    DataTable _result = CHNLSVC.CommonSearch.GetReceiptTypes(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "ReceiptType";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "Division")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptType);
                    DataTable _result = CHNLSVC.CommonSearch.GetDivision(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "Division";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "ReceiptDate")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptDate);
                    DataTable _result = new DataTable();
                    if (!chkUnAllocated.Checked)
                    {
                        _result = CHNLSVC.CommonSearch.GetReceiptsDate(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                    }
                    else
                    {
                        _result = CHNLSVC.CommonSearch.SEARCH_RECEIPT_UNALO(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtFDate.Text), Convert.ToDateTime(txtTDate.Text));
                    }
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "ReceiptDate";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "CustomerCommon")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CustomerCommon);
                    DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "CustomerCommon";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "OutstandingInvOth")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OutstandingInvOth);
                    DataTable _result = CHNLSVC.CommonSearch.GetOutstandingInvoice(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "OutstandingInvOth";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "SalesInvoice")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SalesInvoice);
                    DataTable _result = CHNLSVC.CommonSearch.GetAllInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "SalesInvoice";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "InvoiceByCus")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceByCus);
                    DataTable _result = CHNLSVC.CommonSearch.GetInvoicebyCustomer(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "InvoiceByCus";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "InvoiceItems")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItems);
                    DataTable _result = CHNLSVC.CommonSearch.GetInvoiceItem(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "InvoiceItems";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "Item")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "InvoiceItems";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "InsuCom")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuCom);
                    DataTable _result = CHNLSVC.CommonSearch.GetInsuCompany(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "InsuCom";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "ItemAvailableSerial")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial);
                    DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "ItemAvailableSerial";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "InvoiceItemUnAssable")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "InvoiceItemUnAssable";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "GetItmByType")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                    DataTable _result = CHNLSVC.CommonSearch.GetItemByType(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "GetItmByType";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "OutstandingInv")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OutstandingInv);
                    DataTable _result = CHNLSVC.CommonSearch.GetOutstandingInvoice(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "OutstandingInv";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "InsuPolicy")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuPolicy);
                    DataTable _result = CHNLSVC.CommonSearch.GetInsuPolicy(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "InsuPolicy";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
                else if (lblvalue.Text == "DisposalJOb")
                {
                    string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisposalJOb);
                    DataTable _result = CHNLSVC.CommonSearch.SEARCH_DISPOSAL_JOB(SearchParams, ddlSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString(), Convert.ToDateTime(txtFDate.Text.Trim()).Date, Convert.ToDateTime(txtTDate.Text.Trim()).Date);
                    grdResult.DataSource = _result;
                    grdResult.DataBind();
                    lblvalue.Text = "DisposalJOb";
                    ViewState["SEARCH"] = _result;
                    mpSearchAdance.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }


        #endregion

        private void Clear_Data()
        {
            //pnlPriceNPromotion.Visible = false;

            _invoiceItemList = new List<InvoiceItem>();
            gvInvoiceItem.DataSource = new List<InvoiceItem>();
            gvInvoiceItem.DataBind();
            //_//ucPayModes1.Enabled = true;
            _sheduleDetails = new List<HpSheduleDetails>();
            _regList = new List<VehicalRegistration>();
            _insList = new List<VehicleInsuarance>();
            _HpAccount = new HpAccount();
            _SchemeDetails = new HpSchemeDetails();
            _ResList = new List<ReptPickSerials>();
            ucPayModes1.MainGrid.DataSource = new List<RecieptItem>();
            ucPayModes1.MainGrid.DataBind();
            _list = new List<RecieptItem>();
            _gvDetails = new List<GiftVoucherPages>();
            _tmpRecItem = new List<ReceiptItemDetails>();
            ucPayModes1.PaidAmountLabel.Text = "0.00";
            //_//ucPayModes1.ClearControls();
            _invoiceItemList = new List<InvoiceItem>();
            gvInvoiceItem.DataSource = new List<InvoiceItem>();
            gvInvoiceItem.DataBind();
            lblSalesType.Text = "";
            //_//lblSchme.Text = "";
            dgvGv.AutoGenerateColumns = false;
            dgvGv.DataSource = new List<GiftVoucherPages>();
            dgvGv.DataBind();
            txtUnitAmt.Text = FormatToCurrency("0");
            btn_add_ser.Enabled = true;
            gbInsu.Visible = false;
            gbItem.Visible = false;
            chkDel.Enabled = false;
            chkAnnual.Visible = false;
            lblBackDateInfor.Text = "";
            _regAmt = 0;
            _invType = "";
            _accNo = "";
            _invNo = "";
            _invLine = 0;
            _colTerm = 0;
            _insuTerm = 0;
            _insuAmt = 0;
            _IsRecall = false;
            _RecStatus = false;
            _sunUpload = false;
            _isRes = false;
            _usedAmt = 0;
            chkOth.Enabled = true;
            txtOthSR.Enabled = true;
            chkOth.Checked = false;
            txtOthSR.Text = "";
            Session["ExcelMultiplePc"] = 0;
            _outstandingList = new List<InvOutstanding>();
            //_//ClearHP();
            if (Session["UserCompanyCode"].ToString() != "AAL")
            {
                lblSer1.Text = "Serial 1";
                lblSer2.Text = "Serial 2";
            }

            dgvReg.DataSource = new int[] { };
            dgvReg.DataBind();

            dgvGv.DataSource = new int[] { };
            dgvGv.DataBind();

            dgvIns.DataSource = new int[] { };
            dgvIns.DataBind();

            dgvItem.DataSource = new int[] { };
            dgvItem.DataBind();

            dgvDebtInvoiceList.DataSource = new int[] { };
            dgvDebtInvoiceList.DataBind();

            gvInvoiceItem.DataSource = new int[] { };
            gvInvoiceItem.DataBind();

            txtRecType.Text = "";
            txtRecNo.Text = "";
            txtManual.Text = "";
            txtDivision.Text = "";

            radioButtonManual.Checked = false;
            radioButtonManual.Checked = false;
            radioButtonManual.Enabled = true;

            dtpRecDate.Text = Convert.ToDateTime(DateTime.Now).Date.ToString("dd/MMM/yyyy");
            txtGVCode.Text = "";
            lblFrompg.Text = "";
            lblPageCount.Text = "";
            cmbGvBook.DataSource = new DataTable();
            cmbGvBook.DataBind();
            cmbTopg.DataSource = new DataTable();
            cmbTopg.DataBind();
            txtPgAmt.Text = "";
            txtTotGvAmt.Text = "";
            txtTotal.Text = "";
            txtSalesEx.Text = "";

            cmbDistrict.Enabled = true;
            txtCusCode.Enabled = true;
            txtRecType.Enabled = true;
            ClearSettle_Data();
            ClearCus_Data();
            Load_District();
            btnSave.Enabled = true;

            //btnCancel.Enabled = false;
            setButtionEnable(false, btnCancel, "confCancel");

            setButtionEnable(true, btnSave, "confSave");

            gbGVDet.Enabled = true;
            gbGVDet.Visible = false;
            gbsettle.Visible = true;
            chkAllowPromo.Checked = true;
            chkAllowPromo.Visible = false;
            chkGvFOC.Checked = false;
            chkGvFOC.Visible = false;
            dtGVExp.Text = Convert.ToDateTime(DateTime.Now).Date.ToString("dd/MMM/yyyy");
            dtGVExp.Visible = false;
            lblGVExp.Visible = false;

            bool _allowCurrentTrans = false;


            //IsAllowBackDateForModule(Session["UserCompanyCode"].ToString(), string.Empty, Session["UserDefProf"].ToString(), this.GlbModuleName, dtpRecDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);

            //IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), string.Empty.ToUpper().ToString(), Session["UserDefProf"].ToString(), this.Page, dtpRecDate.Text, lblBackDateInfor, lblBackDateInfor, "m_Trans_Sales_SalesInvoice", out  _allowCurrentTrans);
            setDTPEnable(false);

            txtRecType.Text = "ADVAN";
            txtRecType_TextChanged(null, null);

            //kapila 8/4/2015
            ucPayModes1.TransDate.Text = dtpRecDate.Text;

            if (BaseCls.GlbIsManChkLoc == true)
            {
                txtManual.Text = "";
                // txtManual.Enabled = false;
            }
            else
            {
                txtManual.Text = "";
                txtManual.Enabled = true;
            }
            cmbBook.SelectedIndex = -1;
            cmbLevel.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            txtTaxAmt.Text = "0";
            txtQty.Text = "0";
            txtQty.Text = "1";
            txtSerialNo.Text = "";
            txtUnitPrice.Text = "0";
            txtLineTotAmt.Text = "0";
            TxtAdvItem.Text = "";
            txtItem.Text = "";
            txtRecType.Focus();

            MultiView1.ActiveViewIndex = 0;

            txtInvoice.Enabled = false;

            clearValiables();
            ClearValiables2();

            ucPayModes1.TotalAmount = 0;
            ucPayModes1.InvoiceItemList = _invoiceItemList;
            ucPayModes1.SerialList = InvoiceSerialList;
            ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(0));
            ucPayModes1.PaidAmountLabel.Text = FormatToCurrency(Convert.ToString(0));
            ucPayModes1.LoadData();
            ucPayModes1.DataBind();
            ucPayModes1.setGriDel_enables(true);
            ucPayModes1.RecieptItemList = new List<RecieptItem>();
            ucPayModes1.InvoiceNo = string.Empty;
            ucPayModes1.TransDate.Visible = false;

            txtAllocQty.Text = "";
            txtRecQty.Text = "";
            txtFreeQty.Text = "";

            pnlItemAlloc.Visible = false;

            btnCancel.Enabled = false;

            loadItemStatus();
            chkUnAllocated.Visible = false;
            pnlDebtInvoices.Visible = false;
            btnAllocate.Visible = false;
            pnlPaymentsadd.Visible = true;

            chkUnAllocated.Checked = false;

            txtInvoiceNumAllo.Text = "";
            txtAmountAlloca.Text = "";
            cmdCheqNums.Items.Clear();
            cmdPayTypeAlloca.Items.Clear();

            ViewState["_listReceiptEntry"] = null;
        }

        private void DisplayMessage(String Msg, Int32 option, Exception ex = null)
        {
            Msg = Msg.Replace(@"\r", "").Replace(@"\n", "");
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Err !');", true);
                if (ex != null)
                {
                    WriteErrLog(Msg, ex);
                }
            }
            else if (option == 5)
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

        private void selDatePanal(bool isEnable)
        {
            if (isEnable)
            {
                pnlSearchByDate.Enabled = true;
                lbtnTDate.CssClass = "buttonUndocolor";
                lbtnFDate.CssClass = "buttonUndocolor";
                CalendarExtender1.Enabled = true;
                CalendarExtender3.Enabled = true;
            }
            else
            {
                pnlSearchByDate.Enabled = false;
                lbtnTDate.CssClass = "buttoncolor";
                lbtnFDate.CssClass = "buttoncolor";
                CalendarExtender1.Enabled = false;
                CalendarExtender3.Enabled = false;
            }
        }

        private void ButtonEnableDisavle(bool isEnable, LinkButton oButton, string ScriptName)
        {
            if (isEnable)
            {
                oButton.Enabled = true;
                if (!string.IsNullOrEmpty(ScriptName))
                {
                    oButton.OnClientClick = "return " + ScriptName + "();";
                }
                oButton.CssClass = "buttonUndocolor";
            }
            else
            {
                oButton.CssClass = "buttoncolor";
                oButton.Enabled = false;
                oButton.OnClientClick = "";
            }
        }

        private void ButtonEnableDisavle(bool isEnable, Button oButton, string ScriptName)
        {
            if (isEnable)
            {
                oButton.Enabled = true;
                if (!string.IsNullOrEmpty(ScriptName))
                {
                    oButton.OnClientClick = "return " + ScriptName + "();";
                }
                oButton.CssClass = "buttonUndocolor";
            }
            else
            {
                oButton.CssClass = "buttoncolor";
                oButton.Enabled = false;
                oButton.OnClientClick = "";
            }
        }

        private void ClearSettle_Data()
        {
            txtInvoice.Text = "";
            txtBalance.Text = "0.00";
            txtItem.Text = "";
            txtengine.Text = "";
            txtChasis.Text = "";
            txtInsCom.Text = "";
            txtInsPol.Text = "";
            txtPayment.Text = "0.00";
            lblExtraChg.Text = "0.00";
            chkDel.Checked = false;
            chkAnnual.Checked = false;
        }

        private void ClearCus_Data()
        {
            txtCusCode.Text = "";
            txtCusName.Text = "";
            txtCusAdd1.Text = "";
            txtCusAdd2.Text = "";
            txtNIC.Text = "";
            txtMobile.Text = "";
            txtProvince.Text = "";
        }

        private void loadPrifixes()
        {
            MasterProfitCenter profCenter = CHNLSVC.Sales.GetProfitCenter(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());
            string docTp = "";
            if (radioButtonSystem.Checked)
            {
                //ISSys = false;
                docTp = "SDOC_AVREC";

            }
            else
            {
                //   ISSys = true;
                docTp = "MDOC_AVREC";
            }
            List<string> prifixes = new List<string>();
            try
            {
                prifixes = CHNLSVC.Sales.GetAll_prifixes(profCenter.Mpc_chnl, docTp, 1);
            }
            catch (Exception)
            {
                comboBoxPrefix.DataSource = null;
                comboBoxPrefix.DataBind();
            }
            comboBoxPrefix.DataSource = prifixes;
            comboBoxPrefix.DataBind();
            if (radioButtonSystem.Checked) txtManual.Text = "";

        }

        private Boolean IsNumeric(string stringToTest)
        {
            decimal result;
            return decimal.TryParse(stringToTest, out result);
        }

        private void LoadCustomerDetails()
        {
            MasterBusinessEntity _masterBusinessCompany = new MasterBusinessEntity();
            if (!string.IsNullOrEmpty(txtCusCode.Text))
            {
                _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCusCode.Text.Trim(), string.Empty, string.Empty, "C");
                if (_masterBusinessCompany.Mbe_cd == null)
                {
                    _masterBusinessCompany = CHNLSVC.Sales.GetBusinessCompanyDetail(Session["UserCompanyCode"].ToString(), txtCusCode.Text.Trim(), string.Empty, string.Empty, "S");
                }
                if (_masterBusinessCompany.Mbe_cd != null)
                {

                    if (_masterBusinessCompany.Mbe_cd == "CASH")
                    {
                        txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
                        txtCusName.Text = "";
                        txtCusAdd1.Text = "";
                        txtCusAdd2.Text = "";
                        txtNIC.Text = "";
                        txtMobile.Text = "";
                        txtCusCode.ReadOnly = false;
                        txtCusName.ReadOnly = false;
                    }
                    else
                    {
                        txtCusCode.Text = _masterBusinessCompany.Mbe_cd;
                        //Addded by Dulaj 2018/Sep/21
                        Session["Customer_Code"] = _masterBusinessCompany.Mbe_cd;
                        txtCusName.Text = _masterBusinessCompany.Mbe_name;
                        txtCusAdd1.Text = _masterBusinessCompany.Mbe_add1;
                        txtCusAdd2.Text = _masterBusinessCompany.Mbe_add2;
                        txtNIC.Text = _masterBusinessCompany.Mbe_nic;
                        txtMobile.Text = _masterBusinessCompany.Mbe_mob;

                        if (string.IsNullOrEmpty(_masterBusinessCompany.Mbe_distric_cd))
                        {

                        }
                        else
                        {
                            cmbDistrict.Text = _masterBusinessCompany.Mbe_distric_cd;
                        }

                        txtProvince.Text = _masterBusinessCompany.Mbe_province_cd;
                        txtCusName.ReadOnly = true;
                    }
                }
                else
                {
                    DisplayMessage("Invalid customer.");
                    txtCusCode.Text = "";
                    txtCusName.Text = "";
                    txtCusAdd1.Text = "";
                    txtCusAdd2.Text = "";
                    txtNIC.Text = "";
                    txtMobile.Text = "";
                    txtProvince.Text = "";
                    txtCusCode.Focus();
                    return;
                }
            }
            else
            {
                txtCusName.Text = "";
                txtCusAdd1.Text = "";
                txtCusAdd2.Text = "";
                txtNIC.Text = "";
                txtMobile.Text = "";
                txtProvince.Text = "";
            }
        }

        protected void GetProvince()
        {
            if (string.IsNullOrEmpty(cmbDistrict.Text)) return;
            DistrictProvince _type = CHNLSVC.Sales.GetDistrict(cmbDistrict.Text.Trim())[0];
            if (_type.Mds_district == null)
            {
                DisplayMessage("Invalid district selected.");
                return;
            }
            txtProvince.Text = _type.Mds_province;
        }

        private void clearValiables()
        {
            _regList = new List<VehicalRegistration>();
            _insList = new List<VehicleInsuarance>();
            _HpAccount = new HpAccount();
            _SchemeDetails = new HpSchemeDetails();
            _ResList = new List<ReptPickSerials>();
            _list = new List<RecieptItem>();
            _gvDetails = new List<GiftVoucherPages>();
            _tmpRecItem = new List<ReceiptItemDetails>();
            _SchemeDefinition = new List<HpSchemeDefinition>();
            _businessEntity = new MasterBusinessEntity();
            _masterItemComponent = new List<MasterItemComponent>();
            _priceBookLevelRef = new PriceBookLevelRef();
            _priceBookLevelRefList = new List<PriceBookLevelRef>();
            _isInventoryCombineAdded = new bool();
            ScanSequanceNo = new Int32();
            ScanSerialList = new List<ReptPickSerials>();
            IsPriceLevelAllowDoAnyStatus = new bool();
            WarrantyRemarks = string.Empty;
            WarrantyPeriod = new Int32();
            ScanSerialNo = string.Empty;
            DefaultItemStatus = string.Empty;
            ManagerDiscount = new Dictionary<decimal, decimal>();
            GeneralDiscount = new CashGeneralEntiryDiscountDef();
            DefaultBook = string.Empty;
            DefaultLevel = string.Empty;
            DefaultInvoiceType = string.Empty;
            DefaultStatus = string.Empty;
            DefaultBin = string.Empty;
            _itemdetail = new MasterItem();
            _priceDetailRef = new List<PriceDetailRef>();
            _masterBusinessCompany = new MasterBusinessEntity();
            _MainPriceSerial = new List<PriceSerialRef>();
            _tempPriceSerial = new List<PriceSerialRef>();
            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            _tempPriceCombinItem = new List<PriceCombinedItemRef>();
            _lineNo = new Int32();
            _isEditPrice = new bool();
            _isEditDiscount = new bool();
            GrndSubTotal = new decimal();
            GrndDiscount = new decimal();
            _toBePayNewAmount = new decimal();
            _isCompleteCode = new bool();
            _isGiftVoucherCheckBoxClick = new bool();
            MasterChannel = new DataTable();
            IsToken = new bool();
            IsSaleFigureRoundUp = new bool();
            _tblExecutive = new DataTable();
            IsFwdSaleCancelAllowUser = new bool();
            IsDlvSaleCancelAllowUser = new bool();
            _IsVirtualItem = new bool();
            technicianCode = string.Empty;
            _iswhat = new bool();
            SSPriceBookPrice = new decimal();
            SSPriceBookSequance = string.Empty;
            SSPriceBookItemSequance = string.Empty;
            SSIsLevelSerialized = string.Empty;
            SSPromotionCode = string.Empty;
            SSCirculerCode = string.Empty;
            SSPRomotionType = new Int32();
            SSCombineLine = new Int32();
            MainTaxConstant = new List<MasterItemTax>();
            _promotionSerial = new List<ReptPickSerials>();
            _promotionSerialTemp = new List<ReptPickSerials>();
            _isBackDate = new bool();
            _MasterProfitCenter = new MasterProfitCenter();
            _PriceDefinitionRef = new List<PriceDefinitionRef>();
            InvoiceBackDateName = string.Empty;
            VirtualCounter = 0;
            InvoiceSerialList = new List<InvoiceSerial>();
            InventoryCombinItemSerialList = new List<ReptPickSerials>();
            PriceCombinItemSerialList = new List<ReptPickSerials>();
            BuyBackItemList = new List<ReptPickSerials>();
            _proVouInvcItem = string.Empty;
            _serialMatch = new bool();
            _priorityPriceBook = new PriortyPriceBook();
            _isProcess = new Boolean();
            _regAmt = new decimal();
            _invType = string.Empty;
            _invLine = new Int32();
            _accNo = string.Empty;
            _invNo = string.Empty;
            _insuTerm = new Int32();
            _colTerm = new Int32();
            _insuAmt = new decimal();
            _IsRecall = new Boolean();
            _RecStatus = new Boolean();
            _sunUpload = new Boolean();
            _isRes = new Boolean();
            _usedAmt = new decimal();
            _selectPromoCode = string.Empty;
            _selectSerial = string.Empty;
            _serverDt = new DateTime();
            chkDeliverLater = true;
            chkDeliverNow = new Boolean();
            lblPromoVouNo = string.Empty;
            lblPromoVouUsedFlag = string.Empty;
            _invoiceItemList = new List<InvoiceItem>();
            _invoiceItemListWithDiscount = new List<InvoiceItem>();
            _recieptItem = new List<RecieptItem>();
            _newRecieptItem = new List<RecieptItem>();
            _levelStatus = new DataTable();
            _isBlocked = false;
            _isItemChecking = false;
            _loyaltyType = new LoyaltyType();


            _isNewPromotionProcess = false;
            _PriceDetailRefPromo = new List<PriceDetailRef>();
            _PriceSerialRefPromo = new List<PriceSerialRef>();
            _PriceSerialRefNormal = new List<PriceSerialRef>();

            oDebitInvoices = new List<gvRegItems>();
        }

        private void ClearValiables2()
        {
            _maxAllowQty = 0;
            _NetAmt = 0;
            _TotVat = 0;
            _DisCashPrice = 0;
            _varInstallComRate = 0;
            _SchTP = "";
            _commission = 0;
            _discount = 0;
            _UVAT = 0;
            _varVATAmt = 0;
            _IVAT = 0;
            _varCashPrice = 0;
            _varInitialVAT = 0;
            _vDPay = 0;
            _varInsVAT = 0;
            _MinDPay = 0;
            _varAmountFinance = 0;
            _varIntRate = 0;
            _varInterestAmt = 0;
            _varServiceCharge = 0;
            _varInitServiceCharge = 0;
            _varServiceChargesAdd = 0;
            _varHireValue = 0;
            _varCommAmt = 0;
            _varStampduty = 0;
            _varInitialStampduty = 0;
            _varOtherCharges = 0;
            _varFInsAmount = 0;
            _varInsAmount = 0;
            _varInsCommRate = 0;
            _varInsVATRate = 0;
            _varTotCash = 0;
            _varTotalInstallmentAmt = 0;
            _varRental = 0;
            _varAddRental = 0;
            _ExTotAmt = 0;
            BalanceAmount = 0;
            PaidAmount = 0;
            BankOrOther_Charges = 0;
            AmtToPayForFinishPayment = 0;
            _isBlack = false;
            _insuAllow = false;
            _priceType = 0;
            _invoicePrefix = "";
            _varMgrComm = 0;
            _isCalProcess = false;
            _isSysReceipt = false;
            _isGV = false;
            _manCd = "";
            _isFoundTaxDef = false;
            _calMethod = 0;

            _isCombineAdding = false;
            _combineCounter = 0;
            _paymodedef = string.Empty;
            _isCheckedPriceCombine = false;
            _isFirstPriceComItem = false;
            _serial2 = string.Empty;
            _prefix = string.Empty;

            _isRegistrationMandatory = false;

            GrndTax = 0;
        }

        private void LoadCachedObjects()
        {
            _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
            MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
            IsSaleFigureRoundUp = CacheLayer.Get(CacheLayer.Key.IsSaleValueRoundUp.ToString());
            _MasterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());
            _PriceDefinitionRef = CacheLayer.Get<List<PriceDefinitionRef>>(CacheLayer.Key.PriceDefinition.ToString());
            MasterChannel = CacheLayer.Get<DataTable>(CacheLayer.Key.ChannelDefinition.ToString());
            IsSaleFigureRoundUp = CacheLayer.Get(CacheLayer.Key.IsSaleValueRoundUp.ToString());
        }

        private void LoadPriceDefaultValue()
        {
            if (_PriceDefinitionRef != null) if (_PriceDefinitionRef.Count > 0)
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
                }
        }

        private void LoadPriceLevelMessage()
        {
            DataTable _msg = CHNLSVC.Sales.GetPriceLevelMessage(Session["UserCompanyCode"].ToString(), cmbBook.Text, cmbLevel.Text);
            if (_msg != null && _msg.Rows.Count > 0)
                lblLvlMsg.Text = _msg.Rows[0].Field<string>("Sapl_spmsg");
            else
                lblLvlMsg.Text = string.Empty;
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
                    cmbBook.DataSource = _books;
                    cmbBook.DataBind();
                    cmbBook.SelectedIndex = cmbBook.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultBook)) cmbBook.Text = DefaultBook;
                }
                else
                {
                    cmbBook.DataSource = null;
                    cmbBook.DataBind();
                }

            else
            {
                cmbBook.DataSource = null;
                cmbBook.DataBind();
            }

            return _isAvailable;
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
                        else
                            IsPriceLevelAllowDoAnyStatus = true;
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
                    _isAvailable = true;
                    var _types = _levelStatus.AsEnumerable().Select(x => x.Field<string>("Code")).Distinct().ToList();
                    _types.Add("");
                    cmbStatus.DataSource = _types;
                    cmbStatus.DataBind();
                    cmbStatus.SelectedIndex = cmbStatus.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultInvoiceType)) cmbStatus.Text = DefaultStatus;
                    //Load Level definition
                    _priceBookLevelRef = CHNLSVC.Sales.GetPriceLevel(Session["UserCompanyCode"].ToString(), cmbBook.Text, cmbLevel.Text);
                    LoadPriceLevelMessage();
                }
                else
                {
                    cmbStatus.DataSource = null;
                    cmbStatus.DataBind();
                }
            else
            {
                cmbStatus.DataSource = null;
                cmbStatus.DataBind();
            }
            return _isAvailable;
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
                    cmbLevel.DataSource = _levels;
                    cmbLevel.DataBind();
                    cmbLevel.SelectedIndex = cmbLevel.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultLevel) && !string.IsNullOrEmpty(cmbBook.Text))
                        cmbLevel.Text = DefaultLevel;
                    _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(Session["UserCompanyCode"].ToString(), _book.Trim(), cmbLevel.Text.Trim());
                    LoadPriceLevelMessage();
                }
                else
                {
                    cmbLevel.DataSource = null;
                    cmbLevel.DataBind();
                }
            else
            {
                cmbLevel.DataSource = null;
                cmbLevel.DataBind();
            }

            return _isAvailable;
        }

        private void Load_District()
        {
            cmbDistrict.DataSource = new List<DistrictProvince>();
            List<DistrictProvince> _district = CHNLSVC.Sales.GetDistrict("");
            var _final = (from _lst in _district
                          select _lst.Mds_district).ToList();

            cmbDistrict.DataSource = _final.ToList();
            cmbDistrict.DataBind();
        }

        private bool LoadInvoiceType()
        {
            bool _isAvailable = false;
            if (_PriceDefinitionRef != null)
            {
                if (_PriceDefinitionRef.Count > 0)
                {
                    _isAvailable = true;
                    var _types = _PriceDefinitionRef.Select(x => x.Sadd_doc_tp).Distinct().ToList();
                    _types.Add("");

                    cmbInvType.DataSource = null;
                    cmbInvType.DataBind();

                    cmbInvType.DataSource = _types;
                    cmbInvType.DataBind();
                    cmbInvType.SelectedIndex = cmbInvType.Items.Count - 1;
                    if (!string.IsNullOrEmpty(DefaultInvoiceType))
                    {
                        cmbInvType.Text = DefaultInvoiceType;
                    }
                }
                else
                {
                    cmbInvType.DataSource = null;
                    cmbInvType.DataBind();
                }
            }
            else
            {
                cmbInvType.DataSource = null;
                cmbInvType.DataBind();
            }
            return _isAvailable;
        }

        private void calc_revert_charge(string _invTp, string _prevProv)
        {
            List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
            string _Subchannel = "";
            string _typeSubChnl = "SCHNL";

            string _Mainchannel = "";
            string _typeMainChanl = "CHNL";

            string _Pctype = "PC";
            string _typePc = Session["UserDefProf"].ToString();

            _Subchannel = (from _lst in _Saleshir
                           where _lst.Mpi_cd == "SCHNL"
                           select _lst.Mpi_val).ToList<string>()[0];


            _Mainchannel = (from _lst in _Saleshir
                            where _lst.Mpi_cd == "CHNL"
                            select _lst.Mpi_val).ToList<string>()[0];

            MasterItem _mstItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper());
            string _cate1 = _mstItm.Mi_cate_1;
            string _cate2 = _mstItm.Mi_cate_2;
            string _cate3 = _mstItm.Mi_cate_3;
            string _brand = _mstItm.Mi_brand;

            Decimal _rvtChg = 0;


            DataTable _dtChg = new DataTable();
            _dtChg = CHNLSVC.Financial.Getrevertregistrationcharge(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, _invTp, txtItem.Text.ToUpper().Trim(), _cate1, _cate2, _cate3, _brand, _prevProv, txtProvince.Text, Convert.ToDateTime(dtpRecDate.Text).Date);
            if (_dtChg.Rows.Count > 0)
            {
                if (Convert.ToDecimal(_dtChg.Rows[0]["svr_is_rt"]) > 0)
                    _rvtChg = 99999;
                else
                    _rvtChg = Convert.ToDecimal(_dtChg.Rows[0]["svr_val"]);

            }
            _dtChg = CHNLSVC.Financial.Getrevertregistrationcharge(Session["UserCompanyCode"].ToString(), _Subchannel, _typeSubChnl, _invTp, txtItem.Text.ToUpper().Trim(), _cate1, _cate2, _cate3, _brand, _prevProv, txtProvince.Text, Convert.ToDateTime(dtpRecDate.Text).Date);
            if (_dtChg.Rows.Count > 0)
            {
                if (Convert.ToDecimal(_dtChg.Rows[0]["svr_is_rt"]) > 0)
                    _rvtChg = 99999;
                else
                    _rvtChg = Convert.ToDecimal(_dtChg.Rows[0]["svr_val"]);
            }
            _dtChg = CHNLSVC.Financial.Getrevertregistrationcharge(Session["UserCompanyCode"].ToString(), _Mainchannel, _typeMainChanl, _invTp, txtItem.Text.ToUpper().Trim(), _cate1, _cate2, _cate3, _brand, _prevProv, txtProvince.Text, Convert.ToDateTime(dtpRecDate.Text).Date);
            if (_dtChg.Rows.Count > 0)
            {
                if (Convert.ToDecimal(_dtChg.Rows[0]["svr_is_rt"]) > 0)
                    _rvtChg = 99999;
                else
                    _rvtChg = Convert.ToDecimal(_dtChg.Rows[0]["svr_val"]);
            }
            _dtChg = CHNLSVC.Financial.Getrevertregistrationcharge(Session["UserCompanyCode"].ToString(), "COM", Session["UserCompanyCode"].ToString(), _invTp, txtItem.Text.ToUpper().Trim(), _cate1, _cate2, _cate3, _brand, _prevProv, txtProvince.Text, Convert.ToDateTime(dtpRecDate.Text).Date);
            if (_dtChg.Rows.Count > 0)
            {
                if (Convert.ToDecimal(_dtChg.Rows[0]["svr_is_rt"]) > 0)
                    _rvtChg = 99999;
                else
                    _rvtChg = Convert.ToDecimal(_dtChg.Rows[0]["svr_val"]);
            }
            txtPayment.Text = (Convert.ToDecimal(txtPayment.Text) + _rvtChg).ToString();
            lblExtraChg.Text = _rvtChg.ToString("0.00");
        }

        private void setDTPEnable(bool status)
        {
            if (status)
            {
                dtpRecDate.CssClass = "buttonUndocolor";
                btndtpRecDate.CssClass = "buttonUndocolor";
                dtpRecDateCal.Enabled = true;

                dtpValidTill.CssClass = "buttonUndocolor";
                btnPriceValidTill.CssClass = "buttonUndocolor";
                dtpValidTillCal.Enabled = true;

            }
            else
            {
                dtpRecDate.CssClass = "buttoncolorleft";
                btndtpRecDate.CssClass = "buttoncolorleft";
                dtpRecDateCal.Enabled = false;

                dtpValidTill.CssClass = "buttoncolorleft";
                btnPriceValidTill.CssClass = "buttoncolorleft";
                dtpValidTillCal.Enabled = false;
            }
        }

        private Boolean GetItemDetails(string _itm)
        {
            Boolean _isvalid = true;
            try
            {
                if (!string.IsNullOrEmpty(_itm))
                {
                    MasterItem _masterItemDetails = CHNLSVC.Sales.getMasterItemDetails(Session["UserCompanyCode"].ToString(), _itm, 1);

                    if (_masterItemDetails.Mi_cd != null)
                    {
                        if (_masterItemDetails.Mi_hp_allow == true)
                        {
                            MasterItemBrand _itemBrand = CHNLSVC.Sales.GetItemBrand(_masterItemDetails.Mi_brand);
                            txtQty.Text = "1";
                            btnConfirm.Focus();
                        }
                        else
                        {
                            List<MasterItemComponent> _masterItemComponent = CHNLSVC.Inventory.GetItemComponents(txtItem.Text.ToUpper());
                            if (_masterItemComponent == null)
                            {
                                DisplayMessageJS("Selected item " + txtItem.Text.ToUpper() + " is not allow to HP.");
                                txtItem.Text = "";
                                txtQty.Text = "";
                                txtItem.Focus();
                                _isvalid = false;
                                return _isvalid;
                            }
                            else
                            {
                                MasterItemBrand _itemBrand = CHNLSVC.Sales.GetItemBrand(_masterItemDetails.Mi_brand);
                                txtQty.Text = "1";
                                btnConfirm.Focus();
                            }
                        }
                    }
                    else
                    {
                        DisplayMessageJS("Selected item is invalid.");
                        txtItem.Text = "";
                        txtItem.Focus();
                        _isvalid = false;
                        return _isvalid;
                    }
                }
                return _isvalid;
            }
            catch (Exception ex)
            {
                _isvalid = false;
                return _isvalid;
                DisplayMessage(ex.Message, 4, ex);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private bool LoadItemDetail(string _item)
        {
            _itemdetail = new MasterItem();
            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);
            if (_itemdetail != null && !string.IsNullOrEmpty(_itemdetail.Mi_cd))
            {
                _isValid = true;
                string _description = _itemdetail.Mi_longdesc;
                string _model = _itemdetail.Mi_model;
                string _brand = _itemdetail.Mi_brand;
                string _serialstatus = _itemdetail.Mi_is_ser1 == 1 ? "Available" : "Non";
            }
            else
                _isValid = false;
            return _isValid;
        }

        private bool IsGiftVoucher(string _type)
        {
            if (_type == "G")
                return true;
            else
                return false;
        }

        private bool IsVirtual(string _type)
        {
            if (_type == "V")
            {
                _IsVirtualItem = true;
                return true;
            }
            else
            {
                _IsVirtualItem = false;
                return false;
            }
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

                }
            }
            return _isBlocked;
        }

        protected bool CheckQty(bool _isSearchPromotion, string target)
        {
            //if (pnlPriceNPromotion.Visible == true)
            //    return true;

            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
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
                    DisplayMessageJS("This compete code does not having a collection. Please contact inventory");
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            if (CheckQtyPriliminaryRequirements()) return true;

            if (_isCombineAdding == false)
                if (CheckTaxAvailability())
                {
                    DisplayMessageJS("Tax rates not setup for selected item code and item status.Please contact Inventory Department.");
                    _IsTerminate = true;
                    return _IsTerminate;
                }

            if (_isCombineAdding == false)
                CheckItemTax(txtItem.Text.ToUpper().Trim());
            if (_isCombineAdding == false)
                if (CheckProfitCenterAllowForWithoutPrice())
                {
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            if (_isCombineAdding == false)
                if (ConsumerItemProduct())
                {
                    // _IsTerminate = true;
                    // return _IsTerminate;
                }
            if (_isSearchPromotion)
                if (CheckItemPromotion())
                {
                    _IsTerminate = true; return _IsTerminate;
                }
            if (_isCombineAdding == false && _priceBookLevelRef.Sapl_is_serialized)
                if (CheckSerializedPriceLevelAndLoadSerials(true))
                {
                    _IsTerminate = true;
                    return _IsTerminate;
                }
            if (IsGiftVoucher(_itemdetail.Mi_itm_tp)) return true;
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
            _priceDetailRef = new List<PriceDetailRef>();
            _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCusCode.Text, txtItem.Text.ToUpper(), Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtpRecDate.Text));
            if (_priceDetailRef.Count <= 0)
            {
                if (!_isCompleteCode)
                {
                    DisplayMessageJS("There is no price for the selected item");
                    SetDecimalTextBoxForZero(true, false, true);
                    _IsTerminate = true;
                    return _IsTerminate;
                }
                else
                {
                    txtUnitPrice.Text = FormatToCurrency("0");
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
                        DisplayMessageJS("Price has been suspended. Please contact IT dept.");
                        _IsTerminate = true;
                        // pnlMain.Enabled = true;
                        return _IsTerminate;
                    }
                }
                if (_priceDetailRef.Count > 1)
                {
                    SetColumnForPriceDetailNPromotion(false);
                    gvNormalPrice.DataSource = new List<PriceDetailRef>();
                    gvNormalPrice.DataBind();
                    gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                    gvPromotionPrice.DataBind();
                    gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                    gvPromotionItem.DataBind();
                    gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                    gvPromotionSerial.DataBind();
                    BindNonSerializedPrice(_priceDetailRef);
                    pnlPriceNPromotion.Visible = true;
                    _IsTerminate = true;
                    // pnlMain.Enabled = false;

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
                        decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                        WarrantyRemarks = _single.Sapd_warr_remarks;
                        SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                        Int32 _pbSq = _single.Sapd_pb_seq;
                        Int32 _pbiSq = _single.Sapd_seq_no;
                        string _mItem = _single.Sapd_itm_cd;
                        //if (_promotion.Sarpt_is_com)
                        //{
                        SetColumnForPriceDetailNPromotion(false);
                        gvNormalPrice.DataSource = new List<PriceDetailRef>();
                        gvNormalPrice.DataBind();
                        gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                        gvPromotionPrice.DataBind();
                        gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                        gvPromotionItem.DataBind();
                        gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                        gvPromotionSerial.DataBind();
                        BindNonSerializedPrice(_priceDetailRef);

                        if (gvPromotionPrice.Rows.Count > 0)
                        {
                            // gvPromotionPrice_CellDoubleClick(0, false, false);
                            // pnlPriceNPromotion.Visible = true;
                            //// pnlMain.Enabled = false;
                            // _IsTerminate = true;
                            // return _IsTerminate;
                        }
                        else
                        {
                            if (_isCombineAdding == false) txtUnitPrice.Focus();
                        }

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
            if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
            decimal vals = Convert.ToDecimal(txtQty.Text);
            txtQty.Text = FormatToQty(Convert.ToString(vals));
            CalculateItem();

            //get price for priority pb
            if (_priorityPriceBook != null && cmbBook.SelectedValue != _priorityPriceBook.Sppb_pb && cmbBook.SelectedValue != _priorityPriceBook.Sppb_pb_lvl)
            {
                decimal normalPrice = Convert.ToDecimal(txtLineTotAmt.Text);

                _priceDetailRef = CHNLSVC.Sales.GetPrice_01(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, _priorityPriceBook.Sppb_pb, _priorityPriceBook.Sppb_pb_lvl, txtCusCode.Text, txtItem.Text.ToUpper(), Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtpRecDate.Text));
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
                        _unitPrice = FormatToCurrency("0");
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
                        using (new CenterWinDialog(this)) { _result = MessageBox.Show("This item has " +_priorityPriceBook.Sppb_pb + " " + _priorityPriceBook.Sppb_pb_lvl + " Promotion."+"\nDo you want to select " + _priorityPriceBook.Sppb_pb + " Promotion?", "Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
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
                            decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                            _unitPrice = FormatToCurrency(Convert.ToString(UnitPrice));
                            WarrantyRemarks = _single.Sapd_warr_remarks;
                            //SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                            Int32 _pbSq = _single.Sapd_pb_seq;
                            Int32 _pbiSq = _single.Sapd_seq_no;
                            string _mItem = _single.Sapd_itm_cd;
                            //if (_promotion.Sarpt_is_com)
                            //{
                            SetColumnForPriceDetailNPromotion(false);
                            gvNormalPrice.DataSource = new List<PriceDetailRef>();
                            gvNormalPrice.DataBind();
                            gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                            gvPromotionPrice.DataBind();
                            gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                            gvPromotionItem.DataBind();
                            gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                            gvPromotionSerial.DataBind();
                            BindNonSerializedPrice(_priceDetailRef);

                            if (gvPromotionPrice.Rows.Count > 0)
                            {
                                //gvPromotionPrice_CellDoubleClick(0, false, false);
                                //pnlPriceNPromotion.Visible = true;
                                //pnlMain.Enabled = false;
                                //_IsTerminate = true;
                                //return _IsTerminate;
                            }
                            else
                            {
                                if (_isCombineAdding == false) txtUnitPrice.Focus();
                            }

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
                if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
                decimal vals1 = Convert.ToDecimal(txtQty.Text);
                txtQty.Text = FormatToQty(Convert.ToString(vals1));
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
                    string msg = _priorityPriceBook.Sppb_pb + " " + _priorityPriceBook.Sppb_pb_lvl + " Price - " + FormatToCurrency(otherPrice.ToString()) + "\nDo you want to select " + _priorityPriceBook.Sppb_pb + " Price?";
                    if (Con_selectPriceBookPrice.Value == "Y")
                    {
                        txtUnitPrice.Text = FormatToCurrency("0");
                        txtUnitAmt.Text = FormatToCurrency("0");
                        txtDisRate.Text = FormatToCurrency("0");
                        txtDisAmt.Text = FormatToCurrency("0");
                        txtTaxAmt.Text = FormatToCurrency("0");
                        txtLineTotAmt.Text = FormatToCurrency("0");
                        cmbBook.Text = _priorityPriceBook.Sppb_pb;
                        cmbLevel.Text = _priorityPriceBook.Sppb_pb_lvl;
                        CheckQty(false, "CheckQty");
                    }
                    else if (Con_selectPriceBookPrice.Value == "N")
                    {
                        SSPromotionCode = string.Empty;
                        SSPRomotionType = 0;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "selectPriceBookPrice('" + msg + "');", true);
                        return false;
                    }

                    //_??DialogResult _result = new DialogResult();
                    //_??using (new CenterWinDialog(this)) { _result = MessageBox.Show(_priorityPriceBook.Sppb_pb + " " + _priorityPriceBook.Sppb_pb_lvl + " Price - " + FormatToCurrency(otherPrice.ToString()) + "\nDo you want to select " + _priorityPriceBook.Sppb_pb + " Price?", "Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1); }
                    //_??
                    //_??if (_result == DialogResult.Yes)
                    //_??{
                    //_??    txtUnitPrice.Text = FormatToCurrency("0");
                    //_??    txtUnitAmt.Text = FormatToCurrency("0");
                    //_??    txtDisRate.Text = FormatToCurrency("0");
                    //_??    txtDisAmt.Text = FormatToCurrency("0");
                    //_??    txtTaxAmt.Text = FormatToCurrency("0");
                    //_??    txtLineTotAmt.Text = FormatToCurrency("0");
                    //_??    cmbBook.Text = _priorityPriceBook.Sppb_pb;
                    //_??    cmbLevel.Text = _priorityPriceBook.Sppb_pb_lvl;
                    //_??    CheckQty(false);
                    //_??}
                    //_??else
                    //_??{
                    //_??    SSPRomotionType = 0;
                    //_??    //SSCirculerCode = string.Empty;
                    //_??    //SSPriceBookItemSequance = string.Empty;
                    //_??    //SSPriceBookPrice = Convert.ToDecimal(0);
                    //_??    //SSPriceBookSequance = string.Empty;
                    //_??    SSPromotionCode = string.Empty;
                    //_??    /*
                    //_??    _priceDetailRef = CHNLSVC.Sales.GetPrice(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCusCode.Text, txtItem.Text, Convert.ToDecimal(txtQty.Text), Convert.ToDateTime(dtpRecDate.Value.Date));
                    //_??    if (_priceDetailRef.Count == 1)
                    //_??    {
                    //_??        var _one = from _itm in _priceDetailRef
                    //_??                   select _itm;
                    //_??        int _priceType = 0;
                    //_??        foreach (var _single in _one)
                    //_??        {
                    //_??            _priceType = _single.Sapd_price_type;
                    //_??            PriceTypeRef _promotion = TakePromotion(_priceType);
                    //_??            decimal UnitPrice = FigureRoundUp(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _single.Sapd_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false), true);
                    //_??            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                    //_??            WarrantyRemarks = _single.Sapd_warr_remarks;
                    //_??            SetSSPriceDetailVariable(_single.Sapd_circular_no, Convert.ToString(_single.Sapd_seq_no), Convert.ToString(_single.Sapd_pb_seq), Convert.ToString(_single.Sapd_itm_price), _single.Sapd_promo_cd, Convert.ToString(_single.Sapd_price_type));
                    //_??            Int32 _pbSq = _single.Sapd_pb_seq;
                    //_??            Int32 _pbiSq = _single.Sapd_seq_no;
                    //_??            string _mItem = _single.Sapd_itm_cd;
                    //_??            //if (_promotion.Sarpt_is_com)
                    //_??            //{
                    //_??            SetColumnForPriceDetailNPromotion(false);
                    //_??            gvNormalPrice.DataSource = new List<PriceDetailRef>();
                    //_??            gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                    //_??            gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                    //_??            gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                    //_??            BindNonSerializedPrice(_priceDetailRef);
                    //__??
                    //_??            if (gvPromotionPrice.RowCount > 0)
                    //_??            {
                    //_??                gvPromotionPrice_CellDoubleClick(0, false, false);
                    //_??                pnlPriceNPromotion.Visible = true;
                    //_??                pnlMain.Enabled = false;
                    //_??                _IsTerminate = true;
                    //_??                return _IsTerminate;
                    //_??            }
                    //_??            else
                    //_??            {
                    //_??                if (_isCombineAdding == false) txtUnitPrice.Focus();
                    //_??            }
                    //__??
                    //_??            //}
                    //_??            //else
                    //_??            //{
                    //_??            //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                    //_??            //}
                    //_??        }
                    //_??    }
                    //_??    _isEditPrice = false;
                    //_??    _isEditDiscount = false;
                    //_??    if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
                    //_??    decimal vals2 = Convert.ToDecimal(txtQty.Text);
                    //_??    txtQty.Text = FormatToQty(Convert.ToString(vals2));
                    //_??    CalculateItem();
                    //_??     */
                    //_??}
                }
            }

            return _IsTerminate;
        }

        private bool CheckInventoryCombine()
        {
            bool _IsTerminate = false;
            _isCompleteCode = false;

            if (!string.IsNullOrEmpty(TxtAdvItem.Text))
            {
                MasterItem _itemDet = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), TxtAdvItem.Text.ToUpper().Trim().ToUpper());
                ////if (_itemDet.Mi_itm_tp == "V" && _itemDet.Mi_is_subitem == true)
                ////    _isCompleteCode = BindItemComponent(TxtAdvItem.Text.Trim());

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

        private bool CheckQtyPriliminaryRequirements()
        {
            bool _IsTerminate = false;
            if (string.IsNullOrEmpty(TxtAdvItem.Text))
            {
                _IsTerminate = true; return _IsTerminate;
            }
            if (IsNumeric(txtQty.Text) == false)
            {
                DisplayMessageJS("Please select the valid qty");
                _IsTerminate = true;
                txtQty.Focus();
                return _IsTerminate;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0)
            {
                _IsTerminate = true;
                return _IsTerminate;
            }

            if (_itemdetail.Mi_is_ser1 == 1 && !string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                txtQty.Text = FormatToQty("1");
            _MainPriceCombinItem = new List<PriceCombinedItemRef>();
            if (string.IsNullOrEmpty(txtQty.Text))
            {
                CalculateItem();
                SSPriceBookItemSequance = "0";
                SSPriceBookPrice = 0;
                SSPriceBookSequance = "0";
                WarrantyPeriod = 0;
                WarrantyRemarks = string.Empty;
                _IsTerminate = true;
                return _IsTerminate;
            }
            if (Convert.ToDecimal(txtQty.Text) <= 0)
            {
                CalculateItem(); SSPriceBookItemSequance = "0"; SSPriceBookPrice = 0; SSPriceBookSequance = "0"; WarrantyPeriod = 0; WarrantyRemarks = string.Empty; _IsTerminate = true; return _IsTerminate;
            }
            if (string.IsNullOrEmpty(cmbInvType.Text))
            {
                DisplayMessageJS("Please select the invoice type");
                _IsTerminate = true;
                cmbInvType.Focus();
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(txtCusCode.Text))
            {
                DisplayMessageJS("Please select the customer");
                _IsTerminate = true;
                txtCusCode.Focus();
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(txtItem.Text.ToUpper()))
            {
                DisplayMessageJS("Please select the item");
                _IsTerminate = true;
                txtItem.Focus();
                return _IsTerminate;
            }

            if (string.IsNullOrEmpty(cmbBook.Text))
            {
                DisplayMessageJS("Price book not select.");
                _IsTerminate = true;
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(cmbLevel.Text))
            {
                DisplayMessageJS("Please select the price level");
                _IsTerminate = true;
                cmbLevel.Focus();
                return _IsTerminate;
            }
            if (string.IsNullOrEmpty(cmbStatus.Text))
            {
                DisplayMessageJS("Please select the item status");
                _IsTerminate = true;
                cmbStatus.Focus();
                return _IsTerminate;
            }
            return _IsTerminate;
        }

        private void CalculateItem()
        {
            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtUnitPrice.Text))
            {
                txtUnitAmt.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(txtUnitPrice.Text.Trim()) * Convert.ToDecimal(txtQty.Text.Trim()), true)));

                decimal _vatPortion = FigureRoundUp(TaxCalculation(txtItem.Text.ToUpper().Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true), true);
                txtTaxAmt.Text = FormatToCurrency(Convert.ToString(_vatPortion));

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
                            List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                            if (_tax != null && _tax.Count > 0)
                            {
                                decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                                txtTaxAmt.Text = Convert.ToString(FigureRoundUp(_vatval, true));
                            }
                        }
                    }

                    txtDisAmt.Text = FormatToCurrency(Convert.ToString(_disAmt));
                }

                if (!string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    if (Convert.ToDecimal(txtDisRate.Text) > 0)
                        _totalAmount = FigureRoundUp(_totalAmount + _vatPortion - _disAmt, true);
                    else
                        _totalAmount = FigureRoundUp(_totalAmount + Convert.ToDecimal(txtTaxAmt.Text) - _disAmt, true);
                }

                txtLineTotAmt.Text = FormatToCurrency(Convert.ToString(_totalAmount));
                if (cmbInvType.Text == "HS")
                {
                    if (_totalAmount > 0)
                    {
                        // LoadScheme();
                    }
                }
            }
        }

        private bool CheckTaxAvailability()
        {
            bool _IsTerminate = false;
            //Check for tax setup  - under Darshana confirmation on 02/06/2012
            if (!_isCompleteCode)
            {
                if (Convert.ToDateTime(dtpRecDate.Text).Date == _serverDt)
                {
                    List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), TxtAdvItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty);
                    if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                        _IsTerminate = true;
                    if (_tax.Count <= 0)
                        _IsTerminate = true;
                }
                else
                {
                    List<MasterItemTax> _taxEff = CHNLSVC.Sales.GetItemTaxEffDt(Session["UserCompanyCode"].ToString(), TxtAdvItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty, Convert.ToDateTime(dtpRecDate.Text).Date);
                    if (_taxEff.Count <= 0)
                    {
                        List<LogMasterItemTax> _tax = CHNLSVC.Sales.GetItemTaxLog(Session["UserCompanyCode"].ToString(), TxtAdvItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim(), "VAT", string.Empty, Convert.ToDateTime(dtpRecDate.Text).Date);
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
            if (_priceBookLevelRef.Sapl_vat_calc == true)
            {
                MainTaxConstant = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), _item, cmbStatus.Text.Trim());
            }
        }

        private bool CheckProfitCenterAllowForWithoutPrice()
        {
            bool _isAvailable = false;
            if (_MasterProfitCenter.Mpc_without_price && _priceBookLevelRef.Sapl_is_without_p)
            {
                SetDecimalTextBoxForZero(false, false, false);
                _isAvailable = true;
                return _isAvailable;
            }
            return _isAvailable;
        }

        private void SetDecimalTextBoxForZero(bool _isUnit, bool _isAccBal, bool _isQty)
        {
            txtDisRate.Text = FormatToCurrency("0");
            txtDisAmt.Text = FormatToCurrency("0");
            if (_isQty)
            {
                txtQty.Text = FormatToQty("1");
            }
            txtTaxAmt.Text = FormatToCurrency("0");
            if (_isUnit)
            {
                txtUnitPrice.Text = FormatToCurrency("0");
            }
            txtUnitAmt.Text = FormatToCurrency("0");
            txtLineTotAmt.Text = FormatToCurrency("0");
            if (_isAccBal)
            {

            }
        }

        private bool ConsumerItemProduct()
        {
            bool _isAvailable = false;
            bool _isMRP = _itemdetail.Mi_anal3;
            ////   if (_isMRP && chkDeliverLater == false && chkDeliverNow == false)
            {
                List<InventoryBatchRefN> _batchRef = new List<InventoryBatchRefN>();
                if (_priceBookLevelRef.Sapl_chk_st_tp)
                    _batchRef = CHNLSVC.Sales.GetConsumerProductPriceList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim());
                else
                    _batchRef = CHNLSVC.Sales.GetConsumerProductPriceList(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), string.Empty);
                if (_batchRef.Count > 0)
                    if (_batchRef.Count > 1)
                    {
                        //pnlMain.Enabled = false;
                        //pnlConsumerPrice.Visible = true;
                        //BindConsumableItem(_batchRef);
                    }
                    else if (_batchRef.Count == 1)
                    {
                        if (_batchRef[0].Inb_free_qty < Convert.ToDecimal(txtQty.Text.Trim()))
                        {
                            DisplayMessageJS("Invoice qty is " + txtQty.Text + " and inventory available qty having only " + _batchRef[0].Inb_free_qty.ToString());
                            _isAvailable = true;
                            return _isAvailable;
                        }
                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(_batchRef[0].Inb_unit_price * CheckSubItemTax(_batchRef[0].Inb_itm_cd))));
                        txtUnitPrice.Focus();
                        _isAvailable = false;
                    }
                _isEditPrice = false;
                _isEditDiscount = false;
                if (string.IsNullOrEmpty(txtQty.Text)) txtQty.Text = FormatToQty("0");
                decimal val = Convert.ToDecimal(txtQty.Text);
                txtQty.Text = FormatToQty(Convert.ToString(val));
                CalculateItem();
                _isAvailable = true;
            }
            return _isAvailable;
        }

        private decimal CheckSubItemTax(string _item)
        {
            decimal _fraction = 1;
            List<MasterItemTax> TaxConstant = new List<MasterItemTax>();
            if (_priceBookLevelRef.Sapl_vat_calc == true)
            {
                TaxConstant = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), _item, cmbStatus.Text.Trim());
                if (TaxConstant != null)
                    if (TaxConstant.Count > 0)
                        _fraction = TaxConstant[0].Mict_tax_rate;
            }
            return _fraction;
        }

        private bool CheckItemPromotion()
        {
            _isNewPromotionProcess = false;
            if (string.IsNullOrEmpty(txtItem.Text.ToUpper()))
            {
                DisplayMessageJS("Please select the item");
                return false;
            }
            _PriceDetailRefPromo = null;
            _PriceSerialRefPromo = null;
            _PriceSerialRefNormal = null;

            List<PriceDetailRef> _PriceDetailRefPromoTemp = new List<PriceDetailRef>();
            List<PriceSerialRef> _PriceSerialRefPromoTemp = new List<PriceSerialRef>();
            List<PriceSerialRef> _PriceSerialRefNormalTemp = new List<PriceSerialRef>();

            CHNLSVC.Sales.GetPromotion(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), TxtAdvItem.Text.ToUpper().Trim(), Convert.ToDateTime(dtpRecDate.Text).Date, txtCusCode.Text.Trim(), out _PriceDetailRefPromoTemp, out _PriceSerialRefPromoTemp, out _PriceSerialRefNormalTemp);
            if (_PriceDetailRefPromoTemp == null && _PriceSerialRefPromoTemp == null && _PriceSerialRefNormalTemp == null)
                return false;
            if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0 && !string.IsNullOrEmpty(txtSerialNo.Text))
            {
                var _isSerialAvailable = _PriceSerialRefNormal.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
                if (_isSerialAvailable != null && _isSerialAvailable.Count > 0)
                {
                    if (Con_selectnormalserializedprice.Value == "Y")
                    {
                        _isNewPromotionProcess = true;
                        CheckSerializedPriceLevelAndLoadSerials(true);
                        return true;
                    }
                    else if (Con_selectnormalserializedprice.Value == "N")
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "selectnormalserializedprice();", true);
                        return false;
                    }
                    //__??DialogResult _normalSerialized = new DialogResult();
                    //__??using (new CenterWinDialog(this))
                    //__??{
                    //__??    _normalSerialized = MessageBox.Show("This item is having normal serialized price. Do you need to select normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //__??}
                    //__??if (_normalSerialized == System.Windows.Forms.DialogResult.Yes)
                    //__??{
                    //__??    _isNewPromotionProcess = true;
                    //__??    CheckSerializedPriceLevelAndLoadSerials(true);
                    //__??    return true;
                    //__??}
                }
                else
                {
                    _isNewPromotionProcess = false;
                    _PriceSerialRefNormal = null;
                }
            }
            else if (_PriceSerialRefNormal != null && _PriceSerialRefNormal.Count > 0 && string.IsNullOrEmpty(txtSerialNo.Text))
            {

                if (Con_continuewithnormalSerializedprice.Value == "Y")
                {
                    _isNewPromotionProcess = true;
                    CheckSerializedPriceLevelAndLoadSerials(true);
                    return true;
                }
                else if (Con_continuewithnormalSerializedprice.Value == "N")
                {
                    _isNewPromotionProcess = false;
                    _PriceSerialRefNormal = null;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "continuewithnormalSerializedprice();", true);
                    return false;
                }
                //_??DialogResult _normalSerialized = new System.Windows.Forms.DialogResult();
                //_??using (new CenterWinDialog(this))
                //_??{
                //_??    _normalSerialized = MessageBox.Show("This item having normal serialized price. Do you need to continue with normal serialized price?", "Normal Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //_??}
                //_??if (_normalSerialized == System.Windows.Forms.DialogResult.Yes)
                //_??{
                //_??    _isNewPromotionProcess = true;
                //_??    CheckSerializedPriceLevelAndLoadSerials(true);
                //_??    return true;
                //_??}
                //_??else
                //_??{
                //_??    _isNewPromotionProcess = false;
                //_??    _PriceSerialRefNormal = null;
                //_??}
            }
            if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0 && !string.IsNullOrEmpty(txtSerialNo.Text))
            {
                var _isSerialPromoAvailable = _PriceSerialRefPromo.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
                if (_isSerialPromoAvailable != null && _isSerialPromoAvailable.Count > 0)
                {
                    if (Con_selectPromotionalSerializedPrice.Value == "Y")
                    {
                        _isNewPromotionProcess = true;
                        CheckSerializedPriceLevelAndLoadSerials(true);
                        return true;

                    }
                    else if (Con_selectPromotionalSerializedPrice.Value == "N")
                    {
                        _isNewPromotionProcess = false;
                        _PriceSerialRefPromo = null;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "selectPromotionalSerializedPrice();", true);
                        return false;
                    }
                    //_??DialogResult _promoSerialized = new System.Windows.Forms.DialogResult();
                    //_??using (new CenterWinDialog(this))
                    //_??{
                    //_??    _promoSerialized = MessageBox.Show("This item is having promotional serialized price.Do you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //_??}
                    //_??if (_promoSerialized == System.Windows.Forms.DialogResult.Yes)
                    //_??{
                    //_??    _isNewPromotionProcess = true;
                    //_??    CheckSerializedPriceLevelAndLoadSerials(true);
                    //_??    return true;
                    //_??}
                    //_??else
                    //_??{
                    //_??    _isNewPromotionProcess = false;
                    //_??    _PriceSerialRefPromo = null;
                    //_??}
                }
                else if (_PriceSerialRefPromo != null && _PriceSerialRefPromo.Count > 0 && string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    if (Con_selectPromotionalSerializedPriceTwo.Value == "Y")
                    {
                        _isNewPromotionProcess = true;
                        CheckSerializedPriceLevelAndLoadSerials(true);
                        return true;
                    }
                    else if (Con_selectPromotionalSerializedPriceTwo.Value == "N")
                    {
                        _isNewPromotionProcess = false;
                        _PriceSerialRefPromo = null;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "selectPromotionalSerializedPriceTwo();", true);
                        return false;
                    }
                    //_??DialogResult _promoSerialized = new System.Windows.Forms.DialogResult();
                    //_??using (new CenterWinDialog(this))
                    //_??{
                    //_??    _promoSerialized = MessageBox.Show("This item is having promotional serialized price.\nDo you need to select promotional serialized price?", "Promotional Serialized Price", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //_??}
                    //_??if (_promoSerialized == System.Windows.Forms.DialogResult.Yes)
                    //_??{
                    //_??    _isNewPromotionProcess = true;
                    //_??    CheckSerializedPriceLevelAndLoadSerials(true);
                    //_??    return true;
                    //_??}
                    //_??else
                    //_??{
                    //_??    _isNewPromotionProcess = false;
                    //_??    _PriceSerialRefPromo = null;
                    //_??}
                }
            }
            if (_PriceDetailRefPromoTemp != null && _PriceDetailRefPromoTemp.Count > 0)
            {
                if (Con_continueWithTheAvailablePromotions.Value == "Y")
                {
                    SetColumnForPriceDetailNPromotion(false);
                    gvNormalPrice.DataSource = new List<PriceDetailRef>();
                    gvNormalPrice.DataBind();
                    gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                    gvPromotionPrice.DataBind();
                    gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                    gvPromotionItem.DataBind();
                    gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                    gvPromotionSerial.DataBind();
                    BindNonSerializedPrice(_PriceDetailRefPromoTemp);
                    pnlPriceNPromotion.Visible = true;
                    //  pnlMain.Enabled = false;
                    _isNewPromotionProcess = true;
                    mpPriceNPromotion.Show();
                    return true;
                }
                else if (Con_continueWithTheAvailablePromotions.Value == "N")
                {
                    Con_continueWithTheAvailablePromotions.Value = null;
                    _isNewPromotionProcess = false;
                    return false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "continueWithTheAvailablePromotions();", true);
                    return false;
                }
                //_??DialogResult _promo = new System.Windows.Forms.DialogResult();
                //_??using (new CenterWinDialog(this))
                //_??{
                //_??    _promo = MessageBox.Show("This item is having promotions. Do you need to continue with the available promotions?", "Promotion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //_??}
                //_??if (_promo == System.Windows.Forms.DialogResult.Yes)
                //_??{
                //_??    SetColumnForPriceDetailNPromotion(false);
                //_??    gvNormalPrice.DataSource = new List<PriceDetailRef>();
                //_??    gvPromotionPrice.DataSource = new List<PriceDetailRef>();
                //_??    gvPromotionItem.DataSource = new List<PriceCombinedItemRef>();
                //_??    gvPromotionSerial.DataSource = new List<ReptPickSerials>();
                //_??    BindNonSerializedPrice(_PriceDetailRefPromo);
                //_??    pnlPriceNPromotion.Visible = true;
                //_??    //  pnlMain.Enabled = false;
                //_??    _isNewPromotionProcess = true;
                //_??    return true;
                //_??}
                //_??else
                //_??{
                //_??    _isNewPromotionProcess = false;
                //_??    return false;
                //_??}
            }
            else
                return false;
        }

        private bool CheckSerializedPriceLevelAndLoadSerials(bool _isSerialized)
        {
            bool _isAvailable = false;
            if (_isSerialized)
            {
                if (string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    DisplayMessageJS("You are selected a serialized price level, hence you have not select the serial no. Please select the serial no.");
                    _isAvailable = true;
                    return _isAvailable;
                }
                List<PriceSerialRef> _list = null;
                if (_isNewPromotionProcess == false)
                    _list = CHNLSVC.Sales.GetAllPriceSerialFromSerial(cmbBook.Text, cmbLevel.Text, TxtAdvItem.Text.ToUpper(), Convert.ToDateTime(dtpRecDate.Text).Date, txtCusCode.Text, Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtSerialNo.Text.Trim());
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
                        DisplayMessageJS("There are no serials available for the selected item");
                        txtQty.Text = FormatToQty("0");
                        _isAvailable = true;
                        txtQty.Focus();
                        return _isAvailable;
                    }
                    var _oneSerial = _list.Where(x => x.Sars_ser_no == txtSerialNo.Text.Trim()).ToList();
                    _list = _oneSerial;
                    if (_list.Count < Convert.ToDecimal(txtQty.Text))
                    {
                        DisplayMessageJS("Selected qty is exceeds available serials at the price definition!");
                        txtQty.Text = FormatToQty("0");
                        // IsNoPriceDefinition = true;
                        _isAvailable = true;
                        txtQty.Focus();
                        return _isAvailable;
                    }
                    if (_list.Count == 1)
                    {
                        string _book = _list[0].Sars_pbook;
                        string _level = _list[0].Sars_price_lvl;
                        cmbBook.Text = _book;
                        cmbLevel.Text = _level;
                        if (!_isSerialized)
                            cmbLevel_SelectedIndexChanged(null, null);

                        int _priceType = 0;
                        _priceType = _list[0].Sars_price_type;
                        PriceTypeRef _promotion = TakePromotion(_priceType);
                        decimal UnitPrice = TaxCalculation(TxtAdvItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, _list[0].Sars_itm_price, Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false);

                        txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                        WarrantyRemarks = _list[0].Sars_warr_remarks;
                        SetSSPriceDetailVariable(_list[0].Sars_circular_no, "0", Convert.ToString(_list[0].Sars_pb_seq), Convert.ToString(_list[0].Sars_itm_price), _list[0].Sars_promo_cd, Convert.ToString(_list[0].Sars_price_type));

                        Int32 _pbSq = _list[0].Sars_pb_seq;
                        string _mItem = _list[0].Sars_itm_cd;
                        _isAvailable = true;
                        //if (_promotion.Sarpt_is_com)
                        //{
                        //SetColumnForPriceDetailNPromotion(true);
                        //BindSerializedPrice(_list);

                        //if (gvPromotionPrice.RowCount > 0)
                        //{
                        //    gvPromotionPrice_CellDoubleClick(0, false, _isSerialized);
                        //    pnlPriceNPromotion.Visible = true;
                        //    pnlMain.Enabled = false;
                        //    return _isAvailable;
                        //}
                        //else
                        //    if (_isCombineAdding == false) txtUnitPrice.Focus();
                        ////}
                        ////else
                        ////    if (_isCombineAdding == false) txtUnitPrice.Focus();
                        //return _isAvailable;
                    }
                    if (_list.Count > 1)
                    {
                        //SetColumnForPriceDetailNPromotion(true);
                        //BindPriceAndPromotion(_list);
                        //DisplayAvailableQty(txtItem.Text.ToUpper(), lblPriNProAvailableStatusQty, lblPriNProAvailableQty, cmbStatus.Text);
                        //pnlMain.Enabled = false;
                        //pnlPriceNPromotion.Visible = true;
                        _isAvailable = true;
                        return _isAvailable;
                    }
                }
                else
                {
                    DisplayMessageJS("There are no serials available for the selected item");
                    txtQty.Text = FormatToQty("0");
                    _isAvailable = true;
                    txtQty.Focus();
                    return _isAvailable;
                }
            }
            return _isAvailable;
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

        private decimal TaxCalculation(string _item, string _status, decimal _qty, PriceBookLevelRef _level, decimal _pbUnitPrice, decimal _discount, decimal _disRate, bool _isTaxfaction)
        {
            if (_priceBookLevelRef != null)
                if (_priceBookLevelRef.Sapl_vat_calc)
                {
                    bool _isVATInvoice = false;
                    if (chkTaxPayable.Checked || lblVatExemptStatus.Text == "Available") _isVATInvoice = true;
                    else _isVATInvoice = false;
                    _serverDt = CHNLSVC.Security.GetServerDateTime().Date;
                    if (Convert.ToDateTime(dtpRecDate.Text).Date == _serverDt)
                    {
                        List<MasterItemTax> _taxs = new List<MasterItemTax>();
                        if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTax(Session["UserCompanyCode"].ToString(), _item, _status); else _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT");
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
                        if (_isTaxfaction == false) _taxs = CHNLSVC.Sales.GetTaxEffDt(Session["UserCompanyCode"].ToString(), _item, _status, Convert.ToDateTime(dtpRecDate.Text).Date); else _taxs = CHNLSVC.Sales.GetItemTaxEffDt(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT", Convert.ToDateTime(dtpRecDate.Text).Date);
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
                                _taxsEffDt = CHNLSVC.Sales.GetTaxLog(Session["UserCompanyCode"].ToString(), _item, _status, Convert.ToDateTime(dtpRecDate.Text).Date);
                            else
                                _taxsEffDt = CHNLSVC.Sales.GetItemTaxLog(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, "VAT", Convert.ToDateTime(dtpRecDate.Text).Date);
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

        private void SetSSPriceDetailVariable(string _circuler, string _pblineseq, string _pbseqno, string _pbprice, string _promotioncd, string _promotiontype)
        {
            SSCirculerCode = _circuler;
            SSPriceBookItemSequance = _pblineseq;
            SSPriceBookPrice = Convert.ToDecimal(_pbprice);
            SSPriceBookSequance = _pbseqno;
            SSPromotionCode = _promotioncd;
            if (string.IsNullOrEmpty(_promotioncd) || _promotioncd.Trim().ToUpper() == "N/A")
                SSPromotionCode = string.Empty;
            SSPRomotionType = Convert.ToInt32(_promotiontype);
        }

        private void SetColumnForPriceDetailNPromotion(bool _isSerializedPriceLevel)
        {
            if (_isSerializedPriceLevel)
            {
                //    NorPrice_Select.Visible = true;

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
            }
            else
            {

                BoundField field = (BoundField)this.gvNormalPrice.Columns[1];
                field.DataField = "To";

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

        protected void BindNonSerializedPrice(List<PriceDetailRef> _list)
        {
            _list.ForEach(x => x.Sapd_cre_by = Convert.ToString(x.Sapd_itm_price));
            _list.ForEach(x => x.Sapd_itm_price = CheckSubItemTax(x.Sapd_itm_cd) * x.Sapd_itm_price);

            var _normal = _list.Where(x => x.Sapd_price_type == 0).ToList();
            var _promotion = _list.Where(x => x.Sapd_price_type != 0).ToList();

            DataTable dt = GlobalMethod.ToDataTable(_normal);

            gvNormalPrice.DataSource = _normal;
            gvNormalPrice.DataBind();
            gvPromotionPrice.DataSource = _promotion;
            gvPromotionPrice.DataBind();
        }

        private decimal FigureRoundUp(decimal value, bool _isFinal)
        {
            if (IsSaleFigureRoundUp && _isFinal) return RoundUpForPlace(Math.Round(value), 2);
            else return Math.Round(value, 2);
        }

        private decimal CalculateItemTem(decimal _qty, decimal _unitPrice, decimal _disAmount, decimal _disRt)
        {
            string unitAmt = FormatToCurrency(Convert.ToString(FigureRoundUp(Convert.ToDecimal(_unitPrice) * Convert.ToDecimal(_qty), true)));

            decimal _vatPortion = FigureRoundUp(TaxCalculation(TxtAdvItem.Text.ToUpper().Trim(), cmbStatus.SelectedValue.ToString().Trim(), Convert.ToDecimal(_qty), _priceBookLevelRef, Convert.ToDecimal(_unitPrice), Convert.ToDecimal(_disAmount), Convert.ToDecimal(_disRt), true), true);
            string tax = FormatToCurrency(Convert.ToString(_vatPortion));

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
                        List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), TxtAdvItem.Text.ToUpper().Trim(), Convert.ToString(cmbStatus.SelectedValue), string.Empty, string.Empty);
                        if (_tax != null && _tax.Count > 0)
                        {
                            decimal _vatval = ((_totalAmount + _vatPortion - _disAmt) * _tax[0].Mict_tax_rate) / (100 + _tax[0].Mict_tax_rate);
                            tax = Convert.ToString(FigureRoundUp(_vatval, true));
                        }
                    }
                }

                FormatToCurrency(Convert.ToString(_disAmt));
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

        public static decimal RoundUpForPlace(decimal input, int places)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));
            return Math.Ceiling(input * Convert.ToDecimal(multiplier)) / Convert.ToDecimal(multiplier);
        }

        private void WriteErrLog(string err, Exception ex)
        {
            using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/data.txt"), true))
            {
                _testData.WriteLine(DateTime.Now.ToString("dd/MMM/yyyy  hh:mm:ss tt") + " - " + GetIPAddress() + " | " + err + "   || Exception : " + ex.Message);
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

        private void ClearPriceTextBox()
        {
            txtUnitPrice.Text = FormatToCurrency("0");
            txtUnitAmt.Text = FormatToCurrency("0");
            txtTaxAmt.Text = FormatToCurrency("0");
        }

        #region View Two

        protected void btnBack_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
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
                lblvalue.Text = "ItemAvailableSerial";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        private void txtSerial_TextChange()
        {
            if (!string.IsNullOrEmpty(txtSerialNo.Text))
            {
                ReptPickSerials _serialList = new ReptPickSerials();
                _serialList = CHNLSVC.Inventory.GetAvailableSerIDInformation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), TxtAdvItem.Text.ToUpper().Trim(), txtSerialNo.Text.Trim(), string.Empty, string.Empty);

                if (_serialList.Tus_ser_1 != null)
                {
                    TxtAdvItem.Text = _serialList.Tus_itm_cd;
                }
                else
                {
                    DisplayMessageJS("Invalid serial / engine number.");
                    txtSerialNo.Text = "";
                    txtSerialNo.Focus();
                    return;
                }
            }
        }

        protected void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable);
                DataTable result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "InvoiceItemUnAssable";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        private void txtItemCode_TextChange()
        {
            if (string.IsNullOrEmpty(cmbInvType.Text))
            {
                DisplayMessageJS("Please select sales type");
                return;
            }

            if (string.IsNullOrEmpty(TxtAdvItem.Text.Trim())) return;
            txtItem.Text = TxtAdvItem.Text.ToUpper();
            if (_isItemChecking)
            {
                _isItemChecking = false;
                return;
            }
            _isItemChecking = true;
            try
            {
                if (cmbInvType.Text == "HS")
                {
                    _isCalProcess = false;
                    _selectPromoCode = "";
                    //  LoadScheme();
                    if (GetItemDetails(TxtAdvItem.Text.ToUpper().Trim()) == false)
                    {
                        return;
                    }
                }

                if (!LoadItemDetail(TxtAdvItem.Text.ToUpper().Trim()))
                {
                    DisplayMessageJS("Please check the item code");
                    txtItem.Text = "";
                    txtItem.Focus();
                    if (IsPriceLevelAllowDoAnyStatus == false && chkDeliverLater == false) cmbStatus.Text = "";
                    return;
                }

                if (_itemdetail.Mi_is_ser1 == 1 && IsGiftVoucher(_itemdetail.Mi_itm_tp))
                {
                    if (string.IsNullOrEmpty(txtSerialNo.Text))
                    {

                        DisplayMessageJS("Please select the gift voucher number");
                        txtItem.Text = "";
                        txtSerialNo.Text = "";
                    }

                    return;
                }
                IsVirtual(_itemdetail.Mi_itm_tp);

                if ((_itemdetail.Mi_is_ser1 == 1 && chkDeliverLater == false && chkDeliverNow == false && string.IsNullOrEmpty(txtSerialNo.Text.Trim()) && _priceBookLevelRef.Sapl_is_serialized == false))
                {
                    DisplayMessageJS("You have to select the serial no for the serialized item");
                    return;
                }
                if ((_itemdetail.Mi_is_ser1 == 1 && chkDeliverLater == true && chkDeliverNow == false && string.IsNullOrEmpty(txtSerialNo.Text.Trim()) && _priceBookLevelRef.Sapl_is_serialized == false) && _isRegistrationMandatory)
                {
                    DisplayMessageJS("Registration mandatory items can not save without serial");
                    return;
                }

                if (txtSerialNo.Text != "")
                {
                    txtQty.Text = FormatToQty("1");
                }
                if (_IsVirtualItem)
                {
                    bool block = CheckBlockItem(TxtAdvItem.Text.ToUpper().Trim(), 0, false);
                    if (block)
                    {
                        TxtAdvItem.Text = "";
                    }
                }
                CheckQty(true, "txtItemCode");
                btnConfirm.Focus();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
            finally
            {
                _isItemChecking = false;
            }
        }

        #endregion

        #region Promotions

        protected void btnPromoCLose_Click(object sender, EventArgs e)
        {
            hdfConf.Value = "0";
            Session["mpPriceNPromotion"] = null;
            mpPriceNPromotion.Hide();
        }

        protected void chkSelectPromPrice_CheckedChanged(object sender, EventArgs e)
        {
            mpPriceNPromotion.Show();
        }

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
                    BindPriceCombineItem(Convert.ToInt32(_pbseq), Convert.ToInt32(_pblineseq), Convert.ToInt32(_priceType), _mainItem, string.Empty);
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

                mpPriceNPromotion.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void gvPromotionItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            mpPriceNPromotion.Show();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (_levelStatus == null || _levelStatus.Rows.Count <= 0)
                    return;
                var _types = _levelStatus.AsEnumerable().Select(x => x.Field<string>("Code")).Distinct().ToList();
                _types.Add("");

                DropDownList ddl = (DropDownList)e.Row.FindControl("PromItm_Status");
                foreach (string colName in _types)
                    ddl.Items.Add(new ListItem(colName));
            }
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
                            else if (chkDeliverLater == false && chkDeliverNow == false)
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
                            if (chkDeliverLater == false && chkDeliverNow == false)
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
                            else if (chkDeliverLater == true && chkDeliverNow == false)
                            {
                                DisplayMessageJS("You can not pick similar item unless you have deliver now!");
                                return;
                            }

                        #endregion Similar Item Call
                    }
                }

                mpPriceNPromotion.Show();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message);
            }
        }

        protected void btnPromotionItemSelect_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                promotionsCustomMethod(dr);

                mpPriceNPromotion.Show();
            }
            catch (Exception ex)
            {
                DisplayMessageJS(ex.Message);
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

        protected void PromItm_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpPriceNPromotion.Show();
        }

        protected void btnPriNProConfirm_Click(object sender, EventArgs e)
        {
            try
            {
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
                        //_??var _normalRow = from DataGridViewRow row in gvNormalPrice.Rows where Convert.ToBoolean(((DataGridViewCheckBoxCell)row.Cells[0]).Value) == true select row;
                        //_??if (_normalRow != null)
                        //_??{
                        //_??    foreach (var _row in _normalRow)
                        //_??    {
                        //_??        string _unitPrice = _row.Cells["NorPrice_UnitPrice"].Value.ToString();
                        //_??        string _bkpPrice = _row.Cells["NorPrice_BkpUPrice"].Value.ToString();
                        //_??        string _pbseq = _row.Cells["NorPrice_Pb_Seq"].Value.ToString();
                        //_??        string _pblineseq = string.Empty;
                        //_??        if (string.IsNullOrEmpty(Convert.ToString(_row.Cells["NorPrice_PbLineSeq"].Value))) _pblineseq = "1";
                        //_??        else _pblineseq = _row.Cells["NorPrice_PbLineSeq"].Value.ToString();
                        //_??        string _warrantyrmk = _row.Cells["NorPrice_WarrantyRmk"].Value.ToString();
                        //_??        if (!string.IsNullOrEmpty(_unitPrice))
                        //_??        {
                        //_??            txtUnitPrice.Text = FormatToCurrency(_unitPrice);
                        //_??            SSPriceBookPrice = Convert.ToDecimal(_bkpPrice);
                        //_??            SSPriceBookSequance = _pbseq;
                        //_??            SSPriceBookItemSequance = _pblineseq;
                        //_??            WarrantyRemarks = _warrantyrmk;
                        //_??            CalculateItem();
                        //_??            //  pnlMain.Enabled = true;
                        //_??            pnlPriceNPromotion.Visible = false;
                        //_??        }
                        //_??    }
                        //_??}
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

                                        if (_restriction.Spr_need_cus && (string.IsNullOrEmpty(txtCusCode.Text) || txtCusCode.Text.ToUpper() == "CASH"))
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
                                        if (!string.IsNullOrEmpty(_serialno) && chkDeliverLater == false && chkDeliverNow == false)
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
                                        else if (string.IsNullOrEmpty(_serialno) && chkDeliverLater == false && chkDeliverNow == false)
                                        {
                                            decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                            if (_serialcount != Convert.ToDecimal(_qty))
                                            {
                                                DisplayMessageJS("Quantity and the selected serials mismatch in " + _item);
                                                return;
                                            }
                                        }
                                        else if (_itm.Mi_is_ser1 == 1 && chkDeliverLater && chkDeliverNow == false)
                                        {
                                            ReptPickSerials _one = new ReptPickSerials();
                                            if (!string.IsNullOrEmpty(_serialno)) PriceCombinItemSerialList.Add(VirtualSerialLine(_item, _status, Convert.ToDecimal(_qty), _serialno)[0]);
                                        }
                                    }
                                    else if (_haveSerial == false && _itm.Mi_is_ser1 == 1 && chkDeliverLater == false && chkDeliverNow == false)
                                    {
                                        decimal _serialcount = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item).Select(y => y.Tus_qty).Count();
                                        if (_serialcount != Convert.ToDecimal(_qty))
                                        {
                                            DisplayMessageJS("Quantity and the selected serials mismatch in " + _item);
                                            return;
                                        }
                                    }
                                    else if (_haveSerial == false && (_itm.Mi_is_ser1 == 0 || _itm.Mi_is_ser1 == -1) && chkDeliverLater == false && chkDeliverNow == false)
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
                                    else if (_itm.Mi_is_ser1 == 1 && (chkDeliverLater || chkDeliverNow))
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

                                if (chkDeliverLater == false && _isSingleItemSerialized && chkDeliverNow == false)
                                    if (PriceCombinItemSerialList == null)
                                    {
                                        DisplayMessageJS("You have to select the serial for the promotion items");
                                        return;
                                    }
                                if (chkDeliverLater == false && _isSingleItemSerialized && chkDeliverNow == false)
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
                                btnAddItem.Focus();

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

                                    if (_restriction.Spr_need_cus && (string.IsNullOrEmpty(txtCusCode.Text) || txtCusCode.Text.ToUpper() == "CASH"))
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
                                        _message = _message + "This promotion need Customer code, Please enter customer code. ";
                                    }
                                    if (nic)
                                    {
                                        _message = _message + "This promotion need ID Number, Please enter ID Number. ";
                                    }
                                    if (mob)
                                    {
                                        _message = _message + "This promotion need Mobile Number, Please enter  Mobile Number. ";
                                    }
                                    if (cus || nic || mob)
                                    {
                                        DisplayMessageJS(_message);
                                        return;
                                    }
                                }
                            }

                            if (chkDeliverLater == false && chkDeliverNow == false)
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
                                        if (_serialcount != Convert.ToDecimal(_qty))
                                        {
                                            DisplayMessageJS("Qty and the selected serials mismatch in " + _item);
                                            return;
                                        }
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
                                                if (_pickQty > _invBal)
                                                {
                                                    string msg = _item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal));
                                                    DisplayMessageJS(msg);
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                string msg = _item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0"));
                                                DisplayMessageJS(msg);
                                                return;
                                            }
                                        else
                                        {
                                            string msg = _item + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString("0"));
                                            DisplayMessageJS(msg);
                                            return;
                                        }
                                    }
                                }
                            if (chkDeliverLater || chkDeliverNow)
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
                                            DisplayMessageJS("Qty and the selected serials mismatch in " + _item);
                                            return;
                                        }
                                    }
                                }
                            }
                            if (chkDeliverLater == false && _isSingleItemSerialized && chkDeliverNow == false)
                                if (PriceCombinItemSerialList == null)
                                {
                                    DisplayMessageJS("You have to select the serial for the promotion items");
                                    return;
                                }
                            if (chkDeliverLater == false && _isSingleItemSerialized && chkDeliverNow == false)
                                if (PriceCombinItemSerialList.Count <= 0)
                                {
                                    DisplayMessageJS("You have to select the serial for the promotion items");
                                    return;
                                }
                            SetSSPriceDetailVariable(_circulerncode, _pbLineSeq, _pbSeq, _pbPrice, _promotioncode, _promotiontype);
                            _MainPriceCombinItem = _tempPriceCombinItem;
                            txtUnitPrice.Text = FormatToCurrency(_unitprice);
                            txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text.Trim()), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), false)));
                            CalculateItem();
                            pnlPriceNPromotion.Visible = false;
                            //   LoadScheme();
                            // pnlMain.Enabled = true;
                            btnAddItem.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void btnPriNProCancel_Click(object sender, EventArgs e)
        {

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

        protected void btnSearchFreSerials_Click(object sender, EventArgs e)
        {
            txtPriNProSerialSearch_TextChanged(null, null);
        }

        protected void btnPriNProSerClear_Click(object sender, EventArgs e)
        {

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

        #endregion

        protected void cmbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                _priceBookLevelRefList = CHNLSVC.Sales.GetPriceLevelList(Session["UserCompanyCode"].ToString(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                //   SetColumnForPriceDetailNPromotion(_priceBookLevelRef.Sapl_is_serialized);
                if (_priceBookLevelRef.Sapl_is_serialized && string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                {
                    DisplayMessageJS("You are going to select a serialized price level without serial\n.Please select the serial");
                    txtSerialNo.Text = "";
                    return;
                }
                CheckQty(false, "cmbLevel");
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void cmbBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadPriceLevel(cmbInvType.Text, cmbBook.Text);
                LoadLevelStatus(cmbInvType.Text.Trim(), cmbBook.Text.Trim(), cmbLevel.Text.Trim());
                CheckPriceLevelStatusForDoAllow(cmbLevel.Text.Trim(), cmbBook.Text.Trim());
                ClearPriceTextBox();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
                ClearPriceTextBox();
            }
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            //     if (chkQuotation.Checked) { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("You are not allow to add additional items for the selected quotation.", "Add Item", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
            try
            {
                //if (cmbInvType.Text == "HS" && _isCalProcess == false)
                //{
                //    MessageBox.Show("pls click on the process button.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
                mpPriceNPromotion.Hide();
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    txtItem.Focus();
                    DisplayMessage("Please enter item details");
                    return;
                }
                if (string.IsNullOrEmpty(txtQty.Text)) return;

                #region Check Customer has promotion voucher avoid the discount :: Chamal 04/Jul/2014

                if (!string.IsNullOrEmpty(txtCusCode.Text))
                {
                    if (txtCusCode.Text != "CASH")
                    {
                        if ((string.IsNullOrEmpty(txtDisRate.Text) && string.IsNullOrEmpty(txtDisAmt.Text)))
                        {
                            CashGeneralEntiryDiscountDef _discVou = CHNLSVC.Sales.CheckCustHaveDiscountPromoVoucher(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text, Convert.ToDateTime(dtpRecDate.Text).Date, cmbBook.Text, cmbLevel.Text, txtItem.Text.ToUpper(), string.Empty, txtNIC.Text, txtMobile.Text);
                            if (_discVou != null)
                            {
                                DisplayMessageJS("Promotion voucher discount available for this item");
                                return;
                            }
                        }
                        else
                        {
                            if (Convert.ToDecimal(txtDisRate.Text) <= 0 && Convert.ToDecimal(txtDisAmt.Text) <= 0)
                            {
                                CashGeneralEntiryDiscountDef _discVou = CHNLSVC.Sales.CheckCustHaveDiscountPromoVoucher(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text, Convert.ToDateTime(dtpRecDate.Text).Date, cmbBook.Text, cmbLevel.Text, txtItem.Text.ToUpper(), string.Empty, txtNIC.Text, txtMobile.Text);
                                if (_discVou != null)
                                {
                                    DisplayMessageJS("Promotion voucher discount available for this item");
                                    return;
                                }
                            }
                        }
                    }
                }

                #endregion Check Customer has promotion voucher avoid the discount :: Chamal 04/Jul/2014

                if (!string.IsNullOrEmpty(SSPromotionCode))
                {
                    //check promotion qty anr return
                    List<PriceDetailRef> _promoList = CHNLSVC.Sales.GetPriceByPromoCD(SSPromotionCode);
                    if (_promoList == null && _promoList.Count <= 0)
                    {
                        return;
                    }
                    else
                    {
                        decimal qty = _promoList[0].Sapd_qty_to;
                        if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                        {
                            List<InvoiceItem> _alredyAddList = (from _res in _invoiceItemList
                                                                where _res.Sad_itm_cd == txtItem.Text.ToUpper().Trim() && _res.Sad_itm_stus == cmbStatus.Text
                                                                select _res).ToList<InvoiceItem>();
                            if (_alredyAddList != null)
                            {
                                qty = qty + _alredyAddList.Count;
                            }
                            if (Convert.ToDecimal(txtQty.Text) > qty)
                            {
                                DisplayMessageJS("Invoice qty exceed promotion allow qty.");
                                return;
                            }

                        }
                        //free item check
                        //not define the process
                        //sachith 2014/01/29
                    }
                }

                List<MasterItemComponent> _com = CHNLSVC.Inventory.GetItemComponents(txtItem.Text.ToUpper().Trim());
                if (_com != null && _com.Count > 0)
                {
                    foreach (MasterItemComponent _itmCom in _com)
                    {
                        //REGISTRATION PROCESS
                        //ADDED 2013/12/10
                        //REGISTRATION PEOCESS CHECK
                        //ADDED 2013/12/06
                        //CHECK ITEM NEED REGISTRATION OR NOT
                        MasterItem _temItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _itmCom.ComponentItem.Mi_cd);
                        if (_isRegistrationMandatory)
                        {
                            if (_temItm.Mi_need_reg)
                            {
                                //_isNeedRegistrationReciept = true;
                                //  lnkProcessRegistration.Visible = true;
                            }
                        }
                    }
                }
                else
                {
                    MasterItem _temItm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().Trim());
                    if (_isRegistrationMandatory)
                    {
                        if (_temItm.Mi_need_reg)
                        {
                            //  _isNeedRegistrationReciept = true;
                            //  lnkProcessRegistration.Visible = true;
                        }
                    }
                }
                //END
                //END

                AddItem(SSPromotionCode == "0" || string.IsNullOrEmpty(SSPromotionCode) ? false : true, string.Empty);
                if (_priceDetailRef != null && _priceDetailRef.Count > 0)
                {
                    if (_priceDetailRef[0].Sapd_customer_cd == txtCusCode.Text.Trim())
                    {
                        txtCusCode.ReadOnly = true;
                        //  btnSearch_Customer.Enabled = false;
                    }
                }

                if (cmbInvType.Text == "HS")
                {
                    pnlItem.Enabled = false;
                    //_??LoadScheme(TxtAdvItem.Text);

                    //  btnConfirm_Click(null, null);
                    // ClearHP();
                }

                else
                {
                    txtQty.Text = FormatToQty("1");
                    txtSerialNo.Text = "";

                    TxtAdvItem.Text = "";
                }

                // AddItemDisableCustomer(true);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        private void AddItem(bool _isPromotion, string _originalItem)
        {
            try
            {
                if (_invoiceItemList == null)
                {
                    _invoiceItemList = new List<InvoiceItem>();
                }
                if (InvoiceSerialList == null)
                {
                    InvoiceSerialList = new List<InvoiceSerial>();
                }

                if (ScanSerialList == null)
                {
                    ScanSerialList = new List<ReptPickSerials>();
                }

                if (!string.IsNullOrEmpty(SSPromotionCode) && SSPromotionCode != "N/A")
                    ucPayModes1.ISPromotion = true;

                ReptPickSerials _serLst = null;
                List<ReptPickSerials> _nonserLst = null;
                MasterItem _itm = null;

                #region Gift Voucher Check

                //if ((chkPickGV.Checked || IsGiftVoucher(_itemdetail.Mi_itm_tp)) && _isCombineAdding == false)
                //{
                //    if (gvInvoiceItem.Rows.Count <= 0)
                //    { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Please select the selling item before add gift voucher.", "Need Selling Item", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                //    if (gvInvoiceItem.Rows.Count > 0)
                //    {
                //        var _noOfSets = _invoiceItemList.Select(x => x.Sad_job_line).Distinct().ToList();

                //        var _giftCount = _invoiceItemList.Where(x => IsGiftVoucher(x.Sad_itm_tp)).Sum(x => x.Sad_qty);
                //        var _nonGiftCount = _invoiceItemList.Sum(x => x.Sad_qty) - _giftCount;
                //        if (_nonGiftCount < _giftCount + 1)
                //        {
                //            this.Cursor = Cursors.Default;
                //            using (new CenterWinDialog(this)) { MessageBox.Show("You can not add more gift vouchers than selling qty", "Gift Voucher", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                //            return;
                //        }
                //    }

                //    DataTable _giftVoucher = CHNLSVC.Inventory.GetDetailByPageNItem(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToInt32(txtSerialNo.Text.Trim()), txtItem.Text.Trim());
                //    if (_giftVoucher != null)
                //        if (_giftVoucher.Rows.Count > 0)
                //        {
                //            _serial2 = Convert.ToString(_giftVoucher.Rows[0].Field<Int64>("gvp_book"));
                //            _prefix = Convert.ToString(_giftVoucher.Rows[0].Field<string>("gvp_gv_prefix"));
                //        }
                //}

                #endregion Gift Voucher Check

                #region Check for Payment

                if (_recieptItem != null)
                    if (_recieptItem.Count > 0)
                    {
                        DisplayMessageJS("You have already payment added!");
                        return;
                    }

                #endregion Check for Payment

                #region Priority Base Validation

                if (_masterBusinessCompany == null)
                    //   { this.Cursor = Cursors.Default; using (new CenterWinDialog(this)) { MessageBox.Show("Please select the customer code", "No Customer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); } return; }
                    //if (_masterBusinessCompany.Mbe_cd != null && _masterBusinessCompany.Mbe_sub_tp == "C")                //    if ((Convert.ToDecimal(lblAvailableCredit.Text) - Convert.ToDecimal(txtLineTotAmt.Text) - Convert.ToDecimal(lblGrndTotalAmount.Text) < 0) && txtCusCode.Text != "CASH")                //    {                //        this.Cursor = Cursors.Default;                //        using (new CenterWinDialog(this)) { MessageBox.Show("Please check the customer's account balance", "Account Balance", MessageBoxButtons.OK, MessageBoxIcon.Information); }                //        return;                //    }
                    if (string.IsNullOrEmpty(cmbBook.Text))
                    {
                        DisplayMessage("Please select the price book");
                        cmbBook.Focus();
                        return;
                    }

                if (string.IsNullOrEmpty(cmbLevel.Text))
                {
                    DisplayMessage("Please select the price level");
                    cmbLevel.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbStatus.Text))
                {
                    DisplayMessage("Please select the item status");
                    cmbStatus.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(cmbInvType.Text))
                {
                    DisplayMessage("Please select the invoice type");
                    cmbInvType.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    DisplayMessage("Please select the customer");
                    txtCusCode.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtItem.Text))
                {
                    if (chkDeliverLater == false && chkDeliverNow == false)
                    {
                        if (string.IsNullOrEmpty(txtSerialNo.Text))
                        {
                            DisplayMessage("Please select the serial");
                            txtSerialNo.Focus();
                            return;
                        }
                        else
                        {
                            DisplayMessage("Please select the item");
                            txtItem.Focus();
                            return;
                        }
                    }
                    else
                    {
                        DisplayMessage("Please select the item");
                        txtItem.Focus();
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    DisplayMessage("Please select the qty");
                    txtQty.Focus();
                    return;
                }
                else if (IsNumeric(txtQty.Text) == false)
                {
                    DisplayMessage("Please select valid qty");
                    return;
                }
                else if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0)
                {
                    DisplayMessage("Please select the valid qty amount.");
                    return;
                }
                if (string.IsNullOrEmpty(txtUnitPrice.Text))
                {
                    DisplayMessage("Please select the unit price");
                    txtUnitPrice.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDisRate.Text))
                {
                    DisplayMessage("Please select the discount %");
                    txtDisRate.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtDisAmt.Text))
                {
                    DisplayMessage("Please select the discount amount");
                    txtDisAmt.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtTaxAmt.Text))
                {
                    DisplayMessage("Please select the VAT amount");
                    txtTaxAmt.Focus();
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
                                             where _list.Sad_itm_cd == txtItem.Text.ToUpper() && _list.Sad_itm_stus == cmbStatus.Text && _list.Sad_pbook == cmbBook.Text && _list.Sad_pb_lvl == cmbLevel.Text && _list.Sad_unit_rt == Convert.ToDecimal(txtUnitPrice.Text) && _list.Sad_disc_rt == Convert.ToDecimal(txtDisRate.Text.Trim())
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
                    if (_isCombineAdding == false)
                    {
                        SSPromotionCode = string.Empty;
                    }
                    SSPRomotionType = 0;
                    txtItem.Focus();
                    BindAddItem();
                    SetDecimalTextBoxForZero(true);
                    decimal _tobepays0 = 0;
                    if (lblSVatStatus.Text == "Available")
                    {
                        _tobepays0 = FigureRoundUp(Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim()), true);
                    }
                    else
                    {
                        _tobepays0 = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                    }
                    ucPayModes1.TotalAmount = _tobepays0;
                    ucPayModes1.InvoiceItemList = _invoiceItemList;
                    ucPayModes1.SerialList = InvoiceSerialList;
                    ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays0));
                    if (_loyaltyType != null)
                    {
                        ucPayModes1.LoyaltyCard = _loyaltyType.Salt_loty_tp;
                    }
                    if (ucPayModes1.HavePayModes && _isCombineAdding == false)
                        ucPayModes1.LoadData();
                    //LookingForBuyBack();
                    if (_isCombineAdding == false)
                    {
                        //_??if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //_??{
                        //_??    if (chkDeliverLater == false && chkDeliverNow == false)
                        //_??    {
                        //_??        txtSerialNo.Focus();
                        //_??    }
                        //_??    else
                        //_??    {
                        //_??        txtItem.Focus();
                        //_??    }
                        //_??}
                        //_??else
                        //_??{
                        //_??    ucPayModes1.button1.Focus();
                        //_??}
                    }
                    return;
                }

                _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().Trim());

                //if (!chkDeliverLater && !chkDeliverNow)
                //{
                //    List<ReptPickSerials> _temp = CHNLSVC.Inventory.Search_by_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _itm.Mi_cd, string.Empty, txtSerialNo.Text.Trim(), string.Empty);
                //    if (!string.IsNullOrEmpty(Convert.ToString(cmbLevel.Text)) && !string.IsNullOrEmpty(Convert.ToString(cmbBook.Text)))
                //    {
                //        bool _isAgeLevel = false;
                //        int _noofday = 0;
                //        CheckNValidateAgeItem(_itm.Mi_cd, string.Empty, cmbBook.Text, cmbLevel.Text, cmbStatus.Text, out _isAgeLevel, out _noofday);
                //        if (_isAgeLevel)
                //            _temp = GetAgeItemList(Convert.ToDateTime(dtpRecDate..Value.Date).Date, _isAgeLevel, _noofday, _temp);
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
                if (string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    if (!string.IsNullOrEmpty(txtItem.Text.ToUpper().Trim()))
                    {
                        //Edt0001
                        if (_itm.Mi_is_ser1 == 1 && (chkDeliverLater == false && chkDeliverNow == false && _priceBookLevelRef.Sapl_is_serialized))
                        {
                            DisplayMessage("Please select the serial no");
                            txtSerialNo.Focus();
                            return;
                        }
                    }
                }

                #region sachith check item balance

                if (chkDeliverNow && _itm.Mi_itm_tp == "M")
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
                        //_??if (MessageBox.Show("Inventory has only " + serial_list.Count + " items\n Do you want to proceed?", "Serial Qty", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //_??{
                        //_??    return;
                        //_??}
                        //_??else
                        //_??{
                        //_??}
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
                            if (CheckBlockItem(_mItem, SSPRomotionType, _isCombineAdding)) { _isCheckedPriceCombine = false; return; }
                            var _dupsMain = ScanSerialList.Where(x => x.Tus_itm_cd == _mItem && x.Tus_ser_1 == ScanSerialNo);
                            if (_dupsMain != null)
                            {
                                if (_dupsMain.Count() > 0)
                                {
                                    {
                                        _isCheckedPriceCombine = false;
                                        string msg2 = _mItem + " serial " + ScanSerialNo + " is already picked!";
                                        DisplayMessage(msg2);
                                        return;
                                    }
                                }
                            }
                            foreach (PriceCombinedItemRef _ref in _MainPriceCombinItem)
                            {
                                string _item = _ref.Sapc_itm_cd;
                                string _originalItm = _ref.Sapc_itm_cd;
                                decimal _qty = _ref.Sapc_qty;
                                string _status = _ref.Status;
                                string _similerItem = Convert.ToString(_ref.Similer_item);
                                if (!string.IsNullOrEmpty(_similerItem)) _item = _similerItem;
                                if (CheckBlockItem(_item, SSPRomotionType, _isCombineAdding)) { _isCheckedPriceCombine = false; break; }
                                List<MasterItemTax> _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _item, _status, string.Empty, string.Empty);
                                if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                                {
                                    if (string.IsNullOrEmpty(_taxNotdefine))
                                        _taxNotdefine = _item;
                                    else
                                        _taxNotdefine += "," + _item;
                                }
                                if (CheckItemWarranty(_item, _status))
                                {
                                    if (string.IsNullOrEmpty(_noWarrantySetup))
                                        _noWarrantySetup = _item;
                                    else
                                        _noWarrantySetup += "," + _item;
                                }
                                MasterItem _itmS = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _item);

                                if ((chkDeliverLater == false && chkDeliverNow == false && _isCheckedPriceCombine == false) || IsGiftVoucher(_itmS.Mi_itm_tp))
                                {
                                    _isCheckedPriceCombine = true;
                                    if (_itmS.Mi_is_ser1 == 1)
                                    {
                                        var _exist = PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == _item);
                                        if (_qty > _exist.Count())
                                        {
                                            if (string.IsNullOrEmpty(_serialiNotpick))
                                                _serialiNotpick = _item;
                                            else
                                                _serialiNotpick += "," + _item;
                                        }
                                        foreach (ReptPickSerials _p in _exist)
                                        {
                                            string _serial = _p.Tus_ser_1;
                                            var _dup = ScanSerialList.Where(x => x.Tus_ser_1 == _serial);
                                            if (_dup != null)
                                                if (_dup.Count() > 0)
                                                { if (string.IsNullOrEmpty(_serialDuplicate)) _serialDuplicate = _item + "/" + _serial; else _serialDuplicate = "," + _item + "/" + _serial; }
                                        }
                                    }
                                    if (!IsGiftVoucher(_itmS.Mi_itm_tp))
                                    {
                                        decimal _pickQty = 0;
                                        if (IsPriceLevelAllowDoAnyStatus) _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item).ToList().Select(x => x.Sad_qty).Sum(); else _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _item && x.Mi_itm_stus == _status).ToList().Select(x => x.Sad_qty).Sum();
                                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                        List<InventoryLocation> _inventoryLocation = null;
                                        if (IsPriceLevelAllowDoAnyStatus) _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, string.Empty); else _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _item, _status);
                                        if (_inventoryLocation != null)
                                            if (_inventoryLocation.Count > 0)
                                            {
                                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                                if (_pickQty > _invBal)
                                                { if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item; else _noInventoryBalance = "," + _item; }
                                            }
                                            else
                                            { if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item; else _noInventoryBalance = "," + _item; }
                                        else
                                        { if (string.IsNullOrEmpty(_noInventoryBalance)) _noInventoryBalance = _item; else _noInventoryBalance = "," + _item; }
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(_taxNotdefine))
                            {
                                string msgT = _taxNotdefine + " does not have setup tax definition for the selected status. Please contact Inventory dept.";
                                DisplayMessage(msgT);
                                return;
                            }
                            if (!string.IsNullOrEmpty(_serialiNotpick))
                            {
                                string msgQ = "Item Qty and picked serial mismatch for the following item(s) " + _serialiNotpick;
                                DisplayMessage(msgQ);
                                return;
                            }
                            if (!string.IsNullOrEmpty(_serialDuplicate))
                            {
                                string msgAq = "Serial duplicating for the following item(s) " + _serialDuplicate;
                                DisplayMessage(msgAq);
                                return;
                            }
                            if (!string.IsNullOrEmpty(_noInventoryBalance) && !IsGiftVoucher(_itm.Mi_itm_tp))
                            {
                                DisplayMessage(_noInventoryBalance + " item(s) does not having inventory balance for release.");
                                return;
                            }

                            if (!string.IsNullOrEmpty(_noWarrantySetup))
                            {
                                string msgW = _noWarrantySetup + " item(s) warranty not define.";
                                DisplayMessage(msgW);
                                return;
                            }
                            _isFirstPriceComItem = true;
                            _isCheckedPriceCombine = true;
                        }
                if (_isCompleteCode && _isInventoryCombineAdded == false)
                {
                    BindItemComponent(txtItem.Text.ToUpper());
                }
                if (_masterItemComponent != null && _masterItemComponent.Count > 0 && _isInventoryCombineAdded == false)
                {
                    string _combineStatus = string.Empty;
                    decimal _discountRate = -1;
                    decimal _combineQty = 0;
                    string _mainItem = string.Empty;
                    _combineCounter = 0;
                    _isInventoryCombineAdded = true;
                    _isCombineAdding = true;
                    if (string.IsNullOrEmpty(_combineStatus))
                    {
                        _combineStatus = cmbStatus.Text;
                    }
                    if (_combineQty == 0)
                    {
                        _combineQty = Convert.ToDecimal(txtQty.Text);
                    }
                    if (_discountRate == -1)
                    {
                        _discountRate = Convert.ToDecimal(txtDisRate.Text);
                    }
                    List<MasterItemComponent> _comItem = new List<MasterItemComponent>();
                    var _item_ = (from _n in _masterItemComponent where _n.Micp_itm_tp == "M" select _n.ComponentItem.Mi_cd).ToList();
                    if (!string.IsNullOrEmpty(_item_[0]))
                    {
                        string _mItem = Convert.ToString(_item_[0]);
                        _mainItem = Convert.ToString(_item_[0]);
                        _priceDetailRef = new List<PriceDetailRef>();
                        _priceDetailRef = CHNLSVC.Sales.GetPrice(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), cmbInvType.Text, cmbBook.Text, cmbLevel.Text, txtCusCode.Text, _mItem, _combineQty, Convert.ToDateTime(dtpRecDate.Text).Date);
                        _priceDetailRef = _priceDetailRef.Where(X => X.Sapd_price_type == 0).ToList();

                        if (CheckItemWarranty(_mItem, cmbStatus.Text.Trim()))
                        {
                            string msgPw = _mItem + " item's warranty period not setup by the inventory department. Please contact inventory department";
                            DisplayMessage(msgPw);
                            _isInventoryCombineAdded = false;
                            return;
                        }

                        if (_priceDetailRef.Count <= 0)
                        {
                            string msgP = _item_[0].ToString() + " does not having price. Please contact IT dept.";
                            DisplayMessage(msgP);
                            _isInventoryCombineAdded = false;
                            return;
                        }
                        else
                        {
                            if (CheckBlockItem(_mItem, _priceDetailRef[0].Sapd_price_type, _isCombineAdding))
                            {
                                _isInventoryCombineAdded = false;
                                return;
                            }
                            if (_priceDetailRef.Count == 1 && _priceDetailRef[0].Sapd_price_type != 0 && _priceDetailRef[0].Sapd_price_type != 4)
                            {
                                string msg = _item_[0].ToString() + " price is available for only promotion. Complete code does not support for promotion";
                                DisplayMessage(msg);
                                _isInventoryCombineAdded = false;
                                return;
                            }
                        }
                    }
                    foreach (MasterItemComponent _com in _masterItemComponent.Where(X => X.ComponentItem.Mi_cd != _item_[0]))
                    {
                        if (CheckItemWarranty(_com.ComponentItem.Mi_cd, cmbStatus.Text.Trim()))
                        {
                            string msg = _com.ComponentItem.Mi_cd + " item's warranty period not setup by the inventory department. Please contact inventory department";
                            DisplayMessage(msg);
                            _isInventoryCombineAdded = false;
                            return;
                        }
                        if (CheckBlockItem(_com.ComponentItem.Mi_cd, _priceDetailRef[0].Sapd_price_type, _isCombineAdding))
                        { _isInventoryCombineAdded = false; return; }
                    }
                    bool _isMainSerialCheck = false;
                    if (ScanSerialList != null && chkDeliverLater == false && chkDeliverNow == false)
                    {
                        if (ScanSerialList.Count > 0)
                        {
                            if (_isMainSerialCheck == false)
                            {
                                var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == _item_[0].ToString() && x.Tus_ser_1 == ScanSerialNo);
                                if (_dup != null)
                                    if (_dup.Count() > 0)
                                    {
                                        string msg = _item_[0].ToString() + " serial " + ScanSerialNo + " is already picked!";
                                        DisplayMessage(msg);
                                        _isInventoryCombineAdded = false;
                                        return;

                                    } _isMainSerialCheck = true;
                            }
                            foreach (MasterItemComponent _com in _masterItemComponent)
                            {
                                string _serial = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _com.ComponentItem.Mi_cd).Select(y => y.Tus_ser_1).ToString();
                                var _dup = ScanSerialList.Where(x => x.Tus_ser_1 == _serial && x.Tus_itm_cd == _com.ComponentItem.Mi_cd);
                                if (_dup != null)
                                    if (_dup.Count() > 0)
                                    {
                                        DisplayMessage("Item " + _com.ComponentItem.Mi_cd + "," + _serial + " serial is already picked!");
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
                            List<MasterItemTax> _taxs = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), _com.ComponentItem.Mi_cd, _combineStatus, string.Empty, string.Empty);
                            if (_taxs.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                            {
                                DisplayMessage(_com.ComponentItem.Mi_cd + " does not have setup tax definition for the selected status. Please contact Inventory dept.");
                                _isInventoryCombineAdded = false;
                                return;
                            }

                            if (chkDeliverLater == false && chkDeliverNow == false)
                            {
                                decimal _pickQty = 0;
                                if (IsPriceLevelAllowDoAnyStatus) _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd).ToList().Select(x => x.Sad_qty).Sum();
                                else _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == _com.ComponentItem.Mi_cd && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();
                                _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                                List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), _com.ComponentItem.Mi_cd, cmbStatus.Text.Trim());
                                if (_inventoryLocation != null)
                                    if (_inventoryLocation.Count > 0)
                                    {
                                        decimal _invBal = _inventoryLocation[0].Inl_qty;
                                        if (_pickQty > _invBal)
                                        {
                                            string msg = _com.ComponentItem.Mi_cd + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal));
                                            DisplayMessage(msg);
                                            _isInventoryCombineAdded = false;
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        string msg = _com.ComponentItem.Mi_cd + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty("0");
                                        DisplayMessage(msg);
                                        _isInventoryCombineAdded = false;
                                        return;
                                    }
                                else
                                {
                                    DisplayMessage(_com.ComponentItem.Mi_cd + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty("0"));
                                    _isInventoryCombineAdded = false;
                                    return;
                                }
                            }
                            _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), _com.ComponentItem.Mi_cd);

                            if (_itm.Mi_is_ser1 == 1 && chkDeliverLater == false && chkDeliverNow == false)
                            {
                                _comItem.Add(_com);
                            }
                        }

                        if (_comItem.Count > 1 && chkDeliverLater == false && chkDeliverNow == false)
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

                            //_??gvPopComItem.DataSource = _listComItem;
                            //_??pnlInventoryCombineSerialPick.Visible = true;

                            DisplayMessage("gvPopComItem.DataSource");

                            //  pnlMain.Enabled = false;
                            _isInventoryCombineAdded = false;
                            return;
                        }
                        else if (_comItem.Count == 1 && chkDeliverLater == false && chkDeliverNow == false)
                        {//hdnItemCode.Value
                            ReptPickSerials _pick = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, _mainItem.Trim(), txtSerialNo.Text.Trim());
                            if (_pick != null)
                                if (!string.IsNullOrEmpty(_pick.Tus_itm_cd))
                                {
                                    var _dup = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _pick.Tus_itm_cd && x.Tus_ser_1 == _pick.Tus_ser_1).ToList();
                                    if (_dup != null)
                                        if (_dup.Count <= 0) InventoryCombinItemSerialList.Add(_pick);
                                }
                        }
                    }

                    SSCombineLine += 1;
                    foreach (MasterItemComponent _com in _masterItemComponent.OrderByDescending(x => x.ComponentItem.Mi_itm_tp))
                    {
                        //If going to deliver now
                        if (chkDeliverLater == false && chkDeliverNow == false && InventoryCombinItemSerialList.Count > 0)
                        {
                            var _comItemSer = InventoryCombinItemSerialList.Where(x => x.Tus_itm_cd == _com.ComponentItem.Mi_cd).ToList();
                            if (_comItemSer != null)
                                if (_comItemSer.Count > 0)
                                {
                                    foreach (ReptPickSerials _serItm in _comItemSer)
                                    {
                                        txtSerialNo.Text = _serItm.Tus_ser_1; ScanSerialNo = txtSerialNo.Text;
                                        txtSerialNo.Text = ScanSerialNo;
                                        txtItem.Text = _com.ComponentItem.Mi_cd;
                                        cmbStatus.Text = _combineStatus;
                                        txtQty.Text = FormatToQty("1");
                                        CheckQty(false, "btnAddItem");
                                        txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
                                        txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                                        txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.ToUpper(), cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true)));
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
                                    cmbStatus.Text = _combineStatus;
                                    txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));
                                    CheckQty(false, "btnAddItem");
                                    txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate)); txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                                    txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.ToUpper(), cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true)));
                                    txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    AddItem(false, string.Empty);
                                    ScanSerialNo = string.Empty;
                                    txtSerialNo.Text = string.Empty;
                                    txtSerialNo.Text = string.Empty; _combineCounter += 1;
                                }
                        }
                        //If deliver later
                        else if ((chkDeliverLater || chkDeliverNow) && InventoryCombinItemSerialList.Count == 0)
                        {
                            txtItem.Text = _com.ComponentItem.Mi_cd;
                            LoadItemDetail(txtItem.Text.ToUpper().Trim());
                            cmbStatus.Text = _combineStatus;
                            txtQty.Text = FormatToQty(Convert.ToString(_com.Micp_qty * _combineQty));
                            CheckQty(false, "btnAddItem");
                            txtDisRate.Text = FormatToCurrency(Convert.ToString(_discountRate));
                            txtDisAmt.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtUnitPrice.Text) * Convert.ToDecimal(txtQty.Text) * _discountRate / 100));
                            txtTaxAmt.Text = FormatToCurrency(Convert.ToString(TaxCalculation(txtItem.Text.ToUpper(), cmbStatus.Text, Convert.ToDecimal(txtQty.Text), _priceBookLevelRef, Convert.ToDecimal(txtUnitPrice.Text), Convert.ToDecimal(txtDisAmt.Text.Trim()), Convert.ToDecimal(txtDisRate.Text), true)));
                            txtLineTotAmt.Text = FormatToCurrency("0"); CalculateItem();
                            AddItem(false, string.Empty);
                            _combineCounter += 1;
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

                            txtItem.Focus();
                            BindAddItem();
                            SetDecimalTextBoxForZero(true);

                            decimal _tobepay = 0;
                            if (lblSVatStatus.Text == "Available")
                                _tobepay = FigureRoundUp(Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim()), true);
                            else
                                _tobepay = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());

                            ucPayModes1.TotalAmount = _tobepay;
                            ucPayModes1.InvoiceItemList = _invoiceItemList;
                            ucPayModes1.SerialList = InvoiceSerialList;
                            ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepay));
                            if (_loyaltyType != null)
                            {
                                ucPayModes1.LoyaltyCard = _loyaltyType.Salt_loty_tp;
                            }
                            if (ucPayModes1.HavePayModes)
                                ucPayModes1.LoadData();

                            //_??if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            //_??{
                            //_??    if (chkDeliverLater == false && chkDeliverNow == false)
                            //_??    {
                            //_??        txtSerialNo.Focus();
                            //_??    }
                            //_??    else
                            //_??    {
                            //_??        txtItem.Focus();
                            //_??    }
                            //_??}
                            //_??else
                            //_??{
                            //_??    ucPayModes1.button1.Focus();
                            //_??}
                        }
                        return;
                    }
                }
                bool _isAgePriceLevel = false;
                int _noofDays = 0;
                DateTime _serialpickingdate = Convert.ToDateTime(dtpRecDate.Text).Date;
                CheckNValidateAgeItem(txtItem.Text.ToUpper().Trim(), _itm.Mi_cate_1, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), cmbStatus.Text, out _isAgePriceLevel, out _noofDays);
                if (_isAgePriceLevel) _serialpickingdate = _serialpickingdate.AddDays(-_noofDays);
                if (_priceBookLevelRef.Sapl_is_serialized)
                {
                    if (chkDeliverLater == false && chkDeliverNow == false || (_isRegistrationMandatory))
                    {
                        if (_itm.Mi_is_ser1 == 1)
                        {
                            if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                            {
                                DisplayMessage("Please select the serial no");
                                txtSerialNo.Focus(); return;
                            }
                            _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, txtItem.Text.ToUpper().Trim(), txtSerialNo.Text.Trim());
                            if (_serLst == null || string.IsNullOrEmpty(_serLst.Tus_com))
                            {
                                if (_isAgePriceLevel)
                                    DisplayMessage("There is no serial available for the selected item in a aging price level.");
                                else
                                    DisplayMessage("There is no serial available for the selected item.");
                                return;
                            }
                        }
                        else if (_itm.Mi_is_ser1 == 0)
                        {
                            if (IsPriceLevelAllowDoAnyStatus == false) _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date); else _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                            if (_nonserLst == null || _nonserLst.Count <= 0)
                            {
                                if (_isAgePriceLevel)
                                    DisplayMessage("There is no available qty for the selected item in a aging price level.");
                                else

                                    DisplayMessage("There is no available qty for the selected item.");
                                return;
                            }
                        }
                        else if (_itm.Mi_is_ser1 == -1)
                        {
                            //if (IsPriceLevelAllowDoAnyStatus == false) _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date); else _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                            //if (_nonserLst == null || _nonserLst.Count <= 0) { this.Cursor = Cursors.Default; if (_isAgePriceLevel)                                    using (new CenterWinDialog(this)) { MessageBox.Show("There is no available qty for the selected item in a ageing price level.", "No Available Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } else                                    using (new CenterWinDialog(this)) { MessageBox.Show("There is no available qty for the selected item.", "No Available Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                        }
                    }
                    else
                    {
                        // if (_itm.Mi_is_ser1 == 1) _serLst = VirtualSerialLine(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), txtSerialNo.Text.Trim())[0]; else if (_itm.Mi_is_ser1 == 0) _nonserLst = VirtualSerialLine(txtItem.Text.Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), string.Empty);
                    }
                }
                else if ((chkDeliverLater == false && chkDeliverNow == false) || IsGiftVoucher(_itm.Mi_itm_tp) || (_isRegistrationMandatory))
                {
                    if (_itm.Mi_is_ser1 == 1)
                    {
                        if (string.IsNullOrEmpty(txtSerialNo.Text.Trim()))
                        {
                            DisplayMessage("Please select the serial no");
                            txtSerialNo.Focus();
                            return;
                        }

                        bool _isGiftVoucher = IsGiftVoucher(_itm.Mi_itm_tp);
                        if (!_isGiftVoucher)
                            _serLst = CHNLSVC.Inventory.Get_all_details_on_serial(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), string.Empty, txtItem.Text.ToUpper().Trim(), txtSerialNo.Text.Trim());
                        else _serLst = CHNLSVC.Inventory.GetGiftVoucherDetail(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtItem.Text.ToUpper().Trim(), Convert.ToInt32(_serial2), Convert.ToInt32(txtSerialNo.Text.Trim()), _prefix);
                        if (_serLst != null && !string.IsNullOrEmpty(_serLst.Tus_com))
                        {
                            if (_serLst.Tus_doc_dt >= _serialpickingdate)
                            {
                                if (_isAgePriceLevel)
                                {
                                    DisplayMessage("There is no serial available for the selected item in a aging price level.");
                                    return;
                                }
                            }
                            //else using (new CenterWinDialog(this)) { MessageBox.Show("There is no serial available for the selected item.", "No Serial", MessageBoxButtons.OK, MessageBoxIcon.Information); } return;
                        }
                    }
                    else if (_itm.Mi_is_ser1 == 0)
                    {
                        if (IsPriceLevelAllowDoAnyStatus == false)
                            _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                        else
                            _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                        if (_nonserLst == null || _nonserLst.Count <= 0)
                        {
                            if (_isAgePriceLevel)

                                DisplayMessage("There is no available qty for the selected item in a aging price level.");
                            else
                                DisplayMessage("There is no available qty for the selected item.");
                            return;
                        }
                    }
                    else if (_itm.Mi_is_ser1 == -1)
                    {
                        //if (IsPriceLevelAllowDoAnyStatus == false) _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim(), Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                        //else _nonserLst = CHNLSVC.Inventory.GetNonSerializedItemRandomlyByDate(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), string.Empty, Convert.ToDecimal(txtQty.Text.Trim()), _serialpickingdate.Date);
                        //if (_nonserLst == null || _nonserLst.Count <= 0)
                        //{ this.Cursor = Cursors.Default; if (_isAgePriceLevel) using (new CenterWinDialog(this)) { MessageBox.Show("There is no available qty for the selected item in a ageing price level.", "No Available Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } else                                using (new CenterWinDialog(this)) { MessageBox.Show("There is no available qty for the selected item.", "No Available Qty", MessageBoxButtons.OK, MessageBoxIcon.Information); } return; }
                    }
                }
                if ((SSPriceBookSequance == "0" || string.IsNullOrEmpty(SSPriceBookSequance)) && !IsGiftVoucher(_itm.Mi_itm_tp) && _MasterProfitCenter.Mpc_without_price == false && _priceBookLevelRef.Sapl_is_without_p == false)
                {
                    if (!_isCombineAdding)
                    {
                        DisplayMessage("Please select the valid price");
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtQty.Text.Trim()))
                {
                    DisplayMessage("Please select the valid qty");
                    return;
                }

                if (Convert.ToDecimal(txtQty.Text) == 0)
                {
                    DisplayMessage("Please select the valid qty");
                    return;
                }
                if (Convert.ToDecimal(txtQty.Text.Trim()) <= 0)
                {
                    DisplayMessage("Please select the valid qty");
                    return;
                }
                if (string.IsNullOrEmpty(txtUnitPrice.Text.Trim()))
                {
                    DisplayMessage("Please select the valid unit price");
                    return;
                }
                if (!_isCombineAdding)
                {
                    List<MasterItemTax> _tax = CHNLSVC.Sales.GetItemTax(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim(), string.Empty, string.Empty);
                    if (_tax.Count <= 0 && _priceBookLevelRef.Sapl_vat_calc == true)
                    {
                        DisplayMessage("Tax rates not setup for selected item code and item status.Please contact Inventory Department.");
                        cmbStatus.Focus();
                        return;
                    }
                }
                if (Convert.ToDecimal(txtUnitPrice.Text.Trim()) == 0 && _isCombineAdding == false && !IsGiftVoucher(_itm.Mi_itm_tp) && (SSPriceBookSequance == "0" || string.IsNullOrEmpty(SSPriceBookSequance)))
                {
                    bool _isTerminate = CheckQty(false, "btnAddItem");
                    if (_isTerminate)
                    {
                        return;
                    }
                }
                if (CheckBlockItem(txtItem.Text.ToUpper().Trim(), SSPRomotionType, _isCombineAdding))
                    return;
                if (_isCombineAdding == false && _MasterProfitCenter.Mpc_without_price == false && _priceBookLevelRef.Sapl_is_without_p == false)
                {
                    PriceDetailRef _lsts = CHNLSVC.Sales.GetPriceDetailByItemLineSeq(txtItem.Text.ToUpper().Trim(), Convert.ToInt32(SSPriceBookItemSequance), Convert.ToInt32(SSPriceBookSequance));
                    if (_lsts != null && _isCombineAdding == false)
                    {
                        if (string.IsNullOrEmpty(_lsts.Sapd_itm_cd))
                        {
                            DisplayMessage(txtItem.Text.ToUpper() + " does not available price. Please contact IT dept.");
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
                                        if (!_MasterProfitCenter.Mpc_edit_price)
                                        {
                                            //comment by darshana 23-08-2013
                                            //re-open by chamal 18-Nov-2014
                                            if (Math.Round(_lsts.Sapd_itm_price, 0) != Math.Round(pickUPrice, 0))
                                            {
                                                DisplayMessage("Price Book price and the unit price is different. Please check the price you selected!");
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            if (sysUPrice != pickUPrice)
                                                if (sysUPrice > pickUPrice)
                                                {
                                                    decimal sysEditRate = _MasterProfitCenter.Mpc_edit_rate;
                                                    decimal ddUprice = sysUPrice - ((sysUPrice * sysEditRate) / 100);
                                                    if (ddUprice > pickUPrice)
                                                    {
                                                        DisplayMessage("Price Book price and the unit price is different. Please check the price you selected!");
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
                            DisplayMessage(txtItem.Text.ToUpper() + " does not available price. Please contact IT dept.");
                            return;
                        }
                    }
                }
                if (_isCombineAdding == false)
                    if (chkDeliverLater == false && chkDeliverNow == false || (_isRegistrationMandatory))
                    {
                        if (_itm.Mi_is_ser1 == 1)
                        {
                            var _dup = ScanSerialList.Where(x => x.Tus_itm_cd == txtItem.Text.ToUpper() && x.Tus_ser_1 == ScanSerialNo).ToList();
                            if (_dup != null)
                                if (_dup.Count > 0)
                                {
                                    DisplayMessage(ScanSerialNo + " serial is already picked!");
                                    txtSerialNo.Focus();
                                    return;
                                }
                        }

                        if (!IsPriceLevelAllowDoAnyStatus)
                        {
                            if (_serLst != null)
                                if (string.IsNullOrEmpty(_serLst.Tus_com))
                                {
                                    if (_serLst.Tus_itm_stus != cmbStatus.Text.Trim())
                                    {
                                        DisplayMessage(ScanSerialNo + " serial status is not match with the price level status");
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
                    if (chkDeliverLater == false && chkDeliverNow == false || (_isRegistrationMandatory))
                    {
                        decimal _pickQty = 0;
                        if (IsPriceLevelAllowDoAnyStatus)
                            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.ToUpper().Trim()).ToList().Select(x => x.Sad_qty).Sum();
                        else
                            _pickQty = _invoiceItemList.Where(x => x.Sad_itm_cd == txtItem.Text.ToUpper().Trim() && x.Mi_itm_stus == cmbStatus.Text.Trim()).ToList().Select(x => x.Sad_qty).Sum();

                        _pickQty += Convert.ToDecimal(txtQty.Text.Trim());
                        List<InventoryLocation> _inventoryLocation = CHNLSVC.Inventory.GetItemInventoryBalance(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim());

                        if (_inventoryLocation != null)
                            if (_inventoryLocation.Count > 0)
                            {
                                decimal _invBal = _inventoryLocation[0].Inl_qty;
                                if (_pickQty > _invBal)
                                {
                                    string msg = txtItem.Text.ToUpper() + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_invBal));
                                    DisplayMessage(msg);
                                    return;
                                }
                            }
                            else
                            {
                                string msg = txtItem.Text.ToUpper() + " item qty exceeding it's inventory balance. Inventory balance : 0.00";
                                DisplayMessage(msg);
                                return;
                            }
                        else
                        {
                            DisplayMessage(txtItem.Text.ToUpper() + " item qty exceeding it's inventory balance. Inventory balance : 0.00");
                            return;
                        }

                        if (_itm.Mi_is_ser1 == 1 && ScanSerialList.Count > 0)
                        {
                            var _serDup = (from _lst in ScanSerialList
                                           where _lst.Tus_ser_1 == txtSerialNo.Text.Trim() && _lst.Tus_itm_cd == txtItem.Text.ToUpper().Trim()
                                           select _lst).ToList();

                            if (_serDup != null)
                                if (_serDup.Count > 0)
                                {
                                    DisplayMessage("Selected Serial is duplicating.");
                                    return;
                                }
                        }
                    }
                List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
                if (_lvl != null)
                    if (_lvl.Count > 0)
                    {
                        var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == cmbStatus.Text.Trim() select _l).ToList();
                        if (_lst != null)
                            if (_lst.Count > 0)
                            {
                                DataTable _temWarr = CHNLSVC.Sales.GetPCWara(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim(), Convert.ToDateTime(dtpRecDate.Text).Date);

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
                                    MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(txtItem.Text.ToUpper().Trim(), cmbStatus.Text.Trim());
                                    if (_period != null)
                                    {
                                        WarrantyPeriod = _period.Mwp_val;
                                        WarrantyRemarks = _period.Mwp_rmk;
                                    }
                                    else
                                    {
                                        DisplayMessage("Warranty period not setup by the inventory department. Please contact inventory department");
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
                    var _duplicateItem = from _list in _invoiceItemList
                                         where _list.Sad_itm_cd == txtItem.Text.ToUpper() && _list.Sad_itm_stus == cmbStatus.Text && _list.Sad_pbook == cmbBook.Text && _list.Sad_pb_lvl == cmbLevel.Text && _list.Sad_unit_rt == Convert.ToDecimal(txtUnitPrice.Text) && _list.Sad_disc_rt == Convert.ToDecimal(txtDisRate.Text.Trim())
                                         select _list;

                    if (_duplicateItem.Count() > 0)
                    {
                        _isDuplicateItem = true;
                        foreach (var _similerList in _duplicateItem)
                        {
                            _duplicateComLine = _similerList.Sad_job_line;
                            _duplicateItmLine = _similerList.Sad_itm_line;
                            _similerList.Sad_disc_amt = Convert.ToDecimal(_similerList.Sad_disc_amt) + Convert.ToDecimal(txtDisAmt.Text);
                            _similerList.Sad_itm_tax_amt = Convert.ToDecimal(_similerList.Sad_itm_tax_amt) + Convert.ToDecimal(txtTaxAmt.Text);
                            _similerList.Sad_qty = Convert.ToDecimal(_similerList.Sad_qty) + Convert.ToDecimal(txtQty.Text);
                            _similerList.Sad_unit_amt = Convert.ToDecimal(txtUnitPrice.Text) * _similerList.Sad_qty;
                            _similerList.Sad_tot_amt = Convert.ToDecimal(_similerList.Sad_tot_amt) + Convert.ToDecimal(txtLineTotAmt.Text);
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

                //Scan By Serial ----------start----------------------------------
                if ((chkDeliverLater == false && chkDeliverNow == false) || _priceBookLevelRef.Sapl_is_serialized || IsGiftVoucher(_itm.Mi_itm_tp) || _isRegistrationMandatory)
                {
                    if (_isFirstPriceComItem)
                        _isCombineAdding = true;
                    if (ScanSequanceNo == 0) ScanSequanceNo = -100;
                    if (_itm.Mi_is_ser1 == 1)
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
                    if (_itm.Mi_is_ser1 == 0)
                    {
                        if (_nonserLst.Count < Convert.ToDecimal(txtQty.Text.Trim()))
                        {
                            if (_isAgePriceLevel == false)
                            {
                                DisplayMessage(txtItem.Text.ToUpper() + " item qty exceeding it's inventory balance. Inventory balance : " + FormatToQty(Convert.ToString(_nonserLst.Count)));
                                var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                                foreach (InvoiceItem _one in _partly)
                                    //DeleteIfPartlyAdded(_one.Sad_job_line, _one.Sad_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Sad_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);
                                    return;
                            }
                            else
                            {
                                if (gvInvoiceItem.Rows.Count > 0)
                                {
                                    DisplayMessage("This serial can't select under aging price level. Please check the aging status with IT dept.");
                                    return;
                                }
                                var _partly = _invoiceItemList.Where(x => x.Sad_job_line == SSCombineLine).ToList();
                                foreach (InvoiceItem _one in _partly)
                                    //   DeleteIfPartlyAdded(_one.Sad_job_line, _one.Sad_itm_cd, _one.Sad_unit_rt, _one.Sad_pbook, _one.Sad_pb_lvl, _one.Sad_qty, _one.Sad_disc_amt, _one.Sad_itm_tax_amt, _one.Sad_itm_line, _one.Sad_itm_line);
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
                        _chk.Tus_itm_stus = cmbStatus.Text;
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

                    //gvPopSerial.DataSource = new List<ReptPickSerials>();
                    //gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList();
                    //var filenamesList = new BindingList<ReptPickSerials>(ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList());
                    //gvGiftVoucher.DataSource = filenamesList;

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
                    {
                        if (_itm.Mi_is_ser1 == 1)
                        {
                            var _dup = (from _i in InvoiceSerialList where _i.Sap_ser_1 == txtSerialNo.Text.Trim() && _i.Sap_itm_cd == txtItem.Text.ToUpper().Trim() select _i).ToList();
                            if (_dup != null)
                                if (_dup.Count > 0)
                                    _isDuplicate = true;
                        }
                    }
                if (_isDuplicate == false)
                {
                    InvoiceSerial _invser = new InvoiceSerial(); _invser.Sap_del_loc = Session["UserDefLoca"].ToString();
                    _invser.Sap_itm_cd = txtItem.Text.ToUpper().Trim(); _invser.Sap_itm_line = _lineNo;
                    _invser.Sap_remarks = string.Empty; _invser.Sap_seq_no = Convert.ToInt32(SSPriceBookSequance);
                    _invser.Sap_ser_1 = txtSerialNo.Text; _invser.Sap_ser_line = _isCombineAdding ? Convert.ToInt32(SSCombineLine) : 0;
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
                        if (string.IsNullOrEmpty(_combineStatus)) _combineStatus = cmbStatus.Text;
                        if (_combineQty == 0) _combineQty = Convert.ToDecimal(txtQty.Text);
                        if (chkDeliverLater == true || chkDeliverNow == true)
                        {
                            foreach (PriceCombinedItemRef _list in _MainPriceCombinItem)
                            {
                                string _originalItm = _list.Sapc_itm_cd; string _similerItem = _list.Similer_item;
                                _combineStatus = _list.Status; if (!string.IsNullOrEmpty(_similerItem)) txtItem.Text = _similerItem; else txtItem.Text = _list.Sapc_itm_cd;
                                if (_priceBookLevelRef.Sapl_is_serialized) txtSerialNo.Text = _list.Sapc_sub_ser;
                                LoadItemDetail(txtItem.Text.ToUpper().Trim());
                                if (IsGiftVoucher(_itemdetail.Mi_itm_tp))
                                {
                                    foreach (ReptPickSerials _lists in PriceCombinItemSerialList.Where(x => x.Tus_itm_cd == txtItem.Text.ToUpper().Trim()).ToList())
                                    {
                                        txtSerialNo.Text = _lists.Tus_ser_1;
                                        ScanSerialNo = _lists.Tus_ser_1;
                                        string _originalItms = _lists.Tus_session_id;
                                        if (string.IsNullOrEmpty(_originalItm))
                                        {
                                            txtItem.Text = _lists.Tus_itm_cd;
                                            _serial2 = _lists.Tus_ser_2;
                                            _prefix = _lists.Tus_ser_3;
                                            LoadItemDetail(txtItem.Text.ToUpper().Trim());
                                            cmbStatus.Text = _combineStatus;
                                            decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_price).Sum();
                                            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_qty).Sum();
                                            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                            if (_list.Sapc_increse)
                                                txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty)));
                                            else
                                                txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                            txtDisRate.Text = FormatToCurrency("0");
                                            txtDisAmt.Text = FormatToCurrency("0");
                                            txtTaxAmt.Text = FormatToCurrency("0");
                                            txtLineTotAmt.Text = FormatToCurrency("0");
                                            CalculateItem();
                                            AddItem(_isPromotion, string.Empty);
                                        }
                                        else
                                        {
                                            txtItem.Text = _lists.Tus_itm_cd;
                                            _serial2 = _lists.Tus_ser_2;
                                            _prefix = _lists.Tus_ser_3;
                                            LoadItemDetail(txtItem.Text.ToUpper().Trim());
                                            cmbStatus.Text = _combineStatus;
                                            decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_price).Sum();
                                            decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_qty).Sum();
                                            txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                            if (_list.Sapc_increse)
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
                                }
                                else
                                {
                                    cmbStatus.Text = _combineStatus; txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
                                    if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty /* * _combineQty */))); else txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty)));
                                    txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion, _originalItm);
                                    _combineCounter += 1;
                                }
                            }
                        }
                        else
                        {
                            if (PriceCombinItemSerialList == null || PriceCombinItemSerialList.Count == 0) _isSingleItemSerializedInCombine = false;
                            foreach (ReptPickSerials _list in PriceCombinItemSerialList)
                            {
                                txtSerialNo.Text = _list.Tus_ser_1;
                                ScanSerialNo = _list.Tus_ser_1;
                                string _originalItm = _list.Tus_session_id;
                                _combineStatus = _list.Tus_itm_stus;
                                if (string.IsNullOrEmpty(_originalItm))
                                {
                                    txtItem.Text = _list.Tus_itm_cd;
                                    _serial2 = _list.Tus_ser_2;
                                    _prefix = _list.Tus_ser_3;
                                    LoadItemDetail(txtItem.Text.ToUpper().Trim());
                                    cmbStatus.Text = _combineStatus;
                                    decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_price).Sum();
                                    decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_qty).Sum();
                                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice)); txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty)));
                                    txtDisRate.Text = FormatToCurrency("0");
                                    txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0");
                                    txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem();
                                    AddItem(_isPromotion, string.Empty);
                                }
                                else
                                {
                                    txtItem.Text = _list.Tus_itm_cd;
                                    _serial2 = _list.Tus_ser_2;
                                    _prefix = _list.Tus_ser_3;
                                    LoadItemDetail(txtItem.Text.ToUpper().Trim());
                                    cmbStatus.Text = _combineStatus;
                                    decimal UnitPrice = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_price).Sum();
                                    decimal Qty = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(y => y.Sapc_qty).Sum();
                                    var _Increaseable = _MainPriceCombinItem.Where(x => x.Sapc_itm_cd == txtItem.Text.ToUpper().Trim()).Select(x => x.Sapc_increse).Distinct().ToList();
                                    bool _isIncreaseable = Convert.ToBoolean(_Increaseable[0]);
                                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(UnitPrice));
                                    if (_isIncreaseable)
                                        txtQty.Text = FormatToQty(Convert.ToString((Qty * _combineQty)));
                                    else
                                        txtQty.Text = FormatToQty(Convert.ToString((Qty)));
                                    txtDisRate.Text = FormatToCurrency("0");
                                    txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion, _originalItm);
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
                                    LoadItemDetail(txtItem.Text.ToUpper().Trim()); cmbStatus.Text = _combineStatus;
                                    txtUnitPrice.Text = FormatToCurrency(Convert.ToString(_list.Sapc_price));
                                    if (_list.Sapc_increse) txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty * _combineQty))); else txtQty.Text = FormatToQty(Convert.ToString((_list.Sapc_qty)));
                                    txtDisRate.Text = FormatToCurrency("0"); txtDisAmt.Text = FormatToCurrency("0");
                                    txtTaxAmt.Text = FormatToCurrency("0"); txtLineTotAmt.Text = FormatToCurrency("0");
                                    CalculateItem(); AddItem(_isPromotion, _originalItm);
                                    _combineCounter += 1;
                                }
                            }
                        }

                        if (chkDeliverLater == true || chkDeliverNow == true)
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
                                if (ucPayModes1.HavePayModes)
                                    ucPayModes1.LoadData();
                                if (_isCombineAdding == false)
                                {
                                    //_??if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    //_??{
                                    //_??    if (chkDeliverLater == false && chkDeliverNow == false || (_isRegistrationMandatory))
                                    //_??    {
                                    //_??        txtSerialNo.Focus();
                                    //_??    }
                                    //_??    else
                                    //_??    {
                                    //_??        txtItem.Focus();
                                    //_??    }
                                    //_??}
                                    //_??else
                                    //_??{
                                    //_??    ucPayModes1.button1.Focus();
                                    //_??}
                                }
                                return;
                            }

                        //hdnSerialNo.Value = ""

                        if (chkDeliverLater == false && chkDeliverNow == false || (_isRegistrationMandatory))
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
                                    if (ucPayModes1.HavePayModes)
                                        ucPayModes1.LoadData();
                                    if (_isCombineAdding == false)
                                    {
                                        //_??if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                                        //_??{
                                        //_??    if (chkDeliverLater == false && chkDeliverNow == false || (_isRegistrationMandatory)) 
                                        //_??    {
                                        //_??        txtSerialNo.Focus(); 
                                        //_??    }
                                        //_??    else 
                                        //_??    {
                                        //_??        txtItem.Focus();
                                        //_??    }
                                        //_??}
                                        //_??else
                                        //_??{
                                        //_??    ucPayModes1.button1.Focus(); 
                                        //_??} 
                                    }
                                    return;
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
                                    if (ucPayModes1.HavePayModes)
                                        ucPayModes1.LoadData();
                                    if (_isCombineAdding == false)
                                    {
                                        //_??if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        //_??{
                                        //_??    if (chkDeliverLater == false && chkDeliverNow == false)
                                        //_??    {
                                        //_??        txtSerialNo.Focus();
                                        //_??    }
                                        //_??    else
                                        //_??    {
                                        //_??        txtItem.Focus();
                                        //_??    }
                                        //_??}
                                        //_??else
                                        //_??{
                                        //_??    ucPayModes1.button1.Focus();
                                        //_??}
                                    }
                                    return;
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
                                    if (ucPayModes1.HavePayModes)
                                        ucPayModes1.LoadData();
                                    if (_isCombineAdding == false)
                                    {
                                        //_??if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        //_??{
                                        //_??    if (chkDeliverLater == false && chkDeliverNow == false)
                                        //_??    {
                                        //_??        txtSerialNo.Focus();
                                        //_??    }
                                        //_??    else
                                        //_??    {
                                        //_??        txtItem.Focus();
                                        //_??    }
                                        //_??}
                                        //_??else
                                        //_??{
                                        //_??    ucPayModes1.button1.Focus();
                                        //_??}
                                    }
                                    return;
                                }//hdnSerialNo.Value = ""
                        }
                    }
                }

                // #endregion Rooting for Invnetory Combine

                txtSerialNo.Text = "";
                ClearAfterAddItem();
                SSPriceBookSequance = "0";
                SSPriceBookItemSequance = "0";
                SSPriceBookPrice = 0;
                if (_isCombineAdding == false) SSPromotionCode = string.Empty;
                SSPRomotionType = 0;
                txtItem.Focus();
                BindAddItem();
                SetDecimalTextBoxForZero(true);
                decimal _tobepays = 0;

                if (lblSVatStatus.Text == "Available")
                    _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim()) - Convert.ToDecimal(lblGrndTax.Text.Trim());
                else
                    _tobepays = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());

                ucPayModes1.TotalAmount = _tobepays;
                ucPayModes1.InvoiceItemList = _invoiceItemList;
                ucPayModes1.SerialList = InvoiceSerialList;
                ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(_tobepays));
                ucPayModes1.IsTaxInvoice = chkTaxPayable.Checked;
                if (ucPayModes1.HavePayModes && _isCombineAdding == false)
                    ucPayModes1.LoadData();
                if (_loyaltyType != null)
                {
                    ucPayModes1.LoyaltyCard = _loyaltyType.Salt_loty_tp;
                }
                //  LookingForBuyBack();
                if (_isCombineAdding == false)
                {
                    if (cmbInvType.Text != "HS")
                    {
                        //_??if (MessageBox.Show("Do you need to add another item?", "Another Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //_??{
                        //_??    if (chkDeliverLater == false && chkDeliverNow == false)
                        //_??    {
                        //_??        txtSerialNo.Focus();
                        //_??    }
                        //_??    else
                        //_??    {
                        //_??        txtItem.Focus();
                        //_??    }
                        //_??}
                        //_??else
                        //_??{
                        //_??    ucPayModes1.button1.Focus();
                        //_??}
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
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
            _tempItem.Sad_itm_stus = cmbStatus.Text;
            _tempItem.Sad_itm_stus_desc = getItemStatusDesc(cmbStatus.Text);
            _tempItem.Sad_itm_tax_amt = Convert.ToDecimal(txtTaxAmt.Text);
            _tempItem.Sad_itm_tp = _item.Mi_itm_tp;
            _tempItem.Sad_job_no = "";
            _tempItem.Sad_res_line_no = 0;
            _tempItem.Sad_res_no = "";
            //if (_proVouInvcItem == txtItem.Text.ToString())
            //{
            //    if (string.IsNullOrEmpty(lblPromoVouUsedFlag.Text))
            //    {
            //        lblPromoVouUsedFlag.Text = "U";
            //        _proVouInvcLine = _lineNo;
            //        _tempItem.Sad_res_line_no = Convert.ToInt32(lblPromoVouNo.Text.ToString());
            //        _tempItem.Sad_res_no = "PROMO_VOU";
            //    }
            //}
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
            _tempItem.Sad_job_no = txtSerialNo.Text;
            if (!string.IsNullOrEmpty(txtDisRate.Text.Trim()) && IsNumeric(txtDisRate.Text.Trim()))
                if (Convert.ToDecimal(txtDisRate.Text.Trim()) > 0 && GeneralDiscount != null)
                {
                    _tempItem.Sad_dis_type = "M";
                    _tempItem.Sad_dis_seq = GeneralDiscount.Sgdd_seq;
                    _tempItem.Sad_dis_line = 0;
                }
            return _tempItem;
        }

        private void CalculateGrandTotal(decimal _qty, decimal _uprice, decimal _discount, decimal _tax, bool _isAddition)
        {
            if (_isAddition)//++
            {
                GrndSubTotal = GrndSubTotal + Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount + Convert.ToDecimal(_discount);
                GrndTax = GrndTax + Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = FormatToCurrency(Convert.ToString(GrndSubTotal));
                lblGrndDiscount.Text = FormatToCurrency(Convert.ToString(GrndDiscount));
                lblGrndTax.Text = FormatToCurrency(Convert.ToString(GrndTax));
            }
            else//--
            {
                GrndSubTotal = GrndSubTotal - Convert.ToDecimal(_qty) * Convert.ToDecimal(_uprice);
                GrndDiscount = GrndDiscount - Convert.ToDecimal(_discount);
                GrndTax = GrndTax - Convert.ToDecimal(_tax);

                lblGrndSubTotal.Text = FormatToCurrency(Convert.ToString(GrndSubTotal));
                lblGrndDiscount.Text = FormatToCurrency(Convert.ToString(GrndDiscount));
                lblGrndTax.Text = FormatToCurrency(Convert.ToString(GrndTax));
            }

            lblGrndAfterDiscount.Text = FormatToCurrency(Convert.ToString(GrndSubTotal - GrndDiscount));
            //lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(FigureRoundUp(GrndSubTotal - GrndDiscount + GrndTax, true,false)));
            if (_invoiceItemList != null || _invoiceItemList.Count > 0)
                lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString(_invoiceItemList.Sum(x => x.Sad_tot_amt)));
            else
                lblGrndTotalAmount.Text = FormatToCurrency(Convert.ToString("0"));
            //TODO: remove remark, when apply payment UC
            //txtPayAmount.Text = FormatToCurrency(Convert.ToString((Convert.ToDecimal(lblGrndTotalAmount.Text) - Convert.ToDecimal(_paidAmount))));
            //lblPayBalance.Text = lblGrndTotalAmount.Text;
        }

        private void ClearAfterAddItem()
        {
            if (cmbInvType.Text != "HS")
            {
                txtItem.Text = "";
            }
            cmbStatus.Text = DefaultItemStatus;
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

        private bool CheckItemWarranty(string _item, string _status)
        {
            bool _isNoWarranty = false;
            List<PriceBookLevelRef> _lvl = _priceBookLevelRefList;
            if (_lvl != null)
            {
                if (_lvl.Count > 0)
                {
                    var _lst = (from _l in _lvl where _l.Sapl_itm_stuts == _status.Trim() select _l).ToList();
                    if (_lst != null)
                        if (_lst.Count > 0)
                        {
                            DataTable _temWarr = CHNLSVC.Sales.GetPCWara(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), _item.Trim(), _status.Trim(), Convert.ToDateTime(dtpRecDate.Text).Date);

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
                                else
                                {
                                    _isNoWarranty = true;
                                }
                            }
                        }
                }
            }
            return _isNoWarranty;
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

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (_IsVirtualItem) return;
            try
            {
                if (Convert.ToDecimal(txtQty.Text.Trim()) < 0)
                {
                    DisplayMessage("Quantity should be positive value.");
                    return;
                }

                CheckQty(false, "txtQty");

            }
            catch (Exception ex)
            {
                txtQty.Text = FormatToQty("1");
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
                if (Convert.ToDecimal(txtDisRate.Text.Trim()) < 0)
                {
                    //MessageBox.Show("Discount rate should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtDisRate.Clear();
                    //txtDisRate.Text = FormatToQty("0");
                    //return;
                }

                if (string.IsNullOrEmpty(lblPromoVouNo))
                {
                    if (_isCompleteCode && _MasterProfitCenter.Mpc_edit_price && Convert.ToDecimal(txtDisRate.Text.Trim()) > 0)
                    {
                        DisplayMessage("You are not allow discount for com codes!");
                        txtDisRate.Text = "";
                        txtDisRate.Text = FormatToQty("0");
                        return;
                    }
                }
                else
                {
                    if (Convert.ToDecimal(txtQty.Text) != 1)
                    {
                        DisplayMessage("Promotion voucher allow for only one(1) item!");
                        txtDisRate.Text = "";
                        txtDisRate.Text = FormatToQty("0");
                        return;
                    }
                }
                CheckNewDiscountRate();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        private bool CheckNewDiscountAmount()
        {
            if (string.IsNullOrEmpty(txtItem.Text)) return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                DisplayMessage("Please select the valid qty");
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
                    if (GeneralDiscount != null)
                    {
                        decimal vals = GeneralDiscount.Sgdd_disc_val;
                        decimal rates = GeneralDiscount.Sgdd_disc_rt;

                        if (vals < _disAmt && rates == 0)
                        {
                            DisplayMessage("You can not discount price more than " + vals + ".");
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
                        if (string.IsNullOrEmpty(lblPromoVouNo))
                        {
                            GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToDateTime(dtpRecDate.Text).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCusCode.Text.Trim(), txtItem.Text.ToUpper().Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                        }
                        else
                        {
                            GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text.Trim(), Convert.ToDateTime(dtpRecDate.Text).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtItem.Text.ToUpper().Trim(), lblPromoVouNo.Trim());
                            if (GeneralDiscount != null)
                            {
                                _IsPromoVou = true;
                                GeneralDiscount.Sgdd_seq = Convert.ToInt32(lblPromoVouNo);
                            }
                        }

                        if (GeneralDiscount != null)
                        {
                            decimal vals = GeneralDiscount.Sgdd_disc_val;
                            decimal rates = GeneralDiscount.Sgdd_disc_rt;

                            if (_IsPromoVou == true)
                            {
                                if (vals < _disAmt && rates == 0)
                                {
                                    string msg = "Voucher discount amount should be " + vals + "!. Not allowed discount amount " + _disAmt + " discounted price is " + txtLineTotAmt.Text;
                                    DisplayMessage(msg);
                                    txtDisAmt.Text = FormatToCurrency("0");
                                    txtDisRate.Text = FormatToCurrency("0");
                                    _isEditDiscount = false;
                                    return false;
                                }
                            }

                            if (vals < _disAmt && rates == 0)
                            {
                                DisplayMessage("You can not discount price more than " + vals + ".");
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

            if (string.IsNullOrEmpty(txtDisAmt.Text)) txtDisAmt.Text = FormatToCurrency("0");
            decimal val = Convert.ToDecimal(txtDisAmt.Text);
            txtDisAmt.Text = FormatToCurrency(Convert.ToString(val));
            CalculateItem();
            return true;
        }

        protected bool CheckNewDiscountRate()
        {
            if (string.IsNullOrEmpty(txtItem.Text.ToUpper())) return false;
            if (IsNumeric(txtQty.Text) == false)
            {
                DisplayMessage("Please select the valid qty");
                return false;
            }
            if (Convert.ToDecimal(txtQty.Text.Trim()) == 0) return false;

            if (!string.IsNullOrEmpty(txtDisRate.Text) && _isEditPrice == false)
            {
                decimal _disRate = Convert.ToDecimal(txtDisRate.Text);
                bool _IsPromoVou = false;
                if (_disRate > 0)
                {
                    if (GeneralDiscount == null) GeneralDiscount = new CashGeneralEntiryDiscountDef();
                    if (string.IsNullOrEmpty(lblPromoVouNo))
                    {
                        GeneralDiscount = CHNLSVC.Sales.GetGeneralDiscountDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), Convert.ToDateTime(dtpRecDate.Text).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtCusCode.Text.Trim(), txtItem.Text.ToUpper().Trim(), _priceBookLevelRef.Sapl_is_serialized ? true : false, false);
                    }
                    else
                    {
                        GeneralDiscount = CHNLSVC.Sales.GetPromoVoucherNoDefinition(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text.Trim(), Convert.ToDateTime(dtpRecDate.Text).Date, cmbBook.Text.Trim(), cmbLevel.Text.Trim(), txtItem.Text.ToUpper().Trim(), lblPromoVouNo.Trim());
                        if (GeneralDiscount != null)
                        {
                            _IsPromoVou = true;
                            GeneralDiscount.Sgdd_seq = Convert.ToInt32(lblPromoVouNo);
                        }
                    }
                    if (GeneralDiscount != null)
                    {
                        decimal vals = GeneralDiscount.Sgdd_disc_val;
                        decimal rates = GeneralDiscount.Sgdd_disc_rt;

                        if (lblPromoVouUsedFlag.Contains("U") == true)
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
                                    string msg = "Voucher discount amount should be " + vals + ".\nNot allowed discount rate " + _disRate + "%";
                                    DisplayMessage(msg);
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
                                    DisplayMessage("Voucher discount rate should be " + rates + "% !. Not allowed discount rate " + _disRate + "% discounted price is " + txtLineTotAmt.Text);
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
                                DisplayMessage("Exceeds maximum discount allowed " + rates + "% !.\n" + _disRate + "% discounted price is " + txtLineTotAmt.Text);
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
                            //lblPromoVouUsedFlag.Text = "U";
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
            btnConfirm.Focus();
            return true;
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
                if (string.IsNullOrEmpty(txtDisAmt.Text)) return;
                if (Convert.ToDecimal(txtDisAmt.Text) < 0)
                {
                    //MessageBox.Show("Discount amount should be positive value.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtDisAmt.Clear();
                    //txtDisAmt.Text = FormatToQty("0");
                    //return;
                }
                CheckNewDiscountAmount();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void lbtndelitem_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
                if (dr != null)
                {
                    Int32 _rowIndex = dr.RowIndex;
                    Int32 _colIndex = 0;

                    Label lblsad_itm_line = dr.FindControl("lblsad_itm_line") as Label;
                    Label lblsad_res_no = dr.FindControl("lblsad_res_no") as Label;
                    Label InvItm_ResLine = dr.FindControl("InvItm_ResLine") as Label;
                    Label InvItm_Item = dr.FindControl("InvItm_Item") as Label;
                    Label InvItm_UPrice = dr.FindControl("InvItm_UPrice") as Label;
                    Label InvItm_Book = dr.FindControl("InvItm_Book") as Label;
                    Label InvItm_Level = dr.FindControl("InvItm_Level") as Label;
                    Label InvItm_Qty = dr.FindControl("InvItm_Qty") as Label;
                    Label InvItm_DisAmt = dr.FindControl("InvItm_DisAmt") as Label;
                    Label InvItm_TaxAmt = dr.FindControl("InvItm_TaxAmt") as Label;

                    if (_rowIndex != -1)
                    {
                        if (_colIndex == 0)
                        {
                            if (_recieptItem != null)
                                if (_recieptItem.Count > 0)
                                {
                                    DisplayMessage("You have already payment added!");
                                    return;
                                }
                            int _combineLine = Convert.ToInt32(lblsad_itm_line.Text);
                            string _resNo = lblsad_res_no.Text.Trim();//Add by Chamal 6-Jul-2014
                            int _resLine = Convert.ToInt32(InvItm_ResLine.Text.Trim());//Add by Chamal 6-Jul-2014
                            if (_MainPriceSerial != null)
                                if (_MainPriceSerial.Count > 0)
                                {
                                    string _item = InvItm_Item.Text.Trim();
                                    decimal _uRate = Convert.ToDecimal(InvItm_UPrice.Text.Trim());
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
                                CalculateGrandTotal(Convert.ToDecimal(InvItm_Qty.Text.Trim()), Convert.ToDecimal(InvItm_UPrice.Text.Trim()), Convert.ToDecimal(InvItm_DisAmt.Text.Trim()), Convert.ToDecimal(InvItm_TaxAmt.Text), false);
                                InvoiceSerialList.RemoveAll(x => x.Sap_itm_line == _tempList[_rowIndex].Sad_itm_line);
                                _tempList.RemoveAt(_rowIndex);
                            }
                            _invoiceItemList = _tempList;

                            if (_resNo == "PROMO_VOU" && _resLine == Convert.ToInt32(lblPromoVouNo))
                            {
                                //Add by Chamal 6-Jul-2014
                                //lblPromoVouUsedFlag.Text = "";
                                //lblPromoVouNo.Text = "";
                                //_proVouInvcItem = "";
                                //_proVouInvcLine = 0;
                            }

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
                            //gvPopSerial.DataSource = new List<ReptPickSerials>();
                            //gvPopSerial.DataSource = ScanSerialList.Where(x => x.Tus_ser_1 != "N/A" && !IsGiftVoucher(x.ItemType)).ToList();
                            //gvGiftVoucher.DataSource = new List<ReptPickSerials>();
                            //gvGiftVoucher.DataSource = ScanSerialList.Where(x => IsGiftVoucher(x.ItemType)).ToList();

                            //update promotion
                            //update promotion
                            List<InvoiceItem> _temItems = (from _invItm in _invoiceItemList
                                                           where !string.IsNullOrEmpty(_invItm.Sad_promo_cd)
                                                           select _invItm).ToList<InvoiceItem>();
                            if (_temItems != null && _temItems.Count > 0)
                            {
                                ucPayModes1.ISPromotion = true;
                            }
                            else
                                ucPayModes1.ISPromotion = false;
                            ucPayModes1.TotalAmount = Convert.ToDecimal(lblGrndTotalAmount.Text.Trim());
                            ucPayModes1.Amount.Text = FormatToCurrency(lblGrndTotalAmount.Text.Trim());
                            ucPayModes1.LoadData();
                            //  LookingForBuyBack();

                            //REGISTRATION PROCESS
                            //ADDED 2013/12/06
                            //search invoice item list if registration item not found set visibility
                            if (_isRegistrationMandatory)
                            {
                                bool _isHaveReg = false;
                                if (_invoiceItemList != null && _invoiceItemList.Count > 0)
                                {
                                    foreach (InvoiceItem _invItm in _invoiceItemList)
                                    {
                                        //check item need registration
                                        MasterItem _itm = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().Trim());
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
                                    //_isNeedRegistrationReciept = false;
                                    //lnkProcessRegistration.Visible = false;
                                }
                            }
                            //END

                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtRecType.Text == "ADVAN")
            {
                MultiView1.ActiveViewIndex = 0;
                if (cmbInvType.Text == "HS" && _isCalProcess == false)
                {
                    DisplayMessage("please click on the process button.");
                    return;
                }

                if (_invoiceItemList.Count == 0)
                {
                    DisplayMessage("Please enter the items details.");
                    return;
                }

                dgvItem.DataSource = new int[] { };
                dgvItem.DataBind();

                List<gvInvoiceItems> oShowItems = new List<gvInvoiceItems>();

                foreach (InvoiceItem itm in _invoiceItemList)
                {
                    MasterItem _item = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), itm.Sad_itm_cd);
                    ReptPickSerials _tempItem = new ReptPickSerials();
                    _tempItem.Tus_itm_desc = _item.Mi_shortdesc;
                    _tempItem.Tus_itm_model = _item.Mi_model;
                    _tempItem.Tus_itm_brand = _item.Mi_brand;
                    _tempItem.Tus_base_doc_no = null;
                    _tempItem.Tus_base_itm_line = 0;
                    _tempItem.Tus_isapp = 0;
                    _tempItem.Tus_iscovernote = 0;
                    _tempItem.Tus_com = Session["UserCompanyCode"].ToString();
                    _tempItem.Tus_loc = Session["UserDefLoca"].ToString();

                    _tempItem.Tus_itm_cd = itm.Sad_itm_cd;
                    _tempItem.Tus_itm_stus = itm.Sad_itm_stus;
                    _tempItem.Tus_unit_price = itm.Sad_unit_rt;
                    _tempItem.Tus_unit_cost = itm.Sad_unit_amt;

                    _tempItem.Tus_orig_grnno = itm.Sad_pbook;
                    _tempItem.Tus_orig_supp = itm.Sad_pb_lvl;
                    _tempItem.Tus_resqty = itm.Sad_itm_tax_amt;
                    _tempItem.Tus_qty = itm.Sad_qty;
                    _tempItem.Tus_ser_1 = itm.Sad_job_no;//serial 
                    _tempItem.Tus_new_remarks = itm.Sad_promo_cd;
                    _ResList.Add(_tempItem);

                    //dgvItem.Rows.Add();
                    //dgvItem["col_itmItem", dgvItem.Rows.Count - 1].Value = itm.Sad_itm_cd;
                    //dgvItem["colpb", dgvItem.Rows.Count - 1].Value = itm.Sad_pbook;
                    //dgvItem["colpblvl", dgvItem.Rows.Count - 1].Value = itm.Sad_pb_lvl;
                    //dgvItem["col_itmStatus", dgvItem.Rows.Count - 1].Value = itm.Sad_itm_stus;
                    //dgvItem["colQty", dgvItem.Rows.Count - 1].Value = itm.Sad_qty;
                    //dgvItem["colRate", dgvItem.Rows.Count - 1].Value = itm.Sad_unit_rt.ToString("N2");
                    //dgvItem["colamt", dgvItem.Rows.Count - 1].Value = itm.Sad_unit_amt.ToString("N2");
                    //dgvItem["colTax", dgvItem.Rows.Count - 1].Value = itm.Sad_itm_tax_amt.ToString("N2");
                    //lblSalesType.Text = cmbInvType.Text;
                    //dgvItem["col_itmDesc", dgvItem.Rows.Count - 1].Value = _item.Mi_shortdesc;
                    //dgvItem["col_itmModel", dgvItem.Rows.Count - 1].Value = _item.Mi_model;
                    //dgvItem["col_itmSerial", dgvItem.Rows.Count - 1].Value = itm.Sad_job_no;//serial 


                    gvInvoiceItems oNewitem = new gvInvoiceItems();
                    oNewitem.col_itmItem = itm.Sad_itm_cd;
                    oNewitem.colpb = itm.Sad_pbook;
                    oNewitem.colpblvl = itm.Sad_pb_lvl;
                    oNewitem.col_itmStatus = itm.Sad_itm_stus;
                    oNewitem.col_itmStatus_Desc = getItemStatusDesc(itm.Sad_itm_stus);
                    oNewitem.colQty = itm.Sad_qty;
                    oNewitem.colRate = Convert.ToDecimal(itm.Sad_unit_rt.ToString());
                    oNewitem.colamt = Convert.ToDecimal(itm.Sad_unit_amt.ToString());
                    oNewitem.colTax = Convert.ToDecimal(itm.Sad_itm_tax_amt.ToString());
                    lblSalesType.Text = cmbInvType.Text;
                    oNewitem.col_itmDesc = _item.Mi_shortdesc;
                    oNewitem.col_itmModel = _item.Mi_model;
                    oNewitem.col_itmSerial = itm.Sad_job_no;//serial 
                    oShowItems.Add(oNewitem);
                }

                dgvItem.DataSource = oShowItems;
                dgvItem.DataBind();

                decimal TotRec = 0;

                Decimal _recPer = 0;
                decimal _shouldpay = 0;
                if (cmbInvType.Text == "HS")
                {
                    _shouldpay = Convert.ToDecimal("0.00");

                    Decimal _Amt = 0;
                    _Amt = Convert.ToDecimal(_shouldpay.ToString("N2"));
                    ucPayModes1.TotalAmount = _Amt;
                    ucPayModes1.Amount.Text = Convert.ToString(ucPayModes1.TotalAmount - Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text));
                    ucPayModes1.Date = Convert.ToDateTime(dtpRecDate.Text).Date;

                    ucPayModes1.LoadData();
                    txtTotal.Text = Convert.ToDecimal(ucPayModes1.TotalAmount).ToString();
                }
                else
                {
                    if (txtRecType.Text == "ADVAN")
                    {
                        List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("RECTPER", "COM", Session["UserCompanyCode"].ToString());
                        if (para.Count > 0)
                        {
                            _recPer = para[0].Hsy_val;
                        }
                    }

                    TotRec = 0;
                    foreach (InvoiceItem itm in _invoiceItemList)
                    {
                        TotRec = TotRec + itm.Sad_tot_amt;
                    }

                    if (TotRec > 0 && _recPer > 0)
                    {
                        _shouldpay = TotRec * (_recPer / 100);
                    }

                    ucPayModes1.TotalAmount = 0;
                    ucPayModes1.Amount.Text = "0";
                    Decimal _Amt = 0;
                    _Amt = Convert.ToDecimal(_shouldpay.ToString("N2"));
                    ucPayModes1.TotalAmount = _Amt;
                    ucPayModes1.Amount.Text = Convert.ToString(ucPayModes1.TotalAmount - Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text));
                    ucPayModes1.Date = Convert.ToDateTime(dtpRecDate.Text).Date;

                    ucPayModes1.LoadData();
                    txtTotal.Text = Convert.ToDecimal(ucPayModes1.TotalAmount).ToString();

                }
                if (dgvItem.Rows.Count > 0)
                {
                    //dgvItem["colpay", dgvItem.Rows.Count - 1].Value = _shouldpay.ToString("N2");
                }

                //pnlAdv.Visible = false;

                _invoiceItemList = new List<InvoiceItem>();
                gvInvoiceItem.DataSource = new List<InvoiceItem>();
                gvInvoiceItem.DataSource = _invoiceItemList;
                gvInvoiceItem.DataBind();

                //ClearHP();
                txtItem.Text = "";
                TxtAdvItem.Text = "";
            }
        }

        private void SaveReceiptHeader()
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;
            decimal _valPd = 0;
            ReptPickHeader _SerHeader = new ReptPickHeader();
            List<ReptPickSerials> _tempSerialSave = new List<ReptPickSerials>();
            List<VehicalRegistration> _tempRegSave = new List<VehicalRegistration>();
            List<VehicleInsuarance> _tempInsSave = new List<VehicleInsuarance>();

            if (txtRecType.Text == "ADVAN")
            {
                List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADREFMAXDT", "COM", Session["UserCompanyCode"].ToString());
                if (para.Count > 0)
                {
                    _valPd = para[0].Hsy_val;
                }
            }
            else if (txtRecType.Text == "ADINS")
            {
                List<Hpr_SysParameter> para = CHNLSVC.Sales.GetAll_hpr_Para("ADINREMXDT", "COM", Session["UserCompanyCode"].ToString());
                if (para.Count > 0)
                {
                    _valPd = para[0].Hsy_val;
                }
            }


            RecieptHeader _ReceiptHeader = new RecieptHeader();
            _ReceiptHeader.Sar_seq_no = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, Session["UserCompanyCode"].ToString());
            _ReceiptHeader.Sar_com_cd = Session["UserCompanyCode"].ToString();
            _ReceiptHeader.Sar_receipt_type = txtRecType.Text.Trim();
            _ReceiptHeader.Sar_receipt_no = _ReceiptHeader.Sar_seq_no.ToString();// txtRecNo.Text.Trim();
            if (txtRecType.Text == "ADVAN")
            {
                _ReceiptHeader.Sar_prefix = comboBoxPrefix.SelectedValue.ToString();
            }
            else
            {
                _ReceiptHeader.Sar_prefix = txtDivision.Text.Trim();
            }
            if (string.IsNullOrEmpty(txtManual.Text))
            {
                txtManual.Text = "0";
            }
            else
            {
                _ReceiptHeader.Sar_manual_ref_no = txtManual.Text.Trim();
            }

            _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(dtpRecDate.Text).Date;
            _ReceiptHeader.Sar_direct = true;
            _ReceiptHeader.Sar_acc_no = "";
            if (chkOth.Checked == true)
            {
                _ReceiptHeader.Sar_is_oth_shop = true;
                _ReceiptHeader.Sar_oth_sr = "All";
            }

            else
            {
                _ReceiptHeader.Sar_is_oth_shop = false;
                _ReceiptHeader.Sar_oth_sr = "";
            }
            //Added by dulaj 2018/Aug/24
            if (Session["ExcelMultiplePc"] != null)
            {
                int chkPc = Convert.ToInt32(Session["ExcelMultiplePc"].ToString());
                if (chkPc == 1)
                {
                    _ReceiptHeader.Sar_is_oth_shop = true;
                    _ReceiptHeader.Sar_oth_sr = "All";
                }
                //Session["ExcelMultiplePc"] = 0;
            }
            _ReceiptHeader.Sar_profit_center_cd = Session["UserDefProf"].ToString();
            _ReceiptHeader.Sar_debtor_cd = txtCusCode.Text.Trim();
            _ReceiptHeader.Sar_debtor_name = txtCusName.Text.Trim();
            _ReceiptHeader.Sar_debtor_add_1 = txtCusAdd1.Text.Trim();
            _ReceiptHeader.Sar_debtor_add_2 = txtCusAdd2.Text.Trim();
            _ReceiptHeader.Sar_tel_no = "";
            _ReceiptHeader.Sar_mob_no = txtMobile.Text.Trim();
            _ReceiptHeader.Sar_nic_no = txtNIC.Text.Trim();
            _ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text);
            _ReceiptHeader.Sar_comm_amt = 0;
            _ReceiptHeader.Sar_is_mgr_iss = false;
            _ReceiptHeader.Sar_esd_rate = 0;
            _ReceiptHeader.Sar_wht_rate = 0;
            _ReceiptHeader.Sar_epf_rate = 0;
            _ReceiptHeader.Sar_currency_cd = "LKR";
            _ReceiptHeader.Sar_uploaded_to_finance = false;
            _ReceiptHeader.Sar_act = true;
            _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
            _ReceiptHeader.Sar_direct_deposit_branch = "";
            _ReceiptHeader.Sar_remarks = txtRemarks.Text.Trim();
            _ReceiptHeader.Sar_is_used = false;
            if (txtRecType.Text == "ADVAN")
            {
                _ReceiptHeader.Sar_ref_doc = txtDivision.Text.Trim();
            }
            else if (txtRecType.Text == "DISP")
            {
                _ReceiptHeader.Sar_ref_doc = txtDisposalJob.Text.Trim();
            }
            else
            {
                _ReceiptHeader.Sar_ref_doc = "";
            }
            _ReceiptHeader.Sar_ser_job_no = "";
            _ReceiptHeader.Sar_used_amt = 0;

            if ((txtRecType.Text == "DEBT" || txtRecType.Text == "SRTN" || txtRecType.Text == "SVAT" || txtRecType.Text == "DAJ") && dgvDebtInvoiceList.Rows.Count == 0)
            {
                _ReceiptHeader.Sar_used_amt = -1;
            }

            _ReceiptHeader.Sar_create_by = Session["UserID"].ToString();
            _ReceiptHeader.Sar_mod_by = Session["UserID"].ToString();
            _ReceiptHeader.Sar_session_id = Session["SessionID"].ToString();
            _ReceiptHeader.Sar_anal_1 = cmbDistrict.Text;
            _ReceiptHeader.Sar_anal_2 = txtProvince.Text.Trim();

            if (radioButtonManual.Checked == true)
            {
                _ReceiptHeader.Sar_anal_3 = "MANUAL";
                _ReceiptHeader.Sar_anal_8 = 1;
            }
            else
            {
                _ReceiptHeader.Sar_anal_3 = "SYSTEM";
                _ReceiptHeader.Sar_anal_8 = 0;
            }

            _ReceiptHeader.Sar_anal_4 = txtSalesEx.Text;
            _ReceiptHeader.Sar_anal_5 = 0;
            _ReceiptHeader.Sar_anal_6 = 0;
            _ReceiptHeader.Sar_anal_7 = 0;
            _ReceiptHeader.Sar_anal_9 = 0;
            _ReceiptHeader.SAR_VALID_TO = _ReceiptHeader.Sar_receipt_date.AddDays(Convert.ToDouble(_valPd));
            //_ReceiptHeader.Sar_scheme = lblSchme.Text;
            _ReceiptHeader.Sar_inv_type = lblSalesType.Text;

            List<RecieptItem> _ReceiptDetailsSave = new List<RecieptItem>();
            Int32 _line = 0;
            foreach (RecieptItem line in ucPayModes1.RecieptItemList)
            {
                line.Sard_seq_no = _ReceiptHeader.Sar_seq_no;
                _line = _line + 1;
                line.Sard_line_no = _line;
                _ReceiptDetailsSave.Add(line);
            }

            MasterAutoNumber masterAuto = new MasterAutoNumber();
            masterAuto.Aut_cate_cd = Session["UserDefProf"].ToString();
            masterAuto.Aut_cate_tp = "PC";
            masterAuto.Aut_direction = null;
            masterAuto.Aut_modify_dt = null;
            masterAuto.Aut_moduleid = "RECEIPT";
            masterAuto.Aut_number = 5;//what is Aut_number
            masterAuto.Aut_start_char = txtDivision.Text.Trim();
            masterAuto.Aut_year = null;

            DataTable _pcInfo = new DataTable();
            _pcInfo = CHNLSVC.Sales.GetProfitCenterTable(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString());


            MasterAutoNumber masterAutoRecTp = new MasterAutoNumber();
            masterAutoRecTp.Aut_cate_cd = Session["UserDefProf"].ToString();
            masterAutoRecTp.Aut_cate_tp = "PC";
            masterAutoRecTp.Aut_direction = null;
            masterAutoRecTp.Aut_modify_dt = null;

            if (_pcInfo.Rows[0]["mpc_ope_cd"].ToString() == "INV_LRP" && Session["UserCompanyCode"].ToString() == "LRP")
            {
                masterAutoRecTp.Aut_moduleid = "REC_LRP";
            }
            else
            {
                masterAutoRecTp.Aut_moduleid = "RECEIPT";
            }
            masterAutoRecTp.Aut_number = 5;//what is Aut_number
            masterAutoRecTp.Aut_start_char = txtRecType.Text.Trim();
            masterAutoRecTp.Aut_year = null;

            if (dgvItem.Rows.Count > 0)
            {
                _SerHeader.Tuh_usrseq_no = CHNLSVC.Inventory.GetSerialID();  //CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "RECEIPT", 0, Session["UserCompanyCode"].ToString());
                _SerHeader.Tuh_usr_id = Session["UserID"].ToString();
                _SerHeader.Tuh_usr_com = Session["UserCompanyCode"].ToString();
                _SerHeader.Tuh_session_id = Session["SessionID"].ToString();
                _SerHeader.Tuh_cre_dt = Convert.ToDateTime(dtpRecDate.Text).Date;
                if (_invType == "HS")
                {
                    _SerHeader.Tuh_doc_tp = "RECEIPT";
                }
                else
                {
                    _SerHeader.Tuh_doc_tp = "DO";
                }
                _SerHeader.Tuh_direct = false;
                if (_isRes == false)
                {
                    _SerHeader.Tuh_ischek_itmstus = false;
                }
                else
                {
                    _SerHeader.Tuh_ischek_itmstus = true;
                }
                _SerHeader.Tuh_ischek_simitm = true;
                _SerHeader.Tuh_ischek_reqqty = true;


                if (txtRecType.Text == "VHREG" || txtRecType.Text == "VHINS")
                {
                    _SerHeader.Tuh_doc_no = _invNo;
                }
                else
                {
                    _SerHeader.Tuh_doc_no = "na";
                }


                foreach (ReptPickSerials line in _ResList)
                {
                    line.Tus_usrseq_no = _SerHeader.Tuh_usrseq_no;
                    line.Tus_cre_by = Session["UserID"].ToString();
                    _tempSerialSave.Add(line);
                }
            }

            if (txtRecType.Text == "VHREG")
            {
                foreach (VehicalRegistration _reg in _regList)
                {
                    Int32 _vehSeq = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "VHREG", 1, Session["UserCompanyCode"].ToString());
                    _reg.P_seq = _vehSeq;
                    _tempRegSave.Add(_reg);
                }
            }

            if (txtRecType.Text == "VHINS")
            {
                foreach (VehicleInsuarance _ins in _insList)
                {
                    Int32 _insSeq = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "VHINS", 1, Session["UserCompanyCode"].ToString());
                    _ins.Svit_seq = _insSeq;
                    _ins.Svit_rec_tp = txtRecType.Text.Trim();
                    _tempInsSave.Add(_ins);
                }
            }

            if (txtRecType.Text == "ADINS")
            {
                foreach (VehicleInsuarance _ins in _insList)
                {
                    Int32 _insSeq = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(Session["UserID"].ToString(), "VHINS", 1, Session["UserCompanyCode"].ToString());
                    _ins.Svit_seq = _insSeq;
                    _ins.Svit_rec_tp = txtRecType.Text.Trim();
                    _tempInsSave.Add(_ins);
                }
            }

            string QTNum;

            List<RecieptHeader> otest = new List<RecieptHeader>();
            otest.Add(_ReceiptHeader);
            DataTable dt = GlobalMethod.ToDataTable(_ReceiptDetailsSave);
            DataTable d2t = GlobalMethod.ToDataTable(otest);

            btnSave.Enabled = false;
            row_aff = (Int32)CHNLSVC.Sales.SaveNewReceipt(_ReceiptHeader, _ReceiptDetailsSave, masterAuto, _SerHeader, _tempSerialSave, _tempRegSave, _tempInsSave, _sheduleDetails, null, masterAutoRecTp, _gvDetails, out QTNum);
            //Added by dulaj 2018/Sep/26
            if (Session["ExcelMultiplePc"] != null)
            {
                //Session["ExcelMultiplePc"] = 0;
            }
            if (radioButtonManual.Checked == true)
            {
                if (Session["UserDefLoca"].ToString() != Session["UserDefProf"].ToString())
                {
                    CHNLSVC.Inventory.UpdateManualDocNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "MDOC_AVREC", Convert.ToInt32(txtManual.Text), QTNum);
                }
            }

            if (radioButtonSystem.Checked == true)
            {
                if (Session["UserDefLoca"].ToString() != Session["UserDefProf"].ToString())
                {
                    CHNLSVC.Inventory.UpdateManualDocNo(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), "SDOC_AVREC", Convert.ToInt32(txtManual.Text), QTNum);
                }
            }

            //dilshan on 11-09-2018*********
            if (txtRecType.Text == "DEBT")
            {
                if (!string.IsNullOrEmpty(QTNum))
                {
                    CHNLSVC.MsgPortal.GenaratePaymentSMS(Session["UserCompanyCode"].ToString(), QTNum, txtCusCode.Text, txtInvoice.Text, txtBalance.Text, Session["UserDefLoca"].ToString());
                }
            }
            //******************************

            if (row_aff == 1)
            {
                if (radioButtonManual.Checked == true)
                {
                    string msg = "Successfully created.Receipt No: " + QTNum;
                    DisplayMessage(msg, 3);
                    Clear_Data();
                    btnSave.Enabled = true;
                    _outstandingList = new List<InvOutstanding>();
                    //Immediate Print after Save  -- Lakshika
                    Session["GlbReportType"] = "";
                    Session["documntNo"] = QTNum;//txtRecNo.Text;
                    Session["GlbReportName"] = "ReceiptPrints_n.rpt";
                    BaseCls.GlbReportHeading = "Receipt Print Report";

                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsSales obj = new clsSales();
                    obj.Receipt_print_n(Session["documntNo"].ToString());
                    PrintPDF(targetFileName, obj.recreport1_n);

                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);


                    // return;
                }
                else
                {
                    string msg = "Successfully created.Receipt No: " + QTNum;
                    DisplayMessage(msg, 3);
                    Clear_Data();
                    btnSave.Enabled = true;

                    //Immediate Print after Save -- Lakshika
                    Session["GlbReportType"] = "";
                    Session["documntNo"] = QTNum;//txtRecNo.Text;
                    Session["GlbReportName"] = "ReceiptPrints_n.rpt";
                    BaseCls.GlbReportHeading = "Receipt Print Report";

                    string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                    string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                    clsSales obj = new clsSales();
                    obj.Receipt_print_n(Session["documntNo"].ToString());
                    PrintPDF(targetFileName, obj.recreport1_n);

                    string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                    // return;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(QTNum))
                {
                    DisplayMessage(QTNum, 5);
                    btnSave.Enabled = true;
                    setButtionEnable(true, btnSave, "confSave");
                    return;

                }
                else
                {
                    DisplayMessage("Creation Fail.");
                    btnSave.Enabled = true;
                }
            }
        }

        private void setButtionEnable(bool isEnable, LinkButton btn, string confirmation)
        {
            if (isEnable)
            {
                btn.OnClientClick = confirmation + "();";
                btn.CssClass = "buttonUndocolor";
                btn.Enabled = true;


                ////Immediate Print after Save
                //Session["GlbReportType"] = "";
                //Session["documntNo"] = txtRecNo.Text;
                //Session["GlbReportName"] = "ReceiptPrints_n.rpt";
                //BaseCls.GlbReportHeading = "Receipt Print Report";

                //string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                //string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                //clsSales obj = new clsSales();
                //obj.Receipt_print_n(Session["documntNo"].ToString());
                //PrintPDF(targetFileName, obj.recreport1_n);

                //string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
            }
            else
            {
                btn.Enabled = false;
                btn.OnClientClick = "";
                btn.CssClass = "buttoncolor";
            }
        }

        protected void btnPayment_Click(object sender, EventArgs e)
        {
            btnPaymentSub_Click(null, null);
        }

        protected void btnPaymentSub_Click(object sender, EventArgs e)
        {
            try
            {
                pnlItemAlloc.Visible = false;
                decimal _Amt = 0;
                string _Htype = "";
                string _Hvalue = "";
                Int32 I = 0;

                if (string.IsNullOrEmpty(txtPayment.Text))
                {
                    DisplayMessage("Please enter amount which customer is going to pay.");
                    txtPayment.Focus();
                    return;
                }

                if (!IsNumeric(txtPayment.Text))
                {
                    DisplayMessage("Payment amount should be numeric.");
                    txtPayment.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    DisplayMessage("Please select payment customer.");
                    txtCusCode.Focus();
                    return;
                }

                if (txtRecType.Text == "ADVAN")
                {
                    if (ucPayModes1.MainGrid.Rows.Count > 0)
                    {
                        DisplayMessage("Payments are already added. Now you cannot add more details.");
                        return;
                    }

                    foreach (ReptPickSerials _list in _ResList)
                    {
                    }
                    decimal _qtyTotal = _ResList.Where(x => x.Tus_itm_cd == txtItem.Text.ToUpper()).Count();
                    // _qtyTotal =_qtyTotal+1;

                    txtAllocQty.Text = (txtAllocQty.Text.Trim() == "") ? "0.00" : txtAllocQty.Text;

                    if (Convert.ToDecimal(txtAllocQty.Text) > 0)
                    {
                        if (Convert.ToDecimal(txtFreeQty.Text) < _qtyTotal)
                        {
                            DisplayMessage("Allocated Qty exceeded. Now you cannot add more items.");
                            return;
                        }
                    }
                }

                if (txtRecType.Text == "DEBT" || txtRecType.Text == "SRTN" || txtRecType.Text == "SVAT" || txtRecType.Text == "DAJ")
                {
                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16036))
                    {
                        if (Convert.ToDecimal(txtPayment.Text) > Convert.ToDecimal(txtBalance.Text))//excel total amt
                        {
                            DisplayMessage("Payment cannot exceed outstanding amount.");
                            txtPayment.Focus();
                            return;
                        }

                        if (ucPayModes1.InvoiceNo == txtInvoice.Text.Trim())
                        {
                            DisplayMessage("Already add this invoice.");
                            txtPayment.Text = "";
                            txtPayment.Focus();
                            return;
                        }
                    }

                    if (Convert.ToDecimal(txtPayment.Text) <= 0)
                    {
                        DisplayMessage("Settle amount cannot be zero.");
                        txtPayment.Text = "";
                        txtPayment.Focus();
                        return;
                    }

                    if (!CHNLSVC.Security.Is_OptionPerimitted(Session["UserCompanyCode"].ToString(), Session["UserID"].ToString(), 16036))
                    {
                        foreach (RecieptItem line in ucPayModes1.RecieptItemList)
                        {
                            if (line.Sard_inv_no == txtInvoice.Text.Trim())
                            {
                                DisplayMessage("Already add this invoice.");
                                txtPayment.Text = "";
                                txtPayment.Focus();
                                return;
                            }
                        }
                    }
                }
                else if (txtRecType.Text == "VHREG" || txtRecType.Text == "VHINS")
                {
                    if (Convert.ToDecimal(txtPayment.Text) != Convert.ToDecimal(txtBalance.Text))
                    {
                        DisplayMessage("Payment amount not match with define amount.");
                        txtPayment.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtProvince.Text))
                    {
                        DisplayMessage("Please select district & province.");
                        cmbDistrict.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtengine.Text))
                    {
                        DisplayMessage("Please select engine #.");
                        txtengine.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtChasis.Text))
                    {
                        DisplayMessage("Please select chassis #.");
                        txtChasis.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtItem.Text))
                    {
                        DisplayMessage("Please select item.");
                        txtItem.Focus();
                        return;
                    }

                    if (txtRecType.Text == "VHINS")
                    {
                        if (string.IsNullOrEmpty(txtInsCom.Text))
                        {
                            DisplayMessage("Please select insurance company.");
                            txtInsCom.Focus();
                            return;
                        }

                        if (string.IsNullOrEmpty(txtInsPol.Text))
                        {
                            DisplayMessage("Please select insurance policy.");
                            txtInsPol.Focus();
                            return;
                        }
                    }

                    if (Con_IsThisSelectProvinceAndDistrictIsCorrect.Value == "Y")
                    {


                    }
                    else if (Con_IsThisSelectProvinceAndDistrictIsCorrect.Value == "N")
                    {
                        cmbDistrict.Enabled = true;
                        cmbDistrict.Focus();
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "IsThisSelectProvinceAndDistrictIsCorrect('');", true);
                        return;
                    }

                    //_??if (MessageBox.Show("Is this select province and district is correct ?", "Receipt Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    //_??{
                    //_??    cmbDistrict.Enabled = true;
                    //_??    cmbDistrict.Focus();
                    //_??    return;
                    //_??}

                    // cmbDistrict.Enabled = false;
                }
                else if (txtRecType.Text == "ADINS")
                {
                    if (Convert.ToDecimal(txtPayment.Text) != Convert.ToDecimal(txtBalance.Text))
                    {
                        DisplayMessage("Payment amount not match with define amount.");
                        txtPayment.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtItem.Text))
                    {
                        DisplayMessage("Please select item.");
                        txtItem.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtengine.Text))
                    {
                        DisplayMessage("Please select engine #.");
                        txtengine.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtInsCom.Text))
                    {
                        DisplayMessage("Please select insurance company.");
                        txtInsCom.Focus();
                        return;
                    }

                    if (string.IsNullOrEmpty(txtInsPol.Text))
                    {
                        DisplayMessage("Please select insurance policy.");
                        txtInsPol.Focus();
                        return;
                    }
                }
                else
                {
                    if (Convert.ToDecimal(txtPayment.Text) <= 0)
                    {
                        DisplayMessage("Settle amount cannot be zero.");
                        txtPayment.Text = "";
                        txtPayment.Focus();
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(ucPayModes1.InvoiceNo))
                {
                    if (ucPayModes1.InvoiceNo != txtInvoice.Text.Trim())
                    {
                        if (ucPayModes1.Balance > 0)
                        {
                            DisplayMessage("Not allowed to go another payment with out complete selected payment.");
                            return;
                        }
                    }
                }

                if (txtRecType.Text == "VHREG")
                {

                    foreach (VehicalRegistration temp in _regList)
                    {
                        if (temp.P_srvt_itm_cd == txtItem.Text.ToUpper().Trim() && temp.P_svrt_engine == txtengine.Text.Trim())
                        {
                            DisplayMessage("Already exist.");
                            return;
                        }
                    }

                    foreach (ReptPickSerials _serial in _ResList)
                    {
                        if (_serial.Tus_itm_cd == txtItem.Text.Trim() && _serial.Tus_ser_1 == txtengine.Text.Trim())
                        {
                            DisplayMessage("Selected serial is already exist.");
                            txtengine.Focus();
                            return;
                        }
                    }

                    _regList.Add(AssingRegDetails());

                    dgvReg.DataSource = new int[] { };
                    dgvReg.DataBind();

                    List<gvRegItems> oRegItems = new List<gvRegItems>();

                    foreach (VehicalRegistration reg in _regList)
                    {
                        gvRegItems oNewItem = new gvRegItems();
                        oNewItem.col_regInv = reg.P_svrt_inv_no;
                        oNewItem.col_regCus = reg.P_svrt_full_name;
                        oNewItem.col_regItem = reg.P_srvt_itm_cd;
                        oNewItem.col_regModel = reg.P_svrt_model;
                        oNewItem.col_regBrand = reg.P_svrt_brd;
                        oNewItem.col_regDis = reg.P_svrt_district;
                        oNewItem.col_regPro = reg.P_svrt_province;
                        oNewItem.col_regEngine = reg.P_svrt_engine;
                        oNewItem.col_regChasis = reg.P_svrt_chassis;
                        oNewItem.col_regVal = reg.P_svrt_reg_val;
                        oRegItems.Add(oNewItem);
                    }

                    dgvReg.DataSource = oRegItems;
                    dgvReg.DataBind();


                    //_??tbOth.SelectedTab = tpReg;
                    //dgvReg.DataSource = _regList;

                }
                else if (txtRecType.Text == "ADINS")
                {
                    foreach (VehicleInsuarance temp in _insList)
                    {
                        if (temp.Svit_itm_cd == txtItem.Text.ToUpper().Trim())
                        {
                            DisplayMessage("Selected item already exsist.");
                            return;
                        }
                    }

                    _insList.Add(AssingInsDetails());

                    dgvIns.DataSource = new int[] { };
                    dgvIns.DataBind();

                    List<dgvInsItems> oItemsDgvIns = new List<dgvInsItems>();

                    foreach (VehicleInsuarance ins in _insList)
                    {
                        dgvInsItems oNewItem = new dgvInsItems();
                        oNewItem.col_insInv = ins.Svit_inv_no;
                        oNewItem.col_insCus = ins.Svit_full_name;
                        oNewItem.col_insItem = ins.Svit_itm_cd;
                        oNewItem.col_insModel = ins.Svit_model;
                        oNewItem.col_insDistrict = ins.Svit_district;
                        oNewItem.col_insPro = ins.Svit_province;
                        oNewItem.col_insCom = ins.Svit_ins_com;
                        oNewItem.col_insPol = ins.Svit_ins_polc;
                        oNewItem.col_insEngine = ins.Svit_engine;
                        oNewItem.col_insChasis = ins.Svit_chassis;
                        oNewItem.col_insVal = ins.Svit_ins_val;
                        oItemsDgvIns.Add(oNewItem);
                    }

                    dgvIns.DataSource = oItemsDgvIns;
                    dgvIns.DataBind();

                    //_??tbOth.SelectedTab = tpInsu;
                }
                else if (txtRecType.Text == "VHINS")
                {
                    Boolean _isInsuFound = false;

                    InvoiceItem _invItm = new InvoiceItem();

                    //Get invoice details
                    string _pBook = "";
                    string _pLvl = "";
                    string _promoCd = "";
                    decimal _itmVal = 0;
                    _isInsuFound = false;

                    _invItm = CHNLSVC.Sales.GetInvDetByLine(txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), _invLine);
                    if (_invItm.Sad_itm_cd == null)
                    {
                        DisplayMessage("Cannot find invoice item details.");
                        txtItem.Text = "";
                        txtItem.Focus();
                        return;
                    }
                    else
                    {
                        _pBook = _invItm.Sad_pbook;
                        _pLvl = _invItm.Sad_pb_lvl;
                        _promoCd = _invItm.Sad_promo_cd;
                        _itmVal = _invItm.Sad_tot_amt / _invItm.Sad_qty;
                    }

                    foreach (VehicleInsuarance temp in _insList)
                    {
                        if (temp.Svit_itm_cd == txtItem.Text.ToUpper().Trim() && temp.Svit_engine == txtengine.Text.Trim())
                        {
                            DisplayMessage("Selected engine # already exist.");
                            return;
                        }
                    }

                    foreach (ReptPickSerials _serial in _ResList)
                    {
                        if (_serial.Tus_itm_cd == txtItem.Text.ToUpper().Trim() && _serial.Tus_ser_1 == txtengine.Text.Trim())
                        {
                            DisplayMessage("Selected serial is already exist.");
                            txtengine.Focus();
                            return;
                        }
                    }

                    _insList.Add(AssingInsDetails());

                    dgvIns.DataSource = new int[] { };
                    dgvIns.DataBind();

                    List<dgvInsItems> oItemsDgvIns = new List<dgvInsItems>();

                    foreach (VehicleInsuarance ins in _insList)
                    {
                        dgvInsItems oNewItem = new dgvInsItems();
                        oNewItem.col_insInv = ins.Svit_inv_no;
                        oNewItem.col_insCus = ins.Svit_full_name;
                        oNewItem.col_insItem = ins.Svit_itm_cd;
                        oNewItem.col_insModel = ins.Svit_model;
                        oNewItem.col_insDistrict = ins.Svit_district;
                        oNewItem.col_insPro = ins.Svit_province;
                        oNewItem.col_insCom = ins.Svit_ins_com;
                        oNewItem.col_insPol = ins.Svit_ins_polc;
                        oNewItem.col_insEngine = ins.Svit_engine;
                        oNewItem.col_insChasis = ins.Svit_chassis;
                        oNewItem.col_insVal = ins.Svit_ins_val;
                        oItemsDgvIns.Add(oNewItem);
                    }

                    dgvIns.DataSource = oItemsDgvIns;
                    dgvIns.DataBind();

                    //_??tbOth.SelectedTab = tpInsu;

                    //calculate insurarance future rental
                    if (_accNo != null && _accNo != "")
                    {
                        Int32 _MainInsTerm = 0;
                        Int32 _SubInsTerm = 0;
                        _HpAccount = new HpAccount();
                        _HpAccount = CHNLSVC.Sales.GetHP_Account_onAccNo(_accNo);
                        List<MasterSalesPriorityHierarchy> _Saleshir = CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());

                        if (_Saleshir.Count > 0)
                        {
                            foreach (MasterSalesPriorityHierarchy _one in _Saleshir)
                            {
                                _Htype = _one.Mpi_cd;
                                _Hvalue = _one.Mpi_val;

                                _SchemeDetails = CHNLSVC.Sales.getSchemeDetails(_Htype, _Hvalue, 1, _HpAccount.Hpa_sch_cd);

                                if (_SchemeDetails.Hsd_cd != null)
                                {
                                    if (_SchemeDetails.Hsd_veh_insu_term != null)
                                    {

                                        _insuTerm = _SchemeDetails.Hsd_veh_insu_term;

                                        if (_SchemeDetails.Hsd_veh_insu_col_term != null)
                                        {
                                            _colTerm = _SchemeDetails.Hsd_veh_insu_col_term;
                                        }
                                        else
                                        {
                                            _colTerm = _insuTerm;
                                        }


                                        _MainInsTerm = _insuTerm / 12;

                                        if (_MainInsTerm > 0)
                                        {

                                            List<MasterSalesPriorityHierarchy> _Saleshir1 = CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                                            string _Subchannel = "";
                                            string _typeSubChnl = "SCHNL";

                                            string _Mainchannel = "";
                                            string _typeMainChanl = "CHNL";

                                            string _Pctype = "PC";
                                            string _typePc = Session["UserDefProf"].ToString();

                                            if (_Saleshir1.Count > 0)
                                            {
                                                _Subchannel = (from _lst in _Saleshir1
                                                               where _lst.Mpi_cd == "SCHNL"
                                                               select _lst.Mpi_val).ToList<string>()[0];


                                                _Mainchannel = (from _lst in _Saleshir1
                                                                where _lst.Mpi_cd == "CHNL"
                                                                select _lst.Mpi_val).ToList<string>()[0];

                                                MasterVehicalInsuranceDefinitionNew _vehIns = new MasterVehicalInsuranceDefinitionNew();
                                                //_vehIns = CHNLSVC.Sales.GetVehInsDef(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), Convert.ToDateTime(dtpRecDate.Value).Date, Session["UserDefProf"].ToString(), txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), 12);
                                                Int32 _HpTerm = 12;

                                                if (!string.IsNullOrEmpty(_promoCd))
                                                {

                                                    //check serial + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _vehIns.Svid_val * _MainInsTerm;
                                                        _isInsuFound = true;
                                                        goto L56;
                                                    }
                                                    //check pc + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _vehIns.Svid_val * _MainInsTerm;
                                                        _isInsuFound = true;
                                                        goto L56;
                                                    }

                                                    //check sub Channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _vehIns.Svid_val * _MainInsTerm;
                                                        _isInsuFound = true;
                                                        goto L56;
                                                    }

                                                    //Check channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _vehIns.Svid_val * _MainInsTerm;
                                                        _isInsuFound = true;
                                                        goto L56;
                                                    }

                                                    //check serial
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _vehIns.Svid_val * _MainInsTerm;
                                                        _isInsuFound = true;
                                                        goto L56;
                                                    }
                                                    //check pc
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _vehIns.Svid_val * _MainInsTerm;
                                                        _isInsuFound = true;
                                                        goto L56;
                                                    }

                                                    //check sub Channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _vehIns.Svid_val * _MainInsTerm;
                                                        _isInsuFound = true;
                                                        goto L56;
                                                    }

                                                    //Check channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _vehIns.Svid_val * _MainInsTerm;
                                                        _isInsuFound = true;
                                                        goto L56;
                                                    }


                                                    //if (_vehIns.Ins_com_cd != null)
                                                    //{
                                                    //    _insuAmt = _vehIns.Value * _MainInsTerm;
                                                    //}
                                                    //else
                                                    //{
                                                    //    MessageBox.Show("Insuarance definition not found for term 12.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    //    return;
                                                    //}
                                                }
                                                else
                                                {
                                                    //check serial
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _vehIns.Svid_val * _MainInsTerm;
                                                        _isInsuFound = true;
                                                        goto L56;
                                                    }
                                                    //check pc
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _vehIns.Svid_val * _MainInsTerm;
                                                        _isInsuFound = true;
                                                        goto L56;
                                                    }

                                                    //check sub Channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _vehIns.Svid_val * _MainInsTerm;
                                                        _isInsuFound = true;
                                                        goto L56;
                                                    }

                                                    //Check channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _HpTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _vehIns.Svid_val * _MainInsTerm;
                                                        _isInsuFound = true;
                                                        goto L56;
                                                    }
                                                }

                                                if (_isInsuFound == false)
                                                {
                                                    DisplayMessage("Insurance definition not found for term 12.");
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                DisplayMessage("Hierarchy not define.");
                                                return;
                                            }
                                        }
                                    L56:

                                        _SubInsTerm = _insuTerm % 12;

                                        if (_SubInsTerm > 0)
                                        {
                                            _isInsuFound = false;

                                            if ((_SubInsTerm) <= 3)
                                            {
                                                _SubInsTerm = 3;
                                            }
                                            else if ((_SubInsTerm) <= 6)
                                            {
                                                _SubInsTerm = 6;
                                            }
                                            else if ((_SubInsTerm) <= 9)
                                            {
                                                _SubInsTerm = 9;
                                            }
                                            else
                                            {
                                                _SubInsTerm = 12;
                                            }

                                            List<MasterSalesPriorityHierarchy> _Saleshir2 = CHNLSVC.Sales.GetSalesPriorityHierarchy(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), CommonUIDefiniton.SalesPriorityHierarchyCategory.PC_PRIT_HIERARCHY.ToString(), CommonUIDefiniton.SalesPriorityHierarchyType.PC.ToString());
                                            string _Subchannel = "";
                                            string _typeSubChnl = "SCHNL";

                                            string _Mainchannel = "";
                                            string _typeMainChanl = "CHNL";

                                            string _Pctype = "PC";
                                            string _typePc = Session["UserDefProf"].ToString();

                                            if (_Saleshir2.Count > 0)
                                            {
                                                _Subchannel = (from _lst in _Saleshir2
                                                               where _lst.Mpi_cd == "SCHNL"
                                                               select _lst.Mpi_val).ToList<string>()[0];


                                                _Mainchannel = (from _lst in _Saleshir2
                                                                where _lst.Mpi_cd == "CHNL"
                                                                select _lst.Mpi_val).ToList<string>()[0];


                                                MasterVehicalInsuranceDefinitionNew _vehIns = new MasterVehicalInsuranceDefinitionNew();
                                                //_vehIns = CHNLSVC.Sales.GetVehInsDef(Session["UserCompanyCode"].ToString(), txtInvoice.Text.Trim(), txtItem.Text.ToUpper().Trim(), Convert.ToDateTime(dtpRecDate.Value).Date, Session["UserDefProf"].ToString(), txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), _SubInsTerm);

                                                if (!string.IsNullOrEmpty(_promoCd))
                                                {
                                                    //check serial + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //check pc + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //check serial + sub Channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //check sub Channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //Check serial + channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, txtengine.Text);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //Check channel + promo
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, _promoCd, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //check serial
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //check pc
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //check serial + sub Channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //check sub Channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //Check serial + channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //Check channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }


                                                    //if (_vehIns.Ins_com_cd != null)
                                                    //{
                                                    //    _insuAmt = _vehIns.Value * _MainInsTerm;
                                                    //}
                                                    //else
                                                    //{
                                                    //    MessageBox.Show("Insuarance definition not found for term 12.", "Receipt Entry", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    //    return;
                                                    //}
                                                }
                                                else
                                                {
                                                    //check serial
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //check pc
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _Pctype, _typePc, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //check serial + sub Channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //check sub Channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeSubChnl, _Subchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //Check serial + channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, txtengine.Text);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                    //Check channel
                                                    _vehIns = CHNLSVC.Sales.GetVehInsAmtNewMethod(Session["UserCompanyCode"].ToString(), _typeMainChanl, _Mainchannel, txtInsCom.Text.Trim(), txtInsPol.Text.Trim(), "HS", txtItem.Text.ToUpper().Trim(), _SubInsTerm, _pBook, _pLvl, Convert.ToDateTime(dtpRecDate.Text).Date, _itmVal - 10, _itmVal + 10, null, null);

                                                    if (_vehIns.Svid_itm != null)
                                                    {
                                                        _insuAmt = _insuAmt + _vehIns.Svid_val;
                                                        _isInsuFound = true;
                                                        goto L57;
                                                    }
                                                }

                                                if (_isInsuFound == false)
                                                {
                                                    DisplayMessage("Insurance definition not found for term." + _SubInsTerm);
                                                    return;
                                                }
                                            }
                                        }

                                    L57:

                                        for (int x = 1; x <= _colTerm; x++)
                                        {
                                            HpSheduleDetails _tempShedule = new HpSheduleDetails();
                                            _tempShedule.Hts_seq = 0;
                                            _tempShedule.Hts_acc_no = _accNo;
                                            _tempShedule.Hts_rnt_no = x;
                                            _tempShedule.Hts_due_dt = DateTime.Today.Date;
                                            _tempShedule.Hts_rnt_val = 0;
                                            _tempShedule.Hts_intr = 0; //double.Parse(number.ToString("####0.00"));
                                            _tempShedule.Hts_vat = 0;
                                            _tempShedule.Hts_ser = 0;
                                            _tempShedule.Hts_ins = 0;
                                            _tempShedule.Hts_sdt = 0;
                                            _tempShedule.Hts_cre_by = Session["UserID"].ToString();
                                            _tempShedule.Hts_cre_dt = DateTime.Today.Date;
                                            _tempShedule.Hts_mod_by = Session["UserID"].ToString();
                                            _tempShedule.Hts_mod_dt = DateTime.Today.Date;
                                            _tempShedule.Hts_upload = false;
                                            _tempShedule.Hts_veh_insu = _insuAmt / _colTerm;
                                            _tempShedule.Hts_tot_val = _insuAmt / _colTerm;
                                            _sheduleDetails.Add(_tempShedule);
                                        }

                                        goto L6;
                                    }
                                    else
                                    {

                                        goto L6;
                                    }

                                }
                            }
                        L6: I = 2;
                        }
                    }

                }

                if (!string.IsNullOrEmpty(txtItem.Text.ToUpper()))
                {

                    if ((txtRecType.Text == "ADVAN") || (txtRecType.Text == "VHREG") || (txtRecType.Text == "VHINS"))
                    {
                        ReptPickSerials _tempItem = new ReptPickSerials();
                        _tempItem = CHNLSVC.Inventory.GetAvailableSerIDInformation(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), txtItem.Text.ToUpper().Trim(), txtengine.Text.Trim(), string.Empty, string.Empty);

                        if (_tempItem.Tus_itm_cd != null)
                        {
                            MasterItem _itemList = new MasterItem();
                            _itemList = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().Trim());

                            _isRes = true;
                            _tempItem.Tus_itm_desc = _itemList.Mi_shortdesc;
                            _tempItem.Tus_itm_model = _itemList.Mi_model;
                            _tempItem.Tus_itm_brand = _itemList.Mi_brand;
                            _tempItem.Tus_base_doc_no = txtInvoice.Text.Trim();
                            _tempItem.Tus_base_itm_line = _invLine;
                            if (_invType == "HS")
                            {
                                _tempItem.Tus_isapp = 0;
                                _tempItem.Tus_iscovernote = 0;
                            }
                            else
                            {
                                _tempItem.Tus_isapp = 1;
                                _tempItem.Tus_iscovernote = 1;
                            }

                            //2012/12/24
                            //if no serial
                            _tempItem.Tus_com = Session["UserCompanyCode"].ToString();
                            _tempItem.Tus_loc = Session["UserDefLoca"].ToString();
                            if (_tempItem.Tus_itm_cd == "" || _tempItem.Tus_itm_cd == null)
                            {
                                _isRes = false;
                                _tempItem.Tus_itm_cd = txtItem.Text.ToUpper();
                            }

                            //END
                            if (_tempItem.Tus_itm_cd != "" || _tempItem.Tus_itm_cd != null)
                            {
                                _ResList.Add(_tempItem);
                            }

                            dgvItem.DataSource = new int[] { };
                            dgvItem.DataBind();

                            if (_ResList != null)
                            {
                                List<gvInvoiceItems> oitemsasd = new List<gvInvoiceItems>();
                                foreach (ReptPickSerials ser in _ResList)
                                {
                                    gvInvoiceItems oNewItem = new gvInvoiceItems();
                                    oNewItem.col_itmItem = ser.Tus_itm_cd;
                                    oNewItem.col_itmDesc = ser.Tus_itm_desc;
                                    oNewItem.col_itmModel = ser.Tus_itm_model;
                                    oNewItem.col_itmStatus = ser.Tus_itm_stus;
                                    oNewItem.col_itmStatus_Desc = getItemStatusDesc(oNewItem.col_itmStatus);
                                    oNewItem.col_itmSerial = ser.Tus_ser_1;
                                    oNewItem.col_itmOthSerial = ser.Tus_ser_2;
                                    oitemsasd.Add(oNewItem);
                                }

                                dgvItem.DataSource = oitemsasd;
                                dgvItem.DataBind();
                            }
                        }
                        else
                        {
                            if (txtRecType.Text == "ADVAN")
                            {
                                MasterItem _itemList = new MasterItem();
                                _itemList = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper().Trim());

                                _isRes = false;
                                _tempItem.Tus_itm_desc = _itemList.Mi_shortdesc;
                                _tempItem.Tus_itm_model = _itemList.Mi_model;
                                _tempItem.Tus_itm_brand = _itemList.Mi_brand;
                                _tempItem.Tus_base_doc_no = null;
                                _tempItem.Tus_base_itm_line = 0;
                                _tempItem.Tus_isapp = 1;
                                _tempItem.Tus_iscovernote = 1;



                                //2012/12/24
                                //if no serial
                                _tempItem.Tus_com = Session["UserCompanyCode"].ToString();
                                _tempItem.Tus_loc = Session["UserDefLoca"].ToString();
                                if (_tempItem.Tus_itm_cd == "" || _tempItem.Tus_itm_cd == null)
                                {
                                    _isRes = false;
                                    _tempItem.Tus_itm_cd = txtItem.Text.ToUpper();
                                }

                                //END
                                if (_tempItem.Tus_itm_cd != "" || _tempItem.Tus_itm_cd != null)
                                {
                                    _ResList.Add(_tempItem);
                                }

                                dgvItem.DataSource = new int[] { };
                                dgvItem.DataBind();

                                if (_ResList != null)
                                {
                                    List<gvInvoiceItems> oitemsasd = new List<gvInvoiceItems>();

                                    foreach (ReptPickSerials ser in _ResList)
                                    {
                                        gvInvoiceItems oNewim = new gvInvoiceItems();
                                        oNewim.col_itmItem = ser.Tus_itm_cd;
                                        oNewim.col_itmDesc = ser.Tus_itm_desc;
                                        oNewim.col_itmModel = ser.Tus_itm_model;
                                        oNewim.col_itmStatus = ser.Tus_itm_stus;
                                        oNewim.col_itmStatus_Desc = getItemStatusDesc(oNewim.col_itmStatus);
                                        oNewim.col_itmSerial = ser.Tus_ser_1;
                                        oNewim.col_itmOthSerial = ser.Tus_ser_2;
                                        oNewim.colQty = 1;
                                        oitemsasd.Add(oNewim);
                                    }

                                    dgvItem.DataSource = oitemsasd;
                                    dgvItem.DataBind();
                                }
                            }
                        }
                    }
                }

                if (ViewState["RecieptEntryDetails"] != null)
                {
                    if (Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text) != ucPayModes1.TotalAmount)
                    {
                        _listReceiptEntry = new List<ReceiptEntryExcel>();
                    }
                    else
                    {
                        _listReceiptEntry = ViewState["RecieptEntryDetails"] as List<ReceiptEntryExcel>;
                        ucPayModes1.RecieptEntryDetails = _listReceiptEntry;
                        ucPayModes1.TotalAmount = _listReceiptEntry.Sum(x => x.Amount);
                    }

                    Session["ReceiptType"] = txtRecType.Text;
                    decimal ucValue = ucPayModes1.TotalAmount - Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text);
                    ucPayModes1.Amount.Text = ucValue.ToString("N2");
                    ucPayModes1.Date = Convert.ToDateTime(dtpRecDate.Text).Date;
                    if (txtRecType.Text == "DEBT" || txtRecType.Text == "VHREG" || txtRecType.Text == "VHINS" || txtRecType.Text == "SRTN" || txtRecType.Text == "SVAT" || txtRecType.Text == "DAJ")
                    {
                        ucPayModes1.Customer_Code = txtCusCode.Text;
                        ucPayModes1.IsZeroAllow = true;
                    }
                    //if (Convert.ToDecimal(ucPayModes1.Balance) != Convert.ToDecimal(txtPayment.Text))
                    //{
                    //    DisplayMessage("Total excel amount and entered customer payment not tally...!!!");
                    //    return;
                    //}
                    if (ucPayModes1.TotalAmount < (Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text) + Convert.ToDecimal(txtPayment.Text)))
                    {
                        DisplayMessage("Total excel amount and entered customer payment not tally...!!!");
                        return;
                    }
                }
                else
                {
                    Session["ReceiptType"] = txtRecType.Text;
                    _Amt = Convert.ToDecimal(txtPayment.Text);
                    ucPayModes1.TotalAmount = ucPayModes1.TotalAmount + _Amt;
                    decimal ucValue = ucPayModes1.TotalAmount + _Amt - Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text);
                    ucPayModes1.Amount.Text = ucValue.ToString("N2");
                    ucPayModes1.Date = Convert.ToDateTime(dtpRecDate.Text).Date;
                    if (txtRecType.Text == "DEBT" || txtRecType.Text == "VHREG" || txtRecType.Text == "VHINS" || txtRecType.Text == "SRTN" || txtRecType.Text == "SVAT" || txtRecType.Text == "DAJ")
                    {
                        ucPayModes1.InvoiceNo = txtInvoice.Text.Trim();
                        ucPayModes1.Customer_Code = txtCusCode.Text;
                        ucPayModes1.IsZeroAllow = true;
                        _invNo = txtInvoice.Text.Trim();
                    }
                }

                ucPayModes1.LoadData();
                txtTotal.Text = Convert.ToDecimal(ucPayModes1.TotalAmount).ToString("N2");
                //txtRecType.Enabled = false;
                txtInvoice.Text = "";
                txtBalance.Text = "0.00";
                txtItem.Text = "";
                txtengine.Text = "";
                txtChasis.Text = "";
                // chkDel.Checked = false;
                txtInsCom.Text = "";
                txtInsPol.Text = "";
                txtPayment.Text = "0.00";
                lblExtraChg.Text = "0.00";
                // chkAnnual.Checked = false;

                //chkOth.Enabled = false;
                //txtOthSR.Enabled = false;
                // btnOthSR.Enabled = false;

                if (txtRecType.Text == "ADVAN")
                {
                    //_??tbOth.SelectedTab = tpItem;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        private VehicalRegistration AssingRegDetails()
        {
            VehicalRegistration _tempReg = new VehicalRegistration();
            MasterItem _itemList = new MasterItem();
            _itemList = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper());

            _tempReg.P_seq = 1;
            _tempReg.P_srvt_ref_no = "na";
            _tempReg.P_svrt_com = Session["UserCompanyCode"].ToString();
            _tempReg.P_svrt_pc = Session["UserDefProf"].ToString();
            _tempReg.P_svrt_dt = Convert.ToDateTime(dtpRecDate.Text).Date;
            _tempReg.P_svrt_inv_no = txtInvoice.Text.Trim();
            _tempReg.P_svrt_sales_tp = _invType;
            _tempReg.P_svrt_reg_val = Convert.ToDecimal(txtPayment.Text);
            _tempReg.P_svrt_claim_val = _regAmt;
            _tempReg.P_svrt_id_tp = "NIC";
            _tempReg.P_svrt_id_ref = txtNIC.Text.Trim();
            _tempReg.P_svrt_cust_cd = txtCusCode.Text.Trim();
            _tempReg.P_svrt_cust_title = "Mr.";
            _tempReg.P_svrt_last_name = txtCusName.Text.Trim();
            _tempReg.P_svrt_full_name = txtCusName.Text.Trim();
            _tempReg.P_svrt_initial = "";
            _tempReg.P_svrt_add01 = txtCusAdd1.Text.Trim();
            _tempReg.P_svrt_add02 = txtCusAdd2.Text.Trim();
            _tempReg.P_svrt_city = "";
            _tempReg.P_svrt_district = cmbDistrict.Text;
            _tempReg.P_svrt_province = txtProvince.Text.Trim();
            _tempReg.P_svrt_contact = txtMobile.Text.Trim();
            _tempReg.P_svrt_model = _itemList.Mi_model;
            _tempReg.P_svrt_brd = _itemList.Mi_brand;
            _tempReg.P_svrt_chassis = txtChasis.Text.Trim();
            _tempReg.P_svrt_engine = txtengine.Text.Trim();
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
            _tempReg.P_srvt_itm_cd = txtItem.Text.ToUpper().Trim();
            return _tempReg;

        }

        private VehicleInsuarance AssingInsDetails()
        {
            VehicleInsuarance _tempIns = new VehicleInsuarance();
            MasterItem _itemList = new MasterItem();
            _itemList = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtItem.Text.ToUpper());
            _tempIns.Svit_seq = 1;
            _tempIns.Svit_ref_no = "na";
            _tempIns.Svit_com = Session["UserCompanyCode"].ToString();
            _tempIns.Svit_pc = Session["UserDefProf"].ToString();
            _tempIns.Svit_dt = Convert.ToDateTime(dtpRecDate.Text).Date;
            _tempIns.Svit_inv_no = txtInvoice.Text.Trim();
            _tempIns.Svit_sales_tp = _invType;
            _tempIns.Svit_ins_com = txtInsCom.Text.Trim();
            _tempIns.Svit_ins_polc = txtInsPol.Text.Trim();
            _tempIns.Svit_ins_val = Convert.ToDecimal(txtPayment.Text);
            _tempIns.Svit_cust_cd = txtCusCode.Text.Trim();
            _tempIns.Svit_cust_title = "Mr.";
            _tempIns.Svit_last_name = txtCusName.Text.Trim();
            _tempIns.Svit_full_name = txtCusName.Text.Trim();
            _tempIns.Svit_initial = "";
            _tempIns.Svit_add01 = txtCusAdd1.Text.Trim();
            _tempIns.Svit_add02 = txtCusAdd2.Text.Trim();
            _tempIns.Svit_city = "";
            _tempIns.Svit_district = cmbDistrict.Text;
            _tempIns.Svit_province = txtProvince.Text.Trim();
            _tempIns.Svit_contact = txtMobile.Text.Trim();
            _tempIns.Svit_model = _itemList.Mi_model;
            _tempIns.Svit_brd = _itemList.Mi_brand;
            _tempIns.Svit_chassis = txtChasis.Text.Trim();
            _tempIns.Svit_engine = txtengine.Text.Trim();
            _tempIns.Svit_capacity = "0";
            _tempIns.Svit_cre_by = Session["UserID"].ToString();
            _tempIns.Svit_cre_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_mod_by = Session["UserID"].ToString();
            _tempIns.Svit_mod_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_cvnt_issue = 0;
            _tempIns.Svit_cvnt_no = "";
            _tempIns.Svit_cvnt_days = 0;
            _tempIns.Svit_cvnt_from_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_cvnt_to_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_cvnt_by = "";
            _tempIns.Svit_cvnt_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_ext_issue = false;
            _tempIns.Svit_ext_no = "";
            _tempIns.Svit_ext_days = 0;
            _tempIns.Svit_ext_from_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_ext_to_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_ext_by = "";
            _tempIns.Svit_ext_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_veh_reg_no = "";
            _tempIns.Svit_reg_by = "";
            _tempIns.Svit_reg_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_polc_stus = false;
            _tempIns.Svit_polc_no = "";
            _tempIns.Svit_eff_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_expi_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_net_prem = 0;
            _tempIns.Svit_srcc_prem = 0;
            _tempIns.Svit_oth_val = 0;
            _tempIns.Svit_tot_val = 0;
            _tempIns.Svit_polc_by = "";
            _tempIns.Svit_polc_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_dbt_no = "";
            _tempIns.Svit_dbt_set_stus = false;
            _tempIns.Svit_dbt_by = "";
            _tempIns.Svit_dbt_dt = Convert.ToDateTime("31/Dec/2999");
            _tempIns.Svit_veg_ref = "";
            _tempIns.Svit_itm_cd = txtItem.Text.ToUpper().Trim();
            return _tempIns;
        }

        protected void btnGVCode_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                DataTable result = CHNLSVC.CommonSearch.GetItemByType(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "GetItmByType";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void txtGVCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtGVCode.Text))
                    return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GetItmByType);
                DataTable _result = CHNLSVC.CommonSearch.GetItemByType(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("ITEM") == txtGVCode.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    DisplayMessage("Please select the valid Gift voucher code");
                    txtGVCode.Text = "";
                    txtGVCode.Focus();
                    return;
                }

                MasterItem _itemList = new MasterItem();
                _itemList = CHNLSVC.Inventory.GetItem(Session["UserCompanyCode"].ToString(), txtGVCode.Text.Trim());

                if (_itemList.Mi_cd == null)
                {
                    DisplayMessage("Invalid item selected.");
                    txtGVCode.Text = "";
                    txtGVCode.Focus();
                    return;
                }

                if (_itemList.Mi_cate_3 != "N/A")
                {
                    DisplayMessage("Selected Gift voucher is not allowed to sales.");
                    txtGVCode.Text = "";
                    txtGVCode.Focus();
                    return;
                }

                cmbGvBook.DataSource = new DataTable();
                DataTable _book = CHNLSVC.Inventory.GetAvailableGvBooks(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "VALUE", "P", txtGVCode.Text.Trim(), "");

                if (_book != null)
                {
                    cmbGvBook.DataSource = _book;
                    cmbGvBook.DataValueField = "GVP_BOOK";
                    cmbGvBook.DataTextField = "GVP_BOOK";
                    cmbGvBook.DataBind();
                    cmbGvBook_SelectedIndexChanged(null, null);
                }
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4, err);
            }
        }

        protected void cmbGvBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<GiftVoucherPages> _tmpList = new List<GiftVoucherPages>();
                cmbTopg.DataSource = _tmpList;

                if (!IsNumeric(cmbGvBook.Text))
                {
                    return;
                }

                _tmpList = CHNLSVC.Inventory.GetAvailableGvPages(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "VALUE", "P", Convert.ToInt32(cmbGvBook.Text), txtGVCode.Text.Trim());


                if (_tmpList != null)
                {
                    cmbTopg.DataSource = _tmpList;
                    cmbTopg.DataValueField = "gvp_page";
                    cmbTopg.DataTextField = "gvp_page";
                    cmbTopg.DataBind();
                    cmbTopg_SelectedIndexChanged(null, null);

                    foreach (GiftVoucherPages tmp in _tmpList)
                    {
                        lblFrompg.Text = tmp.Gvp_page.ToString();
                        goto L1;
                    }
                }
            L1: Int16 i = 0;
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4, err);
            }
        }

        protected void txtPgAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPgAmt.Text))
                {
                    if (string.IsNullOrEmpty(lblPageCount.Text))
                    {
                        DisplayMessage("Pages not select properly");
                        cmbGvBook.Focus();
                        return;
                    }

                    if (!IsNumeric(lblPageCount.Text))
                    {
                        DisplayMessage("Pages not select properly");
                        cmbGvBook.Focus();
                        return;
                    }

                    if (Convert.ToInt32(lblPageCount.Text) <= 0)
                    {
                        DisplayMessage("Pages not select properly");
                        cmbGvBook.Focus();
                        return;
                    }

                    if (!IsNumeric(txtPgAmt.Text))
                    {
                        DisplayMessage("Invalid amount");
                        txtPgAmt.Text = "";
                        txtPgAmt.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(txtPgAmt.Text) < 0)
                    {
                        DisplayMessage("Invalid amount");
                        txtPgAmt.Text = "";
                        txtPgAmt.Focus();
                        return;
                    }

                    txtPgAmt.Text = Convert.ToDecimal(txtPgAmt.Text).ToString("n");
                    txtTotGvAmt.Text = Convert.ToDecimal(Convert.ToDecimal(txtPgAmt.Text) * Convert.ToInt32(lblPageCount.Text)).ToString("n");
                }


            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4, err);
            }
        }

        protected void cmbTopg_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<GiftVoucherPages> _tmpList = new List<GiftVoucherPages>();

            if (!IsNumeric(cmbGvBook.Text) || !IsNumeric(cmbTopg.Text) || !IsNumeric(lblFrompg.Text))
            {
                return;
            }

            _tmpList = CHNLSVC.Inventory.GetAvailableGvPagesRange(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "VALUE", "P", Convert.ToInt32(cmbGvBook.Text), txtGVCode.Text.Trim(), Convert.ToInt32(lblFrompg.Text), Convert.ToInt32(cmbTopg.Text));

            if (_tmpList != null && _tmpList.Count > 0)
            {
                lblPageCount.Text = _tmpList.Count.ToString();
            }
        }

        protected void btnAddGv_Click(object sender, EventArgs e)
        {
            try
            {
                decimal _Amt = 0;

                if (string.IsNullOrEmpty(txtTotGvAmt.Text))
                {
                    DisplayMessage("Invalid amount");
                    txtPgAmt.Text = "";
                    txtPgAmt.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCusCode.Text))
                {
                    DisplayMessage("Please select customer.");
                    txtCusCode.Text = "";
                    txtCusCode.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(txtCusAdd1.Text))
                {
                    DisplayMessage("Please enter customer address.");
                    txtCusAdd1.Text = "";
                    txtCusAdd1.Focus();
                    return;
                }

                _gvDetails = new List<GiftVoucherPages>();
                List<GiftVoucherPages> _tmpList = new List<GiftVoucherPages>();

                _tmpList = CHNLSVC.Inventory.GetAvailableGvPagesRange(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), "VALUE", "P", Convert.ToInt32(cmbGvBook.Text), txtGVCode.Text.Trim(), Convert.ToInt32(lblFrompg.Text), Convert.ToInt32(cmbTopg.Text));

                if (_tmpList == null)
                {
                    DisplayMessage("Cannot find details.");
                    return;
                }

                foreach (GiftVoucherPages _lst in _tmpList)
                {
                    GiftVoucherPages _newTmp = new GiftVoucherPages();
                    _newTmp.Gvp_amt = Convert.ToDecimal(txtPgAmt.Text);
                    _newTmp.Gvp_app_by = null;
                    _newTmp.Gvp_bal_amt = Convert.ToDecimal(txtPgAmt.Text);
                    _newTmp.Gvp_book = _lst.Gvp_book;
                    _newTmp.Gvp_can_by = null;
                    _newTmp.Gvp_can_dt = Convert.ToDateTime(DateTime.Today);
                    _newTmp.Gvp_com = _lst.Gvp_com;
                    _newTmp.Gvp_cre_by = Session["UserID"].ToString();
                    _newTmp.Gvp_cre_dt = Convert.ToDateTime(DateTime.Today);
                    _newTmp.Gvp_cus_add1 = txtCusAdd1.Text;
                    _newTmp.Gvp_cus_add2 = txtCusAdd2.Text;
                    _newTmp.Gvp_cus_cd = txtCusCode.Text;
                    _newTmp.Gvp_cus_mob = txtMobile.Text;
                    _newTmp.Gvp_cus_name = txtCusName.Text;
                    _newTmp.Gvp_gv_cd = _lst.Gvp_gv_cd;
                    _newTmp.Gvp_gv_prefix = _lst.Gvp_gv_prefix;
                    _newTmp.Gvp_gv_tp = _lst.Gvp_gv_tp;
                    _newTmp.Gvp_issue_by = Session["UserID"].ToString();
                    _newTmp.Gvp_issue_dt = Convert.ToDateTime(dtpRecDate.Text).Date;
                    _newTmp.Gvp_line = _lst.Gvp_line;
                    _newTmp.Gvp_mod_by = Session["UserID"].ToString();
                    _newTmp.Gvp_mod_dt = Convert.ToDateTime(DateTime.Today);
                    _newTmp.Gvp_oth_ref = null;
                    _newTmp.Gvp_page = _lst.Gvp_page;
                    _newTmp.Gvp_pc = _lst.Gvp_pc;
                    _newTmp.Gvp_ref = _lst.Gvp_ref;
                    _newTmp.Gvp_stus = "A";
                    _newTmp.Gvp_valid_from = Convert.ToDateTime(dtpRecDate.Text).Date;
                    _newTmp.Gvp_is_allow_promo = chkAllowPromo.Checked;
                    MasterItemWarrantyPeriod _period = CHNLSVC.Inventory.GetItemWarrantyDetail(_lst.Gvp_gv_cd, "GOD");
                    if (_period == null)
                    {
                        DisplayMessage("Voucher valid period not set.");
                        return;
                    }

                    //kapila 24/2/2015
                    if (Convert.ToDateTime(dtGVExp.Text).Date > DateTime.Now.Date)
                        _newTmp.Gvp_valid_to = Convert.ToDateTime(dtGVExp.Text).Date;
                    else
                        _newTmp.Gvp_valid_to = Convert.ToDateTime(dtpRecDate.Text).Date.AddMonths(_period.Mwp_val).Date;

                    _gvDetails.Add(_newTmp);

                }


                dgvGv.AutoGenerateColumns = false;
                dgvGv.DataSource = new List<GiftVoucherPages>();
                dgvGv.DataSource = _gvDetails;
                dgvGv.DataBind();

                _Amt = Convert.ToDecimal(txtTotGvAmt.Text);
                ucPayModes1.TotalAmount = ucPayModes1.TotalAmount + _Amt;
                ucPayModes1.Amount.Text = Convert.ToString(ucPayModes1.TotalAmount + _Amt - Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text));
                ucPayModes1.IsZeroAllow = true;

                ucPayModes1.LoadData();

                //_??tbOth.SelectedTab = tbGv;

                txtGVCode.Text = "";
                lblFrompg.Text = "";
                lblPageCount.Text = "";
                cmbGvBook.DataSource = new DataTable();
                cmbGvBook.DataBind();
                cmbTopg.DataSource = new DataTable();
                cmbTopg.DataBind();
                txtPgAmt.Text = "";
                txtTotGvAmt.Text = "";
                gbGVDet.Enabled = false;
            }
            catch (Exception err)
            {
                DisplayMessage(err.Message, 4, err);
            }
        }

        protected void chkGvFOC_CheckedChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPgAmt.Text))
            {
                if (chkGvFOC.Checked == true)
                {
                    txtTotGvAmt.Text = "0.00";
                }
                else
                {
                    txtTotGvAmt.Text = Convert.ToDecimal(Convert.ToDecimal(txtPgAmt.Text) * Convert.ToInt32(lblPageCount.Text)).ToString("n");
                }
            }
        }

        protected void btnPolicy_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.InsuPolicy);
                DataTable result = CHNLSVC.CommonSearch.GetInsuPolicy(SearchParams, null, null);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "InsuPolicy";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Text = "";
                selDatePanal(false);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 4, ex);
            }
        }

        protected void btnChechQty_Click(object sender, EventArgs e)
        {

        }

        private void BindPriceCombineItem(Int32 _pbseq, Int32 _pblineseq, Int32 _priceType, string _mainItem, string _mainSerial)
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
                _tempPriceCombinItem.ForEach(x => x.Mi_cre_by = Convert.ToString(x.Mi_std_price));
                _tempPriceCombinItem.Where(x => x.Sapc_increse).ToList().ForEach(x => x.Sapc_qty = x.Sapc_qty * Convert.ToDecimal(txtQty.Text.Trim()));
                _tempPriceCombinItem.ForEach(x => x.Sapc_price = x.Sapc_price * CheckSubItemTax(x.Sapc_itm_cd));
                _tempPriceCombinItem.Where(x => !string.IsNullOrEmpty(x.Sapc_sub_ser)).ToList().ForEach(x => x.Sapc_increse = true);
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

        private void promotionsCustomMethod(GridViewRow row1)
        {

            mpPriceNPromotion.Show();

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
                if (_haveSerial == false && chkDeliverLater == false && chkDeliverNow == false)
                    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, 7);
                else if (_haveSerial == true && !string.IsNullOrEmpty(_similerItem) && chkDeliverLater == false)
                    LoadSelectedItemSerialForPriceComItemSerialGv(_item, _status, Convert.ToDecimal(_qty), true, 7);
                else if (_haveSerial == true && chkDeliverLater == false && chkDeliverNow == false)
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
            else if (chkDeliverLater == false && chkDeliverNow == false)
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

        private void LoadSelectedItemSerialForPriceComItemSerialGv(string _item, string _status, decimal _qty, bool _isPromotion, int _isStatusCol)
        {
            DateTime txtDate = Convert.ToDateTime(dtpRecDate.Text);
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
                List<ReptPickSerials> _newlist = GetAgeItemList(Convert.ToDateTime(dtpRecDate.Text).Date, _isAgePriceLevel, _noOfDays, _lst);

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

        protected void btnCloseSimiler_Click(object sender, EventArgs e)
        {
            mpSimilrItmes.Hide();
            mpPriceNPromotion.Show();
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

        private void getSilimerItems(string DocumentType, string ItemCode, string PromotionCode)
        {
            List<MasterItemSimilar> dt = CHNLSVC.Inventory.GetSimilarItems(DocumentType, ItemCode, Session["UserCompanyCode"].ToString(), Convert.ToDateTime(dtpRecDate.Text).Date, string.Empty, PromotionCode, Session["UserDefLoca"].ToString(), Session["UserDefProf"].ToString());
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

        private List<ReptPickSerials> GetAgeItemList(List<ReptPickSerials> _referance, int _noOfDays)
        {
            List<ReptPickSerials> _ageLst = new List<ReptPickSerials>();

            bool _isAgePriceLevel = false;
            DateTime _documentDate = Convert.ToDateTime(dtpRecDate.Text);
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

        private List<ReptPickSerials> GetAgeItemList(DateTime _date, bool _isAgePriceLevel, int _noOfDays, List<ReptPickSerials> _referance)
        {
            List<ReptPickSerials> _ageLst = new List<ReptPickSerials>();
            if (_isAgePriceLevel)
                _ageLst = _referance.Where(x => x.Tus_exist_grndt <= _date.AddDays(-_noOfDays)).ToList();
            else
                _ageLst = _referance;
            return _ageLst;
        }

        protected void btnConfClose_Click(object sender, EventArgs e)
        {
            lblConfText.Text = "";
            mpConfirmation.Hide();
        }
        protected void btnConfClose_ClickExcel(object sender, EventArgs e)
        {
            //lblConfText.Text = "";
            ModalPopupExtenderExcel.Hide();
        }
        protected void btnConfNo_ClickExcel(object sender, EventArgs e)
        {
            ModalPopupExtenderExcel.Hide();
        }
        protected void btnConfYes_Click(object sender, EventArgs e)
        {
            mpConfirmation.Hide();
            hdfConf.Value = "1";
            hdfConfItem.Value = txtItem.Text.ToUpper().Trim();
            hdfConfStatus.Value = cmbStatus.SelectedValue.ToString();
            txtItemCode_TextChange();
        }

        protected void btnConfNo_Click(object sender, EventArgs e)
        {
            hdfConf.Value = "0";
            hdfConfItem.Value = txtItem.Text.ToUpper().Trim();
            hdfConfStatus.Value = cmbStatus.SelectedValue.ToString();
            txtItemCode_TextChange();
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

        private string SetDecimalPoint(decimal amount)
        {
            return amount.ToString("N2");
        }

        private string SetDecimalPoint(string amount)
        {
            decimal value = Convert.ToDecimal(amount);
            return value.ToString("N2");
        }

        protected void txtRecNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRecNo.Text))
            {
                _IsRecall = false;
                _RecStatus = false;
                _sunUpload = false;
                Boolean _isValidRec = false;

                RecieptHeader _ReceiptHeader = null;
                _ReceiptHeader = CHNLSVC.Sales.GetReceiptHeader(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtRecNo.Text.Trim());

                if (_ReceiptHeader != null)
                {
                    _IsRecall = true;
                    txtRecType.Text = _ReceiptHeader.Sar_receipt_type;
                    //  ucPayModes1.Enabled = false;

                    _isValidRec = CHNLSVC.Sales.IsValidReceiptType(Session["UserCompanyCode"].ToString(), _ReceiptHeader.Sar_receipt_type);

                    if (_isValidRec == false)
                    {
                        string msg = "Not allowed to view receipt type " + _ReceiptHeader.Sar_receipt_type + " in receipt module.";
                        DisplayMessage(msg);
                        txtRecType.Text = "";
                        Clear_Data();
                        return;
                    }

                    txtRecNo.Text = _ReceiptHeader.Sar_receipt_no;
                    dtpRecDate.Text = Convert.ToDateTime(_ReceiptHeader.Sar_receipt_date).Date.ToString("dd/MMM/yyyy");
                    //dtpRecDate.Enabled = false;
                    dtpRecDate.ReadOnly = true;
                    if (_ReceiptHeader.Sar_anal_8 == 1)
                    {
                        radioButtonManual.Checked = true;
                    }
                    else
                    {
                        radioButtonManual.Checked = false;
                    }
                    txtManual.Text = _ReceiptHeader.Sar_manual_ref_no;
                    txtNote.Text = _ReceiptHeader.Sar_remarks;
                    txtRemarks.Text = _ReceiptHeader.Sar_remarks;
                    txtTotal.Text = Convert.ToDecimal(_ReceiptHeader.Sar_tot_settle_amt).ToString("n");
                    txtCusCode.Text = _ReceiptHeader.Sar_debtor_cd;
                    txtCusAdd1.Text = _ReceiptHeader.Sar_debtor_add_1;
                    txtCusAdd2.Text = _ReceiptHeader.Sar_debtor_add_2;
                    txtCusName.Text = _ReceiptHeader.Sar_debtor_name;
                    txtMobile.Text = _ReceiptHeader.Sar_mob_no;
                    txtNIC.Text = _ReceiptHeader.Sar_nic_no;
                    txtProvince.Text = _ReceiptHeader.Sar_anal_2;
                    ucPayModes1.PaidAmountLabel.Text = Convert.ToDecimal(_ReceiptHeader.Sar_tot_settle_amt).ToString("n");
                    _usedAmt = _ReceiptHeader.Sar_used_amt;
                    if (_ReceiptHeader.Sar_receipt_type == "ADVAN")
                    {
                        txtDivision.Text = _ReceiptHeader.Sar_ref_doc;
                    }
                    else
                    {
                        txtDivision.Text = _ReceiptHeader.Sar_prefix;
                    }
                    if (_ReceiptHeader.Sar_is_oth_shop == true)
                    {
                        chkOth.Checked = true;
                        txtOthSR.Text = _ReceiptHeader.Sar_oth_sr;
                        chkOth.Enabled = false;
                        txtOthSR.Enabled = false;
                    }


                    if (string.IsNullOrEmpty(_ReceiptHeader.Sar_anal_1))
                    {
                        //  ddlDistrict.SelectedValue = " ";
                    }
                    else
                    {
                        cmbDistrict.Text = _ReceiptHeader.Sar_anal_1;
                    }
                    _RecStatus = _ReceiptHeader.Sar_act;
                    _sunUpload = _ReceiptHeader.Sar_uploaded_to_finance;

                    if (string.IsNullOrEmpty(_ReceiptHeader.Sar_anal_4))
                    {
                        txtSalesEx.Text = "";
                    }
                    else
                    {
                        txtSalesEx.Text = _ReceiptHeader.Sar_anal_4;
                    }
                    txtCusCode.Enabled = false;


                    BindSaveReceiptDetails(_ReceiptHeader.Sar_receipt_no);
                    BindSaveVehicleReg(_ReceiptHeader.Sar_receipt_no);
                    BindSaveVehicleIns(_ReceiptHeader.Sar_receipt_no);

                    _gvDetails = new List<GiftVoucherPages>();
                    _gvDetails = CHNLSVC.Inventory.GetGiftVoucherByOthRef(_ReceiptHeader.Sar_com_cd, _ReceiptHeader.Sar_profit_center_cd, _ReceiptHeader.Sar_receipt_no);

                    dgvGv.AutoGenerateColumns = false;
                    dgvGv.DataSource = new List<GiftVoucherPages>();
                    dgvGv.DataSource = _gvDetails;
                    dgvGv.DataBind();

                    //if (txtRecType.Text == "VHREG")
                    //{
                    //    tbOth.SelectedTab = tpReg;
                    //}
                    //else if (txtRecType.Text == "VHINS")
                    //{
                    //    tbOth.SelectedTab = tpInsu;
                    //}
                    //else if (txtRecType.Text == "GVISU")
                    //{
                    //    tbOth.SelectedTab = tbGv;
                    //}

                    _tmpRecItem = new List<ReceiptItemDetails>();
                    _tmpRecItem = CHNLSVC.Sales.GetAdvanReceiptItems(_ReceiptHeader.Sar_receipt_no);

                    //dgvItem.Rows.Clear();
                    dgvItem.DataSource = new int[] { };
                    dgvItem.DataBind();

                    if (_tmpRecItem != null)
                    {
                        List<gvInvoiceItems> oItem = new List<gvInvoiceItems>();

                        foreach (ReceiptItemDetails ser in _tmpRecItem)
                        {
                            gvInvoiceItems irtm = new gvInvoiceItems();
                            irtm.col_itmItem = ser.Sari_item;
                            irtm.col_itmDesc = ser.Sari_item_desc;
                            irtm.col_itmModel = ser.Sari_model;
                            irtm.col_itmSerial = ser.Sari_serial;
                            irtm.col_itmOthSerial = ser.Sari_serial_1;
                            irtm.colpb = ser.Sari_pb;
                            irtm.colpblvl = ser.Sari_pb_lvl;
                            irtm.col_itmStatus = ser.Sari_sts;
                            irtm.col_itmStatus_Desc = getItemStatusDesc(ser.Sari_sts);
                            irtm.colQty = ser.Sari_qty;
                            irtm.colTax = ser.Sari_tax_amt;
                            irtm.col_itmModel = ser.Sari_model;
                            irtm.colRate = ser.Sari_unit_rate;
                            irtm.colamt = ser.Sari_unit_amt;
                            oItem.Add(irtm);
                        }

                        dgvItem.DataSource = oItem;
                        dgvItem.DataBind();
                    }

                    btnSave.Enabled = false;
                    btnCancel.Enabled = true;
                    btn_add_ser.Enabled = false;

                    ButtonEnableDisavle(false, btnSave, "");
                    ButtonEnableDisavle(true, btnCancel, "confCancel");
                    ButtonEnableDisavle(false, btn_add_ser, "");

                    pnlPaymentsadd.Visible = true;

                    if (chkUnAllocated.Checked)
                    {
                        pnlDebtInvoices.Visible = true;
                        pnlPaymentsadd.Visible = false;
                        gbsettle.Visible = false;
                        cmdPayTypeAlloca.DataSource = ucPayModes1.PayModeCombo.DataSource;
                        cmdPayTypeAlloca.DataBind();

                        btnAllocate.Visible = true;
                    }
                }
            }
        }

        private void BindSaveReceiptDetails(string _RecNo)
        {
            RecieptItem _paramRecDetails = new RecieptItem();

            _paramRecDetails.Sard_receipt_no = _RecNo;
            _list = new List<RecieptItem>();
            _list = CHNLSVC.Sales.GetReceiptDetails(_paramRecDetails);
            ucPayModes1.RecieptItemList = _list;
            ucPayModes1.MainGrid.DataSource = _list;
            ucPayModes1.MainGrid.DataBind();

            if (_list != null && _list.Count > 0)
            {
                List<string> paymnetType = _list.Where(z => z.Sard_pay_tp != "").Select(x => x.Sard_pay_tp).ToList();
                if (paymnetType != null && paymnetType.Count > 0)
                {
                    paymnetType.Insert(0, "Select");
                    cmdPayTypeAlloca.DataSource = paymnetType;
                    cmdPayTypeAlloca.DataBind();
                }

                List<string> ChequeNums = _list.Where(z => z.Sard_ref_no != "").Select(x => x.Sard_ref_no).ToList();
                if (ChequeNums != null && ChequeNums.Count > 0)
                {
                    ChequeNums.Insert(0, "Select");
                    cmdCheqNums.DataSource = ChequeNums;
                    cmdCheqNums.DataBind();
                }
            }
        }

        private void BindSaveVehicleReg(string _RecNo)
        {
            List<VehicalRegistration> _list = CHNLSVC.General.GetVehicalRegistrations(_RecNo);
            _regList = new List<VehicalRegistration>();
            _regList = _list;

            dgvReg.DataSource = new int[] { };
            dgvReg.DataBind();

            if (_regList != null)
            {
                List<gvRegItems> item = new List<gvRegItems>();
                foreach (VehicalRegistration reg in _regList)
                {
                    gvRegItems oItem = new gvRegItems();
                    oItem.col_regInv = reg.P_svrt_inv_no;
                    oItem.col_regCus = reg.P_svrt_full_name;
                    oItem.col_regItem = reg.P_srvt_itm_cd;
                    oItem.col_regModel = reg.P_svrt_model;
                    oItem.col_regBrand = reg.P_svrt_brd;
                    oItem.col_regDis = reg.P_svrt_district;
                    oItem.col_regPro = reg.P_svrt_province;
                    oItem.col_regEngine = reg.P_svrt_engine;
                    oItem.col_regChasis = reg.P_svrt_chassis;
                    oItem.col_regVal = reg.P_svrt_reg_val;
                    item.Add(oItem);
                }

                dgvReg.DataSource = item;
                dgvReg.DataBind();
            }
        }

        private void BindSaveVehicleIns(string _RecNo)
        {
            List<VehicleInsuarance> _list = CHNLSVC.General.GetVehicalInsuarance(_RecNo, null);
            _insList = new List<VehicleInsuarance>();
            _insList = _list;

            dgvIns.DataSource = new int[] { };
            dgvIns.DataBind();

            if (_insList != null)
            {
                List<dgvInsItems> item = new List<dgvInsItems>();
                foreach (VehicleInsuarance ins in _insList)
                {
                    dgvInsItems oItem = new dgvInsItems();
                    oItem.col_insInv = ins.Svit_inv_no;
                    oItem.col_insCus = ins.Svit_full_name;
                    oItem.col_insItem = ins.Svit_itm_cd;
                    oItem.col_insModel = ins.Svit_model;
                    oItem.col_insDistrict = ins.Svit_district;
                    oItem.col_insPro = ins.Svit_province;
                    oItem.col_insCom = ins.Svit_ins_com;
                    oItem.col_insPol = ins.Svit_ins_polc;
                    oItem.col_insEngine = ins.Svit_engine;
                    oItem.col_insChasis = ins.Svit_chassis;
                    oItem.col_insVal = ins.Svit_ins_val;
                    item.Add(oItem);
                }
                dgvIns.DataSource = item;
                dgvIns.DataBind();
            }
        }

        protected void txtDisposalJob_TextChanged(object sender, EventArgs e)
        {
            DisposalHeader oDisposalHeader = CHNLSVC.Inventory.GET_DISPOSAL_JOB_HEADER(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtDisposalJob.Text.Trim(), "ALL");
            if (oDisposalHeader != null && !string.IsNullOrEmpty(oDisposalHeader.Dh_doc_no))
            {
                if (oDisposalHeader.Dh_stus != "P" && oDisposalHeader.Dh_stus != "A")
                {
                    DisplayMessage("Job should be in pending stage");
                    txtDisposalJob.Text = "";
                    txtDisposalJob.Focus();
                    return;
                }
            }
        }

        protected void btnSearchDisposal_Click(object sender, EventArgs e)
        {
            try
            {
                txtFDate.Text = DateTime.Now.AddMonths(-1).ToString("dd/MMM/yyyy");
                txtTDate.Text = DateTime.Now.ToString("dd/MMM/yyyy");
                ViewState["SEARCH"] = null;
                txtSearchbyword.Text = string.Empty;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.DisposalJOb);
                DataTable result = CHNLSVC.CommonSearch.SEARCH_DISPOSAL_JOB(SearchParams, null, null, DateTime.Now, DateTime.Now);
                grdResult.DataSource = result;
                grdResult.DataBind();
                lblvalue.Text = "DisposalJOb";
                BindUCtrlDDLData(result);
                ViewState["SEARCH"] = result;
                mpSearchAdance.Show();
                txtSearchbyword.Focus();
                selDatePanal(true);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, 1);
                mpSearchAdance.Show();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRecNo.Text))
            {
                DisplayMessage("Please select receipt number.");
                txtRecNo.Focus();
                return;
            }

            if (_RecStatus == false)
            {
                DisplayMessage("This receipt is already cancelled.");
                return;
            }

            if (_sunUpload == true)
            {
                DisplayMessage("Cannot cancel.Already uploaded to accounts.");
                return;
            }

            if (txtRecType.Text == "ADVAN")
            {
                DataTable _adv = CHNLSVC.Sales.CheckAdvanForIntr(Session["UserCompanyCode"].ToString(), txtRecNo.Text.Trim());
                if (_adv != null && _adv.Rows.Count > 0)
                {
                    DisplayMessage("This advance receipt is already picked for a inter-transfer. You are not allow to cancel.");
                    txtRecNo.Text = "";
                    txtRecNo.Focus();
                    return;
                }

                if (_usedAmt > 0)
                {
                    DisplayMessage("This advance receipt is already utilized. You are not allow to cancel.");
                    txtRecNo.Text = "";
                    txtRecNo.Focus();
                    return;
                }

            }

            if (txtRecType.Text == "VHREG")
            {
                List<VehicalRegistration> _tempList = new List<VehicalRegistration>();
                _tempList = CHNLSVC.Sales.GetVehicalRegByRefNo(txtRecNo.Text.Trim());

                foreach (VehicalRegistration temp in _tempList)
                {
                    if (temp.P_srvt_rmv_stus == 1)
                    {
                        string msg1 = "Cannot cancel Receipt.Documents are already send to the RMV. Engine # : " + temp.P_svrt_engine;
                        DisplayMessage(msg1);
                        return;
                    }
                    else if (temp.P_svrt_prnt_stus == 2)
                    {
                        string msg2 = "Cannot cancel Receipt. Engine # : " + temp.P_svrt_engine + " already cancel.";
                        DisplayMessage(msg2);
                        return;
                    }
                }

            }

            if (txtRecType.Text == "VHINS")
            {
                List<VehicleInsuarance> _tempInsuList = new List<VehicleInsuarance>();
                _tempInsuList = CHNLSVC.Sales.GetVehicalInsByRefNo(txtRecNo.Text.Trim());

                foreach (VehicleInsuarance temp in _tempInsuList)
                {
                    if (temp.Svit_polc_stus == true)
                    {
                        string msg3 = "Cannot cancel Receipt.Policy is already issued. Engine # : " + temp.Svit_engine;
                        DisplayMessage(msg3);
                        return;
                    }

                    else if (temp.Svit_cvnt_issue == 2)
                    {
                        string msg4 = "Cannot cancel Receipt. Engine # : " + temp.Svit_engine + " already cancel.";
                        DisplayMessage(msg4);
                        return;
                    }
                }
            }
            bool _allowCurrentTrans = false;
            if (!IsAllowBackDateForModuleNew(Session["UserCompanyCode"].ToString(), Session["UserDefLoca"].ToString(), Session["UserDefProf"].ToString(), this.Page, dtpRecDate.Text, btndtpRecDate, lblBackDateInfor, "m_Trans_Sales_SalesInvoice", out  _allowCurrentTrans))
            {
                if (_allowCurrentTrans == true)
                {
                    if (Convert.ToDateTime(dtpRecDate.Text).Date != DateTime.Now.Date)
                    {
                        DisplayMessage("Back date not allow for selected date!");
                        return;
                    }
                }
                else
                {
                    DisplayMessage("Back date not allow for selected date!");
                    return;
                }
            }
            UpdateRecStatus(false);
        }

        private void UpdateRecStatus(Boolean _RecUpdateStatus)
        {
            Int32 row_aff = 0;
            string _msg = string.Empty;

            RecieptHeader _UpdateReceipt = new RecieptHeader();
            _UpdateReceipt.Sar_receipt_no = txtRecNo.Text.Trim();
            _UpdateReceipt.Sar_act = _RecUpdateStatus;
            _UpdateReceipt.Sar_com_cd = Session["UserCompanyCode"].ToString();
            _UpdateReceipt.Sar_profit_center_cd = Session["UserDefProf"].ToString();
            _UpdateReceipt.Sar_mod_by = Session["UserID"].ToString();
            _UpdateReceipt.Sar_receipt_type = txtRecType.Text.Trim();
            _UpdateReceipt.Sar_debtor_cd = txtCusCode.Text.Trim();
            _UpdateReceipt.Sar_debtor_name = txtCusName.Text.Trim();
            _UpdateReceipt.Sar_debtor_add_1 = txtCusAdd1.Text.Trim();
            _UpdateReceipt.Sar_debtor_add_2 = txtCusAdd2.Text.Trim();
            _UpdateReceipt.Sar_mob_no = txtMobile.Text.Trim();
            _UpdateReceipt.Sar_anal_1 = cmbDistrict.Text.Trim();
            _UpdateReceipt.Sar_anal_2 = txtProvince.Text.Trim();
            _UpdateReceipt.Sar_receipt_date = Convert.ToDateTime(dtpRecDate.Text).Date;
            _UpdateReceipt.Sar_tot_settle_amt = Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text);
            if (chkOth.Checked == true)
            {
                _UpdateReceipt.Sar_is_oth_shop = true;
                _UpdateReceipt.Sar_oth_sr = txtOthSR.Text;
            }

            row_aff = (Int32)CHNLSVC.Sales.ReceiptCancelProcess(_UpdateReceipt, _list, _regList, _insList, _gvDetails, _tmpRecItem);

            if (row_aff == 1)
            {
                DisplayMessage("Receipt cancelled successfully.", 3);
                if (txtRecType.Text == "VHREG" || txtRecType.Text == "VHINS")
                {
                    row_aff = (Int32)CHNLSVC.Sales.ReceiptCancelInfo(_UpdateReceipt, _regList, _insList);
                }
                Clear_Data();
            }
            else
            {
                DisplayMessage("Error occurred while processing.");
            }
        }

        protected void btnAddPanyment_Click(object sender, EventArgs e)
        {
            bool _isoutstnd = false;
            if (txtRecType.Text == "DEBT")
            {
                //Add by Udaya 13.09.2017
                HpSystemParameters _SystemPara = new HpSystemParameters();
                _SystemPara = CHNLSVC.Sales.GetSystemParameter("PC", Session["UserDefProf"].ToString(), "DEBTALAMT", DateTime.Now.Date);
                if (_SystemPara.Hsy_cd != null && (Convert.ToDecimal(txtPayment.Text) > Convert.ToDecimal(txtBalance.Text)))
                {
                    Decimal balance = Convert.ToDecimal(txtPayment.Text) - Convert.ToDecimal(txtBalance.Text);
                    if (balance > _SystemPara.Hsy_val)
                    {
                        DisplayMessage("Payment exceed the outstanding amount by Rs. " + balance);
                        txtPayment.Focus();
                        return;
                    }
                    else
                    {
                        if (balance > 0)
                        {
                            lblerror.Text = string.Empty;
                            lbldata.Text = "Payment amount exceed the outstanding by Rs. " + balance;
                            lblmsg.Text = "Do you want to continue ?";
                            mdlconfDb.Show();
                            _isoutstnd = true;
                        }
                    }
                }
                else if (_SystemPara.Hsy_cd == null && (Convert.ToDecimal(txtPayment.Text) > Convert.ToDecimal(txtBalance.Text)))
                {
                    DisplayMessage("Payment cannot exceed outstanding amount.");
                    return;
                }

                if (Convert.ToDecimal(txtPayment.Text) <= 0)
                {
                    DisplayMessage("Settle amount cannot be zero.");
                    //txtPayment.Text = "";
                    txtPayment.Focus();
                    return;
                }

                foreach (RecieptItem line in ucPayModes1.RecieptItemList)
                {
                    if (line.Sard_inv_no == txtInvoice.Text.Trim())
                    {
                        DisplayMessage("Already add this invoice.");
                        //txtPayment.Text = "";
                        txtPayment.Focus();
                        return;
                    }
                }

                if (ucPayModes1.InvoiceNo == txtInvoice.Text.Trim())
                {
                    DisplayMessage("Already add this invoice.");
                    //txtPayment.Text = "";
                    txtPayment.Focus();
                    return;
                }
                else
                {
                    if (!_isoutstnd)
                    {
                        btnPayment_Click(null, null);
                    }
                }
            }
            else
            {
                btnPayment_Click(null, null);
            }
            //btnPayment_Click(null, null);
        }

        private void loadItemStatus()
        {
            List<MasterItemStatus> oMasterItemStatuss = CHNLSVC.General.GetAllStockTypes(Session["UserCompanyCode"].ToString());
            Session["ItemStatus"] = oMasterItemStatuss;
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

        protected void btnAddInvcoices_Click(object sender, EventArgs e)
        {
            if (true)
            {

            }
        }

        protected void btnDeletInvoices_Click(object sender, EventArgs e)
        {
            GridViewRow dr = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblcol_regInv = dr.FindControl("lblcol_regInv") as Label;
            Label lblcol_PayType = dr.FindControl("lblcol_PayType") as Label;

            if (oDebitInvoices.Count > 0 && oDebitInvoices.FindAll(x => x.col_regInv == lblcol_regInv.Text.Trim() && x.col_PayType == lblcol_PayType.Text).Count > 0)
            {
                oDebitInvoices.RemoveAll(x => x.col_regInv == lblcol_regInv.Text.Trim() && x.col_PayType == lblcol_PayType.Text);
            }

            dgvDebtInvoiceList.DataSource = oDebitInvoices;
            dgvDebtInvoiceList.DataBind();

            PaymentValueChange(txtRecNo.Text.Trim(), true);
        }

        protected void btnSearchInvoiceAll_Click(object sender, EventArgs e)
        {

            ViewState["SEARCH"] = null;
            txtSearchbyword.Text = string.Empty;

            if (string.IsNullOrEmpty(txtCusCode.Text))
            {
                DisplayMessage("Please select customer.");
                txtCusCode.Focus();
                return;
            }

            DataTable result = null;
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.OutstandingInv);
            result = CHNLSVC.CommonSearch.GetOutstandingInvoice(SearchParams, null, null);
            lblvalue.Text = "OutstandingInv_Allo";
            grdResult.DataSource = result;
            grdResult.DataBind();
            BindUCtrlDDLData(result);
            ViewState["SEARCH"] = result;
            mpSearchAdance.Show();
            txtSearchbyword.Text = "";
            selDatePanal(false);
        }

        protected void btnAddInvoiceAlloc_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNumAllo.Text == "")
            {
                DisplayMessage("Please select invoice number");
                return;
            }

            if (txtAmountAlloca.Text == "")
            {
                DisplayMessage("Please enter invoice amount");
                return;
            }
            if (cmdPayTypeAlloca.SelectedIndex == 0)
            {
                DisplayMessage("Please select a payment type");
                return;
            }

            if (cmdPayTypeAlloca.SelectedValue.ToString() == "CHEQUE" && cmdCheqNums.SelectedIndex == 0)
            {
                DisplayMessage("Please select cheque number");
                return;
            }
            gvRegItems oNewItem = new gvRegItems();
            oNewItem.col_regInv = txtInvoiceNumAllo.Text.Trim();
            oNewItem.col_Amount = Convert.ToDecimal(txtAmountAlloca.Text.Trim());
            oNewItem.col_PayType = cmdPayTypeAlloca.SelectedValue.ToString();
            if (oNewItem.col_PayType == "CHEQUE")
            {
                oNewItem.col_regModel = cmdCheqNums.SelectedValue.ToString();
            }
            else
            {
                oNewItem.col_regModel = "";
            }

            if (oDebitInvoices.Count > 0 && oDebitInvoices.FindAll(x => x.col_regInv == txtInvoiceNumAllo.Text.Trim() && x.col_PayType == oNewItem.col_PayType).Count > 0)
            {
                DisplayMessage("Selected invoice is already added.");
                txtInvoiceNumAllo.Focus();
                return;
            }
            else
            {
                RecieptItem oReduceItem = null;
                if (cmdPayTypeAlloca.SelectedValue.ToString().ToUpper() == "CHEQUE")
                {
                    oReduceItem = _list.Find(x => x.Sard_pay_tp == cmdPayTypeAlloca.SelectedValue.ToString() && x.Sard_ref_no == cmdCheqNums.SelectedValue.ToString());
                }
                else
                {
                    oReduceItem = _list.Find(x => x.Sard_pay_tp == cmdPayTypeAlloca.SelectedValue.ToString());
                }
                if (oReduceItem == null)
                {
                    return;
                }

                oReduceItem.Sard_settle_amt = oReduceItem.Sard_settle_amt - Convert.ToDecimal(txtAmountAlloca.Text);
                if (oReduceItem.Sard_settle_amt < 0)
                {
                    DisplayMessage("Please enter valid amount");
                    return;
                }
                else
                {
                    //if (PaymentValueChange(txtRecNo.Text.Trim(), false))
                    {
                        oDebitInvoices.Add(oNewItem);
                        PaymentValueChange(txtRecNo.Text.Trim(), true);
                        txtInvoiceNumAllo.Text = "";
                        txtAmountAlloca.Text = "";
                        cmdCheqNums.SelectedIndex = 0;
                        cmdPayTypeAlloca.SelectedIndex = 0;
                        cmdCheqNums.Enabled = false;
                        txtInvoiceNumAllo.Focus();
                    }
                    //else
                    //{
                    //    DisplayMessage("Please enter valid amount");
                    //    return;
                    //}
                }

            }

            dgvDebtInvoiceList.DataSource = oDebitInvoices;
            dgvDebtInvoiceList.DataBind();

        }

        protected void txtInvoiceNumAllo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInvoiceNumAllo.Text))
            {
                if (txtRecType.Text == "DEBT" || txtRecType.Text == "SRTN" || txtRecType.Text == "SVAT" || txtRecType.Text == "DAJ")
                {
                    if (string.IsNullOrEmpty(txtCusCode.Text))
                    {
                        DisplayMessage("Please select customer first.");
                        txtCusCode.Text = "";
                        txtCusCode.Focus();
                        return;
                    }

                    //check valid invoice
                    List<InvoiceHeader> _invHdr = new List<InvoiceHeader>();
                    if (chkOth.Checked == true)
                        _invHdr = CHNLSVC.Sales.GetPendingInvoices(Session["UserCompanyCode"].ToString(), txtOthSR.Text, txtCusCode.Text.Trim(), txtInvoiceNumAllo.Text.Trim(), "C", dtpRecDate.Text.Trim(), dtpRecDate.Text.Trim());
                    else
                        _invHdr = CHNLSVC.Sales.GetPendingInvoices(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text.Trim(), txtInvoiceNumAllo.Text.Trim(), "C", dtpRecDate.Text.Trim(), dtpRecDate.Text.Trim());

                    if (_invHdr == null || _invHdr.Count == 0)
                    {
                        DisplayMessage("Invalid invoice number.");
                        txtInvoiceNumAllo.Text = "";
                        txtInvoiceNumAllo.Focus();
                        return;
                    }

                    foreach (InvoiceHeader _tmpInv in _invHdr)
                    {
                        if (_tmpInv.Sah_stus == "C" || _tmpInv.Sah_stus == "R")
                        {
                            DisplayMessage("Selected invoice is canceled or reversed.");
                            txtInvoiceNumAllo.Text = "";
                            txtInvoiceNumAllo.Focus();
                            return;
                        }
                    }

                    txtInvoiceNumAllo.ToolTip = txtInvoiceNumAllo.Text.Trim();

                    if (chkOth.Checked == true)
                        txtAmountAlloca.Text = Convert.ToDecimal(CHNLSVC.Sales.GetOutInvAmt(Session["UserCompanyCode"].ToString(), txtOthSR.Text, txtCusCode.Text, txtInvoiceNumAllo.Text)).ToString("n");
                    else
                        txtAmountAlloca.Text = Convert.ToDecimal(CHNLSVC.Sales.GetOutInvAmt(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text, txtInvoiceNumAllo.Text)).ToString("n");

                    if (Convert.ToDecimal(txtAmountAlloca.Text) <= 0)
                    {
                        DisplayMessage("Cannot find outstanding amount.");
                        txtInvoiceNumAllo.Text = "";
                        txtAmountAlloca.Text = "0.00";
                        txtInvoiceNumAllo.Focus();
                    }

                    if (oDebitInvoices.Count > 0 && oDebitInvoices.FindAll(x => x.col_regInv == txtInvoiceNumAllo.Text.Trim()).Count > 0)
                    {
                        decimal addedValue = oDebitInvoices.Where(x => x.col_regInv == txtInvoiceNumAllo.Text.Trim()).Sum(c => c.col_Amount);
                        txtAmountAlloca.Text = (Convert.ToDecimal(txtAmountAlloca.Text) - addedValue).ToString("N2");
                    }
                }
                else
                {
                    txtAmountAlloca.Text = "0.00";
                }
            }
        }

        protected void btnAllocate_Click(object sender, EventArgs e)
        {
            RecieptHeader _ReceiptHeader = new RecieptHeader();
            _ReceiptHeader.Sar_seq_no = CHNLSVC.Inventory.GetSerialID(); //CHNLSVC.Inventory.Generate_new_seq_num(GlbUserName, "RECEIPT", 1, Session["UserCompanyCode"].ToString());
            _ReceiptHeader.Sar_com_cd = Session["UserCompanyCode"].ToString();
            _ReceiptHeader.Sar_receipt_type = txtRecType.Text.Trim();
            _ReceiptHeader.Sar_receipt_no = _ReceiptHeader.Sar_seq_no.ToString();// txtRecNo.Text.Trim();
            if (txtRecType.Text == "ADVAN")
            {
                _ReceiptHeader.Sar_prefix = comboBoxPrefix.SelectedValue.ToString();
            }
            else
            {
                _ReceiptHeader.Sar_prefix = txtDivision.Text.Trim();
            }
            if (string.IsNullOrEmpty(txtManual.Text))
            {
                txtManual.Text = "0";
            }
            else
            {
                _ReceiptHeader.Sar_manual_ref_no = txtManual.Text.Trim();
            }

            _ReceiptHeader.Sar_receipt_date = Convert.ToDateTime(dtpRecDate.Text).Date;
            _ReceiptHeader.Sar_direct = true;
            _ReceiptHeader.Sar_acc_no = "";
            if (chkOth.Checked == true)
            {
                _ReceiptHeader.Sar_is_oth_shop = true;
                _ReceiptHeader.Sar_oth_sr = "All";
            }
            else
            {
                _ReceiptHeader.Sar_is_oth_shop = false;
                _ReceiptHeader.Sar_oth_sr = "";
            }
            _ReceiptHeader.Sar_profit_center_cd = Session["UserDefProf"].ToString();
            _ReceiptHeader.Sar_debtor_cd = txtCusCode.Text.Trim();
            _ReceiptHeader.Sar_debtor_name = txtCusName.Text.Trim();
            _ReceiptHeader.Sar_debtor_add_1 = txtCusAdd1.Text.Trim();
            _ReceiptHeader.Sar_debtor_add_2 = txtCusAdd2.Text.Trim();
            _ReceiptHeader.Sar_tel_no = "";
            _ReceiptHeader.Sar_mob_no = txtMobile.Text.Trim();
            _ReceiptHeader.Sar_nic_no = txtNIC.Text.Trim();
            _ReceiptHeader.Sar_tot_settle_amt = Convert.ToDecimal(ucPayModes1.PaidAmountLabel.Text);
            _ReceiptHeader.Sar_comm_amt = 0;
            _ReceiptHeader.Sar_is_mgr_iss = false;
            _ReceiptHeader.Sar_esd_rate = 0;
            _ReceiptHeader.Sar_wht_rate = 0;
            _ReceiptHeader.Sar_epf_rate = 0;
            _ReceiptHeader.Sar_currency_cd = "LKR";
            _ReceiptHeader.Sar_uploaded_to_finance = false;
            _ReceiptHeader.Sar_act = true;
            _ReceiptHeader.Sar_direct_deposit_bank_cd = "";
            _ReceiptHeader.Sar_direct_deposit_branch = "";
            _ReceiptHeader.Sar_remarks = txtRemarks.Text.Trim();
            _ReceiptHeader.Sar_is_used = false;
            if (txtRecType.Text == "ADVAN")
            {
                _ReceiptHeader.Sar_ref_doc = txtDivision.Text.Trim();
            }
            else if (txtRecType.Text == "DISP")
            {
                _ReceiptHeader.Sar_ref_doc = txtDisposalJob.Text.Trim();
            }
            else
            {
                _ReceiptHeader.Sar_ref_doc = "";
            }
            _ReceiptHeader.Sar_ser_job_no = "";
            _ReceiptHeader.Sar_used_amt = 0;

            if ((txtRecType.Text == "DEBT" || txtRecType.Text == "SRTN" || txtRecType.Text == "SVAT" || txtRecType.Text == "DAJ") && dgvDebtInvoiceList.Rows.Count == 0)
            {
                _ReceiptHeader.Sar_used_amt = -1;
            }

            _ReceiptHeader.Sar_create_by = Session["UserID"].ToString();
            _ReceiptHeader.Sar_mod_by = Session["UserID"].ToString();
            _ReceiptHeader.Sar_session_id = Session["SessionID"].ToString();
            _ReceiptHeader.Sar_anal_1 = cmbDistrict.Text;
            _ReceiptHeader.Sar_anal_2 = txtProvince.Text.Trim();

            if (radioButtonManual.Checked == true)
            {
                _ReceiptHeader.Sar_anal_3 = "MANUAL";
                _ReceiptHeader.Sar_anal_8 = 1;
            }
            else
            {
                _ReceiptHeader.Sar_anal_3 = "SYSTEM";
                _ReceiptHeader.Sar_anal_8 = 0;
            }

            _ReceiptHeader.Sar_anal_4 = txtSalesEx.Text;
            _ReceiptHeader.Sar_anal_5 = 0;
            _ReceiptHeader.Sar_anal_6 = 0;
            _ReceiptHeader.Sar_anal_7 = 0;
            _ReceiptHeader.Sar_anal_9 = 0;
            _ReceiptHeader.SAR_VALID_TO = _ReceiptHeader.Sar_receipt_date.AddDays(Convert.ToDouble(0));
            //_ReceiptHeader.Sar_scheme = lblSchme.Text;
            _ReceiptHeader.Sar_inv_type = lblSalesType.Text;

            List<RecieptItem> _ReceiptDetailsSave = new List<RecieptItem>();
            Int32 _line = 0;
            foreach (RecieptItem line in ucPayModes1.RecieptItemList)
            {
                line.Sard_seq_no = _ReceiptHeader.Sar_seq_no;
                _line = _line + 1;
                line.Sard_line_no = _line;
                _ReceiptDetailsSave.Add(line);
            }

            List<RecieptItem> oDebitReceiptItems = new List<RecieptItem>();

            foreach (gvRegItems item in oDebitInvoices)
            {
                RecieptItem oNewItem = new RecieptItem();
                oNewItem.Sard_inv_no = item.col_regInv;
                oNewItem.Sard_pay_tp = item.col_PayType;
                oNewItem.Sard_settle_amt = item.col_Amount;
                oNewItem.Sard_ref_no = item.col_regModel;
                oDebitReceiptItems.Add(oNewItem);
            }

            string err = string.Empty;
            Int32 result = CHNLSVC.Sales.ReceiptInvoiceAllocation(_ReceiptHeader, _ReceiptDetailsSave, oDebitReceiptItems, out err);
            if (result > 0)
            {
                DisplayMessage("Successfully allocated", 3);
                Clear_Data();
            }
            else
            {
                DisplayMessage(err, 5);
            }
        }

        private bool PaymentValueChange(string recNo, bool isBind)
        {
            bool status = true;
            RecieptItem _paramRecDetails = new RecieptItem();
            _paramRecDetails.Sard_receipt_no = recNo;
            _list = new List<RecieptItem>();
            _list = CHNLSVC.Sales.GetReceiptDetails(_paramRecDetails);

            //if (oDebitInvoices.Count == 0)
            //{
            //    RecieptItem oReduceItem = _list.Find(x => x.Sard_pay_tp == cmdPayTypeAlloca.SelectedValue.ToString());
            //    oReduceItem.Sard_settle_amt = oReduceItem.Sard_settle_amt - Convert.ToDecimal(txtAmountAlloca.Text);
            //    if (oReduceItem.Sard_settle_amt < 0)
            //    {
            //        status = false;
            //        return status;
            //    }
            //}
            //else
            {
                foreach (gvRegItems item in oDebitInvoices)
                {
                    if (_list.FindAll(x => x.Sard_pay_tp == item.col_PayType).Count > 0)
                    {
                        RecieptItem oReduceItem = _list.Find(x => x.Sard_pay_tp == item.col_PayType);
                        oReduceItem.Sard_settle_amt = oReduceItem.Sard_settle_amt - item.col_Amount;
                        if (oReduceItem.Sard_settle_amt < 0)
                        {
                            status = false;
                            return status;
                        }
                    }
                }
            }
            if (isBind)
            {
                ucPayModes1.RecieptItemList = _list;
                ucPayModes1.MainGrid.DataSource = _list;
                ucPayModes1.MainGrid.DataBind();

                //if (_list != null && _list.Count > 0)
                //{
                //    List<string> paymnetType = _list.Select(x => x.Sard_pay_tp).ToList();
                //    if (paymnetType != null && paymnetType.Count > 0)
                //    {
                //        cmdPayTypeAlloca.DataSource = paymnetType;
                //        cmdPayTypeAlloca.DataBind();
                //    }
                //}
            }

            return status;
        }

        protected void cmdPayTypeAlloca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmdPayTypeAlloca.SelectedValue.ToString().ToUpper() == "CHEQUE")
            {
                cmdCheqNums.Enabled = true;
            }
            else
            {
                cmdCheqNums.Enabled = false;
            }
        }

        protected void lbtnDateS_Click(object sender, EventArgs e)
        {
            string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ReceiptDate);
            DataTable _result = new DataTable();
            if (!chkUnAllocated.Checked)
            {
                _result = CHNLSVC.CommonSearch.GetReceiptsDate(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, Convert.ToDateTime(txtFDate.Text.ToString()), Convert.ToDateTime(txtTDate.Text.ToString()));
            }
            else
            {
                _result = CHNLSVC.CommonSearch.SEARCH_RECEIPT_UNALO(SearchParams, ddlSearchbykey.SelectedItem.Text, txtSearchbyword.Text, Convert.ToDateTime(txtFDate.Text.ToString()), Convert.ToDateTime(txtTDate.Text.ToString()));
            }
            grdResult.DataSource = _result;
            grdResult.DataBind();
            lblvalue.Text = "ReceiptDate";
            ViewState["SEARCH"] = _result;
            mpSearchAdance.Show();
            txtSearchbyword.Focus();
        }

        protected void lbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GlbReportType"] = "";
                Session["documntNo"] = txtRecNo.Text;
                Session["GlbReportName"] = Session["UserCompanyCode"].ToString() == "SGL" ? "ReceiptPrints_SGL.rpt" : "ReceiptPrints_n.rpt";
                BaseCls.GlbReportHeading = "Receipt Print Report";

                string name = Session["GlbReportName"].ToString().Split('.')[0].ToString().Replace("_", "");
                string targetFileName = Server.MapPath("~\\Temp\\") + Session["UserID"].ToString() + ".pdf";

                clsSales obj = new clsSales();
                obj.Receipt_print_n(Session["documntNo"].ToString());
                if (Session["UserCompanyCode"].ToString() == "SGL")
                {
                    PrintPDF(targetFileName, obj.recreport_SGL);
                }
                else if (Session["UserCompanyCode"].ToString() == "AOA")
                {
                    PrintPDF(targetFileName, obj.recreport_AOA);
                }
                else PrintPDF(targetFileName, obj.recreport1_n);

                string url = "<script>window.open('/Temp/" + Session["UserID"].ToString() + ".pdf','_blank');</script>";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "alert", url, false);

                //2016-08-30 Sanjeewa
                Reprint_Docs _reprint = new Reprint_Docs();

                _reprint.Drp_tp = "CS";
                _reprint.Drp_doc_no = txtRecNo.Text;
                _reprint.Drp_doc_dt = Convert.ToDateTime(dtpRecDate.Text);
                _reprint.Drp_loc = Session["UserDefLoca"].ToString();
                _reprint.Drp_req_dt = Convert.ToDateTime(dtpRecDate.Text);
                _reprint.Drp_is_add_pending = 0;
                _reprint.Drp_stus = "A";
                _reprint.Drp_app_dt = Convert.ToDateTime(dtpRecDate.Text);
                _reprint.Drp_can_dt = Convert.ToDateTime(dtpRecDate.Text);
                _reprint.Drp_rej_dt = Convert.ToDateTime(dtpRecDate.Text);
                _reprint.Drp_stus_change_by = "";
                _reprint.Drp_printed = 1;
                _reprint.Drp_print_dt = Convert.ToDateTime(dtpRecDate.Text);
                _reprint.Drp_reason = "";

                int X = CHNLSVC.General.SaveReprintDocRequest(_reprint);

                if (X == 9999)
                {
                    DisplayMessage("Can not add request, Pending request exists", 1);
                    return;
                }
            }
            catch (Exception ex)
            {
                CHNLSVC.MsgPortal.SaveReportErrorLog("Receip tEntry Print", "ReceiptEntry", ex.Message, Session["UserID"].ToString());
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

        protected void lbtnExcelUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCusCode.Text))
            {
                DisplayMessage("Please select the customer", 2);
                return;
            }
            if (string.IsNullOrEmpty(txtRecType.Text))
            {
                DisplayMessage("Please select rceipt type", 2);
                return;
            }

            DisplayMessage("Please unselect blank cell/cells which you have selected in excel sheet", 1);
            lblalert.Text = "";
            lblsuccess.Text = "";
            lblsuccess.Visible = false;
            lblalert.Visible = false;
            divUpcompleted.Visible = false;
            excelUpload.Show();
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

        public DataTable[] LoadData2(string FileName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataTable Tax = new DataTable();
            using (OleDbConnection cn2 = new OleDbConnection { ConnectionString = ConnectionString(FileName, "Yes") })
            {
                try
                {
                    cn2.Open();
                    OleDbCommand cmdExcel = new OleDbCommand();
                    OleDbDataAdapter oda = new OleDbDataAdapter();
                    DataTable dtExcelSchema;
                    cmdExcel.Connection = cn2;

                    dtExcelSchema = cn2.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                    cn2.Close();

                    //Read Data from First Sheet
                    cn2.Open();
                    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                    oda.SelectCommand = cmdExcel;
                    oda.Fill(Tax);
                }
                catch (Exception ex)
                {

                    lblalert.Visible = true;
                    lblalert.Text = "Invalid data found from the excel sheet. Please check data...." + ex.Message;
                    excelUpload.Show();
                    return new DataTable[] { Tax };
                }
                return new DataTable[] { Tax };
            }
        }

        protected void btnprocess_Click(object sender, EventArgs e)
        {
            string invNo = string.Empty;
            decimal _curBalance = 0;
            DataTable[] GetExecelTbl = LoadData2(Session["FilePath"].ToString());
            DataTable paymentDetails = new DataTable();
            decimal amuntLbl = 0;

            List<string> invoicesNotLoggedPcList = new List<string>(); //Added By Dulaj 2018/Aug/22

            if (GetExecelTbl != null)
            {
                if (GetExecelTbl[0].Rows.Count > 0)
                {
                    for (int i = 0; i < GetExecelTbl[0].Rows.Count; i++)
                    {
                        invNo = GetExecelTbl[0].Rows[i][0].ToString().Trim();
                        try
                        {
                            ReceiptEntryExcel _inv = new ReceiptEntryExcel();
                            _inv.Invoice = GetExecelTbl[0].Rows[i][0].ToString();
                            List<InvoiceCustomer> cusList = CHNLSVC.General.getInvoiceCustomer(invNo);
                            if (cusList.Count == 0)
                            {
                                divUpcompleted.Visible = false;
                                lblalert.Visible = true;
                                lblalert.Text = "Invalide Invoice no: " + invNo;
                                excelUpload.Show();
                                return;
                            }
                            if (cusList.FirstOrDefault().SAH_PC != Session["UserDefProf"].ToString())
                            {
                                //Editd By Dulaj 2108/Aug/22
                                //lblalert.Visible = true;
                                //lblalert.Text = "Selected invoice no : " + invNo + " - (Invoice Profit center - " + cusList.FirstOrDefault().SAH_PC + ") mismatch with logged profit center";
                                //excelUpload.Show();
                                //return;
                                //
                                invoicesNotLoggedPcList.Add(invNo);
                            }
                            if (txtCusCode.Text != cusList.FirstOrDefault().SAH_CUS_CD)
                            {
                                divUpcompleted.Visible = false;
                                lblalert.Visible = true;
                                lblalert.Text = "Invoice no " + invNo + " not related to " + txtCusCode.Text + " Customer";
                                excelUpload.Show();
                                return;
                            }
                            if (invoicesNotLoggedPcList.Count == 0)
                            {
                                //_curBalance = CHNLSVC.Sales.GetOutInvAmt(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text, invNo);
                                //if (_curBalance < Convert.ToDecimal(GetExecelTbl[0].Rows[i][1].ToString()))
                                //{
                                //    lblalert.Visible = true;
                                //    lblalert.Text = "Cannot proceed : Outstanding balance is " + _curBalance + " and settlement amount is " + Convert.ToDecimal(GetExecelTbl[0].Rows[i][1].ToString()) + " for the invoice " + invNo;
                                //    excelUpload.Show();
                                //}
                                _curBalance = CHNLSVC.Sales.GetOutInvAmt(Session["UserCompanyCode"].ToString(), Session["UserDefProf"].ToString(), txtCusCode.Text, invNo);
                                if (_curBalance <= 0)
                                {
                                    lblalert.Visible = true;
                                    lblalert.Text = "Cannot proceed : Outstanding balance is invalid" + _curBalance + " for the invoice " + invNo;
                                    excelUpload.Show();
                                }
                                if (_curBalance < Convert.ToDecimal(GetExecelTbl[0].Rows[i][1].ToString()))
                                {
                                    InvOutstanding _invout = new InvOutstanding();
                                    _invout.invoiceNo = invNo;
                                    _invout.amount = Convert.ToDecimal(GetExecelTbl[0].Rows[i][1].ToString());
                                    _outstandingList.Add(_invout);
                                }
                            }
                            else
                            {
                                _curBalance = CHNLSVC.Sales.GetOutInvAmt(Session["UserCompanyCode"].ToString(), "All", txtCusCode.Text, invNo);
                                if (_curBalance <= 0)
                                {                                    
                                    lblalert.Visible = true;
                                    lblalert.Text = "Cannot proceed : Outstanding balance is invalid" + _curBalance + " for the invoice " + invNo;
                                    excelUpload.Show();
                                }
                                if (_curBalance < Convert.ToDecimal(GetExecelTbl[0].Rows[i][1].ToString()))
                                {
                                    InvOutstanding _invout = new InvOutstanding();
                                    _invout.invoiceNo = invNo;
                                    _invout.amount = Convert.ToDecimal(GetExecelTbl[0].Rows[i][1].ToString());
                                    _outstandingList.Add(_invout);
                                }
                            }
                            _inv.Amount = Convert.ToDecimal(GetExecelTbl[0].Rows[i][1].ToString());
                            _inv.ReceiptType = txtRecType.Text;

                            _listReceiptEntry.Add(_inv);
                            amuntLbl = amuntLbl + _inv.Amount;
                            ViewState["RecieptEntryDetails"] = _listReceiptEntry;
                            Session["RecieptEntryDetailsExcel"] = _listReceiptEntry;
                        }
                        catch (Exception ex)
                        {
                            DisplayMessage("Excel  Data Invalid Please check Excel File and Upload ", 2);
                            lblalert.Visible = true;
                            lblalert.Text = "Excel Data Invalid Please check Excel File and Upload - " + ex.Message;
                            excelUpload.Show();
                            return;
                        }
                    }
                    amountLabl.Value = amuntLbl.ToString();
                    if (invoicesNotLoggedPcList.Count > 0)
                    {
                        lblsuccess.Text = "Successfully added invoices data ...!";
                        lblsuccess.Visible = true;
                        lblalert.Visible = false;
                        divUpcompleted.Visible = false;
                        ModalPopupExtenderExcel.Show();
                        Session["ExcelMultiplePc"] = 1;
                    }
                    else
                    {
                        lblsuccess.Text = "Successfully added invoices data ...!";
                        lblsuccess.Visible = true;
                        LoadRecieptGrid(_listReceiptEntry, amuntLbl);
                        lblalert.Visible = false;
                        divUpcompleted.Visible = false;
                        excelUpload.Show();


                        ViewState["_listReceiptEntry"] = null;
                        Session["RecieptEntryDetailsExcel"] = null;
                        _listReceiptEntry = null;
                    }


                }
            }
        }
        public void LoadRecieptGrid(List<ReceiptEntryExcel> RecieptItemList, decimal amuntLbl)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("SARD_PAY_TP");
            dt.Columns.Add("SARD_INV_NO");
            dt.Columns.Add("sard_chq_bank_cd");
            dt.Columns.Add("sard_chq_branch");
            dt.Columns.Add("sard_cc_tp");
            dt.Columns.Add("sard_anal_3");
            dt.Columns.Add("sard_settle_amt", typeof(decimal));
            dt.Columns.Add("Sard_ref_no");
            dt.Columns.Add("sard_anal_1");
            dt.Columns.Add("sard_anal_4");
            dt.Columns.Add("Sard_cc_period");
            dt.Columns.Add("Sard_deposit_bank_cd");

            foreach (ReceiptEntryExcel ri in RecieptItemList)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "";
                dr[1] = ri.Invoice;
                dr[2] = "";
                dr[3] = "";
                dr[4] = "";
                dr[5] = "0";
                dr[6] = ri.Amount.ToString();
                dr[7] = "";
                dr[8] = "";
                dr[9] = "0";
                dr[11] = "0";
                dt.Rows.Add(dr);

                // RecieptItem recieptItem = new RecieptItem();
                // recieptItem.Sard_settle_amt = ri.Amount;
                // recieptItem.Sard_inv_no = ri.Invoice;
                // recieptItem.Sard_receipt_no = ri.ReceiptType;
                ucPayModes1.RecieptEntryDetails.Add(ri);
            }

            //ViewState["Payments"] = dt;
            ucPayModes1.lblBalanceAmountPub.Text = amuntLbl.ToString();
            // ucPayModes1.MainGrid.AutoGenerateColumns = false;
            // ucPayModes1.MainGrid.DataSource = dt;
            //  ucPayModes1.MainGrid.DataBind();
            ucPayModes1.TotalAmount = amuntLbl;
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPayment.Text))
            {
                DisplayMessage("Please add customer payment amount before excel upload", 1);
            }
            lblalert.Visible = false;
            if (fileupexcelupload.HasFile)
            {
                string FileName = Path.GetFileName(fileupexcelupload.PostedFile.FileName);
                string Extension = Path.GetExtension(fileupexcelupload.PostedFile.FileName);

                if (Extension != ".xls" && Extension != ".XLS" && Extension != ".xlsx" && Extension != ".XLSX")
                {
                    lblsuccess.Visible = false;
                    lblsuccess.Text = string.Empty;

                    lblalert.Visible = true;
                    lblalert.Text = "Please select a valid excel (.xls) file";
                    excelUpload.Show();
                    return;
                }
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath(FolderPath + FileName);
                fileupexcelupload.SaveAs(FilePath);
                Session["FilePath"] = FilePath;
                divUpcompleted.Visible = true;
                DisplayMessage("Excel file upload completed. Do you want to process?", 2);
                excelUpload.Show();
                Import_To_Grid(FilePath, Extension, null);
            }
            else
            {
                lblalert.Visible = true;
                lblalert.Text = "Please select the correct upload file path";
                DisplayMessage("Please select the correct upload file path", 2);
                excelUpload.Show();
            }
        }

        private DataTable[] Import_To_Grid(string FilePath, string Extension, string isHDR)
        {
            DataTable dt = new DataTable();
            try
            {
                string conStr = "";
                switch (Extension)
                {
                    case ".xls":
                        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx":
                        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                }
                conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();


                cmdExcel.Connection = connExcel;
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();


                connExcel.Open();
                cmdExcel.CommandText = "SELECT F1,F2,F3,F4,F5,F6,F7,F8 From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);

                string sessiontype1 = string.Empty;
                string sessiontype2 = string.Empty;

                sessiontype1 = (string)Session["ROUTES"];
                sessiontype2 = (string)Session["EXCEL_SCH"];
                connExcel.Close();
            }
            catch (Exception ex)
            {
                return new DataTable[] { dt };
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myScript", "showStickyErrorToast('Error Occurred while processing !!!')", true);
            }
            return new DataTable[] { dt };
        }

        protected void btnClose3_Click(object sender, EventArgs e)
        {
            lblalert.Text = "";
            lblsuccess.Text = "";
            lblsuccess.Visible = false;
            lblalert.Visible = false;
            divUpcompleted.Visible = false;
        }

        protected void btnDebOK_Click(object sender, EventArgs e)
        {
            btnPayment_Click(null, null);
        }

        protected void btnDebNO_Click(object sender, EventArgs e)
        {
            txtPayment.Focus();
            return;
        }
        protected void btnConfYes_ClickExcel(object sender, EventArgs e)
        {
            ModalPopupExtenderExcel.Hide();
            lblsuccess.Text = "Successfully added invoices data ...!";
            lblsuccess.Visible = true;
            _listReceiptEntry = Session["RecieptEntryDetailsExcel"] as List<ReceiptEntryExcel>;
            LoadRecieptGrid(_listReceiptEntry, Convert.ToDecimal(amountLabl.Value));
            lblalert.Visible = false;
            divUpcompleted.Visible = false;
            excelUpload.Show();
            ViewState["_listReceiptEntry"] = null;
            Session["RecieptEntryDetailsExcel"] = null;
            _listReceiptEntry = null;
            ucPayModes1.LoadPayModes();

        }

    }
    public class gvInvoiceItems
    {
        public string col_itmItem { get; set; }
        public string col_itmDesc { get; set; }
        public string col_itmModel { get; set; }
        public string col_itmStatus { get; set; }
        public string col_itmStatus_Desc { get; set; }
        public string col_itmSerial { get; set; }
        public string col_itmOthSerial { get; set; }
        public string colpb { get; set; }
        public string colpblvl { get; set; }
        public decimal colQty { get; set; }
        public decimal colRate { get; set; }
        public decimal colamt { get; set; }
        public decimal colTax { get; set; }
        public decimal colpay { get; set; }
    }
    public class gvRegItems
    {
        public string col_regInv { get; set; }
        public string col_regCus { get; set; }
        public string col_regItem { get; set; }
        public string col_regModel { get; set; }
        public string col_regBrand { get; set; }
        public string col_regDis { get; set; }
        public string col_regPro { get; set; }
        public string col_regEngine { get; set; }
        public string col_regChasis { get; set; }
        public decimal col_regVal { get; set; }
        public decimal col_Amount { get; set; }
        public string col_PayType { get; set; }
    }
    public class dgvInsItems
    {
        public string col_insInv { get; set; }
        public string col_insCus { get; set; }
        public string col_insItem { get; set; }
        public string col_insModel { get; set; }
        public string col_insDistrict { get; set; }
        public string col_insPro { get; set; }
        public string col_insCom { get; set; }
        public string col_insPol { get; set; }
        public string col_insEngine { get; set; }
        public string col_insChasis { get; set; }
        public decimal col_insVal { get; set; }
    }
    public class InvOutstanding
    {
        public string invoiceNo{get;set;}
        public decimal amount { get; set; }
        public InvOutstanding() { }
    }

}