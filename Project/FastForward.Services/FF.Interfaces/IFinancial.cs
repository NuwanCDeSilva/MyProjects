using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using FF.BusinessObjects;
using System.Data;
using FF.BusinessObjects.Financial;
using FF.BusinessObjects.Commission;
using FF.BusinessObjects.BITool;
using FF.BusinessObjects.General;
using FF.BusinessObjects.Account;



namespace FF.Interfaces
{
    [ServiceContract]
    public interface IFinancial
    {
        [OperationContract]
        Boolean IsPrvDayTxnsFound_DO_Rcc(string _com, string _loc, DateTime _date);
        [OperationContract]
        Boolean IsPrvDayTxnsFound_Sale_DO(string _com, string _pc, string _loc, DateTime _date);
        [OperationContract]
        Boolean IsPrvDayTxnsFound_Sale_Rec(string _com, string _pc, DateTime _date);
        [OperationContract]
        Boolean IsPrvDayTxnsFound_DO(string _com, string _loc, DateTime _date);
        //kapila
        [OperationContract]
        Int32 SaveExcsShortSettlement(ExcessShortTrna _excsShortRem, ExcessShortDet _excsShortDet, RemitanceSummaryDetail _remsumdet, out string _msg);
        //kapila
        [OperationContract]
        int GetRemSumDetWOCurDt(string _com, DateTime _fromdate, DateTime _todate, DateTime _date, string _sec, string _code, string _pc, out Decimal _value, out Decimal _valueFinal);
        //kapila
        [OperationContract]
        Decimal Get_Inr_sys_para(string _com, string _loc, string _item, string _itmstus, Int32 _qty);

        //kapila
        [OperationContract]
        DataTable get_rem_sum_rep_recon(string _com, string _user, DateTime _from, DateTime _to);
        //kapila
        [OperationContract]
        DataTable GetAccVoucher(string _ref);
        //kapila
        [OperationContract]
        DataTable GetCostByOthDoc(string _com, string _othdoc);

        //kapila
        [OperationContract]
        decimal GetOriginalAccInsurance(string _com, string _revno);

        //kapila
        [OperationContract]
        DataTable Get_credit_Outs_ByInv(string _com, string _pc, string _inv, string _user, Int32 _isOuts);
        //kapila
        [OperationContract]
        Boolean isGVFound(string _ref, string _cust);
        //kapila
        [OperationContract]
        DataTable getVouDetByCode(string _vou);
        //kapila
        [OperationContract]
        DataTable getMyAbVouTypes();
        //kapila
        [OperationContract]
        DataTable RegistrationProcessReport(string _com, string _loc, DateTime _from, DateTime _to, Int32 _opt, string _fincomp);
        //kapila
        [OperationContract]
        DataTable getVehRegComDetails(string _com, string _recno);

        //kapila
        [OperationContract]
        DataTable processIntroduComm(string _com, string _pc, DateTime _date, string _user);
        //kapila
        [OperationContract]
        Boolean IsChkVehRelease(string _inv);
        //kapila
        [OperationContract]
        int UpdateCardMemberSerial(string _ser, string _cus, string _no, Int32 _isact);
        //kapila
        [OperationContract]
        int UpdatePromotCommHdr(string _circ, DateTime _todate, string _user);
        //kapila
        [OperationContract]
        Int32 UpdateCashConvInv(string _accno, string _reqno);

        //kapila
        [OperationContract]
        int GetReversedCredVal(string _invno, decimal _revAmt, out Decimal _value);

        [OperationContract]
        DataTable GetPromotCommHdr(string _com, string _circ);
        [OperationContract]
        DataTable GetPromotCommParty(Int32 _seq);
        [OperationContract]
        DataTable GetPromotCommSaleTp(Int32 _seq);
        [OperationContract]
        DataTable GetPromotCommSch(Int32 _seq);
        //kapila
        [OperationContract]
        DataTable GetPromotCommDet(Int32 _seq);
        [OperationContract]
        DataTable GetPromotCommDetails(Int32 _seq);
        //kapila
        [OperationContract]
        List<MasterInvoiceType> GetAllMainInvType();
        //kapila
        [OperationContract]
        Int32 SavePromotCommDefi(PromotComHdr _lstHdr, List<PromotComItem> _lstItem, List<PromotComSch> _lstSch, List<PromotComParty> _lstParty, List<PromotComDet> _lstDet);
        //kapila
        [OperationContract]
        int GetNonUtiRecTotal(string _com, string _pc, string _accno, out Decimal _value);
        //kapila
        [OperationContract]
        DataTable IsDoDaysExceed(string _com, string _pc, Int32 _val);

        //kapila
        [OperationContract]
        int GetTotSadQty(string _com, string _pc, out Decimal _tot_qty);

        //kapila
        [OperationContract]
        DataTable GetAddRecDetails(string _recno);

        //kapila
        [OperationContract]
        string GetConsignmentDetails(DateTime _fromDate, DateTime _toDate, string _com, string _User, string _PB, string _pblevel, string _doctp, out string _err);
        //kapila
        [OperationContract]
        DataTable GetVehRegPayTracker(string _com, string _pc, Int32 _withDtRange, DateTime _from, DateTime _to, string _invoice, string _engine, string _cust, string _chassis, string _reciept, string _vehNo, Int32 _stus);

        //kapila
        [OperationContract]
        Boolean IsInvalidAccount(string _invno, DateTime _dt);

        //kapila
        [OperationContract]
        int SaveInsuHistory(MasterInsuHistory _mstInsuHis);
        //kapila
        [OperationContract]
        DataTable GetInsuHistory(string Com, string _mloc, string _sloc);

        //kapila
        [OperationContract]
        List<PromotionVoucherPara> getProVouPara(string _vouCd);

        //kapila
        [OperationContract]
        DataTable get_pro_dis_def_by_seq(Int32 _seq);
        //kapila
        [OperationContract]
        int GetQuotationTotal(string _qno, out Decimal _totamt);

        //kapila
        [OperationContract]
        int GetQuotationDPTotal(string _qno, out Decimal _totamt);

        //kapila
        [OperationContract]
        DataTable ProcessSUNUpload_JAD(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _acc_period, string _sunID, string _file);

        //kapila
        [OperationContract]
        Int32 CalcAddiProdComm(string _com, string _pc, DateTime _from, DateTime _to);

        //kapila
        [OperationContract]
        DataTable ProcessSUNUpload_Common(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _acc_period, string _type, string _sbtype, string _sunID, string _file);

        //kapila
        [OperationContract]
        DataTable getUserTotalSales(string _com, string _pc, DateTime _date, string _user);

        //kapila
        [OperationContract]
        DataTable getDenominationSum(Int32 _seq);
        //kapila
        [OperationContract]
        Int32 SaveDenominationDet(List<DenomiDet> _lstDenomination, List<DenomiSum> _lstDenomiSum);

        //kapila
        [OperationContract]
        DataTable getSignOnBySeq(Int32 _seq);

        //kapila
        [OperationContract]
        DataTable getPendingDenom(string _com, string _pc, string _user);

        //kapila
        [OperationContract]
        DataTable getDenominationDet(Int32 _seq);

        //kapila
        [OperationContract]
        DataTable Get_veh_ins_renewal_hdr(string com, string pc, string accno);

        //kapila
        [OperationContract]
        int ProcessVehInsPayReq(string _com, string _pc, string _accno, DateTime _dt, Decimal _amt, string _user, DataTable _dtDet, out string _msg);
        //kapila
        [OperationContract]
        List<QoutationDetails> GetSupQoutation(string _com, string _pc, string _quot);

        //kapila
        [OperationContract]
        DataTable get_denom_types(string _com, Int32 _isCash);

        //kapila
        [OperationContract]
        DataTable get_PrintSignOn(Int32 _seq);

        //kapila
        [OperationContract]
        DataTable get_SignOn(string _com, string _pc, string _user, DateTime _dt);

        //kapila
        [OperationContract]
        int GetOpenBal(string _com, string _pc, DateTime _from, DateTime _to, string _user, out Decimal _opBal, out Decimal _closeBal, string _stus);

        //kapila
        [OperationContract]
        DataTable getPromotorTrans(string _cd);

        //kapila
        [OperationContract]
        DataTable getPromotorRedeems(string _cd);

        //kapila
        [OperationContract]
        Int32 UpdateRedeemPromotor(string _mpr_cd, Decimal _mpr_red_pt, Decimal _mpr_red_cs, string _user, string _rem);
        //kapila
        [OperationContract]
        Int32 deleteRedeemPoints(Int32 _seq, string _mgrCode);

        //kapila
        [OperationContract]
        int GetCashRefundTot(DateTime _fromdate, DateTime _todate, string _pc, string _com, out Decimal _value);

        //kapila
        [OperationContract]
        int GetCreditNoteTot(DateTime _fromdate, DateTime _todate, string _pc, string _com, out Decimal _value);

        //kapila
        [OperationContract]
        DataTable getShortSetle(string _com, string _pc, DateTime _month, Int32 _week);

        //kapila
        [OperationContract]
        DataTable GetReceiptByRefNo(string _com, string _pc, DateTime _from, DateTime _to, string _ref);

        //kapila
        [OperationContract]
        Boolean checkDocrealized(string _com, string _pc, DateTime _dt, string _doctp, string _accno, string _ref);

        //kapila
        [OperationContract]
        DataTable ProcessSUNUpload_BankCharges(DateTime _from, DateTime _to, string _com, string _user, string _chanel, string _acc_period, string _sunID, string _file);

        //kapila
        [OperationContract]
        Int32 Update_hpr_ars_dt_defn(List<ArrearsDateDef> _LstdefHeader);

        //kapila
        [OperationContract]
        Boolean checkDocBankState(string _pc, DateTime _dt, string _doctp, string _ref);

        //kapila
        [OperationContract]
        Boolean isValidReqType(string _code);

        //kapila
        [OperationContract]
        Int32 CancelApprovedRequest(string _ref, string _user);

        //kapila
        [OperationContract]
        Boolean CheckAppReqCancelPerm(string _user, string _reqtp);

        //kapila
        [OperationContract]
        DataTable GetApprovedRequests(string _com, string _pc, DateTime _from, DateTime _to, Int32 _isAllDate, string _reqTp);

        //kapila
        [OperationContract]
        Int32 CancelExcessShortFinal(string _pc, DateTime _month, string _user);

        //kapila
        [OperationContract]
        Int32 CancelCashControlFinal(string _com, string _pc, DateTime _month, string _user);

        //kapila
        [OperationContract]
        Int32 UpdateIsSUNUpload(string _com, string _pc, DateTime _from, DateTime _to, Int32 _week);

        //kapila
        [OperationContract]
        DataTable GetBankStmntDetails(string _docTp, string _ref);

        //kapila
        [OperationContract]
        int GetCCRecTot(string _com, DateTime _date, string _accno, out decimal _val);

        //kapila
        [OperationContract]
        Int32 UpdateCashControl(string _com, string _pc, DateTime _month);

        //kapila
        [OperationContract]
        DataTable GetcashControl(string _com, string _pc, DateTime _month);

        //kapila
        [OperationContract]
        DataTable GetcashControlAdj(string _com, string _pc, DateTime _month);

        //kapila
        [OperationContract]
        Int32 UpdateBankRealizationDetails(ScanPhysicalDocReceiveDet _doc, List<BankRealDet> _bnkRlsList, out string _msg);

        //kapila
        [OperationContract]
        Int32 SaveBankAdj(ScanPhysicalDocReceiveDet _doc, string _bsta_com, string _bsta_pc, DateTime _bsta_dt, string _bsta_accno, string _bsta_adj_tp, string _bsta_adj_tp_desc, Decimal _bsta_amt, string _bsta_refno, string _bsta_rem, string _bsta_cre_by, DateTime _month, Int32 _week, string _bnk_id, string _bnk_cd, string _mid);

        //kapila
        [OperationContract]
        DataTable GetBankAdj(string _com, DateTime _date, string _accno);

        //kapila
        [OperationContract]
        DataTable getBankRlsHeader(string _com, DateTime _date, string _accno);

        //kapila
        [OperationContract]
        Int32 UpdateBankRealizationHdr(string _Bsth_com, DateTime _Bsth_dt, string _Bsth_accno, Decimal _Bsth_opbal, Decimal _Bsth_realizes, Decimal _Bsth_prv_realize, Decimal _Bsth_cc, Decimal _Bsth_adj, Decimal _Bsth_clbal, Decimal _Bsth_state_bal, string _Bsth_stus, string _Bsth_cre_by);

        //kapila
        [OperationContract]
        Int32 FinalizeBankRealization(string _Bsth_com, DateTime _Bsth_dt, string _Bsth_accno, string _user, List<BankRealDet> _bnkRlsList = null);

        //kapila
        [OperationContract]
        Int32 UpdateBankRealHdr(string _Bsth_com, DateTime _Bsth_dt, string _Bsth_accno, Decimal _Bsth_opbal, Decimal _Bsth_realizes, Decimal _Bsth_prv_realize, Decimal _Bsth_cc, Decimal _Bsth_adj, Decimal _Bsth_clbal, Decimal _Bsth_state_bal, string _Bsth_stus, string _Bsth_cre_by);

        //kapila
        [OperationContract]
        Int32 UpdateBankRealizationDet(List<BankRealDet> _bnkRlsList, string _Bsth_com, DateTime _Bsth_dt, string _Bsth_accno, Decimal _Bsth_opbal, Decimal _Bsth_realizes, Decimal _Bsth_prv_realize, Decimal _Bsth_cc, Decimal _Bsth_adj, Decimal _Bsth_clbal, Decimal _Bsth_state_bal, string _Bsth_stus, string _Bsth_cre_by, out string _msg);
        //kapila
        [OperationContract]
        DataTable get_bank_realization_det(string _com, string _pc, DateTime _date, string _accno, string _doctp, Decimal _amtfrom, Decimal _amtto, Int32 _is_real_stus, Int32 _nt_in_state, Int32 _oth_bank, Int32 _withNIS, string _ref);

        //kapila
        [OperationContract]
        DataTable get_bank_realization_Hdr(string _com, DateTime _date, string _accno);

        //kapila
        [OperationContract]
        Int32 Process_bank_realization(string _com, string _pc, DateTime _date, string _accno, Int32 _isall, string _user, DateTime _month, Int32 _week);
        //kapila
        [OperationContract]
        Int32 savePromoterDetails(MasterAutoNumber _masterAutoNumber, SalesPromotor _salesPromot, List<string> pc_List, out string _docNo);

        // Nadeeka
        [OperationContract]
        Int32 savePromoter(MasterAutoNumber _masterAutoNumber, MST_PROMOTOR _salesPromot, out string _docNo, out string _msg);

        //kapila 12/8/2014
        [OperationContract]
        DataTable GetSalesPromoterDet(string _com, string _pc, string _code);

        //kapila 30/7/2014
        [OperationContract]
        int ProcessVehInsRentPay(string _com, string _pc, string _accno, DateTime _dt, Decimal _amt, string _user, string _chqno, string _chqbank, string _chqbranch, DateTime _chqdate, string _depbank, string _depbranch, DataTable _dtDet, out string _msg);

        //kapila
        [OperationContract]
        DataTable PrintAdvReceiptRecon(string _com, string _pc, DateTime _from, DateTime _to, string _user);

        //kapila
        [OperationContract]
        Boolean IsInvReversed(string _com, string _pc, string _invno);

        //kapila
        [OperationContract]
        DataTable GetESDRecon(DateTime _month, string _User, string _com, string _pc);

        //kapila
        [OperationContract]
        int GetIncSchPsnCatDesc(string _cd, out string _desc);

        [OperationContract]
        DataTable Process_AgeOfDebtors_Arrears_Chanel(DateTime _asatdate, string _com, string _pc, string _user, string _scheme, string _item, string _cat1, string _cat2, string _cat3, string _model, string _brand, Boolean _Isdetail);

        [OperationContract]
        List<RemSection> GetSection();

        [OperationContract]
        DataTable GetRemSumHeadingBySec(string _section);

        [OperationContract]
        RemitanceSumHeading GetRemitanceData(string _sec, string _code);

        [OperationContract]
        int SaveRemSumHeading(RemitanceSumHeading _remsumheading);

        [OperationContract]
        List<RemitanceSumHeading> get_rem_type_by_sec(string _sec, int p_fix);

        [OperationContract]
        DataTable GetRemSumLimitations(string _type, string _code, string _sec, string _remcode);

        [OperationContract]
        int SaveRemSumLimitations(RemSumDefinitions _remsumdef);

        [OperationContract]
        int SaveRemSummaryDetails(RemitanceSummaryDetail _remsumdet);

        [OperationContract]
        List<RemitanceSummaryDetail> GetRemitanceSumDetail(DateTime _remDate, string _pc);

        [OperationContract]
        List<RemSection> GetAuthorizeTansactions(string _user_id);
        //sachith
        [OperationContract]
        DataTable GetBanks();

        //Shalika
        [OperationContract]
        List<RemSection> GetDistinctSartMainTP(string _user_id);

        //sachith
        [OperationContract]
        DataTable GetReturnCheques(string _loc, string _ref, string _bCode);

