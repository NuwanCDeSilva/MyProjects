
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects;
using System.Data;
using FF.BusinessObjects.General;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.BITool;
using FF.BusinessObjects.Financial;

namespace FF.Interfaces
{
    [ServiceContract]


    public interface ISales
    {
        //kapila
        [OperationContract]
        List<PriceDetailRef> GetPriceForExchange(string _company, string _profitCenter, string _invType, string _priceBook, string _priceLevel, string _customer, string _item, decimal _qty, DateTime _currentDate);
        //kapila
        [OperationContract]
        List<MasterInvoiceType> GetInvoiceTypeByCode(string _code);
        //kapila
        [OperationContract]
        DataTable SP_CHECK_MST_SYS_PARA_MRN(string _com, string _pc, string _loc, string _item, decimal _qty);
        //kapila
        [OperationContract]
        DataTable GetItemByJobRegNo(string _regNo);
        //kapila
        [OperationContract]
        DataTable GetReqAppByType(string _com, string _Pc, string _type);
        //kapila
        [OperationContract]
        REF_ITM_CATE4 GetItemCate4(string _cd);
        //kapila
        [OperationContract]
        REF_ITM_CATE5 GetItemCate5(string _cd);
        //kapila
        [OperationContract]
        DataTable GetSchDetByPriceCirc(string _circ, string _type);
        //kapila
        [OperationContract]
        DataTable GET_SI_ITEMS(string _si, Int32 _wfoc);
        //kapila
        [OperationContract]
        DataTable GET_SI_4_Price(string _com, string _si,DateTime _from,DateTime _to, string _item, string _cat1, string _cat2, string _model);
        //kapila
        [OperationContract]
        List<MasterItemTax> GetTax_strucbase(string _company, string _item, string _status, string _taxCode, string _taxRateCode, string _struc);
        //kapila
        [OperationContract]
        DataTable getProVouRdmPb(Int32 _seq);
        //kapila
        [OperationContract]
        DataTable getReqAppByCrNo(string _com, string _crno, string _anal5);

        //kapila
        [OperationContract]
        DataTable GetPromoVoucherNo(string _company, string _cust, string _nic, string _mobi, DateTime _date, int _vouNo, string gvcode = null);
        //kapila
        [OperationContract]
        Int32 DeleteTempPromoVoucherRedeemPB(string CreateUser);
        //kapila
        [OperationContract]
        Int32 SaveTempPromoVoucherRedeemPB(List<PromoVouRedeemPB> _lstRdmPB);
        //kapila
        [OperationContract]
        DataTable GetCreditInvoices(DateTime _from, DateTime _to, string _cust, string _com, Int32 _isall, Int32 _outs);
        //kapila
        [OperationContract]
        Boolean IsExchangeReqFound(string _ser, Int32 _isinv);
        //kapila
        [OperationContract]
        Int32 SavePVoucherPages(List<GiftVoucherPages> _voucherList);

        //kapila
        [OperationContract]
        DataTable GetInvoiceAdj(string _invoice);
        //kapila
        [OperationContract]
        Int32 Update_Item_Tax(string _com, string _pc, DateTime _date);

        //kapila
        [OperationContract]
        Int16 UpdateQuotationReserve(string _com, string _loc, string _QutNo, string _itm, string _Ser, string _newSer, string _newser2, out string _errmsg);

        //kapila
        [OperationContract]
        DataTable SP_CHECK_MST_SYS_PARA(string _com, string _pc, string _loc, string _item);        
        
        //kapila
        [OperationContract]
        DataTable GetTransPBSupplier(string _com, string _pb, string _level);
        
        //kapila
        [OperationContract]
        Int32 SaveVehRegCompany(List<VehicleRegCompany> _RegComp);

        //kapila
        [OperationContract]
        DataTable GetMailLocations(string _pb, string _lvl);

        //kapila
        [OperationContract]
        DataTable GetMailPBLevels(string _promocode);

        //kapila
        [OperationContract]
        Int16 CancelSerialPrice(List<PriceSerialRef> _priceRef);

        //kapila
        [OperationContract]
        Int32 SaveMyAbansDetails(MyAbans _myab, MasterBusinessEntity _businessEntity, GroupBussinessEntity _groupCus, LoyaltyMemeber _loyal, Boolean _isExist);


        //kapila
        [OperationContract]
        Int32 UpdateAccountManager(List<HpAccount> accNoList, string _newmgrcd, string _user);

        //kapila
        [OperationContract]
        DataTable GetLoyaltyMemberByCardNo(string _cardNo);

        //kapila
        [OperationContract]
        DataTable GetSalesHdrByReq(string _com, string _req);

        //kapila
        [OperationContract]
        int Update_veh_DocReg_stus(Int32 seq, DateTime recDate, string FinRemark, string recBy, Int32 _stus);
        //kapila
        [OperationContract]
        DataTable GetinvBySer(string _com, string _loc, string _ser, string _war);

        //kapila
        [OperationContract]
        DataTable getSubLocationByCode(string _com, string _mloc, string _sloc);

   

        //kapila
        [OperationContract]
        bool CheckPromoVoucherParaNo(string _company, string _vou, string _tp, string _cd, Int32 _prd, out string _err);
        //kapila
        [OperationContract]
        DataTable get_DCN_receipt(string _company, string _loc, string _qno);
        //kapila
        [OperationContract]
        DataTable get_DCN_DP_Cust(string _company, string _loc, DateTime _date);

        //kapila
        [OperationContract]
        DataTable GetVehInsuAllowDCNItems(string _company, string _loc, DateTime _date);

        //kapila
        [OperationContract]
        List<InvoiceItem> GetVehInsuAllowCredInv(string _company, string _profitcenter, DateTime _date);

        //kapila
        [OperationContract]
        DataTable GetSalesDetailsByLine(string _invNo, Int32 _line);

        //kapila
        [OperationContract]
        List<RecieptItem> GetReceiptItemsByInvoice(string _inv);

        //kapila
        [OperationContract]
        Int32 SaveVehInsuarance(VehicleInsuarance _Insuarance);

        //kapila
        [OperationContract]
        DataTable GetHPSalesDet(string _com, DateTime _frmDt, DateTime _toDt, string _cus, string _invNo);

        //kapila
        [OperationContract]
        DataTable GetCustomerHPSalesDet(string _com, DateTime _frmDt, DateTime _toDt, string _cus, string _invNo);

        //kapila
        [OperationContract]
        Int32 ExchangeIssueCancelation(string _com, string _loc, string _docNo, string _user, out string _message);

        //kapila
        [OperationContract]
        DataTable getReqHdrByReqNo(string _com, string _pc, string _req);

        //kapila
        [OperationContract]
        Boolean IsMainItemReplace(string _item);

        //kapila
        [OperationContract]
        Int32 SaveGiftVoucherPages(List<GiftVoucherPages> _voucherList);

        //Nadeeka
        [OperationContract]
        Int32 UpdateVouTransfer(List<GiftVoucherPages> _voucherList);



        //kapila
        [OperationContract]
        Int32 UpdateBulkToDipositBank(string _com, string _pc, string _ref, string _curBankCd, string _dipBankCd, string _modby);

        //kapila
        [OperationContract]
        DataTable GetReceiptByRecNo(string _recNo);

        #region Quotations

        [OperationContract]
        Int32 Quotation_save(QuotationHeader header, List<QoutationDetails> det_line_list, MasterAutoNumber _masterAutoNumber, List<QuotationSerial> _saveQuoSer, InventoryRequest _inventoryRequest, List<QuotationSerial> _saveQuoSerSCM, MasterAutoNumber _mastAutoNo, Boolean _isSer, ReptPickHeader _pickHeader, List<ReptPickSerials> _pickSerials, out string docno);
        //[OperationContract]
        //Int32 Quotation_save(QuotationHeader header, List<QoutationDetails> det_line_list);
        [OperationContract]
        Int32 Save_QuotationDET(List<QoutationDetails> det_line_list);
        [OperationContract]
        Int32 Delete_Quotation_DET(string quotation_no);

        [OperationContract]
        Int32 Update_Quotation_HDR_status(string quotation_no, string newStatus);

        [OperationContract]
        QuotationHeader Get_Quotation_HDR(string quotation_no);

        [OperationContract]
        Int32 Save_QuotationHDR(QuotationHeader header);

        [OperationContract]
        List<QoutationDetails> Get_all_linesForQoutation(string qoutation_no);

        [OperationContract]
        List<QuotationHeader> Get_all_Quotations(string company, string profCenter, string supCD, string status, string fromDT, string toDT);

        //kapila
        [OperationContract]
        Int16 QuotationCancelProcess(string _com, string _pc, string _QutNo, string _jobno, string _whcode,string _user,string _sess, out string _errmsg);

        #endregion

        #region SalesInvoice

        /// <summary>
        /// Get Invoice header details.
        /// Code By : M.Geeganage on 18/04/2012  
        /// </summary>
        /// <param name="_invoiceNo"></param>
        /// <returns></returns>
        [OperationContract]
        InvoiceHeader GetInvoiceHeaderDetails(string _invoiceNo);

        [OperationContract]
        List<InvoiceItem> GetInvoiceHeaderDetailsList(string _invoiceNo);
        /// <summary>
        /// Get Pending Invoice details.
        /// Code By : Shani on 28/04/2012  
        /// </summary>
        /// <param name="prof_center"></param>
        /// <param name="company"></param>
        /// <param name="from_dt"></param>
        /// <param name="to_date"></param>
        /// <returns></returns>

        //can pass null to both  from_dt & to_date
        [OperationContract]
        DataTable GetPendingInvoiceDetails(string prof_center, string company, string from_dt, string to_date, string _del_loc);

        //Chamal 09-02-2013
        [OperationContract]
        DataTable GetPendingInvoicesToDO(string _company, DateTime _fromDate, DateTime _toDate, string _delLoc, string _custCode, string _invoiceNo, Int32 _delanyloc);

        #endregion


        // Nadeeka 09-11-2015
        [OperationContract]
        DataTable GetInvoicesToChangeCust(string _company, DateTime _fromDate, DateTime _toDate, string _delLoc, string _custCode, string _invoiceNo, Int32 _delanyloc);
        // Nadeeka 09-11-2015
        [OperationContract]
        Int32 Update_CustomerName(string _invNo, string _custCode, string _custName, string _custAdd1, string _custAdd2, string _com, string _user);

        //kapila
        [OperationContract]
        DataTable PrintExtraAddBankDocs(string com, string _accNo, DateTime _from, DateTime _to, string _user, string _docType);

        //kapila
        [OperationContract]
        List<ReptPickSerials> GetInvoiceSerial_Rep(string _company, string _location, string _user, string _session, string _defBin, string _invoice, int _baseRefline);

        //kapila
        [OperationContract]
        Int32 SavePromotorCommDef(List<HPPrmotorCommDef> _Promolst, out string _msg);
                
        // Nadeeka 21-05-2015
        [OperationContract]
        Int32 CheckAssignChannel(string p_schannel, string p_com);

        // Nadeeka 21-05-2015
        [OperationContract]
        List<HpAccount> GetActiveAccount(string _com, string _ac, DateTime _date);

        // Nadeeka 05-11-2015
        [OperationContract]
        int Update_Veh_submit_Date(DataTable _submitDoc);

        // Nadeeka 21-05-2015
        [OperationContract]
        Int32 SendWarrantyRepMail(string _com, string _pc, string _acc);

        // Nadeeka 21-05-2015
        [OperationContract]
        Int32 SaveMsgForExpiryReceipt(string _smsDocType, string _company, string _location, string _documentno, string _user, DateTime _expDate);


        // Nadeeka 17-8-2015
        [OperationContract]
        Decimal Get_Acc_Arrears(string _acc, DateTime _date, string _issame_date);

        // Nadeeka 21-05-2015
        [OperationContract]
        Int32 CheckAssignChannelDef(string p_schannel, string p_com);

        // Nadeeka 20-05-2015
        [OperationContract]
        List<SAR_DOC_CHANNEL_PRICE_DEFN> GetPriceDefinitionByBookAndLevelSubChannel(string _company, string _book, string _level, string _invoiceType, string _subchannel);


        // Nadeeka 21-05-2015
        [OperationContract]
        Int32 RemovePriceAccessSubChannel(string p_schannel, string p_invTp, string p_lvl, string p_com, string p_pb, string p_usr);

        // Nadeeka 21-05-2015
        // [OperationContract]
        //  Int32 UpdateVouTransfer(List<GiftVoucherPages> _voucherList);


        // Nadeeka 20-05-2015
        [OperationContract]
        Int32 SavePcPriceDefinitionsChannel(List<SAR_DOC_CHANNEL_PRICE_DEFN> _priceDef);


        //Nadeeka 17-04-2015
        [OperationContract]
        //    DataTable GetPendingQuotationToDO(string _company, DateTime _fromDate, DateTime _toDate, string _custCode, string _QuoNo, string _sts);
        DataTable GetPendingQuotationToDO(string _company, DateTime _fromDate, DateTime _toDate, string _custCode, string _QuoNo, string _sts, string _pc);
        //Nadeeka 17-04-2015
        [OperationContract]
        // DataTable GetPendingDoToInv(string _company, DateTime _fromDate, DateTime _toDate, string _doNo, string _sts);
        DataTable GetPendingDoToInv(string _company, DateTime _fromDate, DateTime _toDate, string _doNo, string _sts, string _pc);

        //Nadeeka 10-09-2015
        [OperationContract]
        DataTable GetDeliveredQuotation(string _company, string _quo);

        //Nadeeka 25-03-2013
        [OperationContract]
        List<QoutationDetails> GetAllQuotationItemList(string _QuoNo);

        //Nadeeka 17-04-2015
        [OperationContract]
        DataTable GetMsgColumn();

        //kapila
        [OperationContract]
        List<HpAccount> GetHP_Accounts_Adj(string com, string pc, string seqNo, string status, DateTime _date);

        //kapila
        [OperationContract]
        DataTable GetServiceAgentbyLoc(string _com, string _cd);

        //kapila
        [OperationContract]
        Int32 SaveCustomerPriorityLevel(List<MST_BUSPRIT_LVL> _custLevel, string _cusCode);

        //kapila
        [OperationContract]
        DataTable GetESDManagerEPF(string _company, string _profitcenter, DateTime _fromDate, DateTime _toDate);

        //kapila
        [OperationContract]
        MasterBusinessEntity GetCustomerProfileByNIC(string nic);



        //kapila
        [OperationContract]
        Int32 SaveTownMaster(MasterTown _mstTown, out string _refNo);

        //kapila 20/8/2014
        [OperationContract]
        DataTable GetCustInBOCProject(string _company, string _loc, string _custID);

   

        //kapila 20/8/14
        [OperationContract]
        Int32 SaveBulkSaleInvReservation(MasterBusinessEntity _businessEntity, Int32 _seqno, string _batch_no, string _com, string _loc, string _itm_cd, string _itm_stus, string _ser1, string _ser2, string _ref_no, string _last_modi_by, string _rem, DateTime _date, Int32 _isNIC, Int32 _isPP, Int32 _isPID, Int32 _isDL, Int32 _isBRNo, Int32 _isIID, Int32 _isFoundBOCProj, string _recno, out string customerCD);

        //kapila
        [OperationContract]
        DataTable getReceiptByEngNo(string _com, string _pc, string _eng);

        //kapila
        [OperationContract]
        Int32 UpdateBulkSaleInvReservation(MasterBusinessEntity _businessEntity, Int32 _seqno, string _batch_no, string _com, string _loc, string _itm_cd, string _itm_stus, string _ser1, string _ser2, string _ref_no, string _last_modi_by, Int32 _seqno2, string _itm_cd2, string _itm_stus2, string _ser12, string _ser22, string _ref_no2, string _rem, DateTime _date, Int32 _isNIC, Int32 _isPP, Int32 _isPID, Int32 _isDL, Int32 _isBRNo, Int32 _isIID, string _recno, out string customerCD);

        //Written By Prabhath on 28/04/2012
        [OperationContract]
        List<InvoiceItem> GetAllSaleDocumentItemList(string _company, string _profitCenter, string _documentType, string _invoiceNo, string _status);

        //Written By Chamal on 12/06/2012 using DO
        [OperationContract]
        InvoiceHeader GetPendingInvoiceHeader(string _company, string _profitCenter, string _documentType, string _invoiceNo, string _status);

        [OperationContract]
        DataTable GetAllSaleDocumentItemTable(string _company, string _profitCenter, string _documentType, string _invoiceNo, string _status);

        //Written By Prabhath on 02/05/2012
        [OperationContract]
        MasterProfitCenter GetProfitCenter(string _company, string _profitCenter);
        [OperationContract]
        List<MasterProfitCenter> GetProfitCenterList(string _company, string _profitCenter);
                

        [OperationContract]
        List<MasterProfitCenter> GetProfitCenterListbyLike(string _company, string _profitCenter, string _description);

        //Written By Prabhath on 04/05/2012
        [OperationContract]
        PriceBookLevelRef GetPriceLevel(string _company, string _book, string _level);

        //Written By Prabhath on 04/05/2012
        [OperationContract]
        List<PriceBookLevelRef> GetPriceLevelList(string _company, string _book, string _level);

        //Written By Prabhath on 04/05/2012
        [OperationContract]
        DataTable GetPriceLevelTable(string _company, string _book, string _level);

        //Written By Prabhath on 04/05/2012
        [OperationContract]
        DataTable GetPriceBookTable(string _company, string _book);

        //kapila
        [OperationContract]
        DataTable GetManagerEPF(string _company, string _profitcenter);

        //kapila
        [OperationContract]
        DataTable get_Dep_Bank_Name(string _com, string _pc, string _paytp, string _acc);

        //kapila
        [OperationContract]
        Int16 UpdateBusDesig(string _code, string _rmk);

        //kapila
        [OperationContract]
        DataTable GetESDSrDet(string _com, string _PC, string _epf);

        //kapila
        [OperationContract]
        DataTable Get_Manager_Issue_rec(string AccountNo);

        //kapila
        [OperationContract]
        DataTable GetReceiptDetByChqNo(string _chqno, Int32 _isAll);

        //kapila
        [OperationContract]
        DataTable PrintNotRecManIssues(string _com, string _pc, DateTime _from, DateTime _to);

        //kapila
        [OperationContract]
        Int32 CreateRefundByAdj(RecieptHeader _refundHdr, Deposit_Bank_Pc_wise objDeposit, List<RecieptItem> _refundItm, MasterAutoNumber _receiptAuto, List<ReceiptItemDetails> _resItmDet, RemitanceSummaryDetail _remsumdet, out string _refNo);

        //kapila
        [OperationContract]
        Int32 UpdateManagerIssueReceipt(string _com, string _pc, string _recno, string _user);

        //kapila
        [OperationContract]
        DataTable get_Def_dep_Bank(string _com, string _pc, string _paytp);

        //kapila
        [OperationContract]
        DataTable Get_Manager_receive_rec(string AccountNo);

        //Written By Prabhath on 04/05/2012
        [OperationContract]
        PriceBookRef GetPriceBook(string _company, string _book);

        //Written By Prabhath on 04/05/2012
        [OperationContract]
        List<PriceBookRef> GetPriceBooklist(string _company);
        //Dilshan 29/09/2017
        [OperationContract]
        List<GetPCCategory> GetPCCatlist(string _company);
        //[OperationContract]
        //DataTable GetPCCatlist(string _company);

        //kapila
        [OperationContract]
        DataTable GetGenDiscByCirc(string _circ);

        [OperationContract]
        List<PriceDetailRef> GetPrice(string _company, string _profitCenter, string _invType, string _priceBook, string _priceLevel, string _customer, string _item, decimal _qty, DateTime _currentDate);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        List<PriceDefinitionRef> GetPriceDefinitionByBookAndLevel(string _company, string _book, string _level, string _invoiceType, string _profitCenter);

        [OperationContract]
        DataTable GetPriceDefinitionByBookAndLevelTable(string _company, string _book, string _level, string _invoiceType, string _profitCenter);


        //Written By Shanuka Perera 20/05/2014
        [OperationContract]
        DataTable GetAllInvDetails(string _invNo);

        //Written By Shanuka Perera 20/05/2014
        [OperationContract]
        DataTable GetOutItemDet(string _invNo);

        //Written By Shanuka Perera 23/05/2014
        [OperationContract]
        DataTable GetNewOutItems(string _invoiceNo, string _itemcde, double price, string serial, string scm);

        [OperationContract]
        DataTable GetProfitCenterTable(string _company, string _profitCenter);
        
        //Written By Prabhath on 08/05/2012
        [OperationContract]
        PriceDefinitionRef GetPriceDefinition(string _company, string _profitCenter, string _invType, string _book, string _level);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        bool IsValidInvoiceType(string _company, string _profitCenter, string _invType);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        MasterBusinessEntity GetBusinessCompanyDetail(string _company, string _businessCompanyCode, string _nic, string _mobile, string _businessType);

        //Written By Chamal on 10/04/2014
        [OperationContract]
        MasterBusinessEntity GetActiveBusinessCompanyDetail(string _company, string _businessCompanyCode, string _nic, string _mobile, string _businessType);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        List<RecieptItem> GetReceiptItemList(string _invoice);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        DataTable GetReceiptItemTable(string _invoice);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        Int32 SavePriceLevel(PriceBookLevelRef _level);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        Int32 SavePriceBook(List<PriceBookRef> _book);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        List<MasterInvoiceType> GetAllInvoiceType();

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        Int32 SavePriceDefinition(List<PriceDefinitionRef> _priceDef);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        List<PriceCategoryRef> GetAllPriceCategory(string _code);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        List<PriceTypeRef> GetAllPriceType(string _code);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        bool IsValidBook(string _company, string _book);

        [OperationContract]
        bool IsValidLevel(string _company, string _book, string _level);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        List<PriceSerialRef> GetAllPriceSerial(string _book, string _level, string _item, DateTime _date, string _customer, string _company, string _profitcenter);

        //Written By Prabhath on 23/05/2013
        [OperationContract]
        List<PriceSerialRef> GetAllPriceSerialFromSerial(string _book, string _level, string _item, DateTime _date, string _customer, string _company, string _profitcenter, string _serial);

        //kapila 26/8/2015
        [OperationContract]
        List<MasterItemTax> GetItemTax_strucbase(string _company, string _item, string _status, string _taxCode, string _taxRateCode, string _struc);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        List<PriceCombinedItemRef> GetPriceCombinedItem(Int32 _priceBookSeq, string _mainItem, string _mainSerial);

        //Written By Prabhath on 08/05/2012
        [OperationContract]
        List<PriceCombinedItemRef> GetPriceCombinedItemLine(Int32 _priceBookSeq, Int32 _priceitemseq, string _mainItem, string _mainSerial);

        //Written By Prabhath on 01/06/2012
        [OperationContract]
        List<MasterItemTax> GetItemTax(string _company, string _item, string _status, string _taxCode, string _taxRateCode);

        //Written By Prabhath on 01/06/2012
        [OperationContract]
        decimal GetMaxTax(string _company, string _item);

        //Written By Prabhath on 01/06/2012
        [OperationContract]
        List<MasterItemTax> GetTax(string _company, string _item, string _status);


        //Written By Prabhath on 04/05/2012
        [OperationContract]
        List<InventoryBatchRefN> GetConsumerProductPriceList(string _company, string _location, string _item, string _status);

        //Written By Prabhath on 04/05/2012
        [OperationContract]
        //Int32 SaveInvoice(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist);
        Int32 SaveInvoice(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo);


        //Written By Prabhath on 04/05/2012
        [OperationContract]
        List<InterCompanySalesParameter> GetInterCompanyParameter(string _adminTm, string _fromCompany, string _fromProfit, string _toCompany, string _toProfit);

        //Written By Prabhath on 12/06/2012
        [OperationContract]
        CustomerAccountRef GetCustomerAccount(string _company, string _customer);

        //Written By Prabhath on 12/06/2012
        [OperationContract]
        List<PaymentTypeRef> GetAllPaymentType(string _company, string _profitCenter, string _code);

        //Written By Sanjeewa on 23/07/2013
        [OperationContract]
        DataTable GetAllRepPriceType();
        //Written By dilshan on 08/11/2018
        [OperationContract]
        DataTable GetAllinvType();

        //kapila 14/6/2012
        [OperationContract]
        DataTable GetInvDet(string _InvNo);

        ////darshana 15/06/2012
        [OperationContract]
        List<InvoiceHeader> GetPendingInvoices(string _com, string _pc, string _cus, string _inv, string _status, string _fdate, string _tdate);

        ////darshana 15/06/2012
        [OperationContract]
        List<InvoiceHeaderTBS> GetPendingInvoicesTBS(string _com, string _pc, string _cus, string _inv, string _status, string _fdate, string _tdate);

        //darshana 18/07/2012
        [OperationContract]
        List<InvoiceHeader> GetHireSaleInvoiceForReverse(string _com, string _pc, string _cus, string _inv, string _fdate, string _tdate, string _AccNo);

        //darshana 15/06/2012
        [OperationContract]
        List<InvoiceItem> GetPendingInvoiceItems(string _inv);

        //chamal 15/08/2012
        [OperationContract]
        InvoiceItem GetPendingInvoiceItemsByItem(string _inv, string _itemcode);

        //darshana 19/07/2012
        [OperationContract]
        List<InvoiceItem> GetAllInvoiceItems(string _inv);

        //darshana 19/06/2012
        [OperationContract]
        //update by akila 2017/01/17, 2017/07/25
        Int16 SaveNewReceipt(RecieptHeader _NewReceipt, List<RecieptItem> _NewReceiptDetails, MasterAutoNumber _masterAutoNumber, ReptPickHeader _pickHeader, List<ReptPickSerials> _pickSerials, List<VehicalRegistration> _regDetails, List<VehicleInsuarance> _insDetails, List<HpSheduleDetails> _HPSheduleDetails, List<ReceiptAddDetails> _redAddDetails, MasterAutoNumber _masterAutoNumberType, List<GiftVoucherPages> _pageList, out string docno, string _comCode = null, string _userId = null, bool _isFreeRegistered = false, decimal _actualRegistationAmt = 0, bool itemres = false, bool _isResser=false);
        //Int16 SaveNewReceipt(RecieptHeader _NewReceipt, List<RecieptItem> _NewReceiptDetails, MasterAutoNumber _masterAutoNumber, ReptPickHeader _pickHeader, List<ReptPickSerials> _pickSerials, List<VehicalRegistration> _regDetails, List<VehicleInsuarance> _insDetails, List<HpSheduleDetails> _HPSheduleDetails, List<ReceiptAddDetails> _redAddDetails, MasterAutoNumber _masterAutoNumberType, List<GiftVoucherPages> _pageList, out string docno);

        //Pramil
        [OperationContract]
        Int32 SaveNewReceiptTBS(RecieptHeaderTBS _NewReceipt, List<RecieptItemTBS> _NewReceiptDetails, MasterAutoNumber _masterAutoNumber, ReptPickHeader _pickHeader, List<ReptPickSerials> _pickSerials, List<VehicalRegistration> _regDetails, List<VehicleInsuarance> _insDetails, List<HpSheduleDetails> _HPSheduleDetails, MasterAutoNumber _masterAutoNumberType, List<GiftVoucherPages> _pageList, out string docno);

        //Written by darshana on 19/06/2012
        [OperationContract]
        MasterBankAccount GetBankDetails(string _company, string _BankCode, string _AccCode);

        //written by darshan on 19/06/2012
        [OperationContract]
        RecieptHeader GetReceiptHeader(string _com, string _pc, string _doc);

        //written by Chamal on 15/08/2012
        [OperationContract]
        RecieptHeader GetReceiptHeaderByType(string _com, string _pc, string _doc, string _type);

        //written by darshana on 19/06/2012
        [OperationContract]
        List<RecieptItem> GetReceiptDetails(RecieptItem _paramRecDetails);

        //written by darshana on 19/06/2012
        [OperationContract]
        List<RecieptItemTBS> GetReceiptDetailsTBS(RecieptItemTBS _paramRecDetails);

        //writtn by darshana on 10/08/2012
        [OperationContract]
        List<ReptPickSerials> GetSerialByBaseDoc(string _com, string _doc);

        [OperationContract]
        List<ReptPickSerials> GetSerialByBaseDoc2(string _com, string _doc);

        //written by darshana on 19/06/2012
        [OperationContract]
        Int16 UpdateRecStatus(RecieptHeader _UpdateRec);

        //written by darshana on 10-11-2012
        [OperationContract]
        Int16 CancelWaraRec(RecieptHeader _ExtendRec, List<ReceiptWaraExtend> _ExtendRecDet);

        //written by darshana on 19/06/2012
        [OperationContract]
        Int32 SaveReversal(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, MasterAutoNumber _invoiceAuto, Boolean _isHP, out string _invoiceNo, InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, out string _docNo);

        //written by darshana on 18/02/2013
        [OperationContract]
        Int32 SaveHPReversal(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, MasterAutoNumber _invoiceAuto, Boolean _isHP, out string _invoiceNo, InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, MasterAutoNumber _reversReceiptAuto, List<RecieptHeader> _hpReversReceiptHeader, HpTransaction _transaction, MasterAutoNumber _txnAuto, out string _docNo);

        //written by darshana on 19/06/2012
        [OperationContract]
        List<DistrictProvince> GetDistrict(string _pCode);

        //written by darshana on 03/08/2012
        [OperationContract]
        Int32 SaveHPAdjustment(HpAdjustment _MainAdj, HpTransaction _MainTxn, MasterAutoNumber _MainAdjAuto, MasterAutoNumber _MainTxnAuto, HpAdjustment _SubAdj, HpTransaction _SubTxn, MasterAutoNumber _SubAdjAuto, MasterAutoNumber _SubTxnAuto, Boolean _isMulti, out string _docNo);


        #region receipt

        [OperationContract]
        bool IsValidReceiptType(string _company, string _type);

        [OperationContract]
        bool IsValidDivision(string _company, string _pc, string _cd);

        [OperationContract]
        decimal GetOutInvAmt(string _company, string _pc, string _cus, string _inv);

        [OperationContract]
        decimal GetOutInvAmtTBS(string _company, string _pc, string _cus, string _inv);

        #endregion

        #region Hire Purchase
        #region Collection
        //Shani 15-06-2012
        [OperationContract]
        List<string> GetAllProfCenters(string company);

        //Shani 19-06-2012
        [OperationContract]
        List<PaymentType> GetPossiblePaymentTypes(string pc, string txn_tp, DateTime today);

        [OperationContract]
        List<PaymentType> GetPossiblePaymentTypes_new(string _com, string _schnl, string _pc, string txn_tp, DateTime today, Int32 _isBOCN);

        //Shani 20-06-2012
        [OperationContract]
        List<HpAccount> GetHP_Accounts(string com, string pc, string seqNo, string status);

      
        //Shani 20-06-2012
        [OperationContract]
        List<string> GetAll_prifixes(string channel, string docTP, Int32 status);

        //Shani 20-06-2012
        [OperationContract]
        List<GntManualDocument> Get_valid_Man_ReceiptNo();

        //Shani 21-06-2012
        [OperationContract]
        Decimal Get_AccountBalance(DateTime date, string accNo);

        //Shani 22-06-2012
        [OperationContract]
        List<TempPickManualDocDet> Get_temp_collection_Man_Receipts(string user, string Loc, string prefix, Int32 receipt_seqno);

        //Shani 23-06-2012
        [OperationContract]
        Decimal Get_MonthlyRental(DateTime date, string accNo);

        //Shani 26-06-2012
        [OperationContract]
        Decimal Get_FutureRentals(DateTime date, string accNo);

        //Shani 26-06-2012
        [OperationContract]
        string saveAll_HP_Collect_Recipts(List<RecieptHeader> receiptHeaderList, List<RecieptItem> receipItemList, List<HpTransaction> transactList, MasterAutoNumber receipAuto, MasterAutoNumber tranxAuto, string loc, Boolean isECDgiven, List<Object> listECD_info, Boolean _isNormalClose);

        //Chamal 03-08-2012
        [OperationContract]
        // Int32 saveAll_HP_ReceiptReversal(List<RecieptHeader> receiptHeaderList, string com, string loc, string loginuser, bool isAppCycle, string appReqno);
        Int32 saveAll_HP_ReceiptReversal(List<RecieptHeader> receiptHeaderList, string com, string loc, string loginuser, bool isAppCycle, string appReqno, out string _RefundReceiptNo);

        //Shani 27-06-2012
        [OperationContract]
        RecieptHeader Get_ReceiptHeader(string prefix, string seqNo);

        //Chamal 12-07-2012
        [OperationContract]
        List<RecieptHeader> Get_ReceiptHeaderListALL(string _parm, string _type);

        //Chamal 12-07-2012
        [OperationContract]
        DataTable Get_ReceiptHeaderTableALL(string _parm, string _type);

        //Shani 27-06-2012
        [OperationContract]
        Int32 Save_HpTransaction(HpTransaction _transaction);

