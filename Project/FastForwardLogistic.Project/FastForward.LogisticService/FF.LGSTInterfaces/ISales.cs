using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using FF.BusinessObjects.Genaral;
using FF.BusinessObjects;
using System.Data;
using System.Web;
using FF.BusinessObjects.Sales;
using FF.BusinessObjects.Security;
using FF.BusinessObjects.Search;

namespace FF.Interfaces
{
    [ServiceContract]
    public interface ISales
    {
        //Isuru 2017/05/29
        [OperationContract]
        String SaveCustomerDetails(cus_details cust, string _IS_TAX, string _ACT, string _IS_SVAT, string _TAX_EX, string _IS_SUSPEND, string _AGRE_SEND_SMS, string _AGRE_SEND_EMAIL, string userId, string userDefPro, string company, string loc, out string err);

        //Dilshan 2017/08/30
        [OperationContract]
        Int32 UpdateCustomerDetails(cus_details cust, string _IS_TAX, string _ACT, string _IS_SVAT, string _TAX_EX, string _IS_SUSPEND, string _AGRE_SEND_SMS, string _AGRE_SEND_EMAIL, string userId, string userDefPro, string company, string loc, out string err);

        [OperationContract]
        Int32 SaveEmployeeData(MST_EMP mst_employee_tbs, string userId, string userDefPro, DateTime serverDatetime, string driverCom, out string err);
        //Dilshan 2017/09/04
        [OperationContract]
        Int32 UpdateEmployeeData(MST_EMP mst_employee_tbs, string userId, string userDefPro, DateTime serverDatetime, string driverCom, out string err);

        //Dilshan 2017/09/06
        [OperationContract]
        Int32 SaveVessalData(MST_VESSEL mst_vessal_tbs, string userId, string userDefPro, DateTime serverDatetime, out string err);
        //Dilshan 2017/09/06
        [OperationContract]
        Int32 UpdateVessalData(MST_VESSEL mst_vessal_tbs, string userId, string userDefPro, DateTime serverDatetime, out string err);

        //Dilshan 2017/09/09
        [OperationContract]
        Int32 SaveCostEleData(MST_VESSEL mst_vessal_tbs, string userId, string userDefPro, DateTime serverDatetime, out string err);
        //Dilshan 2017/09/09
        [OperationContract]
        Int32 UpdateCostEleData(MST_VESSEL mst_vessal_tbs, string userId, string userDefPro, DateTime serverDatetime, out string err);

        //Dilshan 2017/09/09
        [OperationContract]
        Int32 SavePortData(MST_VESSEL mst_vessal_tbs, string userId, string userDefPro, DateTime serverDatetime, out string err);
        //Dilshan 2017/09/09
        [OperationContract]
        Int32 UpdatePortData(MST_VESSEL mst_vessal_tbs, string userId, string userDefPro, DateTime serverDatetime, out string err);

        // Isuru 2017/05/29
        //[OperationContract]
        //List<cus_details> CustomerSearchAll(string _com, string _nic, string _mob, string _br, string _pp, string _dl, int _type);
        //nuwan 2017.06.29
        [OperationContract]
        List<MST_REQ_TYPE> getReqyestTypes(string module, out string error);
        [OperationContract]
        MST_PROFIT_CENTER getProfitCenterDetails(string pccd, string com, string userId);
        [OperationContract]
        MST_EMP getEmployeeDetails(string epf, string com);
        [OperationContract]
        MST_EMP getReqEmployeeDetails(string epf, string com);
        [OperationContract]
        MST_BUSENTITY getConsigneeDetailsByAccCode(string cuscd, string company, string type);
        [OperationContract]
        TRN_PETTYCASH_REQ_HDR getReqyestDetials(string type, string reqno, string company, string userDefPro, out string error);

        [OperationContract]
        List<TRN_PETTYCASH_REQ_DTL> getReqyestItemDetials(Int32 seq, out string error);

