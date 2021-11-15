using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
using FF.BusinessObjects.ToursNew;
using System.Web.UI;

namespace FastForward.SCMWeb.View.Reports.Sales
{
    public class clsSales : Page
    {
        public InvoiceFullPrints invfullReport = new InvoiceFullPrints();
        public rpt_PendingDeliveryReportNew _fwSales = new rpt_PendingDeliveryReportNew();
        public rpt_PendingSalesReportNew _pndSales = new rpt_PendingSalesReportNew();
        public rptSaleOrderstatusReport _sostatus = new rptSaleOrderstatusReport();
        public sales_order _sales_order = new sales_order();
        // public Rep_INV_DP _invdp = new Rep_INV_DP();
        //public Rep_INV_DP_lrp_rp_new _invauto = new Rep_INV_DP_lrp_rp_new();//Dilshan 09/09/2017
        public SalesInv_AAL _invauto = new SalesInv_AAL();//Dilshan 09/09/2017
        public Rep_INV_DP_lrp_rp _invdp = new Rep_INV_DP_lrp_rp();
        public Rep_INV_DP_lrp_rp_new _invdpAEC = new Rep_INV_DP_lrp_rp_new();//Added By Dulaj 2018/Aug/02
        public SalesInv_SGL _invSGL = new SalesInv_SGL(); //Sales_Inv_SGL, Rep_INV_SGL
        public Sales__Inv_ABT _invABT = new Sales__Inv_ABT();
        public Sales_Inv_AIS _invAIS = new Sales_Inv_AIS();
        public ReceiptPrints_n recreport1_n = new ReceiptPrints_n();
        public ReceiptPrints_SGL recreport_SGL = new ReceiptPrints_SGL();
        public ReceiptPrints_SGL recreport_AOA = new ReceiptPrints_SGL();
        public DeliveredSalesReport _delSalesrptPC = new DeliveredSalesReport();
        public SalesReconcilation _salesRecon = new SalesReconcilation();
        public DebtorSettlement1 _DebtSett = new DebtorSettlement1();
        public DebtorSettlement_Outs _DebtSettOuts = new DebtorSettlement_Outs();
        public DebtorSettlement_Outs_PC _DebtSettOutPC = new DebtorSettlement_Outs_PC();
        public DebtorSettlement_Outs_PC_Meeting _DebtSettOutPCMeeting = new DebtorSettlement_Outs_PC_Meeting();
        public DebtorSettlement_PC _DebtSettPC = new DebtorSettlement_PC();
        public DebtorSettlement_Outs_PC_with_comm _DebtSettOutPCWithComm = new DebtorSettlement_Outs_PC_with_comm();
        public Age_Debtor_Outstanding _AgeDebtOuts = new Age_Debtor_Outstanding();
        public Age_Debtor_Outstanding_PC _AgeDebtOutsPC = new Age_Debtor_Outstanding_PC();
        public Age_Debtor_Outstanding_new _AgeDebtOuts_new = new Age_Debtor_Outstanding_new();
        public Age_Debtor_Outstanding_PC_new _AgeDebtOutsPC_new = new Age_Debtor_Outstanding_PC_new();
        public Age_Debtor_Outstanding_adv _AgeDebtOuts_adv = new Age_Debtor_Outstanding_adv();
        public pcdetails _pcdetails = new pcdetails();
        public Forward_Sales_Report1 _ForwardSalesrpt1 = new Forward_Sales_Report1();
        public Forward_Sales_Report2 _ForwardSalesrpt2 = new Forward_Sales_Report2();
        public Forward_Sales_Report_cost _ForwardSalesrptcost = new Forward_Sales_Report_cost();
        public CollectionDetails _Collectiondetail = new CollectionDetails();
        public SRN _SRNreport = new SRN();
        public Quotation_RepPrint _QuoPrint = new Quotation_RepPrint();
        public Delivered_Sales_GRN _delSalesrptGRN = new Delivered_Sales_GRN();
        public SalesReversalDetails _SalesReversalDetails = new SalesReversalDetails();
        public Discount_Report _discountReport = new Discount_Report();
        public GP_Analysis_Report _gpAnalysisRpt = new GP_Analysis_Report();
        public GP_Detail_Repl _gpdtlrepl = new GP_Detail_Repl();
        public excecutivewisesales _exectSales = new excecutivewisesales();
        public Valuation_Dtl _valdtl = new Valuation_Dtl();
        public CustomerEntryReport _CustomerEntryReport = new CustomerEntryReport();
        public SelloutDetails _Sellout = new SelloutDetails();
        public ReturnChequeDetail _chequedetail = new ReturnChequeDetail();
        //public ReturnChequeDetail _chequedetail = new ReturnChequeDetail();
        public BOQ_Details_Rpt _boqDetails = new BOQ_Details_Rpt();
        public SRN_AAL _SRNreport_AAL = new SRN_AAL();
        public SalesInv_ABE _invABE = new SalesInv_ABE();
        public SRN_ABE _SRNreport_ABE = new SRN_ABE();
        public SalesInv_AEC _invAEC = new SalesInv_AEC();
        DataTable tmp_user_pc = new DataTable();
        DataTable GLOB_DataTable = new DataTable();

        public Sales_Order_Summary _sosumAAL = new Sales_Order_Summary();

        Services.Base bsObj;
        public clsSales()
        {
            bsObj = new Services.Base();

        }

