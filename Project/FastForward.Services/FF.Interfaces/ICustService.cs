using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using FF.BusinessObjects;
using FF.BusinessObjects.CustService;
using FF.BusinessObjects.Services;
using FF.BusinessObjects.TempObject;
using FF.BusinessObjects.General;


namespace FF.Interfaces
{
    [ServiceContract]
    public interface ICustService
    {
        //Chamal 26-Sep-2014
        [OperationContract]
        int Sample01(string _com, string _pc);
        //kapila
        [OperationContract]
        DataTable Get_oldpart_byjob(string _com, string _job);

        //kapila
        [OperationContract]
        string InsuranceForServiceReport(string _com,  DateTime _from, DateTime _to, string _user, out string _err);
        //kapila
        [OperationContract]
        List<Service_Purchase_Approval> GetServicePOApp(string _job, Int32 _line);
        //kapila
        [OperationContract]
        DataTable getServicejobDetBySer(string _ser);
        //kapila
        [OperationContract]
        DataTable get_receipt_byjobno(string _com, string _jobno);
        //kapila
        [OperationContract]
        DataTable Get_Job_Item_grade_Val(string _com, DateTime _date, string _item);
        [OperationContract]
        Boolean Is_EstimateAvailable(string _jobno);
        //kapila
        [OperationContract]
        Int32 Cancel_Gate_Pass(string _com, string _jobno, DateTime _jobdate, string _rccno, string _rccLoca, string _serLoca, string _user, out string _err);

        //kapila
        [OperationContract]
        DataTable get_gatepass_byjob(string _job, DateTime _date);
        //kapila
        [OperationContract]
        DataTable sp_get_pcbyloc_details(string _com, string _loc);

        //kapila
        [OperationContract]
        DataTable get_SCV_CLS_ALW_LOC(string _com, string _loc, string _type, Int32 _isDef, string _alwloc);

        //Sanjeewa
        [OperationContract]
        DataTable get_JobHeader(string _job);

        //kapila
        [OperationContract]
        List<InvoiceItem> GetSCMInvDetails(string _invoice, string _itm, string _ser);
        //kapila
        [OperationContract]
        string GetServiceSupplierWarranty_Excel(string _com, string _loc, string _supp, string _cat1, string _cat2, string _cat3, string _model, string _brand, string _item, DateTime _fdate, DateTime _tdate, string _doctp, string _user, out string _err);
        //kapila
        [OperationContract]
        Int32 UpdateAcceptedPendJobs(List<Service_Job_StageLog> oMainList);
        //Sanjeewa
        [OperationContract]
        Int32 UpdateAgreementSession(string _agreeNo, int _agreeline, int _agreeSession, string _ReqNo);

        //kapila
        [OperationContract]
        DataTable GetAcceptPendingJobs(string _com, string _pc, string _ser, string _job, string _tech, string _chnl, Int32 _istech, decimal _stage);

        //kapila
        [OperationContract]
        DataTable GetPendingAcceptanceStatus(string _com, string _loc, string _cat);

        [OperationContract]
        DataTable getTransportMethod();

        //kapila
        [OperationContract]
        DataTable getNotAllocatedJobs(string _com, string _loc, DateTime _fdate, DateTime _tdate);

        //kapila
        [OperationContract]
        DataTable getAllocatedPendingJobs(string _com, string _empCd, Int32 _istech, string _chnl, DateTime _fdate, DateTime _tdate);

        //kapila
        [OperationContract]
        DataTable GET_TEMPISSUE_By_SER(string Com, string loc, string Item, string ser, string Type);

        //kapila
        [OperationContract]
        Int32 UPD_TMP_ISSUE_RET(List<Service_TempIssue> oMainList);

        //kapila
        [OperationContract]
        Boolean IsWarReplaceFound(string _jobno);

        [OperationContract]
        Boolean IsWarReplaceFound_Exchnge(string _jobno);


        //kapila
        [OperationContract]
        DataTable getSCVAGRITM_bySer(string _ser);

        //kapila
        [OperationContract]
        DataTable GetSCVReqData(string reqNo, string com);

        //kapila
        [OperationContract]
        Int32 upd_brnd_man_alloc(string _com, string _man);

        //kapila
        [OperationContract]
        DataTable getBrandMgrAlloc(string _com, string _man);

        //kapila
        [OperationContract]
        Int32 DeleteCustFeedback(String job, Int32 Line);

        //kapila
        [OperationContract]
        DataTable getCreditNote4PendingSRN(string _com, DateTime _from, DateTime _to, string _delLoc, string _custCd, string _invno, Int32 _isDelAny);

        //kapila
        [OperationContract]
        DataTable getAgrNo4ReqGen(string _com, string _pc, DateTime _from, DateTime _to, string _agrNo);

        // Nadeeka 28-07-2015
        [OperationContract]
        DataTable getAgrNoInv(string _com, string _pc, DateTime _from, DateTime _to);

        // Nadeeka 28-07-2015
        [OperationContract]
        DataTable get_ServiceAgreement(string _com, string _pc, DateTime _fromdate, DateTime _todate);

        [OperationContract]
        DataTable get_ServiceIncentive(string _com, string _pc, DateTime _fromdate, DateTime _todate, string _reqcat);


        // Nadeeka 18-08-2015
        [OperationContract]
        DataTable geTechAllocationPending(string _com, string _userid);

        // Nadeeka 28-07-2015
        [OperationContract]
        List<scv_agr_payshed> getAgrPay(string _Agree);

        // Nadeeka 28-07-2015
        [OperationContract]
        List<scv_agr_cha> getAgrItem(string _Agree);

        //kapila
        [OperationContract]
        DataTable getAgreementItems(string _agrno);
        //kapila
        [OperationContract]
        DataTable getAgrDetailsGenReq(string _agrno, Int32 _line);

        //kapila
        [OperationContract]
        DataTable GetAgrClaimType(string _code);

        //kapila
        [OperationContract]
        DataTable GetAgrType(string _code);

        //Sanjeewa
        [OperationContract]
        DataTable getJobTechnician(string _jobno, string _com);

        //kapila
        [OperationContract]
        Int32 updateAgreement(string _agrno, string _stus, string _user);

        //kapila
        //[OperationContract]
        //Int32 Process_Service_Agreement(SCV_AGR_HDR oHeader, MasterAutoNumber _Auto, List<SCV_AGR_ITM> oAgrItems, List<SCV_AGR_SES> oAgrSes, List<SCV_AGR_COVER_ITM> oAgrCovItm, InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, List<InvoiceSerial> _invoiceSerial, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, out string GenAgr);

        //kapila
        [OperationContract]
        int DeleteSatisVal(Int32 _seq, Int32 _line);

        //kapila
        [OperationContract]
        DataTable sp_get_EstimateItem_det(string _EstNo);

        //kapila
        [OperationContract]
        DataTable sp_get_Estimate_det(string _com, string _EstNo);

        //kapila
        [OperationContract]
        DataTable PrintRepeatedJobs(string _com, string _pc, DateTime _from, DateTime _to, string _user, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3, string _ser, string _jobcat, Int32 _jobstus);

        //kapila
        [OperationContract]
        DataTable GetServiceJobDetailSubItemsData(string job, Int32 jobLine);

        //Sanjeewa
        [OperationContract]
        DataTable DefectAnalysisDetails(DateTime _from, DateTime _to, string _com, string _loc, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3, string in_defect, string in_Warrstatus, string _user);

        //kapila
        [OperationContract]
        DataTable GetPartRemoveByJobline(string Com, string job, Int32 jobline);

        //kapila
        [OperationContract]
        DataTable PrintTechComments(string _com, string _pc, DateTime _from, DateTime _to, string _user, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3, string _job, string _coment, string _jobcat);

        //Sanjeewa
        [OperationContract]
        DataTable JobSummaryDetails(DateTime _from, DateTime _to, string _com, string _loc, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string _technician, string _jobcat, string _itemtp, Int16 _jobstatus, Int16 _warrstatus, string _jobno, string _user);

        //Sanjeewa
        [OperationContract]
        string JobProcessTrackingDetails(DateTime _from, DateTime _to, string _com, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string _technician, string _jobcat, string _itemtp, decimal _jobstatus, string _warrstatus, string _jobno, string _user, string _option, int _export, string _origin, out string _err, int _chkw_cr_time, DateTime _create_from, DateTime _create_to);

        //Sanjeewa
        [OperationContract]
        string JobDetails(DateTime _from, DateTime _to, string _com, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string _technician, string _jobcat, string _itemtp, decimal _jobstatus, string _warrstatus, string _jobno, string _user, out string _err);

        //Sanjeewa
        [OperationContract]
        string AgreementDetailsReport(DateTime _from, DateTime _to, string _com, string _user, out string _err);

        //Sanjeewa
        [OperationContract]
        string JobTimeDetails(DateTime _from, DateTime _to, string _com, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string _user, string _defect, string _serial, out string _err);

        //Sanjeewa
        [OperationContract]
        DataTable JobProcessTrackingDetails1(DateTime _from, DateTime _to, string _com, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string _technician, string _jobcat, string _itemtp, decimal _jobstatus, string _warrstatus, string _jobno, string _user, string _option, string _loc, int _export, string _origin,
             int _chkw_cr_time, DateTime _create_from, DateTime _create_to);

