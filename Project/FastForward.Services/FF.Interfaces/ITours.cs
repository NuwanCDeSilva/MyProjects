using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Web;
using FF.BusinessObjects;
using FF.BusinessObjects.Tours;
using FF.BusinessObjects.ToursNew;

namespace FF.Interfaces
{
    [ServiceContract]
    public interface ITours
    {
        //Sahan
        [OperationContract]
        DataTable SP_TOURS_GET_OVERLAP_DATES(string P_MFD_VEH_NO, string P_MFD_DRI, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt, string p_mfd_seq_no);


        //Sahan
        [OperationContract]
        DataTable sp_tours_get_Selected_alloc(string p_mfd_veh_no, string p_mfd_dri, Int32 p_mfd_act, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt);

        //Sahan
        [OperationContract]
        DataTable SP_TOURS_GET_ALLOCATIONS(string P_MFD_VEH_NO, string p_MFD_DRI);

        //Sahan
        [OperationContract]
        DataTable SP_TOURS_GET_DRIVER_COM(string P_MPE_EPF);

        //Sahan
        [OperationContract]
        Int32 sp_tours_update_driver_alloc(string p_mfd_seq, string p_mfd_veh_no, string p_mfd_dri, Int32 p_mfd_act, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt, string p_mfd_cre_by, DateTime p_mfd_cre_dt, string p_mfd_mod_by, DateTime p_mfd_mod_dt, string p_mfd_com, string p_mfd_pc);


        //Sahan
        [OperationContract]
        DataTable SP_TOURS_GET_DRIVER(string P_MFA_PC);

        //Sahan
        [OperationContract]
        DataTable SP_TOURS_GET_VEHICLE(string P_MFA_PC);

        //Sahan
        [OperationContract]
        DataTable sp_tours_get_fleet_alloc2(string p_mfa_regno);

        //Sahan
        [OperationContract]
        DataTable sp_tours_get_fleet_alloc(string p_mfa_regno, string p_mfa_pc);

        //Sahan
        [OperationContract]
        Int32 sp_tours_update_fleet_alloc(string p_mfa_regno, string p_mfa_pc, Int32 p_mfa_act, string p_mfa_cre_by, DateTime p_mfa_cre_dt, string p_mfa_mod_by, DateTime p_mfa_mod_dt);

        //Sahan
        [OperationContract]
        DataTable Get_Fleet(string p_mstf_regno);
        ////Sahan
        //[OperationContract]
        //Int32 sp_tours_update_fleet(string p_mstf_regno, DateTime p_mstf_dt, string p_mstf_brd, string p_mstf_model, string p_mstf_veh_tp, string p_mstf_sipp_cd, Int32 p_mstf_st_meter, string p_mstf_own, string p_mstf_own_nm, Int32 p_mstf_own_cont, Int32 p_mstf_lst_sermet, string p_mstf_tou_regno, Int32 p_mstf_is_lease, DateTime p_mstf_insu_exp, DateTime p_mstf_reg_exp, string p_mstf_fual_tp, Int32 p_mstf_act, string p_mstf_cre_by, DateTime p_mstf_cre_dt, string p_mstf_mod_by, DateTime p_mstf_mod_dt, string p_mstf_engin_cap, Int32 p_mstf_noof_seat, string p_mst_email, string p_mst_comment, string p_mst_inscom);

        //Sahan
        [OperationContract]
        DataTable Get_Vehicle_Type();
        //Rukshan
        [OperationContract]
        DataTable GetInvoiceDetailsForPrint(string _invoice, string _code);
        //Rukshan
        [OperationContract]
        List<invoiceCenter> InvoiceDeatilsForPrintList(string _invoice);

        //Tharaka 2015-03-09
        [OperationContract]
        List<MST_ENQTP> GET_ENQUIRY_TYPE(string Com);

        //Tharaka 2015-03-09
        [OperationContract]
        List<MST_FACBY> GET_FACILITY_BY(string Com, string type);

        //Tharaka 2015-03-09
        [OperationContract]
        int Save_GEN_CUST_ENQ(GEN_CUST_ENQ oItem, MasterAutoNumber _ReqInsAuto, out string err);

        //Tharaka 2015-03-09
        [OperationContract]
        GEN_CUST_ENQ GET_CUST_ENQRY(string Com, string PC, string ENQID);

        //Tharaka 2015-03-10
        [OperationContract]
        List<GEN_CUST_ENQ> GET_ENQRY_BY_CUST(string Com, string CustomerCode);

