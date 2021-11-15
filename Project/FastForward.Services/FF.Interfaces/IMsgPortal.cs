using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using FF.BusinessObjects;
using System.Data;
using FF.BusinessObjects.General;
using FF.BusinessObjects.ReptObj;
using FF.BusinessObjects.MessagePortal;
using FF.BusinessObjects.InventoryNew;

namespace FF.Interfaces
{
    [ServiceContract]
    public interface IMsgPortal
    {
        //kapila
        [OperationContract]
        DataTable HPMobileIntIncomeReport(string _user, DateTime _from, DateTime _to, string _item, string _cate, string _brand, string _group, string _com, string _pc);
        //kapila
        [OperationContract]
        DataTable getValuationDetails_Insu_new(DateTime _fromDate, DateTime _toDate, string _ItemClasif, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _Stktype, string _Group, string _CostOrValue, string _Com, string _User, Int32 withsts, out string _err);
        //kapila
        [OperationContract]
        string getValuationDetails_Insu(DateTime _fromDate, DateTime _toDate, string _ItemClasif, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _Stktype, string _Group, string _CostOrValue, string _Com, string _User, out string _err, Int32 withsts);
        //kapila
        [OperationContract]
        bool valuationProcess_Insu(DateTime _fromdate, DateTime _todate, DateTime asAtDate, string company, string location, bool mnthEnd, string userid, string adminteam, Int32 _inSeq, Int32 _outSeq, out string error, bool isAgemonitor, bool isAgemonitor_com, bool unComPath = false);
        //kapila
        [OperationContract]
         DataTable GetItemWiseGpExcel_new(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor,
    int _freeissue, string _itemclasif, string _brndmgr, string _Group, bool withReversal, int _rplitm, DateTime _fromDate2, DateTime _toDate2, Int16 _intercom);
        //Sanjeewa
        [OperationContract]
        string GetPLDetails(DateTime _fromDate, DateTime _toDate, string _Com, string _User, out string _err);
        //kapila
        [OperationContract]
        DataTable GetLastNoSeqPageDetails_Pro(string _loc, string _reptype);
        //kapila
        [OperationContract]
        DataTable GetLastNoSeqDetails_Pro(string _loc,string _reptype);
        //kapila
        [OperationContract]
        DataTable GetForwardSalesDetailAudit_Pro(string _loc);
        //kapila
        [OperationContract]
        DataTable UnconfirmedAODDet_Pro(string _loc);
        //kapila
        [OperationContract]
        string GetSchemeDefinitionDetails(string _Com, string _User, string _Circ, out string _err);

        string getAdjustmentDetails(DateTime _fromDate, DateTime _toDate, string _user, string _com, string _channel, out string _err);
        //kapila
        [OperationContract]
        string Get_Cash_Comm_Summ(DateTime _from, DateTime _to, string _com, string _user, out string _err);
        //kapila
        [OperationContract]
        string Get_ClosedAccountsDet(DateTime _fromDate, DateTime _toDate, string _com, string _user, out string _err);
        //kapila
        [OperationContract]
        string Get_CIH_Summary(DateTime _asatdate, string _com, string _user, out string _err);
        //kapila
        [OperationContract]
        string GetCreditNoteDetails(DateTime _fromDate, DateTime _toDate, string _com, string _User, string _doctp, out string _err);
        //kapila
        [OperationContract]
        string GetSOSReconcilation(DateTime _fromDate, DateTime _toDate, string _com, string _pc, string _user, out string _err);
        //kapila
        [OperationContract]
        DataTable GetSalesReconcilationDetails(DateTime _fromDate, DateTime _toDate, string _com, string _loc);
        //kapila
        [OperationContract]
        DataTable PrintHPServiceCharge(string _com, string _pc, DateTime _from, DateTime _to, string _item, string _cate1, string _cate2, string _cate3, string _brand, string _model);

        //kapila
        [OperationContract]
        DataTable ProcessMovementListing(DateTime in_FromDate, DateTime in_ToDate, string in_user_id, string in_Location_code, string in_Company, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3);
        //written by Sanjeewa 20-03-2014
        [OperationContract]
        DataTable GetLoyalityDiscountDetails(DateTime _fdate, DateTime _tdate, string _Com, string _Pc, string _Loytp, string _custtp, string _userid);

        //written by Sanjeewa 2014-03-10
        [OperationContract]
        DataTable GetASalesDetails(DateTime _fromDate, DateTime _toDate, string _User, string _com, string _Pc);
        //written by Sanjeewa 2013-03-03
        [OperationContract]
        DataTable GetSalesFiguresDetails(DateTime _fromDate, DateTime _toDate, string _User, string _Pc);


        [OperationContract]
        DataTable GetPriceLocationDetails1(int in_seq, string in_promo);

        [OperationContract]
        DataTable GetPriceCombineDetails1(string in_item, int in_seq, int in_line);

        [OperationContract]
        DataTable GetPriceDetails1(string _circular, string _promocode, string _pricetp, string _pb, string _pblevel, DateTime _dtfrom, DateTime _dtto, DateTime _dtasat,
            string _customer, string _itemcode, string _brand, string _model, string _itemcat1, string _itemcat2, string _itemcat3, string _user, string _reptp);

        //written by Sanjeewa 2013-03-28
        [OperationContract]
        DataTable GetForwardSalesDetails(DateTime _asAtDate, string _User, string _brand, string _model, string _itemcode, string _itemcat1, string _itemcat2, string _itemcat3, string _agetp, decimal _age, string _com, string _pc, string _latestcost, string _otherloc, string _customer);

        [OperationContract]
        DataTable GetHireSalesCancelDetails(DateTime _fromDate, DateTime _toDate, string _User, string _pc);
        //written by Sanjeewa 2013-02-11
        [OperationContract]
        DataTable GetHireSalesDetails(DateTime _fromDate, DateTime _toDate, string _DocSubTp, string _PCenter, string _User, string _RepType);

        //written by Sanjeewa 2013-02-11
        [OperationContract]
        DataTable GetCashSalesAccountDetails(DateTime _fromDate, DateTime _toDate, string _DocSubTp, string _PCenter, string _User, string _RepType);

        //written by Sanjeewa 2013-02-11
        [OperationContract]
        DataTable GetCashSalesDODetails(DateTime _fromDate, DateTime _toDate, string _DocSubTp, string _PCenter, string _User, string _RepType);

        //written by Sanjeewa 2013-02-10
        [OperationContract]
        DataTable GetCashSalesCommissionDetails(DateTime _fromDate, DateTime _toDate, string _DocSubTp, string _PCenter, string _User, string _RepType);

        //written by Sanjeewa 2013-02-08
        [OperationContract]
        DataTable GetCashSalesSummaryDetails(DateTime _fromDate, DateTime _toDate, string _DocSubTp, string _PCenter, string _User, string _RepType);

        //kapila 18/2/2013
        [OperationContract]
        DataTable Get_Receipts_Rem_Det(string _com, string _pc, string _user, DateTime _from, DateTime _to);
        //written by Sanjeewa 2014-10-13
        [OperationContract]
        Boolean ActivePromotionDetails(string _com, string _circular, string _promocode, string _pricebook, string _pblevel, string _status, string _paytp, string _isasatdate, DateTime _fDate, DateTime _tDate, DateTime _asatDate, string _User, string _isallloc, out string _err, out string _filePath);

        //kapila
        [OperationContract]
        DataTable BOCCustomersPrint(string _com, string _loc, string _batch, DateTime _fromDate, DateTime _toDate, string _user);
        //kapila
        [OperationContract]
        DataTable BOCCustReceipt(string _com, string _loc, string _ser);
        //written by Sanjeewa 29-09-2015
        [OperationContract]
        DataTable GetDeliveryCustomerPendingDetails(DateTime _fdate, DateTime _tdate, string _Com, string _Pc, string _cat1, string _cat2, string _cat3, string _item, string _brand, string _model, string _customer, string _userid, Int16 _cost);

        //written by Wimal 22/06/2015
        [OperationContract]
        DataTable GetSalesPromoterDetails(DateTime _fDate, DateTime _tDate, string _Com, string _Pc);
        //kapila
        [OperationContract]
        DataTable GetPricebyCircular(string _cir, string _promo);
        //written by Sanjeewa 2013-03-03
        [OperationContract]
        DataTable GetSalesTargetAchievementDetails(DateTime _fromDate, DateTime _toDate, string _brand, string _Com, string _Pc, string _User);

        //kapila
        [OperationContract]
        DataTable PrintTempIssueItems(string _com, string _loc, DateTime _fromDate, DateTime _toDate);
        //Written By Wimal on 14/08/2013
        [OperationContract]
        DataTable GetSerialCompanyLocationAge(string _user, string _comcode, string _location);
        //Written By Nadeeka 28/02/2013
        [OperationContract]
        DataTable ProcessMovementAuditTrial(DateTime in_FromDate, DateTime in_ToDate, string in_DocType, string in_Location_code, string in_Company, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3, string in_direc, string in_user, string in_subType, string _docno);

        [OperationContract]
        DataTable ProcessMovementAuditTrial_ARL(DateTime in_FromDate, DateTime in_ToDate, string in_DocType, string in_Location_code, string in_Oth_Location_code, string in_Company, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3, string in_direc, string in_user, string in_subType, string _docno);

        //Written By Sanjeewa on 30/11/2013
        [OperationContract]
        DataTable GetSerialBalance_Curr(string _com, string _loc, string _item, string _itemstatus, string _withRCC);

        //Written By Sanjeewa on 03/03/2013
        [OperationContract]
        DataTable GetSerialBalance();
        //Sanjeewa
        [OperationContract]
        DataTable GetInsuredStockDetails(string _user, string _com, string _loc);
        //kapila
        [OperationContract]
        DataTable ProcessBOCReservedSerials(DateTime in_FromDate, DateTime in_ToDate, string in_DocType, string in_Location_code, string in_Company, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3, string in_direc, string in_user, string in_subType);
  
        //kapila
        [OperationContract]
        DataTable getSubLocationStock(string _com, string _mloc, string _sloc, Int32 _isOpr, DateTime _endDate, string _user, Int32 _isRep);

        //Lakshan 05 Oct 2016
        [OperationContract]
        DataTable StockBalanceSearchNew(DateTime _from, DateTime _to, string _item, string _loc, string _com, bool isStatus, string _doctype = null);

        //Lakshika 2016-10-04
        [OperationContract]
        DataTable GetPurchaseReturnDetails(DateTime _fromDate, DateTime _toDate, string _com, string _loc, string _channel, string _supplier, string _user);

        //Lakshan 05 Oct 2016
        [OperationContract]
        DataTable GetInventoryBalanceAsAt(
          string _userr, string _chnl, string _brand, string _model, string _item, bool _itemSts, string _itmCat1, string _itmCat2, string _itmCat3,
          Int32 _withCost, DateTime _asAtDate, Int32 _withSer, Int32 _status, string _com, string _loc);

        //kapila
        [OperationContract]
        DataTable ProcessPSI(DateTime in_FromDate, DateTime in_ToDate, string in_user_id, string in_Location_code, string in_Company, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3);
       
        //Lakshika 2016-09-05
        [OperationContract]
        DataTable Get_INR_SER_DATA_ADVANCED(InventorySerialN _ser);
        //Sanjeewa
        [OperationContract]
        string Get_GIT_Details(DateTime _asat, string _com, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string in_Itemcat4, string in_Itemcat5, string _user, out string _err);

        [OperationContract]
        string GetCommonExcelDetails(string _SQL, string _user, out string _err);

        //written by sachith on 01/01/2013
        //modified b kelum on 2016-May-11
        [OperationContract]
        DataTable SerialBalanceSearch1(DateTime _from, DateTime _to, string _item, string _loc, string _com, string _serial = null);

        //written by sachith on 01/01/2013
        //modified by kelum 2016-May-11
        [OperationContract]
        DataTable StockBalanceSearch1(DateTime _from, DateTime _to, string _item, string _loc, string _com, bool isStatus, string _doctype = null);

        //Written By Sanjeewa on 04/03/2013 
        [OperationContract]
        DataTable GetStockBalanceCurrent(string _user, string _channel, string _brand, string _model, string _itemcode, string _Itemstatus,
            string _itemcat1, string _itemcat2, string _itemcat3, Int16 _withcost, Int16 _serialrep, string _com, string _loc, string _withGIT, string _withRCC, string _withJob,Int16 _discontinue);

        [OperationContract]
        DataTable GetStockBalanceCurrent_Expiry(string _user, string _channel, string _brand, string _model, string _itemcode, string _Itemstatus,
            string _itemcat1, string _itemcat2, string _itemcat3, Int16 _withcost, Int16 _serialrep, string _com, string _loc, string _withGIT, string _withRCC, string _withJob, Int16 _discontinue);


        //Written By Sanjeewa on 11/12/2013 
        [OperationContract]
        DataTable GetReservedSerialDetails(DateTime _fromDate, DateTime _toDate, string _user, string _com, string _pc);

        //Written By Sanjeewa on 23/08/2013 
        [OperationContract]
        DataTable GetSIntrDetails(string _user, DateTime _fromDate, DateTime _toDate, string _brand, string _model, string _itemcode, string _itemcat1, string _itemcat2, string _itemcat3, string _doctype, string _reptpdesc, string _com, string _loc);