        //Shani 28-06-2012
        [OperationContract]
        DataTable Get_hpAcc_TransactionDet(string AccountNo);

        //Shani 30-06-2012
        [OperationContract]
        Decimal Get_TotFutureRentalValue(DateTime firstDateOfNextMonth, string accNo);

        //Shani 30-06-2012
        [OperationContract]
        DataTable Get_hpHierachy(string pc);

        //Shani 30-06-2012
        [OperationContract]
        DataTable Get_ECD(string ecdTp, DateTime date, string scheme, string partyTP, string partyCD, Decimal futureRentals, string AccNo, Int32 IsReduce);

        //Written By Darshana on 02/06/2012
        [OperationContract]
        BlackListCustomers GetBlackListCustomerDetails(string _cusCode, Int32 _active);

        //writtne by darshana on 04/07/2012
        [OperationContract]
        MasterItem getMasterItemDetails(string _company, string _item, Int16 _active);
        [OperationContract]
        MasterItem getMasterItemDetails2(string _company, string _item);

        //written by darshana on 13/07/2012
        [OperationContract]
        HpSchemeDetails getSchemeDetails(string _type, string _value, Int16 _active, string _cd);

        //written by darshana on 06/07/2012
        [OperationContract]
        Int32 SaveTempPrice(TempCommonPrice _tempPrice);

        //written by darshana on 06/07/2012
        [OperationContract]
        Int32 DeleteTempPrice(string usr, string ip, string itm);

        //kapila
        [OperationContract]
        Int32 DeleteMgrIssueReceive(Int32 _seqno, string _com, string _pc, string _recno, Decimal _val);

        //kapila
        [OperationContract]
        Int32 Save_mgr_rcv(string _com, string _pc, string _accno, string _recno, DateTime _dt, Decimal _amt, string _creby, string _rem);

        //written by darshana on 06/07/2012
        [OperationContract]
        List<TempCommonPrice> GetCommonPriceBook(string _user, string _ip, Int32 _count);

        //written by darshana on 06/07/2012
        [OperationContract]
        List<PriceDefinitionRef> GetPriceBooksForPC(string _com, string _pc, string _type);

        //written by darshana on 06/07/2012
        [OperationContract]
        List<TempCommonPrice> GetItemsWithPrice(string _user, string _ip, string _pb, string _lvl);

        //Shani 04-07-2012
        [OperationContract]
        HpAccount GetHP_Account_onAccNo(string AccountNo);

        //Shani 04-07-2012
        [OperationContract]
        Int32 Edit_HP_Collect_Recipt(List<RecieptHeader> receiptHeaderList, List<RecieptItem> receipItemList, List<HpTransaction> transactList, MasterAutoNumber tranxAuto);

        //Shani 09-09-2012
        [OperationContract]
        Int32 Edit_HP_Collect_Recipt_NEW(List<RecieptHeader> Old_receiptHeaderList, List<RecieptHeader> receiptHeaderList, List<RecieptItem> receipItemList, List<HpTransaction> transactList, MasterAutoNumber receipAuto, MasterAutoNumber tranxAuto, string loc);
        //Shani 05-07-2012
        [OperationContract]
        Int32 Get_TotReciptsForAccount(string accNo);

        //Shani 05-07-2012
        [OperationContract]
        Decimal Get_hp_additionalCommision(DateTime receipt_date, string scheme, Decimal inst_comRate);

        //Shani 05-07-2012
        [OperationContract]
        DataTable Get_ArrearsInfo(DateTime lastDayOfPrevMonth, string partyTP, string partyCD);

        //Shani 05-07-2012
        [OperationContract]
        Decimal Get_hp_TotalDue(string accNo, DateTime ars_date);

        //Shani 06-07-2012
        [OperationContract]
        Decimal Get_hp_ArrearsSettlement(string accNo, DateTime sup_date);

        //Shani 06-07-2012
        [OperationContract]
        Decimal Get_hp_MinArrears(string partyTP, string partyCD);

        //Shani 06-07-2012
        [OperationContract]
        Decimal Get_hp_Adjustment(string accNo);

        //Shani 06-07-2012
        [OperationContract]
        Decimal Get_hp_Tot_Receipts(string accNo, DateTime date);

        //Shani 08-07-2012
        [OperationContract]
        DataTable validate_Voucher(DateTime receiptDt, string AccNo, string voucherNo);

        //Shani 08-07-2012
        [OperationContract]
        Int32 cancelReceipt(string p_com, string p_prifix, string p_receiptNo, HpTransaction transaction);

        //Shani 19-07-2012
        [OperationContract]
        string GetHpCustomerName(string accNo);

        [OperationContract]
        string GetQuoCustomerMail(string quo);

        //Shani 20-07-2012
        [OperationContract]
        DateTime Get_EndingDate(string AccNo);

        //Shani 14-08-2012
        [OperationContract]
        Decimal Get_MaxHpReceiptAmount(string hsyCD, string parytyTP, string partyCD);

        //Shani 14-08-2012
        [OperationContract]
        DataTable Get_ESD_EPF_WHT(string com, string pc, DateTime p_reciptDate);

        //Shani 03-09-2012
        [OperationContract]
        Decimal Get_hp_TotalDue_onType(string accNo, DateTime ars_date, string type, string receiptNo, DateTime receiptDt);

        //Shani 17-09-2012
        [OperationContract]
        List<RecieptHeader> Get_ReceiptHeaderList(string prefix, string seqNo);

        //Shani 20-09-2012
        [OperationContract]
        Decimal Get_ProtectionPayment_RefundValue(string accNo);

        //Shani 21-09-2012
        [OperationContract]
        RecieptHeader Get_last_ReceiptHeaderOfTheDay(DateTime date, string AccNo);

        //Shani 24-09-2012
        [OperationContract]
        Decimal Get_Diriya_CommissionRate(string accNo, DateTime ars_date);


        //Shani 03-12-2012
        [OperationContract]
        Decimal Get_hp_AllDue(string accNo, DateTime ars_date, DateTime sup_date);

        #endregion Collection

        #endregion Hire Purchase

        //Written By Prabhath on 25/06/2012
        [OperationContract]
        RecieptHeader GetReceiptHeaderByInvoice(string _invoice);

        //Written By Prabhath on 25/06/2012
        [OperationContract]
        Int32 InvoiceCancelation(InvoiceHeader _header, out string _message, List<InventoryHeader> InventoryList);

        //Written By Prabhath on 25/06/2012
        [OperationContract]
        List<LoyaltyMemeber> GetCustomerLoyality(string _customer, string _cardno, DateTime _date);

        //Written By Prabhath on 25/06/2012
        [OperationContract]
        List<LoyaltyType> GetLoyalityType(string _cardtype, DateTime _date);

        //Written By Prabhath on 25/06/2012
        [OperationContract]
        List<LoyaltyPointDiscount> GetLoyaltyDiscount(string _locyaltyType, string _partyType, string _party, DateTime _date, decimal _point, string _pricebook, string _pricelevel);

        //Written By Prabhath on 25/06/2012
        [OperationContract]
        List<LoyaltyPriorityCode> GetLoyaltyPriority(string _company);

        //Written By Prabhath on 27/06/2012
        [OperationContract]
        List<InvoiceItem> GetInvoiceDetailByInvoice(string _invoice);

        //Written By Prabhath on 05/07/2012
        [OperationContract]
        List<InvoiceHeader> GetInvoiceByAccountNo(string _company, string _profitCenter, string _account);

        //Written By Prabhath on 05/07/2012
        [OperationContract]
        DataTable GetInvoiceByAccountNoTable(string _company, string _profitCenter, string _account);


        //Written By Prabhath on 05/07/2012
        [OperationContract]
        List<RecieptHeader> GetReceiptByAccountNo(string _company, string _profitCenter, string _account);

        //Written By Prabhath on 05/07/2012
        [OperationContract]
        List<HpAdjustment> GetAccountAdjustment(string _profitCenter, string _account, string _adjtype);

        //Written By Prabhath on 05/07/2012
        [OperationContract]
        List<HpCashConversionDefinition> GetCashConversionDefinition(string _scheme, Int32 _period, string _book, string _level, DateTime _uptoDate, decimal _upvalue, decimal _afvalue, decimal _hpvalue, DateTime _acc_create_Date);



        //Written By Prabhath on 05/07/2012
        [OperationContract]
        List<HpInsurance> GetAccountInsurance(string _account, Int16 _type);

        //Written By Prabhath on 05/07/2012
        [OperationContract]
        List<MasterSalesPriorityHierarchy> GetSalesPriorityHierarchy(string _company, string _profitCenter, string _category, string _type);

        //kapila 6/7/2012
        [OperationContract]
        DataTable GetCustomerDetByGroupCode(string _grpCode);

        //kapila 6/7/2012
        [OperationContract]
        int SaveGroupSaleData(GroupSaleHeader _groupSale, MasterAutoNumber _mastAutoNo, out string _docNo);

        //kapila 6/7/2012
        [OperationContract]
        Int32 GetGroupSaleCountByCompany(string _groupCompany);

        //kapila 9/7/2012
        [OperationContract]
        GroupSaleHeader GetGroupSaleHeaderDetails(string _groupSaleCode);

        //kapila 9/7/2012
        [OperationContract]
        List<GroupSaleCustomer> GetGroupSaleCustomers(string _groupSaleCode);

        //Written By Prabhath on 05/07/2012
        [OperationContract]
        Int32 SaveCashConversionEntry(InvoiceHeader _hpReversInvoiceHeader, List<InvoiceItem> _hpReversInvoiceItem, List<RecieptHeader> _hpReversReceiptHeader, InvoiceHeader _ccInvoiceHeader, List<InvoiceItem> _ccInvoiceItem, RecieptHeader _ccReceiptHeader, List<RecieptItem> _ccReceiptItem, MasterAutoNumber _reversInvoiceAuto, MasterAutoNumber _reversReceiptAuto, MasterAutoNumber _convertInvoiceAuto, MasterAutoNumber _convertReceiptAuto, HpInsurance _reversInsurance, InventoryHeader _ccInv, MasterAutoNumber _invAuto, out string _convertInvoice,
            RequestApprovalHeader _regReqHdr, List<RequestApprovalDetail> _regReqDet, List<RequestApprovalSerials> _regReqSer, RequestApprovalHeaderLog _regReqLogHdr, List<RequestApprovalDetailLog> _regReqLogDet, List<RequestApprovalSerialsLog> _regReqLogSer, MasterAutoNumber _regReqAuto, RequestApprovalHeader _insReqHdr, List<RequestApprovalDetail> _insReqDet, List<RequestApprovalSerials> _insReqSer, RequestApprovalHeaderLog _insReqLogHdr, List<RequestApprovalDetailLog> _insReqLogDet, List<RequestApprovalSerialsLog> _insReqLogSer,
            MasterAutoNumber _insReqAuto, List<RecieptHeader> _regReciept, List<RecieptItem> _regRecieptItem, MasterAutoNumber _regRecieptAuto, List<RecieptHeader> _insReciept, List<RecieptItem> _insRecieptItem, MasterAutoNumber _insRecieptAuto, int option, HpTransaction _transaction, out string _err);

        //Written By Prabhath on 05/07/2012
        [OperationContract]
        DataTable GetInvoiceDetailByInvoiceTable(string _invoice);



        //written by darshana on 11/07/2012
        [OperationContract]
        List<HpSchemeDefinition> GetAllScheme(string _type, string _value, string _book, string _level, DateTime _date, string _item, string _brand, string _maincat, string _cat, string _scheme);

        //written by darshana on 11/07/2012
        [OperationContract]
        List<HpSchemeDefinition> GetAllSchemeNew(string _type, string _value, string _book, string _level, DateTime _date, string _item, string _brand, string _maincat, string _cat, string _scheme, string _promo);

        //written by darshana 15/07/2012
        [OperationContract]
        List<HpServiceCharges> getServiceCharges(string _type, string _value, string _cd);

        //written by darshana 15/07/2012
        [OperationContract]
        List<HpServiceCharges> getServiceChargesNew(string _type, string _value, string _cd, DateTime _accDt);

        //written by darshana 16/07/2012
        [OperationContract]
        List<HpOtherCharges> GetOtherCharges(string _scheme, string _book, string _level, DateTime _date, string _item, string _brand, string _maincat, string _cat);

        //written by darshana 17/07/2012
        [OperationContract]
        List<HpAdditionalServiceCharges> getAddServiceCharges(string _scheme, string _type, string _value, DateTime _date);

        //Written by darshana 18/07/2012
        [OperationContract]
        List<HpInsuranceDefinition> GetInsuDefinition(string _scheme, string _type, string _value, DateTime _date);



        //written by darshana 18/07/2012
        [OperationContract]
        HpSchemeSheduleDefinition GetSchemeSheduleDef(string _scheme, Int16 _rentNo);

        //written by darshana 25/07/2012
        [OperationContract]
        HpSystemParameters GetSystemParameter(string _type, string _value, string _code, DateTime _date);

        //written by darshana 26/07/2012
        [OperationContract]
        MasterCompanyItem GetAllCompanyItems(string _com, string _item, Int16 _active);

        //written by darshana 24/07/2012
        [OperationContract]
        Int32 CreateCustomer(string _pc, MasterBusinessEntity _businessCompany, out string _cusCode);

        #region Price Enquiry
        //Shani 09/07/2012
        [OperationContract]
        DataTable EnquirePriceDetails(string user, string com, string pc, string piceBookCD, string priceLevel, string itemCD, string customerCD, string Type, DateTime frmDt, DateTime toDt, string circular, string cate1, string cate2, string cate3, Int32 type);

        //Shani 09/07/2012
        [OperationContract]
        List<MasterPartyHierachy> get_hierarchy(string _cate);

        ////Shani 13/07/2012
        //[OperationContract]
        //DataTable get_hp_schemes(DateTime as_atDate, string itemCD, string parytTP, string partyCD, string brand, string cate1, string cate2);

        //Shani 14/07/2012
        [OperationContract]
        List<HpSchemeDefinition> get_hp_Schemes(DateTime as_atDate, string itemCD, string parytTP, string partyCD, string brand, string cate1, string cate2);

        //Shani 14/07/2012
        [OperationContract]
        DataTable EnquirePriceDetails_forAsAtDate(string user, string com, string pc, string piceBookCD, string priceLevel, string itemCD, string customerCD, string Type, DateTime asAtDate, string circular, string cate1, string cate2, string cate3, Int32 type);

        //Shani 25/07/2012
        [OperationContract] //use this instead of get_hp_Schemes
        List<HpSchemeDefinition> get_HP_Schemes(DateTime as_atDate, string itemCD, string parytTP, string partyCD, string brand, string cate1, string cate2, string schemeCD, string priceBook, string priceLevel);

        //Shani 25/07/2012
        [OperationContract]
        Decimal GET_Item_vat_Rate(string com, string itemCD, string taxCode);

        #endregion  Price Enquiry

        //kapila 13/7/2012
        [OperationContract]
        Int16 Approve_group_Sale(string _groupSaleCode, string _user);

        //kapila 
        [OperationContract]
        List<MasterSalesPriorityHierarchy> get_pc_info_by_code(string _code);

        //kapila
        [OperationContract]
        Boolean IsGroupSaleCodeFound(string _grpCode);

        //kapila
        [OperationContract]
        Int32 GetGroupSaleDet(string _groupSaleCode, out Int32 _noofacc, out Int32 _noofprod, out Int32 _noofcust, out Decimal _val);

        [OperationContract]
        Int32 SaveReceiptHeader(RecieptHeader _recieptHeader);

        [OperationContract]
        Int32 SaveReceiptItem(RecieptItem _recieptItem);

        [OperationContract]
        string GetRecieptNo(MasterAutoNumber _receiptAuto);

        //written by darshana 02/08/2012
        [OperationContract]
        List<HPAdjustmentTypes> GetHPAdjTypes();

        //Writtn by darshana 02/08/2012
        [OperationContract]
        HPAdjustmentTypes GetHPAdjByCode(string _code);

        //written by darshana 11/08/2012
        [OperationContract]
        VehicalRegistrationDefnition GetVehRegDef(string _com, string _inv, string _itm, DateTime _date);

        //writtn by darshana 15-08-2012
        [OperationContract]
        MasterOutsideParty GetOutSidePartyDetails(string _cd, string _tp);

        //written by darshana 15-08-2012
        [OperationContract]
        InsuarancePolicy GetInusPolicy(string _cd);

        //written by darshana 15/08/2012
        [OperationContract]
        MasterVehicalInsuranceDefinition GetVehInsDef(string _com, string _inv, string _itm, DateTime _date, string _pc, string _insCom, string _insPol, Int32 _tearm);

        //written by darshana 13-09-2012
        [OperationContract]
        MasterVehicalInsuranceDefinition GetVehInsAmtDirect(string _com, string _pc, string _salesTP, string _insCom, string _insPol, string _itm, DateTime _date, Int32 _tearm);

        //writtrn by darshana 13-09-2012
        [OperationContract]
        VehicalRegistrationDefnition GetVehRegAmtDirect(string _com, string _pc, string _type, string _itm, DateTime _date);

        #region Customer Creation
        //Shani 31/07/2012
        [OperationContract]
        Int32 SaveBusinessEntityDetail(MasterBusinessEntity _businessEntity, CustomerAccountRef _account, List<MasterBusinessEntityInfo> bisInfoList, out string customerCD, List<MasterBusinessEntitySalesType> SalesType);

        //Shani 02/08/2012
        [OperationContract]
        DataTable GetBusinessEntityTypes(string category);

        //Shani 02/08/2012
        [OperationContract]
        DataTable GetBusinessEntityAllValues(string category, string type_);

        //Shani 03/08/2012
        [OperationContract]
        MasterBusinessEntity GetCustomerProfile(string CustCD, string nic, string DL, string PPNo, string brNo);

        //Shani 04/08/2012
        [OperationContract]
        Int32 UpdateBusinessEntityProfile(MasterBusinessEntity _businessEntity, string modby, DateTime modDate, Decimal Newcredlimit, List<MasterBusinessEntityInfo> bisInfoList, List<MasterBusinessEntitySalesType> _salesTypes);

        //Shani 08/08/2012
        [OperationContract]
        Int32 SaveBusinessEntityDiscount(CashGeneralDicountDef dicDef, List<string> pc);
        #endregion

        //kapila 2/6/2014
        [OperationContract]
        Int32 SaveGeneralDiscDef(List<CashGeneralDicountDef> _genDiscList, string _circ, out string _err);

        //Prabhath on 02/08/2012
        [OperationContract]
        List<CashCommissionDetailRef> GetCashCommissionDetail(string _invoicetype, DateTime _date, string _pricebook, string _pricelevel, string _com, string _pc);

        //Prabhath on 02/08/2012
        [OperationContract]
        Dictionary<ItemHierarchyElement, string> FixItemHierarchyElements(string _serial, string _promotion, string _item, string _brand, string _maincategory, string _subcategory, string _pricebook, string _pricelevel);
        //Prabhath on 02/08/2012
        [OperationContract]
        Int32 SaveRevert(decimal _balancepotion, HpRevertHeader _rvhdr, InventoryHeader _invhdr, List<ReptPickSerials> _pickserial, List<ReptPickSerialsSub> _picksubserial, MasterAutoNumber _rvAuto, MasterAutoNumber _invAuto, out string _rvdoc, out string _adjDoc);

        //Shanuka Perera on 26/05/2014
        [OperationContract]
        Int32 SaveOutItemdetails(CashSales_Out_RevItems objOutItems, out string _err);

        [OperationContract]
        Int32 SaveAdvancedRep_Details(CashSales_Out_RevItems _objAdvanced, out string _err);




        //shanuka perera  on 27/05/2014
        [OperationContract]
        Int32 SaveCommentDetails(string invNO, string user, string remark, string dept, string rec_tp, out string _errr);

        //written by darshana on 08/08/2012
        [OperationContract]
        Int32 CreateHPAccount(HpAccount _HPAccount, MasterAutoNumber _AccAutoNo, InvoiceHeader _InvHeader, List<InvoiceItem> _InvItem, MasterAutoNumber _InvNo, HPAccountLog _HPAccLog, List<HpSheduleDetails> _HPSheduleDetails, List<RecieptHeader> receiptHeaderList, List<RecieptItem> receipItemList, MasterAutoNumber receipAuto, List<HpTransaction> _TxnList, MasterAutoNumber _TxnAuto, List<HpCustomer> _AccCus, HpInsurance _insu, MasterAutoNumber _InsuRecNo, string _loc, out string _docNo, out string _AccountNo, out string _InvoiceNo, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, decimal _mgrCommAmt, out string _sysRecNo, Boolean _isSysRecPC, Boolean _isManRec, List<InvoiceVoucher> _voucher, RecieptHeader _insuRec, List<RecieptItem> _insuRecDet, out string _sysAddRec);

        //written by darshana on 11/04/2014
        [OperationContract]
        Int32 CreateHPAccountNew(HpAccount _HPAccount, MasterAutoNumber _AccAutoNo, InvoiceHeader _InvHeader, List<InvoiceItem> _InvItem, MasterAutoNumber _InvNo, HPAccountLog _HPAccLog, List<HpSheduleDetails> _HPSheduleDetails, List<RecieptHeader> receiptHeaderList, List<RecieptItem> receipItemList, MasterAutoNumber receipAuto, List<HpTransaction> _TxnList, MasterAutoNumber _TxnAuto, List<HpCustomer> _AccCus, HpInsurance _insu, MasterAutoNumber _InsuRecNo, string _loc, out string _docNo, out string _AccountNo, out string _InvoiceNo, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, decimal _mgrCommAmt, out string _sysRecNo, Boolean _isSysRecPC, Boolean _isManRec, List<InvoiceVoucher> _voucher, RecieptHeader _insuRec, List<RecieptItem> _insuRecDet, out string _sysAddRec);

        //written by darshana on 12/04/2014
        [OperationContract]
        Int32 CreateHPAccountNew02(HpAccount _HPAccount, MasterAutoNumber _AccAutoNo, InvoiceHeader _InvHeader, List<InvoiceItem> _InvItem, MasterAutoNumber _InvNo, HPAccountLog _HPAccLog, List<HpSheduleDetails> _HPSheduleDetails, List<RecieptHeader> receiptHeaderList, List<RecieptItem> receipItemList, MasterAutoNumber receipAuto, List<HpTransaction> _TxnList, MasterAutoNumber _TxnAuto, List<HpCustomer> _AccCus, HpInsurance _insu, MasterAutoNumber _InsuRecNo, string _loc, out string _docNo, out string _AccountNo, out string _InvoiceNo, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, decimal _mgrCommAmt, out string _sysRecNo, Boolean _isSysRecPC, Boolean _isManRec, List<InvoiceVoucher> _voucher, RecieptHeader _insuRec, List<RecieptItem> _insuRecDet, out string _sysAddRec);

        //written by Shani on 15/08/2012
        [OperationContract]
        Boolean validateBank_and_Branch(string bus_cd, string branch_cd, string _type);

        //written by Prabhath on 16/08/2012
        [OperationContract]
        Dictionary<decimal, decimal> GetGeneralEntityDiscountDefinition(string _company, string _profitcenter, DateTime _date, string _book, string _level, string _customer, string _item, bool _isAllowSerial, bool _isAllowPromotion);


        //written by Prabhath on 17/08/2012
        [OperationContract]
        void GetGeneralPromotionDiscount(string _company, string _profitcenter, string _invoicetype, Int32 _time, string _day, DateTime _date, List<InvoiceItem> _InInvDet, List<RecieptItem> _InReceiptDet, out List<InvoiceItem> _outInvItem, out bool _isDifferent, out decimal _tobepay, InvoiceHeader _invoiceheader);

        //kapila
        [OperationContract]
        Int32 Process_Service_Agreement(SCV_AGR_HDR oHeader, MasterAutoNumber _Auto, List<SCV_AGR_ITM> oAgrItems, List<SCV_AGR_SES> oAgrSes, List<SCV_AGR_COVER_ITM> oAgrCovItm, List<scv_agr_cha> oAgrCha, List<scv_agr_payshed> oAgrPayShed, MasterAutoNumber _invoiceAuto, out string GenAgr);



        #region  Vehicle job

        //written by Shani on 17/08/2012
        [OperationContract]
        InventorySerialMaster GetVehicleDetails(string RegistrationNo, string EngineNo, string ChasseNo);

        //written by Shani on 18/08/2012
        [OperationContract]
        DataTable GetServiceTypes(string com, string loc);

        //written by Shani on 20/08/2012
        [OperationContract]
        DataTable GetNextServiceDet(string RegistrationNo, string EngineNo, string ChasseNo);

        //written by Shani on 20/08/2012
        [OperationContract]
        DataTable GetLastServiceDet(string RegistrationNo, string EngineNo, string ChasseNo);

        //written by Shani on 20/08/2012
        [OperationContract]
        DataTable Get_DefectTypes(string com, string loc);

        ////written by Shani on 21/08/2012
        //[OperationContract]
        //Int32 Save_Job_Header(ServiceJobHeader _stagelog);

        ////written by Shani on 22/08/2012
        //[OperationContract]
        //Int32 Save_JobDetail(ServiceJobDetail jobDet);

        ////written by Shani on 22/08/2012
        //[OperationContract]
        //Int32 Save_Job_Defect(List<ServiceJobDefect> _revertDefect);

        ////written by Shani on 22/08/2012
        //[OperationContract]
        //Int32 Save_Job_stagelog(ServiceJobStageLog _stagelog);

        //written by Shani on 22/08/2012
        [OperationContract]
        Int32 SaveVehicleJob(ServiceJobHeader jobHdr, ServiceJobDetail jobDet, List<ServiceJobDefect> defectsList, ServiceJobStageLog _stagelog, Int32 serID, Int32 IsService, Int32 IsUpdateShedule, MasterAutoNumber _jobAuto, bool isExternal, out string jobNo);

        #endregion  Vehicle job

        //written by Prabhath on 18/08/2012
        [OperationContract]
        string GetInvoicePrefix(string _company, string _profitcenter, string _invoicetype);

        //written by Prabhath on 22/08/2012
        [OperationContract]
        List<CashGeneralEntiryDiscountDef> GetGeneralEntityDiscountDef(string _company, string _profitcenter, DateTime _date, string _book, string _level);


        //written by Prabhath on 24/08/2012
        [OperationContract]
        Int32 SaveCashGeneralEntityDiscount(string _smsDocType, string _company, string _location, string _documentno, string _user, List<CashGeneralEntiryDiscountDef> _list);

        //written by darshana on 27/07/2012
        [OperationContract]
        HpSchemeType getSchemeType(string _type);

        //written by Prabhath on 29/07/2012
        [OperationContract]
        Int32 SaveHPExchange(DateTime _date, string _accountno, string _company, string _location, string _profitcenter, string _createdBy, string _inSubType, string _outSubType, List<ReptPickSerials> _list, List<ReptPickSerials> _outList, List<InvoiceItem> _outPureInvoiceItem, List<RecieptHeader> receiptHeaderList, List<RecieptItem> receipItemList, MasterAutoNumber receipAuto, out string _creditnotelist, out string _inventorydoclist, HPAccountLog _accLog, HpAccount _newAccount, List<HpSheduleDetails> _currentSchedule, List<HpSheduleDetails> _newSchedule, HpInsurance _insurance, MasterAutoNumber _insuranceAuto, RequestApprovalHeader _request, out string _diriya, out string _invNo, out string _recNo);

        //sachith 2012/08/31
        [OperationContract]
        DataTable GetExternalVehicalJob(string _company, string _loc);

        #region Hp Acc Restriction
        //Shani 05/09/2012
        [OperationContract]
        Int32 SaveAccRestriction(List<AccountRestriction> _accRestList);

        //Shani 05/09/2012
        [OperationContract]
        DataTable GetPC_from_Hierachy(string com, string channel, string subChannel, string area, string region, string zone, string pc_code);

        //kapila  23/1/2013
        [OperationContract]
        DataTable GetPC_from_Hierachy_Rep(string user, string com, string channel, string subChannel, string area, string region, string zone, string pc_code);

        //kapila  24/1/2013
        [OperationContract]
        DataTable GetLoc_from_Hierachy_Rep(string user, string com, string channel, string subChannel, string area, string region, string zone, string pc_code);

        #endregion Hp Acc Restriction

        //sachith 2012/09/08
        [OperationContract]
        Int32 GetDOPbCount(string _com, string _pb, string _pb_level);

        //darshana 11/09/2012
        [OperationContract]
        VehicalRegistration CheckPrvRegDetails(string _engine, string _item, string _com, Int32 _status);

        //darshana 11/09/2012
        [OperationContract]
        VehicleInsuarance CheckPrvInsDetails(string _engine, string _item, string _com, Int32 _status);

        //Written By Prabhath on 11/09/2012
        [OperationContract]
        bool CheckItemStatus(string _company, string _book, string _level, string _status);

        [OperationContract]
        List<ReptPickSerials> GetStatusGodSerial(string company, string location, string itemCode, string pbook, string pblvl, string itemstatus);

        //written by darshana on 17/09/2012
        [OperationContract]
        List<RecieptHeader> GetReceiptBydaterange(string _com, string _pc, DateTime _from, DateTime _to, string _type);

        // Nadeeka 17-09-2015
        [OperationContract]
        List<RecieptHeader> GetReceiptBydaterangeExpired(string _com, string _pc, DateTime _from, DateTime _to, string _type, string _recno);

        // Nadeeka 17-09-2015
        [OperationContract]
        List<RecieptHeader> GetReceiptBydaterangeItem(string _com, string _pc, DateTime _from, DateTime _to, string _type, string _recno);

        // Nadeeka 17-09-2015
        [OperationContract]
        Int32 Update_ReceiptItemExpriryDate(string _com, string _recNo, DateTime _Todate, DateTime _serDate, DateTime _refDate);

        // Nadeeka 17-09-2015
        [OperationContract]
        Int32 Update_ReceiptItemSerial(string _com, string _recNo, string _reqNo);


        //written by darshana on 18-09-2012
        [OperationContract]
        List<RecieptItem> GetAllReceiptItems(string _Rec);

        //writtrn by darshana on 19-09-2012
        [OperationContract]
        Int32 CreateRefund(RecieptHeader _refundHdr, List<RecieptItem> _refundItm, MasterAutoNumber _receiptAuto, List<ReceiptItemDetails> _resItmList, RemitanceSummaryDetail _remsumdet, out string _refNo);

        //Written by darsahana on 04-10-2012
        [OperationContract]
        MasterReceiptDivision GetDefRecDivision(string _com, string _pc);


        #region cash conversioon
        //sachith 2012/09/18
        [OperationContract]
        List<HpSchemeType> GetSchemeTypeByCategory(string _cat);

        [OperationContract]
        DataTable GetSAllchemeCategoryies(string _code);

        [OperationContract]
        DataTable GetSchemes(string _type, string _code);

        #endregion

        [OperationContract]
        DataTable GetPendingInvoiceItemsByItemDT(string _inv, string _itemcode);

        //Written By Prabhath on 21/09/2012
        [OperationContract]
        PriceDetailRef GetPriceDetailByItemLineSeq(string _item, Int32 _itemline, Int32 _pbseq);




        //writtrn by shani on 26-09-2012
        [OperationContract]
        Int32 SaveCashConv(string scheme, string parttype, string partycode, string pb, string pblvl, CashConversionDefinition _cashdef);

        [OperationContract]
        Int32 Save_TEMP_PC_LOC(string username, string com, string pc, string loc);

        //writtrn by shani on 30-09-2012
        [OperationContract]
        Int32 Delete_TEMP_PC_LOC(string userid, string com, string _pc, string loc);

        [OperationContract]
        Int32 Save_TEMP_PC_LOC_RPTDB(string username, string com, string pc, string loc);

        [OperationContract]
        Int32 Delete_TEMP_PC_LOC_RPTDB(string userid, string com, string _pc, string loc);

        [OperationContract]
        DataTable GetInvRep(string _inv);

        //kapila 3/10/2012
        [OperationContract]
        int Process_DayEnd_Commission(string _com, string _pc, DateTime _date, out string _strMsg);

        #region Accounts Detail Updates

        //writtrn by shani on 03-10-2012
        [OperationContract]
        DataTable GetHp_ActiveAccounts(string com, string pc, DateTime currentDate, DateTime fromDt, DateTime toDt, string accountNo, string status, Int32 given_esd=0);

        //writtrn by shani on 04-10-2012
        [OperationContract]
        DataTable GetHp_flag_bank_onType(string type_);

        //writtrn by shani on 04-10-2012
        [OperationContract]
        Int32 Update_Flag_Bank(string type, string newCode, List<string> accNoList);

        //writtrn by shani on 04-10-2012
        [OperationContract]
        Int32 Transfer_accounts(string newPC, DateTime transferingDate, List<string> accNoList, Int32 IntCommZero, decimal CommAmt);

