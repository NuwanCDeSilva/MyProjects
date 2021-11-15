using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FF.BusinessObjects;
using System.Data;
using FF.BusinessObjects.General;
using System.Net.Mail;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Commission;
using System.Threading.Tasks;


namespace FF.Interfaces
{
    [ServiceContract]
    public interface IGeneral
    {
        //kapila
        [OperationContract]
        List<MasterColor> GetItemColorByCode(string _code);
        //kapila 
        [OperationContract]
        Int32 SaveComItem(List<MasterCompanyItem> _lstcomItem);
        //kapila
        [OperationContract]
        DataTable GetExpenseLimitAloc(string _com, string _pc, string _expcd, DateTime _date);
        //kapila
        [OperationContract]
        Int32 SaveExpenseLimitAloc(List<Expense_Limit_Alloc> _serList, out string _err);
        //kapila
        [OperationContract]
        DataTable GetMstItmUOM(string _code);
        //kapila
        [OperationContract]
        Int32 Save_Item_Master(List<MasterItem> _item, List<MasterCompanyItem> _lstcomItem, out string _err);
        //kapila
        [OperationContract]
        MasterLocation GetAllLocationByLocCode(string _CompCode, string _LocCode, Int32 _isAll);
        //kapila
        [OperationContract]
        DataTable GetPBSchemeRem(string _code);

        [OperationContract]
        DataTable GetPBScheme(string _code);
        //kapila
        [OperationContract]
        Int32 SaveItemComTax(List<MasterItemTax> _lstItmComtax, out string _err);
        //kapila
        [OperationContract]
        DataTable GetTaxRateCode(string _type, string _code);
        //kapila
        [OperationContract]
        DataTable getreqSerByReqno(string _req_no, string _item,string _ser);
        //kapila
        [OperationContract]
        DataTable Get_gen_reqapp_ser_byitem(string com, string reqNo, string _item);
        //kapila
        [OperationContract]
        DataTable getPSITransLocs(DateTime _from, DateTime _to);
        //kapila
        [OperationContract]
        DataTable getMyAbansSMSContacts(Int32 _opt, string _lan, String _town);
        //kapila
        [OperationContract]
        Int32 UpdateMarkAsPrint(string _com, DateTime _fromDate, DateTime _toDate, Int32 _isAll, string _user);

        //kapila
        [OperationContract]
        DataTable getMyAbansBySerial(string _ser, Int32 _isNIC);

        //kapila
        [OperationContract]
        DataTable getFavouriteByCat(string _cat);

        //kapila
        [OperationContract]
        Int32 CancelTempWaraWaranty(List<ReptPickSerials> _serList);

        //kapila
        [OperationContract]
        Int32 sp_updateexchangeissuenew(List<RequestApprovalDetail> _list,Boolean isdutyfree=false);

        //kapila
        [OperationContract]
        Int32 SaveSubLocations(MasterSubLocation _subLoc, out string _err);

        //kapila
        [OperationContract]
        Int32 UpdateVoucherPara(List<PromotionVoucherPara> _lstVouPara);

        //kapila
        [OperationContract]
        DataTable getInvBySerial(string _com, string _loc, string _type, string _ser);

        //kapila
        [OperationContract]
        DataTable GetSCM2LocationByCompany(string _Comp);

        //kapila
        [OperationContract]
        List<VehicalRegistration> GetVehRegNoByInvoiceNo(string com, string pc, string invNo);

        //kapila
        [OperationContract]
        DataTable get_job_defects(string _jobNo, string _tp);

        //kapila
        [OperationContract]
        DataTable GetSerialSupplierGRN(String _com, String _item, String _serial, Int16 _key);

        //kapila
        [OperationContract]
        DataTable GetSerialSupplierCode(String _com, String _item, String _serial, Int16 _key);

        //kapila
        [OperationContract]
        Int16 SaveStockRequest(string p_MRQ_COM, DateTime p_MRQ_VALID_FRM, DateTime p_MRQ_VALID_TO, string p_MRQ_RES_TP, string p_MRQ_PTY_TP, string p_MRQ_PTY_CD, string p_MRQ_ITM_CD, string p_MRQ_BRD, string p_MRQ_CAT1, string p_MRQ_CAT2, string p_MRQ_CAT3, string p_MRQ_CAT4, string p_MRQ_CAT5, decimal p_MRQ_QTY, Int32 p_MRQ_STUS, string p_MRQ_CRE_BY, string p_MRQ_MOD_BY, decimal p_MRQ_DAYS, decimal p_MRQ_WSDAYS, decimal p_MRQ_SSDAYS);

        //kapila
        [OperationContract]
        DataTable GetStockRequest(string _restp, string _loc, string _chnl, string _com, DateTime _date, string _item, String _brand, string _cat1, string _cat2, string _cat3, string _cat4, string _cat5);

        //kapila
        [OperationContract]
        DataTable GetItemGIT(string _com, string _loc, string _item, string _brand, string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, Decimal _days);//, Decimal _wsdays, Decimal _ssdays);
        //Dilshan
        [OperationContract]
        DataTable GetItemGITWH(string _com, string _loc, string _item, string _brand, string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, Decimal _days, string _uloc);//, Decimal _wsdays, Decimal _ssdays);

        //kapila
        [OperationContract]
        DataTable sp_get_pc_prit_hierarchy(string _com, string _type);

        //kapila
        [OperationContract]
        DataTable ManualDocumentsReport(DateTime _fromDate, DateTime _toDate, Int32 _isasat, DateTime _asatdate, string _user, string _com, string _pc);

        //kapila
        [OperationContract]
        Int32 SaveBrandAlloc(string _mba_com, string _mba_pty_tp, string _mba_pty_cd, string _mba_emp_id, string _mba_brnd, string _mba_ca1, string _mba_ca2, string _mba_ca3, string _mba_ca4, string _mba_ca5, DateTime _mba_frm_dt, DateTime _mba_to_dt, Int32 _mba_act, string _mba_cre_by, string _mba_mod_by);

        //kapila
        [OperationContract]
        Int32 SaveSMSOut(OutSMS _smsout);

        //Added by Udesh - 26-Nov-2018
        [OperationContract]
        Task<int> SendPromotionSMS(string userId, string company, OutSMS _smsout, string sessionId);


        //kapila
        [OperationContract]
        Boolean IsVehRegNoExist(string _com, string _recno, string _regno);

        #region Master Companies

        [OperationContract]
        List<MasterCompany> GetALLMasterCompaniesData();

        [OperationContract]  //kapila 21/3/2012
        MasterCompany GetCompByCode(string _Code);



        // Nadeeka 25-05-2015
        [OperationContract]
        Int32 SaveItemBrand(List<MasterItemBrand> _lstbrand, List<MasterItemBrand> _lstbranddel, out string _err);

        // Nadeeka 25-05-2015
        [OperationContract]
        Int32 SaveItemModel(List<MasterItemModel> _lstmodel, List<MasterItemModel> _lstmodeldel, List<mst_model_replace>_lstRplModel, List<mst_commodel> _lstcommodel, List<BusinessEntityVal>_lstBusEntity, List<UnitConvert> _uomdata, out string _err);

        // Nadeeka 25-05-2015 copy by lakshan 20 APr 2017
        [OperationContract]
        Int32 SaveItemModelWeb(List<MasterItemModel> _lstmodel, List<MasterItemModel> _lstmodeldel, List<mst_model_replace> _lstRplModel, List<mst_commodel> _lstcommodel, List<BusinessEntityVal> _lstBusEntity, List<UnitConvert> _uomdata, out string _err);

        // Nadeeka 25-05-2015
        [OperationContract]
        Int32 SaveItemUOM(List<MasterUOM> _lstUOM, List<MasterUOM> _lstUOMdel, out string _err);
        // Nadeeka 25-05-2015
        [OperationContract]
        List<mst_itm_tax_structure_det> getItemTaxStructure(string _code);
        // Nadeeka 25-05-2015
        [OperationContract]
        List<mst_itm_tax_structure_hdr> GetItemTaxStructureHeader(string _code);

        // Nadeeka 25-05-2015
        [OperationContract]
        Int32 SaveItemColor(List<MasterColor> _lstUOM, List<MasterColor> _lstUOMdel, out string _err);
        // Nadeeka 25-05-2015
        [OperationContract]
        DataTable GetTaxCode();

        // Nadeeka 25-05-2015
        [OperationContract]
        Int32 SaveItemTaxStructure(mst_itm_tax_structure_hdr _tax, List<mst_itm_tax_structure_det> _taxDet, MasterAutoNumber _masterAuto, out string _err);

        // Nadeeka 25-05-2015
        [OperationContract]
        Int32 SaveItemCate1(List<REF_ITM_CATE1> _cate1, List<REF_ITM_CATE1> _cate1del, out string _err);
        // Nadeeka 25-05-2015
        [OperationContract]
        Int32 SaveItemCate2(List<MasterItemSubCate> _cate2, List<MasterItemSubCate> _cate2del, out string _err);
        // Nadeeka 25-05-2015
        [OperationContract]
        Int32 SaveItemCate3(List<REF_ITM_CATE3> _cate3, List<REF_ITM_CATE3> _cate3del, out string _err);
        // Nadeeka 25-05-2015
        [OperationContract]
        Int32 SaveItemCate4(List<REF_ITM_CATE4> _cate4, List<REF_ITM_CATE4> _cate4del, out string _err);
        // Nadeeka 25-05-2015
        [OperationContract]
        Int32 SaveItemCate5(List<REF_ITM_CATE5> _cate5, List<REF_ITM_CATE5> _cate5del, out string _err);

        // Nadeeka 27-08-2015
        [OperationContract]
        REF_ITM_CATE3 GetItemCategory3(string _cd);


        // Nadeeka 27-08-2015
        [OperationContract]
        REF_ITM_CATE4 GetItemCategory4(string _cd);

        // Nadeeka 27-08-2015
        [OperationContract]
        REF_ITM_CATE5 GetItemCategory5(string _cd);


        //Nadeeka 09/07/2015
        [OperationContract]
        MasterItem GetItemMaster(string _item);


        // Nadeeka 25-05-2015
        //Add user id 
        [OperationContract]

        Int32 SaveItemMaster(MasterItem _item, List<mst_itm_channlwara> _lstChnelWarra, List<mst_itm_pc_warr> _lstpcwara, List<MasterItemWarrantyPeriod> _lstitemWara, List<mst_itm_sevpd> _lstserPrd, List<MasterItemTaxClaim> _lstitemClaim, List<mst_itm_container> _lstcont, List<MasterItemComponent> _lstitemCompo, List<mst_itm_replace> _lstitemprep, List<mst_itm_mrn_com> _lstmrn, List<mst_itm_redeem_com> _lstredCom, List<BusEntityItem> _lstspCom, List<BusEntityItem> _lstcutItem, List<MasterCompanyItem> _lstcomItem, List<mst_itm_com_reorder> _lstreorder, List<mst_itm_fg_cost> _lstfg, List<ItemPrefix> _Prefix, Boolean _autoCode, string _com, out string _err);

        // Nadeeka 25-05-2015
        [OperationContract]
        List<MasterItemBrand> GetItemBrand();
        // Nadeeka 25-05-2015
        [OperationContract]
        List<MasterItemModel> GetItemModel(string _code = null);
        // Nadeeka 25-05-2015
        [OperationContract]
        List<MasterUOM> GetItemUOM();
        // Nadeeka 25-05-2015
        [OperationContract]
        List<MasterColor> GetItemColor();
        // Nadeeka 25-05-2015
        [OperationContract]
        DataTable GetItemTpAll();
        // Nadeeka 25-05-2015
        [OperationContract]
        DataTable GetContainerType();
        // Nadeeka 25-05-2015
        [OperationContract]
        DataTable GetWarrantyPeriod();