        //Tharaka 2015-03-11
        [OperationContract]
        List<GEN_CUST_ENQ> GET_ENQRY_BY_PC_STATUS(string Com, string PC, String Status, string UserID, Int32 PermissionCode);

        //Tharaka 2015-03-11
        [OperationContract]
        List<MST_COST_CATE> GET_COST_CATE(string Com, string PC);

        //Tharaka 2015-03-16
        [OperationContract]
        Int32 Save_QUO_COST_HDR(QUO_COST_HDR lst, out string err);

        //Tharaka 2015-03-17
        [OperationContract]
        Int32 SaveCostingSheet(QUO_COST_HDR oHeader, List<QUO_COST_DET> oItems, MasterAutoNumber _auto, out string err);

        //Tharaka 2015-03-17
        [OperationContract]
        Int32 getCostSheetDetails(string Com, string PC, string enquiryID, string stages, out QUO_COST_HDR oHeader, out  List<QUO_COST_DET> oDetails, out String err);

        //Tharaka 2015-03-25
        [OperationContract]
        Int32 SaveToursrInvoice(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, List<InvoiceVoucher> _voucher, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, bool isTranInvoice, GEN_CUST_ENQSER _genCustEnqser = null, GEN_CUST_ENQ oItem = null, MasterAutoNumber _enqAuto = null, List<MST_ST_PAX_DET> paxDetList = null, bool Tours = true, MasterBusinessEntity _custProfile = null, GroupBussinessEntity _custGroup = null, List<ST_ENQ_CHARGES> enqChrgItems = null);

        //Tharaka 2015-03-26
        [OperationContract]
        Int32 UpdateEnquiryStage(Int32 Stage, String user, String enquiryID, String com, String PC, out String err);

        //Tharaka 2015-03-26
        [OperationContract]
        int GetInvoiceDetails(string Com, string PC, string InvoiceNum, out InvoiceHeader oheader, out List<InvoiceItem> oMainItems, out RecieptHeader oRecieptHeader, out List<RecieptItem> oRecieptItems, out String err);

        //Tharaka 2015-03-31
        [OperationContract]
        int UPDATE_COST_HDR_STATUS(Int32 StageEnqiry, Int32 costHDRStatus, Int32 SeqCost, string com, string pc, string User, string enquiryID, out string err, bool updatePo = false, QUO_COST_HDR oHeader = null);

        //Tharaka 2015-03-30
        [OperationContract]
        SR_AIR_CHARGE GetChargeDetailsByCode(string com, String Cate, string Code, string pc);

        //Tharaka 2015-04-01
        [OperationContract]
        SR_TRANS_CHA GetTransferChargeDetailsByCode(string com, String Cate, string Code, string PC);

        // Nadeeka 06-04-2015
        [OperationContract]
        DataTable GET_ENQUIRY_STATUS(string Com);

        //Tharaka 2015-04-02
        [OperationContract]
        SR_SER_MISS GetMiscellaneousChargeDetailsByCode(string com, String Cate, string Code, string PC);

        //Tharaka 2015-04-06
        [OperationContract]
        List<PriceDefinitionRef> GetToursPriceDefByBookAndLevel(string _company, string _book, string _level, string _invoiceType, string _profitCenter);

        //Tharaka 2015-04-07
        [OperationContract]
        List<PriceDetailRef> GetToursPrice(string _company, string _profitCenter, string _invType, string _priceBook, string _priceLevel, string _customer, string _item, decimal _qty, DateTime _currentDate);

        //Tharaka 2015-04-08
        [OperationContract]
        List<ComboBoxObject> GetServiceClasses(string com, String Cate);

        //Tharaka 2015-04-08
        [OperationContract]
        List<ComboBoxObject> GetServiceProviders(string com, String Cate);

        //Tharaka 2015-04-08
        [OperationContract]
        Int32 SaveAirChageCodes(SR_AIR_CHARGE lst, out string err);

        //Tharaka 2015-04-09
        [OperationContract]
        Int32 SaveTrasportChageCodes(SR_TRANS_CHA lst, out string err);

        //Tharaka 2015-04-09
        [OperationContract]
        Int32 SaveMiscellaneousChageCodes(SR_SER_MISS lst, out string err);

        //Tharaka 2015-04-21
        [OperationContract]
        int SendSMS(OutSMS smsOut, out String err);

        //Tharaka 2015-05-13
        [OperationContract]
        Int32 UpdateEnquiryStageWithlog(Int32 Stage, String user, String enquiryID, String com, String PC, out string err);

        [OperationContract]
        DataTable Get_CostingFormat(string costNumber);

        //Tharaka 2015-05-27 
        [OperationContract]
        RecieptHeaderTBS GetReceiptHeaderTBS(string _com, string _pc, string _doc);

