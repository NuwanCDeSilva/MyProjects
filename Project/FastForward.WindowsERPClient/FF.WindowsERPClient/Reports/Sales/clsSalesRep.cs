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
using System.Text.RegularExpressions;

namespace FF.WindowsERPClient
{
    class clsSalesRep
    {
        public FF.WindowsERPClient.Reports.HP.ReceivableMovementReports _recMoverpt = new FF.WindowsERPClient.Reports.HP.ReceivableMovementReports();
        public FF.WindowsERPClient.Reports.HP.ReceivableMovementSummaryReports _recMoveSumrpt = new FF.WindowsERPClient.Reports.HP.ReceivableMovementSummaryReports();
        public FF.WindowsERPClient.Reports.Sales.ServiceReceiptPrints _recSevRecrpt = new FF.WindowsERPClient.Reports.Sales.ServiceReceiptPrints();
        public FF.WindowsERPClient.Reports.Sales.SServiceReceiptPrints _SrecSevRecrpt = new FF.WindowsERPClient.Reports.Sales.SServiceReceiptPrints();
        public FF.WindowsERPClient.Reports.Sales.DeliveredSalesReport _delSalesrptPC = new FF.WindowsERPClient.Reports.Sales.DeliveredSalesReport();
        public FF.WindowsERPClient.Reports.Sales.Stock_Sale_Report _stocksale = new FF.WindowsERPClient.Reports.Sales.Stock_Sale_Report();
        public FF.WindowsERPClient.Reports.Sales.Stock_Sale_Report_sum _stocksalesum = new FF.WindowsERPClient.Reports.Sales.Stock_Sale_Report_sum();
        public FF.WindowsERPClient.Reports.Sales.DeliveredSalesComparisonReport _delSalesComrpt = new FF.WindowsERPClient.Reports.Sales.DeliveredSalesComparisonReport();
        public FF.WindowsERPClient.Reports.Sales.DeliveredSalesReport80_20 _delSalesrpt8020 = new FF.WindowsERPClient.Reports.Sales.DeliveredSalesReport80_20();
        public FF.WindowsERPClient.Reports.Sales.Delivered_Sales_GRN _delSalesrptGRN = new FF.WindowsERPClient.Reports.Sales.Delivered_Sales_GRN();
        public FF.WindowsERPClient.Reports.Sales.Delivered_Sales_GRN__Cost _delSalesrptGRNCost = new FF.WindowsERPClient.Reports.Sales.Delivered_Sales_GRN__Cost();
        public FF.WindowsERPClient.Reports.Sales.DeliveredSalesReport_Itemwise _delSalesrptItem = new FF.WindowsERPClient.Reports.Sales.DeliveredSalesReport_Itemwise();
        public FF.WindowsERPClient.Reports.Sales.DeliveredSalesReport_withCust _delSalesrptCust = new FF.WindowsERPClient.Reports.Sales.DeliveredSalesReport_withCust();
        public FF.WindowsERPClient.Reports.Sales.DeliveredSalesInsuranceReport _delSalesrptInsu = new FF.WindowsERPClient.Reports.Sales.DeliveredSalesInsuranceReport();
        public FF.WindowsERPClient.Reports.Sales.SalesSummary1 _cashSalesrpt = new FF.WindowsERPClient.Reports.Sales.SalesSummary1();
        public FF.WindowsERPClient.Reports.Sales.HP_SummaryRep _HPSalesrpt = new FF.WindowsERPClient.Reports.Sales.HP_SummaryRep();
        public FF.WindowsERPClient.Reports.Sales.Sales_Figures _SalesFigrpt = new FF.WindowsERPClient.Reports.Sales.Sales_Figures();
        public FF.WindowsERPClient.Reports.Sales.Sales_Figures_OrderBy _SalesFigOrdrpt = new FF.WindowsERPClient.Reports.Sales.Sales_Figures_OrderBy();
        public FF.WindowsERPClient.Reports.Sales.Forward_Sales_Report1 _ForwardSalesrpt1 = new FF.WindowsERPClient.Reports.Sales.Forward_Sales_Report1();
        public FF.WindowsERPClient.Reports.Sales.Forward_Sales_Report2 _ForwardSalesrpt2 = new FF.WindowsERPClient.Reports.Sales.Forward_Sales_Report2();
        public FF.WindowsERPClient.Reports.Sales.Forward_Sales_Report_cost _ForwardSalesrptcost = new FF.WindowsERPClient.Reports.Sales.Forward_Sales_Report_cost();
        public FF.WindowsERPClient.Reports.Sales.POS_Detail_Report _POSDtlrpt = new FF.WindowsERPClient.Reports.Sales.POS_Detail_Report();
        public FF.WindowsERPClient.Reports.Sales.Execitivewise_Sales_with_Invoices _ExecSaleInvoice = new FF.WindowsERPClient.Reports.Sales.Execitivewise_Sales_with_Invoices();
        public FF.WindowsERPClient.Reports.Sales.exchange_crnote_report _excrnote = new FF.WindowsERPClient.Reports.Sales.exchange_crnote_report();
        public FF.WindowsERPClient.Reports.Sales.DebtorSettlement _DebtSett = new FF.WindowsERPClient.Reports.Sales.DebtorSettlement();

        public FF.WindowsERPClient.Reports.Sales.DebtorSettlement_Outs _DebtSettOuts = new FF.WindowsERPClient.Reports.Sales.DebtorSettlement_Outs();
        public FF.WindowsERPClient.Reports.Sales.DebtorSettlement_Outs_PC _DebtSettOutPC = new FF.WindowsERPClient.Reports.Sales.DebtorSettlement_Outs_PC();
        public FF.WindowsERPClient.Reports.Sales.DebtorSettlement_Outs_PC_Meeting _DebtSettOutPCMeeting = new FF.WindowsERPClient.Reports.Sales.DebtorSettlement_Outs_PC_Meeting();
        public FF.WindowsERPClient.Reports.Sales.DebtorSettlement_PC _DebtSettPC = new FF.WindowsERPClient.Reports.Sales.DebtorSettlement_PC();
        public FF.WindowsERPClient.Reports.Sales.DebtorSettlement_Outs_PC_with_comm _DebtSettOutPCWithComm = new FF.WindowsERPClient.Reports.Sales.DebtorSettlement_Outs_PC_with_comm();

        public FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding _AgeDebtOuts = new FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding();
        public FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_PC _AgeDebtOutsPC = new FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_PC();
        public FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_dcn _AgeDebtOutsdcn = new FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_dcn();
        public FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_new_dcn _AgeDebtOuts_newdcn = new FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_new_dcn();
        public FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_adv _AgeDebtOuts_adv = new FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_adv();

        public FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_new _AgeDebtOuts_new = new FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_new();
        public FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_PC_new _AgeDebtOutsPC_new = new FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_PC_new();


        public FF.WindowsERPClient.Reports.Sales.DebtorSalesReceipts _DebtSalesRec = new FF.WindowsERPClient.Reports.Sales.DebtorSalesReceipts();
        public FF.WindowsERPClient.Reports.Sales.Receipt_List _RecList = new FF.WindowsERPClient.Reports.Sales.Receipt_List();
        public FF.WindowsERPClient.Reports.Sales.SOS _sos = new FF.WindowsERPClient.Reports.Sales.SOS();
        public FF.WindowsERPClient.Reports.Sales.Remitance_Det _remit_det = new FF.WindowsERPClient.Reports.Sales.Remitance_Det();
        public FF.WindowsERPClient.Reports.Sales.Remitance_Sum _RemSum = new FF.WindowsERPClient.Reports.Sales.Remitance_Sum();
        public FF.WindowsERPClient.Reports.Sales.Variation _Variation = new FF.WindowsERPClient.Reports.Sales.Variation();
        public FF.WindowsERPClient.Reports.Sales.Stamp_Duty_Report _Stamp_Duty = new FF.WindowsERPClient.Reports.Sales.Stamp_Duty_Report();
        public FF.WindowsERPClient.Reports.Sales.SVAT_Report _SVAT = new FF.WindowsERPClient.Reports.Sales.SVAT_Report();

        public FF.WindowsERPClient.Reports.Sales.InsuranceCoverNote _insCover = new FF.WindowsERPClient.Reports.Sales.InsuranceCoverNote();
        public FF.WindowsERPClient.Reports.Sales.InsuranceCoverNoteMBSL _insCoverMBSL = new FF.WindowsERPClient.Reports.Sales.InsuranceCoverNoteMBSL();
        public FF.WindowsERPClient.Reports.Sales.InsuranceCoverNoteUMS _insCoverUMS = new FF.WindowsERPClient.Reports.Sales.InsuranceCoverNoteUMS();
        public FF.WindowsERPClient.Reports.Sales.InsuranceCoverNoteJS _insCoverJS = new FF.WindowsERPClient.Reports.Sales.InsuranceCoverNoteJS();
        public FF.WindowsERPClient.Reports.Sales.InsuranceCoverNoteAIA _insCoverAIA = new FF.WindowsERPClient.Reports.Sales.InsuranceCoverNoteAIA();

        public FF.WindowsERPClient.Reports.Sales.HPInsuranceRegister _insReg = new FF.WindowsERPClient.Reports.Sales.HPInsuranceRegister();
        public FF.WindowsERPClient.Reports.Sales.VoucherPrints _intVou = new FF.WindowsERPClient.Reports.Sales.VoucherPrints();

        public FF.WindowsERPClient.Reports.Sales.ChequePrinting1 _vouPrn = new FF.WindowsERPClient.Reports.Sales.ChequePrinting1();
        public FF.WindowsERPClient.Reports.Sales.ChequePrinting _vouPrnvou = new FF.WindowsERPClient.Reports.Sales.ChequePrinting();

        public FF.WindowsERPClient.Reports.Sales.Channel_det_Report _chnlPrn = new FF.WindowsERPClient.Reports.Sales.Channel_det_Report();
        public FF.WindowsERPClient.Reports.Sales.Sub_channel_det_Report _sub_chnlPrn = new FF.WindowsERPClient.Reports.Sales.Sub_channel_det_Report();


        public FF.WindowsERPClient.Reports.Sales.Area_det_Report _arePrn = new FF.WindowsERPClient.Reports.Sales.Area_det_Report();
        public FF.WindowsERPClient.Reports.Sales.AllocateItemReport _allocateItm = new FF.WindowsERPClient.Reports.Sales.AllocateItemReport();

        public FF.WindowsERPClient.Reports.Sales.ServiceLocDetailsReport _serloc = new FF.WindowsERPClient.Reports.Sales.ServiceLocDetailsReport();

        public FF.WindowsERPClient.Reports.Sales.ServiceChnlParaReport _ser_chnlpara = new FF.WindowsERPClient.Reports.Sales.ServiceChnlParaReport();
        public FF.WindowsERPClient.Reports.Sales.GP_Detail_Repl _gpRepl = new FF.WindowsERPClient.Reports.Sales.GP_Detail_Repl();


        public FF.WindowsERPClient.Reports.Sales.Region_det_Report _regPrn = new FF.WindowsERPClient.Reports.Sales.Region_det_Report();
        public FF.WindowsERPClient.Reports.Sales.Zone_det_Report _zonPrn = new FF.WindowsERPClient.Reports.Sales.Zone_det_Report();

        public FF.WindowsERPClient.Reports.Sales.EcdVouchar _ecdVou = new FF.WindowsERPClient.Reports.Sales.EcdVouchar();
        public FF.WindowsERPClient.Reports.Sales.SparePartPrint _sprPrint = new FF.WindowsERPClient.Reports.Sales.SparePartPrint();
        public FF.WindowsERPClient.Reports.Sales.Quotation_RepPrint _QuoPrint = new FF.WindowsERPClient.Reports.Sales.Quotation_RepPrint();
        public FF.WindowsERPClient.Reports.Sales.Elite_Commission_Details _ECommPrint = new FF.WindowsERPClient.Reports.Sales.Elite_Commission_Details();
        public FF.WindowsERPClient.Reports.Sales.Elite_Commission_Summary _ECommSumm = new FF.WindowsERPClient.Reports.Sales.Elite_Commission_Summary();
        public FF.WindowsERPClient.Reports.Sales.Not_Reg_Vehicles_Report _NotRegVeh = new FF.WindowsERPClient.Reports.Sales.Not_Reg_Vehicles_Report();
        public FF.WindowsERPClient.Reports.Sales.Warr_Rpl_CRNote _Warr_Rpl_CRNote = new FF.WindowsERPClient.Reports.Sales.Warr_Rpl_CRNote();
        public FF.WindowsERPClient.Reports.Sales.Sales_Promotion_Achievement_Report _SalePromoArchieve = new FF.WindowsERPClient.Reports.Sales.Sales_Promotion_Achievement_Report();
        public FF.WindowsERPClient.Reports.Sales.DeliveredSalesWithSerial _delSerlSales = new FF.WindowsERPClient.Reports.Sales.DeliveredSalesWithSerial();
        //HASITH 25/01/2015
        public FF.WindowsERPClient.Reports.Sales.CreditNoteDetails _creditnotedetails = new FF.WindowsERPClient.Reports.Sales.CreditNoteDetails();
        public FF.WindowsERPClient.Reports.Sales.Price_Details_Report _pricedtl = new FF.WindowsERPClient.Reports.Sales.Price_Details_Report();
        public FF.WindowsERPClient.Reports.Sales.HPInsuranceClaimRegister _insRegClaim = new FF.WindowsERPClient.Reports.Sales.HPInsuranceClaimRegister();
        public FF.WindowsERPClient.Reports.Sales.HPInsuranceDocumentRequired _insRegDoc = new FF.WindowsERPClient.Reports.Sales.HPInsuranceDocumentRequired();
        public FF.WindowsERPClient.Reports.Sales.HPInsurancePolicyReport _insRegPol = new FF.WindowsERPClient.Reports.Sales.HPInsurancePolicyReport();
        public FF.WindowsERPClient.Reports.Sales.HPInsuranceRegisterNew _insRegnew = new FF.WindowsERPClient.Reports.Sales.HPInsuranceRegisterNew();
        public FF.WindowsERPClient.Reports.Sales.HPInsuranceSettlemetInscom _insRegSett = new FF.WindowsERPClient.Reports.Sales.HPInsuranceSettlemetInscom();
        public FF.WindowsERPClient.Reports.Sales.CancelledDocuments _canDoc = new FF.WindowsERPClient.Reports.Sales.CancelledDocuments();
        public FF.WindowsERPClient.Reports.Sales.ExtendedWarranty _extendedWar = new FF.WindowsERPClient.Reports.Sales.ExtendedWarranty();

        public FF.WindowsERPClient.Reports.Sales.ReturnChequeSettmentOutstanding _rtnChe = new FF.WindowsERPClient.Reports.Sales.ReturnChequeSettmentOutstanding();
        public FF.WindowsERPClient.Reports.Sales.ManualDocument _manDoc = new FF.WindowsERPClient.Reports.Sales.ManualDocument();
        public FF.WindowsERPClient.Reports.Sales.ReturnChequeSettlemtPayments _rtnCheDet = new FF.WindowsERPClient.Reports.Sales.ReturnChequeSettlemtPayments();

        public FF.WindowsERPClient.Reports.Sales.ExcessShort _excesShort = new FF.WindowsERPClient.Reports.Sales.ExcessShort();
        public FF.WindowsERPClient.Reports.Sales.Total_Revenue_Report _totrev = new FF.WindowsERPClient.Reports.Sales.Total_Revenue_Report();
        public FF.WindowsERPClient.Reports.Sales.PaymodewiseTr_Report _pmodewise = new FF.WindowsERPClient.Reports.Sales.PaymodewiseTr_Report();
        public FF.WindowsERPClient.Reports.Sales.DF_Sales_Statement _DFSaleSt = new FF.WindowsERPClient.Reports.Sales.DF_Sales_Statement();
        public FF.WindowsERPClient.Reports.Sales.DF_Consolidated_Sales_new _DFSaleConsolidate = new FF.WindowsERPClient.Reports.Sales.DF_Consolidated_Sales_new();
        public FF.WindowsERPClient.Reports.Sales.DF_Categorywise_Sales _DFCatSale = new FF.WindowsERPClient.Reports.Sales.DF_Categorywise_Sales();
        public FF.WindowsERPClient.Reports.Sales.DF_Sales_Currencywise3 _DFCurrSale = new FF.WindowsERPClient.Reports.Sales.DF_Sales_Currencywise3();
        public FF.WindowsERPClient.Reports.Sales.DF_Sales_Currencywise_dtl _DFCurrSaleDtl = new FF.WindowsERPClient.Reports.Sales.DF_Sales_Currencywise_dtl();
        public FF.WindowsERPClient.Reports.Sales.DF_Sales_CurrencywiseTr _DFCurrTr = new FF.WindowsERPClient.Reports.Sales.DF_Sales_CurrencywiseTr();
        public FF.WindowsERPClient.Reports.Sales.DF_SalesWithQty _DFSaleQty = new FF.WindowsERPClient.Reports.Sales.DF_SalesWithQty();
        public FF.WindowsERPClient.Reports.Sales.DF_Sales_Qty _DFSQty = new FF.WindowsERPClient.Reports.Sales.DF_Sales_Qty();
        public FF.WindowsERPClient.Reports.Sales.DF_MothlySales _DFSMonSls = new FF.WindowsERPClient.Reports.Sales.DF_MothlySales();
        public FF.WindowsERPClient.Reports.Sales.DF_WeeklySales _DFSWeekSls = new FF.WindowsERPClient.Reports.Sales.DF_WeeklySales();
        public FF.WindowsERPClient.Reports.Sales.DF_ItemCategorywise_Sales _DFSCatSls = new FF.WindowsERPClient.Reports.Sales.DF_ItemCategorywise_Sales();
        public FF.WindowsERPClient.Reports.Sales.SalesVatSchedule _salesTaxSch = new FF.WindowsERPClient.Reports.Sales.SalesVatSchedule();
        public FF.WindowsERPClient.Reports.Sales.A_Sales_Report _ASales = new FF.WindowsERPClient.Reports.Sales.A_Sales_Report();
        public FF.WindowsERPClient.Reports.Sales.Loyality_Disc_Report _LDisc = new FF.WindowsERPClient.Reports.Sales.Loyality_Disc_Report();
        public FF.WindowsERPClient.Reports.Sales.DF_ModelwiseSales _DFSaleModel = new FF.WindowsERPClient.Reports.Sales.DF_ModelwiseSales();
        public FF.WindowsERPClient.Reports.Sales.DealerCommision _delSerDelComm = new FF.WindowsERPClient.Reports.Sales.DealerCommision();

        public FF.WindowsERPClient.Reports.Sales.BOCCusReserveList _bocReserveList = new FF.WindowsERPClient.Reports.Sales.BOCCusReserveList();
        public FF.WindowsERPClient.Reports.Sales.BOCCusResReceipt _bocCusReceipt = new FF.WindowsERPClient.Reports.Sales.BOCCusResReceipt();

        public FF.WindowsERPClient.Reports.Sales.VehicleRegistrationSlip _vheRegSlip = new FF.WindowsERPClient.Reports.Sales.VehicleRegistrationSlip();
        public FF.WindowsERPClient.Reports.Sales.RegistrationReport _vheRegRPT = new FF.WindowsERPClient.Reports.Sales.RegistrationReport();
        public FF.WindowsERPClient.Reports.Sales.SummaryofWeekly _rtnSumWeek = new FF.WindowsERPClient.Reports.Sales.SummaryofWeekly();
        public FF.WindowsERPClient.Reports.Sales.QuoationDet _quoDet = new FF.WindowsERPClient.Reports.Sales.QuoationDet();
        public FF.WindowsERPClient.Reports.Sales.Sales_Target_Achievement_Del _trgt_Ach = new FF.WindowsERPClient.Reports.Sales.Sales_Target_Achievement_Del();
        public FF.WindowsERPClient.Reports.Sales.SalesPromoterDetails _salePromot_Details = new FF.WindowsERPClient.Reports.Sales.SalesPromoterDetails();
        public FF.WindowsERPClient.Reports.Sales.VehicleInsuranceCollection _vehicleInsCollect = new FF.WindowsERPClient.Reports.Sales.VehicleInsuranceCollection();
        public FF.WindowsERPClient.Reports.Service.Job_Invoice_SGL _JobInvoiceSGL = new FF.WindowsERPClient.Reports.Service.Job_Invoice_SGL();
        public FF.WindowsERPClient.Reports.Inventory.Del_Conf_Dtl_Report _DelConfRep = new FF.WindowsERPClient.Reports.Inventory.Del_Conf_Dtl_Report();

        public FF.WindowsERPClient.Reports.Sales.InvoicePOSPrint _invPosPrint = new FF.WindowsERPClient.Reports.Sales.InvoicePOSPrint();
        public FF.WindowsERPClient.Reports.Sales.SignOn_Slip _signOnSlip = new FF.WindowsERPClient.Reports.Sales.SignOn_Slip();
        public FF.WindowsERPClient.Reports.Sales.SignOff_Slip _signOffSlip = new FF.WindowsERPClient.Reports.Sales.SignOff_Slip();
        public FF.WindowsERPClient.Reports.Sales.SignOff_Deno _signOffDeno = new FF.WindowsERPClient.Reports.Sales.SignOff_Deno();
        public FF.WindowsERPClient.Reports.Sales.PO_Allocation _poalloc = new FF.WindowsERPClient.Reports.Sales.PO_Allocation();
        public FF.WindowsERPClient.Reports.Sales.PriceDet _priceDet = new FF.WindowsERPClient.Reports.Sales.PriceDet();

        public FF.WindowsERPClient.Reports.Sales.Discount_Report _discRep = new FF.WindowsERPClient.Reports.Sales.Discount_Report();
        public FF.WindowsERPClient.Reports.Sales.Remitance_Sum_Recon _RemSumRecon = new FF.WindowsERPClient.Reports.Sales.Remitance_Sum_Recon();
        public FF.WindowsERPClient.Reports.Sales.GPSummary _gpSumm = new FF.WindowsERPClient.Reports.Sales.GPSummary();
        public FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_Veh_reg _Age_Debtor_Outstanding_Veh_reg = new FF.WindowsERPClient.Reports.Sales.Age_Debtor_Outstanding_Veh_reg();

        public FF.WindowsERPClient.Reports.Sales.GP_Report _GP_Report = new FF.WindowsERPClient.Reports.Sales.GP_Report();
        public FF.WindowsERPClient.Reports.Sales.Receipt_List_summary _Receipt_List_summary = new FF.WindowsERPClient.Reports.Sales.Receipt_List_summary();
        public FF.WindowsERPClient.Reports.Sales.Bill_Collection_detail _billcoll_dtl = new FF.WindowsERPClient.Reports.Sales.Bill_Collection_detail();
        public FF.WindowsERPClient.Reports.Sales.Bill_Collection_summery _billcoll_summ = new FF.WindowsERPClient.Reports.Sales.Bill_Collection_summery();
        DataTable tmp_user_pc = new DataTable();
        DataTable GLOB_DataTable = new DataTable();

        //Invoice Direct Print Data Table
        DataTable mst_tax_master = new DataTable();
        DataTable salesDetails = new DataTable();
        DataTable sat_vou_det = new DataTable();
        DataTable param = new DataTable();
        DataTable mst_customer = new DataTable();
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
        DataTable hpt_acc = new DataTable();
        DataTable int_hdr = new DataTable();
        DataTable int_ser = new DataTable();
        DataTable sat_receiptCQ = new DataTable();
        DataTable sat_receiptitmCQ = new DataTable();
        DataTable sat_receiptitmCQ_copy = new DataTable();
        DataTable ref_rep_infor = new DataTable();
        DataTable accountDetails = new DataTable();
        DataTable deliveredSerials = new DataTable();
        DataTable MST_ITM1 = new DataTable();
        DataTable receiptDetails = new DataTable();
        DataTable hpt_shed = new DataTable();
        DataTable Promo = new DataTable();
        public bool isAccess = false;
        string invNo = default(string);
        decimal totAmt = 0;
        decimal _TotAmt = 0;
        decimal totDis = 0;
        bool run = false;
        bool chktotVal = false;
        //List<decimal> itemAmtTot = new List<decimal>();
        List<decimal> itemAmtDis = new List<decimal>();
        List<decimal> itemAmtTax = new List<decimal>();
        DataTable mst_item_copy = new DataTable();
        decimal vat = 0;
        Base bsObj;
        public static int _totYCount;
        public static decimal satitmTaxVal = 0;
        public static int _unitAmt = 0;
        public static int _disAmt = 0;
        public static int _totAmt = 0;
        public clsSalesRep()
        {
            bsObj = new Base();

        }

        public void GPSummaryReport()
        {
            //kapila 27/5/2017
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetItemWiseGpExcel_new(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportCusId, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode,
                        BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                       BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, BaseCls.GlbUserComCode,
                        BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFreeIssue, BaseCls.GlbReportItmClasif, BaseCls.GlbReportBrandMgr,
                        "INV", true/*withReversal */, 0, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, 0);
                    //Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(),);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("grouplocation", typeof(Int32));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);

            _gpSumm.Database.Tables["GLB_GP_REP"].SetDataSource(GLOB_DataTable);
            _gpSumm.Database.Tables["param"].SetDataSource(param);

        }

        public void RemSummaryReconPrint()
        {
            DataTable gnt_rem_sum = new DataTable();
            DataTable rtn_chq = new DataTable();
            DataTable dtNonPost = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            gnt_rem_sum.Clear();
            param.Clear();
            rtn_chq.Clear();
            dtNonPost.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("RemToBeBanked", typeof(Decimal));
            param.Columns.Add("CIH", typeof(Decimal));
            param.Columns.Add("CIHFinal", typeof(Decimal));
            param.Columns.Add("RemToBeBankedFinal", typeof(Decimal));
            param.Columns.Add("TotRemitance", typeof(Decimal));
            param.Columns.Add("TotRemitanceFinal", typeof(Decimal));
            param.Columns.Add("CommWithdrawn", typeof(Decimal));
            param.Columns.Add("CommWithdrawnFinal", typeof(Decimal));
            param.Columns.Add("status", typeof(string));
            param.Columns.Add("fin_status", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate.Date;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["RemToBeBanked"] = BaseCls.GlbRemToBeBanked;
            dr["CIH"] = BaseCls.GlbCIH;
            dr["CIHFinal"] = BaseCls.GlbCIHFinal;
            dr["RemToBeBankedFinal"] = BaseCls.GlbRemToBeBankedFinal;
            dr["TotRemitance"] = BaseCls.GlbTotRemitance;
            dr["TotRemitanceFinal"] = BaseCls.GlbTotRemitanceFinal;
            dr["CommWithdrawn"] = BaseCls.GlbCommWithdrawn;
            dr["CommWithdrawnFinal"] = BaseCls.GlbCommWithdrawnFinal;
            dr["status"] = BaseCls.GlbStatus;
            dr["fin_status"] = BaseCls.GlbReportStrStatus;
            param.Rows.Add(dr);

            gnt_rem_sum = bsObj.CHNLSVC.Financial.get_rem_sum_rep_recon(BaseCls.GlbUserComCode, BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            _RemSumRecon.Database.Tables["REM_SUM_REP_RECON"].SetDataSource(gnt_rem_sum);
            _RemSumRecon.Database.Tables["param"].SetDataSource(param);
        }

        public void GetDiscountReportDetails()
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
            dr["com"] = BaseCls.GlbReportComp;
            dr["pc"] = BaseCls.GlbReportProfit;
            dr["user"] = BaseCls.GlbUserID;
            dr["from_date"] = BaseCls.GlbReportFromDate;
            dr["to_date"] = BaseCls.GlbReportToDate;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["customer"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["discountgt"] = BaseCls.GlbReportParaLine1;
            dr["discountlt"] = BaseCls.GlbReportParaLine2;
            dr["executive"] = BaseCls.GlbReportExecCode == "" ? "ALL" : BaseCls.GlbReportExecCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');

            Param.Rows.Add(dr);

            DataTable data = bsObj.CHNLSVC.Sales.GetDiscountReportDetails(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3,
             BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportParaLine1, BaseCls.GlbReportParaLine2, BaseCls.GlbUserID);

            _discRep.Database.Tables["DISCOUNT_DETAILS"].SetDataSource(data);
            _discRep.Database.Tables["param"].SetDataSource(Param);
        }

        public void PriceDetailReport()
        {// kapila

            DataTable sar_pb_det = new DataTable();

            sar_pb_det = bsObj.CHNLSVC.MsgPortal.GetPricebyCircular(BaseCls.GlbReportDoc, null);

            _priceDet.Database.Tables["sar_pb_det"].SetDataSource(sar_pb_det);


        }
        public void POSReceiptDirectPrint()
        {// kapila 3/11/2015
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);

            pd.Document = pdoc;

            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintReceiptPage);
            pdoc.Print();
        }

        public void pdoc_PrintReceiptPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Tahoma", 10);
            float fontHeight = font.GetHeight();
            int startX = 0;
            int OffsetY = 0;

            string docNo = default(string);
            docNo = BaseCls.GlbReportDoc;
            Int32 _noOfPcs = 0;
            string _head = "";
            string _body = "";
            decimal _val = 0;
            string _vouNo = "";

            DataTable sat_receipt = new DataTable();
            DataTable sat_receiptitm = new DataTable();
            sat_receipt.Clear();
            sat_receipt = bsObj.CHNLSVC.Sales.GetReceipt(BaseCls.GlbReportDoc);
            sat_receiptitm = bsObj.CHNLSVC.Sales.GetReceiptItemDetails(BaseCls.GlbReportDoc);
            DataTable mst_profit_center = new DataTable();

            DataTable mst_rec_tp = new DataTable();
            DataTable hpt_insu = new DataTable();
            DataTable sat_recwarrex = new DataTable();
            DataTable sat_veh_reg_txn = new DataTable();
            DataTable sat_receiptitemdetails = new DataTable();
            DataTable mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            DataTable mst_emp = new DataTable();



            foreach (DataRow row in sat_receipt.Rows)
            {

                mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit);
                mst_rec_tp = bsObj.CHNLSVC.Sales.GetReceiptType(row["SAR_RECEIPT_TYPE"].ToString());
                sat_receiptitemdetails = bsObj.CHNLSVC.Sales.GetAdvanRecItems(row["SAR_RECEIPT_NO"].ToString());
                if (!string.IsNullOrEmpty(row["sar_anal_4"].ToString()))
                {
                    mst_emp = bsObj.CHNLSVC.Sales.GetinvEmp(row["SAR_COM_CD"].ToString(), row["sar_anal_4"].ToString());
                }

                _noOfPcs = _noOfPcs + 1;
                int index = sat_receipt.Rows.IndexOf(row);


                if (index == 0)
                {
                    graphics.DrawString(mst_com.Rows[0]["mc_anal5"].ToString(), new Font("Tahoma", 14),
                    new SolidBrush(Color.Black), startX + 100, OffsetY);

                    OffsetY = OffsetY + 18;
                    graphics.DrawString(mst_profit_center.Rows[0]["MPC_DESC"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString(mst_profit_center.Rows[0]["MPC_ADD_1"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString(mst_profit_center.Rows[0]["MPC_ADD_2"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Tel :", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);

                    graphics.DrawString(mst_profit_center.Rows[0]["MPC_TEL"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 75, OffsetY);

                    OffsetY = OffsetY + 16;
                    if (BaseCls.GlbReportTp == "REC")
                        _head = "GIFT VOUCHER RECEIPT";
                    else if (BaseCls.GlbReportTp == "ADVREC")
                        _head = "ADVANCE RECEIPT";
                    else
                        _head = "PAYMENT RECEIPT";

                    graphics.DrawString(_head, new Font("Tahoma", 12),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);

                    OffsetY = OffsetY + 20;
                    graphics.DrawString("Receipt No", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString(":", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 73, OffsetY);

                    graphics.DrawString(row["sar_anal_3"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 80, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Date", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);
                    graphics.DrawString(":", new Font("Tahoma", 8),
                             new SolidBrush(Color.Black), startX + 73, OffsetY);

                    graphics.DrawString(Convert.ToDateTime(row["sar_receipt_date"]).ToShortDateString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 80, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Showroom", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);
                    graphics.DrawString(":", new Font("Tahoma", 8),
                             new SolidBrush(Color.Black), startX + 73, OffsetY);

                    graphics.DrawString(mst_profit_center.Rows[0]["MPC_DESC"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 80, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("System Prefix", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);
                    graphics.DrawString(":", new Font("Tahoma", 8),
                             new SolidBrush(Color.Black), startX + 73, OffsetY);

                    graphics.DrawString(row["SAR_RECEIPT_NO"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 80, OffsetY);

                    List<ReceiptItemDetails> _tmpRecItem = new List<ReceiptItemDetails>();
                    _tmpRecItem = bsObj.CHNLSVC.Sales.GetAdvanReceiptItems(BaseCls.GlbReportDoc);
                    foreach (ReceiptItemDetails recitmdet in _tmpRecItem)
                    {
                        _vouNo = _vouNo + recitmdet.Sari_serial_1.ToString();
                    }

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Gift Voucher #", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);
                    graphics.DrawString(":", new Font("Tahoma", 8),
                             new SolidBrush(Color.Black), startX + 73, OffsetY);

                    graphics.DrawString(_vouNo, new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 80, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Customer", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);
                    graphics.DrawString(":", new Font("Tahoma", 8),
                             new SolidBrush(Color.Black), startX + 73, OffsetY);

                    graphics.DrawString(row["sar_debtor_name"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 80, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString(row["sar_debtor_add_1"].ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + 80, OffsetY);


                    if (BaseCls.GlbReportTp == "TICKT") //kapila 9/5/2016
                    {
                        DataTable _dtAddRec = bsObj.CHNLSVC.Financial.GetAddRecDetails(BaseCls.GlbReportDoc);
                        if (_dtAddRec.Rows.Count > 0)
                        {
                            OffsetY = OffsetY + 14;
                            graphics.DrawString("Ref #", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);
                            graphics.DrawString(":", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 73, OffsetY);
                            graphics.DrawString(_dtAddRec.Rows[0]["SRA_REF_NO"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 80, OffsetY);

                            OffsetY = OffsetY + 14;
                            graphics.DrawString("No of Tickets", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);
                            graphics.DrawString(":", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 73, OffsetY);
                            graphics.DrawString(_dtAddRec.Rows[0]["SRA_UNITS"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 80, OffsetY);

                            OffsetY = OffsetY + 14;
                            graphics.DrawString("Reserve Date", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);
                            graphics.DrawString(":", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 73, OffsetY);
                            graphics.DrawString(_dtAddRec.Rows[0]["SRA_REQ_DT"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 80, OffsetY);

                            OffsetY = OffsetY + 14;
                            graphics.DrawString("Ticket Type", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);
                            graphics.DrawString(":", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 73, OffsetY);
                            graphics.DrawString(_dtAddRec.Rows[0]["SRA_TP"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 80, OffsetY);

                            OffsetY = OffsetY + 14;
                            graphics.DrawString("Seat Type", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);
                            graphics.DrawString(":", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 73, OffsetY);
                            graphics.DrawString(_dtAddRec.Rows[0]["SRA_OTH_TP"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 80, OffsetY);

                            OffsetY = OffsetY + 14;
                            graphics.DrawString("Remarks", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);
                            graphics.DrawString(":", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 73, OffsetY);
                            graphics.DrawString(_dtAddRec.Rows[0]["SRA_RMKS"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 80, OffsetY);


                        }

                    }

                    OffsetY = OffsetY + 28;
                    _val = Convert.ToDecimal(row["sar_tot_settle_amt"]);
                    _body = "Received with thanks from";
                    graphics.DrawString(_body, new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    OffsetY = OffsetY + 14;
                    _body = row["sar_debtor_name"].ToString();
                    graphics.DrawString(_body, new Font("Tahoma", 8),
                     new SolidBrush(Color.Black), startX, OffsetY);

                    OffsetY = OffsetY + 14;
                    _body = "Sum of Rupees ";
                    graphics.DrawString(_body, new Font("Tahoma", 8),
                     new SolidBrush(Color.Black), startX, OffsetY);

                    OffsetY = OffsetY + 14;
                    _body = NumberToWords(Convert.ToInt32(_val)) + " Only";
                    graphics.DrawString(_body, new Font("Tahoma", 8),
                     new SolidBrush(Color.Black), startX, OffsetY);

                    OffsetY = OffsetY + 14;

                    graphics.DrawString(_val.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                         new SolidBrush(Color.Black), startX + 205, OffsetY);
                }


            }

            OffsetY = OffsetY + 28;
            graphics.DrawString("Pay Type", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            graphics.DrawString("Amount", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + 125, OffsetY);
            OffsetY = OffsetY + 12;
            graphics.DrawString("---------------------------------------------".ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);


            foreach (DataRow rowItm in sat_receiptitm.Rows)
            {
                OffsetY = OffsetY + 14;
                graphics.DrawString(rowItm["sard_pay_tp"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                _val = Convert.ToDecimal(rowItm["sard_settle_amt"]);
                graphics.DrawString(_val.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);
            }
        }


        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }


        public void POSCredNoteDirectPrint()
        {// kapila 8/10/2015
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);

            pd.Document = pdoc;

            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintCredNotePage);
            pdoc.Print();
        }

        public void pdoc_PrintCredNotePage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Tahoma", 10);
            float fontHeight = font.GetHeight();
            int startX = 0;
            int OffsetY = 0;

            string invNo = BaseCls.GlbReportDoc;
            Int32 _noOfPcs = 0;
            Int32 _noOfItms = 0;
            decimal _grossAmt = 0;
            decimal _grandTot = 0;
            decimal _totAmt = 0;
            decimal _unitAmt = 0;
            decimal _cashAmt = 0;
            decimal _discAmt = 0;
            decimal _changeGiven = 0;
            string _head = "";
            string _start = "";
            string _end = "";
            DataTable salesDetails = new DataTable();


            salesDetails.Clear();
            salesDetails = bsObj.CHNLSVC.Sales.GetSalesDetails(invNo, null);

            foreach (DataRow row in salesDetails.Rows)
            {
                _noOfPcs = _noOfPcs + Convert.ToInt32(row["SAD_QTY"]);
                _noOfItms = _noOfItms + 1;
                int index = salesDetails.Rows.IndexOf(row);


                if (index == 0)
                {
                    graphics.DrawString(row["mc_anal5"].ToString(), new Font("Tahoma", 14),
                    new SolidBrush(Color.Black), startX + 100, OffsetY);

                    OffsetY = OffsetY + 18;
                    graphics.DrawString(row["MPC_DESC"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString(row["MPC_ADD_1"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString(row["MPC_ADD_2"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Tel :", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);

                    graphics.DrawString(row["MPC_TEL"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 75, OffsetY);

                    OffsetY = OffsetY + 14;
                    if (BaseCls.GlbReportIsCostPrmission == 0)
                        _head = "CREDIT NOTE";
                    else if (BaseCls.GlbReportIsCostPrmission == 1)
                        _head = "CREDIT NOTE-COPY";

                    graphics.DrawString(_head, new Font("Tahoma", 12),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);

                    OffsetY = OffsetY + 18;
                    graphics.DrawString("Cashier :", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString(row["SE_USR_ID"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Inv No :", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString(row["SAH_INV_NO"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);

                    graphics.DrawString("Date :", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 155, OffsetY);

                    graphics.DrawString(Convert.ToDateTime(row["SAH_DT"]).ToShortDateString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 190, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString("PLU", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString("QTY", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 75, OffsetY);

                    graphics.DrawString("PRICE", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 125, OffsetY);

                    graphics.DrawString("AMOUNT", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 205, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                }

                OffsetY = OffsetY + 14;

                graphics.DrawString(row["MI_SHORTDESC"].ToString(), new Font("Tahoma", 8),
                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString(row["MI_CD"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);


                graphics.DrawString(row["SAD_QTY"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 100, OffsetY);

                _unitAmt = Convert.ToDecimal(row["SAD_UNIT_RT"]);
                graphics.DrawString(_unitAmt.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                _totAmt = Convert.ToDecimal(row["SAD_TOT_AMT"]);
                graphics.DrawString(_totAmt.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 205, OffsetY);

                _grossAmt = _grossAmt + Convert.ToDecimal(row["SAD_UNIT_AMT"]);
                _discAmt = _discAmt + Convert.ToDecimal(row["SAD_DISC_AMT"]);

                _grandTot = _grandTot + (_totAmt);
                _start = row["SAH_CRE_WHEN"].ToString();
                _end = row["SAH_CRE_WHEN"].ToString();
            }

            OffsetY = OffsetY + 14;
            graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            OffsetY = OffsetY + 30;
            graphics.DrawString("No of Items", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            graphics.DrawString(_noOfItms.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + 125, OffsetY);
            OffsetY = OffsetY + 14;
            graphics.DrawString("No of Pieces", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            graphics.DrawString(_noOfPcs.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + 125, OffsetY);

            OffsetY = OffsetY + 14;
            graphics.DrawString("Gross Amount", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            graphics.DrawString(_grossAmt.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + 125, OffsetY);

            if (_discAmt > 0)
            {
                OffsetY = OffsetY + 14;
                graphics.DrawString("Discount", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(_discAmt.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);
            }

            OffsetY = OffsetY + 14;
            graphics.DrawString("Grand Total", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            graphics.DrawString(_grandTot.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + 125, OffsetY);

            //List<RecieptItem> _ListRecItem = bsObj.CHNLSVC.Sales.GetReceiptItemsByInvoice(invNo);
            //foreach (RecieptItem _lst in _ListRecItem)
            //{
            //    if (_lst.Sard_pay_tp == "CASH")
            //    {
            //        _cashAmt = _lst.Sard_settle_amt;
            //        _changeGiven = _lst.Sard_anal_4;
            //    }
            //}
            if (_cashAmt > 0)
            {
                OffsetY = OffsetY + 14;
                graphics.DrawString("CASH", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString((_cashAmt + _changeGiven).ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);
            }
            OffsetY = OffsetY + 14;
            graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            if (_changeGiven > 0)
            {
                OffsetY = OffsetY + 14;
                graphics.DrawString("Change", new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(_changeGiven.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

            }
            //OffsetY = OffsetY + 14;
            //graphics.DrawString("Start Time :", new Font("Tahoma", 8),
            //                new SolidBrush(Color.Black), startX,  OffsetY);

            //graphics.DrawString(_start.ToString(), new Font("Tahoma", 8),
            //                new SolidBrush(Color.Black), startX + 80,  OffsetY);
            //OffsetY = OffsetY + 14;
            //graphics.DrawString("End Time :", new Font("Tahoma", 8),
            //                new SolidBrush(Color.Black), startX,  OffsetY);

            //graphics.DrawString(_end.ToString(), new Font("Tahoma", 8),
            //                new SolidBrush(Color.Black), startX + 80,  OffsetY);





        }

        public void POSDenoDirectPrint()
        {// kapila 8/10/2015
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);

            pd.Document = pdoc;

            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintDenoPage);
            pdoc.Print();
        }
        public void pdoc_PrintDenoPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Tahoma", 10);
            float fontHeight = font.GetHeight();
            int startX = 10;
            int OffsetY = 0;
            int OffsetY2 = 0;
            decimal _val = 0;
            decimal _totDenom = 0;
            decimal _totVal = 0;
            decimal _totSales = 0;

            string invNo = BaseCls.GlbReportDoc;


            DataTable _signOn = new DataTable();
            DataTable _dtDenom = new DataTable();


            _signOn.Clear();
            _signOn = bsObj.CHNLSVC.Financial.get_PrintSignOn(BaseCls.GlbReportParaLine1);

            foreach (DataRow row in _signOn.Rows)
            {


                graphics.DrawString(row["mc_desc"].ToString(), new Font("Tahoma", 14),
                    new SolidBrush(Color.Black), startX + 100, OffsetY);

                OffsetY = OffsetY + 28;
                graphics.DrawString(row["MPC_ADD_1"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 50, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString(row["MPC_ADD_2"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 50, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("Tel :", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 50, OffsetY);

                graphics.DrawString(row["MPC_TEL"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 75, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("** CLOSE SALE REPORT **".ToString(), new Font("Tahoma", 10),
                                new SolidBrush(Color.Black), startX + 55, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Cashier", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 110, OffsetY);

                graphics.DrawString(row["sig_cashier"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Cashier Name", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 110, OffsetY);

                graphics.DrawString(row["se_usr_name"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Station ID", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 110, OffsetY);

                graphics.DrawString(row["sig_terminal"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Sign On Date/Time", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 110, OffsetY);

                graphics.DrawString(row["sig_on_dt"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Sign Off Date/Time", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 110, OffsetY);

                graphics.DrawString(row["sig_off_dt"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                OffsetY2 = OffsetY;
                graphics.DrawString("Float", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 50, OffsetY);

                _val = Convert.ToDecimal(row["sig_op_bal"]);
                graphics.DrawString(_val.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 60, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Sales", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 50, OffsetY);

                DataTable _dtSales = bsObj.CHNLSVC.Financial.getUserTotalSales(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, Convert.ToDateTime(row["sig_on_dt"]), row["sig_cashier"].ToString());
                if (_dtSales.Rows.Count > 0)
                    _totSales = Convert.ToDecimal(_dtSales.Rows[0]["sad_tot_amt"]);

                graphics.DrawString(_totSales.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 60, OffsetY);

                DataTable _dtDenomSum = new DataTable();
                _dtDenomSum = bsObj.CHNLSVC.Financial.getDenominationSum(BaseCls.GlbReportParaLine1);
                foreach (DataRow rowSum in _dtDenomSum.Rows)
                {
                    graphics.DrawString(rowSum["gds_pay_tp"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 125, OffsetY2);

                    graphics.DrawString(":", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 200, OffsetY2);

                    if (rowSum["gds_pay_tp"].ToString() == "CASH")
                        _val = Convert.ToDecimal(rowSum["gds_phy_amt"]) + Convert.ToDecimal(row["sig_op_bal"]);
                    else
                        _val = Convert.ToDecimal(rowSum["gds_phy_amt"]);

                    graphics.DrawString(_val.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 210, OffsetY2);

                    OffsetY2 = OffsetY2 + 14;
                    _totVal = _totVal + _val;

                }

                OffsetY = OffsetY2;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Total", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 50, OffsetY);

                _val = Convert.ToDecimal(row["sig_op_bal"]) + _totSales;
                graphics.DrawString(_val.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 60, OffsetY);

                graphics.DrawString("Total", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 175, OffsetY);

                graphics.DrawString(_totVal.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 210, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 3;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("** Physical Cash Denominations **".ToString(), new Font("Tahoma", 10),
                                new SolidBrush(Color.Black), startX + 20, OffsetY);


                _dtDenom.Clear();
                _dtDenom = bsObj.CHNLSVC.Financial.getDenominationDet(BaseCls.GlbReportParaLine1);
                OffsetY = OffsetY + 10;

                foreach (DataRow row1 in _dtDenom.Rows)
                {
                    OffsetY = OffsetY + 14;

                    graphics.DrawString(row1["gdd_pay_subtp"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 30, OffsetY);

                    graphics.DrawString("X", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 90, OffsetY);


                    graphics.DrawString(row1["gdd_unit"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 120, OffsetY);

                    graphics.DrawString("=", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 150, OffsetY);

                    _val = Convert.ToDecimal(row1["gdd_amt"]);
                    graphics.DrawString(_val.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 210, OffsetY);

                    _totDenom = _totDenom + _val;

                }

                OffsetY = OffsetY + 14;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Total", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 30, OffsetY);

                graphics.DrawString(_totDenom.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                new SolidBrush(Color.Black), startX + 210, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Solution By", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(row["mc_it_powered"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 75, OffsetY);

            }
        }

        public void POSSignOffDirectPrint()
        {// kapila 8/10/2015
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);

            pd.Document = pdoc;

            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintSignOffPage);
            pdoc.Print();
        }
        public void pdoc_PrintSignOffPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Tahoma", 10);
            float fontHeight = font.GetHeight();
            int startX = 0;
            int OffsetY = 0;
            decimal _opBal = 0;

            string invNo = BaseCls.GlbReportDoc;


            DataTable _signOn = new DataTable();


            _signOn.Clear();
            _signOn = bsObj.CHNLSVC.Financial.get_PrintSignOn(BaseCls.GlbReportParaLine1);

            foreach (DataRow row in _signOn.Rows)
            {


                graphics.DrawString(row["mc_desc"].ToString(), new Font("Tahoma", 14),
                    new SolidBrush(Color.Black), startX + 100, OffsetY);

                OffsetY = OffsetY + 28;
                graphics.DrawString(row["MPC_ADD_1"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 50, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString(row["MPC_ADD_2"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 50, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("Tel :", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 50, OffsetY);

                graphics.DrawString(row["MPC_TEL"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 75, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("** SIGN OFF SLIP **".ToString(), new Font("Tahoma", 10),
                                new SolidBrush(Color.Black), startX + 75, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Cashier", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 110, OffsetY);

                graphics.DrawString(row["sig_cashier"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Cashier Name", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 110, OffsetY);

                graphics.DrawString(row["se_usr_name"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Station ID", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 110, OffsetY);

                graphics.DrawString(row["sig_terminal"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Sign On Date/Time", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 110, OffsetY);

                graphics.DrawString(row["sig_on_dt"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Sign Off Date/Time", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 110, OffsetY);

                graphics.DrawString(row["sig_off_dt"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 21;
                graphics.DrawString("Total Cash Collection", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 110, OffsetY);

                _opBal = Convert.ToDecimal(row["sig_close_bal"]);
                graphics.DrawString(_opBal.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Solution By", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(row["mc_it_powered"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 75, OffsetY);

            }
        }

        public void hugpdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Tahoma", 10);
            float fontHeight = font.GetHeight();
            int startX = 0;
            int OffsetY = 0;

            string invNo = BaseCls.GlbReportDoc;
            Int32 _noOfPcs = 0;
            Int32 _noOfItms = 0;
            decimal _grossAmt = 0;
            decimal _grandTot = 0;
            decimal _grandTax = 0;
            decimal _totTax = 0;
            decimal _totAmt = 0;
            decimal _unitAmt = 0;
            decimal _cashAmt = 0;
            decimal _changeGiven = 0;
            string _head = "";
            string _start = "";
            string _end = "";
            string _payTp = "";
            Int32 _isTaxInv = 0;
            DataTable salesDetails = new DataTable();

            if (BaseCls.GlbReportIsCostPrmission == 0)
                _head = "Invoice";
            else if (BaseCls.GlbReportIsCostPrmission == 1)
                _head = "Invoice-Copy";

            salesDetails.Clear();
            salesDetails = bsObj.CHNLSVC.Sales.GetSalesDetails(invNo, null);

            foreach (DataRow row in salesDetails.Rows)
            {
                _noOfPcs = _noOfPcs + Convert.ToInt32(row["SAD_QTY"]);
                int index = salesDetails.Rows.IndexOf(row);

                _noOfItms = _noOfItms + 1;
                if (index == 0)
                {
                    graphics.DrawString("BOSS", new Font("Times New Roman", 50),
                    new SolidBrush(Color.Black), startX + 40, OffsetY);
                    OffsetY = OffsetY + 70;
                    graphics.DrawString("H  U  G  O   B  O  S  S", new Font("Times New Roman", 11),
                    new SolidBrush(Color.Black), startX + 56, OffsetY);

                    OffsetY = OffsetY + 25;
                    graphics.DrawString(_head, new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 90, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString(row["MPC_DESC"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 25, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString(row["MPC_ADD_1"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 25, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString(row["MPC_ADD_2"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 25, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Tel :", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 25, OffsetY);

                    graphics.DrawString(row["MPC_TEL"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);

                    OffsetY = OffsetY + 20;
                    graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString(_head, new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 90, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX, OffsetY);

                    OffsetY = OffsetY + 20;
                    graphics.DrawString("Transaction No :", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString(row["SAH_INV_NO"].ToString(), new Font("Tahoma", 8),
                       new SolidBrush(Color.Black), startX + 130, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX, OffsetY);

                    //graphics.DrawString(row["se_usr_name"].ToString(), new Font("Tahoma", 8),
                    //                new SolidBrush(Color.Black), startX + 60, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Date :", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString(Convert.ToDateTime(row["SAH_DT"]).ToShortDateString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);

                    graphics.DrawString("Time :", new Font("Tahoma", 8),
                new SolidBrush(Color.Black), startX + 130, OffsetY);

                    TimeSpan _printTime = DateTime.Now.TimeOfDay;

                    graphics.DrawString(_printTime.ToString().Substring(0, 5), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 170, OffsetY);

                    //graphics.DrawString(row["ESEP_FIRST_NAME"].ToString() + " " + row["ESEP_LAST_NAME"].ToString(), new Font("Tahoma", 8),
                    //                new SolidBrush(Color.Black), startX + 60, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Site :", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString(row["MPC_CD"].ToString(), new Font("Tahoma", 8),
                new SolidBrush(Color.Black), startX + 50, OffsetY);

                    graphics.DrawString("POS :", new Font("Tahoma", 8),
new SolidBrush(Color.Black), startX + 130, OffsetY);

                    graphics.DrawString("1", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 170, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX, OffsetY);


                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Cashier :", new Font("Tahoma", 8),
                                   new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString(row["ESEP_FIRST_NAME"].ToString() + " " + row["ESEP_LAST_NAME"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString(row["ESEP_CD"].ToString(), new Font("Tahoma", 8),
                                   new SolidBrush(Color.Black), startX + 50, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Consumer No :", new Font("Tahoma", 8),
                                   new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString(row["SAH_CUS_CD"].ToString(), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + 80, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString(row["SAH_CUS_NAME"].ToString(), new Font("Tahoma", 8),
                                   new SolidBrush(Color.Black), startX, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX, OffsetY);

                }


                OffsetY = OffsetY + 18;
                graphics.DrawString("EAN :", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(row["MI_CD"].ToString(), new Font("Tahoma", 7),
                new SolidBrush(Color.Black), startX + 40, OffsetY);

                graphics.DrawString(row["SAD_QTY"].ToString(), new Font("Tahoma", 7),
                new SolidBrush(Color.Black), startX + 140, OffsetY);

                _unitAmt = Convert.ToDecimal(row["SAD_TOT_AMT"]);
                graphics.DrawString(_unitAmt.ToString("#,###,##0.00") + " " + "LKR", new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX + 190, OffsetY);

                OffsetY = OffsetY + 14;

                graphics.DrawString(row["MI_SHORTDESC"].ToString(), new Font("Tahoma", 7),
                 new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("You were served by :", new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(row["ESEP_CD"].ToString(), new Font("Tahoma", 7),
                new SolidBrush(Color.Black), startX + 110, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Color :", new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX, OffsetY);

                MasterItem _mstItem = bsObj.CHNLSVC.General.GetItemMaster(row["MI_CD"].ToString());

                graphics.DrawString(_mstItem.Mi_color_int.ToString(), new Font("Tahoma", 7),
                 new SolidBrush(Color.Black), startX + 40, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Size :", new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX, OffsetY);


                graphics.DrawString(_mstItem.Mi_size.ToString(), new Font("Tahoma", 7),
                 new SolidBrush(Color.Black), startX + 40, OffsetY);

                _totAmt = Convert.ToDecimal(row["SAD_UNIT_AMT"]);
                _totTax = Convert.ToDecimal(row["SAD_ITM_TAX_AMT"]);

                _grossAmt = _grossAmt + (_totAmt);
                _grandTax = _grandTax + _totTax;

                _start = row["SAH_CRE_WHEN"].ToString();
                _end = row["SAH_CRE_WHEN"].ToString();
                _isTaxInv = Convert.ToInt32(row["SAH_TAX_INV"]);
            }

            _grandTot = _grossAmt + (_grandTax);

            OffsetY = OffsetY + 14;
            graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);
            OffsetY = OffsetY + 3;
            graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            OffsetY = OffsetY + 18;
            graphics.DrawString("Total", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            if (_isTaxInv == 1)
            {
                graphics.DrawString(_grossAmt.ToString("#,###,##0.00") + " " + "LKR", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 175, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("VAT", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(_grandTax.ToString("#,###,##0.00") + " " + "LKR", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 175, OffsetY);

            }
            else
            {
                graphics.DrawString(_grandTot.ToString("#,###,##0.00") + " " + "LKR", new Font("Tahoma", 8),
                new SolidBrush(Color.Black), startX + 175, OffsetY);
            }

            OffsetY = OffsetY + 14;
            graphics.DrawString("Net Amount", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            graphics.DrawString(_grandTot.ToString("#,###,##0.00") + " " + "LKR", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + 175, OffsetY);

            OffsetY = OffsetY + 20;
            List<RecieptItem> _ListRecItem = bsObj.CHNLSVC.Sales.GetReceiptItemsByInvoice(invNo);
            foreach (RecieptItem _lst in _ListRecItem)
            {
                if (_lst.Sard_pay_tp == "CASH")
                {
                    _cashAmt = _lst.Sard_settle_amt;
                    _changeGiven = _lst.Sard_anal_4;
                    if (_cashAmt > 0)
                    {
                        OffsetY = OffsetY + 14;
                        graphics.DrawString("CASH", new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX, OffsetY);

                        graphics.DrawString((_cashAmt + _changeGiven).ToString("#,###,##0.00") + " " + "LKR", new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + 175, OffsetY);
                    }
                }
                else
                {
                    OffsetY = OffsetY + 14;
                    if (_lst.Sard_pay_tp == "CRCD")
                        _payTp = "CREDIT CARD";
                    if (_lst.Sard_pay_tp == "GVO")
                        _payTp = "GIFT VOUCHER";
                    if (_lst.Sard_pay_tp == "CRNOTE")
                        _payTp = "EXCHANGE";
                    if (_lst.Sard_pay_tp == "ADVAN")
                        _payTp = "ADVANCE";
                    if (_lst.Sard_pay_tp == "GVS")
                        _payTp = "GIFT VOUCHER";
                    if (_lst.Sard_pay_tp == "MCASH")
                        _payTp = "MCASH";
                    if (_lst.Sard_pay_tp == "STAR_PO")
                        _payTp = "STAR POINTS";
                    if (_lst.Sard_pay_tp == "CHEQUE")
                        _payTp = "CHEQUE";
                    if (_lst.Sard_pay_tp == "LORE")
                        _payTp = "LOYALITY REEDIM";
                    if (_lst.Sard_pay_tp == "DEBT")
                        _payTp = "DEBIT CARD";
                    //if (_lst.Sard_pay_tp == "LORE")
                    //    _payTp = "LOYALITY REEDIM";

                    graphics.DrawString(_payTp, new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString((_lst.Sard_settle_amt).ToString("#,###,##0.00") + " " + "LKR", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 175, OffsetY);
                }
            }

            OffsetY = OffsetY + 14;
            graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);



            OffsetY = OffsetY + 28;
            if (BaseCls.GlbUserDefProf == "ABWDC")
            {
                graphics.DrawString("No Exchanges. No Refund.".ToString(), new Font("Tahoma", 8),
                                 new SolidBrush(Color.Black), startX, OffsetY);
            }
            else
            {
                graphics.DrawString("Exchange and Refund Policies".ToString(), new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 20;
                graphics.DrawString("Kindly examine the goods for any observable defects.".ToString(), new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("New and unused goods may be exchanged once within".ToString(), new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("7 days with proof of purchase. Exchanges or refund".ToString(), new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("(unless permitted by law) do not apply to used, altered".ToString(), new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("goods, sales items and damaged items (except inherent".ToString(), new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("defects). Exchanges do not apply to swimwear,".ToString(), new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("bodywear and hosiery due to hygiene reasons.".ToString(), new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("Repairs may be applied subject to the Sri Lanka consumer".ToString(), new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("protections Laws.".ToString(), new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 20;
                graphics.DrawString("Thank you for shopping at HUGO BOSS.".ToString(), new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX + 15, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("We hope to see you again soon.".ToString(), new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX + 30, OffsetY);

                OffsetY = OffsetY + 20;
                graphics.DrawString("We are proud to represent Hugo Boss in Sri Lanka".ToString(), new Font("Tahoma", 7, FontStyle.Bold),
                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("ABANS PLC".ToString(), new Font("Tahoma", 10, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX + 60, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("No. 498, Galle Road, Colombo 03".ToString(), new Font("Tahoma", 7),
                                new SolidBrush(Color.Black), startX + 30, OffsetY);


            }
            //OffsetY = OffsetY + 28;
            //graphics.DrawString("** THANK YOU **".ToString(), new Font("Tahoma", 12),
            //                new SolidBrush(Color.Black), startX + 50, OffsetY);


            //OffsetY = OffsetY + 50;
            //graphics.DrawString(".".ToString(), new Font("Tahoma", 2),
            //                new SolidBrush(Color.Black), startX + 50, OffsetY);


        }

        public void HugPOSInvoiceDirectPrint()
        {// kapila 8/10/2015
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);

            pd.Document = pdoc;

            pdoc.PrintPage += new PrintPageEventHandler(hugpdoc_PrintPage);
            pdoc.Print();
        }

        public void POSInvoiceDirectPrint()
        {// kapila 8/10/2015
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);

            pd.Document = pdoc;

            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
            pdoc.Print();
        }

        public void POSSignOnDirectPrint()
        {// kapila 8/10/2015
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);

            pd.Document = pdoc;

            pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintSignOnPage);
            pdoc.Print();
        }
        public void pdoc_PrintSignOnPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Tahoma", 10);
            float fontHeight = font.GetHeight();
            int startX = 0;
            int OffsetY = 0;
            decimal _opBal = 0;

            string invNo = BaseCls.GlbReportDoc;


            DataTable _signOn = new DataTable();


            _signOn.Clear();
            _signOn = bsObj.CHNLSVC.Financial.get_PrintSignOn(BaseCls.GlbReportParaLine1);

            foreach (DataRow row in _signOn.Rows)
            {


                graphics.DrawString(row["mc_desc"].ToString(), new Font("Tahoma", 14),
                    new SolidBrush(Color.Black), startX + 100, OffsetY);

                OffsetY = OffsetY + 28;
                graphics.DrawString(row["MPC_ADD_1"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 50, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString(row["MPC_ADD_2"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 50, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("Tel :", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 50, OffsetY);

                graphics.DrawString(row["MPC_TEL"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 75, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("** SIGN ON SLIP **".ToString(), new Font("Tahoma", 10),
                                new SolidBrush(Color.Black), startX + 75, OffsetY);
                OffsetY = OffsetY + 14;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Cashier", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 100, OffsetY);

                graphics.DrawString(row["sig_cashier"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Cashier Name", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 100, OffsetY);

                graphics.DrawString(row["se_usr_name"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Station ID", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 100, OffsetY);

                graphics.DrawString(row["sig_terminal"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Sign On Date/Time", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 100, OffsetY);

                graphics.DrawString(row["sig_on_dt"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Float Cash", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(":", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 100, OffsetY);

                _opBal = Convert.ToDecimal(row["sig_op_bal"]);
                graphics.DrawString(_opBal.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString("Solution By", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(row["mc_it_powered"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 75, OffsetY);

            }




        }
        public void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Tahoma", 10);
            float fontHeight = font.GetHeight();
            int startX = 0;
            int OffsetY = 0;

            string invNo = BaseCls.GlbReportDoc;
            Int32 _noOfPcs = 0;
            Int32 _noOfItms = 0;
            decimal _grossAmt = 0;
            decimal _grandTot = 0;
            decimal _totAmt = 0;
            decimal _unitAmt = 0;
            decimal _cashAmt = 0;
            decimal _changeGiven = 0;
            string _head = "";
            string _start = "";
            string _end = "";
            string _payTp = "";
            DataTable salesDetails = new DataTable();


            salesDetails.Clear();
            salesDetails = bsObj.CHNLSVC.Sales.GetSalesDetails(invNo, null);

            foreach (DataRow row in salesDetails.Rows)
            {
                _noOfPcs = _noOfPcs + Convert.ToInt32(row["SAD_QTY"]);
                int index = salesDetails.Rows.IndexOf(row);

                _noOfItms = _noOfItms + 1;
                if (index == 0)
                {
                    graphics.DrawString(row["mc_anal5"].ToString(), new Font("Tahoma", 14),
                    new SolidBrush(Color.Black), startX + 100, OffsetY);

                    OffsetY = OffsetY + 18;
                    graphics.DrawString(row["MPC_DESC"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString(row["MPC_ADD_1"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString(row["MPC_ADD_2"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Tel :", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);

                    graphics.DrawString(row["MPC_TEL"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 75, OffsetY);

                    OffsetY = OffsetY + 20;
                    if (BaseCls.GlbReportIsCostPrmission == 0)
                        _head = "INVOICE";
                    else if (BaseCls.GlbReportIsCostPrmission == 1)
                        _head = "INVOICE-COPY";

                    graphics.DrawString(_head, new Font("Tahoma", 12),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);

                    OffsetY = OffsetY + 20;
                    graphics.DrawString("Cashier", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString(":", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 50, OffsetY);

                    graphics.DrawString(row["se_usr_name"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 60, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Executive", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString(":", new Font("Tahoma", 8),
                new SolidBrush(Color.Black), startX + 50, OffsetY);

                    graphics.DrawString(row["ESEP_FIRST_NAME"].ToString() + " " + row["ESEP_LAST_NAME"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 60, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Inv No", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString(":", new Font("Tahoma", 8),
                new SolidBrush(Color.Black), startX + 50, OffsetY);

                    graphics.DrawString(row["SAH_INV_NO"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 60, OffsetY);

                    graphics.DrawString("Date :", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 155, OffsetY);

                    graphics.DrawString(Convert.ToDateTime(row["SAH_DT"]).ToShortDateString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 190, OffsetY);

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("Time :", new Font("Tahoma", 8),
                                   new SolidBrush(Color.Black), startX + 155, OffsetY);

                    TimeSpan _printTime = DateTime.Now.TimeOfDay;

                    graphics.DrawString(_printTime.ToString().Substring(0, 5), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 190, OffsetY);

                    if (!string.IsNullOrEmpty(row["SAH_REMARKS"].ToString()))
                    {
                        OffsetY = OffsetY + 14;
                        graphics.DrawString("Remarks", new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX, OffsetY);

                        graphics.DrawString(":", new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + 50, OffsetY);

                        graphics.DrawString(row["SAH_REMARKS"].ToString(), new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + 60, OffsetY);
                    }

                    OffsetY = OffsetY + 14;
                    graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString("PLU", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString("QTY", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 75, OffsetY);

                    graphics.DrawString("PRICE", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 125, OffsetY);

                    graphics.DrawString("AMOUNT", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 205, OffsetY);
                    OffsetY = OffsetY + 14;
                    graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                }

                OffsetY = OffsetY + 14;

                graphics.DrawString(row["MI_SHORTDESC"].ToString(), new Font("Tahoma", 8),
                new SolidBrush(Color.Black), startX, OffsetY);

                OffsetY = OffsetY + 14;
                graphics.DrawString(row["MI_CD"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);


                graphics.DrawString(row["SAD_QTY"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 100, OffsetY);

                _unitAmt = Convert.ToDecimal(row["SAD_TOT_AMT"]) / Convert.ToDecimal(row["SAD_QTY"]);

                graphics.DrawString(_unitAmt.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

                _totAmt = Convert.ToDecimal(row["SAD_TOT_AMT"]);
                graphics.DrawString(_totAmt.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 205, OffsetY);

                _grossAmt = _grossAmt + (_totAmt);
                _grandTot = _grandTot + (_totAmt);
                _start = row["SAH_CRE_WHEN"].ToString();
                _end = row["SAH_CRE_WHEN"].ToString();
            }

            OffsetY = OffsetY + 14;
            graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            OffsetY = OffsetY + 30;
            graphics.DrawString("No of Pieces", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            graphics.DrawString(_noOfPcs.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + 125, OffsetY);
            OffsetY = OffsetY + 14;
            graphics.DrawString("No of Items", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            graphics.DrawString(_noOfItms.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + 125, OffsetY);

            OffsetY = OffsetY + 14;
            graphics.DrawString("Gross Amount", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            graphics.DrawString(_grossAmt.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + 125, OffsetY);
            OffsetY = OffsetY + 14;
            graphics.DrawString("Grand Total", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            graphics.DrawString(_grandTot.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + 125, OffsetY);

            OffsetY = OffsetY + 8;
            List<RecieptItem> _ListRecItem = bsObj.CHNLSVC.Sales.GetReceiptItemsByInvoice(invNo);
            foreach (RecieptItem _lst in _ListRecItem)
            {
                if (_lst.Sard_pay_tp == "CASH")
                {
                    _cashAmt = _lst.Sard_settle_amt;
                    _changeGiven = _lst.Sard_anal_4;
                    if (_cashAmt > 0)
                    {
                        OffsetY = OffsetY + 14;
                        graphics.DrawString("CASH", new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX, OffsetY);

                        graphics.DrawString((_cashAmt + _changeGiven).ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + 125, OffsetY);
                    }
                }
                else
                {
                    OffsetY = OffsetY + 14;
                    if (_lst.Sard_pay_tp == "CRCD")
                        _payTp = "CREDIT CARD";
                    if (_lst.Sard_pay_tp == "GVO")
                        _payTp = "GIFT VOUCHER";
                    if (_lst.Sard_pay_tp == "CRNOTE")
                        _payTp = "EXCHANGE";
                    if (_lst.Sard_pay_tp == "ADVAN")
                        _payTp = "ADVANCE";
                    if (_lst.Sard_pay_tp == "GVS")
                        _payTp = "GIFT VOUCHER";
                    if (_lst.Sard_pay_tp == "MCASH")
                        _payTp = "MCASH";
                    if (_lst.Sard_pay_tp == "STAR_PO")
                        _payTp = "STAR POINTS";
                    if (_lst.Sard_pay_tp == "CHEQUE")
                        _payTp = "CHEQUE";
                    if (_lst.Sard_pay_tp == "LORE")
                        _payTp = "LOYALITY REEDIM";
                    if (_lst.Sard_pay_tp == "DEBT")
                        _payTp = "DEBIT CARD";
                    //if (_lst.Sard_pay_tp == "LORE")
                    //    _payTp = "LOYALITY REEDIM";

                    graphics.DrawString(_payTp, new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX, OffsetY);

                    graphics.DrawString((_lst.Sard_settle_amt).ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + 125, OffsetY);
                }
            }

            OffsetY = OffsetY + 14;
            graphics.DrawString("-----------------------------------------------------------------".ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX, OffsetY);

            if (_changeGiven > 0)
            {
                OffsetY = OffsetY + 14;
                graphics.DrawString("Change", new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX, OffsetY);

                graphics.DrawString(_changeGiven.ToString("#,###,##0.00"), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + 125, OffsetY);

            }
            //OffsetY = OffsetY + 14;
            //graphics.DrawString("Start Time :", new Font("Tahoma", 8),
            //                new SolidBrush(Color.Black), startX,  OffsetY);

            //graphics.DrawString(_start.ToString(), new Font("Tahoma", 8),
            //                new SolidBrush(Color.Black), startX + 80,  OffsetY);
            //OffsetY = OffsetY + 14;
            //graphics.DrawString("End Time :", new Font("Tahoma", 8),
            //                new SolidBrush(Color.Black), startX,  OffsetY);

            //graphics.DrawString(_end.ToString(), new Font("Tahoma", 8),
            //                new SolidBrush(Color.Black), startX + 80,  OffsetY);

            OffsetY = OffsetY + 28;
            if (BaseCls.GlbUserDefProf == "ABWDC")
            {
                graphics.DrawString("No Exchanges. No Refund.".ToString(), new Font("Tahoma", 8),
                                 new SolidBrush(Color.Black), startX, OffsetY);
            }
            else
            {
                graphics.DrawString("Exchange is possible with receipt & original".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 16;
                graphics.DrawString("packaging & tags within a period of 7 days".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 16;
                graphics.DrawString("at the outlet where the purchase is made.".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 16;
                graphics.DrawString("Discounted items not exchangable".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 16;
                graphics.DrawString("# NO RETURNS OR EXCHANGE ON SAREES,".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 16;
                graphics.DrawString("HAND BAGS, ACCESSORIES, JEWELLERY,".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 16;
                graphics.DrawString("ORNAMENTS, TOYS, INTIMATE ITEMS,".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 16;
                graphics.DrawString("COSMETICS AND PERFUMES, FOOT WEAR".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);
                OffsetY = OffsetY + 16;
                graphics.DrawString("NO CASH REFUND".ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX, OffsetY);
            }
            OffsetY = OffsetY + 28;
            graphics.DrawString("** THANK YOU **".ToString(), new Font("Tahoma", 12),
                            new SolidBrush(Color.Black), startX + 50, OffsetY);


            OffsetY = OffsetY + 50;
            graphics.DrawString(".".ToString(), new Font("Tahoma", 2),
                            new SolidBrush(Color.Black), startX + 50, OffsetY);


        }
        public void PrintSignOnOffSlip()
        {// kapila
            DataTable mst_com = new DataTable();
            DataRow dr;

            mst_com.Clear();


            mst_com.Columns.Add("mc_desc", typeof(string));
            mst_com.Columns.Add("mc_add1", typeof(string));
            mst_com.Columns.Add("mc_add2", typeof(string));
            mst_com.Columns.Add("mc_tel", typeof(string));


            MasterCompany _MasterComp = null;
            _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
            dr = mst_com.NewRow();
            dr["mc_desc"] = _MasterComp.Mc_desc.ToString();
            dr["mc_add1"] = _MasterComp.Mc_add1.ToString();
            dr["mc_add2"] = _MasterComp.Mc_add2.ToString();
            dr["mc_tel"] = _MasterComp.Mc_tel.ToString();
            mst_com.Rows.Add(dr);

            GLOB_DataTable = bsObj.CHNLSVC.Financial.get_PrintSignOn(BaseCls.GlbReportParaLine1);

            if (BaseCls.GlbReportName == "SignOn_Slip.rpt")
            {
                _signOnSlip.Database.Tables["GNT_SIGNON"].SetDataSource(GLOB_DataTable);
                _signOnSlip.Database.Tables["mst_com"].SetDataSource(mst_com);
            }
            else
            {
                _signOffSlip.Database.Tables["GNT_SIGNON"].SetDataSource(GLOB_DataTable);
                _signOffSlip.Database.Tables["mst_com"].SetDataSource(mst_com);
            }

        }

        public void PrintSignOnOffDeno()
        {// kapila
            DataTable mst_com = new DataTable();
            DataRow dr;

            mst_com.Clear();


            mst_com.Columns.Add("mc_desc", typeof(string));
            mst_com.Columns.Add("mc_add1", typeof(string));
            mst_com.Columns.Add("mc_add2", typeof(string));
            mst_com.Columns.Add("mc_tel", typeof(string));


            MasterCompany _MasterComp = null;
            _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
            dr = mst_com.NewRow();
            dr["mc_desc"] = _MasterComp.Mc_desc.ToString();
            dr["mc_add1"] = _MasterComp.Mc_add1.ToString();
            dr["mc_add2"] = _MasterComp.Mc_add2.ToString();
            dr["mc_tel"] = _MasterComp.Mc_tel.ToString();
            mst_com.Rows.Add(dr);

            GLOB_DataTable = bsObj.CHNLSVC.Financial.getDenominationDet(BaseCls.GlbReportParaLine1);


            _signOffDeno.Database.Tables["GNT_DENOM_DET"].SetDataSource(GLOB_DataTable);
            _signOffDeno.Database.Tables["mst_com"].SetDataSource(mst_com);


        }
        public void SalesTargetAchievementReport()
        {// Sanjeewa 25-05-2015
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetSalesTargetAchievementDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportBrand, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("brand", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            param.Rows.Add(dr);

            _trgt_Ach.Database.Tables["SALE_TARGET"].SetDataSource(GLOB_DataTable);
            _trgt_Ach.Database.Tables["param"].SetDataSource(param);

        }

        public void InvociePOSPrint()
        {
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
            if (BaseCls.GlbReportName == "InvoiceHalfPrints.rpt")
                _invPosPrint.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            else
                _invPosPrint.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();


            //DataTable accountDetails = bsObj.CHNLSVC.Sales.GetAccountDetails(invNo);
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
                    //if (accountDetails.Rows.Count > 0)
                    //{
                    //    tblComDate = bsObj.CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), accNo);
                    //}
                    //else
                    //{
                    tblComDate = bsObj.CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), invNo);
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

            // DataTable hpt_shed = bsObj.CHNLSVC.Sales.GetAccountSchedule(invNo);
            DataTable Promo = bsObj.CHNLSVC.Sales.GetPromotionByInvoice(invNo);
            DataTable ref_rep_infor = new DataTable();

            if (BaseCls.GlbReportName == "InvoiceHalfPrints.rpt")
                ref_rep_infor = bsObj.CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
            else
                ref_rep_infor = bsObj.CHNLSVC.Sales.GetReportInfor("InvoicePOSPrint.rpt");        //kapila 29/6/2015

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

            if (BaseCls.GlbReportName == "InvoiceHalfPrints.rpt")
            {
                _invPosPrint.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                _invPosPrint.Database.Tables["mst_com"].SetDataSource(mst_com);
                _invPosPrint.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
                _invPosPrint.Database.Tables["sat_itm"].SetDataSource(sat_itm);
                _invPosPrint.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
                _invPosPrint.Database.Tables["mst_item"].SetDataSource(mst_item);
                _invPosPrint.Database.Tables["sec_user"].SetDataSource(sec_user);
                _invPosPrint.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
                _invPosPrint.Database.Tables["param"].SetDataSource(param);
                _invPosPrint.Database.Tables["Promo"].SetDataSource(Promo);
            }
            else
            {
                _invPosPrint.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                _invPosPrint.Database.Tables["mst_com"].SetDataSource(mst_com);
                _invPosPrint.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
                _invPosPrint.Database.Tables["sat_itm"].SetDataSource(sat_itm);
                _invPosPrint.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
                _invPosPrint.Database.Tables["mst_item"].SetDataSource(mst_item);
                _invPosPrint.Database.Tables["sec_user"].SetDataSource(sec_user);
                _invPosPrint.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
                _invPosPrint.Database.Tables["param"].SetDataSource(param);
                _invPosPrint.Database.Tables["Promo"].SetDataSource(Promo);
            }



            if (BaseCls.GlbReportName == "InvoiceHalfPrints.rpt")
            {
                foreach (object repOp in _invPosPrint.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        //if (_cs.SubreportName == "rptWarranty")
                        //{
                        //    ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];

                        //    subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);

                        //}


                        //if (_cs.SubreportName == "rptCheque")
                        //{
                        //    ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];

                        //    subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                        //    subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);

                        //}
                        //if (_cs.SubreportName == "rptAccount")
                        //{
                        //    ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];

                        //    subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                        //    subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                        //}



                        if (_cs.SubreportName == "tax")
                        {
                            ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);


                        }
                        if (_cs.SubreportName == "rptComm")
                        {
                            ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                        }
                        //if (_cs.SubreportName == "rptWarr")
                        //{
                        //    mst_item1 = mst_item1.DefaultView.ToTable(true);
                        //    ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];

                        //    subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                        //    subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                        //    subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
                        //    subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);



                        //}
                        if (_cs.SubreportName == "giftVou")
                        {
                            ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        }
                        if (_cs.SubreportName == "loc")
                        {
                            ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                        }


                        //if (tblComDate.Rows.Count >0) 
                        //{
                        //  if (_cs.SubreportName == "warrComDate")
                        //  {
                        //      ReportDocument subRepDoc = invReport1.Subreports[_cs.SubreportName];
                        //      subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                        //  }
                        //}

                    }
                }
            }
            else
            {
                foreach (object repOp in _invPosPrint.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "rptWarranty")
                        {
                            ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);

                        }


                        if (_cs.SubreportName == "rptCheque")
                        {
                            ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                            subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);

                        }
                        //if (_cs.SubreportName == "rptAccount")
                        //{
                        //    ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];

                        //    subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                        //    subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                        //}



                        if (_cs.SubreportName == "tax")
                        {
                            ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);


                        }
                        if (_cs.SubreportName == "rptComm")
                        {
                            ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                        }
                        if (_cs.SubreportName == "rptWarr")
                        {
                            mst_item1 = mst_item1.DefaultView.ToTable(true);
                            ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                            subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                            subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
                            subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);



                        }
                        if (_cs.SubreportName == "giftVou")
                        {
                            ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                        }
                        if (_cs.SubreportName == "loc")
                        {
                            ReportDocument subRepDoc = _invPosPrint.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                        }


                        //if (tblComDate.Rows.Count >0) 
                        //{
                        //  if (_cs.SubreportName == "warrComDate")
                        //  {
                        //      ReportDocument subRepDoc = invReport1.Subreports[_cs.SubreportName];
                        //      subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                        //  }
                        //}

                    }
                }
            }




            //this.Text = "Invoice Print";

            //if (GlbReportName == "InvoiceHalfPrints.rpt")
            //    crystalReportViewer1.ReportSource = invReport1;
            //else
            //    crystalReportViewer1.ReportSource = invReportPOS;

            //crystalReportViewer1.RefreshReport();




        }
        public void GetSalesPromoterDetails()
        {// Wimal 22/06/2015
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetSalesPromoterDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            //DataTable TMP_DataTable = new DataTable();
            //TMP_DataTable = bsObj.CHNLSVC.Sales.GetSalesPromoterDetails(BaseCls.GlbUserID);
            //GLOB_DataTable.Merge(TMP_DataTable);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("brand", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");// System.DateTime.Now.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            param.Rows.Add(dr);

            _salePromot_Details.Database.Tables["glb_sale_promoter_details"].SetDataSource(GLOB_DataTable);
            _salePromot_Details.Database.Tables["param"].SetDataSource(param);

        }

        public void GetDeliveryConfirmationPendingDetails()
        {// Sanjeewa 29/09/2015
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetDeliveryCustomerPendingDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportCustomerCode, BaseCls.GlbUserID, BaseCls.GlbReportWithCost);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("channel", typeof(string));
            param.Columns.Add("withcost", typeof(Int16));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["channel"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode;
            dr["withcost"] = BaseCls.GlbReportWithCost;
            param.Rows.Add(dr);

            _DelConfRep.Database.Tables["DEL_CONF_DTL"].SetDataSource(GLOB_DataTable);
            _DelConfRep.Database.Tables["param"].SetDataSource(param);

        }

        public void BOCCustReceipt()
        {
            // kapila 
            DataTable glbBOC = new DataTable();
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


            glbBOC.Clear();
            glbBOC = bsObj.CHNLSVC.MsgPortal.BOCCustReceipt(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbReportDoc);


            _bocCusReceipt.Database.Tables["BOC_CUS_RESERVE"].SetDataSource(glbBOC);
            _bocCusReceipt.Database.Tables["param"].SetDataSource(param);
        }

        public void BOCReserveList()
        {
            // kapila 
            DataTable glbBOC = new DataTable();
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


            glbBOC.Clear();
            glbBOC = bsObj.CHNLSVC.MsgPortal.BOCCustomersPrint(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, "", BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbUserID);


            _bocReserveList.Database.Tables["BOC_CUS_RESERVE"].SetDataSource(glbBOC);
            _bocReserveList.Database.Tables["param"].SetDataSource(param);
        }
        public void VehicleRegistrationSlip()
        {
            // Nadeeka 
            DataTable sat_veh_reg_txn = new DataTable();

            sat_veh_reg_txn = bsObj.CHNLSVC.Sales.GetVehicalRegistrations(BaseCls.GlbReportDoc);
            DataTable mst_item = new DataTable();

            foreach (DataRow row in sat_veh_reg_txn.Rows)
            {
                mst_item = bsObj.CHNLSVC.Sales.GetItemCode(BaseCls.GlbUserComCode, row["srvt_itm_cd"].ToString());
            }

            mst_item = mst_item.DefaultView.ToTable(true);
            _vheRegSlip.Database.Tables["sat_veh_reg_txn"].SetDataSource(sat_veh_reg_txn);
            _vheRegSlip.Database.Tables["mst_item"].SetDataSource(mst_item);
        }
        public void ExcessShortPrint()
        {
            DataTable glb_excess_short = new DataTable();
            DataTable glb_excess_short_bal = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Clear();
            glb_excess_short.Clear();
            glb_excess_short_bal.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("prvBal", typeof(Decimal));
            param.Columns.Add("curBal", typeof(Decimal));
            param.Columns.Add("manager", typeof(string));
            param.Columns.Add("status", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["prvBal"] = BaseCls.GlbReportPrvBal;
            dr["curBal"] = BaseCls.GlbReportCurBal;
            dr["manager"] = "";

            MasterProfitCenter _masterPC = null;
            string _manCode = string.Empty;
            _masterPC = bsObj.CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            if (_masterPC != null)
            {
                _manCode = _masterPC.Mpc_man;
                DataTable _dt = bsObj.CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, _manCode);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    dr["manager"] = _dt.Rows[0]["ESEP_FIRST_NAME"].ToString() + ' ' + _dt.Rows[0]["ESEP_LAST_NAME"].ToString();
                }
            }

            dr["status"] = BaseCls.GlbStatus;

            param.Rows.Add(dr);

            glb_excess_short = bsObj.CHNLSVC.Sales.PrintExcessShort(BaseCls.GlbReportCompCode, BaseCls.GlbUserID, BaseCls.GlbReportDoc);
            glb_excess_short_bal = bsObj.CHNLSVC.Sales.PrintExcessShortBal(BaseCls.GlbReportCompCode, BaseCls.GlbUserDefProf, BaseCls.GlbReportAsAtDate);

            _excesShort.Database.Tables["EXCS_HDR"].SetDataSource(glb_excess_short);
            _excesShort.Database.Tables["EXCS_BAL"].SetDataSource(glb_excess_short_bal);
            _excesShort.Database.Tables["param"].SetDataSource(param);

        }

        public void RegistrationDetailsReport()
        {// Sanjeewa 13-11-2014
            DataTable param = new DataTable();
            //DataRow dr;
            string _err;
            string _filePath;

            Boolean TRPAYTP = bsObj.CHNLSVC.MsgPortal.GetRegistraion_ExcelDetails(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportDocType, BaseCls.GlbUserID, BaseCls.GlbUserComCode, out _err, out _filePath);

            if (TRPAYTP == false)
            {
                MessageBox.Show(_err.ToString(), "No data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(_err))
                {
                    MessageBox.Show(_err.ToString(), "System Error in Exporting to Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();
                }
            }
        }

        public void ActivePromotionReport()
        {// Sanjeewa 13-10-2014
            DataTable param = new DataTable();
            //DataRow dr;
            string _err;
            string _filePath;

            Boolean TRPAYTP = bsObj.CHNLSVC.MsgPortal.ActivePromotionDetails(BaseCls.GlbUserComCode, BaseCls.GlbReportDoc, BaseCls.GlbReportDirection, BaseCls.GlbReportPriceBook, BaseCls.GlbReportPBLevel, BaseCls.GlbReportStrStatus, BaseCls.GlbPayType, BaseCls.GlbReportType, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportDoc2, out _err, out _filePath);

            if (TRPAYTP == false)
            {
                MessageBox.Show(_err.ToString(), "No data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!string.IsNullOrEmpty(_err))
                {
                    MessageBox.Show(_err.ToString(), "System Error in Exporting to Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo(_filePath);
                    p.Start();
                }
            }
        }

        public void ExtendedWarrantyPrint()
        {
            DataTable glb_ext_war = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Clear();
            glb_ext_war.Clear();

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

            glb_ext_war = bsObj.CHNLSVC.Sales.PrintExtendedWarranty(BaseCls.GlbReportCompCode, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportStatus);

            _extendedWar.Database.Tables["WAR_EXT"].SetDataSource(glb_ext_war);
            _extendedWar.Database.Tables["param"].SetDataSource(param);

        }

        public void TransVariationPrint()
        {
            DataTable glb_Variation = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Clear();
            glb_Variation.Clear();

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

            glb_Variation = bsObj.CHNLSVC.Financial.get_Trans_Variation(BaseCls.GlbReportCompCode, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, "P", BaseCls.GlbUserID, BaseCls.GlbReportDocType);

            _Variation.Database.Tables["GLB_VARIATION"].SetDataSource(glb_Variation);
            _Variation.Database.Tables["param"].SetDataSource(param);

        }

        public void RemSummaryPrint()
        {
            DataTable gnt_rem_sum = new DataTable();
            DataTable rtn_chq = new DataTable();
            DataTable dtNonPost = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            gnt_rem_sum.Clear();
            param.Clear();
            rtn_chq.Clear();
            dtNonPost.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("RemToBeBanked", typeof(Decimal));
            param.Columns.Add("CIH", typeof(Decimal));
            param.Columns.Add("CIHFinal", typeof(Decimal));
            param.Columns.Add("RemToBeBankedFinal", typeof(Decimal));
            param.Columns.Add("TotRemitance", typeof(Decimal));
            param.Columns.Add("TotRemitanceFinal", typeof(Decimal));
            param.Columns.Add("CommWithdrawn", typeof(Decimal));
            param.Columns.Add("CommWithdrawnFinal", typeof(Decimal));
            param.Columns.Add("status", typeof(string));
            param.Columns.Add("fin_status", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate.Date;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["RemToBeBanked"] = BaseCls.GlbRemToBeBanked;
            dr["CIH"] = BaseCls.GlbCIH;
            dr["CIHFinal"] = BaseCls.GlbCIHFinal;
            dr["RemToBeBankedFinal"] = BaseCls.GlbRemToBeBankedFinal;
            dr["TotRemitance"] = BaseCls.GlbTotRemitance;
            dr["TotRemitanceFinal"] = BaseCls.GlbTotRemitanceFinal;
            dr["CommWithdrawn"] = BaseCls.GlbCommWithdrawn;
            dr["CommWithdrawnFinal"] = BaseCls.GlbCommWithdrawnFinal;
            dr["status"] = BaseCls.GlbStatus;
            dr["fin_status"] = BaseCls.GlbReportStrStatus;
            param.Rows.Add(dr);

            if (BaseCls.GlbReportName == "Remitance_Sum.rpt")
            {
                gnt_rem_sum = bsObj.CHNLSVC.Financial.get_Rem_Sum_Rep(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
                dtNonPost = bsObj.CHNLSVC.Financial.get_non_post_txns(BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);
                _RemSum.Database.Tables["NON_POST"].SetDataSource(dtNonPost);
            }
            else
            {
                MasterCompany mst_com = bsObj.CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
                gnt_rem_sum = bsObj.CHNLSVC.Financial.get_Rem_Sum_Rep_View_dt_range(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, (BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, BaseCls.GlbReportProfit, BaseCls.GlbUserID, "ALL", 0, mst_com.Mc_anal24);
            }

            rtn_chq = bsObj.CHNLSVC.Financial.get_Rtn_Chq(BaseCls.GlbReportProfit, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);


            _RemSum.Database.Tables["GNT_REM_SUM"].SetDataSource(gnt_rem_sum);
            _RemSum.Database.Tables["param"].SetDataSource(param);
            _RemSum.Database.Tables["RTN_CHQ"].SetDataSource(rtn_chq);

        }

        public void RemitanceDetPrint()
        {
            DataTable _remDet = new DataTable();
            DataTable param = new DataTable();
            DataTable _Receipt = new DataTable();
            DataRow dr;

            param.Clear();
            _remDet.Clear();
            _Receipt.Clear();


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

            // _remDet = bsObj.CHNLSVC.Financial.ProcessRemDetail(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, BaseCls.GlbReportProfit, BaseCls.GlbUserID);
            // _Receipt = bsObj.CHNLSVC.Sales.Get_Receipts_Rem_Det(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate));

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_remDet = new DataTable();

                    tmp_remDet = bsObj.CHNLSVC.Financial.ProcessRemDetail(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID);

                    _remDet.Merge(tmp_remDet);

                }
            }

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_Receipt = new DataTable();

                    tmp_Receipt = bsObj.CHNLSVC.MsgPortal.Get_Receipts_Rem_Det(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate));
                    //tmp_Receipt = bsObj.CHNLSVC.Sales.Get_Receipts_Rem_Det(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate));

                    _Receipt.Merge(tmp_Receipt);

                }
            }

            _remit_det.Database.Tables["TEMP_REM_DET"].SetDataSource(_remDet);
            _remit_det.Database.Tables["param"].SetDataSource(param);

            foreach (object repOp in _remit_det.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Adv_Rec")
                    {
                        ReportDocument subRepDoc = _remit_det.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SAT_RECEIPT"].SetDataSource(_Receipt);
                    }
                }
            }
            // _remit_det.Database.Tables["SAT_RECEIPT"].SetDataSource(_Receipt);

        }

        public void SOSPrint()
        {
            DataTable sos = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;

            param.Clear();
            sos.Clear();


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

            sos = bsObj.CHNLSVC.Financial.ProcessSOS(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, BaseCls.GlbReportProfit, BaseCls.GlbUserID);

            _sos.Database.Tables["TEMP_SOS"].SetDataSource(sos);
            _sos.Database.Tables["param"].SetDataSource(param);

        }

        public void ReceiptListingPrint()
        {
            DataTable glob_Rec_List = new DataTable();
            DataTable param = new DataTable();
            DataTable Veh_Reg_Txn = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            param.Clear();
            glob_Rec_List.Clear();
            Veh_Reg_Txn.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("withtime", typeof(Int16));


            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["withtime"] = BaseCls.GlbReportnoofDays;
            param.Rows.Add(dr);
            DateTime _filterfrom = Convert.ToDateTime("01/01/0001 00:00:00");
            DateTime _filterto = Convert.ToDateTime("01/01/0001 00:00:00");
            if (BaseCls.GlbReportnoofDays == 1)
            {
                _filterfrom = BaseCls.GlbReportFromDate;
                _filterto = BaseCls.GlbReportToDate;
            }
            // glob_Rec_List = bsObj.CHNLSVC.Financial.ProcessReceipt_Listing_win(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbRecType, BaseCls.GlbPrefix,BaseCls.GlbPayType);
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_Rec_List = new DataTable();
                    tmp_Rec_List = bsObj.CHNLSVC.Financial.ProcessReceipt_Listing_win(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbRecType, BaseCls.GlbPrefix, BaseCls.GlbPayType, BaseCls.GlbReportnoofDays, _filterfrom, _filterto);
                    //tmp_Rec_List = bsObj.CHNLSVC.Financial.ProcessReceipt_Listing_win(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbRecType, BaseCls.GlbPrefix, BaseCls.GlbPayType, BaseCls.GlbReportnoofDays);
                    //tmp_Rec_List = bsObj.CHNLSVC.Financial.ProcessReceipt_Listing_win(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbRecType, BaseCls.GlbPrefix, BaseCls.GlbPayType, BaseCls.GlbReportnoofDays);
                    glob_Rec_List.Merge(tmp_Rec_List);

                }
            }

            _RecList.Database.Tables["sp_process_receipt_listing"].SetDataSource(glob_Rec_List);
            _RecList.Database.Tables["param"].SetDataSource(param);
            if (BaseCls.GlbReportName == "Receipt_List_summary.rpt")
            {
                _Receipt_List_summary.Database.Tables["sp_process_receipt_listing"].SetDataSource(glob_Rec_List);
                _Receipt_List_summary.Database.Tables["param"].SetDataSource(param);
            }
        }

        public void ExchangeCreditNotesPrint()
        {
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            param.Clear();
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_Rec = new DataTable();
                    tmp_Rec = bsObj.CHNLSVC.MsgPortal.getExchangeCreditNoteDetails(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);
                    GLOB_DataTable.Merge(tmp_Rec);
                }
            }

            _excrnote.Database.Tables["EXCHG_CRNOTE"].SetDataSource(GLOB_DataTable);
            _excrnote.Database.Tables["param"].SetDataSource(param);

        }
        public void DebtorSales_ReceiptsPrint()
        {
            DataTable mst_PC = new DataTable();
            DataTable glob_debt_sale_rec = new DataTable();
            DataTable param = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            DataRow dr;

            mst_PC.Clear();
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            mst_PC.Columns.Add("MPC_CD", typeof(string));
            mst_PC.Columns.Add("MC_DESC", typeof(string));
            mst_PC.Columns.Add("MC_COM", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            param.Rows.Add(dr);


            //  glob_debt_sale_rec = bsObj.CHNLSVC.Financial.Process_Debtor_Sales_Receipts(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, null, BaseCls.GlbUserID);
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_debt_sale_rec = new DataTable();

                    tmp_debt_sale_rec = bsObj.CHNLSVC.Financial.Process_Debtor_Sales_Receipts(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID);
                    glob_debt_sale_rec.Merge(tmp_debt_sale_rec);

                }
            }

            _DebtSalesRec.Database.Tables["param"].SetDataSource(param);
            _DebtSalesRec.Database.Tables["sp_process_Debt_sales_Rec"].SetDataSource(glob_debt_sale_rec);

        }

        public void AgeAnalysisOfDebtorsOutstandingPrint()
        {
            DataTable mst_com = new DataTable();
            DataTable glob_debt_age = new DataTable();
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
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);

            MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

            dr = mst_com.NewRow();
            dr["MC_CD"] = BaseCls.GlbReportComp;   // row["MC_CD"].ToString();
            dr["MC_IT_POWERED"] = _MasterComp.Mc_it_powered.ToString();

            mst_com.Rows.Add(dr);

            // glob_debt_age = bsObj.CHNLSVC.Financial.Process_Age_Anal_Debt_Outstand(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, null, BaseCls.GlbUserID, BaseCls.GlbRecType);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_debt_age = new DataTable();
                    Debug.Print(drow["tpl_pc"].ToString());
                    if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_adv.rpt")
                    {
                        tmp_debt_age = bsObj.CHNLSVC.MsgPortal.GetAgeAnalysisDebotrs(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, "ALL", "0", BaseCls.GlbReportDoc);
                    }
                    else
                    {
                        tmp_debt_age = bsObj.CHNLSVC.Financial.Process_Age_Anal_Debt_Outstand(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbRecType, BaseCls.GlbReportParaLine1);
                    }
                    glob_debt_age.Merge(tmp_debt_age);

                }
            }

            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding.rpt")
            {
                _AgeDebtOuts.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOuts.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOuts.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_PC.rpt")
            {
                _AgeDebtOutsPC.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOutsPC.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOutsPC.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_new.rpt")
            {
                _AgeDebtOuts_new.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOuts_new.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOuts_new.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_PC_new.rpt")
            {
                _AgeDebtOutsPC_new.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOutsPC_new.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOutsPC_new.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_dcn.rpt")
            {
                _AgeDebtOutsdcn.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOutsdcn.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOutsdcn.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_new_dcn.rpt")
            {
                _AgeDebtOuts_newdcn.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOuts_newdcn.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOuts_newdcn.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_adv.rpt")
            {
                _AgeDebtOuts_adv.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOuts_adv.Database.Tables["GLOB_DEBT_AGE"].SetDataSource(glob_debt_age);
                _AgeDebtOuts_adv.Database.Tables["param"].SetDataSource(param);
            }
        }

        public void AgeAnalysisOfDebtorsOutstandingDCNPrint()
        {
            DataTable mst_com = new DataTable();
            DataTable glob_debt_age = new DataTable();
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
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);

            MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

            dr = mst_com.NewRow();
            dr["MC_CD"] = BaseCls.GlbReportComp;   // row["MC_CD"].ToString();
            dr["MC_IT_POWERED"] = _MasterComp.Mc_it_powered.ToString();

            mst_com.Rows.Add(dr);

            // glob_debt_age = bsObj.CHNLSVC.Financial.Process_Age_Anal_Debt_Outstand(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, null, BaseCls.GlbUserID, BaseCls.GlbRecType);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_debt_age = new DataTable();

                    tmp_debt_age = bsObj.CHNLSVC.Financial.Process_Age_Anal_Debt_OutstandDCN(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbRecType, BaseCls.GlbReportParaLine1);
                    glob_debt_age.Merge(tmp_debt_age);

                }
            }

            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding.rpt")
            {
                _AgeDebtOuts.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOuts.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOuts.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_PC.rpt")
            {
                _AgeDebtOutsPC.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOutsPC.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOutsPC.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_new.rpt")
            {
                _AgeDebtOuts_new.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOuts_new.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOuts_new.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_PC_new.rpt")
            {
                _AgeDebtOutsPC_new.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOutsPC_new.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOutsPC_new.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_dcn.rpt")
            {
                _AgeDebtOutsdcn.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOutsdcn.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOutsdcn.Database.Tables["param"].SetDataSource(param);
            }
            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_new_dcn.rpt")
            {
                _AgeDebtOuts_newdcn.Database.Tables["mst_com"].SetDataSource(mst_com);
                _AgeDebtOuts_newdcn.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _AgeDebtOuts_newdcn.Database.Tables["param"].SetDataSource(param);
            }
        }

        //kapila 1/1/13
        public void DebtorSettlementPrint()
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
            dr["user"] = BaseCls.GlbUserID;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            MasterCompany _MasterComp = null;
            _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());
            dr = mst_com.NewRow();
            dr["MC_CD"] = BaseCls.GlbReportCompCode;   // row["MC_CD"].ToString();
            dr["MC_IT_POWERED"] = _MasterComp.Mc_it_powered.ToString();
            mst_com.Rows.Add(dr);

            //  glob_debt_sett = bsObj.CHNLSVC.Financial.Process_Debtor_Sales_Settlement(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportCompCode, null, BaseCls.GlbUserID, BaseCls.GlbReportOutsOnly);
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_debt_sett = new DataTable();

                    tmp_debt_sett = bsObj.CHNLSVC.Financial.Process_Debtor_Sales_Settlement(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportAsAtDate), BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportDoc, BaseCls.GlbReportOutsOnly, BaseCls.GlbReportCustomerCode);
                    glob_debt_sett.Merge(tmp_debt_sett);

                }
            }

            if (BaseCls.GlbReportName == "DebtorSettlement.rpt")
            {
                _DebtSett.Database.Tables["mst_com"].SetDataSource(mst_com);
                _DebtSett.Database.Tables["param"].SetDataSource(param);
                _DebtSett.Database.Tables["sp_process_Debt_Settlements"].SetDataSource(glob_debt_sett);
            }
            if (BaseCls.GlbReportName == "DebtorSettlement_PC.rpt")
            {
                _DebtSettPC.Database.Tables["mst_com"].SetDataSource(mst_com);
                _DebtSettPC.Database.Tables["param"].SetDataSource(param);
                _DebtSettPC.Database.Tables["sp_process_Debt_Settlements"].SetDataSource(glob_debt_sett);
            }
            if (BaseCls.GlbReportName == "DebtorSettlement_Outs_PC.rpt")
            {
                _DebtSettOutPC.Database.Tables["mst_com"].SetDataSource(mst_com);
                _DebtSettOutPC.Database.Tables["param"].SetDataSource(param);
                _DebtSettOutPC.Database.Tables["sp_process_Debt_Settlements"].SetDataSource(glob_debt_sett);
            }
            if (BaseCls.GlbReportName == "DebtorSettlement_Outs.rpt")
            {
                _DebtSettOuts.Database.Tables["mst_com"].SetDataSource(mst_com);
                _DebtSettOuts.Database.Tables["param"].SetDataSource(param);
                _DebtSettOuts.Database.Tables["sp_process_Debt_Settlements"].SetDataSource(glob_debt_sett);
            }
            if (BaseCls.GlbReportName == "DebtorSettlement_Outs_PC_Meeting.rpt")
            {
                _DebtSettOutPCMeeting.Database.Tables["mst_com"].SetDataSource(mst_com);
                _DebtSettOutPCMeeting.Database.Tables["param"].SetDataSource(param);
                _DebtSettOutPCMeeting.Database.Tables["sp_process_Debt_Settlements"].SetDataSource(glob_debt_sett);
            }
            if (BaseCls.GlbReportName == "DebtorSettlement_Outs_PC_with_comm.rpt")
            {
                _DebtSettOutPCWithComm.Database.Tables["mst_com"].SetDataSource(mst_com);
                _DebtSettOutPCWithComm.Database.Tables["param"].SetDataSource(param);
                _DebtSettOutPCWithComm.Database.Tables["sp_process_Debt_Settlements"].SetDataSource(glob_debt_sett);
            }

        }

        public void SalesSummaryReport()
        {// Sanjeewa 31-01-13
            DataTable param = new DataTable();
            DataRow dr;

            DataTable SAT_COMM = new DataTable();
            DataTable INT_BATCH = new DataTable();
            DataTable HPT_ACC_LOG = new DataTable();

            DataTable CASH_SALES = bsObj.CHNLSVC.MsgPortal.GetCashSalesSummaryDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocSubType, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType);
            SAT_COMM = bsObj.CHNLSVC.MsgPortal.GetCashSalesCommissionDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocSubType, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType);
            INT_BATCH = bsObj.CHNLSVC.MsgPortal.GetCashSalesDODetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocSubType, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType);
            HPT_ACC_LOG = bsObj.CHNLSVC.MsgPortal.GetCashSalesAccountDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocSubType, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("doctype", typeof(string));
            param.Columns.Add("docsubtype", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["doctype"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["docsubtype"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocSubType;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);

            _cashSalesrpt.Database.Tables["CASH_SALES"].SetDataSource(CASH_SALES);
            _cashSalesrpt.Database.Tables["param"].SetDataSource(param);

            foreach (object repOp in _cashSalesrpt.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "CommDetails")
                    {
                        ReportDocument subRepDoc = _cashSalesrpt.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SAT_COMM"].SetDataSource(SAT_COMM);
                    }
                    if (_cs.SubreportName == "GrandTotComm")
                    {
                        ReportDocument subRepDoc = _cashSalesrpt.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SAT_COMM"].SetDataSource(SAT_COMM);
                    }
                    if (_cs.SubreportName == "PCTotComm")
                    {
                        ReportDocument subRepDoc = _cashSalesrpt.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SAT_COMM"].SetDataSource(SAT_COMM);
                    }
                    if (_cs.SubreportName == "DoDetails")
                    {
                        ReportDocument subRepDoc = _cashSalesrpt.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["INT_BATCH"].SetDataSource(INT_BATCH);
                    }
                    if (_cs.SubreportName == "Account_Details")
                    {
                        ReportDocument subRepDoc = _cashSalesrpt.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["HPT_ACC_LOG"].SetDataSource(HPT_ACC_LOG);
                    }
                    if (_cs.SubreportName == "InvComm")
                    {
                        ReportDocument subRepDoc = _cashSalesrpt.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SAT_COMM"].SetDataSource(SAT_COMM);
                    }
                }
            }
        }

        public void HireSalesSummaryReport()
        {// Sanjeewa 12-02-2013
            DataTable param = new DataTable();
            DataRow dr;
            DataTable HIRE_SALE = new DataTable();
            DataTable HIRE_SALE_CANCEL = new DataTable();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_debt_sett = new DataTable();

                    tmp_debt_sett = bsObj.CHNLSVC.MsgPortal.GetHireSalesDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocSubType, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportType);
                    HIRE_SALE.Merge(tmp_debt_sett);

                    tmp_debt_sett = new DataTable();
                    tmp_debt_sett = bsObj.CHNLSVC.MsgPortal.GetHireSalesCancelDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, drow["tpl_pc"].ToString());
                    HIRE_SALE_CANCEL.Merge(tmp_debt_sett);

                }
            }

            //  DataTable HIRE_SALE = bsObj.CHNLSVC.Sales.GetHireSalesDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDocSubType, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportType);
            //    DataTable HIRE_SALE_CANCEL = bsObj.CHNLSVC.Sales.GetHireSalesCancelDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID,drow["tpl_pc"].ToString());

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("doctype", typeof(string));
            param.Columns.Add("docsubtype", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["doctype"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["docsubtype"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocSubType;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);

            _HPSalesrpt.Database.Tables["HIRE_SALE"].SetDataSource(HIRE_SALE);
            _HPSalesrpt.Database.Tables["HIRE_SALE_CANCEL"].SetDataSource(HIRE_SALE_CANCEL);
            _HPSalesrpt.Database.Tables["param"].SetDataSource(param);

        }

        public void ForwardSalesReport()
        {// Sanjeewa 28-03-2013
            DataTable param = new DataTable();
            DataRow dr;
            string showCost = "N";

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (BaseCls.GlbReportWithCost == 1)
            {
                showCost = "Y";
            }

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetForwardSalesDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportExeType, BaseCls.GlbReportDiscRate, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), showCost, "", BaseCls.GlbReportCustomerCode);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            //DataTable FORWARD_SALES_REP = bsObj.CHNLSVC.Sales.GetForwardSalesDetails(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3,BaseCls.GlbReportExeType, BaseCls.GlbReportDiscRate);

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
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["doctype"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["docsubtype"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocSubType;
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["age"] = BaseCls.GlbReportExeType + ' ' + BaseCls.GlbReportDiscRate;
            dr["customer"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode;

            param.Rows.Add(dr);

            if (BaseCls.GlbReportType == "N")
            {
                if (BaseCls.GlbReportWithCost == 1)
                {
                    _ForwardSalesrptcost.Database.Tables["PROC_FWD_SALES"].SetDataSource(GLOB_DataTable);
                    _ForwardSalesrptcost.Database.Tables["param"].SetDataSource(param);
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

        }

        public void PriceDetailsReport()
        {// Sanjeewa 23-07-2013
            DataTable param = new DataTable();
            DataTable PRICECOMDTL = new DataTable();
            DataTable PRICELOCDTL = new DataTable();
            DataTable PRICECOMDTL1 = new DataTable();
            DataTable PRICELOCDTL1 = new DataTable();
            DataRow dr;

            DataTable PRICEDTL = bsObj.CHNLSVC.MsgPortal.GetPriceDetails1(BaseCls.GlbReportDoc, BaseCls.GlbReportDirection, BaseCls.GlbPayType, BaseCls.GlbReportPriceBook, BaseCls.GlbReportPBLevel, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbUserID, BaseCls.GlbReportType);

            if (PRICEDTL.Rows.Count > 0)
            {
                foreach (DataRow drow in PRICEDTL.Rows)
                {
                    PRICECOMDTL1 = new DataTable();
                    PRICELOCDTL1 = new DataTable();

                    PRICECOMDTL1 = bsObj.CHNLSVC.MsgPortal.GetPriceCombineDetails1(drow["main_itemcode"].ToString(), Convert.ToInt32(drow["pbseq"].ToString()), Convert.ToInt32(drow["pbline"].ToString()));
                    PRICELOCDTL1 = bsObj.CHNLSVC.MsgPortal.GetPriceLocationDetails1(Convert.ToInt32(drow["pbseq"].ToString()), drow["promocode"].ToString());
                    PRICECOMDTL.Merge(PRICECOMDTL1);
                    PRICELOCDTL.Merge(PRICELOCDTL1);
                }
            }
            else
            {
                PRICECOMDTL = bsObj.CHNLSVC.MsgPortal.GetPriceCombineDetails1("", 0, 0);
                PRICELOCDTL = bsObj.CHNLSVC.MsgPortal.GetPriceLocationDetails1(0, "");
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("asatdate", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("itemcode", typeof(string));
            param.Columns.Add("brand", typeof(string));
            param.Columns.Add("model", typeof(string));
            param.Columns.Add("cat1", typeof(string));
            param.Columns.Add("cat2", typeof(string));
            param.Columns.Add("cat3", typeof(string));
            param.Columns.Add("poweredby", typeof(string));
            param.Columns.Add("company", typeof(string));
            param.Columns.Add("reptp", typeof(string));
            param.Columns.Add("customer", typeof(string));
            param.Columns.Add("pricetype", typeof(string));
            param.Columns.Add("pb", typeof(string));
            param.Columns.Add("pblevel", typeof(string));
            param.Columns.Add("circular", typeof(string));
            param.Columns.Add("promocode", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["asatdate"] = BaseCls.GlbReportAsAtDate.Date;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["itemcode"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["brand"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["model"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["cat1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["cat2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["cat3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["poweredby"] = "";
            dr["company"] = BaseCls.GlbReportComp;
            dr["reptp"] = BaseCls.GlbReportType;
            dr["customer"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode;
            dr["pricetype"] = BaseCls.GlbPayType == "" ? "ALL" : BaseCls.GlbPayType;
            dr["pb"] = BaseCls.GlbReportPriceBook == "" ? "ALL" : BaseCls.GlbReportPriceBook;
            dr["pblevel"] = BaseCls.GlbReportPBLevel == "" ? "ALL" : BaseCls.GlbReportPBLevel;
            dr["circular"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc;
            dr["promocode"] = BaseCls.GlbReportDirection == "" ? "ALL" : BaseCls.GlbReportDirection;
            param.Rows.Add(dr);

            _pricedtl.Database.Tables["GLB_REPPBDETAILS"].SetDataSource(PRICEDTL);
            _pricedtl.Database.Tables["GLB_REPPB_COMBINEITEM"].SetDataSource(PRICECOMDTL);

            foreach (object repOp in _pricedtl.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Locations")
                    {
                        ReportDocument subRepDoc = _pricedtl.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["GLB_REPPB_LOC"].SetDataSource(PRICELOCDTL.DefaultView.ToTable(true));
                    }
                }
            }

            _pricedtl.Database.Tables["param"].SetDataSource(param);

        }

        public void POSDetailReport()
        {// Sanjeewa 01-04-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable POS_DTL = bsObj.CHNLSVC.MsgPortal.GetPOSDetailDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);

            _POSDtlrpt.Database.Tables["POS_DETAIL"].SetDataSource(POS_DTL);
            _POSDtlrpt.Database.Tables["param"].SetDataSource(param);

        }
        public void POAllocationDetailReport()
        {// Sanjeewa 10-02-2016            

            DataTable POS_DTL = bsObj.CHNLSVC.Sales.getPOAllocation(BaseCls.GlbReportPurchaseOrder);

            _poalloc.Database.Tables["PO_ALLOC"].SetDataSource(POS_DTL);

        }
        public void SalesFiguresReport()
        {// Sanjeewa 03-03-2013
            DataTable param = new DataTable();
            DataRow dr;
            decimal PcTot = 0;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    PcTot = 0;
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetSalesFiguresDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, drow["tpl_pc"].ToString());
                    if (TMP_DataTable.Rows.Count > 0)
                    {
                        foreach (DataRow drow1 in TMP_DataTable.Rows)
                        {
                            PcTot = PcTot + Convert.ToDecimal(drow1["TOT_AMT"].ToString());
                        }
                    }
                    TMP_DataTable.AsEnumerable().ToList().ForEach(x => x.SetField<decimal>("PC_TOTAMT", PcTot));

                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            //DataTable VIW_SALESFIGURES = bsObj.CHNLSVC.Sales.GetSalesFiguresDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);

            if (BaseCls.GlbReportDocType == "Y")
            {
                _SalesFigOrdrpt.Database.Tables["VIW_SALESFIGURES"].SetDataSource(GLOB_DataTable);
                _SalesFigOrdrpt.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _SalesFigrpt.Database.Tables["VIW_SALESFIGURES"].SetDataSource(GLOB_DataTable);
                _SalesFigrpt.Database.Tables["param"].SetDataSource(param);
            }

        }

        public void A_SalesReport()
        {// Sanjeewa 10-03-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetASalesDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());

                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = BaseCls.GlbReportAsAtDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);

            _ASales.Database.Tables["A_SALES"].SetDataSource(GLOB_DataTable);
            _ASales.Database.Tables["param"].SetDataSource(param);

        }

        public void LoyalityDiscountReport()
        {// Sanjeewa 20-03-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetLoyalityDiscountDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbRccType, BaseCls.GlbReportCusId, BaseCls.GlbUserID);

                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("loytp", typeof(string));
            param.Columns.Add("custtp", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["loytp"] = BaseCls.GlbRccType == "" ? "ALL" : BaseCls.GlbRccType;
            dr["custtp"] = BaseCls.GlbReportCusId == "" ? "ALL" : BaseCls.GlbReportCusId;
            param.Rows.Add(dr);

            _LDisc.Database.Tables["LOY_DISC"].SetDataSource(GLOB_DataTable);
            _LDisc.Database.Tables["param"].SetDataSource(param);

        }

        public void GetSalesFiguresDetailsWithTax()
        {// Nadeeka 28-02-2014
            DataTable param = new DataTable();
            DataRow dr;
            DataRow dr1;
            DataTable PROC_RVT_N_RLS = new DataTable();
            DataTable tmp_RVT_N_RLS1 = new DataTable();

            tmp_RVT_N_RLS1.Clear();

            tmp_RVT_N_RLS1.Columns.Add("DOC_TYPE", typeof(string));
            tmp_RVT_N_RLS1.Columns.Add("PROFIT_CENTER", typeof(string));
            tmp_RVT_N_RLS1.Columns.Add("ACC_NO", typeof(string));
            tmp_RVT_N_RLS1.Columns.Add("RVT_CAPITAL", typeof(Decimal));
            tmp_RVT_N_RLS1.Columns.Add("PC_DESC", typeof(string));
            tmp_RVT_N_RLS1.Columns.Add("RVT_REFNO", typeof(string));



            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();

                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetSalesFiguresDetailsWithTax(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, drow["tpl_pc"].ToString(), BaseCls.GlbUserComCode);

                    GLOB_DataTable.Merge(TMP_DataTable);

                    DataTable tmp_RVT_N_RLS = new DataTable();

                    tmp_RVT_N_RLS = bsObj.CHNLSVC.MsgPortal.GetRevertNReleaseDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate), Convert.ToDateTime(BaseCls.GlbReportToDate), BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID);

                    if (tmp_RVT_N_RLS.Rows.Count == 0)
                    {
                        dr1 = tmp_RVT_N_RLS1.NewRow();
                        dr1["DOC_TYPE"] = "N/A";
                        dr1["PROFIT_CENTER"] = drow["tpl_pc"].ToString();
                        dr1["ACC_NO"] = "N/A";
                        dr1["RVT_CAPITAL"] = 0;
                        dr1["PC_DESC"] = "N/A";
                        dr1["RVT_REFNO"] = "N/A";
                        tmp_RVT_N_RLS1.Rows.Add(dr1);
                        PROC_RVT_N_RLS.Merge(tmp_RVT_N_RLS1);
                    }
                    else
                    {
                        PROC_RVT_N_RLS.Merge(tmp_RVT_N_RLS);
                    }
                }
            }


            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);


            _salesTaxSch.Database.Tables["VIW_SALESFIGURES"].SetDataSource(GLOB_DataTable);
            _salesTaxSch.Database.Tables["param"].SetDataSource(param);


            foreach (object repOp in _salesTaxSch.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "revert")
                    {
                        ReportDocument subRepDoc = _salesTaxSch.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["PROC_RVT_N_RLS"].SetDataSource(PROC_RVT_N_RLS);



                    }
                }
            }


        }
        public void TotalRevenueReport()
        {// Sanjeewa 03-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TOT_REV = bsObj.CHNLSVC.MsgPortal.GetTotalRevenueDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TOT_REV);
                }
            }
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);

            _totrev.Database.Tables["TOT_REVENUE"].SetDataSource(GLOB_DataTable);
            _totrev.Database.Tables["param"].SetDataSource(param);

        }

        public void PaymodewiseTrReport()
        {// Sanjeewa 09-10-2013
            DataTable param = new DataTable();
            DataRow dr;
            DataTable PAY_MODE = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);
            if (BaseCls.GlbReportDocType == "AMEND")
            {
                PAY_MODE = bsObj.CHNLSVC.MsgPortal.GetPaymodeAmendDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbPayType, BaseCls.GlbUserID, BaseCls.GlbReportIsExport);
            }
            else
            {
                if (tmp_user_pc.Rows.Count > 0)
                {
                    foreach (DataRow drow in tmp_user_pc.Rows)
                    {
                        DataTable TOT_REV = bsObj.CHNLSVC.MsgPortal.GetPaymodeDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbPayType, BaseCls.GlbUserID, BaseCls.GlbReportIsExport, drow["tpl_pc"].ToString(), BaseCls.GlbUserComCode);
                        PAY_MODE.Merge(TOT_REV);
                    }
                }
            }
            //if (BaseCls.GlbReportDocType == "AMEND")
            //    PAY_MODE = bsObj.CHNLSVC.MsgPortal.GetPaymodeAmendDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbPayType, BaseCls.GlbUserID, BaseCls.GlbReportIsExport);
            //else
            //    PAY_MODE = bsObj.CHNLSVC.MsgPortal.GetPaymodeDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbPayType, BaseCls.GlbUserID, BaseCls.GlbReportIsExport);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("paymode", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["paymode"] = BaseCls.GlbPayType;
            param.Rows.Add(dr);

            _pmodewise.Database.Tables["PAYMODE_DTL"].SetDataSource(PAY_MODE);
            _pmodewise.Database.Tables["param"].SetDataSource(param);

        }

        public void NotRegVehiclesReport()
        {// Sanjeewa 20-06-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable NOTREGVEH = bsObj.CHNLSVC.MsgPortal.GetNotRegVehDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

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

            _NotRegVeh.Database.Tables["NOT_REG_VEH"].SetDataSource(NOTREGVEH);
            _NotRegVeh.Database.Tables["param"].SetDataSource(param);
        }

        public void Warr_Rpl_CRNoteReport()
        {// Sanjeewa 24-06-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable Warr_Rpl_CRNote = bsObj.CHNLSVC.MsgPortal.GetBalanceWarrantyClaimCRNoteDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbUserID);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("dealer", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["dealer"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode;
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            _Warr_Rpl_CRNote.Database.Tables["WR_RPL_CRNOTE"].SetDataSource(Warr_Rpl_CRNote);
            _Warr_Rpl_CRNote.Database.Tables["param"].SetDataSource(param);
        }

        public void SalesPromoAchievementReport()
        {// Sanjeewa 01-07-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable PROMOACHIEVE = bsObj.CHNLSVC.MsgPortal.GetSalesPromoAchievementDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportDoc, BaseCls.GlbReportDirection, BaseCls.GlbUserID);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("Circular", typeof(string));
            param.Columns.Add("Scheme", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["Circular"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc;
            dr["Scheme"] = BaseCls.GlbReportDirection == "" ? "ALL" : BaseCls.GlbReportDirection;
            param.Rows.Add(dr);

            _SalePromoArchieve.Database.Tables["PROMOACHIEVE"].SetDataSource(PROMOACHIEVE);
            _SalePromoArchieve.Database.Tables["param"].SetDataSource(param);
        }

        public void QuotationPrintReport()
        {// Sanjeewa 12-06-2013
            DataTable QUO_DTL = bsObj.CHNLSVC.MsgPortal.GetQuotationPrintDetails(BaseCls.GlbReportDoc);
            DataTable QUO_WARR_DTL = bsObj.CHNLSVC.MsgPortal.GetQuotationWarrantyPrintDetails(BaseCls.GlbReportComp);

            _QuoPrint.Database.Tables["QUO_DTL"].SetDataSource(QUO_DTL);
            _QuoPrint.Database.Tables["QUO_WARR_DTL"].SetDataSource(QUO_WARR_DTL);
        }

        public void ExecutivewiseSalesInvoiceReport()
        {// Sanjeewa 31-05-2013 modified by nadeeka  07-mar-2014
            DataTable param = new DataTable();
            DataRow dr;
            DataTable EXEC_SALE_INVOICE = new DataTable();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_HP_CLS_BAL = new DataTable();
                    TMP_HP_CLS_BAL = bsObj.CHNLSVC.MsgPortal.GetExecwiseSalesInvDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportDiscRate, BaseCls.GlbReportDiscTp, BaseCls.GlbReportIsDelivered, BaseCls.GlbReportExeType, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportAppBy);
                    EXEC_SALE_INVOICE.Merge(TMP_HP_CLS_BAL);

                }
            }
            //  EXEC_SALE_INVOICE = bsObj.CHNLSVC.Sales.GetExecwiseSalesInvDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportDiscRate, BaseCls.GlbReportDiscTp, BaseCls.GlbReportIsDelivered, BaseCls.GlbReportExeType );

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("discrate", typeof(Decimal));
            param.Columns.Add("disctp", typeof(Decimal));
            param.Columns.Add("isdeliver", typeof(Decimal));
            param.Columns.Add("ExeType", typeof(string));
            param.Columns.Add("allComm", typeof(Decimal));
            param.Columns.Add("appby", typeof(string));

            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);
            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["discrate"] = BaseCls.GlbReportDiscRate;
            dr["disctp"] = BaseCls.GlbReportDiscTp;
            dr["isdeliver"] = BaseCls.GlbReportIsDelivered;
            dr["ExeType"] = BaseCls.GlbReportExeType;
            dr["allComm"] = BaseCls.GlbReportAllowCommision;
            dr["appby"] = BaseCls.GlbReportAppBy;

            param.Rows.Add(dr);

            _ExecSaleInvoice.Database.Tables["EXEC_SALE_INVOICE"].SetDataSource(EXEC_SALE_INVOICE);
            _ExecSaleInvoice.Database.Tables["param"].SetDataSource(param);
            _ExecSaleInvoice.Database.Tables["MST_COM"].SetDataSource(MST_COM);
        }
        public void StampDutyReport()
        {// Sanjeewa 07-03-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable STAMP_DUTY = bsObj.CHNLSVC.MsgPortal.GetStampDutyDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);

            _Stamp_Duty.Database.Tables["STAMP_DUTY"].SetDataSource(STAMP_DUTY);
            _Stamp_Duty.Database.Tables["param"].SetDataSource(param);

        }

        public void SVATReport()
        {// Sanjeewa 07-03-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable SVAT = bsObj.CHNLSVC.MsgPortal.GetSVATDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportDocType, BaseCls.GlbReportDocSubType);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("inv_type", typeof(string));
            param.Columns.Add("inv_subtype", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            if (BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") == BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy"))
            {
                dr["period"] = "Date : " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + "12 AM To 11.59 PM ";
            }
            else
            {
                dr["period"] = "Period : FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            }
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["inv_type"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["inv_subtype"] = BaseCls.GlbReportDocSubType == "" ? "ALL" : BaseCls.GlbReportDocSubType;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);

            _SVAT.Database.Tables["SVAT"].SetDataSource(SVAT);
            _SVAT.Database.Tables["param"].SetDataSource(param);

        }

        public void DFSalesStatementReport()
        {// Sanjeewa 30-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable GLB_DF_SALES = bsObj.CHNLSVC.MsgPortal.DFSalesDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
            DataTable DF_RECEIPTS = bsObj.CHNLSVC.MsgPortal.DFSalesReceiptDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            if (BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") == BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy"))
            {
                dr["period"] = "Date : " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " 12 AM To 11.59 PM ";
            }
            else
            {
                dr["period"] = "Period : FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            }
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            _DFSaleSt.Database.Tables["GLB_DF_SALES"].SetDataSource(GLB_DF_SALES);
            _DFSaleSt.Database.Tables["param"].SetDataSource(param);

            foreach (object repOp in _DFSaleSt.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Paymodes")
                    {
                        ReportDocument subRepDoc = _DFSaleSt.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DF_RECEIPTS"].SetDataSource(DF_RECEIPTS);
                    }
                }
            }
        }

        public void DFSalesStatementCurrencyReport()
        {// Sanjeewa 31-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable GLB_DF_SALES = bsObj.CHNLSVC.MsgPortal.DFSalesDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
            DataTable DF_RECEIPTS = bsObj.CHNLSVC.MsgPortal.DFSalesReceiptDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
            DataTable DF_RECEIPTS_CURR = bsObj.CHNLSVC.MsgPortal.DFSalesReceiptCurrDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            //_DFCurrSale.Database.Tables["GLB_DF_SALES"].SetDataSource(GLB_DF_SALES);
            _DFCurrSale.Database.Tables["DF_RECEIPTS_CURR"].SetDataSource(DF_RECEIPTS_CURR);
            _DFCurrSale.Database.Tables["param"].SetDataSource(param);

            foreach (object repOp in _DFCurrSale.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Paymodes")
                    {
                        ReportDocument subRepDoc = _DFCurrSale.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DF_RECEIPTS"].SetDataSource(DF_RECEIPTS);
                    }
                }
            }
        }

        public void DFSalesStatementCurrencyDtlReport()
        {// Sanjeewa 31-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable GLB_DF_SALES = bsObj.CHNLSVC.MsgPortal.DFSalesDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
            DataTable DF_RECEIPTS = bsObj.CHNLSVC.MsgPortal.DFSalesReceiptDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
            DataTable DF_RECEIPTS_CURR = bsObj.CHNLSVC.MsgPortal.DFSalesReceiptCurrDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            //_DFCurrSaleDtl.Database.Tables["GLB_DF_SALES"].SetDataSource(GLB_DF_SALES);
            _DFCurrSaleDtl.Database.Tables["DF_RECEIPTS_CURR"].SetDataSource(DF_RECEIPTS_CURR);
            _DFCurrSaleDtl.Database.Tables["param"].SetDataSource(param);

            foreach (object repOp in _DFCurrSaleDtl.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "Paymodes")
                    {
                        ReportDocument subRepDoc = _DFCurrSaleDtl.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["DF_RECEIPTS"].SetDataSource(DF_RECEIPTS);
                    }
                }
            }
        }

        public void DFSalesStatementCurrencyTrReport()
        {// Sanjeewa 26-12-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable DF_RECEIPTS_CURR = bsObj.CHNLSVC.MsgPortal.DFSalesReceiptCurrTrDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            _DFCurrTr.Database.Tables["DF_RECEIPTS_CURR"].SetDataSource(DF_RECEIPTS_CURR);
            _DFCurrTr.Database.Tables["param"].SetDataSource(param);

        }

        public void DFConsolidatedSalesStatementReport()
        {// Sanjeewa 30-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable GLB_DF_SALES = bsObj.CHNLSVC.MsgPortal.DFSalesDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            _DFSaleConsolidate.Database.Tables["GLB_DF_SALES"].SetDataSource(GLB_DF_SALES);
            _DFSaleConsolidate.Database.Tables["param"].SetDataSource(param);

        }
        public void DFSalesModelwise()
        {// Nadeeka 07-05-2014
            DataTable param = new DataTable();
            DataRow dr;

            DataTable GLB_DF_SALES = bsObj.CHNLSVC.MsgPortal.DFSalesDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            _DFSaleModel.Database.Tables["GLB_DF_SALES"].SetDataSource(GLB_DF_SALES);
            _DFSaleModel.Database.Tables["param"].SetDataSource(param);

        }


        public void DFSaleswithQtyReport()
        {// Sanjeewa 01-11-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable GLB_DF_SALES = bsObj.CHNLSVC.MsgPortal.DFSalesDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            _DFSaleQty.Database.Tables["GLB_DF_SALES"].SetDataSource(GLB_DF_SALES);
            _DFSaleQty.Database.Tables["param"].SetDataSource(param);

        }

        public void DFSalesQtyReport()
        {// Sanjeewa 04-11-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable GLB_DF_SALES = bsObj.CHNLSVC.MsgPortal.DFSalesDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            _DFSQty.Database.Tables["GLB_DF_SALES"].SetDataSource(GLB_DF_SALES);
            _DFSQty.Database.Tables["param"].SetDataSource(param);

        }
        public void DFMonthSalesReport(int repid)
        {// Nadeeka 06-11-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable GLB_DF_SALES = bsObj.CHNLSVC.MsgPortal.DFSalesDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
            DataTable GLB_DF_SALES1 = new DataTable();
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            if (repid == 1)
            {
                _DFSMonSls.Database.Tables["GLB_DF_SALES"].SetDataSource(GLB_DF_SALES);
                _DFSMonSls.Database.Tables["param"].SetDataSource(param);
            }
            if (repid == 2)
            {
                _DFSWeekSls.Database.Tables["GLB_DF_SALES"].SetDataSource(GLB_DF_SALES);
                _DFSWeekSls.Database.Tables["param"].SetDataSource(param);

                DataRow[] dr2 = GLB_DF_SALES.Select("(INV_STATUS <>'C') ");
                if (dr2.Count() > 0)
                {
                    GLB_DF_SALES1 = GLB_DF_SALES.Select("(INV_STATUS <>'C') ").CopyToDataTable();
                    _DFSWeekSls.Database.Tables["GLB_DF_SALES"].SetDataSource(GLB_DF_SALES1);
                    _DFSWeekSls.Database.Tables["param"].SetDataSource(param);
                }





            }
            if (repid == 3)
            {
                _DFSCatSls.Database.Tables["GLB_DF_SALES"].SetDataSource(GLB_DF_SALES);
                _DFSCatSls.Database.Tables["param"].SetDataSource(param);
            }

        }
        public void DFCategorywiseSalesReport()
        {// Sanjeewa 31-10-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable GLB_DF_SALES = bsObj.CHNLSVC.MsgPortal.DFSalesDetails(BaseCls.GlbUserID, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["profitcenter"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            param.Rows.Add(dr);

            _DFCatSale.Database.Tables["GLB_DF_SALES"].SetDataSource(GLB_DF_SALES);
            _DFCatSale.Database.Tables["param"].SetDataSource(param);

        }


        public void EliteCommissionReport()
        {// Sanjeewa 19-06-2013
            DataTable param = new DataTable();
            DataRow dr;

            DataTable ECOMM = bsObj.CHNLSVC.MsgPortal.GetEliteCommissionDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID);

            param.Clear();

            param.Columns.Add("PARA_USER", typeof(string));
            param.Columns.Add("PARA_PERIOD", typeof(string));
            param.Columns.Add("PARA_PCENTER", typeof(string));
            param.Columns.Add("PARA_HEADING", typeof(string));

            dr = param.NewRow();
            dr["PARA_USER"] = BaseCls.GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["PARA_HEADING"] = BaseCls.GlbReportHeading;
            param.Rows.Add(dr);

            if (BaseCls.GlbReportName == "Elite_Commission_Details.rpt")
            {
                _ECommPrint.Database.Tables["GLB_REP_SALES"].SetDataSource(ECOMM);
                _ECommPrint.Database.Tables["REP_PARA"].SetDataSource(param);
            }
            else
            {
                _ECommSumm.Database.Tables["GLB_REP_SALES"].SetDataSource(ECOMM);
                _ECommSumm.Database.Tables["REP_PARA"].SetDataSource(param);
            }
        }

        public void StockSalesReport()
        {// Sanjeewa 17-12-2015
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetStockSalesDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, drow["tpl_pc"].ToString(), BaseCls.GlbUserComCode, BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFast, BaseCls.GlbReportSupplier);
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
            param.Columns.Add("PARA_SUPPLIER", typeof(string));

            dr = param.NewRow();
            dr["PARA_USER"] = BaseCls.GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["PARA_CUST"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode;
            dr["PARA_EXEC"] = BaseCls.GlbReportExecCode == "" ? "ALL" : BaseCls.GlbReportExecCode;
            dr["PARA_DOCTYPE"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["PARA_ITCODE"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["PARA_BRAND"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["PARA_MODEL"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["PARA_CAT1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["PARA_CAT2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["PARA_CAT3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["PARA_CAT4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4;
            dr["PARA_CAT5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5;
            dr["PARA_STKTYPE"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["PARA_INVNO"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc;
            dr["PARA_PROMOTOR"] = BaseCls.GlbReportPromotor == "" ? "ALL" : BaseCls.GlbReportPromotor;
            dr["PARA_HEADING"] = BaseCls.GlbReportHeading;
            dr["PARA_FREE_ISS_TP"] = BaseCls.GlbReportIsFast;
            dr["PARA_SUPPLIER"] = BaseCls.GlbReportSupplier == "" ? "ALL" : BaseCls.GlbReportSupplier;

            param.Rows.Add(dr);

            if (BaseCls.GlbReportType == "DTL")
            {
                _stocksale.Database.Tables["STOCK_SALE"].SetDataSource(GLOB_DataTable);
                _stocksale.Database.Tables["REP_PARA"].SetDataSource(param);
            }
            else
            {
                _stocksalesum.Database.Tables["STOCK_SALE"].SetDataSource(GLOB_DataTable);
                _stocksalesum.Database.Tables["REP_PARA"].SetDataSource(param);

            }
        }

        public void DeliveredSalesReport()
        {// Sanjeewa 31-01-13
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    //updated by akila 2018/03/16
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetDeliveredSalesDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, drow["tpl_pc"].ToString(), BaseCls.GlbUserComCode, BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFast, BaseCls.GlbReportParaLine1, 2, BaseCls.GlbReportDoc1, BaseCls.GlbReportDoc2, BaseCls.GlbReportCountry, BaseCls.GlbReportProvince, BaseCls.GlbReportDistrict, BaseCls.GlbReportCity);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            //DataTable PROC_DELIVERED_SALES = bsObj.CHNLSVC.Sales.GetDeliveredSalesDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc);

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
            param.Columns.Add("GRP_COLOR", typeof(int));
            param.Columns.Add("GRP_SIZE", typeof(int));

            param.Columns.Add("color", typeof(string));
            param.Columns.Add("size", typeof(string));

            //Add by akila 2018/03/16
            param.Columns.Add("GRP_NATIONALITY", typeof(int));
            param.Columns.Add("GRP_DISTRICT", typeof(int));
            param.Columns.Add("GRP_PROVINCE", typeof(int));
            param.Columns.Add("GRP_CITY", typeof(int));

            dr = param.NewRow();
            dr["PARA_USER"] = BaseCls.GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["PARA_CUST"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode;
            dr["PARA_EXEC"] = BaseCls.GlbReportExecCode == "" ? "ALL" : BaseCls.GlbReportExecCode;
            dr["PARA_DOCTYPE"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["PARA_ITCODE"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["PARA_BRAND"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["PARA_MODEL"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["PARA_CAT1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["PARA_CAT2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["PARA_CAT3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["PARA_CAT4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4;
            dr["PARA_CAT5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5;
            dr["PARA_STKTYPE"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["PARA_INVNO"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc;
            dr["PARA_PROMOTOR"] = BaseCls.GlbReportPromotor == "" ? "ALL" : BaseCls.GlbReportPromotor;
            dr["PARA_HEADING"] = BaseCls.GlbReportHeading;
            dr["PARA_FREE_ISS_TP"] = BaseCls.GlbReportIsFast;

            dr["GRP_PCENTER"] = BaseCls.GlbReportGroupProfit;
            dr["GRP_DOCTYPE"] = BaseCls.GlbReportGroupDocType;
            dr["GRP_CUST"] = BaseCls.GlbReportGroupCustomerCode;
            dr["GRP_EXEC"] = BaseCls.GlbReportGroupExecCode;
            dr["GRP_ITCODE"] = BaseCls.GlbReportGroupItemCode;
            dr["GRP_BRAND"] = BaseCls.GlbReportGroupBrand;
            dr["GRP_MODEL"] = BaseCls.GlbReportGroupModel;
            dr["GRP_CAT1"] = BaseCls.GlbReportGroupItemCat1;
            dr["GRP_CAT2"] = BaseCls.GlbReportGroupItemCat2;
            dr["GRP_CAT3"] = BaseCls.GlbReportGroupItemCat3;
            dr["GRP_CAT4"] = BaseCls.GlbReportGroupItemCat4;
            dr["GRP_CAT5"] = BaseCls.GlbReportGroupItemCat5;
            dr["GRP_STKTYPE"] = BaseCls.GlbReportGroupItemStatus;
            dr["GRP_INVNO"] = BaseCls.GlbReportGroupInvoiceNo;
            dr["GRP_LAST_GROUP"] = BaseCls.GlbReportGroupLastGroup;
            dr["GRP_LAST_GROUP_CAT"] = BaseCls.GlbReportGroupLastGroupCat;
            dr["GRP_PRMOTOR"] = BaseCls.GlbReportGroupPromotor;
            dr["GRP_DLOC"] = BaseCls.GlbReportGroupDOLoc;
            dr["GRP_JOBNO"] = BaseCls.GlbReportGroupJobNo;
            dr["GRP_COLOR"] = BaseCls.GlbReportGroupColor;
            dr["GRP_SIZE"] = BaseCls.GlbReportGroupSize;

            dr["color"] = BaseCls.GlbReportDoc1 == "" ? "ALL" : BaseCls.GlbReportDoc1;
            dr["size"] = BaseCls.GlbReportDoc2 == "" ? "ALL" : BaseCls.GlbReportDoc2;

            //Add by akila 2018/03/16
            dr["GRP_NATIONALITY"] = BaseCls.GlbReportGroupByNationality;
            dr["GRP_DISTRICT"] = BaseCls.GlbReportGroupByDistrict;
            dr["GRP_PROVINCE"] = BaseCls.GlbReportGroupByProvince;
            dr["GRP_CITY"] = BaseCls.GlbReportGroupByCity;

            param.Rows.Add(dr);

            if (BaseCls.GlbReportName == "DeliveredSalesReport.rpt")
            {
                _delSalesrptPC.Database.Tables["GLB_REP_SALES"].SetDataSource(GLOB_DataTable);
                _delSalesrptPC.Database.Tables["REP_PARA"].SetDataSource(param);
            }
            else if (BaseCls.GlbReportName == "DeliveredSalesReport_withCust.rpt")
            {
                _delSalesrptCust.Database.Tables["GLB_REP_SALES"].SetDataSource(GLOB_DataTable);
                _delSalesrptCust.Database.Tables["REP_PARA"].SetDataSource(param);
            }
            else
            {
                _delSalesrptItem.Database.Tables["GLB_REP_SALES"].SetDataSource(GLOB_DataTable);
                _delSalesrptItem.Database.Tables["REP_PARA"].SetDataSource(param);
            };

        }

        public void GpDetailwithReplacementReport()
        {// Sanjeewa 11-06-2017
            DataTable param = new DataTable();
            DataRow dr;

            GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.GetItemWiseGp_Rpl(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode,
                    BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5,
                    BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, BaseCls.GlbReportCompCode,
                    BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFreeIssue, BaseCls.GlbReportItmClasif, BaseCls.GlbReportBrandMgr, "", true,
                    BaseCls.GlbReportIsFast, BaseCls.GlbReportFromDate2, BaseCls.GlbReportToDate2, BaseCls.GlbReportWithStatus);

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
            dr["PARA_USER"] = BaseCls.GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["PARA_CUST"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode;
            dr["PARA_EXEC"] = BaseCls.GlbReportExecCode == "" ? "ALL" : BaseCls.GlbReportExecCode;
            dr["PARA_DOCTYPE"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["PARA_ITCODE"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["PARA_BRAND"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["PARA_MODEL"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["PARA_CAT1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["PARA_CAT2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["PARA_CAT3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["PARA_CAT4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4;
            dr["PARA_CAT5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5;
            dr["PARA_STKTYPE"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["PARA_INVNO"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc;
            dr["PARA_PROMOTOR"] = BaseCls.GlbReportPromotor == "" ? "ALL" : BaseCls.GlbReportPromotor;
            dr["PARA_HEADING"] = BaseCls.GlbReportHeading;
            dr["PARA_FREE_ISS_TP"] = BaseCls.GlbReportIsFast;

            dr["GRP_PCENTER"] = BaseCls.GlbReportGroupProfit;
            dr["GRP_DOCTYPE"] = BaseCls.GlbReportGroupDocType;
            dr["GRP_CUST"] = BaseCls.GlbReportGroupCustomerCode;
            dr["GRP_EXEC"] = BaseCls.GlbReportGroupExecCode;
            dr["GRP_ITCODE"] = BaseCls.GlbReportGroupItemCode;
            dr["GRP_BRAND"] = BaseCls.GlbReportGroupBrand;
            dr["GRP_MODEL"] = BaseCls.GlbReportGroupModel;
            dr["GRP_CAT1"] = BaseCls.GlbReportGroupItemCat1;
            dr["GRP_CAT2"] = BaseCls.GlbReportGroupItemCat2;
            dr["GRP_CAT3"] = BaseCls.GlbReportGroupItemCat3;
            dr["GRP_CAT4"] = BaseCls.GlbReportGroupItemCat4;
            dr["GRP_CAT5"] = BaseCls.GlbReportGroupItemCat5;
            dr["GRP_STKTYPE"] = BaseCls.GlbReportGroupItemStatus;
            dr["GRP_INVNO"] = BaseCls.GlbReportGroupInvoiceNo;
            dr["GRP_LAST_GROUP"] = BaseCls.GlbReportGroupLastGroup;
            dr["GRP_LAST_GROUP_CAT"] = BaseCls.GlbReportGroupLastGroupCat;
            dr["GRP_PRMOTOR"] = BaseCls.GlbReportGroupPromotor;
            dr["GRP_DLOC"] = BaseCls.GlbReportGroupDOLoc;
            dr["GRP_JOBNO"] = BaseCls.GlbReportGroupJobNo;

            param.Rows.Add(dr);

            _gpRepl.Database.Tables["GP_DTL"].SetDataSource(GLOB_DataTable);
            _gpRepl.Database.Tables["REP_PARA"].SetDataSource(param);

        }

        public void DeliveredSalesInsuReport()
        {// Sanjeewa 21-01-15
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetDeliveredSalesInsuDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, drow["tpl_pc"].ToString(), BaseCls.GlbUserComCode, BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFast);
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
            param.Columns.Add("GRP_STKTYPE", typeof(int));
            param.Columns.Add("GRP_INVNO", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP_CAT", typeof(string));
            param.Columns.Add("GRP_PRMOTOR", typeof(int));
            param.Columns.Add("GRP_DLOC", typeof(int));

            dr = param.NewRow();
            dr["PARA_USER"] = BaseCls.GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["PARA_CUST"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode;
            dr["PARA_EXEC"] = BaseCls.GlbReportExecCode == "" ? "ALL" : BaseCls.GlbReportExecCode;
            dr["PARA_DOCTYPE"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["PARA_ITCODE"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["PARA_BRAND"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["PARA_MODEL"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["PARA_CAT1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["PARA_CAT2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["PARA_CAT3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["PARA_STKTYPE"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["PARA_INVNO"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc;
            dr["PARA_PROMOTOR"] = BaseCls.GlbReportPromotor == "" ? "ALL" : BaseCls.GlbReportPromotor;
            dr["PARA_HEADING"] = BaseCls.GlbReportHeading;
            dr["PARA_FREE_ISS_TP"] = BaseCls.GlbReportIsFast;

            dr["GRP_PCENTER"] = BaseCls.GlbReportGroupProfit;
            dr["GRP_DOCTYPE"] = BaseCls.GlbReportGroupDocType;
            dr["GRP_CUST"] = BaseCls.GlbReportGroupCustomerCode;
            dr["GRP_EXEC"] = BaseCls.GlbReportGroupExecCode;
            dr["GRP_ITCODE"] = BaseCls.GlbReportGroupItemCode;
            dr["GRP_BRAND"] = BaseCls.GlbReportGroupBrand;
            dr["GRP_MODEL"] = BaseCls.GlbReportGroupModel;
            dr["GRP_CAT1"] = BaseCls.GlbReportGroupItemCat1;
            dr["GRP_CAT2"] = BaseCls.GlbReportGroupItemCat2;
            dr["GRP_CAT3"] = BaseCls.GlbReportGroupItemCat3;
            dr["GRP_STKTYPE"] = BaseCls.GlbReportGroupItemStatus;
            dr["GRP_INVNO"] = BaseCls.GlbReportGroupInvoiceNo;
            dr["GRP_LAST_GROUP"] = BaseCls.GlbReportGroupLastGroup;
            dr["GRP_LAST_GROUP_CAT"] = BaseCls.GlbReportGroupLastGroupCat;
            dr["GRP_PRMOTOR"] = BaseCls.GlbReportGroupPromotor;
            dr["GRP_DLOC"] = BaseCls.GlbReportGroupDOLoc;

            param.Rows.Add(dr);

            _delSalesrptInsu.Database.Tables["GLB_REP_SALES"].SetDataSource(GLOB_DataTable);
            _delSalesrptInsu.Database.Tables["REP_PARA"].SetDataSource(param);

        }

        public void ComparisonofDeliveredSalesReport()
        {// Sanjeewa 12-12-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetComparisonofDeliveredSalesDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, drow["tpl_pc"].ToString(), BaseCls.GlbUserComCode, BaseCls.GlbReportPromotor, BaseCls.GlbReportIsFast, BaseCls.GlbReportCusId, BaseCls.GlbReportType);
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
            param.Columns.Add("PARA_STKTYPE", typeof(string));
            param.Columns.Add("PARA_INVNO", typeof(string));
            param.Columns.Add("PARA_PROMOTOR", typeof(string));
            param.Columns.Add("PARA_HEADING", typeof(string));
            param.Columns.Add("PARA_FREE_ISS_TP", typeof(int));
            param.Columns.Add("PARA_ISMONTH", typeof(string));

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
            param.Columns.Add("GRP_STKTYPE", typeof(int));
            param.Columns.Add("GRP_INVNO", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP_CAT", typeof(string));
            param.Columns.Add("GRP_PRMOTOR", typeof(int));
            param.Columns.Add("GRP_DLOC", typeof(int));

            dr = param.NewRow();
            dr["PARA_USER"] = BaseCls.GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["PARA_CUST"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode;
            dr["PARA_EXEC"] = BaseCls.GlbReportExecCode == "" ? "ALL" : BaseCls.GlbReportExecCode;
            dr["PARA_DOCTYPE"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["PARA_ITCODE"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["PARA_BRAND"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["PARA_MODEL"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["PARA_CAT1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["PARA_CAT2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["PARA_CAT3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["PARA_STKTYPE"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["PARA_INVNO"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc;
            dr["PARA_PROMOTOR"] = BaseCls.GlbReportPromotor == "" ? "ALL" : BaseCls.GlbReportPromotor;
            dr["PARA_HEADING"] = BaseCls.GlbReportHeading;
            dr["PARA_FREE_ISS_TP"] = BaseCls.GlbReportIsFast;
            dr["PARA_ISMONTH"] = BaseCls.GlbReportCusId;

            dr["GRP_PCENTER"] = BaseCls.GlbReportGroupProfit;
            dr["GRP_DOCTYPE"] = BaseCls.GlbReportGroupDocType;
            dr["GRP_CUST"] = BaseCls.GlbReportGroupCustomerCode;
            dr["GRP_EXEC"] = BaseCls.GlbReportGroupExecCode;
            dr["GRP_ITCODE"] = BaseCls.GlbReportGroupItemCode;
            dr["GRP_BRAND"] = BaseCls.GlbReportGroupBrand;
            dr["GRP_MODEL"] = BaseCls.GlbReportGroupModel;
            dr["GRP_CAT1"] = BaseCls.GlbReportGroupItemCat1;
            dr["GRP_CAT2"] = BaseCls.GlbReportGroupItemCat2;
            dr["GRP_CAT3"] = BaseCls.GlbReportGroupItemCat3;
            dr["GRP_STKTYPE"] = BaseCls.GlbReportGroupItemStatus;
            dr["GRP_INVNO"] = BaseCls.GlbReportGroupInvoiceNo;
            dr["GRP_LAST_GROUP"] = BaseCls.GlbReportGroupLastGroup;
            dr["GRP_LAST_GROUP_CAT"] = BaseCls.GlbReportGroupLastGroupCat;
            dr["GRP_PRMOTOR"] = BaseCls.GlbReportGroupPromotor;
            dr["GRP_DLOC"] = BaseCls.GlbReportGroupDOLoc;

            param.Rows.Add(dr);

            _delSalesComrpt.Database.Tables["GLB_REP_SALES"].SetDataSource(GLOB_DataTable);
            _delSalesComrpt.Database.Tables["REP_PARA"].SetDataSource(param);

        }

        public void TotalSales8020Report()
        {// Sanjeewa 22-08-2014
            DataTable param = new DataTable();
            DataRow dr;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            int delrep = bsObj.CHNLSVC.MsgPortal.DeleteCustomerAnalysisRep(BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetTotalSales8020Details(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, drow["tpl_pc"].ToString(), BaseCls.GlbUserComCode, BaseCls.GlbReportGroupProfit, BaseCls.GlbReportExeType);
                    //GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            GLOB_DataTable = bsObj.CHNLSVC.MsgPortal.GetTotalSales8020Summary(BaseCls.GlbReportGroupProfit, BaseCls.GlbReportExeType, BaseCls.GlbUserID);

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
            param.Columns.Add("PARA_HEADING", typeof(string));

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
            param.Columns.Add("GRP_STKTYPE", typeof(int));
            param.Columns.Add("GRP_INVNO", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP_CAT", typeof(string));
            param.Columns.Add("GRP_DLOC", typeof(int));

            dr = param.NewRow();
            dr["PARA_USER"] = BaseCls.GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["PARA_CUST"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode;
            dr["PARA_EXEC"] = BaseCls.GlbReportExecCode == "" ? "ALL" : BaseCls.GlbReportExecCode;
            dr["PARA_DOCTYPE"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["PARA_ITCODE"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["PARA_BRAND"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["PARA_MODEL"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["PARA_CAT1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["PARA_CAT2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["PARA_CAT3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["PARA_STKTYPE"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["PARA_INVNO"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc;
            dr["PARA_HEADING"] = BaseCls.GlbReportHeading;

            dr["GRP_PCENTER"] = BaseCls.GlbReportGroupProfit;
            dr["GRP_DOCTYPE"] = BaseCls.GlbReportGroupDocType;
            dr["GRP_CUST"] = BaseCls.GlbReportGroupCustomerCode;
            dr["GRP_EXEC"] = BaseCls.GlbReportGroupExecCode;
            dr["GRP_ITCODE"] = BaseCls.GlbReportGroupItemCode;
            dr["GRP_BRAND"] = BaseCls.GlbReportGroupBrand;
            dr["GRP_MODEL"] = BaseCls.GlbReportGroupModel;
            dr["GRP_CAT1"] = BaseCls.GlbReportGroupItemCat1;
            dr["GRP_CAT2"] = BaseCls.GlbReportGroupItemCat2;
            dr["GRP_CAT3"] = BaseCls.GlbReportGroupItemCat3;
            dr["GRP_STKTYPE"] = BaseCls.GlbReportGroupItemStatus;
            dr["GRP_INVNO"] = BaseCls.GlbReportGroupInvoiceNo;
            dr["GRP_LAST_GROUP"] = BaseCls.GlbReportGroupLastGroup;
            dr["GRP_LAST_GROUP_CAT"] = BaseCls.GlbReportGroupLastGroupCat;
            dr["GRP_DLOC"] = BaseCls.GlbReportGroupDOLoc;

            param.Rows.Add(dr);

            _delSalesrpt8020.Database.Tables["GLB_REP_SALES_ANAL"].SetDataSource(GLOB_DataTable);
            _delSalesrpt8020.Database.Tables["REP_PARA"].SetDataSource(param);

        }

        public void DeliveredSalesGRNReport()
        {// Sanjeewa 05-09-13
            DataTable param = new DataTable();
            DataTable DEL_SER = new DataTable();
            DataRow dr;
            int i = 0;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {

                    i = i + 1;
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetDeliveredSalesGRNDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, Convert.ToString(i), BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, BaseCls.GlbReportSupplier, BaseCls.GlbReportPurchaseOrder, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportIsExport);
                    GLOB_DataTable.Merge(TMP_DataTable);
                    DataTable TMP_DataTable_NEW = TMP_DataTable.DefaultView.ToTable(true, "do_no", "item_code");

                    if (TMP_DataTable_NEW.Rows.Count > 0)
                    {
                        foreach (DataRow drow1 in TMP_DataTable_NEW.Rows)
                        {
                            DataTable TMP_DataTable_SER = new DataTable();
                            TMP_DataTable_SER = bsObj.CHNLSVC.MsgPortal.GetDeliveredSerDetails(drow1["do_no"].ToString(), drow1["item_code"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportIsExport, 0);
                            DEL_SER.Merge(TMP_DataTable_SER);
                        }
                    }


                }

                DataTable TMP_DataTable_SER1 = new DataTable();
                TMP_DataTable_SER1 = bsObj.CHNLSVC.MsgPortal.GetDeliveredSerDetails("", "", BaseCls.GlbUserID, BaseCls.GlbReportIsExport, 1);
                DEL_SER.Merge(TMP_DataTable_SER1);
            }

            //DataTable PROC_DELIVERED_SALES = bsObj.CHNLSVC.Sales.GetDeliveredSalesGRNDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, BaseCls.GlbReportSupplier, BaseCls.GlbReportPurchaseOrder);

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
            param.Columns.Add("GRP_STKTYPE", typeof(int));
            param.Columns.Add("GRP_INVNO", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP_CAT", typeof(string));
            param.Columns.Add("GRP_DLOC", typeof(int));

            dr = param.NewRow();
            dr["PARA_USER"] = BaseCls.GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["PARA_CUST"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode;
            dr["PARA_EXEC"] = BaseCls.GlbReportExecCode == "" ? "ALL" : BaseCls.GlbReportExecCode;
            dr["PARA_DOCTYPE"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["PARA_ITCODE"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["PARA_BRAND"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["PARA_MODEL"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["PARA_CAT1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["PARA_CAT2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["PARA_CAT3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["PARA_STKTYPE"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["PARA_INVNO"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc;
            dr["PARA_PONO"] = BaseCls.GlbReportPurchaseOrder == "" ? "ALL" : BaseCls.GlbReportPurchaseOrder;
            dr["PARA_SUPP"] = BaseCls.GlbReportSupplier == "" ? "ALL" : BaseCls.GlbReportSupplier;
            dr["PARA_HEADING"] = BaseCls.GlbReportHeading;

            dr["GRP_PCENTER"] = 0;
            dr["GRP_DOCTYPE"] = 0;
            dr["GRP_CUST"] = 0;
            dr["GRP_EXEC"] = 0;
            dr["GRP_ITCODE"] = 0;
            dr["GRP_BRAND"] = 0;
            dr["GRP_MODEL"] = 0;
            dr["GRP_CAT1"] = 0;
            dr["GRP_CAT2"] = 0;
            dr["GRP_CAT3"] = 0;
            dr["GRP_STKTYPE"] = 0;
            dr["GRP_INVNO"] = 0;
            dr["GRP_LAST_GROUP"] = 0;
            dr["GRP_LAST_GROUP_CAT"] = 0;
            dr["GRP_DLOC"] = 0;

            param.Rows.Add(dr);

            if (BaseCls.GlbReportName == "Delivered_Sales_GRN.rpt")
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
                        }
                    }
                }

            };

        }

        public void DeliveredSalesGRNCostReport()
        {// Sanjeewa 23-02-2016
            DataTable param = new DataTable();
            DataTable DEL_SER = new DataTable();
            DataRow dr;
            int i = 0;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    i = i + 1;
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetDeliveredSalesGRNCostDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, Convert.ToString(i), BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, BaseCls.GlbReportSupplier, BaseCls.GlbReportPurchaseOrder, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportIsExport);
                    GLOB_DataTable.Merge(TMP_DataTable);
                    DataTable TMP_DataTable_NEW = TMP_DataTable.DefaultView.ToTable(true, "do_no", "item_code");

                    if (TMP_DataTable_NEW.Rows.Count > 0)
                    {
                        foreach (DataRow drow1 in TMP_DataTable_NEW.Rows)
                        {
                            DataTable TMP_DataTable_SER = new DataTable();
                            TMP_DataTable_SER = bsObj.CHNLSVC.MsgPortal.GetDeliveredSerDetails(drow1["do_no"].ToString(), drow1["item_code"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportIsExport, 0);
                            DEL_SER.Merge(TMP_DataTable_SER);
                        }
                    }

                }

                DataTable TMP_DataTable_SER1 = new DataTable();
                TMP_DataTable_SER1 = bsObj.CHNLSVC.MsgPortal.GetDeliveredSerDetails("", "", BaseCls.GlbUserID, BaseCls.GlbReportIsExport, 1);
                DEL_SER.Merge(TMP_DataTable_SER1);
            }

            //DataTable PROC_DELIVERED_SALES = bsObj.CHNLSVC.Sales.GetDeliveredSalesGRNDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportProfit, BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, BaseCls.GlbReportSupplier, BaseCls.GlbReportPurchaseOrder);

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
            param.Columns.Add("GRP_STKTYPE", typeof(int));
            param.Columns.Add("GRP_INVNO", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP", typeof(int));
            param.Columns.Add("GRP_LAST_GROUP_CAT", typeof(string));
            param.Columns.Add("GRP_DLOC", typeof(int));

            dr = param.NewRow();
            dr["PARA_USER"] = BaseCls.GlbUserID;
            dr["PARA_PERIOD"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");
            dr["PARA_PCENTER"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["PARA_CUST"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode;
            dr["PARA_EXEC"] = BaseCls.GlbReportExecCode == "" ? "ALL" : BaseCls.GlbReportExecCode;
            dr["PARA_DOCTYPE"] = BaseCls.GlbReportDocType == "" ? "ALL" : BaseCls.GlbReportDocType;
            dr["PARA_ITCODE"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode;
            dr["PARA_BRAND"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand;
            dr["PARA_MODEL"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel;
            dr["PARA_CAT1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1;
            dr["PARA_CAT2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2;
            dr["PARA_CAT3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3;
            dr["PARA_STKTYPE"] = BaseCls.GlbReportItemStatus == "" ? "ALL" : BaseCls.GlbReportItemStatus;
            dr["PARA_INVNO"] = BaseCls.GlbReportDoc == "" ? "ALL" : BaseCls.GlbReportDoc;
            dr["PARA_PONO"] = BaseCls.GlbReportPurchaseOrder == "" ? "ALL" : BaseCls.GlbReportPurchaseOrder;
            dr["PARA_SUPP"] = BaseCls.GlbReportSupplier == "" ? "ALL" : BaseCls.GlbReportSupplier;
            dr["PARA_HEADING"] = BaseCls.GlbReportHeading;

            dr["GRP_PCENTER"] = 0;
            dr["GRP_DOCTYPE"] = 0;
            dr["GRP_CUST"] = 0;
            dr["GRP_EXEC"] = 0;
            dr["GRP_ITCODE"] = 0;
            dr["GRP_BRAND"] = 0;
            dr["GRP_MODEL"] = 0;
            dr["GRP_CAT1"] = 0;
            dr["GRP_CAT2"] = 0;
            dr["GRP_CAT3"] = 0;
            dr["GRP_STKTYPE"] = 0;
            dr["GRP_INVNO"] = 0;
            dr["GRP_LAST_GROUP"] = 0;
            dr["GRP_LAST_GROUP_CAT"] = 0;
            dr["GRP_DLOC"] = 0;

            param.Rows.Add(dr);

            if (BaseCls.GlbReportName == "Delivered_Sales_GRN _Cost.rpt")
            {
                _delSalesrptGRNCost.Database.Tables["GLB_REP_SALES_GRN"].SetDataSource(GLOB_DataTable);
                _delSalesrptGRNCost.Database.Tables["REP_PARA"].SetDataSource(param);

                foreach (object repOp in _delSalesrptGRNCost.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;
                        if (_cs.SubreportName == "del_serial")
                        {
                            ReportDocument subRepDoc = _delSalesrptGRNCost.Subreports[_cs.SubreportName];
                            subRepDoc.Database.Tables["GLB_REP_DEL_SER"].SetDataSource(DEL_SER);
                        }
                    }
                }

            };

        }
        public void ReceivableMovementReport()
        {// Nadeeka 11-01-13
            DataTable param = new DataTable();
            DataRow dr;
            string accNo = default(string);
            accNo = BaseCls.GlbReportDoc;
            DataTable tmp_user_pc = new DataTable();
            DataTable mst_profit_center = new DataTable();
            //  DataTable mst_profit_center1 = default(DataTable);
            DataTable PROC_RECEIVABLE_MOVEMENTS = new DataTable();
            Int16 isSumm = 0;
            // DataTable PROC_RECEIVABLE_MOVEMENTS = bsObj.CHNLSVC.Sales.ReceivableMovemntReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportProfit, BaseCls.GlbReportComp);
            DataTable mst_com = default(DataTable);

            if (BaseCls.GlbReportName == "ReceivableMovementReports.rpt")
            {
                isSumm = 0;
            }
            else
            {
                isSumm = 1;
            }

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportComp, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DATA_TABLE = new DataTable();
                    TMP_DATA_TABLE = bsObj.CHNLSVC.Sales.ReceivableMovemntReport(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, drow["tpl_pc"].ToString(), BaseCls.GlbReportComp, isSumm);
                    PROC_RECEIVABLE_MOVEMENTS.Merge(TMP_DATA_TABLE);


                }
            }


            mst_profit_center.Columns.Add("MPC_CD", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            DataTable TMP_DATA_LOC = new DataTable();

            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);

            TMP_DATA_LOC = PROC_RECEIVABLE_MOVEMENTS.DefaultView.ToTable(true, "PROFIT", "COMPANYCODE");

            foreach (DataRow row in TMP_DATA_LOC.Rows)
            {
                if (row["COMPANYCODE"].ToString() != "")
                {
                    int index = TMP_DATA_LOC.Rows.IndexOf(row);
                    DataTable mst_profit_center1 = new DataTable();
                    mst_profit_center1 = bsObj.CHNLSVC.Sales.GetProfitCenterTable(row["COMPANYCODE"].ToString(), row["PROFIT"].ToString());

                    //foreach (DataRow row1 in mst_profit_center1.Rows)
                    //{
                    //    dr = mst_profit_center.NewRow();
                    //    dr["MPC_CD"] = row1["MPC_CD"].ToString();
                    //    dr["MPC_DESC"] = row1["MPC_DESC"].ToString();
                    //    dr["MPC_COM"] = row1["MPC_COM"].ToString();
                    //    mst_profit_center.Rows.Add(dr);
                    //}
                    mst_profit_center.Merge(mst_profit_center1);
                }
            }
            if (mst_profit_center.Rows.Count > 0)
            {
                mst_profit_center = mst_profit_center.DefaultView.ToTable(true);
            }
            if (mst_com.Rows.Count > 0)
            {
                mst_com = mst_com.DefaultView.ToTable(true);
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            param.Rows.Add(dr);

            if (BaseCls.GlbReportName == "ReceivableMovementReports.rpt")
            {
                _recMoverpt.Database.Tables["PROC_RECEIVABLE_MOVEMENTS"].SetDataSource(PROC_RECEIVABLE_MOVEMENTS);
                _recMoverpt.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                _recMoverpt.Database.Tables["mst_com"].SetDataSource(mst_com);
                _recMoverpt.Database.Tables["param"].SetDataSource(param);
            }
            else
            {
                _recMoveSumrpt.Database.Tables["PROC_RECEIVABLE_MOVEMENTS"].SetDataSource(PROC_RECEIVABLE_MOVEMENTS);
                _recMoveSumrpt.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
                _recMoveSumrpt.Database.Tables["mst_com"].SetDataSource(mst_com);
                _recMoveSumrpt.Database.Tables["param"].SetDataSource(param);
            };

        }

        public void SServiceReceiptPrint()
        {// Nadeeka 11-01-13

            string accNo = default(string);
            accNo = BaseCls.GlbReportDoc;
            DataTable sev_costsheet = default(DataTable);
            DataTable sev_job_hdr = default(DataTable);
            DataTable sev_job_det = default(DataTable);
            DataTable mst_profit_center = default(DataTable);
            DataTable sat_receipt = bsObj.CHNLSVC.Sales.GetReceipt(BaseCls.GlbReportDoc);
            DataTable mst_com = default(DataTable);


            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(accNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _SrecSevRecrpt.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            foreach (DataRow row in sat_receipt.Rows)
            {
                int index = sat_receipt.Rows.IndexOf(row);
                if (index == 0)
                {
                    mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(row["SAR_COM_CD"].ToString(), row["SAR_PROFIT_CENTER_CD"].ToString());
                    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(row["SAR_COM_CD"].ToString());
                    sev_job_hdr = bsObj.CHNLSVC.Sales.GetSevJobHeader(row["SAR_SER_JOB_NO"].ToString());
                    sev_job_det = bsObj.CHNLSVC.Sales.GetSevJobDet(row["SAR_SER_JOB_NO"].ToString());
                    sev_costsheet = bsObj.CHNLSVC.Sales.GetSevJobCost(row["SAR_SER_JOB_NO"].ToString());
                }
            }

            _SrecSevRecrpt.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            //  _recSevRecrpt.Database.Tables["mst_com"].SetDataSource(mst_com);
            _SrecSevRecrpt.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
            _SrecSevRecrpt.Database.Tables["sev_job_hdr"].SetDataSource(sev_job_hdr);
            _SrecSevRecrpt.Database.Tables["sev_job_det"].SetDataSource(sev_job_det);


            foreach (object repOp in _SrecSevRecrpt.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCharge")
                    {
                        ReportDocument subRepDoc = _SrecSevRecrpt.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sev_costsheet"].SetDataSource(sev_costsheet);

                    }

                }
            }
        }
        public void ServiceReceiptPrint()
        {// Nadeeka 11-01-13

            string accNo = default(string);
            accNo = BaseCls.GlbReportDoc;
            DataTable sev_costsheet = default(DataTable);
            DataTable sev_job_hdr = default(DataTable);
            DataTable sev_job_det = default(DataTable);
            DataTable mst_profit_center = default(DataTable);
            DataTable sat_receipt = bsObj.CHNLSVC.Sales.GetReceipt(BaseCls.GlbReportDoc);
            DataTable mst_com = default(DataTable);


            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;



            _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(accNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));


            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;
            PRINT_DOC.Rows.Add(dr3);
            _recSevRecrpt.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            foreach (DataRow row in sat_receipt.Rows)
            {
                int index = sat_receipt.Rows.IndexOf(row);
                if (index == 0)
                {
                    mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(row["SAR_COM_CD"].ToString(), row["SAR_PROFIT_CENTER_CD"].ToString());
                    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(row["SAR_COM_CD"].ToString());
                    sev_job_hdr = bsObj.CHNLSVC.Sales.GetSevJobHeader(row["SAR_SER_JOB_NO"].ToString());
                    sev_job_det = bsObj.CHNLSVC.Sales.GetSevJobDet(row["SAR_SER_JOB_NO"].ToString());
                    sev_costsheet = bsObj.CHNLSVC.Sales.GetSevJobCost(row["SAR_SER_JOB_NO"].ToString());
                }
            }

            _recSevRecrpt.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            //  _recSevRecrpt.Database.Tables["mst_com"].SetDataSource(mst_com);
            _recSevRecrpt.Database.Tables["sat_receipt"].SetDataSource(sat_receipt);
            _recSevRecrpt.Database.Tables["sev_job_hdr"].SetDataSource(sev_job_hdr);
            _recSevRecrpt.Database.Tables["sev_job_det"].SetDataSource(sev_job_det);
            _recSevRecrpt.Database.Tables["mst_com"].SetDataSource(mst_com);

            foreach (object repOp in _recSevRecrpt.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "rptCharge")
                    {
                        ReportDocument subRepDoc = _recSevRecrpt.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["sev_costsheet"].SetDataSource(sev_costsheet);

                    }

                }
            }
        }
        public void RegistrationReport()
        {// shalika 30-10-2014 -- Modified Sanjeewa 2014-11-13
            DataTable glob_Rec_List = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            DataTable DT_REG_RPT = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);
            DataTable MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_Rec_List = new DataTable();
                    DT_REG_RPT = bsObj.CHNLSVC.Sales.GetRegistraionDetails(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportDocType, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());

                    glob_Rec_List.Merge(DT_REG_RPT);

                }
            }
            //else
            //{
            //    DT_REG_RPT = bsObj.CHNLSVC.Sales.GetRegistraionDetails(BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportDocType, BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbReportProfit);
            //    glob_Rec_List.Merge(DT_REG_RPT);
            //}
            string Name = "";
            if (BaseCls.GlbReportDocType == "RMV") Name = "RMV sent Details";
            if (BaseCls.GlbReportDocType == "CRR") Name = "CR Received Details";
            if (BaseCls.GlbReportDocType == "NO") Name = "No Plate Received Details";
            param.Clear();
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("Name", typeof(string));
            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["Name"] = Name;
            param.Rows.Add(dr);

            _vheRegRPT.Database.Tables["REG_RPT"].SetDataSource(glob_Rec_List);
            _vheRegRPT.Database.Tables["mst_com"].SetDataSource(MST_COM);
            _vheRegRPT.Database.Tables["param"].SetDataSource(param);

        }
        public void InsuranceCoverNote()
        {// Nadeeka  13-03-2013

            DataTable INSURANCE_COVER_NOTE = bsObj.CHNLSVC.Sales.ProcessInsuranceCoverNote(BaseCls.GlbReportDoc);

            if (BaseCls.GlbReportName == "InsuranceCoverNote.rpt")    //kapila 11/3/2014
            {
                _insCover.Database.Tables["INSURANCE_COVER_NOTE"].SetDataSource(INSURANCE_COVER_NOTE);
            }
            if (BaseCls.GlbReportName == "InsuranceCoverNoteMBSL.rpt")    //kapila 11/3/2014
            {
                _insCoverMBSL.Database.Tables["INSURANCE_COVER_NOTE"].SetDataSource(INSURANCE_COVER_NOTE);
            }
            if (BaseCls.GlbReportName == "InsuranceCoverNoteUMS.rpt")    //kapila 11/3/2014
            {
                _insCoverUMS.Database.Tables["INSURANCE_COVER_NOTE"].SetDataSource(INSURANCE_COVER_NOTE);
            }
            if (BaseCls.GlbReportName == "InsuranceCoverNoteJS.rpt")    //kapila 11/3/2014
            {
                _insCoverJS.Database.Tables["INSURANCE_COVER_NOTE"].SetDataSource(INSURANCE_COVER_NOTE);
            }
            if (BaseCls.GlbReportName == "InsuranceCoverNoteAIA.rpt")    //kapila 7/5/2014
            {
                _insCoverAIA.Database.Tables["INSURANCE_COVER_NOTE"].SetDataSource(INSURANCE_COVER_NOTE);
            }


        }
        public void DeliveredSalesWithSerial()
        {// Nadeeka  05-07-2013
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
            DataTable GLB_TEMP_DEL_SALESSERIAL = new DataTable();
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_SALES = new DataTable();
                    TMP_SALES = bsObj.CHNLSVC.MsgPortal.DeliveredSalesWithSerial(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportCompCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, drow["tpl_pc"].ToString());
                    GLB_TEMP_DEL_SALESSERIAL.Merge(TMP_SALES);
                }
            }

            GLB_TEMP_DEL_SALESSERIAL = GLB_TEMP_DEL_SALESSERIAL.DefaultView.ToTable(true);
            _delSerlSales.Database.Tables["GLB_TEMP_DEL_SALESSERIAL"].SetDataSource(GLB_TEMP_DEL_SALESSERIAL);
            _delSerlSales.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _delSerlSales.Database.Tables["param"].SetDataSource(param);

        }
        //hasith 25/12/2015
        public void CreditNoteDetails()
        {
            DataTable param = new DataTable();
            DataTable CreditNoteDetails = new DataTable();
            DataRow dr;
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_CreditNoteDetails = new DataTable();
                    tmp_CreditNoteDetails = bsObj.CHNLSVC.MsgPortal.GetCreditnoteDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString());
                    CreditNoteDetails.Merge(tmp_CreditNoteDetails);
                }
            }
            // tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("pc", typeof(string));
            param.Columns.Add("com", typeof(string));

            dr = param.NewRow();
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["pc"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["com"] = BaseCls.GlbReportComp;
            param.Rows.Add(dr);

            //check all profit center 
            //CreditNoteDetails = bsObj.CHNLSVC.Sales.GetCreditnoteDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, BaseCls.GlbReportProfit);
            _creditnotedetails.Database.Tables["CreditNoteDetails"].SetDataSource(CreditNoteDetails);
            _creditnotedetails.Database.Tables["param"].SetDataSource(param);



        }
        public void DealerCommission()
        {// Nadeeka  06-05-2014
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
            DataTable GLB_TEMP_DEL_SALESSERIAL = new DataTable();
            MST_COM = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_SALES = new DataTable();
                    TMP_SALES = bsObj.CHNLSVC.MsgPortal.DeliveredSalesWithSerial(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportCompCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportModel, drow["tpl_pc"].ToString());
                    GLB_TEMP_DEL_SALESSERIAL.Merge(TMP_SALES);
                }
            }

            GLB_TEMP_DEL_SALESSERIAL = GLB_TEMP_DEL_SALESSERIAL.DefaultView.ToTable(true);
            _delSerDelComm.Database.Tables["GLB_TEMP_DEL_SALESSERIAL"].SetDataSource(GLB_TEMP_DEL_SALESSERIAL);
            _delSerDelComm.Database.Tables["MST_COM"].SetDataSource(MST_COM);
            _delSerDelComm.Database.Tables["param"].SetDataSource(param);

            foreach (object repOp in _delSerDelComm.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    if (_cs.SubreportName == "subCommission")
                    {
                        ReportDocument subRepDoc = _delSerDelComm.Subreports[_cs.SubreportName];

                        subRepDoc.Database.Tables["GLB_TEMP_DEL_SALESSERIAL"].SetDataSource(GLB_TEMP_DEL_SALESSERIAL);

                    }

                }
            }
        }
        public int InternalVoucher()
        {// Nadeeka  18-04-2013
            int rtnValue = 0;
            DataTable VOU_HDR = bsObj.CHNLSVC.MsgPortal.InternalPaymentVoucher(BaseCls.GlbReportDoc);
            DataTable mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);

            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(BaseCls.GlbReportDoc);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            if (VOU_HDR.Rows.Count > 0)
            {
                _intVou.Database.Tables["VOU_HDR"].SetDataSource(VOU_HDR);
                _intVou.Database.Tables["mst_com"].SetDataSource(mst_com);
                _intVou.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);
            }

            if (VOU_HDR.Rows.Count == 0)
            { rtnValue = 1; }


            return rtnValue;
        }


        public void getVoudets(List<string> _lstVoucher)
        {
            BaseCls.GlbReportDataTable = null;
            foreach (var item in _lstVoucher)
            {
                DataTable TMP_DataTable = new DataTable();
                TMP_DataTable = bsObj.CHNLSVC.Sales.Load_cheque_printing_details(item.ToString());
                GLOB_DataTable.Merge(TMP_DataTable);

                BaseCls.GlbReportDataTable = GLOB_DataTable;

            }
        }

        public void get_chan_dets(string code)
        {
            DataRow[] foundRows;
            BaseCls.GlbReportDataTable = null;
            DataTable hdt1 = null;

            DataTable TMP_DataTable_ch = new DataTable();
            TMP_DataTable_ch = bsObj.CHNLSVC.General.getAllChannel_details(BaseCls.GlbUserComCode);
            //GLOB_DataTable.Merge(TMP_DataTable);
            string expression = "msc_cd like '%" + code + "%'";
            foundRows = TMP_DataTable_ch.Select(expression);
            if (foundRows.Count() > 0)
            {
                hdt1 = foundRows.CopyToDataTable<DataRow>();
            }

            BaseCls.GlbReportDataTable = hdt1;


        }

        public void get_area_dets(string code)
        {
            DataRow[] foundRows;
            BaseCls.GlbReportDataTable = null;
            DataTable hdt1 = null;

            DataTable TMP_DataTable_ch = new DataTable();
            TMP_DataTable_ch = bsObj.CHNLSVC.General.getAllarea_details(BaseCls.GlbUserComCode);
            //GLOB_DataTable.Merge(TMP_DataTable);
            string expression = "MSAR_CD like '%" + code + "%'";
            foundRows = TMP_DataTable_ch.Select(expression);
            if (foundRows.Count() > 0)
            {
                hdt1 = foundRows.CopyToDataTable<DataRow>();
            }

            BaseCls.GlbReportDataTable = hdt1;


        }

        public void get_region_dets(string code)
        {
            DataRow[] foundRows;
            BaseCls.GlbReportDataTable = null;
            DataTable hdt1 = null;

            DataTable TMP_DataTable_ch = new DataTable();
            TMP_DataTable_ch = bsObj.CHNLSVC.General.getAllregion_details(BaseCls.GlbUserComCode);
            //GLOB_DataTable.Merge(TMP_DataTable);
            string expression = "MSRG_CD like '%" + code + "%'";
            foundRows = TMP_DataTable_ch.Select(expression);
            if (foundRows.Count() > 0)
            {
                hdt1 = foundRows.CopyToDataTable<DataRow>();
            }

            BaseCls.GlbReportDataTable = hdt1;


        }

        public void get_zone_dets(string code)
        {
            DataRow[] foundRows;
            BaseCls.GlbReportDataTable = null;
            DataTable hdt1 = null;

            DataTable TMP_DataTable_ch = new DataTable();
            TMP_DataTable_ch = bsObj.CHNLSVC.General.getAllzone_details(BaseCls.GlbUserComCode);
            //GLOB_DataTable.Merge(TMP_DataTable);
            string expression = "MSZN_CD like '%" + code + "%'";
            foundRows = TMP_DataTable_ch.Select(expression);
            if (foundRows.Count() > 0)
            {
                hdt1 = foundRows.CopyToDataTable<DataRow>();
            }

            BaseCls.GlbReportDataTable = hdt1;


        }



        public void get_sub_chan_dets(string code)
        {
            DataRow[] foundRows;
            BaseCls.GlbReportDataTable = null;
            DataTable dtsub = null;

            DataTable TMP_DataTable_sub = new DataTable();
            TMP_DataTable_sub = bsObj.CHNLSVC.General.getAllSubChannel_details(BaseCls.GlbUserComCode);
            //GLOB_DataTable.Merge(TMP_DataTable);
            string expression = "MSSC_CD like '%" + code + "%'";
            foundRows = TMP_DataTable_sub.Select(expression);
            if (foundRows.Count() > 0)
            {
                dtsub = foundRows.CopyToDataTable<DataRow>();
            }

            BaseCls.GlbReportDataTable = dtsub;


        }


        public void get_AllocateItems_dets(string type, string cat, List<string> _lstpc)
        {
            DataRow[] foundRows;
            BaseCls.GlbReportDataTable = null;
            DataTable dtsub = null;

            DataTable TMP_DataTable_sub = new DataTable();
            TMP_DataTable_sub = bsObj.CHNLSVC.Sales.getAllAllocateItems_details(BaseCls.GlbUserComCode);
            foreach (var itm in _lstpc)
            {
                string expression = "SAM_PTY_TP like '%" + type + "%' and SAM_CAT like '%" + cat + "%' and SAM_PTY_CD like '%" + itm.ToString() + "%'";
                foundRows = TMP_DataTable_sub.Select(expression);
                if (foundRows.Count() > 0)
                {
                    dtsub = foundRows.CopyToDataTable<DataRow>();
                    GLOB_DataTable.Merge(dtsub);
                    BaseCls.GlbReportDataTable = GLOB_DataTable;
                }

            }
            //string expression = "SAM_PTY_TP like '%" + type + "%' and SAM_CAT like '%" + cat + "%'";
            //foundRows = TMP_DataTable_sub.Select(expression);
            //if (foundRows.Count() > 0)
            //{
            //    dtsub = foundRows.CopyToDataTable<DataRow>();
            //}

            //BaseCls.GlbReportDataTable = dtsub;


            //BaseCls.GlbReportDataTable = null;
            //foreach (var item in _lstVoucher)
            //{
            //    DataTable TMP_DataTable = new DataTable();
            //    TMP_DataTable = bsObj.CHNLSVC.Sales.Load_cheque_printing_details(item.ToString());
            //    GLOB_DataTable.Merge(TMP_DataTable);

            //    BaseCls.GlbReportDataTable = GLOB_DataTable;

            //}


        }

        public void Load_Area_dets()
        {// shanuka  30-09-2014

            DataTable param = new DataTable();
            DataRow dr;

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;

            param.Rows.Add(dr);


            _arePrn.Database.Tables["Area_tb"].SetDataSource(BaseCls.GlbReportDataTable);
            _arePrn.Database.Tables["param"].SetDataSource(param);

        }

        public void Load_Allocate_Items()
        {// shanuka  04-10-2014

            DataTable param = new DataTable();
            DataRow dr;
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;

            param.Rows.Add(dr);


            _allocateItm.Database.Tables["ItemAllocate"].SetDataSource(BaseCls.GlbReportDataTable);
            _allocateItm.Database.Tables["param"].SetDataSource(param);

        }

        public void Load_Service_loc_dets()
        {// shanuka  11-10-2014

            DataTable param = new DataTable();
            DataRow dr;

            DataTable dt = new DataTable();
            dt = bsObj.CHNLSVC.General.getAllservice_locDetails(BaseCls.GlbUserComCode, null, null);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;

            param.Rows.Add(dr);


            _serloc.Database.Tables["Ser_Loc"].SetDataSource(dt);
            _serloc.Database.Tables["param"].SetDataSource(param);

        }

        public void Load_Service_chnl_dets()
        {
            // shanuka  11-10-2014

            DataTable param = new DataTable();
            DataRow dr;

            DataTable dt = new DataTable();
            dt = bsObj.CHNLSVC.General.getAllservice_paraDets(BaseCls.GlbUserComCode);

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;

            param.Rows.Add(dr);


            _ser_chnlpara.Database.Tables["Ser_chnlPara"].SetDataSource(dt);
            _ser_chnlpara.Database.Tables["param"].SetDataSource(param);

        }


        public void Load_Region_dets()
        {// shanuka  30-09-2014

            DataTable param = new DataTable();
            DataRow dr;

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;

            param.Rows.Add(dr);


            _regPrn.Database.Tables["Region_dt"].SetDataSource(BaseCls.GlbReportDataTable);
            _regPrn.Database.Tables["param"].SetDataSource(param);

        }
        public void Load_Zone_dets()
        {// shanuka  30-09-2014

            DataTable param = new DataTable();
            DataRow dr;

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;

            param.Rows.Add(dr);


            _zonPrn.Database.Tables["Zone_dt"].SetDataSource(BaseCls.GlbReportDataTable);
            _zonPrn.Database.Tables["param"].SetDataSource(param);

        }





        public void Load_Sub_Channel_dets()
        {// shanuka  30-09-2014

            DataTable param = new DataTable();
            DataRow dr;

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;

            param.Rows.Add(dr);


            _sub_chnlPrn.Database.Tables["SubChnl"].SetDataSource(BaseCls.GlbReportDataTable);
            _sub_chnlPrn.Database.Tables["param"].SetDataSource(param);

        }


        public void Load_Channel_dets()
        {// shanuka  30-09-2014

            DataTable param = new DataTable();
            DataRow dr;

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;

            param.Rows.Add(dr);


            _chnlPrn.Database.Tables["Channel"].SetDataSource(BaseCls.GlbReportDataTable);
            _chnlPrn.Database.Tables["param"].SetDataSource(param);



        }





        public void SearchVoucher()
        {// shanuka  18-09-2014

            DataTable param = new DataTable();
            DataRow dr;

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("doctype", typeof(string));
            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["doctype"] = BaseCls.GlbRecType;
            param.Rows.Add(dr);

            _vouPrn.Database.Tables["VouPrinting"].SetDataSource(BaseCls.GlbReportDataTable);
            _vouPrn.Database.Tables["param"].SetDataSource(param);


        }
        public void SearchVoucherPrint()
        {// Nadeeka 13-01-2015

            DataTable param = new DataTable();
            DataRow dr;

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("doctype", typeof(string));
            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["doctype"] = BaseCls.GlbRecType;
            param.Rows.Add(dr);

            _vouPrnvou.Database.Tables["VouPrinting"].SetDataSource(BaseCls.GlbReportDataTable);
            _vouPrnvou.Database.Tables["param"].SetDataSource(param);


        }


        public void ECDVoucher(DataTable ECD_VOU_PRINT)
        {// Nadeeka  14-05-2013

            Reports.Sales.ReportViewer _view = new Reports.Sales.ReportViewer();

            //DataTable ECD_VOU_PRINT = bsObj.CHNLSVC.Sales.ECD_vouchers_Print(BaseCls.GlbReportDoc);


            DataTable PRINT_DOC = new DataTable();


            _ecdVou.Database.Tables["ECD_VOU_PRINT"].SetDataSource(ECD_VOU_PRINT);


            //_ecdVou.PrintOptions.PrinterName = _view.GetDefaultPrinter();
            //int papernbr = _view.getprintertnbr("Letter"); // returns 257 int

            //_ecdVou.PrintOptions.PaperSize = (CrystalDecisions.Shared.PaperSize)papernbr;
            //_ecdVou.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
            //_ecdVou.PrintToPrinter(1, false, 1, 2);

        }


        public void SparePartPrint()
        {// Nadeeka 26-12-12
            string invNo = default(string);
            invNo = BaseCls.GlbReportDoc;
            DataTable mst_tax_master = new DataTable();

            DataTable salesDetails = new DataTable();
            salesDetails.Clear();
            salesDetails = bsObj.CHNLSVC.Sales.GetSalesDetails(invNo, null);

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


            DataTable sar_sub_tp = new DataTable();
            DataTable PRINT_DOC = new DataTable();
            int _numPages = 0;
            DataRow dr3;

            _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;

            PRINT_DOC.Rows.Add(dr3);
            _sprPrint.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);


            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();


            DataRow dr;
            DataRow dr1;
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


            mst_profit_center.Columns.Add("MPC_ADD_1", typeof(string));
            mst_profit_center.Columns.Add("MPC_ADD_2", typeof(string));
            mst_profit_center.Columns.Add("MPC_CHNL", typeof(string));
            mst_profit_center.Columns.Add("MPC_DESC", typeof(string));
            mst_profit_center.Columns.Add("MPC_COM", typeof(string));
            mst_profit_center.Columns.Add("MPC_CD", typeof(string));



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
                    dr["SAH_ANAL_11"] = Convert.ToInt16(row["SAH_ANAL_11"].ToString());

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
                sat_itm.Rows.Add(dr);

                dr = mst_item.NewRow();
                dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                dr["MI_CD"] = row["MI_CD"].ToString();

                mst_item.Rows.Add(dr);

                if (index == 0)
                {


                    dr = mst_profit_center.NewRow();
                    dr["MPC_ADD_1"] = row["MPC_ADD_1"].ToString();
                    dr["MPC_ADD_2"] = row["MPC_ADD_2"].ToString();
                    dr["MPC_CHNL"] = row["MPC_CHNL"].ToString();
                    dr["MPC_DESC"] = row["MPC_DESC"].ToString();
                    dr["MPC_COM"] = row["MPC_COM"].ToString();
                    dr["MPC_CD"] = row["MPC_CD"].ToString();
                    mst_profit_center.Rows.Add(dr);


                    dr = mst_com.NewRow();
                    dr["MC_TAX1"] = row["MC_TAX1"].ToString();
                    dr["MC_TAX2"] = row["MC_TAX2"].ToString();
                    dr["MC_CD"] = row["MC_CD"].ToString();
                    dr["MC_DESC"] = row["MC_DESC"].ToString();
                    dr["MC_IT_POWERED"] = row["MC_IT_POWERED"].ToString();
                    mst_com.Rows.Add(dr);

                    dr = sec_user.NewRow();
                    dr["SE_USR_NAME"] = row["SE_USR_NAME"].ToString();
                    dr["SE_USR_ID"] = row["SE_USR_ID"].ToString();
                    sec_user.Rows.Add(dr);
                };

            }




            _sprPrint.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _sprPrint.Database.Tables["mst_com"].SetDataSource(mst_com);
            _sprPrint.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _sprPrint.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            _sprPrint.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            _sprPrint.Database.Tables["mst_item"].SetDataSource(mst_item);
            _sprPrint.Database.Tables["sec_user"].SetDataSource(sec_user);
            _sprPrint.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);







        }

        //public int InsuranceRegistration()
        //{// Nadeeka  16-03-2013

        //    int rtnValue = 0;

        //    DataTable veh_ins_txn = bsObj.CHNLSVC.Sales.ProcessVehicleInsurance(BaseCls.GlbUserID, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
        //    DataRow dr;
        //    DataTable param = new DataTable();

        //    param.Clear();

        //    param.Columns.Add("user", typeof(string));
        //    param.Columns.Add("fromdate", typeof(DateTime));
        //    param.Columns.Add("todate", typeof(DateTime));
        //    param.Columns.Add("profitcenter", typeof(string));
        //    param.Columns.Add("comp", typeof(string));
        //    param.Columns.Add("par1", typeof(Int16));
        //    param.Columns.Add("par2", typeof(Int16));
        //    param.Columns.Add("insPay", typeof(Int16));
        //    param.Columns.Add("insclaim", typeof(Int16));




        //    DataTable VehicleInsPay = new DataTable();
        //    DataTable vehicleInsClaims = new DataTable();
        //    DataTable mst_com = new DataTable();
        //    DataTable VehicleInsPay1 = new DataTable();
        //    DataTable vehicleInsClaims1 = new DataTable();


        //    DataTable hpt_acc = new DataTable();
        //    DataTable hpt_acc1 = new DataTable();
        //    hpt_acc.Columns.Add("HPA_ACC_NO", typeof(string));
        //    hpt_acc.Columns.Add("HPA_TERM", typeof(decimal));

        //    mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);

        //    VehicleInsPay.Columns.Add("SVIPY_SEQ", typeof(string));
        //    VehicleInsPay.Columns.Add("SVIPY_PAY_REF_NO", typeof(string));
        //    VehicleInsPay.Columns.Add("SVIPY_PAY_REF_LINE", typeof(Int16));
        //    VehicleInsPay.Columns.Add("SVIPY_PAY_TP", typeof(string));
        //    VehicleInsPay.Columns.Add("SVIPY_REF_NO", typeof(string));
        //    VehicleInsPay.Columns.Add("SVIPY_BANK", typeof(string));
        //    VehicleInsPay.Columns.Add("SVIPY_BANK_BRANCH", typeof(string));
        //    VehicleInsPay.Columns.Add("SVIPY_CHQ_DT", typeof(DateTime));
        //    VehicleInsPay.Columns.Add("SVIPY_VAL", typeof(double));
        //    VehicleInsPay.Columns.Add("SVIPY_CRE_BY", typeof(string));
        //    VehicleInsPay.Columns.Add("SVIPY_CRE_DT", typeof(DateTime));
        //    VehicleInsPay.Columns.Add("SVIT_VEH_REG_NO", typeof(string));



        //    vehicleInsClaims.Columns.Add("SVICL_VEH_REG_NO", typeof(string));
        //    vehicleInsClaims.Columns.Add("SVICL_ACDT_DT", typeof(DateTime));
        //    vehicleInsClaims.Columns.Add("SVICL_CLAIM_VAL", typeof(double));
        //    vehicleInsClaims.Columns.Add("SVICL_REPAIR_ESTM_VAL", typeof(double));
        //    vehicleInsClaims.Columns.Add("SVICL_POLC_EXCESS", typeof(double));
        //    vehicleInsClaims.Columns.Add("SVICL_OTH_DEDUCTION", typeof(double));
        //    vehicleInsClaims.Columns.Add("SVICL_BAL_VAL", typeof(double));
        //    vehicleInsClaims.Columns.Add("SVICL_ACC_NO", typeof(string));
        //    vehicleInsClaims.Columns.Add("SVICL_CHQ_NO", typeof(string));
        //    vehicleInsClaims.Columns.Add("SVICL_CHQ_BANK", typeof(string));
        //    vehicleInsClaims.Columns.Add("SVICL_INIT_DT", typeof(DateTime));
        //    vehicleInsClaims.Columns.Add("SVICL_CLAIM_FORM_REC_DT", typeof(DateTime));
        //    vehicleInsClaims.Columns.Add("SVICL_DL_REC_DT", typeof(DateTime));
        //    vehicleInsClaims.Columns.Add("SVICL_REPAIR_ESTM_REC_DT", typeof(DateTime));
        //    vehicleInsClaims.Columns.Add("SVICL_POLICE_RPT_REC_DT", typeof(DateTime));
        //    vehicleInsClaims.Columns.Add("SVICL_APP_DT", typeof(DateTime));
        //    vehicleInsClaims.Columns.Add("SVICL_INV_DT", typeof(DateTime));

        //    foreach (DataRow row in veh_ins_txn.Rows)
        //    {
        //        VehicleInsPay1 = bsObj.CHNLSVC.Sales.ProcessVehicleInsPay(row["svit_veh_reg_no"].ToString());
        //        vehicleInsClaims1 = bsObj.CHNLSVC.Sales.ProcessVehicleInsClaims(row["svit_veh_reg_no"].ToString());
        //        hpt_acc1 = bsObj.CHNLSVC.Sales.GetAccountDetails(row["sah_inv_no"].ToString());
        //        if (VehicleInsPay1.Rows.Count == 0)
        //        {
        //            dr = VehicleInsPay.NewRow();
        //            dr["SVIPY_SEQ"] = '0';

        //            VehicleInsPay.Rows.Add(dr);
        //        }
        //        if (vehicleInsClaims1.Rows.Count == 0)
        //        {
        //            dr = vehicleInsClaims.NewRow();
        //            dr["SVICL_VEH_REG_NO"] = '0';

        //            vehicleInsClaims.Rows.Add(dr);
        //        }
        //        foreach (DataRow row1 in VehicleInsPay1.Rows)
        //        {
        //            dr = VehicleInsPay.NewRow();
        //            dr["SVIPY_SEQ"] = row1["SVIPY_SEQ"].ToString();
        //            dr["SVIPY_PAY_REF_NO"] = row1["SVIPY_PAY_REF_NO"].ToString();
        //            dr["SVIPY_PAY_REF_LINE"] = row1["SVIPY_PAY_REF_LINE"].ToString();
        //            dr["SVIPY_PAY_TP"] = row1["SVIPY_PAY_TP"].ToString();
        //            dr["SVIPY_REF_NO"] = row1["SVIPY_REF_NO"].ToString();
        //            dr["SVIPY_BANK"] = row1["SVIPY_BANK"].ToString();
        //            dr["SVIPY_BANK_BRANCH"] = row1["SVIPY_BANK_BRANCH"].ToString();
        //            dr["SVIPY_CHQ_DT"] = row1["SVIPY_CHQ_DT"].ToString();
        //            dr["SVIPY_VAL"] = row1["SVIPY_VAL"].ToString();
        //            dr["SVIPY_CRE_BY"] = row1["SVIPY_CRE_BY"].ToString();
        //            dr["SVIPY_CRE_DT"] = row1["SVIPY_CRE_DT"].ToString();
        //            dr["SVIT_VEH_REG_NO"] = row1["SVIT_VEH_REG_NO"].ToString();
        //            VehicleInsPay.Rows.Add(dr);
        //        }

        //        foreach (DataRow row2 in vehicleInsClaims1.Rows)
        //        {
        //            dr = vehicleInsClaims.NewRow();
        //            dr["SVICL_VEH_REG_NO"] = row2["SVICL_VEH_REG_NO"].ToString();
        //            dr["SVICL_ACDT_DT"] = row2["SVICL_ACDT_DT"].ToString();
        //            dr["SVICL_CLAIM_VAL"] = row2["SVICL_CLAIM_VAL"].ToString();
        //            dr["SVICL_REPAIR_ESTM_VAL"] = row2["SVICL_REPAIR_ESTM_VAL"].ToString();
        //            dr["SVICL_POLC_EXCESS"] = row2["SVICL_POLC_EXCESS"].ToString();
        //            dr["SVICL_OTH_DEDUCTION"] = row2["SVICL_OTH_DEDUCTION"].ToString();
        //            dr["SVICL_BAL_VAL"] = row2["SVICL_BAL_VAL"].ToString();
        //            dr["SVICL_ACC_NO"] = row2["SVICL_ACC_NO"].ToString();
        //            dr["SVICL_CHQ_NO"] = row2["SVICL_CHQ_NO"].ToString();
        //            dr["SVICL_CHQ_BANK"] = row2["SVICL_CHQ_BANK"].ToString();
        //            dr["SVICL_INIT_DT"] = row2["SVICL_INIT_DT"].ToString();
        //            dr["SVICL_CLAIM_FORM_REC_DT"] = row2["SVICL_CLAIM_FORM_REC_DT"].ToString();
        //            dr["SVICL_DL_REC_DT"] = row2["SVICL_DL_REC_DT"].ToString();
        //            dr["SVICL_REPAIR_ESTM_REC_DT"] = row2["SVICL_REPAIR_ESTM_REC_DT"].ToString();
        //            dr["SVICL_POLICE_RPT_REC_DT"] = row2["SVICL_POLICE_RPT_REC_DT"].ToString();
        //            dr["SVICL_APP_DT"] = row2["SVICL_APP_DT"].ToString();
        //            dr["SVICL_INV_DT"] = row2["SVICL_INV_DT"].ToString();
        //            vehicleInsClaims.Rows.Add(dr);

        //        }
        //        foreach (DataRow row3 in hpt_acc1.Rows)
        //        {
        //            dr = hpt_acc.NewRow();
        //            dr["HPA_ACC_NO"] = row3["HPA_ACC_NO"].ToString();
        //            dr["HPA_TERM"] = row3["HPA_TERM"].ToString();

        //            hpt_acc.Rows.Add(dr);
        //        }


        //    };
        //    if (veh_ins_txn.Rows.Count == 0)
        //    { rtnValue = 1; }
        //    if (veh_ins_txn.Rows.Count > 0)
        //    {

        //        dr = param.NewRow();
        //        dr["user"] = BaseCls.GlbUserID;
        //        dr["fromdate"] = BaseCls.GlbReportFromDate;
        //        dr["todate"] = BaseCls.GlbReportToDate;
        //        dr["profitcenter"] = BaseCls.GlbReportProfit;
        //        dr["comp"] = BaseCls.GlbReportComp;
        //        dr["par1"] = BaseCls.GlbwithClaims;
        //        dr["par2"] = BaseCls.GlbwithSettle;
        //        if (VehicleInsPay.Rows.Count > 0)
        //        { dr["inspay"] = 1; }
        //        else
        //        { dr["inspay"] = 0; }

        //        if (vehicleInsClaims.Rows.Count > 0)
        //        { dr["insclaim"] = 1; }
        //        else
        //        { dr["insclaim"] = 0; }
        //        param.Rows.Add(dr);

        //        _insReg.Database.Tables["veh_ins_txn"].SetDataSource(veh_ins_txn);
        //        _insReg.Database.Tables["mst_com"].SetDataSource(mst_com);
        //        _insReg.Database.Tables["param"].SetDataSource(param);


        //        vehicleInsClaims = vehicleInsClaims.DefaultView.ToTable(true);
        //        VehicleInsPay = VehicleInsPay.DefaultView.ToTable(true);

        //        foreach (object repOp in _insReg.ReportDefinition.ReportObjects)
        //        {
        //            string _s = repOp.GetType().ToString();
        //            if (_s.ToLower().Contains("subreport"))
        //            {
        //                SubreportObject _cs = (SubreportObject)repOp;
        //                if (_cs.SubreportName == "inspay")
        //                {
        //                    ReportDocument subRepDoc = _insReg.Subreports[_cs.SubreportName];

        //                    subRepDoc.Database.Tables["VehicleInsPay"].SetDataSource(VehicleInsPay);

        //                }
        //                if (_cs.SubreportName == "insclaims")
        //                {
        //                    ReportDocument subRepDoc = _insReg.Subreports[_cs.SubreportName];

        //                    subRepDoc.Database.Tables["vehicleInsClaims"].SetDataSource(vehicleInsClaims);

        //                }
        //                if (_cs.SubreportName == "term")
        //                {
        //                    ReportDocument subRepDoc = _insReg.Subreports[_cs.SubreportName];

        //                    subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);

        //                }
        //            }
        //        }
        //    }
        //    return rtnValue;
        //}
        public int InsuranceRegistrationnew()
        {// Nadeeka  16-03-2013

            int rtnValue = 0;

            DataTable veh_ins_txn = bsObj.CHNLSVC.MsgPortal.ProcessVehicleInsurance(BaseCls.GlbUserID, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
            DataRow dr;
            DataTable param = new DataTable();

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("par1", typeof(Int16));
            param.Columns.Add("par2", typeof(Int16));
            param.Columns.Add("insPay", typeof(Int16));
            param.Columns.Add("insclaim", typeof(Int16));




            DataTable VehicleInsPay = new DataTable();
            DataTable vehicleInsClaims = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable VehicleInsPay1 = new DataTable();
            DataTable vehicleInsClaims1 = new DataTable();


            DataTable hpt_acc = new DataTable();
            DataTable hpt_acc1 = new DataTable();
            hpt_acc.Columns.Add("HPA_ACC_NO", typeof(string));
            hpt_acc.Columns.Add("HPA_TERM", typeof(decimal));

            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);

            foreach (DataRow row in veh_ins_txn.Rows)
            {
                hpt_acc1 = bsObj.CHNLSVC.Sales.GetAccountDetails(row["sah_inv_no"].ToString());

                foreach (DataRow row3 in hpt_acc1.Rows)
                {
                    dr = hpt_acc.NewRow();
                    dr["HPA_ACC_NO"] = row3["HPA_ACC_NO"].ToString();
                    dr["HPA_TERM"] = row3["HPA_TERM"].ToString();

                    hpt_acc.Rows.Add(dr);
                }


            };
            if (veh_ins_txn.Rows.Count == 0)
            { rtnValue = 1; }
            if (veh_ins_txn.Rows.Count > 0)
            {

                dr = param.NewRow();
                dr["user"] = BaseCls.GlbUserID;
                dr["fromdate"] = BaseCls.GlbReportFromDate;
                dr["todate"] = BaseCls.GlbReportToDate;
                dr["profitcenter"] = BaseCls.GlbReportProfit;
                dr["comp"] = BaseCls.GlbReportComp;
                param.Rows.Add(dr);

                if (BaseCls.GlbReportName == "HPInsuranceRegisterNew.rpt")
                {

                    _insRegnew.Database.Tables["veh_ins_txn"].SetDataSource(veh_ins_txn);
                    _insRegnew.Database.Tables["mst_com"].SetDataSource(mst_com);
                    _insRegnew.Database.Tables["param"].SetDataSource(param);
                    foreach (object repOp in _insRegnew.ReportDefinition.ReportObjects)
                    {
                        string _s = repOp.GetType().ToString();
                        if (_s.ToLower().Contains("subreport"))
                        {
                            SubreportObject _cs = (SubreportObject)repOp;

                            if (_cs.SubreportName == "term")
                            {
                                ReportDocument subRepDoc = _insRegnew.Subreports[_cs.SubreportName];

                                subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);

                            }
                        }
                    }
                }

                else if (BaseCls.GlbReportName == "HPInsurancePolicyReport.rpt")
                {
                    _insRegPol.Database.Tables["veh_ins_txn"].SetDataSource(veh_ins_txn);
                    _insRegPol.Database.Tables["mst_com"].SetDataSource(mst_com);
                    _insRegPol.Database.Tables["param"].SetDataSource(param);


                }

            }
            return rtnValue;
        }
        public int InsuranceRegistrationClaims()
        {// Nadeeka  16-03-2013

            int rtnValue = 0;

            DataTable veh_ins_txn = bsObj.CHNLSVC.MsgPortal.ProcessVehicleInsurance(BaseCls.GlbUserID, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
            DataTable vehicleInsClaims = bsObj.CHNLSVC.MsgPortal.ProcessVehicleInsClaims(BaseCls.GlbUserID, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            DataRow dr;
            DataTable param = new DataTable();

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("par1", typeof(Int16));
            param.Columns.Add("par2", typeof(Int16));
            param.Columns.Add("insPay", typeof(Int16));
            param.Columns.Add("insclaim", typeof(Int16));




            DataTable VehicleInsPay = new DataTable();

            DataTable mst_com = new DataTable();
            DataTable VehicleInsPay1 = new DataTable();
            DataTable vehicleInsClaims1 = new DataTable();


            DataTable hpt_acc = new DataTable();
            DataTable hpt_acc1 = new DataTable();
            hpt_acc.Columns.Add("HPA_ACC_NO", typeof(string));
            hpt_acc.Columns.Add("HPA_TERM", typeof(decimal));

            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);

            foreach (DataRow row in veh_ins_txn.Rows)
            {
                hpt_acc1 = bsObj.CHNLSVC.Sales.GetAccountDetails(row["sah_inv_no"].ToString());

                foreach (DataRow row3 in hpt_acc1.Rows)
                {
                    dr = hpt_acc.NewRow();
                    dr["HPA_ACC_NO"] = row3["HPA_ACC_NO"].ToString();
                    dr["HPA_TERM"] = row3["HPA_TERM"].ToString();

                    hpt_acc.Rows.Add(dr);
                }


            };
            if (veh_ins_txn.Rows.Count == 0)
            { rtnValue = 1; }
            if (veh_ins_txn.Rows.Count > 0)
            {

                dr = param.NewRow();
                dr["user"] = BaseCls.GlbUserID;
                dr["fromdate"] = BaseCls.GlbReportFromDate;
                dr["todate"] = BaseCls.GlbReportToDate;
                dr["profitcenter"] = BaseCls.GlbReportProfit;
                dr["comp"] = BaseCls.GlbReportComp;
                param.Rows.Add(dr);
                if (BaseCls.GlbReportName == "HPInsuranceClaimRegister.rpt")
                {
                    _insRegClaim.Database.Tables["veh_ins_txn"].SetDataSource(veh_ins_txn);
                    _insRegClaim.Database.Tables["vehicleInsClaims"].SetDataSource(vehicleInsClaims);
                    _insRegClaim.Database.Tables["mst_com"].SetDataSource(mst_com);
                    _insRegClaim.Database.Tables["param"].SetDataSource(param);
                }
                else if (BaseCls.GlbReportName == "HPInsuranceDocumentRequired.rpt")
                {
                    _insRegDoc.Database.Tables["veh_ins_txn"].SetDataSource(veh_ins_txn);
                    _insRegDoc.Database.Tables["vehicleInsClaims"].SetDataSource(vehicleInsClaims);
                    _insRegDoc.Database.Tables["mst_com"].SetDataSource(mst_com);
                    _insRegDoc.Database.Tables["param"].SetDataSource(param);
                }




            }
            return rtnValue;
        }
        public int InsuranceRegistrationSettlements()
        {// Nadeeka  16-03-2013

            int rtnValue = 0;

            //  DataTable veh_ins_txn = bsObj.CHNLSVC.Sales.ProcessVehicleInsurance(BaseCls.GlbUserID, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
            DataTable VehicleInsPay = bsObj.CHNLSVC.MsgPortal.ProcessVehicleInsPay(BaseCls.GlbUserID, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            DataRow dr;
            DataTable param = new DataTable();

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("par1", typeof(Int16));
            param.Columns.Add("par2", typeof(Int16));
            param.Columns.Add("insPay", typeof(Int16));
            param.Columns.Add("insclaim", typeof(Int16));






            DataTable mst_com = new DataTable();
            DataTable VehicleInsPay1 = new DataTable();
            DataTable vehicleInsClaims1 = new DataTable();


            //DataTable hpt_acc = new DataTable();
            //DataTable hpt_acc1 = new DataTable();
            //hpt_acc.Columns.Add("HPA_ACC_NO", typeof(string));
            //hpt_acc.Columns.Add("HPA_TERM", typeof(decimal));

            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);

            //foreach (DataRow row in veh_ins_txn.Rows)
            //{
            //    hpt_acc1 = bsObj.CHNLSVC.Sales.GetAccountDetails(row["sah_inv_no"].ToString());

            //    foreach (DataRow row3 in hpt_acc1.Rows)
            //    {
            //        dr = hpt_acc.NewRow();
            //        dr["HPA_ACC_NO"] = row3["HPA_ACC_NO"].ToString();
            //        dr["HPA_TERM"] = row3["HPA_TERM"].ToString();

            //        hpt_acc.Rows.Add(dr);
            //    }


            //};
            if (VehicleInsPay.Rows.Count == 0)
            { rtnValue = 1; }
            if (VehicleInsPay.Rows.Count > 0)
            {

                dr = param.NewRow();
                dr["user"] = BaseCls.GlbUserID;
                dr["fromdate"] = BaseCls.GlbReportFromDate;
                dr["todate"] = BaseCls.GlbReportToDate;
                dr["profitcenter"] = BaseCls.GlbReportProfit;
                dr["comp"] = BaseCls.GlbReportComp;
                param.Rows.Add(dr);


                //   _insRegSett.Database.Tables["veh_ins_txn"].SetDataSource(veh_ins_txn);
                _insRegSett.Database.Tables["VehicleInsPay"].SetDataSource(VehicleInsPay);
                _insRegSett.Database.Tables["mst_com"].SetDataSource(mst_com);
                _insRegSett.Database.Tables["param"].SetDataSource(param);




            }
            return rtnValue;
        }


        public int InsuranceCollection()
        {// Nadeeka  02-07-2015

            int rtnValue = 0;
            DataTable glb_vehicle_insu = new DataTable();

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.ProcessVehicleColletion(drow["tpl_pc"].ToString(), BaseCls.GlbUserComCode, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
                    glb_vehicle_insu.Merge(TMP_DataTable);
                }
            }


            DataRow dr;
            DataTable param = new DataTable();

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("par1", typeof(Int16));
            param.Columns.Add("par2", typeof(Int16));
            param.Columns.Add("insPay", typeof(Int16));
            param.Columns.Add("insclaim", typeof(Int16));






            DataTable mst_com = new DataTable();
            DataTable VehicleInsPay1 = new DataTable();
            DataTable vehicleInsClaims1 = new DataTable();


            //};
            if (glb_vehicle_insu.Rows.Count == 0)
            { rtnValue = 1; }
            if (glb_vehicle_insu.Rows.Count > 0)
            {

                dr = param.NewRow();
                dr["user"] = BaseCls.GlbUserID;
                dr["fromdate"] = BaseCls.GlbReportFromDate;
                dr["todate"] = BaseCls.GlbReportToDate;
                dr["profitcenter"] = BaseCls.GlbReportProfit;
                dr["comp"] = BaseCls.GlbReportComp;
                param.Rows.Add(dr);



                _vehicleInsCollect.Database.Tables["glb_vehicle_insu"].SetDataSource(glb_vehicle_insu);
                _vehicleInsCollect.Database.Tables["param"].SetDataSource(param);




            }
            return rtnValue;
        }



        public int InsurancePolicy()
        {// Nadeeka  16-03-2013

            int rtnValue = 0;

            DataTable veh_ins_txn = bsObj.CHNLSVC.MsgPortal.ProcessVehicleInsurance(BaseCls.GlbUserID, BaseCls.GlbReportComp, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);

            DataRow dr;
            DataTable param = new DataTable();

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("par1", typeof(Int16));
            param.Columns.Add("par2", typeof(Int16));
            param.Columns.Add("insPay", typeof(Int16));
            param.Columns.Add("insclaim", typeof(Int16));




            DataTable VehicleInsPay = new DataTable();
            DataTable vehicleInsClaims = new DataTable();
            DataTable mst_com = new DataTable();
            DataTable VehicleInsPay1 = new DataTable();
            DataTable vehicleInsClaims1 = new DataTable();


            DataTable hpt_acc = new DataTable();
            DataTable hpt_acc1 = new DataTable();
            hpt_acc.Columns.Add("HPA_ACC_NO", typeof(string));
            hpt_acc.Columns.Add("HPA_TERM", typeof(decimal));

            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);

            foreach (DataRow row in veh_ins_txn.Rows)
            {
                hpt_acc1 = bsObj.CHNLSVC.Sales.GetAccountDetails(row["sah_inv_no"].ToString());

                foreach (DataRow row3 in hpt_acc1.Rows)
                {
                    dr = hpt_acc.NewRow();
                    dr["HPA_ACC_NO"] = row3["HPA_ACC_NO"].ToString();
                    dr["HPA_TERM"] = row3["HPA_TERM"].ToString();

                    hpt_acc.Rows.Add(dr);
                }


            };
            if (veh_ins_txn.Rows.Count == 0)
            { rtnValue = 1; }
            if (veh_ins_txn.Rows.Count > 0)
            {

                dr = param.NewRow();
                dr["user"] = BaseCls.GlbUserID;
                dr["fromdate"] = BaseCls.GlbReportFromDate;
                dr["todate"] = BaseCls.GlbReportToDate;
                dr["profitcenter"] = BaseCls.GlbReportProfit;
                dr["comp"] = BaseCls.GlbReportComp;
                param.Rows.Add(dr);

                _insRegnew.Database.Tables["veh_ins_txn"].SetDataSource(veh_ins_txn);
                _insRegnew.Database.Tables["mst_com"].SetDataSource(mst_com);
                _insRegnew.Database.Tables["param"].SetDataSource(param);



                foreach (object repOp in _insRegnew.ReportDefinition.ReportObjects)
                {
                    string _s = repOp.GetType().ToString();
                    if (_s.ToLower().Contains("subreport"))
                    {
                        SubreportObject _cs = (SubreportObject)repOp;

                        if (_cs.SubreportName == "term")
                        {
                            ReportDocument subRepDoc = _insRegnew.Subreports[_cs.SubreportName];

                            subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);

                        }
                    }
                }
            }
            return rtnValue;
        }
        public int ManualDocuments()
        {// Nadeeka  14-08-2013

            int rtnValue = 0;
            DataTable PROC_CANCEL_DOCUMENT = bsObj.CHNLSVC.MsgPortal.GetManualDocuemnt(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportProfit, BaseCls.GlbReportComp, BaseCls.GlbReportDocType);


            DataRow dr;
            DataTable param = new DataTable();
            if (PROC_CANCEL_DOCUMENT.Rows.Count == 0)
            { rtnValue = 1; }
            if (PROC_CANCEL_DOCUMENT.Rows.Count > 0)
            {
                param.Clear();

                param.Columns.Add("user", typeof(string));
                param.Columns.Add("fromdate", typeof(DateTime));
                param.Columns.Add("todate", typeof(DateTime));
                param.Columns.Add("profitcenter", typeof(string));
                param.Columns.Add("comp", typeof(string));
                param.Columns.Add("compaddr", typeof(string));




                DataTable mst_com = new DataTable();


                mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);


                dr = param.NewRow();
                dr["user"] = BaseCls.GlbUserID;
                dr["fromdate"] = BaseCls.GlbReportFromDate;
                dr["todate"] = BaseCls.GlbReportToDate;
                dr["profitcenter"] = BaseCls.GlbReportProfit;
                dr["comp"] = BaseCls.GlbReportComp;
                dr["compaddr"] = BaseCls.GlbReportDocType;
                param.Rows.Add(dr);

                _manDoc.Database.Tables["PROC_CANCEL_DOCUMENT"].SetDataSource(PROC_CANCEL_DOCUMENT);
                _manDoc.Database.Tables["mst_com"].SetDataSource(mst_com);
                _manDoc.Database.Tables["param"].SetDataSource(param);
                rtnValue = 0;
            }

            return rtnValue;
        }
        public int CancelledDocuments()
        {// Nadeeka  14-08-2013

            int rtnValue = 0;
            DataTable PROC_CANCEL_DOCUMENT = bsObj.CHNLSVC.MsgPortal.GetCancelledDocuemnt(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportProfit, BaseCls.GlbReportComp, BaseCls.GlbReportDocType);


            DataRow dr;
            DataTable param = new DataTable();
            if (PROC_CANCEL_DOCUMENT.Rows.Count == 0)
            { rtnValue = 1; }
            if (PROC_CANCEL_DOCUMENT.Rows.Count > 0)
            {
                param.Clear();

                param.Columns.Add("user", typeof(string));
                param.Columns.Add("fromdate", typeof(DateTime));
                param.Columns.Add("todate", typeof(DateTime));
                param.Columns.Add("profitcenter", typeof(string));
                param.Columns.Add("comp", typeof(string));
                param.Columns.Add("compaddr", typeof(string));




                DataTable mst_com = new DataTable();


                mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportComp);


                dr = param.NewRow();
                dr["user"] = BaseCls.GlbUserID;
                dr["fromdate"] = BaseCls.GlbReportFromDate;
                dr["todate"] = BaseCls.GlbReportToDate;
                dr["profitcenter"] = BaseCls.GlbReportProfit;
                dr["comp"] = BaseCls.GlbReportComp;
                dr["compaddr"] = BaseCls.GlbReportDocType;
                param.Rows.Add(dr);

                _canDoc.Database.Tables["PROC_CANCEL_DOCUMENT"].SetDataSource(PROC_CANCEL_DOCUMENT);
                _canDoc.Database.Tables["mst_com"].SetDataSource(mst_com);
                _canDoc.Database.Tables["param"].SetDataSource(param);
                rtnValue = 0;
            }

            return rtnValue;
        }
        public int ReturnChequeSettlemet()
        {
            int rowcnt = 0;

            string _type = "";
            string _value = "";
            decimal _intVal = 0;
            decimal _outstanding = 0;
            decimal _actValue = 0;
            decimal _payments = 0;
            decimal _intRate60 = 0;
            decimal _intRate90 = 0;
            DataRow dr;

            DataTable RETURN_CHQ_SETTE_NEW = new DataTable();
            DataTable tmp_user_pc = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable RETURN_CHQ_SETTE = new DataTable();
                    RETURN_CHQ_SETTE.Clear();

                    RETURN_CHQ_SETTE.Columns.Add("FIELD_CODE", typeof(string));
                    RETURN_CHQ_SETTE.Columns.Add("FIELD_MGR", typeof(string));
                    RETURN_CHQ_SETTE.Columns.Add("REGION", typeof(string));
                    RETURN_CHQ_SETTE.Columns.Add("REGION_MGR", typeof(string));
                    RETURN_CHQ_SETTE.Columns.Add("ZONE", typeof(string));
                    RETURN_CHQ_SETTE.Columns.Add("ZONE_MGR", typeof(string));
                    RETURN_CHQ_SETTE.Columns.Add("CHQ_NO", typeof(string));
                    RETURN_CHQ_SETTE.Columns.Add("BANK", typeof(string));
                    RETURN_CHQ_SETTE.Columns.Add("BRANCH", typeof(string));
                    RETURN_CHQ_SETTE.Columns.Add("RETURN_DATE", typeof(DateTime));
                    RETURN_CHQ_SETTE.Columns.Add("CHEQUE_VALUE", typeof(double));
                    RETURN_CHQ_SETTE.Columns.Add("OUTSTANDING", typeof(double));
                    RETURN_CHQ_SETTE.Columns.Add("LESS30", typeof(double));
                    RETURN_CHQ_SETTE.Columns.Add("GRATEER30", typeof(double));
                    RETURN_CHQ_SETTE.Columns.Add("INTAMT", typeof(double));
                    RETURN_CHQ_SETTE.Columns.Add("DUEAMT", typeof(double));
                    RETURN_CHQ_SETTE.Columns.Add("0-30", typeof(double));
                    RETURN_CHQ_SETTE.Columns.Add("30-60", typeof(double));
                    RETURN_CHQ_SETTE.Columns.Add("60-90", typeof(double));
                    RETURN_CHQ_SETTE.Columns.Add("90-120", typeof(double));
                    RETURN_CHQ_SETTE.Columns.Add("PRFIT", typeof(string));
                    RETURN_CHQ_SETTE.Columns.Add("PRFITDES", typeof(string));
                    RETURN_CHQ_SETTE.Columns.Add("120-150", typeof(double));
                    RETURN_CHQ_SETTE.Columns.Add("150-180", typeof(double));
                    RETURN_CHQ_SETTE.Columns.Add("over180", typeof(double));
                    Int32 _dueDays1 = 0;

                    DataTable sat_rtn_chq = new DataTable();
                    DataTable sat_rtn_chqSet = new DataTable();

                    sat_rtn_chq = bsObj.CHNLSVC.MsgPortal.getReturnChequeSettlemts(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, BaseCls.GlbUserID, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString());

                    foreach (DataRow row in sat_rtn_chq.Rows)
                    {
                        _intVal = 0;
                        List<MasterSalesPriorityHierarchy> _hir2 = bsObj.CHNLSVC.Sales.GetSalesPriorityHierarchyWithDescription(BaseCls.GlbUserComCode, row["SRCQ_PC"].ToString());
                        if (_hir2 != null)
                        {
                            if (_hir2.Count > 0)
                            {
                                foreach (MasterSalesPriorityHierarchy _two in _hir2)
                                {
                                    _type = _two.Mpi_cd;
                                    _value = _two.Mpi_val;


                                    DateTime _chqRtnDt = Convert.ToDateTime(row["SRCQ_RTN_DT"].ToString()).Date;


                                    Int32 _dueDays = Convert.ToInt32((Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date - Convert.ToDateTime(_chqRtnDt).Date).TotalDays);
                                    _dueDays1 = _dueDays;
                                    List<HpServiceCharges> _ser = bsObj.CHNLSVC.Sales.getServiceChargesNew(_type, _value, "RTNCHQCUS", Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date);
                                    if (_ser != null)
                                    {
                                        foreach (HpServiceCharges _ser1 in _ser)
                                        {



                                            if (_ser1.Hps_from_val <= 60 && _ser1.Hps_from_val > 30)
                                            {
                                                _intRate60 = _ser1.Hps_rt;
                                            }

                                            if (_ser1.Hps_from_val <= 90 && _ser1.Hps_from_val > 60)
                                            {
                                                _intRate90 = _ser1.Hps_rt;
                                            }
                                            _payments = 0;
                                            sat_rtn_chqSet = bsObj.CHNLSVC.Sales.getReturnChequePayments(Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date, row["srcq_ref"].ToString());
                                            foreach (DataRow row1 in sat_rtn_chqSet.Rows)
                                            {
                                                if (row1["tot"].ToString() != "")
                                                {
                                                    _payments = (Convert.ToDecimal(row1["tot"].ToString()));
                                                }
                                            }

                                            _outstanding = (Convert.ToDecimal(row["SRCQ_ACT_VAL"].ToString())) - _payments;

                                            if (_ser1.Hps_from_val <= _dueDays)
                                            {

                                                //check whether already cal interst for this
                                                List<ReturnChequeCalInterest> _tmpCalInt = new List<ReturnChequeCalInterest>();
                                                _tmpCalInt = bsObj.CHNLSVC.Financial.get_calrtn_int(BaseCls.GlbUserComCode, row["SRCQ_PC"].ToString(), row["SRCQ_CHQ"].ToString(), row["SRCQ_BANK"].ToString(), Convert.ToInt32(_ser1.Hps_from_val), Convert.ToInt32(_ser1.Hps_to_val), Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date);

                                                if (_tmpCalInt == null)
                                                {







                                                    _intVal = _intVal + ((Convert.ToDecimal(_outstanding) * _ser1.Hps_rt / 100) + _ser1.Hps_chg);

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if ((Convert.ToDecimal(row["SRCQ_ACT_VAL"].ToString())) - _payments + _intVal > 0)
                        {
                            dr = RETURN_CHQ_SETTE.NewRow();
                            DataTable LocDes = new DataTable();
                            DataTable BankName = new DataTable();
                            Boolean AREA = false;
                            Boolean REGION = false;
                            Boolean ZONE = false;

                            List<MasterSalesPriorityHierarchyLog> _hirL = bsObj.CHNLSVC.Sales.GetSalesPriorityHierarchyWithDescription_log(BaseCls.GlbUserComCode, row["SRCQ_PC"].ToString(), Convert.ToDateTime(BaseCls.GlbReportAsAtDate).Date);
                            if (_hirL != null)
                            {
                                if (_hirL.Count > 0)
                                {
                                    foreach (MasterSalesPriorityHierarchyLog _loch in _hirL)
                                    {


                                        if (_loch.Mpil_cd == "AREA")
                                        {
                                            dr["FIELD_CODE"] = _loch.Mpil_val;

                                            LocDes = bsObj.CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "AREA", _loch.Mpil_val);
                                            foreach (DataRow row2 in LocDes.Rows)
                                            {
                                                dr["FIELD_MGR"] = row2["descp"].ToString();
                                                AREA = true;
                                            }
                                        }

                                        if (_loch.Mpil_cd == "REGION")
                                        {
                                            dr["REGION"] = _loch.Mpil_val;

                                            LocDes = bsObj.CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "REGION", _loch.Mpil_val);
                                            foreach (DataRow row2 in LocDes.Rows)
                                            {
                                                dr["REGION_MGR"] = row2["descp"].ToString();
                                                REGION = true;
                                            }
                                        }



                                        if (_loch.Mpil_cd == "ZONE")
                                        {
                                            dr["ZONE"] = _loch.Mpil_val;

                                            LocDes = bsObj.CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "ZONE", _loch.Mpil_val);
                                            foreach (DataRow row2 in LocDes.Rows)
                                            {
                                                dr["ZONE_MGR"] = row2["descp"].ToString();
                                                ZONE = true;
                                            }
                                        }
                                    }
                                }
                            }


                            List<MasterSalesPriorityHierarchy> _hirLl = bsObj.CHNLSVC.Sales.GetSalesPriorityHierarchyWithDescription(BaseCls.GlbUserComCode, row["SRCQ_PC"].ToString());
                            if (_hirLl.Count > 0)
                            {
                                foreach (MasterSalesPriorityHierarchy _loch in _hirLl)
                                {


                                    if (_loch.Mpi_cd == "AREA")
                                    {
                                        if (AREA == false)
                                        {
                                            dr["FIELD_CODE"] = _loch.Mpi_val;

                                            LocDes = bsObj.CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "AREA", _loch.Mpi_val);
                                            foreach (DataRow row2 in LocDes.Rows)
                                            {
                                                dr["FIELD_MGR"] = row2["descp"].ToString();

                                            }
                                        }
                                    }

                                    if (_loch.Mpi_cd == "REGION")
                                    {
                                        if (REGION == false)
                                        {
                                            dr["REGION"] = _loch.Mpi_val;

                                            LocDes = bsObj.CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "REGION", _loch.Mpi_val);
                                            foreach (DataRow row2 in LocDes.Rows)
                                            {
                                                dr["REGION_MGR"] = row2["descp"].ToString();

                                            }
                                        }
                                    }



                                    if (_loch.Mpi_cd == "ZONE")
                                    {
                                        if (ZONE == false)
                                        {
                                            dr["ZONE"] = _loch.Mpi_val;

                                            LocDes = bsObj.CHNLSVC.Sales.getReturnChequeLocHigh(BaseCls.GlbUserComCode, "ZONE", _loch.Mpi_val);
                                            foreach (DataRow row2 in LocDes.Rows)
                                            {
                                                dr["ZONE_MGR"] = row2["descp"].ToString();

                                            }
                                        }
                                    }
                                }
                            }



                            BankName = bsObj.CHNLSVC.Sales.getReturnChequeBank(row["SRCQ_BANK"].ToString());
                            foreach (DataRow row2 in BankName.Rows)
                            {
                                dr["BANK"] = row2["mbi_desc"].ToString();
                            }



                            dr["CHQ_NO"] = row["SRCQ_CHQ"].ToString();
                            //   dr["BANK"] = row["SRCQ_BANK"].ToString();
                            dr["BRANCH"] = row["SRCQ_CHQ_BRANCH"].ToString();
                            dr["RETURN_DATE"] = row["SRCQ_RTN_DT"].ToString();
                            dr["CHEQUE_VALUE"] = row["SRCQ_ACT_VAL"].ToString();
                            dr["OUTSTANDING"] = _outstanding;
                            if (_dueDays1 < 30)
                            {
                                dr["LESS30"] = _outstanding;
                            }
                            else
                            {
                                dr["GRATEER30"] = _outstanding;
                            }


                            dr["INTAMT"] = _intVal;
                            dr["DUEAMT"] = ((Convert.ToDecimal(row["SRCQ_ACT_VAL"].ToString())) + _intVal) - _payments;
                            if (_dueDays1 <= 30)
                            {
                                dr["0-30"] = _dueDays1;
                            }
                            if (_dueDays1 <= 60 && _dueDays1 > 30)
                            {
                                dr["30-60"] = _dueDays1;
                            }

                            if (_dueDays1 <= 90 && _dueDays1 > 60)
                            {
                                dr["60-90"] = _dueDays1;
                                // _intRate60 = _ser1.Hps_rt;
                            }
                            if (_dueDays1 <= 120 && _dueDays1 > 90)
                            {
                                dr["90-120"] = _dueDays1;

                            }

                            if (_dueDays1 <= 150 && _dueDays1 > 120)
                            {
                                dr["120-150"] = _dueDays1;

                            }
                            if (_dueDays1 <= 180 && _dueDays1 > 150)
                            {
                                dr["150-180"] = _dueDays1;

                            }
                            if (_dueDays1 > 180)
                            {
                                dr["over180"] = _dueDays1;

                            }
                            dr["PRFIT"] = row["SRCQ_PC"].ToString();

                            DataTable mst_profit_center = new DataTable();
                            mst_profit_center = bsObj.CHNLSVC.Sales.GetProfitCenterTable(BaseCls.GlbUserComCode, row["SRCQ_PC"].ToString());
                            foreach (DataRow row4 in mst_profit_center.Rows)
                            {
                                dr["PRFITDES"] = row4["MPC_DESC"].ToString();
                            }


                            RETURN_CHQ_SETTE.Rows.Add(dr);

                            _outstanding = 0;
                            _payments = 0;
                        }

                    }//// next

                    RETURN_CHQ_SETTE_NEW.Merge(RETURN_CHQ_SETTE);
                }



            }//end if

            DataTable param = new DataTable();
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("fromdate", typeof(DateTime));
            param.Columns.Add("todate", typeof(DateTime));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));
            param.Columns.Add("para2", typeof(double));
            param.Columns.Add("para3", typeof(double));



            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportAsAtDate;
            dr["todate"] = BaseCls.GlbReportAsAtDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportDocType;
            dr["para2"] = _intRate60;
            dr["para3"] = _intRate90;
            param.Rows.Add(dr);
            if (RETURN_CHQ_SETTE_NEW.Rows.Count > 0)
            {
                rowcnt = 1;
                RETURN_CHQ_SETTE_NEW = RETURN_CHQ_SETTE_NEW.DefaultView.ToTable(true);
                _rtnChe.Database.Tables["RETURN_CHQ_SETTE"].SetDataSource(RETURN_CHQ_SETTE_NEW);
                DataTable mst_com = new DataTable();
                mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbUserComCode);
                _rtnChe.Database.Tables["mst_com"].SetDataSource(mst_com);
                _rtnChe.Database.Tables["param"].SetDataSource(param);

            }
            else
            {
                rowcnt = 0;
            }
            return rowcnt;

        }


        public int ReturnChequeSettlements()
        {
            DataTable RETURN_CHQ_SETTE_SETTLEMENTS = new DataTable();
            DataTable param = new DataTable();
            DataRow dr;
            int rtnValue = 0;
            param.Clear();
            RETURN_CHQ_SETTE_SETTLEMENTS.Clear();

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
            DataTable mst_com = new DataTable();


            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.getReturnChequeSettlemtsDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }

            //RETURN_CHQ_SETTE_SETTLEMENTS = bsObj.CHNLSVC.Sales.getReturnChequeSettlemtsDetails(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserID,BaseCls.GlbReportCompCode );
            if (GLOB_DataTable.Rows.Count == 0)
            {
                rtnValue = 1;
            }
            if (GLOB_DataTable.Rows.Count > 0)
            {
                rtnValue = 0;
                _rtnCheDet.Database.Tables["RETURN_CHQ_SETTE_SETTLEMENTS"].SetDataSource(GLOB_DataTable);
                _rtnCheDet.Database.Tables["param"].SetDataSource(param);
                _rtnCheDet.Database.Tables["mst_com"].SetDataSource(mst_com);
            }
            return rtnValue;
        }


        public int SummaryOfWeekly()
        {

            DataTable param = new DataTable();
            DataRow dr;
            int rtnValue = 0;
            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("period", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("comp", typeof(string));
            param.Columns.Add("compaddr", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["period"] = "FROM " + BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy") + " TO " + BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy");

            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);
            DataTable mst_com = new DataTable();


            mst_com = bsObj.CHNLSVC.General.GetCompanyByCode(BaseCls.GlbReportCompCode);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.SummaryOfWeekly(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString());
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }
            if (GLOB_DataTable.Rows.Count == 0)
            {
                rtnValue = 1;
            }
            if (GLOB_DataTable.Rows.Count > 0)
            {
                rtnValue = 0;
                _rtnSumWeek.Database.Tables["GLB_SUM_WEEK"].SetDataSource(GLOB_DataTable);
                _rtnSumWeek.Database.Tables["param"].SetDataSource(param);
                _rtnSumWeek.Database.Tables["mst_com"].SetDataSource(mst_com);
            }
            return rtnValue;
        }

        public void QuotationDetails()
        {
            //Nadeeka 29-04-2015
            DataTable param = new DataTable();
            DataTable GLB_QUOTATION_DETAILS = new DataTable();
            DataRow dr;
            GLB_QUOTATION_DETAILS = bsObj.CHNLSVC.MsgPortal.GetQuotationDet(BaseCls.GlbReportCompCode, BaseCls.GlbReportDocSubType, "N", BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);


            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbReportCompCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_GLB_QUOTATION_DETAILS = new DataTable();
                    TMP_GLB_QUOTATION_DETAILS = bsObj.CHNLSVC.MsgPortal.GetQuotationDet(BaseCls.GlbReportCompCode, BaseCls.GlbReportDocSubType, drow["tpl_pc"].ToString(), BaseCls.GlbReportItemCode, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date);
                    GLB_QUOTATION_DETAILS.Merge(TMP_GLB_QUOTATION_DETAILS);


                }
            }



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
            param.Rows.Add(dr);
            _quoDet.Database.Tables["GLB_QUOTATION_DETAILS"].SetDataSource(GLB_QUOTATION_DETAILS);
            _quoDet.Database.Tables["param"].SetDataSource(param);

        }

        public void InvociePrintServiceSGL()
        {// Sanjeewa 2015-07-22
            string invNo = default(string);
            string accNo = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = BaseCls.GlbReportDoc;
            DataTable mst_tax_master = new DataTable();
            DataRow drr;
            DataTable salesDetails = new DataTable();
            //DataTable sat_vou_det = new DataTable();
            DataTable param = new DataTable();

            param.Clear();
            param.Columns.Add("isCnote", typeof(Int16));

            Int16 isCredit = 0;

            salesDetails.Clear();

            //salesDetails = bsObj.CHNLSVC.Sales.GetSalesDetails(invNo, null);
            salesDetails = bsObj.CHNLSVC.Inventory.GetSalesDetailsMobSer(invNo);
            //sat_vou_det = bsObj.CHNLSVC.Sales.getSalesGiftVouchaer(invNo);
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
            //DataTable tblComDate = new DataTable();

            int _numPages = 0;
            DataRow dr3;

            _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);
            _JobInvoiceSGL.Database.Tables["PRINT_DOC"].SetDataSource(PRINT_DOC);

            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();

            //DataTable accountDetails = bsObj.CHNLSVC.Sales.GetAccountDetails(invNo);
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
            sat_itm.Columns.Add("SAD_JOB_NO", typeof(string));
            sat_itm.Columns.Add("SAD_JOB_LINE", typeof(Int16));
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
                dr["SAD_JOB_NO"] = row["SAD_JOB_NO"].ToString();
                dr["SAD_JOB_LINE"] = Convert.ToInt16(row["SAD_JOB_LINE"].ToString());
                dr["VAT_AMT"] = Convert.ToDecimal(row["VAT_AMT"].ToString());
                dr["NBT_AMT"] = Convert.ToDecimal(row["NBT_AMT"].ToString());
                dr["OTH_TAX_AMT"] = Convert.ToDecimal(row["OTH_TAX_AMT"].ToString());
                sat_itm.Rows.Add(dr);

                dr = mst_item.NewRow();
                dr["MI_MODEL"] = row["MI_MODEL"].ToString();
                dr["MI_SHORTDESC"] = row["MI_SHORTDESC"].ToString();
                dr["MI_CD"] = row["MI_CD"].ToString();

                mst_item.Rows.Add(dr);

                if (index == 0)
                {
                    //if (accountDetails.Rows.Count > 0)
                    //{
                    //    tblComDate = bsObj.CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), accNo);
                    //}
                    //else
                    //{
                    //    tblComDate = bsObj.CHNLSVC.Sales.GetWarrantyCommenceDate(row["MPC_COM"].ToString(), row["MPC_CD"].ToString(), invNo);
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

            DataTable deliveredSerials = bsObj.CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
            //DataTable int_hdr = new DataTable();
            DataTable int_ser = new DataTable();

            //int_hdr.Columns.Add("ITH_SEQ_NO", typeof(decimal));
            //int_hdr.Columns.Add("ITH_COM", typeof(string));
            //int_hdr.Columns.Add("ITH_OTH_DOCNO", typeof(string));
            //int_hdr.Columns.Add("ITH_DOC_NO", typeof(string));
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
                //    dr = int_hdr.NewRow();
                //    dr["ITH_SEQ_NO"] = row["ITH_SEQ_NO"].ToString();
                //    dr["ITH_COM"] = row["ITH_COM"].ToString();
                //    dr["ITH_OTH_DOCNO"] = row["ITH_OTH_DOCNO"].ToString();
                //    dr["ITH_DOC_NO"] = row["ITH_DOC_NO"].ToString();
                //    int_hdr.Rows.Add(dr);

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

                //    DataTable MST_ITM1 = new DataTable();
                //    MST_ITM1 = bsObj.CHNLSVC.Sales.GetItemCode(row["ITH_COM"].ToString(), row["ITS_ITM_CD"].ToString());
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

            //DataTable hpt_shed = bsObj.CHNLSVC.Sales.GetAccountSchedule(invNo);
            //DataTable Promo = bsObj.CHNLSVC.Sales.GetPromotionByInvoice(invNo);

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


            _JobInvoiceSGL.Database.Tables["mst_profit_center"].SetDataSource(mst_profit_center);
            _JobInvoiceSGL.Database.Tables["mst_com"].SetDataSource(mst_com);
            _JobInvoiceSGL.Database.Tables["mst_busentity"].SetDataSource(mst_busentity);
            _JobInvoiceSGL.Database.Tables["sat_itm"].SetDataSource(sat_itm);
            _JobInvoiceSGL.Database.Tables["sat_hdr"].SetDataSource(sat_hdr1);
            _JobInvoiceSGL.Database.Tables["mst_item"].SetDataSource(mst_item);
            _JobInvoiceSGL.Database.Tables["sec_user"].SetDataSource(sec_user);
            _JobInvoiceSGL.Database.Tables["sar_sub_tp"].SetDataSource(sar_sub_tp);
            _JobInvoiceSGL.Database.Tables["param"].SetDataSource(param);

            DataTable ServiceJobDef = new DataTable();
            DataTable ServiceJobSer = new DataTable();
            DataTable ServiceJobSerSub = new DataTable();

            if (sat_itm.Rows.Count > 0)
            {
                foreach (DataRow drow in sat_itm.Rows)
                {
                    DataTable ServiceJobDef1 = new DataTable();
                    ServiceJobDef1 = bsObj.CHNLSVC.CustService.sp_get_job_defects(drow["SAD_JOB_NO"].ToString());
                    ServiceJobDef.Merge(ServiceJobDef1);

                    //kapila 17/6/2015
                    DataTable ServiceJobSerials = new DataTable();
                    ServiceJobSerials = bsObj.CHNLSVC.CustService.getServicejobDet(drow["SAD_JOB_NO"].ToString(), Convert.ToInt32(drow["SAD_JOB_LINE"]));
                    ServiceJobSer.Merge(ServiceJobSerials);

                    DataTable ServiceJobSerialsSub = new DataTable();
                    ServiceJobSerialsSub = bsObj.CHNLSVC.CustService.GetServiceJobDetailSubItemsData(drow["SAD_JOB_NO"].ToString(), Convert.ToInt32(drow["SAD_JOB_LINE"]));
                    ServiceJobSerSub.Merge(ServiceJobSerialsSub);

                }

                ServiceJobDef = ServiceJobDef.DefaultView.ToTable(true);
                ServiceJobSer = ServiceJobSer.DefaultView.ToTable(true);
                ServiceJobSerSub = ServiceJobSerSub.DefaultView.ToTable(true);

            }

            //_JobInvoice.Database.Tables["Promo"].SetDataSource(Promo);

            foreach (object repOp in _JobInvoiceSGL.ReportDefinition.ReportObjects)
            {
                string _s = repOp.GetType().ToString();
                if (_s.ToLower().Contains("subreport"))
                {
                    SubreportObject _cs = (SubreportObject)repOp;
                    //if (_cs.SubreportName == "rptWarranty")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    //}

                    if (_cs.SubreportName == "rptCheque")
                    {
                        ReportDocument subRepDoc = _JobInvoiceSGL.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["sat_receipt"].SetDataSource(sat_receiptCQ);
                        subRepDoc.Database.Tables["sat_receiptitm"].SetDataSource(sat_receiptitmCQ);
                    }
                    //if (_cs.SubreportName == "rptAccount")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];

                    //    subRepDoc.Database.Tables["hpt_acc"].SetDataSource(hpt_acc);
                    //    subRepDoc.Database.Tables["hpt_shed"].SetDataSource(hpt_shed);
                    //}

                    if (_cs.SubreportName == "tax")
                    {
                        ReportDocument subRepDoc = _JobInvoiceSGL.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["mst_tax_master"].SetDataSource(mst_tax_master);
                    }
                    if (_cs.SubreportName == "rptComm")
                    {
                        ReportDocument subRepDoc = _JobInvoiceSGL.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["ref_rep_infor"].SetDataSource(ref_rep_infor);
                    }
                    //if (_cs.SubreportName == "rptWarr")
                    //{
                    //    mst_item1 = mst_item1.DefaultView.ToTable(true);
                    //    ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];

                    //    subRepDoc.Database.Tables["int_ser"].SetDataSource(int_ser);
                    //    subRepDoc.Database.Tables["int_batch"].SetDataSource(int_batch);
                    //    subRepDoc.Database.Tables["MST_ITM"].SetDataSource(mst_item1);
                    //    subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                    //}
                    //if (_cs.SubreportName == "giftVou")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["sat_vou_det"].SetDataSource(sat_vou_det);
                    //}
                    if (_cs.SubreportName == "loc")
                    {
                        ReportDocument subRepDoc = _JobInvoiceSGL.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["MST_LOC"].SetDataSource(MST_LOC);
                    }
                    //if (_cs.SubreportName == "serials")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoiceSGL.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["INT_BATCH"].SetDataSource(int_batch1);
                    //    subRepDoc.Database.Tables["INT_SER"].SetDataSource(int_ser);
                    //}
                    //if (_cs.SubreportName == "Job_Defects")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoiceSGL.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["SCV_JOB_DEF"].SetDataSource(ServiceJobDef);
                    //}
                    if (_cs.SubreportName == "Job_Serials")
                    {
                        ReportDocument subRepDoc = _JobInvoiceSGL.Subreports[_cs.SubreportName];
                        subRepDoc.Database.Tables["SCV_JOB_DET"].SetDataSource(ServiceJobSer);
                    }
                    //if (_cs.SubreportName == "Job_Serials_Sub")
                    //{
                    //    ReportDocument subRepDoc = _JobInvoiceSGL.Subreports[_cs.SubreportName];
                    //    subRepDoc.Database.Tables["SCV_JOB_DET_SUB"].SetDataSource(ServiceJobSerSub);
                    //}

                    //if (tblComDate.Rows.Count >0) 
                    //{
                    //  if (_cs.SubreportName == "warrComDate")
                    //  {
                    //      ReportDocument subRepDoc = _JobInvoice.Subreports[_cs.SubreportName];
                    //      subRepDoc.Database.Tables["tblComDate"].SetDataSource(tblComDate);
                    //  }
                    //}
                }
            }




        }

        //Udaya 20/04/2017
        public void InvoicePrint_Direct()
        {
            PrintDocument pdoc = null;
            PrintDialog pd = new PrintDialog();
            pdoc = new PrintDocument();
            PrinterSettings ps = new PrinterSettings();
            Font font = new Font("Tahoma", 8);
            int _pageCount = 0;
            pd.Document = pdoc;

            DialogResult result = pd.ShowDialog();
            if (result == DialogResult.OK)
            {
                DialogResult dialogResult = MessageBox.Show("Insert Invoice document to the printer & Press Ok.", "Invoice Print", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dialogResult == DialogResult.OK)
                {
                    isAccess = false;
                    InvoiceDirectPrintDataSouces();
                    BaseCls.GlbReportnoofDays = 1;
                    pdoc.PrintPage += new PrintPageEventHandler(Inv_PrintPage);
                    Margins margins = new Margins(1, 1, 1, 4);
                    pdoc.DefaultPageSettings.Margins = margins;
                    //int pageCunt = (int)Math.Ceiling((OffsetYVal / 340.0));
                    pdoc.Print();
                }
            }
        }

        #region initialize data tables
        protected void
            InvoiceDirectPrintDataSouces()
        {
            string accNo = default(string);
            string cust_code = default(string);
            Boolean ishp = default(Boolean);
            ishp = false;
            invNo = BaseCls.GlbReportDoc;

            DataRow drr;
            param.Clear();
            param.Columns.Add("isCnote", typeof(Int16));
            Int16 isCredit = 0;
            salesDetails.Clear();
            salesDetails = bsObj.CHNLSVC.Sales.GetSalesDetails(invNo, null);
            sat_vou_det = bsObj.CHNLSVC.Sales.getSalesGiftVouchaer(invNo);
            mst_tax_master = bsObj.CHNLSVC.Sales.GetinvTax(invNo);
            int _numPages = 0;
            DataRow dr3;

            _numPages = bsObj.CHNLSVC.General.GetReprintDocCount(invNo);
            PRINT_DOC.Columns.Add("NOOFPAGES", typeof(Int16));
            PRINT_DOC.Columns.Add("SHOWCOM", typeof(Int16));

            dr3 = PRINT_DOC.NewRow();
            dr3["NOOFPAGES"] = _numPages;
            dr3["SHOWCOM"] = BaseCls.ShowComName;

            PRINT_DOC.Rows.Add(dr3);

            sat_hdr1.Clear();
            sat_itm.Clear();
            mst_profit_center.Clear();
            mst_item.Clear();
            mst_com.Clear();
            sec_user.Clear();
            mst_busentity.Clear();

            accountDetails = bsObj.CHNLSVC.Sales.GetAccountDetails(invNo);
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
            hpt_acc.Columns.Add("HPA_PND_VOU", typeof(Int16));
            hpt_acc.Columns.Add("HPA_VOU_RMK", typeof(string));

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
                if (row["HPA_PND_VOU"].ToString() == "") { dr["HPA_PND_VOU"] = 0; } else { dr["HPA_PND_VOU"] = Convert.ToInt16(row["HPA_PND_VOU"].ToString()); }
                dr["HPA_VOU_RMK"] = row["HPA_VOU_RMK"].ToString();
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
            sat_hdr1.Columns.Add("SAH_QT_CUST", typeof(string));
            sat_hdr1.Columns.Add("SAH_ANAL_4", typeof(string));

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
            sat_itm.Columns.Add("SAD_INV_NO", typeof(string));

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
                    dr["SAH_ANAL_4"] = row["SAH_ANAL_4"].ToString();
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
                    cust_code = row["SAH_STRUCTURE_SEQ"].ToString(); //Sanjeewa 2015-08-05 QUOTATION NO
                    mst_customer = new DataTable();
                    mst_customer = bsObj.CHNLSVC.Sales.Get_CustomerDetails(cust_code);
                    foreach (DataRow row2 in mst_customer.Rows)
                    {
                        dr["SAH_QT_CUST"] = row2["mbg_name"].ToString();
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
                dr["SAD_INV_NO"] = row["SAD_INV_NO"].ToString();
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
                dr["MI_IS_SER1"] = 0;
                dr["MI_IS_SER2"] = 0;
                dr["MI_WARR"] = 0;

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

            deliveredSerials = bsObj.CHNLSVC.Sales.GetDeliveredSerialDetails(invNo);
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
                    dr["ITS_ITM_CD"] = row["ITS_ITM_CD"].ToString();
                    dr["ITS_WARR_PERIOD"] = Convert.ToDecimal(row["ITS_WARR_PERIOD"].ToString());
                    int_ser.Rows.Add(dr);
                };

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

            receiptDetails = bsObj.CHNLSVC.Sales.GetInvoiceReceiptDet(invNo);
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
            sat_receiptitmCQ.Columns.Add("SARD_CC_PERIOD", typeof(int));

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
                    dr["SARD_CC_PERIOD"] = Convert.ToDecimal(row["SARD_CC_PERIOD"].ToString());
                    sat_receiptitmCQ.Rows.Add(dr);

                    if (row["SARD_PAY_TP"].ToString() == "CRNOTE")
                    {
                        isCredit = 1;
                    }
                };
            }
            sat_receiptitmCQ_copy = sat_receiptitmCQ.Copy();

            hpt_shed = bsObj.CHNLSVC.Sales.GetAccountSchedule(invNo);
            Promo = bsObj.CHNLSVC.Sales.GetPromotionByInvoice(invNo);

            //if (GlbReportName == "InvoiceHalfPrints.rpt")
            //    ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoiceHalfPrints.rpt");
            //else
            //    ref_rep_infor = CHNLSVC.Sales.GetReportInfor("InvoicePOSPrint.rpt");        //kapila 29/6/2015

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
        }
        #endregion initialize data tables

        //Udaya 20/04/2017
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

        public static string SpliceText(string text, int lineLength)
        {
            return Regex.Replace(text, "(.{" + lineLength + "})", "$1" + Environment.NewLine);
        }

        //Udaya 20/04/2017
        public void Inv_PrintPage(object sender, PrintPageEventArgs e)
        {
            int startX = 0;
            int startY = 0;
            int OffsetY = 5;
            int OffsetX = 5; //24_05_2017 110 to 5
            int OffsetYStatic = 0;
            Graphics graphics = e.Graphics;
            //SizeF stringSize = new SizeF();
            int stringSize = 0;
            decimal _val = 0;
            StringFormat format = new StringFormat(StringFormatFlags.DirectionRightToLeft);

            #region variables
            string invHdr = string.Empty;
            string heading = string.Empty;
            decimal totDisAmt = 0;
            string rptChequePayMode = string.Empty;
            string rptChequeRemark = string.Empty;
            decimal rptChequeTotSettleAmt = 0;
            decimal satItmQty = 0;
            decimal satItmUnitRate = 0;
            decimal satItmTotAmt = 0;
            decimal satItmDisAmt = 0;
            Int16 intbatchline = 0;
            Int16 intitemline = 0;
            string vouValue = string.Empty;
            string vou = string.Empty;
            string vouCond = string.Empty;
            string WarrRemark = string.Empty;
            string _rState = string.Empty;
            decimal satItmTaxAmt = 0;
            string satItemRmk = string.Empty;
            Int16 satitmwarrPrd = 0;
            decimal satitmDisRate = 0;
            string intSerSer1 = string.Empty;
            string intSerSer2 = string.Empty;
            string intSerWarNo = string.Empty;
            if (isAccess == false)
            {
                mst_item_copy = mst_item.Copy();
            }

            Int16 SAH_TAX_EXEMPTED = (from DataRow drw in sat_hdr1.Rows select Convert.ToInt16(drw["SAH_TAX_EXEMPTED"])).FirstOrDefault();
            Int16 SAH_IS_SVAT = (from DataRow drw in sat_hdr1.Rows select Convert.ToInt16(drw["SAH_IS_SVAT"])).FirstOrDefault();
            Int16 SAH_TAX_INV = (from DataRow drw in sat_hdr1.Rows select Convert.ToInt16(drw["SAH_TAX_INV"])).FirstOrDefault();
            string SAH_INV_TP = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_INV_TP"] == null || drw["SAH_INV_TP"].ToString() == "") ? string.Empty : (string)drw["SAH_INV_TP"]).FirstOrDefault();
            string SAH_CRE_BY = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_CRE_BY"] == null || drw["SAH_CRE_BY"].ToString() == "") ? string.Empty : (string)drw["SAH_CRE_BY"]).FirstOrDefault();
            string SAH_INV_SUB_TP = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_INV_SUB_TP"] == null || drw["SAH_INV_SUB_TP"].ToString() == "") ? string.Empty : (string)drw["SAH_INV_SUB_TP"]).FirstOrDefault();
            string SAH_CUS_NAME = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_CUS_NAME"] == null || drw["SAH_CUS_NAME"].ToString() == "") ? string.Empty : (string)drw["SAH_CUS_NAME"]).FirstOrDefault();
            string SAH_CUS_ADD1 = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_CUS_ADD1"] == null || drw["SAH_CUS_ADD1"].ToString() == "") ? string.Empty : (string)drw["SAH_CUS_ADD1"]).FirstOrDefault();
            string SAH_CUS_ADD2 = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_CUS_ADD2"] == null || drw["SAH_CUS_ADD2"].ToString() == "") ? string.Empty : (string)drw["SAH_CUS_ADD2"]).FirstOrDefault();
            string SAH_ANAL_4 = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_ANAL_4"] == null || drw["SAH_ANAL_4"].ToString() == "") ? string.Empty : (string)drw["SAH_ANAL_4"]).FirstOrDefault();
            string SAH_INV_NO = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_INV_NO"] == null || drw["SAH_INV_NO"].ToString() == "") ? string.Empty : (string)drw["SAH_INV_NO"]).FirstOrDefault();
            string SAH_DT = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_DT"] == null || drw["SAH_DT"].ToString() == "") ? string.Empty : Convert.ToDateTime(drw["SAH_DT"]).ToShortDateString()).FirstOrDefault();
            string SAH_ACC_NO = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_ACC_NO"] == null || drw["SAH_ACC_NO"].ToString() == "") ? string.Empty : (string)drw["SAH_ACC_NO"]).FirstOrDefault();
            string SAH_REF_DOC = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_REF_DOC"] == null || drw["SAH_REF_DOC"].ToString() == "") ? string.Empty : (string)drw["SAH_REF_DOC"]).FirstOrDefault();
            string SAH_MAN_REF = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_MAN_REF"] == null || drw["SAH_MAN_REF"].ToString() == "") ? string.Empty : (string)drw["SAH_MAN_REF"]).FirstOrDefault();
            string SAH_REMARKS = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_REMARKS"] == null || drw["SAH_REMARKS"].ToString() == "") ? string.Empty : (string)drw["SAH_REMARKS"]).FirstOrDefault();
            string esep_name_initials = (from DataRow drw in sat_hdr1.Rows select (drw["esep_name_initials"] == null || drw["esep_name_initials"].ToString() == "") ? string.Empty : (string)drw["esep_name_initials"]).FirstOrDefault();
            string SAH_SALES_EX_CD = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_SALES_EX_CD"] == null || drw["SAH_SALES_EX_CD"].ToString() == "") ? string.Empty : (string)drw["SAH_SALES_EX_CD"]).FirstOrDefault();
            string SAH_COM = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_COM"] == null || drw["SAH_COM"].ToString() == "") ? string.Empty : (string)drw["SAH_COM"]).FirstOrDefault();
            Int16 SAH_ANAL_11 = (from DataRow drw in sat_hdr1.Rows select Convert.ToInt16(drw["SAH_ANAL_11"])).FirstOrDefault();

            string SAH_QT_CUST = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_QT_CUST"] == null || drw["SAH_QT_CUST"].ToString() == "") ? string.Empty : (string)drw["SAH_QT_CUST"]).FirstOrDefault();
            string SAH_D_CUST_ADD1 = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_D_CUST_ADD1"] == null || drw["SAH_D_CUST_ADD1"].ToString() == "") ? string.Empty : (string)drw["SAH_D_CUST_ADD1"]).FirstOrDefault();
            string SAH_D_CUST_ADD2 = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_D_CUST_ADD2"] == null || drw["SAH_D_CUST_ADD2"].ToString() == "") ? string.Empty : (string)drw["SAH_D_CUST_ADD2"]).FirstOrDefault();
            string sah_del_loc = (from DataRow drw in sat_hdr1.Rows select (drw["sah_del_loc"] == null || drw["sah_del_loc"].ToString() == "") ? string.Empty : (string)drw["sah_del_loc"]).FirstOrDefault();
            string SAH_PC = (from DataRow drw in sat_hdr1.Rows select (drw["SAH_PC"] == null || drw["SAH_PC"].ToString() == "") ? string.Empty : (string)drw["SAH_PC"]).FirstOrDefault();

            string MC_DESC = (from DataRow drw in mst_com.Rows select drw["MC_DESC"] == null ? string.Empty : (string)drw["MC_DESC"]).FirstOrDefault();
            string MC_TAX3 = (from DataRow drw in mst_com.Rows select (drw["MC_TAX3"] == null || drw["MC_TAX3"].ToString() == "") ? string.Empty : (string)drw["MC_TAX3"]).FirstOrDefault();
            string MC_TAX2 = (from DataRow drw in mst_com.Rows select (drw["MC_TAX2"] == null || drw["MC_TAX2"].ToString() == "") ? string.Empty : (string)drw["MC_TAX2"]).FirstOrDefault();
            string MC_TAX1 = (from DataRow drw in mst_com.Rows select (drw["MC_TAX1"] == null || drw["MC_TAX1"].ToString() == "") ? string.Empty : (string)drw["MC_TAX1"]).FirstOrDefault();
            string MC_ANAL18 = (from DataRow drw in mst_com.Rows select (drw["MC_ANAL18"] == null || drw["MC_ANAL18"].ToString() == "") ? string.Empty : (string)drw["MC_ANAL18"]).FirstOrDefault();
            string MC_CD = (from DataRow drw in mst_com.Rows select (drw["MC_CD"] == null || drw["MC_CD"].ToString() == "") ? string.Empty : (string)drw["MC_CD"]).FirstOrDefault();

            string PC_Add = (from DataRow drw in mst_profit_center.Rows select (drw["MPC_ADD_1"] == null || drw["MPC_ADD_1"].ToString() == "") ? string.Empty : (string)drw["MPC_ADD_1"]).FirstOrDefault() + ", " + (from DataRow drw in mst_profit_center.Rows select (drw["MPC_ADD_2"] == null || drw["MPC_ADD_2"].ToString() == "") ? string.Empty : (string)drw["MPC_ADD_2"]).FirstOrDefault();
            string MPC_TEL = (from DataRow drw in mst_profit_center.Rows select (drw["MPC_TEL"] == null || drw["MPC_TEL"].ToString() == "") ? string.Empty : (string)drw["MPC_TEL"]).FirstOrDefault();
            string MPC_FAX = (from DataRow drw in mst_profit_center.Rows select (drw["MPC_FAX"] == null || drw["MPC_FAX"].ToString() == "") ? string.Empty : (string)drw["MPC_FAX"]).FirstOrDefault();
            string MPC_EMAIL = (from DataRow drw in mst_profit_center.Rows select (drw["MPC_EMAIL"] == null || drw["MPC_EMAIL"].ToString() == "") ? string.Empty : (string)drw["MPC_EMAIL"]).FirstOrDefault();
            string MPC_CD = (from DataRow drw in mst_profit_center.Rows select (drw["MPC_CD"] == null || drw["MPC_CD"].ToString() == "") ? string.Empty : (string)drw["MPC_CD"]).FirstOrDefault();
            string MPC_DESC = (from DataRow drw in mst_profit_center.Rows select (drw["MPC_DESC"] == null || drw["MPC_DESC"].ToString() == "") ? string.Empty : (string)drw["MPC_DESC"]).FirstOrDefault();
            string MPC_CHNL = (from DataRow drw in mst_profit_center.Rows select (drw["MPC_CHNL"] == null || drw["MPC_CHNL"].ToString() == "") ? string.Empty : (string)drw["MPC_CHNL"]).FirstOrDefault();
            string MPC_OTH_REF = (from DataRow drw in mst_profit_center.Rows select (drw["MPC_OTH_REF"] == null || drw["MPC_OTH_REF"].ToString() == "") ? string.Empty : (string)drw["MPC_OTH_REF"]).FirstOrDefault();

            string MBE_CD = (from DataRow drw in mst_busentity.Rows select (drw["MBE_CD"] == null || drw["MBE_CD"].ToString() == "") ? string.Empty : (string)drw["MBE_CD"]).FirstOrDefault();
            string MBE_MOB = (from DataRow drw in mst_busentity.Rows select (drw["MBE_MOB"] == null || drw["MBE_MOB"].ToString() == "") ? string.Empty : (string)drw["MBE_MOB"]).FirstOrDefault();
            string MBE_NIC = (from DataRow drw in mst_busentity.Rows select (drw["MBE_NIC"] == null || drw["MBE_NIC"].ToString() == "") ? string.Empty : (string)drw["MBE_NIC"]).FirstOrDefault();
            string mbe_svat_no = (from DataRow drw in mst_busentity.Rows select (drw["mbe_svat_no"] == null || drw["mbe_svat_no"].ToString() == "") ? string.Empty : (string)drw["mbe_svat_no"]).FirstOrDefault();
            string MBE_TAX_NO = (from DataRow drw in mst_busentity.Rows select (drw["MBE_TAX_NO"] == null || drw["MBE_TAX_NO"].ToString() == "") ? string.Empty : (string)drw["MBE_TAX_NO"]).FirstOrDefault();

            Int16 NOOFPAGES = (from DataRow drw in PRINT_DOC.Rows select Convert.ToInt16(drw["NOOFPAGES"])).FirstOrDefault();
            Int16 SHOWCOM = (from DataRow drw in PRINT_DOC.Rows select Convert.ToInt16(drw["SHOWCOM"])).FirstOrDefault();

            //string TAX_CD = (from DataRow drw in mst_tax_master.Rows select (drw["TAX_CD"] == null || drw["TAX_CD"].ToString() == "") ? string.Empty : (string)drw["TAX_CD"]).FirstOrDefault();
            string TAX_CD = (from DataRow drw in mst_tax_master.Rows where (decimal)drw["tax_is_tax_inv"] == SAH_TAX_INV select (drw["TAX_CD"] == null || drw["TAX_CD"].ToString() == "") ? string.Empty : (string)drw["TAX_CD"]).FirstOrDefault();

            decimal SAD_DISC_AMT = (from DataRow drw in sat_itm.Rows select Convert.ToDecimal(drw["SAD_DISC_AMT"])).FirstOrDefault();
            decimal SAD_DISC_RT = (from DataRow drw in sat_itm.Rows select Convert.ToDecimal(drw["SAD_DISC_RT"])).FirstOrDefault();
            string SAD_WARR_REMARKS = (from DataRow drw in sat_itm.Rows select (drw["SAD_WARR_REMARKS"] == null || drw["SAD_WARR_REMARKS"].ToString() == "") ? string.Empty : (string)drw["SAD_WARR_REMARKS"]).FirstOrDefault();
            Int16 sad_warr_period = (from DataRow drw in sat_itm.Rows select Convert.ToInt16(drw["sad_warr_period"])).FirstOrDefault();

            string ITS_SER_1 = (from DataRow drw in int_ser.Rows select (drw["ITS_SER_1"] == null || drw["ITS_SER_1"].ToString() == "") ? string.Empty : (string)drw["ITS_SER_1"]).FirstOrDefault();
            string ITS_SER_2 = (from DataRow drw in int_ser.Rows select (drw["ITS_SER_2"] == null || drw["ITS_SER_2"].ToString() == "") ? string.Empty : (string)drw["ITS_SER_2"]).FirstOrDefault();
            string ITS_WARR_NO = (from DataRow drw in int_ser.Rows select (drw["ITS_WARR_NO"] == null || drw["ITS_WARR_NO"].ToString() == "") ? string.Empty : (string)drw["ITS_WARR_NO"]).FirstOrDefault();

            string GCL_DATE = (from DataRow drw in tblComDate.Rows select (drw["GCL_DATE"] == null || drw["GCL_DATE"].ToString() == "") ? string.Empty : Convert.ToDateTime(drw["GCL_DATE"]).ToShortDateString()).FirstOrDefault();
            string GCL_INV = (from DataRow drw in tblComDate.Rows select (drw["GCL_INV"] == null || drw["GCL_INV"].ToString() == "") ? string.Empty : (string)drw["GCL_INV"]).FirstOrDefault();

            string spdd_bank = (from DataRow drw in Promo.Rows select (drw["spdd_bank"] == null || drw["spdd_bank"].ToString() == "") ? string.Empty : (string)drw["spdd_bank"]).FirstOrDefault();
            string TOTAMT = (from DataRow drw in Promo.Rows select (drw["TOTAMT"] == null || drw["TOTAMT"].ToString() == "") ? string.Empty : (string)drw["TOTAMT"]).FirstOrDefault();

            Int16 HTS_RNT_NO = (from DataRow drw in hpt_shed.Rows select Convert.ToInt16(drw["HTS_RNT_NO"])).FirstOrDefault();
            double HTS_RNT_VAL = (from DataRow drw in hpt_shed.Rows select Convert.ToDouble(drw["HTS_RNT_VAL"])).FirstOrDefault();

            Int16 SRTD_DIRECT = (from DataRow drw in sar_sub_tp.Rows select Convert.ToInt16(drw["SRTD_DIRECT"])).FirstOrDefault();

            Int16 isCnote = (from DataRow drw in param.Rows select Convert.ToInt16(drw["isCnote"])).FirstOrDefault();

            decimal SARD_SETTLE_AMT = (from DataRow drw in sat_receiptitmCQ.Rows select Convert.ToDecimal(drw["SARD_SETTLE_AMT"])).FirstOrDefault();

            string vouType = (from DataRow drw in sat_vou_det.Rows select (drw["vouType"] == null || drw["vouType"].ToString() == "") ? string.Empty : (string)drw["vouType"]).FirstOrDefault();
            decimal Amt = (from DataRow drw in sat_vou_det.Rows select Convert.ToDecimal(drw["Amt"])).FirstOrDefault();
            string stvo_gv_itm = (from DataRow drw in sat_vou_det.Rows select (drw["stvo_gv_itm"] == null || drw["stvo_gv_itm"].ToString() == "") ? string.Empty : (string)drw["stvo_gv_itm"]).FirstOrDefault();
            Int32 STVO_PAGENO = (from DataRow drw in sat_vou_det.Rows select Convert.ToInt32(drw["STVO_PAGENO"])).FirstOrDefault();
            //Int32 STVO_PAGENO = (from DataRow drw in sat_vou_det.Rows select (drw["STVO_PAGENO"] == null || drw["STVO_PAGENO"].ToString() == "") ? string.Empty : (Int32)drw["STVO_PAGENO"]).FirstOrDefault();
            string spt_cond = (from DataRow drw in sat_vou_det.Rows select (drw["spt_cond"] == null || drw["spt_cond"].ToString() == "") ? string.Empty : (string)drw["spt_cond"]).FirstOrDefault();
            string valid_from = (from DataRow drw in sat_vou_det.Rows select (drw["valid_from"] == null || drw["valid_from"].ToString() == "") ? string.Empty : Convert.ToDateTime(drw["valid_from"]).ToShortDateString()).FirstOrDefault();
            string valid_to = (from DataRow drw in sat_vou_det.Rows select (drw["valid_to"] == null || drw["valid_to"].ToString() == "") ? string.Empty : Convert.ToDateTime(drw["valid_to"]).ToShortDateString()).FirstOrDefault();

            string SE_USR_NAME = (from DataRow drw in sec_user.Rows select (drw["SE_USR_NAME"] == null || drw["SE_USR_NAME"].ToString() == "") ? string.Empty : (string)drw["SE_USR_NAME"]).FirstOrDefault();

            string ML_LOC_DESC = (from DataRow drw in MST_LOC.Rows select (drw["ML_LOC_DESC"] == null || drw["ML_LOC_DESC"].ToString() == "") ? string.Empty : (string)drw["ML_LOC_DESC"]).FirstOrDefault();
            #endregion variables

            #region if else string
            if (sat_hdr1.Rows.Count > 0)
            {
                //Invoice Header
                if (SAH_TAX_EXEMPTED == 1)
                {
                    invHdr = "TAX EXEMPTED";
                }
                else if (SAH_IS_SVAT == 1)
                {
                    invHdr = "SUSPENDED TAX INVOICE";
                }
                else if (SAH_TAX_INV == 1)
                {
                    if (SAH_INV_TP == "CRED" || SAH_INV_TP == "HS" || SAH_INV_TP == "CS")
                        invHdr = "TAX INVOICE";
                }

                //Heading
                if (SAH_INV_TP == "CRED")
                {
                    if (SAH_INV_SUB_TP == "REV")
                    {
                        heading = "CREDIT SALES REVERSAL";
                    }
                    else
                    {
                        heading = "CREDIT INVOICE";
                    }
                }
                if (SAH_INV_TP == "HS")
                {
                    if (SAH_INV_SUB_TP == "REV")
                    {
                        heading = "HIRE SALES REVERSAL";
                    }
                    else
                    {
                        heading = "HIRE SALES DELIVERY ORDER";
                    }
                }
                if (SAH_INV_TP == "INTR")
                {
                    if (SAH_TAX_INV == 0)
                    {
                        heading = "INVOICE";
                    }
                    else
                    {
                        heading = "TAX INVOICE";
                    }
                }
                if (SAH_INV_TP == "CS")
                {
                    if (SAH_TAX_INV == 0)
                    {
                        if (SAH_INV_SUB_TP == "CC")
                        {
                            heading = "CASH INVOICE-CONVERSION";
                        }
                        else if (SAH_INV_SUB_TP == "REV")
                        {
                            heading = "CASH SALES REVERSAL";
                        }
                        else
                            heading = "CASH INVOICE";
                    }
                    else
                    {
                        heading = "TAX INVOICE";
                    }
                }
                //if (SAH_TAX_INV == 1)
                //{
                //    heading = "TAX INVOICE";
                //}
                //else
                //{
                //    heading = "CASH INVOICE";
                //}
            }
            #endregion if else string

            #region Header
            //23/06/2017 comment
            OffsetX = OffsetX + 5;
            if (SAH_IS_SVAT != 0)
            {
                OffsetX = OffsetX + 0;  //24_05_2017 50 to OffsetX
                graphics.DrawString(invHdr.ToString(), new Font("Tahoma", 10),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }
            if (SHOWCOM != 0 || (SAH_COM != "LRP" && SAH_PC != "40"))
            {
                //OffsetX = 350 - (MC_DESC.ToString().Length / 2);
                OffsetX = Convert.ToInt16(Math.Round((Convert.ToDecimal((10 * 72) / 2) - Convert.ToDecimal((((MC_DESC.ToString().Length) * 72) / 14) / 4)), 0));
                //OffsetX = 260;
                graphics.DrawString(MC_DESC == null ? string.Empty : MC_DESC.ToString(), new Font("Tahoma", 14),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }
            OffsetX = 605;
            graphics.DrawString(DateTime.Now.ToString(), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY + 3);
            OffsetX = 728;
            graphics.DrawString(MC_TAX3 == null ? string.Empty : MC_TAX3.ToString(), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY + 3);
            OffsetY = OffsetY + 20;

            OffsetX = 5;
            graphics.DrawString(heading == null ? string.Empty : heading.ToString(), new Font("Tahoma", 10, FontStyle.Bold),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            //OffsetX = 350 - (PC_Add.ToString().Length / 2);            
            OffsetX = Convert.ToInt16(Math.Round((Convert.ToDecimal((10 * 72) / 2) - Convert.ToDecimal((((PC_Add.ToString().Length) * 72) / 8) / 4)), 0));
            //OffsetX = 270;
            graphics.DrawString(PC_Add == null ? string.Empty : PC_Add.ToString(), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            OffsetX = 605;
            if (SAH_CRE_BY != null)
            {
                graphics.DrawString("Created By: " + (SAH_CRE_BY == null ? string.Empty : SAH_CRE_BY.ToString()), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                OffsetY = OffsetY + 10;
            }
            if (SAH_IS_SVAT != 0)
            {
                OffsetX = 5;
                graphics.DrawString("Company SVAT Reg. No: " + (MC_TAX2 == null ? string.Empty : MC_TAX2.ToString()), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY + 5);
            }
            if (SAH_ANAL_4 != "" && SAH_INV_SUB_TP != "REV" && SAH_ANAL_4 != null)
            {
                OffsetX = 200;
                graphics.DrawString("PO # " + (SAH_ANAL_4 == null ? string.Empty : SAH_ANAL_4.ToString()), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }
            //OffsetX = 350 - (("Tel - " + (MPC_TEL == null ? string.Empty : MPC_TEL.ToString()) + " Fax - " + (MPC_FAX == null ? string.Empty : MPC_FAX.ToString())).Length / 2);            
            OffsetX = Convert.ToInt16(Math.Round((Convert.ToDecimal((10 * 72) / 2) - Convert.ToDecimal((((("Tel - " + (MPC_TEL == null ? string.Empty : MPC_TEL.ToString()) + " Fax - " + (MPC_FAX == null ? string.Empty : MPC_FAX.ToString())).Length) * 72) / 8) / 4)), 0));
            //OffsetX = 270;
            graphics.DrawString("Tel - " + (MPC_TEL == null ? string.Empty : MPC_TEL.ToString()) + " Fax - " + (MPC_FAX == null ? string.Empty : MPC_FAX.ToString()), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            OffsetY = OffsetY + 10;
            OffsetX = 5;
            graphics.DrawString("Customer - " + (MBE_CD == null ? string.Empty : MBE_CD.ToString()), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            //OffsetX = 350 - (("Email - " + (MPC_EMAIL == null ? string.Empty : MPC_EMAIL.ToString())).Length / 2);            
            OffsetX = Convert.ToInt16(Math.Round((Convert.ToDecimal((10 * 72) / 2) - Convert.ToDecimal((((("Email - " + (MPC_EMAIL == null ? string.Empty : MPC_EMAIL.ToString())).Length) * 72) / 8) / 4)), 0));
            //OffsetX = 270;
            graphics.DrawString("Email - " + (MPC_EMAIL == null ? string.Empty : MPC_EMAIL.ToString()), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            OffsetX = 5;
            OffsetY = OffsetY + 15;
            graphics.DrawString(SAH_CUS_NAME == null ? string.Empty : SAH_CUS_NAME.ToString(), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            OffsetX = 555 + 50;
            graphics.DrawString(SAH_INV_NO == null ? string.Empty : SAH_INV_NO.ToString(), new Font("Tahoma", 8),//ref no
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            OffsetY = OffsetY + 17;
            OffsetX = 5;
            graphics.DrawString(SAH_CUS_ADD1 == null ? string.Empty : SAH_CUS_ADD1.ToString() + " " + SAH_CUS_ADD2.ToString(), new Font("Tahoma", 8),
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            //OffsetX = 105;
            //graphics.DrawString(MC_ANAL18 == null ? string.Empty : MC_ANAL18.ToString(), new Font("Tahoma", 8), //QA request
            //        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            OffsetX = 555 + 50;
            graphics.DrawString(SAH_DT == null ? string.Empty : SAH_DT.ToString(), new Font("Tahoma", 8),//date
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            OffsetY = OffsetY + 17;
            if (MBE_MOB != "")// && MBE_NIC != ""
            {
                OffsetX = 5;
                graphics.DrawString("Mob - " + (MBE_MOB == null ? string.Empty : MBE_MOB.ToString()) + ", NIC - " + (MBE_NIC == null ? string.Empty : MBE_NIC.ToString()), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }
            OffsetX = 555 + 50;
            graphics.DrawString(MPC_CD.ToString() + "-" + MPC_DESC.ToString(), new Font("Tahoma", 8),//location    
                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            if (MC_TAX1 != null)
            {
                OffsetY = OffsetY + 15;//10
                OffsetX = 270;
                graphics.DrawString("Company VAT Reg. No: " + (MC_TAX1 == null ? string.Empty : MC_TAX1.ToString()), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }
            if (NOOFPAGES != 0)
            {
                //OffsetY = OffsetY + 10;
                OffsetX = 270;
                graphics.DrawString("Reprint Copy: " + NOOFPAGES.ToString(), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY + 10);
            }

            OffsetY = OffsetY + 10;//20      //23/05/2017 30 to 20
            if (SAH_ACC_NO != "")
            {
                OffsetX = 555 + 50;
                graphics.DrawString("A/C No: " + (SAH_ACC_NO == null ? string.Empty : SAH_ACC_NO.ToString()), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }
            //new
            if (!(SAH_TAX_INV == 0 || SRTD_DIRECT == 1) && MBE_TAX_NO != null) //{sat_hdr.SAH_TAX_INV}=0 OR {sar_sub_tp.SRTD_DIRECT}=1
            {
                OffsetY = OffsetY + 10;
                OffsetX = 5;
                graphics.DrawString("VAT No: " + MBE_TAX_NO, new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }
            OffsetY = OffsetY + 20;      //23/05/2017 40 to 20
            if ((SAH_TAX_INV != 0 || SRTD_DIRECT != 1) && SAH_REF_DOC != "")
            {
                OffsetX = 5;
                graphics.DrawString("Ref. Doc: " + (SAH_REF_DOC == null ? string.Empty : SAH_REF_DOC.ToString()), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }
            if (SAH_IS_SVAT != 0)
            {
                OffsetX = 155;
                graphics.DrawString("SVAT NO: " + (mbe_svat_no == null ? string.Empty : mbe_svat_no.ToString()), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }
            if (SAH_MAN_REF != "" && SAH_MAN_REF != null)
            {
                OffsetX = 355;//200
                graphics.DrawString("Manual Ref.: " + (SAH_MAN_REF == null ? string.Empty : SAH_MAN_REF.ToString()), new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }
            #endregion Header

            if (mst_item.Rows.Count > 0)
            {
                OffsetY = OffsetY + 20;
                OffsetX = 5;
                graphics.DrawString("ITEM CODE", new Font("Tahoma", 8, FontStyle.Bold),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                OffsetX = OffsetX + 120;
                graphics.DrawString("DESCRIPTION", new Font("Tahoma", 8, FontStyle.Bold),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                OffsetX = OffsetX + 260;//280
                graphics.DrawString("MODEL", new Font("Tahoma", 8, FontStyle.Bold),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                if (MPC_CHNL == "ELITE")
                {
                    OffsetX = OffsetX + 80;
                    graphics.DrawString("VAT", new Font("Tahoma", 8, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                }
                OffsetX = OffsetX + 42;//62 82 80
                graphics.DrawString("QTY", new Font("Tahoma", 8, FontStyle.Bold),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                OffsetX = OffsetX + 40;
                _unitAmt = OffsetX;
                graphics.DrawString("UNIT PRICE", new Font("Tahoma", 8, FontStyle.Bold),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                OffsetX = OffsetX + 90;//100 80
                _disAmt = OffsetX;
                graphics.DrawString("DISCOUNT", new Font("Tahoma", 8, FontStyle.Bold),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                OffsetX = OffsetX + 115;//75
                _totAmt = OffsetX;
                graphics.DrawString("TOTAL", new Font("Tahoma", 8, FontStyle.Bold),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //int _newY = 0;
                //int _linecount1 = 0;
                //int _linecount2 = 0;
                OffsetY = OffsetY + 20; //25

                #region new code
                List<DataRow> mst_item_list = mst_item.AsEnumerable().ToList(); //mst_item
                //updated by akila 2017/12/13
                foreach (DataRow ditemRows in mst_item_list) //mst_item.Rows
                {
                    intSerSer1 = string.Empty;
                    intSerSer2 = string.Empty;

                    var _invItemList = sat_itm.AsEnumerable()
                        .Where(x => x.Field<string>("SAD_INV_NO") == invNo && x.Field<string>("SAD_ITM_CD") == ditemRows["MI_CD"].ToString())
                        .GroupBy(grp => new
                        {
                            ITEM_LINE = grp.Field<Int16>("SAD_ITM_LINE"),
                            totDis = grp.Field<decimal>("SAD_DISC_AMT"),
                            Qty = grp.Field<decimal>("SAD_QTY"),
                            SAD_UNIT_RT = grp.Field<decimal>("SAD_UNIT_RT"),
                            SAD_TOT_AMT = grp.Field<decimal>("SAD_TOT_AMT"),
                            SAD_DISC_AMT = grp.Field<decimal>("SAD_DISC_AMT"),
                            SAD_ITM_TAX_AMT = grp.Field<decimal>("SAD_ITM_TAX_AMT"),
                            SAD_WARR_REMARKS = grp.Field<string>("SAD_WARR_REMARKS"),
                            SAD_WARR_PERIOD = grp.Field<Int16>("SAD_WARR_PERIOD"),
                            SAD_DISC_RT = grp.Field<decimal>("SAD_DISC_RT")
                        }).Select(y => y.Key).ToList();

                    if (_invItemList != null && _invItemList.Count > 0)
                    {
                        foreach (var _item in _invItemList)
                        {
                            totDisAmt = _item.totDis;
                            satItmQty = Convert.ToDecimal(_item.Qty);
                            satItmUnitRate = _item.SAD_UNIT_RT;
                            satItmTotAmt = _item.SAD_TOT_AMT;
                            satItmDisAmt = _item.SAD_DISC_AMT;
                            satItmTaxAmt = _item.SAD_ITM_TAX_AMT;
                            satitmDisRate = _item.SAD_DISC_RT;

                            var _serials = int_ser.AsEnumerable()
                                .Where(x => x.Field<string>("its_itm_cd") == ditemRows["MI_CD"].ToString() && x.Field<Int16>("its_itm_line") == _item.ITEM_LINE)
                                .Select(y => new
                                {
                                    ITS_SER_1 = y.Field<string>("ITS_SER_1"),
                                    ITS_SER_2 = y.Field<string>("ITS_SER_2"),
                                    ITS_WARR_NO = y.Field<string>("ITS_WARR_NO")
                                }).ToList();

                            foreach (var s in _serials)
                            {
                                intSerWarNo = s.ITS_WARR_NO;
                            }

                            OffsetY = OffsetY + 20;


                            OffsetX = 5;

                            graphics.DrawString(ditemRows["MI_CD"].ToString(), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                            OffsetX = OffsetX + 120;
                            graphics.DrawString(SpliceText(ditemRows["MI_SHORTDESC"].ToString(), 40), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);



                            OffsetX = OffsetX + 260;//280
                            //OffsetY = _newY;
                            graphics.DrawString(SpliceText(ditemRows["MI_MODEL"].ToString(), 6), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                            if (MPC_CHNL == "ELITE")
                            {
                                OffsetX = OffsetX + 80;
                                graphics.DrawString(TAX_CD.ToString(), new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                            }
                            else
                            {
                                OffsetX = OffsetX + 10;
                            }
                            OffsetX = OffsetX + 40;//60 80 85
                            graphics.DrawString(satItmQty.ToString("N2"), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);


                            _val = (SAH_TAX_INV.ToString() == "1" || SAH_IS_SVAT.ToString() == "1") ? Convert.ToDecimal(satItmUnitRate.ToString("#,#00.00")) : Convert.ToDecimal(((satItmTotAmt + satItmDisAmt) / satItmQty).ToString("#,#00.00"));

                            OffsetX = Convert.ToInt16((OffsetX + 10 * 72 / 8 / 2) + (MPC_CHNL == "ELITE" ? 80 : 80) - (_val.ToString("N2").Length * 72 / 8 / 2) - 25);

                            graphics.DrawString((SAH_TAX_INV.ToString() == "1" || SAH_IS_SVAT.ToString() == "1") ? satItmUnitRate.ToString("N2") : ((satItmTotAmt + satItmDisAmt) / satItmQty).ToString("N2"), new Font("Tahoma", 8),
                                     new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);


                            _val = Convert.ToDecimal(totDisAmt.ToString("N2")).ToString("N2").Replace(".", "").Replace(",", "").Length;
                            _val = Convert.ToDecimal(satitmDisRate.ToString("N2")).ToString("N2").Replace(".", "").Replace(",", "").Length + _val;
                            OffsetX = Convert.ToInt16((577 + (MPC_CHNL == "ELITE" ? 80 : 0) + 5) - _val) - 8;

                            graphics.DrawString(totDisAmt.ToString("N2") + "" + "(" + satitmDisRate.ToString("N2") + ")%", new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                            _val = ((SAH_TAX_INV.ToString() == "1" || SAH_IS_SVAT.ToString() == "1") ? Convert.ToDecimal((satItmTotAmt - satItmTaxAmt).ToString("#,#00.00")) : Convert.ToDecimal(satItmTotAmt.ToString("#,#00.00")));

                            OffsetX = Convert.ToInt16((OffsetX + 5 * 72 / 8 / 2) + (MPC_CHNL == "ELITE" ? 80 : 80) - (_val.ToString("N2").Length * 72 / 8 / 2) + 35);

                            graphics.DrawString((SAH_TAX_INV.ToString() == "1" || SAH_IS_SVAT.ToString() == "1") ? (satItmTotAmt - satItmTaxAmt).ToString("#,#00.00") : satItmTotAmt.ToString("#,#00.00"), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                            totAmt += (SAH_TAX_INV.ToString() == "1" || SAH_IS_SVAT.ToString() == "1") ? (satItmTotAmt - satItmTaxAmt) : satItmTotAmt;

                            itemAmtDis.Add(totDisAmt);
                            itemAmtTax.Add(satItmTaxAmt);

                            OffsetY = (ditemRows["MI_SHORTDESC"].ToString().Length > 45 || (ditemRows["MI_MODEL"].ToString().Length > 12 && ditemRows["MI_SHORTDESC"].ToString().Length < 45)) ? OffsetY + 10 : OffsetY;

                            if (_item.SAD_WARR_REMARKS != null && _item.SAD_WARR_REMARKS != "N/A" && isCnote == 1)
                            {
                                OffsetY = OffsetY + 15;//20
                                OffsetX = 5;
                                graphics.DrawString("Warranty:" + _item.SAD_WARR_REMARKS, new Font("Tahoma", 8),
                                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                OffsetX = 125;
                                graphics.DrawString(_item.SAD_WARR_REMARKS, new Font("Tahoma", 8),
                                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                            }

                            if (_item.SAD_WARR_REMARKS != null && _item.SAD_WARR_REMARKS != "N/A" && isCnote == 1)
                            {
                                OffsetY = OffsetY + 15;//20
                                OffsetX = 5;
                                graphics.DrawString("Warranty:" + _item.SAD_WARR_REMARKS, new Font("Tahoma", 8),
                                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                OffsetX = 125;
                                graphics.DrawString(_item.SAD_WARR_REMARKS, new Font("Tahoma", 8),
                                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                            }

                            foreach (var s in _serials)
                            {
                                OffsetY = OffsetY + 15;//20
                                OffsetX = 5;
                                graphics.DrawString(MC_CD.ToString() == "ABL" ? "Serial" : "Engine #/Chassis #", new Font("Tahoma", 8),
                                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                OffsetX = 125;//105
                                graphics.DrawString(MC_CD.ToString() == "ABL" ? s.ITS_SER_1.ToString() : (s.ITS_SER_1.ToString() + "/ " + s.ITS_SER_2.ToString()), new Font("Tahoma", 8),
                                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                            }

                            if ((isCnote != 1 && MPC_OTH_REF != "CLS"))
                            {
                                OffsetY = OffsetY + 15;//20
                                OffsetX = 5;
                                graphics.DrawString("Warranty:", new Font("Tahoma", 8),
                                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                OffsetX = 125;
                                //graphics.DrawString(SpliceText((_item.SAD_WARR_PERIOD.ToString() + " Month(s) ") + (_item.SAD_WARR_REMARKS.ToString() == "N/A" ? "" : _item.SAD_WARR_REMARKS.ToString()),40), new Font("Tahoma", 8),
                                //            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                                WarrRemark = (_item.SAD_WARR_PERIOD.ToString() + " Month(s) ") + (_item.SAD_WARR_REMARKS.ToString() == "N/A" ? "" : _item.SAD_WARR_REMARKS.ToString());

                                string _tmpModel = ditemRows["MI_MODEL"].ToString();
                                int _lineIgnoreCount = 0;
                                if (!string.IsNullOrEmpty(_tmpModel) && _tmpModel.Length > 0)
                                {
                                    _lineIgnoreCount = _tmpModel.Length / 6;
                                }

                                int i = 0;
                                int oldCharLength = 0;
                                if (_lineIgnoreCount > 0)
                                {
                                    for (i = 1; i <= _lineIgnoreCount; i++)
                                    {
                                        if (WarrRemark.Length - oldCharLength > 0)
                                        {
                                            string tmpString = WarrRemark.Substring(oldCharLength, (WarrRemark.Length - oldCharLength) < 45 ? (WarrRemark.Length - oldCharLength) : 45);
                                            oldCharLength += 45;
                                            graphics.DrawString(tmpString, new Font("Tahoma", 8),
                                                   new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                            OffsetY = OffsetY + 15;
                                        }
                                    }
                                }

                                i = 0;
                                for (i = 1; i <= ((WarrRemark.Length) / 100) + 1; i++)
                                {
                                    if (WarrRemark.Length - oldCharLength > 0)
                                    {
                                        string tmpString = WarrRemark.ToString().Substring(oldCharLength, (WarrRemark.Length - oldCharLength) < 100 ? (WarrRemark.Length - oldCharLength) : 100);
                                        oldCharLength += 100;
                                        graphics.DrawString(tmpString, new Font("Tahoma", 8),
                                               new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                        OffsetY = OffsetY + 15;
                                    }
                                }
                            }

                            if ((isCnote != 0 && MPC_OTH_REF != "CLS") && intSerWarNo != null)// && GCL_INV != null
                            {
                                OffsetY = OffsetY + 15;//20
                                OffsetX = 5;
                                graphics.DrawString("Warranty:", new Font("Tahoma", 8),
                                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                OffsetX = 125;
                                //graphics.DrawString(SpliceText( _item.SAD_WARR_PERIOD.ToString() + " Month(s) " + _item.SAD_WARR_REMARKS.ToString() + Environment.NewLine + "Warranty No: " + intSerWarNo.ToString() + "Warranty Commence From: " + (GCL_DATE == null ? string.Empty : GCL_DATE + "Invoice No: " + GCL_INV == null ? string.Empty : GCL_INV),40), new Font("Tahoma", 8),
                                //            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                                WarrRemark = _item.SAD_WARR_PERIOD.ToString() + " Month(s) " + _item.SAD_WARR_REMARKS.ToString() + Environment.NewLine + "Warranty No: " + intSerWarNo.ToString() + "Warranty Commence From: " + (GCL_DATE == null ? string.Empty : GCL_DATE + "Invoice No: " + GCL_INV == null ? string.Empty : GCL_INV);

                                int i = 0;

                                string _tmpModel = ditemRows["MI_MODEL"].ToString();
                                int _lineIgnoreCount = 0;
                                if (!string.IsNullOrEmpty(_tmpModel) && _tmpModel.Length > 0)
                                {
                                    _lineIgnoreCount = _tmpModel.Length / 6;
                                }

                                int oldCharLength = 0;
                                if (_lineIgnoreCount > 0)
                                {
                                    for (i = 1; i <= _lineIgnoreCount; i++)
                                    {
                                        if (WarrRemark.Length - oldCharLength > 0)
                                        {
                                            string tmpString = WarrRemark.Substring(oldCharLength, (WarrRemark.Length - oldCharLength) < 45 ? (WarrRemark.Length - oldCharLength) : 45);
                                            oldCharLength += 45;
                                            graphics.DrawString(tmpString, new Font("Tahoma", 8),
                                                   new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                            OffsetY = OffsetY + 15;
                                        }
                                    }
                                }

                                i = 0;
                                for (i = 1; i <= ((WarrRemark.Length) / 100) + 1; i++)
                                {
                                    if (WarrRemark.Length - oldCharLength > 0)
                                    {
                                        string tmpString = WarrRemark.ToString().Substring(oldCharLength, (WarrRemark.Length - oldCharLength) < 100 ? (WarrRemark.Length - oldCharLength) : 100);
                                        oldCharLength += 100;
                                        graphics.DrawString(tmpString, new Font("Tahoma", 8),
                                               new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                        OffsetY = OffsetY + 15;
                                    }
                                }
                            }

                            if ((GCL_INV != "" && GCL_INV != null) && (GCL_DATE != "" && GCL_DATE != null))
                            {
                                OffsetY = OffsetY + 15;//20
                                OffsetX = 5;
                                graphics.DrawString("Warranty Commence From:", new Font("Tahoma", 8),
                                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                                OffsetX = 125;
                                graphics.DrawString((GCL_DATE == null ? string.Empty : GCL_DATE + " " + GCL_INV == null ? string.Empty : GCL_INV), new Font("Tahoma", 8),
                                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                            }

                            satitmTaxVal += _item.SAD_ITM_TAX_AMT;
                        }

                        ditemRows.Delete();
                        _rState = ditemRows.RowState.ToString();
                        if (_rState == "Deleted")
                        {
                            ditemRows.AcceptChanges();
                        }
                        if (OffsetY <= 350)//340
                        {
                            //e.HasMorePages = false;
                            //_totYCount = OffsetY + _totYCount;
                        }
                        else
                        {
                            e.HasMorePages = true;
                            startX = 0;
                            startY = 0;
                            OffsetY = 5;
                            return;
                        }
                    }


                    //if (sat_itm.Rows.Count > 0)
                    //{
                    //    bool dataExist = sat_itm.Select().ToList().Exists(r => r["SAD_ITM_CD"].ToString() == ditemRows["MI_CD"].ToString());
                    //    if (dataExist)
                    //    {
                    //        sat_itm.AsEnumerable().Where(r => r.Field<string>("SAD_ITM_CD") == ditemRows["MI_CD"].ToString()).FirstOrDefault().Delete();
                    //    }
                    //}
                    //if (int_ser.Rows.Count > 0)
                    //{
                    //    bool dataExist = int_ser.Select().ToList().Exists(r => r["its_itm_cd"].ToString() == ditemRows["MI_CD"].ToString());
                    //    if (dataExist)
                    //    {
                    //        int_ser.AsEnumerable().Where(r => r.Field<string>("its_itm_cd") == ditemRows["MI_CD"].ToString()).FirstOrDefault().Delete();
                    //    }
                    //}


                    //ditemRows.Delete();
                    //_rState = ditemRows.RowState.ToString();
                    //if (_rState == "Deleted")
                    //{
                    //    ditemRows.AcceptChanges();
                    //}

                    //if (OffsetY <= 350)//340
                    //{
                    //    //e.HasMorePages = false;
                    //    //_totYCount = OffsetY + _totYCount;
                    //}
                    //else
                    //{
                    //    e.HasMorePages = true;
                    //    startX = 0;
                    //    startY = 0;
                    //    OffsetY = 5;
                    //    return;
                    //}
                }
                #endregion

                #region old code
                //List<DataRow> mst_item_list = mst_item.AsEnumerable().ToList(); //mst_item
                //foreach (DataRow ditemRows in mst_item_list) //mst_item.Rows
                //{
                //    intSerSer1 = string.Empty;
                //    intSerSer2 = string.Empty;

                //    var ty = from row in sat_itm.AsEnumerable()
                //             where row.Field<string>("SAD_INV_NO") == invNo && row.Field<string>("SAD_ITM_CD") == ditemRows["MI_CD"].ToString()
                //             select new
                //             {
                //                 totDis = row.Field<decimal>("SAD_DISC_AMT"),
                //                 Qty = row.Field<decimal>("SAD_QTY"),
                //                 SAD_UNIT_RT = row.Field<decimal>("SAD_UNIT_RT"),
                //                 SAD_TOT_AMT = row.Field<decimal>("SAD_TOT_AMT"),
                //                 SAD_DISC_AMT = row.Field<decimal>("SAD_DISC_AMT"),
                //                 SAD_ITM_TAX_AMT = row.Field<decimal>("SAD_ITM_TAX_AMT"),//SAD_ITM_TAX_AMT
                //                 SAD_WARR_REMARKS = row.Field<string>("SAD_WARR_REMARKS"),
                //                 SAD_WARR_PERIOD = row.Field<Int16>("SAD_WARR_PERIOD"),
                //                 SAD_DISC_RT = row.Field<decimal>("SAD_DISC_RT")
                //             };

                //    foreach (var m in ty)
                //    {
                //        totDisAmt = m.totDis;
                //        satItmQty = Convert.ToDecimal(m.Qty);
                //        satItmUnitRate = m.SAD_UNIT_RT;
                //        satItmTotAmt = m.SAD_TOT_AMT;
                //        satItmDisAmt = m.SAD_DISC_AMT;
                //        satItmTaxAmt = m.SAD_ITM_TAX_AMT;
                //        //satItemRmk = m.SAD_WARR_REMARKS;
                //        //satitmwarrPrd = m.SAD_WARR_PERIOD;
                //        satitmDisRate = m.SAD_DISC_RT;
                //    }

                //    var iSer = from r in int_ser.AsEnumerable()
                //               where r.Field<string>("its_itm_cd") == ditemRows["MI_CD"].ToString() //r.Field<string>("its_doc_no") == invNo &&
                //               select new
                //               {
                //                   ITS_SER_1 = r.Field<string>("ITS_SER_1"),
                //                   ITS_SER_2 = r.Field<string>("ITS_SER_2"),
                //                   ITS_WARR_NO = r.Field<string>("ITS_WARR_NO")
                //               };

                //    foreach (var s in iSer)
                //    {
                //        //intSerSer1 = s.ITS_SER_1;
                //        //intSerSer2 = s.ITS_SER_2;
                //        intSerWarNo = s.ITS_WARR_NO;
                //    }

                //    //if (OffsetY <= 350)//340
                //    //{
                //    //    //e.HasMorePages = false;
                //    //    //_totYCount = OffsetY;
                //    //}
                //    //else
                //    //{
                //    //    e.HasMorePages = true;
                //    //    startX = 0;
                //    //    startY = 0;
                //    //    OffsetY = 5;
                //    //    return;
                //    //}
                //    OffsetY = OffsetY + 20;
                //    //_newY = OffsetY + (_linecount2*15);

                //    OffsetX = 5;
                //    //OffsetY = _newY;                    
                //    //_linecount1 = 0;
                //    //_linecount2 = 0;
                //    graphics.DrawString(ditemRows["MI_CD"].ToString(), new Font("Tahoma", 8),
                //            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    OffsetX = OffsetX + 120;
                //    graphics.DrawString(SpliceText(ditemRows["MI_SHORTDESC"].ToString(), 40), new Font("Tahoma", 8),
                //            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //    //for (int i = 1; i <= (ditemRows["MI_SHORTDESC"].ToString().Length / 40) + 1; i++)
                //    //{
                //    //    graphics.DrawString(ditemRows["MI_SHORTDESC"].ToString().Substring(((i - 1) * 40), ditemRows["MI_SHORTDESC"].ToString().Length - (i - 1) * 40 < 40 ? ditemRows["MI_SHORTDESC"].ToString().Length - (i - 1) * 40 : 40), new Font("Tahoma", 8),
                //    //                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    //    OffsetY = OffsetY + 15;
                //    //    _linecount1 = _linecount1 + 1;
                //    //}
                //    //if (_linecount2 < _linecount1)
                //    //{
                //    //    _linecount2 = _linecount1;
                //    //    _linecount1 = 0;
                //    //}

                //    OffsetX = OffsetX + 260;//280
                //    //OffsetY = _newY;
                //    graphics.DrawString(SpliceText(ditemRows["MI_MODEL"].ToString(), 6), new Font("Tahoma", 8),
                //            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    //for (int i = 1; i <= (ditemRows["MI_MODEL"].ToString().Length / 6) + 1; i++)
                //    //{
                //    //    graphics.DrawString(ditemRows["MI_MODEL"].ToString().Substring(((i - 1) * 6), ditemRows["MI_MODEL"].ToString().Length - (i - 1) * 6 < 6 ? ditemRows["MI_MODEL"].ToString().Length - (i - 1) * 6 : 6), new Font("Tahoma", 8),
                //    //                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    //    OffsetY = OffsetY + 15;
                //    //    _linecount1 = _linecount1 + 1;
                //    //}
                //    //if (_linecount2 < _linecount1)
                //    //{
                //    //    _linecount2 = _linecount1;
                //    //    _linecount1 = 0;
                //    //}

                //    //OffsetY = _newY;
                //    if (MPC_CHNL == "ELITE")
                //    {
                //        OffsetX = OffsetX + 80;
                //        graphics.DrawString(TAX_CD.ToString(), new Font("Tahoma", 8),
                //                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    }
                //    else
                //    {
                //        OffsetX = OffsetX + 10;
                //    }
                //    OffsetX = OffsetX + 40;//60 80 85
                //    graphics.DrawString(satItmQty.ToString("N2"), new Font("Tahoma", 8),
                //            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //    //stringSize = graphics.MeasureString("UNIT PRICE", new Font("Consolas", 10)).ToSize().Width;
                //    //OffsetX = OffsetX + 55;//50 40
                //    //(graphics.MeasureString((SAH_TAX_INV.ToString() == "1" || SAH_IS_SVAT.ToString() == "1") ? satItmUnitRate.ToString("#,#00.00") : ((satItmTotAmt + satItmDisAmt) / satItmQty).ToString("#,#00.00"), new Font("Tahoma", 8))).Width
                //    //Math.Ceiling(Math.Log10());
                //    //RectangleF rectUnitPrice = new RectangleF(OffsetX, OffsetY, 15, 5);

                //    //527
                //    _val = (SAH_TAX_INV.ToString() == "1" || SAH_IS_SVAT.ToString() == "1") ? Convert.ToDecimal(satItmUnitRate.ToString("#,#00.00")) : Convert.ToDecimal(((satItmTotAmt + satItmDisAmt) / satItmQty).ToString("#,#00.00"));
                //    //OffsetX = Convert.ToInt16((487 + (MPC_CHNL == "ELITE" ? 80 : 0) + 26) - _val.ToString("N2").Replace(".","").Replace(",","").Length) - 8;
                //    OffsetX = Convert.ToInt16((OffsetX + 10 * 72 / 8 / 2) + (MPC_CHNL == "ELITE" ? 80 : 80) - (_val.ToString("N2").Length * 72 / 8 / 2) - 25);

                //    graphics.DrawString((SAH_TAX_INV.ToString() == "1" || SAH_IS_SVAT.ToString() == "1") ? satItmUnitRate.ToString("N2") : ((satItmTotAmt + satItmDisAmt) / satItmQty).ToString("N2"), new Font("Tahoma", 8),
                //             new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //    //stringSize = graphics.MeasureString("DISCOUNT", new Font("Consolas", 10)).ToSize().Width;
                //    //OffsetX = OffsetX + 80;//90 80
                //    //(graphics.MeasureString(totDisAmt.ToString("#,#00.00") + "" + "(" + satitmDisRate.ToString("#,#00.00") + ")%", new Font("Tahoma", 8))).Width

                //    //587
                //    _val = Convert.ToDecimal(totDisAmt.ToString("N2")).ToString("N2").Replace(".", "").Replace(",", "").Length;
                //    _val = Convert.ToDecimal(satitmDisRate.ToString("N2")).ToString("N2").Replace(".", "").Replace(",", "").Length + _val;
                //    OffsetX = Convert.ToInt16((577 + (MPC_CHNL == "ELITE" ? 80 : 0) + 5) - _val) - 8;

                //    graphics.DrawString(totDisAmt.ToString("N2") + "" + "(" + satitmDisRate.ToString("N2") + ")%", new Font("Tahoma", 8),
                //            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //    //stringSize = graphics.MeasureString("TOTAL", new Font("Consolas", 10)).ToSize().Width;
                //    //OffsetX = OffsetX + 80;//75
                //    //(graphics.MeasureString((SAH_TAX_INV.ToString() == "1" || SAH_IS_SVAT.ToString() == "1") ? (satItmTotAmt - satItmTaxAmt).ToString("#,#00.00") : satItmTotAmt.ToString("#,#00.00"), new Font("Tahoma", 8))).Width

                //    //682
                //    _val = ((SAH_TAX_INV.ToString() == "1" || SAH_IS_SVAT.ToString() == "1") ? Convert.ToDecimal((satItmTotAmt - satItmTaxAmt).ToString("#,#00.00")) : Convert.ToDecimal(satItmTotAmt.ToString("#,#00.00")));
                //    //OffsetX = Convert.ToInt16((672 + (MPC_CHNL == "ELITE" ? 80 : 0) + 2) - _val.ToString("N2").Replace(".", "").Replace(",", "").Length);
                //    OffsetX = Convert.ToInt16((OffsetX + 5 * 72 / 8 / 2) + (MPC_CHNL == "ELITE" ? 80 : 80) - (_val.ToString("N2").Length * 72 / 8 / 2) + 35);

                //    graphics.DrawString((SAH_TAX_INV.ToString() == "1" || SAH_IS_SVAT.ToString() == "1") ? (satItmTotAmt - satItmTaxAmt).ToString("#,#00.00") : satItmTotAmt.ToString("#,#00.00"), new Font("Tahoma", 8),
                //            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                //    totAmt += (SAH_TAX_INV.ToString() == "1" || SAH_IS_SVAT.ToString() == "1") ? (satItmTotAmt - satItmTaxAmt) : satItmTotAmt;
                //    //itemAmtTot.Add(totAmt);
                //    itemAmtDis.Add(totDisAmt);
                //    itemAmtTax.Add(satItmTaxAmt);

                //    OffsetY = (ditemRows["MI_SHORTDESC"].ToString().Length > 45 || (ditemRows["MI_MODEL"].ToString().Length > 12 && ditemRows["MI_SHORTDESC"].ToString().Length < 45)) ? OffsetY + 10 : OffsetY;
                //    //if (OffsetY <= 350)//340
                //    //{
                //    //    //e.HasMorePages = false;
                //    //    //_totYCount = OffsetY + _totYCount;
                //    //}
                //    //else
                //    //{
                //    //    e.HasMorePages = true;
                //    //    startX = 0;
                //    //    startY = 0;
                //    //    OffsetY = 5;
                //    //    return;
                //    //}

                //    //_newY = OffsetY + (_linecount2 * 15);
                //    //OffsetY = _newY;
                //    foreach (var m in ty)
                //    {
                //        if (m.SAD_WARR_REMARKS != null && m.SAD_WARR_REMARKS != "N/A" && isCnote == 1)
                //        {
                //            OffsetY = OffsetY + 15;//20
                //            OffsetX = 5;
                //            graphics.DrawString("Warranty:" + m.SAD_WARR_REMARKS, new Font("Tahoma", 8),
                //                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //            OffsetX = 125;
                //            graphics.DrawString(m.SAD_WARR_REMARKS, new Font("Tahoma", 8),
                //                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        }
                //    }
                //    foreach (var s in iSer)
                //    {
                //        OffsetY = OffsetY + 15;//20
                //        OffsetX = 5;
                //        graphics.DrawString(MC_CD.ToString() == "ABL" ? "Serial" : "Engine #/Chassis #", new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = 125;//105
                //        graphics.DrawString(MC_CD.ToString() == "ABL" ? s.ITS_SER_1.ToString() : (s.ITS_SER_1.ToString() + "/ " + s.ITS_SER_2.ToString()), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    }

                //    //if (OffsetY <= 350)//340
                //    //{
                //    //    //e.HasMorePages = false;
                //    //    //_totYCount = OffsetY + _totYCount;
                //    //}
                //    //else
                //    //{
                //    //    e.HasMorePages = true;
                //    //    startX = 0;
                //    //    startY = 0;
                //    //    OffsetY = 5;
                //    //    return;
                //    //}

                //    foreach (var m in ty)
                //    {
                //        if ((isCnote != 1 && MPC_OTH_REF != "CLS"))
                //        {
                //            OffsetY = OffsetY + 15;//20
                //            OffsetX = 5;
                //            graphics.DrawString("Warranty:", new Font("Tahoma", 8),
                //                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //            OffsetX = 125;
                //            graphics.DrawString((m.SAD_WARR_PERIOD.ToString() + " Month(s) ") + (m.SAD_WARR_REMARKS.ToString() == "N/A" ? "" : m.SAD_WARR_REMARKS.ToString()), new Font("Tahoma", 8),
                //                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        }

                //        if ((isCnote != 0 && MPC_OTH_REF != "CLS") && intSerWarNo != null)// && GCL_INV != null
                //        {
                //            OffsetY = OffsetY + 15;//20
                //            OffsetX = 5;
                //            graphics.DrawString("Warranty:", new Font("Tahoma", 8),
                //                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //            OffsetX = 125;
                //            graphics.DrawString(m.SAD_WARR_PERIOD.ToString() + " Month(s) " + m.SAD_WARR_REMARKS.ToString() + Environment.NewLine + "Warranty No: " + intSerWarNo.ToString() + "Warranty Commence From: " + (GCL_DATE == null ? string.Empty : GCL_DATE + "Invoice No: " + GCL_INV == null ? string.Empty : GCL_INV), new Font("Tahoma", 8),
                //                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        }
                //    }
                //    if ((GCL_INV != "" && GCL_INV != null) && (GCL_DATE != "" && GCL_DATE != null))
                //    {
                //        OffsetY = OffsetY + 15;//20
                //        OffsetX = 5;
                //        graphics.DrawString("Warranty Commence From:", new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //        OffsetX = 125;
                //        graphics.DrawString((GCL_DATE == null ? string.Empty : GCL_DATE + " " + GCL_INV == null ? string.Empty : GCL_INV), new Font("Tahoma", 8),
                //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                //    }

                //    //satitmTaxVal = 0;
                //    foreach (DataRow dSatItm in sat_itm.Rows)
                //    {
                //        satitmTaxVal += Convert.ToDecimal(dSatItm["SAD_ITM_TAX_AMT"]);
                //    }

                //    //if (sat_itm.Rows.Count > 0)
                //    //{
                //    //    sat_itm.AsEnumerable().Where(r => r.Field<string>("SAD_ITM_CD") == ditemRows["MI_CD"].ToString()).FirstOrDefault().Delete();
                //    //}
                //    //if (int_ser.Rows.Count > 0)
                //    //{
                //    //    int_ser.AsEnumerable().Where(r => r.Field<string>("its_itm_cd") == ditemRows["MI_CD"].ToString()).FirstOrDefault().Delete();
                //    //}

                //    if (sat_itm.Rows.Count > 0)
                //    {
                //        bool dataExist = sat_itm.Select().ToList().Exists(r => r["SAD_ITM_CD"].ToString() == ditemRows["MI_CD"].ToString());
                //        if (dataExist)
                //        {
                //            sat_itm.AsEnumerable().Where(r => r.Field<string>("SAD_ITM_CD") == ditemRows["MI_CD"].ToString()).FirstOrDefault().Delete();
                //        }
                //    }
                //    if (int_ser.Rows.Count > 0)
                //    {
                //        bool dataExist = int_ser.Select().ToList().Exists(r => r["its_itm_cd"].ToString() == ditemRows["MI_CD"].ToString());
                //        if (dataExist)
                //        {
                //            int_ser.AsEnumerable().Where(r => r.Field<string>("its_itm_cd") == ditemRows["MI_CD"].ToString()).FirstOrDefault().Delete();
                //        }
                //    }


                //    ditemRows.Delete();
                //    _rState = ditemRows.RowState.ToString();
                //    if (_rState == "Deleted")
                //    {
                //        ditemRows.AcceptChanges();
                //    }

                //    if (OffsetY <= 350)//340
                //    {
                //        //e.HasMorePages = false;
                //        //_totYCount = OffsetY + _totYCount;
                //    }
                //    else
                //    {
                //        e.HasMorePages = true;
                //        startX = 0;
                //        startY = 0;
                //        OffsetY = 5;
                //        return;
                //    }
                //}
                #endregion
                //totAmt = itemAmtTot.Sum(x => x);
                //totDis = itemAmtDis.Sum(x => x);
                //vat = itemAmtTax.Sum(x => x);
            }

            totDis = itemAmtDis.Sum(x => x);
            vat = itemAmtTax.Sum(x => x);

            itemAmtDis.Clear();
            itemAmtTax.Clear();

            if (OffsetY <= 350)//340
            {
                //e.HasMorePages = false;
                //_totYCount = OffsetY + _totYCount;
            }
            else
            {
                e.HasMorePages = true;
                startX = 0;
                startY = 0;
                OffsetY = 5;
                return;
            }

            if (SAH_REMARKS != null && SAH_REMARKS != "")
            {
                if (SAH_INV_TP != "HS")
                {
                    OffsetY = OffsetY + 15 + ((isCnote != 0 && MPC_OTH_REF != "CLS") ? +15 : 0);//20
                    OffsetX = 5;
                    graphics.DrawString("Remarks: " + SAH_REMARKS.ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                }
            }
            if (MPC_CHNL == "ELITE")
            {
                OffsetX = 577 + 80;
            }
            else OffsetX = 577;
            if (_TotAmt != 0)
            {
                graphics.DrawString("--------------------------------------", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }

            //List<DataRow> sat_hdr1_list = sat_hdr1.AsEnumerable().ToList();
            //foreach (DataRow dsatHdr in sat_hdr1_list) //sat_hdr1.Rows
            //{
            //    if (dsatHdr["SAH_TAX_INV"].ToString() == "1" && dsatHdr["SAH_IS_SVAT"].ToString() == "0")
            //    {
            //        vat += Convert.ToDecimal(dsatHdr["SAD_ITM_TAX_AMT"]);
            //    }
            //    else
            //        vat = 0;
            //    dsatHdr.Delete();
            //    _rState = dsatHdr.RowState.ToString();
            //    if (_rState == "Deleted")
            //       {
            //           dsatHdr.AcceptChanges();
            //       }
            //}

            if (OffsetY <= 350)//340
            {
                //e.HasMorePages = false;
                //_totYCount = OffsetY + _totYCount;
            }
            else
            {
                e.HasMorePages = true;
                startX = 0;
                startY = 0;
                OffsetY = 5;
                return;
            }

            OffsetY = OffsetY + 15;//20
            OffsetYStatic = OffsetY;
            if (sat_receiptitmCQ.Rows.Count > 0)
            {
                if (SAH_INV_TP != "HS")
                {
                    OffsetX = 5;
                    graphics.DrawString("Pay Mode", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                }
                if (SAH_INV_TP != "HS")
                {
                    OffsetX = OffsetX + 120;
                    graphics.DrawString("Ref No", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                }
                if (SAH_INV_TP != "HS")
                {
                    OffsetX = OffsetX + 100;
                    graphics.DrawString("Amount", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                }
            }
            if (totAmt != 0)
            {//455                
                OffsetX = 435;
                graphics.DrawString("Sub Totals: ", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                if (MPC_CHNL == "ELITE")
                {
                    OffsetX = 577 + 80;//587
                }
                else
                    OffsetX = 577;//587

                graphics.DrawString("--------------------------------------", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY - 7);
                graphics.DrawString(totDis.ToString("#,#00.00"), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                _val = Convert.ToDecimal(totAmt.ToString("#,#00.00"));
                OffsetX = Convert.ToInt16((672 + (MPC_CHNL == "ELITE" ? 80 : 0) + 2) - _val.ToString("N2").Replace(".", "").Replace(",", "").Length);

                graphics.DrawString(totAmt.ToString("#,#00.00"), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                _TotAmt = totAmt;
                totAmt = 0;
            }
            OffsetX = 5;
            if (sat_receiptitmCQ.Rows.Count > 0)
            {
                OffsetY = OffsetY + 5;
                graphics.DrawString("-----------------------------------------------------------------", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }
            if (SAH_INV_TP != "HS")
            {
                OffsetY = OffsetY + 10;
                List<DataRow> sat_receiptitmCQ_list = sat_receiptitmCQ.AsEnumerable().ToList();
                foreach (DataRow dsatRptItem in sat_receiptitmCQ_list) //sat_receiptitmCQ.Rows
                {
                    rptChequePayMode = dsatRptItem["SAPT_DESC"].ToString();
                    if (Convert.ToInt16(dsatRptItem["SARD_CC_PERIOD"]) > 0)
                    {
                        rptChequeRemark = "Period : " + dsatRptItem["SARD_CC_PERIOD"].ToString() + "; " + dsatRptItem["SARD_RMK"].ToString();
                    }
                    else
                        dsatRptItem["SARD_RMK"].ToString();

                    OffsetX = 5;
                    graphics.DrawString(rptChequePayMode.ToString() + " " + rptChequeRemark.ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = OffsetX + 120;
                    graphics.DrawString(dsatRptItem["SARD_REF_NO"].ToString() + " " + rptChequeRemark.ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = OffsetX + 100;
                    graphics.DrawString(Convert.ToDecimal(dsatRptItem["SARD_SETTLE_AMT"]).ToString("#,#00.00") + "  " + rptChequeRemark.ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    rptChequeTotSettleAmt += Convert.ToDecimal(dsatRptItem["SARD_SETTLE_AMT"]);
                    OffsetY = OffsetY + 10;
                    dsatRptItem.Delete();
                    _rState = dsatRptItem.RowState.ToString();
                    if (_rState == "Deleted")
                    {
                        dsatRptItem.AcceptChanges();
                    }
                }
            }
            else OffsetY = OffsetY + 10;

            //if (OffsetY <= 350)//340
            //{
            //    //e.HasMorePages = false;
            //    //_totYCount = OffsetY + _totYCount;
            //}
            //else
            //{
            //    e.HasMorePages = true;
            //    startX = 0;
            //    startY = 0;
            //    OffsetY = 5;
            //    return;
            //}

            if (run == false && (SAH_TAX_INV == 1 && SAH_IS_SVAT == 0))
            {
                OffsetX = 435;
                OffsetY = OffsetYStatic + 15;//20 //OffsetY + 10;
                graphics.DrawString("VAT", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                _val = Convert.ToDecimal(vat.ToString("#,#00.00"));
                OffsetX = Convert.ToInt16((672 + (MPC_CHNL == "ELITE" ? 80 : 0) + 2) - _val.ToString("N2").Replace(".", "").Replace(",", "").Length);

                graphics.DrawString(vat.ToString("#,#00.00"), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                run = true;
            }
            OffsetY = OffsetY + 8;
            //OffsetX = 200;
            //graphics.DrawString(rptChequeTotSettleAmt.ToString(), new Font("Tahoma", 8),
            //            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

            if (MPC_CHNL == "ELITE")
            {
                OffsetX = 577 + 80;
            }
            else OffsetX = 577;
            if (_TotAmt != 0)
            {
                graphics.DrawString("--------------------------------------", new Font("Tahoma", 8),
                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }
            OffsetY = OffsetY + 7;
            if (chktotVal == false)
            {
                OffsetX = 435;
                graphics.DrawString("Total Invoice Amount:     LKR", new Font("Tahoma", 8),
                           new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                _val = Convert.ToDecimal((SAH_TAX_INV == 1 && SAH_IS_SVAT == 0) ? (_TotAmt + vat).ToString("#,#00.00") : _TotAmt.ToString("#,#00.00"));
                OffsetX = Convert.ToInt16((672 + (MPC_CHNL == "ELITE" ? 80 : 0) + 2) - _val.ToString("N2").Replace(".", "").Replace(",", "").Length);

                graphics.DrawString(((SAH_TAX_INV == 1 && SAH_IS_SVAT == 0) ? (_TotAmt + vat).ToString("#,#00.00") : _TotAmt.ToString("#,#00.00")), new Font("Tahoma", 8), //2nd position totAmt replace with _TotAmt
                           new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                OffsetY = OffsetY + 7;
                if (MPC_CHNL == "ELITE")
                {
                    OffsetX = 577 + 80;
                }
                else
                    OffsetX = 577;//587

                graphics.DrawString("==================", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                chktotVal = true;
                _TotAmt = 0;
                totAmt = 0;
                vat = 0;
            }

            //if (SARD_SETTLE_AMT != 0)
            //{
            //    List<DataRow> sat_receiptitmCQ_copy_list = sat_receiptitmCQ_copy.AsEnumerable().ToList();
            //    foreach (DataRow dsatRptItem_copy in sat_receiptitmCQ_copy_list) //sat_receiptitmCQ.Rows
            //    {
            //        OffsetY = OffsetY + 10;
            //        OffsetX = 5;//100
            //        graphics.DrawString("Credit Card    " + dsatRptItem_copy["SARD_REF_NO"].ToString() + " " + dsatRptItem_copy["SARD_SETTLE_AMT"].ToString(), new Font("Tahoma", 8),
            //                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            //        dsatRptItem_copy.Delete();
            //        _rState = dsatRptItem_copy.RowState.ToString();
            //        if (_rState == "Deleted")
            //        {
            //            dsatRptItem_copy.AcceptChanges();
            //        }
            //    }
            //}

            if (SAH_INV_TP != "HS" && spdd_bank != null)
            {
                OffsetY = OffsetY + 15;//20
                OffsetX = 5;
                graphics.DrawString(spdd_bank == null ? string.Empty : spdd_bank.ToString() + " " + TOTAMT == null ? "0" : TOTAMT.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                spdd_bank = null;
                TOTAMT = null;
            }

            if (SAH_IS_SVAT != 0 && satitmTaxVal != 0)
            {
                OffsetY = OffsetY + 15;
                OffsetX = 435;
                graphics.DrawString("SVAT:", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                _val = Convert.ToDecimal(satitmTaxVal.ToString("#,#00.00"));
                OffsetX = Convert.ToInt16((672 + (MPC_CHNL == "ELITE" ? 80 : 0) + 2) - _val.ToString("N2").Replace(".", "").Replace(",", "").Length);

                graphics.DrawString(satitmTaxVal.ToString("#,#00.00"), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                satitmTaxVal = Convert.ToDecimal(default(decimal?));
            }
            if (hpt_acc.Rows.Count > 0)
            {
                List<DataRow> hpt_acc_list = hpt_acc.AsEnumerable().ToList();
                foreach (DataRow dhptAcc in hpt_acc_list) //hpt_acc.Rows
                {
                    OffsetY = OffsetY + 15;//20
                    OffsetX = 5;
                    graphics.DrawString("ACCOUNT DETAILS", new Font("Tahoma", 8, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetY = OffsetY + 15;//20
                    OffsetX = 5;
                    graphics.DrawString("Account No         ", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = Convert.ToInt16(300 - (dhptAcc["HPA_ACC_NO"].ToString().Length * 72 / 8));
                    graphics.DrawString(dhptAcc["HPA_ACC_NO"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                    OffsetY = OffsetY + 15;//20
                    OffsetX = 5;
                    graphics.DrawString("Scheme Code         ", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = Convert.ToInt16(300 - (dhptAcc["HPA_SCH_CD"].ToString().Length * 72 / 8) - 17);
                    graphics.DrawString(dhptAcc["HPA_SCH_CD"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                    OffsetY = OffsetY + 15;//20
                    OffsetX = 5;
                    graphics.DrawString("Term         ", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = Convert.ToInt16(300 - (dhptAcc["HPA_TERM"].ToString().Length * 72 / 8) - 22.5);
                    graphics.DrawString(dhptAcc["HPA_TERM"].ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                    OffsetY = OffsetY + 15;//20
                    OffsetX = 5;
                    graphics.DrawString("First Installment Date         ", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = Convert.ToInt16(300 - (Convert.ToDateTime(dhptAcc["HPA_ACC_CRE_DT"]).AddMonths(1).ToShortDateString().Length * 72 / 8) + 12);
                    graphics.DrawString(Convert.ToDateTime(dhptAcc["HPA_ACC_CRE_DT"]).AddMonths(1).ToShortDateString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                    OffsetY = OffsetY + 15;//20
                    OffsetX = 5;
                    //+ HTS_RNT_NO.ToString() == "1" ? HTS_RNT_VAL.ToString() : "0"
                    graphics.DrawString("First Installment Amount         ", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                    OffsetX = Convert.ToInt16(300 - (HTS_RNT_NO.ToString() == "1" ? HTS_RNT_VAL.ToString("N2").Length * 72 / 8 : "0.00".Length * 72 / 8) - 10);
                    graphics.DrawString(HTS_RNT_NO.ToString() == "1" ? HTS_RNT_VAL.ToString("N2").ToString() : "0.00", new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                    if (!(dhptAcc["HPA_PND_VOU"].ToString() == "0" || Convert.ToBoolean(dhptAcc["HPA_PND_VOU"]) == true))
                    {
                        OffsetY = OffsetY + 15;//20
                        OffsetX = 5;
                        if (dhptAcc["HPA_VOU_RMK"].ToString() != null)
                        {
                            graphics.DrawString("Special Voucher         " + dhptAcc["HPA_VOU_RMK"].ToString(), new Font("Tahoma", 8),
                                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        }
                    }
                    dhptAcc.Delete();
                    _rState = dhptAcc.RowState.ToString();
                    if (_rState == "Deleted")
                    {
                        dhptAcc.AcceptChanges();
                    }
                    OffsetY = OffsetY + 15;
                }
            }

            if (OffsetY <= 350)//340
            {
                //e.HasMorePages = false;
                //_totYCount = OffsetY + _totYCount;
            }
            else
            {
                e.HasMorePages = true;
                startX = 0;
                startY = 0;
                OffsetY = 5;
                return;
            }

            if (vouType == null)
            {
                vouValue = string.Empty;
            }
            else
            {
                if (vouType == "VALUE")
                {
                    vouValue = "- LKR :" + Amt;
                }
                else
                    vouValue = "-" + Amt.ToString();
            }

            if (stvo_gv_itm != null && vouValue != null)
            {
                vou = stvo_gv_itm + "/" + STVO_PAGENO + "" + vouValue; //want to text - STVO_PAGENO
            }
            if (spt_cond != null)
            {
                vouCond = spt_cond;
            }

            if (vou != null && vou != "")
            {
                OffsetY = OffsetY + 15;//20
                OffsetX = 5;

                graphics.DrawString("Voucher:" + vou.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY); //comment by tharanga 2018/02/28
                //graphics.DrawString("Voucher:" + "You are entitle to voucher and voucher number send to mobile number " + MBE_MOB, new Font("Tahoma", 8),
                //            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                if (vouCond != null)
                {
                    OffsetY = OffsetY + 15;//20
                    OffsetX = 5;
                    //graphics.DrawString("Voucher Condition:" + SpliceText(vouCond.ToString(), 80), new Font("Tahoma", 8),
                    //            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);

                    int i = 0;
                    OffsetY = OffsetY + 15;
                    for (i = 1; i <= (vouCond.ToString().Length / 100) + 1; i++)
                    {
                        graphics.DrawString(vouCond.ToString().Substring(((i - 1) * 100), vouCond.ToString().Length - (i - 1) * 100 < 100 ? vouCond.ToString().Length - (i - 1) * 100 : 100), new Font("Tahoma", 8),
                                    new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                        OffsetY = OffsetY + 15;
                        if (OffsetY <= 350)//340
                        {
                            //e.HasMorePages = false;
                            //_totYCount = OffsetY + _totYCount;
                        }
                        else
                        {
                            foreach (DataRow dr in sat_vou_det.Rows)
                            {
                                dr["spt_cond"] = vouCond.ToString().Substring((i - 1) * 100 + 1, vouCond.ToString().Length - (i - 1) * 100 - 1);
                                dr["stvo_gv_itm"] = null;
                            }
                            //vouCond = vouCond.ToString().Substring((i - 1) * 100 + 1, vouCond.ToString().Length - (i - 1) * 100 - 1);
                            e.HasMorePages = true;
                            startX = 0;
                            startY = 0;
                            OffsetY = 5;
                            return;
                        }
                    }
                    foreach (DataRow dr in sat_vou_det.Rows)
                    {
                        dr["spt_cond"] = null;
                    }
                }
                if (valid_from != null && valid_to != null)
                {
                    OffsetY = OffsetY + 15;//20
                    OffsetX = 5;
                    graphics.DrawString("Valid Period " + valid_from.ToString() + " to " + valid_to.ToString(), new Font("Tahoma", 8),
                                new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                }
                stvo_gv_itm = null;
                spt_cond = null;
                vouValue = null;
            }

            if (OffsetY <= 350)//340
            {
                //e.HasMorePages = false;
                //_totYCount = OffsetY + _totYCount;
            }
            else
            {
                //foreach (DataRow dr in sat_vou_det.Rows)
                //{
                //    dr["valid_from"] = null;            
                //}

                e.HasMorePages = true;
                startX = 0;
                startY = 0;
                OffsetY = 5;
                return;
            }

            if (MPC_OTH_REF == "CLS" && esep_name_initials != null)
            {
                OffsetY = OffsetY + 15;//20
                OffsetX = 5;
                graphics.DrawString("Sales Execute: " + esep_name_initials.ToString() + " " + SAH_SALES_EX_CD.ToString() + " -- " + " Sales Cashier: " + SE_USR_NAME.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                esep_name_initials = null;
            }
            if ((MPC_CHNL == "ELITE" || MPC_CHNL == "AOA_CH" || MPC_CHNL == "APPLE" || MPC_CHNL == "RMSR") && esep_name_initials != null)
            {
                OffsetY = OffsetY + 15;//20
                OffsetX = 5;
                graphics.DrawString("Sales Execute: " + esep_name_initials.ToString() + " (" + SAH_SALES_EX_CD.ToString() + ") -- " + " Sales Cashier: " + SE_USR_NAME.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                esep_name_initials = null;
            }
            if (SAH_CUS_NAME != null)
            {
                OffsetY = OffsetY + 15;//20
                OffsetX = 5;
                graphics.DrawString("Deliver To: " + SAH_CUS_NAME.ToString() + ", " + SAH_D_CUST_ADD1.ToString() + ", " + SAH_D_CUST_ADD2.ToString(), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                SAH_CUS_NAME = null;
            }
            if (ML_LOC_DESC != null)
            {
                OffsetY = OffsetY + 15;//20
                OffsetX = 5;
                graphics.DrawString("Prefered Delivery Location: " + (sah_del_loc == null ? string.Empty : sah_del_loc.ToString()) + " " + (ML_LOC_DESC == null ? string.Empty : ML_LOC_DESC.ToString()), new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
                ML_LOC_DESC = null;
            }
            if (MPC_CHNL == "ELITE")
            {
                OffsetX = 577 + 80;
            }
            else
                OffsetX = 577;
            if (SAH_ANAL_11 == 0)
            {
                graphics.DrawString("Pending Delivery", new Font("Tahoma", 8),
                            new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
            }
            OffsetY = OffsetY + 15;
            OffsetX = 5;
            graphics.DrawString("For Your Comments, inquires and complains login to wwww.abansgroup.com online contacts. Shop online at www.buyabans.com", new Font("Tahoma", 8),
                        new SolidBrush(Color.Black), startX + OffsetX, startY + OffsetY);
        }
        public Boolean checkIsDirectPrint()
        {
            HpSystemParameters _SystemPara = new HpSystemParameters();
            _SystemPara = bsObj.CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "DIRPRT_INV", DateTime.Now.Date);
            if (_SystemPara.Hsy_cd != null)
            {
                if (_SystemPara.Hsy_val == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public Boolean checkIsDirectPrintDO()
        {
            HpSystemParameters _SystemPara = new HpSystemParameters();
            _SystemPara = bsObj.CHNLSVC.Sales.GetSystemParameter("COM", BaseCls.GlbUserComCode, "DIRPRT_DO", DateTime.Now.Date);
            if (_SystemPara.Hsy_cd != null)
            {
                if (_SystemPara.Hsy_val == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public Boolean removeIsDirectPrint()
        {
            HpSystemParameters _SystemPara = new HpSystemParameters();
            _SystemPara = bsObj.CHNLSVC.Sales.GetSystemParameter("PC", BaseCls.GlbUserDefProf, "DIRPRT_INV", DateTime.Now.Date);
            if (_SystemPara.Hsy_cd != null)
            {
                if (_SystemPara.Hsy_val == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        //Tharindu 2017-11-16
        int val = 0;
        string com = string.Empty;
        string _err = string.Empty;
        string _error = string.Empty;
        public string DeliveredSalesGRNReport_Execl(DateTime fromdate, DateTime todate, string cuscode, string execcode, string type, string itmcode, string brand, string model, string cat1, string cat2, string cat3, string val, string userid, string rpttype, string itmstatus, string itmdoc, string sup, string po, string comcode, string com, int export)
        {
            DataTable param = new DataTable();
            DataTable DEL_SER = new DataTable();
            DataTable dtheader = new DataTable();

            string _filePath = string.Empty;
            DataRow dr;
            int i = 0;

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    i = i + 1;
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.GetDeliveredSalesGRNDetails_Execl(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportExecCode, BaseCls.GlbReportDocType, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel, BaseCls.GlbReportItemCat1, BaseCls.GlbReportItemCat2, BaseCls.GlbReportItemCat3, Convert.ToString(i), BaseCls.GlbUserID, BaseCls.GlbReportType, BaseCls.GlbReportItemStatus, BaseCls.GlbReportDoc, BaseCls.GlbReportSupplier, BaseCls.GlbReportPurchaseOrder, BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportIsExport, out _err);

                    GLOB_DataTable.Merge(TMP_DataTable);
                    DataTable TMP_DataTable_NEW = TMP_DataTable.DefaultView.ToTable(true, "do_no", "item_code");

                    if (TMP_DataTable_NEW.Rows.Count > 0)
                    {
                        foreach (DataRow drow1 in TMP_DataTable_NEW.Rows)
                        {
                            DataTable TMP_DataTable_SER = new DataTable();
                            TMP_DataTable_SER = bsObj.CHNLSVC.MsgPortal.GetDeliveredSerDetails(drow1["do_no"].ToString(), drow1["item_code"].ToString(), BaseCls.GlbUserID, BaseCls.GlbReportIsExport, 0);
                            DEL_SER.Merge(TMP_DataTable_SER);

                            // joininh the tables 
                            var resultingTable = from t1 in TMP_DataTable.AsEnumerable()
                                                 join t2 in TMP_DataTable_SER.AsEnumerable()
                                                     on t1.Field<string>("DO_NO") equals t2.Field<string>("ITS_DOC_NO")
                                                 select new
                                                 {
                                                     lOCATION = string.IsNullOrEmpty(t1["DO_LOC_DESC"].ToString()) ? "NA" : t1["DO_LOC_DESC"],
                                                     INVOICE = string.IsNullOrEmpty(t1["INV_NO"].ToString()) ? "NA" : (string)t1["INV_NO"],
                                                     DO_NO = string.IsNullOrEmpty(t1["DO_NO"].ToString()) ? "N/A" : (string)t1["DO_NO"],
                                                     ITEM_CODE = string.IsNullOrEmpty(t1["ITEM_CODE"].ToString()) ? "N/A" : (string)t1["ITEM_CODE"],
                                                     INVOICE_DATE = t1["INV_DATE"] != null ? (DateTime)t1["INV_DATE"] : DateTime.Now,
                                                     DO_DATE = t1["DO_DATE"] != null ? (DateTime)t1["DO_DATE"] : DateTime.Now,
                                                     GRNNO = string.IsNullOrEmpty(t1["GRNNO"].ToString()) ? "N/A" : (string)t1["GRNNO"],
                                                     MANUVAL = string.IsNullOrEmpty(t1["MODEL"].ToString()) ? "N/A" : (string)t1["MODEL"],
                                                     GRN_DATE = t1["GRNDATE"] != null ? (DateTime)t1["GRNDATE"] : DateTime.Now,
                                                     BRAND = string.IsNullOrEmpty(t1["BRAND"].ToString()) ? "N/A" : (string)t1["BRAND"],
                                                     PONO = string.IsNullOrEmpty(t1["PONO"].ToString()) ? "N/A" : (string)t1["PONO"],
                                                     QTY = t1["QTY"] != null ? (decimal)t1["QTY"] : 0,
                                                     PO_DATE = t1["PODATE"] != null ? (DateTime)t1["PODATE"] : DateTime.Now,
                                                     SUPP_CODE = string.IsNullOrEmpty(t1["SUPP_CODE"].ToString()) ? "N/A" : (string)t1["SUPP_CODE"],
                                                     SUPP_NAME = string.IsNullOrEmpty(t1["SUPP_NAME"].ToString()) ? "N/A" : (string)t1["SUPP_NAME"],
                                                     ITS_SER_1 = string.IsNullOrEmpty(t2["ITS_SER_1"].ToString()) ? "N/A" : (string)t2["ITS_SER_1"],
                                                 };

                            #region Adding Coloumns
                            DataColumnCollection columns = param.Columns;
                            if (!columns.Contains("DO_LOC_DESC"))
                            {
                                param.Columns.Add("DO_LOC_DESC", typeof(string));
                            }
                            if (!columns.Contains("INV_NO"))
                            {
                                param.Columns.Add("INV_NO", typeof(string));
                            }
                            if (!columns.Contains("DO_NO"))
                            {
                                param.Columns.Add("DO_NO", typeof(string));
                            }
                            if (!columns.Contains("ITEM_CODE"))
                            {
                                param.Columns.Add("ITEM_CODE", typeof(string));
                            }
                            if (!columns.Contains("INV_DATE"))
                            {
                                param.Columns.Add("INV_DATE", typeof(string));
                            }
                            if (!columns.Contains("DO_DATE"))
                            {
                                param.Columns.Add("DO_DATE", typeof(DateTime));
                            }
                            if (!columns.Contains("GRNNO"))
                            {
                                param.Columns.Add("GRNNO", typeof(string));
                            }
                            if (!columns.Contains("MODEL"))
                            {
                                param.Columns.Add("MODEL", typeof(string));
                            }
                            if (!columns.Contains("GRNDATE"))
                            {
                                param.Columns.Add("GRNDATE", typeof(DateTime));
                            }
                            if (!columns.Contains("BRAND"))
                            {
                                param.Columns.Add("BRAND", typeof(string));
                            }
                            if (!columns.Contains("PONO"))
                            {
                                param.Columns.Add("PONO", typeof(string));
                            }
                            if (!columns.Contains("QTY"))
                            {
                                param.Columns.Add("QTY", typeof(decimal));
                            }
                            if (!columns.Contains("PODATE"))
                            {
                                param.Columns.Add("PODATE", typeof(DateTime));
                            }
                            if (!columns.Contains("SUPP_CODE"))
                            {
                                param.Columns.Add("SUPP_CODE", typeof(string));
                            }
                            if (!columns.Contains("SUPP_NAME"))
                            {
                                param.Columns.Add("SUPP_NAME", typeof(string));
                            }
                            if (!columns.Contains("ITS_SER_1"))
                            {
                                param.Columns.Add("ITS_SER_1", typeof(string));
                            }
                            #endregion

                            #region Fetch Data to Datatble
                            foreach (var item in resultingTable)
                            {
                                DataRow dr1 = param.NewRow();
                                dr1["DO_LOC_DESC"] = item.lOCATION;
                                dr1["INV_NO"] = item.INVOICE;
                                dr1["DO_NO"] = item.DO_NO;
                                dr1["ITEM_CODE"] = item.ITEM_CODE;
                                dr1["INV_DATE"] = item.INVOICE_DATE;
                                dr1["DO_DATE"] = item.DO_DATE;
                                dr1["GRNNO"] = item.GRNNO;
                                dr1["MODEL"] = item.MANUVAL;
                                dr1["GRNDATE"] = item.GRN_DATE;
                                dr1["BRAND"] = item.BRAND;
                                dr1["PONO"] = item.PONO;
                                dr1["QTY"] = item.QTY;
                                dr1["PODATE"] = item.PO_DATE;
                                dr1["SUPP_CODE"] = item.SUPP_CODE;
                                dr1["SUPP_NAME"] = item.SUPP_NAME;
                                dr1["ITS_SER_1"] = item.ITS_SER_1;
                                param.Rows.Add(dr1);
                            }
                            #endregion
                        }

                        #region Execl ExportPath
                        param.TableName = "tbl";

                        DataColumnCollection titcolumns = dtheader.Columns;
                        if (!titcolumns.Contains("Title"))
                        {
                            dtheader.Columns.Add("Title", typeof(string));
                            dtheader.Rows.Add("Deliverd Sales Quantity Report");
                            dtheader.TableName = "ABC";
                        }



                        _err = "";
                        _filePath = "";
                        //  _filePath = bsObj.CHNLSVC.MsgPortal.ExportExcel2007(BaseCls.GlbUserComCode, BaseCls.GlbUserID, param, out _err);
                        _filePath = bsObj.CHNLSVC.MsgPortal.ExportExcel2007WithHDR(BaseCls.GlbUserComCode, BaseCls.GlbUserID, dtheader, param, out _err, "Y");
                        if (GLOB_DataTable.Rows.Count == 0)
                        {
                            _err = "No Records Found.";
                        }
                        #endregion

                    }

                }
            }

            return _filePath;
        }

        public void AgeAnalysisOfDebtorsOutstanding_vehical_reg()//add by tharanga 2017/11/10
        {
            DataTable mst_com = new DataTable();
            DataTable glob_debt_age = new DataTable();
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
            dr["user"] = BaseCls.GlbUserID;
            dr["fromdate"] = BaseCls.GlbReportFromDate;
            dr["todate"] = BaseCls.GlbReportToDate;
            dr["profitcenter"] = BaseCls.GlbReportProfit;
            dr["comp"] = BaseCls.GlbReportComp;
            dr["compaddr"] = BaseCls.GlbReportCompAddr;
            param.Rows.Add(dr);

            MasterCompany _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

            dr = mst_com.NewRow();
            dr["MC_CD"] = BaseCls.GlbReportComp;   // row["MC_CD"].ToString();
            dr["MC_IT_POWERED"] = _MasterComp.Mc_it_powered.ToString();

            mst_com.Rows.Add(dr);

            // glob_debt_age = bsObj.CHNLSVC.Financial.Process_Age_Anal_Debt_Outstand(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, null, BaseCls.GlbUserID, BaseCls.GlbRecType);

            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPcRptDB(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable tmp_debt_age = new DataTable();
                    Debug.Print(drow["tpl_pc"].ToString());
                    tmp_debt_age = bsObj.CHNLSVC.Financial.Process_Age_Anal_Debt_Outstand_veh_re(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbReportCompCode, drow["tpl_pc"].ToString(), BaseCls.GlbUserID, BaseCls.GlbRecType, BaseCls.GlbReportParaLine1);
                    glob_debt_age.Merge(tmp_debt_age);

                }
            }

            if (BaseCls.GlbReportName == "Age_Debtor_Outstanding_Veh_reg.rpt")
            {
                _Age_Debtor_Outstanding_Veh_reg.Database.Tables["mst_com"].SetDataSource(mst_com);
                _Age_Debtor_Outstanding_Veh_reg.Database.Tables["SP_PROCESS_AGE_DEBT_OUTSTAND"].SetDataSource(glob_debt_age);
                _Age_Debtor_Outstanding_Veh_reg.Database.Tables["param"].SetDataSource(param);
            }

        }

        public void GPSummaryReportNew()//add by tharanga 2017/12/11
        {

            DataTable param = new DataTable();
            DataRow dr;
            GLOB_DataTable = new DataTable();
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            if (tmp_user_pc.Rows.Count > 0)
            {
                foreach (DataRow drow in tmp_user_pc.Rows)
                {
                    DataTable TMP_DataTable = new DataTable();
                    TMP_DataTable = bsObj.CHNLSVC.MsgPortal.Get_gp_report(BaseCls.GlbUserComCode, drow["tpl_pc"].ToString(), BaseCls.GlbReportFromDate, BaseCls.GlbReportToDate, BaseCls.GlbReportItemCat1
                        , BaseCls.GlbReportItemCat2, BaseCls.GlbReportExecCode, BaseCls.GlbReportCustomerCode, BaseCls.GlbReportItemCode, BaseCls.GlbReportBrand, BaseCls.GlbReportModel
                        , BaseCls.GlbReportItemCat3, BaseCls.GlbReportItemCat4, BaseCls.GlbReportItemCat5);
                    GLOB_DataTable.Merge(TMP_DataTable);
                }
            }


            MasterCompany _MasterComp = null;
            _MasterComp = CacheLayer.Get<MasterCompany>(CacheLayer.Key.CompanyInfor.ToString());

            param.Clear();

            param.Columns.Add("PARA_USER", typeof(string));
            param.Columns.Add("PARA_PCENTER", typeof(string));
            param.Columns.Add("PARA_HEADING", typeof(string));
            param.Columns.Add("PARA_PERIOD", typeof(string));
            param.Columns.Add("PARA_ITCODE", typeof(string));
            param.Columns.Add("PARA_CAT1", typeof(string));
            param.Columns.Add("PARA_CAT2", typeof(string));
            param.Columns.Add("PARA_BRAND", typeof(string));
            param.Columns.Add("PARA_MODEL", typeof(string));
            param.Columns.Add("PARA_CUST", typeof(string));
            param.Columns.Add("PARA_EXEC", typeof(string));
            param.Columns.Add("PARA_CAT3", typeof(string));
            param.Columns.Add("PARA_CAT4", typeof(string));
            param.Columns.Add("PARA_CAT5", typeof(string));
            param.Columns.Add("MC_DESC", typeof(string));
            param.Columns.Add("MC_ADD1", typeof(string));
            param.Columns.Add("MC_ADD2", typeof(string));
            param.Columns.Add("MC_TEL", typeof(string));
            param.Columns.Add("MC_IT_POWERED", typeof(string));

            dr = param.NewRow();
            dr["PARA_USER"] = BaseCls.GlbUserID;
            dr["PARA_PCENTER"] = BaseCls.GlbReportProfit == "" ? "ALL" : BaseCls.GlbReportProfit;
            dr["PARA_HEADING"] = "Item wise GP Summary  Report (With Free Item)";
            dr["PARA_PERIOD"] = "From " + Convert.ToString(BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy")) + " " + "To " + Convert.ToString(BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy"));
            dr["PARA_ITCODE"] = BaseCls.GlbReportItemCode == "" ? "ALL" : BaseCls.GlbReportItemCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_CAT1"] = BaseCls.GlbReportItemCat1 == "" ? "ALL" : BaseCls.GlbReportItemCat1.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_CAT2"] = BaseCls.GlbReportItemCat2 == "" ? "ALL" : BaseCls.GlbReportItemCat2.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_BRAND"] = BaseCls.GlbReportBrand == "" ? "ALL" : BaseCls.GlbReportBrand.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_MODEL"] = BaseCls.GlbReportModel == "" ? "ALL" : BaseCls.GlbReportModel.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_CUST"] = BaseCls.GlbReportCustomerCode == "" ? "ALL" : BaseCls.GlbReportCustomerCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_EXEC"] = BaseCls.GlbReportExecCode == "" ? "ALL" : BaseCls.GlbReportExecCode.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_CAT3"] = BaseCls.GlbReportItemCat3 == "" ? "ALL" : BaseCls.GlbReportItemCat3.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_CAT4"] = BaseCls.GlbReportItemCat4 == "" ? "ALL" : BaseCls.GlbReportItemCat4.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["PARA_CAT5"] = BaseCls.GlbReportItemCat5 == "" ? "ALL" : BaseCls.GlbReportItemCat5.Replace('$', ' ').Replace('^', ' ').Replace('|', ',');
            dr["MC_DESC"] = _MasterComp.Mc_desc.ToString();
            dr["MC_ADD1"] = _MasterComp.Mc_add1.ToString();
            dr["MC_ADD2"] = _MasterComp.Mc_add2.ToString();
            dr["MC_TEL"] = _MasterComp.Mc_tel.ToString();
            dr["MC_IT_POWERED"] = _MasterComp.Mc_it_powered.ToString();
            param.Rows.Add(dr);

            _GP_Report.Database.Tables["GP_DET"].SetDataSource(GLOB_DataTable);
            _GP_Report.Database.Tables["REP_PARA"].SetDataSource(param);

        }

        public void BillCollection()//Wimal 11/09/2018
        {

            DataTable param = new DataTable();
            DataRow dr;
            string pcList = "";
            tmp_user_pc = bsObj.CHNLSVC.Sales.GetTempUserPc(BaseCls.GlbUserComCode, BaseCls.GlbUserID);

            foreach (DataRow drpc in tmp_user_pc.Rows)
            {
                pcList = pcList + drpc["TPL_PC"] + ",";
            }

            param.Clear();

            param.Columns.Add("user", typeof(string));
            param.Columns.Add("heading_1", typeof(string));
            param.Columns.Add("profitcenter", typeof(string));
            param.Columns.Add("period", typeof(string));

            dr = param.NewRow();
            dr["user"] = BaseCls.GlbUserID;
            dr["heading_1"] = BaseCls.GlbReportHeading;
            dr["profitcenter"] = pcList;
            dr["period"] = "From " + Convert.ToString(BaseCls.GlbReportFromDate.Date.ToString("dd/MMM/yyyy")) + " " + "To " + Convert.ToString(BaseCls.GlbReportToDate.Date.ToString("dd/MMM/yyyy")); ;
            param.Rows.Add(dr);

            // dr = param.NewRow();

            DataTable TMP_DataTable = bsObj.CHNLSVC.MsgPortal.Get_Bill_Collection(Convert.ToDateTime(BaseCls.GlbReportFromDate).Date, Convert.ToDateTime(BaseCls.GlbReportToDate).Date, BaseCls.GlbUserComCode, "", BaseCls.GlbUserID);
            DataTable TMP_DataTable1 = new DataTable();
            _billcoll_dtl.Database.Tables["billCollection"].SetDataSource(TMP_DataTable);
            _billcoll_summ.Database.Tables["billCollection"].SetDataSource(TMP_DataTable);
            _billcoll_summ.Database.Tables["param"].SetDataSource(param);

        }
    }

}