        public void SalesReconcilationReport(InvReportPara _objRepoPara)
        {// kapila 20/3/2017
            DataTable param = new DataTable();
            DataRow dr;
            tmp_user_pc = new DataTable();
            GLOB_DataTable = new DataTable();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB_AllCom(_objRepoPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetSalesReconcilationDetails(Convert.ToDateTime(_objRepoPara._GlbReportFromDate).Date,
                        Convert.ToDateTime(_objRepoPara._GlbReportToDate).Date, drow["tpl_com"].ToString(),
                        drow["tpl_pc"].ToString()
                        );
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("PARA_USER", typeof(string));
            param.Columns.Add("PARA_PERIOD", typeof(string));
            param.Columns.Add("PARA_PCENTER", typeof(string));

            dr = param.NewRow();
            dr["PARA_USER"] = _objRepoPara._GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + _objRepoPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepoPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = _objRepoPara._GlbReportProfit == "" ? "ALL" : _objRepoPara._GlbReportProfit;


            param.Rows.Add(dr);


            if (_objRepoPara._GlbReportName == "SalesReconcilation.rpt")
            {
                _salesRecon.Database.Tables["GLB_SALES_RECON"].SetDataSource(GLOB_DataTable);
                _salesRecon.Database.Tables["REP_PARA"].SetDataSource(param);
            };

            if (GLOB_DataTable.Rows.Count > 0)
            {
                foreach (DataRow drow1 in GLOB_DataTable.Rows)
                {
                    WriteErrLog(drow1["pc_code"].ToString() + " Report : DeliveredSalesReport", _objRepoPara._GlbUserID);
                }
            }


        }

        public void QuotationPrintReport(string _docno, string _com)
        {// Sanjeewa 05-07-2016
            DataTable QUO_DTL = bsObj.CHNLSVC.MsgPortal.GetQuotationPrintDetails(_docno);
            DataTable QUO_WARR_DTL = bsObj.CHNLSVC.MsgPortal.GetQuotationWarrantyPrintDetails(_com);

            _QuoPrint.Database.Tables["QUO_DTL"].SetDataSource(QUO_DTL);
            _QuoPrint.Database.Tables["QUO_WARR_DTL"].SetDataSource(QUO_WARR_DTL);
        }

        public void DeliveredSalesGRNReport(InvReportPara _objRepoPara)
        {// Sanjeewa 11-07-2016
            DataTable param = new DataTable();
            DataTable DEL_SER = new DataTable();
            DataRow dr;
            int i = 0;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB_AllCom(_objRepoPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    i = i + 1;
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetDeliveredSalesGRNDetails(Convert.ToDateTime(_objRepoPara._GlbReportFromDate).Date,
                        Convert.ToDateTime(_objRepoPara._GlbReportToDate).Date,
                        _objRepoPara._GlbReportCustomer,
                        _objRepoPara._GlbReportExecutive,
                        _objRepoPara._GlbReportDocType,
                        _objRepoPara._GlbReportItemCode,
                        _objRepoPara._GlbReportBrand,
                        _objRepoPara._GlbReportModel,
                        _objRepoPara._GlbReportItemCat1,
                        _objRepoPara._GlbReportItemCat2,
                        _objRepoPara._GlbReportItemCat3,
                         Convert.ToString(i),
                        _objRepoPara._GlbUserID,
                        _objRepoPara._GlbReportType,
                        _objRepoPara._GlbReportItemStatus,
                        _objRepoPara._GlbReportDoc,
                        _objRepoPara._GlbReportSupplier,
                        _objRepoPara._GlbReportPONo,
                        drow["tpl_com"].ToString(),
                        drow["tpl_pc"].ToString(),
                        _objRepoPara._GlbReportExport);
                    GLOB_DataTable.Merge(TMP_DataTable);
                    DataTable TMP_DataTable_NEW = TMP_DataTable.DefaultView.ToTable(true, "do_no", "item_code");

                    if (TMP_DataTable_NEW.Rows.Count > 0)
                    {
                        foreach (DataRow drow1 in TMP_DataTable_NEW.Rows)
                        {
                            DataTable TMP_DataTable_SER = new DataTable();
                            TMP_DataTable_SER = bsObj.CHNLSVC.MsgPortal.GetDeliveredSerDetails(drow1["do_no"].ToString(), drow1["item_code"].ToString(), _objRepoPara._GlbUserID, _objRepoPara._GlbReportExport, 0);
                            DEL_SER.Merge(TMP_DataTable_SER);
                        }
                    }

                }

                DataTable TMP_DataTable_SER1 = new DataTable();
                TMP_DataTable_SER1 = bsObj.CHNLSVC.MsgPortal.GetDeliveredSerDetails("", "", _objRepoPara._GlbUserID, _objRepoPara._GlbReportExport, 1);
                DEL_SER.Merge(TMP_DataTable_SER1);
            }

            param.Clear();

            param.Columns.Add("PARA_USER", typeof(string));
            param.Columns.Add("PARA_PERIOD", typeof(string));
            param.Columns.Add("PARA_PCENTER", typeof(string));
            param.Columns.Add("PARA_CUST", typeof(string));
            param.Columns.Add("PARA_EXEC", typeof(string));
            param.Columns.Add("PARA_DOCTYPE", typeof(string));
            param.Columns.Add("PARA_ITCODE", typeof(string));
            param.Columns.Add("PARA_BRAND", typeof(string));
            param.Columns.Add("PARA_MODEL", typeof(string));
            param.Columns.Add("PARA_CAT1", typeof(string));
            param.Columns.Add("PARA_CAT2", typeof(string));
            param.Columns.Add("PARA_CAT3", typeof(string));
            param.Columns.Add("PARA_STKTYPE", typeof(string));
            param.Columns.Add("PARA_INVNO", typeof(string));
            param.Columns.Add("PARA_SUPP", typeof(string));
            param.Columns.Add("PARA_PONO", typeof(string));
            param.Columns.Add("PARA_HEADING", typeof(string));

            dr = param.NewRow();
            dr["PARA_USER"] = _objRepoPara._GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + _objRepoPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepoPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = _objRepoPara._GlbReportProfit == "" ? "ALL" : _objRepoPara._GlbReportProfit;
            dr["PARA_CUST"] = _objRepoPara._GlbReportCustomer == "" ? "ALL" : _objRepoPara._GlbReportCustomer;
            dr["PARA_EXEC"] = _objRepoPara._GlbReportExecutive == "" ? "ALL" : _objRepoPara._GlbReportExecutive;
            dr["PARA_DOCTYPE"] = _objRepoPara._GlbReportDocType == "" ? "ALL" : _objRepoPara._GlbReportDocType;
            dr["PARA_ITCODE"] = _objRepoPara._GlbReportItemCode == "" ? "ALL" : _objRepoPara._GlbReportItemCode;
            dr["PARA_BRAND"] = _objRepoPara._GlbReportBrand == "" ? "ALL" : _objRepoPara._GlbReportBrand;
            dr["PARA_MODEL"] = _objRepoPara._GlbReportModel == "" ? "ALL" : _objRepoPara._GlbReportModel;
            dr["PARA_CAT1"] = _objRepoPara._GlbReportItemCat1 == "" ? "ALL" : _objRepoPara._GlbReportItemCat1;
            dr["PARA_CAT2"] = _objRepoPara._GlbReportItemCat2 == "" ? "ALL" : _objRepoPara._GlbReportItemCat2;
            dr["PARA_CAT3"] = _objRepoPara._GlbReportItemCat3 == "" ? "ALL" : _objRepoPara._GlbReportItemCat3;
            dr["PARA_STKTYPE"] = _objRepoPara._GlbReportItemStatus == "" ? "ALL" : _objRepoPara._GlbReportItemStatus;
            dr["PARA_INVNO"] = _objRepoPara._GlbReportDoc == "" ? "ALL" : _objRepoPara._GlbReportDoc;
            dr["PARA_PONO"] = _objRepoPara._GlbReportPONo == "" ? "ALL" : _objRepoPara._GlbReportPONo;
            dr["PARA_SUPP"] = _objRepoPara._GlbReportSupplier == "" ? "ALL" : _objRepoPara._GlbReportSupplier;
            dr["PARA_HEADING"] = _objRepoPara._GlbReportHeading;

            param.Rows.Add(dr);

            if (_objRepoPara._GlbReportName == "Delivered_Sales_GRN.rpt")
            {
                _delSalesrptGRN.Database.Tables["GLB_REP_SALES_GRN"].SetDataSource(GLOB_DataTable);
                _delSalesrptGRN.Database.Tables["REP_PARA"].SetDataSource(param);

                foreach (object repOp in _delSalesrptGRN.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "del_serial")
                        {
                            ReportDocument subRepDoc = _delSalesrptGRN.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["GLB_REP_DEL_SER"].SetDataSource(DEL_SER);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }
                        _cs.Dispose();
                    }
                }

            };

        }
        public void DeliveredSalesReport(InvReportPara _objRepoPara)
        {// Sanjeewa 27-04-2016
            DataTable param = new DataTable();
            DataRow dr;
            tmp_user_pc = new DataTable();
            GLOB_DataTable = new DataTable();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB_AllCom(_objRepoPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetDeliveredSalesDetails(Convert.ToDateTime(_objRepoPara._GlbReportFromDate).Date,
                        Convert.ToDateTime(_objRepoPara._GlbReportToDate).Date,
                        _objRepoPara._GlbReportCustomer,
                        _objRepoPara._GlbReportExecutive,
                        _objRepoPara._GlbReportDocType,
                        _objRepoPara._GlbReportItemCode,
                        _objRepoPara._GlbReportBrand,
                        _objRepoPara._GlbReportModel,
                        _objRepoPara._GlbReportItemCat1,
                        _objRepoPara._GlbReportItemCat2,
                        _objRepoPara._GlbReportItemCat3,
                        _objRepoPara._GlbReportProfit,
                        _objRepoPara._GlbUserID,
                        _objRepoPara._GlbReportType,
                        _objRepoPara._GlbReportItemStatus,
                        _objRepoPara._GlbReportDoc,
                        drow["tpl_pc"].ToString(),
                        drow["tpl_com"].ToString(),
                        _objRepoPara._GlbReportPromotor,
                        _objRepoPara._GlbReportIsFreeIssue,
                        2,//Company Currency
                        _objRepoPara._GlbReportFromAge,
                        "", ""
                        );
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("PARA_USER", typeof(string));
            param.Columns.Add("PARA_PERIOD", typeof(string));
            param.Columns.Add("PARA_PCENTER", typeof(string));
            param.Columns.Add("PARA_CUST", typeof(string));
            param.Columns.Add("PARA_EXEC", typeof(string));
            param.Columns.Add("PARA_DOCTYPE", typeof(string));
            param.Columns.Add("PARA_ITCODE", typeof(string));
            param.Columns.Add("PARA_BRAND", typeof(string));
            param.Columns.Add("PARA_MODEL", typeof(string));
            param.Columns.Add("PARA_CAT1", typeof(string));
            param.Columns.Add("PARA_CAT2", typeof(string));
            param.Columns.Add("PARA_CAT3", typeof(string));
            param.Columns.Add("PARA_CAT4", typeof(string));
            param.Columns.Add("PARA_CAT5", typeof(string));
            param.Columns.Add("PARA_STKTYPE", typeof(string));
            param.Columns.Add("PARA_INVNO", typeof(string));
            param.Columns.Add("PARA_PROMOTOR", typeof(string));
            param.Columns.Add("PARA_HEADING", typeof(string));
            param.Columns.Add("PARA_FREE_ISS_TP", typeof(int));

            param.Columns.Add("GRP_PCENTER", typeof(int));
            param.Columns.Add("GRP_CUST", typeof(int));
            param.Columns.Add("GRP_EXEC", typeof(int));
            param.Columns.Add("GRP_DOCTYPE", typeof(int));
            param.Columns.Add("GRP_ITCODE", typeof(int));
            param.Columns.Add("GRP_BRAND", typeof(int));
            param.Columns.Add("GRP_MODEL", typeof(int));
            param.Columns.Add("GRP_CAT1", typeof(int));
            param.Columns.Add("GRP_CAT2", typeof(int));
            param.Columns.Add("GRP_CAT3", typeof(int));
            param.Columns.Add("GRP_CAT4", typeof(int));
            param.Columns.Add("GRP_CAT5", typeof(int));
            param.Columns.Add("GRP_STKTYPE", typeof(int));
            param.Columns.Add("GRP_INVNO", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP_CAT", typeof(string));
            param.Columns.Add("GRP_PRMOTOR", typeof(int));
            param.Columns.Add("GRP_DLOC", typeof(int));
            param.Columns.Add("GRP_JOBNO", typeof(int));

            dr = param.NewRow();
            dr["PARA_USER"] = _objRepoPara._GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + _objRepoPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepoPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = _objRepoPara._GlbReportProfit == "" ? "ALL" : _objRepoPara._GlbReportProfit;
            dr["PARA_CUST"] = _objRepoPara._GlbReportCustomer == "" ? "ALL" : _objRepoPara._GlbReportCustomer;
            dr["PARA_EXEC"] = _objRepoPara._GlbReportExecutive == "" ? "ALL" : _objRepoPara._GlbReportExecutive;
            dr["PARA_DOCTYPE"] = _objRepoPara._GlbReportDocType == "" ? "ALL" : _objRepoPara._GlbReportDocType;
            dr["PARA_ITCODE"] = _objRepoPara._GlbReportItemCode == "" ? "ALL" : _objRepoPara._GlbReportItemCode;
            dr["PARA_BRAND"] = _objRepoPara._GlbReportBrand == "" ? "ALL" : _objRepoPara._GlbReportBrand;
            dr["PARA_MODEL"] = _objRepoPara._GlbReportModel == "" ? "ALL" : _objRepoPara._GlbReportModel;
            dr["PARA_CAT1"] = _objRepoPara._GlbReportItemCat1 == "" ? "ALL" : _objRepoPara._GlbReportItemCat1;
            dr["PARA_CAT2"] = _objRepoPara._GlbReportItemCat2 == "" ? "ALL" : _objRepoPara._GlbReportItemCat2;
            dr["PARA_CAT3"] = _objRepoPara._GlbReportItemCat3 == "" ? "ALL" : _objRepoPara._GlbReportItemCat3;
            dr["PARA_CAT4"] = _objRepoPara._GlbReportItemCat4 == "" ? "ALL" : _objRepoPara._GlbReportItemCat4;
            dr["PARA_CAT5"] = _objRepoPara._GlbReportItemCat5 == "" ? "ALL" : _objRepoPara._GlbReportItemCat5;
            dr["PARA_STKTYPE"] = _objRepoPara._GlbReportItemStatus == "" ? "ALL" : _objRepoPara._GlbReportItemStatus;
            dr["PARA_INVNO"] = _objRepoPara._GlbReportDoc == "" ? "ALL" : _objRepoPara._GlbReportDoc;
            dr["PARA_PROMOTOR"] = _objRepoPara._GlbReportPromotor == "" ? "ALL" : _objRepoPara._GlbReportPromotor;
            dr["PARA_HEADING"] = _objRepoPara._GlbReportHeading;
            dr["PARA_FREE_ISS_TP"] = _objRepoPara._GlbReportIsFreeIssue;

            dr["GRP_PCENTER"] = _objRepoPara._GlbReportGroupPC;
            dr["GRP_DOCTYPE"] = _objRepoPara._GlbReportGroupDocTp;
            dr["GRP_CUST"] = _objRepoPara._GlbReportGroupCust;
            dr["GRP_EXEC"] = _objRepoPara._GlbReportGroupExec;
            dr["GRP_ITCODE"] = _objRepoPara._GlbReportGroupItem;
            dr["GRP_BRAND"] = _objRepoPara._GlbReportGroupBrand;
            dr["GRP_MODEL"] = _objRepoPara._GlbReportGroupModel;
            dr["GRP_CAT1"] = _objRepoPara._GlbReportGroupCat1;
            dr["GRP_CAT2"] = _objRepoPara._GlbReportGroupCat2;
            dr["GRP_CAT3"] = _objRepoPara._GlbReportGroupCat3;
            dr["GRP_CAT4"] = _objRepoPara._GlbReportGroupCat4;
            dr["GRP_CAT5"] = _objRepoPara._GlbReportGroupCat5;
            dr["GRP_STKTYPE"] = _objRepoPara._GlbReportGroupStockTp;
            dr["GRP_INVNO"] = _objRepoPara._GlbReportGroupDocNo;
            dr["GRP_LAST_GROUP"] = _objRepoPara._GlbReportGroupLastGroup;
            dr["GRP_LAST_GROUP_CAT"] = _objRepoPara._GlbReportGroupLastGroupCat;
            dr["GRP_PRMOTOR"] = _objRepoPara._GlbReportGroupPromotor;
            dr["GRP_DLOC"] = _objRepoPara._GlbReportGroupDLoc;
            dr["GRP_JOBNO"] = _objRepoPara._GlbReportGroupJobNo;

            param.Rows.Add(dr);


            if (_objRepoPara._GlbReportName == "DeliveredSalesReport.rpt")
            {
                _delSalesrptPC.Database.Tables["GLB_REP_SALES"].SetDataSource(GLOB_DataTable);
                _delSalesrptPC.Database.Tables["REP_PARA"].SetDataSource(param);
            };

            if (GLOB_DataTable.Rows.Count > 0)
            {
                foreach (DataRow drow1 in GLOB_DataTable.Rows)
                {
                    WriteErrLog(drow1["pc_code"].ToString() + " Report : DeliveredSalesReport", _objRepoPara._GlbUserID);
                }
            }
            //else if (Session["GlbReportName"] == "DeliveredSalesReport_withCust.rpt")
            //{
            //    _delSalesrptCust.Database.Tables["GLB_REP_SALES"].SetDataSource(GLOB_DataTable);
            //    _delSalesrptCust.Database.Tables["REP_PARA"].SetDataSource(param);
            //}
            //else
            //{
            //    _delSalesrptItem.Database.Tables["GLB_REP_SALES"].SetDataSource(GLOB_DataTable);
            //    _delSalesrptItem.Database.Tables["REP_PARA"].SetDataSource(param);
            //};

        }

        public void GpDetailwithReplacementReport(InvReportPara _objRepoPara)
        {// Sanjeewa 25-10-2016
            DataTable param = new DataTable();
            DataRow dr;
            tmp_user_pc = new DataTable();
            GLOB_DataTable = new DataTable();

            GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.GetItemWiseGp_Rpl(_objRepoPara._GlbReportFromDate, _objRepoPara._GlbReportToDate, _objRepoPara._GlbReportCustomer, _objRepoPara._GlbReportExecutive, _objRepoPara._GlbReportDocType, _objRepoPara._GlbReportItemCode,
                    _objRepoPara._GlbReportBrand, _objRepoPara._GlbReportModel, _objRepoPara._GlbReportItemCat1, _objRepoPara._GlbReportItemCat2, _objRepoPara._GlbReportItemCat3, _objRepoPara._GlbReportItemCat4, _objRepoPara._GlbReportItemCat5,
                    _objRepoPara._GlbUserID, _objRepoPara._GlbReportType, _objRepoPara._GlbReportItemStatus, _objRepoPara._GlbReportDoc, _objRepoPara._GlbReportCompCode,
                    _objRepoPara._GlbReportPromotor, _objRepoPara._GlbReportIsFreeIssue, _objRepoPara._GlbReportItmClasif, _objRepoPara._GlbReportBrandMgr, "", true,
                    _objRepoPara._GlbReportIsReplItem, _objRepoPara._GlbReportFromDate2, _objRepoPara._GlbReportToDate2, 0);

            param.Clear();

            param.Columns.Add("PARA_USER", typeof(string));
            param.Columns.Add("PARA_PERIOD", typeof(string));
            param.Columns.Add("PARA_PCENTER", typeof(string));
            param.Columns.Add("PARA_CUST", typeof(string));
            param.Columns.Add("PARA_EXEC", typeof(string));
            param.Columns.Add("PARA_DOCTYPE", typeof(string));
            param.Columns.Add("PARA_ITCODE", typeof(string));
            param.Columns.Add("PARA_BRAND", typeof(string));
            param.Columns.Add("PARA_MODEL", typeof(string));
            param.Columns.Add("PARA_CAT1", typeof(string));
            param.Columns.Add("PARA_CAT2", typeof(string));
            param.Columns.Add("PARA_CAT3", typeof(string));
            param.Columns.Add("PARA_CAT4", typeof(string));
            param.Columns.Add("PARA_CAT5", typeof(string));
            param.Columns.Add("PARA_STKTYPE", typeof(string));
            param.Columns.Add("PARA_INVNO", typeof(string));
            param.Columns.Add("PARA_PROMOTOR", typeof(string));
            param.Columns.Add("PARA_HEADING", typeof(string));
            param.Columns.Add("PARA_FREE_ISS_TP", typeof(int));

            param.Columns.Add("GRP_PCENTER", typeof(int));
            param.Columns.Add("GRP_CUST", typeof(int));
            param.Columns.Add("GRP_EXEC", typeof(int));
            param.Columns.Add("GRP_DOCTYPE", typeof(int));
            param.Columns.Add("GRP_ITCODE", typeof(int));
            param.Columns.Add("GRP_BRAND", typeof(int));
            param.Columns.Add("GRP_MODEL", typeof(int));
            param.Columns.Add("GRP_CAT1", typeof(int));
            param.Columns.Add("GRP_CAT2", typeof(int));
            param.Columns.Add("GRP_CAT3", typeof(int));
            param.Columns.Add("GRP_CAT4", typeof(int));
            param.Columns.Add("GRP_CAT5", typeof(int));
            param.Columns.Add("GRP_STKTYPE", typeof(int));
            param.Columns.Add("GRP_INVNO", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP_CAT", typeof(string));
            param.Columns.Add("GRP_PRMOTOR", typeof(int));
            param.Columns.Add("GRP_DLOC", typeof(int));
            param.Columns.Add("GRP_JOBNO", typeof(int));

            dr = param.NewRow();
            dr["PARA_USER"] = _objRepoPara._GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + _objRepoPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepoPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = _objRepoPara._GlbReportProfit == "" ? "ALL" : _objRepoPara._GlbReportProfit;
            dr["PARA_CUST"] = _objRepoPara._GlbReportCustomer == "" ? "ALL" : _objRepoPara._GlbReportCustomer;
            dr["PARA_EXEC"] = _objRepoPara._GlbReportExecutive == "" ? "ALL" : _objRepoPara._GlbReportExecutive;
            dr["PARA_DOCTYPE"] = _objRepoPara._GlbReportDocType == "" ? "ALL" : _objRepoPara._GlbReportDocType;
            dr["PARA_ITCODE"] = _objRepoPara._GlbReportItemCode == "" ? "ALL" : _objRepoPara._GlbReportItemCode;
            dr["PARA_BRAND"] = _objRepoPara._GlbReportBrand == "" ? "ALL" : _objRepoPara._GlbReportBrand;
            dr["PARA_MODEL"] = _objRepoPara._GlbReportModel == "" ? "ALL" : _objRepoPara._GlbReportModel;
            dr["PARA_CAT1"] = _objRepoPara._GlbReportItemCat1 == "" ? "ALL" : _objRepoPara._GlbReportItemCat1;
            dr["PARA_CAT2"] = _objRepoPara._GlbReportItemCat2 == "" ? "ALL" : _objRepoPara._GlbReportItemCat2;
            dr["PARA_CAT3"] = _objRepoPara._GlbReportItemCat3 == "" ? "ALL" : _objRepoPara._GlbReportItemCat3;
            dr["PARA_CAT4"] = _objRepoPara._GlbReportItemCat4 == "" ? "ALL" : _objRepoPara._GlbReportItemCat4;
            dr["PARA_CAT5"] = _objRepoPara._GlbReportItemCat5 == "" ? "ALL" : _objRepoPara._GlbReportItemCat5;
            dr["PARA_STKTYPE"] = _objRepoPara._GlbReportItemStatus == "" ? "ALL" : _objRepoPara._GlbReportItemStatus;
            dr["PARA_INVNO"] = _objRepoPara._GlbReportDoc == "" ? "ALL" : _objRepoPara._GlbReportDoc;
            dr["PARA_PROMOTOR"] = _objRepoPara._GlbReportPromotor == "" ? "ALL" : _objRepoPara._GlbReportPromotor;
            dr["PARA_HEADING"] = _objRepoPara._GlbReportHeading;
            dr["PARA_FREE_ISS_TP"] = _objRepoPara._GlbReportIsFreeIssue;

            dr["GRP_PCENTER"] = _objRepoPara._GlbReportGroupPC;
            dr["GRP_DOCTYPE"] = _objRepoPara._GlbReportGroupDocTp;
            dr["GRP_CUST"] = _objRepoPara._GlbReportGroupCust;
            dr["GRP_EXEC"] = _objRepoPara._GlbReportGroupExec;
            dr["GRP_ITCODE"] = _objRepoPara._GlbReportGroupItem;
            dr["GRP_BRAND"] = _objRepoPara._GlbReportGroupBrand;
            dr["GRP_MODEL"] = _objRepoPara._GlbReportGroupModel;
            dr["GRP_CAT1"] = _objRepoPara._GlbReportGroupCat1;
            dr["GRP_CAT2"] = _objRepoPara._GlbReportGroupCat2;
            dr["GRP_CAT3"] = _objRepoPara._GlbReportGroupCat3;
            dr["GRP_CAT4"] = _objRepoPara._GlbReportGroupCat4;
            dr["GRP_CAT5"] = _objRepoPara._GlbReportGroupCat5;
            dr["GRP_STKTYPE"] = _objRepoPara._GlbReportGroupStockTp;
            dr["GRP_INVNO"] = _objRepoPara._GlbReportGroupDocNo;
            dr["GRP_LAST_GROUP"] = _objRepoPara._GlbReportGroupLastGroup;
            dr["GRP_LAST_GROUP_CAT"] = _objRepoPara._GlbReportGroupLastGroupCat;
            dr["GRP_PRMOTOR"] = _objRepoPara._GlbReportGroupPromotor;
            dr["GRP_DLOC"] = _objRepoPara._GlbReportGroupDLoc;
            dr["GRP_JOBNO"] = _objRepoPara._GlbReportGroupJobNo;

            param.Rows.Add(dr);

            _gpdtlrepl.Database.Tables["GP_DTL"].SetDataSource(GLOB_DataTable);
            _gpdtlrepl.Database.Tables["REP_PARA"].SetDataSource(param);

        }


        private void WriteErrLog(string _txt, string _user)
        {
            try
            {
                //using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/reporterr.txt"), true))
                //{
                //    _testData.WriteLine(DateTime.Now.ToString("dd/MMM/yyyy  hh:mm:ss tt") + " - " + _txt + " User :" + _user);

                //}
            }
            catch (Exception _err)
            {
                //DisplayMessage(_err.Message, 4);
            }
        }

        public void DebtorSettlementPrint(InvReportPara _objRepoPara)
        {
            DataTable mst_com = new DataTable();
            DataTable glob_debt_sett = new DataTable();
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            mst_com.Clear();
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("asatdate", typeof(DateTime));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepoPara._GlbUserID;
            dr["asatdate"] = _objRepoPara._GlbReportAsAtDate;
            dr["comp"] = _objRepoPara._GlbReportCompName;
            dr["compaddr"] = _objRepoPara._GlbReportCompAddr;
            dr["profitcenter"] = _objRepoPara._GlbReportProfit;
            param.Rows.Add(dr);

            dr = mst_com.NewRow();
            dr["MC_CD"] = _objRepoPara._GlbReportComp;
            dr["MC_IT_POWERED"] = "";
            mst_com.Rows.Add(dr);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(_objRepoPara._GlbReportCompCode, _objRepoPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_debt_sett = new DataTable();

                    tmp_debt_sett = bsObj.CHNLSVC.Financial.Process_Debtor_Sales_Settlement(Convert.ToDateTime(_objRepoPara._GlbReportFromDate).Date, Convert.ToDateTime(_objRepoPara._GlbReportAsAtDate), _objRepoPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepoPara._GlbUserID, "N", _objRepoPara._GlbReportOutstandingStatus,"");
                    glob_debt_sett.Merge(tmp_debt_sett);

                }
            }

            if (_objRepoPara._GlbReportName == "DebtorSettlement.rpt")
            {
                _DebtSett.Database.Tables["mst_com"].SetDataSource(mst_com);
                _DebtSett.Database.Tables["param"].SetDataSource(param);
                _DebtSett.Database.Tables["sp_process_Debt_Settlements"].SetDataSource(glob_debt_sett);
            }
            if (_objRepoPara._GlbReportName == "DebtorSettlement_PC.rpt")
            {
                _DebtSettPC.Database.Tables["mst_com"].SetDataSource(mst_com);
                _DebtSettPC.Database.Tables["param"].SetDataSource(param);
                _DebtSettPC.Database.Tables["sp_process_Debt_Settlements"].SetDataSource(glob_debt_sett);
            }
            if (_objRepoPara._GlbReportName == "DebtorSettlement_Outs_PC.rpt")
            {
                _DebtSettOutPC.Database.Tables["mst_com"].SetDataSource(mst_com);
                _DebtSettOutPC.Database.Tables["param"].SetDataSource(param);
                _DebtSettOutPC.Database.Tables["sp_process_Debt_Settlements"].SetDataSource(glob_debt_sett);
            }
            if (_objRepoPara._GlbReportName == "DebtorSettlement_Outs.rpt")
            {
                _DebtSettOuts.Database.Tables["mst_com"].SetDataSource(mst_com);
                _DebtSettOuts.Database.Tables["param"].SetDataSource(param);
                _DebtSettOuts.Database.Tables["sp_process_Debt_Settlements"].SetDataSource(glob_debt_sett);
            }
            if (_objRepoPara._GlbReportName == "DebtorSettlement_Outs_PC_Meeting.rpt")
            {
                _DebtSettOutPCMeeting.Database.Tables["mst_com"].SetDataSource(mst_com);
                _DebtSettOutPCMeeting.Database.Tables["param"].SetDataSource(param);
                _DebtSettOutPCMeeting.Database.Tables["sp_process_Debt_Settlements"].SetDataSource(glob_debt_sett);
            }
            if (_objRepoPara._GlbReportName == "DebtorSettlement_Outs_PC_with_comm.rpt")
            {
                _DebtSettOutPCWithComm.Database.Tables["mst_com"].SetDataSource(mst_com);
                _DebtSettOutPCWithComm.Database.Tables["param"].SetDataSource(param);
                _DebtSettOutPCWithComm.Database.Tables["sp_process_Debt_Settlements"].SetDataSource(glob_debt_sett);
            }
        }

        public void AgeAnalysisOfDebtorsOutstandingPrint(InvReportPara _objRepoPara)
        {
            DataTable mst_com = new DataTable();
            DataTable glob_debt_age = new DataTable();
            DataTable glob_debt_adv_bal = new DataTable();
            DataTable glob_debt_adv_credit = new DataTable();
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            mst_com.Clear();
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));

            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepoPara._GlbUserID;
            dr["fromdate"] = _objRepoPara._GlbReportFromDate;
            dr["todate"] = _objRepoPara._GlbReportToDate;
            dr["comp"] = _objRepoPara._GlbReportCompName;
            dr["compaddr"] = _objRepoPara._GlbReportCompAddr;
            dr["profitcenter"] = _objRepoPara._GlbReportProfit;
            param.Rows.Add(dr);

            dr = mst_com.NewRow();
            dr["MC_CD"] = _objRepoPara._GlbReportComp;
            dr["MC_IT_POWERED"] = "";
            mst_com.Rows.Add(dr);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(_objRepoPara._GlbReportCompCode, _objRepoPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_debt_age = new DataTable();
                    DataTable tmp_debt_adv = new DataTable();
                    DataTable tmp_debt_credit = new DataTable();

                    tmp_debt_age = bsObj.CHNLSVC.Financial.Process_Age_Anal_Debt_Outstand(Convert.ToDateTime(_objRepoPara._GlbReportFromDate).Date, Convert.ToDateTime(_objRepoPara._GlbReportToDate).Date, _objRepoPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepoPara._GlbUserID, _objRepoPara._GlbReportCustomer, _objRepoPara._GlbReportCheckRegDate);
                    tmp_debt_adv = bsObj.CHNLSVC.Financial.Age_Anal_Debt_Outstand_Adv(Convert.ToDateTime(_objRepoPara._GlbReportFromDate).Date, Convert.ToDateTime(_objRepoPara._GlbReportToDate).Date, _objRepoPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepoPara._GlbUserID, _objRepoPara._GlbReportCustomer, _objRepoPara._GlbReportCheckRegDate);
                    tmp_debt_credit = bsObj.CHNLSVC.Financial.Age_Anal_Debt_Outstand_Credit(Convert.ToDateTime(_objRepoPara._GlbReportFromDate).Date, Convert.ToDateTime(_objRepoPara._GlbReportToDate).Date, _objRepoPara._GlbReportCompCode, drow["tpl_pc"].ToString(), _objRepoPara._GlbUserID, _objRepoPara._GlbReportCustomer, _objRepoPara._GlbReportCheckRegDate);
                    glob_debt_age.Merge(tmp_debt_age);
                    glob_debt_adv_bal.Merge(tmp_debt_adv);
                    glob_debt_adv_credit.Merge(tmp_debt_credit);
                }
            }

            if (_objRepoPara._GlbReportName == "Age_Debtor_Outstanding.rpt")
            {
                _AgeDebtOuts.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOuts.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOuts.Database.Tables["param"].SetDataSource(param);

                foreach (object repOp in _AgeDebtOuts.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();

                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "Age_Debtor_out_Adv_sub")
                        {
                            ReportDocument subRepDoc = _AgeDebtOuts.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["SP_AGE_ADV_BAL"].SetDataSource(glob_debt_adv_bal);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }

                        if (_cs.SubreportName == "Age_Debtor_out_Credit_sub")
                        {
                            ReportDocument subRepDoc = _AgeDebtOuts.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["SP_AGE_ADV_CREDIT"].SetDataSource(glob_debt_adv_credit);
                            subRepDoc.Close();
                            subRepDoc.Dispose();
                        }

                        _cs.Dispose();
                    }
                }      

            }
            if (_objRepoPara._GlbReportName == "Age_Debtor_Outstanding_PC.rpt")
            {
                _AgeDebtOutsPC.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOutsPC.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOutsPC.Database.Tables["param"].SetDataSource(param);

                //foreach (object repOp in _AgeDebtOutsPC.ReportDefinition.ReportObjects)
                //{
                //    string _s = repOp.GetType().ToString();

                //    if (_s.ToLower().Contains("subreport"))
                //    {
                //        SubreportObject _cs = (SubreportObject)repOp;
                //        if (_cs.SubreportName == "Age_Debtor_out_Adv_sub_PC")
                //        {
                //            ReportDocument subRepDoc = _AgeDebtOutsPC.Subreports[_cs.SubreportName];
                //            subRepDoc.Database.Tables["SP_AGE_ADV_BAL"].SetDataSource(glob_debt_adv_bal);
                //            subRepDoc.Close();
                //            subRepDoc.Dispose();
                //        }

                //        if (_cs.SubreportName == "Age_Debtor_out_Credit_sub_PC")
                //        {
                //            ReportDocument subRepDoc = _AgeDebtOutsPC.Subreports[_cs.SubreportName];
                //            subRepDoc.Database.Tables["SP_AGE_ADV_CREDIT"].SetDataSource(glob_debt_adv_credit);
                //            subRepDoc.Close();
                //            subRepDoc.Dispose();
                //        }

                //        _cs.Dispose();
                //    }
                //}
            }
            if (_objRepoPara._GlbReportName == "Age_Debtor_Outstanding_new.rpt")
            {
                _AgeDebtOuts_new.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOuts_new.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOuts_new.Database.Tables["param"].SetDataSource(param);
            }
            if (_objRepoPara._GlbReportName == "Age_Debtor_Outstanding_PC_new.rpt")
            {
                _AgeDebtOutsPC_new.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOutsPC_new.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOutsPC_new.Database.Tables["param"].SetDataSource(param);
            }

        }
        public void InvoicePrintAUTO(string _invoiceno)
        {
            string invNo = default(string);
            string accNo = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = _invoiceno;
            DataTable mst_tax_master = new DataTable();
            DataRow drr;
            DataTable salesDetails = new DataTable();
            //DataTable sat_vou_det = new DataTable();
            DataTable param = new DataTable();

            param.Clear();
            param.Columns.Add("isCnote", typeof(Int16));
            param.Columns.Add("vatrate", typeof(string));
            Int16 isCredit = 0;

            salesDetails.Clear();

            DataTable typedef = new DataTable();
            DataTable tyedef1 = new DataTable();
            salesDetails = bsObj.CHNLSVC.Sales.GetSalesDetails(invNo, null);


            typedef = bsObj.CHNLSVC.Sales.typedetails(invNo);


            tyedef1.Columns.Add("invtype", typeof(string));
            DataRow dr6;
            if (typedef != null)
            {
                foreach (DataRow data1 in typedef.Rows)
                {
                    if (data1["invtype"].ToString() == "")
                    {
                        data1["invtype"] = "N/A";
                    }

                    string intype = data1["invtype"].ToString();

                    dr6 = tyedef1.NewRow();
                    dr6["invtype"] = intype;
                    tyedef1.Rows.Add(dr6);


                }
            }
            _invauto.Database.Tables["invoicetype"].SetDataSource(tyedef1);
            //sat_vou_det = CHNLSVC.Sales.getSalesGiftVouchaer(invNo);
            mst_tax_master = bsObj.CHNLSVC.Sales.GetinvTax(invNo);

            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            //DataTable int_batch = new DataTable();
            // DataTable int_batch1 = new DataTable();
            DataTable MST_ITM = new DataTable();
            DataTable MST_LOC = new DataTable();
            DataTable sar_sub_tp = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            //DataTable tblComDate = new DataTable();
            DataTable JOB_ALL = new DataTable();
            DataTable dt = new DataTable();

            int _numPages = 0;
            DataRow dr3;

            _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            _invauto.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();
            JOB_ALL.Clear();

            //DataTable accountDetails = CHNLSVC.Sales.GetAccountDetails(invNo);
            //DataTable hpt_acc = new DataTable();
            DataRow dr;
            DataRow dr1;

            //hpt_acc.Columns.Add("HPA_ACC_NO", typeof(string));
            //hpt_acc.Columns.Add("HPA_ACC_CRE_DT", typeof(DateTime));
            //hpt_acc.Columns.Add("HPA_SCH_CD", typeof(string));
            //hpt_acc.Columns.Add("HPA_TERM", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_DP_VAL", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_AF_VAL", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_INIT_INS", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_INIT_VAT", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_INIT_STM", typeof(decimal));
            //hpt_acc.Columns.Add("HPA_INIT_SER_CHG", typeof(decimal));

            //foreach (DataRow row in accountDetails.Rows)
            //{
            //    dr = hpt_acc.NewRow();
            //    accNo = row["HPA_ACC_NO"].ToString();
            //    dr["HPA_ACC_NO"] = row["HPA_ACC_NO"].ToString();
            //    dr["HPA_ACC_CRE_DT"] = row["HPA_ACC_CRE_DT"].ToString();
            //    dr["HPA_SCH_CD"] = row["HPA_SCH_CD"].ToString();
            //    dr["HPA_TERM"] = Convert.ToDecimal(row["HPA_TERM"].ToString());
            //    dr["HPA_DP_VAL"] = Convert.ToDecimal(row["HPA_DP_VAL"].ToString());
            //    dr["HPA_AF_VAL"] = Convert.ToDecimal(row["HPA_AF_VAL"].ToString());
            //    dr["HPA_INIT_INS"] = Convert.ToDecimal(row["HPA_INIT_INS"].ToString());
            //    dr["HPA_INIT_VAT"] = Convert.ToDecimal(row["HPA_INIT_VAT"].ToString());
            //    dr["HPA_INIT_STM"] = Convert.ToDecimal(row["HPA_INIT_STM"].ToString());
            //    dr["HPA_INIT_SER_CHG"] = Convert.ToDecimal(row["HPA_INIT_SER_CHG"].ToString());
            //    hpt_acc.Rows.Add(dr);
            //}

            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_SVAT_NO", typeof(string));//SAH_IS_SVAT
            mst_busentity.Columns.Add("SAH_IS_SVAT", typeof(string));
            mst_busentity.Columns.Add("mbe_tit", typeof(string));

            sat_hdr1.Columns.Add("SAH_DT", typeof(DateTime));
            sat_hdr1.Columns.Add("SAH_INV_NO", typeof(string));
            sat_hdr1.Columns.Add("SAH_INV_TP", typeof(string));
            sat_hdr1.Columns.Add("SAH_IS_SVAT", typeof(Int16));
            sat_hdr1.Columns.Add("SAH_MAN_REF", typeof(string));
            sat_hdr1.Columns.Add("SAH_PC", typeof(string));
            sat_hdr1.Columns.Add("SAH_REMARKS", typeof(string));
            sat_hdr1.Columns.Add("SAH_SALES_EX_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_TAX_EXEMPTED", typeof(Int16));
            sat_hdr1.Columns.Add("SAH_TAX_INV", typeof(Int16));
            sat_hdr1.Columns.Add("SAH_ACC_NO", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_NAME", typeof(string)); //Add by Chamal 23/05/2013
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
            sat_hdr1.Columns.Add("SAH_INV_SUB_TP", typeof(string));
            sat_hdr1.Columns.Add("SAH_REF_DOC", typeof(string));
            sat_hdr1.Columns.Add("esep_name_initials", typeof(string));
            sat_hdr1.Columns.Add("SAH_ANAL_11", typeof(Int16));
            sat_hdr1.Columns.Add("sah_del_loc", typeof(string));


            sat_itm.Columns.Add("SAD_WARR_REMARKS", typeof(string));
            sat_itm.Columns.Add("SAD_UNIT_RT", typeof(decimal));
            sat_itm.Columns.Add("SAD_TOT_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_QTY", typeof(decimal));
            sat_itm.Columns.Add("SAD_ITM_TAX_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_ITM_CD", typeof(string));
            sat_itm.Columns.Add("SAD_DISC_RT", typeof(decimal));
            sat_itm.Columns.Add("SAD_DISC_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_SEQ_NO", typeof(string));
            sat_itm.Columns.Add("SAD_ITM_LINE", typeof(Int16));
            sat_itm.Columns.Add("SAD_WARR_PERIOD", typeof(Int16));
            sat_itm.Columns.Add("SAD_JOB_NO", typeof(string));
            sat_itm.Columns.Add("SAD_MERGE_ITM", typeof(string));
            sat_itm.Columns.Add("SAD_MERGE_ITM_DESC", typeof(string));
            sat_itm.Columns.Add("VAT_AMT", typeof(decimal));
            sat_itm.Columns.Add("NBT_AMT", typeof(decimal));
            sat_itm.Columns.Add("OTH_TAX_AMT", typeof(decimal));

            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));
            mst_profit_center.Columns.Add("MPC_TEL", typeof(string));
            mst_profit_center.Columns.Add("MPC_FAX", typeof(string));
            mst_profit_center.Columns.Add("MPC_EMAIL", typeof(string));
            mst_profit_center.Columns.Add("MPC_OTH_REF", typeof(string));

            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_IS_SER1", typeof(Int16));
            mst_item.Columns.Add("MI_IS_SER2", typeof(Int16));
            mst_item.Columns.Add("MI_WARR", typeof(Int16));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_IS_SER1", typeof(Int16));
            mst_item1.Columns.Add("MI_IS_SER2", typeof(Int16));
            mst_item1.Columns.Add("MI_WARR", typeof(Int16));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            mst_com.Columns.Add("MC_TAX3", typeof(string));
            mst_com.Columns.Add("MC_ANAL18", typeof(string));
            mst_com.Columns.Add("MC_ANAL19", typeof(string));

            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));

            //int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            //int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            //int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            //int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            //int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));
            //int_batch.Columns.Add("ITB_BATCH_LINE", typeof(Int16));


            foreach (DataRow row in salesDetails.Rows)
            {
                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);

                //int_batch1 = CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));
                //foreach (DataRow row1 in int_batch1.Rows)
                //{
                //    dr1 = int_batch.NewRow();
                //    dr1["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                //    dr1["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                //    dr1["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                //    dr1["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                //    dr1["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                //    dr1["ITB_BATCH_LINE"] = Convert.ToInt16(row1["ITB_BATCH_LINE"].ToString());
                //    int_batch.Rows.Add(dr1);

                //}
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_SVAT_NO"] = row["MBE_SVAT_NO"].ToString();
                    dr["SAH_IS_SVAT"] = row["SAH_IS_SVAT"].ToString();
                    dr["mbe_tit"] = row["mbe_tit"].ToString() == "" ? " " : row["mbe_tit"].ToString();
                    // BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
                    mst_busentity.Rows.Add(dr);

                    dr = sat_hdr1.NewRow();
                    dr["SAH_DT"] = Convert.ToDateTime(row["SAH_DT"].ToString());
                    dr["SAH_INV_NO"] = row["SAH_INV_NO"].ToString();
                    dr["SAH_INV_TP"] = row["SAH_INV_TP"].ToString();
                    dr["SAH_IS_SVAT"] = Convert.ToInt16(row["SAH_IS_SVAT"].ToString());
                    dr["SAH_MAN_REF"] = row["SAH_MAN_REF"].ToString();
                    dr["SAH_PC"] = row["SAH_PC"].ToString();
                    dr["SAH_REMARKS"] = row["SAH_REMARKS"].ToString();
                    dr["SAH_SALES_EX_CD"] = row["SAH_SALES_EX_CD"].ToString();
                    dr["SAH_TAX_EXEMPTED"] = Convert.ToInt16(row["SAH_TAX_EXEMPTED"].ToString());
                    dr["SAH_TAX_INV"] = Convert.ToInt16(row["SAH_TAX_INV"].ToString());
                    dr["SAH_ACC_NO"] = row["SAH_ACC_NO"].ToString();
                    dr["SAH_D_CUST_CD"] = row["SAH_D_CUST_CD"].ToString();
                    dr["SAH_D_CUST_NAME"] = row["SAH_D_CUST_NAME"].ToString();//Add by Chamal 23/05/2013
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_INV_SUB_TP"] = row["SAH_INV_SUB_TP"].ToString();
                    dr["SAH_REF_DOC"] = row["SAH_REF_DOC"].ToString();
                    dr["esep_name_initials"] = row["esep_name_initials"].ToString();



                    if (!string.IsNullOrEmpty(row["SAH_ANAL_11"].ToString()))// Nadeeka 02-03-2015
                    {
                        dr["SAH_ANAL_11"] = Convert.ToInt16(row["SAH_ANAL_11"].ToString());
                    }
                    else
                    {
                        dr["SAH_ANAL_11"] = 0;
                    }
                    if (!string.IsNullOrEmpty(row["sah_del_loc"].ToString()))
                    {
                        dr["sah_del_loc"] = row["sah_del_loc"].ToString();
                        MST_LOC = bsObj.CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
                    }
                    else
                    {
                        dr["sah_del_loc"] = string.Empty;
                    }
                    sar_sub_tp = bsObj.CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

                    sat_hdr1.Rows.Add(dr);
                };


                dr = sat_itm.NewRow();
                dr["SAD_WARR_REMARKS"] = row["SAD_WARR_REMARKS"].ToString();
                dr["SAD_UNIT_RT"] = Convert.ToDecimal(row["SAD_UNIT_RT"].ToString());
                dr["SAD_TOT_AMT"] = Convert.ToDecimal(row["SAD_TOT_AMT"].ToString());
                dr["SAD_QTY"] = Convert.ToDecimal(row["SAD_QTY"].ToString());
                dr["SAD_ITM_TAX_AMT"] = Convert.ToDecimal(row["SAD_ITM_TAX_AMT"].ToString());
                dr["SAD_ITM_CD"] = row["SAD_ITM_CD"].ToString();
                dr["SAD_DISC_RT"] = Convert.ToDecimal(row["SAD_DISC_RT"].ToString());
                dr["SAD_DISC_AMT"] = Convert.ToDecimal(row["SAD_DISC_AMT"].ToString());
                dr["SAD_SEQ_NO"] = Convert.ToDecimal(row["SAD_SEQ_NO"].ToString());
                dr["SAD_ITM_LINE"] = Convert.ToInt16(row["SAD_ITM_LINE"].ToString());
                dr["SAD_WARR_PERIOD"] = Convert.ToInt16(row["SAD_WARR_PERIOD"].ToString());
                dr["SAD_JOB_NO"] = row["SAD_JOB_NO"].ToString();
                if (row["SAD_MERGE_ITM"].ToString() == "")
                { dr["SAD_MERGE_ITM"] = "N/A"; }
                else
                { dr["SAD_MERGE_ITM"] = row["SAD_MERGE_ITM"].ToString(); }
                dr["SAD_MERGE_ITM_DESC"] = row["SAD_MERGE_ITM_DESC"].ToString();
                dr["VAT_AMT"] = Convert.ToDecimal(row["VAT_AMT"].ToString());
                dr["NBT_AMT"] = Convert.ToDecimal(row["NBT_AMT"].ToString());
                dr["OTH_TAX_AMT"] = Convert.ToDecimal(row["OTH_TAX_AMT"].ToString());
                sat_itm.Rows.Add(dr);

                dr = mst_item.NewRow();
                dr["MI_MODEL"] = row["MI_MODEL"].ToString();

                if (row["SAD_ALT_ITM_DESC"].ToString() != "" && row["SAD_ALT_ITM_DESC"].ToString() != "N/A")
                {
                    dr["MI_SHORTDESC"] = row["SAD_ALT_ITM_DESC"].ToString();
                }
                else
                {
                    dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                }


                dr["MI_CD"] = row["MI_CD"].ToString();

                mst_item.Rows.Add(dr);


                string jobnum = row["SAD_JOB_NO"].ToString();


                dt = bsObj.CHNLSVC.Sales.getjobdetailsforjobinvoiceall(jobnum);

                if (index == 0)
                {
                    //if (accountDetails.Rows.Count > 0)
                    //{
                    //    tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), accNo);
                    //}
                    //else
                    //{
                    //    tblComDate = CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), invNo);
                    //}

                    dr = mst_profit_center.NewRow();
                    dr["MPC_ADD_1"] = row["MPC_ADD_1"].ToString();
                    dr["MPC_ADD_2"] = row["MPC_ADD_2"].ToString();
                    dr["MPC_CHNL"] = row["MPC_CHNL"].ToString();
                    dr["MPC_DESC"] = row["MPC_DESC"].ToString();
                    dr["MPC_COM"] = row["MPC_COM"].ToString();
                    dr["MPC_CD"] = row["MPC_CD"].ToString();
                    dr["MPC_TEL"] = row["MPC_TEL"].ToString();
                    dr["MPC_FAX"] = row["MPC_FAX"].ToString();
                    dr["MPC_EMAIL"] = row["MPC_EMAIL"].ToString();
                    dr["MPC_OTH_REF"] = row["MPC_OTH_REF"].ToString();

                    mst_profit_center.Rows.Add(dr);

                    dr = mst_com.NewRow();
                    dr["MC_TAX1"] = row["MC_TAX1"].ToString();
                    dr["MC_TAX2"] = row["MC_TAX2"].ToString();
                    dr["MC_CD"] = row["MC_CD"].ToString();
                    dr["MC_DESC"] = row["MC_DESC"].ToString();
                    dr["MC_IT_POWERED"] = row["MC_IT_POWERED"].ToString();
                    dr["MC_TAX3"] = row["MC_TAX3"].ToString();
                    dr["MC_ANAL18"] = row["MC_ANAL18"].ToString();
                    dr["MC_ANAL19"] = row["MC_ANAL19"].ToString();
                    mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };
            }

            //DataTable deliveredSerials = CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
            //DataTable int_hdr = new DataTable();
            //DataTable int_ser = new DataTable();

            //int_hdr.Columns.Add("ITH_SEQ_NO", typeof(decimal));
            //int_hdr.Columns.Add("ITH_COM", typeof(string));
            //int_hdr.Columns.Add("ITH_OTH_DOCNO", typeof(string));
            //int_hdr.Columns.Add("ITH_DOC_NO", typeof(string));
            //int_ser.Columns.Add("ITS_SEQ_NO", typeof(string));
            //int_ser.Columns.Add("ITS_ITM_LINE", typeof(Int16));
            //int_ser.Columns.Add("ITS_BATCH_LINE", typeof(Int16));
            //int_ser.Columns.Add("ITS_SER_LINE", typeof(Int16));
            //int_ser.Columns.Add("ITS_DOC_NO", typeof(string));
            //int_ser.Columns.Add("ITS_SER_ID", typeof(string));
            //int_ser.Columns.Add("ITS_SER_1", typeof(string));
            //int_ser.Columns.Add("ITS_SER_2", typeof(string));
            //int_ser.Columns.Add("ITS_WARR_NO", typeof(string));
            //int_ser.Columns.Add("ITS_WARR_PERIOD", typeof(decimal));
            //int_ser.Columns.Add("ITS_ITM_CD", typeof(string));

            //foreach (DataRow row in deliveredSerials.Rows)
            //{
            //    dr = int_hdr.NewRow();
            //    dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
            //    dr["ITH_COM"] = row["ITH_COM"].ToString();
            //    dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
            //    dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
            //    int_hdr.Rows.Add(dr);

            //    if (row["ITS_SER_1"].ToString() != "N/A")
            //    {
            //        dr = int_ser.NewRow();
            //        dr["ITS_SEQ_NO"] = row["ITS_SEQ_NO"].ToString();
            //        dr["ITS_ITM_LINE"] = Convert.ToInt16(row["ITS_ITM_LINE"].ToString());
            //        dr["ITS_BATCH_LINE"] = Convert.ToInt16(row["ITS_BATCH_LINE"].ToString());
            //        dr["ITS_SER_LINE"] = row["ITS_SER_LINE"].ToString();
            //        dr["ITS_DOC_NO"] = row["ITS_DOC_NO"].ToString();
            //        dr["ITS_SER_ID"] = row["ITS_SER_ID"].ToString();
            //        dr["ITS_SER_1"] = row["ITS_SER_1"].ToString();
            //        dr["ITS_SER_2"] = row["ITS_SER_2"].ToString();
            //        dr["ITS_WARR_NO"] = row["ITS_WARR_NO"].ToString();
            //        dr["ITS_WARR_PERIOD"] = Convert.ToDecimal(row["ITS_WARR_PERIOD"].ToString());
            //        int_ser.Rows.Add(dr);
            //    };

            //    DataTable MST_ITM1 = new DataTable();
            //    MST_ITM1 = CHNLSVC.Sales.GetItemCode(row["ITH_COM"].ToString(), row["ITS_ITM_CD"].ToString());
            //    foreach (DataRow row1 in MST_ITM1.Rows)
            //    {
            //        dr = mst_item1.NewRow();
            //        dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
            //        dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
            //        dr["MI_CD"] = row1["MI_CD"].ToString();
            //        dr["MI_IS_SER1"] = row1["MI_IS_SER1"].ToString();
            //        dr["MI_IS_SER2"] = row1["MI_IS_SER2"].ToString();
            //        dr["MI_WARR"] = row1["MI_WARR"].ToString();
            //        mst_item1.Rows.Add(dr);
            //    }
            //}

            DataTable receiptDetails = bsObj.CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);

            DataTable sat_receiptCQ = new DataTable();
            DataTable sat_receiptitmCQ = new DataTable();
            sat_receiptCQ.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receiptCQ.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receiptCQ.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receiptCQ.Columns.Add("SAR_RECEIPT_NO", typeof(string));

            sat_receiptitmCQ.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            sat_receiptitmCQ.Columns.Add("SAPT_DESC", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_RMK", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_ANAL_3", typeof(decimal));

            foreach (DataRow row in receiptDetails.Rows)
            {
                if (row["SAR_RECEIPT_TYPE"].ToString() == "DIR")//(row["SARD_PAY_TP"].ToString() == "CHEQUE" && row["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                {
                    dr = sat_receiptCQ.NewRow();
                    dr["SAR_SEQ_NO"] = row["SAR_SEQ_NO"].ToString();
                    dr["SAR_COM_CD"] = row["SAR_COM_CD"].ToString();
                    dr["SAR_RECEIPT_TYPE"] = row["SAR_RECEIPT_TYPE"].ToString();
                    dr["SAR_RECEIPT_NO"] = row["SAR_RECEIPT_NO"].ToString();
                    sat_receiptCQ.Rows.Add(dr);

                    dr = sat_receiptitmCQ.NewRow();
                    dr["SARD_SEQ_NO"] = row["SARD_SEQ_NO"].ToString();
                    dr["SARD_RECEIPT_NO"] = row["SARD_RECEIPT_NO"].ToString();
                    dr["SARD_INV_NO"] = row["SARD_INV_NO"].ToString();
                    dr["SARD_PAY_TP"] = row["SARD_PAY_TP"].ToString();
                    dr["SARD_REF_NO"] = row["SARD_REF_NO"].ToString();
                    dr["SAPT_DESC"] = row["SAPT_DESC"].ToString();
                    dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row["SARD_SETTLE_AMT"].ToString());
                    dr["SARD_RMK"] = row["SARD_RMK"].ToString();
                    dr["SARD_ANAL_3"] = Convert.ToDecimal(row["SARD_ANAL_3"].ToString());
                    sat_receiptitmCQ.Rows.Add(dr);


                    if (row["SARD_PAY_TP"].ToString() == "CRNOTE")
                    {
                        isCredit = 1;
                    }
                };
            }

            //DataTable hpt_shed = CHNLSVC.Sales.GetAccountSchedule(invNo);
            //DataTable Promo = CHNLSVC.Sales.GetPromotionByInvoice(invNo);

            DataTable ref_rep_infor = bsObj.CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
            mst_item = mst_item.DefaultView.ToTable(true);
            //mst_tax_master
            if (isCredit == 1)
            {
                if (mst_tax_master.Rows.Count > 0)
                {
                    drr = param.NewRow();
                    drr["isCnote"] = 1;
                    drr["vatrate"] = mst_tax_master.Rows[0]["SATX_ITM_TAX_RT"];
                    param.Rows.Add(drr);
                }
            }
            else
            {
                if (mst_tax_master.Rows.Count > 0)
                {
                    drr = param.NewRow();
                    drr["isCnote"] = 0;
                    drr["vatrate"] = mst_tax_master.Rows[0]["SATX_ITM_TAX_RT"];
                    param.Rows.Add(drr);
                }

            }






            _invauto.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _invauto.Database.Tables["mst_com"].SetDataSource(mst_com);
            _invauto.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _invauto.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            _invauto.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            _invauto.Database.Tables["mst_item"].SetDataSource(mst_item);
            _invauto.Database.Tables["sec_user"].SetDataSource(sec_user);
            _invauto.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
            _invauto.Database.Tables["param"].SetDataSource(param);
            //_JobInvoice.Database.Tables["Promo"].SetDataSource(Promo);
            _invauto.Database.Tables["jobdetails"].SetDataSource(dt);

            //foreach (object repOp in _invauto.ReportDefinition.ReportObjects)
            foreach (object repOp in (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD") ? _invSGL.ReportDefinition.ReportObjects : Session["UserCompanyCode"].ToString() == "ABT" ? _invABT.ReportDefinition.ReportObjects : Session["UserCompanyCode"].ToString() == "AIS" ? _invAIS.ReportDefinition.ReportObjects : Session["UserCompanyCode"].ToString() == "AAL" ? _invauto.ReportDefinition.ReportObjects : _invdp.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    //if (_cs.SubreportName == "rptWarranty")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoiceall.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    //}

                    if (_cs.SubreportName == "rptCheque")
                    {
                        //ReportDocument subRepDoc = _invauto.Subreports[_cs.SubreportName];
                        ReportDocument subRepDoc = (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD") ? _invSGL.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "ABT" ? _invABT.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AIS" ? _invAIS.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AAL" ? _invauto.Subreports[_cs.SubreportName] : _invdp.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    //if (_cs.SubreportName == "rptAccount")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoiceall.Subreports[_cs.SubreportName];

                    //    subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                    //    subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                    //}

                    if (_cs.SubreportName == "tax")
                    {
                        //ReportDocument subRepDoc = _invauto.Subreports[_cs.SubreportName];
                        ReportDocument subRepDoc = (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD") ? _invSGL.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "ABT" ? _invABT.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AIS" ? _invAIS.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AAL" ? _invauto.Subreports[_cs.SubreportName] : _invdp.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "rptComm")
                    {
                        //ReportDocument subRepDoc = _invauto.Subreports[_cs.SubreportName];
                        ReportDocument subRepDoc = (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD") ? _invSGL.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "ABT" ? _invABT.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AIS" ? _invAIS.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AAL" ? _invauto.Subreports[_cs.SubreportName] : _invdp.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    //if (_cs.SubreportName == "rptWarr")
                    //{
                    //    mst_item1 = mst_item1.DefaultView.ToTable(true);
                    //    ReportDocument subRepDoc = _JobInvoiceall.Subreports[_cs.SubreportName];

                    //    subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                    //    subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                    //    subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
                    //    subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                    //}
                    //if (_cs.SubreportName == "giftVou")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoiceall.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                    //}
                    if (_cs.SubreportName == "loc")
                    {
                        //ReportDocument subRepDoc = _invauto.Subreports[_cs.SubreportName];
                        ReportDocument subRepDoc = (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD") ? _invSGL.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "ABT" ? _invABT.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AIS" ? _invAIS.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AAL" ? _invauto.Subreports[_cs.SubreportName] : _invdp.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }

                    //if (tblComDate.Rows.Count >0) 
                    //{
                    //  if (_cs.SubreportName == "warrComDate")
                    //  {
                    //      ReportDocument subRepDoc = _JobInvoiceall.Subreports[_cs.SubreportName];
                    //      subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                    //  }
                    //}
                }
            }

            //this.Text = "Invoice Print";
            //crystalReportViewer1.ReportSource = _invauto;
            //crystalReportViewer1.RefreshReport();
        }


        public void IvoicePrint(string _invoiceno)
        {
            DataTable MST_COM = new DataTable();
            DataTable Pendingdelivery = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            string SOA = string.Empty;
            //MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            param.Columns.Add("user", typeof(string));
            //param.Columns.Add("heading_1", typeof(string));
            //param.Columns.Add("period", typeof(string));
            //param.Columns.Add("company", typeof(string));
            //param.Columns.Add("company_name", typeof(string));
            //param.Columns.Add("companies", typeof(string));

            dr = param.NewRow();
            dr["user"] = Session["UserID"];
            //dr["heading_1"] = BaseCls.GlbReportHeading;
            //dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            //dr["company"] = BaseCls.GlbReportComp;
            //dr["company_name"] = BaseCls.GlbReportCompCode;
            //dr["companies"] = BaseCls.GlbReportCompanies;
            param.Rows.Add(dr);

            Pendingdelivery.Clear();
            DataTable testtab = new DataTable();
            //dilshan
            if (Session["UserCompanyCode"].ToString() == "AAL")
            {
                // InvoicePrintAUTO(_invoiceno);
                testtab = bsObj.CHNLSVC.MsgPortal.PrintInvoice_Hdr(Session["UserCompanyCode"].ToString(), _invoiceno);
                if (testtab.Rows.Count > 0)
                {
                    //commented by dilshan 07/03/2018
                    //DataTable tbl = bsObj.CHNLSVC.Sales.SOACancleCheck(testtab.Rows[0].Field<string>("INVH_OTHER_DOC_NO"));
                    //if (tbl.Rows.Count > 0)
                    //{
                    //    SOA = tbl.Rows[0].Field<string>("itr_req_no");
                    //}
                    //added by dilshan 07/03/2018
                    DataTable tbl = bsObj.CHNLSVC.Sales.SOACancleChecknew(testtab.Rows[0].Field<string>("INVH_OTHER_DOC_NO"), _invoiceno);
                    if (tbl.Rows.Count > 0)
                    {
                        SOA = tbl.Rows[0].Field<string>("itr_req_no");
                    }
                }
            }
            else
            {
                testtab = bsObj.CHNLSVC.MsgPortal.PrintInvoice_Hdr(Session["UserCompanyCode"].ToString(), _invoiceno);
            }

            int k = 0;
            string iswarr = "";
            foreach (var printli in testtab.Rows)
            {
                if (testtab.Rows[k]["mpc_print_wara_remarks"].ToString() == "0")
                {
                    iswarr = testtab.Rows[k]["mpc_print_wara_remarks"].ToString();
                }
                k++;
            }

            if (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD")
            {
                _invSGL.Database.Tables["WHF_INVOICE_HEADER"].SetDataSource(testtab);
            }
            else if (Session["UserCompanyCode"].ToString() == "ABE")
            {
                _invABE.Database.Tables["WHF_INVOICE_HEADER"].SetDataSource(testtab);
                _invABE.Database.Tables["param"].SetDataSource(param);
            }
            else if (Session["UserCompanyCode"].ToString() == "AEC")
            {
                _invdpAEC.Database.Tables["WHF_INVOICE_HEADER"].SetDataSource(testtab);
//                _invdpAEC.Database.Tables["param"].SetDataSource(param);
            }
            else if (Session["UserCompanyCode"].ToString() == "ABT")
            {
                _invABT.Database.Tables["WHF_INVOICE_HEADER"].SetDataSource(testtab);
            }
            else if (Session["UserCompanyCode"].ToString() == "AIS")
            {
                _invAIS.Database.Tables["WHF_INVOICE_HEADER"].SetDataSource(testtab);
            }
            else if (Session["UserCompanyCode"].ToString() == "AAL")
            {
                _invauto.Database.Tables["WHF_INVOICE_HEADER"].SetDataSource(testtab);
                //_invauto.Database.Tables["invoicetype"].SetDataSource(testtab);
            }
            else _invdp.Database.Tables["WHF_INVOICE_HEADER"].SetDataSource(testtab);

            //DataTable tmp_invHdr = new DataTable();
            //tmp_invHdr = bsObj.CHNLSVC.MsgPortal.PrintInvoice_Hdr(BaseCls.GlbReportComp,D:\Sanjeewa\FastForwardERP.Project\FastForward.SCMWeb\FastForward.SCMWeb\View\Reports\Sales\clsSales.cs BaseCls.GlbReportDoc);
            //_invdp.Database.Tables["dsInvoiceNo"].SetDataSource(tmp_invHdr);

            //DataTable tmp_invDtl = new DataTable();
            //tmp_invDtl = bsObj.CHNLSVC.MsgPortal.PrintInvoice_Dtl(BaseCls.GlbReportComp, BaseCls.GlbReportDoc);
            //_invdp.Database.Tables["dsInvoiceItem"].SetDataSource(tmp_invDtl);

            testtab = new DataTable();
            testtab = bsObj.CHNLSVC.MsgPortal.PrintInvoice_Dtl(Session["UserCompanyCode"].ToString(), _invoiceno);
            k = 0;
            foreach (var printli in testtab.Rows)
            {
                if (iswarr == "0")
                {
                    //testtab.Rows[k]["IND_WARA_PERIOD"] = 0;
                    testtab.Rows[k]["IND_WARA_REMARKS"] = "";
                }
                k++;
            }
         

            DataTable sat_vou_det = new DataTable();
            sat_vou_det = bsObj.CHNLSVC.Sales.getSalesGiftVouchaer(_invoiceno);

            DataTable _invtax = new DataTable();
            _invtax = bsObj.CHNLSVC.MsgPortal.PrintInvoice_Tax(_invoiceno);

            /* DataTable receiptDetails = bsObj.CHNLSVC.Sales.GetInvoiceReceiptDet(_invoiceno);


             DataTable sat_receiptCQ = new DataTable();
             DataTable sat_receiptitmCQ = new DataTable();
             sat_receiptCQ.Columns.Add("SAR_SEQ_NO", typeof(string));
             sat_receiptCQ.Columns.Add("SAR_COM_CD", typeof(string));
             sat_receiptCQ.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
             sat_receiptCQ.Columns.Add("SAR_RECEIPT_NO", typeof(string));


             sat_receiptitmCQ.Columns.Add("SARD_SEQ_NO", typeof(string));
             sat_receiptitmCQ.Columns.Add("SARD_RECEIPT_NO", typeof(string));
             sat_receiptitmCQ.Columns.Add("SARD_INV_NO", typeof(string));
             sat_receiptitmCQ.Columns.Add("SARD_PAY_TP", typeof(string));
             sat_receiptitmCQ.Columns.Add("SARD_REF_NO", typeof(string));
             sat_receiptitmCQ.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
             sat_receiptitmCQ.Columns.Add("SAPT_DESC", typeof(string));
             sat_receiptitmCQ.Columns.Add("SARD_RMK", typeof(string));

             DataTable mst_tax_master = bsObj.CHNLSVC.Sales.GetinvTax(_invoiceno);

             DataTable ref_rep_infor = bsObj.CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");*/

            //DataTable mst_loc = bsObj.CHNLSVC.Sales.GetLocationCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca);
            if (Session["UserCompanyCode"].ToString() == "AEC")
            {
                DataTable receiptDetails = bsObj.CHNLSVC.Sales.GetInvoiceReceiptDet(_invoiceno);
                DataTable sat_receiptCQAEC = new DataTable();
                DataTable sat_receiptitmCQAEC = new DataTable();
                sat_receiptCQAEC.Columns.Add("SETTELENAME", typeof(string));
                sat_receiptCQAEC.Columns.Add("SETTLEAMT", typeof(decimal));
                sat_receiptCQAEC.Columns.Add("BALANCEAMT", typeof(decimal));
                sat_receiptCQAEC.Columns.Add("BALANCENAME", typeof(string));
                sat_receiptCQAEC.Columns.Add("ISRECIEPT", typeof(bool));
                decimal total = 0;
                bool isRecipt = false;
                decimal totaltPayble = 0;
                decimal balanceAmt = 0;
                if (receiptDetails != null)
                {
                    foreach (DataRow satReceiptRow in receiptDetails.Rows)
                    {
                        decimal settleamt = Convert.ToDecimal(satReceiptRow["sard_settle_amt"].ToString());
                        total = total + settleamt;
                    }
                    if(receiptDetails.Rows.Count<1)
                    {
                        sat_receiptCQAEC.Rows.Add("",0,0,"",false);
                    }
                    else
                    {
                        if (testtab!=null)
                        {
                            foreach(DataRow satitmRow in testtab.Rows)
                            {
                                decimal satPrice = Convert.ToDecimal(satitmRow["sad_tot_amt"].ToString());
                                totaltPayble = totaltPayble + satPrice;
                            }
                        }
                        balanceAmt = totaltPayble-total;
                        isRecipt = true;
                        sat_receiptCQAEC.Rows.Add("Advance Received", total, balanceAmt,"Balance Payable", isRecipt);
                        
                    }
                }
                else
                {
                    sat_receiptCQAEC.Rows.Add("", 0,0,"",false);
                }

                _invdpAEC.Database.Tables["Receipt"].SetDataSource(sat_receiptCQAEC);
            }
            if (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD")
            {
                _invSGL.Database.Tables["_WHF_INVOICE_DETAILS"].SetDataSource(testtab);
            }
            else if (Session["UserCompanyCode"].ToString() == "ABE")
            {
                _invABE.Database.Tables["_WHF_INVOICE_DETAILS"].SetDataSource(testtab);
            }
            else if (Session["UserCompanyCode"].ToString() == "AEC")
            {
                _invdpAEC.Database.Tables["WHF_INVOICE_DETAILS"].SetDataSource(testtab);
            }
            else if (Session["UserCompanyCode"].ToString() == "ABT")
            {
                _invABT.Database.Tables["WHF_INVOICE_DETAILS"].SetDataSource(testtab);
            }
            else if (Session["UserCompanyCode"].ToString() == "AIS")
            {
                _invAIS.Database.Tables["_WHF_INVOICE_DETAILS"].SetDataSource(testtab);
            }
            else if (Session["UserCompanyCode"].ToString() == "AAL")
            {
                _invauto.Database.Tables["_WHF_INVOICE_DETAILS"].SetDataSource(testtab);
                //_invauto.Database.Tables["invoicetype"].SetDataSource(testtab);
            }
            else _invdp.Database.Tables["WHF_INVOICE_DETAILS"].SetDataSource(testtab);

            foreach (object repOp in (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD") ? _invSGL.ReportDefinition.ReportObjects : Session["UserCompanyCode"].ToString() == "ABT" ? _invABT.ReportDefinition.ReportObjects : Session["UserCompanyCode"].ToString() == "AIS" ? _invAIS.ReportDefinition.ReportObjects : Session["UserCompanyCode"].ToString() == "AAL" ? _invauto.ReportDefinition.ReportObjects : Session["UserCompanyCode"].ToString() == "ABE" ? _invABE.ReportDefinition.ReportObjects : Session["UserCompanyCode"].ToString() == "AEC" ? _invdpAEC.ReportDefinition.ReportObjects : _invdp.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "inv_tax")
                    {
                        ReportDocument subRepDoc = (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD") ? _invSGL.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "ABT" ? _invABT.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AIS" ? _invAIS.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AAL" ? _invauto.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "ABE" ? _invABE.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AEC" ? _invdpAEC.Subreports[_cs.SubreportName] : _invdp.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["WHF_TAX_DTL"].SetDataSource(_invtax);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "giftVou")
                    {
                        ReportDocument subRepDoc = (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD") ? _invSGL.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "ABT" ? _invABT.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AIS" ? _invAIS.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AAL" ? _invauto.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "ABE" ? _invABE.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AEC" ? _invdpAEC.Subreports[_cs.SubreportName] : _invdp.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    /* if (_cs.SubreportName == "rptCheque")
                     {
                         ReportDocument subRepDoc = (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD") ? _invSGL.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "ABT" ? _invABT.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AIS" ? _invAIS.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AAL" ? _invauto.Subreports[_cs.SubreportName] : _invdp.Subreports[_cs.SubreportName];
                         subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                         subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);
                     }


                     if (_cs.SubreportName == "tax")
                     {
                         ReportDocument subRepDoc = (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD") ? _invSGL.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "ABT" ? _invABT.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AIS" ? _invAIS.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AAL" ? _invauto.Subreports[_cs.SubreportName] : _invdp.Subreports[_cs.SubreportName];
                         subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);
                     }
                     if (_cs.SubreportName == "rptComm")
                     {
                         ReportDocument subRepDoc = (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD") ? _invSGL.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "ABT" ? _invABT.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AIS" ? _invAIS.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AAL" ? _invauto.Subreports[_cs.SubreportName] : _invdp.Subreports[_cs.SubreportName];
                         subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                     }*/

                    //if (_cs.SubreportName == "loc")
                    //{
                    //    ReportDocument subRepDoc = (Session["UserCompanyCode"].ToString() == "SGL" || Session["UserCompanyCode"].ToString() == "SGD") ? _invSGL.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "ABT" ? _invABT.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AIS" ? _invAIS.Subreports[_cs.SubreportName] : Session["UserCompanyCode"].ToString() == "AAL" ? _invauto.Subreports[_cs.SubreportName] : _invdp.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                    //}

                }
            }

            // DataTable tmp_invPay = new DataTable();
            // tmp_invPay = bsObj.CHNLSVC.MsgPortal.PrintInvoice_Pay(BaseCls.GlbReportComp, BaseCls.GlbReportDoc);
            //_invdp.Database.Tables["WHF_INV_PAY_DETAILS"].SetDataSource(tmp_invPay);                          

            //_fwSales.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            //_fwSales.Database.Tables["temp_pendingdelivery"].SetDataSource(Pendingdelivery);
            //_fwSales.Database.Tables["param"].SetDataSource(param);
            _invauto.SetParameterValue("SOA", SOA);
        }

        public void Receipt_print_n(string _docno)
        {// Sanjeewa 31-08-2015
            string docNo = default(string);
            docNo = _docno;

            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            DataTable sat_recItm = new DataTable();
            sat_receipt.Clear();

            sat_receipt = bsObj.CHNLSVC.Sales.GetReceipt(_docno);
            sat_receiptitm = bsObj.CHNLSVC.Sales.GetReceiptItemDetails(_docno);
            sat_recItm = bsObj.CHNLSVC.Sales.GetReceiptItemDetails(_docno);
            DataTable mst_profit_center = new DataTable();

            DataTable mst_rec_tp = new DataTable();
            DataTable hpt_insu = new DataTable();
            DataTable sat_recwarrex = new DataTable();
            DataTable sat_veh_reg_txn = new DataTable();
            DataTable sat_receiptitemdetails = new DataTable();
            DataTable mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(Session["UserCompanyCode"].ToString());
            DataTable mst_emp = new DataTable();


            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(_docno);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;
            PRINT_DOC.Rows.Add(dr3);
            if (Session["UserCompanyCode"].ToString() == "SGL")
            {
                recreport_SGL.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            }
            else
            {
                recreport1_n.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            }

            foreach (DataRow row in sat_receipt.Rows)
            {
                mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(row["sar_com_cd"].ToString(), row["sar_profit_center_cd"].ToString());
                mst_rec_tp = bsObj.CHNLSVC.Sales.GetReceiptType(row["SAR_RECEIPT_TYPE"].ToString());
                hpt_insu = bsObj.CHNLSVC.Sales.GetInsurance(row["SAR_RECEIPT_NO"].ToString());
                sat_recwarrex = bsObj.CHNLSVC.Sales.GetReceiptWarranty(row["SAR_RECEIPT_NO"].ToString());
                sat_veh_reg_txn = bsObj.CHNLSVC.Sales.GetVehicalRegistrations(row["SAR_RECEIPT_NO"].ToString());
                sat_receiptitemdetails = bsObj.CHNLSVC.Sales.GetAdvanRecItems(row["SAR_RECEIPT_NO"].ToString());
                mst_emp = bsObj.CHNLSVC.Sales.GetinvEmp(row["SAR_COM_CD"].ToString(), row["sar_anal_4"].ToString());
            }
            if (sat_receipt.Rows.Count <= 0)
            {
                mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable("", "");
                mst_rec_tp = bsObj.CHNLSVC.Sales.GetReceiptType("");
                hpt_insu = bsObj.CHNLSVC.Sales.GetInsurance("");
                sat_recwarrex = bsObj.CHNLSVC.Sales.GetReceiptWarranty("");
                sat_veh_reg_txn = bsObj.CHNLSVC.Sales.GetVehicalRegistrations("");
                sat_receiptitemdetails = bsObj.CHNLSVC.Sales.GetAdvanRecItems("");
                mst_emp = bsObj.CHNLSVC.Sales.GetinvEmp("", "");
            }

            DataTable mst_buscom = bsObj.CHNLSVC.Sales.GetInsuranceCompanyName(_docno);

            DataTable itemSerial = bsObj.CHNLSVC.Sales.GetInvoice_Serials(_docno);

            DataTable MST_ITM = new DataTable();

            foreach (DataRow row in sat_veh_reg_txn.Rows)
            {
                MST_ITM = bsObj.CHNLSVC.Sales.GetItemCode(Session["UserCompanyCode"].ToString(), row["srvt_itm_cd"].ToString());
            }
            if (sat_veh_reg_txn.Rows.Count <= 0)
            {
                MST_ITM = bsObj.CHNLSVC.Sales.GetItemCode("", "");
            }
            if (Session["UserCompanyCode"].ToString() == "SGL")
            {
                recreport_SGL.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                recreport_SGL.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                recreport_SGL.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);
                recreport_SGL.Database.Tables["mst_com"].SetDataSource(mst_com);
                recreport_SGL.Database.Tables["sat_recItm"].SetDataSource(sat_recItm);
            }
            else
            {
                recreport1_n.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
                recreport1_n.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                recreport1_n.Database.Tables["mst_rec_tp"].SetDataSource(mst_rec_tp);
                recreport1_n.Database.Tables["mst_com"].SetDataSource(mst_com);
            }
            // recreport1_n.Database.Tables["REPRINT_DATA"].SetDataSource(sat_receipt);

            foreach (object repOp in Session["UserCompanyCode"].ToString() == "SGL" ? recreport_SGL.ReportDefinition.ReportObjects : recreport1_n.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptVehicle")
                    {
                        ReportDocument subRepDoc = Session["UserCompanyCode"].ToString() == "SGL" ? recreport_SGL.Subreports[_cs.SubreportName] : recreport1_n.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sat_veh_reg_txn"].SetDataSource(sat_veh_reg_txn);
                        subRepDoc.Database.Tables["MST_ITM"].SetDataSource(MST_ITM);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "warr")
                    {
                        ReportDocument subRepDoc = Session["UserCompanyCode"].ToString() == "SGL" ? recreport_SGL.Subreports[_cs.SubreportName] : recreport1_n.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_recwarrex"].SetDataSource(sat_recwarrex);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "rptPower")
                    {
                        ReportDocument subRepDoc = Session["UserCompanyCode"].ToString() == "SGL" ? recreport_SGL.Subreports[_cs.SubreportName] : recreport1_n.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_com"].SetDataSource(mst_com);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "pay")
                    {
                        ReportDocument subRepDoc = Session["UserCompanyCode"].ToString() == "SGL" ? recreport_SGL.Subreports[_cs.SubreportName] : recreport1_n.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "paymode")
                    {
                        ReportDocument subRepDoc = Session["UserCompanyCode"].ToString() == "SGL" ? recreport_SGL.Subreports[_cs.SubreportName] : recreport1_n.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "inv")
                    {
                        ReportDocument subRepDoc = Session["UserCompanyCode"].ToString() == "SGL" ? recreport_SGL.Subreports[_cs.SubreportName] : recreport1_n.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "item")
                    {
                        ReportDocument subRepDoc = Session["UserCompanyCode"].ToString() == "SGL" ? recreport_SGL.Subreports[_cs.SubreportName] : recreport1_n.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receiptitemdetails"].SetDataSource(sat_receiptitemdetails);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }

                    if (_cs.SubreportName == "gv")
                    {
                        ReportDocument subRepDoc = Session["UserCompanyCode"].ToString() == "SGL" ? recreport_SGL.Subreports[_cs.SubreportName] : recreport1_n.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receiptitemdetails"].SetDataSource(sat_receiptitemdetails);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "emp")
                    {
                        ReportDocument subRepDoc = Session["UserCompanyCode"].ToString() == "SGL" ? recreport_SGL.Subreports[_cs.SubreportName] : recreport1_n.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_emp"].SetDataSource(mst_emp);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "insCom")
                    {
                        ReportDocument subRepDoc = Session["UserCompanyCode"].ToString() == "SGL" ? recreport_SGL.Subreports[_cs.SubreportName] : recreport1_n.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_buscom"].SetDataSource(mst_buscom);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }

                    if (_cs.SubreportName == "ItemSerials")
                    {
                        ReportDocument subRepDoc = Session["UserCompanyCode"].ToString() == "SGL" ? recreport_SGL.Subreports[_cs.SubreportName] : recreport1_n.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["itemSerial"].SetDataSource(itemSerial);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }

                    if (_cs.SubreportName == "paytype")
                    {
                        ReportDocument subRepDoc = Session["UserCompanyCode"].ToString() == "SGL" ? recreport_SGL.Subreports[_cs.SubreportName] : recreport1_n.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitm);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();

                }
            }
        }

        public void InvocieFullPrint()
        {// Sanjeewa 18-04-2015
            string invNo = default(string);
            string accNo = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = BaseCls.GlbReportDoc;
            DataTable mst_tax_master = new DataTable();
            DataRow drr;
            DataTable salesDetails = new DataTable();
            DataTable sat_vou_det = new DataTable();
            DataTable param = new DataTable();

            param.Clear();
            param.Columns.Add("isCnote", typeof(Int16));

            Int16 isCredit = 0;

            salesDetails.Clear();

            salesDetails = bsObj.CHNLSVC.Sales.GetSalesDetails(invNo, null);

            sat_vou_det = bsObj.CHNLSVC.Sales.getSalesGiftVouchaer(invNo);

            mst_tax_master = bsObj.CHNLSVC.Sales.GetinvTax(invNo);
            DataTable sat_hdr1 = new DataTable();
            DataTable sat_itm = new DataTable();
            DataTable mst_profit_center = new DataTable();
            DataTable mst_item = new DataTable();
            DataTable mst_item1 = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable sec_user = new DataTable();
            DataTable mst_busentity = new DataTable();
            DataTable int_batch = new DataTable();
            DataTable int_batch1 = new DataTable();
            DataTable MST_ITM = new DataTable();

            DataTable MST_LOC = new DataTable();
            DataTable sar_sub_tp = new DataTable();
            DataTable PRINT_DOC = new DataTable();


            DataTable tblComDate = new DataTable();



            int _numPages = 0;
            DataRow dr3;

            _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            invfullReport.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();


            DataTable accountDetails = bsObj.CHNLSVC.Sales.GetAccountDetails(invNo);
            DataTable hpt_acc = new DataTable();
            DataRow dr;
            DataRow dr1;

            hpt_acc.Columns.Add("HPA_ACC_NO", typeof(string));
            hpt_acc.Columns.Add("HPA_ACC_CRE_DT", typeof(DateTime));
            hpt_acc.Columns.Add("HPA_SCH_CD", typeof(string));
            hpt_acc.Columns.Add("HPA_TERM", typeof(decimal));
            hpt_acc.Columns.Add("HPA_DP_VAL", typeof(decimal));
            hpt_acc.Columns.Add("HPA_AF_VAL", typeof(decimal));
            hpt_acc.Columns.Add("HPA_INIT_INS", typeof(decimal));
            hpt_acc.Columns.Add("HPA_INIT_VAT", typeof(decimal));
            hpt_acc.Columns.Add("HPA_INIT_STM", typeof(decimal));
            hpt_acc.Columns.Add("HPA_INIT_SER_CHG", typeof(decimal));

            foreach (DataRow row in accountDetails.Rows)
            {
                dr = hpt_acc.NewRow();
                accNo = row["HPA_ACC_NO"].ToString();
                dr["HPA_ACC_NO"] = row["HPA_ACC_NO"].ToString();
                dr["HPA_ACC_CRE_DT"] = row["HPA_ACC_CRE_DT"].ToString();
                dr["HPA_SCH_CD"] = row["HPA_SCH_CD"].ToString();
                dr["HPA_TERM"] = Convert.ToDecimal(row["HPA_TERM"].ToString());
                dr["HPA_DP_VAL"] = Convert.ToDecimal(row["HPA_DP_VAL"].ToString());
                dr["HPA_AF_VAL"] = Convert.ToDecimal(row["HPA_AF_VAL"].ToString());
                dr["HPA_INIT_INS"] = Convert.ToDecimal(row["HPA_INIT_INS"].ToString());
                dr["HPA_INIT_VAT"] = Convert.ToDecimal(row["HPA_INIT_VAT"].ToString());
                dr["HPA_INIT_STM"] = Convert.ToDecimal(row["HPA_INIT_STM"].ToString());
                dr["HPA_INIT_SER_CHG"] = Convert.ToDecimal(row["HPA_INIT_SER_CHG"].ToString());
                hpt_acc.Rows.Add(dr);
            }

            mst_busentity.Columns.Add("MBE_IS_TAX", typeof(Int16));
            mst_busentity.Columns.Add("MBE_MOB", typeof(string));
            mst_busentity.Columns.Add("MBE_NIC", typeof(string));
            mst_busentity.Columns.Add("MBE_COM", typeof(string));
            mst_busentity.Columns.Add("MBE_TAX_NO", typeof(string));
            mst_busentity.Columns.Add("MBE_CD", typeof(string));
            mst_busentity.Columns.Add("MBE_TEL", typeof(string));
            mst_busentity.Columns.Add("MBE_SVAT_NO", typeof(string));

            sat_hdr1.Columns.Add("SAH_DT", typeof(DateTime));
            sat_hdr1.Columns.Add("SAH_INV_NO", typeof(string));
            sat_hdr1.Columns.Add("SAH_INV_TP", typeof(string));
            sat_hdr1.Columns.Add("SAH_IS_SVAT", typeof(Int16));
            sat_hdr1.Columns.Add("SAH_MAN_REF", typeof(string));
            sat_hdr1.Columns.Add("SAH_PC", typeof(string));
            sat_hdr1.Columns.Add("SAH_REMARKS", typeof(string));
            sat_hdr1.Columns.Add("SAH_SALES_EX_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_TAX_EXEMPTED", typeof(Int16));
            sat_hdr1.Columns.Add("SAH_TAX_INV", typeof(Int16));
            sat_hdr1.Columns.Add("SAH_ACC_NO", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_NAME", typeof(string)); //Add by Chamal 23/05/2013
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_D_CUST_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_NAME", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_CD", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD2", typeof(string));
            sat_hdr1.Columns.Add("SAH_CUS_ADD1", typeof(string));
            sat_hdr1.Columns.Add("SAH_SEQ_NO", typeof(decimal));
            sat_hdr1.Columns.Add("SAH_COM", typeof(string));
            sat_hdr1.Columns.Add("SAH_CRE_BY", typeof(string));
            sat_hdr1.Columns.Add("SAH_INV_SUB_TP", typeof(string));
            sat_hdr1.Columns.Add("SAH_REF_DOC", typeof(string));
            sat_hdr1.Columns.Add("esep_name_initials", typeof(string));
            sat_hdr1.Columns.Add("SAH_ANAL_11", typeof(Int16));
            sat_hdr1.Columns.Add("sah_del_loc", typeof(string));

            sat_itm.Columns.Add("SAD_WARR_REMARKS", typeof(string));
            sat_itm.Columns.Add("SAD_UNIT_RT", typeof(decimal));
            sat_itm.Columns.Add("SAD_TOT_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_QTY", typeof(decimal));
            sat_itm.Columns.Add("SAD_ITM_TAX_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_ITM_CD", typeof(string));
            sat_itm.Columns.Add("SAD_DISC_RT", typeof(decimal));
            sat_itm.Columns.Add("SAD_DISC_AMT", typeof(decimal));
            sat_itm.Columns.Add("SAD_SEQ_NO", typeof(string));
            sat_itm.Columns.Add("SAD_ITM_LINE", typeof(Int16));
            sat_itm.Columns.Add("SAD_WARR_PERIOD", typeof(Int16));

            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));
            mst_profit_center.Columns.Add("MPC_TEL", typeof(string));
            mst_profit_center.Columns.Add("MPC_FAX", typeof(string));
            mst_profit_center.Columns.Add("MPC_EMAIL", typeof(string));
            mst_profit_center.Columns.Add("MPC_OTH_REF", typeof(string));
            mst_profit_center.Columns.Add("MPC_ORD_ALPBT", typeof(Int16));

            mst_item.Columns.Add("MI_MODEL", typeof(string));
            mst_item.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item.Columns.Add("MI_CD", typeof(string));
            mst_item.Columns.Add("MI_IS_SER1", typeof(Int16));
            mst_item.Columns.Add("MI_IS_SER2", typeof(Int16));
            mst_item.Columns.Add("MI_WARR", typeof(Int16));

            mst_item1.Columns.Add("MI_MODEL", typeof(string));
            mst_item1.Columns.Add("MI_SHORTDESC", typeof(string));
            mst_item1.Columns.Add("MI_CD", typeof(string));
            mst_item1.Columns.Add("MI_IS_SER1", typeof(Int16));
            mst_item1.Columns.Add("MI_IS_SER2", typeof(Int16));
            mst_item1.Columns.Add("MI_WARR", typeof(Int16));

            mst_com.Columns.Add("MC_TAX1", typeof(string));
            mst_com.Columns.Add("MC_TAX2", typeof(string));
            mst_com.Columns.Add("MC_CD", typeof(string));
            mst_com.Columns.Add("MC_DESC", typeof(string));
            mst_com.Columns.Add("MC_IT_POWERED", typeof(string));
            mst_com.Columns.Add("MC_TAX3", typeof(string));
            mst_com.Columns.Add("MC_ANAL18", typeof(string));
            mst_com.Columns.Add("MC_ANAL19", typeof(string));


            sec_user.Columns.Add("SE_USR_NAME", typeof(string));
            sec_user.Columns.Add("SE_USR_ID", typeof(string));


            int_batch.Columns.Add("ITB_SEQ_NO", typeof(string));
            int_batch.Columns.Add("ITB_ITM_LINE", typeof(Int16));
            int_batch.Columns.Add("ITB_ITM_CD", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REF_NO", typeof(string));
            int_batch.Columns.Add("ITB_BASE_REFLINE", typeof(Int16));
            int_batch.Columns.Add("ITB_BATCH_LINE", typeof(Int16));


            foreach (DataRow row in salesDetails.Rows)
            {

                dr = mst_busentity.NewRow();
                int index = salesDetails.Rows.IndexOf(row);



                int_batch1 = bsObj.CHNLSVC.Inventory.GetDeliveryOrderDetailDt(row["SAH_COM"].ToString(), row["SAH_INV_NO"].ToString(), Convert.ToInt16(row["SAD_ITM_LINE"].ToString()));
                foreach (DataRow row1 in int_batch1.Rows)
                {
                    dr1 = int_batch.NewRow();
                    dr1["ITB_SEQ_NO"] = row1["ITB_SEQ_NO"].ToString();
                    dr1["ITB_ITM_LINE"] = Convert.ToInt16(row1["ITB_ITM_LINE"].ToString());
                    dr1["ITB_ITM_CD"] = row1["ITB_ITM_CD"].ToString();
                    dr1["ITB_BASE_REF_NO"] = row1["ITB_BASE_REF_NO"].ToString();
                    dr1["ITB_BASE_REFLINE"] = Convert.ToInt16(row1["ITB_BASE_REFLINE"].ToString());
                    dr1["ITB_BATCH_LINE"] = Convert.ToInt16(row1["ITB_BATCH_LINE"].ToString());
                    int_batch.Rows.Add(dr1);

                }
                if (index == 0)
                {
                    dr["MBE_IS_TAX"] = Convert.ToInt16(row["MBE_IS_TAX"].ToString());
                    dr["MBE_MOB"] = row["MBE_MOB"].ToString();
                    dr["MBE_NIC"] = row["MBE_NIC"].ToString();
                    dr["MBE_TAX_NO"] = row["MBE_TAX_NO"].ToString();
                    dr["MBE_COM"] = row["MBE_COM"].ToString();
                    dr["MBE_CD"] = row["MBE_CD"].ToString();
                    dr["MBE_TEL"] = row["MBE_TEL"].ToString();
                    dr["MBE_SVAT_NO"] = row["MBE_SVAT_NO"].ToString();

                    mst_busentity.Rows.Add(dr);


                    dr = sat_hdr1.NewRow();

                    dr["SAH_DT"] = Convert.ToDateTime(row["SAH_DT"].ToString());
                    dr["SAH_INV_NO"] = row["SAH_INV_NO"].ToString();
                    dr["SAH_INV_TP"] = row["SAH_INV_TP"].ToString();
                    dr["SAH_IS_SVAT"] = Convert.ToInt16(row["SAH_IS_SVAT"].ToString());
                    dr["SAH_MAN_REF"] = row["SAH_MAN_REF"].ToString();
                    dr["SAH_PC"] = row["SAH_PC"].ToString();
                    dr["SAH_REMARKS"] = row["SAH_REMARKS"].ToString();
                    dr["SAH_SALES_EX_CD"] = row["SAH_SALES_EX_CD"].ToString();
                    dr["SAH_TAX_EXEMPTED"] = Convert.ToInt16(row["SAH_TAX_EXEMPTED"].ToString());
                    dr["SAH_TAX_INV"] = Convert.ToInt16(row["SAH_TAX_INV"].ToString());
                    dr["SAH_ACC_NO"] = row["SAH_ACC_NO"].ToString();
                    dr["SAH_D_CUST_CD"] = row["SAH_D_CUST_CD"].ToString();
                    dr["SAH_D_CUST_NAME"] = row["SAH_D_CUST_NAME"].ToString();//Add by Chamal 23/05/2013
                    dr["SAH_D_CUST_ADD2"] = row["SAH_D_CUST_ADD2"].ToString();
                    dr["SAH_D_CUST_ADD1"] = row["SAH_D_CUST_ADD1"].ToString();
                    dr["SAH_CUS_NAME"] = row["SAH_CUS_NAME"].ToString();
                    dr["SAH_CUS_CD"] = row["SAH_CUS_CD"].ToString();
                    dr["SAH_CUS_ADD2"] = row["SAH_CUS_ADD2"].ToString();
                    dr["SAH_CUS_ADD1"] = row["SAH_CUS_ADD1"].ToString();
                    dr["SAH_SEQ_NO"] = Convert.ToDecimal(row["SAH_SEQ_NO"].ToString());
                    dr["SAH_COM"] = row["SAH_COM"].ToString();
                    dr["SAH_CRE_BY"] = row["SAH_CRE_BY"].ToString();
                    dr["SAH_INV_SUB_TP"] = row["SAH_INV_SUB_TP"].ToString();
                    dr["SAH_REF_DOC"] = row["SAH_REF_DOC"].ToString();
                    dr["esep_name_initials"] = row["esep_name_initials"].ToString();
                    if (!string.IsNullOrEmpty(row["SAH_ANAL_11"].ToString()))// Nadeeka 02-03-2015
                    {
                        dr["SAH_ANAL_11"] = Convert.ToInt16(row["SAH_ANAL_11"].ToString());
                    }
                    else
                    {
                        dr["SAH_ANAL_11"] = 0;
                    }
                    if (!string.IsNullOrEmpty(row["sah_del_loc"].ToString()))
                    {
                        dr["sah_del_loc"] = row["sah_del_loc"].ToString();
                        MST_LOC = bsObj.CHNLSVC.Sales.GetLocationCode(row["SAH_COM"].ToString(), row["sah_del_loc"].ToString());
                    }
                    else
                    {
                        dr["sah_del_loc"] = string.Empty;
                    }
                    sar_sub_tp = bsObj.CHNLSVC.Sales.GetSalesSubTypeTable(row["SAH_INV_TP"].ToString(), row["SAH_INV_SUB_TP"].ToString());

                    sat_hdr1.Rows.Add(dr);
                };



                dr = sat_itm.NewRow();
                dr["SAD_WARR_REMARKS"] = row["SAD_WARR_REMARKS"].ToString();
                dr["SAD_UNIT_RT"] = Convert.ToDecimal(row["SAD_UNIT_RT"].ToString());
                dr["SAD_TOT_AMT"] = Convert.ToDecimal(row["SAD_TOT_AMT"].ToString());
                dr["SAD_QTY"] = Convert.ToDecimal(row["SAD_QTY"].ToString());
                dr["SAD_ITM_TAX_AMT"] = Convert.ToDecimal(row["SAD_ITM_TAX_AMT"].ToString());
                dr["SAD_ITM_CD"] = row["SAD_ITM_CD"].ToString();
                dr["SAD_DISC_RT"] = Convert.ToDecimal(row["SAD_DISC_RT"].ToString());
                dr["SAD_DISC_AMT"] = Convert.ToDecimal(row["SAD_DISC_AMT"].ToString());
                dr["SAD_SEQ_NO"] = Convert.ToDecimal(row["SAD_SEQ_NO"].ToString());
                dr["SAD_ITM_LINE"] = Convert.ToInt16(row["SAD_ITM_LINE"].ToString());
                dr["SAD_WARR_PERIOD"] = Convert.ToInt16(row["SAD_WARR_PERIOD"].ToString());
                sat_itm.Rows.Add(dr);

                dr = mst_item.NewRow();
                dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                dr["MI_CD"] = row["MI_CD"].ToString();

                mst_item.Rows.Add(dr);

                if (index == 0)
                {
                    if (accountDetails.Rows.Count > 0)
                    {
                        tblComDate = bsObj.CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), accNo);
                    }
                    else
                    {
                        tblComDate = bsObj.CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), invNo);
                    }

                    dr = mst_profit_center.NewRow();
                    dr["MPC_ADD_1"] = row["MPC_ADD_1"].ToString();
                    dr["MPC_ADD_2"] = row["MPC_ADD_2"].ToString();
                    dr["MPC_CHNL"] = row["MPC_CHNL"].ToString();
                    dr["MPC_DESC"] = row["MPC_DESC"].ToString();
                    dr["MPC_COM"] = row["MPC_COM"].ToString();
                    dr["MPC_CD"] = row["MPC_CD"].ToString();
                    dr["MPC_TEL"] = row["MPC_TEL"].ToString();
                    dr["MPC_FAX"] = row["MPC_FAX"].ToString();
                    dr["MPC_EMAIL"] = row["MPC_EMAIL"].ToString();
                    dr["MPC_OTH_REF"] = row["MPC_OTH_REF"].ToString();
                    dr["MPC_ORD_ALPBT"] = Convert.ToInt16(row["MPC_ORD_ALPBT"].ToString());

                    mst_profit_center.Rows.Add(dr);


                    dr = mst_com.NewRow();
                    dr["MC_TAX1"] = row["MC_TAX1"].ToString();
                    dr["MC_TAX2"] = row["MC_TAX2"].ToString();
                    dr["MC_CD"] = row["MC_CD"].ToString();
                    dr["MC_DESC"] = row["MC_DESC"].ToString();
                    dr["MC_IT_POWERED"] = row["MC_IT_POWERED"].ToString();
                    dr["MC_TAX3"] = row["MC_TAX3"].ToString();
                    dr["MC_ANAL18"] = row["MC_ANAL18"].ToString();
                    dr["MC_ANAL19"] = row["MC_ANAL19"].ToString();
                    mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };

            }

            DataTable deliveredSerials = bsObj.CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
            DataTable int_hdr = new DataTable();
            DataTable int_ser = new DataTable();



            int_hdr.Columns.Add("ITH_SEQ_NO", typeof(decimal));
            int_hdr.Columns.Add("ITH_COM", typeof(string));
            int_hdr.Columns.Add("ITH_OTH_DOCNO", typeof(string));
            int_hdr.Columns.Add("ITH_DOC_NO", typeof(string));

            int_ser.Columns.Add("ITS_SEQ_NO", typeof(string));
            int_ser.Columns.Add("ITS_ITM_LINE", typeof(Int16));
            int_ser.Columns.Add("ITS_BATCH_LINE", typeof(Int16));
            int_ser.Columns.Add("ITS_SER_LINE", typeof(Int16));
            int_ser.Columns.Add("ITS_DOC_NO", typeof(string));
            int_ser.Columns.Add("ITS_SER_ID", typeof(string));
            int_ser.Columns.Add("ITS_SER_1", typeof(string));
            int_ser.Columns.Add("ITS_SER_2", typeof(string));
            int_ser.Columns.Add("ITS_WARR_NO", typeof(string));
            int_ser.Columns.Add("ITS_WARR_PERIOD", typeof(decimal));
            int_ser.Columns.Add("ITS_ITM_CD", typeof(string));


            foreach (DataRow row in deliveredSerials.Rows)
            {
                dr = int_hdr.NewRow();
                dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                dr["ITH_COM"] = row["ITH_COM"].ToString();
                dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                int_hdr.Rows.Add(dr);

                if (row["ITS_SER_1"].ToString() != "N/A")
                {
                    dr = int_ser.NewRow();
                    dr["ITS_SEQ_NO"] = row["ITS_SEQ_NO"].ToString();
                    dr["ITS_ITM_LINE"] = Convert.ToInt16(row["ITS_ITM_LINE"].ToString());
                    dr["ITS_BATCH_LINE"] = Convert.ToInt16(row["ITS_BATCH_LINE"].ToString());
                    dr["ITS_SER_LINE"] = row["ITS_SER_LINE"].ToString();
                    dr["ITS_DOC_NO"] = row["ITS_DOC_NO"].ToString();
                    dr["ITS_SER_ID"] = row["ITS_SER_ID"].ToString();
                    dr["ITS_SER_1"] = row["ITS_SER_1"].ToString();
                    dr["ITS_SER_2"] = row["ITS_SER_2"].ToString();
                    dr["ITS_WARR_NO"] = row["ITS_WARR_NO"].ToString();
                    dr["ITS_WARR_PERIOD"] = Convert.ToDecimal(row["ITS_WARR_PERIOD"].ToString());
                    int_ser.Rows.Add(dr);
                };

                DataTable MST_ITM1 = new DataTable();
                MST_ITM1 = bsObj.CHNLSVC.Sales.GetItemCode(row["ITH_COM"].ToString(), row["ITS_ITM_CD"].ToString());
                foreach (DataRow row1 in MST_ITM1.Rows)
                {
                    dr = mst_item1.NewRow();
                    dr["MI_MODEL"] = row1["MI_MODEL"].ToString();
                    dr["MI_SHORTDESC"] = row1["MI_SHORTDESC"].ToString();
                    dr["MI_CD"] = row1["MI_CD"].ToString();
                    dr["MI_IS_SER1"] = row1["MI_IS_SER1"].ToString();
                    dr["MI_IS_SER2"] = row1["MI_IS_SER2"].ToString();
                    dr["MI_WARR"] = row1["MI_WARR"].ToString();
                    mst_item1.Rows.Add(dr);
                }

            }

            DataTable receiptDetails = bsObj.CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);

            DataTable sat_receiptCQ = new DataTable();
            DataTable sat_receiptitmCQ = new DataTable();
            sat_receiptCQ.Columns.Add("SAR_SEQ_NO", typeof(string));
            sat_receiptCQ.Columns.Add("SAR_COM_CD", typeof(string));
            sat_receiptCQ.Columns.Add("SAR_RECEIPT_TYPE", typeof(string));
            sat_receiptCQ.Columns.Add("SAR_RECEIPT_NO", typeof(string));


            sat_receiptitmCQ.Columns.Add("SARD_SEQ_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_RECEIPT_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_INV_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_PAY_TP", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_REF_NO", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_CHQ_BANK_CD", typeof(string));//Lakshika 2016/08/30 
            sat_receiptitmCQ.Columns.Add("SARD_CHQ_BRANCH", typeof(string));//Lakshika 2016/08/30 
            sat_receiptitmCQ.Columns.Add("SARD_SETTLE_AMT", typeof(decimal));
            sat_receiptitmCQ.Columns.Add("SAPT_DESC", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_RMK", typeof(string));
            sat_receiptitmCQ.Columns.Add("SARD_ANAL_3", typeof(decimal));

            foreach (DataRow row in receiptDetails.Rows)
            {
                if (row["SAR_RECEIPT_TYPE"].ToString() == "DIR")//(row["SARD_PAY_TP"].ToString() == "CHEQUE" && row["SAR_RECEIPT_TYPE"].ToString() == "DIR")
                {

                    dr = sat_receiptCQ.NewRow();
                    dr["SAR_SEQ_NO"] = row["SAR_SEQ_NO"].ToString();
                    dr["SAR_COM_CD"] = row["SAR_COM_CD"].ToString();
                    dr["SAR_RECEIPT_TYPE"] = row["SAR_RECEIPT_TYPE"].ToString();
                    dr["SAR_RECEIPT_NO"] = row["SAR_RECEIPT_NO"].ToString();
                    sat_receiptCQ.Rows.Add(dr);

                    dr = sat_receiptitmCQ.NewRow();
                    dr["SARD_SEQ_NO"] = row["SARD_SEQ_NO"].ToString();
                    dr["SARD_RECEIPT_NO"] = row["SARD_RECEIPT_NO"].ToString();
                    dr["SARD_INV_NO"] = row["SARD_INV_NO"].ToString();
                    dr["SARD_PAY_TP"] = row["SARD_PAY_TP"].ToString();
                    dr["SARD_REF_NO"] = row["SARD_REF_NO"].ToString();
                    dr["SARD_CHQ_BANK_CD"] = row["SARD_CHQ_BANK_CD"].ToString();//Lakshika 2016/08/30 
                    dr["SARD_CHQ_BRANCH"] = row["SARD_CHQ_BRANCH"].ToString(); //Lakshika 2016/08/30 
                    dr["SAPT_DESC"] = row["SAPT_DESC"].ToString();
                    dr["SARD_SETTLE_AMT"] = Convert.ToDecimal(row["SARD_SETTLE_AMT"].ToString());
                    dr["SARD_RMK"] = row["SARD_RMK"].ToString();
                    dr["SARD_ANAL_3"] = Convert.ToDecimal(row["SARD_ANAL_3"].ToString());
                    sat_receiptitmCQ.Rows.Add(dr);


                    if (row["SARD_PAY_TP"].ToString() == "CRNOTE")
                    {
                        isCredit = 1;

                    }
                };



            }

            DataTable hpt_shed = bsObj.CHNLSVC.Sales.GetAccountSchedule(invNo);
            DataTable Promo = bsObj.CHNLSVC.Sales.GetPromotionByInvoice(invNo);

            DataTable ref_rep_infor = bsObj.CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
            mst_item = mst_item.DefaultView.ToTable(true);

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


            invfullReport.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            invfullReport.Database.Tables["mst_com"].SetDataSource(mst_com);
            invfullReport.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            invfullReport.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            invfullReport.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            invfullReport.Database.Tables["mst_item"].SetDataSource(mst_item);
            invfullReport.Database.Tables["sec_user"].SetDataSource(sec_user);
            invfullReport.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
            invfullReport.Database.Tables["param"].SetDataSource(param);
            invfullReport.Database.Tables["Promo"].SetDataSource(Promo);





            foreach (object repOp in invfullReport.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptWarranty")
                    {
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }

                    if (_cs.SubreportName == "rptCheque")
                    {
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "rptAccount")
                    {
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                        subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }

                    if (_cs.SubreportName == "tax")
                    {
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "rptComm")
                    {
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "rptWarr")
                    {
                        mst_item1 = mst_item1.DefaultView.ToTable(true);
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                        subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                        subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
                        subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "giftVou")
                    {
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    if (_cs.SubreportName == "loc")
                    {
                        ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();

                    //if (tblComDate.Rows.Count >0) 
                    //{
                    //  if (_cs.SubreportName == "warrComDate")
                    //  {
                    //      ReportDocument subRepDoc = invfullReport.Subreports[_cs.SubreportName];
                    //      subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                    //  }
                    //}

                }
            }




            //this.Text = "Invoice Print";

            //crystalReportViewer1.ReportSource = invfullReport;

            //crystalReportViewer1.RefreshReport();




        }

        public void PendingDeliveryReport(InvReportPara _objRepPara)
        {
            // Sanjeewa 28-03-2013
            DataTable tmp_user_pc = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            string showCost = "N";

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(_objRepPara._GlbReportCompCode, _objRepPara._GlbUserID);

            if (_objRepPara._GlbReportWithCost == 1)
            {
                showCost = "Y";
            }

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetForwardSalesDetails(Convert.ToDateTime(_objRepPara._GlbReportAsAtDate).Date, _objRepPara._GlbUserID, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat4,
                        _objRepPara._GlbReportDoc2, _objRepPara._GlbReportToAge, _objRepPara._GlbReportCompCode, drow["tpl_pc"].ToString(), showCost, _objRepPara._GlbReportOtherLoc, _objRepPara._GlbReportCustomer);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("doctype", typeof(string));
            param.Columns.Add("docsubtype", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("age", typeof(string));
            param.Columns.Add("customer", typeof(string));

            dr = param.NewRow();
            dr["user"] = _objRepPara._GlbUserID;
            dr["heading_1"] = _objRepPara._GlbReportHeading;
            dr["asatdate"] = _objRepPara._GlbReportAsAtDate.Date;
            dr["profitcenter"] = _objRepPara._GlbReportProfit == "" ? "ALL" : _objRepPara._GlbReportProfit;
            dr["doctype"] = _objRepPara._GlbReportDocType == "" ? "ALL" : _objRepPara._GlbReportDocType;
            dr["docsubtype"] = _objRepPara._GlbReportDocSubType == "" ? "ALL" : _objRepPara._GlbReportDocSubType;
            dr["itemcode"] = _objRepPara._GlbReportItemCode == "" ? "ALL" : _objRepPara._GlbReportItemCode;
            dr["brand"] = _objRepPara._GlbReportBrand == "" ? "ALL" : _objRepPara._GlbReportBrand;
            dr["model"] = _objRepPara._GlbReportModel == "" ? "ALL" : _objRepPara._GlbReportModel;
            dr["cat1"] = _objRepPara._GlbReportItemCat1 == "" ? "ALL" : _objRepPara._GlbReportItemCat1;
            dr["cat2"] = _objRepPara._GlbReportItemCat2 == "" ? "ALL" : _objRepPara._GlbReportItemCat2;
            dr["cat3"] = _objRepPara._GlbReportItemCat3 == "" ? "ALL" : _objRepPara._GlbReportItemCat3;
            dr["age"] = _objRepPara._GlbReportDoc2 + ' ' + _objRepPara._GlbReportToAge;
            dr["customer"] = _objRepPara._GlbReportCustomer == "" ? "ALL" : _objRepPara._GlbReportCustomer;

            param.Rows.Add(dr);

            if (_objRepPara._GlbReportType == "N")
            {
                if (_objRepPara._GlbReportWithCost == 1)
                {
                    _Collectiondetail.Database.Tables["PROC_FWD_SALES"].SetDataSource(GLOB_DataTable);
                    _Collectiondetail.Database.Tables["param"].SetDataSource(param);
                }
                else
                {
                    _ForwardSalesrpt1.Database.Tables["PROC_FWD_SALES"].SetDataSource(GLOB_DataTable);
                    _ForwardSalesrpt1.Database.Tables["param"].SetDataSource(param);
                }
            }

            else
            {
                _ForwardSalesrpt2.Database.Tables["PROC_FWD_SALES"].SetDataSource(GLOB_DataTable);
                _ForwardSalesrpt2.Database.Tables["param"].SetDataSource(param);
            }


            //  // Wimal  @ 21/12/2015
            //  DataTable MST_COM = new DataTable();
            //  DataTable Pendingdelivery = new DataTable();
            //  DataTable param = new DataTable();
            //  DataRow dr;
            //  MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            //  param.Columns.Add("user", typeof(string));
            //  param.Columns.Add("heading_1", typeof(string));
            //  param.Columns.Add("period", typeof(string));
            //  param.Columns.Add("company", typeof(string));
            //  param.Columns.Add("company_name", typeof(string));
            //  param.Columns.Add("companies", typeof(string));

            //  dr = param.NewRow();
            //  dr["user"] = BaseCls.GlbUserID;
            //  dr["heading_1"] = BaseCls.GlbReportHeading;
            //  dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            //  dr["company"] = BaseCls.GlbReportComp;
            //  dr["company_name"] = BaseCls.GlbReportCompCode;
            //  dr["companies"] = BaseCls.GlbReportCompanies;
            //  param.Rows.Add(dr);

            //  Pendingdelivery.Clear();
            //  DataTable tmp_user_pc = new DataTable();
            //  DataTable tmp_Table = new DataTable();

            //  tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(_objRepPara._GlbReportCompCode, _objRepPara._GlbUserID);

            //  if (tmp_user_pc.Rows.Count > 0)
            //  {
            //      foreach (DataRow row in tmp_user_pc.Rows)
            //      {
            //          DataTable TMP_SER = new DataTable();
            //          TMP_SER = bsObj.CHNLSVC.MsgPortal.pendingDelivery(_objRepPara._GlbReportAsAtDate.Date, _objRepPara._GlbUserID, _objRepPara._GlbReportBrand, _objRepPara._GlbReportModel, _objRepPara._GlbReportItemCode, _objRepPara._GlbReportItemCat1, _objRepPara._GlbReportItemCat2, _objRepPara._GlbReportItemCat3, "", 0, _objRepPara._GlbReportCompCode, row["tpl_pc"].ToString(), "");                    
            //          Pendingdelivery.Merge(TMP_SER);
            //      }
            //  }

            //  //tmp_Table = bsObj.CHNLSVC.MsgPortal.pendingDelivery(BaseCls.GlbReportAsAtDate, BaseCls.GlbReportUser, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, "", 0, BaseCls.GlbReportComp,"89", "");
            //  //Pendingdelivery.Merge(tmp_Table);
            //  ////    tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbReportComp, BaseCls.GlbUserID);
            //  //if (tmp_user_pc.Rows.Count > 0)
            //  //{
            //  //    foreach (DataRow drow in tmp_user_pc.Rows)
            //  //    {
            //  //        DataTable tmp_Table = new DataTable();
            //  //        tmp_Table = bsObj.CHNLSVC.MsgPortal.pendingDelivery(BaseCls.GlbReportAsAtDate, BaseCls.GlbReportUser, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, "",0, BaseCls.GlbReportComp, drow["tpl_pc"].ToString(),"");
            //  //        Pendingdelivery.Merge(tmp_Table);
            //  //    }
            //  //}          

            //// _fwSales.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            //  _fwSales.Database.Tables["temp_pendingdelivery"].SetDataSource(Pendingdelivery);
            // //_fwSales.Database.Tables["param"].SetDataSource(param);

        }

        public void PendingSaelsReport()
        {
            // Wimal  @ 21/12/2015
            DataTable MST_COM = new DataTable();
            DataTable Pendingdelivery = new DataTable();
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
            dr["user"] = Session["UserID"].ToString();
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = BaseCls.GlbReportComp;
            dr["company_name"] = BaseCls.GlbReportCompCode;
            dr["companies"] = BaseCls.GlbReportCompanies;
            param.Rows.Add(dr);

            Pendingdelivery.Clear();
            DataTable tmp_user_pc = new DataTable();
            DataTable tmp_Table = new DataTable();
            tmp_Table = bsObj.CHNLSVC.MsgPortal.pendingDelivery(BaseCls.GlbReportAsAtDate, BaseCls.GlbReportUser, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, "", 0, BaseCls.GlbReportComp, "", "");
            Pendingdelivery.Merge(tmp_Table);
            //    tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbReportComp, BaseCls.GlbUserID);
            //if (tmp_user_pc.Rows.Count > 0)
            //{
            //    foreach (DataRow drow in tmp_user_pc.Rows)
            //    {
            //        DataTable tmp_Table = new DataTable();
            //        tmp_Table = bsObj.CHNLSVC.MsgPortal.pendingDelivery(BaseCls.GlbReportAsAtDate, BaseCls.GlbReportUser, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, "",0, BaseCls.GlbReportComp, drow["tpl_pc"].ToString(),"");
            //        Pendingdelivery.Merge(tmp_Table);
            //    }
            //}          

            _pndSales.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _pndSales.Database.Tables["temp_pendingdelivery"].SetDataSource(Pendingdelivery);
            _pndSales.Database.Tables["param"].SetDataSource(param);

        }


        public void so_status()
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
            dr["user"] = Session["UserID"].ToString();
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["company"] = BaseCls.GlbReportComp;
            dr["company_name"] = BaseCls.GlbReportCompCode;
            dr["companies"] = BaseCls.GlbReportCompanies;

            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["doc_no"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["doc_type"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            param.Rows.Add(dr);



            purchaseOrderSumm.Clear();
            DataTable tmp_Table = new DataTable();
            //tmp_Table = bsObj.CHNLSVC.MsgPortal.GetStockBalanceCurrent_SCM(BaseCls.GlbUserID, BaseCls.GlbReportChannel, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemStatus, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportWithCost, BaseCls.GlbReportWithSerial, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportWithRCC, BaseCls.GlbReportWithJob, BaseCls.GlbReportWithGIT);
            tmp_Table = bsObj.CHNLSVC.MsgPortal.getsostatus(BaseCls.GlbReportComp, "", BaseCls.GlbReportSupplier, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportDocType, BaseCls.GlbReportDoc);
            purchaseOrderSumm.Merge(tmp_Table);
            //DataTable tmp_user_pc = new DataTable();
            //tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbReportComp, BaseCls.GlbUserID);
            //if (tmp_user_pc.Rows.Count > 0)
            //{
            //    foreach (DataRow drow in tmp_user_pc.Rows)
            //    {
            //        DataTable tmp_Table = new DataTable();
            //        //tmp_Table = bsObj.CHNLSVC.MsgPortal.GetStockBalanceCurrent_SCM(BaseCls.GlbUserID, BaseCls.GlbReportChannel, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemStatus, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportWithCost, BaseCls.GlbReportWithSerial, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportWithRCC, BaseCls.GlbReportWithJob, BaseCls.GlbReportWithGIT);
            //        tmp_Table = bsObj.CHNLSVC.MsgPortal.getsostatus(BaseCls.GlbReportComp, drow["tpl_pc"].ToString(), BaseCls.GlbReportSupplier, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportDocType, BaseCls.GlbReportDoc);
            //        purchaseOrderSumm.Merge(tmp_Table);
            //    }
            //}


            _sostatus.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _sostatus.Database.Tables["dsSaleOrderStatus"].SetDataSource(purchaseOrderSumm);
            _sostatus.Database.Tables["param"].SetDataSource(param);
        }
        //subodana

        public void get_Sales_Orders(string OrderNo, string COM, string pc, string loc)
        {
            List<SalesOrderItems> sales_order = new List<SalesOrderItems>();
            DataTable param = new DataTable();
            DataTable mstitems = new DataTable();
            DataTable emp = new DataTable();
            var descrip = new MasterItem();
            DataTable param2 = new DataTable();
            DataRow dr;
            dr = param2.NewRow();
            param2.Columns.Add("Channel", typeof(string));
            Service_Chanal_parameter chanal = bsObj.CHNLSVC.General.GetChannelParamers(COM, loc);
            if (chanal != null)
            {
                string channel = chanal.SP_SERCHNL.ToString();
                dr["Channel"] = channel;
                param2.Rows.Add(dr);
            }
            else
            {
                string channel = "";
                dr["Channel"] = channel;
                param2.Rows.Add(dr);
            }

            sales_order.Clear();
            param = bsObj.CHNLSVC.Sales.SearchSalesOrdData(OrderNo, COM, pc);
            sales_order = bsObj.CHNLSVC.Sales.SearchSalesOrdItems(OrderNo);
            if (param.Rows[0]["soh_tax_inv"].ToString() == "0")
            {
                foreach (var soitm in sales_order)
                {
                    soitm.SOI_UNIT_AMT = soitm.SOI_UNIT_AMT + soitm.SOI_ITM_TAX_AMT;
                    soitm.SOI_UNIT_RT = soitm.SOI_UNIT_RT + (soitm.SOI_ITM_TAX_AMT / soitm.SOI_QTY);
                    soitm.SOI_ITM_TAX_AMT = 0;
                }


            }

            mstitems = bsObj.CHNLSVC.Sales.GetItemCodeDes(OrderNo);
            int k = 0;

            if (param.Rows[0]["soh_tax_inv"].ToString() == "0")
            {
                foreach (var itm2 in mstitems.Rows)
                {
                    mstitems.Rows[k]["SOI_UNIT_AMT"] = Convert.ToDecimal(mstitems.Rows[k]["SOI_UNIT_AMT"]) + Convert.ToDecimal(mstitems.Rows[k]["soi_itm_tax_amt"]);
                    mstitems.Rows[k]["SOI_UNIT_RT"] = Convert.ToDecimal(mstitems.Rows[k]["SOI_UNIT_RT"]) + (Convert.ToDecimal(mstitems.Rows[k]["soi_itm_tax_amt"]) / Convert.ToDecimal(mstitems.Rows[k]["SOI_QTY"]));
                    mstitems.Rows[k]["soi_itm_tax_amt"] = 0;
                    k++;

                }

            }


            string code = param.Rows[0].Field<string>(17);
            emp = bsObj.CHNLSVC.Sales.GetEmployee(COM, code);
            List<MST_PROF_CEN_SEARCH_HEAD> pcenterst = bsObj.CHNLSVC.CommonSearch.getAllProfitCenters(COM, "1", "100000", "Code", "").ToList();
            var st = pcenterst.Where(a => a.MPC_CD == pc).Max(z => z.mpc_ord_alpbt);
            int stnew = Convert.ToInt32(st);
            if (stnew == 1)
            {
                DataView dv = mstitems.DefaultView;
                dv.Sort = "soi_itm_cd";
                DataTable sortedDT = dv.ToTable();
                List<SalesOrderItems> sales_ordernew = sales_order.OrderBy(x => x.SOI_ITM_CD).ToList();

                _sales_order.Database.Tables["so_details"].SetDataSource(sortedDT);
            }
            else
            {
                _sales_order.Database.Tables["so_details"].SetDataSource(mstitems);
            }
            _sales_order.Database.Tables["so_hdr"].SetDataSource(param);
            _sales_order.Database.Tables["emp_data"].SetDataSource(emp);
            _sales_order.Database.Tables["param"].SetDataSource(param2);

        }

        public void ProfitCenterDetails(string com, string location, string pclist, string user)
        {
            DataTable Param = new DataTable();
            DataRow dr;
            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("User", typeof(string));
            dr = Param.NewRow();
            dr["Company"] = com;
            dr["Location"] = location;
            dr["User"] = user;
            Param.Rows.Add(dr);


            DataTable data = bsObj.CHNLSVC.Sales.GetProfitCenterDetails(com, pclist);
            _pcdetails.Database.Tables["PCDetails"].SetDataSource(data);
            _pcdetails.Database.Tables["Param"].SetDataSource(Param);
        }

        public void GetSRNdata(string user, string com, string invno, string location)
        {

            DataTable Param = new DataTable();
            DataRow dr;

            Param.Columns.Add("Location", typeof(string));
            Param.Columns.Add("User", typeof(string));
            dr = Param.NewRow();

            dr["Location"] = location;
            dr["User"] = user;
            Param.Rows.Add(dr);
            //get revers data
            DataTable SRNData = bsObj.CHNLSVC.Sales.GetReversalInvoiceData(com, invno);
            if (Session["UserCompanyCode"].ToString() == "AAL")
            {
                _SRNreport_AAL.Database.Tables["SRNData"].SetDataSource(SRNData);
                _SRNreport_AAL.Database.Tables["Param"].SetDataSource(Param);
            }
            if (Session["UserCompanyCode"].ToString() == "ABE")
            {
                _SRNreport_ABE.Database.Tables["SRNData"].SetDataSource(SRNData);
                _SRNreport_ABE.Database.Tables["Param"].SetDataSource(Param);
            }
            else
            {
                _SRNreport.Database.Tables["SRNData"].SetDataSource(SRNData);
                _SRNreport.Database.Tables["Param"].SetDataSource(Param);
            }
        }
        public void SRNDeatils(string com, DateTime _from, DateTime _to, string _sbu, string _pc, string _cus, string _itm,
            string _brand, string _model, string _CompanyDes, string _user)
        {// Rukshan 05-07-2016
            DataTable MST_COM = new DataTable();

            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(com);

            DataTable Param = new DataTable();
            DataRow dr;
            string Itemcode = string.Empty;
            Param.Columns.Add("From", typeof(string));
            Param.Columns.Add("To", typeof(string));
            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("user", typeof(string));
            Param.Columns.Add("ItemCode", typeof(string));
            dr = Param.NewRow();
            dr["From"] = _from;
            dr["To"] = _to;
            dr["Company"] = _CompanyDes;
            dr["user"] = _user;
            if (string.IsNullOrEmpty(_itm))
            {
                Itemcode = "ALL";
            }
            else
            {
                Itemcode = _itm;
            }

            dr["ItemCode"] = Itemcode;
            Param.Rows.Add(dr);
            DataTable result = bsObj.CHNLSVC.MsgPortal.GET_SALESREVERSALDETAILS_REP(com, _from, _to, _sbu, _pc, _cus, _itm, _brand, _model);

            _SalesReversalDetails.Database.Tables["SRNDetails"].SetDataSource(result);
            _SalesReversalDetails.Database.Tables["Param"].SetDataSource(Param);
            _SalesReversalDetails.Database.Tables["MST_COM"].SetDataSource(MST_COM);
        }

        public void GetDiscountReportDetails(string _com, string _pc, string _itemCode, string _cat1, string _cat2, string _cat3,
            string _brand, string _model, string _customer, string _executive, DateTime _fromDate, DateTime _toDate, int _fromDisc, int _toDisc, string _user, string comDesc)
        {
            DataTable Param = new DataTable();
            DataRow dr;
            Param.Columns.Add("com", typeof(string));
            Param.Columns.Add("pc", typeof(string));
            Param.Columns.Add("user", typeof(string));
            Param.Columns.Add("from_date", typeof(DateTime));
            Param.Columns.Add("to_date", typeof(DateTime));
            Param.Columns.Add("itemcode", typeof(string));
            Param.Columns.Add("cat1", typeof(string));
            Param.Columns.Add("cat2", typeof(string));
            Param.Columns.Add("cat3", typeof(string));
            Param.Columns.Add("brand", typeof(string));
            Param.Columns.Add("model", typeof(string));
            Param.Columns.Add("customer", typeof(string));
            Param.Columns.Add("discountgt", typeof(string));
            Param.Columns.Add("discountlt", typeof(string));
            Param.Columns.Add("executive", typeof(string));

            dr = Param.NewRow();
            dr["com"] = comDesc;
            //dr["pc"] = _pc;
            dr["user"] = _user;
            dr["from_date"] = _fromDate;
            dr["to_date"] = _toDate;
            dr["itemcode"] = _itemCode == "" ? "ALL" : _itemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = _cat1 == "" ? "ALL" : _cat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = _cat2 == "" ? "ALL" : _cat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = _cat3 == "" ? "ALL" : _cat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = _brand == "" ? "ALL" : _brand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = _model == "" ? "ALL" : _model.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["customer"] = _customer == "" ? "ALL" : _customer.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["discountgt"] = _fromDisc == 0 ? "0" : _fromDisc.ToString();
            dr["discountlt"] = _toDisc == 0 ? "100" : _toDisc.ToString();
            dr["executive"] = _executive == "" ? "ALL" : _executive.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            Param.Rows.Add(dr);

            DataTable data = bsObj.CHNLSVC.Sales.GetDiscountReportDetails(_com, _pc, _itemCode, _cat1, _cat2, _cat3,
             _brand, _model, _customer, _executive, _fromDate, _toDate, _fromDisc, _toDisc, _user);

            _discountReport.Database.Tables["DISCOUNT_DETAILS"].SetDataSource(data);
            _discountReport.Database.Tables["param"].SetDataSource(Param);
        }

        public void GP_AnalysisData(List<DataTable> dtResult, InvReportPara rptParam)
        {
            DataTable param = new DataTable();
            string customer = rptParam._GlbReportCustomer == null ? "" : rptParam._GlbReportCustomer;
            string executive = rptParam._GlbReportExecutive == null ? "" : rptParam._GlbReportExecutive;

            DataRow dr;

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("com", typeof(string));
            param.Columns.Add("customer", typeof(string));
            param.Columns.Add("executive", typeof(string));

            dr = param.NewRow();
            dr["user"] = rptParam._GlbUserID;
            dr["fromdate"] = rptParam._GlbReportFromDate;
            dr["todate"] = rptParam._GlbReportToDate;
            dr["com"] = rptParam._GlbReportCompName;
            dr["customer"] = customer == "" ? "ALL" : customer.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["executive"] = executive == "" ? "ALL" : executive.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            param.Rows.Add(dr);

            _gpAnalysisRpt.Database.Tables["GP_ANALYSIS"].SetDataSource(dtResult[0]);
            //  _gpAnalysisRpt.Database.Tables["GP_ANALYSIS_BRAND"].SetDataSource(dtResult[1]);
            _gpAnalysisRpt.Database.Tables["param"].SetDataSource(param);

            foreach (object repOp in _gpAnalysisRpt.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "gp_analysis_brand_sub")
                    {
                        ReportDocument subRepDoc = _gpAnalysisRpt.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["GP_ANALYSIS_BRAND"].SetDataSource(dtResult[1]);
                        subRepDoc.Close();
                        subRepDoc.Dispose();
                    }
                    _cs.Dispose();
                }
            }

        }
        public void getValuationDetails(InvReportPara _objRepoPara)
        {
            DataTable Param = new DataTable();
            DataRow dr;
            Param.Columns.Add("user", typeof(string));
            Param.Columns.Add("header", typeof(string));
            Param.Columns.Add("loc", typeof(string));
            Param.Columns.Add("period", typeof(string));

            dr = Param.NewRow();
            dr["user"] = _objRepoPara._GlbUserID;
            dr["header"] = _objRepoPara._GlbReportHeading;
            dr["loc"] = "FROM " + _objRepoPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepoPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["period"] = _objRepoPara._GlbReportProfit == "" ? "ALL" : _objRepoPara._GlbReportProfit;
            Param.Rows.Add(dr);

            DataTable data = bsObj.CHNLSVC.MsgPortal.getValuationDetails_Cr(_objRepoPara._GlbReportFromDate, _objRepoPara._GlbReportToDate, _objRepoPara._GlbReportItmClasif,
                            _objRepoPara._GlbReportItemCode, _objRepoPara._GlbReportBrand, _objRepoPara._GlbReportModel, _objRepoPara._GlbReportItemCat1, _objRepoPara._GlbReportItemCat2, _objRepoPara._GlbReportItemCat3, _objRepoPara._GlbReportItemCat4, _objRepoPara._GlbReportItemCat5,
                            _objRepoPara._GlbReportItemStatus, "", "", _objRepoPara._GlbReportCompCode, _objRepoPara._GlbUserID);

            _valdtl.Database.Tables["VAL_DTL"].SetDataSource(data);
            _valdtl.Database.Tables["param"].SetDataSource(Param);
        }

        public void executiveWiseSalesReport(InvReportPara _objRepoPara)
        {//Wimal 27/10/2016
            DataTable param = new DataTable();
            DataRow dr;
            tmp_user_pc = new DataTable();
            GLOB_DataTable = new DataTable();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB_AllCom(_objRepoPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetDeliveredSalesDetails(Convert.ToDateTime(_objRepoPara._GlbReportFromDate).Date,
                        Convert.ToDateTime(_objRepoPara._GlbReportToDate).Date,
                        _objRepoPara._GlbReportCustomer,
                        _objRepoPara._GlbReportExecutive,
                        _objRepoPara._GlbReportDocType,
                        _objRepoPara._GlbReportItemCode,
                        _objRepoPara._GlbReportBrand,
                        _objRepoPara._GlbReportModel,
                        _objRepoPara._GlbReportItemCat1,
                        _objRepoPara._GlbReportItemCat2,
                        _objRepoPara._GlbReportItemCat3,
                        _objRepoPara._GlbReportProfit,
                        _objRepoPara._GlbUserID,
                        _objRepoPara._GlbReportType,
                        _objRepoPara._GlbReportItemStatus,
                        _objRepoPara._GlbReportDoc,
                        drow["tpl_pc"].ToString(),
                        drow["tpl_com"].ToString(),
                        _objRepoPara._GlbReportPromotor,
                        _objRepoPara._GlbReportIsFreeIssue,
                        2,//Company Currency
                       2, "", ""
                        );
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("PARA_USER", typeof(string));
            param.Columns.Add("PARA_PERIOD", typeof(string));
            param.Columns.Add("PARA_PCENTER", typeof(string));
            param.Columns.Add("PARA_CUST", typeof(string));
            param.Columns.Add("PARA_EXEC", typeof(string));
            param.Columns.Add("PARA_DOCTYPE", typeof(string));
            param.Columns.Add("PARA_ITCODE", typeof(string));
            param.Columns.Add("PARA_BRAND", typeof(string));
            param.Columns.Add("PARA_MODEL", typeof(string));
            param.Columns.Add("PARA_CAT1", typeof(string));
            param.Columns.Add("PARA_CAT2", typeof(string));
            param.Columns.Add("PARA_CAT3", typeof(string));
            param.Columns.Add("PARA_CAT4", typeof(string));
            param.Columns.Add("PARA_CAT5", typeof(string));
            param.Columns.Add("PARA_STKTYPE", typeof(string));
            param.Columns.Add("PARA_INVNO", typeof(string));
            param.Columns.Add("PARA_PROMOTOR", typeof(string));
            param.Columns.Add("PARA_HEADING", typeof(string));
            param.Columns.Add("PARA_FREE_ISS_TP", typeof(int));

            param.Columns.Add("GRP_PCENTER", typeof(int));
            param.Columns.Add("GRP_CUST", typeof(int));
            param.Columns.Add("GRP_EXEC", typeof(int));
            param.Columns.Add("GRP_DOCTYPE", typeof(int));
            param.Columns.Add("GRP_ITCODE", typeof(int));
            param.Columns.Add("GRP_BRAND", typeof(int));
            param.Columns.Add("GRP_MODEL", typeof(int));
            param.Columns.Add("GRP_CAT1", typeof(int));
            param.Columns.Add("GRP_CAT2", typeof(int));
            param.Columns.Add("GRP_CAT3", typeof(int));
            param.Columns.Add("GRP_CAT4", typeof(int));
            param.Columns.Add("GRP_CAT5", typeof(int));
            param.Columns.Add("GRP_STKTYPE", typeof(int));
            param.Columns.Add("GRP_INVNO", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP_CAT", typeof(string));
            param.Columns.Add("GRP_PRMOTOR", typeof(int));
            param.Columns.Add("GRP_DLOC", typeof(int));
            param.Columns.Add("GRP_JOBNO", typeof(int));

            dr = param.NewRow();
            dr["PARA_USER"] = _objRepoPara._GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + _objRepoPara._GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + _objRepoPara._GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = _objRepoPara._GlbReportProfit == "" ? "ALL" : _objRepoPara._GlbReportProfit;
            dr["PARA_CUST"] = _objRepoPara._GlbReportCustomer == "" ? "ALL" : _objRepoPara._GlbReportCustomer;
            dr["PARA_EXEC"] = _objRepoPara._GlbReportExecutive == "" ? "ALL" : _objRepoPara._GlbReportExecutive;
            dr["PARA_DOCTYPE"] = _objRepoPara._GlbReportDocType == "" ? "ALL" : _objRepoPara._GlbReportDocType;
            dr["PARA_ITCODE"] = _objRepoPara._GlbReportItemCode == "" ? "ALL" : _objRepoPara._GlbReportItemCode;
            dr["PARA_BRAND"] = _objRepoPara._GlbReportBrand == "" ? "ALL" : _objRepoPara._GlbReportBrand;
            dr["PARA_MODEL"] = _objRepoPara._GlbReportModel == "" ? "ALL" : _objRepoPara._GlbReportModel;
            dr["PARA_CAT1"] = _objRepoPara._GlbReportItemCat1 == "" ? "ALL" : _objRepoPara._GlbReportItemCat1;
            dr["PARA_CAT2"] = _objRepoPara._GlbReportItemCat2 == "" ? "ALL" : _objRepoPara._GlbReportItemCat2;
            dr["PARA_CAT3"] = _objRepoPara._GlbReportItemCat3 == "" ? "ALL" : _objRepoPara._GlbReportItemCat3;
            dr["PARA_CAT4"] = _objRepoPara._GlbReportItemCat4 == "" ? "ALL" : _objRepoPara._GlbReportItemCat4;
            dr["PARA_CAT5"] = _objRepoPara._GlbReportItemCat5 == "" ? "ALL" : _objRepoPara._GlbReportItemCat5;
            dr["PARA_STKTYPE"] = _objRepoPara._GlbReportItemStatus == "" ? "ALL" : _objRepoPara._GlbReportItemStatus;
            dr["PARA_INVNO"] = _objRepoPara._GlbReportDoc == "" ? "ALL" : _objRepoPara._GlbReportDoc;
            dr["PARA_PROMOTOR"] = _objRepoPara._GlbReportPromotor == "" ? "ALL" : _objRepoPara._GlbReportPromotor;
            dr["PARA_HEADING"] = _objRepoPara._GlbReportHeading;
            dr["PARA_FREE_ISS_TP"] = _objRepoPara._GlbReportIsFreeIssue;

            dr["GRP_PCENTER"] = _objRepoPara._GlbReportGroupPC;
            dr["GRP_DOCTYPE"] = _objRepoPara._GlbReportGroupDocTp;
            dr["GRP_CUST"] = _objRepoPara._GlbReportGroupCust;
            dr["GRP_EXEC"] = _objRepoPara._GlbReportGroupExec;
            dr["GRP_ITCODE"] = _objRepoPara._GlbReportGroupItem;
            dr["GRP_BRAND"] = _objRepoPara._GlbReportGroupBrand;
            dr["GRP_MODEL"] = _objRepoPara._GlbReportGroupModel;
            dr["GRP_CAT1"] = _objRepoPara._GlbReportGroupCat1;
            dr["GRP_CAT2"] = _objRepoPara._GlbReportGroupCat2;
            dr["GRP_CAT3"] = _objRepoPara._GlbReportGroupCat3;
            dr["GRP_CAT4"] = _objRepoPara._GlbReportGroupCat4;
            dr["GRP_CAT5"] = _objRepoPara._GlbReportGroupCat5;
            dr["GRP_STKTYPE"] = _objRepoPara._GlbReportGroupStockTp;
            dr["GRP_INVNO"] = _objRepoPara._GlbReportGroupDocNo;
            dr["GRP_LAST_GROUP"] = _objRepoPara._GlbReportGroupLastGroup;
            dr["GRP_LAST_GROUP_CAT"] = _objRepoPara._GlbReportGroupLastGroupCat;
            dr["GRP_PRMOTOR"] = _objRepoPara._GlbReportGroupPromotor;
            dr["GRP_DLOC"] = _objRepoPara._GlbReportGroupDLoc;
            dr["GRP_JOBNO"] = _objRepoPara._GlbReportGroupJobNo;

            param.Rows.Add(dr);


            if (_objRepoPara._GlbReportName == "excecutivewisesales.rpt")
            {
                _exectSales.Database.Tables["GLB_REP_SALES"].SetDataSource(GLOB_DataTable);
                _exectSales.Database.Tables["REP_PARA"].SetDataSource(param);
            };

            if (GLOB_DataTable.Rows.Count > 0)
            {
                foreach (DataRow drow1 in GLOB_DataTable.Rows)
                {
                    WriteErrLog(drow1["pc_code"].ToString() + " Report : ExecutiveWiseSalesReport", _objRepoPara._GlbUserID);
                }
            }
            //else if (Session["GlbReportName"] == "DeliveredSalesReport_withCust.rpt")
            //{
            //    _delSalesrptCust.Database.Tables["GLB_REP_SALES"].SetDataSource(GLOB_DataTable);
            //    _delSalesrptCust.Database.Tables["REP_PARA"].SetDataSource(param);
            //}
            //else
            //{
            //    _delSalesrptItem.Database.Tables["GLB_REP_SALES"].SetDataSource(GLOB_DataTable);
            //    _delSalesrptItem.Database.Tables["REP_PARA"].SetDataSource(param);
            //};

        }

        public void CustomerEntry(string com, DateTime _from, DateTime _to, string _CompanyDes, string _user)
        {// Rukshan 05-07-2016
            DataTable MST_COM = new DataTable();

            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(com);

            DataTable Param = new DataTable();
            DataRow dr;
            string Itemcode = string.Empty;
            Param.Columns.Add("From", typeof(string));
            Param.Columns.Add("To", typeof(string));
            Param.Columns.Add("Company", typeof(string));
            Param.Columns.Add("user", typeof(string));
            Param.Columns.Add("ItemCode", typeof(string));
            dr = Param.NewRow();
            dr["From"] = _from;
            dr["To"] = _to;
            dr["Company"] = _CompanyDes;
            dr["user"] = _user;
            //if (string.IsNullOrEmpty(_itm))
            //{
            //    Itemcode = "ALL";
            //}
            //else
            //{
            //    Itemcode = _itm;
            //}

            dr["ItemCode"] = Itemcode;
            Param.Rows.Add(dr);
            DataTable result = bsObj.CHNLSVC.MsgPortal.GETCUSTOMERENTRY(com, _from, _to);

            _CustomerEntryReport.Database.Tables["CUSTOMERENTRY"].SetDataSource(result);
            _CustomerEntryReport.Database.Tables["Param"].SetDataSource(Param);
            _CustomerEntryReport.Database.Tables["MST_COM"].SetDataSource(MST_COM);
        }

        public void Get_Sellout_Report(DateTime _fromdate, DateTime _todate, string _user_id, string _Brand, string _Model, string _Itemcode, string _Itemcat1, string _Itemcat2, string _Itemcat3, string _Promotor, string _Customer, string _SalesExc)
        {// Wimal 16/12/2016
            DataTable MST_COM = new DataTable();

            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode("ABL");

            DataTable Param = new DataTable();
            DataRow dr;
            string Itemcode = string.Empty;
            //Param.Columns.Add("From", typeof(string));
            //Param.Columns.Add("To", typeof(string));
            //Param.Columns.Add("Company", typeof(string));
            //Param.Columns.Add("user", typeof(string));
            //Param.Columns.Add("ItemCode", typeof(string));
            //dr = Param.NewRow();
            //dr["From"] = _fromdate;
            //dr["To"] = _todate;
            ////dr["Company"] = _CompanyDes;
            //dr["user"] = _user_id;
            ////if (string.IsNullOrEmpty(_itm))
            ////{
            ////    Itemcode = "ALL";
            ////}
            ////else
            ////{
            ////    Itemcode = _itm;
            ////}
            //dr["ItemCode"] = Itemcode;

            Param.Columns.Add("PARA_USER", typeof(string));
            Param.Columns.Add("PARA_PERIOD", typeof(string));
            Param.Columns.Add("PARA_PCENTER", typeof(string));
            Param.Columns.Add("PARA_CUST", typeof(string));
            Param.Columns.Add("PARA_EXEC", typeof(string));
            Param.Columns.Add("PARA_DOCTYPE", typeof(string));
            Param.Columns.Add("PARA_ITCODE", typeof(string));
            Param.Columns.Add("PARA_BRAND", typeof(string));
            Param.Columns.Add("PARA_MODEL", typeof(string));
            Param.Columns.Add("PARA_CAT1", typeof(string));
            Param.Columns.Add("PARA_CAT2", typeof(string));
            Param.Columns.Add("PARA_CAT3", typeof(string));
            Param.Columns.Add("PARA_HEADING", typeof(string));
            Param.Columns.Add("PARA_STKTYPE", typeof(string));
            Param.Columns.Add("PARA_INVNO", typeof(string));
            Param.Columns.Add("PARA_SUPP", typeof(string));
            Param.Columns.Add("PARA_PONO", typeof(string));
            Param.Columns.Add("PARA_PROMOTOR", typeof(string));
            Param.Columns.Add("PARA_CAT4", typeof(string));
            Param.Columns.Add("PARA_CAT5", typeof(string));
            Param.Columns.Add("PARA_COM", typeof(string));
            dr = Param.NewRow();

            dr["PARA_USER"] = _user_id;
            dr["PARA_PERIOD"] = "FROM " + _fromdate.Date.ToString("dd/MMM/yyyy") + " TO " + _todate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = "ALL";
            dr["PARA_CUST"] = _Customer == "" ? "ALL" : _Customer.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_EXEC"] = _SalesExc == "" ? "ALL" : _SalesExc.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_DOCTYPE"] = "ALL";
            dr["PARA_ITCODE"] = _Itemcode == "" ? "ALL" : _Itemcode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_BRAND"] = _Brand == "" ? "ALL" : _Brand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_MODEL"] = _Model == "" ? "ALL" : _Model.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_CAT1"] = _Itemcat1 == "" ? "ALL" : _Itemcat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_CAT2"] = _Itemcat2 == "" ? "ALL" : _Itemcat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_CAT3"] = _Itemcat3 == "" ? "ALL" : _Itemcat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_CAT4"] = "ALL";
            dr["PARA_CAT5"] = "ALL";
            dr["PARA_STKTYPE"] = "ALL";
            dr["PARA_INVNO"] = "ALL";
            dr["PARA_PROMOTOR"] = _Promotor == "" ? "ALL" : _Promotor.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_HEADING"] = "ITEM SELL-OUT REPORT";
            dr["PARA_COM"] = "ABANS PLC";



            Param.Rows.Add(dr);
            DataTable result = bsObj.CHNLSVC.MsgPortal.GETSELL_OUT_REPORT(_fromdate, _todate, _user_id, _Brand, _Model, _Itemcode, _Itemcat1, _Itemcat2, _Itemcat3, _Promotor, _Customer, _SalesExc);

            _Sellout.Database.Tables["SellOut"].SetDataSource(result);
            _Sellout.Database.Tables["REP_PARA"].SetDataSource(Param);
            _Sellout.Database.Tables["MST_COM"].SetDataSource(MST_COM);
        }

        //ISURU 2017/03/06
        public void CollectionDetailRep(string company, DateTime fromdate, DateTime todate, string saletype, string customer, string invoiceno, string userid)
        {
            DataTable CollectData = bsObj.CHNLSVC.MsgPortal.GetCollectionDetails(company, fromdate, todate, saletype, customer, invoiceno, userid); ;
            DataTable filterData = new DataTable("fildata");

            filterData.Columns.Add("fromdate", typeof(DateTime));
            filterData.Columns.Add("todate", typeof(DateTime));
            filterData.Columns.Add("userid", typeof(string));
            filterData.Rows.Add(fromdate, todate, userid);
            CollectData.Columns.Add("invdate3", typeof(int));
            int i = 0;
            foreach (var data in CollectData.Rows)
            {
                if (CollectData.Rows[i]["INVDATE"].ToString() == "" || CollectData.Rows[i]["sar_receipt_date"].ToString() == "")
                {
                    CollectData.Rows[i]["invdate3"] = 0;
                }
                else
                {
                    DateTime invdate = Convert.ToDateTime(CollectData.Rows[i]["INVDATE"].ToString());
                    DateTime invdate2 = Convert.ToDateTime(CollectData.Rows[i]["sar_receipt_date"].ToString());
                    int invdate3 = invdate2.Subtract(invdate).Days;
                    CollectData.Rows[i]["invdate3"] = invdate3.ToString();
                }

                i++;
            }

            _Collectiondetail.Database.Tables["CollectData"].SetDataSource(CollectData);
            _Collectiondetail.Database.Tables["HeadDetail"].SetDataSource(filterData);

        }

        //ISURU 2017/05/22
        public void returnchequedet(string company, DateTime fromdate, DateTime todate, string customer, string userid, string cheque, string accountcode, string grntxt, string CusName, string Bank)
        {
            DataTable ChequeDetails = bsObj.CHNLSVC.MsgPortal.getreturnchequedet(company, fromdate, todate, customer, cheque, accountcode, grntxt, userid);
            DataTable ChequeDetailFilter = new DataTable("fildata");
            DataTable asd = bsObj.CHNLSVC.Sales.getcompanyforpanalty(company);
            string headcustomer = "";
            if (string.IsNullOrEmpty(customer))
            {
                headcustomer = "ALL";
            }
            else
            {
                headcustomer = customer;
            }

            ChequeDetailFilter.Columns.Add("fromdate", typeof(DateTime));
            ChequeDetailFilter.Columns.Add("todate", typeof(DateTime));
            ChequeDetailFilter.Columns.Add("userid", typeof(string));
            ChequeDetailFilter.Columns.Add("company", typeof(string));
            ChequeDetailFilter.Columns.Add("customer", typeof(string));
            ChequeDetailFilter.Columns.Add("headcustomer", typeof(string));
            ChequeDetailFilter.Columns.Add("CusName", typeof(string));
            ChequeDetailFilter.Columns.Add("Bank", typeof(string));
            ChequeDetailFilter.Columns.Add("cheque", typeof(string));
            ChequeDetailFilter.Rows.Add(fromdate, todate, userid, company, customer, headcustomer, (CusName == string.Empty || CusName == null) ? "ALL" : CusName, (Bank == string.Empty || Bank == null) ? "ALL" : Bank, (cheque == string.Empty || cheque == null) ? "ALL" : cheque);

            _chequedetail.Database.Tables["ChequeDetail"].SetDataSource(ChequeDetails);
            _chequedetail.Database.Tables["ChequeDetailFilter"].SetDataSource(ChequeDetailFilter);
            _chequedetail.Database.Tables["com"].SetDataSource(asd);
        }
        public void BOQDetails(InvReportPara _objRepoPara, string _boqNo, string _subCordinator)
        {
            DataTable param = new DataTable();
            DataTable result = new DataTable();
            tmp_user_pc = new DataTable();
            GLOB_DataTable = new DataTable();
            DataRow dr;
            bool _def = true;

            param.Columns.Add("fromdate", typeof(string));
            param.Columns.Add("todate", typeof(string));
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("com", typeof(string));
            param.Columns.Add("powered_by", typeof(string));
            param.Columns.Add("ProfitCenter", typeof(string));
            param.Columns.Add("Customer", typeof(string));
            param.Columns.Add("SubCordinator", typeof(string));

            dr = param.NewRow();

            dr["fromdate"] = _objRepoPara._GlbReportFromDate.ToShortDateString();
            dr["todate"] = _objRepoPara._GlbReportToDate.ToShortDateString();
            dr["user"] = _objRepoPara._GlbUserID == "" ? "" : _objRepoPara._GlbUserID.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["com"] = _objRepoPara._GlbReportCompName;
            dr["powered_by"] = _objRepoPara._GlbReportPoweredBy;
            dr["ProfitCenter"] = _objRepoPara._GlbReportProfit;
            dr["Customer"] = _objRepoPara._GlbReportCustomer;
            dr["SubCordinator"] = string.Empty;

            param.Rows.Add(dr);
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB_AllCom(_objRepoPara._GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.Inventory.GetBOQDetails("", _objRepoPara._GlbReportFromDate, _objRepoPara._GlbReportToDate, _objRepoPara._GlbReportProfit, _objRepoPara._GlbReportCustomer, "", _objRepoPara._GlbReportCompCode, _def);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }
            _boqDetails.Database.Tables["BOQDetails"].SetDataSource(GLOB_DataTable);
            _boqDetails.Database.Tables["BOQPara"].SetDataSource(param);
        }

        public void GetSalesOrderDetails(InvReportPara _objRepoPara)
        {
            string _err = "";
            DataTable results = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

             results = bsObj.CHNLSVC.MsgPortal.getSalesOrderSummaryDetails(_objRepoPara._GlbReportFromDate, _objRepoPara._GlbReportToDate, _objRepoPara._GlbReportCompCode, _objRepoPara._GlbReportOtherLoc, _objRepoPara._GlbReportExecutive, _objRepoPara._GlbUserID, _objRepoPara._GlbReportCustomer, out _err);

             param.Columns.Add("fromdate", typeof(string));
             param.Columns.Add("todate", typeof(string));
             param.Columns.Add("user", typeof(string));
             param.Columns.Add("com", typeof(string));

             dr = param.NewRow();

             dr["fromdate"] = _objRepoPara._GlbReportFromDate.ToShortDateString();
             dr["todate"] = _objRepoPara._GlbReportToDate.ToShortDateString();
             dr["user"] = _objRepoPara._GlbUserID;
             dr["com"] = _objRepoPara._GlbReportCompName;

             param.Rows.Add(dr);
            
             _sosumAAL.Database.Tables["Sales_Summary_Details"].SetDataSource(results);
             _sosumAAL.Database.Tables["param"].SetDataSource(param);
           
        }
    }
}