        // Nadeeka10-07-2015
        [OperationContract]
        List<mst_itm_fg_cost> GetFinishGood(string _item);
        // Nadeeka10-07-2015
        [OperationContract]
        List<mst_itm_com_reorder> GetReOrder(string _item);
        // Nadeeka10-07-2015
        [OperationContract]
        List<mst_itm_pc_warr> getPcWarrant(string _item);
        // Nadeeka10-07-2015
        [OperationContract]
        List<MasterCompanyItem> GetComItem(string _item);
        // Nadeeka10-07-2015
        [OperationContract]
        List<BusEntityItem> GetBuninessEntityItem(string _item, string _type);
        // Nadeeka10-07-2015
        // [OperationContract]
        //  List<mst_sup_itm> GetSupplierItem(string _item);
        // Nadeeka10-07-2015
        [OperationContract]
        List<mst_itm_redeem_com> GetRedeem(string _item);
        // Nadeeka10-07-2015
        [OperationContract]
        List<mst_itm_mrn_com> getItemMRN(string _item);
        // Nadeeka10-07-2015
        [OperationContract]
        List<mst_itm_replace> getReplaceItem(string _item);
        // Nadeeka10-07-2015
        [OperationContract]
        List<mst_itm_sevpd> getServiceSchedule(string _item);
        // Nadeeka10-07-2015
        [OperationContract]
        List<mst_itm_container> getRContainerItem(string _item);
        // Nadeeka10-07-2015
        [OperationContract]
        List<mst_itm_channlwara> getChannelWarranty(string _item);
        // Nadeeka10-07-2015
        [OperationContract]
        List<MasterItemComponent> getitemComponent(string _item);
        // Nadeeka10-07-2015
        [OperationContract]
        List<MasterItemTaxClaim> getitemTaxClaim(string _item);
        // Nadeeka10-07-2015
        [OperationContract]
        List<MasterItemWarrantyPeriod> getitemWarranty(string _item);
        // Nadeeka 25-05-2015
        [OperationContract]
        List<REF_ITM_CATE1> GetItemCate1();

        //Nadeeka
        [OperationContract]
        Int32 GetCutomerValidationCode(string _mob);
        //Nadeeka
        [OperationContract]
        Int32 UpdateCutomerMobile(string _mobile, string _type, out string _err);

        // Nadeeka 25-05-2015
        [OperationContract]
        List<MasterItemSubCate> GetItemCate2(string _cate1);
        // Nadeeka 25-05-2015
        [OperationContract]
        List<REF_ITM_CATE3> GetItemCate3(string _cate1, string _cate2);
        // Nadeeka 25-05-2015
        [OperationContract]
        List<REF_ITM_CATE4> GetItemCate4(string _cate1, string _cate2, string _cate3);
        // Nadeeka 25-05-2015
        [OperationContract]
        List<REF_ITM_CATE5> GetItemCate5(string _cate1, string _cate2, string _cate3, string _cate4);



        //written by Nadeeka 2012-12-28
        [OperationContract]
        DataTable GetCompanyByCode(string _CompCode);
        //written by shalika 2014/05/06 WebERP Portal
        [OperationContract]
        DataTable GetAuth_Transactions(string _Trn_Type, string _user_id, string com);

        [OperationContract]
        DataTable GetHireSales_Transaction(string _sart_desc, string _user_id, string com);

        [OperationContract]
        DataTable GetDept_Comment();
        [OperationContract]
        DataTable GetRequestDetails_For_history(string _sart_desc, DateTime FromDate, DateTime ToDate, decimal Level, string status, string Location);
        #endregion

        #region Master locations
        [OperationContract]
        DataTable GetLocationSubChannel(string _com, string _loc);

        [OperationContract]
        List<MasterLocation> GetAllLocationData();

        [OperationContract]
        List<MasterLocation> GetLocationByCompany(string _Comp);

        //kapila 21/3/2012
        [OperationContract]
        MasterLocation GetLocationByLocCode(string _CompCode, string _LocCode);

        //Chamal 30/05/2012 use ConverterTotal
        [OperationContract]
        MasterLocation GetLocationInfor(string _CompCode, string _LocCode);

        //lakshan 10-12-2015
        [OperationContract]

        DataTable GetLocationByComs(string _CompCode);
        #endregion

        #region Master User Category
        [OperationContract]
        List<MasterUserCategory> GetUserCategory();

        //kapila 26/3/2012
        [OperationContract]
        MasterUserCategory GetUserCatByCode(string _catCode);

        #endregion

        #region Master Department
        [OperationContract]
        List<MasterDepartment> GetDepartment();

        //kapila 26/3/2012
        [OperationContract]
        MasterDepartment GetDeptByCode(string _deptCode);
        #endregion

        #region Master Types
        [OperationContract]
        List<MasterType> GetAllType(string _Cate);

        [OperationContract]
        List<MasterType> GetOutwardTypes();
        #endregion

        #region Master Sub Types
        /// <summary>
        /// Created By : Miginda Geeganage On 26-03-2012
        /// </summary>
        [OperationContract]
        List<MasterSubType> GetAllSubTypes(string _mainCode);

        #endregion

        #region Check back date *** Chamal De Silva 29/06/2012
        /// <summary>
        /// Created By : Chamal De Silva On 29-06-2012
        /// </summary>
        [OperationContract]
        bool IsAllowBackDate(string _com, string _loc, string _pc, string _backdate);

        /// <summary>
        /// Created By : Chamal De Silva On 10-08-2012
        /// </summary>
        [OperationContract]
        bool IsAllowBackDateForModule(string _com, string _loc, string _pc, string _module, string _backdate, out BackDates _backdateobj);

        #endregion

        #region Master Currency

        //Written By Prabhath on 21/05/2012
        [OperationContract]
        List<MasterCurrency> GetAllCurrency(string _currencyCode);

        #endregion

        #region Request Approval
        //Code By D.Samarathunga 02/06/2012
        [OperationContract]
        List<RequestApprovalHeader> GetAllPendingRequest(RequestApprovalHeader _paramApproval);

        [OperationContract]
        int UpdateApprovalStatus(RequestApprovalHeader _paramUpdateApprovalstatus);

        [OperationContract]
        List<RequestApprovalDetail> GetRequestApprovalDetails(RequestApprovalDetail _paramRequestApprovalDetails);

        //Code Shani 18/07/2012
        [OperationContract]
        DataTable GetApprovedRequestDetails(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel);
        //Code Prabhath 18/07/2012
        [OperationContract]
        List<RequestApprovalHeader> GetApprovedRequestDetailsList(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel);

        //Code Shani 19/07/2012
        [OperationContract]
        DataTable GetPendingRequestDetails(string com, string pc, string fucCD, string appType, Int32 isInApprovedStatus, Int32 userApprovalLevel);

        //Code Prabhath 18/07/2012
        [OperationContract]
        Int32 SaveHirePurchasRequest(RequestApprovalHeader _hdr, List<RequestApprovalDetail> _det, RequestApprovalHeaderLog _hdrlog, List<RequestApprovalDetailLog> _delLog, int UserPermissionLevel, bool _isFinalApprovalUser, bool _isRequestGenerateUser, out string _userSeqNo);

        //Code Prabhath 13/06/2013
        [OperationContract]
        Int32 SaveHirePurchasRequestForRevertRelease(RequestApprovalHeader _hdr, List<RequestApprovalDetail> _det, RequestApprovalHeaderLog _hdrlog, List<RequestApprovalDetailLog> _delLog, int UserPermissionLevel, bool _isFinalApprovalUser, bool _isRequestGenerateUser, out string _userSeqNo, MasterAutoNumber _auto);

        //Chamal 02/08/2012
        [OperationContract]
        List<RequestApprovalDetailLog> GetRequestApprovalDetailLog(string seqNo);

        //Shani 02/08/2012
        [OperationContract]
        DataTable GetRequestApprovalDetailFromLog(string seqNo);
        #endregion

        //kapila 2/7/2012
        #region master outside party
        [OperationContract]
        int SaveOutsideParty(MasterOutsideParty _outsideParty, MasterAutoNumber _masterAutoNumber, out string _CompCode);

        //kapila
        [OperationContract]
        Int32 SaveESDTransaction(List<ESDTxn> _esdtxnList);

        //kapila
        [OperationContract]
        Int32 SaveESDBalance(ESDBal _esdBal);

        //kapila
        [OperationContract]
        DataTable GetESDTransactions(string _com, string _pc, string _epf, DateTime _month);

        //kapila
        [OperationContract]
        DataTable GetESDBalance(string _com, string _pc, string _epf, DateTime _month);

        //kapila
        [OperationContract]
        Boolean checkESDStatus(string _com, string _pc, string _epf, DateTime _month);

        //kapila
        [OperationContract]
        int GetESDPrvBalance(string _com, string _pc, string _epf, DateTime _month, DateTime _prvmonth, out Decimal _prvESDBal, out Decimal _prvIntBal, out Decimal _ESDContr, out Decimal _ESDAdj, out Decimal _IntAdj);

        //kapila
        [OperationContract]
        int updateESDStatus(string _com, string _pc, string _epf, DateTime _month);

        //kapila
        [OperationContract]
        List<VehicleInsuarance> GetInsuranceByRef(string _recNo);

        [OperationContract]
        MasterOutsideParty GetOutsideParty(string _code);
        #endregion

        //kapila
        [OperationContract]
        DataTable GetBusDeptByCode(string _code);

        //kapila
        [OperationContract]
        DataTable getSubChannelDet(string _company, string _code);
        //kapila
        [OperationContract]
        DataTable GetPayCircByCode(string _com, string _circ);

        //kapila
        [OperationContract]
        DataTable GetDistrictByCode(string _code);
        //kapila
        [OperationContract]
        DataTable GetProvinceByCode(string _code);
        //kapila
        [OperationContract]
        DataTable GetTownByCode(string _code);

        //kapila
        [OperationContract]
        DataTable GetBusDesigByCode(string _code);
        //kapila
        //Udesh Add string _company parameter - 08-Oct-2018
        [OperationContract]
        DataTable GetReprintDocs(string _docType, string _loc, string _pc, string _company, DateTime _fromDate, DateTime _toDate);

        //kapila 26/5/2014
        [OperationContract]
        int update_area(MasterArea _area, MasterAutoNumber _masterAutoNumber, out string _code);
        [OperationContract]
        int update_zone(MasterZone _zone, MasterAutoNumber _masterAutoNumber, out string _code);
        [OperationContract]
        int update_region(MasterRegion _region, MasterAutoNumber _masterAutoNumber, out string _code);

        //kapila
        [OperationContract]
        Int32 Update_ESD_SR_Details(string _com, string _pc, string _epf, DateTime _join, DateTime _ho, DateTime _AD, string _sunacc, string _user);

        [OperationContract]
        DataTable Get_All_PC(string _com);

        //kapila
        [OperationContract]
        DataTable Get_Dept_Cont_Dt(string _com, string _chnl, string _cd, Int32 _tp);

        //kapila
        [OperationContract]
        int SaveReprintDocRequest(Reprint_Docs _reprintDoc);

        //kapila
        [OperationContract]
        Int32 Update_SAR_TXN_PAY_TP(string _Stp_circ, DateTime _Stp_to_dt);

        //kapila
        [OperationContract]
        DataTable getDetail_on_serial2(string _company, string _location, string _serial1);

        //kapila
        [OperationContract]
        DataTable GetRequestedReprintDocs(string _loc, string _status, DateTime _fromDate, DateTime _toDate);

        [OperationContract]
        int UpdatePrintStatus(string _docno);

        //kapila
        [OperationContract]
        Boolean checkESDBalFound(string _com, string _pc, string _epf);

        [OperationContract]
        Boolean IsReprintDocFound(string _docno);

        //kapila
        [OperationContract]
        DataTable GetReqAppStatusLog(string _ref);

        //kapila
        [OperationContract]
        DataTable GetReqAppStatusLogInItems(string _ref, Int32 _level);

        //kapila
        [OperationContract]
        DataTable GetReqAppStatusLogOutItems(string _ref, Int32 _level);