        //Sanjeewa
        [OperationContract]
        DataTable SmartInsuClaimDetails(DateTime _from, DateTime _to, string _com, string _loc, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3);

        //Sanjeewa
        [OperationContract]
        DataTable BERLetterDetails(string _jobno, string _type);

        //Sanjeewa
        [OperationContract]
        DataTable JobProcesses();

        //kapila
        [OperationContract]
        Int32 Save_Supplier_Claim_Itm(List<Service_supp_claim_itm> _supClaimList, List<SCV_SUPP_CLAIM_REC> _lstamt, out string _msg);

        //kapila
        [OperationContract]
        DataTable GetTempIssueItemsByJobline(string Com, string job, Int32 jobline, string _type);

        //kapila
        [OperationContract]
        DataTable GetJobMRNByJobline(string Com, string job, Int32 jobline);

        //kapila
        [OperationContract]
        DataTable GetJobTaskByJobline(string Com, string job, Int32 jobline);

        //kapila
        [OperationContract]
        DataTable GetReqByJobline(string Com, string job, Int32 jobline);

        //kapila
        [OperationContract]
        DataTable GetReceiptByJobNo(string _job, Int32 _line);

        //kapila
        [OperationContract]
        DataTable GetJobServiceCharge(string _job, Int32 _line, string _cust);

        //kapila
        [OperationContract]
        Int32 Save_Cust_Satis(List<SCV_JOB_SATIS> _jobSatis, out string _err);

        //kapila
        [OperationContract]
        DataTable GetCustSatisReplyVal(string _com, string _chnl, Int32 _issms, string _job);

        //kapila
        [OperationContract]
        DataTable GetCustSatisByChnl(string _com, string _chnl, Int32 _issms);

        //kapila
        [OperationContract]
        Int32 UpdateCustomerQuest(Int32 ssq_seq, string ssq_com, string ssq_schnl, string ssq_quest, Int32 ssq_act, string ssq_cre_by, Int32 ssq_issms);

        //kapila
        [OperationContract]
        DataTable GetJobConfByJob(string _com, string _jobNo, string _confNo);

        //kapila
        [OperationContract]
        DataTable getSalesTypeByInvNo(string _invno);

        [OperationContract]
        Int32 UpdateCustomerQSatis(Int32 ssv_seq, Int32 ssv_line, string ssv_desc, string ssv_grade, Int32 ssv_act, string ssv_cre_by);

        //kapila
        [OperationContract]
        DataTable GetCustQuestData(Int32 _seq);

        //kapila
        [OperationContract]
        DataTable GetCustSatisfData(Int32 _seq, Int32 _line);

        //kapila
        [OperationContract]
        DataTable ReorderItemsPrint(string _com, string _user, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3, Int32 _withStores);


        //kapila
        [OperationContract]
        Int32 Update_SCV_Req_Hdr(string _reqno);

        //kapila
        [OperationContract]
        Boolean IsJobOpenReq(string _reqno);

        //KAPILA
        [OperationContract]
        Int32 Save_Req(Service_Req_Hdr _jobHdr, List<Service_Req_Det> _jobItems, List<Service_Req_Def> _jobDefList, List<Service_Req_Det_Sub> _jobDetSubList, MasterAutoNumber _recAuto, string _sbChnl, string _itemType, string _brand, Int32 _warStus, MasterAutoNumber _masterAuto, out string _err, out string _jNo, Int32 _isProcess, DateTime _fromDate, DateTime _toDate);


        //kapila
        [OperationContract]
        Int32 SAVE_SCV_JOBCUS_FEED(string _sjf_jobno, Int32 _sjf_jobline, DateTime _sjf_date, string _sjf_cuscd, string _sjf_feedback, string _sjf_cre_by, Int32 _sjf_jb_type, Int32 sjf_jb_stage, Int32 sjf_infm_all, Int32 sjf_infm_tech, Int32 sjf_is_sms,string com,string pc);

        //kapila
        [OperationContract]
        DataTable GetCustJobFeedback(string _jobno);

        //kapila
        [OperationContract]
        int GetScvAgreement(string _com, string _AgrNo, out SCV_AGR_HDR _agrHdr, out List<SCV_AGR_ITM> _agrDet, out List<SCV_AGR_SES> _agrSess, out List<scv_agr_cha> _agrCha, out List<scv_agr_payshed> _agrPayShed, out string _returnMsg);
        //kapila
        [OperationContract]
        int GetScvReq(string _com, string _jobNo, out Service_Req_Hdr _jobHdr, out List<Service_Req_Det> _jobDet, out List<Service_Req_Det_Sub> _jobDetSub, out List<Service_Req_Def> _jobDef, out string _returnMsg);

        //kapila
        [OperationContract]
        Service_Req_Hdr GetServiceReqHeader(string _com, string _reqNo);

        //kapila
        [OperationContract]
        DataTable sp_get_job_hdrby_jobno(string _jobNo);

        [OperationContract]
        DataTable sp_get_job_category(string _jobcat);

        //kapila
        [OperationContract]
        DataTable getServicejobDet(string _jobNo, Int32 _line);

        //kapila
        [OperationContract]
        DataTable getServicejobDef(string _jobNo, Int32 _line);

        //kapila
        [OperationContract]
        DataTable getServiceTempIssuItems(string _jobNo, Int32 _line, Int32 _visitline);

        //sanjeewa
        [OperationContract]
        DataTable sp_get_job_header(string _jobNo);

        //sanjeewa
        [OperationContract]
        DataTable get_profitcenter(string _com, string _pc);

        //sanjeewa
        [OperationContract]
        DataTable get_agree_header(string _agreeNo);

        //sanjeewa
        [OperationContract]
        DataTable get_agree_item(string _agreeNo);

        //sanjeewa
        [OperationContract]
        DataTable getServiceJobUser(string _UserID);

        //sanjeewa
        [OperationContract]
        DataTable get_agree_session(string _agreeNo);

        //sanjeewa
        [OperationContract]
        DataTable get_agree_charge(string _agreeNo);

        //sanjeewa
        [OperationContract]
        DataTable DFExchangeDetails(DateTime _from, DateTime _to, string _com, string _pc, int _isExport, string _reqtp);

        [OperationContract]
        string DFExchangeDetails1(DateTime _from, DateTime _to, string _com, int _isExport, string _reqtp, string _user, out string _err);

        //sanjeewa
        [OperationContract]
        DataTable sp_get_Report_info_chnl(string _report, string _channel);

        //sanjeewa
        [OperationContract]
        DataTable sp_get_job_details(string _jobNo, string _type);

        //sanjeewa
        [OperationContract]
        DataTable sp_get_job_detailsSub(string _jobNo);

        //sanjeewa
        [OperationContract]
        DataTable sp_get_job_defects(string _jobNo);

        //sanjeewa
        [OperationContract]
        DataTable sp_get_com_details(string _comcode);

        //sanjeewa
        [OperationContract]
        DataTable sp_get_loc_details(string _loccode);

        //sanjeewa
        [OperationContract]
        DataTable sp_get_locbypc_details(string _com, string _pc);
        
        //sanjeewa
        [OperationContract]
        DataTable sp_get_gatepass_details(string _gpNo);

        //sanjeewa
        [OperationContract]
        DataTable sp_get_gpOldpart_details(string _jobNo, Int32 _jobline);

        //sanjeewa
        [OperationContract]
        DataTable sp_get_Estimate_details(string _EstNo);

        //kapila
        [OperationContract]
        DataTable sp_get_Estimatejobs(string _com, string _pc, DateTime _from, DateTime _to, string _estTp, string _cust, string _tech);

        //sanjeewa
        [OperationContract]
        DataTable sp_get_EstimateItem_details(string _EstNo);

        //kapila
        [OperationContract]
        Int32 Save_Allocated_Employee(List<MasterServiceEmployee> lst_all_items, out string _err);

        //kapila
        [OperationContract]
        DataTable getItemComponentDet();

        //kapila
        [OperationContract]
        Int32 UpdateItemComponent(string _cat1, string _cat2, string _cat3, string _item, Int32 _qty, Int32 _war, Int32 _act, Int32 _isser, string _user);


        //kapila
        [OperationContract]
        DataTable getJobStageByJobNo(string JOb, string Com, Int32 _isSCM2);

        //kapila
        [OperationContract]
        Int32 Save_Allocated_Priority(List<scv_prit_task> lst_all_items, out string _err);

        //kapila
        [OperationContract]
        DataTable getPriorityDataByCode(string _code);

        //kapila
        [OperationContract]
        Int32 updatePriorityData(string _code, string _desc, Int32 _act, Int32 _def, string _color, string _user);

        //kapila
        [OperationContract]
        DataTable getPriorityData();

        //kapila
        [OperationContract]
        DataTable getChannelPara(string _com, string _code);

        //kapila
        [OperationContract]
        List<Service_Confirm_detail> GetServiceConfirmDet(string _jobNo, Int32 _line);

        //Nadeeka

        [OperationContract]
        DataTable get_supp_claim_amt(string _com, string _claimno);



        //Nadeeka

        [OperationContract]
        DataTable getWCNHeaderBrand(string _com, string _loc, DateTime _fdate, DateTime _todate, string _sts);

        //Nadeeka