        //Written By Sanjeewa on 25/03/2013 
        [OperationContract]
        DataTable GetSGRANDetails(string _user, DateTime _fromDate, DateTime _toDate, string _brand, string _model, string _itemcode, string _itemcat1, string _itemcat2, string _itemcat3, string _doctype, string _reptpdesc);

        [OperationContract]
        DataTable GetStockBalanceWithSerial_Asat(string _user, string _channel, string _brand, string _model, string _itemcode, string _Itemstatus,
            string _itemcat1, string _itemcat2, string _itemcat3, Int16 _withcost, DateTime _asatdate, Int16 _serialrep, Int16 _status, string _Loc, string _Com);

        [OperationContract]
        DataTable GetStockBalanceWithSerial(string _user, string _channel, string _brand, string _model, string _itemcode, string _Itemstatus,
            string _itemcat1, string _itemcat2, string _itemcat3, Int16 _withcost, DateTime _asatdate, Int16 _serialrep, Int16 _status);

        //Written By Nadeeka on 18/06/2013
        [OperationContract]
        DataTable GetStockBalanceWithCost(string _user, string _channel, string _brand, string _model, string _itemcode, string _Itemstatus,
            string _itemcat1, string _itemcat2, string _itemcat3, Int16 _withcost, DateTime _asatdate, Int16 _serialrep, Int16 _status, string _Loc, string _Com, string _color, string _size);

        //Written By Chamal on 17/02/2014
        [OperationContract]
        DataTable GetFATDetails1(DateTime _fromDate, DateTime _toDate, string _User, string _com, string _pc, string _docType);
        //Written By Sanjeewa on 14/10/2013
        [OperationContract]
        DataTable GetOtherShopDO(DateTime _fromDate, DateTime _toDate, string _Cust, string _brand, string _model, string _itemcode,
            string _itemcat1, string _itemcat2, string _itemcat3, string _User, string _InvNo, Int16 _isExport);
        //Written By Sanjeewa on 01/03/2013
        [OperationContract]
        DataTable GetStockBalance(string _user, string _channel, string _brand, string _model, string _itemcode, string _Itemstatus,
            string _itemcat1, string _itemcat2, string _itemcat3, Int16 _withcost, DateTime _asatdate, Int16 _serialrep, Int16 _discontinue);

        //Written By Chamal on 17/02/2014
        [OperationContract]
        DataTable GetFIFONotFollowedDetails1(string _user, string _brand, string _model, string _itemcode, string _Itemstatus,
            string _itemcat1, string _itemcat2, string _itemcat3, DateTime _asatdate, string _com, string _loc, DateTime _fromDate, DateTime _toDate, Int16 _fifo);

        [OperationContract]
        DataTable GetRCCSerialSearchData_Stock(string _com, string _loc, int _isSameLoc, int _isStockItem, string _searchText, string _searchCriteria);

        //Written By Nadeeka 27/02/2013
        [OperationContract]
        DataTable ProcessMovementAuditTrialWithSerial(DateTime in_FromDate, DateTime in_ToDate, string in_DocType, string in_Location_code, string in_Company, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3, string in_direc, string in_user, string in_subType);

        //Written By Nadeeka 23/02/2013
        [OperationContract]
        DataTable ProcessInventoryStatement(DateTime in_FromDate, DateTime in_ToDate, string in_user_id, string in_Location_code, string in_Company, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3);
       
        //Written By Nadeeka 21/05/2013
        [OperationContract]
        DataTable SerialAgeReport(DateTime in_AsatDate, string in_user_id, string in_Company, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_model, string in_itemsStatus, string in_loc, Int32 in_fromage, Int32 in_toage, Int32 in_iscomage);

        //Written By Nadeeka 21/05/2013
        [OperationContract]
        DataTable NonMovingReport(DateTime in_FromDate, DateTime in_ToDate, string in_user_id, string in_Company, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_model, Int16 in_AllCompany);

        //Written By Nadeeka 21/05/2013
        [OperationContract]
        DataTable FastMovingReport(DateTime in_FromDate, DateTime in_ToDate, string in_user_id, string in_Company, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_model, Int16 in_AllCompany, Int16 in_isFast,string in_pc);

        //Sanjeewa
        [OperationContract]
        DataTable GetCurrBalancewithPriceDetails(string _user, string _com, string _loc, string _supplier, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3);

        //written by Nadeeka 2015-04-27
        [OperationContract]
        DataTable GetpoSummary(string _com, string _type, string _supplier, string _item, string _cat1, string _cat2, string _cat3, string _brand, String _model, DateTime _fdate, DateTime _tdate);

        //kapila
        [OperationContract]
        DataTable GetPendingMyAbansDocs();
        //kapila
        [OperationContract]
        string  inventoryBalance_Asat(string _user, string _channel, string _brand, string _model, string _itemcode, string _Itemstatus,
string _itemcat1, string _itemcat2, string _itemcat3, string _itemcat4, string _itemcat5, Int16 _withcost, DateTime _asatdate, Int16 _serialrep, string _com, string _loc, string _withRCC, string _withJob, string _withGIT, out string _err);
        //kapila
        [OperationContract]
        string inventoryBalanceSerial_Asat(string _user, string _channel, string _brand, string _model, string _itemcode, string _Itemstatus,
  string _itemcat1, string _itemcat2, string _itemcat3, string _itemcat4, string _itemcat5, Int16 _withcost, DateTime _asatdate, Int16 _serialrep, string _com, string _loc, string _withRCC, string _withJob, string _withGIT, out string _err);
        //kapila
        [OperationContract]
        string GetStockBalanceCurrent_SCM_new(string _user, string _channel, string _brand, string _model, string _itemcode, string _Itemstatus,
  string _itemcat1, string _itemcat2, string _itemcat3, string _itemcat4, string _itemcat5, Int16 _withcost, Int16 _serialrep, string _com, string _loc, string _withRCC, string _withJob, string _withGIT, out string _err);
        //kapila
        [OperationContract]
        DataTable ProcessInventoryStatement_SCM(DateTime in_FromDate, DateTime in_ToDate, string in_user_id, string in_Location_code, string in_Company, string in_Brand, string in_Model, string in_Itemcode, string in_Itemcat1, string in_Itemcat2, string in_Itemcat3);
        //kapila
        [OperationContract]
        string ConsignDetailsReport(DateTime _fromDate, DateTime _toDate, string _com, string _user, string _pb, string _pblvl, out string _err);
        [OperationContract]
        string getItemListDetails(string _cat,string _cat1, string _com, string _user, out string _err);
                
        //Sanjeewa
        [OperationContract]
        string SRMRNStatusReport(DateTime _fromtDate, DateTime _totDate, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _Com, string _OtherLoc, string _User, out string _err);

        //kapila
        [OperationContract]
        DataTable CusDecEntryRequest(string in_user, string in_Company, DateTime in_fromdate, DateTime in_todate, string in_subtp, Int32 _in_isall);
        //dilshan on 22/03/2018
        [OperationContract]
        DataTable TRNoteRequest(string in_user, string in_Company, DateTime in_fromdate, DateTime in_todate, string in_subtp, Int32 _in_isall, string in_docno, string entry01, string entry02);
        //kapila
        [OperationContract]
        DataTable getAsAtAgeDetails(DateTime _from, DateTime _to, string _com, string _loc, string _itemcat1, string _itemcat2, string _itemcat3, string _itemcat4, string _itemcat5,
string _itemcode, string _brand, string _model, string _supplier, string _user, string _brndMan,Int16 _isComAge,string _pb="ABANS", string _pblvl="A");

        //kapila
        [OperationContract]
        DataTable ReservationReport(string in_user, string in_Company, string in_loc, DateTime in_fromdate, DateTime in_todate, DateTime in_expdate, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_cat4, string in_cat5, string in_model);

        //kapila
        [OperationContract]
        DataTable MRNStatusReport(string in_user, string in_Company, string in_loc, string in_to, DateTime in_fromdate, DateTime in_todate, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_cat4, string in_cat5, string in_model);

        //kapila
        [OperationContract]
        string ItemBufferStatusReport_Excel(string in_user_id, string in_Company, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_cat4, string in_cat5, string in_model, string in_loc, string _in_subtp, out string _err);

        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetDeliverCustomer(string _invNo);
        
        //kapila
        [OperationContract]
        DataTable ToBondStatusReport(DateTime _fromdate, DateTime _todate, string _tobondno, string _grnno, string _reqno, string in_user_id, string in_Company, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_cat4, string in_cat5, string in_model, string in_loc);

        //Sanjeewa
        [OperationContract]
        DataTable ImportScheduleReport(DateTime _fromdate, DateTime _todate, string in_Company, string _adminteam, string in_user_id);
        
        [OperationContract]
        DataTable ImportScheduleGRNDtl(DateTime _fromdate, DateTime _todate, string in_Company, string _adminteam, string in_user_id);
        
        [OperationContract]
        string ImportScheduleReport_Excel(DateTime _fromdate, DateTime _todate, string in_Company, string _adminteam, string in_user_id, out string _err);

        //kapila
        [OperationContract]
        DataTable ItemBufferStatusReport(string in_user_id, string in_Company, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_cat4, string in_cat5, string in_model, string in_loc, string _in_subtp);
        //kapila
        [OperationContract]
        DataTable PrintMoveCostDetailReport(string _com, string _loc, DateTime in_FromDate, DateTime in_ToDate,string _doctp,string _direct, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_cat4, string in_cat5, string in_model,string _docno);

        //kapila
        [OperationContract]
        DataTable PrintTransDetListReport(string _com, string _loc, DateTime in_FromDate, DateTime in_ToDate, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_cat4, string in_cat5, string in_model);

        //kapila
        [OperationContract]
        DataTable SerialAgeReport_SCM(DateTime in_AsatDate, string in_user_id, string in_Company, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_cat4, string in_cat5, string in_model, string in_itemsStatus, string in_loc, Int32 in_age_frm, Int32 in_age_to);

        //subodana 2016-06-08
        [OperationContract]
        DataTable GetLoc_from_Hierachy_withOpteam(string com, string channel, string subChannel, string area, string region, string zone, string pc_code, string opteam);

        //Wimal
        [OperationContract]
        DataTable getpurchaseOrderSummery(string _com, string _loc, string _purType, string _supplier, string _itemcode, string _cat1, string _cat2, DateTime _fromDate, DateTime _toDate, string doctype, string docno);

        //Wimal
        [OperationContract]
        DataTable getsostatus(string _com, string _loc, string _supplier, string _itemcode, string _cat1, string _cat2, DateTime _fromDate, DateTime _toDate, string doctype, string docno);

        [OperationContract]
        DataTable getpurchaseOrder_grnpening(string _com, string _loc, string _supplier, string _itemcode, string _cat1, string _cat2, DateTime _fromDate, DateTime _toDate, string doctype, string docno);

        //Wimal
        [OperationContract]
        DataTable getpurchaseOrderPrint_HDR(string _com, string _loc);

        //Wimal
        [OperationContract]
        DataTable pendingDelivery(DateTime _asAtDate, string _User, string _brand, string _model, string _itemcode, string _itemcat1, string _itemcat2, string _itemcat3, string _agetp, decimal _age, string _com, string _pc, string _latestcost);

        //Wimal
        [OperationContract]
        DataTable getGITReport_Current(DateTime _asatDate, string _com, string _toloc, string _fromloc, string _itemcode, string _brand, string _model, string _cat1, string _cat2, string _cat3, string _cat4, string _cat5);
        //Lakshika 2016-09-21
        [OperationContract]
        DataTable GetGitSerialDetails(string _com, string _docNo, int _itemline, int _batchline);

        //Sanjeewa
        [OperationContract]
        string getGITReport_Asat(DateTime _asat, string _com, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string in_Itemcat4, string in_Itemcat5, string _user, string _othloc, string _allcom, out string _err);
        
        [OperationContract]
        string getGITReport_ASAT_Recon(DateTime _asat, string _com, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string in_Itemcat4, string in_Itemcat5, string _user, string _othloc, out string _err);

        [OperationContract]
        DataTable getGITReport_ASAT1(DateTime _asat, string _com, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string in_Itemcat4, string in_Itemcat5, string _user, string _othloc, string _allcom);
        
        //Wimal
        [OperationContract]
        DataTable getGITReport_ASAT(string _com, string _toloc, string _fromloc, string _supplier, string _itemcode, string _cat1, string _cat2, DateTime _fromDate, DateTime _toDate, string doctype, string docno);

        [OperationContract]
        DataTable get_liabilityReport(string _com, string entryNo);

        [OperationContract]
        DataTable getItemCanibalisePrint(string _docno);

        [OperationContract]
        string getSalesRestrictbyUser(string _user);

        [OperationContract]
        DataTable getMRNPrint(string _docno);

        [OperationContract]
        DataTable get_WaraPrint_Main(string _com, string entryNo, string mainItemCode, string mainserial);

        [OperationContract]
        DataTable get_WaraPrint_Sub(string _com, string entryNo, string mainItemCode, string mainserial);