        //kapila
        [OperationContract]
        Boolean IsDayEndDone4ScanDocs(string _com, string _pc, DateTime _date);

        [OperationContract]
        int UpdateReprintApproval(string _docno, string _status, string _statusChg);

        //kapila
        [OperationContract]
        Int32 UpdateCashComEndDate(string _comm, DateTime _toDate, string _user);

        //Prabhath on 02/08/2012
        [OperationContract]
        RequestApprovalHeader GetRequestApprovalHeaderByRequestNo(string _company, string _location, string _request);

        //sachith 2012/08/03
        [OperationContract]
        DataTable GetVehicalRegistrationBrand(string _mainCt, string _SubCt, string _Range, string _code, string _brand, string _type);

        //sachith 2012/07/03
        [OperationContract]
        DataTable GetPartyTypes();

        //sachith 2012/07/04
        [OperationContract]
        Int32 SaveVehicalRegistrationDefinition(List<MasterProfitCenter> pcs, List<string> items, DateTime from, DateTime to, string satlesType, decimal regVal, decimal calVal, string cre, int isMan);

        //sachith 2012/07/04
        [OperationContract]
        DataTable GetSalesTypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //sachith 2012/07/04
        [OperationContract]
        DataTable GetPartyCodes(string _com, string _loc);

        //sachith 2012/07/06
        [OperationContract]
        DataTable GetVehicalreciept(string _com, string _loc, string _type);

        //sachith
        [OperationContract]
        List<VehicalRegistration> GetVehicalRegistrations(string _regNo);

        //sachith
        [OperationContract]
        Int32 SaveVehicalRegistration(VehicalRegistration vehical, out string _err);

        //sachith
        [OperationContract]
        int UpdateInsuranceFromReg(string _regNo, string _regBy, DateTime _regDate, string _ref);


        //shanuka 10/05/2014
        [OperationContract]
        List<PendingApproval> GetPendingApproveDetails(string _userid, string _com);

        [OperationContract]
        List<PendingApproval> GetPendingReqTypes(string _user_id, string _srtmaintp, string _com);

        //kapila 
        [OperationContract]
        MasterProfitCenter GetPCByPCCode(string _CompCode, string _PCCode);

        //sachith
        [OperationContract]
        DataTable GetInsuranceCompanies();

        //sachith
        [OperationContract]
        DataTable GetInsurancePolicies();

        //sachith
        [OperationContract]
        Int32 SaveVehicalInsuranceDefinition(List<MasterProfitCenter> pcs, List<string> items, DateTime from, DateTime to, string satlesType, decimal regVal, string cre, string insCom, string insPol, int term, bool isReq);

        //sachith
        [OperationContract]
        int SaveInsurancePolicy(string _desc, string _cre);

        //sachith
        [OperationContract]
        int SaveInsuranceCompany(MasterOutsideParty _outPar);

        //sachith
        [OperationContract]
        DataTable GetInsuranceInvoice(string _com, string _pc, DateTime _from, DateTime _to, string _type, string _veh, string _acc, string _invNo, string _enginNo, string _chassisNo);

        //sachith
        [OperationContract]
        List<VehicleInsuarance> GetInsuranceDetails(string _type, string _recNo);

        //darshana
        [OperationContract]
        List<VehicleInsuarance> GetVehicalInsuarance(string _recNo, string _debNo);


        //kapila
        [OperationContract]
        int GetWeek(DateTime _Date, out Decimal _week, string _com);

        //sachith
        [OperationContract]
        Int32 SaveVehicalInsurance(VehicleInsuarance insurance, MasterAutoNumber _receiptAuto);

        //sachith
        [OperationContract]
        Int32 SaveInsurancePay(VehicalInsurancePay _insPay);

        ////sachith
        [OperationContract]
        Int32 SaveInsuranceClaim(FF.BusinessObjects.VehicalInsuranceClaim _insClaim);

        //sachith
        [OperationContract]
        List<VehicleInsuarance> GetClaimCusDetails(string _regNo);

        //Written by Prabhath on 24/08/2012
        [OperationContract]
        List<MsgInformation> GetMsgInformation(string _company, string _location, string _documenttype);

        //sachith
        [OperationContract]
        List<VehicalInsuranceClaim> GetClaimDetails(string type, string _regNo, DateTime date);

        //sachith
        [OperationContract]
        Int32 UpdateCoverNote(string _invNo, string _itmCd, string _com);

        #region Vehicle Approval

        //Written by Shani on 27/08/2012
        [OperationContract]
        List<VehicalRegistration> GetVehiclesByInvoiceNo(string com, string pc, string invNo);

        //Written by Shani on 27/08/2012
        [OperationContract]
        List<ReptPickSerials> getserialByInvoiceNum(string docNo, string com);

        //Written by Shani on 28/08/2012
        [OperationContract]
        Int32 Update_VehicleApproveStatus(string p_com, Int32 p_usrseq_no, string p_engineNo, string p_chasseNo, Int32 isApprove);


        //Written by Shani on 28/08/2012
        [OperationContract]
        Int32 Update_ListVehicleApproveStatus(string p_com, List<ReptPickSerials> approvedList, DateTime approvedDate, string approvedBy, string inoviceNo);

        //Written by Shani on 29/08/2012
        [OperationContract]
        DataTable SearchInvoiceNo_forVehicle(string com, string pc, string AccountNo, string _regNo, string InvoiceFromDate, string InvoiceToDate);

        //Written by Shani on 29/08/2012
        [OperationContract]
        DataTable Get_RequiredDocuments(string schemeCD);
        #endregion

        //Written by Shani on 29/08/2012
        [OperationContract]
        DataTable Get_DetBy_town(string town);


        //sachith 2012/09/04
        [OperationContract]
        Int32 SaveBackDate(BackDates _backdate, int _Dayend, Day_End_Log _log, DayEnd _dayend, bool noLoc, bool isPc, bool backdateModule, out string _err);

        //sachith 2012/09/05
        [OperationContract]
        Int32 CheckBackDate(string _com, string _ope);

        //ssachith 2012/09/05
        [OperationContract]
        Int32 SaveDayEndLog(Day_End_Log _log);

        //ssachith 2012/09/06
        [OperationContract]
        string GetCoverNoteNo(MasterAutoNumber _receiptAuto, string _type);

        [OperationContract]
        DataTable GetVehicalSearch(string _com, string _loc, string _type, string _reg, string _chassis, string _engine, string _inv, string _rec, string _acc, DateTime _fromdate, DateTime _todate, string _cust);

        //sachith 2012/09/13
        [OperationContract]
        DataTable GetRegAndInsSearch(string _item, string _model, string _sales, string _term, string _pc, string _com);

        //sachith 2012/09/28
        [OperationContract]
        DataTable GetHpSch(string _inv);

        //sachith 2012/11/08
        [OperationContract]
        Int32 UpdateVehReg(VehicalRegistration _vehReg);

        //Shani 2012/11/26
        [OperationContract]
        DataTable Get_All_User_paramTypes(string type);


        #region PC Definition
        //Shani 2012/11/28
        [OperationContract]
        Int32 Save_profit_center(MasterProfitCenter _pcheader, string _chnl, string _schnl, string _area, string _reg, string _zone, string _user, out string _err);

        //Shani 2012/11/29
        [OperationContract]
        Int32 Update_profit_center(MasterProfitCenter _pcheader);

        //Shani 2012/12/04
        [OperationContract]
        Int32 Save_MST_PC_INFO(List<MasterSalesPriorityHierarchy> _pcInfoHeaders, Dictionary<string, string> code_and_value);

        //Shani 2012/12/04
        [OperationContract]
        List<MasterSalesPriorityHierarchy> Get_AllPc_info(string com, string pc, string code, string type);

        //Shani 2012/12/05
        [OperationContract]
        Int32 Update_MST_PC_INFO(MasterSalesPriorityHierarchy pc_info, Dictionary<string, string> code_and_value);

        //Shani 2012/12/05
        [OperationContract]
        Int32 Save_MST_REC_DIV(List<MasterReceiptDivision> recHeaderList, List<string> pcList);

        //Shani 2012/12/05
        [OperationContract]
        Int32 Save_MST_PC_CHG(List<MasterProfitCenterCharges> chargePcList);

        //Shani 2012/12/06
        [OperationContract]
        Int32 Save_SAR_TXN_PAY_TP(List<PaymentType> txnPTList, List<string> pc_list, Boolean _isNew);

        //Shani 2012/12/13
        [OperationContract]
        List<MasterProfitCenterCharges> Get_latest_PcCharges(string com, string pc);
        #endregion PC Definition

        //sachith 2012/12/21
        [OperationContract]
        List<RequestApprovalDetail> GetRequestByRef(string _com, string _pc, string _ref);
        //sachith 2013/02/01
        [OperationContract]
        int GetReprintDocCount(string docNo);

        [OperationContract]
        int GetPrintDocCopies(string _com, string _docName);

        //Shani 2013/02/23
        [OperationContract]
        Boolean IsUser_InRole(string userCompany, string userId, string roleId);

        //kapila 1/3/2013
        [OperationContract]
        Boolean IsBackDateFound(string _pc, DateTime _date, string _module);

        //Shani 2013/03/08
        [OperationContract]
        DataTable Get_backdates(string com, string pc_or_loc_Code, string p_pc_or_loc, out List<BackDates> backDatesList);

        //darshana 12-03-2013
        [OperationContract]
        RequestApprovalHeader GetRelatedRequestByRef(string _com, string _pc, string _ref, string _appTP);

        //Shani 2013/03/13
        [OperationContract]
        Int32 Save_RequestApprove_Ser_and_log(List<RequestApprovalSerials> ReqApp_ser, Int32 approveUser_level);

        //Shani 2013/03/13
        [OperationContract]
        Int32 Save_RequestApprove_forReceiptReverse(List<RequestApprovalSerials> ReqApp_ser, MasterAutoNumber reqAuto, RequestApprovalHeader _hdr, List<RequestApprovalDetail> _det, RequestApprovalHeaderLog _hdrlog, List<RequestApprovalDetailLog> _delLog, int UserPermissionLevel, bool _isApprovalUser, bool _isRequestGenerateUser, out string _userReqNo, out string newRequstsStatus);

        //Shani 2013/03/13
        [OperationContract]
        DataTable Get_gen_reqapp_ser(string com, string reqNo, out List<RequestApprovalSerials> List);

        //kapila 18/3/2013
        [OperationContract]
        DataTable GetWeekDefinition(Int32 _month, Int32 _year, Int32 _week, out DateTime _from, out DateTime _to, string _com, string _tp);

        //Chamal 19-03-2013
        [OperationContract]
        DataTable Get_PC_Hierarchy(string _company, string _profitCenter);

        //Shani 2013/03/20
        [OperationContract]
        Int32 SaveHirePurchasRequest_NEW(MasterAutoNumber reqAuto, RequestApprovalHeader _hdr, List<RequestApprovalDetail> _det, RequestApprovalHeaderLog _hdrlog, List<RequestApprovalDetailLog> _delLog, int UserPermissionLevel, bool _isApprovalUser, bool _isRequestGenerateUser, out string _userSeqNo, out string requestStatus);

        //Shani 2013/04/02
        [OperationContract]
        Int32 Save_approve_ser_and_Log(List<RequestApprovalSerials> ReqApp_ser, bool _isNewRequest, string _userReqNo, int UserPermissionLevel, bool isFinalApprovalLevel);

        //Written By Prabhath on 09/04/2013
        [OperationContract]
        Int32 save_mst_loc_info(List<MasterLocationPriorityHierarchy> _locInfoHeaders, Dictionary<string, string> code_and_value);

        //Written By Prabhath on 10/04/2013
        [OperationContract]
        DataTable GetAllReceiptDivision(string _company, string _profitcenter);

        //kapila 11/4/2013
        [OperationContract]
        Int32 Delete_Doc_Check_List(string _com, string _pc, DateTime monthYear, Int32 week);