        //sachith
        [OperationContract]
        Int32 UpdateReturnCheques(string _ref, string _oldRef, string _bCode, string _oldBcode, string _branch, DateTime _date, string _com, string _pc);

        //kapila
        [OperationContract]
        Int32 UpdateVehRegDefin(Int32 _seq);

        //sachith
        [OperationContract]
        DataTable GetReturnChequeCount(string _ref, string _bCode, string _loc);

        //sachith
        [OperationContract]
        Int32 SaveReturnCheque(ChequeReturn _chequeReturn);

        //sachith
        [OperationContract]
        DataTable GetReturnChequeDetails();

        //kapila
        [OperationContract]
        DataTable GetRemSummaryReport(string _com, string _pc, DateTime _fromDate, DateTime _toDate, string _sec, out Decimal _totval, out Decimal _totvalFinal);

        //kapila
        [OperationContract]
        DataTable GetRemSummaryReport_Win(string _com, string _pc, DateTime _fromDate, DateTime _toDate, string _sec, out Decimal _totval, out Decimal _totvalFinal);


        //kapila
        [OperationContract]
        DataTable ProcessDayEnd(DateTime _date, string _com, string _pc, Int32 _week, string _user, string _insTp);

        //kapila 
        [OperationContract]
        DataTable getRevertChargeDef(string com, DateTime _date);

        //kapila
        [OperationContract]
        Boolean IsRemHeadFixed(string _sec, string _code);

        //kapila 8/8/2012
        [OperationContract]
        int SaveRemAdjustment(RemitanceAdjustment _remAdj, MasterAutoNumber _masterAutoNumber);

        //kapila 8/8/2012
        [OperationContract]
        int GetTotRemitance(string _com, DateTime _from, DateTime _to, string _pc, out Decimal _totvalue);

        //kapila
        [OperationContract]
        int GetPrvDayExcess(DateTime _date, string _pc, string _com, out Decimal _value);

        //kapila 9/8/2012
        [OperationContract]
        int SaveDayEnd(DayEnd _dayEnd);

        //kapila 9/8/2012
        [OperationContract]
        Boolean IsValidDayEndDate(DateTime _date, string _pc, out DateTime _invalidDate);

        //kapila 10/8/2012
        [OperationContract]
        int GetDayEndInit(DateTime _fromdate, DateTime _todate, string _sec, string _code, string _type, string _pc, string _com, out Decimal _value, out Decimal _CIH);

        //kapila 13/8/2012
        //[OperationContract]
        //List<RemAdjTypes> GetAdjTypes();

        [OperationContract]
        List<MasterOutsideParty> GetBusCom(string _type);

        //kapila 21/8/2012
        [OperationContract]
        int GetTotDeductions(DateTime _from, DateTime _to, string _pc, out Decimal _value);

        //kapila
        [OperationContract]
        int GetTotRemAdj(DateTime _from, DateTime _to, string _sec, string _code, string _pc, out Decimal _totamt);

        //kapila 21/8/2012
        [OperationContract]
        Int32 DeleteRemSum(string _com, string _pc, DateTime _date, string _sec, string _code, string _ref);

        //kapila
        [OperationContract]
        DataTable GetRemSummaryReport_without_comm_withdraw(string _com, string _pc, DateTime _fromDate, DateTime _toDate, string _sec, out Decimal _totval, out Decimal _totvalFinal, Int32 _type);

        //kapila
        [OperationContract]
        DataTable GetRemSummaryReport_without_comm_withdraw_win(string _com, string _pc, DateTime _fromDate, DateTime _toDate, string _sec, out Decimal _totval, out Decimal _totvalFinal, Int32 _type);

        //kapila
        [OperationContract]
        DataTable getVehInsReceipts(string _com, string _pc, DateTime _date, string _invno);

        //kapila
        [OperationContract]
        DataTable ProcessSOS(DateTime _from, DateTime _to, string _com, string _pc, string _user);

        //kapila 27/8/2012
        [OperationContract]
        int GetRemSumDet(DateTime _fromdate, DateTime _todate, string _sec, string _code, string _pc, string _com, out Decimal _value, out Decimal _valueFinal);

        //kapila 3/9/2012
        [OperationContract]
        int GetTotDisbForCalc_Excess(DateTime _from, DateTime _to, string _pc, out Decimal _value);

        //kapila 10/9/2012
        [OperationContract]
        DataTable ProcessRemDetail(DateTime _from, DateTime _to, string _com, string _pc, string _user);

        //kapila 13/9/2012
        [OperationContract]
        DataTable ProcessSUNUpload(string _month, DateTime _from, DateTime _to, string _com, string _pc, string _user, string _acc_period, string _sun_user, string _file, string _insuTp);
        //dilshan 31/10/2017
        [OperationContract]
        DataTable EvaluationSUNUpload(string _month, DateTime _from, DateTime _to, string _com, string _pc, string _user, string _acc_period, string _sun_user, string _doctype);
        //dilshan 31/10/2017
        [OperationContract]
        DataTable EvaluationSUNUpload2(string _month, DateTime _from, DateTime _to, string _com, string _pc, string _user, string _acc_period, string _sun_user, string _doctype);
        //dilshan 31/10/2017
        [OperationContract]
        DataTable GetUploadForwardSalesDetails(DateTime _asAtDate, string _User, string _brand, string _model, string _itemcode, string _itemcat1, string _itemcat2, string _itemcat3, string _agetp, decimal _age, string _com, string _pc, string _latestcost, string _otherloc, string _customer, string _user, string _acc_period, string _sun_user);

        [OperationContract]
        int Get_prv_day_CIH(DateTime _date, string _com, string _pc, out Decimal _value);

        //kapila 20/9/2012
        [OperationContract]
        int Generate_SOS_Text_File(string _month, DateTime _from, DateTime _to, string _com, string _pc, string _user, string _acc_period, string _sun_user, string _file);

        //kapila 24/9/2012
        [OperationContract]
        DataTable ProcessSUNUpload_Receipt(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _acc_period, string _sunID, string _file);

        [OperationContract]
        DataTable ProcessSUNUpload_Invoice(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _acc_period, Int32 _is_dealer, string _sunID, string _file);

        //kapila 27/9/2012
        [OperationContract]
        DataTable ProcessReceipt_Listing(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _recType);
        [OperationContract]
        DataTable ProcessReceipt_Listing_win(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _recType, string _prefix, string _payType, Int32 _iswithtime, DateTime _filterfromdt, DateTime _filtertodt);

        //kapila 1/10/2012
        [OperationContract]
        int Print_HP_Agreement(string _accNo);

        //kapila 6/10/2012
        [OperationContract]
        DataTable Process_AgeOfDebtors_Arrears(DateTime _asatdate, string _com, string _pc, string _user, string _scheme);

        //kapila 15/10/2012
        [OperationContract]
        DataTable Process_Debtor_Sales_Settlement(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _checkfromdt, Int32 _isOuts, string _cusdcd);

        //kapila 16/10/2012
        [OperationContract]
        Decimal Get_Arrears(string _pc, string _accNo, DateTime _arrDate, DateTime _supDate);

        //kapila 18/10/2012
        [OperationContract]
        DataTable Process_Age_Anal_Debt_Outstand(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _cust, Int32 _tp);

        [OperationContract]
        DataTable Process_Age_Anal_Debt_OutstandDCN(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _cust, Int32 _tp);

        //sachith 19/10/2012
        [OperationContract]
        RemitanceSummaryDetail GetRemitanceAdjesmentDetails(string _com, string _pc, DateTime _date, string _section, string _code, string _ref);

        //sachith 22/10/2012
        [OperationContract]
        Int32 UpdateRemitanceAdjusment(RemitanceSummaryDetail _remSum);

        //sachith 22/10/2012
        [OperationContract]
        List<RemitanceSummaryDetail> GetRemitanceSumDetailAdjusment(DateTime _remDate, string _pc);

        //kapila
        [OperationContract]
        Int32 UPDATE_VOUCHER_END(string _com, string _vou, DateTime _dt, string _user);

        //sachith 22/10/2012
        [OperationContract]
        Int32 SaveRemitanceStatus(RemitanceStatus _remSta);

        //kapila 24/10/2012
        [OperationContract]
        Int32 SaveExcessTrans(ExcessShortTrna _excsShortRem);

        //kapila 25/10/2012
        [OperationContract]
        DataTable GetExcsSettlementDridData(string _pc, DateTime _mnth);

        //kapila 25/10/2012
        [OperationContract]
        Int32 DeleteExcsSettlement(string _com, string _pc, DateTime _month, DateTime _txnDate);

        //kapila 25/10/2012
        [OperationContract]
        DataTable GetExcsBalanceDridData(string _com, string _pc);

        //kapila 25/10/2012
        [OperationContract]
        DataTable GetExcessBalance(string _com, string _pc, DateTime _month, out Decimal _totval);

        //kapila 26/10/2012
        [OperationContract]
        List<ExcessRemitTypes> GetExcsShortRemitType(Int16 _isExcess);

        //kapila 30/10/2012
        [OperationContract]
        DataTable GetExcsStatus(string _pc, DateTime _month, out string _status, out string _id);

        //kapila 30/10/2012
        [OperationContract]
        int ProcessExcessShort(string _com, string _pc, Int32 _year, Int32 _month, string _ID, string _user);

        //kapila 31/10/2012
        [OperationContract]
        Int32 SaveExcessHeader(ExcessShortHeader _excsShortHead);

        //kapila 31/10/2012
        [OperationContract]
        Int32 DeleteExcsShortDetail(string _ID);

        //kapila 31/10/2012
        [OperationContract]
        DataTable GetExcsShortGridData(string _com, string _pc, DateTime _month);

        //kapila 31/10/2012
        [OperationContract]
        Int32 SaveExcessDetails(ExcessShortDet _excsShortDet);

        //kapila 31/10/2012
        [OperationContract]
        DataTable GetExcsShortOthRemGridData(string _ID);

        //kapila 31/10/2012
        [OperationContract]
        Int32 DeleteExcsShortOthRem(string _ID, Int32 _week, string _sec, string _code);

        //kapila 1/11/2012
        [OperationContract]
        int ConfirmExcessShort(string _com, string _pc, DateTime _month, DateTime _date, string _ID, string _desc, string _user);

        //kapila 1/11/2012
        [OperationContract]
        Boolean IsDayEndDone(string _com, string _pc, DateTime _from, DateTime _to);
        [OperationContract]
        Boolean IsDayEndDone_win(string _com, string _pc, DateTime _from, DateTime _to);

        //kapila 1/11/2012
        [OperationContract]
        Boolean IsPrvDayTxnsFound(string _com, string _pc, DateTime _date);

        //kapila 1/3/2013
        //[OperationContract]
        //Boolean IsPrvDayTxnsFound_rec(string _com, string _pc, DateTime _date);

        //kapila 2/11/2012
        [OperationContract]
        Int32 ProcessFinalizeDayEnd(DateTime _date, string _com, string _pc);

        //kapila 3/11/2012
        [OperationContract]
        int PrintExcessShort(string _com, string _pc, string _ID, DateTime _date, string _status, string _user, out Decimal _prvBal, out Decimal _curBal);

        //kapila 9/11/2012
        [OperationContract]
        int Process_Excess_short_statement(DateTime _date, string _com, string _pc, string _user);

        //6/11/2012
        [OperationContract]
        int GetClosingBalance(DateTime _asAtDate, string _accno, out Decimal _value);

        //kapila 13/11/2012
        [OperationContract]
        int GetFirstDayEndDate(string _com, string _pc, DateTime _from, DateTime _to, out DateTime _date);

        //kapila 15/11/2012
        [OperationContract]
        int ProcessNoOfAccounts(DateTime _from, DateTime _to, DateTime _from1, string _com, string _user, Int32 _status);

        //kapila 21/11/2012
        [OperationContract]
        int Process_daily_trans_sum(DateTime _from, DateTime _to, string _com, string _pc, string _user);
        #region Scan/Physical Document

        //Shani 23-10-2012
        [OperationContract]
        DataTable GetWeeks_on_month(Int32 month, Int32 year, Int32 weekNo);

        //Shani 24-10-2012
        [OperationContract]
        DataTable Get_GNT_RCV_DSK_DOC(string com, string pc, DateTime monthYear, Int32 week, string doc_tp, Int32 _isDocRec);

        //Shani 02-05-2013
        [OperationContract]
        DataTable Get_GNT_RCV_DSK_DOC_onDateRange(string com, string pc, DateTime fromDt, DateTime toDt, string doc_tp, Int32 _isDocRec);

        //Shani 04-05-2013
        [OperationContract]
        DataTable Get_SunUploaded_gnt_rcv_dsk_doc(string com, string pc, DateTime monthYear, Int32 week, string doc_tp);


        //Shani 25-10-2012
        [OperationContract]
        Int32 Update_GNT_RCV_DSK_DOC(List<ScanPhysicalDocReceiveDet> updateDoc_list, Int32 isa_scanUpdate, Int32 isa_sunUpdate, Int32 isa_rcvUpdate);

        //Shani 26-10-2012
        [OperationContract]
        Int32 Save_GNT_RCV_DSK_DOC(string createBy, DateTime createDt, string com, string pc, Int32 month, Int32 year, Int32 week, DateTime monthYear);

        //Shani 26-10-2012
        [OperationContract]
        Int32 new_Save_GNT_RCV_DSK_DOC(string createBy, DateTime createDt, string com, string pc, Int32 WEEK, DateTime frmDt, DateTime toDt, string _type, DateTime _month);

        //Shani 26-10-2012
        [OperationContract]
        Int32 Delete_GNT_RCV_DSK_DOC(string _com, string _pc, DateTime monthYear, Int32 week);

        //kapila
        [OperationContract]
        Int32 UpdateBankAdjPC(string _com, string _pc, DateTime _date, string _accno, string _adjtp, string _ref, string _newpc, Decimal _val, string _newrem);

        //Shani 27-04-2013
        [OperationContract]
        Int32 Delete_GNT_RCV_DSK_DOC_new(string _com, string _pc, DateTime fromDt, DateTime toDt);

        //Shani 27-10-2012
        [OperationContract]
        Dictionary<string, Decimal> Get_RemDet(string com, string pc, DateTime date);

        //Shani 30-10-2012
        [OperationContract]
        DataTable Get_ShortBankDocs(string com, string pc, DateTime monthYear, Int32 week, string doc_tp, string _accno, Int32 _withdtrange, DateTime _from, DateTime _to);

        //Shani 30-10-2012
        [OperationContract]
        ScanPhysicalDocReceiveDet Get_GNT_RCV_DSK_DOC_on_Seq(Int32 seqNum);

        //Shani 31-10-2012
        [OperationContract]
        Int32 saveExtraDoc(ScanPhysicalDocReceiveDet _doc, Boolean isShortBank);

        //Shani 31-10-2012
        [OperationContract]
        DataTable Get_GNR_RCV_DSK_DOC_Types();

        //Shani 02-11-2012
        [OperationContract]
        DataTable GET_BANKS_of_PC_on_docType(string com, string pc, string doc_tp);

        #endregion Scan/Physical Document

        //Shani 05-11-2012
        [OperationContract]
        Boolean IsPeriodClosed(string _com, string _pc, string type, DateTime _date);

        //kapila 19/12/12
        [OperationContract]
        DataTable Process_Debtor_Sales_Receipts(DateTime _from, DateTime _to, string _com, string _pc, string _user);

        #region  Monthly Summary Period Definition
        //Shani 19-11-2012
        [OperationContract]
        Int32 Save_gnr_week(Int32 _Gw_year, Int32 _Gw_month, Int32 _Gw_week, DateTime _Gw_from_dt, DateTime _Gw_to_dt, string _Gw_cre_by, List<string> _company);

        //Shani 20-11-2012
        [OperationContract]
        List<GnrWeek> Get_ListOfWeeks_on_month(Int32 month, Int32 year, Int32 weekNo, string _com);

        //Shani 21-11-2012
        [OperationContract]
        List<ArrearsDateDef> Get_ArrearsDateDef(string com, DateTime arrDt, string partyTp, string partyCode);

        //Shani 21-11-2012
        [OperationContract]
        Int32 Save_hpr_ars_dt_defn(List<ArrearsDateDef> _defHeader);
        #endregion

        //Shani 10-01-2013
        [OperationContract]
        Int32 Save_AC_Job_ManagerClaim_Remitance(RemitanceSummaryDetail _remSumDet);

        //Shani 11-01-2013
        [OperationContract]
        DataTable GetRemSummary(string _pc, DateTime _fromDate, DateTime _toDate, string _sec);

        //kapila 17/1/2013
        [OperationContract]
        DataTable Process_AgeOfDebtors_Arrears_Win(DateTime _asatdate, string _com, string _pc, string _user, string _scheme, string _item, string _cat1, string _cat2, string _cat3, string _model, string _brand, Boolean _Isdetail, string _showJobs, out DataTable _jobs);

        [OperationContract]
        string Process_AgeOfDebtors_Arrears36(DateTime _asatdate, string _com, string _user, string _scheme, string _item, string _cat1, string _cat2, string _cat3, string _model, string _brand, Int16 _noof_months, Boolean _Isdetail, out string _err);