        //writtrn by shani on 04-10-2012
        [OperationContract]
        Int32 Update_Account_Ownership(string NewCustCode, List<string> accNoList, DateTime curDate);

        //writtrn by shani on 04-10-2012
        [OperationContract]
        Int32 UpdateBizEntity_OnPermission(MasterBusinessEntity _businessEntity);

        //writtrn by shani on 05-10-2012
        [OperationContract]
        Int32 Update_AccCustomer(HpCustomer _HpAccountCus, List<string> AccountsList, string CustCode);

        //writtrn by shani on 05-10-2012
        [OperationContract]
        HpCustomer Get_HpAccCustomer(string custTp, string custID, Int32 addrTp, string accountNo);

        //writtrn by shani on 08-10-2012
        [OperationContract]
        Int32 Save_FlagBank(HPR_FlagBank _flagBank);

        #endregion Accounts Detail Updates

        //Written By Prabhath on 04/10/2012
        [OperationContract]
        List<InvoiceSerial> GetInvoiceSerial(string _invoiceno);

        //Written By Prabhath on 04/10/2012
        [OperationContract]
        HpSchemeDetails GetSchemeDetailAccordingToHierarchy(string _company, string _profitcenter, string _scheme, string _salesPriorityHierarchyCategory, string _salesPriorityHierarchyType);

        #region loyalty definions

        [OperationContract]
        Int32 SaveLoyaltyType(LoyaltyType _loyal);

        [OperationContract]
        Int32 SaveLoyaltyPointDefinition(LoyaltyPointDefinition _loyal, List<LoyaltyPointDefinition> _loyaltyList, List<string> _party, List<PriceBookLevelRef> _pricebook, List<CashCommissionDetailRef> item, string _itemType);

        [OperationContract]
        Int32 SaveLoyaltyRedeemDefinition(LoyaltyPointRedeemDefinition _loyal, List<string> _party);

        [OperationContract]
        Int32 SaveLoyaltyDiscountDefinition(LoyaltyPointDiscount _loyal, List<CashCommissionDetailRef> item, List<string> _party, List<PriceBookLevelRef> _pricebook, string _itemType);

        [OperationContract]
        Int32 SaveLoyaltyCustomerSpecification(LoyaltyCustomerSpecification _loyal);

        #endregion

        #region Commission Definition
        //Shani 10/10/2012
        [OperationContract]
        DataTable GetBrandsCatsItems(string selectOn, string brand, string cate1, string cate2, string cate3, string itemCode, string serialNo, string circularNo, string promotionCd);

        //kapila
        [OperationContract]
        DataTable GetBrandsCatsItems_new(string selectOn, string brand, string cate1, string cate2, string cate3, string itemCode, string serialNo, string circularNo, string promotionCd, string model);


        //Shani 11/10/2012
        [OperationContract]
        // Int32 saveTempTablesForCommision(List<string> Selected_PC_List, List<PriceBookLevelRef> PBook_List, List<CashCommissionDetailRef> ExcecutiveList, List<string> BrandCatItmList, string selectType, string username, string circularNo, Dictionary<string, Decimal> commissionValues, DateTime fromDt, DateTime toDt);
        Int32 saveTempTablesForCommision(CashCommissionHeaderRef commHeader, List<string> Selected_PC_List, List<PriceBookLevelRef> PBook_List, List<CashCommissionDetailRef> ExcecutiveList, List<CashCommissionDetailRef> BrandCatItmList, string selectType, string username, string circularNo, Dictionary<string, Decimal> commissionValues, DateTime fromDt, DateTime toDt, MasterAutoNumber masterAuto, out string commisson_code, string _selectedPcType,
            string _cusType, decimal _from, decimal _to, int _isComb, int _isEpf, decimal _amt);

        //Shani 15/10/2012
        [OperationContract]
        Int32 Save_CloneCommissions(string pc, List<string> cloningPC_list, string user);

        #endregion Commission Definition

        //kapila
        [OperationContract]
        Int32 Save_Temp_PC(string _com, string _pc, string _loc, string _user);

        //kapila
        [OperationContract]
        Int32 Delete_Temp_PC(string userid);

        //sachith
        //2012/10/15
        #region loyalty methods

        [OperationContract]
        object[] SaveLoyaltyMembership(LoyaltyMemeber _loyal, MasterAutoNumber _autoNum, MasterAutoNumber _recAuto, List<RecieptItem> _recieptItem, RecieptHeader _reciept, out string _cardNo, out string _recieptNo, out string _serialNumber, string _pc);



        [OperationContract]
        decimal GetDiscount(string _cusCode, string _loyaltytype, string _item, DateTime _date, string _loc, string _pb, string _pblvl);

        [OperationContract]
        bool UpdateLoyaltyPoints(string _cuscode, string _card, DateTime _date, string _item, string _loc, string _pb, string _pblvl, int _qty, int _val, string _salesTp, string _bank, string _cdTp);

        //2012/10/16
        [OperationContract]
        LoyaltyPointRedeemDefinition GetLoyaltyRedeemPoints(string _cusCode, string _loyaltytype, string _item, DateTime _date, string _loc, string _pb, string _pblvl);

        //2012/10/17
        [OperationContract]
        LoyaltyMemeber GetLoyaltyMember(LoyaltyMemeber _loyal, DateTime _date);

        //2012/10/17
        [OperationContract]
        bool UpdateLoyaltyMembership(LoyaltyMemeber _loyal, MasterAutoNumber _autoNum, LoyaltyMemberLog _loyalLog);

        //2012/10/17
        [OperationContract]
        Int32 SaveLoyaltyMemberLog(LoyaltyMemberLog _loyal);

        //2012/10/17
        [OperationContract]
        bool CheckIsCompulsory(string _item, DateTime _date, string _loc, string _pb, string _pbLvl);

        //2012/10/18
        [OperationContract]
        DataTable GetLoyaltyCustomerSpecifications(string _loyalType);

        //2012/10/18
        [OperationContract]
        bool LoyaltyRedeem(string _cuscode, string _card, string _loyalty, DateTime _date, decimal _points);

        //2012/10/18
        [OperationContract]
        DataTable GetBankCC(string _bank);

        //2012/10/18
        [OperationContract]
        DataTable GetLoyaltySpecifications();

        //2012/10/18
        [OperationContract]
        Int32 SaveLoyaltySpecifications(string _spec, string _creBy, DateTime _creDt);

        //2012/10/18
        [OperationContract]
        List<LoyaltyMemeber> GetLoyaltyMemberList(string _cusCd, string _cardNo, DateTime _date, string _lotyTp);


        #endregion


        //darshana
        [OperationContract]
        Boolean IsCheckLeaseCom(string _inv, string _com, string _pc, string _cate);

        //kapila
        [OperationContract]
        Boolean IsCheckLeaseCompany(string _inv, string _com, string _pc, string _cate);

        //written By Prabhath on 18/10/2012
        [OperationContract]
        List<string> DeliveryOrderNoByInvoice(string _invoice);

        //darshana 18-10-2012
        [OperationContract]
        List<HPGurantorParam> getGurParam(string _sch, string _type, string _value, DateTime _date);

        //darshana 19-10-2012
        [OperationContract]
        List<HpAccRestriction> getAccRest(string _pc, DateTime _date, Int32 _type);

        //DARSHANA 19-10-2012
        [OperationContract]
        List<HpAccount> getAccDetRest(string _com, string _pc, DateTime _fdate, DateTime _tdate);


        #region Commission Change
        //written By Shani on 18/10/2012
        [OperationContract]
        DataTable Get_invoiceItemsForCommis(string inv_no, string item_code);

        //written By Shani on 18/10/2012
        [OperationContract]
        DataTable Get_Paymodes_ofItemsForCommis(string inv_no, string item_code);

        //written By Shani on 19/10/2012
        [OperationContract]
        Int32 UpdateCommissionLine(string invoiceNo, string itemCode, Int32 itmLine, Decimal commLine, Decimal finCommRt, Decimal finCommAmt);

        //written By Shani on 19/10/2012
        [OperationContract]
        Int32 circularWise_Commission_change(string _com, List<string> _pcList, DateTime _date, string circularName, string circularCode);
        //written By Shani on 19/10/2012
        [OperationContract]
        Int32 Process_Commission_change_at_PC(string _com, List<string> _pc, List<DateTime> _dateList);

        #endregion Commission Change

        //Written By Prabhath on 19/10/2012
        [OperationContract]
        Dictionary<string, string> GetInvoiceWarrantyDetail(string _invoice);

        //Written by darshana on 21-10-2012
        [OperationContract]
        Int32 SaveHPResheduleApp(RequestApprovalHeader _reqAppHdr, RequestApprovalDetail _reqAppDet, MasterAutoNumber _reqAppAuto, out string _docNo);

        [OperationContract]
        List<RequestApprovalHeader> getReqbyType(string _com, string _pc, string _type, DateTime _fdate, DateTime _tdate);

        [OperationContract]
        List<HPResheScheme> getAllowSch(string _sch);

        [OperationContract]
        List<PriceBookLevelRef> getWarrExBook(string _com, string _pbook, string _plevel);

        //wrrin by darshana 24-10-2012
        [OperationContract]
        Int16 SaveWarrExReceipt(RecieptHeader _NewExReceipt, List<RecieptItem> _NewExReceiptDetails, List<ReceiptWaraExtend> _NewExRecWaraDetails, MasterAutoNumber _masterAutoNumber, out string DocNo);

        //writtrn by darshana 24-10-2012
        [OperationContract]
        List<ReceiptWaraExtend> GetWarrantyExtendReceipt(string _recNo);

        //Written By Prabhath on 20/10/2012
        [OperationContract]
        List<HpSheduleDetails> GetHpAccountSchedule(string _account);

        //Written By sachith on 24/10/2012
        [OperationContract]
        DataTable GetCustomerAcknowledgment(string _com, string _pc, DateTime _from, DateTime _to);

        //Written By Prabhath on 26/10/2012
        [OperationContract]
        string GetMonitorCustomer(string _company, string _profitcenter, string _parameter, string _searchtype);

        //Written By Prabhath on 26/10/2012
        [OperationContract]
        DataTable GetMonitorByCustomerDocument(string _company, string _profitcenter, string _customer);

        //kapila
        [OperationContract]
        DataTable GetSMSManagers(string _com, string _type, string _code);

        //Written By sachith on 26/10/2012
        [OperationContract]
        // DataTable GetHPCustomer(string _com, string _pc, DateTime _from, DateTime _to, string _type, string _accNo, DateTime _date, DateTime _ars, DateTime _sup, Int32 _isAll);
        DataTable GetHPCustomer(string _com, string _pc, DateTime _from, DateTime _to, string _type, string _accNo, DateTime _date, DateTime _ars, DateTime _sup, Int32 _isAll, List<InsuItem> _items, Boolean _isInv, DateTime _fromInv, DateTime _ToInv, Boolean _isReg, DateTime _fromReg, DateTime _ToReg, Boolean _isno, DateTime _fromPlate, DateTime _ToPlate, Boolean _isCR, DateTime _fromCr, DateTime _ToCr);

        //Written By Prabhath on 30/10/2012
        [OperationContract]
        DataTable GetHpGuarantor(string _type, string _account);

        [OperationContract]
        DataTable GetHpItems(string _account);

        //Written By Prabhath on 30/10/2012
        [OperationContract]
        DataTable GetAccountSummary(string _account);

        //Written By Prabhath on 30/10/2012
        [OperationContract]
        DataTable GetAccountScheduleHistory(string _account);

        //Written By Prabhath on 30/10/2012
        [OperationContract]
        DataTable GetInvoiceWithSerial(string _invoice);

        //Written By Prabhath on 30/10/2012
        [OperationContract]
        DataTable GetRevertAccountDetail(string _account);

        //Written By Prabhath on 30/10/2012
        [OperationContract]
        DataTable GetRevertReleaseAccountDetail(string _account);

        //Written By Prabhath on 30/10/2012
        [OperationContract]
        DataTable GetExchangeDetail(string _com, string _account);

        //Written By sachith on 30/10/2012
        [OperationContract]
        Int32 SaveHPReminderLetter(ReminderLetter _ltr);


        //Written By sachith on 31/10/2012
        [OperationContract]
        Int32 GetReminderLetterCount(string _accNo, string _loc, string _com, string _type, DateTime _date);


        //written by darsahana and prabhaaaaaath
        [OperationContract]
        MasterExchangeRate GetLaterstExchangeRate(string _company, string _execode, DateTime _date);



        //Written By sachith on 01/11/2012
        [OperationContract]
        Int32 SaveHPReminder(HPReminder _rmd);

        //Written By sachith on 02/11/2012
        [OperationContract]
        DataTable GetReminders(string _ref, string _stus, string _com, string _pc, string _type, DateTime _date);


        //written by darshana on 02/11/2012
        [OperationContract]
        bool IsCheckAllowFunction(string _company, string _pc, string _module, string _func);

        //Written By sachith on 08/11/2012
        [OperationContract]
        Int32 UpdateHPReminder(HPReminder _rmd);

        //Written By sachith on 02/11/2012
        [OperationContract]
        Int32 SaveReminderSMS(HPReminderSMS _sms, OutSMS _smsOut, string userId, string company, string sessionId /*,out string _err*/);


        //Written By sachith on 02/11/2012
        [OperationContract]
        HPReminderSMS GetSMSReminder(string _com, string _pc, string _acc, DateTime _date);

        //written by darshana on 06/11/2012
        [OperationContract]
        HpSchemeDetails getSchemeDetByCode(string _cd);

        //written by darshana on 09/11/2012
        [OperationContract]
        decimal Process_WaraEx_Commission(string _com, string _pc, DateTime _date, string _itm, string _pb, string _lvl, string _invTP, Int32 _line, decimal _amt, string _payMode);

        /// <summary>
        /// Written By Prabhath on 05/11/2012
        /// </summary>
        [OperationContract]
        List<InvoiceItem> GetQuotationDetail(string _company, string _profit, string _quotation, DateTime _date);

        //Written By Nadeeka 28-03-2015
        [OperationContract]
        List<InvoiceItem> GetQuotationDetailforInvoice(string _company, string _DO);

        //Written By sachith on 07/11/2012
        [OperationContract]
        int UpdateHPPrintStus(string _com, string _pc, int _stus, string _acc);

        //kapila 5/11/2012
        [OperationContract]
        int SaveGrpCompAsCustomer(MasterBusinessEntity _customer);

        //Written By Prabhath on 08/11/2012
        [OperationContract]
        Int32 CheckforInvoiceRegistration(string _company, string _profitcenter, string _invoice);

        //Written By Prabhath on 08/11/2012
        [OperationContract]
        Int32 CheckforInvoiceInsurance(string _company, string _profitcenter, string _invoice);





        #region Paymode Inquiry
        //Written By Shani on 08/11/2012
        [OperationContract]
        DataTable GetPaymodeDetail(string com, string pc, string payType, string docNo, string selectedCd, string selectedTp);

        //Written By Shani on 09/11/2012
        [OperationContract]
        DataTable getInvoicesBased_onPayType(string com, string docNo, string payType, string bankCode, string selectedTp);

        //Written By Shani on 09/11/2012
        [OperationContract]
        DataTable get_Receipts_BasedonPayType(string com, string docNo, string payType, string bankCode, string selectedTp);

        //Written By Shani on 16/11/2012
        [OperationContract]
        DataTable GetReturnCheque_detWithPayments(string chequeNo, string chequeBank);

        #endregion Paymode Inquiry

        //sachith 9/11/2012
        [OperationContract]
        Int32 SaveCustomerPassportNums(CustomerPassoprt _pass);

        //Written By Prabhath on 13/11/2012
        [OperationContract]
        DataTable GetCustomerDocumentWithSettlement(string _company, string _profitcenter, string _customer, Int16 _isOutstand);

        //Written By Prabhath on 13/11/2012
        [OperationContract]
        DataTable GetCustomerPaymentSummary(string _company, string _profitcenter, string _customer);

        //Written By sachith on 13/11/2012
        [OperationContract]
        List<HpSchemeType> GetAllSchemeTypes();

        //Written By Prabhath on 13/11/2012
        [OperationContract]
        DataTable GetInvoiceReceipt(string _invoice);

        //Written By Prabhath on 13/11/2012
        [OperationContract]
        DataTable GetHireSaleAccountBalance(string _company, string _profitcenter, string _customer, DateTime _date);

        //Written By Sachith on 14/11/2012
        [OperationContract]
        List<HpSchemeDetails> GetHPInsuranceSchemeCodes(string _schCd, string _cshTp, string _type, int _term, string _condi);

        //Written By Sachith on 14/11/2012
        [OperationContract]
        int SaveHPInsurance(List<string> _pcList, List<string> _schList, HPInsuranceScheme _scheme);

        //Written By Sachith on 14/11/2012
        [OperationContract]
        MasterExchangeRate GetExchangeRate(string _com, string _fromCur, DateTime _date, string _toCur, string _pc);


        //written by darshana on 08/08/2012
        [OperationContract]
        Int32 SaveExchangeRate(List<MasterExchangeRate> _ExchangeRate);
        //Written By Sachith on 15/11/2012
        [OperationContract]
        DataTable GetInsuranceOnEngine(string _com, string _pc, string _type, string _eng);

        //Written By Sachith on 15/11/2012
        [OperationContract]
        List<HPInsuranceScheme> GetSchemeByPCOrSchemeCode(string _ptTp, string _ptCd, string _type, string _sch, DateTime _date);

        //written by darshana 19--11--2012
        [OperationContract]
        List<PriceDetailRef> GetCombinePrice(string _company, string _profitCenter, string _invType, string _priceBook, string _priceLevel, string _customer, string _item, decimal _qty, DateTime _currentDate);
        //Written By Prabhath on 19/11/2012
        [OperationContract]
        DataTable GetCustomerAccountSchedule(string _account);

        //Written By Prabhath on 19/11/2012
        [OperationContract]
        DataTable GetCustomerAccountBalance(string _account);

        //Written By Prabhath on 19/11/2012
        [OperationContract]
        DataTable GetAccountTransfer(string _account);

        //Written By Prabhath on 19/11/2012
        [OperationContract]
        DataTable GetAccountCustomerTrasnfer(string _account);


        //Written By Prabhath on 19/11/2012
        [OperationContract]
        DataTable GetAccountDiriyaDetail(string _account);


        //Written By Prabhath on 19/11/2012
        //[OperationContract]
        //List<string> GetPaymode(string _company, string _profitcenter, string _documenttype, DateTime _date, List<InvoiceItem> _invoiceItem, List<ReptPickSerials> _serialItem);

        //Written By sachith on 22/11/2012
        [OperationContract]
        DataTable GetDelaerCommissionDetails(string _com, string _pc, DateTime _from, DateTime _to);

        //Written By sachith on 22/11/2012
        [OperationContract]
        Int32 UpdateItemCommission(List<InvoiceItem> _list);

        #region HP Parameters

        //Written By Shani on 23/11/2012
        [OperationContract]
        Int32 Save_hpr_sys_para(List<Hpr_SysParameter> paraheaders);

        //Written By Shani on 23/11/2012
        [OperationContract]
        List<Hpr_SysParameter> GetAll_hpr_Para(string code, string type, string value);

        //Written By Shani on 23/11/2012
        [OperationContract]
        DataTable Get_get_hpr_para_types(string _code);

        //Written By Shani on 24/11/2012
        [OperationContract]
        Int32 Clone_hpr_para_types(string code, string pc, List<string> pcList_toClone, string createdBy, DateTime createdDate);

        //Written By Shani on 26/11/2012
        //method belongs to Account Restriction
        [OperationContract]
        List<HpAccRestriction> GetAll_SavedAccountRestrictons(string pc, Int32 type);
        #endregion HP Parameters

        //kapila 23/11/2012
        [OperationContract]
        int SaveServiceAgentDetail(MasterBusinessEntity _customer);

        //Written By sachith on 26/11/2012
        [OperationContract]
        List<HpSchemeDetails> GetSchemaByTypeCat(string _schtp, string _schcat);

        //Written By sachith on 26/11/2012
        [OperationContract]
        Int32 SaveECDDefinition(EarlyClosingDiscount _ecd, string _pcList, string _pbList, string _pblvlList, string _schList);

        //Written by Prabhath on 26/11/2012
        [OperationContract]
        Int32 SaveRevertRelease(string _profitCenter, InventoryHeader _inventoryheader, MasterAutoNumber _inventoryAuto, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, List<RecieptHeader> _receiptHeader, MasterAutoNumber _receiptAuto, List<RecieptItem> _receiptItem, out string _inventorydoc, out string _salesdoc, bool _isPartlyPayment, decimal _accountbal, decimal _settlement, decimal _ecd, decimal _balance, List<HpTransaction> _hpTansaction, MasterAutoNumber _transactionAuto, List<HpTransaction> _hpECDTxns);


        //written by sachith on 2002/12/04
        [OperationContract]
        Int32 SaveECDVoucher(ECDVoucher _vou, string _pcList, string _schList);

        //written by sachith on 2002/12/04
        [OperationContract]
        DataTable GetECDVoucherGeneratePc(string _com, DateTime _date);

        //written by sachith 2012/12/05
        [OperationContract]
        Int32 Save_TEMP_PC_LOC_HP(string username, string com, List<string> pcList, string loc);

        //written by sachith 2012/12/06
        [OperationContract]
        Int32 ProcessECDVoucherGeneration(List<string> pcList, DateTime _from, string _com, string _user);

        //written by sachith 2012/12/06
        [OperationContract]
        DataTable GetECDNotPrintPcs(string _com, DateTime _from, DateTime _to);

        //written by sachith 2012/12/08
        [OperationContract]
        DataTable GetECDDefnSchemesFromPcsAndDates(DateTime _from, DateTime _to, string _pcList);

        //written by sachith 2012/12/08
        [OperationContract]
        DataTable GetECDDefnRateFromPcAndSchmes(DateTime _from, DateTime _to, string _pcList, string _schList);

        //written by sachith 2012/12/08
        [OperationContract]
        DataTable GetECDDefnVouFromPcAndSchAndRate(DateTime _from, DateTime _to, string _pcList, string _schList, string _rateList);

        //written by sachith 2012/12/09
        [OperationContract]
        Int32 UpdateECDVoucherPrintStatus(string _vou);


        //written by darshana 2012-12-11
        [OperationContract]
        MasterItemBrand GetItemBrand(string _cd);
                
        //written by Nadeeka 
        [OperationContract]
        // DataTable GetReduceBalInterestAccno(string _com, string _pc, string _accNo);
        DataTable GetReduceBalInterestAccno(DateTime in_ToDate, string _com, string _pc, string _accNo);

        //written by Nadeeka 2013-09-06
        [OperationContract]
        DataTable GetWarrantyCommenceDate(string _com, string _pc, string _invNo);
        //written by Nadeeka 2013-09-06
        [OperationContract]
        DataTable GetPromotionByInvoice(string _inv);
        //written by Nadeeka 2013-09-06
        [OperationContract]
        DataTable GetReceivableDetails(DateTime in_FromDate, DateTime in_ToDate, string in_user_id, string _com, string _pc, string _item, string _cat, string _stype);

        
        //written by Nadeeka 2013-09-06
        [OperationContract]
        DataTable getReturnChequeBank(string _Bank);
        //written by Nadeeka 2013-09-06
        [OperationContract]
        DataTable getReturnChequeLocHigh(string _Com, string _Type, string _Code);
        //written by Nadeeka 2013-09-16
        [OperationContract]
        DataTable getDailyExpences(DateTime _FromDate, DateTime _ToDate, string _user_id, string _company, string _code, string _pc);
        //written by Nadeeka 2013-09-06
        [OperationContract]
        DataTable getReturnChequePayments(DateTime _AsAtDate, string _RetNo);
        //written by Nadeeka 2013-09-06
        [OperationContract]
        DataTable getSalesGiftVouchaer(string _InvNo);
        
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetInsuranceAgreement(string _accNo);
        
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetDeliverCustomer(string _invNo);
        
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetSalesDetails(string _invNo, string _acc);
        
        [OperationContract]
        DataTable GetInvCreditDetails(string _invNo);

        [OperationContract]
        int generate_Voucher(string in_acc);


        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetinvSubType(string _Type, string _sType);

        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetinvBatch(string _invNo);

        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetinvEmp(string _com, string _emp);
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetinvUser(string _user);
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetinvSer(double _seq);
        //written by Nadeeka 2013-07-17
        [OperationContract]
        DataTable GetSalesHdr(string _invNo);
        //written by Nadeeka 2013-07-17
        [OperationContract]
        DataTable GetSalesDet(string _invNo);

        //written by Nadeeka 2013-07-17
        [OperationContract]
        DataTable GetInvItemCode(string _invNo);
        //written by Nadeeka 2013-07-17
        [OperationContract]
        DataTable GetInvoiceReceiptHdr(string _invNo);

        [OperationContract]
        DataTable GetPriceCombineDetails();

        [OperationContract]
        DataTable GetPriceLocationDetails();
                

        ////written by Nadeeka 2012-07-04
        //[OperationContract]
        //DataTable DeliveredSalesWithSerial(DateTime in_FromDate, DateTime in_ToDate, string in_user_id, string in_Company, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_model, string in_Profit);

        ////written by Sanjeewa 2013-10-05
        //[OperationContract]
        //string GetCutomerDetails(DateTime in_FromDate, DateTime in_ToDate, DateTime in_asatDate, string in_user_id, Int16 in_NoOfMonths, string _com, out string _err);

        ////written by Nadeeka 2014-08-27
        //[OperationContract]
        //string GetCutomerDetails_ReduceBal(DateTime in_FromDate, DateTime in_ToDate, DateTime in_asatDate, string in_user_id, Int16 in_NoOfMonths, string _com, out string _err);
                
      

        //written by Nadeeka 2013-05-14
        [OperationContract]
        DataTable ECD_vouchers_Print(string voucherNo);

        //written by Nadeeka 2013-05-14
        [OperationContract]
        DataTable GetinvTax(string _invNo);
        
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetDFCusPassport(string _inv);
                      
        
        //written by Nadeeka 2013-03-13
        [OperationContract]
        DataTable ProcessInsuranceCoverNote(string _refNo);



        //written by Nadeeka 2013-03-13
        [OperationContract]
        DataTable GetManagerDefProfit(string _com, string _epf);

       
        //written by Nadeeka 2013-01-14
        [OperationContract]
        DataTable CollectionSummaryReport(DateTime _fromDate, DateTime _toDate, string _user, string _company, string _pc, string _mgrcd);

        //written by Nadeeka 2013-01-14
        [OperationContract]
        DataTable CollectionSummaryReportECD(DateTime _fromDate, DateTime _toDate, string _user, string _company, string _pc);

        //written by Nadeeka 2013-01-14
        [OperationContract]
        DataTable CollectionSummaryReport_otherCol(DateTime _fromDate, DateTime _toDate, string _user, string _company, string _pc);
        //written by Nadeeka 2013-01-14
        [OperationContract]
        DataTable CollectionSummaryReport_other(DateTime _fromDate, DateTime _toDate, string _user, string _company, string _pc);

        //written by Nadeeka 2013-01-14
        [OperationContract]
        DataTable TransactionVarienceReport(DateTime _fromDate, DateTime _toDate, string _user, string _company, string _loc);


        //written by Nadeeka 2013-01-14
        [OperationContract]
        DataTable HPExcessShortReport(DateTime _fromDate, string _user, string _company, string _PC);

        //written by Nadeeka 2012-12-13

        [OperationContract]
        DataTable GetReceiptItemDetails(string _doc);

        [OperationContract]
        DataTable GetSalesSubTypeTable(string _type, string _stype);

        //written by Nadeeka 2013-02-06

        [OperationContract]
        DataTable HPCashFlowForecastingReport(DateTime _fromDate, string _user, string _company, string _pc);

        [OperationContract]
        DataTable GetCollectionBonusDetailsRep(string _user, DateTime _asAtDate, Int16 _issum, string _loc);

        [OperationContract]
        string GetCollectionBonusDetailsRepNew(string _user, DateTime _asAtDate, Int16 _issum, string _loc, string _company, out string _err);

        //written by Nadeeka 2013-01-19
        [OperationContract]
        DataTable GetBusinessCompanyDetailTable(string _company, string _businessCompanyCode, string _nic, string _mobile, string _businessType);


        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetHPSaleswithDO(string _acc);


        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetHPSaleswithDOItems(string _acc, string _item);

        //written by Nadeeka 2013-01-16
        [OperationContract]
        DataTable GetSevJobHeader(string _job);

        //written by Nadeeka 2013-01-16
        [OperationContract]
        DataTable GetSevJobDet(string _job);

        //written by Nadeeka 2013-01-16
        [OperationContract]
        DataTable GetSevJobCost(string _job);

        //written by Nadeeka 2013-01-15
        [OperationContract]
        DataTable GetHPRevertHeader(string _refNo);

        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable ReceivableMovemntReport(DateTime _fromDate, DateTime _toDate, string _user, string _pc, string _company, Int16 _isSum);
        //written by Nadeeka 2013-02-12
        [OperationContract]
        DataTable GPInsuranceFundReport(DateTime _fromDate, string _user, string _company, string _LOC, Int16 _isSum);

        //written by Nadeeka 2013-02-13
        [OperationContract]
        DataTable HPInsuranceReport(DateTime _fromDate, DateTime _toDate, string _user, string _company, string _LOC);

        //written by Nadeeka 2013-01-07
        [OperationContract]
        DataTable GetAccountTrans(string _acc);
        //written by Nadeeka 2013-01-07
        [OperationContract]
        DataTable GetHP_Account_AccNo(string _acc);

        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetDeliveredSerialDetails(string _invNo);        

        //written by Sanjeewa 2013-05-21
        [OperationContract]
        DataTable GetVehicalRegDetails(string _accNo);

        //written by Sanjeewa 2013-05-22
        [OperationContract]
        DataTable GetVehicalInsuranceDetails(string _accNo);

        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable ProcessVehicleInsuranceArrearsDet();        

        //written by Sanjeewa 2013-09-16
        [OperationContract]
        DataTable GetReq_App_Headings();

        //written by Sanjeewa 2013-09-16
        [OperationContract]
        DataTable GetReq_App_HeadingsView();        

        //written by Sanjeewa 2013-01-03
        [OperationContract]
        DataTable GetAccountTransactionDetails(string _accNo);        

        //written by Sanjeewa 2013-03-06
        [OperationContract]
        DataTable GetCustAcknowledgeDetails(string _User);

        ////written by Sanjeewa 2013-01-31
        //[OperationContract]
        //DataTable GetDeliveredSalesDetails(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Pc, string _Com);

        //Nadeeka 17-04-2015
        [OperationContract]
        DataTable TrPayTpDefEnquiry(string _com, string _circular, string _promocode, string _paytp, string _isasatdate, DateTime _fDate, DateTime _tDate, DateTime _asatDate, string _barnd, string _pb, string _pblevel, string _mcate, string _scate, string _pc, string _item);
        //written by Sanjeewa 2013-02-14
        [OperationContract]
        DataTable GetNoOfAccountDetails(DateTime _fromDate, DateTime _toDate, string _User, string _com, string _pc);        

        //written by Sanjeewa 2013-01-03
        [OperationContract]
        DataTable GetAccountSchedule_Acc(string _accNo);

        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetAccountDetails(string _invNo);

        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetInvoiceReceiptDet(string _invNo);

        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetInsuranceCompanyName(string _recNo);

        //written by Nadeeka 2015-01-14
        [OperationContract]
        DataTable GetInvoice_Serials(string _recNo);


        [OperationContract]
        DataTable GetExchangeReceiptHdr(string _ReqNo);

        [OperationContract]
        DataTable GetExchangeReceiptDet(string _ReqNo);

        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetAccountSchedule(string _invNo);

        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetReportInfor(string _RepName);

        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetMovementSerials(string _docNo);

        //written by Sanjeewa 2017-04-22
        [OperationContract]
        DataTable GetMovementSerials_wo_Ser(string _docNo);
        
        //written by Wimal 2016-04-16
        [OperationContract]
        DataTable GetMovementSerials_WithExpireDates(string _docNo);


        //written by Wimal 14/03/2017
        [OperationContract]
        DataTable GetCaseQty(string _docNo);

      
        //written by Wimal 14/03/2017
        [OperationContract]
        DataTable GetVehicleNo(string _docNo);


        //written by Sanjeewa 2015-08-05
        [OperationContract]
        DataTable Get_CustomerDetails(string _CustCode);

        [OperationContract]
        DataTable GetPODetails(string _docNo);

        //written by Nadeeka 2012-12-20
        [OperationContract]
        DataTable GetLocationCode(string _comCode, string _locCode);

        //written by Nadeeka 2012-12-20
        [OperationContract]
        DataTable GetItemCode(string _comCode, string _itmCode);