        [OperationContract]
        trn_jb_hdr GetJobDetails(string jobno, string company, out string error);
        [OperationContract]
        MST_COST_ELEMENT GetCostElementDetails(string eleCode, out string error);
        [OperationContract]
        FTW_MES_TP GetUOMDetails(string uomcd, out string error);
        [OperationContract]
        MST_COM getCompanyDetails(string company, out string error);
        [OperationContract]
        MST_CUR GetCurrencyDetails(string curcd, out string error);
        [OperationContract]
        FTW_VEHICLE_NO getTelVehLcDet(string code, out string error);
        [OperationContract]
        MasterProfitCenter GetProfitCenter(string _company, string _profitCenter);
        [OperationContract]
        MasterCompany GetUserCompanySet(string _company, string _profitCenter);
        [OperationContract]
        MasterExchangeRate GetExchangeRate(string _com, string _fromCur, DateTime _date, string _toCur, string _pc);
        [OperationContract]
        Int32 savePetttyCashRequest(TRN_PETTYCASH_REQ_HDR hdr, List<TRN_PETTYCASH_REQ_DTL> reqDet, MasterAutoNumber _ptyAuto, out string error);
        [OperationContract]
        Int32 updateRequestApproveStus(TRN_PETTYCASH_REQ_HDR request, Int32 user, out string error);
        [OperationContract]
        Int32 updateReprintDocStus_New(string request, string company, out string error);
        [OperationContract]
        Int32 updateReprintDocStus(string request, string userId, out string error);
        [OperationContract]
        List<TRN_PETTYCASH_REQ_HDR> loadPendingptyReq(string company, string pc, DateTime fromdt, DateTime tdt, Int32 applvl, out string error);
        [OperationContract]
        TRN_PETTYCASH_REQ_HDR loadRequestDetailsbySeq(Int32 seq, string company, string userDefPro, out string error);
        [OperationContract]
        DataTable getPetyCshRptData(Int32 reqSeq, string type, out string error);
        [OperationContract]
        DataTable getCompanyDetailsBycd(string company);
        [OperationContract]
        Int32 rejectPtyCshRequest(Int32 sqNo, string userId, DateTime dt, string sessionid, out string error);
        [OperationContract]
        List<TRN_PETTYCASH_REQ_HDR> loadPendingSetReq(string company, string userDefPro, string type, DateTime fmdt, DateTime tdt, string jobno, out string error);
        [OperationContract]
        TRN_PETTYCASH_SETTLE_HDR loadSettlementHdr(string company, string userDefPro, string reqNo, out string error);
        [OperationContract]
        List<TRN_PETTYCASH_SETTLE_DTL> loadSettlementDet(string company, string pc, string reqNo, Int32 reqSeq, out string error);
        [OperationContract]
        Int32 saveSetlementDetails(TRN_PETTYCASH_SETTLE_HDR hdr, List<TRN_PETTYCASH_SETTLE_DTL> setDets, MasterAutoNumber _ptyAuto, string sessionid, out string error);
        [OperationContract]
        Int32 updateSetlementApproveStus(TRN_PETTYCASH_SETTLE_HDR request, Int32 appl, out string error);
        [OperationContract]
        Int32 rejectSettlementRequest(string reqno, string userId, DateTime date, string sessionid, List<TRN_PETTYCASH_SETTLE_DTL> setDets, out string error);
        [OperationContract]
        List<PaymentType> GetPossiblePaymentTypes_new(string _com, string _schnl, string _pc, string txn_tp, DateTime today, Int32 _isBOCN);
        [OperationContract]
        string getBankCode(string bankId);
        [OperationContract]
        DataTable get_bank_mid_code(string branch_code, string pc, int mode, int period, DateTime _trdate, string _com);
        [OperationContract]
        MasterOutsideParty GetOutSidePartyDetailsById(string _code);
        [OperationContract]
        DataTable GetBankCC(string _bank);
        [OperationContract]
        string getAdvanceRefAmount(string cuscd, string company, string receiptno);
        [OperationContract]
        string getCreditRefAmount(string cuscd, string company, string refNo, string profcen);
        [OperationContract]
        List<GiftVoucherPages> GetGiftVoucherPages(string _com, int _page);
        [OperationContract]
        DataTable GetGVAlwCom(string _comCode, string _itm, Int32 _act);
        [OperationContract]
        LoyaltyMemeber getLoyaltyDetails(string customer, string loyalNu);
        [OperationContract]
        LoyaltyPointRedeemDefinition GetLoyaltyRedeemDefinition(string prtTp, string prt, DateTime date, string loyalty);
        [OperationContract]
        BlackListCustomers GetBlackListCustomerDetails(string _cus, Int32 _active);
        [OperationContract]
        DataTable get_Dep_Bank_Name(string _com, string _pc, string _paytp, string _acc);
        [OperationContract]
        GiftVoucherPages getGiftVoucherPage(string voucherNo, string voucherBook, string company);
        [OperationContract]
        MasterItem GetItem(string _company, string _item);
        [OperationContract]
        GiftVoucherPages GetGiftVoucherPage(string _com, string _pc, string _item, int _book, int _page, string _prefix);
        [OperationContract]
        DataTable GetReceipt(string _doc);
        [OperationContract]
        Boolean validateBank_and_Branch(string bus_cd, string branch_cd, string _type);
        [OperationContract]
        DataTable PettyCash_SettlementDetls(Int32 reqSeq, string comCode, out string error);
        [OperationContract]
        TRN_PETTYCASH_SETTLE_HDR load_PtyC_Setl_RequestDetails(Int32 seq, string company, string userDefPro, out string error);
        [OperationContract]
        Int32 updatePetttyCashRequest(TRN_PETTYCASH_REQ_HDR request, List<TRN_PETTYCASH_REQ_DTL> reqDet, out string error);
        [OperationContract]
        string requestTypeDesc(Int32 seq);
        [OperationContract]
        DataTable Inv_Details(string InvNo, string company, string type, out string error);
        [OperationContract]
        MasterReceiptDivision GetDefRecDivision(string _com, string _pc);
        [OperationContract]
        Int32 SaveJobInvoice(trn_inv_hdr _hdr, List<trn_inv_det> _job_inv_det, RecieptHeader _rec, List<RecieptItem> _rec_itm, MasterAutoNumber _masterinvauto, MasterAutoNumber _masterrecauto, out string err);
        [OperationContract]
        List<trn_inv_hdr> GetInvHdr(string doc, string com);
        [OperationContract]
        List<trn_inv_det> Get_Inv_det(string seq);