        //kapila
        [OperationContract]
        string Process_No_of_pending_app(string _com, string _user, out string _err);

        //Sanjeewa 
        [OperationContract]
        string Process_GivenPaymode_Collection(DateTime _fromdate, DateTime _todate, string _com, string _paymode, string _user, out string _err);

        //kapila 17/1/2013
        [OperationContract]
        DataTable Process_AgeOfDebtors_Arrears_Sum(DateTime _asatdate, string _com, string _pc, string _user, string _scheme, string _item, string _cat1, string _cat2, string _cat3, string _model, string _brand, Boolean _Isdetail);

        //kapila 28/1/2013
        [OperationContract]
        DataTable get_Rem_Sum_Rep(string _com, string _pc, DateTime _from, DateTime _to);

        //kapila 28/1/2013
        [OperationContract]
        DataTable get_Rtn_Chq(string _pc, DateTime _from, DateTime _to);

        //kapila 30/1/2013
        [OperationContract]
        DataTable get_Rem_Sum_Rep_View(DateTime _date, string _com, string _pc, string _user, string _sec, Decimal _comm, string _insTp);

        //kapila
        [OperationContract]
        DataTable getVehInsReceipts4Payment(string _com, string _pc, DateTime _date, string _invno);

        //kapila 30/1/2013
        [OperationContract]
        DataTable GetRemitanceSumDet(DateTime _remDate, string _pc);

        //kapila 22/2/2013
        [OperationContract]
        DataTable get_Trans_Variation(string _com, DateTime _from, DateTime _to, string _type, string _user, string _perm);

        //Shani 27-02-2013
        [OperationContract]
        Int32 ChequeReturn(RecieptHeader recieptHeadder, RecieptItem recieptItem, ChequeReturn chequeReturn);

        //kapila 11/3/2013
        [OperationContract]
        DataTable Print_RCC_Receipt(string _rccNo);

        //kapila
        [OperationContract]
        DataTable Print_ACInsNote(string _rccNo);


        //kapila 12/3/2013
        [OperationContract]
        DataTable Print_RCC_Ret_Condition(string _rccNo);

        //kapila
        [OperationContract]
        DataTable get_Rem_Sum_Rep_View_dt_range(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _sec, Decimal _comm, string _insTp);

        //kapila 13/3/2013
        [OperationContract]
        Boolean IsTxnFoundAfterDayEnd(string _com, string _pc, DateTime _date, string _type);

        //kapila 14/3/2013
        [OperationContract]
        int GetTotCollBonus(DateTime _from, DateTime _to, string _sec, string _code, string _pc, out Decimal _totamt);

        //kapila 14/3/2013
        [OperationContract]
        Boolean IsOnceRemType(string _sec, string _code);

        //kapila 14/3/2013
        [OperationContract]
        Boolean IsOnceRemDataExist(string _com, string _pc, string _sec, string _code, DateTime _month);

        //Shani 19-03-2013
        [OperationContract]
        Int32 Cancel_ReturnChequesSettlement(string _ref, Int32 _seq, Decimal _reductAmt);

        //Shani 19-03-2013
        [OperationContract]
        DataTable get_Rtn_Chq_DET(string _pc, DateTime _from, DateTime _to);

        //Nadeeka 20-03-2013
        [OperationContract]
        DataTable ProcessPersonalChequeStatement(string _user_id, DateTime _fromdate, DateTime _toDate, string _Company, string _pc);

        //Darshana 07-09-2013
        [OperationContract]
        DataTable ProcessSUNUpload_RtnCheque(DateTime _from, DateTime _to, string _com, string _user, string _acc_period, string _sunID, string _file);

        //Darshana 09-09-2013
        [OperationContract]
        DataTable ProcessSUNUpload_RtnChequeSettlement(DateTime _from, DateTime _to, string _com, string _user, string _acc_period, string _sunID, string _file);

        //Nadeeka 25-06-2013
        [OperationContract]
        DataTable ProcessSunUploadElite(string _user_id, DateTime _fromdate, DateTime _toDate, string _Company, string _Profit);

        //Nadeeka 25-06-2013
        [OperationContract]
        DataTable ProcessSunUploadDutyFree(string _user_id, DateTime _fromdate, DateTime _toDate, string _Company, string _Profit);

        //Nadeeka 25-06-2013
        [OperationContract]
        DataTable ProcessSunUploadLoyalty(string _user_id, DateTime _fromdate, DateTime _toDate);

        //written by Sanjeewa 2013-04-05
        [OperationContract]
        DataTable GetADLoanSettDetails(DateTime _fromDate, DateTime _toDate, string _User);

        //written by Sanjeewa 2014-06-14
        [OperationContract]
        DataTable GetESDStatement(DateTime _fromDate, DateTime _toDate, string _User, string _com, string _pc);

        //written by Sanjeewa 2014-09-09
        [OperationContract]
        string GetCollBonusDtl(DateTime _fromDate, DateTime _toDate, string _com, string _status, string _User, out string _err);

        //written by Sanjeewa 2014-07-10
        [OperationContract]
        DataTable GetRcvDskProcessed(DateTime _fromDate, DateTime _toDate, string _User, string _com, string _pc);

        //written by Sanjeewa 2014-01-27
        [OperationContract]
        DataTable GetRcvDeskRemSumDetails(int _Year, int _Month, int NoofMonth, string _User, string _Com, string _Pc);

        //written by Sanjeewa 2014-02-03
        [OperationContract]
        DataTable GetShortSettDetails(DateTime _fromDate, DateTime _toDate, string _User, string _Com, string _Pc);

        //written by Sanjeewa 2013-04-08
        [OperationContract]
        DataTable GetClaimExpVoucherDetails(DateTime _fromDate, DateTime _toDate, string _User);

        ////kapila 19/3/2013
        [OperationContract]
        Boolean IsRemLimitFound(string _ptype, string _pcode, DateTime _date, string _sec, string _code, out DateTime _from, out DateTime _to, out Decimal _val);
        //Boolean IsRemLimitFound(string _ptype, string _pcode, DateTime _date, string _sec, string _code);

        //Shani 19-03-2013
        [OperationContract]
        DataTable get_rtn_chq_byBankChqNo(string p_bank, string p_chqNo);


        //kapila 27/3/2013
        [OperationContract]
        DataTable ProcessFinalReminder(string _accno, string _type, DateTime _due);

        //kapila 27/3/2013
        [OperationContract]
        DataTable ProcessHPArrearsPrint(string _accno, string _type, DateTime _due);

        //kapila 9/4/2013
        [OperationContract]
        DataTable Process_Short_Rem_statement(string _user, string _com, DateTime _asatdate, Int32 _isAsAt, DateTime _from, DateTime _to);

        //Nadeeka 31-jan-2014
        [OperationContract]
        DataTable Process_Excess_Rem_statement(string _user, string _com, DateTime _asatdate, Int32 _isAsAt, DateTime _from, DateTime _to);

        //sachith 8/4/2013
        [OperationContract]
        VoucherPrintExpenseDefinition GetVoucherExpense(string _com, string _expense);

        //sachith 23/4/2013
        [OperationContract]
        List<VoucherPrintExpenseDefinition> GetVoucherExpenseByCode(string _expense);

        //sachith 7/4/2013
        [OperationContract]
        int SaveVoucherExpenseDefinition(List<VoucherPrintExpenseDefinition> _expense);

        //kapila 10/4/2013
        [OperationContract]
        int SaveShortBanking(Short_Banking _short_Banking);

        //sachith 10/4/2013
        [OperationContract]
        List<VoucherPrintExpenseDefinition> GetAllVoucherExpense(string _com, DateTime _date);

        //sachith 11/4/2013
        [OperationContract]
        int SaveVoucherHeader(VoucherHeader _voucher);

        //sachith 11/4/2013
        [OperationContract]
        int SaveVoucherDefinition(VoucherDetails _voucher);

        //sachith 16/4/2013
        [OperationContract]
        int SaveVoucher(VoucherHeader _voucherHeader, List<VoucherDetails> _voucherDetails, MasterAutoNumber _voucherAuto, out string _docNo);

        //sachith 16/4/2013
        [OperationContract]
        VoucherHeader GetVoucher(string _com, string _vou);
        //sachith 16/4/2013
        [OperationContract]
        List<VoucherDetails> GetVoucherDetail(string _vou);
        //sachith 16/4/2013
        [OperationContract]
        int CancelVoucher(string _com, string _vou, Int32 act, Int32 print, string mod_by, DateTime mod_dt);
        //sachith 16/4/2013
        [OperationContract]
        List<VoucherHeader> GetVoucherSearch(int cancel, int print, string _com, DateTime _from, DateTime _to, string _vou);

        //kapila 19/4/2013
        [OperationContract]
        DataTable GetManualDocCheckListPrint(string _com, string _pc, DateTime _month, Int32 _week);

        [OperationContract]
        DataTable CashControlPrint(string _user, string _com, string _pc, Int32 _year, Int32 _month, DateTime _from, DateTime _to, string _stus);

        //kapila
        [OperationContract]
        DataTable CashControlCashPrint(string _user, string _com, string _pc, Int32 _year, Int32 _month, DateTime _from, DateTime _to);

        //kapila
        [OperationContract]
        DataTable RemitanceCheckListPrint(string _user, string _com, string _pc, DateTime _from, DateTime _to);

        //kapila 8/5/2013
        [OperationContract]
        DataTable ProcessSUNUpload_ScanDocs(DateTime _month, Int32 _week, DateTime _fromdate, DateTime _to, string _com, string _user, string _acc_period, string _sunID, string _type, string _mnth_str, string _insuTp);

        //Shani 09-05-2013
        [OperationContract]
        Int32 SaveReturnCheque_NEW(RecieptHeader receiptHdr, Deposit_Bank_Pc_wise objDeposit, List<RecieptItem> receiptItmList, ChequeReturn _chequeReturn, List<ReturnChequeCalInterest> _IntCal, RemitanceSummaryDetail _remsumdet, out string RtnReceiptNo);

        //kapila
        [OperationContract]
        Int32 DeleteRemSummary(string _com, string _pc, DateTime _date, string _sec, string _code);

        //kapila 13/5/2013
        [OperationContract]
        Boolean IsWeekDefFound(Int32 _year, Int32 _month, Int32 _week, string _com);

        [OperationContract]
        Boolean IsCashControlFinalized(string _com, string _pc, DateTime _month);

        //kapila
        [OperationContract]
        DataTable CashControlReconPrint(string _user, string _com, string _pc, DateTime _month);

        //Sanjeewa
        [OperationContract]
        DataTable CrcdReconciliationDetails(DateTime _asatdate, string _account, string _com, string _pc, string _user);

        //Sanjeewa
        [OperationContract]
        string CrcdReconciliation1Details(DateTime _asatdate, string _account, string _com, string _user, out string _err);

        //kapila 18/5/2013
        [OperationContract]
        DataTable PrintSignOff(string _com, string _pc, DateTime _from, DateTime _to, string _user, Int32 _isGroup, Int32 _isAll);

        //kapila
        [OperationContract]
        DataTable PrintCancelInvs(string _com, string _pc, DateTime _from, DateTime _to, string _user, Int32 _isGroup, Int32 _isAll);

        //kapila
        [OperationContract]
        DataTable PrintReverseInv(string _com, string _pc, DateTime _from, DateTime _to, string _user, Int32 _isGroup, Int32 _isAll);

        //kapila 
        [OperationContract]
        DataTable get_SignOff_Rec(string _user, string _pc, DateTime _from, DateTime _to, out Decimal _cashtotal);

        //kapila 
        [OperationContract]
        DataTable get_SignOff_less(string _pc, DateTime _from, DateTime _to);

        //Shani on 23-05-2013
        [OperationContract]
        Boolean Is_PC_Finalized(string _com, string _pc, DateTime date);

        //kapila
        [OperationContract]
        int GetLastDayEndDate(string _com, string _pc, DateTime _from, out DateTime _date);

        //kapila
        [OperationContract]
        Int32 FinalizeDayEnd(RemitanceStatus _remSta, DateTime _from, DateTime _to, string _com, string _pc, string _user, out string _msg);

        //kapila
        [OperationContract]
        DataTable get_non_post_txns(string _com, string _pc, DateTime _from, DateTime _to, string _user);

        //kapila
        [OperationContract]
        Boolean IsCommCalculated(string _invno);

        //kapila
        [OperationContract]
        DataTable Print_RCC_Listing_Report(string _com, string _pc, DateTime _from, DateTime _to, string _user, string _rcctp, string _agent, string _colMethod, string _closeTp, string _status, string _cat1, string _cat2, string _cat3, string _brand, string _model, string _item);

        [OperationContract]
        DataTable Print_RCC_Job_Report(string _com, string _pc, DateTime _from, DateTime _to, string _user);

        //kapila
        [OperationContract]
        Int32 UpdatePrvDayCIH(string _user, string _com, string _pc, DateTime _date, Int32 _week);

        [OperationContract]
        DataTable PrintAdvReceipts(string _com, string _pc, DateTime _from, DateTime _to, string _user, Int32 _isGroup, Int32 _isAll);

        [OperationContract]
        DataTable PrintCreditInvoices(string _com, string _pc, DateTime _from, DateTime _to, string _user, Int32 _isGroup, Int32 _isAll);

        [OperationContract]
        DataTable GetIncSch(string _circular, string _ref);

        [OperationContract]
        DataTable GetIncSchDet(string _circular, string _ref);
        [OperationContract]
        DataTable GetIncSchPerson(string _circular, string _ref);
        [OperationContract]
        DataTable GetIncSchPC(string _circular, string _ref);
        [OperationContract]
        DataTable GetIncSchSalesTp(string _circular, string _ref);
        [OperationContract]
        DataTable GetIncSchInc(string _circular, string _ref);
        [OperationContract]
        DataTable GetIncSchIncDt(string _circular, string _ref);

        //Shani on 23-05-2013
        [OperationContract]
        DataTable GetFromCurr_to_ToCurr(string _fromCur, string to_curr, DateTime TODAY, string company);

        [OperationContract]
        int ProcessProductBonus(string _com, string _circ, string _ref, DateTime _from, DateTime _to, string _user);

        [OperationContract]
        Int32 Process_OtherLocation_Selling_Comm(DateTime _from, DateTime _to, string _com, string _loc, string _defPC, Int32 _week, string _user);

        //kapila
        [OperationContract]
        DataTable PrintCashRefunds(string _com, string _pc, DateTime _from, DateTime _to, string _user, Int32 _isGroup, Int32 _isAll);

        //sanjeewa
        [OperationContract]
        DataTable getProductBonusDetails(string _user, DateTime _from, DateTime _to, string _com, string _pc);

        //sanjeewa
        [OperationContract]
        DataTable GetInternalPayVouDetails(DateTime _fromDate, DateTime _toDate, string _Exec, string _Com, string _Pc, string _RepTp, string _User, out string _err, out string _filePath);

        //sanjeewa
        [OperationContract]
        DataTable GetESDDetails(DateTime _fromDate, DateTime _toDate, string _Exec, string _Com, string _Pc, string _RepTp, string _User);

        //kapila
        [OperationContract]
        Int32 IsValidWeekDataRange(Int32 _year, Int32 _month, DateTime _from, DateTime _to, string _com);

        //darshana on 04-09-2013
        [OperationContract]
        List<ReturnChequeCalInterest> get_calrtn_int(string _com, string _pc, string _ref, string _bank, Int32 _from, Int32 _to, DateTime _AsAtDat);

        //kapila
        [OperationContract]
        DataTable PrintAdvReceiptRegistry(string _com, DateTime _from, DateTime _to, string _user);

        //kapila
        [OperationContract]
        DataTable PrintDiriyaFund(string _com, DateTime _from, DateTime _to, string _user);

        //kapila
        [OperationContract]
        DataTable PrintOverandShort(string _com, DateTime _date, string _user);
        //dilshan
        [OperationContract]
        DataTable PrintOverandShortDetail(string _pc, string _com, DateTime _dateFrom, DateTime _dateTo, string _user);
        //dilshan
        [OperationContract]
        DataTable PrintOverandShortMovement(string _pc, string _com, DateTime _dateFrom, DateTime _dateTo, string _user);
        //dilshan
        [OperationContract]
        DataTable PrintOverandShortSum(string _pc, string _com, DateTime _dateFrom, DateTime _dateTo, string _user);
        //kapila
        [OperationContract]
        DataTable PrintCashCommDefHead(string _user, DateTime _from, DateTime _to, string _circ);

        [OperationContract]
        DataTable PrintCashCommDefDet(string _user, DateTime _from, DateTime _to, string _circ);

        //kapila
        [OperationContract]
        Int16 Is_Prod_Bonus_Finish(string _userID);

        //kapila
        [OperationContract]
        DataTable PrintDeliveredSalesForComm(DateTime _from, DateTime _to, string _com, string _user);