        //written by Nadeeka 2012-12-20
        [OperationContract]
        DataTable GetItemStatus(string _itmSts);

        //written by Nadeeka 2012-12-21
        [OperationContract]
        DataTable GetInsurance(string _accNo);


        //written by Nadeeka 2012-12-21
        [OperationContract]
        DataTable GetHpAccCustomer(string _accNo);


        //written by Nadeeka 2012-12-28
        [OperationContract]
        DataTable GetReceiptType(string _recType);

        //written by Nadeeka 2012-12-28
        [OperationContract]
        DataTable GetReceiptWarranty(string _recNo);

        //written by Nadeeka 2012-12-28
        [OperationContract]
        DataTable GetVehicalRegistrations(string _recNo);


        //written by Nadeeka 2012-12-28
        [OperationContract]
        DataTable GetAdvanRecItems(string _recNo);

        //written by Nadeeka 2012-12-28
        [OperationContract]
        DataTable GetReceipt(string _doc);

        [OperationContract]
        DataTable GetReceiptTBS(string _doc);

        //kapila
        [OperationContract]
        DataTable GetReceiptByAnal3(string _doc, string _pc, string _com);

        //written by Nadeeka 2013-01-24
        [OperationContract]
        DataTable GetSalesTax(string _inv);

        //written by Nadeeka 2013-01-02
        [OperationContract]
        DataTable GetRecDivision(string _com, string _pc);

        //Written By Prabhath on 14/12/2012
        [OperationContract]
        DataTable GetInvoiceDocumentDefinition(string _company, string _profitCenter, string _invType);

        //Written By Sachith on 19/12/2012
        [OperationContract]
        List<RecieptHeader> GetRecieptHeaderByTypeAndAccNo(string _com, string _pc, string _type, string _accno);

        //Written By Sachith on 20/12/2012
        [OperationContract]
        int SaveHpInsu(HpInsurance _hpInsu);

        //written by darshana on 20-12-2012
        [OperationContract]
        PriceBookLevelRef GetPriceLevelForHp(string _company, string _book, string _level, string _stus);

        //written by darshana on 31-12-2012
        [OperationContract]
        List<HpAccount> GetAccByCustType(string _com, string _cust, string _custTP);

        //kapila 
        [OperationContract]
        DataTable GetGlobDebtAgeOuts(string _user);

        //kapila 
        [OperationContract]
        DataTable GetGlobDebtSettle(string _user);

        //kapila 
        [OperationContract]
        DataTable GetTempPCLoc(string _user);

        #region AC Services Job
        //Written By Shani on 02/01/2013
        [OperationContract]
        DataTable GetInvoiceServiceItemSerDet(string invoiceNo, string serialID);

        //Written By Shani on 03/01/2013
        [OperationContract]
        DataTable Get_all_jobTypes(string com, string sev_chanel);

        //Written By Shani on 03/01/2013
        [OperationContract]
        DataTable Get_item_servicChargeInfo(string itmeCd, string serviceType);

        //Written By Shani on 07/01/2013
        [OperationContract]
        Int32 Save_Ac_Service_Job(MasterAutoNumber jobAuto, ServiceJobHeader jobHdr, ServiceJobDetail jobDet, MasterAutoNumber receipAuto, RecieptHeader _recieptHeader, List<RecieptItem> receipItemList, List<ServiceCostSheet> costsheets, ServiceJobStageLog stageLog, out string outPara_JobNumber, out string outPara_ReceiptNo);

        //Written By Shani on 07/01/2013
        [OperationContract]
        ServiceJobHeader Get_AC_JobHeaderOnDet(string jobno, string jobStatus);

        //Written By Shani on 08/01/2013
        [OperationContract]
        List<ServiceCostSheet> Get_Sev_CostSheets(string jobno);

        //Written By Shani on 08/01/2013
        [OperationContract]
        DataTable Get_AC_chargeItem_VAT(string com, string chgItemCd, DateTime date);

        //Written By Shani on 09/01/2013
        [OperationContract]
        Int32 Approve_Ac_Job(string jobNo, string status, string modifyBy, DateTime modifyDt, ServiceJobStageLog stageLog);

        //Written By Shani on 09/01/2013
        [OperationContract]
        Int32 UpdateManagerClaims_custPayments(List<ServiceCostSheet> chargeItemsList);

        //Written By Shani on 09/01/2013
        [OperationContract]
        List<ServiceJobDetail> Get_Sev_JobDet(string jobno);

        //Written By Shani on 10/01/2013
        [OperationContract]
        Decimal Get_MaxAC_ClaimRate(string hsyCD, string parytyTP, string partyCD, DateTime currentDt);

        //Written By Shani on 16/01/2013
        [OperationContract]
        DataTable Get_AC_jobItem_shedule(string serialID);

        //Written By Shani on 16/01/2013
        [OperationContract]
        Int32 Save_AC_JobShedule(ServiceJobShedule shed);

        //Written By Shani on 17/01/2013
        [OperationContract]
        Int32 Update_AC_jobItem_shedule(string itemCd, string serialID, string job_no, Boolean stuts);

        //Written By Shani on 22/01/2013
        [OperationContract]
        DataTable GetInvoiceServiceItemSerDet_POS(string invoiceNo, string serialNo);

        //Written By Darshana on 26/02/2013
        [OperationContract]
        DataTable GetInvoiceServiceItemSerDet_Oth(string invoiceNo, string serialNo);

        //Written By Shani on 01/03/2013
        [OperationContract]
        DataTable Get_item_servicChargeInfo_New(string itmeCd, string serviceType, List<string> seviceItemCode);
        #endregion AC Services Job

        //Darshana 04/01/2013
        [OperationContract]
        MasterBusinessEntity GetCustomerProfileByCom(string CustCD, string nic, string DL, string PPNo, string brNo, string com);

        //written by Prabhath on 04/01/2013
        [OperationContract]
        Int32 SaveCashGeneralEntityDiscountWindows(string _smsDocType, string _company, string _location, string _documentno, string _user, List<CashGeneralEntiryDiscountDef> _list, string _customer, decimal _discountRate, string _reqRemarks="");

        //written by darshana 16/01/2013
        [OperationContract]
        Int16 ReceiptCancelProcess(RecieptHeader _UpdateRec, List<RecieptItem> _recItem, List<VehicalRegistration> _regList, List<VehicleInsuarance> _insList, List<GiftVoucherPages> _gvlist, List<ReceiptItemDetails> _itmDet);

        //written by darshana 16/01/2013
        [OperationContract]
        Int16 ReceiptCancelProcessTBS(RecieptHeaderTBS _UpdateRec, List<RecieptItemTBS> _recItem, List<VehicalRegistration> _regList, List<VehicleInsuarance> _insList, List<GiftVoucherPages> _gvlist, List<ReceiptItemDetails> _itmDet);

        //kapila
        [OperationContract]
        Int16 VehRegReceiptCancelProcess(List<VehicalRegistration> _regList);

        //darshana 17/01/2013
        [OperationContract]
        DataTable Process_Hp_Closing_Bal(string _com, string _user, string _schTp, string _schCD, DateTime _asAT);

        //Written By Prabhath on on  22/01/2013
        [OperationContract]
        DataTable GetMonthlyDueWithAccountSummary(string _account);

        //Written by Prabhath on  22/01/2013
        [OperationContract]
        int SaveBlackListCustomer(BlackListCustomers _customer);

        //Written by darshana on 23/01/2013
        [OperationContract]
        List<RequestApprovalHeader> getPendingReqbyType(string _com, string _pc, string _type, string _user, string _selectPC);

        //written by darshana on 23/01/2013
        [OperationContract]
        SystemAppLevelParam CheckApprovePermission(string type, string user);
        //Written by Prabhath on  22/01/2013
        [OperationContract]
        int ReleaseBlackListCustomer(string _customer, string _company, string _profitcenter, string _isheadofficelevel);

        //darshana 24/01/2013
        [OperationContract]
        List<InvoiceItem> GetInvoiceDetailsForReversal(string _inv, string _tp);

        //Chamal 30/04/2014
        [OperationContract]
        List<InvoiceItem> GetInvoiceDetailsForIntrPRN(string _inv, string _tp, string _baseDoc);

        //Chamal 29/04/2013
        [OperationContract]
        List<InvoiceItem> GetInvoiceDetailsForReversalSCM(string _inv, string _tp, string _baseDoc);

        //darshana 29-01-2013
        [OperationContract]
        List<InvoiceItem> GetRevDetailsFromRequest(string _inv, string _tp, string _com, string _pc, string _reqNo);

        //darshana 29-01-2013
        [OperationContract]
        Int32 SaveSaleRevReqApp(RequestApprovalHeader _AppHdr, List<RequestApprovalDetail> _AppDet, List<RequestApprovalSerials> _AppSer, MasterAutoNumber _AppReqAuto, RequestApprovalHeader _RegHdr, List<RequestApprovalDetail> _RegDet, List<RequestApprovalSerials> _RegSer, MasterAutoNumber _AppRegAuto, RequestApprovalHeaderLog _AppHdrLog, List<RequestApprovalDetailLog> _AppDetLog, List<RequestApprovalSerialsLog> _AppSerLog, RequestApprovalHeaderLog _RegHdrLog, List<RequestApprovalDetailLog> _RegDetLog, List<RequestApprovalSerialsLog> _RegSerLog, Boolean _isRegReq, RequestApprovalHeader _insHdr, List<RequestApprovalDetail> _insDet, List<RequestApprovalSerials> _insSer, MasterAutoNumber _AppinsAuto, RequestApprovalHeaderLog _insHdrLog, List<RequestApprovalDetailLog> _insDetLog, List<RequestApprovalSerialsLog> _insSerLog, Boolean _isinsReq, out string _docNo, out string _regReq, out string _insReq, List<RequestAppAddDet> _tmpAddDet);

        //Shani 05-02-2013
        [OperationContract]
        List<RecieptHeader> GetReceiptHdr(string _receiptNo);

        //kapila
        [OperationContract]
        List<RecieptHeader> GetReceiptHdrByAnal3(string _receiptNo, string _pc, string _com);

        //sachith 08-02-2013
        [OperationContract]
        CollectionBonusDefinition GetCollectionBonus(decimal _arrFrom, decimal _arrTo, int _locFrom, int _locTo);

        //sachith 08-02-2013
        [OperationContract]
        int SaveCollectionBonusDefinition(CollectionBonusDefinition _col);

        //sachith 08-02-2013
        [OperationContract]
        List<CollectionBonusDefinition> GetAllCollectionBonusDefinition();

        //sachith 08-02-2013
        [OperationContract]
        int UpdateCollectionBonus(CollectionBonusDefinition _col, decimal _arsFrom, decimal _arsTo, int _locFrom, int _locTo);

        //sachith 11-02-2013
        [OperationContract]
        int SaveManagerCreation(ManegerCreation mgr);

        //sachith 11-02-2013
        [OperationContract]
        DataTable GetManagerCreation(string _com, string _pc, string _mgr);

        //sachith 11-02-2013
        [OperationContract]
        int UpdateManagerCreation(ManegerCreation mgr);

        //kapila 11/2/2013
        [OperationContract]
        DataTable GetPC_from_Hierachy_Rep_All(string com, string channel, string subChannel, string area, string region, string zone, string pc_code);

        // Nadeeka 19-05-2015
        [OperationContract]
        DataTable GetPC_from_Hierachy_SubChannel(string com, string channel, string subChannel, string area, string region, string zone, string pc_code);

        // Nadeeka 04-09-2015
        [OperationContract]
        Int32 SavePriceBookDefinition(sar_pb_def _pbdef, List<sar_pb_def_det> _pbdet, out string _docNo);

        //sachith 11/2/2013
        [OperationContract]
        DataTable GetEmployee(string _company, string _employee);


        //Written by Prabhath on 13/02/2013
        [OperationContract]
        List<InvoiceItem> GetInterTransferInvoice(string _invoice);




        //sachith 12/2/2013
        [OperationContract]
        int DeleteManager(string _com, string _pc, string _mgr);

        //sachith 13/2/2013
        [OperationContract]
        int SaveDisregardValueDefinition(DisregardValueDefinition val);

        //sachith 13/2/2013
        [OperationContract]
        int UpdateDisregardValueDefinition(DisregardValueDefinition val, decimal _from, decimal _to, decimal _val, int _type);

        //sachith 13/2/2013
        [OperationContract]
        List<DisregardValueDefinition> GetDisregardValueDefinitiom(decimal _from, decimal _to, decimal _value, int _type, string _select);

        //sachith 13/2/2013
        [OperationContract]
        int SaveDisregardPCDefinition(DisregardPCDefinition pc);

        //sachith 13/2/2013
        [OperationContract]
        int UpdateDisregardPCDefinition(DisregardPCDefinition pc, string _com, string _pc);

        //sachith 13/2/2013
        [OperationContract]
        List<DisregardPCDefinition> GetDisregardPCDefinitiom(string _com, string _pc, string _type);

        //sachith 14/2/2013
        [OperationContract]
        int SaveHandlingOveAccounts(HandlingOverAccount hand);

        //sachith 14/2/2013
        [OperationContract]
        int UpdateHandlinOverAccount(HandlingOverAccount hand, string _com, string _pc, string _ac);

        //sachith 14/2/2013
        [OperationContract]
        List<HandlingOverAccount> GetHandlingOverAccounts(string _com, string _pc, string _ac, DateTime _bonus, string _type);

        //kapila
        [OperationContract]
        Int32 UpdateDayEnd(string _com, string _pc, DateTime _date);

        //darshana 15-02-2013
        [OperationContract]
        Int16 ReceiptCancelInfo(RecieptHeader _UpdateRec, List<VehicalRegistration> _vehReg, List<VehicleInsuarance> _vehIns);

        //darshana 15-02-2013
        [OperationContract]
        Int16 ReceiptCancelInfoTBS(RecieptHeaderTBS _UpdateRec, List<VehicalRegistration> _vehReg, List<VehicleInsuarance> _vehIns);

 
        //sachith 2012/02/20
        [OperationContract]
        DataTable GetCollectionBonusByDate(string _com, string _pc, DateTime _date);

        //Written By Prabhath on 21/02/2013
        [OperationContract]
        DataTable GetPriceCombinedItemTable(Int32 _priceBookSeq, Int32 _priceitemseq, string _mainItem, string _mainSerial);

        //Written By Prabhath on 21/02/2013
        [OperationContract]
        DataTable GetAllCombineItemDetail();

        //sachith 2012/02/20
        [OperationContract]
        DataTable GetArrearsScheme(DateTime _date, string _scheme);

        //sachith 2012/02/20
        [OperationContract]
        List<CollectionBonusAdjusment> GetCollectionBonusAdjusment(string _com, string _pc, string _acc, string _type);

        //sachith 2012/02/21
        [OperationContract]
        int SaveCollectionBonusAdjusment(CollectionBonusAdjusment adj);

        //Written By darhsna on 27/02/2013
        [OperationContract]
        Int32 Save_Ac_Service_Job_New(MasterAutoNumber jobAuto, ServiceJobHeader jobHdr, ServiceJobDetail jobDet, MasterAutoNumber receipAuto, RecieptHeader _recieptHeader, List<RecieptItem> receipItemList, List<ServiceCostSheet> costsheets, ServiceJobStageLog stageLog, ServiceJobAlloc JobAlloc, out string outPara_JobNumber, out string outPara_ReceiptNo);

        //kapila 28/2/2013
        [OperationContract]
        Boolean IsCheckSalesComm(Int32 _seqno);


        //Written By Prabhath on 2/3/2013
        [OperationContract]
        DataTable GetVehicleInsurance(string _account);

        //darshana 03-03-2013
        [OperationContract]
        List<VehicalRegistration> GetVehRegForRev(string _com, string _pc, string _inv, string _itm, Int32 _status);

        //Written By shani on 5/3/2013
        [OperationContract]
        List<RecieptItem> Get_receiptitm_OnPayTp(string seq, string payTp, string receiptNo);

        //Written By Prabhath on 6/3/2013
        [OperationContract]
        DataTable LoadWarrantyClaimCreditNote(string _company, string _profitcenter);
        // Nadeeka  24-06-2015
        [OperationContract]
        DataTable LoadWarrantyClaimCompany(string _company, string _supp);

        //Written By sachith on 6/3/2013
        [OperationContract]
        List<VehicleInsuarance> GetVehicalInsuranceByInvoice(string _inv);

        //kapila
        [OperationContract]
        DataTable getSerialByInvLine(string _inv, Int32 _line);

        //kapila
        [OperationContract]
        Boolean IsRegInsuAllowed(string _com, string _type, string _itmStus, string _saleType);

        //Written By sachith on 6/3/2013
        [OperationContract]
        int ProcessHPReschedule(HpAccount _Oldaccount, HpAccount _newAccount, List<HpSheduleDetails> _newSchedule, RequestApprovalHeader _request, List<RecieptHeader> _recieptList, List<RecieptItem> _recieptItemList, List<HpTransaction> _transactionList, MasterAutoNumber _recieptAuto, MasterAutoNumber _transactionAuto, DateTime _date, out string _recNo, out string _error);

        //Written By Prabhath on 6/3/2013
        [OperationContract]
        DataTable GetSCMInvoice(string _company, string _customer, string _item);

        //Written By Prabhath on 6/3/2013
        [OperationContract]
        DataTable GetSCMInvoiceSerial(string _company, string _invoice, string _item);

        //Written By Prabhath on 7/3/2013
        [OperationContract]
        DataTable GetInvoiceDetail(string _company, string _invoice, string _item);


        //kapila 7/3/2013
        [OperationContract]
        DataTable GetLoc_from_Hierachy_Rep_all(string com, string channel, string subChannel, string area, string region, string zone, string pc_code);

        //Written By sachith on 8/3/2013
        [OperationContract]
        DataTable GetCashCommissionserach(string circular);

        //Written By Prabhath on 9/3/2013
        [OperationContract]
        int GenerateWarrantyClaimCredit(DateTime _date, string _invType, string _requestno, string _customer, string _remarks, string _receiveCompany, string _receiveProfitCenter, string _receiveLocation, string _createBy, InventoryHeader _adjPlusHeader, MasterAutoNumber _adjPlusAuto, List<RequestApprovalDetail> _itemLst, string _sessionID, out string _creditNote, out string _adjPlusDocument, out int _effects, out string _msg, string _sapNo = null, string _claimNo = null); // updated by akila 2017/09/02

        //kapila 11/3/2013
        [OperationContract]
        Boolean IsCheckServiceAgent(string _com, string _cd);

        //sachith 11/03/2013
        [OperationContract]
        int CashConvertionApproval(RequestApprovalHeader _regReqHdr, List<RequestApprovalDetail> _regReqDet, List<RequestApprovalSerials> _regReqSer, RequestApprovalHeaderLog _regReqLogHdr, List<RequestApprovalDetailLog> _regReqLogDet, List<RequestApprovalSerialsLog> _regReqLogSer, MasterAutoNumber _regReqAuto, bool _regNeed, RequestApprovalHeader _insReqHdr, List<RequestApprovalDetail> _insReqDet, List<RequestApprovalSerials> _insReqSer, RequestApprovalHeaderLog _insRegLogHdr, List<RequestApprovalDetailLog> _insRegLogDet, List<RequestApprovalSerialsLog> _insReqLogSer, MasterAutoNumber _insReqAuto, bool _insNeed, List<RecieptHeader> _regReciept, List<RecieptItem> _regRecieptItem, MasterAutoNumber _regRecieptAuto, bool _regRecNeed, List<RecieptHeader> _insReciept, List<RecieptItem> _insRecieptItem, MasterAutoNumber _insRecieptAuto, bool _insRecNeed, bool _completeReg, bool _completeIns);

        //kapila 11/3/2013
        [OperationContract]
        DataTable GetInvWarrDet(string _InvNo, Int32 _seq, Int32 _itmLine, Int32 _batchLine, Int32 _serLine);

        #region receipt refund -Insurance / Registration

        //Written By shani on 12/3/2013
        [OperationContract]
        DataTable Get_vehinsubyRef(string refNo);

        //Written By shani on 14/3/2013
        [OperationContract]
        Int32 Refund_Insurance(RecieptHeader _ReceiptHeader, RecieptItem recieptItem, MasterAutoNumber _recNo, string reciptNo, string itemCd, string chassis, string engine, string modby, DateTime modDt, out string _ReciptNo);

        //Written By shani on 14/3/2013
        [OperationContract]
        Int32 Refund_Registration(RecieptHeader _ReceiptHeader, RecieptItem recieptItem, MasterAutoNumber _recNo, string reciptNo, string itemCd, string chassis, string engine, string modby, DateTime modDt, out string _ReciptNo);

        //Written By shani on 14/3/2013
        [OperationContract]
        DataTable Get_vehRegbyref(string refNo);

        #endregion


        //darshana 05-03-2013
        [OperationContract]
        List<VehicalRegistration> GetVehicalRegByRefNo(string _recNo);

        //darshana 05-03-2013
        [OperationContract]
        List<VehicleInsuarance> GetVehicalInsByRefNo(string _recNo);

        //written by darshana on 05/03/2013
        [OperationContract]
        Int32 SaveReversalNew(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, MasterAutoNumber _invoiceAuto, Boolean _isHP, out string _invoiceNo, InventoryHeader _inventoryMovementHeader, List<ReptPickSerials> _reptPickSerials, List<ReptPickSerialsSub> _reptPickSerialsSub, MasterAutoNumber _masterAutoNumber, MasterAutoNumber _regRevAuto, List<RecieptHeader> _regRecHdr, List<VehicalRegistration> _regRevDet, Boolean _isregRefund, MasterAutoNumber _insRevAuto, List<RecieptHeader> _insRecHdr, List<VehicleInsuarance> _insRevDet, Boolean _isinsRefund, Boolean _isOthSaleRev, string _RevPC, RequestApprovalHeader _refAppHdr, List<RequestApprovalDetail> _refAppDet, MasterAutoNumber _refAppAuto, RequestApprovalHeaderLog _refAppHdrLog, List<RequestApprovalDetailLog> _refAppDetLog, Boolean _isCashRef, out string _docNo, bool _IsTemp = false);

        //written by darshana on 12-03-2013
        [OperationContract]
        List<VehicalRegistration> GetRefundReqVehReg(string _com, string _pc, string _ref, string _appTp);

        //darshana 14-03-2013
        [OperationContract]
        List<VehicleInsuarance> GetVehInsForRev(string _com, string _pc, string _inv, string _itm, Int32 _status);

        //written by darshana on 14-03-2013
        [OperationContract]
        List<VehicleInsuarance> GetRefundReqVehIns(string _com, string _pc, string _ref, string _appTp);

        //Written By shani on 12/3/2013
        [OperationContract]
        HPAccountLog GetAccountLog_LatestRecord(string _account);

        //Written By sachith on 19/3/2013
        [OperationContract]
        List<MasterBusinessEntityInfo> GetCustomerSegmentation(string _cus);

        [OperationContract]
        MasterBusinessEntity Get_HpAccCustomer_NEW(string custTp, string custID, Int32 addrTp, string accountNo);

        //Written By Chamal on 29/03/2013
        [OperationContract]
        int UpdateInvoiceSimilarItemCode(string _invoiceNo, int _lineNo, string _itemCode, string _similarItemCode);

        //Written By sachith on 20/3/2013
        [OperationContract]
        int SaveAccountRescheduleRequestApproval(MasterAutoNumber _auto, RequestApprovalHeader _hdr, List<RequestApprovalDetail> _det, RequestApprovalHeaderLog _hdrlog, List<RequestApprovalDetailLog> _delLog, int UserPermissionLevel, bool _isApprovalUser, bool _isRequestGenerateUser, out string Reference);

        //Written By Prabhath on 27/03/2013
        [OperationContract]
        List<PriceDetailRef> GetPriceEnquiryDetail(string _profitcenter, int _startFrom, int _endFrom, string _user, string _company, string _priceBook, string _priceLevel, string _customer, string _item, string _category1,
            string _category2, string _category3, string _status, string _pricetype, string _circular, DateTime _fromDate, DateTime _toDate, bool _isHistory, bool _isAsAtHistory,
            bool _isAllStatus, bool _isSuperUser, string _promocd);

        //Written By Prabhath on 27/03/2013
        [OperationContract]
        List<PriceSerialRef> GetPriceEnquirySerialDetail(string _profitcenter, int _startFrom, int _endFrom, string _user, string _company, string _priceBook, string _priceLevel, string _customer, string _item, string _category1,
            string _category2, string _category3, string _status, string _pricetype, string _circular, DateTime _fromDate, DateTime _toDate, bool _isHistory, bool _isAsAtHistory,
            bool _isAllStatus, bool _isSuperUser);

        //Written By Prabhath on 03/04/2013
        [OperationContract]
        List<MasterSalesPriorityHierarchy> GetSalesPriorityHierarchyWithDescription(string _company, string _profitcenter);

        //Written By Nadeeka on 03/06/2014
        [OperationContract]
        List<MasterSalesPriorityHierarchyLog> GetSalesPriorityHierarchyWithDescription_log(string _company, string _profitcenter, DateTime _date);

        //Written By shani on 04/4/2013
        [OperationContract]
        DataTable get_priceDet_ForAC_sevChgItG(string _company, string chgItemCode, DateTime today);

        //written by darshana on 05/04/2013
        [OperationContract]
        HpSchemeType getSchemeTypeByTypeAndCate(string _cate, string _type);

        //writtn by darshana on 05/04/2013
        [OperationContract]
        Int16 UpdateSchemeType(HpSchemeType _schemeType, string _user, Int32 _defTerm);


        //writtn by darshaan on 05-04-2013
        [OperationContract]
        Int16 SaveNewSchemeType(HpSchemeType _schemeType);


        //written by darshana on 09-04-2013
        [OperationContract]
        Int16 CreateNewSchemeDetails(HpSchemeDetails _NewSchDet, List<HPAddSchemePara> _VouDet, List<HPAddSchemePara> _SchAddPara);

        //written by darshana on 09-04-2013
        [OperationContract]
        Int16 UpdateExsistSchemeDetails(HpSchemeDetails _UpdateSchDet, List<HPAddSchemePara> _VouDet, List<HPAddSchemePara> _SchAddPara);

        //Written By Prabhath on 09/04/2013
        [OperationContract]
        List<MasterLocationPriorityHierarchy> GetLocationPriorityHierarchyWithDescription(string _company, string _location);

        //Written By shani on 10/09/2013
        [OperationContract]
        List<PaymentTypeRef> Get_enqiry_paytypes(bool isAll);

        //Writtn by darshana on 10-04-2013
        [OperationContract]
        List<HpSchemeSheduleDefinition> Get_Define_Scheme_Shedule(string _schCode);

        //written by darshana on 10-04-2013
        [OperationContract]
        Int16 CreateNewSchemeSheduleDefinition(List<HpSchemeSheduleDefinition> _SheduleList, string _schCode, List<HPResheScheme> _reShe);

        //written by darshana on 12-04-2013
        //[OperationContract]
        //Int16 SaveNewSchemeCommDefinition(List<HpSchemeDefinition> __schSchemeCommDef,string _cir);

        [OperationContract]
        Int16 SaveNewSchemeCommDefinition(string _user, string _cir);

        //Written By Prabhath on 12/04/2013
        [OperationContract]
        Int32 SaveAdditionalProductBonus(List<MasterAdditionalProductBonus> _productBonus, List<string> pc_List);

        //Written By Prabhath on 12/04/2013
        [OperationContract]
        List<MasterAdditionalProductBonus> GetAllProductBonusSetup(string _company);

        //written by darshana on 16/04/2013
        [OperationContract]
        MasterItemSubCate GetItemSubCate(string _cd);

        //written by darshana on 16/04/2013
        [OperationContract]
        List<MasterItem> GetItemsByCateAndBrand(string _mainCate, string _subCate, string _itmRange, string _brand, string _com);

        //written by darshana on 16-04-2013
        [OperationContract]
        List<PriceDetailRef> GetPriceDetailsByCir(string _book, string _level, string _promo, string _cir);

        //written by shani on 17-04-2013
        [OperationContract]
        List<MasterExchangeRate> GetValid_ExchangeRates(string _com, string fromCur, string toCur, DateTime fromDt);

        //written by darshana on 17-04-2013
        [OperationContract]
        Int16 SaveHpGurantorsParam(List<HPGurantorParam> _hpGurParam);

        //written by darshana on 17-04-2013
        [OperationContract]
        Int16 SaveHpOtherChargeDef(List<HpOtherCharges> _hoOthCha);

        //written by darshana on 18-04-2013
        [OperationContract]
        List<HpSchemeDetails> getAllActiveSchemes(string _cd);

        //written by darshana on 19-04-2013
        [OperationContract]
        Int16 SaveServiceChgDef(List<HpServiceCharges> _serChg);

        //written by darshana on 19-04-2013
        [OperationContract]
        List<PriceSerialRef> getSerialpriceDetailsForCir(string _itm, string _cir);

        //written by darshana on 19-04-2013
        [OperationContract]
        Int16 SaveProofDoc(List<HpProofDoc> _tmpProofDoc);

        //written by darshana on 20-04-2013
        [OperationContract]
        MasterProofDocs GetMasterProofDoc(string _cd);

        //written by shani on 22-04-2013
        [OperationContract]
        Decimal AvailableForRefund(string manualno);

        //written by darshana on 29-04-2013
        [OperationContract]
        DataTable GetTotalRevAmtByInv(string _invNo);

        //by darshana on 30-04-2013
        [OperationContract]
        // List<HpSchemeDefinition> getSchemeByPC(string _pc, DateTime _dt);
        DataTable getSchemeByPC(string _pc, DateTime _dt);

        //by darshana on 04-05-2013
        [OperationContract]
        Int16 SavePriceDetails(List<PriceDetailRef> _priceDet, List<PriceCombinedItemRef> _priceDetCom, MasterAutoNumber _priceAuto, List<PriceProfitCenterPromotion> _appPCList, List<PriceSerialRef> _serialPrice, PriceDetailRestriction _restriction, out string _err, string session, string user, string _com, List<Circular_Schemes> _circSchList, Int32 _isInfCrd, string _remark, SAR_PB_CIREFFECT _SAR_PB_CIREFFECT=null);

        //darshana on 05-05-2013
        [OperationContract]
        DataTable GetPromoCodesByCir(string _cir, string _promo, string _pb, string pbl);

        //darshana on 06-05-2013
        [OperationContract]
        List<PriceDetailRef> GetPricebyCirandPromo(string _cir, string _promo);

        //darshana on 06-05-2013
        [OperationContract]
        List<PriceProfitCenterPromotion> GetAllocPromoPc(string _com, string _promo, Int32 _pbSeq);

        //darshana on 06-05-2013
        [OperationContract]
        int SaveAppPromoPc(List<PriceProfitCenterPromotion> _appPromoPc);

        //darshana on 07-05-2013
        [OperationContract]
        List<PriceDetailRef> GetPriceByPromoCD(string _promo);

        //darshana on 07-05-2013
        [OperationContract]
        InvoiceHeader GetInvoiceHdrByCom(string _com, string _inv);

        //darshana on 07-05-2013
        [OperationContract]
        Int16 SaveSimilarItems(List<MasterItemSimilar> _SaveList);

        //darshana on 07-05-2013
        [OperationContract]
        Int16 UpdateSimilarItems(List<MasterItemSimilar> _UpdateList);

        //darshana on 08-05-2013
        [OperationContract]
        List<MasterItemSimilar> GetSimilarSetupDet(string _com, string _itm, string _tp);

        //written by Shani on 07-05-2013
        [OperationContract]
        Int32 Save_ECD_definition(List<string> _hierchyList, List<string> _schList, List<string> _pbookList, List<string> _pbList, EarlyClosingDiscount _ecdDefinition);
        //Written By Prabhath on 08/05/2013
        [OperationContract]
        Int32 SaveInvoiceDuplicate(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, List<InvoiceVoucher> _voucher, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo);

        //written by darshana on 09-05-2013
        [OperationContract]
        Int32 SavePcPriceDefinition(List<PriceDefinitionRef> _priceDef);

        //written by shani on 10-05-2013
        [OperationContract]
        List<HpSchemeDetails> GetSchemeByType_orCode(string _schtp, string _schcode);

        //written by darshana on 10-05-2013
        [OperationContract]
        Int32 RemovePriceAccess(string p_pc, string p_invTp, string p_lvl, string p_com, string p_pb, string p_usr);

        //written by shani on 10-05-2013
        [OperationContract]
        List<HpSchemeDetails> GetSchmeDet_on_term_tp(string _schtp, string _schcode, string term);

        //written by shani on 10-05-2013
        [OperationContract]
        Int32 Save_hpr_ecd_vou_defn(List<string> schemeList, List<string> pc_list, ECDVoucher evdVouDef);

        //written by shani on 11-05-2013
        [OperationContract]
        DataTable Get_ecd_vou_defn_PClist(DateTime fromDt, DateTime toDt);