        //kapila 11/4/2013
        [OperationContract]
        Int32 Save_Doc_Check_List(string createBy, DateTime createDt, string com, string pc, Int32 month, Int32 year, Int32 week, DateTime monthYear);

        //kapila 11/4/2013
        [OperationContract]
        DataTable Get_Doc_Check_List(string com, string pc, DateTime monthYear, Int32 week);

        //kapila 12/4/2013
        [OperationContract]
        Int32 Update_Doc_Check_List(List<DocCheckList> _docLst);

        //kapila 12/4/2013
        [OperationContract]
        DataTable Get_Party_Types(string _cd);

        //kapila 12/4/2013
        [OperationContract]
        DataTable Get_Partycodes(string _pType, string _cd);

        //Written By Prabhath on 12/04/2013
        [OperationContract]
        DataTable GetUnReadMessages(string _receiver, string _company, string _location, string _profitcenter);

        //Written By Prabhath on 17/04/2013
        [OperationContract]
        DataTable GetOperationAdminTeam(string _company);

        //Written By Prabhath on 17/04/2013
        [OperationContract]
        DataTable GetLocationType();

        //Written By Prabhath on 17/04/2013
        [OperationContract]
        DataTable GetLocationGrade(string _company, string _loctype);

        //Written By Prabhath on 17/04/2013
        [OperationContract]
        DataTable GetLocationCategory1();

        //Written By Prabhath on 17/04/2013
        [OperationContract]
        DataTable GetLocationCategory2(string _company);

        //Written By Prabhath on 17/04/2013
        [OperationContract]
        DataTable GetLocationCategory3();

        //Written By Prabhath on 17/04/2013
        [OperationContract]
        DataTable GetCountry();

        //Written By Prabhath on 17/04/2013
        [OperationContract]
        DataTable GetProvince();

        //Written By Prabhath on 17/04/2013
        [OperationContract]
        DataTable GetDistrict(string _province);

        //Written By Prabhath on 17/04/2013
        [OperationContract]
        DataTable GetTown(string _province, string _district);

        //Written By Prabhath on 17/04/2013
        [OperationContract]
        DataTable GetOutletDepartment(string _company);

        //Written By Prabhath on 17/04/2013
        [OperationContract]
        DataTable GetEmployeeCategory();

        //Shani 2013/04/02
        [OperationContract]
        List<RequestApprovalHeader> GetAllRequests(string _com, string _pc, string _type, string _status);

        //kapila
        [OperationContract]
        Int32 Confirm_Check_List(string _com, string _pc, DateTime _month, Int32 _week, Int32 _SR, Int32 _RT, Int32 _SE, DateTime _cour_date, string _POD, string _user, DateTime _ho_dt);

        [OperationContract]
        Boolean IsConfirmCheckList(string _com, string _pc, DateTime _month, Int32 _week, string _type);

        //Shani 2013/04/23
        [OperationContract]
        DataTable Get_GET_GPC(string gpc, string desc);

        //Shani 2013/04/24
        [OperationContract]
        DataTable Get_hpr_insu(string sch_cd, string pty_tp, string pty_cd);

        //Written By Prabhath on 2/5/2013
        [OperationContract]
        DataTable GetArea(string _comapny, string _area);

        //Written By Prabhath on 2/5/2013
        [OperationContract]
        DataTable GetProfitCenterCharge(string _company, string _profitcenter);

        //Written By Prabhath on 14/5/2013
        [OperationContract]
        DataTable GetMainCategoryDetail(string _mainCat);

        //Written By Udesh on 22-Oct-2018
        [OperationContract]
        DataTable GetModelDetail(string _modelCode);

        //kapila 
        [OperationContract]
        DataTable GetCrdSaleDocsSavedData(Int32 _seq);

        //kapila 23/5/2013
        [OperationContract]
        Int32 SaveSignOnOff(Sign_On _signon, out Int32 _seq);

        //Written by Prabhath on 11/06/2013
        [OperationContract]
        bool IsSaleFigureRoundUp(string _company);

        //kapila 
        [OperationContract]
        DataTable GetCrdSaleDocsData(string _saleTp);

        //kapila
        [OperationContract]
        Boolean checkCredSaleDocs(string _eng, out Int32 _seq_no);

        [OperationContract]
        RequestApprovalHeader GetRequest_HeaderByRef(string _com, string _pc, string _ref);

        //kapila
        [OperationContract]
        Int32 SaveCredSaleDocHeader(CreditSaleDocsHeader _credSaleDocHdr, out Int32 _seqno);

        //kapila
        [OperationContract]
        Int32 SaveCredSaleDocDetail(List<CreditSaleDocsDetail> _credSaleDocdet);

        //kapila
        [OperationContract]
        Int32 UpdateCredSaleDocAllIssue(DateTime _gdh_iss_dt, Int32 _gdh_seq);

        //Shani 02-06-2013
        [OperationContract]
        DataTable Get_ITEMS_ONSelect(string _mainCt, string _SubCt, string _Range, string _code, string _brand, string _type);

        //Shani 03-06-2013
        [OperationContract]
        DataTable GetAC_SevCharge_itmes(string _company, string itemCode);

        //kapila
        [OperationContract]
        DataTable GetIncentiveSaleTypes();

        [OperationContract]
        DataTable GetIncentiveSTypeByCode(string _code);

        //Shani 03-06-2013
        [OperationContract]
        Int32 Save_MST_ITM_BLOCK(List<MasterItemBlock> itmBlockList, Int32 approveUser_level);

        //Shani 16-07-2013
        [OperationContract]
        DataTable Get_sar_price_type(string _tpCode);

        //kapila
        [OperationContract]
        int SaveIncentiveSchPC(List<IncentiveSchInc> _detSchInc, List<IncentiveSchIncDet> _detSchIncDet, List<IncentiveSch> _detSch, List<IncentiveSchPC> _detail, List<IncentiveSchDet> _incDet, List<IncentiveSchStkTp> _detStkTp, List<IncentiveSchPersn> _detPerson, List<IncentiveSchSerial> _detSerial, List<IncentiveSchPB> _detPB, List<IncentiveSchMode> _detMode);

        [OperationContract]
        List<MasterItemStatus> GetAllStockTypes(string _comp);

        //Shani 19-07-2013
        [OperationContract]
        Int32 SaveVehicalRegistrationDefinition_NEW(List<VehicalRegistrationDefnition> VehRegDef_List);

        //kapila
        [OperationContract]
        List<IncentiveSch> GetIncentiveSchemes(string _circ);
        [OperationContract]
        List<IncentiveSchDet> GetIncSchDet(string _ref);

        //Shani 23-07-2013
        [OperationContract]
        List<VehicalRegistrationDefnition> Get_vehRegDefnByCircular(string _circular);

        //Shani 23-07-2013
        [OperationContract]
        Int32 Update_Veh_reg_defn(string _circular, DateTime fromNew, DateTime ToNew);

        //Written by Prabhath on 24/07/2013
        [OperationContract]
        DataTable GetReceiptDivision(string _company, List<string> _profitcenter);

        //Written by Shani on 26/07/2013
        [OperationContract]
        int Save_AccountAgreementDetails(string AccountNo, AccountAgreement _agreement, List<AccountAgreementDoc> _docList);

        //Written by Shani on 26/07/2013
        [OperationContract]
        Decimal GetNotReciveCount(string accNo);

        //Written by Shani on 26/07/2013
        [OperationContract]
        DataTable Get_AccountAgreementHdr(string accountNo);

        // Nadeeka 11-05-2015
        [OperationContract]
        DataTable Get_PromotorType();

        // Nadeeka 11-05-2015
        [OperationContract]
        List<MST_PROMOTOR> Get_PROMOTOR(string _CODE, string _MOB, string _NIC);

        //Written by Shani on 26/07/2013
        [OperationContract]
        List<AccountAgreementDoc> Get_HPT_AccountAgreementDocs(string accountNo, string isRecieved);

        //Written by Shani on 27/07/2013
        [OperationContract]
        int Save_AccountAgreement_MisMatch_Details(string AccountNo, AccountAgreementContact AgrContact, AccountAgreementScheme AgrSchm);

        //Written by Shani on 27/07/2013
        [OperationContract]
        int Save_AccountAgreementProduct(AccountAgreementProduct _agrProduct);

        //Written by Shani on 27/07/2013
        [OperationContract]
        Int32 Delete_AccountAgreementProduct(string _accountNo, string itemCode);

        //Written by Shani on 27/07/2013
        [OperationContract]
        AccountAgreementContact Get_Accounts_Agreement_Contact(string _accountNo);
        //kapila
        [OperationContract]
        AccountAgreementScheme Get_Accounts_Agreement_Scheme(string _accountNo);

        //Written by Shani on 31/07/2013
        [OperationContract]
        DataTable Get_Account_Agreement_StartDt(string pc);

        //Written by Shani on 31/07/2013
        [OperationContract]
        int Save_accountAgreement_startdate(AccountAgreementStartDate _startDt);

        //Written by Shani on 31/07/2013
        [OperationContract]
        List<AccountAgreementProduct> Get_Acc_Agreement_products(string accountNo);
        //Written by sachith on 17/09/2013
        [OperationContract]
        int DuplicateCashDiscount(string _oldCircular, string _newCircular, DateTime _from, DateTime _to, decimal _discountRate, decimal _discountValue, List<string> _items, List<string> _location, string _creby, DateTime _credate, string _com, out string _error);


        //written by sachith on 20-07-2013
        [OperationContract]
        int UpdateCashPromotionalDiscountHdr(int _seq, DateTime _from, DateTime _to, decimal _disval, decimal _disrt, string user, string session);

        //Written by Prabhath on 20/09/2013
        [OperationContract]
        Int32 Save_MST_PC_INFO_LOG(List<MasterSalesPriorityHierarchyLog> _pcInfoHeaders, Dictionary<string, string> code_and_value, out string _msg);

        //Written by Prabhath on 20/09/2013
        [OperationContract]
        Int32 save_mst_loc_info_log(List<MasterLocationPriorityHierarchyLog> _locInfoHeaders, Dictionary<string, string> code_and_value, out string _msg);

        //Written by Prabhath on 20/09/2013
        [OperationContract]
        DataTable GetInforBackDate(string _company, string _module);
        //Written by sachith on 21/09/2013
        [OperationContract]
        void ProcessRccAgent();
        //writtn by darshana on 28-09-2013
        [OperationContract]
        DataTable SearchRequestApprovalDetails(string _com, string _usr, string _type, DateTime _frmDt, DateTime _toDt, string _stus, string _baseTp);
        //writtn by darshana on 30-09-2013
        [OperationContract]
        DataTable SearchrequestAppDetByRef(string _ref);
        //writtn by sachith on 10-10-2013
        [OperationContract]
        List<RequestApprovalHeader> GetPendingSRNRequest(string _com, string _pc, string _inv, string _req);

        //writtn by sachith on 10-10-2013
        [OperationContract]
        List<RequestApprovalHeader> GetPendingExchangeRequest(string _com, string _pc, string _itm, string _req);

        //Written by Prabhath on 7/11/2013
        [OperationContract]
        Int32 UpdateExchangeApprovalStatus(RequestApprovalHeader _UpdateApproval, List<RequestApprovalDetail> _AppDet, bool _isItemChanged, RequestApprovalHeaderLog _AppHdrLog, List<RequestApprovalDetailLog> _AppDetLog, List<RequestApprovalSerialsLog> _AppSerLog);
        //writtn by sachith on 08-11-2013
        [OperationContract]
        DataTable GetSCMCreditNote(string _inv, string _cus);

        //Written by Prabhath on 20/11/2013
        [OperationContract]
        DataTable GetGvCategory(string _company, string _category);

        //Written by Prabhath on 21/11/2013
        [OperationContract]
        int SaveGvCategoryCombination(MasterGiftVoucherCategory _hdr, List<MasterGiftVoucherCategoryDetail> _detail, out string _error);
        //kapila
        [OperationContract]
        List<MasterSalesPriorityHierarchy> GetPCHeirachy(string _loc,string com);