        //Tharaka 2015-05-28
        [OperationContract]
        List<ComboBoxObject> GET_TOUR_PACKAGE_TYPES();

        //Tharaka 2015-05-28
        [OperationContract]
        Int32 UPDATE_INVOICE_STATUS(string status, string user, string com, string pc, string invoice, out string err);

        //Pemil 2015-06-01
        [OperationContract]
        List<Ref_Title> GET_REF_TITLE();

        //Pemil 2015-06-03
        [OperationContract]
        List<Mst_empcate> Get_mst_empcate();

        //Pemil 2015-06-03
        [OperationContract]
        List<MST_VEH_TP> Get_mst_veh_tp();

        //Pemil 2015-06-03
        [OperationContract]
        DataTable Get_mst_profit_center(string com);

        //Pemil 2015-06-03
        [OperationContract]
        Int32 SaveEmployee(MST_EMPLOYEE_TBS mst_employee_tbs, List<MST_PCEMP> mst_pcemp, out string err);

        //Pemil 2015-06-04
        [OperationContract]
        Int32 UpdateEmployee(MST_EMPLOYEE_TBS mst_employee_tbs, List<MST_PCEMP> mst_pcemp, out string err);

        //Pemil 2015-06-04
        [OperationContract]
        MST_EMPLOYEE_TBS Get_mst_employee(string memp_epf);

        //Pemil 2015-06-04
        [OperationContract]
        List<MST_PCEMP> Get_mst_pcemp(string mpe_epf);

        //Tharaka 2015-06-03
        [OperationContract]
        List<ComboBoxObject> GET_ALL_TOWN_FOR_COMBO();

        //Tharaka 2015-06-03
        [OperationContract]
        List<ComboBoxObject> GET_ALL_VEHICLE_FOR_COMBO();

        //Pemil 2015-06-05
        [OperationContract]
        DataTable Get_gen_cust_enq(string com, string PC, string enq_tp, string fleet, string driver, string cus_cd, DateTime? fromDate, DateTime? toDate);

        //Tharaka 2015-06-06
        [OperationContract]
        MST_EMPLOYEE_TBS GetEmployeeByComPC(String com, String PC, String EPF);

        //Tharaka 2015-06-08
        [OperationContract]
        Int32 SaveTripRequestWithInvoice(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, List<InvoiceVoucher> _voucher, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, GEN_CUST_ENQ oItem, MasterAutoNumber _ReqInsAuto, out string errEnquiry, bool isInvoice, GEN_CUST_ENQSER _genCustEnqser = null);

        //Pemil 2015-06-08
        [OperationContract]
        Int32 Check_Employeeepf(string epf);

        //Pemil 2015-06-09
        [OperationContract]
        DataTable Get_triprequest(string enq_id);

        //Pemil 2015-06-12
        [OperationContract]
        DataTable Get_tour_searchreceipttype(string com, Int32 is_refund);

        //Tharaka 2015-05-13
        [OperationContract]
        Int32 saveLogSheet(TR_LOGSHEET_HDR oHeader, List<TR_LOGSHEET_DET> oItems, bool isNew, MasterAutoNumber _auto, out String err, out String logNumber);

        //Tharaka 2015-06-13
        [OperationContract]
        TR_LOGSHEET_HDR GetLogSheetHeader(String com, String PC, String LOG);

        //Tharaka 2015-06-13
        [OperationContract]
        List<TR_LOGSHEET_DET> GetLogSheetDetails(Int32 seqNum);

        //Pemil 2015-06-13
        [OperationContract]
        DataTable Get_tour_logsheet(string com, string pc, string dri_cd, DateTime from_dt, DateTime to_dt);

        //Pemil 2015-06-16
        [OperationContract]
        Int32 SaveNewReceiptTBS(RecieptHeaderTBS _NewReceipt, List<RecieptItemTBS> _NewReceiptDetails, List<SR_PAY_LOG> sr_pay_logList, MasterAutoNumber _masterAutoNumber, ReptPickHeader _pickHeader, List<ReptPickSerials> _pickSerials, List<VehicalRegistration> _vehReg, List<VehicleInsuarance> _insList, List<HpSheduleDetails> _HPSheduleDetails, MasterAutoNumber _masterAutoNumberType, List<GiftVoucherPages> _pageList, string lohNo, Int16 pay_dri, out string docno);

        //Sahan 15 Jun 2015
        [OperationContract]
        DataTable SP_TOURS_GET_ALL_OVERLAP_DATES(string P_MFD_VEH_NO, string P_MFD_DRI, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt);