        //Wimal
        [OperationContract]
        DataTable getpurchaseOrder_Print_Dtl(string _com, string _loc);

        //Wimal
        [OperationContract]
        DataTable PrintInvoice_Hdr(string _comcode, string _DocNo);

        //Wimal
        [OperationContract]
        DataTable PrintInvoice_Dtl(string _comcode, string _DocNo);
        
        [OperationContract]
        DataTable PrintInvoice_Tax(string _DocNo);

        //Wimal
        [OperationContract]
        DataTable PrintInvoice_Pay(string _comcode, string _DocNo);

        //Wimal
        [OperationContract]
        DataTable getvalueAddtion(string _comCode, DateTime _fromDate, DateTime _toDate);

        //kapila
        [OperationContract]
        DataTable GetStockBalanceWithSerial_Asat_SCM(string _user, string _channel, string _brand, string _model, string _itemcode, string _Itemstatus,
   string _itemcat1, string _itemcat2, string _itemcat3, Int16 _withcost, DateTime _asatdate, Int16 _serialrep, Int16 _status, string _Loc, string _Com);

        //Sanjeewa
        [OperationContract]
        string GetItemConditionDetail(string _itemcode, string _brand, string _model, string _itemcat1, string _itemcat2, string _itemcat3, string _itemcat4, string _itemcat5,
            string _Itemstatus, string _Com, string _condtp, string _user, string _itemcond, string _allroute, string _route, out string _err);

        //Sanjeewa
        [OperationContract]
        string Get_Receipt_Sett_Det(DateTime _from, DateTime _to, string _com, string _user, out string _err);

        //kapila
        [OperationContract]
        DataTable GetStockBalanceWithCost_SCM(string _user, string _channel, string _brand, string _model, string _itemcode, string _Itemstatus,
  string _itemcat1, string _itemcat2, string _itemcat3, Int16 _withcost, DateTime _asatdate, Int16 _serialrep, Int16 _status, string _Loc, string _Com);
        //kapila
        [OperationContract]
        DataTable get_ref_rpt_para(string _pty_tp, string _pty_cd);

        //kapila
        [OperationContract]
        DataTable AgeReport_SCM(DateTime in_AsatDate, string in_user_id, string in_Company, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_cat4, string in_cat5, string in_model, string in_itemsStatus, string in_loc);

        //kapila
        [OperationContract]
        DataTable GetSerialBalance_Curr_SCM(string _com, string _loc, string _item, string _itemstatus, string _withRCC);

        //kapila
        [OperationContract]
        DataTable GetStockBalanceCurrent_SCM(string _user, string _channel, string _brand, string _model, string _itemcode, string _Itemstatus,
    string _itemcat1, string _itemcat2, string _itemcat3, string _itemcat4, string _itemcat5, Int16 _withcost, Int16 _serialrep, string _com, string _loc, string _withRCC, string _withJob, string _withGIT);

        //kapila
        [OperationContract]
        DataTable PrintMovementAuditTrialReport(string _com, string _loc, DateTime in_FromDate, DateTime in_ToDate, string _doctp, string _subdoctp, string _direct, string in_item, string in_Brand, string in_model, string in_cat1, string in_cat2, string in_cat3, string in_cat4, string in_cat5, Int32 _isSer, string _othloc, string _docno, Int32 _appstus);

        //kapila
        [OperationContract]
        DataTable PrintContainerVolReport(string _com, string _loc, DateTime in_FromDate, DateTime in_ToDate, string _country, string _agent, string _port);

        //kapila
        [OperationContract]
        DataTable PrintTotalImportsReport(string _com, string _loc, DateTime in_FromDate, DateTime in_ToDate, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_cat4, string in_cat5, string in_model, string _supp, string _agent);

        [OperationContract]
        int ProcessMessages(out string outMsg);

        [OperationContract]
        int ProcessPriority(out string outMsg);

        //Tharaka 2015-02-17
        [OperationContract]
        List<Service_Pending_Jobs> GetPendingJobSCMREPP(String level, String empCOde, String com, String chnl, String loc);

        //Tharaka 2015-02-20
        [OperationContract]
        List<String> GetPendingJObDistinctEMPs(String level, String com, String chnl, String loc);

        //Tharaka 2015-02-20
        [OperationContract]
        MsgInformation GET_MST_MSG_INFOBASE_BY_EMP(String EMP, String com, String loc, String DocType);

        //Tharaka 2015-02-20
        [OperationContract]
        MST_ScvEmpCate GET_SCV_EMPCATE(String com, String Chanl, String CATE);

        //Tharaka 2015-02-21
        [OperationContract]
        Int32 SAVE_MST_MSG_INFOBASE(MsgInformation oItem);

        //Sanjeewa 2016-10-12
        [OperationContract]
        Int32 Insert_SelectedPB(string _pb, string _pbdtl, string _com, string _user);

        //Sanjeewa 2016-10-12
        [OperationContract]
        DataTable get_SelectedPB(string _com, string _user);
        
        //Sanjeewa 2016-10-12
        [OperationContract]
        Int32 Delete_SelectedPB(string _com, string _user);
        

        //Sanjeewa 2016-07-20
        [OperationContract]
        Int32 SaveReportErrorLog(string _erropt, string _errform, string _error, string _user);


        //Tharaka 2015-02-20
        [OperationContract]
        SystemUser GetSystemuserbyEMP(String EMP);

        //Tharaka 2015-02-20
        [OperationContract]
        int SendEMail(string _recipientEmailAddress, string _subject, string _message, string attachment, out String err, string bcc);

        //Tharaka 2015-02-20
        [OperationContract]
        int SendSMS(OutSMS smsOut, out String err);

        //Tharaka 2015-02-123
        [OperationContract]
        int UPDATE_MSGINFOBASE_MODDT(MsgInformation oItem);

        //Tharaka 2015-02-23
        [OperationContract]
        List<COM_CHNL_LOC> GET_DISTINCT_COM_CHN_LOC();


        ////written by Nadeeka 2012-07-04
        [OperationContract]
        DataTable DeliveredSalesWithSerial(DateTime in_FromDate, DateTime in_ToDate, string in_user_id, string in_Company, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_model, string in_Profit);

        //Sanjeewa 2015-11-07
        [OperationContract]
        DataTable getLCDetailsBankwise(DateTime _fromDate, DateTime _toDate, string _com, string _bank, string _lcno, string _type, string _all, string _user);

        //Sanjeewa 2015-11-07
        [OperationContract]
        DataTable getCostingSheetDetails(string _blno, string _costtype);

        //Sanjeewa 2015-11-07
        [OperationContract]
        DataTable getCostingSheetSumDetails(string _blno, string _costtype);

        //Sanjeewa 2016-02-24
        [OperationContract]
        DataTable getShipmentScheduleDetails(DateTime _fromDate, DateTime _toDate, string _com, string _sino, string _user);

        //Sanjeewa 2016-06-28
        [OperationContract]
        DataTable getExcessStockDetails(string in_item, string in_Brand, string in_model, string in_cat1, string in_cat2, string in_cat3, string in_cat4, string in_cat5, string in_com, string in_loc, string in_tp, string in_user);

        //Sanjeewa 2016-06-16
        [OperationContract]
        string getSalesOrderDetails(DateTime _fromDate, DateTime _toDate, string _com, string _otherloc, string _exec, string _user, string _customer, out string _err);

        //Dilshan 2017-10-16
        [OperationContract]
        string getSalesOrderSummery(DateTime _fromDate, DateTime _toDate, string _com, string _otherloc, string _exec, string _user, string _customer, out string _err);

        //Sanjeewa 2016-06-19
        [OperationContract]
        string getIntercompanySalesDetails(DateTime _fromDate, DateTime _toDate, string _com, string _user, out string _err);
        
        //Sanjeewa 2016-06-22
        [OperationContract]
        string GetCanibaliseStockReport(DateTime _fromDate, DateTime _toDate, string _itemclassif, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _User, string _StockType, string _Com, string _pb, string _pblvl, out string _err);

        //Sanjeewa 2016-06-19
        [OperationContract]
        string getIntercompanyReversalDetails(DateTime _fromDate, DateTime _toDate, string _com, string _user, out string _err);

        //Sanjeewa 2017-01-31
        [OperationContract]
        string IntertransferDtlReport(DateTime _fromdate, DateTime _todate, string in_Company, string _fromloc, string _toloc, string in_user_id, string _docno, string _route, string _status, out string _err);
        
        //Sanjeewa 2016-06-16 //Edited By Dulaj 2019/Jan/09
        [OperationContract]
        string getCostInformationSummaryDetails(DateTime _fromDate, DateTime _toDate, string _com, string _user,string cat01,string cat02,string cat03,string cat04,string cat05,string items,string models,string brand, out string _err);

        //Sanjeewa 2016-06-16
        [OperationContract]
        string getAODReconciliationDetails(DateTime _fromDate, DateTime _toDate, string _com, string _fromadmin, string _toadmin, string _user, out string _err);

        //Wimal 25/10/2016
        [OperationContract]
        string getAODInOutDetails(DateTime _fromDate, DateTime _toDate, string _com, string _fromadmin, string _toadmin, string _fromloc, string _toloc, string _in_aodin, string _in_aodout, string _in_deltype, string _user, out string _err);

        //Wimal 12/08/2016
        [OperationContract]
        string getdialyWHStock(string _itemcode, string _itemcat1, string _itemcat2, string _itemcat3, string _itemModel, string _itembrand, string _brandMGR, string _user, out string _err);
        
        //Sanjeewa 2016-05-30
        [OperationContract]
        DataTable PickPlanReport(DateTime _fromdate, DateTime _todate, string in_Company, string _loc, string in_user_id, string _docno, string _route, string _reqtp, int timeSelect);
        
        //Sanjeewa 2016-07-26
        [OperationContract]
        DataTable MRNPRintReport(string _docno);

        //Sanjeewa 2016-02-24
        [OperationContract]
        DataTable getShipmentScheduleContainerDetails(DateTime _fromDate, DateTime _toDate, string _com, string _sino, string _user);

        //Sanjeewa 2016-12-27
        [OperationContract]
        DataTable getContainerDetailsBySI(string _sino, string _user);
        
        //Sanjeewa 2015-11-07
        [OperationContract]
        DataTable getOrderStatusDetails(DateTime _fromDate, DateTime _toDate, string _com, string _user, string _supplier, string _cat1, string _cat2, string _cat3, string _item, string _brand, string _model, string _bldone);

        //Sanjeewa 2016-08-11
        [OperationContract]
        DataTable getProfitabilityDetails(DateTime _fromDate, DateTime _toDate, string _com, decimal _buyrt, decimal _forcastrt, decimal _costrt, decimal _markup,
              decimal _asschg, string _sino, string _entryno, decimal _overhd, string _pb, string _pblvl, string _user);
        
        //Sanjeewa 22-12-2015
        [OperationContract]
        string getBondBalanceDetails(DateTime _asatDate, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _Stktype, string _Com, string _User, out string _err);

        [OperationContract]
        string getBondBalanceDetails1(string _com, string _user, Boolean _isOldRep, out string _err);

        //Sanjeewa 14-07-2016
        [OperationContract]
        string getBMSalesDetails(DateTime _FromDate, DateTime _ToDate, string _invdate, string _com, string _user, DataTable _col, DataTable _row, DataTable _val, out string _err);

        //Wimal 04/11/2016
        [OperationContract]
        string getSalesCount(DateTime _FromDate, DateTime _ToDate, string _invdate, string _com, string _user, DataTable _col, DataTable _row, DataTable _val, out string _err);

        //Wimal 04/11/2016
        [OperationContract]
        string getHpaccountCount(DateTime _FromDate, DateTime _ToDate, string _invdate, string _com, string _user, DataTable _col, DataTable _row, DataTable _val, out string _err);

        //Tharaka 2015-08-12
        [OperationContract]
        List<Service_Pending_Jobs> GetPendingJobSCMREPPByLoc(String level, String empCOde, String com, String chnl, String loc);

        //written by Sanjeewa 2013-10-05
        [OperationContract]
        string GetCutomerDetails(DateTime in_FromDate, DateTime in_ToDate, DateTime in_asatDate, string in_user_id, Int16 in_NoOfMonths, string _com, out string _err);

        //written by Nadeeka 2014-08-27
        [OperationContract]
        string GetCutomerDetails_ReduceBal(DateTime in_FromDate, DateTime in_ToDate, DateTime in_asatDate, string in_user_id, Int16 in_NoOfMonths, string _com, out string _err);

        //Lakshan 2016/01/04 - Sanjeewa 2016-05-27 - Lakshika 2016/10/18
        [OperationContract]
        string GetItemWiseGpExcel(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor,
            int _freeissue, string _itemclasif, string _brndmgr, string _Group, bool withReversal, int _rplitm, DateTime _fromDate2, DateTime _toDate2, Int16 _intercom, out string _err);
        
        //Sanjeewa 2016-11-03
        [OperationContract]
        string GetItemWiseGpExcelRecon(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor,
            int _freeissue, string _itemclasif, string _brndmgr, string _Group, bool withReversal, int _rplitm, DateTime _fromDate2, DateTime _toDate2, Int16 _intercom, string _fromadmin, string _toadmin, out string _err);
        
