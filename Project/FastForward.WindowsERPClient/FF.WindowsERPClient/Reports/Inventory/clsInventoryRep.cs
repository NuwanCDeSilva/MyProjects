using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms.Internal.Win32;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using FF.BusinessObjects;
using System.Collections;


namespace FF.WindowsERPClient
{
    class clsInventoryRep : Base
    {
        public FF.WindowsERPClient.Reports.Inventory.RevertSRN _recRvtrpt = new FF.WindowsERPClient.Reports.Inventory.RevertSRN();
        public FF.WindowsERPClient.Reports.Inventory.SRevertSRN S_recRvtrpt = new FF.WindowsERPClient.Reports.Inventory.SRevertSRN();
        public FF.WindowsERPClient.Reports.Inventory.InventoryStatements _invSts = new FF.WindowsERPClient.Reports.Inventory.InventoryStatements();
        public FF.WindowsERPClient.Reports.Inventory.InventoryStatementsTr _invStsTr = new FF.WindowsERPClient.Reports.Inventory.InventoryStatementsTr();
        public FF.WindowsERPClient.Reports.Inventory.InventoryStatementsTr2 _invStsTr2 = new FF.WindowsERPClient.Reports.Inventory.InventoryStatementsTr2();
        public FF.WindowsERPClient.Reports.Inventory.InventoryStatementsTr3 _invStsTr3 = new FF.WindowsERPClient.Reports.Inventory.InventoryStatementsTr3();
        public FF.WindowsERPClient.Reports.Inventory.InventoryStatementsTr3_new _invStsTr3_new = new FF.WindowsERPClient.Reports.Inventory.InventoryStatementsTr3_new();
        public FF.WindowsERPClient.Reports.Inventory.InventoryMovementAuditTrials _invAudit = new FF.WindowsERPClient.Reports.Inventory.InventoryMovementAuditTrials();
        public FF.WindowsERPClient.Reports.Inventory.InventoryMovementAuditTrials_ARL _invAudit_ARL = new FF.WindowsERPClient.Reports.Inventory.InventoryMovementAuditTrials_ARL();
        public FF.WindowsERPClient.Reports.Inventory.InventoryMovementAuditTrials_sum _invAuditSum = new FF.WindowsERPClient.Reports.Inventory.InventoryMovementAuditTrials_sum();
        public FF.WindowsERPClient.Reports.Inventory.InventoryMovementAuditTrialWithSerials _invAuditSrl = new FF.WindowsERPClient.Reports.Inventory.InventoryMovementAuditTrialWithSerials();
        //hasith 28/12/2015
        public FF.WindowsERPClient.Reports.Inventory.MovementSummaryReport _invMovsum = new FF.WindowsERPClient.Reports.Inventory.MovementSummaryReport();
        public FF.WindowsERPClient.Reports.Inventory.Stock_Balance _invBal = new FF.WindowsERPClient.Reports.Inventory.Stock_Balance();
        public FF.WindowsERPClient.Reports.Inventory.Stock_Balance_WO_Stus _invBalWOStus = new FF.WindowsERPClient.Reports.Inventory.Stock_Balance_WO_Stus();
        public FF.WindowsERPClient.Reports.Inventory.Stock_Balance_WO_Det _invBalWODet = new FF.WindowsERPClient.Reports.Inventory.Stock_Balance_WO_Det();
        public FF.WindowsERPClient.Reports.Inventory.Stock_Balance_AST _invBalAST = new FF.WindowsERPClient.Reports.Inventory.Stock_Balance_AST();
        public FF.WindowsERPClient.Reports.Inventory.DamageGoodsApproval _dmgApp = new FF.WindowsERPClient.Reports.Inventory.DamageGoodsApproval();
        public FF.WindowsERPClient.Reports.Inventory.GRAN_Details_Report _GRAN = new FF.WindowsERPClient.Reports.Inventory.GRAN_Details_Report();
        public FF.WindowsERPClient.Reports.Inventory.InterTransfer_Details_Report _Intr = new FF.WindowsERPClient.Reports.Inventory.InterTransfer_Details_Report();
        public FF.WindowsERPClient.Reports.Inventory.PurchaseOrderPrint _porpt = new FF.WindowsERPClient.Reports.Inventory.PurchaseOrderPrint();
        public FF.WindowsERPClient.Reports.Inventory.PurchaseOrderPrint_AST _porptast = new FF.WindowsERPClient.Reports.Inventory.PurchaseOrderPrint_AST();
        public FF.WindowsERPClient.Reports.Inventory.FastMovingItems _fastMov = new FF.WindowsERPClient.Reports.Inventory.FastMovingItems();
        public FF.WindowsERPClient.Reports.Inventory.NonMovingItems _nonMov = new FF.WindowsERPClient.Reports.Inventory.NonMovingItems();
        public FF.WindowsERPClient.Reports.Inventory.StockBalanceWithSerialAge _serAge = new FF.WindowsERPClient.Reports.Inventory.StockBalanceWithSerialAge();
        public FF.WindowsERPClient.Reports.Inventory.FIFO_Not_Followed_Report _FIFO = new FF.WindowsERPClient.Reports.Inventory.FIFO_Not_Followed_Report();
        public FF.WindowsERPClient.Reports.Inventory.WarrantyClaimNote _warrClaim = new FF.WindowsERPClient.Reports.Inventory.WarrantyClaimNote();
        public FF.WindowsERPClient.Reports.Inventory.Stock_BalanceCost _invBalCst = new FF.WindowsERPClient.Reports.Inventory.Stock_BalanceCost();
        public FF.WindowsERPClient.Reports.Inventory.Stock_BalanceCost_AST _invBalCstAST = new FF.WindowsERPClient.Reports.Inventory.Stock_BalanceCost_AST();
        public FF.WindowsERPClient.Reports.Inventory.Stock_BalanceSerial _invBalSrl = new FF.WindowsERPClient.Reports.Inventory.Stock_BalanceSerial();
        public FF.WindowsERPClient.Reports.Inventory.Exchange_Docs _ExchangeRep = new FF.WindowsERPClient.Reports.Inventory.Exchange_Docs();
        public FF.WindowsERPClient.Reports.Inventory.Exchange_Docs_Full _ExchangeRepfull = new FF.WindowsERPClient.Reports.Inventory.Exchange_Docs_Full();
        public FF.WindowsERPClient.Reports.Inventory.SExchange_Docs S_ExchangeRep = new FF.WindowsERPClient.Reports.Inventory.SExchange_Docs();
        public FF.WindowsERPClient.Reports.Inventory.OtherShop_DO_Report _OthShopDO = new FF.WindowsERPClient.Reports.Inventory.OtherShop_DO_Report();
        public FF.WindowsERPClient.Reports.Inventory.Exchange_Docs _serialAge = new FF.WindowsERPClient.Reports.Inventory.Exchange_Docs();
        public FF.WindowsERPClient.Reports.Inventory.Stock_BalanceSerialAsat _invBalSrlAsat = new FF.WindowsERPClient.Reports.Inventory.Stock_BalanceSerialAsat();
        public FF.WindowsERPClient.Reports.Inventory.Reserved_Serial_Report _ResSer = new FF.WindowsERPClient.Reports.Inventory.Reserved_Serial_Report();
        public FF.WindowsERPClient.Reports.Inventory.FAT_Dtl_Report _FAT = new FF.WindowsERPClient.Reports.Inventory.FAT_Dtl_Report();
        public FF.WindowsERPClient.Reports.Inventory.FixedAssetForSUN _FixedAsset = new FF.WindowsERPClient.Reports.Inventory.FixedAssetForSUN();
        public FF.WindowsERPClient.Reports.Inventory.PurchaseOrderPrint_HalfLett _porpth = new FF.WindowsERPClient.Reports.Inventory.PurchaseOrderPrint_HalfLett();
        public FF.WindowsERPClient.Reports.Sales.df_InvBal_Report _dfInvBal = new FF.WindowsERPClient.Reports.Sales.df_InvBal_Report();
        public FF.WindowsERPClient.Reports.Inventory.BOCReservedSerials _BOCRes = new FF.WindowsERPClient.Reports.Inventory.BOCReservedSerials();

        public FF.WindowsERPClient.Reports.Inventory.Reorder_Items _reOrder = new FF.WindowsERPClient.Reports.Inventory.Reorder_Items();
        public FF.WindowsERPClient.Reports.Inventory.POSummary _poSummary = new FF.WindowsERPClient.Reports.Inventory.POSummary();
        public FF.WindowsERPClient.Reports.Inventory.Insu_Stock_Report _insustk = new FF.WindowsERPClient.Reports.Inventory.Insu_Stock_Report();
        public FF.WindowsERPClient.Reports.Inventory.temp_saved_doc_report _tempsave = new FF.WindowsERPClient.Reports.Inventory.temp_saved_doc_report();
        public FF.WindowsERPClient.Reports.Inventory.Current_balance_with_price _CurrBalPrice = new FF.WindowsERPClient.Reports.Inventory.Current_balance_with_price();
        public FF.WindowsERPClient.Reports.Inventory.TemporaryIssueItems _tempIssueItms = new FF.WindowsERPClient.Reports.Inventory.TemporaryIssueItems();
        public FF.WindowsERPClient.Reports.Inventory.SubLocationStockVal _subLocStock = new FF.WindowsERPClient.Reports.Inventory.SubLocationStockVal();
        public FF.WindowsERPClient.Reports.Inventory.curr_age_report _CurrAge = new FF.WindowsERPClient.Reports.Inventory.curr_age_report();

        public FF.WindowsERPClient.Reports.Inventory.CatScatwiseItemAge _catScatAge = new FF.WindowsERPClient.Reports.Inventory.CatScatwiseItemAge();
        public FF.WindowsERPClient.Reports.Inventory.LocwiseItemAge _locAge = new FF.WindowsERPClient.Reports.Inventory.LocwiseItemAge();
        public FF.WindowsERPClient.Reports.Inventory.CatwiseItemAge _catAge = new FF.WindowsERPClient.Reports.Inventory.CatwiseItemAge();
        public FF.WindowsERPClient.Reports.Inventory.ItmBrndwiseItemAge _ItmBrndAge = new FF.WindowsERPClient.Reports.Inventory.ItmBrndwiseItemAge();
        public FF.WindowsERPClient.Reports.Inventory.CatItmwiseItemAge _catItmAge = new FF.WindowsERPClient.Reports.Inventory.CatItmwiseItemAge();
        public FF.WindowsERPClient.Reports.Inventory.ItmwiseItemAge _ItmAge = new FF.WindowsERPClient.Reports.Inventory.ItmwiseItemAge();
        public FF.WindowsERPClient.Reports.Inventory.LocItemStusAge _LocItmAge = new FF.WindowsERPClient.Reports.Inventory.LocItemStusAge();
        public FF.WindowsERPClient.Reports.Inventory.rpt_GLB_Git_Document _GitAsat = new FF.WindowsERPClient.Reports.Inventory.rpt_GLB_Git_Document();
        public FF.WindowsERPClient.Reports.Inventory.PSI _PSI = new FF.WindowsERPClient.Reports.Inventory.PSI();
        public FF.WindowsERPClient.Reports.Inventory.excess_stock_report _Esxcessstk = new FF.WindowsERPClient.Reports.Inventory.excess_stock_report();

        public FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial _moveAuditTrial = new FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial();
        public FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial_det _moveAuditTrialDet = new FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial_det();
        public FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial_summary _moveAuditTrialSum = new FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial_summary();
        public FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial_sum _moveAuditTrialList = new FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial_sum();
        public FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial_ser _moveAuditTrialSer = new FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial_ser();
        public FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial_ser_cost _moveAuditTrialSerCost = new FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial_ser_cost();
        public FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial_cost _moveAuditTrialCost = new FF.WindowsERPClient.Reports.Inventory.Movement_audit_trial_cost();
        public FF.WindowsERPClient.Reports.Inventory.PurchaseOrderPrintUpdate _poupdaterpt = new FF.WindowsERPClient.Reports.Inventory.PurchaseOrderPrintUpdate();
        public FF.WindowsERPClient.Reports.Inventory.Valuation_Dtl _valdtl = new FF.WindowsERPClient.Reports.Inventory.Valuation_Dtl();
        public FF.WindowsERPClient.Reports.Inventory.PurchaseOrderPrint_ABE _PurchaseOrderPrint_ABE = new FF.WindowsERPClient.Reports.Inventory.PurchaseOrderPrint_ABE();
        public FF.WindowsERPClient.Reports.Inventory.InventoryMovementAuditTrials_GroupByCate _InvMovementAuditTrailGrpByCate = new FF.WindowsERPClient.Reports.Inventory.InventoryMovementAuditTrials_GroupByCate();
        public FF.WindowsERPClient.Reports.Inventory.AdjusmentCostWithCatType _StkAdjDtlWCate = new FF.WindowsERPClient.Reports.Inventory.AdjusmentCostWithCatType();
        public FF.WindowsERPClient.Reports.Inventory.summarized_age_report _summarized_age_report = new FF.WindowsERPClient.Reports.Inventory.summarized_age_report();
        public FF.WindowsERPClient.Reports.Inventory.Status_wise_ageing_report _Status_wise_ageing_report = new FF.WindowsERPClient.Reports.Inventory.Status_wise_ageing_report();
        public FF.WindowsERPClient.Reports.Inventory.Disposal_summary _Disposal_summary = new FF.WindowsERPClient.Reports.Inventory.Disposal_summary(); 
        //public FF.WindowsERPClient.Reports.Inventory.Status_wise_ageing_report _Status_wise_ageing_report = new FF.WindowsERPClient.Reports.Inventory.Status_wise_ageing_report();
        public FF.WindowsERPClient.Reports.Inventory.LocItemStusAgenew _itmwise = new FF.WindowsERPClient.Reports.Inventory.LocItemStusAgenew();
 
        public FF.WindowsERPClient.Reports.Audit.Charge_Sheet _chargesheet = new FF.WindowsERPClient.Reports.Audit.Charge_Sheet();
        public FF.WindowsERPClient.Reports.Inventory.AgeSummery _ageSummery = new FF.WindowsERPClient.Reports.Inventory.AgeSummery();


        DataTable tmp_user_pc = new DataTable();
        DataTable GLOB_DataTable = new DataTable();
        DataTable GV_PRINTDoc = new DataTable();

        //DO Print
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
        DataTable param1 = new DataTable();
        DataTable tblComDate = new DataTable();
        DataTable PRINT_DOC = new DataTable();
        DataTable receiptDetails = new DataTable();
        DataTable param = new DataTable();


        DataTable VIW_INV_MOVEMENT_SERIAL_uni_nonDel = new DataTable();
        DataTable VIW_INV_MOVEMENT_SERIAL_Root = new DataTable();
        DataTable VIW_INV_MOVEMENT_SERIAL_uni = new DataTable();
        DataTable itemSerials = new DataTable();
        decimal QTY = 0;//VIW_INV_MOVEMENT_SERIAL_Root.Rows.Count;
        public bool isAccess = false;
        //end DO Print Table

        private int _OffsetYVal;
        private int OffsetYVal
        {
            get { return _OffsetYVal; }
            set { _OffsetYVal = value; }
        }

        //Base bsObj;

        public clsInventoryRep()
        {
            //bsObj = new Base();

        }

        public void getValuationDetails_Insu(Int32 withsts, out string _err)
        {
            DataTable Param = new DataTable();
            DataRow dr;
            Param.Columns.Add("user", typeof(string));
            Param.Columns.Add("header", typeof(string));
            Param.Columns.Add("loc", typeof(string));
            Param.Columns.Add("period", typeof(string));

            dr = Param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["header"] = BaseCls.GlbReportHeading;
            dr["loc"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["period"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            Param.Rows.Add(dr);
            DataTable data = new DataTable();
            try
            {
                data = CHNLSVC.MsgPortal.getValuationDetails_Insu_new(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportItmClasif,
                           BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                           BaseCls.GlbReportItemStatus, BaseCls.GlbReportDocSubType, BaseCls.GlbReportDocType, BaseCls.GlbReportCompCode, BaseCls.GlbUserID, withsts, out _err);
                
            }
            catch (Exception)
            {

                throw;//new System.ArgumentException("Data Not Found", "valuation report");

            }
            _valdtl.Database.Tables["VAL_DTL"].SetDataSource(data);
            _valdtl.Database.Tables["param"].SetDataSource(Param);

            
        }

        public void PrintSubLocationStockValue()
        {// kapila
            DataTable param = new DataTable();
            DataRow dr;

            DataTable GLB_ITMS = CHNLSVC.MsgPortal.getSubLocationStock(BaseCls.GlbUserComCode, BaseCls.GlbReportDoc, BaseCls.GlbReportDoc1, BaseCls.GlbReportParaLine1, BaseCls.GlbReportToDate.Date, BaseCls.GlbUserID, 1);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("withcost", typeof(Int32));



            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["withcost"] = BaseCls.GlbReportParaLine2;

            param.Rows.Add(dr);

            _subLocStock.Database.Tables["MST_SUB_LOC"].SetDataSource(GLB_ITMS);
            _subLocStock.Database.Tables["param"].SetDataSource(param);

        }

        public void ExcessStockReport()
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
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["company"] = BaseCls.GlbReportComp;
            dr["company_name"] = BaseCls.GlbReportComp;
            dr["companies"] = BaseCls.GlbReportCompCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["Location"] = BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            Excessstk.Clear();

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow row in tmp_user_pc.Rows)
                {
                    DataTable TMP_SER = new DataTable();
                    TMP_SER = CHNLSVC.MsgPortal.getExcessStockDetails(BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbUserComCode, row["tpl_pc"].ToString(), BaseCls.GlbReportDoc, BaseCls.GlbUserID);
                    Excessstk.Merge(TMP_SER);
                }
            }

            _Esxcessstk.Database.Tables["EXCESS_STOCK"].SetDataSource(Excessstk);
            _Esxcessstk.Database.Tables["param"].SetDataSource(param);
        }

        public void PrintTempIssueItems()
        {// kapila
            DataTable param = new DataTable();
            DataRow dr;

            DataTable GLB_ITMS = CHNLSVC.MsgPortal.PrintTempIssueItems(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["fromdate"] = BaseCls.GlbReportFromDate.Date;
            dr["todate"] = BaseCls.GlbReportToDate.Date;
            param.Rows.Add(dr);

            _tempIssueItms.Database.Tables["SCV_TEMP_ISSUE"].SetDataSource(GLB_ITMS);
            _tempIssueItms.Database.Tables["param"].SetDataSource(param);

        }

        public void ReorderItems()
        {
            // kapila 27/2/2015
            DataTable GLB_REORDER = new DataTable();
            DataTable MST_LOC = new DataTable();

            DataTable param = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("withstores", typeof(Int32));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["withstores"] = BaseCls.GlbReportIsFast;
            param.Rows.Add(dr);

            GLB_REORDER.Clear();
            tmp_user_pc = CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV_BAL = new DataTable();
                    TMP_INV_BAL = CHNLSVC.CustService.ReorderItemsPrint(BaseCls.GlbReportCompCode, BaseCls.GlbUserID, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportIsFast);

                    GLB_REORDER.Merge(TMP_INV_BAL);
                }
            }

            //  MST_LOC = CHNLSVC.Inventory.Get_all_LocationsTable(BaseCls.GlbReportCompCode);
            // MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            _reOrder.Database.Tables["reorder"].SetDataSource(GLB_REORDER);
            //    _reOrder.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