        [OperationContract]
        List<trn_inv_det> Get_Inv_detApp(string seq);
        //Udaya 26.07.2017 Get Data for Manifest Letter
        [OperationContract]
        DataTable GetManifestLetter_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode);
        //Udaya 26.07.2017 Get Data for Cargo Manifest
        [OperationContract]
        DataTable Get_CargoManifest_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode);
        //Udaya 27.07.2017 Get Data for Delivary Order
        [OperationContract]
        DataTable Get_Container_Dtl(string docNo);
        //dilshan
        [OperationContract]
        DataTable Get_Container_Dtlcount(string docNo);
        //Udaya 27.07.2017 Get Data for Delivary Order
        [OperationContract]
        DataTable Get_DeliveryOrder_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode);
        [OperationContract]
        bool IsValidReceiptType(string _company, string _type);
        [OperationContract]
        Int32 SaveJobReciept(RecieptHeader _rec, List<RecieptItem> _rec_itm, MasterAutoNumber _masterrecauto, out string err);
        [OperationContract]
        RecieptHeader GetReceiptHeader(string _com, string _pc, string _doc);
        [OperationContract]
        List<RecieptItem> GetReceiptDetails(RecieptItem _paramRecDetails);
        //Udaya 28.07.2017 Get Data for Draft
        [OperationContract]
        DataTable Get_Draft_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode);
        //Udaya 28.07.2017 Get Data for House rpt (Air Wise Bill Report)
        [OperationContract]
        DataTable Get_House_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode);
        //Udaya 31.07.2017 Get Data for Sales rpt
        [OperationContract]
        DataTable Get_Sales_Dtl(DateTime frmDate, DateTime toDate, string docNo, string comCode);
        [OperationContract]
        Int32 SaveJobCreditNote(trn_inv_hdr _hdr, List<trn_inv_det> _job_inv_det, MasterAutoNumber _masterinvauto, out string err);
        [OperationContract]
        Int32 SaveJobDebitNote(trn_inv_hdr _hdr, List<trn_inv_det> _job_inv_det, MasterAutoNumber _masterinvauto, out string err);
        [OperationContract]
        DataTable GetContainerType();
        //Udaya 01.08.2017 Get Data for Debtors Outstanding rpt
        [OperationContract]
        DataTable Get_Debtors_Outstanding(DateTime frmDate, DateTime toDate, string cusCode, string comCode, string proCntCode, string userId);
        //DILSHAN
        [OperationContract]
        DataTable Get_Debtors_Out(DateTime frmDate, DateTime toDate, string cusCode, string comCode, string proCntCode, string userId);
        //DILSHAN
        [OperationContract]
        DataTable Get_Debtors_Out_Summ(DateTime frmDate, DateTime toDate, string cusCode, string comCode, string proCntCode, string userId);
        [OperationContract]
        List<RecieptItem> GetReceiptDetailsWithinvno(string _invoiceno);
        //Udaya 02.08.2017 Get Data for Invoice Enquiry
        [OperationContract]
        List<trn_inv_hdr> GetInvHdr_Dtls(string JobNo, string modOfShpmnt, string typOfShpmnt, string cusCode, string hbl, string comCode);
        [OperationContract]
        DataTable GetSunPC(string type, string com);
        //Udaya 05.08.2017 Get Data for Payment Voucher Details Enquiry
        [OperationContract]
        List<TRN_PETTYCASH_REQ_DTL> GetVou_Dtls(string ReqNo, string SeqNo);
        //Udaya 05.08.2017 Get Data for Payment Voucher Header Enquiry
        [OperationContract]
        List<TRN_PETTYCASH_REQ_HDR> GetVou_Hdr(DateTime frmDate, DateTime toDate, string reqNo, string jobNo, string manRefNo, string proCnt);
        [OperationContract]
        List<SUN_JURNAL> GetSunJurnalnew(String com);
        [OperationContract]
        List<SUNINVHDR> GetSunInvdatanew(String Com, string pc, DateTime sdate, DateTime edate);
        [OperationContract]
        string GetAccountCodeByTp(string _company, string _type);
        [OperationContract]
        int UpdateInvoiceStatus(string invno, string com, string status, string user);
        [OperationContract]
        List<TRN_JOB_COST> GetJobCostData(String jobno);
        [OperationContract]
        List<TRN_JOB_COST> GetJobCostData_New(String jobno, String com, String pc);
        [OperationContract]
        int GetJobCostSavedData(String jobno, String com, String pc);      
        [OperationContract]
        int SaveJobClose(string jobno, string remark, string user, DateTime date, List<TRN_JOB_COST> _cost, out string err);
        //dilshan on 26/10/2018
        [OperationContract]
        int SaveJobCosting(string com,string jobno, string remark, string user, DateTime date, List<TRN_JOB_COST> _cost, List<TRN_JOB_COST> _rev, out string err);
        //dilshan on 26/10/2018
        [OperationContract]
        int ApproveJobCosting(string jobno, string remark, string user, DateTime date, List<TRN_JOB_COST> _cost,List<TRN_JOB_COST> _rev,int x, out string err);
        //dilshan
        [OperationContract]
        int SaveAutoJobClose(string jobno, string remark, string user, DateTime date, List<TRN_JOB_COST> _cost, out string err);        
        //dilshan
        [OperationContract]
        int ReopenJobClose(string jobno, string remark, string user, DateTime date, out string err);
        [OperationContract]
        List<SUNRECIEPTHDR> GetSunRecieptdatanew(String Com, string pc, DateTime sdate, DateTime edate, string type);
        [OperationContract]
        List<PetyCashUpload> GetSunPetyCash(String Com, DateTime sdate, DateTime edate);
        [OperationContract]
        int CheckJobUse(string job);
        [OperationContract]
        Int32 updateSetlementDetails(TRN_PETTYCASH_SETTLE_HDR hdr, List<TRN_PETTYCASH_SETTLE_DTL> oldsetDets, List<TRN_PETTYCASH_SETTLE_DTL> setDets, Int32 setSeq, string sessionid, out string error);

        [OperationContract]
        Int32 updateSetlementDetailsRefund(TRN_PETTYCASH_SETTLE_HDR hdr, List<TRN_PETTYCASH_SETTLE_DTL> oldsetDets, List<TRN_PETTYCASH_SETTLE_DTL> setDets, Int32 setSeq, string sessionid, out string error);
        //Created By Udaya 09.09.2017 Settelement Summary rpt
        [OperationContract]
        DataTable PettyCash_SettlementDetls_Summ(Int32 reqSeq, string comCode, out string error);
        [OperationContract]
        TRN_PETTYCASH_SETTLE_HDR load_PtyC_Setl_RequestDtl_Validate(Int32 seq, string company, string userDefPro, out string error);
        [OperationContract]
        TRN_PETTYCASH_REQ_HDR loadRequestDetailsbySeq_val(Int32 seq, string company, string userDefPro, out string error);
        // Added by Chathura on 15-sep-2017
        [OperationContract]
        List<SystemUserChannel> GetUserChannels(string UserID, string Comp);

        // Added by Chathura on 15-sep-2017
        [OperationContract]
        List<SystemUserProf> GetUserProfCenters(string UserID, string Comp, string User_def_chnl);
        // Added by Chathura on 21-sep-2017
        [OperationContract]
        Int32 CancelJobReciept(string receiptNo, string type, out string error);
        // Added by Chathura on 3-oct-2017
        [OperationContract]
        Int32 setInvoicePrintedStatus(string Inv, string company);

        // Added by Chathura on 3-oct-2017
        [OperationContract]
        List<TRN_PETTYCASH_SETTLE_DTL> LoadAllRefundableJobData(string jobno);
        [OperationContract]
        Int32 saveSetlementDetailsRefund(TRN_PETTYCASH_SETTLE_HDR hdr, List<TRN_PETTYCASH_SETTLE_DTL> setDets, MasterAutoNumber _ptyAuto, string sessionid, out string error);

        [OperationContract]
        Int32 CancelRefund(string jobno);

        [OperationContract]
        List<TRN_PETTYCASH_SETTLE_DTL> CheckJobAlreadyHasRefunds(string jobno);

        [OperationContract]
        List<TRN_PETTYCASH_SETTLE_DTL> CheckJobFullyRefunded(string jobno);

        [OperationContract]
        List<TRN_PETTYCASH_SETTLE_DTL> CheckSettlementRejected(string jobno);

        [OperationContract]
        List<TRN_PETTYCASH_SETTLE_DTL> CheckSettlementApproved(string jobno);
        [OperationContract]
        trn_inv_hdr validateInvoiceNUmber(string company, string cus, string othpc, string pc);

        [OperationContract]
        List<JOB_NUM_SEARCH> JobOrPouchDetails(string code, string type, string company, string userId);
        [OperationContract]
        List<JOB_NUM_SEARCH> JobOrPouchCostDetails(string code, string type, string company, string userId);

        [OperationContract]
        List<TRN_JOB_COST> GetJobActualCostData(String jobno);
        [OperationContract]
        List<TRN_JOB_COST> GetJobActualCostData_New(String jobno, String com, String pc);
        [OperationContract]
        List<RecieptItem> GetReceiptDetailsNonAlocated(string receiptNo);
        [OperationContract]
        Int32 updateunalocatedReceipt(string recno, decimal setleamt, List<RecieptItem> newrecieptItem, RecieptHeader _ReceiptHeader, out string error);

        [OperationContract]
        List<mst_item_tax> GetElementWiseTaxDetails(String element, string channel, string company);

        [OperationContract]
        List<mst_item_tax> GetAllTaxDetails(string channel, string company);

        [OperationContract]
        List<MainServices> GetJobServiceCode(String jobno, string cusid, string company, string pc);

        [OperationContract]
        List<cus_details> GetCustomerTaxEx(string invparty);

        [OperationContract]
        List<InvoiceCom> GetSunUpRestrictStatus(string company, string userDefPro, DateTime invdate);

        [OperationContract]
        List<InvoiceCom> GetNumOfBackdates(string company, string userDefPro);

        [OperationContract]
        List<InvoiceCom> GetEtaEtdInvoiceDate(string hblnum, string pc);

        //Tharindu 2017-12-26
        [OperationContract]
        DataTable Get_Cash_Outstanding(DateTime frmDate, DateTime toDate, string comCode, string proCntCode);

        [OperationContract]
        DataTable Get_Job_Status_Detail(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string jobstatus);
        
        [OperationContract]
        DataTable Get_IRD_Detail(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string jobstatus);       

        //Tharindu 2017-12-29
        [OperationContract]
        DataTable Get_Collection_Summary(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string type, string paytype, string usertype);

        //Tharindu 2018-01-04
        [OperationContract]
        DataTable Get_jb_header_new(string comCode, string jobnum, string hbl);
        //Tharindu 2018-01-04
        [OperationContract]
        DataTable Get_jb_header(string comCode, string jobnum);
        //Tharindu 2018-01-04
        [OperationContract]
        DataTable GetJobCostingData(string jobno);
        //Dilshan 2018-11-06
        [OperationContract]
        DataTable GetJobCostingData_new(string jobno, string status);
        //Tharindu 2018-01-04
        [OperationContract]
        DataTable GetJobActualCostingData(string jobno);

        //Tharindu 2018-01-05
        [OperationContract]
        DataTable GetInvoiceAuditTrail(DateTime frmDate, DateTime toDate, string comCode, string pc, string cusid);
        //subodana
        [OperationContract]
        string GetEleAccount(string code, string costtype);

        //Tharindu 2018-01-10
        [OperationContract]
        DataTable GetRptReceiptDetails(string comCode, string pc, string receiptid);
        //Subodana 2018-01-12 
        [OperationContract]
        List<PetyCashUpload> GetSunPetyCashReq(String Type, DateTime sdate, DateTime edate, string com);
        //Subodana 2018-01-12 
        [OperationContract]
        List<PayReqUploads> GetPayCashReq(String Type, DateTime sdate, DateTime edate);

        //Tharindu 2018-01-12
        [OperationContract]
        DataTable GetrptContainerDetails(string comCode, string BLNo);

        //Tharindu 2018-01-12
        [OperationContract]
        DataTable GetfrightChargePayble(string comCode, string HouseblNo, string InvNo);
        //subodana 2018-01-24
        [OperationContract]
        Int32 SaveSunT4(SunLC _obsunlcdata);
        //subodana 2018-01-24
        [OperationContract]
        DataTable CheckSUNLC(string COM, string CAT, string CODE);
        //subodana 2018-01-24
        [OperationContract]
        Int32 UPDATE_RECIEPT_HDRENGLOG(string recNo, Int32 value, string com);
        //subodana 2018-01-24
         [OperationContract]
        Int32 UPDATE_INV_HDRENGLOG(string invNo, Int32 value, string com);
         //subodana 2018-01-24
        [OperationContract]
         Int32 UPDATE_PETTYREQ(string doc, Int32 value, string com);
        //subodana 2018-01-24
        [OperationContract]
        Int32 UPDATE_PETTYSETTL(string doc, Int32 value, string com);
        //subodana 2018-01-24
        [OperationContract]
        List<PetyCashUpload> GetSunPetyCashPaymentReq(String Type, DateTime sdate, DateTime edate);
        //subodana 2018-01-24
        [OperationContract]
        bool CheckNBTVAT(string com, string pc, string code);
        //subodana 2018-02-20
        [OperationContract]
        List<SUNINVHDR> GetSunInvdatanewRev(String Com, string pc, DateTime sdate, DateTime edate);
        //Dilshan
        [OperationContract]
        Int32 SaveSunAccountsAll(SunAccountall _sundata);
        //Dilshan 2018-04-19
        [OperationContract]
        Int32 UPDATE_JNALNUMBER( string com);
        //Dilshan 2018-04-19
        [OperationContract]
        DataTable Get_Sales_GPProduct(DateTime frmDate, DateTime toDate, string docNo, string comCode);
        [OperationContract]
        DataTable Get_Cost_Of_Sales(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId);
        [OperationContract]
        DataTable Get_Cost_Of_Sales_Hdr(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId);
        [OperationContract]
        DataTable Get_Cost_Of_Sales_Req(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId);
        [OperationContract]
        DataTable Get_GP_Closed_Job(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId);
        [OperationContract]
        DataTable Get_GP_Closed_Job_Cost(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId);
        //dilshan
        [OperationContract]
        DataTable Get_Pending_Adv(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string userId);
        //dilshan
        [OperationContract]
        DataTable Get_Sales_GPSales(DateTime frmDate, DateTime toDate, string docNo, string comCode);
        //dilshan
        [OperationContract]
        DataTable GetBusinessEntityAllValues(string category, string type_);
        [OperationContract]
        List<CUSTOMER_SALES> getCustomerDetails(string selectedcompany, string Channel, string Subchnl, string Area, string Region, string Zone, string pc, DateTime SalesFrom, DateTime SalesTo, string Brand, string MainCat, string txtModel, string txtItem, decimal CheckAmount, string filterby, string cat2, string cat3, Int32 visit, string age, string salary, string customer, string invtype, string schemetype, string schemecode, string CTown, string PTown, string BankCode, string Withserial, string Paymenttype, string user, string dist, string prov);
        [OperationContract]
        List<CUSTOMER_SALES> getCustomerDetails_new(string town, string selectedcompany, string mode, string procenter, string district, string province, string dist, string prov, DateTime SalesFrom, DateTime SalesTo, decimal CheckAmount, string CheckAge, string CheckSalary);
        [OperationContract]
        List<trn_inv_hdr> GetInvHdrAct(string doc, string com);
        [OperationContract]
        Int32 saveSetlementDetailsAllocate(string Sett_no, string userid, DateTime credt, string sessionid, out string error);
        //Dilan 17-Jan-2019
        [OperationContract]
        DataTable Get_Job_Invoice_Detail(DateTime frmDate, DateTime toDate, string comCode, string proCntCode, string jobstatus);
        [OperationContract]
        DataTable GetJobStatusForSun(string _company, string _type, string _no);
        [OperationContract]
        DataTable GetFwdAccForSun(string _company, string _type, string _pc, string _doctype);
    }
}