        //Pemil 2015-06-08
        [OperationContract]
        Int32 Cancel_paymentTBS(int seq, string log_no, string receipt_no);

        //Pemil 2015-06-13
        [OperationContract]
        DataTable Get_tour_searchDriverTBS(string com_cd, string ep, string cat_subcd2, string first_name2, string last_name, string nic, string tou_lic);

        //Pemil 2015-06-18
        [OperationContract]
        DataTable Get_sr_pay_log(string rec_no);

        //Tharaka 2015-06-24
        [OperationContract]
        List<TR_LOGSHEET_DET> GetLogDetailsCustInvoice(String custCode, String Com, DateTime From, DateTime TO, Int32 Status);

        //Tharaka 2015-06-26
        [OperationContract]
        Int32 UPDATE_LOG_HDR_INVOICE(int seq, int STATUS, string USER, out String err);

        //Tharaka 2015-06-30
        [OperationContract]
        TR_LOGSHEET_HDR GET_LOG_HDR_BY_ENQRY(String enquiryID);

        //Tharaka 2016-01-14
        [OperationContract]
        int SaveEnquiryRequestList(List<GEN_CUST_ENQ> oItems, MasterAutoNumber _ReqInsAuto, MasterAutoNumber _mainNumber, out string err);

        //Rukshan 2016-1-28
        [OperationContract]
        List<QUO_COST_HDR> GET_COST_PRFITABILITY(string _com, string _procenter, string _req, string _costRef, string _customer, string _category,
            DateTime _fromdate, DateTime _todate);

        //Rukshan 2016-1-28
        [OperationContract]
        List<QUO_COST_HDR> GET_COST_PRFITABILITY_DETAILS(string _com, string _procenter, string _req, string _costRef, string _customer, string _category,
        DateTime _fromdate, DateTime _todate);

