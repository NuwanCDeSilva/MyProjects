using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using FF.BusinessObjects;
using FF.BusinessObjects.InventoryNew;
using FF.BusinessObjects.General;
using System.Reflection;
using Neodynamic.SDK.Web;

namespace FastForward.SCMWeb.View.Reports.Inventory
{
    public class clsInventory : Page 
    {
        public FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial _moveAuditTrial = new FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial();
        public FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial_cost _moveAuditTrialCost = new FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial_cost();
        public FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial_ser _moveAuditTrialSer = new FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial_ser();
        public FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial_ser_cost _moveAuditTrialSerCost = new FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial_ser_cost();
        public FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial_sum _moveAuditTrialSum = new FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial_sum();
        public FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial_summary _moveAuditTrialSummary = new FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial_summary();
        public FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial_det _moveAuditTrialDet = new FastForward.SCMWeb.View.Reports.Inventory.Movement_audit_trial_det();

        public FastForward.SCMWeb.View.Reports.Inventory.Stock_Balance _curStkBal = new FastForward.SCMWeb.View.Reports.Inventory.Stock_Balance();
        public FastForward.SCMWeb.View.Reports.Inventory.Stock_BalanceCatWise _curStkBalCat = new FastForward.SCMWeb.View.Reports.Inventory.Stock_BalanceCatWise();

        public FastForward.SCMWeb.View.Reports.Inventory.InventoryStatementsTr3 _invStsTr3 = new FastForward.SCMWeb.View.Reports.Inventory.InventoryStatementsTr3();

        public FastForward.SCMWeb.View.Reports.Inventory.StockAge _stkAge = new FastForward.SCMWeb.View.Reports.Inventory.StockAge();
        public FastForward.SCMWeb.View.Reports.Inventory.Stock_BalanceCost _invBalCst = new FastForward.SCMWeb.View.Reports.Inventory.Stock_BalanceCost();
        public FastForward.SCMWeb.View.Reports.Inventory.Stock_BalanceSerialAs_at _invBalSrlAsat = new FastForward.SCMWeb.View.Reports.Inventory.Stock_BalanceSerialAs_at();
        public FastForward.SCMWeb.View.Reports.Inventory.rptPurchaseorderSummery_Detail _purOrdSumm_dtl = new FastForward.SCMWeb.View.Reports.Inventory.rptPurchaseorderSummery_Detail();
        public FastForward.SCMWeb.View.Reports.Inventory.rptPurchaseorderSummery_Summery _purOrdSumm_summ = new FastForward.SCMWeb.View.Reports.Inventory.rptPurchaseorderSummery_Summery();
        public FastForward.SCMWeb.View.Reports.Inventory.rptPOGRNPending _pendingGRN = new FastForward.SCMWeb.View.Reports.Inventory.rptPOGRNPending();
        public FastForward.SCMWeb.View.Reports.Inventory.rptLocalpurchaseCostDetail _localPurcaseCost = new FastForward.SCMWeb.View.Reports.Inventory.rptLocalpurchaseCostDetail();
        public FastForward.SCMWeb.View.Reports.Inventory.StockBalanceWithSerialAge _serAge = new FastForward.SCMWeb.View.Reports.Inventory.StockBalanceWithSerialAge();
        public FastForward.SCMWeb.View.Reports.Inventory.rptValueAddition _valueAddition = new FastForward.SCMWeb.View.Reports.Inventory.rptValueAddition();
        public FastForward.SCMWeb.View.Reports.Inventory.ItemwiseTransDetList _itmwiseTransList = new FastForward.SCMWeb.View.Reports.Inventory.ItemwiseTransDetList();
        public FastForward.SCMWeb.View.Reports.Inventory.MoveCostDetail _movCostDet = new FastForward.SCMWeb.View.Reports.Inventory.MoveCostDetail();
        public FastForward.SCMWeb.View.Reports.Inventory.SerialMovement _serMove = new FastForward.SCMWeb.View.Reports.Inventory.SerialMovement();
        public FastForward.SCMWeb.View.Reports.Inventory.serial_items _serialItems = new FastForward.SCMWeb.View.Reports.Inventory.serial_items();
        public FastForward.SCMWeb.View.Reports.Inventory.curr_age_report _CurrAge = new FastForward.SCMWeb.View.Reports.Inventory.curr_age_report();
        public FastForward.SCMWeb.View.Reports.Inventory.InventoryStatements _invSts = new FastForward.SCMWeb.View.Reports.Inventory.InventoryStatements();
        public FastForward.SCMWeb.View.Reports.Inventory.StockLedger _stkLedger = new FastForward.SCMWeb.View.Reports.Inventory.StockLedger();
        public FastForward.SCMWeb.View.Reports.Inventory.ConsignmentMovement _consMove = new FastForward.SCMWeb.View.Reports.Inventory.ConsignmentMovement();

        public FastForward.SCMWeb.View.Reports.Inventory.LocwiseItemAge _locAge = new FastForward.SCMWeb.View.Reports.Inventory.LocwiseItemAge();
        public FastForward.SCMWeb.View.Reports.Inventory.CatwiseItemAge _catAge = new FastForward.SCMWeb.View.Reports.Inventory.CatwiseItemAge();
        public FastForward.SCMWeb.View.Reports.Inventory.CatScatwiseItemAge _catScatAge = new FastForward.SCMWeb.View.Reports.Inventory.CatScatwiseItemAge();
        public FastForward.SCMWeb.View.Reports.Inventory.StuswiseItemAge _StusAge = new FastForward.SCMWeb.View.Reports.Inventory.StuswiseItemAge();
        public FastForward.SCMWeb.View.Reports.Inventory.ItmBrndwiseItemAge _ItmBrndAge = new FastForward.SCMWeb.View.Reports.Inventory.ItmBrndwiseItemAge();

        public FastForward.SCMWeb.View.Reports.Inventory.AgeMonitoring _ageMonit = new FastForward.SCMWeb.View.Reports.Inventory.AgeMonitoring();
        public FastForward.SCMWeb.View.Reports.Inventory.abscourier _abscour = new FastForward.SCMWeb.View.Reports.Inventory.abscourier();
        public FastForward.SCMWeb.View.Reports.Inventory.ItemBufferStatus _itmBufStus = new FastForward.SCMWeb.View.Reports.Inventory.ItemBufferStatus();
        public FastForward.SCMWeb.View.Reports.Inventory.ToBondStatus _toBondStus = new FastForward.SCMWeb.View.Reports.Inventory.ToBondStatus();
        public FastForward.SCMWeb.View.Reports.Inventory.ToBondStatus_Old _toBondStusOld = new FastForward.SCMWeb.View.Reports.Inventory.ToBondStatus_Old();
        public FastForward.SCMWeb.View.Reports.Inventory.AllocationDetails _allocDet = new FastForward.SCMWeb.View.Reports.Inventory.AllocationDetails();
        public FastForward.SCMWeb.View.Reports.Inventory.rpt_GLB_Git_Document rptgit = new FastForward.SCMWeb.View.Reports.Inventory.rpt_GLB_Git_Document();
        public FastForward.SCMWeb.View.Reports.Inventory.MRN_Status _mrnStus = new FastForward.SCMWeb.View.Reports.Inventory.MRN_Status();
        public FastForward.SCMWeb.View.Reports.Inventory.ReservationDetail _resDet = new FastForward.SCMWeb.View.Reports.Inventory.ReservationDetail();
        public FastForward.SCMWeb.View.Reports.Inventory.ReservationSummary _resSumm = new FastForward.SCMWeb.View.Reports.Inventory.ReservationSummary();
        public FastForward.SCMWeb.View.Reports.Inventory.rpt_Bond_Balance_Report1 _LiableRep = new FastForward.SCMWeb.View.Reports.Inventory.rpt_Bond_Balance_Report1();
        public FastForward.SCMWeb.View.Reports.Inventory.cr_locationdetails _Locationdet = new FastForward.SCMWeb.View.Reports.Inventory.cr_locationdetails();
        public FastForward.SCMWeb.View.Reports.Inventory.Last_No_Seq_Rep _LastNoSeq = new FastForward.SCMWeb.View.Reports.Inventory.Last_No_Seq_Rep();
        public FastForward.SCMWeb.View.Reports.Inventory.RequistionNote _ReqestionNote = new FastForward.SCMWeb.View.Reports.Inventory.RequistionNote();
        public FastForward.SCMWeb.View.Reports.Inventory.excess_stock_report _Esxcessstk = new FastForward.SCMWeb.View.Reports.Inventory.excess_stock_report();

        public FastForward.SCMWeb.View.Reports.Inventory.item_profile _itemprofile = new FastForward.SCMWeb.View.Reports.Inventory.item_profile();
        //Prints
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs _outdoc = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full _outdocfull = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full__AEC _outdocfull_AEC = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full__AEC();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_IN_SGL_ _outdocfull_IN_SGL = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_IN_SGL_();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_SGL _outdocfull_OUT_SGL = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_SGL();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_ARL _outdocfull_Arl = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_ARL();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_ARL_EXP_Details _outdocfull_Arl_Exp = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_ARL_EXP_Details();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_AODOUTNEW _outdocfull_auto = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_AODOUTNEW();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_DO _outdocDO = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_DO();
        public FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs _indoc = new FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs();
        public FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full _indocfull = new FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full();
        public FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_GRN _indocfullGRN = new FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_GRN();
        public FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_GRN_ARL _indocfullGRN_ARL = new FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_GRN_ARL();
        public FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_GRN_SGL _indocfullGRN_SGL = new FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_GRN_SGL();
        public FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_GRN_AAL _indocfullGRN_AAL = new FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_GRN_AAL();
        public FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_GRN_ABE _indocfullGRN_ABE = new FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_GRN_ABE();

        public FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_GRN_AOA _indocfullGRN_AOA = new FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_GRN_AOA();

        public FastForward.SCMWeb.View.Reports.Inventory.Rep_Pur_OrderN _PoPrint = new FastForward.SCMWeb.View.Reports.Inventory.Rep_Pur_OrderN();
        public FastForward.SCMWeb.View.Reports.Inventory.Rep_Pur_OrderN_ABL_N _PoPrint_ABL = new FastForward.SCMWeb.View.Reports.Inventory.Rep_Pur_OrderN_ABL_N();
        public FastForward.SCMWeb.View.Reports.Inventory.Rep_Pur_Order_ARL _PoPrint_ARL = new FastForward.SCMWeb.View.Reports.Inventory.Rep_Pur_Order_ARL();
        public FastForward.SCMWeb.View.Reports.Inventory.Rep_Pur_OrderN_ABE _PoPrint_ABE = new FastForward.SCMWeb.View.Reports.Inventory.Rep_Pur_OrderN_ABE();
        public FastForward.SCMWeb.View.Reports.Inventory.Rep_Pur_OrderN_AAL _PoPrint_AAL = new FastForward.SCMWeb.View.Reports.Inventory.Rep_Pur_OrderN_AAL();

        public FastForward.SCMWeb.View.Reports.Inventory.RepWarrantyCard_AOA2 _warraPrint = new FastForward.SCMWeb.View.Reports.Inventory.RepWarrantyCard_AOA2();
        public FastForward.SCMWeb.View.Reports.Inventory.Item_Canibalise_Print _itemcanib = new FastForward.SCMWeb.View.Reports.Inventory.Item_Canibalise_Print();
        public FastForward.SCMWeb.View.Reports.Inventory.MRNPrint _mrn = new FastForward.SCMWeb.View.Reports.Inventory.MRNPrint();
        public FastForward.SCMWeb.View.Reports.Inventory.WarrPrint_abl _warrprint = new FastForward.SCMWeb.View.Reports.Inventory.WarrPrint_abl();
        public FastForward.SCMWeb.View.Reports.Inventory.WarrPrint_SGL _warrprint_SGL = new FastForward.SCMWeb.View.Reports.Inventory.WarrPrint_SGL();
        public FastForward.SCMWeb.View.Reports.Inventory.WarrPrint_mobile _warrprintmobile = new FastForward.SCMWeb.View.Reports.Inventory.WarrPrint_mobile();
        public FastForward.SCMWeb.View.Reports.Inventory.ReservationItems _resitems = new FastForward.SCMWeb.View.Reports.Inventory.ReservationItems();
        public FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Consign _consIn = new FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Consign();
        //
        public FastForward.SCMWeb.View.Reports.Inventory.Itemconditionreport _itemconditionreport = new FastForward.SCMWeb.View.Reports.Inventory.Itemconditionreport();
        public FastForward.SCMWeb.View.Reports.Imports.CusDecEntryReq _cusDecEntry = new FastForward.SCMWeb.View.Reports.Imports.CusDecEntryReq();

        public FastForward.SCMWeb.View.Reports.Inventory.Serial_History _serialHistory = new FastForward.SCMWeb.View.Reports.Inventory.Serial_History();
        public FastForward.SCMWeb.View.Reports.Purchasing.Purchase_Returns_Report _purchaseReturns = new FastForward.SCMWeb.View.Reports.Purchasing.Purchase_Returns_Report();
        public FastForward.SCMWeb.View.Reports.Inventory.PurchaseRequest _PurchaseRequest = new FastForward.SCMWeb.View.Reports.Inventory.PurchaseRequest();
        public FastForward.SCMWeb.View.Reports.Inventory.Statement_Of_Accounts _statementofacc = new FastForward.SCMWeb.View.Reports.Inventory.Statement_Of_Accounts();
        public FastForward.SCMWeb.View.Reports.Purchasing.ItemWiseStationeryRequirements _ItemWiseStationeryRequirements = new FastForward.SCMWeb.View.Reports.Purchasing.ItemWiseStationeryRequirements();
        public FastForward.SCMWeb.View.Reports.Inventory.WarrPrint_abl_new _warrprintnew = new FastForward.SCMWeb.View.Reports.Inventory.WarrPrint_abl_new(); // add by tharanga 2017/08/18
        public FastForward.SCMWeb.View.Reports.Purchasing.SupplierPrices _SupplierPrices = new FastForward.SCMWeb.View.Reports.Purchasing.SupplierPrices();
        public FastForward.SCMWeb.View.Reports.Inventory.AALcourier _aalcour = new FastForward.SCMWeb.View.Reports.Inventory.AALcourier();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_DO_ABE _outdocDO_ABE = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_DO_ABE();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_ABE _outdocfull_AEB = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_ABE();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_AOA_OUTP _outdocfull_AOA = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_AOA_OUTP();       
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_ABE_OUTN _outdocfull_N = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_ABE_OUTN();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_ABE_OUTP _outdocfull_P = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_ABE_OUTP();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_AAL _outdocfull_AAL = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_AAL();
        public FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_ABE_PRN _outdocfull_ABE_PRN = new FastForward.SCMWeb.View.Reports.Inventory.Outward_Docs_Full_ABE_PRN();
        public FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_ADJ_ABE _indocfull_ABE_ADJ = new FastForward.SCMWeb.View.Reports.Inventory.Inward_Docs_Full_ADJ_ABE();
        public FastForward.SCMWeb.View.Reports.Sales.CreditCardPayReceipt _creditcardPaydoc = new FastForward.SCMWeb.View.Reports.Sales.CreditCardPayReceipt();
        public FastForward.SCMWeb.View.Reports.Sales.CreditCardPaymentSummary _creditcardsummarydoc = new FastForward.SCMWeb.View.Reports.Sales.CreditCardPaymentSummary();
        public FastForward.SCMWeb.View.Reports.Inventory.Current_Availability_Against_BL _curr_available_rpt = new FastForward.SCMWeb.View.Reports.Inventory.Current_Availability_Against_BL();
        public FastForward.SCMWeb.View.Reports.Inventory.summarized_age_report _summarized_age_report = new FastForward.SCMWeb.View.Reports.Inventory.summarized_age_report();
        public FastForward.SCMWeb.View.Reports.Sales.CategorywiseTradingGP _cattrading = new FastForward.SCMWeb.View.Reports.Sales.CategorywiseTradingGP();
        public FastForward.SCMWeb.View.Reports.Inventory.FIXA_GrnDepre_details _FIXA_DepreDtl = new FastForward.SCMWeb.View.Reports.Inventory.FIXA_GrnDepre_details();
        public FastForward.SCMWeb.View.Reports.Inventory.KITComponentSetup _KIT_ComSetup = new FastForward.SCMWeb.View.Reports.Inventory.KITComponentSetup();


        public RCCReport _rccReport = new RCCReport();

        DataTable tmp_user_pc = new DataTable();
        DataTable GLOB_DataTable = new DataTable();
        DataTable _PRINTDoc = new DataTable();
        int warrno = 1;

        Services.Base bsObj;
        public clsInventory()
        {
            bsObj = new Services.Base();

        }

        public void Ageing_Report_for_Provisioning(InvReportPara _objRepPara)
        {
            DataTable param = new DataTable();
            DataTable GLOB_DataTable = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;
            DataTable dtheding = new DataTable();
            dtheding = bsObj.CHNLSVC.General.GET_REF_AGE_SLOT(_objRepPara._GlbUserID);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("itemstatus", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("age", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["itemstatus"] = _objRepPara._GlbReportItemStatus == "" ? "ALL" : _objRepPara._GlbReportItemStatus;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["age"] = _objRepPara._GlbReportGroupLastGroupCat.Trim();
            param.Rows.Add(dr);

            DataTable STKAGEDET = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB_AllCom(_objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TEMP = new DataTable();
                    TEMP = bsObj.CHNLSVC.MsgPortal.GET_provisioning_AGE(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbUserID, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportGroupLastGroupCat.Trim());
                    STKAGEDET.Merge(TEMP);
                }
        }
            param.TableName = "aa";
            STKAGEDET.TableName = "bb";

            _summarized_age_report.Database.Tables["param"].SetDataSource(param);
            _summarized_age_report.Database.Tables["GLB_AGE_PROVISIONING"].SetDataSource(STKAGEDET);
        }

        public void Fixa_dtl_WDepreciation(InvReportPara _objRepPara)
        {

            string docNo = default(string);
            docNo = _objRepPara._GlbReportDoc;
            tmp_user_pc = new DataTable();

            DataTable FIXA_DTL = new DataTable();
           
            DataTable ITEM_TRANS = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM1 = new DataTable();

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("TOdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));


            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["fromdate"] = _objRepPara._GlbReportFromDate;
            dr["TOdate"] = _objRepPara._GlbReportToDate;
            dr["todate"] = _objRepPara._GlbReportToDate;
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["comp"] = _objRepPara._GlbReportComp;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["compaddr"] = _objRepPara._GlbReportCompAddr;
            dr["docType"] = _objRepPara._GlbReportType == "" ? "ALL" : _objRepPara._GlbReportType;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["Direct"] = _objRepPara._GlbReportDirection == "" ? "ALL" : _objRepPara._GlbReportDirection;            
            param.Rows.Add(dr);

            //ITEM_TRANS.Clear();
    DataTable TMP = new DataTable();
            FIXA_DTL.Clear();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);
            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                   
                    // TMP_SER = bsObj.CHNLSVC.MsgPortal.PrintTransDetListReport(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel);
                    // TMP_SER = bsObj.CHNLSVC.MsgPortal.ProcessInventoryStatement_SCM(_objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbUserID, drow["tpl_pc"].ToString(), drow["tpl_com"].ToString(), _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3);
                    //TMP_SER = bsObj.CHNLSVC.MsgPortal.PrintTransDetListReport(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel);
                    //ITEM_TRANS.Merge(TMP_SER);
                   // TMP_SER = bsObj.CHNLSVC.MsgPortal.GetFIXA_dtl_WithDepreciation(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel);
                   // FIXA_DTL.Merge(TMP);
                    TMP = bsObj.CHNLSVC.MsgPortal.GetFIXA_dtl_WithDepreciation(_objRepPara._GlbReportComp, drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel);
                    FIXA_DTL.Merge(TMP);
                }
            }