        //written by shani on 11-05-2013
        [OperationContract]
        DataTable Get_ecd_vou_defn_SchmList(List<string> pc_list, DateTime fromDt, DateTime toDt);

        //Written By Prabhath on 11-05-2013
        [OperationContract]
        DataTable GetInvoiceVoucher(string _invoice, string _item);

        //written by shani on 11-05-2013
        [OperationContract]
        Int32 Process_hpr_ecd_vouchers(MasterAutoNumber masterAuto, List<Int32> schemeSeqList, EarlyClosingDiscount evdVouDef, DateTime current_date, DateTime fromDt, DateTime toDt, out List<string> voucherNoList, out string _error);

        //written by Sanjeewa 2016-06-02
        [OperationContract]
        Int32 send_SMS_ecd_vouchers(string _com, string _cust, string _acc, string _vou, string _msg, string _user, out string _error);

        //written by shani on 13-05-2013
        [OperationContract]
        DataTable Get_voucher_details(List<string> voucherNoList);

        //writtn by darshana on 13-05-2013
        [OperationContract]
        Int32 UpdateGiftVoucherValidDate(DateTime p_validto, string p_modby, string p_book, string p_page, string p_pc, string p_item, string p_com, string p_status, Int32 p_amendNoofItems);

        //written by darshaa non 14-05-2013
        [OperationContract]
        Int32 UpdateGiftVoucherStatus(string p_com, string p_pc, string p_book, string p_page, string p_item, Int16 p_status);

        //written by shani on 14-05-2013
        [OperationContract]
        DataTable Get_vouchers_to_Print(List<string> pc_List);

        //written by shani on 14-05-2013
        [OperationContract]
        DataTable Get_ecdDefnVoucher_PClist(DateTime fromDt, DateTime toDt);

        //written by shani on 14-05-2013
        [OperationContract]
        DataTable Get_ecd_vou_defn_For_Print_SchmList(List<string> pc_list, DateTime fromDt, DateTime toDt);

        //written by shani on 14-05-2013
        [OperationContract]
        DataTable Get_VoucherOnSchemeRate(List<string> pc_list, List<string> scheme_list, DateTime fromDt, DateTime toDt, List<Decimal> rateOrAmtList);

        //written by shani on 14-05-2013
        [OperationContract]
        Int32 Update_ECD_Voucher_printStatus(string voucherNo, string printBy, DateTime printDate);

        //written by darshana on 15-05-2013
        [OperationContract]
        InvoiceHeader get_CrNote(string _inv, string _com, string _pc);

        //Written by darshana on 16/05-2013
        [OperationContract]
        Int32 SaveCashRefundReqApp(RequestApprovalHeader _AppHdr, List<RequestApprovalDetail> _AppDet, MasterAutoNumber _AppReqAuto, RequestApprovalHeaderLog _AppHdrLog, List<RequestApprovalDetailLog> _AppDetLog, out string _docNo);


        //wrrittn by darshana on 16-05-2013
        [OperationContract]
        Int16 SaveCashRefund(RecieptHeader _NewReceipt, RecieptItem _NewReceiptDetails, MasterAutoNumber _masterAutoNumber, MasterAutoNumber _masterAutoNumberType, out string docno);

        //written by sachith on 15-05-2013
        [OperationContract]
        int SaveDiscountDefinition(CashPromotionDiscountHeader _hdr, CashPromotionDiscountDetail _detail, List<string> _locList, DataTable _itemList, List<string> _salesTypeList, List<PriceBookLevelRef> _pbList, List<PaymentTypeRef> _payType, int _itemtp, out int _hdrCount, out int _detCount, out int _itemCount, out int _locCount, string _com, string _pc, out string err, string user, string session);
        //written by sachith on 16-05-2013
        [OperationContract]
        int UpdatePromotionlDiscountStatus(string _cir, string user, string session);

        //Written by Prabhath on 16/05/2013
        [OperationContract]
        DataTable GetPriceTypeByIndent(int _indentseq);

        //written by sachith on 16-05-2013
        [OperationContract]
        DataTable GetVehicalRegistrationReciept(string _acc);

        //written by darshana on 20-05-2013
        [OperationContract]
        List<HpSchemeDefinition> GetSchemeDetailsByCir(string _cir);

        //by darshana on 21-05-2013
        [OperationContract]
        Int32 UpdateSchemeStatus(string _cir, string _stus, string _usr, List<HpSchemeDefinitionLog> _DefLog);

        //darshana on 22-05-2013
        [OperationContract]
        List<HpSchemeDefinition> GetAllSchemeCirculars();

        //darshana on 22-05-2013
        [OperationContract]
        Int32 SaveGeneralSchemeUpdation(List<HpSchemeDefinitionLog> _DefLog, DateTime _dt, decimal _rate, Int16 _isRate, string _usr, string _type, string _stus);


        //darshana on 28-05-2013
        [OperationContract]
        Int32 UpdateAddPricingParam(string _user, Boolean _age, string _msg, Boolean _cus, string _lvl, string _book, string _com);

        //darshana on 01-06-2013
        [OperationContract]
        //Int16 SaveNewSchemeCommProcess(List<HpSchemeDefinitionProcess> _schSchemeCommDef);
        Int16 SaveNewSchemeCommProcess(DataTable _schSchemeCommDefdt);

        //darshana on 02-06-2013
        [OperationContract]
        int DeleteHPSchProcess(string _user);

        //darshana on 04-06-2013
        [OperationContract]
        DataTable GetSchItembyCir(string _cir);

        //DARSHANA ON 04-06-2013
        [OperationContract]
        DataTable GetSchPCbyCir(string _cir);


        //darshana on 04-06-2013
        [OperationContract]
        DataTable GetSchShedulebyCir(string _cir);

        //darshana on 04-06-2013
        [OperationContract]
        DataTable GetItemsByCateAndBrandNew(string _mainCate, string _subCate, string _itmRange, string _brand, string _com);

        //darshana on 04-06-2013
        [OperationContract]
        DataTable GetSchPromobyCir(string _cir);

        //darshan on 04-06-2013
        [OperationContract]
        DataTable GetSchCusbyCir(string _cir);

        //sachith on 05-06-2013
        [OperationContract]
        LoyaltyType GetLoyaltyType(string _type);


        //sachith on 06-06-2013
        [OperationContract]
        LoyaltyPointRedeemDefinition GetLoyaltyRedeemDefinition(string prtTp, string prt, DateTime date, string loyalty);

        //darshana on 06-06-2013
        [OperationContract]
        GroupSaleCustomer GetGroupSaleDetByCus(string _grpCd, string _grpCus);

        //sachith on 07-06-2013
        [OperationContract]
        DataTable GetHPAccountFromDate(DateTime _from, DateTime _to, string _com, string _pc);
        //sachith on 07-06-2013
        [OperationContract]
        DataTable GetCircularNo(string _circular);

        //darshana on 08-06-2013
        [OperationContract]
        DataTable GetGrpNoOfAccByCus(string _com, string _tp, string _cus, string _pc, string _grpCd);

        //darshana on 08-06-2013
        [OperationContract]
        DataTable GetGrpCusCashVal(string _com, string _tp, string _cus, string _pc, string _grpCd);

        //darshana on 08-06-2013
        [OperationContract]
        DataTable GetGrpCusNoofItms(string _com, string _tp, string _cus, string _pc, string _grpCd);

        //darshana on 10-06-2013
        [OperationContract]
        DataTable checkAppStatus(string _com, string _loc, string _tp, string _mod);

        //written by shani on 10-06-2013
        [OperationContract]
        DataTable Get_Processed_ecd_vou_defn_PClist(DateTime fromDt, DateTime toDt);


        //written by sachith on 10-06-2013
        [OperationContract]
        LoyaltyMemeber GetLoyaltyMemberByCard(string _cardNo);

        //written by sachith on 10-06-2013
        [OperationContract]
        int SaveLoyaltyRenewal(LoyaltyMemeber _loyal, MasterAutoNumber _recieptAuto, RecieptHeader _reciept, List<RecieptItem> _recieptItem, out string _recieptNo);
        //written by shani on 10-06-2013
        [OperationContract]
        DataTable GetRecievedDocFor_VehReg(string userID);

        //written by shani on 10-06-2013
        [OperationContract]
        DataTable Get_DocDet_For_VehReg(string receiptNo);

        //written by shani on 11-06-2013
        [OperationContract]
        Int32 Update_Veh_Doc_receive(Int32 seq, Dictionary<int, string> line_list, DateTime recDate, string remark, string recBy);
        //written by sachith on 11-06-2013
        [OperationContract]
        DataTable GetVehicalDocumentProcess(string _com, string _loc, string _invoice, string _engine, string _chassis, string _reciept, string _vehNo);

        //written by sachith on 11-06-2013
        [OperationContract]
        DataTable GetVehicalDocummentDetail(int seq);

        //written by darshana on 11-06-2013
        [OperationContract]
        Int32 IssueGvItems(GiftVoucherPages _gv, List<GiftVoucherItems> _gvItems);

        //written by sachith on 12-06-2013
        [OperationContract]
        DataTable GetVehicalRMVNotSendDetails(string _com, string _loc, string _invoice, string _engine, string _chassis, string _reciept, string _vehNo);

        //written by shani on 13-06-2013
        [OperationContract]
        Int32 Update_veh_DocPay(Int32 seq, DateTime payDate, string payRemark, string payBy);
        //written by Dilshan on 05/12/2018
        [OperationContract]
        Int32 Update_veh_AccRec(Int32 seq, DateTime recDate, string recBy);

        //written by shani on 13-06-2013
        [OperationContract]
        Int32 Update_veh_Collect_Cheque(Int32 seq, DateTime collDate, string collRemark, string collBy, string chequeNo, Int32 isCollect);

        //written by shani on 13-06-2013
        [OperationContract]
        DataTable Get_IssuedDocFor_VehReg(string userID, string receiptNo, string invoiceNo, string engineNo, string chassisNo);

        //written by sachith on 13-06-2013
        [OperationContract]
        int Update_Veh_RMVSend(string _inv, string _rec, string _eng, string _cha, DateTime _dt, string _rmk);

        //written by sachith on 13-06-2013
        [OperationContract]
        int Update_CR_Recieve(string _inv, string _rec, string _eng, string _cha, DateTime _dt, string _rmk, string _no);

        //writtn by darshana on 15-06-2013
        [OperationContract]
        Int16 SaveNewSchemeClone(DataTable _schSchemeClonedt);

        //written by darshana on 17-06-2013
        [OperationContract]
        List<PriceDetailRef> GetPriceForQuo(string _company, string _profitCenter, string _invType, string _priceBook, string _priceLevel, string _customer, string _item, decimal _qty, DateTime _currentDate, DateTime _validDate);

        //Written by Prabhath on 18/06/2013
        [OperationContract]
        DataTable GetPriceLevelMessage(string _company, string _book, string _level);

        //Written by Chamal on 19/06/2013
        [OperationContract]
        HPAccountSummaryValues GetHPAccSummValues(HpAccount _hpAcc, DateTime _receiptDate, string _com);

        //writtn by darshana on 20-06-2013
        [OperationContract]
        Int16 SaveNewSchemeCloneRevamp(string _pc, DateTime _dt, List<string> _pcList, string _user, string _cir);

        //Written by Prabhath on 20/06/2013
        [OperationContract]
        CashGeneralEntiryDiscountDef GetGeneralDiscountDefinition(string _company, string _profitcenter, DateTime _date, string _book, string _level, string _customer, string _item, bool _isAllowSerial, bool _isAllowPromotion);

        //Written by sachith on 21/06/2013
        [OperationContract]
        LoyaltyMemeber ValidateLoyaltyMember(string _card, string _cus, string _loty);

        //Written by Prabhath on 21/06/2013
        [OperationContract]
        DataTable GetUpdatableInvoiceforDiscounted(string _company);

        //Written by Prabhath on 21/06/2013
        [OperationContract]
        int UpdateDiscountRef(string _invoice, int _line, int _disseq, int _disline, string _distp);

        //Written by darshana on 24-06-2013
        [OperationContract]
        string GetNxtAccNo(MasterAutoNumber _AccAutoNo);

        //Written by Prabhath on 27/06/2013
        [OperationContract]
        DataTable GetCustomerAllowInvoiceType(string _company, string _customer);

        //written by darshana on 26-06-2013
        [OperationContract]
        List<PriceDetailRef> GetCombinePriceForHp(string _company, string _profitCenter, string _invType, List<PriceDefinitionRef> _paramPBForPC, string _customer, string _item, decimal _qty, DateTime _currentDate);

        //written by darshana on 26-06-2013
        [OperationContract]
        List<PriceDetailRef> GetPriceForHp(string _company, string _profitCenter, string _invType, List<PriceDefinitionRef> _priceList, string _customer, string _item, decimal _qty, DateTime _currentDate);

        //written by Shani on 01-07-2013
        [OperationContract]
        Int32 Save_AC_ServiceChargesDefinitions(MasterItem mstItm, MasterItemTax itmTax, PriceDetailRef pbDet);

        //written by Shani on 01-07-2013
        [OperationContract]
        Int32 Save_mst_itm_sev(MasterItemService _itmSev, List<string> ItemCodeList);
        //written by sachith on 04-07-2013
        [OperationContract]
        int SaveEliteCommissionDefinition(EliteCommissionDefinition _definition, List<EliteCommissionDetail> _detail, List<EliteCommissionPrty> _prty, List<EliteCommissionAdditional> _additional, List<EliteCommissionIgnore> _ignore, List<CashCommissionDetailRef> _item, string _itemType, MasterAutoNumber _auto, List<EliteCommissionSalesTypes> _salesType, out string err, bool _type);
        //written by sachith on 04-07-2013
        [OperationContract]
        List<EliteCommissionDefinition> GetEliteCommissionDefinition(string _circular);
        //written by sachith on 04-07-2013
        [OperationContract]
        List<EliteCommissionDetail> GetEliteCommissionDetailsByCircular(string circular);
        //written by sachith on 04-07-2013
        [OperationContract]
        List<EliteCommissionPrty> GetEliteCommissionLocation(string circular);
        //written by sachith on 04-07-2013
        [OperationContract]
        List<EliteCommissionAdditional> GetEliteCommissionAdditional(string circular);
        //written by sachith on 04-07-2013
        [OperationContract]
        List<EliteCommissionIgnore> GetEliteCommissionIgnore(string circular);
        //written by sachith on 04-07-2013
        [OperationContract]
        List<EliteCommissionItem> GetEliteCommissionItem(string circular);

        //written by Shani on 04-07-2013
        [OperationContract]
        DataTable Get_gurantors(string accountNo);

        //written by Shani on 04-07-2013
        [OperationContract]
        DataTable GetHpCustomer_Details(string accNo);
        //written by sachith on 04-07-2013
        [OperationContract]
        int CancelEliteCommission(string _circular);

        //written by Shani on 04-07-2013
        [OperationContract]
        Int32 Add_Remove_Guranter_Of_Account(string accountNo, List<string> RemoveGuranterCodeList, List<HpCustomer> newGuranterList);

        //Written by Prabhath on 04/07/2013
        [OperationContract]
        DataTable GetProfitCenterAllocatedExecutive(string _company, string _profitcenter);
        //Written by sachith on 06/07/2013
        [OperationContract]
        DataTable GetEliteCommissionEliteCommission(string _pc, DateTime _from, DateTime _to, decimal _discount, string _com, int _type, string _salesType, string _item);

        //Written by sachith on 08/07/2013
        [OperationContract]
        int EliteCommissionProcess(string _circular, List<string> _pcList, DateTime _from, DateTime _to, string _com, int year, int month, string creby, DateTime credt, out List<EliteCommission> _errlist, bool isErrorProcess, out string err);
        //Written by Prabhath on 04/07/2013
        [OperationContract]
        List<CashPromotionDiscountDetail> GetPromotionalDiscount(DateTime _date, int _time, string _day, string _book, string _level, string _item, string _company, string _profitcenter, int _isPromotion, int _isSuperUser);

        //Written by Prabhath on 11/07/2013
        [OperationContract]
        List<HpAccount> GetHPAccount(string _company, string _profitcenter, int _seqno, string _status);

        //written by darshana on 11-07-2013
        //Changed by Tharaka on 01-08-2014
        [OperationContract]
        Int32 AmendPromotion(string _promoCd, string _usr, DateTime _newDt, string _session, List<PriceDetailRef> _priceList, List<PriceCombinedItemRef> _priceComList, MasterAutoNumber _auto, out string _err, string _pb, string _plevel, string _circular, List<string> lstPromoI);

        //written by sachith on 11-07-2013
        [OperationContract]
        RecieptHeader GetAcJobReciept(string _jobno, string _com, string _pc);
        //written by sachith on 12-07-2013
        [OperationContract]
        InventoryHeader GetWarrDoDate(string _invoice, string _item, string _serial);
        //writtn by darshana on 17-07-2013
        [OperationContract]
        List<RecieptHeader> GetAccRecDet(string _company, string _account);

        //Written by Prabhath on 17/07/2013
        [OperationContract]
        bool IsForwardSaleExceed(string _company, string _profitcenter);

        //written by sachith on 17-07-2013
        [OperationContract]
        List<CashPromotionDiscountHeader> GetPromotionalHeader(string _circular);

        // Nadeeka 11-09-2015
        [OperationContract]
        List<sar_pb_def> GetPriceDefHeader(string _circular);
        // Nadeeka 11-09-2015
        [OperationContract]
        List<sar_pb_def_det> GetPriceDefDet(int seq);

        //written by sachith on 17-07-2013
        [OperationContract]
        List<CashPromotionDiscountDetail> GetPromotinalDiscountDetail(int seq);
        //written by darshana on 19-07-2013
        [OperationContract]
        DataTable GetRegInvDet(string _company, string _pc, string _inv);
        //writtn by darshana on 19-07-2013
        [OperationContract]
        DataTable GetRegInvItmDet(string _company, string _pc, string _inv, Int32 _invLine, string _itm);
        //kapila
        [OperationContract]
        DataTable GetRegDCNItmDet(string _company, string _pc, string _qno, string _itm);

        //written by darshana on 19-07-2013
        [OperationContract]
        VehicalRegistrationDefnition GetVehRegAmtDirectNew(string _com, string _pc, string _type, string _itm, DateTime _date, string _sch, decimal _qty, decimal _val, string _pb, string _plvl, string _ser);
        //written by sachith on 18-07-2013
        [OperationContract]
        List<CashPromotionDiscountItem> GetPromotinalDiscountItem(int seq);
        //written by sachith on 18-07-2013
        [OperationContract]
        List<CashPromotionDiscountLocation> GetPromotinalDiscountLoc(int seq);

        //written by sachith on 19-07-2013
        [OperationContract]
        int UpdatePromotionalDiscountDefinition(DataTable _itmList, List<string> _locList, int _seq, string _modBy, DateTime _date, string _com);

        //written by sachith on 19-07-2013
        [OperationContract]
        DataTable GetPcFromHirarchey(string _val, string _type);


        //written by sachith on 20-07-2013
        [OperationContract]
        List<EliteCommissionSalesTypes> GetEliteCommissionSalesType(string circular);

        //Written by Prabhath on 26/07/2013
        [OperationContract]
        void GetPromotion(string _company, string _profitcenter, string _item, DateTime _date, string _customer, out List<PriceDetailRef> _priceDetailRefPromo, out List<PriceSerialRef> _priceSerialRefPromo, out List<PriceSerialRef> _priceSerialRefNormal);

        //written by Chamal on 26-07-2013
        [OperationContract]
        DataTable GetSCMInvc(string _invcNo);

        //written by Chamal on 26-07-2013
        [OperationContract]
        DataTable GetCustomerDetails(string _com, string _custCode);

        //written by Chamal on 26-07-2013
        [OperationContract]
        Int32 UploadSCMInvoice(string _scmInvoiceNo, string _user, out string _msg);
        //written by darshana on 27-07-2013
        [OperationContract]
        List<ReceiptItemDetails> GetAdvanReceiptItems(string _recNo);
        //written by darshana on 30-07-2013
        [OperationContract]
        DataTable GetInvoiceWithSerialCusMonitor(string _com, string _invoice);
        //writtn by darshan on 31-07-2013
        [OperationContract]
        DataTable GetCusMonitorByDocument(string _company, string _profitcenter, string _doc);
        //writtn by sachith on 06-08-2013
        [OperationContract]
        DataTable GetReprintSevericeJob(string _type, string _no);

        //writtn by Shani on 07-08-2013
        [OperationContract]
        DataTable GetItemStatusWiseWarrantyPeriods(string itemCd, string status);

        //writtn by sachith on 07-08-2013
        [OperationContract]
        int ProcessMonthEnd(DateTime _asAtDate, string _com, string _pc, string _user, string _scheme, int seq);
        //writtn by sachith on 07-08-2013
        [OperationContract]
        int ProcessMontEnd(List<MonthEndHeadder> _hdr, out string _err);
        //writtn by sachith on 07-08-2013
        [OperationContract]
        MonthEndHeadder GetMonthEndHeader(string _com, string _pc, DateTime _monDt);

        //kapila
        [OperationContract]
        DataTable GetItemSubCate2(string _cat1, string _cd);
        [OperationContract]
        DataTable GetItemSubCate3(string _cat1, string _cat2, string _cd);
        [OperationContract]
        Boolean IsvalidPC(string _com, string _pc);
        //sachith
        [OperationContract]
        int ProcessPromotionCancel(List<string> _promoCodes, out string _err, out List<string> _errList, string user, string session);
        // Nadeeka
        [OperationContract]
        int ProcessPromotionCancelSubPb(List<string> _promoCodes, out string _err, out List<string> _errList, string user, string session);

        //darshana 22-08-2013
        [OperationContract]
        DataTable CheckValidAppPage(string _com, string _pc, string _appTp, string _stus, string _preFix, string _page, Int32 _used);
        //Darshana 23-08-2013
        [OperationContract]
        RequestApprovalHeader GetRequestApprovalHdr(string _com, string _pc, string _appTp, string _funcCD);

        //sachith 28-08-2013
        [OperationContract]
        List<MasterBusinessEntity> GetCustomerByKeys(string _com, string _nic, string _mob, string _br, string _pp, string _dl, int _type);

        //Written by Prabhath on 28/05/2013 to handle fucking data errors.
        [OperationContract]
        List<MasterBusinessEntity> GetCustomerDetailList(string _company, string _businessCompanyCode, string _nic, string _mobile, string _businessType);

        //Written by Chamal on 10/04/2014
        [OperationContract]
        List<MasterBusinessEntity> GetActiveCustomerDetailList(string _company, string _businessCompanyCode, string _nic, string _mobile, string _businessType);

        //Written by Prabhath on 30/05/2013
        [OperationContract]
        DataTable CheckTheDocument(string _company, string _document);

        //Written by Prabhath on 02/09/2013
        [OperationContract]
        DataTable GetCreditCustomerRequest(string _company, string _location, int _status);

        //Written by Prabhath on 01/09/2013
        [OperationContract]
        Int32 SaveCreditCustomerRequest(MasterBusinessEntity _businessEntity, string _location, int _status, out string customerCD, RequestApprovalHeader _reqhdr);

        //kapila 4/9/2013
        [OperationContract]
        DataTable PrintExtendedWarranty(string _com, DateTime _from, DateTime _to, string _user, string _cat1, string _cat2, string _cat3, string _brand, string _model, string _item, Int32 _stus);

        //kapila 15/6/2015
        [OperationContract]
        DataTable GetAllPriceSerialData(string _book, string _level, string _item, DateTime _date, string _customer, string _serial);

        //Written by Prabhath on 04/09/2013
        [OperationContract]
        Int32 UpdateCreditCustomerRequest(string _company, string _reqno, string _customer);

        //Written by sachith on 04/09/2013
        [OperationContract]
        void GetGeneralPromotionDiscountCreditNote(string _company, string _profitcenter, string _invoicetype, Int32 _time, string _day, DateTime _date, List<InvoiceItem> _InInvDet, out List<InvoiceItem> _outInvItem, out bool _isDifferent, out decimal _tobepay, InvoiceHeader _invoiceheader, List<InvoiceItem> _promotionList, List<RecieptItem> _InReceiptDet, out string _error);

        //Written by sachith on 04/09/2013
        [OperationContract]
        List<InvoiceItem> GetInvoiceItems(string _invoice);

        //written by darshana on 10-09-2013
        [OperationContract]
        List<LoyaltyMemeber> GetCurrentLoyalByCus(string _customer, string _type);
        //darshana on 11-09-2013
        [OperationContract]
        DataTable GetCustomerLoyalEarnHis(string _company, string _customer, string _card);
        //darshana on 11-09-2013
        [OperationContract]
        DataTable GetCustomerLoyalRedeemHis(string _company, string _customer, string _ref, string _payTp);

        //Written by Prabhath on 11/09/2013
        [OperationContract]
        DataTable GetInvoiceSerialByInvoice(string _company, string _invoice);
        //Written by sachith on 07/09/2013
        [OperationContract]
        PriceDetailRestriction GetPromotionRestriction(string _com, string _promo);

        //writtn by darshana on 14-09-2013
        [OperationContract]
        List<MasterBusinessEntity> GetBusinessCompanyDetailList(string _company, string _businessCompanyCode, string _nic, string _mobile, string _businessType);
        //written by darshana on 16-09-2013
        [OperationContract]
        Int32 HPInvoiceCancelation(InvoiceHeader _header, out string _message, List<InventoryHeader> InventoryList);
        //darshana on 16-09-2013
        [OperationContract]
        DataTable GetHpTxnDetailsForCancel(string _com, string _pc, string _accNo, DateTime _date);
        //darshana on 16-09-2013
        [OperationContract]
        DataTable GetHpLogDetailsForCancel(string _com, string _pc, string _accNo);
        //Written by Prabhath on 14/09/2013
        [OperationContract]
        DataTable GetDeliveryOrader(string _invoice);

        //Get to client by Prabhath on 14/09/2013
        [OperationContract]
        Int32 UpdateRequestCloseStatus(string _com, string _pc, string _type, string _refNo, string _stus, string _user);

        // Nadeeka 15-06-2015
        [OperationContract]
        Int32 UpdateRequestCloseStatusSer(string _com, string _pc, string _type, string _refNo, string _stus, string _user);

        //kapila
        [OperationContract]
        DataTable PrintExcessShort(string _com, string _user, string _doc);


        //kapila
        [OperationContract]
        DataTable PrintExcessShortBal(string _com, string _pc, DateTime _date);
        //sachith 2013/09/23
        [OperationContract]
        int SaveSalesTarget(SalesTargetHeadder _hdr, List<SalesTargetDetail> _details, List<String> _pcList, int noMonths, out string _error);

        //Written by Prabhath on 26/09/2013
        [OperationContract]
        List<PriceDetailRef> GetPriceEnquiryDetailNew(string _profitcenter, int _startFrom, int _endFrom, string _user, string _company, string _priceBook, string _priceLevel, string _customer, string _item, string _category1,
string _category2, string _category3, string _status, string _pricetype, string _circular, DateTime _fromDate, DateTime _toDate, bool _isHistory, bool _isAsAtHistory,
bool _isAllStatus, bool _isSuperUser, string _promocd, string _Taxstructure);

        //Written by Prabhath on 26/09/2013
        [OperationContract]
        DataTable GetProfitCenterDetail(string _company, int _type, string _book, string _level, string _promotion);

        //Written by Prabhath on 26/09/2013
        [OperationContract]
        DataTable GetPriceStatus(string _item, decimal _price, string _company, string _book, string _level);

        //Written by Prabhath on 28/09/2013
        [OperationContract]
        DataTable GetDPExchange(string _company, string _pc, string _loc, string _type, string _doc);

        //kapila
        [OperationContract]
        DataTable GetDPExchange_new(string _company, string _pc, string _loc, string _type, string _doc);

        //Written by sachith on 01/10/2013
        [OperationContract]
        void GetGeneralPromotionDiscountAdvanCredit(string _company, string _profitcenter, string _invoicetype, Int32 _time, string _day, DateTime _date, List<InvoiceItem> _InInvDet, List<RecieptItem> _InReceiptDet, out List<InvoiceItem> _outInvItem, out bool _isDifferent, out decimal _tobepay, InvoiceHeader _invoiceheader);
        //Written by sachith on 02/10/2013
        [OperationContract]
        int ProcessReversalCancel(InvoiceHeader _hdr, string _modBy, DateTime _modDt, out string _error);

        //Written by sachith on 03/10/2013
        [OperationContract]
        MasterOutsideParty GetOutSidePartyDetailsById(string _code);

        //Written by Prabhath on 05/10/2013
        [OperationContract]
        int UpdateFromExchange(List<ReptPickSerials> _reversSerial, List<InvoiceItem> _invoiceItem);

        //Written by Prabhath on 05/10/2013
        [OperationContract]
        List<RequestApprovalHeader> getExchangeRequest(string _com, string _loc, string _type, string _status, Int32 _level, string _invLoc, Int32 _isservice);

        // Nadeeka 25-06-2015
        [OperationContract]
        List<RequestApprovalHeader> getExchangeRequestJob(string _com, string _loc, string _type, string _status, string _job, Int32 _isser);



        //Written by sachith on 14/10/2013
        [OperationContract]
        int SavePhysicalCashVerification(List<AuditAccountableCash> _accountableCash, AuditCashVeriDenomination _denomination, List<AuditCashVeriDetail> _detail, List<AuditCashVeriDocument> _documnts, AuditCashVeriMain _main, string _jobNo, out string _error);
        //Written by sachith on 14/10/2013
        [OperationContract]
        AuditCashVeriMain GetJobFromDate(DateTime _from, DateTime _to, string _com, string _pc);
        //Written by sachith on 14/10/2013
        [OperationContract]
        List<RecieptHeader> GetRecieptByTypeByDate(string _pc, string _com, DateTime _from, DateTime _to, string _type);
        //Written by sachith on 15/10/2013
        [OperationContract]
        int ProcessPhysicalCashVerification(DateTime _from, DateTime _to, string _com, string _pc, MasterAutoNumber _auto, out List<AuditCashVeriDetail> _details, string _user, out string _jobNo, out string _error);
        //Written by sachith on 16/10/2013
        [OperationContract]
        List<AuditCashVeriDetail> GetAuditDetailsByJob(string _job, int _seq);
        //Written by sachith on 16/10/2013
        [OperationContract]
        int ReProcessPhysicalCashVerification(DateTime _from, DateTime _to, string _com, string _pc, out List<AuditCashVeriDetail> _details, string _user, AuditCashVeriMain _auditMain, out string _error);
        //by darshana on 21-10-2013
        [OperationContract]
        DataTable GetTempUserPc(string _com, string _user);
        //darshana 21/10/2013
        [OperationContract]
        DataTable Process_Hp_Closing_Bal_New(string _com, string _user, string _schTp, string _schCD, DateTime _asAT, string _pc, string _cat1, string _cat2, string _cat3, string _item, string _brand, string _model);
        //darshana 22/10/2013
        [OperationContract]
        Int32 SaveManualHpRecReq(RequestApprovalHeader _AppHdr, List<RequestApprovalSerials> _AppSer, MasterAutoNumber _AppReqAuto, RequestApprovalHeaderLog _AppHdrLog, List<RequestApprovalSerialsLog> _AppSerLog, out string _docNo);
        //sachith 29/10/2013
        [OperationContract]
        List<AuditCashVeriDetail> GetCashVerificationDetails(string _com, string _pc, string _jobNo);
        //sachith 29/10/2013
        [OperationContract]
        AuditCashVeriMain GetCashVerificationMain(string _com, string _pc, string _jobNo);
        //Written by Prabhath on 29/10/2013
        [OperationContract]
        Int32 SaveSaleRevReqAppDF(RequestApprovalHeader _AppHdr, List<RequestApprovalDetail> _AppDet, List<RequestApprovalSerials> _AppSer, MasterAutoNumber _AppReqAuto, RequestApprovalHeader _RegHdr, List<RequestApprovalDetail> _RegDet, List<RequestApprovalSerials> _RegSer, MasterAutoNumber _AppRegAuto, RequestApprovalHeaderLog _AppHdrLog, List<RequestApprovalDetailLog> _AppDetLog, List<RequestApprovalSerialsLog> _AppSerLog, RequestApprovalHeaderLog _RegHdrLog, List<RequestApprovalDetailLog> _RegDetLog, List<RequestApprovalSerialsLog> _RegSerLog, Boolean _isRegReq, RequestApprovalHeader _insHdr, List<RequestApprovalDetail> _insDet, List<RequestApprovalSerials> _insSer, MasterAutoNumber _AppinsAuto, RequestApprovalHeaderLog _insHdrLog, List<RequestApprovalDetailLog> _insDetLog, List<RequestApprovalSerialsLog> _insSerLog, Boolean _isinsReq, out string _docNo, out string _regReq, out string _insReq);
        //Written by darshana on 30-10-2013
        [OperationContract]
        DataTable GetHPCustomerDet(string _type, string _no);
        //Written by sachith on 31-10-2013
        [OperationContract]
        DataTable GetPromotionalDiscountSequences(string _company, string _profitcenter, string _invoicetype, Int32 _time, string _day, DateTime _date, List<InvoiceItem> _InInvDet, List<RecieptItem> _InReceiptDet, InvoiceHeader _invoiceheader, out bool isMulty, out int _seq);
        //Written by sachith on 31-10-2013
        [OperationContract]
        void GetGeneralPromotionProcess(int _disSeq, string _company, List<InvoiceItem> _InInvDet, out List<InvoiceItem> _outInvItem, out bool _isDifferent, out decimal _tobepay, InvoiceHeader _invoiceheader);
        //Written by darshana on 01-11-2013
        [OperationContract]
        DataTable GetPCWara(string _com, string _pc, string _itm, string _itmStatus, DateTime _frmDt);