        //Written by Prabhath on 09/12/2013
        [OperationContract]
        decimal GetRentalValue(string _account);
        //Written by sachith on 17/12/2013
        [OperationContract]
        CashPromotionDiscountHeader GetPromotionalDiscountBySeq(int _seq);

        //Written by Prabhath on 21/12/2013
        [OperationContract]
        bool CheckCompany(string _company);

        //Written by Prabhath on 21/12/2013
        [OperationContract]
        bool CheckChannel(string _company, string _channel);

        //Written by Prabhath on 21/12/2013
        [OperationContract]
        bool CheckSubChannel(string _company, string _subchannel);

        //Written by Prabhath on 21/12/2013
        [OperationContract]
        bool CheckArea(string _company, string _area);

        //Written by Prabhath on 21/12/2013
        [OperationContract]
        bool CheckRegion(string _company, string _region);

        //Written by Prabhath on 21/12/2013
        [OperationContract]
        bool CheckZone(string _company, string _zone);

        //Written by Prabhath on 21/12/2013
        [OperationContract]
        bool CheckProfitCenter(string _company, string _profitcenter);

        //kapila
        [OperationContract]
        DataTable GetDeductionDet(Int32 _cd);
        //darshana on 21-02-2014
        [OperationContract]
        DataTable SearchrequestAppAddDetByRef(string _ref);

        //Written by Prabhath on 24/02/2014
        [OperationContract]
        DataTable GetAgentCategory();

        //Written by Prabhath on 25/02/2014
        [OperationContract]
        DataTable SearchServiceAgent(string _company, string _code);

        //Written by Prabhath on 28/02/2014
        [OperationContract]
        DataTable CheckGroupSaleInvoiceStatus(string _com, string _pc, string _code);

        //Sanjeewa 2014-03-24
        [OperationContract]
        void CheckReportName(string _com, string _chnl, string _doctp, out string _repname, out Int16 _ShowComName, out string _PaperSize);

        //Written by Prabhath on 25/03/2014
        [OperationContract]
        Int32 SaveVehicalInsuranceDefinitionNew(List<MasterProfitCenter> pcs, List<InsuItem> items, DateTime from, DateTime to, string satlesType, decimal regVal, string cre, string insCom, string insPol, int term, bool isReq, string _tp, string _book, string _level, string _serial, string _promotion, string _circuler, decimal _isRate, decimal _fromValue, decimal _toValue, string _type, string _circular);


        // Nadeeka 10-04-2015
        [OperationContract]
        bool CheckInsCircular(string _company, string _circular);

        //Written by Prabhath on 26/03/2014
        [OperationContract]
        DataTable GetItemLPStatus();

        //kapila
        [OperationContract]
        int SaveBankBranch(MasterBankBranch _bankBranch, out string _error);

        //Tharaka on 09-07-2014
        [OperationContract]
        DataTable GetProVoutype(string _company, string _Code);

        //Tharaka on 09-07-2014
        // tharanga 2017/06/09 add _separetPrint
        [OperationContract]
        Int32 UpdateProVouTypes(string p_com, string vou_cd, string vou_desc, Int32 vou_act, string p_mod_by, DateTime p_mod_when, Int32 _QtyWise, Int32 _isSMS, string _purSMS, string _redSMS, decimal _minVal, String _cond, List<PromotionVoucherPara> _lstVouPara, Int32 _opt, Int32 _separetPrint);

        //Tharaka on 10-07-2014
        // tharanga 2017/06/09 add _separetPrint
        [OperationContract]
        Int32 SavePromoVouType(string p_com, string vou_cd, string vou_desc, Int32 vou_act, string p_Cre_by, Int32 vou_qty_wise, Int32 _SMS, string _purSMS, string _redSMS, decimal _minVal, String _cond, List<PromotionVoucherPara> _lstVouPara, Int32 _opt, Int32 _sp_print, out string _error, Int32 _dule_auth);

        //Tharaka on 11-07-2014
        [OperationContract]
        Int32 SavePromoVouDefinition(string p_Cre_by);

        //Tharaka on 16-07-2014
        [OperationContract]
        DataTable GetCompanyItemsByCompany(string _company);

        //Tharaka on 19-07-2014
        [OperationContract]
        bool GetNotification(string _company, string Location, string ProfitCenter, string User, out List<Notification> _a, out List<Notification> _b, out List<Thoughts> _Thoughts);

        //Tharaka on 21-07-2014
        [OperationContract]
        List<HPReminder> Notification_Get_AccountRemindersDetails(string _company, string ProfitCenter);

        //Tharaka 28-07-2014
        [OperationContract]
        DataTable Notification_Get_LastDayendDates(string _company, string ProfitCenter);

        //Darshana 30-07-2014
        [OperationContract]
        DataTable GetProfitCenterAllocatedPromotors(string _company, string _profitcenter);

        //Tharaka 13-08-2014
        [OperationContract]
        DataTable GetFunctionApplevel_UserAppLevel(string user, string sart_desc);
        //Shanuka 29-08-2014
        [OperationContract]
        string Get_ProfitCenter_desc(string _company, string _profitcenter);

        //Tharaka on 01-09-2014
        [OperationContract]
        List<ItemConditionCategori> Get_ItemConditionCategories(string Com);

        //Tharaka on 01-09-2014
        [OperationContract]
        List<ItemConditionSetup> Get_ItemConditionBySerial(string Com, string serialNo, string Loc, string itemCode);

        //Tharaka on 02-09-2014
        [OperationContract]
        int Save_itemConditions(ItemConditionSetup oItemConditionSetup);

        //Tharaka on 02-09-2014
        [OperationContract]
        int Update_itemConditions(ItemConditionSetup oItemConditionSetup);

        //Tharaka on 03-09-2014
        [OperationContract]
        DataTable Get_ItemCondition_Inquiary(string UserName, string comnapy, string MainCategori, string SubCategori, string Brand, string ItemCode, string Model, string Condition, List<string> LocationList);

        //shanuka 
        [OperationContract]
        bool check_avl_chn(string com, string code);
        //shanuka 
        [OperationContract]
        DataTable get_stus_chnl(string com, string code);

        //shanuka 
        [OperationContract]
        Int32 Insert_to_chanelDets(Deposit_Bank_Pc_wise _objchanel, out string _err);
        //shanuka 
        [OperationContract]
        Int32 Update_to_chanelDets(Deposit_Bank_Pc_wise _objcha, out string _err);



        //shanuka 
        [OperationContract]
        Int32 Insert_to_subchanelDets(Deposit_Bank_Pc_wise objChanel, out string _err);
        //shanuka 
        [OperationContract]
        Int32 Update_to_subchanelDets(Deposit_Bank_Pc_wise obj_chanel, out string _err);
        //shanuka 
        [OperationContract]
        DataTable getAllChannel_details(string com);
        //shanuka 
        [OperationContract]
        DataTable getAllSubChannel_details(string com);
        //shanuka 
        [OperationContract]
        DataTable getAllarea_details(string com);
        //shanuka 
        [OperationContract]
        DataTable getAllregion_details(string com);
        //shanuka 
        [OperationContract]
        DataTable getAllzone_details(string com);

        //Tharaka 2014-10-07
        [OperationContract]
        Service_Chanal_parameter GetChannelParamers(string com, string Location);
        //shanuka 2014-10-09
        [OperationContract]
        Int32 Insert_to_serviceChnl_para(ServiceChnlDetails objservice_chnl, out string _err);
        //shanuka 2014-10-09
        [OperationContract]
        DataTable getAllserviceChannels(string com);
        //shanuka 2014-10-09
        [OperationContract]
        Int32 Update_to_serviceChnl_para(ServiceChnlDetails objservice_chnl, out string _err);
        //shanuka 2014-10-10
        [OperationContract]
        DataTable get_sub_chanels(string _initialSearchParams, string _searchCatergory, string _searchText);
        //shanuka 2014-10-10
        [OperationContract]
        DataTable GetServiceLocation(string _initialSearchParams, string _searchCatergory, string _searchText);
        //shanuka 2014-10-10
        [OperationContract]
        DataTable get_serviceCenters(string com, string chnl);
        // Nadeeka 2014-12-13
        [OperationContract]
        DataTable get_Language();

        // Nadeeka 2014-12-13
        [OperationContract]
        DataTable get_Buss_ent_type();
        //shanuka 2014-10-11
        [OperationContract]
        Int32 Insert_to_serviceloc(List<ServiceChnlDetails> lstserviceloc, out string _err);
        //shanuka 2014-10-11
        [OperationContract]
        DataTable getAllservice_locDetails(string com, string loc, string sloc);
        //shanuka 2014-10-11
        [OperationContract]
        DataTable getAllservice_paraDets(string com);
        //shanuka 2014-10-11
        [OperationContract]
        DataTable get_service_center_dets(string com);
        //shanuka 2014-10-11
        [OperationContract]
        DataTable get_Status_for_services(string com, string srcenter, string type, string loc);
        //shanuka 2014-10-11
        [OperationContract]
        DataTable get_loc_services(string com, string srcenter, string type);
        //shanuka 2014-10-11
        [OperationContract]
        Int32 Update_to_InactiveServices(ServiceChnlDetails obj_services, out string _err);
        //Tharaka 2014-10-14
        [OperationContract]
        DataTable GetTownByDesc(string _Desc);
        //shanuka 2014-10-15
        [OperationContract]
        DataTable get_sub_chnl_stus(string com, string code);
        //shanuka 2014-10-18
        [OperationContract]
        DataTable get_Default_val(string code);
        //shanuka 2014-10-21
        [OperationContract]
        DataTable get_availble_AllocateItems(string com, string tpe, string loc, string cate);
        //Darshana 2014-10-23
        [OperationContract]
        List<HpAccount> GetHpAccountsByDtRange(string _com, string _pc, DateTime _frmDt, DateTime _toDt);
        //shanuka 2014-10-25
        [OperationContract]
        DataTable get_All_Bnk_StmDetails(DateTime from, DateTime to, string acc);

        //damith 29-12-2014
        [OperationContract]
        String GetJobEstimateTot(string jobNo);

        //Tharaka 2015-01-13
        [OperationContract]
        List<Notification_OthShpCollecitons> GetOtherShopCollections(String Com, String Loc, DateTime Date);

        //Tharaka 2015-02-19
        [OperationContract]
        Master_Employee GetMasterEmployee(String Com, String emp);
        //Darshana 2015-03-04
        [OperationContract]
        DataTable GetProfitCenter(string _company, string _profitcenter);

        //Tharaka 2015-05-16
        [OperationContract]
        List<SEC_SYSTEM_MENU_SUB> GET_REPORT_LIST_BY_MENU(String MenuName);

        //Tharaka 2015-05-21
        [OperationContract]
        DataTable GetSecurityUsers(String Com, String User, String Department, DateTime FromDAte, DateTime Todate);

        //Chamal 09-Jun-2015
        [OperationContract]
        int CheckSCMPeriodIsOpen(BackDates _backdate, out string _err);

        [OperationContract]
        Int32 Common_send_Email();
        