        [OperationContract]
        List<Service_WCN_Hdr> getWCNHeader_basedRef(string _com, string _ref);



        //Nadeeka

        [OperationContract]
        DataTable getWCNTypes(string _type);

        //Nadeeka

        [OperationContract]
        Int32 SendConfirmationMail(string _com, string _pc, string _loc, string _jobNo, string _rccno, DateTime _date, String _remarks, string _subChanel, string _user);

        //Nadeeka

        [OperationContract]
        Int32 SendWarantyReplacementMail(string _com, string _pc, string _loc, string _jobNo, DateTime _date, String _remarks, string _subChanel, string _user, Int32 isComplete);

        //Nadeeka
        [OperationContract]
        List<Service_confirm_Header> GetConfDetByJobNo(string _com, string _jobNo, Int32 _jobLine);
        //Nadeeka
        [OperationContract]
        List<SCV_SUPP_CLAIM_REC> GetSuppWaraPayment(Int64 _seq);

        //Nadeeka
        [OperationContract]
        DataTable GetServiceSupplierWarranty(string _com, string _loc, string _supp, string _cat1, string _cat2, string _cat3, string _model, string _brand, string _item, DateTime _fdate, DateTime _tdate, string _doctp);

        //Nadeeka
        [OperationContract]
        List<Service_Confirm_detail> GetServiceConfirmDetJob(string _jobNo, string _conNo);

        //kapila
        [OperationContract]
        DataTable getCustSatisData(string _com, string _schnl, string _code);

        //kapila
        [OperationContract]
        Boolean IsJobLineInvoiced(string _jobno, Int32 _line);

        //kapila
        [OperationContract]
        Boolean checkFiedJob(string _jobno);

        //kapila
        [OperationContract]
        Boolean IsCustAllwGatePassWOutInv(string _com, string _code);

        //kapila
        [OperationContract]
        DataTable getSerialIDByDocument(string _doc, string _item, string _ser);

        //kapila
        [OperationContract]
        Int32 Process_Gate_Pass(List<Service_job_Det> _jobDetList, RCC _rcc, InventoryHeader _inventoryHeader, MasterAutoNumber _inventoryAuto, List<ReptPickSerials> _reptPickSerials, string _loc, DateTime _date, string _custSatis, string _custSatisRem, string _user, string _transMethod, string _ref, MasterAutoNumber _masterAuto, MasterAutoNumber _masterAutoDo, string _session, out string _err, out string _docNo);

        //kapila
        [OperationContract]
        DataTable InvoiceByJobLine(string _jobno, Int32 _line, string _item, string _stus);

        //kpila
        [OperationContract]
        DataTable GetEstiByJobline(string Com, string job, string _type);

        //kapila
        [OperationContract]
        DataTable GetRecByJobline(string Com, string job, Int32 jobline);
        //kapila
        [OperationContract]
        Int32 SaveRequestDetail(Service_Req_Det _serviceReqDet);

        //kapila
        [OperationContract]
        Int32 SaveRequestHeader(Service_Req_Hdr _serviceReqHdr);

        //kapila
        //Edit by Chamal
        //update by akila 2017/06/015
        [OperationContract]
        Int32 Save_Job(Service_JOB_HDR _jobHdr, List<Service_job_Det> _jobDetList, List<Service_Job_Defects> _jobDefList, List<Service_Tech_Aloc_Hdr> _jobEmpList, List<Service_Job_Det_Sub> _jobDetSubList, RecieptHeader _recHeader, List<RecieptItem> _recItems, List<ImageUploadDTO> _imgList, MasterAutoNumber _recAuto, string _sbChnl, string _itemType, string _brand, Int32 _warStus, MasterAutoNumber _masterAuto, out string _err, out string _jNo, out string _receiptNo, List<ServiceTechAlocSupervice> _supervisor = null, Int32 _autoStartJob = 0, List<Transport> transportList = null);

        //KAPILA
        [OperationContract]
        DataTable getDefectTypes(string _com, string _chnl, string _cat, string _code);

        //Tharaka 2014-09-30
        [OperationContract]
        DataTable GetTechAllocJobs(string com, DateTime From, DateTime To, string jobno, string Stage, Int32 isCusexpectDate, string customer, string PC, string town = null);

        //Tharaka 2014-09-30
        [OperationContract]
        List<Service_Job_Defects> GetJobDefects(string jobNo, Int32 lineNo, string Type);

        //Tharaka 01-10-2014
        [OperationContract]
        DataTable GetEmployeByDefect(string com, string defect);

        //Tharaka 02-10-2014
        [OperationContract]
        DataTable GetEmployeBySkillDesignation(string com, string Skill, string Designation, string EPF, string PC);

        //Tharaka 02-10-2014
        [OperationContract]
        Int32 Save_TechnicianAllocatoinHEader(List<Service_Tech_Aloc_Hdr> oHeader, MasterAutoNumber _ReqInsAuto, bool isNeedJobStageUpdate, Int32 _autoStartJob = 0);

        //Tharaka 03-10-2014
        [OperationContract]
        Int32 Save_ServiceJobStageLog(Service_Job_StageLog oLog);

        //Tharaka 03-10-2014
        [OperationContract]
        Int32 Update_JobDetailStage(string JObNo, Int32 JobLine, decimal Stage);

        //Tharaka 03-10-2014
        [OperationContract]
        DataTable GetAllocatedHistory(DateTime From, DateTime To, string com, string lOC, string Town, string Emp, out List<Service_TechAllocation> oItems);

        //Tharaka 04-10-2014
        [OperationContract]
        List<Service_Tech_Aloc_Hdr> GetJobAllocations(string jobNo, Int32 lineNo, string com);

        //Tharaka 2014-10-04
        [OperationContract]
        Int32 GetLocationCapacity(string com, string lOC, string Type);

        //Tharaka 2014-10-07
        [OperationContract]
        Int32 GetLocationCurrectSlotCount(string com, string lOC, string Terminal, DateTime From, DateTime To);

        //Shalika 07/10/2014
        [OperationContract]
        DataTable LoadTypes();
        //Shalika 07/10/2014
        [OperationContract]
        Int32 UpdateTaskType(string code, string desc, string type, int act, string userid);
        //Shalika 07/10/2014
        [OperationContract]
        DataTable LoadCat();

        //Tharaka 2014-10-09
        [OperationContract]
        Service_JOB_HDR GetServiceJobHeader(string jobNo, string com);

        //Tharaka 2014-10-09
        [OperationContract]
        List<Service_job_Det> GetJobDetails(string jobNo, Int32 lineNo, string com);

        //Tharaka 2014-10-09
        [OperationContract]
        DataTable getServiceJobDefects(string jobNo, Int32 lineNo);
        //Shalika 10/10/2014
        [OperationContract]
        Int32 Save_Allocated_Category(List<ServiceTaskLocations> lst_all_items, out string _err);
        //Shalika 10/10/2014
        [OperationContract]
        DataTable bind_task_loc(string loc);
        //Shalika 10/10/2014
        [OperationContract]
        Int32 SaveUpdateUtilization(int active, string com, string loc, decimal capacity, string type, string usr);

        //Tharaka 2014-10-13
        [OperationContract]
        DataTable getServiceJobEmployees(string jobNo, Int32 lineNo);

        //Tharaka 2014-10-16
        [OperationContract]
        List<ComboBoxObject> getEstimateTypes(string TYPE);

        //17/10/2014 Shalika
        [OperationContract]
        DataTable GetUtilitiDetails(string com);

        //17/10/2014 Shalika
        [OperationContract]
        DataTable CheckDefault(string com, string party_type, string loc);

        //Tharaka 2014-10-23
        [OperationContract]
        Int32 SAVE_ServiceHeader(Service_Estimate_Header oHeader, MasterAutoNumber _ReqInsAuto, List<Service_Estimate_Item> oEstimateItems, out string GenEstimate);

        //Tharaka 2014-10-09
        [OperationContract]
        Service_Estimate_Header GetServiceEstimateHeader(string EstimateNo, string com);

        //Tharaka 2014-10-24
        [OperationContract]
        List<Service_Estimate_Item> GetServiceEstimateItems(string EstimateNo);

        //Tharaka 2014-10-23
        [OperationContract]
        Int32 Update_Estimate_HEaderStatus(string status, string EstimateNo, string user, string com);

        //Tharaka 2014-10-27
        [OperationContract]
        Int32 Update_Job_dates(string jobNo, Int32 lineNo, DateTime techStart, DateTime techEnd, DateTime techStartMAn, DateTime techEndMan);

        //Tharaka 2014-10-30
        [OperationContract]
        List<ComboBoxObject> GetServiceWIPMRNLocation(string Com, string Loc);
        
        [OperationContract]
        DataTable checkAppMRNforJob(string _job);

        //Tharaka 2014-10-31
        [OperationContract]
        DataTable GetMRNItemsByJobline(string Com, string job, Int32 jobline);

        //Tharaka 2014-11-04
        [OperationContract]
        Int32 Update_ReqHeaderStatus(string STATUS, string USER, string COM, string MRN);

        //Tharaka 2014-10-31
        [OperationContract]
        List<Service_stockReturn> Get_ServiceWIP_StockReturnItems(string Com, string job, Int32 jobline, string Item, string LOC);