        //Written by Prabhath on 02/11/2013
        [OperationContract]
        DataTable GetDPExchangeSerial(string _company, string _pc, string _loc, string _type, string _doc);

        [OperationContract]
        DataTable GetTempUserPcRptDB(string _com, string _user);
        [OperationContract]
        DataTable GetTempUserPcRptDB_AllCom(string _user);

        //written by darshana on 11/11/2013
        [OperationContract]
        DataTable GetServiceJobDet(string _comCode, string _job);
        // Nadeeka 17-04-2015
        [OperationContract]
        Int32 SaveInvoiceDuplicateWithTransactionQuo(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, List<InvoiceVoucher> _voucher, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, Dictionary<string,string> DOdet);

        //Written by sachith on 06/11/2013
        [OperationContract]
        Int32 SaveInvoiceDuplicateWithTransaction(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, List<InvoiceVoucher> _voucher, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo);

        //Written by sachith on 12/11/2013
        [OperationContract]
        Int32 SaveInvoiceDuplicateWithTransaction01(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, List<InvoiceVoucher> _voucher, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, ref bool _isInvoiced, List<Transport> _traList = null, List<string> _SoaList = null, bool _pdabase = false, CctTransLog _transLog = null, List<EventRegistry> _eventList = null, List<EventItems> _eventItems = null);// updated by akila 2018/02/26 add new objects _eventList, _eventItems

        //Written by darshana on 20-11-2013
        [OperationContract]
        Decimal Get_Diriya_CommissionVatRt(string accNo, DateTime ars_date);
        //written by darshana on 20-11-2013
        [OperationContract]
        List<QuotationSerial> GetQuoSerials(string _No);
        //sachith
        [OperationContract]
        Int32 SaveAuditCashVerificationAccountableCash(AuditAccountableCash _cash);
        //sachith
        [OperationContract]
        int UpdateAuditAccountableCash(string _job, string _type);
        //sachith
        [OperationContract]
        List<AuditAccountableCash> GetAccounableCash(int seq, string _job);
        //sachith
        [OperationContract]
        DataTable GetPromotionalDiscountSequences01(string _company, string _profitcenter, string _invoicetype, Int32 _time, string _day, DateTime _date, List<InvoiceItem> _InInvDet, List<RecieptItem> _InReceiptDet, InvoiceHeader _invoiceheader, out bool isMulty, out int _seq);
        //kapila
        [OperationContract]
        DataTable get_Branch_Name(string _BankCode, string _code);
        //sachith 25/11/2013
        [OperationContract]
        int SaveSpeicalReschedule(List<HpSheduleDetails> _insertList, List<HpSheduleDetails> _update, out string _error);

        //sachith 26/11/2013
        [OperationContract]
        List<PriceDetailRef> GetPrice_01(string _company, string _profitCenter, string _invType, string _priceBook, string _priceLevel, string _customer, string _item, decimal _qty, DateTime _currentDate);

        //Prabhath on 27/11/2013
        [OperationContract]
        bool GetLimit(string _company, string _partycd, string _partyval, string _code1, string _code2, string _code3, Int16 _valuetp, decimal _uvalue);

        //kapila
        [OperationContract]
        DataTable GetRevertItemDetails(DateTime _fromDate, DateTime _toDate, string _com, string _PCenter, string _User);

        //Nadeeka
        [OperationContract]
        DataTable GetRevertItemDetailsAudit(DateTime _fromDate, DateTime _toDate, string _com, string _PCenter, string _User, string _jobno);

        //kapila
        [OperationContract]
        DataTable get_Bank_Name(string _Bank);
        //sachith 2013/12/10
        [OperationContract]
        Int32 SaveInvoiceDuplicateWithTransactionRegistration(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, List<InvoiceVoucher> _voucher, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, RecieptHeader _regReciept, MasterAutoNumber _recAuto, List<VehicalRegistration> _regList, List<RecieptItem> _regRecList, ReptPickHeader _SerHeader, out string _registration, ref bool _isInvoiced, List<Transport> _traList = null, CctTransLog _transLog = null);
        //sachith 2013/12/11
        [OperationContract]
        List<HpTransaction> GetHpTransactionByRef(string _ref);

        //darshana on 17-12-2013
        [OperationContract]
        RecieptHeader Check_ManRef_Rec(string com, string pc, string recTp, string refNo);

        // Nadeeka 12-09-2015
        [OperationContract]
        RecieptHeader Check_ManRef_Rec_prefix(string com, string pc, string recTp, string refNo, string _prefix);

        //darshana on 17-12-2013
        [OperationContract]
        RecieptHeaderTBS Check_ManRef_RecTBS(string com, string pc, string recTp, string refNo);

        //sachith 2013/12/17
        [OperationContract]
        Int32 SaveCashConversionEntry_BackUp(InvoiceHeader _hpReversInvoiceHeader, List<InvoiceItem> _hpReversInvoiceItem, List<RecieptHeader> _hpReversReceiptHeader, InvoiceHeader _ccInvoiceHeader, List<InvoiceItem> _ccInvoiceItem, RecieptHeader _ccReceiptHeader, List<RecieptItem> _ccReceiptItem, MasterAutoNumber _reversInvoiceAuto, MasterAutoNumber _reversReceiptAuto, MasterAutoNumber _convertInvoiceAuto, MasterAutoNumber _convertReceiptAuto, HpInsurance _reversInsurance, InventoryHeader _ccInv, MasterAutoNumber _invAuto, out string _convertInvoice,
                    RequestApprovalHeader _regReqHdr, List<RequestApprovalDetail> _regReqDet, List<RequestApprovalSerials> _regReqSer, RequestApprovalHeaderLog _regReqLogHdr, List<RequestApprovalDetailLog> _regReqLogDet, List<RequestApprovalSerialsLog> _regReqLogSer, MasterAutoNumber _regReqAuto, RequestApprovalHeader _insReqHdr, List<RequestApprovalDetail> _insReqDet, List<RequestApprovalSerials> _insReqSer, RequestApprovalHeaderLog _insReqLogHdr, List<RequestApprovalDetailLog> _insReqLogDet, List<RequestApprovalSerialsLog> _insReqLogSer, MasterAutoNumber _insReqAuto, List<RecieptHeader> _regReciept, List<RecieptItem> _regRecieptItem, MasterAutoNumber _regRecieptAuto, List<RecieptHeader> _insReciept, List<RecieptItem> _insRecieptItem, MasterAutoNumber _insRecieptAuto, int option, HpTransaction _transaction, out string _err);
        //darshana 2013/12/19
        [OperationContract]
        DataTable GetSerialPriceForHp(string _company, string _Location, List<PriceDefinitionRef> _priceList, string _item, DateTime _currentDate);


        //sachith 2013/12/21
        [OperationContract]
        List<RecieptItem> GetRecieptItemByRef(string _refNo);

        // Nadeeka 12-05-2015
        [OperationContract]
        DataTable Check_SRN_Stock_Avilability(string _refNo);

        //darshana 24-12-2013
        [OperationContract]
        List<HpSchemeDefinition> GetSerialSchemeNew(string _type, string _value, DateTime _date, string _item, string _serial, string _scheme, string _pbBook = null, string _pbLvl = null);
        //darshana 26-12-2013
        [OperationContract]
        HpAccount GetLatestHPAccount(string _company, string _profitcenter);
        //darshana 26-12-2013
        [OperationContract]
        List<InvoiceItem> GetRegAllowInvItem(string _company, string _profitcenter, string _invoice);
        //darshana 26-12-2013
        [OperationContract]
        List<VehicalRegistration> CheckVehRegTxn(string _company, string _profitcenter, string _invoice, string _itm);

        //2013/12/26
        [OperationContract]
        Int32 SaveHPExchangeWithDo(DateTime _date, string _accountno, string _company, string _location, string _profitcenter, string _createdBy, string _inSubType, string _outSubType, List<ReptPickSerials> _list, List<ReptPickSerials> _outList, List<InvoiceItem> _outPureInvoiceItem, List<RecieptHeader> receiptHeaderList, List<RecieptItem> receipItemList, MasterAutoNumber receipAuto, out string _creditnotelist, out string _inventorydoclist, HPAccountLog _accLog, HpAccount _newAccount, List<HpSheduleDetails> _currentSchedule, List<HpSheduleDetails> _newSchedule, HpInsurance _insurance, MasterAutoNumber _insuranceAuto, RequestApprovalHeader _request, out string _diriya, out string _invNo, out string _error, string _sessionId, InventoryHeader _invHdr, MasterAutoNumber _invAuto);

        //kapila
        [OperationContract]
        DataTable getLocDesc(string _Com, string _Type, string _Code);
        //sachith
        //2013/12/30
        [OperationContract]
        int EliteCommissionProcessWithExchangeRate(string _circular, List<string> _pcList, DateTime _from, DateTime _to, string _com, int year, int month, string creby, DateTime credt, out List<EliteCommission> _errlist, bool isErrorProcess, out string err, decimal __exgRate);

        //darshana 06-01-2013
        [OperationContract]
        DataTable CheckAdvanForIntr(string _company, string _document);
        //sachith 2014/01/17
        [OperationContract]
        DataTable GetTrimmingAccounts(string _com, List<string> _pcList, DateTime _fromDate, decimal _margin, out string _error);
        //sachith 2014/01/17
        [OperationContract]
        int SaveAccountTrim(out string _error, string _com, List<string> _pcList, DateTime _fromDate, decimal _margin, DateTime _date, string _userId);
        //sachith 2014/01/20
        [OperationContract]
        int SaveHPAcoountChecklist(HPAccountChecklistHdr _hdr, List<HpAccountChecklistDet> _detList, MasterAutoNumber _auto, out string _error);
        //darshana on 2014-01-19
        [OperationContract]
        MasterItemTaxClaim GetTaxClaimDet(string _company, string _item, string _claimCd);
        //sachith 2014/01/21
        [OperationContract]
        int ConfirmHPAccountChecklist(List<HpAccountChecklistDet> _det, string _pc, DateTime _date, out string _error, string _com);
        //sachith 2014/01/24
        [OperationContract]
        HPAccountChecklistHdr GetPODNo(string _podNo);
        //sachith 2014/01/28
        [OperationContract]
        Int32 UpdateRequestAppStatus(string _appStus, string _req, string _itm);
        //darshana 2014-01-30
        [OperationContract]
        List<ExtendWaraParam> GetExWaraParam(string _type, string _value, DateTime _date, decimal _invVal, Int32 _invPd, string _searchTp, string _cusCd, string _itm, string _ser, string _brd, string _mCat, string _sCat, string _promo);

        //sachith 2014/02/05
        [OperationContract]
        List<PriceDefinitionUserRestriction> GetUserRestriction(string _userId, string _com, DateTime _date, string _pb, string _plevel, int _type);

        //Written by Prabhath on 15/02/2014
        [OperationContract]
        DataTable GetNBookNLevel(string _pc, string _com, string _item, decimal _qty, string _customer, DateTime _date);

        //Written by Prabhath on 17/02/2014
        [OperationContract]
        DataTable GetAccountCustomer(string _com, string _pc, string _acc);

        //Written by Prabhath on 17/02/2014
        [OperationContract]
        int ProcessOnlinePayment(string _com, List<RecieptHeader> _hdr, List<RecieptItem> _item, out List<string> Receipts, string _msg);
        //sachith 2014/02/19
        [OperationContract]
        DataTable GetPromotionalDiscountSequencesBackUp(string _company, string _profitcenter, string _invoicetype, Int32 _time, string _day, DateTime _date, List<InvoiceItem> _InInvDet, List<RecieptItem> _InReceiptDet, InvoiceHeader _invoiceheader, out bool isMulty, out int _seq);
        //darshana 2014-02-20
        [OperationContract]
        DataTable GetInvItemQty(string _invNo, string _itm);
        //darshana 2014-02-20
        [OperationContract]
        DataTable GetReqItemQty(string _tp, string _subTp, string _invNo, string _itm);
        //sachith 2014-02-21
        [OperationContract]
        DataTable GetAccountAcknoledge(string _com, string _pc, DateTime _fromDate, DateTime _toDate, string _scheme, decimal _margin, out string _error);
        //sachith 2014-02-21
        [OperationContract]
        int UpdateAcknoledgementPrintCount(List<string> _accList, out string _error);

        //Prabhath on 22/02/2014
        [OperationContract]
        DataTable CheckBlackListCustomer(string _customer);

        //sachith 2014-02-25
        [OperationContract]
        int GetAgePriceActivation(List<Tuple<string, decimal>> _itemList, string _oriPb, string _oriPlevel, string _clonePb, string _clonePlevel, string _user, string _com, string _circular, out string _error, out DataTable _priceList);

        //sachith 2014-02-26
        [OperationContract]
        CashGeneralEntiryDiscountDef GetGeneralDiscountDefinitionBySequence(int _seq);
        //sachith 2014-02-25
        [OperationContract]
        int SaveAgePriceActivation(List<Tuple<string, decimal>> _itemList, string _oriPb, string _oriPlevel, string _clonePb, string _clonePlevel, string _user, string _com, string _circular, out string _error, DateTime _from, DateTime _to);
        //darshana on 26-02-2014
        [OperationContract]
        Int32 UpdateAccLogStatus(string _com, string _account, string _status, DateTime _date, Int32 _isCls);
        //darshana on 26-02-2014
        [OperationContract]
        DataTable getSerialpriceDetailsForCirDT(string _itm, string _cir);
        //darshana on 26-02-2014
        [OperationContract]
        int UpdateHpAccActive(string _com, string _pc, string _acc, DateTime _date, out string _error);
        //darshana on 06-03-2014
        [OperationContract]
        DataTable GetPriceTypeByCir(string _cir);
        //darshana on 06-03-2014
        [OperationContract]
        DataTable GetPromobyCirAndTp(string _cir, Int32 _tp);
               

        //Written by Sanjeewa 2015-10-12
        [OperationContract]
        string GetSaleswithWarranty(DateTime _fdate, DateTime _tdate, string _Com, string _cat1, string _cat2, string _cat3, string _item, string _brand, string _model, string _InvoiceDate, string _userid, out string _error);

        //Written by Sanjeewa 2015-10-23
        [OperationContract]
        string GetSaleswithWarrantySCM(DateTime _fdate, DateTime _tdate, string _Com, string _cat1, string _cat2, string _cat3, string _item, string _brand, string _model, string _InvoiceDate, string _userid, out string _error);

        //Written by Sanjeewa on 18-11-2014
        [OperationContract]
        string GetNoofCompletedAgreements(DateTime _fromdate, DateTime _todate, string _Com, string _user, out string _error);

        //Written by Prbhath on 11/03/2014
        [OperationContract]
        DataTable GetInsuReportDet(string _com, string _item, int _term, DateTime _asatdate, string _inscom, string _pol, string _PB, string _level, string _party, string _partycode, string _user, string _PC, out string _error);

        //Written by Sanjeewa on 07/09/2015
        [OperationContract]
        DataTable GetInsuReportDetCirc(string _com, string _item, int _term, DateTime _asatdate, string _inscom, string _pol, string _PB, string _level, string _party, string _partycode, string _user, string _Circular, string _PC, out string _error);

        //Written by sachith on 11 3 2014
        [OperationContract]
        Int32 SaveClearInvoicePriceEdit(string _com, string _userId, string _invNo, string _sessrionId, DateTime _logDate, out string _error);
        //Written by Prbhath on 11/03/2014
        [OperationContract]
        DataTable GetInsuEnquiry(string _com, string _pc, string _item, int _term, DateTime _asatdate, string _user, string _tp, string _insucom, string _pol, string _PB, string _Level, string _party, string _partycode, out string _error);

        //Written by Prabhath on 17/03/2014
        [OperationContract]
        List<CashPromotionDiscountDetail> GetPromotionalDiscountCacnel(DateTime _date, int _time, string _day, string _book, string _level, string _item, string _company, string _profitcenter, int _isPromotion, int _isSuperUser);
        // Written by sachith on 17/03/2014
        [OperationContract]
        List<PayTypeRestrict> GetPaymodeRestriction(string _com, string _pc, DateTime _date);

        //Written by Prabhath on 17/03/2014
        [OperationContract]
        int DocumentInvoice(string _dodoc, string _docom, string _user, DateTime _dodate, string _doloc, string _dopc, string _invcom, out string error, out string _invoiceNo);
        //darshana on 21-03-2014
        [OperationContract]
        DataTable GetItemTp(string _tpCd);
        //darshana on 24-03-2014
        [OperationContract]
        DataTable GetDefInvPrefix(string _com, string _tp);

        //Written by sachith on 17/03/2014
        //[OperationContract]
        //List<PayTypeRestrict> GetPaymodeRestriction(string _com, string _pc, DateTime _date, int _type, string _code);

        //Written by Prabhath on 25/03/2014
        [OperationContract]
        DataTable GetPriceForItem(string _book, string _level, string _item, DateTime _date, string _promo, string _circular);

        //Written by Prabhath on 27/03/2014
        [OperationContract]
        DataTable GetInsuCriteria(string _com, string _search, string _item, string _brand, string _model, string _main, string _sub, string _serial, string _circular, string _promotion);

        [OperationContract]
        DataTable GetInsuCriteriaAdditional(string _com, string _search, string _item, string _brand, string _model, string _main, string _sub, string _serial, string _circular, string _promotion, Int32 _cat);

        //kapila
        [OperationContract]
        List<InvoiceItem> GetVehInsuAllowInv(string _company, string _profitcenter, DateTime _date);

        //kapila 31/3/2014
        [OperationContract]
        Int32 UpdateVouSettlement(string _company, string _profitcenter, string _book, string _page, string _prefix, string _item, string _user, string _ref, decimal _amount, DateTime _setDate);
        //Written by sachith on 31/03/2014
        [OperationContract]
        int SaveAppPromoPcDataTable(DataTable _appPromoPc, out string _error);
        //Written by sachith on 31/03/2014
        [OperationContract]
        Int16 SavePriceDetailsSaveAs(List<PriceDetailRef> _priceDet, List<PriceCombinedItemRef> _priceDetCom, MasterAutoNumber _priceAuto, List<PriceProfitCenterPromotion> _appPCList, List<PriceSerialRef> _serialPrice, PriceDetailRestriction _restriction, out string _err, string session, string user);
        //wtritten by darshana on 01-04-2014
        [OperationContract]
        MasterVehicalInsuranceDefinitionNew GetVehInsAmtNewMethod(string _com, string _ptyTp, string _ptyCd, string _insCom, string _insPol, string _salesTp, string _itm, Int32 _tearm, string _pBook, string _pLvl, DateTime _date, decimal _frmItmVal, decimal _toItmVal, string _prmoCd, string _ser);
        //By darshana on 01-04-2014
        [OperationContract]
        InvoiceItem GetInvDetByLine(string _invNo, string _itm, Int32 _line);
        //kapila
        [OperationContract]
        QoutationDetails GetQuotDetByItem(string _qno, string _itm);

        //kapila
        [OperationContract]
        DataTable GetPossiblePayTypes(string _com, string _party, string _cd, string txn_tp, DateTime today);

        //kapila 22/5/2014
        [OperationContract]
        List<InvoiceItem> GetVehRegAllowInv(string _company, string _profitcenter, DateTime _date);

        //Written by Darshana on 23-05-2014
        [OperationContract]
        bool IsAdvanAmtExceed(string _company, string _profitcenter, string _recNo, decimal _usedAmt);

        //Written by Prabhath on 02/04/2014
        [OperationContract]
        DataTable CheckAccountClose(string _prifix, string _receiptno);

        [OperationContract]
        DataTable GetPromoDetails(string _item);

        [OperationContract]
        DataTable GET_RECEIPTS(string _invNo);

        //By darshana on 25-04-2014
        [OperationContract]
        MasterBusinessEntity GetCustomerAllProfileByCom(string CustCD, string nic, string DL, string PPNo, string brNo, string com, string ent_type);
        //Written By Shalika on 19/05/2014
        [OperationContract]
        List<CashSalesRev_History> Load_reversalsItems(string _fuc_cd);
        [OperationContract]
        List<CashSalesRev_History> Load_Invoicedetails(string _fuc_cd);
        [OperationContract]
        List<CashSalesRev_History> Load_OutItemDetails(string _fuc_cd);
        [OperationContract]
        List<CashSalesRev_History> Load_DeptComments(string _fuc_cd);
        [OperationContract]
        List<CashSalesRev_History> Load_AdvanceRecepts(string _fuc_cd);
        [OperationContract]
        List<HireSalesReversal_Det> Load_ExchangeItems(string _fuc_cd);
        //[OperationContract]
        //List<InvoiceItem> GetVehRegAllowInv(string _company, string _profitcenter, DateTime _date);
        //darshana on 26-05-2014
        [OperationContract]
        List<HpSchemeDetails> GetSchForActivation(string _tp, string _cd, Int16 _act);
        //darshana on 26-05-2014
        [OperationContract]
        Int16 SchemeActivation(List<HpSchemeDetails> _actDet, out string _err);
        [OperationContract]
        DataTable Get_Itemstatus();
        [OperationContract]
        List<HireSalesReversal_Det> Get_Itemdetails(string Itemcode);
        [OperationContract]
        Int32 SaveOutItemdetailsHS(CashSales_Out_RevItems objOutItems, out string _err);
        [OperationContract]
        Int32 SaveINItemdetailsHS(CashSales_Out_RevItems objOutItems, out string _err);
        [OperationContract]
        DataTable GetAllInvDetailsHS(string _invNo);
        [OperationContract]
        DataTable _lstCustomerDetails(string _fuc_cd);
        [OperationContract]
        DataTable _lstRevertItemDetails(string _fuc_cd);
        [OperationContract]
        Int32 SaveApprovals(CashSales_Out_RevItems objOutItems, out string _err);
        [OperationContract]
        DataTable Load_RevertReleaseHistory(string _fuc_cd);
        [OperationContract]
        List<CashSalesRev_History> Load_OutItemDetailsHS(string _fuc_cd);
        [OperationContract]
        DataTable Load_ManagerIssueReversal_Details(string _invNo);
        [OperationContract]
        DataTable Load_ManagerChqRect_Details(string _invNo);
        [OperationContract]
        DataTable Load_ManagerIssueRevHistory(string _fuc_cd);
        [OperationContract]
        DataTable Load_CashConversionHistory(string _fuc_cd);
        [OperationContract]
        DataTable Load_ReceiptReversalHis(string _fuc_cd);
        [OperationContract]
        DataTable Load_ManualDoc_TrnsrHis(string _fuc_cd);
        [OperationContract]
        DataTable Load_AccountReScheduleHistory(string _fuc_cd);
        //End shalika
        //Darshana 16-06-2014
        [OperationContract]
        List<HPAddSchemePara> GetAddParaDetails(string _type, string _schCd);
        //Darshana 17-06-2014
        [OperationContract]
        DataTable GetAllSchemeCircularsToDataTable();
        //Darshana 17-06-2014
        [OperationContract]
        DataTable GetCustomerSalesDet(string _com, string _invTp, DateTime _frmDt, DateTime _toDt, string _cus, string _invNo);
        //Darshana 27-06-2014
        [OperationContract]
        DataTable CheckPreviousUseInvoice(string _com, string _invNo);


        //Written by SHANUKA PERERA on 18/06/2014
        [OperationContract]
        DataTable GetVehicalRegisDetails(string _invoice_no, string _sart_cd);


        //Shanuka Perera on 19/06/2014
        [OperationContract]
        Int32 SaveVehReDetails(CashSales_Out_RevItems objrev_det, out string _err);
        //shalika on 19/06/2014
        [OperationContract]
        Int32 SaveApprovalsManagerIsuRev(CashSales_Out_RevItems objrev_det, out string _err);

        //written by Chamal on 19-06-2014
        [OperationContract]
        List<PromoVoucherDefinition> GetProVouhByCir(string _cir);

        //written by Chamal on 20-06-2014
        [OperationContract]
        Int16 SaveTempPromoVoucher(DataTable _schSchemeCommDefdt);

        //written by Chamal on 20-06-2014
        [OperationContract]
        Int16 SavePromoVoucher(string _com, string _vouCode);

        //Written by SHANUKA PERERA on 21/06/2014
        [OperationContract]
        DataTable FillVehRegisHisDetails(string _invoice_no, string _sart_cd);
        //written by shalika on 25-06-2014
        [OperationContract]
        DataTable Load_CashConersion_Details(string _invNo);
        //shalika on 25/06/2014
        [OperationContract]
        Int32 SaveApprovalsCashConversion(CashSales_Out_RevItems objrev_det, out string _err);
        //shalika on 27/06/2014
        [OperationContract]
        DataTable Load_AccountReschedule_Details(string _invNo);
        [OperationContract]
        DataTable Get_RequstingSchm(string _Grad_aNal2);

        //Chamal 28-Jun-2014
        [OperationContract]
        bool CheckPromoVoucherNo(string _company, string _cust, string _nic, string _mobi, DateTime _date, int _vouNo, out string _err, string gvcdode = null);

        //Chamal 28-Jun-2014
        [OperationContract]
        CashGeneralEntiryDiscountDef GetPromoVoucherNoDefinition(string _company, string _pc, string _customer, DateTime _date, string _book, string _level, string _item, string _voucherNo, string gvp_gv_cd=null);

        //Chamal 30-Jun-2014
        [OperationContract]
        List<PromoVoucherDefinition> GetPromotionalVouchersDefinition(string _company, string _salesType, string _profitCenter, DateTime _date, string _pb, string _pbLevel, string _brand, string _cate1, string _cate2, string _item, bool _isopenCnn);

        //Chamal 28-Jun-2014
        [OperationContract]
        CashGeneralEntiryDiscountDef CheckCustHaveDiscountPromoVoucher(string _company, string _pc, string _customer, DateTime _date, string _book, string _level, string _item, string _voucherNo, string _nic, string _mobi);

        //Chamal 7-Jul-2014
        [OperationContract]
        bool CheckPromoVoucherInvoiceUsed(string _company, string _pc, string _invoiceNo);
        [OperationContract]
        DataTable Load_CashRefund_Details(string _invNo);
        //shalika on 07/07/2014
        [OperationContract]
        Int32 SaveApprovalsCashRefund(CashSales_Out_RevItems objrev_det, out string _err);
        [OperationContract]
        List<CashSalesRev_History> Load_CashRefundHis(string _fuc_cd);
        //shalika on 08/07/2014
        [OperationContract]
        DataTable Load_Manual_Doc_Tranfr_Details(string _invNo);
        //shalika on 08/07/2014
        [OperationContract]
        Int32 SaveManualDocTrnsr(CashSales_Out_RevItems objrev_det, out string _err);
        //shalika on 08/07/2014
        [OperationContract]
        DataTable Load_Rev_Acc_Holder(string _invNo);

        //SHANUKA on 08/07/2014
        [OperationContract]
        DataTable Get_Special_EarlyDiscount_Details(string _invNo);
        //SHANUKA on 09/07/2014
        [OperationContract]
        Int32 Insert_Special_Closing_Dis_Details(CashSales_Out_RevItems objrev_det, out string _err);

        //Tharaka 11-07-2014
        [OperationContract]
        Int32 DeleteTempPromoVoucher(string CreateUser);
        //SHANUKA on 11/07/2014
        [OperationContract]
        DataTable Get_Dutyfree_Ex_Det(string _invNo);

        //Tharaka 2014-07-14
        [OperationContract]
        Int16 SaveTempPromoVoucherItems(DataTable _PromotionVoucherItems);

        //Tharaka 2014-07-15
        [OperationContract]
        DataTable GetPromotionItemsByBatchSeq(Int32 BatchSeq);

        //SHANUKA on 15/07/2014
        [OperationContract]
        Int32 SaveDutyFreeDet(CashSales_Out_RevItems objrev_det, out string _err);

        //Tharaka 2014-07-17
        [OperationContract]
        Int32 UpdatePromotionVoucherStatus(string CricularNo, int Status, string modUser);
        //Shalika 2014/07/24
        [OperationContract]
        DataTable Load_Summery_History(string _fuc_cd);
        //Shalika 2014/07/25
        [OperationContract]
        DataTable Get_ReqType();
        //shalika on 27/07/2014
        [OperationContract]
        Int32 SaveApprovalsAccountReschedule(CashSales_Out_RevItems objrev_det, out string _err);
        //shalika on 28/07/2014
        [OperationContract]
        DataTable Get_NewOutItemPrice(string _invNo, string _itm_Code);

        //SHANUKA on 29/07/2014
        [OperationContract]
        DataTable Load_Period_Over_Details(string _invNo);

        //Tharaka 29/07/2014
        [OperationContract]
        Int32 UpdateBusinessEntityProfileWithPermission(MasterBusinessEntity _businessEntity, string modby, DateTime modDate, Decimal Newcredlimit, List<MasterBusinessEntityInfo> bisInfoList, List<MasterBusinessEntitySalesType> _salesTypes);

        //SHANUKA on 30/07/2014
        [OperationContract]
        DataTable Load_DutyFree_det(string _invNo);
        //SHANUKA on 30/07/2014
        [OperationContract]
        DataTable Load_Specila_Closing_his(string _invNo);

        //SHANUKA on 30/07/2014
        [OperationContract]
        DataTable GetSerialsForItem(string _com, string _item, string _itemstatus);


        //SHANUKA on 31/07/2014
        [OperationContract]
        DataTable Get_Prf_Cent_Det(string invNo);


        //SHANUKA on 31/07/2014
        [OperationContract]
        DataTable Get_Loc_Det(string com, string prf);


        //SHANUKA on 1/08/2014
        [OperationContract]
        DataTable Get_Pblevel_book(string invNo);

        // Tharaka 01-08-2014
        [OperationContract]
        int ProcessPromotionApprove(List<string> _promoCodes, out string _err, out List<string> _errList, string user, string session);

        //written by Darshana on 02/08/2014
        [OperationContract]
        Int32 SaveHPExchangeNew(DateTime _date, string _accountno, string _company, string _location, string _profitcenter, string _createdBy, string _inSubType, string _outSubType, List<ReptPickSerials> _list, List<ReptPickSerials> _outList, List<InvoiceItem> _outPureInvoiceItem, List<RecieptHeader> receiptHeaderList, List<RecieptItem> receipItemList, MasterAutoNumber receipAuto, out string _creditnotelist, out string _inventorydoclist, HPAccountLog _accLog, HpAccount _newAccount, List<HpSheduleDetails> _currentSchedule, List<HpSheduleDetails> _newSchedule, HpInsurance _insurance, MasterAutoNumber _insuranceAuto, RequestApprovalHeader _request, out string _diriya, out string _invNo, out string _recNo, InventoryHeader _ccInv, MasterAutoNumber _invAuto);

        // Nadeeka 30-10-2015
        [OperationContract]
        Int32 SaveExchangeOutHP(DateTime _date, string _accountno, string _company, string _location, string _profitcenter, string _createdBy, string _inSubType, string _outSubType, List<ReptPickSerials> _list, List<ReptPickSerials> _outList, List<InvoiceItem> _outPureInvoiceItem, List<RecieptHeader> receiptHeaderList, List<RecieptItem> receipItemList, MasterAutoNumber receipAuto, out string _creditnotelist, out string _inventorydoclist, HPAccountLog _accLog, HpAccount _newAccount, List<HpSheduleDetails> _currentSchedule, List<HpSheduleDetails> _newSchedule, HpInsurance _insurance, MasterAutoNumber _insuranceAuto, RequestApprovalHeader _request, out string _diriya, out string _invNo, out string _recNo, InventoryHeader _ccInv, MasterAutoNumber _invAuto, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, string _outInv, string _out_do, Int32 _isNew, List<HpTransaction> _trans);

        //SHANUKA on 04/08/2014
        [OperationContract]
        DataTable GetJobNoServiceApp(string invNo);

        // Tharaka 05-08-2014
        [OperationContract]
        Int32 DiscountDefinitionChangeStatus(string _cir, Int16 status);