        //Rukshan 2016-01-29
        [OperationContract]
        DataTable Get_COST_HDR_NO(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Rukshan 2016-01-29
        [OperationContract]
        DataTable Get_SERVICE_CODE(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Rukshan 2016-01-30
        [OperationContract]
        List<SR_AIR_CHARGE> GetALLChargeDetailsByCode(string com, String Cate, string Code, DateTime date);

        //Rukshan 2016-01-30
        [OperationContract]
        List<SR_TRANS_CHA> GetAllTransferChargeDetailsByCode(string com, String Cate, string Code, DateTime date);

        //Rukshan 2016-01-30
        [OperationContract]
        List<SR_SER_MISS> GetALLMiscellaneousChargeDetailsByCode(string com, String Cate, string Code, DateTime date);

        /* Created on 09/Feb/2016 4:40:04 PM Lakshan*/
        [OperationContract]
        List<MST_ENQSUBTP> GET_ENQRY_SUB_TP(MST_ENQSUBTP obj);

        //Nuwan 2016/02/10
        [OperationContract]
        List<MST_TITLE> GetTitleList();

        //Nuwan 2016/02/11
        [OperationContract]
        MST_EMPLOYEE_NEW_TBS getMstEmployeeDetails(string empCode);

        //Subodana 2016/02/11
        [OperationContract]
        MST_FLEET_NEW getMstFleetDetails(string regNo);

        //Nuwan 2016/02/11
        [OperationContract]
        Int32 SaveEmployeeData(MST_EMPLOYEE_NEW_TBS mst_employee_tbs, List<MST_PCEMP> mst_pcemp, List<mst_fleet_driver> vehicleList, string userId, string userDefPro, DateTime serverDatetime, string driverCom, out string err);

        //Subodana 2016/02/16
        [OperationContract]
        Int32 SaveFleetData(string SeqNo, string p_mstf_regno, DateTime p_mstf_dt, string p_mstf_brd, string p_mstf_model, string p_mstf_veh_tp, string p_mstf_sipp_cd, Int32 p_mstf_st_meter, string p_mstf_own, string p_mstf_own_nm, Int32 p_mstf_own_cont, Int32 p_mstf_lst_sermet, string p_mstf_tou_regno, Int32 p_mstf_is_lease, DateTime p_mstf_insu_exp, DateTime p_mstf_reg_exp, string p_mstf_fual_tp, Int32 p_mstf_act, string p_mstf_cre_by, DateTime p_mstf_cre_dt, string p_mstf_mod_by, DateTime p_mstf_mod_dt, string p_mstf_engin_cap, Int32 p_mstf_noof_seat, string p_mst_email, string p_mst_comment, string p_mst_inscom, string p_mfd_veh_no, string p_mfd_dri, Int32 p_mfd_act, DateTime p_mfd_frm_dt, DateTime p_mfd_to_dt, string p_mfd_cre_by, DateTime p_mfd_cre_dt, string p_mfd_mod_by, DateTime p_mfd_mod_dt, string p_mfd_com, string p_mfd_pc, List<mst_fleet_alloc> mst_pcemp, decimal cost, string reason, string ownadd1, string ownadd2, DateTime todate, DateTime fromdt, string p_nic, decimal mileage, decimal fullday, decimal halfday, decimal air, decimal corramount, decimal deposit);

        //Nuwan 2016/02/11
        [OperationContract]
        Int32 UpdateEmployeeData(MST_EMPLOYEE_NEW_TBS mst_employee_tbs, List<MST_PCEMP> mst_pcemp, List<mst_fleet_driver> vehicleList, string userId, string userDefPro, DateTime serverDatetime, string driverCom, out string err);

        //Nuwan 2016/02/12
        [OperationContract]
        List<mst_fleet_driver> getAllocateVehicles(string driver);

        [OperationContract]
        List<Mst_Fleet_driver_new> getAllocateVehiclesnew(string driver);

        //Subodana 2016/02/16
        [OperationContract]
        List<mst_fleet_driver> getAllocateVehiclesByID(string regNo);

        [OperationContract]
        string getBankCode(string bankId);

        //subodana
        [OperationContract]
        List<mst_fleet_alloc> Get_mst_fleet_alloc(string regNo);

        //Nuwan 2016/02/17
        [OperationContract]
        string getAdvanceRefAmount(string cuscd, string company, string receiptno);

        //Nuwan 2016/02/17
        [OperationContract]
        string getCreditRefAmount(string cuscd, string company, string refNo, string profcen);

        //Subodana 2016-02-23
        [OperationContract]
        int UPDATE_DRIVER_ALLO_STATUS_TO_INACT(Int32 MFD_SEQ);

        [OperationContract]
        int UPDATE_DRIVER_ALLO_STATUS_TO_ACT(Int32 MFD_SEQ);

        //Nuwan 2016/02/29
        [OperationContract]
        GEN_CUST_ENQ getEnquiryDetails(string Com, string PC, string ENQID);

        [OperationContract]
        int UPDATE_ENQ_STATUS_WITH_REASON(string ENQ_ID, string ENQ);

        [OperationContract]
        int Save_ENQ_DATA(GEN_CUST_ENQ oItem, MasterAutoNumber _ReqInsAuto, GEN_CUST_ENQSER sItem, out string err, string countrycode, string userDefPro, MasterBusinessEntity cus);

        [OperationContract]
        List<SEARCH_MST_EMP> Get_mst_emp(string _company, string _profitcenter);

        //Nuwan 2016/03/05
        [OperationContract]
        MST_EMPLOYEE_NEW_TBS getMstEmployeeDetailsByNic(string Nic);

        //subodana
        [OperationContract]
        List<GEN_CUST_ENQ> GET_ENQRY_BY_CUST_PEN_INV(string Com, string CustomerCode);

        //subodana
        [OperationContract]
        int UPDATE_ENQ_STATUS(string cuscode, string enqid);

        //Sanjeewa 2016-03-24
        [OperationContract]
        DataTable Get_DailySalesReport(DateTime _fdate, DateTime _tdate, string _customer, string _itemcode, string _com, string _pc, string _user);

        //Sanjeewa 2016-04-19
        [OperationContract]
        DataTable Get_DebtorStatementReport(DateTime _fdate, DateTime _tdate, string _customer, string _com, string _pc, string _user);

        //subodana
        [OperationContract]
        List<GEN_CUST_ENQSER> GetEnqSerData(string enqid);

        //Sanjeewa 2016-03-24
        [OperationContract]
        DataTable Get_DailySalesDetailReport(DateTime _fdate, DateTime _tdate, string _customer, string _itemcode, string _com, string _pc, string _user);

        //Sanjeewa 2016-04-01
        [OperationContract]
        DataTable Get_ATSInquiryReport(DateTime _fdate, DateTime _tdate, string _customer, string _inqid, Int16 _status, string _type, string _com, string _pc, string _user);

        //Sanjeewa 2016-03-25
        [OperationContract]
        DataTable Get_ReceiptDetailReport(DateTime _fdate, DateTime _tdate, string _customer, string _com, string _pc, string _user);
        //subodadana
        [OperationContract]
        List<GEN_CUST_ENQ> GET_ENQRY_BY_PC_STATUS_NEW(string Com, string PC, String Status, string UserID, Int32 PermissionCode);
        //subodana
        [OperationContract]
        List<InvoiceHeader> GetInvoiceData(string EnqId);
        //subodana
        [OperationContract]
        List<InvoiceItem> GetInvoiceDetailforInvNo(string InvNo);
        //subodana
        [OperationContract]
        List<InvoiceHeader> GetInvoiceHDRData(string InvNo);
        //Nuwan 2016/03/31
        [OperationContract]
        string GetServiceByCode(string company, string userDefPro, string chgCd);
        //Nuwan 2016/03/31
        [OperationContract]
        string GetServiceByCodeTRANS(string company, string userDefPro, string chgCd);
        //Nuwan 2016/03/31
        [OperationContract]
        string GetServiceByCodeMSCELNS(string company, string userDefPro, string chgCd);
        //Nuwan 2016/03/31
        [OperationContract]
        Int32 genarateCostngPurchaseOrder(List<MST_PR_HED_DET> hetdet, List<string> serpro, MasterAutoNumber _Auto, out string err, string company, string pc, string userid, string sessionid, Int32 statPo, Int32 statCost);
        //Nuwan 2016/04/05
        [OperationContract]
        List<MST_PR_HED_DET> getcostingforPurchaseOrder(string costNo, string com, string procen);
        //Nuwan 2016/04/05
        [OperationContract]
        List<String> getcostingSerProPurchaseOrder(string costNo);

        // subodana
        [OperationContract]
        List<InvoiceHeader> GetAllSalesHRDdata(string com, string procen, DateTime startdate, DateTime enddate);
        [OperationContract]
        List<RecieptHeader> GetAllRecieptHRDdata(string com, string procen, DateTime startdate, DateTime enddate);
        // subodana
        [OperationContract]
        List<MST_GNR_ACC> GetgrnalDetails(string com);

        // subodana
        [OperationContract]
        Int32 UPDATE_INV_HDRENGLOG(string invNo, Int32 value);
        // subodana
        [OperationContract]
        Int32 UPDATE_RECIEPT_HDRENGLOG(string recNo, Int32 value);
        //Nuwan 
        [OperationContract]
        List<MST_ST_PAX_DET> GetInvoicePaxDet(string invNo);
        //Nuwan 
        [OperationContract]
        List<GEN_CUST_ENQ> SP_TOUR_GET_TRANSPORT_ENQRY(string Com, string PC, String Status, string type, string UserID, Int32 PermissionCode);
        //Nuwan 
        [OperationContract]
        InvoiceHeader getInvoiceHederData(string invNo, string com, string procen);
        //Nuwan 2016/04/20
        [OperationContract]
        Int32 cancelInvoice(InvoiceHeader _invoiceHeder, RecieptHeader _recieptHeader, Int32 EnquiryStages, Int32 ToursStatus, out string _error);

        //Nuwan 2016/04/20
        [OperationContract]
        List<RecieptItem> getReceiptItemList(string invNo);
        //Nuwan 2016/04/22
        [OperationContract]
        MST_CHKINOUT getEnqChkData(string enqId);
        //Nuwan 2016/04/22
        [OperationContract]
        Int32 saveCheckoutDetails(MST_CHKINOUT check, out string _error);
        //Nuwan 2016/04/25
        [OperationContract]
        List<MST_TEMP_MESSAGES> getTempSmsMessage(string company, string pc, string msgtyp);
        //Nuwan 2016/05/02
        [OperationContract]
        List<GEN_CUST_ENQ> getAllEnquiryData(string enqId, string Com, string PC, String Status, string UserID, Int32 PermissionCode);
        //Nuwan 2016/05/04
        [OperationContract]
        List<QUO_COST_HDR> GET_INVOICEDCOST_PRFITABILITY_DETAILS(string _com, string _procenter, string _req, string _costRef, string _customer, string _category,
        DateTime _fromdate, DateTime _todate);

        //Nuwan 2016/05/04
        [OperationContract]
        List<QUO_COST_HDR> GET_INVOICEDCOST_PRFITABILITY(string _com, string _procenter, string _req, string _costRef, string _customer, string _category,
            DateTime _fromdate, DateTime _todate);
        //Nuwan 2016/05/04
        [OperationContract]
        int SaveToursrInvoiceReverce(InvoiceHeader oHeader, List<InvoiceItem> oMainItemsList2, MasterAutoNumber _invoiceAuto, out string _invoiceNo, MasterBusinessEntity oCust, out string _error, List<MST_ST_PAX_DET> paxDetList, InvoiceHeader _oldInvoiceHeader = null);
        //Nuwan 2016/05/05
        [OperationContract]
        List<MST_COUNTRY> getCountryDetails(string countryCd);
        //Nuwan 2016/05/05
        [OperationContract]
        List<MST_CUSTOMER_TYPE> getCustomerTypes();
        //Nuwan 2016/05/07
        [OperationContract]
        List<RecieptItemTBS> getReceiptItemByinvNo(string invNo, string com, string pc);


        //subodana2016/05/09
        [OperationContract]
        List<RecieptHeader> GET_RECIEPT_BY_ENQ(string company, string pc, string ENQID);
        //Nuwan 2016.05.11
        [OperationContract]
        List<ST_MENU> getUserMenu(string userId);

        //Nuwan 2016.05.11
        [OperationContract]
        ST_MENU getAcccessPermission(string userId, Int32 menuId);

        //Nuwan 2016.05.12
        [OperationContract]
        MST_PCADDPARA getPcAdditionalPara(string com, string pc, string code);

        //Nuwan 2016.05.14
        [OperationContract]
        List<RecieptHeaderTBS> getReceiptItems(string company, string pc, string type, string enqId);

        //Nuwan 2016.05.17
        [OperationContract]
        List<ST_SATIS_QUEST> getFeedBackQuestions(string channel, string company, string userDefPro);

        //Nuwan 2016.05.17
        [OperationContract]
        List<ST_SATIS_QUEST> getFeedBackQuestionsOnly(string channel, string company, string userDefPro);

        //Nuwan 2016.05.17
        [OperationContract]
        Int32 SaveCustomerFeedBack(List<ST_SATIS_RESULT> resultData, out string error);
        //Nuwan 2016.5.18
        [OperationContract]
        List<ST_SATIS_RESULT> getCustermerFeedData(string channel, string company, string userDefPro, string enqId);

        //Nuwan 2016.05.25
        [OperationContract]
        DataTable GetReceipt(string _doc);
        //Nuwan 2016.05.25
        [OperationContract]
        List<MST_FAC_LOC> getFacLocations(string company, string pc);

        //Nuwan 2016-05-30
        [OperationContract]
        Int32 SaveToursrInvoiceTransport(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, List<InvoiceVoucher> _voucher, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, bool isTranInvoice, GEN_CUST_ENQSER _genCustEnqser = null, GEN_CUST_ENQ oItem = null, MasterAutoNumber _enqAuto = null, List<MST_ST_PAX_DET> paxDetList = null, bool Tours = true, MasterBusinessEntity _custProfile = null, GroupBussinessEntity _custGroup = null, List<ST_ENQ_CHARGES> enqChrgItems = null, List<GEN_CUS_ENQ_DRIVER> driverList = null, List<GEN_CUS_ENQ_FLEET> vehicleList = null);
        //Nuwan 2016-05-31
        [OperationContract]
        GEN_CUST_ENQ getEnquiryDetailsTours(string Com, string PC, string ENQID);

        //Nuwan 2016-06-02
        [OperationContract]
        List<ST_AIRTCKT_TYPS> getAirTicketTypes();
        //Nuwan 2016-06-02
        [OperationContract]
        List<ST_VEHICLE_TP> getVehicleTypes();
        //Nuwan 2016-06-02
        [OperationContract]
        List<ST_PKG_TP> getpKGTypes();
        //Nuwan 2016.6.03
        [OperationContract]
        List<RecieptHeaderTBS> getOtherPartyReceipts(string dateFrom, string dateTo, string OthCus, string Cus, string company, string pc);
        //Nuwan 2016.06.06
        [OperationContract]
        Int32 saveOtherPartyPayments(RecieptHeaderTBS receiptHeaderTBS, List<RecieptHeaderTBS> existsReceiptItems, List<RecieptItemTBS> RecieptItemList, MasterAutoNumber masterAuto, out string docno);
        //Nuwan 2016.06.16
        [OperationContract]
        List<ST_ENQ_CHARGES> tempEnquiryCharges(string ENQID);
        //Nuwan 201.06.29
        [OperationContract]
        List<mst_fleet_driver> getAlowcatedFleetAndDriverDetails(string driver, string fleet, DateTime frmDt, DateTime toDt, string fletordri);
        //Nuwan 2016.06.29
        [OperationContract]
        List<GEN_CUS_ENQ_DRIVER> getAlowcatedFleetAndDriverDetailsInEnquiry(string driver, DateTime frmDt, DateTime toDt);
        //Nuwan 2016.07.18
        [OperationContract]
        List<GEN_CUS_ENQ_DRIVER> getEnquiryDriverDetails(string enqId);
        //Nuwan 2016.07.20
        [OperationContract]
        List<GEN_CUS_ENQ_FLEET> getAlowcatedFleetDetailsInEnquiry(string fleet, DateTime frmDt, DateTime toDt);
        //Nuwan 2016.07.18
        [OperationContract]
        List<GEN_CUS_ENQ_FLEET> getEnquiryFleetDetails(string enqId);
        //Nuwan 2016.07.22
        [OperationContract]
        DataTable getEnquiryHeaderData(string enqNo, string pc);
        //Nuwan 2016.07.22
        [OperationContract]
        DataTable getEnquiryInvoiceItems(string enqNo);
        //Nuwan 2016.11.07
        [OperationContract]
        DEPO_AMT_DATA getLiabilityDetails(string liability);
        //Nuwan 2016.11.07
        [OperationContract]
        DEPO_AMT_DATA getLiabilityDatabyChgCd(string chgCd);

        //2016-12-19 subodana
        [OperationContract]
        DataTable GetAgreementParameters(Int32 agrrno);

        //2017-01-06 subodana
        [OperationContract]
        Int32 SaveBusChargeCode(List<Cus_chg_cds> chg,string userid, out string _error);

        //2017-01-06 subodana
        [OperationContract]
        List<Cus_chg_cds> GETBUSCHARGECODES(string cuscd);

        //2017-01-11 subodana
        [OperationContract]
        List<DriverAllocationHome> GET_TOURS_DRIALLOC(string company, string userDefPro, string pgeNum, string pgeSize, string searchFld, string searchVal, String Status);

        //2017-01-13 SUBODANA
        [OperationContract]
        List<FleetAlert> FleetAlertdata(DateTime DATE, string TYPE);

        //2017-01-19 SUBODANA
        [OperationContract]
        DataTable GETINVCHARGES(string INVNO);

        //2017-01-20 SUBODANA
        [OperationContract]
        DataTable GETFLEETGES(string fleet, DateTime fdate, DateTime tdate);
             //2017-01-23 SUBODANA
        [OperationContract]
       int SaveCreditNote(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, MasterAutoNumber _invoiceAuto, out  string _invoiceNo, MasterBusinessEntity _businessCompany, out string _errorlist, List<MST_ST_PAX_DET> paxDetList = null, InvoiceHeader _oldInvoiceHeader = null);
        //subodana
        [OperationContract]
        int Isinvoiced(string com, string enqid);

        //subodana
        [OperationContract]
        DataTable CHECK_DRIVER_ALLOC(string fleet, DateTime fdate, DateTime tdate);

        // ISURU 2017/02/21
        
        [OperationContract] 
        DataTable GET_PRINT_DATA(string id,string company);

        // ISURU 2017/02/22

        [OperationContract]
        DataTable GET_TRIPREQUEST_DATA(DateTime fromdate, DateTime todate, string company, string profcen);
    
        // ISURU 2017/02/24
        
        [OperationContract]
        DataTable GET_LOGSHEET_DATA(DateTime fromdate, DateTime todate, string company, string profcen);

        // ISURU 2017/02/27

        [OperationContract]
        DataTable HOME_CONFIG_DATA(string user, string company, string profcen , string type);

        // ISURU 2017/02/27

        [OperationContract]
        DataTable LEASED_CAR_DATA(DateTime fromdate, DateTime todate, string com, string fleet);

        //2017-02-28 ISURU
        [OperationContract]
        List<FleetAllocDaily> FleetAllocDailydata(string com, DateTime fdate, DateTime todate, string prc);
        [OperationContract]
        Int32 SaveToursrInvoiceDBNT(InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, InventoryHeader _inventoryHeader, List<ReptPickSerials> _pickSerial, List<ReptPickSerialsSub> _pickSubSerial, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, MasterAutoNumber _inventoryAuto, bool _isDeliveryNow, out  string _invoiceNo, out string _receiptNo, out string _deliveryOrder, MasterBusinessEntity _businessCompany, bool _isHold, bool _isHoldInvoiceProcess, out string _errorlist, List<InvoiceVoucher> _voucher, InventoryHeader _buybackheader, MasterAutoNumber _buybackauto, List<ReptPickSerials> _buybacklist, out string BuyBackInvNo, bool isTranInvoice, GEN_CUST_ENQSER _genCustEnqser = null, GEN_CUST_ENQ oItem = null, MasterAutoNumber _enqAuto = null, List<MST_ST_PAX_DET> paxDetList = null, bool Tours = true, MasterBusinessEntity cus = null, GroupBussinessEntity _custGroup = null, List<ST_ENQ_CHARGES> enqChrgItems = null);
    }
}