            //  _reOrder.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _reOrder.Database.Tables["param"].SetDataSource(param);
        }

        public void BOCReservedSerialDetails()
        {
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable _glbReservedSer = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));


            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["docType"] = BaseCls.GlbReportType == "" ? "ALL" : BaseCls.GlbReportType;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["Direct"] = BaseCls.GlbReportDirection == "" ? "ALL" : BaseCls.GlbReportDirection;
            dr["Model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["Brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["itemCode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;

            param.Rows.Add(dr);

            _glbReservedSer.Clear();
            tmp_user_pc = CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV_BAL = new DataTable();
                    TMP_INV_BAL = CHNLSVC.MsgPortal.ProcessBOCReservedSerials(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocType, drow["tpl_pc"].ToString(), BaseCls.GlbReportCompCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportDirection, BaseCls.GlbUserID, BaseCls.GlbReportDocSubType);

                    _glbReservedSer.Merge(TMP_INV_BAL);
                }
            }

            MST_LOC = CHNLSVC.Inventory.Get_all_LocationsTable(BaseCls.GlbReportCompCode);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            _BOCRes.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(_glbReservedSer);
            _BOCRes.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _BOCRes.Database.Tables["param"].SetDataSource(param);

        }

        public void DutyFreeInventoryBalwithPriceReport()
        {// Sanjeewa 10-10-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = CHNLSVC.MsgPortal.DFInvBalwithPriceDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());

                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("asatdate", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);

            _dfInvBal.Database.Tables["DF_INVBAL"].SetDataSource(GLOB_DataTable);
            _dfInvBal.Database.Tables["param"].SetDataSource(param);

        }

        public void FixedAsset()
        {
            // kapila
            DataTable remCheckList = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);


            remCheckList.Clear();
            remCheckList = CHNLSVC.Inventory.FixedAsset(BaseCls.GlbUserComCode, "MSR", Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);


            _FixedAsset.Database.Tables["Fixed_Asset"].SetDataSource(remCheckList);
            //_FixedAsset.Database.Tables["param"].SetDataSource(param);
        }

        public void Revert_Adj_print()
        {  // Nadeeka 17-01-2013
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

            DataTable VIW_INV_MOVEMENT_SERIAL = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();

            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable HPT_RVT_HDR = new DataTable();
            DataTable deliverSales = new DataTable();
            DataTable hpt_cust = new DataTable();
            DataTable mst_busentity = new DataTable();



            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;
            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            _recRvtrpt.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);


            string _com = default(string);
            foreach (DataRow row in VIW_INV_MOVEMENT_SERIAL.Rows)
            {
                _com = row["ITH_COM"].ToString();
                string _loc = row["ITH_LOC"].ToString();
                string _itm = row["ITS_ITM_CD"].ToString();
                string _othLoc = row["ITH_OTH_LOC"].ToString();
                int index = VIW_INV_MOVEMENT_SERIAL.Rows.IndexOf(row);
                if (index == 0)
                {
                    HPT_RVT_HDR = CHNLSVC.Sales.GetHPRevertHeader(row["ITH_OTH_DOCNO"].ToString());
                    MST_LOC = CHNLSVC.Sales.GetLocationCode(_com, _loc);
                    MST_COM = CHNLSVC.General.GetCompanyByCode(_com);

                }

                //  MST_ITM = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM.Merge(CHNLSVC.Sales.GetItemCode(_com, _itm));

                // MST_ITM_STUS = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                MST_ITM_STUS.Merge(CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString()));


                foreach (DataRow row1 in HPT_RVT_HDR.Rows)
                {
                    deliverSales = CHNLSVC.Sales.GetHPSaleswithDOItems(row1["HRT_ACC_NO"].ToString(), _itm);

                    hpt_cust = CHNLSVC.Sales.GetHpAccCustomer(row1["HRT_ACC_NO"].ToString());
                }
            }

            deliverSales = deliverSales.DefaultView.ToTable(true);
            hpt_cust = hpt_cust.DefaultView.ToTable(true);

            foreach (DataRow row in hpt_cust.Rows)
            {
                int index = hpt_cust.Rows.IndexOf(row);
                if (index == 0)
                {
                    mst_busentity = CHNLSVC.Sales.GetBusinessCompanyDetailTable(_com, row["HTC_CUST_CD"].ToString(), "", "", "C");
                }
            }

            VIW_INV_MOVEMENT_SERIAL = VIW_INV_MOVEMENT_SERIAL.DefaultView.ToTable(true);
            MST_LOC = MST_LOC.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            HPT_RVT_HDR = HPT_RVT_HDR.DefaultView.ToTable(true);
            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            deliverSales = deliverSales.DefaultView.ToTable(true);
            hpt_cust = hpt_cust.DefaultView.ToTable(true);
            mst_busentity = mst_busentity.DefaultView.ToTable(true);

            _recRvtrpt.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            _recRvtrpt.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _recRvtrpt.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            _recRvtrpt.Database.Tables["HPT_RVT_HDR"].SetDataSource(HPT_RVT_HDR);
            _recRvtrpt.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _recRvtrpt.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _recRvtrpt.Database.Tables["deliverSales"].SetDataSource(deliverSales);
            _recRvtrpt.Database.Tables["hpt_cust"].SetDataSource(hpt_cust);
            _recRvtrpt.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);

            //foreach (object repOp in _recRvtrpt.ReportDefinition.ReportObjects)                                                       
            //{
            //    string _s = repOp.GetType().ToString();
            //    if (_s.ToLower().Contains("subreport"))
            //    {
            //        SubreportObject _cs = (SubreportObject)repOp;
            //        if (_cs.SubreportName == "rptCustomer")
            //        {
            //            ReportDocument subRepDoc = _recRvtrpt.Subreports[_cs.SubreportName];

            //            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

            //        }

            //    }
            //}


        }

        public void SRevert_Adj_print()
        {  // Nadeeka 17-01-2013
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;

            //Inward_Docs report1 = new Inward_Docs();

            DataTable VIW_INV_MOVEMENT_SERIAL = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();

            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataTable HPT_RVT_HDR = new DataTable();
            DataTable deliverSales = new DataTable();
            DataTable hpt_cust = new DataTable();
            DataTable mst_busentity = new DataTable();



            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;
            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            S_recRvtrpt.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);


            string _com = default(string);
            foreach (DataRow row in VIW_INV_MOVEMENT_SERIAL.Rows)
            {
                _com = row["ITH_COM"].ToString();
                string _loc = row["ITH_LOC"].ToString();
                string _itm = row["ITS_ITM_CD"].ToString();
                string _othLoc = row["ITH_OTH_LOC"].ToString();
                int index = VIW_INV_MOVEMENT_SERIAL.Rows.IndexOf(row);
                if (index == 0)
                {
                    HPT_RVT_HDR = CHNLSVC.Sales.GetHPRevertHeader(row["ITH_OTH_DOCNO"].ToString());
                    MST_LOC = CHNLSVC.Sales.GetLocationCode(_com, _loc);
                    MST_COM = CHNLSVC.General.GetCompanyByCode(_com);

                }

                //MST_ITM = CHNLSVC.Sales.GetItemCode(_com, _itm);
                //MST_ITM_STUS = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());


                //  MST_ITM = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM.Merge(CHNLSVC.Sales.GetItemCode(_com, _itm));

                // MST_ITM_STUS = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
                MST_ITM_STUS.Merge(CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString()));

                foreach (DataRow row1 in HPT_RVT_HDR.Rows)
                {
                    deliverSales = CHNLSVC.Sales.GetHPSaleswithDOItems(row1["HRT_ACC_NO"].ToString(), _itm);

                    hpt_cust = CHNLSVC.Sales.GetHpAccCustomer(row1["HRT_ACC_NO"].ToString());
                }
            }
            deliverSales = deliverSales.DefaultView.ToTable(true);
            hpt_cust = hpt_cust.DefaultView.ToTable(true);

            foreach (DataRow row in hpt_cust.Rows)
            {
                int index = hpt_cust.Rows.IndexOf(row);
                if (index == 0)
                {
                    mst_busentity = CHNLSVC.Sales.GetBusinessCompanyDetailTable(_com, row["HTC_CUST_CD"].ToString(), "", "", "C");
                }
            }

            VIW_INV_MOVEMENT_SERIAL = VIW_INV_MOVEMENT_SERIAL.DefaultView.ToTable(true);
            MST_LOC = MST_LOC.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            HPT_RVT_HDR = HPT_RVT_HDR.DefaultView.ToTable(true);
            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            deliverSales = deliverSales.DefaultView.ToTable(true);
            hpt_cust = hpt_cust.DefaultView.ToTable(true);
            mst_busentity = mst_busentity.DefaultView.ToTable(true);

            S_recRvtrpt.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            S_recRvtrpt.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            S_recRvtrpt.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            S_recRvtrpt.Database.Tables["HPT_RVT_HDR"].SetDataSource(HPT_RVT_HDR);
            S_recRvtrpt.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            S_recRvtrpt.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            S_recRvtrpt.Database.Tables["deliverSales"].SetDataSource(deliverSales);
            S_recRvtrpt.Database.Tables["hpt_cust"].SetDataSource(hpt_cust);
            S_recRvtrpt.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);

            //foreach (object repOp in _recRvtrpt.ReportDefinition.ReportObjects)                                                       
            //{
            //    string _s = repOp.GetType().ToString();
            //    if (_s.ToLower().Contains("subreport"))
            //    {
            //        SubreportObject _cs = (SubreportObject)repOp;
            //        if (_cs.SubreportName == "rptCustomer")
            //        {
            //            ReportDocument subRepDoc = _recRvtrpt.Subreports[_cs.SubreportName];

            //            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

            //        }

            //    }
            //}


        }

        public void PrintPSI_Report()
        {

            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable PROC_INVENTORY_STATEMENT = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();

            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);


            PROC_INVENTORY_STATEMENT.Clear();
            DataTable _dtLoc = CHNLSVC.General.getPSITransLocs(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
            /* foreach (DataRow drow in _dtLoc.Rows)
             {
                 DataTable TEMP = new DataTable();
                 TEMP = CHNLSVC.MsgPortal.ProcessPSI(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, drow["ith_loc"].ToString(), drow["ith_com"].ToString(), BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3);
                 PROC_INVENTORY_STATEMENT.Merge(TEMP);
             }*/

            DataTable TEMP = new DataTable();
            TEMP = CHNLSVC.MsgPortal.ProcessPSI(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, "", "", BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3);
            PROC_INVENTORY_STATEMENT.Merge(TEMP);

            MST_LOC = CHNLSVC.Inventory.Get_all_LocationsTable(BaseCls.GlbReportCompCode);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            if (BaseCls.GlbReportName == "PSI.rpt")
            {
                _PSI.Database.Tables["PROC_INVENTORY_STATEMENT"].SetDataSource(PROC_INVENTORY_STATEMENT);
                _PSI.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                _PSI.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _PSI.Database.Tables["param"].SetDataSource(param);
            }


        }

        public void inventoryStatement()
        {
            // Nadeeka 26-02-2013
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable PROC_INVENTORY_STATEMENT = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);


            PROC_INVENTORY_STATEMENT.Clear();
            if (BaseCls.GlbReportName == "InventoryStatementsTr.rpt" || BaseCls.GlbReportName == "InventoryStatementsTr2.rpt" || BaseCls.GlbReportName == "InventoryStatementsTr3.rpt")
                PROC_INVENTORY_STATEMENT = CHNLSVC.MsgPortal.ProcessMovementListing(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportProfit, BaseCls.GlbReportCompCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3);
            else
                PROC_INVENTORY_STATEMENT = CHNLSVC.MsgPortal.ProcessInventoryStatement(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportProfit, BaseCls.GlbReportCompCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3);

            //PROC_INVENTORY_STATEMENT.WriteXml(@"D:\Sanjeewa\MyDataset.xml");
            //for (Int32 i = 1; i <= 30000; i++)
            //{
            //    Console.WriteLine(i);
            //}
            MST_LOC = CHNLSVC.Inventory.Get_all_LocationsTable(BaseCls.GlbReportCompCode);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            if (BaseCls.GlbReportName == "InventoryStatements.rpt") //Sanjeewa 2013-06-21
            {
                _invSts.Database.Tables["PROC_INVENTORY_STATEMENT"].SetDataSource(PROC_INVENTORY_STATEMENT);
                _invSts.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                _invSts.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _invSts.Database.Tables["param"].SetDataSource(param);
            }
            else if (BaseCls.GlbReportName == "InventoryStatementsTr.rpt")
            {
                _invStsTr.Database.Tables["PROC_INVENTORY_STATEMENT"].SetDataSource(PROC_INVENTORY_STATEMENT);
                _invStsTr.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                _invStsTr.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _invStsTr.Database.Tables["param"].SetDataSource(param);
            }
            else if (BaseCls.GlbReportName == "InventoryStatementsTr3.rpt")
            {
                _invStsTr3.Database.Tables["PROC_INVENTORY_STATEMENT"].SetDataSource(PROC_INVENTORY_STATEMENT);
                _invStsTr3.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                _invStsTr3.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _invStsTr3.Database.Tables["param"].SetDataSource(param);
            }
            else if (BaseCls.GlbReportName == "InventoryStatementsTr3_new.rpt")
            {
                _invStsTr3_new.Database.Tables["PROC_INVENTORY_STATEMENT"].SetDataSource(PROC_INVENTORY_STATEMENT);
                _invStsTr3_new.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                _invStsTr3.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _invStsTr3.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _invStsTr2.Database.Tables["PROC_INVENTORY_STATEMENT"].SetDataSource(PROC_INVENTORY_STATEMENT);
                _invStsTr2.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

                _invStsTr2.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _invStsTr2.Database.Tables["param"].SetDataSource(param);
            }
        }

        public void GiftVoucherPrintReport()
        {// Sanjeewa 21-11-2013
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);

            //System.Drawing.Printing.PaperSize paperSize = new System.Drawing.Printing.PaperSize("MyCustomSize", 600, 400);
            //paperSize.RawKind = (int)PaperKind.Custom;
            // pdoc.DefaultPageSettings.PaperSize = paperSize;            
            //pdoc.DefaultPageSettings.Margins = new Margins(220, 0, 0, 0);
            //pdoc.DefaultPageSettings.PaperSize.Height = 400;
            //pdoc.DefaultPageSettings.PaperSize.Width = 700;
            //pdoc.DefaultPageSettings.Landscape = true;

            pd.Document = pdoc;

            //pdoc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("GvSize", 600, 400);
            //pdoc.DefaultPageSettings.PaperSize.RawKind = (int)PaperKind.Custom;

            //pdoc.DefaultPageSettings.PaperSize.Height = 520;
            //pdoc.DefaultPageSettings.PaperSize.Width = 820;

            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                //DataTable GV_PRINT1 = CHNLSVC.Inventory.GetGVPrintDetails(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, BaseCls.GlbReportBook, BaseCls.GlbReportFromPage, BaseCls.GlbReportToPage);
                DataTable GV_PRINT1 = CHNLSVC.Inventory.GetGVPrintDetails(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, 0, 0, 0);
                DataTable GV_PRINTPage = GV_PRINT1.DefaultView.ToTable(true, "gvi_book", "gvi_page", "gvp_cus_name", "gvp_from");

                if (GV_PRINTPage.Rows.Count > 0)
                {
                    foreach (DataRow drow in GV_PRINTPage.Rows)
                    {
                        GV_PRINTDoc = new DataTable();
                        BaseCls.GlbReportDoc1 = "";
                        BaseCls.GlbReportDoc2 = "";
                        BaseCls.GlbReportCustomerCode = "";
                        BaseCls.GlbReportExecCode = "";

                        GV_PRINTDoc = GV_PRINT1.Select("gvi_book = '" + drow["gvi_book"].ToString() + "' AND gvi_page = '" + drow["gvi_page"].ToString() + "'").CopyToDataTable();

                        BaseCls.GlbReportDoc1 = drow["gvi_book"].ToString();
                        BaseCls.GlbReportDoc2 = drow["gvi_page"].ToString();
                        BaseCls.GlbReportCustomerCode = drow["gvp_cus_name"].ToString();
                        BaseCls.GlbReportExecCode = drow["gvp_from"].ToString();

                        DialogResult dialogResult = MessageBox.Show("Insert the Gift Voucher - Book : " + BaseCls.GlbReportDoc1 + " , Page : " + BaseCls.GlbReportDoc2 + " - 1 st side to the printer & Press Ok.", "Gift Voucher Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (dialogResult == DialogResult.OK)
                        {
                            BaseCls.GlbReportnoofDays = 1;
                            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
                            pdoc.Print();
                        }

                        DialogResult dialogResult1 = MessageBox.Show("Insert the Gift Voucher - Book : " + BaseCls.GlbReportDoc1 + " , Page : " + BaseCls.GlbReportDoc2 + " - back side to the printer & Press Ok.", "Gift Voucher Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (dialogResult1 == DialogResult.OK)
                        {
                            BaseCls.GlbReportnoofDays = 2;
                            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
                            pdoc.Print();
                        }
                    }
                }

            }

        }

        public void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Tahoma", 10);
            float fontHeight = font.GetHeight();
            int startX = 110;
            int startY = 120;
            int Offset = 25;
            int OffsetX = 110;

            if (BaseCls.GlbReportnoofDays == 1)
            {
                startX = 110 + OffsetX;
                graphics.DrawString(BaseCls.GlbReportCustomerCode, new Font("Tahoma", 10),
                                new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 30;
                graphics.DrawString(BaseCls.GlbReportExecCode, new Font("Tahoma", 10),
                                new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
                graphics.DrawString("“Please refer overleaf for Details of Gift”", new Font("Tahoma", 10),
                                new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 20;
            }
            else
            {
                DataTable GV_PrintCat = GV_PRINTDoc.DefaultView.ToTable(true, "gvi_val");
                DataTable GV_PrintCatDtl = new DataTable();

                int j = 0;
                startY = 10;

                if (GV_PrintCat.Rows.Count > 4)
                {
                    MessageBox.Show("Only Four (4) Categories are allowed to be printed on one Gift Voucher.", "Gift Voucher Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (GV_PrintCat.Rows.Count > 0)
                {
                    foreach (DataRow drowcat in GV_PrintCat.Rows)
                    {
                        j = j + 1;
                        if (j > 1)
                        {
                            Offset = Offset + 30;
                        }
                        if (j == 3)
                        {
                            Offset = 20;
                        }

                        if (j > 2)
                        {
                            startX = 310 + OffsetX;
                        }
                        else
                        {
                            startX = 10 + OffsetX;
                        }
                        graphics.DrawString("Option " + j + " :", new Font("Tahoma", 8, FontStyle.Underline), new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 20;

                        GV_PrintCatDtl = GV_PRINTDoc.Select("gvi_val = '" + drowcat["gvi_val"].ToString() + "'").CopyToDataTable();
                        int i = 0;
                        if (GV_PrintCatDtl.Rows.Count > 0)
                        {
                            DataTable _mytbl = GV_PrintCatDtl.AsEnumerable().OrderByDescending(x => x.Field<Int16>("gvi_verb")).OrderBy(x => x.Field<string>("gvi_val")).ToList().CopyToDataTable();
                            foreach (DataRow drow in _mytbl.Rows)
                            {
                                i = i + 1;
                                string itemdesc = CHNLSVC.Inventory.getItemDescription(drow["gvi_itm"].ToString());

                                if (j > 2)
                                {
                                    startX = 310 + OffsetX;
                                }
                                else
                                {
                                    startX = 10 + OffsetX;
                                }

                                graphics.DrawString(itemdesc + " - " + drow["gvi_itm"].ToString(), new Font("Tahoma", 8),
                                                new SolidBrush(Color.Black), startX, startY + Offset);
                                Offset = Offset + 15;

                                if (i < GV_PrintCatDtl.Rows.Count)
                                {
                                    if (j > 2)
                                    {
                                        startX = 450 + OffsetX;
                                    }
                                    else
                                    {
                                        startX = 150 + OffsetX;
                                    }
                                    graphics.DrawString(drow["gvi_verb"].ToString() == "1" ? " & " : " OR ", new Font("Tahoma", 8), new SolidBrush(Color.Black), startX, startY + Offset);
                                    Offset = Offset + 15;
                                }

                            }
                        }
                    }
                }

            }

        }

        public void GRAN_Report()
        {
            // Sanjeewa 25-03-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable GRAN_DETAIL = CHNLSVC.MsgPortal.GetSGRANDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbStatus, BaseCls.GlbReportType);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("itemcat1", typeof(string));
            param.Columns.Add("itemcat2", typeof(string));
            param.Columns.Add("itemcat3", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("doctype", typeof(string));
            param.Columns.Add("docStatus", typeof(string));
            param.Columns.Add("showcost", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcat1"] = BaseCls.GlbReportItemCat1;
            dr["itemcat2"] = BaseCls.GlbReportItemCat2;
            dr["itemcat3"] = BaseCls.GlbReportItemCat3;
            dr["itemcode"] = BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel;
            dr["doctype"] = BaseCls.GlbReportDocType;
            dr["docStatus"] = BaseCls.GlbStatus == "F" ? "Finished" : BaseCls.GlbStatus == "C" ? "Cancelled" : BaseCls.GlbStatus == "A" ? "Approved" : BaseCls.GlbStatus == "P" ? "Pending" : BaseCls.GlbStatus == "R" ? "Rejected" : "ALL";
            dr["showcost"] = BaseCls.GlbReportWithCost;

            param.Rows.Add(dr);

            _GRAN.Database.Tables["GRAN_DETAIL"].SetDataSource(GRAN_DETAIL);
            _GRAN.Database.Tables["param"].SetDataSource(param);

        }

        public void FAT_Report()
        {
            // Sanjeewa 05-02-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();

                    TMP_DataTable = CHNLSVC.MsgPortal.GetFATDetails1(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbStatus);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("itemcat1", typeof(string));
            param.Columns.Add("itemcat2", typeof(string));
            param.Columns.Add("itemcat3", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("doctype", typeof(string));
            param.Columns.Add("docStatus", typeof(string));
            param.Columns.Add("showcost", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcat1"] = BaseCls.GlbReportItemCat1;
            dr["itemcat2"] = BaseCls.GlbReportItemCat2;
            dr["itemcat3"] = BaseCls.GlbReportItemCat3;
            dr["itemcode"] = BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel;
            dr["doctype"] = BaseCls.GlbReportDocType;
            dr["docStatus"] = BaseCls.GlbStatus == "F" ? "Finished" : BaseCls.GlbStatus == "C" ? "Cancelled" : BaseCls.GlbStatus == "A" ? "Approved" : BaseCls.GlbStatus == "P" ? "Pending" : BaseCls.GlbStatus == "R" ? "Rejected" : "ALL";
            dr["showcost"] = BaseCls.GlbReportWithCost;

            param.Rows.Add(dr);

            _FAT.Database.Tables["FAT_DTL"].SetDataSource(GLOB_DataTable);
            _FAT.Database.Tables["param"].SetDataSource(param);

        }

        public void OthershopDO_Report()
        {
            // Sanjeewa 14-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable OTHPCDO_DETAIL = CHNLSVC.MsgPortal.GetOtherShopDO(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbUserID, BaseCls.GlbReportDoc, 0);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("itemcat1", typeof(string));
            param.Columns.Add("itemcat2", typeof(string));
            param.Columns.Add("itemcat3", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("docno", typeof(string));
            param.Columns.Add("customer", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcat1"] = BaseCls.GlbReportItemCat1;
            dr["itemcat2"] = BaseCls.GlbReportItemCat2;
            dr["itemcat3"] = BaseCls.GlbReportItemCat3;
            dr["itemcode"] = BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel;
            dr["docno"] = BaseCls.GlbReportDoc;
            dr["customer"] = BaseCls.GlbReportCustomerCode;

            param.Rows.Add(dr);

            _OthShopDO.Database.Tables["OTHER_DOC_DO"].SetDataSource(OTHPCDO_DETAIL);
            _OthShopDO.Database.Tables["param"].SetDataSource(param);

        }

        public void ReservedSerial_Report()
        {
            // Sanjeewa 11-12-2013
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = CHNLSVC.MsgPortal.GetReservedSerialDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }


            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("period", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");

            param.Rows.Add(dr);

            _ResSer.Database.Tables["RES_SER"].SetDataSource(GLOB_DataTable);
            _ResSer.Database.Tables["param"].SetDataSource(param);

        }

        public void Intr_Report()
        {
            // Sanjeewa 23-08-2013
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = CHNLSVC.MsgPortal.GetSIntrDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbStatus, BaseCls.GlbReportType, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("itemcat1", typeof(string));
            param.Columns.Add("itemcat2", typeof(string));
            param.Columns.Add("itemcat3", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("doctype", typeof(string));
            param.Columns.Add("docStatus", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcat1"] = BaseCls.GlbReportItemCat1;
            dr["itemcat2"] = BaseCls.GlbReportItemCat2;
            dr["itemcat3"] = BaseCls.GlbReportItemCat3;
            dr["itemcode"] = BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel;
            dr["doctype"] = BaseCls.GlbReportDocType;
            dr["docStatus"] = BaseCls.GlbStatus == "F" ? "Finished" : BaseCls.GlbStatus == "C" ? "Cancelled" : BaseCls.GlbStatus == "A" ? "Approved" : BaseCls.GlbStatus == "P" ? "Pending" : BaseCls.GlbStatus == "R" ? "Rejected" : "ALL";

            param.Rows.Add(dr);

            _Intr.Database.Tables["GRAN_DETAIL"].SetDataSource(GLOB_DataTable);
            _Intr.Database.Tables["param"].SetDataSource(param);

        }

       

        public void InsuredStock_Report()
        {
            // Sanjeewa 13-07-2015
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = CHNLSVC.MsgPortal.GetInsuredStockDetails(BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            _insustk.Database.Tables["INSU_STOCK"].SetDataSource(GLOB_DataTable);
            _insustk.Database.Tables["param"].SetDataSource(param);

        }

        public void TempSaveDoc_Report()
        {
            // Sanjeewa 30-10-2015
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = CHNLSVC.MsgPortal.GetInsuredStockDetails(BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("doctype", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["doctype"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;

            param.Rows.Add(dr);

            _tempsave.Database.Tables["TEMP_SAVED_DOC"].SetDataSource(GLOB_DataTable);
            _tempsave.Database.Tables["param"].SetDataSource(param);

        }

        public void CurrBalwithPrice_Report()
        {
            // Sanjeewa 20-01-2016
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = CHNLSVC.MsgPortal.GetCurrBalancewithPriceDetails(BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportSupplier, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("supplier", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["supplier"] = BaseCls.GlbReportSupplier == "" ? "ALL" : BaseCls.GlbReportSupplier;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;

            param.Rows.Add(dr);

            _CurrBalPrice.Database.Tables["STOCK_BAL_PRICE"].SetDataSource(GLOB_DataTable);
            _CurrBalPrice.Database.Tables["param"].SetDataSource(param);

        }

        public void GITAsat_Report()
        {
            // Sanjeewa 02-11-2016
            DataTable param = new DataTable();
            DataRow dr;

            GLOB_DataTable = CHNLSVC.MsgPortal.getGITReport_ASAT1(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbUserID, BaseCls.GlbReportOtherLoc, BaseCls.GlbReportDoc);

            param.Clear();

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
            param.Columns.Add("withcost", typeof(Int32));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["cat4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4;
            dr["cat5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5;
            dr["withcost"] = BaseCls.GlbReportWithCost;

            param.Rows.Add(dr);

            //DataTable SERIAL_DETAIL = new DataTable();
            //SERIAL_DETAIL.Columns.Add("DOC_NO", typeof(string));
            //SERIAL_DETAIL.Columns.Add("ITEM_CODE", typeof(string));
            //SERIAL_DETAIL.Columns.Add("SERIAL_NO", typeof(string));
            //SERIAL_DETAIL.TableName = "tbl";

            _GitAsat.Database.Tables["GIT_RECONCILIATION"].SetDataSource(GLOB_DataTable);
            _GitAsat.Database.Tables["param"].SetDataSource(param);

            //foreach (object repOp in _GitAsat.ReportDefinition.ReportObjects)
            //{
            //    string _s = repOp.GetType().ToString();
            //    if (_s.ToLower().Contains("subreport"))
            //    {
            //        SubreportObject _cs = (SubreportObject)repOp;
            //        if (_cs.SubreportName == "GIT_Serial_sub_rpt")
            //        {
            //            ReportDocument subRepDoc = _GitAsat.Subreports[_cs.SubreportName];
            //            subRepDoc.Database.Tables["GIT_SERIAL"].SetDataSource(SERIAL_DETAIL);
            //            //subRepDoc.Close();
            //            //subRepDoc.Dispose();
            //        }

            //    }
            //}
        }

        public void Curr_Age_Report()
        {
            // Sanjeewa 30-03-2016
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = CHNLSVC.MsgPortal.getCurrentAgeDetails(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportSupplier, BaseCls.GlbUserID, BaseCls.GlbReportPromotor);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            DataTable dtheding = new DataTable();
            dtheding = CHNLSVC.General.GET_REF_AGE_SLOT(BaseCls.GlbUserComCode);
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
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["itemstatus"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["cat4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4;
            dr["cat5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5;
            dr["withcost"] = BaseCls.GlbReportWithCost;
            dr["withserial"] = 0;
            dr["withstatus"] = 0;
            dr["supplier"] = BaseCls.GlbReportSupplier == "" ? "ALL" : BaseCls.GlbReportSupplier;

            param.Rows.Add(dr);

            _CurrAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
            _CurrAge.Database.Tables["param"].SetDataSource(param);
            _CurrAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);

        }
        public void inventoryBalanceWithCost()
        {
            //Nadeeka 18-06-2013
            DataTable param = new DataTable();
            DataTable PROC_INVENTORY_BALANCE = new DataTable();
            DataRow dr;


            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);


            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV_BAL = new DataTable();
                    Debug.Print(drow["tpl_pc"].ToString());
                    TMP_INV_BAL = CHNLSVC.MsgPortal.GetStockBalanceWithCost(BaseCls.GlbUserID, BaseCls.GlbReportChannel, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemStatus, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportWithCost, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportWithSerial, BaseCls.GlbReportWithStatus, drow["tpl_pc"].ToString(), BaseCls.GlbReportComp, BaseCls.GlbReportDoc, BaseCls.GlbReportDoc1);

                    PROC_INVENTORY_BALANCE.Merge(TMP_INV_BAL);
                }
            }


            DataTable INV_SER = new DataTable();


            param.Clear();

            param.Columns.Add("user", typeof(string));
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
            param.Columns.Add("withcost", typeof(Int16));
            param.Columns.Add("withserial", typeof(Int16));
            param.Columns.Add("withstatus", typeof(Int16));
            param.Columns.Add("color", typeof(string));
            param.Columns.Add("size", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["channel"] = BaseCls.GlbReportChannel == "" ? "ALL" : BaseCls.GlbReportChannel;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["itemstatus"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["withcost"] = BaseCls.GlbReportWithCost;
            dr["withserial"] = BaseCls.GlbReportWithSerial;
            dr["withstatus"] = BaseCls.GlbReportWithStatus;
            dr["color"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc;
            dr["size"] = BaseCls.GlbReportDoc1 == "" ? "ALL" : BaseCls.GlbReportDoc1;
            param.Rows.Add(dr);

            if (BaseCls.GlbReportDocType == "NOR")
            {
                _invBalCst.Database.Tables["STOCK_BAL"].SetDataSource(PROC_INVENTORY_BALANCE);
                // if (BaseCls.GlbReportWithSerial == 1) { _invBalCst.Database.Tables["BAL_SERIAL"].SetDataSource(INV_SER); }
                _invBalCst.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _invBalCstAST.Database.Tables["STOCK_BAL"].SetDataSource(PROC_INVENTORY_BALANCE);
                // if (BaseCls.GlbReportWithSerial == 1) { _invBalCst.Database.Tables["BAL_SERIAL"].SetDataSource(INV_SER); }
                _invBalCstAST.Database.Tables["param"].SetDataSource(param);

                DataTable SUPP_ITEM = new DataTable();
                if (PROC_INVENTORY_BALANCE.Rows.Count > 0)
                {
                    foreach (DataRow drow in PROC_INVENTORY_BALANCE.Rows)
                    {
                        DataTable _supp = CHNLSVC.MsgPortal.PhyStkBalCommStkDetails(BaseCls.GlbUserComCode, drow["ITEM_CODE"].ToString());
                        SUPP_ITEM.Merge(_supp);
                    }
                }
                else
                {
                    SUPP_ITEM = CHNLSVC.MsgPortal.PhyStkBalCommStkDetails(BaseCls.GlbUserComCode, "");
                }
                _invBalCstAST.Database.Tables["SUPP_ITEM"].SetDataSource(SUPP_ITEM);
            }

        }

        public void Loc_wise_item_age()
        {

            DataTable param = new DataTable();
            DataTable GLOB_DataTable = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPcRptDB(null, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                DataTable TMP_DataTable = new DataTable();
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    //  Debug.Print (drow["tpl_pc"].ToString());
                    
                    if (BaseCls.GlbReportType == "CUR")
                        TMP_DataTable = CHNLSVC.MsgPortal.getCurrentAgeDetails(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportSupplier, BaseCls.GlbUserID, BaseCls.GlbReportPromotor);
                    else if (BaseCls.GlbReportType == "CURCOM")
                    {
                        TMP_DataTable = CHNLSVC.MsgPortal.getCurrentComAgeDetails_crystal(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportSupplier, BaseCls.GlbUserID, BaseCls.GlbReportRole, "BAL");
                    }
                    else
                    {
                        TMP_DataTable = CHNLSVC.MsgPortal.getAsAtAgeDetails(BaseCls.GlbReportFromDate.Date, BaseCls.GlbReportToDate.Date, drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportSupplier, BaseCls.GlbUserID, BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFast, BaseCls.GlbReportPriceBook, BaseCls.GlbReportPBLevel);

                    }
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
                if (BaseCls.GlbReportRole == "Y")
                {
                    TMP_DataTable = CHNLSVC.MsgPortal.getCurrentComAgeDetails_crystal(BaseCls.GlbUserComCode, "", BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportSupplier, BaseCls.GlbUserID, BaseCls.GlbReportRole, "GIT");
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }
            DataTable dtheding = new DataTable();
            dtheding = CHNLSVC.General.GET_REF_AGE_SLOT(BaseCls.GlbUserComCode);
            
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
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["itemstatus"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            dr["withcost"] = BaseCls.GlbReportWithCost;
            dr["withserial"] = 0;
            dr["withstatus"] = 0;
            dr["supplier"] = BaseCls.GlbReportSupplier == "" ? "ALL" : BaseCls.GlbReportSupplier;

            param.Rows.Add(dr);

            if (BaseCls.GlbReportName == "LocwiseItemAge.rpt")
            {
                _locAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                _locAge.Database.Tables["param"].SetDataSource(param);

                _locAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            }
            if (BaseCls.GlbReportName == "CatwiseItemAge.rpt")
            {
                _catAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                _catAge.Database.Tables["param"].SetDataSource(param);
                _catAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            }
            if (BaseCls.GlbReportName == "CatScatwiseItemAge.rpt")
            {
                _catScatAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                _catScatAge.Database.Tables["param"].SetDataSource(param);
                _catScatAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            }
            if (BaseCls.GlbReportName == "StuswiseItemAge.rpt")
            {
                //  _StusAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                // _StusAge.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "ItmBrndwiseItemAge.rpt")
            {
                _ItmBrndAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                _ItmBrndAge.Database.Tables["param"].SetDataSource(param);
                _ItmBrndAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            }
            if (BaseCls.GlbReportName == "CatItmwiseItemAge.rpt")
            {
                _catItmAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                _catItmAge.Database.Tables["param"].SetDataSource(param);
                _catItmAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            }
            if (BaseCls.GlbReportName == "ItmwiseItemAge.rpt")
            {
                _ItmAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                _ItmAge.Database.Tables["param"].SetDataSource(param);
                _ItmAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            }
            if (BaseCls.GlbReportName == "LocItemStusAge.rpt")
            {
                _LocItmAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                _LocItmAge.Database.Tables["param"].SetDataSource(param);
                _LocItmAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            }
            if (BaseCls.GlbReportName == "LocItemStusAgenew.rpt")
            {
                _itmwise.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
                _itmwise.Database.Tables["param"].SetDataSource(param);
                _itmwise.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            }


        }

        public void POSummary()
        {
            //Nadeeka 29-04-2015
            DataTable param = new DataTable();
            DataTable glb_po_summary = new DataTable();
            DataRow dr;


            //tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);

            //if (tmp_user_pc.Rows.Count > 0)
            //{
            //    foreach (DataRow drow in tmp_user_pc.Rows)
            //{
            DataTable TMP_INV_BAL = new DataTable();
            TMP_INV_BAL = CHNLSVC.MsgPortal.GetpoSummary(BaseCls.GlbReportCompCode, BaseCls.GlbReportDocSubType, BaseCls.GlbReportSupplier, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
            glb_po_summary.Merge(TMP_INV_BAL);


            //    }
            //}



            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));


            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["docType"] = BaseCls.GlbReportDocSubType == "" ? "ALL" : BaseCls.GlbReportDocSubType;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["Direct"] = BaseCls.GlbReportSupplier == "" ? "ALL" : BaseCls.GlbReportSupplier;
            dr["Model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["Brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["itemCode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;

            _poSummary.Database.Tables["glb_po_summary"].SetDataSource(glb_po_summary);
            _poSummary.Database.Tables["param"].SetDataSource(param);

        }

        public void inventoryBalance()
        {
            // Sanjeewa 01-03-2013
            DataTable param = new DataTable();
            DataRow dr;

            //DataTable TMP_SER = CHNLSVC.MsgPortal.PrintTransDetListReport("ABL", "DPS43", Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date,null,null,null,null,null,null,null,null);

            DataTable PROC_INVENTORY_BALANCE = CHNLSVC.MsgPortal.GetStockBalance(BaseCls.GlbUserID, BaseCls.GlbReportChannel, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemStatus, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportWithCost, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportWithSerial, BaseCls.GlbReportDiscontinueItems);
            DataTable INV_SER = new DataTable();
            if (BaseCls.GlbReportWithSerial == 1) { INV_SER = CHNLSVC.MsgPortal.GetSerialBalance(); }

            param.Clear();

            param.Columns.Add("user", typeof(string));
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
            param.Columns.Add("withcost", typeof(Int16));
            param.Columns.Add("withserial", typeof(Int16));
            param.Columns.Add("withstatus", typeof(Int16));


            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["channel"] = BaseCls.GlbReportChannel == "" ? "ALL" : BaseCls.GlbReportChannel;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["itemstatus"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["withcost"] = BaseCls.GlbReportWithCost;
            dr["withserial"] = BaseCls.GlbReportWithSerial;
            dr["withstatus"] = BaseCls.GlbReportParaLine1;

            param.Rows.Add(dr);

            if (BaseCls.GlbReportDocType == "NOR")
            {
                //  _invBal.Database.Tables["STOCK_BAL"].SetDataSource(PROC_INVENTORY_BALANCE);
                if (BaseCls.GlbReportWithSerial == 1) { _invBal.Database.Tables["SERIAL_BAL"].SetDataSource(INV_SER); }
                _invBal.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                //  _invBalAST.Database.Tables["STOCK_BAL"].SetDataSource(PROC_INVENTORY_BALANCE);
                if (BaseCls.GlbReportWithSerial == 1) { _invBalAST.Database.Tables["SERIAL_BAL"].SetDataSource(INV_SER); }
                _invBalAST.Database.Tables["param"].SetDataSource(param);
                DataTable SUPP_ITEM = new DataTable();
                if (PROC_INVENTORY_BALANCE.Rows.Count > 0)
                {
                    foreach (DataRow drow in PROC_INVENTORY_BALANCE.Rows)
                    {
                        DataTable _supp = CHNLSVC.MsgPortal.PhyStkBalCommStkDetails(BaseCls.GlbUserComCode, drow["ITEM_CODE"].ToString());
                        SUPP_ITEM.Merge(_supp);
                    }
                }
                else
                {
                    SUPP_ITEM = CHNLSVC.MsgPortal.PhyStkBalCommStkDetails(BaseCls.GlbUserComCode, "");
                }
                //DataTable SUPP_ITEM = CHNLSVC.MsgPortal.PhyStkBalCommStkDetails(BaseCls.GlbUserComCode);
                _invBalAST.Database.Tables["SUPP_ITEM"].SetDataSource(SUPP_ITEM);
            }

        }

        public void inventoryBalanceSerial()
        {
            // Sanjeewa 01-03-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable PROC_INVENTORY_BALANCE = CHNLSVC.MsgPortal.GetStockBalanceWithSerial(BaseCls.GlbUserID, BaseCls.GlbReportChannel, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemStatus, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportWithCost, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportWithSerial, BaseCls.GlbReportWithStatus);
            DataTable INV_SER = new DataTable();
            if (BaseCls.GlbReportWithSerial == 1) { INV_SER = CHNLSVC.MsgPortal.GetSerialBalance(); }

            param.Clear();

            param.Columns.Add("user", typeof(string));
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
            param.Columns.Add("withcost", typeof(Int16));
            param.Columns.Add("withserial", typeof(Int16));
            param.Columns.Add("withstatus", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["channel"] = BaseCls.GlbReportChannel == "" ? "ALL" : BaseCls.GlbReportChannel;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["itemstatus"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["withcost"] = BaseCls.GlbReportWithCost;
            dr["withserial"] = BaseCls.GlbReportWithSerial;
            dr["withstatus"] = BaseCls.GlbReportWithStatus;
            param.Rows.Add(dr);

            _invBalSrl.Database.Tables["STOCK_BAL"].SetDataSource(PROC_INVENTORY_BALANCE);
            if (BaseCls.GlbReportWithSerial == 1) { _invBalSrl.Database.Tables["BAL_SERIAL"].SetDataSource(INV_SER); }
            _invBalSrl.Database.Tables["param"].SetDataSource(param);

        }

        public void inventoryBalanceSerial_Asat()
        {
            // Nadeeka 30-11-2013
            DataTable param = new DataTable();
            DataTable PROC_INVENTORY_BALANCE = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV = new DataTable();
                    TMP_INV = CHNLSVC.MsgPortal.GetStockBalanceWithSerial_Asat(BaseCls.GlbUserID, BaseCls.GlbReportChannel, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemStatus, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportWithCost, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportWithSerial, BaseCls.GlbReportWithStatus, drow["tpl_pc"].ToString(), BaseCls.GlbReportCompCode);
                    PROC_INVENTORY_BALANCE.Merge(TMP_INV);


                }
            }



            param.Clear();

            param.Columns.Add("user", typeof(string));
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
            param.Columns.Add("withcost", typeof(Int16));
            param.Columns.Add("withserial", typeof(Int16));
            param.Columns.Add("withstatus", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["channel"] = BaseCls.GlbReportChannel == "" ? "ALL" : BaseCls.GlbReportChannel;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["itemstatus"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["withcost"] = BaseCls.GlbReportWithCost;
            dr["withserial"] = BaseCls.GlbReportWithSerial;
            dr["withstatus"] = BaseCls.GlbReportWithStatus;
            param.Rows.Add(dr);

            _invBalSrlAsat.Database.Tables["STOCK_BAL"].SetDataSource(PROC_INVENTORY_BALANCE);
            //  _invBalSrlAsat.Database.Tables["BAL_SERIAL"].SetDataSource(INV_SER);
            _invBalSrlAsat.Database.Tables["param"].SetDataSource(param);

        }


        public void FIFONotFollowedReport()
        {
            // Sanjeewa  29-05-2013
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();

                    TMP_DataTable = CHNLSVC.MsgPortal.GetFIFONotFollowedDetails1(BaseCls.GlbUserID, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemStatus, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, 0);
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

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            //dr["asatdate"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["itemstatus"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            param.Rows.Add(dr);

            _FIFO.Database.Tables["FIFO_NOT_FOLLOWED"].SetDataSource(GLOB_DataTable);
            _FIFO.Database.Tables["param"].SetDataSource(param);

        }

        public void ExchangePrintReport()
        {
            // Sanjeewa 08-10-2013

            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;
            _numPages = CHNLSVC.General.GetReprintDocCount(BaseCls.GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));
            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);

            DataTable EXCHG = CHNLSVC.Inventory.GetExchangeDetails(BaseCls.GlbReportDoc);
            DataTable mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            DataTable sat_receiptitm = CHNLSVC.Sales.GetExchangeReceiptDet(BaseCls.GlbReportDoc);
            DataTable sat_receipt = CHNLSVC.Sales.GetExchangeReceiptHdr(BaseCls.GlbReportDoc);

            if (BaseCls.GlbReportName == "Exchange_Docs.rpt")
            {
                _ExchangeRep.Database.Tables["EXCHANGE_DOC"].SetDataSource(EXCHG);
                _ExchangeRep.Database.Tables["mst_com"].SetDataSource(mst_com);
                _ExchangeRep.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

                foreach (object repOp in _ExchangeRep.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Reciept")
                        {
                            ReportDocument subRepDoc = _ExchangeRep.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                            subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);
                        }

                    }
                }
            }
            else
            {
                _ExchangeRepfull.Database.Tables["EXCHANGE_DOC"].SetDataSource(EXCHG);
                _ExchangeRepfull.Database.Tables["mst_com"].SetDataSource(mst_com);
                _ExchangeRepfull.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

                foreach (object repOp in _ExchangeRepfull.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Reciept")
                        {
                            ReportDocument subRepDoc = _ExchangeRepfull.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                            subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);
                        }

                    }
                }
            }

        }

        public void SExchangePrintReport()
        {
            // Sanjeewa 08-10-2013

            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;
            _numPages = CHNLSVC.General.GetReprintDocCount(BaseCls.GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);

            DataTable EXCHG = CHNLSVC.Inventory.GetExchangeDetails(BaseCls.GlbReportDoc);
            DataTable mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            DataTable sat_receiptitm = CHNLSVC.Sales.GetExchangeReceiptDet(BaseCls.GlbReportDoc);
            DataTable sat_receipt = CHNLSVC.Sales.GetExchangeReceiptHdr(BaseCls.GlbReportDoc);

            S_ExchangeRep.Database.Tables["EXCHANGE_DOC"].SetDataSource(EXCHG);
            S_ExchangeRep.Database.Tables["mst_com"].SetDataSource(mst_com);
            S_ExchangeRep.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            foreach (object repOp in S_ExchangeRep.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Reciept")
                    {
                        ReportDocument subRepDoc = S_ExchangeRep.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);
                    }

                }
            }

        }

        public void inventoryBalance_Current()
        {
            // Sanjeewa 04-03-2013
            DataTable param = new DataTable();
            DataRow dr;
            DataTable PROC_INVENTORY_BALANCE = new DataTable();

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = CHNLSVC.MsgPortal.GetStockBalanceCurrent(BaseCls.GlbUserID, BaseCls.GlbReportChannel, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemStatus, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportWithCost, BaseCls.GlbReportWithSerial, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportDoc2, BaseCls.GlbReportWithRCC, BaseCls.GlbReportJobStatus, BaseCls.GlbReportDiscontinueItems);

                    PROC_INVENTORY_BALANCE.Merge(TMP_DataTable);
                }
            }

            DataTable INV_SER = new DataTable();
            if (BaseCls.GlbReportWithSerial == 1)
            {
                if (PROC_INVENTORY_BALANCE.Rows.Count > 0)
                {
                    foreach (DataRow drow in PROC_INVENTORY_BALANCE.Rows)
                    {
                       
                        DataTable TEMP_SER = new DataTable();
                        TEMP_SER = CHNLSVC.MsgPortal.GetSerialBalance_Curr(drow["com_code"].ToString(), drow["loc_code"].ToString(), drow["item_code"].ToString(), drow["item_status"].ToString(), BaseCls.GlbReportWithRCC);
                        INV_SER.Merge(TEMP_SER);
                    }
                }
                else
                {
                    DataTable TEMP_SER = new DataTable();
                    TEMP_SER = CHNLSVC.MsgPortal.GetSerialBalance_Curr(BaseCls.GlbUserComCode, "", "", "", BaseCls.GlbReportWithRCC);
                    INV_SER.Merge(TEMP_SER);
                }

            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
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
            param.Columns.Add("withcost", typeof(Int16));
            param.Columns.Add("withserial", typeof(Int16));
            param.Columns.Add("isjob", typeof(Int32));
            param.Columns.Add("withstatus", typeof(Int32));
            param.Columns.Add("withdet", typeof(Int32));


            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["channel"] = BaseCls.GlbReportChannel == "" ? "ALL" : BaseCls.GlbReportChannel;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["itemstatus"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["withcost"] = BaseCls.GlbReportWithCost;
            dr["withserial"] = BaseCls.GlbReportWithSerial;
            dr["withstatus"] = BaseCls.GlbReportParaLine1;    //kapila 7/12/2016
            dr["withdet"] = BaseCls.GlbReportWithDetail;        //kapila 16/3/2017
            if (BaseCls.GlbReportJobStatus == "Y")
            {
                dr["isjob"] = 1;
            }
            else
            {
                dr["isjob"] = 0;
            }

            param.Rows.Add(dr);

            _invBalWOStus = new FF.WindowsERPClient.Reports.Inventory.Stock_Balance_WO_Stus();
            _invBal = new FF.WindowsERPClient.Reports.Inventory.Stock_Balance();

            if (BaseCls.GlbReportDocType == "NOR")
            {
                if (BaseCls.GlbReportWithDetail == 1)
                {
                    if (BaseCls.GlbReportWithStatus == 1)
                    {
                        _invBal.Database.Tables["STOCK_BAL"].SetDataSource(PROC_INVENTORY_BALANCE);
                        if (BaseCls.GlbReportWithSerial == 1)
                        {
                            _invBal.Database.Tables["SERIAL_BAL"].SetDataSource(INV_SER);
                        }
                        _invBal.Database.Tables["param"].SetDataSource(param);
                    }
                    else
                    {
                        _invBalWOStus.Database.Tables["STOCK_BAL"].SetDataSource(PROC_INVENTORY_BALANCE);
                        if (BaseCls.GlbReportWithSerial == 1) 
                        { _invBalWOStus.Database.Tables["SERIAL_BAL"].SetDataSource(INV_SER); }
                        _invBalWOStus.Database.Tables["param"].SetDataSource(param);
                    }

                }
                else
                {
                    _invBalWODet.Database.Tables["STOCK_BAL"].SetDataSource(PROC_INVENTORY_BALANCE);
                    if (BaseCls.GlbReportWithSerial == 1) { _invBalWODet.Database.Tables["SERIAL_BAL"].SetDataSource(INV_SER); }
                    _invBalWODet.Database.Tables["param"].SetDataSource(param);
                }
            }
            else
            {
                _invBalAST.Database.Tables["STOCK_BAL"].SetDataSource(PROC_INVENTORY_BALANCE);
                if (BaseCls.GlbReportWithSerial == 1) { _invBalAST.Database.Tables["SERIAL_BAL"].SetDataSource(INV_SER); }
                _invBalAST.Database.Tables["param"].SetDataSource(param);
                DataTable SUPP_ITEM = new DataTable();
                if (PROC_INVENTORY_BALANCE.Rows.Count > 0)
                {
                    foreach (DataRow drow in PROC_INVENTORY_BALANCE.Rows)
                    {
                        DataTable _supp = CHNLSVC.MsgPortal.PhyStkBalCommStkDetails(BaseCls.GlbUserComCode, drow["ITEM_CODE"].ToString());
                        SUPP_ITEM.Merge(_supp);
                    }
                }
                else
                {
                    SUPP_ITEM = CHNLSVC.MsgPortal.PhyStkBalCommStkDetails(BaseCls.GlbUserComCode, "");
                }
                //DataTable SUPP_ITEM = CHNLSVC.MsgPortal.PhyStkBalCommStkDetails(BaseCls.GlbUserComCode);
                _invBalAST.Database.Tables["SUPP_ITEM"].SetDataSource(SUPP_ITEM);
            }
        }

        public void MovementAuditTrialSerial()
        {
            // Nadeeka 27-02-2013
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable VIW_INV_MOVEMENT_SERIAL = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));


            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["docType"] = BaseCls.GlbReportType == "" ? "ALL" : BaseCls.GlbReportType;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["Direct"] = BaseCls.GlbReportDirection == "" ? "ALL" : BaseCls.GlbReportDirection;
            dr["Model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["Brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["itemCode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;


            param.Rows.Add(dr);


            VIW_INV_MOVEMENT_SERIAL.Clear();
            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV_BAL = new DataTable();
                    TMP_INV_BAL = CHNLSVC.MsgPortal.ProcessMovementAuditTrialWithSerial(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocType, drow["tpl_pc"].ToString(), BaseCls.GlbReportCompCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportDirection, BaseCls.GlbUserID, BaseCls.GlbReportDocSubType);

                    VIW_INV_MOVEMENT_SERIAL.Merge(TMP_INV_BAL);


                }
            }

            //Tharindu
            if (BaseCls.GlbReportOtherLoc != null && BaseCls.GlbReportOtherLoc != "")
            {
                VIW_INV_MOVEMENT_SERIAL = VIW_INV_MOVEMENT_SERIAL.AsEnumerable().Where(c => c.Field<string>("ITH_OTH_LOC") == BaseCls.GlbReportOtherLoc).CopyToDataTable();
            }

            MST_LOC = CHNLSVC.Inventory.Get_all_LocationsTable(BaseCls.GlbReportCompCode);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            _invAuditSrl.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            //_invAudit.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _invAuditSrl.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _invAuditSrl.Database.Tables["param"].SetDataSource(param);

            foreach (object repOp in _invAuditSrl.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Serial")
                    {
                        ReportDocument subRepDoc = _invAuditSrl.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);

                    }

                }
            }


        }
        //hasith 28/01/2016
        public void MovementSummary()
        {
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable VIW_INV_MOVEMENT = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));
            param.Columns.Add("cost", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["docType"] = BaseCls.GlbReportType == "" ? "ALL" : BaseCls.GlbReportType;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["Direct"] = BaseCls.GlbReportDirection == "" ? "ALL" : BaseCls.GlbReportDirection;
            dr["Model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["Brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["itemCode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;

            dr["cost"] = BaseCls.GlbReportIsCostPrmission;
            param.Rows.Add(dr);
            VIW_INV_MOVEMENT.Clear();
            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV_BAL = new DataTable();
                    TMP_INV_BAL = CHNLSVC.MsgPortal.ProcessMovementAuditTrial(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocType, drow["tpl_pc"].ToString(), BaseCls.GlbReportCompCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportDirection, BaseCls.GlbUserID, BaseCls.GlbReportDocSubType, BaseCls.GlbReportDoc);

                    VIW_INV_MOVEMENT.Merge(TMP_INV_BAL);


                }
            }

            MST_LOC = CHNLSVC.Inventory.Get_all_LocationsTable(BaseCls.GlbReportCompCode);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            _invMovsum.Database.Tables["VIW_INV_MOVEMENT"].SetDataSource(VIW_INV_MOVEMENT);
            _invMovsum.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _invMovsum.Database.Tables["param"].SetDataSource(param);
            foreach (object repOp in _invAudit.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Serial")
                    {
                        ReportDocument subRepDoc = _invAudit.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["VIW_INV_MOVEMENT"].SetDataSource(VIW_INV_MOVEMENT);
                    }
                }
            }
            VIW_INV_MOVEMENT.Dispose();
            MST_LOC.Dispose();
            MST_LOC1.Dispose();

            param.Dispose();
            sat_hdr.Dispose();
            MST_COM.Dispose();

            VIW_INV_MOVEMENT = null;
            MST_LOC = null;
            MST_LOC1 = null;

            param = null;
            sat_hdr = null;
            MST_COM = null;

            GC.Collect();
        }

        public void Movement_Audit_Trial()
        {
            // kapila 
            DataTable glbTable = new DataTable();
            DataTable param = new DataTable();
            DataTable _dtMoveCateTp = new DataTable();

            DataRow dr;
            Int32 _isSerWise = 0;

            if (BaseCls.GlbReportName == "Movement_audit_trial_summary.rpt" || BaseCls.GlbReportName == "Movement_audit_trial_det.rpt" || BaseCls.GlbReportName == "Movement_audit_trial_cost.rpt" || BaseCls.GlbReportName == "Movement_audit_trial.rpt" || BaseCls.GlbReportName == "Movement_audit_trial_sum.rpt")
                _isSerWise = 0;
            else
                _isSerWise = 1;

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
            dr["user"] = BaseCls.GlbUserID;
            dr["company"] = BaseCls.GlbReportComp;
            dr["companies"] = ""; // BaseCls.GlbReportCompanies;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["fromdate"] = BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["location"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["channel"] = BaseCls.GlbReportChannel == "" ? "ALL" : BaseCls.GlbReportChannel;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["doctype"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["docsubtype"] = BaseCls.GlbReportDocSubType == "" ? "ALL" : BaseCls.GlbReportDocSubType;
            dr["direction"] = BaseCls.GlbReportDirection == "" ? "ALL" : BaseCls.GlbReportDirection;
            dr["doccategory"] = "";  // BaseCls.GlbReportDocCat == "" ? "ALL" : BaseCls.GlbReportDocCat;
            dr["itemrange"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["withcost"] = BaseCls.GlbReportWithCost;

            param.Rows.Add(dr);


            glbTable.Clear();
            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(null, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_Table = new DataTable();
                    tmp_Table = CHNLSVC.MsgPortal.PrintMovementAuditTrialReport(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType, BaseCls.GlbReportDirection, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, _isSerWise, BaseCls.GlbReportOtherLoc, BaseCls.GlbReportDoc, BaseCls.GlbReportParaLine1);
                    glbTable.Merge(tmp_Table);
                }
            }

            if (BaseCls.GlbReportName == "Movement_audit_trial_summary.rpt")
            {
                _dtMoveCateTp = CHNLSVC.Inventory.GetMoveSubTypeTable(null);
                _moveAuditTrialSum.Database.Tables["mst_movcatetp"].SetDataSource(_dtMoveCateTp);
                _moveAuditTrialSum.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                _moveAuditTrialSum.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Movement_audit_trial.rpt")
            {
                _moveAuditTrial.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                _moveAuditTrial.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Movement_audit_trial_cost.rpt")
            {
                _moveAuditTrialCost.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                _moveAuditTrialCost.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Movement_audit_trial_ser.rpt")
            {
                _moveAuditTrialSer.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                _moveAuditTrialSer.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Movement_audit_trial_ser_cost.rpt")
            {
                _moveAuditTrialSerCost.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                _moveAuditTrialSerCost.Database.Tables["param"].SetDataSource(param);
            }
            //if (BaseCls.GlbReportName == "SerialMovement.rpt")
            //{
            //    _serMove.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
            //    _serMove.Database.Tables["param"].SetDataSource(param);
            //}
            if (BaseCls.GlbReportName == "Movement_audit_trial_det.rpt")
            {
                _dtMoveCateTp = CHNLSVC.Inventory.GetMoveSubTypeTable(null);
                _moveAuditTrialDet.Database.Tables["mst_movcatetp"].SetDataSource(_dtMoveCateTp);
                _moveAuditTrialDet.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                _moveAuditTrialDet.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Movement_audit_trial_sum.rpt")
            {
                _moveAuditTrialList.Database.Tables["movementaudittrial"].SetDataSource(glbTable);
                _moveAuditTrialList.Database.Tables["param"].SetDataSource(param);
            }
            //}
        }

        public void MovementAuditTrial()
        {
            // Nadeeka 27-02-2013
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable VIW_INV_MOVEMENT = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));
            param.Columns.Add("cost", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["docType"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["Direct"] = BaseCls.GlbReportDirection == "" ? "ALL" : BaseCls.GlbReportDirection;
            dr["Model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["Brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["itemCode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;

            dr["cost"] = BaseCls.GlbReportIsCostPrmission;
            param.Rows.Add(dr);


            VIW_INV_MOVEMENT.Clear();
            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV_BAL = new DataTable();
                    TMP_INV_BAL = CHNLSVC.MsgPortal.ProcessMovementAuditTrial(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocType, drow["tpl_pc"].ToString(), BaseCls.GlbReportCompCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportDirection, BaseCls.GlbUserID, BaseCls.GlbReportDocSubType, BaseCls.GlbReportDoc);
                    VIW_INV_MOVEMENT.Merge(TMP_INV_BAL);
                }
            }

            if (BaseCls.GlbReportOtherLoc != null && BaseCls.GlbReportOtherLoc != "")
            {
                VIW_INV_MOVEMENT = VIW_INV_MOVEMENT.AsEnumerable().Where(c => c.Field<string>("ITH_OTH_LOC") == BaseCls.GlbReportOtherLoc).CopyToDataTable();
            }
            MST_LOC = CHNLSVC.Inventory.Get_all_LocationsTable(BaseCls.GlbReportCompCode);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            if (BaseCls.GlbReportType == "DTL")
            {
                //Akila  2017/12/06
                if (BaseCls.GlbReportGroupLastGroupCat == "CAT1")
                {
                    _InvMovementAuditTrailGrpByCate.Database.Tables["VIW_INV_MOVEMENT"].SetDataSource(VIW_INV_MOVEMENT);
                    _InvMovementAuditTrailGrpByCate.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                    _InvMovementAuditTrailGrpByCate.Database.Tables["param"].SetDataSource(param);
                }
                else
                {
                    _invAudit.Database.Tables["VIW_INV_MOVEMENT"].SetDataSource(VIW_INV_MOVEMENT);
                    //_invAudit.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                    _invAudit.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                    _invAudit.Database.Tables["param"].SetDataSource(param);
                }
                //_invAudit.Database.Tables["VIW_INV_MOVEMENT"].SetDataSource(VIW_INV_MOVEMENT);
                ////_invAudit.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                //_invAudit.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                //_invAudit.Database.Tables["param"].SetDataSource(param);

                foreach (object repOp in _invAudit.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Serial")
                        {
                            ReportDocument subRepDoc = _invAudit.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_INV_MOVEMENT"].SetDataSource(VIW_INV_MOVEMENT);
                        }
                    }
                }
            }
            else
            {
                _invAuditSum.Database.Tables["VIW_INV_MOVEMENT"].SetDataSource(VIW_INV_MOVEMENT);
                //_invAuditSum.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _invAuditSum.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _invAuditSum.Database.Tables["param"].SetDataSource(param);

                foreach (object repOp in _invAuditSum.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Serial")
                        {
                            ReportDocument subRepDoc = _invAuditSum.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_INV_MOVEMENT"].SetDataSource(VIW_INV_MOVEMENT);
                        }
                    }
                }
            }

            //bsObj = null;

            //Add By Chamal 22-Aug-2013
            VIW_INV_MOVEMENT.Dispose();
            MST_LOC.Dispose();
            MST_LOC1.Dispose();

            param.Dispose();
            sat_hdr.Dispose();
            MST_COM.Dispose();

            VIW_INV_MOVEMENT = null;
            MST_LOC = null;
            MST_LOC1 = null;

            param = null;
            sat_hdr = null;
            MST_COM = null;

            GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.SuppressFinalize(this);
        }

        public void MovementAuditTrial_ARL()
        {
            // Nadeeka 27-02-2013
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable VIW_INV_MOVEMENT = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));
            param.Columns.Add("cost", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["docType"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["Direct"] = BaseCls.GlbReportDirection == "" ? "ALL" : BaseCls.GlbReportDirection;
            dr["Model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["Brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["itemCode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;

            dr["cost"] = BaseCls.GlbReportIsCostPrmission;
            param.Rows.Add(dr);


            VIW_INV_MOVEMENT.Clear();
            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_INV_BAL = new DataTable();
                    TMP_INV_BAL = CHNLSVC.MsgPortal.ProcessMovementAuditTrial_ARL(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocType, drow["tpl_pc"].ToString(), BaseCls.GlbReportOtherLoc, BaseCls.GlbReportCompCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportDirection, BaseCls.GlbUserID, BaseCls.GlbReportDocSubType, BaseCls.GlbReportDoc);
                    VIW_INV_MOVEMENT.Merge(TMP_INV_BAL);
                }
            }

            MST_LOC = CHNLSVC.Inventory.Get_all_LocationsTable(BaseCls.GlbReportCompCode);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            if (BaseCls.GlbReportType == "DTL")
            {
                _invAudit_ARL.Database.Tables["VIW_INV_MOVEMENT"].SetDataSource(VIW_INV_MOVEMENT);
                //_invAudit.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _invAudit_ARL.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _invAudit_ARL.Database.Tables["param"].SetDataSource(param);

                foreach (object repOp in _invAudit_ARL.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Serial")
                        {
                            ReportDocument subRepDoc = _invAudit_ARL.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_INV_MOVEMENT"].SetDataSource(VIW_INV_MOVEMENT);
                        }
                    }
                }
            }
            else
            {
                _invAuditSum.Database.Tables["VIW_INV_MOVEMENT"].SetDataSource(VIW_INV_MOVEMENT);
                //_invAuditSum.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                _invAuditSum.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _invAuditSum.Database.Tables["param"].SetDataSource(param);

                foreach (object repOp in _invAuditSum.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Serial")
                        {
                            ReportDocument subRepDoc = _invAuditSum.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["VIW_INV_MOVEMENT"].SetDataSource(VIW_INV_MOVEMENT);
                        }
                    }
                }
            }

            //bsObj = null;

            //Add By Chamal 22-Aug-2013
            VIW_INV_MOVEMENT.Dispose();
            MST_LOC.Dispose();
            MST_LOC1.Dispose();

            param.Dispose();
            sat_hdr.Dispose();
            MST_COM.Dispose();

            VIW_INV_MOVEMENT = null;
            MST_LOC = null;
            MST_LOC1 = null;

            param = null;
            sat_hdr = null;
            MST_COM = null;

            GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.SuppressFinalize(this);
        }
        public void PurcaseOrderPrintHalfLetter()
        {
            // Nadeeka 02-04-2013
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable PUR_HDR = new DataTable();
            DataTable PUR_DEL = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();



            PUR_HDR.Clear();
            PUR_HDR = CHNLSVC.Inventory.GetPODetails(docNo);
            PUR_DEL = CHNLSVC.Inventory.GetPODeliveryDetails(docNo);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            _porpth.Database.Tables["PUR_HDR"].SetDataSource(PUR_HDR);
            _porpth.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            //  _porpt.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            foreach (object repOp in _porpth.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delDet")
                    {
                        ReportDocument subRepDoc = _porpt.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["PUR_DEL"].SetDataSource(PUR_DEL);

                    }

                }
            }

        }

        public void PurcaseOrderPrint()
        {
            // Nadeeka 02-04-2013
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable PUR_HDR = new DataTable();
            DataTable PUR_DEL = new DataTable();
            DataTable PUR_ALOC = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();



            PUR_HDR.Clear();
            PUR_HDR = CHNLSVC.Inventory.GetPODetails(docNo);
            PUR_DEL = CHNLSVC.Inventory.GetPODeliveryDetails(docNo);
            //PUR_ALOC = CHNLSVC.Inventory.GetPOAlocItems(docNo);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            _porpt.Database.Tables["PUR_HDR"].SetDataSource(PUR_HDR);
            //  _porpt.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            foreach (object repOp in _porpt.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delDet")
                    {
                        ReportDocument subRepDoc = _porpt.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["PUR_DEL"].SetDataSource(PUR_DEL);

                    }
                    ////if (_cs.SubreportName == "AlocDet")
                    ////{
                    ////    ReportDocument subRepDoc = _porpt.Subreports[_cs.SubreportName];

                    ////    subRepDoc.Database.Tables["PUR_ALOC"].SetDataSource(PUR_ALOC);

                    ////}

                }
            }
        }

        public void PurcaseOrderPrint_AST()
        {
            // Sanjeewa 05-11-2013
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;

            DataTable PUR_HDR = new DataTable();
            DataTable PUR_DEL = new DataTable();
            DataTable PUR_ALOC = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();

            PUR_HDR.Clear();
            PUR_HDR = CHNLSVC.Inventory.GetPODetails(docNo);
            PUR_DEL = CHNLSVC.Inventory.GetPODeliveryDetails(docNo);
            //PUR_ALOC = CHNLSVC.Inventory.GetPOAlocItems(docNo);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);

            _porptast.Database.Tables["PUR_HDR"].SetDataSource(PUR_HDR);
            _porptast.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            foreach (object repOp in _porptast.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delDet")
                    {
                        ReportDocument subRepDoc = _porptast.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["PUR_DEL"].SetDataSource(PUR_DEL);
                    }
                }
                //if (_cs.SubreportName == "AlocDet")
                //{
                //    ReportDocument subRepDoc = _porptast.Subreports[_cs.SubreportName];
                //    subRepDoc.Database.Tables["PUR_ALOC"].SetDataSource(PUR_ALOC);
                //}

            }
        }


        public void DamageGoodsApproval()
        {
            // Nadeeka 07-03-2013
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable PROC_DAMEGE_ITEMS = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));


            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["docType"] = BaseCls.GlbReportType == "" ? "ALL" : BaseCls.GlbReportType;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["Direct"] = BaseCls.GlbReportDirection == "" ? "ALL" : BaseCls.GlbReportDirection;
            dr["Model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["Brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["itemCode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;


            param.Rows.Add(dr);


            PROC_DAMEGE_ITEMS.Clear();

            PROC_DAMEGE_ITEMS = CHNLSVC.Inventory.DamgeGoodApproval(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportProfit, BaseCls.GlbReportCompCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbUserID);
            MST_LOC = CHNLSVC.Inventory.Get_all_LocationsTable(BaseCls.GlbReportCompCode);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            _dmgApp.Database.Tables["PROC_DAMEGE_ITEMS"].SetDataSource(PROC_DAMEGE_ITEMS);
            _dmgApp.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _dmgApp.Database.Tables["param"].SetDataSource(param);



        }
        public void FastMovingItems()
        {
            // Nadeeka 22-05-2013
            Int16 IsNon;
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;
            if (BaseCls.GlbReportName == "FastMovingItems.rpt")
            {
                IsNon = 0;
            }
            else
            {
                IsNon = 1;
            }

            DataTable GLB_FAST_MOVING_ITEMS = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("docType", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));
            param.Columns.Add("isFast", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["docType"] = BaseCls.GlbReportType == "" ? "ALL" : BaseCls.GlbReportType;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["Direct"] = BaseCls.GlbReportDirection == "" ? "ALL" : BaseCls.GlbReportDirection;
            dr["Model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["Brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["itemCode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["isFast"] = BaseCls.GlbReportIsFast;


            param.Rows.Add(dr);


            GLB_FAST_MOVING_ITEMS.Clear();
            if (IsNon == 0)
            {
                tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

                if (tmp_user_pc.Rows.Count > 0)
                {
                    foreach (DataRow drow in tmp_user_pc.Rows)
                    {
                        DataTable TMP_SER = new DataTable();
                        TMP_SER = CHNLSVC.MsgPortal.FastMovingReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportCompCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, BaseCls.GlbReportforAllCompany, BaseCls.GlbReportIsFast, drow["tpl_pc"].ToString());
                        GLB_FAST_MOVING_ITEMS.Merge(TMP_SER);
                    }
                }
                //GLB_FAST_MOVING_ITEMS = CHNLSVC.Financial.Get_Gp_Data(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, "", "", BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, "","", BaseCls.GlbReportCompCode,"", "", "", BaseCls.GlbUserDefProf, BaseCls.GlbUserComCode, "", 0, "", "", false, 0, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, 0);
                //GLB_FAST_MOVING_ITEMS = CHNLSVC.MsgPortal.FastMovingReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportCompCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, BaseCls.GlbReportforAllCompany, BaseCls.GlbReportIsFast);
            }

            else
            {
                GLB_FAST_MOVING_ITEMS = CHNLSVC.MsgPortal.NonMovingReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportCompCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, BaseCls.GlbReportforAllCompany);

            }
            MST_LOC = CHNLSVC.Inventory.Get_all_LocationsTable(BaseCls.GlbReportCompCode);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            if (IsNon == 0)
            {
                _fastMov.Database.Tables["GLB_FAST_MOVING_ITEMS"].SetDataSource(GLB_FAST_MOVING_ITEMS);
                _fastMov.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _fastMov.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _nonMov.Database.Tables["GLB_FAST_MOVING_ITEMS"].SetDataSource(GLB_FAST_MOVING_ITEMS);
                _nonMov.Database.Tables["MST_COM"].SetDataSource(MST_COM);
                _nonMov.Database.Tables["param"].SetDataSource(param);
            }


        }

        public void SerialAgeReport()
        {
            // Nadeeka 27-05-2013

            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


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
            param.Columns.Add("Direct", typeof(string));
            param.Columns.Add("Model", typeof(string));
            param.Columns.Add("Brand", typeof(string));
            param.Columns.Add("itemCode", typeof(string));
            param.Columns.Add("cost", typeof(double));// No of Days
            param.Columns.Add("showcost", typeof(Int16)); 
            param.Columns.Add("fromage", typeof(Int16));
            param.Columns.Add("toage", typeof(Int16));
            param.Columns.Add("isFast", typeof(Int16));
            param.Columns.Add("ReportDocType", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportAsAtDate;

            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["docType"] = BaseCls.GlbReportType == "" ? "ALL" : BaseCls.GlbReportType;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["Direct"] = BaseCls.GlbReportDirection == "" ? "ALL" : BaseCls.GlbReportDirection;
            dr["Model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["Brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["itemCode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["cost"] = BaseCls.GlbReportnoofDays;
            dr["showcost"] = BaseCls.GlbReportWithCost;
            dr["fromage"] = BaseCls.GlbReportFromPage;
            dr["toage"] = BaseCls.GlbReportToPage;
            dr["isFast"] = BaseCls.GlbReportIsFast;
            dr["ReportDocType"] = BaseCls.GlbReportDocType;
           
            param.Rows.Add(dr);


            BAL_SERIAL.Clear();


            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_SER = new DataTable();
                    TMP_SER = CHNLSVC.MsgPortal.SerialAgeReport(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportCompCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, BaseCls.GlbReportItemStatus, drow["tpl_pc"].ToString(), BaseCls.GlbReportFromPage, BaseCls.GlbReportToPage, BaseCls.GlbReportIsFast);

                    BAL_SERIAL.Merge(TMP_SER);
                }
            }

            MST_LOC = CHNLSVC.Inventory.Get_all_LocationsTable(BaseCls.GlbReportCompCode);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            _serAge.Database.Tables["BAL_SERIAL"].SetDataSource(BAL_SERIAL);
            _serAge.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _serAge.Database.Tables["param"].SetDataSource(param);
            _serAge.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            //   _serAge.Database.Tables["MST_ITEM"].SetDataSource(MST_ITM);

        }

        public void SerialAgeReport_SCM()
        {
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;
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
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportAsAtDate;

            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["docType"] = BaseCls.GlbReportType == "" ? "ALL" : BaseCls.GlbReportType;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["Direct"] = BaseCls.GlbReportDirection == "" ? "ALL" : BaseCls.GlbReportDirection;
            // dr["fromage"] = BaseCls.GlbReportFromAge;
            //  dr["toage"] = BaseCls.GlbReportToAge;
            dr["withcost"] = BaseCls.GlbReportWithCost;

            param.Rows.Add(dr);

            BAL_SERIAL.Clear();


            tmp_user_pc = CHNLSVC.Sales.GetTempUserPcRptDB(null, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_SER = new DataTable();
                    //TMP_SER = CHNLSVC.MsgPortal.SerialAgeReport_SCM(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserID, drow["tpl_com"].ToString(), BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportModel, BaseCls.GlbReportItemStatus, drow["tpl_pc"].ToString(), BaseCls.GlbReportFromAge, BaseCls.GlbReportToAge);

                    BAL_SERIAL.Merge(TMP_SER);


                }
            }

            MST_LOC = CHNLSVC.Inventory.Get_all_LocationsTable(BaseCls.GlbReportCompCode);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            _serAge.Database.Tables["BAL_SERIAL"].SetDataSource(BAL_SERIAL);
            _serAge.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _serAge.Database.Tables["param"].SetDataSource(param);
            _serAge.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);

        }

        public void serilaCompanyLocationAge()
        {
            // Wimal 14/08/2013 
            DataTable param = new DataTable();
            DataRow dr;

            DataTable VW_SERIALAGE = CHNLSVC.MsgPortal.GetSerialCompanyLocationAge(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbReportProfit);
            // DataTable INV_SER = new DataTable();
            // if (BaseCls.GlbReportWithSerial == 1) { INV_SER = bsObj.CHNLSVC.Inventory.GetSerialBalance(); }


            param.Clear();

            param.Columns.Add("company", typeof(string));
            param.Columns.Add("location", typeof(string));
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("AsAt", typeof(string));
            param.Columns.Add("Heading", typeof(string));
            //param.Columns.Add("asatdate", typeof(DateTime));
            //param.Columns.Add("profitcenter", typeof(string));
            //param.Columns.Add("channel", typeof(string));
            //param.Columns.Add("itemcode", typeof(string));
            //param.Columns.Add("itemstatus", typeof(string));
            //param.Columns.Add("brand", typeof(string));
            //param.Columns.Add("model", typeof(string));
            //param.Columns.Add("cat1", typeof(string));
            //param.Columns.Add("cat2", typeof(string));
            //param.Columns.Add("cat3", typeof(string));
            //param.Columns.Add("withcost", typeof(Int16));
            //param.Columns.Add("withserial", typeof(Int16));

            dr = param.NewRow();
            dr["company"] = BaseCls.GlbReportComp;
            dr["location"] = BaseCls.GlbReportProfit;
            dr["user"] = BaseCls.GlbUserID;
            dr["AsAt"] = BaseCls.GlbReportAsAtDate;
            dr["Heading"] = BaseCls.GlbReportHeading;
            //dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            //dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            //dr["channel"] = BaseCls.GlbReportChannel == "" ? "ALL" : BaseCls.GlbReportChannel;
            //dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            //dr["itemstatus"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            //dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            //dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            //dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            //dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            //dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            //dr["withcost"] = BaseCls.GlbReportWithCost;
            //dr["withserial"] = BaseCls.GlbReportWithSerial;
            param.Rows.Add(dr);

            _serialAge.Database.Tables["VW_SERIALAGE"].SetDataSource(VW_SERIALAGE);
            ////if (BaseCls.GlbReportWithSerial == 1) { _invBal.Database.Tables["SERIAL_BAL"].SetDataSource(INV_SER); }
            _serialAge.Database.Tables["param"].SetDataSource(param);

        }


        public void WarrantyClimInNote()
        {



            DataTable VIW_INV_MOVEMENT_SERIAL = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_ITM_STUS = new DataTable();
            DataTable MST_ITM1 = new DataTable();
            DataTable MST_ITM_STUS1 = new DataTable();
            DataTable MST_LOC_1 = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            DataTable mst_movcatetp = new DataTable();

            DataTable WARRANTY_CLAIM = new DataTable();

            int _numPages = 0;
            DataRow dr;
            _numPages = CHNLSVC.General.GetReprintDocCount(BaseCls.GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr = PRINT_DOC.NewRow();
            dr["NOOFPAGES"] = _numPages;
            dr["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr);

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));


            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(BaseCls.GlbReportDoc);

            // Tharindu TODO 
            //if (VIW_INV_MOVEMENT_SERIAL.Rows.Count <= 0)
            //{
            //    VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Inventory.GetWarrantyClaimAdj(BaseCls.GlbReportDoc);
            //}

            WARRANTY_CLAIM = CHNLSVC.Inventory.GetCrditNoteforWarrantyClaimScm2(BaseCls.GlbReportDoc);

            if (WARRANTY_CLAIM.Rows.Count <= 0)
            {
                WARRANTY_CLAIM = CHNLSVC.Inventory.GetCrditNoteforWarrantyClaim(BaseCls.GlbReportDoc);
            }

            DataTable sat_hdr = new DataTable();
            DataTable mst_com = new DataTable();
            mst_com = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);

            //foreach (DataRow row in VIW_INV_MOVEMENT_SERIAL.Rows)
            //{
            //    string _com = row["ITH_COM"].ToString();
            //    string _loc = row["ITH_LOC"].ToString();
            //    string _itm = row["ITS_ITM_CD"].ToString();
            //    string _othLoc = row["ITH_OTH_LOC"].ToString();
            //    int index = VIW_INV_MOVEMENT_SERIAL.Rows.IndexOf(row);
            //    if (index == 0)
            //    {
            //        MST_LOC = CHNLSVC.Sales.GetLocationCode(_com, _loc);
            //        MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(_com, _othLoc);
            //        sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
            //        mst_com = CHNLSVC.General.GetCompanyByCode(_com);
            //        mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeMainTable(row["ITH_SUB_TP"].ToString(), "ADJ");
            //    }


            //    MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
            //    MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());
            //    foreach (DataRow row1 in MST_ITM1.Rows)
            //    {
            //        dr = MST_ITM.NewRow();
            //        dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
            //        dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
            //        dr["MI_CD"] = row1["MI_CD"].ToString();
            //        MST_ITM.Rows.Add(dr);
            //    }
            //    foreach (DataRow row2 in MST_ITM_STUS1.Rows)
            //    {
            //        dr = MST_ITM_STUS.NewRow();
            //        dr["MIS_CD"] = row2["MIS_CD"].ToString();
            //        dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

            //        MST_ITM_STUS.Rows.Add(dr);
            //    }
            //}
            //  mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            //   MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);

            // _warrClaim.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            //_warrClaim.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            //_warrClaim.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            //_warrClaim.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            //_warrClaim.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            _warrClaim.Database.Tables["mst_com"].SetDataSource(mst_com);
            _warrClaim.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            _warrClaim.Database.Tables["WARRANTY_CLAIM"].SetDataSource(WARRANTY_CLAIM);
            //   _warrClaim.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);




            foreach (object repOp in _warrClaim.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "serial")
                    {
                        ReportDocument subRepDoc = _warrClaim.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);

                    }
                    if (_cs.SubreportName == "doc")
                    {
                        ReportDocument subRepDoc = _warrClaim.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);

                    }
                }
            }
        }

        //Udaya 12/04/2017
        public void DoRecPrint_Direct()
        {
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);

            pd.Document = pdoc;

            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                DialogResult dialogResult = MessageBox.Show("Insert document to the printer & Press Ok.", "Do Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dialogResult == DialogResult.OK)
                {
                    isAccess = false;
                    DOPrintDataSouces();
                    BaseCls.GlbReportnoofDays = 1;
                    pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage_DO);
                    int pageCunt = (int)Math.Ceiling((OffsetYVal / 340.0));
                    Margins margins = new Margins(100, 100, 100, 750);
                    pdoc.DefaultPageSettings.Margins = margins;
                    //items = ((IEnumerable<string>)this).GetEnumerator();
                    pdoc.Print();
                }
            }
        }

        public DataTable RemoveDuplicateRows(DataTable dTable, string colName)
        {
            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();

            //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
            //And add duplicate item value in arraylist.
            foreach (DataRow drow in dTable.Rows)
            {
                if (hTable.Contains(drow[colName]))
                    duplicateList.Add(drow);
                else
                    hTable.Add(drow[colName], string.Empty);
            }

            //Removing a list of duplicate items from datatable.
            foreach (DataRow dRow in duplicateList)
                dTable.Rows.Remove(dRow);

            //Datatable which contains unique records will be return as output.
            return dTable;
        }

        //#region data table declaration
        //private DataTable _VIW_INV_MOVEMENT_SERIAL;
        //private DataTable _MST_LOC;
        //private DataTable _MST_ITM;
        //private DataTable _MST_ITM_STUS;
        //private DataTable _MST_LOC_1;
        //private DataTable _MST_ITM1;
        //private DataTable _MST_ITM_STUS1;
        //private DataTable _mst_movcatetp;
        //private DataTable _VIW_SALES_DETAILS;
        //private DataTable _DelCustomer;
        //private DataTable _mst_profit_center;
        //private DataTable _sat_vou_det;
        //private DataTable _sat_hdr;
        //private DataTable _mst_com;
        //private DataTable _param1;
        //private DataTable _tblComDate;
        //private DataTable _PRINT_DOC;
        //private DataTable _receiptDetails;
        //private DataTable _param;
        //public DataTable VIW_INV_MOVEMENT_SERIAL
        //{
        //    get { return _VIW_INV_MOVEMENT_SERIAL; }
        //    set { _VIW_INV_MOVEMENT_SERIAL = value; }
        //}
        //public DataTable MST_LOC
        //{
        //    get { return _MST_LOC; }
        //    set { _MST_LOC = value; }
        //}
        //public DataTable MST_ITM
        //{
        //    get { return _MST_ITM; }
        //    set { _MST_ITM = value; }
        //}
        //public DataTable MST_ITM_STUS
        //{
        //    get { return _MST_ITM_STUS; }
        //    set { _MST_ITM_STUS = value; }
        //}
        //public DataTable MST_LOC_1
        //{
        //    get { return _MST_LOC_1; }
        //    set { _MST_LOC_1 = value; }
        //}
        //public DataTable MST_ITM1
        //{
        //    get { return _MST_ITM1; }
        //    set { _MST_ITM1 = value; }
        //}
        //public DataTable MST_ITM_STUS1
        //{
        //    get { return _MST_ITM_STUS1; }
        //    set { _MST_ITM_STUS1 = value; }
        //}
        //public DataTable mst_movcatetp
        //{
        //    get { return _mst_movcatetp; }
        //    set { _mst_movcatetp = value; }
        //}
        //public DataTable VIW_SALES_DETAILS
        //{
        //    get { return _VIW_SALES_DETAILS; }
        //    set { _VIW_SALES_DETAILS = value; }
        //}
        //public DataTable DelCustomer
        //{
        //    get { return _DelCustomer; }
        //    set { _DelCustomer = value; }
        //}
        //public DataTable mst_profit_center
        //{
        //    get { return _mst_profit_center; }
        //    set { _mst_profit_center = value; }
        //}
        //public DataTable sat_vou_det
        //{
        //    get { return _sat_vou_det; }
        //    set { _sat_vou_det = value; }
        //}
        //public DataTable sat_hdr
        //{
        //    get { return _sat_hdr; }
        //    set { _sat_hdr = value; }
        //}
        //public DataTable mst_com
        //{
        //    get { return _mst_com; }
        //    set { _mst_com = value; }
        //}
        //public DataTable param1
        //{
        //    get { return _param1; }
        //    set { _param1 = value; }
        //}
        //public DataTable tblComDate
        //{
        //    get { return _tblComDate; }
        //    set { _tblComDate = value; }
        //}
        //public DataTable PRINT_DOC
        //{
        //    get { return _PRINT_DOC; }
        //    set { _PRINT_DOC = value; }
        //}
        //public DataTable receiptDetails
        //{
        //    get { return _receiptDetails; }
        //    set { _receiptDetails = value; }
        //}
        //public DataTable param
        //{
        //    get { return _param; }
        //    set { _param = value; }
        //}
        //#endregion data table declaration

        public void DOPrintDataSouces()
        {
            #region Reference code for DO Direct Print..
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;
            string inv_no = "";

            DataRow dr;
            VIW_INV_MOVEMENT_SERIAL.Clear();
            VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

            param1.Columns.Add("saleType", typeof(string));
            int _numPages = 0;
            Int16 isCredit = 0;
            DataRow dr3;
            _numPages = CHNLSVC.General.GetReprintDocCount(docNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            //outwardReport2.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            MST_ITM.Columns.Add("MI_MODEL", typeof(string));
            MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
            MST_ITM.Columns.Add("MI_CD", typeof(string));
            MST_ITM.Columns.Add("MI_ITM_UOM", typeof(string));

            MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
            MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

            foreach (DataRow row in VIW_INV_MOVEMENT_SERIAL.Rows)
            {
                string _com = row["ITH_COM"].ToString();
                string _loc = row["ITH_LOC"].ToString();
                string _itm = row["ITS_ITM_CD"].ToString();
                string _othLoc = row["ITH_OTH_LOC"].ToString();
                int index = VIW_INV_MOVEMENT_SERIAL.Rows.IndexOf(row);
                if (index == 0)
                {
                    MST_LOC = CHNLSVC.Sales.GetLocationCode(_com, _loc);
                    MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(_com, _othLoc);
                    if (!string.IsNullOrEmpty(row["ITH_OTH_DOCNO"].ToString()))
                    {
                        if (row["ITH_DOC_TP"].ToString() == "DO")
                        {
                            inv_no = row["ITH_OTH_DOCNO"].ToString();
                            sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                            VIW_SALES_DETAILS = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                            //DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(row["ITH_OTH_DOCNO"].ToString()); //Sanjeewa 2017-02-15
                            DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(docNo);
                            //SearchCustomer
                            foreach (DataRow row1 in sat_hdr.Rows)
                            {
                                tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row1["SAH_COM"].ToString(), row1["SAH_PC"].ToString(), row["ITH_OTH_DOCNO"].ToString());
                            }
                            receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(row["ITH_OTH_DOCNO"].ToString());
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
                            VIW_SALES_DETAILS.Columns.Add("SAD_ITM_CD", typeof(string));
                            VIW_SALES_DETAILS.Columns.Add("sad_itm_line", typeof(Int16));

                            dr3 = VIW_SALES_DETAILS.NewRow();
                            dr3["SAD_ITM_CD"] = "1";
                            dr3["sad_itm_line"] = 1;

                            VIW_SALES_DETAILS.Rows.Add(dr3);
                        }
                    }
                    else
                    {
                        VIW_SALES_DETAILS.Columns.Add("SAD_ITM_CD", typeof(string));
                        VIW_SALES_DETAILS.Columns.Add("sad_itm_line", typeof(Int16));

                        dr3 = VIW_SALES_DETAILS.NewRow();
                        dr3["SAD_ITM_CD"] = "1";
                        dr3["sad_itm_line"] = 1;

                        VIW_SALES_DETAILS.Rows.Add(dr3);

                    }
                    if (sat_hdr.Rows.Count > 0)
                    {
                        foreach (DataRow roww in sat_hdr.Rows)
                        {
                            dr = param1.NewRow();
                            dr["saleType"] = roww["sah_inv_tp"].ToString();
                            param1.Rows.Add(dr);
                            mst_profit_center = CHNLSVC.Sales.GetProfitCenterTable(roww["SAH_COM"].ToString(), roww["SAH_PC"].ToString());
                        }
                    }
                    else
                    {
                        dr = param1.NewRow();
                        dr["saleType"] = "OTH";
                        param1.Rows.Add(dr);
                    }
                    mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                    mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                }
                
                DataTable sat_vou_det1 = CHNLSVC.Sales.getSalesGiftVouchaer(inv_no);
                sat_vou_det = sat_vou_det1.DefaultView.ToTable(true);

                MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());

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

            DataRow drr;
            param.Columns.Add("isCnote", typeof(Int16));
            if (isCredit == 1)
            {
                drr = param.NewRow();
                drr["isCnote"] = 1;
                param.Rows.Add(drr);
            }
            else
            {
                drr = param.NewRow();
                drr["isCnote"] = 0;
                param.Rows.Add(drr);
            }

            MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
            mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
            MST_ITM = MST_ITM.DefaultView.ToTable(true);
            param1 = param1.DefaultView.ToTable(true);

            #region report table bind commented
            //outwardReport2.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
            //outwardReport2.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            //outwardReport2.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
            //outwardReport2.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
            //outwardReport2.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
            //outwardReport2.Database.Tables["mst_com"].SetDataSource(mst_com);
            //outwardReport2.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
            //outwardReport2.Database.Tables["param1"].SetDataSource(param1);
            //outwardReport2.Database.Tables["param"].SetDataSource(param);

            //foreach (object repOp in outwardReport2.ReportDefinition.ReportObjects)
            //{
            //    string _s = repOp.GetType().ToString();
            //    if (_s.ToLower().Contains("subreport"))
            //    {
            //        SubreportObject _cs = (SubreportObject)repOp;
            //        if (_cs.SubreportName == "rptCustomer")
            //        {
            //            ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
            //            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
            //            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

            //        }
            //        if (_cs.SubreportName == "warr")
            //        {
            //            ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
            //            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);

            //        }
            //        if (sat_hdr.Rows.Count > 0)
            //        {
            //            if (_cs.SubreportName == "OtherDel")
            //            {
            //                ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
            //                subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
            //                subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            //            }
            //            if (tblComDate.Rows.Count > 0)
            //            {
            //                if (_cs.SubreportName == "WarrComDate")
            //                {
            //                    ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
            //                    subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
            //                }
            //            }
            //            if (_cs.SubreportName == "Vou_Det")
            //            {
            //                ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
            //                subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
            //            }

            //        }

            //    }

            //}
            #endregion #region report table bind

            this.Text = "Outward Print";

            var ty = VIW_INV_MOVEMENT_SERIAL.AsEnumerable()
                .GroupBy(r => r.Field<string>("ITS_ITM_CD"))
                .Select(gr => new
                    {
                        totQty = gr.Sum(row => row.Field<decimal>("QTY"))
                    });
            foreach (var m in ty)
            {
                QTY += m.totQty;
            }

            #endregion DO Direct Print
        }

        private static bool isRun = false;
        private static readonly object syncLock = new object();

        //Udaya 12/04/2017
        public void pdoc_PrintPage_DO(object sender, PrintPageEventArgs e)
        {
            int startX = 0;
            int startY = 0;
            int OffsetY = 5;
            int OffsetX = 110;
            try
            {
                #region start
                //lock (syncLock)
                //{
                //    if (!isRun)
                //    {
                //#region Reference code for DO Direct Print..
                //string docNo = default(string);
                //docNo = BaseCls.GlbReportDoc;

                //string inv_no = "";

                //DataRow dr;
                //VIW_INV_MOVEMENT_SERIAL.Clear();
                //VIW_INV_MOVEMENT_SERIAL = CHNLSVC.Sales.GetMovementSerials(docNo);

                //param1.Columns.Add("saleType", typeof(string));
                //int _numPages = 0;
                //Int16 isCredit = 0;
                //DataRow dr3;
                //_numPages = CHNLSVC.General.GetReprintDocCount(docNo);
                //PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
                //PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

                //dr3 = PRINT_DOC.NewRow();
                //dr3["NOOFPAGES"] = _numPages;
                //dr3["SHOWCOM"] = BaseCls.ShowComName;

                //PRINT_DOC.Rows.Add(dr3);
                ////outwardReport2.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

                //MST_ITM.Columns.Add("MI_MODEL", typeof(string));
                //MST_ITM.Columns.Add("MI_SHORTDESC", typeof(string));
                //MST_ITM.Columns.Add("MI_CD", typeof(string));
                //MST_ITM.Columns.Add("MI_ITM_UOM", typeof(string));

                //MST_ITM_STUS.Columns.Add("MIS_CD", typeof(string));
                //MST_ITM_STUS.Columns.Add("MIS_DESC", typeof(string));

                //foreach (DataRow row in VIW_INV_MOVEMENT_SERIAL.Rows)
                //{
                //    string _com = row["ITH_COM"].ToString();
                //    string _loc = row["ITH_LOC"].ToString();
                //    string _itm = row["ITS_ITM_CD"].ToString();
                //    string _othLoc = row["ITH_OTH_LOC"].ToString();
                //    int index = VIW_INV_MOVEMENT_SERIAL.Rows.IndexOf(row);
                //    if (index == 0)
                //    {
                //        MST_LOC = CHNLSVC.Sales.GetLocationCode(_com, _loc);
                //        MST_LOC_1 = CHNLSVC.Sales.GetLocationCode(_com, _othLoc);
                //        if (!string.IsNullOrEmpty(row["ITH_OTH_DOCNO"].ToString()))
                //        {
                //            if (row["ITH_DOC_TP"].ToString() == "DO")
                //            {
                //                inv_no = row["ITH_OTH_DOCNO"].ToString();
                //                sat_hdr = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                //                VIW_SALES_DETAILS = CHNLSVC.Sales.GetSalesDetails(row["ITH_OTH_DOCNO"].ToString(), null);
                //                //DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(row["ITH_OTH_DOCNO"].ToString()); //Sanjeewa 2017-02-15
                //                DelCustomer = CHNLSVC.MsgPortal.GetDeliverCustomer(docNo);
                //                foreach (DataRow row1 in sat_hdr.Rows)
                //                {
                //                    tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row1["SAH_COM"].ToString(), row1["SAH_PC"].ToString(), row["ITH_OTH_DOCNO"].ToString());
                //                }
                //                receiptDetails = CHNLSVC.Sales.GetInvoiceReceiptDet(row["ITH_OTH_DOCNO"].ToString());
                //                foreach (DataRow row1 in receiptDetails.Rows)
                //                {
                //                    if (row1["SARD_PAY_TP"].ToString() == "CRNOTE")
                //                    {
                //                        isCredit = 1;
                //                    }
                //                }
                //            }
                //            else
                //            {
                //                VIW_SALES_DETAILS.Columns.Add("SAD_ITM_CD", typeof(string));
                //                VIW_SALES_DETAILS.Columns.Add("sad_itm_line", typeof(Int16));

                //                dr3 = VIW_SALES_DETAILS.NewRow();
                //                dr3["SAD_ITM_CD"] = "1";
                //                dr3["sad_itm_line"] = 1;

                //                VIW_SALES_DETAILS.Rows.Add(dr3);
                //            }
                //        }
                //        else
                //        {
                //            VIW_SALES_DETAILS.Columns.Add("SAD_ITM_CD", typeof(string));
                //            VIW_SALES_DETAILS.Columns.Add("sad_itm_line", typeof(Int16));

                //            dr3 = VIW_SALES_DETAILS.NewRow();
                //            dr3["SAD_ITM_CD"] = "1";
                //            dr3["sad_itm_line"] = 1;

                //            VIW_SALES_DETAILS.Rows.Add(dr3);

                //        }
                //        if (sat_hdr.Rows.Count > 0)
                //        {
                //            foreach (DataRow roww in sat_hdr.Rows)
                //            {
                //                dr = param1.NewRow();
                //                dr["saleType"] = roww["sah_inv_tp"].ToString();
                //                param1.Rows.Add(dr);
                //                mst_profit_center = CHNLSVC.Sales.GetProfitCenterTable(roww["SAH_COM"].ToString(), roww["SAH_PC"].ToString());
                //            }
                //        }
                //        else
                //        {
                //            dr = param1.NewRow();
                //            dr["saleType"] = "OTH";
                //            param1.Rows.Add(dr);
                //        }
                //        mst_com = CHNLSVC.General.GetCompanyByCode(_com);
                //        mst_movcatetp = CHNLSVC.Inventory.GetMoveSubTypeTable(row["ITH_SUB_TP"].ToString());
                //    }
                //    sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(inv_no);

                //    MST_ITM1 = CHNLSVC.Sales.GetItemCode(_com, _itm);
                //    MST_ITM_STUS1 = CHNLSVC.Sales.GetItemStatus(row["ITS_ITM_STUS"].ToString());

                //    foreach (DataRow row1 in MST_ITM1.Rows)
                //    {
                //        dr = MST_ITM.NewRow();
                //        dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                //        dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                //        dr["MI_CD"] = row1["MI_CD"].ToString();
                //        dr["MI_ITM_UOM"] = row1["MI_ITM_UOM"].ToString();
                //        MST_ITM.Rows.Add(dr);
                //    }
                //    foreach (DataRow row2 in MST_ITM_STUS1.Rows)
                //    {
                //        dr = MST_ITM_STUS.NewRow();
                //        dr["MIS_CD"] = row2["MIS_CD"].ToString();
                //        dr["MIS_DESC"] = row2["MIS_DESC"].ToString();

                //        MST_ITM_STUS.Rows.Add(dr);
                //    }
                //}

                //DataRow drr;
                //param.Columns.Add("isCnote", typeof(Int16));
                //if (isCredit == 1)
                //{
                //    drr = param.NewRow();
                //    drr["isCnote"] = 1;
                //    param.Rows.Add(drr);
                //}
                //else
                //{
                //    drr = param.NewRow();
                //    drr["isCnote"] = 0;
                //    param.Rows.Add(drr);
                //}

                //MST_ITM_STUS = MST_ITM_STUS.DefaultView.ToTable(true);
                //mst_movcatetp = mst_movcatetp.DefaultView.ToTable(true);
                //MST_ITM = MST_ITM.DefaultView.ToTable(true);
                //param1 = param1.DefaultView.ToTable(true);

                //#region report table bind commented
                ////outwardReport2.Database.Tables["VIW_INV_MOVEMENT_SERIAL"].SetDataSource(VIW_INV_MOVEMENT_SERIAL);
                ////outwardReport2.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                ////outwardReport2.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                ////outwardReport2.Database.Tables["MST_LOC_1"].SetDataSource(MST_LOC_1);
                ////outwardReport2.Database.Tables["MST_ITM_STUS"].SetDataSource(MST_ITM_STUS);
                ////outwardReport2.Database.Tables["mst_com"].SetDataSource(mst_com);
                ////outwardReport2.Database.Tables["mst_movcatetp"].SetDataSource(mst_movcatetp);
                ////outwardReport2.Database.Tables["param1"].SetDataSource(param1);
                ////outwardReport2.Database.Tables["param"].SetDataSource(param);

                ////foreach (object repOp in outwardReport2.ReportDefinition.ReportObjects)
                ////{
                ////    string _s = repOp.GetType().ToString();
                ////    if (_s.ToLower().Contains("subreport"))
                ////    {
                ////        SubreportObject _cs = (SubreportObject)repOp;
                ////        if (_cs.SubreportName == "rptCustomer")
                ////        {
                ////            ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
                ////            subRepDoc.Database.Tables["DelCustomer"].SetDataSource(DelCustomer);
                ////            subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);

                ////        }
                ////        if (_cs.SubreportName == "warr")
                ////        {
                ////            ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
                ////            subRepDoc.Database.Tables["VIW_SALES_DETAILS"].SetDataSource(VIW_SALES_DETAILS);

                ////        }
                ////        if (sat_hdr.Rows.Count > 0)
                ////        {
                ////            if (_cs.SubreportName == "OtherDel")
                ////            {
                ////                ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
                ////                subRepDoc.Database.Tables["sat_hdr"].SetDataSource(sat_hdr);
                ////                subRepDoc.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                ////            }
                ////            if (tblComDate.Rows.Count > 0)
                ////            {
                ////                if (_cs.SubreportName == "WarrComDate")
                ////                {
                ////                    ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
                ////                    subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                ////                }
                ////            }
                ////            if (_cs.SubreportName == "Vou_Det")
                ////            {
                ////                ReportDocument subRepDoc = outwardReport2.Subreports[_cs.SubreportName];
                ////                subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                ////            }

                ////        }

                ////    }

                ////}
                //#endregion #region report table bind
                //this.Text = "Outward Print";
                //#endregion DO Direct Print
                //isRun = true;
                //    }
                //}
                #endregion end

                #region Data table for HP
                //DataTable GLB_HP_RECEIPT_PRINT = CHNLSVC.MsgPortal.ProcessHPReceiptPrint(BaseCls.GlbReportDoc);
                //DataTable GLB_HP_RECEIPT_PAYMENTS = CHNLSVC.MsgPortal.ProcessHPReceiptPrintPayment(BaseCls.GlbReportDoc);
                //DataTable GLB_HP_RECEIPT_PAYTYPE = CHNLSVC.MsgPortal.ProcessHPReceiptPrintPayMode(BaseCls.GlbReportDoc);
                #endregion

                Graphics graphics = e.Graphics;
                Font font = new Font("Tahoma", 8);
                float fontHeight = font.GetHeight();
                bool newpage = false;
                int count = 0;
                int counts = 0;
                int countpc = 0;
                int countcd = 0;
                int countv = 0;
                string VIW_INV_MOVEMENT_SERIAL_loc = string.Empty;
                string VIW_INV_MOVEMENT_SERIAL_date = string.Empty;
                string item_Code = string.Empty;
                string VIW_INV_MOVEMENT_SERIAL_remarks = string.Empty;
                string VIW_INV_MOVEMENT_SERIAL_othLoc = string.Empty;                

                #region Section 01
                //if (GLB_HP_RECEIPT_PRINT.Rows.Count > 0)
                //{
                //    foreach (DataRow drowh in GLB_HP_RECEIPT_PRINT.Rows)
                //    {
                //        startX = 0;

                //        if (drowh["mc_anal18"].ToString() != "")
                //        {
                //            OffsetX = 400;
                //            graphics.DrawString(drowh["mc_anal18"].ToString(), new Font("Tahoma", 8),
                //                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //            OffsetX = OffsetX + (drowh["mc_anal18"].ToString().Length * 6) + 20;
                //            graphics.DrawString(drowh["mc_tax3"].ToString(), new Font("Tahoma", 8),
                //                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        }
                //        OffsetY = OffsetY + 20;

                //        OffsetX = 180;
                //        graphics.DrawString("An. " + drowh["hpr_acc"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = 370;
                //        graphics.DrawString(drowh["hpr_acc"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = 640;
                //        graphics.DrawString(drowh["hpr_recno"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //        OffsetY = OffsetY + 20;
                //        OffsetX = 150;
                //        graphics.DrawString("Rn. " + drowh["hpr_recno"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = 370;
                //        graphics.DrawString(drowh["hpr_name"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = 640;
                //        graphics.DrawString(Convert.ToDateTime(drowh["hpr_date"]).ToString("dd/MMM/yyyy"), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //        OffsetY = OffsetY + 20;
                //        OffsetX = 450;
                //        graphics.DrawString(String.Format("{0:#,##0.00}", Convert.ToDecimal(drowh["hpr_hval"].ToString())) + "(LKR)", new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = 640;
                //        graphics.DrawString(drowh["hpr_loc"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //        OffsetY = OffsetY + 40;
                //        OffsetX = 200;
                //        graphics.DrawString(Convert.ToDateTime(drowh["hpr_date"]).ToString("dd/MMM/yyyy"), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //        OffsetY = OffsetY + 20;
                //        OffsetX = 180;
                //        graphics.DrawString("Ml. " + drowh["hpr_model"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = 300;
                //        graphics.DrawString(drowh["hpr_model"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = 580;
                //        graphics.DrawString(drowh["hpr_serial"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //        OffsetY = OffsetY + 60;
                //        OffsetX = 120;
                //        graphics.DrawString("Sn. " + drowh["hpr_serial"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //        OffsetY = OffsetY + 260;
                //        OffsetX = 120;
                //        graphics.DrawString(drowh["hpr_remark"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //        OffsetY = OffsetY + 40;
                //        OffsetX = 120;
                //        graphics.DrawString(drowh["hpr_prefix"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = 200;
                //        graphics.DrawString(drowh["hpr_manref"].ToString(), new Font("Tahoma", 11),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    }
                //}
                #endregion Section 01

                #region Section 02
                //if (GLB_HP_RECEIPT_PAYMENTS.Rows.Count > 0)
                //{
                //    OffsetY = 245;
                //    decimal _Total = 0;
                //    foreach (DataRow drowp in GLB_HP_RECEIPT_PAYMENTS.Rows)
                //    {
                //        string _desc = drowp["hpr_desc"].ToString() == "EARLY CLOSING DISCOUNT" ? "ECD" : "";

                //        OffsetY = OffsetY + 20;
                //        OffsetX = 120;
                //        graphics.DrawString(_desc, new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = 200;
                //        graphics.DrawString(String.Format("{0:#,##0.00}", Convert.ToDecimal(drowp["hpr_amt"].ToString())), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        _Total = _Total + Convert.ToDecimal(drowp["hpr_amt"].ToString());
                //        OffsetX = 300;
                //        graphics.DrawString(drowp["hpr_desc"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = 450;
                //        graphics.DrawString(String.Format("{0:#,##0.00}", Convert.ToDecimal(drowp["hpr_amt"].ToString())), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    }

                //    OffsetY = OffsetY + 20;
                //    OffsetX = 200;
                //    graphics.DrawString("------------------", new Font("Tahoma", 8),
                //                new SolidBrush(Color.Black), startX + OffsetX - 20, startY + OffsetY - 7);
                //    graphics.DrawString(String.Format("{0:#,##0.00}", _Total), new Font("Tahoma", 8),
                //                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    OffsetX = 360;
                //    graphics.DrawString("LKR", new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    OffsetX = 450;
                //    graphics.DrawString("------------------", new Font("Tahoma", 8),
                //                new SolidBrush(Color.Black), startX + OffsetX - 20, startY + OffsetY - 7);
                //    graphics.DrawString(String.Format("{0:#,##0.00}", _Total), new Font("Tahoma", 8),
                //                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //}
                #endregion

                #region Section 03
                //if (GLB_HP_RECEIPT_PAYTYPE.Rows.Count > 0)
                //{
                //    OffsetY = 245;
                //    OffsetX = 525;
                //    graphics.DrawString("Pay Mode", new Font("Tahoma", 8),
                //                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    OffsetX = OffsetX + 50;
                //    graphics.DrawString("Amount", new Font("Tahoma", 8),
                //                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    OffsetX = OffsetX + 50;
                //    graphics.DrawString("Bank", new Font("Tahoma", 8),
                //                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    OffsetX = OffsetX + 45;
                //    graphics.DrawString("Ref. No", new Font("Tahoma", 8),
                //                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    OffsetX = OffsetX + 50;
                //    graphics.DrawString("C. Date", new Font("Tahoma", 8),
                //                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //    foreach (DataRow drowpm in GLB_HP_RECEIPT_PAYTYPE.Rows)
                //    {
                //        OffsetY = OffsetY + 20;
                //        OffsetX = 525;
                //        graphics.DrawString(drowpm["hpr_mode"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = OffsetX + 50;
                //        graphics.DrawString(String.Format("{0:#,##0.00}", Convert.ToDecimal(drowpm["hpr_amt"].ToString())), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = OffsetX + 50;
                //        graphics.DrawString(drowpm["hpr_bank"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = OffsetX + 45;
                //        graphics.DrawString(drowpm["hpr_ref"].ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = OffsetX + 50;
                //        if (drowpm["hpr_mode"].ToString() != "CHEQUE")
                //        {
                //            graphics.DrawString(Convert.ToDateTime(drowpm["hpr_date"]).ToString("dd/MMM/yyyy"), new Font("Tahoma", 8),
                //                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        }
                //    }

                //    OffsetY = 325;
                //    foreach (DataRow drowpm in GLB_HP_RECEIPT_PAYTYPE.Rows)
                //    {
                //        if (drowpm["hpr_mode"].ToString() != "CHEQUE")
                //        {
                //            string _pmode = drowpm["hpr_mode"].ToString() == "CRCD" ? "Cr." : drowpm["hpr_mode"].ToString() == "CHEQUE" ? "Cn." : "";

                //            OffsetY = OffsetY + 20;
                //            OffsetX = 120;
                //            graphics.DrawString(_pmode + "  " + drowpm["hpr_ref"].ToString() + "  Cd.  " + Convert.ToDateTime(drowpm["hpr_date"]).ToString("dd/MMM/yyyy"), new Font("Tahoma", 8),
                //                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        }
                //    }
                //}
                #endregion

                #region variables
                string MST_LOC_des = (from DataRow drw in MST_LOC.Rows select (string)drw["ML_LOC_DESC"]).FirstOrDefault();
                string MST_LOC_Address = (from DataRow drw in MST_LOC.Rows select drw["ML_ADD1"]).FirstOrDefault() + " " + (from DataRow drw in MST_LOC.Rows select drw["ML_ADD2"]).FirstOrDefault();
                string MST_LOC_Tax_Fax = "Tel : " + (from DataRow drw in MST_LOC.Rows select (string)drw["ML_TEL"]).FirstOrDefault() + "  Fax : " + (from DataRow drw in MST_LOC.Rows select (string)drw["ML_FAX"]).FirstOrDefault(); ;
                string MST_LOC_1_des = (from DataRow drw in MST_LOC_1.Rows select (string)drw["ML_LOC_DESC"]).FirstOrDefault();
                string MST_LOC_1_code = (from DataRow drw in MST_LOC_1.Rows select drw["ML_LOC_CD"] == null ? string.Empty : (string)drw["ML_LOC_CD"]).FirstOrDefault();

                if (isAccess == false)
                {
                    VIW_INV_MOVEMENT_SERIAL_loc = (from DataRow drw in VIW_INV_MOVEMENT_SERIAL == null ? VIW_INV_MOVEMENT_SERIAL_uni_nonDel.Rows : VIW_INV_MOVEMENT_SERIAL.Rows select drw["ITH_LOC"] == null ? string.Empty : (string)drw["ITH_LOC"]).FirstOrDefault();
                    VIW_INV_MOVEMENT_SERIAL_date = (from DataRow drw in VIW_INV_MOVEMENT_SERIAL == null ? VIW_INV_MOVEMENT_SERIAL_uni_nonDel.Rows : VIW_INV_MOVEMENT_SERIAL.Rows select Convert.ToDateTime(drw["ITH_DOC_DATE"]).ToShortDateString()).FirstOrDefault();
                    item_Code = (from DataRow drw in VIW_INV_MOVEMENT_SERIAL == null ? VIW_INV_MOVEMENT_SERIAL_uni_nonDel.Rows : VIW_INV_MOVEMENT_SERIAL.Rows select drw["ITS_ITM_CD"] == null ? string.Empty : (string)drw["ITS_ITM_CD"]).FirstOrDefault();
                    VIW_INV_MOVEMENT_SERIAL_remarks = (from DataRow drw in VIW_INV_MOVEMENT_SERIAL == null ? VIW_INV_MOVEMENT_SERIAL_uni_nonDel.Rows : VIW_INV_MOVEMENT_SERIAL.Rows select (drw["ITH_REMARKS"] == null || drw["ITH_REMARKS"].ToString() == "") ? string.Empty : (string)drw["ITH_REMARKS"]).FirstOrDefault();
                    VIW_INV_MOVEMENT_SERIAL_othLoc = (from DataRow drw in VIW_INV_MOVEMENT_SERIAL == null ? VIW_INV_MOVEMENT_SERIAL_uni_nonDel.Rows : VIW_INV_MOVEMENT_SERIAL.Rows select (drw["ITH_OTH_LOC"] == null || drw["ITH_OTH_LOC"].ToString() == "") ? string.Empty : (string)drw["ITH_OTH_LOC"]).FirstOrDefault();

                    VIW_INV_MOVEMENT_SERIAL_Root = VIW_INV_MOVEMENT_SERIAL.Copy();
                    VIW_INV_MOVEMENT_SERIAL_uni = RemoveDuplicateRows(VIW_INV_MOVEMENT_SERIAL, "ITS_ITM_CD");
                    VIW_INV_MOVEMENT_SERIAL_uni_nonDel = VIW_INV_MOVEMENT_SERIAL.Copy();
                    isAccess = true;
                }
                else
                {
                    VIW_INV_MOVEMENT_SERIAL_loc = (from DataRow drw in VIW_INV_MOVEMENT_SERIAL_uni_nonDel.Rows select drw["ITH_LOC"] == null ? string.Empty : (string)drw["ITH_LOC"]).FirstOrDefault();
                    VIW_INV_MOVEMENT_SERIAL_date = (from DataRow drw in VIW_INV_MOVEMENT_SERIAL_uni_nonDel.Rows select Convert.ToDateTime(drw["ITH_DOC_DATE"]).ToShortDateString()).FirstOrDefault();
                    item_Code = (from DataRow drw in VIW_INV_MOVEMENT_SERIAL_uni_nonDel.Rows select drw["ITS_ITM_CD"] == null ? string.Empty : (string)drw["ITS_ITM_CD"]).FirstOrDefault();
                    VIW_INV_MOVEMENT_SERIAL_remarks = (from DataRow drw in VIW_INV_MOVEMENT_SERIAL_uni_nonDel.Rows select (drw["ITH_REMARKS"] == null || drw["ITH_REMARKS"].ToString() == "") ? string.Empty : (string)drw["ITH_REMARKS"]).FirstOrDefault();
                    VIW_INV_MOVEMENT_SERIAL_othLoc = (from DataRow drw in VIW_INV_MOVEMENT_SERIAL_uni_nonDel.Rows select (drw["ITH_OTH_LOC"] == null || drw["ITH_OTH_LOC"].ToString() == "") ? string.Empty : (string)drw["ITH_OTH_LOC"]).FirstOrDefault();
                }

                Int16 NOOFPAGES = (from DataRow drw in PRINT_DOC.Rows select Convert.ToInt16(drw["NOOFPAGES"])).FirstOrDefault();
                Int16 SHOWCOM = (from DataRow drw in PRINT_DOC.Rows select Convert.ToInt16(drw["SHOWCOM"])).FirstOrDefault();

                string SAH_CRE_BY = (from DataRow drw in VIW_INV_MOVEMENT_SERIAL_uni_nonDel.Rows select (drw["ITH_CRE_BY"] == null || drw["ITH_CRE_BY"].ToString() == "") ? string.Empty : (string)drw["ITH_CRE_BY"]).FirstOrDefault();
                string mc_desc = (from DataRow drw in mst_com.Rows select drw["MC_DESC"] == null ? string.Empty : (string)drw["MC_DESC"]).FirstOrDefault();
                string mc_tax = (from DataRow drw in mst_com.Rows select (drw["MC_TAX3"] == null || drw["MC_TAX3"].ToString() == "") ? string.Empty : (string)drw["MC_TAX3"]).FirstOrDefault();
                string mc_anal1 = (from DataRow drw in mst_com.Rows select drw["MC_ANAL1"] == null ? string.Empty : (string)drw["MC_ANAL1"]).FirstOrDefault();
                string mc_anal2 = (from DataRow drw in mst_com.Rows select drw["MC_ANAL2"] == null ? string.Empty : (string)drw["MC_ANAL2"]).FirstOrDefault();
                string salesTy = (from DataRow drw in param1.Rows select drw["saleType"] == null ? string.Empty : (string)drw["saleType"]).FirstOrDefault();
                string Deli_Name = (from DataRow drw in DelCustomer.Rows select (drw["MBE_NAME"] == null || drw["MBE_NAME"].ToString() == "") ? string.Empty : (string)drw["MBE_NAME"]).FirstOrDefault();
                string Deli_Add1 = (from DataRow drw in DelCustomer.Rows select (drw["ITH_DEL_ADD1"] == null || drw["ITH_DEL_ADD1"].ToString() == "") ? string.Empty : (string)drw["ITH_DEL_ADD1"]).FirstOrDefault();
                string Deli_Add2 = (from DataRow drw in DelCustomer.Rows select (drw["ITH_DEL_ADD2"] == null || drw["ITH_DEL_ADD2"].ToString() == "") ? string.Empty : (string)drw["ITH_DEL_ADD2"]).FirstOrDefault();
                //added by Wimal @ 07/Nov/2018
                string invCus_Name = (from DataRow drw in sat_hdr.Rows select (drw["SAH_CUS_NAME"] == null || drw["SAH_CUS_NAME"].ToString() == "") ? string.Empty : (string)drw["SAH_CUS_NAME"]).FirstOrDefault();
                string invCus_Add1 = (from DataRow drw in sat_hdr.Rows select (drw["SAH_CUS_ADD1"] == null || drw["SAH_CUS_ADD1"].ToString() == "") ? string.Empty : (string)drw["SAH_CUS_ADD1"]).FirstOrDefault();
                string invCus_Add2 = (from DataRow drw in sat_hdr.Rows select (drw["SAH_CUS_ADD2"] == null || drw["SAH_CUS_ADD2"].ToString() == "") ? string.Empty : (string)drw["SAH_CUS_ADD2"]).FirstOrDefault();

                
                string Deli_Contact = "Tel : " + (from DataRow drw in DelCustomer.Rows select (drw["MBE_TEL"] == null || drw["MBE_TEL"].ToString() == "") ? string.Empty : (string)drw["MBE_TEL"]).FirstOrDefault() + "  /" + (from DataRow drw in DelCustomer.Rows select (drw["MBE_MOB"] == null || drw["MBE_MOB"].ToString() == "") ? string.Empty : (string)drw["MBE_MOB"]).FirstOrDefault();
                string subType_desc = (from DataRow drw in mst_movcatetp.Rows select (drw["MMCT_SDESC"] == null || drw["MMCT_SDESC"].ToString() == "") ? string.Empty : (string)drw["MMCT_SDESC"]).FirstOrDefault();
                string sales_Details = "";//"Warranty Remarks : " + (from DataRow drw in VIW_SALES_DETAILS.Rows select drw["sad_warr_period"] == null ? 0 : (int)drw["sad_warr_period"]).FirstOrDefault() + " Month(s) " + (from DataRow drw in VIW_SALES_DETAILS.Rows select drw["SAD_WARR_REMARKS"] == null ? string.Empty : drw["SAD_WARR_REMARKS"]).FirstOrDefault();
                string pcDefLocation = (from DataRow drw in mst_profit_center.Rows select (drw["MPC_DEF_LOC"] == null || drw["MPC_DEF_LOC"].ToString() == "") ? string.Empty : (string)drw["MPC_DEF_LOC"]).FirstOrDefault();


                string docType = string.Empty;
                string subType = string.Empty;
                string Job_No = string.Empty;
                string Ref_No = string.Empty;
                string Location = string.Empty;
                string MRN_No = string.Empty;
                string Account_No = string.Empty;
                string Manual_Ref_No = string.Empty;
                string Address = string.Empty;
                string TelePhone = string.Empty;
                List<int> qty = new List<int>();
                string _ITS_ITM_CD = string.Empty;
                var filterSerial = Enumerable.Empty<DataRow>();

                #endregion

                #region comment
                //if (MST_LOC.Rows.Count > 0)
                //{
                //    foreach (DataRow drowLOC in MST_LOC.Rows)
                //    {
                //        OffsetY = OffsetY + 50;
                //        OffsetX = 180;
                //        graphics.DrawString(MST_LOC_des.ToString(), new Font("Tahoma", 8),//mis
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetY = OffsetY + 20;
                //        OffsetX = 400;
                //        graphics.DrawString(VIW_INV_MOVEMENT_SERIAL_loc.ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = OffsetX + 100;
                //        graphics.DrawString(MST_LOC_1_des.ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetY = OffsetY + 20;
                //        graphics.DrawString(MST_LOC_1_code.ToString(), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetY = OffsetY + 20;
                //        OffsetX = 180;
                //        graphics.DrawString(Convert.ToDateTime(VIW_INV_MOVEMENT_SERIAL_date).ToString("dd/MMM/yyyy"), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetY = OffsetY + 20;
                //    }
                //}
                #endregion

                if (mst_com.Rows.Count > 0)
                {
                    foreach (DataRow docValues in isAccess == true ? VIW_INV_MOVEMENT_SERIAL_uni_nonDel.Rows : VIW_INV_MOVEMENT_SERIAL.Rows)
                    {
                        //doc type
                        if (docValues["ITH_DOC_TP"].ToString() == "DO")
                        {
                            if (salesTy == "HS")
                            {
                                docType = "DELIVERY CONFIRMATION NOTE";
                            }
                            else
                            {
                                docType = "DELIVERY ORDER NOTE";
                            }
                        }

                        #region Doc Type commented
                        //else if (docValues["ITH_DOC_TP"].ToString() == "PRN")
                        //{
                        //    docType = "PURCHASE RETURN NOTE";
                        //}
                        //else if (docValues["ITH_DOC_TP"].ToString() == "AOD")
                        //{
                        //    docType = "ADVICE OF DISPATCH OUT NOTE";
                        //}
                        //else if (docValues["ITH_DOC_TP"].ToString() == "ADJ" && docValues["ITH_DOC_TP"].ToString() == "DISPO")
                        //{
                        //    docType = "DISPOSAL NOTE";
                        //}
                        //else if (docValues["ITH_DOC_TP"].ToString() == "ADJ" && docValues["ITH_DOC_TP"].ToString() == "ADJ+")
                        //{
                        //    docType = "STOCK ADJUSTMENT NOTE / ADJ-";
                        //}
                        #endregion

                        //sub type
                        if (docValues["ITH_SUB_TP"].ToString() == "CONS" && docValues["ITH_DOC_TP"].ToString() == "ADJ")
                        {
                            subType = "CONSIGNMENT RETURN NOTE";
                        }
                        else
                        {
                            subType = docValues["ITH_SUB_TP"].ToString() + "-" + (subType_desc == null ? string.Empty : subType_desc.ToString());
                        }
                        Job_No = docValues["itb_job_no"].ToString();
                        Ref_No = docValues["ITH_DOC_NO"].ToString();
                        Location = docValues["ITH_LOC"].ToString() + " " + MST_LOC_des.ToString();
                        MRN_No = docValues["ITH_OTH_DOCNO"].ToString();
                        Account_No = docValues["ITH_ACC_NO"].ToString();
                        Manual_Ref_No = docValues["ITH_MANUAL_REF"].ToString();
                        Address = MST_LOC_Address;
                    }
                    OffsetX = 5;
                    graphics.DrawString(docType.ToString(), new Font("Tahoma", 10, FontStyle.Bold),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    //if (SHOWCOM != 0)
                    //{
                        OffsetX = Convert.ToInt16(Math.Round((Convert.ToDecimal((10 * 72) / 2) - Convert.ToDecimal((((mc_desc.ToString().Length) * 72) / 14) / 4)), 0));
                        graphics.DrawString(mc_desc.ToString(), new Font("Tahoma", 14, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    //}
                    OffsetY = OffsetY + 20;
                    OffsetX = 605;
                    if (SAH_CRE_BY != null)
                    {
                        graphics.DrawString("Created By: " + (SAH_CRE_BY == null ? string.Empty : SAH_CRE_BY.ToString()), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        OffsetY = OffsetY + 10;
                    }
                    if (NOOFPAGES != 0)
                    {
                        OffsetX = 285;
                        graphics.DrawString("Reprint Copy: " + NOOFPAGES.ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY + 10);
                    }
                    OffsetX = 605;
                    graphics.DrawString(DateTime.Now.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY + 3);
                    OffsetX = 5;
                    graphics.DrawString(subType.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = Convert.ToInt16(Math.Round((Convert.ToDecimal((10 * 72) / 2) - Convert.ToDecimal((((Address.ToString().Length) * 72) / 8) / 4)), 0));
                    graphics.DrawString(Address.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetY = OffsetY + 10;
                    OffsetX = OffsetX + 40;
                    OffsetX = Convert.ToInt16(Math.Round((Convert.ToDecimal((10 * 72) / 2) - Convert.ToDecimal((((MST_LOC_Tax_Fax.ToString().Length) * 72) / 8) / 4)), 0));
                    graphics.DrawString(MST_LOC_Tax_Fax.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    //OffsetX = 600;
                    //graphics.DrawString(mc_tax.ToString(), new Font("Tahoma", 8),
                    //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetY = OffsetY + 15;
                    OffsetX = 5;
                    graphics.DrawString(DelCustomer.Rows.Count <= 0 ? string.Empty : "Deliver To", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = 590;
                    graphics.DrawString(Ref_No.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = 5;
                    OffsetY = OffsetY + 15;
                    graphics.DrawString(Deli_Name == null ? string.Empty : Deli_Name.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = 590;
                    graphics.DrawString(VIW_INV_MOVEMENT_SERIAL_date.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetY = OffsetY + 15;
                    OffsetX = 5;
                    graphics.DrawString(Deli_Add1 == null ? string.Empty : Deli_Add1.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = 590;
                    graphics.DrawString(Location.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetY = OffsetY + 20;
                    OffsetX = 5;
                    graphics.DrawString(Deli_Add2 == null ? string.Empty : Deli_Add2.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetY = OffsetY + 10;
                    graphics.DrawString(Deli_Contact == null ? string.Empty : Deli_Contact == "Tel : " ? string.Empty : Deli_Contact.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetY = OffsetY + 15;
                    graphics.DrawString(Job_No == string.Empty ? string.Empty : "Job No : ", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = OffsetX + 50;
                    graphics.DrawString(Job_No.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = 500;
                    graphics.DrawString("Invoice No : ", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = OffsetX + 80;
                    graphics.DrawString(MRN_No.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetY = OffsetY + 15;
                    OffsetX = 5;
                    graphics.DrawString("Manual Ref No : ", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = OffsetX + 100;
                    graphics.DrawString(Manual_Ref_No.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    if (Account_No != "" && Account_No != null)
                    {
                        OffsetX = 500;
                        graphics.DrawString("Account No : ", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    }
                    OffsetX = OffsetX + 80;
                    graphics.DrawString(Account_No.ToString(), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                }

                //VIW_INV_MOVEMENT_SERIAL_Root = VIW_INV_MOVEMENT_SERIAL.Copy();
                //VIW_INV_MOVEMENT_SERIAL_uni = RemoveDuplicateRows(VIW_INV_MOVEMENT_SERIAL, "ITS_ITM_CD");
                //VIW_INV_MOVEMENT_SERIAL_uni_nonDel = VIW_INV_MOVEMENT_SERIAL.Copy();
                //serialQty = VIW_INV_MOVEMENT_SERIAL_uni.Rows.Count;
                if (VIW_INV_MOVEMENT_SERIAL_uni.Rows.Count > 0)
                {
                    OffsetY = OffsetY + 15;
                    OffsetX = 5;

                    graphics.DrawString("ITEM CODE", new Font("Tahoma", 8, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = OffsetX + 145;
                    graphics.DrawString("DESCRIPTION", new Font("Tahoma", 8, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = OffsetX + 350;
                    graphics.DrawString("MODEL", new Font("Tahoma", 8, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = OffsetX + 150;
                    graphics.DrawString("TYPE", new Font("Tahoma", 8, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = OffsetX + 80;
                    graphics.DrawString("QUANTITY", new Font("Tahoma", 8, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetY = OffsetY + 5;
                }
 

                if (VIW_INV_MOVEMENT_SERIAL_uni.Rows.Count <= 0 && filterSerial.Count() > 0) // VIW_INV_MOVEMENT_SERIAL_Root.Rows.Count > 0
                {
                    List<DataRow> VIW_INV_MOVEMENT_SERIAL_Root_list = VIW_INV_MOVEMENT_SERIAL_Root.AsEnumerable().ToList();
                    foreach (DataRow drowmserials in VIW_INV_MOVEMENT_SERIAL_Root_list) //VIW_INV_MOVEMENT_SERIAL_Root.Rows
                    {
                        //newpage = false;
                        //if (OffsetY <= 350 && newpage == false) //deduct 20 for next line
                        //{
                        //    //e.HasMorePages = false;
                        //}
                        //else
                        //{
                        //    e.HasMorePages = true;
                        //    startX = 0;
                        //    startY = 0;
                        //    OffsetY = 5;
                        //    return;
                        //}

                        //qty.Add(QTY); //Convert.ToInt16(drowms["QTY"])
                        OffsetY = OffsetY + 20;
                        OffsetX = 5;
                        graphics.DrawString(mc_anal1.ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        OffsetX = OffsetX + 50;
                        graphics.DrawString(drowmserials["ITS_SER_1"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        OffsetX = OffsetX + 150;
                        graphics.DrawString(mc_anal2.ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        OffsetX = OffsetX + 50;
                        graphics.DrawString(drowmserials["ITS_SER_2"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        OffsetX = OffsetX + 100;
                        graphics.DrawString("Warranty No :", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        OffsetX = OffsetX + 80;
                        graphics.DrawString(drowmserials["ITS_WARR_NO"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);                        
                        
                        sales_Details = "";                        
                        var iSer = from r in VIW_SALES_DETAILS.AsEnumerable()
                                   where r.Field<string>("SAD_INV_NO") == drowmserials["ITH_OTH_DOCNO"].ToString()
                                   && r.Field<int>("SAD_ITM_LINE") == Convert.ToInt16(drowmserials["ITB_BASE_REFLINE"])
                                   select new
                                   {
                                       _warrremark = r.Field<string>("SAD_WARR_REMARKS"),
                                       _warrperiod = r.Field<int>("sad_warr_period")
                                   };

                        foreach (var s in iSer)
                        {
                            sales_Details = "Warranty Remarks : " + (s._warrperiod == null ? "0" : Convert.ToString(s._warrperiod)) + " Month(s) " + (s._warrremark == null ? string.Empty : s._warrremark);
                        }

                        OffsetX = 150;
                        for (int i = 1; i <= (sales_Details.ToString().Length / 100) + 1; i++)
                        {
                            OffsetY = OffsetY + 15;
                            graphics.DrawString(sales_Details.ToString().Substring(((i - 1) * 100), sales_Details.ToString().Length - (i - 1) * 100 < 100 ? sales_Details.ToString().Length - (i - 1) * 100 : 100), new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        }

                        drowmserials.Delete();
                        drowmserials.AcceptChanges();
                    }
                    //OffsetX = 150;
                    //graphics.DrawString("Warranty Remarks: ", new Font("Tahoma", 8),
                    //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);                    
                    //OffsetX = OffsetX + 145;
                    //graphics.DrawString(sales_Details.ToString(), new Font("Tahoma", 8),
                    //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    //for (int i = 1; i <= (sales_Details.ToString().Length / 100) + 1; i++)
                    //{
                    //    OffsetY = OffsetY + 15;
                    //    graphics.DrawString(sales_Details.ToString().Substring(((i - 1) * 100), sales_Details.ToString().Length - (i - 1) * 100 < 100 ? sales_Details.ToString().Length - (i - 1) * 100 : 100), new Font("Tahoma", 8),
                    //                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);                        
                    //}

                    
                }
                else
                {
                    List<DataRow> VIW_INV_MOVEMENT_SERIAL_uni_list = VIW_INV_MOVEMENT_SERIAL_uni.AsEnumerable().ToList();
                    foreach (DataRow drowms in VIW_INV_MOVEMENT_SERIAL_uni_list) //VIW_INV_MOVEMENT_SERIAL
                    {
                        newpage = false;
                        if (OffsetY <= 350 && newpage == false) //deduct 20 for next line
                        {
                            //e.HasMorePages = false;
                        }
                        else
                        {
                            e.HasMorePages = true;
                            startX = 0;
                            startY = 0;
                            OffsetY = 5;
                            return;
                        }

                        _ITS_ITM_CD = drowms["ITS_ITM_CD"].ToString();
                        //QTY = Convert.ToInt16(drowms["QTY"]);
                        OffsetY = OffsetY + 15;
                        OffsetX = 5;
                        graphics.DrawString(drowms["ITS_ITM_CD"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        OffsetX = OffsetX + 145;
                        graphics.DrawString(drowms["ITEMDES"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        OffsetX = OffsetX + 350;
                        graphics.DrawString(drowms["ITEMMODEL"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        OffsetX = OffsetX + 150;
                        graphics.DrawString(drowms["ITEMSTS"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        OffsetX = OffsetX + 80;
                        OffsetX = Convert.ToInt16((OffsetX + 7 * 72 / 8 / 2) - (Convert.ToDecimal(drowms["QTY"]).ToString("N2").Length * 72 / 8 / 2) + 20);
                        graphics.DrawString(Convert.ToDecimal(drowms["QTY"]).ToString("N2"), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                        //var query = VIW_INV_MOVEMENT_SERIAL_uni.AsEnumerable().Where(r => r.Field<string>("ITS_ITM_CD") == drowms["ITS_ITM_CD"].ToString());
                        //foreach (var row in query.ToList())
                        //    row.Delete();
                        drowms.Delete();
                        string _rState = drowms.RowState.ToString();
                        drowms.AcceptChanges();
                        
                        OffsetX = 5; //itemSerials
                        filterSerial = from row in VIW_INV_MOVEMENT_SERIAL_Root.AsEnumerable().ToList()
                                       where row.Field<string>("ITS_ITM_CD").Trim() == _ITS_ITM_CD//drowms["ITS_ITM_CD"].ToString()
                                       select row;
                        
                        //}

                        List<DataRow> VIW_INV_MOVEMENT_SERIAL_Root_list = filterSerial.AsEnumerable().ToList();
                        foreach (DataRow drowmserials in VIW_INV_MOVEMENT_SERIAL_Root_list) //VIW_INV_MOVEMENT_SERIAL_Root.Rows
                        {
                            //newpage = false;
                            //if (OffsetY <= 350 && newpage == false) //deduct 20 for next line
                            //{
                            //    //e.HasMorePages = false;
                            //}
                            //else
                            //{
                            //    e.HasMorePages = true;
                            //    startX = 0;
                            //    startY = 0;
                            //    OffsetY = 5;
                            //    return;
                            //}

                            //qty.Add(QTY); //Convert.ToInt16(drowms["QTY"])
                            if (!(drowmserials["WARRPRINT"].ToString() == "0" && drowmserials["ITS_SER_1"].ToString() == "N/A" && drowmserials["ITS_SER_2"].ToString() == "N/A"))
                            {
                                OffsetY = OffsetY + 15;
                                OffsetX = 5;
                                graphics.DrawString(mc_anal1.ToString() + " : ", new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                OffsetX = OffsetX + 50;
                                graphics.DrawString(drowmserials["ITS_SER_1"].ToString(), new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                OffsetX = OffsetX + 150;
                                graphics.DrawString(mc_anal2.ToString() + " : ", new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                OffsetX = OffsetX + 50;
                                graphics.DrawString(drowmserials["ITS_SER_2"].ToString(), new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                OffsetX = OffsetX + 100;
                                graphics.DrawString("Warranty No : ", new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                OffsetX = OffsetX + 80;
                                graphics.DrawString(drowmserials["ITS_WARR_NO"].ToString(), new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                            }
                            //var querySerial = VIW_INV_MOVEMENT_SERIAL_Root.AsEnumerable().Where(r => r.Field<string>("ITS_SER_1") == drowmserials["ITS_SER_1"].ToString());// drowms["ITS_ITM_CD"].ToString()
                            //foreach (var row in querySerial.ToList())
                            //    row.Delete();

                            sales_Details = "";                            
                            var iSer = from r in VIW_SALES_DETAILS.AsEnumerable()
                                       where r.Field<string>("SAD_INV_NO") == drowmserials["ITH_OTH_DOCNO"].ToString()
                                       && r.Field<int>("SAD_ITM_LINE") == Convert.ToInt16(drowmserials["ITB_BASE_REFLINE"])
                                       select new
                                       {
                                           _warrremark = r.Field<string>("SAD_WARR_REMARKS"),
                                           _warrperiod = r.Field<int>("sad_warr_period")
                                       };

                            foreach (var s in iSer)
                            {
                                sales_Details = "Warranty Remarks : " + (s._warrperiod == null ? "0" : Convert.ToString(s._warrperiod)) + " Month(s) " + (s._warrremark == null ? string.Empty : s._warrremark);
                            }

                            OffsetX = 150;
                            for (int i = 1; i <= (sales_Details.ToString().Length / 100) + 1; i++)
                            {
                                OffsetY = OffsetY + 15;
                                graphics.DrawString(sales_Details.ToString().Substring(((i - 1) * 100), sales_Details.ToString().Length - (i - 1) * 100 < 100 ? sales_Details.ToString().Length - (i - 1) * 100 : 100), new Font("Tahoma", 8),
                                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                            }

                            drowmserials.Delete();
                            drowmserials.AcceptChanges();
                        }
                        _ITS_ITM_CD = string.Empty;

                        //OffsetX = 150;
                        //graphics.DrawString("Warranty Remarks:", new Font("Tahoma", 8),
                        //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY + 15);
                        //sales_Details = "Warranty Remarks : " + sales_Details;
                        //OffsetX = OffsetX + 145;
                        //graphics.DrawString(sales_Details.ToString(), new Font("Tahoma", 8),
                        //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        //for (int i = 1; i <= (sales_Details.ToString().Length / 100) + 1; i++)
                        //{
                        //    OffsetY = OffsetY + 15;
                        //    graphics.DrawString(sales_Details.ToString().Substring(((i - 1) * 100), sales_Details.ToString().Length - (i - 1) * 100 < 100 ? sales_Details.ToString().Length - (i - 1) * 100 : 100), new Font("Tahoma", 8),
                        //                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);                            
                        //}
                    }
                }
                if (VIW_INV_MOVEMENT_SERIAL_loc != pcDefLocation)
                {
                    List<DataRow> mst_profit_center_list = mst_profit_center.AsEnumerable().ToList();
                    foreach (DataRow mst_pc in mst_profit_center_list)
                    {
                        newpage = false;
                        if (OffsetY <= 350 && newpage == false) //deduct 20 for next line
                        {
                            //e.HasMorePages = false;
                        }
                        else
                        {
                            e.HasMorePages = true;
                            startX = 0;
                            startY = 0;
                            OffsetY = 5;
                            return;
                        }

                        OffsetY = OffsetY + 15;
                        OffsetX = 5;
                        string _delmade = "";
                        _delmade = "This delivery made on behalf of ";
                        _delmade += mst_pc["MPC_CD"].ToString() + " - " + mst_pc["MPC_DESC"].ToString();

                        for (int i = 1; i <= (sales_Details.ToString().Length / 100) + 1; i++)
                        {
                            graphics.DrawString(_delmade.ToString().Substring(((i - 1) * 100), _delmade.ToString().Length - (i - 1) * 100 < 100 ? _delmade.ToString().Length - (i - 1) * 100 : 100), new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                            OffsetY = OffsetY + 15;
                        }
                        _delmade = "Contact # : " + mst_pc["MPC_TEL"].ToString();
                        for (int i = 1; i <= (sales_Details.ToString().Length / 100) + 1; i++)
                        {
                            graphics.DrawString(_delmade.ToString().Substring(((i - 1) * 100), _delmade.ToString().Length - (i - 1) * 100 < 100 ? _delmade.ToString().Length - (i - 1) * 100 : 100), new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                            OffsetY = OffsetY + 15;
                        }

                        //graphics.DrawString("This delivery made on behalf of", new Font("Tahoma", 8),
                        //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        //OffsetX = OffsetX + 10;
                        //graphics.DrawString(mst_pc["MPC_CD"].ToString(), new Font("Tahoma", 8),
                        //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        //OffsetX = OffsetX + 5;
                        //graphics.DrawString(mst_pc["MPC_DESC"].ToString(), new Font("Tahoma", 8),
                        //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        //OffsetY = OffsetY + 20;
                        //OffsetX = 150;
                        //graphics.DrawString("Contact # : ", new Font("Tahoma", 8),
                        //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        //OffsetX = OffsetX + 10;
                        //graphics.DrawString(mst_pc["MPC_TEL"].ToString(), new Font("Tahoma", 8),
                        //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        mst_pc.Delete();
                        mst_pc.AcceptChanges();
                    }
                }

                if (tblComDate.Rows.Count > 0)
                {
                    List<DataRow> tblComDate_list = tblComDate.AsEnumerable().ToList();
                    foreach (DataRow comDate in tblComDate_list)
                    {
                        newpage = false;
                        if (OffsetY <= 350 && newpage == false) //deduct 20 for next line
                        {
                            //e.HasMorePages = false;
                        }
                        else
                        {
                            e.HasMorePages = true;
                            startX = 0;
                            startY = 0;
                            OffsetY = 5;
                            return;
                        }

                        OffsetY = OffsetY + 15;
                        OffsetX = 5;
                        graphics.DrawString("Warranty Commence From ", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        OffsetX = OffsetX + 10;
                        graphics.DrawString(comDate["GCL_DATE"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        OffsetX = OffsetX + 10;
                        graphics.DrawString(comDate["GCL_INV"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        comDate.Delete();
                        comDate.AcceptChanges();
                    }
                }
                if (sat_vou_det.Rows.Count > 0)
                {                    
                    List<DataRow> sat_vou_det_list = sat_vou_det.AsEnumerable().ToList();
                    foreach (DataRow vouDet in sat_vou_det_list)
                    {
                        newpage = false;
                        if (OffsetY <= 350 && newpage == false) //deduct 20 for next line
                        {
                            //e.HasMorePages = false;
                        }
                        else
                        {
                            e.HasMorePages = true;
                            startX = 0;
                            startY = 0;
                            OffsetY = 5;
                            return;
                        }

                        string vouAmt = vouDet["vouType"] == null ? string.Empty : vouDet["vouType"].ToString() == "VALUE" ? "- LKR :" + vouDet["Amt"] : "-" + vouDet["Amt"] + "%";
                        vouAmt = "Voucher : " + vouAmt + " " + vouDet["stvo_gv_itm"].ToString() + "/ " + vouDet["STVO_PAGENO"].ToString();
                        OffsetY = OffsetY + 15;
                        OffsetX = 5;
                        //graphics.DrawString("amt", new Font("Tahoma", 8),
                        //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        //OffsetX = OffsetX + 10;
                        graphics.DrawString(vouAmt, new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        //OffsetX = OffsetX + 10;
                        //graphics.DrawString(vouDet["stvo_gv_itm"].ToString() + "/ " + vouDet["STVO_PAGENO"].ToString() + vouAmt, new Font("Tahoma", 8),
                        //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        vouDet.Delete();
                        string _rState = vouDet.RowState.ToString();
                        if (_rState == "Deleted")
                        {
                            vouDet.AcceptChanges();
                        }
                        //vouDet.AcceptChanges();
                    }
                }
                newpage = false;
                if (OffsetY <= 350 && newpage == false) //deduct 80 for next line  
                {
                    //e.HasMorePages = false;
                }
                else
                {
                    e.HasMorePages = true;
                    startX = 0;
                    startY = 0;
                    OffsetY = 5;
                    return;
                }

                OffsetY = OffsetY + 15;
                OffsetX = 5;
                graphics.DrawString("Remarks :", new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                OffsetX = OffsetX + 90;
                graphics.DrawString(VIW_INV_MOVEMENT_SERIAL_remarks.ToString(), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                OffsetY = OffsetY + 15;
                OffsetX = 5;
                graphics.DrawString("Other Location :", new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                OffsetX = OffsetX + 90;
                graphics.DrawString(VIW_INV_MOVEMENT_SERIAL_othLoc.ToString(), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                OffsetY = OffsetY + 20;
                OffsetX = 745;
                graphics.DrawString("---------------", new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                OffsetY = OffsetY + 10;
                OffsetX = 5;
                graphics.DrawString("No Of Units :", new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                OffsetX = 750;
                OffsetX = Convert.ToInt16((OffsetX + 7 * 72 / 8 / 2) - (QTY.ToString("N2").Length * 72 / 8 / 2));
                graphics.DrawString(QTY.ToString("N2"), new Font("Tahoma", 8),//qty.Sum()
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                OffsetX = 745;
                OffsetY = OffsetY + 10;
                graphics.DrawString("=======", new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                #region Commented Footer
                //OffsetY = OffsetY + 40;
                //OffsetX = 150;
                //graphics.DrawString("----------------", new Font("Tahoma", 8),
                //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //OffsetX = OffsetX + 250;
                //graphics.DrawString("----------------", new Font("Tahoma", 8),
                //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //OffsetX = OffsetX + 250;
                //graphics.DrawString("----------------", new Font("Tahoma", 8),
                //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //OffsetY = OffsetY + 20;
                //OffsetX = 150;
                //graphics.DrawString("Prepared By", new Font("Tahoma", 8),
                //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //OffsetX = OffsetX + 250;
                //graphics.DrawString("Checked By", new Font("Tahoma", 8),
                //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //OffsetX = OffsetX + 250;
                //graphics.DrawString("Customer", new Font("Tahoma", 8),
                //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //OffsetY = OffsetY + 30;
                //OffsetX = 250;
                //graphics.DrawString("Please note that the Warranty conditions will not apply for warranty remarks not applicable product.", new Font("Tahoma", 8),
                //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            OffsetYVal = OffsetY;
            //int pageCunt = (int)Math.Ceiling((OffsetY / 340.0));
            //if (OffsetY > 340)
            //{
            //    e.HasMorePages = true;
            //}
            //else
            //    e.HasMorePages = false;
        }

        //isuru 2017/05/16
        public void PurcaseOrderPrintUpdate()
        {
            // Isuru 2017/05/16
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable PUR_HDR = new DataTable();
            DataTable PUR_DEL = new DataTable();
            DataTable PUR_ALOC = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();



            PUR_HDR.Clear();
            PUR_HDR = CHNLSVC.Inventory.GetPODetails(docNo);
            PUR_DEL = CHNLSVC.Inventory.GetPODeliveryDetails(docNo);
            //PUR_ALOC = CHNLSVC.Inventory.GetPOAlocItems(docNo);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            _poupdaterpt.Database.Tables["PUR_HDR"].SetDataSource(PUR_HDR);
            //  _porpt.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            foreach (object repOp in _poupdaterpt.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delDet")
                    {
                        ReportDocument subRepDoc = _poupdaterpt.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["PUR_DEL"].SetDataSource(PUR_DEL);

                    }
                    ////if (_cs.SubreportName == "AlocDet")
                    ////{
                    ////    ReportDocument subRepDoc = _porpt.Subreports[_cs.SubreportName];

                    ////    subRepDoc.Database.Tables["PUR_ALOC"].SetDataSource(PUR_ALOC);

                    ////}

                }
            }
        }
    
           public void PurcaseOrderPrint_ABE()
        {
            // Nadeeka 02-04-2013
            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;


            DataTable PUR_HDR = new DataTable();
            DataTable PUR_DEL = new DataTable();
            DataTable PUR_ALOC = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable MST_LOC1 = new DataTable();

            DataTable param = new DataTable();
            DataTable sat_hdr = new DataTable();
            DataTable MST_COM = new DataTable();



            PUR_HDR.Clear();
            PUR_HDR = CHNLSVC.Inventory.GetPODetails(docNo);
            PUR_DEL = CHNLSVC.Inventory.GetPODeliveryDetails(docNo);
            //PUR_ALOC = CHNLSVC.Inventory.GetPOAlocItems(docNo);
            MST_LOC = CHNLSVC.Inventory.GetLocationDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
        

            _PurchaseOrderPrint_ABE.Database.Tables["PUR_HDR"].SetDataSource(PUR_HDR);
            _PurchaseOrderPrint_ABE.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
            _PurchaseOrderPrint_ABE.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            //  _porpt.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            foreach (object repOp in _PurchaseOrderPrint_ABE.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "delDet")
                    {
                        ReportDocument subRepDoc = _PurchaseOrderPrint_ABE.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["PUR_DEL"].SetDataSource(PUR_DEL);

                    }
                    ////if (_cs.SubreportName == "AlocDet")
                    ////{
                    ////    ReportDocument subRepDoc = _porpt.Subreports[_cs.SubreportName];

                    ////    subRepDoc.Database.Tables["PUR_ALOC"].SetDataSource(PUR_ALOC);

                    ////}

                }
            }
        }


         public void PrintAdjDetlWDocCate_Report()
        {

            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;
        
            DataTable param = new DataTable();      

            DataRow dr;

            param.Columns.Add("user", typeof(string));
            //param.Columns.Add("fromdate", typeof(DateTime));
            //param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("com", typeof(string));
            param.Columns.Add("header", typeof(string));
            param.Columns.Add("loc", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "From " + BaseCls.GlbReportFromDate.ToShortDateString() + "  To" + BaseCls.GlbReportToDate.ToShortDateString();
           // dr["todate"] = BaseCls.GlbReportToDate;
            //dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["com"] = BaseCls.GlbReportComp;
            dr["header"] = BaseCls.GlbReportHeading;
            dr["loc"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);
            _StkAdjDtlWCate.Database.Tables["param1"].SetDataSource(param);

            //DataTable TEMP = new DataTable();
            //TEMP = CHNLSVC.MsgPortal.Get_GetAdjDetailsWDocCat(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, "", BaseCls.GlbUserID);
            //_StkAdjDtlWCate.Database.Tables["STKADJDET"].SetDataSource(TEMP);

            DataTable STKADJDET = new DataTable();
            tmp_user_pc = CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
            DataTable TEMP = new DataTable();
                    //TEMP = CHNLSVC.MsgPortal.ProcessMovementAuditTrial(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocType, drow["tpl_pc"].ToString(), BaseCls.GlbReportCompCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportDirection, BaseCls.GlbUserID, BaseCls.GlbReportDocSubType, BaseCls.GlbReportDoc);
                    TEMP = CHNLSVC.MsgPortal.Get_GetAdjDetailsWDocCat(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID);
                    STKADJDET.Merge(TEMP);
                }
            }

            _StkAdjDtlWCate.Database.Tables["STKADJDET"].SetDataSource(STKADJDET);

            


        }

         public string Ageing_Report_for_Provisioning()
         {
             string _err = "";
             DataTable param = new DataTable();
             DataTable GLOB_DataTable = new DataTable();
             DataTable tmp_user_pc = new DataTable();
             DataTable MST_COM = new DataTable();
             DataRow dr;
             DataTable dtheding = new DataTable();
             dtheding = CHNLSVC.General.GET_REF_AGE_SLOT(BaseCls.GlbUserComCode);

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
             dr["user"] = BaseCls.GlbUserID;
             dr["heading_1"] = BaseCls.GlbReportHeading;
             dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
             dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
             dr["itemstatus"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
             dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["cat4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["cat5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["age"] = BaseCls.GlbReportDocType.Trim();
             param.Rows.Add(dr);

             DataTable STKAGEDET = new DataTable();
            tmp_user_pc = CHNLSVC.Sales.GetTempUserPcRptDB_AllCom(BaseCls.GlbUserID);
             MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);

            


             if (tmp_user_pc.Rows.Count > 0)
             {
                 foreach (DataRow drow in tmp_user_pc.Rows)
                 {
                    DataTable TEMP = new DataTable();
                    
                     TEMP = CHNLSVC.MsgPortal.GET_provisioning_AGE(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportAsAtDate.Date, BaseCls.GlbReportItemType);

                     STKAGEDET.Merge(TEMP);
                 }
                 _summarized_age_report.Database.Tables["param"].SetDataSource(param);
                 _summarized_age_report.Database.Tables["GLB_AGE_PROVISIONING"].SetDataSource(STKAGEDET);
                 _summarized_age_report.Database.Tables["MST_COM"].SetDataSource(MST_COM);
             }
             else
             {
                 _err = "Location not selected";
                
             }
             return _err;
           
         }

        public string status_wise_ageing_report()
         {
             string _err = "";
             DataTable param = new DataTable();
             DataTable GLOB_DataTable = new DataTable();
             DataTable tmp_user_pc = new DataTable();
             DataTable MST_COM = new DataTable();
             DataRow dr;
             DataTable dtheding = new DataTable();
             dtheding = CHNLSVC.General.GET_REF_AGE_SLOT(BaseCls.GlbUserComCode);

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
             dr["user"] = BaseCls.GlbUserID;
             dr["heading_1"] = BaseCls.GlbReportHeading;
             dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
             dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
             dr["itemstatus"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
             dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["cat4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["cat5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
             dr["age"] = BaseCls.GlbReportDocType.Trim();
             param.Rows.Add(dr);

             DataTable STKAGEDET = new DataTable();
             tmp_user_pc = CHNLSVC.Sales.GetTempUserPcRptDB_AllCom(BaseCls.GlbUserID);
             MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);




             if (tmp_user_pc.Rows.Count > 0)
             {
                 foreach (DataRow drow in tmp_user_pc.Rows)
                 {
                     DataTable TEMP = new DataTable();

                    TEMP = CHNLSVC.MsgPortal.GET_STATUS_WISE_AGEING(drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportAsAtDate.Date);
            
                     STKAGEDET.Merge(TEMP);
                 }
                _Status_wise_ageing_report.Database.Tables["param"].SetDataSource(param);
                _Status_wise_ageing_report.Database.Tables["Status_wise_ageing_n"].SetDataSource(STKAGEDET);
                _Status_wise_ageing_report.Database.Tables["MST_COM"].SetDataSource(MST_COM);
             }
             else
             {
                 _err = "Location not selected";

             }
             return _err;

        }

         public void Chargesheetreport() // Tharindu 2018-03-27
         {
             DataTable TMP_SER = new DataTable();

             TMP_SER = CHNLSVC.MsgPortal.getChargeSheet(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, BaseCls.GlbReportDoc, BaseCls.GlbUserID);

             _chargesheet.Database.Tables["Charge_Sheet"].SetDataSource(TMP_SER);
             //  _chargesheet.Database.Tables["param"].SetDataSource(param);
        }
    
        public string Dispoal_summary()
        {
            string _err = "";
            DataTable param = new DataTable();
            DataTable GLOB_DataTable = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataTable MST_COM = new DataTable();
            DataRow dr;
            DataTable dtheding = new DataTable();
            dtheding = CHNLSVC.General.GET_REF_AGE_SLOT(BaseCls.GlbUserComCode);

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
            param.Columns.Add("month", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["itemstatus"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["month"] = BaseCls.GlbReportDoc.Trim();
            dr["fromdate"] = BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
             
            param.Rows.Add(dr);

            DataTable STKAGEDET = new DataTable();
            tmp_user_pc = CHNLSVC.Sales.GetTempUserPcRptDB_AllCom(BaseCls.GlbUserID);
            MST_COM = CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);




            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TEMP = new DataTable();

                    TEMP = CHNLSVC.MsgPortal.GET_disposal_summary(drow["tpl_com"].ToString(), BaseCls.GlbReportFromDate.Date, BaseCls.GlbReportToDate.Date, drow["tpl_pc"].ToString());

                    STKAGEDET.Merge(TEMP);
                }
            }
                        
            else
            {
                _err = "Location not selected";

            }
            _Disposal_summary.Database.Tables["param"].SetDataSource(param);
            _Disposal_summary.Database.Tables["Disposal_summary"].SetDataSource(STKAGEDET);
            _Disposal_summary.Database.Tables["MST_COM"].SetDataSource(MST_COM);

            return _err;

        }

        public void age_summery()
        {
            DataTable param = new DataTable();
            DataTable GLOB_DataTable = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            tmp_user_pc = CHNLSVC.Sales.GetTempUserPcRptDB(null, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                DataTable TMP_DataTable = new DataTable();
                foreach (DataRow drow in tmp_user_pc.Rows)
                {                  
                        TMP_DataTable = CHNLSVC.MsgPortal.getAgesummery(BaseCls.GlbReportFromDate.Date, BaseCls.GlbReportToDate.Date,BaseCls.GlbReportAsAtDate.Date, drow["tpl_com"].ToString(), drow["tpl_pc"].ToString(), BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportSupplier, BaseCls.GlbUserID, BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFast, BaseCls.GlbReportPriceBook, BaseCls.GlbReportPBLevel);
                        GLOB_DataTable.Merge(TMP_DataTable);
                        break;
                }
            }
            DataTable dtheding = new DataTable();
            dtheding = CHNLSVC.General.GET_REF_AGE_SLOT(BaseCls.GlbUserComCode);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
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
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["todate"] = BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["itemstatus"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            dr["withcost"] = BaseCls.GlbReportWithCost;
            dr["withserial"] = 0;
            dr["withstatus"] = 0;
            dr["supplier"] = BaseCls.GlbReportSupplier == "" ? "ALL" : BaseCls.GlbReportSupplier;

            param.Rows.Add(dr);

            _ageSummery.Database.Tables["AGE_COMPARE"].SetDataSource(GLOB_DataTable);
            _ageSummery.Database.Tables["param"].SetDataSource(param);
            _ageSummery.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);

            //if (BaseCls.GlbReportName == "LocwiseItemAge.rpt")
            //{
            //    _locAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
            //    _locAge.Database.Tables["param"].SetDataSource(param);

            //    _locAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            //}
            //if (BaseCls.GlbReportName == "CatwiseItemAge.rpt")
            //{
            //    _catAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
            //    _catAge.Database.Tables["param"].SetDataSource(param);
            //    _catAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            //}
            //if (BaseCls.GlbReportName == "CatScatwiseItemAge.rpt")
            //{
            //    _catScatAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
            //    _catScatAge.Database.Tables["param"].SetDataSource(param);
            //    _catScatAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            //}
            //if (BaseCls.GlbReportName == "StuswiseItemAge.rpt")
            //{
            //    //  _StusAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
            //    // _StusAge.Database.Tables["param"].SetDataSource(param);
            //}
            //if (BaseCls.GlbReportName == "ItmBrndwiseItemAge.rpt")
            //{
            //    _ItmBrndAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
            //    _ItmBrndAge.Database.Tables["param"].SetDataSource(param);
            //    _ItmBrndAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            //}
            //if (BaseCls.GlbReportName == "CatItmwiseItemAge.rpt")
            //{
            //    _catItmAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
            //    _catItmAge.Database.Tables["param"].SetDataSource(param);
            //    _catItmAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            //}
            //if (BaseCls.GlbReportName == "ItmwiseItemAge.rpt")
            //{
            //    _ItmAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
            //    _ItmAge.Database.Tables["param"].SetDataSource(param);
            //    _ItmAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            //}
            //if (BaseCls.GlbReportName == "LocItemStusAge.rpt")
            //{
            //    _LocItmAge.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
            //    _LocItmAge.Database.Tables["param"].SetDataSource(param);
            //    _LocItmAge.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            //}
            //if (BaseCls.GlbReportName == "LocItemStusAgenew.rpt")
            //{
            //    _itmwise.Database.Tables["CURR_AGE"].SetDataSource(GLOB_DataTable);
            //    _itmwise.Database.Tables["param"].SetDataSource(param);
            //    _itmwise.Database.Tables["REF_AGE_SLOT"].SetDataSource(dtheding);
            //}


        }
    }
}