        //Tharaka 2014-11-08
        [OperationContract]
        Int32 Save_OldParts(List<Service_OldPartRemove> Item, out string err);

        //Tharka 2014-11-08
        [OperationContract]
        List<Service_OldPartRemove> Get_SCV_Oldparts(string jobNumber, Int32 lineNumber, string itemCode, string serial);

        //17/10/2014 Shalika
        [OperationContract]
        DataTable GetCatogeryDetails(string code);

        //10/11/2014 Shalika
        [OperationContract]
        DataTable GetSupplierDetails(string code, string com);

        //Tharaka 2014-11-08
        [OperationContract]
        Int32 Update_SCV_Oldpart_Refix(List<Service_OldPartRemove> Item, out string err);

        //Tharaka 2014-11-11
        [OperationContract]
        Int32 Save_SCV_TempIssueWithAOD_IN(List<Service_TempIssue> oMainList, MasterAutoNumber _ReqInsAuto, List<Tuple<String, String>> oAOD_Serials, out string err, InventoryHeader invHdr, string UserBIN, String jobNUm, Int32 jobLine);

        //Tharaka 2014-10-31
        [OperationContract]
        List<Service_TempIssue> Get_ServiceWIP_TempIssued_Items(string Com, string job, Int32 jobline, string Item, string LOC, string Type);

        //Tharaka 2014-11-11
        [OperationContract]
        Int32 Save_tempIssued_Return(List<Service_TempIssue> oMainList, MasterAutoNumber _ReqInsAuto, List<Tuple<Int32, Int32>> _IssueItemList);

        //Tharaka 2014-11-12
        [OperationContract]
        List<Service_TempIssue> GET_TEMPISSUE_RETURNED_ITMS(string Com, string job, Int32 jobline, string Item, string LOC);


        //Nadeeka 27-jan-2015
        [OperationContract]
        List<Service_supp_claim_itm> getSupClaimItems(string _com, string _sup, string _claimsup);

        //Nadeeka 27-jan-2015
        [OperationContract]
        List<SCV_SUPP_CLAIM_REC> getSupClaimAmt(Int32 _seq);

        //Tharaka 2014-11-13
        [OperationContract]
        DataTable GetSupplierWarrantyClaimItems(string Com, string job, Int32 jobline);

        //Tharaka 2014-11-14
        [OperationContract]
        Int32 Service_SupplierClaimRequest(List<Service_SupplierWarrantyClaim> oMainList, String user, string ComCode, String Loca, String SesstionID, out String errMSg);
       
        //Nadeeka 27-jan-2016
        [OperationContract]
        Int32 CancelSupplierClaimWarrantyReq(string _job, Int32 _line, Int32 _seq, string _user, out String errMSg);

        //kapila
        [OperationContract]
        DataTable GetJobTaskByJob(string Com, string job, Int32 jobline);

        //Tharaka 2014-11-17
        [OperationContract]

        DataTable GetSupplierWarrantyClaimRequestedItems(string Com, string job, Int32 jobline);


        // Nadeeka 25-01-2016
        [OperationContract]
        DataTable GET_SUP_WRNT_CLM_Requested_Serial(string Com, string job, Int32 jobline);



        //Nadeeka 16-07-2015
        [OperationContract]

        DataTable get_Customer_Feedback(string Com, string job, Int32 jobline);


        //Tharaka 2014-11-18
        [OperationContract]
        DataTable getEstimateTypesDataTable(string TYPE);

        //Tharaka 2014-11-18
        [OperationContract]
        List<Service_WCN_Detail> GetSupWarntyClaimReveiedItems(string job, Int32 jobline);

        //Nadeeka 2015-03-20
        [OperationContract]
        List<Service_WCN_Detail> GetSupWarntyClaimAvb(string job, Int32 jobline);
        //Darshana 2014-11-19
        [OperationContract]
        List<Service_job_Det> GetPcJobDetails(string com, string pc, string JobNo);

        //kapila
        [OperationContract]
        Dictionary<List<InventorySerialMaster>, List<InventorySubSerialMaster>> GetWarrantyMasterAgr(string _ser1, string _ser2, string _regno, string _warr, string _invoice, string _item, int _serid, out int _returnStatus, out string _returnMsg);

        //Written by Chamal on 19/11/2014
        [OperationContract]
        Dictionary<List<InventorySerialMaster>, List<InventorySubSerialMaster>> GetWarrantyMaster(string _ser1, string _ser2, string _regno, string _warr, string _invoice, string _item, int _serid, out int _returnStatus, out string _returnMsg);

        //Tharaka 2014-11-19
        [OperationContract]
        Int32 Save_ServiceWIP_SuppWarrntyClainReceive(List<Service_WCN_Detail> oMainList, String user, string ComCode, string defLocation, string binCode, string sesstionID, out string docNum);

        //Tharaka 2014-11-21
        [OperationContract]
        List<Service_stockReturn> Get_ServiceWIP_ConsumableItems(string Com, string LOC, string Item);

        //THARAKA 2014-11-22
        [OperationContract]
        Int32 UpdateConsumableItemQty(List<Service_TempIssue> oMainList);

        //Tharaka 2014-11-24
        [OperationContract]
        DataTable Get_service_location(string Com, string loc);

        //Sanjeewa 2017-04-27
        [OperationContract]
        DataTable Get_last_outwarddoc(string _item, string _serial);

        //THARAKA 2014-11-24
        [OperationContract]
        Int32 Update_Olppart_ReturnWarehouse(List<Tuple<Int32, String, String>> returnItemList, String user, string Com, string loc, string binCode, string sesstionID, string selectedLocation, List<Service_Job_Det_Sub> selectedITem, out string docNum, String jobNo, Int32 JObline);

        //THARAKA 2014-11-24
        [OperationContract]
        Int32 Update_NewItems_ReturnWarehouse(List<Service_stockReturn> oNewItems, String user, string ComCode, string defLocation, string binCode, string sesstionID, string OtherLocation, DateTime closeDate, out string docNum);

        // NADEEKA 16-10-2015
        [OperationContract]
        Int32 Update_NewItems_ReturnWarehouse_JobTrans(List<Service_stockReturn> oNewItems, String user, string ComCode, string defLocation, string binCode, string sesstionID, string OtherLocation, DateTime closeDate, out string docNum);

        // NADEEKA 16-10-2015
        [OperationContract]
        Int32 Update_Olppart_ReturnWarehouse_JobTrans(List<Tuple<Int32, String, String>> returnItemList, String user, string Com, string loc, string binCode, string sesstionID, string selectedLocation, List<Service_Job_Det_Sub> selectedITem, out string docNum, String jobNo, Int32 JObline);
 
        //Tharaka 2014-11-25
        [OperationContract]
        Int32 Update_ScvJobDetRemark(string TechRemark, string cusRemark, string job, Int32 jobLine, string Com, string location, string closeType, String clsRemark);

        //Tharaka 2014-11-26
        [OperationContract]
        List<Service_Defect_Types> GetDefectTypes(string Com, string CHN);

        //Tharaka 2014-11-26
        [OperationContract]
        Int32 SAVE_SCV_JOB_DEFECTS(List<Service_Job_Defects> items);

        //Tharaka 2014-11-26
        [OperationContract]
        List<Service_Tech_Comment> GetTechCommtByChnnl(string Com, string CHN);

        //Tharaka 2014-11-26
        [OperationContract]
        List<Service_Job_Tech_Comments> GetServiceJobTechComments(Int32 seq, string job, Int32 jobLine);

        //Tharaka 2014-11-26
        [OperationContract]
        Int32 SaveTechnicainComments(List<Service_Job_Tech_Comments> items);

        //Tharaka 2014-11-27
        [OperationContract]
        List<Service_Close_Type> GetServiceCloseType(string Com, string CHN, string _jobType = null); //updated by akila 2017/07/04

        //Tharaka 2014-11-27
        [OperationContract]
        List<Service_Job_Det_Sub> GetServiceJobDetailSubItems(string job, Int32 jobLine);

        //Tharaka 2014-11-28
        [OperationContract]
        List<Service_VisitComments> GET_SCV_JOB_VISIT_COMNT(string job, Int32 jobLine);

        //Tharaka 2014-11-28
        [OperationContract]
        Int32 SaveServiceVisitiComment(List<Service_VisitComments> items, List<Tuple<Int32, String>> empList);

        //Tharaka 2014-11-29
        [OperationContract]
        List<Service_job_visit_Technician> GET_SCV_JOB_VISIT_TECH(string job, Int32 jobLine, Int32 Seq);

        //Tharaka 2014-12-01
        [OperationContract]
        List<Service_confirm_Header> GetServiceConfirmHeader(String Com, String Channal, String Location, String ProfitCenter, String JobNumber, String RequestNO, String CustomerCode, String ConfrimNum, DateTime From, DateTime To);

        //Tharaka 2014-12-01
        [OperationContract]
        List<Service_Confirm_detail> GetServiceConfirmDetials(String Com, Int32 Seq, String ConfirmNum);

        //Tharaka 2014-12-03
        [OperationContract]
        List<Service_JOB_HDR> GetServiceJobHeaderAll(string jobNo, string com);

        //Darshana 2014-12-03
        [OperationContract]
        List<Service_Cost_sheet> ProcessJobCost(List<Service_job_Det> _serJobDet);