        // Tharaka 05-08-2014
        [OperationContract]
        int ApprovePromotionlDiscount(string _cir, string user, string session);

        //Tharaka on 06-08-2014
        [OperationContract]
        DataTable GetDiscountRequest(string Com, string PC, string Loc, string user);

        // Tharaka 07-08-2014
        [OperationContract]
        int UpdateDiscountDefintionStatus(string user, decimal discount, int status, string SeqNo, decimal amount);

        // Tharaka 08-08-2014
        [OperationContract]
        int InsertIntoSCM_POPUP_COLECTOR(PopupCollect oPopupCollector);

        // Tharaka 09-08-2014
        [OperationContract]
        int InsetInto_DicountRqstLog(DiscountReqLog oDiscountReqLog);

        // Shalika 12/08/2014
        [OperationContract]
        List<CashSalesRev_History> GetReprintDetails(string _Usrid, string _Com);

        //tharaka on 13-08-2014
        [OperationContract]
        int SaveHPDownPaymentReceipt(string User, string com, List<string> receiptList, string RequestNo, string remark, bool IsApprove);

        //tharaka on 13-08-2014
        [OperationContract]
        DataTable GetHPdownPaymentReceipt(string Com, string user, string sart_desc);

        //tharaka on 13-08-2014
        [OperationContract]
        RequestApprovalHeader GetRequest_Header_ByRef(string _com, string _ref);

        //tharaka on 13-08-2014
        [OperationContract]
        List<RequestApprovalHeaderLog> GetRequestHeaderLog(string reffNumber, string _Com);

        //tharaka on 13-08-2014
        [OperationContract]
        List<RequestApprovalSerialsLog> GetRequestSerialLog(string reffNumber);
        //Shalika 13/08/2014
        [OperationContract]
        DataTable Load_SaleForRevAccHistory(string _fuc_cd);

        //SHANUKA 13/08/2014
        [OperationContract]
        DataTable GetScmDet();
        //Shanuka Perera on 13/08/2014
        [OperationContract]
        Int32 UpdateReprintDets(CashSales_Out_RevItems objOutItems, out string _err);
        //Shanuka Perera on 13/08/2014
        [OperationContract]
        Int32 UpdateScmCommDets(CashSales_Out_RevItems objOutItems, out string _err);

        //tharaka on 14-08-2014
        [OperationContract]
        List<RequestApprovalHeader> Get_CustomerCreationRequset(string App_type, string user, string Company);

        //tharaka 2014-08-15
        [OperationContract]
        DataTable Get_CustomerRequsetDetails(string ReqNo, string Company);

        //Shanuka Perera on 15/08/2014
        [OperationContract]
        DataTable GetBankDetais(string com);

        //Shanuka Perera on 15/08/2014
        [OperationContract]
        DataTable GetPayModeDetas();


        //Shanuka Perera on 15/08/2014
        [OperationContract]
        Int32 SaveToDipositBankDet(List<Deposit_Bank_Pc_wise> listBankDetails, out string _err);

        //tharaka 2014-08-15
        [OperationContract]
        int CustomerCreationUpdate(string requestNo, string user, string company, string NICNo, bool status, string type);


        //Shanuka Perera on 15/08/2014
        [OperationContract]
        DataTable LoadBankNewDets();

        //shanuka 2014-08-16
        [OperationContract]
        Int32 InsertNewBankDets(Deposit_Bank_Pc_wise _Deposit, out string _err);

        //Shanuka Perera on 17/08/2014
        [OperationContract]
        DataTable Get_LoadDipDetais(string profit, DateTime from, DateTime To);
        //Shanuka Perera on 17/08/2014
        [OperationContract]
        Int32 UpdateToDiposit(List<Deposit_Bank_Pc_wise> listBankDetails, List<Deposit_Bank_Pc_wise> listBankSlips, out string _err);


        //Shanuka Perera on 21/08/2014
        [OperationContract]
        DataTable Load_Adj_TypesDets();


        //Shanuka Perera on 21/08/2014
        [OperationContract]
        DataTable Load_Sub_Adj_Types(string _AdjType);

        //Shanuka Perera on 21/08/2014
        [OperationContract]
        DataTable Load_Adj_Acc_Details(string _AdjType, string _subType);
        //darshana on 22-08-2014
        [OperationContract]
        Decimal Get_OutHPInsValue(string accNo);

        //Tharaka on 2014-08-21
        [OperationContract]
        int SaveAccountCancalRequest(RequestApprovalHeader ReqHdr, MasterAutoNumber _AppReqAuto, out string genaratedReqNo);
        //shanuka on 25/08/2014
        [OperationContract]
        Int32 InsertTo_sat_Adj(Deposit_Bank_Pc_wise _objadjust, out string _err);

        //shanuka on 25/08/2014
        [OperationContract]
        Int32 Insert_CheqRetn_with_Adj(RecieptHeader receiptHdr, Deposit_Bank_Pc_wise objDeposit, ChequeReturn _chequeReturn, RemitanceSummaryDetail _remsumdet, out string RtnReceiptNo);
        //shalika
        [OperationContract]
        DataTable GetUserAssignComment(string _usrId);

        //shanuka on 29/08/2014
        [OperationContract]
        bool Check_Available_Col_Bonus(string _company, string _profitcenter, string _voucher_no, DateTime _month);
        //shanuka on 29/08/2014
        [OperationContract]
        Int32 Insert_to_Col_bonus(List<Deposit_Bank_Pc_wise> listColl_bonus, out string _err);
        //shanuka 
        [OperationContract]
        DataTable Load_Recept_details(string _manual_ref_no);
        //shanuka on 1/09/2014
        [OperationContract]
        bool Check_Available_ProfitCenters(string _usr, string _com, string _profitcenter);

        //shanuka on 02/09/2014
        [OperationContract]
        Int32 UpdateToSat_hdr(decimal _anal8, string inv_no, out string _err);
        //shanuka 
        [OperationContract]
        DataTable Load_Default_PriceBook(string com, string prof_cen);
        //shanuka 
        [OperationContract]
        DataTable Load_Promotion_PriceBook(string com, string prof_cen);
        //shanuka 
        [OperationContract]
        DataTable Load_PcWise_PriceBook(string com, string prof_cen);
        //shanuka 
        [OperationContract]
        DataTable Load_PcWise_Price_level(string com, string prof_cen, string _pbook);
        //shanuka 
        [OperationContract]
        DataTable Load_Item_dets(string com, string p_lvl, string _pbook);
        //shanuka 
        [OperationContract]
        DataTable Check_price_bookDetails(string com, string _prof_center, string p_lvl, string _pbook, string stus);
        //shanuka 
        [OperationContract]
        Int32 Update_To_Parameters(Deposit_Bank_Pc_wise objDefaultpara, Deposit_Bank_Pc_wise objpromotion, out string _err);
        //shanuka 
        [OperationContract]
        bool Check_Available_Mids(string bankcode, string _mid, int _pun_tp);
        //shanuka 
        [OperationContract]
        DataTable Load_mids(string bankcode, int _pun_tp);
        //shanuka 
        [OperationContract]
        Int32 Insert_to_master_mid(Deposit_Bank_Pc_wise _obj_mas_mid, out string _err);
        //shanuka 
        [OperationContract]
        Int32 Update_to_master_mid(Deposit_Bank_Pc_wise _obj_mas_mid, out string _err);

        //Tharaka 10-09-2014
        [OperationContract]
        DataTable Get_buscom_branch_det(string bankcode);
        //Darshana 10-09-2014
        [OperationContract]
        DataTable GetAllPCWara(string _com, string _pc, string _itm);
        //shalika 10/08/2014
        [OperationContract]
        DataTable getCusInvoiceDetails(string _CusCode);
        //shanuka 10-09-2014
        [OperationContract]
        Int32 SaveToMasterPcWiseMid(List<Deposit_Bank_Pc_wise> lst_master_mid, out string _err);
        //shanuka 10-09-2014
        [OperationContract]
        DataTable getMerchantIdDets(int pun_tp);
        //shanuka 12-09-2014
        [OperationContract]
        DataTable Get_stus_for_mid(string bankcode, string _mid, int _pun_tp);
        //shanuka 12-09-2014
        [OperationContract]
        DataTable getAllMid_Details(string prof_cen);
        //shalika 15-09-2014
        [OperationContract]
        DataTable SearchAgreementTrackingData(string prof_cen, string Doc_no, string Pod_no, DateTime Fromdate, DateTime Todate, Int32 From, Int32 To, string ischk);
        //shanuka 
        [OperationContract]
        Int32 SaveToIssueChqBank(List<Deposit_Bank_Pc_wise> lst_chqBank, out string _err);
        //shanuka 
        [OperationContract]
        Int32 SaveToDocPages(List<Deposit_Bank_Pc_wise> lst_docpages, out string _err);
        //shalika 16/09/2014
        [OperationContract]
        Int32 UpdateToAgreementTracker(List<HPAgreementTracker> listAgreementTracker, string usr, out string _err);
        //shalika 17/09/2014
        [OperationContract]
        DataTable GetHeadofficeRecdata(string Acc_no);
        //Shalika 17/09/2014
        [OperationContract]
        DataTable LoadOtherClosedTypes(string type);
        //Shanuka 18/09/2014
        [OperationContract]
        DataTable Load_cheque_printing_details(string vou);
        //Written By Shalika on 19/09/2014
        [OperationContract]
        DataTable GetInvoiceItemDetails(string _company, string _profitCenter, string _account);
        //Written by Darshana 19-09-2014
        [OperationContract]
        DataTable GetTotalCRAmtByInv(string _invNo);
        //Written By Shanuka on 20/09/2014
        [OperationContract]
        DataTable get_Terms(string com, int puntp, string prof);
        //Written By Shanuka on 20/09/2014
        [OperationContract]
        DataTable get_mids_forIn(string com, int puntp, string prof, int term);
        //shanuka 
        [OperationContract]
        Int32 Update_to_mst_pc_mid(string com, string pc, int puntp, int term, string mid, string creatby, out string _err);
        //shanuka 
        [OperationContract]
        DataTable get_unused_doc_report(DateTime from, DateTime to, string profitcen);
        //shalika 30/09/2014
        [OperationContract]
        DataTable get_bank_mid_code(string branch_code, string pc, int mode, int period, DateTime _trdate, string _com);
        [OperationContract]
        DataTable check_mid_code_Allowed(string _com, string _pc);

        //Shalika 01/10/2014
        [OperationContract]
        DataTable LoadTransactionAccounts();

        //Shalika 02/10/2014
        [OperationContract]
        DataTable Load_MID_codes_perAcc(string AccNo, string pc);
        //shanuka 04/10/2014
        [OperationContract]
        Int32 SaveToAllocateItems(List<Deposit_Bank_Pc_wise> lst_all_items, out string _err);
        //shanuka 04/10/2014
        [OperationContract]
        DataTable getAllAllocateItems_details(string com);
        //shalika 11/10/2014
        [OperationContract]
        DataTable GetReturnType(string Acc_no);
        //Shalika 20/10/2014
        [OperationContract]
        InvoiceHeader GetInvoiceHeaderDetails_AGR(string _invoiceNo);
        //shanuka 23/10/2014
        [OperationContract]
        DataTable GetAll_Collection_Bonus(string com);
        //shanuka 23/10/2014
        [OperationContract]
        Int32 UpdateToCollectionBonus(Deposit_Bank_Pc_wise objColBonus, out string _err);
        //shalika 24/10/2014
        [OperationContract]
        DataTable GetNotRealizeTransactionDet(string com, string _acc, DateTime _Date, DateTime _from, DateTime _to, string _user, Int32 _isAsAt);
        //shalika 27/10/2014
        [OperationContract]
        DataTable Realization_Finaliz_sts(string acc, DateTime _fromDate, DateTime _toDate, string _com);
        //Shalika 27/10/2014
        [OperationContract]
        DataTable LoadUsers();
        //shalika 29/10/2014
        [OperationContract]
        DataTable Bank_Reconciliation_Rpt(DateTime _Asatdate, string Acc);
        //shanuka 29/10/2014
        [OperationContract]
        DataTable Get_Bank_Deposit_Slip(string pro, DateTime from, DateTime to);

        //shanuka 29/10/2014
        [OperationContract]
        DataTable Get_Receive_Cheque_details(string pro, DateTime from, DateTime to);
        //shanuka 30/10/2014
        [OperationContract]
        string Get_midno(string pro, string sun);
        //shalika 30/10/2014
        [OperationContract]
        DataTable GetRegistraionDetails(DateTime _from_date, DateTime _to_date, string _rpt_mode, string _usr, string _com, string pc);
        //Shalika 01/10/2014
        [OperationContract]
        DataTable LoadBankAccounts();

        //Tharaka 2014-11-04
        [OperationContract]
        DataTable Get_Currect_By_Book(string ITEMCODE, string BOOKNUM, string LOC);

        //Darshana 27/10/2014
        [OperationContract]
        GroupBussinessEntity GetCustomerProfileByGrup(string CustCD, string nic, string DL, string PPNo, string brNo, string mobNo);
        //Darshana 28-10-2014
        [OperationContract]
        Int32 SaveBusinessEntityDetailWithGroup(MasterBusinessEntity _businessEntity, CustomerAccountRef _account, List<MasterBusinessEntityInfo> bisInfoList, List<BusEntityItem> busItemList, out string customerCD, List<MasterBusinessEntitySalesType> SalesType, Boolean _isExsistCom, Boolean _isGroup, GroupBussinessEntity _groupCus, bool _isTours = true, List<MasterBusinessOfficeEntry> _MstBusOffEntry = null, string addasdriver = "false", 
            string pc = null, CustomsProcedureCodes _cusProcCd = null, SupplierWiseNBT _supplierNBT = null);
        //darshana 28-10-2014
        [OperationContract]
        Int32 UpdateBusinessEntityProfileWithGroup(MasterBusinessEntity _businessEntity, string modby, DateTime modDate, Decimal Newcredlimit, List<MasterBusinessEntityInfo> bisInfoList, List<MasterBusinessEntitySalesType> _salesTypes, List<BusEntityItem> busItemList, GroupBussinessEntity _grupCust, List<MasterBusinessOfficeEntry> _MstBusOffEntry = null, SupplierWiseNBT _supplierNBT = null, CustomerAccountRef _custAccount = null);
        //darshana 30-10-2014
        [OperationContract]
        HPAccountLog GetHpAccLogByTP(string _acc, string _tp);
        //darshana 31-10-2014
        [OperationContract]
        Int32 UpdateBusinessEntityProfileWithGroupWithPermission(MasterBusinessEntity _businessEntity, string modby, DateTime modDate, Decimal Newcredlimit, List<MasterBusinessEntityInfo> bisInfoList, List<MasterBusinessEntitySalesType> _salesTypes, GroupBussinessEntity _grupCust);

        //Tharaka 2014-12-03
        [OperationContract]
        //Int32 ServiceInvoiceSave(InvoiceHeader _invoiceHeader, List<Service_Confirm_detail> _confirmDetails, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, MasterAutoNumber _aodAuto, RCC _rcc, Boolean _isRcc, Boolean _isstockUpdate, InventoryHeader _aodHdr, List<ReptPickSerials> _reptPickSerialsAod, List<scv_agr_payshed> _lstShed, out string ADONumber);
        Int32 ServiceInvoiceSave(InvoiceHeader _invoiceHeader, List<Service_Confirm_detail> _confirmDetails, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, MasterAutoNumber _aodAuto, RCC _rcc, Boolean _isRcc, Boolean _isstockUpdate, InventoryHeader _aodHdr, List<ReptPickSerials> _reptPickSerialsAod, List<scv_agr_payshed> _lstShed, out string ADONumber, out string _scvreq,
               Service_Req_Hdr _jobHdrnew = null, List<Service_Req_Det> _jobItems = null, List<Service_free_det> _Service_free_detlist = null,
            List<Service_Req_Def> _jobDefList = null, List<Service_Req_Det_Sub> _jobDetSubList = null, MasterAutoNumber _recAuto = null,
            string _sbChnlnew = null, string _itemType = null, string _brandnew = null, Int32 _warStus = 0, MasterAutoNumber _masterAutonew = null,
           Int32 _isProcess = 0);

        //Tharaka 2014-12-17
        [OperationContract]
        Int32 SaveDiscount(CashGeneralDicountDef _genDisc);
        //Darshana 2014-12-29
        [OperationContract]
        List<LogMasterItemTax> GetTaxLog(string _company, string _item, string _status, DateTime _effDate);
        //Darshana 2014-12-29
        [OperationContract]
        List<LogMasterItemTax> GetItemTaxLog(string _company, string _item, string _status, string _taxCode, string _taxRateCode, DateTime _effDate);
        //Darshana 2014-12-31
        [OperationContract]
        List<MasterItemTax> GetTaxEffDt(string _company, string _item, string _status, DateTime _effDate);
        //Darshana 2014-Dec-31
        [OperationContract]
        List<MasterItemTax> GetItemTaxEffDt(string _company, string _item, string _status, string _taxCode, string _taxRateCode, DateTime _effDate);
        //Darshana 2015-01-12
        [OperationContract]
        MasterVehicalInsuranceDefinitionNew GetAddInsAmt(string _com, string _ptyTp, string _ptyCd, string _insCom, string _insPol, string _salesTp, DateTime _date, decimal _ItmVal, string _insTp, string _itm, string _cat1, string _cat2, string _brand, string _pb, string _lvl);
        //Darshana 2015-01-17
        [OperationContract]
        Decimal Get_HpIntAmt(Int32 _term, decimal _rent, decimal _amtF, Int32 _pv, Int32 _payEnd);
        //DArshana 2015-01-27
        [OperationContract]
        DataTable GetDeduceBalEcd(DateTime _tDate, string _acc);
        //Darshana 2015-01-27
        [OperationContract]
        DataTable GetDeduceBal(DateTime _fDate, DateTime _tDate, string _com, string _pc, Int32 _isSum, string _acc);
        //Darshana 2015-02-25
        [OperationContract]
        MasterItemWarrantyPeriod GetItemWarrEffDt(string _item, string _status, Int32 _act, DateTime _effDate);
        //Darshana 2015-02-25
        [OperationContract]
        LogMasterItemWarranty GetItemWarrEffDtLog(string _item, string _status, Int32 _act, DateTime _effDate);
        //Darshana 2015-03-04
        [OperationContract]
        int SaveOthPCECDRequest(RequestApprovalHeader ReqHdr, MasterAutoNumber _AppReqAuto, OutSMS _newSMS, out string genaratedReqNo);
        //Darshana 2015-07-18
        [OperationContract]
        InvoiceHeader GetInvoiceHeader(string _invoiceno);


        [OperationContract]
        DataTable CheckRequestType(string com, string pc, string doc_tp);
        [OperationContract]
        DataTable CheckExecutive(string emp_id);
        [OperationContract]
        DataTable Select_EMP_ID(string usr_name);


        //Tharaka 2015-08-04
        [OperationContract]
        DataTable GETCUST_BY_ACC(string _company, string _profitcenter, string _Acc);

        [OperationContract]
        Int32 SaveSalesOrderRequest(INT_REQ int_req, List<INT_REQ_ITM> int_req_itm, MasterAutoNumber mastAutoNo, out string msg);
        [OperationContract]
        Int32 UpdateStatus_INT_REQ(string req_no, string stus);
        [OperationContract]
        INT_REQ GetRequest(string req_no);
        [OperationContract]
        List<INT_REQ_ITM> GetRequestItem(Int32 seq_no);
        [OperationContract]
        DataTable Check_INT_REQ(string req_no, string tp, string excecutive);
        [OperationContract]
        Int32 UpdateSalesOrderRequest(INT_REQ int_req, List<INT_REQ_ITM> int_req_itm, out string msg);