        [OperationContract]
        DataTable GetItemWiseGp(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor,
            int _freeissue, string _itemclasif, string _brndmgr, string _Group, bool withReversal, int _rplitm, DateTime _fromDate2, DateTime _toDate2,Int16 _intercom);

        //Sanjeewa 2016-12-05
        [OperationContract]
        DataTable GetItemWiseGp_Rpl(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor,
            int _freeissue, string _itemclasif, string _brndmgr, string _Group, bool withReversal, int _rplitm, DateTime _fromDate2, DateTime _toDate2, Int16 _intercom);
        
        //Sanjeewa 2016-06-05 
        [OperationContract]
        string GetItemwiseWarehouseMovement(DateTime _fromDate, DateTime _toDate, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _User, string _StockType, string _Com, int _direction, out string _err);

        //Sanjeewa 2016-06-05 
        [OperationContract]
        string getValuationDetails(DateTime _fromtDate, DateTime _totDate, string _ItemClasif, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _Stktype, string _Group, string _CostOrValue, string _Com, string _User, out string _err);

        [OperationContract]
        string getValuationDetails_ARL(DateTime _fromtDate, DateTime _totDate, string _ItemClasif, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _Stktype, string _Group, string _CostOrValue, string _Com, string _User, out string _err);
        
        [OperationContract]
        DataTable getValuationDetails_Cr(DateTime _fromtDate, DateTime _totDate, string _ItemClasif, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _Stktype, string _Group, string _CostOrValue, string _Com, string _User);

        //Sanjeewa 2016-06-08 
        [OperationContract]
        string getDispatchAppDetails(DateTime _fromtDate, DateTime _totDate, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _Com, string _User, out string _err);

        //Sanjeewa 2016-06-09 
        [OperationContract]
        string LOPOCostDetailsReport(DateTime _fromDate, DateTime _toDate, string _com, string _user, string _pb, string _pblvl, out string _err);


        //Lakshan 2016/01/05
        [OperationContract]
        string GetSalesValuation(DateTime in_fdate, DateTime in_tdate, string in_clasif, string in_itemcode, string in_brand, string in_model,
           string in_itemcat1, string in_itemcat2, string in_itemcat3, string in_itemcat4, string in_itemcat5,
               string in_stktype, string in_com, string in_pc, string in_user, string _Group, out string _err);
        //hasith 25/01/2015
        [OperationContract]
        DataTable GetCreditnoteDetails(DateTime _fromdate, DateTime _todate, string _com, string _pc);
        
        //Sanjeewa 19/02/2016
        [OperationContract]
        DataTable getExchangeCreditNoteDetails(string _com, string _pc, DateTime _fromdate, DateTime _todate, string _user);

        //Sanjeewa 30/03/2016
        [OperationContract]
        DataTable getCurrentAgeDetails(string _com, string _loc, string _itemcat1, string _itemcat2, string _itemcat3, string _itemcat4, string _itemcat5,
            string _itemcode, string _brand, string _model, string _supplier, string _user, string _brndMan);

        //Randima 04/Oct/2016
        [OperationContract]
        string getCurrentComAgeDetails(string _com, List<string> _loclist, string _itemcat1, string _itemcat2, string _itemcode, string _brand, string _model, string _brndMan, string _user);


        [OperationContract]
        string getCurrentComAgeDetails_Serials(string _com, List<string> _loclist, string _itemcat1, string _itemcat2, string _itemcode, string _brand, string _model, string _brndMan, string _user);

        //Wimal 09/11/2016
        [OperationContract]
        DataTable getCurrentComAgeDetails_crystal(string _com, string _loc, string _itemcat1, string _itemcat2, string _itemcode, string _brand, string _model, string _brndMan, string _user, string _allloc, string _option);
        
        //hasith 26/01/2015
        [OperationContract]
        DataTable GetGVDetails(DateTime _fromdate, DateTime _todate, string _com, string _pc);
       
        [OperationContract]
        string getConsignmentStockWithValue(string _com, List<string> _loclist, string _itemcat1, string _itemcat2, string _itemcode, string _brand, string _model, string _brndMan, string _user, bool IsAsat,DateTime _AsatDate);

        //Sanjeewa 2017-03-04
        [OperationContract]
        string GetSipmentStatusDetails(DateTime _fromDate, DateTime _toDate, string _com, string _item, string _blNo, string _entryno, string _user,string _cat1, string _cat2, string _cat3, string _model, string _brand, out string _err);

        //Sanjeewa 2017-03-04
        [OperationContract]
        string GetPhysicalStockVerification(string _mainjobno, string _jobno, string _user, out string _err);

        //Tharaka 2016-01-28
        [OperationContract]
        List<MST_SCV_ADHOC_MAIL> GET_SCV_ADHOC_MAIL();

        //Tharaka 2016-01-28
        [OperationContract]
        List<Service_Pending_Jobs> GET_ALL_PENDINGJOS_FOR_CHL(string com, string chl);

        //written by Sanjeewa 2016-01-02
        [OperationContract]
        string GetDetailsOfCollectionDetails(string _Jobno, Int16 _Export, string _Com, string _User, out string _err);


        //Tharaka 2016-01-29
        [OperationContract]
        int SendEMail_HTML(string _recipientEmailAddress, string _subject, string _message, string attachment, out String err, string bcc);

        //subodana 2016-04-20
        [OperationContract]
        DataTable getItemSerials(string doc);

        //subodana 2016-05-10
        [OperationContract]
        DataTable getpurchaseOrderPrint_HDR_new(string _com, string _doc);

        //Randima 2016-07-06
        [OperationContract]
        DataTable GetLocationSearch(string _compCode, string _searchCatergory, string _searchText);

        //Randima 2016-07-06
        [OperationContract]
        DataTable GetCompanySearch(string _searchCatergory, string _searchText);

        //Randima 2016-07-07
        [OperationContract]
        Int32 UpdateLocationIP(MasterLocation _loc);

        //Lakshika 2016-Aug-04
        [OperationContract]
        DataTable GetEntryReportDetails(DateTime _fromDate, DateTime _toDate, string _entrType, string _cusdecNo);
        
        //Lakshika 2016-Aug-11
        [OperationContract]
        DataTable DispatchReqSummaryReport(DateTime _fromdate, DateTime _todate, string in_Company, string _loc, string _other_loc, string in_user_id, string _route_det, string status, DateTime frm, DateTime to, int printMark, int ExDteWise);

        //Lakshika 2016-Aug-13
        [OperationContract]
        DataTable DispatchRequestDtlReport(DateTime _fromdate, DateTime _todate, string in_Company, string _loc, string _other_loc, string in_user_id, string _route_det, string status, DateTime frm, DateTime to, int printMark, int ExDteWise, out string rep_status);

        //Lakshika 2016-Aug-18
        [OperationContract]
        DataTable ItemDispatchDtlReport(DateTime _fromdate, DateTime _todate, string in_user_id);

        //Lakshika 2016-Aug-23
        [OperationContract]
        DataTable GetShippingDetailReport(string in_company, string in_doc_no);


        //Lakshika 2016-Aug-23
        [OperationContract]
        DataTable GetCostDetailsShipInvReport(string in_doc_no);

        //Lakshika 2016/09/20
        [OperationContract]
        string GetSaleDetailsInvoiceMain(DateTime _fromtDate, DateTime _totDate, string _Com, string _User, out string _err);
        
        //Lakshika 2019/09/23
        //[OperationContract]
        //DataTable GetGpDetailsForReconcilation(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor, int _freeissue, string _itemclasif, string _brndmgr, string _Group, out string _err);

        [OperationContract]
        List<DataTable> GetItemWiseGpExcel2(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor, int _freeissue, string _itemclasif, string _brndmgr, string _Group, string AdminTMList, string delAdminTMList);


        //Lakshika 2016/09/23
        [OperationContract]
        string GetGP_ReconciExcelReport(DataTable _gpDetaails, string _com, string _op_code, string _User, out string _err);

         //Lakshika 2016/09/23
        [OperationContract]
        string GetStockAdjustmentSummary(DateTime _fromtDate, DateTime _totDate, string _Com, string _adminTeam, string _User, out string _err);

        //Lakshika 2016-09-28
        [OperationContract]
        string GetGIT_ReconcilationByGIT_AsAt(DateTime _asat, string _com, string in_Itemcode, string in_Brand, string in_Model, string in_Itemcat1,
            string in_Itemcat2, string in_Itemcat3, string in_Itemcat4, string in_Itemcat5, string _user, string _othloc, out string _err);

        //Lakshika 2016-09-30
        [OperationContract]
        string GetAOD_ReconcilationDetails(DateTime _fromDate, DateTime _toDate, string _com, string _loc, string _admin_tm, string _channel, string _user, out string _err);


        // RUKSHAN 2016-09-25s
        [OperationContract]
        DataTable GET_SALESREVERSALDETAILS_REP(string com, DateTime _from, DateTime _to, string _sbu, string _pc, string _cus, string _itm,
            string _brand, string _model);

         //Lakshika 2016-10-03
        [OperationContract]
        string GetSaleRepDetailsCatWise(DateTime _fromtDate, DateTime _totDate, string _Com, string _User, string groupCat, out string _err);

        //Lakshika 2016-10-07
        [OperationContract]
        string GetAgeOfRevertReportDetails(DateTime _fromdate, DateTime _todate, string in_Company, string _brand, string _cat1, string _cat2, string _itemCode, string _user, out string _err);

        //Lakshika 2016-10-11
        [OperationContract]
        string GetDeliveredSalesDetailsExcel1(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor, int _freeissue, string rep_type, int currencyType, int revOrSaleOrAll, out string _err);

        [OperationContract]
        string GetTotalSaleswithInvType(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor, int _freeissue, int currencyType, int revOrSaleOrAll, string _color, string _size, out string _err);

        [OperationContract]
        string GetDeliveredSalesDetailsExcel1_old(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor, int _freeissue, string rep_type, int currencyType, int revOrSaleOrAll, out string _err);

         //Nuwan 2016.10.17
        [OperationContract]
        MST_ITM getItemDetail(string itmcd);

        //subodana 2016-10-18
        [OperationContract]
         string ExportExcel2007(string _com, string _user, DataTable _dt1, out string _err);

        //subodana 2016-12-10
        [OperationContract]
        string ExportExcel2007WithHDR(string _com, string _user, DataTable TitleData, DataTable _dt1, out string _err, string WithColHeader = "Y");


        //Lakshika 2016/10/18
        [OperationContract]
        List<DataTable> GetGP_AnalysisDetails(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor, int _freeissue, string _itemclasif, string _brndmgr, string _Group, bool withReversal, out string _err);

        //subodana 2016-11-21
        [OperationContract]
        DataTable Barcodeserdata(string com, string doc);

        //Sanjeewa 2017-03-17
        [OperationContract]
        DataTable GetAuditExecSummary1(string _mainjobno, string _jobno, string _com, string _user, out Dictionary<string, DataTable> dict, out DataTable _diffserial);
        
         //RUKSHAN 2016-11-23
        [OperationContract]
        DataTable GETCUSTOMERENTRY(string com, DateTime _from, DateTime _to);


        //WIMAL 2016/12/16
        [OperationContract]
        DataTable GETSELL_OUT_REPORT(DateTime _fromdate, DateTime _todate, string _user_id, string _Brand, string _Model, string _Itemcode, string _Itemcat1, string _Itemcat2, string _Itemcat3, string _Promotor, string _Customer, string _SalesExc);