        //Darshana 2014-12-09
        [OperationContract]
        List<Service_Confirm_detail> ProcessJobRev(List<Service_job_Det> _serJobDet);

        //Tharaka 2014-12-10
        [OperationContract]
        Int32 ServiceApprove(string jobNumber, Int32 jobLine, String com, String location, String PC, String userID, String sesstionID, String status, String Remark, Int32 option, out String errorMsg, String Additional1, String Additional2);

        //Tharaka 2014-12-11
        [OperationContract]
        List<Service_Appove> GetJobsServiceApprove(string com, DateTime From, DateTime To, string jobno, string Stage, string customer, string PC, Int32 option, String Loc);

        //Tharaka 2014-12-15
        [OperationContract]
        List<Service_Estimate_Header> GetEstimateApprove(string com, DateTime From, DateTime To, string jobno, string Stage, string customer, string PC, Int32 option, String Loc);

        //Chamal 2014-Dec-15
        [OperationContract]
        Dictionary<Service_Req_Hdr, List<Service_Req_Det>> GetScvRequest(string _com, string _loc, string _reqNo, string _reqStatus, int _reqlineno, out int _returnStatus, out string _returnMsg);

        //Tharaka 2014-12-16
        [OperationContract]
        List<Service_Appove_MRN> GET_MRN_FOR_JOB(string Job, string com, string Loc, string Req);

        [OperationContract]
        Int32 updateMPCBWarranty(string _serial, out string _err);

        //Tharaka 2014-12-17
        [OperationContract]
        List<Scv_wrrt_App> GET_SCV_CUST_WRT_CLM(string Com, string Loc, string Status, string Job, DateTime From, DateTime To);

        //Tharaka 2014-12-17
        [OperationContract]
        List<ComboBoxObject> GET_INV_TYPES(string Com, string Loc);

        //damith 17-dec-2014
        [OperationContract]
        List<Service_Reminder> GetServJobReminder(ServiceRreminder _srvJReminderDet);

        [OperationContract]
        List<Service_Enquiry_Job_Det> GET_JOB_DET_ENQRY(String Ser1, String Ser2, String RegNum, String Com, String Loc, DateTime Fromdt, DateTime Todt);

        //Tharaka 2014-12-23
        [OperationContract]
        Int32 GetJobDetailsEnquiry(String job, String User, String Com, String Loc, String PC, out List<Service_job_Det> oJobDetails, out Service_Enquiry_Job_Hdr oheader, out string msg);

        //Chamal 2014-12-22
        [OperationContract]
        int GetScvJob(string _com, string _jobNo, out Service_JOB_HDR _jobHdr, out List<Service_job_Det> _jobDet, out List<Service_Job_Det_Sub> _jobDetSub, out List<Service_Job_Defects> _jobDef, out List<Service_Tech_Aloc_Hdr> _jobEmp, out List<Service_TempIssue> _jobTempIssue, out string _returnMsg);

        //Darshana 2014-12-23
        [OperationContract]
        Int32 Save_PO_Confirmation(PurchaseOrder _poHdr, List<PurchaseOrderDelivery> _poDel, List<Service_Purchase_Approval> _poApp, out string errorMsg);

        //Tharaka 2014-12-24
        [OperationContract]
        int GetAllJobDetailsEnquiry(String JobNumber, Int32 JobLine, String Com, String Location, String PC, out Service_job_Det oJobDetail, out List<Service_Job_Defects> oJobDefects, out List<Service_Enquiry_TechAllo_Hdr> oJobAllocations, out List<Service_Enquiry_Tech_Cmnt> oTechComments, out List<Tuple<string, string, string>> ConfirRmk_Type_User, out List<Service_Enquiry_StandByItems> oStandByItems, out string msg, out Decimal totalAmount);

        //Damith
        [OperationContract]
        List<Service_Reminder_Template> GetReminderTemplate();

        //Tharaka 2014-12-29
        [OperationContract]
        Service_JOB_HDR GET_SCV_JOB_HDR(string Job, string com);

        //Damith
        [OperationContract]
        Int32 SendReminderSms(List<SmsOutMember> _reminderLst, string userId, string company, string sessionId, out string error);

        //Chamal 29-12-2014
        [OperationContract]
        decimal GetScvJobStageRate(string _com, string _schnl, string _loc, string _scvCate, decimal _qty, DateTime _date, string _jobStage, string _item, string _fromTown, string _toTown, out string _msg);

        //Tharaka 2014-12-30
        [OperationContract]
        List<Service_job_Det> GET_SCV_JOB_DET_BY_SERIAL(String Serial, String item, String com);

        //Tharaka 2014-12-30
        [OperationContract]
        List<Service_Enquiry_InventryItems> GET_INVITMS_BYJOBLINE_ENQRY(String jobNo, Int32 lineNo, String Com);


        //Tharaka 2014-12-30
        [OperationContract]
        List<Service_Enquiry_Estimate_Hdr> GetEstimateHeaderEnquiry(String Com, String Loc, String Pc, String Job);

        //Tharaka 2015-01-02
        [OperationContract]
        int GetEstimateDetailsEnquiry(Int32 Seq, out List<Service_Enquiry_Estimate_Items> oItems);

        //Tharaka 2015-01-02
        [OperationContract]
        List<Service_Enquiry_Estimate_TAX> GET_SCV_EST_TAX_ENQRY(Int32 Seq, Int32 ItemLine);

        //Tharaka 2015-01-02
        [OperationContract]
        int GetInvoiceDetailsEnquiry(String JOb, String COm, out List<Service_Enquiry_Invoice_Items> oItems, out List<Service_Enquiry_Invoice_Header> oHeaders);

        //Tharaka 2015-01-02
        [OperationContract]
        List<Service_Enquiry_Invoice_TAX> GET_SCV_INVO_ITM_TAX_ENQRY(String InvoiceNum, Int32 lineNum);

        //Tharaka 2015-01-03
        [OperationContract]
        List<Service_Enquiry_PartTrasferd> GET_SCV_PART_TRSFER_ENQRY(String jobNo, Int32 lineNo, String Com);

        //Tharaka 2015-01-03
        [OperationContract]
        List<Service_Enquiry_SupplierWrntyClaim> SCV_PART_TRSFER_ENQRY(String jobNo, Int32 lineNo, String Com);

        //Tharaka 2015-01-03
        [OperationContract]
        List<Service_Enquiry_SupplierWrntyDetails> GET_SCV_SUPP_WRNTREQHDR_ENQ(String jobNo, Int32 lineNo, Int32 Seq, String Type);
        // Nadeeka
        [OperationContract]
        List<Service_job_Det> getSupplierClaimRequest(string _com, string _loc, string _supp, string _job, string _jobPart, string _jobserial, string _jobBrand, string _jobItem, DateTime _FromDate, DateTime _ToDate, string _cat, Int32 _sts);


        // Nadeeka
        [OperationContract]
        Int32 GetJobsmsSeq();
        // Nadeeka
        [OperationContract]
        List<Service_JOB_HDR> getTransferJob(string _com, string _loc, DateTime _FromDate, DateTime _ToDate, string _job);

        // Nadeeka
        [OperationContract]
        List<Service_job_Det> sp_getSupClaimDetails(string _claimNo);
        // Nadeeka
        [OperationContract]
        List<Service_WCN_Hdr> getWCNHeader(string _com, string _loc, DateTime _fdate, DateTime _todate, string _sts, string _job, string _order, string _docno);

        // Nadeeka 10-03-2015
        [OperationContract]
        Int32 Save_ItemCanibalize(MasterAutoNumber _masterAuto_out, InventoryHeader _outHeader, List<ReptPickSerials> _outSerial, List<InventorySubSerialMaster> _inserList, string Type, out string _err);

        // Nadeeka 10-03-2015
        [OperationContract]
        Int32 Save_SupplierClaim(Service_WCN_Hdr _ReqSupHdr, List<Service_WCN_Detail> _claimItemList, MasterAutoNumber _masterAuto, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, MasterAutoNumber _recieptAuto, InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, MasterAutoNumber _invoiceAuto, string _type, Boolean _isRecall, string _sessionID, out string _err);


        [OperationContract]
        Int32 Save_SupplierClaimChecking(Service_WCN_Hdr _ReqSupHdr, List<Service_WCN_Detail> _claimItemList, string _remarks, string _user, string _sessionID, out string _err);

        // Nadeeka 10-03-2015
        [OperationContract]
        Int32 Update_SupplierClaim(Service_WCN_Hdr _ReqSupHdr, out string _err);

        // Nadeeka 27-01-2016
        [OperationContract]
        DataTable GetServiceSupplierWarrantyJob(string _com, string _job,Int32 _line);

        // Nadeeka 29-01-2016
        [OperationContract]
        List<Service_job_Det> getSupplierClaimRequestMRN(string _com, string _job, Int32 _jobline);
       
        // Nadeeka 29-01-2016
        [OperationContract]
        Int32 UpdateSupplierClaimForMRN(string _job, Int32 _jobLine, string _user, string _rem, out string _err);

            // Nadeeka 29-01-2016
        [OperationContract]
        List<Service_WCN_Hdr> getWCNHeaderCheck(string _com, string _loc, DateTime _fdate, DateTime _todate, string _sts, string _job,string _doc);