        /*Warehouse Zone*/
        //Rukshan 09-Jul-2015
        [OperationContract]
        Int32 SaveWarehouseZone(List<WarehouseZone> _WarehouseZone, List<WarehouseAisle> _WarehouseAisle, List<WarehouseRow> _WarehouseRow, List<WarehouseLevel> _WarehouseLevel, DataTable _tbl);
        //Rukshan 09-Jul-2015
        [OperationContract]
        Int32 UpdateWarehouseZone(List<WarehouseZone> _WarehouseZone, List<WarehouseAisle> _WarehouseAisle, List<WarehouseRow> _WarehouseRow, List<WarehouseLevel> _WarehouseLevel);
        //Rukshan 09-Jul-2015
        [OperationContract]
        List<WarehouseZone> GetWarehouseZone(string _Com, string _Loc);
        //Rukshan 09-Jul-2015
        [OperationContract]
        List<WarehouseAisle> GetWarehouseAisle(string _Com, string _Loc, string _Zone);
        //Rukshan 09-Jul-2015
        [OperationContract]
        List<WarehouseRow> GetWarehouseRow(string _Com, string _Loc, string _Zseq, string _Aisle);
        //Rukshan 09-Jul-2015
        [OperationContract]
        List<WarehouseLevel> GetWarehouseLevel(string _Com, string _Loc, string p_Zseq, string _Aseq, string Rseq);
        //Rukshan 09-Jul-2015
        [OperationContract]
        List<MasterUOM> GetItemUOM_active();
        //Rukshan 09-Jul-2015
        [OperationContract]
        DataTable GetItemCate1_active(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Rukshan 09-Jul-2015
        [OperationContract]
        DataTable GetItemSubCate2_active(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Rukshan 09-Jul-2015
        [OperationContract]
        DataTable GetItemSubCate3_active(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Rukshan 09-Jul-2015
        [OperationContract]
        DataTable GetItemSubCate4_active(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Rukshan 09-Jul-2015
        [OperationContract]
        DataTable GetItemSubCate5_active(string _initialSearchParams, string _searchCatergory, string _searchText);


        #region FastForwardSCMWeb
        //Sahan
        [OperationContract]
        Int32 UpdateSupplierCurrency(SupplierCurrency currency);

        //Sahan
        [OperationContract]
        Int32 DeactivateSupplierCurrency(SupplierCurrency currency);

        //Sahan
        [OperationContract]
        Int32 UpdateSupplierPorts(SupplierPort port);

        //Sahan
        [OperationContract]
        Int32 DeactivateSupplierPorts(SupplierPort port);

        //Sahan
        [OperationContract]
        Int32 DeactivateSupplierItems(BusEntityItem _item);

        //Sahan
        [OperationContract]
        Int32 UpdateSupplierItems(BusEntityItem _item);

        //Sahan
        [OperationContract]
        Int32 UpdateSupplierTax(BusEntityTax _tax);

        #endregion
        //Rukshan 09-Jul-2015
        [OperationContract]
        Int32 SaveWarehouseByExcel(List<WarehouseZone> _WarehouseZone, List<WarehouseAisle> _WarehouseAisle, List<WarehouseRow> _WarehouseRow, List<WarehouseLevel> _WarehouseLevel, List<WarehouseBin> _WarehouseBin, out string _code);
        //Rukshan 09-Jul-2015
        [OperationContract]
        DataTable GetWarehouseStorage();
        //Rukshan 09-Jul-2015
        [OperationContract]
        Int32 SaveWarehouseBin(List<WarehouseBin> _WarehouseBin, List<warehouseStorageFacility> _warehouseStorageFacility, List<warehouseBinItems> _warehouseBinItems);
        //Rukshan 09-Jul-2015
        [OperationContract]
        Tuple<List<WarehouseBin>, List<warehouseBinItems>, List<warehouseStorageFacility>> GetWarehouseBin(int Seq, string _Loc, string _Com, int _Zone, int _Aisle, int _Row, int _Level);
        //Rukshan 09-Jul-2015
        [OperationContract]
        Int32 UpdateWarehouseBin(List<WarehouseBin> _WarehouseBin, List<warehouseStorageFacility> _warehouseStorageFacility, List<warehouseBinItems> _warehouseBinItems);

        //24/Aug/2015 Rukshan
        [OperationContract]
        DataTable GetWarehouseBinByLoc(string _initialSearchParams, string _searchCatergory, string _searchText);

        //10-12-2015 Lakshan
        [OperationContract]
        DataTable GetItemSubCat4(string _initialSearchParams, string _searchCatergory, string _searchText);

        //10-12-2015 Lakshan
        [OperationContract]
        DataTable GetItemSubCat5(string _initialSearchParams, string _searchCatergory, string _searchText);

        //09/Dec/2015 Rukshan
        [OperationContract]
        Int32 SaveDemurrage(List<DemurrageTracker> _Demurrage);

        //17-12-2015 Lakshan
        [OperationContract]
        Int32 SaveHsCodeClaim(List<HsCodeClaim> hsCodeClaimList);

        //17-12-2015 Lakshan
        [OperationContract]
        Int32 SaveHsCode(List<HsCode> hsCodeList);

        //13-01-2016 Lakshan
        [OperationContract]
        Int32 UpdateSupplierPort(List<SupplierPort> supPorts);

        //Chamal 18-Dec-2015
        [OperationContract]
        DataTable GetCompanyInforDT(string _comCode);


        //Rukshan 2015-12-28
        [OperationContract]
        List<ItemPrefix> GET_ITM_PREFIX(string _itm);

        //Lakshan 2015-12-28
        [OperationContract]
        List<BusEntityItem> GetBuninessEntityItemBySupplier(string _comp, string _code);

        //Rukshan 2015/12/29
        [OperationContract]
        DataTable GetBinType();

        //Rukshan 2015/12/29
        [OperationContract]
        DataTable[] GetITEMCOST(string _com, string _Ord, string _item, string _model, string _status, int _row, string _bl);

        //Rukshan 2015/12/30
        [OperationContract]
        DataTable GetDUTYPRICE_BYITEM(string _com, string _item, DateTime _date);

        //Rukshan 2016/1/2
        [OperationContract]
        DataTable GETLOGISTIC_COST(string _com, string _item, string _doc);

        //Sahan 06 Jan 2016
        [OperationContract]
        Int32 SaveFleet(RouteHeader Header, RouteWareHouse Warehouse, RouteShowRooms ShowRoom, DataTable dtwarehouses, DataTable dtlocations, string user, string session);

        //Sahan 06 Jan 2016
        [OperationContract]
        Int32 CreateSchedule(RouteSchedule Schedule);

        //Sahan 06 Jan 2016
        [OperationContract]
        DataTable SearchRoutes(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 11 Jan 2016
        [OperationContract]
        DataTable LoadRouteSchedules(string p_route_code);

        //Sahan 11 Jan 2016
        [OperationContract]
        DataTable LoadRouteWareHouses(string p_route_code);

        //Sahan 11 Jan 2016
        [OperationContract]
        DataTable LoadRouteLocations(string p_route_code);

        //Sahan 11 Jan 2016
        [OperationContract]
        DataTable SearchWareHouses(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 11 Jan 2016
        [OperationContract]
        DataTable SearchCompanies(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 2016-01-12
        [OperationContract]
        Int32 BackDateProcess(String Company, String GlbUserID, String GlbUserSessionID, List<string> selected_Module_list, List<string> pc_list, List<string> loc_list, Int32 SelectedIndex, bool IsAllowTxn, DateTime from, DateTime To, DateTime FromApply, DateTime ToApply, String OtheCom, String OthChanal, String OPE, Int32 SelectedOption, out string err);


        //Lakshan 2016-01-13
        [OperationContract]
        DataTable GET_PORT_DATA_BY_CD(string code);



        //Lakshan 2016-01-22
        [OperationContract]
        Int32 SaveWarrAmendReq(List<WarrantyAmendRequest> watAmendReq, out string err);

        //Lakshan 2016-01-22
        [OperationContract]
        Int32 UpdateWarrAmendReq(List<WarrantyAmendRequest> watAmendReq, out string err);


        //Lakshan 2016-01-22
        [OperationContract]
        Int32 SaveSerialMasterLog(List<SerialMasterLog> serialMasterLog, out string err);

        //Lakshan 2016-01-22
        [OperationContract]
        Int32 UpdateWarrantyMasterAmend(List<InventoryWarrantyDetail> warrDetails, out string err);

        //Lakshan 2016-01-22
        [OperationContract]
        WarrantyAmendRequest GetWarrantyAmendReqData(WarrantyAmendRequest obj);

        //Rukshan 2016/01/26
        [OperationContract]
        DataTable GET_REF_SER2();

        //Sahan 29 Jan 2016
        [OperationContract]
        DataTable LoadLocationDetailsByCode(string company, string code);

        //Lakshan 29/01/2016
        [OperationContract]
        DataTable SearchCurrencyData(string curr);

        //Lakshan 29/01/2016
        [OperationContract]
        bool CheckCurrency(string curr);

        //Lakshan 29/01/2016
        [OperationContract]
        string Get_Currency_desc(string curr);

        //Lakshan 02/02/2016
        [OperationContract]
        bool CheckCustomer(string customer);

        //Lakshan 02/02/2016
        [OperationContract]
        string Get_Customer_desc(string customer);

        //Lakshan 02/02/2016
        [OperationContract]
        DataTable SearchCustomerData(string customer);

        //Tharaka 2016-02-02
        [OperationContract]
        List<PromotionVoucherPara> GET_VOUPARA_BY_CD(string Code);

        //Tharaka 2016-02-03
        [OperationContract]
        List<REF_ALERT_SETUP> GET_REF_ALERT_SETUP(string Com, string Type, string PartyCode, string ModuleName, string Status);

        //Sahan 05/Feb/2016
        [OperationContract]
        Int32 SaveItemKitComponent(ItemKitComponent ItemKitComp);

        //Sahan 05/Feb/2016
        [OperationContract]
        DataTable LoadItemKitComponents(string item);

        //Sahan 8 Feb 2016
        [OperationContract]
        DataTable SearchMainItems(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 8 Feb 2016
        [OperationContract]
        DataTable SearchComponentItems(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 8 Feb 2016
        [OperationContract]
        DataTable LoadSerialByBin(string company, string location, string serial, string _bin);

        //Rukshan 11/Feb/2016
        [OperationContract]
        bool Check_MRN_Item(string _com, string _loc, string _item, out string qty);

        //Lakshan 20/Feb/2016
        [OperationContract]
        Int32 UpdateLocationMasterNew(MasterLocationNew _loc, string err);

        //Lakshan 20/Feb/2016
        [OperationContract]
        List<MasterLocationNew> GetMasterLocations(MasterLocationNew _loc);

        //Lakshan 20/Feb/2016
        [OperationContract]
        MasterLocationNew GetMasterLocation(MasterLocationNew _loc);

        //Lakshan 2016-Mar-01
        [OperationContract]
        Int32 Save_profit_center_new(MasterProfitCenter _pcheader, string _chnl, string _schnl, string _area, string _reg, string _zone, string _user, out string _err);

        //Lakshan 2016-Mar-04
        [OperationContract]
        List<MasterBinLocation> GetMasterBinLocations(MasterBinLocation _bin);

        //Lakshan 2016-Mar-04
        [OperationContract]
        MasterBinLocation GetMasterBinLocation(MasterBinLocation _bin);

        //Lakshan 16 Mar 2016
        [OperationContract]
        List<SupplierPort> GetSupplierPorts(SupplierPort port);

        //Lakshan 16 Mar 2016
        [OperationContract]
        SupplierPort GetSupplierPort(SupplierPort port);

        //Sahan 25 March 2016
        [OperationContract]
        Int32 UpdateBankAccounts(MasterBankAccount Account);

        //Sahan 25 Mar 2016
        [OperationContract]
        DataTable LoadBankAccountData(MasterBankAccount AccountData);

        //Sahan 25 March 2016
        [OperationContract]
        Int32 UpdateAccountFacility(BankAccountFacility Facility);

        //Sahan 25 Mar 2016
        [OperationContract]
        DataTable LoadBankAccountFacilityData(BankAccountFacility FacilityData);

        //30 March 2016
        [OperationContract]
        DataTable SearchAgent(string _initialSearchParams, string _searchCatergory, string _searchText);

        //31 Mar 2016
        [OperationContract]
        DataTable SearchBankAccounts(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Lakshan 01 Apr 2016
        [OperationContract]
        List<TransportMethod> GET_TRANS_METH(TransportMethod _obj);

        //Lakshan 01 Apr 2016
        [OperationContract]
        List<TransportParty> GET_TRANSPORT_PTY(TransportParty _obj);

        //Sahan 6 APril 2016
        [OperationContract]
        MasterBusinessEntity GetAllCustomerProfileByCom(string CustCD, string nic, string DL, string PPNo, string brNo, string com);

        //Lakshan 12 Apr 2016
        [OperationContract]
        Int32 Save_Int_Transport(Transport _obj);

        //Rukshan 12/Apr/2016
        [OperationContract]
        DataTable GetItemStatusByCom(string _comp);

        //Rukshan 12/Apr/2016
        [OperationContract]
        bool CHECKALLOCATE_CUS(string _company, string _profitcenter, string _emp);

        //Kelum : 20-April-2016

        #region Employee Creation

        [OperationContract]
        Int32 SaveNewEmployee(Employee _employeeNew, List<MasterProfitCenter_LocationEmployee> _MPcE, List<MasterProfitCenter_LocationEmployee> _MLE, List<MasterCustomerEmployee> _MstCusEmp, string _com, out string _err);

        #endregion

        #region Employee Update

        [OperationContract]
        Int32 UpdateEmployee(Employee _employeeNew, List<MasterProfitCenter_LocationEmployee> _MPcE, List<MasterProfitCenter_LocationEmployee> _MLE, List<MasterCustomerEmployee> _MstCusEmp, string _com, out string _err);

        #endregion

        //Kelum : 22-April-2016

        #region Get Employee Details

        [OperationContract]
        DataTable Get_EPFNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable Get_EMPCode(string _initialSearchParams, string _searchCatergory, string _searchText);

        #endregion

        //Kelum : 22-April-2016

        #region Get Category Details

        [OperationContract]
        DataTable Get_Category(string _initialSearchParams, string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable Get_SubCategory(string _initialSearchParams, string _searchCatergory, string _searchText);

        #endregion

        //Kelum : Get Employee Details : 2016-April-26
        [OperationContract]
        Employee GetEmployeeMaster(string _employee, string _com);

        // Kelum : Get Employee Profit Center or Location Details : 2016-April-26
        [OperationContract]
        List<MasterProfitCenter_LocationEmployee> getEmployeeProfitCenter_Location(string _employee, string _com, string PCorL);

        // Kelum : Get Employee's Assinged Customers : 2016-April-26
        [OperationContract]
        List<MasterCustomerEmployee> getEmployeeCustomers(string _com, string _empcode);

        // Rukshan : 2016-April-27
        [OperationContract]
        DataTable getSeason(string _com);

        //Lakshan 2016 Apr 30
        [OperationContract]
        List<Transport> GET_INT_TRANSPORT(Transport _obj);

        // Kelum : Get Customer's Office of Entry : 2016-May-02
        [OperationContract]
        List<MasterBusinessOfficeEntry> getCustomerOfficeofEntry(string _com, string _emp, string _type);

        //Ruskahn 2016 May 09
        [OperationContract]
        DataTable SearchBOQDocNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Kelum : Get PO/BL Number : 2016-June-01 
        [OperationContract]
        DataTable Get_PoBlNumber(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Kelum : Get DOC_GRN Number : 2016-June-01
        [OperationContract]
        DataTable Get_DocGrnNumber(string _initialSearchParams, DateTime? _docdtfrom, DateTime? _docdtto, string _searchCatergory, string _searchText);

        //Kelum : Get Entry Number : 2016-June-01
        [OperationContract]
        DataTable Get_EntryNumber(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Kelum : Load Document Wise Balances : 2016-June-01
        [OperationContract]
        DataTable LoadDocumentWiseBalance(string _initialSearchParams, string _poblnumber, string _docno, string _entryno);

        //subodana 2016-07-11
        [OperationContract]
        DataTable GetAllChannel(string _company, string _channel);

        //Lakshan 15 Jul 2016
        [OperationContract]
        List<MasterStatus> GET_MST_STUS(MasterStatus _obj);

        //Randima 20 Jul 2016
        [OperationContract]
        DataTable SearchPurOrdCostingDet(string _initialSearchParams, string _PONo, string _item, string _model,string serachType, DateTime? _frmDt, DateTime? _toDt);

        //Randima 20 Jul 2016
        [OperationContract]
        DataTable SearchLatestPriceBooks(string _initialSearchParams, string _item, string _status);

        //Lakshika 2016-07-21
        [OperationContract]
        List<mst_commodel> GetCompanyByModel(string _code = null);

        //Lakshika 2016-07-21
        [OperationContract]
        List<mst_model_replace> GetReplacedModelsByModel(string _code = null);

        //Randima 2016-07-21
        [OperationContract]
        DataTable SearchLatestOrders(string _item, string _tp);

        //Randima 2016-07-21
        [OperationContract]
        DataTable SearchPriceForGPCal(string _item, string _status, DateTime _frmDt, DateTime _toDt);

        //Lakshika 2016-07-22
        [OperationContract]
        List<BusinessEntityVal> GetModelClassificationByModel(string _code = null);

        //Randima 2016-07-26
        [OperationContract]
        DataTable SerachBLOrdDet(string _blNo, string _item, string serachType, DateTime? _frmDt, DateTime? _toDt, string com);

        // Randima 2016-12-19
        [OperationContract]
        DataTable GetItmCostDetail(string _item, string com, string type, string reccount, string status);

        //Randima 2016-08-10
        [OperationContract]
        DataTable GetVATRate(string _com, string _item);
        
        //Randima 2016-08-11
        [OperationContract]
        DataTable checkOrdType(string ordNo);

        //Lakshan 2016-08-29
        [OperationContract]
        List<MasterBankBranch> GetBankBranchData(MasterBankBranch _obj);
        //Lakshan 2016 Sep 22
        [OperationContract]
        List<SystemUserLoc> GET_SEC_USER_LOC_DATA(SystemUserLoc _obj);
        //Nuwa 2016.11.23
        [OperationContract]
        List<PendingExchgAppHead> getPendingExchangeRequests(string company, string status, string appcd, string user, string page, string pagesize, string searchVal, string searchFld, string finstus, Int32 appLvl);
        //Nuwan 2016.11.24
        [OperationContract]
        string getApproveItemsModels(string reqno);
         //Rukshan 2016.11.27s
        [OperationContract]
        DataTable SearchProDocNo(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _from, DateTime _to);

        //SUBODANA 2016-11-29
           [OperationContract]
        List<UnitConvert> GetModelUOM(string MODEL);
        //Nuwan 2016.12.02
           [OperationContract]
           List<ApprovalPermission> getApprovalPermission(string userid, string docLoc, string appcd);
        //Nuwan 2016.12.06
           [OperationContract]
           List<ReferenceDetails> getReferenceDetails(string reqNo);
        //Nuwan 2016.12.06
           [OperationContract]
           List<InvoiceCustomer> getInvoiceCustomer(string invNo);
        //Nuwan 2016.12.06
           [OperationContract]
           string getAccountSchema(string accNo);
        //Nuwan 2016.12.07
           [OperationContract]
           string getPromotionDesc(string promocd);
        //Nuwan 2016.12.08
           [OperationContract]
           List<ApprovalHistory> getApprovalHistoryDetails(string reqNo, string appcd);
           //Nuwan 2016.12.10
           [OperationContract]
           ReferenceDetails getCreditTypData(string reqNo);
        //Nuwan 2016.12.15
           [OperationContract]
           bool validateItemCode(string itemCode);
           //Nuwan 2016.12.15
           [OperationContract]
           bool ApproveExchangeRequest(ApprovalData data, out string error); 
        //Nuwan 2016.12.23
           [OperationContract]
           List<ApprovalPriceBook> getRelatedPriceBook(string pc, string type, string com);
           //Nuwan 2016.12.23
           [OperationContract]
           List<ApprovalPriceBookLevel> getRelatedPriceBookLevel(string pc, string type, string com, string pbook);
        //Nuwan 2016.12.23
           [OperationContract]
           List<ApprovalItemStatus> getPbLvlItemStatus(string pb, string lvl, string com);
        //Nuwan 2016.12.23
           [OperationContract]
           decimal getPricebookItemPrice(string pbook, string pbooklvl, string itmcd, string cuscd, DateTime curdate, Int32 qty, string com,string status);
        //Nuwan 2016.12.23
           [OperationContract]
           ApproveItemDetail getApproveItemDetails(string itmcd);

        //subodana 2016-12-28
         [OperationContract]
           Int32 AmmendEPFNew(string empcd, string epfnw, string com);
        //Nuwan 2016.12.29
         [OperationContract]
         List<InventorySerialN> getSerialDetailsForDeVal(string com, string location);
         //Lakshan 03 Jan 2017
         [OperationContract]
         Int32 SaveFleetVehicleMaster(FleetVehicleMaster _obj, out string _err);
         //Lakshan 03 Jan 2017
         [OperationContract]
         List<FleetVehicleMaster> GET_FLT_VEHICLE_DATA(FleetVehicleMaster _obj);
        //Lakshan 24 Jan 2017
         [OperationContract]
         List<RouteHeader> GET_ROUTE_HDR_DATA(RouteHeader _obj);
         //Lakshan 24 Jan 2017
         [OperationContract]
         List<RouteShowRooms> GET_ROUTE_SHOWROOM_DATA(RouteShowRooms _obj);
         //Lakshan 24 Jan 2017
         [OperationContract]
         List<RouteWareHouse> GET_ROUTE_WAREHOUSE_DATA(RouteWareHouse _obj);
         //Lakshan 24 Jan 2017
         [OperationContract]
         List<RouteSchedule> GET_ROUTE_SHEDULE_DATA(RouteSchedule _obj);

         //Lakshan 24 Jan 2017
         [OperationContract]
         List<mst_itm_pc_warr> getPcWarrantNew(string _item);

         //RUKSHAN 24 Jan 2017
         [OperationContract]
         Int32 SaveItemKitComponentNEW(List<ItemKitComponent> _comlist, List<mst_itm_fg_cost> _cost, List<MasterKitComFineItem> _FITEM, out string _err);
        //subodana added to client to service Randima's function
        [OperationContract]
         DataTable CostInqirytblitemlist(List<string> itmList, string com, bool chkGPAllDoc, string pricebook, string pricelevel, string reccount, string status,string user);
        //subodana added to client to service Randima's function
        [OperationContract]
        DataTable CostInqirytblitem(List<string> itmList, string com, bool chkGPAllDoc, string pricebook, string pricelevel, string item, string reccount, string status,bool chkAllPrice);
        //Lakshan 18 Feb 2017
        [OperationContract]
        MasterLocationNew GetLocationDataForPdaSend(MasterLocationNew _loc);
        //Lakshan 18 Feb 2017
        [OperationContract]
        MasterLocationNew GET_MST_LOC_DATA(MasterLocationNew _loc);
        //Lakshan 18 Feb 2017
        [OperationContract]
        MasterLocationPriorityHierarchy GET_MST_LOC_INFO_DATA(string _mliLocCd, string _mliCd);
        //Nuwan 2017.02.22
        [OperationContract]
        string userEmailList(string reqno);
        //Nuwan 2017.02.22
        [OperationContract]
        string getFinalApprovalLevel(string appcd,string company,string pc,string cate);
        //subodana 2017-03-02
         [OperationContract]
        DataTable GetItemPrefix(string _item);
         [OperationContract]
         Int32 saveLoadingBay(List<LoadingBay> _loadingBay);
        //dilshan 24/10/2017
         [OperationContract]
         Int32 saveDocLoadingBay(List<ReptPickHeader> _loadingBay);

         //Isuru 2017/03/24
         [OperationContract]
         DataTable LoadSunPrefixFacilityData(gnr_acc_sun_ledger FacilityData);


         //Isuru 2017/03/25
         [OperationContract]
         List<sar_tp> GetMasterPrefixDatas(sar_tp _loc);

         //Isuru 2017/03/25
         [OperationContract]
        sar_tp GetMasterPrefixData(sar_tp _loc);

        //Udaya 27/03/2017
        [OperationContract]
         DataTable ViewCommissionsDetails(DateTime FromData, DateTime ToDate, string circularCode, string userid, string com);
        // Nadeeka 25-05-2015 copy by lakshan 20 Apr 2017 add new converter
        [OperationContract]
        List<MasterItemModel> GetItemModelNew(string _code = null);
        //Lakshan 20 Apr 2017
        [OperationContract]
        MasterItem GetItemMasterNew(string _item);

        [OperationContract]
        DataTable Get_DocGrnNumber2(string _initialSearchParams, DateTime? _docdtfrom, DateTime? _docdtto, string _searchCatergory, string _searchText);
        //Udaya 25/04/2017
        [OperationContract]
        DataTable SearchProjectCode(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime FromDate, DateTime ToDate, string code);

        //Isuru 2017/05/03
        [OperationContract]
        Int32 uploadlocmasterdet(List<MasterLocationNew> _locmasterdet);

        //Isuru 2017/05/03
        [OperationContract]
        Int32 Updatelogdetails(List<MasterLocationNew> _locmasterdet);
        //Tharanga 2017/05/13
        [OperationContract]
        Int32 SaveVehicalTransportDefinitionNew(List<MasterProfitCenter> pcs,  DateTime from, DateTime to, string satlesType, decimal regVal, string cre, string insCom, string insPol, int term, bool isReq, string _tp, string _book, string _level, string _serial, string _promotion, string _circuler, decimal _isRate, decimal _fromValue, decimal _toValue, string _type, string _circular);
        //Udaya 30/05/2017
        [OperationContract]
        Int32 saveItemWarrenty(List<mst_itm_pc_warr> _lstpcwara, List<MasterItemWarrantyPeriod> _lstitmWrd, out string _err);
        //Add by Lakshan 05 Jun 2017
        [OperationContract]
        List<mst_itm_tax_structure_det> GetItemTaxStructureWeb(string _code);
        //Add by Dilshan 26 sep 2017
        [OperationContract]
        List<MgrCreation> GetMgrlocationWeb(string _code);
        //Add by Lakshan 05 Jun 2017
        [OperationContract]
        MST_TAX_CD GET_MST_TAX_CD_DATA(string _taxRtCode);
        //Tharanga 2017/07/04
        [OperationContract]
        Int32 Save_MST_PC_INFO_LOG_Excle_upload(List<MasterSalesPriorityHierarchyLog> _pcInfoHeaders, List<PcList> _PcList, out string _msg);
         //Tharanga 2017/07/04
        [OperationContract]
        Int32 save_mst_loc_info_log_exle(List<MasterLocationPriorityHierarchyLog> _locInfoHeaders, List<PcList> _PcList, out string _msg);
        //Dilshan 2017-09-26
        [OperationContract]
        int SaveMangerDetails(List<MgrCreation> _lst, out string _errer);
        //Dilshan 2017-09-26
        [OperationContract]
        int SaveCirDetails(List<hpr_disr_val_ref> _lst, int status, out string _errer);
        //Dilshan 2017-09-26
        [OperationContract]
        int SaveArrearsDetails(List<hpr_ars_rls_sch> _lst, out string _errer);
        //Dilshan 2017-09-29
        [OperationContract]
        int SaveBonusDetails(List<BonusDefinition> _lst, out string _errer);
        //Dilshan 2017-09-26 
        [OperationContract]
        int SaveLocDetails(List<hpr_disr_pc_defn> _lst, out string _errer);
        //Dilshan 2017-09-27
        [OperationContract]
        List<MgrCreation> GetMgrextData(String com, string mgr);

        //By Akila 2017/09/29
        [OperationContract]
        DataTable GetBankDetailsByBinCode(string _binCode);
        //Dilshan 2017-09-27
        [OperationContract]
        List<hpr_disr_val_ref> GetCircularData(String com, string mgr, string cat);
        //Dilshan 2017-09-27
        [OperationContract]
        List<hpr_disr_val_ref> CheckCircularData(String com, string mgr, string cat);
        //Dilshan 2017-09-27
        [OperationContract]
        List<hpr_disr_pc_defn> GetCirLocData(String com, string mgr);
        //Dilshan 2017-09-27
        [OperationContract]
        List<BonusDefinition> GetCircularcbdData(String com, string mgr);
        //Dilshan 2017-09-27
        [OperationContract]
        List<hpr_ars_rls_sch> GetSchemeData(string com, string channel, string location);
        //Dilshan 2017-09-27
        [OperationContract]
        Int32 GetSchemeTerm(string _type, string _code, string _term);
        //Added By Udaya 06.10.2017
        [OperationContract]
        DataTable getItemInSerial_Rev(string reqNo);

        //akila 2017/010/10
        [OperationContract]
        List<PcAllowBanks> GetPcAllowBanks(string _comCode, string _profitCenter, string _moduleName);
        //Added By Udaya 09.10.2017
        [OperationContract]
        DataTable GetItemInProduction(string docNo, string comCode, string chk);

        //Akila 2017/10/26
        [OperationContract]
        DataTable SearchSalesTypes(string _initialSearchParams, string _searchCatergory, string _searchText);

        // Added by Chathura on 09-nov-2017
        [OperationContract]
        DataTable GetAllCompanies();
        //subodana 2017-12-04
        [OperationContract]
        decimal getPricebookItemPriceNew(string pbook, string pbooklvl, string itmcd, string cuscd, DateTime curdate, Int32 qty, string com, string status);

        //Add by lakshan 28Nov2017
        [OperationContract]
        MasterItemBrand GET_ITM_BRAND_DATA(string _cd);
        //Add by lakshan 28Nov2017
        [OperationContract]
        GradeMaster GET_MST_GRADE_DATA(string _com, string _chnl, string _cd);

        ////  Akila 2017/12/11
        //  [OperationContract]
        //  DataTable GetSysParaDetails(string _company, string _chnl, string _type);
        //  Akila 2017/12/11
        [OperationContract]
        DataTable GetSysParaDetails(string _company, string _chnl, string _type);

        //Akila 2017/12/15
        [OperationContract]
        string UpdateRccReminderDetails(RCC _rcc, out string _message);

        //Tharindu 2017-12-12
        [OperationContract]
        DataTable GetBrand(string _brnd);

        //Tharindu 2017-12-12
        [OperationContract]
        DataTable GetItemStatus(string _itmstatus);

        //Akila 2017/12/28
        [OperationContract]
        string UpdateRccWithRequest(RCC _rcc);

        //Tharanga 2018/01/22
        [OperationContract]
        DataTable GET_REF_AGE_SLOT(string _com);
        //Add by lakshan 28Dec2017
        [OperationContract]
        BusEntityItem GetBuninessEntityItemBySupplierItm(string _comp, string _code, string _itm);
        
        //Add by Dulaj 2018-Feb-08
        [OperationContract]
        Int32 SaveUpdateWarrentyAmend(List<SerialMasterLog> objs, List<InventoryWarrantyDetail> objsWarrenty, out string err);
        //Add by tharanga 2018-mar-08
        [OperationContract]
        List<ApprovalReqCategory> getAppReqCateList_New(string _Type, string _main);

        //Dulaj 2018-MAr-10
        [OperationContract]
        DataTable GetProductCondtionParameters(string _com, string para_cd, string _tp, string to_stus);
           //Dulaj 2018-MAr-15
        [OperationContract]
        DataTable GetProductCondtionParametersByCd(string _com, string para_cd, string _cd, string to_stus);
        //Dulaj 2018-Mar-19
        [OperationContract]
        DataTable GetBatchStatus(string _com,string _loc,string _docType,string _direction,DateTime _frm,DateTime _to,Int16 _dateType,Int16 _isFinished,Int16 _wip,string doctypePending,int sp);
        //Dulaj 2018-Mar-20
        [OperationContract]
        DataTable GetItemStatusByUserSeq(string _tus_usrseq_no);
      //Nuwan 2018.03.27
        [OperationContract]
        DataTable getDetailsForDocGenarate(string pc, string type);
        //Nuwan 2018.03.29
        [OperationContract]
        Int32 updateTempAndAnalDetails(string invoiceno, decimal cost, string pc, string com, out string error);
        //dilshan
        [OperationContract]
        DataTable GetItemAvailability(string _item, string _com);

        //Dulaj 2018-Apr-10
        [OperationContract]
        DataTable GetUserDefineTemplate(string template,string user);
        //Dulaj 2018-Apr-11
        [OperationContract]
        Int32 SaveUserProfileTemplate(string _templateName, string _codes, string _values, string _userId, string _dectription, string _key,string sessionId);

        //Dulaj 2018-apr-12
        [OperationContract]
        Int32 CheckTemplateName(string _tempName);

        //Pasindu 2018/05/31
        [OperationContract]
        DataTable GetItemDetails(string p_location, string p_itemcode, string p_serial, string p_fromdt, string p_todt,string category01,string category02,string com);

        //Pasindu 2018/05/31
        [OperationContract]
        DataTable GetItemAvaialableLocation(string p_serial);
        //Dulaj 2018/Jul/04
        [OperationContract]
         DataTable GetRestrictedMrnLoc(string p_location, string p_itemcode,string p_todt);
        [OperationContract]
        DataTable CostInqirytblitemSerial(List<string> itmList, string com, bool chkGPAllDoc, string pricebook, string pricelevel, string item, string reccount, string status, bool chkAllPrice, string serial);

        // Wimal 16/06/2018
        [OperationContract]
        List<MasterItem> GetItemFromModel(string _Model);

        //Add by tharanga 2018-09/12
        [OperationContract]
        Int32 save_busentity_newcom(string p_com, string p_cust_cd);
        //Add by tharanga 2018-09/12
        [OperationContract]
        DataTable LoadItemKitComponents_ACTIVE(string _itm);

        //Udesh 31-Oct-2018
        [OperationContract]
        OtpAuthentication OTPGenerator(OtpAuthentication _otpAuth);
        
        //Udesh 31-Oct-2018
        [OperationContract]
        int UpdateOTPAthentication(OtpAuthentication _otpAuth);

        //Udesh 31-Oct-2018        
        [OperationContract]
        int SendOtpAuthenticationSMS(OutSMS _out);
        //Dulaj 2018/Nov/01
        [OperationContract]
        DataTable GetBinLocGRN(string com, string loc, string binCd, string decs);

        //Udesh 15-Nov-2018  
        [OperationContract]
        DataTable GetMstGradeByCompany(string _com);

        //Added by Udesh 12-Nov-2018
        [OperationContract]
        int SaveBinAssignment(List<REF_BIN_ASSIGN> _bankBranch, out string _error);

        //Added by Udesh 12-Nov-2018
        [OperationContract]
        DataTable GetBinAssignmentDetailsByBankCode(REF_BIN_ASSIGN _binAssign);

        //Added by Udesh 23-Nov-2018
        [OperationContract]
        REF_BIN_ASSIGN GetBinAssignmentDetailsByBinNumber(REF_BIN_ASSIGN _binAssign);
        //Dulaj 2018/Nov/27
        [OperationContract]
        DataTable GetContribution(string itmCd);
        //Dulaj 2018/Nov/27
        [OperationContract]
        Int32 UpdateItemContribution(string itmCd, string invenCon, string gPCon, string brnCon, out string _error);
        //Dulaj 2018/Dec/04
        [OperationContract]
        Int32 UpdateItemCatContribution(string cat, string invenCon, string gPCon, out string _error);
        //Dulaj 2018/Dec/04
        [OperationContract]
        Int32 UpdateItemBrndContribution(string brndCon, string brnd, out string _error);
        //Dulaj 2018/Dec/05
        [OperationContract]
        REF_ITM_CATE1 GetItemCate1ById(string cd);
        //Dulaj 2018/Nov/28
        [OperationContract]
        bool IsvalidMobileNo(string mobileNo, out string msg);

        //Dilan 2019-02-06
        [OperationContract]
        DataTable GetReimbusmentStus(string docNo);


    }
}