        //Add by Akila 2016/1/24
        [OperationContract]
        int UpdateMstMyAbans(string key_Code);
        //kapila
        [OperationContract]
        String ProcessMyAbansReport(String _user, string _com, DateTime _fdate, DateTime _tdate, Int32 _opt, out String _err);
       //Wimal
        [OperationContract]
        String ProcessDayEndProcessReport(String _user, string _com, DateTime _fdate, DateTime _tdate, Int32 _opt, out String _err);
        //kapila
        [OperationContract]
        String ProcessECDVoucherDetReport(String _user, string _com, DateTime _fdate, DateTime _tdate, out String _err);
        //kapila
        [OperationContract]
        String ProcessHPIntroCommReport(String _user, string _com, string _pc, DateTime _fdate, DateTime _tdate, out String _err);
        //kapila
        [OperationContract]
        string RegisProcessReport(string _user, string _com, string _chnl, DateTime _fdate, DateTime _tdate, Int32 _opt, string _fincomp, out String _err);
        //kapila
        [OperationContract]
        DataTable PrintCompletedHPAgreement(string _com, DateTime _from, DateTime _to, string _pc, string _agrUser, string _user);
        //Written By Sanjeewa on 23/07/2013
        [OperationContract]
        DataTable GetPriceDetails(string _circular, string _promocode, string _pricetp, string _pb, string _pblevel, DateTime _dtfrom, DateTime _dtto, DateTime _dtasat,
            string _customer, string _itemcode, string _brand, string _model, string _itemcat1, string _itemcat2, string _itemcat3, string _user, string _reptp);                
        //Written By Sanjeewa on 11/07/2013
        [OperationContract]
        DataTable GetCompanyTable();
        //Written By Sanjeewa on 11/07/2013
        [OperationContract]
        DataTable GetEmployeeTable(string _company);
        //Written By Sanjeewa on 11/07/2013
        [OperationContract]
        DataTable GetEliteCommDetails(string _circular, Int16 _year, Int16 _month, string _user);        
        //Written By Sanjeewa on 15/07/2013
        [OperationContract]
        DataTable GetEliteCommDefDetails(string _circular, string _user);
        //Written By Sanjeewa on 15/07/2013
        [OperationContract]
        DataTable GetEliteCommDtlDetails(string _circular, string _user);
        //Written By Sanjeewa on 15/07/2013
        [OperationContract]
        DataTable GetEliteCommPartyDetails(string _circular, string _user);
        //Written By Sanjeewa on 11/07/2013
        [OperationContract]
        DataTable GetEliteCommAdditionalDetails(string _circular);
        //written by Nadeeka 2013-07-18
        [OperationContract]
        DataTable GetVehicleRegistrationReport(DateTime _date, string _com, string _user, string _invType, string _itemCode, DateTime _fromdate, DateTime _todate);
        //written by Sanjeewa 2014-10-27
        [OperationContract]
        DataTable GetVehicleRegUnreg_Report(DateTime _fdate, DateTime _tdate, string _regtp, string _user, string _invtp);
        //written by Sanjeewa 2014-10-27
        [OperationContract]
        string GetVehicleRegUnreg1_Report(DateTime _fdate, DateTime _tdate, string _regtp, string _com, string _user, out string _err);
        //written by Dilshan 2017-10-26
        [OperationContract]
        string GetPriceDiscrepancyReport(DateTime _fdate, DateTime _tdate, string _regtp, string _com, string _user, out string _err);
        