        // Nadeeka 18-03-2015
        [OperationContract]
        Int32 Save_Job_Transfer(List<Service_job_Det> _jobItems, List<SCV_TRANS_LOG> _jobTransfer, MasterAutoNumber _masterAuto, string _loc, DateTime _transDate, string _com, string _sbChnl, string _itemType, string _brand, Int32 _warStus, Int32 _isJobClose, string _sessionID, string _PC, out string _err);


        //Darshana 06-01-2014
        [OperationContract]
        // Int32 Save_Job_Confirmation(List<Service_confirm_Header> _confHdr, List<Service_Confirm_detail> _confDet, List<Service_Cost_sheet> _jobCost, MasterAutoNumber _masterAuto, InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, Boolean _isInv, List<Service_job_Det> _processJobList, InventoryHeader _inventoryHeader, MasterAutoNumber _inventoryAuto, string _loc, InventoryHeader _aodHdr, List<ReptPickSerials> _reptPickSerialsAod, MasterAutoNumber _aodAuto, RCC _rcc, Boolean _isRcc, Boolean _isstockUpdate, string _cusPreferLoc, string _subChannel, out string errorMsg, Int32 _autoStartJob = 0); // updated by akila 2017/06/05
        Int32 Save_Job_Confirmation(List<Service_confirm_Header> _confHdr, List<Service_Confirm_detail> _confDet, List<Service_Cost_sheet> _jobCost, MasterAutoNumber _masterAuto, InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, Boolean _isInv, List<Service_job_Det> _processJobList, InventoryHeader _inventoryHeader, MasterAutoNumber _inventoryAuto, string _loc, InventoryHeader _aodHdr, List<ReptPickSerials> _reptPickSerialsAod, MasterAutoNumber _aodAuto, RCC _rcc, Boolean _isRcc, Boolean _isstockUpdate, string _cusPreferLoc, string _subChannel, out string errorMsg,out string scv_req_no, Int32 _autoStartJob = 0,
            Service_Req_Hdr _jobHdr = null, List<Service_Req_Det> _jobItems = null, List<Service_free_det> _Service_free_detlist = null,
             List<Service_Req_Def> _jobDefList = null, List<Service_Req_Det_Sub> _jobDetSubList = null, MasterAutoNumber _recAuto = null,
             string _sbChnl = null, string _itemType = null, string _brand = null, Int32 _warStus = 0, MasterAutoNumber _masterAutonew = null,
            Int32 _isProcess = 0);
             
        // Int32 Save_Job_Confirmation(List<Service_confirm_Header> _confHdr, List<Service_Confirm_detail> _confDet, List<Service_Cost_sheet> _jobCost, MasterAutoNumber _masterAuto, InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, Boolean _isInv, List<Service_job_Det> _processJobList, InventoryHeader _inventoryHeader, MasterAutoNumber _inventoryAuto, string _loc, InventoryHeader _aodHdr, List<ReptPickSerials> _reptPickSerialsAod, MasterAutoNumber _aodAuto, RCC _rcc, Boolean _isRcc, Boolean _isstockUpdate, string _cusPreferLoc,  string _subChannelout ,string errorMsg);
        //Int32 Save_Job_Confirmation(Service_confirm_Header _confHdr, MasterAutoNumber _masterAuto, out string errorMsg);

        //Tharaka 2015-01-10
        [OperationContract]
        List<Service_Enquiry_ConfDetails> GET_SCV_CONFDET_ENQRY(String jobNo, Int32 lineNo, String Com);

        //Tharaka 2015-01-10
        [OperationContract]
        List<Service_Enquiry_CostSheet> GET_SCV_COST_SHEET_ENQRY(String jobNo, Int32 lineNo, String Com, String Loc);

        //Tharaka 2015-01-12
        [OperationContract]
        List<Service_Enquiry_WarrtyReplacement> GET_SCV_WARTYREPLMENT_ENQRY(String jobNo, Int32 lineNo, String Com);

        //Tharaka 2015-01-12
        [OperationContract]
        List<Service_Enquiry_CustCollectionData> GET_CUST_COLLDATE_ENQRY(String jobNo, Int32 lineNo, String Com);

        //Tharaka 2015-01-12
        [OperationContract]
        List<_Service_Enquiry_StageLog> GET_STAGELOG_ENQRY(String jobNo, Int32 lineNo);

        //Tharaka 2015-01-13
        [OperationContract]
        List<Service_Enquiry_Inssuarance> GET_INSSURANCE_ENQRY(String Com, String Ser, String Item, String Invoice);

        //Tharaka 2015-01-14
        [OperationContract]
        List<Service_job_Det> SCV_JOB_GET_SER_OSR_REG(String Com, String Loc, String Serial, String Serial2, String RegNum);

        //Tharaka 2015-01-16
        [OperationContract]
        Decimal GET_SCV_ITM_COST(String Com, String Job, Int32 Line, String Item, String Status, Decimal Qty);

        //Tharaka 2015-01-21
        [OperationContract]
        List<Service_stockReturn> Get_ServiceWIP_ViewStockItems(string Com, string job, Int32 jobline, string Item, string LOC);


        //Tharaka 2015-01-22
        [OperationContract]
        List<Service_TempIssue> GET_TEMPISSUE_By_LOC(string Com, string job, Int32 jobline, string Item, string LOC, string Type);

        //Tharaka 2015-01-22
        [OperationContract]
        String GET_SCV_JOB_CATE(string Com, string job);

        //Tharaka 2015-01-23
        [OperationContract]
        List<Service_Gate_Pass_HDR> SCV_CHEK_GP_FOR_JOBLINE(String jobNo, Int32 lineNo, String Com);

        //Tharaka 2015-01-23
        [OperationContract]
        Service_Category GET_SCV_CATE_BY_JOB(String jobNo, String Com);

        //Tharaka 2014-09-30
        [OperationContract]
        DataTable GetJObsFOrWIP(string com, DateTime From, DateTime To, string jobno, string Stage, Int32 isCusexpectDate, string customer, string PC, String userID);

        //Tharaka 2015-01-27
        [OperationContract]
        List<MST_BUSPRIT_LVL> GetCustomerPriorityLevel(String custCode, String Com);

        //Tharaka 2015-01-27
        [OperationContract]
        List<MST_BUSPRIT_TASK> GetCustomerPriorityTask(MST_BUSPRIT_TASK OInput);

        //Tharaka 2015-01-27
        [OperationContract]
        List<SCV_ALRT_LVL> GetAlertLevel(Int32 Seq);

        //Tharaka 2015-01-27
        [OperationContract]
        List<SCV_ALRT_EMP> GetAlertEmployees(Int32 Seq);

        //Tharaka 2015-01-27
        [OperationContract]
        List<scv_prit_task> GetPriorityTask(scv_prit_task OInput);
        //Darshana
        [OperationContract]
        List<Service_Job_Charges> GetServiceJobCharges(String jobNo, Int32 _jobLine);

        //Tharaka 2015-01-29
        [OperationContract]
        List<Service_StandBy> GetStandByItems(string Com, string job, Int32 jobline, string Item, string LOC);

        //Tharaka 2015-01-30
        [OperationContract]
        List<Service_stockReturn> GetServiceJobStockItems(string Com, string job, Int32 jobline, string Item, string LOC);

        //Tharaka 2015-01-05
        [OperationContract]
        int ServiceSaveImages(String com, String pc, String loc, List<ImageUploadDTO> oMainList, out String err);

        //Tharaka 2015-01-05
        [OperationContract]
        List<ImageUploadDTO> GetImages(String com, String pc, String loc, ImageUploadDTO job_n_Line, out String err);

        //Tharaka 2015-02-06
        [OperationContract]
        List<Service_Request_Defects> getRequestDefects(string Req, Int32 line);

        //Tharaka 2015-02-06
        [OperationContract]
        List<Service_Job_Defects> GetRequestJobDefectsJobEnty(string Req, Int32 lineNo);

        //Tharaka 2015-02-07
        [OperationContract]
        DataTable GET_INT_HDR_ITMS_JOBENTY(String DocNum, String InvoiceNum);
        //Darshana 2015-02-12
        [OperationContract]
        List<MasterServiceEmployee> GetMasterSerEmp(string _com, string _Tp, string _cd, string _cate, string _emp, Int16 _act);
        //Darshana 2015-02-12
        [OperationContract]
        Service_Tech_Aloc_Hdr GetAllocTechJob(String _com, String _pc, string _tp, string _jobNo, Int32 _jobLine, string _empCd, string _stus, Int32 _curStatus);

        // Nadeeka 18-05-2015
        [OperationContract]
        Service_Tech_Aloc_Hdr GetAllocationDet(String _com, String _jobNo);

        //Tharaka 2015-02-11
        [OperationContract]
        List<MST_ITM_CAT_COMP> getMasterItemCategoryComponant(string Cate1);

        //Tharaka 2015-02-12
        [OperationContract]
        bool CheckItemCategoriWarrantyStatus(string com, string itemCode, out string msg, out MST_ITM_CAT_COMP oCateComp);

        //Tharaka 2015-02-12
        [OperationContract]
        int INSERT_INR_SERMSTSUB(List<InventoryWarrantySubDetail> oItems, out string msg);

        //Tharaka 2015-02-17
        [OperationContract]
        int SaveServiceMsg(Service_Message oItem, out string msg);