        //kapila
        [OperationContract]
        DataTable PrintHPInformation(DateTime _from, DateTime _to, string _com, string _user, string _pc);
        //darshana - 29-10-2013
        [OperationContract]
        DataTable GetReturnChequesFromRemSum(string _pc, string _ref, string _bCode);

        //Written by Prabhath on 05/11/2013
        [OperationContract]
        int UpdateSalesSummary(string _company, string _profitcenter, DateTime _fromdate, DateTime _todate, string _user, out string _msg);

        //Written by Prabhath on 05/11/2013
        [OperationContract]
        DataTable GetScanChequeDetail(string _company, string _profitcenter, DateTime _fromdate, DateTime _todate);
        //darshana on 09-11-2013
        [OperationContract]
        DataTable GetChequesFromRemDet(string _com, string _ref);

        //kapila
        [OperationContract]
        DataTable Get_gnt_inc_calc(string _circular, string _ref);
        //kapila
        [OperationContract]
        DataTable Get_dt_inc_calc_dt(string _circular, string _ref);
        //kapila
        [OperationContract]
        DataTable Get_dt_inc_calc_inc(string _circular, string _ref);
        //kapila
        [OperationContract]
        DataTable Get_dt_inc_sch_inc(string _circular, string _ref);
        //kapila
        [OperationContract]
        DataTable Get_dt_inc_sch_inc_dt(string _circular, string _ref);
        //kapila
        [OperationContract]
        DataTable Get_dt_inc_sch_dt(string _circular, string _ref);
        //kapila
        [OperationContract]
        Int32 Process_group_sale_Selling_Comm(string _com, string _pc, DateTime _date);
        //kapila
        [OperationContract]
        Int32 confirm_inc_sch(string _circ, string _user);
        [OperationContract]
        DataTable Get_dt_inc_calc_inv(string _circular, string _ref);
        //kapila
        [OperationContract]
        VoucherHeader GetValidVoucher(DateTime _date, string _com, string _vou);
        //kapila
        [OperationContract]
        DataTable ProcessSUNUpload_PayVouEntry(DateTime _from, DateTime _to, string _com, string _user, string _chanel, string _acc_period, string _sunID, string _file);

        [OperationContract]
        DataTable ProcessSUNUpload_PayVouClaim(DateTime _from, DateTime _to, string _com, string _user, string _chanel, string _acc_period, string _sunID, string _file);

        //kapila
        [OperationContract]
        Int32 GetReportAllowPeriod(string _com, Int32 _id);

        //kapila
        [OperationContract]
        int getHPAccValue(string _com, string _pc, string _acc, out Decimal _val);

        //kapila
        [OperationContract]
        Int16 IsHOFinalized(string _com, string _pc, DateTime _date);

        //kapila
        [OperationContract]
        DataTable Get_PBonus_VOu(string _ref, string _com, string _pc);

        //kapila
        [OperationContract]
        Int32 UpdatePBonusVoucher(MasterAutoNumber _masterAutoNumber, string _com, List<PBonusVouHeader> _vouHeader, List<PBonusVouDetail> _vouDet, List<PBonusVouDedc> _vouDed, List<PBonusVouDedc> _vouRef, out string _docNo);
        //Int32 UpdatePBonusVoucher(string _com, string _pc, string _circ, string _sch, Decimal _pbih_refund, string _pbih_refund_rem, string _pbih_refund_upd_by, Decimal _pbih_acc_ded, string _pbih_acc_rem, string _pbih_acc_upd_by, Decimal _pbih_cc_ded, string _pbih_cc_rem, string _pbih_cc_upd_by, Decimal _pbih_crd_ded, string _pbih_crd_rem, string _pbih_crd_upd_by, Decimal _pbih_net, string _pbih_prep_by, string _pbih_chk_by, string _pbih_auth_by, string _man);

        //kapila
        [OperationContract]
        Int32 ConfirmPBonusVoucher(string _sch, Int32 _isAcc, Int32 _isCC, Int32 _isCrd);

        //kapila
        [OperationContract]
        DataTable GetValidPBVoucher(DateTime _date, string _com, string _pc, string _vou);

        //kapila 
        [OperationContract]
        DataTable GetValidColBonusVoucher(string _com, string _pc, string _vou);

        //kapila
        [OperationContract]
        DataTable PrintPBonusVoucher(string _ref, string _com, string _pc);

        //kapila
        [OperationContract]
        Int32 Save_PBonus_Vou_Header(PBonusVouHeader _PBVouHdr);
        [OperationContract]
        Int32 Save_PBonus_Vou_Det(PBonusVouDetail _PBVouDet);
        [OperationContract]
        DataTable Get_PBVoucher_detail(string voucherNo);
        [OperationContract]
        DataTable Get_PBVoucher_header(string voucherNo);
        [OperationContract]
        DataTable ProcessSUNUpload_PBonus(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _acc_period, Int32 _is_dealer, string _sunID, string _file);

        //kapila
        [OperationContract]
        Int32 UpdateShortSettlement(Int32 _seq, Decimal _val, Int32 _isSUN, string _rem, DateTime _month, Int32 _week, string _user, DateTime _date);

        //kapila
        [OperationContract]
        DataTable getDocSettle(string com, string pc, DateTime _from, DateTime _to, Int32 _week, string doc_tp);

        //kapila
        [OperationContract]
        Int32 Save_Short_Settlements(string createBy, DateTime createDt, string com, string pc, Int32 WEEK, DateTime frmDt, DateTime toDt, string _type);

        //kapila
        [OperationContract]
        Int16 confirmPBVoucher(string _com, string _pc, string _vou, Int32 _isChk, Int32 _isAuth);

        //kapila
        [OperationContract]
        Int32 UpdatePBDeductions(string _vRef, Decimal _vgross, Decimal _vccDed, string _vccRem, string _vccUpdBy, Decimal _vcrdDed, string _vcrdRem, string _vcrdUpdBy, Decimal _vnet, string _usrType, List<PBonusVouDedc> _vouDed);

        //kapila
        [OperationContract]
        int GetTotalSettlements(string _com, string _pc, DateTime _month, Int32 _week, out Decimal _value);

        //kapila 21/3/2014
        [OperationContract]
        DataTable ProcessSUNUpload_AOAInvoice(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _acc_period, Int32 _is_dealer, string _sunID, string _file);

        #region Brand Reports
        [OperationContract]
        DataTable GetBrandRep01Details(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Com, string _Pc, string _User, string _repTp, out string _err, out string _filePath);

        #endregion