        [OperationContract]
        string getCoverNoteDetailsReport(DateTime _fdate, DateTime _tdate, string _com, string _user, out string _err);
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable ProcessHPReceiptPrint(string _refNo);
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable ProcessHPReceiptPrintPayment(string _refNo);
        //written by Nadeeka 2013-11-20
        [OperationContract]
        DataTable ProcessHPReceiptList(DateTime _fromDate, DateTime _toDate, string _com, string _pc);
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable ProcessHPReceiptPrintPayMode(string _refNo);
        //written by Sanjeewa 2014-05-28
        [OperationContract]
        string GetDiscountPromoDetails(DateTime in_FromDate, DateTime in_ToDate, string in_Circular, string in_equal, string _com, string in_user_id, out string _err);
        //written by Sanjeewa 2015-01-12
        [OperationContract]
        string GetSalesInfoDetails(DateTime _FromDate, DateTime _ToDate, string _invtp, string _invsubtp, string _com, string _user_id, out string _err);
        //written by Sanjeewa 2016-05-30
        [OperationContract]
        string GetPriceDefinitionDetails(string _com, string _user_id, out string _err);
        //written by Sanjeewa 2015-09-04
        [OperationContract]
        string GetInsurancePremiumDetails(DateTime _FromDate, DateTime _ToDate, string _com, string _user_id, out string _err);
        //written by Sanjeewa 2015-01-12
        [OperationContract]
        string GetRevertInfoDetails(DateTime _FromDate, DateTime _ToDate, string _invtp, string _com, string _user_id, out string _err);
        //written by Nadeeka 2013-09-06
        [OperationContract]
        DataTable GetReduceBalInterest(DateTime in_FromDate, DateTime in_ToDate, string _com, string _pc, Int16 _issum, Int16 _isSch);
        //written by Nadeeka 2013-06-03
        [OperationContract]
        DataTable ProcessHPMultipleAccounts(string _user, DateTime _asatDate, string _com, string _scheme, string _cusId, Double _cusAccBal, string _pc);
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetCancelledDocuemnt(DateTime _fdate, DateTime _tdate, string _userid, string _Location, string _Company, string docType);
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable GetManualDocuemnt(DateTime _fdate, DateTime _tdate, string _userid, string _Location, string _Company, string docType);
        //written by Sanjeewa 18-03-2014
        [OperationContract]
        DataTable GetAddIncentiveDetails(DateTime _fdate, DateTime _tdate, string _Com, string _Pc, string _Scheme, string _userid);        
        //written by Sanjeewa 19-03-2014
        [OperationContract]
        DataTable GetTrimAccountDetails(DateTime _fdate, DateTime _tdate, string _Com, string _Pc, string _Scheme, string _userid);        
        //written by Sanjeewa 21-03-2014
        [OperationContract]
        DataTable GetCustomerAckLogDetails(DateTime _fdate, DateTime _tdate, string _Com, string _Pc, string _userid, string _accno);
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable getReturnChequeSettlemtsDetails(DateTime _FromDate, DateTime _ToDate, string _user_id, string _company, string _Pc);
        //written by Nadeeka 2015-02-25
        [OperationContract]
        DataTable GetAddIntroduceCommision(DateTime _fdate, DateTime _tdate, string _Com, string _Pc, string _Scheme, string _userid, string _promoter);
        //written by Nadeeka 2013-05-16
        [OperationContract]
        DataTable UnusedReceiptReport(DateTime _fromDate, DateTime _toDate, string _user, string _com, string _pc);
        //written by Nadeeka 2013-05-16
        [OperationContract]
        DataTable SummaryOfWeekly(DateTime _fromDate, DateTime _toDate, string _com, string _pc);
        //written by Nadeeka 2013-04-08
        [OperationContract]
        DataTable ProcessManagerCommission(string _profit, string _user, string _epfNo, string _comCode, DateTime _fromDate, DateTime _toDate);
        //written by Nadeeka 2013-04-08
        [OperationContract]
        DataTable ProcessManagerCollBonus(string _profit, string _user, string _epfNo, string _comCode, DateTime _fromDate, DateTime _toDate);
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable ProcessClosedAccounts(string _user, DateTime _fromDate, DateTime _toDate, string _company, string _pc);
        //written by Nadeeka 2012-12-13
        [OperationContract]
        DataTable ProcessVehicleInsuranceArrears(string _user, string _comCode, DateTime _fromDate, DateTime _toDate, string _pc);
        //written by Nadeeka 2013-04-10
        [OperationContract]
        DataTable ProcessVehicleInsurance(string _user, string _comCode, DateTime _fromDate, DateTime _toDate);        
        //written by Sanjeewa 2014-06-06
        [OperationContract]
        DataTable getHPPureCreationDetails(DateTime _fromDate, DateTime _toDate, string _user, string _comCode, string _Pc);
        //written by Nadeeka 2013-04-16
        [OperationContract]
        DataTable ProcessVehicleInsPay(string _user, string _comCode, DateTime _fromDate, DateTime _toDate);
        //written by Nadeeka 2013-04-16
        [OperationContract]
        DataTable ProcessVehicleInsClaims(string _user, string _comCode, DateTime _fromDate, DateTime _toDate);
        //written by Nadeeka 2015-07-02
        [OperationContract]
        DataTable ProcessVehicleColletion(string _pc, string _comCode, DateTime _fromDate, DateTime _toDate);
        //written by Nadeeka 2013-04-17
        [OperationContract]
        DataTable InternalPaymentVoucher(string _vouNo);
        //written by Nadeeka 2013-01-14
        [OperationContract]
        DataTable HPDebitCreditRep(DateTime _fromDate, DateTime _toDate, string _user, string _company, string _PC);
        //written by Nadeeka 2013-09-06
        [OperationContract]
        DataTable getReturnChequeSettlemts(DateTime _AsAtDate, string _user_id, string _company, string _loc);
        //written by Sanjeewa 2013-05-21
        [OperationContract]
        DataTable GetArrears_ClosingBalance(string _accNo, DateTime _asAtDate);
        //Written By Sanjeewa on 16/10/2013
        [OperationContract]
        DataTable GetPhyCashVerifyMDetails(string _jobno);
        //Written By Sanjeewa on 08/11/2015
        [OperationContract]
        DataTable GetPhyCashVerifyRemDetails(string _com, string _pc, DateTime _fdate, DateTime _tdate);
        //Written By Sanjeewa on 16/10/2013
        [OperationContract]
        DataTable GetPhyCashVerifyDTDetails(string _jobno);
        //Written By Sanjeewa on 16/10/2013
        [OperationContract]
        DataTable GetPhyCashVerifyDNDetails(string _jobno);
        //Written By Sanjeewa on 16/10/2013
        [OperationContract]
        DataTable GetPhyCashVerifyCSDetails(string _jobno);
        //written by Sanjeewa 2013-08-05
        [OperationContract]
        DataTable GetTotal_Arrears(DateTime _asAtDate, string _Scheme, string _user);
        ////written by Sanjeewa 2013-11-20
        //[OperationContract]
        //DataTable GetRec_Age_Analysis(int _Year, int _Month, int _NoOfMonth, string _Com, string _Pc, string _reptp, string _user);     
        //Written by Prabhath on 08/03/2014
        //[OperationContract]
        //string GetRec_Age_Analysis_Excel(int _Year, int _Month, int _NoOfMonth, string _Com, string _reptp, string _user, out string _error);
        //Written by Prabhath on 10 3 2014
        [OperationContract]
        string GetRec_Age_Analysis_New(int _Year, int _Month, int _NoOfMonth, string _Com, string _reptp, string _user, string _order, string _groupintr, bool isSupp,DateTime _todate,  out string _error, out string excle2);
        //written by Sanjeewa 2013-01-03
        [OperationContract]
        DataTable GetAccountLogDetails(string _accNo);
        //written by Sanjeewa 2013-09-16
        [OperationContract]
        DataTable GetReq_App_Details(DateTime _fDate, DateTime _tDate, string _reqtp, string _appStatus, string _user);
        //written by Sanjeewa 2014-01-20
        [OperationContract]
        DataTable Get_Group_Sale_Details(DateTime _fDate, DateTime _tDate, string _user, string _com, string _pc);
        //written by Sanjeewa 2013-10-11
        [OperationContract]
        DataTable GetCommDetails(DateTime _fDate, DateTime _tDate, string _circular, string _user, Int16 _isExport);
        //written by Sanjeewa 2013-09-13
        [OperationContract]
        DataTable GetGracePeriod_Arrears(DateTime _asAtDate, string _user, string _com, string _pc);
        //written by Sanjeewa 2013-01-07
        [OperationContract]
        DataTable GetHPCustomerDetails(string _custId, string _custCode, Int32 _AddType, string _accNo);
        //written by Sanjeewa 2013-12-08
        [OperationContract]
        Boolean GetDiscountDetails(string _Circular, string _User, int _isExport, string _com, out string _err, out string _filePath);
        //written by Sanjeewa 2014-06-10
        [OperationContract]
        Boolean GetDiscountDetails1(string _Circular, string _User, int _isExport, string _com, string _brand, string _model, string _itemcode, string _itemcat1, string _itemcat2, string _itemcat3, DateTime _fromDate, DateTime _toDate, out string _err, out string _filePath);
        //written by Sanjeewa 2013-12-09
        [OperationContract]
        DataTable GetDeliveredSerDetails(string _InvNo, string _Item, string _User, int _isExport, int _isLastPc);
        //written by Sanjeewa 2013-03-28/ Added filter by create date by Nadeeka
        [OperationContract]
        DataTable GetForwardSalesDetailAudit(DateTime _asAtDate, string _User, string _brand, string _model, string _itemcode, string _itemcat1, string _itemcat2, string _itemcat3, string _agetp, decimal _age, string _com, string _pc, string _latestcost);
        //written by Sanjeewa 2014-06-24
        [OperationContract]
        DataTable GetItemRestrictionDetails(string _brand, string _model, string _itemcode, string _itemcat1, string _itemcat2, string _itemcat3, string _com, string _pc);
        //written by Sanjeewa 2013-03-28
        [OperationContract]
        DataTable GetForwardSalesDetails1(DateTime _asAtDate, string _User, string _brand, string _model, string _itemcode, string _itemcat1, string _itemcat2, string _itemcat3, string _agetp, decimal _age, string _com, string _pc);
        //written by Sanjeewa 2013-05-17
        [OperationContract]
        DataTable GetCurrentMonthDueDetails(int _Year, int _Month, string _Scheme, string _User, string _Com, string _Pc);
        //written by Sanjeewa 2013-05-23
        [OperationContract]
        DataTable GetAllDueDetails(int _Year, int _Month, string _Scheme, string _User, string _com, string _pc);
        //written by Sanjeewa 2013-04-10
        [OperationContract]
        DataTable GetLastDayEndLogDetails(DateTime _asAtDate, string _User, string _Com, string _Pc);
        //written by Sanjeewa 2013-04-09
        [OperationContract]
        DataTable GetLastNoSeqDetails(DateTime _asAtDate, string _User, string _DocType, string _Com, string _Loc);
        //written by Sanjeewa 2013-10-25
        [OperationContract]
        DataTable PhyStkBalCollectionHeaderDetails(string _User, string _JoNo);
        //written by Sanjeewa 2013-10-25
        [OperationContract]
        DataTable PhyStkBalCollectionItemDetails(string _User, string _JoNo);
       //Wimal 02/03/2017
        [OperationContract]
        DataTable UserMonitor(string _Com, string _Dept, string _User);
             //written by Sanjeewa 2013-10-29
         [OperationContract]
        DataTable PhyStkBalCollectionRemarkDetails(string _User, string _JoNo, Int16 _line);
        //written by Sanjeewa 2013-10-25
        [OperationContract]
        DataTable PhyStkBalCollectionSerialDetails(string _User, string _JoNo, string _StkCat, string _RepType, string _RepStatus, string _RepFilter);
        //written by Sanjeewa 2013-10-29
        [OperationContract]
        DataTable PhyStkBalCollectionSerialAgeDetails(string _User, string _JoNo);
        [OperationContract]
        DataTable PhyStkBalCommStkDetails(string _com, string _itm);
        //written by Sanjeewa 2013-10-29
        [OperationContract]
        DataTable PhyStkBalFIFODetails(string _User, string _JoNo);
        //written by Sanjeewa 2013-10-30
        [OperationContract]
        DataTable UnconfirmedAODDetails(string _User, DateTime _fromDate, DateTime _toDate);
        //written by Sanjeewa 2013-10-30
        [OperationContract]
        DataTable DFSalesDetails(string _User, DateTime _fromDate, DateTime _toDate);
        //written by Sanjeewa 2013-10-30
        [OperationContract]
        DataTable DFSalesReceiptDetails(string _User, DateTime _fromDate, DateTime _toDate);
        //written by Sanjeewa 2013-10-31
        [OperationContract]
        DataTable DFSalesReceiptCurrDetails(string _User, DateTime _fromDate, DateTime _toDate);
        //written by Sanjeewa 2013-12-30
        [OperationContract]
        DataTable RecDeskSummDetails(string _User, DateTime _fromDate, DateTime _toDate, string _com, string _loc);
        //written by Sanjeewa 2014-11-24
        [OperationContract]
        DataTable ReqAppCurrentStatusDetails(DateTime _fromDate, DateTime _toDate, string _com, string _pc, string _reqtp, string _status, string _User);
        //kapila
        [OperationContract]
        DataTable ReqAppCurrentStatusUserWise(DateTime _fromDate, DateTime _toDate, string _com, string _pc, string _reqtp, string _status, string _User, string _userID);
        //kapila
        [OperationContract]
        DataTable ReqAppDetByReasonReport(DateTime _fromDate, DateTime _toDate, string _com, string _pc, string _reason, string _status, string _User);
        [OperationContract]
        string ReqAppDetByReasonReport_new(DateTime _fromDate, DateTime _toDate, string _com, string _reason, string _User, string _cat, string _tp, out string _err);
        //kapila
        [OperationContract]
        DataTable GetHPCreditNoteDetails(string _accNo);
        //written by Sanjeewa 2014-10-10
        [OperationContract]
        DataTable DFInvBalwithPriceDetails(string _User, DateTime _asatDate, string _com, string _loc);
        //written by Sanjeewa 2014-10-13
        [OperationContract]
        Boolean TrPayTpDefDetails(string _com, string _circular, string _promocode, string _paytp, string _isasatdate, DateTime _fDate, DateTime _tDate, DateTime _asatDate, string _User, out string _err, out string _filePath);
        //written by Sanjeewa 2014-11-13
        [OperationContract]
        Boolean GetRegistraion_ExcelDetails(DateTime _from_date, DateTime _to_date, string _rpt_mode, string _usr, string _com, out string _err, out string _filePath);
        //written by Sanjeewa 2013-12-27
        [OperationContract]
        DataTable ProfitCenterDetails(string _User, int _isExport);
        //written by Sanjeewa 2013-12-26
        [OperationContract]
        DataTable DFSalesReceiptCurrTrDetails(string _User, DateTime _fromDate, DateTime _toDate);
        //written by Sanjeewa 2013-10-25
        [OperationContract]
        DataTable PhyStkBalCollectionSerialMatchDetails(string _User, string _JoNo, string _StkCat, string _RepStatus, string _SerMisMatch);
        //written by Sanjeewa 2013-05-13
        [OperationContract]
        DataTable GetSchemeGuaranter(string _Circular);
        //written by Sanjeewa 2013-05-13
        [OperationContract]
        DataTable GetSchemeOtherCharge(string _Circular);
        //written by Sanjeewa 2013-05-13
        [OperationContract]
        DataTable GetSchemeSerCharge(string _Circular);
        //written by Sanjeewa 2013-05-13
        [OperationContract]
        DataTable GetSchemeProofDoc(string _Circular);
        //written by Sanjeewa 2013-05-13
        [OperationContract]
        DataTable GetSchemeReSchedule(string _Circular);
        //written by Sanjeewa 2013-05-13
        [OperationContract]
        DataTable GetSchemeSchedule(string _Circular);
        //written by Sanjeewa 2013-05-13
        [OperationContract]
        DataTable GetSchemeCommission(string _Circular);
        //written by Sanjeewa 2013-05-13
        [OperationContract]
        DataTable GetSchemeDetail(string _Circular);
        //written by Sanjeewa 2013-05-13
        [OperationContract]
        DataTable GetSchemeType(string _Circular);
        //written by Sanjeewa 2013-05-13
        [OperationContract]
        DataTable GetSchemeCategory(string _Circular);        
        //written by Nadeeka 2014-02-28
        [OperationContract]
        DataTable GetSalesFiguresDetailsWithTax(DateTime _fromDate, DateTime _toDate, string _User, string _Pc, string _Com);
        //written by Nadeeka 2015-04-27
        [OperationContract]
        DataTable GetServiceGP(string _com, string _pc, DateTime _fdate, DateTime _tdate);
        //written by Sanjeewa 2015-09-21
        [OperationContract]
        DataTable GetServiceGPDetail(string _com, string _pc, DateTime _fdate, DateTime _tdate, string _doctp);
        //written by Nadeeka 2015-04-27
        [OperationContract]
        DataTable GetQuotationDet(string _com, string _type, string _customer, string _item, string _cat1, string _cat2, string _cat3, string _brand, String _model, DateTime _fdate, DateTime _tdate);
        //written by Nadeeka 2015-04-27
        [OperationContract]
        DataTable GetServiceStandBy(string _com, string _loc, DateTime _fdate, DateTime _tdate);
        //written by Sanjeewa 2013-11-07
        [OperationContract]
        DataTable GetCrBalanceDetails(DateTime _fromDate, DateTime _toDate, string _User);
        //written by Sanjeewa 2013-10-22
        [OperationContract]
        DataTable GetHPGroupCommissionDetails(DateTime _fromDate, DateTime _toDate, string _User, string _DocType);
        //written by Sanjeewa 2013-10-03
        [OperationContract]
        DataTable GetTotalRevenueDetails(DateTime _fromDate, DateTime _toDate, string _User, string _com, string _pc);
        //written by Sanjeewa 2014-12-22
        [OperationContract]
        DataTable GetColBonusReconDetails(DateTime _fromDate, DateTime _toDate, string _com, string _pc, string _User, string _type);
        //written by Sanjeewa 2013-06-08
        [OperationContract]
        DataTable GetAgreementPrintingHeaderDetails(string _accno);
        //written by Sanjeewa 2013-06-08
        [OperationContract]
        DataTable GetAgreementPrintingItemDetails(string _accno);
        //written by Sanjeewa 2013-06-08
        [OperationContract]
        DataTable GetAgreementPrintingCustomerDetails(string _accno);
        //written by Sanjeewa 2014-08-29
        [OperationContract]
        DataTable GetDepositBankDefDetails(string _com, string _pc);
        //written by Sanjeewa 2014-09-23
        [OperationContract]
        DataTable GetVehicleRegistrationDetails(string _refno);
        //written by Sanjeewa 2014-09-23
        [OperationContract]
        DataTable GetBrandDetails(string _brandcd);
        //written by Sanjeewa 2014-09-23
        [OperationContract]
        DataTable GetCountryDetails(string _countrycd);
        //written by Sanjeewa 2013-05-31
        [OperationContract]
        DataTable GetExecwiseSalesInvDetails(DateTime _fromDate, DateTime _toDate, string _User, decimal _discRate, Int16 _discTp, Int16 _isdel, String _ExType, String _com, String _pc, String _appby);
        //written by Sanjeewa 2013-02-25
        [OperationContract]
        DataTable GetTransferedAccountDetails(DateTime _fromDate, DateTime _toDate, string _User, string _Com, string _Pc);
        //written by Sanjeewa 2013-02-27
        [OperationContract]
        DataTable GetJobDetails(DateTime _fromDate, DateTime _toDate, string _User, string _Channel, Int16 _Status);
        //written by Sanjeewa 2013-10-09
        [OperationContract]
        DataTable GetPaymodeDetails(DateTime _fromDate, DateTime _toDate, string _Paymode, string _User, int _Export, string _pc, string _com);
        //written by Sanjeewa 2014-01-18
        [OperationContract]
        DataTable GetPaymodeAmendDetails(DateTime _fromDate, DateTime _toDate, string _Paymode, string _User, int _Export);
        //written by Sanjeewa 2013-08-15
        [OperationContract]
        DataTable GetJobSummaryDetails(DateTime _fromDate, DateTime _toDate, string _User, string _Pc, string _Com);
        //written by Sanjeewa 2013-03-07
        [OperationContract]
        DataTable GetStampDutyDetails(DateTime _fromDate, DateTime _toDate, string _User);
        //written by Sanjeewa 2013-03-08
        [OperationContract]
        DataTable GetSVATDetails(DateTime _fromDate, DateTime _toDate, string _User, string _doctype, string _docsubtype);
        //written by Sanjeewa 2013-02-27
        [OperationContract]
        DataTable GetJobScheduleDetails(DateTime _fromDate, DateTime _toDate, string _User);
        //written by Sanjeewa 2013-12-19
        [OperationContract]
        DataTable GetAgreementStatementDetails(DateTime _asAtDate, string _Com, string _Pc, string _User);
        //written by Sanjeewa 2018-05-03
        [OperationContract]
        DataTable GetAgreementStatementDetailsAudit(DateTime _asAtDate, string _Com, string _Pc, string _User);
        //written by Sanjeewa 2013-12-21
        [OperationContract]
        DataTable GetAgreementStatementDtlDetails(DateTime _asAtDate, string _Com, string _Pc, string _User);
        //written by Sanjeewa 2014-10-22
        [OperationContract]
        DataTable GetAgreementPendingDetails(DateTime _asAtDate, string _Com, string _Pc, string _User);
        //written by Sanjeewa 2013-12-20
        [OperationContract]
        DataTable GetAgreementCheckDetails(DateTime _asAtDate, string _Com, string _Pc, string _User);
        //written by Sanjeewa 2013-02-19
        [OperationContract]
        DataTable GetNoOfActAccountDetails(DateTime _fromDate, DateTime _toDate, string _User, string _com, string _pc);
        //written by Sanjeewa 2013-02-13
        [OperationContract]
        DataTable GetRevertNReleaseDetails(DateTime _fromDate, DateTime _toDate, string _com, string _PCenter, string _User);
        //written by Nadeeka 2014-04-25
        [OperationContract]
        DataTable GetRevertReleaseOther(DateTime _fromDate, DateTime _toDate, string _com, string _PCenter, string _User, Int16 _Opt);
        [OperationContract]
        string GetRevertStatus(DateTime _fromDate, DateTime _toDate, string _com, string _PCenter, string _User, out string _err);
        //written by Sanjeewa 2014-01-23
        [OperationContract]
        DataTable GetAgreementChecklistDetails(DateTime _fromDate, DateTime _toDate, string _com, string _pc, string _User);
        //written by Sanjeewa 2013-04-01
        [OperationContract]
        DataTable GetPOSDetailDetails(DateTime _fromDate, DateTime _toDate, string _User);
        //written by Sanjeewa 2013-01-31
        [OperationContract]
        DataTable GetDeliveredSalesDetails(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Pc, string _Com, string _Promotor, int _freeissue, int currencyType, int _reversal, string _color, string _size, string _country = null, string _province = null, string _district = null, string _city = null); //updated by akila 2018/03/16
        //written by Sanjeewa 2016-11-09
        [OperationContract]
        string GetDeliveredSalesDetailsExcel(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor, int _freeissue, int currencyType, int revOrSaleOrAll, out string _err, string _country = null, string _province = null, string _district = null, string _city = null); //updated by akila 2018/04/02        
        [OperationContract]
        DataTable GetStockSalesDetails(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Pc, string _Com, string _Promotor, int _freeissue, string _Supplier);
        //written by Sanjeewa 2015-01-21
        [OperationContract]
        DataTable GetDeliveredSalesInsuDetails(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Pc, string _Com, string _Promotor, int _freeissue);
        [OperationContract]
        string GetDeliveredSalesInsuDetails1(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor, int _freeissue, out string _err);
        //written by Sanjeewa 2014-12-12
        [OperationContract]
        DataTable GetComparisonofDeliveredSalesDetails(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Pc, string _Com, string _Promotor, int _freeissue, string _IsMonth, string _IsDelivered);
        //written by Sanjeewa 2014-08-22
        [OperationContract]
        DataTable GetTotalSales8020Details(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Pc, string _Com, int _Margin, string _type);
        //written by Sanjeewa 2014-08-22
        [OperationContract]
        DataTable GetTotalSales8020Summary(int _Margin, string _type, string _User);
        //written by Sanjeewa 2014-08-22
        [OperationContract]
        Int32 DeleteCustomerAnalysisRep(string _User);
        //written by Sanjeewa 2013-01-31
        [OperationContract]
        DataTable GetDeliveredSalesDetailsAuditReport(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Pc, string _Com, int _noDays);
        //written by Sanjeewa 2014-02-10
        [OperationContract]
        DataTable GetCustomerInfoDetails(DateTime _fromDate, DateTime _toDate, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _User, string _Pc, string _Com, Int16 _locno);
        //written by Sanjeewa 2016-02-23
        [OperationContract]
        DataTable GetDeliveredSalesGRNCostDetails(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Supplier, string _PONo, string _com, string _pc, int _isExport);
        //written by Sanjeewa 2016-02-23
        [OperationContract]
        DataTable GetDeliveredSalesGRNDetails(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Supplier, string _PONo, string _com, string _pc, int _isExport);
        //written by Sanjeewa 2013-06-12
        [OperationContract]
        DataTable GetQuotationPrintDetails(string _QuoNo);
        //written by Sanjeewa 2013-08-10
        [OperationContract]
        DataTable GetQuotationWarrantyPrintDetails(string _Company);
        //written by Sanjeewa 2013-06-19
        [OperationContract]
        DataTable GetEliteCommissionDetails(DateTime _fromDate, DateTime _toDate, string _User);
        //written by Sanjeewa 2013-06-24
        [OperationContract]
        DataTable GetBalanceWarrantyClaimCRNoteDetails(DateTime _fromDate, DateTime _toDate, string _Dealer, string _User);
        //written by Sanjeewa 2013-07-01
        [OperationContract]
        DataTable GetSalesPromoAchievementDetails(DateTime _fromDate, DateTime _toDate, string _circularno, string _scheme, string _User);
        //written by Sanjeewa 2013-06-20
        [OperationContract]
        DataTable GetNotRegVehDetails(string _User, DateTime _fromDate, DateTime _toDate);