        //Tharaka 2015-02-18
        [OperationContract]
        List<MST_ITM_CAT_COMP> getMasterItemCategoryComByItem(string Item);
        //DArshana 2015-02-19
        [OperationContract]
        List<Service_job_Det> getPrejobDetails(string _com, string _ser, string _itm);
        //Darshana 2015-02-25
        [OperationContract]
        decimal GetScvJobStageCost(string _com, string _schnl, string _loc, string _scvCate, decimal _qty, DateTime _date, string _jobStage, string _item);

        //Tharaka 2015-03-16
        [OperationContract]
        Service_Message_Template GetMessageTemplates_byID(String com, String Chnal, Int32 Id);
        //Darshana 2015-03-18
        [OperationContract]
        Service_confirm_Header GetConfDetByJob(string _com, string _jobNo, string _confNo);
        //Darshana 2015-03-18
        [OperationContract]
        int JobConfCancel(string _com, string _jobNo, string _confNo, Int32 _confSeq, string _status, string _canBy, out string err);

        // Tharaka 2015-05-12
        [OperationContract]
        Int32 UPDATE_SCV_CONF_HDR_ISINVD(Int32 Status, Int32 seq, string com, out string err, String jobNum);

        //Tharaka 2015-05-15
        [OperationContract]
        SCV_TRANS_LOG GetTrasferDetailsEnquiry(String job, Int32 Line, String loc);

        //Tharaka 2015-05-29
        [OperationContract]
        List<Service_stockReturn> GetStandyPendingADOItems(string Com, string job, Int32 jobline, string Item, string LOC);

        //Tharaka 2015-05-30
        [OperationContract]
        Int32 Save_SCV_TempIssue(List<Service_TempIssue> oMainList, MasterAutoNumber _ReqInsAuto, out string err);

        //Tharaka 2015-06-01
        [OperationContract]
        DataTable GET_CON_HDRS_JOB_COM(string Com, string job);

        //Sanjeewa 2016-03-24
        [OperationContract]
        DataTable check_Invoiced_JobClosed(string _invoice);

        //Tharaka 2015-06-01
        [OperationContract]
        int getSerialDetails(String serial, out string err, out DataTable dtTempWarra, out DataTable dtInvoiceDetails, out DataTable dtWarrReplace, out DataTable dtIntHdr);

        //Tharka 2015-07-13
        [OperationContract]
        DataTable GET_WRR_RPLC_DETAILS(string Com, string job, out DataTable dtDocDetails, out DataTable dtRecSerial, out DataTable dtIssuedSer);

        //Darshana 2015-07-13
        [OperationContract]
        List<RequestApprovalHeader> GetWarrRepReqByJobNumber(string _jobNo, Int32 _jobLineNo);

        //Tharaka 2015-07-16
        [OperationContract]
        bool CheckPendignAODForInvoiceReversal(String invoiceNUm, String COm, String Loc, out string err);

        //Tharaka 2015-07-21
        [OperationContract]
        List<Service_Enquiry_Job_Det> GET_JOB_DET_ENQRY_New(String Ser1, String Ser2, String RegNum, String Com, String Loc, DateTime Fromdt, DateTime Todt, out string err);
        //DArshana 2015-07-16
        [OperationContract]
        Warr_Replacement_Det GetWarrantyReplacementHistory(string _nItm, string _nSer, string _tp, string _nWara);
        //Darshana 2015-07-29
        [OperationContract]
        DataTable GetInvDetBySerial(string _invoice, string _serial, string _itm);
        //Darshana 2015-07-29
        [OperationContract]
        DataTable GetInvDetWithDofrmScm(string _invoice, string _itm, string _ser);

        //Tharaka 2015-08-14
        [OperationContract]
        Int32 jobCancel(String com, String Loc, String User, String sesstion, List<Service_job_Det> oJobDetails, out string err);

        //Tharaka 2015-08-19
        [OperationContract]
        Int32 UpdateInspectionSerial(Service_job_Det _jobDet, Service_Job_StageLog oLog1, out string err);

        //Tharaka 2015-08-20
        [OperationContract]
        DataTable GET_ALLALOCATEDJOBS_BY_USER(String Com, String Loc, String User);

        //Tharaka 2015-09-08
        [OperationContract]
        DataTable GET_EQULPC_TO_PC(string Com, string PC);

        // Tharaka 2015-09-18
        [OperationContract]
        List<Service_Confirm_detail> GET_CONF_DET_BY_JOB(string _jobNo);

        //Tharaka 2015-09-18

        [OperationContract]
        Int32 ServiceApporvalDiscountProcess(string jobnum, CashGeneralDicountDef oItem, out string err);

        //Sanjeewa 2015-11-12
        [OperationContract]
        DataTable get_BulkJobPrint(string _com);

        [OperationContract]
        DataTable get_isSmartWarr(String Jobno, Int16 lineno);

        [OperationContract]
        DataTable get_isSmartWarr_Job(Decimal sw_perc, String invno, String serial, String item);
        //Sanjeewa 2016-04-07
        [OperationContract]
        Boolean check_IsJobClosed(string _Job, Int16 _line);

        [OperationContract]
        int get_UpdateBulkJobPrint(string _Job);

        [OperationContract]
        Int32 Save_ItemCanibalize_web(MasterAutoNumber _masterAuto_out, InventoryHeader _outHeader, List<ReptPickSerials> _outSerial, List<ItemKitComponent> _inserList, string Type, out string _err);
        //subodana 2016-04-12
        [OperationContract]
        int ServiceSaveScanImages(String com, String pc, String loc, List<TBS_IMG_UPLOAD> oMainList, out String err);

        //subodana 2016-04-12
        [OperationContract]
        List<TBS_IMG_UPLOAD> GetImageDetails(string jobid);

        //subodana 2016-05-12
        [OperationContract]
        DataTable GetCusValuDecar(string com, string docNo);

        //subodana 2016-05-16
        [OperationContract]
        DataTable GetCustomElements(string com, string docNo, string type);

        //subodana 2016-05-17
        [OperationContract]
        DataTable GetCustomWorkingSheet(string docNo, string type);

        //subodana 2016-05-25
        [OperationContract]
        DataTable GetRouteDetails(string com);

        //subodana 2016-05-27
        [OperationContract]
        DataTable StockVerification(string com, string docno);

        //subodana 2016-05-27
         [OperationContract]
         DataTable GetSysSerials(string com, string loc, string itemcode);

         //subodana 2016-05-28
         [OperationContract]
         DataTable GetSystemQTY(string com, string loc, string itemcode);

         //subodana 2016-05-28
         [OperationContract]
         DataTable GetScanQTY(string com, string doc);

         //subodana 2016-06-17
         [OperationContract]
         DataTable GetGoodsDeclarationDetails(string entryno);

         //subodana 2016-06-18
         [OperationContract]
         DataTable GetCusdecHDRData(string entryno, string com);

         //subodana 2016-06-24
         [OperationContract]
         DataTable GetCusdecElementnew(string entryno);

         //subodana 2016-06-24
         [OperationContract]
         DataTable GetCusdecCommonNew(string country);

        //subodana 2016-07-12
        [OperationContract]
        List<Cusdec_Goods_decl> GetGoodsDeclarationDetailsList(string entryno);

        //subodana 2016-07-21
        [OperationContract]
        List<SunAccountBusEntity> GetSunAccountDetails(string accno, string com);

        //subodana 2016-08-15
        [OperationContract]
        DataTable GetCusdecAssessmentData(string entryno);

        //subodana 2016-08-27
        [OperationContract]
        DataTable GetCusdecAssessmentAccountData(string entryno, string com);


        //Rukshan 2016-10-03
        [OperationContract]
        List<SunAccountBusEntity> GetSunAccountDetailsforSO(string com, string cus);
        //subodana
        [OperationContract]
        DataTable GetMaxSRnum(string code);

        //Akila 2017/04/01
        [OperationContract]
        DataTable GetServiceJobHistoryBySerial(string _serail, string _item);
        //tharanga 2017/06/05
        [OperationContract]
        Int32 SaveserviceAreas(List<ServiceAreaS> oServiceAreaS);
        //Int32 Save_Supplier_Claim_Itm(List<Service_supp_claim_itm> _supClaimList, List<SCV_SUPP_CLAIM_REC> _lstamt, out string _msg);


        //Akila 2017/05/05 get job serial details
        [OperationContract]
        List<InventorySerialMaster> GetServiceItemDetails(string _item, string _ser1, string _ser2, string _regno, string _warr, string _invoice, int _serid);

        ////Akila 2017/05/06 -Save service tech alocated supervicers
        //[OperationContract]
        //Int32 SaveServiceAlocatedSupervicer(ServiceTechAlocSupervice _supervicer);
        //Add by Lakshan 23 May 2017
        [OperationContract]
        List<TmpServiceWorkingProcess> GetJObsFOrWIPWeb(string com, DateTime From, DateTime To, string jobno, string Stage, Int32 isCusexpectDate, string customer, string PC, String userID);
    //Tharanga 2017/06/06
         [OperationContract]
        List<_Service_Enquiry_StageLog_stage> GET_STAGELOG_ENQRY_Stage(String jobNo, Int32 lineNo);
        //Tharanga 2017/06/06
         [OperationContract]
         DataTable GetJobStage_des(string _P_com, string _P_chnl, decimal _P_stage);