        //Sahan 12/Aug/2015
        [OperationContract]
        DataTable SearchSalesRequest(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 12/Aug/2015
        [OperationContract]
        DataTable SearchCustomer(string p_mbe_com, string p_mbe_cd);
        [OperationContract]
        DataTable SearchCustomer2(string p_mbe_com, string p_mbe_cd);


        //Sahan 12/Aug/2015
        [OperationContract]
        Tuple<int, string> PlaceSalesOrder(SalesOrderHeader SalesOrder, MasterAutoNumber _masterAutoNumber);

        //Sahan 12/Aug/2015
        [OperationContract]
        DataTable SearchSalesOrder(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 12/Aug/2015
        [OperationContract]
        Int32 SaveSOItems(SalesOrderItems SalesOrderItems);

        //Sahan 12/Aug/2015
        [OperationContract]
        Int32 SaveSOItemTax(SalesOrderItemTax SalesOrderItemTax);

        //Sahan 12/Aug/2015
        [OperationContract]
        Int32 SaveSOSer(SalesOrderSer _SalesOrderSer);

        //Sahan 12/Aug/2015
        [OperationContract]
        Int32 UpdateSOStatus(SalesOrderHeader _SalesOrder, out string _error);

        [OperationContract]
        DataTable Check_INT_REQ_RER(string req_no, string tp);

        [OperationContract]
        List<INR_LOC> GetINR_LOC(string com, string loc, string item_cd);

        [OperationContract]
        Int32 SaveReservationRequest(INT_REQ int_req, List<INT_REQ_ITM> int_req_itm, List<INT_REQ_SER> int_req_ser, MasterAutoNumber mastAutoNo, out string msg);

        [OperationContract]
        DataTable Select_MST_STUS(string cd);

        [OperationContract]
        List<INT_REQ_SER> GetINT_REQ_SER(Int32 seq_no);

        [OperationContract]
        DataTable Select_REF_LOC_CATE1();

        [OperationContract]
        DataTable Select_REF_REQ_SUBTP(string main_tp);

        //Sahan 14/Aug/2015
        [OperationContract]
        DataTable SearchLocations(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 14/Aug/2015
        [OperationContract]
        DataTable SearchSalesOrdData(string p_soh_so_no, string p_soh_com, string p_soh_pc);

        //Sahan 14/Aug/2015 
        [OperationContract]
        List<SalesOrderItems> SearchSalesOrdItems(string p_soi_so_no);

        //Sahan 14/Aug/2015
        [OperationContract]
        DataTable SearchSalesOrdItemTax(string p_sotx_so_no);

        //Sahan 14/Aug/2015
        [OperationContract]
        DataTable SearchSalesOrdSerials(string p_sose_so_no);

        //Sahan 15/Aug/2015
        [OperationContract]
        DataTable GetInvReqItems(string p_seq_no, string p_itri_com, string p_itri_loc);

        //Sahan 18/Aug/2015

        [OperationContract]
        DataTable GetWareHousetemsData(string p_inl_com, string p_inl_loc, string p_inl_itm_cd, string p_inl_itm_stus);


        //Sahan 18/Aug/2015
        [OperationContract]
        DataTable GetItemStatusVal(string p_mis_desc);

        //Sahan 18/Aug/2015
        [OperationContract]
        DataTable GetItemStatusTxt(string p_mis_cd);


        //Sahan 18/Aug/2015
        [OperationContract]
        Int32 BalanceItemStock(INT_REQ_ITM _ReqItem);

        //Pemil 2015-08-20
        [OperationContract]
        List<INT_REQ> Search_INT_REQ(string com, string code, string chnl, string pc, DateTime fdate, DateTime tdate);

        //Pemil 2015-08-20
        [OperationContract]
        DataTable SELECT_INT_REQ_ITMbyREQ_NO(string req_no);

        //Pemil 2015-08-20
        [OperationContract]
        DataTable Select_REF_LOC_TP(string cd);

        //Pemil 2015-08-21
        [OperationContract]
        DataTable Select_InventoryBalance(string cd, string itm_cd,string company=null);

        //Pemil 2015-08-22
        [OperationContract]
        Int32 SaveReservationApproval(INR_RES inr_res, List<INR_RES_DET> inr_res_det, List<INR_RES_LOG> inr_res_log, MasterAutoNumber mastAutoNo, out string msg, out string finaldoc);

        //Pemil 2015-08-22
        [OperationContract]
        Int32 UpdateReservationApproval(INR_RES inr_res, List<INR_RES_DET> inr_res_det, out string msg);

        //Pemil 2015-08-22
        [OperationContract]
        INR_RES GetReservationApproval(string req_no);

        //Pemil 2015-08-22
        [OperationContract]
        List<INR_RES_DET> GetGetReservationApprovalItem(Int32 seq_no);

        //Pemil 2015-08-27
        [OperationContract]
        List<INR_RES_LOG> GetGetReservationlog(Int32 seq_no, string cd);

        //Pemil 2015-08-28
        [OperationContract]
        List<INT_REQ> GetINT_REQList(Int32 seq_no);

        //Darshana 21-08-2015
        [OperationContract]
        DataTable GetDetailsforBatchPrice(string p_com, string p_loc, decimal p_margin, string p_cat1, string p_cat2, string p_cat3, string p_docNo, DateTime _frmDt, DateTime _toDt);

        //Rukshan 26-08-2015
        [OperationContract]
        DataTable GetDetailsforBarcodePrice(string p_BType, string p_lvlcode, string p_Icode);

        //Pemil 2015-08-22
        [OperationContract]
        List<INR_RES> Select_INR_RES(string com, string chnl, string res_no, string tp, string cust_cd, DateTime fromDate, DateTime toDate, string status);

        //Pemil 2015-08-31
        [OperationContract]
        Int32 CancelINR_RES_LOG(List<INR_RES_LOG> inr_res_log, decimal qty, string mod_by, DateTime mod_dt);

        //Tharaka 2015-09-29
        [OperationContract]
        List<INR_RES> GET_RESERVATION_HDR(string com, string custCode, String Status,string _loc);

        //Tharaka 2015-09-30
        [OperationContract]
        List<INR_RES_DET> GET_RESERVATION_DET(Int32 seq_no,string _doc);

        //Rukshan 2015-10-15
        [OperationContract]
        List<ReptPickSerials> GetInvItem(string _doc);

        //Sanjeewa 17-10-2015
        [OperationContract]
        DataTable Get_GroupSaleInvoiceDetails(DataTable _groupsale);

        [OperationContract]
        DataTable Get_GroupSaleInvoiceDetails1(string _invno);

        //Tharaka 2015-10-19
        [OperationContract]
        List<ReptPickSerials> GET_BUYBACKITMES_FOR_INVOIC(string invoice);

        //Tharaka 2015-09-29
        [OperationContract]
        List<InvoiceItem> GET_INV_ITM_BY_RESNO_LINE(String Res, Int32 ResLine);

        //Sahan 02/Nov/2015
        [OperationContract]
        Int32 UpdatePriceBook(PriceBookRef book);

        //Sahan 02/Nov/2015
        [OperationContract]
        DataTable SearchPriceBooks(string company);

        //Tharaka 2015-11-02
        [OperationContract]
        List<CashGeneralEntiryDiscountDef> GET_DIS_REQ_BY_CUSTOMER(string saleType, string com, string pc, DateTime date, string customer);

        //Sahan 03/Nov/2015
        [OperationContract]
        Int32 UpdatePriceBookLevel(PriceBookLevelRef booklevel);

        //Sahan 04/Nov/2015
        [OperationContract]
        DataTable SearchPriceBookLevels(string company, string code);

        //Tharaka 2015-12-03
        [OperationContract]
        List<InterCompanySalesParameter> GET_INTERCOM_PAR_BY_CUST(string Com, String Cust);

        //Tharaka 2015-12-04
        [OperationContract]
        List<ReptPickSerials> GetServicesForPO(String Com, String loc, String InvoiceNumber);

        //Darshana 2015-12-04
        [OperationContract]
        List<PaymentType> GetPossiblePaymentTypes_Default(string _com, string _schnl, string _pc, string txn_tp, DateTime today, Int32 _isBOCN);

        //Tharaka 2015-12-22
        [OperationContract]
        Int32 ReceiptInvoiceAllocation(RecieptHeader _NewReceipt, List<RecieptItem> oRecieptItems, List<RecieptItem> oDebitInvoices, out string err);

        //Tharaka 2015-12-28
        [OperationContract]
        DataTable GET_SAO_ITEMS_BY_SO_NO(string code);

        //Tharaka 2015-12-28
        [OperationContract]
        DataTable GET_SAO_SER_BY_SO_NO(string code);

        //Darshana 2015-12-30
        [OperationContract]
        List<MasterPCTax> GetPcTax(string _company, string _pc, Int16 _stus, DateTime _dt);
        //hasith 25/01/2016
        //[OperationContract]
        //DataTable GetCreditnoteDetails(DateTime _fromdate, DateTime _todate, string _com, string _pc);

        //Lakshan 2016-01-30
        [OperationContract]
        Int32 Save_Exchange_Rate(List<MasterExchangeRate> _Exchange, string p_type);

        //Rukshan 2016-02-06
        [OperationContract]
        DataTable GETRES_PO_DETAILS(string _com, string _sbu, string _supp, string _blno, DateTime _fdate, DateTime _todate, string _item);

        [OperationContract]
        DataTable getPOAllocation(string _po);

        //Written by Sanjeewa on 18/02/2016
        [OperationContract]
        Int16 CheckBalanceInvoiceQty(string _invoice, string _item);

        //Written by Sanjeewa on 19/02/2016
        [OperationContract]
        Boolean CheckIsAccountClosed(string _invoice, DateTime _asatdate);

        //Lakshan 19-02-2016
        [OperationContract]
        Int32 SaveBusinessEntityDetailWithGroupNew(MasterBusinessEntity _businessEntity, CustomerAccountRef _account, List<MasterBusinessEntityInfo> bisInfoList,
            List<BusEntityItem> busItemList, out string customerCD, List<MasterBusinessEntitySalesType> SalesType,
            Boolean _isExsistCom, Boolean _isGroup, GroupBussinessEntity _groupCus, MasterItem _item, SupplierWiseNBT _supplierNBT = null);

        //Lakshan 19-02-2016
        [OperationContract]
        Int32 UpdateBusinessEntityProfileWithGroupNew(MasterBusinessEntity _businessEntity, string modby, DateTime modDate, Decimal Newcredlimit, List<MasterBusinessEntityInfo> bisInfoList,
            List<MasterBusinessEntitySalesType> _salesTypes, List<BusEntityItem> busItemList, GroupBussinessEntity _grupCust, MasterItem _item, SupplierWiseNBT _supplierNBT = null);

        //Nuwan 2016/02/19
        [OperationContract]
        LoyaltyMemeber getLoyaltyDetails(string customer, string loyalNu);

        //Nuwan 2016/02/20
        [OperationContract]
        GiftVoucherPages getGiftVoucherPage(string voucherNo, string voucherBook, string company);

        /*Nuwan 2016/02/26*/
        [OperationContract]
        List<PriceDefinitionRef> GetPriceDefinitionRefs(PriceDefinitionRef _priceDef);

        /*Lakshan 2016-02-27*/
        [OperationContract]
        Int32 UpdatePriceDefinitionRef(PriceDefinitionRef _ref, out string err);

        //Sahan 29/Feb/2016
        [OperationContract]
        Int32 UpdateSerialAvailability(string company, string location, string serial);

        //Sahan 07/Feb/2016
        [OperationContract]
        DataTable Check_PC_SO_REST_STK(string company, string pc);

        //Sahan 07/Feb/2016
        [OperationContract]
        Int32 UpdateINRLoc(string company, string location, string Item, string status, Decimal _qty);

        //Sahan 17 Mar 2015
        [OperationContract]
        DataTable CheckLocationBaBalance(string company, string loc, string item, string itemstus);

        //Sahan 18 Mar 2016
        [OperationContract]
        DataTable LoadPoData(string company, string pc, string doc);
        //subodana
        [OperationContract]
        List<QUO_COST_DET> GetInvoiceDetailsForEnq(string cuscode, string enqid);

        //Ruksha 24 Mar 2016
        [OperationContract]
        Int32 SaveSalesOrder(SalesOrderHeader SalesOrder_hdr, MasterAutoNumber mastAutoNo, List<SalesOrderItems> SalesOrder_itm, List<SalesOrderSer> SalesOrder_ser, out string msg, bool _AppStatus = false);

        //Darshana 29-03-2016
        [OperationContract]
        void GetPromotionNew(string _company, string _profitcenter, string _item, DateTime _date, string _customer, decimal _qty, out List<PriceDetailRef> _priceDetailRefPromo, out List<PriceSerialRef> _priceSerialRefPromo, out List<PriceSerialRef> _priceSerialRefNormal);
        //subodana 2016-04-05
        [OperationContract]
        Int32 Save_ExchangeRate(MasterExchangeRate _exg);

        //nuwan 2016/04/18
        [OperationContract]
        DataTable GetReceiptPayDetails(string recno);

        //Rukshan 2016/04/26
        [OperationContract]
        List<RequestApprovalHeader> getcompleded_ReqbyType(string _com, string _pc, string _type, string _user, string _selectPC);

        //Rukshan 2016/04/27
        [OperationContract]
        SarDocumentPriceDefn GetDocPriceDetails(string _company, string _profitcenter, string _book, string _level, string _doctype);
        //SUBODANA 2016-05-03
        [OperationContract]
        DataTable GetItemCodeDes(string ORNO);

        //Rukshan 2016/05/03
        [OperationContract]
        DataTable GetChanelData(string _company, string _code);

        //SUBODANA
        [OperationContract]
        List<InventorySearchItemsAll> GETALLITEMS(string docno, string code);

        //Rukshan 2016/05/09
        [OperationContract]
        Int32 SaveBOQHDD(SatProjectHeader _BOQ, MasterAutoNumber mastAutoNo, List<SatProjectDetails> Cost, out string doc);

        //Rukshan 2016/05/10
        [OperationContract]
        SatProjectHeader GETBOQHDR(string com, string code);

        //Rukshan 2016/05/10
        [OperationContract]
        List<SatProjectDetails> GETBOQDETAILS(string code);

        //Rukshan 2016/05/09  //Edited By Dulaj 2018/Dec/15
        [OperationContract]
        Int32 UpdateBOQHDD(SatProjectHeader _BOQ, List<SatProjectDetails> Cost, List<SatProjectKitDetails> satKitDetails, out string doc);

        //Rukshan 2016/05/09
        [OperationContract]
        Int32 APPROVEBOQHDD(SatProjectHeader _BOQ, out string doc);

        //Rukshan 2016/05/11
        [OperationContract]
        DataTable GetLASTMONTHSALE(string _com, string _pc, DateTime _from, DateTime _to, string _item);

        //Rukshan 2016/05/11
        [OperationContract]
        String GetDescription(string req_no);

        //Kelum : Get Bus Entity Profile: 2016-May:13
        [OperationContract]
        MasterBusinessEntity GetBusEntityProfile(string CustCD, string nic, string DL, string PPNo, string brNo, string com, string mobile, string type, int activestatus);

        //subodana 2016-05-19
        [OperationContract]
        DataTable GetProfitCenterDetails(string company, string pclist);

        //Rukshan 2016-05-20
        [OperationContract]
        List<ImpCusdecHdr> GECUSBOND_CUS(string _com, string _loc, string _cus, string _code, DateTime _form, DateTime _To);

        //subodana 2016-06-08
        [OperationContract]
        DataTable GetPC_from_Hierachy_with_Opteam(string com, string channel, string subChannel, string area, string region, string zone, string pc_code, string opteam);
                
        //Lakshan 09 Jun 2016
        [OperationContract]
        DataTable GetSalesTrackerInvoices(InvoiceHeader hdr, InvoiceItem itm);

        //Lakshan 09 Jun 2016
        [OperationContract]
        DataTable GetIntHdrData(InventoryHeader hdr);

        //Lakshan 09 Jun 2016
        [OperationContract]
        DataTable GetIntHdrDataByInv(InventoryHeader hdr);

        //subodana 2016-06-12
        [OperationContract]
        DataTable GetReversalInvoiceData(string _com, string _invNo);

        //subodana 2016-06-14
        [OperationContract]
        Int32 UpdateBusinessEntityProfileWithGroupNeww(MasterBusinessEntity _businessEntity, CustomerAccountRef _account, string modby, DateTime modDate, Decimal Newcredlimit, List<MasterBusinessEntityInfo> bisInfoList, List<MasterBusinessEntitySalesType> _salesTypes, List<BusEntityItem> busItemList, GroupBussinessEntity _grupCust
            , List<MasterBusinessOfficeEntry> _MstBusOffEntry = null,CustomsProcedureCodes _cusProcCd = null);

        //Rukshan 2016-06-23
        [OperationContract]
        CashPromotionDiscountDetail GetitemDiscount(string _pctype, string _pc, string _item, string _stype, DateTime _date, string _book,
                                                     string _lvl, decimal _qty);

        //Subodana 2016-06-25
        [OperationContract]
        DataTable GetTotalApproveQty(string req_no);

        //Subodana 2016-06-25
        [OperationContract]
        Int32 SaveReservationApprovalNew(INR_RES inr_res, List<INR_RES_DET> inr_res_det, List<INR_RES_LOG> inr_res_log, MasterAutoNumber mastAutoNo, out string msg, out string finaldoc, decimal Totalallqty, decimal BeforetotAppqty, bool _isPendingReq, string _pendingDoc);

        //Subodana 2016-07-05
        [OperationContract]
        DataTable GetUnitPriceNew(string itemcode, string loc);

        //Lakshan 2016-07-06
        [OperationContract]
        List<INR_RES_DET> GetInrResDet(INR_RES_DET _obj);

        //Subodana 2016-07-06
        [OperationContract]
        Int32 Update_filenoforCostsheet(string _com, string _doc, string file);

        //Subodana 2016-07-07
        [OperationContract]
        DataTable GetSOSeqno(string com, string sono);

        //Subodana 2016-07-07
        [OperationContract]
        int Update_SalesOrder(List<SalesOrderItems> SalesOrder_itm, Int32 seq, string _sono, string cuscode, string com, Int32 reqseqno, SalesOrderHeader SalesOrder_hdr);

        //Subodana 2016-07-07
        [OperationContract]
        DataTable GetREQITMSeqno(string com, string sono);

        //Subodana 2016-07-26
        [OperationContract]
        Int32 SavesunAccount(MasterBusinessEntity _businessEntity);

        //Lakshan 27 Jul 2016
        [OperationContract]
        Int32 SaveSalesForecastingCalendar(MasterAutoNumber _mstAutoCalendar, SalesForecastingCalendar _salForCasCal,out string _docNo,out string _err);

        //Lakshan 27 Jul 2016
        [OperationContract]
        DataTable SearchSalesForecastingCalendar(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 27 Jul 2016
        [OperationContract]
        List<SalesForecastingCalendar> GetSarForCal(SalesForecastingCalendar _obj);

        //Lakshan 29 Jul 2016
        [OperationContract]
        Int32 SaveSalesForecastingCompany(List<SalesForecastingCalendarCom> _calComList, out string _err);

        //Lakshan 29 Jul 2016
        [OperationContract]
        List<SalesForecastingCalendarCom> GetSarForCom(SalesForecastingCalendarCom _obj);

        //Lakshan 01 Aug 2016
        [OperationContract]
        List<SalesForecastingMasterPeriod> Get_MST_FOR_PD_TP(SalesForecastingMasterPeriod _obj);

        //Lakshan 01 Aug 2016
        [OperationContract]
        List<SalesForecastingPeriod> Get_SAR_FOR_PD(SalesForecastingPeriod _obj);

        
        //Lakshan 01 Aug 2016
        [OperationContract]
        Int32 SaveSalesForecastingPeriod(MasterAutoNumber _mstAutoSalForPer ,SalesForecastingPeriod _obj, out string _err, out string _docNo);

        //Lakshan 2016 Aug 02
        [OperationContract]
        DataTable SearchSalesForecastingPeriod(string _initialSearchParams, string _searchCatergory, string _searchText, string frmDt, string toDt);

        //Lakshan 2016 Aug 08   
        [OperationContract]
        DataTable SearchSalesForecastingPeriodByCal(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 2016 Aug 08   
        [OperationContract]
        Int32 SaveSalesForecasting(SalesForecastingHeader _sarHdr, List<SalesForecastingDetail> _sarDet, out string _err);

        //Lakshan 2016 Aug 08   
        [OperationContract]
        List<SalesForecastingDetail> GET_SAR_FOR_DET_BY_HDR(SalesForecastingHeader _hdr);

     

        //Lakshan 2016 Aug 16
        [OperationContract]
        List<MasterBusinessEntity> GetCustomerProfileNew(MasterBusinessEntity _obj);

        //subodana 2016-08-22
         [OperationContract]
        DataTable GET_CST_DATA_BYDOC(string docno);

         //rukshan 2016-09-10
         [OperationContract]
         bool Check_resno(string _COM, string DOCNO,string _cus,string _loc);

        //subodana
         [OperationContract]
         Int32 SaveBusinessEntityDetailWithGroupNew2(MasterBusinessEntity _businessEntity, CustomerAccountRef _account, List<MasterBusinessEntityInfo> bisInfoList, List<BusEntityItem> busItemList, out string customerCD, List<MasterBusinessEntitySalesType> SalesType, Boolean _isExsistCom, Boolean _isGroup, GroupBussinessEntity _groupCus, bool _isTours = true, List<MasterBusinessOfficeEntry> _MstBusOffEntry = null, string addasdriver = "false"
     , string pc = null, CustomsProcedureCodes _cusProCd = null);

        //SUBODANA 
        [OperationContract]
       DataTable GET_HSLIMIT_DATA(string loc, DateTime firstdate, DateTime lastdate);

        //SUBODANA 
        [OperationContract]
        DataTable GET_HSINVDATA(string loc, DateTime firstdate, DateTime lastdate);

        //SUBODANA 
        [OperationContract]
        DataTable GET_PCFROMLOC(string loc, string com);

        //SUBODANA 
        [OperationContract]
        DataTable GET_ADVRECFORMRN(string itmcode, string com, string pc, DateTime fromdate, DateTime todate);
       
        //SUBODANA 
        [OperationContract]
        DataTable SP_GETERRLINEDOC(string com, DateTime fromdate, DateTime todate);

        //SUBODANA 
        [OperationContract]
        DataTable SP_GETERRLINEDOC2(string doc, string itm, Int32 line, Int32 batchline,string status);

        //Lakshika 2016-10-14
        [OperationContract]
        DataTable GetDiscountReportDetails(string _com, string _pc, string _itemCode, string _cat1, string _cat2, string _cat3,
            string _brand, string _model, string _customer, string _executive, DateTime _fromDate, DateTime _toDate, int _fromDisc, int _toDisc, string _user);

        //Lakshika 2016/10/20 -audit rpt
        [OperationContract]
        DataTable GetStockSummaryReportDetail(string _User, string _JoNo);


        //Rukshan 
        [OperationContract]
        List<MasterItemTax> GetCustomerTax(string _cus, string _company);

        //RUKSHAN 2016-10-23
        [OperationContract]
        List<ReptPickSerials> Get_TEMP_PICK_SER_BY_loc(string _company, string _docNo, string _loc);


        //Randima 2016-11-17
        [OperationContract]
        List<RecieptHeader> GetRecieptByRefDoc(string _com, string _refDocNo);

        //RUKSHAN 26 Nov 2016
        [OperationContract]
        List<InterCompanySalesParameter> GET_INTERCOM_PAR_BY_SUP(string _com, string _sup);

        //RUKSHAN 26 Nov 2016
        [OperationContract]
        List<ProductionFinGood> GETFINGOD(string code);

        //RUKSHAN 26 Nov 2016
        [OperationContract]
        List<ProductionPlaneDetails> GETSATPROJ_LINE(string code);

        //Rukshan 15/Dec/2016
        [OperationContract]
        DataTable SearchSalesOrderRequest(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _from, DateTime _to);


        //Randima 16 Dec 2016
        [OperationContract]
        List<SalesForecastingHeader> GET_SAR_FOR_HDR(string _com, string _calCd, string _pdCd); 

        
        
        //Add by Akila 2016/12/20
        [OperationContract]
        DataTable GetAdvanReceiptSettings(string _CompanyCode, string _profitCenter, string _receiptType, string _subReceiptType, string _itemCategory = null);
        //Lakshan 2016 Dec 21
        [OperationContract]
        Int32 DeleteSalesForecasting(SalesForecastingHeader _sarHdr, List<SalesForecastingDetail> _sarDet, out string _err);
        //Lakshan 2016 Dec 21
        [OperationContract]
        DataTable SearchSalesForecastingCalendarWithDate(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Add by akila 2017/01/18 Load HP schemas - use the script sp_get_act_sch_web_new which was developed by Dilantha
        [OperationContract]
        DataTable LoadHPSchemasNew(DateTime _date, string _comCode, string _location, string _item, string _priceBook, string _priceBookLevel);
        //subodana
          [OperationContract]
        List<InvoiceItem> GetRefInvHeaderDetailsList(string _invoiceNo, int status);

        //Isuru 2017/03/17
          [OperationContract]
          Int32 SaveBrandMangerDtL(List<mst_brnd_alloc> _managerdetailsList);

          //Isuru 2017/03/17
          [OperationContract]
          List<mst_brnd_alloc> GetBrandManagerDet(string com, DateTime fdate, DateTime tdate);
          
         //Isuru 2017/03/23
          [OperationContract]
          Int32 SavePrefixMasterDetails(List<sar_tp> _PrefixList);

        //Isuru 2017/03/24
          [OperationContract]
          Int32 SaveSunLedgerDetails(List<gnr_acc_sun_ledger> _sunledgerlist1);

          //Isuru 2017/03/27
          [OperationContract]
          Int32 UpdatePrefixMasterDetails(List<sar_doc_price_defn> _PrefixList1);
          //Isuru 2017/03/27
          [OperationContract]
          DataTable CheckPrefixMasterDetails(sar_doc_price_defn _PrefixList1);
          [OperationContract]
          Int32 BOQCanclation(string com, string loc, string boqNo);
          //Udaya 27/04/2017
          [OperationContract]
          Int32 UpdateBOQsatProHdr(SatProjectHeader _BOQ, List<SatProjectDetails> Cost, out string doc);
          //Udaya 27/04/2017
          [OperationContract]
          DataTable SOACancleCheck(string itr_ref);
          //Dilshan 08/03/2018
          [OperationContract]
          DataTable SOACancleChecknew(string itr_ref, string invoiceno);
          //Isuru 2017/04/28
          [OperationContract]
          Int32 SavePluCreationDetails(List<mst_busentity_itm> _pludetailslist);
          //Udaya 29/04/2017
          [OperationContract]
          Int32 BOQCostDelete(string boqNo, int seqNo, int lineNo, string itemNo);

          //Add by Lakshan 29 Apr 2017
          [OperationContract]
          RecieptItem GET_SAT_RECIEPT_FOR_RETURN_CHK(string itr_ref);

          //Isuru 2017/05/02
          [OperationContract]
          List<mst_busentity_itm> Getcustomerpluupdate(string com, string cuscode, string itmcode);

          //Isuru 2017/05/02
          [OperationContract]
          Int32 UpdatePluCreationDetails(List<mst_busentity_itm> _pludetailslist);
        //Udaya 08/05/2017
          [OperationContract]
          SupplierWiseNBT GetSupplierNBT(string _company, string _suppCode);

          //Tharanga 2017/05/08
          [OperationContract]
          DataTable Get_SVID_VAL(string _p_svid_com, string _p_svid_pty_cd, string _p_svid_ins_tp, DateTime _fromDate);

          //Isuru 2017/05/16
          [OperationContract]
        DataTable getjobdetailsforjobinvoiceall(string jobnum);

          //Isuru 2017/05/19
          [OperationContract]
          DataTable getmeasuredetails(string _itm);

          //Isuru 2017/05/19
          [OperationContract]
          DataTable typedetails(string _invNo);

        //Tharanga 2017/05/20
         [OperationContract]
          DataTable GetmaxCount_SCH_ALW_COM(string sch_tp);
        //Tharanga 2017/05/20
         [OperationContract]
         Int32 Save_SCH_ALW_COM(List<SchemetypeCom> _SchemetypeCom);

         //Tharanga 2017/05/22
         [OperationContract]
         DataTable Get_SCH_ALW_COM(string sch_tp);

         //Tharanga 2017/05/22
         [OperationContract]
         Int32 update_SCH_ALW_COM(List<SchemetypeCom> _SchemetypeCom);
          //Tharanga 2017/05/25
         [OperationContract]
         DataTable GetSchemes_alw_com(string _type, string _code, string _com);
         //Tharanga 2017/05/25
         [OperationContract]
         List<HpSchemeType> GetSchemeTypeByCat_com(string _cat, string _com);

        //Isuru 2017/05/30
         [OperationContract]
         DataTable getcompanyforpanalty(string com);

         //Dilshan 2017/09/18
         [OperationContract]
         DataTable getsaleswithserial(string selectedcompany, string Channel, string Brand, string txtModel, string txtItem, DateTime fromdate, DateTime todate, string Category, string type, string pc, string itemcode, string txtCircular, string txtSchemeType, string txtSchemeCode, string txtPriceBook, string txtPriceLevel, string txtInvoiceType, string txtCustomer, string txtExecutive, string allreversal, string allfissue, string allicom);

         //Dilshan 2017/09/18
         [OperationContract]
         DataTable getsaleswithpayment(string selectedcompany, string Channel, string Brand, string txtModel, string txtItem, DateTime fromdate, DateTime todate, string Category, string type, string pc, string itemcode, string txtCircular, string txtSchemeType, string txtSchemeCode, string txtPriceBook, string txtPriceLevel, string txtInvoiceType, string txtCustomer, string txtExecutive, string allreversal, string allfissue, string allicom);

         //Tharanga 2017/06/07
         [OperationContract]
         DataTable get_sar_provou_tp(string com, string invNo);
        //Udaya 04.07.2017 Exceute Sales Target
         [OperationContract]
         DataTable executive_Sales_Target(string ProfitCenter, DateTime FromDate, DateTime ToDate, string circularCode, string Epf);
         //Tharanga 2017/07/24
         [OperationContract]
         Int32 SaveMyAbansDetailsNew(MyAbans _myab, LoyaltyMemeber _loyal, Boolean _isExist, string _Ser_no);

         //Written By Tharanga on 2018/08/09
         [OperationContract]
         List<PriceSerialRef> GetEnquirySerialDetailwithLoc(string _profitcenter, int _startFrom, int _endFrom, string _user, string _company, string _priceBook, string _priceLevel, string _customer, string _item, string _category1,
             string _category2, string _category3, string _status, string _pricetype, string _circular, DateTime _fromDate, DateTime _toDate, bool _isHistory, bool _isAsAtHistory,
             bool _isAllStatus, bool _isSuperUser, string _loc);

         //Add by Lakshan 20 Aug 2017
         [OperationContract]
         List<SatProjectDetails> GET_SAT_PRO_DET_DATA(SatProjectDetails _obj);
         //Tharanga 2017/08/29
         [OperationContract]
         DataTable getrootDesc(string _Com, string _route);
         //Tharanga 2017/08/30
         [OperationContract]
         List<InvoiceItem> Getref_doc_items(string _invoice);
        //Added By Udaya 07.09.2017
         [OperationContract]
         Int32 SupplierNBTDetails(SupplierWiseNBT _NBTDetails);
         //Add by Lakshan 20 Aug 2017
         [OperationContract]
         Int32 CancelReservationDocument(List<INR_RES_LOG> inr_res_log, out string _err);
         //Tharanga 2017/09/04
         [OperationContract]
         DataTable getinvshed_item(string _Com, string _loc, string _inv);
         //Tharanga 2017/10/02
         [OperationContract]
         Int32 CreateRefundNEW(RecieptHeader _refundHdr, List<RecieptItem> _refundItm, MasterAutoNumber _receiptAuto, List<ReceiptItemDetails> _resItmDet, RemitanceSummaryDetail _remsumdet, out string _refNo);
        
         // Added by Chathura on 20-oct-2017
         [OperationContract]
         DataTable Bank_Reconciliation_Summery_Rpt(DateTime _Asatdate, string _accountNo);

         // Added by Chathura on 20-oct-2017
         [OperationContract]
         DataTable RealizationStatusReport(string _reportType, string _accountNo, DateTime _fromDate, DateTime _toDate, string _profitCenter);
         // Added by Chathura on 24-oct-2017
         [OperationContract] 
         DataTable RemitanceControlReconReport(string _pc, string _com, DateTime _asatdate, int _isasat, DateTime _fromdate, DateTime _todate, string _user);
        //Add by lakshan 25Oct2017
        [OperationContract] 
         Int32 SaveReservationApprovalAll(INR_RES _inrRes, MasterAutoNumber mastAutoNo, out string msg, out string finaldoc);

        //Akila 2017/10/26
        [OperationContract]
        DataTable GetCustomerAllowInvoiceTypeNew(string _company, string _customer);

        //Akila 2017/10/26
        [OperationContract]
        DataTable GetCustomerInvoiceBalance(string _comCode, string _custCode, string _invType);
        //tharanga 2017/11/07
        [OperationContract]
        List<ReceiptItemDetails> ReceiptItemDetailsNew(string _code);


        // Added by Chathura on 09nov-2017
        [OperationContract]
        DataTable processMovementSubtypeReport(string compString, int hasMultiComp, string docType, string docSubType, DateTime fromDate, DateTime toDate, string userName);
    

        //Akila 2017/10/11
         [OperationContract]
         Int16 SaveCctTransLog(CctTransLog _transLog, ref string _error);

         [OperationContract]
         Int16 UpdateCctTransLog(CctTransLog _transLog, ref string _error);
        //By Akila   - 2017/11/13
        [OperationContract]
        DataTable GetExchangeRequestBySerail(string _ser, Int32 _isinv);
        //tharanga 2017/11/21
        [OperationContract]
        DataTable GetBrandManager(string _com);
          //tharanga 2017/11/21
        [OperationContract]
        List<SAR_PB_CIREFFECT> get_SAR_PB_CIREFFECT(string spc_circular, Int32 spc_act);
         //tharanga 2017/11/21
        [OperationContract]
        Int32 SAVE_SAR_PB_CIREFFECT(SAR_PB_CIREFFECT _SAR_PB_CIREFFECT);
          //tharanga 2017/11/22
        [OperationContract]
        Int32 Update_price_def_EST(List<PriceDetailRef> _PriceDetailRef);
        //Tharindu 2017-11-21
        //[OperationContract]
        //bool GetLoyalityExistsStatus(string _cardtype, string _cardref);
        //Nuwan 2017.11.10
        [OperationContract]
        Int32 getExpireNumberofDateConf(string location,string com,out string error);
        //Nuwan 2017.11.11
        [OperationContract]
        INR_RES GetReservationApprovalNew(string req_no);
        //Nuwan 2017.11.13
        [OperationContract]
        Int32 UpdateReservationApprovalNew(INR_RES inr_res, List<INR_RES_DET> inr_res_det, out string msg);
        [OperationContract]
        bool GetLoyalityExistsStatus(string _cardtype, string _cardref);
        //subodana 2017-12-11
        [OperationContract]
        Int32 SaveAddtionalCustomer(List<mst_busentity_add_cus> _addcus, ref string _error);
        //subodana 2017-12-11
        [OperationContract]
        List<mst_busentity_add_cus> GetAddtinalCusCodes(string mastercode);
        //subodana 2017-12-11
        [OperationContract]
        List<InvoiceHeader> GetPendingInvoicesweb(string _com, string _pc, string _cus, string _inv, string _status, string _fdate, string _tdate);
        //subodana 2017-12-11
        [OperationContract]
        decimal GetOutInvAmtweb(string _company, string _pc, string _cus, string _inv);
        //tharanga 2017/12/19
        [OperationContract]
        DataTable get_quo_to_inv(string _company, DateTime _fromDate, DateTime _toDate, string _doNo, string _sts, string _pc);
        //Add by lakshan 20Dec2017
        [OperationContract]
        List<InvoiceHeader> ChkInvoiceAvailableForSalesOredr(string _soNo);
        //tharanga 2017/01/08
        [OperationContract]
        Int32 SaveCustomerPriorityLevel_jonentry(List<MST_BUSPRIT_LVL> _custLevel, string _cusCode);

        //By Akila 2018/01/08
        [OperationContract]
        DataTable GetSalesDetailsWithDo(string _doNumber, string _refNumber, string _acc);
        //tharanga 2018/02/17
         [OperationContract]
        DataTable GetRever_balance(string _doc);

        //Akila 2018/02/23
         [OperationContract]
         DataTable GetInvoiceDetailsInSCM2(string _invNo, string _item, Int32 _lineNo);
         //tharanga 2018/03/02
         [OperationContract]
         DataTable GetInvoicecom_item_details(string _invNo, string _prom_cd, string _com);

         //Tharindu 2018-03-02
         [OperationContract]
         DataTable GetRequestdetails(string _basedoc);

        //Akila 2018/01/31
        [OperationContract]
        List<EventRegistry> GetEventDetById(string _eventId);

        //Akila 2018/01/31
        [OperationContract]
        List<EventItems> GetEventItemsByEventId(string _eventId);

        //Akila 2018/02/06
        [OperationContract]
        Int16 UpdateEventHdrStatus(EventRegistry _event);

        //Akila 2018/02/06
        [OperationContract]
        Int16 UpdateEventItemQty(EventItems _eventItem);

        //Akila 2018/02/14
        [OperationContract]
        Int16 SaveInvoicedEventItems(InvoicedEventItems _invItems);

        //Akila 2018/02/14
        [OperationContract]
        List<InvoicedEventItems> GetInvoiceEventItems(string _invNo);

        //Akila 2018/02/26
        [OperationContract]
        Int16 UpdateInvoiceEventItems(string _invNo, string _eventId);

        //Tharindu 2018-03-12
        [OperationContract]
        DataTable GetSCMInvoice2(string _company, string _customer, string _item);

          //Tharindu 2018-03-16
        [OperationContract]
        DataTable Getsupplierinvoicedetails(string _company, string _docNo);
        //tharanga 2018-04-05
        [OperationContract]
        Int32 updateToMasterPcWiseMid(List<Deposit_Bank_Pc_wise> lst_master_mid, out string _err);

        //Akila 2018/03/
        [OperationContract]
         List<InvoiceItem> GetGeneralDiscountForTotalInvoice(int _disSeq, string _company, List<InvoiceItem> invoiceItems, out bool _isDifferent, out decimal _tobepay, InvoiceHeader _invoiceheader, out string _errorMsg);
        //tharanga 2018-04-26
        [OperationContract]
        List<ReceiptItemDetails> GetAdvanReceiptdet_frominvoice(string _recNo);
        [OperationContract]
        //tharanga 2018-04-26
        List<ReptPickSerials> search_res_serials_for_do(string company, string location, string itemCode, string pbook, string pblvl, string itemstatus,string ser1);


        //Tharindu
        [OperationContract]
        DataTable getcustomermobno(string p_com, string p_mobile, string p_type);

        //Wimal 03/05/2018
        [OperationContract]
        List<InterCompanySalesParameter> GET_INTERCOM_PAR_BY_SUP_PO(string _com, string _sup);
        //tharanga 2018-05/17
        [OperationContract]
        Int32 cancel_MasterPcWiseMid(List<Deposit_Bank_Pc_wise> lst_master_mid, out string _err);
        //tharanga 2018-05/18
        [OperationContract]
        List<TradingInterest> get_trd_int_det(string _com, string _itm, DateTime _date, string _sch_cd);

        [OperationContract]
        DataTable GetSchemes_term(string _type, int _term,string _sch_cd);
        //tharanga 2018-05/18
        [OperationContract]
        DataTable GetPromoCodesByCirnew(string _cir, string _promo, string _pb, string pbl);
        
        [OperationContract]
        DataTable getTradingInterest(string _scheme, string _item);

        [OperationContract]
        Int16 DeleteTrialCalSum();

        [OperationContract]
        Int16 SaveTrialCalSum(string p_com_cd, string p_pc_cd, string p_pc_desc, string p_pc_chnl, string p_cat1, string p_cat2, string p_cat3,
            string p_item_cd, string p_item_desc, string p_brnd, string p_pbook, string p_pblvl, string p_scheme, string p_scheme_name,
            decimal p_gross_amt, decimal p_vat_amt, decimal p_d_net_amt, decimal p_d_cost, decimal p_d_gp, decimal p_init_vat, decimal p_inst_vat,
            decimal p_trd_intr_rt, decimal p_trd_intr_amt, decimal p_ser_charge, decimal p_intr_charge, decimal p_diriya, decimal
            p_dp_amt, decimal p_hp_amt, decimal p_init_ser_charge, decimal p_inst_ser_charge, decimal p_init_diriya, decimal p_inst_diriya, decimal
            p_coll_comm, decimal p_coll_comm_rt, decimal p_dp_comm, decimal p_dp_comm_rt, decimal p_diriya_comm, decimal p_diriya_comm_rt, decimal p_monthly_inst);

        //Dulaj 2018-Apr-10
        [OperationContract]
        DataTable GetUserDefineTemplate(string template, string user);
        //Dulaj 2018-Apr-11
        [OperationContract]
        Int32 SaveUserProfileTemplate(string _templateName, string _codes, string _values, string _userId, string _dectription, string _key, string sessionId);

        //Dulaj 2018-apr-12
        [OperationContract]
        Int32 CheckTemplateName(string _tempName);

        [OperationContract]
        //Pasindu 2018/05/06
        List<RBH_FRMDT_TODT> get_hdr_dates(string p_cir_code);

        [OperationContract]
        //Pasindu 2018/05/06
        DataTable GetProductBonusDet(string p_circular_code, string p_scehema_code, string p_fromdata, string p_todate);
        [OperationContract]
        //Dulaj 2018/Jun/29
        string GetAccountNo(string customerCode, string companyCode);


        //Tharindu 2018-06-14
        [OperationContract]
        DataTable GetInvoiceSerial_SCM2(string _company, string _invoice, string _item);
        //Dulaj 2018/Jul/19
        [OperationContract]
        DataTable GetProductBonusInvoiceDetails(string circular_code, string scehema_code, string FromDate, string ToDate,string com);
        //tharanga 2018-07/26
        [OperationContract]
        List<RequestApprovalDetail> GET_EXCHANGE_REC_DET(string _ref);
        //subodana
        [OperationContract]
        List<HpSchemeDefinition> GetDPCommission(string _book, string _level, DateTime _date, string _item, string _brand, string _maincat, string _cat, string _scheme);
        //subodana
        [OperationContract]
        decimal GetCashCommissionRate(string _book, string _level, DateTime _date, string _item, string _brand, string _maincat, string _cat, string type);
        //tharanga 2018-08/06
        [OperationContract]
        DataTable Get_hp_balance_bycust(DateTime _date, string _cust_cd, string _loc);

        [OperationContract]
        DataTable Get_warr_det_by_ser(string _itm, string _ser);
        //tharanga 2018-08/06
        [OperationContract]

        InvoiceLoyalty GetInvoiceLoyalty(string _invoice);
        //tharanga 2018-09/20
        [OperationContract]
        List<InvoiceHeader> GetInvoiceByAccountNo_HS(string _company, string _profitCenter, string _account);

        //tharanga 2018-09/20
        [OperationContract]
        Int32 update_loty_crdmember(string account_no, string type, Int32 _status, out string _errmsg);
        //tharanga 2018-10/15
        [OperationContract]
        LoyaltyMemeber getLoyaltyDetails_by_ser(string _cd_ser);
        //tharanga 2018-10/31
        [OperationContract]
        DataTable getAllMid_Detailsnew(string prof_cen);
   
        //Wimal 2018-Oct-01
         [OperationContract]
        List<string> GetAdjType();
   
         //Wimal 2018-Nov-01
         [OperationContract]
         DataTable CC_Reconciliation_Rpt(string com, DateTime _fromdate, DateTime _todate, string Acc);

         //Wimal 2018-Nov-02
         [OperationContract]
         DataTable LoadCCAccounts();
   
        //Darshana 2018-11-07
         [OperationContract]
         DataTable GetAlwItm(string _itm);
        //Darshana 2018-11-07
         [OperationContract]
         DataTable GetCurInvt();
        //Dulaj 2018/Nov/14
         [OperationContract]
         int UpdateCashConvertionDocNo(string oldDoc, string newDoc);
         [OperationContract]
         List<SER_HIS> get_ser_details(string serial, out string _err);

         //Wimal 2018/Nov/29
         [OperationContract]
         Int32 UpdateDipositBankDet(List<Deposit_Bank_Pc_wise> listBankDetails, out string _err);
        //Dulaj 2018/Dec/03

         [OperationContract]
         Int32 Delete_inv_type(string com,string customerCd, string invType);
        //Udesh 03-Nov-2018
         [OperationContract]
         DataTable Get_GV_Page_Availability(string _loyaltyType, string _pc, int _pageNo);

         //Wimal @ 16/Jan/2018 
         [OperationContract]
         DataTable loyaltyPointEarning(string _com, string _pc, string _itemCode, string _cat1, string _cat2, string _cat3, string _cat4, string _cat5,
            string _brand, string _model, string _customer, DateTime _fromDate, DateTime _toDate, string _rptType, string _userID);

         //Sadaruwan @ 2019-02-14
         [OperationContract]
         DataTable GetHP_CheckedAccounts(string com, string pc, DataTable _dt);

    }

    
    
    [DataContract]
    public enum ItemHierarchyElement
    {
        [EnumMember]
        DISCOUNT,
        [EnumMember]
        SERIAL,
        [EnumMember]
        PROMOTION,
        [EnumMember]
        ITEM,
        [EnumMember]
        BRAND,
        [EnumMember]
        MAIN_CATEGORY,
        [EnumMember]
        SUB_CATEGORY,
        [EnumMember]
        PRICE_BOOK,
        [EnumMember]
        PRICE_LEVEL
    }


}