        //Wimal 2016/12/24
        [OperationContract]
        string GetFitchExcel(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor,
            int _freeissue, string _itemclasif, string _brndmgr, string _Group, bool withReversal, int _rplitm, DateTime _fromDate2, DateTime _toDate2, Int16 _intercom, out string _err);

        //Wimal 2016/12/24
        [OperationContract]
        string GetFitch_CatBrndExcel(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _User, string _RepType, string _StockType, string _InvNo, string _Com, string _Promotor,
            int _freeissue, string _itemclasif, string _brndmgr, string _Group, bool withReversal, int _rplitm, DateTime _fromDate2, DateTime _toDate2, Int16 _intercom, out string _err);

        //Lakshan 27 Dec 2016
        [OperationContract]
        ImportsCostItem GET_PREV_ITEM_COST_FOR_COSTSHEET(string _item, string _comp);
        
        //Chamal 30 Dec 2016
        [OperationContract]
        DataTable GetClosingBalanceWithValueProcess(string _com, string _loc, DateTime _fromDt, DateTime _toDt, int _procId, string _sessionId, string _user);

        //Lakshan 11 Jan 2017
        [OperationContract]
        void SendMailReservationRequestApprove(InventoryRequest _invReq,string type);

        //Lakshan 11 Jan 2017
        [OperationContract]
        void SendMailReservationApprove(string _com, string _docNo);

        [OperationContract]
        string PSIReport(string _user, string _com, DateTime _fromdate, DateTime _todate, string _model, string _brand, string _cat1, string _cat2, string _cat3, int _intcom, string _MNG, out string _err,string Adminteam);

        //Lakshan 17 Jan 2017
        [OperationContract]
        List<mst_itm_com_reorder> GetReorderDataWithItemCost(string _itmCd);
        
        //Nuwan 2017.01.17
        [OperationContract]
        bool valuationProcess(DateTime asAtDate, string company, string location,bool mnthEnd,string userid,string adminteam,out string error,bool unComPath=false);
        //Lakshan 26 Jan 2017
        [OperationContract]
        List<mst_itm_com_reorder> GetItemMasterCostDataByItem(string _itmCd);
         //Lakshan 26 Jan 2017
        [OperationContract]
        void GenarateAodOutMailAndSMS(string _com, string _docNo);
        //Dilshan 2018/09/03
        [OperationContract]
        void GenarateDOSMS(string _com, string _docNo, string _cusNo, string _invNo);
        //Dilshan 2018/09/04
        [OperationContract]
        void GenarateSOSMS(string _com, string _docNo, string _cusNo, string _invNo);
        //Dilshan 2018/09/04
        [OperationContract]
        void GenaratePaymentSMS(string _com, string _docNo, string _cusNo, string _invNo, string _invamt, string _pc);
        //Lakshan 26 Jan 2017
        [OperationContract]
        void GenarateAodInwardMailAndSMS(string _com, string _docNo);
       
        //Lakshan 26 Jan 2017
        [OperationContract]
        DataTable MakeGridBalWithoutStatus(DataTable dt);

        // ISURU 2017/03/06
        [OperationContract]
        DataTable GetCollectionDetails(string company, DateTime fromdate, DateTime todate, string saletype, string customer, string invoiceno, string userid);

        //Udaya 28/03/2017
        [OperationContract]
        string GetCommisionProcessReport(DateTime _fromDate, DateTime _toDate, string _Com, string _User, string circularCode, string comName, out string _err);

        // ISURU 2017/03/28
        [OperationContract]
        string GetCommisionDetailExcl(DateTime _fromDate, DateTime _toDate, string _Com, string _User, string circularCode, string userid,string pcstring, out string _err);
        //Lakshan 29 Mar 2017
         [OperationContract]
        DataTable GET_DISTRIBUTION_DOC_DATA_FOR_PRINT(string _reqNo);
        //Nuwan 2017.04.22
         [OperationContract]
        DataTable getAutoPrintAodDocumentDtls(string aodNo,string doctp);
         //Nuwan 2017.04.25
         [OperationContract]
         DataTable GetPendingPrintDocumentsNew(string lp,string loc,string doctp=null);
         //Nuwan 2017.04.25
         [OperationContract]
         DataTable GetPendingPrintDocuments(string lp);
         //Nuwan 2017.04.25
         [OperationContract]
         Int32 updatePrintedDocument(string docno);

         //Isuru 2017/04/26
         [OperationContract]
         ITM_PLU getItemDetailwithPLU(string itmcd);

         //Isuru 2017/05/14
         [OperationContract]
         string ExReversalReport(string _com, string _user, DataTable _dt1, out string _err);

         // ISURU 2017/05/22
         [OperationContract]
         DataTable getreturnchequedet(string company, DateTime fromdate, DateTime todate, string customer, string cheque, string accountcode, string grntxt, string userID);

        // Udaya 31/05/2017
        [OperationContract]
         string GetSlowMovingInventry(string _Com, string _User, string comName, DataTable _dtResults, out string _err);
        //Subodana 2017/06/15
        [OperationContract]
        DataTable Barcodepludocdata(string com, string doc);
        //Udaya 10.08.2017 Collect data to MRN Approved Summary report
        [OperationContract]
        DataTable MRN_Approved_Report(DateTime _fromdate, DateTime _todate, string in_Company, string _loc, string in_user_id, string _rpt_Type, string _route, int _allRoute);
        //Udaya 14.08.2017 Daily Entry Details (Bond)
        [OperationContract]
        string Bond_DaillyEntryDetails(DateTime _fromDate, DateTime _toDate, string _com, string _user, string _comName, string rptType, string comCode, out string _err);
        //Udaya Update Item vice max serial no for Barcode print
        [OperationContract]
        Int32 getItemMaxSerial(mst_itm_max_serNo serDtl, out string _err);
        //Udaya Collect Item vice max serial no for Barcode print
        [OperationContract]
        mst_itm_max_serNo ItemSerialNo(string itmCd, string comCd);
        //Udaya 02.10.2017 Collect data to Courier Dailly Summary report
        [OperationContract]
        DataTable CourierDaillySummary(DateTime _fromdate, DateTime _todate, string in_Company, string _loc, string in_user_id, string _FromNo, string _ToNo, string _courCom);
        // Dilshan 2017/10/03
        [OperationContract]
        string GetCollectionBonusExcl(DateTime _fromDate, DateTime _toDate, string _Com, string _User, string circularCode, string userid, string pcstring, out string _err);

        //Tharindu 2017/11/08
        [OperationContract]
      //  string GetSalesWithCurrentInv_Bal(DateTime _fromDate, DateTime _toDate, string _cat1,string _cat2,string _cat3,string _cat4,string _cat5,string _brand,string _model,string _itemcode,string _Com, string _User,string _pc,string _loc,out string _err);

       // string GetSalesWithCurrentInv_Bal_new(DateTime _fromDate, DateTime _toDate, string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, string _brand, string _model, string _itemcode, string _pc, string _loc,string _isasatdate,string _user,string _channel,string _itmstatus,Int32 withcost,Int32 withserial,Int32 withdiscount,Int32 status,string isasatdate,string pricebook,string pblevel,out string _err);
        string GetSalesWithCurrentInv_Bal_new(DateTime _fromDate, DateTime _toDate, string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, string _brand, string _model, string _itemcode, string _pc, string _loc, string _isasatdate, string _user, string _channel, string _itmstatus, Int32 withcost, Int32 withserial, Int32 withdiscount, Int32 status, string isasatdate,string pricebook,string pblevel, out string _err);

        //Tharindu 2017/11/11
        [OperationContract]
        string GetSGRANDetails_Execl_Details(string _user, DateTime _fromDate, DateTime _toDate, string _brand, string _model, string _itemcode, string _itemcat1, string _itemcat2, string _itemcat3, string _doctype, string _reptpdesc, out string _err, string _Com,string root);
       
        //Tharindu 2017/11/15
        [OperationContract]
          DataTable GetDeliveredSalesGRNDetails_Execl (DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Supplier, string _PONo, string _com, string _pc, int _isExport, out string _err);
                                                                                                                                            
        //Nuwan 2017.11.13
        [OperationContract]
        void SendMailReservationCancel(List<INR_RES_LOG> updateResLogList, string type);
        //Nuwan 2017.11.13
        [OperationContract]
        void SendMailReservationUpdate(string resno, string com,string expdt);
        //Tharanga 2017/12/11
        [OperationContract]
        DataTable Get_gp_report(string _com, string _pc, DateTime _frmDate, DateTime _todate, string _itm_cat1, string _itm_cat2, string _exec_cd, string _cust_cd, string _itm_cd, string _brand, string _model, string _itm_cat3, string _itm_cat4, string _itm_cat5);
          //NUWAN 2017 DEC 14
        [OperationContract]
        void SendMailPOApprove(PurchaseOrder PURHDR, List<PurchaseOrderDetail> PURDET, string type);

        //Tharindu 2017-12-20
        [OperationContract]
        string GetTotalSalesDetailsExecl(DateTime _fromDate, DateTime _toDate, string _Cust, string _Exec, string _DocTp, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _PCenter, string _User, string _RepType, string _StockType, string _InvNo, string _Pc, string _Com, string _Promotor, int _freeissue, int currencyType, int _reversal, string _color, string _size, out string _err, string _country = null, string _province = null, string _district = null, string _city = null); //updated by akila 2018/04/02

        //Tharindu 2018-01-22
        [OperationContract]
        DataTable GetCreditCardPayDetails(string _Com, string _Pc, DateTime _FromDate, DateTime _ToDate, string _Bank, string _Mid, string _ReceiptNo, string _Userid, out string _err);
        
        //Tharindu 2018-01-27
        [OperationContract]
        DataTable ImportScheduleReport_New(DateTime _fromdate, DateTime _todate, string in_Company, string _adminteam, string in_user_id);

        [OperationContract]
        DataTable ImportScheduleGRNDtl_New(DateTime _fromdate, DateTime _todate, string in_Company, string _adminteam, string in_user_id);
        //sube
        [OperationContract]
         DataTable ImportScheduleSun(DateTime _fromdate, DateTime _todate, string in_Company, string _adminteam, string in_user_id);
        //Add by lakshan 20Feb2018
        [OperationContract]
        decimal GetMrnShowroomStockValue(string _com, string _loc);
        //Add by lakshan 20Feb2018
        [OperationContract]
        decimal GetApprovedMrnShowroomStockValue(string _com, string _loc, out string _err);

         //Add by lakshan 20Feb2018
        [OperationContract]
        decimal GetMrnItemsStockValue(string _com, string _loc, List<InventoryRequestItem> _reqItmList, out string _err);
        //subodana 2018-03-06
        [OperationContract]
        void SendBransMails(string _excelpath, string sino, string com);

        //tHARINDU 2018-03-06
        [OperationContract]
        DataTable getSalesOrderSummaryDetails(DateTime _fromDate, DateTime _toDate, string _com, string _otherloc, string _exec, string _user, string _customer, out string _err);