        //By akila 2017/06/12 send sms to get job confirmetion
        [OperationContract]
         Int32 GetJobConfirmationBySMS(string _company, string _location, string _user, string _customer, string _jobNo, out string _outMessage);

        //By akila 2017/06/17 get po details
        [OperationContract]
        DataTable GetPoItemDetails(string _jobNo, Int32 _lineNo, string _itemCode, string _poNo);

        //By akila 2017/06/20
        [OperationContract]
        DataTable GetJobDetailsWithStage(string _company, string _location, string _jobNo);
        //Tharanga 2017/06/26
        [OperationContract]
        DataTable GetInvoice_Summary(string _company, string _location, DateTime _fromdate, DateTime _todate, string _pc);

        //Akila 2017/07/03
        [OperationContract]
        DataTable GetConfirmedServicePoDetails(string _jobNo, string _poNo);

        //Tharanga 2017/07/13
        [OperationContract]
        Int32 SendConfirmationMailPromoter(string _com, string _loc, string _doc, string subchannel, string _user, string _toemail, string _mobile);

        //By Akila  2017/07/11
        [OperationContract]
        Int32 SaveJobConfirmationWithRequest(List<Service_confirm_Header> _confHdr, List<Service_Confirm_detail> _confDet, List<Service_Cost_sheet> _jobCost, MasterAutoNumber _masterAuto, InvoiceHeader _invoiceHeader, List<InvoiceItem> _invoiceItem, RecieptHeader _recieptHeader, List<RecieptItem> _recieptItem, MasterAutoNumber _invoiceAuto, MasterAutoNumber _recieptAuto, Boolean _isInv, List<Service_job_Det> _processJobList, InventoryHeader _inventoryHeader, MasterAutoNumber _inventoryAuto, string _loc, InventoryHeader _aodHdr, List<ReptPickSerials> _reptPickSerialsAod, MasterAutoNumber _aodAuto, RCC _rcc, Boolean _isRcc, Boolean _isstockUpdate, string _cusPreferLoc, string _subChannel, out string errorMsg, out string _rccNo, Int32 _autoStartJob = 0, bool _genarateNewRequest = false, Service_Req_Hdr _rccHdr = null, List<Service_Req_Det> _rccDetails = null, MasterAutoNumber _rccAuto = null);
       
        //Akila 2017/07/14
        [OperationContract]
        DataTable GetAppovelPendingJobs(string _comCode, string _location, DateTime _fromDate, DateTime _toDate);

        //Dulanga 2017/7/20
        [OperationContract]
        DataTable GetMobJobStage(string p_code, string p_type);

        //Dulanga 2017/7/25
        [OperationContract]
        DataTable GetServiceTownByDist(string p_code);

        //Dulanga 2017/8/16
        [OperationContract]
        DataTable GetTecAllocateJob(string p_com, string p_loc, string p_pc, DateTime p_sdate, DateTime p_edate,
        string p_sstage, string p_estage, string p_user);

        //Dulanga 2017/8/16
 
        [OperationContract]
        DataTable GetJobByKey(string p_key);

        [OperationContract]
        DataTable GetJobByKeyNew(string p_key, string p_com, string p_pc, string p_loc);

        //Dulanga 2017/08/16
        [OperationContract]
        DataTable GetJobBySerial(string p_serial,string p_com,string p_pc,string p_loc);

        //Dulanga 2017/08/17
        [OperationContract]
        Int32 UpdateJobStageAndJobLog(string jobNo, string jobLine, string stage, string _user, DateTime techStart, DateTime techEnd
            , DateTime techStartMAn, DateTime techEndMan, string com, string loc, string sessionId);

        //Dulanga 2017/08/21
        [OperationContract]
        DataTable getCommentType(string com, string chnl);

        //Dulanga 2017/08/21
        [OperationContract]
        DataTable getMobDefectType(string com, string chnl, string cate);

        [OperationContract]
        DataTable getMobEmailSetup(string com, string pc, string module);

        //Dulanga 2017/08/26
        [OperationContract]
        DataTable getMobMRNLocations(string com, string loc,string type);

        //Dulanga 2017/08/26
        [OperationContract]
        DataTable getMobSerialDetail(string serial);

        //Dulanga 2016/08/29
        [OperationContract]
        List<InventorySerialMaster> GetWarrantyMasterSCM2(string _item, string _ser1, string _ser2, string _regno, string _warr, string _invoice, int _serid);

        //Tharanga 2017/08/17
        [OperationContract]
        DataTable sp_get_scv_job_amount(string p_jobno, int p_jobline);

        //Tharanga 2017/08/17
        [OperationContract]
        DataTable JobDetails_ABE(DateTime _from, DateTime _to, string _com, string _loc, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
                   string in_Itemcat2, string in_Itemcat3, string _technician, string _jobcat, string _itemtp, decimal _jobstatus, string _warrstatus, string _jobno, string _user, out string _err);
     //Tharanga 2017/09/09
        [OperationContract]
        Int32 Update_Approve_MRN(string STATUS, string USER, string COM, string MRN, InventoryRequest _inventoryRequest);

        //add by thranga 2017/09/14
        [OperationContract]
        DataTable get_msg_info_MAIL(string p_com, string p_pc, string p_doc_tp);

        //Akila 2017/011/15
        [OperationContract]
        DataTable GetWarrantyReplaceSerialDetails(string _serialNo, string _itemCode);

        //Tharindu 2017-12-14
        [OperationContract]
        DataTable GetRccLetterDetails(string _rccNo);
        //Sanjeewa 2018-02-23
        [OperationContract]
        DataTable GetErrJobList();
        //Sanjeewa 2018-02-23
        [OperationContract]
        Int32 UpdatesatitmDOqty_GP(string _Jobno);

        //Akila 2018/02/21
        [OperationContract]
        DataTable GetPendingClaimDocs(string _docNo);
        //sube
         [OperationContract]
        DataTable GetMailBrands(string _docNo);
         //sube
         [OperationContract]
         DataTable GetBrandsMail(string _docNo, string cat, string com);

         //Tharindu 2018-03-30
         [OperationContract]
         DataTable GetJobHeader(string jobNo, string com);

         //Tharindu 2018-03-30
         [OperationContract]
         DataTable GetJobDetail(string jobNo, string com);
         //Tharanga
         [OperationContract]
         DataTable get_REF_OLD_PART_CAT(string _cate_1, string _cate_2, string _cate_3);

      //Wimal @ 13/07/2018       
       [OperationContract]
       DataTable get_BalstockItem(string _comcode, string _InvNo);
          
        //Tharanga
        [OperationContract]
        Int32 SAVE_SCV_commnet(List<scv_jobcus_feed> oMainListCommnets);
        //tharanga 2018/07/11
        [OperationContract]
        List<scv_jobcus_feed> GetCustJobFeedback_list(string _jobno, Int32 _seq, Int32 _jobline);
        //tharanga 2018/06/22
        [OperationContract]
        DataTable GET_ALLEMP_BY_TEAM_CD(string _COM, string _PC, string _TEAM_CD);
        //tharanga 2018/09/15
        [OperationContract]
        Int32 update_scv_agr_hdr(SCV_AGR_HDR oHeader, out string _err);
         //tharanga 2018/09/19
        [OperationContract]
        Int32 update_svc_hdr_cst_exdate(string _jobno, Int32 _jobline, DateTime _date, string _user, out string _err);

        //tharanga 2018/09/19
        [OperationContract]
        DataTable GET_WRR_INVTRYDET_BY_SUBDOC(string doc);
        //Tharanga 2018/06/20
        [OperationContract]
        Int32 save_busentity_newcom(string _com, string _cust_cd);
        //tharanga 2018/06/21
        [OperationContract]
        DataTable GET_JOB_REQ_DET(string _ComCode, string _loc, string _ProfitCenter, string _req, DateTime _frmdate, DateTime _todate, string _town);
        //tharanga 2018/06/21
        [OperationContract]
        Int32 Save_Job_by_job_req(List<Service_JOB_HDR> _jobHdrlist, List<Service_job_Det> _jobItems, List<Service_Job_Defects> _jobDefList, List<Service_Tech_Aloc_Hdr> _jobEmpList, List<Service_Job_Det_Sub> _jobDetSubList, RecieptHeader _recHeader, List<RecieptItem> _recItems, List<ImageUploadDTO> _imgList, MasterAutoNumber _recAuto, string _sbChnl, string _itemType, string _brand, Int32 _warStus, MasterAutoNumber _masterAuto, out string _err, out string _jNo, out string _receiptNo, List<ServiceTechAlocSupervice> _supervisor = null, Int32 _autoStartJob = 0, List<Transport> transportList = null);

        //tharanga 2018/07/13
        [OperationContract]
        List<ServiceAreaS> GetServiceCenterDetailslist(string _com_CD, string _svc_CD, string _Town_CD, string _searchText);
        //tharanga 2018/11/30
        [OperationContract]
        Int32 updateRequestHeader(Service_Req_Hdr _Service_Req_Hdr, out string _err);
        //Dulaj 2018/Dec/26
        [OperationContract]
        DataTable GetCustomWorkingSheetHS(string docNo, string type);
    }
}