            //FIXA_DTL = bsObj.CHNLSVC.MsgPortal.GetFIXA_dtl_WithDepreciation(_objRepPara._GlbReportComp, "RNGD", _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel);
            //MST_LOC = bsObj.CHNLSVC.Inventory.Get_all_LocationsTable(_objRepPara._GlbReportCompCode);
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);
            _FIXA_DepreDtl.Database.Tables["fixa_GrnDepre_Dtl"].SetDataSource(FIXA_DTL);
            _FIXA_DepreDtl.Database.Tables["mst_com"].SetDataSource(MST_COM);
            _FIXA_DepreDtl.Database.Tables["param"].SetDataSource(param);
            //_itmwiseTransList.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            //_invStsTr3.Database.Tables["PROC_INVENTORY_STATEMENT"].SetDataSource(ITEM_TRANS);
            //_invStsTr3.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            //_invStsTr3.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            //_invStsTr3.Database.Tables["param"].SetDataSource(param);
        }

        public void GetKITComponent(string comcode,string kitItm)
        {    
            //Wimal 21/06/2018
            DataTable FIXA_DTL = new DataTable();
            DataTable MST_COM = new DataTable();          
            FIXA_DTL.Clear();
            MST_COM.Clear();
            FIXA_DTL = bsObj.CHNLSVC.MsgPortal.GetKIT_Component(kitItm);
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(comcode);
            _KIT_ComSetup.Database.Tables["KITComponentSetUp"].SetDataSource(FIXA_DTL);
            _KIT_ComSetup.Database.Tables["mst_com"].SetDataSource(MST_COM);    
  
        }


        public void CusDecEntryRequest(InvReportPara _objRepPara)
        {
            // kapila 
            DataTable glbTable = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("location", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("supplier", typeof(string));
            param.Columns.Add("agent", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["fromdate"] = _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            dr["supplier"] = _objRepPara._GlbReportSupplier;
            dr["agent"] = _objRepPara._GlbReportAgent;
            dr["location"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["channel"] = _objRepPara._GlbReportChannel == "" ? "ALL" : _objRepPara._GlbReportChannel;

            param.Rows.Add(dr);

            glbTable.Clear();

            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.CusDecEntryRequest(_objRepPara._GlbUserID, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportType, _objRepPara._GlbReportParaLine1);
            glbTable.Merge(tmp_Table);

            _cusDecEntry.Database.Tables["CusDecEntryReq"].SetDataSource(glbTable);
            _cusDecEntry.Database.Tables["param"].SetDataSource(param);
        }
        public void printWarrantyCard(string _warrno, int _noofcopy, string _papersize, string _doc_no)
        {
            try
            {
                PrintDocument pd = new PrintDocument();
                pd.DefaultPageSettings.Margins.Left = 30;
                pd.DefaultPageSettings.Margins.Top = 10;
                pd.DefaultPageSettings.Margins.Right = 10;
                pd.DefaultPageSettings.Margins.Bottom = 10;
                pd.OriginAtMargins = true;

                Font font = new Font("Tahoma", 8);
                PrinterSettings ps = new PrinterSettings();
                pd.PrinterSettings.PrinterName = ps.PrinterName;
                Session["papersize"] = _papersize;
                _PRINTDoc = new DataTable();
                _PRINTDoc = bsObj.CHNLSVC.Inventory.getWarrantyPrintDetails(_warrno, 1, _doc_no);
                Session["PRINTDoc"] = _PRINTDoc;

                for (warrno = 1; warrno <= _noofcopy; warrno++)
                {
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                    pd.Print();
                    ClientPrintJob cpj = new ClientPrintJob();
                    //set file to print...
                    //cpj.PrintFile = pd;
                    //set client printer...
                    if (useDefaultPrinter || printerName == "null")
                        cpj.ClientPrinter = new DefaultPrinter();
                    else
                        cpj.ClientPrinter = new InstalledPrinter(printerName);
                    //send it...
                    cpj.SendToClient(Response);
                    Response.Write("<script>window.print();</script>");
                }

            }
            catch (Exception ex)
            {

            }
        }


        void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Graphics graphics = ev.Graphics;
            Font font = new Font("Tahoma", 10);
            SolidBrush br = new SolidBrush(Color.Black);
            float fontHeight = font.GetHeight();
            int startX = 25;
            int startY = 0;
            int OffsetY = 0;
            int OffsetX = 0;

            DataTable PRINTDoc = new DataTable();
            PRINTDoc = Session["PRINTDoc"] as DataTable;

            if (PRINTDoc.Rows.Count > 0)
            {
                foreach (DataRow drow in PRINTDoc.Rows)
                {
                    DateTime _docdate = Convert.ToDateTime(drow["irsm_doc_dt"].ToString()).Date;
                    if (Session["papersize"].ToString() == "4Inch" || Session["papersize"].ToString() == "8Inch")
                    {
                        OffsetY = 0;
                        graphics.DrawString(Convert.ToString(warrno), font, br, 550 + OffsetX + startX, 25 + OffsetY + startY); //" #"
                        graphics.DrawString(drow["irsm_warr_no"].ToString(), font, br, 60 + OffsetX + startX, 17 + OffsetY + startY); //"Warranty Card #"
                        graphics.DrawString(drow["irsm_itm_brandnm"].ToString(), font, br, 150 + OffsetX + startX, 75 + OffsetY + startY); //"Brand Name"
                        graphics.DrawString(drow["irsm_itm_cd"].ToString(), font, br, 90 + OffsetX + startX, 105 + OffsetY + startY); //"Item Code"
                        graphics.DrawString(drow["irsm_itm_model"].ToString(), font, br, 120 + OffsetX + startX, 135 + OffsetY + startY); //"Model #"
                        graphics.DrawString(drow["irsm_ser_1"].ToString(), font, br, 130 + OffsetX + startX, 165 + OffsetY + startY); //"Serial #"
                        if (drow["ith_doc_tp"].ToString() == "AOD")
                        {
                            graphics.DrawString(drow["ml_loc_desc"].ToString(), font, br, 550 + OffsetX + startX, 75 + OffsetY + startY); //"Mr. ABC Defghijklmnopqrstuvwxyz"
                            graphics.DrawString("AOD : " + drow["ith_doc_no"].ToString() + " ON " + _docdate.ToString("dd/MMM/yyyy"), font, br, 40 + OffsetX + startX, 550 + OffsetY + startY); //" AOD #, Date of Issue"
                        }
                        else
                        {
                            graphics.DrawString(drow["irsm_cust_name"].ToString(), font, br, 135 + OffsetX + startX, 14 + OffsetY + startY); //"Mr. ABC Defghijklmnopqrstuvwxyz"
                            graphics.DrawString(drow["irsm_cust_addr"].ToString(), font, br, 135 + OffsetX + startX, 18 + OffsetY + startY); //"No. Abcdefghijklmnopqrstuvwxyz,Abcdefghijklmnopqrstuvwxyz"
                            graphics.DrawString(drow["irsm_invoice_no"].ToString(), font, br, 135 + OffsetX + startX, 22 + OffsetY + startY); //"RTSCS000931"
                            graphics.DrawString(drow["sah_man_ref"].ToString(), font, br, 135 + OffsetX + startX, 26 + OffsetY + startY); //"Inv ref no"
                            graphics.DrawString(drow["ith_doc_no"].ToString(), font, br, 135 + OffsetX + startX, 30 + OffsetY + startY); //"DO"
                            graphics.DrawString(drow["sah_sales_ex_nm"].ToString(), font, br, 135 + OffsetX + startX, 34 + OffsetY + startY); //"Exe Name"
                            graphics.DrawString("", font, br, 135 + OffsetX + startX, 38 + OffsetY + startY); //"Exe Type"

                            graphics.DrawString("" + _docdate.ToString("dd/MMM/yyyy"), font, br, 55 + OffsetX + startX, 65 + OffsetY + startY); //"Commence date"
                            graphics.DrawString("DATE OF ISSUE : " + _docdate.ToString("dd/MMM/yyyy"), font, br, 40 + OffsetX + startX, 75 + OffsetY + startY); //"Date of Issue"
                            //graphics.DrawString("WARRATY PERIOD : " & Trim(WComPeriod), font, br, 40+ OffsetX + startX, 80 + shift) //"Date of Issue"
                            graphics.DrawString("FORWARD DATE : " + _docdate.ToString("dd/MMM/yyyy"), font, br, 40 + OffsetX + startX, 80 + OffsetY + startY); //"Date of Issue"
                        }
                    }
                    else
                    {
                        if (warrno == 1) { OffsetY = 30; } else { OffsetY = 580; }

                        graphics.DrawString(Convert.ToString(warrno), font, br, 500 + OffsetX + startX, 25 + OffsetY + startY); //" #"
                        graphics.DrawString(drow["irsm_warr_no"].ToString(), font, br, 80 + OffsetX + startX, 17 + OffsetY + startY); //"Warranty Card #"
                        graphics.DrawString(drow["irsm_itm_brandnm"].ToString(), font, br, 240 + OffsetX + startX, 80 + OffsetY + startY); //"Brand Name"
                        graphics.DrawString(drow["irsm_itm_cd"].ToString(), font, br, 105 + OffsetX + startX, 113 + OffsetY + startY); //"Item Code"
                        graphics.DrawString(drow["irsm_itm_model"].ToString(), font, br, 165 + OffsetX + startX, 148 + OffsetY + startY); //"Model #"
                        graphics.DrawString(drow["irsm_ser_1"].ToString(), font, br, 205 + OffsetX + startX, 180 + OffsetY + startY); //"Serial #"
                        if (drow["ith_doc_tp"].ToString() == "AOD")
                        {
                            graphics.DrawString(drow["ml_loc_desc"].ToString(), font, br, 550 + OffsetX + startX, 75 + OffsetY + startY); //"Mr. ABC Defghijklmnopqrstuvwxyz"
                            graphics.DrawString("AOD : " + drow["ith_doc_no"].ToString() + " ON " + _docdate.ToString("dd/MMM/yyyy"), font, br, 40 + OffsetX + startX, 550 + OffsetY + startY); //"Date of Issue"
                        }
                        else
                        {
                            graphics.DrawString(drow["irsm_cust_name"].ToString(), font, br, 90 + OffsetX + startX, 205 + OffsetY + startY); //"Mr. ABC Defghijklmnopqrstuvwxyz"
                            graphics.DrawString(drow["irsm_cust_addr"].ToString(), font, br, 120 + OffsetX + startX, 235 + OffsetY + startY); //"No. Abcdefghijklmnopqrstuvwxyz,Abcdefghijklmnopqrstuvwxyz"
                            graphics.DrawString(drow["irsm_invoice_no"].ToString(), font, br, 50 + OffsetX + startX, 430 + OffsetY + startY); //"RTSCS000931"
                            graphics.DrawString(drow["sah_man_ref"].ToString(), font, br, 150 + OffsetX + startX, 430 + OffsetY + startY); //"Inv ref no"
                            graphics.DrawString(drow["ith_doc_no"].ToString(), font, br, 300 + OffsetX + startX, 430 + OffsetY + startY); //"DO"
                            graphics.DrawString(drow["sah_sales_ex_nm"].ToString(), font, br, 450 + OffsetX + startX, 165 + OffsetY + startY); //"Exe Name"
                            graphics.DrawString("", font, br, 135 + OffsetX + startX, 38 + OffsetY + startY); //"Exe Type"

                            graphics.DrawString("" + _docdate.ToString("dd/MMM/yyyy"), font, br, 230 + OffsetX + startX, 382 + OffsetY + startY); //"Commence date"
                            graphics.DrawString("DATE OF ISSUE : " + _docdate.ToString("dd/MMM/yyyy"), font, br, 120 + OffsetX + startX, 320 + OffsetY + startY); //"Date of Issue"
                            //graphics.DrawString("WARRAMTY PERIOD : " & Trim(WComPeriod), font, br, 40+ OffsetX + startX, 114 + OffsetY + startY); //"Date of Issue"
                            graphics.DrawString("FORWARD DATE : " + _docdate.ToString("dd/MMM/yyyy"), font, br, 120 + OffsetX + startX, 350 + OffsetY + startY); //"Date of Issue"
                        }
                    }
                }
            }

        }


        public void LastNoSeqReport(InvReportPara _objRepPara)
        {
            DataTable param = new DataTable();
            DataTable LASTNOSEQPAGE = new DataTable();
            DataTable DataTable_U = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetLastNoSeqDetails(Convert.ToDateTime(_objRepPara._GlbReportAsAtDate).Date, _objRepPara._GlbUserID, _objRepPara._GlbReportDocType, drow["tpl_com"].ToString(), drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            DataTable_U = GLOB_DataTable.DefaultView.ToTable(true, "profitcenter");

            if (DataTable_U.Rows.Count > 0)
            {
                foreach (DataRow drow in DataTable_U.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Inventory.GetLastNoSeqPageDetails(Convert.ToDateTime(_objRepPara._GlbReportAsAtDate).Date, drow["profitcenter"].ToString());
                    LASTNOSEQPAGE.Merge(TMP_DataTable);
                }
            }

            if (LASTNOSEQPAGE.Rows.Count <= 0)
            {
                LASTNOSEQPAGE = bsObj.CHNLSVC.Inventory.GetLastNoSeqPageDetails(Convert.ToDateTime(_objRepPara._GlbReportAsAtDate).Date, "");
            }
            //var result = LASTNOSEQ_DOC.Where(t1=>!db.table2.Any(t2=>t2.DealReference==t1.DealReference))


            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            param.Rows.Add(dr);

            _LastNoSeq.Database.Tables["PROC_LASTNO_SEQ"].SetDataSource(GLOB_DataTable);
            _LastNoSeq.Database.Tables["LAST_NO_SEQ_PAGES"].SetDataSource(LASTNOSEQPAGE);
            _LastNoSeq.Database.Tables["param"].SetDataSource(param);
        }
        public void Reservation_Summary(InvReportPara _objRepPara)
        {
            // 
            DataTable param = new DataTable();
            GLOB_DataTable = new DataTable();
            tmp_user_pc = new DataTable();
            DataTable MST_COM = new DataTable();

            DataRow dr;

            DataTable TMP_DataTable = new DataTable();

            GLOB_DataTable.Merge(TMP_DataTable);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_SER = new DataTable();

                    TMP_SER = bsObj.CHNLSVC.MsgPortal.ReservationReport(_objRepPara._GlbUserID, drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportExpDate.Date, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel);
                    GLOB_DataTable.Merge(TMP_SER);


                }
            }


            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("TOdate", typeof(DateTime));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("itemstatus", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));


            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["fromdate"] = _objRepPara._GlbReportFromDate;
            dr["asatdate"] = _objRepPara._GlbReportExpDate;
            dr["TOdate"] = _objRepPara._GlbReportToDate;
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["itemstatus"] = _objRepPara._GlbReportItemStatus == "" ? "ALL" : _objRepPara._GlbReportItemStatus;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            param.Rows.Add(dr);

            _resSumm.Database.Tables["RESERVE_DET"].SetDataSource(GLOB_DataTable);
            _resSumm.Database.Tables["MST_COM"].SetDataSource(MST_COM);

            _resSumm.Database.Tables["param"].SetDataSource(param);

        }

        public void Reservation_Details(InvReportPara _objRepPara)
        {
            // 
            DataTable param = new DataTable();
            GLOB_DataTable = new DataTable();
            tmp_user_pc = new DataTable();
            DataTable MST_COM = new DataTable();

            DataRow dr;

            DataTable TMP_DataTable = new DataTable();

            GLOB_DataTable.Merge(TMP_DataTable);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_SER = new DataTable();

                    GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.ReservationReport(_objRepPara._GlbUserID, drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportExpDate.Date, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel);
                    GLOB_DataTable.Merge(TMP_SER);


                }
            }


            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("itemstatus", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));


            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["itemstatus"] = _objRepPara._GlbReportItemStatus == "" ? "ALL" : _objRepPara._GlbReportItemStatus;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            param.Rows.Add(dr);

            _resDet.Database.Tables["TO_BOND_STUS"].SetDataSource(GLOB_DataTable);
            _resDet.Database.Tables["MST_COM"].SetDataSource(MST_COM);

            _resDet.Database.Tables["param"].SetDataSource(param);

        }

        public void MRN_Status(InvReportPara _objRepPara)
        {
            // 
            DataTable param = new DataTable();
            GLOB_DataTable = new DataTable();
            tmp_user_pc = new DataTable();
            DataTable MST_COM = new DataTable();

            DataRow dr;

            DataTable TMP_DataTable = new DataTable();

            GLOB_DataTable.Merge(TMP_DataTable);

            //tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            //if (tmp_user_pc.Rows.Count > 0)
            //{
            //    foreach (DataRow drow in tmp_user_pc.Rows)
            //    {
            DataTable TMP_SER = new DataTable();

            GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.MRNStatusReport(_objRepPara._GlbUserID, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportReqFrom, _objRepPara._GlbReportReqTo, _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel);
            GLOB_DataTable.Merge(TMP_SER);


            //    }
            //}


            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("itemstatus", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("TOdate", typeof(DateTime));


            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["channel"] = _objRepPara._GlbReportReqFrom;
            dr["itemstatus"] = _objRepPara._GlbReportReqTo;
            dr["fromdate"] = _objRepPara._GlbReportFromDate;
            dr["TOdate"] = _objRepPara._GlbReportToDate;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;

            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            param.Rows.Add(dr);

            _mrnStus.Database.Tables["MRN_STUS"].SetDataSource(GLOB_DataTable);
            _mrnStus.Database.Tables["MST_COM"].SetDataSource(MST_COM);

            _mrnStus.Database.Tables["param"].SetDataSource(param);

        }

        public void Item_Buffer_Status(InvReportPara _objRepPara)
        {
            // 
            DataTable param = new DataTable();
            GLOB_DataTable = new DataTable();
            tmp_user_pc = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.ItemBufferStatusReport(_objRepPara._GlbUserID, drow["tpl_com"].ToString(), _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel, drow["tpl_pc"].ToString(), _objRepPara._GlbReportDocType);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("itemstatus", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("doctype", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["itemstatus"] = _objRepPara._GlbReportItemStatus == "" ? "ALL" : _objRepPara._GlbReportItemStatus;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["doctype"] = _objRepPara._GlbReportDocType;

            param.Rows.Add(dr);

            _itmBufStus.Database.Tables["ITEM_BUFFER_STUS"].SetDataSource(GLOB_DataTable);
            _itmBufStus.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _itmBufStus.Database.Tables["param"].SetDataSource(param);

        }
        public void To_Bond_Status(InvReportPara _objRepPara)
        {
            // 
            DataTable param = new DataTable();
            GLOB_DataTable = new DataTable();
            tmp_user_pc = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;

            DataTable TMP_DataTable = new DataTable();
            TMP_DataTable = bsObj.CHNLSVC.MsgPortal.ToBondStatusReport(_objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportToBondNo, _objRepPara._GlbReportGRNNo, _objRepPara._GlbReportReqNo, _objRepPara._GlbUserID, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel, "");
            GLOB_DataTable.Merge(TMP_DataTable);

            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("itemstatus", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));


            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["itemstatus"] = _objRepPara._GlbReportItemStatus == "" ? "ALL" : _objRepPara._GlbReportItemStatus;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            param.Rows.Add(dr);

            _toBondStusOld.Database.Tables["TO_BOND_STUS"].SetDataSource(GLOB_DataTable);
            _toBondStusOld.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _toBondStusOld.Database.Tables["param"].SetDataSource(param);

        }
        public void Age_Monitoring(InvReportPara _objRepPara)
        {

            DataTable param = new DataTable();
            GLOB_DataTable = new DataTable();
            tmp_user_pc = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.getCurrentAgeDetails(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportSupplier, _objRepPara._GlbUserID, _objRepPara._GlbReportPromotor);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("itemstatus", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("withcost", typeof(int));
            param.Columns.Add("withserial", typeof(int));
            param.Columns.Add("withstatus", typeof(int));
            param.Columns.Add("supplier", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["itemstatus"] = _objRepPara._GlbReportItemStatus == "" ? "ALL" : _objRepPara._GlbReportItemStatus;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            dr["withcost"] = 0;
            dr["withserial"] = 0;
            dr["withstatus"] = 0;
            dr["supplier"] = _objRepPara._GlbReportSupplier == "" ? "ALL" : _objRepPara._GlbReportSupplier;

            param.Rows.Add(dr);

            _ageMonit.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
            _ageMonit.Database.Tables["param"].SetDataSource(param);

        }

        public void Loc_wise_item_age(InvReportPara _objRepPara)
        {

            DataTable param = new DataTable();
            GLOB_DataTable = new DataTable();
            tmp_user_pc = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    if (_objRepPara._GlbReportType == "CUR")
                        TMP_DataTable = bsObj.CHNLSVC.MsgPortal.getCurrentAgeDetails(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportSupplier, _objRepPara._GlbUserID, _objRepPara._GlbReportPromotor);
                    else
                        TMP_DataTable = bsObj.CHNLSVC.MsgPortal.getAsAtAgeDetails(_objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportSupplier, _objRepPara._GlbUserID, _objRepPara._GlbReportPromotor, _objRepPara._GlbReportIsFast);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("itemstatus", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("withcost", typeof(int));
            param.Columns.Add("withserial", typeof(int));
            param.Columns.Add("withstatus", typeof(int));
            param.Columns.Add("supplier", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["itemstatus"] = _objRepPara._GlbReportItemStatus == "" ? "ALL" : _objRepPara._GlbReportItemStatus;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            dr["withcost"] = _objRepPara._GlbReportWithCost;
            dr["withserial"] = 0;
            dr["withstatus"] = 0;
            dr["supplier"] = _objRepPara._GlbReportSupplier == "" ? "ALL" : _objRepPara._GlbReportSupplier;

            param.Rows.Add(dr);

            if (_objRepPara._GlbReportName == "LocwiseItemAge.rpt")
            {
                _locAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                _locAge.Database.Tables["param"].SetDataSource(param);
            }
            if (_objRepPara._GlbReportName == "CatwiseItemAge.rpt")
            {
                _catAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                _catAge.Database.Tables["param"].SetDataSource(param);
            }
            if (_objRepPara._GlbReportName == "CatScatwiseItemAge.rpt")
            {
                _catScatAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                _catScatAge.Database.Tables["param"].SetDataSource(param);
            }
            if (_objRepPara._GlbReportName == "StuswiseItemAge.rpt")
            {
                _StusAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                _StusAge.Database.Tables["param"].SetDataSource(param);
            }
            if (_objRepPara._GlbReportName == "ItmBrndwiseItemAge.rpt")
            {
                _ItmBrndAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                _ItmBrndAge.Database.Tables["param"].SetDataSource(param);
            }

        }
        public void ConsignmentMovement(InvReportPara _objRepPara)
        {

            string docNo = default(string);
            docNo = _objRepPara._GlbReportDoc;

            DataTable ITEM_TRANS = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM1 = new DataTable();

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("TOdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));


            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["fromdate"] = _objRepPara._GlbReportFromDate;
            dr["TOdate"] = _objRepPara._GlbReportToDate;
            dr["todate"] = _objRepPara._GlbReportToDate;
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["comp"] = _objRepPara._GlbReportComp;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["compaddr"] = _objRepPara._GlbReportCompAddr;
            dr["docType"] = _objRepPara._GlbReportType == "" ? "ALL" : _objRepPara._GlbReportType;
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1;
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2;
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3;
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4;
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5;
            dr["Direct"] = _objRepPara._GlbReportDirection == "" ? "ALL" : _objRepPara._GlbReportDirection;
            dr["Model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel;
            dr["Brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand;
            dr["itemCode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode;


            param.Rows.Add(dr);

            ITEM_TRANS.Clear();


            //tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(_objRepPara._GlbReportCompCode, _objRepPara._GlbUserID);

            //if (tmp_user_pc.Rows.Count > 0)
            //{
            //    foreach (DataRow drow in tmp_user_pc.Rows)
            //    {
            //        DataTable TMP_SER = new DataTable();
            //        TMP_SER = bsObj.CHNLSVC.MsgPortal.PrintTransDetListReport(_objRepPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel);

            //        ITEM_TRANS.Merge(TMP_SER);


            //    }
            //}


            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);


            _consMove.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _consMove.Database.Tables["param"].SetDataSource(param);


        }

        public void StockLedger(InvReportPara _objRepPara)
        {

            string docNo = default(string);
            docNo = _objRepPara._GlbReportDoc;
            tmp_user_pc = new DataTable();

            DataTable ITEM_TRANS = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM1 = new DataTable();

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("TOdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));


            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["fromdate"] = _objRepPara._GlbReportFromDate;
            dr["TOdate"] = _objRepPara._GlbReportToDate;
            dr["todate"] = _objRepPara._GlbReportToDate;
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["comp"] = _objRepPara._GlbReportComp;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["compaddr"] = _objRepPara._GlbReportCompAddr;
            dr["docType"] = _objRepPara._GlbReportType == "" ? "ALL" : _objRepPara._GlbReportType;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            dr["Direct"] = _objRepPara._GlbReportDirection == "" ? "ALL" : _objRepPara._GlbReportDirection;



            param.Rows.Add(dr);

            ITEM_TRANS.Clear();


            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_SER = new DataTable();
                    TMP_SER = bsObj.CHNLSVC.MsgPortal.PrintTransDetListReport(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel);
                    // TMP_SER = bsObj.CHNLSVC.Inventory.ProcessInventoryStatement(_objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbUserID, drow["tpl_pc"].ToString(), drow["tpl_com"].ToString(), _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3);

                    ITEM_TRANS.Merge(TMP_SER);


                }
            }

            MST_LOC = bsObj.CHNLSVC.Inventory.Get_all_LocationsTable(_objRepPara._GlbReportCompCode);
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            _stkLedger.Database.Tables["TRANS_DET_LIST"].SetDataSource(ITEM_TRANS);
            _stkLedger.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _stkLedger.Database.Tables["param"].SetDataSource(param);
            _stkLedger.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

        }
        public void inventoryStatement(InvReportPara _objRepPara)
        {
            string docNo = default(string);
            docNo = _objRepPara._GlbReportDoc;


            DataTable PROC_INVENTORY_STATEMENT = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["fromdate"] = _objRepPara._GlbReportFromDate;
            dr["todate"] = _objRepPara._GlbReportToDate;
            dr["profitcenter"] = _objRepPara._GlbReportProfit;
            dr["comp"] = _objRepPara._GlbReportComp;
            dr["compaddr"] = _objRepPara._GlbReportCompAddr;
            param.Rows.Add(dr);


            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_SER = new DataTable();
                    //TMP_SER = bsObj.CHNLSVC.MsgPortal.PrintTransDetListReport(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel);
                    TMP_SER = bsObj.CHNLSVC.MsgPortal.ProcessInventoryStatement(_objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbUserID, drow["tpl_pc"].ToString(), drow["tpl_com"].ToString(), _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3);

                    PROC_INVENTORY_STATEMENT.Merge(TMP_SER);


                }
            }

            //  PROC_INVENTORY_STATEMENT.Clear();
            //PROC_INVENTORY_STATEMENT = bsObj.CHNLSVC.Inventory.ProcessInventoryStatement(Convert.ToDateTime(_objRepPara._GlbReportFromDate).Date, Convert.ToDateTime(_objRepPara._GlbReportToDate).Date, _objRepPara._GlbUserID, _objRepPara._GlbReportProfit, _objRepPara._GlbReportCompCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3);
            //  PROC_INVENTORY_STATEMENT = bsObj.CHNLSVC.Inventory.ProcessInventoryStatement(_objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbUserID, drow["tpl_pc"].ToString(), drow["tpl_com"].ToString(), _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3);

            MST_LOC = bsObj.CHNLSVC.Inventory.Get_all_LocationsTable(_objRepPara._GlbReportCompCode);
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            if (_objRepPara._GlbReportName == "InventoryStatements.rpt") //Sanjeewa 2013-06-21
            {
                _invSts.Database.Tables["PROC_INVENTORY_STATEMENT"].SetDataSource(PROC_INVENTORY_STATEMENT);
                _invSts.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                _invSts.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _invSts.Database.Tables["param"].SetDataSource(param);
            }
            //else if (_objRepPara._GlbReportName == "InventoryStatementsTr.rpt")
            //{
            //    _invStsTr.Database.Tables["PROC_INVENTORY_STATEMENT"].SetDataSource(PROC_INVENTORY_STATEMENT);
            //    _invStsTr.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

            //    _invStsTr.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            //    _invStsTr.Database.Tables["param"].SetDataSource(param);
            //}
            //else if (_objRepPara._GlbReportName == "InventoryStatementsTr3.rpt")
            //{
            //    _invStsTr3.Database.Tables["PROC_INVENTORY_STATEMENT"].SetDataSource(PROC_INVENTORY_STATEMENT);
            //    _invStsTr3.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

            //    _invStsTr3.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            //    _invStsTr3.Database.Tables["param"].SetDataSource(param);
            //}
            //else
            //{
            //    _invStsTr2.Database.Tables["PROC_INVENTORY_STATEMENT"].SetDataSource(PROC_INVENTORY_STATEMENT);
            //    _invStsTr2.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

            //    _invStsTr2.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            //    _invStsTr2.Database.Tables["param"].SetDataSource(param);
            //}
        }

        public void Curr_Age_Report(InvReportPara _objRepPara)
        {
            // Sanjeewa 30-03-2016
            DataTable param = new DataTable();
            GLOB_DataTable = new DataTable();
            tmp_user_pc = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.getCurrentAgeDetails(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportSupplier, _objRepPara._GlbUserID, _objRepPara._GlbReportPromotor);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("itemstatus", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("withcost", typeof(int));
            param.Columns.Add("withserial", typeof(int));
            param.Columns.Add("withstatus", typeof(int));
            param.Columns.Add("supplier", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["itemstatus"] = _objRepPara._GlbReportItemStatus == "" ? "ALL" : _objRepPara._GlbReportItemStatus;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            dr["withcost"] = 0;
            dr["withserial"] = 0;
            dr["withstatus"] = 0;
            dr["supplier"] = _objRepPara._GlbReportSupplier == "" ? "ALL" : _objRepPara._GlbReportSupplier;

            param.Rows.Add(dr);

            _CurrAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
            _CurrAge.Database.Tables["param"].SetDataSource(param);

        }

        public void printInwardDocs(string _reportName, string _docno)
        {
            string docNo = default(string);
            docNo = _docno;

            DataTable VIW_INV_MOVEMENT_SERIAL = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable mst_movcatetp = new DataTable();
            DataTable PO_DTL = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable CUSDEC_DTL = new DataTable();
            DataTable param = new DataTable();
            DataTable supinvdetails = new DataTable();
            
            int i = 0;
            DataRow dr;

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_ITM_UOM", typeof(string));

            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            int _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));
            PRINT_DOC.Columns.Add("USER", typeof(string));

            param.Columns.Add("USER", typeof(string));
            param.Columns.Add("docType", typeof(string));
            dr = param.NewRow();
            dr["user"] = Session["UserID"].ToString();
            if (Session["direction"] != null)
            {
                dr["docType"] = Session["direction"].ToString();
            }
            param.Rows.Add(dr);

            dr = PRINT_DOC.NewRow();
            dr["NOOFPAGES"] = _numPages;
            dr["SHOWCOM"] = BaseCls.ShowComName;
            dr["USER"] = Session["UserID"].ToString();
            PRINT_DOC.Rows.Add(dr);

            //if (_reportName == "Inward_Docs_Consign.rpt")
            //{
            VIW_INV_MOVEMENT_SERIAL = bsObj.CHNLSVC.Sales.GetMovementSerials_wo_Ser(docNo);
            //}
            //else
            //{
            //    VIW_INV_MOVEMENT_SERIAL = bsObj.CHNLSVC.Sales.GetMovementSerials(docNo);
            //}
            PO_DTL.Clear();
            PO_DTL = bsObj.CHNLSVC.Sales.GetPODetails(docNo);
            

            foreach (DataRow row in VIW_INV_MOVEMENT_SERIAL.Rows)
            {
                i = i + 1;
                if (i == 1)
                {
                    MST_LOC = bsObj.CHNLSVC.Sales.GetLocationCode(row["ITH_COM"].ToString(), row["ITH_LOC"].ToString());
                    MST_LOC_1 = bsObj.CHNLSVC.Sales.GetLocationCode(row["ITH_COM"].ToString(), row["ITH_OTH_LOC"].ToString());
                    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(row["ITH_COM"].ToString());
                    mst_movcatetp = bsObj.CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());

                    if (!string.IsNullOrEmpty(row["ITH_OTH_DOCNO"].ToString()))
                    {
                        sat_hdr = bsObj.CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                    }

                    CUSDEC_DTL.Clear();
                    CUSDEC_DTL = bsObj.CHNLSVC.Inventory.GetEntryDtl(row["ITH_OTH_DOCNO"].ToString());

                }

                MST_ITM1 = bsObj.CHNLSVC.Sales.GetItemCode(row["ITH_COM"].ToString(), row["ITS_ITM_CD"].ToString());
                MST_ITM_STUS1 = bsObj.CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                supinvdetails = bsObj.CHNLSVC.Sales.Getsupplierinvoicedetails(row["ITH_COM"].ToString(), docNo);
 
                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    dr["MI_ITM_UOM"] = row1["MI_ITM_UOM"].ToString();
                    MST_ITM.Rows.Add(dr);
                }
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();
                    MST_ITM_STUS.Rows.Add(dr);
                }

            }

            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);

            if (_reportName == "Inward_Docs.rpt")
            {
                _indoc.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _indoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _indoc.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _indoc.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _indoc.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _indoc.Database.Tables["mst_com"].SetDataSource(mst_com);
                _indoc.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
                _indoc.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _indoc.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);

                foreach (object repOp in _indoc.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _indoc.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                    }
                }
            }

            if (_reportName == "Inward_Docs_Full.rpt")
            {
                _indocfull.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _indocfull.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _indocfull.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _indocfull.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _indocfull.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _indocfull.Database.Tables["mst_com"].SetDataSource(mst_com);
                _indocfull.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
                _indocfull.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _indocfull.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);

                foreach (object repOp in _indocfull.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _indocfull.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                    }
                }
            }

            if (_reportName == "Inward_Docs_Full_ADJ_ABE.rpt")
            {
                _indocfull_ABE_ADJ.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _indocfull_ABE_ADJ.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _indocfull_ABE_ADJ.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _indocfull_ABE_ADJ.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _indocfull_ABE_ADJ.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _indocfull_ABE_ADJ.Database.Tables["mst_com"].SetDataSource(mst_com);
                _indocfull_ABE_ADJ.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
                _indocfull_ABE_ADJ.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _indocfull_ABE_ADJ.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);
                _indocfull_ABE_ADJ.Database.Tables["param"].SetDataSource(param);

                foreach (object repOp in _indocfull_ABE_ADJ.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _indocfull_ABE_ADJ.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                    }
                }
            }

            if (_reportName == "Inward_Docs_Full_GRN.rpt")
            {
                _indocfullGRN.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _indocfullGRN.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _indocfullGRN.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _indocfullGRN.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _indocfullGRN.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _indocfullGRN.Database.Tables["mst_com"].SetDataSource(mst_com);
                _indocfullGRN.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
                _indocfullGRN.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _indocfullGRN.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);

                foreach (object repOp in _indocfullGRN.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _indocfullGRN.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "entry_dtl")
                        {
                            ReportDocument subRepDoc = _indocfullGRN.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["CUSDEC_DTL"].SetDataSource(CUSDEC_DTL);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                    }
                }
            }

            if (_reportName == "Inward_Docs_Full_GRN_AAL.rpt")
            {
                _indocfullGRN_AAL.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _indocfullGRN_AAL.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _indocfullGRN_AAL.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _indocfullGRN_AAL.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _indocfullGRN_AAL.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _indocfullGRN_AAL.Database.Tables["mst_com"].SetDataSource(mst_com);
                _indocfullGRN_AAL.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
                _indocfullGRN_AAL.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _indocfullGRN_AAL.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);
                _indocfullGRN_AAL.Database.Tables["Sup_Inv_Details"].SetDataSource(supinvdetails);
                

                foreach (object repOp in _indocfullGRN_AAL.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _indocfullGRN_AAL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "entry_dtl")
                        {
                            ReportDocument subRepDoc = _indocfullGRN_AAL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["CUSDEC_DTL"].SetDataSource(CUSDEC_DTL);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                    }
                }
            }

            if (_reportName == "Inward_Docs_Full_GRN_ARL.rpt")
            {
                _indocfullGRN_ARL.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _indocfullGRN_ARL.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _indocfullGRN_ARL.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _indocfullGRN_ARL.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _indocfullGRN_ARL.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _indocfullGRN_ARL.Database.Tables["mst_com"].SetDataSource(mst_com);
                _indocfullGRN_ARL.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
                _indocfullGRN_ARL.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _indocfullGRN_ARL.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);

                foreach (object repOp in _indocfullGRN_ARL.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _indocfullGRN_ARL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "entry_dtl")
                        {
                            ReportDocument subRepDoc = _indocfullGRN_ARL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["CUSDEC_DTL"].SetDataSource(CUSDEC_DTL);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                    }
                }
            }

            if (_reportName == "Inward_Docs_Full_GRN_SGL.rpt")
            {
                _indocfullGRN_SGL.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _indocfullGRN_SGL.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _indocfullGRN_SGL.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _indocfullGRN_SGL.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _indocfullGRN_SGL.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _indocfullGRN_SGL.Database.Tables["mst_com"].SetDataSource(mst_com);
                _indocfullGRN_SGL.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
                _indocfullGRN_SGL.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _indocfullGRN_SGL.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);

                foreach (object repOp in _indocfullGRN_SGL.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _indocfullGRN_SGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "entry_dtl")
                        {
                            ReportDocument subRepDoc = _indocfullGRN_SGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["CUSDEC_DTL"].SetDataSource(CUSDEC_DTL);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                    }
                }
            }

            //Added by Tharindu 2018-01-19
            if (_reportName == "Inward_Docs_Full_GRN_ABE.rpt")
            {
                _indocfullGRN_ABE.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _indocfullGRN_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _indocfullGRN_ABE.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _indocfullGRN_ABE.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _indocfullGRN_ABE.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _indocfullGRN_ABE.Database.Tables["mst_com"].SetDataSource(mst_com);
                _indocfullGRN_ABE.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
                _indocfullGRN_ABE.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _indocfullGRN_ABE.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);

                foreach (object repOp in _indocfullGRN_ABE.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _indocfullGRN_ABE.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "entry_dtl")
                        {
                            ReportDocument subRepDoc = _indocfullGRN_ABE.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["CUSDEC_DTL"].SetDataSource(CUSDEC_DTL);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                    }
                }
            }


            // tHARINDU

            if (_reportName == "Inward_Docs_Full_GRN_AOA.rpt")
            {
                _indocfullGRN_AOA.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _indocfullGRN_AOA.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _indocfullGRN_AOA.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _indocfullGRN_AOA.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _indocfullGRN_AOA.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _indocfullGRN_AOA.Database.Tables["mst_com"].SetDataSource(mst_com);
                _indocfullGRN_AOA.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
                _indocfullGRN_AOA.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _indocfullGRN_AOA.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);

                foreach (object repOp in _indocfullGRN_AOA.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _indocfullGRN_AOA.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "entry_dtl")
                        {
                            ReportDocument subRepDoc = _indocfullGRN_AOA.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["CUSDEC_DTL"].SetDataSource(CUSDEC_DTL);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                    }
                }
            }


            if (_reportName == "Inward_Docs_Consign.rpt")
            {
                _consIn.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _consIn.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _consIn.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _consIn.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _consIn.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _consIn.Database.Tables["mst_com"].SetDataSource(mst_com);
                _consIn.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
                _consIn.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _consIn.Database.Tables["PO_DTL"].SetDataSource(PO_DTL);

                foreach (object repOp in _consIn.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _consIn.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "entry_dtl")
                        {
                            ReportDocument subRepDoc = _consIn.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["CUSDEC_DTL"].SetDataSource(CUSDEC_DTL);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                    }
                }
            }
        }

        public void printOutwardDocs(string _reportName, string _docno)
        {
            string docNo = default(string);
            docNo = _docno;

            DataTable VIW_INV_MOVEMENT_SERIAL = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable mst_movcatetp = new DataTable();
            DataTable VIW_SALES_DETAILS = new DataTable();
            DataTable DelCustomer = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable sat_vou_det = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable tblComDate = new DataTable();
            DataTable receiptDetails = new DataTable();
            DataTable param1 = new DataTable();
            DataTable DISPOSAL_ENTRY = new DataTable();
            DataTable Reqdetails = new DataTable();

            List<ReptPickSerials> _serList = new List<ReptPickSerials>();
            string SearchParams = string.Empty;
            DataTable result = new DataTable();
            string SerFL = string.Empty;

            param1.Columns.Add("saleType", typeof(string));
            param1.Columns.Add("MPC_ORD_ALPBT", typeof(Int16));
            param1.Columns.Add("SerialFL", typeof(string));
            param1.Columns.Add("USER", typeof(string));

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_ITM_UOM", typeof(string));

            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            int i = 0;
            int isCredit = 0;
            string inv_no = "";
            DataRow dr;

            int _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));
            dr = PRINT_DOC.NewRow();
            dr["NOOFPAGES"] = _numPages;
            //Mod By Udaya 30.08.2017
            HpSystemParameters _comNameHide = bsObj.CHNLSVC.Sales.GetSystemParameter("COM", Session["UserCompanyCode"].ToString(), "DOCOMNM", DateTime.Now); //BaseCls.ShowComName;
            dr["SHOWCOM"] = _comNameHide.Hsy_val;
            PRINT_DOC.Rows.Add(dr);

            if (!string.IsNullOrEmpty(docNo))
            {
                result = bsObj.CHNLSVC.General.GetItemInProduction(docNo, Session["UserCompanyCode"].ToString(), "I");
                _serList = bsObj.CHNLSVC.Inventory.GetSerialsByDocument(0, docNo);
            }
            if (_serList != null && _serList.Count > 0 && result.Rows.Count > 0)
            {
                SerFL = "From: "+_serList[0].Tus_ser_1 + " To: " + _serList[_serList.Count - 1].Tus_ser_1;
            }

            //VIW_INV_MOVEMENT_SERIAL = bsObj.CHNLSVC.Sales.GetMovementSerials(docNo);

            VIW_INV_MOVEMENT_SERIAL = bsObj.CHNLSVC.Sales.GetMovementSerials_wo_Ser(docNo);
            DISPOSAL_ENTRY = bsObj.CHNLSVC.Sales.GetMovementSerials_wo_Ser(docNo);

            foreach (DataRow row in VIW_INV_MOVEMENT_SERIAL.Rows)
            {
                i = i + 1;
                if (i == 1) 
                {
                    MST_LOC = bsObj.CHNLSVC.Sales.GetLocationCode(row["ITH_COM"].ToString(), row["ITH_LOC"].ToString());
                    MST_LOC_1 = bsObj.CHNLSVC.Sales.GetLocationCode(row["ITH_COM"].ToString(), row["ITH_OTH_LOC"].ToString());
                    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(row["ITH_COM"].ToString());
                    mst_movcatetp = bsObj.CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                    sat_vou_det = bsObj.CHNLSVC.Sales.getSalesGiftVouchaer(inv_no);

                    if (!string.IsNullOrEmpty(row["ITH_OTH_DOCNO"].ToString()))
                    {
                        sat_hdr = bsObj.CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                        VIW_SALES_DETAILS = bsObj.CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                        //DelCustomer = bsObj.CHNLSVC.MsgPortal.GetDeliverCustomer(row["ITH_OTH_DOCNO"].ToString());
                        DelCustomer = bsObj.CHNLSVC.MsgPortal.GetDeliverCustomer(docNo);
                        //foreach (DataRow row1 in sat_hdr.Rows)
                        //{
                        //    tblComDate = bsObj.CHNLSVC.Sales.GetWarrantyCommenceDate(row1["SAH_COM"].ToString(), row1["SAH_PC"].ToString(), row["ITH_OTH_DOCNO"].ToString());
                        //}
                        //if (sat_hdr.Rows.Count == 0)
                        //{
                        //    tblComDate = bsObj.CHNLSVC.Sales.GetWarrantyCommenceDate("", "", "");
                        //}

                        Reqdetails = bsObj.CHNLSVC.Sales.GetRequestdetails(row["ITH_OTH_DOCNO"].ToString());

                        tblComDate = bsObj.CHNLSVC.Sales.GetWarrantyCommenceDate("", "", "");
                        receiptDetails = bsObj.CHNLSVC.Sales.GetInvoiceReceiptDet(row["ITH_OTH_DOCNO"].ToString());
                        foreach (DataRow row1 in receiptDetails.Rows)
                        {
                            if (row1["SARD_PAY_TP"].ToString() == "CRNOTE")
                            {
                                isCredit = 1;
                            }
                        }
                    }
                    else
                    {
                        tblComDate = bsObj.CHNLSVC.Sales.GetWarrantyCommenceDate("", "", "");

                        VIW_SALES_DETAILS.Columns.Add("SAD_ITM_CD", typeof(string));
                        VIW_SALES_DETAILS.Columns.Add("sah_dt", typeof(DateTime));
                        dr = VIW_SALES_DETAILS.NewRow();
                        dr["SAD_ITM_CD"] = "1";
                        dr["sah_dt"] = Convert.ToDateTime("01/JAN/2016").Date.ToShortDateString();
                        VIW_SALES_DETAILS.Rows.Add(dr);
                    }

                    if (sat_hdr.Rows.Count > 0)
                    {
                        foreach (DataRow roww in sat_hdr.Rows)
                        {
                            mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(roww["SAH_COM"].ToString(), roww["SAH_PC"].ToString());
                            dr = param1.NewRow();
                            dr["saleType"] = roww["sah_inv_tp"].ToString();
                            if (result.Rows.Count >= 0)
                            {
                                dr["SerialFL"] = SerFL;
                            }
                            dr["USER"] = Session["UserID"].ToString();
                            if (mst_profit_center.Rows.Count > 0)
                            {
                                foreach (DataRow rowx in mst_profit_center.Rows)
                                {
                                    if (rowx["MPC_ORD_ALPBT"].ToString() == "")
                                    {
                                        dr["MPC_ORD_ALPBT"] = 0;
                                    }
                                    else
                                    {
                                        dr["MPC_ORD_ALPBT"] = Convert.ToInt16(rowx["MPC_ORD_ALPBT"].ToString());
                                    }
                                }
                            }
                            else
                            {
                                dr["MPC_ORD_ALPBT"] = 0;
                            }
                            param1.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable("", "");
                        dr = param1.NewRow();
                        dr["saleType"] = "OTH";
                        dr["MPC_ORD_ALPBT"] = 0;
                        if (result.Rows.Count >= 0)
                        {
                            dr["SerialFL"] = SerFL;
                        }
                        dr["USER"] = Session["UserID"].ToString();
                        param1.Rows.Add(dr);
                    }
                }

                MST_ITM1 = bsObj.CHNLSVC.Sales.GetItemCode(row["ITH_COM"].ToString(), row["ITS_ITM_CD"].ToString());
                MST_ITM_STUS1 = bsObj.CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());

                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = MST_ITM.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    dr["MI_ITM_UOM"] = row1["MI_ITM_UOM"].ToString();
                    MST_ITM.Rows.Add(dr);
                }
                foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                {
                    dr = MST_ITM_STUS.NewRow();
                    dr["MIS_CD"] = row2["MIS_CD"].ToString();
                    dr["MIS_DESC"] = row2["MIS_DESC"].ToString();
                    MST_ITM_STUS.Rows.Add(dr);
                }

            }  

            DataTable param = new DataTable();
            param.Columns.Add("isCnote", typeof(Int16));
            if (isCredit == 1)
            {
                dr = param.NewRow();
                dr["isCnote"] = 1;
                param.Rows.Add(dr);
            }
            else
            {
                dr = param.NewRow();
                dr["isCnote"] = 0;
                param.Rows.Add(dr);
            }

            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            param1 = param1.DefaultView.ToTable(true);

            if (_reportName == "Outward_Docs.rpt")
            {
                _outdoc.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                //_outdoc.Database.Tables["DISPOSAL_ENTRY"].SetDataSource(DISPOSAL_ENTRY);
                _outdoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdoc.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdoc.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdoc.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdoc.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdoc.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdoc.Database.Tables["param1"].SetDataSource(param1);
                _outdoc.Database.Tables["param"].SetDataSource(param);
                _outdoc.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

                foreach (object repOp in _outdoc.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdoc.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdoc.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "invoice_date")
                        {
                            ReportDocument subRepDoc = _outdoc.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdoc.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdoc.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdoc.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
            }

            if (_reportName == "Outward_Docs_DO.rpt")
            {
                
                _outdocDO.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocDO.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocDO.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocDO.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocDO.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocDO.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocDO.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocDO.Database.Tables["param1"].SetDataSource(param1);
                _outdocDO.Database.Tables["param"].SetDataSource(param);
                _outdocDO.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
                //       _outdocDO.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                foreach (object repOp in _outdocDO.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocDO.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "invoice_date")
                        {
                            ReportDocument subRepDoc = _outdocDO.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocDO.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocDO.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocDO.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocDO.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
            }

            if (_reportName == "Outward_Docs_DO_ABE.rpt")
            {
                _outdocDO_ABE.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocDO_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocDO_ABE.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocDO_ABE.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocDO_ABE.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocDO_ABE.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocDO_ABE.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocDO_ABE.Database.Tables["param1"].SetDataSource(param1);
                _outdocDO_ABE.Database.Tables["param"].SetDataSource(param);
                _outdocDO_ABE.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
                _outdocDO_ABE.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                foreach (object repOp in _outdocDO_ABE.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocDO_ABE.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "invoice_date")
                        {
                            ReportDocument subRepDoc = _outdocDO_ABE.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocDO_ABE.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocDO_ABE.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocDO_ABE.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocDO_ABE.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
                _outdocDO_ABE.SetParameterValue("USER", Session["UserID"].ToString());
            }

            if (_reportName == "Outward_Docs_Full_ABE_PRN.rpt")
            {
                _outdocfull_ABE_PRN.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocfull_ABE_PRN.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocfull_ABE_PRN.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocfull_ABE_PRN.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocfull_ABE_PRN.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocfull_ABE_PRN.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocfull_ABE_PRN.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocfull_ABE_PRN.Database.Tables["param1"].SetDataSource(param1);
                _outdocfull_ABE_PRN.Database.Tables["param"].SetDataSource(param);
                _outdocfull_ABE_PRN.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

                foreach (object repOp in _outdocfull_ABE_PRN.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocfull_ABE_PRN.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocfull_ABE_PRN.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocfull_ABE_PRN.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocfull_ABE_PRN.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocfull_ABE_PRN.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
                _outdocfull_ABE_PRN.SetParameterValue("Supplier", Session["Suppler"].ToString());
            }

            if (_reportName == "Outward_Docs_Full.rpt")
            {
                _outdocfull.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocfull.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocfull.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocfull.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocfull.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocfull.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocfull.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocfull.Database.Tables["param1"].SetDataSource(param1);
                _outdocfull.Database.Tables["param"].SetDataSource(param);
                _outdocfull.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

                foreach (object repOp in _outdocfull.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocfull.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocfull.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocfull.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocfull.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocfull.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
            }

            if (_reportName == "Outward_Docs_Full_ABE_OUTN.rpt")
            {
                _outdocfull_N.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocfull_N.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocfull_N.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocfull_N.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocfull_N.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocfull_N.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocfull_N.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocfull_N.Database.Tables["param1"].SetDataSource(param1);
                _outdocfull_N.Database.Tables["param"].SetDataSource(param);
                _outdocfull_N.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

                foreach (object repOp in _outdocfull_N.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocfull_N.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocfull_N.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocfull_N.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocfull_N.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocfull_N.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
            }

            if (_reportName == "Outward_Docs_Full_ABE_OUTP.rpt")
            {
                _outdocfull_P.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocfull_P.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocfull_P.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocfull_P.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocfull_P.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocfull_P.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocfull_P.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocfull_P.Database.Tables["param1"].SetDataSource(param1);
                _outdocfull_P.Database.Tables["param"].SetDataSource(param);
                _outdocfull_P.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

                foreach (object repOp in _outdocfull_P.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocfull_P.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocfull_P.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocfull_P.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocfull_P.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocfull_P.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
            }

            if (_reportName == "Outward_Docs_Full_ABE.rpt")
            {
                _outdocfull_AEB.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocfull_AEB.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocfull_AEB.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocfull_AEB.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocfull_AEB.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocfull_AEB.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocfull_AEB.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocfull_AEB.Database.Tables["param1"].SetDataSource(param1);
                _outdocfull_AEB.Database.Tables["param"].SetDataSource(param);
                _outdocfull_AEB.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

                foreach (object repOp in _outdocfull_AEB.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocfull_AEB.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocfull_AEB.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocfull_AEB.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocfull_AEB.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocfull_AEB.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
            }

            if (_reportName == "Outward_Docs_Full__AEC.rpt")
            {
                _outdocfull_AEC.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocfull_AEC.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocfull_AEC.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocfull_AEC.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocfull_AEC.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocfull_AEC.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocfull_AEC.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocfull_AEC.Database.Tables["param1"].SetDataSource(param1);
                _outdocfull_AEC.Database.Tables["param"].SetDataSource(param);
                _outdocfull_AEC.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

                foreach (object repOp in _outdocfull_AEC.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocfull_AEC.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocfull_AEC.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocfull_AEC.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocfull_AEC.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocfull_AEC.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
                _outdocfull_AEC.SetParameterValue("USER", Session["UserID"].ToString());
            }
            //Inward
            if (_reportName == "Outward_Docs_Full_IN_SGL_.rpt")
            {
                _outdocfull_IN_SGL.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocfull_IN_SGL.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocfull_IN_SGL.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocfull_IN_SGL.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocfull_IN_SGL.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocfull_IN_SGL.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocfull_IN_SGL.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocfull_IN_SGL.Database.Tables["param1"].SetDataSource(param1);
                _outdocfull_IN_SGL.Database.Tables["param"].SetDataSource(param);
                _outdocfull_IN_SGL.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

                foreach (object repOp in _outdocfull_IN_SGL.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocfull_IN_SGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocfull_IN_SGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocfull_IN_SGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocfull_IN_SGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocfull_IN_SGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
            }

          


            //OutWard
            if (_reportName == "Outward_Docs_Full_SGL.rpt")
            {
                _outdocfull_OUT_SGL.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocfull_OUT_SGL.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocfull_OUT_SGL.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocfull_OUT_SGL.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocfull_OUT_SGL.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocfull_OUT_SGL.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocfull_OUT_SGL.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocfull_OUT_SGL.Database.Tables["param1"].SetDataSource(param1);
                _outdocfull_OUT_SGL.Database.Tables["param"].SetDataSource(param);
                _outdocfull_OUT_SGL.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

                foreach (object repOp in _outdocfull_OUT_SGL.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocfull_OUT_SGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocfull_OUT_SGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocfull_OUT_SGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocfull_OUT_SGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocfull_OUT_SGL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
            }

            if (_reportName == "Outward_Docs_Full_ARL.rpt")
            {
                VIW_INV_MOVEMENT_SERIAL.Columns.Add("case_qty", typeof(System.Decimal));
                VIW_INV_MOVEMENT_SERIAL.Columns.Add("VEHICLENO", typeof(System.String));
                DataTable caseqty = new DataTable();
                caseqty = bsObj.CHNLSVC.Sales.GetCaseQty(docNo);
                DataTable vehicleno = new DataTable();
                vehicleno = bsObj.CHNLSVC.Sales.GetVehicleNo(docNo);
                foreach (DataRow caserow in caseqty.Rows)
                {
                    if (caserow["iti_uom_qty"] != DBNull.Value)
                    {
                        foreach (DataRow serrow in VIW_INV_MOVEMENT_SERIAL.Rows)
                        {
                            if ((caserow["iti_itm_cd"].ToString() == serrow["ITS_ITM_CD"].ToString()) && (caserow["iti_itm_stus"].ToString() == serrow["ITS_ITM_STUS"].ToString()))
                            {

                                if (caserow["iti_itm_cd"].ToString() == "497/000")
                                {
                                    Debug.Print("adsds");
                                }
                                serrow["case_qty"] = Convert.ToDecimal(caserow["iti_uom_qty"]) * Convert.ToDecimal(serrow["QTY"]);
                            }
                        }
                    }
                }

                foreach (DataRow caserow in vehicleno.Rows)
                {
                    if (caserow["itrn_ref_no"] != DBNull.Value)
                    {
                        foreach (DataRow serrow in VIW_INV_MOVEMENT_SERIAL.Rows)
                        {
                            serrow["VEHICLENO"] = caserow["itrn_ref_no"].ToString();

                        }
                    }
                }

                _outdocfull_Arl.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocfull_Arl.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocfull_Arl.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocfull_Arl.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocfull_Arl.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocfull_Arl.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocfull_Arl.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocfull_Arl.Database.Tables["param1"].SetDataSource(param1);
                _outdocfull_Arl.Database.Tables["param"].SetDataSource(param);
                _outdocfull_Arl.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);





                foreach (object repOp in _outdocfull_Arl.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocfull_Arl.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocfull_Arl.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocfull_Arl.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocfull_Arl.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocfull_Arl.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
            }

            if (_reportName == "Outward_Docs_Full_ARL_EXP_Details.rpt")
            {
                VIW_INV_MOVEMENT_SERIAL = bsObj.CHNLSVC.Sales.GetMovementSerials_WithExpireDates(docNo);
                VIW_INV_MOVEMENT_SERIAL.Columns.Add("case_qty", typeof(System.Decimal));
                VIW_INV_MOVEMENT_SERIAL.Columns.Add("VEHICLENO", typeof(System.String));
                DataTable caseqty = new DataTable();
                caseqty = bsObj.CHNLSVC.Sales.GetCaseQty(docNo);
                DataTable vehicleno = new DataTable();
                vehicleno = bsObj.CHNLSVC.Sales.GetVehicleNo(docNo);
                foreach (DataRow caserow in caseqty.Rows)
                {
                    if (caserow["iti_uom_qty"] != DBNull.Value)
                    {
                        foreach (DataRow serrow in VIW_INV_MOVEMENT_SERIAL.Rows)
                        {
                            if (caserow["iti_itm_cd"].ToString() == serrow["ITS_ITM_CD"].ToString())
                            {
                                serrow["case_qty"] = caserow["iti_uom_qty"].ToString();
                            }
                        }
                    }
                }

                foreach (DataRow caserow in vehicleno.Rows)
                {
                    if (caserow["itrn_ref_no"] != DBNull.Value)
                    {
                        foreach (DataRow serrow in VIW_INV_MOVEMENT_SERIAL.Rows)
                        {
                            serrow["VEHICLENO"] = caserow["itrn_ref_no"].ToString();

                        }
                    }
                }

                _outdocfull_Arl_Exp.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocfull_Arl_Exp.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocfull_Arl_Exp.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocfull_Arl_Exp.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocfull_Arl_Exp.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocfull_Arl_Exp.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocfull_Arl_Exp.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocfull_Arl_Exp.Database.Tables["param1"].SetDataSource(param1);
                _outdocfull_Arl_Exp.Database.Tables["param"].SetDataSource(param);
                _outdocfull_Arl_Exp.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);





                foreach (object repOp in _outdocfull_Arl_Exp.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocfull_Arl_Exp.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocfull_Arl_Exp.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocfull_Arl_Exp.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocfull_Arl_Exp.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocfull_Arl_Exp.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
            }

            if (_reportName == "Outward_Docs_AODOUTNEW.rpt")
            {
                _outdocfull_auto.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocfull_auto.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocfull_auto.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocfull_auto.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocfull_auto.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocfull_auto.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocfull_auto.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocfull_auto.Database.Tables["param1"].SetDataSource(param1);
                _outdocfull_auto.Database.Tables["param"].SetDataSource(param);
                _outdocfull_auto.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
                _outdocfull_auto.Database.Tables["Req_data"].SetDataSource(Reqdetails);

                foreach (object repOp in _outdocfull_auto.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocfull_auto.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocfull_auto.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocfull_auto.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocfull_auto.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocfull_auto.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
            }

            //Tharindu 2018-02-01
            if (_reportName == "Outward_Docs_Full_AAL.rpt")
            {
                _outdocfull_AAL.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                _outdocfull_AAL.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _outdocfull_AAL.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                _outdocfull_AAL.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                _outdocfull_AAL.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                _outdocfull_AAL.Database.Tables["mst_com"].SetDataSource(mst_com);
                _outdocfull_AAL.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                _outdocfull_AAL.Database.Tables["param1"].SetDataSource(param1);
                _outdocfull_AAL.Database.Tables["param"].SetDataSource(param);
                _outdocfull_AAL.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

                foreach (object repOp in _outdocfull_AAL.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptCustomer")
                        {
                            ReportDocument subRepDoc = _outdocfull_AAL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        if (_cs.SubreportName == "warr")
                        {
                            ReportDocument subRepDoc = _outdocfull_AAL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (sat_hdr.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "OtherDel")
                        {
                            ReportDocument subRepDoc = _outdocfull_AAL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                            subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //if (tblComDate.Rows.Count > 0)
                        //{
                        if (_cs.SubreportName == "WarrComDate")
                        {
                            ReportDocument subRepDoc = _outdocfull_AAL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        //}
                        if (_cs.SubreportName == "Vou_Det")
                        {
                            ReportDocument subRepDoc = _outdocfull_AAL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                        //}

                    }
                }
            }
        }

        public void movementCostDetail(InvReportPara _objRepPara)
        {
            //kapila 
            DataTable param = new DataTable();
            tmp_user_pc = new DataTable();
            DataTable PROC_INVENTORY_BALANCE = new DataTable();
            DataRow dr;


            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV_BAL = new DataTable();
                    TMP_INV_BAL = bsObj.CHNLSVC.MsgPortal.PrintMoveCostDetailReport(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportDocType, _objRepPara._GlbReportDirection, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel, _objRepPara._GlbReportDoc);

                    PROC_INVENTORY_BALANCE.Merge(TMP_INV_BAL);


                }
            }


            DataTable INV_SER = new DataTable();
            // if (_objRepPara._GlbReportWithSerial == 1) { INV_SER = CHNLSVC.Inventory.GetSerialBalance(); }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("itemstatus", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("withcost", typeof(Int16));
            param.Columns.Add("withserial", typeof(Int16));
            param.Columns.Add("withstatus", typeof(Int16));
            param.Columns.Add("isjob", typeof(Int32));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["fromdate"] = _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["channel"] = _objRepPara._GlbReportChannel == "" ? "ALL" : _objRepPara._GlbReportChannel;
            dr["itemstatus"] = _objRepPara._GlbReportItemStatus == "" ? "ALL" : _objRepPara._GlbReportItemStatus;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["withcost"] = _objRepPara._GlbReportWithCost;
            dr["withserial"] = _objRepPara._GlbReportWithSerial;
            dr["withstatus"] = _objRepPara._GlbReportWithStatus;
            if (_objRepPara._GlbReportJobStatus == "Y")
            {
                dr["isjob"] = 1;
            }
            else
            {
                dr["isjob"] = 0;
            }
            param.Rows.Add(dr);

            _movCostDet.Database.Tables["MOVE_COST_DET"].SetDataSource(PROC_INVENTORY_BALANCE);
            // if (_objRepPara._GlbReportWithSerial == 1) { _invBalCst.Database.Tables["BAL_SERIAL"].SetDataSource(INV_SER); }
            _movCostDet.Database.Tables["param"].SetDataSource(param);

        }

        public void ItemWiseTransDetListing(InvReportPara _objRepPara)
        {

            string docNo = default(string);
            docNo = _objRepPara._GlbReportDoc;
            tmp_user_pc = new DataTable();

            DataTable ITEM_TRANS = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM1 = new DataTable();

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("TOdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));


            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["fromdate"] = _objRepPara._GlbReportFromDate;
            dr["TOdate"] = _objRepPara._GlbReportToDate;
            dr["todate"] = _objRepPara._GlbReportToDate;
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["comp"] = _objRepPara._GlbReportComp;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["compaddr"] = _objRepPara._GlbReportCompAddr;
            dr["docType"] = _objRepPara._GlbReportType == "" ? "ALL" : _objRepPara._GlbReportType;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["Direct"] = _objRepPara._GlbReportDirection == "" ? "ALL" : _objRepPara._GlbReportDirection;



            param.Rows.Add(dr);

            ITEM_TRANS.Clear();


            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_SER = new DataTable();
                    // TMP_SER = bsObj.CHNLSVC.MsgPortal.PrintTransDetListReport(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel);
                    // TMP_SER = bsObj.CHNLSVC.MsgPortal.ProcessInventoryStatement_SCM(_objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbUserID, drow["tpl_pc"].ToString(), drow["tpl_com"].ToString(), _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3);
                    TMP_SER = bsObj.CHNLSVC.MsgPortal.PrintTransDetListReport(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromDate.Date, _objRepPara._GlbReportToDate.Date, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel);
                    ITEM_TRANS.Merge(TMP_SER);


                }
            }

            MST_LOC = bsObj.CHNLSVC.Inventory.Get_all_LocationsTable(_objRepPara._GlbReportCompCode);
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            _itmwiseTransList.Database.Tables["TRANS_DET_LIST"].SetDataSource(ITEM_TRANS);
            _itmwiseTransList.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _itmwiseTransList.Database.Tables["param"].SetDataSource(param);
            _itmwiseTransList.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            //_invStsTr3.Database.Tables["PROC_INVENTORY_STATEMENT"].SetDataSource(ITEM_TRANS);
            //_invStsTr3.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

            //_invStsTr3.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            //_invStsTr3.Database.Tables["param"].SetDataSource(param);

        }

        public void SerialAgeReport(InvReportPara _objRepPara)
        {

            string docNo = default(string);
            docNo = _objRepPara._GlbReportDoc;
            tmp_user_pc = new DataTable();

            DataTable BAL_SERIAL = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM1 = new DataTable();

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));

            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));
            param.Columns.Add("fromage", typeof(double));
            param.Columns.Add("toage", typeof(double));
            param.Columns.Add("withcost", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["fromdate"] = _objRepPara._GlbReportAsAtDate;

            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["comp"] = _objRepPara._GlbReportComp;
            dr["compaddr"] = _objRepPara._GlbReportCompAddr;
            dr["docType"] = _objRepPara._GlbReportType == "" ? "ALL" : _objRepPara._GlbReportType;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["Direct"] = _objRepPara._GlbReportDirection == "" ? "ALL" : _objRepPara._GlbReportDirection;
            dr["fromage"] = _objRepPara._GlbReportFromAge;
            dr["toage"] = _objRepPara._GlbReportToAge;
            dr["withcost"] = _objRepPara._GlbReportWithCost;

            param.Rows.Add(dr);

            BAL_SERIAL.Clear();


            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_SER = new DataTable();
                    TMP_SER = bsObj.CHNLSVC.MsgPortal.SerialAgeReport_SCM(Convert.ToDateTime(_objRepPara._GlbReportAsAtDate).Date, _objRepPara._GlbUserID, drow["tpl_com"].ToString(), _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemStatus, drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromAge, _objRepPara._GlbReportToAge);

                    BAL_SERIAL.Merge(TMP_SER);


                }
            }

            MST_LOC = bsObj.CHNLSVC.Inventory.Get_all_LocationsTable(_objRepPara._GlbReportCompCode);
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            _serAge.Database.Tables["BAL_SERIAL"].SetDataSource(BAL_SERIAL);
            _serAge.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _serAge.Database.Tables["param"].SetDataSource(param);
            _serAge.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

        }

        public void inventoryBalanceSerial_Asat(InvReportPara _objRepPara)
        {
            // kapila 20/12/2015
            DataTable param = new DataTable();
            DataTable PROC_INVENTORY_BALANCE = new DataTable();
            tmp_user_pc = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV = new DataTable();
                    TMP_INV = bsObj.CHNLSVC.MsgPortal.GetStockBalanceWithSerial_Asat_SCM(_objRepPara._GlbUserID, _objRepPara._GlbReportChannel, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemStatus, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportWithCost, Convert.ToDateTime(_objRepPara._GlbReportAsAtDate).Date, _objRepPara._GlbReportWithSerial, _objRepPara._GlbReportWithStatus, drow["tpl_pc"].ToString(), drow["tpl_com"].ToString());
                    PROC_INVENTORY_BALANCE.Merge(TMP_INV);


                }
            }

            // DataTable INV_SER = CHNLSVC.Inventory.GetSerialBalance_Asat(Convert.ToDateTime(_objRepPara._GlbReportAsAtDate).Date);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("itemstatus", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("withcost", typeof(Int16));
            param.Columns.Add("withserial", typeof(Int16));
            param.Columns.Add("withstatus", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["channel"] = _objRepPara._GlbReportChannel == "" ? "ALL" : _objRepPara._GlbReportChannel;
            dr["itemstatus"] = _objRepPara._GlbReportItemStatus == "" ? "ALL" : _objRepPara._GlbReportItemStatus;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["withcost"] = _objRepPara._GlbReportWithCost;
            dr["withserial"] = _objRepPara._GlbReportWithSerial;
            dr["withstatus"] = _objRepPara._GlbReportWithStatus;
            param.Rows.Add(dr);

            _invBalSrlAsat.Database.Tables["STOCK_BAL"].SetDataSource(PROC_INVENTORY_BALANCE);
            //  _invBalSrlAsat.Database.Tables["BAL_SERIAL"].SetDataSource(INV_SER);
            _invBalSrlAsat.Database.Tables["param"].SetDataSource(param);

        }
        //kapila
        public void inventoryBalanceWithCost(InvReportPara _objRepPara)
        {
            //kapila 20/12/2015
            DataTable param = new DataTable();
            DataTable PROC_INVENTORY_BALANCE = new DataTable();
            tmp_user_pc = new DataTable();
            DataRow dr;


            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV_BAL = new DataTable();
                    TMP_INV_BAL = bsObj.CHNLSVC.MsgPortal.GetStockBalanceWithCost_SCM(_objRepPara._GlbUserID, _objRepPara._GlbReportChannel, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemStatus, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportWithCost, Convert.ToDateTime(_objRepPara._GlbReportAsAtDate).Date, _objRepPara._GlbReportWithSerial, _objRepPara._GlbReportWithStatus, drow["tpl_pc"].ToString(), drow["tpl_com"].ToString());
                    // TMP_INV_BAL = bsObj.CHNLSVC.Inventory.GetStockBalanceWithCost(_objRepPara._GlbUserID, _objRepPara._GlbReportChannel, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemStatus, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportWithCost, Convert.ToDateTime(_objRepPara._GlbReportAsAtDate).Date, _objRepPara._GlbReportWithSerial, _objRepPara._GlbReportWithStatus, drow["tpl_pc"].ToString(), drow["tpl_com"].ToString());

                    PROC_INVENTORY_BALANCE.Merge(TMP_INV_BAL);


                }
            }


            DataTable INV_SER = new DataTable();
            // if (_objRepPara._GlbReportWithSerial == 1) { INV_SER = CHNLSVC.Inventory.GetSerialBalance(); }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("itemstatus", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("withcost", typeof(Int16));
            param.Columns.Add("withserial", typeof(Int16));
            param.Columns.Add("withstatus", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["channel"] = _objRepPara._GlbReportChannel == "" ? "ALL" : _objRepPara._GlbReportChannel;
            dr["itemstatus"] = _objRepPara._GlbReportItemStatus == "" ? "ALL" : _objRepPara._GlbReportItemStatus;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["withcost"] = _objRepPara._GlbReportWithCost;
            dr["withserial"] = _objRepPara._GlbReportWithSerial;
            dr["withstatus"] = _objRepPara._GlbReportWithStatus;
            param.Rows.Add(dr);

            _invBalCst.Database.Tables["STOCK_BAL"].SetDataSource(PROC_INVENTORY_BALANCE);
            // if (_objRepPara._GlbReportWithSerial == 1) { _invBalCst.Database.Tables["BAL_SERIAL"].SetDataSource(INV_SER); }
            _invBalCst.Database.Tables["param"].SetDataSource(param);

        }
        public void StockAgeReport(InvReportPara _objRepPara)
        {
            // kapila 16/12/2015

            string docNo = default(string);
            docNo = _objRepPara._GlbReportDoc;
            tmp_user_pc = new DataTable();


            DataTable BAL_SERIAL = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable rep_param = new DataTable();
            DataTable ref_rpt_param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM1 = new DataTable();


            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));

            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));
            param.Columns.Add("cost", typeof(double));// No of Days
            param.Columns.Add("showcost", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["fromdate"] = _objRepPara._GlbReportAsAtDate;

            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["comp"] = _objRepPara._GlbReportComp;
            dr["compaddr"] = _objRepPara._GlbReportCompAddr;
            dr["docType"] = _objRepPara._GlbReportType == "" ? "ALL" : _objRepPara._GlbReportType;
            dr["Direct"] = _objRepPara._GlbReportDirection == "" ? "ALL" : _objRepPara._GlbReportDirection;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cost"] = _objRepPara._GlbReportnoofDays;
            dr["showcost"] = _objRepPara._GlbReportWithCost;


            param.Rows.Add(dr);

            rep_param.Clear();

            rep_param.Columns.Add("GRP_LOC", typeof(Int16));
            rep_param.Columns.Add("GRP_ITEM", typeof(Int16));
            rep_param.Columns.Add("GRP_ITM_STUS", typeof(Int16));

            dr = rep_param.NewRow();
            dr["GRP_LOC"] = _objRepPara._GlbReportGroupDOLoc;
            dr["GRP_ITEM"] = _objRepPara._GlbReportGroupItemCode;
            dr["GRP_ITM_STUS"] = _objRepPara._GlbReportGroupItemStatus;

            rep_param.Rows.Add(dr);




            BAL_SERIAL.Clear();


            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow row in tmp_user_pc.Rows)
                {
                    DataTable TMP_SER = new DataTable();
                    TMP_SER = bsObj.CHNLSVC.MsgPortal.AgeReport_SCM(Convert.ToDateTime(_objRepPara._GlbReportAsAtDate).Date, _objRepPara._GlbUserID, row["tpl_com"].ToString(), _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemStatus, row["tpl_pc"].ToString());

                    BAL_SERIAL.Merge(TMP_SER);


                }
            }




            ref_rpt_param = bsObj.CHNLSVC.MsgPortal.get_ref_rpt_para("COM", _objRepPara._GlbReportCompCode);
            MST_LOC = bsObj.CHNLSVC.Inventory.Get_all_LocationsTable(_objRepPara._GlbReportCompCode);
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);


            _stkAge.Database.Tables["GLB_STK_AGE"].SetDataSource(BAL_SERIAL);
            _stkAge.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _stkAge.Database.Tables["param"].SetDataSource(param);
            _stkAge.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _stkAge.Database.Tables["REP_PARA"].SetDataSource(rep_param);
            _stkAge.Database.Tables["REF_RPT_PARA"].SetDataSource(ref_rpt_param);

        }

        public void Current_Stock_Balance(InvReportPara _objRepPara)
        {
            // kapila 
            DataTable glbTable = new DataTable();
            DataTable param = new DataTable();

            DataRow dr;

            glbTable.Clear();
            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_Table = new DataTable();
                    tmp_Table = bsObj.CHNLSVC.MsgPortal.GetStockBalanceCurrent_SCM(_objRepPara._GlbUserID, _objRepPara._GlbReportChannel, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemStatus, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportWithCost, _objRepPara._GlbReportWithSerial, drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportWithRCC, _objRepPara._GlbReportWithJob, _objRepPara._GlbReportWithGIT);
                    glbTable.Merge(tmp_Table);
                }
            }

            DataTable INV_SER = new DataTable();
            if (_objRepPara._GlbReportWithSerial == 1)
            {
                if (glbTable.Rows.Count > 0)
                {
                    foreach (DataRow drow in glbTable.Rows)
                    {
                        DataTable TEMP_SER = new DataTable();
                        TEMP_SER = bsObj.CHNLSVC.MsgPortal.GetSerialBalance_Curr_SCM(drow["com_code"].ToString(), drow["loc_code"].ToString(), drow["item_code"].ToString(), drow["item_status"].ToString(), _objRepPara._GlbReportWithRCC);
                        INV_SER.Merge(TEMP_SER);
                    }
                }
                else
                {
                    DataTable TEMP_SER = new DataTable();
                    TEMP_SER = bsObj.CHNLSVC.MsgPortal.GetSerialBalance_Curr_SCM(_objRepPara._GlbUserComCode, "", "", "", _objRepPara._GlbReportWithRCC);
                    INV_SER.Merge(TEMP_SER);
                }

            }
            else
            {
                DataTable TEMP_SER = new DataTable();
                TEMP_SER = bsObj.CHNLSVC.MsgPortal.GetSerialBalance_Curr_SCM(_objRepPara._GlbUserComCode, "", "", "", _objRepPara._GlbReportWithRCC);
                INV_SER.Merge(TEMP_SER);
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("itemstatus", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("withcost", typeof(Int16));
            param.Columns.Add("withserial", typeof(Int16));
            param.Columns.Add("isjob", typeof(Int32));


            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["channel"] = _objRepPara._GlbReportChannel == "" ? "ALL" : _objRepPara._GlbReportChannel;
            dr["itemstatus"] = _objRepPara._GlbReportItemStatus == "" ? "ALL" : _objRepPara._GlbReportItemStatus;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["withcost"] = _objRepPara._GlbReportWithCost;
            dr["withserial"] = _objRepPara._GlbReportWithSerial;
            if (_objRepPara._GlbReportJobStatus == "Y")
            {
                dr["isjob"] = 1;
            }
            else
            {
                dr["isjob"] = 0;
            }


            param.Rows.Add(dr);

            if (_objRepPara._GlbReportName == "Stock_BalanceCatWise.rpt")
            {
                _curStkBalCat.Database.Tables["STOCK_BAL"].SetDataSource(glbTable);
                _curStkBalCat.Database.Tables["SERIAL_BAL"].SetDataSource(INV_SER);
                _curStkBalCat.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _curStkBal.Database.Tables["STOCK_BAL"].SetDataSource(glbTable);
                _curStkBal.Database.Tables["SERIAL_BAL"].SetDataSource(INV_SER);
                _curStkBal.Database.Tables["param"].SetDataSource(param);
            }

        }


        public void purchaseOrderSummery(InvReportPara _objRepPara)
        {
            // Wimal  @ 21/12/2015
            DataTable MST_COM = new DataTable();
            DataTable purchaseOrderSumm = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));

            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("doc_no", typeof(string));
            param.Columns.Add("doc_type", typeof(string));


            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = _objRepPara._GlbReportComp;
            dr["company_name"] = _objRepPara._GlbReportCompCode;
            dr["companies"] = _objRepPara._GlbReportCompanies;

            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["doc_no"] = _objRepPara._GlbReportDoc == "" ? "ALL" : _objRepPara._GlbReportDoc.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["doc_type"] = _objRepPara._GlbReportDocType == "" ? "ALL" : _objRepPara._GlbReportDocType.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            param.Rows.Add(dr);

            //purchaseOrderSumm.Clear();
            //purchaseOrderSumm = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderSummery(_objRepPara._GlbReportComp, "", "",,,, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate);

            purchaseOrderSumm.Clear();
            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(_objRepPara._GlbReportComp, _objRepPara._GlbUserID);
            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_Table = new DataTable();
                    //tmp_Table = bsObj.CHNLSVC.MsgPortal.GetStockBalanceCurrent_SCM(_objRepPara._GlbUserID, _objRepPara._GlbReportChannel, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemStatus, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportWithCost, _objRepPara._GlbReportWithSerial, _objRepPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepPara._GlbReportWithRCC, _objRepPara._GlbReportWithJob, _objRepPara._GlbReportWithGIT);
                    tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderSummery(_objRepPara._GlbReportComp, drow["tpl_pc"].ToString(), _objRepPara._GlbEntryType, _objRepPara._GlbReportSupplier, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportDocType, _objRepPara._GlbReportDoc);
                    purchaseOrderSumm.Merge(tmp_Table);

                }
            }
            else
            {   
                //====Added By Udesh 19/Oct/2018=====
                DataTable tmp_Table = new DataTable();
                tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderSummery("", "", _objRepPara._GlbEntryType, _objRepPara._GlbReportSupplier, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportDocType, _objRepPara._GlbReportDoc);
                purchaseOrderSumm.Merge(tmp_Table);
                //===================================
            }

            _purOrdSumm_summ.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _purOrdSumm_summ.Database.Tables["dsPurchaseOrderSummery"].SetDataSource(purchaseOrderSumm);
            _purOrdSumm_summ.Database.Tables["param"].SetDataSource(param);

            //DataTable tmp_Table = new DataTable();
            ////tmp_Table = bsObj.CHNLSVC.MsgPortal.GetStockBalanceCurrent_SCM(_objRepPara._GlbUserID, _objRepPara._GlbReportChannel, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemStatus, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportWithCost, _objRepPara._GlbReportWithSerial, _objRepPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepPara._GlbReportWithRCC, _objRepPara._GlbReportWithJob, _objRepPara._GlbReportWithGIT);
            //tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderSummery(_objRepPara._GlbReportComp, _objRepPara._GlbEntryType, _objRepPara._GlbReportSupplier, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportDocType, _objRepPara._GlbReportDoc);
            //purchaseOrderSumm.Merge(tmp_Table);

            //_purOrdSumm_summ.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            //_purOrdSumm_summ.Database.Tables["dsPurchaseOrderSummery"].SetDataSource(purchaseOrderSumm);
            //_purOrdSumm_summ.Database.Tables["param"].SetDataSource(param);
        }

        public void purchaseOrderSummery_Detail(InvReportPara _objRepPara)
        {
            // Wimal  @ 21/12/2015
            DataTable MST_COM = new DataTable();
            DataTable purchaseOrderSumm = new DataTable();
            DataTable param = new DataTable();

            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));

            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("doc_no", typeof(string));
            param.Columns.Add("doc_type", typeof(string));


            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = _objRepPara._GlbReportComp;
            dr["company_name"] = _objRepPara._GlbReportCompCode;
            dr["companies"] = _objRepPara._GlbReportCompanies;

            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["doc_no"] = _objRepPara._GlbReportDoc == "" ? "ALL" : _objRepPara._GlbReportDoc.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["doc_type"] = _objRepPara._GlbReportDocType == "" ? "ALL" : _objRepPara._GlbReportDocType.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            param.Rows.Add(dr);



            purchaseOrderSumm.Clear();
            //DataTable tmp_user_pc = new DataTable();
            //tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(_objRepPara._GlbReportComp, _objRepPara._GlbUserID);
            //if (tmp_user_pc.Rows.Count > 0)
            //{
            //    foreach (DataRow drow in tmp_user_pc.Rows)
            //    {
            //        DataTable tmp_Table = new DataTable();
            //        //tmp_Table = bsObj.CHNLSVC.MsgPortal.GetStockBalanceCurrent_SCM(_objRepPara._GlbUserID, _objRepPara._GlbReportChannel, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemStatus, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportWithCost, _objRepPara._GlbReportWithSerial, _objRepPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepPara._GlbReportWithRCC, _objRepPara._GlbReportWithJob, _objRepPara._GlbReportWithGIT);
            //        tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderSummery(_objRepPara._GlbReportComp, drow["tpl_pc"].ToString(), _objRepPara._GlbReportSupplier, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportDocType, _objRepPara._GlbReportDoc);
            //        purchaseOrderSumm.Merge(tmp_Table);
            //    }
            //}

            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(_objRepPara._GlbReportComp, _objRepPara._GlbUserID);
            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_Table = new DataTable();
                    //tmp_Table = bsObj.CHNLSVC.MsgPortal.GetStockBalanceCurrent_SCM(_objRepPara._GlbUserID, _objRepPara._GlbReportChannel, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemStatus, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportWithCost, _objRepPara._GlbReportWithSerial, _objRepPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepPara._GlbReportWithRCC, _objRepPara._GlbReportWithJob, _objRepPara._GlbReportWithGIT);
                    tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderSummery(_objRepPara._GlbReportComp, drow["tpl_pc"].ToString(), _objRepPara._GlbEntryType, _objRepPara._GlbReportSupplier, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportDocType, _objRepPara._GlbReportDoc);
                    purchaseOrderSumm.Merge(tmp_Table);


                }
            }
            _purOrdSumm_dtl.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _purOrdSumm_dtl.Database.Tables["dsPurchaseOrderSummery"].SetDataSource(purchaseOrderSumm);
            _purOrdSumm_dtl.Database.Tables["param"].SetDataSource(param);


            //-------------------------------Removed by Wimal @ 14/09/2016-------------------------- Start
            //DataTable tmp_Table = new DataTable();
            ////tmp_Table = bsObj.CHNLSVC.MsgPortal.GetStockBalanceCurrent_SCM(_objRepPara._GlbUserID, _objRepPara._GlbReportChannel, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemStatus, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportWithCost, _objRepPara._GlbReportWithSerial, _objRepPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepPara._GlbReportWithRCC, _objRepPara._GlbReportWithJob, _objRepPara._GlbReportWithGIT);
            //tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderSummery(_objRepPara._GlbReportComp, _objRepPara._GlbEntryType, _objRepPara._GlbReportSupplier, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportDocType, _objRepPara._GlbReportDoc);
            //purchaseOrderSumm.Merge(tmp_Table);

            //_purOrdSumm_dtl.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            //_purOrdSumm_dtl.Database.Tables["dsPurchaseOrderSummery"].SetDataSource(purchaseOrderSumm);
            //_purOrdSumm_dtl.Database.Tables["param"].SetDataSource(param);
            //-------------------------------Removed by Wimal @ 14/09/2016-------------------------- End 
        }

        public void pending_GRNPO(InvReportPara _objRepPara)
        {
            // Wimal  @ 21/12/2015
            DataTable MST_COM = new DataTable();
            DataTable purchaseOrderSumm = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));

            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("doc_no", typeof(string));
            param.Columns.Add("doc_type", typeof(string));
            param.Columns.Add("loc", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = _objRepPara._GlbReportComp;
            dr["company_name"] = _objRepPara._GlbReportCompCode;
            dr["companies"] = _objRepPara._GlbReportCompanies;

            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["doc_no"] = _objRepPara._GlbReportDoc == "" ? "ALL" : _objRepPara._GlbReportDoc.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["doc_type"] = _objRepPara._GlbReportDocType == "" ? "ALL" : _objRepPara._GlbReportDocType.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["loc"] = _objRepPara._GlbLocation;
            param.Rows.Add(dr);

            //purchaseOrderSumm.Clear();
            //purchaseOrderSumm = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderSummery(_objRepPara._GlbReportComp, "", "",,,, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate);

            purchaseOrderSumm.Clear();
            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(_objRepPara._GlbReportComp, _objRepPara._GlbUserID);
            DataTable tmp_Table = new DataTable();
            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrder_grnpening(_objRepPara._GlbReportComp, drow["tpl_pc"].ToString(), _objRepPara._GlbReportSupplier, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportDocType, _objRepPara._GlbReportDoc);
                    purchaseOrderSumm.Merge(tmp_Table);
                }
            }
            /*
            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_Table = new DataTable();
                    //tmp_Table = bsObj.CHNLSVC.MsgPortal.GetStockBalanceCurrent_SCM(_objRepPara._GlbUserID, _objRepPara._GlbReportChannel, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemStatus, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportWithCost, _objRepPara._GlbReportWithSerial, _objRepPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepPara._GlbReportWithRCC, _objRepPara._GlbReportWithJob, _objRepPara._GlbReportWithGIT);
                    tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrder_grnpening(_objRepPara._GlbReportComp, drow["tpl_pc"].ToString(), _objRepPara._GlbReportSupplier, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportDocType, _objRepPara._GlbReportDoc);
                    purchaseOrderSumm.Merge(tmp_Table);
                }
            }*/


            _pendingGRN.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _pendingGRN.Database.Tables["dsPurchaseOrderSummery"].SetDataSource(purchaseOrderSumm);
            _pendingGRN.Database.Tables["param"].SetDataSource(param);


            // // Wimal  @ 21/12/2015
            // DataTable MST_COM = new DataTable();
            // DataTable purchaseOrderSumm = new DataTable();
            // DataTable param = new DataTable();
            // DataRow dr;
            // MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            // param.Columns.Add("user", typeof(string));
            // param.Columns.Add("heading_1", typeof(string));
            // param.Columns.Add("period", typeof(string));
            // param.Columns.Add("company", typeof(string));
            // param.Columns.Add("company_name", typeof(string));

            // dr = param.NewRow();
            // dr["user"] = _objRepPara._GlbUserID;
            // dr["heading_1"] = _objRepPara._GlbReportHeading;
            // dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            // dr["company"] = _objRepPara._GlbReportComp;
            // dr["company_name"] = _objRepPara._GlbReportCompCode;
            // param.Rows.Add(dr);

            // purchaseOrderSumm.Clear();
            //// purchaseOrderSumm = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderSummery(_objRepPara._GlbUserComCode, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate);

            // _pendingGRN.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            // _pendingGRN.Database.Tables["dsPurchaseOrderSummery"].SetDataSource(purchaseOrderSumm);
            // _pendingGRN.Database.Tables["param"].SetDataSource(param);
        }
        public void Local_Purchase_Costing(InvReportPara _objRepPara)
        {
            // Wimal  @ 23/12/2015
            DataTable MST_COM = new DataTable();
            DataTable purchaseOrderSumm = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = _objRepPara._GlbReportComp;
            dr["company_name"] = _objRepPara._GlbReportCompCode;
            param.Rows.Add(dr);

            purchaseOrderSumm.Clear();
            //purchaseOrderSumm = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderSummery(_objRepPara._GlbUserComCode, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate);

            _localPurcaseCost.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _localPurcaseCost.Database.Tables["dsPurchaseOrderSummery"].SetDataSource(purchaseOrderSumm);
            _localPurcaseCost.Database.Tables["param"].SetDataSource(param);
        }

        public void purchaseOrderPrint(string com, string _docno)
        {
            // Wimal  @ 21/12/2015
            DataTable MST_COM = new DataTable();
            DataTable purchaseOrderSumm = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));

            dr = param.NewRow();
            dr["user"] = Session["UserID"];
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = BaseCls.GlbReportComp;
            dr["company_name"] = BaseCls.GlbReportCompCode;
            dr["companies"] = BaseCls.GlbReportCompanies;
            param.Rows.Add(dr);



            purchaseOrderSumm.Clear();
            DataTable tmp_user_pc = new DataTable();
            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderPrint_HDR(com, _docno);
            //purchaseOrderSumm.Merge(tmp_Table);

            //_PoPrint.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _PoPrint.Database.Tables["LOPUR_PURCHASE_ORDER_HEADER"].SetDataSource(tmp_Table);
            //_PoPrint.Database.Tables["param"].SetDataSource(param);
            //  purchaseOrderSumm = new DataTable();
            tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrder_Print_Dtl(com, _docno);

            //purchaseOrderSumm.Merge(tmp_Table);

            _PoPrint.Database.Tables["LOPUR_PURCHASE_ORDER_DETAILS"].SetDataSource(tmp_Table);


        }

        //

        public void purchaseOrderPrint_AAL(string com, string _docno)
        {
            // Wimal  @ 21/12/2015
            DataTable MST_COM = new DataTable();
            DataTable purchaseOrderSumm = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));

            dr = param.NewRow();
            dr["user"] = Session["UserID"];
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = BaseCls.GlbReportComp;
            dr["company_name"] = BaseCls.GlbReportCompCode;
            dr["companies"] = BaseCls.GlbReportCompanies;
            param.Rows.Add(dr);



            purchaseOrderSumm.Clear();
            DataTable tmp_user_pc = new DataTable();
            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderPrint_HDR(com, _docno);

            _PoPrint_AAL.Database.Tables["LOPUR_PURCHASE_ORDER_HEADER"].SetDataSource(tmp_Table);

            tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrder_Print_Dtl(com, _docno);

            _PoPrint_AAL.Database.Tables["LOPUR_PURCHASE_ORDER_DETAILS"].SetDataSource(tmp_Table);
        }

        //Added by Tharindu 2018-01-19
        public void purchaseOrderPrint_ABE(string com, string _docno)
        {
            // Wimal  @ 21/12/2015
            DataTable MST_COM = new DataTable();
            DataTable purchaseOrderSumm = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));

            dr = param.NewRow();
            dr["user"] = Session["UserID"];
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = BaseCls.GlbReportComp;
            dr["company_name"] = BaseCls.GlbReportCompCode;
            dr["companies"] = BaseCls.GlbReportCompanies;
            param.Rows.Add(dr);



            purchaseOrderSumm.Clear();
            DataTable tmp_user_pc = new DataTable();
            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderPrint_HDR(com, _docno);

            _PoPrint_ABE.Database.Tables["LOPUR_PURCHASE_ORDER_HEADER"].SetDataSource(tmp_Table);

            tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrder_Print_Dtl(com, _docno);

            _PoPrint_ABE.Database.Tables["LOPUR_PURCHASE_ORDER_DETAILS"].SetDataSource(tmp_Table);
        }

        public void purchaseOrderPrint_ARL(string com, string _docno)
        {
            // Wimal  @ 21/12/2015
            DataTable MST_COM = new DataTable();
            DataTable purchaseOrderSumm = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));

            dr = param.NewRow();
            dr["user"] = Session["UserID"];
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = BaseCls.GlbReportComp;
            dr["company_name"] = BaseCls.GlbReportCompCode;
            dr["companies"] = BaseCls.GlbReportCompanies;
            param.Rows.Add(dr);



            purchaseOrderSumm.Clear();
            DataTable tmp_user_pc = new DataTable();
            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.getpurchaseOrderPrint_HDR(com, _docno);
            //purchaseOrderSumm.Merge(tmp_Table);

            //_PoPrint.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _PoPrint_ARL.Database.Tables["LOPUR_PURCHASE_ORDER_HEADER"].SetDataSource(tmp_Table);
            //_PoPrint.Database.Tables["param"].SetDataSource(param);
            //  purchaseOrderSumm = new DataTable();
            DataTable tmp_Table2 = new DataTable();
            tmp_Table2 = bsObj.CHNLSVC.MsgPortal.getpurchaseOrder_Print_Dtl(com, _docno);

            //purchaseOrderSumm.Merge(tmp_Table);

            _PoPrint_ARL.Database.Tables["LOPUR_PURCHASE_ORDER_DETAILS"].SetDataSource(tmp_Table2);


        }

        public void currentGIT(InvReportPara _objRepPara)
        {
            // Wimal  @ 29/12/2015      Modified 08/06/2016     
            DataTable valueAddtion = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode; ;
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand;
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel;
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1;
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2;
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3;
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4;
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5;
            param.Rows.Add(dr);

            valueAddtion.Clear();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(_objRepPara._GlbReportComp, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow row in tmp_user_pc.Rows)
                {
                    DataTable TMP_SER = new DataTable();

                    //if (_objRepPara._GlbReportType == "CURR")
                    //{
                    TMP_SER = bsObj.CHNLSVC.MsgPortal.getGITReport_Current(_objRepPara._GlbReportAsAtDate.Date, _objRepPara._GlbReportComp, row["tpl_pc"].ToString(), _objRepPara._GlbReportOtherLoc, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5);
                    //}
                    //else
                    //{
                    //    TMP_SER = bsObj.CHNLSVC.MsgPortal.getGITReport_Asat(_objRepPara._GlbReportAsAtDate.Date, _objRepPara._GlbReportComp, row["tpl_pc"].ToString(), _objRepPara._GlbReportOtherLoc, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbUserID);
                    //}
                    valueAddtion.Merge(TMP_SER);
                }
            }

            DataTable SERIAL_DETAIL = new DataTable();

            if (_objRepPara._GlbGITWithSerials > 0)
            {
                foreach (DataRow row in valueAddtion.Rows)
                {
                    DataTable dtSerial = new DataTable();

                    dtSerial = bsObj.CHNLSVC.MsgPortal.GetGitSerialDetails(_objRepPara._GlbReportComp, row["DOC_NO"].ToString(), Convert.ToInt16(row["ITEM_LINE"].ToString()), Convert.ToInt16(row["BATCH_LINE"].ToString()));
                    SERIAL_DETAIL.Merge(dtSerial);
                }
            }
            else
            {
                SERIAL_DETAIL.Columns.Add("DOC_NO", typeof(string));
                SERIAL_DETAIL.Columns.Add("ITEM_CODE", typeof(string));
                SERIAL_DETAIL.Columns.Add("SERIAL_NO", typeof(string));
            }


            rptgit.Database.Tables["GIT_RECONCILIATION"].SetDataSource(valueAddtion);
            rptgit.Database.Tables["param"].SetDataSource(param);
            //rptgit.Subreports[0].Database.Tables["GIT_SERIAL"].SetDataSource(SERIAL_DETAIL);

            foreach (object repOp in rptgit.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "GIT_Serial_sub_rpt")
                    {
                        ReportDocument subRepDoc = rptgit.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["GIT_SERIAL"].SetDataSource(SERIAL_DETAIL);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }

                }
            }

        }

        public void ExcessStockReport(InvReportPara _objRepPara)
        {
            //Sanjeewa 2016-06-28     
            DataTable Excessstk = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("Location", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode;
            dr["company"] = _objRepPara._GlbReportCompCode;
            dr["company_name"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand;
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel;
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1;
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2;
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3;
            dr["Location"] = _objRepPara._GlbReportProfit;
            param.Rows.Add(dr);

            Excessstk.Clear();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(_objRepPara._GlbReportComp, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow row in tmp_user_pc.Rows)
                {
                    DataTable TMP_SER = new DataTable();
                    TMP_SER = bsObj.CHNLSVC.MsgPortal.getExcessStockDetails(_objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportComp, row["tpl_pc"].ToString(), _objRepPara._GlbReportDocType, _objRepPara._GlbUserID);
                    Excessstk.Merge(TMP_SER);
                }
            }

            _Esxcessstk.Database.Tables["EXCESS_STOCK"].SetDataSource(Excessstk);
            _Esxcessstk.Database.Tables["param"].SetDataSource(param);
        }

        public void ItemCanibalisePrint(string _docno)
        {
            //Sanjeewa 2016-06-29     
            DataTable ItemCanib = new DataTable();

            ItemCanib.Clear();
            ItemCanib = bsObj.CHNLSVC.MsgPortal.getItemCanibalisePrint(_docno);

            _itemcanib.Database.Tables["ITEM_CANIB"].SetDataSource(ItemCanib);
        }

        public void MRNPrint(string _docno)
        {
            //Sanjeewa 2016-07-29     
            DataTable MRN = new DataTable();

            MRN.Clear();
            MRN = bsObj.CHNLSVC.MsgPortal.getMRNPrint(_docno);

            _mrn.Database.Tables["MRN_PRINT"].SetDataSource(MRN);
        }

        public void asatGIT(InvReportPara _objRepPara)
        {
            // Wimal  @ 29/12/2015           
            DataTable valueAddtion = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode; ;
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand;
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel;
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1;
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2;
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3;
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4;
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5;
            param.Rows.Add(dr);

            valueAddtion.Clear();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(_objRepPara._GlbReportComp, _objRepPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow row in tmp_user_pc.Rows)
                {
                    DataTable TMP_SER = new DataTable();
                    TMP_SER = bsObj.CHNLSVC.MsgPortal.getGITReport_Current(_objRepPara._GlbReportAsAtDate.Date, _objRepPara._GlbReportComp, row["tpl_pc"].ToString(), _objRepPara._GlbReportOtherLoc, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5);
                    valueAddtion.Merge(TMP_SER);
                }
            }

            rptgit.Database.Tables["GIT_RECONCILIATION"].SetDataSource(valueAddtion);
            rptgit.Database.Tables["param"].SetDataSource(param);
        }

        public void Value_Addtion(InvReportPara _objRepPara)
        {
            // Wimal  @ 29/12/2015           
            DataTable valueAddtion = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = _objRepPara._GlbReportComp;
            dr["company_name"] = _objRepPara._GlbReportCompCode;
            param.Rows.Add(dr);

            valueAddtion.Clear();
            valueAddtion = bsObj.CHNLSVC.MsgPortal.getvalueAddtion(_objRepPara._GlbReportComp, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate);


            _valueAddition.Database.Tables["ValueAddition"].SetDataSource(valueAddtion);
            _valueAddition.Database.Tables["param"].SetDataSource(param);
        }

        public void liabilityReport(InvReportPara _objRepPara)
        {
            // Wimal  @ 21/12/2015
            DataTable MST_COM = new DataTable();
            DataTable purchaseOrderSumm = new DataTable();
            DataTable param = new DataTable();

            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_objRepPara._GlbReportCompCode);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));

            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("doc_no", typeof(string));
            param.Columns.Add("doc_type", typeof(string));


            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = _objRepPara._GlbReportComp;
            dr["company_name"] = _objRepPara._GlbReportCompCode;
            dr["companies"] = _objRepPara._GlbReportCompanies;

            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["doc_no"] = _objRepPara._GlbReportDoc == "" ? "ALL" : _objRepPara._GlbReportDoc.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["doc_type"] = _objRepPara._GlbReportDocType == "" ? "ALL" : _objRepPara._GlbReportDocType.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            param.Rows.Add(dr);



            purchaseOrderSumm.Clear();


            DataTable tmp_Table = new DataTable();
            //tmp_Table = bsObj.CHNLSVC.MsgPortal.GetStockBalanceCurrent_SCM(_objRepPara._GlbUserID, _objRepPara._GlbReportChannel, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemStatus, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportWithCost, _objRepPara._GlbReportWithSerial, _objRepPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepPara._GlbReportWithRCC, _objRepPara._GlbReportWithJob, _objRepPara._GlbReportWithGIT);
            tmp_Table = bsObj.CHNLSVC.MsgPortal.get_liabilityReport(_objRepPara._GlbReportComp, _objRepPara._GlbReportDoc);
            purchaseOrderSumm.Merge(tmp_Table);

            //_LiableRep.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _LiableRep.Database.Tables["TEMP_BOND_BALANCE"].SetDataSource(purchaseOrderSumm);
            //_LiableRep.Database.Tables["param"].SetDataSource(param);
        }

        public void Print_AOA_Warra(InvReportPara _objRepPara)
        {
            // Wimal  @ 21/12/2015
            DataTable MST_COM = new DataTable();
            DataTable purchaseOrderSumm = new DataTable();
            DataTable param = new DataTable();

            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode("ABL");

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));

            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("doc_no", typeof(string));
            param.Columns.Add("doc_type", typeof(string));


            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["period"] = "FROM " + _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = _objRepPara._GlbReportComp;
            dr["company_name"] = _objRepPara._GlbReportCompCode;
            dr["companies"] = _objRepPara._GlbReportCompanies;

            //dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["doc_no"] = _objRepPara._GlbReportDoc == "" ? "ALL" : _objRepPara._GlbReportDoc.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            //dr["doc_type"] = _objRepPara._GlbReportDocType == "" ? "ALL" : _objRepPara._GlbReportDocType.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            param.Rows.Add(dr);

            DataTable tmp_Table = new DataTable();
            //tmp_Table = bsObj.CHNLSVC.MsgPortal.GetStockBalanceCurrent_SCM(_objRepPara._GlbUserID, _objRepPara._GlbReportChannel, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemStatus, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _objRepPara._GlbReportWithCost, _objRepPara._GlbReportWithSerial, _objRepPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepPara._GlbReportWithRCC, _objRepPara._GlbReportWithJob, _objRepPara._GlbReportWithGIT);
            tmp_Table = bsObj.CHNLSVC.MsgPortal.get_WaraPrint_Main(_objRepPara._GlbReportComp, _objRepPara._GlbReportDoc, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportMainSerial);
            _warraPrint.Database.Tables["scm_warranty_movemnet"].SetDataSource(tmp_Table);
            _warraPrint.Database.Tables["param"].SetDataSource(param);

            DataTable tmp_Table1 = new DataTable();
            tmp_Table1 = bsObj.CHNLSVC.MsgPortal.get_WaraPrint_Sub(_objRepPara._GlbReportComp, _objRepPara._GlbReportDoc, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportMainSerial);
            //_warraPrint.Database.Tables["scm_warranty_movemnet_sub"].SetDataSource(tmp_Table1);


            foreach (object repOp in _warraPrint.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptSubSerial")
                    {
                        ReportDocument subRepDoc = _warraPrint.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["scm_warranty_movemnet_sub"].SetDataSource(tmp_Table1);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();
                }
            }

        }

        public void Movement_Audit_Trial(InvReportPara _objRepPara)
        {
            // kapila 
            DataTable glbTable = new DataTable();
            DataTable param = new DataTable();
            DataTable _dtMoveCateTp = new DataTable();
            tmp_user_pc = new DataTable();

            DataRow dr;
            Int32 _isSerWise = 0;

            if (_objRepPara._GlbReportName == "Movement_audit_trial_summary.rpt" || _objRepPara._GlbReportName == "Movement_audit_trial_det.rpt" || _objRepPara._GlbReportName == "Movement_audit_trial_cost.rpt" || _objRepPara._GlbReportName == "Movement_audit_trial.rpt" || _objRepPara._GlbReportName == "Movement_audit_trial_sum.rpt")
                _isSerWise = 0;
            else
                _isSerWise = 1;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(null, _objRepPara._GlbUserID);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("location", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("cat4", typeof(string));
            param.Columns.Add("cat5", typeof(string));
            param.Columns.Add("doctype", typeof(string));
            param.Columns.Add("docsubtype", typeof(string));
            param.Columns.Add("direction", typeof(string));
            param.Columns.Add("doccategory", typeof(string));
            param.Columns.Add("itemrange", typeof(string));
            param.Columns.Add("withcost", typeof(int));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["company"] = _objRepPara._GlbReportCompName;
            dr["companies"] = _objRepPara._GlbReportCompanies;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["fromdate"] = _objRepPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = _objRepPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["location"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["channel"] = _objRepPara._GlbReportChannel == "" ? "ALL" : _objRepPara._GlbReportChannel;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = _objRepPara._GlbReportItemCat4 == "" ? "ALL" : _objRepPara._GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = _objRepPara._GlbReportItemCat5 == "" ? "ALL" : _objRepPara._GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["doctype"] = _objRepPara._GlbReportDocType == "" ? "ALL" : _objRepPara._GlbReportDocType;
            dr["docsubtype"] = _objRepPara._GlbReportDocSubType == "" ? "ALL" : _objRepPara._GlbReportDocSubType;
            dr["direction"] = _objRepPara._GlbReportDirection == "" ? "ALL" : _objRepPara._GlbReportDirection;
            dr["doccategory"] = _objRepPara._GlbReportDocCat == "" ? "ALL" : _objRepPara._GlbReportDocCat;
            dr["itemrange"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode;
            dr["withcost"] = _objRepPara._GlbReportWithCost;

            param.Rows.Add(dr);


            glbTable.Clear();


            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_Table = new DataTable();
                    tmp_Table = bsObj.CHNLSVC.MsgPortal.PrintMovementAuditTrialReport(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbReportDocType, _objRepPara._GlbReportDocSubType, _objRepPara._GlbReportDirection, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, _objRepPara._GlbReportItemCat4, _objRepPara._GlbReportItemCat5, _isSerWise, _objRepPara._GlbReportOtherLoc, _objRepPara._GlbReportDoc, 0);
                    glbTable.Merge(tmp_Table);
                }
            }

            if (_objRepPara._GlbReportIsSummary == 1)
            {
                _dtMoveCateTp = bsObj.CHNLSVC.Inventory.GetMoveSubTypeTable(null);
                _moveAuditTrialSummary.Database.Tables["mst_movcatetp"].SetDataSource(_dtMoveCateTp);
                _moveAuditTrialSummary.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                _moveAuditTrialSummary.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                if (_objRepPara._GlbReportName == "Movement_audit_trial.rpt")
                {
                    _moveAuditTrial.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                    _moveAuditTrial.Database.Tables["param"].SetDataSource(param);
                }
                if (_objRepPara._GlbReportName == "Movement_audit_trial_cost.rpt")
                {
                    _moveAuditTrialCost.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                    _moveAuditTrialCost.Database.Tables["param"].SetDataSource(param);
                }
                if (_objRepPara._GlbReportName == "Movement_audit_trial_ser.rpt")
                {
                    _moveAuditTrialSer.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                    _moveAuditTrialSer.Database.Tables["param"].SetDataSource(param);
                }
                if (_objRepPara._GlbReportName == "Movement_audit_trial_ser_cost.rpt")
                {
                    _moveAuditTrialSerCost.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                    _moveAuditTrialSerCost.Database.Tables["param"].SetDataSource(param);
                }
                if (_objRepPara._GlbReportName == "SerialMovement.rpt")
                {
                    _serMove.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                    _serMove.Database.Tables["param"].SetDataSource(param);
                }
                if (_objRepPara._GlbReportName == "Movement_audit_trial_det.rpt")
                {
                    _moveAuditTrialDet.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                    _moveAuditTrialDet.Database.Tables["param"].SetDataSource(param);
                }
                if (_objRepPara._GlbReportName == "Movement_audit_trial_sum.rpt")
                {
                    _moveAuditTrialSum.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                    _moveAuditTrialSum.Database.Tables["param"].SetDataSource(param);
                }



            }
        }

        //subodana

        public void get_Item_Serials(string docno, string user, string location)
        {
            DataTable item_serials = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            string COM = Session["UserCompanyCode"].ToString();
            param.Columns.Add("DocNo", typeof(string));
            param.Columns.Add("User", typeof(string));
            param.Columns.Add("Location", typeof(string));
            param.Columns.Add("Company", typeof(string));
            //param.Columns.Add("company_name", typeof(string));

            dr = param.NewRow();
            dr["DocNo"] = docno;
            dr["User"] = user;

            DataTable locdet = bsObj.CHNLSVC.Sales.getLocDesc(COM, "LOC", location);
            dr["Location"] = locdet.Rows[0]["descp"].ToString(); //location;

            DataTable Comdescr = bsObj.CHNLSVC.CustService.sp_get_com_details(COM);


            //dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["Company"] = Comdescr.Rows[0]["MC_DESC"].ToString();
            //dr["company_name"] = BaseCls.GlbReportCompCode;
            param.Rows.Add(dr);


            item_serials.Clear();

            item_serials = bsObj.CHNLSVC.MsgPortal.getItemSerials(docno);

            _serialItems.Database.Tables["serial_items"].SetDataSource(item_serials);

            _serialItems.Database.Tables["param"].SetDataSource(param);

        }
        public void get_courierdata(string Refdoc, string com, string type, string user)
        {
            DataTable param = new DataTable();
            DataTable LocationDet = new DataTable();
            DataTable subloc = new DataTable();
            Transport _obj = new Transport();
            List<Transport> couris = new List<Transport>();
            DataRow dr;
            DataRow dr2 = LocationDet.NewRow();
            LocationDet.Columns.Add("ml_loc_desc", typeof(string));
            LocationDet.Columns.Add("ml_add1", typeof(string));
            LocationDet.Columns.Add("ml_add2", typeof(string));
            LocationDet.Columns.Add("ml_tel", typeof(string));
            LocationDet.Columns.Add("ml_loc_desc2", typeof(string));
            LocationDet.Columns.Add("ml_add12", typeof(string));
            LocationDet.Columns.Add("ml_add22", typeof(string));
            LocationDet.Columns.Add("ml_tel2", typeof(string));
            LocationDet.Columns.Add("ml_invoice", typeof(string));
            LocationDet.Columns.Add("ml_dealer", typeof(string));
            //param.Columns.Add("Date", typeof(string));
            param.Columns.Add("packs", typeof(string));
            param.Columns.Add("User", typeof(string));
            //param.Columns.Add("Weight", typeof(string));

            LocationDet.Columns.Add("ml_mob", typeof(string)); //Tharindu 2018-02-22
            LocationDet.Columns.Add("ml_sono", typeof(string));

            dr = param.NewRow();
            string locate;
            string sublocate = "";
            string company = com;

            _obj.Itrn_ref_doc = Refdoc;
            string SOA = string.Empty;

            //InventoryHeader inthdr = bsObj.CHNLSVC.Inventory.GetINTHDRByDocnO(com, type, Refdoc);
            InventoryHeader inthdr = bsObj.CHNLSVC.Inventory.GetINTHDR_Details(com, (Session["CourierType"].ToString() == "DO" || Session["CourierType"].ToString() == "AOD") ? Refdoc : string.Empty, Session["CourierType"].ToString() == "Inv" ? Refdoc : string.Empty);
            couris = bsObj.CHNLSVC.General.GET_INT_TRANSPORT(_obj).ToList();
            if (couris.Count <= 0)//couris.Count <= 0 && Session["InvNo"] != null
            {
                _obj.Itrn_ref_doc = inthdr.Ith_oth_docno;//Session["InvNo"].ToString();
                couris = bsObj.CHNLSVC.General.GET_INT_TRANSPORT(_obj).ToList();
            }
            if (couris.Count <= 0)
            {
                _obj.Itrn_ref_doc = inthdr.Ith_doc_no;
                couris = bsObj.CHNLSVC.General.GET_INT_TRANSPORT(_obj).ToList();
            }
            List<InventoryRequest> int_req = bsObj.CHNLSVC.Inventory.GET_SOA_REQ_DATA_FOR_DO(com, "SOA", Refdoc);
            if (inthdr.Ith_loc != null)
            {
                locate = inthdr.Ith_loc.ToString();
            }
            else
            {
                locate = "";
            }
            if (type == "AOD")
            {
                if (inthdr.Ith_oth_loc != null)
                {
                    sublocate = inthdr.Ith_oth_loc.ToString();
                }
                else
                {
                    sublocate = "";
                }
            }
            if (couris.Count > 0)
            {
                string paks = couris.Count.ToString();
                dr["packs"] = paks;
            }
            else
            {
                dr["packs"] = 0;
            }


            MasterLocation loc = bsObj.CHNLSVC.General.GetLocationByLocCode(company, locate);
            MasterLocation sublocated = bsObj.CHNLSVC.General.GetLocationByLocCode(company, sublocate);

            string name = loc.Ml_loc_desc.ToString();
            string add1 = loc.Ml_add1.ToString();
            string add2 = loc.Ml_add2.ToString();
            string cont = loc.Ml_tel.ToString();
            string name2 = "";
            string add12 = "";
            string add22 = "";
            string cont2 = "";
            string invoice = "";
            string dealer = "";
            string mob = "";
            string sono = "";

            if (int_req != null && int_req.Count > 0)
            {
                SOA = int_req.FirstOrDefault().Itr_req_no;
            }
            if (type == "AOD")
            {
                if (sublocated.Ml_loc_desc == null)
                {
                    name2 = "";
                }
                else
                {
                    name2 = sublocated.Ml_loc_desc.ToString();
                }
                if (sublocated.Ml_add1 == null)
                {
                    add12 = "";
                }
                else
                {
                    add12 = sublocated.Ml_add1.ToString();
                }
                if (sublocated.Ml_add2 == null)
                {
                    add22 = "";
                }
                else
                {
                    add22 = sublocated.Ml_add2.ToString();
                }
                if (sublocated.Ml_tel == null)
                {
                    cont2 = "";
                }
                else
                {
                    cont2 = sublocated.Ml_tel.ToString();
                }
            }
            if (type == "DO")
            {
                if (inthdr.Ith_del_party == null)
                {
                    name2 = "";
                }
                else
                {
                    name2 = inthdr.Ith_del_party.ToString();
                }
                if (inthdr.Ith_del_add1 == null)
                {
                    add12 = "";
                }
                else
                {
                    add12 = inthdr.Ith_del_add1.ToString();
                }
                if (inthdr.Ith_del_add2 == null)
                {
                    add22 = "";
                }
                else
                {
                    add22 = inthdr.Ith_del_add2.ToString();
                }
                if (inthdr.Ith_del_town == null)
                {
                    cont2 = "";
                }
                else
                {
                    cont2 = inthdr.Ith_del_town.ToString();
                }
                if (inthdr.Ith_oth_docno == null)
                {
                    invoice = "";
                }
                else
                {
                    invoice = inthdr.Ith_oth_docno.ToString();
                }

                //string _invNo = string.IsNullOrEmpty(invoice) ? Refdoc : string.Empty;
                InvoiceHeader _inv = bsObj.CHNLSVC.Sales.GetInvoiceHeaderDetails(invoice);
                if (_inv != null)
                {
                    //DataTable tbl = bsObj.CHNLSVC.Inventory.check_AOD_Recieved(Refdoc);
                    DataTable tbl = bsObj.CHNLSVC.Sales.SOACancleCheck(_inv.Sah_inv_no);

                    if (tbl.Rows.Count > 0)
                    {
                        SOA = tbl.Rows[0].Field<string>("itr_req_no");
                    }
                }
                if (_inv != null)
                {
                    if (_inv.Sah_cus_name == null)
                    {
                        dealer = "";
                    }
                    else
                    {
                        dealer = _inv.Sah_cus_name.ToString();
                    }
                    if (_inv.Sah_cus_add1 != null)
                    {
                        add12 = string.IsNullOrEmpty(add12) ? _inv.Sah_cus_add1 : add12;
                    }
                    if (_inv.Sah_cus_add2 != null)
                    {
                        add22 = string.IsNullOrEmpty(add22) ? _inv.Sah_cus_add2 : add22;
                    }
                    if (_inv.Sah_inv_no != null)
                    {
                        invoice = string.IsNullOrEmpty(invoice) ? _inv.Sah_inv_no : invoice;
                    }

                    if (_inv.Sah_cus_Mob != null)  // Tharindu 2018-02-22
                    {
                        mob = string.IsNullOrEmpty(mob) ? _inv.Sah_cus_Mob : mob;
                    }

                    if (_inv.Sah_cus_Tel != null)  // Tharindu 2018-02-22
                    {
                        cont2 = string.IsNullOrEmpty(cont2) ? _inv.Sah_cus_Tel : cont2;
                    }

                    if (_inv.Sah_ref_doc != null)  // Tharindu 2018-02-22
                    {
                        sono = string.IsNullOrEmpty(sono) ? _inv.Sah_ref_doc : sono;
                    }
                }
            }

            dr2["ml_loc_desc"] = name;
            dr2["ml_add1"] = add1;
            dr2["ml_add2"] = add2;
            dr2["ml_tel"] = cont;
            dr2["ml_loc_desc2"] = name2;
            dr2["ml_add12"] = add12;
            dr2["ml_add22"] = add22;
            dr2["ml_tel2"] = cont2;
            dr2["ml_invoice"] = invoice;
            dr2["ml_dealer"] = dealer == name2 ? dealer = string.Empty : dealer;
            dr2["ml_mob"] = mob;
            dr2["ml_sono"] = sono;

            dr["User"] = user;         
            LocationDet.Rows.Add(dr2);
            param.Rows.Add(dr);

            string code = "";
           
           
            if (com == "AAL")
            {
                // Add by Tharindu 2018-04-17

                DataTable CourierCom = bsObj.CHNLSVC.CommonSearch.GetCourierCompany(Session["RefDoc"].ToString(), "", Session["UserCompanyCode"].ToString());                
                if (CourierCom.Rows.Count > 0)
                {
                    code = CourierCom.Rows[0][0].ToString();
                    Session["CourierCom"] = code;
                }

                foreach (Transport doNo in couris)
                {
                    if (doNo.Itrn_ref_doc == invoice)
                    {
                        doNo.Itrn_ref_doc = inthdr.Ith_doc_no;
                    }
                }
                if (couris.Count <= 0)
                {
                    couris = new List<Transport>() { new Transport() { Itrn_ref_doc = inthdr.Ith_doc_no } };
                }
            }

            if (couris.Count > 0)
            {
               

                if (Session["UserCompanyCode"].ToString() == "AAL")
                {
                    if (code == "ABDR1A1353") // abs Company
                    {
                        _abscour.Database.Tables["Transport"].SetDataSource(couris);
                    }
                    //else if (code != "ABDR1A1353" && code == string.Empty)
                    //{
                    //    _abscour.Database.Tables["Transport"].SetDataSource(couris);
                    //}

                    else if (code == "CERTLCS")
                    {
                        _aalcour.Database.Tables["Transport"].SetDataSource(couris);
                    }
                    else
                    {
                        _abscour.Database.Tables["Transport"].SetDataSource(couris);
                    }
                   
                }
                else
                {
                    _abscour.Database.Tables["Transport"].SetDataSource(couris);
                }
            }
            else
            {
                List<Transport> courisss = new List<Transport>();
                Transport tt = new Transport();
                tt.Itrn_cre_dt = DateTime.Today;
                tt.Itrn_ref_doc = "No Any Transport";
                courisss.Add(tt);
                if (Session["UserCompanyCode"].ToString() == "AAL")
                {
                    if (code == "ABDR1A1353")
                    {
                        _abscour.Database.Tables["Transport"].SetDataSource(couris);
                    }
                    //else if (code != "ABDR1A1353" && code == string.Empty)
                    //{
                    //    _abscour.Database.Tables["Transport"].SetDataSource(couris);
                    //}
                    else if (code == "CERTLCS")
                    {
                        _aalcour.Database.Tables["Transport"].SetDataSource(couris);
                    }
                    else
                    {
                        _abscour.Database.Tables["Transport"].SetDataSource(courisss);
                    }
                }
                else
                {
                    _abscour.Database.Tables["Transport"].SetDataSource(courisss);
                }
            }
            _aalcour.Database.Tables["Packs"].SetDataSource(param);
            _aalcour.Database.Tables["Location"].SetDataSource(LocationDet);

            _abscour.Database.Tables["Packs"].SetDataSource(param);
            _abscour.Database.Tables["Location"].SetDataSource(LocationDet);
            _aalcour.SetParameterValue("SOA", SOA);
            _abscour.SetParameterValue("SOA", SOA);
            string masloc = locate;
            Session["InvNo"] = null;
        }

        public void AllocationDetails(string company, string chanal, string location, string cat1, string cat2, string range, string itemcode, string model, string brand, string frmdate, string todate, string user, string docno)
        {
            DateTime frmdatenew = Convert.ToDateTime(frmdate);
            DateTime todatenew = Convert.ToDateTime(todate);
            DataTable param = new DataTable();
            DataRow dr;
            param.Columns.Add("UserId", typeof(string));
            param.Columns.Add("FrmDt", typeof(string));
            param.Columns.Add("ToDt", typeof(string));
            param.Columns.Add("Company", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("MainCate", typeof(string));
            param.Columns.Add("SubCate", typeof(string));
            param.Columns.Add("ItRange", typeof(string));
            param.Columns.Add("ItmCode", typeof(string));
            param.Columns.Add("Itemcode", typeof(string));
            param.Columns.Add("Description", typeof(string));
            param.Columns.Add("Modelreal", typeof(string));
            param.Columns.Add("Chanal", typeof(string));
            param.Columns.Add("DocType", typeof(string));
            param.Columns.Add("DocDate", typeof(string));
            param.Columns.Add("GrnQty", typeof(string));
            param.Columns.Add("Location", typeof(string));
            param.Columns.Add("AllQty", typeof(string));
            param.Columns.Add("BaQty", typeof(string));

            List<InventoryAllocateDetails> mainQuary = new List<InventoryAllocateDetails>();
            InventorySearchItemsAll ser_obj = new InventorySearchItemsAll();
            List<InventoryAllocateDetails> alloc = bsObj.CHNLSVC.Inventory.getAllocationDet(frmdatenew, todatenew);
            //  string format = "MMM  d yyyy"; 
            List<InventorySearchItemsAll> sumqty = new List<InventorySearchItemsAll>();
            mainQuary = alloc.Where(x => x.isa_com == company && x.isa_dt >= frmdatenew && x.isa_dt <= todatenew).ToList();
            // mainQuary.ToList().ForEach(c => c.isa_doc_dt = c.isa_doc_dt.Date);



            if (chanal != "")
                mainQuary = mainQuary.Where(d => d.isa_chnl == chanal).ToList();

            if (location != "")
                mainQuary = mainQuary.Where(d => d.isa_loc == location).ToList();

            if (cat1 != "")
                mainQuary = mainQuary.Where(d => d.mi_cate_1 == cat1).ToList();

            if (cat2 != "")
                mainQuary = mainQuary.Where(d => d.mi_cate_2 == cat2).ToList();

            if (range != "")
                mainQuary = mainQuary.Where(d => d.mi_cate_3 == range).ToList();

            if (itemcode != "")
                mainQuary = mainQuary.Where(d => d.isa_itm_cd == itemcode).ToList();

            if (model != "")
                mainQuary = mainQuary.Where(d => d.mi_model == model).ToList();

            if (brand != "")
                mainQuary = mainQuary.Where(d => d.mi_brand == brand).ToList();

            if (mainQuary.Count == 0)
            {
                ser_obj = new InventorySearchItemsAll();
                // var GnrQty = 1;
                // ser_obj.iti_doc_no = docnomb;
                // ser_obj.iti_itm_cd = code;
                // ser_obj.iti_qty = null;
                sumqty.Add(ser_obj);
                sumqty.Add(ser_obj);
                dr = param.NewRow();
                dr["UserId"] = user;
                dr["FrmDt"] = frmdate;
                dr["ToDt"] = todate;
                dr["Company"] = company;
                dr["Brand"] = brand;
                dr["Model"] = model;
                dr["MainCate"] = cat1;
                dr["SubCate"] = cat2;
                dr["ItRange"] = range;
                dr["ItmCode"] = itemcode;
                dr["Itemcode"] = "";
                dr["Description"] = "";
                dr["Modelreal"] = "";
                dr["Chanal"] = "";
                dr["DocType"] = "";
                dr["DocDate"] = "";
                dr["Location"] = "";
                dr["AllQty"] = "";
                dr["GrnQty"] = "";
                dr["BaQty"] = "";
                param.Rows.Add(dr);
            }
            else
            {

                foreach (var maindet in mainQuary)
                {
                    string desreal;
                    string dt = maindet.isa_dt.ToString();
                    string[] dtarr = dt.Split(' ');
                    string dtneww = dtarr[0];
                    string documentdate = dtneww;

                    string descr = maindet.mi_shortdesc.ToString();
                    if (descr.Length >= 28)
                    {
                        desreal = descr.Substring(0, 27);
                    }
                    else
                    {
                        desreal = maindet.mi_shortdesc.ToString();
                    }



                    if (maindet.isa_doc_no == null | maindet.isa_itm_cd == null)
                    {


                        string docnomb = maindet.isa_doc_no;
                        string code = maindet.isa_itm_cd;
                        ser_obj = new InventorySearchItemsAll();
                        var GnrQty = 0;
                        ser_obj.iti_doc_no = docnomb;
                        ser_obj.iti_itm_cd = code;
                        ser_obj.iti_qty = GnrQty;
                        sumqty.Add(ser_obj);
                        sumqty.Add(ser_obj);
                        dr = param.NewRow();
                        dr["UserId"] = user;
                        dr["FrmDt"] = frmdate;
                        dr["ToDt"] = todate;
                        dr["Company"] = company;
                        dr["Brand"] = brand;
                        dr["Model"] = model;
                        dr["MainCate"] = cat1;
                        dr["SubCate"] = cat2;
                        dr["ItRange"] = range;
                        dr["ItmCode"] = itemcode;
                        dr["Itemcode"] = "";
                        dr["Description"] = "";
                        dr["Modelreal"] = "";
                        dr["Chanal"] = "";
                        dr["DocType"] = "";
                        dr["DocDate"] = "";
                        dr["Location"] = "";
                        dr["AllQty"] = "";
                        dr["GrnQty"] = GnrQty;
                        dr["BaQty"] = "";
                        param.Rows.Add(dr);
                    }
                    else
                    {
                        string docnomb = maindet.isa_doc_no;
                        string code = maindet.isa_itm_cd;
                        List<InventorySearchItemsAll> itemseri = bsObj.CHNLSVC.Sales.GETALLITEMS(docnomb, code);
                        if (itemseri != null)
                        {

                            var GnrQty = itemseri.Where(a => a.iti_doc_no == docnomb && a.iti_itm_cd == code).Sum(z => z.iti_qty);
                            ser_obj = new InventorySearchItemsAll();
                            ser_obj.iti_qty = GnrQty;
                            ser_obj.iti_doc_no = docnomb;
                            ser_obj.iti_itm_cd = code;
                            sumqty.Add(ser_obj);
                            dr = param.NewRow();
                            dr["UserId"] = user;
                            dr["FrmDt"] = frmdate;
                            dr["ToDt"] = todate;
                            dr["Company"] = company;
                            dr["Brand"] = brand;
                            dr["Model"] = model;
                            dr["MainCate"] = cat1;
                            dr["SubCate"] = cat2;
                            dr["ItRange"] = range;
                            dr["ItmCode"] = itemcode;
                            dr["Itemcode"] = maindet.isa_itm_cd;
                            dr["Description"] = desreal;
                            dr["Modelreal"] = maindet.mi_model;
                            dr["Chanal"] = maindet.isa_chnl;
                            dr["DocType"] = maindet.isa_doc_tp;
                            dr["DocDate"] = documentdate;
                            dr["Location"] = maindet.isa_loc;
                            dr["AllQty"] = maindet.isa_aloc_qty;
                            dr["GrnQty"] = GnrQty;
                            dr["BaQty"] = maindet.isa_aloc_bqty;
                            param.Rows.Add(dr);
                        }
                        else
                        {
                            ser_obj = new InventorySearchItemsAll();
                            var GnrQty = 0;
                            ser_obj.iti_doc_no = docnomb;
                            ser_obj.iti_itm_cd = code;
                            ser_obj.iti_qty = GnrQty;
                            sumqty.Add(ser_obj);
                            sumqty.Add(ser_obj);
                            dr = param.NewRow();
                            dr["UserId"] = user;
                            dr["FrmDt"] = frmdate;
                            dr["ToDt"] = todate;
                            dr["Company"] = company;
                            dr["Brand"] = brand;
                            dr["Model"] = model;
                            dr["MainCate"] = cat1;
                            dr["SubCate"] = cat2;
                            dr["ItRange"] = range;
                            dr["ItmCode"] = itemcode;
                            dr["Itemcode"] = maindet.isa_itm_cd;
                            dr["Description"] = desreal;
                            dr["Modelreal"] = maindet.mi_model;
                            dr["Chanal"] = maindet.isa_chnl;
                            dr["DocType"] = maindet.isa_doc_tp;
                            dr["DocDate"] = documentdate;
                            dr["Location"] = maindet.isa_loc;
                            dr["AllQty"] = maindet.isa_aloc_qty;
                            dr["GrnQty"] = GnrQty;
                            dr["BaQty"] = maindet.isa_aloc_bqty;
                            param.Rows.Add(dr);
                        }

                    }



                }

            }

            _allocDet.Database.Tables["Param"].SetDataSource(param);

        }

        public void LocationDetails(string company, string channel, string location, string locvalue)
        {
            //locvalue = locvalue.Remove(locvalue.Length - 2);
            //locvalue = locvalue + "'";
            DataTable Param = new DataTable();
            DataRow dr;
            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("Channel", typeof(string));
            dr = Param.NewRow();
            dr["Company"] = company;
            dr["Location"] = location;
            dr["Channel"] = channel;
            Param.Rows.Add(dr);
            DataTable data = bsObj.CHNLSVC.Inventory.getLocationDetails(company, channel, locvalue);
            _Locationdet.Database.Tables["LocationData"].SetDataSource(data);
            _Locationdet.Database.Tables["Param"].SetDataSource(Param);

        }

        public void ItemProfileDetails(string com, string user, string loc, string cat1, string cat2, string cat3, string cat4, string cat5, string code, string brand, string model, string act)
        {
            DataTable Param = new DataTable();
            DataRow dr;
            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("User", typeof(string));
            Param.Columns.Add("Cat1", typeof(string));
            Param.Columns.Add("Cat2", typeof(string));
            Param.Columns.Add("Cat3", typeof(string));
            Param.Columns.Add("Cat4", typeof(string));
            Param.Columns.Add("Cat5", typeof(string));
            Param.Columns.Add("Code", typeof(string));
            Param.Columns.Add("Brand", typeof(string));
            Param.Columns.Add("Model", typeof(string));



            dr = Param.NewRow();
            dr["Company"] = com;
            dr["Location"] = loc;
            dr["User"] = user;

            if (cat1 == "")
            {
                dr["Cat1"] = "All";
            }
            else
            {
                dr["Cat1"] = cat1;
            }

            if (cat2 == "")
            {
                dr["Cat2"] = "All";
            }
            else
            {
                dr["Cat2"] = cat2;
            }
            if (cat3 == "")
            {
                dr["Cat3"] = "All";
            }
            else
            {
                dr["Cat3"] = cat3;
            }
            if (cat4 == "")
            {
                dr["Cat4"] = "All";
            }
            else
            {
                dr["Cat4"] = cat4;
            }
            if (cat5 == "")
            {
                dr["Cat5"] = "All";
            }
            else
            {
                dr["Cat5"] = cat5;
            }
            if (code == "")
            {
                dr["Code"] = "All";
            }
            else
            {
                dr["Code"] = code;
            }
            if (brand == "")
            {
                dr["Brand"] = "All";
            }
            else
            {
                dr["Brand"] = brand;
            }
            if (model == "")
            {
                dr["Model"] = "All";
            }
            else
            {
                dr["Model"] = model;
            }
            Param.Rows.Add(dr);

            DataTable data = bsObj.CHNLSVC.Inventory.GET_ITEM_PROFILE_DETAILS(com, cat1, cat2, cat3, cat4, cat5, code, brand, model);
            //mi_act
            DataView dv = new DataView(data);
            if (act == "act") dv.RowFilter = "mi_act=1";
            if (act == "inact") dv.RowFilter = "mi_act=0";
            DataTable sortedDT = dv.ToTable();
            _itemprofile.Database.Tables["ItemProfileDetails"].SetDataSource(sortedDT);
            _itemprofile.Database.Tables["Param"].SetDataSource(Param);
        }

        public void RequistionNote(string com, string reqno, string user, string loc, string comName)
        {
            DataTable reqdata = bsObj.CHNLSVC.Inventory.getRequestItemDetails(com, reqno);
            DataTable Param = new DataTable();
            DataRow dr;
            Param.Columns.Add("User", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("Company", typeof(string));
            dr = Param.NewRow();
            dr["User"] = user;
            dr["Location"] = loc;
            dr["Company"] = comName;
            Param.Rows.Add(dr);

            _ReqestionNote.Database.Tables["ReqItmData"].SetDataSource(reqdata);
            _ReqestionNote.Database.Tables["Param"].SetDataSource(Param);

        }

        public bool useDefaultPrinter { get; set; }

        public string printerName { get; set; }

        public void ReservationItemsRep(DateTime fromdate, DateTime todate, string itemcode, string cat, string dispatchloc, string customer, string status, string docno, string cat2, string cat3, string adminTeam)
        {
            try
            {

                string com = Session["UserCompanyCode"].ToString();
                List<ReservationItemsrep> reslist = bsObj.CHNLSVC.Inventory.ReservationItemList(fromdate, todate, itemcode, cat, dispatchloc, customer, status, docno, cat2, cat3, adminTeam, com, Session["UserID"].ToString());
                //if (cat != "" && itemcode == "")
                //{
                //    //check items

                //    List<CatwithItems> itemlist = bsObj.CHNLSVC.Inventory.GetItemsDetWithCat(cat);

                //    if (cat2 != "")
                //    {
                //        itemlist = itemlist.Where(x => x.mi_cate_2 == cat2).ToList();
                //    }
                //    if (cat3 != "")
                //    {
                //        itemlist = itemlist.Where(x => x.mi_cate_3 == cat3).ToList();
                //    }
                //    List<ReservationItemsrep> resitemslist = new List<ReservationItemsrep>();
                //    foreach (var items in itemlist)
                //    {
                //        ReservationItemsrep resitemob = new ReservationItemsrep();
                //        itemcode = items.mi_cd.ToString();
                //        reslist = bsObj.CHNLSVC.Inventory.GetReservationItemsDet(com, fromdate, todate, docno, itemcode, status, dispatchloc, customer, "CUSA", adminTeam);
                //        resitemslist.AddRange(reslist);
                //    }


                //}
                //else
                //{
                //    reslist = bsObj.CHNLSVC.Inventory.GetReservationItemsDet(com, fromdate, todate, docno, itemcode, status, dispatchloc, customer, "CUSA", adminTeam);
                //}

                //foreach (var reslistnew in reslist)
                //{
                //    string refno = reslistnew.itr_req_no.ToString();
                //    string item_cd = reslistnew.itri_itm_cd.ToString();
                //    int line_no = reslistnew.line_no;
                //    string reqnumber = "";
                //    DataTable Hdrdata = new DataTable(); ;
                //    //check bond
                //    DataTable chkbond = bsObj.CHNLSVC.Inventory.CheckINTReqBond(com, refno);
                //    if (chkbond.Rows.Count > 0)
                //    {
                //        reqnumber = chkbond.Rows[0][0].ToString();
                //        //cusdec hdrdara
                //        Hdrdata = bsObj.CHNLSVC.CustService.GetCusdecHDRData(reqnumber, com);
                //    }
                //    if (Hdrdata.Rows.Count > 0)
                //    {
                //        //entryno
                //        reslistnew.itr_ref = Hdrdata.Rows[0]["cuh_doc_no"].ToString();
                //        reslistnew.entryno = Hdrdata.Rows[0]["cuh_cusdec_entry_no"].ToString();
                //        //entrydate
                //        reslistnew.entrydate = Convert.ToDateTime(Hdrdata.Rows[0]["cuh_cusdec_entry_dt"].ToString());
                //    }

                //    DataTable balQty = bsObj.CHNLSVC.Inventory.GetBalanceQtyForResItmRpt(com, refno, item_cd, line_no);
                //    if (balQty.Rows.Count > 0)
                //    {
                //        reslistnew.bal_qty = balQty.Rows[0]["BAL_QTY"].ToString() == "" ? Convert.ToInt32("0") : Convert.ToInt32(balQty.Rows[0]["BAL_QTY"].ToString());
                //    }
                //    else
                //    {
                //        reslistnew.bal_qty = 0;
                //    }
                //}
                DataTable Param = new DataTable();
                DataRow dr;
                Param.Columns.Add("User", typeof(string));
                Param.Columns.Add("Location", typeof(string));
                Param.Columns.Add("Fromdate", typeof(DateTime));
                Param.Columns.Add("Todate", typeof(DateTime));
                Param.Columns.Add("Company", typeof(string));

                dr = Param.NewRow();
                dr["User"] = Session["UserID"].ToString();
                dr["Fromdate"] = fromdate;
                dr["Todate"] = todate;
                dr["Location"] = Session["UserDefLoca"].ToString();
                dr["Company"] = com;

                Param.Rows.Add(dr);

                _resitems.Database.Tables["ResItemData"].SetDataSource(reslist);
                _resitems.Database.Tables["Param"].SetDataSource(Param);
            }
            catch (Exception e)
            { }

        }



        public DataTable ADJDetails(string com, string chanal, DateTime fromdate, DateTime todate)
        {
            List<AdjesmentDet> adjlist = new List<AdjesmentDet>();
            adjlist = bsObj.CHNLSVC.Inventory.StockAdjDetails(com, chanal, fromdate, todate);
            DataTable dt = new DataTable();

            if (adjlist != null)
            {
                adjlist = adjlist.GroupBy(l => new { l.ith_loc, l.ith_sub_tp, l.ith_direct })
  .Select(cl => new AdjesmentDet
  {
      ith_loc = cl.First().ith_loc.ToString(),
      total = cl.Sum(c => c.total),
      ith_doc_tp = cl.First().ith_doc_tp.ToString(),
      ith_stus = cl.First().ith_stus.ToString(),
      ith_sub_tp = cl.First().ith_sub_tp,
      ith_direct = cl.First().ith_direct
  }).ToList();

                foreach (var adjdata in adjlist)
                {
                    if (adjdata.ith_direct == 1)
                    {
                        adjdata.ith_doc_tp = "ADJ+";
                    }
                    if (adjdata.ith_direct == 0)
                    {
                        adjdata.ith_doc_tp = "ADJ-";
                    }

                }

                List<Adjloctype> adjloctypelist = new List<Adjloctype>();

                foreach (var adjdata2 in adjlist)
                {
                    Adjloctype obloctype = new Adjloctype();
                    obloctype.loc = adjdata2.ith_loc;
                    obloctype.type = adjdata2.ith_sub_tp;
                    obloctype.doctype = adjdata2.ith_doc_tp;
                    adjloctypelist.Add(obloctype);
                }

                adjloctypelist = adjloctypelist.GroupBy(l => new { l.loc })
         .Select(cl => new Adjloctype
         {
             loc = cl.First().loc.ToString(),
             type = cl.First().type.ToString(),
             doctype = cl.First().doctype.ToString(),
         }).ToList();


                DataRow dr;
                dt.Columns.Add("LOC", typeof(string));
                Int32 COUNT1 = 0;
                Int32 COUNT2 = 0;

                foreach (var adjdata3 in adjloctypelist)
                {

                    foreach (var adjdata4 in adjlist)
                    {
                        DataColumnCollection columns = dt.Columns;
                        if (adjdata3.loc == adjdata4.ith_loc && adjdata4.ith_doc_tp == "ADJ+")
                        {

                            if (!columns.Contains(adjdata4.ith_sub_tp + " " + "ADJ+"))
                            {
                                dt.Columns.Add(adjdata4.ith_sub_tp + " " + "ADJ+", typeof(string));
                            }
                            if (!columns.Contains(adjdata4.ith_sub_tp + " " + "TOTAL"))
                            {
                                dt.Columns.Add(adjdata4.ith_sub_tp + " " + "TOTAL", typeof(string));
                            }
                            if (!columns.Contains(adjdata4.ith_sub_tp + " " + "ADJ-"))
                            {
                                dt.Columns.Add(adjdata4.ith_sub_tp + " " + "ADJ-", typeof(string));
                            }

                        }
                        if (adjdata3.loc == adjdata4.ith_loc && adjdata4.ith_doc_tp == "ADJ-")
                        {
                            if (!columns.Contains(adjdata4.ith_sub_tp + " " + "ADJ-"))
                            {
                                dt.Columns.Add(adjdata4.ith_sub_tp + " " + "ADJ-", typeof(string));
                            }
                            if (!columns.Contains(adjdata4.ith_sub_tp + " " + "TOTAL"))
                            {
                                dt.Columns.Add(adjdata4.ith_sub_tp + " " + "TOTAL", typeof(string));
                            }
                            if (!columns.Contains(adjdata4.ith_sub_tp + " " + "ADJ+"))
                            {
                                dt.Columns.Add(adjdata4.ith_sub_tp + " " + "ADJ+", typeof(string));
                            }

                        }
                    }

                }

                foreach (var adjdata3 in adjloctypelist)
                {
                    dr = dt.NewRow();
                    dr["LOC"] = adjdata3.loc;
                    foreach (var adjdata4 in adjlist)
                    {
                        DataColumnCollection columns = dt.Columns;
                        if (adjdata3.loc == adjdata4.ith_loc && adjdata4.ith_doc_tp == "ADJ+")
                        {
                            dr[adjdata4.ith_sub_tp + " " + "ADJ+"] = adjdata4.total;

                        }
                        if (adjdata3.loc == adjdata4.ith_loc && adjdata4.ith_doc_tp == "ADJ-")
                        {
                            dr[adjdata4.ith_sub_tp + " " + "ADJ-"] = adjdata4.total * (-1);
                        }
                    }
                    dt.Rows.Add(dr);
                }

                foreach (DataRow dtRow in dt.Rows)
                {
                    foreach (DataColumn columnn in dt.Columns)
                    {
                        if (dtRow[columnn].ToString() == "")
                        {
                            dtRow[columnn] = "0";
                        }

                    }

                }
                int p = 0;
                foreach (DataRow dtRow in dt.Rows)
                {
                    Int32 k = 1;
                    foreach (DataColumn columnn in dt.Columns)
                    {
                        if (dt.Columns.Count >= k + 3)
                        {

                            decimal total = Convert.ToDecimal(dt.Rows[p].Field<string>(k)) + Convert.ToDecimal(dt.Rows[p].Field<string>(k + 2));
                            if (k + 1 != 2)
                            {
                                dt.Rows[p][k + 1] = total.ToString();
                            }
                            else
                            {
                                dt.Rows[p][2] = total.ToString();
                            }

                        }

                        k = k + 3;
                    }
                    p++;
                }

                ExportToExcel(dt, @"C:\EXPORT_DOCS\ADJ_" + com + "_" + chanal + ".xls");

            }
            return dt;


        }

        private void ExportToExcel(DataTable Tbl, string ExcelFilePath = null)
        {
            try
            {
                if (Tbl == null || Tbl.Columns.Count == 0)
                    throw new Exception("ExportToExcel: Null or empty input table!\n");

                // load excel, and create a new workbook
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                excelApp.Workbooks.Add();

                // single worksheet
                Microsoft.Office.Interop.Excel._Worksheet workSheet = excelApp.ActiveSheet;

                // column headings
                for (int i = 0; i < Tbl.Columns.Count; i++)
                {
                    workSheet.Cells[1, (i + 1)] = Tbl.Columns[i].ColumnName;
                }

                // rows
                for (int i = 0; i < Tbl.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (int j = 0; j < Tbl.Columns.Count; j++)
                    {
                        workSheet.Cells[(i + 2), (j + 1)] = Tbl.Rows[i][j];
                    }
                }

                // check fielpath
                if (ExcelFilePath != null && ExcelFilePath != "")
                {
                    try
                    {
                        workSheet.SaveAs(ExcelFilePath);
                        excelApp.Quit();
                        // MessageBox.Show("Excel file saved!");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                        + ex.Message);
                    }
                }
                else // no filepath is given
                {
                    excelApp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ExportToExcel: \n" + ex.Message);
            }
        }


        public void PurchaseReturnReport(InvReportPara _objRepoPara)
        {//Lakshika 2016-10-04
            DataTable param = new DataTable();
            DataRow dr;

            GLOB_DataTable = new DataTable();
            DataTable dtPurchaseReturns = new DataTable();

            param.Columns.Add("fromdate", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("com", typeof(string));
            param.Columns.Add("supplier", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("loc", typeof(string));
            param.Columns.Add("user", typeof(string));

            dr = param.NewRow();

            string fdate = _objRepoPara._GlbReportFromDate.Date.ToShortDateString();
            string fdt = Convert.ToDateTime(fdate).ToShortDateString();

            string tdate = _objRepoPara._GlbReportToDate.Date.ToShortDateString();
            string tdt = Convert.ToDateTime(tdate).ToShortDateString();

            dr["fromdate"] = fdate;
            dr["todate"] = tdate;
            dr["com"] = _objRepoPara._GlbReportCompName == "" ? "" : _objRepoPara._GlbReportCompName.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["loc"] = _objRepoPara._GlbLocation == "" ? "ALL" : _objRepoPara._GlbLocation.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["supplier"] = _objRepoPara._GlbReportSupplier == "" ? "ALL" : _objRepoPara._GlbReportSupplier.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["channel"] = _objRepoPara._GlbReportChannel == "" ? "ALL" : _objRepoPara._GlbReportChannel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["user"] = _objRepoPara._GlbUserID == "" ? "" : _objRepoPara._GlbUserID.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            param.Rows.Add(dr);

            GLOB_DataTable.Clear();

            dtPurchaseReturns = bsObj.CHNLSVC.MsgPortal.GetPurchaseReturnDetails(_objRepoPara._GlbReportFromDate, _objRepoPara._GlbReportToDate, _objRepoPara._GlbCompany, _objRepoPara._GlbLocation, _objRepoPara._GlbReportChannel, _objRepoPara._GlbReportSupplier, _objRepoPara._GlbUserID);

            //DataTable realdata = new DataTable();
            //DataRow redr;
            //realdata.Columns.Add("ITR_EXP_DT", typeof(string));
            //realdata.Columns.Add("ITR_REQ_NO", typeof(string));
            //realdata.Columns.Add("ITR_REF", typeof(string));
            //realdata.Columns.Add("ITR_REC_TO", typeof(string));
            //realdata.Columns.Add("ITR_ISSUE_FROM", typeof(string));
            //realdata.Columns.Add("ROWNUM", typeof(Int32));

            //int i = 0;
            //foreach (var dis in dtPurchaseReturns.Rows)
            //{

            //    redr = realdata.NewRow();

            //    string date = dtPurchaseReturns.Rows[i]["ITR_EXP_DT"].ToString();
            //    string dt = Convert.ToDateTime(date).ToShortDateString();

            //    redr["ITR_EXP_DT"] = dt;
            //    redr["ITR_REQ_NO"] = dtPurchaseReturns.Rows[i]["ITR_REQ_NO"].ToString();
            //    redr["ITR_REF"] = dtPurchaseReturns.Rows[i]["ITR_REF"].ToString();
            //    redr["ITR_REC_TO"] = dtPurchaseReturns.Rows[i]["ITR_REC_TO"].ToString();
            //    redr["ITR_ISSUE_FROM"] = dtPurchaseReturns.Rows[i]["ITR_ISSUE_FROM"].ToString();
            //    redr["ROWNUM"] = Convert.ToInt32(dtPurchaseReturns.Rows[i]["ROWNUM"].ToString());

            //    realdata.Rows.Add(redr);
            //    i++;
            //}


            // param.Rows.Add(realdata);


            _purchaseReturns.Database.Tables["PURCHASERETURNS"].SetDataSource(dtPurchaseReturns);
            _purchaseReturns.Database.Tables["param"].SetDataSource(param);


        }

        public void RCC_Report_Data(DataTable dtResult, InvReportPara rptParam)
        {
            DataTable RCC_Report = new DataTable();
            DataTable param = new DataTable();
            DataTable GLOB_DataTable = null;

            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));

            dr = param.NewRow();
            dr["user"] = rptParam._GlbUserID;
            dr["fromdate"] = rptParam._GlbReportFromDate;
            dr["todate"] = rptParam._GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = rptParam._GlbReportComp;
            dr["compaddr"] = rptParam._GlbReportCompAddr;
            param.Rows.Add(dr);

            _rccReport.Database.Tables["INT_RCC"].SetDataSource(dtResult);
            _rccReport.Database.Tables["param"].SetDataSource(param);
            //
        }
        public void get_printdata(string Refdoc, string com, string type, string user, string loc)
        {
            DataTable param = new DataTable();
            DataTable PurchaseRequest = new DataTable();
            DataRow dr;
            PurchaseRequest.Columns.Add("itr_req_no", typeof(string));
            PurchaseRequest.Columns.Add("itr_note", typeof(string));
            PurchaseRequest.Columns.Add("ml_loc_desc", typeof(string));
            PurchaseRequest.Columns.Add("itr_ref", typeof(string));
            PurchaseRequest.Columns.Add("mc_desc", typeof(string));
            PurchaseRequest.Columns.Add("mss_desc", typeof(string));
            PurchaseRequest.Columns.Add("itri_qty", typeof(decimal));
            PurchaseRequest.Columns.Add("itri_itm_cd", typeof(string));
            PurchaseRequest.Columns.Add("mis_desc", typeof(string));
            PurchaseRequest.Columns.Add("mi_shortdesc", typeof(string));
            PurchaseRequest.Columns.Add("itr_cre_dt", typeof(DateTime));
            PurchaseRequest.Columns.Add("itr_exp_dt", typeof(DateTime));
            PurchaseRequest.Columns.Add("itri_note", typeof(string));
            PurchaseRequest.Columns.Add("REQLOC", typeof(string));
            PurchaseRequest.Columns.Add("UOM", typeof(string));
            PurchaseRequest.Columns.Add("MODEL", typeof(string));

            //param.Columns.Add("Refdoc", typeof(string));
            //param.Columns.Add("com", typeof(string));
            // param.Columns.Add("loc", typeof(string));
            param.Columns.Add("user", typeof(string));
            string company = com;
            dr = param.NewRow();
            List<PurchseOrderPrint> printdata = bsObj.CHNLSVC.Inventory.GetPurReqByDocnO(com, type, Refdoc, loc);
            foreach (var purdata in printdata)
            {
                DataRow dr2 = PurchaseRequest.NewRow();

                string docno = purdata.itr_req_no.ToString();
                string remarks = purdata.itr_note.ToString();
                string location = purdata.ml_loc_desc.ToString();
                string reff = purdata.itr_ref.ToString();
                string comname = purdata.mc_desc.ToString();
                string status = purdata.mss_desc.ToString();
                decimal qty = Convert.ToDecimal(purdata.itri_qty.ToString());
                string itmcode = purdata.itri_itm_cd.ToString();
                string itemstatus = purdata.mis_desc.ToString();
                string itemdes = purdata.mi_shortdesc.ToString();
                DateTime date = Convert.ToDateTime(purdata.itr_cre_dt.ToString());
                DateTime exdate = Convert.ToDateTime(purdata.itr_exp_dt.ToString());
                string itemremark = purdata.itri_note.ToString();
                string REQLOC = purdata.REQLOC.ToString();
                string UOM = purdata.UOM.ToString();
                string MODEL = purdata.MODEL.ToString();

                dr2["itr_req_no"] = docno;
                dr2["itr_note"] = remarks;
                dr2["ml_loc_desc"] = location;
                dr2["itr_ref"] = reff;
                dr2["mc_desc"] = comname;
                dr2["mss_desc"] = status;
                dr2["itri_qty"] = qty;
                dr2["itri_itm_cd"] = itmcode;
                dr2["mis_desc"] = itemstatus;
                dr2["mi_shortdesc"] = itemdes;
                dr2["itr_cre_dt"] = date;
                dr2["itr_exp_dt"] = exdate;
                dr2["itri_note"] = itemremark;
                dr2["REQLOC"] = REQLOC;
                dr2["UOM"] = UOM;
                dr2["MODEL"] = MODEL;
                dr["User"] = user;
                PurchaseRequest.Rows.Add(dr2);
            }
            param.Rows.Add(dr);
            _PurchaseRequest.Database.Tables["PURREQPAR"].SetDataSource(param);
            _PurchaseRequest.Database.Tables["PURREQDET"].SetDataSource(PurchaseRequest);
        }

        //isuru 2017/05/23
        public void printpanaltychargedetails(string _reportName, string _ivn, string userid)
        {
            DataTable procen = new DataTable();

            procen.Columns.Add("name", typeof(string));

            string[] names = _ivn.Split(',');

            for (int i = 0; i < names.Length; i++)
            {
                procen.Rows.Add(new object[] { names[i] });
            }


            DataTable panaltyprintdata = new DataTable();
            int j = 0;

            DataTable alldet = new DataTable();

            alldet.Columns.Add("sah_cus_cd", typeof(string));
            alldet.Columns.Add("sah_cus_name", typeof(string));
            alldet.Columns.Add("sah_currency", typeof(string));
            alldet.Columns.Add("sah_d_cust_add1", typeof(string));
            alldet.Columns.Add("sah_d_cust_add2", typeof(string));
            alldet.Columns.Add("sah_dt", typeof(DateTime));
            alldet.Columns.Add("sah_inv_no", typeof(string));
            alldet.Columns.Add("sah_anal_7", typeof(Decimal));
            alldet.Columns.Add("sah_anal_8", typeof(Decimal));
            DataRow dr;
            foreach (var inv in procen.Rows)
            {
                string invno = procen.Rows[j]["name"].ToString();

                panaltyprintdata = bsObj.CHNLSVC.Inventory.getpanaltystatesdetails(invno);
                int k = 0;
                foreach (var abc in panaltyprintdata.Rows)
                {

                    string sah_cus_cd = Convert.ToString(panaltyprintdata.Rows[k]["SAH_CUS_CD"].ToString());
                    string sah_cus_name = Convert.ToString(panaltyprintdata.Rows[k]["sah_cus_name"].ToString());
                    string sah_currency = Convert.ToString(panaltyprintdata.Rows[k]["sah_currency"].ToString());
                    string sah_d_cust_add1 = Convert.ToString(panaltyprintdata.Rows[k]["sah_d_cust_add1"].ToString());
                    string sah_d_cust_add2 = Convert.ToString(panaltyprintdata.Rows[k]["sah_d_cust_add2"].ToString());
                    DateTime sah_dt = Convert.ToDateTime(panaltyprintdata.Rows[k]["sah_dt"].ToString());
                    string sah_inv_no = Convert.ToString(panaltyprintdata.Rows[k]["sah_inv_no"].ToString());
                    decimal sah_anal_7 = Convert.ToDecimal(panaltyprintdata.Rows[k]["sah_anal_7"].ToString());
                    decimal sah_anal_8 = Convert.ToDecimal(panaltyprintdata.Rows[k]["sah_anal_8"].ToString());


                    dr = alldet.NewRow();
                    dr["sah_cus_cd"] = sah_cus_cd;
                    dr["sah_cus_name"] = sah_cus_name;
                    dr["sah_currency"] = sah_currency;
                    dr["sah_d_cust_add1"] = sah_d_cust_add1;
                    dr["sah_d_cust_add2"] = sah_d_cust_add2;
                    dr["sah_dt"] = sah_dt;
                    dr["sah_inv_no"] = sah_inv_no;
                    dr["sah_anal_7"] = sah_anal_7;
                    dr["sah_anal_8"] = sah_anal_8;
                    alldet.Rows.Add(dr);
                    k++;
                }
                j++;
            }


            _statementofacc.Database.Tables["PANALTYDET"].SetDataSource(alldet);

        }
        public DataTable Requirment_Details(InvReportPara _objRepoPara, string GroupingCat)
        {
            DataTable Param = new DataTable();
            DataTable data = new DataTable();
            DataRow dr;
            Param.Columns.Add("FromDate", typeof(string));
            Param.Columns.Add("ToDate", typeof(string));
            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("GroupingCat", typeof(string));
            dr = Param.NewRow();
            dr["fromdate"] = _objRepoPara._GlbReportFromDate.ToShortDateString();
            dr["todate"] = _objRepoPara._GlbReportToDate.ToShortDateString();
            dr["Company"] = _objRepoPara._GlbCompany;
            dr["GroupingCat"] = GroupingCat;
            Param.Rows.Add(dr);
            data = bsObj.CHNLSVC.Inventory.RequirmentsDetails(_objRepoPara._GlbReportFromDate, _objRepoPara._GlbReportToDate, Session["UserCompanyCode"].ToString(), Session["UserID"].ToString());
            _ItemWiseStationeryRequirements.Database.Tables["RequirmentDetails"].SetDataSource(data);
            _ItemWiseStationeryRequirements.Database.Tables["ParamRe"].SetDataSource(Param);
            return data;
        }
        public DataTable Supplier_Pricess_Details(InvReportPara _objRepoPara)
        {
            DataTable Param = new DataTable();
            DataTable data = new DataTable();
            DataRow dr;
            Param.Columns.Add("FromDate", typeof(string));
            Param.Columns.Add("ToDate", typeof(string));
            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("GroupingCat", typeof(string));
            dr = Param.NewRow();
            dr["fromdate"] = _objRepoPara._GlbReportFromDate.ToShortDateString();
            dr["todate"] = _objRepoPara._GlbReportToDate.ToShortDateString();
            dr["Company"] = _objRepoPara._GlbCompany;
            dr["GroupingCat"] = string.Empty;
            Param.Rows.Add(dr);
            data = bsObj.CHNLSVC.Inventory.SupplierPricess_Details(_objRepoPara._GlbReportFromDate, _objRepoPara._GlbReportToDate, Session["UserCompanyCode"].ToString(), Session["UserID"].ToString());
            _SupplierPrices.Database.Tables["SupplierPrices"].SetDataSource(data);
            _SupplierPrices.Database.Tables["ParamRe"].SetDataSource(Param);
            return data;
        }

        //Tharindu 2018-01-22
        public void CreditCardPayPrint(string _Com, string _Pc, DateTime _FromDate, DateTime _ToDate, string _Bank, string _Mid, string _ReceiptNo, string _Userid)
        {

            DataTable MST_COM = new DataTable();
            DataTable Creditcardpaydetails = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_Com);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));

            dr = param.NewRow();
            dr["user"] = Session["UserID"];
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = BaseCls.GlbReportComp;
            dr["company_name"] = BaseCls.GlbReportCompCode;
            dr["companies"] = BaseCls.GlbReportCompanies;
            param.Rows.Add(dr);

            _creditcardPaydoc.Database.Tables["mst_com"].SetDataSource(MST_COM);


       //     purchaseOrderSumm.Clear();
            DataTable tmp_user_pc = new DataTable();
            DataTable tmp_Table = new DataTable();
            string _err = string.Empty;
            Creditcardpaydetails = bsObj.CHNLSVC.MsgPortal.GetCreditCardPayDetails(_Com, _Pc, _FromDate, _ToDate, _Bank, _Mid, _ReceiptNo, _Userid, out _err);

            _creditcardPaydoc.Database.Tables["sat_adj_CrediCard"].SetDataSource(Creditcardpaydetails);

        }

        //Tharindu 2018-01-23
        public void CreditCardSummaryPrint(string _Com, string _Pc, DateTime _FromDate, DateTime _ToDate, string _Bank, string _Mid, string _ReceiptNo, string _Userid)
        {

            DataTable MST_COM = new DataTable();
            DataTable Creditcardpaydetails = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_Com);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));

            dr = param.NewRow();
            dr["user"] = Session["UserID"];
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = BaseCls.GlbReportComp;
            dr["company_name"] = BaseCls.GlbReportCompCode;
            dr["companies"] = BaseCls.GlbReportCompanies;
            param.Rows.Add(dr);

            _creditcardsummarydoc.Database.Tables["mst_com"].SetDataSource(MST_COM);


            //     purchaseOrderSumm.Clear();
            DataTable tmp_user_pc = new DataTable();
            DataTable tmp_Table = new DataTable();
            string _err = string.Empty;
            Creditcardpaydetails = bsObj.CHNLSVC.MsgPortal.GetCreditCardPayDetails(_Com, _Pc, _FromDate, _ToDate, _Bank, _Mid, _ReceiptNo, _Userid, out _err);

            _creditcardsummarydoc.Database.Tables["sat_adj_CrediCard"].SetDataSource(Creditcardpaydetails);

            _creditcardsummarydoc.SetParameterValue("fromDate", _FromDate);
            _creditcardsummarydoc.SetParameterValue("toDate", _ToDate);
            _creditcardsummarydoc.SetParameterValue("userID", "");
            _creditcardsummarydoc.SetParameterValue("bankID", _Bank);
        }
        //Dulaj
        public void ProductItemUpdate(DateTime fromdate, DateTime todate, string otherLocation, string location, string company,string user)
        {
            try
            {
                DateTime date = todate;
                if (fromdate.Date == todate.Date)
                {
                    date = todate.AddDays(1);
                }

                string com = company;
                DataTable dt = bsObj.CHNLSVC.Inventory.GetItemConditionsByDate(com, fromdate, date, otherLocation, location, user);

                DataTable Param = new DataTable();
                DataRow dr;
                Param.Columns.Add("From", typeof(string));
                Param.Columns.Add("To", typeof(string));
                Param.Columns.Add("Total", typeof(string));
                Param.Columns.Add("PrintedDateTime", typeof(string));
                Param.Columns.Add("User", typeof(string));
                Param.Columns.Add("Company", typeof(string));
                Param.Columns.Add("Location", typeof(string));
                Param.Columns.Add("OtherLocation", typeof(string));
                // Param.Columns.Add("CompanyDescription", typeof(string));

                int x = dt.Rows.Count - 1;
                for (int i = 0; i < x; i++)
                {
                    DataRow itemRow = dt.Rows[i];
                    bool duplicated = false;
                    //if (dr["name"] == "Joe")
                    //    dr.Delete();
                    for (int j = i + 1; j < x; j++)
                    {

                        DataRow checker = dt.Rows[j];
                        if (itemRow["irsm_ser_id"].ToString().Trim().Equals(checker["irsm_ser_id"].ToString().Trim()))
                        {

                            if (itemRow["rct_tp"].ToString().Trim().Equals(checker["rct_tp"].ToString().Trim()))
                            {
                                duplicated = true;
                            }
                        }
                    }
                    if (duplicated)
                    {
                        itemRow.Delete();
                    }
                    x--;
                }

                dt.AcceptChanges();
                dt.Columns.Add("QTY", typeof(string));
                double total = 0;
                foreach (DataRow dataRow in dt.Rows)
                {
                    if (!((dataRow["irsc_cha"]).ToString().Equals("")))
                    {
                        total = total + Convert.ToDouble((dataRow["irsc_cha"]).ToString());
                        dataRow["QTY"] = "1";
                        // dt2.Rows.Add(dataRow);
                    }
                }
                DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(com);
                string companydesc = "";
                if (MST_COM.Rows.Count > 0)
                {
                    companydesc = MST_COM.Rows[0]["MC_DESC"].ToString();
                }
                dr = Param.NewRow();
                dr["User"] = Session["UserID"].ToString();
                dr["To"] = todate.ToString("MMMM dd, yyyy");
                dr["From"] = fromdate.ToString("MMMM dd, yyyy");
                dr["PrintedDateTime"] = companydesc;
                dr["Total"] = total.ToString("N2");
                dr["Company"] = com;
                dr["Location"] = location;
                dr["OtherLocation"] = otherLocation;
                //dr["CompanyDescription"] = companydesc;
                Param.Rows.Add(dr);

                _itemconditionreport.Database.Tables["ProductConditon"].SetDataSource(dt);
                _itemconditionreport.Database.Tables["ProductConditionParms"].SetDataSource(Param);
               
            }
            catch (Exception e)
            { }

        }

        public void GetCurrentAvailabilityBL_rpt(InvReportPara _objRepPara)
        {
            DataTable param = new DataTable();
            DataRow dr;
       //     MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(_Com);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("doctype", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));

            dr = param.NewRow();
            dr["user"] = Session["UserID"];
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["fromdate"] = BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = BaseCls.GlbReportComp;
            dr["company_name"] = BaseCls.GlbReportCompCode;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["doctype"] = _objRepPara._GlbDocNo == "" ? "ALL" : _objRepPara._GlbDocNo;
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1;
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2;
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3;
            param.Rows.Add(dr);


            DataTable Creditcardpaydetails = new DataTable();
            DataTable getpendingshipment = new DataTable();
            Creditcardpaydetails = bsObj.CHNLSVC.MsgPortal.GetCurrentAvailabilityBL(_objRepPara._GlbReportCompCode, _objRepPara._GlbReportFromDate, _objRepPara._GlbReportToDate, _objRepPara._GlbDocNo, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportModel, _objRepPara._GlbReportBrand, _objRepPara._GlbReportItemCat1,_objRepPara._GlbReportItemCat2,_objRepPara._GlbReportItemCat3,"", _objRepPara._GlbReportSupplier, _objRepPara._GlbUserID);
            getpendingshipment = bsObj.CHNLSVC.MsgPortal.GetShipmentDetails(Session["UserID"].ToString());

            _curr_available_rpt.Database.Tables["CURRENT_AVB_BL"].SetDataSource(Creditcardpaydetails);
            _curr_available_rpt.Database.Tables["param"].SetDataSource(param);


            foreach (object repOp in _curr_available_rpt.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();

                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rpt_pendingshipment")
                    {
                        ReportDocument subRepDoc = _curr_available_rpt.Subreports[_cs.SubreportName];
                        _curr_available_rpt.Database.Tables["PENDING_SHIP_DETAILS"].SetDataSource(getpendingshipment);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
         
                    _cs.Dispose();
                }
            }         
        }

        //Tharindu 2018-05-21
        //Tharindu 2018-01-23
       // CategoryWiseTradingGP(InvReportPara _objRepPara)
            // public void CategoryWiseTradingGP(string com,DateTime stdt,DateTime endDt, string chnl, string cate,string userid,string itmcd)
        public void CategoryWiseTradingGP(string com, DateTime stdt, DateTime endDt, string chnl, string cate, string userid, string itmcd)
        {

            DataTable MST_COM = new DataTable();
            DataTable Gpcatdetails = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(com);

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("company_name", typeof(string));
            param.Columns.Add("companies", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("maincat", typeof(string));

            dr = param.NewRow();
            dr["user"] = Session["UserID"];
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = BaseCls.GlbReportComp;
           // dr["fromdate"] = BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy"); 
          //  dr["todate"] = BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy"); 
            dr["fromdate"] = stdt;
            dr["todate"] = endDt;
            dr["company_name"] = BaseCls.GlbReportCompCode;
            dr["companies"] = BaseCls.GlbReportCompanies;
          //  dr["channel"] = _objRepPara._GlbReportChannel == "" ? "ALL" : _objRepPara._GlbReportChannel;
          //  dr["maincat"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1;

            dr["channel"] = chnl == "" ? "ALL" : chnl;
            dr["maincat"] = cate == "" ? "ALL" : cate;
           

            param.Rows.Add(dr);

         //   _cattrading.Database.Tables["mst_com"].SetDataSource(MST_COM);


            //     purchaseOrderSumm.Clear();
            DataTable tmp_user_pc = new DataTable();
            DataTable tmp_Table = new DataTable();
           GLOB_DataTable = new DataTable();
            string _err = string.Empty;
                       
       //     tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB_AllCom(userid);
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(com, Session["UserID"].ToString());

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                   Gpcatdetails = bsObj.CHNLSVC.MsgPortal.GetCatWiseTradingGPReport(drow["tpl_com"].ToString(), stdt, endDt, chnl, cate, drow["tpl_pc"].ToString(), userid, itmcd, out _err);

                    GLOB_DataTable.Merge(Gpcatdetails);
                }
            }
            else
            {
                GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.GetCatWiseTradingGPReport(com, stdt, endDt, chnl, cate, "",userid,itmcd, out _err);
            }      



            _cattrading.Database.Tables["GP_TRADING_CAT_REPORT"].SetDataSource(GLOB_DataTable);
            _cattrading.Database.Tables["param"].SetDataSource(param);
            _cattrading.Database.Tables["MST_COM"].SetDataSource(MST_COM);

    
        }
    }
}