        //Wimal 24/03/2018 
        [OperationContract]
        DataTable Get_FinancialDocDetails(DateTime _fromdate, DateTime _todate, string in_Company, string _adminteam, string in_user_id, string in_lc, string in_pi, string in_bank, string in_acc, int IsSummary,int IsExpireDate , int IsInsurancePolicyDate ,int IsFinanceDocDate);

        //Wimal 27/03/2018 
        [OperationContract]
        DataTable Get_GetAdjDetailsWDocCat(DateTime _fromdate, DateTime _todate, string _Company, string _LocCd, string in_user_id);

        //Tharindu 2018-03-22
        [OperationContract]
        DataTable GetCurrentAvailabilityBL(string _Com, DateTime _FromDate, DateTime _ToDate, string _sino, string _itemcode, string _model, string brand, string category,string category2,string category3,string brandmgr, string supplier, string userid);

        //Tharindu 2018-03-22
        [OperationContract]
        DataTable GetShipmentDetails(string userid);

       // Tharindu 2018-03-22
        [OperationContract]
        DataTable getChargeSheet(string _Com, string _Loc, string _Jobno, string _User);
        
        //subodana 2018-04-19
        [OperationContract]
        int ItemDispatchDetailAutoMail(InvReportPara _objRepoPara);
        //Sanjeewa 2018-03-13
        [OperationContract]
        DataTable getMigrationInvoiceDetails();
         
        //Tharindu 2018-04-24
        [OperationContract]
        string GetAgeAnalysisDebotrsExecl(DateTime p_from, DateTime p_to, string p_com, string p_pc, string p_user, string p_cust, string p_tp, out string _err);

        [OperationContract]
        DataTable GetAgeAnalysisDebotrs(DateTime p_from, DateTime p_to, string p_com, string p_pc, string p_user, string p_cust, string p_tp, string p_excel);

        [OperationContract]
        DataTable getLatestCost(string _com, string _item, string _itemstatus);

        [OperationContract]
        string getTrialCalDetails(out string _err);

        //Tharindu 2018-01-22
        [OperationContract]
        DataTable GetCatWiseTradingGPReport(string com,DateTime stdt,DateTime endDt, string chnl, string cate,string pc,string userid,string itmcd,out string _err);
        //Tharanga 2018-05/22
        [OperationContract]
        DataTable GET_provisioning_AGE(string in_com, string in_loc, string in_user, DateTime in_date, string age);
         //Tharanga 2018-05/22
        [OperationContract]
        DataTable GET_STATUS_WISE_AGEING(string in_com, string in_loc, string in_user, DateTime in_date);
        //Tharanga 2018-05/28
       // [OperationContract]
      //  DataTable GET_disposal_summary(string in_com, DateTime from_date, DateTime to_date,string _loc);
      //   [OperationContract]
      //  DataTable GET_disposal_summary(string in_com, DateTime from_date, DateTime to_date);
        //Dulaj 2018/May/31
        [OperationContract]
        DataTable GET_disposal_summary(string in_com, DateTime from_date, DateTime to_date,string _loc);
        //Tharindu 2018-06-06
        [OperationContract]
        DataTable GetFixAssetLocation_NEW(string _com, string _pc);
        //Tharindu 2018-06-06
        [OperationContract]
        DataTable GetFixAssetLocation_Other(string _com, string _pc);
        [OperationContract]
        DataTable CheckValidItmInFixAsset(string _itm);

        [OperationContract]
        DataTable FixedAssetBalDetailsNew(string _User, string _loc);
        
        //Tharindu 2018-06-06
        [OperationContract]
        DataTable GetFixAssetLoc_NEW(string _com, string _pc);

        //Tharindu 2018-06-06
        //[OperationContract]
        //DataTable CheckValidItmInFixAsset(string _itmcd);

        //Tharindu 2018-06-06
        [OperationContract]
        DataTable GetBinlocationFixasset(string _com, string _pc);

        //Wimal 21/06/2018
        [OperationContract]
        DataTable GetFIXA_dtl_WithDepreciation(string _com, string _loc, DateTime in_FromDate, DateTime in_ToDate, string in_item, string in_Brand, string in_cat1, string in_cat2, string in_cat3, string in_cat4, string in_cat5, string in_model);
        
        //Wimal 21/06/2018
        [OperationContract]
        DataTable GetKIT_Component(string _KIT_itm);
        
       [OperationContract]
        DataTable getCostingSheetParms(string _blno);
       [OperationContract]
       DataTable getCostingParms(string _blno);

       //Tharindu 2018-06-06
       [OperationContract]
       DataTable CheckFixAssetlocAvailability(string _com, string loc);

        // Tharindu 2018-07-03
       [OperationContract]  
       string GetSalesWithInv_Bal_loc_wise(DateTime _fromDate, DateTime _toDate, string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, string _brand, string _model, string _itemcode, string _pc, string _loc, string _isasatdate, string _user, string _channel, string _itmstatus, Int32 withcost, Int32 withserial, Int32 withdiscount, Int32 status, string isasatdate, out string _err);

       // Tharindu 2018-07-03
       [OperationContract]
       DataTable GetLocationCount(string _pcom, string _pid);
       // Thaaranga 2018/08/18
       [OperationContract]  
       DataTable RankAccountTransfferingReport(DateTime fromdate, DateTime todate, string loc, string com);
      
        //Wimal 28/08/2018 Abstract Sales Stock Bal report
       [OperationContract]
       string Get_ABT_sales_W_StkBal(string _com, string _whLoc, DateTime _fromDate, DateTime _toDate, DateTime _asatDate
            , string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, string _model, string _brand, string _itmcode, string _itmStatus,
            string _userID, out string _err);

       //Wimal 11/Sep/2018 Bill Collection Summery/Detail report
       [OperationContract]
       DataTable Get_Bill_Collection(DateTime _fromdate, DateTime _todate, string in_Company, string _pc, string in_user_id);

       //Wimal 17/09/2018
       [OperationContract]
       string Get_rejectAccBalance(string _com, string _loc, DateTime _asatDate, string _userID, out string _err);

       //Wimal 27/Sep/2018
       [OperationContract]       DataTable Get_ReducingBalance(string _accNo, DateTime _asAtDate);

       //Wimal 19/Sep/2018
       [OperationContract]
       string getSMIDetailsExcel(DateTime _fromDate, DateTime _toDate, string _Com, string _User,
            string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, string _brand, string _model, string _itemcode,
            decimal FromSQty, decimal ToSQty, out string _err);

       //Wimal 
       [OperationContract]
       string get_InvRoll_Fwd(string _com, string _whLoc, DateTime _fromDate, DateTime _toDate, DateTime _asatDate
        , string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, string _model, string _brand, string _itmcode, string _itmStatus,
        string _userID, out string _err);

       //Wimal 
       [OperationContract]
        string get_Inv_WSelPrice(string _com, string _comDesc, string _whLoc, DateTime _fromDate, DateTime _toDate, DateTime _asatDate
        , string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, string _model, string _brand, string _itmcode, string _itmStatus,
        string _userID, string _pb, string _pb_lvl, out string _err);
       
       //Udesh 15/Oct/2018
       [OperationContract]
       string getPurchaseOrderNewSummeryExcel(string _com, string _loc, string _purType, string _supplier, string _itemcode, string _cat1, string _cat2, DateTime _fromDate, DateTime _toDate, string doctype, string docno, string userid, out string _err);
       
        //Wimal 19/Sep/2018
       [OperationContract]
       string getpurchaseOrderSummeryExcel(string _com, string _loc, string _purType, string _supplier, string _itemcode, string _cat1, string _cat2, DateTime _fromDate, DateTime _toDate, string doctype, string docno, string userid, out string _err);


       //Wimal 03/OCT/2018
       [OperationContract]
       string Get_AgeMonitoringDtl(string _com, DateTime _asatDate, DateTime _currentDate, string _cat1, string _cat2, string _cat3, string _model, string _brand, string _itmcode, DataTable _ageslot, string _userID, bool isComAge, bool isAllPC, out string _err);

       //Wimal 03/OCT/2018
       [OperationContract]
       string get_voucherDetails(string _com, string _comDesc, string _whLoc, DateTime _fromDate, DateTime _toDate, DateTime _asatDate
   , string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, string _model, string _brand, string _itmcode, string _itmStatus,
   string _userID,string _redeemType, out string _err);


       //Wimal 31/OCT/2018
       [OperationContract]
       string get_BinCardReport(string _com, string _comDesc, string _whLoc, DateTime _fromDate, DateTime _toDate
   , string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, string _model, string _brand, string _itmcode, string _itmStatus,
   string _userID, string _reportType, out string _err);
       //tharanga 17/OCT/2018
       [OperationContract]
       string HP_intrest_report(string _com, string _loc, string userid, DateTime in_FromDate, DateTime in_ToDate, string _pc, Int16 _issum, Int16 _isSch, string _rtp_heading, out string _err);
       //tharanga 17/OCT/2018
       //[OperationContract]
       //string GetRCV_AGE_CURRRNT(string userid, string _com, string _pc, string _loc, string _rtp_heading, DateTime _fromdt, DateTime _todate, out string _err);

       //Wimal @ 05/Nov/2018
       [OperationContract]
       string get_commsionComparioson(string _com, string _comDesc, DateTime _fromDate, DateTime _toDate,
string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, string _model, string _brand, string _itmcode, string _saleType,
string _userID, out string _err);

       //Wimal @ 05/Nov/2018
       [OperationContract]
       DataTable getAgesummery(DateTime _from, DateTime _to, DateTime _pre, string _com, string _loc, string _itemcat1, string _itemcat2, string _itemcat3, string _itemcat4, string _itemcat5,
string _itemcode, string _brand, string _model, string _supplier, string _user, string _brndMan, Int16 _isComAge, string _pb = "ABANS", string _pblvl = "A");
       
        //Wimal @ 12/Nov/2018
       [OperationContract]
       DataTable SearchCustomer(string p_mbe_com, string p_mbe_cd);

       //Dulaj 2018/Oct/26
       [OperationContract]
       DataTable Get_Custom_Assmnt_Det(string com, DateTime from, DateTime to, string type);

       //Dulaj 2018/Dec/10
       [OperationContract]
       DataTable Get_Shipment_Data(string com, DateTime from, DateTime to, string doc);
        //Dulaj 2018/Dec/11
       [OperationContract]
       DataTable Get_Clr_usr(string com, DateTime from, DateTime to, string doc);

////       //Wimal @ 26/Nov/2018
       [OperationContract]
       string getExpireItem(string _com, string _comDesc, DateTime _fromDate, DateTime _toDate,
string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, string _model, string _brand, string _itmcode,
string _userID, out string _err);
       //Dulaj 2018/Dec/11
       [OperationContract]
       DataTable Get_Clr_Com(string com, DateTime from, DateTime to, string doc);
       //Dulaj 2018/Dec/11
       [OperationContract]
       DataTable Get_Clr_ShippingLine(string com, DateTime from, DateTime to, string doc);
       //tHARANGA 2018/Dec/14
       [OperationContract]
       string get_account_det(DataTable _accDet, string _userID, string com, out string _err);
       //Wimal 11/OCT/2018
       [OperationContract]
       string get_depreciation_Summery(string _com, string _loc, DateTime _asatDate, string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, string _brand, string _model, string _itemcode, string _user_id, out string _err);

       //Wimal 11/OCT/2018
       [OperationContract]
       string get_depreciation_PPNote(string _com, string _loc, DateTime _FDate, DateTime _TDate, string _cat1, string _cat2, string _cat3, string _cat4, string _cat5, string _brand, string _model, string _itemcode, string _user_id, out string _err);

       //Wimal 11/OCT/2018
       [OperationContract]
        string get_depreciation_Disposal(string _com, string _loc, DateTime _FDate, DateTime _TDate, string _user_id, out string _err);

       [OperationContract]
       Boolean blockMRN(string _com);

       [OperationContract]
       Boolean blockAOD(string _com);

       [OperationContract]
       Boolean blockAODIN(string _com);

       [OperationContract]
       Boolean blockGRAN(string _com);

       [OperationContract]
       Boolean blockINTR(string _com);

        [OperationContract]
       DataTable updateLastnoSeq(DateTime _date, string _type, string _com, string _loc);

        [OperationContract]
        DataTable updateLastnoSeqpages(DateTime _date, string _type, string _com, string _loc);
        //Dilshan 08/02/2019
        [OperationContract]
        string getSalesRegistor(DateTime _fromtDate, DateTime _totDate, string _ItemClasif, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _Stktype, string _Group, string _CostOrValue, string _Com, string _User, out string _err);
        //Dilshan on 12/Feb/2019
        [OperationContract]
        string getpurchaseRegister(string _com, string _loc, string _purType, string _supplier, string _itemcode, string _cat1, string _cat2, DateTime _fromDate, DateTime _toDate, string doctype, string docno, string userid, out string _err);
        //Dilshan 15/02/2019
        [OperationContract]
        string getExciseSchedules(DateTime _fromtDate, DateTime _totDate, string _ItemClasif, string _ItemCode, string _Brand, string _Model, string _Cat1, string _Cat2, string _Cat3, string _Cat4, string _Cat5, string _Stktype, string _Group, string _CostOrValue, string _Com, string _User, out string _err);
        
    }
}