        //Tharaka 09-09-2014
        [OperationContract]
        DataTable GetChequeVoucherAccount(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Tharaka 09-09-2014
        [OperationContract]
        DataTable GetInvoiceForChqEntry(string _com, string _PC);


        //Tharaka 10-09-2014
        [OperationContract]
        DataTable GET_ACC_DETAILS(string P_COM, string P_ACC);

        //Tharaka on 11-09-2014
        [OperationContract]
        int SaveJournalVouvher(VoucherHeader _voucherHeader, List<InterVoucherDetails> _voucherDetails, MasterAutoNumber _voucherAuto, out string _docNo, bool isSingleCheque);

        //Tharaka 16-09-2014
        [OperationContract]
        List<InterVoucherDetails> GetChequeVoucherDetail(string _vou);

        //Tharaka 16-09-2014
        [OperationContract]
        int UpdateChequeVoucher(VoucherHeader _voucherHeader, List<InterVoucherDetails> _voucherDetails);

        //Tharaka 17-09-2014
        [OperationContract]
        int UpdateVoucherHeader(VoucherHeader _voucher);

        //Tharaka 22-09-2014
        [OperationContract]
        int UpdateVoucherChequeNumber(string com, string chequeNUm, string user, string voucherNum);

        //Tharaka 23-09-2014
        [OperationContract]
        bool ValidateVoucherUser(string User, DateTime date);
        //shalika 01/10/2014
        [OperationContract]
        Int32 Process_bank_realization_crcd(string _com, string _pc, DateTime _date, string _accno, Int32 _isall, string _user, DateTime _month, Int32 _week);
        //shalika 01/10/2014
        [OperationContract]
        DataTable get_crcd_realization_det(string _com, string _pc, DateTime _date, string _accno, string _doctp, Decimal _amtfrom, Decimal _amtto, Int32 _is_real_stus, Int32 _nt_in_state, Int32 _oth_bank, Int32 _withNIS, string MID);
        //shalika 02/10/2014
        [OperationContract]
        Int32 UpdateCRCDRealizationDet(List<BankRealDet> _bnkRlsList, out string _msg);

        //Tharka 2014-10-30
        [OperationContract]
        List<VoucherHeader> GetVoucherSearchToPrint(int cancel, int print, string _com, DateTime _from, DateTime _to, string _vou);
        //shanuka 14/10/2014
        [OperationContract]
        DataTable ProcessSUNUpload_ChequePrnt(DateTime _from, DateTime _to, string _com, string _user, string accNo, string _acc_period, string _sunID, string _file);

        //Tharaka 2014-10-15
        [OperationContract]
        DataTable GetChqVoucherAccount(string com, DateTime date);
        //shanuka 16/10/2014
        [OperationContract]
        DataTable ProcessSUNUpload_Fund(DateTime _from, DateTime _to, string _com, string _user, string accNo, string _acc_period, string _sunID, string _file);
        //shalika 22/10/2014
        [OperationContract]
        DataTable chkIsFinalize(string acc, DateTime _date);
        //kapila
        [OperationContract]
        Boolean IsRevertAccount(string _com, string _pc, string _invno, DateTime _dt);
        //kapila
        [OperationContract]
        DataTable Getrevertregistrationcharge(string _com, string _ptyTp, string _ptyCd, string _salesTp, string _itm, string _cat1, string _cat2, string _cat3, string _brand, string _fromPro, string _toPro, DateTime _date);

        //kapila
        [OperationContract]
        DataTable GetLastRegDetails(string _engine, string _item, string _com, string _inv);

        //shanuka 03/11/2014
        [OperationContract]
        DataTable ProcessSUNUpload_Loyalty(DateTime _from, DateTime _to, string _com, string _user, string pc, string _acc_period, string _sunID, string _file);

        //Tharaka 2014-11-26
        [OperationContract]
        int UPDATE_VOUCHER_STATUS(string _com, string _vou, Int32 act, Int32 print, string mod_by, DateTime mod_dt);

        [OperationContract]
        //Sanjeewa 2015-10-16
        string GetExcessShortID(string _Com, string _pc, DateTime _date);

        [OperationContract]
        //subodana 2016-07-13
        Int32 SaveIMP_CST_ELENew(List<imp_cst_ele_ref> oEleReffItems, int seqno, List<IMP_CST_ELEREF_DET> _cstref, out string _error);

        [OperationContract]
        //subodana 2016-07-14
        DataTable GetOrderPlanRef(string sino);

        //Lakshan 20 Jul 2016
        [OperationContract]
        DataTable LoadEntryPopUpNew(string _initialSearchParams, DateTime dtFrom, DateTime dtTo, string _searchCatergory, string _searchText);

        //Lakshan 20 Jul 2016
        [OperationContract]
        DataTable LoadAssementPopUpNew(string _initialSearchParams, DateTime dtFrom, DateTime dtTo, string _searchCatergory, string _searchText);

        //Lakshan 20 Jul 2016
        [OperationContract]
        DataTable LoadSettlementPopUpNew(string _initialSearchParams, DateTime dtFrom, DateTime dtTo, string _searchCatergory, string _searchText);

        //subodana 2016-07-23
        [OperationContract]
        List<ImportsBLContainer> GetContainers(String DocNum);

        //subodana 2016-07-26
        [OperationContract]
        List<SUN_JURNAL> GetSunJurnalnew(String com);
        //subodana 2016-07-27
        [OperationContract]
        List<SUNINVHDR> GetSunInvdatanew(String Com, string pc, DateTime sdate, DateTime edate);

        //subodana 2016-07-27
        [OperationContract]
        List<SUNRECIEPTHDR> GetSunRecieptdatanew(String Com, string pc, DateTime sdate, DateTime edate, string type);
        //subodana 2016-07-30
        [OperationContract]
        DataTable GetCusdechdrByBL(string com, string blno);

        //subodana 2016-07-30
        [OperationContract]
        int UpdateBLStatus(string com, string blno, string status);

        //subodana 2016-08-08
        [OperationContract]
        DataTable CheckLoctype(string com, string loccd, string loctype);

        //subodana 2016-09-03
        [OperationContract]
        int UpdateOtherdocline(string entryno, Int32 lineno, string itemcode, Int32 upline);

        //subodana 2016-09-03
        [OperationContract]
        DataTable SP_GetGRNDOC(DateTime fdate, DateTime tdate, string com);

        //subodana 2016-09-05
        [OperationContract]
        List<LocPurSun> SP_GetLocPerSUN(string com, string grnno);

        //subodana 2016-09-15
        [OperationContract]
        List<Suncreditnote> SP_SUN_CREDNOTE(string com, DateTime fromdate, DateTime todate, string pc);

        //Rukshan 2016-09-15
        [OperationContract]
        Int32 UpdateBillOfLadingWebWharf(ImportsBLHeader oHeader, List<ImportsBLContainer> oImportsBLContainers, out String Error);

        //subodana 2016-10-10
        [OperationContract]
        DataTable GetMIDNO(string com, string pclist);

        //subodana 2016-10-10
        [OperationContract]
        DataTable GetMIDRECIEPT(string com, string mid, DateTime fdate, DateTime tdate);
        //subodana 2016-10-11
        [OperationContract]
        DataTable GetMIDDETAILS(string com, string pclist, string midno);

        //subodana 2016-10-11
        [OperationContract]
        int SaveCredCardRec(SAT_ADJ_CRCD Crecdlist, MasterAutoNumber _masterAutoNumber, List<RecieptHeader> reciept, out String Error);

        //subodana 2016-10-17
        [OperationContract]
        DataTable GenAssExel(DateTime fdate, DateTime tdate);

        //subodana 2016-11-22
        [OperationContract]
        int SAVE_BL_CONTAINERS(ImportsBLContainer cont);

        //subodana 2016-11-22
        [OperationContract]
        int SAVE_BL_CONTAINERSLog(ImportsBLContainer cont);

        //subodana 2016-12-16
        [OperationContract]
        string DGCDAtaExcel(DateTime fdate, DateTime tdate, string com, string user);

        #region Imports

        [OperationContract]
        List<MasterBusinessEntity> GetCustomerDetailList(string company, string customerCode, string nic, string mobile, string customerType);

        //Sahan
        [OperationContract]
        DataTable GetSupplierPorts(string p_mspr_com, string p_mspr_cd);


        [OperationContract]
        DataTable GetSupplierETA(string p_mspr_com, string p_mspr_cd, string p_mspr_frm_port);
        /*Rukshan 2015-06-27*/
        [OperationContract]
        ImportPIHeader GetPIByPIID(string PIID);
        /*Rukshan 2015-06-27*/
        [OperationContract]
        DataTable GetDPIByPIID(string PIID);
        /*Rukshan */
        [OperationContract]
        MasterAutoNumber GetAutoNumber(string _module, Int16? _direction, string _startChar, string _catType, string _catCode, DateTime? _modifyDate, Int32? _year);

        /*Rukshan*/
        [OperationContract]
        Tuple<int, string> SaveNewOrderFinancing(Order_Financing _OrderFinancing, MasterAutoNumber _masterAutoNumber, PIOrderFinancing _PIData, DataTable _PINO, OrderFinancingAmd _Amendment, OrderFinancingcost _Cost, DataTable _CostTbl, ImportFINPay _ImportFINPay, DataTable _Pay);
        /*Rukshan*/

        [OperationContract]
        DataTable GetExchangeRate(string p_com, string p_scur, string p_ccur);
        /*Rukshn*/
        [OperationContract]
        int UpdateNewOrderFinancing(Order_Financing _OrderFinancing, DataTable _DelPI, PIOrderFinancing _PIData, DataTable _PINO, DataTable _newAPI, OrderFinancingAmd _Amendment, OrderFinancingcost _Cost, DataTable _CostTbl, DataTable _DelCost, ImportFINPay _ImportFINPay, DataTable _Pay, bool _ISAMD);


        [OperationContract]
        DataTable GetOrderPeriod(string opNo);
        /*PEMIL 24-Sep-2015*/
        [OperationContract]
        DataTable GetOrderPeriodALL(string opNo);

        [OperationContract]
        DataTable GetOrderItem(string opNo, int year, int month);

        /*Rukshan*/
        [OperationContract]
        Int32 DeleteFinPay(ImportFINPay _ImportFINPay);

        /*Rukshan*/
        [OperationContract]
        Tuple<int, string> PlaceOrder(OrderPlanHeader orderplan, MasterAutoNumber _masterAutoNumber, List<ImportsBLContainer> orderplancontanier);

        [OperationContract]
        Int32 SaveOPItems(OrderPlanItem orderplanItem);

        //Tharaka 2015-07-04
        [OperationContract]
        Order_Financing GET_IMP_FIN_HDR_BY_DOC(String Com, String DocNum);

        [OperationContract]
        Int32 UpdateOrderHeader(OrderPlanHeader orderplan);

        [OperationContract]
        Int32 UpdateOPItems(OrderPlanItem orderplanItem);

        [OperationContract]
        Int32 UpdateOPStatus(OrderPlanHeader OpHeader);

        [OperationContract]
        Int32 SavePI(ImportPIHeader pih, DataTable dtItem, DataTable dtPIKit, DataTable pic, MasterAutoNumber mastAutoNo, out string msg);

        [OperationContract]
        Int32 SaveKitItems(OrderPlanKitItem orderplanKitItem);

        [OperationContract]
        DataTable GetItemLines(string p_ioi_op_no, Int32 p_ioi_seq_no, Int32 p_ioi_line);

        [OperationContract]
        DataTable GetItemRowCount(string p_ioi_op_no, Int32 p_ioi_seq_no);

        [OperationContract]
        ImportPIHeader GET_IMP_PI(String pi);

        [OperationContract]
        List<ImportPIDetails> GET_IMP_PIITEM(String pi);

        //PEMIL 24-Sep-2015
        [OperationContract]
        DataTable GET_IMP_PI_COST(String pi);

        [OperationContract]
        Int32 UpdatePI(ImportPIHeader pih, DataTable dtItem, DataTable pic, out string msg);

        [OperationContract]
        DataTable ChkItem(string cd);

        [OperationContract]
        DataTable GetKitItem(string cd);

        [OperationContract]
        DataTable GetItemDetails(string cd);

        [OperationContract]
        List<ImportPIKit> GET_IMP_PIKITITEM(String pi);

        [OperationContract]
        Int32 Update_Approval_Cancel(Order_Financing _OrderFinancing, out string err);

        [OperationContract]
        Int32 Update_Cost_Inactive(OrderFinancingcost _Cost);

        //Tharaka 2015-07-07
        [OperationContract]
        List<PIOrderFinancing> GET_IMP_FIN_PI_BY_DOC(String DocNum);

        //Tharaka 2015-07-07
        [OperationContract]
        List<ImportPIDetails> GET_IMP_PI_ITM_BY_PINO(String DocNum, Int32 seq);

        [OperationContract]
        Int32 Update_PI_ACtive(PIOrderFinancing _Cost);

        [OperationContract]
        DataTable GetCost(string com, string cat, string tp);

        [OperationContract]
        Int32 UpdateKitItems(OrderPlanKitItem orderplanKitItem);


        //Master Data Rukshan
        [OperationContract]
        Int32 SaveCostCatergoryMaster(ImportCostCatergoryMaster _ImportCostCatergoryMaster);

        [OperationContract]
        DataTable GetCostCatergoryMaster(string _searchCatergory, string _searchText);

        [OperationContract]
        Int32 SaveCostSegmentMaster(ImportCostSegmentMaster _ImportCostSegmentMaster);

        [OperationContract]
        DataTable GetCostSegementMaster(string _searchCatergory, string _searchText);

        [OperationContract]
        DataTable GetCostSegmentMaster_CODE(string _searchText);

        [OperationContract]
        Int32 SaveCostTypeMaster(ImportCostType _ImportCostType, DataTable _CostTypeTbl, ImpoertCostElement _ImpoertCostElement, DataTable _CostElementTbl);

        [OperationContract]
        DataTable GetCostType_CODE(string _CATCD, string _CD);

        [OperationContract]
        DataTable GetCostELEMaster_CODE(string _initialSearchParams);

        [OperationContract]
        DataTable GetCostCMasterBYID(string _CODE);

        //Tharaka 2015-07-10
        [OperationContract]
        List<MST_CONTAINER_TP> GET_CONTAINER_TYPES();

        //Tharaka 2015-07-10
        [OperationContract]
        List<MST_COST_ELE> GET_COST_ELE(String Com, String Type);

        //Tharaka 2015-07-13
        [OperationContract]
        Int32 SaveBillOfLading(ImportsBLHeader oHeader, List<ImportsBLItems> oImportsBLItems, List<ImportsBLSInvoice> OImportsBLSInvoice, List<ImportsBLContainer> oImportsBLContainers, List<ImportsBLCost> oImportsBLCosts, MasterAutoNumber _masterAuto, bool isNewRecord, out String Error, out String BLNumber);

        //Tharaka 2015-07-13
        [OperationContract]
        ImportsBLHeader GET_BL_HEADER_BY_DOC(String Com, String doc, String stats);

        //Tharaka 2015-07-13
        [OperationContract]
        List<ImportsBLItems> GET_BL_ITMS_BY_SEQ(Int32 Seq);

        //Tharaka 2015-07-14
        [OperationContract]
        Int32 UpdateBillOfLading(ImportsBLHeader oHeader, List<ImportsBLItems> oImportsBLItems, List<ImportsBLSInvoice> OImportsBLSInvoice, List<ImportsBLContainer> oImportsBLContainers, List<ImportsBLCost> oImportsBLCosts, MasterAutoNumber _masterAuto, bool isNewRecord, out String Error, out String BLNumber);

        //Tharaka 2015-07-14
        [OperationContract]
        List<ImportsBLContainer> GET_IMP_BL_CONTNR_BY_SEQ(Int32 Seq);

        //Tharaka 2015-07-14
        [OperationContract]
        List<ImportsBLSInvoice> GET_IMP_BL_SI_BY_SEQ(Int32 Seq);

        //Tharaka 2015-07-15
        [OperationContract]
        List<ImportsBLCost> GET_IMP_BL_COST_BY_SEQ(Int32 Seq);

        //Tharaka 2015-07-15
        [OperationContract]
        Int32 UPDATE_IMP_BL_STUS(String User, String Status, Int32 Seq, string sessionID, out String err);

        [OperationContract]
        Int32 UpdatePRNStatus(IntReq PRNHeader);

        [OperationContract]
        Int32 UpdateStatus_QUO_HDR(String no, String stus, String mod_by, DateTime mod_when, out String err);

        [OperationContract]
        Int32 Update_QUO(QuotationHeader qh, List<QoutationDetails> det_line_list, out String err);

        //Tharaka 2015-07-24
        [OperationContract]
        ImportsBLHeader GET_IMP_BL_BY_BL(String com, String BL);

        //Tharaka 2015-07-27
        [OperationContract]
        List<ImportsCostHeader> GET_IMP_CST_HDR_FOR_CS(String com, String BL, String Bond, String Status, DateTime From, DateTime To, string bypass);

        //Tharaka 2015-07-27
        [OperationContract]
        List<ImportsCostElement> GET_IMP_CST_ELE_BY_DOC(String BL);

        //Tharaka 2015-07-29
        [OperationContract]
        ImportsCostHeader GET_IMP_CST_HDR_BY_DOC(String Doc, String Type);

        //Tharaka 2015-07-29
        [OperationContract]
        Int32 CostSheetApply(ImportsCostHeader oCostHeader, List<ImportsCostItem> oCostItems, List<ImportsCostElement> oImportsCostElements, List<ImportsCostElementItem> oImportsCostElementItems2, String User, String Com, String PC, String Loc, String Session, List<imp_cst_ele_ref> oEleReffItems, out string err);

        //Tharaka 2015-08-03
        [OperationContract]
        List<ImportsCostItem> GET_IMP_CST_ITM_BY_SEQ(Int32 Seq, Int32 Status);

        //Tharaka 2015-08-03
        [OperationContract]
        List<ImportsCostElementItem> GET_IMP_CST_ELE_ITM_BY_ITM(Int32 Seq, String doc, String itemCode);

        //Tharaka 2015-08-03
        [OperationContract]
        Int32 UPDATE_IMP_CST_HDR_STAGE(Int32 Stage, Int32 value, String User, Int32 Seq, String Com, String Doc, DateTime Date, out String err);

        //Tharaka 2015-08-04
        [OperationContract]
        int UPDATE_PUR_HDR_BY_COSTSHEET(Int32 Seq, String Status, String User, out String err);

        [OperationContract]
        DataTable CheckBusentity(string com, string tp, string sub_tp, string cd);

        //Tharaka 2015-08-24
        [OperationContract]
        DataTable GET_TRADTERMDESC_BY_DOC(String Doc, String ElectCAte);

        //DArshana 20-10-2015
        [OperationContract]
        int UPDATE_PI_STATUS(string _com, string _PINo, string _status, string mod_by);

        //Sahan 01/Dec/2105
        [OperationContract]
        DataTable LoadBondNumbers(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 01/Dec/2105
        [OperationContract]
        DataTable LoadCusDecDutySum(string p_cuds_doc_no);

        //Sahan 02/Dec/2105
        [OperationContract]
        Tuple<int, string> SaveSettleHeader(ImpAstHeader Header, MasterAutoNumber _masterAutoNumber, ImpCusdecHdr CusDecHeader, DataTable dtCusDec, string company, ImpAstDet Details, DataTable dtSettlements, string user, string session, ImpCusdecDutySum DutySum);

        //Lakshan 20 Apr 2016
        [OperationContract]
        Tuple<int, string> SaveSettleHeaderNew(ImpAstHeader Header, MasterAutoNumber _masterAutoNumber, ImpCusdecHdr CusDecHeader, DataTable dtCusDec, string company,
            ImpAstDet Details, DataTable dtSettlements, string user, string session, ImpCusdecDutySum DutySum, List<ImpAstDet> _remAstDet);

        //Sahan 03/Dec/2015
        [OperationContract]
        DataTable LoadSettleDocs(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime? _fromDate, DateTime? _toDate);

        //Sahan 03/Dec/2015
        [OperationContract]
        DataTable LoadSettleDocSummary(string company, string docno);

        //Sahan 03/Dec/2015
        [OperationContract]
        DataTable LoadSavedDutyDetails(string entryno, Int32 Seqno);

        //Sahan 03/Dec/2015
        [OperationContract]
        DataTable LoadSavedEntryPopUp(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 03/Dec/2015
        [OperationContract]
        DataTable LoadAllEntryDetails(string doc, string company);

        //Sahan 04/Dec/2015
        [OperationContract]
        DataTable CalculateAmendSum(string p_istd_entry_no, Int32 p_istd_seq_no);

        //Sahan 04/Dec/2015
        [OperationContract]
        DataTable CalculateTotEntrySum(Int32 p_istd_seq_no);

        //Sahan 05/Dec/2015
        [OperationContract]
        Int32 UpdateAssessmentHeaderStatus(string stus, string modifiedby, DateTime modified_date, string modifysession, string docno, string company, Int32 seqno, string cancelby, DateTime canceldate, string cancelsession);

        //Sahan 07/Oct/2015
        [OperationContract]
        Int32 ResetCusDecHeaderAndDutySum(string company, string doc, string modifiedby, DateTime modified_date, string modifysesssion);


        //Wimal 30/11/2015
        [OperationContract]
        DataTable GetSipmentDetails(string _comCode, DateTime _fromDate, DateTime _toDate, string _blNo, string _manualNo, string _tobondNo);

        //Wimal 05/12/2015
        [OperationContract]
        DataTable getImpCstAnalDetails(string _comCode, DateTime _fromDate, DateTime _toDate, string _blNo, string _tobondNo);

        //Wimal 05/12/2015
        [OperationContract]
        DataTable getImportRegister(string _comCode, DateTime _fromDate, DateTime _toDate, string _blNo, string _tobondNo, string _fromfile, string _tofile);

        //Wimal 05/12/2015
        [OperationContract]
        DataTable getSLPARegister(string _filefrom, string _fileto, string _com, string _user);

        //Sahan 09/Dec/2015
        [OperationContract]
        DataTable LoadPendingAssesmentHeaders(ImpAstHeader header);

        //Sahan 09/Dec/2015
        [OperationContract]
        DataTable LoadDGCAccounts();

        //Sahan 09/Dec/2015
        [OperationContract]
        Tuple<int, string> SaveSettleMentsHeader(ImportsSettleHeader SettleHeader, MasterAutoNumber _masterAutoNumber, ImportsSettleDetails SettleDetails, ImpAstHeader AssHeader, DGCAccounts Accounts, DataTable dtSettleDetails, DataTable dtAssHeader, string company, string accountno, Decimal usedamt, ImpCusdecHdr CusDecHeader, DataTable dtCusDec, string user, string session);

        //Wimal 09/12/2015
        [OperationContract]
        DataTable getImpCstinfomation(string _com, DateTime _fromDate, DateTime _toDate);

        //Sahan 10/Dec/2015
        [OperationContract]
        DataTable LoadSavedSettlemets(string doc, string company);

        //Sahan 10/Dec/2015
        [OperationContract]
        DataTable LoadSavedSettlementsPopUp(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 10/Dec/2015
        [OperationContract]
        Int32 CancelSettlement(ImportsSettleHeader SettleHeader, ImpAstHeader AssHeader, DataTable dtSettleDetails, string company);

        //Sahan 11 Dec 2015
        [OperationContract]
        DataTable LoadAODDocs(string doc, string company, string type);

        //Chamal 16 Dec 2015
        [OperationContract]
        List<CustomsProcedureCodes> GetCustomsProcedureCodes(string country, string com, string consigType, string consigCode, string docType, string procCode);

        //Chamal 27-Dec-2015
        [OperationContract]
        int GetBLData(string _company, string _blNo, out ImpCusdecHdr _custHdr, out List<ImpCusdecItm> _custItems, out List<ImpCusdecCost> _custCost, out List<ImportsBLContainer> _custContainer, out string _msg);

        //Sahan 28 Dec 2015
        [OperationContract]
        DataTable LoadSelectedEntryNos(string assessment_no);

        //Sahan 28 Dec 2015
        [OperationContract]
        DataTable LoadEntriesByDate(DateTime from, DateTime to, string status);

        //Sahan 28 Dec 2015
        [OperationContract]
        DataTable CountAssessmentTot(string entryno);

        //Sahan 28 Dec 2015
        [OperationContract]
        DataTable CalSettleTotbyEntry(string entryno);

        //Sahan 28 Dec 2015
        [OperationContract]
        Int32 ByPassCusDec(ImpCusdecHdr CusDecHeader);

        //Sahan 28 Dec 2015
        [OperationContract]
        DataTable LoadCustomsReimbursements(ImpCusdecHdr CusDecHeaders);

        //Sahan 29 Dec 2015
        [OperationContract]
        DataTable LoadAODDetailsWithItem(string entry, string com, string doctype, Int32 direction);

        //Sahan 29 Dec 2015
        [OperationContract]
        DataTable LoadEntryPopUp(string _initialSearchParams, string _searchCatergory, string _searchText);


        //Sahan 29 Dec 2015
        [OperationContract]
        DataTable LoadAssementPopUp(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 29 Dec 2015
        [OperationContract]
        DataTable LoadSettlementPopUp(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 29 Dec 2015
        [OperationContract]
        DataTable LoadCusRefPopUp(string _initialSearchParams, string _searchCatergory, string _searchText);

        //Sahan 29 Dec 2015
        [OperationContract]
        DataTable LoadAssNoticePopUp(string _initialSearchParams, string _searchCatergory, string _searchText);
        //Chamal 10 Jan 2015
        [OperationContract]
        List<HsCode> GetHSDutyCalculation(bool _isOpenConnection, string _company, DateTime _docDT, string _cognType, string _cognCode, string _procCode, string _bondType, string _hsEntryType, string _ownCountry, int _allMPVal, decimal _cif_val, string _itemCode, string _hsCode, decimal _qty, decimal _netMass, string _defCountry, out decimal _totActTaxVal, out decimal _totPreTaxVal);

        //Tharaka 2016-01-16
        [OperationContract]
        List<imp_cst_ele_ref> GET_IMP_CST_ELE_REF_BY_SEQ(Int32 SeQ);

        //Chamal 2016-01-16
        [OperationContract]
        Int32 SaveCusdec(ImpCusdecHdr _cusdecHdr, List<ImpCusdecItm> _cusdecItm, List<ImpCusdecCost> _cusdecCost, bool _isUpdate, out string msg, int isres, out string docnum, bool _isconnopen, bool _hsopen);

        //Tharaka 2016-01-16
        [OperationContract]
        List<ComboBoxObject> GET_PKG_UOM();

        //Tharaka 2016-01-16
        [OperationContract]
        List<ImportsCostItem> GET_PRIVIOUS_VALUES(string DocNumber, string Item);

        //Chamal 2016-01-19
        [OperationContract]
        List<CusdecTypes> GetCusdecTypeInfor(string _country);

        //Chamal 2016-01-19
        [OperationContract]
        int GetCusdecData(string _company, string _country, string _docType, string _docNo, out ImpCusdecHdr _custHdr, out List<ImpCusdecItm> _custItems, out List<ImpCusdecItmCost> _custItemsCost, out List<ImpCusdecCost> _custCost, out List<ImportsBLContainer> _custContainer, out string _msg);

        //Chamal 2016-01-27
        [OperationContract]
        int GetCusdecReqData(string _company, string _country, string _docType, string _reqNo, out ImpCusdecHdr _custHdr, out List<ImpCusdecItm> _custItems, out List<ImpCusdecItmCost> _custItemsCost, out List<ImpCusdecCost> _custCost, out List<ImportsBLContainer> _custContainer, out string _msg, bool _isconopen);

        //Chamal 2016-02-02
        [OperationContract]
        DataTable Get_OFFICE_ENTRY(string country, string entryType, string entrySubType);

        //Chamal 2016-02-02
        [OperationContract]
        DataTable Get_LOC_OF_GOODS(string country, string entryType, string entrySubType);

        //Chamal 2016-02-02
        [OperationContract]
        CusdecCommon Get_CusdecCommon(string country);

        //Tharaka 2016-02-08
        [OperationContract]
        ImpCusdecHdr GET_CUSTDEC_HDR_BY_DOC(String docno);

        //Tharaka 2016-02-08
        [OperationContract]
        List<ImportsBLItems> GET_BL_ITM_BY_DOC_ITM(string Doc, String Item, string status);

        //Tharaka 2016-02-10
        [OperationContract]
        DataTable GET_IMP_OTHER_CST(string Type);

        //Tharaka 2016-02-10
        [OperationContract]
        decimal Cal_SLPA(string _blNo, int _demurrageDays, decimal _exRate);

        //Tharaka 2016-02-10
        [OperationContract]
        decimal Cal_SLPA_RENT(string _blNo, int _demurrageDays, decimal _exRate);

        //Tharaka 2016-02-11
        [OperationContract]
        List<ComboBoxObject> GET_REF_CARY_TYPE();

        //Chamal 2016-02-14
        [OperationContract]
        List<ImpCusdecItm> UpdateHSUsingHistory(List<ImpCusdecItm> _cusItems, string com, string country, string doctype, string sysBLNo, decimal totInvoiceValue, decimal totNetMass, decimal totGrossMass, string _currentdoc);

        //Darshana 2016-03-10
        [OperationContract]
        DataTable Get_PerItemCostVal(string p_docNo, string p_itmCd, Int32 p_itmLine);

        //Lakshan 2016-03-11
        [OperationContract]
        Int32 UpdateBillOfLadingWeb(ImportsBLHeader oHeader, List<ImportsBLItems> oImportsBLItems, List<ImportsBLSInvoice> OImportsBLSInvoice, List<ImportsBLContainer> oImportsBLContainers, List<ImportsBLCost> oImportsBLCosts, MasterAutoNumber _masterAuto, bool isNewRecord, out String Error, out String BLNumber);


        //Chamal 11-Mar-2016
        [OperationContract]
        int UpdateBLItemHS(string _docNo, Int32 _lineNo, string _hs, string _gross, string _net, string preff, string entry);

        //Lakshan 17-Mar-2016
        [OperationContract]
        List<MST_COST_ELE> GET_COST_ELE_DATA(MST_COST_ELE _costEle);

        //Chamal 29-Mar-2016
        [OperationContract]
        bool CheckEntryByPass(string _company, string _entryNo, out string _msg);
        //LAkshan 04 Apr 2016
        [OperationContract]
        DataTable LoadBondNumbersNew(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromdate, DateTime _Todate, int _isdate);
        //Chamal 24-Apr-2016
        [OperationContract]
        DataTable LoadBOIRequestByCustomer(string com, string cust, string type);

        //Chamal 24-Apr-2016
        [OperationContract]
        void Update_BL_HSCode(string blDocNo, string itemcode, string hscode);

        //Chamal 28-Apr-2016
        [OperationContract]
        int GetBOICusdecReqData(string _company, string _country, string _docType, string _custCode, List<string> _reqNo, out ImpCusdecHdr _custHdr, out List<ImpCusdecItm> _custItems, out List<ImpCusdecItmCost> _custItemsCost, out List<ImpCusdecCost> _custCost, out List<ImportsBLContainer> _custContainer, out string _msg);

        //Chamal 09-May-2016
        [OperationContract]
        List<ImpCusdecItmCost> GetDutyElementSummary(string docno);

        //Rukshan 21/May/2016
        [OperationContract]
        List<ImpCusdecItm> GET_CUSDEC_ITEM_BY_DOC(String docno);

        //rukshan 2016-jun-17
        [OperationContract]
        int UPDATE_BL(List<ImportsBLHeader> _BL, out string err);


        //Rukshan 2016-06-18
        [OperationContract]
        ImpCusdecHdr GET_CUSTDECHDR_DOC(String docno);

        //Rukshan 2016-06-18
        [OperationContract]
        List<ImpCusdecItm> GET_CUSTDECITM_DOC(String docno);

        //Rukshan 2016-06-18
        [OperationContract]
        List<OrderPlanItem> GET_IMP_OPBY_PI(String DocNum);

        //Lakshan 21 Jul 2016
        [OperationContract]
        DataTable LoadEntriesByDateNew(DateTime from, DateTime to, string status);

        //Lakshan 21 Jul 2016
        [OperationContract]
        List<ImpCusdecItm> GET_CUSDEC_GRNITEM_DOC(String docno, string _type = null);

        //Lakshika 2016/Aug/09
        [OperationContract]
        DataTable ValidateAssesmentNo(string _initialSearchParams, string _searchCatergory, string _searchText);

        //subodana 2016-09-01
        [OperationContract]
        DataTable GetSunPC(string type, string com);

        //subodana 2016-09-16
        [OperationContract]
        Int32 UPDATE_INV_HDRENGLOG(string invNo, Int32 value, string com);

        //subodana 2016-09-16
        [OperationContract]
        Int32 UPDATE_RECIEPT_HDRENGLOG(string recNo, Int32 value, string com);

        //Rukshan 2016-10-29
        [OperationContract]
        List<ImportsBLContainer> GETORDER_CONTENER(string doc);
        #endregion

        //Lakshan 2016 Dec 19
        [OperationContract]
        Int32 UpdateImportsCostHeaderRefSave(ImportsCostHeader _obj, string _saveTP, out string _error);

        //subodana 2016-12-29
        [OperationContract]
        DataTable CusdecCancelIsGRN(string com, string entryno, int option);

        //subodana 2016-12-29
        [OperationContract]
        Int32 CusdecEntryCancelation(string com, string entryno, string sino, string user, out string _error);

        //subodana 2016-12-30
        [OperationContract]
        Int32 CusdecEntryCancelation_exreboi(string com, string entryno, string reqno, string user, List<ImpCusdecItm> cusitm, string sunbondno, string type, out string _error);
        [OperationContract]
        DataTable GetCreditNoteInfo(string _comCode, string _profitCenter, string _creditNote);
        [OperationContract]
        DataTable ActivateCreditNote(string _CreditNoteNo, string _status, string _userId);
        [OperationContract]
        DataTable GetCreditNotesbyInvoice(string _invoiceNo);
        //subodana 2017-02-15
        [OperationContract]
        int SaveCommissionDeffinition(ref_comm_hdr comm_hdr, List<ref_comm_det> _comm_det, List<ref_comm_emp> _comm_emp, List<ref_comm_pc> _comm_pc, List<ref_comm_target> targets, List<ref_comm_target_ovrt> targetovrt, List<ref_comm_collect_ovrt> collecovrt, List<ref_eli_comm_targ> _elite_trg, List<ref_comm_add_trgt> _elite_add, MasterAutoNumber _masterAutoNumber, out string err);

        //subodana 2017-02-17
        [OperationContract]
        List<ref_comm_hdr> GetCommissionHDR(String Com, string doc);
        //subodana 2017-02-17
        [OperationContract]
        List<ref_comm_det> GetCommissionDetails(string doc);
        //subodana 2017-02-17
        [OperationContract]
        List<ref_comm_emp> GetCommissionEmp(string doc);
        //subodana 2017-02-17
        [OperationContract]
        List<ref_comm_pc> GetCommissionPC(string doc);

        //Lakshan 18 Feb 2017
        [OperationContract]
        List<MST_CONTAINER_TP> GET_MST_CONTAINER_TP_DATA(MST_CONTAINER_TP _obj);
        //Lakshan 18 Feb 2017
        [OperationContract]
        decimal OrderPlanItemQuentity(List<MasterItem> _itmList);

        //subodana 2017/02/20
        [OperationContract]
        int CommissionProcess(string com, List<Commission_pc> Commpc, DateTime fdate, DateTime tdate, out string error, out List<Invoice_Commission> summery, out List<DELI_SALE_NEW> all);

        //Lakshan 22 Feb 2017
        [OperationContract]
        ImportsCostHeader GetImpCstHdrForGrn(string _docNo);
        //subodana 2017-02-23
        [OperationContract]
        int SaveCommissionInvoices(List<Invoice_Commission> _inv, List<Invoice_Commission> _emp, DateTime fdate, DateTime tdate, out string err);
        //subodana 2017-02-24
        [OperationContract]
        DataTable GetMIDRECIEPTAll(string com, string mid, DateTime fdate, DateTime tdate);
        //subodana 2017-03-17
        [OperationContract]
        Int32 UPDATE_LOCPCH_HDRENGLOG(string grnno, Int32 value, string com);
        //subodana 2017-03-25
        [OperationContract]
        int UpdateCusdecEntryData(string entry, string cusentry, DateTime cusentrydate);

        //Isuru 2017/03/28
        [OperationContract]
        DataTable GET_Commission_Details(DateTime fdate, DateTime tdate, string commcode, string userId, string pc);
        //Dilshan 2017/10/03
        [OperationContract]
        DataTable GET_Bonus_Details(DateTime fdate, DateTime tdate, string commcode, string userId, string pc);
        //Udaya 05/04/2017
        [OperationContract]
        string GetAndUpdateAutoNo(string _module, Int16? _direction, string _startChar, string _catType, string _catCode, DateTime? _modifyDate, Int32? _year);
        //Udaya 05/03/2017
        [OperationContract]
        DataTable getInvoiceValues(string commcode, string proCenter, string invNo);
        //Udaya 06/04/2017
        [OperationContract]
        Int32 saveCreditNote(List<CreditNoteHdr> _CreditNoteHdr, List<CreditNotes> _CreditNotes, MasterAutoNumber masterAuto, out string doc);
        //Udaya 07/04/2017
        [OperationContract]
        DataTable getComDetails(string commcode);
        //Udaya 07/04/2017
        [OperationContract]
        Int32 CreditNoteCancel(string invoiceNo, decimal amt, string sessiomModBy, DateTime sessiomModDate, string sessionId);
        //subodana 2017-04-05
        [OperationContract]
        List<ref_comm_target> GetCommissionTargetDetails(string doc);

        //subodana 2017-04-05
        [OperationContract]
        List<ref_comm_target_ovrt> GetCommissionTargetOvtDetails(string doc);
        //subodana 2017-04-05
        [OperationContract]
        List<ref_comm_collect_ovrt> GetCommissionCollectionOvtDetails(string doc);

        //subodana 2017-04-08
        [OperationContract]
        List<sat_settl_discount> GetSettleDiscountDetails(string com, string pc, string invtype);
        //Udaya 11/04/2017
        [OperationContract]
        Int32 saveDebitNote(List<DebitNoteHdr> _DebitNoteHdr, List<DebitNotes> _DebitNotes, MasterAutoNumber masterAuto, out string doc);
        //Udaya 11/04/2017
        [OperationContract]
        Int32 DebitNoteCancel(string invoiceNo, decimal amt, string sessiomModBy, DateTime sessiomModDate, string sessionId);

        //2017-04-27 subodana
        [OperationContract]
        int PanaltyChargeProcess(string com, string pc, string invtype, string user, string session, DateTime Date, out string docs);
        //2017-04-27 subodana
        [OperationContract]
        List<HsCode> GetHSCodeList(string hs);
        //Add by lakshan 29 Aug 2017
        [OperationContract]
        Int32 ChequeReturnWeb(RecieptHeader recieptHeadder, RecieptItem recieptItem, ChequeReturn chequeReturn, List<RecieptItem> recieptItemList, out string _erro);
        //Add by lakshan 05 May 2017
        [OperationContract]
        DataTable GetReturnChequesNew(string _loc, string _ref, string _bCode);

        //subodana 2017-05-09
        [OperationContract]
        int SaveProductBonus(REF_BONUS_HDR _hdr, List<ref_bonus_det> _det, List<ref_bonus_loc> _loc, MasterAutoNumber _masterAutoNumber, out string Docno);

        //Udesh 2018-10-11
        [OperationContract]
        string GetProductBonusExcel(string _Com, string _User, string _bonusCode, out string _err);

        //Udaya 15/05/2017
        [OperationContract]
        List<CreditNotes> CreditNoteDetails(string invNo, string comCode, string proCenter);
        //Udaya 15/05/2017
        [OperationContract]
        List<DebitNotes> DebitNoteDetails(string invNo, string comCode, string proCenter);
        //subodana 2017/05/16
        [OperationContract]
        List<ref_bonus_loc> GetBonusLoc(string doc);
        //subodana 2017/05/16
        [OperationContract]
        List<ref_bonus_det> GetBonusDetails(string doc);
        //subodana 2017/05/16
        [OperationContract]
        List<REF_BONUS_HDR> GetBonusHDR(string doc, string com);
        //subodana 2017/05/17
        [OperationContract]
        List<PoductBonusData> BonusProcess(string Code, DateTime fromdate, DateTime todate, string Com, DateTime salesfdate, DateTime salestdate);
        //subodana 2017-05-23
        [OperationContract]
        DataTable GetPenaltyInvoices(DateTime fromdate, DateTime todate);
        //subodana 2017-05-23
        [OperationContract]
        int CancelPenalty(string InvNo);
        //subodana 2017-05-30
        [OperationContract]
        int SaveProductBonusdata(List<PoductBonusData> _bnslist, string com, string user, out string err);
        //Udaya 02/06/2017
        [OperationContract]
        DataTable getInvoiceDetailsValues(string itemCode, string invNo);
        //subodana 2017-06-02
        [OperationContract]
        DataTable Sp_GetbtuItems(string cat1, string type, string btu, string btu2);
        //Udaya 10.07.2017
        [OperationContract]
        DataTable sp_getCommSalesTypes(string cirCode);
        //Udaya 10.07.2017
        [OperationContract]
        List<DELI_SALE_NEW> GetDeliversaleList();
        //Udaya 10.07.2017
        [OperationContract]
        DataTable Get_Gp_Data(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _User, string _RepType, string _StockType, string _InvNo, string _Pc, string _Com, string _Promotor, int _freeissue, string _itemclasif, string _brndmgr, bool withReversal, int _rplitm, DateTime _fromDate2, DateTime _toDate2, Int16 _intercom);
        //Udaya 11.07.2017
        [OperationContract]
        List<SalesTarget> GetTargetSales(string cirCode, string frmDate, string toDate);
        //Udaya 11.07.2017
        [OperationContract]
        DataTable SalesTargetLevel(string cirCode);
        //Udaya 11.07.2017
        [OperationContract]
        DataTable CommisionReport(string cirCode, string pcCode, DateTime frmDate, DateTime toDate, string comCode, string EmpCode, out List<DELI_SALE_NEW> _totSales, out List<SalesTarget> SlsTarList, out DataTable sales_Level);
        //Lakshan 21 Jul 2017
        [OperationContract]
        List<ImpCusdecHdr> GET_CUSTDEC_HDR_BY_CUSTDEC_ENTRY_NO(String _entryNo);
        //subodana
        [OperationContract]
        string GetAccountCodeByTp(string _company, string _type);
        //Lakshan 24Jun 2017
        [OperationContract]
        Int32 UpdateCusdecEntryNoDate(List<ImpCusdecHdr> _obJList, out string err);
        [OperationContract]
        int UpdateTargertMonth(string com, string calcd, string prcd, DateTime fdate, DateTime tdate);
        [OperationContract]
        List<ref_comm_mngr> GetCommissionExcmngrates(String doc, string exec);
        [OperationContract]
        List<mst_proc> GetProcByConsignee(String com, string type, string consign);
        [OperationContract]
        List<BOIProc> GetDutyProc(String com);
        [OperationContract]
        List<mst_proc_ele> GetProcDutyByConsignee(String com, string proc, string consign);
        [OperationContract]
        int SaveProcElements(List<mst_proc_ele> _lst, out string _errer);

        //by Akila 2017/08/17
        [OperationContract]
        DataTable GetInvalidReceiptsForSunUpload(string _company, string _profitCenter, DateTime _fromDate, DateTime _toDate);
        [OperationContract]
        //Added By Udaya 08.09.207 Collect Sales Exc Contribution to WEBMCV
        DataTable SalesEx_Contribution(string cirCode, string pcCode, DateTime frmDate, DateTime toDate, string comCode);

        //subodana 2017-09-17
        [OperationContract]
        int SaveHandOverAccounts(List<hpr_hand_over_ac> _lst, out string _errer);

        //subodana 2017-09-17
        [OperationContract]
        List<hpr_hand_over_ac> GetHandOverData(String com, string proc, bool avail);
        //by Tharanga 2017/09/16
        [OperationContract]
        DataTable ProcessSUNUpload_Cedit_note(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _acc_period, string _sunID, string _file);
        [OperationContract]
        //subodana 2017-09-19
        int AccountsArrearsProcess(DateTime bonusmonth, string user, string com, out string _errer);
        [OperationContract]
        //subodana 2017-09-20
        List<hpt_arr_acc_det> GetArrBalAccDetails(string procen, DateTime date);
        //subodana 2017-09-21
        [OperationContract]
        List<hpr_hand_over_ac> GetHandOverDataAccAll(string com, string pc, DateTime month, string acc);
        //subodana 2017-09-25
        [OperationContract]
        int SaveAccountAdjDetails(hpt_arr_acc_det _old, hpt_arr_acc_det _new, out string _errer);
        //Dilshan 2017-09-30
        [OperationContract]
        int SaveBonusVouDetails(List<BonusVoucher> _lst, out string _errer);
        //Dilshan 2017-09-30
        [OperationContract]
        int UpdateARR_ACCNew(List<hpt_arr_acc> _lst, out string _errer);
        //subodana
        [OperationContract]
        List<MgrCreation> GetManagerDetails(string com, string pc, string mngr);
        //subodana
        [OperationContract]
        List<hpt_arr_acc> GetArrBalAccHdrByCode(string com, string manager, DateTime date);
        //subodana
        [OperationContract]
        List<hpr_disr_val_ref> AmountDisregard(decimal clsbal, Int32 tp);
        //subodana
        [OperationContract]
        List<hpr_disr_val_ref> GraceperiodnotQulified(DateTime days, Int32 tp, string com, string pc, decimal val);
        //subodana
        [OperationContract]
        List<BonusDefinition> GetCollecBonusDet(string com, decimal rates, decimal yrs, string pccat, DateTime mngcrdt, Int64 Accounts, decimal Avgacc, DateTime Commdate, String PC);
        //subodana
        [OperationContract]
        DataTable GetPCType(string cat);
        //subodana
        [OperationContract]
        DataTable GetEPF_ESP(string com, string pc, DateTime date);
        //subodana
        [OperationContract]
        int FinalizCollecBonus(hpt_col_bonus_vou _lst, MasterAutoNumber _masterAutoNumber, out string _errer);
        //subodana
        [OperationContract]
        bool CheckClosBal(string accno, DateTime date, decimal sysbal, out decimal _bal);
        //dilshan 04/10/2017
        [OperationContract]
        DataTable CollBonusVoucher(string pcCode, DateTime frmDate, string comCode);
        //subodana
        [OperationContract]
        List<hpt_arr_acc> GetArrBalAccHdr(string com, string pc, DateTime date);
        //subodana
        [OperationContract]
        bool CheckSunRef(string refno, string _trdt);
        //subodana
        [OperationContract]
        List<ComboBoxObject> GET_AGGR_REF();
        //subodana
        [OperationContract]
        List<ImpCusdecItm> UpdateHSHistoryOnly(List<ImpCusdecItm> _cusItems, string com, string country, string doctype, string sysBLNo, decimal totInvoiceValue, decimal totNetMass, decimal totGrossMass, string _currentdoc);
        //subodana
        [OperationContract]
        List<ImpCusdecItm> UpdateMassOnly(List<ImpCusdecItm> _cusItems, string com, string country, string doctype, string sysBLNo, decimal totInvoiceValue, decimal totNetMass, decimal totGrossMass, string _currentdoc);
        //subodana
        [OperationContract]
        List<ImpCusdecItmCost> GetDutyElementSummaryForHS(string docno, string hs);
        //subodana
        [OperationContract]
        int SaveCusdecEntry(InventoryRequest _inventoryRequest, MasterAutoNumber _mastAutoNo, List<ImportsBLItems> _ImportsBLItems, out string _docNo);
        //subodana
        [OperationContract]
        DateTime GetEntryDatetime(string Com, string Entry);
        //subodana
        [OperationContract]
        DataTable GetEntryNumbers(string com, string Type, DateTime frmDate, DateTime todate);
        //subodana
        [OperationContract]
        List<ref_comm_trgt_comm> GetTargetCommRates(string docno, string exec, Int32 _month, Int32 year);
        //subodana
        [OperationContract]
        int SaveCommTargetCommission(List<ref_comm_trgt_comm> list, out string _errer);
        //add by tharanga 2017/10/25
        [OperationContract]
        DataTable GET_TAX_BY_INV_TYPE(string p_pc, DateTime p_from, DateTime p_to, string p_inv_type);
        //add by tharanga 2017/10/25
        [OperationContract]
        DataTable GNT_REM_SUM(string p_pc, DateTime p_from, DateTime p_to, Int32 p_rem_dayend);
        //add by tharanga 2017/10/26
        [OperationContract]
        DataTable SAT_RECEIPT_ALL_DET(string p_com, string p_pc, DateTime p_from, DateTime p_to);
        //add by tharanga 2017/10/26
        [OperationContract]
        DataTable SAT_Collec_summery(string p_com, string p_pc, DateTime p_from, DateTime p_to, Int32 p_oth_shp);
        //subodana
        [OperationContract]
        string GetRecAccount(string InvNo);
        //subodana
        [OperationContract]
        string GetRecChassisNo(string InvNo);
        //subodana
        [OperationContract]
        List<MST_COST_ELE> GetCostEleSubType(string com, string code);
        //subodana
        [OperationContract]
        List<ImportsBLContainer> GetCostContainers(string sino);
        //subodana
        [OperationContract]
        List<IMP_CST_ELEREF_DET> GetCostEleSubDetails(Int64 seq, Int32 refline);
        //subodana
        [OperationContract]
        List<SunAALRec> GET_SUNRECREF(String InvNo);
        //Add lakshan 13Nov2017
        [OperationContract]
        List<ImpCusdecItm> GET_CUSDEC_GRNITEM_DOC_WEB(String docno, string _type = null);
        //subodana
        [OperationContract]
        List<ImportsBLContainer> GetSIByContainer(string contai);
        //subodana
        [OperationContract]
        decimal CostTransCost(string com, string type, string costtype, string loc);
        //add by tharanga 2017/11/10
        [OperationContract]
        DataTable Process_Age_Anal_Debt_Outstand_veh_re(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _cust, Int32 _tp);
        //add by tharanga 2017/11/23
        [OperationContract]
        DataTable GET_HP_Acc_Resc_His(string p_pc, DateTime _fromDate, DateTime _toDate, Int32 as_at_date);
        //add by Dilshan 2018/11/02
        [OperationContract]
        DataTable Get_rejectAccBalance_New(string _com, string _loc, DateTime _asatDate, string _userID);
        //add by tharanga 2017/11/24
        [OperationContract]
        decimal Isurance_balance(string p_acc);
        //add by tharanga 2017/11/24
        [OperationContract]
        DataTable Collection_det(string p_pc, string accno, DateTime _Date, decimal _amount, out decimal _ins, out decimal _diriya, out decimal _collection, out decimal _servicecharge, Int32 Collection_det);
        //SUBODANA
        [OperationContract]
        decimal Ship_BankCost(string _com, string _bank, Int32 _foc, Int32 _garrent, string _type);
        //SUBODANA
        [OperationContract]
        List<IMP_CST_ELEREF_DET> GetCostEleSubDetailsAll(Int64 seq);

        //add by tharanga 2017/11/29
        [OperationContract]
        List<InventoryRequestItem> GET_INT_REQ_ITM_BY_SEQ(Int32 seq);
        // add by tharanga 2017/11/29
        [OperationContract]
        List<INT_REQ_SER> _INT_REQ_SER(Int32 _seqNo);
        //subodana
        [OperationContract]
        string GetFinBankCode(string sino);
        //subodana
        [OperationContract]
        bool isstatusvalidation(string com, string item, string status, string tobond, decimal currqty);
        //Add by Lakshan 30Nov2017
        [OperationContract]
        Int32 CreateOrderPlansusingExcel(List<OrderPlanExcelUploader> _opExcelData, MasterAutoNumber _masterAutoNumber, out List<OrderPlanHeader> _opHdrdocs, out string _docNo, out string _err);
        //subodana
        [OperationContract]
        bool IsfocSI(string sino);
        //subodana
        [OperationContract]
        bool IsShipGrnty(string sino);
        //subodana
        [OperationContract]
        string GetFinPaytype(string sino);
        //subodana
        [OperationContract]
        string GetFinPaySubtype(string sino);
        //subodana
        [OperationContract]
        Int32 GetExpiryMonths(string sino);
        //subodana
        [OperationContract]
        List<imp_cst_shp_bnk> GetAllBankData(string _com, string _bank, Int32 _foc, Int32 _garrent, string _type, string term, string subterm);
        //subodana
        [OperationContract]
        decimal GetLCVal(string sino);
        //subodana
        [OperationContract]
        string GetShipSeqNo(string sino);
        //subodana
        [OperationContract]
        bool IsTOTFOC(string sino);
        //subodana
        [OperationContract]
        string GetGRNno(string sino);
        //subodana
        [OperationContract]
        DateTime GetGRNDate(string sino);
        //subodana
        [OperationContract]
        int SaveSunEntries(List<SunAccountall> list, string com, DateTime todate, MasterAutoNumber _masterAutoNumber, MasterAutoNumber _masterAutoNumber2, out string _errer, out long _journo);
        //subodana
        [OperationContract]
        DataTable GetCanibalizeHdrData(string docno);
        //subodana
        [OperationContract]
        string GetGRNnoSBU(string sino);
        //subodana
        [OperationContract]
        int SaveCredCardAchknow(SAT_ADJ_CRCD Crecdlist, MasterAutoNumber _masterAutoNumber, out String Error);
        //Dilshan
        [OperationContract]
        int SaveCredCardAchknow_new(List<SAT_ADJ_CRCD> Crecdlist, MasterAutoNumber _masterAutoNumber, out String Error);
        //subodana
        [OperationContract]
        List<SAT_ADJ_CRCD> GetAdjDetByCirc(string _com, string _ref, string _type);
        //subodana
        [OperationContract]
        bool IsSunAcc(string acc, string com);
        //subodana
        [OperationContract]
        bool IsTodayStatemant(string com, string pc, DateTime date, string bank);
        //subodana
        [OperationContract]
        List<SAT_ADJ_CRCD> GetCreditCardSunData(string _com, string _pc, string _type, DateTime _fdate, DateTime _tdate);
        //subodana
        [OperationContract]
        decimal WalfExsCalc(string _blNo, int _demurrageDays, decimal _exRate);
        //subodana
        [OperationContract]
        decimal GetFinINSVal(string sino);
        //subodana
        [OperationContract]
        decimal GetFinWOFOCVal(string sino);
        //subodana
        [OperationContract]
        decimal GetBLOTHAmtVal(string sino);
        //subodana
        [OperationContract]
        decimal GetTaxValForCode(string invno, string taxcode);
        //subodana
        [OperationContract]
        DateTime GetPCComGraceDate(string comcode, string pccode, DateTime month);
        //subodana
        [OperationContract]
        string GetSchemePeriod(string acc);
        //subodana
        [OperationContract]
        string GetFinRefNo(string sino);
        //subodana
        [OperationContract]
        DataTable LoadBonusAdjResons();
        //subodana
        [OperationContract]
        DataTable CheckSUNLC2(string COM, string CAT, string CODE);
        //subodana
        [OperationContract]
        decimal GetLCValFin(string sino);
        //Sanjeewa
        [OperationContract]
        DataTable GetMigrateSerialDetails(string _com, string _loc, DateTime _date);


        //Tharindu 07/02/2018
        [OperationContract]
        Int32 CheckReturnCheque(string srcq_chq, string chq_rtn_bank);
        [OperationContract]
        string GetRefNumber(string srcq_chq, string chq_rtn_bank);
        [OperationContract]
        DataTable GetReturnChequeCountWithoutPc(string _ref, string _bCode);

        //Tharindu 07/02/2018
        [OperationContract]
        DataTable Age_Anal_Debt_Outstand_Adv(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _cust, Int32 _tp);

        //Tharindu 07/02/2018
        [OperationContract]
        DataTable Age_Anal_Debt_Outstand_Credit(DateTime _from, DateTime _to, string _com, string _pc, string _user, string _cust, Int32 _tp);
        //subodana 
        [OperationContract]
        Int64 GetEffectiveCollSeq(string com, string pc, string account, DateTime bonusdate, DateTime effectivedate);
        //subodana 
        [OperationContract]
        int SaveAccountAdjEffectDetails(hpt_arr_acc_det _old, hpt_arr_acc_det _new, out string _errer);
        //subodana 
        [OperationContract]
        Int64 GetEffectiveCollHDRSeq(string com, string pc, DateTime bonusdate, DateTime effectivedate);
        //subodana 
        [OperationContract]
        int SaveARR_ACCNew(List<hpt_arr_acc> _lst, out string _errer);
        //subodana 
        [OperationContract]
        decimal GetCostExchangeRAte(string SINO);
        //tharanga 2018/03/15
        [OperationContract]
        Decimal get_advance_count_itm_wise(string _itm, string _com, string _pc);
        //subodana
        [OperationContract]
        Int64 GetAllCollSeq(string com, string pc, string account, DateTime bonusdate, DateTime effectivedate);


        //Akila 2018/02/26
        [OperationContract]
        DataTable ProcessSunUploadElite_New(string _user_id, DateTime _fromdate, DateTime _toDate, string _Company, string _Profit, out string _errorMsg);
        //subodana
        [OperationContract]
        string GetSICurrency(string SINO);
        //subodana
        [OperationContract]
        decimal GetPrevMonthAdj(string pc, DateTime bonusdate, DateTime effectivedate);
        //subodana
        [OperationContract]
        DataTable GetPrevMonthActArrears(string pc, DateTime bonusdate, DateTime effectivedate);
        //subodana
        [OperationContract]
        int UpdateBuyingRates(string sino, decimal buying, decimal costing, decimal frieghtRate, string remarks, decimal exRate);
        //subodana
        [OperationContract]
        DataTable GetBuyingRates(string sinum);
        //subodana
        [OperationContract]
        DataTable GetForcastRate(string sinum, string type);
        //subodana
        [OperationContract]
        bool IsTOTOItem(string si);
        //subodana
        [OperationContract]
        bool IsMotoBike(string si);
        //tharanga 2018/04/05
        [OperationContract]
        List<Deposit_Bank_Pc_wise> get_Deposit_Bank_Pc_wise_det(string p_com, string p_prof_cen, string p_mid_no, DateTime p_frm_date, DateTime p_to_date);
        //tharanga 2018/04/06
        [OperationContract]
        List<Deposit_Bank_Pc_wise> get_mid_details(string p_com, string p_prof_cen, string p_mid_no, string p_bandk, int p_period, string p_price_book, string p_price_level, string p_item, string p_promotion, string p_cat, string p_brand, DateTime p_to_date);
        //subodana 2018-04-17
        [OperationContract]
        string GetEXPBOISunReqNo(string BondNo, string Loc);
        //subodana 2018-04-17
        [OperationContract]
        int UpdateBondLoc(string sunreq, string bondno, string loc);
        //tharanga 2018/04/11
        [OperationContract]
        DataTable get_cred_realization_Hdr(string _com, string _mid, string p_bank, DateTime _date, string p_user, string p_session, string p_account);
        //subodana 2018-04-20
        [OperationContract]
        decimal GetBLCostVal(string sino);
        //tharanga 2018/04/18
        [OperationContract]
        DataTable get_advance_dete(string _itm, string _loc);
        //subodana 2018-05-03
        [OperationContract]
        List<hpt_arr_acc> GetAllBonusPCData(string pc, DateTime date, string com, string mnger);
        //subodana 2018-05-03
        [OperationContract]
        List<hpt_arr_acc> GetMonthlyBonusPCData(string pc, DateTime date, string com, string mnger, DateTime effectdt);
        //tharanga 2018/04/24
        [OperationContract]
        DataTable Process_cred_realization(string in_com, string in_pc, DateTime in_recept_date, DateTime in_date, string in_user, string in_session, Int32 in_seq, string in_bank_cd, string in_mid_cd, string in_acc_no);
        //tharanga 2018/04/04/27
        [OperationContract]
        int GetCCRecTot_cred(string _com, DateTime _date, string _accno, out decimal _val);
        //tharanga 2018/05/01
        [OperationContract]
        Int32 UpdatecrdRealizationDet(List<gnt_cred_stmnt_det> _credRlsList, string _Bsth_com, DateTime _Bsth_dt, string _Bsth_accno, Decimal _Bsth_opbal, Decimal _Bsth_realizes, Decimal _Bsth_prv_realize, Decimal _Bsth_cc, Decimal _Bsth_adj, Decimal _Bsth_clbal, Decimal _Bsth_state_bal, string _Bsth_stus, string _Bsth_cre_by, string _STH_MID, out string _msg, string _sessionid);
        //tharanga 2018/05/02
        [OperationContract]
        DataTable get_cred_serch_det(string _com, string _pc, DateTime _date, string _accno, string _doctp, Decimal _amtfrom, Decimal _amtto, Int32 _is_real_stus, Int32 _nt_in_state, Int32 _oth_bank, Int32 _withNIS, string _ref, string _bank, string _mid);

        //tharanga 2018/05/02
        [OperationContract]
        Boolean checkDoc_credk_State(string _pc, DateTime _dt, string _doctp, string _ref);

        //tharanga 2018/05/02
        [OperationContract]
        Int32 Save_cred_Adj(ScanPhysicalDocReceiveDet _doc, string _bsta_com, string _bsta_pc, DateTime _bsta_dt, string _bsta_accno, string _bsta_adj_tp, string _bsta_adj_tp_desc, Decimal _bsta_amt, string _bsta_refno, string _bsta_rem, string _bsta_cre_by, DateTime _month, Int32 _week, string _bnk_id, string _bnk_cd, string _mid, Int32 _seq);
        //tharanga 2018/05/02
        [OperationContract]
        DataTable Get_cred_Adj(string _com, DateTime _date, string _accno);
        //tharanga 2018/05/02
        [OperationContract]
        Int32 Finalize_cred_Realization(string _Bsth_com, DateTime _Bsth_dt, string _Bsth_accno, string _user, string _session, List<gnt_cred_stmnt_det> _credlsList, string _mid);
        //tharanga 2018/05/02
        [OperationContract]
        Int32 Update_cred_AdjPC(string _com, string _pc, DateTime _date, string _accno, string _adjtp, string _ref, string _newpc, Decimal _val, string _newrem);

        //tharanga 2018/05/04
        [OperationContract]
        Int32 Update_bank_recon_remark(string _com, string _pc, DateTime _date, string _accno, string _adjtp, string _ref, string remark, Int32 _seq);
        //tharanga 2018/05/04
        [OperationContract]
        Int32 Update_bank_recon_hdr_status(string _com, DateTime _date, string _accno, string _status);
        //tharanga 2018/05/08
        [OperationContract]
        Int32 uploda_excle_BankAdj(List<ScanPhysicalDocReceiveDet> _doc);
        //tharanga 2018/05/08
        [OperationContract]
        DataTable chk_cheque_realized(string _com, string _pc, DateTime _date, string _doc_tp, string _doc_ref, string _acc_no);
        //Nuwan 2018.05.09
        [OperationContract]
        List<REF_TMPLT_HED> getTemplateHeaderDetails(bool withdeleted, int pgeNum, int pgeSize, string searchFld, string searchVal, out string error);
        //Nuwan 2018.05.09
        [OperationContract]
        Int32 updateStusTemplateHeader(Int32 hedid, Int32 stus, string updateby, out string error);
        //Nuwan 2018.05.09
        [OperationContract]
        REF_TMPLT_HED getTemplateHedDet(Int32 hedid, out string error);
        //Nuwan 2018.05.09
        [OperationContract]
        List<REF_TMP_OBJHEDDET> getTemplateHedItemDet(Int32 hedid, out string error);
        //Nuwan 2018.05.10
        [OperationContract]
        List<REF_OBJ_LIST_DET> getTemplateComboDet(Int32 detid, out string error);
        //Nuwan 2018.05.10
        [OperationContract]
        Int32 updateItemListStatus(Int32 detid, string userid, out string error);
        //Nuwan 2018.05.10
        [OperationContract]
        List<REF_OBJ> getAllTemplateItemDetails(out string error);
        //Nuwan 2018.05.10
        [OperationContract]
        List<REF_OBJ_LIST_DET> getTemplateComboDetItmDet(Int32 itmid, out string error);
        //Nuwan 2018.05.10
        [OperationContract]
        Int32 addTemplateField(Int32 hedid, Int32 detid, string name, string creby, out string error);
        //tharanga 2018/05/09
        [OperationContract]
        DataTable LOAD_MID_DET(string _bank, string _mid);
        //Wimal 18/May/2018
        [OperationContract]
        int Allocate_SUNReceipt(string p_comcode, string p_receiptno);
        //Wimal 18/May/2018
        [OperationContract]
        bool SUNPeriodclose(string p_comcode, string p_db, DateTime chkDate);
        //Wimal 18/May/2018
        [OperationContract]
        bool validateSUNACC(string p_comcode, string p_db, string p_accno);
        //subodana 2018-05-18
        [OperationContract]
        bool IsHUGOItem(string si);
        //subodana 2018-05-21
        [OperationContract]
        decimal Cal_CDEM_RENT(string _blNo, int _demurrageDays, decimal _exRate);
        //subodana 2018-05-23
        [OperationContract]
        List<ref_acc_sgrp> GetAccMainType(string com, string mtype);
        //subodana 2018-05-24
        [OperationContract]
        DataTable GetSubTypes(string _com, string subgrp);
        //subodana 2018-05-25
        [OperationContract]
        decimal GetBLInsuVal(string sino);

        //Tharindu 2017-11-24
        [OperationContract]
        Int32 SaveFineChargesExecl(List<FineCharges> _lstfinecharges, out string _err);

        //Tharindu 2017-11-24
        [OperationContract]
        Int32 SaveFineSetOff(string com, string pc, DateTime setdt, int status, string creby, DateTime credt, decimal setoffval, int seqno, out string _err);

        //Tharindu 2017-11-24
        [OperationContract]
        DataTable ChekSchemeValidCode(string p_type, string p_term);

        //Tharindu 2017-11-24
        [OperationContract]
        Int32 SaveTradingInterestDetails(List<TradingInterest> _lstTradingInterest, out string _err);
        //Pasindu 15/05/2018
        [OperationContract]
        int SAVE_RENT_PAYMENT_DETAILS(MasterAutoNumber p_autonumber, string p_paymenttype, string p_paymentsubtype, string psh_com, string psh_pc, string psh_add1, string psh_add2, string psh_dist, string psh_prv, string p_creditaccount, string p_debitaccount, string psh_frm_dt, string psh_to_dt, string psh_ref_no, string psh_rmk, string psh_trm, string psh_cre_by, string p_sqfeet, List<PAY_SCH_OWNERS_DET> ownerlist, string p_psh_no, List<PAY_SCHEDULE_DETAILS> schedule_data, out string err);

        //pasindu 2018/05/16
        [OperationContract]
        List<SCH_PAY_COLUMN> getSCHColumns(string p_type);

        //Pasindu 2018/05/18
        [OperationContract]
        List<PAY_SCH_OWNERS_DET> getSCHOwnerDetails(string p_schid, string p_type);

        //Pasindu 2018/05/21
        [OperationContract]
        int REMOVE_SCH_OWNER(string p_psa_fld_cd, string p_psa_hed_cd, string p_psa_rec_id, string p_user);

        [OperationContract]
        Int32 UPDATE_PAY_SCH_DETAILS(List<PAY_SCHEDULE_DETAILS> schedule_data, string p_user, out string err);

        //Pasindu 2018/25/28
        [OperationContract]
        Int32 UPDATE_SCH_STATUS(string p_psh_no, string p_userid, out string err);

        //Pasindu 2018/05/28
        [OperationContract]

        DataTable GetTradingdetails(DateTime frmdte, DateTime todte);
        //subodana 2018-05-31
        [OperationContract]
        Int32 UPDATE_ClosingBalAccounts(Int64 accounts, decimal closingbal, Int64 Seq, out string _err);
        //tharanga 2018/05/09
        [OperationContract]
        Int32 save_cred_rls_hdr(gnt_cred_stmnt_hdr _gnt_cred_stmnt_hdr, out string _err);
        //Nuwan 2018.05.11
        [OperationContract]
        Int32 updateLabelText(Int32 id, string name, string userId, out string error);
        //Nuwan 2018.05.11
        [OperationContract]
        Int32 createNewTemplate(string desc, string createby, List<REF_TMPLT_DET_SIN> details, out string error);
        //Nuwan 2018.05.12

        //Pasindu 2018/05/28
        [OperationContract]
        List<PAY_SCHEDULE_DETAILS> getScheduleDetails(string p_psh_no);
        //subodana 2018-06-01
        [OperationContract]
        DataTable GetAccountFMainTypes(string _com);
        //subodana 2018-06-02
        [OperationContract]
        Int32 SaveAccountGroupDetails(ref_acc_sgrp GrpOb, out string _err);
        //subodana 2018-06-05
        [OperationContract]
        Int32 SaveChartAccDetails(List<ref_cht_acc> _list, out string _err);
        //subodana 2018-06-05
        [OperationContract]
        List<ref_cht_accgrp> GetAccGroup(string Code);
        //subodana 2018-06-05
        [OperationContract]
        List<ref_cht_acc> GetAcc(string Code);
        //subodana 2018-06-05
        [OperationContract]
        DataTable GetAccHeading();
        //subodana 2018-06-08
        [OperationContract]
        decimal WalfExsCalcAAL(string _blNo, int _demurrageDays, decimal _exRate);
        //subodana 2018-06-08
        [OperationContract]
        decimal HeroMotobikeCharge(string sino);
        [OperationContract]
        List<REF_OBJ_TEMPITMFRM> getTemplateDetailtoFrom(Int32 hedid, out string error);
        //Nuwan 
        [OperationContract]
        Int32 saveAccountDefinitionData(string name, List<REF_TMPLT_VALUE> itemVal, out string error);
        //Nuwan 2018.05.15
        [OperationContract]
        List<REF_OBJ_TEMPITMFRM> getTeplateSavedData(string code, string company, out string error);
        //Nuwan 2018.05.15
        [OperationContract]
        Int32 removeDocumentTepllate(Int32 hed, string assigncode, string userid, out string error);
        //subodana 2018-06-16
        [OperationContract]
        DataTable GetAccHeadingdetails(string Code);
        //subodana 2018-06-16 
        [OperationContract]
        List<ref_acc_sgrp> GetAccMainTypeDet(string com, string mtype, string sub);
        //subodana 2018-06-16
        [OperationContract]
        DataTable GetSubTypesDetails(string _com, string sungrp, string tsub);
        //subodana 2018-06-18
        [OperationContract]
        string GetSIItemCat1(string ItemCode);
        //subodana 2018-06-18
        [OperationContract]
        string GetSIItemCat2(string ItemCode);
        //subodana 2018-07-10
        [OperationContract]
        Boolean IsCosting(string SINO);
        //subodana 2018-07-10
        [OperationContract]
        decimal GetBDLTransINV(string InvNo);

        //Tharindu 2018-07-23
        [OperationContract]
        DataTable GetBnkdetails(string _com, string _loc, string _chkref, string _accno);
        //Nuwan 2018.07.11
        [OperationContract]
        List<REF_OBJ_TEMPITMFRM> getSavedItemTemplateVal(string module, string code, string company, string savedVal, out string error);
        //Nuwan  2018.07.25
        [OperationContract]
        List<TABLE_HED> getTemplateTableColum(string company, string code, out string error);
        //Nuwan 2018.07.25
        [OperationContract]
        List<REF_TMPLT_ITM_VALUE_TABLE> getItemSavedValues(string module, string code, string company, string savedVal, out String error);

        //Tharindu
        [OperationContract]
        DataTable getcurrfinecharges(string com, string p_pc, DateTime _date);
        //Nuwan 2018.08.03
        [OperationContract]
        string getFieldType(Int32 hedid, Int32 detid, out string error);
        //Nuwan 2018.08.09
        [OperationContract]
        Int32 savePaymentRequestDetails(MST_PAY_REQ_HDR hdr, List<MST_PAY_REQ_DET> det, List<VALUE_ITM_LIST> rndmItm, List<MST_ACC_TAX> MST_ACC_TAX, List<PUR_SELECTED> PUR_SELECTED, MasterAutoNumber _masterAutoNumber, out string error);
        //Nuwan 2018.08.10
        [OperationContract]
        MST_PAY_REQ_HDR getPaymentreqHdr(string reqno, string com, string reqtp, out string error);
        //Nuwan 2018.08.10
        [OperationContract]
        List<MST_PAY_REQ_DET> getPayReqdetails(string com, string type, string reqno, out string error);
        //tharanga 2018/08/24
        [OperationContract]
        DataTable chk_vreg_available(string _com, string _pc, string _invno, string _sal_tp, string _itm, string _engine, string _chasi);
        //subodana 2018-08-29
        [OperationContract]
        Int32 UpdateHPRAccRemks(List<hpt_arr_acc> _list, out string _err);
        //tharanga 2018/09/01
        [OperationContract]
        Int32 Update_cred_recon_hdr_status(string _com, DateTime _date, string _accno, string _status);
        //tharanga 2018/09/01
        [OperationContract]
        Int32 Update_cred_recon_remark(string _com, string _pc, DateTime _date, string _accno, string _adjtp, string _ref, string remark, Int32 _seq);
        //Nuwan 2018.09.07
        [OperationContract]
        Int32 updateTempFiledDefNumber(Int32 detid, bool value, string user, out string error);
        //Nuwan 2008.09.12
        [OperationContract]
        Int32 updateTempFiledSrch(Int32 objid, string val, string userId, out string error);
        //Nuwan 201.09.12
        [OperationContract]
        int updatePaymentRequestDetails(MST_PAY_REQ_HDR hdr, List<MST_PAY_REQ_DET> accdet, List<VALUE_ITM_LIST> VALUE_ITM_LIST, List<MST_ACC_TAX> MST_ACC_TAX, out string error);
        //Nuwan 2018.09.13
        [OperationContract]
        SAR_PAY_TP getAccPAymentType(string code, string company, out string error);
        //Nuwan 2018.09.13
        [OperationContract]
        REF_CHT_ACC getAccountDetail(string code, string company, out string error);
        //subodana
        [OperationContract]
        List<ref_eli_comm_targ> GetEliteCommTrgt(Int64 Seq);
        //subodana
        [OperationContract]
        List<ref_comm_add_trgt> GetEliteCommAdditional(Int64 Seq);
        //Nuwan 2018.09.18
        [OperationContract]
        Int32 getTepmplateUsedCount(Int32 hedid, out string error);
        //Nuwan 2018.09.19
        [OperationContract]
        Int32 approvePaymentRequet(string ReqNo, string company, string userId, string stus, string remark, string sessionid, out string error);
        //Nuwan 2018.09.19
        [OperationContract]
        Int32 processPaymentRequest(string ReqNo, string company, string userId, MasterAutoNumber AutoNum, string sessionid, string reqtp, out string error);
        //Dulaj 2018/OCt/05
        [OperationContract]
        decimal GetAssemblyAmountProfitability(string com, string itemCd, string model, string cat01, string cat02);
        //Dulaj 2018/OCt/05
        [OperationContract]
        DataTable GetBlItm(string siNo);
        //tharanga 2018/10/22
        [OperationContract]
        List<ChequeReturn> Getreturn_cheq_cout_data(string _pc, DateTime rtndt, string _com, Int16 _datecount);
        //tharanga 2018/09/5
        [OperationContract]
        List<Ref_Bill_Collet> GetRef_Bill_Collet_dayend(string Com, string Pc, DateTime Date);
        //tharanga 2018/09/5
        [OperationContract]
        Int32 ref_bill_collect_conform(string _com, string _pc, DateTime _date, string _user, Int32 conf_stus, Int32 _seq);
        //tharanga 2018/09/5
        [OperationContract]
        DataTable GetRevertReleaseAccountDetail(string _COM, string _PC, string _ACC, string _ITM);



        //Dulaj 2018/OCt/05
        [OperationContract]
        //int UpdateBLStatus(string com, string blno, string status);
        int UpdateBLItmRemarks(string blNo, string itmCd, Int32 lineNo, string remarks);
        //Nuwan 2018/10.26
        [OperationContract]
        List<MST_PAY_REQ_HDR> getAllRequestDetails(string company, string stus, string type, out string error);
        //Tharanga 2018/11/06
        [OperationContract]
        Int32 save_log_sunupload(log_sunupload _log_sunupload, out string error);
        //Tharanga 2018/11/06
        [OperationContract]
        List<log_sunupload> getlog_sunupload(log_sunupload _log_sunupload, out string error);
        //Dulaj 2018/Dec/07
        [OperationContract]
        DataTable GetCusdecHDRDataShipment(string entryno);
        [OperationContract]
        int UPDATE_CUS_DEC_HDR(string bondNo, string model, string clrBy, decimal cif, string rmk, string user, string entry, DateTime actulaDate, DateTime filereceidDate);
        //Wimal 28/11/2018
        [OperationContract]
        DataTable get_DepositBank_Pc_wise(string p_com, string p_prof_cen);
        //Dulaj 2018/12/13
        [OperationContract]
        List<MasterSalesPriorityHierarchy> GetPcInfo(string com, string pc, string code, string type);

        //Wimal 13/Dec/2018
        [OperationContract]
        DataTable Get_RevertReconDetl(string _com, string _pc, DateTime _fromDate, DateTime _toDate, string _userID);
        //Nuwan 2018/12/20
        [OperationContract]
        List<MST_ACC_TAX> getRequestTaxDetails(string com, string reqno, out string error);
        //Nuwan 2018/12/21
        [OperationContract]
        List<MST_BUSENTITY_TAX> getCreditorTaxStructure(string creditor, string company, out string error);
        //Nuwan 2018/Dec/27
        [OperationContract]
        Int32 getPurCostAndTax(string purno, string company, out decimal cost, out decimal vat, out decimal nbt, out string error);
        //Nuwan 2018.12.27
        [OperationContract]
        List<MST_PAY_REQ_REF> getPayReqPoDet(string reqno, string reqtp, string compamy, out string error);
        //dulaj 2019/01/2
        [OperationContract]
        DataTable GetCusdechdrByBond(string com, string blno);
    }
}
